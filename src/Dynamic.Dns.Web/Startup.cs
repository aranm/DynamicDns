using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Dynamic.Dns.Web.Infrastructure.Windsor;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Dynamic.Dns.Web.Startup))]
namespace Dynamic.Dns.Web
{
    public class Startup
    {
        private readonly HttpConfiguration _httpConfiguration;
        private IWindsorContainer _container;

        public Startup()
        {
            _httpConfiguration = new HttpConfiguration();

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            WebApiConfig.Register(_httpConfiguration);

            BootstrapContainer();
        }

        private void BootstrapContainer()
        {
            _container = new WindsorContainer()
                .AddFacility<TypedFactoryFacility>();

            _container.Install(FromAssembly.This());
            var dependencyResolver = new WindsorDependencyResolver(_container);
            _httpConfiguration.DependencyResolver = dependencyResolver;
        }

        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(_httpConfiguration);
        }
    }
}
