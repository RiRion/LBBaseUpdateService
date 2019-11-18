using CsvHelper.Configuration;

namespace TheNewCSVEditorForLB.Data.Models
{
    public class CategoriesMap : ClassMap<Categories>
    {
        public CategoriesMap()
        {
            Map(m => m.Categories1).Name("categories_1");
            Map(m => m.Categories2).Name("categories_2");
            Map(m => m.Categories3).Name("categories_3");
        }
    }
}