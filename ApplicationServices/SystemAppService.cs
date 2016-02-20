using System;
using System.Collections.Generic;
using System.Domain;
using System.Linq;
using System.Services;
using System.Services.Exceptions;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Adapters;
using ApplicationServices.Dtos;
using Shared;

namespace ApplicationServices
{
    public class SystemAppService : ISystemAppService
    {
        private ISystemService _systemService;
        private ILogger _logger;
        private ISystemAdapter _adapter;

        public SystemAppService(ISystemService systemService, ILogger logger, ISystemAdapter adapter)
        {
            _systemService = systemService;
            _logger = logger;
            _adapter = adapter;
        }

        public SystemManagerDto GetSystemManager(string username)
        {
            SystemManager systemManager;
            _logger.Log(LogMessageType.Notification, string.Format("Getting system manager {0}", username));
            try
            {
                systemManager = _systemService.GetSystemManager(username);
                _logger.Log(LogMessageType.Notification,
                    string.Format("System manager {0} successfully retrieved.", username));
                return _adapter.AdaptSystemManager(systemManager);
            }
            catch (Exception e)
            {
                _logger.Log(LogMessageType.Error, e.Message);
                return null;
            }
        }

        public IEnumerable<SystemManagerDto> GetSystemManagers()
        {
            IEnumerable<SystemManager> systemManagers;
            _logger.Log(LogMessageType.Notification, "Getting all system managers");
            try
            {
                systemManagers = _systemService.GetSystemManagers();
                _logger.Log(LogMessageType.Notification, "System managers successfully retrieved.");
                return systemManagers.Select(sm => _adapter.AdaptSystemManager(sm));
            }
            catch (Exception e)
            {
                _logger.Log(LogMessageType.Error, e.Message);
                return null;
            }
        }

        public ActionResultDto AddSystemManager(string username)
        {
            ActionResultDto result = new ActionResultDto();
            _logger.Log(LogMessageType.Notification, string.Format("Adding system manager {0}", username));
            try
            {
                _systemService.AddSystemManager(username);
                result.IsSuccess = true;
                result.Message = string.Format("System manager {0} successfully registered!", username);
                _logger.Log(LogMessageType.Notification, string.Format("System manager {0} successfully registered!", username));
            }
            catch (RestaurantSystemException se)
            {
                _logger.Log(LogMessageType.Error, se.Message);
                result.IsSuccess = false;
                result.Message = se.Message;
            }
            catch (Exception e)
            {
                _logger.Log(LogMessageType.Error, e.Message);
                result.IsSuccess = false;
                result.Message = "Unable to process request";
            }
            return result;
        }

        public ActionResultDto RemoveSystemManager(string username)
        {
            ActionResultDto result = new ActionResultDto();
            _logger.Log(LogMessageType.Notification, string.Format("Removing system manager {0}", username));
            try
            {
                _systemService.RemoveSystemManager(username);
                result.IsSuccess = true;
                result.Message = string.Format("System manager {0} successfully removed!", username);
                _logger.Log(LogMessageType.Notification, string.Format("System manager {0} successfully removed!", username));
            }
            catch (RestaurantSystemException se)
            {
                _logger.Log(LogMessageType.Error, se.Message);
                result.IsSuccess = false;
                result.Message = se.Message;
            }
            catch (Exception e)
            {
                _logger.Log(LogMessageType.Error, e.Message);
                result.IsSuccess = false;
                result.Message = "Unable to process request";
            }
            return result;
        }
    }
}
