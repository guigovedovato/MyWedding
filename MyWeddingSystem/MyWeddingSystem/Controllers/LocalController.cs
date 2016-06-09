using MyWeddingSystem.Handlers;
using System.Web.Mvc;

namespace MyWeddingSystem.Controllers
{
    public class LocalController : BaseController
    {
        public LocalController()
        {
        }

        // GET: Local
        public ActionResult Index()
        {
            ViewBag.Title = MessagesHandler.LOCALPAGE;
            ViewBag.Back = MessagesHandler.BACKTOLIST;

            if (userSession.AuthUser.Confirmded)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Unauthorized", "Management");
            }
        }
    }
}