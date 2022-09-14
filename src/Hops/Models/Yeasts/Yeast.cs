using System.ComponentModel.DataAnnotations;

namespace Hops.Models.Yeasts;

public class Yeast
{
    [Key]
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Lab { get; set; }

    public int TempMin { get; set; }

    public int TempMax { get; set; }

    public int AttenuationMin { get; set; }

    public int AttenuationMax { get; set; }

    public string Temp() => $"{FormatValues(TempMin, TempMax)} &deg;C";

    public string Attenuation() => $"{FormatValues(AttenuationMin, AttenuationMax)}%";

    private static string FormatValues(double min, double max) => min == max ? $"{min}" : $"{min} - {max}";
}
