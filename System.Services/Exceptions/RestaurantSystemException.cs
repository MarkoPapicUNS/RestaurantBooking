using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Services.Exceptions
{
    public class RestaurantSystemException : Exception
    {
        public RestaurantSystemException(string message)
            : base(message)
        {
        }

        public RestaurantSystemException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
