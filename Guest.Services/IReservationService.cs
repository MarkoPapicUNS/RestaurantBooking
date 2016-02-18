using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guest.Services
{
    public interface IReservationService
    {
        void MakeReservation(string username, string restaurantId, int tableNumber, DateTime time, double hours);
        void CancelReservation(string username, string restaurantId, int tableNumber, DateTime time);
	    void InviteFriend(string username, int reservationId, string friendUsername);
        void CreateRatingsFromCompletedReservations();
    }
}
