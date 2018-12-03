using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Microsoft.Owin;

namespace Dynamic.Dns.Web.Infrastructure
{
    public static class HttpRequestMessageExtensions
    {
        public static string GetClientIpAddress(this HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_OwinContext"))
            {
                return ((OwinContext)request.Properties["MS_OwinContext"]).Request.RemoteIpAddress;
            }
            return null;
        }
    }
}