using System;
using DAL.Entidades;
using BLL.Utilidades;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace BLL.AccesoDatos
{
    public class cls_Login_BLL
    {
        private readonly string _connectionString;

        public cls_Login_BLL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void logIn(ref cls_Login_DAL Obj_Login_DAL)
        {
            cls_EncriptarMD5 MD5 = new cls_EncriptarMD5();

            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(ConfigurationManager.AppSettings["ValidaLogin"].ToString(), cnx))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Usuario", Obj_Login_DAL.sUsuario));
                    Obj_Login_DAL.sConstrasenna = MD5.MD5Hash(Obj_Login_DAL.sConstrasenna);
                    cmd.Parameters.Add(new SqlParameter("@Contrasenna", Obj_Login_DAL.sConstrasenna));

                    SqlParameter outputParam = new SqlParameter("@RES", SqlDbType.Bit);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

                    try
                    {
                        cnx.Open();
                        cmd.ExecuteNonQuery();

                        cls_Login_DAL.bResLogin = Convert.ToBoolean(outputParam.Value);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cls_Login_DAL.iIdUsuario = Convert.ToInt32(reader["IDUSUARIO"]);
                                cls_Login_DAL.shIdBodega = Convert.ToInt16(reader["idBodega"]);
                                cls_Login_DAL.sNombre = Convert.ToString(reader["NOMBRE_PILA"]);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }
    }
}

