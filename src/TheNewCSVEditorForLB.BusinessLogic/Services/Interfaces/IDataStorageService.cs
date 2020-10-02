using System;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models;
using TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Models;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Interfaces
{
	public interface IDataStorageService
	{
		Product[] GetProductsFromFile(String filePath);
		VendorId[] GetVendorsFromJson(string content);
		VendorId[] GetVendorsFromFile(String filePath);
		void SaveProductsToFile(Product[] products, String filePath);
		void SaveVendorsToFile(VendorId[] vendors, String filePath);
	}
}