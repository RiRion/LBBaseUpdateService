using System;
using System.Collections.Generic;
using System.Linq;
using TheNewCSVEditorForLB.BusinessLogic.Services.Interfaces;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models;
using TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Comparators;
using TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Models;

namespace TheNewCSVEditorForLB.BusinessLogic.Services
{
	public class EntityUpdaterService : IEntityUpdater
	{
		// IEntityUpdater ////////////////////////////////////////////////////////////////////////////
		public VendorId[] ChangeFieldVendorIdAndVendorCountry(Product[] products, VendorId[] vendors)
		{
			// var newVendors = new List<VendorId>();
			//
			// foreach(var item in products)
			// {
			// 	item.VendorCountry = item.VendorId;
			// 	if(vendors.Any(x => x.ExternalId == item.VendorId))
			// 	{
			// 		item.VendorId = vendors.First(x => x.ExternalId.Equals(item.VendorId)).InternalId;
			// 	}
			// 	else
			// 	{
			// 		newVendors.Add(new VendorId { InternalId = 0, ExternalId = item.VendorId });
			// 	}
			// }
			//
			// newVendors.Sort(new VendorIdComparer());
			//
			// return newVendors.Distinct(new VendorIdComparer()).ToArray();
			return new VendorId[]{};
		}
		public void ChangeFieldVibration(Product[] products)
		{
			foreach(var item in products)
			{
				if(item.Vibration == "1")
					item.Vibration = "Есть";
				if(item.Vibration == "0")
					item.Vibration = "Нет";
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
				else if(item.Bestseller == "1")
					item.NewAndBestseller = "Хит продаж";
			}
		}
		public Product[] SetCategoryId(Product[] products, Category[] categories) // TODO: Think about it. How does better this method?
		{
			var productsWithNewCategories = new List<Product>();
			foreach (var product in products)
			{
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
				var internalCategory2 =
					categories.FirstOrDefault(
						c => 
							c.Name == product.Categories.Category2.Name && c.ParentId == product.Categories.Category1.Id
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
				var internalCategory3 =
					categories.FirstOrDefault(
						c => 
							c.Name == product.Categories.Category3.Name && c.ParentId == product.Categories.Category2.Id
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

		/// <summary>
		/// Returns a list of new products.
		/// </summary>
		/// <param name="products"></param>
		/// <param name="ieId"></param>
		/// <returns></returns>
		public Product[] ChangeFieldIeId(Product[] products, ProductIdWithInternalId[] ieId)
		{
			var newProducts = new List<Product>();

			foreach(var item in products)
			{
				if(ieId.Any(x => x.ProductId == item.ProductId))
				{
					item.IeId = ieId.First(x => x.ProductId.Equals(item.ProductId)).IeId;
				}
				else
				{
					newProducts.Add(item);
				}
			}

			return newProducts.ToArray();
		}
	}
}