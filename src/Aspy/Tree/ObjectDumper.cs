using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Caching;
using System.Web.SessionState;

namespace ByteCarrot.Aspy.Tree
{
    public class ObjectDumper
    {
        private readonly int _depth;

        public ObjectDumper(int depth = 5)
        {
            _depth = depth;
        }

        public Node Dump(object o)
        {
            return Process("root", "root", o, 0);
        }

        public List<Node> Dump(HttpSessionState session)
        {
            return session.Keys.Cast<string>()
                .OrderBy(x => x)
                .Select(x => Process("item", x, session[x], 0))
                .ToList();
        }

        public List<Node> Dump(Cache session)
        {
            return session.Cast<DictionaryEntry>()
                .OrderBy(x => x.Key)
                .Select(x => Process("item", (string)x.Key, x.Value, 0))
                .ToList();
        }

        private Node Process(string kind, string name, object o, int level)
        {
            level++;
            var node = new Node {Kind = kind, Name = name};

            if (o == null)
            {
                node.Type = "unknown";
                node.Value = "null";
                return node;
            }

            var type = o.GetType();

            node.Type = type.GetFullName();
            node.Value = o.ToString();
            
            List<Node> children;
            if (type.IsPrimitive || o is String)
            {
                return node;
            }

            if (level == _depth)
            {
                children = new List<Node> {new Node {Kind = "end"}};
            } 
            else if (o is IEnumerable)
            {
                children = HandleEnumerable(o, level);
            }
            else
            {
                children = HandleObject(type, o, level);
            }

            node.Children = children;
            return node;
        }

        private List<Node> HandleEnumerable(object o, int level)
        {
            var list = new List<Node>();
            var index = 0;
            foreach (var item in (IEnumerable)o)
            {
                list.Insert(0, Process("item", "[" + index + "]", item, level));
                index++;
            }
            return list;
        }

        private List<Node> HandleObject(Type type, object o, int level)
        {
            var list = new List<Node>();

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                list.Add(Process("field", field.Name, o, level));
            }

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                var method = property.GetGetMethod(false);
                if (method == null)
                {
                    continue;
                }

                try
                {
                    list.Add(Process("property", property.Name, method.Invoke(o, null), level));
                } 
                catch(TargetInvocationException)
                {
                }
            }

            return list.OrderByDescending(x => x.Name).ToList();
        }
    }
}
