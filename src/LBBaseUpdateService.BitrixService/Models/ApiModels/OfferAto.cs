namespace BitrixService.Models.ApiModels
{
    public class OfferAto
    {
        public int Id { get; set; } // offer ID
        public int ProductIeId { get; set; } // prodid -> CML2_LINK (Property)
        public int ProductExId { get; set; }
        public int XmlId { get; set; } // sku -> B_IBLOCK_ELEMENT.XML_ID
        public string Barcode { get; set; } // barcode -> barcode (Property)
        public string Name { get; set; } // name -> B_IBLOCK_ELEMENT.NAME
        public int Quantity { get; set; } // qty -> B_CATALOG_PRODUCT.QUANTITY
        public string ShippingDate { get; set; } // shippingdate -> shippingdate (Property)
        public string Weight { get; set; } // weight -> B_CATALOG_PRODUCT.WEIGHT
        public string Color { get; set; } // color -> COLOR (Property)
        public string Size { get; set; } // size -> SIZE (Property)
        public string Currency { get; set; } // currency -> B_CATALOG_PRICE.CURRENCY & B_CATALOG_PRODUCT.PURCHASING_CURRENCY
        public double Price { get; set; } // price -> B_CATALOG_PRICE.PRICE
        public double BaseWholePrice { get; set; } // basewoleprice -> B_CATALOG_PRODUCT.PURCHASING_PRICE
        public int P5SStock { get; set; } // p5s_stock -> p5sstock
        public int SuperSale { get; set; } // SuperSale -> SuperSale
        public int StopPromo { get; set; } // StopPromo -> StopPromo
    }
}