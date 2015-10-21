using System.Web.Http;
using WebApiFeatureRouting.Api.Configure.WebApiConfiguration;

namespace WebApiFeatureRouting.Api.Configure
{
    public static class WebApi
    {
        public static void Configure(HttpConfiguration httpConfiguration)
        {
            ContentNegotiation.Configure(httpConfiguration);
            Formatters.Configure(httpConfiguration);
            Routing.Configure(httpConfiguration);
        }
    }
}