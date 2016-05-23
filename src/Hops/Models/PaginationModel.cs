namespace Hops.Models
{
    public class PaginationModel
    {
        public int NumberOfPages { get; set; }
        public int CurrentPageIndex { get; set; }
        public string SearchTerm { get; set; }
        public string HighLight { get; set; }
    }
}
