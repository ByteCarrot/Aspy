using System;

namespace ByteCarrot.Aspy.Infrastructure
{
    public static class StringExtensions
    {
        public static string Args(this string s, params object[] args)
        {
            return String.Format(s, args);
        }

        public static bool IsEmpty(this string s)
        {
            return s == null || s.Trim() == String.Empty;
        }
    }
}