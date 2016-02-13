using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using RestaurantBooking.API.Models;

namespace RestaurantBooking.API.Controllers
{

    public class GuestController : ApiController
    {
        [Authorize(Roles = "RestaurantManager")]
        [Route("api/sendfriendrequest")]
        public IHttpActionResult SendFriendRequest(FriendRequest friendRequest)
        {
            return Ok(string.Format("Guest {0} sent request to guest {1}", friendRequest.SenderUsername, friendRequest.RecipientUsername));
        }
    }
}