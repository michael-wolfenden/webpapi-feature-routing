using System.Web.Http;

namespace WebApiFeatureRouting.Api.Features.V2.Customer
{
    public class GetAll : ApiController
    {
        [HttpGet, Route("")]
        public IHttpActionResult Handle()
        {
            return Ok(new
            {
                Version = "V2",
                Action = "GetAll"
            });
        }
    }
}