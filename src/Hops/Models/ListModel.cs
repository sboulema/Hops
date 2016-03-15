using System.Collections.Generic;

namespace Hops.Models
{
    public class ListModel
    {
        public List<Hop> List { get; set; }
        public int NumberOfPages { get; set; }
        public int CurrentPageIndex { get; set; }
    }
}
