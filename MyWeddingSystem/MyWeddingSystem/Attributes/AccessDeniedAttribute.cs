using System.Web.Mvc;

namespace MyWeddingSystem.Attributes
{
    public class AccessDeniedAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("~/Logon/Login?ReturnUrl=" + filterContext.HttpContext.Request.CurrentExecutionFilePath);
            }

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectResult("~/Logon/Unauthorized");
            }
        }
    }
}