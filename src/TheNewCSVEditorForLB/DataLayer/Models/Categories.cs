using CsvHelper.Configuration.Attributes;
using System;

namespace TheNewCSVEditorForLB.Data.Models
{
	public class Categories
	{
		[Name("categories_1")]
		public String Categories1 { get; set; }
		[Name("categories_2")]
		public String Categories2 { get; set; }
		[Name("categories_3")]
		public String Categories3 { get; set; }
	}
}