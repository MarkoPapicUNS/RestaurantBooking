using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using ApplicationServices;
using ApplicationServices.Adapters;
using Guest.Repositories;
using Guest.Services;
using Guest.Services.RepositoryContracts;
using InversionOfControl;
using Microsoft.Practices.Unity;

namespace AppBuilder
{
    public class InversionOfControlManager
    {
        public static IDependencyResolver GetDependencyResolver()
        {
            var container = new UnityContainer();
            container.RegisterType<IFriendshipAppService, FriendshipAppService>();
            container.RegisterType<IFriendshipService, FriendshipService>();
            container.RegisterType<IGuestAppService, GuestAppService>();
            container.RegisterType<IGuestService, GuestService>();
            container.RegisterType<IGuestRepository, GuestRepository>();
            container.RegisterType<IFriendshipRepository, FriendshipRepository>();
            container.RegisterType<IFriendshipAdapter, FriendshipAdapter>();
            return new UnityResolver(container);
        }
    }
}
