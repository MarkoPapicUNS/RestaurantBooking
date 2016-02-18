using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guest.Domain
{
    public class Visit
    {
        public string RestaurantId { get; set; }
        public string GuestUsername { get; set; }
        public string RestaurantName { get; set; }
        public DateTime Time { get; set; }

        //for Entity Framework
        [JsonIgnore]
        public virtual Guest Guest { get; set; }
    }
}
