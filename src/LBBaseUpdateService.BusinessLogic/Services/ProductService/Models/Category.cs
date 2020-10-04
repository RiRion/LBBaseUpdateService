namespace LBBaseUpdateService.BusinessLogic.Services.ProductService.Models
{
    public class Category
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
    }
}