using System;
using System.Collections.Generic;
using System.Linq;
using LBBaseUpdateService.BusinessLogic.Services.Models;
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
        
        public void ChangeFieldNewAndBest(Product[] products)
        {
            foreach(var item in products)
            {
                if(item.New == "1" & item.Bestseller == "1")
                    item.NewAndBestseller = "Новинка Хит продаж";
                else if(item.New == "1")
                    item.NewAndBestseller = "Новинка";
                else if (item.Bestseller == "1")
	                item.NewAndBestseller = "Хит продаж";
                else item.NewAndBestseller = "";
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
	        foreach(var item in products)
	        {
		        var prodId = ieId.FirstOrDefault(x => x.ProductId.Equals(item.ProductId));
		        if (prodId != null) item.IeId = prodId.IeId;
	        }
        }

        public Product[] GetProductSheetToUpdate(Product[] externalProducts, Product[] internalProducts)
        {
	        var updateSheet = new List<Product>();
	        foreach (var product in internalProducts)
	        {
		        var vendorProduct = externalProducts.FirstOrDefault(p => p.ProductId == product.ProductId);
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