using System;
using System.Configuration;
using System.Threading.Tasks;
using Dynamic.Dns.Contracts.Repository;
using Dynamic.Dns.Service.TableEntities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dynamic.Dns.Service.Repository
{
    public class AddressWriter : IAddressWriter
    {
        private readonly string _storageConnectionString;

        public AddressWriter()
        {
            _storageConnectionString = ConfigurationManager.AppSettings["StorageConnectionString"];
        }

        public async Task<bool> StoreIpAddress(string ipAddress)
        {
            try
            {
                var storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
                var tableClient = storageAccount.CreateCloudTableClient();

                var table = tableClient.GetTableReference("IpAddresses");
                await table.CreateIfNotExistsAsync();

                var data = new IpAddressTable(ipAddress);

                // Create the TableOperation object that inserts the customer entity.
                var insertOperation = TableOperation.Insert(data);

                // Execute the insert operation.
                await table.ExecuteAsync(insertOperation);
                return true;
            }
            catch (StorageException)
            {
                //TODO: Write Exceptions to Error Logger
                return false;
            }
            catch (Exception)
            {
                //TODO: Write Exceptions to Error Logger
                return false;
            }
        }
    }
}
