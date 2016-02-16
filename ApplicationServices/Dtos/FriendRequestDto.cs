using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Dtos
{
    public class FriendRequestDto
    {
        public string RequesterUsername { get; set; }
        public string ResponderUsername { get; set; }
    }
}
