using CygSoft.SmartSession.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Application
{
    public class Session : PersistableEntity, ISession
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

        internal SessionInstance CreateRecordableInstance()
        {
            SessionInstance sessionInstance = new SessionInstance(Id);
            return sessionInstance;
        }
    }
}
