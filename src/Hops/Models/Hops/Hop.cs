using Hops.Extensions;
using Hops.Mappers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hops.Models.Hops;

public class Hop
{
    [Key]
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Pedigree { get; set; }

    public int BrewingUsage { get; set; }

    public string? Aroma { get; set; }

    public double AlphaMin { get; set; }

    public double AlphaMax { get; set; }

    public double BetaMin { get; set; }

    public double BetaMax { get; set; }

    public int CoHumuloneMin { get; set; }

    public int CoHumuloneMax { get; set; }

    public double TotalOilMin { get; set; }

    public double TotalOilMax { get; set; }

    public string? Trade { get; set; }

    public string? Styles { get; set; }

    public string? Info { get; set; }

    public string BrewingUsageString() => ((BrewingUsageEnum)BrewingUsage).Wordify();

    public string AlphaAcid() => FormatValues(AlphaMin, AlphaMax);

    public string BetaAcid() => FormatValues(BetaMin, BetaMax);

    public string CoHumulone() => FormatValues(CoHumuloneMin, CoHumuloneMax);

    public string TotalOil() => FormatValues(TotalOilMin, TotalOilMax);

    private static string FormatValues(double min, double max) => min == max ? $"~{min}" : $"{min} - {max}";

    public string Slug() => SlugMapper.Map(Name);

    public virtual List<Alias>? Aliases { get; set; } = new();

    public virtual List<Substitution>? Substitutions { get; set; } = new();

    public virtual List<Aroma>? Aromas { get; set; } = new();
}
