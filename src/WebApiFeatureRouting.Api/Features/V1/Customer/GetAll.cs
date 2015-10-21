using System.Web.Http;

namespace WebApiFeatureRouting.Api.Features.V1.Customer
{
    public class GetAll : ApiController
    {
        [HttpGet, Route("")]
        public IHttpActionResult Handle()
        {
            return Ok(new
            {
                Version = "V1",
                Action = "GetAll"
            });
        }
    }
}