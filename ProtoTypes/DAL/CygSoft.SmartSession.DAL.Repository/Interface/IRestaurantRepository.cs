using CygSoft.SmartSession.DAL.Repository.Schema;
using System.Collections.Generic;

namespace CygSoft.SmartSession.DAL.Repository.Interface
{
    public interface IRestaurantRepository
    {
        int Insert(RestaurantModel obj);
        RestaurantModel Select(int id);
        List<RestaurantModel> SelectList();
    }
}
