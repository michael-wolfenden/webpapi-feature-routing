using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using WebApiFeatureRouting.Api.Infrastructure.WebApi;

namespace WebApiFeatureRouting.Api.Configure.WebApiConfiguration
{
    public static class ContentNegotiation
    {
        public static void Configure(HttpConfiguration httpConfiguration)
        {
            DisableContentNegotiationAndOnlySupportJson(httpConfiguration);
        }

        private static void DisableContentNegotiationAndOnlySupportJson(HttpConfiguration httpConfiguration)
        {
            var jsonFormatter = httpConfiguration.Formatters.OfType<JsonMediaTypeFormatter>().First();
            httpConfiguration.Services.Replace(typeof(IContentNegotiator), new JsonOnlyContentNegotiator(jsonFormatter));
        }
    }
}