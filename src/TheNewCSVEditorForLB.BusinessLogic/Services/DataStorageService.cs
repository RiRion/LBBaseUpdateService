using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.IO;
using System.Linq;
using TheNewCSVEditorForLB.BusinessLogic.Services.Interfaces;
using TheNewCSVEditorForLB.BusinessLogic.Services.Mappings;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage;

namespace TheNewCSVEditorForLB.BusinessLogic.Services
{
	public class DataStorageService : IDataStorageService
	{
		// IDataStorageService ////////////////////////////////////////////////////////////////////

		// Get
		public Product[] GetProducts(String filePath)
		{
			using(var stream = new StreamReader(filePath))
			using(var csv = new CsvReader(stream))
			{
				csv.Configuration.Delimiter = ";";
				csv.Configuration.HasHeaderRecord = true;
				csv.Configuration.TrimOptions = TrimOptions.Trim;
				csv.Configuration.BadDataFound = null;
				csv.Configuration.RegisterClassMap<ProductMap>();

				return csv.GetRecords<Product>().ToArray();
			}
		}
		public VendorsWithProductId[] GetVendors(String filePath)
		{
			using(var stream = new StreamReader(filePath))
			using(var csv = new CsvReader(stream))
			{
				csv.Configuration.Delimiter = ";";
				csv.Configuration.HasHeaderRecord = true;
				csv.Configuration.TrimOptions = TrimOptions.Trim;
				csv.Configuration.BadDataFound = null;
				csv.Configuration.RegisterClassMap<VendorDictionaryMap>();

				return csv.GetRecords<VendorsWithProductId>().ToArray();
			}
		}

		// Save
		public void SaveProducts(Product[] products, String filePath)
		{
			using(var stream = new StreamWriter(filePath))
			using(var csv = new CsvWriter(stream))
			{
				csv.Configuration.Delimiter = ";";
				csv.Configuration.HasHeaderRecord = true;
				csv.WriteRecords(products);
			}
		}
		public void SaveVendors(VendorsWithProductId[] vendors, String filePath)
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