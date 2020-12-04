using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BitrixService.Clients.Loveberi.Exceptions;
using BitrixService.Clients.Loveberi.Interfaces;
using BitrixService.Clients.Loveberi.Models.Config;
using BitrixService.Models.ApiModels;
using BitrixService.Clients.TypedHttp;
using BitrixService.ConsoleProgressBar;

namespace BitrixService.Clients.Loveberi
{
    public class LoveberiClient : TypedHttpClient, ILoveberiClient
    {
        // BitrixClient FIELDS /////////////////////////////////////////////////////////////////////////////////////////
        private readonly LoveberiClientConfig _clientConfig;

        // VENDORS Paths ///////////////////////////////////////////////////////////////////////////////////////////////
        private const string GetVendorsInternalIdWithExternalIdPath = "/GetVendorsId";
        private const string GetVendorsPath = "/GetVendors";
        private const string AddVendorsPath = "/AddVendors";
        private const string DeleteVendorsPath = "/DeleteVendors";

        // PRODUCTS Paths //////////////////////////////////////////////////////////////////////////////////////////////
        private const string GetAllProductsPath = "/GetAllProducts";
        private const string ProductIdWithIeIdPath = "/ProductIdWithIeId";
        private const string GetCategoriesPath = "/GetCategories";
        private const string AddProductsRangePath = "/AddProductsRange";
        private const string DeleteProductsRangePath = "/DeleteProductsRange";
        private const string UpdateProductsRangePath = "/UpdateProductsRange";
        
        // OFFERS Paths ////////////////////////////////////////////////////////////////////////////////////////////////
        private const string GetAllOffersPath = "/GetAllOffers";
        private const string AddOffersRangPath = "/AddOffersRange";
        private const string UpdateOffersPath = "/UpdateOffers";
        private const string DeleteOffersPath = "/DeleteOffers";

        public LoveberiClient(LoveberiClientConfig clientConfig)
        {
            _clientConfig = clientConfig;
            BaseAddress = new Uri(clientConfig.BaseUri + clientConfig.BasePath);
        }
        
        // FUNCTIONS ///////////////////////////////////////////////////////////////////////////////////////////////////
        
        public void Login()
        { 
            var authData = Encoding.ASCII.GetBytes($"{_clientConfig.Login}:{_clientConfig.Password}"); 
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(authData)
            );
            using (var response = GetAsync(BaseAddress + "/Login").GetAwaiter().GetResult())
            {
                if (!response.IsSuccessStatusCode) throw new AuthentificationFailException("Authentification fail.");
            }
        }
        
        // VENDORS /////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<VendorIdAto[]> GetVendorsInternalIdWithExternalIdAsync()
        {
            return await GetObjectAsync<VendorIdAto[]>(BaseAddress + GetVendorsInternalIdWithExternalIdPath);
        }

        public async Task<VendorAto[]> GetVendorsAsync()
        {
            return await GetObjectAsync<VendorAto[]>(BaseAddress + GetVendorsPath);
        }

        public async Task AddVendorsAsync(VendorAto[] vendors)
        {
            using (var response = await PostObjectAsync(BaseAddress + AddVendorsPath, vendors))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException($"Status code {response.StatusCode}, message: {response.Content.ReadAsStringAsync()}.");
                }
            }
        }

        public async Task AddVendorsWithStepAsync(VendorAto[] vendors, int step = 100)
        {
            Console.Write("Добавление новых поставщиков: ");
            await SendWithStep(vendors, AddVendorsAsync, 100);
            Console.WriteLine("Завершено.");
        }

        public async Task DeleteVendorsAsync(int[] ids)
        {
            using (var response = await DeleteObjectAsync(BaseAddress + DeleteVendorsPath, ids))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException($"Status code {response.StatusCode}, message: {response.Content.ReadAsStringAsync()}.");
                }
            }
        }

        public async Task DeleteWithStepVendors(int[] ids, int step = 100)
        {
            Console.Write("Удаление ненужных поставщиков: ");
            await SendWithStep(ids, DeleteVendorsAsync, 100);
            Console.WriteLine("Завершено.");
        }
        
        // PRODUCT /////////////////////////////////////////////////////////////////////////////////////////////////////
        
        public async Task<ProductAto[]> GetAllProductsAsync()
        {
            return await GetObjectAsync<ProductAto[]>(BaseAddress + GetAllProductsPath);
        }
        
        public async Task<ProductIdWithInternalIdAto[]> GetProductIdWithIeIdAsync()
        {
            return await GetObjectAsync<ProductIdWithInternalIdAto[]>(BaseAddress + ProductIdWithIeIdPath);
        }

        public async Task<CategoryAto[]> GetCategoriesAsync()
        {
            return await GetObjectAsync<CategoryAto[]>(BaseAddress + GetCategoriesPath);
        }

        public async Task AddProductsRangeAsync(ProductAto[] products)
        {
            using (var response = await PostObjectAsync(BaseAddress + AddProductsRangePath, products))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException($"Status code {response.StatusCode}, message: {response.Content.ReadAsStringAsync()}.");
                }
                await response.Content.ReadAsStringAsync();
            }
        }

        public async Task AddProductsRangeWithStepAsync(ProductAto[] products, int step = 100)
        {
            Console.Write("Добавление товаров: ");
            await SendWithStep(products, AddProductsRangeAsync, step);
            Console.WriteLine("Завершено.");
        }

        public async Task UpdateProductsAsync(ProductAto[] products)
        {
            using (var response = await PostObjectAsync(BaseAddress + UpdateProductsRangePath, products))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException($"Status code {response.StatusCode}, message: {response.Content.ReadAsStringAsync()}.");
                }
                await response.Content.ReadAsStringAsync();   
            }
        }

        public async Task UpdateProductsWithStepAsync(ProductAto[] products, int step = 100)
        {
            Console.Write("Обновление продуктов: ");
            await SendWithStep(products, UpdateProductsAsync, step);
            Console.WriteLine("Завершено.");
        }
        
        public async Task DeleteProductsAsync(int[] ids)
        {
            using (var response = PostObjectAsync(BaseAddress + DeleteProductsRangePath, ids))
            {
                await response.Result.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteProductsWithStepAsync(int[] ids, int step = 100)
        {
            Console.Write("Удаление устаревших продуктов: ");
            await SendWithStep(ids, DeleteProductsAsync, step);
            Console.WriteLine("Завершено.");
        }
        
        // OFFERS //////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<OfferAto[]> GetAllOffersAsync()
        {
            return await GetObjectAsync<OfferAto[]>(BaseAddress + GetAllOffersPath);
        }

        public async Task AddOffersRangeAsync(OfferAto[] offers)
        {
            using (var response = PostObjectAsync(BaseAddress + AddOffersRangPath, offers))
            {
                await response.Result.Content.ReadAsStringAsync();
            }
        }

        public async Task AddOffersRangeWithStepAsync(OfferAto[] offers, int step = 100)
        {
            Console.Write("Добавление новых торговых предложений: ");
            await SendWithStep(offers, AddOffersRangeAsync, step);
            Console.WriteLine("Завершено.");
        }

        public async Task UpdateOffersAsync(OfferAto[] offers)
        {
            using (var response = await PostObjectAsync(BaseAddress + UpdateOffersPath, offers))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException($"Status code {response.StatusCode}, message: {response.Content.ReadAsStringAsync()}.");
                }

                await response.Content.ReadAsStringAsync();
            }
        }

        public async Task UpdateOffersWithStepAsync(OfferAto[] offers, int step = 100)
        {
            Console.Write("Обновление торговых предложений: ");
            await SendWithStep(offers, UpdateOffersAsync, 100);
            Console.WriteLine("Завершено.");
        }

        public async Task DeleteOffersAsync(int[] ids)
        {
            using (var response = PostObjectAsync(BaseAddress + DeleteOffersPath, ids))
            {
                await response.Result.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteOffersWithStepAsync(int[] ids, int step = 100)
        {
            Console.Write("Обновление торговых предложений: ");
            await SendWithStep(ids, DeleteOffersAsync, 100);
            Console.WriteLine("Завершено.");
        }
        
        // SUPPORT /////////////////////////////////////////////////////////////////////////////////////////////////////
        
        private async Task SendWithStep<T>(T[] list, Func<T[], Task> action, int step)
        {
            var i = 0;

            using (var progressBar = new ProgressBar())
            {
                do
                {
                    if (list.Length - i < step)
                    {
                        step = list.Length - i;
                        i = list.Length;
                    }
                    else i += step;
                    await action(list.Skip(i - step).Take(step).ToArray());
                    progressBar.Report((double) i/ list.Length);
                } while (i < list.Length); 
            }
        }
    }
}