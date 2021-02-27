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
        
        public override async void Update()
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
                for (int i = 0; i < _context._offers.ListToAdd.Count; i++)
                {
                    var response = await _loveberiClient.AddOfferWithRetryAsync(
                        _mapper.Map<OfferAto>(_context._offers.ListToAdd.Peek()));
                    if (response.Status > 0)
                        _context._offers.ListToAdd.Dequeue();
                    else throw new ApplicationException(response.ErrorMessage);
                }
            }
            
        }

        private async Task UpdateOffers()
        {
            if (_context._offers.ListToUpdate.Count > 0)
            {
                for (int i = 0; i < _context._offers.ListToUpdate.Count; i++)
                {
                    var response = await _loveberiClient.UpdateOfferWithRetryAsync(
                        _mapper.Map<OfferAto>(_context._offers.ListToUpdate.Peek()));
                    if (response.Status > 0) _context._offers.ListToUpdate.Dequeue();
                    else throw new ApplicationException(response.ErrorMessage);
                }
            }
        }

        private async Task DeleteOffers()
        {
            if (_context._offers.ListToDelete.Count > 0)
            {
                for (int i = 0; i < _context._offers.ListToDelete.Count; i++)
                {
                    var response = await _loveberiClient.DeleteOfferWithRetry(
                        _mapper.Map<OfferAto>(_context._offers.ListToDelete.Peek()));
                    if (response.Status > 0) _context._offers.ListToDelete.Dequeue();
                    else throw new ApplicationException(response.ErrorMessage);
                }
            }
        }
        
        
    }
}