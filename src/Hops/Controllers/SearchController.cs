using Hops.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Hops.Mappers;

namespace Hops.Controllers
{
    [Route("[controller]")]
    public class SearchController : Controller
    {
        private readonly ISqliteRepository _sqliteRepository;

        public SearchController(ISqliteRepository sqliteRepository)
        {
            _sqliteRepository = sqliteRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{searchTerm}/{page:int?}")]
        public IActionResult Results(string searchTerm, int page = 1)
        {
            var results = _sqliteRepository.Search(SlugMapper.SlugToString(searchTerm), page);

            switch (results.List.Count)
            {
                case 0:
                    return View("NoResults", _sqliteRepository.GetRandomHop());
                case 1:
                    return Redirect($"/hop/{results.List.First().Hop.Slug()}");
                default:
                    return View(results);
            }   
        }

        [HttpGet("inventory/{page:int?}")]
        public IActionResult Inventory(string searchTerm, int page = 1)
        {
            return View(page);
        }

        [HttpGet("aroma/{profile:int}/{page:int?}")]
        public IActionResult Results(int profile, int page = 1)
        {
            var results = _sqliteRepository.Search(profile, page);

            return View(results);
        }

        [HttpGet("autocomplete/{searchTerm}")]
        public List<string> AutoComplete(string searchTerm)
        {
            return _sqliteRepository.Autocomplete(searchTerm);
        }

        [HttpGet("freetext/{searchterm}/{page:int?}")]
        public IActionResult FreeTextResults(string searchterm, int page = 1)
        {
            var results = _sqliteRepository.FreeTextSearch(searchterm, page);

            return View("Results", results);
        }
    }
}
