using System.Web;
using System.Web.SessionState;
using ByteCarrot.Aspy.Web;

namespace ByteCarrot.Aspy
{
    public class AspyHandler : IHttpHandler, IReadOnlySessionState
    {
        private readonly StaticContentHandler _static = new StaticContentHandler();
        private readonly DynamicContentHandler _dynamic = new DynamicContentHandler();

        public void ProcessRequest(HttpContext context)
        {
            var action = context.Request.QueryString["action"];
            switch (action)
            {
                case "session":
                    _dynamic.HandleSession(context);
                    break;
                case "cache":
                    _dynamic.HandleCache(context);
                    break;
                    break;
                default:
                    _static.Handle(context);
                    break;
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}
