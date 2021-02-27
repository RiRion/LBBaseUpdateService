using System;
using System.Threading.Tasks;
using BitrixService.Models.ApiModels;
using BitrixService.Models.ApiModels.ResponseModels;

namespace BitrixService.Clients.Loveberi.Interfaces
{
    public interface ILoveberiClient
    {
        void Login();
        
        Task<VendorIdAto[]> GetVendorsInternalIdWithExternalIdAsync();
        
        Task<VendorAto[]> GetVendorsAsync();

        Task<ApiResponse> AddVendor(VendorAto vendorAto);

        Task<ApiResponse> AddVendorWithRetryAsync(VendorAto vendorAto, int countRetry = 10);
        
        [Obsolete]
        Task AddVendorsAsync(VendorAto[] vendors);
        
        [Obsolete]
        Task AddVendorsWithStepAsync(VendorAto[] vendors, int step = 100);
        
        [Obsolete]
        Task DeleteVendorsAsync(int[] ids);
        
        [Obsolete]
        Task DeleteWithStepVendors(int[] ids, int step = 100);
        
        Task<ProductAto[]> GetAllProductsAsync();
        
        Task<ProductIdWithInternalIdAto[]> GetProductIdWithIeIdAsync();
        
        Task<CategoryAto[]> GetCategoriesAsync();

        Task<ApiResponse> AddProduct(ProductAto productAto);

        Task<ApiResponse> AddProductWithRetryAsync(ProductAto productAto, int countRetry = 10);
        
        Task AddProductsRangeAsync(ProductAto[] products);

        [Obsolete]
        Task AddProductsRangeWithStepAsync(ProductAto[] products, int step = 100);

        Task<ApiResponse> UpdateProduct(ProductAto productAto);

        Task<ApiResponse> UpdateProductWithRetryAsync(ProductAto productAto, int countRetry = 10);
        
        Task UpdateProductsAsync(ProductAto[] products);

        [Obsolete]
        Task UpdateProductsWithStepAsync(ProductAto[] products, int step = 100);

        Task<ApiResponse> DeleteProduct(int exId);

        Task<ApiResponse> DeleteProductWithRetryAsync(int exId, int countRetry = 10);
        
        Task DeleteProductsAsync(int[] ids);
        
        [Obsolete]
        Task DeleteProductsWithStepAsync(int[] ids, int step = 100);
        
        Task<OfferAto[]> GetAllOffersAsync();

        Task<ApiResponse> AddOfferAsync(OfferAto offer);

        Task<ApiResponse> AddOfferWithRetryAsync(OfferAto offer, int countRetry = 10);
        
        Task AddOffersRangeAsync(OfferAto[] offers);

        [Obsolete]
        Task AddOffersRangeWithStepAsync(OfferAto[] offers, int step = 100);

        Task<ApiResponse> UpdateOfferAsync(OfferAto offer);
        
        Task<ApiResponse> UpdateOfferWithRetryAsync(OfferAto offer, int countRetry = 10);
        
        Task UpdateOffersAsync(OfferAto[] offers);

        [Obsolete]
        Task UpdateOffersWithStepAsync(OfferAto[] offers, int step = 100);

        Task<ApiResponse> DeleteOfferAsync(int exId);

        Task<ApiResponse> DeleteOfferWithRetry(int exId, int countRetry = 10);
        
        Task DeleteOffersAsync(int[] ids);
        
        [Obsolete]
        Task DeleteOffersWithStepAsync(int[] ids, int step = 100);
    }
}