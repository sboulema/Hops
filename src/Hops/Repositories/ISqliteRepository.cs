using Hops.Models;
using System.Collections.Generic;

namespace Hops.Repositories
{
    public interface ISqliteRepository
    {
        Hop GetRandomHop();
        HopModel GetHopModel(long id);
        HopModel GetHopModel(string slug);
        ListModel<HopModel> Search(string searchTerm, int page);
        ListModel<HopModel> Search(List<long> hopIds, int page);
        ListModel<HopModel> Search(int aromaProfile, int page);
        List<string> Autocomplete(string searchTerm);
        List<Malt> GetMalts();
        List<Yeast> GetYeasts();
    }
}
