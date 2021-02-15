using System;
using System.Collections.Generic;
using System.Linq;
using LBBaseUpdateService.BusinessLogic.Services.Models;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Models;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Exceptions;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Interfaces;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Models;

namespace LBBaseUpdateService.BusinessLogic.Services.ProductService
{
    public class ProductService : IProductService
    {
        public void ChangeFieldVendorIdAndVendorCountry(Product[] products, VendorId[] vendors)
        {
            foreach(var product in products)
            {
                product.VendorCountry = product.VendorId;
                var vendor = vendors.FirstOrDefault(x => x.ExternalId.Equals(product.VendorId));
                if (vendor != null)
                {
	                product.VendorId = vendor.InternalId;
                }
                else throw new VendorIdNotFoundException($"Vendor with provided ID {product.VendorId} not found.");
            }
        }
        
        public void ChangeFieldVibration(Product[] products)
        {
            foreach(var product in products)
            {
                if(product.Vibration == "1")
                    product.Vibration = "Есть";
                if(product.Vibration == "0")
                    product.Vibration = "Нет";
            }
        }
        
        public void ChangeFieldOffers(Product[] products)
        {
            foreach(var item in products)
            {
	            if (item.New == "1" & item.Bestseller == "1")
	            {
		            item.Offers.Add((int)OurOffers.New);
		            item.Offers.Add((int)OurOffers.Best);
	            }
	            else if(item.New == "1")
                    item.Offers.Add((int)OurOffers.New);
                else if (item.Bestseller == "1")
	                item.Offers.Add((int)OurOffers.Best);
            }
        }

        public void SetDiscount(Product[] products, Offer[] offers)
        {
	        foreach (var product in products)
	        {
		        var offer = offers.FirstOrDefault(o => o.ProductExId.Equals(product.ProductExId));
		        if (offer != null)
		        {
			        if (offer.Price * 0.9 - offer.BaseWholePrice > 500)
			        {
				        product.Sale = 1;
				        product.Offers.Add((int)OurOffers.Discount10);
				        product.Offers.Add((int)OurOffers.Sale);
			        }
			        else if (offer.Price * 0.8 - offer.BaseWholePrice > 500)
			        {
				        product.Sale = 2;
				        product.Offers.Add((int)OurOffers.Discount20);
				        product.Offers.Add((int)OurOffers.Sale);
			        }
			        else if (offer.Price * 0.7 - offer.BaseWholePrice > 500)
			        {
				        product.Sale = 3;
				        product.Offers.Add((int)OurOffers.Discount30);
				        product.Offers.Add((int)OurOffers.Sale);
			        }
			        else if (offer.Price * 0.6 - offer.BaseWholePrice > 500)
			        {
				        product.Sale = 4;
				        product.Offers.Add((int)OurOffers.Discount40);
				        product.Offers.Add((int)OurOffers.Sale);
			        }
		        }
	        }
        }
        
        public void SetMainCategoryId(Product[] products, Category[] categories)
        		{
        			foreach (var product in products)
                    {
	                    for (int i = 0; i < product.Categories.Count(); i++)
	                    {
		                    if (!String.IsNullOrEmpty(product.Categories.ElementAt(i).Name))
		                    {
			                    if (i != 0)
				                    product.Categories.ElementAt(i).ParentId = product.Categories.ElementAt(i - 1).Id;
			                    SetCategoryId(product.Categories.ElementAt(i), categories);
		                    }
		                    else break;
	                    }

	                    product.CategoryId = product.Categories
		                    .Last(c => !String.IsNullOrEmpty(c.Name))
		                    .Id;
                    }
        		}
        
        public void ChangeFieldIeId(Product[] products, ProductIdWithInternalId[] ieId)
        {
	        foreach(var product in products)
	        {
		        var prodId = ieId.FirstOrDefault(x => x.ProductExId.Equals(product.ProductExId));
		        if (prodId != null) product.ProductIeId = prodId.ProductIeId;
	        }
        }

        public Product[] GetProductListToUpdate(Product[] externalProducts, Product[] internalProducts)
        {
	        var updateSheet = new List<Product>();
	        foreach (var product in internalProducts)
	        {
		        var vendorProduct = externalProducts.FirstOrDefault(p => p.ProductExId == product.ProductExId);
		        if (vendorProduct != null && !vendorProduct.Equals(product))
		        {
			        updateSheet.Add(vendorProduct);
		        }
	        }

	        return updateSheet.ToArray();
        }

        public void SetCategoryId(Category category, Category[] categoriesFromSite)
        {
	        category.Name = category.Name.Trim();
	        var categoryFromSite = categoriesFromSite.FirstOrDefault(
		        c => c.Name.Equals(category.Name) 
		             && (c.ParentId == category.ParentId || c.ParentId == 0)
	        );
	        if (categoryFromSite != null)
	        {
		        category.ParentId = categoryFromSite.ParentId;
		        category.Id = categoryFromSite.Id;
	        }
	        else throw new CategoryNotFoundException($"Category with provided name \"{category.Name}\" not found.");
        }
    }
}