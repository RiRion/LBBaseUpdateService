using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using BitrixService.Clients.Loveberi.Models.Config;
using BitrixService.Clients.Stripmag.Models.Config;
using LBBaseUpdateService.Common.DependencyInjection;

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
			builder.RegisterAssemblyModules(Collector.GetAssemblies("BitrixService"));
			builder.RegisterType<ApplicationContext>();
			builder.RegisterInstance(new LoveberiClientConfig(
				"https://loveberi.ru",
				"/my_tools",
				"admin",
				"Lb0717511930"
			));
			builder.RegisterInstance(new StripmagClientConfig(
				"http://feed.p5s.ru/data",
				"/5d95eca8bec371.02477530",
				"/5d95eca8bec371.02477530?stock"
			));
			
			return builder.Build();
		}
	}
}