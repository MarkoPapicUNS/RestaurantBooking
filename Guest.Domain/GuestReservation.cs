using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guest.Domain
{
    public class GuestReservation
    {
        public string GuestUsername { get; set; }
        public string RestaurantId { get; set; }
        public int TableNumber { get; set; }
        public DateTime Time { get; set; }
        public double Hours { get; set; }

        //for Entity Framework
        public virtual Guest Guest { get; set; }
    }
}
