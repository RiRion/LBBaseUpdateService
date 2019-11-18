using Autofac;
using System;
using System.IO;
using System.Threading.Tasks;
using TheNewCSVEditorForLB.Data.Interfaces;
using TheNewCSVEditorForLB.Data.Models;
using TheNewCSVEditorForLB.Services;
using TheNewCSVEditorForLB.Services.Models;
using TheNewCSVEditorForLB.Services.Models.Exceptions;

namespace TheNewCSVEditorForLB
{
	internal sealed class ApplicationContext : IDisposable
	{
		private readonly ILifetimeScope _lifetimeScope;
		private readonly IProductRepository _productRepository;
		private readonly IVendorsWithProductIdRepository _vendorsWithProductIdRepository;
		private readonly IProductIdWithInternalIdRepository _productIdWithInternalIdRepository;
		private readonly ApplicationConfig _config;
		private readonly String _test;


		public ApplicationContext(
			ILifetimeScope lifetimeScope,
			IProductRepository productRepository,
			IVendorsWithProductIdRepository vendorsWithProductIdRepository,
			IProductIdWithInternalIdRepository productIdWithInternalIdRepository,
			ApplicationConfig config,
			String test)
		{
			_lifetimeScope = lifetimeScope;
			_productRepository = productRepository;
			_vendorsWithProductIdRepository = vendorsWithProductIdRepository;
			_productIdWithInternalIdRepository = productIdWithInternalIdRepository;
			_config = config;
			_test = test;
		}


		// FUNCTIONS ////////////////////////////////////////////////////////////////////////////////////
		public async Task RunAsync()
		{
			var dataSource = GetDataSource();

			var provider = new DataStorageProvider<Product>(dataSource.BitrixFilePath);
			var providerDictionary = new DataStorageProvider<VendorsWithProductId>(dataSource.VendorDictionaryFilePath);


			_productRepository.AddMany(provider.ReadData<ProductMap>());
			_vendorsWithProductIdRepository.AddMany(providerDictionary.ReadData<VendorDictionaryMap>());
			_productIdWithInternalIdRepository.GetInternalIdFromServer("https://loveberi.ru/bitrix/my_tools/getDictionaryId.php");


			var changeData = new ChangeData(_productRepository, _vendorsWithProductIdRepository, _productIdWithInternalIdRepository);


			Task.WaitAll(new[]
			{
				Task.Run(changeData.ChangeFieldVendorIdAndVendorCountry),
				Task.Run(changeData.ChangeFieldVibration),
				Task.Run(changeData.ChangeFieldNewAndBest),
				Task.Run(changeData.ChangeFieldIeId)
			});

			if(changeData.NewVendors.Count != 0)
			{
				providerDictionary.WriteData(changeData.NewVendors, "/new_vendors.csv");
				Console.WriteLine("\nNew vendor ID found. new_vendors.csv was created.");
			}

			if(changeData.NewProductId.Count != 0)
			{
				provider.WriteData(changeData.NewProductId, "/new_productId.csv");
				Console.WriteLine("\nNew product ID found. new_productId.csv was created.");
			}

			provider.WriteData(_productRepository.AllProducts, "/new_base.csv");
		}
		private static DataSourceDto GetDataSource()
		{
#if DEBUG
			return GetTestDataSource();
#else
			return GetUserDataSource();
#endif
		}
		private static DataSourceDto GetTestDataSource()
		{
			const String bitrixDefaultFilePath = "TestData/Bitrix.csv";
			if(!File.Exists(bitrixDefaultFilePath))
				throw new DataFileNotFoundException($"File {bitrixDefaultFilePath} was not found!");

			const String vendorDictionaryDefaultFilePath = "TestData/VendorsDictionary.csv";
			if(!File.Exists(vendorDictionaryDefaultFilePath))
				throw new DataFileNotFoundException($"File {vendorDictionaryDefaultFilePath} was not found!");

			return new DataSourceDto()
			{
				BitrixFilePath = bitrixDefaultFilePath,
				VendorDictionaryFilePath = vendorDictionaryDefaultFilePath
			};
		}
		private static DataSourceDto GetUserDataSource()
		{
			Console.Clear();
			Console.Write("Path to base.csv: ");
			var bitrixFilePath = Console.ReadLine();
			Console.Write("Path to VendorsDictionary.csv: ");
			var vendorDictionaryFilePath = Console.ReadLine();

			if(!File.Exists(bitrixFilePath))
				throw new DataFileNotFoundException($"File {bitrixFilePath} was not found!");

			if(!File.Exists(vendorDictionaryFilePath))
				throw new DataFileNotFoundException($"File {vendorDictionaryFilePath} was not found!");

			return new DataSourceDto()
			{
				BitrixFilePath = bitrixFilePath,
				VendorDictionaryFilePath = vendorDictionaryFilePath
			};
		}


		// IDisposable ////////////////////////////////////////////////////////////////////////////
		public void Dispose()
		{
			_lifetimeScope?.Dispose();
		}
	}
}