using Hops.Models;
using System.Collections.Generic;

namespace Hops.Repositories
{
    public interface ISearchRepository
    {
        ListModel Search(string searchTerm, int page);
        List<string> Autocomplete(string searchTerm);
    }
}
