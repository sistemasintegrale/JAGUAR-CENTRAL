using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SGE.DataAccess
{
    public class AlmacenData
    {
        #region Almacenes
        public List<EAlmacen> listarAlmacenes()
        {
            List<EAlmacen> lista = new List<EAlmacen>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {

                    var query = dc.SGEA_ALMACEN_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EAlmacen()
                        {
                            almac_icod_almacen = item.almac_icod_almacen,
                            almac_iid_almacen = Convert.ToInt32(item.almac_iid_almacen),
                            almac_vdescripcion = item.almac_vdescripcion,
                            almac_vdireccion = item.almac_vdireccion,
                            almac_vrepresentante = item.almac_vrepresentante,
                            almac_sflag_estado = Convert.ToBoolean(item.almac_sflag_estado),
                            almac_tipo_event = Convert.ToInt32(item.almac_tipo_event),
                            almac_iestado_almacen = Convert.ToInt32(item.almac_iestado_almacen),
                            DescripcionEstaAlmacen = item.DescripcionEstaAlmacen,
                            pryc_icod_proyecto = Convert.ToInt32(item.pryc_icod_proyecto),
                            cecoc_icod_centro_costo = Convert.ToInt32(item.cecoc_icod_centro_costo),
                            pryc_vdescripcion = item.pryc_vdescripcion,
                            TipoEventos = item.TipoEventos,
                            CodCC = item.cecoc_vcodigo_centro_costo,
                            almac_vubigeo = item.almac_vubigeo

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
        public List<EKardex> listarkardex()
        {
            List<EKardex> lista = new List<EKardex>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {

                    var query = dc.SGEA_KARDEX_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EKardex()
                        {
                            kardc_icod_correlativo = item.kardc_icod_correlativo,
                            kardc_ianio = item.kardc_ianio,
                            almac_icod_almacen = item.almac_icod_almacen,
                            prdc_icod_producto = item.prdc_icod_producto
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

        public void EntregaMaterialesGuardar(EEntregaMaterial obj)
        {
            int? icodRef = obj.icod_entrega_material;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGE_ENTREGA_MATERIAL_SAVE(
                        ref icodRef,
                        obj.rqmad_icod_item_requerimiento_material,
                        obj.em_sfecha_entrega,
                        obj.prdc_icod_producto,
                        obj.em_ncantidad,
                        obj.perc_icod_personal,
                        obj.intUsuario,
                        DateTime.Now,
                        obj.strPc,
                        obj.intUsuario,
                        DateTime.Now,
                        obj.strPc,
                        obj.IdKardex
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<EAlmacen> listarAlmacenesGR()
        {
            List<EAlmacen> lista = new List<EAlmacen>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {

                    var query = dc.SGEA_ALMACEN_LISTAR_GR();
                    foreach (var item in query)
                    {
                        lista.Add(new EAlmacen()
                        {
                            almac_icod_almacen = item.almac_icod_almacen,
                            almac_iid_almacen = Convert.ToInt32(item.almac_iid_almacen),
                            almac_vdescripcion = item.almac_vdescripcion,
                            almac_vdireccion = item.almac_vdireccion,
                            almac_vrepresentante = item.almac_vrepresentante,
                            almac_sflag_estado = Convert.ToBoolean(item.almac_sflag_estado),
                            almac_tipo_event = Convert.ToInt32(item.almac_tipo_event),
                            almac_iestado_almacen = Convert.ToInt32(item.almac_iestado_almacen),
                            DescripcionEstaAlmacen = item.DescripcionEstaAlmacen,
                            cecoc_icod_centro_costo = Convert.ToInt32(item.cecoc_icod_centro_costo),
                            CodCC = item.cecoc_vcodigo_centro_costo
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

        public EEntregaMaterial EntregaMaterialesGeyById(int icod_entrega_material)
        {
            var obj = new EEntregaMaterial();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var collection = dc.SGE_ENTREGA_MATERIAL_GET_BY_ID(icod_entrega_material);
                    foreach (var item in collection)
                    {
                        obj.icod_entrega_material = item.icod_entrega_material;
                        obj.rqmad_icod_item_requerimiento_material = item.rqmad_icod_item_requerimiento_material;
                        obj.em_sfecha_entrega = item.em_sfecha_entrega;
                        obj.prdc_icod_producto = item.prdc_icod_producto;
                        obj.em_ncantidad = item.em_ncantidad;
                        obj.perc_icod_personal = item.perc_icod_personal;
                        obj.CodigoProducto = item.CodigoProducto;
                        obj.DescripcionProducto = item.DescripcionProducto;
                        obj.IdUnidadMedida = Convert.ToInt32(item.IdUnidadMedida);
                        obj.NombrePersonal = item.NombrePersonal;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return obj;
        }

        public List<EAlmacen> listarAlmacenesProyectos()
        {
            List<EAlmacen> lista = new List<EAlmacen>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {

                    var query = dc.SGEA_ALMACEN_LISTAR_PROYECTOS();
                    foreach (var item in query)
                    {
                        lista.Add(new EAlmacen()
                        {
                            almac_icod_almacen = item.almac_icod_almacen,
                            almac_iid_almacen = Convert.ToInt32(item.almac_iid_almacen),
                            almac_vdescripcion = item.almac_vdescripcion,
                            almac_vdireccion = item.almac_vdireccion,
                            almac_vrepresentante = item.almac_vrepresentante,
                            almac_sflag_estado = Convert.ToBoolean(item.almac_sflag_estado),
                            almac_tipo_event = Convert.ToInt32(item.almac_tipo_event),
                            almac_iestado_almacen = Convert.ToInt32(item.almac_iestado_almacen),
                            DescripcionEstaAlmacen = item.DescripcionEstaAlmacen,
                            pryc_icod_proyecto = Convert.ToInt32(item.pryc_icod_proyecto)
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
        public int insertarAlmacen(EAlmacen oBe)
        {
            int? intIcod = 0;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_ALMACEN_INSERTAR(
                        ref intIcod,
                        oBe.almac_iid_almacen,
                        oBe.almac_vdescripcion,
                        oBe.almac_vdireccion,
                        oBe.almac_vrepresentante,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.almac_sflag_estado,
                        oBe.almac_tipo_event,
                        oBe.almac_iestado_almacen,
                        oBe.pryc_icod_proyecto,
                        oBe.cecoc_icod_centro_costo,
                        oBe.almac_vubigeo
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarAlmacen(EAlmacen oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_ALMACEN_MODIFICAR(
                        oBe.almac_icod_almacen,
                        oBe.almac_vdescripcion,
                        oBe.almac_vdireccion,
                        oBe.almac_vrepresentante,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.almac_tipo_event,
                        oBe.almac_iestado_almacen,
                        oBe.pryc_icod_proyecto,
                        oBe.cecoc_icod_centro_costo,
                        oBe.almac_vubigeo
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarAlmacen(EAlmacen oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_ALMACEN_ELIMINAR(
                        oBe.almac_icod_almacen,
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
        #region Unidad Medida
        public List<EUnidadMedida> listarUnidadMedida()
        {
            List<EUnidadMedida> lista = new List<EUnidadMedida>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_UNIDAD_MEDIDA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EUnidadMedida()
                        {
                            unidc_icod_unidad_medida = item.unidc_icod_unidad_medida,
                            unidc_iid_unidad_medida = Convert.ToInt32(item.unidc_iid_unidad_medida),
                            unidc_vabreviatura_unidad_medida = item.unidc_vabreviatura_unidad_medida,
                            unidc_vdescripcion = item.unidc_vdescripcion,
                            unidc_vcodigo_sunat = item.unidc_vcodigo_sunat,
                            unidc_sflag_estado = Convert.ToBoolean(item.unidc_sflag_estado),
                            unidc_cod_unid_med = item.unidc_cod_unid_med
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
        public int insertarUnidadMedida(EUnidadMedida oBe)
        {
            int? intIcod = 0;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_UNIDAD_MEDIDA_INSERTAR(
                        ref intIcod,
                        oBe.unidc_iid_unidad_medida,
                        oBe.unidc_vabreviatura_unidad_medida,
                        oBe.unidc_vdescripcion,
                        oBe.unidc_vcodigo_sunat,
                        oBe.intUsuario,
                        oBe.unidc_sflag_estado,
                        oBe.unidc_cod_unid_med
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarUnidadMedida(EUnidadMedida oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_UNIDAD_MEDIDA_MODIFICAR(
                        oBe.unidc_icod_unidad_medida,
                        oBe.unidc_vabreviatura_unidad_medida,
                        oBe.unidc_vdescripcion,
                        oBe.unidc_vcodigo_sunat,
                        oBe.intUsuario,
                        oBe.unidc_cod_unid_med
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarUnidadMedida(EUnidadMedida oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_UNIDAD_MEDIDA_ELIMINAR(
                        oBe.unidc_icod_unidad_medida,
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
        #region Familia Cab
        public List<EFamiliaCab> listarFamiliaCab()
        {
            List<EFamiliaCab> lista = new List<EFamiliaCab>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_FAMILIA_CAB_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EFamiliaCab()
                        {
                            famic_icod_familia = item.famic_icod_familia,
                            famic_vabreviatura = item.famic_vabreviatura,
                            famic_vdescripcion = item.famic_vdescripcion,
                            famic_flag_estado = Convert.ToBoolean(item.famic_flag_estado),
                            clasc_icod_clasificacion = item.clasc_icod_clasificacion,
                            strClasificacion = item.strClasificacion,
                            famic_icod_tipo = Convert.ToInt32(item.famic_icod_tipo),
                            Tipo = item.Tipo,
                            almcc_icod_almacen = Convert.ToInt32(item.almcc_icod_almacen),
                            almcc_vdescripcion = item.almcc_vdescripcion,
                            famic_flag_serie = Convert.ToBoolean(item.famic_flag_serie),
                            famic_vtipo_existencia = item.famic_vtipo_existencia
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
        public int insertarFamiliaCab(EFamiliaCab oBe)
        {
            int? intIcod = 0;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_FAMILIA_CAB_INSERTAR(
                        ref intIcod,
                        oBe.famic_vabreviatura,
                        oBe.famic_vdescripcion,
                        oBe.intUsuario,
                        oBe.famic_flag_estado,
                        oBe.clasc_icod_clasificacion,
                        oBe.famic_icod_tipo,
                        oBe.almcc_icod_almacen,
                        oBe.famic_flag_serie,
                        oBe.famic_vtipo_existencia
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFamiliaCab(EFamiliaCab oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_FAMILIA_CAB_MODIFICAR(
                        oBe.famic_icod_familia,
                        oBe.famic_vabreviatura,
                        oBe.famic_vdescripcion,
                        oBe.intUsuario,
                        oBe.clasc_icod_clasificacion,
                        oBe.famic_icod_tipo,
                        oBe.almcc_icod_almacen,
                        oBe.famic_flag_serie,
                        oBe.famic_vtipo_existencia
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFamiliaCab(EFamiliaCab oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_FAMILIA_CAB_ELIMINAR(
                        oBe.famic_icod_familia,
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
        #region Familia Det
        public List<EFamiliaDet> listarFamiliaDet(int intIcodFamiliaCab)
        {
            List<EFamiliaDet> lista = new List<EFamiliaDet>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_FAMILIA_DET_LISTAR(intIcodFamiliaCab);
                    foreach (var item in query)
                    {
                        lista.Add(new EFamiliaDet()
                        {
                            famid_icod_familia_tipo = item.famid_icod_familia_tipo,
                            famic_icod_familia = item.famic_icod_familia,
                            famid_vabreviatura = item.famid_vabreviatura,
                            famid_vdescripcion = item.famid_vdescripcion,
                            famid_flag_estado = Convert.ToBoolean(item.famid_flag_estado),
                            /*-----------------------------------------------------------*/
                            strAbreviaturaFamCab = item.famic_vabreviatura,
                            strDescripcionFamCab = item.famic_vdescripcion,
                            cuenta_iservicio_tercero = item.cuenta_iservicio_tercero,
                            cuenta_iservicio_propio = item.cuenta_iservicio_propio,
                            NumeroCuentaSerTer = item.NumeroCuentaSerTer,
                            DesCuentaSerTer = item.DesCuentaSerTer,
                            NumeroCuentaSerPro = item.NumeroCuentaSerPro,
                            DesCuentaSerPro = item.DesCuentaSerPro
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
        public int insertarFamiliaDet(EFamiliaDet oBe)
        {
            int? intIcod = 0;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_FAMILIA_DET_INSERTAR(
                        ref intIcod,
                        oBe.famic_icod_familia,
                        oBe.famid_vabreviatura,
                        oBe.famid_vdescripcion,
                        oBe.cuenta_iservicio_tercero,
                        oBe.cuenta_iservicio_propio,
                        oBe.intUsuario,
                        oBe.famid_flag_estado
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFamiliaDet(EFamiliaDet oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_FAMILIA_DET_MODIFICAR(
                        oBe.famid_icod_familia_tipo,
                        oBe.famid_vabreviatura,
                        oBe.famid_vdescripcion,
                        oBe.cuenta_iservicio_tercero,
                        oBe.cuenta_iservicio_propio,
                        oBe.intUsuario
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFamiliaDet(EFamiliaDet oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_FAMILIA_DET_ELIMINAR(
                        oBe.famid_icod_familia_tipo,
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

        #region Producto
        public List<EProducto> listarProductoPrecios(int intEjercicio)
        {
            List<EProducto> lista = new List<EProducto>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_COMPRAS_PRECIOS_PRODUCTOS(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EProducto()
                        {
                            prdc_icod_producto = Convert.ToInt32(item.prd_icod_producto),
                            prdc_vcode_producto = item.prdc_vcode_producto,
                            prdc_vdescripcion_larga = item.prdc_vdescripcion_larga,
                            StrUnidadMedida = item.unidc_vabreviatura,
                            dcmlCostoSol = Convert.ToDecimal(item.dcmlCostoSol),
                            dcmlCostoDol = Convert.ToDecimal(item.dcmlCostoDol)
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
        public List<EDocCompraDet> listarUltimoPreciosDetalle(int intProducto, int intEjercicio)
        {
            List<EDocCompraDet> lista = new List<EDocCompraDet>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_ULTIMO_PRECIO_COMPRA(intProducto, intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocCompraDet()
                        {
                            strProveedor = item.strDesProveedor,
                            facd_ncantidad = item.facd_ncantidad,
                            strMoneda = item.strTipoMoneda,
                            facd_nmonto_unit = item.dcmlPrecioUnitario,
                            facd_nmonto_total = Convert.ToDecimal(item.dcmlPrecioTotal),
                            strTipoDoc = item.strTipoDoc,
                            strNroDoc = item.facc_num_doc,
                            dtFecha = Convert.ToDateTime(item.facc_sfecha_doc)
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
        public List<EDocCompraDet> listarProductoPreciosDetalle(int intProducto, int intEjercicio)
        {
            List<EDocCompraDet> lista = new List<EDocCompraDet>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_COMPRAS_PRECIOS_PRODUCTOS_DETALLE(intProducto, intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocCompraDet()
                        {
                            strProveedor = item.strDesProveedor,
                            facd_ncantidad = item.facd_ncantidad,
                            strMoneda = item.strTipoMoneda,
                            facd_nmonto_unit = item.dcmlPrecioUnitario,
                            facd_nmonto_total = Convert.ToDecimal(item.dcmlPrecioTotal),
                            strTipoDoc = item.strTipoDoc,
                            strNroDoc = item.facc_num_doc,
                            dtFecha = Convert.ToDateTime(item.facc_sfecha_doc)
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
        public List<EStock> listaricodstock()
        {
            List<EStock> lista = new List<EStock>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_ICOD_STOCK_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EStock()
                        {
                            stocc_icod_stock_producto = Convert.ToInt32(item.stocc_icod_stock_producto),
                            stocc_ianio = Convert.ToInt32(item.stocc_ianio),
                            stocc_iid_almacen = Convert.ToInt32(item.stocc_iid_almacen),
                            stocc_iid_producto = Convert.ToInt32(item.stocc_iid_producto),
                            stocc_nstock_prod = Convert.ToDecimal(item.stocc_nstock_prod),
                            puvec_icod_punto_venta = Convert.ToInt32(item.puvec_icod_punto_venta),
                            stocc_codigo_producto = item.stocc_codigo_producto,
                            stocc_nprecio_costo_prom_prod = Convert.ToDecimal(item.stocc_nprecio_costo_prom_prod),
                            stocc_ntotal_costo_prod = Convert.ToDecimal(item.stocc_ntotal_costo_prod)
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
        public void modificaricodstock(EStock oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_ICOD_STOCK_MODIFICAR(
                        oBe.stocc_icod_stock_producto
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EProducto> listarProducto(int anio)
        {
            List<EProducto> lista = new List<EProducto>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_PRODUCTO_LISTAR(anio);
                    foreach (var item in query)
                    {
                        lista.Add(new EProducto()
                        {
                            prdc_icod_producto = item.prdc_icod_producto,
                            prdc_vcode_producto = item.prdc_vcode_producto,
                            prdc_vdescripcion_larga = item.prdc_vdescripcion_larga,
                            prdc_vdescripcion_corta = item.prdc_vdescripcion_corta,
                            famic_icod_familia = item.famic_icod_familia,
                            famid_icod_familia = item.famid_icod_familia,
                            unidc_icod_unidad_medida = item.unidc_icod_unidad_medida,
                            prdc_isituacion = Convert.ToBoolean(item.prdc_isituacion),
                            prdc_stock_minimo = item.prdc_stock_minimo,
                            prdc_calibre = Convert.ToDecimal(item.prdc_calibre),
                            tarec_icorrelativo_colorprod = Convert.ToInt32(item.tarec_icorrelativo_colorprod),
                            prdc_icod_proveedor = Convert.ToInt32(item.prdc_icod_proveedor),
                            prdc_precio_costo = Convert.ToDecimal(item.prdc_precio_costo),
                            strEstado = (Convert.ToBoolean(item.prdc_isituacion) == true) ? "ACTIVO" : "INACTIVO",
                            prdc_nMonto_stock = Convert.ToDecimal(item.prdc_nMonto_stock),
                            StrUnidadMedida = item.StrUnidadMedida,
                            prdc_vmarca = item.prdc_vmarca,
                            prdc_vmodelo = item.prdc_vmodelo,
                            prdc_vparte = item.prdc_vparte,
                            prdc_vserie = item.prdc_vserie,
                            prdc_vcaracteristica = item.prdc_vcaracteristica,
                            prdc_vcodigo_fabricante = item.prdc_vcodigo_fabricante,
                            ctacc_icod_cuenta_contable = Convert.ToInt32(item.ctacc_icod_cuenta_contable),
                            NumCunetaContable = item.NumCunetaContable,
                            strFamilia = item.famic_vabreviatura,
                            strSubFamilia = item.famid_vabreviatura,
                            strDesFamilia = item.famic_vdescripcion,
                            strDesSubFamilia = item.famid_vdescripcion,
                            flag_select_mod = false,
                            prdc_icod_tipo = Convert.ToInt32(item.prdc_icod_tipo),
                            strcolor = item.strcolor,
                            prdc_italla = Convert.ToInt32(item.prdc_italla),
                            CodigoSUNAT = item.CodigoSUNAT
                            //strTipo = item.strTipo
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
        public List<EProducto> listarProductoCodigoDescrip(int anio, string codigo, string descripcion)
        {
            List<EProducto> lista = new List<EProducto>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_PRODUCTO_LISTAR_CODIGO_DESCRIP(codigo, descripcion, anio);
                    foreach (var item in query)
                    {
                        lista.Add(new EProducto()
                        {
                            prdc_icod_producto = item.prdc_icod_producto,
                            prdc_vcode_producto = item.prdc_vcode_producto,
                            prdc_vdescripcion_larga = item.prdc_vdescripcion_larga,
                            prdc_vdescripcion_corta = item.prdc_vdescripcion_corta,
                            famic_icod_familia = item.famic_icod_familia,
                            famid_icod_familia = item.famid_icod_familia,
                            unidc_icod_unidad_medida = item.unidc_icod_unidad_medida,
                            prdc_isituacion = Convert.ToBoolean(item.prdc_isituacion),
                            prdc_stock_minimo = item.prdc_stock_minimo,
                            strEstado = (Convert.ToBoolean(item.prdc_isituacion) == true) ? "ACTIVO" : "INACTIVO",
                            prdc_nMonto_stock = Convert.ToDecimal(item.prdc_nMonto_stock),
                            StrUnidadMedida = item.StrUnidadMedida,
                            prdc_vmarca = item.prdc_vmarca,
                            prdc_vmodelo = item.prdc_vmodelo,
                            prdc_vparte = item.prdc_vparte,
                            prdc_vserie = item.prdc_vserie,
                            prdc_vcaracteristica = item.prdc_vcaracteristica,
                            prdc_vcodigo_fabricante = item.prdc_vcodigo_fabricante,
                            ctacc_icod_cuenta_contable = Convert.ToInt32(item.ctacc_icod_cuenta_contable),
                            NumCunetaContable = item.NumCunetaContable,
                            strFamilia = item.famic_vabreviatura,
                            strSubFamilia = item.famid_vabreviatura,
                            strDesFamilia = item.famic_vdescripcion,
                            strDesSubFamilia = item.famid_vdescripcion,
                            flag_select_mod = false
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
        public List<EProducto> listarProductoXCodigp(int anio, string prdc_vcode_producto)
        {
            List<EProducto> lista = new List<EProducto>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_PRODUCTO_LISTAR_X_CODIGO(anio, prdc_vcode_producto);
                    foreach (var item in query)
                    {
                        lista.Add(new EProducto()
                        {
                            prdc_icod_producto = item.prdc_icod_producto,
                            prdc_vcode_producto = item.prdc_vcode_producto,
                            prdc_vdescripcion_larga = item.prdc_vdescripcion_larga,
                            prdc_vdescripcion_corta = item.prdc_vdescripcion_corta,
                            unidc_icod_unidad_medida = item.unidc_icod_unidad_medida,
                            prdc_bAfecto = item.prdc_bAfecto,
                            prdc_isituacion = Convert.ToBoolean(item.prdc_isituacion),
                            prdc_stock_minimo = item.prdc_stock_minimo,
                            prdc_bCombo = item.prdc_bCombo,

                            StrUnidadMedida = item.StrUnidadMedida,
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            dblStock = Convert.ToDecimal(item.dblStock),
                            strEstado = (Convert.ToBoolean(item.prdc_isituacion) == true) ? "ACTIVO" : "INACTIVO",
                            prdc_nMonto_stock = Convert.ToDecimal(item.prdc_nMonto_stock),

                            flag_select_mod = false
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

        public int getCorrelativoProducto(int intFamilia)
        {
            string intIcod = "0";
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGE_PRODUCTO_GET_CODIGO_CORRELATIVO(
                        intFamilia,
                        //intSubFamilia,
                        ref intIcod
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int insertarProducto(EProducto oBe)
        {
            int? intIcod = 0;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_PRODUCTO_INSERTAR(
                        ref intIcod,
                        oBe.prdc_vcode_producto,
                        oBe.prdc_vdescripcion_larga,
                        oBe.prdc_vdescripcion_corta,
                        oBe.famic_icod_familia,
                        oBe.famid_icod_familia,
                        oBe.unidc_icod_unidad_medida,
                        oBe.prdc_isituacion,
                        oBe.prdc_stock_minimo,
                        oBe.prdc_calibre,
                        oBe.tarec_icorrelativo_colorprod,
                        oBe.prdc_icod_proveedor,
                        oBe.prdc_precio_costo,
                        oBe.intUsuario,
                        oBe.prdc_flag_estado,
                        oBe.prdc_nMonto_stock,
                        oBe.prdc_vmarca,
                        oBe.prdc_vmodelo,
                        oBe.prdc_vparte,
                        oBe.prdc_vserie,
                        oBe.prdc_vcaracteristica,
                        oBe.prdc_vcodigo_fabricante,
                        oBe.ctacc_icod_cuenta_contable,
                        oBe.prdc_italla
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarProductoPrecioCosto(int prdc_icod_producto, decimal prdc_precio_costo)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_PRODUCTO_MODIFICAR_PRECIO_COSTO(
                        prdc_icod_producto,
                        prdc_precio_costo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarProducto(EProducto oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_PRODUCTO_MODIFICAR(
                        oBe.prdc_icod_producto,
                        oBe.prdc_vcode_producto,
                        oBe.prdc_vdescripcion_larga,
                        oBe.prdc_vdescripcion_corta,
                        oBe.famic_icod_familia,
                        oBe.famid_icod_familia,
                        oBe.unidc_icod_unidad_medida,
                        oBe.prdc_isituacion,
                        oBe.prdc_stock_minimo,
                        oBe.prdc_calibre,
                        oBe.tarec_icorrelativo_colorprod,
                        oBe.prdc_icod_proveedor,
                        oBe.prdc_precio_costo,
                        oBe.intUsuario,
                        oBe.prdc_nMonto_stock,
                        oBe.prdc_vmarca,
                        oBe.prdc_vmodelo,
                        oBe.prdc_vparte,
                        oBe.prdc_vserie,
                        oBe.prdc_vcaracteristica,
                        oBe.prdc_vcodigo_fabricante,
                        oBe.ctacc_icod_cuenta_contable,
                        oBe.prdc_italla

                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarProducto(EProducto oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_PRODUCTO_ELIMINAR(
                        oBe.prdc_icod_producto,
                        oBe.intUsuario
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EProducto> VerificarProducto(string Code)
        {
            List<EProducto> lista = new List<EProducto>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_VERIFICAR_PRODUCTO(Code);
                    foreach (var item in query)
                    {
                        lista.Add(new EProducto()
                        {
                            prdc_vcode_producto = item.prdc_vcode_producto,
                            prdc_icod_producto = item.prdc_icod_producto,
                            prdc_vdescripcion_larga = item.prdc_vdescripcion_larga
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
            //List<EProducto> lista = new List<EProducto>();
            //try
            //{
            //    using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
            //    {
            //        var query = dc.SGE_VERIFICAR_PRODUCTO(Code);
            //        foreach (var item in query)
            //        {
            //            lista.Add(new EProducto()
            //            {
            //                prdc_icod_producto = item.prdc_icod_producto,
            //                prdc_vdescripcion_larga = item.prdc_vdescripcion_larga,
            //                prdc_vcode_producto = item.prdc_vcode_producto
            //            });
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //return lista;
            //string descripcion_larga = null;
            //try
            //{
            //    using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
            //    {
            //        var query = dc.SGE_VERIFICAR_PRODUCTO(Code);
            //        foreach (var item in query)
            //        {
            //            descripcion_larga = string.Format((item.prdc_vdescripcion_larga));
            //            ;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //return descripcion_larga;
        }
        #endregion
        #region Nota de Ingreso - Cabecera
        public List<ENotaIngreso> listarNotaIngreso(int intEjercicio, int intAlmacen, DateTime? dtFechaDesde, DateTime? dtFechaHasta)
        {
            List<ENotaIngreso> lista = new List<ENotaIngreso>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_NOTA_INGRESO_LISTAR(intEjercicio, intAlmacen, dtFechaDesde, dtFechaHasta);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaIngreso()
                        {
                            ningc_icod_nota_ingreso = item.ningc_icod_nota_ingreso,
                            ningc_ianio = Convert.ToInt32(item.ningc_ianio),
                            ningc_numero_nota_ingreso = item.ningc_numero_nota_ingreso,
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            ningc_iid_motivo = Convert.ToInt32(item.ningc_iid_motivo),
                            ningc_fecha_nota_ingreso = Convert.ToDateTime(item.ningc_fecha_nota_ingreso),
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            ningc_numero_doc = item.ningc_numero_doc,
                            ningc_referencia = item.ningc_referencia,
                            ningc_observaciones = item.ningc_observaciones,
                            strAlmacen = item.strAlmacen,
                            strMotivo = item.strMotivo,
                            strTipoDoc = item.strTipoDoc,
                            strTipoNroDoc = String.Format("{0}-{1}", item.strTipoDoc, item.ningc_numero_doc),
                            fcoc_icod_doc = Convert.ToInt32(item.fcoc_icod_doc)
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
        public int insertarNotaIngreso(ENotaIngreso oBe)
        {
            int? intIcod = 0;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_NOTA_INGRESO_INSERTAR(
                        ref intIcod,
                        oBe.ningc_ianio,
                        oBe.ningc_numero_nota_ingreso,
                        oBe.almac_icod_almacen,
                        oBe.ningc_iid_motivo,
                        oBe.ningc_fecha_nota_ingreso,
                        oBe.tdocc_icod_tipo_doc,
                        oBe.ningc_numero_doc,
                        oBe.ningc_referencia,
                        oBe.ningc_observaciones,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.fcoc_icod_doc
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNotaIngreso(ENotaIngreso oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_NOTA_INGRESO_MODIFICAR(
                        oBe.ningc_icod_nota_ingreso,
                        oBe.ningc_ianio,
                        oBe.ningc_numero_nota_ingreso,
                        oBe.almac_icod_almacen,
                        oBe.ningc_iid_motivo,
                        oBe.ningc_fecha_nota_ingreso,
                        oBe.tdocc_icod_tipo_doc,
                        oBe.ningc_numero_doc,
                        oBe.ningc_referencia,
                        oBe.ningc_observaciones,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.fcoc_icod_doc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaIngreso(ENotaIngreso oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_NOTA_INGRESO_ELIMINAR(
                        oBe.ningc_icod_nota_ingreso,
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
        #region Nota de Ingreso - Detalle
        public List<ENotaIngresoDetalle> listarNotaIngresoDetalle(int intNotaIngreso)
        {
            List<ENotaIngresoDetalle> lista = new List<ENotaIngresoDetalle>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_NOTA_INGRESO_DETALLE_LISTAR(intNotaIngreso);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaIngresoDetalle()
                        {
                            dninc_icod_detalle_ingreso = item.dninc_icod_detalle_ingreso,
                            ningc_icod_nota_ingreso = item.ningc_icod_nota_ingreso,
                            dninc_nro_item = Convert.ToInt32(item.dninc_nro_item),
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            dninc_cantidad = Convert.ToDecimal(item.dninc_cantidad),
                            dninc_monto_unitario = Convert.ToDecimal(item.dninc_monto_unitario),
                            dninc_monto_total = Convert.ToDecimal(item.dninc_monto_total),
                            kardc_icod_correlativo = Convert.ToInt32(item.kardc_icod_correlativo),
                            strCodeProducto = item.strCodeProducto,
                            strProducto = item.strProducto,
                            strUnidadMedida = item.strUnidadMedida,
                            dnicc_ncantidad_recibida = Convert.ToDecimal(item.dnicc_ncantidad_recibida)
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
        public void insertarNotaIngresoDetalle(ENotaIngresoDetalle oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_NOTA_INGRESO_DETALLE_INSERTAR(
                        oBe.ningc_icod_nota_ingreso,
                        oBe.dninc_nro_item,
                        oBe.prdc_icod_producto,
                        oBe.dninc_cantidad,
                        oBe.dninc_monto_unitario,
                        oBe.dninc_monto_total,
                        oBe.kardc_icod_correlativo,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.dnicc_ncantidad_recibida
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNotaIngresoDetalle(ENotaIngresoDetalle oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_NOTA_INGRESO_DETALLE_MODIFICAR(
                        oBe.dninc_icod_detalle_ingreso,
                        oBe.dninc_nro_item,
                        oBe.prdc_icod_producto,
                        oBe.dninc_cantidad,
                        oBe.dninc_monto_unitario,
                        oBe.dninc_monto_total,
                        oBe.kardc_icod_correlativo,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.dnicc_ncantidad_recibida
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaIngresoDetalle(ENotaIngresoDetalle oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_NOTA_INGRESO_DETALLE_ELIMINAR(
                        oBe.dninc_icod_detalle_ingreso,
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
        #region Nota Salida - Cabecera
        public List<ENotaSalida> listarNotaSalida(int intEjercicio, int intAlmacen, DateTime? dtFechaDesde, DateTime? dtFechaHasta)
        {
            List<ENotaSalida> lista = new List<ENotaSalida>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_NOTA_SALIDA_LISTAR(intEjercicio, intAlmacen, dtFechaDesde, dtFechaHasta);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaSalida()
                        {
                            nsalc_icod_nota_salida = item.nsalc_icod_nota_salida,
                            nsalc_ianio = Convert.ToInt32(item.nsalc_ianio),
                            nsalc_numero_nota_salida = item.nsalc_numero_nota_salida,
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            nsalc_iid_motivo = Convert.ToInt32(item.nsalc_iid_motivo),
                            nsalc_fecha_nota_salida = Convert.ToDateTime(item.nsalc_fecha_nota_salida),
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            nsalc_numero_doc = item.nsalc_numero_doc,
                            nsalc_referencia = item.nsalc_referencia,
                            nsalc_observaciones = item.nsalc_observaciones,
                            strAlmacen = item.strAlmacen,
                            strMotivo = item.strMotivo,
                            strTipoDoc = item.strTipoDoc,
                            strTipoNroDoc = String.Format("{0}-{1}", item.strTipoDoc, item.nsalc_numero_doc)
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
        public int insertarNotaSalida(ENotaSalida oBe)
        {
            int? intIcod = 0;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_NOTA_SALIDA_INSERTAR(
                        ref intIcod,
                        oBe.nsalc_ianio,
                        oBe.nsalc_numero_nota_salida,
                        oBe.almac_icod_almacen,
                        oBe.nsalc_iid_motivo,
                        oBe.nsalc_fecha_nota_salida,
                        oBe.tdocc_icod_tipo_doc,
                        oBe.nsalc_numero_doc,
                        oBe.nsalc_referencia,
                        oBe.nsalc_observaciones,
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
        public void modificarNotaSalida(ENotaSalida oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_NOTA_SALIDA_MODIFICAR(
                        oBe.nsalc_icod_nota_salida,
                        oBe.nsalc_ianio,
                        oBe.nsalc_numero_nota_salida,
                        oBe.almac_icod_almacen,
                        oBe.nsalc_iid_motivo,
                        oBe.nsalc_fecha_nota_salida,
                        oBe.tdocc_icod_tipo_doc,
                        oBe.nsalc_numero_doc,
                        oBe.nsalc_referencia,
                        oBe.nsalc_observaciones,
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
        public void eliminarNotaSalida(ENotaSalida oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_NOTA_SALIDA_ELIMINAR(
                        oBe.nsalc_icod_nota_salida,
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
        #region Nota Salida - Detalle
        public List<ENotaSalidaDetalle> listarNotaSalidaDetalle(int intNotasalida)
        {
            List<ENotaSalidaDetalle> lista = new List<ENotaSalidaDetalle>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_NOTA_SALIDA_DETALLE_LISTAR(intNotasalida);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaSalidaDetalle()
                        {
                            dnsalc_icod_detalle_salida = item.dnsalc_icod_detalle_salida,
                            nsalc_icod_nota_salida = item.nsalc_icod_nota_salida,
                            dnsalc_nro_item = Convert.ToInt32(item.dnsalc_nro_item),
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            dnsalc_cantidad = Convert.ToDecimal(item.dnsalc_cantidad),
                            dnsalc_monto_unitario = Convert.ToDecimal(item.dnsalc_monto_unitario),
                            dnsalc_monto_total = Convert.ToDecimal(item.dnsalc_monto_total),
                            kardc_icod_correlativo = Convert.ToInt32(item.kardc_icod_correlativo),
                            strCodeProducto = item.strCodeProducto,
                            strProducto = item.strProducto,
                            strUnidadMedida = item.strUnidadMedida,
                            dblStockDisponible = item.dblStockDisponible,
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
        public void insertarNotaSalidaDetalle(ENotaSalidaDetalle oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_NOTA_SALIDA_DETALLE_INSERTAR(
                        oBe.nsalc_icod_nota_salida,
                        oBe.dnsalc_nro_item,
                        oBe.prdc_icod_producto,
                        oBe.dnsalc_cantidad,
                        oBe.dnsalc_monto_unitario,
                        oBe.dnsalc_monto_total,
                        oBe.kardc_icod_correlativo,
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
        public void modificarNotaSalidaDetalle(ENotaSalidaDetalle oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_NOTA_SALIDA_DETALLE_MODIFICAR(
                        oBe.dnsalc_icod_detalle_salida,
                        oBe.dnsalc_nro_item,
                        oBe.prdc_icod_producto,
                        oBe.dnsalc_cantidad,
                        oBe.dnsalc_monto_unitario,
                        oBe.dnsalc_monto_total,
                        oBe.kardc_icod_correlativo,
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
        public void eliminarNotaSalidaDetalle(ENotaSalidaDetalle oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_NOTA_SALIDA_DETALLE_ELIMINAR(
                        oBe.dnsalc_icod_detalle_salida,
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
        #region Kardex Valorizado
        public int Obtener_Kardex_Max_Correlativo()
        {
            int? intIcod = 0;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_KARDEX_MAX_CORRELATIVO(
                        ref intIcod
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DeleteCierreAnualAlmacenes(int intEjercicio)
        {
            int? intIcod = 0;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGE_SALDO_INICIAL_POR_CIERRE_DELETE(
                        intEjercicio
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsetarMasivoKardexb(DataTable tbl)
        {
            try
            {
                using (var conn = new SqlConnection(Helper.conexion()))
                {
                    using (var cmd = new SqlCommand("INSERCION_MASIVA_KARDEX", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@VCC", tbl);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EKardexValorizado> listarKardexValorizadoPorMotivoFecha(int intEjercicio, DateTime dtInicio, DateTime dtFin, int intTipoMov, int intMotivoMov)
        {
            List<EKardexValorizado> lista = new List<EKardexValorizado>(); ;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_KARDEX_VALORIZADO_POR_TIPO_MOTIVO_FECHA(intEjercicio, dtInicio, dtFin, intTipoMov, intMotivoMov);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardexValorizado()
                        {
                            almcc_icod_almacen = Convert.ToInt32(item.almcc_icod_almacen),
                            //alco_iid_almacen_contable = item.alco_iid_almacen_contable,
                            strDesAlmacenCtbl = item.almcc_vdescripcion,
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            prdc_vcode_producto = item.prdc_vcode_producto,
                            strDesProducto = item.prdc_vdescripcion_larga,
                            //estac_vdescripcion = item.estac_vdescripcion,
                            //situc_vdescripcion = item.situc_vdescripcion,
                            unidc_vabreviatura_unidad_medida = item.unidc_vabreviatura_unidad_medida,
                            Documento = item.documento,
                            kardv_sfecha_movimiento = Convert.ToDateTime(item.kardv_sfecha_movimiento),
                            kardv_icantidad_prod = Math.Round(Convert.ToDecimal(item.kardv_icantidad_prod), 2),
                            kardv_precio_costo_promedio = Math.Round(Convert.ToDecimal(item.kardv_precio_costo_promedio), 4),
                            kardv_monto_unitario_compra = Math.Round(Convert.ToDecimal(item.intPrecionUniCompra), 4),
                            kardv_monto_total_costo = Math.Round(Convert.ToDecimal(item.dblMontoMovimiento), 4),
                            kardv_monto_total_compra = Math.Round(Convert.ToDecimal(item.dblMontoCompra), 4),
                            kardv_vbeneficiario = item.kardv_vbeneficiario
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

        public List<EKardexValorizado> listarResumenValorizadoProductos(int intEjercicio, DateTime dtInicio, DateTime dtFin)
        {
            List<EKardexValorizado> lista = new List<EKardexValorizado>(); ;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_RESUMEN_VALORIZADO_LISTAR(intEjercicio, dtInicio, dtFin);
                    foreach (var item in query)
                    {
                        if (item.dblStockAnterior == 0 && item.dblEntradas == 0 && item.dblSalidas == 0)
                        {
                        }
                        else
                        {
                            lista.Add(new EKardexValorizado()
                            {
                                almcc_icod_almacen = Convert.ToInt32(item.intAlmaCont),
                                strDesAlmacenCtbl = item.strAlmaCont,
                                prdc_icod_producto = Convert.ToInt32(item.intProdEsp),
                                prdc_vcode_producto = item.strCodeProd.ToString(),
                                strDesProducto = item.strProdEsp,
                                unidc_vabreviatura_unidad_medida = item.strUnidadMed,
                                StockAnterior = Convert.ToDecimal(item.dblStockAnterior),
                                dcmlIngreso = item.dblEntradas,
                                dcmlSalida = item.dblSalidas,
                                Stock = Convert.ToDecimal(item.dblStockAnterior) + (Convert.ToDecimal(item.dblEntradas) - Convert.ToDecimal(item.dblSalidas)),
                                StockAnteriorCosto = item.dblAnteriorCosto,
                                IngresoCosto = item.dblEntradasCosto,
                                SalidaCosto = item.dblSalidasCosto,
                                kardv_monto_saldo_valorizado = item.dblAnteriorCosto + item.dblEntradasCosto - item.dblSalidasCosto,
                                intIidAlmaCont = Convert.ToInt32(item.intIidAlmaCont)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public void ActualizarKardexValorizadoMontoManual(int IdKardex, decimal MontoManual)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGE_KARDEX_VALORIZADO_ACTUALIZAR_MONTO_MANUAL(
                       IdKardex,
                       MontoManual
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarFechaKardexValorizado(EKardexValorizado obj)
        {

            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGE_KARDEX_VALORIZADO_ACTUALIZAR_FECHA(
                        obj.kardv_icod_correlativo,
                        obj.kardv_sfecha_movimiento
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EKardexValorizado> ListarKardexValorizadoInventarioResumen(DateTime FechaDesde, DateTime FechaHasta)
        {
            List<EKardexValorizado> lista = new List<EKardexValorizado>(); ;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_KARDEX_VALORIZADO_INVENTARIO_RESUMEN(FechaDesde, FechaHasta);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardexValorizado()
                        {
                            kardv_ianio = Convert.ToInt32(item.kardv_ianio),
                            almcc_icod_almacen = Convert.ToInt32(item.almcc_icod_almacen),
                            //alco_iid_almacen_contable = item.alco_iid_almacen_contable,
                            strDesAlmacenCtbl = item.almcc_vdescripcion,
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            prdc_vcode_producto = item.prdc_vcode_producto,
                            strDesProducto = item.prdc_vdescripcion_larga,
                            //prodc_iid_producto_generico_completo = item.pespc_iid_producto_generico.ToString() + "-" + item.estac_iid_estado_prod.ToString() + "-" + item.situc_iid_situacion_prod.ToString(),
                            //situc_iid_situacion_prod = Convert.ToInt32(item.situc_iid_situacion_prod),
                            //situc_vdescripcion = item.situc_vdescripcion,
                            unidc_vabreviatura_unidad_medida = item.unidc_vabreviatura_unidad_medida.ToString(),
                            //estac_iid_estado_prod = Convert.ToInt32(item.estac_iid_estado_prod),
                            //estac_vdescripcion = item.estac_vdescripcion,
                            Stock = Convert.ToDecimal(item.Stock),
                            kardv_monto_total_compra = Math.Round(Convert.ToDecimal(item.stocv_ntotal_costo_prod), 2),
                            kardv_precio_costo_promedio = Convert.ToDecimal(item.stocv_nprecio_costo_prom_prod),
                            PrimerCosto = Convert.ToDecimal(item.PrimerCosto),
                            CostoAlto = Convert.ToDecimal(item.CostoAlto)
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
        public List<EKardexValorizado> ListarStockValorizadoAunaFecha(DateTime FechaDesde, DateTime FechaHasta)
        {
            List<EKardexValorizado> lista = new List<EKardexValorizado>(); ;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_STOCK_VALORIZADO_A_UNA_FECHA(FechaDesde, FechaHasta);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardexValorizado()
                        {
                            kardv_ianio = Convert.ToInt32(item.kardv_ianio),
                            almcc_icod_almacen = Convert.ToInt32(item.almcc_icod_almacen),
                            strDesAlmacenCtbl = item.almcc_vdescripcion,
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            prdc_vcode_producto = item.prdc_vcode_producto,
                            strDesProducto = item.prdc_vdescripcion_larga,
                            unidc_vabreviatura_unidad_medida = item.unidc_vabreviatura_unidad_medida.ToString(),
                            Stock = Convert.ToDecimal(item.Stock),
                            kardv_monto_total_compra = Math.Round(Convert.ToDecimal(item.stocv_ntotal_costo_prod), 2),
                            kardv_precio_costo_promedio = Convert.ToDecimal(item.stocv_nprecio_costo_prom_prod),
                            //PrimerCosto = Convert.ToDecimal(item.PrimerCosto),
                            //CostoAlto = Convert.ToDecimal(item.CostoAlto)
                            prdc_precio_costo = Convert.ToDecimal(item.prdc_precio_costo),
                            precio_compra_total = Convert.ToDecimal(item.precio_compra_total)
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
        public List<EKardexValorizado> ListarKardexValorizadoInventario(int IdAlmacen, int IdProducto, DateTime FechaDesde, DateTime FechaHasta)
        {
            List<EKardexValorizado> lista = new List<EKardexValorizado>(); ;
            try
            {
                int cont = 0;
                decimal? NullVal;
                NullVal = null;
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_KARDEX_VALORIZADO_INVENTARIO(IdAlmacen, IdProducto, FechaDesde, FechaHasta);
                    foreach (var item in query)
                    {
                        cont++;
                        EKardexValorizado _be = new EKardexValorizado();
                        _be.kardv_icod_correlativo = Convert.ToInt32(item.kardv_iid_correlativo);
                        _be.kardv_ianio = Convert.ToInt32(item.kardv_ianio);
                        _be.kardv_sfecha_movimiento = Convert.ToDateTime(item.kardv_sfecha_movimiento);
                        _be.almcc_icod_almacen = Convert.ToInt32(item.kardv_icod_almacen_contable);
                        _be.strDesAlmacenCtbl = item.alco_vdescripcion;
                        _be.prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto);
                        _be.strDesProducto = item.prdc_vdescripcion_larga;
                        _be.tdocc_icod_tipo_doc = Convert.ToInt32(item.kardv_iid_tipo_doc);
                        _be.strTipoDoc = item.tdocc_vabreviatura_tipo_doc;
                        _be.kardv_inumero_doc = item.kardv_inumero_doc;
                        _be.strTipoNroDoc = item.tdocc_vabreviatura_tipo_doc + "-" + item.kardv_inumero_doc;

                        _be.unidc_vabreviatura_unidad_medida = item.unidc_vabreviatura_unidad_medida;
                        _be.Motivo = item.Motivo;
                        _be.kardv_vbeneficiario = item.kardv_vbeneficiario;
                        _be.kardv_vobservaciones = item.kardv_vobservaciones;
                        _be.dcmlIngreso = (item.Ingresos == 0) ? NullVal : item.Ingresos;
                        _be.dcmlSalida = (item.Salidas == 0) ? NullVal : item.Salidas;
                        _be.Stock = item.Stock;

                        //_be.kardv_monto_total_costo = Convert.ToDecimal(item.kardv_precio_costo_promedio) * Convert.ToDecimal(item.Ingresos);
                        _be.kardv_precio_costo_promedio = Convert.ToDecimal(item.kardv_precio_costo_promedio);
                        _be.kardv_monto_total_compra = Convert.ToDecimal(item.kardv_monto_total_compra);
                        _be.kardv_itipo_movimiento = Convert.ToInt32(item.kardv_itipo_movimiento);
                        _be.CostoPresupuesto = Convert.ToDecimal(item.CostoPresupuesto);
                        _be.ValorPresupuesto = Convert.ToDecimal(item.ValorPresupuesto);
                        //_be.kardv_nmonto_ingreso_manual = item.kardv_nmonto_ingreso_manual;
                        //***//
                        //_be.IngresoCosto = Math.Round(Convert.ToDecimal(item.Ingresos) * Convert.ToDecimal(item.kardv_precio_costo_promedio), 2);
                        //_be.SalidaCosto = (Math.Round(Convert.ToDecimal(item.Salidas) * Convert.ToDecimal(item.kardv_precio_costo_promedio), 2) == 0) ? NullVal :
                        //Math.Round(Convert.ToDecimal(item.Salidas) * Convert.ToDecimal(item.kardv_precio_costo_promedio), 2);
                        //_be.StockCosto = Math.Round(Convert.ToDecimal(item.Stock) * Convert.ToDecimal(item.kardv_precio_costo_promedio), 2);
                        _be.Cont_registro_valorizado = cont;
                        lista.Add(_be);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EKardexValorizadoCompras> ListarKardexValorizadoCompras(int intEjercicio, DateTime dtFechaInicio, DateTime dtFechaFin)
        {
            List<EKardexValorizadoCompras> lista = new List<EKardexValorizadoCompras>(); ;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_KARDEX_VALORIZADO_ACTUALIZAR_COMPRAS(dtFechaInicio, dtFechaFin, intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardexValorizadoCompras()
                        {
                            anio = Convert.ToInt32(item.anio),
                            icod_almacen_contable = Convert.ToInt32(item.alco_icod_almacen_contable),
                            icod_producto_especifico = Convert.ToInt32(item.icod_producto_especifico),
                            icod_tipo_doc = Convert.ToInt32(item.icod_tipo_doc),
                            doc_numero = item.doc_numero,
                            tip_movimiento = item.tip_movimiento,
                            icod_motivo = item.icod_motivo,
                            flag_estado = item.flag_estado,
                            costo_unitario = Convert.ToDecimal(item.costo_unitario),
                            alco_vdescripcion = item.alco_vdescripcion,
                            prodc_vdescripcion = item.prodc_vdescripcion,
                            estac_vdescripcion = item.estac_vdescripcion,
                            situc_vdescripcion = item.situc_vdescripcion,
                            documento = item.documento,
                            motivo = item.motivo,
                            fecha = Convert.ToDateTime(item.fecha),
                            cant_ingreso = Convert.ToDecimal(item.cant_ingreso),
                            unidad_medida = item.unidad_medida,
                            monto_total = Convert.ToDecimal(item.monto_total),
                            referencia = item.referencia,
                            prodc_iid_producto_generico = item.prodc_iid_producto_generico,
                            flag_compras_import = true,
                            fcoc_nmonto_tipo_cambio = Convert.ToDecimal(item.fcoc_nmonto_tipo_cambio),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda)
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
        public List<EKardexValorizadoCompras> ListarKardexValorizadoComprasImportacion(int intEjercicio, DateTime dtFechaInicio, DateTime dtFechaFin)
        {
            List<EKardexValorizadoCompras> lista = new List<EKardexValorizadoCompras>(); ;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_KARDEX_VALORIZADO_ACTUALIZAR_IMPORTACION(dtFechaInicio, dtFechaFin, intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardexValorizadoCompras()
                        {
                            anio = Convert.ToInt32(item.anio),
                            icod_almacen_contable = Convert.ToInt32(item.alco_icod_almacen_contable),
                            icod_producto_especifico = Convert.ToInt32(item.icod_producto_especifico),
                            icod_tipo_doc = Convert.ToInt32(item.icod_tipo_doc),
                            doc_numero = item.doc_numero,
                            tip_movimiento = item.tip_movimiento,
                            icod_motivo = item.icod_motivo,
                            flag_estado = item.flag_estado,
                            costo_unitario = Convert.ToDecimal(item.costo_unitario),
                            alco_vdescripcion = item.alco_vdescripcion,
                            prodc_vdescripcion = item.prodc_vdescripcion,
                            estac_vdescripcion = item.estac_vdescripcion,
                            situc_vdescripcion = item.situc_vdescripcion,
                            documento = item.documento,
                            motivo = item.motivo,
                            fecha = Convert.ToDateTime(item.fecha),
                            cant_ingreso = Convert.ToDecimal(item.cant_ingreso),
                            unidad_medida = item.unidad_medida,
                            monto_total = Convert.ToDecimal(item.monto_total),
                            referencia = item.referencia,
                            prodc_iid_producto_generico = item.prodc_iid_producto_generico,
                            flag_compras_import = true,
                            fcoc_nmonto_tipo_cambio = Convert.ToDecimal(item.fcoc_nmonto_tipo_cambio),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda)
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
        public List<EKardexValorizadoCompras> ListarKardexValorizadoVentas(int Periodo, DateTime FechaInicio, DateTime FechaFin)
        {
            List<EKardexValorizadoCompras> lista = new List<EKardexValorizadoCompras>(); ;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_KARDEX_VALORIZADO_ACTUALIZAR_VENTAS(FechaInicio, FechaFin, Periodo);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardexValorizadoCompras()
                        {
                            anio = Convert.ToInt32(item.anio),
                            icod_almacen_contable = Convert.ToInt32(item.alco_icod_almacen_contable),
                            icod_producto_especifico = Convert.ToInt32(item.icod_producto_especifico),
                            icod_tipo_doc = Convert.ToInt32(item.icod_tipo_doc),
                            doc_numero = item.doc_numero,
                            tip_movimiento = Convert.ToInt32(item.tip_movimiento),
                            icod_motivo = Convert.ToInt32(item.icod_motivo),
                            flag_estado = item.flag_estado,

                            alco_vdescripcion = item.alco_vdescripcion,
                            prodc_vdescripcion = item.prodc_vdescripcion,
                            estac_vdescripcion = item.estac_vdescripcion,
                            documento = item.documento,
                            motivo = item.motivo,
                            fecha = Convert.ToDateTime(item.fecha),
                            cant_salida = Convert.ToDecimal(item.cant_salida),
                            unidad_medida = item.unidad_medida,
                            referencia = item.referencia,
                            observacion = item.observacion,
                            prodc_iid_producto_generico = item.prodc_iid_producto_generico,
                            flag_compras_import = true

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
        public List<EKardexValorizadoCompras> ListarKardexValorizadoDevoluciones(int Periodo, DateTime FechaInicio, DateTime FechaFin)
        {
            List<EKardexValorizadoCompras> lista = new List<EKardexValorizadoCompras>(); ;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_KARDEX_VALORIZADO_ACTUALIZAR_NOTAS_DE_CREDITO(FechaInicio, FechaFin, Periodo);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardexValorizadoCompras()
                        {
                            anio = Convert.ToInt32(item.anio),
                            icod_almacen_contable = item.alco_icod_almacen_contable,
                            icod_producto_especifico = Convert.ToInt32(item.icod_producto_especifico),
                            icod_tipo_doc = Convert.ToInt32(item.icod_tipo_doc),
                            doc_numero = item.doc_numero,
                            tip_movimiento = item.tip_movimiento,
                            icod_motivo = item.icod_motivo,
                            flag_estado = item.flag_estado,
                            costo_unitario = Convert.ToDecimal(item.costo_unitario),

                            alco_vdescripcion = item.alco_vdescripcion,
                            prodc_vdescripcion = item.prodc_vdescripcion,
                            estac_vdescripcion = item.estac_vdescripcion,
                            situc_vdescripcion = item.situc_vdescripcion,
                            documento = item.documento,
                            motivo = item.motivo,
                            fecha = Convert.ToDateTime(item.fecha),
                            cant_salida = Convert.ToDecimal(item.cant_salida),
                            unidad_medida = item.unidad_medida,
                            monto_total = Convert.ToDecimal(item.monto_total),
                            referencia = item.referencia,
                            observacion = item.observacion,
                            prodc_iid_producto_generico = item.prodc_iid_producto_generico,
                            flag_compras_import = true
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
        public void EliminarKardexValorizadoActualizacion(int Periodo, DateTime FechaInicio, DateTime FechaFin, int TipoActualizacion)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGE_KARDEX_VALORIZADO_ELIMINAR_ACTUALIZACION(
                        FechaInicio,
                        FechaFin,
                        Periodo,
                        TipoActualizacion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizacionVentas(int Periodo, DateTime FechaInicio, DateTime FechaFin, int TipoActualizacion)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGE_ACTUALIZAR_VENTAS(
                        FechaInicio,
                        FechaFin,
                        Periodo,
                        TipoActualizacion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EKardexValorizado> listarKardexValorizadoSaldoInicial(int intEjercicio)
        {
            List<EKardexValorizado> lista = new List<EKardexValorizado>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_KARDEX_VALORIZADO_SALDO_INICIAL_LISTAR(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardexValorizado()
                        {
                            kardv_icod_correlativo = item.kardv_icod_correlativo,
                            kardv_ianio = item.kardv_ianio,
                            kardv_sfecha_movimiento = item.kardv_sfecha_movimiento,
                            almcc_icod_almacen = item.almcc_icod_almacen,
                            prdc_icod_producto = item.prdc_icod_producto,
                            kardv_icantidad_prod = item.kardv_icantidad_prod,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            kardv_inumero_doc = item.kardv_inumero_doc,
                            kardv_itipo_movimiento = item.kardv_itipo_movimiento,
                            kardv_iid_motivo = item.kardv_iid_motivo,
                            kardv_vbeneficiario = item.kardv_vbeneficiario,
                            kardv_vobservaciones = item.kardv_vobservaciones,
                            kardv_monto_total_compra = item.kardv_monto_total_compra,
                            kardv_precio_costo_promedio = item.kardv_precio_costo_promedio,
                            kardv_monto_saldo_valorizado = item.kardv_monto_saldo_valorizado,
                            kardv_monto_unitario_compra = item.kardv_monto_unitario_compra,
                            kardv_nmonto_ingreso_manual = item.kardv_nmonto_ingreso_manual,
                            kardv_itipo_actualizacion = item.kardv_itipo_actualizacion,
                            strDesAlmacenCtbl = item.strDesAlmacenCtbl,
                            strCodProducto = item.strCodProducto,
                            strDesProducto = item.strDesProducto,
                            strTipoDoc = item.strTipoDoc,
                            strTipoNroDoc = String.Format("{0}{1}", item.strTipoDoc, item.kardv_inumero_doc),
                            dcmlIngreso = (item.kardv_itipo_movimiento == 1) ? item.kardv_icantidad_prod : 0,
                            dcmlSalida = (item.kardv_itipo_movimiento == 0) ? item.kardv_icantidad_prod : 0
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
        public Int64 insertarKardexValorizado(EKardexValorizado oBe)
        {
            Int64? intIcod = 0;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGE_KARDEX_VALORIZADO_INSERTAR(
                        ref intIcod,
                        oBe.kardv_ianio,
                        oBe.kardv_sfecha_movimiento,
                        oBe.almcc_icod_almacen,
                        oBe.prdc_icod_producto,
                        oBe.kardv_icantidad_prod,
                        oBe.tdocc_icod_tipo_doc,
                        oBe.kardv_inumero_doc,
                        oBe.kardv_itipo_movimiento,
                        oBe.kardv_iid_motivo,
                        oBe.kardv_vbeneficiario,
                        oBe.kardv_vobservaciones,
                        oBe.kardv_monto_total_compra,
                        oBe.kardv_precio_costo_promedio,
                        oBe.kardv_monto_saldo_valorizado,
                        oBe.kardv_monto_unitario_compra,
                        oBe.kardv_nmonto_ingreso_manual,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.kardv_itipo_actualizacion
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarKardexValorizado(EKardexValorizado oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGE_KARDEX_VALORIZADO_MODIFICAR(
                        oBe.kardv_icod_correlativo,
                        oBe.kardv_ianio,
                        oBe.kardv_sfecha_movimiento,
                        oBe.almcc_icod_almacen,
                        oBe.prdc_icod_producto,
                        oBe.kardv_icantidad_prod,
                        oBe.tdocc_icod_tipo_doc,
                        oBe.kardv_inumero_doc,
                        oBe.kardv_itipo_movimiento,
                        oBe.kardv_iid_motivo,
                        oBe.kardv_vbeneficiario,
                        oBe.kardv_vobservaciones,
                        oBe.kardv_monto_total_compra,
                        oBe.kardv_precio_costo_promedio,
                        oBe.kardv_monto_saldo_valorizado,
                        oBe.kardv_monto_unitario_compra,
                        oBe.kardv_nmonto_ingreso_manual,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.kardv_itipo_actualizacion
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarKardexValorizado(EKardexValorizado oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGE_KARDEX_VALORIZADO_ELIMINAR(
                        oBe.kardv_icod_correlativo,
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
        public void ActualizarPrecioCostoPromedioKardexValorizado(int intEjercicio)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGE_ACTUALIZAR_PCP_KARDEX_VALORIZADO(intEjercicio);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarPrecioCostoPromedioKardexValorizadoXproAlma(int prdc_icod_producto, int almcc_icod_almacen, int intEjercicio)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGE_ACTUALIZAR_PCP_KARDEX_VALORIZADO_X_Al_pro(prdc_icod_producto, almcc_icod_almacen, intEjercicio);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Stock Valorizado

        public List<EStockValorizado> listarStockValorizadoProducto(int intEjercicio, int intAlmacenCtbl, int intProducto)
        {
            List<EStockValorizado> lista = new List<EStockValorizado>(); ;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_STOCK_VALORIZADO_PRODUCTO_LISTAR(intEjercicio, intAlmacenCtbl, intProducto);
                    foreach (var item in query)
                    {
                        lista.Add(new EStockValorizado()
                        {
                            stocv_icod_stock = item.stocv_icod_stock,
                            stocv_ianio = item.stocv_ianio,
                            almcc_icod_almacen = item.almcc_icod_almacen,
                            strDesAlmacenCtbl = item.almcc_vdescripcion,
                            prdc_icod_producto = item.prdc_icod_producto,
                            strCodProducto = item.strCodProducto,
                            strDesProducto = item.strDesProducto,
                            strCodUnidadMedida = item.unidc_vabreviatura_unidad_medida,
                            stocv_nstock_prod = Convert.ToDecimal(item.stocv_nstock_prod),
                            stocv_nprecio_costo_prom_prod = Convert.ToDecimal(item.stocv_nprecio_costo_prom_prod),

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
        public int insertarStockValorizado(EStockValorizado objEStockValorizado)
        {
            int? intIcod = 0;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGE_STOCK_VALORIZADO_INSERTAR(
                        ref intIcod,
                        objEStockValorizado.stocv_ianio,
                        objEStockValorizado.almcc_icod_almacen,
                        objEStockValorizado.prdc_icod_producto,
                        objEStockValorizado.stocv_nstock_prod,
                        objEStockValorizado.stocv_nprecio_costo_prom_prod,
                        objEStockValorizado.stocv_ntotal_costo_prod
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void actualizarStockValorizadoCostos(int intEjercicio, int intAlmacenCtbl, int intProducto, decimal dcmlPCP, decimal dcmlCostoTotal)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGE_STOCK_VALORIZADO_ACTUALIZAR_COSTOS(
                        intEjercicio,
                        intAlmacenCtbl,
                        intProducto,
                        dcmlPCP,
                        dcmlCostoTotal
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
        #region Kardex

        public List<EKardex> listarStockConsolidado(int intEjercicio)
        {
            List<EKardex> lista = new List<EKardex>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_STOCK_CONSOLIDADO(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardex()
                        {
                            prdc_icod_producto = item.prdc_icod_producto,
                            strCodProducto = item.strCodProducto,
                            strProducto = item.strProducto,
                            strUnidadMedida = item.strUnidadMedida,
                            dblSaldo = Convert.ToDecimal(item.dblStock)
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
        public List<EKardex> listarStockConsolidadoxAlmacen(int intEjercicio)
        {
            List<EKardex> lista = new List<EKardex>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_STOCK_CONSOLIDADO_X_ALMACEN(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardex()
                        {
                            prdc_icod_producto = item.prdc_icod_producto,
                            strCodProducto = item.strCodProducto,
                            strProducto = item.strProducto,
                            strUnidadMedida = item.strUnidadMedida,
                            dblSaldo = Convert.ToDecimal(item.dblStock),
                            almac_icod_almacen = item.almac_icod_almacen,
                            strAlmacen = item.almac_vdescripcion
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
        public List<EKardex> listarStockConsolidadoxAlmacen2(int intEjercicio)
        {
            List<EKardex> lista = new List<EKardex>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_STOCK_CONSOLIDADO_X_ALMACEN(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardex()
                        {
                            prdc_icod_producto = item.prdc_icod_producto,
                            strCodProducto = item.strCodProducto,
                            strProducto = item.strProducto,
                            strUnidadMedida = item.strUnidadMedida,
                            dblSaldo = Convert.ToDecimal(item.dblStock),
                            almac_icod_almacen = item.almac_icod_almacen,
                            strAlmacen = item.almac_vdescripcion
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
        public List<EKardex> listarCierreAnualAlmacenes(int intEjercicio)
        {
            List<EKardex> lista = new List<EKardex>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_CIERRE_ANUAL(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardex()
                        {
                            almac_icod_almacen = item.almac_icod_almacen,
                            prdc_icod_producto = item.prdc_icod_producto,
                            kardc_icantidad_prod = Convert.ToDecimal(item.dblStock)
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

        public List<EKardex> listarKardexPorFechaAlmacenProducto(DateTime dtFechaDesde, DateTime dtFechaHasta, int? intAlmacen, int? intProducto)
        {
            List<EKardex> lista = new List<EKardex>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_KARDEX_LISTAR_X_FECHA_ALMACEN_PRODUCTO(dtFechaDesde, dtFechaHasta, intAlmacen, intProducto);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardex()
                        {
                            kardc_fecha_movimiento = item.kardc_fecha_movimiento,
                            almac_icod_almacen = item.almac_icod_almacen,
                            prdc_icod_producto = item.prdc_icod_producto,
                            kardc_icantidad_prod = Convert.ToDecimal(item.kardc_icantidad_prod),
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            kardc_numero_doc = item.kardc_numero_doc,
                            kardc_tipo_movimiento = item.kardc_tipo_movimiento,
                            kardc_iid_motivo = item.kardc_iid_motivo,
                            kardc_beneficiario = item.kardc_beneficiario,
                            kardc_observaciones = item.kardc_observaciones,
                            dblIngreso = Convert.ToDecimal(item.dblIngreso),
                            dblSalida = Convert.ToDecimal(item.dblSalida),
                            dblSaldo = Convert.ToDecimal(item.dblSaldo),
                            strDocumento = item.strDocumento,
                            strMotivo = item.strMotivo.ToUpper(),
                            strAlmacen = item.strAlmacen,
                            strCodProducto = item.strCodProducto,
                            strProducto = item.strProducto,
                            kardc_stock = Convert.ToDecimal(item.kardc_stock),
                            kardc_unidMed = item.kardc_unidMed,
                            unidc_vdescripcion = item.unidc_vdescripcion,
                            tdocc_coa = item.tdocc_coa,
                            tarec_vtipo_operacion = item.tarec_vtipo_operacion,
                            kardc_codUnidMEd = item.kardc_codUnidMEd,
                            clasc_vdescripcion = item.clasc_vdescripcion,
                            famic_vtipo_existencia = item.famic_vtipo_existencia

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

        public List<EKardex> listarKardexAlmacenProductoVerificar(int? intAlmacen, int? intProducto)
        {
            List<EKardex> lista = new List<EKardex>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_KARDEX_LISTAR_ALMACEN_PRODUCTO_VERIFICAR(intAlmacen, intProducto);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardex()
                        {
                            kardc_fecha_movimiento = item.kardc_fecha_movimiento,
                            almac_icod_almacen = item.almac_icod_almacen,
                            prdc_icod_producto = item.prdc_icod_producto,
                            kardc_icantidad_prod = Convert.ToDecimal(item.kardc_icantidad_prod),
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            kardc_numero_doc = item.kardc_numero_doc,
                            kardc_tipo_movimiento = item.kardc_tipo_movimiento,
                            kardc_iid_motivo = item.kardc_iid_motivo,
                            kardc_beneficiario = item.kardc_beneficiario,
                            kardc_observaciones = item.kardc_observaciones,
                            dblIngreso = Convert.ToDecimal(item.dblIngreso),
                            dblSalida = Convert.ToDecimal(item.dblSalida),
                            dblSaldo = Convert.ToDecimal(item.dblSaldo),
                            strDocumento = item.strDocumento,
                            strMotivo = item.strMotivo.ToUpper(),
                            strAlmacen = item.strAlmacen,
                            strCodProducto = item.strCodProducto,
                            strProducto = item.strProducto
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

        public List<EKardex> listarKardexPorMotivoAlmacenProducto(DateTime dtFechaDesde, DateTime dtFechaHasta, int? intTipoMov, int? intMotivo, int? intProducto)
        {
            List<EKardex> lista = new List<EKardex>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_KARDEX_LISTAR_X_MOTIVO_PRODUCTO(dtFechaDesde, dtFechaHasta, intTipoMov, intMotivo, intProducto);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardex()
                        {
                            kardc_fecha_movimiento = item.kardc_fecha_movimiento,
                            almac_icod_almacen = item.almac_icod_almacen,
                            prdc_icod_producto = item.prdc_icod_producto,
                            kardc_icantidad_prod = Convert.ToDecimal(item.kardc_icantidad_prod),
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            kardc_numero_doc = item.kardc_numero_doc,
                            kardc_tipo_movimiento = item.kardc_tipo_movimiento,
                            kardc_iid_motivo = item.kardc_iid_motivo,
                            kardc_beneficiario = item.kardc_beneficiario,
                            kardc_observaciones = item.kardc_observaciones,
                            dblIngreso = Convert.ToDecimal(item.dblIngreso),
                            dblSalida = Convert.ToDecimal(item.dblSalida),
                            dblSaldo = Convert.ToDecimal(item.dblSaldo),
                            strDocumento = item.strDocumento,
                            strMotivo = item.strMotivo.ToUpper(),
                            strAlmacen = item.strAlmacen

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
        public List<EKardex> listarResumenPorAlmacen(DateTime dtFechaDesde, DateTime dtFechaHasta, int? intAlmacen)
        {
            List<EKardex> lista = new List<EKardex>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_RESUMEN_MOV_PRODUCTOS(dtFechaDesde, dtFechaHasta, intAlmacen);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardex()
                        {
                            almac_icod_almacen = item.almac_icod_almacen,
                            prdc_icod_producto = item.prdc_icod_producto,
                            dblSaldoAnterior = Convert.ToDecimal(item.dblSaldoAnterior),
                            dblIngreso = Convert.ToDecimal(item.dblIngresos),
                            dblSalida = Convert.ToDecimal(item.dblSalidas),
                            dblSaldo = Convert.ToDecimal(item.dblSaldoActual),
                            strAlmacen = item.strAlmacen,
                            strCodProducto = item.strCodProducto,
                            strProducto = item.strProducto,
                            strUnidadMedida = item.strUnidadMedida
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
        public List<EKardex> listarKardexStockPorFechaAlmacenProducto(DateTime dtFechaDesde, DateTime dtFechaHasta, int? intAlmacen, int? intProducto)
        {
            List<EKardex> lista = new List<EKardex>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_KARDEX_STOCK_LISTAR_X_FECHA_ALMACEN_PRODUCTO(dtFechaDesde, dtFechaHasta, intAlmacen, intProducto);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardex()
                        {
                            almac_icod_almacen = item.almac_icod_almacen,
                            prdc_icod_producto = item.prdc_icod_producto,
                            strAlmacen = item.strAlmacen,
                            strCodProducto = item.strCodProducto,
                            strProducto = item.strProducto,
                            strUnidadMedida = item.strUnidadMedida,
                            dblSaldo = Convert.ToDecimal(item.stock)
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

        public List<EKardex> listarInventarioPermanenteUnidades(DateTime dtFechaDesde, DateTime dtFechaHasta, int? intAlmacen)
        {
            List<EKardex> lista = new List<EKardex>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_INVENTARIO_PERMANENTE_UNIDADES_LISTAR(dtFechaDesde, dtFechaHasta, intAlmacen);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardex()
                        {
                            almac_icod_almacen = item.almac_icod_almacen,
                            prdc_icod_producto = item.prdc_icod_producto,
                            strAlmacen = item.strAlmacen,
                            strCodProducto = item.strCodProducto,
                            strProducto = item.strProducto,
                            strUnidadMedida = item.strUnidadMedida,
                            SaldoAnterior = Convert.ToDecimal(item.dblSaldoAnterior),
                            cantidadIngreso = Convert.ToDecimal(item.dblIngresos),
                            cantidadSalida = Convert.ToDecimal(item.dblSalidas),
                            dblSaldo = Convert.ToDecimal(item.dblSaldoActual)
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

        public List<EKardex> listarVerificarStock(int anio)
        {
            List<EKardex> lista = new List<EKardex>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_VERIFICAR_STOCK(anio);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardex()
                        {
                            almac_icod_almacen = item.almac_icod_almacen,
                            prdc_icod_producto = item.prdc_icod_producto,
                            strAlmacen = item.strAlmacen,
                            strCodProducto = item.strCodProducto,
                            strProducto = item.strProducto,
                            strUnidadMedida = item.strUnidadMedida,
                            dblSaldo = Convert.ToDecimal(item.stock),
                            dblSaldoReal = Convert.ToDecimal(item.StockReal)
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


        public List<EKardex> listarAlmacenSaldoInicial(int intEjercicio)
        {
            List<EKardex> lista = new List<EKardex>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {

                    var query = dc.SGEA_ALMACEN_SALDO_INICIAL_LISTAR(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardex()
                        {
                            kardc_icod_correlativo = item.kardc_icod_correlativo,
                            kardc_fecha_movimiento = item.kardc_fecha_movimiento,
                            almac_icod_almacen = item.almac_icod_almacen,
                            prdc_icod_producto = item.prdc_icod_producto,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            kardc_numero_doc = item.kardc_numero_doc,
                            kardc_icantidad_prod = Convert.ToDecimal(item.kardc_icantidad_prod),
                            strAlmacen = item.strDesAlmacen,
                            strCodProducto = item.strCodProducto,
                            strProducto = item.strDesProducto,
                            strUnidadMedida = item.strUnidadMedida,
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

        public List<EKardex> listarAlmacenSaldoInicialPorCierreAnual(int intEjercicio)
        {
            List<EKardex> lista = new List<EKardex>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_SALDO_INICIAL_POR_CIERRE(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardex()
                        {
                            kardc_icod_correlativo = item.kardc_icod_correlativo,
                            almac_icod_almacen = item.almac_icod_almacen,
                            prdc_icod_producto = item.prdc_icod_producto,
                            kardc_icantidad_prod = Convert.ToDecimal(item.kardc_icantidad_prod)
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

        public int insertarKardex(EKardex oBe)
        {
            int? intIcod = 0;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_KARDEX_INSERTAR(
                        ref intIcod,
                        oBe.kardc_ianio,
                        oBe.kardc_fecha_movimiento,
                        oBe.almac_icod_almacen,
                        oBe.prdc_icod_producto,
                        oBe.kardc_icantidad_prod,
                        oBe.tdocc_icod_tipo_doc,
                        oBe.kardc_numero_doc,
                        oBe.kardc_tipo_movimiento,
                        oBe.kardc_iid_motivo,
                        oBe.kardc_beneficiario,
                        oBe.kardc_observaciones,
                        oBe.kardc_flag_pase,
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
        public void modificarKardex(EKardex oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_KARDEX_MODIFICAR(
                        oBe.kardc_icod_correlativo,
                        oBe.kardc_ianio,
                        oBe.kardc_fecha_movimiento,
                        oBe.almac_icod_almacen,
                        oBe.prdc_icod_producto,
                        oBe.kardc_icantidad_prod,
                        oBe.tdocc_icod_tipo_doc,
                        oBe.kardc_numero_doc,
                        oBe.kardc_tipo_movimiento,
                        oBe.kardc_iid_motivo,
                        oBe.kardc_beneficiario,
                        oBe.kardc_observaciones,
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
        public void eliminarKardex(EKardex oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_KARDEX_ELIMINAR(
                        oBe.kardc_icod_correlativo,
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
        public void actualizarKardexDoc(Int64 id_kardex, int id_tipo_doc, string strNroDoc, int intUsuario, string strPc)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGE_KARDEX_ACTUALIZAR_DOC(
                        id_kardex,
                        id_tipo_doc,
                        strNroDoc,
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
        #endregion
        #region Stock
        public void actualizarStock(EStock oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_STOCK_ACTUALIZAR(
                        oBe.stocc_ianio,
                        oBe.almac_icod_almacen,
                        oBe.prdc_icod_producto,
                        oBe.stocc_stock_producto,
                        oBe.intTipoMovimiento

                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EStock> listarStockPorAlmacen(int intEjercicio, int intAlmacen)
        {
            List<EStock> lista = new List<EStock>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_STOCK_POR_ALMACEN(intEjercicio, intAlmacen);
                    foreach (var item in query)
                    {
                        lista.Add(new EStock()
                        {
                            prdc_icod_producto = item.prdc_icod_producto,
                            almac_icod_almacen = item.almac_icod_almacen,
                            stocc_stock_producto = item.stocc_stock_producto,
                            strAlmacen = item.strAlmacen,
                            strDesProducto = item.strDesProducto,
                            strCodProducto = item.strCodProducto,
                            strCodUM = item.strCodUM,
                            strDesUM = item.strDesUM,
                            strCategoria = item.strCategoria,
                            strSubCategoriaUno = item.strSubCategoriaUno,
                            strSubCategoriaDos = item.strSubCategoriaDos,
                            strEditorial = item.strEditorial,
                            prdc_nPrecio_soles = Convert.ToDecimal(item.prdc_nPrecio_soles),
                            prdc_nPrecio_dolares = Convert.ToDecimal(item.prdc_nPrecio_dolares)

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
        public List<EStock> listarStockPorAlmacenOptimizado(int intEjercicio, int intAlmacen, string codigo, string descripcion)
        {
            List<EStock> lista = new List<EStock>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_STOCK_POR_ALMACEN_OPTIMIZADO(intEjercicio, intAlmacen, codigo, descripcion);
                    foreach (var item in query)
                    {
                        lista.Add(new EStock()
                        {
                            prdc_icod_producto = item.prdc_icod_producto,
                            almac_icod_almacen = item.almac_icod_almacen,
                            stocc_stock_producto = item.stocc_stock_producto,
                            strAlmacen = item.strAlmacen,
                            strDesProducto = item.strDesProducto,
                            strCodProducto = item.strCodProducto,
                            strCodUM = item.strCodUM,
                            strDesUM = item.strDesUM,
                            CodigoSUNAT = item.CodigoSUNAT,
                            unidc_icod_unidad_medida = Convert.ToInt32(item.unidc_icod_unidad_medida)

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

        public List<EStock> listarStockPorAlmacenOptimizadoAfecto(int intEjercicio, int intAlmacen, string codigo, string descripcion, Boolean afecto)
        {
            List<EStock> lista = new List<EStock>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_STOCK_POR_ALMACEN_OPTIMIZADO_AFECTO(intEjercicio, intAlmacen, codigo, descripcion, afecto);
                    foreach (var item in query)
                    {
                        lista.Add(new EStock()
                        {
                            prdc_icod_producto = item.prdc_icod_producto,
                            almac_icod_almacen = item.almac_icod_almacen,
                            stocc_stock_producto = item.stocc_stock_producto,
                            strAlmacen = item.strAlmacen,
                            strDesProducto = item.strDesProducto,
                            strCodProducto = item.strCodProducto,
                            strCodUM = item.strCodUM,
                            strDesUM = item.strDesUM,
                            strCategoria = item.strCategoria,
                            strSubCategoriaUno = item.strSubCategoriaUno,
                            strSubCategoriaDos = item.strSubCategoriaDos,
                            strEditorial = item.strEditorial,
                            prdc_nPrecio_soles = Convert.ToDecimal(item.prdc_nPrecio_soles),
                            prdc_nPrecio_dolares = Convert.ToDecimal(item.prdc_nPrecio_dolares),
                            prdc_nPor_Descuento = Convert.ToDecimal(item.prdc_nPor_Descuento),
                            prdc_nPrecio_venta = Convert.ToDecimal(item.prdc_nPrecio_venta),
                            prdc_nPrecio_venta_Dol = Convert.ToDecimal(item.prdc_nPrecio_venta_Dol)

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
        public decimal listarStockProductoPorAlmacen(int intEjercicio, int intAlmacen, int almac_icod_almacen)
        {
            decimal? Stock_producto = 0;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_STOCK_PRODUCTO_X_ALMACEN(
                        ref Stock_producto,
                        intEjercicio,
                        intAlmacen,
                        almac_icod_almacen);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Convert.ToDecimal(Stock_producto);
        }
        #endregion
        #region Inventario Cab
        public List<EInventarioCab> listarInventarioFisico(int intInventario)
        {
            List<EInventarioCab> lista = new List<EInventarioCab>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_INVENTARIO_CAB_LISTAR(intInventario);
                    foreach (var item in query)
                    {
                        lista.Add(new EInventarioCab()
                        {
                            invc_icod_inventario = item.invc_icod_inventario,
                            invc_iid_correlativo = Convert.ToInt32(item.invc_iid_correlativo),
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            invc_sfecha_inventario = Convert.ToDateTime(item.invc_sfecha_inventario),
                            tablc_iid_tipo_inventario = Convert.ToInt32(item.tablc_iid_tipo_inventario),
                            tablc_iid_situacion = Convert.ToInt32(item.tablc_iid_situacion),
                            invc_vobservaciones = item.invc_vobservaciones,
                            strTipoInventario = item.strTipoInventario,
                            strSituacion = item.strSituacion,
                            strAlmacen = item.strAlmacen,
                            ningc_icod_nota_ingreso = item.ningc_icod_nota_ingreso,
                            nsalc_icod_nota_salida = item.nsalc_icod_nota_salida
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
        public int insertarInventarioFisico(EInventarioCab oBe)
        {
            int? intIcod = 0;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_INVENTARIO_CAB_INSERTAR(
                        ref intIcod,
                        oBe.invc_iid_correlativo,
                        oBe.almac_icod_almacen,
                        oBe.invc_sfecha_inventario,
                        oBe.tablc_iid_tipo_inventario,
                        oBe.tablc_iid_situacion,
                        oBe.invc_vobservaciones,
                        oBe.ningc_icod_nota_ingreso,
                        oBe.nsalc_icod_nota_salida,
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

        public void modificarInventarioFisico(EInventarioCab oBe)
        {

            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_INVENTARIO_CAB_MODIFICAR(
                        oBe.invc_icod_inventario,
                        oBe.invc_iid_correlativo,
                        oBe.almac_icod_almacen,
                        oBe.invc_sfecha_inventario,
                        oBe.tablc_iid_tipo_inventario,
                        oBe.tablc_iid_situacion,
                        oBe.invc_vobservaciones,
                        oBe.ningc_icod_nota_ingreso,
                        oBe.nsalc_icod_nota_salida,
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
        public void eliminarInventarioFisico(EInventarioCab oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_INVENTARIO_CAB_ELIMINAR(
                        oBe.invc_icod_inventario,
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
        #region Inventario Det
        public List<EInventarioDet> listarInventarioFisicoDet(int intInventario)
        {
            List<EInventarioDet> lista = new List<EInventarioDet>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_INVENTARIO_DET_LISTAR(intInventario);
                    foreach (var item in query)
                    {
                        lista.Add(new EInventarioDet()
                        {
                            invd_icod_detalle = item.invd_icod_detalle,
                            invc_icod_inventario = Convert.ToInt32(item.invc_icod_inventario),
                            invd_inro_item = Convert.ToInt32(item.invd_inro_item),
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            invd_icantidad = Convert.ToDecimal(item.invd_icantidad),
                            strCodProducto = item.strCodProducto,
                            strDesProducto = item.strDesProducto,
                            strUnidadMedida = item.strUnidadMedida,
                            invd_sis_stock = Convert.ToDecimal(item.dblStock),
                            dblDiferencia = Convert.ToDecimal(item.invd_icantidad) - Convert.ToDecimal(item.dblStock)
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
        public List<EInventarioCab> listarInventarioFisicoDetExcel(int intInventario, DateTime SFECHA)
        {
            List<EInventarioCab> lista = new List<EInventarioCab>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_INVENTARIO_DET_LISTAR_EXCEL(intInventario, SFECHA);
                    foreach (var item in query)
                    {
                        lista.Add(new EInventarioCab()
                        {
                            invc_iid_correlativo = Convert.ToInt32(item.invc_iid_correlativo),
                            invc_vid_correlativo = "INVENTARIO N° " + string.Format("{0:000}", item.invc_iid_correlativo),
                            invc_sfecha_inventario = Convert.ToDateTime(item.invc_sfecha_inventario),
                            tablc_iid_tipo_inventario = Convert.ToInt32(item.tablc_iid_tipo_inventario),
                            invc_vobservaciones = item.invc_vobservaciones,
                            invd_icod_detalle = item.invd_icod_detalle,
                            invc_icod_inventario = Convert.ToInt32(item.invc_icod_inventario),
                            invd_inro_item = Convert.ToInt32(item.invd_inro_item),
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            invd_icantidad = Convert.ToDecimal(item.invd_icantidad),
                            strCodProducto = item.strCodProducto,
                            strDesProducto = item.strDesProducto,
                            strCategoria = item.strCategoria,
                            strSubCategoriaUno = item.strSubCategoriaUno,
                            strSubCategoriaDos = item.strSubCategoriaDos,
                            strEditorial = item.strEditorial,
                            strUnidadMedida = item.strUnidadMedida,
                            invd_sis_stock = Convert.ToDecimal(item.dblStock),
                            dblDiferencia = Convert.ToDecimal(item.invd_icantidad) - Convert.ToDecimal(item.dblStock),
                            kardv_precio_costo_promedio = item.PCP,
                            kardv_costo_total = Convert.ToDecimal(item.PCP * item.invd_icantidad),
                            dcPrecioProducto = Convert.ToDecimal(item.dcPrecioProducto),
                            dcPrecioTotal = Convert.ToDecimal(item.dcPrecioProducto * item.invd_icantidad)
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
        public List<EInventarioDet> listarInventarioFisicoDetActualizacion(int intInventario)
        {
            List<EInventarioDet> lista = new List<EInventarioDet>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_INVENTARIO_DET_ACTUALIZACION_LISTAR(intInventario);
                    foreach (var item in query)
                    {
                        lista.Add(new EInventarioDet()
                        {
                            invd_icod_detalle = item.invd_icod_detalle,
                            invc_icod_inventario = Convert.ToInt32(item.invc_icod_inventario),
                            invd_inro_item = Convert.ToInt32(item.invd_inro_item),
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            invd_icantidad = Convert.ToDecimal(item.invd_icantidad),
                            strCodProducto = item.strCodProducto,
                            strDesProducto = item.strDesProducto,
                            strUnidadMedida = item.strUnidadMedida,
                            invd_sis_stock = Convert.ToDecimal(item.dblStock),
                            dblDiferencia = Convert.ToDecimal(item.invd_icantidad) - Convert.ToDecimal(item.dblStock)
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
        public void insertarInventarioFisicoDet(EInventarioDet oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {

                    dc.SGEA_INVENTARIO_DET_INSERTAR(
                       oBe.invc_icod_inventario,
                       oBe.invd_inro_item,
                       oBe.prdc_icod_producto,
                       oBe.invd_sis_stock,
                       oBe.invd_icantidad,
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

        public void modificarInventarioFisicoDet(EInventarioDet oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {

                    dc.SGEA_INVENTARIO_DET_MODIFICAR(
                       oBe.invd_icod_detalle,
                       oBe.invc_icod_inventario,
                       oBe.invd_inro_item,
                       oBe.prdc_icod_producto,
                       oBe.invd_sis_stock,
                       oBe.invd_icantidad,
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

        public void eliminarInventarioFisicoDet(EInventarioDet oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_INVENTARIO_DET_ELIMINAR(
                        oBe.invd_icod_detalle,
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
        #region Transferencia
        public List<ETransferenciaAlmacen> listarTransferenciaAlmacen(int intEjercicio, DateTime? dtFechaDesde, DateTime? dtFechaHasta)
        {
            List<ETransferenciaAlmacen> lista = new List<ETransferenciaAlmacen>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_TRANSFERENCIA_ALMACEN_LISTAR(intEjercicio, dtFechaDesde, dtFechaHasta);
                    foreach (var item in query)
                    {
                        lista.Add(new ETransferenciaAlmacen()
                        {
                            trfc_icod_transf = item.trfc_icod_transf,
                            trfc_inum_transf = Convert.ToInt32(item.trfc_inum_transf),
                            trfc_sfecha_transf = Convert.ToDateTime(item.trfc_sfecha_transf),
                            almac_icod_alm_sal = Convert.ToInt32(item.almac_icod_alm_sal),
                            almac_icod_alm_ing = Convert.ToInt32(item.almac_icod_alm_ing),
                            trnfc_iid_motivo = item.trnfc_iid_motivo,
                            trnfc_vobservaciones = item.trnfc_vobservaciones,
                            strAlmacenSal = item.strAlmacenSal,
                            strAlmacenIng = item.strAlmacenIng,
                            strMotivo = item.strMotivo
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
        public int insertarTransferenciaAlmacen(ETransferenciaAlmacen oBe)
        {
            int? intIcod = 0;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_TRANSFERENCIA_ALMACEN_INSERTAR(
                        ref intIcod,
                        oBe.trfc_inum_transf,
                        oBe.trfc_sfecha_transf,
                        oBe.almac_icod_alm_sal,
                        oBe.almac_icod_alm_ing,
                        oBe.trnfc_iid_motivo,
                        oBe.trnfc_vobservaciones,
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
        public void modificarTransferenciaAlmacen(ETransferenciaAlmacen oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_TRANSFERENCIA_ALMACEN_MODIFICAR(
                        oBe.trfc_icod_transf,
                        oBe.trfc_inum_transf,
                        oBe.trfc_sfecha_transf,
                        oBe.trnfc_iid_motivo,
                        oBe.trnfc_vobservaciones,
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
        public void eliminarTransferenciaAlmacen(ETransferenciaAlmacen oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_TRANSFERENCIA_ALMACEN_ELIMINAR(
                        oBe.trfc_icod_transf,
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
        #region Transferencia Det
        public List<ETransferenciaAlmacenDet> listarTransferenciaAlmacenDet(int intIcod)
        {
            List<ETransferenciaAlmacenDet> lista = new List<ETransferenciaAlmacenDet>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_TRANSFERENCIA_ALMACEN_DET_LISTAR(intIcod);
                    foreach (var item in query)
                    {
                        lista.Add(new ETransferenciaAlmacenDet()
                        {
                            trfd_icod_detalle_transf = item.trfd_icod_detalle_transf,
                            trfc_icod_transf = item.trfc_icod_transf,
                            trfd_nro_item = Convert.ToInt32(item.trfd_nro_item),
                            prdc_icod_producto = item.prdc_icod_producto,
                            kardc_icod_correlativo_sal = item.kardc_icod_correlativo_sal,
                            kardc_icod_correlativo_ing = item.kardc_icod_correlativo_ing,
                            trfd_ncantidad = item.trfd_ncantidad,
                            strCodProducto = item.strCodProducto,
                            strDesProducto = item.strDesProducto,
                            strCodUnidadMedida = item.strCodUnidadMedida,
                            dblStockDisponible = item.dblStockDisponible
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
        public void insertarTransferenciaAlmacenDet(ETransferenciaAlmacenDet oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_TRANSFERENCIA_ALMACEN_DET_INSERTAR(
                        oBe.trfc_icod_transf,
                        oBe.trfd_nro_item,
                        oBe.prdc_icod_producto,
                        oBe.kardc_icod_correlativo_sal,
                        oBe.kardc_icod_correlativo_ing,
                        oBe.trfd_ncantidad,
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
        public void modificarTransferenciaAlmacenDet(ETransferenciaAlmacenDet oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_TRANSFERENCIA_ALMACEN_DET_MODIFICAR(
                        oBe.trfd_icod_detalle_transf,
                        oBe.trfd_nro_item,
                        oBe.trfd_ncantidad,
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
        public void eliminarTransferenciaAlmacenDet(ETransferenciaAlmacenDet oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_TRANSFERENCIA_ALMACEN_DET_ELIMINAR(
                        oBe.trfd_icod_detalle_transf,
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
        #region Pase AlMACEN
        public void PaseSaldosAlmacenes(int periodo, int iUSUARIO)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_STOCK_PRODUCTO_CIERRE(periodo, iUSUARIO);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void stockActualizarConKardex(int periodo)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_STOCK_ACTUALIZAR_CON_KADEX(periodo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Plantilla Empaque

        public List<EEmpaquePlantilla> EmpaquePlantillaListar()
        {
            try
            {
                List<EEmpaquePlantilla> lista = new List<EEmpaquePlantilla>();
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_PLANTILLA_EMPAQUE_CAB_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EEmpaquePlantilla()
                        {
                            plemc_iid = item.plemc_iid,
                            plemc_icod = item.plemc_icod,
                            plemc_vcod = string.Format("{0:000000}", item.plemc_icod),
                            plemc_sfecha = item.plemc_sfecha,
                            plemc_vobservacion = item.plemc_vobservacion,
                            prdc_icod_producto = item.prdc_icod_producto,
                            prdc_vdescripcion_larga = item.prdc_vdescripcion_larga,
                            unidc_vabreviatura_unidad_medida = item.unidc_vabreviatura_unidad_medida,
                            prdc_vcode_producto = item.prdc_vcode_producto
                        });
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? EmpaquePlantillaInsertar(EEmpaquePlantilla obe)
        {
            try
            {
                int? id = null;
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_PLANTILLA_EMPAQUE_CAB_INSERTAR(
                        ref id,
                        obe.plemc_icod,
                        obe.plemc_sfecha,
                        obe.plemc_vobservacion,
                        obe.prdc_icod_producto,
                        obe.intUsuario,
                        obe.strPc
                        );
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EmpaquePlantillaModificar(EEmpaquePlantilla obe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_PLANTILLA_EMPAQUE_CAB_MODIFICAR(
                        obe.plemc_iid,
                        obe.plemc_icod,
                        obe.plemc_sfecha,
                        obe.plemc_vobservacion,
                        obe.prdc_icod_producto,
                        obe.intUsuario,
                        obe.strPc
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EmpaquePlantillaEliminar(EEmpaquePlantilla obe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_PLANTILLA_EMPAQUE_CAB_ELIMINAR(
                        obe.plemc_iid,
                        obe.intUsuario,
                        obe.strPc
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion Plantilla Empaque
        #region Plantilla Empaque Detalle

        public List<EEmpaquePlantilla> EmpaquePlantillaDetListar(int plemc_iid)
        {
            try
            {
                List<EEmpaquePlantilla> lista = new List<EEmpaquePlantilla>();
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_PLANTILLA_EMPAQUE_DET_LISTAR(plemc_iid);
                    foreach (var item in query)
                    {
                        lista.Add(new EEmpaquePlantilla()
                        {
                            plemd_iid = item.plemd_iid,
                            plemc_iid = item.plemc_iid,
                            plemd_vitem = string.Format("{0:00}", item.plemd_iitem),
                            plemd_iitem = item.plemd_iitem,
                            prdc_icod_producto = item.prdc_icod_producto,
                            plemd_ncantidad = item.plemd_ncantidad,
                            prdc_vdescripcion_larga = item.prdc_vdescripcion_larga,
                            prdc_vcode_producto = item.prdc_vcode_producto,
                            unidc_vabreviatura_unidad_medida = item.unidc_vabreviatura_unidad_medida
                        });
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EmpaquePlantillaDetInsertar(EEmpaquePlantilla obe)
        {
            try
            {

                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_PLANTILLA_EMPAQUE_DET_INSERTAR(
                        obe.plemd_iid,
                        obe.plemc_iid,
                        obe.plemd_iitem,
                        obe.prdc_icod_producto,
                        obe.plemd_ncantidad,
                        obe.intUsuario,
                        obe.strPc
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EmpaquePlantillaDetModificar(EEmpaquePlantilla obe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_PLANTILLA_EMPAQUE_DET_MODIFICAR(
                        obe.plemd_iid,
                        obe.plemd_iitem,
                        obe.prdc_icod_producto,
                        obe.plemd_ncantidad,
                        obe.intUsuario,
                        obe.strPc
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EmpaquePlantillaDetEliminar(EEmpaquePlantilla obe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_PLANTILLA_EMPAQUE_DET_ELIMINAR(
                        obe.plemd_iid,
                        obe.intUsuario,
                        obe.strPc
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
        #region Empaque Desempaque Cab

        public List<EEmpaqueDesempaqueCab> EmpaqueDesempaqueListar()
        {
            try
            {
                List<EEmpaqueDesempaqueCab> lista = new List<EEmpaqueDesempaqueCab>();
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_EMPAQUE_DESEMPAQUE_CAB_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EEmpaqueDesempaqueCab()
                        {
                            emp_vnuemro_desempaque = item.emp_vnuemro_desempaque,
                            emp_inuemro_desempaque = Convert.ToInt32(item.emp_vnuemro_desempaque),
                            emp_icod_desempaque = item.emp_icod_desempaque,
                            emp_sfecha_desempaque = item.emp_sfecha_desempaque,
                            plemc_iid = item.plemc_iid,
                            prdc_icod_producto = item.prdc_icod_producto,
                            kardc_icod_correlativo = item.kardc_icod_correlativo,
                            almac_icod_almacen = item.almac_icod_almacen,
                            emp_vobservaciones = item.emp_vobservaciones,
                            emp_dcantidad_desempaque = item.emp_dcantidad_desempaque,
                            Tablc_itipo_empaque = item.Tablc_itipo_empaque,
                            almac_vdescripcion = item.almac_vdescripcion,
                            DescripTipo = item.DescripTipo,
                            prdc_vdescripcion_larga = item.prdc_vdescripcion_larga,
                            prdc_vpart_number = item.prdc_vpart_number,
                            prdc_vcode_producto = item.prdc_vcode_producto,
                            unidc_vabreviatura_unidad_medida = item.unidc_vabreviatura_unidad_medida,
                            plemc_Vicod = string.Format("{0:000000}", item.plemc_icod)
                        });
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? EmpaqueDesempaqueInsertar(EEmpaqueDesempaqueCab obe)
        {
            try
            {
                int? emp_icod_desempaque = null;
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_EMPAQUE_DESEMPAQUE_CAB_INSERTAR(
                        ref emp_icod_desempaque,
                        obe.emp_vnuemro_desempaque,
                        obe.emp_sfecha_desempaque,
                        obe.plemc_iid,
                        obe.prdc_icod_producto,
                        obe.kardc_icod_correlativo,
                        obe.almac_icod_almacen,
                        obe.Tablc_itipo_empaque,
                        obe.emp_vobservaciones,
                        obe.emp_dcantidad_desempaque,
                        obe.intUsuario,
                        obe.strPc,
                        obe.emp_flag_estado
                        );
                }
                return emp_icod_desempaque;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EmpaqueDesempaqueModificar(EEmpaqueDesempaqueCab obe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_EMPAQUE_DESEMPAQUE_CAB_MODIFICAR(
                        obe.emp_icod_desempaque,
                        obe.emp_vnuemro_desempaque,
                        obe.emp_sfecha_desempaque,
                        obe.emp_vobservaciones,
                        obe.emp_dcantidad_desempaque,
                        obe.intUsuario,
                        obe.strPc
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EmpaqueDesempaqueEliminar(EEmpaqueDesempaqueCab obe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_EMPAQUE_DESEMPAQUE_CAB_ELIMINAR(
                        obe.emp_icod_desempaque,
                        obe.intUsuario,
                        obe.strPc
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion Plantilla Empaque
        #region Empaque Desempaque Det

        public List<EEmpaqueDesempaqueCab> EmpaqueDesempaqueDetListar(int emp_icod_desempaque)
        {
            try
            {
                List<EEmpaqueDesempaqueCab> lista = new List<EEmpaqueDesempaqueCab>();
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_EMPAQUE_DESEMPAQUE_DET_LISTAR(emp_icod_desempaque);
                    foreach (var item in query)
                    {
                        lista.Add(new EEmpaqueDesempaqueCab()
                        {
                            emp_icod_desempaque = item.emp_icod_desempaque,
                            empd_iitem_desempaque = item.empd_iitem_desempaque,
                            prdc_icod_producto = item.prdc_icod_producto,
                            empd_Vitem_desempaque = string.Format("{0:00}", item.empd_iitem_desempaque),
                            kardc_icod_correlativo = item.kardc_icod_correlativo,
                            empd_dcantidad_desempaque = item.empd_dcantidad_desempaque,
                            prdc_vdescripcion_larga = item.prdc_vdescripcion_larga,
                            prdc_vpart_number = item.prdc_vpart_number,
                            prdc_vcode_producto = item.prdc_vcode_producto,
                            unidc_vabreviatura_unidad_medida = item.unidc_vabreviatura_unidad_medida
                        });
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EmpaqueDesempaqueDetInsertar(EEmpaqueDesempaqueCab obe)
        {
            try
            {

                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_EMPAQUE_DESEMPAQUE_DET_INSERTAR(
                        obe.emp_icod_desempaque,
                        obe.empd_iitem_desempaque,
                        obe.prdc_icod_producto,
                        obe.kardc_icod_correlativo,
                        obe.empd_dcantidad_desempaque,
                        obe.intUsuario,
                        obe.strPc,
                        obe.empd_flag_estado
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EmpaqueDesempaqueDetModificar(EEmpaqueDesempaqueCab obe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_EMPAQUE_DESEMPAQUE_DET_MODIFICAR(
                        obe.empd_icod_desempaque,
                        obe.empd_dcantidad_desempaque,
                        obe.intUsuario,
                        obe.strPc
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EmpaqueDesempaqueDetEliminar(EEmpaqueDesempaqueCab obe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_EMPAQUE_DESEMPAQUE_DET_ELIMINAR(
                        obe.empd_icod_desempaque,
                        obe.intUsuario,
                        obe.strPc
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
        #region Editorial
        public List<EEditorial> listarEditorial()
        {
            List<EEditorial> lista = new List<EEditorial>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {

                    var query = dc.SGEA_EDITORIAL_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EEditorial()
                        {
                            edit_icod_editorial = item.edit_icod_editorial,
                            edit_iid_editorial = Convert.ToInt32(item.edit_iid_editorial),
                            edit_iSituacion = Convert.ToBoolean(item.edit_iSituacion),
                            edit_vdescripcion = item.edit_vdescripcion,
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
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
        public int insertarEditorial(EEditorial oBe)
        {
            int? intIcod = 0;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_EDITORIAL_INSERTAR(
                        ref intIcod,
                        oBe.edit_iid_editorial,
                        oBe.edit_iSituacion,
                        oBe.edit_vdescripcion,
                        oBe.tarec_festado,
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
        public void modificarEditorial(EEditorial oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_EDITORIAL_MODIFICAR(
                        oBe.edit_icod_editorial,
                        oBe.edit_iSituacion,
                        oBe.edit_vdescripcion,
                        oBe.proc_icod_proveedor);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarEditorial(EEditorial oBe)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_EDITORIAL_ELIMINAR(
                        oBe.edit_icod_editorial);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Registro Categorias

        public int? RegistroCategoriasInsertar(ECategoria obj)
        {
            int? Catc_iid_tipo_tabla = 0;
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_CATEGORIA_INSERTAR(
                          ref Catc_iid_tipo_tabla,
                          obj.Catc_vdescripcion,
                          obj.Catc_cestado
                    );

                }
                return Catc_iid_tipo_tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void RegistroCategoriasActualizar(ECategoria obj)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_CATEGORIA_MODIFICAR(
                         obj.Catc_iid_tipo_tabla,
                         obj.Catc_vdescripcion);


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RegistroCategoriasEliminar(ECategoria obj)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_CATEGORIA_ELIMINAR(obj.Catc_iid_tipo_tabla);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ECategoria> RegistroCategoriasListar()
        {
            List<ECategoria> lista = new List<ECategoria>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    lista = new List<ECategoria>();
                    var query = dc.SGEA_CATEGORIA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ECategoria()
                        {
                            Catc_iid_tipo_tabla = item.Catc_iid_tipo_tabla,
                            vidTipoTabla = string.Format("{0:00}", item.Catc_iid_tipo_tabla),
                            Catc_vdescripcion = item.Catc_vdescripcion,
                            Catc_cestado = Convert.ToChar(item.Catc_cestado),
                            vestado = item.estado
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
        #region Registro Categoria Nivel Uno
        public List<ECategoria> ListarCategoriaSubUno(ECategoria obj)
        {
            List<ECategoria> lista = new List<ECategoria>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    lista = new List<ECategoria>();
                    var query = dc.SGEA_SUB_CATEGORIA_NIVEL_UNO_X_CODIGO_LISTAR(obj.Catc_iid_tipo_tabla);
                    foreach (var item in query)
                    {

                        ECategoria BE = new ECategoria();
                        BE.CatNUno_iid_tabla_registro = item.CatNUno_iid_tabla_registro;
                        BE.CatNUno_iid_tipo_tabla = Convert.ToInt32(item.CatNUno_iid_tipo_tabla);
                        BE.CatNUno_icorrelativo_registro = Convert.ToInt32(item.CatNUno_icorrelativo_registro);
                        BE.tarec_viid_correlativo = string.Format("{0:000}", item.CatNUno_icorrelativo_registro);
                        BE.CatNUno_vdescripcion = item.CatNUno_vdescripcion;
                        BE.CatNUno_cestado = Convert.ToChar(item.CatNUno_cestado);
                        BE.CatNUno_vvalor_texto = item.CatNUno_vvalor_texto;
                        lista.Add(BE);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ECategoria> ListarCategoriaSubUnoo(int tablc_iid_tipo_tabla)
        {
            List<ECategoria> lista = new List<ECategoria>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    lista = new List<ECategoria>();
                    var query = dc.SGEA_SUB_CATEGORIA_NIVEL_UNO_X_CODIGO_LISTAR(tablc_iid_tipo_tabla);
                    foreach (var item in query)
                    {

                        ECategoria BE = new ECategoria();
                        BE.CatNUno_iid_tabla_registro = item.CatNUno_iid_tabla_registro;
                        BE.CatNUno_iid_tipo_tabla = Convert.ToInt32(item.CatNUno_iid_tipo_tabla);
                        BE.CatNUno_icorrelativo_registro = Convert.ToInt32(item.CatNUno_icorrelativo_registro);
                        BE.tarec_viid_correlativo = string.Format("{0:000}", item.CatNUno_icorrelativo_registro);
                        BE.CatNUno_vdescripcion = item.CatNUno_vdescripcion;
                        BE.CatNUno_cestado = Convert.ToChar(item.CatNUno_cestado);
                        BE.CatNUno_vvalor_texto = item.CatNUno_vvalor_texto;
                        lista.Add(BE);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public void InsertarCategoriaSubUno(ECategoria obj)
        {
            int? CatNUno_iid_tabla_registro = 0;

            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_SUB_CATEGORIA_NIVEL_UNO_INSERTAR(
                        ref CatNUno_iid_tabla_registro,
                        obj.CatNUno_iid_tipo_tabla,
                        obj.CatNUno_icorrelativo_registro,
                        obj.CatNUno_vdescripcion,
                        obj.CatNUno_nvalor_numerico,
                        obj.CatNUno_vvalor_texto,
                        obj.CatNUno_cestado,
                        obj.intUsuario
                   );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarCategoriaSubUno(ECategoria obj)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_SUB_CATEGORIA_NIVEL_UNO_MODIFICAR(
                        obj.CatNUno_iid_tabla_registro,
                        obj.CatNUno_vdescripcion,
                        obj.CatNUno_vvalor_texto,
                        obj.intUsuario);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarCategoriaSubUno(ECategoria obj)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_SUB_CATEGORIA_NIVEL_UNO_ELIMINAR(obj.CatNUno_iid_tabla_registro, obj.intUsuario);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Registro Categoria Nivel Dos
        public List<ECategoria> ListarCategoriaSubDos(ECategoria obj)
        {
            List<ECategoria> lista = new List<ECategoria>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    lista = new List<ECategoria>();
                    var query = dc.SGEA_SUB_CATEGORIA_NIVEL_DOS_X_CODIGO_LISTAR(obj.CatNUno_iid_tabla_registro);
                    foreach (var item in query)
                    {

                        ECategoria BE = new ECategoria();
                        BE.CatNDos_iid_tabla_registro = item.CatNDos_iid_tabla_registro;
                        BE.CatNUno_iid_tabla_registro = Convert.ToInt32(item.CatNUno_iid_tabla_registro);
                        BE.CatNDos_icorrelativo_registro = Convert.ToInt32(item.CatNDos_icorrelativo_registro);
                        BE.tarec_viid_correlativo = string.Format("{0:000}", item.CatNDos_icorrelativo_registro);
                        BE.CatNDos_vdescripcion = item.CatNDos_vdescripcion;
                        BE.CatNDos_cestado = Convert.ToChar(item.CatNDos_cestado);
                        BE.CatNDos_vvalor_texto = item.CatNDos_vvalor_texto;
                        lista.Add(BE);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ECategoria> ListarCategoriaSubDoss(int tablc_iid_tipo_tabla)
        {
            List<ECategoria> lista = new List<ECategoria>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    lista = new List<ECategoria>();
                    var query = dc.SGEA_SUB_CATEGORIA_NIVEL_DOS_X_CODIGO_LISTAR(tablc_iid_tipo_tabla);
                    foreach (var item in query)
                    {

                        ECategoria BE = new ECategoria();
                        BE.CatNDos_iid_tabla_registro = item.CatNDos_iid_tabla_registro;
                        BE.CatNUno_iid_tabla_registro = Convert.ToInt32(item.CatNUno_iid_tabla_registro);
                        BE.CatNDos_icorrelativo_registro = Convert.ToInt32(item.CatNDos_icorrelativo_registro);
                        BE.tarec_viid_correlativo = string.Format("{0:000}", item.CatNDos_icorrelativo_registro);
                        BE.CatNDos_vdescripcion = item.CatNDos_vdescripcion;
                        BE.CatNDos_cestado = Convert.ToChar(item.CatNDos_cestado);
                        BE.CatNDos_vvalor_texto = item.CatNDos_vvalor_texto;
                        lista.Add(BE);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public void InsertarCategoriaSubDos(ECategoria obj)
        {
            int? CatNDos_iid_tabla_registro = 0;

            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_SUB_CATEGORIA_NIVEL_DOS_INSERTAR(
                        ref CatNDos_iid_tabla_registro,
                        obj.CatNUno_iid_tabla_registro,
                        obj.CatNDos_icorrelativo_registro,
                        obj.CatNDos_vdescripcion,
                        obj.CatNDos_nvalor_numerico,
                        obj.CatNDos_vvalor_texto,
                        obj.CatNDos_cestado,
                        obj.intUsuario
                   );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarCategoriaSubDos(ECategoria obj)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_SUB_CATEGORIA_NIVEL_DOS_MODIFICAR(
                        obj.CatNDos_iid_tabla_registro,
                        obj.CatNDos_vdescripcion,
                        obj.CatNDos_vvalor_texto,
                        obj.intUsuario);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarCategoriaSubDos(ECategoria obj)
        {
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    dc.SGEA_SUB_CATEGORIA_NIVEL_DOS_ELIMINAR(obj.CatNDos_iid_tabla_registro, obj.intUsuario);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public List<ERequerimientoMaterialesDetalle> listarVerificacionStockRequerimientoMateriales(int IcodRQ)
        {
            List<ERequerimientoMaterialesDetalle> lista = new List<ERequerimientoMaterialesDetalle>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_VERIFICAR_STOCK_REQUERIMIENTOS_MATERIALES_DETALLE_LISTAR(IcodRQ);
                    foreach (var item in query)
                    {
                        lista.Add(new ERequerimientoMaterialesDetalle()
                        {
                            rqmd_icod_req_materiales_detalle = item.rqmd_icod_req_materiales_detalle,
                            rqmd_vcodigo_item_requerim = item.rqmd_vcodigo_item_requerim,
                            hjcd3_icod_rubros_hc = Convert.ToInt32(item.hjcd3_icod_rubros_hc),
                            DescripcionRubro = item.hjcd3_vdescripcion,
                            CodigoRubro = item.hjcd3_vcodigo_relacion,
                            Medida = item.Unidad,
                            Cantidad = Convert.ToDecimal(item.hjcd3_ncantidad),
                            rqmd_cantidad_pedida = Convert.ToDecimal(item.rqmd_cantidad_pedida),
                            rqmd_cantidad_aprobada = Convert.ToDecimal(item.rqmd_cantidad_aprobada),
                            Stock = Convert.ToDecimal(item.Stock),
                            StockDiferencia = (Convert.ToDecimal(item.Stock) > Convert.ToDecimal(item.rqmd_cantidad_aprobada)) ? "0" : (Convert.ToDecimal(item.rqmd_cantidad_aprobada) > Convert.ToDecimal(item.Stock)) ? Convert.ToDecimal(item.StockDiferencia).ToString() : "",
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
        public List<EKardexValorizado> listarKardexValorizadoPorFechaAlmacenProducto(DateTime dtFechaDesde, DateTime dtFechaHasta, int? intAlmacen, int? intProducto, int anio)
        {
            List<EKardexValorizado> lista = new List<EKardexValorizado>();
            try
            {
                using (AlmacenDataContext dc = new AlmacenDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_KARDEX_VALORIZADO_LISTAR_X_FECHA_ALMACEN_PRODUCTO(dtFechaDesde, dtFechaHasta, intAlmacen, intProducto, anio);
                    foreach (var item in query)
                    {
                        lista.Add(new EKardexValorizado()
                        {
                            kardv_sfecha_movimiento = item.kardc_fecha_movimiento,
                            almcc_icod_almacen = item.almac_icod_almacen,
                            prdc_icod_producto = item.prdc_icod_producto,
                            kardv_icantidad_prod = Convert.ToDecimal(item.kardc_icantidad_prod),
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            kardv_inumero_doc = item.kardc_numero_doc,
                            kardv_itipo_movimiento = item.kardc_tipo_movimiento,
                            kardv_iid_motivo = item.kardc_iid_motivo,
                            kardv_vbeneficiario = item.kardc_beneficiario,
                            kardv_vobservaciones = item.kardc_observaciones,
                            dcmlIngreso = Convert.ToDecimal(item.dblIngreso),
                            dcmlSalida = Convert.ToDecimal(item.dblSalida),
                            kardv_monto_saldo_valorizado = Convert.ToDecimal(item.dblSaldo),
                            strTipoDoc = item.strDocumento,
                            Motivo = item.strMotivo.ToUpper(),
                            strDesAlmacenCtbl = item.strAlmacen,
                            strCodProducto = item.strCodProducto,
                            strDesProducto = item.strProducto,
                            kardv_precio_costo_promedio = Convert.ToDecimal(item.kardv_precio_costo_promedio),
                            kardv_monto_total_compra = Convert.ToInt32(item.kardv_monto_total_compra)
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
    }
}
