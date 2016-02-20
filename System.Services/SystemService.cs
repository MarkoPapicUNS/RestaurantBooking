using System;
using System.Collections.Generic;
using System.Domain;
using System.Linq;
using System.Services.Exceptions;
using System.Services.RepositoryContracts;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain;
using Restaurant.Services.RepositoryContracts;
using Shared;

namespace System.Services
{
    public class SystemService : ISystemService
    {
        private ISystemRepository _systemRepository;

        public SystemService(ISystemRepository systemRepository, IRestaurantRepository restaurantRepository)
        {
            _systemRepository = systemRepository;
        }

        public void AddSystemManager(string username)
        {
            var systemManager = _systemRepository.Find(username);
            if (systemManager != null)
                throw new RestaurantSystemException(string.Format("System manager {0} is already registered"));
            _systemRepository.Insert(new SystemManager
            {
                Username = username,
                Address = new Address()
            });
            _systemRepository.Commit();
        }

        public void RemoveSystemManager(string username)
        {
            var systemManager = _systemRepository.Find(username);
            if (systemManager != null)
                _systemRepository.Delete(systemManager);
            _systemRepository.Commit();
        }

        public SystemManager GetSystemManager(string username)
        {
            var systemManager = _systemRepository.Find(username);
            if (systemManager == null)
                throw new RestaurantSystemException(string.Format("System manager {0} not registered"));
            return systemManager;
        }

        public IEnumerable<SystemManager> GetSystemManagers()
        {
            var systemManagers = _systemRepository.All();
            return systemManagers;
        }
    }
}
