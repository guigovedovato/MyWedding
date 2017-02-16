using AutoMapper;
using MyWeddingSystem.Attributes;
using MyWeddingSystem.Handlers;
using MyWeddingSystem.Models.Model;
using MyWeddingSystem.Models.ViewModel;
using MyWeddingSystem.Utils;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using MyWeddingSystem.Models.Enum;

namespace MyWeddingSystem.Controllers
{
    [AccessDenied(Roles = "ADM")]
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
                logRepository.Insert(ex, userSession.LoggedUser, LogType.ERROR, local);

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
                    user.Confirmed = true;
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
                    var inserted = userRepository.Insert();
                    ViewBag.UserName = inserted.Name;

                    if (userView.Confirmed)
                    {
                        InsertGuest(userView, inserted.ID);
                    }

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
                logRepository.Insert(ex, userSession.LoggedUser, LogType.ERROR, local);

                userView.Message = string.Format(TranslateHandler.USERINSERTERROR, userView.Login);
                return View(userView);
            }
        }

        private void InsertGuest(UserView userView, int userID)
        {
            GuestRepository guestRepository = new GuestRepository();
            guestRepository.CreatedAt = DateTime.Now;
            guestRepository.Quantity = userView.Quantity;
            guestRepository.UserName = userView.Name;
            guestRepository.UserID = userID;
            guestRepository.Insert();
        }

        private void CreateUser(UserView userView)
        {
            userView.FirstPassword = PasswordGenerator.NewPassword();
            userView.Password = Cript.GetMd5Hash(userView.FirstPassword);
            userView.Profile = (int)UserProfile.LOGGED;
            userView.UpdatedAt = null;
            userView.UpdatedAt = DateTime.Now;
        }
    }
}