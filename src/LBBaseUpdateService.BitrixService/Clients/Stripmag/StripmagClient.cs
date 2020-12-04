using System;
using System.Threading.Tasks;
using BitrixService.Clients.Stripmag.Interfaces;
using BitrixService.Clients.Stripmag.Models.Config;
using BitrixService.Clients.TypedHttp;
using BitrixService.Models.ApiModels;
using CsvHelper.Configuration;

namespace BitrixService.Clients.Stripmag
{
    public class StripmagClient : TypedHttpClient, IStripmagClient
    {
        private readonly StripmagClientConfig _clientConfig;
        private readonly CsvConfiguration _csvConfiguration;

        public StripmagClient(StripmagClientConfig clientConfig, CsvConfiguration csvConfiguration)
        {
            _clientConfig = clientConfig;
            _csvConfiguration = csvConfiguration;
            BaseAddress = new Uri(clientConfig.BaseUri);
        }

        public async Task<ProductFromSupplierAto[]> GetProductsFromSupplierAsync()
        {
            return await GetCsvObjectAsync<ProductFromSupplierAto>(
                BaseAddress + _clientConfig.ProductPath, _csvConfiguration);
        }
        
        public async Task<OfferAto[]> GetOffersFromSupplierAsync()
        {
            return await GetCsvObjectAsync<OfferAto>(BaseAddress + _clientConfig.OfferPath, _csvConfiguration);
        }

        public async Task<VendorAto[]> GetVendorsFromSupplierAsync()
        {
            return await GetCsvObjectAsync<VendorAto>(BaseAddress + _clientConfig.VendorPath, _csvConfiguration);
        }
    }
}