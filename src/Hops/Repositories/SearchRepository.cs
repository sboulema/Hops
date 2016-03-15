using Hops.Models;
using System.Collections.Generic;
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

        public List<Hop> Search(string searchTerm)
        {
            var results = context.Hops.Where(h => h.Name.Contains(searchTerm)).ToList();
            return results;
        }
    }
}
