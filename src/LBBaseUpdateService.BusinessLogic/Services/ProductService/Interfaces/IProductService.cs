using LBBaseUpdateService.BusinessLogic.Services.Models;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Models;

namespace LBBaseUpdateService.BusinessLogic.Services.ProductService.Interfaces
{
    public interface IProductService
    {
        void ChangeFieldVendorIdAndVendorCountry(Product[] products, VendorId[] vendors);
        void ChangeFieldVibration(Product[] products);
        void ChangeFieldNewAndBest(Product[] products);

        void SetMainCategoryId(Product[] products, Category[] categories); // TODO: Think about it. How does better this method?

        void ChangeFieldIeId(Product[] products, ProductIdWithInternalId[] ieId);
        Product[] GetProductSheetToUpdate(Product[] externalProducts, Product[] internalProducts);
    }
}