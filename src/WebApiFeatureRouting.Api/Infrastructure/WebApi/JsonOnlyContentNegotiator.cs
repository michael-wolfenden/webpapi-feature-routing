using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Swashbuckle.Swagger;

namespace WebApiFeatureRouting.Api.Infrastructure.WebApi
{
    // REF: http://www.strathweb.com/2013/06/supporting-only-json-in-asp-net-web-api-the-right-way/
    public class JsonOnlyContentNegotiator : IContentNegotiator
    {
        private readonly JsonMediaTypeFormatter _jsonFormatter;

        public JsonOnlyContentNegotiator(JsonMediaTypeFormatter formatter)
        {
            _jsonFormatter = formatter;
        }

        public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
        {
            // HACK: Squashbuckle will pass a custom formatter that tells json.net to not output nulls as nulls will break swagger ui,
            // so we should respect that formatter and not use our own .. but only when serializing SwaggerDocument 
            var jsonMediaTypeFormatter = type == typeof (SwaggerDocument)
                ? formatters.OfType<JsonMediaTypeFormatter>().First()
                : _jsonFormatter;

            return new ContentNegotiationResult(jsonMediaTypeFormatter, new MediaTypeHeaderValue("application/json"));
        }
    }
}