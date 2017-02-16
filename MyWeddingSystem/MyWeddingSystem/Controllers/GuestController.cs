using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MyWeddingSystem.Handlers;
using MyWeddingSystem.Models.Model;
using AutoMapper;
using MyWeddingSystem.Models.ViewModel;
using System.Linq;
using MyWeddingSystem.Models.Enum;

namespace MyWeddingSystem.Controllers
{
    public class GuestController : BaseController
    {
        private GuestRepository guestRepository;

        public GuestController()
        {
            guestRepository = new GuestRepository();
            config = new MapperConfiguration(cfg => {
                cfg.CreateMap<GuestRepository, GuestView>();
                cfg.CreateMap<GuestView, GuestRepository>().ForMember(t => t.CreatedAt, opt => opt.UseValue(DateTime.Now));
            });
            mapper = config.CreateMapper();
        }

        // GET: Guest
        public ActionResult Index()
        {
            ViewBag.Title = TranslateHandler.GUESTPAGE;
            ViewBag.Type = TranslateHandler.GUESTTYPE;

            local.Controller = this.ControllerContext.RouteData.Values["controller"].ToString();
            local.Action = this.ControllerContext.RouteData.Values["action"].ToString();

            try
            {
                var model = mapper.Map<List<GuestRepository>, List<GuestView>>(guestRepository.GetAll());
                var responseModel = model.GroupBy(w => w.UserID).Select(g => g.LastOrDefault()).ToList();
                return View(responseModel);
            }
            catch (Exception ex)
            {
                logRepository.Insert(ex, userSession.LoggedUser, LogType.ERROR, local);

                var model = new GuestView() { Message = TranslateHandler.GUESTPAGEERROR };
                var listModel = new List<GuestView>();
                listModel.Add(model);
                return View(listModel);
            }
        }

        // GET: Insert
        public ActionResult Insert()
        {
            ViewBag.Title = TranslateHandler.GUESTPAGE;
            ViewBag.Back = TranslateHandler.BACKTOLIST;
            ViewBag.Confirm = TranslateHandler.CONFIRM;
            DateTime date1 = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["DEADLINE"].ToString());
            if (DateTime.Compare(date1, DateTime.Now) < 0)
            {
                return RedirectToAction("Unauthorized", "Logon");
            }

            return View(new GuestView() { UserID = userSession.LoggedUser.ID, UserName = userSession.LoggedUser.Name, Quantity = null });
        }

        // POST: Insert
        [HttpPost]
        public ActionResult Insert(GuestView guestView)
        {
            ViewBag.Title = TranslateHandler.GUESTPAGE;
            ViewBag.Back = TranslateHandler.BACKTOLIST;
            local.Controller = this.ControllerContext.RouteData.Values["controller"].ToString();
            local.Action = this.ControllerContext.RouteData.Values["action"].ToString();

            if (!ModelState.IsValid)
            {
                guestView.Message = TranslateHandler.FORMINVALID;
                return View(guestView);
            }

            try
            {
                mapper.Map<GuestView, GuestRepository>(guestView, guestRepository);
                var model = mapper.Map<GuestRepository, GuestView>(guestRepository.Insert());
                Confirmed(model);
                return View("Thanks", model);
            }
            catch (Exception ex)
            {
                logRepository.Insert(ex, userSession.LoggedUser, LogType.ERROR, local);

                guestView.Message = TranslateHandler.GUESTINSERTERROR;
                return View(guestView);
            }
        }

        private void Confirmed(GuestView model)
        {
            userSession.LoggedUser.Confirmed = true;
        }
    }
}
