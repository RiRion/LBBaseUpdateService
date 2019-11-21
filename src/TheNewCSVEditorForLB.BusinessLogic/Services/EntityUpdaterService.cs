using System.Collections.Generic;
using System.Linq;
using TheNewCSVEditorForLB.BusinessLogic.Services.Comparators;
using TheNewCSVEditorForLB.BusinessLogic.Services.Interfaces;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage;

namespace TheNewCSVEditorForLB.BusinessLogic.Services
{
	public class EntityUpdaterService : IEntityUpdater
	{
		// IEntityUpdater ////////////////////////////////////////////////////////////////////////////
		public VendorsWithProductId[] ChangeFieldVendorIdAndVendorCountry(Product[] products, VendorsWithProductId[] vendors)
		{
			var newVendors = new List<VendorsWithProductId>();

			foreach(var item in products)
			{
				item.VendorCountry = item.VendorId;
				if(vendors.Any(x => x.ImportVendorId == item.VendorId))
				{
					item.VendorId = vendors.First(x => x.ImportVendorId.Equals(item.VendorId)).CorrectVendorId;
				}
				else
				{
					newVendors.Add(new VendorsWithProductId { CorrectVendorId = 0, ImportVendorId = item.VendorId });
				}
			}

			newVendors.Sort(new VendorDictionaryComp());

			return newVendors.ToArray();
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
		public Product[] ChangeFieldIeId(Product[] products, ProductIdWithIntarnalId[] ieId)
		{
			var newProducts = new List<Product>();

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