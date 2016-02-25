﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Dtos;
using Restaurant.Domain;

namespace ApplicationServices
{
    public interface IRestaurantAppService
    {
        RestaurantDto GetRestaurant(string username, string restaurantId);
        IEnumerable<RestaurantDto> GetRestaurants(string username);
        ActionResultDto AddRestaurant(string restaurantId);
        ActionResultDto RemoveRestaurant(string restaurantId);
        RestaurantManagerDto GetRestaurantManager(string restaurantManagerUsername);
        ActionResultDto AddRestaurantManager(string restaurantManagerUsername, string restaurantId);
        ActionResultDto RemoveRestaurantManager(string restaurantManagerUsername);
        ActionResultDto AddMeal(string restaurantId, string mealName, string mealDescription, decimal mealPrice);
        ActionResultDto RemoveMeal(string restaurantId, string mealName);
    }
}
