﻿using Autofac;
using System;
using System.IO;
using System.Threading.Tasks;
using BitrixService.Services;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Config;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Exceptions;
using TheNewCSVEditorForLB.Common.DependencyInjection;

namespace TheNewCSVEditorForLB.Headless
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
			
			builder.RegisterModule(new AutoMapperModule(Collector.GetAssemblies("BusinessLogic")));
			builder.RegisterAssemblyModules(Collector.GetAssemblies("BusinessLogic"));
			builder.RegisterType<ApplicationContext>();
			builder.RegisterInstance(CreateConfig());
			builder.RegisterInstance(new ApiService(
				"http://loveberi.localhost",
				"/my_tools",
				"admin",
				"Lb0717511930"
			));
			
			return builder.Build();
		}
		private static ApplicationConfig CreateConfig()
		{
#if DEBUG
			var applicationConfig = GetTestDataSource();
#else
			var applicationConfig = GetUserDataSource();
#endif
			var bitrixParentDirectory = Directory.GetParent(applicationConfig.BitrixFilePath).FullName;

			applicationConfig.NewProductsFilePath = Path.Combine(bitrixParentDirectory, "new_productId.csv");
			applicationConfig.NewBaseFilePath = Path.Combine(bitrixParentDirectory, "new_base.csv");
			applicationConfig.NewVendorsFilePath = Path.Combine(Directory.GetParent(applicationConfig.VendorsFilePath).FullName, "new_vendors.csv");

			return applicationConfig;
		}
		private static ApplicationConfig GetTestDataSource()
		{
			const String bitrixDefaultFilePath = "TestData/Bitrix.csv";
			if(!File.Exists(bitrixDefaultFilePath))
				throw new DataFileNotFoundException($"File {bitrixDefaultFilePath} was not found!");

			const String vendorDictionaryDefaultFilePath = "TestData/VendorsDictionary.csv";
			if(!File.Exists(vendorDictionaryDefaultFilePath))
				throw new DataFileNotFoundException($"File {vendorDictionaryDefaultFilePath} was not found!");

			return new ApplicationConfig()
			{
				BitrixFilePath = bitrixDefaultFilePath,
				VendorsFilePath = vendorDictionaryDefaultFilePath
			};
		}
		private static ApplicationConfig GetUserDataSource()
		{
			System.Console.Clear();
			System.Console.Write("Path to base.csv: ");
			var bitrixFilePath = System.Console.ReadLine();
			System.Console.Write("Path to VendorsDictionary.csv: ");
			var vendorDictionaryFilePath = System.Console.ReadLine();

			if(!File.Exists(bitrixFilePath))
				throw new DataFileNotFoundException($"File {bitrixFilePath} was not found!");

			if(!File.Exists(vendorDictionaryFilePath))
				throw new DataFileNotFoundException($"File {vendorDictionaryFilePath} was not found!");

			return new ApplicationConfig()
			{
				BitrixFilePath = bitrixFilePath,
				VendorsFilePath = vendorDictionaryFilePath
			};
		}
	}
}