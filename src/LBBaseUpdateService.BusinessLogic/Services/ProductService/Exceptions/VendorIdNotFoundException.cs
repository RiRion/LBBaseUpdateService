using System;

namespace LBBaseUpdateService.BusinessLogic.Services.ProductService.Exceptions
{
    public class VendorIdNotFoundException : ApplicationException
    {
        public VendorIdNotFoundException(string message) : base(message)
        {
            
        }
    }
}