using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Dtos;
using Guest.Services;
using Guest.Services.Exceptions;

namespace ApplicationServices
{
    public class ReservationAppService : IReservationAppService
    {
        private IReservationService _reservationService;

        public ReservationAppService(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public ActionResultDto MakeReservation(string username, string restaurantId, int tableNumber, DateTime time, double hours)
        {
            ActionResultDto result = new ActionResultDto();
            try
            {
                _reservationService.MakeReservation(username, restaurantId, tableNumber, time, hours);
                result.IsSuccess = true;
                result.Message = "Table succesfully reserved!";
            }
            catch(ReservationException re)
            {
                result.IsSuccess = false;
                result.Message = re.Message;
            }
            catch(Exception e)
            {
                result.IsSuccess = false;
                result.Message = "Unable to process request.";
            }
            return result;
        }

        public ActionResultDto CancelReservation(string username, string restaurantId, int tableNumber, DateTime time)
        {
            throw new NotImplementedException();
        }

	    public ActionResultDto InviteFriend(string username, int reservationId, string friendUsername)
		{
		    ActionResultDto result = new ActionResultDto();
		    try
		    {
			    _reservationService.InviteFriend(username, reservationId, friendUsername);
			    result.IsSuccess = true;
			    result.Message = string.Format("{0} invited to join you.", friendUsername);
		    }
			catch (ReservationException re)
			{
				result.IsSuccess = false;
				result.Message = re.Message;
			}
			catch (Exception e)
			{
				result.IsSuccess = false;
				result.Message = "Unable to process request.";
			}
			return result;
		}
    }
}
