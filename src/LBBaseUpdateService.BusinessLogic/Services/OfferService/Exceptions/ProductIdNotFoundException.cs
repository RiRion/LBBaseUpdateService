using System;

namespace LBBaseUpdateService.BusinessLogic.Services.OfferService.Exceptions
{
    public class ProductIdNotFoundException : ApplicationException
    {
        public ProductIdNotFoundException(string message) : base(message){}
    }
}