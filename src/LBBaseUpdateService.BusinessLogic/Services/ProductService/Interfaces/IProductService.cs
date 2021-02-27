using System.Collections.Generic;
using LBBaseUpdateService.BusinessLogic.Services.Models;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Models;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Models;

namespace LBBaseUpdateService.BusinessLogic.Services.ProductService.Interfaces
{
    public interface IProductService
    {
        void ChangeFieldVendorIdAndVendorCountry(List<Product> products, VendorId[] vendors);
        void ChangeFieldVibration(List<Product> products);
        void ChangeFieldOffers(List<Product> products);
        void SetDiscount(List<Product> products, List<Offer> offers);
        void SetMainCategoryId(List<Product> products, List<Category> categories); // TODO: Think about it. How does better this method?

        void ChangeFieldIeId(List<Product> products, ProductIdWithInternalId[] ieId);
        Product[] GetProductListToUpdate(List<Product> externalProducts, List<Product> internalProducts);
    }
}