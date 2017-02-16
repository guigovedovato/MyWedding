using MyWeddingSystem.App_Start;
using MyWeddingSystem.Attributes;
using System.Web.Mvc;
using MyWeddingSystem.Models.Model;
using MyWeddingSystem.Models.ViewModel;
using AutoMapper;
using MyWeddingSystem.Models.Anemic;

namespace MyWeddingSystem.Controllers
{
    [SessionExpire]
    public class BaseController : Controller
    {
        protected ISession userSession;
        protected LogRepository logRepository;
        protected UserView systemUser;
        protected MapperConfiguration config;
        protected IMapper mapper;
        protected LogLocal local;

        public BaseController()
        {
            userSession = new LoggedUserSession();
            logRepository = new LogRepository();
            systemUser = new UserView() { ID = 0, Login = "System" };
            local = new LogLocal();
        }
    }
}