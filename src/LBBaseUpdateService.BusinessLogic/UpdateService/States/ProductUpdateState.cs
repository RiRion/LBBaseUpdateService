using System.Collections.Generic;
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
            await Task.WhenAll(
                AddProducts(),
                UpdateProducts(),
                DeleteProducts()
                );
        }

        private async Task AddProducts()
        {
            if (_context.Products.ListToAdd.Count > 0)
            {
                while (_context.Products.ListToAdd.Count > 0)
                {
                    var response = await _loveberiClient.AddProductWithRetryAsync(
                        _mapper.Map<ProductAto>(_context.Products.ListToAdd.Peek()));
                    if (response.Status > 0)
                        _context.Products.ListToAdd.Dequeue();
                    else
                    {
                        //TODO: need log.
                        //throw new ApplicationException(response.ErrorMessage);
                        _context.Products.ListToAdd.Dequeue();
                    }
                }
            }
        }

        private async Task UpdateProducts()
        {
            if (_context.Products.ListToUpdate.Count > 0)
            {
                while (_context.Products.ListToUpdate.Count > 0)
                {
                    var response = await _loveberiClient.UpdateProductWithRetryAsync(
                        _mapper.Map<ProductAto>(_context.Products.ListToUpdate.Peek()));
                    if (response.Status > 0) _context.Products.ListToUpdate.Dequeue();
                    else
                    {
                        //TODO: need log.
                        //throw new ApplicationException(response.ErrorMessage);
                        _context.Products.ListToUpdate.Dequeue();
                    }
                }
            }
        }

        private async Task DeleteProducts()
        {
            if (_context.Products.ListToDelete.Count > 0)
            {
                while (_context.Products.ListToDelete.Count > 0)
                {
                    var response = await _loveberiClient.DeleteProductWithRetryAsync(_context.Products.ListToDelete.Peek());
                    if (response.Status > 0) _context.Products.ListToDelete.Dequeue();
                    else
                    {
                        //TODO: need log.
                        //throw new ApplicationException(response.ErrorMessage);
                        _context.Products.ListToDelete.Dequeue();
                    }
                }
            }
        }
    }
}