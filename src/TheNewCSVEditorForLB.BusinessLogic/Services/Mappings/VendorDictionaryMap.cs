using CsvHelper.Configuration;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Mappings
{
	public class VendorDictionaryMap : ClassMap<VendorsWithProductId>
	{
		public VendorDictionaryMap()
		{
			Map(m => m.CorrectVendorId).Index(0);
			Map(m => m.ImportVendorId).Index(1);
		}
	}
}