using Shared;
using System.Collections.Generic;

namespace Restaurant.Domain
{
    public class Restaurant : IAggregateRoot
    {
        public string RestaurantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public double Rating { get; set; }
        public List<Meal> Menu { get; set; }
        public List<Table> Tables { get; set; }
        public List<RestaurantReservation> Reservations { get; set; }
        public List<RestaurantRating> Ratings { get; set; }
        public List<RestaurantManager> Managers { get; set; } 

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            var other = (Restaurant)obj;
            return RestaurantId == other.RestaurantId;
        }

        public override int GetHashCode()
        {
            return RestaurantId.GetHashCode();
        }
    }
}
