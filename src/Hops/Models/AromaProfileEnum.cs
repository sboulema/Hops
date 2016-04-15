using System.Text.RegularExpressions;

namespace Hops.Models
{
    public enum AromaProfileEnum
    {
        Unknown = 0,
        Citrus = 1,
        Herbal = 2,
        Earthy = 3,
        Floral = 4,
        Grapefruit = 5,
        Orange = 6,
        Spicy = 7,
        Fruity = 8,
        Pine = 9,
        TropicalFruit = 10,
        Cedar = 11,
        Chocolate = 12,
        Grassy = 13,
        Onion = 14,
        Garlic = 15,
        StoneFruit = 16
    }

    public static class AromaProfileEnumExtension
    {
        public static string Wordify(this AromaProfileEnum input)
        {
            Regex r = new Regex("(?<=[a-z])(?<x>[A-Z])|(?<=.)(?<x>[A-Z])(?=[a-z])");
            return r.Replace(input.ToString(), " ${x}");
        }
    }
}
