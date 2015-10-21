using System.Web.Http;

namespace WebApiFeatureRouting.Api.Features.V1.Customer
{
    public class Get : ApiController
    {
        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Handle(int id)
        {
            return Ok(new
            {
                Version = "V1",
                Action = "Get",
                Id = id
            });
        }
    }
}