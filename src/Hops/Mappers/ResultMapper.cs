using Hops.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hops.Mappers
{
    public class ResultMapper : IResultMapper
    {
        private const int PageSize = 15;

        public ListModel<T> Map<T>(List<T> list, int page) => Map(list, null, page);
        public ListModel<T> Map<T>(List<T> list, string searchTerm, int page) => Map(list, searchTerm, string.Empty, page);
        public ListModel<T> Map<T>(List<T> list, string searchTerm, string highlight, int page) => 
            Map(list, searchTerm, highlight, page, 0, string.Empty);
        public ListModel<T> Map<T>(List<T> list, string searchTerm, string highlight, int page, int aromaProfile, string searchType)
        {
            var results = new ListModel<T>
            {
                Pagination = new PaginationModel
                {
                    NumberOfPages = (int) Math.Ceiling((double) list.Count / PageSize),
                    CurrentPageIndex = page,
                    SearchTerm = searchTerm,
                    HighLight = highlight,
                    PaginationUrl = MapPaginationUrl(searchTerm, aromaProfile, searchType)
                },
                List = list.Skip((page - 1) * PageSize).Take(PageSize).ToList()
            };
            return results;
        }

        private static string MapPaginationUrl(string searchTerm, int aromaProfile, string searchType)
        {
            if (aromaProfile > 0)
            {
                return $"aroma/{aromaProfile}/page/";
            }
            return $"{(string.IsNullOrEmpty(searchType) ? string.Empty : searchType + "/")}{(string.IsNullOrEmpty(searchTerm) ? "" : searchTerm + "/")}page/";
        }
    }
}
