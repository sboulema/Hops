using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hops.Controllers;

[Route("[controller]")]
public class ShortUrlController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public ShortUrlController(IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string longUrl)
    {
        if (string.IsNullOrEmpty(longUrl))
        {
            return BadRequest();
        }

        var signature = _configuration["YourlsApiKey"];

        var client = _httpClientFactory.CreateClient();
        var result = await client.GetFromJsonAsync<dynamic>($"http://yourls.sboulema.nl/yourls-api.php?signature={signature}&action=shorturl&format=json&url={longUrl}");

        return Json(result?.shorturl);
    }
}
