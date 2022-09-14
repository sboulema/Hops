namespace Hops.Models.ViewModels;

public class PaginationModel
{
    public int NumberOfPages { get; set; }

    public int CurrentPageIndex { get; set; }

    public string SearchTerm { get; set; } = string.Empty;

    public string HighLight { get; set; } = string.Empty;

    public string PaginationUrl { get; set; } = string.Empty;
}
