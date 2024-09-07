using SGE.Common;
using SGE.DataAccess;
using SGE.Entity;
using SGE.Entity.FacturaElectronica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Transactions;
using static SGE.Common.Codigos;

namespace SGE.BusinessLogic
{
    public class BVentas
    {
        AlmacenData objAlmacenData = new AlmacenData();
        VentasData objVentasData = new VentasData();
        TesoreriaData objTesoreriaDara = new TesoreriaData();


        #region Cliente
        public List<ECliente> ListarCliente()
        {
            List<ECliente> lista = new List<ECliente>();
            try
            {
                lista = objVentasData.ListarCliente();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EUbigeo> UbigeoListar()
        {
            return objVentasData.UbigeoListar();
        }

        public int InsertarCliente(ECliente objECliente)
        {
            int id_cliente = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    id_cliente = objVentasData.InsertarCliente(objECliente);
                    tx.Complete();
                }
                return id_cliente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarCliente(ECliente objECliente)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarCliente(objECliente);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarCliente(int cliec_icod_cliente, int usuario, string pc, int anac_icod_analitica)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.EliminarCliente(cliec_icod_cliente, usuario, pc, anac_icod_analitica);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ECuotasFactura> CuotasFacturaListar(long doxcc_icod_correlativo)
        {
            return objVentasData.CuotasFacturaListar(doxcc_icod_correlativo);
        }

        public void ActualizarClienteAnalitica(ECliente objECliente)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarClienteAnalitica(objECliente);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Ubicacion
        public List<EUbicacion> ListarUbicacion()
        {
            List<EUbicacion> lista = null;
            try
            {
                lista = objVentasData.ListarUbicacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarUbicacion(EUbicacion objEUbicaion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.InsertarUbicacion(objEUbicaion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarUbicacion(EUbicacion objEUbicacion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarUbicacion(objEUbicacion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarUbicacion(int ubicc_icod_ubicacion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.EliminarUbicacion(ubicc_icod_ubicacion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Giro
        public List<EGiro> ListarGiro()
        {
            List<EGiro> lista = null;
            try
            {
                lista = objVentasData.ListarGiro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EGiro> ListarGiroFiltro()
        {
            List<EGiro> lista = null;
            try
            {
                lista = objVentasData.ListarGiro();
                EGiro entidad = new EGiro();
                entidad.giroc_icod_giro = 0;
                entidad.giroc_vnombre_giro = "Todos";
                lista.Insert(0, entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarGiro(EGiro objGiro)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.InsertarGiro(objGiro);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarGiro(EGiro objGiro)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarGiro(objGiro);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarResumenDiarioResponse(int id, EResumenResponse response)
        {
            objVentasData.modificarResumenDiarioResponse(id, response);
        }

        public EConductor ConductorGet(int remic_icod_remision)
        {
            return objVentasData.ConductorGet(remic_icod_remision);
        }

        public void EliminarGiro(int girc_icod_giro)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.EliminarGiro(girc_icod_giro);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Factura
        public List<EFacturaCab> getFacturaCab(int id_factura)
        {
            List<EFacturaCab> lst = new List<EFacturaCab>();
            try
            {
                lst = objVentasData.getFacturaCab(id_factura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }
        public List<EFacturaCab> listarFacturaCab(int intEjericio)
        {
            List<EFacturaCab> lista = new List<EFacturaCab>();
            try
            {
                lista = objVentasData.listarFacturaCab(intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarFactura(EFacturaCab objEFactura, List<EFacturaDet> lstFacDet, List<ECuotasFactura> lstCuotas)
        {

            int intIcodE = 0;
            try
            {
                AdministracionSistemaData objAdminSistemaData = new AdministracionSistemaData();
                OperacionesData objOperacionesData = new OperacionesData();
                ContabilidadData objContabilidadData = new ContabilidadData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region Descripcion Producto
                    int i = 1;
                    string vdescripcion_1erProducto = "";
                    foreach (var _bbe in lstFacDet)
                    {
                        if (i == 1)
                        {
                            vdescripcion_1erProducto = _bbe.strDesProducto;
                        }
                    }
                    #endregion
                    #region Doc. Por Cobrar de la Factura
                    //INSERTAR LA FACTURA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEFactura.favc_sfecha_factura.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                    objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                    objDXC.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                    objDXC.cliec_icod_cliente = objEFactura.favc_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEFactura.cliec_vnombre_cliente;
                    objDXC.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEFactura.favc_sfecha_vencim_factura;
                    objDXC.tablc_iid_tipo_moneda = objEFactura.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEFactura.favc_sfecha_factura);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEFactura.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) > 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) == 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = objEFactura.favc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEFactura.favc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = objEFactura.tablc_iid_situacion;
                    objDXC.doxcc_vobservaciones = vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;
                    objDXC.doxcc_sfecha_entrega = null;
                    objDXC.doxcc_bind_impresion_nogerencia = false;
                    objDXC.doxc_bind_situacion_legal = false;
                    objDXC.doxc_bind_cierre_cuenta_corriente = false;
                    objDXC.intUsuario = objEFactura.intUsuario;
                    objDXC.strPc = objEFactura.strPc;
                    objDXC.doxcc_tipo_comprobante_referencia = 0;
                    objDXC.doxcc_num_serie_referencia = "";
                    objDXC.doxcc_num_comprobante_referencia = "";
                    objDXC.doxcc_sfecha_emision_referencia = null;
                    //objDXC.docxc_icod_documento = objEFactura.favc_icod_factura;
                    objDXC.anio = objEFactura.favc_sfecha_factura.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "V";
                    objDXC.puvec_icod_punto_venta = objEFactura.favc_icod_pvt;
                    //INGRESAMOS EL DOC POR COBRAR PARA CAPTRA EL ID DEL XC
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    objEFactura.doxcc_icod_correlativo = new CuentasPorCobrarData().insertarDxc(objDXC, Lista);
                    objDXC.doxcc_icod_correlativo = objEFactura.doxcc_icod_correlativo;

                    #endregion
                    #region Factura Cab. Insertar
                    //OBTENER EL CORRELATIVO RECIENTE, PARA ASEGURARSE QUE NO HAYA DUPLICADOS
                    //var lst = objAdminSistemaData.getCorrelativoTipoDocumento(Parametros.intTipoDocFacturaVenta,);

                    //objEFactura.favc_vnumero_factura = String.Format("{0:000}{1:0000000}", lst[0].tdocc_nro_serie, (Convert.ToInt32(lst[0].tdocc_nro_correlativo) + 1));
                    objEFactura.favc_icod_factura = objVentasData.insertarFactura(objEFactura);
                    objEFactura.doc_icod_documento = objEFactura.favc_icod_factura;
                    intIcodE = objVentasData.insertarfacturaVentaElectronica(objEFactura);
                    if (objEFactura.favc_icod_factura == 0)
                    {
                        throw new Exception("El número de la Factura ya fue utilizado, intente con un número de Factura superior");
                    }
                    //ACTUALIZAR EL CORRELATIVO DE LA FACTURA
                    //objAdminSistemaData.updateCorrelativoTipoDocumentoPVT(2,Convert.ToInt32(objEFactura.favc_vnumero_factura.Substring(4, 8)), 1);
                    if (objEFactura.actualizarSerie)
                        new BAdministracionSistema().updateCorrelativoTipoDocumentoSeries(objEFactura.favc_vnumero_factura.Substring(0, 4), Convert.ToInt32(objEFactura.favc_vnumero_factura.Substring(4, 8)));
                    //SI LA FACTURA REFERENCIA A UNA OT, LA OT DEBE DE CAMBIAR DE SITUACION A FACTURADA


                    #endregion

                    #region Factura Det. Insertar
                    //if (objEFactura.remic_icod_remision == 0)
                    //{

                    foreach (var obj in lstFacDet)
                    {
                        object[] Tallas = new object[10];
                        object[] Cantidades = new object[10];
                        object[] idkardex = new object[10];
                        object[] idProducto = new object[10];
                        Tallas[0] = obj.favd_talla1;
                        Tallas[1] = obj.favd_talla2;
                        Tallas[2] = obj.favd_talla3;
                        Tallas[3] = obj.favd_talla4;
                        Tallas[4] = obj.favd_talla5;
                        Tallas[5] = obj.favd_talla6;
                        Tallas[6] = obj.favd_talla7;
                        Tallas[7] = obj.favd_talla8;
                        Tallas[8] = obj.favd_talla9;
                        Tallas[9] = obj.favd_talla10;
                        Cantidades[0] = obj.favd_cant_talla1;
                        Cantidades[1] = obj.favd_cant_talla2;
                        Cantidades[2] = obj.favd_cant_talla3;
                        Cantidades[3] = obj.favd_cant_talla4;
                        Cantidades[4] = obj.favd_cant_talla5;
                        Cantidades[5] = obj.favd_cant_talla6;
                        Cantidades[6] = obj.favd_cant_talla7;
                        Cantidades[7] = obj.favd_cant_talla8;
                        Cantidades[8] = obj.favd_cant_talla9;
                        Cantidades[9] = obj.favd_cant_talla10;
                        idProducto[0] = obj.favd_icod_producto1;
                        idProducto[1] = obj.favd_icod_producto2;
                        idProducto[2] = obj.favd_icod_producto3;
                        idProducto[3] = obj.favd_icod_producto4;
                        idProducto[4] = obj.favd_icod_producto5;
                        idProducto[5] = obj.favd_icod_producto6;
                        idProducto[6] = obj.favd_icod_producto7;
                        idProducto[7] = obj.favd_icod_producto8;
                        idProducto[8] = obj.favd_icod_producto9;
                        idProducto[9] = obj.favd_icod_producto10;
                        if (objEFactura.remic_icod_remision != 0)
                        {
                            idkardex[0] = obj.favd_iid_kardex1;
                            idkardex[1] = obj.favd_iid_kardex2;
                            idkardex[2] = obj.favd_iid_kardex3;
                            idkardex[3] = obj.favd_iid_kardex4;
                            idkardex[4] = obj.favd_iid_kardex5;
                            idkardex[5] = obj.favd_iid_kardex6;
                            idkardex[6] = obj.favd_iid_kardex7;
                            idkardex[7] = obj.favd_iid_kardex8;
                            idkardex[8] = obj.favd_iid_kardex9;
                            idkardex[9] = obj.favd_iid_kardex10;
                        }

                        if (objEFactura.remic_icod_remision == 0)
                        {

                            for (int l = 0; l <= 9; l++)
                            {
                                if (Convert.ToInt32(Cantidades[l]) != 0)
                                {
                                    EProdKardex objk = new EProdKardex();
                                    objk.kardc_ianio = Parametros.intEjercicio;
                                    objk.kardx_sfecha = Convert.ToDateTime(objEFactura.favc_sfecha_factura);
                                    objk.iid_almacen = Convert.ToInt32(objEFactura.favc_iid_almacen);
                                    objk.iid_producto = Convert.ToInt32(idProducto[l]);
                                    objk.puvec_icod_punto_venta = objEFactura.puvec_icod_punto_venta;
                                    objk.Cantidad = Convert.ToDecimal(Cantidades[l]);
                                    //objk.NroNota = Convert.ToInt32(objEFactura.favc_vnumero_factura);
                                    objk.iid_tipo_doc = 26;
                                    objk.NroDocumento = objEFactura.favc_vnumero_factura;
                                    objk.movimiento = 0;
                                    objk.Motivo = 101;
                                    objk.Beneficiario = objEFactura.cliec_vnombre_cliente;
                                    objk.Observaciones = "VENTA DE MERCADERIA";
                                    objk.intUsuario = objEFactura.intUsuario;
                                    objk.Item = Convert.ToInt32(obj.favd_iitem_factura);
                                    objk.stocc_codigo_producto = obj.favd_vcodigo_extremo_prod;
                                    objk.operacion = 1;
                                    objk.kardc_iid_correlativo = new BCentral().InsertarKardexpvt(objk);
                                    idkardex[l] = objk.kardc_iid_correlativo;
                                    idProducto[i] = objk.iid_producto;
                                    EProdStockProducto objStock = new EProdStockProducto()
                                    {
                                        stocc_ianio = Parametros.intEjercicio,
                                        stocc_iid_almacen = objk.iid_almacen,
                                        stocc_iid_producto = objk.iid_producto,
                                        stocc_nstock_prod = objk.Cantidad,
                                        intTipoMovimiento = objk.movimiento,
                                    };
                                    new BCentral().actualizarStockPvt(objStock);
                                }

                            }
                            obj.favd_iid_kardex1 = Convert.ToInt64(idkardex[0]);
                            obj.favd_iid_kardex2 = Convert.ToInt64(idkardex[1]);
                            obj.favd_iid_kardex3 = Convert.ToInt64(idkardex[2]);
                            obj.favd_iid_kardex4 = Convert.ToInt64(idkardex[3]);
                            obj.favd_iid_kardex5 = Convert.ToInt64(idkardex[4]);
                            obj.favd_iid_kardex6 = Convert.ToInt64(idkardex[5]);
                            obj.favd_iid_kardex7 = Convert.ToInt64(idkardex[6]);
                            obj.favd_iid_kardex8 = Convert.ToInt64(idkardex[7]);
                            obj.favd_iid_kardex9 = Convert.ToInt64(idkardex[8]);
                            obj.favd_iid_kardex10 = Convert.ToInt64(idkardex[9]);

                        }

                        obj.IdCabecera = intIcodE;
                        insertarfacturaVentaElectronicaDetalle(obj);
                        obj.favc_icod_factura = objEFactura.favc_icod_factura;
                        insertarFacturaDetalle(obj);
                    };
                    //}
                    //else
                    //{
                    //     lstFacDet.ForEach(x =>
                    //    {
                    //           x.IdCabecera = intIcodE;
                    //           insertarfacturaVentaElectronicaDetalle(x);
                    //           x.kardc_icod_correlativo = null;
                    //           x.favc_icod_factura = objEFactura.favc_icod_factura;
                    //           insertarFacturaDetalle(x);
                    //    });

                    //}
                    #endregion

                    if (objEFactura.remic_icod_remision != 0)
                    {
                        objVentasData.modificarGuiaRemision_Situ_Tipo_Doc(Convert.ToInt32(objEFactura.remic_icod_remision),
                            //26, //TIPO DE DOCUMENTO
                            //objEFactura.favc_icod_factura, //ICOD_DOCUEMTNO
                            219, //FACTURADO
                            objEFactura.intUsuario,
                            objEFactura.strPc);

                    }

                    lstCuotas.ForEach(x =>
                    {
                        x.favc_icod_factura = objEFactura.favc_icod_factura;
                        x.doxcc_icod_correlativo = (int)objEFactura.doxcc_icod_correlativo;
                        x.fcc_icod = objVentasData.CuotasFacturaIngresar(x);
                    });
                    tx.Complete();
                }
                return objEFactura.favc_icod_factura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EUbigeo UbigeoGet(string ubigeo)
        {
            return objVentasData.UbigeoGet(ubigeo);
        }

        public void ResumenDocumentosCabMensajeRespuestaModificar(ESunatResumenDocumentosCab item, string mensajeRespuesta, DateTime? fechaEnvio)
        {
            objVentasData.ResumenDocumentosCabMensajeRespuestaModificar(item, mensajeRespuesta, fechaEnvio);
        }

        public int insertarFacturaServio(EFacturaCab objEFactura, List<EFacturaMPDet> lstFacDet, List<ECuotasFactura> lstCuotas)
        {

            int intIcodE = 0;
            try
            {
                AdministracionSistemaData objAdminSistemaData = new AdministracionSistemaData();
                OperacionesData objOperacionesData = new OperacionesData();
                ContabilidadData objContabilidadData = new ContabilidadData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region Descripcion Producto
                    int i = 1;
                    string vdescripcion_1erProducto = "";
                    foreach (var _bbe in lstFacDet)
                    {
                        if (i == 1)
                        {
                            vdescripcion_1erProducto = _bbe.strDesProducto;
                        }
                    }
                    #endregion
                    #region Doc. Por Cobrar de la Factura
                    //INSERTAR LA FACTURA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEFactura.favc_sfecha_factura.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                    objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                    objDXC.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                    objDXC.cliec_icod_cliente = objEFactura.favc_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEFactura.cliec_vnombre_cliente;
                    objDXC.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEFactura.favc_sfecha_vencim_factura;
                    objDXC.tablc_iid_tipo_moneda = objEFactura.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEFactura.favc_sfecha_factura);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEFactura.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) > 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) == 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = objEFactura.favc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEFactura.favc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = objEFactura.tablc_iid_situacion;
                    objDXC.doxcc_vobservaciones = vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;
                    objDXC.doxcc_sfecha_entrega = null;
                    objDXC.doxcc_bind_impresion_nogerencia = false;
                    objDXC.doxc_bind_situacion_legal = false;
                    objDXC.doxc_bind_cierre_cuenta_corriente = false;
                    objDXC.intUsuario = objEFactura.intUsuario;
                    objDXC.strPc = objEFactura.strPc;
                    objDXC.doxcc_tipo_comprobante_referencia = 0;
                    objDXC.doxcc_num_serie_referencia = "";
                    objDXC.doxcc_num_comprobante_referencia = "";
                    objDXC.doxcc_sfecha_emision_referencia = null;
                    //objDXC.docxc_icod_documento = objEFactura.favc_icod_factura;
                    objDXC.anio = objEFactura.favc_sfecha_factura.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "V";
                    objDXC.puvec_icod_punto_venta = objEFactura.favc_icod_pvt;
                    //INGRESAMOS EL DOC POR COBRAR PARA CAPTRA EL ID DEL XC
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    objEFactura.doxcc_icod_correlativo = new CuentasPorCobrarData().insertarDxc(objDXC, Lista);
                    objDXC.doxcc_icod_correlativo = objEFactura.doxcc_icod_correlativo;

                    #endregion
                    #region Factura Cab. Insertar
                    objEFactura.favc_icod_factura = objVentasData.insertarFactura(objEFactura);
                    objEFactura.doc_icod_documento = objEFactura.favc_icod_factura;
                    intIcodE = objVentasData.insertarfacturaVentaElectronica(objEFactura);
                    if (objEFactura.favc_icod_factura == 0)
                    {
                        throw new Exception("El número de la Factura ya fue utilizado, intente con un número de Factura superior");
                    }
                    //ACTUALIZAR EL CORRELATIVO DE LA FACTURA
                    //objAdminSistemaData.updateCorrelativoTipoDocumentoPVTMP(2, Convert.ToInt32(objEFactura.favc_vnumero_factura.Substring(4, 8)), Convert.ToInt32(objEFactura.favc_tipo_factura));
                    if (objEFactura.actualizarSerie)
                        new BAdministracionSistema().updateCorrelativoTipoDocumentoSeries(objEFactura.favc_vnumero_factura.Substring(0, 4), Convert.ToInt32(objEFactura.favc_vnumero_factura.Substring(4, 8)));
                    #endregion

                    #region Factura Det. Insertar
                    if (objEFactura.remic_icod_remision == 0 && objEFactura.favc_tipo_factura == 2)
                    {
                        lstFacDet.ForEach(x =>
                        {

                            #region Salida de Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = objEFactura.favc_sfecha_factura.Year;
                            obKardex.kardc_fecha_movimiento = objEFactura.favc_sfecha_factura;
                            obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.favd_ncantidad);
                            obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                            obKardex.kardc_numero_doc = objEFactura.favc_vnumero_factura;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.kardc_iid_motivo = Parametros.intMotivoKrdVentasOut;
                            obKardex.kardc_beneficiario = objEFactura.cliec_vnombre_cliente;
                            obKardex.kardc_observaciones = "";
                            obKardex.intUsuario = objEFactura.intUsuario;
                            obKardex.strPc = objEFactura.strPc;
                            x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                            #endregion
                            //verificar stock del producto
                            decimal Stock_Producto = new AlmacenData().listarStockProductoPorAlmacen(Parametros.intEjercicio, Convert.ToInt32(x.almac_icod_almacen), x.prdc_icod_producto);
                            if (Stock_Producto < Convert.ToDecimal(x.favd_ncantidad))
                            {
                                throw new Exception("Stock insuficiente para el producto: " + x.strDesProducto + "\nStock actual para este producto es: " + Stock_Producto.ToString());
                            }

                            #region Actualizando Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = objEFactura.favc_sfecha_factura.Year;
                            stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stck.stocc_stock_producto = x.favd_ncantidad;
                            stck.intTipoMovimiento = 0;
                            objAlmacenData.actualizarStock(stck);
                            #endregion

                            x.IdCabecera = intIcodE;
                            insertarfacturaVentaElectronicaMPDetalle(x);
                            x.favc_icod_factura = objEFactura.favc_icod_factura;
                            insertarFacturaMPDetalle(x);
                        });
                    }
                    else
                    {
                        lstFacDet.ForEach(x =>
                        {

                            x.IdCabecera = intIcodE;
                            insertarfacturaVentaElectronicaMPDetalle(x);
                            x.favc_icod_factura = objEFactura.favc_icod_factura;
                            insertarFacturaMPDetalle(x);
                        });

                    }
                    #endregion

                    if (objEFactura.remic_icod_remision != 0)
                    {
                        objVentasData.modificarGuiaRemision_Situ_Tipo_Doc(Convert.ToInt32(objEFactura.remic_icod_remision),
                            //26, //TIPO DE DOCUMENTO
                            //objEFactura.favc_icod_factura, //ICOD_DOCUEMTNO
                            219, //FACTURADO
                            objEFactura.intUsuario,
                            objEFactura.strPc);

                    }
                    lstCuotas.ForEach(x =>
                    {
                        x.favc_icod_factura = objEFactura.favc_icod_factura;
                        x.doxcc_icod_correlativo = (int)objEFactura.doxcc_icod_correlativo;
                        x.fcc_icod = objVentasData.CuotasFacturaIngresar(x);
                    });
                    tx.Complete();
                }
                return objEFactura.favc_icod_factura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarFacturaDesdePlanilla(EFacturaCab objEFactura, List<EFacturaDet> lstFacDet, ref EPlanillaCobranzaCab oBePlnCab,
            EPlanillaCobranzaDet oBePlnDet, List<EPagoDocVenta> lstPagos)
        {
            //ING. EDGAR ALFARO
            //FECHA: 08/01/2014
            TesoreriaData objTesoreriaData = new TesoreriaData();
            AdministracionSistemaData objAdminSistemaData = new AdministracionSistemaData();
            OperacionesData objOperacionesData = new OperacionesData();
            ContabilidadData objContabilidadData = new ContabilidadData();
            CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //VARIABLES DEL METODO
                    string strNroPlanCab = oBePlnCab.plnc_vnumero_planilla;
                    //

                    #region Factura

                    #region Descripcion Producto
                    int i = 1;
                    string vdescripcion_1erProducto = "";
                    foreach (var _bbe in lstFacDet)
                    {
                        if (i == 1)
                        {
                            vdescripcion_1erProducto = _bbe.strDesProducto;
                        }
                    }
                    #endregion
                    #region Doc. Por Cobrar de la Factura
                    //INSERTAR LA FACTURA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEFactura.favc_sfecha_factura.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                    objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                    objDXC.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                    objDXC.cliec_icod_cliente = objEFactura.favc_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEFactura.cliec_vnombre_cliente;
                    objDXC.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;

                    objDXC.doxcc_sfecha_vencimiento_doc = objEFactura.favc_sfecha_vencim_factura;
                    objDXC.tablc_iid_tipo_moneda = objEFactura.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEFactura.favc_sfecha_factura);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEFactura.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) > 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) == 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = objEFactura.favc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEFactura.favc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = objEFactura.tablc_iid_situacion;
                    objDXC.doxcc_vobservaciones = vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;
                    objDXC.doxcc_sfecha_entrega = null;
                    objDXC.doxcc_bind_impresion_nogerencia = false;

                    objDXC.intUsuario = objEFactura.intUsuario;
                    objDXC.strPc = objEFactura.strPc;
                    objDXC.doxcc_tipo_comprobante_referencia = 0;
                    objDXC.doxcc_num_serie_referencia = "";
                    objDXC.doxcc_num_comprobante_referencia = "";
                    objDXC.doxcc_sfecha_emision_referencia = null;
                    objDXC.anio = objEFactura.favc_sfecha_factura.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "P";
                    //INGRESAMOS EL DOC POR COBRAR PARA CAPTRA EL ID DEL XC
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    objEFactura.doxcc_icod_correlativo = objCuentaCobrarData.insertarDxc(objDXC, Lista);
                    #endregion
                    #region Factura Cab. Insertar

                    //var lst = objAdminSistemaData.getCorrelativoTipoDocumento(Parametros.intTipoDocFacturaVenta);
                    objEFactura.favc_icod_factura = objVentasData.insertarFactura(objEFactura);
                    objAdminSistemaData.updateCorrelativoTipoDocumentoPVT(1, Convert.ToInt32(objEFactura.favc_vnumero_factura.Substring(4, 8)), 2);

                    #endregion
                    #region Factura Det. Insertar
                    if (objEFactura.remic_icod_remision == 0)
                    {
                        lstFacDet.ForEach(x =>
                       {

                           #region Salida de Kardex
                           EKardex obKardex = new EKardex();
                           obKardex.kardc_ianio = objEFactura.favc_sfecha_factura.Year;
                           obKardex.kardc_fecha_movimiento = objEFactura.favc_sfecha_factura;
                           obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                           obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                           obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.favd_ncantidad);
                           obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                           obKardex.kardc_numero_doc = objEFactura.favc_vnumero_factura;
                           obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                           obKardex.kardc_iid_motivo = Parametros.intMotivoKrdVentasOut;
                           obKardex.kardc_beneficiario = objEFactura.cliec_vnombre_cliente;
                           obKardex.kardc_observaciones = "";
                           obKardex.intUsuario = objEFactura.intUsuario;
                           obKardex.strPc = objEFactura.strPc;
                           x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                           #endregion
                           #region Actualizando Stock
                           EStock stck = new EStock();
                           stck.stocc_ianio = objEFactura.favc_sfecha_factura.Year;
                           stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                           stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                           stck.stocc_stock_producto = x.favd_ncantidad;
                           stck.intTipoMovimiento = 0;
                           objAlmacenData.actualizarStock(stck);
                           #endregion

                           x.favc_icod_factura = objEFactura.favc_icod_factura;
                           insertarFacturaDetalle(x);
                       });

                    }
                    else
                    {
                        lstFacDet.ForEach(x =>
                        {
                            x.kardc_icod_correlativo = null;
                            x.favc_icod_factura = objEFactura.favc_icod_factura;
                            insertarFacturaDetalle(x);
                        });
                    }

                    #endregion
                    #region Pagos de la factura
                    lstPagos.ForEach(x =>
                {
                    //SE ASIGNA VALORES FALTANTES, QUE RECIEN SE HAN ADQUIRIDO EN EL METODO
                    x.pgoc_iid_tipo_doc_docventa = Parametros.intTipoDocFacturaVenta;
                    x.pgoc_icod_documento = objEFactura.favc_icod_factura;
                    x.pgoc_dxc_icod_doc = objDXC.doxcc_icod_correlativo;
                    x.pgoc_sfecha_pago = oBePlnDet.plnd_sfecha_doc;
                    x.pgoc_vnumero_planilla = strNroPlanCab;
                    x.pgoc_icod_cliente = objEFactura.favc_icod_cliente;

                    //TOMAR EN CUENTA LOS TIPOS DE PAGO
                    if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                    {
                        #region Pago Efectivo
                        //EL PAGO ES EN EFECTIVO, SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                        x.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        #endregion
                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                    {
                        #region Tarjeta de Credito
                        //EL PAGO ES EN TARJETA DE CRED., SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                        x.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        //EL PAGO ES EN TARJETA DE CRED., DEBE SER INGRESADO EN BANCOS
                        var lstTipoTarjeta = listarTipoTarjeta();
                        ETipoTarjeta oBeTipoTarjeta = lstTipoTarjeta.Where(d => d.tcrc_icod_tipo_tarjeta_cred == Convert.ToInt32(x.pgoc_icod_tipo_tarjeta)).ToList()[0];
                        //CABECERA DEL MOV. DE BANCOS
                        ELibroBancos oBeBcoMovCab = new ELibroBancos();
                        oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                        oBeBcoMovCab.iid_mes = x.pgoc_sfecha_pago.Month;
                        oBeBcoMovCab.dfecha_movimiento = x.pgoc_sfecha_pago;
                        oBeBcoMovCab.icod_enti_financiera_cuenta = oBeTipoTarjeta.bcod_icod_banco_cuenta;
                        oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                        oBeBcoMovCab.vglosa = "PAGO CON TARJETA";
                        oBeBcoMovCab.vdescripcion_beneficiario = oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred;
                        oBeBcoMovCab.iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                        oBeBcoMovCab.cliec_icod_cliente = objEFactura.favc_icod_cliente;
                        oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(x.pgoc_sfecha_pago);
                        oBeBcoMovCab.nmonto_movimiento = x.pgoc_nmonto;
                        oBeBcoMovCab.nmonto_saldo_banco = 0;
                        oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                        oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                        oBeBcoMovCab.vnro_documento = String.Format("{0:000}-{1:00}", Convert.ToInt32(strNroPlanCab), oBeTipoTarjeta.tcrc_iid_tipo_tarjeta_cred);
                        oBeBcoMovCab.cflag_conciliacion = false;
                        oBeBcoMovCab.iusuario_crea = x.intUsuario;
                        oBeBcoMovCab.vpc_crea = x.strPc;
                        oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                        oBeBcoMovCab.mobac_flag_estado = true;
                        oBeBcoMovCab.TipoDocumento = "PVD";
                        oBeBcoMovCab.mobac_origen_regitro = "PLN";
                        x.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                        oBeBcoMovCab.icod_correlativo = Convert.ToInt32(x.pgoc_icod_entidad_finan_mov);
                        //DETALLE DEL MOV. BANCOS
                        ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                        oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                        oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                        oBeBcoMovDet.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;
                        oBeBcoMovDet.mobdc_iid_anio = Parametros.intEjercicio;
                        oBeBcoMovDet.mobdc_iid_mes_periodo = objEFactura.favc_sfecha_factura.Month;
                        oBeBcoMovDet.vnumero_doc = objEFactura.favc_vnumero_factura;
                        oBeBcoMovDet.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                        oBeBcoMovDet.mobdc_icod_cliente = objEFactura.favc_icod_cliente;
                        oBeBcoMovDet.mto_mov_soles = x.pgoc_nmonto;
                        oBeBcoMovDet.mto_mov_dolar = Math.Round(x.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                        oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                        oBeBcoMovDet.iusuario_crea = objEFactura.intUsuario;
                        oBeBcoMovDet.vpc_crea = objEFactura.strPc;
                        oBeBcoMovDet.mobdc_flag_estado = true;
                        oBeBcoMovDet.doxcc_icod_correlativo = objDXC.doxcc_icod_correlativo;
                        oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                        oBeBcoMovDet.docxc_icod_pago = x.pgoc_dxc_icod_pago;

                        //SE INSERTAR EL DETALLE
                        objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                        #endregion
                    }

                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                    {
                        #region Ingreso del pago Dxp Pago
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                        x.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        #endregion
                        #region Insertar Pago Nota de Credito
                        //EL PAGO CON LA NOTA DE CREDITO
                        ENotaCreditoPago oBe = new ENotaCreditoPago();
                        oBe.doxcc_icod_correlativo_pago = x.pgoc_dxc_icod_doc; //el documento a pagar
                        oBe.doxcc_icod_correlativo_nota_credito = Convert.ToInt64(x.pgoc_icod_nota_credito); //correlativo de la nota de crédito   
                        oBe.tablc_iid_tipo_moneda = Convert.ToInt32(x.pgoc_icod_tipo_moneda); //debe grabarse con el tipo de moneda del documento nota de crédito
                        oBe.ncpac_nmonto_pago = Convert.ToDecimal(x.pgoc_nmonto); //monto que se va a pagar guardado con el tipo de moneda del documento nota de crédito
                        oBe.ncpac_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePlnDet.plnd_sfecha_doc); //tipo de cambio de la fecha seleccionada
                        oBe.ncpac_vdescripcion = String.Format("N° PLN VENTA: {0}", strNroPlanCab);
                        oBe.ncpac_sfecha_pago = Convert.ToDateTime(oBePlnDet.plnd_sfecha_doc); //fecha del pago
                        oBe.ncpac_iorigen = "P"; //Nota de credito
                        oBe.ncpac_flag_estado = true;
                        oBe.ncpac_iusuario_crea = x.intUsuario;
                        oBe.ncpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                        oBe.ncpac_iusuario_modifica = x.intUsuario;
                        oBe.ncpac_vpc_modifica = WindowsIdentity.GetCurrent().Name;

                        oBe.pdxcc_icod_correlativo = x.pgoc_dxc_icod_pago;
                        x.pgoc_dxc_icod_canje_doc = new CuentasPorCobrarData().insertarNCPago(oBe);

                        #endregion
                        #region Actualizacion de Estados
                        TesoreriaData objTesoreriaData1 = new TesoreriaData();
                        objTesoreriaData1.ActualizarMontoPagadoSaldoNotaCreditoCliente(oBe.doxcc_icod_correlativo_nota_credito, 0);
                        objTesoreriaData1.ActualizarMontoDXCPagadoSaldo(oBe.doxcc_icod_correlativo_pago, 0);
                        #endregion

                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                    {
                        #region Pago Cheque
                        //EL PAGO ES CHEQUE, SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                        x.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);

                        ELibroBancos oBeBcoMovCab = new ELibroBancos();
                        oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                        oBeBcoMovCab.iid_mes = x.pgoc_sfecha_pago.Month;
                        oBeBcoMovCab.dfecha_movimiento = x.pgoc_sfecha_pago;
                        oBeBcoMovCab.icod_enti_financiera_cuenta = Convert.ToInt32(x.bcod_icod_banco_cuenta);
                        oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                        oBeBcoMovCab.vglosa = "PAGO CHEQUE";
                        oBeBcoMovCab.vdescripcion_beneficiario = objEFactura.clic_vcod_cliente;
                        oBeBcoMovCab.iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                        oBeBcoMovCab.cliec_icod_cliente = objEFactura.favc_icod_cliente;
                        oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(x.pgoc_sfecha_pago);
                        oBeBcoMovCab.nmonto_movimiento = x.pgoc_nmonto;
                        // oBeBcoMovCab.nmonto_saldo_banco = 0;
                        oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                        oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                        oBeBcoMovCab.vnro_documento = String.Format("{0:000}", Convert.ToInt32(strNroPlanCab));
                        oBeBcoMovCab.cflag_conciliacion = false;
                        oBeBcoMovCab.iusuario_crea = x.intUsuario;
                        oBeBcoMovCab.vpc_crea = x.strPc;
                        oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                        oBeBcoMovCab.mobac_flag_estado = true;
                        oBeBcoMovCab.TipoDocumento = "CHQ";
                        oBeBcoMovCab.mobac_origen_regitro = "PLN";
                        x.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                        oBeBcoMovCab.icod_correlativo = Convert.ToInt32(x.pgoc_icod_entidad_finan_mov);
                        //DETALLE DEL MOV. BANCOS
                        ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                        oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                        oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                        oBeBcoMovDet.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;
                        oBeBcoMovDet.mobdc_iid_anio = Parametros.intEjercicio;
                        oBeBcoMovDet.mobdc_iid_mes_periodo = objEFactura.favc_sfecha_factura.Month;
                        oBeBcoMovDet.vnumero_doc = objEFactura.favc_vnumero_factura;
                        oBeBcoMovDet.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                        oBeBcoMovDet.mobdc_icod_cliente = objEFactura.favc_icod_cliente;
                        oBeBcoMovDet.mto_mov_soles = x.pgoc_nmonto;
                        oBeBcoMovDet.mto_mov_dolar = Math.Round(x.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                        // oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                        oBeBcoMovDet.iusuario_crea = objEFactura.intUsuario;
                        oBeBcoMovDet.vpc_crea = objEFactura.strPc;
                        oBeBcoMovDet.mobdc_flag_estado = true;
                        oBeBcoMovDet.doxcc_icod_correlativo = objDXC.doxcc_icod_correlativo;
                        oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                        oBeBcoMovDet.docxc_icod_pago = x.pgoc_dxc_icod_pago;

                        //SE INSERTAR EL DETALLE
                        objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                        #endregion
                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                    {
                        #region Transferencia Bancaria
                        //EL PAGO ES DE TRANSFERENCOIA BANCARIA., SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                        x.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        ////EL PAGO ES EN TARJETA DE CRED., DEBE SER INGRESADO EN BANCOS
                        ////var lstTipoTarjeta = listarTipoTarjeta();
                        ////ETipoTarjeta oBeTipoTarjeta = lstTipoTarjeta.Where(d => d.tcrc_icod_tipo_tarjeta_cred == Convert.ToInt32(x.pgoc_icod_tipo_tarjeta)).ToList()[0];
                        //CABECERA DEL MOV. DE BANCOS 
                        ELibroBancos oBeBcoMovCab = new ELibroBancos();
                        oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                        oBeBcoMovCab.iid_mes = x.pgoc_sfecha_pago.Month;
                        oBeBcoMovCab.dfecha_movimiento = x.pgoc_sfecha_pago;
                        oBeBcoMovCab.icod_enti_financiera_cuenta = Convert.ToInt32(x.bcod_icod_banco_cuenta);
                        oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                        oBeBcoMovCab.vglosa = "TRANSFERENCIA BANCARIA";
                        oBeBcoMovCab.vdescripcion_beneficiario = objEFactura.clic_vcod_cliente;
                        oBeBcoMovCab.iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                        oBeBcoMovCab.cliec_icod_cliente = objEFactura.favc_icod_cliente;
                        oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(x.pgoc_sfecha_pago);
                        oBeBcoMovCab.nmonto_movimiento = x.pgoc_nmonto;
                        //     oBeBcoMovCab.nmonto_saldo_banco = 0;
                        oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                        oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                        oBeBcoMovCab.vnro_documento = String.Format("{0:000}", Convert.ToInt32(strNroPlanCab));
                        oBeBcoMovCab.cflag_conciliacion = false;
                        oBeBcoMovCab.iusuario_crea = x.intUsuario;
                        oBeBcoMovCab.vpc_crea = x.strPc;
                        oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                        oBeBcoMovCab.mobac_flag_estado = true;
                        oBeBcoMovCab.TipoDocumento = "TRANS";
                        oBeBcoMovCab.mobac_origen_regitro = "PLN";
                        x.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                        oBeBcoMovCab.icod_correlativo = Convert.ToInt32(x.pgoc_icod_entidad_finan_mov);
                        //DETALLE DEL MOV. BANCOS
                        ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                        oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                        oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                        oBeBcoMovDet.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;
                        oBeBcoMovDet.mobdc_iid_anio = Parametros.intEjercicio;
                        oBeBcoMovDet.mobdc_iid_mes_periodo = objEFactura.favc_sfecha_factura.Month;
                        oBeBcoMovDet.vnumero_doc = objEFactura.favc_vnumero_factura;
                        oBeBcoMovDet.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                        oBeBcoMovDet.mobdc_icod_cliente = objEFactura.favc_icod_cliente;
                        oBeBcoMovDet.mto_mov_soles = x.pgoc_nmonto;
                        oBeBcoMovDet.mto_mov_dolar = Math.Round(x.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                        // oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                        oBeBcoMovDet.iusuario_crea = objEFactura.intUsuario;
                        oBeBcoMovDet.vpc_crea = objEFactura.strPc;
                        oBeBcoMovDet.mobdc_flag_estado = true;
                        oBeBcoMovDet.doxcc_icod_correlativo = objDXC.doxcc_icod_correlativo;
                        oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                        oBeBcoMovDet.docxc_icod_pago = x.pgoc_dxc_icod_pago;

                        //SE INSERTAR EL DETALLE
                        objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                        #endregion
                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                    { }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                    {
                        #region Pago DxC

                        EDocXCobrarPago oBePagoDXC1 = new EDocXCobrarPago();
                        oBePagoDXC1.doxcc_icod_correlativo = objEFactura.doxcc_icod_correlativo;
                        oBePagoDXC1.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                        oBePagoDXC1.pdxcc_vnumero_doc = x.strNroAnticipo;
                        oBePagoDXC1.pdxcc_sfecha_cobro = objEFactura.favc_sfecha_factura;
                        oBePagoDXC1.tablc_iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                        oBePagoDXC1.pdxcc_nmonto_cobro = x.pgoc_nmonto;
                        oBePagoDXC1.pdxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(objEFactura.favc_sfecha_factura);
                        oBePagoDXC1.pdxcc_vobservacion = String.Format("N° PLN VENTA: {0}", strNroPlanCab);
                        oBePagoDXC1.cliec_icod_cliente = x.pgoc_icod_cliente;
                        oBePagoDXC1.pdxcc_vorigen = "P";
                        oBePagoDXC1.intUsuario = x.intUsuario;
                        oBePagoDXC1.strPc = x.strPc;
                        oBePagoDXC1.pdxcc_flag_estado = true;
                        x.pgoc_dxc_icod_canje_doc = new CuentasPorCobrarData().InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC1);
                        new TesoreriaData().ActualizarMontoDXCPagadoSaldo(objEFactura.doxcc_icod_correlativo, 0);

                        #endregion
                        #region Pago Adelanto Pago
                        EAdelantoPago oBe = new EAdelantoPago();
                        oBe.doxcc_icod_correlativo_pago = objEFactura.doxcc_icod_correlativo; //el documento a pagar
                        oBe.doxcc_icod_correlativo_adelanto = Convert.ToInt32(x.pgoc_icod_anticipo); //correlativo del adelanto
                        oBe.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta; //tipo documento(adelanto)
                        oBe.cliec_icod_cliente = Convert.ToInt32(x.pgoc_icod_cliente); //código del cliente
                        oBe.tablc_iid_tipo_moneda = Convert.ToInt32(x.pgoc_icod_tipo_moneda); //debe grabarse con el tipo de moneda del documento adelanto
                        oBe.adpac_nmonto_pago = Convert.ToDecimal(x.pgoc_nmonto); //monto que se va a pagar guardado con el tipo de moneda del documento adelanto
                        oBe.adpac_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(objEFactura.favc_sfecha_factura); //tipo de cambio de la fecha seleccionada
                        oBe.adpac_vdescripcion = String.Format("N° PLN VENTA: {0} - {1} ", x.pgoc_vnumero_planilla, x.pgoc_descripcion.ToUpper());
                        oBe.adpac_sfecha_pago = Convert.ToDateTime(objEFactura.favc_sfecha_factura); //fecha del pago
                        oBe.adpac_iorigen = "P"; //Adelanto
                        oBe.adpac_iusuario_crea = x.intUsuario;
                        oBe.adpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                        oBe.adpac_iusuario_modifica = x.intUsuario;
                        oBe.adpac_vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                        oBe.adpac_flag_estado = true;
                        oBe.SaldoDXCAdelanto = Convert.ToDecimal(x.pgoc_nmonto);
                        oBe.doxcc_nmonto_pagado = 0;
                        x.pgoc_dxc_icod_pago = new CuentasPorCobrarData().insertarAdelantoPago(oBe);
                        new TesoreriaData().ActualizarMontoPagadoAdelantoCliente(Convert.ToInt32(x.pgoc_icod_anticipo), 0);
                        #endregion
                    }

                    //FINALMENTE SE INSERTA EL PAGO DEL DOCUMENTO DE VENTA(EN ESTE CASO LA FACTURA)
                    objVentasData.insertarPago(x);
                });
                    #endregion
                    //UNA VEZ QUE SE HA TERMINADO CON LOS PAGOS, SE ACTUALIZA LA SITUACION EL DOC. POR COBRAR.
                    objTesoreriaData.ActualizarMontoDXCPagadoSaldo(objEFactura.doxcc_icod_correlativo, 0);
                    #endregion
                    #region Planilla Cab
                    //INSERTAR LA CAB. DE LA PLANILLA (SE INSERTAR SI ES EL PRIMER REGISTRO, SINO SE MODIFICA)
                    if (oBePlnCab.plnc_icod_planilla == 0)
                    {
                        //INSERTAR LA CAB. DE LA PLANILLA (SE REALIZA SOLO CONE L PRIMER REGISTRO DE UN MOVIMIENTO)                     
                        oBePlnCab.plnc_icod_planilla = objVentasData.insertarPlanillaCobranzaCab(oBePlnCab);
                        if (oBePlnCab.plnc_icod_planilla == 0)
                        {

                            //primero obtener el numero mayor de la planilla y sumarlo mas 1.
                            //con el nuevo numero, ejecutar nuevamente el metodo de insertar
                            //por ultimo 
                            throw new Exception("El número de la planilla es:");
                        }
                    }
                    else
                    {
                        //NO SE REALIZA NINGUNA ACCION, PORQUE LA CAB. PLANILLA SE ACTUALIZARA AUTOMATICAMENTE AL TERMINAR LA INSERCION DE LA FACTURA
                    }
                    //INSERTAR EL DET. DE LA PLANILLA
                    #endregion
                    #region Planilla Det
                    oBePlnDet.plnc_icod_planilla = oBePlnCab.plnc_icod_planilla;
                    oBePlnDet.plnd_nmonto_pagado = (lstPagos.Count > 0) ? lstPagos.Where(x => x.pgoc_tipo_pago != 6).Sum(x => x.pgoc_nmonto) : 0;
                    oBePlnDet.tablc_iid_tipo_mov = Parametros.intPlnFacturacion;
                    oBePlnDet.plnd_icod_documento = objEFactura.favc_icod_factura;
                    //INGRESAR EL REGISTRO DETALLE DE LA PLANILLA (Ej. Facturacion, pago o anticipo - En este caso es Facturacion)
                    oBePlnDet.plnd_icod_detalle = objVentasData.insertarPlanillaCobranzaDetalle(oBePlnDet);
                    #endregion
                    tx.Complete();
                    return oBePlnDet.plnd_icod_detalle;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarFacturaDesdePlanilla(EFacturaCab objEFactura, List<EFacturaDet> lstFacDet, List<EFacturaDet> lstFacDelete,
            EPlanillaCobranzaCab oBePlnCab, EPlanillaCobranzaDet oBePlnDet, List<EPagoDocVenta> lstPagos, List<EPagoDocVenta> lstDeletePagos)
        {
            try
            {
                //ING. EDGAR ALFARO
                //FECHA: 08/01/2014
                TesoreriaData objTesoreriaData = new TesoreriaData();
                AdministracionSistemaData objAdminSistemaData = new AdministracionSistemaData();
                OperacionesData objOperacionesData = new OperacionesData();
                ContabilidadData objContabilidadData = new ContabilidadData();
                CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //VARIABLES DEL METODO
                    string strNroPlanCab = oBePlnCab.plnc_vnumero_planilla;
                    //
                    #region Factura
                    //MODIFICAR LA CAB. DE LA FACTURA
                    objVentasData.modificarFactura(objEFactura);
                    //
                    #region Factura Det.
                    lstFacDelete.ForEach(x =>
                    {
                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                        obKardexDel.intUsuario = objEFactura.intUsuario;
                        obKardexDel.strPc = objEFactura.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        #endregion
                        objVentasData.eliminarFacturaDetalle(x);
                    });
                    int i = 1;
                    string vdescripcion_1erProducto = "";
                    lstFacDet.ForEach(x =>
                    {

                        if (x.intTipoOperacion == 1)
                        {

                            #region Salida de Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = objEFactura.favc_sfecha_factura.Year;
                            obKardex.kardc_fecha_movimiento = objEFactura.favc_sfecha_factura;
                            obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.favd_ncantidad);
                            obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                            obKardex.kardc_numero_doc = objEFactura.favc_vnumero_factura;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.kardc_iid_motivo = Parametros.intMotivoKrdVentasOut;
                            obKardex.kardc_beneficiario = "";
                            obKardex.kardc_observaciones = "";
                            obKardex.intUsuario = objEFactura.intUsuario;
                            obKardex.strPc = objEFactura.strPc;
                            x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                            #endregion
                            #region Actualizando Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = objEFactura.favc_sfecha_factura.Year;
                            stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stck.stocc_stock_producto = x.favd_ncantidad;
                            stck.intTipoMovimiento = 0;
                            objAlmacenData.actualizarStock(stck);
                            #endregion

                            //SE INSERTA EL ITEM DE LA FACTURA
                            x.favc_icod_factura = objEFactura.favc_icod_factura;
                            objVentasData.insertarFacturaDetalle(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {

                            #region Eliminar Kardex Previo
                            EKardex obKardexDel = new EKardex();
                            obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                            obKardexDel.intUsuario = objEFactura.intUsuario;
                            obKardexDel.strPc = objEFactura.strPc;
                            objAlmacenData.eliminarKardex(obKardexDel);
                            #endregion
                            #region Salida de Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = objEFactura.favc_sfecha_factura.Year;
                            obKardex.kardc_fecha_movimiento = objEFactura.favc_sfecha_factura;
                            obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.favd_ncantidad);
                            obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                            obKardex.kardc_numero_doc = objEFactura.favc_vnumero_factura;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.kardc_iid_motivo = Parametros.intMotivoKrdVentasOut;
                            obKardex.kardc_beneficiario = "";
                            obKardex.kardc_observaciones = "";
                            obKardex.intUsuario = objEFactura.intUsuario;
                            obKardex.strPc = objEFactura.strPc;
                            x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                            #endregion
                            #region Actualizando Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = objEFactura.favc_sfecha_factura.Year;
                            stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stck.stocc_stock_producto = x.favd_ncantidad;
                            stck.intTipoMovimiento = 0;
                            objAlmacenData.actualizarStock(stck);
                            #endregion

                            objVentasData.modificarFacturaDetalle(x);

                        }
                        if (i == 1)
                        {
                            vdescripcion_1erProducto = x.strDesProducto;
                        }
                    });
                    #endregion
                    #region Doc. Por Cobrar de la Factura
                    //INSERTAR LA FACTURA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = objEFactura.doxcc_icod_correlativo;
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEFactura.favc_sfecha_factura.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                    objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                    objDXC.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                    objDXC.cliec_icod_cliente = objEFactura.favc_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEFactura.cliec_vnombre_cliente;
                    objDXC.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEFactura.favc_sfecha_vencim_factura;
                    objDXC.tablc_iid_tipo_moneda = objEFactura.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEFactura.favc_sfecha_factura);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEFactura.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) > 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) == 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = objEFactura.favc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEFactura.favc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = objEFactura.tablc_iid_situacion;
                    objDXC.doxcc_vobservaciones = vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;
                    objDXC.doxcc_sfecha_entrega = null;
                    objDXC.doxcc_bind_impresion_nogerencia = false;
                    objDXC.doxc_bind_situacion_legal = false;
                    objDXC.doxc_bind_cierre_cuenta_corriente = false;
                    objDXC.intUsuario = objEFactura.intUsuario;
                    objDXC.strPc = objEFactura.strPc;
                    objDXC.doxcc_tipo_comprobante_referencia = 0;
                    objDXC.doxcc_num_serie_referencia = "";
                    objDXC.doxcc_num_comprobante_referencia = "";
                    objDXC.doxcc_sfecha_emision_referencia = null;
                    //objDXC.docxc_icod_documento = objEFactura.favc_icod_factura;
                    objDXC.anio = objEFactura.favc_sfecha_factura.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "P";
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    new CuentasPorCobrarData().modificarDxc(objDXC, Lista, Lista);
                    #endregion

                    #region Eliminar Pagos
                    //PRIMERO SE DEBE ELIMINAR, TODOS LOS PAGOS PARA REINGRESAR LOS PAGOS
                    var lstPagosAux = new BVentas().listarPago(Convert.ToInt32(oBePlnDet.plnd_icod_tipo_doc), Convert.ToInt32(oBePlnDet.plnd_icod_documento));
                    lstPagosAux.ForEach(x =>
                    {
                        if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            #region Pago Efectivo
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            #region Tarjeta Credito
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL MOVIMIENTO DEL BANCO
                            objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            #region Nota de Credito
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);

                            ENotaCreditoPago _beNCP = new ENotaCreditoPago();
                            _beNCP.ncpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            _beNCP.ncpac_vpc_modifica = objEFactura.strPc;
                            _beNCP.ncpac_iusuario_modifica = objEFactura.intUsuario;
                            objCuentaCobrarData.eliminarNCPago(_beNCP);
                            objTesoreriaData.ActualizarMontoPagadoSaldoNotaCreditoCliente(Convert.ToInt64(x.pgoc_icod_nota_credito), 0);
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                        {
                            #region Tarjeta Credito
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL MOVIMIENTO DEL BANCO
                            objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                        {
                            #region Tarjeta Credito
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL MOVIMIENTO DEL BANCO
                            objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                            #endregion

                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {
                            #region Adelanto Cliente
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(objEFactura.doxcc_icod_correlativo), 0);
                            //SE ELIMINA EL PAGO DEL ANTICIPO
                            EAdelantoPago oBePagoDXCAnt = new EAdelantoPago();
                            oBePagoDXCAnt.adpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXCAnt.doxcc_icod_correlativo_adelanto = Convert.ToInt64(x.pgoc_icod_anticipo);
                            oBePagoDXCAnt.intUsuario = objEFactura.intUsuario;
                            oBePagoDXCAnt.strPc = objEFactura.strPc;
                            new CuentasPorCobrarData().eliminarAdelantoPago(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(Convert.ToInt64(x.pgoc_icod_anticipo), 0);
                            #endregion
                        }
                        //FINALMENTE SE ELIMINA EL PAGO DEL DOC. DE VENTA (EN ESTE CASO DE LA FACTURA)
                        objVentasData.eliminarPago(x);
                    });
                    #endregion
                    #region Ingresar Nuevo Pago
                    lstPagos.ForEach(x =>
                    {
                        //SE ASIGNA VALORES FALTANTES, QUE RECIEN SE HAN ADQUIRIDO EN EL METODO
                        x.pgoc_iid_tipo_doc_docventa = Parametros.intTipoDocFacturaVenta;
                        x.pgoc_icod_documento = objEFactura.favc_icod_factura;
                        x.pgoc_dxc_icod_doc = objDXC.doxcc_icod_correlativo;
                        x.pgoc_sfecha_pago = oBePlnDet.plnd_sfecha_doc;
                        x.pgoc_vnumero_planilla = strNroPlanCab;
                        x.pgoc_icod_cliente = objEFactura.favc_icod_cliente;
                        //TOMAR EN CUENTA LOS TIPOS DE PAGO
                        if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            #region Pago Efectivo
                            //EL PAGO ES EN EFECTIVO, SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            #region Tarjeta de Credito
                            //EL PAGO ES EN TARJETA DE CRED., SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            //EL PAGO ES EN TARJETA DE CRED., DEBE SER INGRESADO EN BANCOS
                            var lstTipoTarjeta = listarTipoTarjeta();
                            ETipoTarjeta oBeTipoTarjeta = lstTipoTarjeta.Where(d => d.tcrc_icod_tipo_tarjeta_cred == Convert.ToInt32(x.pgoc_icod_tipo_tarjeta)).ToList()[0];
                            //CABECERA DEL MOV. DE BANCOS
                            ELibroBancos oBeBcoMovCab = new ELibroBancos();
                            oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                            oBeBcoMovCab.iid_mes = x.pgoc_sfecha_pago.Month;
                            oBeBcoMovCab.dfecha_movimiento = x.pgoc_sfecha_pago;
                            oBeBcoMovCab.icod_enti_financiera_cuenta = oBeTipoTarjeta.bcod_icod_banco_cuenta;
                            oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            oBeBcoMovCab.vglosa = "PAGO CON TARJETA";
                            oBeBcoMovCab.vdescripcion_beneficiario = oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred;
                            oBeBcoMovCab.iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBeBcoMovCab.cliec_icod_cliente = objEFactura.favc_icod_cliente;
                            oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(x.pgoc_sfecha_pago);
                            oBeBcoMovCab.nmonto_movimiento = x.pgoc_nmonto;
                            oBeBcoMovCab.nmonto_saldo_banco = 0;
                            oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                            oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                            oBeBcoMovCab.vnro_documento = String.Format("{0:000}-{1:00}", Convert.ToInt32(strNroPlanCab), oBeTipoTarjeta.tcrc_iid_tipo_tarjeta_cred);
                            oBeBcoMovCab.cflag_conciliacion = false;
                            oBeBcoMovCab.iusuario_crea = x.intUsuario;
                            oBeBcoMovCab.vpc_crea = x.strPc;
                            oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                            oBeBcoMovCab.mobac_flag_estado = true;
                            oBeBcoMovCab.TipoDocumento = "PVD";
                            oBeBcoMovCab.mobac_origen_regitro = "PLN";
                            x.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                            oBeBcoMovCab.icod_correlativo = Convert.ToInt32(x.pgoc_icod_entidad_finan_mov);
                            //DETALLE DEL MOV. BANCOS
                            ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                            oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                            oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                            oBeBcoMovDet.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;
                            oBeBcoMovDet.mobdc_iid_anio = Parametros.intEjercicio;
                            oBeBcoMovDet.mobdc_iid_mes_periodo = objEFactura.favc_sfecha_factura.Month;
                            oBeBcoMovDet.vnumero_doc = objEFactura.favc_vnumero_factura;
                            oBeBcoMovDet.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                            oBeBcoMovDet.mobdc_icod_cliente = objEFactura.favc_icod_cliente;
                            oBeBcoMovDet.mto_mov_soles = x.pgoc_nmonto;
                            oBeBcoMovDet.mto_mov_dolar = Math.Round(x.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                            oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                            oBeBcoMovDet.iusuario_crea = objEFactura.intUsuario;
                            oBeBcoMovDet.vpc_crea = objEFactura.strPc;
                            oBeBcoMovDet.mobdc_flag_estado = true;
                            oBeBcoMovDet.doxcc_icod_correlativo = objDXC.doxcc_icod_correlativo;
                            oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                            oBeBcoMovDet.docxc_icod_pago = x.pgoc_dxc_icod_pago;

                            //SE INSERTAR EL DETALLE
                            objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            #region Ingreso del pago Dxp Pago
                            //EL PAGO SE INGRESA COMO PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            #endregion                 
                            #region Insertar Pago Nota de Credito
                            //EL PAGO CON LA NOTA DE CREDITO
                            ENotaCreditoPago oBe = new ENotaCreditoPago();
                            oBe.doxcc_icod_correlativo_pago = x.pgoc_dxc_icod_doc; //el documento a pagar
                            oBe.doxcc_icod_correlativo_nota_credito = Convert.ToInt64(x.pgoc_icod_nota_credito); //correlativo de la nota de crédito   
                            oBe.tablc_iid_tipo_moneda = Convert.ToInt32(x.pgoc_icod_tipo_moneda); //debe grabarse con el tipo de moneda del documento nota de crédito
                            oBe.ncpac_nmonto_pago = Convert.ToDecimal(x.pgoc_nmonto); //monto que se va a pagar guardado con el tipo de moneda del documento nota de crédito
                            oBe.ncpac_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePlnDet.plnd_sfecha_doc); //tipo de cambio de la fecha seleccionada
                            oBe.ncpac_vdescripcion = String.Format("N° PLN VENTA: {0}", strNroPlanCab);
                            oBe.ncpac_sfecha_pago = Convert.ToDateTime(oBePlnDet.plnd_sfecha_doc); //fecha del pago
                            oBe.ncpac_iorigen = "P"; //Nota de credito
                            oBe.ncpac_flag_estado = true;
                            oBe.ncpac_iusuario_crea = x.intUsuario;
                            oBe.ncpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                            oBe.ncpac_iusuario_modifica = x.intUsuario;
                            oBe.ncpac_vpc_modifica = WindowsIdentity.GetCurrent().Name;

                            oBe.pdxcc_icod_correlativo = x.pgoc_dxc_icod_pago;
                            x.pgoc_dxc_icod_canje_doc = new CuentasPorCobrarData().insertarNCPago(oBe);

                            #endregion
                            #region Actualizacion de Estados
                            TesoreriaData objTesoreriaData1 = new TesoreriaData();
                            objTesoreriaData1.ActualizarMontoPagadoSaldoNotaCreditoCliente(oBe.doxcc_icod_correlativo_nota_credito, 0);
                            objTesoreriaData1.ActualizarMontoDXCPagadoSaldo(oBe.doxcc_icod_correlativo_pago, 0);
                            #endregion

                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                        {
                            #region Pago Cheque
                            //EL PAGO ES CHEQUE, SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);

                            ELibroBancos oBeBcoMovCab = new ELibroBancos();
                            oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                            oBeBcoMovCab.iid_mes = x.pgoc_sfecha_pago.Month;
                            oBeBcoMovCab.dfecha_movimiento = x.pgoc_sfecha_pago;
                            oBeBcoMovCab.icod_enti_financiera_cuenta = Convert.ToInt32(x.bcod_icod_banco_cuenta);
                            oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            oBeBcoMovCab.vglosa = "PAGO CHEQUE";
                            oBeBcoMovCab.vdescripcion_beneficiario = objEFactura.clic_vcod_cliente;
                            oBeBcoMovCab.iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBeBcoMovCab.cliec_icod_cliente = objEFactura.favc_icod_cliente;
                            oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(x.pgoc_sfecha_pago);
                            oBeBcoMovCab.nmonto_movimiento = x.pgoc_nmonto;
                            // oBeBcoMovCab.nmonto_saldo_banco = 0;
                            oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                            oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                            oBeBcoMovCab.vnro_documento = String.Format("{0:000}", Convert.ToInt32(strNroPlanCab));
                            oBeBcoMovCab.cflag_conciliacion = false;
                            oBeBcoMovCab.iusuario_crea = x.intUsuario;
                            oBeBcoMovCab.vpc_crea = x.strPc;
                            oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                            oBeBcoMovCab.mobac_flag_estado = true;
                            oBeBcoMovCab.TipoDocumento = "CHQ";
                            oBeBcoMovCab.mobac_origen_regitro = "PLN";
                            x.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                            oBeBcoMovCab.icod_correlativo = Convert.ToInt32(x.pgoc_icod_entidad_finan_mov);
                            //DETALLE DEL MOV. BANCOS
                            ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                            oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                            oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                            oBeBcoMovDet.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;
                            oBeBcoMovDet.mobdc_iid_anio = Parametros.intEjercicio;
                            oBeBcoMovDet.mobdc_iid_mes_periodo = objEFactura.favc_sfecha_factura.Month;
                            oBeBcoMovDet.vnumero_doc = objEFactura.favc_vnumero_factura;
                            oBeBcoMovDet.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                            oBeBcoMovDet.mobdc_icod_cliente = objEFactura.favc_icod_cliente;
                            oBeBcoMovDet.mto_mov_soles = x.pgoc_nmonto;
                            oBeBcoMovDet.mto_mov_dolar = Math.Round(x.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                            // oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                            oBeBcoMovDet.iusuario_crea = objEFactura.intUsuario;
                            oBeBcoMovDet.vpc_crea = objEFactura.strPc;
                            oBeBcoMovDet.mobdc_flag_estado = true;
                            oBeBcoMovDet.doxcc_icod_correlativo = objDXC.doxcc_icod_correlativo;
                            oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                            oBeBcoMovDet.docxc_icod_pago = x.pgoc_dxc_icod_pago;

                            //SE INSERTAR EL DETALLE
                            objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                            #endregion

                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                        {
                            #region Transferencia Bancaria
                            //EL PAGO ES DE TRANSFERENCOIA BANCARIA., SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            ////EL PAGO ES EN TARJETA DE CRED., DEBE SER INGRESADO EN BANCOS
                            ////var lstTipoTarjeta = listarTipoTarjeta();
                            ////ETipoTarjeta oBeTipoTarjeta = lstTipoTarjeta.Where(d => d.tcrc_icod_tipo_tarjeta_cred == Convert.ToInt32(x.pgoc_icod_tipo_tarjeta)).ToList()[0];
                            //CABECERA DEL MOV. DE BANCOS 
                            ELibroBancos oBeBcoMovCab = new ELibroBancos();
                            oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                            oBeBcoMovCab.iid_mes = x.pgoc_sfecha_pago.Month;
                            oBeBcoMovCab.dfecha_movimiento = x.pgoc_sfecha_pago;
                            oBeBcoMovCab.icod_enti_financiera_cuenta = Convert.ToInt32(x.bcod_icod_banco_cuenta);
                            oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            oBeBcoMovCab.vglosa = "TRANSFERENCIA BANCARIA";
                            oBeBcoMovCab.vdescripcion_beneficiario = objEFactura.clic_vcod_cliente;
                            oBeBcoMovCab.iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBeBcoMovCab.cliec_icod_cliente = objEFactura.favc_icod_cliente;
                            oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(x.pgoc_sfecha_pago);
                            oBeBcoMovCab.nmonto_movimiento = x.pgoc_nmonto;
                            //oBeBcoMovCab.nmonto_saldo_banco = 0;
                            oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                            oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                            oBeBcoMovCab.vnro_documento = String.Format("{0:000}", Convert.ToInt32(strNroPlanCab));
                            oBeBcoMovCab.cflag_conciliacion = false;
                            oBeBcoMovCab.iusuario_crea = x.intUsuario;
                            oBeBcoMovCab.vpc_crea = x.strPc;
                            oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                            oBeBcoMovCab.mobac_flag_estado = true;
                            oBeBcoMovCab.TipoDocumento = "TRANS";
                            oBeBcoMovCab.mobac_origen_regitro = "PLN";
                            x.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                            oBeBcoMovCab.icod_correlativo = Convert.ToInt32(x.pgoc_icod_entidad_finan_mov);
                            //DETALLE DEL MOV. BANCOS
                            ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                            oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                            oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                            oBeBcoMovDet.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;
                            oBeBcoMovDet.mobdc_iid_anio = Parametros.intEjercicio;
                            oBeBcoMovDet.mobdc_iid_mes_periodo = objEFactura.favc_sfecha_factura.Month;
                            oBeBcoMovDet.vnumero_doc = objEFactura.favc_vnumero_factura;
                            oBeBcoMovDet.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                            oBeBcoMovDet.mobdc_icod_cliente = objEFactura.favc_icod_cliente;
                            oBeBcoMovDet.mto_mov_soles = x.pgoc_nmonto;
                            oBeBcoMovDet.mto_mov_dolar = Math.Round(x.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                            // oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                            oBeBcoMovDet.iusuario_crea = objEFactura.intUsuario;
                            oBeBcoMovDet.vpc_crea = objEFactura.strPc;
                            oBeBcoMovDet.mobdc_flag_estado = true;
                            oBeBcoMovDet.doxcc_icod_correlativo = objDXC.doxcc_icod_correlativo;
                            oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                            oBeBcoMovDet.docxc_icod_pago = x.pgoc_dxc_icod_pago;

                            //SE INSERTAR EL DETALLE
                            objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {
                            #region Pago DxC

                            EDocXCobrarPago oBePagoDXC1 = new EDocXCobrarPago();
                            oBePagoDXC1.doxcc_icod_correlativo = objEFactura.doxcc_icod_correlativo;
                            oBePagoDXC1.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                            oBePagoDXC1.pdxcc_vnumero_doc = x.strNroAnticipo;
                            oBePagoDXC1.pdxcc_sfecha_cobro = objEFactura.favc_sfecha_factura;
                            oBePagoDXC1.tablc_iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBePagoDXC1.pdxcc_nmonto_cobro = x.pgoc_nmonto;
                            oBePagoDXC1.pdxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(objEFactura.favc_sfecha_factura);
                            oBePagoDXC1.pdxcc_vobservacion = String.Format("N° PLN VENTA: {0}", strNroPlanCab);
                            oBePagoDXC1.cliec_icod_cliente = x.pgoc_icod_cliente;
                            oBePagoDXC1.pdxcc_vorigen = "P";
                            oBePagoDXC1.intUsuario = x.intUsuario;
                            oBePagoDXC1.strPc = x.strPc;
                            oBePagoDXC1.pdxcc_flag_estado = true;
                            x.pgoc_dxc_icod_canje_doc = new CuentasPorCobrarData().InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC1);
                            new TesoreriaData().ActualizarMontoDXCPagadoSaldo(objEFactura.doxcc_icod_correlativo, 0);

                            #endregion
                            #region Pago Adelanto Pago
                            EAdelantoPago oBe = new EAdelantoPago();
                            oBe.doxcc_icod_correlativo_pago = objEFactura.doxcc_icod_correlativo; //el documento a pagar
                            oBe.doxcc_icod_correlativo_adelanto = Convert.ToInt32(x.pgoc_icod_anticipo); //correlativo del adelanto
                            oBe.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta; //tipo documento(adelanto)
                            oBe.cliec_icod_cliente = Convert.ToInt32(x.pgoc_icod_cliente); //código del cliente
                            oBe.tablc_iid_tipo_moneda = Convert.ToInt32(x.pgoc_icod_tipo_moneda); //debe grabarse con el tipo de moneda del documento adelanto
                            oBe.adpac_nmonto_pago = Convert.ToDecimal(x.pgoc_nmonto); //monto que se va a pagar guardado con el tipo de moneda del documento adelanto
                            oBe.adpac_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(objEFactura.favc_sfecha_factura); //tipo de cambio de la fecha seleccionada
                            oBe.adpac_vdescripcion = String.Format("N° PLN VENTA: {0} - {1} ", x.pgoc_vnumero_planilla, x.pgoc_descripcion.ToUpper());
                            oBe.adpac_sfecha_pago = Convert.ToDateTime(objEFactura.favc_sfecha_factura); //fecha del pago
                            oBe.adpac_iorigen = "P"; //Adelanto
                            oBe.adpac_iusuario_crea = x.intUsuario;
                            oBe.adpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                            oBe.adpac_iusuario_modifica = x.intUsuario;
                            oBe.adpac_vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                            oBe.adpac_flag_estado = true;
                            oBe.SaldoDXCAdelanto = Convert.ToDecimal(x.pgoc_nmonto);
                            oBe.doxcc_nmonto_pagado = 0;
                            x.pgoc_dxc_icod_pago = new CuentasPorCobrarData().insertarAdelantoPago(oBe);
                            new TesoreriaData().ActualizarMontoPagadoAdelantoCliente(Convert.ToInt32(x.pgoc_icod_anticipo), 0);
                            #endregion
                        }

                        //FINALMENTE SE INSERTA EL PAGO DEL DOCUMENTO DE VENTA(EN ESTE CASO LA FACTURA)
                        objVentasData.insertarPago(x);
                    });
                    #endregion

                    //UNA VEZ QUE SE HA TERMINADO CON LOS PAGOS, SE ACTUALIZA LA SITUACION EL DOC. POR COBRAR.
                    objTesoreriaData.ActualizarMontoDXCPagadoSaldo(objEFactura.doxcc_icod_correlativo, 0);
                    #endregion
                    #region Planilla Det.
                    oBePlnDet.plnd_nmonto_pagado = (lstPagos.Count > 0) ? lstPagos.Where(x => x.pgoc_tipo_pago != 6).Sum(x => x.pgoc_nmonto) : 0;
                    //MODIFICANDO LA PLANILLA DET.
                    objVentasData.modificarPlanillaCobranzaDetalle(oBePlnDet);
                    //objVentasData.modificarPlanillaCobranzaCab(oBePlnCab);

                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarFactura(EFacturaCab objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarFactura(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFactura(EFacturaCab objEFactura, List<EFacturaDet> lstFacDet, List<EFacturaDet> lstFacDelete, List<ECuotasFactura> lstCuotas, List<ECuotasFactura> lstCuotasDelete)
        {
            try
            {
                ContabilidadData objContabilidadData = new ContabilidadData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //MODIFICAR LA FACTURA
                    #region Descripcion Producto
                    int i = 1;
                    string vdescripcion_1erProducto = "";
                    foreach (var _bbe in lstFacDet)
                    {
                        if (i == 1)
                        {
                            vdescripcion_1erProducto = _bbe.strDesProducto;
                        }
                    }
                    #endregion

                    objVentasData.modificarFactura(objEFactura);

                    #region Factura Det.
                    if (objEFactura.remic_icod_remision == 0)
                    {
                        foreach (var obj in lstFacDet)
                        {
                            if (obj.operacion == 1)
                            {
                                //insertar kardex
                                object[] Tallas = new object[10];
                                object[] Cantidades = new object[10];
                                object[] idkardex = new object[10];
                                object[] idProducto = new object[10];
                                Tallas[0] = obj.favd_talla1;
                                Tallas[1] = obj.favd_talla2;
                                Tallas[2] = obj.favd_talla3;
                                Tallas[3] = obj.favd_talla4;
                                Tallas[4] = obj.favd_talla5;
                                Tallas[5] = obj.favd_talla6;
                                Tallas[6] = obj.favd_talla7;
                                Tallas[7] = obj.favd_talla8;
                                Tallas[8] = obj.favd_talla9;
                                Tallas[9] = obj.favd_talla10;
                                Cantidades[0] = obj.favd_cant_talla1;
                                Cantidades[1] = obj.favd_cant_talla2;
                                Cantidades[2] = obj.favd_cant_talla3;
                                Cantidades[3] = obj.favd_cant_talla4;
                                Cantidades[4] = obj.favd_cant_talla5;
                                Cantidades[5] = obj.favd_cant_talla6;
                                Cantidades[6] = obj.favd_cant_talla7;
                                Cantidades[7] = obj.favd_cant_talla8;
                                Cantidades[8] = obj.favd_cant_talla9;
                                Cantidades[9] = obj.favd_cant_talla10;
                                idProducto[0] = obj.favd_icod_producto1;
                                idProducto[1] = obj.favd_icod_producto2;
                                idProducto[2] = obj.favd_icod_producto3;
                                idProducto[3] = obj.favd_icod_producto4;
                                idProducto[4] = obj.favd_icod_producto5;
                                idProducto[5] = obj.favd_icod_producto6;
                                idProducto[6] = obj.favd_icod_producto7;
                                idProducto[7] = obj.favd_icod_producto8;
                                idProducto[8] = obj.favd_icod_producto9;
                                idProducto[9] = obj.favd_icod_producto10;

                                for (int l = 0; l <= 9; l++)
                                {
                                    if (Convert.ToInt32(Cantidades[l]) != 0)
                                    {
                                        EProdKardex objk = new EProdKardex();
                                        objk.kardc_ianio = Parametros.intEjercicio;
                                        objk.kardx_sfecha = Convert.ToDateTime(objEFactura.favc_sfecha_factura);
                                        objk.iid_almacen = Convert.ToInt32(objEFactura.favc_iid_almacen);
                                        objk.iid_producto = Convert.ToInt32(idProducto[l]);
                                        objk.puvec_icod_punto_venta = objEFactura.puvec_icod_punto_venta;
                                        objk.Cantidad = Convert.ToDecimal(Cantidades[l]);
                                        //objk.NroNota = Convert.ToInt32(objEFactura.favc_vnumero_factura);
                                        objk.iid_tipo_doc = 26;
                                        objk.NroDocumento = objEFactura.favc_vnumero_factura;
                                        objk.movimiento = 0;
                                        objk.Motivo = 101;
                                        objk.Beneficiario = objEFactura.cliec_vnombre_cliente;
                                        objk.Observaciones = "VENTA DE MERCADERIA";
                                        objk.intUsuario = objEFactura.intUsuario;
                                        objk.Item = Convert.ToInt32(obj.favd_iitem_factura);
                                        objk.stocc_codigo_producto = obj.favd_vcodigo_extremo_prod;
                                        objk.operacion = 1;
                                        objk.kardc_iid_correlativo = new BCentral().InsertarKardexpvt(objk);
                                        idkardex[l] = objk.kardc_iid_correlativo;
                                        idProducto[i] = objk.iid_producto;

                                        EProdStockProducto objStock = new EProdStockProducto()
                                        {
                                            stocc_ianio = Parametros.intEjercicio,
                                            stocc_iid_almacen = objk.iid_almacen,
                                            stocc_iid_producto = objk.iid_producto,
                                            stocc_nstock_prod = objk.Cantidad,
                                            intTipoMovimiento = objk.movimiento,
                                        };
                                        new BCentral().actualizarStockPvt(objStock);
                                    }

                                }
                                obj.favd_iid_kardex1 = Convert.ToInt64(idkardex[0]);
                                obj.favd_iid_kardex2 = Convert.ToInt64(idkardex[1]);
                                obj.favd_iid_kardex3 = Convert.ToInt64(idkardex[2]);
                                obj.favd_iid_kardex4 = Convert.ToInt64(idkardex[3]);
                                obj.favd_iid_kardex5 = Convert.ToInt64(idkardex[4]);
                                obj.favd_iid_kardex6 = Convert.ToInt64(idkardex[5]);
                                obj.favd_iid_kardex7 = Convert.ToInt64(idkardex[6]);
                                obj.favd_iid_kardex8 = Convert.ToInt64(idkardex[7]);
                                obj.favd_iid_kardex9 = Convert.ToInt64(idkardex[8]);
                                obj.favd_iid_kardex10 = Convert.ToInt64(idkardex[9]);

                                obj.favc_icod_factura = objEFactura.favc_icod_factura;//codigo cabecera
                                insertarFacturaDetalle(obj);

                            }
                            else if (obj.operacion == 2)
                            {
                                List<EProdProducto> oProducto = new List<EProdProducto>();
                                object[] Tallas = new object[10];
                                object[] Cantidades = new object[10];
                                object[] idkardex = new object[10];
                                object[] idProducto = new object[10];

                                Tallas[0] = obj.favd_talla1;
                                Tallas[1] = obj.favd_talla2;
                                Tallas[2] = obj.favd_talla3;
                                Tallas[3] = obj.favd_talla4;
                                Tallas[4] = obj.favd_talla5;
                                Tallas[5] = obj.favd_talla6;
                                Tallas[6] = obj.favd_talla7;
                                Tallas[7] = obj.favd_talla8;
                                Tallas[8] = obj.favd_talla9;
                                Tallas[9] = obj.favd_talla10;
                                Cantidades[0] = obj.favd_cant_talla1;
                                Cantidades[1] = obj.favd_cant_talla2;
                                Cantidades[2] = obj.favd_cant_talla3;
                                Cantidades[3] = obj.favd_cant_talla4;
                                Cantidades[4] = obj.favd_cant_talla5;
                                Cantidades[5] = obj.favd_cant_talla6;
                                Cantidades[6] = obj.favd_cant_talla7;
                                Cantidades[7] = obj.favd_cant_talla8;
                                Cantidades[8] = obj.favd_cant_talla9;
                                Cantidades[9] = obj.favd_cant_talla10;
                                idProducto[0] = obj.favd_icod_producto1;
                                idProducto[1] = obj.favd_icod_producto2;
                                idProducto[2] = obj.favd_icod_producto3;
                                idProducto[3] = obj.favd_icod_producto4;
                                idProducto[4] = obj.favd_icod_producto5;
                                idProducto[5] = obj.favd_icod_producto6;
                                idProducto[6] = obj.favd_icod_producto7;
                                idProducto[7] = obj.favd_icod_producto8;
                                idProducto[8] = obj.favd_icod_producto9;
                                idProducto[9] = obj.favd_icod_producto10;
                                idkardex[0] = obj.favd_iid_kardex1;
                                idkardex[1] = obj.favd_iid_kardex2;
                                idkardex[2] = obj.favd_iid_kardex3;
                                idkardex[3] = obj.favd_iid_kardex4;
                                idkardex[4] = obj.favd_iid_kardex5;
                                idkardex[5] = obj.favd_iid_kardex6;
                                idkardex[6] = obj.favd_iid_kardex7;
                                idkardex[7] = obj.favd_iid_kardex8;
                                idkardex[8] = obj.favd_iid_kardex9;
                                idkardex[9] = obj.favd_iid_kardex10;
                                for (int l = 0; l <= 9; l++)
                                {
                                    if (Convert.ToInt32(idProducto[l]) > 0)
                                    {
                                        EProdKardex objk = new EProdKardex();
                                        long? kardAux = 0;

                                        objk.kardc_ianio = Parametros.intEjercicio;
                                        objk.kardx_sfecha = Convert.ToDateTime(objEFactura.favc_sfecha_factura);
                                        objk.iid_almacen = Convert.ToInt32(objEFactura.favc_iid_almacen);
                                        objk.iid_producto = Convert.ToInt32(idProducto[l]);
                                        objk.puvec_icod_punto_venta = objEFactura.puvec_icod_punto_venta;
                                        objk.Cantidad = Convert.ToDecimal(Cantidades[l]);
                                        //objk.NroNota = Convert.ToInt32(objEFactura.favc_vnumero_factura);
                                        objk.iid_tipo_doc = 26;
                                        objk.NroDocumento = objEFactura.favc_vnumero_factura;
                                        objk.movimiento = 0;
                                        objk.Motivo = 101;
                                        objk.Beneficiario = objEFactura.cliec_vnombre_cliente;
                                        objk.Observaciones = "VENTA DE MERCADERIA";
                                        objk.intUsuario = objEFactura.intUsuario;
                                        objk.Item = Convert.ToInt32(obj.favd_iitem_factura);
                                        objk.stocc_codigo_producto = obj.favd_vcodigo_extremo_prod;
                                        objk.operacion = 1;
                                        objk.kardc_iid_correlativo = Convert.ToInt32(idkardex[l]);
                                        if (objk.kardc_iid_correlativo > 0)
                                        {
                                            if (Convert.ToDecimal(Cantidades[l]) > 0)
                                            {
                                                new BCentral().modificarKardexPvt(objk);
                                            }
                                            else
                                            {
                                                new BCentral().EliminarKardexPvt(objk);
                                                objk.kardc_iid_correlativo = 0;
                                            }
                                        }
                                        else if (Convert.ToDecimal(Cantidades[l]) > 0)
                                        {
                                            kardAux = new BCentral().InsertarKardexpvt(objk);
                                        }

                                        if (objk.kardc_iid_correlativo > 0)
                                        {
                                            idkardex[l] = objk.kardc_iid_correlativo;
                                        }
                                        else
                                        {
                                            idkardex[l] = kardAux;
                                        }

                                        EProdStockProducto objStock = new EProdStockProducto()
                                        {
                                            stocc_ianio = Parametros.intEjercicio,
                                            stocc_iid_almacen = objk.iid_almacen,
                                            stocc_iid_producto = objk.iid_producto,
                                            stocc_nstock_prod = objk.Cantidad,
                                            intTipoMovimiento = objk.movimiento,
                                        };
                                        new BCentral().actualizarStockPvt(objStock);
                                    }
                                }
                                obj.favd_iid_kardex1 = Convert.ToInt64(idkardex[0]);
                                obj.favd_iid_kardex2 = Convert.ToInt64(idkardex[1]);
                                obj.favd_iid_kardex3 = Convert.ToInt64(idkardex[2]);
                                obj.favd_iid_kardex4 = Convert.ToInt64(idkardex[3]);
                                obj.favd_iid_kardex5 = Convert.ToInt64(idkardex[4]);
                                obj.favd_iid_kardex6 = Convert.ToInt64(idkardex[5]);
                                obj.favd_iid_kardex7 = Convert.ToInt64(idkardex[6]);
                                obj.favd_iid_kardex8 = Convert.ToInt64(idkardex[7]);
                                obj.favd_iid_kardex9 = Convert.ToInt64(idkardex[8]);
                                obj.favd_iid_kardex10 = Convert.ToInt64(idkardex[9]);
                                modificarFacturaDetalle(obj);
                            }

                        }

                        foreach (var objelimi in lstFacDelete)
                        {
                            EProdKardex ekar = new EProdKardex();
                            int[] icod_karde = new int[10];
                            long[] talla = new long[10];
                            object[] idProducto = new object[10];

                            List<EProdProducto> oProducto = new List<EProdProducto>();

                            icod_karde[0] = Convert.ToInt32(objelimi.favd_iid_kardex1);
                            icod_karde[1] = Convert.ToInt32(objelimi.favd_iid_kardex2);
                            icod_karde[2] = Convert.ToInt32(objelimi.favd_iid_kardex3);
                            icod_karde[3] = Convert.ToInt32(objelimi.favd_iid_kardex4);
                            icod_karde[4] = Convert.ToInt32(objelimi.favd_iid_kardex5);
                            icod_karde[5] = Convert.ToInt32(objelimi.favd_iid_kardex6);
                            icod_karde[6] = Convert.ToInt32(objelimi.favd_iid_kardex7);
                            icod_karde[7] = Convert.ToInt32(objelimi.favd_iid_kardex8);
                            icod_karde[8] = Convert.ToInt32(objelimi.favd_iid_kardex9);
                            icod_karde[9] = Convert.ToInt32(objelimi.favd_iid_kardex10);
                            talla[0] = Convert.ToInt32(objelimi.favd_talla1);
                            talla[1] = Convert.ToInt32(objelimi.favd_talla2);
                            talla[2] = Convert.ToInt32(objelimi.favd_talla3);
                            talla[3] = Convert.ToInt32(objelimi.favd_talla4);
                            talla[4] = Convert.ToInt32(objelimi.favd_talla5);
                            talla[5] = Convert.ToInt32(objelimi.favd_talla6);
                            talla[6] = Convert.ToInt32(objelimi.favd_talla7);
                            talla[7] = Convert.ToInt32(objelimi.favd_talla8);
                            talla[8] = Convert.ToInt32(objelimi.favd_talla9);
                            talla[9] = Convert.ToInt32(objelimi.favd_talla10);
                            idProducto[0] = objelimi.favd_icod_producto1;
                            idProducto[1] = objelimi.favd_icod_producto2;
                            idProducto[2] = objelimi.favd_icod_producto3;
                            idProducto[3] = objelimi.favd_icod_producto4;
                            idProducto[4] = objelimi.favd_icod_producto5;
                            idProducto[5] = objelimi.favd_icod_producto6;
                            idProducto[6] = objelimi.favd_icod_producto7;
                            idProducto[7] = objelimi.favd_icod_producto8;
                            idProducto[8] = objelimi.favd_icod_producto9;
                            idProducto[9] = objelimi.favd_icod_producto10;
                            for (int j = 0; j <= 9; j++)
                            {
                                //KARDEX
                                ekar.kardc_ianio = Parametros.intEjercicio;
                                ekar.kardx_sfecha = DateTime.Today;
                                ekar.iid_almacen = Convert.ToInt32(objEFactura.almac_icod_almacen);
                                ekar.iid_producto = Convert.ToInt32(objelimi.prdc_icod_producto);
                                ekar.movimiento = 0;
                                ekar.operacion = 3;
                                ekar.kardc_iid_correlativo = icod_karde[j];
                                new BCentral().EliminarKardexPvt(ekar);
                                EProdStockProducto objStock = new EProdStockProducto()
                                {
                                    stocc_ianio = Parametros.intEjercicio,
                                    stocc_iid_almacen = ekar.iid_almacen,
                                    stocc_iid_producto = ekar.iid_producto,
                                    stocc_nstock_prod = 0,

                                };
                                new BCentral().actualizarStockPvt(objStock);
                            }
                            eliminarFacturaDetalle(objelimi);
                        }
                    }
                    else
                    {
                        lstFacDet.ForEach(x =>
                        {
                            if (x.intTipoOperacion == 1)
                            {
                                x.almac_icod_almacen = 0;
                                x.kardc_icod_correlativo = null;
                                x.favc_icod_factura = objEFactura.favc_icod_factura;
                                objVentasData.insertarFacturaDetalle(x);
                            }
                            else if (x.intTipoOperacion == 2)
                            {
                                objVentasData.modificarFacturaDetalle(x);

                            }
                        });

                    }
                    #endregion
                    #region Doc. Por Cobrar de la Factura
                    //INSERTAR LA FACTURA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = objEFactura.doxcc_icod_correlativo;
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEFactura.favc_sfecha_factura.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                    objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                    objDXC.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                    objDXC.cliec_icod_cliente = objEFactura.favc_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEFactura.cliec_vnombre_cliente;
                    objDXC.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEFactura.favc_sfecha_vencim_factura;
                    objDXC.tablc_iid_tipo_moneda = objEFactura.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEFactura.favc_sfecha_factura);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEFactura.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) > 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) == 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = objEFactura.favc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEFactura.favc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = objEFactura.tablc_iid_situacion;
                    objDXC.doxcc_vobservaciones = vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;
                    objDXC.doxcc_sfecha_entrega = null;
                    objDXC.doxcc_bind_impresion_nogerencia = false;

                    objDXC.intUsuario = objEFactura.intUsuario;
                    objDXC.strPc = objEFactura.strPc;
                    objDXC.doxcc_tipo_comprobante_referencia = 0;
                    objDXC.doxcc_num_serie_referencia = "";
                    objDXC.doxcc_num_comprobante_referencia = "";
                    objDXC.doxcc_sfecha_emision_referencia = null;
                    //objDXC.docxc_icod_documento = objEFactura.favc_icod_factura;
                    objDXC.anio = objEFactura.favc_sfecha_factura.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "V";
                    objDXC.puvec_icod_punto_venta = objEFactura.favc_icod_pvt;
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    new CuentasPorCobrarData().modificarDxc(objDXC, Lista, Lista);
                    #endregion
                    List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
                    List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronicaXid(objEFactura.favc_icod_factura, "01").ToList();
                    if (lstCab.Count > 0)
                    {
                        objEFactura.IdCabecera = lstCab[0].IdCabecera;
                        objVentasData.modificarfacturaVentaElectronica(objEFactura);
                        new BVentas().eliminarFacturaVentaElectronicaDetalle(lstCab[0].IdCabecera);
                        foreach (var ob in lstFacDet)
                        {
                            ob.IdCabecera = lstCab[0].IdCabecera;
                            insertarfacturaVentaElectronicaDetalle(ob);
                        }
                    }
                    lstCuotas.ForEach(x =>
                    {
                        x.favc_icod_factura = objEFactura.favc_icod_factura;
                        x.doxcc_icod_correlativo = (int)objEFactura.doxcc_icod_correlativo;
                        if (x.operacion == 2)
                        {
                            objVentasData.CuotasFacturaModificar(x);
                        }
                        if (x.fcc_icod == 0)
                        {
                            x.fcc_icod = objVentasData.CuotasFacturaIngresar(x);
                        }
                    });

                    lstCuotasDelete.ForEach(x =>
                    {
                        objVentasData.CuotasFacturaEliminar(x);

                    });
                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarFacturaServicios(EFacturaCab objEFactura, List<EFacturaMPDet> lstFacDet, List<EFacturaMPDet> lstFacDelete, List<ECuotasFactura> lstCuotas, List<ECuotasFactura> lstCuotasDelete)
        {
            try
            {
                ContabilidadData objContabilidadData = new ContabilidadData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //MODIFICAR LA FACTURA
                    #region Descripcion Producto
                    int i = 1;
                    string vdescripcion_1erProducto = "";
                    foreach (var _bbe in lstFacDet)
                    {
                        if (i == 1)
                        {
                            vdescripcion_1erProducto = _bbe.strDesProducto;
                        }
                    }
                    #endregion

                    objVentasData.modificarFactura(objEFactura);

                    #region Factura Det.
                    if (objEFactura.remic_icod_remision == 0 || objEFactura.favc_tipo_factura == 1)
                    {
                        lstFacDelete.ForEach(x =>
                        {

                            #region Eliminar Kardex
                            EKardex obKardexDel = new EKardex();
                            obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                            obKardexDel.intUsuario = objEFactura.intUsuario;
                            obKardexDel.strPc = objEFactura.strPc;
                            objAlmacenData.eliminarKardex(obKardexDel);
                            #endregion
                            #region Actualizando Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = objEFactura.favc_sfecha_factura.Year;
                            stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stck.stocc_stock_producto = x.favd_ncantidad;
                            stck.intTipoMovimiento = 0;
                            objAlmacenData.actualizarStock(stck);
                            #endregion

                            objVentasData.eliminarFacturaMPDetalle(x);
                        });

                        lstFacDet.ForEach(x =>
                        {

                            if (x.intTipoOperacion == 1)
                            {

                                #region Salida de Kardex
                                EKardex obKardex = new EKardex();
                                obKardex.kardc_ianio = objEFactura.favc_sfecha_factura.Year;
                                obKardex.kardc_fecha_movimiento = objEFactura.favc_sfecha_factura;
                                obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.favd_ncantidad);
                                obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                                obKardex.kardc_numero_doc = objEFactura.favc_vnumero_factura;
                                obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                                obKardex.kardc_iid_motivo = Parametros.intMotivoKrdVentasOut;
                                obKardex.kardc_beneficiario = "";
                                obKardex.kardc_observaciones = "";
                                obKardex.intUsuario = objEFactura.intUsuario;
                                obKardex.strPc = objEFactura.strPc;
                                x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                                #endregion
                                #region Actualizando Stock
                                EStock stck = new EStock();
                                stck.stocc_ianio = objEFactura.favc_sfecha_factura.Year;
                                stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                stck.stocc_stock_producto = x.favd_ncantidad;
                                stck.intTipoMovimiento = 0;
                                objAlmacenData.actualizarStock(stck);
                                #endregion
                                //SE INSERTA EL ITEM DE LA FACTURA
                                x.favc_icod_factura = objEFactura.favc_icod_factura;
                                objVentasData.insertarFacturaMPDetalle(x);
                            }
                            else if (x.intTipoOperacion == 2)
                            {

                                #region Eliminar Kardex Previo
                                EKardex obKardexDel = new EKardex();
                                obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                                obKardexDel.intUsuario = objEFactura.intUsuario;
                                obKardexDel.strPc = objEFactura.strPc;
                                objAlmacenData.eliminarKardex(obKardexDel);
                                #endregion
                                #region Salida de Kardex
                                EKardex obKardex = new EKardex();
                                obKardex.kardc_ianio = objEFactura.favc_sfecha_factura.Year;
                                obKardex.kardc_fecha_movimiento = objEFactura.favc_sfecha_factura;
                                obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.favd_ncantidad);
                                obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                                obKardex.kardc_numero_doc = objEFactura.favc_vnumero_factura;
                                obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                                obKardex.kardc_iid_motivo = Parametros.intMotivoKrdVentasOut;
                                obKardex.kardc_beneficiario = "";
                                obKardex.kardc_observaciones = "";
                                obKardex.intUsuario = objEFactura.intUsuario;
                                obKardex.strPc = objEFactura.strPc;
                                x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                                #endregion
                                #region Actualizando Stock
                                EStock stck = new EStock();
                                stck.stocc_ianio = objEFactura.favc_sfecha_factura.Year;
                                stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                                stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                stck.stocc_stock_producto = x.favd_ncantidad;
                                stck.intTipoMovimiento = 0;
                                objAlmacenData.actualizarStock(stck);
                                #endregion

                                //SE MODIFICA EL ITEM DEL DETALLE DE LA FACTURA
                                objVentasData.modificarFacturaMPDetalle(x);

                            }


                        });
                    }
                    else
                    {
                        lstFacDet.ForEach(x =>
                        {
                            if (x.intTipoOperacion == 1)
                            {
                                x.almac_icod_almacen = 0;
                                x.kardc_icod_correlativo = null;
                                x.favc_icod_factura = objEFactura.favc_icod_factura;
                                objVentasData.insertarFacturaMPDetalle(x);
                            }
                            else if (x.intTipoOperacion == 2)
                            {
                                objVentasData.modificarFacturaMPDetalle(x);

                            }
                        });

                    }
                    #endregion
                    #region Doc. Por Cobrar de la Factura
                    //INSERTAR LA FACTURA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = objEFactura.doxcc_icod_correlativo;
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEFactura.favc_sfecha_factura.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                    objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                    objDXC.doxcc_vnumero_doc = objEFactura.favc_vnumero_factura;
                    objDXC.cliec_icod_cliente = objEFactura.favc_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEFactura.cliec_vnombre_cliente;
                    objDXC.doxcc_sfecha_doc = objEFactura.favc_sfecha_factura;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEFactura.favc_sfecha_vencim_factura;
                    objDXC.tablc_iid_tipo_moneda = objEFactura.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEFactura.favc_sfecha_factura);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEFactura.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) > 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEFactura.favc_npor_imp_igv) == 0) ? objEFactura.favc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = objEFactura.favc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEFactura.favc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEFactura.favc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = objEFactura.tablc_iid_situacion;
                    objDXC.doxcc_vobservaciones = vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;
                    objDXC.doxcc_sfecha_entrega = null;
                    objDXC.doxcc_bind_impresion_nogerencia = false;

                    objDXC.intUsuario = objEFactura.intUsuario;
                    objDXC.strPc = objEFactura.strPc;
                    objDXC.doxcc_tipo_comprobante_referencia = 0;
                    objDXC.doxcc_num_serie_referencia = "";
                    objDXC.doxcc_num_comprobante_referencia = "";
                    objDXC.doxcc_sfecha_emision_referencia = null;
                    //objDXC.docxc_icod_documento = objEFactura.favc_icod_factura;
                    objDXC.anio = objEFactura.favc_sfecha_factura.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "V";
                    objDXC.puvec_icod_punto_venta = objEFactura.favc_icod_pvt;
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    new CuentasPorCobrarData().modificarDxc(objDXC, Lista, Lista);
                    #endregion




                    var fact = new BVentas().obtnerEstadoFacturacion(objEFactura.favc_icod_factura, "01");
                    objEFactura.IdCabecera = fact.IdCabecera;
                    objVentasData.modificarfacturaVentaElectronica(objEFactura);
                    new BVentas().eliminarFacturaVentaElectronicaDetalle(fact.IdCabecera);
                    foreach (var ob in lstFacDet)
                    {
                        ob.IdCabecera = fact.IdCabecera;
                        insertarfacturaVentaElectronicaMPDetalle(ob);
                    }


                    lstCuotas.ForEach(x =>
                    {
                        x.favc_icod_factura = objEFactura.favc_icod_factura;
                        x.doxcc_icod_correlativo = (int)objEFactura.doxcc_icod_correlativo;
                        if (x.operacion == 2)
                        {
                            objVentasData.CuotasFacturaModificar(x);
                        }
                        else
                        {
                            x.fcc_icod = objVentasData.CuotasFacturaIngresar(x);
                        }
                    });

                    lstCuotasDelete.ForEach(x =>
                    {
                        objVentasData.CuotasFacturaEliminar(x);

                    });
                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AnularFacturaVenta(EFacturaCab objEFactura)
        {
            #region Eliminar DXC
            EDocXCobrar objDXC = new EDocXCobrar();
            objDXC.doxcc_icod_correlativo = objEFactura.doxcc_icod_correlativo;
            objDXC.intUsuario = objEFactura.intUsuario;
            objDXC.strPc = objEFactura.strPc;
            new CuentasPorCobrarData().AnularDocumentoXCobrar(objDXC);
            #endregion Eliminar DXC

            List<EFacturaDet> Mlist = new List<EFacturaDet>();
            Mlist = listarFacturaDetalle(objEFactura.favc_icod_factura);
            foreach (var x in Mlist)
            {

                #region Eliminar Kardex
                EKardex obKardexDel = new EKardex();
                obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                obKardexDel.intUsuario = objEFactura.intUsuario;
                obKardexDel.strPc = objEFactura.strPc;
                objAlmacenData.eliminarKardex(obKardexDel);
                #endregion
                #region Actualizando Stock
                EStock stck = new EStock();
                stck.stocc_ianio = objEFactura.favc_sfecha_factura.Year;
                stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                stck.stocc_stock_producto = x.favd_ncantidad;
                stck.intTipoMovimiento = 0;
                objAlmacenData.actualizarStock(stck);
                #endregion

                eliminarFacturaDetalle(x);
            }
            //ELIMAINAR LA RELACION CON LA GUIA DE REMISION
            if (objEFactura.remic_icod_remision != 0)
            {
                objVentasData.modificarGuiaRemision_Situ_Tipo_Doc(Convert.ToInt32(objEFactura.remic_icod_remision),
                    //0, //TIPO DE DOCUMENTO
                    //0, //ICOD_DOCUEMTNO
                    218, //GENERADO
                    objEFactura.intUsuario,
                    objEFactura.strPc);

            }
            //------------------------------

            objVentasData.anularFactura(objEFactura);
        }
        public void anularFactura(EFacturaCab objEFactura)
        {
            try
            {
                CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();
                TesoreriaData objTesoreriaData = new TesoreriaData();

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    var lstPagosAux = new BVentas().listarPago(Parametros.intTipoDocFacturaVenta, objEFactura.favc_icod_factura);
                    lstPagosAux.ForEach(x =>
                    {
                        if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL MOVIMIENTO DEL BANCO
                            objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL PAGO DE LA NOTA DE CREDITO
                            EDocXCobrarPago oBePagoDXCAnt = new EDocXCobrarPago();
                            oBePagoDXCAnt.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXCAnt.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_icod_nota_credito);
                            oBePagoDXCAnt.intUsuario = objEFactura.intUsuario;
                            oBePagoDXCAnt.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_icod_nota_credito), 0);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL PAGO DEL ANTICIPO
                            EAdelantoPago oBePagoDXCAnt = new EAdelantoPago();
                            oBePagoDXCAnt.adpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXCAnt.doxcc_icod_correlativo_adelanto = Convert.ToInt64(x.pgoc_icod_anticipo);
                            oBePagoDXCAnt.intUsuario = objEFactura.intUsuario;
                            oBePagoDXCAnt.strPc = objEFactura.strPc;
                            new CuentasPorCobrarData().eliminarAdelantoPago(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(Convert.ToInt64(x.pgoc_icod_anticipo), 0);
                        }
                        //FINALMENTE SE ELIMINA EL PAGO DEL DOC. DE VENTA (EN ESTE CASO DE LA FACTURA)
                        objVentasData.eliminarPago(x);
                    });

                    List<EFacturaDet> Mlist = new List<EFacturaDet>();
                    Mlist = listarFacturaDetalle(objEFactura.favc_icod_factura);
                    foreach (var x in Mlist)
                    {

                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                        obKardexDel.intUsuario = objEFactura.intUsuario;
                        obKardexDel.strPc = objEFactura.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        #endregion
                        #region Actualizando Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = objEFactura.favc_sfecha_factura.Year;
                        stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        stck.stocc_stock_producto = x.favd_ncantidad;
                        stck.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stck);

                        #endregion
                        objVentasData.eliminarFacturaDetalle(x);
                    }

                    objVentasData.anularFactura(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarFacturaVenta(EFacturaCab objEFactura)
        {
            #region Eliminar DXC
            EDocXCobrar objDXC = new EDocXCobrar();
            objDXC.doxcc_icod_correlativo = objEFactura.doxcc_icod_correlativo;
            objDXC.intUsuario = objEFactura.intUsuario;
            objDXC.strPc = objEFactura.strPc;
            new CuentasPorCobrarData().eliminarDxc(objDXC);
            #endregion Eliminar DXC
            if (objEFactura.favc_tipo_factura == 1)
            {
                List<EFacturaDet> Mlist = new List<EFacturaDet>();
                Mlist = listarFacturaDetalle(objEFactura.favc_icod_factura);
                foreach (var x in Mlist)
                {

                    #region Eliminar Kardex
                    EKardex obKardexDel = new EKardex();
                    obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                    obKardexDel.intUsuario = objEFactura.intUsuario;
                    obKardexDel.strPc = objEFactura.strPc;
                    objAlmacenData.eliminarKardex(obKardexDel);
                    #endregion
                    #region Actualizando Stock
                    EStock stck = new EStock();
                    stck.stocc_ianio = objEFactura.favc_sfecha_factura.Year;
                    stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                    stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                    stck.stocc_stock_producto = x.favd_ncantidad;
                    stck.intTipoMovimiento = 0;
                    objAlmacenData.actualizarStock(stck);
                    #endregion

                    eliminarFacturaDetalle(x);
                }
            }
            else if (objEFactura.favc_tipo_factura == 2)
            {
                List<EFacturaMPDet> MlistMP = new List<EFacturaMPDet>();
                MlistMP = listarFacturaMPDetalle(objEFactura.favc_icod_factura);
                foreach (var x in MlistMP)
                {

                    #region Eliminar Kardex
                    EKardex obKardexDel = new EKardex();
                    obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                    obKardexDel.intUsuario = objEFactura.intUsuario;
                    obKardexDel.strPc = objEFactura.strPc;
                    objAlmacenData.eliminarKardex(obKardexDel);
                    #endregion
                    #region Actualizando Stock
                    EStock stck = new EStock();
                    stck.stocc_ianio = objEFactura.favc_sfecha_factura.Year;
                    stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                    stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                    stck.stocc_stock_producto = x.favd_ncantidad;
                    stck.intTipoMovimiento = 0;
                    objAlmacenData.actualizarStock(stck);
                    #endregion

                    eliminarFacturaMPDetalle(x);
                }
            }
            else
            {
                List<EFacturaMPDet> MlistMP = new List<EFacturaMPDet>();
                MlistMP = listarFacturaMPDetalle(objEFactura.favc_icod_factura);
                foreach (var x in MlistMP)
                {

                    #region Eliminar Kardex
                    EKardex obKardexDel = new EKardex();
                    obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                    obKardexDel.intUsuario = objEFactura.intUsuario;
                    obKardexDel.strPc = objEFactura.strPc;
                    objAlmacenData.eliminarKardex(obKardexDel);
                    #endregion
                    #region Actualizando Stock
                    EStock stck = new EStock();
                    stck.stocc_ianio = objEFactura.favc_sfecha_factura.Year;
                    stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                    stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                    stck.stocc_stock_producto = x.favd_ncantidad;
                    stck.intTipoMovimiento = 0;
                    objAlmacenData.actualizarStock(stck);
                    #endregion

                    eliminarFacturaMPDetalle(x);
                }
            }

            //eliminar la relacion con la Guia de Remision
            if (objEFactura.remic_icod_remision != 0)
            {
                objVentasData.modificarGuiaRemision_Situ_Tipo_Doc(Convert.ToInt32(objEFactura.remic_icod_remision),
                    //0, //TIPO DE DOCUMENTO
                    //0, //ICOD_DOCUEMTNO
                    218, //GENERADO
                    objEFactura.intUsuario,
                    objEFactura.strPc);

            }
            //------------------------------


            objVentasData.eliminarFactura(objEFactura);
            var fact = objVentasData.obtenerEstadofacturacion(objEFactura.doc_icod_documento, "01");

            new BVentas().eliminarFacturaVentaElectronicaDetalle(fact.IdCabecera);
            new BVentas().eliminarFacturaVentaElectronica(fact.IdCabecera);

        }

        public void eliminarFactura(EFacturaCab objEFactura, EPlanillaCobranzaDet _BE_plani)
        {
            try
            {
                CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();
                TesoreriaData objTesoreriaData = new TesoreriaData();




                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    var lstPagosAux = new BVentas().listarPago(Parametros.intTipoDocFacturaVenta, objEFactura.favc_icod_factura);
                    lstPagosAux.ForEach(x =>
                    {
                        if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            #region Efectivo
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            #region Tarjeta
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL MOVIMIENTO DEL BANCO
                            objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            #region Nota de Credito
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);

                            ENotaCreditoPago _beNCP = new ENotaCreditoPago();
                            _beNCP.ncpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            _beNCP.ncpac_vpc_modifica = objEFactura.strPc;
                            _beNCP.ncpac_iusuario_modifica = objEFactura.intUsuario;
                            objCuentaCobrarData.eliminarNCPago(_beNCP);
                            objTesoreriaData.ActualizarMontoPagadoSaldoNotaCreditoCliente(Convert.ToInt64(x.pgoc_icod_nota_credito), 0);
                            #endregion

                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL MOVIMIENTO DEL BANCO
                            objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));

                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL MOVIMIENTO DEL BANCO
                            objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {
                            #region Anticipio
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEFactura.intUsuario;
                            oBePagoDXC.strPc = objEFactura.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL PAGO DEL ANTICIPO
                            EAdelantoPago oBePagoDXCAnt = new EAdelantoPago();
                            oBePagoDXCAnt.adpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXCAnt.doxcc_icod_correlativo_adelanto = Convert.ToInt64(x.pgoc_icod_anticipo);
                            oBePagoDXCAnt.intUsuario = objEFactura.intUsuario;
                            oBePagoDXCAnt.strPc = objEFactura.strPc;
                            new CuentasPorCobrarData().eliminarAdelantoPago(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(Convert.ToInt64(x.pgoc_icod_anticipo), 0);
                            #endregion
                        }
                        //FINALMENTE SE ELIMINA EL PAGO DEL DOC. DE VENTA (EN ESTE CASO DE LA FACTURA)
                        objVentasData.eliminarPago(x);
                    });

                    //objVentasData.eliminarFactura(objEFactura);
                    EDocXCobrar _DXC = new EDocXCobrar();
                    _DXC.doxcc_icod_correlativo = objEFactura.doxcc_icod_correlativo;
                    _DXC.intUsuario = objEFactura.intUsuario;
                    _DXC.strPc = objEFactura.strPc;
                    new CuentasPorCobrarData().EliminarDocumentoXCobrar(_DXC);

                    List<EFacturaDet> Mlist = new List<EFacturaDet>();
                    Mlist = listarFacturaDetalle(objEFactura.favc_icod_factura);
                    foreach (var x in Mlist)
                    {

                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                        obKardexDel.intUsuario = objEFactura.intUsuario;
                        obKardexDel.strPc = objEFactura.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        #endregion
                        #region Actualizando Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = objEFactura.favc_sfecha_factura.Year;
                        stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        stck.stocc_stock_producto = x.favd_ncantidad;
                        stck.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stck);

                        #endregion
                        objVentasData.eliminarFacturaDetalle(x);
                    }
                    //eliminar la relacion con la Guia de Remision
                    //if (objEFactura.remic_icod_remision != 0)
                    //{
                    //    objVentasData.modificarGuiaRemision_Situ_Tipo_Doc(Convert.ToInt32(objEFactura.remic_icod_remision),
                    //        0, //TIPO DE DOCUMENTO
                    //        0, //ICOD_DOCUEMTNO
                    //        218, //GENERADO
                    //        objEFactura.intUsuario,
                    //        objEFactura.strPc);

                    //}
                    //------------------------------

                    objVentasData.eliminarFactura(objEFactura);
                    if (_BE_plani != null)

                        objVentasData.eliminarPlanillaCobranzaDetalle(_BE_plani);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EFacturaDet> listarFacturaDetalle(int intFactura)
        {
            List<EFacturaDet> lista = new List<EFacturaDet>();
            try
            {
                lista = objVentasData.listarFacturaDetalle(intFactura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EFacturaMPDet> listarFacturaMPDetalle(int intFactura)
        {
            List<EFacturaMPDet> lista = new List<EFacturaMPDet>();
            try
            {
                lista = objVentasData.listarFacturaMPDetalle(intFactura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarFacturaDetalle(EFacturaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.insertarFacturaDetalle(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void insertarFacturaMPDetalle(EFacturaMPDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.insertarFacturaMPDetalle(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFacturaDetalle(EFacturaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarFacturaDetalle(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFacturaMPDetalle(EFacturaMPDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarFacturaMPDetalle(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFacturaDetalle(EFacturaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarFacturaDetalle(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFacturaMPDetalle(EFacturaMPDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarFacturaMPDetalle(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarAfectoFactura(int favc_icod_factura, bool favc_bafecto_igv)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarAfectoFactura(favc_icod_factura, favc_bafecto_igv);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region  Factura venta electronica
        public List<EFacturaVentaElectronica> listarfacturaVentaElectronicaFecha(DateTime fechaInicio)
        {
            List<EFacturaVentaElectronica> lista = new List<EFacturaVentaElectronica>();
            try
            {
                lista = objVentasData.listarfacturaVentaElectronicaFecha(fechaInicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EFacturaVentaElectronica> listarfacturaVentaElectronicaXid(int codigo, string tipoDocumento)
        {
            List<EFacturaVentaElectronica> lista = new List<EFacturaVentaElectronica>();
            try
            {
                lista = objVentasData.listarfacturaVentaElectronicaXid(codigo, tipoDocumento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public EFacturaVentaElectronica obtnerEstadoFacturacion(int doc_icod_documento, string tipoDocumento)
        {

            EFacturaVentaElectronica factura = null;
            try
            {
                factura = objVentasData.obtenerEstadofacturacion(doc_icod_documento, tipoDocumento);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return factura;
        }

        public List<EFacturaVentaElectronica> listarfacturaVentaElectronica()
        {
            List<EFacturaVentaElectronica> lista = new List<EFacturaVentaElectronica>();
            try
            {
                lista = objVentasData.listarfacturaVentaElectronica();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarFacturaElectronicaResponse(EFacturaElectronicaResponse obj)
        {
            int intIcod = 0;
            try
            {
                intIcod = objVentasData.insertarFacturaElectronicaResponse(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intIcod;
        }
        public void modificarFacturaElectronicaResponse(int idCabecera)
        {
            try
            {
                objVentasData.modificarFacturaElectronicaResponse(idCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void actualizarFacturaElectronicaResponse(int id, int estadoSunat)
        {
            try
            {
                objVentasData.ActualizarFacturaElectronicaEstado(id, estadoSunat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void actualizarDocumentosBajaResponse(int id, int estadoSunat)
        {
            try
            {
                objVentasData.actualizarDocumentosBajaResponse(id, estadoSunat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFacturaVentaElectronica(int icodCabecera)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarFacturaVentaElectronica(icodCabecera);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void actualizarFacturaElectronicaCorrelaticoTXT(int icodCabecera, int correlativoTXT)
        {
            try
            {
                objVentasData.actualizarFacturaElectronicaCorrelaticoTXT(icodCabecera, correlativoTXT);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void actualizarDocumentoBajaCorrelaticoTXT(int icodCabecera, int correlativoTXT)
        {
            try
            {
                objVentasData.actualizarDocumentoBajaCorrelaticoTXT(icodCabecera, correlativoTXT);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*Resumen Boletas*/

        public List<EFacturaVentaElectronica> listarfacturaVentaElectronicaResumen(DateTime fechaInicio, DateTime fechaEmision)
        {
            List<EFacturaVentaElectronica> lista = new List<EFacturaVentaElectronica>();
            try
            {
                lista = objVentasData.listarfacturaVentaElectronicaResumen(fechaInicio, fechaEmision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<ESunatResumenDocumentosCab> listarSunatResumenDocumentosCab(DateTime fechaInicio)
        {
            List<ESunatResumenDocumentosCab> lista = new List<ESunatResumenDocumentosCab>();
            try
            {
                lista = objVentasData.listarSunatResumenDocumentosCab(fechaInicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarResumenDiarioResponse(EResumenResponse obj)
        {
            int intIcod = 0;
            try
            {
                intIcod = objVentasData.insertarResumenDiariaResponse(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intIcod;
        }
        public void actualizarResumenDocumentosResponse(int id, int estadoSunat)
        {
            try
            {
                objVentasData.actualizarResumenDocumentosResponse(id, estadoSunat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertarResumenDocumentos(ESunatResumenDocumentosCab oBe, List<ESunatResumenDocumentosDet> mlistDetalle)
        {
            int intIcod = 0;
            int intIcodE = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    intIcod = objVentasData.insertarSunatResumenDocumentosCab(oBe);

                    foreach (var ob in mlistDetalle)
                    {
                        ob.IdCabecera = intIcod;
                        insertarSunatResumenDocumentosDet(ob);
                    }


                    tx.Complete();
                }
                return intIcod;
                return intIcodE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarSunatResumenDocumentosDet(ESunatResumenDocumentosDet obj)
        {
            int intIcod = 0;
            try
            {
                intIcod = objVentasData.insertarSunatResumenDocumentosDet(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intIcod;
        }
        public void eliminarResumenDocumentos(ESunatResumenDocumentosCab obe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    List<ESunatResumenDocumentosDet> mlistFac = new List<ESunatResumenDocumentosDet>();
                    mlistFac = listarSunatResumenDocumentosDet(obe.IdCabecera);
                    foreach (var ob in mlistFac)
                    {
                        objVentasData.eliminarSunatResumenDocumentosDetalle(ob.IdCabecera);
                    }
                    objVentasData.eliminarSunatResumenDocumento(obe.IdCabecera);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ESunatDocumentosBaja> listarSunatDocumentosBajaCab(DateTime fechaInicio)
        {
            List<ESunatDocumentosBaja> lista = new List<ESunatDocumentosBaja>();
            try
            {
                lista = objVentasData.listarSunatDocumentosBajaCab(fechaInicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarSunatDocumentosBajaCab(ESunatDocumentosBaja obj)
        {
            int intIcod = 0;
            try
            {
                intIcod = objVentasData.insertarSunatDocumentosBajaCab(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intIcod;
        }



        #endregion
        #region Factura Venta Electronica Detalle
        public List<EFacturaVentaDetalleElectronico> listarfacturaVentaElectronicaDetalle(int facvd_icod_fac_venta)
        {
            List<EFacturaVentaDetalleElectronico> lista = new List<EFacturaVentaDetalleElectronico>();
            try
            {
                lista = objVentasData.listarfacturaVentaElectronicaDetalle(facvd_icod_fac_venta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarfacturaVentaElectronicaDetalle(EFacturaDet oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarfacturaVentaElectronicaDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarNotaCreditoSuministrosVentaElectronicaDetalle(ENotaCreditoSuministrosDet oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarNotaCreditoSuministrosVentaElectronicaDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarfacturaVentaElectronicaMPDetalle(EFacturaMPDet oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarfacturaVentaElectronicaMPDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarboletaVentaElectronicaDetalle(EBoletaDet oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarboletaVentaElectronicaDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarNotaCreditoVentaElectronicaDetalle(ENotaCreditoDet oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarNotaCreditoVentaElectronicaDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarNotaDebitoVentaElectronicaDetalle(ENotaDebitoDet oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarNotaDebitoVentaElectronicaDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFacturaVentaElectronicaDetalle(int icodCabecera)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarFacturaVentaElectronicaDetalle(icodCabecera);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*Resumen Boletas*/
        public List<ESunatResumenDocumentosDet> listarSunatResumenDocumentosDet(int idCabecera)
        {
            List<ESunatResumenDocumentosDet> lista = new List<ESunatResumenDocumentosDet>();
            try
            {
                lista = objVentasData.listarSunatResumenDocumentosDet(idCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        #endregion
        #region Evento 
        public List<EEventoVenta> ListarEventoVenta()
        {
            List<EEventoVenta> lista = null;
            try
            {
                lista = objVentasData.ListarEventoVenta();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public void InsertarEventoVenta(EEventoVenta objGiro)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.InsertarEventoVenta(objGiro);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarEventoVenta(EEventoVenta objGiro)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarEventoVenta(objGiro);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarEventoVenta(int evev_icod_evento_venta, int intUsuario, string strPc)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.EliminarEventoVenta(evev_icod_evento_venta, intUsuario, strPc);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Boleta
        public List<EBoletaCab> getBoletaCab(int id_boleta)
        {
            List<EBoletaCab> lista = new List<EBoletaCab>();
            try
            {
                lista = objVentasData.getBoletaCab(id_boleta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EBoletaCab> listarBoletaCab(int intEjericio)
        {
            List<EBoletaCab> lista = new List<EBoletaCab>();
            try
            {
                lista = objVentasData.listarBoletaCab(intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarBoletaDesdePlanilla(EBoletaCab objEBoleta, List<EBoletaDet> lstBolDet, ref EPlanillaCobranzaCab oBePlnCab,
          EPlanillaCobranzaDet oBePlnDet, List<EPagoDocVenta> lstPagos)
        {
            //ING. EDGAR ALFARO
            //FECHA: 08/01/2014
            TesoreriaData objTesoreriaData = new TesoreriaData();
            AdministracionSistemaData objAdminSistemaData = new AdministracionSistemaData();
            OperacionesData objOperacionesData = new OperacionesData();
            ContabilidadData objContabilidadData = new ContabilidadData();
            CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //VARIABLES DEL METODO
                    string strNroPlanCab = oBePlnCab.plnc_vnumero_planilla;
                    //
                    #region Doc. Por Cobrar de la boleta
                    //INSERTAR LA FACTURA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEBoleta.bovc_sfecha_boleta.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                    objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocBoletaVentaServicios;
                    objDXC.doxcc_vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                    objDXC.cliec_icod_cliente = objEBoleta.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEBoleta.cliec_vnombre_cliente;
                    objDXC.doxcc_sfecha_doc = objEBoleta.bovc_sfecha_boleta;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEBoleta.bovc_sfecha_vencim_boleta;
                    objDXC.tablc_iid_tipo_moneda = objEBoleta.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEBoleta.bovc_sfecha_boleta);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la boleta, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEBoleta.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEBoleta.bovc_npor_imp_igv) > 0) ? objEBoleta.bovc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEBoleta.bovc_npor_imp_igv) == 0) ? objEBoleta.bovc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = objEBoleta.bovc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEBoleta.bovc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEBoleta.bovc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEBoleta.bovc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = objEBoleta.tablc_iid_situacion;
                    objDXC.doxcc_vobservaciones = "";//vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;
                    objDXC.doxcc_sfecha_entrega = null;
                    objDXC.doxcc_bind_impresion_nogerencia = false;
                    objDXC.doxc_bind_situacion_legal = false;
                    objDXC.doxc_bind_cierre_cuenta_corriente = false;
                    objDXC.intUsuario = objEBoleta.intUsuario;
                    objDXC.strPc = objEBoleta.strPc;
                    objDXC.doxcc_tipo_comprobante_referencia = 0;
                    objDXC.doxcc_num_serie_referencia = "";
                    objDXC.doxcc_num_comprobante_referencia = "";
                    objDXC.doxcc_sfecha_emision_referencia = null;
                    objDXC.anio = objEBoleta.bovc_sfecha_boleta.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "P";
                    //INGRESAMOS EL DOC POR COBRAR PARA CAPTRA EL ID DEL XC
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    objEBoleta.doxcc_icod_correlativo = objCuentaCobrarData.insertarDxc(objDXC, Lista);
                    objDXC.doxcc_icod_correlativo = objEBoleta.doxcc_icod_correlativo;

                    #endregion
                    #region Boleta
                    #region Boleta Cab. Insertar
                    //OBTENER EL CORRELATIVO RECIENTE, PARA ASEGURARSE QUE NO HAYA DUPLICADOS

                    //var lst = objAdminSistemaData.getCorrelativoTipoDocumento(Parametros.intTipoDocBoletaVenta);

                    //objEBoleta.bovc_vnumero_boleta = String.Format("{0:000}{1:0000000}", lst[0].tdocc_nro_serie, (Convert.ToInt32(lst[0].tdocc_nro_correlativo) + 1));
                    objEBoleta.bovc_icod_boleta = objVentasData.insertarBoleta(objEBoleta);
                    //objAdminSistemaData.updateCorrelativoTipoDocumento(Parametros.intTipoDocBoletaVenta, Convert.ToInt32(objEBoleta.bovc_vnumero_boleta.Substring(3, 7)), 1);
                    #endregion
                    #region Boleta Det. Insertar
                    int i = 1;
                    string vdescripcion_1erProducto = "";

                    if (objEBoleta.remic_icod_remision == 0)
                    {


                        lstBolDet.ForEach(x =>
                        {

                            #region Salida de Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = objEBoleta.bovc_sfecha_boleta.Year;
                            obKardex.kardc_fecha_movimiento = objEBoleta.bovc_sfecha_boleta;
                            obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.bovd_ncantidad);
                            obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                            obKardex.kardc_numero_doc = objEBoleta.bovc_vnumero_boleta;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.kardc_iid_motivo = Parametros.intMotivoKrdVentasOut;
                            obKardex.kardc_beneficiario = "";
                            obKardex.kardc_observaciones = "";
                            obKardex.intUsuario = objEBoleta.intUsuario;
                            obKardex.strPc = objEBoleta.strPc;
                            x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                            #endregion
                            #region Actualizando Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = objEBoleta.bovc_sfecha_boleta.Year;
                            stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stck.stocc_stock_producto = x.bovd_ncantidad;
                            stck.intTipoMovimiento = 0;
                            objAlmacenData.actualizarStock(stck);
                            #endregion



                            if (i == 1)
                            {
                                vdescripcion_1erProducto = x.strDesProducto;
                            }
                            x.bovc_icod_boleta = objEBoleta.bovc_icod_boleta;
                            objVentasData.insertarBoletaDetalle(x);
                        });

                    }
                    else
                    {
                        lstBolDet.ForEach(x =>
                        {
                            x.kardc_icod_correlativo = null;
                            if (i == 1)
                            {
                                vdescripcion_1erProducto = x.strDesProducto;
                            }
                            x.bovc_icod_boleta = objEBoleta.bovc_icod_boleta;
                            objVentasData.insertarBoletaDetalle(x);
                        });
                    }
                    #endregion

                    #region Pagos de la boleta
                    lstPagos.ForEach(x =>
                    {
                        //SE ASIGNA VALORES FALTANTES, QUE RECIEN SE HAN ADQUIRIDO EN EL METODO
                        x.pgoc_iid_tipo_doc_docventa = Parametros.intTipoDocBoletaVenta;
                        x.pgoc_icod_documento = objEBoleta.bovc_icod_boleta;
                        x.pgoc_dxc_icod_doc = objDXC.doxcc_icod_correlativo;
                        x.pgoc_sfecha_pago = oBePlnDet.plnd_sfecha_doc;
                        x.pgoc_vnumero_planilla = strNroPlanCab;
                        x.pgoc_icod_cliente = objEBoleta.cliec_icod_cliente;
                        //TOMAR EN CUENTA LOS TIPOS DE PAGO
                        if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            #region Dxp Pago
                            //EL PAGO ES EN EFECTIVO, SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            #region Tarjeta de Credito
                            //EL PAGO ES EN TARJETA DE CRED., SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            //EL PAGO ES EN TARJETA DE CRED., DEBE SER INGRESADO EN BANCOS
                            var lstTipoTarjeta = listarTipoTarjeta();
                            ETipoTarjeta oBeTipoTarjeta = lstTipoTarjeta.Where(d => d.tcrc_icod_tipo_tarjeta_cred == Convert.ToInt32(x.pgoc_icod_tipo_tarjeta)).ToList()[0];
                            //CABECERA DEL MOV. DE BANCOS
                            ELibroBancos oBeBcoMovCab = new ELibroBancos();
                            oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                            oBeBcoMovCab.iid_mes = x.pgoc_sfecha_pago.Month;
                            oBeBcoMovCab.dfecha_movimiento = x.pgoc_sfecha_pago;
                            oBeBcoMovCab.icod_enti_financiera_cuenta = oBeTipoTarjeta.bcod_icod_banco_cuenta;
                            oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            oBeBcoMovCab.vglosa = "PAGO CON TARJETA";
                            oBeBcoMovCab.vdescripcion_beneficiario = oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred;
                            oBeBcoMovCab.iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBeBcoMovCab.cliec_icod_cliente = objEBoleta.cliec_icod_cliente;
                            oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(x.pgoc_sfecha_pago);
                            oBeBcoMovCab.nmonto_movimiento = x.pgoc_nmonto;
                            oBeBcoMovCab.nmonto_saldo_banco = 0;
                            oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                            oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                            oBeBcoMovCab.vnro_documento = String.Format("{0:000}-{1:00}", Convert.ToInt32(strNroPlanCab), oBeTipoTarjeta.tcrc_iid_tipo_tarjeta_cred);
                            oBeBcoMovCab.cflag_conciliacion = false;
                            oBeBcoMovCab.iusuario_crea = x.intUsuario;
                            oBeBcoMovCab.vpc_crea = x.strPc;
                            oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                            oBeBcoMovCab.mobac_flag_estado = true;
                            oBeBcoMovCab.TipoDocumento = "PVD";
                            oBeBcoMovCab.mobac_origen_regitro = "PLN";
                            x.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                            oBeBcoMovCab.icod_correlativo = Convert.ToInt32(x.pgoc_icod_entidad_finan_mov);
                            //DETALLE DEL MOV. BANCOS
                            ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                            oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                            oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                            oBeBcoMovDet.doxcc_sfecha_doc = objEBoleta.bovc_sfecha_boleta;
                            oBeBcoMovDet.mobdc_iid_anio = Parametros.intEjercicio;
                            oBeBcoMovDet.mobdc_iid_mes_periodo = objEBoleta.bovc_sfecha_boleta.Month;
                            oBeBcoMovDet.vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                            oBeBcoMovDet.doxcc_vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                            oBeBcoMovDet.mobdc_icod_cliente = objEBoleta.cliec_icod_cliente;
                            oBeBcoMovDet.mto_mov_soles = x.pgoc_nmonto;
                            oBeBcoMovDet.mto_mov_dolar = Math.Round(x.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                            oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                            oBeBcoMovDet.iusuario_crea = objEBoleta.intUsuario;
                            oBeBcoMovDet.vpc_crea = objEBoleta.strPc;
                            oBeBcoMovDet.mobdc_flag_estado = true;
                            oBeBcoMovDet.doxcc_icod_correlativo = objDXC.doxcc_icod_correlativo;
                            oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                            oBeBcoMovDet.docxc_icod_pago = x.pgoc_dxc_icod_pago;

                            //SE INSERTAR EL DETALLE
                            objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            #region Ingreso del pago Dxp Pago
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            #endregion
                            #region Insertar Pago Nota de Credito
                            //EL PAGO CON LA NOTA DE CREDITO
                            ENotaCreditoPago oBe = new ENotaCreditoPago();
                            oBe.doxcc_icod_correlativo_pago = x.pgoc_dxc_icod_doc; //el documento a pagar
                            oBe.doxcc_icod_correlativo_nota_credito = Convert.ToInt64(x.pgoc_icod_nota_credito); //correlativo de la nota de crédito   
                            oBe.tablc_iid_tipo_moneda = Convert.ToInt32(x.pgoc_icod_tipo_moneda); //debe grabarse con el tipo de moneda del documento nota de crédito
                            oBe.ncpac_nmonto_pago = Convert.ToDecimal(x.pgoc_nmonto); //monto que se va a pagar guardado con el tipo de moneda del documento nota de crédito
                            oBe.ncpac_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePlnDet.plnd_sfecha_doc); //tipo de cambio de la fecha seleccionada
                            oBe.ncpac_vdescripcion = String.Format("N° PLN VENTA: {0}", strNroPlanCab);
                            oBe.ncpac_sfecha_pago = Convert.ToDateTime(oBePlnDet.plnd_sfecha_doc); //fecha del pago
                            oBe.ncpac_iorigen = "P"; //Nota de credito
                            oBe.ncpac_flag_estado = true;
                            oBe.ncpac_iusuario_crea = x.intUsuario;
                            oBe.ncpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                            oBe.ncpac_iusuario_modifica = x.intUsuario;
                            oBe.ncpac_vpc_modifica = WindowsIdentity.GetCurrent().Name;

                            oBe.pdxcc_icod_correlativo = x.pgoc_dxc_icod_pago;
                            x.pgoc_dxc_icod_canje_doc = new CuentasPorCobrarData().insertarNCPago(oBe);

                            #endregion
                            #region Actualizacion de Estados
                            TesoreriaData objTesoreriaData1 = new TesoreriaData();
                            objTesoreriaData1.ActualizarMontoPagadoSaldoNotaCreditoCliente(oBe.doxcc_icod_correlativo_nota_credito, 0);
                            objTesoreriaData1.ActualizarMontoDXCPagadoSaldo(oBe.doxcc_icod_correlativo_pago, 0);
                            #endregion

                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                        {
                            #region Pago Cheque
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);

                            ELibroBancos oBeBcoMovCab = new ELibroBancos();
                            oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                            oBeBcoMovCab.iid_mes = x.pgoc_sfecha_pago.Month;
                            oBeBcoMovCab.dfecha_movimiento = x.pgoc_sfecha_pago;
                            oBeBcoMovCab.icod_enti_financiera_cuenta = Convert.ToInt32(x.bcod_icod_banco_cuenta);
                            oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            oBeBcoMovCab.vglosa = "PAGO CHEQUE";
                            oBeBcoMovCab.vdescripcion_beneficiario = objEBoleta.cliec_vcod_cliente;
                            oBeBcoMovCab.iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBeBcoMovCab.cliec_icod_cliente = objEBoleta.cliec_icod_cliente;
                            oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(x.pgoc_sfecha_pago);
                            oBeBcoMovCab.nmonto_movimiento = x.pgoc_nmonto;
                            //oBeBcoMovCab.nmonto_saldo_banco = 0;
                            oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                            oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                            oBeBcoMovCab.vnro_documento = String.Format("{0:000}", Convert.ToInt32(strNroPlanCab));
                            oBeBcoMovCab.cflag_conciliacion = false;
                            oBeBcoMovCab.iusuario_crea = x.intUsuario;
                            oBeBcoMovCab.vpc_crea = x.strPc;
                            oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                            oBeBcoMovCab.mobac_flag_estado = true;
                            oBeBcoMovCab.TipoDocumento = "CHQ";
                            oBeBcoMovCab.mobac_origen_regitro = "PLN";
                            x.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                            oBeBcoMovCab.icod_correlativo = Convert.ToInt32(x.pgoc_icod_entidad_finan_mov);
                            //DETALLE DEL MOV. BANCOS
                            ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                            oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                            oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                            oBeBcoMovDet.doxcc_sfecha_doc = objEBoleta.bovc_sfecha_boleta;
                            oBeBcoMovDet.mobdc_iid_anio = Parametros.intEjercicio;
                            oBeBcoMovDet.mobdc_iid_mes_periodo = objEBoleta.bovc_sfecha_boleta.Month;
                            oBeBcoMovDet.vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                            oBeBcoMovDet.doxcc_vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                            oBeBcoMovDet.mobdc_icod_cliente = objEBoleta.cliec_icod_cliente;
                            oBeBcoMovDet.mto_mov_soles = x.pgoc_nmonto;
                            oBeBcoMovDet.mto_mov_dolar = Math.Round(x.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                            // oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                            oBeBcoMovDet.iusuario_crea = objEBoleta.intUsuario;
                            oBeBcoMovDet.vpc_crea = objEBoleta.strPc;
                            oBeBcoMovDet.mobdc_flag_estado = true;
                            oBeBcoMovDet.doxcc_icod_correlativo = objDXC.doxcc_icod_correlativo;
                            oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                            oBeBcoMovDet.docxc_icod_pago = x.pgoc_dxc_icod_pago;

                            //SE INSERTAR EL DETALLE
                            objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                        {
                            #region Transferencia Bancaria
                            //EL PAGO ES DE TRANSFERENCOIA BANCARIA., SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            ////EL PAGO ES EN TARJETA DE CRED., DEBE SER INGRESADO EN BANCOS
                            ////var lstTipoTarjeta = listarTipoTarjeta();
                            ////ETipoTarjeta oBeTipoTarjeta = lstTipoTarjeta.Where(d => d.tcrc_icod_tipo_tarjeta_cred == Convert.ToInt32(x.pgoc_icod_tipo_tarjeta)).ToList()[0];
                            //CABECERA DEL MOV. DE BANCOS 
                            ELibroBancos oBeBcoMovCab = new ELibroBancos();
                            oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                            oBeBcoMovCab.iid_mes = x.pgoc_sfecha_pago.Month;
                            oBeBcoMovCab.dfecha_movimiento = x.pgoc_sfecha_pago;
                            oBeBcoMovCab.icod_enti_financiera_cuenta = Convert.ToInt32(x.bcod_icod_banco_cuenta);
                            oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            oBeBcoMovCab.vglosa = "TRANSFERENCIA BANCARIA";
                            oBeBcoMovCab.vdescripcion_beneficiario = objEBoleta.cliec_vcod_cliente;
                            oBeBcoMovCab.iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBeBcoMovCab.cliec_icod_cliente = objEBoleta.cliec_icod_cliente;
                            oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(x.pgoc_sfecha_pago);
                            oBeBcoMovCab.nmonto_movimiento = x.pgoc_nmonto;
                            // oBeBcoMovCab.nmonto_saldo_banco = 0;
                            oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                            oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                            oBeBcoMovCab.vnro_documento = String.Format("{0:000}", Convert.ToInt32(strNroPlanCab));
                            oBeBcoMovCab.cflag_conciliacion = false;
                            oBeBcoMovCab.iusuario_crea = x.intUsuario;
                            oBeBcoMovCab.vpc_crea = x.strPc;
                            oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                            oBeBcoMovCab.mobac_flag_estado = true;
                            oBeBcoMovCab.TipoDocumento = "TRANS";
                            oBeBcoMovCab.mobac_origen_regitro = "PLN";
                            x.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                            oBeBcoMovCab.icod_correlativo = Convert.ToInt32(x.pgoc_icod_entidad_finan_mov);
                            //DETALLE DEL MOV. BANCOS
                            ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                            oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                            oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                            oBeBcoMovDet.doxcc_sfecha_doc = objEBoleta.bovc_sfecha_boleta;
                            oBeBcoMovDet.mobdc_iid_anio = Parametros.intEjercicio;
                            oBeBcoMovDet.mobdc_iid_mes_periodo = objEBoleta.bovc_sfecha_boleta.Month;
                            oBeBcoMovDet.vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                            oBeBcoMovDet.doxcc_vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                            oBeBcoMovDet.mobdc_icod_cliente = objEBoleta.cliec_icod_cliente;
                            oBeBcoMovDet.mto_mov_soles = x.pgoc_nmonto;
                            oBeBcoMovDet.mto_mov_dolar = Math.Round(x.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                            // oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                            oBeBcoMovDet.iusuario_crea = objEBoleta.intUsuario;
                            oBeBcoMovDet.vpc_crea = objEBoleta.strPc;
                            oBeBcoMovDet.mobdc_flag_estado = true;
                            oBeBcoMovDet.doxcc_icod_correlativo = objDXC.doxcc_icod_correlativo;
                            oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                            oBeBcoMovDet.docxc_icod_pago = x.pgoc_dxc_icod_pago;

                            //SE INSERTAR EL DETALLE
                            objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {

                            EDocXCobrarPago oBePagoDXC1 = new EDocXCobrarPago();
                            //oBePagoDXC1.pdxcc_icod_correlativo = oBePago.pgoc_dxc_icod_pago;
                            oBePagoDXC1.doxcc_icod_correlativo = Convert.ToInt64(objEBoleta.doxcc_icod_correlativo);
                            oBePagoDXC1.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                            oBePagoDXC1.pdxcc_vnumero_doc = x.strNroAnticipo;
                            oBePagoDXC1.pdxcc_sfecha_cobro = oBePlnDet.plnd_sfecha_doc;
                            oBePagoDXC1.tablc_iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBePagoDXC1.pdxcc_nmonto_cobro = x.pgoc_nmonto;
                            oBePagoDXC1.pdxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePlnDet.plnd_sfecha_doc);
                            oBePagoDXC1.pdxcc_vobservacion = String.Format("N° PLN VENTA: {0}", strNroPlanCab);
                            oBePagoDXC1.cliec_icod_cliente = x.pgoc_icod_cliente;
                            oBePagoDXC1.pdxcc_vorigen = "P";
                            oBePagoDXC1.intUsuario = x.intUsuario;
                            oBePagoDXC1.strPc = x.strPc;
                            oBePagoDXC1.pdxcc_flag_estado = true;
                            x.pgoc_dxc_icod_canje_doc = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC1);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(objEBoleta.doxcc_icod_correlativo), 0);

                            EAdelantoPago oBe = new EAdelantoPago();
                            oBe.doxcc_icod_correlativo_pago = objEBoleta.doxcc_icod_correlativo; //el documento a pagar
                            oBe.doxcc_icod_correlativo_adelanto = Convert.ToInt32(x.pgoc_icod_anticipo); //correlativo del adelanto
                            oBe.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta; //tipo documento(adelanto)
                            oBe.tdocc_iid_correlativo_adelanto = Convert.ToInt32(oBePagoDXC1.tdodc_iid_correlativo); //clase del documento
                            oBe.vnumero_doc_adelanto = objEBoleta.bovc_vnumero_boleta; //ndoc del adelanto con que se va a pagar
                            oBe.cliec_icod_cliente = Convert.ToInt32(x.pgoc_icod_cliente); //código del cliente
                            oBe.tablc_iid_tipo_moneda = Convert.ToInt32(x.pgoc_icod_tipo_moneda); //debe grabarse con el tipo de moneda del documento adelanto
                            oBe.adpac_nmonto_pago = Convert.ToDecimal(x.pgoc_nmonto); //monto que se va a pagar guardado con el tipo de moneda del documento adelanto
                            oBe.adpac_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBePlnDet.plnd_sfecha_doc); //tipo de cambio de la fecha seleccionada
                            oBe.adpac_vdescripcion = String.Format("N° PLN VENTA: {0} - {1} ", x.pgoc_vnumero_planilla, x.pgoc_descripcion.ToUpper());
                            oBe.adpac_sfecha_pago = Convert.ToDateTime(oBePlnDet.plnd_sfecha_doc); //fecha del pago
                            oBe.adpac_iorigen = "P"; //Adelanto
                            oBe.adpac_iusuario_crea = x.intUsuario;
                            oBe.adpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                            oBe.adpac_iusuario_modifica = x.intUsuario;
                            oBe.adpac_vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                            oBe.adpac_flag_estado = true;
                            oBe.SaldoDXCAdelanto = Convert.ToDecimal(x.pgoc_nmonto);
                            oBe.doxcc_nmonto_pagado = 0;
                            x.pgoc_dxc_icod_pago = new CuentasPorCobrarData().insertarAdelantoPago(oBe);
                            new TesoreriaData().ActualizarMontoPagadoAdelantoCliente(Convert.ToInt32(x.pgoc_icod_anticipo), 0);

                            ////EL PAGO SE INGRESA COMO PAGO DEL DOC. POR COBRAR
                            //EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            //x.pgoc_dxc_icod_pago = objCuentaCobrarData.insertarDXCPago(oBePagoDXC);
                            //
                        }

                        //FINALMENTE SE INSERTA EL PAGO DEL DOCUMENTO DE VENTA(EN ESTE CASO LA FACTURA)
                        objVentasData.insertarPago(x);
                    });
                    #endregion
                    //UNA VEZ QUE SE HA TERMINADO CON LOS PAGOS, SE ACTUALIZA LA SITUACION EL DOC. POR COBRAR.
                    objTesoreriaData.ActualizarMontoDXCPagadoSaldo(objEBoleta.doxcc_icod_correlativo, 0);
                    #endregion
                    #region Planilla Cab
                    //INSERTAR LA CAB. DE LA PLANILLA (SE INSERTAR SI ES EL PRIMER REGISTRO, SINO SE MODIFICA)
                    if (oBePlnCab.plnc_icod_planilla == 0)
                    {
                        //INSERTAR LA CAB. DE LA PLANILLA (SE REALIZA SOLO CONE L PRIMER REGISTRO DE UN MOVIMIENTO)                     
                        oBePlnCab.plnc_icod_planilla = objVentasData.insertarPlanillaCobranzaCab(oBePlnCab);
                    }
                    else
                    {
                        //NO SE REALIZA NINGUNA ACCION, PORQUE LA CAB. PLANILLA SE ACTUALIZARA AUTOMATICAMENTE AL TERMINAR LA INSERCION DE LA FACTURA
                    }
                    //INSERTAR EL DET. DE LA PLANILLA
                    #endregion
                    #region Planilla Det
                    oBePlnDet.plnc_icod_planilla = oBePlnCab.plnc_icod_planilla;
                    oBePlnDet.plnd_nmonto_pagado = (lstPagos.Count > 0) ? lstPagos.Where(x => x.pgoc_tipo_pago != 6).Sum(x => x.pgoc_nmonto) : 0;
                    oBePlnDet.tablc_iid_tipo_mov = Parametros.intPlnFacturacion;
                    oBePlnDet.plnd_icod_documento = objEBoleta.bovc_icod_boleta;
                    //INGRESAR EL REGISTRO DETALLE DE LA PLANILLA (Ej. Facturacion, pago o anticipo - En este caso es Facturacion)
                    oBePlnDet.plnd_icod_detalle = objVentasData.insertarPlanillaCobranzaDetalle(oBePlnDet);
                    #endregion
                    tx.Complete();
                    return oBePlnDet.plnd_icod_detalle;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarBoletaDesdePlanilla(EBoletaCab objEBoleta, List<EBoletaDet> lstBolDet, List<EBoletaDet> lstBolDelete,
           EPlanillaCobranzaCab oBePlnCab, EPlanillaCobranzaDet oBePlnDet, List<EPagoDocVenta> lstPagos, List<EPagoDocVenta> lstDeletePagos)
        {
            try
            {
                //ING. EDGAR ALFARO
                //FECHA: 08/01/2014
                TesoreriaData objTesoreriaData = new TesoreriaData();
                AdministracionSistemaData objAdminSistemaData = new AdministracionSistemaData();
                OperacionesData objOperacionesData = new OperacionesData();
                ContabilidadData objContabilidadData = new ContabilidadData();
                CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //VARIABLES DEL METODO
                    string strNroPlanCab = oBePlnCab.plnc_vnumero_planilla;
                    //
                    #region Boleta
                    //MODIFICAR LA CAB. DE LA FACTURA
                    objVentasData.modificarBoleta(objEBoleta);
                    //
                    #region Boleta Det.
                    lstBolDelete.ForEach(x =>
                    {
                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                        obKardexDel.intUsuario = objEBoleta.intUsuario;
                        obKardexDel.strPc = objEBoleta.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        #endregion
                        objVentasData.eliminarBoletaDetalle(x);
                    });
                    int i = 1;
                    string vdescripcion_1erProducto = "";
                    lstBolDet.ForEach(x =>
                    {



                        if (x.intTipoOperacion == 1)
                        {
                            #region Salida de Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = objEBoleta.bovc_sfecha_boleta.Year;
                            obKardex.kardc_fecha_movimiento = objEBoleta.bovc_sfecha_boleta;
                            obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.bovd_ncantidad);
                            obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                            obKardex.kardc_numero_doc = objEBoleta.bovc_vnumero_boleta;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.kardc_iid_motivo = Parametros.intMotivoKrdVentasOut;
                            obKardex.kardc_beneficiario = "";
                            obKardex.kardc_observaciones = "";
                            obKardex.intUsuario = objEBoleta.intUsuario;
                            obKardex.strPc = objEBoleta.strPc;
                            x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                            #endregion
                            #region Actualizando Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = objEBoleta.bovc_sfecha_boleta.Year;
                            stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stck.stocc_stock_producto = x.bovd_ncantidad;
                            stck.intTipoMovimiento = 0;
                            objAlmacenData.actualizarStock(stck);
                            #endregion

                            //SE INSERTA EL ITEM DE LA FACTURA
                            x.bovc_icod_boleta = objEBoleta.bovc_icod_boleta;
                            objVentasData.insertarBoletaDetalle(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {

                            #region Eliminar Kardex Previo
                            EKardex obKardexDel = new EKardex();
                            obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                            obKardexDel.intUsuario = objEBoleta.intUsuario;
                            obKardexDel.strPc = objEBoleta.strPc;
                            objAlmacenData.eliminarKardex(obKardexDel);
                            #endregion
                            #region Salida de Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = objEBoleta.bovc_sfecha_boleta.Year;
                            obKardex.kardc_fecha_movimiento = objEBoleta.bovc_sfecha_boleta;
                            obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.bovd_ncantidad);
                            obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                            obKardex.kardc_numero_doc = objEBoleta.bovc_vnumero_boleta;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.kardc_iid_motivo = Parametros.intMotivoKrdVentasOut;
                            obKardex.kardc_beneficiario = "";
                            obKardex.kardc_observaciones = "";
                            obKardex.intUsuario = objEBoleta.intUsuario;
                            obKardex.strPc = objEBoleta.strPc;
                            x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                            #endregion
                            #region Actualizando Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = objEBoleta.bovc_sfecha_boleta.Year;
                            stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stck.stocc_stock_producto = x.bovd_ncantidad;
                            stck.intTipoMovimiento = 0;
                            objAlmacenData.actualizarStock(stck);
                            #endregion

                            //SE MODIFICA EL ITEM DEL DETALLE DE LA FACTURA
                            objVentasData.modificarBoletaDetalle(x);
                        }

                    });
                    #endregion
                    #region Doc. Por Cobrar de la Boleta
                    //INSERTAR LA FACTURA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = objEBoleta.doxcc_icod_correlativo;
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEBoleta.bovc_sfecha_boleta.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                    objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocBoletaVentaServicios;
                    objDXC.doxcc_vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                    objDXC.cliec_icod_cliente = objEBoleta.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEBoleta.cliec_vnombre_cliente;
                    objDXC.doxcc_sfecha_doc = objEBoleta.bovc_sfecha_boleta;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEBoleta.bovc_sfecha_vencim_boleta;
                    objDXC.tablc_iid_tipo_moneda = objEBoleta.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEBoleta.bovc_sfecha_boleta);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEBoleta.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEBoleta.bovc_npor_imp_igv) > 0) ? objEBoleta.bovc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEBoleta.bovc_npor_imp_igv) == 0) ? objEBoleta.bovc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = objEBoleta.bovc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEBoleta.bovc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEBoleta.bovc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEBoleta.bovc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = objEBoleta.tablc_iid_situacion;
                    objDXC.doxcc_vobservaciones = vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;
                    objDXC.doxcc_bind_impresion_nogerencia = false;
                    objDXC.doxc_bind_situacion_legal = false;
                    objDXC.doxc_bind_cierre_cuenta_corriente = false;
                    objDXC.intUsuario = objEBoleta.intUsuario;
                    objDXC.strPc = objEBoleta.strPc;
                    //objDXC.docxc_icod_documento = objEBoleta.bovc_icod_boleta;
                    objDXC.anio = objEBoleta.bovc_sfecha_boleta.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "P";
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    new CuentasPorCobrarData().modificarDxc(objDXC, Lista, Lista);
                    #endregion
                    #region Pagos de la boleta
                    //PRIMERO SE DEBE ELIMINAR, TODOS LOS PAGOS PARA REINGRESAR LOS PAGOS
                    var lstPagosAux = new BVentas().listarPago(Convert.ToInt32(oBePlnDet.plnd_icod_tipo_doc), Convert.ToInt32(oBePlnDet.plnd_icod_documento));
                    lstPagosAux.ForEach(x =>
                    {
                        if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            //SE ELIMINA EL MOVIMIENTO DEL BANCO
                            objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            #region Nota de Credito
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);

                            ENotaCreditoPago _beNCP = new ENotaCreditoPago();
                            _beNCP.ncpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            _beNCP.ncpac_vpc_modifica = objEBoleta.strPc;
                            _beNCP.ncpac_iusuario_modifica = objEBoleta.intUsuario;
                            objCuentaCobrarData.eliminarNCPago(_beNCP);
                            objTesoreriaData.ActualizarMontoPagadoSaldoNotaCreditoCliente(Convert.ToInt64(x.pgoc_icod_nota_credito), 0);
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                        {
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            //SE ELIMINA EL MOVIMIENTO DEL BANCO
                            objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                        {
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            //SE ELIMINA EL MOVIMIENTO DEL BANCO
                            objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL PAGO DEL ANTICIPO
                            EAdelantoPago oBePagoDXCAnt = new EAdelantoPago();
                            oBePagoDXCAnt.adpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXCAnt.doxcc_icod_correlativo_adelanto = Convert.ToInt64(x.pgoc_icod_anticipo);
                            oBePagoDXCAnt.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXCAnt.strPc = objEBoleta.strPc;
                            new CuentasPorCobrarData().eliminarAdelantoPago(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(Convert.ToInt64(x.pgoc_icod_anticipo), 0);
                        }
                        //FINALMENTE SE ELIMINA EL PAGO DEL DOC. DE VENTA (EN ESTE CASO DE LA BOLETA)
                        objVentasData.eliminarPago(x);
                    });


                    lstPagos.ForEach(x =>
                    {
                        //SE ASIGNA VALORES FALTANTES, QUE RECIEN SE HAN ADQUIRIDO EN EL METODO
                        x.pgoc_iid_tipo_doc_docventa = Parametros.intTipoDocBoletaVenta;
                        x.pgoc_icod_documento = objEBoleta.bovc_icod_boleta;
                        x.pgoc_dxc_icod_doc = objDXC.doxcc_icod_correlativo;
                        x.pgoc_sfecha_pago = oBePlnDet.plnd_sfecha_doc;
                        x.pgoc_vnumero_planilla = strNroPlanCab;
                        x.pgoc_icod_cliente = objEBoleta.cliec_icod_cliente;
                        //TOMAR EN CUENTA LOS TIPOS DE PAGO
                        if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            #region Dxc Pago
                            //EL PAGO ES EN EFECTIVO, SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            #region Tarjeta de Credito
                            //EL PAGO ES EN TARJETA DE CRED., SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            //EL PAGO ES EN TARJETA DE CRED., DEBE SER INGRESADO EN BANCOS
                            var lstTipoTarjeta = listarTipoTarjeta();
                            ETipoTarjeta oBeTipoTarjeta = lstTipoTarjeta.Where(d => d.tcrc_icod_tipo_tarjeta_cred == Convert.ToInt32(x.pgoc_icod_tipo_tarjeta)).ToList()[0];
                            //CABECERA DEL MOV. DE BANCOS
                            ELibroBancos oBeBcoMovCab = new ELibroBancos();
                            oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                            oBeBcoMovCab.iid_mes = x.pgoc_sfecha_pago.Month;
                            oBeBcoMovCab.dfecha_movimiento = x.pgoc_sfecha_pago;
                            oBeBcoMovCab.icod_enti_financiera_cuenta = oBeTipoTarjeta.bcod_icod_banco_cuenta;
                            oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            oBeBcoMovCab.vglosa = "PAGO CON TARJETA";
                            oBeBcoMovCab.vdescripcion_beneficiario = oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred;
                            oBeBcoMovCab.iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBeBcoMovCab.cliec_icod_cliente = objEBoleta.cliec_icod_cliente;
                            oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(x.pgoc_sfecha_pago);
                            oBeBcoMovCab.nmonto_movimiento = x.pgoc_nmonto;
                            oBeBcoMovCab.nmonto_saldo_banco = 0;
                            oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                            oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                            oBeBcoMovCab.vnro_documento = String.Format("{0:000}-{1:00}", Convert.ToInt32(strNroPlanCab), oBeTipoTarjeta.tcrc_iid_tipo_tarjeta_cred);
                            oBeBcoMovCab.cflag_conciliacion = false;
                            oBeBcoMovCab.iusuario_crea = x.intUsuario;
                            oBeBcoMovCab.vpc_crea = x.strPc;
                            oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                            oBeBcoMovCab.mobac_flag_estado = true;
                            oBeBcoMovCab.TipoDocumento = "PVD";
                            oBeBcoMovCab.mobac_origen_regitro = "PLN";
                            x.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                            oBeBcoMovCab.icod_correlativo = Convert.ToInt32(x.pgoc_icod_entidad_finan_mov);
                            //DETALLE DEL MOV. BANCOS
                            ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                            oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                            oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                            oBeBcoMovDet.doxcc_sfecha_doc = objEBoleta.bovc_sfecha_boleta;
                            oBeBcoMovDet.mobdc_iid_anio = Parametros.intEjercicio;
                            oBeBcoMovDet.mobdc_iid_mes_periodo = objEBoleta.bovc_sfecha_boleta.Month;
                            oBeBcoMovDet.vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                            oBeBcoMovDet.doxcc_vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                            oBeBcoMovDet.mobdc_icod_cliente = objEBoleta.cliec_icod_cliente;
                            oBeBcoMovDet.mto_mov_soles = x.pgoc_nmonto;
                            oBeBcoMovDet.mto_mov_dolar = Math.Round(x.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                            oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                            oBeBcoMovDet.iusuario_crea = objEBoleta.intUsuario;
                            oBeBcoMovDet.vpc_crea = objEBoleta.strPc;
                            oBeBcoMovDet.mobdc_flag_estado = true;
                            oBeBcoMovDet.doxcc_icod_correlativo = objDXC.doxcc_icod_correlativo;
                            oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                            oBeBcoMovDet.docxc_icod_pago = x.pgoc_dxc_icod_pago;

                            //SE INSERTAR EL DETALLE
                            objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            #region Ingreso del pago Dxp Pago
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            #endregion
                            #region Insertar Pago Nota de Credito
                            //EL PAGO CON LA NOTA DE CREDITO
                            ENotaCreditoPago oBe = new ENotaCreditoPago();
                            oBe.doxcc_icod_correlativo_pago = x.pgoc_dxc_icod_doc; //el documento a pagar
                            oBe.doxcc_icod_correlativo_nota_credito = Convert.ToInt64(x.pgoc_icod_nota_credito); //correlativo de la nota de crédito   
                            oBe.tablc_iid_tipo_moneda = Convert.ToInt32(x.pgoc_icod_tipo_moneda); //debe grabarse con el tipo de moneda del documento nota de crédito
                            oBe.ncpac_nmonto_pago = Convert.ToDecimal(x.pgoc_nmonto); //monto que se va a pagar guardado con el tipo de moneda del documento nota de crédito
                            oBe.ncpac_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePlnDet.plnd_sfecha_doc); //tipo de cambio de la fecha seleccionada
                            oBe.ncpac_vdescripcion = String.Format("N° PLN VENTA: {0}", strNroPlanCab);
                            oBe.ncpac_sfecha_pago = Convert.ToDateTime(oBePlnDet.plnd_sfecha_doc); //fecha del pago
                            oBe.ncpac_iorigen = "P"; //Nota de credito
                            oBe.ncpac_flag_estado = true;
                            oBe.ncpac_iusuario_crea = x.intUsuario;
                            oBe.ncpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                            oBe.ncpac_iusuario_modifica = x.intUsuario;
                            oBe.ncpac_vpc_modifica = WindowsIdentity.GetCurrent().Name;

                            oBe.pdxcc_icod_correlativo = x.pgoc_dxc_icod_pago;
                            x.pgoc_dxc_icod_canje_doc = new CuentasPorCobrarData().insertarNCPago(oBe);

                            #endregion
                            #region Actualizacion de Estados
                            TesoreriaData objTesoreriaData1 = new TesoreriaData();
                            objTesoreriaData1.ActualizarMontoPagadoSaldoNotaCreditoCliente(oBe.doxcc_icod_correlativo_nota_credito, 0);
                            objTesoreriaData1.ActualizarMontoDXCPagadoSaldo(oBe.doxcc_icod_correlativo_pago, 0);
                            #endregion

                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                        {
                            #region Pago Cheque
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);

                            ELibroBancos oBeBcoMovCab = new ELibroBancos();
                            oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                            oBeBcoMovCab.iid_mes = x.pgoc_sfecha_pago.Month;
                            oBeBcoMovCab.dfecha_movimiento = x.pgoc_sfecha_pago;
                            oBeBcoMovCab.icod_enti_financiera_cuenta = Convert.ToInt32(x.bcod_icod_banco_cuenta);
                            oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            oBeBcoMovCab.vglosa = "PAGO CHEQUE";
                            oBeBcoMovCab.vdescripcion_beneficiario = objEBoleta.cliec_vcod_cliente;
                            oBeBcoMovCab.iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBeBcoMovCab.cliec_icod_cliente = objEBoleta.cliec_icod_cliente;
                            oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(x.pgoc_sfecha_pago);
                            oBeBcoMovCab.nmonto_movimiento = x.pgoc_nmonto;
                            //oBeBcoMovCab.nmonto_saldo_banco = 0;
                            oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                            oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                            oBeBcoMovCab.vnro_documento = String.Format("{0:000}", Convert.ToInt32(strNroPlanCab));
                            oBeBcoMovCab.cflag_conciliacion = false;
                            oBeBcoMovCab.iusuario_crea = x.intUsuario;
                            oBeBcoMovCab.vpc_crea = x.strPc;
                            oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                            oBeBcoMovCab.mobac_flag_estado = true;
                            oBeBcoMovCab.TipoDocumento = "CHQ";
                            oBeBcoMovCab.mobac_origen_regitro = "PLN";
                            x.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                            oBeBcoMovCab.icod_correlativo = Convert.ToInt32(x.pgoc_icod_entidad_finan_mov);
                            //DETALLE DEL MOV. BANCOS
                            ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                            oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                            oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                            oBeBcoMovDet.doxcc_sfecha_doc = objEBoleta.bovc_sfecha_boleta;
                            oBeBcoMovDet.mobdc_iid_anio = Parametros.intEjercicio;
                            oBeBcoMovDet.mobdc_iid_mes_periodo = objEBoleta.bovc_sfecha_boleta.Month;
                            oBeBcoMovDet.vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                            oBeBcoMovDet.doxcc_vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                            oBeBcoMovDet.mobdc_icod_cliente = objEBoleta.cliec_icod_cliente;
                            oBeBcoMovDet.mto_mov_soles = x.pgoc_nmonto;
                            oBeBcoMovDet.mto_mov_dolar = Math.Round(x.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                            // oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                            oBeBcoMovDet.iusuario_crea = objEBoleta.intUsuario;
                            oBeBcoMovDet.vpc_crea = objEBoleta.strPc;
                            oBeBcoMovDet.mobdc_flag_estado = true;
                            oBeBcoMovDet.doxcc_icod_correlativo = objDXC.doxcc_icod_correlativo;
                            oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                            oBeBcoMovDet.docxc_icod_pago = x.pgoc_dxc_icod_pago;

                            //SE INSERTAR EL DETALLE
                            objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                            #endregion
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                        {
                            #region Transferencia Bancaria
                            //EL PAGO ES DE TRANSFERENCOIA BANCARIA., SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                            EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                            x.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            ////EL PAGO ES EN TARJETA DE CRED., DEBE SER INGRESADO EN BANCOS
                            ////var lstTipoTarjeta = listarTipoTarjeta();
                            ////ETipoTarjeta oBeTipoTarjeta = lstTipoTarjeta.Where(d => d.tcrc_icod_tipo_tarjeta_cred == Convert.ToInt32(x.pgoc_icod_tipo_tarjeta)).ToList()[0];
                            //CABECERA DEL MOV. DE BANCOS 
                            ELibroBancos oBeBcoMovCab = new ELibroBancos();
                            oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                            oBeBcoMovCab.iid_mes = x.pgoc_sfecha_pago.Month;
                            oBeBcoMovCab.dfecha_movimiento = x.pgoc_sfecha_pago;
                            oBeBcoMovCab.icod_enti_financiera_cuenta = Convert.ToInt32(x.bcod_icod_banco_cuenta);
                            oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                            oBeBcoMovCab.vglosa = "TRANSFERENCIA BANCARIA";
                            oBeBcoMovCab.vdescripcion_beneficiario = objEBoleta.cliec_vcod_cliente;
                            oBeBcoMovCab.iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBeBcoMovCab.cliec_icod_cliente = objEBoleta.cliec_icod_cliente;
                            oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(x.pgoc_sfecha_pago);
                            oBeBcoMovCab.nmonto_movimiento = x.pgoc_nmonto;
                            // oBeBcoMovCab.nmonto_saldo_banco = 0;
                            oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                            oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                            oBeBcoMovCab.vnro_documento = String.Format("{0:000}", Convert.ToInt32(strNroPlanCab));
                            oBeBcoMovCab.cflag_conciliacion = false;
                            oBeBcoMovCab.iusuario_crea = x.intUsuario;
                            oBeBcoMovCab.vpc_crea = x.strPc;
                            oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                            oBeBcoMovCab.mobac_flag_estado = true;
                            oBeBcoMovCab.TipoDocumento = "TRANS";
                            oBeBcoMovCab.mobac_origen_regitro = "PLN";
                            x.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                            oBeBcoMovCab.icod_correlativo = Convert.ToInt32(x.pgoc_icod_entidad_finan_mov);
                            //DETALLE DEL MOV. BANCOS
                            ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                            oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                            oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                            oBeBcoMovDet.doxcc_sfecha_doc = objEBoleta.bovc_sfecha_boleta;
                            oBeBcoMovDet.mobdc_iid_anio = Parametros.intEjercicio;
                            oBeBcoMovDet.mobdc_iid_mes_periodo = objEBoleta.bovc_sfecha_boleta.Month;
                            oBeBcoMovDet.vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                            oBeBcoMovDet.doxcc_vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                            oBeBcoMovDet.mobdc_icod_cliente = objEBoleta.cliec_icod_cliente;
                            oBeBcoMovDet.mto_mov_soles = x.pgoc_nmonto;
                            oBeBcoMovDet.mto_mov_dolar = Math.Round(x.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                            // oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                            oBeBcoMovDet.iusuario_crea = objEBoleta.intUsuario;
                            oBeBcoMovDet.vpc_crea = objEBoleta.strPc;
                            oBeBcoMovDet.mobdc_flag_estado = true;
                            oBeBcoMovDet.doxcc_icod_correlativo = objDXC.doxcc_icod_correlativo;
                            oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                            oBeBcoMovDet.docxc_icod_pago = x.pgoc_dxc_icod_pago;

                            //SE INSERTAR EL DETALLE
                            objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                            #endregion

                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {
                            #region Anticipio
                            EDocXCobrarPago oBePagoDXC1 = new EDocXCobrarPago();
                            oBePagoDXC1.doxcc_icod_correlativo = Convert.ToInt64(objEBoleta.doxcc_icod_correlativo);
                            oBePagoDXC1.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                            oBePagoDXC1.pdxcc_vnumero_doc = x.strNroAnticipo;
                            oBePagoDXC1.pdxcc_sfecha_cobro = oBePlnDet.plnd_sfecha_doc;
                            oBePagoDXC1.tablc_iid_tipo_moneda = x.pgoc_icod_tipo_moneda;
                            oBePagoDXC1.pdxcc_nmonto_cobro = x.pgoc_nmonto;
                            oBePagoDXC1.pdxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePlnDet.plnd_sfecha_doc);
                            oBePagoDXC1.pdxcc_vobservacion = String.Format("N° PLN VENTA: {0}", strNroPlanCab);
                            oBePagoDXC1.cliec_icod_cliente = x.pgoc_icod_cliente;
                            oBePagoDXC1.pdxcc_vorigen = "P";
                            oBePagoDXC1.intUsuario = x.intUsuario;
                            oBePagoDXC1.strPc = x.strPc;
                            oBePagoDXC1.pdxcc_flag_estado = true;
                            x.pgoc_dxc_icod_canje_doc = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC1);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(objEBoleta.doxcc_icod_correlativo), 0);

                            EAdelantoPago oBe = new EAdelantoPago();
                            oBe.doxcc_icod_correlativo_pago = objEBoleta.doxcc_icod_correlativo; //el documento a pagar
                            oBe.doxcc_icod_correlativo_adelanto = Convert.ToInt32(x.pgoc_icod_anticipo); //correlativo del adelanto
                            oBe.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta; //tipo documento(adelanto)
                            oBe.cliec_icod_cliente = Convert.ToInt32(x.pgoc_icod_cliente); //código del cliente
                            oBe.tablc_iid_tipo_moneda = Convert.ToInt32(x.pgoc_icod_tipo_moneda); //debe grabarse con el tipo de moneda del documento adelanto
                            oBe.adpac_nmonto_pago = Convert.ToDecimal(x.pgoc_nmonto); //monto que se va a pagar guardado con el tipo de moneda del documento adelanto
                            oBe.adpac_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBePlnDet.plnd_sfecha_doc); //tipo de cambio de la fecha seleccionada
                            oBe.adpac_vdescripcion = String.Format("N° PLN VENTA: {0} - {1} ", x.pgoc_vnumero_planilla, x.pgoc_descripcion.ToUpper());
                            oBe.adpac_sfecha_pago = Convert.ToDateTime(oBePlnDet.plnd_sfecha_doc); //fecha del pago
                            oBe.adpac_iorigen = "P"; //Adelanto
                            oBe.adpac_iusuario_crea = x.intUsuario;
                            oBe.adpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                            oBe.adpac_iusuario_modifica = x.intUsuario;
                            oBe.adpac_vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                            oBe.adpac_flag_estado = true;
                            oBe.SaldoDXCAdelanto = Convert.ToDecimal(x.pgoc_nmonto);
                            oBe.doxcc_nmonto_pagado = 0;
                            x.pgoc_dxc_icod_pago = new CuentasPorCobrarData().insertarAdelantoPago(oBe);
                            new TesoreriaData().ActualizarMontoPagadoAdelantoCliente(Convert.ToInt32(x.pgoc_icod_anticipo), 0);
                            #endregion
                        }

                        //FINALMENTE SE INSERTA EL PAGO DEL DOCUMENTO DE VENTA(EN ESTE CASO LA FACTURA)
                        objVentasData.insertarPago(x);
                    });
                    #endregion
                    //UNA VEZ QUE SE HA TERMINADO CON LOS PAGOS, SE ACTUALIZA LA SITUACION EL DOC. POR COBRAR.
                    objTesoreriaData.ActualizarMontoDXCPagadoSaldo(objEBoleta.doxcc_icod_correlativo, 0);
                    #endregion
                    #region Planilla Det.
                    oBePlnDet.plnd_nmonto_pagado = (lstPagos.Count > 0) ? lstPagos.Where(x => x.pgoc_tipo_pago != 6).Sum(x => x.pgoc_nmonto) : 0;
                    //MODIFICANDO LA PLANILLA DET.
                    objVentasData.modificarPlanillaCobranzaDetalle(oBePlnDet);
                    //objVentasData.modificarPlanillaCobranzaCab(oBePlnCab);

                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int insertarBoleta(EBoletaCab objEBoleta, List<EBoletaDet> lstBolDet)
        {

            int intIcodE = 0;
            try
            {
                AdministracionSistemaData objAdminSistemaData = new AdministracionSistemaData();
                OperacionesData objOperacionesData = new OperacionesData();
                ContabilidadData objContabilidadData = new ContabilidadData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region Descripcion
                    int i = 1;
                    string vdescripcion_1erProducto = "";
                    foreach (var _des in lstBolDet)
                    {
                        if (i == 1)
                        {
                            vdescripcion_1erProducto = _des.strDesProducto;
                        }
                    }
                    #endregion
                    #region Doc. Por Cobrar de la Factura
                    //INSERTAR LA BOLETA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEBoleta.bovc_sfecha_boleta.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                    objDXC.tdodc_iid_correlativo = 9;
                    objDXC.doxcc_vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                    objDXC.cliec_icod_cliente = objEBoleta.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEBoleta.cliec_vnombre_cliente;
                    objDXC.doxcc_sfecha_doc = objEBoleta.bovc_sfecha_boleta;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEBoleta.bovc_sfecha_vencim_boleta;
                    objDXC.tablc_iid_tipo_moneda = objEBoleta.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEBoleta.bovc_sfecha_boleta);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la boleta, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEBoleta.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEBoleta.bovc_npor_imp_igv) > 0) ? objEBoleta.bovc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEBoleta.bovc_npor_imp_igv) == 0) ? objEBoleta.bovc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = objEBoleta.bovc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEBoleta.bovc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEBoleta.bovc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEBoleta.bovc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = objEBoleta.tablc_iid_situacion;
                    objDXC.doxcc_vobservaciones = vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;
                    objDXC.doxcc_sfecha_entrega = null;
                    objDXC.doxcc_bind_impresion_nogerencia = false;

                    objDXC.intUsuario = objEBoleta.intUsuario;
                    objDXC.strPc = objEBoleta.strPc;
                    objDXC.doxcc_tipo_comprobante_referencia = 0;
                    objDXC.doxcc_num_serie_referencia = "";
                    objDXC.doxcc_num_comprobante_referencia = "";
                    objDXC.doxcc_sfecha_emision_referencia = null;
                    objDXC.anio = objEBoleta.bovc_sfecha_boleta.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "V";
                    objDXC.puvec_icod_punto_venta = objEBoleta.bovc_icod_pvt;
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    objEBoleta.doxcc_icod_correlativo = new CuentasPorCobrarData().insertarDxc(objDXC, Lista);
                    objDXC.doxcc_icod_correlativo = objEBoleta.doxcc_icod_correlativo;
                    #endregion
                    #region Boleta Cab. Insertar
                    //OBTENER EL CORRELATIVO RECIENTE, PARA ASEGURARSE QUE NO HAYA DUPLICADOS
                    //var lst = objAdminSistemaData.getCorrelativoTipoDocumento(Parametros.intTipoDocBoletaVenta);
                    objEBoleta.bovc_icod_boleta = objVentasData.insertarBoleta(objEBoleta);
                    objEBoleta.doc_icod_documento = objEBoleta.bovc_icod_boleta;
                    intIcodE = objVentasData.insertarboletaVentaElectronica(objEBoleta);
                    if (objEBoleta.bovc_icod_boleta == 0)
                    {
                        throw new Exception("El número de la Factura ya fue utilizado, intente con un número de Factura superior");
                    }
                    //ACTUALIZAR EL CORRELATIVO DE LA BOLETA
                    new BAdministracionSistema().updateCorrelativoTipoDocumentoSeries(objEBoleta.bovc_vnumero_boleta.Substring(0, 4), Convert.ToInt32(objEBoleta.bovc_vnumero_boleta.Substring(4, 8)));

                    #endregion
                    #region Factura Det. Insertar
                    if (objEBoleta.remic_icod_remision == 0)
                    {

                        foreach (var obj in lstBolDet)
                        {
                            object[] Tallas = new object[10];
                            object[] Cantidades = new object[10];
                            object[] idkardex = new object[10];
                            object[] idProducto = new object[10];

                            Tallas[0] = obj.bovd_talla1;
                            Tallas[1] = obj.bovd_talla2;
                            Tallas[2] = obj.bovd_talla3;
                            Tallas[3] = obj.bovd_talla4;
                            Tallas[4] = obj.bovd_talla5;
                            Tallas[5] = obj.bovd_talla6;
                            Tallas[6] = obj.bovd_talla7;
                            Tallas[7] = obj.bovd_talla8;
                            Tallas[8] = obj.bovd_talla9;
                            Tallas[9] = obj.bovd_talla10;
                            Cantidades[0] = obj.bovd_cant_talla1;
                            Cantidades[1] = obj.bovd_cant_talla2;
                            Cantidades[2] = obj.bovd_cant_talla3;
                            Cantidades[3] = obj.bovd_cant_talla4;
                            Cantidades[4] = obj.bovd_cant_talla5;
                            Cantidades[5] = obj.bovd_cant_talla6;
                            Cantidades[6] = obj.bovd_cant_talla7;
                            Cantidades[7] = obj.bovd_cant_talla8;
                            Cantidades[8] = obj.bovd_cant_talla9;
                            Cantidades[9] = obj.bovd_cant_talla10;
                            idProducto[0] = obj.bovd_icod_producto1;
                            idProducto[1] = obj.bovd_icod_producto2;
                            idProducto[2] = obj.bovd_icod_producto3;
                            idProducto[3] = obj.bovd_icod_producto4;
                            idProducto[4] = obj.bovd_icod_producto5;
                            idProducto[5] = obj.bovd_icod_producto6;
                            idProducto[6] = obj.bovd_icod_producto7;
                            idProducto[7] = obj.bovd_icod_producto8;
                            idProducto[8] = obj.bovd_icod_producto9;
                            idProducto[9] = obj.bovd_icod_producto10;
                            for (int l = 0; l <= 9; l++)
                            {
                                List<EProdProducto> oProducto = new List<EProdProducto>();
                                string codigoexterno = obj.bovd_vcodigo_extremo_prod + Tallas[l];
                                //oProducto = new BCentral().VerificarProdStockProducto(codigoexterno, Convert.ToInt32(obj.almac_icod_almacen), Convert.ToInt32(objEBoleta.puvec_icod_punto_venta));
                                if (oProducto.Count > 0)
                                {
                                    if (Convert.ToInt32(Cantidades[l]) != 0)
                                    {
                                        EProdKardex objk = new EProdKardex();
                                        objk.kardc_ianio = Parametros.intEjercicio;
                                        objk.kardx_sfecha = Convert.ToDateTime(objEBoleta.bovc_sfecha_boleta);
                                        objk.iid_almacen = Convert.ToInt32(objEBoleta.bovc_iid_almacen);
                                        objk.iid_producto = Convert.ToInt32(idProducto[l]);
                                        objk.puvec_icod_punto_venta = objEBoleta.puvec_icod_punto_venta;
                                        objk.Cantidad = Convert.ToDecimal(Cantidades[l]);
                                        //objk.NroNota = Convert.ToInt32(objEFactura.favc_vnumero_factura);
                                        objk.iid_tipo_doc = 26;
                                        objk.NroDocumento = objEBoleta.bovc_vnumero_boleta;
                                        objk.movimiento = 0;
                                        objk.Motivo = 101;
                                        objk.Beneficiario = objEBoleta.cliec_vnombre_cliente;
                                        objk.Observaciones = "VENTA DE MERCADERIA";
                                        objk.intUsuario = objEBoleta.intUsuario;
                                        objk.Item = Convert.ToInt32(obj.bovd_icod_item_boleta);
                                        objk.stocc_codigo_producto = codigoexterno;
                                        objk.operacion = 1;
                                        objk.kardc_iid_correlativo = new BCentral().InsertarKardexpvt(objk);
                                        idkardex[l] = objk.kardc_iid_correlativo;
                                        idProducto[i] = objk.iid_producto;
                                        EProdStockProducto objStock = new EProdStockProducto()
                                        {
                                            stocc_ianio = Parametros.intEjercicio,
                                            stocc_iid_almacen = objk.iid_almacen,
                                            stocc_iid_producto = objk.iid_producto,
                                            stocc_nstock_prod = objk.Cantidad,
                                            intTipoMovimiento = objk.movimiento,
                                        };
                                        new BCentral().actualizarStockPvt(objStock);
                                    }
                                }
                            }
                            obj.bovd_iid_kardex1 = Convert.ToInt64(idkardex[0]);
                            obj.bovd_iid_kardex2 = Convert.ToInt64(idkardex[1]);
                            obj.bovd_iid_kardex3 = Convert.ToInt64(idkardex[2]);
                            obj.bovd_iid_kardex4 = Convert.ToInt64(idkardex[3]);
                            obj.bovd_iid_kardex5 = Convert.ToInt64(idkardex[4]);
                            obj.bovd_iid_kardex6 = Convert.ToInt64(idkardex[5]);
                            obj.bovd_iid_kardex7 = Convert.ToInt64(idkardex[6]);
                            obj.bovd_iid_kardex8 = Convert.ToInt64(idkardex[7]);
                            obj.bovd_iid_kardex9 = Convert.ToInt64(idkardex[8]);
                            obj.bovd_iid_kardex10 = Convert.ToInt64(idkardex[9]);
                            obj.IdCabecera = intIcodE;
                            insertarboletaVentaElectronicaDetalle(obj);
                            obj.bovc_icod_boleta = objEBoleta.bovc_icod_boleta;
                            insertarBoletaDetalle(obj);
                        };
                    }
                    else
                    {
                        lstBolDet.ForEach(x =>
                        {
                            x.IdCabecera = intIcodE;
                            insertarboletaVentaElectronicaDetalle(x);
                            x.kardc_icod_correlativo = null;
                            x.bovc_icod_boleta = objEBoleta.bovc_icod_boleta;
                            insertarBoletaDetalle(x);
                        });

                    }
                    #endregion

                    if (objEBoleta.remic_icod_remision != 0)
                    {
                        objVentasData.modificarGuiaRemision_Situ_Tipo_Doc(Convert.ToInt32(objEBoleta.remic_icod_remision),
                            //9, //TIPO DE DOCUMENTO
                            //objEBoleta.bovc_icod_boleta, //ICOD_DOCUEMTNO
                            219, //FACTURADO
                            objEBoleta.intUsuario,
                            objEBoleta.strPc);

                    }

                    tx.Complete();
                }
                return objEBoleta.bovc_icod_boleta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private EDocXCobrarPago getPagoDXC(EPagoDocVenta oBePago)
        {
            try
            {
                EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                oBePagoDXC.pdxcc_icod_correlativo = oBePago.pgoc_dxc_icod_pago;
                oBePagoDXC.doxcc_icod_correlativo = oBePago.pgoc_dxc_icod_doc;


                if (oBePago.pgoc_tipo_pago == 1)//EFECTIVO
                {
                    oBePagoDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                    oBePagoDXC.pdxcc_vnumero_doc = oBePago.pgoc_vnumero_planilla;
                }
                if (oBePago.pgoc_tipo_pago == 2)//TARJETA
                {
                    oBePagoDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                    oBePagoDXC.pdxcc_vnumero_doc = oBePago.pgoc_vnumero_planilla;
                }
                if (oBePago.pgoc_tipo_pago == 3)//NOTA DE CREDITO
                {
                    oBePagoDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                    oBePagoDXC.pdxcc_vnumero_doc = oBePago.strNroNotaCredito;
                }
                if (oBePago.pgoc_tipo_pago == 4)//CHEQUE
                    oBePagoDXC.tdocc_icod_tipo_doc = Parametros.intTipoPgoCheque;
                oBePagoDXC.pdxcc_vnumero_doc = oBePago.pgoc_vnumero_planilla;


                if (oBePago.pgoc_tipo_pago == 5)//TRANSFERENCIA BANCARIA
                    oBePagoDXC.tdocc_icod_tipo_doc = Parametros.intTipoPgoTransfBancaria;
                oBePagoDXC.pdxcc_vnumero_doc = oBePago.pgoc_vnumero_planilla;


                if (oBePago.pgoc_tipo_pago == 6)//CREDITO
                    oBePagoDXC.pdxcc_vnumero_doc = oBePago.pgoc_vnumero_planilla;
                if (oBePago.pgoc_tipo_pago == 7)//ANTICIPO
                {
                    oBePagoDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                    oBePagoDXC.pdxcc_vnumero_doc = oBePago.strNroAnticipo;
                }

                oBePagoDXC.pdxcc_sfecha_cobro = oBePago.pgoc_sfecha_pago;
                oBePagoDXC.tablc_iid_tipo_moneda = oBePago.pgoc_icod_tipo_moneda;
                oBePagoDXC.pdxcc_nmonto_cobro = oBePago.pgoc_nmonto;
                oBePagoDXC.pdxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBePago.pgoc_sfecha_pago);
                if (Convert.ToDecimal(oBePagoDXC.pdxcc_nmonto_tipo_cambio) == 0)
                {
                    throw new ArgumentException(String.Format("No existe registro de tipo de cambio para la fecha {0}", oBePago.pgoc_sfecha_pago));
                }
                oBePagoDXC.pdxcc_vobservacion = String.Format("N° PLN VENTA: {0} - {1} ", oBePago.pgoc_vnumero_planilla, oBePago.pgoc_descripcion.ToUpper());
                oBePagoDXC.cliec_icod_cliente = oBePago.pgoc_icod_cliente;
                oBePagoDXC.pdxcc_vorigen = "P";
                oBePagoDXC.intUsuario = oBePago.intUsuario;
                oBePagoDXC.strPc = oBePago.strPc;
                oBePagoDXC.pdxcc_flag_estado = true;

                return oBePagoDXC;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarBoleta(EBoletaCab objEBoleta, List<EBoletaDet> lstBolDet, List<EBoletaDet> lstBolDelete)
        {
            try
            {
                ContabilidadData objContabilidadData = new ContabilidadData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    #region Descripcion
                    int i = 1;
                    string vdescripcion_1erProducto = "";
                    foreach (var _des in lstBolDet)
                    {
                        if (i == 1)
                        {
                            vdescripcion_1erProducto = _des.strDesProducto;
                        }
                    }
                    #endregion

                    //MODIFICAR LA BOLETA
                    objVentasData.modificarBoleta(objEBoleta);

                    #region Factura Det.
                    if (objEBoleta.remic_icod_remision == 0)
                    {
                        foreach (var obj in lstBolDet)
                        {
                            if (obj.operacion == 1)
                            {
                                //insertar kardex
                                object[] Tallas = new object[10];
                                object[] Cantidades = new object[10];
                                object[] idkardex = new object[10];

                                Tallas[0] = obj.bovd_talla1;
                                Tallas[1] = obj.bovd_talla2;
                                Tallas[2] = obj.bovd_talla3;
                                Tallas[3] = obj.bovd_talla4;
                                Tallas[4] = obj.bovd_talla5;
                                Tallas[5] = obj.bovd_talla6;
                                Tallas[6] = obj.bovd_talla7;
                                Tallas[7] = obj.bovd_talla8;
                                Tallas[8] = obj.bovd_talla9;
                                Tallas[9] = obj.bovd_talla10;
                                Cantidades[0] = obj.bovd_cant_talla1;
                                Cantidades[1] = obj.bovd_cant_talla2;
                                Cantidades[2] = obj.bovd_cant_talla3;
                                Cantidades[3] = obj.bovd_cant_talla4;
                                Cantidades[4] = obj.bovd_cant_talla5;
                                Cantidades[5] = obj.bovd_cant_talla6;
                                Cantidades[6] = obj.bovd_cant_talla7;
                                Cantidades[7] = obj.bovd_cant_talla8;
                                Cantidades[8] = obj.bovd_cant_talla9;
                                Cantidades[9] = obj.bovd_cant_talla10;

                                for (int l = 0; l <= 9; l++)
                                {
                                    List<EProdProducto> oProducto = new List<EProdProducto>();
                                    string codigoexterno = obj.bovd_vcodigo_extremo_prod + Tallas[l];
                                    //oProducto = new BCentral().VerificarProdStockProducto(codigoexterno, Convert.ToInt32(obj.almac_icod_almacen), Convert.ToInt32(objEBoleta.puvec_icod_punto_venta));
                                    if (oProducto.Count > 0)
                                    {
                                        if (Convert.ToInt32(Cantidades[l]) != 0)
                                        {
                                            EProdKardex objk = new EProdKardex();
                                            objk.kardc_ianio = Parametros.intEjercicio;
                                            objk.kardx_sfecha = Convert.ToDateTime(objEBoleta.bovc_sfecha_boleta);
                                            objk.iid_almacen = Convert.ToInt32(objEBoleta.bovc_iid_almacen);
                                            objk.iid_producto = oProducto[0].pr_icod_producto;
                                            objk.puvec_icod_punto_venta = objEBoleta.puvec_icod_punto_venta;
                                            objk.Cantidad = Convert.ToDecimal(Cantidades[l]);
                                            // objk.NroNota = Convert.ToInt32(objEFactura.favc_vnumero_factura);
                                            //objk.iid_tipo_doc = Convert.ToInt32(objEBoleta.bovc_);
                                            objk.NroDocumento = objEBoleta.bovc_vnumero_boleta;
                                            objk.movimiento = 1;
                                            //objk.Motivo = Convert.ToInt32(objEFactura.tablc_iid_motivo);
                                            //objk.Beneficiario = objEFactura.salgc_vreferencia;
                                            //objk.Observaciones = objEFactura.salgc_vobservaciones;
                                            objk.intUsuario = objEBoleta.intUsuario;
                                            objk.Item = Convert.ToInt32(obj.bovd_icod_item_boleta);
                                            objk.stocc_codigo_producto = codigoexterno;
                                            objk.operacion = 1;
                                            objk.kardc_iid_correlativo = new BCentral().InsertarKardexpvt(objk);
                                            idkardex[l] = objk.kardc_iid_correlativo;

                                            EProdStockProducto objStock = new EProdStockProducto()
                                            {
                                                stocc_ianio = Parametros.intEjercicio,
                                                stocc_iid_almacen = objk.iid_almacen,
                                                stocc_iid_producto = objk.iid_producto,
                                                stocc_nstock_prod = objk.Cantidad,
                                                intTipoMovimiento = objk.movimiento,
                                            };
                                            new BCentral().actualizarStockPvt(objStock);
                                        }
                                    }
                                }
                                obj.bovd_iid_kardex1 = Convert.ToInt64(idkardex[0]);
                                obj.bovd_iid_kardex2 = Convert.ToInt64(idkardex[1]);
                                obj.bovd_iid_kardex3 = Convert.ToInt64(idkardex[2]);
                                obj.bovd_iid_kardex4 = Convert.ToInt64(idkardex[3]);
                                obj.bovd_iid_kardex5 = Convert.ToInt64(idkardex[4]);
                                obj.bovd_iid_kardex6 = Convert.ToInt64(idkardex[5]);
                                obj.bovd_iid_kardex7 = Convert.ToInt64(idkardex[6]);
                                obj.bovd_iid_kardex8 = Convert.ToInt64(idkardex[7]);
                                obj.bovd_iid_kardex9 = Convert.ToInt64(idkardex[8]);
                                obj.bovd_iid_kardex10 = Convert.ToInt64(idkardex[9]);

                                obj.bovc_icod_boleta = objEBoleta.bovc_icod_boleta;//codigo cabecera
                                insertarBoletaDetalle(obj);

                            }
                            else if (obj.operacion == 2)
                            {
                                List<EProdProducto> oProducto = new List<EProdProducto>();
                                object[] Tallas = new object[10];
                                object[] Cantidades = new object[10];
                                object[] idkardex = new object[10];

                                Tallas[0] = obj.bovd_talla1;
                                Tallas[1] = obj.bovd_talla2;
                                Tallas[2] = obj.bovd_talla3;
                                Tallas[3] = obj.bovd_talla4;
                                Tallas[4] = obj.bovd_talla5;
                                Tallas[5] = obj.bovd_talla6;
                                Tallas[6] = obj.bovd_talla7;
                                Tallas[7] = obj.bovd_talla8;
                                Tallas[8] = obj.bovd_talla9;
                                Tallas[9] = obj.bovd_talla10;
                                Cantidades[0] = obj.bovd_cant_talla1;
                                Cantidades[1] = obj.bovd_cant_talla2;
                                Cantidades[2] = obj.bovd_cant_talla3;
                                Cantidades[3] = obj.bovd_cant_talla4;
                                Cantidades[4] = obj.bovd_cant_talla5;
                                Cantidades[5] = obj.bovd_cant_talla6;
                                Cantidades[6] = obj.bovd_cant_talla7;
                                Cantidades[7] = obj.bovd_cant_talla8;
                                Cantidades[8] = obj.bovd_cant_talla9;
                                Cantidades[9] = obj.bovd_cant_talla10;

                                idkardex[0] = obj.bovd_iid_kardex1;
                                idkardex[1] = obj.bovd_iid_kardex2;
                                idkardex[2] = obj.bovd_iid_kardex3;
                                idkardex[3] = obj.bovd_iid_kardex4;
                                idkardex[4] = obj.bovd_iid_kardex5;
                                idkardex[5] = obj.bovd_iid_kardex6;
                                idkardex[6] = obj.bovd_iid_kardex7;
                                idkardex[7] = obj.bovd_iid_kardex8;
                                idkardex[8] = obj.bovd_iid_kardex9;
                                idkardex[9] = obj.bovd_iid_kardex10;
                                for (int l = 0; l <= 9; l++)
                                {
                                    string codigoexterno = obj.bovd_vcodigo_extremo_prod + Tallas[l];
                                    //oProducto = new BCentral().VerificarProdStockProducto(codigoexterno, Convert.ToInt32(obj.almac_icod_almacen), Convert.ToInt32(objEBoleta.puvec_icod_punto_venta));
                                    if (oProducto.Count > 0)
                                    {
                                        EProdKardex objk = new EProdKardex();
                                        if (Convert.ToInt32(Cantidades[l]) == 0)
                                        {
                                            objk.operacion = 3;
                                        }
                                        else
                                        {
                                            objk.operacion = 2;
                                        }
                                        long? kardAux = 0;

                                        objk.kardc_ianio = Parametros.intEjercicio;
                                        objk.kardx_sfecha = Convert.ToDateTime(objEBoleta.bovc_sfecha_boleta);
                                        objk.iid_almacen = Convert.ToInt32(objEBoleta.bovc_iid_almacen);
                                        objk.iid_producto = oProducto[0].pr_icod_producto;
                                        objk.puvec_icod_punto_venta = objEBoleta.puvec_icod_punto_venta;
                                        objk.Cantidad = Convert.ToDecimal(Cantidades[l]);
                                        //objk.NroNota = Convert.ToInt32(objNotaSalida.salgc_inumero_nota_salida);
                                        //objk.iid_tipo_doc = Convert.ToInt32(objEFactura.favc_tipo_factura);
                                        objk.NroDocumento = objEBoleta.bovc_vnumero_boleta;
                                        objk.movimiento = 1;
                                        //objk.Motivo = Convert.ToInt32(objNotaSalida.tablc_iid_motivo);
                                        //objk.Beneficiario = objNotaSalida.salgc_vreferencia;
                                        //objk.Observaciones = objNotaSalida.salgc_vobservaciones;
                                        objk.intUsuario = objEBoleta.intUsuario;
                                        objk.Item = Convert.ToInt32(obj.bovd_icod_item_boleta);
                                        objk.stocc_codigo_producto = codigoexterno;
                                        objk.kardc_iid_correlativo = Convert.ToInt32(idkardex[l]);
                                        kardAux = new BCentral().InsertarKardexpvt(objk);
                                        if (kardAux != objk.kardc_iid_correlativo)
                                        {
                                            idkardex[l] = kardAux;
                                        }
                                        EProdStockProducto objStock = new EProdStockProducto()
                                        {
                                            stocc_ianio = Parametros.intEjercicio,
                                            stocc_iid_almacen = objk.iid_almacen,
                                            stocc_iid_producto = objk.iid_producto,
                                            stocc_nstock_prod = objk.Cantidad,
                                            intTipoMovimiento = objk.movimiento,
                                        };
                                        new BCentral().actualizarStockPvt(objStock);
                                    }
                                }
                                obj.bovd_iid_kardex1 = Convert.ToInt64(idkardex[0]);
                                obj.bovd_iid_kardex2 = Convert.ToInt64(idkardex[1]);
                                obj.bovd_iid_kardex3 = Convert.ToInt64(idkardex[2]);
                                obj.bovd_iid_kardex4 = Convert.ToInt64(idkardex[3]);
                                obj.bovd_iid_kardex5 = Convert.ToInt64(idkardex[4]);
                                obj.bovd_iid_kardex6 = Convert.ToInt64(idkardex[5]);
                                obj.bovd_iid_kardex7 = Convert.ToInt64(idkardex[6]);
                                obj.bovd_iid_kardex8 = Convert.ToInt64(idkardex[7]);
                                obj.bovd_iid_kardex9 = Convert.ToInt64(idkardex[8]);
                                obj.bovd_iid_kardex10 = Convert.ToInt64(idkardex[9]);
                                modificarBoletaDetalle(obj);
                            }

                        }

                        foreach (var objelimi in lstBolDelete)
                        {
                            EProdKardex ekar = new EProdKardex();
                            int[] icod_karde = new int[10];
                            long[] talla = new long[10];
                            List<EProdProducto> oProducto = new List<EProdProducto>();
                            //KARDEX
                            ekar.kardc_ianio = Parametros.intEjercicio;
                            ekar.kardx_sfecha = DateTime.Today;
                            ekar.iid_almacen = Convert.ToInt32(objEBoleta.bovc_iid_almacen);
                            ekar.iid_producto = Convert.ToInt32(objelimi.prdc_icod_producto);
                            ekar.movimiento = 1;
                            ekar.operacion = 3;
                            icod_karde[0] = Convert.ToInt32(objelimi.bovd_iid_kardex1);
                            icod_karde[1] = Convert.ToInt32(objelimi.bovd_iid_kardex2);
                            icod_karde[2] = Convert.ToInt32(objelimi.bovd_iid_kardex3);
                            icod_karde[3] = Convert.ToInt32(objelimi.bovd_iid_kardex4);
                            icod_karde[4] = Convert.ToInt32(objelimi.bovd_iid_kardex5);
                            icod_karde[5] = Convert.ToInt32(objelimi.bovd_iid_kardex6);
                            icod_karde[6] = Convert.ToInt32(objelimi.bovd_iid_kardex7);
                            icod_karde[7] = Convert.ToInt32(objelimi.bovd_iid_kardex8);
                            icod_karde[8] = Convert.ToInt32(objelimi.bovd_iid_kardex9);
                            icod_karde[9] = Convert.ToInt32(objelimi.bovd_iid_kardex10);
                            talla[0] = Convert.ToInt32(objelimi.bovd_talla1);
                            talla[1] = Convert.ToInt32(objelimi.bovd_talla2);
                            talla[2] = Convert.ToInt32(objelimi.bovd_talla3);
                            talla[3] = Convert.ToInt32(objelimi.bovd_talla4);
                            talla[4] = Convert.ToInt32(objelimi.bovd_talla5);
                            talla[5] = Convert.ToInt32(objelimi.bovd_talla6);
                            talla[6] = Convert.ToInt32(objelimi.bovd_talla7);
                            talla[7] = Convert.ToInt32(objelimi.bovd_talla8);
                            talla[8] = Convert.ToInt32(objelimi.bovd_talla9);
                            talla[9] = Convert.ToInt32(objelimi.bovd_talla10);
                            for (int j = 0; j <= 9; j++)
                            {
                                string codigoexterno = objelimi.bovd_vcodigo_extremo_prod + talla[j].ToString();
                                //oProducto = new BCentral().VerificarProdStockProducto(codigoexterno, Convert.ToInt32(objEBoleta.bovc_iid_almacen), Convert.ToInt32(objEBoleta.puvec_icod_punto_venta));
                                ekar.kardc_iid_correlativo = icod_karde[j];
                                if (oProducto.Count > 0)
                                {
                                    ekar.puvec_icod_punto_venta = Convert.ToInt32(objEBoleta.puvec_icod_punto_venta);
                                    ekar.iid_producto = oProducto[0].pr_icod_producto;
                                    new BCentral().InsertarKardexpvt(ekar);
                                }
                            }
                            eliminarBoletaDetalle(objelimi);
                        }
                    }
                    else
                    {
                        lstBolDet.ForEach(x =>
                        {
                            if (x.intTipoOperacion == 1)
                            {
                                x.almac_icod_almacen = 0;
                                x.kardc_icod_correlativo = null;
                                x.bovc_icod_boleta = objEBoleta.bovc_icod_boleta;
                                objVentasData.insertarBoletaDetalle(x);
                            }
                            else if (x.intTipoOperacion == 2)
                            {
                                objVentasData.modificarBoletaDetalle(x);

                            }
                        });

                    }
                    #endregion
                    #region Doc. Por Cobrar de la Factura
                    //INSERTAR LA BOLETA EN DOC. POR COBRAR
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = objEBoleta.doxcc_icod_correlativo;
                    objDXC.mesec_iid_mes = Convert.ToInt16(objEBoleta.bovc_sfecha_boleta.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                    objDXC.tdodc_iid_correlativo = 9;
                    objDXC.doxcc_vnumero_doc = objEBoleta.bovc_vnumero_boleta;
                    objDXC.cliec_icod_cliente = objEBoleta.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = objEBoleta.cliec_vnombre_cliente;
                    objDXC.doxcc_sfecha_doc = objEBoleta.bovc_sfecha_boleta;
                    objDXC.doxcc_sfecha_vencimiento_doc = objEBoleta.bovc_sfecha_vencim_boleta;
                    objDXC.tablc_iid_tipo_moneda = objEBoleta.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(objEBoleta.bovc_sfecha_boleta);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la boleta, favor de registrar Tipo de Cambio");
                    objDXC.tablc_iid_tipo_pago = objEBoleta.tablc_iid_forma_pago;
                    objDXC.doxcc_vdescrip_transaccion = "";//***********>>>>>>>
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(objEBoleta.bovc_npor_imp_igv) > 0) ? objEBoleta.bovc_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(objEBoleta.bovc_npor_imp_igv) == 0) ? objEBoleta.bovc_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = objEBoleta.bovc_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = objEBoleta.bovc_nmonto_imp;
                    objDXC.doxcc_nmonto_total = objEBoleta.bovc_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = objEBoleta.bovc_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = objEBoleta.tablc_iid_situacion;
                    objDXC.doxcc_vobservaciones = vdescripcion_1erProducto;
                    objDXC.doxc_bind_cuenta_corriente = false;
                    objDXC.doxcc_sfecha_entrega = null;
                    objDXC.doxcc_bind_impresion_nogerencia = false;
                    objDXC.doxc_bind_situacion_legal = false;
                    objDXC.doxc_bind_cierre_cuenta_corriente = false;
                    objDXC.intUsuario = objEBoleta.intUsuario;
                    objDXC.strPc = objEBoleta.strPc;
                    objDXC.doxcc_tipo_comprobante_referencia = 0;
                    objDXC.doxcc_num_serie_referencia = "";
                    objDXC.doxcc_num_comprobante_referencia = "";
                    objDXC.doxcc_sfecha_emision_referencia = null;
                    //objDXC.docxc_icod_documento = objEBoleta.bovc_icod_boleta;
                    objDXC.anio = objEBoleta.bovc_sfecha_boleta.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "V";
                    objDXC.puvec_icod_punto_venta = objEBoleta.bovc_icod_pvt;
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    new CuentasPorCobrarData().modificarDxc(objDXC, Lista, Lista);
                    #endregion

                    List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == objEBoleta.bovc_icod_boleta).ToList();

                    if (lstCab.Count > 0)
                    {
                        objEBoleta.IdCabecera = lstCab[0].IdCabecera;
                        objVentasData.modificarboletaVentaElectronica(objEBoleta);
                        new BVentas().eliminarFacturaVentaElectronicaDetalle(lstCab[0].IdCabecera);
                        foreach (var ob in lstBolDet)
                        {
                            ob.IdCabecera = lstCab[0].IdCabecera;
                            insertarboletaVentaElectronicaDetalle(ob);
                        }
                    }

                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int verificarDocVentaPlanilla(int intTipoDoc, int intIcodDoc)
        {
            int intFlag = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intFlag = objVentasData.verificarDocVentaPlanilla(intTipoDoc, intIcodDoc);
                    tx.Complete();
                }
                return intFlag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AnularBoletaVenta(EBoletaCab objEFactura)
        {
            #region Eliminar DXC
            EDocXCobrar objDXC = new EDocXCobrar();
            objDXC.doxcc_icod_correlativo = objEFactura.doxcc_icod_correlativo;
            objDXC.intUsuario = objEFactura.intUsuario;
            objDXC.strPc = objEFactura.strPc;
            new CuentasPorCobrarData().AnularDocumentoXCobrar(objDXC);
            #endregion Eliminar DXC

            List<EBoletaDet> Mlist = new List<EBoletaDet>();
            Mlist = listarBoletaDetalle(objEFactura.bovc_icod_boleta);
            foreach (var x in Mlist)
            {

                #region Eliminar Kardex
                EKardex obKardexDel = new EKardex();
                obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                obKardexDel.intUsuario = objEFactura.intUsuario;
                obKardexDel.strPc = objEFactura.strPc;
                objAlmacenData.eliminarKardex(obKardexDel);
                #endregion
                #region Actualizando Stock
                EStock stck = new EStock();
                stck.stocc_ianio = Parametros.intEjercicio;
                stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                stck.stocc_stock_producto = x.bovd_ncantidad;
                stck.intTipoMovimiento = 0;
                objAlmacenData.actualizarStock(stck);
                #endregion

                eliminarBoletaDetalle(x);
            }

            if (objEFactura.remic_icod_remision != 0)
            {
                objVentasData.modificarGuiaRemision_Situ_Tipo_Doc(Convert.ToInt32(objEFactura.remic_icod_remision),
                    //0, //TIPO DE DOCUMENTO
                    //0, //ICOD_DOCUEMTNO
                    218, //generado
                    objEFactura.intUsuario,
                    objEFactura.strPc);

            }
            objVentasData.anularBoleta(objEFactura);
        }

        public void anularBoleta(EBoletaCab objEBoleta)
        {
            try
            {
                CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();
                TesoreriaData objTesoreriaData = new TesoreriaData();

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    var lstPagosAux = new BVentas().listarPago(Parametros.intTipoDocBoletaVenta, objEBoleta.bovc_icod_boleta);
                    lstPagosAux.ForEach(x =>
                    {
                        if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL MOVIMIENTO DEL BANCO
                            objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL PAGO DE LA NOTA DE CREDITO
                            EDocXCobrarPago oBePagoDXCAnt = new EDocXCobrarPago();
                            oBePagoDXCAnt.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXCAnt.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_icod_nota_credito);
                            oBePagoDXCAnt.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXCAnt.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_icod_nota_credito), 0);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL PAGO DEL ANTICIPO
                            EAdelantoPago oBePagoDXCAnt = new EAdelantoPago();
                            oBePagoDXCAnt.adpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXCAnt.doxcc_icod_correlativo_adelanto = Convert.ToInt64(x.pgoc_icod_anticipo);
                            oBePagoDXCAnt.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXCAnt.strPc = objEBoleta.strPc;
                            new CuentasPorCobrarData().eliminarAdelantoPago(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(Convert.ToInt64(x.pgoc_icod_anticipo), 0);
                        }
                        //FINALMENTE SE ELIMINA EL PAGO DEL DOC. DE VENTA (EN ESTE CASO DE LA FACTURA)
                        objVentasData.eliminarPago(x);
                    });
                    List<EBoletaDet> Mlist = new List<EBoletaDet>();
                    Mlist = listarBoletaDetalle(objEBoleta.bovc_icod_boleta);
                    foreach (var x in Mlist)
                    {

                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                        obKardexDel.intUsuario = objEBoleta.intUsuario;
                        obKardexDel.strPc = objEBoleta.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        #endregion
                        #region Actualizando Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = objEBoleta.bovc_sfecha_boleta.Year;
                        stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        stck.stocc_stock_producto = x.bovd_ncantidad;
                        stck.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stck);

                        #endregion
                        objVentasData.eliminarBoletaDetalle(x);
                    }
                    objVentasData.anularBoleta(objEBoleta);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarBoletaVenta(EBoletaCab objEBoleta)
        {
            try
            {
                CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();
                TesoreriaData objTesoreriaData = new TesoreriaData();

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region Eliminar DXC
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = objEBoleta.doxcc_icod_correlativo;
                    objDXC.intUsuario = objEBoleta.intUsuario;
                    objDXC.strPc = objEBoleta.strPc;
                    new CuentasPorCobrarData().eliminarDxc(objDXC);
                    #endregion Eliminar DXC
                    List<EBoletaDet> Mlist = new List<EBoletaDet>();
                    Mlist = listarBoletaDetalle(objEBoleta.bovc_icod_boleta);
                    foreach (var x in Mlist)
                    {

                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                        obKardexDel.intUsuario = objEBoleta.intUsuario;
                        obKardexDel.strPc = objEBoleta.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        #endregion
                        #region Actualizando Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = Parametros.intEjercicio;
                        stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        stck.stocc_stock_producto = x.bovd_ncantidad;
                        stck.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stck);
                        #endregion

                        eliminarBoletaDetalle(x);
                    }

                    if (objEBoleta.remic_icod_remision != 0)
                    {
                        objVentasData.modificarGuiaRemision_Situ_Tipo_Doc(Convert.ToInt32(objEBoleta.remic_icod_remision),
                            //0, //TIPO DE DOCUMENTO
                            //0, //ICOD_DOCUEMTNO
                            218, //FACTURADO
                            objEBoleta.intUsuario,
                            objEBoleta.strPc);

                    }

                    objVentasData.eliminarBoleta(objEBoleta);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarBoleta(EBoletaCab objEBoleta, EPlanillaCobranzaDet _BE_plani)
        {
            try
            {
                CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();
                TesoreriaData objTesoreriaData = new TesoreriaData();

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    var lstPagosAux = new BVentas().listarPago(Parametros.intTipoDocBoletaVenta, objEBoleta.bovc_icod_boleta);
                    lstPagosAux.ForEach(x =>
                    {
                        if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL MOVIMIENTO DEL BANCO
                            objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL PAGO DE LA NOTA DE CREDITO
                            EDocXCobrarPago oBePagoDXCAnt = new EDocXCobrarPago();
                            oBePagoDXCAnt.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXCAnt.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_icod_nota_credito);
                            oBePagoDXCAnt.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXCAnt.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_icod_nota_credito), 0);
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL MOVIMIENTO DEL BANCO
                            objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));

                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL MOVIMIENTO DEL BANCO
                            objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                        }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                        { }
                        else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                        {
                            //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                            EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                            oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                            oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                            oBePagoDXC.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXC.strPc = objEBoleta.strPc;
                            objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                            //SE ELIMINA EL PAGO DEL ANTICIPO
                            EAdelantoPago oBePagoDXCAnt = new EAdelantoPago();
                            oBePagoDXCAnt.adpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                            oBePagoDXCAnt.doxcc_icod_correlativo_adelanto = Convert.ToInt64(x.pgoc_icod_anticipo);
                            oBePagoDXCAnt.intUsuario = objEBoleta.intUsuario;
                            oBePagoDXCAnt.strPc = objEBoleta.strPc;
                            new CuentasPorCobrarData().eliminarAdelantoPago(oBePagoDXCAnt);
                            objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(Convert.ToInt64(x.pgoc_icod_anticipo), 0);
                        }
                        //FINALMENTE SE ELIMINA EL PAGO DEL DOC. DE VENTA (EN ESTE CASO DE LA FACTURA)
                        objVentasData.eliminarPago(x);
                    });

                    //objVentasData.eliminarBoleta(objEBoleta);


                    EDocXCobrar _DXC = new EDocXCobrar();
                    _DXC.doxcc_icod_correlativo = objEBoleta.doxcc_icod_correlativo;
                    _DXC.intUsuario = objEBoleta.intUsuario;
                    _DXC.strPc = objEBoleta.strPc;
                    new CuentasPorCobrarData().EliminarDocumentoXCobrar(_DXC);


                    List<EBoletaDet> Mlist = new List<EBoletaDet>();
                    Mlist = listarBoletaDetalle(objEBoleta.bovc_icod_boleta);
                    foreach (var x in Mlist)
                    {

                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                        obKardexDel.intUsuario = objEBoleta.intUsuario;
                        obKardexDel.strPc = objEBoleta.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        #endregion
                        #region Actualizando Stock
                        EStock stck = new EStock();
                        stck.stocc_ianio = objEBoleta.bovc_sfecha_boleta.Year;
                        stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        stck.stocc_stock_producto = x.bovd_ncantidad;
                        stck.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stck);

                        #endregion
                        objVentasData.eliminarBoletaDetalle(x);
                    }

                    objVentasData.eliminarBoleta(objEBoleta);
                    if (_BE_plani != null)
                        objVentasData.eliminarPlanillaCobranzaDetalle(_BE_plani);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EBoletaDet> listarBoletaDetalle(int intBoleta)
        {
            List<EBoletaDet> lista = new List<EBoletaDet>();
            try
            {
                lista = objVentasData.listarBoletaDetalle(intBoleta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarBoletaDetalle(EBoletaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.insertarBoletaDetalle(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarBoletaDetalle(EBoletaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarBoletaDetalle(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarBoletaDetalle(EBoletaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarBoletaDetalle(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarAfecto(int bovc_icod_boleta, bool bfavc_bafecto_igv)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarAfecto(bovc_icod_boleta, bfavc_bafecto_igv);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
        #region Venta Directa
        public List<EVentaDirecta> listarVentaDirecta(int intEjericio)
        {
            List<EVentaDirecta> lista = new List<EVentaDirecta>();
            try
            {
                lista = objVentasData.listarVentaDirecta(intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarVentaDirecta(EVentaDirecta obj, List<EVentaDirectaDet> lstDetalle)
        {
            int id_doc = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    id_doc = objVentasData.insertarVentaDirecta(obj);
                    lstDetalle.ForEach(x =>
                    {
                        x.dvdc_icod_doc_venta_directa = id_doc;
                        objVentasData.insertarVentaDirectaDetalle(x);
                    });
                    tx.Complete();
                }
                return id_doc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarVentaDirecta(EVentaDirecta obj, List<EVentaDirectaDet> lstDetalle, List<EVentaDirectaDet> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarVentaDirecta(obj);
                    //
                    lstDelete.ForEach(x =>
                    {
                        objVentasData.eliminarVentaDirectaDetalle(x);
                    });
                    //
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            x.dvdc_icod_doc_venta_directa = obj.dvdc_icod_doc_venta_directa;
                            objVentasData.insertarVentaDirectaDetalle(x);
                        }
                        else if (x.intTipoOperacion == 2)
                            objVentasData.modificarVentaDirectaDetalle(x);
                    });
                    //
                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarVentaDirecta(EVentaDirecta obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarVentaDirecta(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EVentaDirectaDet> listarVentaDirectaDetalle(int intVentaDirecta)
        {
            List<EVentaDirectaDet> lista = new List<EVentaDirectaDet>();
            try
            {
                lista = objVentasData.listarVentaDirectaDetalle(intVentaDirecta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarVentaDirectaDetalle(EVentaDirectaDet objEFactura)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.insertarVentaDirectaDetalle(objEFactura);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarVentaDirectaDetalle(EVentaDirectaDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarVentaDirectaDetalle(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarVentaDirectaDetalle(EVentaDirectaDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarVentaDirectaDetalle(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Pagos de Documentos de Venta
        public List<EPagoDocVenta> listarPago(int intTipoDoc, int intIcodDoc)
        {
            List<EPagoDocVenta> lista = new List<EPagoDocVenta>(); ;
            try
            {
                lista = objVentasData.listarPago(intTipoDoc, intIcodDoc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EPagoDocVenta> getDatosPago(int intPago)
        {
            List<EPagoDocVenta> lista = new List<EPagoDocVenta>(); ;
            try
            {
                lista = objVentasData.getDatosPago(intPago);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int insertarPago(EPagoDocVenta oBe)
        {
            int idPago = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    idPago = objVentasData.insertarPago(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return idPago;

        }
        public void modificarPago(EPagoDocVenta oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarPago(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarPago(EPagoDocVenta oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarPago(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Planilla de Cobranza
        public List<EPlanillaCobranzaCab> listarPlanillaCobranzaCab(int intEjercicio)
        {
            List<EPlanillaCobranzaCab> lista = new List<EPlanillaCobranzaCab>();
            try
            {
                lista = objVentasData.listarPlanillaCobranzaCab(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void cerrarPlanilla(ELibroBancos oBe1, ELibroBancos oBe2, int intPlanilla)
        {

            int? intIcodMovSol = null;
            int? intIcodMovDol = null;

            decimal intSoles = 0;
            decimal intDolares = 0;
            DateTime? fecha = null;

            if (oBe1 != null)
            {
                intDolares = 0;
                intSoles = oBe1.nmonto_movimiento;
                fecha = oBe1.dfecha_movimiento;
            }
            else
            {
                intSoles = 0;
                intDolares = oBe2.nmonto_movimiento_dolares;
                fecha = oBe2.dfecha_movimiento;
            }


            using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
            {
                /************************************************************************/
                if (oBe1 != null)
                    intIcodMovSol = objTesoreriaDara.InsertarMovimientoBancos(oBe1);
                /************************************************************************/
                if (oBe2 != null)
                    intIcodMovDol = objTesoreriaDara.InsertarMovimientoBancos(oBe2);
                /************************************************************************/
                objVentasData.actualizarIcodMovimientoPlanilla(intPlanilla, intIcodMovSol, intIcodMovDol, intSoles, intDolares, fecha);
                /************************************************************************/
                tx.Complete();
            }
        }

        public void revertirCierre(int intPlanilla)
        {
            using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
            {
                objVentasData.revertirCierre(intPlanilla);
                tx.Complete();
            }
        }

        public Tuple<decimal, decimal> getTotalEfectivoPlanilla(int intPlanilla)
        {
            try
            {
                decimal dcmlEfectivoSol = 0;
                decimal dcmlEfectivoDol = 0;
                dcmlEfectivoSol = objVentasData.getTotalEfectivoPlanilla(intPlanilla).Item1;
                dcmlEfectivoDol = objVentasData.getTotalEfectivoPlanilla(intPlanilla).Item2;
                return new Tuple<decimal, decimal>(dcmlEfectivoSol, dcmlEfectivoDol);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<EPlanillaCobranzaDet> listarPlanillaCobranzaDetalle(int intPlanilla)
        {
            List<EPlanillaCobranzaDet> lista = new List<EPlanillaCobranzaDet>();
            try
            {
                lista = objVentasData.listarPlanillaCobranzaDetalle(intPlanilla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EPlanillaCobranzaDet> listarPlanillaCobranzaImpresionDetalle(int intPlanilla)
        {
            List<EPlanillaCobranzaDet> lista = new List<EPlanillaCobranzaDet>();
            try
            {
                lista = objVentasData.listarPlanillaCobranzaImpresionDetalle(intPlanilla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void modificarPlanillaCab(EPlanillaCobranzaCab oBePlnCab)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarPlanillaCobranzaCab(oBePlnCab);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public void eliminarPlanillaCab(EPlanillaCobranzaCab obeplacab)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarPlanillaCobranzaCab(obeplacab);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Pagos desde Planilla de Cobranza
        public int insertarPagoPln(EPlanillaCobranzaCab oBePlnCab, EPlanillaCobranzaDet oBePlnDet, EPagoDocVenta oBePgo,
            EDocXCobrar oBeDXC)
        {
            try
            {
                EFacturaCab objFactCab = new EFacturaCab();

                ContabilidadData objContabilidadData = new ContabilidadData();
                TesoreriaData objTesoreriaData = new TesoreriaData();
                CuentasPorCobrarData CuentaCobrarData = new CuentasPorCobrarData();

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                    {
                        #region DXP Pago
                        //EL PAGO ES EN EFECTIVO, SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(oBePgo);
                        oBePgo.pgoc_dxc_icod_pago = CuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);
                        #endregion
                    }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                    {
                        #region Tarjeta Credito
                        //EL PAGO ES EN TARJETA DE CRED., SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(oBePgo);
                        oBePgo.pgoc_dxc_icod_pago = CuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);
                        //EL PAGO ES EN TARJETA DE CRED., DEBE SER INGRESADO EN BANCOS
                        var lstTipoTarjeta = listarTipoTarjeta();
                        ETipoTarjeta oBeTipoTarjeta = lstTipoTarjeta.Where(d => d.tcrc_icod_tipo_tarjeta_cred == Convert.ToInt32(oBePgo.pgoc_icod_tipo_tarjeta)).ToList()[0];
                        //CABECERA DEL MOV. DE BANCOS
                        ELibroBancos oBeBcoMovCab = new ELibroBancos();
                        oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                        oBeBcoMovCab.iid_mes = oBePgo.pgoc_sfecha_pago.Month;
                        oBeBcoMovCab.dfecha_movimiento = oBePgo.pgoc_sfecha_pago;
                        oBeBcoMovCab.icod_enti_financiera_cuenta = oBeTipoTarjeta.bcod_icod_banco_cuenta;
                        oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                        oBeBcoMovCab.vglosa = "PAGO CON TARJETA";
                        oBeBcoMovCab.vdescripcion_beneficiario = oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred;
                        oBeBcoMovCab.iid_tipo_moneda = oBePgo.pgoc_icod_tipo_moneda;
                        oBeBcoMovCab.cliec_icod_cliente = oBePgo.intCliente;
                        oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePgo.pgoc_sfecha_pago);
                        oBeBcoMovCab.nmonto_movimiento = oBePgo.pgoc_nmonto;
                        oBeBcoMovCab.nmonto_saldo_banco = 0;
                        oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                        oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                        oBeBcoMovCab.vnro_documento = String.Format("{0:000}-{1:00}", Convert.ToInt32(oBePlnCab.plnc_vnumero_planilla), oBeTipoTarjeta.tcrc_iid_tipo_tarjeta_cred);
                        oBeBcoMovCab.cflag_conciliacion = false;
                        oBeBcoMovCab.iusuario_crea = oBePgo.intUsuario;
                        oBeBcoMovCab.vpc_crea = oBePgo.strPc;
                        oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                        oBeBcoMovCab.mobac_flag_estado = true;
                        oBeBcoMovCab.TipoDocumento = "PVD";
                        oBeBcoMovCab.mobac_origen_regitro = "PLN";
                        oBePgo.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                        oBeBcoMovCab.icod_correlativo = Convert.ToInt32(oBePgo.pgoc_icod_entidad_finan_mov);
                        //DETALLE DEL MOV. BANCOS
                        ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                        oBeBcoMovDet.mobdc_iid_anio = Parametros.intEjercicio;
                        oBeBcoMovDet.mobdc_iid_mes_periodo = Convert.ToDateTime(oBeDXC.doxcc_sfecha_doc).Month;
                        oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                        oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                        oBeBcoMovDet.vnumero_doc = oBeDXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.doxcc_vnumero_doc = oBeDXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.mobdc_icod_cliente = oBeDXC.cliec_icod_cliente;
                        oBeBcoMovDet.mto_mov_soles = oBePgo.pgoc_nmonto;
                        oBeBcoMovDet.mto_mov_dolar = Math.Round(oBePgo.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                        oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                        oBeBcoMovDet.iusuario_crea = oBePgo.intUsuario;
                        oBeBcoMovDet.vpc_crea = oBePgo.strPc;
                        oBeBcoMovDet.mobdc_flag_estado = true;
                        oBeBcoMovDet.doxcc_icod_correlativo = oBeDXC.doxcc_icod_correlativo;
                        oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                        oBeBcoMovDet.docxc_icod_pago = oBePgo.pgoc_dxc_icod_pago;
                        //SE INSERTAR EL DETALLE
                        objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                        //ACTUALIZAR LOS SALDOS DEL DXC
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);
                        #endregion
                    }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                    {

                        #region Ingreso del pago Dxp Pago
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(oBePgo);
                        oBePgo.pgoc_dxc_icod_pago = CuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        #endregion
                        #region Insertar Pago Nota de Credito
                        //EL PAGO CON LA NOTA DE CREDITO
                        ENotaCreditoPago oBe = new ENotaCreditoPago();
                        oBe.doxcc_icod_correlativo_pago = oBePgo.pgoc_dxc_icod_doc; //el documento a pagar
                        oBe.doxcc_icod_correlativo_nota_credito = Convert.ToInt64(oBePgo.pgoc_icod_nota_credito); //correlativo de la nota de crédito   
                        oBe.tablc_iid_tipo_moneda = Convert.ToInt32(oBePgo.pgoc_icod_tipo_moneda); //debe grabarse con el tipo de moneda del documento nota de crédito
                        oBe.ncpac_nmonto_pago = Convert.ToDecimal(oBePgo.pgoc_nmonto); //monto que se va a pagar guardado con el tipo de moneda del documento nota de crédito
                        oBe.ncpac_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePlnDet.plnd_sfecha_doc); //tipo de cambio de la fecha seleccionada
                        oBe.ncpac_vdescripcion = String.Format("N° PLN VENTA: {0}", oBePlnCab.plnc_vnumero_planilla);
                        oBe.ncpac_sfecha_pago = Convert.ToDateTime(oBePlnDet.plnd_sfecha_doc); //fecha del pago
                        oBe.ncpac_iorigen = "P"; //Nota de credito
                        oBe.ncpac_flag_estado = true;
                        oBe.ncpac_iusuario_crea = oBePgo.intUsuario;
                        oBe.ncpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                        oBe.ncpac_iusuario_modifica = oBePgo.intUsuario;
                        oBe.ncpac_vpc_modifica = WindowsIdentity.GetCurrent().Name;
                        oBe.pdxcc_icod_correlativo = oBePgo.pgoc_dxc_icod_pago;
                        oBePgo.pgoc_dxc_icod_canje_doc = new CuentasPorCobrarData().insertarNCPago(oBe);

                        #endregion
                        #region Actualizacion de Estados
                        TesoreriaData objTesoreriaData1 = new TesoreriaData();
                        objTesoreriaData1.ActualizarMontoPagadoSaldoNotaCreditoCliente(oBe.doxcc_icod_correlativo_nota_credito, 0);
                        objTesoreriaData1.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(oBePgo.pgoc_dxc_icod_doc), 0);
                        #endregion
                    }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                    {
                        #region Pago Cheque
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(oBePgo);
                        oBePgo.pgoc_dxc_icod_pago = CuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);

                        ELibroBancos oBeBcoMovCab = new ELibroBancos();
                        oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                        oBeBcoMovCab.iid_mes = oBePgo.pgoc_sfecha_pago.Month;
                        oBeBcoMovCab.dfecha_movimiento = oBePgo.pgoc_sfecha_pago;
                        oBeBcoMovCab.icod_enti_financiera_cuenta = Convert.ToInt32(oBePgo.bcod_icod_banco_cuenta);
                        oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                        oBeBcoMovCab.vglosa = "PAGO CHEQUE";
                        oBeBcoMovCab.vdescripcion_beneficiario = Convert.ToString(oBeDXC.cliec_vnombre_cliente);
                        oBeBcoMovCab.iid_tipo_moneda = oBePgo.pgoc_icod_tipo_moneda;
                        oBeBcoMovCab.cliec_icod_cliente = oBePgo.intCliente;
                        oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePgo.pgoc_sfecha_pago);
                        oBeBcoMovCab.nmonto_movimiento = oBePgo.pgoc_nmonto;
                        oBeBcoMovCab.nmonto_saldo_banco = 0;
                        oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                        oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                        oBeBcoMovCab.vnro_documento = String.Format("{0:000}", Convert.ToInt32(oBePlnCab.plnc_vnumero_planilla));
                        oBeBcoMovCab.cflag_conciliacion = false;
                        oBeBcoMovCab.iusuario_crea = oBePgo.intUsuario;
                        oBeBcoMovCab.vpc_crea = oBePgo.strPc;
                        oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                        oBeBcoMovCab.mobac_flag_estado = true;
                        oBeBcoMovCab.TipoDocumento = "CHQ";
                        oBeBcoMovCab.mobac_origen_regitro = "PLN";
                        oBePgo.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                        oBeBcoMovCab.icod_correlativo = Convert.ToInt32(oBePgo.pgoc_icod_entidad_finan_mov);
                        //DETALLE DEL MOV. BANCOS
                        ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                        oBeBcoMovDet.mobdc_iid_anio = Parametros.intEjercicio;
                        oBeBcoMovDet.mobdc_iid_mes_periodo = Convert.ToDateTime(oBeDXC.doxcc_sfecha_doc).Month;
                        oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                        oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                        oBeBcoMovDet.vnumero_doc = oBeDXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.doxcc_vnumero_doc = oBeDXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.mobdc_icod_cliente = oBeDXC.cliec_icod_cliente;
                        oBeBcoMovDet.mto_mov_soles = oBePgo.pgoc_nmonto;
                        oBeBcoMovDet.mto_mov_dolar = Math.Round(oBePgo.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                        //oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                        oBeBcoMovDet.iusuario_crea = oBePgo.intUsuario;
                        oBeBcoMovDet.vpc_crea = oBePgo.strPc;
                        oBeBcoMovDet.mobdc_flag_estado = true;
                        oBeBcoMovDet.doxcc_icod_correlativo = oBeDXC.doxcc_icod_correlativo;
                        oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                        oBeBcoMovDet.docxc_icod_pago = oBePgo.pgoc_dxc_icod_pago;
                        //SE INSERTAR EL DETALLE
                        objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                        //ACTUALIZAR LOS SALDOS DEL DXC
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);
                        #endregion
                    }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                    {

                        #region Tranferencia Bancaria
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(oBePgo);
                        oBePgo.pgoc_dxc_icod_pago = CuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);

                        ELibroBancos oBeBcoMovCab = new ELibroBancos();
                        oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                        oBeBcoMovCab.iid_mes = oBePgo.pgoc_sfecha_pago.Month;
                        oBeBcoMovCab.dfecha_movimiento = oBePgo.pgoc_sfecha_pago;
                        oBeBcoMovCab.icod_enti_financiera_cuenta = Convert.ToInt32(oBePgo.bcod_icod_banco_cuenta);
                        oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                        oBeBcoMovCab.vglosa = "TRANSFERENCIA BANCARIA";
                        oBeBcoMovCab.vdescripcion_beneficiario = Convert.ToString(oBeDXC.cliec_vnombre_cliente);
                        oBeBcoMovCab.iid_tipo_moneda = oBePgo.pgoc_icod_tipo_moneda;
                        oBeBcoMovCab.cliec_icod_cliente = oBePgo.intCliente;
                        oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePgo.pgoc_sfecha_pago);
                        oBeBcoMovCab.nmonto_movimiento = oBePgo.pgoc_nmonto;
                        oBeBcoMovCab.nmonto_saldo_banco = 0;
                        oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                        oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                        oBeBcoMovCab.vnro_documento = String.Format("{0:000}", Convert.ToInt32(oBePlnCab.plnc_vnumero_planilla));
                        oBeBcoMovCab.cflag_conciliacion = false;
                        oBeBcoMovCab.iusuario_crea = oBePgo.intUsuario;
                        oBeBcoMovCab.vpc_crea = oBePgo.strPc;
                        oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                        oBeBcoMovCab.mobac_flag_estado = true;
                        oBeBcoMovCab.TipoDocumento = "TRANS";
                        oBeBcoMovCab.mobac_origen_regitro = "PLN";
                        oBePgo.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                        oBeBcoMovCab.icod_correlativo = Convert.ToInt32(oBePgo.pgoc_icod_entidad_finan_mov);
                        //DETALLE DEL MOV. BANCOS
                        ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                        oBeBcoMovDet.mobdc_iid_anio = Parametros.intEjercicio;
                        oBeBcoMovDet.mobdc_iid_mes_periodo = Convert.ToDateTime(oBeDXC.doxcc_sfecha_doc).Month;
                        oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                        oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                        oBeBcoMovDet.vnumero_doc = oBeDXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.doxcc_vnumero_doc = oBeDXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.mobdc_icod_cliente = oBeDXC.cliec_icod_cliente;
                        oBeBcoMovDet.mto_mov_soles = oBePgo.pgoc_nmonto;
                        oBeBcoMovDet.mto_mov_dolar = Math.Round(oBePgo.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                        //oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                        oBeBcoMovDet.iusuario_crea = oBePgo.intUsuario;
                        oBeBcoMovDet.vpc_crea = oBePgo.strPc;
                        oBeBcoMovDet.mobdc_flag_estado = true;
                        oBeBcoMovDet.doxcc_icod_correlativo = oBeDXC.doxcc_icod_correlativo;
                        oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                        oBeBcoMovDet.docxc_icod_pago = oBePgo.pgoc_dxc_icod_pago;
                        //SE INSERTAR EL DETALLE
                        objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                        //ACTUALIZAR LOS SALDOS DEL DXC
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);

                        #endregion
                    }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                    { }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                    {
                        #region Pago DxC

                        EDocXCobrarPago oBePagoDXC1 = new EDocXCobrarPago();
                        oBePagoDXC1.doxcc_icod_correlativo = oBeDXC.doxcc_icod_correlativo;
                        oBePagoDXC1.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                        oBePagoDXC1.pdxcc_vnumero_doc = oBePgo.strNroAnticipo;
                        oBePagoDXC1.pdxcc_sfecha_cobro = oBeDXC.doxcc_sfecha_doc;
                        oBePagoDXC1.tablc_iid_tipo_moneda = oBePgo.pgoc_icod_tipo_moneda;
                        oBePagoDXC1.pdxcc_nmonto_cobro = oBePgo.pgoc_nmonto;
                        oBePagoDXC1.pdxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBePlnCab.plnc_sfecha_planilla);
                        oBePagoDXC1.pdxcc_vobservacion = String.Format("N° PLN VENTA: {0}", oBePlnCab.plnc_vnumero_planilla);
                        oBePagoDXC1.cliec_icod_cliente = oBePgo.pgoc_icod_cliente;
                        oBePagoDXC1.pdxcc_vorigen = "P";
                        oBePagoDXC1.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC1.strPc = oBePgo.strPc;
                        oBePagoDXC1.pdxcc_flag_estado = true;
                        oBePgo.pgoc_dxc_icod_canje_doc = new CuentasPorCobrarData().InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC1);
                        new TesoreriaData().ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);

                        #endregion
                        #region Pago Adelanto Pago
                        EAdelantoPago oBe = new EAdelantoPago();
                        oBe.doxcc_icod_correlativo_pago = oBeDXC.doxcc_icod_correlativo; //el documento a pagar
                        oBe.doxcc_icod_correlativo_adelanto = Convert.ToInt32(oBePgo.pgoc_icod_anticipo); //correlativo del adelanto
                        oBe.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta; //tipo documento(adelanto)
                        oBe.cliec_icod_cliente = Convert.ToInt32(oBePgo.pgoc_icod_cliente); //código del cliente
                        oBe.tablc_iid_tipo_moneda = Convert.ToInt32(oBePgo.pgoc_icod_tipo_moneda); //debe grabarse con el tipo de moneda del documento adelanto
                        oBe.adpac_nmonto_pago = Convert.ToDecimal(oBePgo.pgoc_nmonto); //monto que se va a pagar guardado con el tipo de moneda del documento adelanto
                        oBe.adpac_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(Convert.ToDateTime(oBeDXC.doxcc_sfecha_doc)); //tipo de cambio de la fecha seleccionada
                        oBe.adpac_vdescripcion = String.Format("N° PLN VENTA: {0} - {1} ", oBePgo.pgoc_vnumero_planilla, oBePgo.pgoc_descripcion.ToUpper());
                        oBe.adpac_sfecha_pago = Convert.ToDateTime(oBeDXC.doxcc_sfecha_doc); //fecha del pago
                        oBe.adpac_iorigen = "P"; //Adelanto
                        oBe.adpac_iusuario_crea = oBePgo.intUsuario;
                        oBe.adpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                        oBe.adpac_iusuario_modifica = oBePgo.intUsuario;
                        oBe.adpac_vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                        oBe.adpac_flag_estado = true;
                        oBe.SaldoDXCAdelanto = Convert.ToDecimal(oBePgo.pgoc_nmonto);
                        oBe.doxcc_nmonto_pagado = 0;
                        oBePgo.pgoc_dxc_icod_pago = new CuentasPorCobrarData().insertarAdelantoPago(oBe);
                        new TesoreriaData().ActualizarMontoPagadoAdelantoCliente(Convert.ToInt32(oBePgo.pgoc_icod_anticipo), 0);
                        #endregion

                    }

                    #region Planilla Cab
                    //INSERTAR LA CAB. DE LA PLANILLA (SE INSERTAR SI ES EL PRIMER REGISTRO, SINO SE MODIFICA)
                    if (oBePlnCab.plnc_icod_planilla == 0)
                    {
                        //INSERTAR LA CAB. DE LA PLANILLA (SE REALIZA SOLO CONE L PRIMER REGISTRO DE UN MOVIMIENTO)                     
                        oBePlnCab.plnc_icod_planilla = objVentasData.insertarPlanillaCobranzaCab(oBePlnCab);
                    }
                    else
                    {
                        //NO SE REALIZA NINGUNA ACCION, PORQUE LA CAB. PLANILLA SE ACTUALIZARA AUTOMATICAMENTE AL TERMINAR LA INSERCION DE LA FACTURA
                    }
                    //INSERTAR EL DET. DE LA PLANILLA
                    #endregion

                    oBePlnDet.plnd_nmonto_pagado = oBePgo.pgoc_nmonto;
                    oBePlnDet.plnc_icod_planilla = oBePlnCab.plnc_icod_planilla;
                    oBePlnDet.pgoc_icod_pago = objVentasData.insertarPago(oBePgo);
                    oBePlnDet.plnd_icod_detalle = objVentasData.insertarPlanillaCobranzaDetalle(oBePlnDet);

                    tx.Complete();
                }
                return oBePlnDet.plnd_icod_detalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarPagoPln(EPlanillaCobranzaCab oBePlnCab, EPlanillaCobranzaDet oBePlnDet, EPagoDocVenta oBePgo, EDocXCobrar oBeDXC)
        {
            try
            {
                ContabilidadData objContabilidadData = new ContabilidadData();
                CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();
                TesoreriaData objTesoreriaData = new TesoreriaData();

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //SE OBTIENEN TODOS LOS DATOS DEL DXC, PARA POSTERIOR USO (AL REINGRESAR)
                    oBeDXC = objCuentaCobrarData.getDXCDatos(oBePgo.pgoc_dxc_icod_doc);
                    //
                    #region Eliminar el pago, para su reingreso
                    //OBTERNER TODOS LOS DATOS DEL PAGO
                    var x = objVentasData.getDatosPago(oBePgo.pgoc_icod_pago)[0];
                    //
                    if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                    {
                        #region DXP Pago
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC.strPc = oBePgo.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        #endregion
                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                    {
                        #region Tarjeta de Credito
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC.strPc = oBePgo.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        //SE ELIMINA EL MOVIMIENTO DEL BANCO
                        objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                        #endregion
                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                    {
                        #region Nota de Credito
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePlnCab.intUsuario;
                        oBePagoDXC.strPc = oBePlnCab.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);

                        ENotaCreditoPago _beNCP = new ENotaCreditoPago();
                        _beNCP.ncpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                        _beNCP.ncpac_vpc_modifica = oBePlnCab.strPc;
                        _beNCP.ncpac_iusuario_modifica = oBePlnCab.intUsuario;
                        objCuentaCobrarData.eliminarNCPago(_beNCP);
                        objTesoreriaData.ActualizarMontoPagadoSaldoNotaCreditoCliente(Convert.ToInt64(x.pgoc_icod_nota_credito), 0);
                        #endregion
                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                    {
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC.strPc = oBePgo.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        //SE ELIMINA EL MOVIMIENTO DEL BANCO
                        objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                    {
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC.strPc = oBePgo.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        //SE ELIMINA EL MOVIMIENTO DEL BANCO
                        objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                    { }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                    {
                        #region Adelanto Cliente
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC.strPc = oBePgo.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        //SE ELIMINA EL PAGO DEL ANTICIPO
                        EAdelantoPago oBePagoDXCAnt = new EAdelantoPago();
                        oBePagoDXCAnt.adpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXCAnt.doxcc_icod_correlativo_adelanto = Convert.ToInt64(x.pgoc_icod_anticipo);
                        oBePagoDXCAnt.intUsuario = oBePgo.intUsuario;
                        oBePagoDXCAnt.strPc = oBePgo.strPc;
                        new CuentasPorCobrarData().eliminarAdelantoPago(oBePagoDXCAnt);
                        objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(Convert.ToInt64(x.pgoc_icod_anticipo), 0);
                        #endregion

                    }
                    //FINALMENTE SE ELIMINA EL PAGO Y EL DET DEL PLANILLA
                    objVentasData.eliminarPago(oBePgo);
                    #endregion
                    #region Reingresar el pago
                    if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                    {
                        #region DXP Pago
                        //EL PAGO ES EN EFECTIVO, SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(oBePgo);
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);
                        #endregion
                    }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                    {
                        #region Tarjeta de Credito
                        //EL PAGO ES EN TARJETA DE CRED., SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(oBePgo);
                        oBePgo.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);
                        //EL PAGO ES EN TARJETA DE CRED., DEBE SER INGRESADO EN BANCOS
                        var lstTipoTarjeta = listarTipoTarjeta();
                        ETipoTarjeta oBeTipoTarjeta = lstTipoTarjeta.Where(d => d.tcrc_icod_tipo_tarjeta_cred == Convert.ToInt32(oBePgo.pgoc_icod_tipo_tarjeta)).ToList()[0];
                        //CABECERA DEL MOV. DE BANCOS
                        ELibroBancos oBeBcoMovCab = new ELibroBancos();
                        oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                        oBeBcoMovCab.iid_mes = oBePgo.pgoc_sfecha_pago.Month;
                        oBeBcoMovCab.dfecha_movimiento = oBePgo.pgoc_sfecha_pago;
                        oBeBcoMovCab.icod_enti_financiera_cuenta = oBeTipoTarjeta.bcod_icod_banco_cuenta;
                        oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                        oBeBcoMovCab.vglosa = "PAGO CON TARJETA";
                        oBeBcoMovCab.vdescripcion_beneficiario = oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred;
                        oBeBcoMovCab.iid_tipo_moneda = oBePgo.pgoc_icod_tipo_moneda;
                        oBeBcoMovCab.cliec_icod_cliente = oBePgo.intCliente;
                        oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePgo.pgoc_sfecha_pago);
                        oBeBcoMovCab.nmonto_movimiento = oBePgo.pgoc_nmonto;
                        oBeBcoMovCab.nmonto_saldo_banco = 0;
                        oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                        oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                        oBeBcoMovCab.vnro_documento = String.Format("{0:000}-{1:00}", Convert.ToInt32(oBePlnCab.plnc_vnumero_planilla), oBeTipoTarjeta.tcrc_iid_tipo_tarjeta_cred);
                        oBeBcoMovCab.cflag_conciliacion = false;
                        oBeBcoMovCab.iusuario_crea = oBePgo.intUsuario;
                        oBeBcoMovCab.vpc_crea = oBePgo.strPc;
                        oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                        oBeBcoMovCab.mobac_flag_estado = true;
                        oBeBcoMovCab.TipoDocumento = "PVD";
                        oBeBcoMovCab.mobac_origen_regitro = "PLN";
                        oBePgo.pgoc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                        oBeBcoMovCab.icod_correlativo = Convert.ToInt32(oBePgo.pgoc_icod_entidad_finan_mov);
                        //DETALLE DEL MOV. BANCOS
                        ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                        oBeBcoMovDet.mobdc_iid_anio = Parametros.intEjercicio;
                        oBeBcoMovDet.mobdc_iid_mes_periodo = Convert.ToDateTime(oBeDXC.doxcc_sfecha_doc).Month;
                        oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                        oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocFacturaVentaServicios;
                        oBeBcoMovDet.vnumero_doc = oBeDXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.doxcc_vnumero_doc = oBeDXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.mobdc_icod_cliente = oBeDXC.cliec_icod_cliente;
                        oBeBcoMovDet.mto_mov_soles = oBePgo.pgoc_nmonto;
                        oBeBcoMovDet.mto_mov_dolar = Math.Round(oBePgo.pgoc_nmonto / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                        oBeBcoMovDet.vglosa = String.Format("PAGO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                        oBeBcoMovDet.iusuario_crea = oBePgo.intUsuario;
                        oBeBcoMovDet.vpc_crea = oBePgo.strPc;
                        oBeBcoMovDet.mobdc_flag_estado = true;
                        oBeBcoMovDet.doxcc_icod_correlativo = oBeDXC.doxcc_icod_correlativo;
                        oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                        oBeBcoMovDet.docxc_icod_pago = oBePgo.pgoc_dxc_icod_pago;
                        //SE INSERTAR EL DETALLE
                        objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                        //ACTUALIZAR LOS SALDOS DEL DXC
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);
                        #endregion
                    }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                    {

                        #region Ingreso del pago Dxp Pago
                        EDocXCobrarPago oBePagoDXC = getPagoDXC(oBePgo);
                        oBePgo.pgoc_dxc_icod_pago = objCuentaCobrarData.InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        #endregion
                        #region Insertar Pago Nota de Credito
                        //EL PAGO CON LA NOTA DE CREDITO
                        ENotaCreditoPago oBe = new ENotaCreditoPago();
                        oBe.doxcc_icod_correlativo_pago = oBePgo.pgoc_dxc_icod_doc; //el documento a pagar
                        oBe.doxcc_icod_correlativo_nota_credito = Convert.ToInt64(oBePgo.pgoc_icod_nota_credito); //correlativo de la nota de crédito   
                        oBe.tablc_iid_tipo_moneda = Convert.ToInt32(oBePgo.pgoc_icod_tipo_moneda); //debe grabarse con el tipo de moneda del documento nota de crédito
                        oBe.ncpac_nmonto_pago = Convert.ToDecimal(oBePgo.pgoc_nmonto); //monto que se va a pagar guardado con el tipo de moneda del documento nota de crédito
                        oBe.ncpac_nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBePlnDet.plnd_sfecha_doc); //tipo de cambio de la fecha seleccionada
                        oBe.ncpac_vdescripcion = String.Format("N° PLN VENTA: {0}", oBePlnCab.plnc_vnumero_planilla);
                        oBe.ncpac_sfecha_pago = Convert.ToDateTime(oBePlnDet.plnd_sfecha_doc); //fecha del pago
                        oBe.ncpac_iorigen = "P"; //Nota de credito
                        oBe.ncpac_flag_estado = true;
                        oBe.ncpac_iusuario_crea = oBePgo.intUsuario;
                        oBe.ncpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                        oBe.ncpac_iusuario_modifica = oBePgo.intUsuario;
                        oBe.ncpac_vpc_modifica = WindowsIdentity.GetCurrent().Name;
                        oBe.pdxcc_icod_correlativo = oBePgo.pgoc_dxc_icod_pago;
                        oBePgo.pgoc_dxc_icod_canje_doc = new CuentasPorCobrarData().insertarNCPago(oBe);

                        #endregion
                        #region Actualizacion de Estados
                        TesoreriaData objTesoreriaData1 = new TesoreriaData();
                        objTesoreriaData1.ActualizarMontoPagadoSaldoNotaCreditoCliente(oBe.doxcc_icod_correlativo_nota_credito, 0);
                        objTesoreriaData1.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(oBePgo.pgoc_dxc_icod_doc), 0);
                        #endregion

                    }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                    { }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                    { }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                    { }
                    else if (oBePgo.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                    {
                        #region Pago DxC

                        EDocXCobrarPago oBePagoDXC1 = new EDocXCobrarPago();
                        oBePagoDXC1.doxcc_icod_correlativo = oBeDXC.doxcc_icod_correlativo;
                        oBePagoDXC1.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                        oBePagoDXC1.pdxcc_vnumero_doc = oBePgo.strNroAnticipo;
                        oBePagoDXC1.pdxcc_sfecha_cobro = oBeDXC.doxcc_sfecha_doc;
                        oBePagoDXC1.tablc_iid_tipo_moneda = oBePgo.pgoc_icod_tipo_moneda;
                        oBePagoDXC1.pdxcc_nmonto_cobro = oBePgo.pgoc_nmonto;
                        oBePagoDXC1.pdxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBePlnCab.plnc_sfecha_planilla);
                        oBePagoDXC1.pdxcc_vobservacion = String.Format("N° PLN VENTA: {0}", oBePlnCab.plnc_vnumero_planilla);
                        oBePagoDXC1.cliec_icod_cliente = oBePgo.pgoc_icod_cliente;
                        oBePagoDXC1.pdxcc_vorigen = "P";
                        oBePagoDXC1.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC1.strPc = oBePgo.strPc;
                        oBePagoDXC1.pdxcc_flag_estado = true;
                        oBePgo.pgoc_dxc_icod_canje_doc = new CuentasPorCobrarData().InsertarPagoDirectoDocumentoXCobrar(oBePagoDXC1);
                        new TesoreriaData().ActualizarMontoDXCPagadoSaldo(oBeDXC.doxcc_icod_correlativo, 0);

                        #endregion
                        #region Pago Adelanto Pago
                        EAdelantoPago oBe = new EAdelantoPago();
                        oBe.doxcc_icod_correlativo_pago = oBeDXC.doxcc_icod_correlativo; //el documento a pagar
                        oBe.doxcc_icod_correlativo_adelanto = Convert.ToInt32(oBePgo.pgoc_icod_anticipo); //correlativo del adelanto
                        oBe.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta; //tipo documento(adelanto)
                        oBe.cliec_icod_cliente = Convert.ToInt32(oBePgo.pgoc_icod_cliente); //código del cliente
                        oBe.tablc_iid_tipo_moneda = Convert.ToInt32(oBePgo.pgoc_icod_tipo_moneda); //debe grabarse con el tipo de moneda del documento adelanto
                        oBe.adpac_nmonto_pago = Convert.ToDecimal(oBePgo.pgoc_nmonto); //monto que se va a pagar guardado con el tipo de moneda del documento adelanto
                        oBe.adpac_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(Convert.ToDateTime(oBeDXC.doxcc_sfecha_doc)); //tipo de cambio de la fecha seleccionada
                        oBe.adpac_vdescripcion = String.Format("N° PLN VENTA: {0} - {1} ", oBePgo.pgoc_vnumero_planilla, oBePgo.pgoc_descripcion.ToUpper());
                        oBe.adpac_sfecha_pago = Convert.ToDateTime(oBeDXC.doxcc_sfecha_doc); //fecha del pago
                        oBe.adpac_iorigen = "P"; //Adelanto
                        oBe.adpac_iusuario_crea = oBePgo.intUsuario;
                        oBe.adpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                        oBe.adpac_iusuario_modifica = oBePgo.intUsuario;
                        oBe.adpac_vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                        oBe.adpac_flag_estado = true;
                        oBe.SaldoDXCAdelanto = Convert.ToDecimal(oBePgo.pgoc_nmonto);
                        oBe.doxcc_nmonto_pagado = 0;
                        oBePgo.pgoc_dxc_icod_pago = new CuentasPorCobrarData().insertarAdelantoPago(oBe);
                        new TesoreriaData().ActualizarMontoPagadoAdelantoCliente(Convert.ToInt32(oBePgo.pgoc_icod_anticipo), 0);
                        #endregion
                    }
                    #endregion

                    oBePlnDet.pgoc_icod_pago = objVentasData.insertarPago(oBePgo);
                    oBePlnDet.pgoc_icod_pago = oBePlnDet.pgoc_icod_pago;
                    objVentasData.modificarPlanillaCobranzaDetalle(oBePlnDet);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarPagoPln(EPlanillaCobranzaCab oBePlnCab, EPlanillaCobranzaDet oBePlnDet, EPagoDocVenta oBePgo)
        {
            try
            {
                CuentasPorCobrarData objCuentaCobrarData = new CuentasPorCobrarData();
                TesoreriaData objTesoreriaData = new TesoreriaData();

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //OBTERNER TODOS LOS DATOS DEL PAGO
                    var x = objVentasData.getDatosPago(oBePgo.pgoc_icod_pago)[0];
                    //
                    if (x.pgoc_tipo_pago == Parametros.intTipoPgoEfectivo)
                    {
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC.strPc = oBePgo.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                    {
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC.strPc = oBePgo.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        //SE ELIMINA EL MOVIMIENTO DEL BANCO
                        objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoNotaCredito)
                    {
                        #region Nota de Credito
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePlnCab.intUsuario;
                        oBePagoDXC.strPc = oBePlnCab.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);

                        ENotaCreditoPago _beNCP = new ENotaCreditoPago();
                        _beNCP.ncpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                        _beNCP.ncpac_vpc_modifica = oBePlnCab.strPc;
                        _beNCP.ncpac_iusuario_modifica = oBePlnCab.intUsuario;
                        objCuentaCobrarData.eliminarNCPago(_beNCP);
                        objTesoreriaData.ActualizarMontoPagadoSaldoNotaCreditoCliente(Convert.ToInt64(x.pgoc_icod_nota_credito), 0);
                        #endregion

                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCheque)
                    {
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC.strPc = oBePgo.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        //SE ELIMINA EL MOVIMIENTO DEL BANCO
                        objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));

                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoTransfBancaria)
                    {
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC.strPc = oBePgo.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        //SE ELIMINA EL MOVIMIENTO DEL BANCO
                        objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(x.pgoc_icod_entidad_finan_mov));
                    }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoCredito)
                    { }
                    else if (x.pgoc_tipo_pago == Parametros.intTipoPgoAnticipo)
                    {
                        #region Adelanto Cliente
                        //SE ELIMINA EL PAGO DEL DOC. POR COBRAR
                        EDocXCobrarPago oBePagoDXC = new EDocXCobrarPago();
                        oBePagoDXC.pdxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_canje_doc);
                        oBePagoDXC.doxcc_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_doc);
                        oBePagoDXC.intUsuario = oBePgo.intUsuario;
                        oBePagoDXC.strPc = oBePgo.strPc;
                        objCuentaCobrarData.EliminarPagoDirectoDocumentoXCobrar(oBePagoDXC);
                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(x.pgoc_dxc_icod_doc), 0);
                        //SE ELIMINA EL PAGO DEL ANTICIPO
                        EAdelantoPago oBePagoDXCAnt = new EAdelantoPago();
                        oBePagoDXCAnt.adpac_icod_correlativo = Convert.ToInt64(x.pgoc_dxc_icod_pago);
                        oBePagoDXCAnt.doxcc_icod_correlativo_adelanto = Convert.ToInt64(x.pgoc_icod_anticipo);
                        oBePagoDXCAnt.intUsuario = oBePgo.intUsuario;
                        oBePagoDXCAnt.strPc = oBePgo.strPc;
                        new CuentasPorCobrarData().eliminarAdelantoPago(oBePagoDXCAnt);
                        objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(Convert.ToInt64(x.pgoc_icod_anticipo), 0);
                        #endregion
                    }

                    //FINALMENTE SE ELIMINA EL PAGO Y EL DET DEL PLANILLA
                    objVentasData.eliminarPago(oBePgo);
                    //
                    objVentasData.eliminarPlanillaCobranzaDetalle(oBePlnDet);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Anticipos desde Planilla de Cobranza
        public int insertarAnticipoPln(EPlanillaCobranzaCab oBePlnCab, EPlanillaCobranzaDet oBePlnDet, EAnticipo oBeAntc)
        {
            int id_pln_det = 0;
            ContabilidadData objContabilidadData = new ContabilidadData();
            TesoreriaData objTesoreriaData = new TesoreriaData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region 1 Insertando Cabecera de Planilla

                    //INSERTAR LA CAB. DE LA PLANILLA (SE INSERTAR SI ES EL PRIMER REGISTRO, SINO SE MODIFICA)
                    if (oBePlnCab.plnc_icod_planilla == 0)
                    {
                        //INSERTAR LA CAB. DE LA PLANILLA (SE REALIZA SOLO CONE L PRIMER REGISTRO DE UN MOVIMIENTO)                     
                        oBePlnCab.plnc_icod_planilla = objVentasData.insertarPlanillaCobranzaCab(oBePlnCab);
                    }
                    else
                    {
                        //NO SE REALIZA NINGUNA ACCION, PORQUE LA CAB. PLANILLA SE ACTUALIZARA AUTOMATICAMENTE AL TERMINAR LA INSERCION DE LA FACTURA
                    }

                    #endregion
                    #region 2 Insertando el Adelanto
                    //En esta parte se inserta el adelanto del cliente

                    EAdelantoCliente oBeAdelanto = new EAdelantoCliente();
                    oBeAdelanto.icod_correlativo = 0;//Este es el id del adelanto, pero como recién se esta insertando no tiene.
                    oBeAdelanto.icod_correlativo_cabecera = 0;//Cuando es desde bancos aqui va el correlativo del mov. de bancos.
                    oBeAdelanto.icod_cliente = oBeAntc.antc_icod_cliente;
                    oBeAdelanto.iid_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                    oBeAdelanto.sfecha_adelanto = oBeAntc.antc_sfecha_anticipo;
                    oBeAdelanto.iid_tipo_moneda = oBeAntc.tablc_iid_tipo_moneda;
                    /**/
                    oBeAdelanto.nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBeAntc.antc_sfecha_anticipo);
                    if (oBeAdelanto.nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    /**/

                    oBeAdelanto.nmonto_adelanto = oBeAntc.antc_nmonto_anticipo;
                    oBeAdelanto.nmonto_pagado = 0;
                    oBeAdelanto.vobservacion = oBeAntc.antc_observaciones;
                    oBeAdelanto.nsituacion_adelanto_cliente = Parametros.intSitClienteGenerado;
                    oBeAdelanto.iusuario_crea = oBeAntc.intUsuario;
                    oBeAdelanto.vpc_crea = oBeAntc.strPc;
                    oBeAdelanto.flag_estado = true;
                    var lstAdeCliente = new BCuentasPorCobrar().ListarAdelantoClietneXAñoTodos(Parametros.intEjercicio);
                    if (lstAdeCliente.Count == 0)
                        oBeAdelanto.vnumero_adelanto = Parametros.intEjercicio.ToString().Remove(0, 1) + "000001";
                    else
                        oBeAdelanto.vnumero_adelanto = Parametros.intEjercicio.ToString().Remove(0, 1) + string.Format("{0:000000}", (Convert.ToInt32(lstAdeCliente.Max(max => max.vnumero_adelanto).Remove(0, 3)) + 1));
                    oBeAdelanto.icod_correlativo = new TesoreriaData().insertarAdelantoCliente(oBeAdelanto);
                    //
                    #endregion
                    #region 3 Insertando el DXC
                    //En esta parte se inserta el dxc del adelanto
                    EDocXCobrar obj_DXC = new EDocXCobrar();
                    obj_DXC.doxcc_icod_correlativo = 0;//el id del dxc, pero aún no existe
                    obj_DXC.doxcc_vnumero_doc = oBeAdelanto.vnumero_adelanto;
                    obj_DXC.anio = Parametros.intEjercicio;
                    obj_DXC.mesec_iid_mes = Convert.ToInt16(oBeAdelanto.sfecha_adelanto.Month);
                    obj_DXC.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                    obj_DXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocAdelantoCliente;
                    obj_DXC.doxcc_sfecha_doc = oBeAdelanto.sfecha_adelanto;
                    obj_DXC.cliec_icod_cliente = oBeAdelanto.icod_cliente;
                    obj_DXC.cliec_vnombre_cliente = oBePlnDet.strCliente;
                    obj_DXC.tablc_iid_tipo_moneda = oBeAdelanto.iid_tipo_moneda;
                    obj_DXC.tablc_iid_tipo_pago = 1;
                    obj_DXC.doxcc_nmonto_tipo_cambio = oBeAdelanto.nmonto_tipo_cambio;
                    obj_DXC.doxcc_vdescrip_transaccion = oBeAdelanto.vobservacion;
                    obj_DXC.doxcc_nmonto_total = oBeAdelanto.nmonto_adelanto;
                    obj_DXC.doxcc_nmonto_saldo = oBeAdelanto.nmonto_adelanto;
                    obj_DXC.doxcc_nmonto_pagado = 0;
                    obj_DXC.tablc_iid_situacion_documento = Parametros.intSitDocCobrarGenerado;
                    obj_DXC.doxcc_vobservaciones = oBeAdelanto.vobservacion;
                    obj_DXC.doxc_bind_cuenta_corriente = false;
                    obj_DXC.doxcc_bind_impresion_nogerencia = false;
                    obj_DXC.doxc_bind_situacion_legal = false;
                    obj_DXC.doxc_bind_cierre_cuenta_corriente = false;
                    obj_DXC.intUsuario = oBeAntc.intUsuario;
                    obj_DXC.strPc = oBeAntc.strPc;
                    //obj_DXC.docxc_icod_documento = oBeAdelanto.icod_correlativo;
                    obj_DXC.doxcc_flag_estado = true;
                    obj_DXC.doxcc_origen = "P";

                    obj_DXC.doxcc_icod_correlativo = new CuentasPorCobrarData().insertarDocumentoXCobrar(obj_DXC);
                    //
                    #endregion
                    #region 4 Ingreso a Bancos si el anticipo se realizó con Tarjeta de Crédito
                    if (oBeAntc.tablc_iid_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                    {
                        #region Tarjeta Credito
                        //EL PAGO ES EN TARJETA DE CRED., SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                        //EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                        //x.pgoc_dxc_icod_pago = objCuentaCobrarData.insertarDXCPago(oBePagoDXC);
                        //EL PAGO ES EN TARJETA DE CRED., DEBE SER INGRESADO EN BANCOS
                        var lstTipoTarjeta = listarTipoTarjeta();
                        ETipoTarjeta oBeTipoTarjeta = lstTipoTarjeta.Where(d => d.tcrc_icod_tipo_tarjeta_cred == Convert.ToInt32(oBeAntc.tablc_iid_tipo_tarjeta)).ToList()[0];
                        //CABECERA DEL MOV. DE BANCOS
                        ELibroBancos oBeBcoMovCab = new ELibroBancos();
                        oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                        oBeBcoMovCab.iid_mes = oBeAntc.antc_sfecha_anticipo.Month;
                        oBeBcoMovCab.dfecha_movimiento = oBeAntc.antc_sfecha_anticipo;
                        oBeBcoMovCab.icod_enti_financiera_cuenta = oBeTipoTarjeta.bcod_icod_banco_cuenta;
                        oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                        oBeBcoMovCab.vglosa = "ANTICIPO CON TARJETA";
                        oBeBcoMovCab.vdescripcion_beneficiario = oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred;
                        oBeBcoMovCab.iid_tipo_moneda = oBeAntc.tablc_iid_tipo_moneda;
                        oBeBcoMovCab.cliec_icod_cliente = oBeAntc.antc_icod_cliente;
                        oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBeAntc.antc_sfecha_anticipo);
                        oBeBcoMovCab.nmonto_movimiento = oBeAntc.antc_nmonto_anticipo;
                        oBeBcoMovCab.nmonto_saldo_banco = 0;
                        oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                        oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                        oBeBcoMovCab.vnro_documento = String.Format("{0:000}-{1:00}", Convert.ToInt32(oBePlnCab.plnc_vnumero_planilla), oBeTipoTarjeta.tcrc_iid_tipo_tarjeta_cred);
                        oBeBcoMovCab.cflag_conciliacion = false;
                        oBeBcoMovCab.iusuario_crea = oBeAntc.intUsuario;
                        oBeBcoMovCab.vpc_crea = oBeAntc.strPc;
                        oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                        oBeBcoMovCab.mobac_flag_estado = true;
                        oBeBcoMovCab.TipoDocumento = "PVD";
                        oBeBcoMovCab.mobac_origen_regitro = "PLN";
                        oBeAntc.antc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                        oBeBcoMovCab.icod_correlativo = Convert.ToInt32(oBeAntc.antc_icod_entidad_finan_mov);
                        //DETALLE DEL MOV. BANCOS
                        ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                        oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                        oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocAdelantoCliente;
                        oBeBcoMovDet.doxcc_sfecha_doc = oBeAntc.antc_sfecha_anticipo;
                        oBeBcoMovDet.vnumero_doc = obj_DXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.doxcc_vnumero_doc = obj_DXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.mobdc_icod_cliente = oBeAntc.antc_icod_cliente;
                        oBeBcoMovDet.mto_mov_soles = oBeAntc.antc_nmonto_anticipo;
                        oBeBcoMovDet.mto_mov_dolar = Math.Round(oBeAntc.antc_nmonto_anticipo / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                        oBeBcoMovDet.vglosa = String.Format("ANTICIPO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                        oBeBcoMovDet.iusuario_crea = oBeAntc.intUsuario;
                        oBeBcoMovDet.vpc_crea = oBeAntc.strPc;
                        oBeBcoMovDet.mobdc_flag_estado = true;
                        oBeBcoMovDet.doxcc_icod_correlativo = obj_DXC.doxcc_icod_correlativo;
                        oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                        //oBeBcoMovDet.docxc_icod_pago = x.pgoc_dxc_icod_pago;
                        //SE INSERTAR EL DETALLE
                        objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                        #endregion
                    }
                    #endregion
                    #region 5 Insertando el Anticipo
                    //En esta parte se inserta el anticipo
                    oBeAntc.antc_icod_adelanto_cliente = oBeAdelanto.icod_correlativo;
                    oBeAntc.antc_icod_dxc_adelanto = obj_DXC.doxcc_icod_correlativo;
                    oBePlnDet.antc_icod_anticipo = objVentasData.insertarAnticipo(oBeAntc);                    //

                    #endregion
                    #region 6 Insertando Planilla Detalle
                    //En esta parte se insertar el detalle de la planilla y modificamos la cabecera de la pln, si no es el primer registro
                    oBePlnDet.plnc_icod_planilla = oBePlnCab.plnc_icod_planilla;
                    oBePlnDet.plnd_icod_documento = oBeAdelanto.icod_correlativo;
                    oBePlnDet.plnd_vnumero_doc = oBeAdelanto.vnumero_adelanto;
                    id_pln_det = objVentasData.insertarPlanillaCobranzaDetalle(oBePlnDet);
                    #endregion

                    tx.Complete();
                }
                return id_pln_det;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarAnticipoPln(EPlanillaCobranzaCab oBePlnCab, EPlanillaCobranzaDet oBePlnDet, EAnticipo oBeAntc)
        {
            ContabilidadData objContabilidadData = new ContabilidadData();
            TesoreriaData objTesoreriaData = new TesoreriaData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region 1 Modificando el Adelanto
                    //En esta parte se modifica el adelanto del cliente                
                    EAdelantoCliente oBeAdelanto = new EAdelantoCliente();
                    var lstADC = new TesoreriaData().getAdelantoClienteCab(oBeAntc.antc_icod_adelanto_cliente);
                    if (lstADC.Count == 0)
                        throw new ArgumentException("Error de datos, no se encontró ADC...");
                    oBeAdelanto = lstADC[0];
                    oBeAdelanto.icod_correlativo = oBeAntc.antc_icod_adelanto_cliente;
                    oBeAdelanto.icod_cliente = oBeAntc.antc_icod_cliente;
                    oBeAdelanto.iid_tipo_moneda = oBeAntc.tablc_iid_tipo_moneda;
                    oBeAdelanto.nmonto_adelanto = oBeAntc.antc_nmonto_anticipo;
                    oBeAdelanto.vobservacion = oBeAntc.antc_observaciones;
                    oBeAdelanto.iusuario_modifica = oBeAntc.intUsuario;
                    oBeAdelanto.vpc_modifica = oBeAntc.strPc;
                    new TesoreriaData().modificarAdelantoCliente(oBeAdelanto);
                    //
                    #endregion                    
                    #region 2 Modificando el DXC
                    //En esta parte se modifica el dxc del adelanto
                    EDocXCobrar obj_DXC = new EDocXCobrar();
                    obj_DXC.doxcc_icod_correlativo = oBeAntc.antc_icod_dxc_adelanto;
                    obj_DXC.doxcc_vnumero_doc = oBeAdelanto.vnumero_adelanto;
                    obj_DXC.anio = Parametros.intEjercicio;
                    obj_DXC.mesec_iid_mes = Convert.ToInt16(oBeAdelanto.sfecha_adelanto.Month);
                    obj_DXC.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                    obj_DXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocAdelantoCliente;
                    obj_DXC.doxcc_sfecha_doc = oBeAdelanto.sfecha_adelanto;
                    obj_DXC.cliec_icod_cliente = oBeAdelanto.icod_cliente;
                    obj_DXC.cliec_vnombre_cliente = oBePlnDet.strCliente;
                    obj_DXC.tablc_iid_tipo_moneda = oBeAdelanto.iid_tipo_moneda;
                    obj_DXC.tablc_iid_tipo_pago = 1;
                    obj_DXC.doxcc_nmonto_tipo_cambio = oBeAdelanto.nmonto_tipo_cambio;
                    obj_DXC.doxcc_vdescrip_transaccion = oBeAdelanto.vobservacion;
                    obj_DXC.doxcc_nmonto_total = oBeAdelanto.nmonto_adelanto;
                    obj_DXC.doxcc_nmonto_saldo = oBeAdelanto.nmonto_adelanto;
                    obj_DXC.doxcc_nmonto_pagado = 0;
                    obj_DXC.tablc_iid_situacion_documento = Parametros.intSitDocCobrarGenerado;
                    obj_DXC.doxcc_vobservaciones = oBeAdelanto.vobservacion;
                    obj_DXC.doxc_bind_cuenta_corriente = false;
                    obj_DXC.doxcc_bind_impresion_nogerencia = false;
                    obj_DXC.doxc_bind_situacion_legal = false;
                    obj_DXC.doxc_bind_cierre_cuenta_corriente = false;
                    obj_DXC.intUsuario = oBeAntc.intUsuario;
                    obj_DXC.strPc = oBeAntc.strPc;
                    //obj_DXC.docxc_icod_documento = oBeAdelanto.icod_correlativo;
                    obj_DXC.doxcc_flag_estado = true;
                    obj_DXC.doxcc_origen = "P";

                    new CuentasPorCobrarData().modificarDocumentoXCobrar(obj_DXC);
                    //
                    #endregion
                    #region 3 Eliminar el depósito de bancos si existe
                    if (Convert.ToInt32(oBePlnDet.intIcodEntidadFinanMov) > 0)
                        objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(oBePlnDet.intIcodEntidadFinanMov));
                    #endregion
                    #region 4 Ingreso a Bancos si el anticipo se realizó con Tarjeta de Crédito
                    if (oBeAntc.tablc_iid_tipo_pago == Parametros.intTipoPgoTarjetaCredito)
                    {
                        #region Tarjeta Credito
                        //EL PAGO ES EN TARJETA DE CRED., SE INGRESA COMO PAGO DEL DOC. POR COBRAR CREADO
                        //EDocXCobrarPago oBePagoDXC = getPagoDXC(x);
                        //x.pgoc_dxc_icod_pago = objCuentaCobrarData.insertarDXCPago(oBePagoDXC);
                        //EL PAGO ES EN TARJETA DE CRED., DEBE SER INGRESADO EN BANCOS
                        var lstTipoTarjeta = listarTipoTarjeta();
                        ETipoTarjeta oBeTipoTarjeta = lstTipoTarjeta.Where(d => d.tcrc_icod_tipo_tarjeta_cred == Convert.ToInt32(oBeAntc.tablc_iid_tipo_tarjeta)).ToList()[0];
                        //CABECERA DEL MOV. DE BANCOS
                        ELibroBancos oBeBcoMovCab = new ELibroBancos();
                        oBeBcoMovCab.iid_anio = Parametros.intEjercicio;
                        oBeBcoMovCab.iid_mes = oBeAntc.antc_sfecha_anticipo.Month;
                        oBeBcoMovCab.dfecha_movimiento = oBeAntc.antc_sfecha_anticipo;
                        oBeBcoMovCab.icod_enti_financiera_cuenta = oBeTipoTarjeta.bcod_icod_banco_cuenta;
                        oBeBcoMovCab.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                        oBeBcoMovCab.vglosa = "ANTICIPO CON TARJETA";
                        oBeBcoMovCab.vdescripcion_beneficiario = oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred;
                        oBeBcoMovCab.iid_tipo_moneda = oBeAntc.tablc_iid_tipo_moneda;
                        oBeBcoMovCab.cliec_icod_cliente = oBeAntc.antc_icod_cliente;
                        oBeBcoMovCab.nmonto_tipo_cambio = objContabilidadData.getTipoCambioPorFecha(oBeAntc.antc_sfecha_anticipo);
                        oBeBcoMovCab.nmonto_movimiento = oBeAntc.antc_nmonto_anticipo;
                        oBeBcoMovCab.nmonto_saldo_banco = 0;
                        oBeBcoMovCab.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                        oBeBcoMovCab.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                        oBeBcoMovCab.vnro_documento = String.Format("{0:000}-{1:00}", Convert.ToInt32(oBePlnCab.plnc_vnumero_planilla), oBeTipoTarjeta.tcrc_iid_tipo_tarjeta_cred);
                        oBeBcoMovCab.cflag_conciliacion = false;
                        oBeBcoMovCab.iusuario_crea = oBeAntc.intUsuario;
                        oBeBcoMovCab.vpc_crea = oBeAntc.strPc;
                        oBeBcoMovCab.iid_motivo_mov_banco = Parametros.intMotivoCuentasPorCobrar;
                        oBeBcoMovCab.mobac_flag_estado = true;
                        oBeBcoMovCab.TipoDocumento = "PVD";
                        oBeBcoMovCab.mobac_origen_regitro = "PLN";
                        oBeAntc.antc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);
                        oBeBcoMovCab.icod_correlativo = Convert.ToInt32(oBeAntc.antc_icod_entidad_finan_mov);
                        //DETALLE DEL MOV. BANCOS
                        ELibroBancosDetalle oBeBcoMovDet = new ELibroBancosDetalle();
                        oBeBcoMovDet.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                        oBeBcoMovDet.tdodc_iid_correlativo = Parametros.intClaseTipoDocAdelantoCliente;
                        oBeBcoMovDet.doxcc_sfecha_doc = oBeAntc.antc_sfecha_anticipo;
                        oBeBcoMovDet.vnumero_doc = obj_DXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.doxcc_vnumero_doc = obj_DXC.doxcc_vnumero_doc;
                        oBeBcoMovDet.mobdc_icod_cliente = oBeAntc.antc_icod_cliente;
                        oBeBcoMovDet.mto_mov_soles = oBeAntc.antc_nmonto_anticipo;
                        oBeBcoMovDet.mto_mov_dolar = Math.Round(oBeAntc.antc_nmonto_anticipo / oBeBcoMovCab.nmonto_tipo_cambio, 2);
                        oBeBcoMovDet.vglosa = String.Format("ANTICIPO CON TARJETA {0}", oBeTipoTarjeta.tcrc_vdescripcion_tipo_tarjeta_cred);
                        oBeBcoMovDet.iusuario_crea = oBeAntc.intUsuario;
                        oBeBcoMovDet.vpc_crea = oBeAntc.strPc;
                        oBeBcoMovDet.mobdc_flag_estado = true;
                        oBeBcoMovDet.doxcc_icod_correlativo = obj_DXC.doxcc_icod_correlativo;
                        oBeBcoMovDet.icod_correlativo_cabecera = oBeBcoMovCab.icod_correlativo;
                        //oBeBcoMovDet.docxc_icod_pago = x.pgoc_dxc_icod_pago;
                        //SE INSERTAR EL DETALLE
                        objTesoreriaData.InsertarLibroBancosDetalle(oBeBcoMovDet);
                        #endregion
                    }
                    #endregion
                    #region 5 Modificando el Anticipo
                    //En esta parte se modifica el anticipo
                    objVentasData.modificarAnticipo(oBeAntc);
                    //
                    #endregion
                    #region 6 Modificando Planilla Detalle
                    //En esta parte se modifica el detalle de la planilla y modificamos la cabecera de la pln, si no es el primer registro
                    objVentasData.modificarPlanillaCobranzaDetalle(oBePlnDet);
                    objVentasData.modificarPlanillaCobranzaCab(oBePlnCab);
                    //                                      
                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarAnticipoPln(EPlanillaCobranzaCab oBePlnCab, EPlanillaCobranzaDet oBePlnDet, EAnticipo oBeAntc)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    TesoreriaData objTesoreriaData = new TesoreriaData();

                    //En esta parte se elimina el adelanto del cliente y dcx a la vez en el mismo procedimiento
                    EAdelantoCliente oBeAdelanto = new EAdelantoCliente();
                    oBeAdelanto.icod_correlativo = oBeAntc.antc_icod_adelanto_cliente;
                    oBeAdelanto.iusuario_modifica = oBeAntc.intUsuario;
                    oBeAdelanto.vpc_modifica = oBeAntc.strPc;
                    objTesoreriaData.eliminarAdelantoCliente(oBeAdelanto);
                    //

                    #region 3 Eliminar el depósito de bancos si existe
                    if (Convert.ToInt32(oBePlnDet.intIcodEntidadFinanMov) > 0)
                        objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(oBePlnDet.intIcodEntidadFinanMov));
                    #endregion

                    //En esta parte se inserta el anticipo
                    objVentasData.eliminarAnticipo(oBeAntc);
                    //

                    //En esta parte se insertar el detalle de la planilla e modificamos la cabecera de la pln, si no es el primer registro
                    objVentasData.eliminarPlanillaCobranzaDetalle(oBePlnDet);
                    objVentasData.modificarPlanillaCobranzaCab(oBePlnCab);
                    //                                      

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Retención
        public List<ERetencion> listarRetencionCab(int intEjericio, int? intPeriodo)
        {
            List<ERetencion> lista = new List<ERetencion>();
            try
            {
                lista = objVentasData.listarRetencionCab(intEjericio, intPeriodo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarRetencionCab(ERetencion oBe_Ret, List<ERetencionDet> lstDetalle)
        {
            int id_retencion = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    oBe_Ret.retc_icod_comprobante_retencion = objVentasData.insertarRetencionCab(oBe_Ret);
                    lstDetalle.ForEach(x =>
                    {
                        #region Pago DXC
                        /*LA RETENCION SE DEBE GRABAR COMO PAGO DEL DOCUMENTO*/
                        EDocXCobrarPago obj_DXC_Pago = new EDocXCobrarPago();
                        obj_DXC_Pago.doxcc_icod_correlativo = Convert.ToInt64(x.intIcodDXC); //IdDocumentoPorCobrar
                        obj_DXC_Pago.tdocc_icod_tipo_doc = Parametros.intTipoDocRetencion;
                        obj_DXC_Pago.pdxcc_vnumero_doc = oBe_Ret.retc_vnumero_comprob_reten;
                        obj_DXC_Pago.pdxcc_sfecha_cobro = oBe_Ret.retc_sfecha_pago;
                        obj_DXC_Pago.tablc_iid_tipo_moneda = oBe_Ret.tablc_iid_moneda;
                        obj_DXC_Pago.pdxcc_nmonto_cobro = x.retd_nmto_retencion;
                        obj_DXC_Pago.pdxcc_nmonto_tipo_cambio = oBe_Ret.retc_nmto_tipo_cambio;
                        obj_DXC_Pago.pdxcc_vobservacion = String.Format("RETENCIÓN N°:{0}", oBe_Ret.retc_vnumero_comprob_reten);
                        //obj_DXC_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                        obj_DXC_Pago.cliec_icod_cliente = oBe_Ret.proc_icod_cliente;
                        //obj_DXC_Pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                        //obj_DXC_Pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                        //obj_DXC_Pago.anac_icod_analitica = item.tablc_icod_tipo_analitica;
                        //obj_DXC_Pago.anac_icod_analitica_det = item.icod_analitica;
                        obj_DXC_Pago.pdxcc_vorigen = "R";
                        obj_DXC_Pago.intUsuario = x.intUsuario;
                        obj_DXC_Pago.strPc = x.strPc;
                        obj_DXC_Pago.pdxcc_flag_estado = true;
                        obj_DXC_Pago.anac_iid_anio = oBe_Ret.anioc_iid_anio;
                        x.pdxpc_icod_correlativo = new CuentasPorCobrarData().InsertarPagoDirectoDocumentoXCobrar(obj_DXC_Pago);
                        /***************************************************************************/
                        #endregion
                        x.retc_icod_comprobante_retencion = oBe_Ret.retc_icod_comprobante_retencion;
                        objVentasData.insertarRetencionDet(x);
                        new TesoreriaData().ActualizarMontoDXCPagadoSaldo(obj_DXC_Pago.doxcc_icod_correlativo, obj_DXC_Pago.tablc_iid_tipo_moneda);
                    });
                    tx.Complete();
                }
                return id_retencion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int modificarRetencionCab(ERetencion oBe_Ret, List<ERetencionDet> lstDetalle, List<ERetencionDet> lstDelete)
        {
            int id_retencion = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarRetencionCab(oBe_Ret);

                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            #region Pago DXC
                            /*LA RETENCION SE DEBE GRABAR COMO PAGO DEL DOCUMENTO*/
                            EDocXCobrarPago obj_DXC_Pago = new EDocXCobrarPago();
                            obj_DXC_Pago.doxcc_icod_correlativo = Convert.ToInt64(x.intIcodDXC); //IdDocumentoPorCobrar
                            obj_DXC_Pago.tdocc_icod_tipo_doc = Parametros.intTipoDocRetencion;
                            obj_DXC_Pago.pdxcc_vnumero_doc = oBe_Ret.retc_vnumero_comprob_reten;
                            obj_DXC_Pago.pdxcc_sfecha_cobro = oBe_Ret.retc_sfecha_pago;
                            obj_DXC_Pago.tablc_iid_tipo_moneda = oBe_Ret.tablc_iid_moneda;
                            obj_DXC_Pago.pdxcc_nmonto_cobro = x.retd_nmto_retencion;
                            obj_DXC_Pago.pdxcc_nmonto_tipo_cambio = oBe_Ret.retc_nmto_tipo_cambio;
                            obj_DXC_Pago.pdxcc_vobservacion = String.Format("RETENCIÓN N°:{0}", oBe_Ret.retc_vnumero_comprob_reten);
                            //obj_DXC_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                            obj_DXC_Pago.cliec_icod_cliente = oBe_Ret.proc_icod_cliente;
                            //obj_DXC_Pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                            //obj_DXC_Pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                            //obj_DXC_Pago.anac_icod_analitica = item.tablc_icod_tipo_analitica;
                            //obj_DXC_Pago.anac_icod_analitica_det = item.icod_analitica;

                            obj_DXC_Pago.pdxcc_vorigen = "R";
                            obj_DXC_Pago.intUsuario = x.intUsuario;
                            obj_DXC_Pago.strPc = x.strPc;
                            obj_DXC_Pago.pdxcc_flag_estado = true;
                            obj_DXC_Pago.anac_iid_anio = oBe_Ret.anioc_iid_anio;
                            x.pdxpc_icod_correlativo = new CuentasPorCobrarData().InsertarPagoDirectoDocumentoXCobrar(obj_DXC_Pago);
                            /***************************************************************************/
                            #endregion
                            x.retc_icod_comprobante_retencion = oBe_Ret.retc_icod_comprobante_retencion;


                            new TesoreriaData().ActualizarMontoDXCPagadoSaldo(obj_DXC_Pago.doxcc_icod_correlativo, obj_DXC_Pago.tablc_iid_tipo_moneda);

                            objVentasData.insertarRetencionDet(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {
                            /*ELIMINAR PAGO ANTERIOR*/
                            EDocXCobrarPago obj_DXC_PagoDel = new EDocXCobrarPago();
                            obj_DXC_PagoDel.pdxcc_icod_correlativo = x.pdxpc_icod_correlativo;
                            obj_DXC_PagoDel.intUsuario = x.intUsuario;
                            obj_DXC_PagoDel.strPc = x.strPc;
                            new CuentasPorCobrarData().EliminarPagoDirectoDocumentoXCobrar(obj_DXC_PagoDel);
                            #region Pago DXC
                            /*LA RETENCION SE DEBE GRABAR COMO PAGO DEL DOCUMENTO*/
                            EDocXCobrarPago obj_DXC_Pago = new EDocXCobrarPago();
                            obj_DXC_Pago.doxcc_icod_correlativo = Convert.ToInt64(x.intIcodDXC); //IdDocumentoPorCobrar
                            obj_DXC_Pago.tdocc_icod_tipo_doc = Parametros.intTipoDocRetencion;
                            obj_DXC_Pago.pdxcc_vnumero_doc = oBe_Ret.retc_vnumero_comprob_reten;
                            obj_DXC_Pago.pdxcc_sfecha_cobro = oBe_Ret.retc_sfec_comprob_reten;
                            obj_DXC_Pago.tablc_iid_tipo_moneda = oBe_Ret.tablc_iid_moneda;
                            obj_DXC_Pago.pdxcc_nmonto_cobro = x.retd_nmto_retencion;
                            obj_DXC_Pago.pdxcc_nmonto_tipo_cambio = oBe_Ret.retc_nmto_tipo_cambio;
                            obj_DXC_Pago.pdxcc_vobservacion = String.Format("RETENCIÓN N°:{0}", oBe_Ret.retc_vnumero_comprob_reten);
                            //obj_DXC_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                            obj_DXC_Pago.cliec_icod_cliente = oBe_Ret.proc_icod_cliente;
                            //obj_DXC_Pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                            //obj_DXC_Pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                            //obj_DXC_Pago.anac_icod_analitica = item.tablc_icod_tipo_analitica;
                            //obj_DXC_Pago.anac_icod_analitica_det = item.icod_analitica;
                            obj_DXC_Pago.pdxcc_vorigen = "R";
                            obj_DXC_Pago.intUsuario = x.intUsuario;
                            obj_DXC_Pago.strPc = x.strPc;
                            obj_DXC_Pago.pdxcc_flag_estado = true;
                            x.pdxpc_icod_correlativo = new CuentasPorCobrarData().InsertarPagoDirectoDocumentoXCobrar(obj_DXC_Pago);
                            /***************************************************************************/
                            #endregion
                            objVentasData.modificarRetencionDet(x);
                            new TesoreriaData().ActualizarMontoDXCPagadoSaldo(obj_DXC_Pago.doxcc_icod_correlativo, obj_DXC_Pago.tablc_iid_tipo_moneda);
                        }
                    });

                    lstDelete.ForEach(x =>
                    {
                        EDocXCobrarPago obj_DXC_PagoDel = new EDocXCobrarPago();
                        obj_DXC_PagoDel.doxcc_icod_correlativo = x.intIcodDXC;
                        obj_DXC_PagoDel.pdxcc_icod_correlativo = x.pdxpc_icod_correlativo;
                        obj_DXC_PagoDel.intUsuario = x.intUsuario;
                        obj_DXC_PagoDel.strPc = x.strPc;
                        new CuentasPorCobrarData().EliminarPagoDirectoDocumentoXCobrar(obj_DXC_PagoDel);
                        new TesoreriaData().ActualizarMontoDXCPagadoSaldo(obj_DXC_PagoDel.doxcc_icod_correlativo, obj_DXC_PagoDel.tablc_iid_tipo_moneda);
                        objVentasData.eliminarRetencionDet(x);
                    });
                    tx.Complete();
                }
                return id_retencion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarRetencionCab(ERetencion oBe_Ret)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var lstDelete = listarRetencionDet(oBe_Ret.retc_icod_comprobante_retencion);
                    lstDelete.ForEach(x =>
                    {
                        EDocXCobrarPago obj_DXC_PagoDel = new EDocXCobrarPago();
                        obj_DXC_PagoDel.doxcc_icod_correlativo = x.intIcodDXC;
                        obj_DXC_PagoDel.pdxcc_icod_correlativo = x.pdxpc_icod_correlativo;
                        obj_DXC_PagoDel.intUsuario = x.intUsuario;
                        obj_DXC_PagoDel.strPc = x.strPc;
                        new CuentasPorCobrarData().EliminarPagoDirectoDocumentoXCobrar(obj_DXC_PagoDel);
                        new TesoreriaData().ActualizarMontoDXCPagadoSaldo(obj_DXC_PagoDel.doxcc_icod_correlativo, obj_DXC_PagoDel.tablc_iid_tipo_moneda);
                        objVentasData.eliminarRetencionDet(x);
                    });
                    objVentasData.eliminarRetencionCab(oBe_Ret);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ERetencionDet> listarRetencionDet(int intRetencion)
        {
            List<ERetencionDet> lista = new List<ERetencionDet>();
            try
            {
                lista = objVentasData.listarRetencionDet(intRetencion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        #endregion
        #region Nota Crédito Clientes
        public List<ENotaCredito> listarNotaCreditoClienteCab(int intEjericio)
        {
            List<ENotaCredito> lista = new List<ENotaCredito>();
            try
            {
                lista = objVentasData.listarNotaCreditoClienteCab(intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ENotaCredito> ConsultaNotaCreditoClienteCab(int intEjericio, DateTime dtFechaDesde, DateTime dtFechaHasta, int? intCliente)
        {
            List<ENotaCredito> lista = new List<ENotaCredito>();
            try
            {
                lista = objVentasData.ConsultaNotaCreditoClienteCab(intEjericio, dtFechaDesde, dtFechaHasta, intCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        private void insertarNotaCreditoClienteDetalle(ENotaCredito oBe, ENotaCreditoDet objDet)
        {
            objDet.ncrec_icod_credito = oBe.ncrec_icod_credito;
            if (oBe.ncrec_tipo_nota_credito == 1)//NOTA DE CREDITO COMERCIAL
            {
                for (int l = 0; l <= 9; l++)
                {
                    int index = l + 1;
                    if (Convert.ToInt32(CommonFunctions.GetValueItem(objDet, $"dcrec_cant_talla{index}")) != 0)
                    {
                        EProdKardex objk = new EProdKardex();
                        objk.kardc_ianio = Parametros.intEjercicio;
                        objk.kardx_sfecha = Convert.ToDateTime(oBe.ncrec_sfecha_credito);
                        objk.iid_almacen = Convert.ToInt32(objDet.almac_icod_almacen);
                        objk.iid_producto = Convert.ToInt32(CommonFunctions.GetValueItem(objDet, $"dcrec_icod_producto{index}"));
                        objk.puvec_icod_punto_venta = 2;
                        objk.Cantidad = Convert.ToDecimal(Convert.ToInt32(CommonFunctions.GetValueItem(objDet, $"dcrec_cant_talla{index}")));
                        objk.iid_tipo_doc = 36;
                        objk.NroDocumento = oBe.ncrec_vnumero_credito;
                        objk.movimiento = 1;
                        objk.Motivo = 101;
                        objk.Beneficiario = oBe.strDesCliente;
                        objk.Observaciones = "VENTA DE MERCADERIA";
                        objk.intUsuario = oBe.intUsuario;
                        objk.Item = Convert.ToInt32(objDet.dcrec_inro_item);
                        objk.stocc_codigo_producto = objDet.dcrec_vcodigo_extremo_prod;
                        objk.operacion = 1;
                        objk.kardc_iid_correlativo = new CentralData().InsertarKardexpvt(objk);
                        CommonFunctions.SetValueItem(objDet, $"dcrec_iid_kardex{index}", objk.kardc_iid_correlativo);
                        EProdStockProducto objStock = new EProdStockProducto()
                        {
                            stocc_ianio = Parametros.intEjercicio,
                            stocc_iid_almacen = objk.iid_almacen,
                            stocc_iid_producto = objk.iid_producto,
                            stocc_nstock_prod = objk.Cantidad,
                            intTipoMovimiento = objk.movimiento,
                        };
                        new CentralData().actualizarStockPvt(objStock);
                    }
                }
            }
            objVentasData.insertarNotaCreditoClienteDet(objDet);
        }



        public int insertarNotaCreditoClienteCab(ENotaCredito oBe, List<ENotaCreditoDet> lstDetalle)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    oBe.ncrec_icod_credito = objVentasData.insertarNotaCreditoClienteCab(oBe);
                    oBe.doc_icod_documento = oBe.ncrec_icod_credito;
                    oBe.IdCabecera = objVentasData.insertarNotaCreditoVentaElectronica(oBe);
                    new BAdministracionSistema().updateCorrelativoTipoDocumentoSeries(oBe.ncrec_vnumero_credito.Substring(0, 4), Convert.ToInt32(oBe.ncrec_vnumero_credito.Substring(4, 8)));

                    #region Dxc...
                    EDocXCobrar objDXC = new EDocXCobrar();
                    //objDXC.doxcc_icod_correlativo = objEBoleta.dxcc_iid_doc_por_cobrar;
                    objDXC.mesec_iid_mes = Convert.ToInt16(oBe.ncrec_sfecha_credito.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                    objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocNotaCredClienteDevolucion;
                    objDXC.doxcc_vnumero_doc = oBe.ncrec_vnumero_credito;
                    objDXC.cliec_icod_cliente = oBe.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = oBe.strDesCliente;
                    objDXC.doxcc_sfecha_doc = oBe.ncrec_sfecha_credito;
                    objDXC.doxcc_sfecha_vencimiento_doc = oBe.ncrec_sfecha_credito;
                    objDXC.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBe.ncrec_sfecha_credito);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.doxcc_vdescrip_transaccion = "";
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(oBe.ncrec_npor_imp_igv) > 0) ? oBe.ncrec_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(oBe.ncrec_npor_imp_igv) == 0) ? oBe.ncrec_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = oBe.ncrec_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = oBe.ncrec_nmonto_total - oBe.ncrec_nmonto_neto;
                    objDXC.doxcc_nmonto_total = oBe.ncrec_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = oBe.ncrec_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;
                    objDXC.doxcc_vobservaciones = "";
                    objDXC.intUsuario = oBe.intUsuario;
                    objDXC.strPc = oBe.strPc;
                    objDXC.tablc_iid_tipo_pago = 176;//Nota de Credito
                    //objDXC.docxc_icod_documento = oBe.ncrec_icod_credito;
                    objDXC.anio = oBe.ncrec_sfecha_credito.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "N";
                    oBe.ncrec_icod_dxc = new CuentasPorCobrarData().insertarDxc(objDXC, new List<EDocXCobrarCuentaContable>());
                    #endregion
                    objVentasData.modificarNotaCreditoClienteCab(oBe);//se modifica el registro, solo para poder contar con el icod del dxc
                    #region Detalle de la NC...

                    foreach (var item in lstDetalle)
                    {
                        insertarNotaCreditoClienteDetalle(oBe, item);
                        item.IdCabecera = oBe.IdCabecera;
                        objVentasData.insertarNotaCreditoVentaElectronicaDetalle(item);
                    }

                    #endregion


                    tx.Complete();
                }
                return oBe.ncrec_icod_credito;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void eliminarNotaCreditoClienteDetalle(ENotaCredito oBe, ENotaCreditoDet x)
        {
            x.intUsuario = oBe.intUsuario;
            x.strPc = oBe.strPc;
            if (oBe.ncrec_tipo_nota_credito == 1) //NOTA DE CREDITO COMERCIAL
            {
                for (int i = 0; i < 9; i++)
                {
                    int index = i + 1;

                    if (Convert.ToInt32(CommonFunctions.GetValueItem(x, $"dcrec_iid_kardex{index}")) > 0)
                    {
                        EProdKardex obKardexDel = new EProdKardex();
                        obKardexDel.kardc_iid_correlativo = Convert.ToInt32(CommonFunctions.GetValueItem(x, $"dcrec_iid_kardex{index}"));
                        obKardexDel.intUsuario = oBe.intUsuario;
                        obKardexDel.strPc = oBe.strPc;
                        new CentralData().EliminarKardexPvt(obKardexDel);

                        EProdStockProducto stck = new EProdStockProducto();
                        stck.stocc_ianio = oBe.ncrec_sfecha_credito.Year;
                        stck.stocc_iid_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        stck.stocc_iid_producto = Convert.ToInt32(CommonFunctions.GetValueItem(x, $"dcrec_icod_producto{index}"));
                        new CentralData().actualizarStockPvt(stck);
                    }

                }
            }
            objVentasData.eliminarNotaCreditoClienteDet(x);
        }


        private void modificarNotaCreditoClienteDetalle(ENotaCredito oBe, ENotaCreditoDet objDet)
        {
            objDet.ncrec_icod_credito = oBe.ncrec_icod_credito;
            if (oBe.ncrec_tipo_nota_credito == 1) //NOTA DE CREDITO COMERCIAL
            {
                for (int l = 0; l <= 9; l++)
                {
                    int index = l + 1;
                    if (Convert.ToInt32(CommonFunctions.GetValueItem(objDet, $"dcrec_iid_kardex{index}")) != 0 ||
                        Convert.ToInt32(CommonFunctions.GetValueItem(objDet, $"dcrec_cant_talla{index}")) != 0)
                    {
                        EProdKardex objk = new EProdKardex();
                        objk.kardc_iid_correlativo = Convert.ToInt32(CommonFunctions.GetValueItem(objDet, $"dcrec_iid_kardex{index}"));
                        objk.kardc_ianio = Parametros.intEjercicio;
                        objk.kardx_sfecha = Convert.ToDateTime(oBe.ncrec_sfecha_credito);
                        objk.iid_almacen = Convert.ToInt32(objDet.almac_icod_almacen);
                        objk.iid_producto = Convert.ToInt32(CommonFunctions.GetValueItem(objDet, $"dcrec_icod_producto{index}"));
                        objk.puvec_icod_punto_venta = 2;
                        objk.Cantidad = Convert.ToInt32(CommonFunctions.GetValueItem(objDet, $"dcrec_cant_talla{index}"));
                        objk.iid_tipo_doc = 36;
                        objk.NroDocumento = oBe.ncrec_vnumero_credito;
                        objk.movimiento = 1;
                        objk.Motivo = 101;
                        objk.Beneficiario = oBe.strDesCliente;
                        objk.Observaciones = "VENTA DE MERCADERIA";
                        objk.intUsuario = oBe.intUsuario;
                        objk.Item = Convert.ToInt32(objDet.dcrec_inro_item);
                        objk.stocc_codigo_producto = objDet.dcrec_vcodigo_extremo_prod;
                        objk.operacion = 1;
                        objk.iid_producto = Convert.ToInt32(CommonFunctions.GetValueItem(objDet, $"dcrec_icod_producto{index}"));
                        if (objk.kardc_iid_correlativo > 0 && objk.Cantidad == 0)
                            new CentralData().EliminarKardexPvt(objk);
                        if (objk.kardc_iid_correlativo > 0 && objk.Cantidad != 0)
                            new CentralData().modificarKardexPvt(objk);
                        if (objk.kardc_iid_correlativo == 0 && objk.Cantidad != 0)
                            objk.kardc_iid_correlativo = new CentralData().InsertarKardexpvt(objk);
                        CommonFunctions.SetValueItem(objDet, $"dcrec_iid_kardex{index}", objk.kardc_iid_correlativo);
                        EProdStockProducto objStock = new EProdStockProducto()
                        {
                            stocc_ianio = Parametros.intEjercicio,
                            stocc_iid_almacen = objk.iid_almacen,
                            stocc_iid_producto = objk.iid_producto,
                            stocc_nstock_prod = objk.Cantidad,
                            intTipoMovimiento = objk.movimiento,
                        };
                        new CentralData().actualizarStockPvt(objStock);
                    }
                }
            }
            objVentasData.insertarNotaCreditoClienteDet(objDet);
        }


        public void modificarNotaCreditoClienteCab(ENotaCredito oBe, List<ENotaCreditoDet> lstDetalle, List<ENotaCreditoDet> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarNotaCreditoClienteCab(oBe);
                    #region Dxc...
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = oBe.ncrec_icod_dxc;
                    objDXC.mesec_iid_mes = Convert.ToInt16(oBe.ncrec_sfecha_credito.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                    objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocNotaCredClienteDevolucion;
                    objDXC.doxcc_vnumero_doc = oBe.ncrec_vnumero_credito;
                    objDXC.cliec_icod_cliente = oBe.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = oBe.strDesCliente;
                    objDXC.doxcc_sfecha_doc = oBe.ncrec_sfecha_credito;
                    objDXC.doxcc_sfecha_vencimiento_doc = oBe.ncrec_sfecha_credito;
                    objDXC.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBe.ncrec_sfecha_credito);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.doxcc_vdescrip_transaccion = "";
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(oBe.ncrec_npor_imp_igv) > 0) ? oBe.ncrec_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(oBe.ncrec_npor_imp_igv) == 0) ? oBe.ncrec_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = oBe.ncrec_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = oBe.ncrec_nmonto_total - oBe.ncrec_nmonto_neto;
                    objDXC.doxcc_nmonto_total = oBe.ncrec_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = oBe.ncrec_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;
                    objDXC.doxcc_vobservaciones = "";
                    objDXC.intUsuario = oBe.intUsuario;
                    objDXC.tablc_iid_tipo_pago = 176;//Nota de Credito
                    objDXC.strPc = oBe.strPc;
                    objDXC.anio = oBe.ncrec_sfecha_credito.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "N";
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    new CuentasPorCobrarData().modificarDxc(objDXC, Lista, Lista);
                    #endregion

                    foreach (var item in lstDelete)
                    {
                        eliminarNotaCreditoClienteDetalle(oBe, item);
                    }

                    foreach (var item in lstDetalle)
                    {
                        if (item.dcrec_icod_detalle_credito > 0 && item.operacion == Enums.Operacion.Modificar)
                        {
                            modificarNotaCreditoClienteDetalle(oBe, item);
                        }
                        if (item.dcrec_icod_detalle_credito == 0)
                        {
                            insertarNotaCreditoClienteDetalle(oBe, item);
                        }
                    }


                    var estadoFac = new BVentas().obtnerEstadoFacturacion(oBe.doc_icod_documento, "07");
                    oBe.IdCabecera = estadoFac.IdCabecera;
                    objVentasData.modificarNotaCreditoVentaElectronica(oBe);
                    new BVentas().eliminarFacturaVentaElectronicaDetalle(estadoFac.IdCabecera);
                    foreach (var ob in lstDetalle)
                    {
                        ob.IdCabecera = estadoFac.IdCabecera;
                        objVentasData.insertarNotaCreditoVentaElectronicaDetalle(ob);
                    }
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaCreditoClienteCab(ENotaCredito oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarNotaCreditoClienteCab(oBe);
                    #region Dxc...
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = oBe.ncrec_icod_dxc;
                    objDXC.intUsuario = oBe.intUsuario;
                    objDXC.strPc = oBe.strPc;
                    new CuentasPorCobrarData().eliminarDxc(objDXC);
                    #endregion
                    //se elimina los items
                    var lstDelete = objVentasData.listarNotaCreditoClienteDet(oBe.ncrec_icod_credito);
                    foreach (var item in lstDelete)
                    {
                        eliminarNotaCreditoClienteDetalle(oBe, item);
                    }
                    var estadoFac = new BVentas().obtnerEstadoFacturacion(oBe.doc_icod_documento, "07");
                    oBe.IdCabecera = estadoFac.IdCabecera;
                    objVentasData.eliminarFacturaVentaElectronica(oBe.IdCabecera);
                    new BVentas().eliminarFacturaVentaElectronicaDetalle(estadoFac.IdCabecera);
                     
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void anularNotaCreditoClienteCab(ENotaCredito oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.anularNotaCreditoClienteCab(oBe);
                    #region Dxc...
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = oBe.ncrec_icod_dxc;
                    objDXC.intUsuario = oBe.intUsuario;
                    objDXC.strPc = oBe.strPc;
                    new CuentasPorCobrarData().AnularDocumentoXCobrar(objDXC);
                    #endregion
                    //se elimina los items
                    var lstDelete = objVentasData.listarNotaCreditoClienteDet(oBe.ncrec_icod_credito);
                    lstDelete.ForEach(x =>
                    {
                        #region elimina krd y stock...
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_iid_correlativo);
                        obKardexDel.intUsuario = oBe.intUsuario;
                        obKardexDel.strPc = oBe.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        #endregion
                        #region Stock...
                        EStock stck = new EStock();
                        stck.stocc_ianio = oBe.ncrec_sfecha_credito.Year;
                        stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        objAlmacenData.actualizarStock(stck);
                        #endregion
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objVentasData.eliminarNotaCreditoClienteDet(x);
                    });

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ENotaCreditoDet> listarNotaCreditoClienteDet(int intNotaCredito)
        {
            List<ENotaCreditoDet> lista = new List<ENotaCreditoDet>();
            try
            {
                lista = objVentasData.listarNotaCreditoClienteDet(intNotaCredito);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ENotaCreditoDet> listarNotaCreditoNoComercialClienteDet(int intNotaCredito)
        {
            List<ENotaCreditoDet> lista = new List<ENotaCreditoDet>();
            try
            {
                lista = objVentasData.listarNotaCreditoNoComercialClienteDet(intNotaCredito);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        #endregion


        #region Nota De Credito Suministros
        public List<ENotaCreditoSuministrosDet> listarNotaCreditoSuministrosClienteDet(int intNotaCredito)
        {
            List<ENotaCreditoSuministrosDet> lista = new List<ENotaCreditoSuministrosDet>();
            try
            {
                lista = objVentasData.listarNotaCreditoSuministrosClienteDet(intNotaCredito);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarNotaCreditoSuministrosClienteCab(ENotaCredito oBe, List<ENotaCreditoSuministrosDet> lstDetalle)
        {
            int intIcodE = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    oBe.ncrec_icod_credito = objVentasData.insertarNotaCreditoClienteCab(oBe);
                    oBe.doc_icod_documento = oBe.ncrec_icod_credito;
                    intIcodE = objVentasData.insertarNotaCreditoVentaElectronica(oBe);
                    //new BAdministracionSistema().updateCorrelativoTipoDocumentoPVT(2, Convert.ToInt32(oBe.ncrec_vnumero_credito.Substring(4, 8)), 3);
                    new BAdministracionSistema().updateCorrelativoTipoDocumentoSeries(oBe.ncrec_vnumero_credito.Substring(0, 4), Convert.ToInt32(oBe.ncrec_vnumero_credito.Substring(4, 8)));

                    #region Dxc...
                    EDocXCobrar objDXC = new EDocXCobrar();
                    //objDXC.doxcc_icod_correlativo = objEBoleta.dxcc_iid_doc_por_cobrar;
                    objDXC.mesec_iid_mes = Convert.ToInt16(oBe.ncrec_sfecha_credito.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                    objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocNotaCredClienteDevolucion;
                    objDXC.doxcc_vnumero_doc = oBe.ncrec_vnumero_credito;
                    objDXC.cliec_icod_cliente = oBe.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = oBe.strDesCliente;
                    objDXC.doxcc_sfecha_doc = oBe.ncrec_sfecha_credito;
                    objDXC.doxcc_sfecha_vencimiento_doc = oBe.ncrec_sfecha_credito;
                    objDXC.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBe.ncrec_sfecha_credito);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.doxcc_vdescrip_transaccion = "";
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(oBe.ncrec_npor_imp_igv) > 0) ? oBe.ncrec_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(oBe.ncrec_npor_imp_igv) == 0) ? oBe.ncrec_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = oBe.ncrec_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = oBe.ncrec_nmonto_total - oBe.ncrec_nmonto_neto;
                    objDXC.doxcc_nmonto_total = oBe.ncrec_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = oBe.ncrec_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;
                    objDXC.doxcc_vobservaciones = "";
                    objDXC.intUsuario = oBe.intUsuario;
                    objDXC.strPc = oBe.strPc;
                    objDXC.tablc_iid_tipo_pago = 176;//Nota de Credito
                                                     //objDXC.docxc_icod_documento = oBe.ncrec_icod_credito;
                    objDXC.anio = oBe.ncrec_sfecha_credito.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "N";
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    oBe.ncrec_icod_dxc = new CuentasPorCobrarData().insertarDxc(objDXC, Lista);
                    #endregion
                    objVentasData.modificarNotaCreditoClienteCab(oBe);//se modifica el registro, solo para poder contar con el icod del dxc
                    #region Detalle de la NC...

                    lstDetalle.ForEach(x =>
                    {


                        #region krd...
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_ianio = oBe.ncrec_sfecha_credito.Year;
                        obKardex.kardc_fecha_movimiento = oBe.ncrec_sfecha_credito;
                        obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.dcrec_ncantidad_producto);
                        obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                        obKardex.kardc_numero_doc = oBe.ncrec_vnumero_credito;
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                        obKardex.kardc_iid_motivo = Parametros.intMotivoKrdDevolucionProdIn;
                        obKardex.kardc_beneficiario = "";
                        obKardex.kardc_observaciones = "";
                        obKardex.intUsuario = oBe.intUsuario;
                        obKardex.strPc = oBe.strPc;
                        x.kardc_iid_correlativo = objAlmacenData.insertarKardex(obKardex);
                        #endregion
                        #region Stock...
                        EStock stck = new EStock();
                        stck.stocc_ianio = oBe.ncrec_sfecha_credito.Year;
                        stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        objAlmacenData.actualizarStock(stck);
                        #endregion
                        x.IdCabecera = intIcodE;
                        insertarNotaCreditoSuministrosVentaElectronicaDetalle(x);
                        x.ncrec_icod_credito = oBe.ncrec_icod_credito;
                        objVentasData.insertarNotaCreditoSuministrosClienteDet(x);





                    });

                    #endregion


                    tx.Complete();
                }
                return oBe.ncrec_icod_credito;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNotaCreditoSuministrosClienteCab(ENotaCredito oBe, List<ENotaCreditoSuministrosDet> lstDetalle, List<ENotaCreditoSuministrosDet> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarNotaCreditoClienteCab(oBe);
                    #region Dxc...
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = oBe.ncrec_icod_dxc;
                    objDXC.mesec_iid_mes = Convert.ToInt16(oBe.ncrec_sfecha_credito.Month);
                    objDXC.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                    objDXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocNotaCredClienteDevolucion;
                    objDXC.doxcc_vnumero_doc = oBe.ncrec_vnumero_credito;
                    objDXC.cliec_icod_cliente = oBe.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = oBe.strDesCliente;
                    objDXC.doxcc_sfecha_doc = oBe.ncrec_sfecha_credito;
                    objDXC.doxcc_sfecha_vencimiento_doc = oBe.ncrec_sfecha_credito;
                    objDXC.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBe.ncrec_sfecha_credito);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.doxcc_vdescrip_transaccion = "";
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(oBe.ncrec_npor_imp_igv) > 0) ? oBe.ncrec_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(oBe.ncrec_npor_imp_igv) == 0) ? oBe.ncrec_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = oBe.ncrec_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = oBe.ncrec_nmonto_total - oBe.ncrec_nmonto_neto;
                    objDXC.doxcc_nmonto_total = oBe.ncrec_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = oBe.ncrec_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;
                    objDXC.doxcc_vobservaciones = "";
                    objDXC.intUsuario = oBe.intUsuario;
                    objDXC.tablc_iid_tipo_pago = 176;//Nota de Credito
                    objDXC.strPc = oBe.strPc;
                    objDXC.anio = oBe.ncrec_sfecha_credito.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "N";
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    new CuentasPorCobrarData().modificarDxc(objDXC, Lista, Lista);
                    #endregion
                    //se elimina los items
                    lstDelete.ForEach(x =>
                    {
                        #region krd...
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_iid_correlativo);
                        obKardexDel.intUsuario = oBe.intUsuario;
                        obKardexDel.strPc = oBe.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        #endregion
                        #region Stock...
                        EStock stck = new EStock();
                        stck.stocc_ianio = oBe.ncrec_sfecha_credito.Year;
                        stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                        stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        objAlmacenData.actualizarStock(stck);
                        #endregion

                        #region Nc det...
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objVentasData.eliminarNotaCreditoClienteSuministrosDet(x);
                        #endregion
                    });
                    //se ingresa o modifica los items
                    lstDetalle.ForEach(x =>
                    {

                        if (x.intTipoOperacion == 1)
                        {

                            #region Registra el ingreso al krd...
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = oBe.ncrec_sfecha_credito.Year;
                            obKardex.kardc_fecha_movimiento = oBe.ncrec_sfecha_credito;
                            obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.dcrec_ncantidad_producto);
                            obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                            obKardex.kardc_numero_doc = oBe.ncrec_vnumero_credito;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                            obKardex.kardc_iid_motivo = Parametros.intMotivoKrdDevolucionProdIn;
                            obKardex.kardc_beneficiario = "";
                            obKardex.kardc_observaciones = "";
                            obKardex.intUsuario = oBe.intUsuario;
                            obKardex.strPc = oBe.strPc;
                            x.kardc_iid_correlativo = objAlmacenData.insertarKardex(obKardex);
                            #endregion
                            #region Actualiza el stock...
                            EStock stck = new EStock();
                            stck.stocc_ianio = oBe.ncrec_sfecha_credito.Year;
                            stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            objAlmacenData.actualizarStock(stck);
                            #endregion


                        }
                        else if (x.intTipoOperacion == 2)
                        {
                            #region elimina krd y stock...
                            EKardex obKardexDel = new EKardex();
                            obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_iid_correlativo);
                            obKardexDel.intUsuario = oBe.intUsuario;
                            obKardexDel.strPc = oBe.strPc;
                            objAlmacenData.eliminarKardex(obKardexDel);
                            #endregion
                            #region Actualiza el stock...
                            EStock stck = new EStock();
                            stck.stocc_ianio = oBe.ncrec_sfecha_credito.Year;
                            stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            objAlmacenData.actualizarStock(stck);
                            #endregion

                            #region Registra el ingreso modificado al krd...
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = oBe.ncrec_sfecha_credito.Year;
                            obKardex.kardc_fecha_movimiento = oBe.ncrec_sfecha_credito;
                            obKardex.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.dcrec_ncantidad_producto);
                            obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente;
                            obKardex.kardc_numero_doc = oBe.ncrec_vnumero_credito;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                            obKardex.kardc_iid_motivo = Parametros.intMotivoKrdDevolucionProdIn;
                            obKardex.kardc_beneficiario = "";
                            obKardex.kardc_observaciones = "";
                            obKardex.intUsuario = oBe.intUsuario;
                            obKardex.strPc = oBe.strPc;
                            x.kardc_iid_correlativo = objAlmacenData.insertarKardex(obKardex);
                            #endregion
                            #region Actualiza el stock...
                            EStock stckk = new EStock();
                            stckk.stocc_ianio = oBe.ncrec_sfecha_credito.Year;
                            stckk.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            stckk.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            objAlmacenData.actualizarStock(stckk);
                            #endregion

                            objVentasData.modificarNotaCreditoSuministrosClienteDet(x);

                        }




                    });

                    List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
                    List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == oBe.doc_icod_documento && x.tipoDocumento == "07").ToList();
                    oBe.IdCabecera = lstCab[0].IdCabecera;
                    objVentasData.modificarNotaCreditoVentaElectronica(oBe);
                    if (lstCab.Count > 0)
                    {
                        new BVentas().eliminarFacturaVentaElectronicaDetalle(lstCab[0].IdCabecera);
                        foreach (var ob in lstDetalle)
                        {
                            ob.IdCabecera = lstCab[0].IdCabecera;
                            insertarNotaCreditoSuministrosVentaElectronicaDetalle(ob);
                        }
                    }

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaCreditoSuministrosClienteCab(ENotaCredito oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarNotaCreditoClienteCab(oBe);
                    #region Dxc...
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = oBe.ncrec_icod_dxc;
                    objDXC.intUsuario = oBe.intUsuario;
                    objDXC.strPc = oBe.strPc;
                    new CuentasPorCobrarData().eliminarDxc(objDXC);
                    #endregion
                    //se elimina los items
                    var lstDelete = objVentasData.listarNotaCreditoSuministrosClienteDet(oBe.ncrec_icod_credito);
                    lstDelete.ForEach(x =>
                    {
                        if (x.intClasificacion != Parametros.intTipoPrdServicio)//SI EL ITEM, ES DIFERENTE DE SERVICIO, ENTONCES AFECTA EL KARDEX
                        {
                            //    #region elimina krd y stock...
                            //    EKardex obKardexDel = new EKardex();
                            //    obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_iid_correlativo);
                            //    obKardexDel.intUsuario = oBe.intUsuario;
                            //    obKardexDel.strPc = oBe.strPc;
                            //    objAlmacenData.eliminarKardex(obKardexDel);
                            //    #endregion
                            //    #region Stock...
                            //    EStock stck = new EStock();
                            //    stck.stocc_ianio = oBe.ncrec_sfecha_credito.Year;
                            //    stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                            //    stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            //    objAlmacenData.actualizarStock(stck);
                            //#endregion
                        }
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objVentasData.eliminarNotaCreditoClienteSuministrosDet(x);
                    });
                    //se ingresa o modifica los items

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Nota Debito Clientes
        public List<ENotaDebito> listarNotaDebitoClienteCab(int intEjericio)
        {
            List<ENotaDebito> lista = new List<ENotaDebito>();
            try
            {
                lista = objVentasData.listarNotaDebitoClienteCab(intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarNotaDebitoClienteCab(ENotaDebito oBe, List<ENotaDebitoDet> lstDetalle)
        {
            int intIcodE = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    oBe.ncrec_icod_credito = objVentasData.insertarNotaDebitoClienteCab(oBe);
                    oBe.doc_icod_documento = oBe.ncrec_icod_credito;
                    intIcodE = objVentasData.insertarNotaDebitoVentaElectronica(oBe);
                    //ACTUALIZAR EL CORRELATIVO DE LA FACTURA
                    new BAdministracionSistema().updateCorrelativoTipoDocumentoPVT(2, Convert.ToInt32(oBe.ncrec_vnumero_credito.Substring(4, 8)), 4);


                    #region Dxc...
                    EDocXCobrar objDXC = new EDocXCobrar();
                    //objDXC.doxcc_icod_correlativo = objEBoleta.dxcc_iid_doc_por_cobrar;
                    objDXC.mesec_iid_mes = Convert.ToInt16(oBe.ncrec_sfecha_credito.Month);
                    objDXC.tdocc_icod_tipo_doc = 110;//Nota Debito
                    objDXC.tdodc_iid_correlativo = 81;//NOTA DEBITO - INT.FINANC
                    objDXC.doxcc_vnumero_doc = oBe.ncrec_vnumero_credito;
                    objDXC.cliec_icod_cliente = oBe.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = oBe.strDesCliente;
                    objDXC.doxcc_sfecha_doc = oBe.ncrec_sfecha_credito;
                    objDXC.doxcc_sfecha_vencimiento_doc = oBe.ncrec_sfecha_credito;
                    objDXC.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBe.ncrec_sfecha_credito);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.doxcc_vdescrip_transaccion = "";
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(oBe.ncrec_npor_imp_igv) > 0) ? oBe.ncrec_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(oBe.ncrec_npor_imp_igv) == 0) ? oBe.ncrec_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = oBe.ncrec_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = oBe.ncrec_nmonto_total - oBe.ncrec_nmonto_neto;
                    objDXC.doxcc_nmonto_total = oBe.ncrec_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = oBe.ncrec_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;
                    objDXC.doxcc_vobservaciones = "";
                    objDXC.intUsuario = oBe.intUsuario;
                    objDXC.strPc = oBe.strPc;
                    objDXC.tablc_iid_tipo_pago = 176;//Nota de Credito
                                                     //objDXC.docxc_icod_documento = oBe.ncrec_icod_credito;
                    objDXC.anio = oBe.ncrec_sfecha_credito.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "N";
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    oBe.ncrec_icod_dxc = new CuentasPorCobrarData().insertarDxc(objDXC, Lista);
                    #endregion
                    objVentasData.modificarNotaDebitoClienteCab(oBe);//se modifica el registro, solo para poder contar con el icod del dxc
                    #region Detalle de la NC...

                    lstDetalle.ForEach(x =>
                    {
                        if (oBe.ncrec_tipo_nota_credito == 1)
                        {
                            if (x.intClasificacion != Parametros.intTipoPrdServicio)//SI EL ITEM, ES DIFERENTE DE SERVICIO, ENTONCES AFECTA EL KARDEX
                            {


                                x.IdCabecera = intIcodE;
                                insertarNotaDebitoVentaElectronicaDetalle(x);
                                x.ncrec_icod_credito = oBe.ncrec_icod_credito;
                                objVentasData.insertarNotaDebitoClienteDet(x);
                                //};


                            }
                            else
                            {
                                x.IdCabecera = intIcodE;
                                insertarNotaDebitoVentaElectronicaDetalle(x);
                                x.ncrec_icod_credito = oBe.ncrec_icod_credito;
                                objVentasData.insertarNotaDebitoClienteDet(x);
                            }
                        }
                        else
                        {
                            x.almac_icod_almacen = null;
                            x.kardc_iid_correlativo = null;
                            x.prdc_icod_producto = null;
                            x.IdCabecera = intIcodE;
                            insertarNotaDebitoVentaElectronicaDetalle(x);
                            x.ncrec_icod_credito = oBe.ncrec_icod_credito;
                            objVentasData.insertarNotaDebitoClienteDet(x);
                        }

                    });

                    #endregion


                    tx.Complete();
                }
                return oBe.ncrec_icod_credito;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNotaDebitoClienteCab(ENotaDebito oBe, List<ENotaDebitoDet> lstDetalle, List<ENotaDebitoDet> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarNotaDebitoClienteCab(oBe);
                    #region Dxc...
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = oBe.ncrec_icod_dxc;
                    objDXC.mesec_iid_mes = Convert.ToInt16(oBe.ncrec_sfecha_credito.Month);
                    objDXC.tdocc_icod_tipo_doc = 110;//Nota Debito
                    objDXC.tdodc_iid_correlativo = 81;//NOTA DEBITO - INT.FINANC
                    objDXC.doxcc_vnumero_doc = oBe.ncrec_vnumero_credito;
                    objDXC.cliec_icod_cliente = oBe.cliec_icod_cliente;
                    objDXC.cliec_vnombre_cliente = oBe.strDesCliente;
                    objDXC.doxcc_sfecha_doc = oBe.ncrec_sfecha_credito;
                    objDXC.doxcc_sfecha_vencimiento_doc = oBe.ncrec_sfecha_credito;
                    objDXC.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    objDXC.doxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(oBe.ncrec_sfecha_credito);
                    if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                        throw new ArgumentException("No se encontró Tipo de Cambio para la fecha de la factura, favor de registrar Tipo de Cambio");
                    objDXC.doxcc_vdescrip_transaccion = "";
                    objDXC.doxcc_nmonto_afecto = (Convert.ToInt32(oBe.ncrec_npor_imp_igv) > 0) ? oBe.ncrec_nmonto_neto : 0;
                    objDXC.doxcc_nmonto_inafecto = (Convert.ToInt32(oBe.ncrec_npor_imp_igv) == 0) ? oBe.ncrec_nmonto_neto : 0;
                    objDXC.doxcc_nporcentaje_igv = oBe.ncrec_npor_imp_igv;
                    objDXC.doxcc_nmonto_impuesto = oBe.ncrec_nmonto_total - oBe.ncrec_nmonto_neto;
                    objDXC.doxcc_nmonto_total = oBe.ncrec_nmonto_total;
                    objDXC.doxcc_nmonto_saldo = oBe.ncrec_nmonto_total;
                    objDXC.doxcc_nmonto_pagado = 0;
                    objDXC.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;
                    objDXC.doxcc_vobservaciones = "";
                    objDXC.intUsuario = oBe.intUsuario;
                    objDXC.tablc_iid_tipo_pago = 176;//Nota de Credito
                    objDXC.strPc = oBe.strPc;
                    objDXC.anio = oBe.ncrec_sfecha_credito.Year;
                    objDXC.doxcc_flag_estado = true;
                    objDXC.doxcc_origen = "N";
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    new CuentasPorCobrarData().modificarDxc(objDXC, Lista, Lista);
                    #endregion
                    //se elimina los items
                    lstDelete.ForEach(x =>
                    {
                        #region Nc det...
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objVentasData.eliminarNotaDebitoClienteDet(x);
                        #endregion
                    });
                    //se ingresa o modifica los items
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intClasificacion != Parametros.intTipoPrdServicio)//SI EL ITEM, ES DIFERENTE DE SERVICIO, ENTONCES AFECTA EL KARDEX
                        {
                            if (x.intTipoOperacion == 1)
                            {
                                x.ncrec_icod_credito = oBe.ncrec_icod_credito;
                                objVentasData.insertarNotaDebitoClienteDet(x);
                                //x.kardc_iid_correlativo = null;
                                //x.almac_icod_almacen = null;



                            }
                            else if (x.intTipoOperacion == 2)
                            {
                                objVentasData.modificarNotaDebitoClienteDet(x);
                            }

                        }
                        else
                        {
                            if (x.intTipoOperacion == 1)
                            {
                                //x.almac_icod_almacen = null;
                                //x.kardc_iid_correlativo = null;
                                x.ncrec_icod_credito = oBe.ncrec_icod_credito;
                                objVentasData.insertarNotaDebitoClienteDet(x);
                            }
                            else if (x.intTipoOperacion == 2)
                            {
                                //x.almac_icod_almacen = null;
                                //x.kardc_iid_correlativo = null;
                                x.ncrec_icod_credito = oBe.ncrec_icod_credito;
                                objVentasData.modificarNotaDebitoClienteDet(x);
                            }
                        }

                    });

                    List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
                    List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == oBe.doc_icod_documento && x.tipoDocumento == "08").ToList();
                    oBe.IdCabecera = lstCab[0].IdCabecera;
                    objVentasData.modificarNotaDebitoVentaElectronica(oBe);
                    if (lstCab.Count > 0)
                    {
                        new BVentas().eliminarFacturaVentaElectronicaDetalle(lstCab[0].IdCabecera);
                        foreach (var ob in lstDetalle)
                        {
                            ob.IdCabecera = lstCab[0].IdCabecera;
                            insertarNotaDebitoVentaElectronicaDetalle(ob);
                        }
                    }

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaDebitoClienteCab(ENotaDebito oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarNotaDebitoClienteCab(oBe);
                    #region Dxc...
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = oBe.ncrec_icod_dxc;
                    objDXC.intUsuario = oBe.intUsuario;
                    objDXC.strPc = oBe.strPc;
                    new CuentasPorCobrarData().eliminarDxc(objDXC);
                    #endregion
                    //se elimina los items
                    var lstDelete = objVentasData.listarNotaDebitoClienteDet(oBe.ncrec_icod_credito);
                    lstDelete.ForEach(x =>
                    {
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objVentasData.eliminarNotaDebitoClienteDet(x);
                    });
                    //se ingresa o modifica los items

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ENotaDebitoDet> listarNotaDebitoClienteDet(int intNotaCredito)
        {
            List<ENotaDebitoDet> lista = new List<ENotaDebitoDet>();
            try
            {
                lista = objVentasData.listarNotaDebitoClienteDet(intNotaCredito);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion




        #region Tipo Tarjeta
        public List<ETipoTarjeta> listarTipoTarjeta()
        {
            List<ETipoTarjeta> lista = null;
            try
            {
                lista = objVentasData.listarTipoTarjeta();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTipoTarjeta(ETipoTarjeta oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarTipoTarjeta(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTipoTarjeta(ETipoTarjeta oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarTipoTarjeta(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTipoTarjeta(ETipoTarjeta oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarTipoTarjeta(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Transportista
        public List<ETransportista> listarTransportista()
        {
            List<ETransportista> lista = null;
            try
            {
                lista = objVentasData.listarTransportista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTransportista(ETransportista oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarTransportista(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTransportista(ETransportista oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarTransportista(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTransportista(ETransportista oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarTransportista(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Guía Remisión
        public EGuiaRemision listarGuiaRemisionxID(int remic_icod_remision)
        {
            EGuiaRemision lista = null;
            try
            {
                lista = objVentasData.listarGuiaRemisionxID(remic_icod_remision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EGuiaRemision> listarGuiaRemision(int intEjercicio)
        {
            List<EGuiaRemision> lista = null;
            try
            {
                lista = objVentasData.listarGuiaRemision(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EGuiaRemision> listarGuiaRemisionElectronica(DateTime intEjercicio)
        {
            List<EGuiaRemision> lista = null;
            try
            {
                lista = objVentasData.listarGuiaRemisionElectronica(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EGuiaRemision> getGRCabVerificarNumero(string vnumero)
        {
            List<EGuiaRemision> lista = new List<EGuiaRemision>();
            try
            {
                lista = objVentasData.getGRCabVerificarNumero(vnumero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarGuiaRemision(EGuiaRemision oBe, List<EGuiaRemisionDet> lstDet, EConductor objConductor)
        {
            int intIcod = 0;
            try
            {
                var options = new TransactionOptions
                {
                    Timeout = TimeSpan.FromMinutes(10)
                };
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    intIcod = objVentasData.insertarGuiaRemision(oBe);
                    objConductor.remic_icod_remision = intIcod;
                    objVentasData.ConductorGuardar(objConductor);

                    oBe.guiaRemisionElectronica.remic_icod_remision = intIcod;
                    objVentasData.insertarGuiaRemisionElectronica(oBe.guiaRemisionElectronica);
                    #region Factura Det. Insertar
                    lstDet.ForEach(x =>
                    {
                        if (x.dremc_itipo_producto == (int)TipoProducto.Mercadería)
                        {



                            #region Salida de Kardex


                            object[] Tallas = new object[10];
                            object[] Cantidades = new object[10];
                            object[] idkardex = new object[10];
                            object[] idproducto = new object[10];
                            Tallas[0] = x.dremc_talla1;
                            Tallas[1] = x.dremc_talla2;
                            Tallas[2] = x.dremc_talla3;
                            Tallas[3] = x.dremc_talla4;
                            Tallas[4] = x.dremc_talla5;
                            Tallas[5] = x.dremc_talla6;
                            Tallas[6] = x.dremc_talla7;
                            Tallas[7] = x.dremc_talla8;
                            Tallas[8] = x.dremc_talla9;
                            Tallas[9] = x.dremc_talla10;
                            Cantidades[0] = x.dremc_cant_talla1;
                            Cantidades[1] = x.dremc_cant_talla2;
                            Cantidades[2] = x.dremc_cant_talla3;
                            Cantidades[3] = x.dremc_cant_talla4;
                            Cantidades[4] = x.dremc_cant_talla5;
                            Cantidades[5] = x.dremc_cant_talla6;
                            Cantidades[6] = x.dremc_cant_talla7;
                            Cantidades[7] = x.dremc_cant_talla8;
                            Cantidades[8] = x.dremc_cant_talla9;
                            Cantidades[9] = x.dremc_cant_talla10;
                            idproducto[0] = x.dremc_icod_producto1;
                            idproducto[1] = x.dremc_icod_producto2;
                            idproducto[2] = x.dremc_icod_producto3;
                            idproducto[3] = x.dremc_icod_producto4;
                            idproducto[4] = x.dremc_icod_producto5;
                            idproducto[5] = x.dremc_icod_producto6;
                            idproducto[6] = x.dremc_icod_producto7;
                            idproducto[7] = x.dremc_icod_producto8;
                            idproducto[8] = x.dremc_icod_producto9;
                            idproducto[9] = x.dremc_icod_producto10;

                            for (int i = 0; i <= 9; i++)
                            {
                                if (Convert.ToInt32(Cantidades[i]) != 0)
                                {

                                    EProdKardex objk = new EProdKardex();
                                    objk.kardx_sfecha = Convert.ToDateTime(oBe.remic_sfecha_inicio);
                                    objk.kardc_ianio = Parametros.intEjercicio;
                                    objk.iid_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                                    objk.iid_producto = Convert.ToInt32(idproducto[i]);
                                    objk.puvec_icod_punto_venta = 1;
                                    objk.Cantidad = Convert.ToDecimal(Cantidades[i]);
                                    objk.iid_tipo_doc = Parametros.intTipoDocGuiaRemision;
                                    objk.NroDocumento = oBe.remic_vnumero_remision;
                                    objk.movimiento = 0;
                                    objk.Motivo = (int)CommonFunctions.GetMotivoByMotivoGRE(oBe.tablc_iid_motivo);
                                    objk.Beneficiario = oBe.NomClie;
                                    objk.Observaciones = oBe.remic_vdescripcion_motivo;
                                    List<ETablaRegistro> lstTblaRegistro = new BGeneral().listarTablaRegistro(51).Where(t => t.tarec_iid_tabla_registro == oBe.tablc_iid_motivo).ToList();
                                    if (lstTblaRegistro.Count > 0)
                                    {
                                        if (lstTblaRegistro[0].tarec_iid_tabla_registro_salida > 0)
                                        {
                                            objk.Motivo = Convert.ToInt32(lstTblaRegistro[0].tarec_iid_tabla_registro_salida);
                                        }
                                    }
                                    objk.Beneficiario = "";
                                    objk.operacion = 1;
                                    objk.kardc_iid_correlativo = new CentralData().InsertarKardexpvt(objk);
                                    idkardex[i] = objk.kardc_iid_correlativo;
                                    idproducto[i] = objk.iid_producto;
                                    EProdStockProducto objStock = new EProdStockProducto()
                                    {
                                        stocc_ianio = Parametros.intEjercicio,
                                        stocc_iid_almacen = Convert.ToInt32(objk.iid_almacen),
                                        stocc_iid_producto = objk.iid_producto,
                                        stocc_nstock_prod = Convert.ToDecimal(Cantidades[i]),
                                        intTipoMovimiento = objk.movimiento,
                                    };
                                    new BCentral().actualizarStockPvt(objStock);

                                    #endregion
                                    if (oBe.tablc_iid_motivo == 226)
                                    {
                                        EProdKardex objkI = new EProdKardex();
                                        objkI.kardx_sfecha = Convert.ToDateTime(oBe.remic_sfecha_inicio);
                                        objkI.kardc_ianio = Parametros.intEjercicio;
                                        objkI.iid_almacen = Convert.ToInt32(oBe.almac_icod_almacen_ingreso);
                                        objkI.iid_producto = Convert.ToInt32(idproducto[i]);
                                        objkI.puvec_icod_punto_venta = 1;
                                        objkI.Cantidad = Convert.ToDecimal(Cantidades[i]);
                                        //objk.NroNota = Convert.ToInt32(objNotaSalida.salgc_inumero_nota_salida);
                                        objkI.iid_tipo_doc = Parametros.intTipoDocGuiaRemision;
                                        objkI.NroDocumento = oBe.remic_vnumero_remision;
                                        objkI.movimiento = 1;



                                        List<ETablaRegistro> lstTblaRegistro2 = new BGeneral().listarTablaRegistro(51).Where(t => t.tarec_iid_tabla_registro == oBe.tablc_iid_motivo).ToList();
                                        if (lstTblaRegistro2.Count > 0)
                                        {
                                            if (lstTblaRegistro2[0].tarec_iid_tabla_registro_ingreso > 0)
                                            {
                                                objkI.Motivo = Convert.ToInt32(lstTblaRegistro2[0].tarec_iid_tabla_registro_ingreso);
                                            }
                                        }

                                        objkI.Beneficiario = "";
                                        objkI.operacion = 1;
                                        objkI.kardc_iid_correlativo = new BCentral().InsertarKardexpvt(objkI);
                                        idkardex[i] = objk.kardc_iid_correlativo;
                                        idproducto[i] = objk.iid_producto;
                                        EProdStockProducto objStockI = new EProdStockProducto()
                                        {
                                            stocc_ianio = Parametros.intEjercicio,
                                            stocc_iid_almacen = Convert.ToInt32(objk.iid_almacen),
                                            stocc_iid_producto = objk.iid_producto,
                                            stocc_nstock_prod = Convert.ToDecimal(Cantidades[i]),
                                            intTipoMovimiento = objk.movimiento,
                                        };
                                        new BCentral().actualizarStockPvt(objStockI);
                                    }

                                }

                            }
                            x.dremc_iid_kardex1 = Convert.ToInt64(idkardex[0]);
                            x.dremc_iid_kardex2 = Convert.ToInt64(idkardex[1]);
                            x.dremc_iid_kardex3 = Convert.ToInt64(idkardex[2]);
                            x.dremc_iid_kardex4 = Convert.ToInt64(idkardex[3]);
                            x.dremc_iid_kardex5 = Convert.ToInt64(idkardex[4]);
                            x.dremc_iid_kardex6 = Convert.ToInt64(idkardex[5]);
                            x.dremc_iid_kardex7 = Convert.ToInt64(idkardex[6]);
                            x.dremc_iid_kardex8 = Convert.ToInt64(idkardex[7]);
                            x.dremc_iid_kardex9 = Convert.ToInt64(idkardex[8]);
                            x.dremc_iid_kardex10 = Convert.ToInt64(idkardex[9]);
                        }
                        x.remic_icod_remision = intIcod;
                        objVentasData.insertarGuiaRemisionDet(x);
                        x.electronicaDet.remic_icod_remision = intIcod;
                        objVentasData.insertarGuiaRemisionDetElectronica(x.generarGREdet());
                    });
                    #endregion

                    tx.Complete();
                }
                if (oBe.remic_vnumero_remision.Contains("T"))
                    new BAdministracionSistema().updateCorrelativoTipoDocumentoSeries(oBe.remic_vnumero_remision.Substring(0, 4), Convert.ToInt32(oBe.remic_vnumero_remision.Substring(4)));
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGuiaRemision(EGuiaRemision oBe, List<EGuiaRemisionDet> lstDet, List<EGuiaRemisionDet> lstDelete, EConductor objConductor)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarGuiaRemision(oBe);
                    objVentasData.EliminarGuiaRemisionElectronica(oBe.remic_icod_remision);
                    objVentasData.EliminarGuiaRemisionElectronicaDet(oBe.remic_icod_remision);
                    objVentasData.ConductorGuardar(objConductor);
                    oBe.guiaRemisionElectronica.remic_icod_remision = oBe.remic_icod_remision;
                    objVentasData.insertarGuiaRemisionElectronica(oBe.guiaRemisionElectronica);
                    lstDelete.ForEach(x =>
                    {

                        if (x.dremc_itipo_producto == (int)TipoProducto.Mercadería)
                        {


                            #region Eliminar Kardex
                            EKardex obKardexDel = new EKardex();
                            obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                            obKardexDel.intUsuario = oBe.intUsuario;
                            obKardexDel.strPc = oBe.strPc;
                            objAlmacenData.eliminarKardex(obKardexDel);
                            /*--------------------------------------------------------*/
                            #region Actualizando Stock
                            EStock stckS = new EStock();
                            stckS.stocc_ianio = Parametros.intEjercicio;
                            stckS.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            stckS.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stckS.stocc_stock_producto = x.dremc_ncantidad_producto;
                            stckS.intTipoMovimiento = 0;
                            objAlmacenData.actualizarStock(stckS);
                            #endregion
                            #endregion
                            if (oBe.tablc_iid_motivo == 226)
                            {
                                #region Eliminar Kardex Ingreso
                                EKardex obKardexDelIngreso = new EKardex();
                                obKardexDelIngreso.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ingreso);
                                obKardexDelIngreso.intUsuario = oBe.intUsuario;
                                obKardexDelIngreso.strPc = oBe.strPc;
                                objAlmacenData.eliminarKardex(obKardexDelIngreso);
                                /*----------------------------------------------------------------------------------*/
                                #region Actualizando Stock
                                EStock stck = new EStock();
                                stck.stocc_ianio = Parametros.intEjercicio;
                                stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                                stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                                stck.intTipoMovimiento = 1;
                                objAlmacenData.actualizarStock(stck);
                                #endregion
                                #endregion
                            }
                        }
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objVentasData.eliminarGuiaRemisionDet(x);
                    });

                    lstDet.ForEach(x =>
                    {

                        if (x.intTipoOperacion == 1)
                        {
                            if (x.dremc_itipo_producto == (int)TipoProducto.Mercadería)
                            {


                                #region Salida de Kardex
                                EKardex obKardex = new EKardex();
                                obKardex.kardc_ianio = Parametros.intEjercicio;
                                obKardex.kardc_fecha_movimiento = oBe.remic_sfecha_remision;
                                obKardex.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                                obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                                obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                                obKardex.kardc_numero_doc = oBe.remic_vnumero_remision;
                                obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                                obKardex.kardc_iid_motivo = (int)CommonFunctions.GetMotivoByMotivoGRE(oBe.tablc_iid_motivo);
                                obKardex.kardc_beneficiario = oBe.NomClie;
                                obKardex.kardc_observaciones = oBe.remic_vdescripcion_motivo;
                                List<ETablaRegistro> lstTblaRegistro = new BGeneral().listarTablaRegistro(51).Where(t => t.tarec_iid_tabla_registro == oBe.tablc_iid_motivo).ToList();
                                if (lstTblaRegistro.Count > 0)
                                {
                                    //if (lstTblaRegistro[0].tarec_iid_tabla_registro_ingreso > 0)
                                    //{
                                    //    obKardex.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro[0].tarec_iid_tabla_registro_ingreso);
                                    //}
                                    if (lstTblaRegistro[0].tarec_iid_tabla_registro_salida > 0)
                                    {
                                        obKardex.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro[0].tarec_iid_tabla_registro_salida);
                                    }
                                }

                                obKardex.kardc_beneficiario = "";
                                obKardex.kardc_observaciones = "";
                                obKardex.intUsuario = oBe.intUsuario;
                                obKardex.strPc = oBe.strPc;
                                x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                                /*------------------------------------------------------------------------*/
                                #region Actualizando Stock
                                EStock stckS = new EStock();
                                stckS.stocc_ianio = Parametros.intEjercicio;
                                stckS.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                                stckS.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                stckS.stocc_stock_producto = x.dremc_ncantidad_producto;
                                stckS.intTipoMovimiento = 0;
                                objAlmacenData.actualizarStock(stckS);
                                #endregion
                                #endregion

                                if (oBe.tablc_iid_motivo == 226)
                                {
                                    #region Ingreso de Kardex
                                    EKardex obKardexIngreso = new EKardex();
                                    obKardexIngreso.kardc_ianio = Parametros.intEjercicio;
                                    obKardexIngreso.kardc_fecha_movimiento = oBe.remic_sfecha_remision;
                                    obKardexIngreso.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen_ingreso);//por definir
                                    obKardexIngreso.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                    obKardexIngreso.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                                    obKardexIngreso.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                                    obKardexIngreso.kardc_numero_doc = oBe.remic_vnumero_remision;
                                    obKardexIngreso.kardc_tipo_movimiento = Parametros.intKardexIn;
                                    obKardexIngreso.kardc_iid_motivo = (int)CommonFunctions.GetMotivoByMotivoGRE(oBe.tablc_iid_motivo);
                                    obKardexIngreso.kardc_beneficiario = oBe.strDesAlmaceningreso;
                                    obKardexIngreso.kardc_observaciones = oBe.remic_vdescripcion_motivo;
                                    List<ETablaRegistro> lstTblaRegistro2 = new BGeneral().listarTablaRegistro(51).Where(t => t.tarec_iid_tabla_registro == oBe.tablc_iid_motivo).ToList();
                                    if (lstTblaRegistro2.Count > 0)
                                    {
                                        if (lstTblaRegistro2[0].tarec_iid_tabla_registro_ingreso > 0)
                                        {
                                            obKardexIngreso.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro2[0].tarec_iid_tabla_registro_ingreso);
                                        }
                                        //if (lstTblaRegistro2[0].tarec_iid_tabla_registro_salida > 0)
                                        //{
                                        //    obKardexIngreso.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro2[0].tarec_iid_tabla_registro_salida);
                                        //}
                                    }

                                    //obKardexIngreso.kardc_iid_motivo = Parametros.intMotivoKrdTransferenciaIn;
                                    obKardexIngreso.kardc_beneficiario = "";
                                    obKardexIngreso.kardc_observaciones = "";
                                    obKardexIngreso.intUsuario = oBe.intUsuario;
                                    obKardexIngreso.strPc = oBe.strPc;
                                    x.kardc_icod_correlativo_ingreso = objAlmacenData.insertarKardex(obKardexIngreso);
                                    /*-------------------------------------------------------------------------*/
                                    #region Actualizando Stock
                                    EStock stck = new EStock();
                                    stck.stocc_ianio = Parametros.intEjercicio;
                                    stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                                    stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                    stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                                    stck.intTipoMovimiento = 1;
                                    objAlmacenData.actualizarStock(stck);
                                    #endregion
                                    #endregion
                                }
                            }

                            x.remic_icod_remision = oBe.remic_icod_remision;
                            objVentasData.insertarGuiaRemisionDet(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {
                            if (x.dremc_itipo_producto == (int)TipoProducto.Mercadería)
                            {


                                #region Eliminar Kardex Previo
                                EKardex obKardexDel = new EKardex();
                                obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                                obKardexDel.intUsuario = oBe.intUsuario;
                                obKardexDel.strPc = oBe.strPc;
                                objAlmacenData.eliminarKardex(obKardexDel);
                                /*---------------------------------------------------------------------*/

                                #endregion
                                if (oBe.tablc_iid_motivo == 226)
                                {
                                    #region Eliminar Kardex Ingreso
                                    EKardex obKardexDelIngreso = new EKardex();
                                    obKardexDelIngreso.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ingreso);
                                    obKardexDelIngreso.intUsuario = oBe.intUsuario;
                                    obKardexDelIngreso.strPc = oBe.strPc;
                                    objAlmacenData.eliminarKardex(obKardexDelIngreso);
                                    /*---------------------------------------------------------------*/
                                    EKardex obKardexIngreso = new EKardex();
                                    obKardexIngreso.kardc_ianio = Parametros.intEjercicio;
                                    obKardexIngreso.kardc_fecha_movimiento = oBe.remic_sfecha_remision;
                                    obKardexIngreso.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen_ingreso);//por definir
                                    obKardexIngreso.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                    obKardexIngreso.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                                    obKardexIngreso.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                                    obKardexIngreso.kardc_numero_doc = oBe.remic_vnumero_remision;
                                    obKardexIngreso.kardc_tipo_movimiento = Parametros.intKardexIn;

                                    List<ETablaRegistro> lstTblaRegistro2 = new BGeneral().listarTablaRegistro(51).Where(t => t.tarec_iid_tabla_registro == oBe.tablc_iid_motivo).ToList();
                                    if (lstTblaRegistro2.Count > 0)
                                    {
                                        if (lstTblaRegistro2[0].tarec_iid_tabla_registro_ingreso > 0)
                                        {
                                            obKardexIngreso.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro2[0].tarec_iid_tabla_registro_ingreso);
                                        }
                                        //if (lstTblaRegistro2[0].tarec_iid_tabla_registro_salida > 0)
                                        //{
                                        //    obKardexIngreso.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro2[0].tarec_iid_tabla_registro_salida);
                                        //}
                                    }

                                    //obKardexIngreso.kardc_iid_motivo = Parametros.intMotivoKrdTransferenciaIn;
                                    obKardexIngreso.kardc_beneficiario = "";
                                    obKardexIngreso.kardc_observaciones = "";
                                    obKardexIngreso.intUsuario = oBe.intUsuario;
                                    obKardexIngreso.strPc = oBe.strPc;
                                    x.kardc_icod_correlativo_ingreso = objAlmacenData.insertarKardex(obKardexIngreso);
                                    #region Actualizando Stock
                                    EStock stck = new EStock();
                                    stck.stocc_ianio = Parametros.intEjercicio;
                                    stck.almac_icod_almacen = Convert.ToInt32(obKardexIngreso.almac_icod_almacen);
                                    stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                    stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                                    stck.intTipoMovimiento = 1;
                                    objAlmacenData.actualizarStock(stck);
                                    #endregion
                                    #endregion
                                }
                                #region Salida de Kardex
                                EKardex obKardex = new EKardex();
                                obKardex.kardc_ianio = Parametros.intEjercicio;
                                obKardex.kardc_fecha_movimiento = oBe.remic_sfecha_remision;
                                obKardex.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                                obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                                obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                                obKardex.kardc_numero_doc = oBe.remic_vnumero_remision;
                                obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                                obKardex.kardc_iid_motivo = (int)CommonFunctions.GetMotivoByMotivoGRE(oBe.tablc_iid_motivo);
                                obKardex.kardc_beneficiario = oBe.NomClie;
                                obKardex.kardc_observaciones = oBe.remic_vdescripcion_motivo;
                                List<ETablaRegistro> lstTblaRegistro = new BGeneral().listarTablaRegistro(51).Where(t => t.tarec_iid_tabla_registro == oBe.tablc_iid_motivo).ToList();
                                if (lstTblaRegistro.Count > 0)
                                {
                                    //if (lstTblaRegistro[0].tarec_iid_tabla_registro_ingreso > 0)
                                    //{
                                    //    obKardex.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro[0].tarec_iid_tabla_registro_ingreso);
                                    //}
                                    if (lstTblaRegistro[0].tarec_iid_tabla_registro_salida > 0)
                                    {
                                        obKardex.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro[0].tarec_iid_tabla_registro_salida);
                                    }
                                }

                                //obKardex.kardc_iid_motivo = oBe.tablc_iid_motivo;
                                obKardex.kardc_beneficiario = "";
                                obKardex.kardc_observaciones = "";
                                obKardex.intUsuario = oBe.intUsuario;
                                obKardex.strPc = oBe.strPc;
                                x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                                /*----------------------------------------------------------------*/
                                #region Actualizando Stock
                                EStock stckS = new EStock();
                                stckS.stocc_ianio = Parametros.intEjercicio;
                                stckS.almac_icod_almacen = Convert.ToInt32(obKardex.almac_icod_almacen);
                                stckS.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                stckS.stocc_stock_producto = x.dremc_ncantidad_producto;
                                stckS.intTipoMovimiento = 0;
                                objAlmacenData.actualizarStock(stckS);
                                #endregion
                                #endregion
                            }

                            x.intUsuario = oBe.intUsuario;
                            x.strPc = oBe.strPc;
                            objVentasData.modificarGuiaRemisionDet(x);
                        }
                        objVentasData.insertarGuiaRemisionDetElectronica(x.generarGREdet());
                    });

                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarGuiaRemision(EGuiaRemision oBe, List<EGuiaRemisionDet> lstDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarGuiaRemision(oBe);

                    lstDetalle.ForEach(x =>
                    {


                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                        obKardexDel.intUsuario = oBe.intUsuario;
                        obKardexDel.strPc = oBe.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        /*------------------------------------------------------*/
                        #region Actualizando Stock
                        EStock stckS = new EStock();
                        stckS.stocc_ianio = Parametros.intEjercicio;
                        stckS.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                        stckS.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        stckS.stocc_stock_producto = x.dremc_ncantidad_producto;
                        stckS.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stckS);
                        #endregion
                        #endregion
                        if (oBe.tablc_iid_motivo == 226)
                        {
                            #region Eliminar Kardex Ingreso
                            EKardex obKardexDelIngreso = new EKardex();
                            obKardexDelIngreso.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ingreso);
                            obKardexDelIngreso.intUsuario = oBe.intUsuario;
                            obKardexDelIngreso.strPc = oBe.strPc;
                            objAlmacenData.eliminarKardex(obKardexDelIngreso);
                            /*--------------------------------------------------------------*/
                            #region Actualizando Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = Parametros.intEjercicio;
                            stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                            stck.intTipoMovimiento = 1;
                            objAlmacenData.actualizarStock(stck);
                            #endregion
                            #endregion
                        }

                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objVentasData.eliminarGuiaRemisionDet(x);
                    });

                    //lstDelete.ForEach(x =>
                    //{
                    //    x.intUsuario = oBe.intUsuario;
                    //    x.strPc = oBe.strPc;
                    //    objVentasData.eliminarGuiaRemisionDet(x);
                    //});

                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void anularGuiaRemision(EGuiaRemision oBe, List<EGuiaRemisionDet> lstDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {


                    lstDetalle.ForEach(x =>
                    {


                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                        obKardexDel.intUsuario = oBe.intUsuario;
                        obKardexDel.strPc = oBe.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        /*------------------------------------------------------*/
                        #region Actualizando Stock
                        EStock stckS = new EStock();
                        stckS.stocc_ianio = Parametros.intEjercicio;
                        stckS.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                        stckS.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        stckS.stocc_stock_producto = x.dremc_ncantidad_producto;
                        stckS.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stckS);
                        #endregion
                        #endregion
                        if (oBe.tablc_iid_motivo == 226)
                        {
                            #region Eliminar Kardex Ingreso
                            EKardex obKardexDelIngreso = new EKardex();
                            obKardexDelIngreso.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ingreso);
                            obKardexDelIngreso.intUsuario = oBe.intUsuario;
                            obKardexDelIngreso.strPc = oBe.strPc;
                            objAlmacenData.eliminarKardex(obKardexDelIngreso);
                            /*--------------------------------------------------------------*/
                            #region Actualizando Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = Parametros.intEjercicio;
                            stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                            stck.intTipoMovimiento = 1;
                            objAlmacenData.actualizarStock(stck);
                            #endregion
                            #endregion
                        }

                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objVentasData.eliminarGuiaRemisionDet(x);
                    });
                    objVentasData.anularGuiaRemision(oBe);
                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int insertarGuiaRemisionMP(EGuiaRemision oBe, List<EGuiaRemisionMPDet> lstMPDet, EConductor objConductor)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarGuiaRemision(oBe);
                    objConductor.remic_icod_remision = intIcod;
                    objVentasData.ConductorGuardar(objConductor);

                    oBe.guiaRemisionElectronica.remic_icod_remision = intIcod;
                    objVentasData.insertarGuiaRemisionElectronica(oBe.guiaRemisionElectronica);
                    #region Factura Det. Insertar
                    lstMPDet.ForEach(x =>
                    {
                        if (x.dremc_itipo_producto == (int)TipoProducto.Mercadería)
                        {


                            #region Salida de Kardex
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = Parametros.intEjercicio;
                            obKardex.kardc_fecha_movimiento = Convert.ToDateTime(oBe.remic_sfecha_inicio);
                            obKardex.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                            obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                            obKardex.kardc_numero_doc = oBe.remic_vnumero_remision;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.kardc_iid_motivo = (int)CommonFunctions.GetMotivoByMotivoGRE(oBe.tablc_iid_motivo);
                            obKardex.kardc_beneficiario = oBe.strDesAlmaceningreso;
                            obKardex.kardc_observaciones = oBe.remic_vdescripcion_motivo;
                            List<ETablaRegistro> lstTblaRegistro = new BGeneral().listarTablaRegistro(51).Where(t => t.tarec_iid_tabla_registro == oBe.tablc_iid_motivo).ToList();
                            if (lstTblaRegistro.Count > 0)
                            {
                                //if (lstTblaRegistro[0].tarec_iid_tabla_registro_ingreso > 0)
                                //{
                                //    obKardex.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro[0].tarec_iid_tabla_registro_ingreso);
                                //}
                                if (lstTblaRegistro[0].tarec_iid_tabla_registro_salida > 0)
                                {
                                    obKardex.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro[0].tarec_iid_tabla_registro_salida);
                                }
                            }

                            //obKardex.kardc_iid_motivo = oBe.tablc_iid_motivo;
                            obKardex.kardc_beneficiario = "";
                            obKardex.kardc_observaciones = "";
                            obKardex.intUsuario = oBe.intUsuario;
                            obKardex.strPc = oBe.strPc;
                            x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                            /*--------------------------------------------------------------------*/
                            #region Actualizando Stock
                            EStock stckS = new EStock();
                            stckS.stocc_ianio = Parametros.intEjercicio;
                            stckS.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            stckS.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stckS.stocc_stock_producto = x.dremc_ncantidad_producto;
                            stckS.intTipoMovimiento = 0;
                            objAlmacenData.actualizarStock(stckS);
                            #endregion
                            #endregion
                            if (oBe.tablc_iid_motivo == 226)
                            {
                                #region Ingreso de Kardex
                                EKardex obKardexIngreso = new EKardex();
                                obKardexIngreso.kardc_ianio = Parametros.intEjercicio;
                                obKardexIngreso.kardc_fecha_movimiento = Convert.ToDateTime(oBe.remic_sfecha_inicio);
                                obKardexIngreso.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen_ingreso);//por definir
                                obKardexIngreso.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                obKardexIngreso.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                                obKardexIngreso.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                                obKardexIngreso.kardc_numero_doc = oBe.remic_vnumero_remision;
                                obKardexIngreso.kardc_tipo_movimiento = Parametros.intKardexIn;
                                obKardexIngreso.kardc_iid_motivo = (int)CommonFunctions.GetMotivoByMotivoGRE(oBe.tablc_iid_motivo);
                                obKardexIngreso.kardc_beneficiario = oBe.strDesAlmaceningreso;
                                obKardexIngreso.kardc_observaciones = oBe.remic_vdescripcion_motivo;
                                List<ETablaRegistro> lstTblaRegistro2 = new BGeneral().listarTablaRegistro(51).Where(t => t.tarec_iid_tabla_registro == oBe.tablc_iid_motivo).ToList();
                                if (lstTblaRegistro2.Count > 0)
                                {
                                    if (lstTblaRegistro2[0].tarec_iid_tabla_registro_ingreso > 0)
                                    {
                                        obKardex.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro2[0].tarec_iid_tabla_registro_ingreso);
                                    }
                                    //if (lstTblaRegistro2[0].tarec_iid_tabla_registro_salida > 0)
                                    //{
                                    //    obKardex.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro2[0].tarec_iid_tabla_registro_salida);
                                    //}
                                }

                                //obKardexIngreso.kardc_iid_motivo = Parametros.intMotivoKrdTransferenciaIn;
                                obKardexIngreso.kardc_beneficiario = "";
                                obKardexIngreso.kardc_observaciones = "";
                                obKardexIngreso.intUsuario = oBe.intUsuario;
                                obKardexIngreso.strPc = oBe.strPc;
                                x.kardc_icod_correlativo_ingreso = objAlmacenData.insertarKardex(obKardexIngreso);
                                /*-----------------------------------------------------------------------------*/
                                #region Actualizando Stock
                                EStock stck = new EStock();
                                stck.stocc_ianio = Parametros.intEjercicio;
                                stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen_ingreso);
                                stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                                stck.intTipoMovimiento = 1;
                                objAlmacenData.actualizarStock(stck);
                                #endregion
                                #endregion
                            }
                        }
                        x.remic_icod_remision = intIcod;
                        objVentasData.insertarGuiaRemisionMPDet(x);
                        x.electronicaDet.remic_icod_remision = intIcod;
                        objVentasData.insertarGuiaRemisionDetElectronica(x.electronicaDet);
                    });
                    #endregion
                    if (oBe.remic_vnumero_remision.Contains("T"))
                        new BAdministracionSistema().updateCorrelativoTipoDocumentoSeries(oBe.remic_vnumero_remision.Substring(0, 4), Convert.ToInt32(oBe.remic_vnumero_remision.Substring(4)));
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGuiaRemisionMP(EGuiaRemision oBe, List<EGuiaRemisionMPDet> lstMPDet, List<EGuiaRemisionMPDet> lstMPDelete, EConductor objConductor)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarGuiaRemision(oBe);
                    objVentasData.ConductorGuardar(objConductor);
                    objVentasData.EliminarGuiaRemisionElectronica(oBe.remic_icod_remision);
                    objVentasData.EliminarGuiaRemisionElectronicaDet(oBe.remic_icod_remision);

                    oBe.guiaRemisionElectronica.remic_icod_remision = oBe.remic_icod_remision;
                    objVentasData.insertarGuiaRemisionElectronica(oBe.guiaRemisionElectronica);

                    lstMPDelete.ForEach(x =>
                    {


                        if (x.dremc_itipo_producto == (int)TipoProducto.Mercadería)
                        {
                            #region Eliminar Kardex
                            EKardex obKardexDel = new EKardex();
                            obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                            obKardexDel.intUsuario = oBe.intUsuario;
                            obKardexDel.strPc = oBe.strPc;
                            objAlmacenData.eliminarKardex(obKardexDel);
                            /*--------------------------------------------------------*/
                            #region Actualizando Stock
                            EStock stckS = new EStock();
                            stckS.stocc_ianio = Parametros.intEjercicio;
                            stckS.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            stckS.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stckS.stocc_stock_producto = x.dremc_ncantidad_producto;
                            stckS.intTipoMovimiento = 0;
                            objAlmacenData.actualizarStock(stckS);
                            #endregion
                            #endregion
                            if (oBe.tablc_iid_motivo == 226)
                            {
                                #region Eliminar Kardex Ingreso
                                EKardex obKardexDelIngreso = new EKardex();
                                obKardexDelIngreso.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ingreso);
                                obKardexDelIngreso.intUsuario = oBe.intUsuario;
                                obKardexDelIngreso.strPc = oBe.strPc;
                                objAlmacenData.eliminarKardex(obKardexDelIngreso);
                                /*----------------------------------------------------------------------------------*/
                                #region Actualizando Stock
                                EStock stck = new EStock();
                                stck.stocc_ianio = Parametros.intEjercicio;
                                stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                                stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                                stck.intTipoMovimiento = 1;
                                objAlmacenData.actualizarStock(stck);
                                #endregion
                                #endregion
                            }
                        }

                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objVentasData.eliminarGuiaRemisionMPDet(x);
                    });

                    lstMPDet.ForEach(x =>
                    {

                        if (x.intTipoOperacion == 1)
                        {
                            if (x.dremc_itipo_producto == (int)TipoProducto.Mercadería)
                            {
                                #region Salida de Kardex
                                EKardex obKardex = new EKardex();
                                obKardex.kardc_ianio = Parametros.intEjercicio;
                                obKardex.kardc_fecha_movimiento = oBe.remic_sfecha_inicio;
                                obKardex.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                                obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                                obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                                obKardex.kardc_numero_doc = oBe.remic_vnumero_remision;
                                obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                                obKardex.kardc_iid_motivo = (int)CommonFunctions.GetMotivoByMotivoGRE(oBe.tablc_iid_motivo);
                                List<ETablaRegistro> lstTblaRegistro = new BGeneral().listarTablaRegistro(51).Where(t => t.tarec_iid_tabla_registro == oBe.tablc_iid_motivo).ToList();
                                if (lstTblaRegistro.Count > 0)
                                {
                                    //if (lstTblaRegistro[0].tarec_iid_tabla_registro_ingreso > 0)
                                    //{
                                    //    obKardex.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro[0].tarec_iid_tabla_registro_ingreso);
                                    //}
                                    if (lstTblaRegistro[0].tarec_iid_tabla_registro_salida > 0)
                                    {
                                        obKardex.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro[0].tarec_iid_tabla_registro_salida);
                                    }
                                }

                                //obKardex.kardc_iid_motivo = oBe.tablc_iid_motivo;
                                obKardex.kardc_beneficiario = "";
                                obKardex.kardc_observaciones = "";
                                obKardex.intUsuario = oBe.intUsuario;
                                obKardex.strPc = oBe.strPc;
                                x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                                /*------------------------------------------------------------------------*/
                                #region Actualizando Stock
                                EStock stckS = new EStock();
                                stckS.stocc_ianio = Parametros.intEjercicio;
                                stckS.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                                stckS.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                stckS.stocc_stock_producto = x.dremc_ncantidad_producto;
                                stckS.intTipoMovimiento = 0;
                                objAlmacenData.actualizarStock(stckS);
                                #endregion
                                #endregion


                                if (oBe.tablc_iid_motivo == 226)
                                {
                                    #region Ingreso de Kardex
                                    EKardex obKardexIngreso = new EKardex();
                                    obKardexIngreso.kardc_ianio = Parametros.intEjercicio;
                                    obKardexIngreso.kardc_fecha_movimiento = oBe.remic_sfecha_inicio;
                                    obKardexIngreso.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen_ingreso);//por definir
                                    obKardexIngreso.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                    obKardexIngreso.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                                    obKardexIngreso.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                                    obKardexIngreso.kardc_numero_doc = oBe.remic_vnumero_remision;
                                    obKardexIngreso.kardc_tipo_movimiento = Parametros.intKardexIn;
                                    obKardexIngreso.kardc_iid_motivo = (int)CommonFunctions.GetMotivoByMotivoGRE(oBe.tablc_iid_motivo);
                                    List<ETablaRegistro> lstTblaRegistro2 = new BGeneral().listarTablaRegistro(51).Where(t => t.tarec_iid_tabla_registro == oBe.tablc_iid_motivo).ToList();
                                    if (lstTblaRegistro2.Count > 0)
                                    {
                                        if (lstTblaRegistro2[0].tarec_iid_tabla_registro_ingreso > 0)
                                        {
                                            obKardex.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro2[0].tarec_iid_tabla_registro_ingreso);
                                        }
                                        //if (lstTblaRegistro2[0].tarec_iid_tabla_registro_salida > 0)
                                        //{
                                        //    obKardex.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro2[0].tarec_iid_tabla_registro_salida);
                                        //}
                                    }

                                    //obKardexIngreso.kardc_iid_motivo = Parametros.intMotivoKrdTransferenciaIn;
                                    obKardexIngreso.kardc_beneficiario = "";
                                    obKardexIngreso.kardc_observaciones = "";
                                    obKardexIngreso.intUsuario = oBe.intUsuario;
                                    obKardexIngreso.strPc = oBe.strPc;
                                    x.kardc_icod_correlativo_ingreso = objAlmacenData.insertarKardex(obKardexIngreso);
                                    /*-------------------------------------------------------------------------*/
                                    #region Actualizando Stock
                                    EStock stck = new EStock();
                                    stck.stocc_ianio = Parametros.intEjercicio;
                                    stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                                    stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                    stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                                    stck.intTipoMovimiento = 1;
                                    objAlmacenData.actualizarStock(stck);
                                    #endregion
                                    #endregion
                                }
                            }

                            x.remic_icod_remision = oBe.remic_icod_remision;
                            objVentasData.insertarGuiaRemisionMPDet(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {
                            if (x.dremc_itipo_producto == (int)TipoProducto.Mercadería)
                            {

                                if (oBe.tablc_iid_motivo == 226)
                                {
                                    #region Eliminar Kardex Ingreso
                                    /*---------------------------------------------------------------*/
                                    EKardex obKardexIngreso = new EKardex();
                                    obKardexIngreso.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ingreso);
                                    obKardexIngreso.kardc_ianio = Parametros.intEjercicio;
                                    obKardexIngreso.kardc_fecha_movimiento = oBe.remic_sfecha_inicio;
                                    obKardexIngreso.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen_ingreso);//por definir
                                    obKardexIngreso.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                    obKardexIngreso.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                                    obKardexIngreso.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                                    obKardexIngreso.kardc_numero_doc = oBe.remic_vnumero_remision;
                                    obKardexIngreso.kardc_tipo_movimiento = Parametros.intKardexIn;

                                    List<ETablaRegistro> lstTblaRegistro = new BGeneral().listarTablaRegistro(51).Where(t => t.tarec_iid_tabla_registro == oBe.tablc_iid_motivo).ToList();
                                    if (lstTblaRegistro.Count > 0)
                                    {
                                        if (lstTblaRegistro[0].tarec_iid_tabla_registro_salida > 0)
                                        {
                                            obKardexIngreso.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro[0].tarec_iid_tabla_registro_salida);
                                        }
                                    }

                                    //obKardexIngreso.kardc_iid_motivo = Parametros.intMotivoKrdTransferenciaIn;
                                    obKardexIngreso.kardc_beneficiario = "";
                                    obKardexIngreso.kardc_observaciones = "";
                                    obKardexIngreso.intUsuario = oBe.intUsuario;
                                    obKardexIngreso.strPc = oBe.strPc;
                                    objAlmacenData.modificarKardex(obKardexIngreso);
                                    #region Actualizando Stock
                                    EStock stck = new EStock();
                                    stck.stocc_ianio = Parametros.intEjercicio;
                                    stck.almac_icod_almacen = Convert.ToInt32(obKardexIngreso.almac_icod_almacen);
                                    stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                    stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                                    stck.intTipoMovimiento = 1;
                                    objAlmacenData.actualizarStock(stck);
                                    #endregion
                                    #endregion
                                }
                                #region Salida de Kardex
                                EKardex obKardex = new EKardex();
                                obKardex.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                                obKardex.kardc_ianio = Parametros.intEjercicio;
                                obKardex.kardc_fecha_movimiento = oBe.remic_sfecha_inicio;
                                obKardex.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                                obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                                obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                                obKardex.kardc_numero_doc = oBe.remic_vnumero_remision;
                                obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                                obKardex.kardc_iid_motivo = (int)CommonFunctions.GetMotivoByMotivoGRE(oBe.tablc_iid_motivo);
                                List<ETablaRegistro> lstTblaRegistro2 = new BGeneral().listarTablaRegistro(51).Where(t => t.tarec_iid_tabla_registro == oBe.tablc_iid_motivo).ToList();
                                if (lstTblaRegistro2.Count > 0)
                                {
                                    //if (lstTblaRegistro[0].tarec_iid_tabla_registro_ingreso > 0)
                                    //{
                                    //    obKardex.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro[0].tarec_iid_tabla_registro_ingreso);
                                    //}
                                    if (lstTblaRegistro2[0].tarec_iid_tabla_registro_salida > 0)
                                    {
                                        obKardex.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro2[0].tarec_iid_tabla_registro_salida);
                                    }
                                }

                                //obKardex.kardc_iid_motivo = oBe.tablc_iid_motivo;
                                obKardex.kardc_beneficiario = "";
                                obKardex.kardc_observaciones = "";
                                obKardex.intUsuario = oBe.intUsuario;
                                obKardex.strPc = oBe.strPc;
                                objAlmacenData.modificarKardex(obKardex);
                                /*----------------------------------------------------------------*/
                                #region Actualizando Stock
                                EStock stckS = new EStock();
                                stckS.stocc_ianio = Parametros.intEjercicio;
                                stckS.almac_icod_almacen = Convert.ToInt32(obKardex.almac_icod_almacen);
                                stckS.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                stckS.stocc_stock_producto = x.dremc_ncantidad_producto;
                                stckS.intTipoMovimiento = 0;
                                objAlmacenData.actualizarStock(stckS);
                                #endregion
                                #endregion
                            }
                            x.intUsuario = oBe.intUsuario;
                            x.strPc = oBe.strPc;
                            objVentasData.modificarGuiaRemisionMPDet(x);
                        }
                        else if (x.intTipoOperacion == 0 || x.intTipoOperacion == null)
                        {
                            if (x.dremc_itipo_producto == (int)TipoProducto.Mercadería)
                            {
                                if (oBe.tablc_iid_motivo == 226)
                                {
                                    #region Eliminar Kardex Ingreso
                                    //EKardex obKardexDelIngreso = new EKardex();
                                    //obKardexDelIngreso.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ingreso);
                                    //obKardexDelIngreso.intUsuario = oBe.intUsuario;
                                    //obKardexDelIngreso.strPc = oBe.strPc;
                                    //objAlmacenData.eliminarKardex(obKardexDelIngreso);
                                    /*---------------------------------------------------------------*/
                                    EKardex obKardexIngreso = new EKardex();
                                    obKardexIngreso.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ingreso);
                                    obKardexIngreso.kardc_ianio = Parametros.intEjercicio;
                                    obKardexIngreso.kardc_fecha_movimiento = oBe.remic_sfecha_inicio;
                                    obKardexIngreso.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen_ingreso);//por definir
                                    obKardexIngreso.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                    obKardexIngreso.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                                    obKardexIngreso.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                                    obKardexIngreso.kardc_numero_doc = oBe.remic_vnumero_remision;
                                    obKardexIngreso.kardc_tipo_movimiento = Parametros.intKardexIn;

                                    List<ETablaRegistro> lstTblaRegistro = new BGeneral().listarTablaRegistro(51).Where(t => t.tarec_iid_tabla_registro == oBe.tablc_iid_motivo).ToList();
                                    if (lstTblaRegistro.Count > 0)
                                    {
                                        if (lstTblaRegistro[0].tarec_iid_tabla_registro_ingreso > 0)
                                        {
                                            obKardexIngreso.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro[0].tarec_iid_tabla_registro_ingreso);
                                        }
                                        //if (lstTblaRegistro[0].tarec_iid_tabla_registro_salida > 0)
                                        //{
                                        //    obKardex.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro[0].tarec_iid_tabla_registro_salida);
                                        //}
                                    }

                                    //obKardexIngreso.kardc_iid_motivo = Parametros.intMotivoKrdTransferenciaIn;
                                    obKardexIngreso.kardc_beneficiario = "";
                                    obKardexIngreso.kardc_observaciones = "";
                                    obKardexIngreso.intUsuario = oBe.intUsuario;
                                    obKardexIngreso.strPc = oBe.strPc;
                                    objAlmacenData.modificarKardex(obKardexIngreso);
                                    #region Actualizando Stock
                                    EStock stck = new EStock();
                                    stck.stocc_ianio = Parametros.intEjercicio;
                                    stck.almac_icod_almacen = Convert.ToInt32(obKardexIngreso.almac_icod_almacen);
                                    stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                    stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                                    stck.intTipoMovimiento = 1;
                                    objAlmacenData.actualizarStock(stck);
                                    #endregion
                                    #endregion
                                }
                                #region Salida de Kardex
                                EKardex obKardex = new EKardex();
                                obKardex.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                                obKardex.kardc_ianio = Parametros.intEjercicio;
                                obKardex.kardc_fecha_movimiento = oBe.remic_sfecha_inicio;
                                obKardex.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                                obKardex.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                obKardex.kardc_icantidad_prod = Convert.ToDecimal(x.dremc_ncantidad_producto);
                                obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocGuiaRemision;
                                obKardex.kardc_numero_doc = oBe.remic_vnumero_remision;
                                obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                                obKardex.kardc_iid_motivo = (int)CommonFunctions.GetMotivoByMotivoGRE(oBe.tablc_iid_motivo);
                                List<ETablaRegistro> lstTblaRegistro2 = new BGeneral().listarTablaRegistro(51).Where(t => t.tarec_iid_tabla_registro == oBe.tablc_iid_motivo).ToList();
                                if (lstTblaRegistro2.Count > 0)
                                {
                                    //if (lstTblaRegistro[0].tarec_iid_tabla_registro_ingreso > 0)
                                    //{
                                    //    obKardex.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro[0].tarec_iid_tabla_registro_ingreso);
                                    //}
                                    if (lstTblaRegistro2[0].tarec_iid_tabla_registro_salida > 0)
                                    {
                                        obKardex.kardc_iid_motivo = Convert.ToInt32(lstTblaRegistro2[0].tarec_iid_tabla_registro_salida);
                                    }
                                }

                                //obKardex.kardc_iid_motivo = oBe.tablc_iid_motivo;
                                obKardex.kardc_beneficiario = "";
                                obKardex.kardc_observaciones = "";
                                obKardex.intUsuario = oBe.intUsuario;
                                obKardex.strPc = oBe.strPc;
                                objAlmacenData.modificarKardex(obKardex);
                                /*----------------------------------------------------------------*/
                                #region Actualizando Stock
                                EStock stckS = new EStock();
                                stckS.stocc_ianio = Parametros.intEjercicio;
                                stckS.almac_icod_almacen = Convert.ToInt32(obKardex.almac_icod_almacen);
                                stckS.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                                stckS.stocc_stock_producto = x.dremc_ncantidad_producto;
                                stckS.intTipoMovimiento = 0;
                                objAlmacenData.actualizarStock(stckS);
                                #endregion
                                #endregion
                            }
                            x.intUsuario = oBe.intUsuario;
                            x.strPc = oBe.strPc;
                            objVentasData.modificarGuiaRemisionMPDet(x);
                        }
                        x.electronicaDet.remic_icod_remision = oBe.remic_icod_remision;
                        objVentasData.insertarGuiaRemisionDetElectronica(x.generarGREdet());
                    });

                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarGuiaRemisionMP(EGuiaRemision oBe, List<EGuiaRemisionMPDet> lstMPDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarGuiaRemision(oBe);

                    lstMPDetalle.ForEach(x =>
                    {


                        #region Eliminar Kardex
                        EKardex obKardexDel = new EKardex();
                        obKardexDel.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo);
                        obKardexDel.intUsuario = oBe.intUsuario;
                        obKardexDel.strPc = oBe.strPc;
                        objAlmacenData.eliminarKardex(obKardexDel);
                        /*------------------------------------------------------*/
                        #region Actualizando Stock
                        EStock stckS = new EStock();
                        stckS.stocc_ianio = Parametros.intEjercicio;
                        stckS.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                        stckS.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                        stckS.stocc_stock_producto = x.dremc_ncantidad_producto;
                        stckS.intTipoMovimiento = 0;
                        objAlmacenData.actualizarStock(stckS);
                        #endregion
                        #endregion
                        if (oBe.tablc_iid_motivo == 226)
                        {
                            #region Eliminar Kardex Ingreso
                            EKardex obKardexDelIngreso = new EKardex();
                            obKardexDelIngreso.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ingreso);
                            obKardexDelIngreso.intUsuario = oBe.intUsuario;
                            obKardexDelIngreso.strPc = oBe.strPc;
                            objAlmacenData.eliminarKardex(obKardexDelIngreso);
                            /*--------------------------------------------------------------*/
                            #region Actualizando Stock
                            EStock stck = new EStock();
                            stck.stocc_ianio = Parametros.intEjercicio;
                            stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                            stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                            stck.stocc_stock_producto = x.dremc_ncantidad_producto;
                            stck.intTipoMovimiento = 1;
                            objAlmacenData.actualizarStock(stck);
                            #endregion
                            #endregion
                        }

                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objVentasData.eliminarGuiaRemisionMPDet(x);
                    });

                    //lstDelete.ForEach(x =>
                    //{
                    //    x.intUsuario = oBe.intUsuario;
                    //    x.strPc = oBe.strPc;
                    //    objVentasData.eliminarGuiaRemisionDet(x);
                    //});

                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EGuiaRemisionDet> listarGuiaRemisionDet(int intIcod, int intEjericio)
        {
            List<EGuiaRemisionDet> lista = null;
            try
            {
                lista = objVentasData.listarGuiaRemisionDet(intIcod, intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion
        public void ActualizarDescripcionDXCFAC(List<EFacturaCab> Mlist)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (var oBe in Mlist)
                    {
                        int i = 1;
                        string vdescripcionProd = "";
                        List<EFacturaDet> lstDetalle = new List<EFacturaDet>();
                        lstDetalle = new BVentas().listarFacturaDetalle(oBe.favc_icod_factura);
                        foreach (var _be in lstDetalle)
                        {
                            if (i == 1)
                            {
                                vdescripcionProd = _be.strDesProducto;
                                i++;
                            }
                            ////List<EDocCompraDet> lstProdPrecioDet = new List<EDocCompraDet>();
                            ////lstProdPrecioDet = new AlmacenData().listarUltimoPreciosDetalle(Convert.ToInt32(_be.prd_icod_producto), Parametros.intEjercicio);
                            ////if (lstProdPrecioDet.Count() > 0)
                            ////{
                            ////    new AlmacenData().modificarProductoPrecioCosto(Convert.ToInt32(_be.prd_icod_producto), Convert.ToDecimal(lstProdPrecioDet[0].facd_nmonto_unit));
                            ////}
                        }
                        new CuentasPorCobrarData().modificarDocumentoXCobrarDescripcion(oBe.doxcc_icod_correlativo, vdescripcionProd);
                    }

                    tx.Complete();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
        public void ActualizarCuentasDXCFAC(List<EFacturaCab> Mlist)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (var oBe in Mlist)
                    {
                        int i = 1;
                        string vdescripcionProd = "";
                        List<EFacturaDet> lstDetalle = new List<EFacturaDet>();
                        lstDetalle = new BVentas().listarFacturaDetalle(oBe.favc_icod_factura);
                        foreach (var _be in lstDetalle)
                        {
                            if (i == 1)
                            {
                                vdescripcionProd = _be.strDesProducto;
                                i++;
                            }

                        }
                        new CuentasPorCobrarData().modificarDocumentoXCobrarDescripcion(oBe.doxcc_icod_correlativo, vdescripcionProd);
                    }

                    tx.Complete();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
        public void ActualizarDescripcionDXCBov(List<EBoletaCab> Mlist)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (var oBe in Mlist)
                    {
                        int i = 1;
                        string vdescripcionProd = "";
                        List<EBoletaDet> lstDetalle = new List<EBoletaDet>();
                        lstDetalle = new BVentas().listarBoletaDetalle(oBe.bovc_icod_boleta);
                        foreach (var _be in lstDetalle)
                        {
                            if (i == 1)
                            {
                                vdescripcionProd = _be.strDesProducto;
                                i++;
                            }
                            ////List<EDocCompraDet> lstProdPrecioDet = new List<EDocCompraDet>();
                            ////lstProdPrecioDet = new AlmacenData().listarUltimoPreciosDetalle(Convert.ToInt32(_be.prd_icod_producto), Parametros.intEjercicio);
                            ////if (lstProdPrecioDet.Count() > 0)
                            ////{
                            ////    new AlmacenData().modificarProductoPrecioCosto(Convert.ToInt32(_be.prd_icod_producto), Convert.ToDecimal(lstProdPrecioDet[0].facd_nmonto_unit));
                            ////}
                        }
                        //new CuentasPorCobrarData().modificarDocumentoXCobrarDescripcion(oBe.ka, vdescripcionProd);
                    }

                    tx.Complete();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EOrdenCompraMercaderia> getOCLCabVerificarNumeroMercaderia(string vnumero, int intEjericio)
        {
            List<EOrdenCompraMercaderia> lista = new List<EOrdenCompraMercaderia>();
            try
            {
                lista = objVentasData.getOCLCabVerificarNumeroMercaderia(vnumero, intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EOrdenCompra> getOCLCabVerificarNumero(string vnumero, int intEjericio)
        {
            List<EOrdenCompra> lista = new List<EOrdenCompra>();
            try
            {
                lista = objVentasData.getOCLCabVerificarNumero(vnumero, intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EOrdenCompraServicio> getOCSCabVerificarNumero(string vnumero, int intEjericio)
        {
            List<EOrdenCompraServicio> lista = new List<EOrdenCompraServicio>();
            try
            {
                lista = objVentasData.getOCSCabVerificarNumero(vnumero, intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EOrdenCompraImportacion> getOCICabVerificarNumero(string vnumero, int intEjericio)
        {
            List<EOrdenCompraImportacion> lista = new List<EOrdenCompraImportacion>();
            try
            {
                lista = objVentasData.getOCICabVerificarNumero(vnumero, intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #region Lista de Precio Venta
        public List<EPedidoClienCab> listarPedidoVenta()
        {
            List<EPedidoClienCab> lista = null;
            try
            {
                lista = (objVentasData).listarPedidoVenta();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int insertarPedidoVenta(EPedidoClienCab oBe, List<EPedidoClienDet> lstDetalle)
        {
            int lpedi_icod_cliente = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    lpedi_icod_cliente = objVentasData.insertarPedidoVenta(oBe);

                    #region Detalle
                    lstDetalle.ForEach(x =>
                    {
                        x.lpedi_icod_cliente = lpedi_icod_cliente;
                        objVentasData.insertarPedidoVentaDet(x);
                    });
                    #endregion
                    tx.Complete();
                }
                return lpedi_icod_cliente;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarPedidoVenta(EPedidoClienCab oBe, List<EPedidoClienDet> lstDetalle,
            List<EPedidoClienDet> lstDelete)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarPedidoVenta(oBe);

                    #region Eliminar
                    lstDelete.ForEach(x =>
                    {
                        objVentasData.eliminarPedidoVentaDet(x);
                    });
                    #endregion
                    #region Detalle
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            x.lpedi_icod_cliente = oBe.lpedi_icod_cliente;
                            objVentasData.insertarPedidoVentaDet(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {
                            objVentasData.modificarPedidoVentaDet(x);
                        }
                    });
                    #endregion

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarPedidoVenta(EPedidoClienCab obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region Eliminación
                    var lst = listarPedidoVentaDet(obj.lpedi_icod_cliente, Parametros.intEjercicio);
                    lst.ForEach(x =>
                    {
                        objVentasData.eliminarPedidoVentaDet(x);
                    });
                    #endregion
                    objVentasData.eliminarPedidoVenta(obj);


                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Lista de Pedido Compra Det
        public List<EPedidoClienDet> listarPedidoVentaDet(int lpedi_icod_proveedor, int Anio)
        {
            List<EPedidoClienDet> lista = null;
            try
            {
                lista = (objVentasData).listarPedidoVentaDet(lpedi_icod_proveedor, Anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public void insertarPedidoVentaDet(EPedidoClienDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.insertarPedidoVentaDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarPedidoVentaDet(EPedidoClienDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarPedidoVentaDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarPedidoVentaDet(EPedidoClienDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarPedidoVentaDet(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Proyectos
        public List<EProyectos> listarProyectos()
        {
            List<EProyectos> lista = null;
            try
            {
                lista = objVentasData.listarProyectos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarProyectos(EProyectos oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarProyectos(oBe);
                    List<EAlmacen> lstAlmacen = new List<EAlmacen>();
                    lstAlmacen = new BAlmacen().listarAlmacenes().Where(x => x.almac_icod_almacen == oBe.almac_icod_almacen).ToList();
                    lstAlmacen.ForEach(x =>
                    {
                        x.pryc_icod_proyecto = intIcod;
                        x.cecoc_icod_centro_costo = oBe.pryc_icod_ccosto;
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        new BAlmacen().modificarAlmacen(x);
                    });
                    List<ECentroCosto> lstCC = new List<ECentroCosto>();
                    lstCC = new BContabilidad().listarCentroCosto().Where(x => x.cecoc_icod_centro_costo == oBe.pryc_icod_ccosto).ToList();
                    lstCC.ForEach(x =>
                    {
                        x.pryc_icod_proyecto = intIcod;
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        new BContabilidad().modificarCentroCosto(x);
                    });
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarProyectos(EProyectos oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarProyectos(oBe);
                    List<EAlmacen> lstAlmacen = new List<EAlmacen>();
                    lstAlmacen = new BAlmacen().listarAlmacenes().Where(x => x.almac_icod_almacen == oBe.almac_icod_almacen_2).ToList();
                    lstAlmacen.ForEach(x =>
                    {
                        if (oBe.almac_icod_almacen != oBe.almac_icod_almacen_2)
                        {
                            x.pryc_icod_proyecto = 0;
                            new BAlmacen().modificarAlmacen(x);
                        }
                    });
                    List<EAlmacen> lstAlmacen2 = new List<EAlmacen>();
                    lstAlmacen2 = new BAlmacen().listarAlmacenes().Where(x => x.almac_icod_almacen == oBe.almac_icod_almacen).ToList();
                    lstAlmacen2.ForEach(x =>
                    {
                        x.pryc_icod_proyecto = oBe.pryc_icod_proyecto;
                        new BAlmacen().modificarAlmacen(x);
                    });
                    List<ECentroCosto> lstCC = new List<ECentroCosto>();
                    lstCC = new BContabilidad().listarCentroCosto().Where(x => x.cecoc_icod_centro_costo == oBe.pryc_icod_ccosto).ToList();
                    lstCC.ForEach(x =>
                    {
                        x.pryc_icod_proyecto = oBe.pryc_icod_proyecto;
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        new BContabilidad().modificarCentroCosto(x);
                    });
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarProyectos(EProyectos oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarProyectos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Archivos
        public List<EArchivos> listarArchivos(int codigo)
        {
            List<EArchivos> lista = null;
            try
            {
                lista = objVentasData.listarArchivos(codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarArchivos(EArchivos oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarArchivos(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarArchivos(EArchivos oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarArchivos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarArchivos(EArchivos oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarArchivos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Hoja Costos
        public List<EHojaCostos> listarHojaCostos()
        {
            List<EHojaCostos> lista = new List<EHojaCostos>();
            try
            {
                lista = objVentasData.listarHojaCostos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarHojaCostos(EHojaCostos oBe, List<ESuministros> lstSuministros, List<EServicios> lstServicios, List<EGastosAdministrativos> lstGastosAdministrativos)
        {
            int intIcod = 0;
            int intIcodC = 0;
            int intIcodR = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    #region Insertar
                    intIcod = objVentasData.insertarHojaCostos(oBe);

                    lstSuministros.ForEach(x =>
                    {
                        x.hjcd1_icod_detalle_hc = intIcod;
                        insertarHojaCostosSuministros(x);
                    });
                    lstServicios.ForEach(xs =>
                    {
                        xs.hjcd1_icod_detalle_hc = intIcod;
                        insertarHojaCostosServicios(xs);
                    });
                    lstGastosAdministrativos.ForEach(xxs =>
                    {
                        xxs.hjcd1_icod_detalle_hc = intIcod;
                        insertarHojaCostosGastosAdministrativos(xxs);
                    });
                    #endregion
                    tx.Complete();

                }
                return intIcod;
                return intIcodC;
                return intIcodR;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarHojaCostos(EHojaCostos oBe, List<ESuministros> lstSuministros, List<ESuministros> lstDeleteSuministros, List<EServicios> lstServicios,
            List<EServicios> lstDeleteServicios, List<EGastosAdministrativos> lstGastosAdministrativos, List<EGastosAdministrativos> lstDeleteGastosAdministrativos)
        {

            int intIcod = 0;
            int intIcodC = 0;
            int intIcodR = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarHojaCostos(oBe);
                    #region Eliminar Detalle
                    /*Elimiar Detalle*/
                    lstDeleteSuministros.ForEach(x =>
                    {
                        eliminarHojaCostosSuministros(x);
                    });
                    lstDeleteServicios.ForEach(x =>
                    {
                        eliminarHojaCostosServicios(x);
                    });
                    lstDeleteGastosAdministrativos.ForEach(x =>
                    {
                        eliminarHojaCostosGastosAdministrativos(x);
                    });
                    #endregion
                    #region Insertar Detalle
                    /*Insertar Detalle*/
                    lstSuministros.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {

                            x.hjcd1_icod_detalle_hc = oBe.hjcc_icod_hoja_costo;
                            insertarHojaCostosSuministros(x);

                        }
                        else
                        {
                            modificarHojaCostosSuministros(x);
                        }
                    });
                    lstServicios.ForEach(x =>
                    {

                        if (x.intTipoOperacion == 1)
                        {
                            insertarHojaCostosServicios(x);
                        }
                        else
                        {
                            modificarHojaCostosServicios(x);
                        }
                    });
                    lstGastosAdministrativos.ForEach(x =>
                    {

                        if (x.intTipoOperacion == 1)
                        {
                            insertarHojaCostosGastosAdministrativos(x);
                        }
                        else
                        {
                            modificarHojaCostosGastosAdministrativos(x);
                        }
                    });
                    #endregion
                    tx.Complete();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarHojaCostos(EHojaCostos oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    var listaHCC = listarHojaCostosConceptos(oBe.hjcc_icod_hoja_costo);
                    listaHCC.ForEach(x =>
                    {
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        eliminarHojaCostosConceptos(x);
                    });
                    var listaHCS = listarHojaCostosSubConceptos(oBe.hjcc_icod_hoja_costo);
                    listaHCS.ForEach(x =>
                    {
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        eliminarHojaCostosSubConceptos(x);
                    });
                    var listaHCR = listarHojaCostosRubros(oBe.hjcc_icod_hoja_costo);
                    listaHCR.ForEach(x =>
                    {
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        eliminarHojaCostosRubros(x);
                    });
                    objVentasData.eliminarHojaCostos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EHojaCostos> getHCCabVerificarNumero(string vnumero, int intEjericio)
        {
            List<EHojaCostos> lista = new List<EHojaCostos>();
            try
            {
                lista = objVentasData.getHCCabVerificarNumero(vnumero, intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion
        #region Hoja Costos Conceptos
        public List<EHojaCostosConceptos> listarHojaCostosConceptos(int Concepto)
        {
            List<EHojaCostosConceptos> lista = new List<EHojaCostosConceptos>();
            try
            {
                lista = objVentasData.listarHojaCostosConceptos(Concepto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarHojaCostosConceptos(EHojaCostosConceptos oBe)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarHojaCostosConceptos(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarHojaCostosConceptos(EHojaCostosConceptos oBe)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarHojaCostosConceptos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarHojaCostosConceptos(EHojaCostosConceptos oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarHojaCostosConceptos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Hoja Costos Sub-Conceptos
        public List<EhojaCostosSubConceptos> listarHojaCostosSubConceptos(int SubConcepto)
        {
            List<EhojaCostosSubConceptos> lista = new List<EhojaCostosSubConceptos>();
            try
            {
                lista = objVentasData.listarHojaCostosSubConceptos(SubConcepto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarHojaCostosSubConceptos(EhojaCostosSubConceptos oBe)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarHojaCostosSubConceptos(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarHojaCostosSubConceptos(EhojaCostosSubConceptos oBe)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarHojaCostosSubConceptos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarHojaCostosSubConceptos(EhojaCostosSubConceptos oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarHojaCostosSubConceptos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Hoja Costos Rubros
        public List<EHojaCostosRubros> listarHojaCostosRubros(int Rubros)
        {
            List<EHojaCostosRubros> lista = new List<EHojaCostosRubros>();
            try
            {
                lista = objVentasData.listarHojaCostosRubros(Rubros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EHojaCostosRubros> listarHojaCostosRubrosRQM(int Rubros)
        {
            List<EHojaCostosRubros> lista = new List<EHojaCostosRubros>();
            try
            {
                lista = objVentasData.listarHojaCostosRubrosRQM(Rubros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarHojaCostosRubros(EHojaCostosRubros oBe)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarHojaCostosRubros(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarHojaCostosRubros(EHojaCostosRubros oBe)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarHojaCostosRubros(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarHojaCostosRubros(EHojaCostosRubros oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarHojaCostosRubros(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Requerimiento Materiales
        public List<ERequerimientoMateriales> listarRequerimientoMateriales()
        {
            List<ERequerimientoMateriales> lista = new List<ERequerimientoMateriales>();
            try
            {
                lista = objVentasData.listarRequerimientoMateriales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ERequerimientoMateriales> listarAutorizacionRequerimientoMateriales()
        {
            List<ERequerimientoMateriales> lista = new List<ERequerimientoMateriales>();
            try
            {
                lista = objVentasData.listarAutorizacionRequerimientoMateriales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ERequerimientoMateriales> listarVerificacionStockRequerimientoMateriales()
        {
            List<ERequerimientoMateriales> lista = new List<ERequerimientoMateriales>();
            try
            {
                lista = objVentasData.listarVerificacionStockRequerimientoMateriales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarRequerimientoMateriales(ERequerimientoMateriales oBe, List<ERequerimientoMaterialesDetalle> lstRequerimientoMaterialesDetalle)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarRequerimientoMateriales(oBe);

                    lstRequerimientoMaterialesDetalle.ForEach(x =>
                    {
                        decimal Cantidad_Saldo = objVentasData.listarCantidadRequeidaRQM(x.hjcd3_icod_rubros_hc, x.prdc_icod_producto);
                        if (Cantidad_Saldo < x.rqmd_cantidad_pedida)
                        {
                            throw new Exception("La Cantidad Requerida, Sobrepasa al Saldo de la Hoja de Costo");
                        }
                        x.rqmc_icod_requerimiento_materiales = intIcod;
                        insertarRequerimientoMaterialesDetalle(x);
                        ActualizarCantidadRequerida(x.hjcd3_icod_rubros_hc, x.prdc_icod_producto);
                    });
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarRequerimientoMateriales(ERequerimientoMateriales oBe, List<ERequerimientoMaterialesDetalle> lstRequerimientoMaterialesDetalle, List<ERequerimientoMaterialesDetalle> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objVentasData.modificarRequerimientoMateriales(oBe);
                    lstRequerimientoMaterialesDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 2)
                        {
                            modificarRequerimientoMaterialesDetalle(x);
                            ActualizarCantidadRequerida(x.hjcd3_icod_rubros_hc, x.prdc_icod_producto);
                        }
                        else
                        {
                            x.rqmd_cantidad_aprobada = x.rqmd_cantidad_pedida;
                            modificarRequerimientoMaterialesDetalle(x);
                            ActualizarCantidadRequerida(x.hjcd3_icod_rubros_hc, x.prdc_icod_producto);
                        }

                    });
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarRequerimientoMateriales(ERequerimientoMateriales oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    List<ERequerimientoMaterialesDetalle> lsrRQMDetalle = new List<ERequerimientoMaterialesDetalle>();
                    lsrRQMDetalle = listarAutorizacionRequerimientoMaterialesDetalle(oBe.rqmc_icod_requerimiento_materiales);
                    lsrRQMDetalle.ForEach(x =>
                    {
                        new BVentas().eliminarRequerimientoMaterialesDetalle(x);
                        ActualizarCantidadRequerida(x.hjcd3_icod_rubros_hc, x.prdc_icod_producto);
                    });
                    objVentasData.eliminarRequerimientoMateriales(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AnularRequerimientoMateriales(ERequerimientoMateriales obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.AnularRequerimientoMateriales(obj.rqmc_icod_requerimiento_materiales, 312);//Parametros.intSituacOCAnulado);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void AutorizacionRequerimientoMaterialesEliminar(ERequerimientoMateriales oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objVentasData.AutorizacionRequerimientoMaterialesEliminar(oBe);
                    List<ERequerimientoMaterialesDetalle> lstDetalleRQ = new List<ERequerimientoMaterialesDetalle>();
                    lstDetalleRQ = new BVentas().listarRequerimientoMaterialesDetalle(oBe.rqmc_icod_requerimiento_materiales);
                    lstDetalleRQ.ForEach(x =>
                    {
                        AutorizacionRequerimientoMaterialesDetalleEliminar(x);
                    });
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Requerimiento Materiales Detalle
        public List<ERequerimientoMaterialesDetalle> listarRequerimientoMaterialesDetalle(int Concepto)
        {
            List<ERequerimientoMaterialesDetalle> lista = new List<ERequerimientoMaterialesDetalle>();
            try
            {
                lista = objVentasData.listarRequerimientoMaterialesDetalle(Concepto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ERequerimientoMaterialesDetalle> listarAutorizacionRequerimientoMaterialesDetalle(int Concepto)
        {
            List<ERequerimientoMaterialesDetalle> lista = new List<ERequerimientoMaterialesDetalle>();
            try
            {
                lista = objVentasData.listarAutorizacionRequerimientoMaterialesDetalle(Concepto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarRequerimientoMaterialesDetalle(ERequerimientoMaterialesDetalle oBe)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarRequerimientoMaterialesDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarRequerimientoMaterialesDetalle(ERequerimientoMaterialesDetalle oBe)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarRequerimientoMaterialesDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarRequerimientoMaterialesDetalle(ERequerimientoMaterialesDetalle oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarRequerimientoMaterialesDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AutorizacionRequerimientoMaterialesDetalleEliminar(ERequerimientoMaterialesDetalle oBe)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.AutorizacionRequerimientoMaterialesDetalleEliminar(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Concepto Costos
        public List<EConceptosCostos> listarConceptosCostos()
        {
            List<EConceptosCostos> lista = new List<EConceptosCostos>();
            try
            {
                lista = objVentasData.listarConceptosCostos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarConceptosCostos(EConceptosCostos oBe)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarConceptosCostos(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarConceptosCostos(EConceptosCostos oBe)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarConceptosCostos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarConceptosCostos(EConceptosCostos oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarConceptosCostos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Concepto Costos Detalle
        public List<EConceptosCostosDetalle> listarConceptosCostosDetalle(int SubConcepto)
        {
            List<EConceptosCostosDetalle> lista = new List<EConceptosCostosDetalle>();
            try
            {
                lista = objVentasData.listarConceptosCostosDetalle(SubConcepto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EConceptosCostosDetalle> listarConceptosCostosDetalle2()
        {
            List<EConceptosCostosDetalle> lista = new List<EConceptosCostosDetalle>();
            try
            {
                lista = objVentasData.listarConceptosCostosDetalle2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarConceptosCostosDetalle(EConceptosCostosDetalle oBe)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarConceptosCostosDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarConceptosCostosDetalle(EConceptosCostosDetalle oBe)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarConceptosCostosDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarConceptosCostosDetalle(EConceptosCostosDetalle oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarConceptosCostosDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Suma Cantidad Requerida
        public void ActualizarCantidadRequerida(int hjcd3_icod_rubros_hc, int prdc_icod_producto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarCantidadRequerida(hjcd3_icod_rubros_hc, prdc_icod_producto);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Garantia Clientes
        public List<EGarantiaClientes> listarGarantiaClientes()
        {
            List<EGarantiaClientes> lista = null;
            try
            {
                lista = objVentasData.listarGarantiaClientes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarGarantiaClientes(EGarantiaClientes oBe)
        {
            CuentasPorCobrarData objCuentasPorPagarDataGC = new CuentasPorCobrarData();
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarGarantiaClientes(oBe);
                    oBe.garc_icod_garantia = intIcod;
                    #region Generación de Doc Por Pagar
                    EDocXCobrar objDXP = crearDocPorPagarGarantiaClientes(oBe);
                    oBe.doxpc_icod_correlativo = objCuentasPorPagarDataGC.insertarDocumentoXCobrar(objDXP);
                    #endregion
                    #region Pago de DXC
                    EDocXCobrarPago obj_DXC_pago = new EDocXCobrarPago();
                    obj_DXC_pago.doxcc_icod_correlativo = oBe.favc_icod_factura;
                    obj_DXC_pago.tdocc_icod_tipo_doc = 112;
                    obj_DXC_pago.pdxcc_vnumero_doc = oBe.garc_vnumero_garantia;
                    obj_DXC_pago.pdxcc_sfecha_cobro = oBe.garc_sfecha_garantia;
                    obj_DXC_pago.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    obj_DXC_pago.pdxcc_nmonto_cobro = oBe.garc_nmonto;
                    obj_DXC_pago.pdxcc_nmonto_tipo_cambio = objDXP.doxcc_nmonto_tipo_cambio;
                    obj_DXC_pago.pdxcc_vobservacion = String.Format("GC N° : {0}", oBe.garc_vnumero_garantia);
                    //obj_DXP_pago.efctc_icod_enti_financiera_cuenta = x.
                    obj_DXC_pago.pdxcc_vorigen = "L";
                    //obj_DXC_pago.doxcc_icod_correlativo = null;
                    //obj_DXP_pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                    //obj_DXP_pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                    //obj_DXP_pago.anac_icod_analitica = item.icod_analitica;
                    obj_DXC_pago.intUsuario = oBe.intUsuario;
                    obj_DXC_pago.strPc = oBe.strPc;
                    //obj_DXC_pago.pdxpc_mes = oBe.garc_sfecha_garantia.Month;
                    //obj_DXC_pago.anio = oBe.garc_sfecha_garantia.Year;
                    obj_DXC_pago.pdxcc_flag_estado = true;

                    oBe.pdxpc_icod_correlativo = new CuentasPorCobrarData().InsertarPagoDirectoDocumentoXCobrar(obj_DXC_pago);
                    new TesoreriaData().ActualizarMontoDXCPagadoSaldo(oBe.favc_icod_factura, obj_DXC_pago.tablc_iid_tipo_moneda);
                    #endregion
                    objVentasData.modificarGarantiaClientes(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGarantiaClientes(EGarantiaClientes oBe)
        {
            CuentasPorCobrarData objCuentasPorPagarDataGC = new CuentasPorCobrarData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    oBe.doxpc_icod_correlativo = oBe.intDXP;
                    objVentasData.modificarGarantiaClientes(oBe);
                    #region Modificación del Doc Por Pagar
                    EDocXCobrar objDXP = crearDocPorPagarGarantiaClientes(oBe);
                    objCuentasPorPagarDataGC.modificarDocumentoXCobrar(objDXP);
                    #endregion
                    #region Pago de DXC
                    EDocXCobrarPago obj_DXC_pago = new EDocXCobrarPago();
                    obj_DXC_pago.doxcc_icod_correlativo = oBe.favc_icod_factura;
                    obj_DXC_pago.tdocc_icod_tipo_doc = 112;
                    obj_DXC_pago.pdxcc_vnumero_doc = oBe.garc_vnumero_garantia;
                    obj_DXC_pago.pdxcc_sfecha_cobro = oBe.garc_sfecha_garantia;
                    obj_DXC_pago.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    obj_DXC_pago.pdxcc_nmonto_cobro = oBe.garc_nmonto;
                    obj_DXC_pago.pdxcc_nmonto_tipo_cambio = objDXP.doxcc_nmonto_tipo_cambio;
                    obj_DXC_pago.pdxcc_vobservacion = String.Format("GC N° : {0}", oBe.garc_vnumero_garantia);
                    //obj_DXP_pago.efctc_icod_enti_financiera_cuenta = x.
                    obj_DXC_pago.pdxcc_vorigen = "L";
                    //obj_DXC_pago.doxcc_icod_correlativo = null;
                    //obj_DXP_pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                    //obj_DXP_pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                    //obj_DXP_pago.anac_icod_analitica = item.icod_analitica;
                    obj_DXC_pago.intUsuario = oBe.intUsuario;
                    obj_DXC_pago.strPc = oBe.strPc;
                    //obj_DXC_pago.pdxpc_mes = oBe.garc_sfecha_garantia.Month;
                    //obj_DXC_pago.anio = oBe.garc_sfecha_garantia.Year;
                    obj_DXC_pago.pdxcc_flag_estado = true;

                    new CuentasPorCobrarData().ActualizarPagoDirectoDocumentoXCobrar(obj_DXC_pago);
                    new TesoreriaData().ActualizarMontoDXCPagadoSaldo(oBe.favc_icod_factura, obj_DXC_pago.tablc_iid_tipo_moneda);
                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarGarantiaClientes(EGarantiaClientes oBe)
        {
            CuentasPorCobrarData objCuentasPorPagarDataGC = new CuentasPorCobrarData();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarGarantiaClientes(oBe);

                    //#region Eliminación de Doc Por Pagar
                    //EDocXCobrar objDXP = crearDocPorPagarGarantiaClientes(oBe);
                    //objCuentasPorPagarDataGC.EliminarDocumentoXCobrar(objDXP);
                    //#endregion
                    EDocXCobrar objDXP = new EDocXCobrar();
                    objDXP.doxcc_icod_correlativo = oBe.intDXP;
                    objDXP.intUsuario = oBe.intUsuario;
                    objDXP.strPc = oBe.strPc;
                    new CuentasPorCobrarData().EliminarDocumentoXCobrar(objDXP);
                    #region Eliminar Pago DXP
                    EDocXCobrarPago oBeDXCPago = new EDocXCobrarPago();
                    oBeDXCPago.pdxcc_icod_correlativo = Convert.ToInt64(oBe.pdxpc_icod_correlativo);
                    oBeDXCPago.intUsuario = oBe.intUsuario;
                    oBeDXCPago.strPc = oBe.strPc;
                    new CuentasPorCobrarData().EliminarPagoDirectoDocumentoXCobrar(oBeDXCPago);
                    new TesoreriaData().ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(oBe.favc_icod_factura), oBe.tablc_iid_tipo_moneda);
                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EDocXCobrar crearDocPorPagarGarantiaClientes(EGarantiaClientes obj)
        {
            EDocXCobrar objDXC = new EDocXCobrar();
            try
            {
                #region
                //objDXP.doxpc_icod_correlativo = obj.intDXP;
                //objDXP.anio = obj.garp_sfecha_garantia.Year;
                //objDXP.mesec_iid_mes = obj.garp_sfecha_garantia.Month;
                //objDXP.tdocc_icod_tipo_doc = 113;//TIPO DOCUMENTO 113 DE GARANTIA PROVEEDORES
                //objDXP.tdodc_iid_correlativo = 84; //CLASE DE DOCUMENTO DE FACTUA DE COMPRA
                //objDXP.doxpc_iid_correlativo = 0;
                //objDXP.doxpc_vnumero_doc = obj.garap_vnumero_garantia;
                //objDXP.doxpc_sfecha_doc = obj.garp_sfecha_garantia;
                //objDXP.doxpc_sfecha_vencimiento_doc = obj.garp_sfecha_garantia;
                //objDXP.proc_icod_proveedor = obj.proc_icod_proveedor;

                //objDXP.tablc_iid_tipo_moneda = obj.tablc_iid_tipo_moneda;

                //objDXP.doxpc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(obj.garp_sfecha_garantia);
                //if (objDXP.doxpc_nmonto_tipo_cambio == 0)
                //    throw new ArgumentException("No se econtró tipo de cambio para la fecha del documento, favor de registrar tipo de cambio");
                //objDXP.doxpc_vdescrip_transaccion = "GARANTIA POR SERVICIO";
                //objDXP.doxpc_nmonto_destino_gravado = 0;
                //objDXP.doxpc_nmonto_destino_mixto = 0;
                //objDXP.doxpc_nmonto_destino_nogravado = 0;
                //objDXP.doxpc_nmonto_nogravado = obj.garp_nmonto;
                //objDXP.doxpc_nmonto_imp_destino_gravado = 0;
                //objDXP.doxpc_nmonto_imp_destino_mixto = 0;
                //objDXP.doxpc_nmonto_imp_destino_nogravado = 0;
                //objDXP.doxpc_nmonto_total_documento = Convert.ToDecimal(obj.garp_nmonto);
                //objDXP.doxpc_nmonto_total_pagado = 0;
                //objDXP.doxpc_nmonto_total_saldo = Convert.ToDecimal(obj.garp_nmonto);
                //objDXP.doxpc_nporcentaje_igv = 0;
                //objDXP.tablc_iid_situacion_documento = 8;
                //objDXP.doxpc_tipo_comprobante_referencia = 0;
                //objDXP.doxpc_num_serie_referencia = "";
                //objDXP.doxpc_num_comprobante_referencia = "";
                //objDXP.doxpc_sfecha_emision_referencia = null;
                //objDXP.doxpc_nporcentaje_isc = 0;
                //objDXP.doxpc_nmonto_isc = 0;
                //objDXP.doxpc_nmonto_referencial_cif = 0;
                //objDXP.doxpc_nmonto_retenido = 0;
                //objDXP.doxpc_nmonto_retencion_rh = 0;
                //objDXP.doxpc_nmonto_servicio_no_domic = 0;
                //objDXP.doxpc_nporcentaje_imp_renta = 0;
                //objDXP.doxpc_vnro_deposito_detraccion = null;
                //objDXP.doxpc_sfec_deposito_detraccion = null;
                //objDXP.intUsuario = obj.intUsuario;
                //objDXP.strPc = obj.strPc;
                //objDXP.doxpc_origen = "9";
                //objDXP.doxpc_flag_estado = true;
                //objDXP.doxpc_numdoc_tipo = 2;
                #endregion
                objDXC.mesec_iid_mes = Convert.ToInt16(obj.garc_sfecha_garantia.Month);
                objDXC.tdocc_icod_tipo_doc = 112;
                objDXC.tdodc_iid_correlativo = 83;
                objDXC.doxcc_vnumero_doc = obj.garc_vnumero_garantia;
                objDXC.cliec_icod_cliente = obj.cliec_icod_cliente;
                objDXC.cliec_vnombre_cliente = obj.NomClie;
                objDXC.doxcc_sfecha_doc = obj.garc_sfecha_garantia;
                objDXC.doxcc_sfecha_vencimiento_doc = obj.garc_sfecha_garantia;
                objDXC.tablc_iid_tipo_moneda = obj.tablc_iid_tipo_moneda;
                objDXC.doxcc_nmonto_tipo_cambio = new ContabilidadData().getTipoCambioPorFecha(obj.garc_sfecha_garantia);
                if (objDXC.doxcc_nmonto_tipo_cambio == 0)
                    throw new ArgumentException("No se econtró tipo de cambio para la fecha del documento, favor de registrar tipo de cambio");
                objDXC.tablc_iid_tipo_pago = 174;
                objDXC.doxcc_vdescrip_transaccion = "GARANTIA POR SERVICIO";
                objDXC.doxcc_nmonto_afecto = 0;
                objDXC.doxcc_nmonto_inafecto = obj.garc_nmonto;
                objDXC.doxcc_nporcentaje_igv = 0;
                objDXC.doxcc_nmonto_impuesto = 0;
                objDXC.doxcc_nmonto_total = obj.garc_nmonto;
                objDXC.doxcc_nmonto_saldo = obj.garc_nmonto;
                objDXC.doxcc_nmonto_pagado = 0;
                objDXC.tablc_iid_situacion_documento = 8;
                objDXC.doxcc_vobservaciones = "";
                objDXC.doxc_bind_cuenta_corriente = false;
                objDXC.doxcc_sfecha_entrega = null;
                objDXC.doxcc_bind_impresion_nogerencia = false;
                objDXC.doxc_bind_situacion_legal = false;
                objDXC.doxc_bind_cierre_cuenta_corriente = false;
                objDXC.intUsuario = obj.intUsuario;
                objDXC.strPc = obj.strPc;
                objDXC.doxcc_tipo_comprobante_referencia = 0;
                objDXC.doxcc_num_serie_referencia = "";
                objDXC.doxcc_num_comprobante_referencia = "";
                objDXC.doxcc_sfecha_emision_referencia = null;
                objDXC.docxc_icod_documento = obj.garc_icod_garantia;
                objDXC.anio = obj.garc_sfecha_garantia.Year;
                objDXC.doxcc_flag_estado = true;
                objDXC.doxcc_origen = "9";
                return objDXC;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region VentasDaot

        public List<EVentasDaot> ListarVentasDaot(decimal monto, int anio)
        {
            List<EVentasDaot> lista = null;
            try
            {
                lista = (objVentasData).ListarVentasDaot(monto, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EVentasDaotDetalle> ListarVentasDaotDetallexCliente(long proc_icod_proveedor, int anio)
        {
            List<EVentasDaotDetalle> lista = null;
            try
            {
                lista = (objVentasData).ListarVentasDaotDetallexCliente(proc_icod_proveedor, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EVentasDaotDetalle> ListarVentasDaotDetalle(decimal monto, int anio)
        {
            List<EVentasDaotDetalle> lista = null;
            try
            {
                lista = (objVentasData).ListarVentasDaotDetalle(monto, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        #endregion

        #region Hoja Costo Suministros
        public List<ESuministros> listarHojaCostosSuministros(int Concepto)
        {
            List<ESuministros> lista = new List<ESuministros>();
            try
            {
                lista = objVentasData.listarHojaCostosSuministros(Concepto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarHojaCostosSuministros(ESuministros oBe)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarHojaCostosSuministros(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarHojaCostosSuministros(ESuministros oBe)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarHojaCostosSuministros(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarHojaCostosSuministros(ESuministros oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarHojaCostosSuministros(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Hoja Costo Servicios
        public List<EServicios> listarHojaCostosServicios(int Concepto)
        {
            List<EServicios> lista = new List<EServicios>();
            try
            {
                lista = objVentasData.listarHojaCostosServicios(Concepto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarHojaCostosServicios(EServicios oBe)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarHojaCostosServicios(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarHojaCostosServicios(EServicios oBe)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarHojaCostosServicios(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarHojaCostosServicios(EServicios oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarHojaCostosServicios(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Hoja Costo Gastos Administrativos
        public List<EGastosAdministrativos> listarHojaCostosGastosAdministrativos(int Concepto)
        {
            List<EGastosAdministrativos> lista = new List<EGastosAdministrativos>();
            try
            {
                lista = objVentasData.listarHojaCostosGastosAdministrativos(Concepto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarHojaCostosGastosAdministrativos(EGastosAdministrativos oBe)
        {
            try
            {
                int intIcod = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarHojaCostosGastosAdministrativos(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarHojaCostosGastosAdministrativos(EGastosAdministrativos oBe)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarHojaCostosGastosAdministrativos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarHojaCostosGastosAdministrativos(EGastosAdministrativos oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarHojaCostosGastosAdministrativos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public List<EPuntoVenta> getCorrelativoPVT(int intPVT)
        {
            try
            {
                var lst = objVentasData.getCorrelativoPVT(intPVT);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EParametroProduccion> getCorrelativoOP(int intPD)
        {
            try
            {
                var lst = objVentasData.getCorrelativoOP(intPD);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Tablas Sunat Cab
        public List<ETablaSunatCab> TablasSunatListar()
        {
            List<ETablaSunatCab> lista = null;
            try
            {
                lista = (objVentasData).TablasSunatListar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void TablasSunatInsertar(ETablaSunatCab obj)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objVentasData.TablasSunatInsertar(obj);
                    tx.Complete();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void TablasSunatModificar(ETablaSunatCab obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objVentasData.TablasSunatModificar(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void TablasSunatEliminar(ETablaSunatCab obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objVentasData.TablasSunatEliminar(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Tablas Sunat Det
        public List<ETablaSunatDet> TablasSunatDetListar(int icod)
        {
            List<ETablaSunatDet> lista = null;
            try
            {
                lista = (objVentasData).TablasSunatDetListar(icod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void TablasSunatDetInsertar(ETablaSunatDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objVentasData.TablasSunatDetInsertar(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void TablasSunatDetModificar(ETablaSunatDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objVentasData.TablasSunatDetModificar(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void TablasSunatDetEliminar(ETablaSunatDet obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objVentasData.TablasSunatDetEliminar(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        public List<EDireccionCliente> listarDireccionCliente(int codigo)
        {
            List<EDireccionCliente> lista = null;
            try
            {
                lista = objVentasData.listarDireccionCliente(codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarDireccionCliente(EDireccionCliente oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarDireccionCliente(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarDireccionCliente(EDireccionCliente oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarDireccionCliente(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarDireccionCliente(EDireccionCliente oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarDireccionCliente(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Series
        public List<ESeries> listarSeries()
        {
            List<ESeries> lista = null;
            try
            {
                lista = objVentasData.listarSeries();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarSeries(ESeries oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarSeries(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarSeries(ESeries oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarSeries(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarSeries(ESeries oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarSeries(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        public void anularNotaDebitoClienteCab(ENotaDebito oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.anularNotaDebitoClienteCab(oBe);
                    #region Dxc...
                    EDocXCobrar objDXC = new EDocXCobrar();
                    objDXC.doxcc_icod_correlativo = oBe.ncrec_icod_dxc;
                    objDXC.intUsuario = oBe.intUsuario;
                    objDXC.strPc = oBe.strPc;
                    new CuentasPorCobrarData().AnularDocumentoXCobrar(objDXC);
                    #endregion
                    //se elimina los items
                    var lstDelete = objVentasData.listarNotaDebitoClienteDet(oBe.ncrec_icod_credito);
                    lstDelete.ForEach(x =>
                    {
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objVentasData.eliminarNotaDebitoClienteDet(x);
                    });

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Guia Remision MP Detalle
        public List<EGuiaRemisionMPDet> listarGuiaRemisionMPDet(int intIcod, int intEjericio)
        {
            List<EGuiaRemisionMPDet> lista = null;
            try
            {
                lista = objVentasData.listarGuiaRemisionMPDet(intIcod, intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarGuiaRemisionMPDet(EGuiaRemisionMPDet oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objVentasData.insertarGuiaRemisionMPDet(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGuiaRemisionMPDet(EGuiaRemisionMPDet oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.modificarGuiaRemisionMPDet(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarGuiaRemisionMPDet(EGuiaRemisionMPDet oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.eliminarGuiaRemisionMPDet(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        public void ActualizarMotivoBajaCabeceraFactura(EFacturaCab obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarMotivoBajaCabeceraFactura(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarMotivoBajaCabeceraBoleta(EBoletaCab obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarMotivoBajaCabeceraBoleta(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarMotivoBajaCabeceraNC(ENotaCredito obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarMotivoBajaCabeceraNC(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarMotivoBajaCabeceraND(ENotaDebito obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objVentasData.ActualizarMotivoBajaCabeceraND(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
