namespace ByteCarrot.Aspy.Infrastructure
{
    public static class JsonExtensions
    {
        public static string ToFormattedJson(this string s)
        {
            return new JsonFormatter(s).Format().Trim();
        }
    }
}