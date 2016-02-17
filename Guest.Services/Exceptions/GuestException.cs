using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guest.Services.Exceptions
{
	public class GuestException : Exception
	{
		public GuestException(string message)
			: base(message)
		{
		}

		public GuestException(string message, Exception exception)
            : base(message, exception)
        {
		}
	}
}
