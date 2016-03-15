using Hops.Models;

namespace Hops.Repositories
{
    public interface ISearchRepository
    {
        ListModel Search(string searchTerm);
    }
}
