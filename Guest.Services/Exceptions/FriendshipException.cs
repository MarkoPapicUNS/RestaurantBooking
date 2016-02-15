using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guest.Services.Exceptions
{
    public class FriendshipException : Exception
    {
        public FriendshipException(string message)
			: base(message)
		{
		}

        public FriendshipException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
