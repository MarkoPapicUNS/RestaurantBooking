using System;
using System.Collections.Generic;
using System.Linq;
using System.Services.Exceptions;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Adapters;
using ApplicationServices.Dtos;
using Restaurant.Domain;
using Restaurant.Services;
using Shared;

namespace ApplicationServices
{
    public class RestaurantAppService : IRestaurantAppService
    {
        private IRestaurantService _service;
        private ILogger _logger;
        private IRestaurantAdapter _adapter;

        public RestaurantAppService(IRestaurantService systemService, ILogger logger, IRestaurantAdapter adapter)
        {
            _service = systemService;
            _logger = logger;
            _adapter = adapter;
        }

        public RestaurantDto GetRestaurant(string username, string restaurantId)
        {
            Restaurant.Domain.Restaurant restaurant;
            try
            {
                restaurant = _service.GetRestaurant(restaurantId);
            }
            catch (Exception e)
            {
                return null;
            }
            return _adapter.AdaptRestaurant(username, restaurant);
        }

        public IEnumerable<RestaurantDto> GetRestaurants(string username)
        {
            return _service.GetRestaurants().Select(r => _adapter.AdaptRestaurant(username, r));
        }

        public ActionResultDto AddRestaurant(string restaurantId)
        {
            ActionResultDto result = new ActionResultDto();
            _logger.Log(LogMessageType.Notification, string.Format("Adding restaurant {0}", restaurantId));
            try
            {
                _service.AddRestaurant(restaurantId);
                result.IsSuccess = true;
                result.Message = string.Format("Restaurant {0} successfully added!", restaurantId);
                _logger.Log(LogMessageType.Notification, string.Format("Restaurant {0} successfully added!", restaurantId));
            }
            catch (RestaurantException se)
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

        public ActionResultDto RemoveRestaurant(string restaurantId)
        {
            ActionResultDto result = new ActionResultDto();
            _logger.Log(LogMessageType.Notification, string.Format("Removing restaurant {0}", restaurantId));
            try
            {
                _service.RemoveRestaurant(restaurantId);
                result.IsSuccess = true;
                result.Message = string.Format("Restaurant {0} successfully removed!", restaurantId);
                _logger.Log(LogMessageType.Notification, string.Format("Restaurant {0} successfully removed!", restaurantId));
            }
            catch (RestaurantException se)
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

        public RestaurantManager GetRestaurantManager(string restaurantManagerUsername)
        {
            throw new NotImplementedException();
        }

        public ActionResultDto AddRestaurantManager(string restaurantManagerUsername, string restaurantId)
        {
            ActionResultDto result = new ActionResultDto();
            _logger.Log(LogMessageType.Notification, string.Format("Adding restaurant manager {0} to restaurant {1}", restaurantManagerUsername, restaurantId));
            try
            {
                _service.AddRestaurantManager(restaurantManagerUsername, restaurantId);
                result.IsSuccess = true;
                result.Message = string.Format("Restaurant manager {0} added successfully!", restaurantManagerUsername);
                _logger.Log(LogMessageType.Notification, string.Format("Restaurant manager {0} added successfully!", restaurantManagerUsername));
            }
            catch (RestaurantException se)
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

        public ActionResultDto RemoveRestaurantManager(string restaurantManagerUsername)
        {
            ActionResultDto result = new ActionResultDto();
            _logger.Log(LogMessageType.Notification, string.Format("Removing restaurant manager {0}", restaurantManagerUsername));
            try
            {
                _service.RemoveRestaurantManager(restaurantManagerUsername);
                result.IsSuccess = true;
                result.Message = string.Format("Restaurant manager {0} successfully reemoved!", restaurantManagerUsername);
                _logger.Log(LogMessageType.Notification, string.Format("Restaurant manager {0} successfully removed!", restaurantManagerUsername));
            }
            catch (RestaurantException se)
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
