using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Guest.Domain;
using Guest.Services.RepositoryContracts;

namespace Guest.Repositories
{
    public class FriendshipRepository : IFriendshipRepository
    {
        private RestaurantBookingContext _context;

        public FriendshipRepository()
        {
            _context = new RestaurantBookingContext();
        }

        public IQueryable<Friendship> All()
        {
            return _context.Frendships;
        }

        public Friendship Find(string requesterUsername, string responderUsername)
        {
            return
                _context.Frendships.SingleOrDefault(
                    f => f.RequesterUsername == requesterUsername && f.ResponderUsername == responderUsername);
        }

        public void Insert(Friendship item)
        {
            _context.Frendships.Add(item);
        }

        public void Delete(Friendship item)
        {
            _context.Frendships.Remove(item);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
