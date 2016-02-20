using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ApplicationServices;
using ApplicationServices.Models;

namespace RestaurantBooking.API.Controllers
{
    public class GuestController : ApiController
    {
        private IGuestAppService _appService;

        public GuestController(IGuestAppService appService)
        {
            _appService = appService;
        }

		[Authorize(Roles="Guest")]
        [Route("api/guest/guest/{guestUsername?}", Name = "GuestRoute")]
        public IHttpActionResult GetGuest(string guestUsername = null)
        {
            var username = User.Identity.Name;
            guestUsername = guestUsername ?? username;
            try
            {
                var guest = _appService.GetGuest(username, guestUsername);
                if (guest == null)
                    return NotFound();
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

        [Route("api/guest/add")]
        public IHttpActionResult AddGuest([FromBody] string username)
        {
            if (username != User.Identity.Name)
                return Unauthorized();
            
            try
            {
                var result = _appService.AddGuest(username);
                if (result.IsSuccess)
                    return Created(Url.Link("GuestRoute", new { guestUsername = username }), result.Message);
                return BadRequest(result.Message);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

		[Route("api/guest/updateprofile")]
	    public IHttpActionResult UpdateProfile(ProfileModel profileModel)
	    {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var username = User.Identity.Name;
			if (username !=  profileModel.Username)
				return Unauthorized();

		    try
		    {
			    var result = _appService.UpdateProfile(profileModel);
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