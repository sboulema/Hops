using Hops.Models.Hops;
using Hops.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hops.Repositories;

public interface IHopRepository
{
    Task<Hop?> GetHopById(long id, bool includeSubstitutions = true);

    Task<Hop?> GetHopByName(string name);

    Task<Hop?> GetHopBySlug(string slug);

    Task<List<string>> Autocomplete(string searchTerm);

    Task<ListModel<Hop>> FreeTextSearch(string searchTerm, int page);

    Task<Hop?> GetRandomHop();

    Task<ListModel<Hop>> Search(int aromaProfile, int page);

    Task<ListModel<Hop>> Search(List<long> hopIds, int page);

    Task<ListModel<Hop>> Search(string searchTerm, int page);

    Task<ListModel<Hop>> TopSubstitutors();
}