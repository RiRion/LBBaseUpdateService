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
        
        Task AddVendorsAsync(VendorAto[] vendors);
        
        Task AddVendorsWithStepAsync(VendorAto[] vendors, int step = 100);
        
        Task DeleteVendorsAsync(int[] ids);
        
        Task DeleteWithStepVendors(int[] ids, int step = 100);
        
        Task<ProductAto[]> GetAllProductsAsync();
        
        Task<ProductIdWithInternalIdAto[]> GetProductIdWithIeIdAsync();
        
        Task<CategoryAto[]> GetCategoriesAsync();
        
        Task AddProductsRangeAsync(ProductAto[] products);

        Task AddProductsRangeWithStepAsync(ProductAto[] products, int step = 100);
        
        Task UpdateProductsAsync(ProductAto[] products);

        Task UpdateProductsWithStepAsync(ProductAto[] products, int step = 100);
        
        Task DeleteProductsAsync(int[] ids);
        
        Task DeleteProductsWithStepAsync(int[] ids, int step = 100);
        
        Task<OfferAto[]> GetAllOffersAsync();

        Task<ApiResponse> AddOfferAsync(OfferAto offer);

        Task<ApiResponse> AddOfferWithRetryAsync(OfferAto offer, int countRetry = 10);
        
        Task AddOffersRangeAsync(OfferAto[] offers);

        Task AddOffersRangeWithStepAsync(OfferAto[] offers, int step = 100);

        Task<ApiResponse> UpdateOfferAsync(OfferAto offer);
        
        Task<ApiResponse> UpdateOfferWithRetryAsync(OfferAto offer, int countRetry = 10);
        
        Task UpdateOffersAsync(OfferAto[] offers);

        Task UpdateOffersWithStepAsync(OfferAto[] offers, int step = 100);

        Task<ApiResponse> DeleteOfferAsync(OfferAto offer);

        Task<ApiResponse> DeleteOfferWithRetry(OfferAto offer, int countRetry = 10);
        
        Task DeleteOffersAsync(int[] ids);
        
        Task DeleteOffersWithStepAsync(int[] ids, int step = 100);
    }
}