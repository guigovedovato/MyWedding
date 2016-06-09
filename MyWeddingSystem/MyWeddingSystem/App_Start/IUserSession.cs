using MyWeddingSystem.Models.ViewModel;

namespace MyWeddingSystem.App_Start
{
    public interface IUserSession
    {
        UserView AuthUser { get; set; }
    }
}