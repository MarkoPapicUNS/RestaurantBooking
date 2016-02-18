using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Guest.Domain
{
    public class GuestReservation
    {
		public int ReservationId { get; set; }
        public string GuestUsername { get; set; }
        public string RestaurantId { get; set; }
		public string RestaurantName { get; set; }
        public int TableNumber { get; set; }
        public DateTime Time { get; set; }
        public double Hours { get; set; }
		public List<ReservationInvitation> Invitations { get; set; }

        //for Entity Framework
		[JsonIgnore]
        public virtual Guest Guest { get; set; }
    }
}
