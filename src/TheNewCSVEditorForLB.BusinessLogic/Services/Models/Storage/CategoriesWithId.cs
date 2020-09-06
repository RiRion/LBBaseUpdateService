using System;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage
{
    public class CategoriesWithId
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string CategoryName { get; set; }
    }
}