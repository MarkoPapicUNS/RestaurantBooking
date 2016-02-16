using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ApplicationServices;

namespace RestaurantBooking.API.Controllers
{
    public class GuestController : ApiController
    {
        private IGuestAppService _appService;

        public GuestController(IGuestAppService appService)
        {
            _appService = appService;
        }

        [Route("api/guest/guest/{guestUsername?}")]
        public IHttpActionResult GetGuest(string guestUsername)
        {
            var username = User.Identity.Name;
            guestUsername = guestUsername ?? username;
            try
            {
                var guest = _appService.GetGuest(username, guestUsername);
                if (guest == null)
                    return BadRequest();
                return Ok(guest);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [Route("api/guest/guests")]
        public IHttpActionResult GetGuests()
        {
            var username = User.Identity.Name;
            return Ok(_appService.GetGuests(username));
        }
    }
}