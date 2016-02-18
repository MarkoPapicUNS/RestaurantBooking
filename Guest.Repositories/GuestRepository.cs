using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Guest.Domain;
using Guest.Services;
using Guest.Services.RepositoryContracts;

namespace Guest.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        private RestaurantBookingContext _context;

        public GuestRepository()
        {
            _context = new RestaurantBookingContext();
        }

        public IQueryable<Domain.Guest> All()
        {
            return
                _context.Guests.Include(g => g.ReceivedFriendships)
                    .Include(g2 => g2.RequestedFriendships)
                    .Include(g => g.Reservations)
                    .Include(g => g.SentReservationInvitations)
                    .Include(g => g.ReservationInvitations);
        }

        public Domain.Guest Find(string id) //I return single quest as IQueryable because of later includes
        {
	        return
		        _context.Guests.Include(g => g.ReceivedFriendships)
			        .Include(g => g.RequestedFriendships)
			        .Include(g => g.Reservations)
			        .Include(g => g.Reservations)
			        .Include(g => g.SentReservationInvitations)
			        .Include(g => g.ReservationInvitations)
                    .Include(g => g.Ratings)
                    .Include(g => g.Visits)
			        .FirstOrDefault(g => g.Username == id);
        }

        public void Insert(Domain.Guest item)
        {
            _context.Guests.Add(item);
        }

        public void Delete(Domain.Guest item)
        {
            _context.Guests.Remove(item);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
