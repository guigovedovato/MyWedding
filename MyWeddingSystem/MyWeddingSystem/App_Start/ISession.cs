using MyWeddingSystem.Models.ViewModel;

namespace MyWeddingSystem.App_Start
{
    public interface ISession
    {
        UserView LoggedUser { get; set; }
    }
}