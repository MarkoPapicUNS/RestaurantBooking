using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Dtos
{
    public class StrangerGuestDto : IGuestDto
    {
        public GuestRelation Relation { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Picture { get; set; }
    }
}
