using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.Generic.Repository
{
    public interface IEntity
    {
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
        string ModifiedBy { get; set; }
        string CreatedBy { get; set; }
    }
}
