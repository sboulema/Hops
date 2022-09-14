namespace Hops.Models.Hops;

public class Substitution
{
    public long HopId { get; set; }

    public long SubId { get; set; }

    public virtual Hop? Hop { get; set; }
}
