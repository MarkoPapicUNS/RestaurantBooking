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
        public static IDependencyResolver GetDependencyResolver()
        {
            var container = new UnityContainer();
            container.RegisterType<IFriendshipAppService, FriendshipAppService>();
            container.RegisterType<IFriendshipService, FriendshipService>();
            container.RegisterType<IGuestAdapter, GuestAdapter>();
            container.RegisterType<IGuestAppService, GuestAppService>();
            container.RegisterType<IGuestService, GuestService>();
            container.RegisterType<IReservationAppService, ReservationAppService>();
            container.RegisterType<IReservationService, ReservationService>();
            container.RegisterType<IGuestRepository, GuestRepository>();
            container.RegisterType<IFriendshipRepository, FriendshipRepository>();
            container.RegisterType<IRestaurantRepository, RestaurantRepository>();
            container.RegisterType<ILogger, DbLoger>();
            return new UnityResolver(container);
        }

        public static T ResolveReservationService<T>() //TODO: Reuse container from constructor
        {
            var container = new UnityContainer();
            container.RegisterType<IFriendshipAppService, FriendshipAppService>();
            container.RegisterType<IFriendshipService, FriendshipService>();
            container.RegisterType<IGuestAdapter, GuestAdapter>();
            container.RegisterType<IGuestAppService, GuestAppService>();
            container.RegisterType<IGuestService, GuestService>();
            container.RegisterType<IReservationAppService, ReservationAppService>();
            container.RegisterType<IReservationService, ReservationService>();
            container.RegisterType<IGuestRepository, GuestRepository>();
            container.RegisterType<IFriendshipRepository, FriendshipRepository>();
            container.RegisterType<IRestaurantRepository, RestaurantRepository>();
            container.RegisterType<ILogger, DbLoger>();
            return container.Resolve<T>();
        }
    }
}
