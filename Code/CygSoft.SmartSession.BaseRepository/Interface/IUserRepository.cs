using CygSoft.SmartSession.BaseRepository.Schema;

namespace CygSoft.SmartSession.BaseRepository.Interface
{
    public interface IUserRepository
    {
        UserModel Select(long cellPhone, string password);
        int Insert(UserModel obj);
    }
}
