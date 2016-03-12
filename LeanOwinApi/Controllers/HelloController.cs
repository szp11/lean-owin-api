using System.Threading.Tasks;
using System.Web.Http;

namespace LeanOwinApi.Controllers
{
    [RoutePrefix("hello")]
    public class HelloController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult HelloWorld()
        {
            var response = GetHelloWorld();
            return Ok(response);
        }

        [Route("async")]
        [HttpGet]
        public async Task<IHttpActionResult> HelloWorldAync()
        {
            var response = await GetHelloWorldAsync();
            return Ok(response);
        }

        private Task<string> GetHelloWorldAsync()
        {
            return Task.Run(() => GetHelloWorld());
        }

        private string GetHelloWorld()
        {
            return "Hello world";
        }
    }
}
