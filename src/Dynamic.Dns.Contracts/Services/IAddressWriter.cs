using System.Threading.Tasks;

namespace Dynamic.Dns.Contracts.Services
{
    public interface IAddressWriter
    {
        Task<bool> StoreIpAddress(string ipAddress);
    }
}