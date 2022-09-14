using System.Collections.Generic;
using System.Linq;

namespace Hops.Models.ViewModels;

public class ListModel<T>
{
    public IEnumerable<T> List { get; set; } = Enumerable.Empty<T>();

    public PaginationModel? Pagination { get; set; }
}
