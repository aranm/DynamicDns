using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dynamic.Dns.Contracts.Services;

namespace Dynamic.Dns.UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IAddressService _addressService;

        public MainWindow(IAddressService addressService)
        {
            _addressService = addressService;
            _addressService.PropertyChanged += _addressService_PropertyChanged;

            InitializeComponent();
        }

        private void _addressService_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                // Set property or change UI compomponents.     
                ipAddressLabel.Content = _addressService.LatestIpAddress;
            });
        }

        private async void ButtonRefreshIpAddress(object sender, RoutedEventArgs e)
        {
            await _addressService.RefreshIpAddress();
            var ipAddress = await _addressService.GetLatestIpAddress();
            this.ipAddressLabel.Content = ipAddress;
            await _addressService.UpdateIpAddress();
        }
    }
}
