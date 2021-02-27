namespace BitrixService.Models.ApiModels.ResponseModels
{
    public class ApiResponse
    {
        public int Status { get; set; }
        public string ObjectType { get; set; }
        public string Method { get; set; }
        public string ExId { get; set; }
        public string ErrorMessage { get; set; }
    }
}