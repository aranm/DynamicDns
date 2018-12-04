using System.IO;
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

        [Route("remote-desktop")]
        public async Task<ActionResult> DownLoadRdpFile()
        {
            var ipAddress = await _addressProvider.GetLatestAddress();
            ipAddress = ipAddress.Replace("\"", string.Empty);

            using (var memoryStream = new MemoryStream())
            {
                TextWriter tw = new StreamWriter(memoryStream);

                tw.WriteLine($"full address:s:{ipAddress}");
                tw.WriteLine("prompt for credentials:i:1");
                tw.WriteLine("administrative session:i:1");
                tw.Flush();
                tw.Close();

                return File(memoryStream.GetBuffer(), "application/x-rdp", "myconnection.rdp");
            }
        }
    }
}