using System;
using System.Collections.Generic;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Interfaces
{
	public interface IProductIdWithInternalIdRepository
	{
		List<ProductIdWithIntarnalId> AllIntarnalId { get; }
		Boolean GetInternalIdFromServer(String path);
	}
}