using System.Diagnostics;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using WebApiFeatureRouting.Api.Features;

namespace WebApiFeatureRouting.Api.Infrastructure.WebApi.Routing
{
    public class PrefixRouteWithNamespaceProvider : DefaultDirectRouteProvider
    {
        // WebApiFeatureRouting.Api.Features.v1.Customer.GetAll => [RoutePrefix("api/v1/customer/getall")]
        protected override string GetRoutePrefix(HttpControllerDescriptor controllerDescriptor)
        {
            var featuresNamespace = typeof(IContainFeatures).Namespace;

            var route = "api" + controllerDescriptor
                .ControllerType
                .FullName
                .Replace(featuresNamespace, "")
                .Replace(".", "/")
                .ToLowerInvariant();

            Debug.Print("Registering route: '{0}'", route);

            return route;
        }
    }
}