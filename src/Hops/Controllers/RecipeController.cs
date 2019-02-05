using Microsoft.AspNetCore.Mvc;

namespace Hops.Controllers
{
    [Route("[controller]")]
    public class RecipeController : Controller
    {
        [Route("[action]")]
        public IActionResult New()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult List()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult Open()
        {
            return View();
        }
    }
}
