using Hops.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hops.Repositories
{
    public class MaltRepository : IMaltRepository
    {
        private List<Malt> Malts;

        public MaltRepository(ISqliteRepository sqliteRepository)
        {
            Malts = sqliteRepository.GetMalts();
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

            var results = new ListModel<Malt>();
            results.Pagination = new PaginationModel {
                NumberOfPages = (totalResultList.Count() / 15) + 1,
                CurrentPageIndex = page,
                SearchTerm = searchTerm
            };
            results.List = totalResultList.Skip((page - 1) * 15).Take(15).ToList();

            return results;
        }

        public ListModel<Malt> Search(List<long> ids, int page)
        {
            var totalResultList = Malts
            .Where(r => ids.IndexOf(r.Id) != -1)
            .OrderBy(m => m.Name)
            .ToList();

            var results = new ListModel<Malt>();
            results.Pagination = new PaginationModel
            {
                NumberOfPages = (totalResultList.Count() / 15) + 1,
                CurrentPageIndex = page
            };
            results.List = totalResultList.Skip((page - 1) * 15).Take(15).ToList();

            return results;
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
