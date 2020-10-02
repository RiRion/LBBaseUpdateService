using TheNewCSVEditorForLB.BusinessLogic.Services.Models;
using TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Models;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Interfaces
{
	public interface IEntityUpdater
	{
		VendorId[] ChangeFieldVendorIdAndVendorCountry(Product[] products, VendorId[] vendors);
		void ChangeFieldVibration(Product[] products);
		void ChangeFieldNewAndBest(Product[] products);
		Product[] ChangeFieldIeId(Product[] products, ProductIdWithInternalId[] ieId);
		Product[] SetCategoryId(Product[] products, Category[] categories);
	}
}