using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ApplicationServices;
using RestaurantBooking.API.Models;

namespace RestaurantBooking.API.Controllers
{
    [Authorize(Roles = "Guest")]
    public class RatingController : ApiController
    {
        private IRatingAppService _appService;

        public RatingController(IRatingAppService appService)
        {
            _appService = appService;
        }

        [Route("api/rating/rating/{guestusername}/{restaurantid}", Name = "RatingRoute")]
        public IHttpActionResult GetRating()
        {
            return Ok();
        }

        [Route("api/rating/rate")]
        public IHttpActionResult RateRestaurant(RatingDto rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var username = User.Identity.Name;
            try
            {
                var result = _appService.RateRestaurant(username, rating.RestaurantId, rating.Rating, rating.Comment);
                if (result.IsSuccess)
                    return Ok(result.Message);
                return BadRequest(result.Message);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}