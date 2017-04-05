using Hops.Mappers;
using Hops.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hops.Repositories
{
    public class SqliteRepository : ISqliteRepository
    {
        private readonly List<Hop> _hops;
        private readonly List<Alias> _aliases;
        private readonly List<Aroma> _aromas;
        private readonly List<Substitution> _substitutions;
        private readonly List<Malt> _malts;
        private readonly List<Yeast> _yeasts;

        private readonly IResultMapper _resultMapper;

        public SqliteRepository(HopContext context, IResultMapper resultMapper)
        {
            _hops = context.Hop.ToList();
            _aliases = context.Alias.ToList();
            _aromas = context.Aroma.ToList();
            _substitutions = context.Substitution.ToList();
            _malts = context.Malt.ToList();
            _yeasts = context.Yeast.ToList();

            _resultMapper = resultMapper;
        }

        private Hop GetHop(long id)
        {
            return _hops.First(t => t.Id == id);
        }

        private long GetHopId(string slug)
        {
            return _hops.First(h => string.Equals(SlugMapper.Map(h.Name), slug, StringComparison.CurrentCultureIgnoreCase)).Id;
        }

        public Hop GetRandomHop()
        {
            return GetHop(new Random().Next(1, _hops.Count + 1));
        }

        public HopModel GetHopModel(long id)
        {
            return new HopModel
            {
                Hop = GetHop(id),
                Substitutions = GetSubstitutions(id),
                Aliases = _aliases.Where(a => a.HopId == id).Select(a => a.Name).ToList(),
                Aromas = _aromas.Where(a => a.HopId == id).Select(a => (AromaProfileEnum)a.Profile).ToList()
            };
        }

        public HopModel GetHopModel(string slug)
        {
            return GetHopModel(GetHopId(slug));
        }

        private List<Hop> GetSubstitutions(long id)
        {
            var substitutions = _substitutions.Where(s => s.HopId == id).ToList();

            var hops = new List<Hop>();
            foreach (var substitute in substitutions)
            {
                hops.Add(GetHop(substitute.SubId));
            }

            return hops.OrderBy(h => h.Name).ToList();
        }

        public ListModel<HopModel> Search(string searchTerm, int page)
        {
            var totalResultList = _hops.GroupJoin(_aliases,
                hop => hop.Id,
                alias => alias.HopId,
                (hop, aliases) => new { hop, aliases }
            )
            .Where(r => Contains(r.hop.Name, searchTerm, StringComparison.OrdinalIgnoreCase) ||
                r.aliases.Any(a => Contains(a.Name, searchTerm, StringComparison.OrdinalIgnoreCase)))
            .Select(r => GetHopModel(r.hop.Id))
            .OrderBy(h => h.Hop.Name)
            .ToList();

            return _resultMapper.Map(totalResultList, searchTerm, page);
        }

        public ListModel<HopModel> FreeTextSearch(string searchTerm, int page)
        {
            var totalResultList = _hops.GroupJoin(_aliases,
                hop => hop.Id,
                alias => alias.HopId,
                (hop, aliases) => new { hop, aliases }
            )
            .Where(r => Contains(r.hop.Name, searchTerm, StringComparison.OrdinalIgnoreCase) ||
                r.aliases.Any(a => Contains(a.Name, searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                Contains(r.hop.Info, searchTerm, StringComparison.OrdinalIgnoreCase) ||
                Contains(r.hop.Pedigree, searchTerm, StringComparison.OrdinalIgnoreCase) ||
                Contains(r.hop.Styles, searchTerm, StringComparison.OrdinalIgnoreCase) ||
                Contains(r.hop.Trade, searchTerm, StringComparison.OrdinalIgnoreCase) ||
                Contains(r.hop.Aroma, searchTerm, StringComparison.OrdinalIgnoreCase))
            .Select(r => GetHopModel(r.hop.Id))
            .OrderBy(h => h.Hop.Name)
            .ToList();

            return _resultMapper.Map(totalResultList, searchTerm, searchTerm, page, 0, "freetext");
        }

        public ListModel<HopModel> Search(List<long> hopIds, int page)
        {
            var totalResultList = _hops.GroupJoin(_aliases,
                hop => hop.Id,
                alias => alias.HopId,
                (hop, aliases) => new { hop, aliases }
            )
            .Where(r => hopIds.IndexOf(r.hop.Id) != -1)
            .Select(r => GetHopModel(r.hop.Id))
            .OrderBy(h => h.Hop.Name)
            .ToList();

            return _resultMapper.Map(totalResultList, "Inventory", page);
        }

        public ListModel<HopModel> Search(int aromaProfile, int page)
        {
            var totalResultList = _hops.GroupJoin(_aromas,
                hop => hop.Id,
                aroma => aroma.HopId,
                (hop, aromas) => new { hop, aromas }
            )
            .Where(r => r.aromas.Any(a => a.Profile == aromaProfile))
            .Select(r => GetHopModel(r.hop.Id))
            .OrderBy(h => h.Hop.Name)
            .ToList();

            return _resultMapper.Map(totalResultList, ((AromaProfileEnum)aromaProfile).Wordify(), string.Empty, page, aromaProfile, string.Empty);
        }

        public ListModel<HopModel> TopSubstitutors()
        {
            var list = _substitutions.GroupBy(s => s.SubId)
                  .OrderByDescending(g => g.Count())
                  .SelectMany(g => g)
                  .DistinctBy(d => d.SubId)
                  .Take(10);

            var hops =  new List<HopModel>();
            foreach (var substitute in list)
            {
                hops.Add(GetHopModel(substitute.SubId));
            }

            return _resultMapper.Map(hops, string.Empty, 1);
        }

        public List<string> Autocomplete(string searchTerm)
        {
            var results = _hops.GroupJoin(_aliases,
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
            return _malts;
        }

        public List<Yeast> GetYeasts()
        {
            return _yeasts;
        }

        private bool Contains(string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
