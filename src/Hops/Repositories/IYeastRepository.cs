using Hops.Models.ViewModels;
using Hops.Models.Yeasts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hops.Repositories;

public interface IYeastRepository
{
    Task<Yeast?> GetRandom();

    Task<Yeast?> Get(long id);

    Task<Yeast?> Get(string name);

    Task<ListModel<Yeast>> Search(string searchTerm, int page);

    Task<ListModel<Yeast>> Search(List<long> ids, int page);

    Task<List<string>> Autocomplete(string searchTerm);
}
