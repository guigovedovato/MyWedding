using MyWeddingSystem.Models.ViewModel;
using System.Web;

namespace MyWeddingSystem.App_Start
{
    public class LoggedUserSession : ISession
    {
        private T Get<T>(string key)
        {
            return (T)HttpContext.Current.Session[key];
        }

        private void Set(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public UserView LoggedUser
        {
            get { return Get<UserView>("AuthUser"); }
            set { Set("AuthUser", value); }
        }
    }
}