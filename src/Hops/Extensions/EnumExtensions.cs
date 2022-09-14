using System;
using System.Text.RegularExpressions;

namespace Hops.Extensions;

public static class EnumExtensions
{
    public static string Wordify(this Enum input)
        => new Regex("(?<=[a-z])(?<x>[A-Z])|(?<=.)(?<x>[A-Z])(?=[a-z])").Replace(input.ToString(), " ${x}");
}
