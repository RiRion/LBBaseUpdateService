using CsvHelper.Configuration.Attributes;
using System;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage
{
	public class VendorsId
	{
		[Name("CorrectVendorId")]
		public Int32 CorrectVendorId { get; set; }

		[Name("ImportVendorId")]
		public Int32 ImportVendorId { get; set; }
	}
}