using Hops.Repositories;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace Hops.Controllers
{
    [Route("[controller]")]
    public class MaltController : Controller
    {
        private readonly IMaltRepository maltRepository;

        public MaltController(IMaltRepository maltRepository)
        {
            this.maltRepository = maltRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(maltRepository.Search(string.Empty, 1));
        }

        [HttpGet("page/{page}")]
        public IActionResult Index(int page)
        {
            return View(maltRepository.Search(string.Empty, page));
        }

        [HttpGet("{id}")]
        public IActionResult Detail(long id)
        {
            return View(maltRepository.Get(id));
        }

        [HttpGet("inventory/{searchTerm}/{page:int?}")]
        public IActionResult Inventory(string searchTerm, int page = 1)
        {
            var results = maltRepository.Search(searchTerm.Split(',').Select(s => long.Parse(s)).ToList(), page);

            return View("List", results);
        }
    }
}
