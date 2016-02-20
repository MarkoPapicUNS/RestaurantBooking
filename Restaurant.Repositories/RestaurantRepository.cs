using DataAccess;
using Restaurant.Services.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain;

namespace Restaurant.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private RestaurantBookingContext _context;

        public RestaurantRepository()
        {
            _context = new RestaurantBookingContext();
        }

        public IQueryable<Domain.Restaurant> All()
        {
            return _context.Restaurants.Include(r => r.Reservations).Include(r => r.Ratings).Include(r => r.Tables).Include(r => r.Menu).Include(r => r.Managers);
        }

        public Restaurant.Domain.Restaurant Find(string id)
        {
            return _context.Restaurants.Include(r => r.Reservations).Include(r => r.Ratings).Include(r => r.Tables).Include(r => r.Menu).Include(r => r.Managers).FirstOrDefault(r => r.RestaurantId == id);
        }

        public void RemoveManager(RestaurantManager manager)
        {
            _context.RestaurantManagers.Remove(manager);
        }

        public void Insert(Restaurant.Domain.Restaurant item)
        {
            _context.Restaurants.Add(item);
        }

        public void Delete(Restaurant.Domain.Restaurant item)
        {
            _context.Restaurants.Remove(item);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
