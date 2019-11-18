using CsvHelper.Configuration.Attributes;
using System;

namespace TheNewCSVEditorForLB.Data.Models
{
	public class VendorsWithProductId
	{
		[Name("CorrectVendorId")]
		public Int32 CorrectVendorId { get; set; }
		[Name("ImportVendorId")]
		public Int32 ImportVendorId { get; set; }
	}
}