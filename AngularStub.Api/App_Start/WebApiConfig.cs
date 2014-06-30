using System.Web.Http;
using AngularStub.Data;
using Microsoft.Practices.Unity;

namespace AngularStub.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // IoC
            var container = new UnityContainer();
            container.RegisterType<IProductRepository, ProductRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional}
                );
        }
    }
}