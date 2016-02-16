using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ApplicationServices;
using ApplicationServices.Dtos;

namespace RestaurantBooking.API.Controllers
{
    public class GuestController : ApiController
    {
        private IGuestAppService _appService;

        public GuestController(IGuestAppService appService)
        {
            _appService = appService;
        }

        [Route("api/guest/guest")]
        public IHttpActionResult GetGuest(string username)
        {
            return Ok();
        }

        [Route("api/guest/friends")]
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
            return Ok(friends);
        }

        /*[Route("api/guest/friendrequests")]
        public IHttpActionResult GetFriendRequests()
        {
            IEnumerable<FriendRequestDto> friendRequests;
            var username = User.Identity.Name;
            try
            {
                friendRequests = _appService.GetFriendRequests(username);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
//            if (friendRequests == null)
//                return BadRequest();
            return Ok(friendRequests);
        }

        [Route("api/guest/sentfriendrequests")]
        public IHttpActionResult GetSentFriendRequests()
        {
            IEnumerable<FriendRequestDto> friendships;
            var username = User.Identity.Name;
            try
            {
                friendships = _appService.GetSentFriendRequests(username);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
//            if (friendships == null)
//                return BadRequest();
            return Ok(friendships);
        }*/
    }
}