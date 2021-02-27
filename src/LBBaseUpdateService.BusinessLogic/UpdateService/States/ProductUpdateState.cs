using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using BitrixService.Clients.Loveberi.Interfaces;
using BitrixService.Models.ApiModels;

namespace LBBaseUpdateService.BusinessLogic.UpdateService.States
{
    public class ProductUpdateState : StateBase
    {
        private readonly ILoveberiClient _loveberiClient;
        private readonly IMapper _mapper;

        public ProductUpdateState(
            ILoveberiClient loveberiClient,
            IMapper mapper
        )
        {
            _loveberiClient = loveberiClient;
            _mapper = mapper;
        }
        
        public override async Task UpdateAsync()
        {
            _loveberiClient.Login();
            await UpdateSite();
            _context.TransitionTo(_context._lifetimeScope.Resolve<OfferUpdateState>());
        }

        private async Task UpdateSite()
        {
            await AddProducts();
            await UpdateProducts();
            await DeleteProducts();
        }

        private async Task AddProducts()
        {
            if (_context._products.ListToAdd.Count > 0)
            {
                while (_context._products.ListToAdd.Count > 0)
                {
                    var response = await _loveberiClient.AddProductWithRetryAsync(
                        _mapper.Map<ProductAto>(_context._products.ListToAdd.Peek()));
                    if (response.Status > 0)
                        _context._products.ListToAdd.Dequeue();
                    else
                    {
                        //TODO: need log.
                        //throw new ApplicationException(response.ErrorMessage);
                        _context._products.ListToAdd.Dequeue();
                    }
                }
            }
        }

        private async Task UpdateProducts()
        {
            if (_context._products.ListToUpdate.Count > 0)
            {
                while (_context._products.ListToUpdate.Count > 0)
                {
                    var response = await _loveberiClient.UpdateProductWithRetryAsync(
                        _mapper.Map<ProductAto>(_context._products.ListToUpdate.Peek()));
                    if (response.Status > 0) _context._products.ListToUpdate.Dequeue();
                    else
                    {
                        //TODO: need log.
                        //throw new ApplicationException(response.ErrorMessage);
                        _context._products.ListToUpdate.Dequeue();
                    }
                }
            }
        }

        private async Task DeleteProducts()
        {
            if (_context._products.ListToDelete.Count > 0)
            {
                while (_context._products.ListToDelete.Count > 0)
                {
                    var response = await _loveberiClient.DeleteProductWithRetryAsync(_context._products.ListToDelete.Peek());
                    if (response.Status > 0) _context._products.ListToDelete.Dequeue();
                    else
                    {
                        //TODO: need log.
                        //throw new ApplicationException(response.ErrorMessage);
                        _context._products.ListToDelete.Dequeue();
                    }
                }
            }
        }
    }
}