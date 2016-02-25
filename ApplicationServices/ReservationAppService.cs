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
			    result.Message = string.Format("{0} successfully invited.", friendUsername);
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

	    public ActionResultDto AcceptInvitation(string username, int reservationId)
	    {
			ActionResultDto result;
			try
			{
				_reservationService.AcceptReservationInvitation(username, reservationId);
				result = new ActionResultDto
				{
					IsSuccess = true,
					Message = string.Format("Reservation invitation from {0} accepted!", username)
				};
				//Task.Run(() => _logger.Log(LogMessageType.Notification, string.Format("{0} removed successfully from {1}'s friends", friendUsername, username)));
				//_logger.Log(LogMessageType.Notification,
					//string.Format("Friend request from {0} to {1} accepted", senderUsername, recipientUsername));

			}
			catch (Exception e)
			{
				//Task.Run(() => _logger.Log(LogMessageType.Notification, e.Message));
				//_logger.Log(LogMessageType.Notification, e.Message);
				result = new ActionResultDto
				{
					IsSuccess = false,
					Message = "Unable to perform this request."
				};
			}
			return result;
		}

	    public ActionResultDto RejectInvitation(string username, int reservationId)
	    {
			ActionResultDto result;
			try
			{
				_reservationService.RejectReservationInvitation(username, reservationId);
				result = new ActionResultDto
				{
					IsSuccess = true,
					Message = string.Format("Reservation invitation from {0} declined!", username)
				};
				//Task.Run(() => _logger.Log(LogMessageType.Notification, string.Format("{0} removed successfully from {1}'s friends", friendUsername, username)));
				//_logger.Log(LogMessageType.Notification,
				//string.Format("Friend request from {0} to {1} accepted", senderUsername, recipientUsername));

			}
			catch (Exception e)
			{
				//Task.Run(() => _logger.Log(LogMessageType.Notification, e.Message));
				//_logger.Log(LogMessageType.Notification, e.Message);
				result = new ActionResultDto
				{
					IsSuccess = false,
					Message = "Unable to perform this request."
				};
			}
			return result;
		}
    }
}
