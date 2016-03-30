using Hops.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hops.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly HopContext context;
        private readonly IHopRepository hopRepository;

        public SearchRepository(HopContext context, IHopRepository hopRepository)
        {
            this.context = context;
            this.hopRepository = hopRepository;
        }

        public ListModel Search(string searchTerm)
        {
            var results = new ListModel();

            results.List = context.Hops.GroupJoin(context.Alias, 
                hop => hop.Id, 
                alias => alias.HopId, 
                (hop, aliases) => new { hop, aliases }
            )
            .Where(r => Contains(r.hop.Name, searchTerm, StringComparison.OrdinalIgnoreCase) || 
                r.aliases.Any(a => Contains(a.Name, searchTerm, StringComparison.OrdinalIgnoreCase)))
            .Select(r => new HopModel { Hop = r.hop, Substitutions = hopRepository.GetSubstitutions(r.hop.Id) })
            .ToList();

            return results;
        }

        public List<string> Autocomplete(string searchTerm)
        {
            var results = context.Hops.GroupJoin(context.Alias,
                hop => hop.Id,
                alias => alias.HopId,
                (hop, aliases) => new { hop, aliases }
            )
            .Where(r => Contains(r.hop.Name, searchTerm, StringComparison.OrdinalIgnoreCase) ||
                r.aliases.Any(a => Contains(a.Name, searchTerm, StringComparison.OrdinalIgnoreCase)))
            .ToList();

            var autocompleteList = new List<string>();
            foreach (var item in results)
            {
                autocompleteList.Add(item.hop.Name);
                foreach (var alias in item.aliases)
                {
                    autocompleteList.Add(alias.Name);
                }               
            }
            return autocompleteList;          
        }

        private bool Contains(string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
