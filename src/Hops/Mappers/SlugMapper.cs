using System;
using System.Globalization;
using System.Text;

namespace Hops.Mappers
{
    public static class SlugMapper
    {
        public static string Map(string input)
        {
            var normalizedString = input.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC).Replace(" ", "-").ToLower();
        }
    }
}
