using Hops.Mappers;
using Hops.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hops.Repositories
{
    public class SqliteRepository : ISqliteRepository
    {
        private List<Hop> Hops;
        private List<Alias> Aliases;
        private List<Aroma> Aromas;
        private List<Substitution> Substitutions;
        private List<Malt> Malts;
        private List<Yeast> Yeasts;

        private IResultMapper resultMapper;

        public SqliteRepository(HopContext context, IResultMapper resultMapper)
        {
            Hops = context.Hops.ToList();
            Aliases = context.Alias.ToList();
            Aromas = context.Aroma.ToList();
            Substitutions = context.Substitutions.ToList();
            Malts = context.Malt.ToList();
            Yeasts = context.Yeast.ToList();

            this.resultMapper = resultMapper;
        }

        private Hop GetHop(long id)
        {
            return Hops.First(t => t.Id == id);
        }

        public Hop GetRandomHop()
        {
            return GetHop(new Random().Next(1, Hops.Count + 1));
        }

        public HopModel GetHopModel(long id)
        {
            return new HopModel
            {
                Hop = GetHop(id),
                Substitutions = GetSubstitutions(id),
                Aliases = Aliases.Where(a => a.HopId == id).Select(a => a.Name).ToList(),
                Aromas = Aromas.Where(a => a.HopId == id).Select(a => (AromaProfileEnum)a.Profile).ToList()
            };
        }

        private List<Hop> GetSubstitutions(long id)
        {
            var substitutions = Substitutions.Where(s => s.HopId == id).ToList();

            var hops = new List<Hop>();
            foreach (var substitute in substitutions)
            {
                hops.Add(GetHop(substitute.SubId));
            }

            return hops.OrderBy(h => h.Name).ToList();
        }

        public ListModel<HopModel> Search(string searchTerm, int page)
        {
            var totalResultList = Hops.GroupJoin(Aliases,
                hop => hop.Id,
                alias => alias.HopId,
                (hop, aliases) => new { hop, aliases }
            )
            .Where(r => Contains(r.hop.Name, searchTerm, StringComparison.OrdinalIgnoreCase) ||
                r.aliases.Any(a => Contains(a.Name, searchTerm, StringComparison.OrdinalIgnoreCase)))
            .Select(r => GetHopModel(r.hop.Id))
            .OrderBy(h => h.Hop.Name)
            .ToList();

            return resultMapper.Map(totalResultList, searchTerm, page);
        }

        public ListModel<HopModel> Search(List<long> hopIds, int page)
        {
            var totalResultList = Hops.GroupJoin(Aliases,
                hop => hop.Id,
                alias => alias.HopId,
                (hop, aliases) => new { hop, aliases }
            )
            .Where(r => hopIds.IndexOf(r.hop.Id) != -1)
            .Select(r => GetHopModel(r.hop.Id))
            .OrderBy(h => h.Hop.Name)
            .ToList();

            return resultMapper.Map(totalResultList, page);
        }

        public ListModel<HopModel> Search(int aromaProfile, int page)
        {
            var totalResultList = Hops.GroupJoin(Aromas,
                hop => hop.Id,
                aroma => aroma.HopId,
                (hop, aromas) => new { hop, aromas }
            )
            .Where(r => r.aromas.Any(a => a.Profile == aromaProfile))
            .Select(r => GetHopModel(r.hop.Id))
            .OrderBy(h => h.Hop.Name)
            .ToList();

            return resultMapper.Map(totalResultList, ((AromaProfileEnum)aromaProfile).Wordify(), page);
        }

        public List<string> Autocomplete(string searchTerm)
        {
            var results = Hops.GroupJoin(Aliases,
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

        public List<Malt> GetMalts()
        {
            return Malts;
        }

        public List<Yeast> GetYeasts()
        {
            return Yeasts;
        }

        private bool Contains(string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
