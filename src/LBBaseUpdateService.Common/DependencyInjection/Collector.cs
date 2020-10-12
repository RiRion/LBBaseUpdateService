using System.Linq;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Configuration;

namespace LBBaseUpdateService.Common.DependencyInjection
{
    public static class Collector
    {
        public static Assembly[] GetAssemblies(string partOfName)
        {
            return DependencyContext.Default.CompileLibraries
                .Where(c => c.Name.Contains(partOfName))
                .Select(a => Assembly.Load(new AssemblyName(a.Name)))
                .ToArray();
        }

        public static Assembly GetAssembly(string name)
        {
            return Assembly.Load(DependencyContext.Default.CompileLibraries
                .First(c => c.Name.Equals(name)).Name);
        }

        public static void RegisterClientConfigurations(this ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterClientConfigurations(configuration, Assembly.GetCallingAssembly());
        }

        public static void RegisterClientConfigurations(this ContainerBuilder builder, IConfiguration configuration, Assembly assemblyToScan)
        {
            var configTypes = assemblyToScan.GetTypes()
                .Where(t => t.Name.EndsWith("Config") && t.IsClass);

            foreach (var configType in configTypes)
            {
                builder.RegisterInstance(configuration.GetSection(configType.Name)
                    .Get(configType)).AsSelf().SingleInstance();
            }
        }
    }
}