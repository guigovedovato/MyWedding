using MyWeddingSystem.Models.ViewModel;
using System.Web;

namespace MyWeddingSystem.App_Start
{
    public class HttpContextUserSession : IUserSession
    {
        private T GetFromSession<T>(string key)
        {
            return (T)HttpContext.Current.Session[key];
        }

        private void SetInSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public UserView AuthUser
        {
            get { return GetFromSession<UserView>("AuthUser"); }
            set { SetInSession("AuthUser", value); }
        }
    }
}