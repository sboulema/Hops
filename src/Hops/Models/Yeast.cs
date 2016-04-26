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
        public int AttenuationMin { get; set; }
        public int AttenuationMax { get; set; }

        public string Temp()
        {
            return $"{FormatValues(TempMin, TempMax)} &deg;C";
        }

        public string Attenuation()
        {
            return $"{FormatValues(AttenuationMin, AttenuationMax)}%";
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
