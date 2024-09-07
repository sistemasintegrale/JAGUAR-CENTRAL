using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SGE.DataAccess
{
    public class ComprasData
    {
        #region Proveedor
        public List<EProveedor> ListarProveedor()
        {
            List<EProveedor> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EProveedor>();
                    var query = dc.SGE_PROVEEDOR_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EProveedor()
                        {
                            iid_icod_proveedor = item.proc_icod_proveedor,
                            iid_proveedor = Convert.ToInt32(item.proc_iid_proveedor),
                            vcod_proveedor = item.proc_vcod_proveedor,
                            vnombre = item.proc_vnombre,
                            vpaterno = item.proc_vpaterno,
                            vmaterno = item.proc_vmaterno,
                            vnombrecompleto = item.proc_vnombrecompleto,
                            iid_tipo_persona = Convert.ToInt32(item.proc_tipo_persona),
                            vcomercial = item.proc_vcomercial,
                            vdireccion = item.proc_vdireccion,
                            vtelefono = item.proc_vtelefono,
                            vfax = item.proc_vfax,
                            isituacion = item.proc_situacion,
                            vrepresentante = item.proc_vrepresentante,
                            vruc = item.proc_vruc,
                            tablc_iid_tipo_relacion = item.tablc_iid_tipo_relacion,
                            iid_usuario_crea = item.proc_iid_usuario_crea,
                            fecha_crea = Convert.ToDateTime(item.proc_sfecha_crea),
                            vpc_crea = item.proc_vpc_crea,
                            iid_usuario_modifica = item.proc_iid_usuario_modifica,
                            fecha_modifica = Convert.ToDateTime(item.proc_sfecha_modifica),
                            vpc_modifica = item.proc_vpc_modifica,
                            iactivo = item.proc_activo,
                            vdni = item.proc_vdni,
                            proc_vcorreo = item.proc_vcorreo,
                            proc_sfecha = item.proc_sfecha,
                            proc_tipo_doc = item.proc_tipo_doc,
                            ubicc_icod_ubicacion = item.ubicc_icod_ubicacion,
                            anac_icod_analitica = Convert.ToInt32(item.anac_icod_analitica),
                            anac_iid_analitica = item.anac_iid_analitica,
                            tarec_icorrelativo = item.tarec_icorrelativo,
                            flag_exceptuado = (item.flag_exceptuado == null) ? 0 : 1,
                            proc_vcta_bco_nacion = item.proc_vcta_bco_nacion,
                            proc_vmodalidad_pago = item.proc_vmodalidad_pago,
                            proc_vbanco_pago = item.proc_vbanco_pago,
                            proc_vcuenta_corriente_banco = item.proc_vcuenta_corriente_banco,
                            proc_vcodigo_interbancario = item.proc_vcodigo_interbancario,
                            proc_pais_nodomic = item.proc_pais_nodomic,
                            proc_responsable_produccion = item.proc_responsable_produccion
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
        public List<EProveedor> ListarProveedorComboOPE()
        {
            List<EProveedor> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EProveedor>();
                    var query = dc.SGE_PROVEEDOR_LISTAR_COMBO();
                    foreach (var item in query)
                    {
                        lista.Add(new EProveedor()
                        {
                            iid_icod_proveedor = item.proc_icod_proveedor,
                            iactivo = item.proc_activo,
                            proc_responsable_produccion = item.proc_responsable_produccion
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
        public int ProveedorInsertar(EProveedor obj)
        {

            try
            {
                int? idProveedor = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_PROVEEDOR_INSERTAR(
                        ref idProveedor,
                        obj.iid_proveedor,
                        obj.vcod_proveedor,
                        obj.vnombre,
                        obj.vpaterno,
                        obj.vmaterno,
                        obj.vnombrecompleto,
                        obj.iid_tipo_persona,
                        obj.vcomercial,
                        obj.vdireccion,
                        obj.vtelefono,
                        obj.vfax,
                        obj.isituacion,
                        obj.vrepresentante,
                        obj.vruc,
                        obj.tablc_iid_tipo_relacion,
                        obj.iid_usuario_crea,
                        obj.vpc_crea,
                        obj.iactivo,
                        obj.vdni,
                        obj.proc_vcorreo,
                        obj.proc_sfecha,
                        obj.proc_tipo_doc,
                        obj.ubicc_icod_ubicacion,
                        obj.anac_icod_analitica,
                        obj.proc_vcta_bco_nacion,
                        obj.proc_vmodalidad_pago,
                        obj.proc_vbanco_pago,
                        obj.proc_vcuenta_corriente_banco,
                        obj.proc_vcodigo_interbancario,
                        obj.proc_pais_nodomic,
                        obj.proc_responsable_produccion
                    );

                }
                return Convert.ToInt32(idProveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarProveedor(EProveedor obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_PROVEEDOR_MODIFICAR(
                        obj.iid_icod_proveedor,
                        obj.vnombre,
                        obj.vpaterno,
                        obj.vmaterno,
                        obj.vnombrecompleto,
                        obj.iid_tipo_persona,
                        obj.vcomercial,
                        obj.vdireccion,
                        obj.vtelefono,
                        obj.vfax,
                        obj.isituacion,
                        obj.vrepresentante,
                        obj.vruc,
                        obj.tablc_iid_tipo_relacion,
                        obj.iid_usuario_modifica,
                        obj.vpc_modifica,
                        obj.vdni,
                        obj.proc_vcorreo,
                        obj.proc_sfecha,
                        obj.proc_tipo_doc,
                        obj.ubicc_icod_ubicacion,
                        obj.anac_icod_analitica,
                        obj.proc_vcta_bco_nacion,
                        obj.proc_vmodalidad_pago,
                        obj.proc_vbanco_pago,
                        obj.proc_vcuenta_corriente_banco,
                        obj.proc_vcodigo_interbancario,
                        obj.proc_pais_nodomic,
                        obj.proc_responsable_produccion
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarProveedorAnalitica(EProveedor ob)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGES_PROVEEDOR_ACTUALIZAR_ANALITICA(
                            ob.iid_icod_proveedor,
                            ob.anac_icod_analitica
                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarProveedor(int IdProveedor)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_PROVEEDOR_ELIMINAR(IdProveedor);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Doc. Compra Cab.
        public List<EDocCompra> listarDocCompra(int intEjercicio, DateTime FechaDesde, DateTime FechaHasta)
        {
            List<EDocCompra> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EDocCompra>();
                    var query = dc.SGEC_DOC_COMPRA_LISTAR(intEjercicio, FechaDesde, FechaHasta);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocCompra()
                        {
                            facc_icod_doc = item.facc_icod_doc,
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            facc_num_doc = item.facc_num_doc,
                            facc_sfecha_doc = Convert.ToDateTime(item.facc_sfecha_doc),
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            doxpc_icod_correlativo = item.intDXP,
                            facc_svencimiento = Convert.ToDateTime(item.facc_svencimiento),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            facc_iforma_pago = Convert.ToInt32(item.facc_iforma_pago),
                            facc_vobservaciones = item.facc_vobservaciones,
                            facc_nporcent_imp_doc = Convert.ToDecimal(item.facc_nporcent_imp_doc),
                            facc_nmonto_neto_doc = Convert.ToDecimal(item.facc_nmonto_neto_doc),
                            facc_nmonto_imp = item.facc_nmonto_imp,
                            facc_nmonto_total_doc = Convert.ToDecimal(item.facc_nmonto_total_doc),
                            facc_isituacion = Convert.ToInt32(item.facc_isituacion),
                            strProveedor = item.strProveedor,
                            strTipoDoc = item.strTipoDocumento,
                            strAlmacen = item.strAlmacen,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            strFormaPago = item.strFormaPago,
                            intDXP = Convert.ToInt64(item.intDXP),
                            facc_flag_incluye_igv = Convert.ToBoolean(item.facc_flag_incluye_igv),
                            intCorrelativoDXP = Convert.ToInt64(item.intCorrelativo),
                            facc_mes = Convert.ToInt16(item.facc_mes),
                            strMesProceso = item.strMesProceso,
                            strComprador = item.perc_vapellidos_nombres,
                            facc_icod_comprador = Convert.ToInt32(item.facc_icod_comprador)
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

        public List<EDocCompraDet> listarConsultaDocCompra(int intEjercicio, DateTime dt1, DateTime dt2)
        {
            List<EDocCompraDet> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EDocCompraDet>();
                    var query = dc.SGEC_DOC_COMPRA_CONSULTA_LISTAR(intEjercicio, dt1, dt2);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocCompraDet()
                        {
                            facc_icod_doc = item.facc_icod_doc,
                            dtFecha = Convert.ToDateTime(item.facc_sfecha_doc),
                            strTipoDoc = item.strTipoDocumento,
                            strNroDoc = item.facc_num_doc,
                            strProveedor = (item.strProveedor == null) ? "" : item.strProveedor,
                            facd_vdescripcion_item = item.prdc_vdescripcion_larga,
                            facd_ncantidad = item.facd_ncantidad,
                            facd_nmonto_unit = item.facd_nmonto_unit,
                            facd_nmonto_total = Convert.ToDecimal(item.facd_nmonto_total),
                            strComprador = (item.perc_vapellidos_nombres == null) ? "" : item.perc_vapellidos_nombres,
                            intCorrelativo = Convert.ToInt32(item.intCorrelativo)
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
        public List<EDocCompra> listarDocCompraSinIGV(int intEjercicio, DateTime dt1, DateTime dt2)
        {
            List<EDocCompra> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EDocCompra>();
                    var query = dc.SGEC_DOC_COMPRA_NO_GRAVADO_LISTAR(intEjercicio, dt1, dt2);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocCompra()
                        {
                            facc_icod_doc = item.facc_icod_doc,
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            facc_num_doc = item.facc_num_doc,
                            facc_sfecha_doc = Convert.ToDateTime(item.facc_sfecha_doc),
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            doxpc_icod_correlativo = item.intDXP,
                            facc_svencimiento = Convert.ToDateTime(item.facc_svencimiento),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            facc_iforma_pago = Convert.ToInt32(item.facc_iforma_pago),
                            facc_vobservaciones = item.facc_vobservaciones,
                            facc_nporcent_imp_doc = Convert.ToDecimal(item.facc_nporcent_imp_doc),
                            facc_nmonto_neto_doc = Convert.ToDecimal(item.facc_nmonto_neto_doc),
                            facc_nmonto_imp = item.facc_nmonto_imp,
                            facc_nmonto_total_doc = Convert.ToDecimal(item.facc_nmonto_total_doc),
                            facc_isituacion = Convert.ToInt32(item.facc_isituacion),
                            strProveedor = item.strProveedor,
                            strTipoDoc = item.strTipoDocumento,
                            strAlmacen = item.strAlmacen,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            strFormaPago = item.strFormaPago,
                            intDXP = Convert.ToInt64(item.intDXP),
                            facc_flag_incluye_igv = Convert.ToBoolean(item.facc_flag_incluye_igv),
                            intCorrelativoDXP = Convert.ToInt64(item.intCorrelativo),
                            facc_mes = Convert.ToInt16(item.facc_mes),
                            strMesProceso = item.strMesProceso,
                            strComprador = item.perc_vapellidos_nombres,
                            facc_icod_comprador = Convert.ToInt32(item.facc_icod_comprador)
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
        public int insertarDocCompra(EDocCompra obj)
        {
            try
            {
                int? idDocCompra = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_DOC_COMPRA_INSERTAR(
                        ref idDocCompra,
                        obj.proc_icod_proveedor,
                        obj.tdocc_icod_tipo_doc,
                        obj.facc_num_doc,
                        obj.facc_sfecha_doc,
                        obj.almac_icod_almacen,
                        obj.doxpc_icod_correlativo,
                        obj.facc_svencimiento,
                        obj.tablc_iid_tipo_moneda,
                        obj.facc_iforma_pago,
                        obj.facc_vobservaciones,
                        obj.facc_nporcent_imp_doc,
                        obj.facc_nmonto_neto_doc,
                        obj.facc_nmonto_imp,
                        obj.facc_nmonto_total_doc,
                        obj.facc_isituacion,
                        obj.intUsuario,
                        obj.strPc,
                        obj.facc_flag_incluye_igv,
                        obj.facc_anio,
                        obj.facc_mes,
                        obj.facc_icod_comprador
                    );
                }
                return Convert.ToInt32(idDocCompra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarDocCompra(EDocCompra obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_DOC_COMPRA_MODIFICAR(
                        obj.facc_icod_doc,
                        obj.proc_icod_proveedor,
                        obj.tdocc_icod_tipo_doc,
                        obj.facc_num_doc,
                        obj.facc_sfecha_doc,
                        obj.almac_icod_almacen,
                        obj.facc_svencimiento,
                        obj.tablc_iid_tipo_moneda,
                        obj.facc_iforma_pago,
                        obj.facc_vobservaciones,
                        obj.facc_nporcent_imp_doc,
                        obj.facc_nmonto_neto_doc,
                        obj.facc_nmonto_imp,
                        obj.facc_nmonto_total_doc,
                        obj.facc_isituacion,
                        obj.intUsuario,
                        obj.strPc,
                        obj.facc_flag_incluye_igv,
                        obj.facc_anio,
                        obj.facc_mes,
                        obj.facc_icod_comprador
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarDocCompra(EDocCompra obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_DOC_COMPRA_ELIMINAR(
                        obj.facc_icod_doc,
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
        #endregion
        #region Doc. Compra Det.  
        public List<EDocCompraDet> listarDocCompraDetCuentas(int intDocCompra)
        {
            List<EDocCompraDet> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EDocCompraDet>();
                    var query = dc.SGEC_DOC_COMPRA_DETALLE_CUENTAS_LISTAR(intDocCompra);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocCompraDet()
                        {
                            facd_iitem = Convert.ToInt32(item.facd_iitem),
                            intClasificacion = Convert.ToInt32(item.intClasificacion),
                            strCodLinea = item.famic_vabreviatura,
                            strLinea = item.famic_vdescripcion,
                            intCtaContable = item.ctacc_icod_cuenta_contable_compra,
                            flagCCosto = item.ctacc_iccosto,
                            intTipoAnalitica = item.tablc_iid_tipo_analitica,
                            intAnaliticaProveedor = Convert.ToInt32(item.intAnaliticaProveedor),
                            facd_vdescripcion_item = item.facd_vdescripcion_item,
                            facd_nmonto_total = Convert.ToDecimal(item.dcmlTotalCuenta)
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
        public List<EDocCompraDet> listarDocCompraDet(int intDocCompra)
        {
            List<EDocCompraDet> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EDocCompraDet>();
                    var query = dc.SGEC_DOC_COMPRA_DETALLE_LISTAR(intDocCompra);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocCompraDet()
                        {
                            facd_icod_doc = item.facd_icod_doc,
                            facd_iitem = Convert.ToInt32(item.facd_iitem),
                            prd_icod_producto = item.prd_icod_producto,
                            facd_ncantidad = item.facd_ncantidad,
                            facd_nmonto_unit = item.facd_nmonto_unit,
                            facd_nmonto_total = Convert.ToDecimal(item.facd_nmonto_total),
                            facd_icod_kardex = item.facd_icod_kardex,
                            facd_vdescripcion_item = item.facd_vdescripcion_item,
                            strCodProducto = item.strCodProducto,
                            //strLinea = item.strLinea,
                            //strSubLinea = item.strSubLinea,
                            strDesUM = item.strDesUM,
                            strMoneda = item.strMoneda
                            //intClasificacion = Convert.ToInt32(item.intClasificacion),
                            //marc_vabreviatura = item.marc_vabreviatura,
                            //marc_vdescripcion = item.marc_vdescripcion,
                            //modc_vabreviatura = item.modc_vabreviatura,
                            //modc_vdescripcion = item.modc_vdescripcion
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
        public void insertarDocCompraDet(EDocCompraDet obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_DOC_COMPRA_DETALLE_INSERTAR(
                        obj.facc_icod_doc,
                        obj.facd_iitem,
                        obj.prd_icod_producto,
                        obj.facd_ncantidad,
                        obj.facd_nmonto_unit,
                        obj.facd_nmonto_total,
                        obj.facd_icod_kardex,
                        obj.facd_vdescripcion_item,
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
        public void modificarDocCompraDet(EDocCompraDet obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_DOC_COMPRA_DETALLE_MODIFICAR(
                        obj.facd_icod_doc,
                        obj.facd_iitem,
                        obj.prd_icod_producto,
                        obj.facd_ncantidad,
                        obj.facd_nmonto_unit,
                        obj.facd_nmonto_total,
                        obj.facd_icod_kardex,
                        obj.facd_vdescripcion_item,
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
        public void eliminarDocCompraDet(EDocCompraDet obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_DOC_COMPRA_DETALLE_ELIMINAR(
                        obj.facd_icod_doc,
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
        #endregion
        #region Fac. Compra Nacional Cab
        public List<EFacturaCompra> listarFacCompra(int intEjercicio)
        {
            List<EFacturaCompra> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EFacturaCompra>();
                    var query = dc.SGEC_FACTURA_COMPRA_LISTAR(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaCompra()
                        {
                            fcoc_icod_doc = item.fcoc_icod_doc,
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            fcoc_num_doc = item.fcoc_num_doc,
                            fcoc_sfecha_doc = Convert.ToDateTime(item.fcoc_sfecha_doc),
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            fcoc_svencimiento = Convert.ToDateTime(item.fcoc_svencimiento),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            fcoc_iforma_pago = Convert.ToInt32(item.fcoc_iforma_pago),
                            fcoc_vreferencia = item.fcoc_vreferencia,
                            fcoc_nporcent_imp_doc = Convert.ToDecimal(item.fcoc_nporcent_imp_doc),
                            fcoc_nmonto_destino_gravado = Convert.ToDecimal(item.fcoc_nmonto_destino_gravado),
                            fcoc_nmonto_destino_mixto = Convert.ToDecimal(item.fcoc_nmonto_destino_mixto),
                            fcoc_nmonto_destino_nogravado = Convert.ToDecimal(item.fcoc_nmonto_destino_nogravado),
                            fcoc_nmonto_nogravado = Convert.ToDecimal(item.fcoc_nmonto_nogravado),
                            fcoc_nmonto_imp_destino_gravado = Convert.ToDecimal(item.fcoc_nmonto_imp_destino_gravado),
                            fcoc_nmonto_imp_destino_mixto = Convert.ToDecimal(item.fcoc_nmonto_imp_destino_mixto),
                            fcoc_nmonto_imp_destino_nogravado = Convert.ToDecimal(item.fcoc_nmonto_imp_destino_nogravado),
                            fcoc_nmonto_tipo_cambio = Convert.ToDecimal(item.fcoc_nmonto_tipo_cambio),
                            fcoc_imes_iid_proceso = Convert.ToInt16(item.fcoc_imes_iid_proceso),
                            fcoc_vnro_depo_detraccion = item.fcoc_vnro_depo_detraccion,
                            fcoc_sfecha_depo_detraccion = item.fcoc_sfecha_depo_detraccion,
                            fcoc_nmonto_total_detalle = Convert.ToDecimal(item.fcoc_nmonto_total_detalle),
                            fcoc_nmonto_adelanto = Convert.ToDecimal(item.fcoc_nmonto_adelanto),
                            fcoc_isituacion = Convert.ToInt32(item.fcoc_isituacion),
                            strProveedor = item.strProveedor,
                            strAlmacen = item.strAlmacen,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            strFormaPago = item.strFormaPago,
                            intDXP = Convert.ToInt64(item.intDXP),
                            fcoc_anio = item.fcoc_anio,
                            prep_icod_presupuesto = item.prep_icod_importacion,
                            //prep_cod_presupuesto = item.prep_cod_presupuesto,
                            fcoc_flag_importacion = Convert.ToBoolean(item.fcoc_flag_importacion),
                            ococ_icod_orden_compra = item.ococ_icod_orden_compra,
                            ococ_numero_orden_compra = item.ococ_numero_orden_compra,
                            fcoc_iestado_recep = Convert.ToInt32(item.fcoc_iestado_recep),
                            DesEstadoRecepcion = item.DesEstadoRecepcion,
                            fcoc_bincluye_igv = Convert.ToBoolean(item.fcoc_bincluye_igv),
                            fcoc_nmonto_neto_detalle = Convert.ToDecimal(item.fcoc_nmonto_neto_detalle),
                            impc_icod_importacion = Convert.ToInt32(item.impc_icod_importacion),
                            fcoc_tipo_producto = Convert.ToInt32(item.fcoc_tipo_producto),
                            fcoc_tipo_documento = Convert.ToInt32(item.fcoc_tipo_documento),
                            fcoc_icod_documento = Convert.ToInt32(item.fcoc_icod_documento),
                            NumTD = item.NumTD

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
        public List<EFacturaCompra> listarFacCompraMercaderia(int intEjercicio)
        {
            List<EFacturaCompra> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EFacturaCompra>();
                    var query = dc.SGEC_FACTURA_COMPRA_MERCADERIA_LISTAR(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaCompra()
                        {
                            fcoc_icod_doc = item.fcoc_icod_doc,
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            fcoc_num_doc = item.fcoc_num_doc,
                            fcoc_sfecha_doc = Convert.ToDateTime(item.fcoc_sfecha_doc),
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            fcoc_svencimiento = Convert.ToDateTime(item.fcoc_svencimiento),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            fcoc_iforma_pago = Convert.ToInt32(item.fcoc_iforma_pago),
                            fcoc_vreferencia = item.fcoc_vreferencia,
                            fcoc_nporcent_imp_doc = Convert.ToDecimal(item.fcoc_nporcent_imp_doc),
                            fcoc_nmonto_destino_gravado = Convert.ToDecimal(item.fcoc_nmonto_destino_gravado),
                            fcoc_nmonto_destino_mixto = Convert.ToDecimal(item.fcoc_nmonto_destino_mixto),
                            fcoc_nmonto_destino_nogravado = Convert.ToDecimal(item.fcoc_nmonto_destino_nogravado),
                            fcoc_nmonto_nogravado = Convert.ToDecimal(item.fcoc_nmonto_nogravado),
                            fcoc_nmonto_imp_destino_gravado = Convert.ToDecimal(item.fcoc_nmonto_imp_destino_gravado),
                            fcoc_nmonto_imp_destino_mixto = Convert.ToDecimal(item.fcoc_nmonto_imp_destino_mixto),
                            fcoc_nmonto_imp_destino_nogravado = Convert.ToDecimal(item.fcoc_nmonto_imp_destino_nogravado),
                            fcoc_nmonto_tipo_cambio = Convert.ToDecimal(item.fcoc_nmonto_tipo_cambio),
                            fcoc_imes_iid_proceso = Convert.ToInt16(item.fcoc_imes_iid_proceso),
                            fcoc_vnro_depo_detraccion = item.fcoc_vnro_depo_detraccion,
                            fcoc_sfecha_depo_detraccion = item.fcoc_sfecha_depo_detraccion,
                            fcoc_nmonto_total_detalle = Convert.ToDecimal(item.fcoc_nmonto_total_detalle),
                            fcoc_nmonto_adelanto = Convert.ToDecimal(item.fcoc_nmonto_adelanto),
                            fcoc_isituacion = Convert.ToInt32(item.fcoc_isituacion),
                            strProveedor = item.strProveedor,
                            strAlmacen = item.strAlmacen,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            strFormaPago = item.strFormaPago,
                            intDXP = Convert.ToInt64(item.intDXP),
                            fcoc_anio = item.fcoc_anio,
                            prep_icod_presupuesto = item.prep_icod_importacion,
                            //prep_cod_presupuesto = item.prep_cod_presupuesto,
                            fcoc_flag_importacion = Convert.ToBoolean(item.fcoc_flag_importacion),
                            ococ_icod_orden_compra = item.ococ_icod_orden_compra,
                            ococ_numero_orden_compra = item.ococ_numero_orden_compra,
                            fcoc_iestado_recep = Convert.ToInt32(item.fcoc_iestado_recep),
                            DesEstadoRecepcion = item.DesEstadoRecepcion,
                            fcoc_bincluye_igv = Convert.ToBoolean(item.fcoc_bincluye_igv),
                            fcoc_nmonto_neto_detalle = Convert.ToDecimal(item.fcoc_nmonto_neto_detalle),
                            impc_icod_importacion = Convert.ToInt32(item.impc_icod_importacion),
                            fcoc_tipo_producto = Convert.ToInt32(item.fcoc_tipo_producto),
                            fcoc_tipo_documento = Convert.ToInt32(item.fcoc_tipo_documento),
                            fcoc_icod_documento = Convert.ToInt32(item.fcoc_icod_documento),
                            NumTD = item.NumTD

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
        public List<EFacturaCompra> listarFacCompraXID(int intEjercicio, int fcoc_icod_doc)
        {
            List<EFacturaCompra> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EFacturaCompra>();
                    var query = dc.SGEC_FACTURA_COMPRA_LISTAR_X_ID(intEjercicio, fcoc_icod_doc);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaCompra()
                        {
                            fcoc_icod_doc = item.fcoc_icod_doc,
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            fcoc_num_doc = item.fcoc_num_doc,
                            fcoc_sfecha_doc = Convert.ToDateTime(item.fcoc_sfecha_doc),
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            fcoc_svencimiento = Convert.ToDateTime(item.fcoc_svencimiento),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            fcoc_iforma_pago = Convert.ToInt32(item.fcoc_iforma_pago),
                            fcoc_vreferencia = item.fcoc_vreferencia,
                            fcoc_nporcent_imp_doc = Convert.ToDecimal(item.fcoc_nporcent_imp_doc),
                            fcoc_nmonto_destino_gravado = Convert.ToDecimal(item.fcoc_nmonto_destino_gravado),
                            fcoc_nmonto_destino_mixto = Convert.ToDecimal(item.fcoc_nmonto_destino_mixto),
                            fcoc_nmonto_destino_nogravado = Convert.ToDecimal(item.fcoc_nmonto_destino_nogravado),
                            fcoc_nmonto_nogravado = Convert.ToDecimal(item.fcoc_nmonto_nogravado),
                            fcoc_nmonto_imp_destino_gravado = Convert.ToDecimal(item.fcoc_nmonto_imp_destino_gravado),
                            fcoc_nmonto_imp_destino_mixto = Convert.ToDecimal(item.fcoc_nmonto_imp_destino_mixto),
                            fcoc_nmonto_imp_destino_nogravado = Convert.ToDecimal(item.fcoc_nmonto_imp_destino_nogravado),
                            fcoc_nmonto_tipo_cambio = Convert.ToDecimal(item.fcoc_nmonto_tipo_cambio),
                            fcoc_imes_iid_proceso = Convert.ToInt16(item.fcoc_imes_iid_proceso),
                            //fcoc_bincluye_igv = Convert.ToBoolean(item.fcoc_bincluye_igv),
                            fcoc_vnro_depo_detraccion = item.fcoc_vnro_depo_detraccion,
                            fcoc_sfecha_depo_detraccion = item.fcoc_sfecha_depo_detraccion,
                            //fcoc_nmonto_neto_detalle = Convert.ToDecimal(item.fcoc_nmonto_neto_detalle),
                            fcoc_nmonto_total_detalle = Convert.ToDecimal(item.fcoc_nmonto_total_detalle),
                            fcoc_isituacion = Convert.ToInt32(item.fcoc_isituacion),
                            strProveedor = item.strProveedor,
                            strAlmacen = item.strAlmacen,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            strFormaPago = item.strFormaPago,
                            intDXP = Convert.ToInt64(item.intDXP),
                            fcoc_anio = item.fcoc_anio,
                            prep_icod_presupuesto = item.prep_icod_importacion,
                            prep_cod_presupuesto = item.prep_cod_presupuesto,
                            fcoc_flag_importacion = Convert.ToBoolean(item.fcoc_flag_importacion),
                            ococ_icod_orden_compra = item.ococ_icod_orden_compra,
                            ococ_numero_orden_compra = item.ococ_numero_orden_compra,
                            fcoc_iestado_recep = Convert.ToInt32(item.fcoc_iestado_recep),
                            DesEstadoRecepcion = item.DesEstadoRecepcion
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
        public List<EFacturaCompra> listarFacCompraRelacionPresupuesto(int intEjercicio, int prep_icod_presupuesto)
        {
            List<EFacturaCompra> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EFacturaCompra>();
                    var query = dc.SGEC_FACTURA_COMPRA_LISTAR_RELA_PRESUPUESTO(intEjercicio, prep_icod_presupuesto);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaCompra()
                        {
                            fcoc_icod_doc = item.fcoc_icod_doc,
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            fcoc_num_doc = item.fcoc_num_doc,
                            fcoc_sfecha_doc = Convert.ToDateTime(item.fcoc_sfecha_doc),
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            fcoc_vreferencia = item.fcoc_vreferencia,
                            fcoc_nmonto_total_detalle = Convert.ToDecimal(item.fcoc_nmonto_total_detalle),
                            fcoc_isituacion = Convert.ToInt32(item.fcoc_isituacion),
                            strProveedor = item.strProveedor,
                            strAlmacen = item.strAlmacen,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            strFormaPago = item.strFormaPago,
                            intDXP = Convert.ToInt64(item.intDXP),
                            fcoc_anio = item.fcoc_anio,
                            prep_icod_presupuesto = item.prep_icod_importacion,
                            prep_cod_presupuesto = item.prep_cod_presupuesto,
                            sflag_relacion_presupuesto = Convert.ToBoolean(item.sflag_relacion_pre),
                            fcoc_flag_importacion = Convert.ToBoolean(item.fcoc_flag_importacion),
                            ococ_icod_orden_compra = Convert.ToInt32(item.ococ_icod_orden_compra),
                            ococ_numero_orden_compra = item.ococ_numero_orden_compra
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
        public int insertarFacCompraNacional(EFacturaCompra obj)
        {

            try
            {
                int? idDocCompra = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_FACTURA_COMPRA_NACIONAL_INSERTAR(
                        ref idDocCompra,
                        obj.proc_icod_proveedor,
                        obj.fcoc_num_doc,
                        obj.fcoc_sfecha_doc,
                        obj.almac_icod_almacen,
                        obj.doxpc_icod_correlativo,
                        obj.fcoc_svencimiento,
                        obj.tablc_iid_tipo_moneda,
                        obj.fcoc_iforma_pago,
                        obj.fcoc_vreferencia,
                        obj.fcoc_nporcent_imp_doc,
                        obj.fcoc_nmonto_destino_gravado,
                        obj.fcoc_nmonto_destino_mixto,
                        obj.fcoc_nmonto_destino_nogravado,
                        obj.fcoc_nmonto_nogravado,
                        obj.fcoc_nmonto_imp_destino_gravado,
                        obj.fcoc_nmonto_imp_destino_mixto,
                        obj.fcoc_nmonto_imp_destino_nogravado,
                        obj.fcoc_nmonto_tipo_cambio,
                        obj.fcoc_nmonto_adelanto,
                        obj.fcoc_imes_iid_proceso,
                        obj.fcoc_bincluye_igv,
                        obj.fcoc_vnro_depo_detraccion,
                        obj.fcoc_sfecha_depo_detraccion,
                        obj.fcoc_nmonto_neto_detalle,
                        obj.fcoc_nmonto_total_detalle,
                        obj.fcoc_isituacion,
                        obj.intUsuario,
                        obj.strPc,
                        obj.fcoc_anio,
                        obj.prep_icod_presupuesto,
                        obj.fcoc_flag_importacion,
                        obj.ococ_icod_orden_compra,
                        obj.fcoc_iestado_recep,
                        obj.fcoc_tipo_producto,
                        obj.fcoc_tipo_documento,
                        obj.fcoc_icod_documento
                    );

                }
                return Convert.ToInt32(idDocCompra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFacCompraNacional(EFacturaCompra obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_FACTURA_COMPRA_NACIONAL_MODIFICAR(
                        obj.fcoc_icod_doc,
                        obj.proc_icod_proveedor,
                        obj.fcoc_num_doc,
                        obj.fcoc_sfecha_doc,
                        obj.almac_icod_almacen,
                        obj.doxpc_icod_correlativo,
                        obj.fcoc_svencimiento,
                        obj.tablc_iid_tipo_moneda,
                        obj.fcoc_iforma_pago,
                        obj.fcoc_vreferencia,
                        obj.fcoc_nporcent_imp_doc,
                        obj.fcoc_nmonto_destino_gravado,
                        obj.fcoc_nmonto_destino_mixto,
                        obj.fcoc_nmonto_destino_nogravado,
                        obj.fcoc_nmonto_nogravado,
                        obj.fcoc_nmonto_imp_destino_gravado,
                        obj.fcoc_nmonto_imp_destino_mixto,
                        obj.fcoc_nmonto_imp_destino_nogravado,
                        obj.fcoc_nmonto_tipo_cambio,
                        obj.fcoc_nmonto_adelanto,
                        obj.fcoc_imes_iid_proceso,
                        obj.fcoc_bincluye_igv,
                        obj.fcoc_vnro_depo_detraccion,
                        obj.fcoc_sfecha_depo_detraccion,
                        obj.fcoc_nmonto_neto_detalle,
                        obj.fcoc_nmonto_total_detalle,
                        obj.fcoc_isituacion,
                        obj.intUsuario,
                        obj.strPc,
                        obj.fcoc_anio,
                        obj.ococ_icod_orden_compra,
                        obj.fcoc_iestado_recep
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFacCompraEstadoRecepcion(EFacturaCompra obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_FACTURA_COMPRA_CAMBIAR_ESTADO_RECEP(
                        obj.fcoc_icod_doc,

                        obj.fcoc_iestado_recep
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFacCompraRelacionPresupuesto(int fcoc_icod_doc, int prep_icod_presupuesto)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_FACTURA_COMPRA_ACTUALIZAR_RELA_PRESUPUESTO(
                        fcoc_icod_doc,
                        prep_icod_presupuesto
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFacCompraNacional(EFacturaCompra obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_FACTURA_COMPRA_NACIONAL_ELIMINAR(
                        obj.fcoc_icod_doc,
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
        #endregion
        #region Fac. Compra Nacional Det
        public List<EFacturaCompraDet> listarFacCompraDetCuentas(int intFacCompra)
        {
            List<EFacturaCompraDet> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EFacturaCompraDet>();
                    var query = dc.SGE_FAC_COMPRA_DETALLE_CUENTAS_LISTAR(intFacCompra);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaCompraDet()
                        {
                            fcod_iitem = Convert.ToInt32(item.fcod_iitem),

                            strCodLinea = item.famic_vabreviatura,
                            strLinea = item.famic_vdescripcion,
                            intCtaContable = item.ctacc_icod_cuenta_contable_compra,
                            flagCCosto = item.ctacc_iccosto,
                            intTipoAnalitica = item.tablc_iid_tipo_analitica,
                            intAnaliticaProveedor = Convert.ToInt32(item.intAnaliticaProveedor),
                            fcod_vdescripcion_item = item.fcod_vdescripcion_item,
                            fcod_nmonto_total = Convert.ToDecimal(item.dcmlTotalCuenta)
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
        public List<EFacturaCompraDet> listarFacCompraDet(int intFacCompra)
        {
            List<EFacturaCompraDet> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EFacturaCompraDet>();
                    var query = dc.SGEC_FACTURA_COMPRA_DET_LISTAR(intFacCompra);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaCompraDet()
                        {
                            fcod_icod_doc = item.fcod_icod_doc,
                            fcod_iitem = Convert.ToInt32(item.fcod_iitem),
                            prd_icod_producto = Convert.ToInt32(item.prd_icod_producto),
                            fcod_ncantidad = item.fcod_ncantidad,
                            fcod_nmonto_unit = Convert.ToDecimal(item.fcod_nmonto_unit),
                            fcod_nmonto_total = Convert.ToDecimal(item.fcod_nmonto_total),
                            fcod_vdescripcion_item = item.fcod_vdescripcion_item,
                            ocod_icod_detalle_oc = Convert.ToInt32(item.ocod_icod_detalle_oc),
                            grcd_icod_detalle = Convert.ToInt32(item.grcd_icod_detalle),
                            fcod_icod_kardex = Convert.ToInt32(item.fcod_icod_kardex),
                            strCodProducto = item.strCodProducto,
                            strDesUM = item.strDesUM,
                            strMoneda = item.strMoneda,


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
        public List<EFacturaCompraDet> listarFacCompraImportacionDet(int intFacCompra)
        {
            List<EFacturaCompraDet> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EFacturaCompraDet>();
                    var query = dc.SGEC_FACTURA_COMPRA_IMPORTACION_DET_LISTAR(intFacCompra);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaCompraDet()
                        {
                            fcod_icod_doc = item.fcod_icod_doc,
                            prd_icod_producto = Convert.ToInt32(item.prd_icod_producto),
                            strDesProducto = item.strDesProducto,
                            fcod_ncantidad = Convert.ToInt32(item.fcod_ncantidad),
                            strDesUM = item.strDesUM,
                            fcod_nmonto_unit = Convert.ToDecimal(item.fcod_nmonto_unit),
                            fcod_nmonto_total = Convert.ToDecimal(item.fcod_nmonto_total),
                            fcod_nmonto_unit_costo = Convert.ToDecimal(item.fcod_nmonto_unit_costo),
                            fcod_nmonto_total_costo = Convert.ToDecimal(item.fcod_nmonto_total_costo),
                            PUnitario = Convert.ToDecimal(item.PUnitario),
                            PCUnit = Convert.ToDecimal(item.PCUnit),
                            PCTot = Convert.ToDecimal(item.PCTot),
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
        public void insertarFacCompraNacionalDet(EFacturaCompraDet obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_FACTURA_COMPRA_NACIONAL_DET_INSERTAR(
                        obj.fcoc_icod_doc,
                        obj.fcod_iitem,
                        obj.prd_icod_producto,
                        obj.fcod_ncantidad,
                        obj.fcod_nmonto_unit,
                        obj.fcod_nmonto_total,
                        obj.fcod_ndescuento,
                        obj.fcod_icod_kardex,
                        obj.fcod_vdescripcion_item,
                        obj.ocod_icod_detalle_oc,
                        obj.grcd_icod_detalle,
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
        public void modificarFacCompraImportacionDet(EFacturaCompraDet obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_FACTURA_COMPRA_DET_MODIFICAR(
                        obj.fcod_icod_doc,
                        obj.fcoc_icod_doc,
                        obj.fcod_iitem,
                        obj.prd_icod_producto,
                        obj.fcod_ncantidad,
                        obj.fcod_nmonto_unit,
                        obj.fcod_nmonto_total,
                        obj.fcod_vdescripcion_item,
                        obj.intUsuario,
                        obj.strPc,
                         obj.fcod_nmonto_unit_costo,
                        obj.fcod_nmonto_total_costo

                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFacCompraNacionalDet(EFacturaCompraDet obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {

                    dc.SGEC_FACTURA_COMPRA_NACIONAL_DET_ELIMINAR(
                        obj.fcod_icod_doc,
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
        #endregion
        #region Fac. Compra Mercaderia Det
        public List<EFacturaCompraDet> listarFacCompraMercaderiaDet(int intFacCompra)
        {
            List<EFacturaCompraDet> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EFacturaCompraDet>();
                    var query = dc.SGEC_FACTURA_COMPRA_MERCADERIA_DET_LISTAR(intFacCompra);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaCompraDet()
                        {
                            fcod_icod_doc = item.fcod_icod_doc,
                            fcod_iitem = Convert.ToInt32(item.fcod_iitem),
                            prd_icod_producto = Convert.ToInt32(item.prd_icod_producto),
                            fcod_ncantidad = Convert.ToInt32(item.fcod_ncantidad),
                            fcod_nmonto_unit = Convert.ToDecimal(item.fcod_nmonto_unit),
                            fcod_nmonto_total = Convert.ToDecimal(item.fcod_nmonto_total),
                            fcod_vdescripcion_item = item.fcod_vdescripcion_item,
                            ocod_icod_detalle_oc = Convert.ToInt32(item.ocod_icod_detalle_oc),
                            //grcd_icod_detalle = Convert.ToInt32(item.grcd_icod_detalle),
                            //fcod_icod_kardex = Convert.ToInt32(item.fcod_icod_kardex),
                            strCodProducto = item.strCodProducto,
                            strDesUM = item.strDesUM,
                            strMoneda = item.strMoneda,
                            fcod_rango_talla = item.fcod_rango_talla,
                            fcod_talla1 = item.fcod_talla1,
                            fcod_talla2 = item.fcod_talla2,
                            fcod_talla3 = item.fcod_talla3,
                            fcod_talla4 = item.fcod_talla4,
                            fcod_talla5 = item.fcod_talla5,
                            fcod_talla6 = item.fcod_talla6,
                            fcod_talla7 = item.fcod_talla7,
                            fcod_talla8 = item.fcod_talla8,
                            fcod_talla9 = item.fcod_talla9,
                            fcod_talla10 = item.fcod_talla10,
                            fcod_cant_talla1 = item.fcod_cant_talla1,
                            fcod_cant_talla2 = item.fcod_cant_talla2,
                            fcod_cant_talla3 = item.fcod_cant_talla3,
                            fcod_cant_talla4 = item.fcod_cant_talla4,
                            fcod_cant_talla5 = item.fcod_cant_talla5,
                            fcod_cant_talla6 = item.fcod_cant_talla6,
                            fcod_cant_talla7 = item.fcod_cant_talla7,
                            fcod_cant_talla8 = item.fcod_cant_talla8,
                            fcod_cant_talla9 = item.fcod_cant_talla9,
                            fcod_cant_talla10 = item.fcod_cant_talla10,
                            fcod_iid_kardex1 = item.fcod_iid_kardex1,
                            fcod_iid_kardex2 = item.fcod_iid_kardex2,
                            fcod_iid_kardex3 = item.fcod_iid_kardex3,
                            fcod_iid_kardex4 = item.fcod_iid_kardex4,
                            fcod_iid_kardex5 = item.fcod_iid_kardex5,
                            fcod_iid_kardex6 = item.fcod_iid_kardex6,
                            fcod_iid_kardex7 = item.fcod_iid_kardex7,
                            fcod_iid_kardex8 = item.fcod_iid_kardex8,
                            fcod_iid_kardex9 = item.fcod_iid_kardex9,
                            fcod_iid_kardex10 = item.fcod_iid_kardex10

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
        public void insertarFacCompraMercaderiaDet(EFacturaCompraDet obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_FACTURA_COMPRA_MERCADERIA_DET_INSERTAR(
                        obj.fcoc_icod_doc,
                        obj.fcod_iitem,
                        obj.prd_icod_producto,
                        obj.fcod_ncantidad,
                        obj.fcod_nmonto_unit,
                        obj.fcod_nmonto_total,
                        obj.fcod_vdescripcion_item,
                        obj.ocod_icod_detalle_oc,
                        obj.grcd_icod_detalle,
                        obj.fcod_rango_talla,
                        obj.fcod_talla1,
                        obj.fcod_talla2,
                        obj.fcod_talla3,
                        obj.fcod_talla4,
                        obj.fcod_talla5,
                        obj.fcod_talla6,
                        obj.fcod_talla7,
                        obj.fcod_talla8,
                        obj.fcod_talla9,
                        obj.fcod_talla10,
                        obj.fcod_cant_talla1,
                        obj.fcod_cant_talla2,
                        obj.fcod_cant_talla3,
                        obj.fcod_cant_talla4,
                        obj.fcod_cant_talla5,
                        obj.fcod_cant_talla6,
                        obj.fcod_cant_talla7,
                        obj.fcod_cant_talla8,
                        obj.fcod_cant_talla9,
                        obj.fcod_cant_talla10,
                        obj.fcod_iid_kardex1,
                        obj.fcod_iid_kardex2,
                        obj.fcod_iid_kardex3,
                        obj.fcod_iid_kardex4,
                        obj.fcod_iid_kardex5,
                        obj.fcod_iid_kardex6,
                        obj.fcod_iid_kardex7,
                        obj.fcod_iid_kardex8,
                        obj.fcod_iid_kardex9,
                        obj.fcod_iid_kardex10,
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
        public void modificarFacCompraMercaderiaDet(EFacturaCompraDet obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_FACTURA_COMPRA_MERCADERIA_DET_MODIFICAR(
                        obj.fcod_icod_doc,
                        obj.fcoc_icod_doc,
                        obj.fcod_iitem,
                        obj.prd_icod_producto,
                        obj.fcod_ncantidad,
                        obj.fcod_nmonto_unit,
                        obj.fcod_nmonto_total,
                        obj.fcod_vdescripcion_item,
                        obj.ocod_icod_detalle_oc,
                        obj.fcod_rango_talla,
                        obj.fcod_talla1,
                        obj.fcod_talla2,
                        obj.fcod_talla3,
                        obj.fcod_talla4,
                        obj.fcod_talla5,
                        obj.fcod_talla6,
                        obj.fcod_talla7,
                        obj.fcod_talla8,
                        obj.fcod_talla9,
                        obj.fcod_talla10,
                        obj.fcod_cant_talla1,
                        obj.fcod_cant_talla2,
                        obj.fcod_cant_talla3,
                        obj.fcod_cant_talla4,
                        obj.fcod_cant_talla5,
                        obj.fcod_cant_talla6,
                        obj.fcod_cant_talla7,
                        obj.fcod_cant_talla8,
                        obj.fcod_cant_talla9,
                        obj.fcod_cant_talla10,
                        obj.fcod_iid_kardex1,
                        obj.fcod_iid_kardex2,
                        obj.fcod_iid_kardex3,
                        obj.fcod_iid_kardex4,
                        obj.fcod_iid_kardex5,
                        obj.fcod_iid_kardex6,
                        obj.fcod_iid_kardex7,
                        obj.fcod_iid_kardex8,
                        obj.fcod_iid_kardex9,
                        obj.fcod_iid_kardex10,
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
        #endregion
        #region Fac.Compra Importacion Cab
        public int insertarFacCompraImportacion(EFacturaCompra obj)
        {

            try
            {
                int? idDocCompra = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_FACTURA_COMPRA_INSERTAR(
                        ref idDocCompra,
                        obj.proc_icod_proveedor,
                        obj.fcoc_num_doc,
                        obj.fcoc_sfecha_doc,
                        obj.doxpc_icod_correlativo,
                        obj.fcoc_svencimiento,
                        obj.tablc_iid_tipo_moneda,
                        obj.fcoc_iforma_pago,
                        obj.fcoc_vreferencia,
                        obj.fcoc_nporcent_imp_doc,
                        obj.fcoc_nmonto_destino_gravado,
                        obj.fcoc_nmonto_destino_mixto,
                        obj.fcoc_nmonto_destino_nogravado,
                        obj.fcoc_nmonto_nogravado,
                        obj.fcoc_nmonto_imp_destino_gravado,
                        obj.fcoc_nmonto_imp_destino_mixto,
                        obj.fcoc_nmonto_imp_destino_nogravado,
                        obj.fcoc_nmonto_tipo_cambio,
                        obj.fcoc_imes_iid_proceso,
                        obj.fcoc_vnro_depo_detraccion,
                        obj.fcoc_sfecha_depo_detraccion,
                        obj.fcoc_nmonto_total_detalle,
                        obj.fcoc_isituacion,
                        obj.intUsuario,
                        obj.strPc,
                        obj.fcoc_anio,
                        obj.prep_icod_presupuesto,
                        obj.fcoc_flag_importacion,
                        obj.ococ_icod_orden_compra,
                        obj.fcoc_iestado_recep
                    );

                }
                return Convert.ToInt32(idDocCompra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFacCompraImportacion(EFacturaCompra obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_FACTURA_COMPRA_MODIFICAR(
                        obj.fcoc_icod_doc,
                        obj.proc_icod_proveedor,
                        obj.fcoc_num_doc,
                        obj.fcoc_sfecha_doc,
                        obj.doxpc_icod_correlativo,
                        obj.fcoc_svencimiento,
                        obj.tablc_iid_tipo_moneda,
                        obj.fcoc_iforma_pago,
                        obj.fcoc_vreferencia,
                        obj.fcoc_nporcent_imp_doc,
                        obj.fcoc_nmonto_destino_gravado,
                        obj.fcoc_nmonto_destino_mixto,
                        obj.fcoc_nmonto_destino_nogravado,
                        obj.fcoc_nmonto_nogravado,
                        obj.fcoc_nmonto_imp_destino_gravado,
                        obj.fcoc_nmonto_imp_destino_mixto,
                        obj.fcoc_nmonto_imp_destino_nogravado,
                        obj.fcoc_nmonto_tipo_cambio,
                        obj.fcoc_imes_iid_proceso,
                        obj.fcoc_vnro_depo_detraccion,
                        obj.fcoc_sfecha_depo_detraccion,
                        obj.fcoc_nmonto_total_detalle,
                        obj.fcoc_isituacion,
                        obj.intUsuario,
                        obj.strPc,
                        obj.fcoc_anio,
                        obj.ococ_icod_orden_compra,
                        obj.fcoc_iestado_recep
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFacCompraImportacion(EFacturaCompra obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_FACTURA_COMPRA_ELIMINAR(
                        obj.fcoc_icod_doc,
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
        #endregion
        #region Fac.Compra Importacion Det
        public void insertarFacCompraImportacionDet(EFacturaCompraDet obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_FACTURA_COMPRA_DET_INSERTAR(
                        obj.fcoc_icod_doc,
                        obj.fcod_iitem,
                        obj.prd_icod_producto,
                        obj.fcod_ncantidad,
                        obj.fcod_nmonto_unit,
                        obj.fcod_nmonto_total,
                        obj.fcod_vdescripcion_item,
                        obj.intUsuario,
                        obj.strPc,
                        obj.fcod_nmonto_unit_costo,
                        obj.fcod_nmonto_total_costo

                    );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFacCompraNacionalDet(EFacturaCompraDet obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_FACTURA_COMPRA_NACIONAL_DET_MODIFICAR(
                        obj.fcod_icod_doc,
                        obj.fcoc_icod_doc,
                        obj.fcod_iitem,
                        obj.prd_icod_producto,
                        obj.fcod_ncantidad,
                        obj.fcod_nmonto_unit,
                        obj.fcod_nmonto_total,
                        obj.fcod_ndescuento,
                        obj.fcod_icod_kardex,
                        obj.fcod_vdescripcion_item,
                        obj.ocod_icod_detalle_oc,
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
        public void eliminarFacCompraImportacionDet(EFacturaCompraDet obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {

                    dc.SGEC_FACTURA_COMPRA_DET_ELIMINAR(
                        obj.fcod_icod_doc,
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
        #endregion
        #region Percepción Cab.
        public List<EPercepcion> listarPercepcionCab(int intEjercicio)
        {
            List<EPercepcion> lista = new List<EPercepcion>(); ;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EPercepcion>();
                    var query = dc.SGE_EXTEND_PERCEPCION_CAB_LISTAR(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EPercepcion()
                        {
                            percc_icod_percepcion = item.percc_icod_percepcion,
                            percc_vnro_percepcion = item.percc_vnro_percepcion,
                            percc_sfecha_percepcion = Convert.ToDateTime(item.percc_sfecha_percepcion),
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            percc_nmonto_cobrado = Convert.ToDecimal(item.percc_nmonto_cobrado),
                            percc_nmonto_percibido = Convert.ToDecimal(item.percc_nmonto_percibido),
                            percc_tipo_cambio = Convert.ToDecimal(item.percc_tipo_cambio),
                            tablc_iid_situacion = Convert.ToInt32(item.tablc_iid_situacion),
                            strProveedor = item.strProveedor,
                            strMoneda = item.strMoneda,
                            strSituacion = item.strSituacion,
                            percc_icod_dxp = Convert.ToInt64(item.percc_icod_dxp),
                            intSituacionDXP = item.intSituacionDXP,
                            strSituacionDXP = item.strSituacionDXP
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
        public int insertarPercepcionCab(EPercepcion obj)
        {

            try
            {
                int? idDocCompra = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PERCEPCION_CAB_INSERTAR(
                        ref idDocCompra,
                        obj.percc_vnro_percepcion,
                        obj.percc_sfecha_percepcion,
                        obj.proc_icod_proveedor,
                        obj.tablc_iid_tipo_moneda,
                        obj.percc_nmonto_cobrado,
                        obj.percc_nmonto_percibido,
                        obj.percc_tipo_cambio,
                        obj.tablc_iid_situacion,
                        obj.percc_icod_dxp,
                        obj.intUsuario,
                        obj.strPc
                    );

                }
                return Convert.ToInt32(idDocCompra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPercepcionCab(EPercepcion obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PERCEPCION_CAB_MODIFICAR(
                        obj.percc_icod_percepcion,
                        obj.percc_vnro_percepcion,
                        obj.percc_sfecha_percepcion,
                        obj.proc_icod_proveedor,
                        obj.tablc_iid_tipo_moneda,
                        obj.percc_nmonto_cobrado,
                        obj.percc_nmonto_percibido,
                        obj.percc_tipo_cambio,
                        obj.tablc_iid_situacion,
                        obj.percc_icod_dxp,
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
        public void eliminarPercepcionCab(EPercepcion obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PERCEPCION_CAB_ELIMINAR(
                        obj.percc_icod_percepcion,
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
        #endregion
        #region Percepción Det.
        public List<EPercepcionDet> listarPercepcionDet(int intPercepcion)
        {
            List<EPercepcionDet> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EPercepcionDet>();
                    var query = dc.SGE_PERCEPCION_DET_LISTAR(intPercepcion);
                    foreach (var item in query)
                    {
                        lista.Add(new EPercepcionDet()
                        {
                            percd_icod_detalle = item.percd_icod_detalle,
                            percc_icod_percepcion = Convert.ToInt32(item.percc_icod_percepcion),
                            tdoc_icod_tipo_documento = Convert.ToInt32(item.tdoc_icod_tipo_documento),
                            percd_icod_dxp = Convert.ToInt64(item.percd_icod_dxp),
                            percd_vnro_doc = item.percd_vnro_doc,
                            percd_sfecha_doc = Convert.ToDateTime(item.percd_sfecha_doc),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            percd_nmonto_doc = Convert.ToDecimal(item.percd_nmonto_doc),
                            percd_nmonto_percibido_doc = Convert.ToDecimal(item.percd_nmonto_percibido_doc),
                            strMoneda = item.strMoneda,
                            strTipoDoc = item.strTipoDoc,
                            tdodc_iid_correlativo = Convert.ToInt32(item.tdodc_iid_correlativo),
                            intAnalitica = Convert.ToInt32(item.intAnalitica),
                            strCodAnalitica = item.strCodAnalitica
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
        public int insertarPercepcionDet(EPercepcionDet obj)
        {
            try
            {
                int? idDocCompra = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_PERCEPCION_DET_INSERTAR(
                    ref idDocCompra,
                    obj.percc_icod_percepcion,
                    obj.tdoc_icod_tipo_documento,
                    obj.percd_icod_dxp,
                    obj.percd_vnro_doc,
                    obj.percd_sfecha_doc,
                    obj.tablc_iid_tipo_moneda,
                    obj.percd_nmonto_doc,
                    obj.percd_nmonto_percibido_doc,
                    obj.intUsuario,
                    obj.strPc
                    );

                }
                return Convert.ToInt32(idDocCompra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPercepcionDet(EPercepcionDet obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_PERCEPCION_DET_MODIFICAR(
                        obj.percd_icod_detalle,
                        obj.percd_nmonto_percibido_doc,
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
        public void eliminarPercepcionDet(EPercepcionDet obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_PERCEPCION_DET_ELIMINAR(
                        obj.percd_icod_detalle,
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
        #endregion
        #region Nota Crédito Proveedor
        public List<ENotaCreditoProvedor> listarNotaCreditoDetCuentas(int intDocCompra)
        {
            List<ENotaCreditoProvedor> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<ENotaCreditoProvedor>();
                    var query = dc.SGEC_NOTA_CREDITO_COMPRA_PROV_DETALLE_CUENTAS_LISTAR(intDocCompra);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaCreditoProvedor()
                        {
                            ncpd_iitem = Convert.ToInt32(item.ncpd_iitem),
                            strCodLinea = item.famic_vabreviatura,
                            strLinea = item.famic_vdescripcion,
                            intCtaContable = item.ctacc_icod_cuenta_contable_compra,
                            flagCCosto = item.ctacc_iccosto,
                            intTipoAnalitica = item.tablc_iid_tipo_analitica,
                            intAnaliticaProveedor = Convert.ToInt32(item.intAnaliticaProveedor),
                            ncpd_vdescripcion_item = item.ncpd_vdescripcion_item,
                            ncpd_nmonto_total = Convert.ToDecimal(item.dcmlTotalCuenta)
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
        public List<ENotaCreditoProvedor> listarNotaCreditoProveedor(int intEjercicio)
        {
            List<ENotaCreditoProvedor> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<ENotaCreditoProvedor>();
                    var query = dc.SGEC_NOTA_CREDITO_COMPRA_CAB_LISTAR(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaCreditoProvedor()
                        {
                            ncpc_icod_nota_cred = item.ncpc_icod_nota_cred,
                            tdodc_iid_clase_nota_cred = Convert.ToInt32(item.tdodc_iid_clase_nota_cred),
                            ncpc_nro_nota_cred = item.ncpc_nro_nota_cred,
                            ncpc_fecha_nota_cred = Convert.ToDateTime(item.ncpc_fecha_nota_cred),
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            ncpc_tipo_doc_ref_doc = item.ncpc_tipo_doc_ref_doc,
                            ncpc_nro_doc_ref_doc = item.ncpc_nro_doc_ref_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            ncpc_tipo_cambio = Convert.ToDecimal(item.ncpc_tipo_cambio),
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            ncpc_mes_proceso = Convert.ToInt32(item.ncpc_mes_proceso),
                            tablc_iid_situacion = Convert.ToInt32(item.tablc_iid_situacion),
                            ncpc_nmonto_neto_doc = Convert.ToDecimal(item.ncpc_nmonto_neto_doc),
                            ncpc_nporcent_imp_doc = Convert.ToDecimal(item.ncpc_nporcent_imp_doc),
                            ncpc_nmonto_total_doc = Convert.ToDecimal(item.ncpc_nmonto_total_doc),
                            ncpc_nmonto_destino_gravado = Convert.ToDecimal(item.ncpc_nmonto_destino_gravado),
                            ncpc_nmonto_destino_mixto = Convert.ToDecimal(item.ncpc_nmonto_destino_mixto),
                            ncpc_nmonto_destino_nogravado = Convert.ToDecimal(item.ncpc_nmonto_destino_nogravado),
                            ncpc_nmonto_adq_nogravado = Convert.ToDecimal(item.ncpc_nmonto_adq_nogravado),
                            ncpc_nmonto_imp_destino_gravado = Convert.ToDecimal(item.ncpc_nmonto_imp_destino_gravado),
                            ncpc_nmonto_imp_destino_mixto = Convert.ToDecimal(item.ncpc_nmonto_imp_destino_mixto),
                            ncpc_nmonto_imp_destino_nogravado = Convert.ToDecimal(item.ncpc_nmonto_imp_destino_nogravado),
                            doxpc_icod_correlativo = item.doxpc_icod_correlativo,
                            ncpc_anio = item.ncpc_anio,
                            strProveedor = item.strProveedor,
                            strAlmacen = item.strAlmacen,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            intDXP = Convert.ToInt64(item.intDXP),
                            intClaseNCP = item.intClaseNCP,
                            strClaseNCP = item.strClaseNCP,
                            ncpc_sfecha_referencia = item.ncpc_sfecha_referencia,
                            ncpc_flag_importacion = Convert.ToBoolean(item.ncpc_flag_importacion)
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
        public int insertarNotaCreditoProveedor(ENotaCreditoProvedor oBe)
        {
            try
            {
                int? IdNotaCreditoPago = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_NOTA_CREDITO_COMPRA_CAB_INSERTAR(
                    ref IdNotaCreditoPago,
                    oBe.tdodc_iid_clase_nota_cred,
                    oBe.ncpc_nro_nota_cred,
                    oBe.ncpc_fecha_nota_cred,
                    oBe.proc_icod_proveedor,
                    oBe.ncpc_tipo_doc_ref_doc,
                    oBe.ncpc_nro_doc_ref_doc,
                    oBe.ncpc_sfecha_referencia,
                    oBe.tablc_iid_tipo_moneda,
                    oBe.ncpc_tipo_cambio,
                    oBe.almac_icod_almacen,
                    oBe.ncpc_mes_proceso,
                    oBe.tablc_iid_situacion,
                    oBe.ncpc_nmonto_neto_doc,
                    oBe.ncpc_nporcent_imp_doc,
                    oBe.ncpc_nmonto_total_doc,
                    oBe.ncpc_nmonto_destino_gravado,
                    oBe.ncpc_nmonto_destino_mixto,
                    oBe.ncpc_nmonto_destino_nogravado,
                    oBe.ncpc_nmonto_adq_nogravado,
                    oBe.ncpc_nmonto_imp_destino_gravado,
                    oBe.ncpc_nmonto_imp_destino_mixto,
                    oBe.ncpc_nmonto_imp_destino_nogravado,
                    oBe.doxpc_icod_correlativo,
                    oBe.ncpc_anio,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.ncpc_flag_importacion);
                }

                return Convert.ToInt32(IdNotaCreditoPago);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNotaCreditoProveedor(ENotaCreditoProvedor oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_NOTA_CREDITO_COMPRA_CAB_MODIFICAR(
                        oBe.ncpc_icod_nota_cred,
                        oBe.tdodc_iid_clase_nota_cred,
                        oBe.ncpc_nro_nota_cred,
                        oBe.ncpc_fecha_nota_cred,
                        oBe.proc_icod_proveedor,
                        oBe.ncpc_tipo_doc_ref_doc,
                        oBe.ncpc_nro_doc_ref_doc,
                        oBe.ncpc_sfecha_referencia,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.ncpc_tipo_cambio,
                        oBe.almac_icod_almacen,
                        oBe.ncpc_mes_proceso,
                        oBe.tablc_iid_situacion,
                        oBe.ncpc_nmonto_neto_doc,
                        oBe.ncpc_nporcent_imp_doc,
                        oBe.ncpc_nmonto_total_doc,
                        oBe.ncpc_nmonto_destino_gravado,
                        oBe.ncpc_nmonto_destino_mixto,
                        oBe.ncpc_nmonto_destino_nogravado,
                        oBe.ncpc_nmonto_adq_nogravado,
                        oBe.ncpc_nmonto_imp_destino_gravado,
                        oBe.ncpc_nmonto_imp_destino_mixto,
                        oBe.ncpc_nmonto_imp_destino_nogravado,
                        oBe.ncpc_anio,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.ncpc_flag_importacion);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public decimal DXPobtenerSaldo(long doxpc_icod_correlativo)
        {
            decimal? saldo = 0;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_DXP_GET_SALDO(
                        Convert.ToInt32(doxpc_icod_correlativo),
                        ref saldo);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Convert.ToDecimal(saldo);
        }

        public void eliminarNotaCreditoProveedor(ENotaCreditoProvedor oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_NOTA_CREDITO_COMPRA_CAB_ELIMINAR(
                        oBe.ncpc_icod_nota_cred,
                        oBe.intUsuario,
                        oBe.strPc);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /**/
        public List<ENotaCreditoProveedorDet> listarNotaCreditoProveedorDet(int intDocCompra)
        {
            List<ENotaCreditoProveedorDet> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<ENotaCreditoProveedorDet>();
                    var query = dc.SGEC_NOTA_CREDITO_COMPRA_DET_LISTAR(intDocCompra);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaCreditoProveedorDet()
                        {
                            ncpd_icod_detalle = item.ncpd_icod_detalle,
                            ncpc_icod_nota_cred = Convert.ToInt32(item.ncpc_icod_nota_cred),
                            ncpd_iitem = Convert.ToInt32(item.ncpd_iitem),
                            prd_icod_producto = Convert.ToInt32(item.prd_icod_producto),
                            prd_iid_clasificacion_prod = Convert.ToInt32(item.prd_iid_clasificacion_prod),
                            ncpd_ncantidad = Convert.ToDecimal(item.ncpd_ncantidad),
                            ncpd_nmonto_unit = Convert.ToDecimal(item.ncpd_nmonto_unit),
                            ncpd_nmonto_total = Convert.ToDecimal(item.ncpd_nmonto_total),
                            ncpd_icod_kardex = item.ncpd_icod_kardex,
                            ncpd_vdescripcion_item = item.ncpd_vdescripcion_item,
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
        public void insertarNotaCreditoProveedorDet(ENotaCreditoProveedorDet oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_NOTA_CREDITO_COMPRA_DET_INSERTAR(
                    oBe.ncpc_icod_nota_cred,
                    oBe.ncpd_iitem,
                    oBe.prd_icod_producto,
                    oBe.prd_iid_clasificacion_prod,
                    oBe.ncpd_ncantidad,
                    oBe.ncpd_nmonto_unit,
                    oBe.ncpd_nmonto_total,
                    oBe.ncpd_icod_kardex,
                    oBe.ncpd_vdescripcion_item,
                    oBe.intUsuario,
                    oBe.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNotaCreditoProveedorDet(ENotaCreditoProveedorDet oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_NOTA_CREDITO_COMPRA_DET_MODIFICAR(
                        oBe.ncpd_icod_detalle,
                        oBe.ncpc_icod_nota_cred,
                        oBe.ncpd_iitem,
                        oBe.prd_icod_producto,
                        oBe.prd_iid_clasificacion_prod,
                        oBe.ncpd_ncantidad,
                        oBe.ncpd_nmonto_unit,
                        oBe.ncpd_nmonto_total,
                        oBe.ncpd_icod_kardex,
                        oBe.ncpd_vdescripcion_item,
                        oBe.intUsuario,
                        oBe.strPc);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarNotaCreditoProveedorDet(ENotaCreditoProveedorDet oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_NOTA_CREDITO_COMPRA_DET_ELIMINAR(
                        oBe.ncpd_icod_detalle,
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
        #region Nota Credito Importacion
        public int insertarNotaCreditoProveedorImportacion(ENotaCreditoProvedor oBe)
        {
            try
            {
                int? IdNotaCreditoPago = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_NOTA_CREDITO_COMPRA_IMPORTACION_CAB_INSERTAR(
                    ref IdNotaCreditoPago,
                    oBe.tdodc_iid_clase_nota_cred,
                    oBe.ncpc_nro_nota_cred,
                    oBe.ncpc_fecha_nota_cred,
                    oBe.proc_icod_proveedor,
                    oBe.ncpc_tipo_doc_ref_doc,
                    oBe.ncpc_nro_doc_ref_doc,
                    oBe.ncpc_sfecha_referencia,
                    oBe.tablc_iid_tipo_moneda,
                    oBe.ncpc_tipo_cambio,
                    oBe.almac_icod_almacen,
                    oBe.ncpc_mes_proceso,
                    oBe.tablc_iid_situacion,
                    oBe.ncpc_nmonto_neto_doc,
                    oBe.ncpc_nporcent_imp_doc,
                    oBe.ncpc_nmonto_total_doc,
                    oBe.ncpc_nmonto_destino_gravado,
                    oBe.ncpc_nmonto_destino_mixto,
                    oBe.ncpc_nmonto_destino_nogravado,
                    oBe.ncpc_nmonto_adq_nogravado,
                    oBe.ncpc_nmonto_imp_destino_gravado,
                    oBe.ncpc_nmonto_imp_destino_mixto,
                    oBe.ncpc_nmonto_imp_destino_nogravado,
                    oBe.doxpc_icod_correlativo,
                    oBe.ncpc_anio,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.ncpc_flag_importacion);
                }

                return Convert.ToInt32(IdNotaCreditoPago);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNotaCreditoProveedorImportacion(ENotaCreditoProvedor oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_NOTA_CREDITO_COMPRA_IMPORTACION_CAB_MODIFICAR(
                        oBe.ncpc_icod_nota_cred,
                        oBe.tdodc_iid_clase_nota_cred,
                        oBe.ncpc_nro_nota_cred,
                        oBe.ncpc_fecha_nota_cred,
                        oBe.proc_icod_proveedor,
                        oBe.ncpc_tipo_doc_ref_doc,
                        oBe.ncpc_nro_doc_ref_doc,
                        oBe.ncpc_sfecha_referencia,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.ncpc_tipo_cambio,
                        oBe.almac_icod_almacen,
                        oBe.ncpc_mes_proceso,
                        oBe.tablc_iid_situacion,
                        oBe.ncpc_nmonto_neto_doc,
                        oBe.ncpc_nporcent_imp_doc,
                        oBe.ncpc_nmonto_total_doc,
                        oBe.ncpc_nmonto_destino_gravado,
                        oBe.ncpc_nmonto_destino_mixto,
                        oBe.ncpc_nmonto_destino_nogravado,
                        oBe.ncpc_nmonto_adq_nogravado,
                        oBe.ncpc_nmonto_imp_destino_gravado,
                        oBe.ncpc_nmonto_imp_destino_mixto,
                        oBe.ncpc_nmonto_imp_destino_nogravado,
                        oBe.ncpc_anio,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.ncpc_flag_importacion);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarNotaCreditoProveedorImportacion(ENotaCreditoProvedor oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_NOTA_CREDITO_COMPRA_IMPORTACION_CAB_ELIMINAR(
                        oBe.ncpc_icod_nota_cred,
                        oBe.intUsuario,
                        oBe.strPc);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /**/
        public void insertarNotaCreditoProveedorDetImportacion(ENotaCreditoProveedorDet oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_NOTA_CREDITO_COMPRA_IMPORTACION_DET_INSERTAR(
                    oBe.ncpc_icod_nota_cred,
                    oBe.ncpd_iitem,
                    oBe.prd_icod_producto,
                    oBe.prd_iid_clasificacion_prod,
                    oBe.ncpd_ncantidad,
                    oBe.ncpd_nmonto_unit,
                    oBe.ncpd_nmonto_total,
                    oBe.ncpd_icod_kardex,
                    oBe.ncpd_vdescripcion_item,
                    oBe.intUsuario,
                    oBe.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNotaCreditoProveedorDetImportacion(ENotaCreditoProveedorDet oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_NOTA_CREDITO_COMPRA_IMPORTACION_DET_MODIFICAR(
                        oBe.ncpd_icod_detalle,
                        oBe.ncpc_icod_nota_cred,
                        oBe.ncpd_iitem,
                        oBe.prd_icod_producto,
                        oBe.prd_iid_clasificacion_prod,
                        oBe.ncpd_ncantidad,
                        oBe.ncpd_nmonto_unit,
                        oBe.ncpd_nmonto_total,
                        oBe.ncpd_icod_kardex,
                        oBe.ncpd_vdescripcion_item,
                        oBe.intUsuario,
                        oBe.strPc);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarNotaCreditoProveedorDetImportacion(ENotaCreditoProveedorDet oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_NOTA_CREDITO_COMPRA_IMPORTACION_DET_ELIMINAR(
                        oBe.ncpd_icod_detalle,
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
        #region "Concepto Presupuesto Nacional"

        public List<EConceptoPresupuestoNacional> ListarConceptoPresupuestoNacional()
        {
            List<EConceptoPresupuestoNacional> lista = new List<EConceptoPresupuestoNacional>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEC_CONCEPTO_PRESUPUESTO_NACIONAL_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EConceptoPresupuestoNacional()
                        {

                            cpn_icod_concepto_nacional = item.cpn_icod_concepto_nacional,
                            cpn_iid_concepto_nacional = item.cpn_iid_concepto_nacional,
                            cpn_vdescripcion_concepto_nacional = item.cpn_vdescripcion_concepto_nacional,
                            cpn_iid_situacion_concepto_nacional = item.cpn_iid_situacion_concepto_nacional,
                            cpn_iusuario_crea = item.cpn_iusuario_crea,
                            cpn_sfecha_crea = item.cpn_sfecha_crea,
                            cpn_vpc_crea = item.cpn_vpc_crea,
                            cpn_iusuario_modifica = item.cpn_iusuario_modifica,
                            cpn_sfecha_modifica = item.cpn_sfecha_modifica,
                            cpn_vpc_modifica = item.cpn_vpc_modifica,
                            cpn_viid_concepto_nacional = string.Format("{0:00}", item.cpn_iid_concepto_nacional)
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

        public void InsertarConceptoPresupuestoNacional(EConceptoPresupuestoNacional obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_CONCEPTO_PRESUPUESTO_NACIONAL_INSERTAR(
                        obj.cpn_iid_concepto_nacional,
                        obj.cpn_vdescripcion_concepto_nacional,
                        obj.cpn_iusuario_crea,
                        obj.cpn_vpc_crea);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarConceptoPresupuestoNacional(EConceptoPresupuestoNacional obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_CONCEPTO_PRESUPUESTO_NACIONAL_MODIFICAR(
                        obj.cpn_icod_concepto_nacional,
                        obj.cpn_vdescripcion_concepto_nacional,
                        obj.cpn_iusuario_modifica,
                        obj.cpn_vpc_modifica);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarConceptoPresupuestoNacional(EConceptoPresupuestoNacional obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_CONCEPTO_PRESUPUESTO_NACIONAL_ELIMINAR(obj.cpn_icod_concepto_nacional);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
        #region "Concepto Presupuesto Nacional Detalle"


        public DataTable ConceptoPresupuestoNacionalReporte()
        {
            DataTable dtResultado = null;

            using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
            {
                var query = dc.SGEC_CONCEPTO_PRESUPUESTO_NACIONAL_REPORTE();
                Convertir convierte = new Convertir();
                dtResultado = new DataTable();
                dtResultado = convierte.ConvertirADataTable(query);
                return dtResultado;
            }
        }

        public List<EConceptoPresupuestoNacionalDetalle> ListarConceptoPresupuestoNacionalDetalle(EConceptoPresupuestoNacional obj)
        {
            List<EConceptoPresupuestoNacionalDetalle> lista = new List<EConceptoPresupuestoNacionalDetalle>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEC_CONCEPTO_PRESUPUESTO_NACIONAL_DETALLE_LISTAR(obj.cpn_icod_concepto_nacional);
                    foreach (var item in query)
                    {
                        lista.Add(new EConceptoPresupuestoNacionalDetalle()
                        {
                            cpnd_icod_detalle_nacional = item.cpnd_icod_detalle_nacional,
                            cpn_icod_concepto_nacional = item.cpn_icod_concepto_nacional,
                            cpnd_iid_detalle_nacional = Convert.ToInt32(item.cpnd_iid_detalle_nacional),
                            cpnd_viid_detalle_nacional = string.Format("{0:00}", item.cpnd_iid_detalle_nacional),
                            cpnd_vdescripcion = item.cpnd_vdescripcion,
                            ctacc_vnumero_cuenta_contable = item.ctacc_numero_cuenta_contable,
                            ctacc_vnombre_descripcion_larga = item.ctacc_nombre_descripcion,
                            ctacc_iid_cuenta_contable = item.ctacc_iid_cuenta_contable,
                            cpnd_iid_situacion_detalle = Convert.ToInt32(item.cpnd_iid_situacion_detalle),
                            cpnd_iusuario_crea = Convert.ToInt32(item.cpnd_iusuario_crea),
                            cpnd_sfecha_crea = item.cpnd_sfecha_crea,
                            cpnd_vpc_crea = item.cpnd_vpc_crea,
                            cpnd_iusuario_modifica = Convert.ToInt32(item.cpnd_iusuario_modifica),
                            cpnd_sfecha_modifica = item.cpnd_sfecha_modifica,
                            cpnd_vpc_modifica = item.cpnd_vpc_modifica,
                            cpnd_flag_estado = Convert.ToBoolean(item.cpnd_flag_estado)
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

        public void InsertarConceptoPresupuestoNacionalDetalle(EConceptoPresupuestoNacionalDetalle obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_CONCEPTO_PRESUPUESTO_NACIONAL_DETALLE_INSERTAR(
                        obj.cpn_icod_concepto_nacional,
                        obj.cpnd_iid_detalle_nacional,
                        obj.cpnd_vdescripcion,
                        obj.cpnd_iid_situacion_detalle,
                        obj.ctacc_iid_cuenta_contable,
                        obj.cpnd_iusuario_crea,
                        obj.cpnd_vpc_crea,
                        obj.cpnd_flag_estado
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarConceptoPresupuestoNacionalDetalle(EConceptoPresupuestoNacionalDetalle obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_CONCEPTO_PRESUPUESTO_NACIONAL_DETALLE_ACTUALIZAR(
                        obj.cpnd_icod_detalle_nacional,
                        obj.cpn_icod_concepto_nacional,
                        obj.cpnd_iid_detalle_nacional,
                        obj.cpnd_vdescripcion,
                        obj.cpnd_iid_situacion_detalle,
                        obj.ctacc_iid_cuenta_contable,
                        obj.cpnd_iusuario_modifica,
                        obj.cpnd_vpc_modifica,
                        obj.cpnd_flag_estado
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarConceptoPresupuestoNacionalDetalle(EConceptoPresupuestoNacionalDetalle obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_CONCEPTO_PRESUPUESTO_NACIONAL_DETALLE_ELIMINAR(obj.cpnd_icod_detalle_nacional);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
        #region "Presupuesto Nacional"

        public List<EPresupuestoNacional> ListarPresupuestoNacional(int Periodo, int IdTipo)
        {
            List<EPresupuestoNacional> lista = new List<EPresupuestoNacional>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEC_PRESUPUESTO_NACIONAL_LISTAR(Periodo, IdTipo);
                    foreach (var item in query)
                    {
                        lista.Add(new EPresupuestoNacional()
                        {
                            prep_icod_presupuesto = item.prep_icod_presupuesto,
                            prep_iid_anio = item.prep_iid_anio,
                            prep_cod_presupuesto = item.prep_cod_presupuesto,
                            prep_sfecha_presupuesto = item.prep_sfecha_presupuesto,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            TipoMoneda = item.TipoMoneda,
                            pespc_icod_producto_especifico = item.pespc_icod_producto_especifico,
                            prep_vconcepto = item.prep_vconcepto,
                            unidc_vabreviatura_unidad_medida = item.unidc_vabreviatura_unidad_medida,
                            situc_vdescripcion = item.situc_vdescripcion,
                            prep_ncant_facturada = item.prep_ncant_facturada,
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            almac_vdescripcion = item.almac_vdescripcion,
                            prep_ncant_presupuesto = Convert.ToDecimal(item.prep_ncant_presupuesto),
                            prep_ncant_recibida = Convert.ToDecimal(item.prep_ncant_recibida),
                            prep_ntipo_cambio = Convert.ToDecimal(item.prep_ntipo_cambio),
                            cecoc_icod_centro_costo = Convert.ToInt32(item.cecoc_icod_centro_costo),
                            prep_nmont_total = Convert.ToDecimal(item.prep_nmont_total),
                            prep_nmont_unit_prod = Convert.ToDecimal(item.prep_nmont_unit_prod),
                            prep_nmont_tot_ejecut = Convert.ToDecimal(item.prep_nmont_tot_ejecut),
                            prep_nmont_unit_ejecut = Convert.ToDecimal(item.prep_nmont_unit_ejecut),
                            prep_sfecha_cierre = item.prep_sfecha_cierre,
                            prep_tipo_nacional = Convert.ToInt32(item.prep_tipo_nacional),
                            TipoPresupuestoNacional = item.TipoPresupuestoNacional,
                            prep_isituacion = Convert.ToInt32(item.prep_isituacion),
                            krdx_icod_kardex = Convert.ToInt64(item.krdx_icod_kardex),
                            krdx_sfecha_kardex = item.krdx_sfecha_kardex,
                            SituacionDoc = item.SituacionDoc,
                            prep_vreferencia = item.prep_vreferencia,
                            agm_icod_maritimo = Convert.ToInt32(item.agm_icod_maritimo),
                            add_icod_aduana = Convert.ToInt32(item.add_icod_aduana),
                            mnv_icod_motonave = Convert.ToInt32(item.mnv_icod_motonave),
                            mnv_vdescripcion = item.mnv_vdescripcion,
                            prep_fecha_llegada = item.prep_fecha_llegada,
                            prep_npeso_total = item.prep_npeso_total,
                            prep_nvolumen_m3 = item.prep_nvolumen_m3,
                            prep_ncantidad_bultos = item.prep_ncantidad_bultos
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

        public void ActualizarMontoNacional(EPresupuestoNacional obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PRESUPUESTO_NACIONAL_ACTUALIZAR_MONTO_NACIONAL(
                        obj.prep_icod_presupuesto,
                        obj.prep_nmont_total,
                        obj.prep_nmont_unit_prod
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertarPresupuestoNacional(EPresupuestoNacional obj)
        {
            try
            {
                int? IdPresupuestoNacional = 0;

                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PRESUPUESTO_NACIONAL_INSERTAR(
                        ref IdPresupuestoNacional,
                        obj.prep_iid_anio,
                        obj.prep_cod_presupuesto,
                        obj.prep_sfecha_presupuesto,
                        obj.tablc_iid_tipo_moneda,
                        obj.pespc_icod_producto_especifico,
                        obj.prep_vconcepto,
                        obj.almac_icod_almacen,
                        obj.prep_ncant_presupuesto,
                        obj.prep_ncant_recibida,
                        obj.prep_ncant_facturada,
                        obj.prep_ntipo_cambio,
                        obj.cecoc_icod_centro_costo,
                        obj.prep_nmont_total,
                        obj.prep_nmont_unit_prod,
                        obj.prep_nmont_tot_ejecut,
                        obj.prep_nmont_unit_ejecut,
                        obj.prep_sfecha_cierre,
                        obj.prep_tipo_nacional,
                        obj.prep_isituacion,
                        obj.krdx_icod_kardex,
                        obj.prep_vreferencia,
                        obj.krdx_sfecha_kardex,
                        obj.prep_iusuario_crea,
                        obj.prep_vpc_crea,
                        obj.prep_flag_estado,
                        obj.agm_icod_maritimo,
                        obj.add_icod_aduana,
                        obj.mnv_icod_motonave,
                        obj.prep_fecha_llegada,
                        obj.prep_npeso_total,
                        obj.prep_nvolumen_m3,
                        obj.prep_ncantidad_bultos
                        );
                }

                return Convert.ToInt32(IdPresupuestoNacional);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ActualizarPresupuestoNacional(EPresupuestoNacional obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PRESUPUESTO_NACIONAL_ACTUALIZAR(
                        obj.prep_icod_presupuesto,
                        obj.prep_iid_anio,
                        obj.prep_cod_presupuesto,
                        obj.prep_sfecha_presupuesto,
                        obj.tablc_iid_tipo_moneda,
                        obj.pespc_icod_producto_especifico,
                        obj.prep_vconcepto,
                        obj.almac_icod_almacen,
                        obj.prep_ncant_presupuesto,
                        obj.prep_ncant_recibida,
                        obj.prep_ntipo_cambio,
                        obj.cecoc_icod_centro_costo,
                        obj.prep_nmont_total,
                        obj.prep_nmont_unit_prod,
                        obj.prep_nmont_tot_ejecut,
                        obj.prep_nmont_unit_ejecut,
                        obj.prep_sfecha_cierre,
                        obj.prep_tipo_nacional,
                        obj.prep_isituacion,
                        obj.krdx_icod_kardex,
                        obj.prep_vreferencia,
                        obj.krdx_sfecha_kardex,
                        obj.prep_iusuario_modifica,
                        obj.prep_vpc_modifica,
                        obj.prep_flag_estado,
                        obj.agm_icod_maritimo,
                        obj.add_icod_aduana,
                        obj.mnv_icod_motonave,
                        obj.prep_fecha_llegada,
                        obj.prep_npeso_total,
                        obj.prep_nvolumen_m3,
                        obj.prep_ncantidad_bultos
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarPresupuestoNacionalKardex(EPresupuestoNacional obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PRESUPUESTO_NACIONAL_ACTUALIZAR_KARDEX(
                        obj.prep_icod_presupuesto,
                        Convert.ToInt64(obj.krdx_icod_kardex),
                        obj.krdx_sfecha_kardex,
                        obj.prep_isituacion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarCierrePresupuestoNacional(int IdPresupuestoNacional, DateTime FechaCierre)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PRESUPUESTO_NACIONAL_ACTUALIZAR_CIERRE(IdPresupuestoNacional, FechaCierre);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarPresupuestoNacional(EPresupuestoNacional obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PRESUPUESTO_NACIONAL_ELIMINAR(obj.prep_icod_presupuesto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AnularPresupuestoNacional(int IdPresupuestoNacional, long IdKardex)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PRESUPUESTO_NACIONAL_ANULAR(IdPresupuestoNacional, IdKardex);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarPresupuestoNacionalDetalleMontoEjecuta(int prepd_icod_detalle, int prep_icod_presupuesto)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PRESUPUESTO_NACIONAL_DETALLE_ACTUALIZAR_MONTOS(prepd_icod_detalle, prep_icod_presupuesto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarPresupuestoNacionalMontoEjecuta(int prepd_icod_detalle, int prep_icod_presupuesto)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PRESUPUESTO_NACIONAL_ACTUALIZAR_MONTOS(prepd_icod_detalle, prep_icod_presupuesto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int NumeroCorrelativoPresupuestoNacional(int anio)
        {
            int numCorrelativo = 0;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEC_PRESUPUESTO_NACIONAL_NUMERO_CORRELATIVO(anio);
                    foreach (var item in query)
                    {
                        numCorrelativo = Convert.ToInt32(item.numeroCorrelativo);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return numCorrelativo;
        }

        #endregion
        #region "Presupuesto Nacional Detalle"

        public List<EPresupuestoNacionalDetalle> ListarNacionalPlantilla()
        {
            List<EPresupuestoNacionalDetalle> lista = new List<EPresupuestoNacionalDetalle>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEC_PRESUPUESTO_NACIONAL_PLANTILLA();
                    foreach (var item in query)
                    {
                        lista.Add(new EPresupuestoNacionalDetalle()
                        {
                            prepd_icod_detalle = item.prepd_icod_detalle,
                            prep_icod_presupuesto = Convert.ToInt32(item.prep_icod_presupuesto),
                            cpn_icod_concepto_nacional = Convert.ToInt32(item.cpn_icod_concepto_nacional),
                            cpn_vdescripcion_concepto_nacional = item.cpn_vdescripcion_concepto_nacional,
                            cpnd_icod_detalle_nacional = Convert.ToInt32(item.cpnd_icod_detalle_nacional),
                            cpnd_vdescripcion = item.cpnd_vdescripcion,
                            prepd_nmont_tot_concepto = Convert.ToDecimal(item.prepd_nmont_tot_concepto),
                            prepd_nmont_unit_concepto = Convert.ToDecimal(item.prepd_nmont_unit_concepto),
                            tablc_iid_tipo_moneda_origen = Convert.ToInt32(item.tablc_iid_tipo_moneda_origen),
                            TipoMoneda = item.TipoMoneda,
                            prepd_nmont_tot_concepto_origen = Convert.ToInt32(item.prepd_nmont_tot_concepto_origen),
                            prepd_nmont_tot_ejecut = Convert.ToDecimal(item.prepd_nmont_tot_ejecut),
                            prepd_nmont_unit_ejecut = Convert.ToDecimal(item.prepd_nmont_unit_ejecut),
                            strCod = item.strCod,
                            TipOper = 4,//Consultar
                            impd_nmonto_concepto_dol = item.impd_nmonto_concepto_dol,
                            impd_nmonto_concepto_sol = item.impd_nmonto_concepto_sol
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

        public List<EPresupuestoNacionalDetalle> ListarPresupuestoNacionalDetalle(int IdPresupuestoNacional)
        {
            List<EPresupuestoNacionalDetalle> lista = new List<EPresupuestoNacionalDetalle>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEC_PRESUPUESTO_NACIONAL_DETALLE_LISTAR(IdPresupuestoNacional);
                    foreach (var item in query)
                    {
                        lista.Add(new EPresupuestoNacionalDetalle()
                        {
                            prepd_icod_detalle = item.prepd_icod_detalle,
                            prep_icod_presupuesto = Convert.ToInt32(item.prep_icod_presupuesto),
                            cpn_icod_concepto_nacional = Convert.ToInt32(item.cpn_icod_concepto_nacional),
                            cpn_vdescripcion_concepto_nacional = item.cpn_vdescripcion_concepto_nacional,
                            cpnd_icod_detalle_nacional = Convert.ToInt32(item.cpnd_icod_detalle_nacional),
                            cpnd_vdescripcion = item.cpnd_vdescripcion,
                            prepd_nmont_tot_concepto = Convert.ToDecimal(item.prepd_nmont_tot_concepto),
                            prepd_nmont_unit_concepto = Convert.ToDecimal(item.prepd_nmont_unit_concepto),
                            tablc_iid_tipo_moneda_origen = Convert.ToInt32(item.tablc_iid_tipo_moneda_origen),
                            TipoMoneda = item.TipoMoneda,
                            prepd_nmont_tot_concepto_origen = Convert.ToInt32(item.prepd_nmont_tot_concepto_origen),
                            prepd_nmont_tot_ejecut = Convert.ToDecimal(item.prepd_nmont_tot_ejecut),
                            prepd_nmont_unit_ejecut = Convert.ToDecimal(item.prepd_nmont_unit_ejecut),
                            TipOper = item.prepd_icod_detalle == 0 ? 1 : 4, //Consultar
                            ctacc_iid_cuenta_contable = Convert.ToInt32(item.ctacc_iid_cuenta_contable),
                            ctacc_vnumero_cuenta_contable = item.ctacc_numero_cuenta_contable,
                            ctacc_vnombre_descripcion_larga = item.ctacc_nombre_descripcion
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

        public void InsertarPresupuestoNacionalDetalle(EPresupuestoNacionalDetalle obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {

                    dc.SGEC_PRESUPUESTO_NACIONAL_DETALLE_INSERTAR(
                        obj.prep_icod_presupuesto,
                        obj.cpn_icod_concepto_nacional,
                        obj.cpnd_icod_detalle_nacional,
                        obj.prepd_nmont_tot_concepto,
                        obj.prepd_nmont_unit_concepto,
                        obj.tablc_iid_tipo_moneda_origen,
                        obj.prepd_nmont_tot_concepto_origen,
                        obj.prepd_nmont_tot_ejecut,
                        obj.prepd_nmont_unit_ejecut,
                        obj.prepd_iusuario_crea,
                        obj.prepd_vpc_crea,
                        obj.prepd_flag_estado
                        );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarPresupuestoNacionalDetalle(EPresupuestoNacionalDetalle obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PRESUPUESTO_NACIONAL_DETALLE_ACTUALIZAR(
                        obj.prepd_icod_detalle,
                            obj.prep_icod_presupuesto,
                            obj.cpn_icod_concepto_nacional,
                            obj.cpnd_icod_detalle_nacional,
                            obj.prepd_nmont_tot_concepto,
                            obj.prepd_nmont_unit_concepto,
                            obj.tablc_iid_tipo_moneda_origen,
                            obj.prepd_nmont_tot_concepto_origen,
                            obj.prepd_nmont_tot_ejecut,
                            obj.prepd_nmont_unit_ejecut,
                            obj.prepd_iusuario_modifica,
                            obj.prepd_vpc_modifica,
                            obj.prepd_flag_estado
                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarPresupuestoNacionalDetalle(EPresupuestoNacionalDetalle obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PRESUPUESTO_NACIONAL_DETALLE_ELIMINAR(obj.prep_icod_presupuesto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region "Motonaves"

        public List<EMotonaves> ListarMotonaves()
        {
            List<EMotonaves> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EMotonaves>();
                    var query = dc.SGEC_MOTONAVE_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EMotonaves()
                        {
                            idd_icod_motonaves = item.mnv_icod_motonave,
                            idd_motonaves = item.mnv_iid_motonave,
                            vidd_motonaves = String.Format("{0:0000}", item.mnv_iid_motonave),
                            Descripcion = item.mnv_vdescripcion,
                            estado = Convert.ToInt32(item.mnv_estado)
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

        public void MotonavesInsertar(EMotonaves obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_MOTONAVE_INSERTAR(
                    Convert.ToInt32(obj.idd_motonaves),
                    obj.Descripcion,
                    obj.estado,
                    obj.usuario_crea,
                    obj.usuario_pc_crea
                    );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarMotonaves(EMotonaves obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_MOTONAVE_MODIFICAR(
                    obj.idd_icod_motonaves,
                     Convert.ToInt32(obj.idd_motonaves),
                    obj.Descripcion,
                    obj.estado,
                    obj.usuario_modifica,
                    obj.usuario_pc_modifica);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarMotonaves(EMotonaves obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_MOTONAVE_ELIMINAR(obj.idd_icod_motonaves);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
        #region "Agencia Aduana"

        public List<EAgenciaAduana> ListarAgenciaAduana()
        {
            List<EAgenciaAduana> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EAgenciaAduana>();
                    var query = dc.SGEC_AGENCIA_ADUANA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EAgenciaAduana()
                        {
                            idd_icod_aduana = item.add_icod_aduana,
                            idd_aduana = item.add_iid_aduana,
                            vidd_aduana = String.Format("{0:000}", item.add_iid_aduana),
                            razon = item.add_vrazon,
                            Direccion = item.add_vDireccion,
                            telefono = item.add_vtelefono,
                            email = item.add_vemail,
                            ruc = item.add_vruc,
                            estado = Convert.ToInt32(item.add_estado)

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

        public void AgenciaAduanaInsertar(EAgenciaAduana obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_AGENCIA_ADUANA_INSERTAR(
                    Convert.ToInt32(obj.idd_aduana),
                    obj.razon,
                    obj.Direccion,
                    obj.telefono,
                    obj.email,
                    obj.ruc,
                    obj.estado,
                    obj.usuario_crea,
                    obj.add_vpc_crea
                    );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarAgenciaAduana(EAgenciaAduana obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_AGENCIA_ADUANA_ACTUALIZAR(
                    obj.idd_icod_aduana,
                    Convert.ToInt32(obj.idd_aduana),
                    obj.razon,
                    obj.Direccion,
                    obj.telefono,
                    obj.email,
                    obj.ruc,
                    obj.estado,
                    obj.usuario_modifica,
                    obj.add_vpc_modifica
                    );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarAgenciaAduana(EAgenciaAduana obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_AGENCIA_ADUANA_ELIMINAR(obj.idd_icod_aduana);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
        #region "Agente Marítimo"

        public List<EAgenteMaritimo> ListarAgenteMaritimo()
        {
            List<EAgenteMaritimo> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EAgenteMaritimo>();
                    var query = dc.SGEC_AGENTE_MARITIMO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EAgenteMaritimo()
                        {
                            agm_icod_maritimo = item.agm_icod_maritimo,
                            agm_iid_maritimo = item.agm_iid_maritimo,
                            agm_viid_maritimo = String.Format("{0:000}", item.agm_iid_maritimo),
                            agm_vrazon = item.agm_vrazon,
                            agm_vDireccion = item.agm_vDireccion,
                            agm_vtelefono = item.agm_vtelefono,
                            agm_vemail = item.agm_vemail,
                            agm_vruc = item.agm_vruc,
                            agm_flag_estado = Convert.ToBoolean(item.agm_flag_estado)

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

        public void AgenteMaritimoInsertar(EAgenteMaritimo obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_AGENTE_MARITIMO_INSERTAR(
                    Convert.ToInt32(obj.agm_iid_maritimo),
                    obj.agm_vrazon,
                    obj.agm_vDireccion,
                    obj.agm_vtelefono,
                    obj.agm_vemail,
                    obj.agm_vruc,
                    obj.agm_iid_usuario_crea,
                    obj.agm_vpc_crea,
                    obj.agm_flag_estado
                    );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarAgenteMaritimo(EAgenteMaritimo obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_AGENTE_MARITIMO_ACTUALIZAR(
                    Convert.ToInt32(obj.agm_icod_maritimo),
                   Convert.ToInt32(obj.agm_iid_maritimo),
                    obj.agm_vrazon,
                    obj.agm_vDireccion,
                    obj.agm_vtelefono,
                    obj.agm_vemail,
                    obj.agm_vruc,
                    obj.agm_iid_usuario_modifica,
                    obj.agm_vpc_modifica,
                    obj.agm_flag_estado
                    );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarAgenteMaritimo(EAgenteMaritimo obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_AGENTE_MARITIMO_ELIMINAR(obj.agm_icod_maritimo);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
        #region Lista de Precio Compra
        public List<EListaPrecioCab> listarPrecioCompra()
        {
            List<EListaPrecioCab> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EListaPrecioCab>();
                    var query = dc.SGEC_LISTA_PRECIOS_PROV_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EListaPrecioCab()
                        {
                            lprec_icod_proveedor = item.lprec_icod_proveedor,
                            edit_icod_editorial = Convert.ToInt32(item.edit_icod_editorial),
                            lprec_Numerolista = item.lprec_Numerolista,
                            lprec_sfecha_lista = Convert.ToDateTime(item.lprec_sfecha_lista),
                            lprec_Observaciones = item.lprec_Observaciones,
                            edit_vdescripcion = item.edit_vdescripcion,
                            proc_vcod_proveedor = item.proc_vcod_proveedor
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

        public int insertarPrecioCompra(EListaPrecioCab obj)
        {

            try
            {
                int? lprec_icod_proveedor = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_LISTA_PRECIOS_PROV_INSERTAR(
                        ref lprec_icod_proveedor,
                        obj.edit_icod_editorial,
                        obj.lprec_Numerolista,
                        obj.lprec_sfecha_lista,
                        obj.lprec_Observaciones,
                        obj.intUsuario,
                        obj.strPc,
                        obj.lprec_sflag_estado
                    );

                }
                return Convert.ToInt32(lprec_icod_proveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPrecioCompra(EListaPrecioCab obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_LISTA_PRECIOS_PROV_MODIFICAR(
                        obj.lprec_icod_proveedor,
                        obj.lprec_sfecha_lista,
                        obj.lprec_Observaciones,
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

        public void eliminarPrecioCompra(EListaPrecioCab obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_LISTA_PRECIOS_PROV_ELIMINAR(
                        obj.lprec_icod_proveedor,
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
        #endregion
        #region Lista de Precio Compra Det
        public List<EListaPrecioDetalle> listarPrecioCompraDet(int lprec_icod_proveedor, int proc_icod_proveedor, int anio)
        {
            List<EListaPrecioDetalle> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EListaPrecioDetalle>();
                    var query = dc.SGEC_LISTA_PRECIOS_PROV_DET_LISTAR(lprec_icod_proveedor, proc_icod_proveedor, anio);
                    foreach (var item in query)
                    {
                        lista.Add(new EListaPrecioDetalle()
                        {
                            lpred_icod_proveedor = item.lpred_icod_proveedor,
                            lprec_icod_proveedor = item.lprec_icod_proveedor,
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            lpred_icod_moneda = item.lpred_icod_moneda,
                            lpred_nprecio_lista = Convert.ToDecimal(item.lpred_nprecio_lista),
                            lpred_nperso_desc = Convert.ToDecimal(item.lpred_nperso_desc),
                            lpred_nprecio_neto = Convert.ToDecimal(item.lpred_nprecio_neto),
                            prdc_vcode_producto = item.prdc_vcode_producto,
                            prdc_vdescripcion_larga = item.prdc_vdescripcion_larga,
                            tarec_vdescripcion = item.tarec_vdescripcion,
                            lpred_vdescripcion_moneda = item.strMoneda,
                            strEditorial = item.edit_vdescripcion,
                            prdc_vAutor = item.prdc_vAutor,
                            intTipoOperacion = item.intOperacion,
                            DescripcionMoneda = item.strMoneda,
                            lpedid_nstock_producto = Convert.ToDecimal(item.lpedid_nstock_producto),
                            lpedid_ncompras_sem1 = item.lpedid_ncompras_sem1,
                            lpedid_ncompras_sem2 = item.lpedid_ncompras_sem2,
                            lpedid_ncompras_sem3 = item.lpedid_ncompras_sem3,
                            lpedid_ncompras_sem4 = item.lpedid_ncompras_sem4,
                            lpedid_bExisteCatalogo = item.prdc_icod_producto == 0 ? false : true
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

        public int insertarPrecioCompraDet(EListaPrecioDetalle obj)
        {

            try
            {
                int? lpred_icod_proveedor = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_LISTA_PRECIOS_PROV_DET_INSERTAR(
                        ref lpred_icod_proveedor,
                        obj.lprec_icod_proveedor,
                        obj.prdc_icod_producto,
                        obj.lpred_icod_moneda,
                        obj.lpred_nprecio_lista,
                        obj.lpred_nperso_desc,
                        obj.lpred_nprecio_neto,
                        obj.intUsuario,
                        obj.strPc,
                        obj.lpred_sflag_estado,
                        obj.prdc_vcode_producto,
                        obj.prdc_vdescripcion_larga,
                        obj.prdc_vAutor,
                        obj.edit_vdescripcion
                    );

                }
                return Convert.ToInt32(lpred_icod_proveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPrecioCompraDet(EListaPrecioDetalle obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_LISTA_PRECIOS_PROV_DET_MODIFICAR(
                        obj.lpred_icod_proveedor,
                        obj.lpred_nprecio_lista,
                        obj.lpred_nperso_desc,
                        obj.lpred_nprecio_neto,
                        obj.intUsuario,
                        obj.strPc,
                        obj.prdc_vcode_producto,
                        obj.prdc_vdescripcion_larga,
                        obj.prdc_vAutor,
                        obj.edit_vdescripcion,
                        obj.prdc_icod_producto
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarPrecioCompraDet(EListaPrecioDetalle obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_LISTA_PRECIOS_PROV_DET_ELIMINAR(
                        obj.lpred_icod_proveedor,
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
        #endregion
        #region Lista de Pedido Compra
        public List<EPedidoProvCab> listarPedidoCompra()
        {
            List<EPedidoProvCab> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EPedidoProvCab>();
                    var query = dc.SGEC_PEDIDO_PROV_CAB_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EPedidoProvCab()
                        {
                            lpedi_icod_proveedor = Convert.ToInt32(item.lpedi_icod_proveedor),
                            proc_icod_proveedor = item.proc_icod_proveedor,
                            lpedi_Numerolista = item.lpedi_Numerolista,
                            lpedi_sfecha_proveedor = Convert.ToDateTime(item.lpedi_sfecha_proveedor),
                            lpedi_Observaciones = item.lpedi_Observaciones,
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            proc_vcod_proveedor = item.proc_vcod_proveedor,
                            lprec_Numerolista = item.lprec_Numerolista,
                            lpedi_isituacion_prov = Convert.ToInt32(item.lpedi_isituacion_prov),
                            StrSituacion = item.StrSituacion,
                            proc_vruc = item.proc_vruc,
                            proc_vdireccion = item.proc_vdireccion,
                            proc_vtelefono = item.proc_vtelefono,
                            proc_vcorreo = item.proc_vcorreo,
                            proc_vdni = item.proc_vdni,
                            CantidadPedidoTotal = Convert.ToInt32(item.CantidadPedidoTotal)
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

        public int insertarPedidoCompra(EPedidoProvCab obj)
        {

            try
            {
                int? lpedi_icod_proveedor = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PEDIDO_PROV_CAB_INSERTAR(
                        ref lpedi_icod_proveedor,
                        obj.proc_icod_proveedor,
                        obj.lpedi_Numerolista,
                        obj.lprec_icod_proveedor,
                        obj.lpedi_sfecha_proveedor,
                        obj.lpedi_Observaciones,
                        obj.intUsuario,
                        obj.strPc,
                        obj.lpedi_sflag_estado,
                        obj.lpedi_isituacion_prov
                    );

                }
                return Convert.ToInt32(lpedi_icod_proveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPedidoCompra(EPedidoProvCab obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PEDIDO_PROV_CAB_MODIFICAR(
                        obj.lpedi_icod_proveedor,
                        obj.lpedi_sfecha_proveedor,
                        obj.lpedi_Observaciones,
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
        public void CambiarSituacionPedidoCompra(EPedidoProvCab obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PEDIDO_PROV_CAB_CAMBIAR_SITUACION(
                        obj.lpedi_icod_proveedor,
                        obj.lpedi_isituacion_prov,
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
        public void eliminarPedidoCompra(EPedidoProvCab obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PEDIDO_PROV_CAB_ELIMINAR(
                        obj.lpedi_icod_proveedor,
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
        #endregion
        #region Lista de Pedido Compra Det
        public List<EPedidoProvDet> listarPedidoCompraDet(int lpedi_icod_proveedor, int ANIO)
        {
            List<EPedidoProvDet> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EPedidoProvDet>();
                    var query = dc.SGEC_PEDIDO_PROV_DET_LISTAR(lpedi_icod_proveedor, ANIO);
                    foreach (var item in query)
                    {
                        lista.Add(new EPedidoProvDet()
                        {
                            lpedid_icod_proveedor = item.lpedid_icod_proveedor,
                            lpedi_icod_proveedor = item.lpedi_icod_proveedor,
                            prdc_icod_producto = item.prdc_icod_producto,
                            lpedid_icod_moneda = Convert.ToInt32(item.lpedid_icod_moneda),
                            lpedid_nprecio_lista = item.lpedid_nprecio_lista,
                            lpedid_nperso_desc = Convert.ToDecimal(item.lpedid_nperso_desc),
                            lpedid_nprecio_neto = Convert.ToDecimal(item.lpedid_nprecio_neto),
                            lpedid_nstock_producto = Convert.ToDecimal(item.lpedid_nstock_producto),
                            lpedid_ncompras_sem1 = Convert.ToDecimal(item.lpedid_ncompras_sem1),
                            lpedid_ncompras_sem2 = Convert.ToDecimal(item.lpedid_ncompras_sem2),
                            lpedid_ncompras_sem3 = Convert.ToDecimal(item.lpedid_ncompras_sem3),
                            lpedid_ncompras_sem4 = Convert.ToDecimal(item.lpedid_ncompras_sem4),
                            lpedid_nCant_pedido = Convert.ToInt32(item.lpedid_nCant_pedido),
                            lpedid_nCosto_pedido = item.lpedid_nCosto_pedido,
                            prdc_vdescripcion_larga = item.prdc_vdescripcion_larga,
                            prdc_vcode_producto = item.prdc_vcode_producto,
                            strEditorial = item.edit_vdescripcion,
                            prdc_vAutor = item.prdc_vAutor,
                            lpedid_vDesc_moneda = item.strMoneda,
                            intTipoOperacion = item.intOperacion,

                            strCategoria = item.strCategoria,
                            strSubCategoriaUno = item.strSubCategoriaUno,
                            strSubCategoriaDos = item.strSubCategoriaDos,
                            StrUnidadMedida = item.StrUnidadMedida,
                            lpedid_item = Convert.ToInt32(item.lpedid_item)
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

        public int insertarPedidoCompraDet(EPedidoProvDet obj)
        {

            try
            {
                int? lpedid_icod_proveedor = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PEDIDO_PROV_DET_INSERTAR(
                        ref lpedid_icod_proveedor,
                        obj.lpedi_icod_proveedor,
                        obj.prdc_icod_producto,
                        obj.lpedid_icod_moneda,
                        obj.lpedid_nprecio_lista,
                        obj.lpedid_nperso_desc,
                        obj.lpedid_nprecio_neto,
                        obj.lpedid_nstock_producto,
                        obj.lpedid_ncompras_sem1,
                        obj.lpedid_ncompras_sem2,
                        obj.lpedid_ncompras_sem3,
                        obj.lpedid_ncompras_sem4,
                        obj.lpedid_nCant_pedido,
                        obj.lpedid_nCosto_pedido,
                        obj.intUsuario,
                        obj.strPc,
                        obj.lpedid_sflag_estado,
                        obj.lpedid_item,
                        obj.prdc_vcode_producto
                    );

                }
                return Convert.ToInt32(lpedid_icod_proveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPedidoCompraDet(EPedidoProvDet obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PEDIDO_PROV_DET_MODIFICAR(
                        obj.lpedid_icod_proveedor,
                        obj.lpedid_nprecio_lista,
                        obj.lpedid_nperso_desc,
                        obj.lpedid_nprecio_neto,
                        obj.lpedid_nstock_producto,
                        obj.lpedid_ncompras_sem1,
                        obj.lpedid_ncompras_sem2,
                        obj.lpedid_ncompras_sem3,
                        obj.lpedid_ncompras_sem4,
                        obj.lpedid_nCant_pedido,
                        obj.lpedid_nCosto_pedido,
                        obj.intUsuario,
                        obj.strPc,
                        obj.lpedid_item,
                        obj.prdc_icod_producto,
                        obj.prdc_vcode_producto
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarPedidoCompraDet(EPedidoProvDet obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PEDIDO_PROV_DET_ELIMINAR(
                        obj.lpedid_icod_proveedor,
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
        #endregion
        #region Orden Compra
        public List<EOrdenCompra> ListarOrdenCompra()
        {
            List<EOrdenCompra> lista = new List<EOrdenCompra>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEO_ORDEN_COMPRA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenCompra()
                        {
                            ococ_icod_orden_compra = item.ococ_icod_orden_compra,
                            ococ_ianio = Convert.ToInt32(item.ococ_ianio),
                            ococ_numero_orden_compra = item.ococ_numero_orden_compra,
                            ococ_sfecha_orden_compra = item.ococ_sfecha_orden_compra,
                            ococ_sfecha_entrega = Convert.ToDateTime(item.ococ_sfecha_entrega),
                            proc_icod_proveedor = item.proc_icod_proveedor,
                            prvc_vcod_proveedor = item.prvc_vcod_proveedor,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            ococ_npor_imp_igv = item.ococ_npor_imp_igv,
                            ococ_nmonto_neto = item.ococ_nmonto_neto,
                            ococ_nmonto_imp = item.ococ_nmonto_imp,
                            ococ_nmonto_total = item.ococ_nmonto_total,
                            tablc_iid_situacion_oc = item.tablc_iid_situacion_oc,
                            tablc_iforma_pago = item.tablc_iforma_pago,
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            strMoneda = item.tablc_iid_tipo_moneda == 3 ? "S/." : "$/.",
                            str_situacion = item.str_situacion,
                            facc_bIncluido_igv = Convert.ToBoolean(item.facc_bIncluido_igv),
                            orpc_nmonto_sub_Total = item.orpc_nmonto_sub_Total,
                            orpc_npor_descuento = item.orpc_npor_descuento,
                            orpc_nmonto_descuento = item.orpc_nmonto_descuento,
                            proc_vruc = item.proc_vruc,
                            proc_vdireccion = item.proc_vdireccion,
                            proc_vcorreo = item.proc_vcorreo,
                            ococ_vciudad = item.ococ_vciudad,
                            ococ_vreferencia = item.ococ_vreferencia,
                            ococ_vlugar_entrega = item.ococ_vlugar_entrega,
                            ococ_vcomprobante = item.ococ_vcomprobante,
                            ococ_vcontacto = item.ococ_vcontacto,
                            ococ_vforma_pago = item.ococ_vforma_pago,
                            ococ_vcotizacion = item.ococ_vcotizacion,
                            ococ_flag_serie = Convert.ToBoolean(item.ococ_flag_serie)
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
        public List<EOrdenCompra> ListarOrdenCompraRPTConsultar(int Generar, int Entregado_Parcial, int Entregado_Total, DateTime? dtFechaDesde, DateTime? dtFechaHasta)
        {
            List<EOrdenCompra> lista = new List<EOrdenCompra>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEO_ORDEN_COMPRA_LISTAR_RPT_CONUSLTAR(Generar, Entregado_Parcial, Entregado_Total, dtFechaDesde, dtFechaHasta);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenCompra()
                        {
                            ococ_icod_orden_compra = item.ococ_icod_orden_compra,
                            ococ_ianio = Convert.ToInt32(item.ococ_ianio),
                            ococ_numero_orden_compra = item.ococ_numero_orden_compra,
                            ococ_sfecha_orden_compra = item.ococ_sfecha_orden_compra,
                            proc_icod_proveedor = item.proc_icod_proveedor,
                            prvc_vcod_proveedor = item.prvc_vcod_proveedor,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            ococ_npor_imp_igv = item.ococ_npor_imp_igv,
                            ococ_nmonto_neto = item.ococ_nmonto_neto,
                            ococ_nmonto_imp = item.ococ_nmonto_imp,
                            ococ_nmonto_total = item.ococ_nmonto_total,
                            tablc_iid_situacion_oc = item.tablc_iid_situacion_oc,
                            tablc_iforma_pago = item.tablc_iforma_pago,
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            strMoneda = item.tablc_iid_tipo_moneda == 3 ? "S/." : "$/.",
                            str_situacion = item.str_situacion,
                            facc_bIncluido_igv = Convert.ToBoolean(item.facc_bIncluido_igv),
                            orpc_nmonto_sub_Total = item.orpc_nmonto_sub_Total,
                            orpc_npor_descuento = item.orpc_npor_descuento,
                            orpc_nmonto_descuento = item.orpc_nmonto_descuento,
                            proc_vruc = item.proc_vruc,
                            proc_vdireccion = item.proc_vdireccion,
                            proc_vcorreo = item.proc_vcorreo,
                            //ococ_vtelefono = item.ococ_vtelefono,
                            ococ_vreferencia = item.ococ_vreferencia,
                            //ococ_iid_motivo = Convert.ToInt32(item.ococ_iid_motivo),
                            //str_motivo = item.str_motivo,
                            //ococ_iid_proyecto = Convert.ToInt32(item.pryc_icod_proyecto),
                            //cecoc_vcodigo_centro_costo = item.cecoc_vcodigo_centro_costo,
                            ococ_vlugar_entrega = item.ococ_vlugar_entrega,
                            //ococ_vgarantia = item.ococ_vgarantia,
                            //ococ_vnota_ocl = item.ococ_vnota_ocl,
                            ococ_vcontacto = item.ococ_vcontacto,
                            ococ_vforma_pago = item.ococ_vforma_pago,
                            //ococ_vrecepcion = item.ococ_vrecepcion,
                            ococ_vcotizacion = item.ococ_vcotizacion
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
        public int InsertarOrdenCompra(EOrdenCompra obj)
        {
            try
            {
                int? intIcod = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_INSERTAR(
                         ref intIcod,
                        obj.ococ_ianio,
                        obj.ococ_numero_orden_compra,
                        obj.ococ_sfecha_orden_compra,
                        obj.ococ_sfecha_entrega,
                        obj.proc_icod_proveedor,
                        obj.prvc_vcod_proveedor,
                        obj.tablc_iid_tipo_moneda,
                        obj.ococ_npor_imp_igv,
                        obj.ococ_nmonto_neto,
                        obj.ococ_nmonto_imp,
                        obj.ococ_nmonto_total,
                        obj.tablc_iid_situacion_oc,
                        obj.tablc_iforma_pago,
                        obj.facc_bIncluido_igv,
                        obj.orpc_nmonto_sub_Total,
                        obj.orpc_npor_descuento,
                        obj.orpc_nmonto_descuento,
                        obj.intUsuario,
                        obj.strPc,
                        obj.ococ_flag_estado,
                        obj.ococ_vciudad,
                        obj.ococ_vreferencia,
                        obj.ococ_vlugar_entrega,
                        obj.ococ_vcomprobante,
                        obj.ococ_vcontacto,
                        obj.ococ_vforma_pago,
                        obj.ococ_vcotizacion,
                        obj.ococ_flag_serie
                    );

                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarOrdenCompra(EOrdenCompra obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_MODIFICAR(
                        obj.ococ_icod_orden_compra,
                        obj.ococ_sfecha_orden_compra,
                        obj.ococ_sfecha_entrega,
                        obj.ococ_npor_imp_igv,
                        obj.ococ_nmonto_neto,
                        obj.ococ_nmonto_imp,
                        obj.ococ_nmonto_total,
                        obj.orpc_nmonto_sub_Total,
                        obj.orpc_npor_descuento,
                        obj.orpc_nmonto_descuento,
                        obj.intUsuario,
                        obj.strPc,
                        obj.facc_bIncluido_igv,
                        obj.ococ_vciudad,
                        obj.ococ_vreferencia,
                        obj.ococ_vlugar_entrega,
                        obj.ococ_vcomprobante,
                        obj.ococ_vcontacto,
                        obj.ococ_vforma_pago,
                        obj.ococ_vcotizacion
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarOrdenCompra(int ococ_icod_orden_compra)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_ELIMINAR(ococ_icod_orden_compra);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AnularOrdenCompra(int ococ_icod_orden_compra, int tablc_iid_situacion_oc)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_ANULAR(ococ_icod_orden_compra, tablc_iid_situacion_oc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Orden Compra Detalle
        public List<EOrdenCompra> ListarOrdenCompraDetalle(int ococ_icod_orden_compra)
        {
            List<EOrdenCompra> lista = new List<EOrdenCompra>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEO_ORDEN_COMPRA_DETALLE_LISTAR(ococ_icod_orden_compra);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenCompra()
                        {
                            ocod_icod_detalle_oc = item.ocod_icod_detalle_oc,
                            ococ_icod_orden_compra = item.ococ_icod_orden_compra,
                            ocod_iitem = item.ocod_iitem,
                            prdc_icod_producto = item.prdc_icod_producto,
                            ocod_ncantidad = item.ocod_ncantidad,
                            ocod_ncantidad_recibida = Convert.ToDecimal(item.ocod_ncantidad_recibida),
                            ocod_ncantidad_saldo = Convert.ToDecimal(item.ocod_ncantidad_saldo),
                            ocod_ncunitario = item.ocod_ncunitario,
                            ocod_nmonto_total = item.ocod_nmonto_total,
                            kardc_iid_correlativo = item.kardc_iid_correlativo,
                            strCodigoProducto = item.prdc_vcode_producto,
                            strDescProducto = item.prdc_vdescripcion_larga,
                            strMedida = item.strUniMed,
                            strAbrevUniMed = item.strAbrevUniMed,
                            ocod_vdescripcion = item.ocod_vdescripcion,
                            ocod_ndescuento_item = Convert.ToDecimal(item.ocod_ndescuento_item),
                            ocod_vdireccion_documento = item.ocod_vdireccion_documento,
                            ocod_vcaracteristicas = item.ocod_vcaracteristicas,
                            prdc_vcodigo_fabricante = item.prdc_vcodigo_fabricante,
                            ocod_flag_estado = item.ocod_flag_estado
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
        public List<EOrdenCompra> ListarOrdenCompraDetalleFC(int ococ_icod_orden_compra)
        {
            List<EOrdenCompra> lista = new List<EOrdenCompra>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEO_ORDEN_COMPRA_DETALLE_LISTAR_FC(ococ_icod_orden_compra);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenCompra()
                        {
                            ocod_icod_detalle_oc = item.ocod_icod_detalle_oc,
                            ococ_icod_orden_compra = item.ococ_icod_orden_compra,
                            ocod_iitem = item.ocod_iitem,
                            prdc_icod_producto = item.prdc_icod_producto,
                            ocod_ncantidad = item.ocod_ncantidad,
                            ocod_ncantidad_recibida = Convert.ToDecimal(item.ocod_ncantidad_facturada),
                            ocod_ncantidad_saldo = Convert.ToDecimal(item.ocod_ncantidad_saldo),
                            ocod_ncunitario = item.ocod_ncunitario,
                            ocod_nmonto_total = item.ocod_nmonto_total,
                            kardc_iid_correlativo = item.kardc_iid_correlativo,
                            strCodigoProducto = item.prdc_vcode_producto,
                            strDescProducto = item.prdc_vdescripcion_larga,
                            strMedida = item.strUniMed,
                            strAbrevUniMed = item.strAbrevUniMed,
                            ocod_vdescripcion = item.ocod_vdescripcion,
                            ocod_ndescuento_item = Convert.ToDecimal(item.ocod_ndescuento_item),
                            ocod_vdireccion_documento = item.ocod_vdireccion_documento,
                            ocod_vcaracteristicas = item.ocod_vcaracteristicas,
                            ocod_dfecha_entrega = Convert.ToDateTime(item.ocod_dfecha_entrega),
                            prdc_vcodigo_fabricante = item.prdc_vcodigo_fabricante,
                            ocod_flag_estado = item.ocod_flag_estado
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
        public int InsertarOrdenCompraDetalle(EOrdenCompra obj)
        {
            try
            {
                int? intIcod = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_DETALLE_INSERTAR(
                         ref intIcod,
                        obj.ococ_icod_orden_compra,
                        obj.ocod_iitem,
                        obj.prdc_icod_producto,
                        obj.mnoc_icod_mano_obra,
                        obj.ocod_ncantidad,
                        obj.ocod_ncantidad_recibida,
                        obj.ocod_ncantidad_saldo,
                        obj.ocod_ncunitario,
                        obj.ocod_nmonto_total,
                        obj.kardc_iid_correlativo,
                        obj.intUsuario,
                        obj.strPc,
                        obj.ocod_flag_estado,
                        obj.ocod_vdescripcion,
                        obj.ocod_ndescuento_item,
                        obj.ocod_vdireccion_documento,
                        obj.ocod_vcaracteristicas
                    );

                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarOrdenCompraDetalle(EOrdenCompra obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_DETALLE_MODIFICAR(
                        obj.ocod_icod_detalle_oc,
                        obj.ocod_iitem,
                        obj.ocod_ncantidad,
                        obj.ocod_ncantidad_recibida,
                        obj.ocod_ncantidad_saldo,
                        obj.ocod_ncunitario,
                        obj.ocod_nmonto_total,
                        obj.intUsuario,
                        obj.strPc,
                        obj.ocod_vdescripcion,
                        obj.ocod_ndescuento_item,
                        obj.ocod_vdireccion_documento,
                        obj.ocod_vcaracteristicas
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarOrdenCompraDetalle(int ocod_icod_detalle_oc)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_DETALLE_ELIMINAR(ocod_icod_detalle_oc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Orden Compra Serie Detalle
        public List<EOrdenCompra> ListarOrdenCompraSerieDetalle(int ococ_icod_orden_compra)
        {
            List<EOrdenCompra> lista = new List<EOrdenCompra>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEO_ORDEN_COMPRA_SERIE_DETALLE_LISTAR(ococ_icod_orden_compra);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenCompra()
                        {
                            ocod_icod_detalle_oc = item.ocod_icod_detalle_oc,
                            ococ_icod_orden_compra = item.ococ_icod_orden_compra,
                            ocod_iitem = item.ocod_iitem,
                            prdc_icod_producto = item.prdc_icod_producto,
                            ocod_ncantidad = item.ocod_ncantidad,
                            ocod_ncantidad_recibida = Convert.ToDecimal(item.ocod_ncantidad_recibida),
                            ocod_ncantidad_saldo = Convert.ToDecimal(item.ocod_ncantidad_saldo),
                            ocod_ncunitario = item.ocod_ncunitario,
                            ocod_nmonto_total = item.ocod_nmonto_total,
                            kardc_iid_correlativo = item.kardc_iid_correlativo,
                            strCodigoProducto = item.prdc_vcode_producto,
                            strDescProducto = item.prdc_vdescripcion_larga,
                            strMedida = item.strUniMed,
                            strAbrevUniMed = item.strAbrevUniMed,
                            ocod_vdescripcion = item.ocod_vdescripcion,
                            ocod_ndescuento_item = Convert.ToDecimal(item.ocod_ndescuento_item),
                            ocod_vdireccion_documento = item.ocod_vdireccion_documento,
                            ocod_vcaracteristicas = item.ocod_vcaracteristicas,
                            prdc_vcodigo_fabricante = item.prdc_vcodigo_fabricante,
                            ocod_flag_estado = item.ocod_flag_estado,
                            ocod_rango_talla = item.ocod_rango_talla,
                            ocod_talla1 = item.ocod_talla1,
                            ocod_talla2 = item.ocod_talla2,
                            ocod_talla3 = item.ocod_talla3,
                            ocod_talla4 = item.ocod_talla4,
                            ocod_talla5 = item.ocod_talla5,
                            ocod_talla6 = item.ocod_talla6,
                            ocod_talla7 = item.ocod_talla7,
                            ocod_talla8 = item.ocod_talla8,
                            ocod_talla9 = item.ocod_talla9,
                            ocod_talla10 = item.ocod_talla10,
                            ocod_cant_talla1 = item.ocod_cant_talla1,
                            ocod_cant_talla2 = item.ocod_cant_talla2,
                            ocod_cant_talla3 = item.ocod_cant_talla3,
                            ocod_cant_talla4 = item.ocod_cant_talla4,
                            ocod_cant_talla5 = item.ocod_cant_talla5,
                            ocod_cant_talla6 = item.ocod_cant_talla6,
                            ocod_cant_talla7 = item.ocod_cant_talla7,
                            ocod_cant_talla8 = item.ocod_cant_talla8,
                            ocod_cant_talla9 = item.ocod_cant_talla9,
                            ocod_cant_talla10 = item.ocod_cant_talla10
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
        public int InsertarOrdenCompraSerieDetalle(EOrdenCompra obj)
        {
            try
            {
                int? intIcod = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_SERIE_DETALLE_INSERTAR(
                         ref intIcod,
                        obj.ococ_icod_orden_compra,
                        obj.ocod_iitem,
                        obj.prdc_icod_producto,
                        obj.mnoc_icod_mano_obra,
                        obj.ocod_ncantidad,
                        obj.ocod_ncantidad_recibida,
                        obj.ocod_ncantidad_saldo,
                        obj.ocod_ncunitario,
                        obj.ocod_nmonto_total,
                        obj.kardc_iid_correlativo,
                        obj.intUsuario,
                        obj.strPc,
                        obj.ocod_flag_estado,
                        obj.ocod_vdescripcion,
                        obj.ocod_ndescuento_item,
                        obj.ocod_vdireccion_documento,
                        obj.ocod_vcaracteristicas,
                        obj.ocod_rango_talla,
                        obj.ocod_talla1,
                        obj.ocod_talla2,
                        obj.ocod_talla3,
                        obj.ocod_talla4,
                        obj.ocod_talla5,
                        obj.ocod_talla6,
                        obj.ocod_talla7,
                        obj.ocod_talla8,
                        obj.ocod_talla9,
                        obj.ocod_talla10,
                        obj.ocod_cant_talla1,
                        obj.ocod_cant_talla2,
                        obj.ocod_cant_talla3,
                        obj.ocod_cant_talla4,
                        obj.ocod_cant_talla5,
                        obj.ocod_cant_talla6,
                        obj.ocod_cant_talla7,
                        obj.ocod_cant_talla8,
                        obj.ocod_cant_talla9,
                        obj.ocod_cant_talla10
                    );

                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarOrdenCompraSerieDetalle(EOrdenCompra obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_SERIE_DETALLE_MODIFICAR(
                        obj.ocod_icod_detalle_oc,
                        obj.ocod_iitem,
                        obj.ocod_ncantidad,
                        obj.ocod_ncantidad_recibida,
                        obj.ocod_ncantidad_saldo,
                        obj.ocod_ncunitario,
                        obj.ocod_nmonto_total,
                        obj.intUsuario,
                        obj.strPc,
                        obj.ocod_vdescripcion,
                        obj.ocod_ndescuento_item,
                        obj.ocod_vdireccion_documento,
                        obj.ocod_vcaracteristicas,
                        obj.ocod_rango_talla,
                        obj.ocod_talla1,
                        obj.ocod_talla2,
                        obj.ocod_talla3,
                        obj.ocod_talla4,
                        obj.ocod_talla5,
                        obj.ocod_talla6,
                        obj.ocod_talla7,
                        obj.ocod_talla8,
                        obj.ocod_talla9,
                        obj.ocod_talla10,
                        obj.ocod_cant_talla1,
                        obj.ocod_cant_talla2,
                        obj.ocod_cant_talla3,
                        obj.ocod_cant_talla4,
                        obj.ocod_cant_talla5,
                        obj.ocod_cant_talla6,
                        obj.ocod_cant_talla7,
                        obj.ocod_cant_talla8,
                        obj.ocod_cant_talla9,
                        obj.ocod_cant_talla10
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarOrdenCompraSerieDetalle(int ocod_icod_detalle_oc)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_DETALLE_ELIMINAR(ocod_icod_detalle_oc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Orden Compra Mercaderia
        public List<EOrdenCompraMercaderia> ListarOrdenCompraMercaderia()
        {
            List<EOrdenCompraMercaderia> lista = new List<EOrdenCompraMercaderia>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEO_ORDEN_COMPRA_MERCADERIA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenCompraMercaderia()
                        {
                            ococ_icod_orden_compra = item.ococ_icod_orden_compra,
                            ococ_ianio = Convert.ToInt32(item.ococ_ianio),
                            ococ_numero_orden_compra = item.ococ_numero_orden_compra,
                            ococ_sfecha_orden_compra = item.ococ_sfecha_orden_compra,
                            ococ_sfecha_entrega = Convert.ToDateTime(item.ococ_sfecha_entrega),
                            proc_icod_proveedor = item.proc_icod_proveedor,
                            prvc_vcod_proveedor = item.prvc_vcod_proveedor,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            ococ_npor_imp_igv = item.ococ_npor_imp_igv,
                            ococ_nmonto_neto = item.ococ_nmonto_neto,
                            ococ_nmonto_imp = item.ococ_nmonto_imp,
                            ococ_nmonto_total = item.ococ_nmonto_total,
                            tablc_iid_situacion_oc = item.tablc_iid_situacion_oc,
                            tablc_iforma_pago = item.tablc_iforma_pago,
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            strMoneda = item.tablc_iid_tipo_moneda == 3 ? "S/." : "$/.",
                            str_situacion = item.str_situacion,
                            facc_bIncluido_igv = Convert.ToBoolean(item.facc_bIncluido_igv),
                            orpc_nmonto_sub_Total = item.orpc_nmonto_sub_Total,
                            orpc_npor_descuento = item.orpc_npor_descuento,
                            orpc_nmonto_descuento = item.orpc_nmonto_descuento,
                            proc_vruc = item.proc_vruc,
                            proc_vdireccion = item.proc_vdireccion,
                            proc_vcorreo = item.proc_vcorreo,
                            ococ_vciudad = item.ococ_vciudad,
                            ococ_vreferencia = item.ococ_vreferencia,
                            ococ_vlugar_entrega = item.ococ_vlugar_entrega,
                            ococ_vcomprobante = item.ococ_vcomprobante,
                            ococ_vcontacto = item.ococ_vcontacto,
                            ococ_vforma_pago = item.ococ_vforma_pago,
                            ococ_vcotizacion = item.ococ_vcotizacion,
                            ococ_flag_serie = Convert.ToBoolean(item.ococ_flag_serie)
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
        public List<EOrdenCompraMercaderia> ListarOrdenCompraMercaderiaRPTConsultar(int Generar, int Entregado_Parcial, int Entregado_Total, DateTime? dtFechaDesde, DateTime? dtFechaHasta)
        {
            List<EOrdenCompraMercaderia> lista = new List<EOrdenCompraMercaderia>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEO_ORDEN_COMPRA_MERCADERIA_LISTAR_RPT_CONUSLTAR(Generar, Entregado_Parcial, Entregado_Total, dtFechaDesde, dtFechaHasta);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenCompraMercaderia()
                        {
                            ococ_icod_orden_compra = item.ococ_icod_orden_compra,
                            ococ_ianio = Convert.ToInt32(item.ococ_ianio),
                            ococ_numero_orden_compra = item.ococ_numero_orden_compra,
                            ococ_sfecha_orden_compra = item.ococ_sfecha_orden_compra,
                            ococ_sfecha_entrega = Convert.ToDateTime(item.ococ_sfecha_entrega),
                            proc_icod_proveedor = item.proc_icod_proveedor,
                            prvc_vcod_proveedor = item.prvc_vcod_proveedor,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            ococ_npor_imp_igv = item.ococ_npor_imp_igv,
                            ococ_nmonto_neto = item.ococ_nmonto_neto,
                            ococ_nmonto_imp = item.ococ_nmonto_imp,
                            ococ_nmonto_total = item.ococ_nmonto_total,
                            tablc_iid_situacion_oc = item.tablc_iid_situacion_oc,
                            tablc_iforma_pago = item.tablc_iforma_pago,
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            strMoneda = item.tablc_iid_tipo_moneda == 3 ? "S/." : "$/.",
                            str_situacion = item.str_situacion,
                            facc_bIncluido_igv = Convert.ToBoolean(item.facc_bIncluido_igv),
                            orpc_nmonto_sub_Total = item.orpc_nmonto_sub_Total,
                            orpc_npor_descuento = item.orpc_npor_descuento,
                            orpc_nmonto_descuento = item.orpc_nmonto_descuento,
                            proc_vruc = item.proc_vruc,
                            proc_vdireccion = item.proc_vdireccion,
                            proc_vcorreo = item.proc_vcorreo,
                            ococ_vciudad = item.ococ_vciudad,
                            ococ_vreferencia = item.ococ_vreferencia,
                            ococ_vlugar_entrega = item.ococ_vlugar_entrega,
                            ococ_vcomprobante = item.ococ_vcomprobante,
                            ococ_vcontacto = item.ococ_vcontacto,
                            ococ_vforma_pago = item.ococ_vforma_pago,
                            ococ_vcotizacion = item.ococ_vcotizacion,
                            ococ_flag_serie = Convert.ToBoolean(item.ococ_flag_serie)
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
        public int InsertarOrdenCompraMercaderia(EOrdenCompraMercaderia obj)
        {
            try
            {
                int? intIcod = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_MERCADERIA_INSERTAR(
                         ref intIcod,
                         obj.ococ_ianio,
                        obj.ococ_numero_orden_compra,
                        obj.ococ_sfecha_orden_compra,
                        obj.ococ_sfecha_entrega,
                        obj.proc_icod_proveedor,
                        obj.prvc_vcod_proveedor,
                        obj.tablc_iid_tipo_moneda,
                        obj.ococ_npor_imp_igv,
                        obj.ococ_nmonto_neto,
                        obj.ococ_nmonto_imp,
                        obj.ococ_nmonto_total,
                        obj.tablc_iid_situacion_oc,
                        obj.tablc_iforma_pago,
                        obj.facc_bIncluido_igv,
                        obj.orpc_nmonto_sub_Total,
                        obj.orpc_npor_descuento,
                        obj.orpc_nmonto_descuento,
                        obj.intUsuario,
                        obj.strPc,
                        obj.ococ_flag_estado,
                        obj.ococ_vciudad,
                        obj.ococ_vreferencia,
                        obj.ococ_vlugar_entrega,
                        obj.ococ_vcomprobante,
                        obj.ococ_vcontacto,
                        obj.ococ_vforma_pago,
                        obj.ococ_vcotizacion,
                        obj.ococ_flag_serie
                    );

                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarOrdenCompraMercaderia(EOrdenCompraMercaderia obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_MERCADERIA_MODIFICAR(
                        obj.ococ_icod_orden_compra,
                        obj.ococ_sfecha_orden_compra,
                        obj.ococ_sfecha_entrega,
                        obj.ococ_npor_imp_igv,
                        obj.ococ_nmonto_neto,
                        obj.ococ_nmonto_imp,
                        obj.ococ_nmonto_total,
                        obj.orpc_nmonto_sub_Total,
                        obj.orpc_npor_descuento,
                        obj.orpc_nmonto_descuento,
                        obj.intUsuario,
                        obj.strPc,
                        obj.facc_bIncluido_igv,
                        obj.ococ_vciudad,
                        obj.ococ_vreferencia,
                        obj.ococ_vlugar_entrega,
                        obj.ococ_vcomprobante,
                        obj.ococ_vcontacto,
                        obj.ococ_vforma_pago,
                        obj.ococ_vcotizacion
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarOrdenCompraMercaderia(int ococ_icod_orden_compra)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_MERCADERIA_ELIMINAR(ococ_icod_orden_compra);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Orden Compra Mercaderia Detalle
        public List<EOrdenCompraMercaderiaDetalle> ListarOrdenCompraMercaderiaDetalle(int Code)
        {
            List<EOrdenCompraMercaderiaDetalle> lista = new List<EOrdenCompraMercaderiaDetalle>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.CommandTimeout = 30;
                    var query = dc.SGEO_ORDEN_COMPRA_MERCADERIA_DETALLE_LISTAR(Code);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenCompraMercaderiaDetalle()
                        {
                            ocod_icod_detalle_oc = item.ocod_icod_detalle_oc,
                            ococ_icod_orden_compra = item.ococ_icod_orden_compra,
                            ocod_iitem = item.ocod_iitem,
                            prdc_icod_producto = item.prdc_icod_producto,
                            ocod_ncantidad = item.ocod_ncantidad,
                            ocod_ncantidad_recibida = Convert.ToDecimal(item.ocod_ncantidad_recibida),
                            ocod_ncantidad_saldo = Convert.ToDecimal(item.ocod_ncantidad_saldo),
                            ocod_ncunitario = item.ocod_ncunitario,
                            ocod_nmonto_total = item.ocod_nmonto_total,
                            kardc_iid_correlativo = item.kardc_iid_correlativo,
                            strCodigoProducto = item.pr_vcodigo_externo,
                            strDescProducto = item.pr_vdescripcion_producto,
                            strMedida = item.strUniMed,
                            strAbrevUniMed = item.strAbrevUniMed,
                            ocod_vdescripcion = item.ocod_vdescripcion,
                            ocod_ndescuento_item = Convert.ToDecimal(item.ocod_ndescuento_item),
                            ocod_vdireccion_documento = item.ocod_vdireccion_documento,
                            ocod_vcaracteristicas = item.ocod_vcaracteristicas,
                            //prdc_vcodigo_fabricante = item.prdc_vcodigo_fabricante,
                            ocod_flag_estado = item.ocod_flag_estado,
                            ocod_rango_talla = item.ocod_rango_talla,
                            ocod_talla1 = item.ocod_talla1,
                            ocod_talla2 = item.ocod_talla2,
                            ocod_talla3 = item.ocod_talla3,
                            ocod_talla4 = item.ocod_talla4,
                            ocod_talla5 = item.ocod_talla5,
                            ocod_talla6 = item.ocod_talla6,
                            ocod_talla7 = item.ocod_talla7,
                            ocod_talla8 = item.ocod_talla8,
                            ocod_talla9 = item.ocod_talla9,
                            ocod_talla10 = item.ocod_talla10,
                            ocod_cant_talla1 = item.ocod_cant_talla1,
                            ocod_cant_talla2 = item.ocod_cant_talla2,
                            ocod_cant_talla3 = item.ocod_cant_talla3,
                            ocod_cant_talla4 = item.ocod_cant_talla4,
                            ocod_cant_talla5 = item.ocod_cant_talla5,
                            ocod_cant_talla6 = item.ocod_cant_talla6,
                            ocod_cant_talla7 = item.ocod_cant_talla7,
                            ocod_cant_talla8 = item.ocod_cant_talla8,
                            ocod_cant_talla9 = item.ocod_cant_talla9,
                            ocod_cant_talla10 = item.ocod_cant_talla10,
                            ocod_cant_recibida1 = item.ocod_cant_recibida1,
                            ocod_cant_recibida2 = item.ocod_cant_recibida2,
                            ocod_cant_recibida3 = item.ocod_cant_recibida3,
                            ocod_cant_recibida4 = item.ocod_cant_recibida4,
                            ocod_cant_recibida5 = item.ocod_cant_recibida5,
                            ocod_cant_recibida6 = item.ocod_cant_recibida6,
                            ocod_cant_recibida7 = item.ocod_cant_recibida7,
                            ocod_cant_recibida8 = item.ocod_cant_recibida8,
                            ocod_cant_recibida9 = item.ocod_cant_recibida9,
                            ocod_cant_recibida10 = item.ocod_cant_recibida10,
                            ocod_cant_saldo1 = item.ocod_cant_saldo1,
                            ocod_cant_saldo2 = item.ocod_cant_saldo2,
                            ocod_cant_saldo3 = item.ocod_cant_saldo3,
                            ocod_cant_saldo4 = item.ocod_cant_saldo4,
                            ocod_cant_saldo5 = item.ocod_cant_saldo5,
                            ocod_cant_saldo6 = item.ocod_cant_saldo6,
                            ocod_cant_saldo7 = item.ocod_cant_saldo7,
                            ocod_cant_saldo8 = item.ocod_cant_saldo8,
                            ocod_cant_saldo9 = item.ocod_cant_saldo9,
                            ocod_cant_saldo10 = item.ocod_cant_saldo10

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
        public int InsertarOrdenCompraMercaderiaDetalle(EOrdenCompraMercaderiaDetalle obj)
        {
            try
            {
                int? intIcod = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_MERCADERIA_DETALLE_INSERTAR(
                        ref intIcod,
                        obj.ococ_icod_orden_compra,
                        obj.ocod_iitem,
                        obj.prdc_icod_producto,
                        obj.mnoc_icod_mano_obra,
                        obj.ocod_ncantidad,
                        obj.ocod_ncantidad_recibida,
                        obj.ocod_ncantidad_saldo,
                        obj.ocod_ncunitario,
                        obj.ocod_nmonto_total,
                        obj.kardc_iid_correlativo,
                        obj.intUsuario,
                        obj.strPc,
                        obj.ocod_flag_estado,
                        obj.ocod_vdescripcion,
                        obj.ocod_ndescuento_item,
                        obj.ocod_vdireccion_documento,
                        obj.ocod_vcaracteristicas,
                        obj.ocod_rango_talla,
                        obj.ocod_talla1,
                        obj.ocod_talla2,
                        obj.ocod_talla3,
                        obj.ocod_talla4,
                        obj.ocod_talla5,
                        obj.ocod_talla6,
                        obj.ocod_talla7,
                        obj.ocod_talla8,
                        obj.ocod_talla9,
                        obj.ocod_talla10,
                        obj.ocod_cant_talla1,
                        obj.ocod_cant_talla2,
                        obj.ocod_cant_talla3,
                        obj.ocod_cant_talla4,
                        obj.ocod_cant_talla5,
                        obj.ocod_cant_talla6,
                        obj.ocod_cant_talla7,
                        obj.ocod_cant_talla8,
                        obj.ocod_cant_talla9,
                        obj.ocod_cant_talla10,
                        obj.ocod_cant_saldo1,
                        obj.ocod_cant_saldo2,
                        obj.ocod_cant_saldo3,
                        obj.ocod_cant_saldo4,
                        obj.ocod_cant_saldo5,
                        obj.ocod_cant_saldo6,
                        obj.ocod_cant_saldo7,
                        obj.ocod_cant_saldo8,
                        obj.ocod_cant_saldo9,
                        obj.ocod_cant_saldo10
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarOrdenCompraMercaderiaDetalle(EOrdenCompraMercaderiaDetalle obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_MERCADERIA_DETALLE_MODIFICAR(
                        obj.ocod_icod_detalle_oc,
                        obj.ocod_iitem,
                        obj.ocod_ncantidad,
                        obj.ocod_ncantidad_recibida,
                        obj.ocod_ncantidad_saldo,
                        obj.ocod_ncunitario,
                        obj.ocod_nmonto_total,
                        obj.intUsuario,
                        obj.strPc,
                        obj.ocod_vdescripcion,
                        obj.ocod_ndescuento_item,
                        obj.ocod_vdireccion_documento,
                        obj.ocod_vcaracteristicas,
                        obj.ocod_rango_talla,
                        obj.ocod_talla1,
                        obj.ocod_talla2,
                        obj.ocod_talla3,
                        obj.ocod_talla4,
                        obj.ocod_talla5,
                        obj.ocod_talla6,
                        obj.ocod_talla7,
                        obj.ocod_talla8,
                        obj.ocod_talla9,
                        obj.ocod_talla10,
                        obj.ocod_cant_talla1,
                        obj.ocod_cant_talla2,
                        obj.ocod_cant_talla3,
                        obj.ocod_cant_talla4,
                        obj.ocod_cant_talla5,
                        obj.ocod_cant_talla6,
                        obj.ocod_cant_talla7,
                        obj.ocod_cant_talla8,
                        obj.ocod_cant_talla9,
                        obj.ocod_cant_talla10,
                        obj.ocod_cant_saldo1,
                        obj.ocod_cant_saldo2,
                        obj.ocod_cant_saldo3,
                        obj.ocod_cant_saldo4,
                        obj.ocod_cant_saldo5,
                        obj.ocod_cant_saldo6,
                        obj.ocod_cant_saldo7,
                        obj.ocod_cant_saldo8,
                        obj.ocod_cant_saldo9,
                        obj.ocod_cant_saldo10
                );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarOrdenCompraMercaderiaDetalle(EOrdenCompraMercaderiaDetalle obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_DETALLE_ELIMINAR(obj.ocod_icod_detalle_oc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Orden Compra Serivicio
        public List<EOrdenCompraServicio> ListarOrdenCompraServicio()
        {
            List<EOrdenCompraServicio> lista = new List<EOrdenCompraServicio>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEO_ORDEN_COMPRA_SERVICIO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenCompraServicio()
                        {
                            ocsc_icod_ocs = item.ocsc_icod_ocs,
                            ocsc_ianio = Convert.ToInt32(item.ocsc_ianio),
                            ocsc_vnumero_ocs = item.ocsc_vnumero_ocs,
                            ocsc_sfecha_ocs = Convert.ToDateTime(item.ocsc_sfecha_ocs),
                            proc_icod_proveedor = item.proc_icod_proveedor,
                            prvc_vcod_proveedor = item.prvc_vcod_proveedor,
                            tablc_iid_situacion_ocs = Convert.ToInt32(item.tablc_iid_situacion_ocs),
                            ocsc_vtelefono = item.ocsc_vtelefono,
                            ocsc_vcontacto = item.ocsc_vcontacto,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            ocsc_npor_imp_igv = Convert.ToDecimal(item.ocsc_npor_imp_igv),
                            ocsc_vreferncia = item.ocsc_vreferncia,
                            ocsc_iid_tipo = Convert.ToInt32(item.ocsc_iid_tipo),
                            str_tipo = item.str_tipo,
                            ocsc_iid_proyecto = Convert.ToInt32(item.pryc_icod_proyecto),
                            cecoc_vcodigo_centro_costo = item.cecoc_vcodigo_centro_costo,
                            strMoneda = item.tablc_iid_tipo_moneda == 3 ? "S/." : "$/.",
                            str_situacion = item.str_situacion,
                            ocsc_vforma_pago = item.ocsc_vforma_pago,
                            ocsc_vlugar_entrega = item.ocsc_vlugar_entrega,
                            ocsc_vgarantia = item.ocsc_vgarantia,
                            ocsc_vnota_ocs = item.ocsc_vnota_ocs,
                            ocsc_nmonto_neto = Convert.ToDecimal(item.ocsc_nmonto_neto),
                            ocsc_nmonto_descuento = Convert.ToDecimal(item.ocsc_nmonto_descuento),
                            ocsc_nmonto_impuesto = Convert.ToDecimal(item.ocsc_nmonto_impuesto),
                            ocsc_nmonto_total = Convert.ToDecimal(item.ocsc_nmonto_total),
                            ocsc_nmonto_sub_total = Convert.ToDecimal(item.ocsc_nmonto_sub_total),
                            ocsc_npor_descuento = Convert.ToDecimal(item.ocsc_npor_descuento),
                            ocsc_bincluye_igv = Convert.ToBoolean(item.ocsc_bincluye_igv),
                            proc_vruc = item.proc_vruc,
                            proc_vdireccion = item.proc_vdireccion,
                            proc_vcorreo = item.proc_vcorreo,
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            cecoc_vdescripcion = item.cecoc_vdescripcion,
                            ocsc_vcotizacion = item.ocsc_vcotizacion,
                            ocsc_vciudad = item.ocsc_vciudad
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
        public int InsertarOrdenCompraServicio(EOrdenCompraServicio obj)
        {
            try
            {
                int? intIcod = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_SERVICIO_INSERTAR(
                         ref intIcod,
                         obj.ocsc_ianio,
                        obj.ocsc_vnumero_ocs,
                        obj.ocsc_sfecha_ocs,
                        obj.proc_icod_proveedor,
                        obj.prvc_vcod_proveedor,
                        obj.tablc_iid_situacion_ocs,
                        obj.ocsc_vtelefono,
                        obj.ocsc_vcontacto,
                        obj.tablc_iid_tipo_moneda,
                        obj.ocsc_npor_imp_igv,
                        obj.ocsc_vreferncia,
                        obj.ocsc_iid_tipo,
                        obj.ocsc_iid_proyecto,
                        obj.ocsc_vforma_pago,
                        obj.ocsc_vlugar_entrega,
                        obj.ocsc_vgarantia,
                        obj.ocsc_vnota_ocs,
                        obj.ocsc_nmonto_neto,
                        obj.ocsc_nmonto_descuento,
                        obj.ocsc_nmonto_impuesto,
                        obj.ocsc_nmonto_total,
                        obj.ocsc_nmonto_sub_total,
                        obj.ocsc_npor_descuento,
                        obj.ocsc_bincluye_igv,
                        obj.intUsuario,
                        obj.strPc,
                        obj.ocsc_flag_estado,
                        obj.ocsc_vcotizacion,
                        obj.ocsc_vciudad
                    );

                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarOrdenCompraServicio(EOrdenCompraServicio obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_SERVICIO_MODIFICAR(
                       obj.ocsc_icod_ocs,
                         obj.ocsc_sfecha_ocs,
                         obj.ocsc_npor_imp_igv,
                         obj.ocsc_nmonto_neto,
                         obj.ocsc_nmonto_impuesto,
                         obj.ocsc_nmonto_total,
                         obj.ocsc_nmonto_sub_total,
                         obj.ocsc_npor_descuento,
                         obj.ocsc_nmonto_descuento,
                         obj.ocsc_bincluye_igv,
                         obj.ocsc_vtelefono,
                         obj.ocsc_vcontacto,
                         obj.ocsc_iid_tipo,
                         obj.ocsc_iid_proyecto,
                         obj.ocsc_vforma_pago,
                         obj.ocsc_vlugar_entrega,
                         obj.ocsc_vgarantia,
                         obj.ocsc_vnota_ocs,
                        obj.intUsuario,
                        obj.strPc,
                        obj.ocsc_vreferncia,
                        obj.ocsc_vcotizacion,
                        obj.ocsc_vciudad

                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarOrdenCompraServicio(int ococ_icod_orden_compra)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_SERVICIO_ELIMINAR(ococ_icod_orden_compra);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AnularOrdenCompraServicio(int ococ_icod_orden_compra, int tablc_iid_situacion_oc)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_SERVICIO_ANULAR(ococ_icod_orden_compra, tablc_iid_situacion_oc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Orden Compra Servicio Detalle
        public List<EOrdenCompraServicio> ListarOrdenCompraServicioDetalle(int ococ_icod_orden_compra)
        {
            List<EOrdenCompraServicio> lista = new List<EOrdenCompraServicio>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEO_ORDEN_COMPRA_SERVICIO_DETALLE_LISTAR(ococ_icod_orden_compra);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenCompraServicio()
                        {
                            ocsd_icod_detalle_ocs = item.ocsd_icod_detalle_ocs,
                            ocsc_icod_ocs = Convert.ToInt32(item.ocsc_icod_ocs),
                            ocsd_iitem = Convert.ToInt32(item.ocsd_iitem),
                            ocsd_vcodigo_servicio_prov = item.ocsd_vcodigo_servicio_prov,
                            ocsd_vdescripcion = item.ocsd_vdescripcion,
                            ocsd_sfecha_entrega = Convert.ToDateTime(item.ocsd_sfecha_entrega),
                            ocsd_ncantidad = Convert.ToDecimal(item.ocsd_ncantidad),
                            unidc_icod_unidad_medida = Convert.ToInt32(item.unidc_icod_unidad_medida),
                            strMedida = item.strUniMed,
                            strAbrevUniMed = item.strAbrevUniMed,
                            ocsd_ncunitaria = Convert.ToDecimal(item.ocsd_ncunitaria),
                            ocsd_nvalor_total = Convert.ToDecimal(item.ocsd_nvalor_total),
                            ocsd_ndescuento = Convert.ToDecimal(item.ocsd_ndescuento),
                            ocsd_vdireccion_documento = item.ocsd_vdireccion_documento,
                            ocsd_vcaracteristicas = item.ocsd_vcaracteristicas,

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
        public int InsertarOrdenCompraServicioDetalle(EOrdenCompraServicio obj)
        {
            try
            {
                int? intIcod = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_SERVICIO_DETALLE_INSERTAR(
                         ref intIcod,
                        obj.ocsc_icod_ocs,
                        obj.ocsd_iitem,
                        obj.ocsd_vcodigo_servicio_prov,
                        obj.ocsd_vdescripcion,
                        obj.ocsd_sfecha_entrega,
                        obj.ocsd_ncantidad,
                        obj.unidc_icod_unidad_medida,
                        obj.ocsd_ncunitaria,
                        obj.ocsd_nvalor_total,
                        obj.ocsd_ndescuento,
                        obj.ocsd_vdireccion_documento,
                        obj.ocsd_vcaracteristicas,
                        obj.intUsuario,
                        obj.strPc,
                        obj.ocsd_flag_esatdo
                    );

                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarOrdenCompraServicioDetalle(EOrdenCompraServicio obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_SERVICIO_DETALLE_MODIFICAR(
                        obj.ocsd_icod_detalle_ocs,
                        obj.ocsd_iitem,
                        obj.ocsd_vcodigo_servicio_prov,
                        obj.ocsd_vdescripcion,
                        obj.ocsd_sfecha_entrega,
                        obj.ocsd_ncantidad,
                        obj.unidc_icod_unidad_medida,
                        obj.ocsd_ncunitaria,
                        obj.ocsd_nvalor_total,
                        obj.ocsd_ndescuento,
                        obj.ocsd_vdireccion_documento,
                        obj.ocsd_vcaracteristicas,
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
        public void EliminarOrdenCompraServicioDetalle(int ocod_icod_detalle_oc)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_SERVICIO_DETALLE_ELIMINAR(ocod_icod_detalle_oc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Orden Compra Importacion
        public List<EOrdenCompraImportacion> ListarOrdenCompraImportacion()
        {
            List<EOrdenCompraImportacion> lista = new List<EOrdenCompraImportacion>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEO_ORDEN_COMPRA_IMPORTACION_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenCompraImportacion()
                        {
                            ocic_icod_oci = item.ocic_icod_oci,
                            ocic_ianio = Convert.ToInt32(item.ocic_ianio),
                            ocic_vnumero_oci = item.ocic_vnumero_oci,
                            ocic_sfecha_oci = Convert.ToDateTime(item.ocic_sfecha_oci),
                            proc_icod_proveedor = item.proc_icod_proveedor,
                            prvc_vcod_proveedor = item.prvc_vcod_proveedor,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            ocic_npor_imp_igv = Convert.ToDecimal(item.ocic_npor_imp_igv),
                            ocic_nmonto_neto = Convert.ToDecimal(item.ocic_nmonto_neto),
                            ocic_nmonto_imp = Convert.ToDecimal(item.ocic_nmonto_imp),
                            ocic_nmonto_total = Convert.ToDecimal(item.ocic_nmonto_total),
                            tablc_iid_situacion_oci = Convert.ToInt32(item.tablc_iid_situacion_oci),
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            strMoneda = item.tablc_iid_tipo_moneda == 3 ? "S/." : "$/.",
                            str_situacion = item.str_situacion,
                            ocic_bincluido_igv = Convert.ToBoolean(item.ocic_bincluido_igv),
                            ocic_nmonto_sub_Total = Convert.ToDecimal(item.ocic_nmonto_sub_Total),
                            ocic_npor_descuento = Convert.ToDecimal(item.ocic_npor_descuento),
                            ocic_nmonto_descuento = Convert.ToDecimal(item.ocic_nmonto_descuento),
                            proc_vruc = item.proc_vruc,
                            proc_vdireccion = item.proc_vdireccion,
                            proc_vcorreo = item.proc_vcorreo,
                            ocic_vtelefono = item.ococ_vtelefono,
                            ocic_vreferencia = item.ocic_vreferencia,
                            ocic_iid_motivo = Convert.ToInt32(item.ocic_iid_motivo),
                            str_motivo = item.str_motivo,
                            ocic_iid_proyecto = Convert.ToInt32(item.pryc_icod_proyecto),
                            cecoc_vcodigo_centro_costo = item.cecoc_vcodigo_centro_costo,
                            ocic_vlugar_entrega = item.ocic_vlugar_entrega,
                            ocic_vgarantia = item.ocic_vgarantia,
                            ocic_vnota_ocl = item.ocic_vnota_ocl,
                            ocic_vcontacto = item.ocic_vcontacto,
                            ocic_vforma_pago = item.ocic_vforma_pago,
                            ocic_vincoterm = item.ocic_vincoterm,
                            ocic_sfecha_entrega = item.ocic_sfecha_entrega,
                            ocic_vcotizacion = item.ocic_vcotizacion,
                            ocic_vsolicitante = item.ocic_vsolicitante
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
        public int InsertarOrdenCompraImportacion(EOrdenCompraImportacion obj)
        {
            try
            {
                int? intIcod = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_IMPORTACION_INSERTAR(
                         ref intIcod,
                        obj.ocic_ianio,
                        obj.ocic_vnumero_oci,
                        obj.ocic_sfecha_oci,
                        obj.proc_icod_proveedor,
                        obj.prvc_vcod_proveedor,
                        obj.tablc_iid_tipo_moneda,
                        obj.ocic_npor_imp_igv,
                        obj.ocic_nmonto_neto,
                        obj.ocic_nmonto_imp,
                        obj.ocic_nmonto_total,
                        obj.tablc_iid_situacion_oci,
                        obj.ocic_bincluido_igv,
                        obj.ocic_nmonto_sub_Total,
                        obj.ocic_npor_descuento,
                        obj.ocic_nmonto_descuento,
                        obj.intUsuario,
                        obj.strPc,
                        obj.ocic_flag_estado,
                        obj.ocic_vtelefono,
                        obj.ocic_vreferencia,
                        obj.ocic_iid_motivo,
                        obj.ocic_iid_proyecto,
                        obj.ocic_vlugar_entrega,
                        obj.ocic_vgarantia,
                        obj.ocic_vnota_ocl,
                        obj.ocic_vcontacto,
                        obj.ocic_vforma_pago,
                        obj.ocic_vincoterm,
                        obj.ocic_sfecha_entrega,
                        obj.ocic_vcotizacion,
                        obj.ocic_vsolicitante
                    );

                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarOrdenCompraImportacion(EOrdenCompraImportacion obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_IMPORTACION_MODIFICAR(
                        obj.ocic_icod_oci,
                        obj.ocic_sfecha_oci,
                        obj.ocic_npor_imp_igv,
                        obj.ocic_nmonto_neto,
                        obj.ocic_nmonto_imp,
                        obj.ocic_nmonto_total,
                        obj.ocic_nmonto_sub_Total,
                        obj.ocic_npor_descuento,
                        obj.ocic_nmonto_descuento,
                        obj.intUsuario,
                        obj.strPc,
                        obj.ocic_bincluido_igv,
                        obj.ocic_vtelefono,
                        obj.ocic_vreferencia,
                        obj.ocic_iid_motivo,
                        obj.ocic_iid_proyecto,
                        obj.ocic_vlugar_entrega,
                        obj.ocic_vgarantia,
                        obj.ocic_vnota_ocl,
                        obj.ocic_vcontacto,
                        obj.ocic_vforma_pago,
                        obj.ocic_vincoterm,
                        obj.ocic_sfecha_entrega,
                        obj.ocic_vcotizacion,
                        obj.ocic_vsolicitante
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarOrdenCompraImportacion(int ococ_icod_orden_compra)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_IMPORTACION_ELIMINAR(ococ_icod_orden_compra);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AnularOrdenCompraImportacion(int ococ_icod_orden_compra, int tablc_iid_situacion_oc)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_IMPORTACION_ANULAR(ococ_icod_orden_compra, tablc_iid_situacion_oc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Orden Compra Importacion Detalle
        public List<EOrdenCompraImportacion> ListarOrdenCompraImportacionDetalle(int ococ_icod_orden_compra)
        {
            List<EOrdenCompraImportacion> lista = new List<EOrdenCompraImportacion>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEO_ORDEN_COMPRA_IMPORTACION_DETALLE_LISTAR(ococ_icod_orden_compra);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenCompraImportacion()
                        {
                            ocid_icod_detalle_oci = item.ocid_icod_detalle_oci,
                            ocic_icod_oci = Convert.ToInt32(item.ocic_icod_oci),
                            ocid_iitem = Convert.ToInt32(item.ocid_iitem),
                            prdc_icod_producto = item.prdc_icod_producto,
                            ocid_ncantidad = Convert.ToDecimal(item.ocid_ncantidad),
                            ocid_ncunitario = Convert.ToDecimal(item.ocid_ncunitario),
                            ocid_nmonto_total = Convert.ToDecimal(item.ocid_nmonto_total),
                            strCodigoProducto = item.prdc_vcode_producto,
                            strDescProducto = item.prdc_vdescripcion_larga,
                            strMedida = item.strUniMed,
                            strAbrevUniMed = item.strAbrevUniMed,
                            ocid_vdescripcion = item.ocid_vdescripcion,
                            ocid_ndescuento_item = Convert.ToDecimal(item.ocid_ndescuento_item),
                            ocid_vcaracteristicas = item.ocid_vcaracteristicas,
                            prdc_vcodigo_fabricante = item.prdc_vcodigo_fabricante
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
        public int InsertarOrdenCompraImportacionDetalle(EOrdenCompraImportacion obj)
        {
            try
            {
                int? intIcod = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_IMPORTACION_DETALLE_INSERTAR(
                         ref intIcod,
                        obj.ocic_icod_oci,
                        obj.ocid_iitem,
                        obj.prdc_icod_producto,
                        obj.ocid_ncantidad,
                        obj.ocid_ncunitario,
                        obj.ocid_nmonto_total,
                        obj.intUsuario,
                        obj.strPc,
                        obj.ocid_flag_estado,
                        obj.ocid_vdescripcion,
                        obj.ocid_ndescuento_item,
                        obj.ocid_vcaracteristicas

                    );

                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarOrdenCompraImportacionDetalle(EOrdenCompraImportacion obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_IMPORTACION_DETALLE_MODIFICAR(
                        obj.ocid_icod_detalle_oci,
                        obj.ocid_iitem,
                        obj.ocid_ncantidad,
                        obj.ocid_ncunitario,
                        obj.ocid_nmonto_total,
                        obj.intUsuario,
                        obj.strPc,
                        obj.ocid_vdescripcion,
                        obj.ocid_ndescuento_item,
                        obj.ocid_vcaracteristicas

                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarOrdenCompraImportacionDetalle(int ocod_icod_detalle_oc)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEO_ORDEN_COMPRA_IMPORTACION_DETALLE_ELIMINAR(ocod_icod_detalle_oc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Guia Remision Compras
        public List<EGuiaRemisionCompras> listarGuiaRemisionCompras(int anio)
        {
            List<EGuiaRemisionCompras> lista = new List<EGuiaRemisionCompras>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_GUIA_REMISION_COMPRAS_LISTAR(anio);
                    foreach (var item in query)
                    {
                        lista.Add(new EGuiaRemisionCompras()
                        {
                            grcc_icod_grc = item.grcc_icod_grc,
                            grcc_vnumero_grc = item.grcc_vnumero_grc,
                            grcc_sfecha_grc = Convert.ToDateTime(item.grcc_sfecha_grc),
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            NomProveedor = item.NomProveedor,
                            ococ_icod_orden_compra = Convert.ToInt32(item.ococ_icod_orden_compra),
                            NumOC = item.NumOC,
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            DesAlmacen = item.DesAlmacen,
                            tablc_iid_motivo = Convert.ToInt32(item.tablc_iid_motivo),
                            Motivo = item.Motivo,
                            tablc_iid_situacion_grc = Convert.ToInt32(item.tablc_iid_situacion_grc),
                            Situacion = item.Situacion,
                            grcc_sfecha_ingreso = Convert.ToDateTime(item.grcc_sfecha_ingreso),
                            grcc_ncantidad = Convert.ToDecimal(item.grcc_ncantidad),
                            NumFac = item.NumFac
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
        public int insertarGuiaRemisionCompras(EGuiaRemisionCompras oBe)
        {
            try
            {
                int? intIcod = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_GUIA_REMISION_COMPRAS_INSERTAR(
                        ref intIcod,
                         oBe.grcc_vnumero_grc,
                        oBe.grcc_sfecha_grc,
                        oBe.proc_icod_proveedor,
                        oBe.ococ_icod_orden_compra,
                        oBe.almac_icod_almacen,
                        oBe.tablc_iid_motivo,
                        oBe.tablc_iid_situacion_grc,
                        oBe.grcc_sfecha_ingreso,
                        oBe.grcc_ncantidad,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.grcc_flag_estatdo
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGuiaRemisionCompras(EGuiaRemisionCompras oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_GUIA_REMISION_COMPRAS_MODIFICAR(
                        oBe.grcc_icod_grc,
                        oBe.grcc_vnumero_grc,
                        oBe.grcc_sfecha_grc,
                        oBe.proc_icod_proveedor,
                        oBe.ococ_icod_orden_compra,
                        oBe.almac_icod_almacen,
                        oBe.tablc_iid_motivo,
                        oBe.tablc_iid_situacion_grc,
                        oBe.grcc_sfecha_ingreso,
                        oBe.grcc_ncantidad,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarGuiaRemisionCompras(EGuiaRemisionCompras oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_GUIA_REMISION_COMPRAS_ELIMINAR(
                        oBe.grcc_icod_grc,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Guia Remision Compras Detalle
        public List<EGuiaRemisionComprasDetalle> listarGuiaRemisionComprasDetalle(int GuiaRemision)
        {
            List<EGuiaRemisionComprasDetalle> lista = new List<EGuiaRemisionComprasDetalle>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_GUIA_REMISION_COMPRAS_DETALLE_LISTAR(GuiaRemision);
                    foreach (var item in query)
                    {
                        lista.Add(new EGuiaRemisionComprasDetalle()
                        {
                            grcd_icod_detalle = item.grcd_icod_detalle,
                            grcd_iid_detalle = item.grcd_iid_detalle,
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            strCodProd = item.strCodProd,
                            DesProducto = item.DesProducto,
                            Unidad = item.Unidad,
                            kardc_icod_correlativo = item.kardc_icod_correlativo,
                            ocod_icod_detalle_oc = Convert.ToInt32(item.ocod_icod_detalle_oc),
                            CantidadSaldo = Convert.ToDecimal(item.CantidadSaldo),
                            grcd_ncantidad = Convert.ToDecimal(item.grcd_ncantidad),
                            CantidadRecibidaOC = Convert.ToDecimal(item.CantidadRecibidaOC),
                            grcd_flag_estado = Convert.ToBoolean(item.grcd_flag_estado)
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
        public List<EGuiaRemisionComprasDetalle> listarGuiaRemisionComprasDetalleFC(int GuiaRemision)
        {
            List<EGuiaRemisionComprasDetalle> lista = new List<EGuiaRemisionComprasDetalle>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_GUIA_REMISION_COMPRAS_DETALLE_LISTAR_FC(GuiaRemision);
                    foreach (var item in query)
                    {
                        lista.Add(new EGuiaRemisionComprasDetalle()
                        {
                            grcd_icod_detalle = item.grcd_icod_detalle,
                            grcd_iid_detalle = item.grcd_iid_detalle,
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            strCodProd = item.strCodProd,
                            DesProducto = item.DesProducto,
                            Unidad = item.Unidad,
                            kardc_icod_correlativo = item.kardc_icod_correlativo,
                            ocod_icod_detalle_oc = Convert.ToInt32(item.ocod_icod_detalle_oc),
                            CantidadSaldo = Convert.ToDecimal(item.CantidadSaldo),
                            grcd_ncantidad = Convert.ToDecimal(item.grcd_ncantidad),
                            CantidadRecibidaOC = Convert.ToDecimal(item.CantidadRecibidaOC),
                            grcd_flag_estado = Convert.ToBoolean(item.grcd_flag_estado)
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
        public int insertarGuiaRemisionComprasDetalle(EGuiaRemisionComprasDetalle oBe)
        {
            try
            {
                int? intIcod = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_GUIA_REMISION_COMPRAS_DETALLE_INSERTAR(
                        ref intIcod,
                        oBe.grcd_iid_detalle,
                        oBe.grcc_icod_grcc,
                        oBe.prdc_icod_producto,
                        oBe.kardc_icod_correlativo,
                        oBe.ocod_icod_detalle_oc,
                        oBe.grcd_ncantidad,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.grcd_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGuiaRemisionDetalle(EGuiaRemisionComprasDetalle oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_GUIA_REMISION_COMPRAS_DETALLE_MODIFICAR(
                        oBe.grcd_icod_detalle,
                        oBe.grcd_iid_detalle,
                        oBe.grcc_icod_grcc,
                        oBe.prdc_icod_producto,
                        oBe.kardc_icod_correlativo,
                        oBe.ocod_icod_detalle_oc,
                        oBe.grcd_ncantidad,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarGuiaRemisionComprasDetalle(EGuiaRemisionComprasDetalle oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_GUIA_REMISION_COMPRAS_DETALLE_ELIMINAR(
                        oBe.grcd_icod_detalle,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Guia Remision Compras Mercaderia
        public List<EGuiaRemisionComprasMercaderia> listarGuiaRemisionComprasMercaderia()
        {
            List<EGuiaRemisionComprasMercaderia> lista = new List<EGuiaRemisionComprasMercaderia>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_GUIA_REMISION_COMPRAS_MERCADERIA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EGuiaRemisionComprasMercaderia()
                        {
                            grcc_icod_grc = item.grcc_icod_grc,
                            grcc_vnumero_grc = item.grcc_vnumero_grc,
                            grcc_sfecha_grc = Convert.ToDateTime(item.grcc_sfecha_grc),
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            NomProveedor = item.NomProveedor,
                            ococ_icod_orden_compra = Convert.ToInt32(item.ococ_icod_orden_compra),
                            NumOC = item.NumOC,
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            DesAlmacen = item.DesAlmacen,
                            tablc_iid_motivo = Convert.ToInt32(item.tablc_iid_motivo),
                            Motivo = item.Motivo,
                            tablc_iid_situacion_grc = Convert.ToInt32(item.tablc_iid_situacion_grc),
                            Situacion = item.Situacion,
                            grcc_sfecha_ingreso = Convert.ToDateTime(item.grcc_sfecha_ingreso),
                            grcc_ncantidad = Convert.ToDecimal(item.grcc_ncantidad),
                            NumFac = item.NumFac
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
        public int insertarGuiaRemisionComprasMercaderia(EGuiaRemisionComprasMercaderia oBe)
        {
            try
            {
                int? intIcod = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_GUIA_REMISION_COMPRAS_MERCADERIA_INSERTAR(
                        ref intIcod,
                         oBe.grcc_vnumero_grc,
                        oBe.grcc_sfecha_grc,
                        oBe.proc_icod_proveedor,
                        oBe.ococ_icod_orden_compra,
                        oBe.almac_icod_almacen,
                        oBe.tablc_iid_motivo,
                        oBe.tablc_iid_situacion_grc,
                        oBe.grcc_sfecha_ingreso,
                        oBe.grcc_ncantidad,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.grcc_flag_estatdo
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGuiaRemisionComprasMercaderia(EGuiaRemisionComprasMercaderia oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_GUIA_REMISION_COMPRAS_MERCADERIA_MODIFICAR(
                        oBe.grcc_icod_grc,
                        oBe.grcc_vnumero_grc,
                        oBe.grcc_sfecha_grc,
                        oBe.proc_icod_proveedor,
                        oBe.ococ_icod_orden_compra,
                        oBe.almac_icod_almacen,
                        oBe.tablc_iid_motivo,
                        oBe.tablc_iid_situacion_grc,
                        oBe.grcc_sfecha_ingreso,
                        oBe.grcc_ncantidad,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarGuiaRemisionComprasMercaderia(EGuiaRemisionComprasMercaderia oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_GUIA_REMISION_COMPRAS_MERCADERIA_ELIMINAR(
                        oBe.grcc_icod_grc,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Guia Remision Compras Mercaderia Detalle
        public List<EGuiaRemisionComprasMercaderiaDetalle> listarGuiaRemisionComprasMercaderiaDetalle(int GuiaRemision)
        {
            List<EGuiaRemisionComprasMercaderiaDetalle> lista = new List<EGuiaRemisionComprasMercaderiaDetalle>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_GUIA_REMISION_COMPRAS_MERCADERIA_DETALLE_LISTAR(GuiaRemision);
                    foreach (var item in query)
                    {
                        lista.Add(new EGuiaRemisionComprasMercaderiaDetalle()
                        {
                            grcd_icod_detalle = item.grcd_icod_detalle,
                            grcd_iid_detalle = item.grcd_iid_detalle,
                            grcc_icod_grcc = item.grcc_icod_grcc,
                            prdc_icod_producto = Convert.ToInt32(item.pr_icod_producto),
                            strCodProd = item.strCodProd,
                            DesProducto = item.DesProducto,
                            Unidad = item.Unidad,
                            kardc_icod_correlativo = item.kardc_icod_correlativo,
                            ocod_icod_detalle_oc = Convert.ToInt32(item.ocod_icod_detalle_oc),
                            CantidadSaldo = Convert.ToDecimal(item.CantidadSaldo),
                            grcd_ncantidad = Convert.ToDecimal(item.grcd_ncantidad),
                            CantidadRecibidaOC = Convert.ToDecimal(item.CantidadRecibidaOC),
                            grcd_flag_estado = Convert.ToBoolean(item.grcd_flag_estado),
                            grcd_rango_talla = item.grcd_rango_talla,
                            grcd_talla1 = item.grcd_talla1,
                            grcd_talla2 = item.grcd_talla2,
                            grcd_talla3 = item.grcd_talla3,
                            grcd_talla4 = item.grcd_talla4,
                            grcd_talla5 = item.grcd_talla5,
                            grcd_talla6 = item.grcd_talla6,
                            grcd_talla7 = item.grcd_talla7,
                            grcd_talla8 = item.grcd_talla8,
                            grcd_talla9 = item.grcd_talla9,
                            grcd_talla10 = item.grcd_talla10,
                            grcd_cant_talla1 = item.grcd_cant_talla1,
                            grcd_cant_talla2 = item.grcd_cant_talla2,
                            grcd_cant_talla3 = item.grcd_cant_talla3,
                            grcd_cant_talla4 = item.grcd_cant_talla4,
                            grcd_cant_talla5 = item.grcd_cant_talla5,
                            grcd_cant_talla6 = item.grcd_cant_talla6,
                            grcd_cant_talla7 = item.grcd_cant_talla7,
                            grcd_cant_talla8 = item.grcd_cant_talla8,
                            grcd_cant_talla9 = item.grcd_cant_talla9,
                            grcd_cant_talla10 = item.grcd_cant_talla10,
                            grcd_iid_kardex1 = item.grcd_iid_kardex1,
                            grcd_iid_kardex2 = item.grcd_iid_kardex2,
                            grcd_iid_kardex3 = item.grcd_iid_kardex3,
                            grcd_iid_kardex4 = item.grcd_iid_kardex4,
                            grcd_iid_kardex5 = item.grcd_iid_kardex5,
                            grcd_iid_kardex6 = item.grcd_iid_kardex6,
                            grcd_iid_kardex7 = item.grcd_iid_kardex7,
                            grcd_iid_kardex8 = item.grcd_iid_kardex8,
                            grcd_iid_kardex9 = item.grcd_iid_kardex9,
                            grcd_iid_kardex10 = item.grcd_iid_kardex10,
                            CantOC1 = item.ocod_cant_talla1,
                            CantOC2 = item.ocod_cant_talla2,
                            CantOC3 = item.ocod_cant_talla3,
                            CantOC4 = item.ocod_cant_talla4,
                            CantOC5 = item.ocod_cant_talla5,
                            CantOC6 = item.ocod_cant_talla6,
                            CantOC7 = item.ocod_cant_talla7,
                            CantOC8 = item.ocod_cant_talla8,
                            CantOC9 = item.ocod_cant_talla9,
                            CantOC10 = item.ocod_cant_talla10,
                            SaldoOC1 = item.ocod_cant_saldo1,
                            SaldoOC2 = item.ocod_cant_saldo2,
                            SaldoOC3 = item.ocod_cant_saldo3,
                            SaldoOC4 = item.ocod_cant_saldo4,
                            SaldoOC5 = item.ocod_cant_saldo5,
                            SaldoOC6 = item.ocod_cant_saldo6,
                            SaldoOC7 = item.ocod_cant_saldo7,
                            SaldoOC8 = item.ocod_cant_saldo8,
                            SaldoOC9 = item.ocod_cant_saldo9,
                            SaldoOC10 = item.ocod_cant_saldo10
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
        public List<EGuiaRemisionComprasMercaderiaDetalle> listarGuiaRemisionComprasMercaderiaDetalleOCD(int ocod_icod_detalle_oc)
        {
            List<EGuiaRemisionComprasMercaderiaDetalle> lista = new List<EGuiaRemisionComprasMercaderiaDetalle>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_GUIA_REMISION_COMPRAS_MERCADERIA_DETALLE_LISTAR(ocod_icod_detalle_oc);
                    foreach (var item in query)
                    {
                        lista.Add(new EGuiaRemisionComprasMercaderiaDetalle()
                        {
                            grcd_icod_detalle = item.grcd_icod_detalle,
                            grcd_iid_detalle = item.grcd_iid_detalle,
                            prdc_icod_producto = Convert.ToInt32(item.pr_icod_producto),
                            strCodProd = item.strCodProd,
                            DesProducto = item.DesProducto,
                            Unidad = item.Unidad,
                            kardc_icod_correlativo = item.kardc_icod_correlativo,
                            ocod_icod_detalle_oc = Convert.ToInt32(item.ocod_icod_detalle_oc),
                            CantidadSaldo = Convert.ToDecimal(item.CantidadSaldo),
                            grcd_ncantidad = Convert.ToDecimal(item.grcd_ncantidad),
                            CantidadRecibidaOC = Convert.ToDecimal(item.CantidadRecibidaOC),
                            grcd_flag_estado = Convert.ToBoolean(item.grcd_flag_estado),
                            grcd_rango_talla = item.grcd_rango_talla,
                            grcd_talla1 = item.grcd_talla1,
                            grcd_talla2 = item.grcd_talla2,
                            grcd_talla3 = item.grcd_talla3,
                            grcd_talla4 = item.grcd_talla4,
                            grcd_talla5 = item.grcd_talla5,
                            grcd_talla6 = item.grcd_talla6,
                            grcd_talla7 = item.grcd_talla7,
                            grcd_talla8 = item.grcd_talla8,
                            grcd_talla9 = item.grcd_talla9,
                            grcd_talla10 = item.grcd_talla10,
                            grcd_cant_talla1 = item.grcd_cant_talla1,
                            grcd_cant_talla2 = item.grcd_cant_talla2,
                            grcd_cant_talla3 = item.grcd_cant_talla3,
                            grcd_cant_talla4 = item.grcd_cant_talla4,
                            grcd_cant_talla5 = item.grcd_cant_talla5,
                            grcd_cant_talla6 = item.grcd_cant_talla6,
                            grcd_cant_talla7 = item.grcd_cant_talla7,
                            grcd_cant_talla8 = item.grcd_cant_talla8,
                            grcd_cant_talla9 = item.grcd_cant_talla9,
                            grcd_cant_talla10 = item.grcd_cant_talla10,
                            grcd_iid_kardex1 = item.grcd_iid_kardex1,
                            grcd_iid_kardex2 = item.grcd_iid_kardex2,
                            grcd_iid_kardex3 = item.grcd_iid_kardex3,
                            grcd_iid_kardex4 = item.grcd_iid_kardex4,
                            grcd_iid_kardex5 = item.grcd_iid_kardex5,
                            grcd_iid_kardex6 = item.grcd_iid_kardex6,
                            grcd_iid_kardex7 = item.grcd_iid_kardex7,
                            grcd_iid_kardex8 = item.grcd_iid_kardex8,
                            grcd_iid_kardex9 = item.grcd_iid_kardex9,
                            grcd_iid_kardex10 = item.grcd_iid_kardex10
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
        //public List<EGuiaRemisionComprasDetalle> listarGuiaRemisionComprasDetalleFC(int GuiaRemision)
        //{
        //    List<EGuiaRemisionComprasDetalle> lista = new List<EGuiaRemisionComprasDetalle>();
        //    try
        //    {
        //        using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
        //        {
        //            var query = dc.SGE_GUIA_REMISION_COMPRAS_DETALLE_LISTAR_FC(GuiaRemision);
        //            foreach (var item in query)
        //            {
        //                lista.Add(new EGuiaRemisionComprasDetalle()
        //                {
        //                    grcd_icod_detalle = item.grcd_icod_detalle,
        //                    grcd_iid_detalle = item.grcd_iid_detalle,
        //                    prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
        //                    strCodProd = item.strCodProd,
        //                    DesProducto = item.DesProducto,
        //                    Unidad = item.Unidad,
        //                    kardc_icod_correlativo = item.kardc_icod_correlativo,
        //                    ocod_icod_detalle_oc = Convert.ToInt32(item.ocod_icod_detalle_oc),
        //                    CantidadSaldo = Convert.ToDecimal(item.CantidadSaldo),
        //                    grcd_ncantidad = Convert.ToDecimal(item.grcd_ncantidad),
        //                    CantidadRecibidaOC = Convert.ToDecimal(item.CantidadRecibidaOC),
        //                    grcd_flag_estado = Convert.ToBoolean(item.grcd_flag_estado)
        //                });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return lista;
        //}
        public int insertarGuiaRemisionComprasMercaderiaDetalle(EGuiaRemisionComprasMercaderiaDetalle oBe)
        {
            try
            {
                int? intIcod = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_GUIA_REMISION_COMPRAS_MERCADERIA_DETALLE_INSERTAR(
                        ref intIcod,
                        oBe.grcd_iid_detalle,
                        oBe.grcc_icod_grcc,
                        oBe.prdc_icod_producto,
                        oBe.kardc_icod_correlativo,
                        oBe.ocod_icod_detalle_oc,
                        oBe.grcd_ncantidad,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.grcd_flag_estado,
                        oBe.grcd_rango_talla,
                        oBe.grcd_talla1,
                        oBe.grcd_talla2,
                        oBe.grcd_talla3,
                        oBe.grcd_talla4,
                        oBe.grcd_talla5,
                        oBe.grcd_talla6,
                        oBe.grcd_talla7,
                        oBe.grcd_talla8,
                        oBe.grcd_talla9,
                        oBe.grcd_talla10,
                        oBe.grcd_cant_talla1,
                        oBe.grcd_cant_talla2,
                        oBe.grcd_cant_talla3,
                        oBe.grcd_cant_talla4,
                        oBe.grcd_cant_talla5,
                        oBe.grcd_cant_talla6,
                        oBe.grcd_cant_talla7,
                        oBe.grcd_cant_talla8,
                        oBe.grcd_cant_talla9,
                        oBe.grcd_cant_talla10,
                        oBe.grcd_iid_kardex1,
                        oBe.grcd_iid_kardex2,
                        oBe.grcd_iid_kardex3,
                        oBe.grcd_iid_kardex4,
                        oBe.grcd_iid_kardex5,
                        oBe.grcd_iid_kardex6,
                        oBe.grcd_iid_kardex7,
                        oBe.grcd_iid_kardex8,
                        oBe.grcd_iid_kardex9,
                        oBe.grcd_iid_kardex10

                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGuiaRemisionMercaderiaDetalle(EGuiaRemisionComprasMercaderiaDetalle oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_GUIA_REMISION_COMPRAS_MERCADERIA_DETALLE_MODIFICAR(
                        oBe.grcd_icod_detalle,
                        oBe.grcd_iid_detalle,
                        oBe.grcc_icod_grcc,
                        oBe.prdc_icod_producto,
                        oBe.kardc_icod_correlativo,
                        oBe.ocod_icod_detalle_oc,
                        oBe.grcd_ncantidad,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.grcd_rango_talla,
                        oBe.grcd_talla1,
                        oBe.grcd_talla2,
                        oBe.grcd_talla3,
                        oBe.grcd_talla4,
                        oBe.grcd_talla5,
                        oBe.grcd_talla6,
                        oBe.grcd_talla7,
                        oBe.grcd_talla8,
                        oBe.grcd_talla9,
                        oBe.grcd_talla10,
                        oBe.grcd_cant_talla1,
                        oBe.grcd_cant_talla2,
                        oBe.grcd_cant_talla3,
                        oBe.grcd_cant_talla4,
                        oBe.grcd_cant_talla5,
                        oBe.grcd_cant_talla6,
                        oBe.grcd_cant_talla7,
                        oBe.grcd_cant_talla8,
                        oBe.grcd_cant_talla9,
                        oBe.grcd_cant_talla10,
                        oBe.grcd_iid_kardex1,
                        oBe.grcd_iid_kardex2,
                        oBe.grcd_iid_kardex3,
                        oBe.grcd_iid_kardex4,
                        oBe.grcd_iid_kardex5,
                        oBe.grcd_iid_kardex6,
                        oBe.grcd_iid_kardex7,
                        oBe.grcd_iid_kardex8,
                        oBe.grcd_iid_kardex9,
                        oBe.grcd_iid_kardex10
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarGuiaRemisionComprasMercaderiaDetalle(EGuiaRemisionComprasMercaderiaDetalle oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_GUIA_REMISION_COMPRAS_MERCADERIA_DETALLE_ELIMINAR(
                        oBe.grcd_icod_detalle,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Recepcion Compras Mercaderia
        public List<ERecepcionComprasMercaderia> listarRecepcionComprasMercaderia()
        {
            List<ERecepcionComprasMercaderia> lista = new List<ERecepcionComprasMercaderia>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_RECEPCION_COMPRAS_MERCADERIA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ERecepcionComprasMercaderia()
                        {
                            rcmc_icod_rcm = item.rcmc_icod_rcm,
                            rcmc_vnumero_rcm = item.rcmc_vnumero_rcm,
                            rcmc_sfecha_rcm = Convert.ToDateTime(item.rcmc_sfecha_rcm),
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            NomProveedor = item.NomProveedor,
                            ococ_icod_orden_compra = Convert.ToInt32(item.ococ_icod_orden_compra),
                            NumOC = item.NumOC,
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            DesAlmacen = item.DesAlmacen,
                            tablc_iid_motivo = Convert.ToInt32(item.tablc_iid_motivo),
                            Motivo = item.Motivo,
                            tablc_iid_situacion_rcm = Convert.ToInt32(item.tablc_iid_situacion_rcm),
                            Situacion = item.Situacion,
                            rcmc_sfecha_ingreso = Convert.ToDateTime(item.rcmc_sfecha_ingreso),
                            rcmc_ncantidad = Convert.ToDecimal(item.rcmc_ncantidad)
                            //NumFac = item.NumFac
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
        public int insertarRecepcionComprasMercaderia(ERecepcionComprasMercaderia oBe)
        {
            try
            {
                int? intIcod = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_RECEPCION_COMPRAS_MERCADERIA_INSERTAR(
                        ref intIcod,
                         oBe.rcmc_vnumero_rcm,
                        oBe.rcmc_sfecha_rcm,
                        oBe.proc_icod_proveedor,
                        oBe.ococ_icod_orden_compra,
                        oBe.almac_icod_almacen,
                        oBe.tablc_iid_motivo,
                        oBe.tablc_iid_situacion_rcm,
                        oBe.rcmc_sfecha_ingreso,
                        oBe.rcmc_ncantidad,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.rcmc_flag_estatdo
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarRecepcionComprasMercaderia(ERecepcionComprasMercaderia oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_RECEPCION_COMPRAS_MERCADERIA_MODIFICAR(
                        oBe.rcmc_icod_rcm,
                        oBe.rcmc_vnumero_rcm,
                        oBe.rcmc_sfecha_rcm,
                        oBe.proc_icod_proveedor,
                        oBe.ococ_icod_orden_compra,
                        oBe.almac_icod_almacen,
                        oBe.tablc_iid_motivo,
                        oBe.tablc_iid_situacion_rcm,
                        oBe.rcmc_sfecha_ingreso,
                        oBe.rcmc_ncantidad,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarRecepcionComprasMercaderia(ERecepcionComprasMercaderia oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_RECEPCION_COMPRAS_MERCADERIA_ELIMINAR(
                        oBe.rcmc_icod_rcm,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Recepcion Compras Mercaderia Detalle
        public List<ERecepcionComprasMercaderiaDetalle> listarRecepcionComprasMercaderiaDetalle(int GuiaRemision)
        {
            List<ERecepcionComprasMercaderiaDetalle> lista = new List<ERecepcionComprasMercaderiaDetalle>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_RECEPCION_COMPRAS_MERCADERIA_DETALLE_LISTAR(GuiaRemision);
                    foreach (var item in query)
                    {
                        lista.Add(new ERecepcionComprasMercaderiaDetalle()
                        {
                            rcmd_icod_detalle = item.rcmd_icod_detalle,
                            rcmd_iid_detalle = item.rcmd_iid_detalle,
                            //rcmc_icod_rcm = item.rcmc_icod_rcm,
                            prdc_icod_producto = Convert.ToInt32(item.pr_icod_producto),
                            strCodProd = item.strCodProd,
                            DesProducto = item.DesProducto,
                            Unidad = item.Unidad,
                            kardc_icod_correlativo = item.kardc_icod_correlativo,
                            ocod_icod_detalle_oc = Convert.ToInt32(item.ocod_icod_detalle_oc),
                            CantidadSaldo = Convert.ToDecimal(item.CantidadSaldo),
                            rcmd_ncantidad = Convert.ToDecimal(item.rcmd_ncantidad),
                            CantidadRecibidaOC = Convert.ToDecimal(item.CantidadRecibidaOC),
                            rcmd_flag_estado = Convert.ToBoolean(item.rcmd_flag_estado),
                            rcmd_rango_talla = item.rcmd_rango_talla,
                            rcmd_talla1 = item.rcmd_talla1,
                            rcmd_talla2 = item.rcmd_talla2,
                            rcmd_talla3 = item.rcmd_talla3,
                            rcmd_talla4 = item.rcmd_talla4,
                            rcmd_talla5 = item.rcmd_talla5,
                            rcmd_talla6 = item.rcmd_talla6,
                            rcmd_talla7 = item.rcmd_talla7,
                            rcmd_talla8 = item.rcmd_talla8,
                            rcmd_talla9 = item.rcmd_talla9,
                            rcmd_talla10 = item.rcmd_talla10,
                            rcmd_cant_talla1 = item.rcmd_cant_talla1,
                            rcmd_cant_talla2 = item.rcmd_cant_talla2,
                            rcmd_cant_talla3 = item.rcmd_cant_talla3,
                            rcmd_cant_talla4 = item.rcmd_cant_talla4,
                            rcmd_cant_talla5 = item.rcmd_cant_talla5,
                            rcmd_cant_talla6 = item.rcmd_cant_talla6,
                            rcmd_cant_talla7 = item.rcmd_cant_talla7,
                            rcmd_cant_talla8 = item.rcmd_cant_talla8,
                            rcmd_cant_talla9 = item.rcmd_cant_talla9,
                            rcmd_cant_talla10 = item.rcmd_cant_talla10,
                            rcmd_iid_kardex1 = item.rcmd_iid_kardex1,
                            rcmd_iid_kardex2 = item.rcmd_iid_kardex2,
                            rcmd_iid_kardex3 = item.rcmd_iid_kardex3,
                            rcmd_iid_kardex4 = item.rcmd_iid_kardex4,
                            rcmd_iid_kardex5 = item.rcmd_iid_kardex5,
                            rcmd_iid_kardex6 = item.rcmd_iid_kardex6,
                            rcmd_iid_kardex7 = item.rcmd_iid_kardex7,
                            rcmd_iid_kardex8 = item.rcmd_iid_kardex8,
                            rcmd_iid_kardex9 = item.rcmd_iid_kardex9,
                            rcmd_iid_kardex10 = item.rcmd_iid_kardex10
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
        public List<ERecepcionComprasMercaderiaDetalle> listarRecepcionComprasMercaderiaDetalleOCD(int ocod_icod_detalle_oc)
        {
            List<ERecepcionComprasMercaderiaDetalle> lista = new List<ERecepcionComprasMercaderiaDetalle>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_RECEPCION_COMPRAS_MERCADERIA_DETALLE_LISTAR(ocod_icod_detalle_oc);
                    foreach (var item in query)
                    {
                        lista.Add(new ERecepcionComprasMercaderiaDetalle()
                        {
                            rcmd_icod_detalle = item.rcmd_icod_detalle,
                            rcmd_iid_detalle = item.rcmd_iid_detalle,
                            prdc_icod_producto = Convert.ToInt32(item.pr_icod_producto),
                            strCodProd = item.strCodProd,
                            DesProducto = item.DesProducto,
                            Unidad = item.Unidad,
                            kardc_icod_correlativo = item.kardc_icod_correlativo,
                            ocod_icod_detalle_oc = Convert.ToInt32(item.ocod_icod_detalle_oc),
                            CantidadSaldo = Convert.ToDecimal(item.CantidadSaldo),
                            rcmd_ncantidad = Convert.ToDecimal(item.rcmd_ncantidad),
                            CantidadRecibidaOC = Convert.ToDecimal(item.CantidadRecibidaOC),
                            rcmd_flag_estado = Convert.ToBoolean(item.rcmd_flag_estado),
                            rcmd_rango_talla = item.rcmd_rango_talla,
                            rcmd_talla1 = item.rcmd_talla1,
                            rcmd_talla2 = item.rcmd_talla2,
                            rcmd_talla3 = item.rcmd_talla3,
                            rcmd_talla4 = item.rcmd_talla4,
                            rcmd_talla5 = item.rcmd_talla5,
                            rcmd_talla6 = item.rcmd_talla6,
                            rcmd_talla7 = item.rcmd_talla7,
                            rcmd_talla8 = item.rcmd_talla8,
                            rcmd_talla9 = item.rcmd_talla9,
                            rcmd_talla10 = item.rcmd_talla10,
                            rcmd_cant_talla1 = item.rcmd_cant_talla1,
                            rcmd_cant_talla2 = item.rcmd_cant_talla2,
                            rcmd_cant_talla3 = item.rcmd_cant_talla3,
                            rcmd_cant_talla4 = item.rcmd_cant_talla4,
                            rcmd_cant_talla5 = item.rcmd_cant_talla5,
                            rcmd_cant_talla6 = item.rcmd_cant_talla6,
                            rcmd_cant_talla7 = item.rcmd_cant_talla7,
                            rcmd_cant_talla8 = item.rcmd_cant_talla8,
                            rcmd_cant_talla9 = item.rcmd_cant_talla9,
                            rcmd_cant_talla10 = item.rcmd_cant_talla10,
                            rcmd_iid_kardex1 = item.rcmd_iid_kardex1,
                            rcmd_iid_kardex2 = item.rcmd_iid_kardex2,
                            rcmd_iid_kardex3 = item.rcmd_iid_kardex3,
                            rcmd_iid_kardex4 = item.rcmd_iid_kardex4,
                            rcmd_iid_kardex5 = item.rcmd_iid_kardex5,
                            rcmd_iid_kardex6 = item.rcmd_iid_kardex6,
                            rcmd_iid_kardex7 = item.rcmd_iid_kardex7,
                            rcmd_iid_kardex8 = item.rcmd_iid_kardex8,
                            rcmd_iid_kardex9 = item.rcmd_iid_kardex9,
                            rcmd_iid_kardex10 = item.rcmd_iid_kardex10
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
        //public List<EGuiaRemisionComprasDetalle> listarGuiaRemisionComprasDetalleFC(int GuiaRemision)
        //{
        //    List<EGuiaRemisionComprasDetalle> lista = new List<EGuiaRemisionComprasDetalle>();
        //    try
        //    {
        //        using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
        //        {
        //            var query = dc.SGE_GUIA_REMISION_COMPRAS_DETALLE_LISTAR_FC(GuiaRemision);
        //            foreach (var item in query)
        //            {
        //                lista.Add(new EGuiaRemisionComprasDetalle()
        //                {
        //                    grcd_icod_detalle = item.grcd_icod_detalle,
        //                    grcd_iid_detalle = item.grcd_iid_detalle,
        //                    prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
        //                    strCodProd = item.strCodProd,
        //                    DesProducto = item.DesProducto,
        //                    Unidad = item.Unidad,
        //                    kardc_icod_correlativo = item.kardc_icod_correlativo,
        //                    ocod_icod_detalle_oc = Convert.ToInt32(item.ocod_icod_detalle_oc),
        //                    CantidadSaldo = Convert.ToDecimal(item.CantidadSaldo),
        //                    grcd_ncantidad = Convert.ToDecimal(item.grcd_ncantidad),
        //                    CantidadRecibidaOC = Convert.ToDecimal(item.CantidadRecibidaOC),
        //                    grcd_flag_estado = Convert.ToBoolean(item.grcd_flag_estado)
        //                });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return lista;
        //}
        public int insertarRecepcionComprasMercaderiaDetalle(ERecepcionComprasMercaderiaDetalle oBe)
        {
            try
            {
                int? intIcod = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_RECEPCION_COMPRAS_MERCADERIA_DETALLE_INSERTAR(
                        ref intIcod,
                        oBe.rcmd_iid_detalle,
                        oBe.rcmc_icod_rcm,
                        oBe.prdc_icod_producto,
                        oBe.kardc_icod_correlativo,
                        oBe.ocod_icod_detalle_oc,
                        oBe.rcmd_ncantidad,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.rcmd_flag_estado,
                        oBe.rcmd_rango_talla,
                        oBe.rcmd_talla1,
                        oBe.rcmd_talla2,
                        oBe.rcmd_talla3,
                        oBe.rcmd_talla4,
                        oBe.rcmd_talla5,
                        oBe.rcmd_talla6,
                        oBe.rcmd_talla7,
                        oBe.rcmd_talla8,
                        oBe.rcmd_talla9,
                        oBe.rcmd_talla10,
                        oBe.rcmd_cant_talla1,
                        oBe.rcmd_cant_talla2,
                        oBe.rcmd_cant_talla3,
                        oBe.rcmd_cant_talla4,
                        oBe.rcmd_cant_talla5,
                        oBe.rcmd_cant_talla6,
                        oBe.rcmd_cant_talla7,
                        oBe.rcmd_cant_talla8,
                        oBe.rcmd_cant_talla9,
                        oBe.rcmd_cant_talla10,
                        oBe.rcmd_iid_kardex1,
                        oBe.rcmd_iid_kardex2,
                        oBe.rcmd_iid_kardex3,
                        oBe.rcmd_iid_kardex4,
                        oBe.rcmd_iid_kardex5,
                        oBe.rcmd_iid_kardex6,
                        oBe.rcmd_iid_kardex7,
                        oBe.rcmd_iid_kardex8,
                        oBe.rcmd_iid_kardex9,
                        oBe.rcmd_iid_kardex10

                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarRecepcionMercaderiaDetalle(ERecepcionComprasMercaderiaDetalle oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_RECEPCION_COMPRAS_MERCADERIA_DETALLE_MODIFICAR(
                        oBe.rcmd_icod_detalle,
                        oBe.rcmd_iid_detalle,
                        oBe.rcmc_icod_rcm,
                        oBe.prdc_icod_producto,
                        oBe.kardc_icod_correlativo,
                        oBe.ocod_icod_detalle_oc,
                        oBe.rcmd_ncantidad,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.rcmd_rango_talla,
                        oBe.rcmd_talla1,
                        oBe.rcmd_talla2,
                        oBe.rcmd_talla3,
                        oBe.rcmd_talla4,
                        oBe.rcmd_talla5,
                        oBe.rcmd_talla6,
                        oBe.rcmd_talla7,
                        oBe.rcmd_talla8,
                        oBe.rcmd_talla9,
                        oBe.rcmd_talla10,
                        oBe.rcmd_cant_talla1,
                        oBe.rcmd_cant_talla2,
                        oBe.rcmd_cant_talla3,
                        oBe.rcmd_cant_talla4,
                        oBe.rcmd_cant_talla5,
                        oBe.rcmd_cant_talla6,
                        oBe.rcmd_cant_talla7,
                        oBe.rcmd_cant_talla8,
                        oBe.rcmd_cant_talla9,
                        oBe.rcmd_cant_talla10,
                        oBe.rcmd_iid_kardex1,
                        oBe.rcmd_iid_kardex2,
                        oBe.rcmd_iid_kardex3,
                        oBe.rcmd_iid_kardex4,
                        oBe.rcmd_iid_kardex5,
                        oBe.rcmd_iid_kardex6,
                        oBe.rcmd_iid_kardex7,
                        oBe.rcmd_iid_kardex8,
                        oBe.rcmd_iid_kardex9,
                        oBe.rcmd_iid_kardex10
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarRecepcionComprasMercaderiaDetalle(ERecepcionComprasMercaderiaDetalle oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_RECEPCION_COMPRAS_MERCADERIA_DETALLE_ELIMINAR(
                        oBe.rcmd_icod_detalle,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Listar Cantidad Saldo O/C
        public decimal listarCantidadSaldoOC(int ocod_icod_detalle_oc)
        {
            decimal? Cantidad_Saldo = 0;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_CANTIDAD_SALDO_OC(
                        ref Cantidad_Saldo,
                        ocod_icod_detalle_oc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Convert.ToDecimal(Cantidad_Saldo);
        }
        public decimal listarCantidadRecibidaOC(int ocod_icod_detalle_oc)
        {
            decimal? Cantidad_Saldo = 0;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_CANTIDAD_RECIBIDA_OC(
                        ref Cantidad_Saldo,
                        ocod_icod_detalle_oc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Convert.ToDecimal(Cantidad_Saldo);
        }
        #endregion
        #region Suma Cantidad Requeridad
        public void ActualizarCantidadRequeridaOC(int ocod_icod_detalle_oc)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.ACTUALIZAR_CANTIDAD_REQUERIDA_OC(ocod_icod_detalle_oc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarCantidadRecibidaOC(int ocod_icod_detalle_oc)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.ACTUALIZAR_CANTIDAD_RECIBIDA_OC(ocod_icod_detalle_oc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarCantidadRecibidaOCRCS(int ocod_icod_detalle_oc)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.ACTUALIZAR_CANTIDAD_RECIBIDA_OC_RCS(ocod_icod_detalle_oc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarCantidadRecibidaOCM(int ocod_icod_detalle_oc, int Indicador)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.ACTUALIZAR_CANTIDAD_RECIBIDA_OCM(ocod_icod_detalle_oc, Indicador);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Cambiar Situacion O/C
        public void CambiarSituacionOC(int ococ_icod_orden_compra)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.CAMBIAR_SITUACION_OC(ococ_icod_orden_compra);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CambiarSituacionGR(int grcc_icod_grc)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.CAMBIAR_SITUACION_GR(grcc_icod_grc);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Garantia Proveedores
        public List<EGarantiaProveedores> listarGarantiaProveedores()
        {
            List<EGarantiaProveedores> lista = new List<EGarantiaProveedores>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_GARANTIA_PROVEEDORES_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EGarantiaProveedores()
                        {
                            garp_icod_garantia = item.garp_icod_garantia,
                            garap_vnumero_garantia = item.garap_vnumero_garantia,
                            garp_sfecha_garantia = Convert.ToDateTime(item.garp_sfecha_garantia),
                            tablc_iid_situacion = Convert.ToInt32(item.tablc_iid_situacion),
                            Situacion = item.Situacion,
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            NomProv = item.NomProv,
                            pryc_icod_proyecto = Convert.ToInt32(item.pryc_icod_proyecto),
                            CentroCostos = item.CentroCostos,
                            DesProyecto = item.DesProyecto,
                            ocsc_icod_ocs = Convert.ToInt32(item.ocsc_icod_ocs),
                            NumOCS = item.NumOCS,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            Moneda = item.Moneda,
                            garp_nmonto = Convert.ToDecimal(item.garp_nmonto),
                            fcoc_icod_doc = Convert.ToInt32(item.fcoc_icod_doc),
                            NumDoc = item.NumDoc,
                            intDXP = Convert.ToInt64(item.intDXP),
                            pdxpc_icod_correlativo = Convert.ToInt64(item.pdxpc_icod_correlativo)

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
        public int insertarGarantiaProveedores(EGarantiaProveedores oBe)
        {
            int? intIcod = 0;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_GARANTIA_PROVEEDORES_INSERTAR(
                        ref intIcod,
                        oBe.garap_vnumero_garantia,
                        oBe.garp_sfecha_garantia,
                        oBe.tablc_iid_situacion,
                        oBe.proc_icod_proveedor,
                        oBe.pryc_icod_proyecto,
                        oBe.ocsc_icod_ocs,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.garp_nmonto,
                        oBe.fcoc_icod_doc,
                        oBe.doxpc_icod_correlativo,
                        oBe.pdxpc_icod_correlativo,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.garp_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGarantiaProveedores(EGarantiaProveedores oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_GARANTIA_PROVEEDORES_MODIFICAR(
                        oBe.garp_icod_garantia,
                        oBe.garap_vnumero_garantia,
                        oBe.garp_sfecha_garantia,
                        oBe.tablc_iid_situacion,
                        oBe.proc_icod_proveedor,
                        oBe.pryc_icod_proyecto,
                        oBe.ocsc_icod_ocs,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.garp_nmonto,
                        oBe.fcoc_icod_doc,
                        oBe.doxpc_icod_correlativo,
                        oBe.pdxpc_icod_correlativo,
                        oBe.intUsuario,
                        oBe.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarGarantiaProveedores(EGarantiaProveedores oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_GARANTIA_PROVEEDORES_ELIMINAR(
                        oBe.garp_icod_garantia,
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
        #region Importacion
        public List<EImportacion> ListarImportacion()
        {
            List<EImportacion> lista = new List<EImportacion>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {

                    var query = dc.SGEC_IMPORTACION_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EImportacion()
                        {

                            impc_icod_importacion = item.impc_icod_importacion,
                            impc_vnumero_importacion = item.impc_vnumero_importacion,
                            impc_sfecha_importacion = item.impc_sfecha_importacion,
                            tablc_iid_sit_import = item.tablc_iid_sit_import,
                            add_icod_aduana = item.add_icod_aduana,
                            impc_sfecha_embarque = item.impc_sfecha_embarque,
                            impc_vconoc_embarque = item.impc_vconoc_embarque,
                            impc_vprocedencia = item.impc_vprocedencia,
                            impc_vemp_transporte = item.impc_vemp_transporte,
                            impc_vnave = item.impc_vnave,
                            impc_vdua = item.impc_vdua,
                            impc_sfecha_arribo = item.impc_sfecha_arribo,
                            impc_sfecha_ingreso = item.impc_sfecha_ingreso,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            impc_nfactor_dolar = item.impc_nfactor_dolar,
                            impc_nfactor_sol = item.impc_nfactor_sol,
                            impc_flag_estado = item.impc_flag_estado,
                            add_vrazon = item.add_vrazon,
                            impc_vguia_ingreso = item.impc_vguia_ingreso,
                            pryc_icod_proyecto = item.pryc_icod_proyecto,
                            strSituacion = item.strSituacion,
                            strAduana = item.strAduana,
                            strMondeda = item.strMondeda,
                            strProyecto = item.strProyecto,
                            strSumSoles = item.strSumSoles,
                            strSumDolares = item.strSumDolares,
                            strFactSoles = item.strFactSoles,
                            strFactDolares = item.strFactDolares,
                            impc_nmonto_total_soles = Convert.ToDecimal(item.impc_nmonto_total_soles),
                            impc_nmonto_total_dolares = Convert.ToDecimal(item.impc_nmonto_total_dolares),
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            fcoc_sfecha_doc = item.fcoc_sfecha_doc

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
        public int InsertarImportacion(EImportacion oBe)
        {
            int? intIcod = 0;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_IMPORTACION_INSERTAR(
                        ref intIcod,
                        oBe.impc_vnumero_importacion,
                        oBe.impc_sfecha_importacion,
                        oBe.tablc_iid_sit_import,
                        oBe.add_icod_aduana,
                        oBe.impc_sfecha_embarque,
                        oBe.impc_vconoc_embarque,
                        oBe.impc_vprocedencia,
                        oBe.impc_vemp_transporte,
                        oBe.impc_vnave,
                        oBe.impc_vdua,
                        oBe.impc_sfecha_arribo,
                        oBe.impc_sfecha_ingreso,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.impc_nfactor_dolar,
                        oBe.impc_nfactor_sol,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.impc_flag_estado,
                        oBe.add_vrazon,
                        oBe.impc_vguia_ingreso,
                        oBe.pryc_icod_proyecto,
                        oBe.impc_nmonto_total_soles,
                        oBe.impc_nmonto_total_dolares
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarImportacion(EImportacion oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_IMPORTACION_MODIFICAR(

                        oBe.impc_icod_importacion,
                        oBe.impc_sfecha_importacion,
                        oBe.tablc_iid_sit_import,
                        oBe.add_icod_aduana,
                        oBe.impc_sfecha_embarque,
                        oBe.impc_vconoc_embarque,
                        oBe.impc_vprocedencia,
                        oBe.impc_vemp_transporte,
                        oBe.impc_vnave,
                        oBe.impc_vdua,
                        oBe.impc_sfecha_arribo,
                        oBe.impc_sfecha_ingreso,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.impc_nfactor_dolar,
                        oBe.impc_nfactor_sol,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.add_vrazon,
                        oBe.impc_vguia_ingreso,
                        oBe.pryc_icod_proyecto,
                        oBe.almac_icod_almacen,
                        oBe.fcoc_sfecha_doc,
                        oBe.impc_nmonto_total_soles,
                        oBe.impc_nmonto_total_dolares
                        );


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarImportacion(EImportacion oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_IMPORTACION_ELIMINAR(
                        oBe.impc_icod_importacion,
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
        #region Importacion_Conceptos


        public List<EImportacionConceptos> ListarImportacionConceptos(int codImportacion)
        {
            List<EImportacionConceptos> lista = new List<EImportacionConceptos>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {

                    var query = dc.SGEC_IMPORTACION_CONCEPTOS_LISTAR(codImportacion);
                    foreach (var item in query)
                    {
                        lista.Add(new EImportacionConceptos()
                        {
                            impd_icod_importacion_detalle = item.impd_icod_importacion_detalle,
                            impc_icod_importacion = item.impc_icod_importacion,
                            cpn_icod_concepto_nacional = item.cpn_icod_concepto_nacional,
                            cpnd_icod_detalle_nacional = item.cpnd_icod_detalle_nacional,
                            impd_nmont_tot_concepto = item.impd_nmont_tot_concepto,
                            impd_nmont_unit_concepto = item.impd_nmont_unit_concepto,
                            tablc_iid_tipo_moneda_origen = item.tablc_iid_tipo_moneda_origen,
                            impd_nmont_tot_concepto_origen = item.impd_nmont_tot_concepto_origen,
                            impd_nmont_tot_ejecut = item.impd_nmont_tot_ejecut,
                            impd_nmont_unit_ejecut = item.impd_nmont_unit_ejecut,
                            impd_flag_estado = item.impd_flag_estado,

                            //-----------------------------
                            cpn_vdescripcion_concepto_nacional = item.cpn_vdescripcion_concepto_nacional,
                            cpnd_vdescripcion = item.cpnd_vdescripcion,
                            prepd_nmont_tot_concepto = item.prepd_nmont_tot_concepto,
                            prepd_nmont_unit_concepto = item.prepd_nmont_unit_concepto,
                            prepd_nmont_tot_concepto_origen = item.prepd_nmont_tot_concepto_origen,
                            TipoMoneda = item.TipoMoneda,
                            strCod = item.strCod,
                            impd_nmonto_concepto_sol = item.impd_nmonto_concepto_sol,
                            impd_nmonto_concepto_dol = item.impd_nmonto_concepto_dol



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

        public int InsertarImportacionConceptos(EImportacionConceptos oBe)
        {
            int? intIcod = 0;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_IMPORTACION_CONCEPTOS_INSERTAR(
                        ref intIcod,
                        oBe.impc_icod_importacion,
                        oBe.cpn_icod_concepto_nacional,
                        oBe.cpnd_icod_detalle_nacional,
                        oBe.impd_nmont_tot_concepto,
                        oBe.impd_nmont_unit_concepto,
                        oBe.tablc_iid_tipo_moneda_origen,
                        oBe.impd_nmont_tot_concepto_origen,
                        oBe.impd_nmont_tot_ejecut,
                        oBe.impd_nmont_unit_ejecut,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.impd_flag_estado

                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarImportacionConceptos(EImportacionConceptos oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_IMPORTACION_CONCEPTOS_MODIFICAR(
                        oBe.impd_icod_importacion_detalle,
                        oBe.cpn_icod_concepto_nacional,
                        oBe.cpnd_icod_detalle_nacional,
                        oBe.impd_nmont_tot_concepto,
                        oBe.impd_nmont_unit_concepto,
                        oBe.tablc_iid_tipo_moneda_origen,
                        oBe.impd_nmont_tot_concepto_origen,
                        oBe.impd_nmont_tot_ejecut,
                        oBe.impd_nmont_unit_ejecut,
                        oBe.intUsuario,
                        oBe.strPc);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarImportacionConceptos(int impd_icod_importacion, int intUsuario, string strPc)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_IMPORTACION_CONCEPTOS_ELIMINAR(
                        impd_icod_importacion,
                        intUsuario,
                        strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarImportacionConceptosMontoSoles(int impc_icod_importacion, decimal impd_nmonto_concepto_sol)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_IMP_CONCEPT_MONTO_SOL(
                        impc_icod_importacion,
                        impd_nmonto_concepto_sol
                        );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarCostos(int fcoc_icod_doc, decimal fcod_nmonto_unit_costo, decimal fcod_nmonto_total_costo)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_ACTUALIZAR_COSTOS(
                        fcoc_icod_doc,
                        fcod_nmonto_unit_costo,
                        fcod_nmonto_total_costo
                        );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarImportacionConceptosMontoDolares(int impc_icod_importacion, decimal impd_nmonto_concepto_dol)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_IMP_CONCEPT_MONTO_DOL(
                        impc_icod_importacion,
                        impd_nmonto_concepto_dol
                        );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarImportacionConceptosDXPMontoSoles(int impc_icod_importacion, decimal impd_nmonto_concepto_sol)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_IMP_CONCEPT_DXP_MONTO_SOL(
                        impc_icod_importacion,
                        impd_nmonto_concepto_sol
                        );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarImportacionConceptosDXPMontoDolares(int impc_icod_importacion, decimal impd_nmonto_concepto_dol)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_IMP_CONCEPT_DXP_MONTO_DOL(
                        impc_icod_importacion,
                        impd_nmonto_concepto_dol
                        );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Importacion Factura

        public List<EImportacionFactura> ListarImportacionFactura(int codImp)
        {
            List<EImportacionFactura> lista = new List<EImportacionFactura>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {

                    var query = dc.SGEC_IMPORTACION_FACTURA_LISTAR(codImp);
                    foreach (var item in query)
                    {
                        lista.Add(new EImportacionFactura()
                        {

                            impd1_icod_import_factura = item.impd1_icod_import_factura,
                            impc_icod_importacion = item.impc_icod_importacion,    //--(Importación)
                            fcoc_icod_doc = item.fcoc_icod_doc,    // --(Factura de compra)		
                            impd1_flag_estado = item.impd1_flag_estado,
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            fcoc_num_doc = item.fcoc_num_doc,
                            fcoc_sfecha_doc = item.fcoc_sfecha_doc,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            tarec_vdescripcion = item.tarec_vdescripcion,
                            fcoc_nmonto_total_detalle = item.fcoc_nmonto_total_detalle,
                            fcoc_nmonto_tipo_cambio = item.fcoc_nmonto_tipo_cambio





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

        public int InsertarImportacionFactura(EImportacionFactura oBe)
        {
            int? intIcod = 0;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_IMPORTACION_FACTURA_INSERTAR(
                        ref intIcod,
                        oBe.impc_icod_importacion,
                        oBe.fcoc_icod_doc,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.impd1_flag_estado,
                        oBe.proc_icod_proveedor

                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ACTUALIZAR_FAC_COMPRA_IMP_FACT(int fcoc_icod_doc, int impd1_icod_import_factura, int impc_icod_importacion)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_ACTUALIZAR_FAC_COMPRA_IMP_FACT(

                       fcoc_icod_doc,
                       impd1_icod_import_factura,
                       impc_icod_importacion
                        );


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ACTUALIZAR_IMPORTACION_MONTOS(int impc_icod_importacion, decimal impc_nmonto_total_soles, decimal impc_nmonto_total_dolares)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_ACTUALIZAR_IMPORTACION_MONTOS(

                       impc_icod_importacion,
                       impc_nmonto_total_soles,
                       impc_nmonto_total_dolares
                        );


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarImportacionFactura(EImportacionFactura oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_IMPORTACION_FACTURA_ELIMINAR(
                        oBe.impd1_icod_import_factura,
                        oBe.intUsuario,
                        oBe.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EFacturaCompra> FILTAR_FAC_IMP_X_PROVEE(int CodProvee)
        {
            List<EFacturaCompra> lista = new List<EFacturaCompra>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {

                    var query = dc.SGEC_FILTAR_FAC_IMP_X_PROVEE(CodProvee);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaCompra()
                        {
                            proc_icod_proveedor = item.proc_icod_proveedor,
                            fcoc_icod_doc = item.fcoc_icod_doc,
                            fcoc_num_doc = item.fcoc_num_doc,
                            fcoc_sfecha_doc = item.fcoc_sfecha_doc,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            strMoneda = item.strMoneda,
                            fcoc_nmonto_total_detalle = item.fcoc_nmonto_total_detalle,
                            fcoc_vreferencia = item.fcoc_vreferencia
                            //impd1_icod_import_factura = item.impd1_icod_import_factura,
                            //impc_icod_importacion = item.impc_icod_importacion,    //--(Importación)
                            //fcoc_icod_doc = item.fcoc_icod_doc,    // --(Factura de compra)		
                            //impd1_flag_estado = item.impd1_flag_estado





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
        #region Suma Cantidad Facturada 
        public void ActualizarCantidadFacturada(int ocod_icod_detalle_oc, int prdc_icod_producto)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.ACTUALIZAR_CANTIDAD_FACTURADA_FCD(ocod_icod_detalle_oc, prdc_icod_producto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region DXP Importacion
        public List<EDXPImportacion> ListarDXPImportacion(long doxpc_icod_correlativo)
        {
            List<EDXPImportacion> lista = new List<EDXPImportacion>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_DXP_IMPORTACION_LISTAR(doxpc_icod_correlativo);
                    foreach (var item in query)
                    {
                        lista.Add(new EDXPImportacion()
                        {
                            dxpd2_icod_correlativo = item.dxpd2_icod_correlativo,
                            doxpc_icod_correlativo = Convert.ToInt32(item.doxpc_icod_correlativo),
                            impd_icod_importacion_detalle = Convert.ToInt32(item.impd_icod_importacion_detalle),
                            Rubros = item.Rubros,
                            impc_icod_importacion = Convert.ToInt32(item.impc_icod_importacion),
                            Concepto = item.Concepto,
                            NumImpo = item.NumImpo,
                            dxpd2_nmonto_importacion = Convert.ToDecimal(item.dxpd2_nmonto_importacion)
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
        public int InsertarDXPImportacion(EDXPImportacion oBe)
        {
            int? intIcod = 0;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_DXP_IMPORTACION_INSERTAR(
                        ref intIcod,
                        oBe.doxpc_icod_correlativo,
                        oBe.impc_icod_importacion,
                        oBe.impd_icod_importacion_detalle,
                        oBe.dxpd2_nmonto_importacion,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.dxpd2_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarDXPImportacion(EDXPImportacion oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_DXP_IMPORTACION_MODIFICAR(

                        oBe.dxpd2_icod_correlativo,
                        oBe.doxpc_icod_correlativo,
                        oBe.impc_icod_importacion,
                        oBe.impd_icod_importacion_detalle,
                        oBe.dxpd2_nmonto_importacion,
                        oBe.intUsuario,
                        oBe.strPc
                        );


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarDXPImportacion(EDXPImportacion oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_DXP_IMPORTACION_ELIMINAR(
                        oBe.dxpd2_icod_correlativo,
                        oBe.intUsuario,
                        oBe.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void eliminarDXPDetCtaContable(List<EDXPImportacion> lstDelete)
        //{
        //    TesoreriaData objTesoreriaData = new TesoreriaData();
        //    try
        //    {
        //        using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
        //        {
        //            lstDelete.ForEach(x =>
        //            {

        //                /**Se eliminan los datos del detalle**/
        //                dc.SGE_DXP_IMPORTACION_ELIMINAR(x.dxpd2_icod_correlativo, x.intUsuario, x.strPc);
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion
        #region Correlativo Letra
        public string ObtenerCorrelativoLetra(int anio)
        {
            string NumDocumento = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEG_OBTENER_CORRELATIVO_LETRA(anio);
                    foreach (var item in query)
                    {
                        NumDocumento = string.Format("{0:0}", (Convert.ToInt32(item.NumLetra)));
                        ;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NumDocumento;
        }
        #endregion
        #region DXP Importacion Detalle
        public List<EDXPImportacion> listarDXPImpDet(int IcodImpoDet)
        {
            List<EDXPImportacion> lista = new List<EDXPImportacion>();

            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EDXPImportacion>();
                    var query = dc.SGE_DXP_IMPORTACION_DETALLE_LISTAR(IcodImpoDet);
                    foreach (var item in query)
                    {
                        lista.Add(new EDXPImportacion()
                        {
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            doxpc_sfecha_doc = Convert.ToDateTime(item.doxpc_sfecha_doc),
                            Moneda = item.Moneda,
                            doxpc_nmonto_tipo_cambio = Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio),
                            dxpd2_nmonto_importacion = Convert.ToDecimal(item.dxpd2_nmonto_importacion),
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            impd_icod_importacion_detalle = Convert.ToInt32(item.impd_icod_importacion_detalle),
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc)
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
        #region 
        public List<EDXPImportacion> listarDXPImpDetTodo()
        {
            List<EDXPImportacion> lista = new List<EDXPImportacion>();

            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EDXPImportacion>();
                    var query = dc.SGE_DXP_IMPORTACION_DETALLE_LISTAR_TODO();
                    foreach (var item in query)
                    {
                        lista.Add(new EDXPImportacion()
                        {
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            doxpc_sfecha_doc = Convert.ToDateTime(item.doxpc_sfecha_doc),
                            Moneda = item.Moneda,
                            doxpc_nmonto_tipo_cambio = Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio),
                            dxpd2_nmonto_importacion = Convert.ToDecimal(item.dxpd2_nmonto_importacion),
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            impd_icod_importacion_detalle = Convert.ToInt32(item.impd_icod_importacion_detalle)
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
        #region Verificar Numero Factura
        public List<EFacturaCompra> getVerificarNumero(string vnumero)
        {
            List<EFacturaCompra> lista = new List<EFacturaCompra>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_FACTURA_COMPRA_VERIFICAR_NUEMRO(vnumero);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaCompra()
                        {
                            fcoc_icod_doc = item.fcoc_icod_doc

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
        #region Importacion Factura
        public List<EFacturaCompraDet> ListarFacturaImpor(int idImport)
        {
            List<EFacturaCompraDet> lista = new List<EFacturaCompraDet>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_IMPORTACION_FACTURA(idImport);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaCompraDet()
                        {
                            favc_vnumero_factura = item.fcoc_num_doc,
                            fcod_ncantidad = Convert.ToDecimal(item.fcod_ncantidad),
                            fcod_iitem = Convert.ToInt32(item.fcod_iitem),
                            fcod_nmonto_total = Convert.ToDecimal(item.fcod_nmonto_total),
                            fcod_vdescripcion_item = item.fcod_vdescripcion_item,
                            fcod_nmonto_unit = Convert.ToDecimal(item.fcod_nmonto_unit),
                            fcoc_nmonto_tipo_cambio = Convert.ToDecimal(item.tipocambio),
                            fcod_nmonto_unit_costo = Convert.ToDecimal(item.fcod_nmonto_unit_costo),
                            fcod_nmonto_total_costo = Convert.ToDecimal(item.fcod_nmonto_total_costo),
                            PunidS = Convert.ToDecimal(item.PunidS),
                            PunidCFactor = Convert.ToDecimal(item.PunidCFactor),
                            PunidCfactorTotal = Convert.ToDecimal(item.PunidCfactorTotal),
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            fcod_factor = Convert.ToDecimal(item.factor)

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
        public void SituacionFacturaImportacion(int impc_icod_importacion, int tablc_iid_sit_import)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_SITUACION_IMPORTACION(impc_icod_importacion, tablc_iid_sit_import);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EDetalleFacOC> ListarDetalleFacOC(int ococ_icod_orden_compra, int prd_icod_producto)
        {
            List<EDetalleFacOC> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EDetalleFacOC>();
                    var query = dc.SGE_LISTAR_DETALLE_FACTURA_OC(ococ_icod_orden_compra, prd_icod_producto);
                    foreach (var item in query)
                    {
                        lista.Add(new EDetalleFacOC()
                        {
                            fcoc_num_doc = item.fcoc_num_doc,
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            fcoc_sfecha_doc = Convert.ToDateTime(item.fcoc_sfecha_doc),
                            fcod_ncantidad = Convert.ToDecimal(item.fcod_ncantidad)
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
        #region Cantidad Recibida OC
        public void ActualizarCantidadRecibidaOC(int ococ_icod_orden_compra, int prdc_icod_producto, int ocod_icod_detalle_oc)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.ACTUALIZAR_CANTIDAD_OC(ococ_icod_orden_compra, prdc_icod_producto, ocod_icod_detalle_oc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region ComprasDaot

        public List<EComprasDaot> ListarComprasDaot(decimal monto, int anio)
        {
            List<EComprasDaot> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EComprasDaot>();
                    var query = dc.SIGTS_COMPRAS_DAOT_LISTAR(monto, anio);
                    foreach (var item in query)
                    {
                        string[] nombres = { string.Empty, string.Empty };
                        string app = string.Empty, apm = string.Empty;
                        if (item.proc_vnombre != null && item.tip_doc_proveedor != null && item.tip_doc_proveedor == 1)
                        {
                            nombres[0] = item.proc_vnombre.Split(' ')[0];
                            nombres[0] = (item.proc_vnombre.Split(' ').Count() == 2) ? item.proc_vnombre.Split(' ')[1] : string.Empty;
                            app = item.proc_vpaterno;
                            apm = item.proc_vmaterno;
                        }
                        lista.Add(new EComprasDaot()
                        {
                            proc_icod_proveedor = item.proc_icod_proveedor,
                            proc_vcod_proveedor = item.proc_vcod_proveedor,
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            valor_compra_dolares = item.valor_compra_dolares,
                            valor_compra_soles = item.valor_compra_soles,
                            tip_doc_proveedor = item.tip_doc_proveedor,
                            tipo_persona = item.tipo_persona,
                            num_doc_proveedor = item.num_doc_proveedor,
                            proc_vnombre1 = nombres[0],
                            proc_vnombre2 = nombres[1],
                            proc_vpaterno = app,
                            proc_vmaterno = apm
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

        public List<EComprasDaotDetalle> ListarComprasDaotDetallexProveedor(long proc_icod_proveedor, int anio)
        {
            List<EComprasDaotDetalle> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EComprasDaotDetalle>();
                    var query = dc.SIGTS_COMPRAS_DAOT_DETALLE_X_PROVEEDOR_LISTAR(proc_icod_proveedor, anio);
                    foreach (var item in query)
                    {
                        lista.Add(new EComprasDaotDetalle()
                        {
                            proc_icod_proveedor = item.proc_icod_proveedor,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            doxpc_sfecha_doc = item.doxpc_sfecha_doc,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            simboloMoneda = (item.tablc_iid_tipo_moneda == 1) ? "S/." : "US$",
                            doxpc_nmonto_tipo_cambio = item.doxpc_nmonto_tipo_cambio,
                            doxpc_nmonto_total_documento = item.doxpc_nmonto_total_documento,
                            valor_compra_soles = item.valor_compra_soles,
                            valor_compra_dolares = item.valor_compra_dolares,
                            valor_compra_dolares_str = (item.valor_compra_dolares == 0) ? string.Empty : Convert.ToDecimal(item.valor_compra_dolares).ToString("n2"),
                            valor_compra_mond_orig = item.valor_compra_mond_orig,
                            doxpc_vdescrip_transaccion = item.doxpc_vdescrip_transaccion
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

        public List<EComprasDaotDetalle> ListarComprasDaotDetalle(decimal monto, int anio)
        {
            List<EComprasDaotDetalle> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EComprasDaotDetalle>();
                    var query = dc.SIGTS_COMPRAS_DAOT_DETALLE_LISTAR(monto, anio);
                    foreach (var item in query)
                    {
                        lista.Add(new EComprasDaotDetalle()
                        {
                            proc_icod_proveedor = item.proc_icod_proveedor,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            doxpc_sfecha_doc = item.doxpc_sfecha_doc,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            simboloMoneda = (item.tablc_iid_tipo_moneda == 1) ? "S/." : "US$",
                            doxpc_nmonto_tipo_cambio = item.doxpc_nmonto_tipo_cambio,
                            doxpc_nmonto_total_documento = item.doxpc_nmonto_total_documento,
                            valor_compra_soles = item.valor_compra_soles,
                            valor_compra_dolares = item.valor_compra_dolares,
                            valor_compra_dolares_str = (item.valor_compra_dolares == 0) ? string.Empty : Convert.ToDecimal(item.valor_compra_dolares).ToString("n2"),
                            valor_compra_mond_orig = item.valor_compra_mond_orig,
                            doxpc_vdescrip_transaccion = item.doxpc_vdescrip_transaccion
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
        #region Registro Firmas
        public List<ERegistroFirmas> listarRegistroFirmas()
        {
            List<ERegistroFirmas> lista = new List<ERegistroFirmas>(); ;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_REGISTRO_FIRMAS_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ERegistroFirmas()
                        {
                            regf_icod_registro_firmas = item.regf_icod_registro_firmas,
                            regf_ocl_elavorado = item.regf_ocl_elavorado,
                            regf_ocl_autorizado = item.regf_ocl_autorizado,
                            regf_ocl_revisado = item.regf_ocl_revisado,
                            regf_oci_aprobado1 = item.regf_oci_aprobado1,
                            regf_oci_aprobado2 = item.regf_oci_aprobado2,
                            regf_oci_aprobado3 = item.regf_oci_aprobado3,
                            regf_oci_aprobado4 = item.regf_oci_aprobado4,
                            regf_ocs_elavorado = item.regf_ocs_elavorado,
                            regf_ocs_revisado = item.regf_ocs_revisado
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
        public void insertarRegistroFirmas(ERegistroFirmas obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_REGISTRO_FIRMAS_INSERTAR(
                        obj.regf_ocl_elavorado,
                        obj.regf_ocl_autorizado,
                        obj.regf_ocl_revisado,
                        obj.regf_oci_aprobado1,
                        obj.regf_oci_aprobado2,
                        obj.regf_oci_aprobado3,
                        obj.regf_oci_aprobado4,
                        obj.regf_ocs_elavorado,
                        obj.regf_ocs_revisado,
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
        public void modificarRegistroFirmas(ERegistroFirmas obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_REGISTRO_FIRMAS_MODIFICAR(
                        obj.regf_icod_registro_firmas,
                        obj.regf_ocl_elavorado,
                        obj.regf_ocl_autorizado,
                        obj.regf_ocl_revisado,
                        obj.regf_oci_aprobado1,
                        obj.regf_oci_aprobado2,
                        obj.regf_oci_aprobado3,
                        obj.regf_oci_aprobado4,
                        obj.regf_ocs_elavorado,
                        obj.regf_ocs_revisado,
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
        #endregion
        #region Documento Recepcion Compras
        public List<EDocRecepcionCompra> listarDocRecepcionCompras()
        {
            List<EDocRecepcionCompra> lista = new List<EDocRecepcionCompra>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_DOC_RECEPCION_COMPRAS_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EDocRecepcionCompra()
                        {
                            drcc_icod_doc_recepcion_compra = item.drcc_icod_doc_recepcion_compra,
                            drcc_vnumero_doc_recepcion_compra = item.drcc_vnumero_doc_recepcion_compra,
                            drcc_sfecha = Convert.ToDateTime(item.drcc_sfecha),
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            NomProveedor = item.NomProveedor,
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            DesAlmacen = item.DesAlmacen,
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            TipoAbreviatura = item.TipoAbreviatura,
                            tablc_iid_motivo = Convert.ToInt32(item.tablc_iid_motivo),
                            Motivo = item.Motivo,
                            tablc_iid_situacion = Convert.ToInt32(item.tablc_iid_situacion),
                            Situacion = item.Situacion,
                            drcc_vobservaciones = item.drcc_vobservaciones
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
        public int insertarDocRecepcionCompras(EDocRecepcionCompra oBe)
        {
            try
            {
                int? intIcod = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_DOC_RECEPCION_COMPRAS_INSERTAR(
                        ref intIcod,
                        oBe.drcc_vnumero_doc_recepcion_compra,
                        oBe.drcc_sfecha,
                        oBe.proc_icod_proveedor,
                        oBe.almac_icod_almacen,
                        oBe.tdocc_icod_tipo_doc,
                        oBe.tablc_iid_motivo,
                        oBe.tablc_iid_situacion,
                        oBe.drcc_vobservaciones,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarDocRecepcionCompras(EDocRecepcionCompra oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_DOC_RECEPCION_COMPRAS_MODIFICAR(
                        oBe.drcc_icod_doc_recepcion_compra,
                        oBe.drcc_vnumero_doc_recepcion_compra,
                        oBe.drcc_sfecha,
                        oBe.proc_icod_proveedor,
                        oBe.almac_icod_almacen,
                        oBe.tdocc_icod_tipo_doc,
                        oBe.tablc_iid_motivo,
                        oBe.tablc_iid_situacion,
                        oBe.drcc_vobservaciones,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarDocRecepcionCompras(EDocRecepcionCompra oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_DOC_RECEPCION_COMPRAS_ELIMINAR(
                        oBe.drcc_icod_doc_recepcion_compra,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Documento Recepcion Compras Detalle
        public List<EDocRecepcionCompraDet> listarDocRecepcionComprasDetalle(int GuiaRemision)
        {
            List<EDocRecepcionCompraDet> lista = new List<EDocRecepcionCompraDet>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_DOC_RECEPCION_COMPRA_DET_LISTAR(GuiaRemision);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocRecepcionCompraDet()
                        {
                            drcd_icod_doc_recepcion_compra = item.drcd_icod_doc_recepcion_compra,
                            drcc_icod_doc_recepcion_compra = Convert.ToInt32(item.drcc_icod_doc_recepcion_compra),
                            drcd_iitem = Convert.ToInt32(item.drcd_iitem),
                            prd_icod_producto = Convert.ToInt32(item.prd_icod_producto),
                            drcd_ncantidad = Convert.ToDecimal(item.drcd_ncantidad),
                            drcd_nmonto_unit = Convert.ToDecimal(item.drcd_nmonto_unit),
                            drcd_nmonto_total = Convert.ToDecimal(item.drcd_nmonto_total),
                            dcrd_icod_kardex = Convert.ToInt32(item.dcrd_icod_kardex),
                            drcd_vdescripcion_item = item.drcd_vdescripcion_item,
                            strCodProducto = item.strCodProducto,
                            strDesUM = item.strDesUM
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
        public int insertarDocRecepcionComprasDetalle(EDocRecepcionCompraDet oBe)
        {
            try
            {
                int? intIcod = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_DOC_RECEPCION_COMPRA_DET_INSERTAR(
                        ref intIcod,
                        oBe.drcc_icod_doc_recepcion_compra,
                        oBe.drcd_iitem,
                        oBe.prd_icod_producto,
                        oBe.drcd_ncantidad,
                        oBe.drcd_nmonto_unit,
                        oBe.drcd_nmonto_total,
                        oBe.dcrd_icod_kardex,
                        oBe.drcd_vdescripcion_item,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarDocRecepcionDetalle(EDocRecepcionCompraDet oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_DOC_RECEPCION_COMPRA_DET_MODIFICAR(
                        oBe.drcd_icod_doc_recepcion_compra,
                        oBe.drcc_icod_doc_recepcion_compra,
                        oBe.drcd_iitem,
                        oBe.prd_icod_producto,
                        oBe.drcd_ncantidad,
                        oBe.drcd_nmonto_unit,
                        oBe.drcd_nmonto_total,
                        oBe.dcrd_icod_kardex,
                        oBe.drcd_vdescripcion_item,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarDocRecepcionComprasDetalle(EDocRecepcionCompraDet oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_DOC_RECEPCION_COMPRA_DET_ELIMINAR(
                        oBe.drcd_icod_doc_recepcion_compra,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Recepcion Compras Suministros
        public List<ERecepcionComprasSuministros> listarRecepcionComprasSuministros()
        {
            List<ERecepcionComprasSuministros> lista = new List<ERecepcionComprasSuministros>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_RECEPCION_COMPRAS_SUMINISTROS_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ERecepcionComprasSuministros()
                        {
                            rcsc_icod_rcs = item.rcsc_icod_rcs,
                            rcsc_vnumero_rcs = item.rcsc_vnumero_rcs,
                            rcsc_sfecha_rcs = Convert.ToDateTime(item.rcsc_sfecha_rcs),
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            NomProveedor = item.NomProveedor,
                            ococ_icod_orden_compra = Convert.ToInt32(item.ococ_icod_orden_compra),
                            NumOC = item.NumOC,
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            DesAlmacen = item.DesAlmacen,
                            tablc_iid_motivo = Convert.ToInt32(item.tablc_iid_motivo),
                            Motivo = item.Motivo,
                            tablc_iid_situacion_rcs = Convert.ToInt32(item.tablc_iid_situacion_rcs),
                            Situacion = item.Situacion,
                            rcsc_sfecha_ingreso = Convert.ToDateTime(item.rcsc_sfecha_ingreso),
                            rcsc_ncantidad = Convert.ToDecimal(item.rcsc_ncantidad)
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
        public int insertarRecepcionComprasSuministros(ERecepcionComprasSuministros oBe)
        {
            try
            {
                int? intIcod = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_RECEPCION_COMPRAS_SUMINISTROS_INSERTAR(
                        ref intIcod,
                         oBe.rcsc_vnumero_rcs,
                        oBe.rcsc_sfecha_rcs,
                        oBe.proc_icod_proveedor,
                        oBe.ococ_icod_orden_compra,
                        oBe.almac_icod_almacen,
                        oBe.tablc_iid_motivo,
                        oBe.tablc_iid_situacion_rcs,
                        oBe.rcsc_sfecha_ingreso,
                        oBe.rcsc_ncantidad,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.rcsc_flag_estatdo
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarRecepcionComprasSuministros(ERecepcionComprasSuministros oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_RECEPCION_COMPRAS_SUMINISTROS_MODIFICAR(
                        oBe.rcsc_icod_rcs,
                        oBe.rcsc_vnumero_rcs,
                        oBe.rcsc_sfecha_rcs,
                        oBe.proc_icod_proveedor,
                        oBe.ococ_icod_orden_compra,
                        oBe.almac_icod_almacen,
                        oBe.tablc_iid_motivo,
                        oBe.tablc_iid_situacion_rcs,
                        oBe.rcsc_sfecha_ingreso,
                        oBe.rcsc_ncantidad,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarRecepcionComprasSuministros(ERecepcionComprasSuministros oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_RECEPCION_COMPRAS_SUMINISTROS_ELIMINAR(
                        oBe.rcsc_icod_rcs,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Recepcion Compras Suministros Detalle
        public List<ERecepcionComprasSuministrosDetalle> listarRecepcionComprasSuministrosDetalle(int GuiaRemision)
        {
            List<ERecepcionComprasSuministrosDetalle> lista = new List<ERecepcionComprasSuministrosDetalle>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_RECEPCION_COMPRAS_SUMINISTROS_DETALLE_LISTAR(GuiaRemision);
                    foreach (var item in query)
                    {
                        lista.Add(new ERecepcionComprasSuministrosDetalle()
                        {
                            rcsd_icod_detalle = item.rcsd_icod_detalle,
                            rcsd_iid_detalle = item.rcsd_iid_detalle,
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            strCodProd = item.strCodProd,
                            DesProducto = item.DesProducto,
                            Unidad = item.Unidad,
                            kardc_icod_correlativo = item.kardc_icod_correlativo,
                            ocod_icod_detalle_oc = Convert.ToInt32(item.ocod_icod_detalle_oc),
                            CantidadSaldo = Convert.ToDecimal(item.CantidadSaldo),
                            rcsd_ncantidad = Convert.ToDecimal(item.rcsd_ncantidad),
                            CantidadRecibidaOC = Convert.ToDecimal(item.CantidadRecibidaOC),
                            rcsd_flag_estado = Convert.ToBoolean(item.rcsd_flag_estado)
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
        public List<ERecepcionComprasSuministrosDetalle> listarRecepcionComprasSuministrosDetalleFC(int GuiaRemision)
        {
            List<ERecepcionComprasSuministrosDetalle> lista = new List<ERecepcionComprasSuministrosDetalle>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_RECEPCION_COMPRAS_SUMINISTROS_DETALLE_LISTAR_FC(GuiaRemision);
                    foreach (var item in query)
                    {
                        lista.Add(new ERecepcionComprasSuministrosDetalle()
                        {
                            rcsd_icod_detalle = item.rcsd_icod_detalle,
                            rcsd_iid_detalle = item.rcsd_iid_detalle,
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            strCodProd = item.strCodProd,
                            DesProducto = item.DesProducto,
                            Unidad = item.Unidad,
                            kardc_icod_correlativo = item.kardc_icod_correlativo,
                            ocod_icod_detalle_oc = Convert.ToInt32(item.ocod_icod_detalle_oc),
                            CantidadSaldo = Convert.ToDecimal(item.CantidadSaldo),
                            rcsd_ncantidad = Convert.ToDecimal(item.rcsd_ncantidad),
                            CantidadRecibidaOC = Convert.ToDecimal(item.CantidadRecibidaOC),
                            rcsd_flag_estado = Convert.ToBoolean(item.rcsd_flag_estado)
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
        public int insertarRecepcionComprasSuministrosDetalle(ERecepcionComprasSuministrosDetalle oBe)
        {
            try
            {
                int? intIcod = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_RECEPCION_COMPRAS_SUMINISTROS_DETALLE_INSERTAR(
                        ref intIcod,
                        oBe.rcsd_iid_detalle,
                        oBe.rcsc_icod_rcs,
                        oBe.prdc_icod_producto,
                        oBe.kardc_icod_correlativo,
                        oBe.ocod_icod_detalle_oc,
                        oBe.rcsd_ncantidad,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.rcsd_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarRecepcionComprasSuministrosDetalle(ERecepcionComprasSuministrosDetalle oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_RECEPCION_COMPRAS_SUMINISTROS_DETALLE_MODIFICAR(
                        oBe.rcsd_icod_detalle,
                        oBe.rcsd_iid_detalle,
                        oBe.rcsc_icod_rcs,
                        oBe.prdc_icod_producto,
                        oBe.kardc_icod_correlativo,
                        oBe.ocod_icod_detalle_oc,
                        oBe.rcsd_ncantidad,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarRecepcionComprasSuministrosDetalle(ERecepcionComprasSuministrosDetalle oBe)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_RECEPCION_COMPRAS_SUMINISTROS_DETALLE_ELIMINAR(
                        oBe.rcsd_icod_detalle,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public List<EProdTablaRegistro> getVerificarColor(int vnumero)
        {
            List<EProdTablaRegistro> lista = new List<EProdTablaRegistro>();
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_VERIFICAR_COLOR(vnumero);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdTablaRegistro()
                        {
                            icod_tabla = item.tarec_iid_tabla_registro

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

        #region Boleta Compra Cab
        public List<EBoletaCompra> listarBoletaCompraNacional(int intEjercicio)
        {
            List<EBoletaCompra> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EBoletaCompra>();
                    var query = dc.SGEC_BOLETA_COMPRA_NACIONAL_LISTAR(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EBoletaCompra()
                        {
                            bcoc_icod_doc = item.bcoc_icod_doc,
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            bcoc_num_doc = item.bcoc_num_doc,
                            bcoc_sfecha_doc = Convert.ToDateTime(item.bcoc_sfecha_doc),
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            bcoc_svencimiento = Convert.ToDateTime(item.bcoc_svencimiento),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            bcoc_iforma_pago = Convert.ToInt32(item.bcoc_iforma_pago),
                            bcoc_vreferencia = item.bcoc_vreferencia,
                            bcoc_nporcent_imp_doc = Convert.ToDecimal(item.bcoc_nporcent_imp_doc),
                            bcoc_nmonto_destino_gravado = Convert.ToDecimal(item.bcoc_nmonto_destino_gravado),
                            bcoc_nmonto_destino_mixto = Convert.ToDecimal(item.bcoc_nmonto_destino_mixto),
                            bcoc_nmonto_destino_nogravado = Convert.ToDecimal(item.bcoc_nmonto_destino_nogravado),
                            bcoc_nmonto_nogravado = Convert.ToDecimal(item.bcoc_nmonto_nogravado),
                            bcoc_nmonto_imp_destino_gravado = Convert.ToDecimal(item.bcoc_nmonto_imp_destino_gravado),
                            bcoc_nmonto_imp_destino_mixto = Convert.ToDecimal(item.bcoc_nmonto_imp_destino_mixto),
                            bcoc_nmonto_imp_destino_nogravado = Convert.ToDecimal(item.bcoc_nmonto_imp_destino_nogravado),
                            bcoc_nmonto_tipo_cambio = Convert.ToDecimal(item.bcoc_nmonto_tipo_cambio),
                            bcoc_imes_iid_proceso = Convert.ToInt16(item.bcoc_imes_iid_proceso),
                            bcoc_vnro_depo_detraccion = item.bcoc_vnro_depo_detraccion,
                            bcoc_sfecha_depo_detraccion = item.bcoc_sfecha_depo_detraccion,
                            bcoc_nmonto_total_detalle = Convert.ToDecimal(item.bcoc_nmonto_total_detalle),
                            bcoc_isituacion = Convert.ToInt32(item.fcoc_isituacion),
                            strProveedor = item.strProveedor,
                            strAlmacen = item.strAlmacen,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            strFormaPago = item.strFormaPago,
                            intDXP = Convert.ToInt64(item.intDXP),
                            bcoc_anio = item.bcoc_anio,
                            prep_icod_presupuesto = item.prep_icod_importacion,
                            //prep_cod_presupuesto = item.prep_cod_presupuesto,
                            bcoc_flag_importacion = Convert.ToBoolean(item.fcoc_flag_importacion),
                            ococ_icod_orden_compra = item.ococ_icod_orden_compra,
                            ococ_numero_orden_compra = item.ococ_numero_orden_compra,
                            bcoc_iestado_recep = Convert.ToInt32(item.bcoc_iestado_recep),
                            DesEstadoRecepcion = item.DesEstadoRecepcion,
                            bcoc_bincluye_igv = Convert.ToBoolean(item.bcoc_bincluye_igv),
                            bcoc_nmonto_neto_detalle = Convert.ToDecimal(item.bcoc_nmonto_neto_detalle),
                            impc_icod_importacion = Convert.ToInt32(item.impc_icod_importacion),
                            fcoc_tipo_documento = Convert.ToInt32(item.fcoc_tipo_documento),
                            fcoc_icod_documento = Convert.ToInt32(item.fcoc_icod_documento),
                            NumTD = item.NumTD

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
        public int insertarBoletaCompraNacional(EBoletaCompra obj)
        {

            try
            {
                int? idDocCompra = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_BOLETA_COMPRA_NACIONAL_INSERTAR(
                        ref idDocCompra,
                        obj.proc_icod_proveedor,
                        obj.bcoc_num_doc,
                        obj.bcoc_sfecha_doc,
                        obj.almac_icod_almacen,
                        obj.doxpc_icod_correlativo,
                        obj.bcoc_svencimiento,
                        obj.tablc_iid_tipo_moneda,
                        obj.bcoc_iforma_pago,
                        obj.bcoc_vreferencia,
                        obj.bcoc_nporcent_imp_doc,
                        obj.bcoc_nmonto_destino_gravado,
                        obj.bcoc_nmonto_destino_mixto,
                        obj.bcoc_nmonto_destino_nogravado,
                        obj.bcoc_nmonto_nogravado,
                        obj.bcoc_nmonto_imp_destino_gravado,
                        obj.bcoc_nmonto_imp_destino_mixto,
                        obj.bcoc_nmonto_imp_destino_nogravado,
                        obj.bcoc_nmonto_tipo_cambio,
                        obj.bcoc_imes_iid_proceso,
                        obj.bcoc_bincluye_igv,
                        obj.bcoc_vnro_depo_detraccion,
                        obj.bcoc_sfecha_depo_detraccion,
                        obj.bcoc_nmonto_neto_detalle,
                        obj.bcoc_nmonto_total_detalle,
                        obj.bcoc_isituacion,
                        obj.intUsuario,
                        obj.strPc,
                        obj.bcoc_anio,
                        obj.prep_icod_presupuesto,
                        obj.bcoc_flag_importacion,
                        obj.ococ_icod_orden_compra,
                        obj.bcoc_iestado_recep,
                        obj.fcoc_tipo_documento,
                        obj.fcoc_icod_documento
                    );

                }
                return Convert.ToInt32(idDocCompra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarBoletaCompraNacional(EBoletaCompra obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_BOLETA_COMPRA_NACIONAL_MODIFICAR(
                        obj.bcoc_icod_doc,
                        obj.proc_icod_proveedor,
                        obj.bcoc_num_doc,
                        obj.bcoc_sfecha_doc,
                        obj.almac_icod_almacen,
                        obj.doxpc_icod_correlativo,
                        obj.bcoc_svencimiento,
                        obj.tablc_iid_tipo_moneda,
                        obj.bcoc_iforma_pago,
                        obj.bcoc_vreferencia,
                        obj.bcoc_nporcent_imp_doc,
                        obj.bcoc_nmonto_destino_gravado,
                        obj.bcoc_nmonto_destino_mixto,
                        obj.bcoc_nmonto_destino_nogravado,
                        obj.bcoc_nmonto_nogravado,
                        obj.bcoc_nmonto_imp_destino_gravado,
                        obj.bcoc_nmonto_imp_destino_mixto,
                        obj.bcoc_nmonto_imp_destino_nogravado,
                        obj.bcoc_nmonto_tipo_cambio,
                        obj.bcoc_imes_iid_proceso,
                        obj.bcoc_bincluye_igv,
                        obj.bcoc_vnro_depo_detraccion,
                        obj.bcoc_sfecha_depo_detraccion,
                        obj.bcoc_nmonto_neto_detalle,
                        obj.bcoc_nmonto_total_detalle,
                        obj.bcoc_isituacion,
                        obj.intUsuario,
                        obj.strPc,
                        obj.bcoc_anio,
                        obj.ococ_icod_orden_compra,
                        obj.bcoc_iestado_recep
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarBoletaCompraNacional(EBoletaCompra obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_BOLETA_COMPRA_NACIONAL_ELIMINAR(
                        obj.bcoc_icod_doc,
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
        #endregion
        #region Boleta Compra Det
        public List<EBoletaCompraDet> listarBoletaCompraNacionalDet(int intFacCompra)
        {
            List<EBoletaCompraDet> lista = null;
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    lista = new List<EBoletaCompraDet>();
                    var query = dc.SGEC_BOLETA_COMPRA_NACIONAL_DET_LISTAR(intFacCompra);
                    foreach (var item in query)
                    {
                        lista.Add(new EBoletaCompraDet()
                        {
                            bcod_icod_doc = item.bcod_icod_doc,
                            bcod_iitem = Convert.ToInt32(item.bcod_iitem),
                            prd_icod_producto = Convert.ToInt32(item.prd_icod_producto),
                            bcod_ncantidad = Convert.ToInt32(item.bcod_ncantidad),
                            bcod_nmonto_unit = Convert.ToDecimal(item.bcod_nmonto_unit),
                            bcod_nmonto_total = Convert.ToDecimal(item.bcod_nmonto_total),
                            bcod_ndescuento = Convert.ToDecimal(item.bcod_ndescuento),
                            bcod_vdescripcion_item = item.bcod_vdescripcion_item,
                            ocod_icod_detalle_oc = Convert.ToInt32(item.ocod_icod_detalle_oc),
                            bcod_icod_kardex = Convert.ToInt32(item.bcod_icod_kardex),
                            strCodProducto = item.strCodProducto,
                            strDesUM = item.strDesUM,
                            strMoneda = item.strMoneda,


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
        public void insertarBoletaCompraNacionalDet(EBoletaCompraDet obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_BOLETA_COMPRA_NACIONAL_DET_INSERTAR(
                        obj.bcoc_icod_doc,
                        obj.bcod_iitem,
                        obj.prd_icod_producto,
                        obj.bcod_ncantidad,
                        obj.bcod_nmonto_unit,
                        obj.bcod_nmonto_total,
                        obj.bcod_ndescuento,
                        obj.bcod_icod_kardex,
                        obj.bcod_vdescripcion_item,
                        obj.ocod_icod_detalle_oc,
                        obj.intUsuario,
                        obj.strPc,
                        obj.grcd_icod_detalle

                    );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarBoletaCompraNacionalDet(EBoletaCompraDet obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGEC_BOLETA_COMPRA_NACIONAL_DET_MODIFICAR(
                        obj.bcod_icod_doc,
                        obj.bcoc_icod_doc,
                        obj.bcod_iitem,
                        obj.prd_icod_producto,
                        obj.bcod_ncantidad,
                        obj.bcod_nmonto_unit,
                        obj.bcod_nmonto_total,
                        obj.bcod_ndescuento,
                        obj.bcod_icod_kardex,
                        obj.bcod_vdescripcion_item,
                        obj.ocod_icod_detalle_oc,
                        obj.intUsuario,
                        obj.strPc,
                        obj.bcod_nmonto_unit_costo,
                        obj.bcod_nmonto_total_costo
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarBoletaCompraNacionalDet(EBoletaCompraDet obj)
        {
            try
            {
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {

                    dc.SGEC_BOLETA_COMPRA_NACIONAL_DET_ELIMINAR(
                        obj.bcod_icod_doc,
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
        #endregion

        public int insertarDocuemntosVarios(EDocumentosVarios obj)
        {
            try
            {
                int? intIcod = 0;
                using (ComprasDataContext dc = new ComprasDataContext(Helper.conexion()))
                {
                    dc.SGE_DOCUMENTOS_VARIOS_INSERTAR(
                    ref intIcod,
                    obj.fcoc_icod_doc,
                    obj.fcoc_icod_documento,
                    obj.intUsuario,
                    obj.strPc
                    );

                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

