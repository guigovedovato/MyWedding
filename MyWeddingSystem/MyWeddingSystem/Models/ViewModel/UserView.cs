using MyWeddingSystem.Handlers;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyWeddingSystem.Models.ViewModel
{
    public class UserView : BaseView
    {
        public int ID { get; set; }
        [MaxLength(15)]
        [Required(ErrorMessage = MessagesHandler.ISREQUIRED)]
        [DisplayName(MessagesHandler.USERLOGIN)]
        public string Login { get; set; }
        [Required(ErrorMessage = MessagesHandler.ISREQUIRED)]
        [DisplayName(MessagesHandler.USERNAME)]
        public string Name { get; set; }
        public string Password { get; set; }
        public int Profile { get; set; }
        public string FirstPassword { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Confirmded { get; set; }
    }
}