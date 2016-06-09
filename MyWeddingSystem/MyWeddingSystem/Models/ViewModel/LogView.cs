using System;

namespace MyWeddingSystem.Models.ViewModel
{
    public class LogView
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string InnerException { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LogType { get; set; }
        public string UserName { get; set; }
        public int UserID { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Class { get; set; }
        public string Method { get; set; }
    }
}