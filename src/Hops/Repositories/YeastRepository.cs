using Hops.Mappers;
using Hops.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hops.Repositories
{
    public class YeastRepository : IYeastRepository
    {
        private List<Yeast> Yeasts;
        private IResultMapper resultMapper;

        public YeastRepository(ISqliteRepository sqliteRepository, IResultMapper resultMapper)
        {
            Yeasts = sqliteRepository.GetYeasts();
            this.resultMapper = resultMapper;
        }

        public Yeast Get(long id)
        {
            return Yeasts.First(t => t.Id == id);
        }

        public Yeast Get(string name)
        {
            return Yeasts.First(y => Contains(y.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public Yeast GetRandom()
        {
            return Get(new Random().Next(1, Yeasts.Count + 1));
        }

        public ListModel<Yeast> Search(string searchTerm, int page)
        {
            var totalResultList = Yeasts
            .Where(r => Contains(r.Name, searchTerm, StringComparison.OrdinalIgnoreCase))
            .OrderBy(m => m.Name)
            .ToList();

            return resultMapper.Map(totalResultList, searchTerm, page);
        }

        public ListModel<Yeast> Search(List<long> ids, int page)
        {
            var totalResultList = Yeasts
            .Where(r => ids.IndexOf(r.Id) != -1)
            .OrderBy(m => m.Name)
            .ToList();

            return resultMapper.Map(totalResultList, "Inventory", page);
        }

        public List<string> Autocomplete(string searchTerm)
        {
            var results = Yeasts
            .Where(r => Contains(r.Name, searchTerm, StringComparison.OrdinalIgnoreCase))
            .Select(m => m.Name)
            .ToList();

            return results;
        }

        private bool Contains(string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
