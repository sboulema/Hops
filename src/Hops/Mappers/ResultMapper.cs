using Hops.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hops.Mappers
{
    public class ResultMapper : IResultMapper
    {
        private const int pageSize = 15;

        public ListModel<T> Map<T>(List<T> list, int page)
        {
            return Map<T>(list, null, page);
        }

        public ListModel<T> Map<T>(List<T> list, string searchTerm, int page)
        {
            var results = new ListModel<T>();
            results.Pagination = new PaginationModel
            {
                NumberOfPages = (int)Math.Ceiling((double)list.Count() / pageSize),
                CurrentPageIndex = page,
                SearchTerm = searchTerm
            };
            results.List = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return results;
        }
    }
}
