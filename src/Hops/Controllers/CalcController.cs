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
        public IActionResult Color()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult Alcohol()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult Hydrometer()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult MaltPotential()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult MashWater()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult MashInfusion()
        {
            return View();
        }
    }
}
