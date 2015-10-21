using System.Web.Http;
using System.Web.Http.Dispatcher;
using WebApiFeatureRouting.Api.Infrastructure.WebApi.Routing;

namespace WebApiFeatureRouting.Api.Configure.WebApiConfiguration
{
    public static class Routing
    {
        public static void Configure(HttpConfiguration httpConfiguration)
        {
            PrefixRoutesWithVersionBasedOnNamespace(httpConfiguration);
            AllowControllersToNotHaveControllerSuffix(httpConfiguration);
            AllowControllersToHaveTheSameNameButExistInDifferentNamespaces(httpConfiguration);
        }

        private static void PrefixRoutesWithVersionBasedOnNamespace(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.MapHttpAttributeRoutes(new PrefixRouteWithNamespaceProvider());
        }

        private static void AllowControllersToNotHaveControllerSuffix(HttpConfiguration httpConfiguration)
        {
           httpConfiguration.Services.Replace(typeof(IHttpControllerTypeResolver), new NoControllerSuffixHttpControllerTypeResolver());
        }

        private static void AllowControllersToHaveTheSameNameButExistInDifferentNamespaces(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(httpConfiguration));
        }

    }
}