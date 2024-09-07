using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.DataAccess
{
    public class OperacionesData
    {
        #region Personal
        public List<EVendedor> listarVendedor()
        {
            List<EVendedor> lista = new List<EVendedor>();
            try
            {
                using (OperacionesDataContext dc = new OperacionesDataContext(Helper.conexion()))
                {
                    lista = new List<EVendedor>();
                    var query = dc.SGEV_VENDEDORES_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EVendedor()
                        {
                            vdrsc_icod_vendedor = Convert.ToInt32(item.vdrsc_icod_vendedor),
                            vdrsc_vcod_vendedor = item.vdrsc_vcod_vendedor.ToString(),
                            vdrsc_vnombre_vendedor = item.vdrsc_vnombre_vendedor,
                            vdrsc_vdireccion = item.vdrsc_vdireccion,
                            vdrsc_vnumero_telefono = item.vdrsc_vnumero_telefono,
                            vdrsc_vnumero_dni = item.vdrsc_vnumero_dni,
                            vdrsc_vcorreo = item.vdrsc_vcorreo,
                            vdrsc_iactivo = Convert.ToBoolean(item.vdrsc_iactivo),
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
        public int insertarVendedor(EVendedor oBe)
        {
            int? intIcod = 0;
            try
            {
                using (OperacionesDataContext dc = new OperacionesDataContext(Helper.conexion()))
                {

                    dc.SGEV_VENDEDORES_INSERTAR(
                    ref intIcod,
                    oBe.vdrsc_vcod_vendedor,
                    oBe.vdrsc_vnombre_vendedor,
                    oBe.vdrsc_vdireccion,
                    oBe.vdrsc_vnumero_telefono,
                    oBe.vdrsc_vnumero_dni,
                    oBe.vdrsc_vcorreo,
                    oBe.intUsuario
                    );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarVendedor(EVendedor oBe)
        {
            try
            {
                using (OperacionesDataContext dc = new OperacionesDataContext(Helper.conexion()))
                {
                    dc.SGEV_VENDEDORES_MODIFICAR(
                    oBe.vdrsc_icod_vendedor,
                    oBe.vdrsc_vcod_vendedor,
                    oBe.vdrsc_vnombre_vendedor,
                    oBe.vdrsc_vdireccion,
                    oBe.vdrsc_vnumero_telefono,
                    oBe.vdrsc_vnumero_dni,
                    oBe.vdrsc_vcorreo,
                    oBe.intUsuario
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarVendedor(EVendedor oBe)
        {
            try
            {
                using (OperacionesDataContext dc = new OperacionesDataContext(Helper.conexion()))
                {
                    dc.SGEV_VENDEDORES_ELIMINAR(
                        oBe.vdrsc_icod_vendedor,
                        oBe.intUsuario
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Proforma Cab
        public List<EProforma> listarProforma(int intEjercicio)
        {
            List<EProforma> lista = new List<EProforma>();

            try
            {
                using (OperacionesDataContext dc = new OperacionesDataContext(Helper.conexion()))
                {
                    lista = new List<EProforma>();
                    var query = dc.SGE_EXTEND_PROFORMA_LISTAR(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EProforma()
                        {
                            prfc_icod_proforma = item.prfc_icod_proforma,
                            prfc_vnumero_proforma = item.prfc_vnumero_proforma,
                            prfc_sfecha_proforma = item.prfc_sfecha_proforma,
                            prfc_iid_orden_trabajo = item.prfc_iid_orden_trabajo,
                            prfc_vnumero_orden_trabajo = item.prfc_vnumero_orden_trabajo,
                            prfc_icod_cliente = Convert.ToInt32(item.prfc_icod_cliente),
                            prfc_iid_vehiculo = Convert.ToInt32(item.prfc_iid_vehiculo),
                            prfc_vkilometraje = item.prfc_vkilometraje,
                            prfc_vcolor = item.prfc_vcolor,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            prfc_nmonto_neto = item.prfc_nmonto_neto,
                            prfc_npor_imp_igv = Convert.ToDecimal(item.prfc_npor_imp_igv),
                            prfc_nmonto_igv = Convert.ToDecimal(item.prfc_nmonto_igv),
                            prfc_nmonto_total = item.prfc_nmonto_total,
                            prfc_nmonto_descuento = Convert.ToDecimal(item.prfc_nmonto_descuento),
                            prfc_iid_situacion_proforma = Convert.ToInt32(item.prfc_iid_situacion_proforma),
                            strDesCliente = item.strDesCliente,
                            strPlaca = item.strPlaca,
                            strMarca = item.strMarca,
                            strModelo = item.strModelo,
                            strSituacion = item.strSituacion,
                            strTelefonoCliente = item.strTelefonoCliente,
                            intAnioVehiculo = Convert.ToInt32(item.strAnioVehiculo),
                            strMoneda = item.strMoneda,
                            strCorreo = item.strCorreo,
                            strDireccion = item.strDireccion,
                            strRUC = item.strRuc,
                            prfc_recepcion = item.profc_recepcion,
                            prfc_recomendacion = item.profc_recomendacion
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

        public void insertarRecepRecom(EProforma oBe)
        {
            try
            {
                using (OperacionesDataContext dc = new OperacionesDataContext(Helper.conexion()))
                {

                    dc.SGE_PROFORMA_RECEP_RECOM_INSERTAR_ACTUALIZAR(
                    oBe.prfc_icod_proforma,
                    oBe.prfc_recepcion,
                    oBe.prfc_recomendacion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarProforma(EProforma oBe)
        {
            int? intIcod = 0;
            try
            {
                using (OperacionesDataContext dc = new OperacionesDataContext(Helper.conexion()))
                {

                    dc.SGE_EXTEND_PROFORMA_INSERTAR(
                    ref intIcod,
                    oBe.prfc_vnumero_proforma,
                    oBe.prfc_sfecha_proforma,
                    oBe.prfc_iid_orden_trabajo,
                    oBe.prfc_vnumero_orden_trabajo,
                    oBe.prfc_icod_cliente,
                    oBe.prfc_iid_vehiculo,
                    oBe.prfc_vkilometraje,
                    oBe.prfc_vcolor,
                    oBe.tablc_iid_tipo_moneda,
                    oBe.prfc_nmonto_neto,
                    oBe.prfc_npor_imp_igv,
                    oBe.prfc_nmonto_igv,
                    oBe.prfc_nmonto_total,
                    oBe.prfc_nmonto_descuento,
                    oBe.prfc_iid_situacion_proforma,
                    oBe.intUsuario,
                    oBe.strPc);
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarProforma(EProforma oBe)
        {
            try
            {
                using (OperacionesDataContext dc = new OperacionesDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PROFORMA_MODIFICAR(
                    oBe.prfc_icod_proforma,
                    oBe.prfc_vnumero_proforma,
                    oBe.prfc_sfecha_proforma,
                    oBe.prfc_iid_orden_trabajo,
                    oBe.prfc_vnumero_orden_trabajo,
                    oBe.prfc_icod_cliente,
                    oBe.prfc_iid_vehiculo,
                    oBe.prfc_vkilometraje,
                    oBe.prfc_vcolor,
                    oBe.tablc_iid_tipo_moneda,
                    oBe.prfc_nmonto_neto,
                    oBe.prfc_npor_imp_igv,
                    oBe.prfc_nmonto_igv,
                    oBe.prfc_nmonto_total,
                    oBe.prfc_nmonto_descuento,
                    oBe.prfc_iid_situacion_proforma,
                    oBe.intUsuario,
                    oBe.strPc);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarProforma(EProforma oBe)
        {
            try
            {
                using (OperacionesDataContext dc = new OperacionesDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PROFORMA_ELIMINAR(
                        oBe.prfc_icod_proforma,
                        oBe.intUsuario,
                        oBe.strPc);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Proforma Det
        public List<EProformaDet> listarProformaDetalle(int intProforma)
        {
            List<EProformaDet> lista = null;

            try
            {
                using (OperacionesDataContext dc = new OperacionesDataContext(Helper.conexion()))
                {
                    lista = new List<EProformaDet>();
                    var query = dc.SGE_EXTEND_PROFORMA_DET_LISTAR(intProforma);
                    foreach (var item in query)
                    {
                        lista.Add(new EProformaDet()
                        {
                            prfd_icod_item_proforma = item.prfd_icod_item_proforma,
                            prfc_icod_proforma = Convert.ToInt32(item.prfc_icod_proforma),
                            prfd_iitem_proforma = Convert.ToInt32(item.prfd_iitem_proforma),
                            prfd_iid_producto = Convert.ToInt32(item.prfd_iid_producto),
                            prfd_ncantidad = item.prfd_ncantidad,
                            prfd_vdescripcion = item.prfd_vdescripcion,
                            prfd_nprecio_unitario_item = item.prfd_nprecio_unitario_item,
                            prfd_nprecio_total_item = item.prfd_nprecio_total_item,
                            strCodProducto = item.strCodProducto,
                            strLinea = item.strLinea,
                            strSubLinea = item.strSubLinea,
                            strDesUM = item.strDesUM,
                            strMoneda = item.strMoneda
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
        public void insertarProformaDetalle(EProformaDet oBe)
        {
            try
            {
                using (OperacionesDataContext dc = new OperacionesDataContext(Helper.conexion()))
                {

                    dc.SGE_EXTEND_PROFORMA_DET_INSERTAR(
                    oBe.prfc_icod_proforma,
                    oBe.prfd_iitem_proforma,
                    oBe.prfd_iid_producto,
                    oBe.prfd_ncantidad,
                    oBe.prfd_vdescripcion,
                    oBe.prfd_nprecio_unitario_item,
                    oBe.prfd_nprecio_total_item,
                    oBe.intUsuario,
                    oBe.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarProformaDetalle(EProformaDet oBe)
        {
            try
            {
                using (OperacionesDataContext dc = new OperacionesDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PROFORMA_DET_MODIFICAR(
                        oBe.prfd_icod_item_proforma,
                        oBe.prfc_icod_proforma,
                        oBe.prfd_iitem_proforma,
                        oBe.prfd_iid_producto,
                        oBe.prfd_ncantidad,
                        oBe.prfd_vdescripcion,
                        oBe.prfd_nprecio_unitario_item,
                        oBe.prfd_nprecio_total_item,
                        oBe.intUsuario,
                        oBe.strPc);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarProformaDetalle(EProformaDet oBe)
        {
            try
            {
                using (OperacionesDataContext dc = new OperacionesDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PROFORMA_DET_ELIMINAR(
                        oBe.prfd_icod_item_proforma,
                        oBe.intUsuario,
                        oBe.strPc);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Productividad
        public List<EProductividad> listarProductividad(DateTime? dtFecha_Inicio, DateTime? dtFecha_Fin, int intEjercicio)
        {
            List<EProductividad> lista = null;

            try
            {
                using (OperacionesDataContext dc = new OperacionesDataContext(Helper.conexion()))
                {
                    lista = new List<EProductividad>();
                    var query = dc.SGE_PRODUCTIVIDAD_ACUMULADO_LISTAR(dtFecha_Inicio, dtFecha_Fin, intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EProductividad()
                        {
                            icod_personal = Convert.ToInt32(item.icod_personal),
                            idd_personal = Convert.ToInt32(item.perc_iid_personal),
                            nombre_personal = item.perc_vapellidos_nombres,
                            cargo_personal = item.tarec_vdescripcion.ToUpper(),
                            cantidad_servicios = Convert.ToDecimal(item.cantidad),
                            total_comision = Convert.ToDecimal(item.total_comision)
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

        public List<EProductividadDet> listarProductividadDetalle(int intPersonal, DateTime? dtFecha_Inicio, DateTime? dtFecha_Fin, int intEjercicio)
        {
            List<EProductividadDet> lista = null;

            try
            {
                using (OperacionesDataContext dc = new OperacionesDataContext(Helper.conexion()))
                {
                    lista = new List<EProductividadDet>();
                    var query = dc.SGE_PRODUCTIVIDAD_DETALLE_LISTAR(intPersonal, dtFecha_Inicio, dtFecha_Fin, intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EProductividadDet()
                        {
                            icod_personal = Convert.ToInt32(item.icod_personal),
                            fecha = Convert.ToDateTime(item.fecha),
                            strTipoDoc = item.strTipoDoc,
                            strNroDoc = item.strNroDoc,
                            strNroOrden = item.strNroOrden,
                            vehd_vplaca = item.vehd_vplaca,
                            prdc_vcode_producto = item.prdc_vcode_producto,
                            prdc_vdescripcion_larga = item.prdc_vdescripcion_larga,
                            dblCantidad = Convert.ToDecimal(item.dblCantidad),
                            dblPrecioUnitario = Convert.ToDecimal(item.dblPrecioUnitario),
                            dblMonto = Convert.ToDecimal(item.dblMonto)
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
        #endregion

        #region Proyectos
        public int insertarProyectos(EPersonal oBe)
        {
            int? intIcod = 0;
            try
            {
                using (OperacionesDataContext dc = new OperacionesDataContext(Helper.conexion()))
                {

                    dc.SGEOP_PERSONAL_INSERTAR(
                    ref intIcod,
                    oBe.perc_iid_personal,
                    oBe.perc_sfecha_registro,
                    oBe.ApellNomb,
                    oBe.perc_sfecha_nacimiento,
                    oBe.tablc_iid_tipo_cargo,
                    oBe.tablc_iid_tipo_area,
                    oBe.perc_iid_situacion_perso,
                    oBe.intUsuario,
                    oBe.strPc);
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarProyectos(EPersonal oBe)
        {
            try
            {
                using (OperacionesDataContext dc = new OperacionesDataContext(Helper.conexion()))
                {
                    dc.SGEOP_PERSONAL_MODIFICAR(
                    oBe.perc_icod_personal,
                    oBe.perc_iid_personal,
                    oBe.perc_sfecha_registro,
                    oBe.ApellNomb,
                    oBe.perc_sfecha_nacimiento,
                    oBe.tablc_iid_tipo_cargo,
                    oBe.tablc_iid_tipo_area,
                    oBe.perc_iid_situacion_perso,
                    oBe.intUsuario,
                    oBe.strPc);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarProyectos(EPersonal oBe)
        {
            try
            {
                using (OperacionesDataContext dc = new OperacionesDataContext(Helper.conexion()))
                {
                    dc.SGEOP_PERSONAL_ELIMINAR(
                        oBe.perc_icod_personal,
                        oBe.intUsuario,
                        oBe.strPc);
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


