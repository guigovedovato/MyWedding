using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyWeddingSystem.Models.Context;
using MyWeddingSystem.Models.ViewModel;
using MyWeddingSystem.Models.Anemic;
using MyWeddingSystem.Models.Enum;

namespace MyWeddingSystem.Models.Model
{
    [Table("Log")]
    public class LogRepository : BaseRepository<LogRepository>
    {
        [Key]
        public int ID { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string InnerException { get; set; }
        public DateTime CreatedAt { get; set; }
        public int LogType { get; set; }
        [MaxLength(30)]
        public string UserName { get; set; }
        public int UserID { get; set; }
        [MaxLength(15)]
        public string Controller { get; set; }
        [MaxLength(15)]
        public string Action { get; set; }
        [MaxLength(15)]
        public string Class { get; set; }
        [MaxLength(15)]
        public string Method { get; set; }

        public LogRepository() : base(MyWeddingContext.Instance) { }

        public void Insert(Exception ex, UserView user, LogType logType, LogLocal local)
        {
            this.Message = ex.Message;
            this.StackTrace = ex.StackTrace;
            if(ex.InnerException != null)
                this.InnerException = ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : ex.InnerException.Message;
            this.CreatedAt = DateTime.Now;
            this.LogType = (int)logType;
            this.UserName = user.Login;
            this.UserID = user.ID;
            this.Controller = local.Controller;
            this.Action = local.Action;
            this.Class = local.Class;
            this.Method = local.Method;
            Insert(this);
        }

        public void Insert(UserView user, LogType logType, LogLocal local, string message)
        {
            this.CreatedAt = DateTime.Now;
            this.LogType = (int)logType;
            this.UserName = user.Login;
            this.UserID = user.ID;
            this.Controller = local.Controller;
            this.Action = local.Action;
            this.Class = local.Class;
            this.Method = local.Method;
            this.Message = message;
            Insert(this);
        }

        public List<LogRepository> GetLogByUserID(int? userID)
        {
            return dbSet.AsNoTracking().Where(x => x.UserID == userID).ToList();
        }
    }
}