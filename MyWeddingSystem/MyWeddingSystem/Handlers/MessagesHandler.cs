namespace MyWeddingSystem.Handlers
{
    public static class MessagesHandler
    {
        #region Errors
        public static string LOGINERROR = "Erro ao fazer Login!";

        public static string LOGPAGEERROR = "Erro ao acessar página de Log!";
        public static string LOGBYIDERROR = "Erro ao consultar log {0}";
        public static string LOGBYUSERIDERROR = "Erro ao consultar log do usuário {0}";

        public static string USERPAGEERROR = "Erro ao acessar página de Usuário!";
        public static string USERBYIDERROR = "Erro ao consultar usuário {0}";
        public static string USERINSERTERROR = "Erro ao criar o usuário {0}";
        public static string USERINACTIVATEERROR = "Erro ao inativar o usuário {0}";
        public static string USERPRINT = "Erro ao imprimir!";
        public static string USERALREADYINSERTED = "Usuário já cadastrado!";

        public static string GUESTPAGEERROR = "Erro ao acessar página de Convidados!";
        public static string GUESTBYIDERROR = "Erro ao consultar convidado {0}";
        public static string GUESTINSERTERROR = "Erro ao confirmar presença!";
        #endregion

        #region Pages
        public static string LOGUINPAGE = "Login Page";
        public static string UNAUTHORIZEDPAGE = "Unauthorized Page";
        public static string HOMEPAGE = "Home Page";
        public static string GUESTPAGE = "Guest Page";
        public static string USERPAGE = "User Page";
        public static string LOGPAGE = "Log Page";
        public static string PHOTOPAGE = "Photo Page";
        public static string LOCALPAGE = "Local Page";
        #endregion

        #region Messages
        public static string LOGINANDPASS = "Digite o login e senha.";
        public static string LOGININVALID = "Login ou senha inválido!";
        
        public const string ISREQUIRED = "Esse campo é obrigatório!";

        public static string FORMINVALID = "Houve um erro ao submeter o formulário!";
        #endregion

        #region Display
        public const string GUESTQUANTITY = "Digite a quantidade de pessoas acima de 10 anos, incluindo o(s) titular(res) do convite:";
        public const string USERNAME = "Nome do Usuário:";
        public const string USERLOGIN = "Login do Usuário:";
        #endregion

        #region Front-End
        public static string BACKTOLIST = "Voltar";
        public static string PRINT = "Imprimir";

        public static string GUESTTYPE = "Lista de Convidados";
        public static string LOGTYPE = "Lista de Logs";
        public static string UserTYPE = "Lista de Usuários";
        #endregion
    }
}