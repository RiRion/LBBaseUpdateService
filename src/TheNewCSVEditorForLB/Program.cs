using Autofac;
using System;
using System.IO;
using System.Threading.Tasks;
using TheNewCSVEditorForLB.BusinessLogic.Services;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Exceptions;
using TheNewCSVEditorForLB.Models.Config;
using DataSourceDto = TheNewCSVEditorForLB.Models.DataSourceDto;

namespace TheNewCSVEditorForLB
{
	class Program
	{
		static void Main(String[] args)
		{
			MainAsync(args).GetAwaiter().GetResult();
		}
		private static async Task MainAsync(String[] args)
		{
			using(var container = CreateContainer())
			{
				var context = container.Resolve<ApplicationContext>();
				await context.RunAsync();
			}
		}


		// SUPPORT FUNCTIONS ////////////////////////////////////////////////////////////////////////////
		private static IContainer CreateContainer()
		{
			var builder = new ContainerBuilder();

			builder.RegisterType<ApplicationContext>()
				.WithParameters(new[]
				{
					new NamedParameter("test", "test")
				});
			builder.RegisterType<EntityUpdater>().AsImplementedInterfaces();
			builder.RegisterInstance(new ApplicationConfig()
			{
				Test = "test"
			});
			builder.RegisterInstance(GetDataSource()).AsSelf();

			return builder.Build();
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
	}
}