using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ApplicationServices;
using ApplicationServices.Dtos;

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

        [Route("api/restaurant/remove")]
        public IHttpActionResult RemoveRestaurant(string restaurantId)
        {
            if (string.IsNullOrEmpty(restaurantId))
                return BadRequest("Invalid request");

            var result = _appService.RemoveRestaurant(restaurantId);
            if (result.IsSuccess)
                return Ok(result.Message);
            return BadRequest("Invalid request");
        }

        [Route("api/restaurant/manager/{managerusername}", Name = "RestaurantManagerRoute")]
        public IHttpActionResult GetRestaurantManger(string managerUsername)
        {
            if (string.IsNullOrEmpty(managerUsername))
                return BadRequest("Invalid request");

            var result = _appService.RemoveRestaurantManager(managerUsername);
            if (result.IsSuccess)
                return Ok(result.Message);
            return BadRequest("Invalid request");
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

        [Route("api/restaurant/removemanager")]
        public IHttpActionResult RemoveRestaurantManager([FromBody] string username)
        {
            if (string.IsNullOrEmpty(username))
                return BadRequest("Invalid request");

            var result = _appService.RemoveRestaurantManager(username);
            if (result.IsSuccess)
                return Ok(result.Message);
            return BadRequest("Invalid request");
        }
    }
}