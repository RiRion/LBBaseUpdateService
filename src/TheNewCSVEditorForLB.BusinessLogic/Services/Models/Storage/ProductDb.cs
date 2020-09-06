using CsvHelper.Configuration.Attributes;
using System;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage
{
	public class ProductDb
	{
		[Name("prodid")]
		public Int32 ProductId { get; set; }
		[Name("vendor_id")]
		public Int32 VendorId { get; set; }
		[Name("vendor_code")]
		public String VendorCode { get; set; }
		[Name("name")]
		public String Name { get; set; }
		[Name("description")]
		public String Description { get; set; }
		public Images ImagesURL { get; set; }
		[Name("batteries")]
		public String Batteries { get; set; }
		[Name("pack")]
		public String Pack { get; set; }
		[Name("material")]
		public String Material { get; set; }
		[Name("length")]
		public String Length { get; set; }
		[Name("diameter")]
		public String Diameter { get; set; }
		[Name("collection")]
		public String Collection { get; set; }
		public Categories Categories { get; set; }
		[Name("bestseller")]
		public String Bestseller { get; set; }
		[Name("new")]
		public String New { get; set; }
		[Name("function")]
		public String Function { get; set; }
		[Name("addfunction")]
		public String AddFunction { get; set; }
		[Name("vibration")]
		public String Vibration { get; set; }
		[Name("volume")]
		public String Volume { get; set; }
		[Name("modelyear")]
		public String ModelYear { get; set; }
		[Name("infoprice")]
		public String InfoPrice { get; set; }
		[Name("img_status")]
		public String ImgStatus { get; set; }
		[Name("ieid")]
		public String IeId { get; set; }
		[Name("vendor_country")]
		public Int32 VendorCountry { get; set; }
		[Name("new_and_bestseller")]
		public String NewAndBestseller { get; set; }
	}
}