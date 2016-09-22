using System;
using System.ComponentModel.DataAnnotations;

namespace MyWeddingSystem.Models.ViewModel
{
    [Serializable()]
    public class GuestView : BaseView
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        public int? Quantity { get; set; }
    }
}