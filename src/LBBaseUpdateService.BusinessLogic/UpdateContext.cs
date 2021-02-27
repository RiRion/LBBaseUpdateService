using System;
using System.Threading.Tasks;
using Autofac;
using LBBaseUpdateService.BusinessLogic.UpdateService.Interfaces;
using LBBaseUpdateService.BusinessLogic.UpdateService.Models;
using LBBaseUpdateService.BusinessLogic.UpdateService.States;

namespace LBBaseUpdateService.BusinessLogic
{
    public class UpdateContext
    {
        public readonly VendorQueue _vendors;
        public readonly ProductQueue _products;
        public readonly OfferQueue _offers;

        internal readonly ILifetimeScope _lifetimeScope;

        public IState _state;

        public UpdateContext(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
            
            _vendors = new VendorQueue();
            _products = new ProductQueue();
            _offers = new OfferQueue();
        }

        public void TransitionTo(IState state)
        {
            _state = state;
            _state.SetContext(this);
        }

        public async Task UpdateAsync()
        {
            TransitionTo(_lifetimeScope.Resolve<UpdateServiceState>());
            while (_state != null)
            {
                await _state.UpdateAsync();
            }
        }
    }
}