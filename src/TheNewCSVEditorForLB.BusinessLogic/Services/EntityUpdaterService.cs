using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TheNewCSVEditorForLB.BusinessLogic.Services.Comparators;
using TheNewCSVEditorForLB.BusinessLogic.Services.Interfaces;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage;

namespace TheNewCSVEditorForLB.BusinessLogic.Services
{
	public class EntityUpdaterService : IEntityUpdater
	{
		// IEntityUpdater ////////////////////////////////////////////////////////////////////////////
		public VendorsId[] ChangeFieldVendorIdAndVendorCountry(ProductDb[] products, VendorsId[] vendors)
		{
			var newVendors = new List<VendorsId>();

			foreach(var item in products)
			{
				item.VendorCountry = item.VendorId;
				if(vendors.Any(x => x.ImportVendorId == item.VendorId))
				{
					item.VendorId = vendors.First(x => x.ImportVendorId.Equals(item.VendorId)).CorrectVendorId;
				}
				else
				{
					newVendors.Add(new VendorsId { CorrectVendorId = 0, ImportVendorId = item.VendorId });
				}
			}

			newVendors.Sort(new VendorDictionaryComp());

			return newVendors.ToArray();
		}
		public void ChangeFieldVibration(ProductDb[] products)
		{
			foreach(var item in products)
			{
				if(item.Vibration == "1")
					item.Vibration = "Есть";
				if(item.Vibration == "0")
					item.Vibration = "Нет";
			}
		}
		public void ChangeFieldNewAndBest(ProductDb[] products)
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

		/// <summary>
		/// Set internal category id to field Category in Product.
		/// </summary>
		/// <param name="products"></param>
		/// <param name="categories"></param>
		/// <returns>
		///Products containing new categories.
		/// </returns>
		public ProductDb[] SetCategoryId(ProductDb[] products, CategoriesWithId[] categories)
		{
			var productsWithNewCategories = new List<ProductDb>();
			foreach (var product in products)
			{
				var searchCategory = new List<Categories>();
				for (int i = 1; i <= 3; i++)
				{
					PropertyInfo info = product.Categories.GetType().GetProperty("Categories" + i);
					if (info == null) continue;
					var obj = info.GetValue(product.Categories);
					if (obj is string)
					{
						var name = (string)obj;

						if (i == 1) 
					}
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
		public ProductDb[] ChangeFieldIeId(ProductDb[] products, ProductIdWithIntarnalId[] ieId)
		{
			var newProducts = new List<ProductDb>();

			foreach(var item in products)
			{
				if(ieId.Any(x => x.ProductId == item.ProductId))
				{
					item.IeId = ieId.First(x => x.ProductId.Equals(item.ProductId)).IeId.ToString();
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