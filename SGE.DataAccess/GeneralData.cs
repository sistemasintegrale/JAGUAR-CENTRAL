using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SGE.DataAccess
{
    public class GeneralData
    {
        #region Tabla Registro
        /*Los ID´s de los Tipos de Tabla se pueden ver en la Clase Parámetros*/
        public List<ETablaRegistro> listarTablaRegistro(int intTipoTabla)
        {
            List<ETablaRegistro> lista = new List<ETablaRegistro>();
            try
            {
                using (GeneralDataContext dc = new GeneralDataContext(Helper.conexion()))
                {
                    var query = dc.SGEG_TABLA_REGISTRO_POR_TIPO_TABLA(intTipoTabla);
                    foreach (var item in query)
                    {
                        lista.Add(new ETablaRegistro()
                        {
                            tarec_iid_tabla_registro = item.tarec_iid_tabla_registro,
                            tarec_icorrelativo_registro = Convert.ToInt32(item.tarec_icorrelativo_registro),
                            tarec_vdescripcion = item.tarec_vdescripcion,
                            tarec_vtipo_operacion = item.tarec_vtipo_operacion,
                            tarec_cestado = Convert.ToChar(item.tarec_cestado),
                            strEstado = (Convert.ToChar(item.tarec_cestado) == 'A') ? "Activo" : "Inactivo",
                            tarec_iid_tabla_registro_ingreso = Convert.ToInt32(item.tarec_iid_tabla_registro_ingreso),
                            tarec_iid_tabla_registro_salida = Convert.ToInt32(item.tarec_iid_tabla_registro_salida)

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        /*-------------------------------------------------------*/
        #endregion

        #region Tipo de Cambio Mensual
        public List<ETipoCambioMensual> ListarTipoCambioMensual()
        {
            List<ETipoCambioMensual> lista = null;
            try
            {
                using (GeneralDataContext dc = new GeneralDataContext(Helper.conexion()))
                {
                    lista = new List<ETipoCambioMensual>();
                    var query = dc.SGET_TIPO_CAMBIO_MENSUAL_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ETipoCambioMensual()
                        {
                            tcamm_icod_tcam_mensual = item.tcamm_icod_tcam_mensual,
                            tcamm_iid_anio = Convert.ToInt32(item.tcamm_iid_anio),
                            mesec_iid_mes = Convert.ToInt32(item.mesec_iid_mes),
                            vdes_mesec_iid_mes = item.vdes_mesec_iid_mes,
                            tcamm_ntipo_cambio_compra = Convert.ToDecimal(item.tcamm_ntipo_cambio_compra),
                            tcamm_ntipo_cambio_venta = Convert.ToDecimal(item.tcamm_ntipo_cambio_venta)
                        });
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public void InsertarTipoCambioMensual(ETipoCambioMensual obj)
        {
            try
            {
                using (GeneralDataContext dc = new GeneralDataContext(Helper.conexion()))
                {
                    dc.SGET_TIPO_CAMBIO_MENSUAL_INSERTAR(
                    obj.tcamm_iid_anio,
                    obj.mesec_iid_mes,
                    obj.tcamm_ntipo_cambio_compra,
                    obj.tcamm_ntipo_cambio_venta,
                    obj.intUsuario,
                    obj.strPc
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Equipo_Ingresar(string strNombreEquipo, string strIdCpu)
        {
            try
            {

                using (SqlConnection cn = new SqlConnection(Helper.conexion()))
                {
                    string query = $"INSERT INTO SGE_CONTROL_EQUIPOS(ceq_vnombre_equipo,cep_vid_cpu)VALUES('{strNombreEquipo}','{strIdCpu}')";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void EliminarTipoCambioMensual(ETipoCambioMensual obj)
        {
            try
            {
                using (GeneralDataContext dc = new GeneralDataContext(Helper.conexion()))
                {
                    dc.SGET_TIPO_CAMBIO_MENSUAL_ELIMINAR(
                    obj.tcamm_icod_tcam_mensual
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarTipoCambioMensual(ETipoCambioMensual obj)
        {
            try
            {
                using (GeneralDataContext dc = new GeneralDataContext(Helper.conexion()))
                {
                    dc.SGET_TIPO_CAMBIO_MENSUAL_ACTUALIZAR(
                    obj.tcamm_iid_anio,
                    obj.mesec_iid_mes,
                    obj.tcamm_ntipo_cambio_compra,
                    obj.tcamm_ntipo_cambio_venta,
                     obj.intUsuario,
                    obj.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion





    }
}
