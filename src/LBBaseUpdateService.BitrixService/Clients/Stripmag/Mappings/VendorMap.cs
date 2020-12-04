using BitrixService.Models.ApiModels;
using CsvHelper.Configuration;

namespace BitrixService.Clients.Stripmag.Mappings
{
    public sealed class VendorMap : ClassMap<VendorAto>
    {
        public VendorMap()
        {
            Map(m=>m.VendorId).Name("vendorID");
            Map(m => m.Title).Name("title");
            Map(m => m.Description).Name("description");
            Map(m => m.Country).Name("country");
            Map(m => m.DescType).Name("descr_type");
        }
    }
}