using Hops.Models.Malts;
using Hops.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hops.Repositories;

public interface IMaltRepository
{
    Task<Malt?> GetRandom();

    Task<Malt?> Get(long id);

    Task<Malt?> Get(string name);

    Task<ListModel<Malt>> Search(string searchTerm, int page);

    Task<ListModel<Malt>> Search(List<long> ids, int page);

    Task<List<string>> Autocomplete(string searchTerm);
}
