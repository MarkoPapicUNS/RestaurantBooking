using System;
using System.Collections.Generic;
using System.Web.Http;
using ApplicationServices;
using ApplicationServices.Dtos;
using Guest.Services.Exceptions;

namespace RestaurantBooking.API.Controllers
{
    [Authorize(Roles = "Guest")]
    public class FriendshipController : ApiController
    {
        private IFriendshipAppService _appService;

        public FriendshipController(IFriendshipAppService appService)
        {
            _appService = appService;
        }

        public IHttpActionResult GetFriends()
        {
            throw new NotImplementedException();
        }

        [Route("api/friendship/friendrequest/{responderUsername}", Name = "FriendshipRoute")]
        public IHttpActionResult GetFriendRequest(string responderUsername)
        {
            if (string.IsNullOrEmpty(responderUsername))
                return BadRequest("Invalid request");

            FriendshipDto friendship;
            var requesterUsername = User.Identity.Name;
            try
            {
                friendship = _appService.GetFriendRequest(requesterUsername, responderUsername);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            if (friendship == null)
                return NotFound();
            return Ok(friendship);
        }

        [Route("api/friendship/friendrequests")]
        public IHttpActionResult GetFriendRequests()
        {
            var username = User.Identity.Name;
            IEnumerable<FriendshipDto> friendships = _appService.GetFriendRequests(username);
            return Ok(friendships);
        }

        public IHttpActionResult GetSentFriendRequests()
        {
            var username = User.Identity.Name;
            IEnumerable<FriendshipDto> friendships = _appService.GetSentFriendRequests(username);
            return Ok(friendships);
        }

        [HttpPost]
        [Route("api/friendship/sendfriendrequest")]
        public IHttpActionResult SendFriendRequest([FromBody] string recipientUsername)
        {
            if (string.IsNullOrEmpty(recipientUsername))
                return BadRequest("Invalid request");

            var senderUsername = User.Identity.Name;
            var result = _appService.SendFriendRequest(senderUsername, recipientUsername);
            if (result.IsSuccess)
                return Created(Url.Link("FriendshipRoute", new { responderUsername = recipientUsername }), result.Message);
            else
                return BadRequest(result.Message);
        }
    }
}