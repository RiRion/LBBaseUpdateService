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
        
        public Product[] SetCategoryId(Product[] products, Category[] categories) // TODO: Think about it. How does better this method?
        		{
        			var productsWithNewCategories = new List<Product>();
        			foreach (var product in products)
                    {
	                    product.Categories.Category1.Name = product.Categories.Category1.Name.Trim();
        				var internalCategory1 = categories.FirstOrDefault(
        						c => c.Name == product.Categories.Category1.Name && c.ParentId == 0);
        				if (internalCategory1 != null) product.Categories.Category1 = internalCategory1;
        				else
        				{
        					productsWithNewCategories.Add(product);
        					continue;
        				}
        				
        				if (String.IsNullOrEmpty(product.Categories.Category2.Name))
        				{
        					product.Categories.CategoryId = product.Categories.Category1.Id;
        					continue;
        				}

                        product.Categories.Category2.Name = product.Categories.Category2.Name.Trim();
        				var internalCategory2 = categories.FirstOrDefault(
        						c => c.Name == product.Categories.Category2.Name 
                                     && c.ParentId == product.Categories.Category1.Id
        						);
        				if (internalCategory2 != null) product.Categories.Category2 = internalCategory2;
        				else
        				{
        					productsWithNewCategories.Add(product);
        					continue;
        				}
        				
        				if (String.IsNullOrEmpty(product.Categories.Category3.Name))
        				{
        					product.Categories.CategoryId = product.Categories.Category2.Id;
        					continue;
        				}

                        product.Categories.Category3.Name = product.Categories.Category3.Name.Trim();
        				var internalCategory3 = categories.FirstOrDefault(
        						c => c.Name == product.Categories.Category3.Name 
                                     && c.ParentId == product.Categories.Category2.Id
        					);
        				if (internalCategory3 != null)
        				{
        					product.Categories.Category3 = internalCategory3;
        					product.Categories.CategoryId = product.Categories.Category3.Id;
        				}
        				else
        				{
        					productsWithNewCategories.Add(product);
        				}
        			}
        
        			return productsWithNewCategories.ToArray();
        		}
        
        public void ChangeFieldIeId(Product[] products, ProductIdWithInternalId[] ieId)
        {
	        foreach(var item in products)
	        {
		        var prodId = ieId.FirstOrDefault(x => x.ProductId.Equals(item.ProductId));
		        if (prodId != null) item.IeId = prodId.IeId;
	        }
        }

        public Product[] GetProductSheetToUpdate(Product[] vendorProducts, Product[] internalProducts)
        {
	        var updateSheet = new List<Product>();
	        foreach (var product in internalProducts)
	        {
		        var vendorProduct = vendorProducts.FirstOrDefault(p => p.ProductId == product.ProductId);
		        if (vendorProduct != null && !vendorProduct.Equals(product))
		        {
			        updateSheet.Add(vendorProduct);
		        }
	        }

	        return updateSheet.ToArray();
        }
    }
}