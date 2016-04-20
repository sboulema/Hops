namespace Hops.Models
{
    public class Yeast
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Lab { get; set; }
        public int TempMin { get; set; }
        public int TempMax { get; set; }

        public string Temp()
        {
            return FormatValues(TempMin, TempMax);
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
