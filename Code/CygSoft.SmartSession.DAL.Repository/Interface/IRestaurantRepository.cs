using CygSoft.SmartSession.BaseRepository.Schema;
using System.Collections.Generic;

namespace CygSoft.SmartSession.BaseRepository.Interface
{
    public interface IRestaurantRepository
    {
        int Insert(RestaurantModel obj);
        RestaurantModel Select(int id);
        List<RestaurantModel> SelectList();
    }
}
