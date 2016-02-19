using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guest.Services.Exceptions
{
    public class RatingException : Exception
    {
        public RatingException(string message)
            : base(message)
        {
        }

        public RatingException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
