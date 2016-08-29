using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyWeddingSystem.Models.Context;

namespace MyWeddingSystem.Models.Model
{
    [Table("Guest")]
    public class GuestRepository : BaseRepository<GuestRepository>
    {
        [Key]
        public int ID { get; set; }
        [Index]
        public int UserID { get; set; }
        [MaxLength(30)]
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("UserID")]
        public virtual UserRepository UserRepository { get; set; }

        public GuestRepository() : base(MyWeddingContext.Instance) { }

        public GuestRepository Insert()
        {
            return Insert(this);
        }

        public GuestRepository GetGuestByUserId(int userID)
        {
            return dbSet.AsNoTracking().Where(x => x.UserID == userID).OrderByDescending(c => c.CreatedAt).FirstOrDefault();
        }
    }
}