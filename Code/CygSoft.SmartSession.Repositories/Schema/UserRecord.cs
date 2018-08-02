namespace CygSoft.SmartSession.Repositories.Schema
{
    public class UserRecord
    {
        public int Id { get; set; }
        public bool HasAccess { get; set; }
        public long CellPhone { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
