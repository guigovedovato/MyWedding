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
            ViewBag.Title = TranslateHandler.LOCALPAGE;
            ViewBag.Back = TranslateHandler.BACKTOLIST;

            return View();
        }
    }
}