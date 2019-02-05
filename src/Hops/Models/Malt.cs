namespace Hops.Models
{
    public class Malt
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double EBCMin { get; set; }
        public double EBCMax { get; set; }
        public int Maltster { get; set; }
        public int? Ratio { get; set; }
        public double? Yield { get; set; }

        public string EBC()
        {
            return FormatValues(EBCMin, EBCMax);
        }

        private string FormatValues(double min, double max)
        {
            if (min == max)
            {
                return $"{min}";
            }
            return $"{min} - {max}";
        }
    }
}
