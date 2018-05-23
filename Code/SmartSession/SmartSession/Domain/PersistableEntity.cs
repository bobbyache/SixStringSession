using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSession.Domain
{
    public abstract class PersistableEntity
    {
        private Guid identifyingGuid;

        public PersistableEntity()
        {
            this.identifyingGuid = Guid.Empty;
            this.DateCreated = DateTime.Now;
            this.DateModified = this.DateCreated;
        }

        public PersistableEntity(string id, DateTime dateCreated, DateTime dateModified)
        {
            this.DateCreated = dateCreated;
            this.DateModified = dateModified;
            this.identifyingGuid = new Guid(id);
        }

        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }

        public string Id
        {
            get
            {
                if (identifyingGuid == Guid.Empty)
                    identifyingGuid = Guid.NewGuid();
                return identifyingGuid.ToString();
            }
            protected set
            {
                identifyingGuid = new Guid(value);
            }
        }
    }
}
