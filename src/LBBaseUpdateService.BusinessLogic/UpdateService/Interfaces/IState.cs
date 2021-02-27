using System.Threading.Tasks;

namespace LBBaseUpdateService.BusinessLogic.UpdateService.Interfaces
{
    public interface IState
    {
        void SetContext(UpdateContext context);
        Task UpdateAsync();
    }
}