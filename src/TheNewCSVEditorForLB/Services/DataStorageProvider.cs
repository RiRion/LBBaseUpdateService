using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheNewCSVEditorForLB.Services.Interfaces;

namespace TheNewCSVEditorForLB.Services
{
	public class DataStorageProvider<T> : IDataStorageProvider<T>
	{
		public String ReadPath { get; set; }
		public String WritePath { get; set; }

		public DataStorageProvider(String path)
		{
			if(File.Exists(path))
			{
				ReadPath = path;
				WritePath = Directory.GetParent(path).FullName;
			}
		}

		public List<T> ReadData<TMap>() where TMap : ClassMap
		{
			using(var stream = new StreamReader(ReadPath))
			using(var csv = new CsvReader(stream))
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

		public void WriteData(List<T> products, String name)
		{
			using(var stream = new StreamWriter(WritePath + name))
			using(var csv = new CsvWriter(stream))
			{
				csv.Configuration.Delimiter = ";";
				csv.Configuration.HasHeaderRecord = true;
				csv.WriteRecords(products);
			}
		}
	}
}