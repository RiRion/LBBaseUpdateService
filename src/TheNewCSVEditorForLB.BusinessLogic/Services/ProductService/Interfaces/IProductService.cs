using TheNewCSVEditorForLB.BusinessLogic.Services.Models;
using TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Models;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Interfaces
{
    public interface IProductService
    {
        void ChangeFieldVendorIdAndVendorCountry(Product[] products, VendorId[] vendors);
        void ChangeFieldVibration(Product[] products);
        void ChangeFieldNewAndBest(Product[] products);

        Product[] SetCategoryId(Product[] products, Category[] categories) // TODO: Think about it. How does better this method?
            ;

        void ChangeFieldIeId(Product[] products, ProductIdWithInternalId[] ieId);
        Product[] GetProductSheetToUpdate(Product[] vendorProducts, Product[] internalProducts);
    }
}