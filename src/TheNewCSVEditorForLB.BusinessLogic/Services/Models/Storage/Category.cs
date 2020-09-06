using CsvHelper.Configuration.Attributes;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage
{
    public class Category
    {
        [Ignore]
        public int Id { get; set; }
        [Ignore]
        public int ParentId { get; set; }
        [Name("")]
        public string Name { get; set; }
    }
}