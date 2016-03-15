using Hops.Models;
using System.Linq;

namespace Hops.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly HopContext context;

        public SearchRepository(HopContext context)
        {
            this.context = context;
        }

        public ListModel Search(string searchTerm)
        {
            var results = new ListModel();
            results.List = context.Hops.Where(h => h.Name.Contains(searchTerm)).ToList();
            return results;
        }
    }
}
