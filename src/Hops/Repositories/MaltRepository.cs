using Hops.Mappers;
using Hops.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hops.Repositories
{
    public class MaltRepository : IMaltRepository
    {
        private List<Malt> Malts;
        private IResultMapper resultMapper;

        public MaltRepository(ISqliteRepository sqliteRepository, IResultMapper resultMapper)
        {
            Malts = sqliteRepository.GetMalts();
            this.resultMapper = resultMapper;
        }

        public Malt Get(long id)
        {
            return Malts.First(t => t.Id == id);
        }

        public Malt GetRandom()
        {
            return Get(new Random().Next(1, Malts.Count + 1));
        }

        public ListModel<Malt> Search(string searchTerm, int page)
        {
            var totalResultList = Malts
            .Where(r => Contains(r.Name, searchTerm, StringComparison.OrdinalIgnoreCase))
            .OrderBy(m => m.Name)
            .ToList();

            return resultMapper.Map(totalResultList, searchTerm, page);
        }

        public ListModel<Malt> Search(List<long> ids, int page)
        {
            var totalResultList = Malts
            .Where(r => ids.IndexOf(r.Id) != -1)
            .OrderBy(m => m.Name)
            .ToList();

            return resultMapper.Map(totalResultList, page);
        }

        public List<string> Autocomplete(string searchTerm)
        {
            var results = Malts
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
