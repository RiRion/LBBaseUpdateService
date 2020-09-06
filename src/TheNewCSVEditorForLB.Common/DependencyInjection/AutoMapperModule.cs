using System.Reflection;
using Autofac;
using AutoMapper;
using Module = Autofac.Module;

namespace TheNewCSVEditorForLB.Common.DependencyInjection
{
    public class AutoMapperModule : Module
    {
        private readonly Assembly[] _assembliesToScan;
        
        public AutoMapperModule(Assembly[] assembliesToScan)
        {
            _assembliesToScan = assembliesToScan;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(GetMapper());
        }

        private IMapper GetMapper()
        {
            var mapperConfiguration = new MapperConfiguration(RegisterMappers);
            mapperConfiguration.CompileMappings();
            return mapperConfiguration.CreateMapper();
        }

        private void RegisterMappers(IMapperConfigurationExpression configurationExpression)
        {
            configurationExpression.AddMaps(_assembliesToScan);
        }
    }
}