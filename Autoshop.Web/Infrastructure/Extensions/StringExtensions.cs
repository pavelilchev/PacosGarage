namespace Autoshop.Web.Infrastructure.Extensions
{
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static string RemoveHtmlTags(this string str)
        {
            return Regex.Replace(str, @"<[^>]*>", string.Empty);
        }
    }
}
