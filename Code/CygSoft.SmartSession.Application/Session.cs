using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Application
{
    public class Session : PersistableEntity
    {
        public string Title { get; set; }

        public Session()
        {

        }

        public Session(string id, string title, DateTime dateCreated, DateTime dateModified) 
            : base(id, dateCreated, dateModified)
        {
            Title = title;
        }

        public SessionInstance CreateRecordableInstance()
        {
            SessionInstance sessionInstance = new SessionInstance(Id);
            return sessionInstance;
        }
    }
}
