using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Dtos;
using Restaurant.Domain;

namespace ApplicationServices.Adapters
{
    public interface IRestaurantAdapter
    {
        RestaurantDto AdaptRestaurant(string username, Restaurant.Domain.Restaurant restaurant);
        RestaurantManagerDto AdaptRestaurantManagerDto(RestaurantManager restaurantManager);
    }
}
