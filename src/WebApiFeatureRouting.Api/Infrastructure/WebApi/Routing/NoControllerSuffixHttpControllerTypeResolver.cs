using System;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace WebApiFeatureRouting.Api.Infrastructure.WebApi.Routing
{
    // REF: http://www.strathweb.com/2013/02/but-i-dont-want-to-call-web-api-controllers-controller/
    public class NoControllerSuffixHttpControllerTypeResolver : DefaultHttpControllerTypeResolver
    {
        public NoControllerSuffixHttpControllerTypeResolver() : base(IsHttpEndpoint)
        {
            var controllerSuffix = typeof(DefaultHttpControllerSelector).GetField("ControllerSuffix", BindingFlags.Static | BindingFlags.Public);
            controllerSuffix?.SetValue(null, string.Empty);
        }

        internal static bool IsHttpEndpoint(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return type.IsClass &&
                   type.IsVisible &&
                   !type.IsAbstract &&
                   typeof(IHttpController).IsAssignableFrom(type);
        }
    }
}