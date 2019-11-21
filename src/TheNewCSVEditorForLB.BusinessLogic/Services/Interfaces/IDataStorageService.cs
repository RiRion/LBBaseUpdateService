using System;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Interfaces
{
	public interface IDataStorageService
	{
		Product[] GetProducts(String filePath);
		VendorsWithProductId[] GetVendors(String filePath);
		void SaveProducts(Product[] products, String filePath);
		void SaveVendors(VendorsWithProductId[] vendors, String filePath);
	}
}