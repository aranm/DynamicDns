using System.Threading.Tasks;
using System.Web.Mvc;
using Dynamic.Dns.Contracts.Repository;
using Dynamic.Dns.Web.Models;

namespace Dynamic.Dns.Web.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        private readonly IAddressProvider _addressProvider;

        public HomeController(IAddressProvider addressProvider)
        {
            _addressProvider = addressProvider;
        }

        [Route]
        public async Task<ActionResult> Index()
        {
            var ipAddress = await _addressProvider.GetLatestAddress();

            var ipAddressModel = new IpAddressModel
            {
                IpAddress = ipAddress
            };

            return View(ipAddressModel);
        }
    }
}