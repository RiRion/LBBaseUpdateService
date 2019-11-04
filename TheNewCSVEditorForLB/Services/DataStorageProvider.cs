using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using TheNewCSVEditorForLB.Services.Interfaces;

namespace TheNewCSVEditorForLB.Services
{
    public class DataStorageProvider<T> : IDataStorageProvider<T>
    {
        public string ReadPath { get; set; }
        public string WritePath { get; set; }

        public DataStorageProvider(string path)
        {
            ReadPath = path;
            WritePath = path.Substring(0, path.Length - path.Substring(path.LastIndexOf('/')).Length + 1);
        }

        public List<T> ReadData<TMap>() where TMap : ClassMap
        {
            using (var stream = new StreamReader(ReadPath))
            using (var csv = new CsvReader(stream))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.TrimOptions = TrimOptions.Trim;
                csv.Configuration.BadDataFound = null;
                csv.Configuration.RegisterClassMap<TMap>();

                var rec = csv.GetRecords<T>();
                return rec.ToList();
            }
        }

        public void WriteData(List<T> products, string name)
        {
            using (var stream = new StreamWriter(WritePath + name))
            using (var csv = new CsvWriter(stream))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = true;
                csv.WriteRecords(products);
            }
        }
    }
}