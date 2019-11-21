using Autofac;
using System;
using System.Threading.Tasks;
using TheNewCSVEditorForLB.BusinessLogic.ExternalClients.LoveBeri.Interfaces;
using TheNewCSVEditorForLB.BusinessLogic.Services.Interfaces;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Config;

namespace TheNewCSVEditorForLB.Headless
{
	internal sealed class ApplicationContext : IDisposable
	{
		private readonly ILifetimeScope _lifetimeScope;
		private readonly ApplicationConfig _config;
		private readonly IDataStorageService _dataStorageService;
		private readonly IEntityUpdater _entityUpdater;
		private readonly ILoveBeriClient _loveBeriClient;


		public ApplicationContext(
			ILifetimeScope lifetimeScope,
			ApplicationConfig config,
			IDataStorageService dataStorageService,
			IEntityUpdater entityUpdater,
			ILoveBeriClient loveBeriClient)
		{
			_lifetimeScope = lifetimeScope;
			_config = config;
			_dataStorageService = dataStorageService;
			_entityUpdater = entityUpdater;
			_loveBeriClient = loveBeriClient;
		}


		// FUNCTIONS ////////////////////////////////////////////////////////////////////////////////////
		public async Task RunAsync()
		{
			var products = _dataStorageService.GetProducts(_config.BitrixFilePath);
			var vendors = _dataStorageService.GetVendors(_config.VendorsFilePath);

			var newVendors = _entityUpdater.ChangeFieldVendorIdAndVendorCountry(products, vendors);
			if(newVendors.Length > 0)
			{
				_dataStorageService.SaveVendors(newVendors, _config.NewVendorsFilePath);

				Console.WriteLine("New vendor ID found. new_vendors.csv was created.\r\n");
			}

			var ieId = await _loveBeriClient.GetInternalIdAsync();
			var newProducts = _entityUpdater.ChangeFieldIeId(products, ieId);
			if(newProducts.Length > 0)
			{
				_dataStorageService.SaveProducts(newProducts, _config.NewProductsFilePath);

				Console.WriteLine("New product ID found. new_productId.csv was created.\r\n");
			}

			_entityUpdater.ChangeFieldVibration(products);
			_entityUpdater.ChangeFieldNewAndBest(products);

			_dataStorageService.SaveProducts(products, _config.NewBaseFilePath);
		}


		// IDisposable ////////////////////////////////////////////////////////////////////////////
		public void Dispose()
		{
			_lifetimeScope?.Dispose();
		}
	}
}