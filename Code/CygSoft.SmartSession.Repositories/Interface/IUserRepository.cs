using CygSoft.SmartSession.Repositories.Schema;
namespace CygSoft.SmartSession.Repositories.Interface{
    public interface IUserRepository
    {
        UserModel Select(long cellPhone, string password);
        int Insert(UserModel obj);
    }
}
