using System.Web.Http;
using Owin;
using WebApiFeatureRouting.Api.Configure;

namespace WebApiFeatureRouting.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration httpConfiguration = new HttpConfiguration();

            WebApi.Configure(httpConfiguration);
            Swagger.Configure(httpConfiguration);

            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}