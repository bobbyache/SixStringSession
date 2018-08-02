using CygSoft.SmartSession.DAL.Repository.Schema;

namespace CygSoft.SmartSession.DAL.Repository.Interface
{
    public interface IUserRepository
    {
        UserModel Select(long cellPhone, string password);
        int Insert(UserModel obj);
    }
}
