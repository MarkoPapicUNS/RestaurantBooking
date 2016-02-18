using ApplicationServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices
{
    public interface IReservationAppService
    {
        ActionResultDto MakeReservation(string username, string restaurantId, int tableNumber, DateTime time, double hours);
        ActionResultDto CancelReservation(string username, string restaurantId, int tableNumber, DateTime time);
		ActionResultDto InviteFriend(string username, int reservationId, string friendUsername);
	}
}
