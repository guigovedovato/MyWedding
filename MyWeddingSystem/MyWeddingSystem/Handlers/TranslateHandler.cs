namespace MyWeddingSystem.Handlers
{
    public static class TranslateHandler
    {
        #region Errors
        public static string LOGINERROR = System.Configuration.ConfigurationManager.AppSettings["LOGINERROR"];

        public static string LOGPAGEERROR = System.Configuration.ConfigurationManager.AppSettings["LOGPAGEERROR"];
        public static string LOGBYIDERROR = System.Configuration.ConfigurationManager.AppSettings["LOGBYIDERROR"];
        public static string LOGBYUSERIDERROR = System.Configuration.ConfigurationManager.AppSettings["LOGBYUSERIDERROR"];

        public static string USERPAGEERROR = System.Configuration.ConfigurationManager.AppSettings["USERPAGEERROR"];
        public static string USERBYIDERROR = System.Configuration.ConfigurationManager.AppSettings["USERBYIDERROR"];
        public static string USERINSERTERROR = System.Configuration.ConfigurationManager.AppSettings["USERINSERTERROR"];
        public static string USERINACTIVATEERROR = System.Configuration.ConfigurationManager.AppSettings["USERINACTIVATEERROR"];
        public static string USERPRINT = System.Configuration.ConfigurationManager.AppSettings["USERPRINT"];
        public static string USERALREADYINSERTED = System.Configuration.ConfigurationManager.AppSettings["USERALREADYINSERTED"];

        public static string GUESTPAGEERROR = System.Configuration.ConfigurationManager.AppSettings["GUESTPAGEERROR"];
        public static string GUESTBYIDERROR = System.Configuration.ConfigurationManager.AppSettings["GUESTBYIDERROR"];
        public static string GUESTINSERTERROR = System.Configuration.ConfigurationManager.AppSettings["GUESTINSERTERROR"];
        #endregion

        #region Pages
        public static string LOGUINPAGE = System.Configuration.ConfigurationManager.AppSettings["LOGUINPAGE"];
        public static string UNAUTHORIZEDPAGE = System.Configuration.ConfigurationManager.AppSettings["UNAUTHORIZEDPAGE"];
        public static string HOMEPAGE = System.Configuration.ConfigurationManager.AppSettings["HOMEPAGE"];
        public static string GUESTPAGE = System.Configuration.ConfigurationManager.AppSettings["GUESTPAGE"];
        public static string USERPAGE = System.Configuration.ConfigurationManager.AppSettings["USERPAGE"];
        public static string LOGPAGE = System.Configuration.ConfigurationManager.AppSettings["LOGPAGE"];
        public static string PHOTOPAGE = System.Configuration.ConfigurationManager.AppSettings["PHOTOPAGE"];
        public static string LOCALPAGE = System.Configuration.ConfigurationManager.AppSettings["LOCALPAGE"];
        #endregion

        #region Messages
        public static string LOGINANDPASS = System.Configuration.ConfigurationManager.AppSettings["LOGINANDPASS"];
        public static string LOGININVALID = System.Configuration.ConfigurationManager.AppSettings["LOGININVALID"];

        public static string FORMINVALID = System.Configuration.ConfigurationManager.AppSettings["FORMINVALID"];
        #endregion

        #region Display
        public static string GUESTQUANTITY = System.Configuration.ConfigurationManager.AppSettings["GUESTQUANTITY"];

        public static string ISREQUIRED = System.Configuration.ConfigurationManager.AppSettings["ISREQUIRED"];

        public static string USERNAME = System.Configuration.ConfigurationManager.AppSettings["USERNAME"];
        public static string USERLOGIN = System.Configuration.ConfigurationManager.AppSettings["USERLOGIN"];
        public static string CONFIRMED = System.Configuration.ConfigurationManager.AppSettings["CONFIRMED"];
        public static string QUANTITY = System.Configuration.ConfigurationManager.AppSettings["QUANTITY"];
        #endregion

        #region Front-End
        public static string BACKTOLIST = System.Configuration.ConfigurationManager.AppSettings["BACKTOLIST"];
        public static string PRINT = System.Configuration.ConfigurationManager.AppSettings["PRINT"];
        public static string NEW = System.Configuration.ConfigurationManager.AppSettings["NEW"];
        public static string REGISTER = System.Configuration.ConfigurationManager.AppSettings["REGISTER"];
        public static string ENTER = System.Configuration.ConfigurationManager.AppSettings["ENTER"];
        public static string SEARCH = System.Configuration.ConfigurationManager.AppSettings["SEARCH"];
        public static string CONFIRM = System.Configuration.ConfigurationManager.AppSettings["CONFIRM"];

        public static string GUESTTYPE = System.Configuration.ConfigurationManager.AppSettings["GUESTTYPE"];
        public static string LOGTYPE = System.Configuration.ConfigurationManager.AppSettings["LOGTYPE"];
        public static string USERTYPE = System.Configuration.ConfigurationManager.AppSettings["USERTYPE"];
        #endregion

        #region Menu
        public static string HOME = System.Configuration.ConfigurationManager.AppSettings["HOME"];
        public static string GUESTLIST = System.Configuration.ConfigurationManager.AppSettings["GUESTLIST"];
        public static string ATTENDANCE = System.Configuration.ConfigurationManager.AppSettings["ATTENDANCE"];
        public static string PHOTO = System.Configuration.ConfigurationManager.AppSettings["PHOTO"];
        public static string LOCAL = System.Configuration.ConfigurationManager.AppSettings["LOCAL"];
        public static string USER = System.Configuration.ConfigurationManager.AppSettings["USER"];
        public static string LOG = System.Configuration.ConfigurationManager.AppSettings["LOG"];
        public static string LOGOUT = System.Configuration.ConfigurationManager.AppSettings["LOGOUT"];
        public static string GIFT = System.Configuration.ConfigurationManager.AppSettings["GIFT"];
        #endregion
    }
}