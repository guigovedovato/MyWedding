using System.Web.Mvc;

namespace MyWeddingSystem.Attributes
{
    public class AccessDeniedAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("~/Management/Login?ReturnUrl=" + filterContext.HttpContext.Request.CurrentExecutionFilePath);
            }

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectResult("~/Management/Unauthorized");
            }
        }
    }
}