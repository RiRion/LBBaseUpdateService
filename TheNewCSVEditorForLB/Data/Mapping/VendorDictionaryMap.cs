using CsvHelper.Configuration;

namespace TheNewCSVEditorForLB.Data.Models
{
    public class VendorDictionaryMap : ClassMap<VendorDictionary>
    {
        public VendorDictionaryMap()
        {
            Map(m => m.CorrectVendorId).Index(0);
            Map(m => m.ImportVendorId).Index(1);
        }
    }
}