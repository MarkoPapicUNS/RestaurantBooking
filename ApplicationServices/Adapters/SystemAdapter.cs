using System;
using System.Collections.Generic;
using System.Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Dtos;

namespace ApplicationServices.Adapters
{
    public class SystemAdapter : ISystemAdapter
    {
        public SystemManagerDto AdaptSystemManager(SystemManager systemManager)
        {
            return new SystemManagerDto
            {
                Username = systemManager.Username,
                FirstName = systemManager.FirstName,
                LastName = systemManager.LastName,
                Address = systemManager.Address,
                Gender = systemManager.Gender,
                Picture = systemManager.Picture
            };
        }
    }
}
