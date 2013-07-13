using System.Collections.Generic;

namespace ByteCarrot.Aspy.Tree
{
    public class Node
    {
        public string Kind { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public List<Node> Children { get; set; }

        public Node()
        {
            Children = new List<Node>();
        }
    }
}