using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Dynamic.Dns.Contracts.Repository;
using Dynamic.Dns.Contracts.Services;
using Dynamic.Dns.Service.Repository;
using Dynamic.Dns.Service.Services;

namespace Dynamic.Dns.UserInterface.Infrastructure.Windsor.Installers
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IAddressService>()
                .ImplementedBy<AddressService>()
                .LifestyleSingleton());

            container.Register(Component.For<IAddressWriter>()
                .ImplementedBy<AddressWriter>()
                .LifestyleTransient());

            container.Register(Component.For<IAddressProvider>()
                .ImplementedBy<AddressProvider>()
                .LifestyleTransient());
        }
    }
}
