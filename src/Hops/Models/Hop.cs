using Hops.Mappers;

namespace Hops.Models
{
    public class Hop
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Pedigree { get; set; }
        public int BrewingUsage { get; set; }
        public string Aroma { get; set; }
        public double AlphaMin { get; set; }
        public double AlphaMax { get; set; }
        public double BetaMin { get; set; }
        public double BetaMax { get; set; }
        public int CoHumuloneMin { get; set; }
        public int CoHumuloneMax { get; set; }
        public double TotalOilMin { get; set; }
        public double TotalOilMax { get; set; }
        public string Trade { get; set; }
        public string Styles { get; set; }
        public string Info { get; set; }

        public string BrewingUsageString()
        {
            return ((BrewingUsage)BrewingUsage).Wordify();
        }

        public string AlphaAcid()
        {
            return FormatValues(AlphaMin, AlphaMax);
        }

        public string BetaAcid()
        {
            return FormatValues(BetaMin, BetaMax);
        }

        public string CoHumulone()
        {
            return FormatValues(CoHumuloneMin, CoHumuloneMax);
        }

        public string TotalOil()
        {
            return FormatValues(TotalOilMin, TotalOilMax);
        }

        private static string FormatValues(double min, double max)
        {
            if (min == max)
            {
                return $"~{min}";
            }
            return $"{min} - {max}";
        }

        public string Slug()
        {
            return SlugMapper.Map(Name);
        }
    }
}
