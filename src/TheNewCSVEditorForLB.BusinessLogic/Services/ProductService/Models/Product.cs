using System;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Models
{
	public class Product : IEquatable<Product>
	{
		public int ProductId { get; set; }
		public int VendorId { get; set; }
		public string VendorCode { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public Images ImagesURL { get; set; }
		public string Batteries { get; set; }
		public string Pack { get; set; }
		public string Material { get; set; }
		public string Length { get; set; }
		public string Diameter { get; set; }
		public string Collection { get; set; }
		public Categories Categories { get; set; }
		public string Bestseller { get; set; }
		public string New { get; set; }
		public string Function { get; set; }
		public string AddFunction { get; set; }
		public string Vibration { get; set; }
		public string Volume { get; set; }
		public string ModelYear { get; set; }
		public string InfoPrice { get; set; }
		public string ImgStatus { get; set; }
		public int IeId { get; set; }
		public int VendorCountry { get; set; }
		public string NewAndBestseller { get; set; }

		public bool Equals(Product other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return ProductId == other.ProductId 
			       && VendorId == other.VendorId 
			       && VendorCode == other.VendorCode 
			       && Name == other.Name 
			       //&& Description == other.Description 
			       && Batteries == other.Batteries 
			       && Pack == other.Pack 
			       && Material == other.Material 
			       && Length == other.Length 
			       && Diameter == other.Diameter
			       && Function == other.Function 
			       && AddFunction == other.AddFunction 
			       && Vibration == other.Vibration 
			       && Volume == other.Volume 
			       && ModelYear == other.ModelYear 
			       && VendorCountry == other.VendorCountry 
			       && NewAndBestseller == other.NewAndBestseller;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Product) obj);
		}

		public override int GetHashCode()
		{
			var hashCode = new HashCode();
			hashCode.Add(ProductId);
			hashCode.Add(VendorId);
			hashCode.Add(VendorCode);
			hashCode.Add(Name);
			//hashCode.Add(Description);
			hashCode.Add(Batteries);
			hashCode.Add(Pack);
			hashCode.Add(Material);
			hashCode.Add(Length);
			hashCode.Add(Diameter);
			hashCode.Add(Function);
			hashCode.Add(AddFunction);
			hashCode.Add(Vibration);
			hashCode.Add(Volume);
			hashCode.Add(ModelYear);
			hashCode.Add(VendorCountry);
			hashCode.Add(NewAndBestseller);
			return hashCode.ToHashCode();
		}
	}
}