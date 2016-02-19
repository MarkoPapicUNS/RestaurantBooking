using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Restaurant.Domain
{
    public class RestaurantRating
    {
        public string RestaurantId { get; set; }
        public string GuestUsername { get; set; }
        public string GuestDisplayName { get; set; }
        public int Grade { get; set; }
        public bool Rated { get; set; }
        public string Comment { get; set; }

        //for Entity Framework
        [JsonIgnore]
        public virtual Restaurant Restaurant { get; set; }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            var other = (RestaurantRating)obj;
            return RestaurantId == other.RestaurantId && GuestUsername == other.GuestUsername;
        }

        public override int GetHashCode()
        {
            return (RestaurantId + GuestUsername).GetHashCode();
        }
    }
}
