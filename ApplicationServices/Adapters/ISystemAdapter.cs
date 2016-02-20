using System;
using System.Collections.Generic;
using System.Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Dtos;

namespace ApplicationServices.Adapters
{
    public interface ISystemAdapter
    {
        SystemManagerDto AdaptSystemManager(SystemManager systemManager);
    }
}
