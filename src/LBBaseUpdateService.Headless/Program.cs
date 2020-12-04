using System;
using System.Threading.Tasks;
using Autofac;
using LBBaseUpdateService.Common.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace LBBaseUpdateService.Headless
{
	static class Program
	{
		static void Main(String[] args)
		{
			MainAsync(args).GetAwaiter().GetResult();
		}
		private static async Task MainAsync(String[] args)
		{
			var configuration = GetConfiguration();
			using(var container = CreateContainer(configuration))
			{
				var context = container.Resolve<ApplicationContext>();
				await context.RunAsync();
			}
		}
		
		// SUPPORT FUNCTIONS ////////////////////////////////////////////////////////////////////////////
		private static IContainer CreateContainer(IConfiguration configuration)
		{
			var builder = new ContainerBuilder();
			
			builder.RegisterModule(new AutoMapperModule(Collector.GetAssemblies("BusinessLogic")));
			builder.RegisterAssemblyModules(Collector.GetAssemblies("BusinessLogic"));
			builder.RegisterAssemblyModules(Collector.GetAssemblies("BitrixService"));
			builder.RegisterType<ApplicationContext>();
			builder.RegisterClientConfigurations(configuration, Collector.GetAssembly("LBBaseUpdateService.BitrixService"));

			return builder.Build();
		}

		private static IConfiguration GetConfiguration()
		{
			return new ConfigurationBuilder()
				.AddJsonFile("appConfig.json")
				.Build();
		}
	}
}