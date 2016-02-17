using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.RepositoryContracts
{
    public interface IRestaurantRepository : IRepository<Domain.Restaurant>
    {
        Domain.Restaurant Find(string id);
    }
}
