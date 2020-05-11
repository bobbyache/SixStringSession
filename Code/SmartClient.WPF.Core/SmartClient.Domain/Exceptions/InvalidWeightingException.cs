using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SmartClient.Domain.Exceptions
{
	[Serializable()]
	public class InvalidWeightingException : Exception, ISerializable
	{
		public InvalidWeightingException() : base() { }
		public InvalidWeightingException(string message) : base(message) { }
		public InvalidWeightingException(string message, System.Exception inner) : base(message, inner) { }
		public InvalidWeightingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
