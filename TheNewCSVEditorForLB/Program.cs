using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheNewCSVEditorForLB.Data.Interfaces;
using TheNewCSVEditorForLB.Data.Models;
using TheNewCSVEditorForLB.Data.Repositories;
using TheNewCSVEditorForLB.Services;
using TheNewCSVEditorForLB.Services.Interfaces;

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
            Execute(basePath, vendorDicPath);
        }

        static void Execute(string basePath, string vendorDicPath)
        {
            IDataStorageProvider<Product> provider = new DataStorageProvider<Product>(basePath);
            IDataStorageProvider<VendorDictionary> providerDictionary = new DataStorageProvider<VendorDictionary>(vendorDicPath);
            IProductRepository product = new ProductRepository();
            IVendorDictionaryRepository vendorDictionary = new VendorDictionaryRepository();
            IIeIdDictionaryRepository ieId = new IeIdDictionaryRepository();
            product.AddMany(provider.ReadData<ProductMap>());
            vendorDictionary.AddMany(providerDictionary.ReadData<VendorDictionaryMap>());
            
            IChangeData changeData = new ChangeData(product, vendorDictionary, ieId);
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Factory.StartNew(changeData.ChangeFieldVendorIdAndVendorCountry));
            tasks.Add(Task.Factory.StartNew(changeData.ChangeFieldVibration));
            tasks.Add(Task.Factory.StartNew(changeData.ChangeFieldNewAndBest));

            if (ieId.GetFromServer("https://loveberi.ru/bitrix/my_tools/getDictionaryId.php"))
            {
                StatusCode.StatusOk("Connect to Loveberi.ru. Downloading IeIdDictionary.");
                tasks.Add(Task.Factory.StartNew(changeData.ChangeFieldIeId));
            }
            else StatusCode.StatusFalse("No connection to server: https://loveberi.ru/bitrix/my_tools/getDictionaryId.php");

            
            
            
            Task.WaitAll(tasks.ToArray());
            if (changeData.NewVendors.Count != 0)
            {
                providerDictionary.WriteData(changeData.NewVendors, "new_vendors.csv");
                Console.WriteLine("\nNew vendor ID found. new_vendors.csv was created.");
            }
            
            if (changeData.NewProductId.Count != 0)
            {
                provider.WriteData(changeData.NewProductId, "new_productId.csv");
                Console.WriteLine("\nNew product ID found. new_productId.csv was created.");
            }
            
            provider.WriteData(product.AllProducts, "new_base.csv");
        }
    }
}