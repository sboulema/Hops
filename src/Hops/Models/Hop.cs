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
    }
}
