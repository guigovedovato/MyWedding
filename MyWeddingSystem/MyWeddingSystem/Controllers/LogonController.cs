using MyWeddingSystem.Models.ViewModel;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using MyWeddingSystem.Handlers;
using MyWeddingSystem.Models.Model;
using MyWeddingSystem.Models.Model.Anemic;
using AutoMapper;
using MyWeddingSystem.Utils;

namespace MyWeddingSystem.Controllers
{
    public class LogonController : BaseController
    {
        public LogonController()
        {
            config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserRepository, UserView>().ForMember(t => t.Password, opt => opt.Ignore());
            });
            mapper = config.CreateMapper();
        }

        // GET: Logon
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult Unauthorized()
        {
            ViewBag.Title = TranslateHandler.UNAUTHORIZEDPAGE;

            return View();
        }

        [HttpGet]
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.Title = TranslateHandler.LOGUINPAGE;
            ViewBag.Enter = TranslateHandler.ENTER;

            local.Controller = this.ControllerContext.RouteData.Values["controller"].ToString();
            local.Action = this.ControllerContext.RouteData.Values["action"].ToString();

            try
            {
                if (userSession.LoggedUser != null)
                {
                    FormsAuthentication.SetAuthCookie(userSession.LoggedUser.Login, false);

                    return Redirect(ReturnUrl);
                }

                Response.Cache.SetNoStore();
            }
            catch (Exception ex)
            {
                logRepository.Insert(ex, systemUser, LogType.ERROR, local);
            }

            return View(new UserView());
        }

        [HttpPost]
        public ActionResult Login(UserView userView, string ReturnUrl)
        {
            ViewBag.Enter = TranslateHandler.ENTER;

            local.Controller = this.ControllerContext.RouteData.Values["controller"].ToString();
            local.Action = this.ControllerContext.RouteData.Values["action"].ToString();

            if (ReturnUrl == null)
            {
                ReturnUrl = Url.RouteUrl("Default", ((Route)RouteTable.Routes["Default"]).Defaults);
            }
            try
            {
                if ((string.IsNullOrWhiteSpace(userView.Login) && string.IsNullOrWhiteSpace(userView.Password)) ||
                    (userView.Login != null && string.IsNullOrWhiteSpace(userView.Password)) || (string.IsNullOrWhiteSpace(userView.Login) && userView.Password != null))
                {
                    TempData["ErrorMessage"] = TranslateHandler.LOGINANDPASS;
                    logRepository.Insert(systemUser, LogType.ERROR, local, string.Format("{0} - {1}",TempData["ErrorMessage"].ToString(), userView.Login));
                    return View(userView);
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        UserRepository userRepository = new UserRepository();
                        GuestRepository guestRepository = new GuestRepository();

                        var authenticatedUser = mapper.Map<UserRepository, UserView>(userRepository.Logon(userView.Login.ToUpper(), Cript.GetMd5Hash(userView.Password)));

                        if (authenticatedUser == null)
                        {
                            TempData["ErrorMessage"] = TranslateHandler.LOGININVALID;
                            logRepository.Insert(systemUser, LogType.ERROR, local, string.Format("{0} - {1}", TempData["ErrorMessage"].ToString(), userView.Login));
                            return Redirect(ReturnUrl);
                        }

                        var confirmed = guestRepository.GetGuestByUserId(authenticatedUser.ID);
                        if (confirmed != null)
                        {
                            authenticatedUser.Confirmded = authenticatedUser.Login.Equals("ADM") ? true : true;
                        }
                        else
                        {
                            authenticatedUser.Confirmded = authenticatedUser.Login.Equals("ADM") ? true : false;
                        }

                        userSession.LoggedUser = authenticatedUser;
                    }
                    catch (Exception ex)
                    {
                        logRepository.Insert(ex, systemUser, LogType.ERROR, local);

                        TempData["ErrorMessage"] = TranslateHandler.LOGINERROR;
                        return View(userView);
                    }
                }

                return Redirect(ReturnUrl);
            }
            catch (Exception ex)
            {
                logRepository.Insert(ex, systemUser, LogType.ERROR, local);

                TempData["ErrorMessage"] = TranslateHandler.LOGINERROR;
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            local.Controller = this.ControllerContext.RouteData.Values["controller"].ToString();
            local.Action = this.ControllerContext.RouteData.Values["action"].ToString();

            try
            {
                Session.Abandon();

                HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
                cookie1.Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Add(cookie1);

                HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
                cookie2.Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Add(cookie2);

                FormsAuthentication.SignOut();
            }
            catch (Exception ex)
            {
                logRepository.Insert(ex, systemUser, LogType.ERROR, local);

                return View("~/Shared/Error.cshtml");
            }

            return RedirectToAction("Login");
        }
    }
}