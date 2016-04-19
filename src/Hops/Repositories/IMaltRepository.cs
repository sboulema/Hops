using Hops.Models;
using System.Collections.Generic;

namespace Hops.Repositories
{
    public interface IMaltRepository
    {
        Malt GetRandom();
        Malt Get(long id);
        ListModel<Malt> Search(string searchTerm, int page);
        ListModel<Malt> Search(List<long> ids, int page);
        List<string> Autocomplete(string searchTerm);
    }
}
