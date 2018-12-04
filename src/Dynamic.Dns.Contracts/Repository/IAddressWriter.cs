using System.Threading.Tasks;

namespace Dynamic.Dns.Contracts.Repository
{
    public interface IAddressWriter
    {
        Task<bool> StoreIpAddress(string ipAddress);
    }
}