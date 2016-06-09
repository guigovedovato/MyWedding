using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MyWeddingSystem.Handlers;
using MyWeddingSystem.Models.Model.Anemic;
using MyWeddingSystem.Models.Model;
using AutoMapper;
using MyWeddingSystem.Models.ViewModel;
using System.Linq;

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
            ViewBag.Title = MessagesHandler.GUESTPAGE;
            ViewBag.Type = MessagesHandler.GUESTTYPE;

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
                logRepository.Insert(ex, userSession.AuthUser, LogType.ERROR, local);

                var model = new GuestView() { Message = MessagesHandler.GUESTPAGEERROR };
                var listModel = new List<GuestView>();
                listModel.Add(model);
                return View(listModel);
            }
        }

        // GET: Insert
        public ActionResult Insert()
        {
            ViewBag.Title = MessagesHandler.GUESTPAGE;
            ViewBag.Back = MessagesHandler.BACKTOLIST;
            DateTime date1 = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["DEADLINE"].ToString());
            if (DateTime.Compare(date1, DateTime.Now) < 0)
            {
                return RedirectToAction("Unauthorized", "Management");
            }

            return View(new GuestView() { UserID = userSession.AuthUser.ID, UserName = userSession.AuthUser.Name, Quantity = null });
        }

        // POST: Insert
        [HttpPost]
        public ActionResult Insert(GuestView guestView)
        {
            ViewBag.Title = MessagesHandler.GUESTPAGE;
            ViewBag.Back = MessagesHandler.BACKTOLIST;
            local.Controller = this.ControllerContext.RouteData.Values["controller"].ToString();
            local.Action = this.ControllerContext.RouteData.Values["action"].ToString();

            if (!ModelState.IsValid)
            {
                guestView.Message = MessagesHandler.FORMINVALID;
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
                logRepository.Insert(ex, userSession.AuthUser, LogType.ERROR, local);

                guestView.Message = MessagesHandler.GUESTINSERTERROR;
                return View(guestView);
            }
        }

        private void Confirmed(GuestView model)
        {
            userSession.AuthUser.Confirmded = true;
        }
    }
}
