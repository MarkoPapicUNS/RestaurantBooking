using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantBooking.API.Models
{
	public class ReservationInvitationDto
	{
		public int ReservationId { get; set; }
		public string InvitedGuestUsername { get; set; }
	}
}