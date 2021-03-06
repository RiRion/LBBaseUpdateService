using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BitrixService.Clients.Loveberi.Interfaces;
using BitrixService.Models.ApiModels;

namespace LBBaseUpdateService.BusinessLogic.UpdateService.States
{
    public class OfferUpdateState : StateBase
    {
        private readonly ILoveberiClient _loveberiClient;
        private readonly IMapper _mapper;

        public OfferUpdateState(
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
            _context.State = null;
        }

        private async Task UpdateSite()
        {
            await Task.WhenAll(
                AddOffers(), 
                UpdateOffers(), 
                DeleteOffers()
                );
        }

        private async Task AddOffers()
        {
            if (_context.Offers.ListToAdd.Count > 0)
            {
                while (_context.Offers.ListToAdd.Count > 0)
                {
                    var response = await _loveberiClient.AddOfferWithRetryAsync(
                        _mapper.Map<OfferAto>(_context.Offers.ListToAdd.Peek()));
                    if (response.Status > 0)
                        _context.Offers.ListToAdd.Dequeue();
                    else
                    {
                        //TODO: need log.
                        //throw new ApplicationException(response.ErrorMessage);
                        _context.Offers.ListToAdd.Dequeue();
                    }
                }
            }
        }

        private async Task UpdateOffers()
        {
            if (_context.Offers.ListToUpdate.Count > 0)
            {
                while (_context.Offers.ListToUpdate.Count > 0)
                {
                    var response = await _loveberiClient.UpdateOfferWithRetryAsync(
                        _mapper.Map<OfferAto>(_context.Offers.ListToUpdate.Peek()));
                    if (response.Status > 0) _context.Offers.ListToUpdate.Dequeue();
                    else
                    {
                        //TODO: need log.
                        //throw new ApplicationException(response.ErrorMessage);
                        _context.Offers.ListToUpdate.Dequeue();
                    }
                }
            }
        }

        private async Task DeleteOffers()
        {
            if (_context.Offers.ListToDelete.Count > 0)
            {
                while (_context.Offers.ListToDelete.Count > 0)
                {
                    var response = await _loveberiClient.DeleteOfferWithRetry(_context.Offers.ListToDelete.Peek());
                    if (response.Status > 0) _context.Offers.ListToDelete.Dequeue();
                    else
                    {
                        //TODO: need log.
                        //throw new ApplicationException(response.ErrorMessage);
                        _context.Offers.ListToDelete.Dequeue();
                    }
                }
            }
        }
        
        
    }
}