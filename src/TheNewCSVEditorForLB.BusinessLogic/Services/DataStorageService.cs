using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TheNewCSVEditorForLB.BusinessLogic.Services.Interfaces;
using TheNewCSVEditorForLB.BusinessLogic.Services.Mappings;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models;
using TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Mappings;
using TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Models;

namespace TheNewCSVEditorForLB.BusinessLogic.Services
{
	public class DataStorageService : IDataStorageService // TODO: Add a once CsvConfiguration for all.
	{
		// IDataStorageService /////////////////////////////////////////////////////////////////////////////////////////
		
		// Get
		public Product[] GetProductsFromFile(String filePath)
		{
			using(var stream = new StreamReader(filePath))
			using(var csv = new CsvReader(stream, CultureInfo.CurrentCulture))
			{
				csv.Configuration.Delimiter = ";";
				csv.Configuration.HasHeaderRecord = true;
				csv.Configuration.TrimOptions = TrimOptions.Trim;
				csv.Configuration.BadDataFound = null;
				csv.Configuration.RegisterClassMap<ProductMap>();

				return csv.GetRecords<Product>().ToArray();
			}
		}

		public VendorId[] GetVendorsFromJson(string content)
		{
			return JsonConvert.DeserializeObject<VendorId[]>(content);
		}
		
		public VendorId[] GetVendorsFromFile(String filePath)
		{
			using(var stream = new StreamReader(filePath))
			using(var csv = new CsvReader(stream, CultureInfo.CurrentCulture))
			{
				csv.Configuration.Delimiter = ";";
				csv.Configuration.HasHeaderRecord = true;
				csv.Configuration.TrimOptions = TrimOptions.Trim;
				csv.Configuration.BadDataFound = null;
				csv.Configuration.RegisterClassMap<VendorDictionaryMap>();

				return csv.GetRecords<VendorId>().ToArray();
			}
		}

		// Save
		public void SaveProductsToFile(Product[] products, String filePath)
		{
			using(var stream = new StreamWriter(filePath))
			using(var csv = new CsvWriter(stream, CultureInfo.CurrentCulture))
			{
				csv.Configuration.Delimiter = ";";
				csv.Configuration.HasHeaderRecord = true;
				csv.WriteRecords(products);
			}
		}
		public void SaveVendorsToFile(VendorId[] vendors, String filePath)
		{
			using(var stream = new StreamWriter(filePath))
			using(var csv = new CsvWriter(stream, CultureInfo.CurrentCulture))
			{
				csv.Configuration.Delimiter = ";";
				csv.Configuration.HasHeaderRecord = true;
				csv.WriteRecords(vendors);
			}
		}
	}
}