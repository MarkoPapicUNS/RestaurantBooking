using Guest.Services.Exceptions;
using Guest.Services.RepositoryContracts;
using Restaurant.Services.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guest.Domain;
using Shared;

namespace Guest.Services
{
    public class ReservationService : IReservationService
    {
        private IGuestRepository _guestRepository;
        private IRestaurantRepository _restaurantRepository;
        private ILogger _logger;

        public ReservationService(IGuestRepository repository, IRestaurantRepository restaurantRepository, ILogger logger)
        {
            _guestRepository = repository;
            _restaurantRepository = restaurantRepository;
            _logger = logger;
        }

        public void MakeReservation(string username, string restaurantId, int tableNumber, DateTime time, double hours)
        {
            var guest = _guestRepository.Find(username);
			if (guest == null)
				throw new ReservationException(string.Format("User {0} doesn't exist.", username));
            if (time < DateTime.Now + TimeSpan.FromHours(2))
                throw new ReservationException("Reservations can only be made 2 hours before arrival.");
			if (guest.Reservations.Any(r => DoReservationsOverlap(r.Time, r.Hours, time, hours)))
				throw new ReservationException("Reservation overlaps with some of your reservations.");
			var restaurant = _restaurantRepository.Find(restaurantId);
            if (restaurant == null)
                throw new ReservationException(string.Format("Restaraunt with id {0} doesn't exist", restaurantId));
            if (guest.Reservations.Any(r => r.RestaurantId == restaurantId && r.Time == time))
                throw new ReservationException("Reservation already exists");

            guest.Reservations.Add(new Domain.GuestReservation
            {
                GuestUsername = username,
                RestaurantId = restaurantId,
				RestaurantName = restaurant.Name,
                TableNumber = tableNumber,
                DidShowUp = true,
                Time = time,
				Hours = hours
            });
            restaurant.Reservations.Add(new Restaurant.Domain.RestaurantReservation
            {
                GuestUsername = username,
                RestaurantId = restaurantId,
				GuestDisplayName = guest.DisplayFullName ? string.Format("{0} {1}", guest.FirstName, guest.LastName) : username,
                TableNumber = tableNumber,
                DidShowUp = true,
                Time = time,
				Hours = hours

			});
            _guestRepository.Commit();
            _restaurantRepository.Commit();

        }

        public void CancelReservation(string username, string restaurantId, int tableNumber, DateTime time)
        {
			throw new NotImplementedException();
		}

	    public void InviteFriend(string username, int reservationId, string friendUsername)
	    {
			var guest = _guestRepository.Find(username);
			if (guest == null)
				throw new ReservationException(string.Format("User {0} doesn't exist.", username));
		    if (
			    !(guest.RequestedFriendships.Any(
				    req => req.ResponderUsername == friendUsername && req.Status == FriendshipStatus.Active) ||
			    guest.ReceivedFriendships.Any(
				    rec => rec.RequesterUsername == friendUsername && rec.Status == FriendshipStatus.Active)))
				throw new ReservationException("You can only invite your friends to restaurant with you.");
			var reservation = guest.Reservations.FirstOrDefault(r => r.ReservationId == reservationId);
			if (reservation == null)
				throw new ReservationException("Reservation doesn't exist.");
			var friend = _guestRepository.Find(friendUsername);
			if (friend == null)
				throw new ReservationException(string.Format("User {0} doesn't exist.", friendUsername));
			friend.ReservationInvitations.Add(new ReservationInvitation
			{
				GuestReservationId = reservation.ReservationId,
				InvitedGuestUsername = friendUsername,
				/*RestaurantId = restaurantId,
				GuestDisplayName = guest.DisplayFullName ? string.Format("{0} {1}", guest.FirstName, guest.LastName) : username,
				InvitedGuestDisplayName = friend.DisplayFullName ? string.Format("{0} {1}", friend.FirstName, friend.LastName) : friendUsername,
				RestaurantName = reservation.RestaurantName,
				TableNumber = reservation.TableNumber,
				Time = time*/
			});
			_guestRepository.Commit();
		}

        public void CreateRatingsFromCompletedReservations()
        {
            Task.Run(() => _logger.Log(LogMessageType.Notification, "Started looking up for completed reservations and creating ratings"));

            try
            {
                var guests = _guestRepository.All();
                //var completeReservations = guests.SelectMany(g => g.Reservations).Where(r => r.DidShowUp && DateTime.Now >= r.Time + TimeSpan.FromHours(r.Hours));
                //var ratings = guests.SelectMany(g => g.Ratings);
                //var reservationsWithoutRating = completeReservations.Where(r => !ratings.Any(r2 => r2.GuestUsername == r.GuestUsername && r2.RestaurantId == r.RestaurantId));

                foreach (var guest in guests)
                {
                    if (guest.Ratings == null)
                        guest.Ratings = new List<GuestRating>();
                    if (guest.Visits == null)
                        guest.Visits = new List<Visit>();
                    var completeReservations = guest.Reservations.Where(r => r.DidShowUp && DateTime.Now >= r.Time + TimeSpan.FromHours(r.Hours));
                    var reservationsWithoutRating = completeReservations.Where(r => !guest.Ratings.Any(r2 => r2.GuestUsername == r.GuestUsername && r2.RestaurantId == r.RestaurantId));
                    foreach (var reservation in reservationsWithoutRating)
                    {
                        if (reservation.Invitations != null)
                        {
                            foreach (var invitation in reservation.Invitations)
                            {
                                if (invitation.InvitedGuest.Ratings == null)
                                    invitation.InvitedGuest.Ratings = new List<GuestRating>();
                                if (!invitation.InvitedGuest.Ratings.Any(r2 => r2.GuestUsername == reservation.GuestUsername && reservation.RestaurantId == reservation.RestaurantId))
                                {
                                    var invitedGuestRating = new GuestRating
                                    {
                                        GuestUsername = invitation.InvitedGuestUsername,
                                        RestaurantId = reservation.RestaurantId,
                                        RestaurantName = reservation.RestaurantName,
                                        Rating = 0,
                                        Rated = false
                                    };
                                    var guestVisit = new Visit
                                    {
                                        GuestUsername = invitation.InvitedGuestUsername,
                                        RestaurantId = reservation.RestaurantId,
                                        RestaurantName = reservation.RestaurantName,
                                        Time = reservation.Time
                                    };
                                    invitation.InvitedGuest.Ratings.Add(invitedGuestRating);
                                    invitation.InvitedGuest.Visits.Add(guestVisit);
                                }

                            }
                        }

                        var guestRating = new GuestRating
                        {
                            GuestUsername = reservation.GuestUsername,
                            RestaurantId = reservation.RestaurantId,
                            RestaurantName = reservation.RestaurantName,
                            Rating = 0,
                            Rated = false
                        };
                        var visit = new Visit
                        {
                            GuestUsername = reservation.GuestUsername,
                            RestaurantId = reservation.RestaurantId,
                            RestaurantName = reservation.RestaurantName,
                            Time = reservation.Time
                        };
                        guest.Ratings.Add(guestRating);
                        guest.Visits.Add(visit);
                    }
                }
                _guestRepository.Commit();
                Task.Run(() => _logger.Log(LogMessageType.Notification, "Finished looking up for completed reservations and creating ratings"));
            }
            catch (Exception e)
            {
                Task.Run(() => _logger.Log(LogMessageType.Error, e.Message));
            }
        }

        private bool DoReservationsOverlap(DateTime time1, double hours1, DateTime time2, double hours2)
	    {
		    if (time1 < time2 && time2 < time1 + TimeSpan.FromHours(hours1))
			    return true;
			if (time2 < time1 && time1 < time2 + TimeSpan.FromHours(hours2))
				return true;
		    return false;
	    }
    }
}
