using DataAccess;
using Restaurant.Services.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return _context.Restaurants.Include("Reservations");
        }

        public Restaurant.Domain.Restaurant Find(string id)
        {
            return _context.Restaurants.Include("Reservations").FirstOrDefault(r => r.RestaurantId == id);
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
