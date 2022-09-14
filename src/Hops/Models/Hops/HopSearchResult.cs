namespace Hops.Models.Hops;

public class HopSearchResult
{
    public HopSearchResult(Hop? hop, Alias? alias)
    {
        Hop = hop;
        Alias = alias;
    }

    public Hop? Hop { get; set; }

    public Alias? Alias { get; set; }
}
