using MyWeddingSystem.App_Start;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyWeddingSystem.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            var userSession = new HttpContextUserSession();

            if (userSession.AuthUser == null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                filterContext.Result = new RedirectResult("~/Management/Login");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}