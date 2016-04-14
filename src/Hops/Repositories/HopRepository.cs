using Hops.Models;
using System.Collections.Generic;
using System.Linq;

namespace Hops.Repositories
{
    public class HopRepository : IHopRepository
    {
        private readonly HopContext context;

        public HopRepository(HopContext context)
        {
            this.context = context;
        }

        public ListModel GetPage(int page)
        {
            var hops = context.Hops;

            var results = new ListModel();
            results.NumberOfPages = (hops.Count() / 15) + 1;
            results.CurrentPageIndex = page;
            results.List = new List<HopModel>();

            foreach (var hop in hops.OrderBy(h => h.Name).Skip((page - 1) * 15).Take(15).ToList())
            {
                results.List.Add(new HopModel { Hop = hop, Substitutions = GetSubstitutions(hop.Id) });
            }

            return results;
        }

        public Hop Get(long id)
        {
            var hop = context.Hops.First(t => t.Id == id);
            return hop;
        }

        public List<Hop> GetSubstitutions(long id)
        {
            var substitutions = context.Substitutions.Where(s => s.HopId == id).ToList();

            var hops = new List<Hop>();
            foreach (var substitute in substitutions)
            {
                hops.Add(Get(substitute.SubId));
            }

            return hops.OrderBy(h => h.Name).ToList();
        }

        public List<string> GetAliases(long id)
        {
            var aliases = context.Alias.Where(a => a.HopId == id).Select(a => a.Name).ToList();
            return aliases;
        }

        public List<AromaProfileEnum> GetAromas(long id)
        {
            var aromas = context.Aroma.Where(a => a.HopId == id).Select(a => (AromaProfileEnum)a.Profile).ToList();
            return aromas;
        }

        public int GetNumberOfHops()
        {
            return context.Hops.Count();
        }
    }
}
