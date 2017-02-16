using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MyWeddingSystem.Handlers;
using MyWeddingSystem.Attributes;
using MyWeddingSystem.Models.Model;
using AutoMapper;
using MyWeddingSystem.Models.ViewModel;
using MyWeddingSystem.Models.Enum;

namespace MyWeddingSystem.Controllers
{
    [AccessDenied(Roles = "ADM")]
    public class LogController : BaseController
    {
        public LogController()
        {
            config = new MapperConfiguration(cfg => {
                cfg.CreateMap<LogRepository, LogView>().AfterMap((re, dist) =>
                {
                    LogType type = (LogType)re.LogType;
                    dist.LogType = type.ToString();
                });
            });
            mapper = config.CreateMapper();
        }

        // GET: Log
        public ActionResult Index()
        {
            ViewBag.Title = TranslateHandler.LOGPAGE;
            ViewBag.Type = TranslateHandler.LOGTYPE;
            ViewBag.Back = TranslateHandler.BACKTOLIST;
            ViewBag.Search = TranslateHandler.SEARCH;
            ViewBag.Index = true;

            local.Controller = this.ControllerContext.RouteData.Values["controller"].ToString();
            local.Action = this.ControllerContext.RouteData.Values["action"].ToString();

            try
            { 
                var model = mapper.Map<List<LogRepository>, List<LogView>>(logRepository.GetAll());
                return View(model);
            }
            catch(Exception ex)
            {
                logRepository.Insert(ex, userSession.LoggedUser, LogType.ERROR, local);

                var model = new LogView() { Message = TranslateHandler.LOGPAGEERROR };
                var listModel = new List<LogView>();
                listModel.Add(model);
                return View(listModel);
            }
        }

        // GET: LogByID
        public ActionResult LogByID(int id)
        {
            ViewBag.Title = TranslateHandler.LOGPAGE;
            ViewBag.Back = TranslateHandler.BACKTOLIST;

            local.Controller = this.ControllerContext.RouteData.Values["controller"].ToString();
            local.Action = this.ControllerContext.RouteData.Values["action"].ToString();

            try
            {
                var model = mapper.Map<LogRepository, LogView>(logRepository.Get(id));
                return View("LogDetail", model);
            }
            catch (Exception ex)
            {
                logRepository.Insert(ex, userSession.LoggedUser, LogType.ERROR, local);

                TempData["ErrorMessage"] = string.Format(TranslateHandler.LOGBYIDERROR, id);
                var model = new LogView() { Message = TempData["ErrorMessage"].ToString() };
                return View("LogDetail", model);
            }
            }

        // GET: LogByUserID
        public ActionResult LogByUserID(int? logUserID)
        {
            if (!logUserID.HasValue)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Title = TranslateHandler.LOGPAGE;
            ViewBag.Type = TranslateHandler.LOGTYPE;
            ViewBag.Back = TranslateHandler.BACKTOLIST;
            ViewBag.Index = false;

            local.Controller = this.ControllerContext.RouteData.Values["controller"].ToString();
            local.Action = this.ControllerContext.RouteData.Values["action"].ToString();

            try
            {
                var model = mapper.Map<List<LogRepository>, List<LogView>>(logRepository.GetLogByUserID(logUserID));
                return View("Index", model);
            }
            catch (Exception ex)
            {
                logRepository.Insert(ex, userSession.LoggedUser, LogType.ERROR, local);

                var model = new LogView() { Message = string.Format(TranslateHandler.LOGBYUSERIDERROR, logUserID) };
                var listModel = new List<LogView>();
                listModel.Add(model);
                return View(listModel);
            }
        }
    }
}