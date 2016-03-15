using Hops.Models;
using System.Collections.Generic;

namespace Hops.Repositories
{
    public interface ISearchRepository
    {
        List<Hop> Search(string searchTerm);
    }
}
