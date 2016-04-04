using Microsoft.AspNet.Mvc;

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
    }
}
