using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Dynamic.Dns.Contracts.Repository;
using Dynamic.Dns.Service.TableEntities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dynamic.Dns.Service.Repository
{
    public class AddressProvider : IAddressProvider
    {
        private readonly string _storageConnectionString;

        public AddressProvider()
        {
            _storageConnectionString = ConfigurationManager.AppSettings["StorageConnectionString"];
        }

        public async Task<string> GetLatestAddress()
        {
            try
            {
                var storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
                var tableClient = storageAccount.CreateCloudTableClient();
                var table = tableClient.GetTableReference("IpAddresses");


                var topItemQuery = new TableQuery<IpAddressTable>()
                    .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "ipaddress"))
                    .Take(1);

                var tableQuerySegment = await table.ExecuteQuerySegmentedAsync(topItemQuery, new TableContinuationToken());

                if (tableQuerySegment.Results.Any())
                {
                    return tableQuerySegment.First()
                        .Ipv4Address;
                }

                return string.Empty;
            }
            catch (StorageException)
            {
                //TODO: Write Exceptions to Error Logger
                return string.Empty;
            }
            catch (Exception)
            {
                //TODO: Write Exceptions to Error Logger
                return string.Empty;
            }
        }
    }
}