using System.ComponentModel.DataAnnotations;

namespace Hops.Models.Malts;

public class Malt
{
    [Key]
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public double EBCMin { get; set; }

    public double EBCMax { get; set; }

    public int Maltster { get; set; }

    public int? Ratio { get; set; }

    public double? Yield { get; set; }

    public string EBC() => FormatValues(EBCMin, EBCMax);

    private static string FormatValues(double min, double max) => min == max ? $"{min}" : $"{min} - {max}";
}
