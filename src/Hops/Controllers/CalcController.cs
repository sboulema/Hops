using Microsoft.AspNetCore.Mvc;

namespace Hops.Controllers
{
    [Route("[controller]")]
    public class CalcController : Controller
    {
        [Route("[action]")]
        public IActionResult Priming()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult IBU()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult Labels()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult PitchRate()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult BeerXml()
        {
            return View();
        }
    }
}
