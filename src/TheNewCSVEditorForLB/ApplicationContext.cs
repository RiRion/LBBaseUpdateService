using Autofac;
using System;
using System.IO;
using System.Threading.Tasks;
using TheNewCSVEditorForLB.BusinessLogic.ExternalClients.LoveBeri.Interfaces;
using TheNewCSVEditorForLB.BusinessLogic.Services.Interfaces;
using TheNewCSVEditorForLB.Models;
using TheNewCSVEditorForLB.Models.Config;

namespace TheNewCSVEditorForLB
{
	internal sealed class ApplicationContext : IDisposable
	{
		private readonly ILifetimeScope _lifetimeScope;
		private readonly IVendorsWithProductIdRepository _vendorsWithProductIdRepository;
		private readonly IProductIdWithInternalIdRepository _productIdWithInternalIdRepository;
		private readonly ApplicationConfig _config;
		private readonly String _test;
		private readonly IDataStorageService _dataStorageService;
		private readonly IEntityUpdater _entityUpdater;
		private readonly DataSourceDto _dataSource;
		private readonly ILoveBeriClient _loveBeriClient;


		public ApplicationContext(
			ILifetimeScope lifetimeScope,
			IVendorsWithProductIdRepository vendorsWithProductIdRepository,
			IProductIdWithInternalIdRepository productIdWithInternalIdRepository,
			ApplicationConfig config,
			String test,
			IDataStorageService dataStorageService,
			IEntityUpdater entityUpdater,
			DataSourceDto dataSource, ILoveBeriClient loveBeriClient)
		{
			_lifetimeScope = lifetimeScope;
			_vendorsWithProductIdRepository = vendorsWithProductIdRepository;
			_productIdWithInternalIdRepository = productIdWithInternalIdRepository;
			_config = config;
			_test = test;
			_dataStorageService = dataStorageService;
			_entityUpdater = entityUpdater;
			_dataSource = dataSource;
			_loveBeriClient = loveBeriClient;
		}


		// FUNCTIONS ////////////////////////////////////////////////////////////////////////////////////
		public async Task RunAsync()
		{
			var products = _dataStorageService.GetProducts(_dataSource.BitrixFilePath);
			var vendors = _dataStorageService.GetVendors(_dataSource.VendorDictionaryFilePath);

			var newVendors = _entityUpdater.ChangeFieldVendorIdAndVendorCountry(products, vendors);
			if(newVendors.Length > 0)
			{
				var parentDirectoryPath = Directory.GetParent(_dataSource.VendorDictionaryFilePath).FullName;
				var newFilePath = Path.Combine(parentDirectoryPath, "new_vendors.csv");

				_dataStorageService.SaveVendors(newVendors, newFilePath);

				Console.WriteLine("\nNew vendor ID found. new_vendors.csv was created.");
			}

			var ieId = await _loveBeriClient.GetInternalIdAsync();
			var newProducts = _entityUpdater.ChangeFieldIeId(products, ieId);
			if(newProducts.Length > 0)
			{
				var parentDirectoryPath = Directory.GetParent(_dataSource.BitrixFilePath).FullName;
				var newFilePath = Path.Combine(parentDirectoryPath, "new_productId.csv");

				_dataStorageService.SaveProducts(newProducts, newFilePath);

				Console.WriteLine("\nNew product ID found. new_productId.csv was created.");
			}

			_entityUpdater.ChangeFieldVibration(products);
			_entityUpdater.ChangeFieldNewAndBest(products);

			var newBaseFilePath = Path.Combine(Directory.GetParent(_dataSource.BitrixFilePath).FullName, "new_base.csv");
			_dataStorageService.SaveProducts(products, newBaseFilePath);
		}



		// IDisposable ////////////////////////////////////////////////////////////////////////////
		public void Dispose()
		{
			_lifetimeScope?.Dispose();
		}
	}
}