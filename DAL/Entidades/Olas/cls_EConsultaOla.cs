using System;

namespace DAL.Entidades
{
    public class cls_EConsultaOla
    {
        #region VARIABLES PRIVADAS

        private string _sIdInterno, _sSSCC;
        DateTime _sFechaInicio, _sFechaFinal;

        #endregion

        #region GETSET

        public string sIdInterno { get => _sIdInterno; set => _sIdInterno = value; }
        public DateTime sFechaInicio { get => _sFechaInicio; set => _sFechaInicio = value; }
        public DateTime sFechaFinal { get => _sFechaFinal; set => _sFechaFinal = value; }
        public string sSSCC { get => _sSSCC; set => _sSSCC = value; }

        #endregion
    }
}
