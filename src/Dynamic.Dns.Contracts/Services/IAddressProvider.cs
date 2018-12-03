using System.Threading.Tasks;

namespace Dynamic.Dns.Contracts.Services
{
    public interface IAddressProvider
    {
        Task<string> GetLatestAddress();
    }
}