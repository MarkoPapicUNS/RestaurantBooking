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

        [Route("api/friendship/friends")]
        public IHttpActionResult GetFriends()
        {
            IEnumerable<FriendDto> friends;
            var username = User.Identity.Name;
            try
            {
                friends = _appService.GetFriends(username);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            if (friends == null)
                return NotFound();
            return Ok(friends);
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
            IEnumerable<FriendshipDto> friendships;
            var username = User.Identity.Name;
            try
            {
                friendships = _appService.GetFriendRequests(username);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            if (friendships == null)
                return BadRequest();
            return Ok(friendships);
        }

        [Route("api/friendship/sentfriendrequests")]
        public IHttpActionResult GetSentFriendRequests()
        {
            IEnumerable<FriendshipDto> friendships;
            var username = User.Identity.Name;
            try
            {
                friendships = _appService.GetSentFriendRequests(username);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            if (friendships == null)
                return BadRequest();
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
                return Created(Url.Link("FriendshipRoute", new {responderUsername = recipientUsername}), result.Message);
            return BadRequest(result.Message);
        }
    }
}