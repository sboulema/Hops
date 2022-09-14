using Hops.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hops.Mappers;

public static class ResultMapper
{
    private const int PageSize = 15;

    public static ListModel<T> Map<T>(List<T> list, string searchTerm, int page,
        string highlight = "", int? aromaProfile = null, string searchType = "")
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

    private static string MapPaginationUrl(string searchTerm, int? aromaProfile, string searchType)
    {
        if (aromaProfile != null)
        {
            return $"aroma/{aromaProfile}/page/";
        }

        return $"{(string.IsNullOrEmpty(searchType) ? string.Empty : searchType + "/")}{(string.IsNullOrEmpty(searchTerm) ? "" : searchTerm + "/")}page/";
    }
}
