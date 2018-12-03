using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Dynamic.Dns.Contracts.Services;
using Dynamic.Dns.Service.Services;
using Dynamic.Dns.UserInterface.Infrastructure.Windsor.Injection;

namespace Dynamic.Dns.UserInterface.Infrastructure.Windsor.Installers
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.ComponentModelBuilder.AddContributor(new InjectionPropertyComponentModelHelper());

            container.Register(Component.For<IAddressProvider>()
                .ImplementedBy<AddressProvider>()
                .LifestyleTransient());
            container.Register(Component.For<IAddressWriter>()
                .ImplementedBy<AddressWriter>()
                .LifestyleTransient());
        }
    }
}
