using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain
{
    public class Table
    {
        public string RestaurantId { get; set; }
        public int Number { get; set; }
        public int NumberOfSeats { get; set; }
        public TablePosition Position { get; set; }

        //for Entity Framework
        public virtual Restaurant Restaurant { get; set; }
    }
}
