using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain
{
    public class Meal
    {
        public string RestaurantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        //for Entity Framework
        public virtual Restaurant Restaurant { get; set; }
    }
}
