using Hops.Repositories;
using Microsoft.AspNet.Mvc;
using System;
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

        [HttpGet("{searchTerm}/{page:int?}")]
        public IActionResult Get(string searchTerm, int page = 1)
        {
            var results = searchRepository.Search(searchTerm, page);

            if (results.List.Count == 0)
            {
                return View("NoResults", hopRepository.Get(new Random().Next(1, hopRepository.GetNumberOfHops() + 1)));
            }

            if (results.List.Count == 1)
            {
                return Redirect($"/Hop/{results.List.First().Hop.Id}");
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
