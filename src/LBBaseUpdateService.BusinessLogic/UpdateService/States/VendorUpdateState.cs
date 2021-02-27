using Autofac;

namespace LBBaseUpdateService.BusinessLogic.UpdateService.States
{
    public class VendorUpdateState : StateBase
    {
        public override void Update()
        {
            
            _context.TransitionTo(_context._lifetimeScope.Resolve<ProductUpdateState>());
            _context._state.Update();
        }
    }
}