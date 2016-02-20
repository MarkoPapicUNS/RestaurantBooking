using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain;

namespace Restaurant.Services
{
    public interface IRestaurantService
    {
        Domain.Restaurant GetRestaurant(string restaurantId);
        IEnumerable<Domain.Restaurant> GetRestaurants();
        void AddRestaurant(string restaurantId);
        void RemoveRestaurant(string restaurantId);
        RestaurantManager GetRestaurantManager(string restaurantManagerUsername);
        void AddRestaurantManager(string restaurantManagerUsername, string restaurantId);
        void RemoveRestaurantManager(string restaurantManagerUsername);
    }
}
