using System;
using System.Web.Http;
using ApplicationServices;
using ApplicationServices.Dtos;

namespace RestaurantBooking.API.Controllers
{

    public class FriendshipController : ApiController
    {
        private IFriendshipService _service;

        public FriendshipController(IFriendshipService service)
        {
            _service = service;
        }

        public IHttpActionResult GetFriends(string username)
        {
            return Ok();
        }

        [Route("api/friendship/friendship/{responderUsername}", Name = "FriendshipRoute")]
        public IHttpActionResult GetFriendship(string responderUsername)
        {
            if (string.IsNullOrEmpty(responderUsername))
                return BadRequest("Invalid request");

            FriendshipDto friendship;
            var requesterUsername = User.Identity.Name;
            try
            {
                friendship = _service.GetFriendship(requesterUsername, responderUsername);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
            if (friendship == null)
                return NotFound();
            return Ok(friendship);
        }

        [Authorize(Roles = "Guest")]
        [HttpPost]
        [Route("api/friendship/sendfriendrequest")]
        public IHttpActionResult SendFriendRequest([FromBody] string recipientUsername)
        {
            if (string.IsNullOrEmpty(recipientUsername))
                return BadRequest("Invalid request");

            var senderUsername = User.Identity.Name;
            var result = _service.SendFriendRequest(senderUsername, recipientUsername);
            if (result.IsSuccess)
                return Created(Url.Link("FriendshipRoute", new { responderUsername = recipientUsername }), result.Message);
            else
                return BadRequest(result.Message);
        }
    }
}