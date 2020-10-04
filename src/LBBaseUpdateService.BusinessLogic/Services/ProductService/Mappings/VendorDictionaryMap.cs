using CsvHelper.Configuration;
using LBBaseUpdateService.BusinessLogic.Services.Models;

namespace LBBaseUpdateService.BusinessLogic.Services.ProductService.Mappings
{
	public sealed class VendorDictionaryMap : ClassMap<VendorId>
	{
		public VendorDictionaryMap()
		{
			Map(m => m.InternalId).Index(0);
			Map(m => m.ExternalId).Index(1);
		}
	}
}