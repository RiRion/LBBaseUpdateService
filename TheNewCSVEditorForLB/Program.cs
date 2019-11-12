using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheNewCSVEditorForLB.Data.Interfaces;
using TheNewCSVEditorForLB.Data.Models;
using TheNewCSVEditorForLB.Data.Repositories;
using TheNewCSVEditorForLB.Services;
using TheNewCSVEditorForLB.Services.Interfaces;
using Autofac;

namespace TheNewCSVEditorForLB 
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.Write("Path to base.csv: ");
            string basePath = Console.ReadLine();
            Console.Write("Path to VendorsDictionary.csv: ");
            string vendorDicPath = Console.ReadLine();
            
            basePath = "/Users/Anton/Projects/TheNewCSVEditorForLB/input/bitrix.csv";
            vendorDicPath = "/Users/Anton/Projects/TheNewCSVEditorForLB/input/VendorsDictionary.csv";

            Execute(basePath, vendorDicPath);
        }

        static void Execute(string basePath, string vendorDicPath)
        {
            var container = Services.Util.RegistrationType.GetContainer();
            
            IDataStorageProvider<Product> provider = new DataStorageProvider<Product>(basePath);
            IDataStorageProvider<VendorsWithProductId> providerDictionary = new DataStorageProvider<VendorsWithProductId>(vendorDicPath);
            var product = container.Resolve<IProductRepository>();
            var listVendorsWithProductId = container.Resolve<IVendorsWithProductIdRepository>();
            var productIdWithInternalId = container.Resolve<IProductIdWithInternalIdRepository>();
            product.AddMany(provider.ReadData<ProductMap>());
            listVendorsWithProductId.AddMany(providerDictionary.ReadData<VendorDictionaryMap>());
            productIdWithInternalId.GetInternalIdFromServer("https://loveberi.ru/bitrix/my_tools/getDictionaryId.php");


            IChangeData changeData = new ChangeData(product, listVendorsWithProductId, productIdWithInternalId);
            var tasks = new List<Task>
            {
                Task.Factory.StartNew(changeData.ChangeFieldVendorIdAndVendorCountry),
                Task.Factory.StartNew(changeData.ChangeFieldVibration),
                Task.Factory.StartNew(changeData.ChangeFieldNewAndBest),
                Task.Factory.StartNew(changeData.ChangeFieldIeId)
            };

            Task.WaitAll(tasks.ToArray());

            if (changeData.NewVendors.Count != 0)
            {
                providerDictionary.WriteData(changeData.NewVendors, "/new_vendors.csv");
                Console.WriteLine("\nNew vendor ID found. new_vendors.csv was created.");
            }
            
            if (changeData.NewProductId.Count != 0)
            {
                provider.WriteData(changeData.NewProductId, "/new_productId.csv");
                Console.WriteLine("\nNew product ID found. new_productId.csv was created.");
            }
            
            provider.WriteData(product.AllProducts, "/new_base.csv");
        }
    }
}