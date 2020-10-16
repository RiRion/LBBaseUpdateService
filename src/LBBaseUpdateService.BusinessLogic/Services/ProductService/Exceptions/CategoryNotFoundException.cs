using System;

namespace LBBaseUpdateService.BusinessLogic.Services.ProductService.Exceptions
{
    public class CategoryNotFoundException : ApplicationException
    {
        public CategoryNotFoundException(string message) : base(message){} 
    }
}