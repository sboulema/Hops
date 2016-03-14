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
        public IActionResult List()
        {
            return View(hopRepository.GetAll());
        }

        [HttpGet("{id}", Name = "Detail")]
        public IActionResult Detail(long id)
        {
            var hop = hopRepository.Get(id);
            var substitutions = hopRepository.GetSubstitutions(id);

            return View(new DetailModel { Hop = hop, Substitutions = substitutions });
        }
    }
}
