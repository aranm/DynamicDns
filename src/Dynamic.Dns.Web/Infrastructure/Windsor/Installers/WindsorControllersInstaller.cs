using System.Web.Http.Controllers;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Dynamic.Dns.Web.Controllers;

namespace Dynamic.Dns.Web.Infrastructure.Windsor.Installers
{
    public class WindsorControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(FindControllers().LifestylePerWebRequest());
            container.Register(FindApiControllers().LifestylePerWebRequest());

            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        private static BasedOnDescriptor FindApiControllers()
        {
            return Classes.FromThisAssembly()
                .BasedOn<IHttpController>()
                .If(Component.IsInSameNamespaceAs<HomeController>(true))
                .If(t => t.Name.EndsWith("Controller"));
        }

        private static BasedOnDescriptor FindControllers()
        {
            return Classes.FromThisAssembly()
                .BasedOn<IController>()
                .If(Component.IsInSameNamespaceAs<HomeController>(true))
                .If(t => t.Name.EndsWith("Controller"));
        }
    }
}