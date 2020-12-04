using System.Threading.Tasks;
using BitrixService.Models.ApiModels;

namespace BitrixService.Clients.Stripmag.Interfaces
{
    public interface IStripmagClient
    {
        Task<ProductFromSupplierAto[]> GetProductsFromSupplierAsync();
        Task<OfferAto[]> GetOffersFromSupplierAsync();
        Task<VendorAto[]> GetVendorsFromSupplierAsync();
    }
}