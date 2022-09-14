using Microsoft.AspNetCore.Mvc;

namespace Hops.Controllers;

[Route("[controller]")]
public class RecipeController : Controller
{
    [Route("[action]")]
    public IActionResult New()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("{recipeId}")]
    public IActionResult Index(int recipeId)
    {
        return View("Detail");
    }

    [Route("[action]")]
    public IActionResult Import()
    {
        return View();
    }

    [Route("[action]")]
    public IActionResult Share(string recipe)
    {
        return View();
    }
}
