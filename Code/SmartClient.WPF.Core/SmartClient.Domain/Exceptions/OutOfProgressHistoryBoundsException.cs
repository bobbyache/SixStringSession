using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SmartClient.Domain.Exceptions
{
	[Serializable()]
	public class OutOfProgressHistoryBoundsException : Exception, ISerializable
	{
		public OutOfProgressHistoryBoundsException() : base() { }
		public OutOfProgressHistoryBoundsException(string message) : base(message) { }
		public OutOfProgressHistoryBoundsException(string message, System.Exception inner) : base(message, inner) { }
		public OutOfProgressHistoryBoundsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
