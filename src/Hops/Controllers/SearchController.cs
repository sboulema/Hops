using Hops.Repositories;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Hops.Controllers
{
    [Route("[controller]")]
    public class SearchController : Controller
    {
        private ISqliteRepository sqliteRepository;

        public SearchController(ISqliteRepository sqliteRepository)
        {
            this.sqliteRepository = sqliteRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{searchTerm}/{page:int?}")]
        public IActionResult Results(string searchTerm, int page = 1)
        {
            var results = sqliteRepository.Search(searchTerm, page);

            if (results.List.Count == 0)
            {
                return View("NoResults", sqliteRepository.GetRandomHop());
            }

            if (results.List.Count == 1)
            {
                return Redirect($"/hop/{results.List.First().Hop.Slug()}");
            }

            return View(results);
        }

        [HttpGet("inventory/{page:int?}")]
        public IActionResult Inventory(string searchTerm, int page = 1)
        {
            return View(page);
        }

        [HttpGet("aroma/{profile:int}/{page:int?}")]
        public IActionResult Results(int profile, int page = 1)
        {
            var results = sqliteRepository.Search(profile, page);

            return View(results);
        }

        [HttpGet("autocomplete/{searchTerm}")]
        public List<string> AutoComplete(string searchTerm)
        {
            return sqliteRepository.Autocomplete(searchTerm);
        }

        [HttpGet("freetext/{searchterm}/{page:int?}")]
        public IActionResult FreeTextResults(string searchterm, int page = 1)
        {
            var results = sqliteRepository.FreeTextSearch(searchterm, page);

            return View("Results", results);
        }
    }
}
