using Hops.Models;
using Hops.Repositories;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Hops.Controllers
{
    [Route("[controller]")]
    public class SearchController : Controller
    {
        private ISearchRepository searchRepository;
        private IHopRepository hopRepository;

        public SearchController(ISearchRepository searchRepository, IHopRepository hopRepository)
        {
            this.searchRepository = searchRepository;
            this.hopRepository = hopRepository;
        }

        [HttpGet("{searchTerm}")]
        public IActionResult Get(string searchTerm)
        {
            var results = searchRepository.Search(searchTerm);

            if (results.List.Count == 1)
            {
                var model = new DetailModel
                {
                    Hop = results.List.First(),
                    Substitutions = hopRepository.GetSubstitutions(results.List.First().Id)
                };
                return View("/Views/Hop/Detail", model);
            }

            return View("List", results);
        }

        [HttpGet("autocomplete/{searchTerm}")]
        public List<string> AutoComplete(string searchTerm)
        {
            return searchRepository.Autocomplete(searchTerm);
        }
    }
}
