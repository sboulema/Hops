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
        public int? Grist { get; set; }
    }
}
