using System;
using System.Collections.Generic;
using System.Web.Http;
using ApplicationServices;

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

        [Route("api/friendship/friendrequest/{responderUsername}", Name = "FriendshipRoute")]
        public IHttpActionResult GetFriendRequest(string responderUsername)
        {
            if (string.IsNullOrEmpty(responderUsername))
                return BadRequest("Invalid request");

            string friendRequest;
            var requesterUsername = User.Identity.Name;
            try
            {
                friendRequest = _appService.GetFriendRequest(requesterUsername, responderUsername);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            if (friendRequest == null)
                return NotFound();
            return Ok(friendRequest);
        }

        [Route("api/friendship/sendfriendrequest")]
        public IHttpActionResult SendFriendRequest([FromBody] string recipientUsername)
        {
            if (string.IsNullOrEmpty(recipientUsername))
                return BadRequest("Invalid request");

            var senderUsername = User.Identity.Name;
            var result = _appService.SendFriendRequest(senderUsername, recipientUsername);
            if (result.IsSuccess)
                return Created(Url.Link("FriendshipRoute", new {responderUsername = recipientUsername}), result.Message);
            return BadRequest(result.Message);
        }

        [Route("api/friendship/acceptfriendrequest")]
        public IHttpActionResult AcceptFriendRequest([FromBody] string senderUsername)
        {
            if (string.IsNullOrEmpty(senderUsername))
                return BadRequest("Invalid request");

            var recipientUsername = User.Identity.Name;
            var result = _appService.AcceptFriendRequest(senderUsername, recipientUsername);
            if (result.IsSuccess)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }

        [Route("api/friendship/removefriend")]
        public IHttpActionResult RemoveFriend([FromBody] string friendUsername)
        {
            if (string.IsNullOrEmpty(friendUsername))
                return BadRequest("Invalid request");

            var username = User.Identity.Name;
            var result = _appService.RemoveFriendship(username, friendUsername);
            if (result.IsSuccess)
                return Ok(result.Message);
            return BadRequest("Invalid request");
        }
    }
}