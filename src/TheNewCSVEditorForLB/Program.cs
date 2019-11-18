using Autofac;
using System;
using System.Threading.Tasks;
using TheNewCSVEditorForLB.Data.Repositories;
using TheNewCSVEditorForLB.Services;
using TheNewCSVEditorForLB.Services.Models;

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
			builder.RegisterType<ProductRepository>().AsImplementedInterfaces();
			builder.RegisterType<VendorsWithProductIdRepository>().AsImplementedInterfaces();
			builder.RegisterType<ProductIdWithInternalIdRepository>().AsImplementedInterfaces();
			builder.RegisterType<ChangeData>().AsImplementedInterfaces();
			builder.RegisterInstance(new ApplicationConfig()
			{
				Test = "test"
			});

			return builder.Build();
		}
	}
}