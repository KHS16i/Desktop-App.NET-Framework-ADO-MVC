using System;
using System.Collections.Generic;
using DAL.Entidades;
using DAL.Entidades.Olas;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace BLL.AccesoDatos
{
    public class cls_Olas_BLL
    {
        private readonly string _connectionString; // <-- Variable para inyeccion de dependencias

        public cls_Olas_BLL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<cls_Olas_DAL> GetSSCCProducts(ref cls_EConsultaOla Obj_Consulta_DAL)
        {
            List<cls_Olas_DAL> ListaOlas = new List<cls_Olas_DAL>();

            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(ConfigurationManager.AppSettings["getOlasSSCC"].ToString(), cnx))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@consecutivoSSCC", Obj_Consulta_DAL.sSSCC));

                    try
                    {
                        cnx.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ListaOlas.Add(new cls_Olas_DAL(reader));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            return ListaOlas;
        }


        public List<cls_Olas_DAL> Obtener_Ecabezado_Olas_a_Certificar(ref cls_EConsultaOla Obj_Consulta_DAL)
        {
            List<cls_Olas_DAL> ListaOlas = new List<cls_Olas_DAL>();

            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(ConfigurationManager.AppSettings["getOlasCertificar"].ToString(), cnx))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@idInternoOrder", Obj_Consulta_DAL.sIdInterno));
                    cmd.Parameters.Add(new SqlParameter("@dateInit", Obj_Consulta_DAL.sFechaInicio));
                    cmd.Parameters.Add(new SqlParameter("@dateEnd", Obj_Consulta_DAL.sFechaFinal));
                    cmd.Parameters.Add(new SqlParameter("@idWareHouse", cls_Login_DAL.shIdBodega));
                    cmd.Parameters.Add(new SqlParameter("@IdUsuarioAsigando", cls_Login_DAL.iIdUsuario));

                    try
                    {
                        cnx.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ListaOlas.Add(new cls_Olas_DAL(reader));
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            return ListaOlas;
        }


        public List<cls_Pedidos_DAL> Obtener_PedidosOla(ref cls_Olas_DAL ObjOla)
        {
            List<cls_Pedidos_DAL> ListaPedidos = new List<cls_Pedidos_DAL>();

            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(ConfigurationManager.AppSettings["getPedidosOla"].ToString(), cnx))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@NumeroOla", ObjOla.iMaestroSolicitud));
                    cmd.Parameters.Add(new SqlParameter("@idBodega", cls_Login_DAL.shIdBodega));

                    try
                    {
                        cnx.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ListaPedidos.Add(new cls_Pedidos_DAL(reader));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            return ListaPedidos;
        }


        public List<cls_DetallePedido_DAL> Obtener_DetallePedido(ref cls_Pedidos_DAL ObjPedidoOLa)
        {
            List<cls_DetallePedido_DAL> ListaDetallePedido = new List<cls_DetallePedido_DAL>();

            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(ConfigurationManager.AppSettings["getDetallePedido"].ToString(), cnx))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@idMaestroOrdenSalida", ObjPedidoOLa.iNumOla));
                    cmd.Parameters.Add(new SqlParameter("@idBodega", cls_Login_DAL.shIdBodega));

                    try
                    {
                        cnx.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ListaDetallePedido.Add(new cls_DetallePedido_DAL(reader));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            return ListaDetallePedido;
        }


        public string procesarLineaPedido(ref cls_DetallePedido_DAL Obj_DetPedidos_DAL)
        {
            string res = string.Empty;

            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(ConfigurationManager.AppSettings["certLineaSSCC"].ToString(), cnx))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@consecutivoSSCC", Obj_DetPedidos_DAL.sSSCCC));
                    cmd.Parameters.Add(new SqlParameter("@idArticulo", Obj_DetPedidos_DAL.iIdArticulo));
                    cmd.Parameters.Add(new SqlParameter("@Lote", "NA")); //Quemado porque lo solicita el SP pero no hace nada con él
                    cmd.Parameters.Add(new SqlParameter("@FechaVencimiento", "1900-01-01")); //Quemado porque lo solicita el SP pero no hace nada con él
                    cmd.Parameters.Add(new SqlParameter("@cantidadCertificar", Obj_DetPedidos_DAL.dCantidadSolicitada));
                    cmd.Parameters.Add(new SqlParameter("@cantidadCertificada", Obj_DetPedidos_DAL.iCantidadCertificada));
                    cmd.Parameters.Add(new SqlParameter("@diferenciaCertificacion", Obj_DetPedidos_DAL.dCantidadRestante));
                    cmd.Parameters.Add(new SqlParameter("@idLineaDetalleSolicitud", Obj_DetPedidos_DAL.iIdLineaDetalleSolicitud));

                    try
                    {
                        cnx.Open();
                        cmd.ExecuteNonQuery();

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res = reader["Resultado"].ToString();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            return res;
        }

        public void certificarPedido(int idBodega, int idPedido, string SSCC, int idArticulo)
        {
            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("", cnx))
                {
                    cmd.CommandType = CommandType.Text;

                    StringBuilder query = new StringBuilder();

                    query.AppendLine("DECLARE @idConsecutivoSSCC bigint");
                    query.AppendLine("SELECT @idConsecutivoSSCC = idConsecutivoSSCC FROM ADMConsecutivosSSCC WHERE SSCCGenerado = " + SSCC + ";");

                    query.AppendLine("IF  EXISTS (");
                    query.AppendLine("    SELECT idArticulo");
                    query.AppendLine("    FROM DetalleCertificacionSSCC");
                    query.AppendLine("    WHERE idConsecutivoSSCC = @idConsecutivoSSCC");
                    query.AppendLine("    AND idArticulo = " + idArticulo);
                    query.AppendLine(")");
                    query.AppendLine("BEGIN");
                    query.AppendLine("    UPDATE OPESALPreMaestroSolicitud");
                    query.AppendLine("    SET PedidoCert = 1"); //Le indica al Pedido que está certificado
                    query.AppendLine("    WHERE idBodega = " + idBodega);
                    query.AppendLine("    AND idMaestroSolicitud = " + idPedido);
                    query.AppendLine("END");


                    cmd.CommandText = query.ToString();

                    try
                    {
                        cnx.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }

        public string certificarOla(int idUsuario, int idMaestroSolicitud)
        {
            string res = string.Empty;

            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(ConfigurationManager.AppSettings["certificarOla"].ToString(), cnx))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@idMaestroSolicitud", idMaestroSolicitud));
                    cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));

                    try
                    {
                        cnx.Open();
                        cmd.ExecuteNonQuery();

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                res = reader["Resultado"].ToString();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
            return res;
        }
    }
}
