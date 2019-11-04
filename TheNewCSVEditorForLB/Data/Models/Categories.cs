using CsvHelper.Configuration.Attributes;

namespace TheNewCSVEditorForLB.Data.Models
{
    public class Categories
    {
        [Name("categories_1")]
        public string Categories1 { get; set; }
        [Name("categories_2")]
        public string Categories2 { get; set; }
        [Name("categories_3")]
        public string Categories3 { get; set; }
    }
}