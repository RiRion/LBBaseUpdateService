using System;
using System.Threading.Tasks;
using AutoMapper;
using BitrixService.Clients.Loveberi.Interfaces;
using BitrixService.Models.ApiModels;
using BitrixService.Models.ApiModels.ResponseModels;
using LBBaseUpdateService.BusinessLogic.UpdateService.Interfaces;

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
            _context._state = null;
        }

        private async Task UpdateSite()
        {
            await AddOffers();
            await UpdateOffers();
            await DeleteOffers();
        }

        private async Task AddOffers()
        {
            if (_context._offers.ListToAdd.Count > 0)
            {
                while (_context._offers.ListToAdd.Count > 0)
                {
                    var response = await _loveberiClient.AddOfferWithRetryAsync(
                        _mapper.Map<OfferAto>(_context._offers.ListToAdd.Peek()));
                    if (response.Status > 0)
                        _context._offers.ListToAdd.Dequeue();
                    else
                    {
                        //TODO: need log.
                        //throw new ApplicationException(response.ErrorMessage);
                        _context._offers.ListToAdd.Dequeue();
                    }
                }
            }
        }

        private async Task UpdateOffers()
        {
            if (_context._offers.ListToUpdate.Count > 0)
            {
                while (_context._offers.ListToUpdate.Count > 0)
                {
                    var response = await _loveberiClient.UpdateOfferWithRetryAsync(
                        _mapper.Map<OfferAto>(_context._offers.ListToUpdate.Peek()));
                    if (response.Status > 0) _context._offers.ListToUpdate.Dequeue();
                    else
                    {
                        //TODO: need log.
                        //throw new ApplicationException(response.ErrorMessage);
                        _context._offers.ListToUpdate.Dequeue();
                    }
                }
            }
        }

        private async Task DeleteOffers()
        {
            if (_context._offers.ListToDelete.Count > 0)
            {
                while (_context._offers.ListToDelete.Count > 0)
                {
                    var response = await _loveberiClient.DeleteOfferWithRetry(_context._offers.ListToDelete.Peek());
                    if (response.Status > 0) _context._offers.ListToDelete.Dequeue();
                    else
                    {
                        //TODO: need log.
                        //throw new ApplicationException(response.ErrorMessage);
                        _context._offers.ListToDelete.Dequeue();
                    }
                }
            }
        }
        
        
    }
}