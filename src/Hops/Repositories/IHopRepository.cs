using Hops.Models;
using System.Collections.Generic;

namespace Hops.Repositories
{
    public interface IHopRepository
    {
        ListModel GetAll();
        ListModel GetPage(int page);
        Hop Get(long id);
        List<Hop> GetSubstitutions(long id);
    }
}
