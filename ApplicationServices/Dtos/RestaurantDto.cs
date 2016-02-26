using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain;

namespace ApplicationServices.Dtos
{
    public class RestaurantDto
    {
        public string RestaurantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public double YourRating { get; set; }
        public List<Meal> Menu { get; set; }
        public List<Table> Tables { get; set; }
        public List<int> ReservedTables { get; set; } 
        public List<RestaurantRating> Ratings { get; set; }
        public List<RestaurantReservation> Reservations { get; set; }
        public List<RestaurantManager> Managers { get; set; }
    }
}
