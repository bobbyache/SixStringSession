using CygSoft.SmartSession.Repositories.Schema;using System.Collections.Generic;

namespace CygSoft.SmartSession.Repositories.Interface{
    public interface IRestaurantRepository
    {
        int Insert(RestaurantModel obj);
        RestaurantModel Select(int id);
        List<RestaurantModel> SelectList();
    }
}
