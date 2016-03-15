using Hops.Repositories;
using Microsoft.AspNet.Mvc;

namespace Hops.Controllers
{
    [Route("[controller]")]
    public class SearchController : Controller
    {
        private ISearchRepository searchRepository;

        public SearchController(ISearchRepository searchRepository)
        {
            this.searchRepository = searchRepository;
        }

        [HttpGet("{searchTerm}")]
        public IActionResult Get(string searchTerm)
        {
            return View("List", searchRepository.Search(searchTerm));
        }
    }
}
