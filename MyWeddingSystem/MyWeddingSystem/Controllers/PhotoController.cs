using MyWeddingSystem.Handlers;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace MyWeddingSystem.Controllers
{
    public class PhotoController : BaseController
    {
        public PhotoController()
        {
        }

        // GET: Photo
        public ActionResult Index()
        {
            ViewBag.Title = MessagesHandler.PHOTOPAGE;
            ViewBag.Back = MessagesHandler.BACKTOLIST;

            var sPath = System.AppDomain.CurrentDomain.BaseDirectory + "Content\\Images\\photos\\";

            var files = Directory.GetFiles(sPath, "*.jpg");
            List<string> model = new List<string>();
            for (int i = 0; i < files.Length; i++)
                model.Add(Path.GetFileNameWithoutExtension(files[i]));

            return View(model);
        }
    }
}