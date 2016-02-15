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
        protected RestaurantBookingContext _context;

        public GuestRepository()
        {
            _context = new RestaurantBookingContext();
        }

        public IQueryable<Domain.Guest> All()
        {
            return _context.Guests.Include(g => g.ReceivedFriendships).Include(g2 => g2.RequestedFriendships);
        }

        public Domain.Guest Find(string id)
        {
            return _context.Guests.SingleOrDefault(g => g.Username == id);
        }

        public Friendship GetFriendship(string requesterUsername, string responderUsername)
        {
            return
                _context.Frendships.SingleOrDefault(
                    f => f.RequesterUsername == requesterUsername && f.ResponderUsername == responderUsername);
        }

        public IQueryable<Friendship> GetFriendships()
        {
            return _context.Frendships;
        }

        public void DeleteFriendship(Friendship item)
        {
            _context.Frendships.Remove(item);
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
