using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Dtos
{
    public interface IGuestDto
    {
        GuestRelation Relation { get; set; }
        string Username { get; set; }
        string DisplayName { get; set; }
        string Picture { get; set; }
    }
}
