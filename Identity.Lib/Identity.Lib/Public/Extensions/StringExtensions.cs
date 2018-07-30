
namespace Identity.Lib.Public.Extensions
{
    public static class StringExtensions
    {
        public static string TryTrim(this string value)
        {
            return string.IsNullOrEmpty(value)
                ? value
                : value.Trim();
        }

        public static bool HasValue(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}
