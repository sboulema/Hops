using System;
using System.Text.RegularExpressions;

namespace Hops.Models
{
    public enum BrewingUsage
    {
        Unknown = 0,
        Aroma = 1,
        Bittering = 2,
        DualPurpose = 3
    }

    public static class BrewingUsageExtension
    {
        public static string Wordify(this BrewingUsage input)
        {
            Regex r = new Regex("(?<=[a-z])(?<x>[A-Z])|(?<=.)(?<x>[A-Z])(?=[a-z])");
            return r.Replace(input.ToString(), " ${x}");
        }
    }
}
