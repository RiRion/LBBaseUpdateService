using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TheNewCSVEditorForLB.BusinessLogic.Services.Interfaces;
using TheNewCSVEditorForLB.BusinessLogic.Services.Mappings;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage;

namespace TheNewCSVEditorForLB.BusinessLogic.Services
{
	public class DataStorageService : IDataStorageService
	{
		// IDataStorageService ////////////////////////////////////////////////////////////////////
		//Properties.
		public ProductDb[] Products { get; set; }
		public ProductDb[] NewProducts { get; set; }
		public ProductDb[] RemoveProducts { get; set; }
		
		// Get
		public ProductDb[] GetProductsFromFile(String filePath)
		{
			using(var stream = new StreamReader(filePath))
			using(var csv = new CsvReader(stream))
			{
				csv.Configuration.Delimiter = ";";
				csv.Configuration.HasHeaderRecord = true;
				csv.Configuration.TrimOptions = TrimOptions.Trim;
				csv.Configuration.BadDataFound = null;
				csv.Configuration.RegisterClassMap<ProductMap>();

				return csv.GetRecords<ProductDb>().ToArray();
			}
		}

		public VendorsId[] GetVendorsFromJson(string content)
		{
			return JsonConvert.DeserializeObject<VendorsId[]>(content);
		}
		
		public VendorsId[] GetVendorsFromFile(String filePath)
		{
			using(var stream = new StreamReader(filePath))
			using(var csv = new CsvReader(stream))
			{
				csv.Configuration.Delimiter = ";";
				csv.Configuration.HasHeaderRecord = true;
				csv.Configuration.TrimOptions = TrimOptions.Trim;
				csv.Configuration.BadDataFound = null;
				csv.Configuration.RegisterClassMap<VendorDictionaryMap>();

				return csv.GetRecords<VendorsId>().ToArray();
			}
		}

		// Save
		public void SaveProductsToFile(ProductDb[] products, String filePath)
		{
			using(var stream = new StreamWriter(filePath))
			using(var csv = new CsvWriter(stream))
			{
				csv.Configuration.Delimiter = ";";
				csv.Configuration.HasHeaderRecord = true;
				csv.WriteRecords(products);
			}
		}
		public void SaveVendorsToFile(VendorsId[] vendors, String filePath)
		{
			using(var stream = new StreamWriter(filePath))
			using(var csv = new CsvWriter(stream))
			{
				csv.Configuration.Delimiter = ";";
				csv.Configuration.HasHeaderRecord = true;
				csv.WriteRecords(vendors);
			}
		}
	}
}