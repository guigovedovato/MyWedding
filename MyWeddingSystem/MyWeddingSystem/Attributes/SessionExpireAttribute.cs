using MyWeddingSystem.App_Start;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyWeddingSystem.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext context = HttpContext.Current;

            var Session = new LoggedUserSession();

            if (Session.LoggedUser == null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                filterContext.Result = new RedirectResult("~/Logon/Login");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}