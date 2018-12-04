using System;
using System.ComponentModel;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dynamic.Dns.Contracts.Repository;
using Dynamic.Dns.Contracts.Services;
using Dynamic.Dns.Service.Annotations;

namespace Dynamic.Dns.Service.Services
{
    //TODO: Throw exceptions and log them
    public class AddressService : IAddressService, IDisposable
    {
        private readonly IAddressProvider _addressProvider;
        private readonly IAddressWriter _addressWriter;
        private string _latestIpAddress;
        private readonly string _baseUri;
        private readonly string _addressPath;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CancellationToken _cancellationToken;
        
        public AddressService(IAddressProvider addressProvider, IAddressWriter addressWriter)
        {
            _addressProvider = addressProvider;
            _addressWriter = addressWriter;
            _latestIpAddress = String.Empty;
            _baseUri = ConfigurationManager.AppSettings["BaseAddressUri"];
            _addressPath = ConfigurationManager.AppSettings["AddressPath"];

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            var task = new Task(PeriodicUpdate, _cancellationToken);

            task.Start();
        }

        public string LatestIpAddress
        {
            get => _latestIpAddress;
            private set
            {
                if (string.Compare(_latestIpAddress, value, StringComparison.InvariantCultureIgnoreCase) != 0)
                {
                    _latestIpAddress = value;
                    OnPropertyChanged(nameof(LatestIpAddress));
                }
            }
        }

        public async Task<string> GetLatestIpAddress()
        {
            if (string.IsNullOrEmpty(LatestIpAddress) == false)
            {
                return LatestIpAddress;
            }

            await RefreshIpAddress();

            return LatestIpAddress;
        }

        public async Task<bool> RefreshIpAddress()
        {
            var ipAddressFromRestApi = await GetIpAddressFromRestApi();

            if (string.IsNullOrEmpty(ipAddressFromRestApi))
            {
                return false;
            }

            var latestIpAddress = await _addressProvider.GetLatestAddress();

            if (string.Compare(ipAddressFromRestApi, latestIpAddress, StringComparison.InvariantCultureIgnoreCase) != 0)
            {
                LatestIpAddress = ipAddressFromRestApi;
            }

            return true;
        }

        public async Task<bool> UpdateIpAddress()
        {
            if (string.IsNullOrEmpty(LatestIpAddress))
            {
                await RefreshIpAddress();
            }

            return await _addressWriter.StoreIpAddress(LatestIpAddress);
        }

        public async Task<string> GetIpAddressFromRestApi()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseUri)
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync(_addressPath, _cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var returnValue = await response.Content.ReadAsStringAsync();
                returnValue = returnValue.Replace("\"", string.Empty);
                return returnValue;
            }

            return String.Empty;
        }

        private async void PeriodicUpdate()
        {
            if (_cancellationToken.IsCancellationRequested == false)
            {
                while (_cancellationToken.IsCancellationRequested == false)
                {
                    var ipAddressFromRestApi = await GetIpAddressFromRestApi();
                    if (string.IsNullOrEmpty(ipAddressFromRestApi) == false)
                    {
                        await _addressWriter.StoreIpAddress(ipAddressFromRestApi);
                        LatestIpAddress = ipAddressFromRestApi;
                    }

                    //wait for an 15 minutes (15minutes * 60 seconds * 1000 milliseconds
                    _cancellationToken.WaitHandle.WaitOne(15 * 60 * 1000);
                }
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
