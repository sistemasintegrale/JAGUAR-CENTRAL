using SGE.Entity;
using SGE.Entity.FacturaElectronica;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SGE.DataAccess
{
    public class VentasData
    {
        #region Cliente
        public List<ECliente> ListarCliente()
        {
            List<ECliente> lista = new List<ECliente>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGES_CLIENTE_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ECliente()
                        {
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            giroc_icod_giro = Convert.ToInt32(item.giroc_icod_giro),
                            giroc_vnombre_giro = item.giroc_vnombre_giro,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            cliec_vnombre_comercial = item.cliec_vnombre_comercial,
                            cliec_vdireccion_cliente = item.cliec_vdireccion_cliente,
                            ubicc_icod_ubicacion = item.ubicc_icod_ubicacion,
                            ubicc_vnombre_ubicacion = item.ubicc_vnombre_ubicacion,
                            cliec_vnro_telefono = item.cliec_vnro_telefono,
                            cliec_vnro_fax = item.cliec_vnro_fax,
                            cliec_vnro_celular = item.cliec_vnro_celular,
                            tabl_iid_tipo_documento = item.tabl_iid_tipo_documento,
                            TipoDoc = item.TipoDoc,
                            cliec_vnumero_doc_cli = (item.cliec_vnumero_doc_cli == null) ? "" : item.cliec_vnumero_doc_cli,
                            cliec_vnombre_contacto = (item.cliec_vnombre_contacto == null) ? "" : item.cliec_vnombre_contacto,
                            tablc_iid_tipo_relacion_cli = item.tablc_iid_tipo_relacion_cli,
                            EmpresaRelacion = item.EmpresaRelacion,
                            vendc_icod_vendedor = item.vendc_icod_vendedor,
                            vendc_vnombre_vendedor = (item.vendc_vnombre_vendedor == null) ? "" : item.vendc_vnombre_vendedor,
                            vendc_iid_vendedor = item.vendc_iid_vendedor,
                            cliec_sfecha_registro_cliente = item.cliec_sfecha_registro_cliente,
                            cliec_iid_situacion_cliente = Convert.ToInt32(item.cliec_iid_situacion_cliente),
                            Situacion = item.Situacion,
                            cliec_vcorreo_electronico = item.cliec_vcorreo_electronico,
                            cliec_vapellido_paterno = item.cliec_vapellido_paterno,
                            cliec_vapellido_materno = item.cliec_vapellido_materno,
                            cliec_vnombres = item.cliec_vnombres,
                            tablc_iid_tipo_cliente = item.tablc_iid_tipo_cliente,
                            cliec_cruc = string.IsNullOrEmpty(item.cliec_cruc) ? "" : item.cliec_cruc,
                            cambiar = true,
                            cliec_icod_autoventa = item.cliec_icod_autoventa,
                            cliec_vcod_cliente = item.cliec_vcod_cliente,
                            anac_icod_analitica = item.anac_icod_analitica,
                            anac_iid_analitica = item.anad_iid_analitica,
                            pcomc_icod_pcompra = item.pcomc_icod_pcompra,
                            cliec_bcredito = item.cliec_bcredito,
                            cliec_nnumero_dias = item.cliec_nnumero_dias,
                            cliec_vubigeo = item.cliec_vubigeo,
                            cliec_bagente_retenedor = Convert.ToBoolean(item.cliec_bagente_retenedor)
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

        public List<EUbigeo> UbigeoListar()
        {
            var lista = new List<EUbigeo>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var collection = dc.SGE_UBIGEO_LISTAR();
                    foreach (var item in collection)
                    {
                        lista.Add(new EUbigeo()
                        {
                            departamento = item.departamento,
                            distrito = item.distrito,
                            latitud = item.latitud,
                            longitud = item.longitud,
                            provincia = item.provincia,
                            region = item.region,
                            ubigeo_inei = item.ubigeo_inei

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

        public List<ECuotasFactura> CuotasFacturaListar(long doxcc_icod_correlativo)
        {
            List<ECuotasFactura> lista = new List<ECuotasFactura>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var collection = dc.SGEV_FACTURA_CREDITO_CUOTAS_LISTAR(Convert.ToInt32(doxcc_icod_correlativo));
                    foreach (var item in collection)
                    {
                        lista.Add(new ECuotasFactura()
                        {
                            fcc_icod = item.fcc_icod,
                            favc_icod_factura = item.favc_icod_factura,
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            fcc_sfecha_pago = item.fcc_sfecha_pago,
                            fcc_nmonto = item.fcc_nmonto,
                            fcc_nsaldo = item.fcc_nsaldo,
                            fcc_iestado = item.fcc_iestado,
                            fcc_inro_cuota = item.fcc_inro_cuota,
                            fcc_nmonto_pagado = item.fcc_nmonto_pagado
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

        public int InsertarCliente(ECliente ob)
        {
            int? id_cliente = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGES_CLIENTE_INSERTAR(
                            ref id_cliente,
                            ob.giroc_icod_giro,
                            ob.cliec_vnombre_cliente,
                            ob.cliec_vnombre_comercial,
                            ob.cliec_vdireccion_cliente,
                            ob.ubicc_icod_ubicacion,
                            ob.cliec_vnro_telefono,
                            ob.cliec_vnro_fax,
                            ob.cliec_vnro_celular,
                            ob.tabl_iid_tipo_documento,
                            ob.cliec_vnumero_doc_cli,
                            ob.cliec_vnombre_contacto,
                            ob.tablc_iid_tipo_relacion_cli,
                            ob.vendc_icod_vendedor,
                            ob.cliec_sfecha_registro_cliente,
                            ob.cliec_iid_situacion_cliente,
                            ob.usuario,
                            ob.pc,
                            ob.cliec_vcorreo_electronico,
                            ob.pcomc_icod_pcompra,
                            ob.cliec_vapellido_paterno,
                            ob.cliec_vapellido_materno,
                            ob.cliec_vnombres,
                            ob.tablc_iid_tipo_cliente,
                            ob.cliec_cruc,
                            ob.cliec_vcod_cliente,
                            ob.cliec_bcredito,
                            ob.cliec_nnumero_dias,
                            ob.cliec_vubigeo,
                            ob.cliec_bagente_retenedor
                            );
                }
                return Convert.ToInt32(id_cliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public EConductor ConductorGet(int remic_icod_remision)
        {
            var obj = new EConductor();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var collection = dc.SGEV_DATOS_CONDUCTOR_GET(remic_icod_remision);
                    foreach (var item in collection)
                    {
                        obj.remic_icod_remision = item.remic_icod_remision;
                        obj.cod_vdni = item.cod_vdni;
                        obj.cod_vnombres = item.cod_vnombres;
                        obj.cod_vapellidos = item.cod_vapellidos;
                        obj.cod_vlicencia = item.cod_vlicencia;

                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return obj;
        }

        public void modificarResumenDiarioResponse(int id, EResumenResponse response)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_RESUEMEN_RESPONSE_MODIFCAR(id, response.NroTicket, response.NombreArchivo, response.Exito, response.MensajeError, response.CodigoRespuesta);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ActualizarCliente(ECliente ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGES_CLIENTE_ACTUALIZAR(ob.cliec_icod_cliente,
                            ob.giroc_icod_giro,
                            ob.cliec_vnombre_cliente,
                            ob.cliec_vnombre_comercial,
                            ob.cliec_vdireccion_cliente,
                            ob.ubicc_icod_ubicacion,
                            ob.cliec_vnro_telefono,
                            ob.cliec_vnro_fax,
                            ob.cliec_vnro_celular,
                            ob.tabl_iid_tipo_documento,
                            ob.cliec_vnumero_doc_cli,
                            ob.cliec_vnombre_contacto,
                            ob.tablc_iid_tipo_relacion_cli,
                            ob.vendc_icod_vendedor,
                            ob.cliec_sfecha_registro_cliente,
                            ob.cliec_iid_situacion_cliente,
                            ob.usuario,
                            ob.pc,
                            ob.cliec_vcorreo_electronico,
                            ob.pcomc_icod_pcompra,
                            ob.cliec_vapellido_paterno,
                            ob.cliec_vapellido_materno,
                            ob.cliec_vnombres,
                            ob.tablc_iid_tipo_cliente,
                            ob.cliec_cruc,
                            ob.anac_icod_analitica,
                            ob.cliec_bcredito,
                            ob.cliec_nnumero_dias,
                            ob.cliec_vubigeo,
                            ob.cliec_bagente_retenedor
                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuiaRemisionElectronicaCambiarEstado(int remic_icod_remision, int estadoEnvio)
        {

            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_GUIA_REMISION_ELECTRONICA_CAMBIAR_ESTADO(remic_icod_remision, estadoEnvio);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGES_CLIENTE_ELIMINAR(cliec_icod_cliente, usuario, pc, anac_icod_analitica);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void actualizarClienteOT(int intOT, int intClienteReal, int intVehiculo)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_ACTUALIZAR_CLIENTE_OT(
                        intOT,
                        intClienteReal,
                        intVehiculo
                           );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarClienteAnalitica(ECliente ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGES_CLIENTE_ACTUALIZAR_ANALITICA(
                            ob.cliec_icod_cliente,
                            ob.anac_icod_analitica
                            );
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
            List<EUbicacion> lista = new List<EUbicacion>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGES_UBICACION_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EUbicacion()
                        {
                            ubicc_icod_ubicacion = item.ubicc_icod_ubicacion,
                            tablc_iid_tipo_ubicacion = item.tablc_iid_tipo_ubicacion,
                            Ubicacion = item.Ubicacion,
                            ubicc_ccod_ubicacion = item.ubicc_ccod_ubicacion,
                            ubicc_vnombre_ubicacion = item.ubicc_vnombre_ubicacion,
                            ubicc_icod_ubicacion_padre = item.ubicc_icod_ubicacion_padre,
                            ubicc_iid_situacion_ubicacion = item.ubicc_iid_situacion_ubicacion
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
        public void InsertarUbicacion(EUbicacion ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGES_UBICACION_INSERTAR(ob.tablc_iid_tipo_ubicacion,
                        ob.ubicc_ccod_ubicacion,
                        ob.ubicc_vnombre_ubicacion,
                        ob.ubicc_icod_ubicacion_padre,
                        ob.ubicc_iid_situacion_ubicacion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarUbicacion(EUbicacion ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGES_UBICACION_ACTUALIZAR(ob.ubicc_icod_ubicacion,
                        ob.tablc_iid_tipo_ubicacion,
                        ob.ubicc_ccod_ubicacion,
                        ob.ubicc_vnombre_ubicacion,
                        ob.ubicc_icod_ubicacion_padre,
                        ob.ubicc_iid_situacion_ubicacion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void GuiaRemisionElectronicaResponseGuardar(EGuiaRemisionElectronicaResponse resGRE)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_GUIA_REMISION_ELECTRONICA_RESPONSE_INSERTAR
                        (
                        resGRE.remic_icod_remision,
                        resGRE.FechaEnvio,
                        resGRE.Exito,
                        resGRE.MensajeError,
                        resGRE.NumeroTicket,
                        resGRE.CodigoRespuesta,
                        resGRE.MensajeRespuesta,
                        resGRE.LinkDescarga
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGES_UBICACION_ELIMINAR(ubicc_icod_ubicacion);
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
            List<EGiro> lista = new List<EGiro>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGES_GIRO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EGiro()
                        {
                            giroc_icod_giro = item.giroc_icod_giro,
                            giroc_iid_giro = item.giroc_iid_giro,
                            giroc_vnombre_giro = item.giroc_vnombre_giro,
                            tablc_iid_situacion_giro = item.tablc_iid_situacion_giro,
                            giroc_bindicador_expo_nextel = item.giroc_bindicador_expo_nextel,
                            DescripSituacion = item.DescripSituacion
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
        public void InsertarGiro(EGiro ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGES_GIRO_INSERTAR(ob.giroc_iid_giro,
                            ob.giroc_vnombre_giro,
                            ob.tablc_iid_situacion_giro,
                            ob.giroc_bindicador_expo_nextel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarGiro(EGiro ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGES_GIRO_ACTUALIZAR(ob.giroc_icod_giro,
                            ob.giroc_iid_giro,
                            ob.giroc_vnombre_giro,
                            ob.tablc_iid_situacion_giro,
                            ob.giroc_bindicador_expo_nextel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EliminarGiro(int giroc_icod_giro)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGES_GIRO_ELIMINAR(giroc_icod_giro);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        public List<EAdelantoProveedor> ListarAdelantoProveedoresCorrelativo(int intEjercicio)
        {
            List<EAdelantoProveedor> lista = new List<EAdelantoProveedor>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTT_ADELANTO_PROVEEDOR_CORRELATIVOS_LISTAR(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EAdelantoProveedor()
                        {
                            vnumero_adelanto = item.adpr_vnumero_adelanto
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
        public List<EAdelantoCliente> ListarAdelantoClientexAñoTodos(int intEjercicio)
        {
            List<EAdelantoCliente> lista = new List<EAdelantoCliente>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTT_ADELANTO_CLIENTE_LISTAR_X_AÑO(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EAdelantoCliente()
                        {
                            vnumero_adelanto = item.adci_vnumero_adelanto
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
        #region Factura
        public List<EFacturaCab> getFacturaCab(int id_factura)
        {
            List<EFacturaCab> lista = new List<EFacturaCab>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FACTURA_CAB_GET(id_factura);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaCab()
                        {
                            favc_icod_factura = item.favc_icod_factura,
                            favc_vnumero_factura = item.favc_vnumero_factura,
                            favc_sfecha_factura = item.favc_sfecha_factura,
                            favc_sfecha_vencim_factura = item.favc_sfecha_vencim_factura,
                            favc_icod_cliente = item.favc_icod_cliente,
                            clic_vcod_cliente = item.cliec_vcod_cliente,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            favc_vdireccion_cliente = item.cliec_vdireccion_cliente,
                            favc_vruc = item.cliec_cruc,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            tablc_iid_forma_pago = item.tablc_iid_forma_pago,
                            tablc_iid_situacion = item.tablc_iid_situacion,
                            favc_npor_imp_igv = item.favc_npor_imp_igv,
                            favc_nmonto_neto = item.favc_nmonto_neto,
                            favc_nmonto_imp = item.favc_nmonto_imp,
                            favc_nmonto_total = item.favc_nmonto_total,
                            doxcc_icod_correlativo = Convert.ToInt32(item.doxcc_icod_correlativo),
                            strFormaPago = item.strFormaPago,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            strTelefonoCliente = item.strTelefonoCliente,
                            strDistritoCliente = item.strDistritoCliente,
                            favc_vobservacion = item.favc_vobservacion,
                            favc_bincluye_igv = Convert.ToBoolean(item.favc_bincluye_igv),
                            remic_icod_remision = item.remic_icod_remision,
                            remic_vnumero_remision = item.remic_vnumero_remision,
                            //favc_bafecto_igv=Convert.ToBoolean(item.favc_bafecto_igv),

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

        public EUbigeo UbigeoGet(string ubigeo)
        {
            var obj = new EUbigeo();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var collection = dc.SGE_UBIGEO_GET(ubigeo);
                    foreach (var item in collection)
                    {
                        obj.ubigeo_inei = item.ubigeo_inei;
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return obj;
        }

        public void GuiaRemisionElectronicaResponseModificar(EGuiaRemisionElectronicaResponse responseGRE)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.GUIA_REMISION_ELECTRONICA_RESPONSE_MODIFICAR
                        (
                        responseGRE.NumeroTicket,
                        responseGRE.MensajeError,
                        responseGRE.CodigoRespuesta,
                        responseGRE.MensajeRespuesta,
                        responseGRE.LinkDescarga
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public EFacturaCab getFacturaCabXDxp(long doxcc_icod_correlativo)
        {
            List<EFacturaCab> lista = new List<EFacturaCab>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FACTURA_CAB_GET_X_DXP(Convert.ToInt32(doxcc_icod_correlativo));
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaCab()
                        {
                            favc_icod_factura = item.favc_icod_factura,
                            favc_vnumero_factura = item.favc_vnumero_factura,
                            favc_sfecha_factura = item.favc_sfecha_factura,
                            favc_sfecha_vencim_factura = item.favc_sfecha_vencim_factura,
                            favc_icod_cliente = item.favc_icod_cliente,
                            clic_vcod_cliente = item.cliec_vcod_cliente,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            favc_vdireccion_cliente = item.cliec_vdireccion_cliente,
                            favc_vruc = item.cliec_cruc,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            tablc_iid_forma_pago = item.tablc_iid_forma_pago,
                            tablc_iid_situacion = item.tablc_iid_situacion,
                            favc_npor_imp_igv = item.favc_npor_imp_igv,
                            favc_nmonto_neto = item.favc_nmonto_neto,
                            favc_nmonto_imp = item.favc_nmonto_imp,
                            favc_nmonto_total = item.favc_nmonto_total,
                            doxcc_icod_correlativo = Convert.ToInt32(item.doxcc_icod_correlativo),
                            strFormaPago = item.strFormaPago,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            strTelefonoCliente = item.strTelefonoCliente,
                            strDistritoCliente = item.strDistritoCliente,
                            favc_vobservacion = item.favc_vobservacion,
                            favc_bincluye_igv = Convert.ToBoolean(item.favc_bincluye_igv),
                            remic_icod_remision = item.remic_icod_remision,
                            remic_vnumero_remision = item.remic_vnumero_remision,
                            //favc_bafecto_igv=Convert.ToBoolean(item.favc_bafecto_igv),
                            facv_nmonto_1era_cuota = item.facv_nmonto_1era_cuota,
                            facv_sfecha_pago_1era_cuota = item.facv_sfecha_pago_1era_cuota,
                            facv_nmonto_credito = item.facv_nmonto_credito

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista.FirstOrDefault();
        }

        public void ResumenDocumentosCabMensajeRespuestaModificar(ESunatResumenDocumentosCab item, string mensajeRespuesta, DateTime? fechaEnvio)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_SUNAT_RESUMEN_DOCUMENTOS_CAB_MENSAJE_RESPUESTA_ACTUALIZAR(item.IdCabecera, mensajeRespuesta, fechaEnvio);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<EFacturaCab> listarFacturaCab(int intEjericio)
        {
            List<EFacturaCab> lista = new List<EFacturaCab>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FACTURA_CAB_LISTAR(intEjericio);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaCab()
                        {
                            favc_icod_factura = item.favc_icod_factura,
                            favc_vnumero_factura = item.favc_vnumero_factura,
                            favc_sfecha_factura = Convert.ToDateTime(item.favc_sfecha_factura),
                            favc_sfecha_vencim_factura = item.favc_sfecha_vencim_factura,
                            favc_icod_cliente = item.favc_icod_cliente,
                            clic_vcod_cliente = item.cliec_vcod_cliente,
                            favc_vdireccion_cliente = item.cliec_vdireccion_cliente,
                            favc_vruc = item.cliec_cruc,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            tablc_iid_forma_pago = item.tablc_iid_forma_pago,
                            tablc_iid_situacion = item.tablc_iid_situacion,
                            favc_npor_imp_igv = item.favc_npor_imp_igv,
                            favc_nmonto_neto = item.favc_nmonto_neto,
                            favc_noperacion_gravadas = Convert.ToDecimal(item.favc_noperacion_gravadas),
                            favc_noperacion_inafectas = Convert.ToDecimal(item.favc_noperacion_inafectas),
                            favc_nmonto_imp = item.favc_nmonto_imp,
                            favc_nmonto_total = item.favc_nmonto_total,
                            doxcc_icod_correlativo = Convert.ToInt32(item.doxcc_icod_correlativo),
                            strFormaPago = item.strFormaPago,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            strTelefonoCliente = item.strTelefonoCliente,
                            strDistritoCliente = item.strDistritoCliente,
                            favc_vobservacion = item.favc_vobservacion,
                            favc_bincluye_igv = Convert.ToBoolean(item.favc_bincluye_igv),
                            ubicc_icod_ubicacion = Convert.ToInt32(item.ubicc_icod_ubicacion),
                            cliec_nnumero_dias = Convert.ToInt32(item.cliec_nnumero_dias),
                            remic_icod_remision = item.remic_icod_remision,
                            //favc_bafecto_igv = Convert.ToBoolean(item.favc_bafecto_igv),
                            vendc_icod_vendedor = Convert.ToInt32(item.vendc_icod_vendedor),
                            puvec_icod_punto_venta = Convert.ToInt32(item.puvec_icod_punto_venta),
                            favc_iid_almacen = Convert.ToInt32(item.favc_iid_almacen),
                            DesAlmacen = item.DesAlmacen,
                            favc_vnumero_orden = item.favc_vnumero_orden,
                            favc_vnumero_guia = item.favc_vnumero_guia,
                            doc_icod_documento = item.favc_icod_factura,
                            favc_tipo_factura = Convert.ToInt32(item.favc_tipo_factura),
                            favc_itipo_venta = Convert.ToInt32(item.favc_itipo_venta),
                            favc_vnumero_orden_pedido = item.favc_vnumero_orden_pedido,
                            favc_icod_pvt = Convert.ToInt32(item.favc_icod_pvt),
                            DesPVT = item.DesPVT,
                            facv_nmonto_1era_cuota = item.facv_nmonto_1era_cuota,
                            facv_sfecha_pago_1era_cuota = item.facv_sfecha_pago_1era_cuota,
                            facv_nmonto_credito = item.facv_nmonto_credito,
                            FEmisionPresentacion = item.favc_sfecha_factura.ToString().Substring(0, 10),
                            EstadoSunat = item.EstadoSunat,
                            EstadoFacturacion = Convert.ToInt32(item.EstadoFacturacion),
                            facv_nporc_retencion = item.facv_nporc_retencion,
                            facv_nmonto_retencion = item.facv_nmonto_retencion
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

        public int CuotasFacturaIngresar(ECuotasFactura obj)

        {
            int? code = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_CREDITO_CUOTAS_INSERTAR(
                        ref code,
                        obj.favc_icod_factura,
                        obj.doxcc_icod_correlativo,
                        obj.fcc_inro_cuota,
                        obj.fcc_sfecha_pago,
                        obj.fcc_nmonto,
                        obj.fcc_nmonto_pagado,
                        obj.fcc_nsaldo,
                        obj.fcc_iestado,
                        obj.intUsuario,
                        obj.strPc,
                        DateTime.Now
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Convert.ToInt32(code);
        }

        public List<EGuiaRemisionElectronicaDet> GetByIDGuiaRemisionElectronicaDet(int remic_icod_remision)
        {
            List<EGuiaRemisionElectronicaDet> lista = new List<EGuiaRemisionElectronicaDet>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var collection = dc.SGE_GUIA_REMISION_ELECTRONICA_DET_GET_BY_ID(remic_icod_remision);
                    foreach (var item in collection)
                    {
                        lista.Add(new EGuiaRemisionElectronicaDet()
                        {
                            remic_icod_remision = Convert.ToInt32(item.remic_icod_remision),
                            Correlativo = Convert.ToInt32(item.Correlativo),
                            CodigoItem = item.CodigoItem,
                            Descripcion = item.Descripcion,
                            UnidadMedida = item.UnidadMedida,
                            Cantidad = Convert.ToDecimal(item.Cantidad),
                            PesoItem = Convert.ToDecimal(item.PesoItem),
                            LineaReferencia = Convert.ToInt32(item.LineaReferencia),
                            UM = item.UM
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

        public EGuiaRemisionElectronica GetByIDGuiaRemisionElectronica(int remic_icod_remision)
        {
            EGuiaRemisionElectronica lista = new EGuiaRemisionElectronica();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_GUIA_REMISION_ELECTRONICA_GET_BY_ID(remic_icod_remision);
                    foreach (var item in query)
                    {
                        lista.IdDocumento = item.IdDocumento;
                        lista.remic_icod_remision = Convert.ToInt32(item.remic_icod_remision);
                        lista.FechaEmision = item.FechaEmision;
                        lista.HoraEmision = item.HoraEmision;
                        lista.TipoDocumento = item.TipoDocumento;
                        lista.Glosa = item.Glosa;
                        lista.numDocRemitente = item.numDocRemitente;
                        lista.tipDocRemitente = item.tipDocRemitente;
                        lista.rznSocialRemitente = item.rznSocialRemitente;
                        lista.numDocDestinatario = item.numDocDestinatario;
                        lista.tipDocDestinatario = item.tipDocDestinatario;
                        lista.rznSocialDestinatario = item.rznSocialDestinatario;
                        lista.CodigoMotivoTraslado = item.CodigoMotivoTraslado;
                        lista.DescripcionMotivo = item.DescripcionMotivo;
                        lista.Transbordo = Convert.ToBoolean(item.Transbordo);
                        lista.PesoBrutoTotal = Convert.ToDecimal(item.PesoBrutoTotal);
                        lista.NroPallets = Convert.ToInt32(item.NroPallets);
                        lista.ModalidadTraslado = item.ModalidadTraslado;
                        lista.FechaInicioTraslado = item.FechaInicioTraslado;
                        lista.RucTransportista = item.RucTransportista;
                        lista.RazonSocialTransportista = item.RazonSocialTransportista;
                        lista.NroPlacaVehiculo = item.NroPlacaVehiculo;
                        lista.NroDocumentoConductor = item.NroDocumentoConductor;
                        lista.ubiLlegada = item.ubiLlegada;
                        lista.dirLlegada = item.dirLlegada;
                        lista.ubiPartida = item.ubiPartida;
                        lista.dirPartida = item.dirPartida;
                        lista.obsGuia = item.obsGuia;
                        lista.NumeroContenedor = item.NumeroContenedor;
                        lista.CodigoPuerto = item.CodigoPuerto;
                        lista.ShipmentId = item.ShipmentId;
                        lista.ObsServaciones = item.ObsServaciones;
                        lista.UmPesoBruto = item.UmPesoBruto;
                        lista.NumBultos = Convert.ToDecimal(item.NumBultos);
                        lista.TipoDocumentoTransportista = item.TipoDocumentoTransportista;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int insertarFactura(EFacturaCab ob)
        {
            int? id_factura = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_CAB_INSERTAR(
                            ref id_factura,
                            ob.favc_vnumero_factura,
                            ob.favc_sfecha_factura,
                            ob.favc_sfecha_vencim_factura,
                            ob.favc_icod_cliente,
                            ob.tablc_iid_tipo_moneda,
                            ob.tablc_iid_forma_pago,
                            ob.tablc_iid_situacion,
                            ob.favc_npor_imp_igv,
                            ob.favc_nmonto_neto,
                            ob.favc_noperacion_gravadas,
                            ob.favc_noperacion_inafectas,
                            ob.favc_nmonto_imp,
                            ob.favc_nmonto_total,
                            ob.doxcc_icod_correlativo,
                            ob.favc_vobservacion,
                            ob.favc_bincluye_igv,
                            ob.intUsuario,
                            ob.strPc,
                            ob.remic_icod_remision,
                            ob.vendc_icod_vendedor,
                            ob.puvec_icod_punto_venta,
                            ob.favc_iid_almacen,
                            ob.favc_vnumero_orden,
                            ob.favc_vnumero_guia,
                            ob.favc_tipo_factura,
                            ob.favc_itipo_venta,
                            ob.favc_vnumero_orden_pedido,
                            ob.favc_icod_pvt,
                            ob.facv_nmonto_1era_cuota,
                            ob.facv_sfecha_pago_1era_cuota,
                            ob.facv_nmonto_credito,
                            ob.facv_nporc_retencion,
                            ob.facv_nmonto_retencion
                            );
                }
                return Convert.ToInt32(id_factura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFactura(EFacturaCab ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_CAB_MODIFICAR(
                            ob.favc_icod_factura,
                            ob.favc_vnumero_factura,
                            ob.favc_sfecha_factura,
                            ob.favc_sfecha_vencim_factura,
                            ob.favc_icod_cliente,
                            ob.tablc_iid_tipo_moneda,
                            ob.tablc_iid_forma_pago,
                            ob.tablc_iid_situacion,
                            ob.favc_npor_imp_igv,
                            ob.favc_nmonto_neto,
                            ob.favc_noperacion_gravadas,
                            ob.favc_noperacion_inafectas,
                            ob.favc_nmonto_imp,
                            ob.favc_nmonto_total,
                            ob.favc_vobservacion,
                            ob.favc_bincluye_igv,
                            ob.intUsuario,
                            ob.strPc,
                            ob.remic_icod_remision,
                            ob.vendc_icod_vendedor,
                            ob.puvec_icod_punto_venta,
                            ob.favc_iid_almacen,
                            ob.favc_vnumero_orden,
                            ob.favc_vnumero_guia,
                            ob.favc_vnumero_orden_pedido,
                            ob.favc_icod_pvt,
                            ob.facv_nmonto_1era_cuota,
                            ob.facv_sfecha_pago_1era_cuota,
                            ob.facv_nmonto_credito,
                            ob.facv_nporc_retencion,
                            ob.facv_nmonto_retencion
                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFactura(EFacturaCab ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_CAB_ELIMINAR(
                            ob.favc_icod_factura,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void anularFactura(EFacturaCab ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_CAB_ANULAR(
                            ob.favc_icod_factura,
                            ob.intUsuario,
                            ob.strPc
                            );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FACTURA_DET_LISTAR(intFactura);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaDet()
                        {
                            favd_icod_item_factura = item.favd_icod_item_factura,
                            favc_icod_factura = item.favc_icod_factura,
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            favd_iitem_factura = Convert.ToInt32(item.favd_iitem_factura),
                            tablc_iid_motivo = item.tablc_iid_motivo,
                            favd_vcodigo_extremo_prod = item.favd_vcodigo_extremo_prod,
                            favd_vdescripcion_prod = item.favd_vdescripcion_prod,
                            favd_ncantidad_total_producto = Convert.ToDecimal(item.favd_ncantidad_total_producto),
                            favd_icod_serie = Convert.ToInt32(item.favd_icod_serie),
                            favd_rango_talla = item.favd_rango_talla,
                            favd_talla1 = item.favd_talla1,
                            favd_talla2 = item.favd_talla2,
                            favd_talla3 = item.favd_talla3,
                            favd_talla4 = item.favd_talla4,
                            favd_talla5 = item.favd_talla5,
                            favd_talla6 = item.favd_talla6,
                            favd_talla7 = item.favd_talla7,
                            favd_talla8 = item.favd_talla8,
                            favd_talla9 = item.favd_talla9,
                            favd_talla10 = item.favd_talla10,
                            favd_cant_talla1 = item.favd_cant_talla1,
                            favd_cant_talla2 = item.favd_cant_talla2,
                            favd_cant_talla3 = item.favd_cant_talla3,
                            favd_cant_talla4 = item.favd_cant_talla4,
                            favd_cant_talla5 = item.favd_cant_talla5,
                            favd_cant_talla6 = item.favd_cant_talla6,
                            favd_cant_talla7 = item.favd_cant_talla7,
                            favd_cant_talla8 = item.favd_cant_talla8,
                            favd_cant_talla9 = item.favd_cant_talla9,
                            favd_cant_talla10 = item.favd_cant_talla10,
                            favd_iid_kardex1 = item.favd_iid_kardex1,
                            favd_iid_kardex2 = item.favd_iid_kardex2,
                            favd_iid_kardex3 = item.favd_iid_kardex3,
                            favd_iid_kardex4 = item.favd_iid_kardex4,
                            favd_iid_kardex5 = item.favd_iid_kardex5,
                            favd_iid_kardex6 = item.favd_iid_kardex6,
                            favd_iid_kardex7 = item.favd_iid_kardex7,
                            favd_iid_kardex8 = item.favd_iid_kardex8,
                            favd_iid_kardex9 = item.favd_iid_kardex9,
                            favd_iid_kardex10 = item.favd_iid_kardex10,
                            favd_icod_producto1 = Convert.ToInt32(item.favd_icod_producto1),
                            favd_icod_producto2 = Convert.ToInt32(item.favd_icod_producto2),
                            favd_icod_producto3 = Convert.ToInt32(item.favd_icod_producto3),
                            favd_icod_producto4 = Convert.ToInt32(item.favd_icod_producto4),
                            favd_icod_producto5 = Convert.ToInt32(item.favd_icod_producto5),
                            favd_icod_producto6 = Convert.ToInt32(item.favd_icod_producto6),
                            favd_icod_producto7 = Convert.ToInt32(item.favd_icod_producto7),
                            favd_icod_producto8 = Convert.ToInt32(item.favd_icod_producto8),
                            favd_icod_producto9 = Convert.ToInt32(item.favd_icod_producto9),
                            favd_icod_producto10 = Convert.ToInt32(item.favd_icod_producto10),
                            favd_nprecio_unitario_item = Convert.ToDecimal(item.favd_nprecio_unitario_item),
                            favd_nmonto_impuesto_item = Convert.ToDecimal(item.favd_nmonto_impuesto_item),
                            favd_nprecio_total_item = Convert.ToDecimal(item.favd_nprecio_total_item),
                            resec_vdescripcion = item.resec_vdescripcion,
                            resec_vtalla_inicial = item.resec_vtalla_inicial,
                            resec_vtalla_final = item.resec_vtalla_final,
                            unidc_icod_unidad_medida = Convert.ToInt32(item.unidc_icod_unidad_medida),
                            strDesUM = item.strUM,
                            CodigoSUNAT = item.CodigoSUNAT,
                            flag_afecto_igv = Convert.ToBoolean(item.flag_afecto_igv),
                            favd_nneto_igv = Convert.ToDecimal(item.favd_nneto_igv),
                            favd_nneto_exo = Convert.ToDecimal(item.favd_nneto_exo),


                            NumeroOrdenItem = item.favd_iitem_factura,
                            cantidad = Convert.ToDecimal(item.favd_ncantidad_total_producto),
                            unidadMedida = item.CodigoSUNAT,
                          
                            ValorVentaItem = Math.Round((Convert.ToDecimal(item.favd_nprecio_total_item) - Convert.ToDecimal(item.favd_nmonto_impuesto_item)), 2),
                            CodMotivoDescuentoItem = 0,
                            FactorDescuentoItem = 0,
                            DescuentoItem = 0,
                            BaseDescuentotem = 0,
                            CodMotivoCargoItem = 0,
                            FactorCargoItem = 0,
                            MontoCargoItem = 0,
                            BaseCargoItem = 0,
                            MontoTotalImpuestosItem = Convert.ToDecimal(item.favd_nmonto_impuesto_item),
                            MontoImpuestoIgvItem = Convert.ToDecimal(item.favd_nmonto_impuesto_item),
                            MontoAfectoImpuestoIgv = Convert.ToDecimal(item.favd_nmonto_impuesto_item) == 0 ? 0 : Convert.ToDecimal((item.favd_nneto_igv)),
                            PorcentajeIGVItem = 18,
                            MontoInafectoItem = Convert.ToDecimal(item.favd_nmonto_impuesto_item) == 0 ? Convert.ToDecimal(item.favd_nprecio_total_item) : 0,
                            MontoImpuestoISCItem = 0,
                            MontoAfectoImpuestoIsc = 0,
                            PorcentajeISCtem = 0,
                            MontoImpuestoIVAPtem = 0,
                            MontoAfectoImpuestoIVAPItem = 0,
                            PorcentajeIVAPItem = 0,
                            descripcion = item.favd_vdescripcion_prod,
                            codigoItem = item.favd_vcodigo_extremo_prod,
                            ObservacionesItem = "",
                            ValorUnitarioItem = Math.Round(Convert.ToDecimal(item.favd_nprecio_unitario_item / Convert.ToDecimal("1." + "18".Replace(".", ""))), 6),
                            PrecioVentaUnitarioItem = (item.favd_nprecio_total_item / item.favd_ncantidad_total_producto),
                            tipoImpuesto = item.favd_nmonto_impuesto_item > 0 ? "10" : "30",
                            UMedida = item.strUM

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
        public List<EFacturaMPDet> listarFacturaMPDetalle(int intFactura)
        {
            List<EFacturaMPDet> lista = new List<EFacturaMPDet>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FACTURA_MP_DET_LISTAR(intFactura);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaMPDet()
                        {
                            favd_icod_item_factura = item.favd_icod_item_factura,
                            favc_icod_factura = item.favc_icod_factura,
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            favd_iitem_factura = Convert.ToInt32(item.favd_iitem_factura),
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            favd_ncantidad = Convert.ToDecimal(item.favd_ncantidad),
                            favd_vdescripcion = item.favd_vdescripcion,
                            favd_nprecio_unitario_item = Convert.ToDecimal(item.favd_nprecio_unitario_item),
                            favd_nmonto_impuesto_item = Convert.ToDecimal(item.favd_nmonto_impuesto_item),
                            favd_nprecio_total_item = Convert.ToDecimal(item.favd_nprecio_total_item),
                            strDesUM = item.strUM,
                            strUM = item.strUM,
                            CodigoSUNAT = item.CodigoSUNAT,
                            flag_afecto_igv = Convert.ToBoolean(item.flag_afecto_igv),
                            strCodProducto = item.CodProducto,
                            strDesProducto = item.favd_vdescripcion,
                            strMoneda = item.strMoneda,
                            unidc_icod_unidad_medida = Convert.ToInt32(item.unidc_icod_unidad_medida),
                            favd_nneto_igv = Convert.ToDecimal(item.favd_nneto_igv),
                            favd_nneto_exo = Convert.ToDecimal(item.favd_nneto_exo),

                            NumeroOrdenItem = item.favd_iitem_factura,
                            cantidad = Convert.ToDecimal(item.favd_ncantidad),
                            unidadMedida = item.CodigoSUNAT,
                            ValorVentaItem = Convert.ToDecimal(item.favd_nprecio_total_item),
                            CodMotivoDescuentoItem = 0,
                            FactorDescuentoItem = 0,
                            DescuentoItem = 0,
                            BaseDescuentotem = 0,
                            CodMotivoCargoItem = 0,
                            FactorCargoItem = 0,
                            MontoCargoItem = 0,
                            BaseCargoItem = 0,
                            MontoTotalImpuestosItem = Convert.ToDecimal(item.favd_nmonto_impuesto_item),
                            MontoImpuestoIgvItem = Convert.ToDecimal(item.favd_nmonto_impuesto_item),
                            MontoAfectoImpuestoIgv = Convert.ToDecimal(item.favd_nmonto_impuesto_item) == 0 ? 0 : Convert.ToDecimal((item.favd_nneto_igv)),
                            PorcentajeIGVItem = 18,
                            MontoInafectoItem = Convert.ToDecimal(item.favd_nmonto_impuesto_item) == 0 ? Convert.ToDecimal(item.favd_nprecio_total_item) : 0,
                            MontoImpuestoISCItem = 0,
                            MontoAfectoImpuestoIsc = 0,
                            PorcentajeISCtem = 0,
                            MontoImpuestoIVAPtem = 0,
                            MontoAfectoImpuestoIVAPItem = 0,
                            PorcentajeIVAPItem = 0,
                            descripcion = item.favd_vdescripcion,
                            codigoItem = item.CodProducto,
                            ObservacionesItem = "",
                            ValorUnitarioItem = Math.Round(Convert.ToDecimal((item.favd_nneto_igv / item.favd_ncantidad)), 4),
                            tipoImpuesto = item.favd_nmonto_impuesto_item > 0 ? "10" : "30",
                            UMedida = item.strUM

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
        public void insertarFacturaDetalle(EFacturaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_DET_INSERTAR(
                            ob.favc_icod_factura,
                            ob.almac_icod_almacen,
                            ob.favd_iitem_factura,
                            ob.tablc_iid_motivo,
                            ob.favd_vcodigo_extremo_prod,
                            ob.favd_vdescripcion_prod,
                            ob.favd_ncantidad_total_producto,
                            ob.favd_icod_serie,
                            ob.favd_rango_talla,
                            ob.favd_talla1,
                            ob.favd_talla2,
                            ob.favd_talla3,
                            ob.favd_talla4,
                            ob.favd_talla5,
                            ob.favd_talla6,
                            ob.favd_talla7,
                            ob.favd_talla8,
                            ob.favd_talla9,
                            ob.favd_talla10,
                            ob.favd_cant_talla1,
                            ob.favd_cant_talla2,
                            ob.favd_cant_talla3,
                            ob.favd_cant_talla4,
                            ob.favd_cant_talla5,
                            ob.favd_cant_talla6,
                            ob.favd_cant_talla7,
                            ob.favd_cant_talla8,
                            ob.favd_cant_talla9,
                            ob.favd_cant_talla10,
                            ob.favd_iid_kardex1,
                            ob.favd_iid_kardex2,
                            ob.favd_iid_kardex3,
                            ob.favd_iid_kardex4,
                            ob.favd_iid_kardex5,
                            ob.favd_iid_kardex6,
                            ob.favd_iid_kardex7,
                            ob.favd_iid_kardex8,
                            ob.favd_iid_kardex9,
                            ob.favd_iid_kardex10,
                            ob.favd_icod_producto1,
                            ob.favd_icod_producto2,
                            ob.favd_icod_producto3,
                            ob.favd_icod_producto4,
                            ob.favd_icod_producto5,
                            ob.favd_icod_producto6,
                            ob.favd_icod_producto7,
                            ob.favd_icod_producto8,
                            ob.favd_icod_producto9,
                            ob.favd_icod_producto10,
                            ob.intUsuario,
                            ob.strPc,
                            ob.favd_nprecio_unitario_item,
                            ob.favd_nmonto_impuesto_item,
                            ob.favd_nprecio_total_item,
                            ob.unidc_icod_unidad_medida,
                            ob.flag_afecto_igv,
                            ob.favd_nneto_igv,
                            ob.favd_nneto_exo
                           );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void insertarFacturaMPDetalle(EFacturaMPDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_MP_DET_INSERTAR(
                            ob.favc_icod_factura,
                            ob.almac_icod_almacen,
                            ob.favd_iitem_factura,
                            ob.prdc_icod_producto,
                            ob.favd_ncantidad,
                            ob.favd_vdescripcion,
                            ob.intUsuario,
                            ob.strPc,
                            ob.favd_nprecio_unitario_item,
                            ob.favd_nmonto_impuesto_item,
                            ob.favd_nprecio_total_item,
                            ob.unidc_icod_unidad_medida,
                            ob.kardc_icod_correlativo,
                            ob.flag_afecto_igv,
                            ob.favd_nneto_igv,
                            ob.favd_nneto_exo
                           );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFacturaDetalle(EFacturaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_DET_MODIFICAR(
                            ob.favd_icod_item_factura,
                            ob.almac_icod_almacen,
                            ob.favd_iitem_factura,
                            ob.tablc_iid_motivo,
                            ob.favd_vcodigo_extremo_prod,
                            ob.favd_vdescripcion_prod,
                            ob.favd_ncantidad_total_producto,
                            ob.favd_icod_serie,
                            ob.favd_rango_talla,
                            ob.favd_talla1,
                            ob.favd_talla2,
                            ob.favd_talla3,
                            ob.favd_talla4,
                            ob.favd_talla5,
                            ob.favd_talla6,
                            ob.favd_talla7,
                            ob.favd_talla8,
                            ob.favd_talla9,
                            ob.favd_talla10,
                            ob.favd_cant_talla1,
                            ob.favd_cant_talla2,
                            ob.favd_cant_talla3,
                            ob.favd_cant_talla4,
                            ob.favd_cant_talla5,
                            ob.favd_cant_talla6,
                            ob.favd_cant_talla7,
                            ob.favd_cant_talla8,
                            ob.favd_cant_talla9,
                            ob.favd_cant_talla10,
                            ob.favd_iid_kardex1,
                            ob.favd_iid_kardex2,
                            ob.favd_iid_kardex3,
                            ob.favd_iid_kardex4,
                            ob.favd_iid_kardex5,
                            ob.favd_iid_kardex6,
                            ob.favd_iid_kardex7,
                            ob.favd_iid_kardex8,
                            ob.favd_iid_kardex9,
                            ob.favd_iid_kardex10,
                            ob.favd_icod_producto1,
                            ob.favd_icod_producto2,
                            ob.favd_icod_producto3,
                            ob.favd_icod_producto4,
                            ob.favd_icod_producto5,
                            ob.favd_icod_producto6,
                            ob.favd_icod_producto7,
                            ob.favd_icod_producto8,
                            ob.favd_icod_producto9,
                            ob.favd_icod_producto10,
                            ob.intUsuario,
                            ob.strPc,
                            ob.favd_nprecio_unitario_item,
                            ob.favd_nmonto_impuesto_item,
                            ob.favd_nprecio_total_item,
                            ob.favd_nneto_igv,
                            ob.favd_nneto_exo
                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFacturaMPDetalle(EFacturaMPDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_MP_DET_MODIFICAR(
                            ob.favd_icod_item_factura,
                            ob.almac_icod_almacen,
                            ob.favd_iitem_factura,
                            ob.prdc_icod_producto,
                            ob.favd_ncantidad,
                            ob.favd_vdescripcion,
                            //ob.intUsuario,
                            ob.strPc,
                            ob.favd_nprecio_unitario_item,
                            ob.favd_nmonto_impuesto_item,
                            ob.favd_nprecio_total_item,
                            ob.kardc_icod_correlativo,
                            ob.favd_nneto_igv,
                            ob.favd_nneto_exo
                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFacturaDetalle(EFacturaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_DET_ELIMINAR(
                            ob.favd_icod_item_factura,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFacturaMPDetalle(EFacturaMPDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_MP_DET_ELIMINAR(
                            ob.favd_icod_item_factura,
                            ob.intUsuario,
                            ob.strPc);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_ACTUALIZAR_FACTURA_VENTA(favc_icod_factura, favc_bafecto_igv);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Mantenimiento Factura Venta Electronica
        public List<EFacturaVentaElectronica> listarfacturaVentaElectronicaFecha(DateTime fechaInicio)
        {
            int index = 0;
            DateTime dateOut;
            List<EFacturaVentaElectronica> lista = new List<EFacturaVentaElectronica>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FACTURA_VENTA_ELECTRONICA_LISTAR_FECHA(fechaInicio);
                    foreach (var item in query)
                    {
                        index++;
                        if (index == 791)
                        {

                        }
                        lista.Add(new EFacturaVentaElectronica()
                        {
                            IdCabecera = item.IdCabecera,
                            idDocumento = item.idDocumento.Trim(),
                            fechaEmision = DateTime.ParseExact(item.fechaEmision.Length == 9 ? "0" + item.fechaEmision.Trim() : item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            horaEmision = item.horaEmision.Trim(),
                            FEmisionPresentacion = DateTime.ParseExact(item.fechaEmision.Length == 9 ? "0" + item.fechaEmision.Trim() : item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            fechaVencimiento = DateTime.TryParseExact(item.fehaVencimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOut) == true ? DateTime.ParseExact(item.fehaVencimiento.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy") : "",
                            tipoDocumento = item.tipoDocumento.Trim(),
                            StrTipoDoc = item.StrTipoDoc,
                            moneda = item.moneda.Trim(),
                            CodMotivoNota = item.CodMotivoNota,
                            DescripMotivoNota = item.DescripMotivoNota,
                            NroDocqModifica = item.NroDocqModifica,
                            TipoDocqModifica = item.TipoDocqModifica,
                            cantidadItems = Convert.ToInt32(item.cantidadItems),
                            nombreComercialEmisor = item.nombreComercialEmisor.Trim(),
                            nombreLegalEmisor = item.nombreLegalEmisor.Trim(),
                            tipoDocumentoEmisor = item.tipoDocumentoEmisor.Trim(),
                            nroDocumentoEmisior = item.nroDocumentoEmisior.Trim(),
                            CodLocalEmisor = Convert.ToInt32(item.CodLocalEmisor),
                            nroDocumentoReceptor = item.nroDocumentoReceptor.Trim(),
                            tipoDocumentoReceptor = item.tipoDocumentoReceptor.Trim(),
                            nombreLegalReceptor = item.nombreLegalReceptor.Trim(),
                            direccionReceptor = item.direccionReceptor,
                            CodMotivoDescuento = Convert.ToInt32(item.CodMotivoDescuento),
                            PorcDescuento = Convert.ToDecimal(item.PorcDescuento),
                            MontoDescuentoGlobal = Convert.ToDecimal(item.MontoDescuentoGlobal),
                            BaseMontoDescuento = Convert.ToDecimal(item.BaseMontoDescuento),
                            MontoTotalImpuesto = Convert.ToDecimal(item.MontoTotalImpuesto),
                            MontoGravadasIGV = Convert.ToDecimal(item.MontoGravadasIGV),
                            CodigoTributo = Convert.ToInt32(item.CodigoTributo),
                            MontoExonerado = Convert.ToDecimal(item.MontoExonerado),
                            MontoInafecto = Convert.ToDecimal(item.MontoInafecto),
                            MontoGratuitoImpuesto = Convert.ToDecimal(item.MontoGratuitoImpuesto),
                            MontoBaseGratuito = Convert.ToDecimal(item.MontoBaseGratuito),
                            totalIgv = Convert.ToDecimal(item.totalIgv),
                            MontoGravadosISC = Convert.ToDecimal(item.MontoGravadosISC),
                            totalIsc = Convert.ToDecimal(item.totalIsc),
                            MontoGravadosOtros = Convert.ToDecimal(item.MontoGravadosOtros),
                            totalOtrosTributos = Convert.ToDecimal(item.totalOtrosTributos),
                            TotalValorVenta = Convert.ToDecimal(item.TotalValorVenta),
                            TotalPrecioVenta = Convert.ToDecimal(item.TotalPrecioVenta),
                            MontoDescuento = Convert.ToDecimal(item.MontoDescuento),
                            MontoTotalCargo = Convert.ToDecimal(item.MontoTotalCargo),
                            MontoTotalAnticipo = Convert.ToDecimal(item.MontoTotalAnticipo),
                            ImporteTotalVenta = Convert.ToDecimal(item.ImporteTotalVenta),
                            doc_icod_documento = Convert.ToInt32(item.doc_icod_documento),
                            EstadoFacturacion = Convert.ToInt32(item.EstadoFacturacion),
                            CodigoSunat = (item.CodigoSunat),
                            EstadoSunat = item.EstadoSunat,
                            UsuarioOSE = item.UsuarioOSE,
                            FormaPago = item.FormaPago,
                            NroTicketCdr = item.NroTicketCdr,
                            TramaZipCdr = item.TramaZipCdr
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                index = index;
                throw ex;
            }
            return lista;
        }


        public List<EFacturaVentaElectronica> listarfacturaVentaElectronicaXid(int codigo, string tipoDocumento)
        {
            DateTime dateOut;
            List<EFacturaVentaElectronica> lista = new List<EFacturaVentaElectronica>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FACTURA_VENTA_ELECTRONICA_LISTAR_X_ID(codigo, tipoDocumento);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaVentaElectronica()
                        {
                            IdCabecera = item.IdCabecera,
                            idDocumento = item.idDocumento.Trim(),
                            fechaEmision = DateTime.ParseExact(item.fechaEmision.Length == 9 ? "0" + item.fechaEmision.Trim() : item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            horaEmision = item.horaEmision.Trim(),
                            FEmisionPresentacion = DateTime.ParseExact(item.fechaEmision.Length == 9 ? "0" + item.fechaEmision.Trim() : item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            fechaVencimiento = DateTime.TryParseExact(item.fehaVencimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOut) == true ? DateTime.ParseExact(item.fehaVencimiento.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy") : "",
                            tipoDocumento = item.tipoDocumento.Trim(),
                            StrTipoDoc = item.StrTipoDoc,
                            moneda = item.moneda.Trim(),
                            CodMotivoNota = item.CodMotivoNota,
                            DescripMotivoNota = item.DescripMotivoNota,
                            NroDocqModifica = item.NroDocqModifica,
                            TipoDocqModifica = item.TipoDocqModifica,
                            cantidadItems = Convert.ToInt32(item.cantidadItems),
                            nombreComercialEmisor = item.nombreComercialEmisor.Trim(),
                            nombreLegalEmisor = item.nombreLegalEmisor.Trim(),
                            tipoDocumentoEmisor = item.tipoDocumentoEmisor.Trim(),
                            nroDocumentoEmisior = item.nroDocumentoEmisior.Trim(),
                            CodLocalEmisor = Convert.ToInt32(item.CodLocalEmisor),
                            nroDocumentoReceptor = item.nroDocumentoReceptor.Trim(),
                            tipoDocumentoReceptor = item.tipoDocumentoReceptor.Trim(),
                            nombreLegalReceptor = item.nombreLegalReceptor.Trim(),
                            direccionReceptor = item.direccionReceptor,
                            CodMotivoDescuento = Convert.ToInt32(item.CodMotivoDescuento),
                            PorcDescuento = Convert.ToDecimal(item.PorcDescuento),
                            MontoDescuentoGlobal = Convert.ToDecimal(item.MontoDescuentoGlobal),
                            BaseMontoDescuento = Convert.ToDecimal(item.BaseMontoDescuento),
                            MontoTotalImpuesto = Convert.ToDecimal(item.MontoTotalImpuesto),
                            MontoGravadasIGV = Convert.ToDecimal(item.MontoGravadasIGV),
                            CodigoTributo = Convert.ToInt32(item.CodigoTributo),
                            MontoExonerado = Convert.ToDecimal(item.MontoExonerado),
                            MontoInafecto = Convert.ToDecimal(item.MontoInafecto),
                            MontoGratuitoImpuesto = Convert.ToDecimal(item.MontoGratuitoImpuesto),
                            MontoBaseGratuito = Convert.ToDecimal(item.MontoBaseGratuito),
                            totalIgv = Convert.ToDecimal(item.totalIgv),
                            MontoGravadosISC = Convert.ToDecimal(item.MontoGravadosISC),
                            totalIsc = Convert.ToDecimal(item.totalIsc),
                            MontoGravadosOtros = Convert.ToDecimal(item.MontoGravadosOtros),
                            totalOtrosTributos = Convert.ToDecimal(item.totalOtrosTributos),
                            TotalValorVenta = Convert.ToDecimal(item.TotalValorVenta),
                            TotalPrecioVenta = Convert.ToDecimal(item.TotalPrecioVenta),
                            MontoDescuento = Convert.ToDecimal(item.MontoDescuento),
                            MontoTotalCargo = Convert.ToDecimal(item.MontoTotalCargo),
                            MontoTotalAnticipo = Convert.ToDecimal(item.MontoTotalAnticipo),
                            ImporteTotalVenta = Convert.ToDecimal(item.ImporteTotalVenta),
                            doc_icod_documento = Convert.ToInt32(item.doc_icod_documento),
                            EstadoFacturacion = Convert.ToInt32(item.EstadoFacturacion),
                            CodigoSunat = (item.CodigoSunat),
                            EstadoSunat = item.EstadoSunat,
                            UsuarioOSE = item.UsuarioOSE,
                            FormaPago = item.FormaPago,
                            NroTicketCdr = item.NroTicketCdr,
                            TramaZipCdr = item.TramaZipCdr
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


        public EFacturaVentaElectronica obtenerEstadofacturacion(int doc_icod_documento, string tipoDocumento)
        {
            int? EstadoFacturacion = 0;
            int? IdCabecera = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_OBTENER_ESTADO_FACTURACION(doc_icod_documento, tipoDocumento, ref EstadoFacturacion, ref IdCabecera);

                    EFacturaVentaElectronica obj = new EFacturaVentaElectronica();
                    obj.EstadoFacturacion = Convert.ToInt32(EstadoFacturacion);
                    obj.IdCabecera = Convert.ToInt32(IdCabecera);

                    return obj;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }




        public List<EFacturaVentaElectronica> listarfacturaVentaElectronica()
        {
            DateTime dateOut;
            int indice = 0;
            List<EFacturaVentaElectronica> lista = new List<EFacturaVentaElectronica>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FACTURA_VENTA_ELECTRONICA_LISTAR();
                    foreach (var item in query)
                    {
                        indice++;
                        lista.Add(new EFacturaVentaElectronica()
                        {
                            IdCabecera = item.IdCabecera,
                            idDocumento = item.idDocumento.Trim(),
                            fechaEmision = DateTime.ParseExact(item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            horaEmision = item.horaEmision.Trim(),
                            FEmisionPresentacion = DateTime.ParseExact(item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            fechaVencimiento = DateTime.TryParseExact(item.fehaVencimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOut) == true ? DateTime.ParseExact(item.fehaVencimiento.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy") : "",
                            tipoDocumento = item.tipoDocumento.Trim(),
                            StrTipoDoc = item.StrTipoDoc,
                            moneda = item.moneda.Trim(),
                            CodMotivoNota = item.CodMotivoNota,
                            DescripMotivoNota = item.DescripMotivoNota,
                            NroDocqModifica = item.NroDocqModifica,
                            TipoDocqModifica = item.TipoDocqModifica,
                            cantidadItems = Convert.ToInt32(item.cantidadItems),
                            nombreComercialEmisor = item.nombreComercialEmisor.Trim(),
                            nombreLegalEmisor = item.nombreLegalEmisor.Trim(),
                            tipoDocumentoEmisor = item.tipoDocumentoEmisor.Trim(),
                            nroDocumentoEmisior = item.nroDocumentoEmisior.Trim(),
                            CodLocalEmisor = Convert.ToInt32(item.CodLocalEmisor),
                            nroDocumentoReceptor = item.nroDocumentoReceptor.Trim(),
                            tipoDocumentoReceptor = item.tipoDocumentoReceptor.Trim(),
                            nombreLegalReceptor = item.nombreLegalReceptor.Trim(),
                            direccionReceptor = item.direccionReceptor,
                            CodMotivoDescuento = Convert.ToInt32(item.CodMotivoDescuento),
                            PorcDescuento = Convert.ToDecimal(item.PorcDescuento),
                            MontoDescuentoGlobal = Convert.ToDecimal(item.MontoDescuentoGlobal),
                            BaseMontoDescuento = Convert.ToDecimal(item.BaseMontoDescuento),
                            MontoTotalImpuesto = Convert.ToDecimal(item.MontoTotalImpuesto),
                            MontoGravadasIGV = Convert.ToDecimal(item.MontoGravadasIGV),
                            CodigoTributo = Convert.ToInt32(item.CodigoTributo),
                            MontoExonerado = Convert.ToDecimal(item.MontoExonerado),
                            MontoInafecto = Convert.ToDecimal(item.MontoInafecto),
                            MontoGratuitoImpuesto = Convert.ToDecimal(item.MontoGratuitoImpuesto),
                            MontoBaseGratuito = Convert.ToDecimal(item.MontoBaseGratuito),
                            totalIgv = Convert.ToDecimal(item.totalIgv),
                            MontoGravadosISC = Convert.ToDecimal(item.MontoGravadosISC),
                            totalIsc = Convert.ToDecimal(item.totalIsc),
                            MontoGravadosOtros = Convert.ToDecimal(item.MontoGravadosOtros),
                            totalOtrosTributos = Convert.ToDecimal(item.totalOtrosTributos),
                            TotalValorVenta = Convert.ToDecimal(item.TotalValorVenta),
                            TotalPrecioVenta = Convert.ToDecimal(item.TotalPrecioVenta),
                            MontoDescuento = Convert.ToDecimal(item.MontoDescuento),
                            MontoTotalCargo = Convert.ToDecimal(item.MontoTotalCargo),
                            MontoTotalAnticipo = Convert.ToDecimal(item.MontoTotalAnticipo),
                            ImporteTotalVenta = Convert.ToDecimal(item.ImporteTotalVenta),
                            doc_icod_documento = Convert.ToInt32(item.doc_icod_documento),
                            EstadoFacturacion = Convert.ToInt32(item.EstadoFacturacion),
                            CodigoSunat = (item.CodigoSunat),
                            EstadoSunat = item.EstadoSunat,
                            UsuarioOSE = item.UsuarioOSE,
                            FormaPago = item.FormaPago,
                            MontoTotalPago = item.MontoTotalPago,
                            MontoCuota = item.MontoCuota,
                            FechaPago = item.FechaPago
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                indice = indice;
                throw ex;
            }
            return lista;
        }
        public int insertarfacturaVentaElectronica(EFacturaCab oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_INSERTAR(
                            ref intIcod,
                            oBe.idDocumento,
                            oBe.fechaEmision.ToString(),
                            oBe.horaEmision,
                            oBe.fechaVencimiento.ToString(),
                            oBe.tipoDocumento,
                            oBe.moneda,
                            oBe.CodMotivoNota,
                            oBe.DescripMotivoNota,
                            oBe.NroDocqModifica,
                            oBe.TipoDocqModifica,
                            oBe.cantidadItems,
                            oBe.nombreComercialEmisor,
                            oBe.nombreLegalEmisor,
                            oBe.tipoDocumentoEmisor,
                            oBe.nroDocumentoEmisior,
                            oBe.CodLocalEmisor,
                            oBe.nroDocumentoReceptor,
                            oBe.tipoDocumentoReceptor,
                            oBe.nombreLegalReceptor,
                            oBe.direccionReceptor,
                            oBe.CodMotivoDescuento,
                            Convert.ToDecimal(oBe.PorcDescuento),
                            Convert.ToDecimal(oBe.MontoDescuentoGlobal),
                            Convert.ToDecimal(oBe.BaseMontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalImpuesto),
                            Convert.ToDecimal(oBe.MontoGravadasIGV),
                            oBe.CodigoTributo,
                            Convert.ToDecimal(oBe.MontoExonerado),
                            Convert.ToDecimal(oBe.MontoInafecto),
                            Convert.ToDecimal(oBe.MontoGratuitoImpuesto),
                            Convert.ToDecimal(oBe.MontoBaseGratuito),
                            Convert.ToDecimal(oBe.totalIgv),
                            Convert.ToDecimal(oBe.MontoGravadosISC),
                            Convert.ToDecimal(oBe.totalIsc),
                            Convert.ToDecimal(oBe.MontoGravadosOtros),
                            Convert.ToDecimal(oBe.totalOtrosTributos),
                            Convert.ToDecimal(oBe.TotalValorVenta),
                            Convert.ToDecimal(oBe.TotalPrecioVenta),
                            Convert.ToDecimal(oBe.MontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalCargo),
                            Convert.ToDecimal(oBe.MontoTotalAnticipo),
                            Convert.ToDecimal(oBe.ImporteTotalVenta),
                            oBe.doc_icod_documento,
                            oBe.EstadoFacturacion,
                            oBe.CodigoSunat,
                            oBe.UsuarioOSE,
                            oBe.FormaPagoS,
                            oBe.MontoTotalPago,
                            oBe.MontoCuota,
                            oBe.FechaPago
                            );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int modificarfacturaVentaElectronica(EFacturaCab oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_MODIFICAR(
                            oBe.IdCabecera,
                            oBe.idDocumento,
                            oBe.fechaEmision.ToString(),
                            oBe.horaEmision,
                            oBe.fechaVencimiento.ToString(),
                            oBe.tipoDocumento,
                            oBe.moneda,
                            oBe.CodMotivoNota,
                            oBe.DescripMotivoNota,
                            oBe.NroDocqModifica,
                            oBe.TipoDocqModifica,
                            oBe.cantidadItems,
                            oBe.nombreComercialEmisor,
                            oBe.nombreLegalEmisor,
                            oBe.tipoDocumentoEmisor,
                            oBe.nroDocumentoEmisior,
                            oBe.CodLocalEmisor,
                            oBe.nroDocumentoReceptor,
                            oBe.tipoDocumentoReceptor,
                            oBe.nombreLegalReceptor,
                            oBe.direccionReceptor,
                            oBe.CodMotivoDescuento,
                            Convert.ToDecimal(oBe.PorcDescuento),
                            Convert.ToDecimal(oBe.MontoDescuentoGlobal),
                            Convert.ToDecimal(oBe.BaseMontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalImpuesto),
                            Convert.ToDecimal(oBe.MontoGravadasIGV),
                            oBe.CodigoTributo,
                            Convert.ToDecimal(oBe.MontoExonerado),
                            Convert.ToDecimal(oBe.MontoInafecto),
                            Convert.ToDecimal(oBe.MontoGratuitoImpuesto),
                            Convert.ToDecimal(oBe.MontoBaseGratuito),
                            Convert.ToDecimal(oBe.totalIgv),
                            Convert.ToDecimal(oBe.MontoGravadosISC),
                            Convert.ToDecimal(oBe.totalIsc),
                            Convert.ToDecimal(oBe.MontoGravadosOtros),
                            Convert.ToDecimal(oBe.totalOtrosTributos),
                            Convert.ToDecimal(oBe.TotalValorVenta),
                            Convert.ToDecimal(oBe.TotalPrecioVenta),
                            Convert.ToDecimal(oBe.MontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalCargo),
                            Convert.ToDecimal(oBe.MontoTotalAnticipo),
                            Convert.ToDecimal(oBe.ImporteTotalVenta),
                            oBe.doc_icod_documento,
                            oBe.EstadoFacturacion,
                            oBe.CodigoSunat,
                            oBe.UsuarioOSE,
                            oBe.FormaPagoS,
                            oBe.MontoTotalPago,
                            oBe.MontoCuota,
                            oBe.FechaPago
                            );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int insertarboletaVentaElectronica(EBoletaCab oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_INSERTAR(
                            ref intIcod,
                            oBe.idDocumento,
                            oBe.fechaEmision.ToString(),
                            oBe.horaEmision,
                            oBe.fechaVencimiento.ToString(),
                            oBe.tipoDocumento,
                            oBe.moneda,
                            oBe.CodMotivoNota,
                            oBe.DescripMotivoNota,
                            oBe.NroDocqModifica,
                            oBe.TipoDocqModifica,
                            oBe.cantidadItems,
                            oBe.nombreComercialEmisor,
                            oBe.nombreLegalEmisor,
                            oBe.tipoDocumentoEmisor,
                            oBe.nroDocumentoEmisior,
                            oBe.CodLocalEmisor,
                            oBe.nroDocumentoReceptor,
                            oBe.tipoDocumentoReceptor,
                            oBe.nombreLegalReceptor,
                            oBe.direccionReceptor,
                            oBe.CodMotivoDescuento,
                            Convert.ToDecimal(oBe.PorcDescuento),
                            Convert.ToDecimal(oBe.MontoDescuentoGlobal),
                            Convert.ToDecimal(oBe.BaseMontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalImpuesto),
                            Convert.ToDecimal(oBe.MontoGravadasIGV),
                            oBe.CodigoTributo,
                            Convert.ToDecimal(oBe.MontoExonerado),
                            Convert.ToDecimal(oBe.MontoInafecto),
                            Convert.ToDecimal(oBe.MontoGratuitoImpuesto),
                            Convert.ToDecimal(oBe.MontoBaseGratuito),
                            Convert.ToDecimal(oBe.totalIgv),
                            Convert.ToDecimal(oBe.MontoGravadosISC),
                            Convert.ToDecimal(oBe.totalIsc),
                            Convert.ToDecimal(oBe.MontoGravadosOtros),
                            Convert.ToDecimal(oBe.totalOtrosTributos),
                            Convert.ToDecimal(oBe.TotalValorVenta),
                            Convert.ToDecimal(oBe.TotalPrecioVenta),
                            Convert.ToDecimal(oBe.MontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalCargo),
                            Convert.ToDecimal(oBe.MontoTotalAnticipo),
                            Convert.ToDecimal(oBe.ImporteTotalVenta),
                            oBe.doc_icod_documento,
                            oBe.EstadoFacturacion,
                            oBe.CodigoSunat,
                            oBe.UsuarioOSE,
                            "",
                            0,
                            0,
                            null

                            );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int modificarboletaVentaElectronica(EBoletaCab oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_MODIFICAR(
                            oBe.IdCabecera,
                            oBe.idDocumento,
                            oBe.fechaEmision.ToString(),
                            oBe.horaEmision,
                            oBe.fechaVencimiento.ToString(),
                            oBe.tipoDocumento,
                            oBe.moneda,
                            oBe.CodMotivoNota,
                            oBe.DescripMotivoNota,
                            oBe.NroDocqModifica,
                            oBe.TipoDocqModifica,
                            oBe.cantidadItems,
                            oBe.nombreComercialEmisor,
                            oBe.nombreLegalEmisor,
                            oBe.tipoDocumentoEmisor,
                            oBe.nroDocumentoEmisior,
                            oBe.CodLocalEmisor,
                            oBe.nroDocumentoReceptor,
                            oBe.tipoDocumentoReceptor,
                            oBe.nombreLegalReceptor,
                            oBe.direccionReceptor,
                            oBe.CodMotivoDescuento,
                            Convert.ToDecimal(oBe.PorcDescuento),
                            Convert.ToDecimal(oBe.MontoDescuentoGlobal),
                            Convert.ToDecimal(oBe.BaseMontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalImpuesto),
                            Convert.ToDecimal(oBe.MontoGravadasIGV),
                            oBe.CodigoTributo,
                            Convert.ToDecimal(oBe.MontoExonerado),
                            Convert.ToDecimal(oBe.MontoInafecto),
                            Convert.ToDecimal(oBe.MontoGratuitoImpuesto),
                            Convert.ToDecimal(oBe.MontoBaseGratuito),
                            Convert.ToDecimal(oBe.totalIgv),
                            Convert.ToDecimal(oBe.MontoGravadosISC),
                            Convert.ToDecimal(oBe.totalIsc),
                            Convert.ToDecimal(oBe.MontoGravadosOtros),
                            Convert.ToDecimal(oBe.totalOtrosTributos),
                            Convert.ToDecimal(oBe.TotalValorVenta),
                            Convert.ToDecimal(oBe.TotalPrecioVenta),
                            Convert.ToDecimal(oBe.MontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalCargo),
                            Convert.ToDecimal(oBe.MontoTotalAnticipo),
                            Convert.ToDecimal(oBe.ImporteTotalVenta),
                            oBe.doc_icod_documento,
                            oBe.EstadoFacturacion,
                            oBe.CodigoSunat,
                            oBe.UsuarioOSE,
                            "",
                            0,
                            0,
                            null
                            );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int insertarNotaCreditoVentaElectronica(ENotaCredito oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_INSERTAR(
                       ref intIcod,
                            oBe.idDocumento,
                            oBe.fechaEmision.ToString(),
                            oBe.horaEmision,
                            oBe.fechaVencimiento.ToString(),
                            oBe.tipoDocumento,
                            oBe.moneda,
                            oBe.CodMotivoNota,
                            oBe.DescripMotivoNota,
                            oBe.NroDocqModifica,
                            oBe.TipoDocqModifica,
                            oBe.cantidadItems,
                            oBe.nombreComercialEmisor,
                            oBe.nombreLegalEmisor,
                            oBe.tipoDocumentoEmisor,
                            oBe.nroDocumentoEmisior,
                            oBe.CodLocalEmisor,
                            oBe.nroDocumentoReceptor,
                            oBe.tipoDocumentoReceptor,
                            oBe.nombreLegalReceptor,
                            oBe.direccionReceptor,
                            oBe.CodMotivoDescuento,
                            Convert.ToDecimal(oBe.PorcDescuento),
                            Convert.ToDecimal(oBe.MontoDescuentoGlobal),
                            Convert.ToDecimal(oBe.BaseMontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalImpuesto),
                            Convert.ToDecimal(oBe.MontoGravadasIGV),
                            oBe.CodigoTributo,
                            Convert.ToDecimal(oBe.MontoExonerado),
                            Convert.ToDecimal(oBe.MontoInafecto),
                            Convert.ToDecimal(oBe.MontoGratuitoImpuesto),
                            Convert.ToDecimal(oBe.MontoBaseGratuito),
                            Convert.ToDecimal(oBe.totalIgv),
                            Convert.ToDecimal(oBe.MontoGravadosISC),
                            Convert.ToDecimal(oBe.totalIsc),
                            Convert.ToDecimal(oBe.MontoGravadosOtros),
                            Convert.ToDecimal(oBe.totalOtrosTributos),
                            Convert.ToDecimal(oBe.TotalValorVenta),
                            Convert.ToDecimal(oBe.TotalPrecioVenta),
                            Convert.ToDecimal(oBe.MontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalCargo),
                            Convert.ToDecimal(oBe.MontoTotalAnticipo),
                            Convert.ToDecimal(oBe.ImporteTotalVenta),
                            oBe.doc_icod_documento,
                            Convert.ToInt32(oBe.EstadoFacturacion),
                            oBe.CodigoSunat,
                            oBe.UsuarioOSE,
                            "",
                            0,
                            0,
                            null
                       );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int modificarNotaCreditoVentaElectronica(ENotaCredito oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_MODIFICAR(
                            oBe.IdCabecera,
                            oBe.idDocumento,
                            oBe.fechaEmision.ToString(),
                            oBe.horaEmision,
                            oBe.fechaVencimiento.ToString(),
                            oBe.tipoDocumento,
                            oBe.moneda,
                            oBe.CodMotivoNota,
                            oBe.DescripMotivoNota,
                            oBe.NroDocqModifica,
                            oBe.TipoDocqModifica,
                            oBe.cantidadItems,
                            oBe.nombreComercialEmisor,
                            oBe.nombreLegalEmisor,
                            oBe.tipoDocumentoEmisor,
                            oBe.nroDocumentoEmisior,
                            oBe.CodLocalEmisor,
                            oBe.nroDocumentoReceptor,
                            oBe.tipoDocumentoReceptor,
                            oBe.nombreLegalReceptor,
                            oBe.direccionReceptor,
                            oBe.CodMotivoDescuento,
                            Convert.ToDecimal(oBe.PorcDescuento),
                            Convert.ToDecimal(oBe.MontoDescuentoGlobal),
                            Convert.ToDecimal(oBe.BaseMontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalImpuesto),
                            Convert.ToDecimal(oBe.MontoGravadasIGV),
                            oBe.CodigoTributo,
                            Convert.ToDecimal(oBe.MontoExonerado),
                            Convert.ToDecimal(oBe.MontoInafecto),
                            Convert.ToDecimal(oBe.MontoGratuitoImpuesto),
                            Convert.ToDecimal(oBe.MontoBaseGratuito),
                            Convert.ToDecimal(oBe.totalIgv),
                            Convert.ToDecimal(oBe.MontoGravadosISC),
                            Convert.ToDecimal(oBe.totalIsc),
                            Convert.ToDecimal(oBe.MontoGravadosOtros),
                            Convert.ToDecimal(oBe.totalOtrosTributos),
                            Convert.ToDecimal(oBe.TotalValorVenta),
                            Convert.ToDecimal(oBe.TotalPrecioVenta),
                            Convert.ToDecimal(oBe.MontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalCargo),
                            Convert.ToDecimal(oBe.MontoTotalAnticipo),
                            Convert.ToDecimal(oBe.ImporteTotalVenta),
                            oBe.doc_icod_documento,
                            Convert.ToInt32(oBe.EstadoFacturacion),
                            oBe.CodigoSunat,
                            oBe.UsuarioOSE,
                            "",
                            0,
                            0,
                            null
                       );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int insertarNotaDebitoVentaElectronica(ENotaDebito oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_INSERTAR(
                       ref intIcod,
                            oBe.idDocumento,
                            oBe.fechaEmision.ToString(),
                            oBe.horaEmision,
                            oBe.fechaVencimiento.ToString(),
                            oBe.tipoDocumento,
                            oBe.moneda,
                            oBe.CodMotivoNota,
                            oBe.DescripMotivoNota,
                            oBe.NroDocqModifica,
                            oBe.TipoDocqModifica,
                            oBe.cantidadItems,
                            oBe.nombreComercialEmisor,
                            oBe.nombreLegalEmisor,
                            oBe.tipoDocumentoEmisor,
                            oBe.nroDocumentoEmisior,
                            oBe.CodLocalEmisor,
                            oBe.nroDocumentoReceptor,
                            oBe.tipoDocumentoReceptor,
                            oBe.nombreLegalReceptor,
                            oBe.direccionReceptor,
                            oBe.CodMotivoDescuento,
                            Convert.ToDecimal(oBe.PorcDescuento),
                            Convert.ToDecimal(oBe.MontoDescuentoGlobal),
                            Convert.ToDecimal(oBe.BaseMontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalImpuesto),
                            Convert.ToDecimal(oBe.MontoGravadasIGV),
                            oBe.CodigoTributo,
                            Convert.ToDecimal(oBe.MontoExonerado),
                            Convert.ToDecimal(oBe.MontoInafecto),
                            Convert.ToDecimal(oBe.MontoGratuitoImpuesto),
                            Convert.ToDecimal(oBe.MontoBaseGratuito),
                            Convert.ToDecimal(oBe.totalIgv),
                            Convert.ToDecimal(oBe.MontoGravadosISC),
                            Convert.ToDecimal(oBe.totalIsc),
                            Convert.ToDecimal(oBe.MontoGravadosOtros),
                            Convert.ToDecimal(oBe.totalOtrosTributos),
                            Convert.ToDecimal(oBe.TotalValorVenta),
                            Convert.ToDecimal(oBe.TotalPrecioVenta),
                            Convert.ToDecimal(oBe.MontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalCargo),
                            Convert.ToDecimal(oBe.MontoTotalAnticipo),
                            Convert.ToDecimal(oBe.ImporteTotalVenta),
                            oBe.doc_icod_documento,
                            Convert.ToInt32(oBe.EstadoFacturacion),
                            oBe.CodigoSunat,
                            oBe.UsuarioOSE,
                            "",
                            0,
                            0,
                            null
                       );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int modificarNotaDebitoVentaElectronica(ENotaDebito oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_MODIFICAR(
                            oBe.IdCabecera,
                            oBe.idDocumento,
                            oBe.fechaEmision.ToString(),
                            oBe.horaEmision,
                            oBe.fechaVencimiento.ToString(),
                            oBe.tipoDocumento,
                            oBe.moneda,
                            oBe.CodMotivoNota,
                            oBe.DescripMotivoNota,
                            oBe.NroDocqModifica,
                            oBe.TipoDocqModifica,
                            oBe.cantidadItems,
                            oBe.nombreComercialEmisor,
                            oBe.nombreLegalEmisor,
                            oBe.tipoDocumentoEmisor,
                            oBe.nroDocumentoEmisior,
                            oBe.CodLocalEmisor,
                            oBe.nroDocumentoReceptor,
                            oBe.tipoDocumentoReceptor,
                            oBe.nombreLegalReceptor,
                            oBe.direccionReceptor,
                            oBe.CodMotivoDescuento,
                            Convert.ToDecimal(oBe.PorcDescuento),
                            Convert.ToDecimal(oBe.MontoDescuentoGlobal),
                            Convert.ToDecimal(oBe.BaseMontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalImpuesto),
                            Convert.ToDecimal(oBe.MontoGravadasIGV),
                            oBe.CodigoTributo,
                            Convert.ToDecimal(oBe.MontoExonerado),
                            Convert.ToDecimal(oBe.MontoInafecto),
                            Convert.ToDecimal(oBe.MontoGratuitoImpuesto),
                            Convert.ToDecimal(oBe.MontoBaseGratuito),
                            Convert.ToDecimal(oBe.totalIgv),
                            Convert.ToDecimal(oBe.MontoGravadosISC),
                            Convert.ToDecimal(oBe.totalIsc),
                            Convert.ToDecimal(oBe.MontoGravadosOtros),
                            Convert.ToDecimal(oBe.totalOtrosTributos),
                            Convert.ToDecimal(oBe.TotalValorVenta),
                            Convert.ToDecimal(oBe.TotalPrecioVenta),
                            Convert.ToDecimal(oBe.MontoDescuento),
                            Convert.ToDecimal(oBe.MontoTotalCargo),
                            Convert.ToDecimal(oBe.MontoTotalAnticipo),
                            Convert.ToDecimal(oBe.ImporteTotalVenta),
                            oBe.doc_icod_documento,
                            Convert.ToInt32(oBe.EstadoFacturacion),
                            oBe.CodigoSunat,
                            oBe.UsuarioOSE,
                            "",
                            0,
                            0,
                            null
                       );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public int insertarFacturaElectronicaResponse(EFacturaElectronicaResponse obj)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_RESPONSE_INSERTAR(
                         ref intIcod,
                         obj.IdCabezera,
                         obj.ProcesoGenerar,
                         obj.ProcesoFirmar,
                         obj.ProcesoEnviar,
                         obj.NombreArchivo,
                         obj.CodigoRespuesta,
                         obj.MensajeRespuesta,
                         obj.MensajeError,
                         obj.NroTicketCdr,
                         obj.TramaZipCdr,
                         obj.Exito
                        );
                    return Convert.ToInt32(intIcod);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void modificarFacturaElectronicaResponse(int idCabecera)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_RESPONSE_MODIFICAR(
                        idCabecera
                      );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarFacturaElectronicaEstado(int id, int estadoSunat)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_ESTADO(
                        id,
                        estadoSunat
                      );
                }
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_SUNAT_DOCUMENTOS_BAJA_ESTADO(
                        id,
                        estadoSunat
                      );
                }
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_ELIMINAR(icodCabecera);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_MODIFICAR_CORRELATIVO_TXT(
                        icodCabecera,
                        correlativoTXT
                      );
                }
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_DOCUMENTO_BAJA_MODIFICAR_CORRELATIVO_TXT(
                        icodCabecera,
                        correlativoTXT
                      );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /*Resumen Boletas*/
        public List<EFacturaVentaElectronica> listarfacturaVentaElectronicaResumen(DateTime fechaInicio, DateTime fechaEmision)
        {
            DateTime dateOut;
            List<EFacturaVentaElectronica> lista = new List<EFacturaVentaElectronica>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FACTURA_VENTA_ELECTRONICA_LISTAR_RESUMEN(fechaInicio, fechaEmision);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaVentaElectronica()
                        {
                            IdCabecera = item.IdCabecera,
                            idDocumento = item.idDocumento.Trim(),
                            fechaEmision = DateTime.ParseExact(item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            //horaEmision = DateTime.ParseExact(item.horaEmision.Trim(), "HH:mm:ss", CultureInfo.InvariantCulture).ToString("HH:mm:ss"),
                            //horaEmision = Convert.ToDateTime(item.horaEmision),
                            FEmisionPresentacion = DateTime.ParseExact(item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            fechaVencimiento = DateTime.TryParseExact(item.fehaVencimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOut) == true ? DateTime.ParseExact(item.fehaVencimiento.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy") : "",
                            tipoDocumento = item.tipoDocumento.Trim(),
                            moneda = item.moneda.Trim(),
                            cantidadItems = Convert.ToInt32(item.cantidadItems),
                            nombreComercialEmisor = item.nombreComercialEmisor.Trim(),
                            nombreLegalEmisor = item.nombreLegalEmisor.Trim(),
                            tipoDocumentoEmisor = item.tipoDocumentoEmisor.Trim(),
                            nroDocumentoEmisior = item.nroDocumentoEmisior.Trim(),
                            CodLocalEmisor = Convert.ToInt32(item.CodLocalEmisor),
                            nroDocumentoReceptor = item.nroDocumentoReceptor.Trim(),
                            tipoDocumentoReceptor = item.tipoDocumentoReceptor.Trim(),
                            nombreLegalReceptor = item.nombreLegalReceptor.Trim(),
                            CodMotivoDescuento = Convert.ToInt32(item.CodMotivoDescuento),
                            PorcDescuento = Convert.ToDecimal(item.PorcDescuento),
                            MontoDescuentoGlobal = Convert.ToDecimal(item.MontoDescuentoGlobal),
                            BaseMontoDescuento = Convert.ToDecimal(item.BaseMontoDescuento),
                            MontoTotalImpuesto = Convert.ToDecimal(item.MontoTotalImpuesto),
                            MontoGravadasIGV = Convert.ToDecimal(item.MontoGravadasIGV),
                            CodigoTributo = Convert.ToInt32(item.CodigoTributo),
                            MontoExonerado = Convert.ToDecimal(item.MontoExonerado),
                            MontoInafecto = Convert.ToDecimal(item.MontoInafecto),
                            MontoGratuitoImpuesto = Convert.ToDecimal(item.MontoGratuitoImpuesto),
                            MontoBaseGratuito = Convert.ToDecimal(item.MontoBaseGratuito),
                            totalIgv = Convert.ToDecimal(item.totalIgv),
                            //TotalIvap = Convert.ToDecimal(item.TotalIvap),
                            MontoGravadosISC = Convert.ToDecimal(item.MontoGravadosISC),
                            totalIsc = Convert.ToDecimal(item.totalIsc),
                            MontoGravadosOtros = Convert.ToDecimal(item.MontoGravadosOtros),
                            totalOtrosTributos = Convert.ToDecimal(item.totalOtrosTributos),
                            TotalValorVenta = Convert.ToDecimal(item.TotalValorVenta),
                            TotalPrecioVenta = Convert.ToDecimal(item.TotalPrecioVenta),
                            MontoDescuento = Convert.ToDecimal(item.MontoDescuento),
                            MontoTotalCargo = Convert.ToDecimal(item.MontoTotalCargo),
                            MontoTotalAnticipo = Convert.ToDecimal(item.MontoTotalAnticipo),
                            ImporteTotalVenta = Convert.ToDecimal(item.ImporteTotalVenta),
                            doc_icod_documento = Convert.ToInt32(item.doc_icod_documento),
                            EstadoFacturacion = Convert.ToInt32(item.EstadoFacturacion),
                            //CodigoSunat = (item.Cod),
                            //EAnulado = item.EAnulado,
                            EstadoSunat = item.EstadoSunat,
                            Mensaje = item.Mensaje,
                            direccionReceptor = item.direccionReceptor,
                            StrTipoDoc = item.StrTipoDoc,
                            NroDocqModifica = item.NroDocqModifica,
                            TipoDocqModifica = item.TipoDocqModifica
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

        public List<ESunatResumenDocumentosCab> listarSunatResumenDocumentosCab(DateTime fechaInicio)
        {
            DateTime dateOut;
            List<ESunatResumenDocumentosCab> lista = new List<ESunatResumenDocumentosCab>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_SUNAT_RESUMEN_DOCUMENTOS_CAB_LISTAR(fechaInicio);
                    foreach (var item in query)
                    {
                        lista.Add(new ESunatResumenDocumentosCab()
                        {
                            IdCabecera = item.IdCabecera,
                            IdDocumento = item.IdDocumento.ToString(),
                            FechaEmision = DateTime.ParseExact(item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            FEmisionPresentacion = DateTime.ParseExact(item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            FechaGeneracion = DateTime.ParseExact(item.FechaGeneracion.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            NroDocumento = item.NroDocumento,
                            TipoDocumento = item.TipoDocumento,
                            NombreLegal = item.NombreLegal.Trim(),
                            NombreComercial = item.NombreComercial.Trim(),
                            Ubigeo = item.Ubigeo,
                            Direccion = item.Direccion,
                            Urbanizacion = item.Urbanizacion,
                            Departamento = item.Departamento,
                            Provincia = item.Provincia,
                            Distrito = item.Distrito,
                            EstadoSunat = item.EstadoSunat,
                            Fecha = DateTime.ParseExact(item.fechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            FechaEnvio = item.FechaEnvio,
                            NroTicket = item.NroTicket,
                            IdResponse = item.Id
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
        public int insertarSunatResumenDocumentosCab(ESunatResumenDocumentosCab obj)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_SUNAT_RESUMEN_DOCUMENTOS_CAB_INSERTAR(
                         ref intIcod,
                         obj.IdDocumento,
                         obj.FechaEmision,
                         obj.FechaGeneracion,
                         obj.NroDocumento,
                         obj.TipoDocumento,
                         obj.NombreLegal,
                         obj.NombreComercial,
                         obj.Ubigeo,
                         obj.Direccion,
                         obj.Urbanizacion,
                         obj.Departamento,
                         obj.Provincia,
                         obj.Distrito,
                         obj.EstadoResumen
                        );
                    return Convert.ToInt32(intIcod);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int insertarResumenDiariaResponse(EResumenResponse obj)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_RESUEMEN_RESPONSE_INSERTAR(
                         ref intIcod,
                         obj.IdItems,
                         obj.NroTicket,
                         obj.NombreArchivo,
                         obj.Exito,
                         obj.MensajeError,
                         obj.Pila,
                         obj.CodigoRespuesta,
                         obj.ProcesoFirmar,
                         obj.ProcesoEnviar,
                         obj.ProcesoGenerar
                        );
                    return Convert.ToInt32(intIcod);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void actualizarResumenDocumentosResponse(int id, int estadoSunat)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_SUNAT_RESUMEN_DOCUMENTOS_ESTADO(
                        id,
                        estadoSunat
                      );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarSunatResumenDocumento(int icodCabecera)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGEV_SUNAT_RESUMEN_DOCUMENTOS_ELIMINAR(icodCabecera);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public List<ESunatDocumentosBaja> listarSunatDocumentosBajaCab(DateTime fechaInicio)
        {
            List<ESunatDocumentosBaja> lista = new List<ESunatDocumentosBaja>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_SUNAT_DOCUMENTOS_BAJA_LISTAR(fechaInicio);
                    foreach (var item in query)
                    {
                        lista.Add(new ESunatDocumentosBaja()
                        {
                            IdCabecera = item.IdCabecera,
                            Id = Convert.ToInt32(item.Id),
                            FechaEmision = DateTime.ParseExact(item.FechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            horaEmision = item.horaEmision,
                            FEmisionPresentacion = DateTime.ParseExact(item.FechaEmision.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                            TipoDocumento = item.TipoDocumento,
                            StrTipoDoc = item.StrTipoDoc,
                            Serie = item.Serie,
                            Correlativo = item.Correlativo,
                            MotivoBaja = item.MotivoBaja,
                            contador = Convert.ToInt32(item.contador),
                            EstadoFacturacion = Convert.ToInt32(item.EstadoFacturacion),
                            EstadoSunat = item.EstadoSunat,
                            CodigoSunat = item.CodigoSunat,
                            CorrelativoTXT = Convert.ToInt32(item.CorrelativoTXT),
                            UsuarioOSE = item.UsuarioOSE
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
        public int insertarSunatDocumentosBajaCab(ESunatDocumentosBaja obj)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_SUNAT_DOCUMENTOS_BAJA_INSERTAR(
                         ref intIcod,
                         obj.Id,
                         obj.FechaEmision,
                         obj.TipoDocumento,
                         obj.Serie,
                         obj.Correlativo,
                         obj.MotivoBaja,
                         obj.contador,
                         obj.EstadoFacturacion,
                         obj.CodigoSunat,
                         obj.UsuarioOSE
                        );
                    return Convert.ToInt32(intIcod);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        #endregion
        #region Factura Venta Detalle Electronico
        public List<EFacturaVentaDetalleElectronico> listarfacturaVentaElectronicaDetalle(int facvd_icod_fac_venta)
        {
            List<EFacturaVentaDetalleElectronico> lista = new List<EFacturaVentaDetalleElectronico>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_FACTURA_VENTA_ELECTRONICA_DETALLE_LISTAR(facvd_icod_fac_venta);
                    foreach (var item in query)
                    {
                        lista.Add(new EFacturaVentaDetalleElectronico()
                        {
                            IdItems = item.IdItems,
                            IdCabecera = Convert.ToInt32(item.IdCabecera),
                            NumeroOrdenItem = Convert.ToInt32(item.NumeroOrdenItem),
                            cantidad = Convert.ToInt32(item.cantidad),
                            unidadMedida = item.unidadMedida,
                            ValorVentaItem = Convert.ToDecimal(item.ValorVentaItem),
                            CodMotivoDescuentoItem = Convert.ToInt32(item.CodMotivoDescuentoItem),
                            FactorDescuentoItem = Convert.ToDecimal(item.FactorDescuentoItem),
                            DescuentoItem = Convert.ToDecimal(item.DescuentoItem),
                            BaseDescuentotem = Convert.ToDecimal(item.BaseDescuentotem),
                            CodMotivoCargoItem = Convert.ToInt32(item.CodMotivoCargoItem),
                            FactorCargoItem = Convert.ToDecimal(item.FactorCargoItem),
                            MontoCargoItem = Convert.ToDecimal(item.MontoCargoItem),
                            BaseCargoItem = Convert.ToDecimal(item.BaseCargoItem),
                            MontoTotalImpuestosItem = Convert.ToDecimal(item.MontoTotalImpuestosItem),
                            MontoImpuestoIgvItem = Convert.ToDecimal(item.MontoImpuestoIgvItem),
                            MontoAfectoImpuestoIgv = Convert.ToDecimal(item.MontoAfectoImpuestoIgv),
                            PorcentajeIGVItem = Convert.ToDecimal(item.PorcentajeIGVItem),
                            MontoInafectoItem = Convert.ToDecimal(item.MontoInafectoItem),
                            MontoImpuestoISCItem = Convert.ToDecimal(item.MontoImpuestoISCItem),
                            MontoAfectoImpuestoIsc = Convert.ToDecimal(item.MontoAfectoImpuestoIsc),
                            PorcentajeISCtem = Convert.ToDecimal(item.PorcentajeISCtem),
                            descripcion = item.descripcion,
                            codigoItem = item.codigoItem,
                            ObservacionesItem = item.ObservacionesItem,
                            ValorUnitarioItem = Convert.ToDecimal(item.ValorUnitarioItem),
                            PrecioVentaUnitarioItem = Convert.ToDecimal(item.PrecioVentaUnitarioItem),
                            tipoImpuesto = item.tipoImpuesto

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
        public int insertarfacturaVentaElectronicaDetalle(EFacturaDet oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_DETALLE_INSERTAR(
                            ref intIcod,
                                oBe.IdCabecera,
                                oBe.NumeroOrdenItem,
                                oBe.cantidad,
                                oBe.unidadMedida,
                                oBe.ValorVentaItem,
                                oBe.CodMotivoDescuentoItem,
                                oBe.FactorDescuentoItem,
                                oBe.DescuentoItem,
                                oBe.BaseDescuentotem,
                                oBe.CodMotivoCargoItem,
                                oBe.FactorCargoItem,
                                oBe.MontoCargoItem,
                                oBe.BaseCargoItem,
                                oBe.MontoTotalImpuestosItem,
                                oBe.MontoImpuestoIgvItem,
                                oBe.MontoAfectoImpuestoIgv,
                                oBe.PorcentajeIGVItem,
                                oBe.MontoInafectoItem,
                                oBe.MontoImpuestoISCItem,
                                oBe.MontoAfectoImpuestoIsc,
                                oBe.PorcentajeISCtem,
                                oBe.MontoImpuestoIVAPtem,
                                oBe.MontoAfectoImpuestoIVAPItem,
                                oBe.PorcentajeIVAPItem,
                                oBe.descripcion,
                                oBe.codigoItem,
                                oBe.ObservacionesItem,
                                oBe.ValorUnitarioItem,
                                oBe.PrecioVentaUnitarioItem,
                                oBe.tipoImpuesto

                            );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int insertarNotaCreditoSuministrosVentaElectronicaDetalle(ENotaCreditoSuministrosDet oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_DETALLE_INSERTAR(
                         ref intIcod,
                                oBe.IdCabecera,
                                oBe.NumeroOrdenItem,
                                oBe.cantidad,
                                oBe.unidadMedida,
                                oBe.ValorVentaItem,
                                oBe.CodMotivoDescuentoItem,
                                oBe.FactorDescuentoItem,
                                oBe.DescuentoItem,
                                oBe.BaseDescuentotem,
                                oBe.CodMotivoCargoItem,
                                oBe.FactorCargoItem,
                                oBe.MontoCargoItem,
                                oBe.BaseCargoItem,
                                oBe.MontoTotalImpuestosItem,
                                oBe.MontoImpuestoIgvItem,
                                oBe.MontoAfectoImpuestoIgv,
                                oBe.PorcentajeIGVItem,
                                oBe.MontoInafectoItem,
                                oBe.MontoImpuestoISCItem,
                                oBe.MontoAfectoImpuestoIsc,
                                oBe.PorcentajeISCtem,
                                oBe.MontoImpuestoIVAPtem,
                                oBe.MontoAfectoImpuestoIVAPItem,
                                oBe.PorcentajeIVAPItem,
                                oBe.descripcion,
                                oBe.codigoItem,
                                oBe.ObservacionesItem,
                                oBe.ValorUnitarioItem,
                                oBe.PrecioVentaUnitarioItem,
                                oBe.tipoImpuesto
                                         );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int insertarfacturaVentaElectronicaMPDetalle(EFacturaMPDet oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_DETALLE_INSERTAR(
                           ref intIcod,
                                oBe.IdCabecera,
                                oBe.NumeroOrdenItem,
                                oBe.cantidad,
                                oBe.unidadMedida,
                                oBe.ValorVentaItem,
                                oBe.CodMotivoDescuentoItem,
                                oBe.FactorDescuentoItem,
                                oBe.DescuentoItem,
                                oBe.BaseDescuentotem,
                                oBe.CodMotivoCargoItem,
                                oBe.FactorCargoItem,
                                oBe.MontoCargoItem,
                                oBe.BaseCargoItem,
                                oBe.MontoTotalImpuestosItem,
                                oBe.MontoImpuestoIgvItem,
                                oBe.MontoAfectoImpuestoIgv,
                                oBe.PorcentajeIGVItem,
                                oBe.MontoInafectoItem,
                                oBe.MontoImpuestoISCItem,
                                oBe.MontoAfectoImpuestoIsc,
                                oBe.PorcentajeISCtem,
                                oBe.MontoImpuestoIVAPtem,
                                oBe.MontoAfectoImpuestoIVAPItem,
                                oBe.PorcentajeIVAPItem,
                                oBe.descripcion,
                                oBe.codigoItem,
                                oBe.ObservacionesItem,
                                oBe.ValorUnitarioItem,
                                oBe.PrecioVentaUnitarioItem,
                                oBe.tipoImpuesto

                            );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public int insertarboletaVentaElectronicaDetalle(EBoletaDet oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_DETALLE_INSERTAR(
                            ref intIcod,
                                oBe.IdCabecera,
                                oBe.NumeroOrdenItem,
                                oBe.cantidad,
                                oBe.unidadMedida,
                                oBe.ValorVentaItem,
                                oBe.CodMotivoDescuentoItem,
                                oBe.FactorDescuentoItem,
                                oBe.DescuentoItem,
                                oBe.BaseDescuentotem,
                                oBe.CodMotivoCargoItem,
                                oBe.FactorCargoItem,
                                oBe.MontoCargoItem,
                                oBe.BaseCargoItem,
                                oBe.MontoTotalImpuestosItem,
                                oBe.MontoImpuestoIgvItem,
                                oBe.MontoAfectoImpuestoIgv,
                                oBe.PorcentajeIGVItem,
                                oBe.MontoInafectoItem,
                                oBe.MontoImpuestoISCItem,
                                oBe.MontoAfectoImpuestoIsc,
                                oBe.PorcentajeISCtem,
                                oBe.MontoImpuestoIVAPtem,
                                oBe.MontoAfectoImpuestoIVAPItem,
                                oBe.PorcentajeIVAPItem,
                                oBe.descripcion,
                                oBe.codigoItem,
                                oBe.ObservacionesItem,
                                oBe.ValorUnitarioItem,
                                oBe.PrecioVentaUnitarioItem,
                                oBe.tipoImpuesto

                            );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int insertarNotaCreditoVentaElectronicaDetalle(ENotaCreditoDet oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_DETALLE_INSERTAR(
                         ref intIcod,
                                oBe.IdCabecera,
                                oBe.NumeroOrdenItem,
                                oBe.cantidad,
                                oBe.unidadMedida,
                                oBe.ValorVentaItem,
                                oBe.CodMotivoDescuentoItem,
                                oBe.FactorDescuentoItem,
                                oBe.DescuentoItem,
                                oBe.BaseDescuentotem,
                                oBe.CodMotivoCargoItem,
                                oBe.FactorCargoItem,
                                oBe.MontoCargoItem,
                                oBe.BaseCargoItem,
                                oBe.MontoTotalImpuestosItem,
                                oBe.MontoImpuestoIgvItem,
                                oBe.MontoAfectoImpuestoIgv,
                                oBe.PorcentajeIGVItem,
                                oBe.MontoInafectoItem,
                                oBe.MontoImpuestoISCItem,
                                oBe.MontoAfectoImpuestoIsc,
                                oBe.PorcentajeISCtem,
                                oBe.MontoImpuestoIVAPtem,
                                oBe.MontoAfectoImpuestoIVAPItem,
                                oBe.PorcentajeIVAPItem,
                                oBe.descripcion,
                                oBe.codigoItem,
                                oBe.ObservacionesItem,
                                oBe.ValorUnitarioItem,
                                oBe.PrecioVentaUnitarioItem,
                                oBe.tipoImpuesto
                                         );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void CuotasFacturaEliminar(ECuotasFactura obe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_CREDITO_CUOTAS_ELIMINAR(
                        obe.fcc_icod,
                        obe.intUsuario,
                        obe.strPc,
                        DateTime.Now
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void CuotasFacturaModificar(ECuotasFactura obj)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_CREDITO_CUOTAS_MODIFICAR(
                        obj.fcc_icod,
                        obj.favc_icod_factura,
                        obj.doxcc_icod_correlativo,
                        obj.fcc_inro_cuota,
                        obj.fcc_sfecha_pago,
                        obj.fcc_nmonto,
                        obj.fcc_nmonto_pagado,
                        obj.fcc_nsaldo,
                        obj.fcc_iestado,
                        obj.intUsuario,
                        obj.strPc,
                        DateTime.Now
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int insertarNotaDebitoVentaElectronicaDetalle(ENotaDebitoDet oBe)
        {
            try
            {
                int? intIcod = 0;
                //using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                //{
                //    dc.SGEV_FACTURA_VENTA_ELECTRONICA_DETALLE_INSERTAR(
                //          ref intIcod,
                //           oBe.IdCabecera,
                //           oBe.NumeroOrdenItem,
                //           oBe.cantidad,
                //           oBe.unidadMedida,
                //           oBe.ValorVentaItem,
                //           oBe.CodMotivoDescuentoItem,
                //           oBe.FactorDescuentoItem,
                //           oBe.DescuentoItem,
                //           oBe.BaseDescuentotem,
                //           oBe.CodMotivoCargoItem,
                //           oBe.FactorCargoItem,
                //           oBe.MontoCargoItem,
                //           oBe.BaseCargoItem,
                //           oBe.MontoTotalImpuestosItem,
                //           oBe.MontoImpuestoIgvItem,
                //           oBe.MontoAfectoImpuestoIgv,
                //           oBe.PorcentajeIGVItem,
                //           oBe.MontoInafectoItem,
                //           oBe.MontoImpuestoISCItem,
                //           oBe.MontoAfectoImpuestoIsc,
                //           oBe.PorcentajeISCtem,
                //           oBe.MontoImpuestoIVAPtem,
                //           oBe.MontoAfectoImpuestoIVAPItem,
                //           oBe.PorcentajeIVAPItem,
                //           oBe.descripcion,
                //           oBe.codigoItem,
                //           oBe.ObservacionesItem,
                //           oBe.ValorUnitarioItem,
                //           oBe.tipoImpuesto,
                //           oBe.UMedida,
                //           oBe.CodConceptoAsignarItem,
                //           oBe.DescripcionAdicionalItem
                //                         );
                //}
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGEV_FACTURA_VENTA_ELECTRONICA_DETALLE_ELIMINAR(icodCabecera);
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
            List<ESunatResumenDocumentosDet> lista = new List<ESunatResumenDocumentosDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_SUNAT_RESUMEN_DOCUMENTOS_DET_LISTAR(idCabecera);
                    foreach (var item in query)
                    {
                        lista.Add(new ESunatResumenDocumentosDet()
                        {
                            IdItems = item.IdItems,
                            IdCabecera = Convert.ToInt32(item.IdCabecera),
                            Id = Convert.ToInt32(item.Id),
                            TipoDocumento = item.TipoDocumento,
                            IdDocumento = item.IdDocumento,
                            TipoDocumentoReceptor = item.TipoDocumentoReceptor,
                            NroDocumentoReceptor = item.NroDocumentoReceptor,
                            CodigoEstadoItem = Convert.ToInt32(item.CodigoEstadoItem),
                            DocumentoRelacionado = item.DocumentoRelacionado,
                            TipoDocumentoRelacionado = item.TipoDocumentoRelacionado,
                            Moneda = item.Moneda,
                            TotalVenta = Convert.ToDecimal(item.TotalVenta),
                            TotalDescuentos = Convert.ToDecimal(item.TotalDescuentos),
                            TotalIgv = Convert.ToDecimal(item.TotalIgv),
                            TotalIsc = Convert.ToDecimal(item.TotalIsc),
                            TotalIvap = Convert.ToDecimal(item.TotalIvap),
                            TotalOtrosImpuestos = Convert.ToDecimal(item.TotalOtrosImpuestos),
                            Gravadas = Convert.ToDecimal(item.Gravadas),
                            Exoneradas = Convert.ToDecimal(item.Exoneradas),
                            Inafectas = Convert.ToDecimal(item.Inafectas),
                            Exportacion = Convert.ToDecimal(item.Exportacion),
                            Gratuitas = Convert.ToDecimal(item.Gratuitas),
                            doc_icod_documento = Convert.ToInt32(item.doc_icod_documento),
                            strTipoDoc = item.StrTipoDoc
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
        public int insertarSunatResumenDocumentosDet(ESunatResumenDocumentosDet obj)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_SUNAT_RESUMEN_DOCUMENTOS_DET_INSERTAR(
                        ref intIcod,
                        obj.IdCabecera,
                        obj.Id,
                        obj.TipoDocumento,
                        obj.IdDocumento,
                        obj.TipoDocumentoReceptor,
                        obj.NroDocumentoReceptor,
                        obj.CodigoEstadoItem,
                        obj.DocumentoRelacionado,
                        obj.TipoDocumentoRelacionado,
                        obj.Moneda,
                        obj.TotalVenta,
                        obj.TotalDescuentos,
                        obj.TotalIgv,
                        obj.TotalIsc,
                        obj.TotalIvap,
                        obj.TotalOtrosImpuestos,
                        obj.Gravadas,
                        obj.Exoneradas,
                        obj.Inafectas,
                        obj.Exportacion,
                        obj.Gratuitas,
                        obj.doc_icod_documento
                      );
                    return Convert.ToInt32(intIcod);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarSunatResumenDocumentosDetalle(int icodCabecera)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGEV_SUNAT_RESUMEN_DOCUMENTOS_DETALLE_ELIMINAR(icodCabecera);
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
            List<EBoletaCab> lista = new List<EBoletaCab>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_BOLETA_CAB_GET(id_boleta);
                    foreach (var item in query)
                    {
                        lista.Add(new EBoletaCab()
                        {
                            bovc_icod_boleta = item.bovc_icod_boleta,
                            bovc_vnumero_boleta = item.bovc_vnumero_boleta,
                            bovc_sfecha_boleta = item.bovc_sfecha_boleta,
                            bovc_sfecha_vencim_boleta = item.bovc_sfecha_vencim_boleta,
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            cliec_vcod_cliente = item.cliec_vcod_cliente,
                            cliec_vdireccion_cliente = item.cliec_vdireccion_cliente,
                            cliec_cruc = item.cliec_cruc,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            tablc_iid_forma_pago = item.tablc_iid_forma_pago,
                            tablc_iid_situacion = Convert.ToInt32(item.tablc_iid_situacion_documento),
                            bovc_npor_imp_igv = item.bovc_npor_imp_igv,
                            bovc_nmonto_neto = item.bovc_nmonto_neto,
                            bovc_nmonto_imp = item.bovc_nmonto_imp,
                            bovc_nmonto_total = item.bovc_nmonto_total,
                            cliec_vnombre_cliente = item.strDesCliente,
                            strFormaPago = item.strFormaPago,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            strTelefonoCliente = item.strTelefonoCliente,
                            strDistritoCliente = item.strDistritoCliente,
                            bovc_vobservacion = item.bovc_vobservacion,
                            bovc_vnombre_cliente = item.bovc_vnombre_cliente,
                            remic_icod_remision = item.remic_icod_remision,
                            remic_vnumero_remision = item.remic_vnumero_remision,
                            bfavc_bafecto_igv = Convert.ToBoolean(item.bfavc_bafecto_igv)

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
        public List<EBoletaCab> listarBoletaCab(int intEjericio)
        {
            List<EBoletaCab> lista = new List<EBoletaCab>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_BOLETA_CAB_LISTAR(intEjericio);
                    foreach (var item in query)
                    {
                        lista.Add(new EBoletaCab()
                        {
                            bovc_icod_boleta = item.bovc_icod_boleta,
                            bovc_vnumero_boleta = item.bovc_vnumero_boleta,
                            bovc_sfecha_boleta = item.bovc_sfecha_boleta,
                            bovc_sfecha_vencim_boleta = item.bovc_sfecha_vencim_boleta,
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            cliec_vcod_cliente = item.cliec_vcod_cliente,
                            cliec_vdireccion_cliente = item.cliec_vdireccion_cliente,
                            cliec_cruc = item.cliec_cruc,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            tablc_iid_forma_pago = item.tablc_iid_forma_pago,
                            tablc_iid_situacion = Convert.ToInt32(item.tablc_iid_situacion),
                            bovc_npor_imp_igv = item.bovc_npor_imp_igv,
                            bovc_nmonto_neto = item.bovc_nmonto_neto,
                            bovc_noperacion_gravadas = Convert.ToDecimal(item.bovc_noperacion_gravadas),
                            bovc_noperacion_inafectas = Convert.ToDecimal(item.bovc_noperacion_inafectas),
                            bovc_nmonto_imp = item.bovc_nmonto_imp,
                            bovc_nmonto_total = item.bovc_nmonto_total,
                            cliec_vnombre_cliente = item.strDesCliente,
                            strFormaPago = item.strFormaPago,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            strTelefonoCliente = item.strTelefonoCliente,
                            strDistritoCliente = item.strDistritoCliente,
                            bovc_vobservacion = item.bovc_vobservacion,
                            ubicc_icod_ubicacion = Convert.ToInt32(item.ubicc_icod_ubicacion),
                            cliec_nnumero_dias = Convert.ToInt32(item.cliec_nnumero_dias),
                            remic_icod_remision = item.remic_icod_remision,
                            remic_vnumero_remision = item.remic_vnumero_remision,
                            bovc_vnombre_cliente = item.bovc_vnombre_cliente,
                            bfavc_bafecto_igv = Convert.ToBoolean(item.bfavc_bafecto_igv),
                            vendc_icod_vendedor = Convert.ToInt32(item.vendc_icod_vendedor),
                            puvec_icod_punto_venta = Convert.ToInt32(item.puvec_icod_punto_venta),
                            bovc_iid_almacen = Convert.ToInt32(item.bovc_iid_almacen),
                            bovc_viid_almacen = string.Format("{0:00}", item.almac_iid_almacen),
                            bovc_vdescripcion_almacen = item.almac_vdescripcion,
                            NomVendedor = item.NomVendedor,
                            doc_icod_documento = item.bovc_icod_boleta,
                            bovc_itipo_boleta = Convert.ToInt32(item.bovc_itipo_boleta),
                            bovc_icod_pvt = Convert.ToInt32(item.bovc_icod_pvt),
                            DesPVT = item.DesPVT
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
        public int insertarBoleta(EBoletaCab ob)
        {
            int? bovc_icod_boleta = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_BOLETA_CAB_INSERTAR(
                            ref bovc_icod_boleta,
                            ob.bovc_vnumero_boleta,
                            ob.bovc_sfecha_boleta,
                            ob.bovc_sfecha_vencim_boleta,
                            ob.doxcc_icod_correlativo,
                            ob.cliec_icod_cliente,
                            ob.tablc_iid_tipo_moneda,
                            ob.tablc_iid_forma_pago,
                            ob.tablc_iid_situacion,
                            ob.bovc_npor_imp_igv,
                            ob.bovc_nmonto_neto,
                            ob.bovc_noperacion_gravadas,
                            ob.bovc_noperacion_inafectas,
                            ob.bovc_nmonto_imp,
                            ob.bovc_nmonto_total,
                            ob.anio,
                            ob.intUsuario,
                            ob.strPc,
                            ob.bovc_vobservacion,
                            ob.remic_icod_remision,
                            ob.bovc_vnombre_cliente,
                            ob.bovc_vdni,
                            ob.bfavc_bafecto_igv,
                            ob.vendc_icod_vendedor,
                            ob.puvec_icod_punto_venta,
                            ob.bovc_iid_almacen,
                            ob.bovc_itipo_boleta,
                            ob.bovc_icod_pvt
                            );
                }
                return Convert.ToInt32(bovc_icod_boleta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarBoleta(EBoletaCab ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_BOLETA_CAB_MODIFICAR(
                    ob.bovc_icod_boleta,
                    ob.bovc_vnumero_boleta,
                    ob.bovc_sfecha_boleta,
                    ob.bovc_sfecha_vencim_boleta,
                    ob.cliec_icod_cliente,
                    ob.tablc_iid_tipo_moneda,
                    ob.tablc_iid_forma_pago,
                    ob.tablc_iid_tipo_moneda,
                    ob.bovc_npor_imp_igv,
                    ob.bovc_nmonto_neto,
                    ob.bovc_noperacion_gravadas,
                    ob.bovc_noperacion_inafectas,
                    ob.bovc_nmonto_imp,
                    ob.bovc_nmonto_total,
                    ob.intUsuario,
                    ob.strPc,
                    ob.bovc_vobservacion,
                    ob.bovc_vnombre_cliente,
                    ob.bovc_vdni,
                    ob.bfavc_bafecto_igv,
                    ob.vendc_icod_vendedor,
                    ob.puvec_icod_punto_venta,
                    ob.bovc_iid_almacen,
                    ob.bovc_icod_pvt
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarBoleta(EBoletaCab ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_BOLETA_CAB_ELIMINAR(
                            ob.bovc_icod_boleta,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void anularBoleta(EBoletaCab ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_BOLETA_CAB_ANULAR(
                            ob.bovc_icod_boleta,
                            ob.intUsuario,
                            ob.strPc);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_ACTUALIZAR_BOLETA_VENTA(bovc_icod_boleta, bfavc_bafecto_igv);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Boleta Detalle
        public List<EBoletaDet> listarBoletaDetalle(int intFactura)
        {
            List<EBoletaDet> lista = new List<EBoletaDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_BOLETA_DET_LISTAR(intFactura);
                    foreach (var item in query)
                    {
                        lista.Add(new EBoletaDet()
                        {
                            bovd_icod_item_boleta = item.bovd_icod_item_boleta,
                            bovc_icod_boleta = item.bovc_icod_boleta,
                            almac_icod_almacen = item.almac_icod_almacen,
                            bovd_iitem_boleta = Convert.ToInt32(item.bovd_iitem_boleta),
                            tablc_iid_motivo = item.tablc_iid_motivo,
                            bovd_vcodigo_extremo_prod = item.bovd_vcodigo_extremo_prod,
                            bovd_vdescripcion_prod = item.bovd_vdescripcion_prod,
                            bovd_ncantidad_total_producto = item.bovd_ncantidad_total_producto,
                            bovd_icod_serie = Convert.ToInt32(item.bovd_icod_serie),
                            bovd_rango_talla = item.bovd_rango_talla,
                            bovd_talla1 = Convert.ToInt32(item.bovd_talla1),
                            bovd_talla2 = Convert.ToInt32(item.bovd_talla2),
                            bovd_talla3 = Convert.ToInt32(item.bovd_talla3),
                            bovd_talla4 = Convert.ToInt32(item.bovd_talla4),
                            bovd_talla5 = Convert.ToInt32(item.bovd_talla5),
                            bovd_talla6 = Convert.ToInt32(item.bovd_talla6),
                            bovd_talla7 = Convert.ToInt32(item.bovd_talla7),
                            bovd_talla8 = Convert.ToInt32(item.bovd_talla8),
                            bovd_talla9 = Convert.ToInt32(item.bovd_talla9),
                            bovd_talla10 = Convert.ToInt32(item.bovd_talla10),
                            bovd_cant_talla1 = Convert.ToInt32(item.bovd_cant_talla1),
                            bovd_cant_talla2 = Convert.ToInt32(item.bovd_cant_talla2),
                            bovd_cant_talla3 = Convert.ToInt32(item.bovd_cant_talla3),
                            bovd_cant_talla4 = Convert.ToInt32(item.bovd_cant_talla4),
                            bovd_cant_talla5 = Convert.ToInt32(item.bovd_cant_talla5),
                            bovd_cant_talla6 = Convert.ToInt32(item.bovd_cant_talla6),
                            bovd_cant_talla7 = Convert.ToInt32(item.bovd_cant_talla7),
                            bovd_cant_talla8 = Convert.ToInt32(item.bovd_cant_talla8),
                            bovd_cant_talla9 = Convert.ToInt32(item.bovd_cant_talla9),
                            bovd_cant_talla10 = Convert.ToInt32(item.bovd_cant_talla10),
                            bovd_iid_kardex1 = Convert.ToInt32(item.bovd_iid_kardex1),
                            bovd_iid_kardex2 = Convert.ToInt32(item.bovd_iid_kardex2),
                            bovd_iid_kardex3 = Convert.ToInt32(item.bovd_iid_kardex3),
                            bovd_iid_kardex4 = Convert.ToInt32(item.bovd_iid_kardex4),
                            bovd_iid_kardex5 = Convert.ToInt32(item.bovd_iid_kardex5),
                            bovd_iid_kardex6 = Convert.ToInt32(item.bovd_iid_kardex6),
                            bovd_iid_kardex7 = Convert.ToInt32(item.bovd_iid_kardex7),
                            bovd_iid_kardex8 = Convert.ToInt32(item.bovd_iid_kardex8),
                            bovd_iid_kardex9 = Convert.ToInt32(item.bovd_iid_kardex9),
                            bovd_iid_kardex10 = Convert.ToInt32(item.bovd_iid_kardex10),
                            bovd_icod_producto1 = Convert.ToInt32(item.bovd_icod_producto1),
                            bovd_icod_producto2 = Convert.ToInt32(item.bovd_icod_producto2),
                            bovd_icod_producto3 = Convert.ToInt32(item.bovd_icod_producto3),
                            bovd_icod_producto4 = Convert.ToInt32(item.bovd_icod_producto4),
                            bovd_icod_producto5 = Convert.ToInt32(item.bovd_icod_producto5),
                            bovd_icod_producto6 = Convert.ToInt32(item.bovd_icod_producto6),
                            bovd_icod_producto7 = Convert.ToInt32(item.bovd_icod_producto7),
                            bovd_icod_producto8 = Convert.ToInt32(item.bovd_icod_producto8),
                            bovd_icod_producto9 = Convert.ToInt32(item.bovd_icod_producto9),
                            bovd_icod_producto10 = Convert.ToInt32(item.bovd_icod_producto10),
                            bovd_nprecio_unitario_item = item.bovd_nprecio_unitario_item,
                            bovd_nprecio_total_item = item.bovd_nprecio_total_item,
                            prdc_icod_producto = item.prdc_icod_producto,
                            bovd_nmonto_impuesto_item = item.bovd_nmonto_impuesto_item,
                            bovd_nporcentaje_descuento_item = item.bovd_nporcentaje_descuento_item,
                            strDesUM = item.strDesUM,
                            strAlmacen = item.strAlmacen,
                            strMoneda = item.strMoneda,
                            kardc_icod_correlativo = Convert.ToInt32(item.kardc_icod_correlativo),
                            tablc_iid_tipo_venta = Convert.ToInt32(item.tablc_iid_tipo_venta),
                            strTipoVenta = item.StrTipoVenta,
                            strUM = item.strUM,
                            CodigoSUNAT = item.CodigoSUNAT,
                            unidc_icod_unidad_medida = Convert.ToInt32(item.unidc_icod_unidad_medida),
                            flag_afecto_igv = Convert.ToBoolean(item.flag_afecto_igv),
                            bovd_nneto_igv = Convert.ToDecimal(item.bovd_nneto_igv),
                            bovd_nneto_exo = Convert.ToDecimal(item.bovd_nneto_exo),


                            NumeroOrdenItem = item.bovd_iitem_boleta,
                            cantidad = Convert.ToDecimal(item.bovd_ncantidad_total_producto),
                            unidadMedida = item.CodigoSUNAT,
                            ValorVentaItem = Convert.ToDecimal(item.bovd_nprecio_total_item),
                            CodMotivoDescuentoItem = 0,
                            FactorDescuentoItem = 0,
                            DescuentoItem = 0,
                            BaseDescuentotem = 0,
                            CodMotivoCargoItem = 0,
                            FactorCargoItem = 0,
                            MontoCargoItem = 0,
                            BaseCargoItem = 0,
                            MontoTotalImpuestosItem = Convert.ToDecimal(item.bovd_nmonto_impuesto_item),
                            MontoImpuestoIgvItem = Convert.ToDecimal(item.bovd_nmonto_impuesto_item),
                            MontoAfectoImpuestoIgv = Convert.ToDecimal(item.bovd_nmonto_impuesto_item) == 0 ? 0 : Convert.ToDecimal((item.bovd_nneto_igv)),
                            PorcentajeIGVItem = 18,
                            MontoInafectoItem = Convert.ToDecimal(item.bovd_nmonto_impuesto_item) == 0 ? Convert.ToDecimal(item.bovd_nprecio_total_item) : 0,
                            MontoImpuestoISCItem = 0,
                            MontoAfectoImpuestoIsc = 0,
                            PorcentajeISCtem = 0,
                            MontoImpuestoIVAPtem = 0,
                            MontoAfectoImpuestoIVAPItem = 0,
                            PorcentajeIVAPItem = 0,
                            descripcion = item.bovd_vdescripcion_prod,
                            codigoItem = item.bovd_vcodigo_extremo_prod,
                            ObservacionesItem = "",
                            ValorUnitarioItem = Math.Round(Convert.ToDecimal((item.bovd_nneto_igv / item.bovd_ncantidad_total_producto)), 4),
                            tipoImpuesto = item.bovd_nmonto_impuesto_item > 0 ? "10" : "30",
                            UMedida = item.strUM


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
        public void insertarBoletaDetalle(EBoletaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_BOLETA_DET_INSERTAR(
                     ob.bovc_icod_boleta
                    , ob.almac_icod_almacen
                    , ob.bovd_iitem_boleta
                    , ob.tablc_iid_motivo
                    , ob.bovd_vcodigo_extremo_prod
                    , ob.bovd_vdescripcion_prod
                    , ob.bovd_ncantidad_total_producto
                    , ob.bovd_icod_serie
                    , ob.bovd_rango_talla
                    , ob.bovd_talla1
                    , ob.bovd_talla2
                    , ob.bovd_talla3
                    , ob.bovd_talla4
                    , ob.bovd_talla5
                    , ob.bovd_talla6
                    , ob.bovd_talla7
                    , ob.bovd_talla8
                    , ob.bovd_talla9
                    , ob.bovd_talla10
                    , ob.bovd_cant_talla1
                    , ob.bovd_cant_talla2
                    , ob.bovd_cant_talla3
                    , ob.bovd_cant_talla4
                    , ob.bovd_cant_talla5
                    , ob.bovd_cant_talla6
                    , ob.bovd_cant_talla7
                    , ob.bovd_cant_talla8
                    , ob.bovd_cant_talla9
                    , ob.bovd_cant_talla10
                    , ob.bovd_iid_kardex1
                    , ob.bovd_iid_kardex2
                    , ob.bovd_iid_kardex3
                    , ob.bovd_iid_kardex4
                    , ob.bovd_iid_kardex5
                    , ob.bovd_iid_kardex6
                    , ob.bovd_iid_kardex7
                    , ob.bovd_iid_kardex8
                    , ob.bovd_iid_kardex9
                    , ob.bovd_iid_kardex10
                    , ob.bovd_icod_producto1
                    , ob.bovd_icod_producto2
                    , ob.bovd_icod_producto3
                    , ob.bovd_icod_producto4
                    , ob.bovd_icod_producto5
                    , ob.bovd_icod_producto6
                    , ob.bovd_icod_producto7
                    , ob.bovd_icod_producto8
                    , ob.bovd_icod_producto9
                    , ob.bovd_icod_producto10
                    , ob.bovd_nprecio_unitario_item
                    , ob.bovd_nprecio_total_item
                    , ob.prdc_icod_producto
                    , ob.bovd_nmonto_impuesto_item
                    , ob.bovd_nporcentaje_descuento_item
                    , ob.kardc_icod_correlativo
                    , ob.bolvd_vobservaciones
                    , ob.intUsuario
                    , ob.strPc
                    , ob.bovd_flag_estado
                    , ob.tablc_iid_tipo_venta
                    , ob.unidc_icod_unidad_medida
                    , ob.flag_afecto_igv
                    , ob.bovd_nneto_igv
                    , ob.bovd_nneto_exo

                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarBoletaDetalle(EBoletaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_BOLETA_DET_MODIFICAR(
                     ob.bovd_icod_item_boleta
                    , ob.almac_icod_almacen
                    , ob.bovd_iitem_boleta
                    , ob.tablc_iid_motivo
                    , ob.bovd_vcodigo_extremo_prod
                    , ob.bovd_vdescripcion_prod
                    , ob.bovd_ncantidad_total_producto
                    , ob.bovd_icod_serie
                    , ob.bovd_rango_talla
                    , ob.bovd_talla1
                    , ob.bovd_talla2
                    , ob.bovd_talla3
                    , ob.bovd_talla4
                    , ob.bovd_talla5
                    , ob.bovd_talla6
                    , ob.bovd_talla7
                    , ob.bovd_talla8
                    , ob.bovd_talla9
                    , ob.bovd_talla10
                    , ob.bovd_cant_talla1
                    , ob.bovd_cant_talla2
                    , ob.bovd_cant_talla3
                    , ob.bovd_cant_talla4
                    , ob.bovd_cant_talla5
                    , ob.bovd_cant_talla6
                    , ob.bovd_cant_talla7
                    , ob.bovd_cant_talla8
                    , ob.bovd_cant_talla9
                    , ob.bovd_cant_talla10
                    , ob.bovd_iid_kardex1
                    , ob.bovd_iid_kardex2
                    , ob.bovd_iid_kardex3
                    , ob.bovd_iid_kardex4
                    , ob.bovd_iid_kardex5
                    , ob.bovd_iid_kardex6
                    , ob.bovd_iid_kardex7
                    , ob.bovd_iid_kardex8
                    , ob.bovd_iid_kardex9
                    , ob.bovd_iid_kardex10
                    , ob.bovd_icod_producto1
                    , ob.bovd_icod_producto2
                    , ob.bovd_icod_producto3
                    , ob.bovd_icod_producto4
                    , ob.bovd_icod_producto5
                    , ob.bovd_icod_producto6
                    , ob.bovd_icod_producto7
                    , ob.bovd_icod_producto8
                    , ob.bovd_icod_producto9
                    , ob.bovd_icod_producto10
                    , ob.bovd_nprecio_unitario_item
                    , ob.bovd_nprecio_total_item
                    , ob.bovd_nmonto_impuesto_item
                    , ob.bovd_nporcentaje_descuento_item
                    , ob.bolvd_vobservaciones
                    , ob.intUsuario
                    , ob.strPc
                    , ob.tablc_iid_tipo_venta
                    , ob.bovd_nneto_igv
                    , ob.bovd_nneto_exo
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarBoletaDetalle(EBoletaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_BOLETA_DET_ELIMINAR(
                            ob.bovd_icod_item_boleta,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public int verificarDocVentaPlanilla(int intTipoDoc, int intIcodDoc)
        {
            int? intFlag = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_VERIFICAR_DOC_VENTA_PLANILLA(
                            intTipoDoc,
                            intIcodDoc,
                            ref intFlag
                            );
                }
                return Convert.ToInt32(intFlag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EOrdenCompraMercaderia> getOCLCabVerificarNumeroMercaderia(string vnumero, int ANIO)
        {
            List<EOrdenCompraMercaderia> lista = new List<EOrdenCompraMercaderia>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_OCL_CAB_VERIFICAR_NUEMRO_MERCADERIA(vnumero, ANIO);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenCompraMercaderia()
                        {
                            ococ_icod_orden_compra = item.ococ_icod_orden_compra

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
        public List<EOrdenCompra> getOCLCabVerificarNumero(string vnumero, int ANIO)
        {
            List<EOrdenCompra> lista = new List<EOrdenCompra>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_OCL_CAB_VERIFICAR_NUEMRO(vnumero, ANIO);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenCompra()
                        {
                            ococ_icod_orden_compra = item.ococ_icod_orden_compra

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
        public List<EOrdenCompraServicio> getOCSCabVerificarNumero(string vnumero, int ANIO)
        {
            List<EOrdenCompraServicio> lista = new List<EOrdenCompraServicio>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_OCS_CAB_VERIFICAR_NUEMRO(vnumero, ANIO);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenCompraServicio()
                        {
                            ocsc_icod_ocs = item.ocsc_icod_ocs

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
        public List<EOrdenCompraImportacion> getOCICabVerificarNumero(string vnumero, int ANIO)
        {
            List<EOrdenCompraImportacion> lista = new List<EOrdenCompraImportacion>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_OCI_CAB_VERIFICAR_NUEMRO(vnumero, ANIO);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenCompraImportacion()
                        {
                            ocic_icod_oci = item.ocic_icod_oci

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
        #region Venta Directa
        public void actualizarSituacionVentaDirecta(int intVD, int intSituacion)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_DOC_VENTA_DIRECTA_SITUACION(
                        intVD,
                        intSituacion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EVentaDirecta> listarVentaDirecta(int intEjericio)
        {
            List<EVentaDirecta> lista = new List<EVentaDirecta>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_EXTEND_DOC_VENTA_DIRECTA_LISTAR(intEjericio);
                    foreach (var item in query)
                    {
                        lista.Add(new EVentaDirecta()
                        {
                            dvdc_icod_doc_venta_directa = item.dvdc_icod_doc_venta_directa,
                            dvdc_vnumero_doc_venta_directa = item.dvdc_vnumero_doc_venta_directa,
                            dvdc_sfecha_doc_venta_directa = item.dvdc_sfecha_doc_venta_directa,
                            dvdc_icod_cliente = item.dvdc_icod_cliente,
                            clic_vcod_cliente = item.clic_vcod_cliente,
                            dvdc_vdireccion_cliente = item.dvdc_vdireccion_cliente,
                            dvdc_vruc = item.dvdc_vruc,
                            dvdc_vdni = item.dvdc_vdni,
                            dvdc_iid_vehiculo = item.dvdc_iid_vehiculo,
                            dvdc_vcolor = item.dvdc_vcolor,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            dvdc_npor_imp_igv = item.dvdc_npor_imp_igv,
                            dvdc_nmonto_neto = item.dvdc_nmonto_neto,
                            dvdc_nmonto_imp = item.dvdc_nmonto_imp,
                            dvdc_nmonto_total = item.dvdc_nmonto_total,
                            tablc_iid_situacion = Convert.ToInt32(item.tablc_iid_situacion),
                            strDesCliente = item.strDesCliente,
                            strPlaca = item.strPlaca,
                            strMarca = item.strMarca,
                            strModelo = item.strModelo,
                            strSituacion = item.strSituacion,
                            intAnioVehiculo = Convert.ToInt32(item.strAnioVehiculo),
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
        public int insertarVentaDirecta(EVentaDirecta ob)
        {
            int? id_factura = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_DOC_VENTA_DIRECTA_INSERTAR(
                            ref id_factura,
                            ob.dvdc_vnumero_doc_venta_directa,
                            ob.dvdc_sfecha_doc_venta_directa,
                            ob.dvdc_icod_cliente,
                            ob.clic_vcod_cliente,
                            ob.dvdc_vdireccion_cliente,
                            ob.dvdc_vruc,
                            ob.dvdc_vdni,
                            ob.dvdc_iid_vehiculo,
                            ob.dvdc_vcolor,
                            ob.tablc_iid_tipo_moneda,
                            ob.dvdc_npor_imp_igv,
                            ob.dvdc_nmonto_neto,
                            ob.dvdc_nmonto_imp,
                            ob.dvdc_nmonto_total,
                            ob.tablc_iid_situacion,
                            ob.intUsuario,
                            ob.strPc);
                }
                return Convert.ToInt32(id_factura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarVentaDirecta(EVentaDirecta ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_DOC_VENTA_DIRECTA_MODIFICAR(
                            ob.dvdc_icod_doc_venta_directa,
                            ob.dvdc_vnumero_doc_venta_directa,
                            ob.dvdc_sfecha_doc_venta_directa,
                            ob.dvdc_icod_cliente,
                            ob.clic_vcod_cliente,
                            ob.dvdc_vdireccion_cliente,
                            ob.dvdc_vruc,
                            ob.dvdc_vdni,
                            ob.dvdc_iid_vehiculo,
                            ob.dvdc_vcolor,
                            ob.tablc_iid_tipo_moneda,
                            ob.dvdc_npor_imp_igv,
                            ob.dvdc_nmonto_neto,
                            ob.dvdc_nmonto_imp,
                            ob.dvdc_nmonto_total,
                            ob.tablc_iid_situacion,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarVentaDirecta(EVentaDirecta ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_DOC_VENTA_DIRECTA_ELIMINAR(
                            ob.dvdc_icod_doc_venta_directa,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EVentaDirectaDet> listarVentaDirectaDetalle(int intVentaDirecta)
        {
            List<EVentaDirectaDet> lista = new List<EVentaDirectaDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_EXTEND_DOC_VENTA_DIRECTA_DETALLE_LISTAR(intVentaDirecta);
                    foreach (var item in query)
                    {
                        lista.Add(new EVentaDirectaDet()
                        {
                            dvdd_iid_icod_doc_venta_directa = item.dvdd_iid_icod_doc_venta_directa,
                            dvdd_iitem_doc_venta_directa = Convert.ToInt32(item.dvdd_iitem_doc_venta_directa),
                            dvdd_iid_almacen = Convert.ToInt32(item.dvdd_iid_almacen),
                            dvdd_iid_producto = Convert.ToInt32(item.dvdd_iid_producto),
                            dvdd_ncantidad = item.dvdd_ncantidad,
                            dvdd_vdescripcion = item.dvdd_vdescripcion,
                            dvdd_bbonificacion = Convert.ToBoolean(item.dvdd_bbonificacion),
                            dvdd_nprecio_unitario_item = item.dvdd_nprecio_unitario_item,
                            dvdd_nmonto_impuesto_item = item.dvdd_nmonto_impuesto_item,
                            dvdd_nprecio_total_item = item.dvdd_nprecio_total_item,
                            strCodProducto = item.strCodProducto,
                            strDesProducto = item.strDesProducto
                            //strLinea = item.strLinea,
                            //strSubLinea = item.strSubLinea,
                            //strDesUM = item.strDesUM,
                            //strAlmacen = item.strAlmacen,
                            //strMoneda = item.strMoneda,
                            //dblStockDisponible = item.dblStockDisponible,
                            //dvdd_icod_personal = item.dvdd_icod_personal,
                            //dvdd_area_personal = item.dvdd_area_personal,
                            //strPersonal = item.perc_vapellidos_nombres,
                            //dvdd_nporc_productividad = Convert.ToDecimal(item.dvdd_nporc_productividad),
                            //dvdd_nmonto_productividad = Convert.ToDecimal(item.dvdd_nmonto_productividad),
                            //dvdd_clasificacion = Convert.ToInt32(item.dvdd_clasificacion),
                            //dvdd_fecha_servicio = item.dvdd_fecha_servicio
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
        public void insertarVentaDirectaDetalle(EVentaDirectaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_DOC_VENTA_DIRECTA_DETALLE_INSERTAR(
                            ob.dvdc_icod_doc_venta_directa,
                            ob.dvdd_iitem_doc_venta_directa,
                            ob.dvdd_iid_almacen,
                            ob.dvdd_iid_producto,
                            ob.dvdd_ncantidad,
                            ob.dvdd_vdescripcion,
                            ob.dvdd_bbonificacion,
                            ob.dvdd_nprecio_unitario_item,
                            ob.dvdd_nmonto_impuesto_item,
                            ob.dvdd_nprecio_total_item,
                            ob.dvdd_icod_personal,
                            ob.dvdd_area_personal,
                            ob.dvdd_clasificacion,
                            ob.dvdd_nporc_productividad,
                            ob.dvdd_nmonto_productividad,
                            ob.dvdd_fecha_servicio,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarVentaDirectaDetalle(EVentaDirectaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_DOC_VENTA_DIRECTA_DETALLE_MODIFICAR(
                            ob.dvdd_iid_icod_doc_venta_directa,
                            ob.dvdc_icod_doc_venta_directa,
                            ob.dvdd_iitem_doc_venta_directa,
                            ob.dvdd_iid_almacen,
                            ob.dvdd_iid_producto,
                            ob.dvdd_ncantidad,
                            ob.dvdd_vdescripcion,
                            ob.dvdd_bbonificacion,
                            ob.dvdd_nprecio_unitario_item,
                            ob.dvdd_nmonto_impuesto_item,
                            ob.dvdd_nprecio_total_item,
                            ob.dvdd_icod_personal,
                            ob.dvdd_area_personal,
                            ob.dvdd_clasificacion,
                            ob.dvdd_nporc_productividad,
                            ob.dvdd_nmonto_productividad,
                            ob.dvdd_fecha_servicio,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarVentaDirectaDetalle(EVentaDirectaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_DOC_VENTA_DIRECTA_DETALLE_ELIMINAR(
                            ob.dvdd_iid_icod_doc_venta_directa,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Pago de Documentos de Venta
        public List<EPagoDocVenta> listarPago(int intTipoDoc, int intIcodDoc)
        {
            List<EPagoDocVenta> lista = new List<EPagoDocVenta>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_EXTEND_PAGO_LISTAR(intTipoDoc, intIcodDoc);
                    foreach (var item in query)
                    {
                        lista.Add(new EPagoDocVenta()
                        {
                            pgoc_icod_pago = item.pgoc_icod_pago,
                            tdocc_icoc_tipo_documento = Convert.ToInt32(item.tdocc_icoc_tipo_documento),
                            pgoc_icod_documento = Convert.ToInt32(item.pgoc_icod_documento),
                            pgoc_sfecha_pago = Convert.ToDateTime(item.pgoc_sfecha_pago),
                            pgoc_tipo_pago = Convert.ToInt32(item.pgoc_tipo_pago),
                            pgoc_icod_nota_credito = item.pgoc_icod_nota_credito,
                            pgoc_icod_tipo_tarjeta = item.pgoc_icod_tipo_tarjeta,
                            pgoc_icod_tipo_moneda = Convert.ToInt32(item.pgoc_icod_tipo_moneda),
                            pgoc_descripcion = item.pgoc_descripcion,
                            pgoc_nmonto = Convert.ToDecimal(item.pgoc_nmonto),
                            pgoc_dxc_icod_pago = Convert.ToInt64(item.pgoc_dxc_icod_pago),
                            strTipoPago = item.strTipoPago,
                            pgoc_fecha_venc_credito = item.pgoc_fecha_venc_credito,
                            tblc_iid_banco_cheque = item.tblc_iid_banco_cheque,
                            pgoc_nro_cheque = item.pgoc_nro_cheque,
                            pgoc_fecha_cob_cheque = item.pgoc_fecha_cob_cheque,
                            bcoc_icod_banco = item.bcoc_icod_banco,
                            bcod_icod_banco_cuenta = item.bcod_icod_banco_cuenta,
                            pgoc_icod_anticipo = item.pgoc_icod_anticipo,
                            strNroNotaCredito = item.strNroNotaCredito,
                            strNroAnticipo = item.strNroAnticipo,
                            pgoc_dxc_icod_canje_doc = item.pgoc_dxc_icod_canje_doc,
                            pgoc_iid_tipo_doc_docventa = Convert.ToInt32(item.pgoc_iid_tipo_doc_docventa),
                            pgoc_dxc_icod_doc = Convert.ToInt64(item.pgoc_dxc_icod_doc),
                            pgoc_icod_entidad_finan_mov = item.pgoc_icod_entidad_finan_mov,
                            strTipoMoneda = (Convert.ToInt32(item.pgoc_icod_tipo_moneda) == 3) ? "S/." : (Convert.ToInt32(item.pgoc_icod_tipo_moneda) == 4) ? "US$" : "",

                            intCtaContableBcoTarjeta = item.intCtaContableBcoTarjeta,
                            intAnaliticaBcoTarjeta = item.intAnaliticaBcoTarjeta,
                            strCodAnaliticaBcoTarjeta = item.strCodAnaliticaBcoTarjeta,
                            tdodc_iid_correlativo_nota_credito = item.tdodc_iid_correlativo_nota_credito,
                            tdodc_iid_correlativo_anticipo = item.tdodc_iid_correlativo_anticipo,

                            intAnaliticaClienteNC = item.intAnaliticaClienteNC,
                            strCodAnaliticaClienteNC = item.strCodAnaliticaClienteNC,
                            intAnaliticaClienteAnticipo = item.intAnaliticaClienteAnticipo,
                            strCodAnaliticaClienteAnticipo = item.strCodAnaliticaClienteAnticipo
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

        public List<EPagoDocVenta> getDatosPago(int intPago)
        {
            List<EPagoDocVenta> lista = new List<EPagoDocVenta>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_EXTEND_PAGO_GET_DATOS(intPago);
                    foreach (var item in query)
                    {
                        lista.Add(new EPagoDocVenta()
                        {
                            pgoc_icod_pago = item.pgoc_icod_pago,
                            tdocc_icoc_tipo_documento = Convert.ToInt32(item.tdocc_icoc_tipo_documento),
                            pgoc_icod_documento = item.pgoc_icod_documento,
                            pgoc_sfecha_pago = Convert.ToDateTime(item.pgoc_sfecha_pago),
                            pgoc_tipo_pago = Convert.ToInt32(item.pgoc_tipo_pago),
                            pgoc_icod_nota_credito = item.pgoc_icod_nota_credito,
                            pgoc_icod_tipo_tarjeta = item.pgoc_icod_tipo_tarjeta,
                            pgoc_icod_tipo_moneda = Convert.ToInt32(item.pgoc_icod_tipo_moneda),
                            pgoc_descripcion = item.pgoc_descripcion,
                            pgoc_nmonto = Convert.ToDecimal(item.pgoc_nmonto),
                            pgoc_dxc_icod_pago = Convert.ToInt64(item.pgoc_dxc_icod_pago),
                            strTipoPago = item.strTipoPago,
                            pgoc_fecha_venc_credito = item.pgoc_fecha_venc_credito,
                            tblc_iid_banco_cheque = item.tblc_iid_banco_cheque,
                            pgoc_nro_cheque = item.pgoc_nro_cheque,
                            pgoc_fecha_cob_cheque = item.pgoc_fecha_cob_cheque,
                            bcoc_icod_banco = item.bcoc_icod_banco,
                            bcod_icod_banco_cuenta = item.bcod_icod_banco_cuenta,
                            pgoc_icod_anticipo = item.pgoc_icod_anticipo,
                            strNroNotaCredito = item.strNroNotaCredito,
                            strNroAnticipo = item.strNroAnticipo,
                            pgoc_dxc_icod_canje_doc = item.pgoc_dxc_icod_canje_doc,
                            pgoc_iid_tipo_doc_docventa = Convert.ToInt32(item.pgoc_iid_tipo_doc_docventa),
                            pgoc_dxc_icod_doc = Convert.ToInt64(item.pgoc_dxc_icod_doc),
                            pgoc_icod_entidad_finan_mov = item.pgoc_icod_entidad_finan_mov,
                            pgoc_icod_cliente = Convert.ToInt32(item.pgoc_icod_cliente),
                            strTipoMoneda = (Convert.ToInt32(item.pgoc_icod_tipo_moneda) == 1) ? "S/." : (Convert.ToInt32(item.pgoc_icod_tipo_moneda) == 2) ? "US$" : "",

                            intCtaContableBcoTarjeta = item.intCtaContableBcoTarjeta,
                            intAnaliticaBcoTarjeta = item.intAnaliticaBcoTarjeta,
                            strCodAnaliticaBcoTarjeta = item.strCodAnaliticaBcoTarjeta,
                            tdodc_iid_correlativo_nota_credito = item.tdodc_iid_correlativo_nota_credito,
                            tdodc_iid_correlativo_anticipo = item.tdodc_iid_correlativo_anticipo,

                            intAnaliticaClienteNC = item.intAnaliticaClienteNC,
                            strCodAnaliticaClienteNC = item.strCodAnaliticaClienteNC,
                            intAnaliticaClienteAnticipo = item.intAnaliticaClienteAnticipo,
                            strCodAnaliticaClienteAnticipo = item.strCodAnaliticaClienteAnticipo
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

        public int insertarPago(EPagoDocVenta oBe)
        {
            int? idPago = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PAGO_INSERTAR(
                        ref idPago,
                        oBe.tdocc_icoc_tipo_documento,
                        oBe.pgoc_icod_documento,
                        oBe.pgoc_icod_cliente,
                        oBe.pgoc_sfecha_pago,
                        oBe.pgoc_vnumero_planilla,
                        oBe.pgoc_tipo_pago,
                        oBe.pgoc_icod_nota_credito,
                        oBe.pgoc_icod_tipo_tarjeta,
                        oBe.pgoc_icod_tipo_moneda,
                        oBe.pgoc_descripcion,
                        oBe.pgoc_nmonto,
                        oBe.pgoc_dxc_icod_doc,
                        oBe.pgoc_dxc_icod_pago,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.pgoc_fecha_venc_credito,
                        oBe.tblc_iid_banco_cheque,
                        oBe.pgoc_nro_cheque,
                        oBe.pgoc_fecha_cob_cheque,
                        oBe.bcoc_icod_banco,
                        oBe.bcod_icod_banco_cuenta,
                        oBe.pgoc_icod_anticipo,
                        oBe.pgoc_dxc_icod_canje_doc,
                        oBe.pgoc_icod_entidad_finan_mov,
                        oBe.pgoc_iid_tipo_doc_docventa
                        );
                }
                return Convert.ToInt32(idPago);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarPago(EPagoDocVenta oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PAGO_MODIFICAR(
                        oBe.pgoc_icod_pago,
                        oBe.tdocc_icoc_tipo_documento,
                        oBe.pgoc_icod_documento,
                        oBe.pgoc_icod_cliente,
                        oBe.pgoc_sfecha_pago,
                        oBe.pgoc_vnumero_planilla,
                        oBe.pgoc_tipo_pago,
                        oBe.pgoc_icod_nota_credito,
                        oBe.pgoc_icod_tipo_tarjeta,
                        oBe.pgoc_icod_tipo_moneda,
                        oBe.pgoc_descripcion,
                        oBe.pgoc_nmonto,
                        oBe.pgoc_dxc_icod_doc,
                        oBe.pgoc_dxc_icod_pago,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.pgoc_fecha_venc_credito,
                        oBe.tblc_iid_banco_cheque,
                        oBe.pgoc_nro_cheque,
                        oBe.pgoc_fecha_cob_cheque,
                        oBe.bcoc_icod_banco,
                        oBe.bcod_icod_banco_cuenta,
                        oBe.pgoc_icod_anticipo,
                        oBe.pgoc_dxc_icod_canje_doc,
                        oBe.pgoc_icod_entidad_finan_mov,
                        oBe.pgoc_iid_tipo_doc_docventa);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PAGO_ELIMINAR(
                        oBe.pgoc_icod_pago,
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
        #region Planilla Cobranza Cabecera
        public List<EPlanillaCobranzaCab> listarPlanillaCobranzaCab(int intEjercicio)
        {
            List<EPlanillaCobranzaCab> lista = new List<EPlanillaCobranzaCab>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_EXTEND_PLANILLA_COBRANZA_CAB_LISTAR(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EPlanillaCobranzaCab()
                        {
                            plnc_icod_planilla = item.plnc_icod_planilla,
                            plnc_vnumero_planilla = item.plnc_vnumero_planilla,
                            plnc_sfecha_planilla = Convert.ToDateTime(item.plnc_sfecha_planilla),
                            tblc_iid_tipo_moneda = Convert.ToInt32(item.tblc_iid_tipo_moneda),
                            tblc_iid_situacion = Convert.ToInt32(item.tblc_iid_situacion),
                            plnc_nmonto_importe = Convert.ToDecimal(item.plnc_nmonto_importe),
                            plnc_nmonto_pagado = Convert.ToDecimal(item.plnc_nmonto_pagado),
                            plnc_vobservaciones = item.plnc_vobservaciones,
                            strSituacion = item.strSituacion,
                            plnc_icod_ent_finan_mov_sol = item.plnc_icod_ent_finan_mov_sol,
                            plnc_icod_ent_finan_mov_dol = item.plnc_icod_ent_finan_mov_dol,
                            plnc_monto_soles = item.plnc_monto_soles,
                            plnc_monto_dolares = item.plnc_monto_dolares,
                            plnc_fecha_modi = item.plnc_fecha_modi,

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
        public List<EPlanillaCobranzaCab> listarPlanillaCobranzaContabilizacionCabVCO(int intEjercicio, int intPeriodo)
        {
            List<EPlanillaCobranzaCab> lista = new List<EPlanillaCobranzaCab>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PLANILLA_COBRANZA_CAB_CONTABILIZACION_LISTAR(intEjercicio, intPeriodo);
                    foreach (var item in query)
                    {
                        lista.Add(new EPlanillaCobranzaCab()
                        {
                            plnc_icod_planilla = item.plnc_icod_planilla,
                            plnc_vnumero_planilla = item.plnc_vnumero_planilla,
                            plnc_sfecha_planilla = Convert.ToDateTime(item.plnc_sfecha_planilla),
                            tblc_iid_tipo_moneda = Convert.ToInt32(item.tblc_iid_tipo_moneda),
                            tblc_iid_situacion = Convert.ToInt32(item.tblc_iid_situacion),
                            plnc_nmonto_importe = Convert.ToDecimal(item.plnc_nmonto_importe),
                            plnc_nmonto_pagado = Convert.ToDecimal(item.plnc_nmonto_pagado),
                            plnc_vobservaciones = item.plnc_vobservaciones,
                            strSituacion = item.strSituacion,
                            plnc_icod_ent_finan_mov_sol = item.plnc_icod_ent_finan_mov_sol,
                            plnc_icod_ent_finan_mov_dol = item.plnc_icod_ent_finan_mov_dol,
                            /**/
                            intAnaliticaCtaBancariaSol = item.intAnaliticaCtaBancariaSol,
                            strCodAnaliticaCtaBancariaSol = item.strCodAnaliticaCtaBancariaSol,
                            intCtaContableCtaBancariaSol = item.intCtaContableCtaBancariaSol,
                            dcmlTipoCambioSol = item.dcmlTipoCambioSol,
                            dcmlTotalSol = item.dcmlTotalSol,
                            intAnaliticaCtaBancariaDol = item.intAnaliticaCtaBancariaDol,
                            strCodAnaliticaCtaBancariaDol = item.strCodAnaliticaCtaBancariaDol,
                            intCtaContableCtaBancariaDol = item.intCtaContableCtaBancariaDol,
                            dcmlTipoCambioDol = item.dcmlTipoCambioDol,
                            dcmlTotalDol = item.dcmlTotalDol
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
        public List<EPlanillaCobranzaCab> listarPlanillaCobranzaContabilizacionCab(int intEjercicio, int intPeriodo)
        {
            List<EPlanillaCobranzaCab> lista = new List<EPlanillaCobranzaCab>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PLANILLA_COBRANZA_CAB_CONTABILIZACION_LISTAR(intEjercicio, intPeriodo);
                    foreach (var item in query)
                    {
                        lista.Add(new EPlanillaCobranzaCab()
                        {
                            plnc_icod_planilla = item.plnc_icod_planilla,
                            plnc_vnumero_planilla = item.plnc_vnumero_planilla,
                            plnc_sfecha_planilla = Convert.ToDateTime(item.plnc_sfecha_planilla),
                            tblc_iid_tipo_moneda = Convert.ToInt32(item.tblc_iid_tipo_moneda),
                            tblc_iid_situacion = Convert.ToInt32(item.tblc_iid_situacion),
                            plnc_nmonto_importe = Convert.ToDecimal(item.plnc_nmonto_importe),
                            plnc_nmonto_pagado = Convert.ToDecimal(item.plnc_nmonto_pagado),
                            plnc_vobservaciones = item.plnc_vobservaciones,
                            strSituacion = item.strSituacion,
                            plnc_icod_ent_finan_mov_sol = item.plnc_icod_ent_finan_mov_sol,
                            plnc_icod_ent_finan_mov_dol = item.plnc_icod_ent_finan_mov_dol,
                            /**/
                            intAnaliticaCtaBancariaSol = item.intAnaliticaCtaBancariaSol,
                            strCodAnaliticaCtaBancariaSol = item.strCodAnaliticaCtaBancariaSol,
                            intCtaContableCtaBancariaSol = item.intCtaContableCtaBancariaSol,
                            dcmlTipoCambioSol = item.dcmlTipoCambioSol,
                            dcmlTotalSol = item.dcmlTotalSol,
                            intAnaliticaCtaBancariaDol = item.intAnaliticaCtaBancariaDol,
                            strCodAnaliticaCtaBancariaDol = item.strCodAnaliticaCtaBancariaDol,
                            intCtaContableCtaBancariaDol = item.intCtaContableCtaBancariaDol,
                            dcmlTipoCambioDol = item.dcmlTipoCambioDol,
                            dcmlTotalDol = item.dcmlTotalDol,
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
        public Tuple<decimal, decimal> getTotalEfectivoPlanilla(int intPlanilla)
        {
            decimal? dcmlEfectivoSol = 0;
            decimal? dcmlEfectivoDol = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_GET_TOTAL_EFECTIVO_PLANILLA(intPlanilla, ref dcmlEfectivoSol, ref dcmlEfectivoDol);
                }
                return new Tuple<decimal, decimal>(Convert.ToDecimal(dcmlEfectivoSol), Convert.ToDecimal(dcmlEfectivoDol));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void actualizarIcodMovimientoPlanilla(int intPlanilla, int? intIcodSol, int? intIcodDol, decimal? intSoles, decimal? intDolares,
            DateTime? fecha)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PLANILLA_ICOD_MOV_ACTUALIZAR(
                        intPlanilla,
                        intIcodSol,
                        intIcodDol,
                        intSoles,
                        intDolares,
                        fecha);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void revertirCierre(int intPlanilla)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PLANILLA_REVERTIR_CIERRE(intPlanilla);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarPlanillaCobranzaCab(EPlanillaCobranzaCab ob)
        {
            int? id_planilla = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PLANILLA_COBRANZA_CAB_INSERTAR(
                            ref id_planilla,
                            ob.plnc_vnumero_planilla,
                            ob.plnc_sfecha_planilla,
                            ob.tblc_iid_tipo_moneda,
                            ob.tblc_iid_situacion,
                            ob.plnc_nmonto_importe,
                            ob.plnc_nmonto_pagado,
                            ob.plnc_vobservaciones,
                            ob.intUsuario,
                            ob.strPc);
                }
                return Convert.ToInt32(id_planilla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPlanillaCobranzaCab(EPlanillaCobranzaCab ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PLANILLA_COBRANZA_CAB_MODIFICAR(
                            ob.plnc_icod_planilla,
                            ob.plnc_vnumero_planilla,
                            ob.plnc_sfecha_planilla,
                            ob.tblc_iid_tipo_moneda,
                            ob.tblc_iid_situacion,
                            ob.plnc_nmonto_importe,
                            ob.plnc_nmonto_pagado,
                            ob.plnc_vobservaciones,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarPlanillaCobranzaCab(EPlanillaCobranzaCab ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PLANILLA_COBRANZA_CAB_ELIMINAR(
                            ob.plnc_icod_planilla,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Planilla Cobranza Detalle
        public List<EPlanillaCobranzaDet> listarPlanillaCobranzaDetalleVCO(int intPlanilla)
        {
            List<EPlanillaCobranzaDet> lista = new List<EPlanillaCobranzaDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_EXTEND_PLANILLA_DET_LISTAR_VCO(intPlanilla);
                    foreach (var item in query)
                    {
                        lista.Add(new EPlanillaCobranzaDet()
                        {
                            tablc_iid_tipo_mov = Convert.ToInt32(item.tablc_iid_tipo_mov),
                            plnd_icod_tipo_doc = item.plnd_icod_tipo_doc,
                            plnd_icod_documento = item.plnd_icod_documento,
                            plnd_vnumero_doc = item.plnd_vnumero_doc,
                            plnd_nmonto_pagado = Convert.ToDecimal(item.plnd_nmonto_pagado),
                            strTipoDoc = item.tdocc_vabreviatura_tipo_doc,
                            strCliente = item.strCliente,
                            pgoc_icod_pago = item.pgoc_icod_pago,
                            intIcodDxc = item.intIcodDxc,
                            intCliente = Convert.ToInt32(item.intCliente),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            intSituacionFavBov = Convert.ToInt32(item.intSituacion),
                            strSituacionFavBov = item.strSituacion,
                            intIcodEntidadFinanMov = item.antc_icod_entidad_finan_mov,
                            intAnaliticaCliente = item.intAnaliticaCliente,
                            strCodAnaliticaCliente = item.strAnaliticaCliente,
                            tdocd_iid_correlativo = Convert.ToInt32(item.tdodc_iid_correlativo),
                            /*Se utiliza los sgts. campos en contabilización para anticipos*/
                            intAnaliticaBancoTarjetaBanco = item.intAnaliticaBancoTarjetaBanco,
                            strCodAnaliticaBancoTarjetaBanco = item.strAnaliticaBancoTarjetaBanco,
                            intCtaCbleTarjetaBanco = item.intCtaCbleTarjetaBanco,
                            intTipoDocDelPago = item.intTipoDocDelPago,
                            strNroOt = item.strNroOt,
                            tdocc_vabreviatura_tipo_docPago = item.tdocc_vabreviatura_tipo_docPago,
                            ctacc_icod_cuenta_contable_nac = item.ctacc_icod_cuenta_contable_nac,
                            tdocd_iid_codigo_doc_det = Convert.ToInt32(item.tdocd_iid_codigo_doc_det),
                            tablc_iid_tipo_pago = Convert.ToInt32(item.tablc_iid_tipo_pago),
                            intTipoTarjeta = Convert.ToInt32(item.tablc_iid_tipo_tarjeta)
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

        public List<EPlanillaCobranzaDet> listarPlanillaCobranzaDetalle(int intPlanilla)
        {
            List<EPlanillaCobranzaDet> lista = new List<EPlanillaCobranzaDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_EXTEND_PLANILLA_DET_LISTAR(intPlanilla);
                    foreach (var item in query)
                    {
                        lista.Add(new EPlanillaCobranzaDet()
                        {
                            plnd_icod_detalle = item.plnd_icod_detalle,
                            plnc_icod_planilla = item.plnc_icod_planilla,
                            tablc_iid_tipo_mov = Convert.ToInt32(item.tablc_iid_tipo_mov),
                            plnd_sfecha_doc = Convert.ToDateTime(item.plnd_sfecha_doc),
                            plnd_icod_tipo_doc = item.plnd_icod_tipo_doc,
                            plnd_icod_documento = item.plnd_icod_documento,
                            plnd_vnumero_doc = item.plnd_vnumero_doc,
                            plnd_nmonto = Convert.ToDecimal(item.plnd_nmonto),
                            plnd_nmonto_pagado = Convert.ToDecimal(item.plnd_nmonto_pagado),
                            strTipoDoc = item.tdocc_vabreviatura_tipo_doc,
                            strCliente = item.strCliente,
                            strTipoMov = item.strTipoMov,
                            pgoc_icod_pago = item.pgoc_icod_pago,
                            pgoc_dxc_icod_pago = item.pgoc_dxc_icod_pago,
                            intIcodDxc = item.intIcodDxc,
                            intCliente = Convert.ToInt32(item.intCliente),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            strTipoMoneda = item.strTipoMoneda,
                            intTipoPago = item.pgoc_tipo_pago,
                            intNotaCredito = item.pgoc_icod_nota_credito,
                            intTipoTarjeta = item.pgoc_icod_tipo_tarjeta,
                            strPagoDescripcion = item.strPagoDescripcion,
                            StrNotaCredito = item.StrNotaCredito,
                            antc_icod_anticipo = item.antc_icod_anticipo,
                            strAnticipo = item.strAnticipo,
                            strAdelantoCliente = item.strAdelantoCliente,
                            intSituacionFavBov = Convert.ToInt32(item.intSituacion),
                            strSituacionFavBov = item.strSituacion,
                            plnd_tipo_cambio = Convert.ToDecimal(item.plnd_tipo_cambio),
                            intIcodEntidadFinanMov = item.antc_icod_entidad_finan_mov,
                            intAnaliticaCliente = item.intAnaliticaCliente,
                            strCodAnaliticaCliente = item.strAnaliticaCliente,
                            tdocd_iid_correlativo = Convert.ToInt32(item.tdodc_iid_correlativo),
                            /*Se utiliza los sgts. campos en contabilización para anticipos*/
                            intAnaliticaBancoTarjetaBanco = item.intAnaliticaBancoTarjetaBanco,
                            strCodAnaliticaBancoTarjetaBanco = item.strAnaliticaBancoTarjetaBanco,
                            intCtaCbleTarjetaBanco = item.intCtaCbleTarjetaBanco,
                            intTipoDocDelPago = item.intTipoDocDelPago,
                            strNroOt = item.strNroOt

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

        public List<EPlanillaCobranzaDet> listarPlanillaCobranzaImpresionDetalle(int intPlanilla)
        {
            List<EPlanillaCobranzaDet> lista = new List<EPlanillaCobranzaDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_EXTEND_PLANILLA_DET_IMPRESION_LISTAR(intPlanilla);
                    foreach (var item in query)
                    {
                        lista.Add(new EPlanillaCobranzaDet()
                        {
                            plnd_icod_detalle = item.plnd_icod_detalle,
                            plnc_icod_planilla = item.plnc_icod_planilla,
                            tablc_iid_tipo_mov = Convert.ToInt32(item.tablc_iid_tipo_mov),
                            plnd_sfecha_doc = Convert.ToDateTime(item.plnd_sfecha_doc),
                            plnd_icod_tipo_doc = item.plnd_icod_tipo_doc,
                            plnd_icod_documento = item.plnd_icod_documento,
                            plnd_vnumero_doc = item.plnd_vnumero_doc,
                            plnd_nmonto = Convert.ToDecimal(item.plnd_nmonto),
                            plnd_nmonto_pagado = Convert.ToDecimal(item.plnd_nmonto_pagado),
                            strTipoDoc = String.Format("{0}-{1}", item.tdocc_vabreviatura_tipo_doc, item.plnd_vnumero_doc),
                            strCliente = item.strCliente,
                            strTipoMov = item.strTipoMov,
                            pgoc_icod_pago = item.pgoc_icod_pago,
                            pgoc_dxc_icod_pago = item.pgoc_dxc_icod_pago,
                            intIcodDxc = item.pgoc_dxc_icod_doc,
                            intCliente = Convert.ToInt32(item.intCliente),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            strMonedaGroup = (Convert.ToInt32(item.tablc_iid_tipo_moneda) == 3) ? "MOVIMIENTOS EN NUEVOS SOLES" : "MOVIMIENTOS EN DOLARES",
                            strTipoMoneda = item.strTipoMoneda,
                            intTipoPago = item.pgoc_tipo_pago,
                            intNotaCredito = item.pgoc_icod_nota_credito,
                            intTipoTarjeta = item.pgoc_icod_tipo_tarjeta,
                            strPagoDescripcion = item.strPagoDescripcion,
                            StrNotaCredito = item.StrNotaCredito,
                            antc_icod_anticipo = item.antc_icod_anticipo,
                            strAnticipo = item.strAnticipo,
                            strAdelantoCliente = item.strAdelantoCliente,
                            intSituacionFavBov = Convert.ToInt32(item.intSituacion),
                            strSituacionFavBov = item.strSituacion,
                            plnd_tipo_cambio = Convert.ToDecimal(item.plnd_tipo_cambio),
                            dblPagoEfectivo = Convert.ToDecimal(item.dblPagoEfectivo),
                            dblPagoTarjetaCredito = Convert.ToDecimal(item.dblPagoTarjetaCredito),
                            dblPagoNotaCredito = Convert.ToDecimal(item.dblPagoNotaCredito),
                            dblPagoCheque = Convert.ToDecimal(item.dblPagoCheque),
                            dblPagoTransferencia = Convert.ToDecimal(item.dblPagoTransferencia),
                            dblPagoCredito = Convert.ToDecimal(item.dblPagoCredito),
                            dblPagoAnticipo = Convert.ToDecimal(item.dblPagoAnticipo)
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

        public int insertarPlanillaCobranzaDetalle(EPlanillaCobranzaDet ob)
        {
            int? id_planilla_det = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PLANILLA_DET_INSERTAR(
                            ref id_planilla_det,
                            ob.plnc_icod_planilla,
                            ob.tablc_iid_tipo_mov,
                            ob.plnd_sfecha_doc,
                            ob.plnd_icod_tipo_doc,
                            ob.plnd_icod_documento,
                            ob.plnd_vnumero_doc,
                            ob.plnd_nmonto,
                            ob.plnd_nmonto_pagado,
                            ob.intUsuario,
                            ob.strPc,
                            ob.pgoc_icod_pago,
                            ob.tablc_iid_tipo_moneda,
                            ob.antc_icod_anticipo,
                            ob.plnd_tipo_cambio);
                }
                return Convert.ToInt32(id_planilla_det);

                if (ob.tablc_iid_tipo_mov == 2)
                {
                    ob.plnd_icod_detalle = Convert.ToInt32(id_planilla_det);
                    modificarPlanillaCobranzaDetalleIcodTipoDocumento(ob);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPlanillaCobranzaDetalleIcodTipoDocumento(EPlanillaCobranzaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PLANILLA_DET_MODIFICAR_TIPO_DOC(
                            ob.plnd_icod_detalle,
                            ob.plnd_icod_tipo_doc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarPlanillaCobranzaDetalle(EPlanillaCobranzaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PLANILLA_DET_MODIFICAR(
                            ob.plnd_icod_detalle,
                            ob.plnd_sfecha_doc,
                            ob.plnd_icod_tipo_doc,
                            ob.plnd_icod_documento,
                            ob.plnd_vnumero_doc,
                            ob.plnd_nmonto,
                            ob.plnd_nmonto_pagado,
                            ob.intUsuario,
                            ob.strPc,
                            ob.plnd_tipo_cambio,
                            ob.pgoc_icod_pago);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarPlanillaCobranzaDetalle(EPlanillaCobranzaDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_PLANILLA_DET_ELIMINAR(
                            ob.plnd_icod_detalle,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Anticipo
        public int insertarAnticipo(EAnticipo ob)
        {
            int? id_anticipo = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_ANTICIPO_INSERTAR(
                            ref id_anticipo,
                            ob.antc_vnumero_anticipo,
                            ob.antc_sfecha_anticipo,
                            ob.antc_icod_cliente,
                            ob.antc_observaciones,
                            ob.antc_nmonto_anticipo,
                            ob.tablc_iid_tipo_moneda,
                            ob.tablc_iid_situacion_anticipo,
                            ob.antc_icod_adelanto_cliente,
                            ob.antc_icod_dxc_adelanto,
                            ob.tablc_iid_tipo_pago,
                            ob.tablc_iid_tipo_tarjeta,
                            ob.antc_icod_entidad_finan_mov,
                            ob.intUsuario,
                            ob.strPc
                           );
                }
                return Convert.ToInt32(id_anticipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarAnticipo(EAnticipo ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_ANTICIPO_MODIFICAR(
                            ob.antc_icod_anticipo,
                            ob.antc_vnumero_anticipo,
                            ob.antc_sfecha_anticipo,
                            ob.antc_icod_cliente,
                            ob.antc_observaciones,
                            ob.antc_nmonto_anticipo,
                            ob.tablc_iid_tipo_moneda,
                            ob.tablc_iid_situacion_anticipo,
                            ob.antc_icod_adelanto_cliente,
                            ob.antc_icod_dxc_adelanto,
                            ob.tablc_iid_tipo_pago,
                            ob.tablc_iid_tipo_tarjeta,
                            ob.antc_icod_entidad_finan_mov,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarAnticipo(EAnticipo ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_EXTEND_ANTICIPO_ELIMINAR(
                            ob.antc_icod_anticipo,
                            ob.intUsuario,
                            ob.strPc);
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
            List<ERetencion> lista = new List<ERetencion>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_RETENCION_CAB_LISTAR(intEjericio, intPeriodo);
                    foreach (var item in query)
                    {
                        lista.Add(new ERetencion()
                        {
                            retc_icod_comprobante_retencion = item.retc_icod_comprobante_retencion,
                            anioc_iid_anio = Convert.ToInt32(item.anioc_iid_anio),
                            mesec_iid_mes = Convert.ToInt32(item.mesec_iid_mes),
                            retc_vnumero_comprob_reten = item.retc_vnumero_comprob_reten,
                            retc_sfec_comprob_reten = Convert.ToDateTime(item.retc_sfec_comprob_reten),
                            proc_icod_cliente = Convert.ToInt32(item.proc_icod_cliente),
                            tablc_iid_moneda = Convert.ToInt32(item.tablc_iid_moneda),
                            retc_nmto_tipo_cambio = Convert.ToDecimal(item.retc_nmto_tipo_cambio),
                            retc_nmto_total_pago = Convert.ToDecimal(item.retc_nmto_total_pago),
                            retc_nmto_total_retencion = Convert.ToDecimal(item.retc_nmto_total_retencion),
                            strCliente = item.strCliente,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            intAnalitica = Convert.ToInt32(item.intAnalitica),
                            strCodAnalitica = item.strCodAnalitica,
                            retc_sfecha_pago = Convert.ToDateTime(item.retc_sfecha_pago)
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
        public int insertarRetencionCab(ERetencion ob)
        {
            int? id_retencion = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_RETENCION_CAB_INSERTAR(
                        ref id_retencion,
                        ob.anioc_iid_anio,
                        ob.mesec_iid_mes,
                        ob.retc_vnumero_comprob_reten,
                        ob.retc_sfec_comprob_reten,
                        ob.proc_icod_cliente,
                        ob.tablc_iid_moneda,
                        ob.retc_nmto_tipo_cambio,
                        ob.retc_nmto_total_pago,
                        ob.retc_nmto_total_retencion,
                        ob.tablc_iid_situacion,
                        ob.intUsuario,
                        ob.strPc,
                        ob.retc_sfecha_pago);
                }
                return Convert.ToInt32(id_retencion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarRetencionCab(ERetencion ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_RETENCION_CAB_MODIFICAR(
                        ob.retc_icod_comprobante_retencion,
                        ob.anioc_iid_anio,
                        ob.mesec_iid_mes,
                        ob.retc_vnumero_comprob_reten,
                        ob.retc_sfec_comprob_reten,
                        ob.proc_icod_cliente,
                        ob.tablc_iid_moneda,
                        ob.retc_nmto_tipo_cambio,
                        ob.retc_nmto_total_pago,
                        ob.retc_nmto_total_retencion,
                        ob.tablc_iid_situacion,
                        ob.intUsuario,
                        ob.strPc,
                        ob.retc_sfecha_pago);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarRetencionCab(ERetencion ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_RETENCION_CAB_ELIMINAR(
                            ob.retc_icod_comprobante_retencion,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ERetencionDet> listarRetencionDet(int intRetencion)
        {
            List<ERetencionDet> lista = new List<ERetencionDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_RETENCION_DET_LISTAR(intRetencion);
                    foreach (var item in query)
                    {
                        lista.Add(new ERetencionDet()
                        {
                            retd_icod_detalle = item.retd_icod_detalle,
                            retc_icod_comprobante_retencion = Convert.ToInt32(item.retc_icod_comprobante_retencion),
                            tdoc_icod_tipo_documento = Convert.ToInt32(item.tdoc_icod_tipo_documento),
                            retd_vnumero_documento = item.retd_vnumero_documento,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            retd_sfec_documento = Convert.ToDateTime(item.retd_sfec_documento),
                            retd_nmto_tipo_cambio_doc = Convert.ToDecimal(item.retd_nmto_tipo_cambio_doc),
                            retd_nmto_pago_doc = Convert.ToDecimal(item.retd_nmto_pago_doc),
                            retd_nmto_retencion = Convert.ToDecimal(item.retd_nmto_retencion),
                            retd_nmto_total_doc = Convert.ToDecimal(item.retd_nmto_total_doc),
                            pdxpc_icod_correlativo = Convert.ToInt64(item.pdxpc_icod_correlativo),
                            strTipoDoc = item.strTipoDoc,
                            strMoneda = item.strMoneda,
                            intIcodDXC = Convert.ToInt64(item.intIcodDXC),
                            tdodc_iid_correlativo = Convert.ToInt32(item.tdodc_iid_correlativo),
                            intAnalitica = Convert.ToInt32(item.intAnalitica),
                            strCodAnalitica = item.strCodAnalitica,
                            Moneda_DXC = item.Moneda_DXC
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
        public void insertarRetencionDet(ERetencionDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_RETENCION_DET_INSERTAR(
                        ob.retc_icod_comprobante_retencion,
                        ob.tdoc_icod_tipo_documento,
                        ob.retd_vnumero_documento,
                        ob.tablc_iid_tipo_moneda,
                        ob.retd_sfec_documento,
                        ob.retd_nmto_tipo_cambio_doc,
                        ob.retd_nmto_pago_doc,
                        ob.retd_nmto_retencion,
                        ob.retd_nmto_total_doc,
                        ob.pdxpc_icod_correlativo,
                        ob.intUsuario,
                        ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarRetencionDet(ERetencionDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_RETENCION_DET_MODIFICAR(
                            ob.retd_icod_detalle,
                            ob.tdoc_icod_tipo_documento,
                            ob.retd_vnumero_documento,
                            ob.tablc_iid_tipo_moneda,
                            ob.retd_sfec_documento,
                            ob.retd_nmto_tipo_cambio_doc,
                            ob.retd_nmto_pago_doc,
                            ob.retd_nmto_retencion,
                            ob.retd_nmto_total_doc,
                            ob.pdxpc_icod_correlativo,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarRetencionDet(ERetencionDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_RETENCION_DET_ELIMINAR(
                            ob.retd_icod_detalle,
                            ob.pdxpc_icod_correlativo,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Nota Crédito Ventas
        public List<ENotaCredito> listarNotaCreditoClienteCab(int intEjericio)
        {
            List<ENotaCredito> lista = new List<ENotaCredito>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_NOTA_CREDITO_VENTA_LISTAR(intEjericio);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaCredito()
                        {
                            ncrec_icod_credito = item.ncrec_icod_credito,
                            ncrec_vnumero_credito = item.ncrec_vnumero_credito,
                            ncrec_sfecha_credito = item.ncrec_sfecha_credito,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            ncrec_ianio = Convert.ToInt32(item.ncrec_ianio),
                            ncrec_vreferencia = item.ncrec_vreferencia,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            tablc_iid_tipo_nota_credito_venta = item.tablc_iid_tipo_nota_credito_venta,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo,
                            ncrec_vnumero_documento = item.ncrec_vnumero_documento,
                            ncrec_sfecha_documento = item.ncrec_sfecha_documento,
                            vendc_icod_vendedor = item.vendc_icod_vendedor,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            ncrec_nmonto_neto = item.ncrec_nmonto_neto,
                            ncrec_npor_imp_igv = item.ncrec_npor_imp_igv,
                            ncrec_nmonto_total = item.ncrec_nmonto_total,
                            ncrec_iid_situacion_credito = Convert.ToInt32(item.ncrec_iid_situacion_credito),
                            almac_icod_almacen = item.almac_icod_almacen,
                            ncrec_tipo_cambio_fecha_doc_venta = item.ncrec_tipo_cambio_fecha_doc_venta,
                            ncrec_nmonto_pagado = item.ncrec_nmonto_pagado,
                            strSituacion = item.strSituacion,
                            strDesCliente = item.strDesCliente,
                            strRuc = item.strRuc,
                            DirecionCliente = item.DireccionCliente,
                            strTipoDoc = item.strTipoDoc,
                            strMoneda = item.strMoneda,
                            ncrec_icod_dxc = Convert.ToInt64(item.ncrec_icod_dxc),
                            ncrec_bincluye_igv = item.ncrec_bincluye_igv,
                            ubicc_icod_ubicacion = Convert.ToInt32(item.ubicc_icod_ubicacion),
                            ncrec_iclase_doc = Convert.ToInt32(item.ncrec_iclase_doc),
                            StrClaseDocumento = item.StrClaseDocumento,
                            ncrec_tipo_nota_credito = Convert.ToInt32(item.ncrec_tipo_nota_credito),
                            ncrec_vmotivo_sunat = item.ncrec_vmotivo_sunat,
                            doc_icod_documento = item.ncrec_icod_credito,
                            ncrec_vdetalle = item.ncrec_vdetalle
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
        public List<ENotaCredito> ConsultaNotaCreditoClienteCab(int intEjericio, DateTime dtFechaDesde, DateTime dtFechaHasta, int? intCliente)
        {
            List<ENotaCredito> lista = new List<ENotaCredito>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_NOTA_CREDITO_VENTA_CONSULTA(intEjericio, dtFechaDesde, dtFechaHasta, intCliente);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaCredito()
                        {
                            ncrec_icod_credito = item.ncrec_icod_credito,
                            ncrec_vnumero_credito = item.ncrec_vnumero_credito,
                            ncrec_sfecha_credito = item.ncrec_sfecha_credito,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            ncrec_ianio = Convert.ToInt32(item.ncrec_ianio),
                            ncrec_vreferencia = item.ncrec_vreferencia,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            tablc_iid_tipo_nota_credito_venta = item.tablc_iid_tipo_nota_credito_venta,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo,
                            ncrec_vnumero_documento = item.ncrec_vnumero_documento,
                            ncrec_sfecha_documento = item.ncrec_sfecha_documento,
                            vendc_icod_vendedor = item.vendc_icod_vendedor,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            ncrec_nmonto_neto = item.ncrec_nmonto_neto,
                            ncrec_npor_imp_igv = item.ncrec_npor_imp_igv,
                            ncrec_nmonto_total = item.ncrec_nmonto_total,
                            ncrec_iid_situacion_credito = Convert.ToInt32(item.ncrec_iid_situacion_credito),
                            almac_icod_almacen = item.almac_icod_almacen,
                            ncrec_tipo_cambio_fecha_doc_venta = item.ncrec_tipo_cambio_fecha_doc_venta,
                            ncrec_nmonto_pagado = item.ncrec_nmonto_pagado,
                            strSituacion = item.strSituacion,
                            strDesCliente = item.strDesCliente,
                            strRuc = item.strRuc,
                            DirecionCliente = item.DireccionCliente,
                            strTipoDoc = item.strTipoDoc,
                            strMoneda = item.strMoneda,
                            ncrec_icod_dxc = Convert.ToInt64(item.ncrec_icod_dxc),
                            ncrec_bincluye_igv = item.ncrec_bincluye_igv,
                            ubicc_icod_ubicacion = Convert.ToInt32(item.ubicc_icod_ubicacion),
                            ncrec_iclase_doc = Convert.ToInt32(item.ncrec_iclase_doc),
                            StrClaseDocumento = item.StrClaseDocumento,
                            ncrec_tipo_nota_credito = Convert.ToInt32(item.ncrec_tipo_nota_credito),
                            ncrec_vmotivo_sunat = item.ncrec_vmotivo_sunat,
                            doc_icod_documento = item.ncrec_icod_credito,
                            ncrec_vdetalle = item.ncrec_vdetalle
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
        public int insertarNotaCreditoClienteCab(ENotaCredito ob)
        {
            int? id_retencion = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_CREDITO_VENTA_INSERTAR(
                        ref id_retencion,
                        ob.ncrec_vnumero_credito,
                        ob.ncrec_sfecha_credito,
                        ob.cliec_icod_cliente,
                        ob.ncrec_ianio,
                        ob.ncrec_vreferencia,
                        ob.tdocc_icod_tipo_doc,
                        ob.tablc_iid_tipo_nota_credito_venta,
                        ob.tdodc_iid_correlativo,
                        ob.ncrec_vnumero_documento,
                        ob.ncrec_sfecha_documento,
                        ob.vendc_icod_vendedor,
                        ob.tablc_iid_tipo_moneda,
                        ob.ncrec_nmonto_neto,
                        ob.ncrec_npor_imp_igv,
                        ob.ncrec_nmonto_total,
                        ob.ncrec_iid_situacion_credito,
                        ob.almac_icod_almacen,
                        ob.ncrec_tipo_cambio_fecha_doc_venta,
                        ob.ncrec_nmonto_iva,
                        ob.intUsuario,
                        ob.strPc,
                        ob.ncrec_nmonto_pagado,
                        ob.ncrec_icod_dxc,
                        ob.ncrec_bincluye_igv,
                        ob.ncrec_iclase_doc,
                        ob.ncrec_tipo_nota_credito,
                        ob.ncrec_vmotivo_sunat,
                        ob.ncrec_vdetalle);
                }
                return Convert.ToInt32(id_retencion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNotaCreditoClienteCab(ENotaCredito ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_CREDITO_VENTA_MODIFICAR(
                        ob.ncrec_icod_credito,
                        ob.ncrec_vnumero_credito,
                        ob.ncrec_sfecha_credito,
                        ob.cliec_icod_cliente,
                        ob.ncrec_ianio,
                        ob.ncrec_vreferencia,
                        ob.tdocc_icod_tipo_doc,
                        ob.tablc_iid_tipo_nota_credito_venta,
                        ob.tdodc_iid_correlativo,
                        ob.ncrec_vnumero_documento,
                        ob.ncrec_sfecha_documento,
                        ob.vendc_icod_vendedor,
                        ob.tablc_iid_tipo_moneda,
                        ob.ncrec_nmonto_neto,
                        ob.ncrec_npor_imp_igv,
                        ob.ncrec_nmonto_total,
                        ob.ncrec_iid_situacion_credito,
                        ob.almac_icod_almacen,
                        ob.ncrec_tipo_cambio_fecha_doc_venta,
                        ob.ncrec_nmonto_iva,
                        ob.intUsuario,
                        ob.strPc,
                        ob.ncrec_nmonto_pagado,
                        ob.ncrec_icod_dxc,
                        ob.ncrec_bincluye_igv,
                        ob.ncrec_iclase_doc,
                        ob.ncrec_vmotivo_sunat,
                        ob.ncrec_vdetalle);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaCreditoClienteCab(ENotaCredito ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_CREDITO_VENTA_ELIMINAR(
                        ob.ncrec_icod_credito,
                        ob.intUsuario,
                        ob.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void anularNotaCreditoClienteCab(ENotaCredito ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_CREDITO_VENTA_ANULAR(
                        ob.ncrec_icod_credito,
                        ob.intUsuario,
                        ob.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ENotaCreditoDet> listarNotaCreditoClienteDet(int intNotaCredito)
        {
            List<ENotaCreditoDet> lista = new List<ENotaCreditoDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_NOTA_CREDITO_DET_LISTAR(intNotaCredito);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaCreditoDet()
                        {
                            dcrec_icod_detalle_credito = item.dcrec_icod_detalle_credito,
                            ncrec_icod_credito = item.ncrec_icod_credito,
                            dcrec_inro_item = item.dcrec_inro_item,
                            dcrec_ncantidad_producto = item.dcrec_ncantidad_producto,
                            dcrec_nmonto_unitario = decimal.Round(item.dcrec_nmonto_unitario, 2),
                            dcrec_vcodigo_extremo_prod = item.dcrec_vcodigo_extremo_prod,
                            dcrec_vdescripcion = item.dcrec_vdescripcion,
                            dcrec_icod_serie = Convert.ToInt32(item.dcrec_icod_serie),
                            dcrec_rango_talla = item.dcrec_rango_talla,
                            dcrec_talla1 = item.dcrec_talla1,
                            dcrec_talla2 = item.dcrec_talla2,
                            dcrec_talla3 = item.dcrec_talla3,
                            dcrec_talla4 = item.dcrec_talla4,
                            dcrec_talla5 = item.dcrec_talla5,
                            dcrec_talla6 = item.dcrec_talla6,
                            dcrec_talla7 = item.dcrec_talla7,
                            dcrec_talla8 = item.dcrec_talla8,
                            dcrec_talla9 = item.dcrec_talla9,
                            dcrec_talla10 = item.dcrec_talla10,
                            dcrec_cant_talla1 = item.dcrec_cant_talla1,
                            dcrec_cant_talla2 = item.dcrec_cant_talla2,
                            dcrec_cant_talla3 = item.dcrec_cant_talla3,
                            dcrec_cant_talla4 = item.dcrec_cant_talla4,
                            dcrec_cant_talla5 = item.dcrec_cant_talla5,
                            dcrec_cant_talla6 = item.dcrec_cant_talla6,
                            dcrec_cant_talla7 = item.dcrec_cant_talla7,
                            dcrec_cant_talla8 = item.dcrec_cant_talla8,
                            dcrec_cant_talla9 = item.dcrec_cant_talla9,
                            dcrec_cant_talla10 = item.dcrec_cant_talla10,
                            dcrec_iid_kardex1 = item.dcrec_iid_kardex1,
                            dcrec_iid_kardex2 = item.dcrec_iid_kardex2,
                            dcrec_iid_kardex3 = item.dcrec_iid_kardex3,
                            dcrec_iid_kardex4 = item.dcrec_iid_kardex4,
                            dcrec_iid_kardex5 = item.dcrec_iid_kardex5,
                            dcrec_iid_kardex6 = item.dcrec_iid_kardex6,
                            dcrec_iid_kardex7 = item.dcrec_iid_kardex7,
                            dcrec_iid_kardex8 = item.dcrec_iid_kardex8,
                            dcrec_iid_kardex9 = item.dcrec_iid_kardex9,
                            dcrec_iid_kardex10 = item.dcrec_iid_kardex10,
                            dcrec_icod_producto1 = item.dcrec_icod_producto1,
                            dcrec_icod_producto2 = item.dcrec_icod_producto1,
                            dcrec_icod_producto3 = item.dcrec_icod_producto1,
                            dcrec_icod_producto4 = item.dcrec_icod_producto1,
                            dcrec_icod_producto5 = item.dcrec_icod_producto1,
                            dcrec_icod_producto6 = item.dcrec_icod_producto1,
                            dcrec_icod_producto7 = item.dcrec_icod_producto1,
                            dcrec_icod_producto8 = item.dcrec_icod_producto1,
                            dcrec_icod_producto9 = item.dcrec_icod_producto1,
                            dcrec_icod_producto10 = item.dcrec_icod_producto10,
                            dcrec_nmonto_item = item.dcrec_nmonto_item,
                            dcrec_nmonto_impuesto = item.dcrec_nmonto_impuesto,
                            prdc_icod_producto = item.prdc_icod_producto,
                            almac_icod_almacen = item.almac_icod_almacen,
                            tablc_iid_sit_item_nota_credito = item.tablc_iid_sit_item_nota_credito,
                            kardc_iid_correlativo = item.kardc_iid_correlativo,
                            //strCodProducto = item.strCodProducto,
                            //strDesProducto = item.strDesProducto,
                            strDesUM = item.strDesUM,
                            strAlmacen = item.strAlmacen,
                            strMoneda = item.strMoneda,
                            Serie = item.Serie,
                            CodigoSUNAT = item.CodigoSUNAT,
                            unidc_icod_unidad_medida = Convert.ToInt32(item.unidc_icod_unidad_medida),

                            NumeroOrdenItem = item.dcrec_inro_item,
                            cantidad = item.dcrec_ncantidad_producto,
                            unidadMedida = item.CodigoSUNAT,
                            ValorVentaItem = item.dcrec_nmonto_item,
                            CodMotivoDescuentoItem = 0,
                            FactorDescuentoItem = 0,
                            DescuentoItem = 0,
                            BaseDescuentotem = 0,
                            CodMotivoCargoItem = 0,
                            FactorCargoItem = 0,
                            MontoCargoItem = 0,
                            BaseCargoItem = 0,
                            MontoTotalImpuestosItem = Convert.ToDecimal(item.dcrec_nmonto_impuesto),
                            MontoImpuestoIgvItem = Convert.ToDecimal(item.dcrec_nmonto_impuesto),
                            MontoAfectoImpuestoIgv = Convert.ToDecimal(item.dcrec_nmonto_impuesto) == 0 ? 0 : Convert.ToDecimal((item.dcrec_nneto_igv)),
                            PorcentajeIGVItem = Convert.ToDecimal(18),
                            MontoImpuestoISCItem = 0,
                            MontoAfectoImpuestoIsc = 0,
                            PorcentajeISCtem = 0,
                            MontoImpuestoIVAPtem = 0,
                            MontoAfectoImpuestoIVAPItem = 0,
                            PorcentajeIVAPItem = 0,
                            descripcion = item.dcrec_vdescripcion,
                            codigoItem = item.dcrec_vcodigo_extremo_prod.ToString(),
                            ObservacionesItem = "",
                            ValorUnitarioItem = Math.Round((Convert.ToDecimal(item.dcrec_nneto_igv / item.dcrec_ncantidad_producto)), 4),
                            UMedida = item.UMedida


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
        public List<ENotaCreditoDet> listarNotaCreditoNoComercialClienteDet(int intNotaCredito)
        {
            List<ENotaCreditoDet> lista = new List<ENotaCreditoDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_NOTA_CREDITO_DET_LISTAR(intNotaCredito);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaCreditoDet()
                        {
                            dcrec_icod_detalle_credito = item.dcrec_icod_detalle_credito,
                            dcrec_inro_item = item.dcrec_inro_item,
                            dcrec_ncantidad_producto = item.dcrec_ncantidad_producto,
                            dcrec_nmonto_unitario = decimal.Round(item.dcrec_nmonto_unitario, 2),
                            dcrec_vcodigo_extremo_prod = item.dcrec_vcodigo_extremo_prod,
                            dcrec_vdescripcion = item.dcrec_vdescripcion,
                            dcrec_icod_serie = Convert.ToInt32(item.dcrec_icod_serie),
                            dcrec_rango_talla = item.dcrec_rango_talla,
                            dcrec_talla1 = item.dcrec_talla1,
                            dcrec_talla2 = item.dcrec_talla2,
                            dcrec_talla3 = item.dcrec_talla3,
                            dcrec_talla4 = item.dcrec_talla4,
                            dcrec_talla5 = item.dcrec_talla5,
                            dcrec_talla6 = item.dcrec_talla6,
                            dcrec_talla7 = item.dcrec_talla7,
                            dcrec_talla8 = item.dcrec_talla8,
                            dcrec_talla9 = item.dcrec_talla9,
                            dcrec_talla10 = item.dcrec_talla10,
                            dcrec_cant_talla1 = item.dcrec_cant_talla1,
                            dcrec_cant_talla2 = item.dcrec_cant_talla2,
                            dcrec_cant_talla3 = item.dcrec_cant_talla3,
                            dcrec_cant_talla4 = item.dcrec_cant_talla4,
                            dcrec_cant_talla5 = item.dcrec_cant_talla5,
                            dcrec_cant_talla6 = item.dcrec_cant_talla6,
                            dcrec_cant_talla7 = item.dcrec_cant_talla7,
                            dcrec_cant_talla8 = item.dcrec_cant_talla8,
                            dcrec_cant_talla9 = item.dcrec_cant_talla9,
                            dcrec_cant_talla10 = item.dcrec_cant_talla10,
                            dcrec_iid_kardex1 = item.dcrec_iid_kardex1,
                            dcrec_iid_kardex2 = item.dcrec_iid_kardex2,
                            dcrec_iid_kardex3 = item.dcrec_iid_kardex3,
                            dcrec_iid_kardex4 = item.dcrec_iid_kardex4,
                            dcrec_iid_kardex5 = item.dcrec_iid_kardex5,
                            dcrec_iid_kardex6 = item.dcrec_iid_kardex6,
                            dcrec_iid_kardex7 = item.dcrec_iid_kardex7,
                            dcrec_iid_kardex8 = item.dcrec_iid_kardex8,
                            dcrec_iid_kardex9 = item.dcrec_iid_kardex9,
                            dcrec_iid_kardex10 = item.dcrec_iid_kardex10,
                            dcrec_icod_producto1 = item.dcrec_icod_producto1,
                            dcrec_icod_producto2 = item.dcrec_icod_producto1,
                            dcrec_icod_producto3 = item.dcrec_icod_producto1,
                            dcrec_icod_producto4 = item.dcrec_icod_producto1,
                            dcrec_icod_producto5 = item.dcrec_icod_producto1,
                            dcrec_icod_producto6 = item.dcrec_icod_producto1,
                            dcrec_icod_producto7 = item.dcrec_icod_producto1,
                            dcrec_icod_producto8 = item.dcrec_icod_producto1,
                            dcrec_icod_producto9 = item.dcrec_icod_producto1,
                            dcrec_icod_producto10 = item.dcrec_icod_producto10,
                            dcrec_nmonto_item = item.dcrec_nmonto_item,
                            dcrec_nmonto_impuesto = item.dcrec_nmonto_impuesto,
                            prdc_icod_producto = item.prdc_icod_producto,
                            almac_icod_almacen = item.almac_icod_almacen,
                            tablc_iid_sit_item_nota_credito = item.tablc_iid_sit_item_nota_credito,
                            kardc_iid_correlativo = item.kardc_iid_correlativo,
                            //strCodProducto = item.strCodProducto,
                            //strDesProducto = item.strDesProducto,
                            strDesUM = item.strDesUM,
                            strAlmacen = item.strAlmacen,
                            strMoneda = item.strMoneda,
                            Serie = item.Serie,
                            CodigoSUNAT = item.CodigoSUNAT,
                            unidc_icod_unidad_medida = Convert.ToInt32(item.unidc_icod_unidad_medida),

                            NumeroOrdenItem = item.dcrec_inro_item,
                            cantidad = item.dcrec_ncantidad_producto,
                            unidadMedida = "ZZ",
                            ValorVentaItem = item.dcrec_nmonto_item,
                            CodMotivoDescuentoItem = 0,
                            FactorDescuentoItem = 0,
                            DescuentoItem = 0,
                            BaseDescuentotem = 0,
                            CodMotivoCargoItem = 0,
                            FactorCargoItem = 0,
                            MontoCargoItem = 0,
                            BaseCargoItem = 0,
                            MontoTotalImpuestosItem = Convert.ToDecimal(item.dcrec_nmonto_impuesto),
                            MontoImpuestoIgvItem = Convert.ToDecimal(item.dcrec_nmonto_impuesto),
                            MontoAfectoImpuestoIgv = Convert.ToDecimal(item.dcrec_nneto_igv),
                            PorcentajeIGVItem = Convert.ToDecimal(18),
                            MontoImpuestoISCItem = 0,
                            MontoAfectoImpuestoIsc = 0,
                            PorcentajeISCtem = 0,
                            MontoImpuestoIVAPtem = 0,
                            MontoAfectoImpuestoIVAPItem = 0,
                            PorcentajeIVAPItem = 0,
                            descripcion = item.dcrec_vdescripcion,
                            codigoItem = item.dcrec_inro_item.ToString(),
                            ObservacionesItem = "",
                            ValorUnitarioItem = Math.Round((Convert.ToDecimal(item.dcrec_nneto_igv / item.dcrec_ncantidad_producto)), 4),
                            UMedida = item.UMedida


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
        public void insertarNotaCreditoClienteDet(ENotaCreditoDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_CREDITO_DET_INSERTAR(
                        ob.ncrec_icod_credito,
                        ob.dcrec_inro_item,
                        ob.dcrec_ncantidad_producto,
                        ob.dcrec_nmonto_unitario,
                        ob.dcrec_vcodigo_extremo_prod,
                        ob.dcrec_vdescripcion,
                        ob.dcrec_icod_serie,
                        ob.dcrec_rango_talla,
                        ob.dcrec_talla1,
                        ob.dcrec_talla2,
                        ob.dcrec_talla3,
                        ob.dcrec_talla4,
                        ob.dcrec_talla5,
                        ob.dcrec_talla6,
                        ob.dcrec_talla7,
                        ob.dcrec_talla8,
                        ob.dcrec_talla9,
                        ob.dcrec_talla10,
                        ob.dcrec_cant_talla1,
                        ob.dcrec_cant_talla2,
                        ob.dcrec_cant_talla3,
                        ob.dcrec_cant_talla4,
                        ob.dcrec_cant_talla5,
                        ob.dcrec_cant_talla6,
                        ob.dcrec_cant_talla7,
                        ob.dcrec_cant_talla8,
                        ob.dcrec_cant_talla9,
                        ob.dcrec_cant_talla10,
                        ob.dcrec_iid_kardex1,
                        ob.dcrec_iid_kardex2,
                        ob.dcrec_iid_kardex3,
                        ob.dcrec_iid_kardex4,
                        ob.dcrec_iid_kardex5,
                        ob.dcrec_iid_kardex6,
                        ob.dcrec_iid_kardex7,
                        ob.dcrec_iid_kardex8,
                        ob.dcrec_iid_kardex9,
                        ob.dcrec_iid_kardex10,
                        ob.dcrec_icod_producto1,
                        ob.dcrec_icod_producto2,
                        ob.dcrec_icod_producto3,
                        ob.dcrec_icod_producto4,
                        ob.dcrec_icod_producto5,
                        ob.dcrec_icod_producto6,
                        ob.dcrec_icod_producto7,
                        ob.dcrec_icod_producto8,
                        ob.dcrec_icod_producto9,
                        ob.dcrec_icod_producto10,
                        ob.dcrec_nmonto_item,
                        ob.dcrec_nmonto_impuesto,
                        ob.prdc_icod_producto,
                        ob.almac_icod_almacen,
                        ob.tablc_iid_sit_item_nota_credito,
                        ob.kardc_iid_correlativo,
                        ob.intUsuario,
                        ob.strPc,
                        ob.unidc_icod_unidad_medida


                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNotaCreditoClienteDet(ENotaCreditoDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_CREDITO_DET_MODIFICAR(
                        ob.dcrec_icod_detalle_credito,
                        ob.ncrec_icod_credito,
                        ob.dcrec_inro_item,
                        ob.dcrec_ncantidad_producto,
                        ob.dcrec_nmonto_unitario,
                        ob.dcrec_vcodigo_extremo_prod,
                        ob.dcrec_vdescripcion,
                        ob.dcrec_icod_serie,
                        ob.dcrec_rango_talla,
                        ob.dcrec_talla1,
                        ob.dcrec_talla2,
                        ob.dcrec_talla3,
                        ob.dcrec_talla4,
                        ob.dcrec_talla5,
                        ob.dcrec_talla6,
                        ob.dcrec_talla7,
                        ob.dcrec_talla8,
                        ob.dcrec_talla9,
                        ob.dcrec_talla10,
                        ob.dcrec_cant_talla1,
                        ob.dcrec_cant_talla2,
                        ob.dcrec_cant_talla3,
                        ob.dcrec_cant_talla4,
                        ob.dcrec_cant_talla5,
                        ob.dcrec_cant_talla6,
                        ob.dcrec_cant_talla7,
                        ob.dcrec_cant_talla8,
                        ob.dcrec_cant_talla9,
                        ob.dcrec_cant_talla10,
                        ob.dcrec_iid_kardex1,
                        ob.dcrec_iid_kardex2,
                        ob.dcrec_iid_kardex3,
                        ob.dcrec_iid_kardex4,
                        ob.dcrec_iid_kardex5,
                        ob.dcrec_iid_kardex6,
                        ob.dcrec_iid_kardex7,
                        ob.dcrec_iid_kardex8,
                        ob.dcrec_iid_kardex9,
                        ob.dcrec_iid_kardex10,
                        ob.dcrec_icod_producto1,
                        ob.dcrec_icod_producto2,
                        ob.dcrec_icod_producto3,
                        ob.dcrec_icod_producto4,
                        ob.dcrec_icod_producto5,
                        ob.dcrec_icod_producto6,
                        ob.dcrec_icod_producto7,
                        ob.dcrec_icod_producto8,
                        ob.dcrec_icod_producto9,
                        ob.dcrec_icod_producto10,
                        ob.dcrec_nmonto_item,
                        ob.dcrec_nmonto_impuesto,
                        ob.prdc_icod_producto,
                        ob.almac_icod_almacen,
                        ob.tablc_iid_sit_item_nota_credito,
                        ob.kardc_iid_correlativo,
                        ob.intUsuario,
                        ob.strPc,
                        ob.unidc_icod_unidad_medida
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaCreditoClienteDet(ENotaCreditoDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_CREDITO_DET_ELIMINAR(
                        ob.dcrec_icod_detalle_credito,
                        ob.intUsuario,
                        ob.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Nota de Credito Suministros
        public List<ENotaCreditoSuministrosDet> listarNotaCreditoSuministrosClienteDet(int intNotaCredito)
        {
            List<ENotaCreditoSuministrosDet> lista = new List<ENotaCreditoSuministrosDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_NOTA_CREDITO_MP_DET_LISTAR(intNotaCredito);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaCreditoSuministrosDet()
                        {
                            dcrec_icod_detalle_credito = item.dcrec_icod_detalle_credito,
                            dcrec_inro_item = item.dcrec_inro_item,
                            dcrec_ncantidad_producto = item.dcrec_ncantidad_producto,
                            dcrec_nmonto_unitario = decimal.Round(item.dcrec_nmonto_unitario, 2),
                            dcrec_vdescripcion = item.dcrec_vdescripcion,
                            dcrec_nmonto_item = item.dcrec_nmonto_item,
                            dcrec_nmonto_impuesto = item.dcrec_nmonto_impuesto,
                            prdc_icod_producto = item.prdc_icod_producto,
                            almac_icod_almacen = item.almac_icod_almacen,
                            tablc_iid_sit_item_nota_credito = item.tablc_iid_sit_item_nota_credito,
                            kardc_iid_correlativo = item.kardc_iid_correlativo,
                            strCodProducto = item.strCodProducto,
                            strDesProducto = item.strDesProducto,
                            strDesUM = item.strDesUM,
                            strAlmacen = item.strAlmacen,
                            strMoneda = item.strMoneda,
                            CodigoSUNAT = item.CodigoSUNAT,
                            unidc_icod_unidad_medida = Convert.ToInt32(item.unidc_icod_unidad_medida),

                            NumeroOrdenItem = item.dcrec_inro_item,
                            cantidad = item.dcrec_ncantidad_producto,
                            unidadMedida = item.CodigoSUNAT,
                            ValorVentaItem = item.dcrec_nmonto_item,
                            CodMotivoDescuentoItem = 0,
                            FactorDescuentoItem = 0,
                            DescuentoItem = 0,
                            BaseDescuentotem = 0,
                            CodMotivoCargoItem = 0,
                            FactorCargoItem = 0,
                            MontoCargoItem = 0,
                            BaseCargoItem = 0,
                            MontoTotalImpuestosItem = Convert.ToDecimal(item.dcrec_nmonto_impuesto),
                            MontoImpuestoIgvItem = Convert.ToDecimal(item.dcrec_nmonto_impuesto),
                            MontoAfectoImpuestoIgv = Convert.ToDecimal(item.dcrec_nneto_igv),
                            PorcentajeIGVItem = Convert.ToDecimal(18),
                            MontoImpuestoISCItem = 0,
                            MontoAfectoImpuestoIsc = 0,
                            PorcentajeISCtem = 0,
                            MontoImpuestoIVAPtem = 0,
                            MontoAfectoImpuestoIVAPItem = 0,
                            PorcentajeIVAPItem = 0,
                            descripcion = item.dcrec_vdescripcion,
                            codigoItem = item.strCodProducto,
                            ObservacionesItem = "",
                            ValorUnitarioItem = Math.Round((Convert.ToDecimal(item.dcrec_nneto_igv / item.dcrec_ncantidad_producto)), 4),
                            UMedida = item.UMedida


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
        public void insertarNotaCreditoSuministrosClienteDet(ENotaCreditoSuministrosDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_CREDITO_MP_DET_INSERTAR(
                        ob.ncrec_icod_credito,
                        ob.dcrec_inro_item,
                        ob.dcrec_ncantidad_producto,
                        ob.dcrec_nmonto_unitario,
                        ob.dcrec_vdescripcion,
                        ob.dcrec_nmonto_item,
                        ob.dcrec_nmonto_impuesto,
                        ob.prdc_icod_producto,
                        ob.almac_icod_almacen,
                        ob.tablc_iid_sit_item_nota_credito,
                        ob.kardc_iid_correlativo,
                        ob.intUsuario,
                        ob.strPc,
                        ob.unidc_icod_unidad_medida,
                        ob.dcrec_nneto_igv


                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNotaCreditoSuministrosClienteDet(ENotaCreditoSuministrosDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_CREDITO_MP_DET_MODIFICAR(
                        ob.dcrec_icod_detalle_credito,
                        ob.ncrec_icod_credito,
                        ob.dcrec_inro_item,
                        ob.dcrec_ncantidad_producto,
                        ob.dcrec_nmonto_unitario,
                        ob.dcrec_vdescripcion,
                        ob.dcrec_nmonto_item,
                        ob.dcrec_nmonto_impuesto,
                        ob.prdc_icod_producto,
                        ob.almac_icod_almacen,
                        ob.tablc_iid_sit_item_nota_credito,
                        ob.kardc_iid_correlativo,
                        ob.intUsuario,
                        ob.strPc,
                        ob.unidc_icod_unidad_medida,
                        ob.dcrec_nneto_igv
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaCreditoClienteSuministrosDet(ENotaCreditoSuministrosDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_CREDITO_MP_DET_ELIMINAR(
                        ob.dcrec_icod_detalle_credito,
                        ob.intUsuario,
                        ob.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Nota Debito Ventas
        public List<ENotaDebito> listarNotaDebitoClienteCab(int intEjericio)
        {
            List<ENotaDebito> lista = new List<ENotaDebito>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_NOTA_DEBITO_VENTA_LISTAR(intEjericio);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaDebito()
                        {
                            ncrec_icod_credito = item.ncrec_icod_credito,
                            ncrec_vnumero_credito = item.ncrec_vnumero_credito,
                            ncrec_sfecha_credito = item.ncrec_sfecha_credito,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            ncrec_ianio = Convert.ToInt32(item.ncrec_ianio),
                            ncrec_vreferencia = item.ncrec_vreferencia,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            tablc_iid_tipo_nota_credito_venta = item.tablc_iid_tipo_nota_credito_venta,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo,
                            ncrec_vnumero_documento = item.ncrec_vnumero_documento,
                            ncrec_sfecha_documento = item.ncrec_sfecha_documento,
                            vendc_icod_vendedor = item.vendc_icod_vendedor,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            ncrec_nmonto_neto = item.ncrec_nmonto_neto,
                            ncrec_npor_imp_igv = item.ncrec_npor_imp_igv,
                            ncrec_nmonto_total = item.ncrec_nmonto_total,
                            ncrec_iid_situacion_credito = Convert.ToInt32(item.ncrec_iid_situacion_credito),
                            almac_icod_almacen = item.almac_icod_almacen,
                            ncrec_tipo_cambio_fecha_doc_venta = item.ncrec_tipo_cambio_fecha_doc_venta,
                            ncrec_nmonto_pagado = item.ncrec_nmonto_pagado,
                            strSituacion = item.strSituacion,
                            strDesCliente = item.strDesCliente,
                            strRuc = item.strRuc,
                            DireccionCliente = item.DireccionCliente,
                            strTipoDoc = item.strTipoDoc,
                            strMoneda = item.strMoneda,
                            ncrec_icod_dxc = Convert.ToInt64(item.ncrec_icod_dxc),
                            ncrec_bincluye_igv = item.ncrec_bincluye_igv,
                            ubicc_icod_ubicacion = Convert.ToInt32(item.ubicc_icod_ubicacion),
                            ncrec_iclase_doc = Convert.ToInt32(item.ncrec_iclase_doc),
                            StrClaseDocumento = item.StrClaseDocumento,
                            ncrec_tipo_nota_credito = Convert.ToInt32(item.ncrec_tipo_nota_credito),
                            ncrec_vmotivo_sunat = item.ncrec_vmotivo_sunat,
                            doc_icod_documento = item.ncrec_icod_credito,
                            ncrec_vdetalle = item.ncrec_vdetalle
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
        public int insertarNotaDebitoClienteCab(ENotaDebito ob)
        {
            int? id_retencion = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_DEBITO_VENTA_INSERTAR(
                        ref id_retencion,
                        ob.ncrec_vnumero_credito,
                        ob.ncrec_sfecha_credito,
                        ob.cliec_icod_cliente,
                        ob.ncrec_ianio,
                        ob.ncrec_vreferencia,
                        ob.tdocc_icod_tipo_doc,
                        ob.tablc_iid_tipo_nota_credito_venta,
                        ob.tdodc_iid_correlativo,
                        ob.ncrec_vnumero_documento,
                        ob.ncrec_sfecha_documento,
                        ob.vendc_icod_vendedor,
                        ob.tablc_iid_tipo_moneda,
                        ob.ncrec_nmonto_neto,
                        ob.ncrec_npor_imp_igv,
                        ob.ncrec_nmonto_total,
                        ob.ncrec_iid_situacion_credito,
                        ob.almac_icod_almacen,
                        ob.ncrec_tipo_cambio_fecha_doc_venta,
                        ob.ncrec_nmonto_iva,
                        ob.intUsuario,
                        ob.strPc,
                        ob.ncrec_nmonto_pagado,
                        ob.ncrec_icod_dxc,
                        ob.ncrec_bincluye_igv,
                        ob.ncrec_iclase_doc,
                        ob.ncrec_tipo_nota_credito,
                        ob.ncrec_vmotivo_sunat,
                        ob.ncrec_vdetalle);
                }
                return Convert.ToInt32(id_retencion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNotaDebitoClienteCab(ENotaDebito ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_DEBITO_VENTA_MODIFICAR(
                        ob.ncrec_icod_credito,
                        ob.ncrec_vnumero_credito,
                        ob.ncrec_sfecha_credito,
                        ob.cliec_icod_cliente,
                        ob.ncrec_ianio,
                        ob.ncrec_vreferencia,
                        ob.tdocc_icod_tipo_doc,
                        ob.tablc_iid_tipo_nota_credito_venta,
                        ob.tdodc_iid_correlativo,
                        ob.ncrec_vnumero_documento,
                        ob.ncrec_sfecha_documento,
                        ob.vendc_icod_vendedor,
                        ob.tablc_iid_tipo_moneda,
                        ob.ncrec_nmonto_neto,
                        ob.ncrec_npor_imp_igv,
                        ob.ncrec_nmonto_total,
                        ob.ncrec_iid_situacion_credito,
                        ob.almac_icod_almacen,
                        ob.ncrec_tipo_cambio_fecha_doc_venta,
                        ob.ncrec_nmonto_iva,
                        ob.intUsuario,
                        ob.strPc,
                        ob.ncrec_nmonto_pagado,
                        ob.ncrec_icod_dxc,
                        ob.ncrec_bincluye_igv,
                        ob.ncrec_iclase_doc,
                        ob.ncrec_vmotivo_sunat,
                        ob.ncrec_vdetalle);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaDebitoClienteCab(ENotaDebito ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_DEBITO_VENTA_ELIMINAR(
                        ob.ncrec_icod_credito,
                        ob.intUsuario,
                        ob.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ENotaDebitoDet> listarNotaDebitoClienteDet(int intNotaCredito)
        {
            List<ENotaDebitoDet> lista = new List<ENotaDebitoDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_NOTA_DEBITO_DET_LISTAR(intNotaCredito);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaDebitoDet()
                        {
                            dcrec_icod_detalle_credito = item.dcrec_icod_detalle_credito,
                            dcrec_inro_item = item.dcrec_inro_item,
                            dcrec_ncantidad_producto = item.dcrec_ncantidad_producto,
                            dcrec_nmonto_unitario = decimal.Round(item.dcrec_nmonto_unitario, 2),
                            dcrec_vcodigo_extremo_prod = item.dcrec_vcodigo_extremo_prod,
                            dcrec_vdescripcion = item.dcrec_vdescripcion,
                            dcrec_nmonto_item = item.dcrec_nmonto_item,
                            dcrec_nmonto_impuesto = item.dcrec_nmonto_impuesto,
                            prdc_icod_producto = item.prdc_icod_producto,
                            almac_icod_almacen = item.almac_icod_almacen,
                            tablc_iid_sit_item_nota_credito = item.tablc_iid_sit_item_nota_credito,
                            kardc_iid_correlativo = item.kardc_iid_correlativo,
                            //strCodProducto = item.strCodProducto,
                            //strDesProducto = item.strDesProducto,
                            strDesUM = item.strDesUM,
                            strAlmacen = item.strAlmacen,
                            strMoneda = item.strMoneda,
                            unidc_icod_unidad_medida = Convert.ToInt32(item.unidc_icod_unidad_medida),
                            dcrec_nneto_igv = Convert.ToDecimal(item.dcrec_nneto_igv),


                            NumeroOrdenItem = item.dcrec_inro_item,
                            cantidad = item.dcrec_ncantidad_producto,
                            unidadMedida = "ZZ",
                            ValorVentaItem = item.dcrec_nmonto_item,
                            CodMotivoDescuentoItem = 0,
                            FactorDescuentoItem = 0,
                            DescuentoItem = 0,
                            BaseDescuentotem = 0,
                            CodMotivoCargoItem = 0,
                            FactorCargoItem = 0,
                            MontoCargoItem = 0,
                            BaseCargoItem = 0,
                            MontoTotalImpuestosItem = Convert.ToDecimal(item.dcrec_nmonto_impuesto),
                            MontoImpuestoIgvItem = Convert.ToDecimal(item.dcrec_nmonto_impuesto),
                            MontoAfectoImpuestoIgv = Convert.ToDecimal(item.dcrec_nneto_igv),
                            PorcentajeIGVItem = Convert.ToDecimal(18),
                            MontoImpuestoISCItem = 0,
                            MontoAfectoImpuestoIsc = 0,
                            PorcentajeISCtem = 0,
                            MontoImpuestoIVAPtem = 0,
                            MontoAfectoImpuestoIVAPItem = 0,
                            PorcentajeIVAPItem = 0,
                            descripcion = item.dcrec_vdescripcion,
                            codigoItem = item.dcrec_inro_item.ToString(),
                            ObservacionesItem = "",
                            ValorUnitarioItem = Math.Round((Convert.ToDecimal(item.dcrec_nneto_igv / item.dcrec_ncantidad_producto)), 4),
                            UMedida = item.UMedida
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
        public void insertarNotaDebitoClienteDet(ENotaDebitoDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_DEBITO_DET_INSERTAR(
                        ob.ncrec_icod_credito,
                        ob.dcrec_inro_item,
                        ob.dcrec_ncantidad_producto,
                        ob.dcrec_nmonto_unitario,
                        ob.dcrec_vcodigo_extremo_prod,
                        ob.dcrec_vdescripcion,
                        ob.dcrec_nmonto_item,
                        ob.dcrec_nmonto_impuesto,
                        ob.prdc_icod_producto,
                        ob.almac_icod_almacen,
                        ob.tablc_iid_sit_item_nota_credito,
                        ob.kardc_iid_correlativo,
                        ob.intUsuario,
                        ob.strPc,
                        ob.unidc_icod_unidad_medida,
                        ob.dcrec_nneto_igv
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNotaDebitoClienteDet(ENotaDebitoDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_DEBITO_DET_MODIFICAR(
                        ob.dcrec_icod_detalle_credito,
                        ob.ncrec_icod_credito,
                        ob.dcrec_inro_item,
                        ob.dcrec_ncantidad_producto,
                        ob.dcrec_nmonto_unitario,
                        ob.dcrec_vcodigo_extremo_prod,
                        ob.dcrec_vdescripcion,
                        ob.dcrec_nmonto_item,
                        ob.dcrec_nmonto_impuesto,
                        ob.prdc_icod_producto,
                        ob.almac_icod_almacen,
                        ob.tablc_iid_sit_item_nota_credito,
                        ob.kardc_iid_correlativo,
                        ob.intUsuario,
                        ob.strPc,
                        ob.unidc_icod_unidad_medida,
                        ob.dcrec_nneto_igv
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaDebitoClienteDet(ENotaDebitoDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_DEBITO_DET_ELIMINAR(
                        ob.dcrec_icod_detalle_credito,
                        ob.intUsuario,
                        ob.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        #region Tipo Tarjeta
        public List<ETipoTarjeta> listarTipoTarjeta()
        {
            List<ETipoTarjeta> lista = new List<ETipoTarjeta>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_TIPO_TARJETA_CREDITO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ETipoTarjeta()
                        {
                            tcrc_icod_tipo_tarjeta_cred = item.tcrc_icod_tipo_tarjeta_cred,
                            tcrc_iid_tipo_tarjeta_cred = Convert.ToInt32(item.tcrc_iid_tipo_tarjeta_cred),
                            tcrc_vdescripcion_tipo_tarjeta_cred = item.tcrc_vdescripcion_tipo_tarjeta_cred,
                            tcrc_nporcentaje_comision = Convert.ToDecimal(item.tcrc_nporcentaje_comision),
                            bcoc_icod_banco = Convert.ToInt32(item.bcoc_icod_banco),
                            bcod_icod_banco_cuenta = Convert.ToInt32(item.bcod_icod_banco_cuenta),
                            strDesBanco = item.strDesBanco,
                            strNroCuenta = item.strNroCuenta
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
        public int insertarTipoTarjeta(ETipoTarjeta ob)
        {
            int? id_anticipo = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TIPO_TARJETA_CREDITO_INSERTAR(
                            ref id_anticipo,
                            ob.tcrc_iid_tipo_tarjeta_cred,
                            ob.tcrc_vdescripcion_tipo_tarjeta_cred,
                            ob.tcrc_nporcentaje_comision,
                            ob.bcoc_icod_banco,
                            ob.bcod_icod_banco_cuenta,
                            ob.intUsuario,
                            ob.strPc
                           );
                }
                return Convert.ToInt32(id_anticipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTipoTarjeta(ETipoTarjeta ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TIPO_TARJETA_CREDITO_MODIFICAR(
                            ob.tcrc_icod_tipo_tarjeta_cred,
                            ob.tcrc_iid_tipo_tarjeta_cred,
                            ob.tcrc_vdescripcion_tipo_tarjeta_cred,
                            ob.tcrc_nporcentaje_comision,
                            ob.bcoc_icod_banco,
                            ob.bcod_icod_banco_cuenta,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTipoTarjeta(ETipoTarjeta ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TIPO_TARJETA_CREDITO_ELIMINAR(
                            ob.tcrc_icod_tipo_tarjeta_cred,
                            ob.intUsuario,
                            ob.strPc);
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
            List<ETransportista> lista = new List<ETransportista>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_TRANSPORTISTA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ETransportista()
                        {
                            tranc_icod_transportista = item.tranc_icod_transportista,
                            tranc_iid_transportista = item.tranc_iid_transportista,
                            tranc_vnombre_transportista = item.tranc_vnombre_transportista,
                            tranc_vruc = item.tranc_vruc,
                            tranc_vdireccion = item.tranc_vdireccion,
                            tranc_vnumero_telefono = item.tranc_vnumero_telefono,
                            tranc_vmicti = item.tranc_vmicti,
                            tranc_cnumero_dni = item.tranc_cnumero_dni,
                            tranc_vnombre_conductor = item.tranc_vnombre_conductor,
                            tranc_vnum_licencia_conducir = item.tranc_vnum_licencia_conducir,
                            tranc_vnum_placa = item.tranc_vnum_placa,
                            tranc_vnum_matricula = item.tranc_vnum_matricula,
                            tranc_iid_situacion_transporte = Convert.ToInt32(item.tranc_iid_situacion_transporte),
                            strSituacion = item.strSituacion
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
        public int insertarTransportista(ETransportista ob)
        {
            int? id_transportista = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TRANSPORTISTA_INSERTAR(
                            ref id_transportista,
                            ob.tranc_iid_transportista,
                            ob.tranc_vnombre_transportista,
                            ob.tranc_vruc,
                            ob.tranc_vdireccion,
                            ob.tranc_vnumero_telefono,
                            ob.tranc_vmicti,
                            ob.tranc_cnumero_dni,
                            ob.tranc_vnombre_conductor,
                            ob.tranc_vnum_licencia_conducir,
                            ob.tranc_vnum_placa,
                            ob.tranc_vnum_matricula,
                            ob.tranc_iid_situacion_transporte,
                            ob.intUsuario,
                            ob.strPc
                           );
                }
                return Convert.ToInt32(id_transportista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTransportista(ETransportista ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TRANSPORTISTA_MODIFICAR(
                            ob.tranc_icod_transportista,
                            ob.tranc_iid_transportista,
                            ob.tranc_vnombre_transportista,
                            ob.tranc_vruc,
                            ob.tranc_vdireccion,
                            ob.tranc_vnumero_telefono,
                            ob.tranc_vmicti,
                            ob.tranc_cnumero_dni,
                            ob.tranc_vnombre_conductor,
                            ob.tranc_vnum_licencia_conducir,
                            ob.tranc_vnum_placa,
                            ob.tranc_vnum_matricula,
                            ob.tranc_iid_situacion_transporte,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTransportista(ETransportista ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TRANSPORTISTA_ELIMINAR(
                            ob.tranc_icod_transportista,
                            ob.intUsuario,
                            ob.strPc);
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
            EGuiaRemision _be = new EGuiaRemision();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_GUIA_REMISION_LISTAR_X_ID(remic_icod_remision);
                    foreach (var item in query)
                    {
                        _be.remic_icod_remision = item.remic_icod_remision;
                        _be.remic_vnumero_remision = item.remic_vnumero_remision;
                        _be.remic_sfecha_remision = item.remic_sfecha_remision;
                        _be.cliec_icod_cliente = Convert.ToInt32(item.cliec_icod_cliente);
                        _be.remic_vnombre_destinatario = item.remic_vnombre_destinatario;
                        _be.remic_vdireccion_destinatario = item.remic_vdireccion_destinatario;
                        _be.remic_vruc_destinatario = item.remic_vruc_destinatario;
                        _be.remic_vdireccion_procedencia = item.remic_vdireccion_procedencia;
                        _be.almac_icod_almacen = item.almac_icod_almacen;
                        _be.tablc_iid_motivo = Convert.ToInt32(item.tablc_iid_motivo);
                        _be.tranc_icod_transportista = Convert.ToInt32(item.tranc_icod_transportista);
                        _be.remic_vlicencia = item.remic_vlicencia;
                        _be.remic_vruc_transportista = item.remic_vruc_transportista;
                        _be.tablc_iid_situacion_remision = Convert.ToInt32(item.tablc_iid_situacion_remision);
                        _be.remic_vreferencia = item.remic_vreferencia;
                        _be.strDesAlmacen = item.strDesAlmacen;
                        _be.strTransportista = item.strTransportista;
                        _be.StrSitucion = item.StrSitucion;
                        _be.remic_sfecha_inicio = Convert.ToDateTime(item.remic_sfecha_inicio);
                        _be.almac_icod_almacen_ingreso = item.almac_icod_almacen_ingreso;
                        _be.strDesAlmaceningreso = item.strDesAlmaceningreso;
                        _be.remic_vmarca_placa = item.remic_vmarca_placa;
                        _be.remic_vcertif_inscripcion = item.remic_vcertif_inscripcion;
                        _be.remic_vubigeo_destino = item.remic_vubigeo_destino;
                        _be.remic_vdescripcion_motivo = item.remic_vdescripcion_motivo;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _be;
        }
        public List<EGuiaRemision> listarGuiaRemision(int intEjericio)
        {
            List<EGuiaRemision> lista = new List<EGuiaRemision>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_GUIA_REMISION_LISTAR(intEjericio);
                    foreach (var item in query)
                    {
                        lista.Add(new EGuiaRemision()
                        {
                            remic_icod_remision = item.remic_icod_remision,
                            remic_vnumero_remision = item.remic_vnumero_remision,
                            remic_sfecha_remision = item.remic_sfecha_remision,
                            cliec_icod_cliente = Convert.ToInt32(item.cliec_icod_cliente),
                            NomClie = item.NomClie,
                            remic_vnombre_destinatario = item.remic_vnombre_destinatario,
                            remic_vdireccion_destinatario = item.remic_vdireccion_destinatario,
                            remic_vruc_destinatario = item.remic_vruc_destinatario,
                            remic_vdireccion_procedencia = item.remic_vdireccion_procedencia,
                            almac_icod_almacen = item.almac_icod_almacen,
                            tablc_iid_motivo = Convert.ToInt32(item.tablc_iid_motivo),
                            tranc_icod_transportista = Convert.ToInt32(item.tranc_icod_transportista),
                            remic_vlicencia = item.remic_vlicencia,
                            remic_vruc_transportista = item.remic_vruc_transportista,
                            tablc_iid_situacion_remision = Convert.ToInt32(item.tablc_iid_situacion_remision),
                            remic_vreferencia = item.remic_vreferencia,
                            strDesAlmacen = item.strDesAlmacen,
                            strTransportista = item.strTransportista,
                            StrSitucion = item.StrSitucion,
                            ubicc_icod_ubicacion = Convert.ToInt32(item.ubicc_icod_ubicacion),
                            remic_sfecha_inicio = Convert.ToDateTime(item.remic_sfecha_inicio),
                            almac_icod_almacen_ingreso = item.almac_icod_almacen_ingreso,
                            strDesAlmaceningreso = item.strDesAlmaceningreso,
                            cliec_vcod_cliente = item.cliec_vcod_cliente,
                            cecoc_icod_centro_costo = Convert.ToInt32(item.cecoc_icod_centro_costo),
                            CentroCosto = item.CentroCosto,
                            CodProyecto = item.CodProyecto,
                            remic_vmarca_placa = item.remic_vmarca_placa,
                            remic_vcertif_inscripcion = item.remic_vcertif_inscripcion,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            remic_num_comprobante = item.remic_num_comprobante,
                            remic_inum_cajas = Convert.ToInt32(item.remic_inum_cajas),
                            remic_itotal_pares = Convert.ToInt32(item.remic_itotal_pares),
                            remic_itipo_guia = Convert.ToInt32(item.remic_itipo_guia),
                            FechaEnvio = item.FechaEnvio,
                            Exito = Convert.ToBoolean(item.Exito),
                            MensajeError = item.MensajeError,
                            NumeroTicket = item.NumeroTicket,
                            ZimpCdr = item.ZimpCdr,
                            EstadoSunat = item.EstadoSunat,
                            LinkDescarga = item.LinkDescarga,
                            remic_vubigeo_destino = item.remic_vubigeo_destino,
                            remic_vdescripcion_motivo = item.remic_vdescripcion_motivo
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


        public List<EGuiaRemision> listarGuiaRemisionElectronica(DateTime date)
        {
            List<EGuiaRemision> lista = new List<EGuiaRemision>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_GUIA_REMISION_ELECTRONICO_LISTAR(date);
                    foreach (var item in query)
                    {
                        lista.Add(new EGuiaRemision()
                        {
                            remic_icod_remision = Convert.ToInt32(item.remic_icod_remision),
                            remic_vnumero_remision = item.remic_vnumero_remision,
                            remic_sfecha_remision = Convert.ToDateTime(item.remic_sfecha_remision),
                            cliec_icod_cliente = Convert.ToInt32(item.cliec_icod_cliente),
                            NomClie = item.NomClie,
                            remic_vnombre_destinatario = item.remic_vnombre_destinatario,
                            remic_vdireccion_destinatario = item.remic_vdireccion_destinatario,
                            remic_vruc_destinatario = item.remic_vruc_destinatario,
                            remic_vdireccion_procedencia = item.remic_vdireccion_procedencia,
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            tablc_iid_motivo = Convert.ToInt32(item.tablc_iid_motivo),
                            tranc_icod_transportista = Convert.ToInt32(item.tranc_icod_transportista),
                            remic_vlicencia = item.remic_vlicencia,
                            remic_vruc_transportista = item.remic_vruc_transportista,
                            tablc_iid_situacion_remision = Convert.ToInt32(item.tablc_iid_situacion_remision),
                            remic_vreferencia = item.remic_vreferencia,
                            strDesAlmacen = item.strDesAlmacen,
                            strTransportista = item.strTransportista,
                            StrSitucion = item.StrSitucion,
                            ubicc_icod_ubicacion = Convert.ToInt32(item.ubicc_icod_ubicacion),
                            remic_sfecha_inicio = Convert.ToDateTime(item.remic_sfecha_inicio),
                            almac_icod_almacen_ingreso = item.almac_icod_almacen_ingreso,
                            strDesAlmaceningreso = item.strDesAlmaceningreso,
                            cliec_vcod_cliente = item.cliec_vcod_cliente,
                            cecoc_icod_centro_costo = Convert.ToInt32(item.cecoc_icod_centro_costo),
                            CentroCosto = item.CentroCosto,
                            CodProyecto = item.CodProyecto,
                            remic_vmarca_placa = item.remic_vmarca_placa,
                            remic_vcertif_inscripcion = item.remic_vcertif_inscripcion,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            remic_num_comprobante = item.remic_num_comprobante,
                            remic_inum_cajas = Convert.ToInt32(item.remic_inum_cajas),
                            remic_itotal_pares = Convert.ToInt32(item.remic_itotal_pares),
                            remic_itipo_guia = Convert.ToInt32(item.remic_itipo_guia),
                            FechaEnvio = item.FechaEnvio,
                            Exito = Convert.ToBoolean(item.Exito),
                            MensajeError = item.MensajeError,
                            NumeroTicket = item.NumeroTicket,
                            ZimpCdr = item.ZimpCdr,
                            EstadoSunat = item.EstadoSunat,
                            LinkDescarga = item.LinkDescarga,
                            remic_vubigeo_destino = item.remic_vubigeo_destino,
                            remic_vdescripcion_motivo = item.remic_vdescripcion_motivo
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
        public List<EGuiaRemision> getGRCabVerificarNumero(string vnumero)
        {
            List<EGuiaRemision> lista = new List<EGuiaRemision>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_GR_CAB_VERIFICAR_NUEMRO(vnumero);
                    foreach (var item in query)
                    {
                        lista.Add(new EGuiaRemision()
                        {
                            remic_icod_remision = item.remic_icod_remision

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
        public int insertarGuiaRemision(EGuiaRemision ob)
        {
            int? id_guia_remision = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_GUIA_REMISION_INSERTAR(
                        ref id_guia_remision,
                        ob.remic_vnumero_remision,
                        ob.remic_sfecha_remision,
                        ob.cliec_icod_cliente,
                        ob.remic_vnombre_destinatario,
                        ob.remic_vdireccion_destinatario,
                        ob.remic_vruc_destinatario,
                        ob.remic_vdireccion_procedencia,
                        ob.almac_icod_almacen,
                        ob.tablc_iid_motivo,
                        ob.tranc_icod_transportista,
                        ob.remic_vlicencia,
                        ob.remic_vruc_transportista,
                        ob.tablc_iid_situacion_remision,
                        ob.remic_vreferencia,
                        ob.intUsuario,
                        ob.strPc,
                        ob.remic_sfecha_inicio,
                        ob.almac_icod_almacen_ingreso,
                        ob.pryc_icod_proyecto,
                        ob.cecoc_icod_centro_costo,
                        ob.remic_vmarca_placa,
                        ob.remic_vcertif_inscripcion,
                        ob.tdocc_vabreviatura_tipo_doc,
                        ob.remic_num_comprobante,
                        ob.remic_inum_cajas,
                        ob.remic_itotal_pares,
                        ob.remic_itipo_guia,
                        ob.remic_vubigeo_destino,
                        ob.remic_vdescripcion_motivo
                        );
                }
                return Convert.ToInt32(id_guia_remision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGuiaRemision(EGuiaRemision ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_GUIA_REMISION_MODIFICAR(
                            ob.remic_icod_remision,
                            ob.remic_vnumero_remision,
                            ob.remic_sfecha_remision,
                            ob.cliec_icod_cliente,
                            ob.remic_vnombre_destinatario,
                            ob.remic_vdireccion_destinatario,
                            ob.remic_vruc_destinatario,
                            ob.remic_vdireccion_procedencia,
                            ob.almac_icod_almacen,
                            ob.tablc_iid_motivo,
                            ob.tranc_icod_transportista,
                            ob.remic_vlicencia,
                            ob.remic_vruc_transportista,
                            ob.tablc_iid_situacion_remision,
                            ob.remic_vreferencia,
                            ob.intUsuario,
                            ob.strPc,
                            ob.remic_sfecha_inicio,
                            ob.almac_icod_almacen_ingreso,
                            ob.pryc_icod_proyecto,
                            ob.cecoc_icod_centro_costo,
                            ob.remic_vmarca_placa,
                            ob.remic_vcertif_inscripcion,
                            ob.tdocc_vabreviatura_tipo_doc,
                            ob.remic_num_comprobante,
                            ob.remic_inum_cajas,
                            ob.remic_itotal_pares,
                            ob.remic_vubigeo_destino,
                            ob.remic_vdescripcion_motivo
                           );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGuiaRemision_Situ_Tipo_Doc(int remic_icod_remision, int tablc_iid_situacion_remision, int intUsuario, string strPc)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_GUIA_REMISION_MODIFICAR_SITUACION_AND_TIPO_DOC(
                            remic_icod_remision,
                            tablc_iid_situacion_remision,
                            intUsuario,
                            strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarGuiaRemision(EGuiaRemision ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_GUIA_REMISION_ELIMINAR(
                            ob.remic_icod_remision,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void anularGuiaRemision(EGuiaRemision ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_GUIA_REMISION_ANULAR(
                            ob.remic_icod_remision
                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EGuiaRemisionDet> listarGuiaRemisionDet(int remic_icod_remision, int intEjericio)
        {
            List<EGuiaRemisionDet> lista = new List<EGuiaRemisionDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_GUIA_REMISION_DET_LISTAR(remic_icod_remision, intEjericio);
                    foreach (var item in query)
                    {
                        lista.Add(new EGuiaRemisionDet()
                        {
                            dremc_icod_detalle_remision = item.dremc_icod_detalle_remision,
                            remic_icod_remision = Convert.ToInt32(item.remic_icod_remision),
                            dremc_inro_item = item.dremc_inro_item,
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            dremc_vcodigo_extremo_prod = item.dremc_vcodigo_extremo_prod,
                            dremc_vdescripcion_prod = item.dremc_vdescripcion_prod,
                            dremc_ncantidad_producto = item.dremc_ncantidad_producto,
                            dremc_vcantidad_producto = Convert.ToInt32(item.dremc_ncantidad_producto).ToString(),
                            kardc_icod_correlativo = item.kardc_icod_correlativo,
                            dremc_itipo_producto = item.dremc_itipo_producto,
                            dremc_imodelo = Convert.ToInt32(item.dremc_imodelo),
                            dremc_icolor = Convert.ToInt32(item.dremc_icolor),
                            dremc_imarca = Convert.ToInt32(item.dremc_imarca),
                            dremc_itipo = Convert.ToInt32(item.dremc_itipo),
                            dremc_iserie = Convert.ToInt32(item.dremc_iserie),
                            dremc_rango_talla = item.dremc_rango_talla,
                            dremc_talla1 = item.dremc_talla1,
                            dremc_talla2 = item.dremc_talla2,
                            dremc_talla3 = item.dremc_talla3,
                            dremc_talla4 = item.dremc_talla4,
                            dremc_talla5 = item.dremc_talla5,
                            dremc_talla6 = item.dremc_talla6,
                            dremc_talla7 = item.dremc_talla7,
                            dremc_talla8 = item.dremc_talla8,
                            dremc_talla9 = item.dremc_talla9,
                            dremc_talla10 = item.dremc_talla10,
                            dremc_cant_talla1 = item.dremc_cant_talla1,
                            dremc_cant_talla2 = item.dremc_cant_talla2,
                            dremc_cant_talla3 = item.dremc_cant_talla3,
                            dremc_cant_talla4 = item.dremc_cant_talla4,
                            dremc_cant_talla5 = item.dremc_cant_talla5,
                            dremc_cant_talla6 = item.dremc_cant_talla6,
                            dremc_cant_talla7 = item.dremc_cant_talla7,
                            dremc_cant_talla8 = item.dremc_cant_talla8,
                            dremc_cant_talla9 = item.dremc_cant_talla9,
                            dremc_cant_talla10 = item.dremc_cant_talla10,
                            dremc_iid_kardex1 = item.dremc_iid_kardex1,
                            dremc_iid_kardex2 = item.dremc_iid_kardex2,
                            dremc_iid_kardex3 = item.dremc_iid_kardex3,
                            dremc_iid_kardex4 = item.dremc_iid_kardex4,
                            dremc_iid_kardex5 = item.dremc_iid_kardex5,
                            dremc_iid_kardex6 = item.dremc_iid_kardex6,
                            dremc_iid_kardex7 = item.dremc_iid_kardex7,
                            dremc_iid_kardex8 = item.dremc_iid_kardex8,
                            dremc_iid_kardex9 = item.dremc_iid_kardex9,
                            dremc_iid_kardex10 = item.dremc_iid_kardex10,
                            dremc_icod_producto1 = item.dremc_icod_producto1,
                            dremc_icod_producto2 = item.dremc_icod_producto2,
                            dremc_icod_producto3 = item.dremc_icod_producto3,
                            dremc_icod_producto4 = item.dremc_icod_producto4,
                            dremc_icod_producto5 = item.dremc_icod_producto5,
                            dremc_icod_producto6 = item.dremc_icod_producto6,
                            dremc_icod_producto7 = item.dremc_icod_producto7,
                            dremc_icod_producto8 = item.dremc_icod_producto8,
                            dremc_icod_producto9 = item.dremc_icod_producto9,
                            dremc_icod_producto10 = item.dremc_icod_producto10,
                            //strCodProducto = item.strCodProducto,
                            //strDesProducto = item.strDesProducto,
                            strDesUM = item.strDesUM,
                            strAbreUM = item.strAbreUM,
                            dremc_vobservaciones = item.dremc_vobservaciones,
                            dblStockDisponible = Convert.ToDecimal(item.dblStockDisponible),
                            kardc_icod_correlativo_ingreso = item.kardc_icod_correlativo_ingreso,
                            dremc_icodigo = item.dremc_icodigo,
                            resec_vdescripcion = item.resec_vdescripcion,
                            resec_vtalla_inicial = item.resec_vtalla_inicial,
                            resec_vtalla_final = item.resec_vtalla_final,
                            unidc_icod_unidad_medida = Convert.ToInt32(item.unidc_icod_unidad_medida),
                            CodigoSUNAT = item.CodigoSUNAT
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
        public int insertarGuiaRemisionDet(EGuiaRemisionDet ob)
        {
            int? id_guia_remision = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_GUIA_REMISION_DET_INSERTAR(
                        ref id_guia_remision,
                        ob.remic_icod_remision,
                        ob.dremc_inro_item,
                        ob.prdc_icod_producto,
                        ob.dremc_vcodigo_extremo_prod,
                        ob.dremc_vdescripcion_prod,
                        ob.dremc_ncantidad_producto,
                        ob.kardc_icod_correlativo,
                        ob.dremc_imodelo,
                        ob.dremc_icolor,
                        ob.dremc_imarca,
                        ob.dremc_itipo,
                        ob.dremc_iserie,
                        ob.dremc_rango_talla,
                        ob.dremc_talla1,
                        ob.dremc_talla2,
                        ob.dremc_talla3,
                        ob.dremc_talla4,
                        ob.dremc_talla5,
                        ob.dremc_talla6,
                        ob.dremc_talla7,
                        ob.dremc_talla8,
                        ob.dremc_talla9,
                        ob.dremc_talla10,
                        ob.dremc_cant_talla1,
                        ob.dremc_cant_talla2,
                        ob.dremc_cant_talla3,
                        ob.dremc_cant_talla4,
                        ob.dremc_cant_talla5,
                        ob.dremc_cant_talla6,
                        ob.dremc_cant_talla7,
                        ob.dremc_cant_talla8,
                        ob.dremc_cant_talla9,
                        ob.dremc_cant_talla10,
                        ob.dremc_iid_kardex1,
                        ob.dremc_iid_kardex2,
                        ob.dremc_iid_kardex3,
                        ob.dremc_iid_kardex4,
                        ob.dremc_iid_kardex5,
                        ob.dremc_iid_kardex6,
                        ob.dremc_iid_kardex7,
                        ob.dremc_iid_kardex8,
                        ob.dremc_iid_kardex9,
                        ob.dremc_iid_kardex10,
                        ob.dremc_icod_producto1,
                        ob.dremc_icod_producto2,
                        ob.dremc_icod_producto3,
                        ob.dremc_icod_producto4,
                        ob.dremc_icod_producto5,
                        ob.dremc_icod_producto6,
                        ob.dremc_icod_producto7,
                        ob.dremc_icod_producto8,
                        ob.dremc_icod_producto9,
                        ob.dremc_icod_producto10,
                        ob.intUsuario,
                        ob.strPc,
                        ob.dremc_vobservaciones,
                        ob.kardc_icod_correlativo_ingreso,
                        ob.dremc_icodigo,
                        ob.unidc_icod_unidad_medida,
                        ob.dremc_itipo_producto);
                }
                return Convert.ToInt32(id_guia_remision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGuiaRemisionDet(EGuiaRemisionDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_GUIA_REMISION_DET_MODIFICAR(
                            ob.dremc_icod_detalle_remision,
                            ob.remic_icod_remision,
                            ob.dremc_inro_item,
                            ob.prdc_icod_producto,
                            ob.dremc_vcodigo_extremo_prod,
                            ob.dremc_vdescripcion_prod,
                            ob.dremc_ncantidad_producto,
                            ob.kardc_icod_correlativo,
                            ob.dremc_imodelo,
                            ob.dremc_icolor,
                            ob.dremc_imarca,
                            ob.dremc_itipo,
                            ob.dremc_iserie,
                            ob.dremc_rango_talla,
                            ob.dremc_talla1,
                            ob.dremc_talla2,
                            ob.dremc_talla3,
                            ob.dremc_talla4,
                            ob.dremc_talla5,
                            ob.dremc_talla6,
                            ob.dremc_talla7,
                            ob.dremc_talla8,
                            ob.dremc_talla9,
                            ob.dremc_talla10,
                            ob.dremc_cant_talla1,
                            ob.dremc_cant_talla2,
                            ob.dremc_cant_talla3,
                            ob.dremc_cant_talla4,
                            ob.dremc_cant_talla5,
                            ob.dremc_cant_talla6,
                            ob.dremc_cant_talla7,
                            ob.dremc_cant_talla8,
                            ob.dremc_cant_talla9,
                            ob.dremc_cant_talla10,
                            ob.dremc_iid_kardex1,
                            ob.dremc_iid_kardex2,
                            ob.dremc_iid_kardex3,
                            ob.dremc_iid_kardex4,
                            ob.dremc_iid_kardex5,
                            ob.dremc_iid_kardex6,
                            ob.dremc_iid_kardex7,
                            ob.dremc_iid_kardex8,
                            ob.dremc_iid_kardex9,
                            ob.dremc_iid_kardex10,
                            ob.dremc_icod_producto1,
                            ob.dremc_icod_producto2,
                            ob.dremc_icod_producto3,
                            ob.dremc_icod_producto4,
                            ob.dremc_icod_producto5,
                            ob.dremc_icod_producto6,
                            ob.dremc_icod_producto7,
                            ob.dremc_icod_producto8,
                            ob.dremc_icod_producto9,
                            ob.dremc_icod_producto10,
                            ob.intUsuario,
                            ob.strPc,
                            ob.dremc_vobservaciones,
                            ob.dremc_icodigo,
                            ob.unidc_icod_unidad_medida,
                            ob.dremc_itipo_producto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarGuiaRemisionDet(EGuiaRemisionDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_GUIA_REMISION_DET_ELIMINAR(
                            ob.dremc_icod_detalle_remision,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Lista de Pedido Venta
        public List<EPedidoClienCab> listarPedidoVenta()
        {
            List<EPedidoClienCab> lista = null;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<EPedidoClienCab>();
                    var query = dc.SGEC_PEDIDO_CLIEN_CAB_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EPedidoClienCab()
                        {
                            lpedi_icod_cliente = Convert.ToInt32(item.lpedi_icod_cliente),
                            perc_icod_personal = item.perc_icod_personal,
                            lpedi_Numerolista = item.lpedi_Numerolista,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            lpedi_sfecha_cliente = Convert.ToDateTime(item.lpedi_sfecha_cliente),
                            lpedi_Observaciones = item.lpedi_Observaciones,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            cliec_vcod_cliente = item.cliec_vcod_cliente,
                            perc_vapellidos_nombres = item.perc_vapellidos_nombres,
                            lpedi_isituacion_prov = Convert.ToInt32(item.lpedi_isituacion_prov),
                            StrSituacion = item.StrSituacion,
                            tablc_iid_tipo_ped = Convert.ToInt32(item.tablc_iid_tipo_ped),
                            StrTipoPedido = item.StrTipoPedido
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

        public int insertarPedidoVenta(EPedidoClienCab obj)
        {

            try
            {
                int? lpedi_icod_cliente = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PEDIDO_CLIEN_CAB_INSERTAR(
                        ref lpedi_icod_cliente,
                        obj.perc_icod_personal,
                        obj.lpedi_Numerolista,
                        obj.cliec_icod_cliente,
                        obj.lpedi_sfecha_cliente,
                        obj.lpedi_Observaciones,
                        obj.intUsuario,
                        obj.strPc,
                        obj.lpedi_sflag_estado,
                        obj.lpedi_isituacion_prov,
                        obj.tablc_iid_tipo_ped
                    );

                }
                return Convert.ToInt32(lpedi_icod_cliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPedidoVenta(EPedidoClienCab obj)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PEDIDO_CLIEN_CAB_MODIFICAR(
                        obj.lpedi_icod_cliente,
                        obj.lpedi_sfecha_cliente,
                        obj.lpedi_Observaciones,
                        obj.intUsuario,
                        obj.strPc,
                        obj.tablc_iid_tipo_ped
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PEDIDO_CLIEN_CAB_ELIMINAR(
                        obj.lpedi_icod_cliente,
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
        #region Lista de Pedido Venta Det
        public List<EPedidoClienDet> listarPedidoVentaDet(int lpedi_icod_proveedor, int ANIO)
        {
            List<EPedidoClienDet> lista = null;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<EPedidoClienDet>();
                    var query = dc.SGEC_PEDIDO_CLIEN_DET_LISTAR(lpedi_icod_proveedor, ANIO);
                    foreach (var item in query)
                    {
                        lista.Add(new EPedidoClienDet()
                        {
                            lpedid_icod_cliente = item.lpedid_icod_cliente,
                            lpedi_icod_cliente = item.lpedi_icod_cliente,
                            lpedid_iitem = item.lpedid_iitem,
                            prdc_icod_producto = item.prdc_icod_producto,
                            lpedid_icod_moneda = Convert.ToInt32(item.lpedid_icod_moneda),
                            lpedid_nstock_producto = item.lpedid_nstock_producto,
                            lpedid_nCant_pedido = Convert.ToInt32(item.lpedid_nCant_pedido),
                            lpedid_nprecio_uni = Convert.ToDecimal(item.lpedid_nprecio_uni),
                            prdc_vdescripcion_larga = item.prdc_vdescripcion_larga,
                            prdc_vcode_producto = item.prdc_vcode_producto,
                            strEditorial = item.edit_vdescripcion,
                            prdc_vAutor = item.prdc_vAutor,
                            lpedid_vDesc_moneda = item.strMoneda,
                            intTipoOperacion = item.intOperacion,
                            lpedid_nTotal_precio = Convert.ToDecimal(item.lpedid_nprecio_uni * item.lpedid_nCant_pedido),
                            strCategoria = item.strCategoria,
                            strSubCategoriaUno = item.strSubCategoriaUno,
                            StrUnidadMedida = item.StrUnidadMedida
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

        public int insertarPedidoVentaDet(EPedidoClienDet obj)
        {

            try
            {
                int? lpedid_icod_cliente = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PEDIDO_CLIEN_DET_INSERTAR(
                        ref lpedid_icod_cliente,
                        obj.lpedi_icod_cliente,
                        obj.lpedid_iitem,
                        obj.prdc_icod_producto,
                        obj.lpedid_nstock_producto,
                        obj.lpedid_nCant_pedido,
                        obj.lpedid_icod_moneda,
                        obj.lpedid_nprecio_uni,
                        obj.intUsuario,
                        obj.strPc,
                        obj.lpedid_sflag_estado
                    );

                }
                return Convert.ToInt32(lpedid_icod_cliente);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PEDIDO_CLIEN_DET_MODIFICAR(
                        obj.lpedid_icod_cliente,
                        obj.lpedid_iitem,
                        obj.lpedid_nstock_producto,
                        obj.lpedid_nCant_pedido,
                        obj.lpedid_nprecio_uni,
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

        public void eliminarPedidoVentaDet(EPedidoClienDet obj)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEC_PEDIDO_CLIEN_DET_ELIMINAR(
                        obj.lpedid_icod_cliente,
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
        #region EventoVenta
        public List<EEventoVenta> ListarEventoVenta()
        {
            List<EEventoVenta> lista = new List<EEventoVenta>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_EVENTO_VENTA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EEventoVenta()
                        {
                            evev_icod_evento_venta = item.evev_icod_evento_venta,
                            evev_vnumero_evento_venta = item.evev_vnumero_evento_venta,
                            evev_isituacion_even_venta = item.evev_isituacion_even_venta,
                            evev_vlugar_evento_venta = item.evev_vlugar_evento_venta,
                            evev_vDirecc_evento_venta = item.evev_vDirecc_evento_venta,
                            evev_vcorreo_evento_venta = item.evev_vcorreo_evento_venta,
                            evev_vcontac_evento_venta = item.evev_vcontac_evento_venta,
                            evev_vTelefo_evento_venta = item.evev_vTelefo_evento_venta,
                            evev_sfecha_evento_inicio = item.evev_sfecha_evento_inicio,
                            evev_sfecha_evento_final = item.evev_sfecha_evento_final,
                            almac_icod_almacen = item.almac_icod_almacen,
                            almac_vresponsa_even_venta = item.almac_vresponsa_even_venta,
                            almac_vdescripcion = item.evev_vDirecc_evento_venta,
                            desSituacion = item.desSituacion,
                            evev_vnombre_evento_venta = item.evev_vnombre_evento_venta
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
        public int? InsertarEventoVenta(EEventoVenta ob)
        {
            int? evev_icod_evento_venta = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_EVENTO_VENTA_INSERTAR(
                        ref evev_icod_evento_venta,
                        ob.evev_vnumero_evento_venta,
                        ob.evev_isituacion_even_venta,
                        ob.evev_vlugar_evento_venta,
                        ob.evev_vDirecc_evento_venta,
                        ob.evev_vcorreo_evento_venta,
                        ob.evev_vcontac_evento_venta,
                        ob.evev_vTelefo_evento_venta,
                        ob.evev_sfecha_evento_inicio,
                        ob.evev_sfecha_evento_final,
                        ob.almac_icod_almacen,
                        ob.almac_vresponsa_even_venta,
                        ob.intUsuario,
                        DateTime.Now,
                        ob.strPc,
                        null,
                        null,
                        null,
                        ob.evev_flag_estado,
                        ob.evev_vnombre_evento_venta);
                }
                return evev_icod_evento_venta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarEventoVenta(EEventoVenta ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_EVENTO_VENTA_MODIFICAR(
                        ob.evev_icod_evento_venta,
                        ob.evev_vnumero_evento_venta,
                        ob.evev_isituacion_even_venta,
                        ob.evev_vlugar_evento_venta,
                        ob.evev_vDirecc_evento_venta,
                        ob.evev_vcorreo_evento_venta,
                        ob.evev_vcontac_evento_venta,
                        ob.evev_vTelefo_evento_venta,
                        ob.evev_sfecha_evento_inicio,
                        ob.evev_sfecha_evento_final,
                        ob.almac_icod_almacen,
                        ob.almac_vresponsa_even_venta,
                        ob.intUsuario,
                        DateTime.Now,
                        ob.strPc,
                        null, null, null,
                        ob.evev_vnombre_evento_venta);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_EVENTO_VENTA_ELIMINAR(evev_icod_evento_venta, intUsuario, strPc);
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
            List<EProyectos> lista = new List<EProyectos>();

            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<EProyectos>();
                    var query = dc.SGE_PROYECTOS_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EProyectos()
                        {
                            pryc_icod_proyecto = Convert.ToInt32(item.pryc_icod_proyecto),
                            pryc_ianio = Convert.ToInt32(item.pryc_ianio),
                            pryc_vcorrelativo = item.pryc_vcorrelativo,
                            pryc_vdescripcion = item.pryc_vdescripcion,
                            pryc_vdireccion_prov = item.pryc_vdireccion_prov,
                            pryc_icod_cliente = Convert.ToInt32(item.pryc_icod_cliente),
                            NomCliente = item.NomCliente,
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            StrAlmacen = item.StrAlmacen,
                            StrAlmacenDir = item.StrAlmacenDir,
                            RUC = item.cliec_cruc,
                            pryc_icod_ccosto = Convert.ToInt32(item.pryc_icod_ccosto),
                            CentroCosto = item.CentroCosto,
                            pryc_sfecha_inicio = Convert.ToDateTime(item.pryc_sfecha_inicio),
                            pryc_sfecha_emtrega = Convert.ToDateTime(item.pryc_sfecha_emtrega),
                            pryc_nrentabilidad = Convert.ToDecimal(item.pryc_nrentabilidad),
                            pryc_icod_acta_entrega = Convert.ToInt32(item.pryc_icod_acta_entrega),
                            strActaEntrega = item.strActaEntrega,
                            pryc_iestado = Convert.ToInt32(item.pryc_iestado),
                            strEstado = item.strEstado,
                            NumHojaCosto = item.NumHojaCosto,
                            hjcc_icod_hoja_costo = Convert.ToInt32(item.hjcc_icod_hoja_costo)

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
        public int insertarProyectos(EProyectos oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGE_PROYECTOS_INSERTAR(
                    ref intIcod,
                    oBe.pryc_ianio,
                    oBe.pryc_vcorrelativo,
                    oBe.pryc_vdescripcion,
                    oBe.pryc_icod_cliente,
                    oBe.almac_icod_almacen,
                    oBe.pryc_vdireccion_prov,
                    oBe.pryc_icod_ccosto,
                    oBe.pryc_sfecha_inicio,
                    oBe.pryc_sfecha_emtrega,
                    oBe.pryc_icod_acta_entrega,
                    oBe.pryc_nrentabilidad,
                    oBe.pryc_iestado,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.pryc_flag_estado,
                    oBe.hjcc_icod_hoja_costo);
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PROYECTOS_MODIFICAR(
                    oBe.pryc_icod_proyecto,
                    oBe.pryc_vcorrelativo,
                    oBe.pryc_vdescripcion,
                    oBe.pryc_icod_cliente,
                    oBe.almac_icod_almacen,
                    oBe.pryc_vdireccion_prov,
                    oBe.pryc_icod_ccosto,
                    oBe.pryc_sfecha_inicio,
                    oBe.pryc_sfecha_emtrega,
                    oBe.pryc_icod_acta_entrega,
                    oBe.pryc_nrentabilidad,
                    oBe.pryc_iestado,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.hjcc_icod_hoja_costo);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_PROYECTOS_ELIMINAR(
                        oBe.pryc_icod_proyecto,
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
        #region Archivos
        public List<EArchivos> listarArchivos(int codigo)
        {
            List<EArchivos> lista = new List<EArchivos>();

            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<EArchivos>();
                    var query = dc.SGE_ARCHIVOS_LISTAR(codigo);
                    foreach (var item in query)
                    {
                        lista.Add(new EArchivos()
                        {
                            arch_icod_archivos = Convert.ToInt32(item.arch_icod_archivos),
                            arch_iid_correlativo = Convert.ToInt32(item.arch_iid_correlativo),
                            arch_iid_orden_compra_local = Convert.ToInt32(item.arch_iid_orden_compra_local),
                            arch_vdescripcion = item.arch_vdescripcion,
                            arch_vruta = item.arch_vruta,
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
        public int insertarArchivos(EArchivos oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGE_ARCHIVOS_INSERTAR(
                    ref intIcod,
                    oBe.arch_iid_correlativo,
                    oBe.arch_iid_orden_compra_local,
                    oBe.arch_vdescripcion,
                    oBe.arch_vruta,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.arch_flag_estado);
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_ARCHIVOS_MODIFICAR(
                    oBe.arch_icod_archivos,
                    oBe.arch_iid_correlativo,
                    oBe.arch_iid_orden_compra_local,
                    oBe.arch_vdescripcion,
                    oBe.arch_vruta,
                    oBe.intUsuario,
                    oBe.strPc);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_ARCHIVOS_ELIMINAR(
                        oBe.arch_icod_archivos,
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
        #region Hoja Costos
        public List<EHojaCostos> listarHojaCostos()
        {
            List<EHojaCostos> lista = new List<EHojaCostos>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_HOJA_COSTOS_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EHojaCostos()
                        {
                            hjcc_icod_hoja_costo = item.hjcc_icod_hoja_costo,
                            hjcc_vnumero_hoja_costo = item.hjcc_vnumero_hoja_costo,
                            hjcc_sfecha_hoja_costo = Convert.ToDateTime(item.hjcc_sfecha_hoja_costo),
                            hjcc_ntipo_cambio = Convert.ToInt32(item.hjcc_ntipo_cambio),
                            tablc_iid_situacion_hc = Convert.ToInt32(item.tablc_iid_situacion_hc),
                            Situacion = item.Situacion,
                            pryc_icod_proyecto = Convert.ToInt32(item.pryc_icod_proyecto),
                            CentroCostos = item.CentroCostos,
                            pryc_vdescripcion = item.pryc_vdescripcion,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            Moneda = item.Moneda,
                            hjcc_vdescripcion = item.hjcc_vdescripcion,
                            hjcc_nmonto_presup = Convert.ToInt32(item.hjcc_nmonto_presup),
                            hjcc_nmonto_real = Convert.ToInt32(item.hjcc_nmonto_real),
                            hjcc_ntotal_soles = Convert.ToDecimal(item.hjcc_ntotal_soles),
                            hjcc_ntotal_dolares = Convert.ToDecimal(item.hjcc_ntotal_dolares),

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
        public int insertarHojaCostos(EHojaCostos oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_INSERTAR(
                        ref intIcod,
                        oBe.hjcc_vnumero_hoja_costo,
                        oBe.hjcc_sfecha_hoja_costo,
                        oBe.hjcc_ntipo_cambio,
                        oBe.tablc_iid_situacion_hc,
                        oBe.pryc_icod_proyecto,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.hjcc_vdescripcion,
                        oBe.hjcc_nmonto_presup,
                        oBe.hjcc_nmonto_real,
                        oBe.hjcc_ntotal_soles,
                        oBe.hjcc_ntotal_dolares,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.hjcc_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarHojaCostos(EHojaCostos oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_MODIFICAR(
                        oBe.hjcc_icod_hoja_costo,
                        oBe.hjcc_sfecha_hoja_costo,
                        oBe.hjcc_ntipo_cambio,
                        oBe.tablc_iid_situacion_hc,
                        oBe.pryc_icod_proyecto,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.hjcc_vdescripcion,
                        oBe.hjcc_nmonto_presup,
                        oBe.hjcc_nmonto_real,
                        oBe.hjcc_ntotal_soles,
                        oBe.hjcc_ntotal_dolares,
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
        public void eliminarHojaCostos(EHojaCostos oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_ELIMINAR(
                        oBe.hjcc_icod_hoja_costo,
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
        public List<EHojaCostos> getHCCabVerificarNumero(string vnumero, int ANIO)
        {
            List<EHojaCostos> lista = new List<EHojaCostos>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_HC_CAB_VERIFICAR_NUEMRO(vnumero, ANIO);
                    foreach (var item in query)
                    {
                        lista.Add(new EHojaCostos()
                        {
                            hjcc_icod_hoja_costo = item.hjcc_icod_hoja_costo

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
        #region Hoja Costo Conceptos
        public List<EHojaCostosConceptos> listarHojaCostosConceptos(int Concepto)
        {
            List<EHojaCostosConceptos> lista = new List<EHojaCostosConceptos>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_HOJA_COSTOS_CONCEPTO_LISTAR(Concepto);
                    foreach (var item in query)
                    {
                        lista.Add(new EHojaCostosConceptos()
                        {
                            hjcd1_icod_detalle_hc = item.hjcd1_icod_detalle_hc,
                            hjcd1_iitem_orden = item.hjcd1_iitem_orden,
                            hjcd1_vcodigo_concepto_hc = item.hjcd1_vcodigo_concepto_hc,
                            hjcd1_vdescripcion = item.hjcd1_vdescripcion,
                            hjcd1_nmonto_presup = Convert.ToDecimal(item.hjcd1_nmonto_presup),
                            hjcd1_nmonto_real = Convert.ToDecimal(item.hjcd1_nmonto_real),
                            Indicador = Convert.ToInt32(item.Indicador)

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
        public int insertarHojaCostosConceptos(EHojaCostosConceptos oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_CONCEPTO_INSERTAR(
                        ref intIcod,
                         oBe.hjcd1_iitem_orden,
                        oBe.hjcc_icod_hoja_costo,
                        oBe.hjcd1_vcodigo_concepto_hc,
                        oBe.hjcd1_vdescripcion,
                        oBe.hjcd1_nmonto_presup,
                        oBe.hjcd1_nmonto_real,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.hjcd1_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_CONCEPTO_MODIFICAR(
                        oBe.hjcd1_icod_detalle_hc,
                        oBe.hjcd1_iitem_orden,
                        oBe.hjcd1_vcodigo_concepto_hc,
                        oBe.hjcd1_vdescripcion,
                        oBe.hjcd1_nmonto_presup,
                        oBe.hjcd1_nmonto_real,
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
        public void eliminarHojaCostosConceptos(EHojaCostosConceptos oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_CONCEPTO_ELIMINAR(
                        oBe.hjcd1_icod_detalle_hc,
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
        #region Hoja Costos Sub-Conceptos
        public List<EhojaCostosSubConceptos> listarHojaCostosSubConceptos(int SubConcepto)
        {
            List<EhojaCostosSubConceptos> lista = new List<EhojaCostosSubConceptos>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_HOJA_COSTOS_SUB_CONCEPTO_LISTAR(SubConcepto);
                    foreach (var item in query)
                    {
                        lista.Add(new EhojaCostosSubConceptos()
                        {
                            hjcd2_icod_subconcepto_hc = item.hjcd2_icod_subconcepto_hc,
                            hjcd2_iitem_orden = item.hjcd2_iitem_orden,
                            hjcc_icod_hoja_costo = Convert.ToInt32(item.hjcc_icod_hoja_costo),
                            hjcc1_iiten = item.hjcc1_iiten,
                            hjcd2_vcodigo_concepto_hc = item.hjcd2_vcodigo_concepto_hc,
                            hjcd2_vdescripcion = item.hjcd2_vdescripcion,
                            hjcd2_nmonto_presup = Convert.ToDecimal(item.hjcd2_nmonto_presup),
                            hjcd2_nmonto_real = Convert.ToDecimal(item.hjcd2_nmonto_real)

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
        public int insertarHojaCostosSubConceptos(EhojaCostosSubConceptos oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_SUB_CONCEPTO_INSERTAR(
                        ref intIcod,
                        oBe.hjcd2_iitem_orden,
                        oBe.hjcc_icod_hoja_costo,
                        oBe.hjcd1_icod_concepto_hc,
                        oBe.hjcc1_iiten,
                        oBe.hjcd2_vcodigo_concepto_hc,
                        oBe.hjcd2_vdescripcion,
                        oBe.hjcd2_nmonto_presup,
                        oBe.hjcd2_nmonto_real,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.hjcd2_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_SUB_CONCEPTO_MODIFICAR(
                        oBe.hjcd2_icod_subconcepto_hc,
                        oBe.hjcd2_iitem_orden,
                        oBe.hjcd2_vcodigo_concepto_hc,
                        oBe.hjcd2_vdescripcion,
                        oBe.hjcd2_nmonto_presup,
                        oBe.hjcd2_nmonto_real,
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
        public void eliminarHojaCostosSubConceptos(EhojaCostosSubConceptos oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_SUB_CONCEPTO_ELIMINAR(
                        oBe.hjcd2_icod_subconcepto_hc,
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
        #region Hoja Costos Rubros
        public List<EHojaCostosRubros> listarHojaCostosRubros(int Rubros)
        {
            List<EHojaCostosRubros> lista = new List<EHojaCostosRubros>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_HOJA_COSTOS_RUBROS_LISTAR(Rubros);
                    foreach (var item in query)
                    {
                        lista.Add(new EHojaCostosRubros()
                        {
                            hjcd3_icod_rubros_hc = item.hjcd3_icod_rubros_hc,
                            hjcd3_iitem_orden = item.hjcd3_iitem_orden,
                            hjcc_icod_hoja_costo = Convert.ToInt32(item.hjcc_icod_hoja_costo),
                            hjcd2_iitem = item.hjcd2_iitem,
                            hjcd3_vcodigo_relacion = item.hjcd3_vcodigo_relacion,
                            hjcd3_vcodigo_concepto_hc = item.hjcd3_vcodigo_concepto_hc,
                            hjcd3_ncantidad = Convert.ToDecimal(item.hjcd3_ncantidad),
                            hjcd3_unidc_icod_unidad_medida = Convert.ToInt32(item.hjcd3_unidc_icod_unidad_medida),
                            Unidad = item.Unidad,
                            hjcd3_vdescripcion = item.hjcd3_vdescripcion,
                            tablc_icod_tipo_moneda = Convert.ToInt32(item.tablc_icod_tipo_moneda),
                            Moneda = item.Moneda,
                            TipoModena = item.TipoModena,
                            hjcd3_nmonto_unitario = Convert.ToDecimal(item.hjcd3_nmonto_unitario),
                            hjcd3_nmonto_real = Convert.ToDecimal(item.hjcd3_nmonto_real),
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            str_producto = item.str_producto,
                            hjcd3_ncantidad_requerida = Convert.ToDecimal(item.hjcd3_ncantidad_requerida),
                            hjcd3_ncantidad_autorizada = Convert.ToDecimal(item.hjcd3_ncantidad_autorizada),
                            hjcd3_ncantidad_atendida = Convert.ToDecimal(item.hjcd3_ncantidad_atendida),
                            hjcd3_ncantidad_saldo = Convert.ToDecimal(item.hjcd3_ncantidad_saldo),
                            tablc_icod_tipo_rubro = Convert.ToInt32(item.tablc_icod_tipo_rubro)

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
        public List<EHojaCostosRubros> listarHojaCostosRubrosRQM(int Rubros)
        {
            List<EHojaCostosRubros> lista = new List<EHojaCostosRubros>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_HOJA_COSTOS_RUBROS_RQM_LISTAR(Rubros);
                    foreach (var item in query)
                    {
                        lista.Add(new EHojaCostosRubros()
                        {
                            hjcd3_icod_rubros_hc = item.hjcd3_icod_rubros_hc,
                            hjcc_icod_hoja_costo = Convert.ToInt32(item.hjcc_icod_hoja_costo),
                            hjcd2_iitem = item.hjcd2_iitem,
                            hjcd3_vcodigo_relacion = item.hjcd3_vcodigo_relacion,
                            hjcd3_vcodigo_concepto_hc = item.hjcd3_vcodigo_concepto_hc,
                            hjcd3_ncantidad = Convert.ToDecimal(item.hjcd3_ncantidad),
                            hjcd3_unidc_icod_unidad_medida = Convert.ToInt32(item.hjcd3_unidc_icod_unidad_medida),
                            Unidad = item.Unidad,
                            hjcd3_vdescripcion = item.hjcd3_vdescripcion,
                            tablc_icod_tipo_moneda = Convert.ToInt32(item.tablc_icod_tipo_moneda),
                            Moneda = item.Moneda,
                            TipoModena = item.TipoModena,
                            hjcd3_nmonto_unitario = Convert.ToDecimal(item.hjcd3_nmonto_unitario),
                            hjcd3_nmonto_real = Convert.ToDecimal(item.hjcd3_nmonto_real),
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            str_producto = item.str_producto,
                            hjcd3_ncantidad_atendida = Convert.ToDecimal(item.hjcd3_ncantidad_atendida),
                            hjcd3_ncantidad_saldo = Convert.ToDecimal(item.hjcd3_ncantidad_saldo),
                            tablc_icod_tipo_rubro = Convert.ToInt32(item.tablc_icod_tipo_rubro),
                            hjcd3_ncantidad_requerida = Convert.ToDecimal(item.hjcd3_ncantidad_requerida)

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
        public int insertarHojaCostosRubros(EHojaCostosRubros oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_RUBROS_INSERTAR(
                        ref intIcod,
                        oBe.hjcd3_iitem_orden,
                        oBe.hjcc_icod_hoja_costo,
                        oBe.hjcd2_icod_subconcepto_hc,
                        oBe.hjcd2_iitem,
                        oBe.hjcd3_vcodigo_relacion,
                        oBe.hjcd3_vcodigo_concepto_hc,
                        oBe.hjcd3_ncantidad,
                        oBe.hjcd3_unidc_icod_unidad_medida,
                        oBe.hjcd3_vdescripcion,
                        oBe.tablc_icod_tipo_moneda,
                        oBe.hjcd3_nmonto_unitario,
                        oBe.hjcd3_nmonto_real,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.hjcd3_flag_estado,
                        oBe.prdc_icod_producto,
                        oBe.hjcd3_ncantidad_requerida,
                        oBe.hjcd3_ncantidad_autorizada,
                        oBe.hjcd3_ncantidad_atendida,
                        oBe.hjcd3_ncantidad_saldo,
                        oBe.tablc_icod_tipo_rubro
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_RUBROS_MODIFICAR(
                        oBe.hjcd3_icod_rubros_hc,
                        oBe.hjcd3_iitem_orden,
                        oBe.hjcd3_vcodigo_relacion,
                        oBe.hjcd3_vcodigo_concepto_hc,
                        oBe.hjcd3_ncantidad,
                        oBe.hjcd3_unidc_icod_unidad_medida,
                        oBe.hjcd3_vdescripcion,
                        oBe.tablc_icod_tipo_moneda,
                        oBe.hjcd3_nmonto_unitario,
                        oBe.hjcd3_nmonto_real,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.prdc_icod_producto,
                        oBe.hjcd3_ncantidad_requerida,
                        oBe.hjcd3_ncantidad_autorizada,
                        oBe.hjcd3_ncantidad_atendida,
                        oBe.hjcd3_ncantidad_saldo,
                        oBe.tablc_icod_tipo_rubro
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_RUBROS_ELIMINAR(
                        oBe.hjcd3_icod_rubros_hc,
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
        #region Requerimiento Materiales
        public List<ERequerimientoMateriales> listarRequerimientoMateriales()
        {
            List<ERequerimientoMateriales> lista = new List<ERequerimientoMateriales>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_REQUERIMIENTOS_MATERIALES_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ERequerimientoMateriales()
                        {
                            rqmc_icod_requerimiento_materiales = item.rqmc_icod_requerimiento_materiales,
                            rqmc_numero_req_material = item.rqmc_numero_req_material,
                            rqmc_sfecha_req_material = Convert.ToDateTime(item.rqmc_sfecha_req_material),
                            tablc_iid_tipo_requerimiento = Convert.ToInt32(item.tablc_iid_tipo_requerimiento),
                            Tipo = item.Tipo,
                            tablc_iid_situación_hc = Convert.ToInt32(item.tablc_iid_situación_hc),
                            Situacion = item.Situacion,
                            pryc_icod_proyecto = Convert.ToInt32(item.pryc_icod_proyecto),
                            CentroCostos = item.CentroCostos,
                            DesProyecto = item.DesProyecto,
                            rqmc_vdescripcion = item.rqmc_vdescripcion,
                            rqmc_bautorizado = Convert.ToBoolean(item.rqmc_bautorizado),
                            NomCliente = item.NomCliente,
                            NumHojaCosto = item.NumHojaCosto,
                            hjcc_icod_hoja_costo = Convert.ToInt32(item.hjcc_icod_hoja_costo)


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
        public List<ERequerimientoMateriales> listarAutorizacionRequerimientoMateriales()
        {
            List<ERequerimientoMateriales> lista = new List<ERequerimientoMateriales>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_AUTORIZACION_REQUERIMIENTOS_MATERIALES_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ERequerimientoMateriales()
                        {
                            rqmc_icod_requerimiento_materiales = item.rqmc_icod_requerimiento_materiales,
                            rqmc_numero_req_material = item.rqmc_numero_req_material,
                            rqmc_sfecha_req_material = Convert.ToDateTime(item.rqmc_sfecha_req_material),
                            tablc_iid_tipo_requerimiento = Convert.ToInt32(item.tablc_iid_tipo_requerimiento),
                            Tipo = item.Tipo,
                            tablc_iid_situación_hc = Convert.ToInt32(item.tablc_iid_situación_hc),
                            Situacion = item.Situacion,
                            pryc_icod_proyecto = Convert.ToInt32(item.pryc_icod_proyecto),
                            CentroCostos = item.CentroCostos,
                            DesProyecto = item.DesProyecto,
                            rqmc_vdescripcion = item.rqmc_vdescripcion,
                            rqmc_bautorizado = Convert.ToBoolean(item.rqmc_bautorizado),
                            NomCliente = item.NomCliente,
                            NumHojaCosto = item.NumHojaCosto


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
        public List<ERequerimientoMateriales> listarVerificacionStockRequerimientoMateriales()
        {
            List<ERequerimientoMateriales> lista = new List<ERequerimientoMateriales>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_AUTORIZACION_REQUERIMIENTOS_MATERIALES_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ERequerimientoMateriales()
                        {
                            rqmc_icod_requerimiento_materiales = item.rqmc_icod_requerimiento_materiales,
                            rqmc_numero_req_material = item.rqmc_numero_req_material,
                            rqmc_sfecha_req_material = Convert.ToDateTime(item.rqmc_sfecha_req_material),
                            tablc_iid_tipo_requerimiento = Convert.ToInt32(item.tablc_iid_tipo_requerimiento),
                            Tipo = item.Tipo,
                            tablc_iid_situación_hc = Convert.ToInt32(item.tablc_iid_situación_hc),
                            Situacion = item.Situacion,
                            pryc_icod_proyecto = Convert.ToInt32(item.pryc_icod_proyecto),
                            CentroCostos = item.CentroCostos,
                            DesProyecto = item.DesProyecto,
                            rqmc_vdescripcion = item.rqmc_vdescripcion,
                            rqmc_bautorizado = Convert.ToBoolean(item.rqmc_bautorizado),
                            NomCliente = item.NomCliente,
                            NumHojaCosto = item.NumHojaCosto


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
        public int insertarRequerimientoMateriales(ERequerimientoMateriales oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTOS_MATERIALES_INSERTAR(
                        ref intIcod,
                        oBe.rqmc_numero_req_material,
                        oBe.rqmc_sfecha_req_material,
                        oBe.tablc_iid_situación_hc,
                        oBe.tablc_iid_tipo_requerimiento,
                        oBe.pryc_icod_proyecto,
                        oBe.rqmc_vdescripcion,
                        oBe.rqmc_bautorizado,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.rqmc_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarRequerimientoMateriales(ERequerimientoMateriales oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTOS_MATERIALES_MODIFICAR(
                        oBe.rqmc_icod_requerimiento_materiales,
                        oBe.rqmc_sfecha_req_material,
                        oBe.tablc_iid_situación_hc,
                        oBe.tablc_iid_tipo_requerimiento,
                        oBe.pryc_icod_proyecto,
                        oBe.rqmc_vdescripcion,
                        oBe.rqmc_bautorizado,
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
        public void eliminarRequerimientoMateriales(ERequerimientoMateriales oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTOS_MATERIALES_ELIMINAR(
                        oBe.rqmc_icod_requerimiento_materiales,
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
        public void AnularRequerimientoMateriales(int rqmc_icod_requerimiento_materiales, int tablc_iid_situación_hc)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTOS_MATERIALES_ANULAR(rqmc_icod_requerimiento_materiales, tablc_iid_situación_hc);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_AUTORIZACION_REQUERIMIENTOS_MATERIALES_ELIMINAR(
                        oBe.rqmc_icod_requerimiento_materiales,
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
        #region Requerimiento Materiales Detalle
        public List<ERequerimientoMaterialesDetalle> listarRequerimientoMaterialesDetalle(int Concepto)
        {
            List<ERequerimientoMaterialesDetalle> lista = new List<ERequerimientoMaterialesDetalle>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_REQUERIMIENTOS_MATERIALES_DETALLE_LISTAR(Concepto);
                    foreach (var item in query)
                    {
                        lista.Add(new ERequerimientoMaterialesDetalle()
                        {
                            rqmd_icod_req_materiales_detalle = item.rqmd_icod_req_materiales_detalle,
                            rqmd_vcodigo_item_requerim = item.rqmd_vcodigo_item_requerim,
                            hjcd3_icod_rubros_hc = Convert.ToInt32(item.hjcd3_icod_rubros_hc),
                            rqmd_cantidad_pedida = Convert.ToDecimal(item.rqmd_cantidad_pedida),
                            rqmd_cantidad_aprobada = Convert.ToDecimal(item.rqmd_cantidad_aprobada),
                            DescripcionRubro = item.hjcd3_vdescripcion,
                            CodigoRubro = item.hjcd3_vcodigo_relacion,
                            Medida = item.Unidad,
                            Cantidad = Convert.ToDecimal(item.hjcd3_ncantidad),
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto)


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
        public List<ERequerimientoMaterialesDetalle> listarAutorizacionRequerimientoMaterialesDetalle(int Concepto)
        {
            List<ERequerimientoMaterialesDetalle> lista = new List<ERequerimientoMaterialesDetalle>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_AUTORIZACION_REQUERIMIENTOS_MATERIALES_DETALLE_LISTAR(Concepto);
                    foreach (var item in query)
                    {
                        lista.Add(new ERequerimientoMaterialesDetalle()
                        {
                            rqmd_icod_req_materiales_detalle = item.rqmd_icod_req_materiales_detalle,
                            rqmd_vcodigo_item_requerim = item.rqmd_vcodigo_item_requerim,
                            hjcd3_icod_rubros_hc = Convert.ToInt32(item.hjcd3_icod_rubros_hc),
                            rqmd_cantidad_pedida = Convert.ToDecimal(item.rqmd_cantidad_pedida),
                            rqmd_cantidad_aprobada = Convert.ToDecimal(item.rqmd_cantidad_aprobada),
                            DescripcionRubro = item.hjcd3_vdescripcion,
                            CodigoRubro = item.hjcd3_vcodigo_relacion,
                            Medida = item.Unidad,
                            Cantidad = Convert.ToDecimal(item.hjcd3_ncantidad),
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto)


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
        public int insertarRequerimientoMaterialesDetalle(ERequerimientoMaterialesDetalle oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTOS_MATERIALES_DETALLE_INSERTAR(
                        ref intIcod,
                        oBe.rqmc_icod_requerimiento_materiales,
                        oBe.rqmd_vcodigo_item_requerim,
                        oBe.hjcd3_icod_rubros_hc,
                        oBe.rqmd_cantidad_pedida,
                        oBe.rqmd_cantidad_aprobada,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.rqmd_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTOS_MATERIALES_DETALLE_MODIFICAR(
                        oBe.rqmd_icod_req_materiales_detalle,
                        oBe.rqmd_vcodigo_item_requerim,
                        oBe.hjcd3_icod_rubros_hc,
                        oBe.rqmd_cantidad_pedida,
                        oBe.rqmd_cantidad_aprobada,
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
        public void eliminarRequerimientoMaterialesDetalle(ERequerimientoMaterialesDetalle oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTOS_MATERIALES_DETALLE_ELIMINAR(
                        oBe.rqmd_icod_req_materiales_detalle,
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
        public void AutorizacionRequerimientoMaterialesDetalleEliminar(ERequerimientoMaterialesDetalle oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_AUTORIZACION_REQUERIMIENTOS_MATERIALES_DETALLE_ELIMINAR(
                        oBe.rqmd_icod_req_materiales_detalle
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Conceptos Costos
        public List<EConceptosCostos> listarConceptosCostos()
        {
            List<EConceptosCostos> lista = new List<EConceptosCostos>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_CONCEPTO_COSTOS_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EConceptosCostos()
                        {
                            chc_icod_detalle_hc = item.chc_icod_detalle_hc,
                            chc_iitem_orden = item.chc_iitem_orden,
                            chc_vcodigo_concepto_hc = item.chc_vcodigo_concepto_hc,
                            chc_vdescripcion = item.chc_vdescripcion,
                            chc_nmonto_presup = Convert.ToDecimal(item.chc_nmonto_presup),
                            chc_nmonto_real = Convert.ToDecimal(item.chc_nmonto_real)

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
        public int insertarConceptosCostos(EConceptosCostos oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_CONCEPTO_COSTOS_INSERTAR(
                        ref intIcod,
                         oBe.chc_iitem_orden,
                        oBe.chc_icod_hoja_costo,
                        oBe.chc_vcodigo_concepto_hc,
                        oBe.chc_vdescripcion,
                        oBe.chc_nmonto_presup,
                        oBe.chc_nmonto_real,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.chc_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_CONCEPTO_COSTOS_MODIFICAR(
                        oBe.chc_icod_detalle_hc,
                        oBe.chc_iitem_orden,
                        oBe.chc_vcodigo_concepto_hc,
                        oBe.chc_vdescripcion,
                        oBe.chc_nmonto_presup,
                        oBe.chc_nmonto_real,
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
        public void eliminarConceptosCostos(EConceptosCostos oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_CONCEPTO_COSTOS_ELIMINAR(
                        oBe.chc_icod_detalle_hc,
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
        #region Concepto Costos Detalle
        public List<EConceptosCostosDetalle> listarConceptosCostosDetalle(int SubConcepto)
        {
            List<EConceptosCostosDetalle> lista = new List<EConceptosCostosDetalle>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_CONCEPTO_COSTOS_DETALLE_LISTAR(SubConcepto);
                    foreach (var item in query)
                    {
                        lista.Add(new EConceptosCostosDetalle()
                        {
                            chcd_icod_subconcepto_hc = item.chcd_icod_subconcepto_hc,
                            chcd_iitem_orden = item.chcd_iitem_orden,
                            hjcc_icod_hoja_costo = Convert.ToInt32(item.hjcc_icod_hoja_costo),
                            chc_iiten = item.chc_iiten,
                            chcd_vcodigo_concepto_hc = item.chcd_vcodigo_concepto_hc,
                            chcd_vdescripcion = item.chcd_vdescripcion,
                            chcd_nmonto_presup = Convert.ToDecimal(item.chcd_nmonto_presup),
                            chcd_nmonto_real = Convert.ToDecimal(item.chcd_nmonto_real)

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
        public List<EConceptosCostosDetalle> listarConceptosCostosDetalle2()
        {
            List<EConceptosCostosDetalle> lista = new List<EConceptosCostosDetalle>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_CONCEPTO_COSTOS_DETALLE2_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EConceptosCostosDetalle()
                        {
                            chcd_icod_subconcepto_hc = item.chcd_icod_subconcepto_hc,
                            chcd_iitem_orden = item.chcd_iitem_orden,
                            hjcc_icod_hoja_costo = Convert.ToInt32(item.hjcc_icod_hoja_costo),
                            chc_iiten = item.chc_iiten,
                            chcd_vcodigo_concepto_hc = item.chcd_vcodigo_concepto_hc,
                            chcd_vdescripcion = item.chcd_vdescripcion,
                            chcd_nmonto_presup = Convert.ToDecimal(item.chcd_nmonto_presup),
                            chcd_nmonto_real = Convert.ToDecimal(item.chcd_nmonto_real)

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
        public int insertarConceptosCostosDetalle(EConceptosCostosDetalle oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_CONCEPTO_COSTOS_DETALLE_INSERTAR(
                        ref intIcod,
                        oBe.chcd_iitem_orden,
                        oBe.hjcc_icod_hoja_costo,
                        oBe.chc_icod_concepto_hc,
                        oBe.chc_iiten,
                        oBe.chcd_vcodigo_concepto_hc,
                        oBe.chcd_vdescripcion,
                        oBe.chcd_nmonto_presup,
                        oBe.chcd_nmonto_real,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.chcd_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_CONCEPTO_COSTOS_DETALLE_MODIFICAR(
                        oBe.chcd_icod_subconcepto_hc,
                        oBe.chcd_iitem_orden,
                        oBe.chcd_vcodigo_concepto_hc,
                        oBe.chcd_vdescripcion,
                        oBe.chcd_nmonto_presup,
                        oBe.chcd_nmonto_real,
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
        public void eliminarConceptosCostosDetalle(EConceptosCostosDetalle oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_CONCEPTO_COSTOS_DETALLE_ELIMINAR(
                        oBe.chcd_icod_subconcepto_hc,
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
        #region Suma Cantidad Requeridad
        public void ActualizarCantidadRequerida(int hjcd3_icod_rubros_hc, int prdc_icod_producto)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.ACTUALIZAR_CANTIDAD_REQUERIDA_RQM(hjcd3_icod_rubros_hc, prdc_icod_producto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Listar Cantidad Requerida RQM
        public decimal listarCantidadRequeidaRQM(int hjcd3_icod_rubros_hc, int prdc_icod_producto)
        {
            decimal? Cantidad_Saldo = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_CANTIDAD_REQUERIDA_RQM(
                        ref Cantidad_Saldo,
                        hjcd3_icod_rubros_hc,
                        prdc_icod_producto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Convert.ToDecimal(Cantidad_Saldo);
        }
        #endregion
        #region Garantia Clientes
        public List<EGarantiaClientes> listarGarantiaClientes()
        {
            List<EGarantiaClientes> lista = new List<EGarantiaClientes>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_GARANTIA_CLIENTES_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EGarantiaClientes()
                        {
                            garc_icod_garantia = item.garc_icod_garantia,
                            garc_vnumero_garantia = item.garc_vnumero_garantia,
                            garc_sfecha_garantia = Convert.ToDateTime(item.garc_sfecha_garantia),
                            tablc_iid_situacion = Convert.ToInt32(item.tablc_iid_situacion),
                            Situacion = item.Situacion,
                            cliec_icod_cliente = Convert.ToInt32(item.cliec_icod_cliente),
                            NomClie = item.NomClie,
                            pryc_icod_proyecto = Convert.ToInt32(item.pryc_icod_proyecto),
                            CentroCostos = item.CentroCostos,
                            DesProyecto = item.DesProyecto,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            Moneda = item.Moneda,
                            garc_nmonto = Convert.ToDecimal(item.garc_nmonto),
                            favc_icod_factura = Convert.ToInt32(item.favc_icod_factura),
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
        public int insertarGarantiaClientes(EGarantiaClientes oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_GARANTIA_CLIENTES_INSERTAR(
                        ref intIcod,
                        oBe.garc_vnumero_garantia,
                        oBe.garc_sfecha_garantia,
                        oBe.tablc_iid_situacion,
                        oBe.cliec_icod_cliente,
                        oBe.pryc_icod_proyecto,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.garc_nmonto,
                        oBe.favc_icod_factura,
                        oBe.doxpc_icod_correlativo,
                        oBe.pdxpc_icod_correlativo,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.garc_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGarantiaClientes(EGarantiaClientes oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_GARANTIA_CLIENTES_MODIFICAR(
                        oBe.garc_icod_garantia,
                        oBe.garc_vnumero_garantia,
                        oBe.garc_sfecha_garantia,
                        oBe.tablc_iid_situacion,
                        oBe.cliec_icod_cliente,
                        oBe.pryc_icod_proyecto,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.garc_nmonto,
                        oBe.favc_icod_factura,
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
        public void eliminarGarantiaClientes(EGarantiaClientes oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_GARANTIA_CLIENTES_ELIMINAR(
                        oBe.garc_icod_garantia,
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
        #region VentasDaot

        public List<EVentasDaot> ListarVentasDaot(decimal monto, int anio)
        {
            List<EVentasDaot> lista = null;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<EVentasDaot>();
                    var query = dc.SIGTS_VENTAS_DAOT_LISTAR(monto, anio);
                    foreach (var item in query)
                    {
                        string[] nombres = { string.Empty, string.Empty };
                        string app = string.Empty, apm = string.Empty;
                        if (item.cliec_vnombres != null && item.tip_doc_cliente != null && item.tip_doc_cliente == 1)
                        {
                            nombres[0] = item.cliec_vnombres.Split(' ')[0];
                            nombres[0] = (item.cliec_vnombres.Split(' ').Count() == 2) ? item.cliec_vnombres.Split(' ')[1] : string.Empty;
                            app = item.cliec_vapellido_paterno;
                            apm = item.cliec_vapellido_materno;
                        }
                        lista.Add(new EVentasDaot()
                        {
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            cliec_vcod_cliente = item.cliec_vcod_cliente,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            valor_venta_dolares = item.valor_venta_dolares,
                            valor_venta_soles = item.valor_venta_soles,
                            tip_doc_cliente = item.tip_doc_cliente,
                            tipo_persona = item.tipo_persona,
                            num_doc_cliente = item.num_doc_cliente,
                            cliec_vnombre1 = nombres[0],
                            cliec_vnombre2 = nombres[1],
                            cliec_vapellido_paterno = app,
                            cliec_vapellido_materno = apm
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

        public List<EVentasDaotDetalle> ListarVentasDaotDetallexCliente(long proc_icod_proveedor, int anio)
        {
            List<EVentasDaotDetalle> lista = null;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<EVentasDaotDetalle>();
                    var query = dc.SIGTS_VENTAS_DAOT_DETALLE_X_CLIENTE_LISTAR(proc_icod_proveedor, anio);
                    foreach (var item in query)
                    {
                        lista.Add(new EVentasDaotDetalle()
                        {
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            simboloMoneda = (item.tablc_iid_tipo_moneda == 1) ? "S/." : "US$",
                            doxcc_nmonto_tipo_cambio = item.doxcc_nmonto_tipo_cambio,
                            doxcc_nmonto_total = item.doxcc_nmonto_total,
                            valor_venta_soles = item.valor_venta_soles,
                            valor_venta_dolares = item.valor_venta_dolares,
                            valor_venta_dolares_str = (item.tablc_iid_tipo_moneda == 1) ? string.Empty : Convert.ToDecimal(item.valor_venta_dolares).ToString("n2"),
                            valor_venta_mond_orig = item.valor_venta_mond_orig,
                            doxcc_vdescrip_transaccion = item.doxcc_vdescrip_transaccion
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

        public List<EVentasDaotDetalle> ListarVentasDaotDetalle(decimal monto, int anio)
        {
            List<EVentasDaotDetalle> lista = null;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<EVentasDaotDetalle>();
                    var query = dc.SIGTS_VENTAS_DAOT_DETALLE_LISTAR(monto, anio);
                    foreach (var item in query)
                    {
                        lista.Add(new EVentasDaotDetalle()
                        {
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            simboloMoneda = (item.tablc_iid_tipo_moneda == 1) ? "S/." : "US$",
                            doxcc_nmonto_tipo_cambio = item.doxcc_nmonto_tipo_cambio,
                            doxcc_nmonto_total = item.doxcc_nmonto_total,
                            valor_venta_soles = item.valor_venta_soles,
                            valor_venta_dolares = item.valor_venta_dolares,
                            valor_venta_dolares_str = (item.tablc_iid_tipo_moneda == 1) ? string.Empty : Convert.ToDecimal(item.valor_venta_dolares).ToString("n2"),
                            valor_venta_mond_orig = item.valor_venta_mond_orig,
                            doxcc_vdescrip_transaccion = item.doxcc_vdescrip_transaccion,
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
        #region Hoja de Costo Suministro
        public List<ESuministros> listarHojaCostosSuministros(int Concepto)
        {
            List<ESuministros> lista = new List<ESuministros>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_HOJA_COSTOS_SUMINISTROS_LISTAR(Concepto);
                    foreach (var item in query)
                    {
                        lista.Add(new ESuministros()
                        {
                            sumd_icod_suministros_hc = item.sumd_icod_suministros_hc,
                            hjcd1_icod_detalle_hc = Convert.ToInt32(item.hjcd1_icod_detalle_hc),
                            sumd_vpartida = item.sumd_vpartida,
                            sumd_vdescripcion = item.sumd_vdescripcion,
                            sumd_vunidad_medida = item.sumd_vunidad_medida,
                            sumd_icantidad = Convert.ToInt32(item.sumd_icantidad),
                            sumd_precio_unitario = Convert.ToDecimal(item.sumd_precio_unitario),
                            sumd_precio_total = Convert.ToDecimal(item.sumd_precio_total)

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
        public int insertarHojaCostosSuministros(ESuministros oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_SUMINISTROS_INSERTAR(
                        ref intIcod,
                        oBe.hjcd1_icod_detalle_hc,
                        oBe.sumd_vpartida,
                        oBe.sumd_vdescripcion,
                        oBe.sumd_vunidad_medida,
                        oBe.sumd_icantidad,
                        oBe.sumd_precio_unitario,
                        oBe.sumd_precio_total,
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
        public void modificarHojaCostosSuministros(ESuministros oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_SUMINISTROS_MODIFICAR(
                        oBe.sumd_icod_suministros_hc,
                        oBe.hjcd1_icod_detalle_hc,
                        oBe.sumd_vpartida,
                        oBe.sumd_vdescripcion,
                        oBe.sumd_vunidad_medida,
                        oBe.sumd_icantidad,
                        oBe.sumd_precio_unitario,
                        oBe.sumd_precio_total,
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
        public void eliminarHojaCostosSuministros(ESuministros oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_SUMINISTROS_ELIMINAR(
                        oBe.sumd_icod_suministros_hc,
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
        #region Hoja de Costo Servicios
        public List<EServicios> listarHojaCostosServicios(int Concepto)
        {
            List<EServicios> lista = new List<EServicios>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_HOJA_COSTOS_SERVICIOS_LISTAR(Concepto);
                    foreach (var item in query)
                    {
                        lista.Add(new EServicios()
                        {
                            servd_icod_servicios = item.servd_icod_servicios,
                            hjcd1_icod_detalle_hc = Convert.ToInt32(item.hjcd1_icod_detalle_hc),
                            servd_vdescripcion = item.servd_vdescripcion,
                            servd_vunidad_medida = item.servd_vunidad_medida,
                            servd_icantidad = Convert.ToInt32(item.servd_icantidad),
                            servd_precio_unitario = Convert.ToDecimal(item.servd_precio_unitario),
                            servd_precio_total = Convert.ToDecimal(item.servd_precio_total)

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
        public int insertarHojaCostosServicios(EServicios oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_SERVICIOS_INSERTAR(
                        ref intIcod,
                        oBe.hjcd1_icod_detalle_hc,
                        oBe.servd_vdescripcion,
                        oBe.servd_vunidad_medida,
                        oBe.servd_icantidad,
                        oBe.servd_precio_unitario,
                        oBe.servd_precio_total,
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
        public void modificarHojaCostosServicios(EServicios oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_SERVICIOS_MODIFICAR(
                        oBe.servd_icod_servicios,
                        oBe.hjcd1_icod_detalle_hc,
                        oBe.servd_vdescripcion,
                        oBe.servd_vunidad_medida,
                        oBe.servd_icantidad,
                        oBe.servd_precio_unitario,
                        oBe.servd_precio_total,
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
        public void eliminarHojaCostosServicios(EServicios oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_SERVICIOS_ELIMINAR(
                        oBe.servd_icod_servicios,
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
        #region Hoja de Costo Gastos Administrativos
        public List<EGastosAdministrativos> listarHojaCostosGastosAdministrativos(int Concepto)
        {
            List<EGastosAdministrativos> lista = new List<EGastosAdministrativos>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_HOJA_COSTOS_GASTOS_ADMINISTRATIVOS_LISTAR(Concepto);
                    foreach (var item in query)
                    {
                        lista.Add(new EGastosAdministrativos()
                        {
                            gadm_icod_gastos_administrativos = item.gadm_icod_gastos_administrativos,
                            hjcd1_icod_detalle_hc = Convert.ToInt32(item.hjcd1_icod_detalle_hc),
                            gadm_vpartida = item.gadm_vpartida,
                            gadm_vdescripcion = item.gadm_vdescripcion,
                            gadm_vunidad_medida = item.gadm_vunidad_medida,
                            gadm_icantidad = Convert.ToInt32(item.gadm_icantidad),
                            gadm_precio_unitario = Convert.ToDecimal(item.gadm_precio_unitario),
                            gadm_precio_total = Convert.ToDecimal(item.gadm_precio_total)

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
        public int insertarHojaCostosGastosAdministrativos(EGastosAdministrativos oBe)
        {
            try
            {
                int? intIcod = 0;
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_GASTOS_ADMINISTRATIVOS_INSERTAR(
                        ref intIcod,
                        oBe.hjcd1_icod_detalle_hc,
                        oBe.gadm_vpartida,
                        oBe.gadm_vdescripcion,
                        oBe.gadm_vunidad_medida,
                        oBe.gadm_icantidad,
                        oBe.gadm_precio_unitario,
                        oBe.gadm_precio_total,
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
        public void modificarHojaCostosGastosAdministrativos(EGastosAdministrativos oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_GASTOS_ADMINISTRATIVOS_MODIFICAR(
                        oBe.gadm_icod_gastos_administrativos,
                        oBe.hjcd1_icod_detalle_hc,
                        oBe.gadm_vpartida,
                        oBe.gadm_vdescripcion,
                        oBe.gadm_vunidad_medida,
                        oBe.gadm_icantidad,
                        oBe.gadm_precio_unitario,
                        oBe.gadm_precio_total,
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
        public void eliminarHojaCostosGastosAdministrativos(EGastosAdministrativos oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_HOJA_COSTOS_GASTOS_ADMINISTRATIVOS_ELIMINAR(
                        oBe.gadm_icod_gastos_administrativos,
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

        public List<EPuntoVenta> getCorrelativoPVT(int intPVT)
        {
            string strSerieFactura = "";
            int? intCorrelativoFactura = 0;
            string strSerieFacturaSuministros = "";
            int? intCorrelativoFacturaSuministros = 0;
            string strSerieFacturaAlquileres = "";
            int? intCorrelativoFacturaAlquileres = 0;
            string strSerieBoleta = "";
            int? intCorrelativoBoleta = 0;
            string strSerieNotaFCredito = "";
            string strSerieNotaBCredito = "";
            int? intCorrelativoNotaCredito = 0;
            string strSerieNotaFDebito = "";
            string strSerieNotaBDebito = "";
            int? intCorrelativoNotaDebito = 0;
            List<EPuntoVenta> lst = new List<EPuntoVenta>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGE_GET_CORRELATIVO_PVT(
                    ref strSerieFactura,
                    ref intCorrelativoFactura,
                    ref strSerieFacturaSuministros,
                    ref intCorrelativoFacturaSuministros,
                    ref strSerieFacturaAlquileres,
                    ref intCorrelativoFacturaAlquileres,
                    ref strSerieBoleta,
                    ref intCorrelativoBoleta,
                    ref strSerieNotaFCredito,
                    ref strSerieNotaBCredito,
                    ref intCorrelativoNotaCredito,
                    ref strSerieNotaFDebito,
                    ref strSerieNotaBDebito,
                    ref intCorrelativoNotaDebito,
                    intPVT);

                    lst.Add(new EPuntoVenta()
                    {
                        puvec_vserie_factura = strSerieFactura,
                        puvec_icorrelativo_factura = Convert.ToInt32(intCorrelativoFactura),
                        puvec_vserie_factura_suministros = strSerieFacturaSuministros,
                        puvec_icorrelativo_factura_suministros = Convert.ToInt32(intCorrelativoFacturaSuministros),
                        puvec_vserie_factura_alquileres = strSerieFacturaAlquileres,
                        puvec_icorrelativo_factura_alquileres = Convert.ToInt32(intCorrelativoFacturaAlquileres),
                        puvec_vserie_boleta = strSerieBoleta,
                        puvec_icorrelativo_boleta = Convert.ToInt32(intCorrelativoBoleta),
                        puvec_vserie_notaF_credito = strSerieNotaFCredito,
                        puvec_vserie_notaB_credito = strSerieNotaBCredito,
                        puvec_icorrelativo_nota_credito = Convert.ToInt32(intCorrelativoNotaCredito),
                        puvec_vserie_notaF_debito = strSerieNotaFDebito,
                        puvec_vserie_notaB_debito = strSerieNotaBDebito,
                        puvec_icorrelativo_nota_debito = Convert.ToInt32(intCorrelativoNotaDebito),
                    });

                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EParametroProduccion> getCorrelativoOP(int intPD)
        {
            int? intCorrelativoOrdenPedido = 0;
            int? intCorrelativoFichaTecnica = 0;
            int? intCorrelativoOrdenProduccion = 0;
            int? intCorrelativoNotaPedido = 0;
            List<EParametroProduccion> lst = new List<EParametroProduccion>();
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGE_PVT_GET_CORRELATIVO_OP(
                    ref intCorrelativoOrdenPedido,
                    ref intCorrelativoFichaTecnica,
                    ref intCorrelativoOrdenProduccion,
                    ref intCorrelativoNotaPedido,
                    intPD);

                    lst.Add(new EParametroProduccion()
                    {
                        pmprc_inumero_orden_pedido = Convert.ToInt32(intCorrelativoOrdenPedido),
                        pmprc_inumero_ficha_tecnica = Convert.ToInt32(intCorrelativoFichaTecnica),
                        pmprc_inumero_orden_produccion = Convert.ToInt32(intCorrelativoOrdenProduccion),
                        pmprc_inumero_nota_pedido = Convert.ToInt32(intCorrelativoNotaPedido)
                    });

                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Tablas Sunat
        public List<ETablaSunatCab> TablasSunatListar()
        {
            List<ETablaSunatCab> lista = null;

            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<ETablaSunatCab>();
                    var query = dc.SGE_TABLAS_SUNAT_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ETablaSunatCab()
                        {
                            suntc_icod = item.suntc_icod,
                            suntc_codigo = item.suntc_codigo,
                            suntc_vdescripcion = item.suntc_vdescripcion

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
        public void TablasSunatInsertar(ETablaSunatCab obj)
        {

            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TABLAS_SUNAT_INSERTAR(
                     obj.suntc_codigo,
                     obj.suntc_vdescripcion,
                     obj.suntc_iusuario_crea,
                     obj.suntc_vpc_crea
                   );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TABLAS_SUNAT_MODIFICAR(
                        obj.suntc_icod,
                        obj.suntc_vdescripcion,
                        obj.suntc_iusuario_modifica,
                        obj.suntc_vpc_modifica
                    );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TABLAS_SUNAT_ELIMINAR(
                        obj.suntc_icod,
                        obj.suntc_iusuario_elimina,
                        obj.suntc_vpc_elimina
                    );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<ETablaSunatDet>();
                    var query = dc.SGE_TABLAS_SUNAT_DET_LISTAR(icod);
                    foreach (var item in query)
                    {
                        lista.Add(new ETablaSunatDet()
                        {
                            suntc_icod = Convert.ToInt32(item.suntc_icod),
                            suntd_icod = item.suntd_icod,
                            suntd_codigo = item.suntd_codigo,
                            suntd_vdescripcion = item.suntd_vdescripcion

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
        public void TablasSunatDetInsertar(ETablaSunatDet obj)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TABLAS_SUNAT_DET_INSERTAR(
                        obj.suntc_icod,
                        obj.suntd_codigo,
                        obj.suntd_vdescripcion,
                        obj.suntd_iusuario_crea,
                        obj.suntd_vpc_crea

                    );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TABLAS_SUNAT_DET_MODIFICAR(
                        obj.suntd_icod,
                        obj.suntd_codigo,
                        obj.suntd_vdescripcion,
                        obj.suntd_iusuario_modifica,
                        obj.suntd_vpc_modifica
                    );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_TABLAS_SUNAT_DET_ELIMINAR(
                        obj.suntd_icod,
                        obj.suntd_iusuario_eliminar,
                        obj.suntd_vpc_eliminar
                    );
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
            List<EDireccionCliente> lista = new List<EDireccionCliente>();

            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<EDireccionCliente>();
                    var query = dc.SGE_DIRECCION_CLIENTE_LISTAR(codigo);
                    foreach (var item in query)
                    {
                        lista.Add(new EDireccionCliente()
                        {
                            dc_icod_direccion = Convert.ToInt32(item.dc_icod_direccion),
                            cliec_icod_cliente = Convert.ToInt32(item.cliec_icod_cliente),
                            dc_vdireccion = item.dc_vdireccion
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
        public int insertarDireccionCliente(EDireccionCliente oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGE_DIRECCION_CLIENTE_INSERTAR(
                    ref intIcod,
                    oBe.cliec_icod_cliente,
                    oBe.dc_vdireccion,
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
        public void modificarDireccionCliente(EDireccionCliente oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_DIRECCION_CLIENTE_MODIFICAR(
                    oBe.dc_icod_direccion,
                    oBe.cliec_icod_cliente,
                    oBe.dc_vdireccion,
                    oBe.intUsuario,
                    oBe.strPc);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_DIRECCION_CLIENTE_ELIMINAR(
                        oBe.dc_icod_direccion);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Archivos
        public List<ESeries> listarSeries()
        {
            List<ESeries> lista = new List<ESeries>();

            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    lista = new List<ESeries>();
                    var query = dc.SGE_REGISTRO_SERIE_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ESeries()
                        {
                            rs_icod_registro_serie = Convert.ToInt32(item.rs_icod_registro_serie),
                            rs_vserie = item.rs_vserie,
                            rs_icorrelativo = Convert.ToInt32(item.rs_icorrelativo),
                            rs_icod_pvt = Convert.ToInt32(item.rs_icod_pvt),
                            DesPVT = item.DesPVT,
                            rs_itipo_documento = Convert.ToInt32(item.rs_itipo_documento),
                            strTipoDoc = item.strTipoDoc
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
        public int insertarSeries(ESeries oBe)
        {
            int? intIcod = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {

                    dc.SGE_REGISTRO_SERIE_INSERTAR(
                    ref intIcod,
                    oBe.rs_vserie,
                    oBe.rs_icorrelativo,
                    oBe.rs_icod_pvt,
                    oBe.rs_itipo_documento,
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
        public void modificarSeries(ESeries oBe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_REGISTRO_SERIE_MODIFICAR(
                    oBe.rs_icod_registro_serie,
                    oBe.rs_vserie,
                    oBe.rs_icorrelativo,
                    oBe.rs_icod_pvt,
                    oBe.rs_itipo_documento,
                    oBe.intUsuario,
                    oBe.strPc);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_REGISTRO_SERIE_ELIMINAR(
                        oBe.rs_icod_registro_serie,
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



        public void anularNotaDebitoClienteCab(ENotaDebito ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_DEBITO_VENTA_ANULAR(
                        ob.ncrec_icod_credito,
                        ob.intUsuario,
                        ob.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Guia Remision MP Detalle
        public List<EGuiaRemisionMPDet> listarGuiaRemisionMPDet(int remic_icod_remision, int intEjericio)
        {
            List<EGuiaRemisionMPDet> lista = new List<EGuiaRemisionMPDet>(); ;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_GUIA_REMISION_MP_DET_LISTAR(remic_icod_remision, intEjericio);
                    foreach (var item in query)
                    {
                        lista.Add(new EGuiaRemisionMPDet()
                        {
                            dremc_icod_detalle_remision = item.dremc_icod_detalle_remision,
                            remic_icod_remision = Convert.ToInt32(item.remic_icod_remision),
                            dremc_inro_item = item.dremc_inro_item,
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            dremc_ncantidad_producto = item.dremc_ncantidad_producto,
                            dremc_vcantidad_producto = Convert.ToInt32(item.dremc_ncantidad_producto).ToString(),
                            kardc_icod_correlativo = item.kardc_icod_correlativo,
                            strCodProducto = item.strCodProducto,
                            strDesProducto = item.strDesProducto,
                            strDesUM = item.strDesUM,
                            strAbreUM = item.strAbreUM,
                            dremc_vobservaciones = item.dremc_vobservaciones,
                            dblStockDisponible = Convert.ToDecimal(item.dblStockDisponible),
                            kardc_icod_correlativo_ingreso = item.kardc_icod_correlativo_ingreso,
                            dremc_icodigo = item.dremc_icodigo,
                            unidc_icod_unidad_medida = Convert.ToInt32(item.unidc_icod_unidad_medida),
                            CodigoSUNAT = item.CodigoSUNAT,
                            dremc_itipo_producto = item.dremc_itipo_producto,
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
        public int insertarGuiaRemisionMPDet(EGuiaRemisionMPDet ob)
        {
            int? id_guia_remision = 0;
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_GUIA_REMISION_MP_DET_INSERTAR(
                        ref id_guia_remision,
                        ob.remic_icod_remision,
                        ob.dremc_inro_item,
                        ob.prdc_icod_producto,
                        ob.dremc_ncantidad_producto,
                        ob.kardc_icod_correlativo,
                        ob.intUsuario,
                        ob.strPc,
                        ob.dremc_vobservaciones,
                        ob.kardc_icod_correlativo_ingreso,
                        ob.dremc_icodigo,
                        ob.unidc_icod_unidad_medida,
                        ob.dremc_itipo_producto);
                }
                return Convert.ToInt32(id_guia_remision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarGuiaRemisionMPDet(EGuiaRemisionMPDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_GUIA_REMISION_MP_DET_MODIFICAR(
                            ob.dremc_icod_detalle_remision,
                            ob.remic_icod_remision,
                            ob.dremc_inro_item,
                            ob.prdc_icod_producto,
                            ob.dremc_ncantidad_producto,
                            ob.kardc_icod_correlativo,
                            ob.intUsuario,
                            ob.strPc,
                            ob.dremc_vobservaciones,
                            ob.dremc_icodigo,
                            ob.unidc_icod_unidad_medida,
                            ob.dremc_itipo_producto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarGuiaRemisionMPDet(EGuiaRemisionMPDet ob)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_GUIA_REMISION_MP_DET_ELIMINAR(
                            ob.dremc_icod_detalle_remision,
                            ob.intUsuario,
                            ob.strPc);
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_FACTURA_CAB_MOTIVO_BAJA_ACTUALIZAR(obj.favc_icod_factura,
                        obj.favc_descripcion_motivo_baja
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_BOLETA_CAB_MOTIVO_BAJA_ACTUALIZAR(obj.bovc_icod_boleta,
                        obj.bovc_descripcion_motivo_baja
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_NC_CAB_MOTIVO_BAJA_ACTUALIZAR(obj.ncrec_icod_credito,
                        obj.ncrec_descripcion_motivo_baja
                        );
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
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_ND_CAB_MOTIVO_BAJA_ACTUALIZAR(obj.ncrec_icod_credito,
                        obj.ncrec_descripcion_motivo_baja
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void insertarGuiaRemisionElectronica(EGuiaRemisionElectronica obj)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_GUIA_REMISION_ELECTRONICA_INSERTAR(
                        obj.IdDocumento,
                        obj.remic_icod_remision,
                        obj.FechaEmision,
                        obj.HoraEmision,
                        obj.TipoDocumento,
                        obj.Glosa,
                        obj.numDocRemitente,
                        obj.tipDocRemitente,
                        obj.rznSocialRemitente,
                        obj.numDocDestinatario,
                        obj.tipDocDestinatario,
                        obj.rznSocialDestinatario,
                        obj.CodigoMotivoTraslado,
                        obj.DescripcionMotivo,
                        obj.Transbordo,
                        obj.PesoBrutoTotal,
                        obj.UmPesoBruto,
                        obj.NumBultos,
                        obj.NroPallets,
                        obj.ModalidadTraslado,
                        obj.FechaInicioTraslado,
                        obj.RucTransportista,
                        obj.TipoDocumentoTransportista,
                        obj.RazonSocialTransportista,
                        obj.NroPlacaVehiculo,
                        obj.NroDocumentoConductor,
                        obj.ubiLlegada,
                        obj.dirLlegada,
                        obj.ubiPartida,
                        obj.dirPartida,
                        obj.obsGuia,
                        obj.NumeroContenedor,
                        obj.CodigoPuerto,
                        obj.ShipmentId,
                        obj.ObsServaciones

                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void insertarGuiaRemisionDetElectronica(EGuiaRemisionElectronicaDet obe)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_GUIA_REMISION_ELECTRONICA_DET_INSERTAR(
                    obe.remic_icod_remision,
                    obe.Correlativo,
                    obe.CodigoItem,
                    obe.Descripcion,
                    obe.UnidadMedida,
                    obe.Cantidad,
                    obe.PesoItem,
                    obe.LineaReferencia,
                    obe.UM

                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarGuiaRemisionElectronica(int remic_icod_remision)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_GUIA_REMISION_ELECTRONICA_ELIMINAR(remic_icod_remision);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarGuiaRemisionElectronicaDet(int remic_icod_remision)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGE_GUIA_REMISION_ELECTRONICA_DET_ELIMINAR(remic_icod_remision);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConductorGuardar(EConductor obj)
        {
            try
            {
                using (VentasDataContext dc = new VentasDataContext(Helper.conexion()))
                {
                    dc.SGEV_DATOS_CONDUCTOR_GUARDAR(
                        obj.remic_icod_remision,
                        obj.cod_vdni,
                        obj.cod_vnombres,
                        obj.cod_vapellidos,
                        obj.cod_vlicencia

                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
