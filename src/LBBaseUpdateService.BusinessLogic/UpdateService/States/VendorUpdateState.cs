using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using BitrixService.Clients.Loveberi.Interfaces;
using BitrixService.Models.ApiModels;

namespace LBBaseUpdateService.BusinessLogic.UpdateService.States
{
    public class VendorUpdateState : StateBase
    {
        private readonly ILoveberiClient _loveberiClient;
        private readonly IMapper _mapper;

        public VendorUpdateState(
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
            await UpdateSiteAsync();
            _context.TransitionTo(_context._lifetimeScope.Resolve<ProductUpdateState>());
        }

        private async Task UpdateSiteAsync()
        {
            await Task.WhenAll(AddVendorsAsync(),
                UpdateVendorsAsync(),
                DeleteVendorsAsync()
                );
        }

        private async Task AddVendorsAsync()
        {
            if (_context.Vendors.ListToAdd.Count > 0)
            {
                while (_context.Vendors.ListToAdd.Count > 0)
                {
                    var response = await _loveberiClient.AddVendorWithRetryAsync(
                        _mapper.Map<VendorAto>(_context.Vendors.ListToAdd.Peek()));
                    if (response.Status > 0)
                        _context.Vendors.ListToAdd.Dequeue();
                    else
                    {
                        //TODO: need log.
                        //throw new ApplicationException(response.ErrorMessage);
                        _context.Vendors.ListToAdd.Dequeue();
                    }
                }
            }
        }

        private async Task UpdateVendorsAsync()
        {
            
        }

        private async Task DeleteVendorsAsync()
        {
            
        }
    }
}