using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using ByteCarrot.Aspy.Infrastructure;

namespace ByteCarrot.Aspy.Web
{
    public class StaticContentHandler
    {
        private static readonly Assembly Assembly = typeof (StaticContentHandler).Assembly;
        private static readonly string Namespace = typeof (StaticContentHandler).Namespace;
        private static readonly Dictionary<string, string> MimeTypes = 
            new Dictionary<string, string>
                {
                    {"html", "text/html"},
                    {"js", "text/javascript"},
                    {"css", "text/css"},
                    {"png", "image/png"},
                    {"gif", "image/gif"}
                };

        public void Handle(HttpContext context)
        {
            var req = context.Request;
            var res = context.Response;

            string file = null;
            if (req.QueryString.Count > 0)
            {
                file = req.QueryString[0];
            }

            if (file.IsEmpty())
            {
                file = "index.html";
            }

            var index = file.LastIndexOf('.');
            if (index == -1)
            {
                throw new NotSupportedException();
            }

            var extension = file.Substring(index + 1, file.Length - index - 1);
            if (extension.IsEmpty())
            {
                throw new NotSupportedException();
            }

            res.ContentType = MimeTypes[extension];
            res.CacheControl = "no-cache";
            using (var stream = Assembly.GetManifestResourceStream("{0}.{1}".Args(Namespace, file)))
            {
                var b = new BinaryReader(stream);
                res.BinaryWrite(b.ReadBytes((int) stream.Length));
            }
        }
    }
}