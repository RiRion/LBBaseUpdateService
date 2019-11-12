using TheNewCSVEditorForLB.Data.Models;
using System.Collections.Generic;

namespace TheNewCSVEditorForLB.Data.Interfaces
{
    public interface IProductIdWithInternalIdRepository
    {
        List<ProductIdWithIntarnalId> AllIntarnalId { get; }
        bool GetInternalIdFromServer(string path);
    }
}