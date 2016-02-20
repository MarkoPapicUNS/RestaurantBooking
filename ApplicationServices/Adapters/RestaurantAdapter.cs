using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Dtos;
using Restaurant.Domain;

namespace ApplicationServices.Adapters
{
    public class RestaurantAdapter : IRestaurantAdapter
    {
        public RestaurantDto AdaptRestaurant(string username, Restaurant.Domain.Restaurant restaurant)
        {
            if (restaurant ==  null)
                throw new ArgumentNullException("restaurant");

            var yourRating = restaurant.Ratings.FirstOrDefault(r => r.GuestUsername == username);

            var restaurantDto = new RestaurantDto
            {
                RestaurantId = restaurant.RestaurantId,
                Name = restaurant.Name,
                Description = restaurant.Description,
                Menu = restaurant.Menu,
                Tables = restaurant.Tables,
                Rating = (double)restaurant.Ratings.Sum(x => x.Grade) / (double)restaurant.Ratings.Count(),
                YourRating = yourRating == null ? 0 : yourRating.Grade,
                Ratings = restaurant.Ratings
            };
            return restaurantDto;
        }

        public RestaurantManagerDto AdaptRestaurantManagerDto(RestaurantManager restaurantManager)
        {
            if (restaurantManager == null)
                throw new ArgumentNullException("restaurantManager");

            var restaurantManagerDto = new RestaurantManagerDto
            {
                Username = restaurantManager.Username,
                FirstName = restaurantManager.FirstName,
                LastName = restaurantManager.LastName,
                Address = restaurantManager.Address,
                Gender = restaurantManager.Gender,
                Picture = restaurantManager.Picture
            };
            return restaurantManagerDto;
        }
    }
}
