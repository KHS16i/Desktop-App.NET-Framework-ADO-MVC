
namespace DAL.Entidades
{
    public class cls_Login_DAL
    {
        #region VARIABLES PRIVADAS

        private string _sUsuario, _sConstrasenna;
        public static string sNombre;
        public static short shIdBodega;
        public static int iIdUsuario;
        public static bool bResLogin;

        #endregion

        #region GETSET

        public string sUsuario { get => _sUsuario; set => _sUsuario = value; }
        public string sConstrasenna { get => _sConstrasenna; set => _sConstrasenna = value; }

        #endregion

    }
}
