using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guest.Services
{
    public interface IRatingService
    {
        void CreateRatingsFromCompletedReservations();
        void RateRestaurant(string username, string restaurantId, int rating, string comment);
    }
}
