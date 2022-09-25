using Microsoft.AspNetCore.Mvc;

namespace Hops.Controllers;

[Route("[controller]")]
public class RecipeController : Controller
{
    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet("[action]")]
    public IActionResult New() => View();

    [HttpGet("{recipeId:long}")]
    public IActionResult Index(long recipeId)
    {
        ViewData["RecipeId"] = recipeId;
        return View("Detail");
    }

    [HttpGet("[action]")]
    public IActionResult Import() => View();

    [HttpGet("[action]")]
    public IActionResult Share(string recipe)
    {
        return View();
    }
}
