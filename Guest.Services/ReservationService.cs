using Guest.Services.Exceptions;
using Guest.Services.RepositoryContracts;
using Restaurant.Services.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guest.Services
{
    public class ReservationService : IReservationService
    {
        private IGuestRepository _guestRepository;
        private IRestaurantRepository _restaurantRepository;

        public ReservationService(IGuestRepository repository, IRestaurantRepository restaurantRepository)
        {
            _guestRepository = repository;
            _restaurantRepository = restaurantRepository;
        }

        public void MakeReservation(string username, string restaurantId, int tableNumber, DateTime time)
        {
            var guest = _guestRepository.Find(username);
            var restaurant = _restaurantRepository.Find(restaurantId);
            if (guest == null)
                throw new ReservationException(string.Format("User {0} doesn't exist.", username));
            if (restaurant == null)
                throw new ReservationException(string.Format("Restaraunt with id {0} doesn't exist", restaurantId));
            if (guest.Reservations.Any(r => r.RestaurantId == restaurantId && r.Time == time))
                throw new ReservationException("Reservation already exists");

            guest.Reservations.Add(new Domain.GuestReservation
            {
                GuestUsername = username,
                RestaurantId = restaurantId,
                TableNumber = tableNumber,
                Time = time
            });
            restaurant.Reservations.Add(new Restaurant.Domain.RestaurantReservation
            {
                GuestUsername = username,
                RestaurantId = restaurantId,
                TableNumber = tableNumber,
                Time = time
            });
            _guestRepository.Commit();
            _restaurantRepository.Commit();

        }

        public void CancelReservation(string username, string restaurantId, int tableNumber, DateTime time)
        {
            throw new NotImplementedException();
        }
    }
}
