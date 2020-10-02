using CsvHelper.Configuration;
using TheNewCSVEditorForLB.Data.Models;

namespace TheNewCSVEditorForLB.Data.Mapping
{
    public class VendorsWithProductIdMap : ClassMap<VendorsWithProductId>
    {
        public VendorsWithProductIdMap()
        {
            Map(m => m.CorrectVendorId).Index(0);
            Map(m => m.ImportVendorId).Index(1);
        }
    }
}