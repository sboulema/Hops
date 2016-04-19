using System.Collections.Generic;

namespace Hops.Models
{
    public class ListModel<T>
    {
        public List<T> List { get; set; }
        public PaginationModel Pagination { get; set; }
    }
}
