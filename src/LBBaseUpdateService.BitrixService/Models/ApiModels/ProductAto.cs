using System.Collections.Generic;

namespace BitrixService.Models.ApiModels
{
    public class ProductAto
    {
        public int ProductIeId { get; set; }
        public int ProductExId { get; set; } 
        public int VendorId { get; set; } 
        public string VendorCode { get; set; } 
        public string Name { get; set; } 
        public string Description { get; set; } 
        public ImagesAto ImagesURL { get; set; }
        public string Batteries { get; set; }
        public string Pack { get; set; }
        public string Material { get; set; }
        public string Length { get; set; }
        public string Diameter { get; set; }
        public int CategoryId { get; set; }
        public string Function { get; set; }
        public string AddFunction { get; set; }
        public string Vibration { get; set; }
        public string Volume { get; set; }
        public string ModelYear { get; set; }
        public string InfoPrice { get; set; }
        public int VendorCountry { get; set; }
        public List<int> Offers { get; set; }
        public int Sale { get; set; }
    }
}