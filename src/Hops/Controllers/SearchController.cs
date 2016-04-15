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
                return Redirect($"/Hop/{results.List.First().Hop.Id}");
            }

            return View(results);
        }

        [HttpGet("partial/inventory/{searchTerm}/{page:int?}")]
        public IActionResult PartialInventory(string searchTerm, int page = 1)
        {
            var results = sqliteRepository.Search(searchTerm.Split(',').Select(s => long.Parse(s)).ToList(), page);

            return View("List", results);
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
    }
}
