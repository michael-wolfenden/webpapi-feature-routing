using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace WebApiFeatureRouting.Api.Configure.WebApiConfiguration
{
    public static class Formatters
    {
        public static void Configure(HttpConfiguration httpConfiguration)
        {
            CamelCaseJson(httpConfiguration);
        }

        private static void CamelCaseJson(HttpConfiguration httpConfiguration)
        {
            var jsonFormatter = httpConfiguration.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}