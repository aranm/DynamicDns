using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Dynamic.Dns.Contracts.Repository;
using Dynamic.Dns.Service.Repository;
using Dynamic.Dns.Service.Services;

namespace Dynamic.Dns.Web.Infrastructure.Windsor.Installers
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IAddressProvider>().ImplementedBy<AddressProvider>().LifestyleTransient());
        }
    }
}