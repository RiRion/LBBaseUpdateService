using System;
using System.Collections.Generic;
using TheNewCSVEditorForLB.Data.Models;

namespace TheNewCSVEditorForLB.Data.Interfaces
{
	public interface IProductIdWithInternalIdRepository
	{
		List<ProductIdWithIntarnalId> AllIntarnalId { get; }
		Boolean GetInternalIdFromServer(String path);
	}
}