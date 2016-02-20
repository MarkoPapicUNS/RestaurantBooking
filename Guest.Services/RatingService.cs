using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guest.Domain;
using Guest.Services.Exceptions;
using Guest.Services.RepositoryContracts;
using Restaurant.Domain;
using Restaurant.Services.RepositoryContracts;
using Shared;

namespace Guest.Services
{
    public class RatingService : IRatingService
    {
        private IGuestRepository _guestRepository;
        private IRestaurantRepository _restaurantRepository;
        private ILogger _logger;

        public RatingService(IGuestRepository repository, IRestaurantRepository restaurantRepository, ILogger logger)
        {
            _guestRepository = repository;
            _restaurantRepository = restaurantRepository;
            _logger = logger;
        }

        public void CreateRatingsFromCompletedReservations()
        {
            try
            {
                //Task.Run(() => _logger.Log(LogMessageType.Notification, "Started looking up for completed reservations and creating ratings"));
                _logger.Log(LogMessageType.Notification, "Started looking up for completed reservations and creating ratings");

                var guests = _guestRepository.All();
                //var completeReservations = guests.SelectMany(g => g.Reservations).Where(r => r.DidShowUp && DateTime.Now >= r.Time + TimeSpan.FromHours(r.Hours));
                //var ratings = guests.SelectMany(g => g.Ratings);
                //var reservationsWithoutRating = completeReservations.Where(r => !ratings.Any(r2 => r2.GuestUsername == r.GuestUsername && r2.RestaurantId == r.RestaurantId));
                var added = false;
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
                                        Rated = false,
                                        Comment = ""
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
                //Task.Run(() => _logger.Log(LogMessageType.Notification, "Finished looking up for completed reservations and creating ratings"));
                _logger.Log(LogMessageType.Notification, "Finished looking up for completed reservations and creating ratings");

            }
            catch (Exception e)
            {
                //Task.Run(() => _logger.Log(LogMessageType.Error, e.Message));
                _logger.Log(LogMessageType.Error, e.Message);
            }
        }

        public void RateRestaurant(string username, string restaurantId, int rating, string comment)
        {
            var guest = _guestRepository.Find(username);
            var guestRating = guest.Ratings.FirstOrDefault(r => r.RestaurantId == restaurantId);
            if (guestRating == null)
                throw new RatingException("Rating not found");
            var restaurant = _restaurantRepository.Find(restaurantId);
            if (restaurant.Ratings == null)
                restaurant.Ratings = new List<RestaurantRating>();
            var restaurantRating = restaurant.Ratings.FirstOrDefault(r => r.GuestUsername == username);
            if (restaurantRating == null)
                restaurant.Ratings.Add(new RestaurantRating
                {
                    GuestUsername = username,
                    RestaurantId = restaurantId,
                    GuestDisplayName = guest.DisplayFullName ? string.Format("{0} {1}", guest.FirstName, guest.LastName) : guest.Username,
                    Rated = true,
                    Grade = rating,
                    Comment = comment
                });
            else
            {
                restaurantRating.Grade = rating;
                restaurantRating.Rated = true;
                restaurantRating.Comment = comment;
            }
            guestRating.Rating = rating;
            guestRating.Rated = true;
            guestRating.Comment = comment;
            _guestRepository.Commit();
            _restaurantRepository.Commit();
        }
    }
}
