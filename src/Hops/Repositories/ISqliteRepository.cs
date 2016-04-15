using Hops.Models;
using System.Collections.Generic;

namespace Hops.Repositories
{
    public interface ISqliteRepository
    {
        Hop GetRandomHop();
        HopModel GetHopModel(long id);
        ListModel Search(string searchTerm, int page);
        ListModel Search(List<long> hopIds, int page);
        ListModel Search(int aromaProfile, int page);
        List<string> Autocomplete(string searchTerm);
    }
}
