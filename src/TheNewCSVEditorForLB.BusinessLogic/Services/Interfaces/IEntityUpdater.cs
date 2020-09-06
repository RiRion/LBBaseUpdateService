using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Interfaces
{
	public interface IEntityUpdater
	{
		VendorsId[] ChangeFieldVendorIdAndVendorCountry(ProductDb[] products, VendorsId[] vendors);
		void ChangeFieldVibration(ProductDb[] products);
		void ChangeFieldNewAndBest(ProductDb[] products);
		ProductDb[] ChangeFieldIeId(ProductDb[] products, ProductIdWithIntarnalId[] ieId);
		ProductDb[] SetCategoryId(ProductDb[] products, CategoriesWithId[] categories);
	}
}