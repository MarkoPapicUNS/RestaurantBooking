using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain;
using Restaurant.Services.RepositoryContracts;
using Shared;

namespace Restaurant.Services
{
    public class RestaurantService : IRestaurantService
    {
        private IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public Domain.Restaurant GetRestaurant(string restaurantId)
        {
            var restaurant = _restaurantRepository.Find(restaurantId);
            if (restaurant == null)
                throw new RestaurantException(string.Format("Restaurant {0} doesn't exist"));
            return restaurant;
        }

        public IEnumerable<Domain.Restaurant> GetRestaurants()
        {
            return _restaurantRepository.All().ToArray();
        }

        public void AddRestaurant(string restaurantId)
        {
            var restaurant = _restaurantRepository.Find(restaurantId);
            if (restaurant != null)
                throw new RestaurantException(string.Format("Restaurant {0} is already exists"));
            _restaurantRepository.Insert(new Restaurant.Domain.Restaurant
            {
                RestaurantId = restaurantId
            });
            _restaurantRepository.Commit();
        }

        public void RemoveRestaurant(string restaurantId)
        {
            var restaurant = _restaurantRepository.Find(restaurantId);
            if (restaurant != null)
                _restaurantRepository.Delete(restaurant);
            _restaurantRepository.Commit();
        }

        public RestaurantManager GetRestaurantManager(string restaurantManagerUsername)
        {
            var restaurantManager =
                _restaurantRepository.All()
                    .SelectMany(r => r.Managers)
                    .FirstOrDefault(rm => rm.Username == restaurantManagerUsername);
            if (restaurantManager == null)
                throw new RestaurantException(string.Format("Manger {0} doesn't exist"));
            return restaurantManager;
        }

        public void AddRestaurantManager(string restaurantManagerUsername, string restaurantId)
        {
            var restaurants = _restaurantRepository.All();
            if (restaurants.Any(r => r.Managers.Any(rm => rm.Username == restaurantManagerUsername)))
                throw new RestaurantException(string.Format("{0} is already a manager", restaurantManagerUsername));
            var restaurant = restaurants.FirstOrDefault(r => r.RestaurantId == restaurantId);
            if (restaurant == null)
                throw new RestaurantException("Restaurant not found");
            restaurant.Managers.Add(new RestaurantManager
            {
                Username = restaurantManagerUsername,
                Address = new Address()
            });
            _restaurantRepository.Commit();
        }

        public void RemoveRestaurantManager(string restaurantManagerUsername)
        {
            var restaurantManager =
                _restaurantRepository.All()
                    .SelectMany(r => r.Managers)
                    .FirstOrDefault(rm => rm.Username == restaurantManagerUsername);
            if (restaurantManager != null)
                _restaurantRepository.RemoveManager(restaurantManager);
            _restaurantRepository.Commit();
        }
    }
}
