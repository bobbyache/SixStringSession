using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Dal.MySql
{
    [Serializable]
    public class DatabaseEntityNotFoundException : Exception, ISerializable
    {
        public DatabaseEntityNotFoundException() : base() { }
        public DatabaseEntityNotFoundException(string message) : base(message) { }
        public DatabaseEntityNotFoundException(string message, System.Exception inner) : base(message, inner) { }
        public DatabaseEntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class DatabaseCommitException : Exception, ISerializable
    {
        public DatabaseCommitException() : base() { }
        public DatabaseCommitException(string message) : base(message) { }
        public DatabaseCommitException(string message, System.Exception inner) : base(message, inner) { }
        public DatabaseCommitException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class DatabaseRollbackException : Exception, ISerializable
    {
        public DatabaseRollbackException() : base() { }
        public DatabaseRollbackException(string message) : base(message) { }
        public DatabaseRollbackException(string message, System.Exception inner) : base(message, inner) { }
        public DatabaseRollbackException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
