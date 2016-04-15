using Hops.Repositories;
using Microsoft.AspNet.Mvc;

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

        [HttpGet("/{page}", Name = "Page")]
        public IActionResult Index(int page)
        {
            return View(sqliteRepository.Search(string.Empty, page));
        }

        [HttpGet("{id}", Name = "Detail")]
        public IActionResult Detail(long id)
        {
            return View(sqliteRepository.GetHopModel(id));
        }
    }
}
