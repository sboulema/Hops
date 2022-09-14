using System.Text.RegularExpressions;

namespace Hops.Models.Yeasts;

public enum YeastLabEnum
{
    Unknown = 0,
    MangroveJacks = 1
}

public static class YeastLabEnumExtension
{
    public static string Wordify(this YeastLabEnum input)
    {
        Regex r = new Regex("(?<=[a-z])(?<x>[A-Z])|(?<=.)(?<x>[A-Z])(?=[a-z])");
        return r.Replace(input.ToString(), " ${x}");
    }
}
