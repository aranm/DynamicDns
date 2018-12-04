using System.ComponentModel;
using System.Threading.Tasks;

namespace Dynamic.Dns.Contracts.Services
{
    public interface IAddressService : INotifyPropertyChanged
    {
        Task<string> GetLatestIpAddress();
        Task<bool> RefreshIpAddress();
        Task<bool> UpdateIpAddress();
        string LatestIpAddress { get; }
    }
}