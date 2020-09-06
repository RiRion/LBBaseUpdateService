using System;
using CsvHelper.Configuration.Attributes;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage
{
    public class ProductDto
    {
	    public Int32 ProductId { get; set; } 
	    public Int32 VendorId { get; set; } 
	    public String VendorCode { get; set; } 
	    public String Name { get; set; } 
	    public String Description { get; set; } 
	    public Images ImagesURL { get; set; }
	    public String Batteries { get; set; }
	    public String Pack { get; set; }
	    public String Material { get; set; }
	    public String Length { get; set; }
	    public String Diameter { get; set; }
	    public Int32 Category { get; set; }
	    public String Function { get; set; }
	    public String AddFunction { get; set; }
	    public String Vibration { get; set; }
	    public String Volume { get; set; }
	    public String ModelYear { get; set; }
	    public String InfoPrice { get; set; }
	    public String IeId { get; set; }
	    public Int32 VendorCountry { get; set; }
	    public String NewAndBestseller { get; set; }
    }
}