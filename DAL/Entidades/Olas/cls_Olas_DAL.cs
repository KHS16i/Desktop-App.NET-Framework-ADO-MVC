using System;
using System.Collections.Generic;
using System.Data;

namespace DAL.Entidades
{
    public class cls_Olas_DAL
    {
        #region VARIABLES PRIVADAS

        private int _iMaestroSolicitud;
        private string _sNombreOla, _sDestinoOla, _sComentarios, _sPrioridad, _sPorcentajeAlisto, _sPorcentajeAsignado;
        private DateTime _dtFechaEntrega, _dtFechaCreacion;
        private bool _bCertificado;
        public static bool _bSsccUbicado;

        private List<cls_Olas_DAL> listOlas;

        #endregion

        #region CONSTRUCTORES

        public cls_Olas_DAL(IDataReader reader)
        {
            _iMaestroSolicitud = Convert.ToInt32(reader["idMaestroSolicitud"]);
            _dtFechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]);
            _sNombreOla = Convert.ToString(reader["Nombre"]);
            _sComentarios = Convert.ToString(reader["Comentarios"]);
            _dtFechaEntrega = Convert.ToDateTime(reader["FechaEntrega"]);
            _sPorcentajeAlisto = Convert.ToString(reader["PorcentajeAlistado"]);
            _sPrioridad = Convert.ToString(reader["PrioridadDescripcion"]);
            _sDestinoOla = Convert.ToString(reader["DestinoNombre"]);
            _bCertificado = Convert.ToBoolean(reader["certificado"]);
            _bSsccUbicado = Convert.ToBoolean(reader["SsccUbicados"]);
        }

        public cls_Olas_DAL() { }

        #endregion

        #region GETSET

        public int iMaestroSolicitud { get => _iMaestroSolicitud; set => _iMaestroSolicitud = value; }
        public string sNombreOla { get => _sNombreOla; set => _sNombreOla = value; }
        public string sDestinoOla { get => _sDestinoOla; set => _sDestinoOla = value; }
        public string sComentarios { get => _sComentarios; set => _sComentarios = value; }
        public string sPrioridad { get => _sPrioridad; set => _sPrioridad = value; }
        public DateTime dtFechaEntrega { get => _dtFechaEntrega; set => _dtFechaEntrega = value; }
        public DateTime dtFechaCreacion { get => _dtFechaCreacion; set => _dtFechaCreacion = value; }
        public string sPorcentajeAlisto { get => _sPorcentajeAlisto; set => _sPorcentajeAlisto = value; }
        public string sPorcentajeAsignado { get => _sPorcentajeAsignado; set => _sPorcentajeAsignado = value; }
        public List<cls_Olas_DAL> ListOlas { get => listOlas; set => listOlas = value; }
        public bool bCertificado { get => _bCertificado; set => _bCertificado = value; }
        #endregion
    }
}
