using System.Threading.Tasks;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage;

namespace TheNewCSVEditorForLB.BusinessLogic.ExternalClients.LoveBeri.Interfaces
{
	public interface ILoveBeriClient
	{
		Task<ProductIdWithIntarnalId[]> GetInternalIdAsync();
	}
}