using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices
{
    public enum GuestRelation
    {
        Me = 0,
        Friend,
		RequestReceived,
		RequestSent,
        Stranger
    }
}
