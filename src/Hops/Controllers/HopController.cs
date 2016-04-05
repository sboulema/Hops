using Hops.Models;
using Hops.Repositories;
using Microsoft.AspNet.Mvc;

namespace Hops.Controllers
{
    [Route("[controller]")]
    public class HopController : Controller
    {
        private readonly IHopRepository hopRepository;

        public HopController(IHopRepository hopRepository)
        {
            this.hopRepository = hopRepository;
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View(hopRepository.GetPage(1));
        }

        [HttpGet("/{page}", Name = "Page")]
        public IActionResult Index(int page)
        {
            return View(hopRepository.GetPage(page));
        }

        [HttpGet("{id}", Name = "Detail")]
        public IActionResult Detail(long id)
        {
            var hop = hopRepository.Get(id);
            var substitutions = hopRepository.GetSubstitutions(id);
            var aliases = hopRepository.GetAliases(id);
            var aromas = hopRepository.GetAromas(id);

            return View(new HopModel { Hop = hop, Substitutions = substitutions, Aliases = aliases, Aromas = aromas });
        }
    }
}
