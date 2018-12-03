using System.Web.Http;

namespace Dynamic.Dns.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;
        }
    }
}
