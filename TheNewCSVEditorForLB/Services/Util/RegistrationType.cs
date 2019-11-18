using System;
using Autofac;
using TheNewCSVEditorForLB.Data.Interfaces;
using TheNewCSVEditorForLB.Data.Models;
using TheNewCSVEditorForLB.Data.Repositories;
using TheNewCSVEditorForLB.Services.Interfaces;

namespace TheNewCSVEditorForLB.Services.Util
{
    public class RegistrationType
    {
        public static void RegistrTypes(ContainerBuilder builder)
        {
            builder.RegisterType<ProductRepository>().AsImplementedInterfaces();
            builder.RegisterType<VendorsWithProductIdRepository>().As<IVendorsWithProductIdRepository>();
            builder.RegisterType<ProductIdWithInternalIdRepository>().As<IProductIdWithInternalIdRepository>();
            builder.RegisterType<ChangeData>().As<IChangeData>();
            builder.RegisterType<DataStorageProvider>().As<IDataStorageProvider>();
        }

        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            RegistrTypes(builder);
            return builder.Build();
        }
    }
}
