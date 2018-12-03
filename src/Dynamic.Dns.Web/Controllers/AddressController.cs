using System.Web.Http;

namespace Dynamic.Dns.Web.Controllers
{
    public class AddressController : ApiController
    {
        [Route("api/v1/address")]
        public string Get()
        {
            var ipAddress = "127.0.0.1";
            return ipAddress;
        }
    }
}
