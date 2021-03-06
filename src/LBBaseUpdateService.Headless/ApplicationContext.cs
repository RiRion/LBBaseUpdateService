using System;
using System.Threading.Tasks;
using Autofac;
using LBBaseUpdateService.BusinessLogic;

namespace LBBaseUpdateService.Headless
{
	internal sealed class ApplicationContext
	{
		private readonly ILifetimeScope _lifetimeScope;
		
		public ApplicationContext(ILifetimeScope lifetimeScope)
		{
			_lifetimeScope = lifetimeScope;
		}
		
		public async Task RunAsync()
		{
			try
			{
				await _lifetimeScope.Resolve<UpdateContext>().UpdateAsync();
				// TODO: Require update categories.
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error: {e.Message}");
				Console.WriteLine(e.StackTrace);
			}
		}
	}
}