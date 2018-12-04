using System.Threading.Tasks;

namespace Dynamic.Dns.Contracts.Repository
{
    public interface IAddressProvider
    {
        Task<string> GetLatestAddress();
    }
}