using Hops.Models;
using System.Collections.Generic;

namespace Hops.Repositories
{
    public interface IYeastRepository
    {
        Yeast GetRandom();
        Yeast Get(long id);
        Yeast Get(string name);
        ListModel<Yeast> Search(string searchTerm, int page);
        ListModel<Yeast> Search(List<long> ids, int page);
        List<string> Autocomplete(string searchTerm);
    }
}
