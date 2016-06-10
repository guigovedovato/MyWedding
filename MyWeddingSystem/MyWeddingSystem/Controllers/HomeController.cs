using System.Web.Mvc;
using MyWeddingSystem.Handlers;

namespace MyWeddingSystem.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {
        }

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Title = TranslateHandler.HOMEPAGE;

            return View();
        }
    }
}
