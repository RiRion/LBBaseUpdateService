using System;
using System.Runtime.Serialization;

namespace LBBaseUpdateService.BusinessLogic.Services.Models.Exceptions
{
	public class ExternalIdRetrievalException : ApplicationException
	{
		public ExternalIdRetrievalException()
		{

		}
		public ExternalIdRetrievalException(String message) : base(message)
		{

		}
		public ExternalIdRetrievalException(String message, Exception ex) : base(message)
		{

		}
		protected ExternalIdRetrievalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{

		}
	}
}