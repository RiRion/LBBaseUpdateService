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
        public VendorQueue Vendors { get; }
        public ProductQueue Products { get; }
        public OfferQueue Offers { get; }
        public IState State { get; set; }
        
        internal readonly ILifetimeScope _lifetimeScope;

        public UpdateContext(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
            
            Vendors = new VendorQueue();
            Products = new ProductQueue();
            Offers = new OfferQueue();
        }

        public void TransitionTo(IState state)
        {
            State = state;
            State.SetContext(this);
        }

        public async Task UpdateAsync()
        {
            TransitionTo(_lifetimeScope.Resolve<UpdateServiceState>());
            while (State != null)
            {
                await State.UpdateAsync();
            }
        }
    }
}