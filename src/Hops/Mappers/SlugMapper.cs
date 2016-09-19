using System;
using System.Globalization;
using System.Text;

namespace Hops.Mappers
{
    public static class SlugMapper
    {
        public static string Map(string input)
        {
            return input.Replace('ü', 'u').Replace(" ", "-").ToLower();
        }
    }
}
