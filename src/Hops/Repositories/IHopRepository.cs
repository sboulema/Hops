using Hops.Models;
using System.Collections.Generic;

namespace Hops.Repositories
{
    public interface IHopRepository
    {
        List<Hop> GetAll();
        Hop Get(long id);
        List<Hop> GetSubstitutions(long id);
    }
}
