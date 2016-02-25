using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantBooking.API.Models
{
    public class MealDto
    {
        public string RestaurantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}