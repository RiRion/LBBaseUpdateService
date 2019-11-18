using CsvHelper.Configuration;
using TheNewCSVEditorForLB.Data.Models;

namespace TheNewCSVEditorForLB.Data.Mapping
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