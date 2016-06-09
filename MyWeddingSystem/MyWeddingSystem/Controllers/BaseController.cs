using MyWeddingSystem.App_Start;
using MyWeddingSystem.Attributes;
using System.Web.Mvc;
using MyWeddingSystem.Models.Model;
using MyWeddingSystem.Models.ViewModel;
using AutoMapper;
using MyWeddingSystem.Models.Model.Anemic;

namespace MyWeddingSystem.Controllers
{
    [SessionExpireFilter]
    public class BaseController : Controller
    {
        protected IUserSession userSession;
        protected LogRepository logRepository;
        protected UserView systemUser;
        protected MapperConfiguration config;
        protected IMapper mapper;
        protected LogLocal local;

        public BaseController()
        {
            userSession = new HttpContextUserSession();
            logRepository = new LogRepository();
            systemUser = new UserView() { ID = 0, Login = "System" };
            local = new LogLocal();
        }
    }
}