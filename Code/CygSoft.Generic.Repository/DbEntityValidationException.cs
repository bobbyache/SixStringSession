using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.Generic.Repository
{
    public class DbEntityValidationException : Exception
    {
        public DbEntityValidationException(string message, EntityValidationErrors[] entityValidationErrors) : base(message)
        {
            EntityValidationErrors = entityValidationErrors;
        }

        public EntityValidationErrors[] EntityValidationErrors { get; }
    }

    public class EntityValidationErrors
    {
        public IEnumerable<EntityValidationError> ValidationErrors { get; internal set; }
    }

    public class EntityValidationError
    {
        public string ErrorMessage { get; internal set; }
    }
}
