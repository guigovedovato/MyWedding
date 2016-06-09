using MyWeddingSystem.Handlers;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyWeddingSystem.Models.ViewModel
{
    public class GuestView : BaseView
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required(ErrorMessage = MessagesHandler.ISREQUIRED)]
        [DisplayName(MessagesHandler.GUESTQUANTITY)]
        public int? Quantity { get; set; }
    }
}