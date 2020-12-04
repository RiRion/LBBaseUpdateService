using System.Linq;
using System.Reflection;
using CsvHelper.Configuration;

namespace BitrixService.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterClassMapCollection(this CsvConfiguration csvConfiguration, Assembly assembly)
        {
            assembly.GetTypes()
                .Where(t=> t.Name.EndsWith("Map") || t.IsSubclassOf(typeof(ClassMap)))
                .ToList()
                .ForEach(t=>csvConfiguration.RegisterClassMap(t));
        }
    }
}