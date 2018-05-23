using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSession.Domain
{
    public class SessionInstance : PersistableEntity
    {
        public string SessionId { get; set; }

        public SessionInstance(string sessionId)
        {
            SessionId = sessionId;
        }

        public SessionInstance(string id, string sessionId, DateTime dateCreated, DateTime dateModified) 
            : base(id, dateCreated, dateModified)
        {
            SessionId = sessionId;
        }
    }
}
