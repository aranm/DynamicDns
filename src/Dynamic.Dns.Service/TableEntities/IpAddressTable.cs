using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dynamic.Dns.Service.TableEntities
{
    public class IpAddressTable : TableEntity
    {
        public IpAddressTable() : base()
        {

        }

        public IpAddressTable(string ipv4Address) : base("ipaddress", (DateTime.MaxValue.Ticks - DateTime.UtcNow.Ticks).ToString("d19"))
        {
            Ipv4Address = ipv4Address;
        }

        public string Ipv4Address { get; set; }
    }
}
