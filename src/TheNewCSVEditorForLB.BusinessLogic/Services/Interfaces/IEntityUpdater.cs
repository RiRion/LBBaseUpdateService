using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Interfaces
{
	public interface IEntityUpdater
	{
		VendorsWithProductId[] ChangeFieldVendorIdAndVendorCountry(Product[] products, VendorsWithProductId[] vendors);
		void ChangeFieldVibration(Product[] products);
		void ChangeFieldNewAndBest(Product[] products);
		Product[] ChangeFieldIeId(Product[] products, ProductIdWithIntarnalId[] ieId);
	}
}