using System;

namespace LBBaseUpdateService.BusinessLogic.Services.OfferService.Models
{
    public class Offer : IEquatable<Offer>
    {
        public int Id { get; set; }
        public int ProductIeId { get; set; }
        public int ProductExId { get; set; }
        public int XmlId { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string ShippingDate { get; set; }
        public string Weight { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Currency { get; set; }
        public double Price { get; set; }
        public double BaseWholePrice { get; set; }
        public int P5SStock { get; set; }
        public int SuperSale { get; set; }
        public int StopPromo { get; set; }

        public bool Equals(Offer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return XmlId == other.XmlId
                   && Barcode == other.Barcode 
                   && Name == other.Name 
                   && Quantity == other.Quantity 
                   //&& ShippingDate == other.ShippingDate 
                   && Weight == other.Weight 
                   && Color == other.Color 
                   && Size == other.Size 
                   && Currency == other.Currency 
                   && Price.Equals(other.Price) 
                   && BaseWholePrice.Equals(other.BaseWholePrice) 
                   && P5SStock == other.P5SStock 
                   && SuperSale == other.SuperSale 
                   && StopPromo == other.StopPromo;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Offer) obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(ProductIeId);
            hashCode.Add(XmlId);
            hashCode.Add(Barcode);
            hashCode.Add(Name);
            hashCode.Add(Quantity);
            //hashCode.Add(ShippingDate);
            hashCode.Add(Weight);
            hashCode.Add(Color);
            hashCode.Add(Size);
            hashCode.Add(Currency);
            hashCode.Add(Price);
            hashCode.Add(BaseWholePrice);
            hashCode.Add(P5SStock);
            hashCode.Add(SuperSale);
            hashCode.Add(StopPromo);
            return hashCode.ToHashCode();
        }
    }
}