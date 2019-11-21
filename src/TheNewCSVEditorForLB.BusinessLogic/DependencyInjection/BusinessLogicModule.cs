using Autofac;
using System.Reflection;
using Module = Autofac.Module;

namespace TheNewCSVEditorForLB.BusinessLogic.DependencyInjection
{
	public class BusinessLogicModule : Module
	{
		private readonly Assembly _currentAssembly;
		private readonly Assembly _callingAssembly;


		public BusinessLogicModule()
		{
			_currentAssembly = Assembly.GetAssembly(typeof(BusinessLogicModule));
			_callingAssembly = Assembly.GetCallingAssembly();
		}


		// FUNCTIONS //////////////////////////////////////////////////////////////////////////////
		protected override void Load(ContainerBuilder builder)
		{
			builder
				.RegisterAssemblyTypes(_currentAssembly)
				.Where(p => p.Name.EndsWith("Client"))
				.AsSelf()
				.AsImplementedInterfaces();

			builder
				.RegisterAssemblyTypes(_currentAssembly)
				.Where(p => p.IsClass && p.Name.EndsWith("Service"))
				.AsSelf()
				.AsImplementedInterfaces();
		}
	}
}