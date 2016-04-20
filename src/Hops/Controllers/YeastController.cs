using Hops.Repositories;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace Hops.Controllers
{
    [Route("[controller]")]
    public class YeastController : Controller
    {
        private readonly IYeastRepository yeastRepository;

        public YeastController(IYeastRepository yeastRepository)
        {
            this.yeastRepository = yeastRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(yeastRepository.Search(string.Empty, 1));
        }

        [HttpGet("page/{page}")]
        public IActionResult Index(int page)
        {
            return View(yeastRepository.Search(string.Empty, page));
        }

        [HttpGet("{id}")]
        public IActionResult Detail(long id)
        {
            return View(yeastRepository.Get(id));
        }

        [HttpGet("inventory/{searchTerm}/{page:int?}")]
        public IActionResult Inventory(string searchTerm, int page = 1)
        {
            var results = yeastRepository.Search(searchTerm.Split(',').Select(s => long.Parse(s)).ToList(), page);

            return View("List", results);
        }
    }
}
