using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIContagem.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ValuesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ValuesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<string> GetState()
        {
            // define a 404 page  
            var url = $"https://www.c-sharpcorner.com/mytestpagefor404";

            var client = _httpClientFactory.CreateClient("Test");
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
