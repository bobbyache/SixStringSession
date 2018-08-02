using CygSoft.SmartSession.Repositories.Schema;using System.Collections.Generic;

namespace CygSoft.SmartSession.Repositories.Interface
{
    public interface IRestaurantRepository
    {
        int Insert(RestaurantRecord obj);
        RestaurantRecord Select(int id);
        List<RestaurantRecord> SelectList();
    }
}
