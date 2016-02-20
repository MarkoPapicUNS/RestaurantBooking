using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Dtos;

namespace ApplicationServices
{
    public interface ISystemAppService
    {
        SystemManagerDto GetSystemManager(string username);
        IEnumerable<SystemManagerDto> GetSystemManagers();
        ActionResultDto AddSystemManager(string username);
        ActionResultDto RemoveSystemManager(string username);
    }
}
