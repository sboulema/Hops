using Hops.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hops.Repositories
{
    public class YeastRepository : IYeastRepository
    {
        private List<Yeast> Yeasts;

        public YeastRepository(ISqliteRepository sqliteRepository)
        {
            Yeasts = sqliteRepository.GetYeasts();
        }

        public Yeast Get(long id)
        {
            return Yeasts.First(t => t.Id == id);
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

            var results = new ListModel<Yeast>();
            results.Pagination = new PaginationModel {
                NumberOfPages = (totalResultList.Count() / 15) + 1,
                CurrentPageIndex = page,
                SearchTerm = searchTerm
            };
            results.List = totalResultList.Skip((page - 1) * 15).Take(15).ToList();

            return results;
        }

        public ListModel<Yeast> Search(List<long> ids, int page)
        {
            var totalResultList = Yeasts
            .Where(r => ids.IndexOf(r.Id) != -1)
            .OrderBy(m => m.Name)
            .ToList();

            var results = new ListModel<Yeast>();
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
