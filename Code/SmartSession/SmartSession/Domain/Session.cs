using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSession.Domain
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
    }
}
