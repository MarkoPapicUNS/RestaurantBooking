using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantBooking.API.Models
{
    public class ReservationDto
    {
        public string RestaurantId { get; set; }
        public int TableNumber { get; set; }
        public DateTime Time { get; set; }
		public double Hours { get; set; }
    }
}