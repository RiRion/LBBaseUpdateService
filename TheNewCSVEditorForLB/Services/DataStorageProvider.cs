using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using TheNewCSVEditorForLB.Data.Mapping;
using TheNewCSVEditorForLB.Data.Models;
using TheNewCSVEditorForLB.Services.Interfaces;

namespace TheNewCSVEditorForLB.Services
{
    public class DataStorageProvider : IDataStorageProvider
    {
        public string ProductBasePath { get; }
        public string VendorsWithProductIdPath { get; }
        public string WritePath { get; set; }
        
        public DataStorageProvider(string productBasePath, string vendorsWithProductIdPath)
        {
            if (File.Exists(productBasePath))
            {
                ProductBasePath = productBasePath;
                WritePath = Directory.GetParent(productBasePath).FullName;
            }
            else throw new Exception("File not found.");
            if (File.Exists(vendorsWithProductIdPath)) VendorsWithProductIdPath = vendorsWithProductIdPath;
            else throw new Exception("File not found.");
        }
        
        public List<Product> ReadProductBase()
        {
            using (var stream = new StreamReader(ProductBasePath))
            using (var csv = new CsvReader(stream))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.TrimOptions = TrimOptions.Trim;
                csv.Configuration.BadDataFound = null;
                csv.Configuration.RegisterClassMap<ProductMap>();

                var rec = csv.GetRecords<Product>();
                return rec.ToList();
            };
        }

        public List<VendorsWithProductId> ReadListVendorsWithProductId()
        {
            using (var stream = new StreamReader(VendorsWithProductIdPath))
            using (var csv = new CsvReader(stream))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.TrimOptions = TrimOptions.Trim;
                csv.Configuration.BadDataFound = null;
                csv.Configuration.RegisterClassMap<VendorsWithProductIdMap>();

                var rec = csv.GetRecords<VendorsWithProductId>();
                return rec.ToList();
            }
        }

        public void WriteData<T>(List<T> list, string name)
        {
            using (var stream = new StreamWriter(WritePath + name))
            using (var csv = new CsvWriter(stream))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = true;
                csv.WriteRecords(list);
            }
        }
    }
}