using System.Web.Mvc;
using Dynamic.Dns.Web.Models;

namespace Dynamic.Dns.Web.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        [Route]
        public ActionResult Index()
        {
            var ipAddressModel = new IpAddressModel
            {
                IpAddress = "127.0.0.1"
            };

            return View(ipAddressModel);
        }
    }
}