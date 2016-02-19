using System.Web.Http.Dependencies;
using ApplicationServices;
using ApplicationServices.Adapters;
using Guest.Repositories;
using Guest.Services;
using Guest.Services.RepositoryContracts;
using InversionOfControl;
using Microsoft.Practices.Unity;
using Restaurant.Repositories;
using Restaurant.Services.RepositoryContracts;
using Shared;
using Logger;

namespace AppBuilder
{
	public class InversionOfControlManager
	{
	    private static UnityContainer _container;
        public static UnityContainer Container {
            get
            {
                if (_container == null)
                {
                    _container = new UnityContainer();
                    RegisterTypes();
                }
                return _container;
            }
            set { _container = value; }
        }

	    private static void RegisterTypes()
	    {
            _container.RegisterType<IFriendshipAppService, FriendshipAppService>();
            _container.RegisterType<IFriendshipService, FriendshipService>();
            _container.RegisterType<IGuestAdapter, GuestAdapter>();
            _container.RegisterType<IGuestAppService, GuestAppService>();
            _container.RegisterType<IGuestService, GuestService>();
            _container.RegisterType<IReservationAppService, ReservationAppService>();
            _container.RegisterType<IReservationService, ReservationService>();
            _container.RegisterType<IRatingAppService, RatingAppService>();
            _container.RegisterType<IRatingService, RatingService>();
            _container.RegisterType<IGuestRepository, GuestRepository>();
            _container.RegisterType<IFriendshipRepository, FriendshipRepository>();
            _container.RegisterType<IRestaurantRepository, RestaurantRepository>();
            _container.RegisterType<ILogger, DbLoger>();
	    }

        public static IDependencyResolver GetDependencyResolver()
        {
            return new UnityResolver(Container);
        }

        public static T Resolve<T>() //TODO: Reuse container from constructor
        {
            return Container.Resolve<T>();
        }
    }
}
