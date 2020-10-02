using System;
using System.Runtime.Serialization;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Models.Exceptions
{
	public class DataFileNotFoundException : ApplicationException
	{
		public DataFileNotFoundException()
		{

		}
		public DataFileNotFoundException(String message) : base(message)
		{

		}
		public DataFileNotFoundException(String message, Exception ex) : base(message)
		{

		}
		protected DataFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{

		}
	}
}