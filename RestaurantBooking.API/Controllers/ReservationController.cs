using ApplicationServices;
using RestaurantBooking.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RestaurantBooking.API.Controllers
{
    public class ReservationController : ApiController
    {
        private IReservationAppService _appService;

        public ReservationController(IReservationAppService appService)
        {
            _appService = appService;
        }

        [Route("api/reservation/reservation/{restaurantid}/{tablenumber}/{time}", Name = "ReservationRoute")]
        public IHttpActionResult GetReservation(string restaurantId, int tableNumber, DateTime time)
        {
            return Ok();
        }

        [Route("api/reservation/makereservation")]
        public IHttpActionResult MakeReservation(ReservationDto reservation)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.Identity.Name;
            var result = _appService.MakeReservation(username, reservation.RestaurantId, reservation.TableNumber, reservation.Time);
            if (result.IsSuccess)
                return Created(Url.Link("ReservationRoute", new { restaurantid = reservation.RestaurantId, tablenumber = reservation.TableNumber, time = reservation.Time }), result.Message);
            return BadRequest(result.Message);
        }
    }
}