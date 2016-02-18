using DataAccess;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class DbLoger : ILogger
    {
        private RestaurantBookingContext _context;

        public DbLoger()
        {
            _context = new RestaurantBookingContext();
        }

        public void Log(LogMessageType messageType, string message)
        {
            var logMessage = new Log
            {
                MessageType = messageType,
                Message = message,
                Time = DateTime.Now
            };
            _context.Logs.Add(logMessage);
            _context.SaveChanges();
        }
    }
}
