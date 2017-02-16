using MyWeddingSystem.Handlers;
using System.Collections.Generic;
using System.Configuration;
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
            ViewBag.Title = TranslateHandler.PHOTOPAGE;
            ViewBag.Back = TranslateHandler.BACKTOLIST;

            var sPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, 
                                     ConfigurationManager.AppSettings["IMAGEPATH"]);

            var files = Directory.GetFiles(sPath, ConfigurationManager.AppSettings["IMAGEEXTENSION"]);
            List<string> model = new List<string>();
            for (int i = 0; i < files.Length; i++)
                model.Add(Path.GetFileNameWithoutExtension(files[i]));

            return View(model);
        }
    }
}