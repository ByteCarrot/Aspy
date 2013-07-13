using System.Collections.Generic;
using System.Text;
using ByteCarrot.Aspy.Infrastructure;

namespace ByteCarrot.Aspy.Tree
{
    public static class NodeExtensions
    {
        public static string ToJson(this Node node)
        {
            var sb = new StringBuilder();
            ToJson(node, sb);
            return sb.ToString();
        }

        public static string ToJson(this IEnumerable<Node> nodes)
        {
            var sb = new StringBuilder();
            sb.Append("[");
            var first = false;
            foreach (var child in nodes)
            {
                if (!first)
                {
                    ToJson(child, sb);
                    first = true;
                    continue;
                }
                sb.Append(",");
                ToJson(child, sb);
            }
            sb.Append("]");
            return sb.ToString();
        }

        private static void ToJson(Node node, StringBuilder sb)
        {
            sb.Append("{");
            sb.AppendValue("kind", node.Kind);
            sb.Append(",");
            sb.AppendValue("name", node.Name);
            sb.Append(",");
            sb.AppendValue("type", node.Type);
            sb.Append(",");
            sb.AppendValue("value", node.Value);
            sb.Append(",");
            sb.Append("\"children\":[");
            var first = false;
            foreach (var child in node.Children)
            {
                if (!first)
                {
                    ToJson(child, sb);
                    first = true;
                    continue;
                }
                sb.Append(",");
                ToJson(child, sb);
            }
            sb.Append("]");
            sb.Append("}");
        }

        private static void AppendValue(this StringBuilder sb, string name, string value)
        {
            sb.Append("\"");
            sb.Append(name);
            sb.Append("\":");
            sb.Append("\"");
            if (!value.IsEmpty())
            {
                sb.Append(
                    value.Replace(@"\", @"\\").Replace("\"", "\\\"").Replace("\r", "\\r").Replace("\n", "\\n").
                        EncodeHtmlTags());
            }
            sb.Append("\"");
        }

        private static string EncodeHtmlTags(this string s)
        {
            return s.Replace("<", "&lt;").Replace(">", "&gt;");
        }
    }
}