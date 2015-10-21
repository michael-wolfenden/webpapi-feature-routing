using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace WebApiFeatureRouting.Api.Infrastructure.WebApi.Routing
{
    public class NamespaceHttpControllerSelector : IHttpControllerSelector
    {
        private readonly HttpConfiguration _configuration;
        private readonly Lazy<Dictionary<string, HttpControllerDescriptor>> _controllers;

        public NamespaceHttpControllerSelector(HttpConfiguration config)
        {
            _configuration = config;
            _controllers = new Lazy<Dictionary<string, HttpControllerDescriptor>>(InitializeControllerDictionary);
        }

        private Dictionary<string, HttpControllerDescriptor> InitializeControllerDictionary()
        {
            var dictionary = new Dictionary<string, HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);

            var assembliesResolver = _configuration.Services.GetAssembliesResolver();
            var controllersResolver = _configuration.Services.GetHttpControllerTypeResolver();
            var controllerTypes = controllersResolver.GetControllerTypes(assembliesResolver);

            foreach (Type controllerType in controllerTypes)
            {
                var key = GetKeyFromControllerType(controllerType);
                dictionary[key] = new HttpControllerDescriptor(_configuration, controllerType.Name, controllerType);
            }

            return dictionary;
        }

        public HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var routeData = request.GetRouteData();
            if (routeData == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var key = GetKeyFromRouteData(routeData);
            if (key == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            HttpControllerDescriptor controllerDescriptor;
            if (_controllers.Value.TryGetValue(key, out controllerDescriptor))
            {
                return controllerDescriptor;
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            return _controllers.Value;
        }

        private string GetKeyFromControllerType(Type controllerType)
        {
            return controllerType.FullName.ToLowerInvariant();
        }

        private string GetKeyFromRouteData(IHttpRouteData routeData)
        {
            var subRouteData = routeData.GetSubRoutes().FirstOrDefault();
            if (subRouteData == null) return null;

            var httpActionDescriptors = (HttpActionDescriptor[])subRouteData.Route.DataTokens["actions"];
            return httpActionDescriptors.First().ControllerDescriptor.ControllerType.FullName.ToLowerInvariant();
        }
    }
}