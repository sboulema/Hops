using Hops.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace Hops.Controllers
{
    [Route("[controller]")]
    public class ShortUrlController : Controller
    {
        private readonly IConfigurationService _configurationService;

        public ShortUrlController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        [HttpGet]
        public IActionResult Get(string longUrl)
        {
            if (string.IsNullOrEmpty(longUrl)) return BadRequest();

            var signature = _configurationService.Get("YourlsApiKey");

            var response = new HttpClient().GetAsync($"http://yourls.sboulema.nl/yourls-api.php?signature={signature}&action=shorturl&format=json&url={longUrl}").Result;

            dynamic json = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

            return Json(json.shorturl);
        }
    }
}
