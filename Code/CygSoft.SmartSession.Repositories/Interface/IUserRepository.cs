using CygSoft.SmartSession.Repositories.Schema;
namespace CygSoft.SmartSession.Repositories.Interface
{
    public interface IUserRepository
    {
        UserRecord Select(long cellPhone, string password);
        int Insert(UserRecord obj);
    }
}
