using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantBooking.API.Models
{
    public class FriendRequest
    {
        public string SenderUsername { get; set; }
        public string RecipientUsername { get; set; }
    }
}