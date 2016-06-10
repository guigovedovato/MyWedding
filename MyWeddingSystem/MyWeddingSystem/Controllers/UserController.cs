using AutoMapper;
using MyWeddingSystem.Attributes;
using MyWeddingSystem.Handlers;
using MyWeddingSystem.Models.Model;
using MyWeddingSystem.Models.Model.Anemic;
using MyWeddingSystem.Models.ViewModel;
using MyWeddingSystem.Utils;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace MyWeddingSystem.Controllers
{
    [AccessDeniedAuthorize(Roles = "ADM")]
    public class UserController : BaseController
    {
        private UserRepository userRepository;

        public UserController()
        {
            userRepository = new UserRepository();
            config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserRepository, UserView>().ForMember(t => t.Password, opt => opt.Ignore());
                cfg.CreateMap<UserView, UserRepository>();
            });
            mapper = config.CreateMapper();
        }

        // GET: User
        public ActionResult Index()
        {
            ViewBag.Title = TranslateHandler.USERPAGE;
            ViewBag.Type = TranslateHandler.USERTYPE;
            ViewBag.Print = TranslateHandler.PRINT;
            ViewBag.New = TranslateHandler.NEW;

            local.Controller = this.ControllerContext.RouteData.Values["controller"].ToString();
            local.Action = this.ControllerContext.RouteData.Values["action"].ToString();

            try
            {
                var model = mapper.Map<List<UserRepository>, List<UserView>>(userRepository.GetAll());
                VerifyConfirm(model);
                return View(model);
            }
            catch (Exception ex)
            {
                logRepository.Insert(ex, userSession.AuthUser, LogType.ERROR, local);

                var model = new UserView() { Message = TranslateHandler.USERPAGEERROR };
                var listModel = new List<UserView>();
                listModel.Add(model);
                return View(listModel);
            }
        }

        private void VerifyConfirm(List<UserView> model)
        {
            GuestRepository guestRepository = new GuestRepository();
            var guests = guestRepository.GetAll();
            foreach(var user in model)
            {
                if(guests.Exists(g => g.UserID == user.ID))
                {
                    user.Confirmded = true;
                }
            }
        }

        // GET: Insert
        public ActionResult Insert()
        {
            ViewBag.Title = TranslateHandler.USERPAGE;
            ViewBag.Back = TranslateHandler.BACKTOLIST;
            ViewBag.Register = TranslateHandler.REGISTER;

            return View(new UserView());
        }

        // POST: Insert
        [HttpPost]
        public ActionResult Insert(UserView userView)
        {
            ViewBag.Title = TranslateHandler.USERPAGE;
            ViewBag.Back = TranslateHandler.BACKTOLIST;
            ViewBag.Register = TranslateHandler.REGISTER;

            local.Controller = this.ControllerContext.RouteData.Values["controller"].ToString();
            local.Action = this.ControllerContext.RouteData.Values["action"].ToString();

            if (!ModelState.IsValid)
            {
                userView.Message = TranslateHandler.FORMINVALID;
                return View(userView);
            }

            try
            {
                if (!userRepository.IsInserted(userView.Login))
                {
                    CreateUser(userView);
                    mapper.Map<UserView, UserRepository>(userView, userRepository);
                    ViewBag.UserName = userRepository.Insert().Name;
                    return View("Thanks", new UserView());
                }
                else
                {
                    userView.Message = TranslateHandler.USERALREADYINSERTED;
                    return View(userView);
                }
            }
            catch (Exception ex)
            {
                logRepository.Insert(ex, userSession.AuthUser, LogType.ERROR, local);

                userView.Message = string.Format(TranslateHandler.USERINSERTERROR, userView.Login);
                return View(userView);
            }
        }

        private void CreateUser(UserView userView)
        {
            userView.FirstPassword = PasswordGenerator.NewPassword();
            userView.Password = Cript.GetMd5Hash(userView.FirstPassword);
            userView.Profile = (int)UserProfile.LOGGED;
            userView.UpdatedAt = null;
            userView.UpdatedAt = DateTime.Now;
        }

        [HttpGet]
        public ActionResult SavePDF()
        {
            local.Controller = this.ControllerContext.RouteData.Values["controller"].ToString();
            local.Action = this.ControllerContext.RouteData.Values["action"].ToString();

            try
            {
                var model = mapper.Map<List<UserRepository>, List<UserView>>(userRepository.GetAll().Where(x => !x.Login.Equals("ADM")).ToList());
                //return View(model);
                return new Rotativa.ViewAsPdf("SavePDF", model) { FileName = "MyWeddingLoginList.pdf" };
            }
            catch (Exception ex)
            {
                logRepository.Insert(ex, userSession.AuthUser, LogType.ERROR, local);

                var model = new UserView() { Message = TranslateHandler.USERPRINT };
                var listModel = new List<UserView>();
                listModel.Add(model);
                return View(listModel);
            }
        }
    }
}