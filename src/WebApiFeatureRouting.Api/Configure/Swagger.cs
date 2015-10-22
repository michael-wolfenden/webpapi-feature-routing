using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Swashbuckle.Application;
using WebApiFeatureRouting.Api.Features;

namespace WebApiFeatureRouting.Api.Configure
{
    public static class Swagger
    {
        public const string ApiName = "WebApi Feature Routing";

        public static void Configure(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.EnableSwagger(c => 
                { 
                    c.MultipleApiVersions(ResolveVersionByControllerName, AddVersions);
                    c.GroupActionsBy(GetGroupName);
                    c.UseFullTypeNameInSchemaIds();
                })
                .EnableSwaggerUi(c =>
                {
                    c.EnableDiscoveryUrlSelector();
                });
        }

        private static void AddVersions(VersionInfoBuilder versionInfoBuilder)
        {
            var versionsAvaliable = GetVersionsAvaliable();

            foreach (var version in versionsAvaliable)
            {
                versionInfoBuilder.Version(version, $"{ApiName} {version}");
            }
        }

        private static bool ResolveVersionByControllerName(ApiDescription apiDesc, string targetApiVersion)
        {
            return apiDesc.ActionDescriptor
                        .ControllerDescriptor
                        .ControllerType
                        .FullName
                        .ToLowerInvariant()
                        .Contains(targetApiVersion);
        }

        private static string[] GetVersionsAvaliable()
        {
            return typeof(Swagger)
                .Assembly
                .GetTypes()
                .Where(type => type.IsSubclassOf(typeof(ApiController)))
                .Select(type => GetVersionFromControllerType(type).ToLower())
                .Distinct()
                .OrderBy(version => version)
                .ToArray();
        }

        private static string GetGroupName(ApiDescription apiDescription)
        {
            var controllerType = apiDescription.ActionDescriptor
                .ControllerDescriptor
                .ControllerType;

            return GetGroupName(controllerType);
        }

        // WebApiFeatureRouting.Api.Features.V1.Customer.GetAll => ['v1', 'customer', 'getall']
        private static string[] GetRouteSegments(Type controllerType)
        {
            var featuresNamespace = typeof(IContainFeatures).Namespace;

            return controllerType.FullName
                .Replace(featuresNamespace + ".", "")
                .Split('.');
        }

        private static string GetVersionFromControllerType(Type controllerType)
        {
            return GetRouteSegments(controllerType)[0];
        }

        private static string GetGroupName(Type controllerType)
        {
            return GetRouteSegments(controllerType)[1];
        }
    }
}