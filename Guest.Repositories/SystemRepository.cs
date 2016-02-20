using System;
using System.Collections.Generic;
using System.Domain;
using System.Linq;
using System.Services.RepositoryContracts;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Shared;

namespace Guest.Repositories
{
    public class SystemRepository : ISystemRepository
    {
        private RestaurantBookingContext _context;

        public SystemRepository()
        {
            _context = new RestaurantBookingContext();
        }

        public SystemManager Find(string Id)
        {
            return _context.SystemManagers.FirstOrDefault(rm => rm.Username == Id);
        }

        public IQueryable<SystemManager> All()
        {
            return _context.SystemManagers;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Delete(SystemManager item)
        {
            _context.SystemManagers.Remove(item);
        }

        public void Insert(SystemManager item)
        {
            _context.SystemManagers.Add(item);
        }
    }
}
