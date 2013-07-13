using System.Text;

namespace ByteCarrot.Aspy.Infrastructure
{
    public class IndentWriter
    {
        readonly StringBuilder _sb = new StringBuilder();
        int _indent;

        public void Indent()
        {
            _indent++;
        }

        public void UnIndent()
        {
            if (_indent > 0)
                _indent--;
        }

        public void WriteLine(string line)
        {
            _sb.AppendLine(CreateIndent() + line);
        }

        private string CreateIndent()
        {
            var indentString = new StringBuilder();
            for (var i = 0; i < _indent; i++)
                indentString.Append("    ");
            return indentString.ToString();
        }

        public override string ToString()
        {
            return _sb.ToString();
        }
    }
}