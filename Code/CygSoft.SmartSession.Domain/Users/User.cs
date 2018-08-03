using CygSoft.SmartSession.Infrastructure;

namespace CygSoft.SmartSession.Domain.Users
{
    public class User : EntityBase
    {
        public bool HasAccess { get; set; }
        public long CellPhone { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
