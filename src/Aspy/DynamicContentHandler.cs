using System.Web;
using ByteCarrot.Aspy.Tree;

namespace ByteCarrot.Aspy
{
    public class DynamicContentHandler
    {
        public void HandleSession(HttpContext context)
        {
            var res = context.Response;
            var ses = context.Session;

            var node = new ObjectDumper().Dump(ses);

            res.ContentType = "application/json";
            res.Write(node.ToJson());
        }

        public void HandleCache(HttpContext context)
        {
            var res = context.Response;
            var cache = context.Cache;

            var node = new ObjectDumper().Dump(cache);

            res.ContentType = "application/json";
            res.Write(node.ToJson());
        }
    }
}