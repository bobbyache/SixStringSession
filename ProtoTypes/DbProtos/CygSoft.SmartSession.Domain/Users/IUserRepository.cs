
using CygSoft.SmartSession.Domain.Users;

namespace CygSoft.SmartSession.Domain.Users
{
    public interface IUserRepository
    {
        User Select(long cellPhone, string password);
        int Insert(User obj);
    }
}
