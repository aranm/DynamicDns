using System.Windows;
using System.Windows.Input;
using Dynamic.Dns.UserInterface.Infrastructure.Windsor.Factories;
using Dynamic.Dns.UserInterface.Infrastructure.Wpf;

namespace Dynamic.Dns.UserInterface
{
    public class DynamicDnsUserInterfaceViewModel
    {
        private readonly IMainWindowFactory _mainWindowFactory;
        private MainWindow _mainWindow;

        public DynamicDnsUserInterfaceViewModel(IMainWindowFactory mainWindowFactory)
        {
            _mainWindowFactory = mainWindowFactory;
        }

        /// <summary>
        /// Shows a window, if none is already open.
        /// </summary>
        public ICommand ShowWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => Application.Current.MainWindow == null,
                    CommandAction = () =>
                    {
                        _mainWindow = _mainWindowFactory.Create();
                        Application.Current.MainWindow = _mainWindow;
                        _mainWindow.Show();
                    }
                };
            }
        }

        /// <summary>
        /// Hides the main window. This command is only enabled if a window is open.
        /// </summary>
        public ICommand HideWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => _mainWindow != null,
                    CommandAction = () =>
                    {
                        if (_mainWindow != null)
                        {
                            _mainWindow.Close();
                            Application.Current.MainWindow = null;
                            _mainWindowFactory.Release(_mainWindow);
                        }
                    }
                };
            }
        }

        /// <summary>
        /// Shuts down the application.
        /// </summary>
        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand {
                    CommandAction = () =>
                    {
                        if (_mainWindow != null)
                        {
                            _mainWindow.Close();
                            _mainWindowFactory.Release(_mainWindow);
                            _mainWindow = null;
                        }
                        Application.Current.Shutdown();
                    }
                };
            }
        }
    }
}