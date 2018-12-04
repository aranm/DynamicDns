using System.Windows;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Dynamic.Dns.Contracts.Services;
using Hardcodet.Wpf.TaskbarNotification;

namespace Dynamic.Dns.UserInterface
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private WindsorContainer _windsorContainer;
        private TaskbarIcon _notifyIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _windsorContainer = new WindsorContainer();
            _windsorContainer.Install(FromAssembly.This());

            //resolve the address service as it is a singleton and it will update the latest IP addresss
            _windsorContainer.Resolve<IAddressService>();

            //create the notifyicon (it's a resource declared in NotifyIconResources.xaml
            _notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            if (_notifyIcon != null)
            {
                _notifyIcon.DataContext = _windsorContainer.Resolve<DynamicDnsUserInterfaceViewModel>();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            //the icon would clean up automatically, but this is cleaner
            _notifyIcon.Dispose();
            _windsorContainer.Dispose();

            base.OnExit(e);
        }
    }
}
