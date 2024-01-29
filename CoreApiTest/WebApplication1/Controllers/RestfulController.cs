using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Net6Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestfulController : ControllerBase
    {
        [HttpGet]
        public object GetValue() {
            return new
            {
                Success = true,
                Mag = "Ok"
            };
        }

        [HttpPost]
        public object PostValue()
        {
            return new
            {
                Success = true,
                Mag = "Ok"
            };
        }

        [HttpPut]
        public object PutValue()
        {
            return new
            {
                Success = true,
                Mag = "Ok"
            };
        }

        [HttpDelete]
        public object DeleteValue()
        {
            return new
            {
                Success = true,
                Mag = "Ok"
            };
        }


    }
}
