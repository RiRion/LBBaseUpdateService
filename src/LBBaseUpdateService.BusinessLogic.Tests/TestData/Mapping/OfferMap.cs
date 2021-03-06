using CsvHelper.Configuration;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Models;

namespace LBBaseUpdateService.BusinessLogic.Tests.TestData.Mapping
{
    public class OfferMap : ClassMap<Offer>
    {
        public OfferMap()
        {
            Map(m => m.Id).Optional();
            Map(m => m.ProductIeId).Name("prodid");
            Map(m => m.XmlId).Name("sku");
            Map(m => m.Barcode).Name("barcode");
            Map(m => m.Name).Name("name");
            Map(m => m.Quantity).Name("qty");
            Map(m => m.ShippingDate).Name("shippingdate");
            Map(m=>m.Weight).Name("weight").Default("0");
            Map(m => m.Color).Name("color");
            Map(m => m.Size).Name("size");
            Map(m => m.Currency).Name("currency");
            Map(m => m.Price).Name("price");
            Map(m => m.BaseWholePrice).Name("basewholeprice");
            Map(m => m.P5SStock).Name("p5s_stock");
            Map(m => m.SuperSale).Name("SuperSale");
            Map(m => m.StopPromo).Name("StopPromo");
        }
    }
}