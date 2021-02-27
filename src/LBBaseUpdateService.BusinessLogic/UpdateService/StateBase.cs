using LBBaseUpdateService.BusinessLogic.UpdateService.Interfaces;

namespace LBBaseUpdateService.BusinessLogic.UpdateService
{
    public abstract class StateBase : IState
    {
        protected UpdateContext _context;

        public void SetContext(UpdateContext context)
        {
            _context = context;
        }

        public abstract void Update();
    }
}