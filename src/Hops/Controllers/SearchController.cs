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
        public IActionResult Results(string searchTerm, int page = 1)
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

            return View(results);
        }

        [HttpGet("inventory/{searchTerm}/{page:int?}")]
        public IActionResult Inventory(string searchTerm, int page = 1)
        {
            var results = searchRepository.Search(searchTerm.Split(',').Select(s => long.Parse(s)).ToList(), page);

            return View(results);
        }

        [HttpGet("aroma/{profile:int}/{page:int?}")]
        public IActionResult Results(int profile, int page = 1)
        {
            var results = searchRepository.Search(profile, page);

            return View(results);
        }

        [HttpGet("autocomplete/{searchTerm}")]
        public List<string> AutoComplete(string searchTerm)
        {
            return searchRepository.Autocomplete(searchTerm);
        }
    }
}
