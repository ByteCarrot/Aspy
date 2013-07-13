namespace ByteCarrot.Aspy.Infrastructure
{
    public class StringWalker
    {
        readonly string _s;
        public int Index { get; set; }

        public StringWalker(string s)
        {
            _s = s;
            Index = -1;
        }

        public bool MoveNext()
        {
            if (Index == _s.Length - 1)
                return false;
            Index++;
            return true;
        }

        public char CharAtIndex()
        {
            return _s[Index];
        }
    }
}