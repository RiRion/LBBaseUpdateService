using CsvHelper.Configuration;

namespace TheNewCSVEditorForLB.Data.Models
{
    public sealed class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Map(m => m.ProductId).Name("prodid");
            Map(m => m.VendorId).Name("vendor_id");
            Map(m => m.VendorCode).Name("vendor_code");
            Map(m => m.Name).Name("name");
            Map(m => m.Description).Name("description");
            References<ImagesMap>(m => m.ImagesURL);
            Map(m => m.Batteries).Name("batteries");
            Map(m => m.Pack).Name("pack");
            Map(m => m.Material).Name("material");
            Map(m => m.Length).Name("length");
            Map(m => m.Diameter).Name("diameter");
            Map(m => m.Collection).Name("collection");
            References<CategoriesMap>(m => m.Categories);
            Map(m => m.Bestseller).Name("bestseller");
            Map(m => m.New).Name("new");
            Map(m => m.Function).Name("function");
            Map(m => m.AddFunction).Name("addfunction");
            Map(m => m.Vibration).Name("vibration");
            Map(m => m.Volume).Name("volume");
            Map(m => m.ModelYear).Name("modelyear");
            Map(m => m.InfoPrice).Name("infoprice");
            Map(m => m.ImgStatus).Name("img_status");
            Map(m => m.IeId).Name("ieid").Optional(); 
            Map(m => m.VendorCountry).Name("vendor_country").Optional();
            Map(m => m.NewAndBestseller).Name("new__and_bestseller").Optional();
        }
    }
}