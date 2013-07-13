using System.Text;

namespace ByteCarrot.Aspy.Infrastructure
{
    public class JsonFormatter
    {
        readonly StringWalker _walker;
        readonly IndentWriter _writer = new IndentWriter();
        readonly StringBuilder _currentLine = new StringBuilder();
        bool _quoted;

        public JsonFormatter(string json)
        {
            _walker = new StringWalker(json);
            ResetLine();
        }

        public void ResetLine()
        {
            _currentLine.Length = 0;
        }

        public string Format()
        {
            while (MoveNextChar())
            {
                if (_quoted == false && IsOpenBracket())
                {
                    WriteCurrentLine();
                    AddCharToLine();
                    WriteCurrentLine();
                    _writer.Indent();
                }
                else if (_quoted == false && IsCloseBracket())
                {
                    WriteCurrentLine();
                    _writer.UnIndent();
                    AddCharToLine();
                }
                else if (_quoted == false && IsColon())
                {
                    AddCharToLine();
                    WriteCurrentLine();
                }
                else
                {
                    AddCharToLine();
                }
            }
            WriteCurrentLine();
            return _writer.ToString();
        }

        private bool MoveNextChar()
        {
            var success = _walker.MoveNext();
            if (IsApostrophe())
            {
                _quoted = !_quoted;
            }
            return success;
        }

        public bool IsApostrophe()
        {
            return _walker.CharAtIndex() == '"';
        }

        public bool IsOpenBracket()
        {
            return _walker.CharAtIndex() == '{'
                   || _walker.CharAtIndex() == '[';
        }

        public bool IsCloseBracket()
        {
            return _walker.CharAtIndex() == '}'
                   || _walker.CharAtIndex() == ']';
        }

        public bool IsColon()
        {
            return _walker.CharAtIndex() == ',';
        }

        private void AddCharToLine()
        {
            _currentLine.Append(_walker.CharAtIndex());
        }

        private void WriteCurrentLine()
        {
            var line = _currentLine.ToString().Trim();
            if (line.Length > 0)
            {
                _writer.WriteLine(line);
            }
            ResetLine();
        }

        
    }
}