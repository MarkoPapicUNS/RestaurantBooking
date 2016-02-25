using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ApplicationServices;
using ApplicationServices.Dtos;
using RestaurantBooking.API.Models;

namespace RestaurantBooking.API.Controllers
{
    public class RestaurantController : ApiController
    {
        IRestaurantAppService _appService;

        public RestaurantController(IRestaurantAppService appService)
        {
            _appService = appService;
        }

        [Route("api/restaurant/restaurants")]
        public IHttpActionResult GetRestaurants()
        {
            var username = User.Identity.Name;
            try
            {
                var restaurants = _appService.GetRestaurants(username);
                if (restaurants == null)
                    return NotFound();
                return Ok(restaurants);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [Route("api/restaurant/restaurant/{restaurantid}", Name = "RestaurantRoute")]
        public IHttpActionResult GetRestaurant(string restaurantId)
        {
            var username = User.Identity.Name;
            try
            {
                var restaurant = _appService.GetRestaurant(username, restaurantId);
                if (restaurant == null)
                    return NotFound();
                return Ok(restaurant);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [Route("api/restaurant/add")]
        public IHttpActionResult AddRestaurant([FromBody] string restaurantId)
        {
            try
            {
                var result = _appService.AddRestaurant(restaurantId);
                if (result.IsSuccess)
                    return Created(Url.Link("RestaurantRoute", new { restaurantid = restaurantId }), result.Message);
                return BadRequest(result.Message);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpDelete]
        [Route("api/restaurant/remove/{restaurantId}")]
        public IHttpActionResult RemoveRestaurant(string restaurantId)
        {
            if (string.IsNullOrEmpty(restaurantId))
                return BadRequest("Invalid request");

            var result = _appService.RemoveRestaurant(restaurantId);
            if (result.IsSuccess)
                return Ok(result.Message);
            return BadRequest("Invalid request");
        }

        [Route("api/restaurant/manager/{managerusername?}", Name = "RestaurantManagerRoute")]
        public IHttpActionResult GetRestaurantManger(string managerUsername = null)
        {
            var username = managerUsername ?? User.Identity.Name;

            var manager = _appService.GetRestaurantManager(username);
            if (manager == null)
                return NotFound();
            return Ok(manager);
        }

        [Route("api/restaurant/addmanager")]
        public IHttpActionResult AddRestaurantManager(RestaurantManagerInputDto restaurantManager)
        {
            try
            {
                var result = _appService.AddRestaurantManager(restaurantManager.Username, restaurantManager.RestaurantId);
                if (result.IsSuccess)
                    return Created(Url.Link("RestaurantManagerRoute", new { managerusername = restaurantManager.Username }), result.Message);
                return BadRequest(result.Message);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpDelete]
        [Route("api/restaurant/removemanager/{username}")]
        public IHttpActionResult RemoveRestaurantManager(string username)
        {
            if (string.IsNullOrEmpty(username))
                return BadRequest("Invalid request");

            var result = _appService.RemoveRestaurantManager(username);
            if (result.IsSuccess)
                return Ok(result.Message);
            return BadRequest("Invalid request");
        }

        [Route("api/restaurant/addmeal")]
        public IHttpActionResult AddMeal(MealDto meal)
        {
            try
            {
                var result = _appService.AddMeal(meal.RestaurantId, meal.Name, meal.Description, meal.Price);
                if (result.IsSuccess)
                    return Created(Url.Link("RestaurantManagerRoute", new { managerusername = "" }), result.Message);
                return BadRequest(result.Message);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpDelete]
        [Route("api/restaurant/removemeal/{restaurantId}/{mealname}")]
        public IHttpActionResult RemoveMeal(string restaurantId, string mealname)
        {
            if (string.IsNullOrEmpty(mealname))
                return BadRequest("Invalid request");

            var result = _appService.RemoveMeal(restaurantId, mealname);
            if (result.IsSuccess)
                return Ok(result.Message);
            return BadRequest("Invalid request");
        }
    }
}