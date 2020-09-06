using Autofac;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using BitrixService.Models;
using BitrixService.Services;
using Newtonsoft.Json;
using TheNewCSVEditorForLB.BusinessLogic.Services.Interfaces;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Config;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage;

namespace TheNewCSVEditorForLB.Headless
{
	internal sealed class ApplicationContext : IDisposable
	{
		private readonly ILifetimeScope _lifetimeScope;
		private readonly ApplicationConfig _config;
		private readonly IDataStorageService _dataStorageService;
		private readonly IEntityUpdater _entityUpdater;
		private readonly ApiService _apiService;
		private readonly IMapper _mapper;


		public ApplicationContext(
			ILifetimeScope lifetimeScope,
			ApplicationConfig config,
			IDataStorageService dataStorageService,
			IEntityUpdater entityUpdater,
			ApiService apiService,
			IMapper mapper
		)
		{
			_lifetimeScope = lifetimeScope;
			_config = config;
			_dataStorageService = dataStorageService;
			_entityUpdater = entityUpdater;
			_apiService = apiService;
			_mapper = mapper;
		}


		// FUNCTIONS ////////////////////////////////////////////////////////////////////////////////////
		public async Task RunAsync()
		{
			var products = _dataStorageService.GetProductsFromFile(_config.BitrixFilePath);
			var vendors = _dataStorageService.GetVendorsFromFile(_config.VendorsFilePath);

			var newVendors = _entityUpdater.ChangeFieldVendorIdAndVendorCountry(products, vendors);
			if(newVendors.Length > 0)
			{
				//_dataStorageService.SaveVendorsToFile(newVendors, _config.NewVendorsFilePath);

				Console.WriteLine("New vendor ID found. new_vendors.csv was created.\r\n");
			}

			var ieId = JsonConvert.DeserializeObject<ProductIdWithIntarnalId[]>(
				await _apiService.ProductIdWithIeIdAsync());
			var newProducts = _entityUpdater.ChangeFieldIeId(products, ieId);
			if(newProducts.Length > 0)
			{
				//_dataStorageService.SaveProductsToFile(newProducts, _config.NewProductsFilePath);

				Console.WriteLine("New product ID found. new_productId.csv was created.\r\n");
			}

			_entityUpdater.ChangeFieldVibration(products);
			_entityUpdater.ChangeFieldNewAndBest(products);
			var categoriesWithId =
				JsonConvert.DeserializeObject<CategoriesWithId[]>(await _apiService.GetCategoriesAsync());
			var productsWithNewCategories = _entityUpdater.SetCategoryId(products, categoriesWithId);

			//_dataStorageService.SaveProductsToFile(products, _config.NewBaseFilePath);
			//Test BitrixService.
			var sw = new Stopwatch();
			sw.Start();
			SendToBitrixService(products.Skip(100).Take(100).ToArray());
			sw.Stop();
			Console.WriteLine(sw.ElapsedMilliseconds/1000);
		}
		
		///<summary>
		///Test BitrixService. Send request to api.
		/// </summary>
		private void SendToBitrixService(ProductDb[] products)
		{
			var content = JsonConvert.SerializeObject(_mapper.Map<ProductDto[]>(products));
			var response = _apiService.AddProductsRangeAsync(content).GetAwaiter().GetResult(); 
			// var response = _apiService.GetCategoriesAsync().GetAwaiter().GetResult();
			// var response = api.GetAllProductsAsync().GetAwaiter().GetResult();
			// var response = api.DeleteAsync(new int[] {266402, 266403}).GetAwaiter().GetResult();
			Console.WriteLine(response);
		}
		//End Test.

		// IDisposable ////////////////////////////////////////////////////////////////////////////
		public void Dispose()
		{
			_lifetimeScope?.Dispose();
		}
	}
}