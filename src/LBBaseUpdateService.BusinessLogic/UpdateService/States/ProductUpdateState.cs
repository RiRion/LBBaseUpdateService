using Autofac;

namespace LBBaseUpdateService.BusinessLogic.UpdateService.States
{
    public class ProductUpdateState : StateBase
    {
        public override void Update()
        {
            
            _context.TransitionTo(_context._lifetimeScope.Resolve<OfferUpdateState>());
            _context._state.Update();
        }
    }
}