using Autofac;
using System.Reflection;
using Module = Autofac.Module;

namespace LBBaseUpdateService.BusinessLogic.DependencyInjection
{
	public class BusinessLogicModule : Module
	{
		private readonly Assembly _currentAssembly;


		public BusinessLogicModule()
		{
			_currentAssembly = Assembly.GetAssembly(typeof(BusinessLogicModule));
		}


		// FUNCTIONS //////////////////////////////////////////////////////////////////////////////
		protected override void Load(ContainerBuilder builder)
		{
			builder
				.RegisterAssemblyTypes(_currentAssembly)
				.Where(p => p.Name.EndsWith("Service") || p.IsClass)
				.AsSelf()
				.AsImplementedInterfaces();
			
			builder
				.RegisterAssemblyTypes(_currentAssembly)
				.Where(p => p.Name.EndsWith("State") || p.IsClass)
				.AsSelf()
				.AsImplementedInterfaces();

			builder.RegisterType<UpdateContext>();
		}
	}
}