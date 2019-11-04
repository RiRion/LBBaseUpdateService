using CsvHelper.Configuration.Attributes;

namespace TheNewCSVEditorForLB.Data.Models
{
    public class Product
    {
        [Name("prodid")]
        public int ProductId { get; set; }
        [Name("vendor_id")]
        public int VendorId { get; set; }
        [Name("vendor_code")]
        public string VendorCode { get; set; }
        [Name("name")]
        public string Name { get; set; }
        [Name("description")]
        public string Description { get; set; }
        public Images ImagesURL { get; set; }
        [Name("batteries")]
        public string Batteries { get; set; }
        [Name("pack")]
        public string Pack { get; set; }
        [Name("material")]
        public string Material { get; set; }
        [Name("length")]
        public string Length { get; set; }
        [Name("diameter")]
        public string Diameter { get; set; }
        [Name("collection")]
        public string Collection { get; set; }
        public Categories Categories { get; set; }
        [Name("bestseller")]
        public string Bestseller { get; set; }
        [Name("new")]
        public string New { get; set; }
        [Name("function")]
        public string Function { get; set; }
        [Name("addfunction")]
        public string AddFunction { get; set; }
        [Name("vibration")]
        public string Vibration { get; set; }
        [Name("volume")]
        public string Volume { get; set; }
        [Name("modelyear")]
        public string ModelYear { get; set; }
        [Name("infoprice")]
        public string InfoPrice { get; set; }
        [Name("img_status")]
        public string ImgStatus { get; set; }
        [Name("ieid")]
        public string IeId { get; set; }
        [Name("vendor_country")]
        public int VendorCountry { get; set; }
        [Name("new_and_bestseller")]
        public string NewAndBestseller { get; set; }
    }
}