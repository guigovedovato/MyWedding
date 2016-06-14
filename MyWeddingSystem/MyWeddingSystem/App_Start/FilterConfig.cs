using MyWeddingSystem.Filters;
using System.Web.Mvc;

namespace MyWeddingSystem
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new NoCacheFilter());
        }
    }
}
