using Hops.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Hops.Controllers
{
    [Route("[controller]")]
    public class HopController : Controller
    {
        private readonly ISqliteRepository _sqliteRepository;

        public HopController(ISqliteRepository sqliteRepository)
        {
            _sqliteRepository = sqliteRepository;
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View(_sqliteRepository.Search(string.Empty, 1));
        }

        [HttpGet]
        public IActionResult IndexHop()
        {
            return View("Index", _sqliteRepository.Search(string.Empty, 1));
        }

        [HttpGet("page/{page}", Name = "Page")]
        public IActionResult Index(int page)
        {
            return View("~/Views/Hop/Index.cshtml", _sqliteRepository.Search(string.Empty, page));
        }

        [HttpGet("{id:long}", Name = "Detail")]
        public IActionResult Detail(long id)
        {
            return View(_sqliteRepository.GetHopModel(id));
        }

        [HttpGet("{slug}", Name = "DetailBySlug")]
        public IActionResult DetailBySlug(string slug)
        {
            return View("Detail", _sqliteRepository.GetHopModel(slug));
        }

        [HttpGet("[action]/{searchTerm}/{page:int?}")]
        public IActionResult Inventory(string searchTerm, int page = 1)
        {
            var results = _sqliteRepository.Search(searchTerm.Split(',').Select(s => long.Parse(s)).ToList(), page);

            return View("~/Views/Hop/List.cshtml", results);
        }

        [HttpGet("[action]/{page:int?}")]
        public IActionResult Top(string searchTerm, int page = 1)
        {
            var results = _sqliteRepository.TopSubstitutors();

            return View("Index", results);
        }
    }
}
