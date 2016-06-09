using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MyWeddingSystem.Handlers;
using MyWeddingSystem.Attributes;
using MyWeddingSystem.Models.Model.Anemic;
using MyWeddingSystem.Models.Model;
using AutoMapper;
using MyWeddingSystem.Models.ViewModel;

namespace MyWeddingSystem.Controllers
{
    [AccessDeniedAuthorize(Roles = "ADM")]
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
            ViewBag.Title = MessagesHandler.LOGPAGE;
            ViewBag.Type = MessagesHandler.LOGTYPE;
            ViewBag.Back = MessagesHandler.BACKTOLIST;
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
                logRepository.Insert(ex, userSession.AuthUser, LogType.ERROR, local);

                var model = new LogView() { Message = MessagesHandler.LOGPAGEERROR };
                var listModel = new List<LogView>();
                listModel.Add(model);
                return View(listModel);
            }
        }

        // GET: LogByID
        public ActionResult LogByID(int id)
        {
            ViewBag.Title = MessagesHandler.LOGPAGE;
            ViewBag.Back = MessagesHandler.BACKTOLIST;

            local.Controller = this.ControllerContext.RouteData.Values["controller"].ToString();
            local.Action = this.ControllerContext.RouteData.Values["action"].ToString();

            try
            {
                var model = mapper.Map<LogRepository, LogView>(logRepository.Get(id));
                return View("LogDetail", model);
            }
            catch (Exception ex)
            {
                logRepository.Insert(ex, userSession.AuthUser, LogType.ERROR, local);

                TempData["ErrorMessage"] = string.Format(MessagesHandler.LOGBYIDERROR, id);
                var model = new LogView() { Message = TempData["ErrorMessage"].ToString() };
                return View("LogDetail", model);
            }
            }

        // GET: LogByUserID
        public ActionResult LogByUserID(int logUserID)
        {
            ViewBag.Title = MessagesHandler.LOGPAGE;
            ViewBag.Type = MessagesHandler.LOGTYPE;
            ViewBag.Back = MessagesHandler.BACKTOLIST;
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
                logRepository.Insert(ex, userSession.AuthUser, LogType.ERROR, local);

                var model = new LogView() { Message = string.Format(MessagesHandler.LOGBYUSERIDERROR, logUserID) };
                var listModel = new List<LogView>();
                listModel.Add(model);
                return View(listModel);
            }
        }
    }
}