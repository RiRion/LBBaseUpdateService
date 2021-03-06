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
using BitrixService.Models.ApiModels.ResponseModels;

namespace BitrixService.Clients.Loveberi
{
    public class LoveberiClient : TypedHttpClient, ILoveberiClient
    {
        // BitrixClient FIELDS /////////////////////////////////////////////////////////////////////////////////////////
        private readonly LoveberiClientConfig _clientConfig;

        // VENDORS Paths ///////////////////////////////////////////////////////////////////////////////////////////////
        private const string AddVendorPath = "/AddVendor";
        private const string UpdateVendorPath = "/UpdateVendor";
        private const string DeleteVendor = "/DeleteVendor";

        private const string GetVendorsInternalIdWithExternalIdPath = "/GetVendorsId";
        private const string GetVendorsPath = "/GetVendors";
        private const string AddVendorsPath = "/AddVendors";
        private const string DeleteVendorsPath = "/DeleteVendors";

        // PRODUCTS Paths //////////////////////////////////////////////////////////////////////////////////////////////
        private const string AddProductPath = "/AddProduct";
        private const string UpdateProductPath = "/UpdateProduct";
        private const string DeleteProductPath = "/DeleteProduct";
        
        private const string GetAllProductsPath = "/GetAllProducts";
        private const string ProductIdWithIeIdPath = "/ProductIdWithIeId";
        private const string GetCategoriesPath = "/GetCategories";
        private const string AddProductsRangePath = "/AddProductsRange";
        private const string DeleteProductsRangePath = "/DeleteProductsRange";
        private const string UpdateProductsRangePath = "/UpdateProductsRange";
        
        // OFFERS Paths ////////////////////////////////////////////////////////////////////////////////////////////////
        private const string AddOfferPath = "/AddOffer";
        private const string UpdateOfferPath = "/UpdateOffer";
        private const string DeleteOfferPath = "/DeleteOffer";
        
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

        public async Task<ApiResponse> AddVendor(VendorAto vendorAto)
        {
            using var response = await PostObjectAsync(BaseAddress + AddVendorPath, vendorAto);
            return await DeserializeAsync<ApiResponse>(response, SerializerSettings);
        }

        public async Task<ApiResponse> AddVendorWithRetryAsync(VendorAto vendorAto, int countRetry = 10)
        {
            return await SendWithRetryAsync(vendorAto, AddVendor, countRetry);
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
        
        public async Task AddVendorsWithStepAsync(VendorAto[] vendors, int step = 50)
        {
            Console.Write("Добавление новых поставщиков: ");
            await SendWithStep(vendors, AddVendorsAsync, step);
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

        public async Task DeleteWithStepVendors(int[] ids, int step = 50)
        {
            Console.Write("Удаление ненужных поставщиков: ");
            await SendWithStep(ids, DeleteVendorsAsync, step);
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

        public async Task<ApiResponse> AddProduct(ProductAto productAto)
        {
            using var response = await PostObjectAsync(BaseAddress + AddProductPath, productAto);
            var str = await response.Content.ReadAsStringAsync();
            return await DeserializeAsync<ApiResponse>(response, SerializerSettings);
        }

        public async Task<ApiResponse> AddProductWithRetryAsync(ProductAto productAto, int countRetry = 10)
        {
            return await SendWithRetryAsync(productAto, AddProduct, countRetry);
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

        public async Task AddProductsRangeWithStepAsync(ProductAto[] products, int step = 50)
        {
            Console.Write("Добавление товаров: ");
            await SendWithStep(products, AddProductsRangeAsync, step);
            Console.WriteLine("Завершено.");
        }

        public async Task<ApiResponse> UpdateProduct(ProductAto productAto)
        {
            using var response = await PostObjectAsync(BaseAddress + UpdateProductPath, productAto);
            return await DeserializeAsync<ApiResponse>(response, SerializerSettings);
        }

        public async Task<ApiResponse> UpdateProductWithRetryAsync(ProductAto productAto, int countRetry = 10)
        {
            return await SendWithRetryAsync(productAto, UpdateProduct, countRetry);
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

        public async Task UpdateProductsWithStepAsync(ProductAto[] products, int step = 50)
        {
            Console.Write("Обновление продуктов: ");
            await SendWithStep(products, UpdateProductsAsync, step);
            Console.WriteLine("Завершено.");
        }

        public async Task<ApiResponse> DeleteProduct(int exId)
        {
            using var response = await PostObjectAsync(BaseAddress + DeleteProductPath, exId);
            return await DeserializeAsync<ApiResponse>(response, SerializerSettings);
        }

        public async Task<ApiResponse> DeleteProductWithRetryAsync(int exId, int countRetry = 10)
        {
            return await SendWithRetryAsync(exId, DeleteProduct, countRetry);
        }
        
        public async Task DeleteProductsAsync(int[] ids)
        {
            using (var response = PostObjectAsync(BaseAddress + DeleteProductsRangePath, ids))
            {
                await response.Result.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteProductsWithStepAsync(int[] ids, int step = 50)
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

        public async Task<ApiResponse> AddOfferAsync(OfferAto offer)
        {
            using var response = await PostObjectAsync(BaseAddress + AddOfferPath, offer);
            return await DeserializeAsync<ApiResponse>(response, SerializerSettings);
        }

        public async Task<ApiResponse> AddOfferWithRetryAsync(OfferAto offer, int countRetry = 10)
        {
            return await SendWithRetryAsync(offer, AddOfferAsync, countRetry);
        }

        public async Task AddOffersRangeAsync(OfferAto[] offers)
        {
            using (var response = PostObjectAsync(BaseAddress + AddOffersRangPath, offers))
            {
                await response.Result.Content.ReadAsStringAsync();
            }
        }

        public async Task AddOffersRangeWithStepAsync(OfferAto[] offers, int step = 50)
        {
            Console.Write("Добавление новых торговых предложений: ");
            await SendWithStep(offers, AddOffersRangeAsync, step);
            Console.WriteLine("Завершено.");
        }

        public async Task<ApiResponse> UpdateOfferAsync(OfferAto offer)
        {
            using var response = await PostObjectAsync(BaseAddress +  UpdateOfferPath, offer);
            var str = await response.Content.ReadAsStringAsync();
            return await DeserializeAsync<ApiResponse>(response, SerializerSettings);
        }

        public async Task<ApiResponse> UpdateOfferWithRetryAsync(OfferAto offer, int countRetry = 10)
        {
            return await SendWithRetryAsync(offer, UpdateOfferAsync, countRetry);
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

        public async Task UpdateOffersWithStepAsync(OfferAto[] offers, int step = 50)
        {
            Console.Write("Обновление торговых предложений: ");
            await SendWithStep(offers, UpdateOffersAsync, step);
            Console.WriteLine("Завершено.");
        }

        public async Task<ApiResponse> DeleteOfferAsync(int exId)
        {
            using var response = await PostObjectAsync(BaseAddress + DeleteOfferPath, exId);
            return await DeserializeAsync<ApiResponse>(response, SerializerSettings);
        }

        public async Task<ApiResponse> DeleteOfferWithRetry(int exId, int countRetry = 10)
        {
            return await SendWithRetryAsync(exId, DeleteOfferAsync, countRetry);
        }
        
        public async Task DeleteOffersAsync(int[] ids)
        {
            using (var response = PostObjectAsync(BaseAddress + DeleteOffersPath, ids))
            {
                await response.Result.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteOffersWithStepAsync(int[] ids, int step = 50)
        {
            Console.Write("Удаление неактуальных торговых предложений: ");
            await SendWithStep(ids, DeleteOffersAsync, step);
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

        private async Task<ApiResponse> SendWithRetryAsync<T>(T obj, Func<T, Task<ApiResponse>> action, int countRetry = 10)
        {
            var currentRetry = 0;
            ApiResponse response = new ApiResponse();
            do
            {
                try
                {
                    response = await action(obj);
                    break;
                }
                catch (Exception e)
                {
                    await Task.Delay(new TimeSpan(0, 0, 10));
                    currentRetry++;
                    if (currentRetry == countRetry) throw;
                }
            } while (currentRetry < countRetry);

            return response;
        }
    }
}