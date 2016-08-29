using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyWeddingSystem.Models.Context;
using MyWeddingSystem.Models.Model.Anemic;

namespace MyWeddingSystem.Models.Model
{
    [Table("User")]
    public class UserRepository : BaseRepository<UserRepository>
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(15)]
        public string Login { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(40)]
        public string Password { get; set; }
        public int Profile { get; set; }
        [MaxLength(8)]
        public string FirstPassword { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public UserRepository() : base(MyWeddingContext.Instance) { }

        public string[] GetRolesByLogin(string login)
        {
            UserProfile profile = (UserProfile)dbSet.AsNoTracking().Where(x => x.Login.Equals(login)).Select(x => x.Profile).FirstOrDefault();
            return new string[] { profile.ToString() };
        }

        public UserRepository Logon(string login, string password)
        {
            return dbSet.AsNoTracking().FirstOrDefault(x => x.Login.Equals(login) && x.Password.Equals(password));
        }

        public UserRepository Insert()
        {
            return Insert(this);
        }

        public bool IsInserted(string userLogin)
        {
            return dbSet.AsNoTracking().FirstOrDefault(x => x.Login.Equals(userLogin)) != null ? true : false;
        }
    }
}