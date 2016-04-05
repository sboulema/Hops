using Hops.Models;
using System.Collections.Generic;

namespace Hops.Repositories
{
    public interface IHopRepository
    {
        ListModel GetPage(int page);
        Hop Get(long id);
        List<Hop> GetSubstitutions(long id);
        List<string> GetAliases(long id);
        List<AromaProfileEnum> GetAromas(long id);
        int GetNumberOfHops();
    }
}
