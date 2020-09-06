using System;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Interfaces
{
	public interface IDataStorageService
	{
		ProductDb[] GetProductsFromFile(String filePath);
		VendorsId[] GetVendorsFromJson(string content);
		VendorsId[] GetVendorsFromFile(String filePath);
		void SaveProductsToFile(ProductDb[] products, String filePath);
		void SaveVendorsToFile(VendorsId[] vendors, String filePath);
	}
}