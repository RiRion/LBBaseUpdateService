using System.Globalization;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Core;
using BitrixService.Clients.TypedHttp;
using BitrixService.Extensions;
using CsvHelper.Configuration;
using Module = Autofac.Module;

namespace BitrixService.DependencyInjection
{
    public class ApiClientModule : Module
    {
        private readonly Assembly _currentAssembly;

        public ApiClientModule()
        {
            _currentAssembly = Assembly.GetAssembly(typeof(ApiClientModule));
        }
        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(_currentAssembly)
                .Where(t => t.Name.EndsWith("Client") || t.IsSubclassOf(typeof(TypedHttpClient)))
                .AsSelf()
                .AsImplementedInterfaces();
            builder.RegisterInstance(GetCsvConfiguration()).AsSelf(); // TODO: This configuration must be register with a main assembly.
        }

        private CsvConfiguration GetCsvConfiguration()
        {
            var csvConfig = new CsvConfiguration(new CultureInfo("en-US"))
            {
                Delimiter = ";",
                HasHeaderRecord = true,
                TrimOptions = TrimOptions.Trim,
                BadDataFound = null,
                Encoding = Encoding.UTF8
            };
            csvConfig.RegisterClassMapCollection(_currentAssembly);
            return csvConfig;
        }
    }
}