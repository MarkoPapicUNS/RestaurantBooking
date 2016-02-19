using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Dtos;
using Guest.Services;
using Guest.Services.Exceptions;

namespace ApplicationServices
{
    public class RatingAppService : IRatingAppService
    {
        private IRatingService _service;

        public RatingAppService(IRatingService service)
        {
            _service = service;
        }

        public ActionResultDto RateRestaurant(string username, string restaurantId, int rating, string comment)
        {
            ActionResultDto result = new ActionResultDto();
            try
            {
                _service.RateRestaurant(username, restaurantId, rating, comment);
                result.IsSuccess = true;
                result.Message = "Restaurant successfully rated";
            }
            catch (RatingException re)
            {
                result.IsSuccess = false;
                result.Message = re.Message;
            }
            return result;
        }
    }
}
