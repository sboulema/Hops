using Hops.Repositories;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace Hops.Controllers
{
    [Route("[controller]")]
    public class HopController : Controller
    {
        private readonly ISqliteRepository sqliteRepository;

        public HopController(ISqliteRepository sqliteRepository)
        {
            this.sqliteRepository = sqliteRepository;
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View(sqliteRepository.Search(string.Empty, 1));
        }

        [HttpGet]
        public IActionResult IndexHop()
        {
            return View("Index", sqliteRepository.Search(string.Empty, 1));
        }

        [HttpGet("page/{page}", Name = "Page")]
        public IActionResult Index(int page)
        {
            return View(sqliteRepository.Search(string.Empty, page));
        }

        [HttpGet("{id:long}", Name = "Detail")]
        public IActionResult Detail(long id)
        {
            return View(sqliteRepository.GetHopModel(id));
        }

        [HttpGet("{slug}", Name = "DetailBySlug")]
        public IActionResult DetailBySlug(string slug)
        {
            return View("Detail", sqliteRepository.GetHopModel(slug));
        }

        [HttpGet("[action]/{searchTerm}/{page:int?}")]
        public IActionResult Inventory(string searchTerm, int page = 1)
        {
            var results = sqliteRepository.Search(searchTerm.Split(',').Select(s => long.Parse(s)).ToList(), page);

            return View("List", results);
        }

    }
}
