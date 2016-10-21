namespace Hops.Mappers
{
    public static class SlugMapper
    {
        public static string Map(string input)
        {
            return input.Replace('ü', 'u').Replace(" ", "-").ToLower();
        }

        public static string SlugToString(string input)
        {
            return input.Replace("-", " ");
        }
    }
}
