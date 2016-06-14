using System.Web;
using System.Web.Mvc;

namespace MyWeddingSystem.Filters
{
    public class NoCacheFilter : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
            cache.SetCacheability(HttpCacheability.NoCache);

            base.OnResultExecuted(filterContext);
        }
    }
}