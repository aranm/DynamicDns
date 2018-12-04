using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Dynamic.Dns.UserInterface.Infrastructure.Windsor.Factories;

namespace Dynamic.Dns.UserInterface.Infrastructure.Windsor.Installers
{
    public class UserInterfaceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<TypedFactoryFacility>();
            container.Register(Component.For<IMainWindowFactory>().AsFactory());
            container.Register(Component.For<MainWindow>().LifestyleTransient());
            container.Register(Component.For<DynamicDnsUserInterfaceViewModel>().LifestyleTransient());
        }
    }
}