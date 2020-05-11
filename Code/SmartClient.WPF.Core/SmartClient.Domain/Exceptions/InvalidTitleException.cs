using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SmartClient.Domain.Exceptions
{
	[Serializable()]
	public class InvalidTitleException : Exception, ISerializable
	{
		public InvalidTitleException() : base() { }
		public InvalidTitleException(string message) : base(message) { }
		public InvalidTitleException(string message, System.Exception inner) : base(message, inner) { }
		public InvalidTitleException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
