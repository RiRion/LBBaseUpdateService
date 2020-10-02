using CsvHelper.Configuration;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Mappings
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