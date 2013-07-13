using System;
using System.Collections.Generic;
using System.Text;
using ByteCarrot.Aspy.Infrastructure;

namespace ByteCarrot.Aspy.Tree
{
    public static class TypeExtensions
    {
        private static readonly Dictionary<string, string> TypeMap = 
            new Dictionary<string,string>
            {
                {"System.String", "string"},
                {"System.Object", "object"},
                {"System.Boolean", "bool"},
                {"System.Byte", "byte"},
                {"System.SByte", "sbyte"},
                {"System.Int16", "short"},
                {"System.UInt16", "ushort"},
                {"System.Int32", "int"},
                {"System.UInt32", "uint"},
                {"System.Int64", "long"},
                {"System.UInt64", "ulong"},
                {"System.Single", "float"},
                {"System.Double", "double"},
                {"System.Decimal", "decimal"},
                {"System.Char", "char"}
            };

        public static string GetFullName(this Type type)
        {
            var sb = new StringBuilder();
            var name = type.Name;
            var index = name.IndexOf('`');
            if (name.IndexOf('`') > -1)
            {
                name = name.Substring(0, index);
            }
            name = "{0}.{1}".Args(type.Namespace, name);
            name = TypeMap.ContainsKey(name) ? TypeMap[name] : name;
            sb.Append(name);

            var args = type.GetGenericArguments();
            if (args.Length > 0)
            {
                sb.Append("<");
                var count = 0;
                foreach (var arg in args)
                {
                    if (count > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(arg.GetFullName());
                    count++;
                }
                sb.Append(">");
            }

            return sb.ToString();
        }
    }
}