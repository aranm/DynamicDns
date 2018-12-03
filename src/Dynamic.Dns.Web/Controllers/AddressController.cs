using System.Web.Http;
using Dynamic.Dns.Web.Infrastructure;

namespace Dynamic.Dns.Web.Controllers
{
    public class AddressController : ApiController
    {
        [Route("api/v1/address")]
        public string Get()
        {
            var ipAddress = this.Request.GetClientIpAddress();
            return ipAddress;
        }
    }
}
