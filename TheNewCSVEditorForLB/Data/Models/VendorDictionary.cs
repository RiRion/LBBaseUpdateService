using CsvHelper.Configuration.Attributes;

namespace TheNewCSVEditorForLB.Data.Models
{
    public class VendorDictionary
    {
        [Name("CorrectVendorId")]
        public int CorrectVendorId { get; set; }
        [Name("ImportVendorId")]
        public int ImportVendorId { get; set; }
    }
}