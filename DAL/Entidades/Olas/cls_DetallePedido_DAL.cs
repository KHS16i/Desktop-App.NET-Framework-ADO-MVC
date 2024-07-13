using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entidades.Olas
{
    public class cls_DetallePedido_DAL
    {
        #region VARIABLES PRIVADAS

        private string _sNombreSector, _sNombre, _sIdInterno, _sSSCCC;
        private int _iIdLineaDetalleSolicitud, _iIdArticulo, _iEstado, _iCantidadAlistada, _iCantidadDispBodega;
        private double _dCantidadSolicitada, _dCantidadRestante, _dCantidadCertificada;

        private List<cls_DetallePedido_DAL> ListaDetallePedido;

        #endregion

        #region CONSTRUCTORES

        public cls_DetallePedido_DAL(IDataReader reader)
        {
            _iIdLineaDetalleSolicitud = Convert.ToInt32(reader["idLineaDetalleSolicitud"]);
            _sNombreSector = Convert.ToString(reader["NombreSector"]);
            _iIdArticulo = Convert.ToInt32(reader["idArticulo"]);
            _sNombre = Convert.ToString(reader["Nombre"]);
            _sIdInterno = Convert.ToString(reader["idInterno"]);
            _dCantidadSolicitada = Convert.ToDouble(reader["Cantidad"]);
            _iCantidadAlistada = Convert.ToInt32(reader["CantidadAlistada"]);
            _iCantidadDispBodega = Convert.ToInt32(reader["CantidadDisponible"]);
            _dCantidadCertificada = Convert.ToDouble(reader["CantidadCertificada"]);

            _dCantidadRestante = _dCantidadSolicitada - _dCantidadCertificada;
        }

        public cls_DetallePedido_DAL() { }

        #endregion

        #region GETSET

        public string sNombreSector { get => _sNombreSector; set => _sNombreSector = value; }
        public string sNombre { get => _sNombre; set => _sNombre = value; }
        public string sIdInterno { get => _sIdInterno; set => _sIdInterno = value; }
        public int iIdArticulo { get => _iIdArticulo; set => _iIdArticulo = value; }
        public int iEstado { get => _iEstado; set => _iEstado = value; }
        public int iCantidadAlistada { get => _iCantidadAlistada; set => _iCantidadAlistada = value; }
        public int iCantidadDispBodega { get => _iCantidadDispBodega; set => _iCantidadDispBodega = value; }
        public double dCantidadRestante { get => _dCantidadRestante; set => _dCantidadRestante = value; }
        public int iIdLineaDetalleSolicitud { get => _iIdLineaDetalleSolicitud; set => _iIdLineaDetalleSolicitud = value; }
        public double dCantidadSolicitada { get => _dCantidadSolicitada; set => _dCantidadSolicitada = value; }
        public double iCantidadCertificada { get => _dCantidadCertificada; set => _dCantidadCertificada = value; }
        public List<cls_DetallePedido_DAL> ListaDetallePedido1 { get => ListaDetallePedido; set => ListaDetallePedido = value; }
        public string sSSCCC { get => _sSSCCC; set => _sSSCCC = value; }

        #endregion
    }
}
