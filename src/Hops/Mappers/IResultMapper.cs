﻿using Hops.Models;
using System.Collections.Generic;

namespace Hops.Mappers
{
    public interface IResultMapper
    {
        ListModel<T> Map<T>(List<T> list, string searchTerm, string highlight, int page, int aromaProfile, string searchType);
        ListModel<T> Map<T>(List<T> list, string searchTerm, string highlight, int page);
        ListModel<T> Map<T>(List<T> list, string searchTerm, int page);
        ListModel<T> Map<T>(List<T> list, int page);
    }
}
