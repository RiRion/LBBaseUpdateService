namespace LBBaseUpdateService.BusinessLogic.UpdateService.Interfaces
{
    public interface IState
    {
        void SetContext(UpdateContext context);
        void Update();
    }
}