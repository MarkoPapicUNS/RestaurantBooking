using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Dtos;

namespace ApplicationServices
{
    public interface IRatingAppService
    {
        ActionResultDto RateRestaurant(string username, string restaurantId, int rating, string comment);
    }
}
