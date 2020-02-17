using DAL;
using System.Configuration;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;
using WebUI.Controllers;

namespace WebUI
{
    public static class UnityConfig
    {
        private static readonly string connString = ConfigurationManager.AppSettings["connectionString"];

        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IDataService, DataService>(new InjectionConstructor(connString));
            container.RegisterType<IController, DataServiceController>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}