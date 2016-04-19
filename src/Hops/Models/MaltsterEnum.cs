using System.Text.RegularExpressions;

namespace Hops.Models
{
    public enum MaltsterEnum
    {
        Unknown = 0,
        Dingemans = 1,
        CastleMalting = 2,
        BestBrew = 3
    }

    public static class MaltsterEnumExtension
    {
        public static string Wordify(this MaltsterEnum input)
        {
            Regex r = new Regex("(?<=[a-z])(?<x>[A-Z])|(?<=.)(?<x>[A-Z])(?=[a-z])");
            return r.Replace(input.ToString(), " ${x}");
        }
    }
}
