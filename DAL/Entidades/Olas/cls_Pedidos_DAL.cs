using System;
using System.Collections.Generic;
using System.Data;

namespace DAL.Entidades.Olas
{
    public class cls_Pedidos_DAL
    {
        #region VARIABLES PRIVADAS

        private int _iNumOla;
        private bool _bProcesada, _bPedidoCert;
        private string _sNombrePedido, _sComentarios, _sDestino, _sBodega, _sPrioridad, _sDireccion;
        private DateTime _dtFechaCreacion, _dtFechaProcesamiento;
        private short _shEstado;

        private List<cls_Pedidos_DAL> listPedidos;

        #endregion

        #region CONSTRUCTORES

        public cls_Pedidos_DAL(IDataReader reader)
        {
            _iNumOla = Convert.ToInt32(reader["Numero Ola"]);
            _sNombrePedido = Convert.ToString(reader["Pedido"]);
            _sComentarios = Convert.ToString(reader["Comentarios"]);
            _sDestino = Convert.ToString(reader["Destino"]);
            _sBodega = Convert.ToString(reader["Bodega"]);
            _sPrioridad = Convert.ToString(reader["Prioridad"]);
            _bPedidoCert = Convert.ToBoolean(reader["Pedido Certificado"]);
            _bProcesada = Convert.ToBoolean(reader["Procesada"]);
            _dtFechaCreacion = Convert.ToDateTime(reader["Fecha de creacion"]);
            _dtFechaProcesamiento = Convert.ToDateTime(reader["Fecha de Procesamiento"]);
            _sDireccion = Convert.ToString(reader["Direccion"]);
            _shEstado = Convert.ToInt16(reader["estado"]);
        }

        public cls_Pedidos_DAL() { }

        #endregion

        #region GETSET

        public int iNumOla { get => _iNumOla; set => _iNumOla = value; }
        public string sNombrePedido { get => _sNombrePedido; set => _sNombrePedido = value; }
        public string sComentarios { get => _sComentarios; set => _sComentarios = value; }
        public string sDestino { get => _sDestino; set => _sDestino = value; }
        public string sBodega { get => _sBodega; set => _sBodega = value; }
        public string sPrioridad { get => _sPrioridad; set => _sPrioridad = value; }
        public string sDireccion { get => _sDireccion; set => _sDireccion = value; }
        public DateTime dtFechaCreacion { get => _dtFechaCreacion; set => _dtFechaCreacion = value; }
        public DateTime dtFechaProcesamiento { get => _dtFechaProcesamiento; set => _dtFechaProcesamiento = value; }
        public short shEstado { get => _shEstado; set => _shEstado = value; }
        public bool bProcesada { get => _bProcesada; set => _bProcesada = value; }
        public bool bPedidoCert { get => _bPedidoCert; set => _bPedidoCert = value; }
        public List<cls_Pedidos_DAL> ListPedidos { get => listPedidos; set => listPedidos = value; }

        #endregion
    }
}
