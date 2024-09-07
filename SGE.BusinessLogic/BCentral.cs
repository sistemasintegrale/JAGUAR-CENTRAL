using SGE.Common;
using SGE.DataAccesDapper.Modules;
using SGE.DataAccess;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using static SGE.Common.Codigos;
using static SGE.Common.Enums;

namespace SGE.BusinessLogic
{
    public class BCentral
    {
        CentralData objCentral = new CentralData();
        AlmacenData objAlmacenData = new AlmacenData();
        private readonly CentralService centralService;

        public BCentral()
        {
            centralService = new CentralService();
        }

        public List<ENotaIngresoOP> NotaIngresoOpListar()
        {
            return objCentral.NotaIngresoOpListar();
        }

        public int UltimoCorrelativoTabla(string NombreTabla)
        {
            return centralService.UltimoCorrelativoTabla(NombreTabla);
        }

        #region Productos
        public List<EProdProducto> ListarProdProductos()
        {
            List<EProdProducto> lista = null;
            try
            {
                //lista = objCentral.ListarProdProductos();
                lista = centralService.ProductoListar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<ControlPersonalOPR> ControlPersonalOprListar()
        {
            return objCentral.ControlPersonalOprListar();
        }

        public List<EProdProducto> ListarProdProductosPorMarca(int icod) => centralService.ProductoListarPorMarca(icod);

        public List<EProdProducto> ListarProdProductoByMarcaModelo(int intMarca, int intModelo)
        {
            return objCentral.ListarProdProductoByMarcaModelo(intMarca, intModelo);
        }
        public List<EProdProducto> ListarProdProductoByMarcaModeloStock(int intMarca, int intModelo, int anio)
        {
            return objCentral.ListarProdProductoByMarcaModeloStock(intMarca, intModelo, anio);
        }

        public List<ENotaIngresoOPDetalle> NotaIngresoOpDetalleListar(int niop_icod_nota_ingreso)
        {
            return objCentral.NotaIngresoOpDetalleListar(niop_icod_nota_ingreso);
        }

        

        public EOrdenPedidoDet OrdenPedidoDetalleGetById(int orped_icod_item_orden_pedido)
        {
            return objCentralData.OrdenPedidoDetalleGetById(orped_icod_item_orden_pedido);
        }
        public ENotaIngresoOPDetalleSaldo OrdenPedidoDetSaldo(int orped_icod_item_orden_pedido)
        {
            var obj = new ENotaIngresoOPDetalleSaldo();
            try
            {
                obj = objCentralData.OrdenPedidoDetSaldo(orped_icod_item_orden_pedido);
                if (obj.niopds_icod_nota_ingreso_detalle_saldo == 0)
                {
                    var OPDet = objCentralData.OrdenPedidoDetalleGetById(orped_icod_item_orden_pedido);
                    //CREAR TABLA SALDO 
                    obj.orped_icod_item_orden_pedido = orped_icod_item_orden_pedido;
                    obj.niopds_saldos_1 = OPDet.orped_cant_talla1;
                    obj.niopds_saldos_2 = OPDet.orped_cant_talla2;
                    obj.niopds_saldos_3 = OPDet.orped_cant_talla3;
                    obj.niopds_saldos_4 = OPDet.orped_cant_talla4;
                    obj.niopds_saldos_5 = OPDet.orped_cant_talla5;
                    obj.niopds_saldos_6 = OPDet.orped_cant_talla6;
                    obj.niopds_saldos_7 = OPDet.orped_cant_talla7;
                    obj.niopds_saldos_8 = OPDet.orped_cant_talla8;
                    obj.niopds_saldos_9 = OPDet.orped_cant_talla9;
                    obj.niopds_saldos_10 = OPDet.orped_cant_talla10;
                    objCentralData.OrdenPedidoDetSaldoInsertar(obj);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return obj;
        }

        public int DestinosGuadar(EDestinos obj) => objCentralData.DestinosGuadar(obj);

        public List<EProdProducto> ListarProdProductosIndexes(string Codigo, string Descripcion, string modelo)
        {
            List<EProdProducto> lista = null;
            try
            {
                lista = objCentral.ListarProdProductosIndexes(Codigo, Descripcion, modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EProdModelo> ModeloListarSimple(int Marca)
        {
            return centralService.ModeloListar(Marca);
            //return objCentral.ModeloListarSimple();
        }



        private EProdProducto GenerarProducto(int idmarca, int idcolor, int idmodelo, int talla, int usuario)
        {
            EProdProducto obj = new EProdProducto();
            var serie = objCentralData.ListarRegistroSerie().Where(x => talla >= Convert.ToInt32(x.resec_vtalla_inicial) && talla <= Convert.ToInt32(x.resec_vtalla_final)).FirstOrDefault();
            var marca = objCentralData.ListarProdTablaRegistro(new EProdTablaRegistro() { iid_tipo_tabla = 11 }).Where(x => Convert.ToInt32(x.icod_tabla) == idmarca).FirstOrDefault();
            var color = objCentralData.ListarProdTablaRegistro(new EProdTablaRegistro() { iid_tipo_tabla = 8 }).Where(x => Convert.ToInt32(x.icod_tabla) == idcolor).FirstOrDefault();

            var modelo = objCentralData.ModeloGetById(idmodelo);
            obj.pr_iid_producto = objCentralData.ProductoPvtGetMaxID();
            obj.pr_iid_marca = Convert.ToInt32(marca.tarec_viid_correlativo);
            obj.pr_iid_modelo = Convert.ToInt32(modelo.mo_iid_modelo);
            obj.pr_iid_color = Convert.ToInt32(color.tarec_viid_correlativo);


            obj.pr_vcodigo_externo = $"CJA{marca.tarec_viid_correlativo}{string.Format("{0:0000}", modelo.mo_iid_modelo)}{color.tarec_viid_correlativo}{talla}";
            obj.pr_vdescripcion_producto = $"{marca.descripcion} {modelo.mo_vdescripcion} {modelo.pr_iid_categoria_descripcion} {modelo.pr_iid_linea_descripcion} {modelo.pr_iid_tipo_marca_descripcion} {color.descripcion} {talla}";
            obj.pr_vabreviatura_producto = $"{marca.tarec_vabreviatura}{modelo.mo_vdescripcion}";
            obj.pr_isituacion_producto = 1;
            obj.unidc_icod_unidad_medida = 1;
            obj.intUsuario = usuario;
            obj.pr_tarec_icorrelativo = Convert.ToInt32(modelo.tarec_icorrelativo);
            obj.pr_afecto_igv = true;
            obj.pr_nporcentaje_igv = Convert.ToDecimal(Parametros.strPorcIGV);
            obj.pr_icod_serie = serie.resec_iid_registro_serie;

            var product = objCentralData.VerificarProductoPvt(obj.pr_vcodigo_externo).FirstOrDefault();
            if (product == null)
            {
                obj.pr_icod_producto = objCentral.InsertarProdProductos(obj);
            }
            else
            {
                obj.pr_icod_producto = product.pr_icod_producto;
            }


            return obj;
        }

        private void NotaIngresoOpDetalleInsertar(ENotaIngresoOP objCab, ENotaIngresoOPDetalle item)
        {
            item.niop_icod_nota_ingreso = objCab.niop_icod_nota_ingreso;
            item.intUsuario = objCab.intUsuario;
            var itemOP = objCentralData.OrdenPedidoDetalleGetById(item.orped_icod_item_orden_pedido);
            for (int i = 0; i < 10; i++)
            {
                int index = i + 1;

                if (Convert.ToInt32(CommonFunctions.GetValueItem(item, $"niopd_cant_{index}")) > 0)
                {
                    int talla = Convert.ToInt32(CommonFunctions.GetValueItem(itemOP, $"orped_talla{index}"));
                    var producto = GenerarProducto(item.orped_imarca, item.orped_icolor, item.orped_imodelo, talla, objCab.intUsuario);
                    EProdKardex objk = new EProdKardex();
                    objk.kardc_ianio = objCab.niop_sfecha.Year;
                    objk.kardx_sfecha = Convert.ToDateTime(objCab.niop_sfecha);
                    objk.iid_almacen = Convert.ToInt32(objCab.niop_ialmacen);
                    objk.iid_producto = producto.pr_icod_producto;
                    objk.puvec_icod_punto_venta = 2;
                    objk.Cantidad = Convert.ToInt32(CommonFunctions.GetValueItem(item, $"niopd_cant_{index}"));
                    objk.NroNota = Convert.ToInt32(objCab.niop_inumero);
                    objk.iid_tipo_doc = (int)TipoDocumento.NotaDeIngresoDeProduccion;
                    objk.NroDocumento = objCab.niop_inumero.ToString("d4");
                    objk.movimiento = 1;
                    objk.Motivo = 5684;
                    objk.Beneficiario = objCab.NombreAlmacen;
                    objk.Observaciones = objCab.niop_vobservacion;
                    objk.intUsuario = objCab.intUsuario;
                    objk.Item = Convert.ToInt32(item.niopd_iitem_nota_ingreso_detalle);
                    objk.stocc_codigo_producto = producto.pr_vcodigo_externo;
                    objk.operacion = 1;
                    objk.orped_icod_item_orden_pedido = item.orped_icod_item_orden_pedido;
                    objk.kardc_iid_correlativo = objCentralData.InsertarKardexpvtOP(objk);
                    
                    CommonFunctions.SetValueItem(item, $"niopd_kardex_{index}", objk.kardc_iid_correlativo);
                    CommonFunctions.SetValueItem(item, $"niopd_iprod_{index}", producto.pr_icod_producto);
                    EProdStockProducto objStock = new EProdStockProducto()
                    {
                        stocc_ianio = Parametros.intEjercicio,
                        stocc_iid_almacen = objk.iid_almacen,
                        stocc_iid_producto = objk.iid_producto,
                        stocc_nstock_prod = objk.Cantidad,
                        intTipoMovimiento = objk.movimiento,
                        puvec_icod_punto_venta = 2,
                        vcodigo_externo = producto.pr_vcodigo_externo,
                    };
                    objCentralData.actualizarStockPvt(objStock);
                }
            }
           
            objCentral.NotaIngresoOpDetalleInsertar(item);
            objCentral.OrdenPedidoDetSaldoActualizar(item.orped_icod_item_orden_pedido);
        }

        private void NotaIngresoOpDetalleModificar(ENotaIngresoOP objCab, ENotaIngresoOPDetalle item)
        {
            item.niop_icod_nota_ingreso = objCab.niop_icod_nota_ingreso;
            item.intUsuario = objCab.intUsuario;
           
            for (int i = 0; i < 10; i++)
            {
                int index = i + 1;

                if (Convert.ToInt32(CommonFunctions.GetValueItem(item, $"niopd_cant_{index}")) > 0)
                {
                    var producto = objCentralData.ProductoPvtGetByID(Convert.ToInt32(CommonFunctions.GetValueItem(item, $"niopd_iprod_{index}")));
                    EProdKardex objk = new EProdKardex();
                    if (Convert.ToInt32(CommonFunctions.GetValueItem(item, $"niopd_kardex_{index}")) > 0)
                    {
                        objk.kardc_iid_correlativo = Convert.ToInt32(CommonFunctions.GetValueItem(item, $"niopd_kardex_{index}"));
                        objk.kardc_ianio = objCab.niop_sfecha.Year;
                        objk.kardx_sfecha = Convert.ToDateTime(objCab.niop_sfecha);
                        objk.iid_almacen = Convert.ToInt32(objCab.niop_ialmacen);
                        objk.iid_producto = producto.pr_icod_producto;
                        objk.puvec_icod_punto_venta = 2;
                        objk.Cantidad = Convert.ToInt32(CommonFunctions.GetValueItem(item, $"niopd_cant_{index}"));
                        objk.NroNota = Convert.ToInt32(objCab.niop_inumero);
                        objk.iid_tipo_doc = (int)TipoDocumento.NotaDeIngresoDeProduccion; 
                        objk.NroDocumento = objCab.niop_inumero.ToString("d4");
                        objk.movimiento = 1;
                        objk.Motivo = 5684;
                        objk.Beneficiario = objCab.NombreAlmacen;
                        objk.Observaciones = objCab.niop_vobservacion;
                        objk.intUsuario = objCab.intUsuario;
                        objk.Item = Convert.ToInt32(item.niopd_iitem_nota_ingreso_detalle);
                        objk.stocc_codigo_producto = producto.pr_vcodigo_externo;
                        objk.operacion = 1;
                        objCentralData.modificarKardexPvt(objk);
                    }
                    else
                    {
                        objk.kardc_ianio = objCab.niop_sfecha.Year;
                        objk.kardx_sfecha = Convert.ToDateTime(objCab.niop_sfecha);
                        objk.iid_almacen = Convert.ToInt32(objCab.niop_ialmacen);
                        objk.iid_producto = producto.pr_icod_producto;
                        objk.puvec_icod_punto_venta = 2;
                        objk.Cantidad = Convert.ToInt32(CommonFunctions.GetValueItem(item, $"niopd_cant_{index}"));
                        objk.NroNota = Convert.ToInt32(objCab.niop_inumero);
                        objk.iid_tipo_doc = 81;
                        objk.NroDocumento = objCab.niop_inumero.ToString("d4");
                        objk.movimiento = 1;
                        objk.Motivo = 5684;
                        objk.Beneficiario = objCab.NombreAlmacen;
                        objk.Observaciones = objCab.niop_vobservacion;
                        objk.intUsuario = objCab.intUsuario;
                        objk.Item = Convert.ToInt32(item.niopd_iitem_nota_ingreso_detalle);
                        objk.stocc_codigo_producto = producto.pr_vcodigo_externo;
                        objk.operacion = 1;
                        objk.kardc_iid_correlativo = objCentralData.InsertarKardexpvt(objk);
                        CommonFunctions.SetValueItem(item, $"niopd_kardex_{index}", objk.kardc_iid_correlativo);
                        CommonFunctions.SetValueItem(item, $"niopd_iprod_{index}", producto.pr_icod_producto);
                    }
                    EProdStockProducto objStock = new EProdStockProducto()
                    {
                        stocc_ianio = Parametros.intEjercicio,
                        stocc_iid_almacen = objk.iid_almacen,
                        stocc_iid_producto = objk.iid_producto,
                        stocc_nstock_prod = objk.Cantidad,
                        intTipoMovimiento = objk.movimiento,
                        puvec_icod_punto_venta = 2,
                        vcodigo_externo = producto.pr_vcodigo_externo,
                    };
                    objCentralData.actualizarStockPvt(objStock);
                }
                else if (Convert.ToInt32(CommonFunctions.GetValueItem(item, $"niopd_kardex_{index}")) > 0)
                {
                    var producto = objCentralData.ProductoPvtGetByID(Convert.ToInt32(CommonFunctions.GetValueItem(item, $"niopd_iprod_{index}")));
                    EProdKardex objk = new EProdKardex();
                    objk.kardc_iid_correlativo = Convert.ToInt32(CommonFunctions.GetValueItem(item, $"niopd_kardex_{index}"));
                    objk.intUsuario = objCab.intUsuario;
                    objCentralData.EliminarKardexPvt(objk);
                    CommonFunctions.SetValueItem(item, $"niopd_kardex_{index}", 0);
                    EProdStockProducto objStock = new EProdStockProducto()
                    {
                        stocc_ianio = Parametros.intEjercicio,
                        stocc_iid_almacen = objCab.niop_ialmacen,
                        stocc_iid_producto = producto.pr_icod_producto,
                        stocc_nstock_prod = 0,
                        puvec_icod_punto_venta = 2,
                        vcodigo_externo = producto.pr_vcodigo_externo,
                    };
                    objCentralData.actualizarStockPvt(objStock);
                }
            }
            
            objCentral.NotaIngresoOpDetalleModificar(item);
            objCentral.OrdenPedidoDetSaldoActualizar(item.orped_icod_item_orden_pedido);
        }

        private void NotaIngresoOpDetalleEliminar(ENotaIngresoOP objCab, ENotaIngresoOPDetalle item)
        {
            item.niop_icod_nota_ingreso = objCab.niop_icod_nota_ingreso;
            item.intUsuario = objCab.intUsuario;
            for (int i = 0; i < 10; i++)
            {
                int index = i + 1;
                if (Convert.ToInt32(CommonFunctions.GetValueItem(item, $"niopd_kardex_{index}")) > 0)
                {
                    var producto = objCentralData.ProductoPvtGetByID(Convert.ToInt32(CommonFunctions.GetValueItem(item, $"niopd_iprod_{index}")));
                    EProdKardex objk = new EProdKardex();
                    objk.kardc_iid_correlativo = Convert.ToInt32(CommonFunctions.GetValueItem(item, $"niopd_kardex_{index}"));
                    objk.intUsuario = objCab.intUsuario;
                    objCentralData.EliminarKardexPvt(objk);
                    CommonFunctions.SetValueItem(item, $"niopd_kardex_{index}", 0);
                    EProdStockProducto objStock = new EProdStockProducto()
                    {
                        stocc_ianio = Parametros.intEjercicio,
                        stocc_iid_almacen = objCab.niop_ialmacen,
                        stocc_iid_producto = producto.pr_icod_producto,
                        stocc_nstock_prod = 0,
                        puvec_icod_punto_venta = 2,
                        vcodigo_externo = producto.pr_vcodigo_externo,
                    };
                    objCentralData.actualizarStockPvt(objStock);
                }
            }
            
            objCentral.NotaIngresoOpDetalleElimiar(item);
            objCentral.OrdenPedidoDetSaldoActualizar(item.orped_icod_item_orden_pedido);
        }

        public void NotaIngresoOpModificar(ENotaIngresoOP objCab, List<ENotaIngresoOPDetalle> listDetalle, List<ENotaIngresoOPDetalle> listEliminar)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.NotaIngresoOpModificar(objCab);
                    foreach (var item in listDetalle)
                    {
                        if (item.niopd_icod_nota_ingreso_detalle == 0)
                        {
                            NotaIngresoOpDetalleInsertar(objCab, item);
                        }
                        if (item.niopd_icod_nota_ingreso_detalle != 0 && item.Operacion == Operacion.Modificar)
                        {
                            NotaIngresoOpDetalleModificar(objCab, item);
                        }
                    }

                    foreach (var item in listEliminar)
                    {
                        NotaIngresoOpDetalleEliminar(objCab, item);
                    }
                    tx.Complete();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void NotaIngresoOpEliminar(ENotaIngresoOP objCab)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.NotaIngresoOpEliminar(objCab);

                    var listEliminar = objCentralData.NotaIngresoOpDetalleListar(objCab.niop_icod_nota_ingreso);
                    foreach (var item in listEliminar)
                    {
                        NotaIngresoOpDetalleEliminar(objCab, item);
                    }
                    tx.Complete();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int NotaIngresoOpInsertar(ENotaIngresoOP objCab, List<ENotaIngresoOPDetalle> listDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCab.niop_icod_nota_ingreso = objCentralData.NotaIngresoOpInsertar(objCab);
                    foreach (var item in listDetalle)
                    {
                        NotaIngresoOpDetalleInsertar(objCab, item);
                    }
                    tx.Complete();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return objCab.niop_icod_nota_ingreso;
        }

        public void InsertarProdProductos(EProdProducto objProductos)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.InsertarProdProductos(objProductos);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertMasivo(DataTable tbl, string procedure)
        {
            try
            {
                using (var conn = new SqlConnection(Helper.conexion()))
                {
                    using (var cmd = new SqlCommand(procedure, conn))
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

        public void ActualizarProdProductos(EProdProducto objEProdProducto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ActualizarProdProductos(objEProdProducto);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EDestinos> DestinosListar() => objCentral.DestinosListar();

        public int ProductoPvtGetMaxID()
        {
            return objCentral.ProductoPvtGetMaxID();
        }

        public EProdModelo ModeloGetById(int mo_icod_modelo)
        {
            return objCentral.ModeloGetById(mo_icod_modelo);
        }

        public void EliminarProdProductos(EProdProducto objEProdProducto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarProdProductos(objEProdProducto);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EProdProducto> VerificarProdProducto(string code)
        {
            List<EProdProducto> lista = null;
            try
            {
                lista = objCentral.VerificarProdProducto(code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int VerificarProdProductoEnkardex(int Kardc_iid_producto)
        {
            int cantidad = 0;
            try
            {
                cantidad = objCentral.VerificarProdProductoEnkardex(Kardc_iid_producto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cantidad;
        }
        #endregion
        #region Almacen

        public List<EProdAlmacen> ListarProdAlmacen()
        {
            List<EProdAlmacen> lista = null;
            try
            {
                lista = objCentral.ListarProdAlmacen();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public EProdProducto ProductoPvtGetByID(int pr_icod_producto)
        {
            return objCentral.ProductoPvtGetByID(pr_icod_producto);
        }

        public int InsertarProdAlmacen(EProdAlmacen objEAlmacen)
        {
            int blnResultado = 0;
            int intIdAlmacen = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //1. Registramos Almacen y obtenemos el id generado
                    intIdAlmacen = objCentral.InsertarProdAlmacen(objEAlmacen);

                    if (intIdAlmacen <= 0)
                    {
                        throw new ApplicationException("Almacen no pudo ser registrado");
                    }
                    blnResultado = 1;
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return blnResultado;
        }

        public int ActualizarProdAlmacen(EProdAlmacen objEAlmacen)
        {
            int blnResultado = 0;
            int intIdAlmacen = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //1. Actualizamos Almacen
                    intIdAlmacen = objCentral.ActualizarProdAlmacen(objEAlmacen);
                    if (intIdAlmacen <= 0)
                    {
                        throw new ApplicationException("Almacen no pudo ser actualizado");
                    }
                    blnResultado = 1;
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return blnResultado;
        }

        public int EliminarProdAlmacen(int intIdAlmacen)
        {
            int blnResultado = 0;
            int intId = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //1. Actualizamos Almacen
                    intId = objCentral.EliminarProdAlmacen(intIdAlmacen);
                    if (intId <= 0)
                    {
                        throw new ApplicationException("Almacen no pudo ser eliminado");
                    }
                    blnResultado = 1;
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return blnResultado;
        }

        #endregion
        #region Modelo


        public List<EProdModelo> ListarProdModeloTodo()
        {
            List<EProdModelo> lista = null;
            try
            {
                lista = objCentral.ListarProdModeloTodo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProdModelo> ListarProdModelo(EProdModelo obj)
        {
            List<EProdModelo> lista = null;
            try
            {
                lista = objCentral.ListarProdModelo(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarProdModelo(EProdModelo objModelo)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    int icod = objCentral.InsertarProdModelo(objModelo);
                    tx.Complete();
                    if (objModelo.image != null)
                    {
                        WebDavTest.Put($"{Rutas.RutaImagenes}{Rutas.RutaModelos}", $"{icod}.png", objModelo.image);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarProdModelo(EProdModelo objEModelo)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ActualizarProdModelo(objEModelo);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarProdModelo(EProdModelo objEModelo)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarProdModelo(objEModelo);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region UnidadMedida
        public List<EProdUnidadMedida> ListarProdUnidadMedida()
        {
            List<EProdUnidadMedida> lista = null;
            try
            {
                lista = objCentral.ListarProdUnidadMedida();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int InsertarProdUnidadMedida(EProdUnidadMedida objEUnidadMedida)
        {
            int blnResultado = 0;
            int intIdUnidadMedida = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIdUnidadMedida = objCentral.InsertarProdUnidadMedida(objEUnidadMedida);

                    if (intIdUnidadMedida <= 0)
                    {
                        throw new ApplicationException("UnidadMedida no pudo ser registrado");
                    }
                    blnResultado = 1;
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return blnResultado;
        }

        public int ActualizarProdUnidadMedida(EProdUnidadMedida objEUnidadMedida)
        {
            int blnResultado = 0;
            int intIdUnidadMedida = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    intIdUnidadMedida = objCentral.ActualizarProdUnidadMedida(objEUnidadMedida);
                    if (intIdUnidadMedida <= 0)
                    {
                        throw new ApplicationException("UnidadMedida no pudo ser actualizado");
                    }
                    blnResultado = 1;
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return blnResultado;
        }

        public int EliminarProdUnidadMedida(int intIdUnidadMedida)
        {
            int blnResultado = 0;
            int intId = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intId = objCentral.EliminarProdUnidadMedida(intIdUnidadMedida);
                    if (intId <= 0)
                    {
                        throw new ApplicationException("UnidadMedida no pudo ser eliminado");
                    }
                    blnResultado = 1;
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return blnResultado;
        }
        #endregion
        #region Tabla Registro

        public List<EProdTabla> listarProdTabla()
        {
            List<EProdTabla> lista = null;
            try
            {
                lista = objCentral.listarProdTabla();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarProdTabla(EProdTabla oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objCentral.insertarProdTabla(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarProdTabla(EProdTabla oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.modificarProdTabla(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarProdTabla(EProdTabla oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.eliminarProdTabla(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EProdTablaRegistro> ListarProdTablaRegistroTodo()
        {
            List<EProdTablaRegistro> lista = null;
            try
            {
                lista = objCentral.ListarProdTablaRegistroTodo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProdTablaRegistro> ListarProdTablaRegistro(EProdTablaRegistro obj)
        {
            List<EProdTablaRegistro> lista = null;
            try
            {
                lista = objCentral.ListarProdTablaRegistro(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarProdTablaRegistro(EProdTablaRegistro obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.InsertarProdTablaRegistro(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ModificarProdTablaRegistro(EProdTablaRegistro obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ActualizarProdTablaRegistro(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarProdTablaRegistro(EProdTablaRegistro obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarProdTablaRegistro(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public List<EProdTablaRegistro> ListarProdMotivoXTipo(int tablc_iid_tipo_tabla)
        {
            List<EProdTablaRegistro> lista = null;
            try
            {
                lista = objCentral.ListarProdMotivoXTipo(tablc_iid_tipo_tabla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProdTablaRegistro> ListarProdTablaRegistroFiltros(EProdTablaRegistro obj)
        {
            List<EProdTablaRegistro> lista = null;
            try
            {
                lista = objCentral.ListarProdTablaRegistro(obj);
                EProdTablaRegistro entidad = new EProdTablaRegistro();
                entidad.tarec_viid_correlativo = "00";
                entidad.descripcion = "Todos";
                lista.Insert(0, entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion
        #region Area Produccion
        public List<EAreaProduccion> listarAreaProduccion()
        {
            List<EAreaProduccion> lista = null;
            try
            {
                lista = objCentral.listarAreaProduccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarAreaProduccion(EAreaProduccion oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objCentral.insertarAreaProduccion(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarAreaProduccion(EAreaProduccion oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.modificarAreaProduccion(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarAreaProduccion(EAreaProduccion oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.eliminarAreaProduccion(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Area Ubicacion
        public List<EAreaUbicacion> ListarAreaUbicacion(EAreaUbicacion obj)
        {
            List<EAreaUbicacion> lista = null;
            try
            {
                lista = objCentral.ListarAreaUbicacion(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarAreaUbicacion(EAreaUbicacion obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.InsertarAreaUbicacion(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ModificarAreaUbicacion(EAreaUbicacion obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ActualizarAreaUbicacion(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarAreaUbicacion(EAreaUbicacion obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarAreaUbicacion(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Area Empresa
        public List<EAreaEmpresa> listarAreaEmpresa()
        {
            List<EAreaEmpresa> lista = null;
            try
            {
                lista = objCentral.listarAreaEmpresa();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarAreaEmpresa(EAreaEmpresa oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objCentral.insertarAreaEmpresa(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarAreaEmpresa(EAreaEmpresa oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.modificarAreaEmpresa(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarAreaEmpresa(EAreaEmpresa oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.eliminarAreaEmpresa(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Sub Area Empresa
        public List<ESubAreaEmpresa> ListarSubAreaEmpresa(ESubAreaEmpresa obj)
        {
            List<ESubAreaEmpresa> lista = null;
            try
            {
                lista = objCentral.ListarSubAreaEmpresa(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarSubAreaEmpresa(ESubAreaEmpresa obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.InsertarSubAreaEmpresa(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ModificarSubAreaEmpresa(ESubAreaEmpresa obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ActualizarSubAreaEmpresa(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarSubAreaEmpresa(ESubAreaEmpresa obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarSubAreaEmpresa(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Area Descripcion
        public List<EAreaDescripcion> ListarAreaDescripcion(EAreaDescripcion obj)
        {
            List<EAreaDescripcion> lista = null;
            try
            {
                lista = objCentral.ListarAreaDescripcion(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarAreaDescripcion(EAreaDescripcion obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.InsertarAreaDescripcion(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ModificarAreaDescripcion(EAreaDescripcion obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ActualizarAreaDescripcion(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarAreaDescripcion(EAreaDescripcion obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarAreaDescripcion(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Nota Ingreso
        public void EliminarProdNotaIngresoAlmacen(int Code)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarProdNotaIngresoAlmacen(Code);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<EProdNotaIngreso> ListarProdNotaIngresoXfechaAlmacen(EProdReporte obj)
        {
            List<EProdNotaIngreso> lista = null;
            try
            {
                lista = objCentral.ListarProdNotaIngresoXfechaAlmacen(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProdNotaIngreso> ListarProdNotaIngreso(int intfechainicio)
        {
            List<EProdNotaIngreso> lista = null;
            try
            {
                lista = objCentral.ListarProdNotaIngreso(intfechainicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        private void InsertarNotaIngressoMasivo(EProdNotaIngreso objNotaIngreso, List<EProdNotaIngresoDetalle> mlist)
        {

            try
            {
                int index = 0;
                int UltimoCorrelativoKardex = centralService.UltimoCorrelativoTabla("SGE_PVT_KARDEX");
                List<EProdKardex> lstProdKardex = new List<EProdKardex>();
                List<EProdStockProducto> lstStockProducto = new List<EProdStockProducto>();
                foreach (var obj in mlist)
                {
                    index++;
                    object[] Tallas = {
                        obj.ningcd_talla1, obj.ningcd_talla2, obj.ningcd_talla3, obj.ningcd_talla4,
                        obj.ningcd_talla5, obj.ningcd_talla6, obj.ningcd_talla7, obj.ningcd_talla8,
                        obj.ningcd_talla9, obj.ningcd_talla10
                        };

                    object[] Cantidades = {
                        obj.ningcd_cant_talla1, obj.ningcd_cant_talla2, obj.ningcd_cant_talla3, obj.ningcd_cant_talla4,
                        obj.ningcd_cant_talla5, obj.ningcd_cant_talla6, obj.ningcd_cant_talla7, obj.ningcd_cant_talla8,
                        obj.ningcd_cant_talla9, obj.ningcd_cant_talla10
                        };

                    object[] idkardex = new object[10];

                    for (int i = 0; i <= 9; i++)
                    {
                        if (Convert.ToInt32(Cantidades[i]) != 0)
                        {
                            string codigoexterno = obj.pr_vcodigo_externo + Tallas[i];
                            var oProducto = new BCentral().VerificarProdProducto(codigoexterno);
                            if (oProducto.Any())
                            {
                                EProdKardex objk = new EProdKardex();
                                objk.kardc_ianio = objNotaIngreso.ningc_sfecha_nota_ingreso.Value.Year;
                                objk.kardx_sfecha = Convert.ToDateTime(objNotaIngreso.ningc_sfecha_nota_ingreso);
                                objk.iid_almacen = Convert.ToInt32(objNotaIngreso.ningc_iid_almacen);
                                objk.iid_producto = oProducto[0].pr_icod_producto;
                                objk.puvec_icod_punto_venta = objNotaIngreso.puvec_icod_punto_venta;
                                objk.Cantidad = Convert.ToDecimal(Cantidades[i]);
                                objk.NroNota = Convert.ToInt32(objNotaIngreso.ningc_inumero_nota_ingreso);
                                objk.iid_tipo_doc = Convert.ToInt32(objNotaIngreso.ningc_iid_tipo_doc);
                                objk.NroDocumento = objNotaIngreso.ningc_inumero_doc;
                                objk.movimiento = 1;
                                objk.Motivo = Convert.ToInt32(objNotaIngreso.tablc_iid_motivo);
                                objk.Beneficiario = objNotaIngreso.ningc_vreferencia;
                                objk.Observaciones = objNotaIngreso.ningc_vobservaciones;
                                objk.intUsuario = obj.intUsuario;
                                objk.Item = Convert.ToInt32(obj.ningcd_num_item);
                                objk.stocc_codigo_producto = codigoexterno;
                                objk.operacion = 1;
                                objk.kardc_iid_correlativo = ++UltimoCorrelativoKardex;
                                idkardex[i] = objk.kardc_iid_correlativo;
                                lstProdKardex.Add(objk);
                                EProdStockProducto objStock = new EProdStockProducto()
                                {
                                    stocc_ianio = Parametros.intEjercicio,
                                    stocc_iid_almacen = objk.iid_almacen,
                                    stocc_iid_producto = objk.iid_producto,
                                    stocc_nstock_prod = objk.Cantidad,
                                    intTipoMovimiento = objk.movimiento,
                                    puvec_icod_punto_venta = objNotaIngreso.puvec_icod_punto_venta,
                                    vcodigo_externo = obj.pr_vcodigo_externo,
                                };
                                lstStockProducto.Add(objStock);
                            }
                            //objCentralData.actualizarStockPvt(objStock);
                        }

                    }
                    obj.ningcd_iid_kardex1 = Convert.ToInt64(idkardex[0]);
                    obj.ningcd_iid_kardex2 = Convert.ToInt64(idkardex[1]);
                    obj.ningcd_iid_kardex3 = Convert.ToInt64(idkardex[2]);
                    obj.ningcd_iid_kardex4 = Convert.ToInt64(idkardex[3]);
                    obj.ningcd_iid_kardex5 = Convert.ToInt64(idkardex[4]);
                    obj.ningcd_iid_kardex6 = Convert.ToInt64(idkardex[5]);
                    obj.ningcd_iid_kardex7 = Convert.ToInt64(idkardex[6]);
                    obj.ningcd_iid_kardex8 = Convert.ToInt64(idkardex[7]);
                    obj.ningcd_iid_kardex9 = Convert.ToInt64(idkardex[8]);
                    obj.ningcd_iid_kardex10 = Convert.ToInt64(idkardex[9]);

                    obj.ningc_icod_nota_ingreso = objNotaIngreso.ningc_icod_nota_ingreso;
                    //objCentralData.InsertarProdNotaIngresoDetalle(obj);

                }

                #region Insert masivo Detalle
                var dataTable = new DataTable();
                dataTable.Columns.Add("ningc_icod_nota_ingreso");
                dataTable.Columns.Add("ningc_iid_almacen");
                dataTable.Columns.Add("ningcd_num_item");
                dataTable.Columns.Add("tablc_iid_motivo");
                dataTable.Columns.Add("pr_vcodigo_externo", typeof(string));
                dataTable.Columns.Add("pr_vdescripcion_producto", typeof(string));
                dataTable.Columns.Add("pr_ncant_total_producto", typeof(decimal));
                dataTable.Columns.Add("ningcd_rango_talla", typeof(string));
                dataTable.Columns.Add("ningcd_iid_moneda");
                dataTable.Columns.Add("ningcd_monto_unit", typeof(decimal));
                dataTable.Columns.Add("ningcd_talla1");
                dataTable.Columns.Add("ningcd_talla2");
                dataTable.Columns.Add("ningcd_talla3");
                dataTable.Columns.Add("ningcd_talla4");
                dataTable.Columns.Add("ningcd_talla5");
                dataTable.Columns.Add("ningcd_talla6");
                dataTable.Columns.Add("ningcd_talla7");
                dataTable.Columns.Add("ningcd_talla8");
                dataTable.Columns.Add("ningcd_talla9");
                dataTable.Columns.Add("ningcd_talla10");
                dataTable.Columns.Add("ningcd_cant_talla1");
                dataTable.Columns.Add("ningcd_cant_talla2");
                dataTable.Columns.Add("ningcd_cant_talla3");
                dataTable.Columns.Add("ningcd_cant_talla4");
                dataTable.Columns.Add("ningcd_cant_talla5");
                dataTable.Columns.Add("ningcd_cant_talla6");
                dataTable.Columns.Add("ningcd_cant_talla7");
                dataTable.Columns.Add("ningcd_cant_talla8");
                dataTable.Columns.Add("ningcd_cant_talla9");
                dataTable.Columns.Add("ningcd_cant_talla10");
                dataTable.Columns.Add("ningcd_iid_kardex1", typeof(long));
                dataTable.Columns.Add("ningcd_iid_kardex2", typeof(long));
                dataTable.Columns.Add("ningcd_iid_kardex3", typeof(long));
                dataTable.Columns.Add("ningcd_iid_kardex4", typeof(long));
                dataTable.Columns.Add("ningcd_iid_kardex5", typeof(long));
                dataTable.Columns.Add("ningcd_iid_kardex6", typeof(long));
                dataTable.Columns.Add("ningcd_iid_kardex7", typeof(long));
                dataTable.Columns.Add("ningcd_iid_kardex8", typeof(long));
                dataTable.Columns.Add("ningcd_iid_kardex9", typeof(long));
                dataTable.Columns.Add("ningcd_iid_kardex10", typeof(long));
                dataTable.Columns.Add("ningcd_iusuario_crea");

                foreach (var detalle in mlist)
                {
                    dataTable.Rows.Add(
                        detalle.ningc_icod_nota_ingreso,
                        detalle.ningc_iid_almacen,
                        detalle.ningcd_num_item,
                        detalle.tablc_iid_motivo,
                        detalle.pr_vcodigo_externo,
                        detalle.pr_vdescripcion_producto,
                        detalle.pr_ncant_total_producto,
                        detalle.ningcd_rango_talla,
                        detalle.ningcd_iid_moneda,
                        detalle.ningcd_monto_unit,
                        detalle.ningcd_talla1,
                        detalle.ningcd_talla2,
                        detalle.ningcd_talla3,
                        detalle.ningcd_talla4,
                        detalle.ningcd_talla5,
                        detalle.ningcd_talla6,
                        detalle.ningcd_talla7,
                        detalle.ningcd_talla8,
                        detalle.ningcd_talla9,
                        detalle.ningcd_talla10,
                        detalle.ningcd_cant_talla1,
                        detalle.ningcd_cant_talla2,
                        detalle.ningcd_cant_talla3,
                        detalle.ningcd_cant_talla4,
                        detalle.ningcd_cant_talla5,
                        detalle.ningcd_cant_talla6,
                        detalle.ningcd_cant_talla7,
                        detalle.ningcd_cant_talla8,
                        detalle.ningcd_cant_talla9,
                        detalle.ningcd_cant_talla10,
                        detalle.ningcd_iid_kardex1,
                        detalle.ningcd_iid_kardex2,
                        detalle.ningcd_iid_kardex3,
                        detalle.ningcd_iid_kardex4,
                        detalle.ningcd_iid_kardex5,
                        detalle.ningcd_iid_kardex6,
                        detalle.ningcd_iid_kardex7,
                        detalle.ningcd_iid_kardex8,
                        detalle.ningcd_iid_kardex9,
                        detalle.ningcd_iid_kardex10,
                        detalle.intUsuario
                    );
                }


                #endregion

                #region insert masivo kardex
                var dataTableKardex = new DataTable();
                dataTableKardex.Columns.Add("kardc_iid_correlativo");
                dataTableKardex.Columns.Add("kardc_ianio");
                dataTableKardex.Columns.Add("kardc_fecha_movimiento", typeof(DateTime));
                dataTableKardex.Columns.Add("kardc_iid_almacen");
                dataTableKardex.Columns.Add("kardc_iid_producto");
                dataTableKardex.Columns.Add("puvec_icod_punto_venta");
                dataTableKardex.Columns.Add("kardc_icantidad_prod", typeof(decimal));
                dataTableKardex.Columns.Add("kardc_inumero_nota");
                dataTableKardex.Columns.Add("kardc_iid_tipo_doc");
                dataTableKardex.Columns.Add("kardc_inumero_doc", typeof(string));
                dataTableKardex.Columns.Add("kardc_itipo_movimiento");
                dataTableKardex.Columns.Add("kardc_iid_motivo");
                dataTableKardex.Columns.Add("kardc_vbeneficiario", typeof(string));
                dataTableKardex.Columns.Add("kardc_vobservaciones", typeof(string));
                dataTableKardex.Columns.Add("intUsuario");

                foreach (var kardex in lstProdKardex)
                {
                    dataTableKardex.Rows.Add(
                        kardex.kardc_iid_correlativo,
                        kardex.kardc_ianio,
                        kardex.kardx_sfecha,
                        kardex.iid_almacen,
                        kardex.iid_producto,
                        kardex.puvec_icod_punto_venta,
                        kardex.Cantidad,
                        kardex.NroNota,
                        kardex.iid_tipo_doc,
                        kardex.NroDocumento,
                        kardex.movimiento,
                        kardex.Motivo,
                        kardex.Beneficiario,
                        kardex.Observaciones,
                        kardex.intUsuario
                    );
                }

                #endregion


                int indexStok = 0;
                new CentralData().InsertMasivo(dataTable, "SGE_PVT_NOTA_INGRESO_DETALLE_INSERTAR_MASIVO");
                new CentralData().InsertMasivo(dataTableKardex, "SGEA_KARDEX_PVT_INSERTAR_MASIVO");
                var produc = lstStockProducto
                    .GroupBy(x => new { x.stocc_ianio, x.stocc_iid_almacen, x.stocc_iid_producto })
                    .Select(gr => gr.First()).ToList();
                foreach (var item in produc)
                {
                    indexStok++;
                    new CentralData().actualizarStockPvt(new EProdStockProducto() { stocc_ianio = item.stocc_ianio, stocc_iid_almacen = item.stocc_iid_almacen, stocc_iid_producto = item.stocc_iid_producto, stocc_nstock_prod = 0 });
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int InsertarProdNotaIngreso(EProdNotaIngreso objNotaIngreso, List<EProdNotaIngresoDetalle> mlist)
        {
            try
            {
                objNotaIngreso.ningc_icod_nota_ingreso = objCentral.InsertarProdNotaIngreso(objNotaIngreso);
                InsertarNotaIngressoMasivo(objNotaIngreso, mlist);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objNotaIngreso.ningc_icod_nota_ingreso;
        }
        public void ActualizarProdNotaIngresoo(EProdNotaIngreso objNotaIngreso, List<EProdNotaIngresoDetalle> mlist, List<EProdNotaIngresoDetalle> mlisteliminado)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { Timeout = TimeSpan.FromMinutes(20) }))
                {
                    objCentral.ActualizarProdNotaIngreso(objNotaIngreso);
                    InsertarNotaIngressoMasivo(objNotaIngreso, mlist.Where(x => x.ningcd_icod_nota_ingreso == 0).ToList());
                    foreach (var obj in mlist.Where(x => x.operacion == 2 && x.ningcd_icod_nota_ingreso != 0).ToList())
                    {
                        if (obj.operacion == 2)
                        {
                            List<EProdProducto> oProducto = new List<EProdProducto>();
                            object[] Tallas = new object[10];
                            object[] Cantidades = new object[10];
                            object[] idkardex = new object[10];

                            Tallas[0] = obj.ningcd_talla1;
                            Tallas[1] = obj.ningcd_talla2;
                            Tallas[2] = obj.ningcd_talla3;
                            Tallas[3] = obj.ningcd_talla4;
                            Tallas[4] = obj.ningcd_talla5;
                            Tallas[5] = obj.ningcd_talla6;
                            Tallas[6] = obj.ningcd_talla7;
                            Tallas[7] = obj.ningcd_talla8;
                            Tallas[8] = obj.ningcd_talla9;
                            Tallas[9] = obj.ningcd_talla10;
                            Cantidades[0] = obj.ningcd_cant_talla1;
                            Cantidades[1] = obj.ningcd_cant_talla2;
                            Cantidades[2] = obj.ningcd_cant_talla3;
                            Cantidades[3] = obj.ningcd_cant_talla4;
                            Cantidades[4] = obj.ningcd_cant_talla5;
                            Cantidades[5] = obj.ningcd_cant_talla6;
                            Cantidades[6] = obj.ningcd_cant_talla7;
                            Cantidades[7] = obj.ningcd_cant_talla8;
                            Cantidades[8] = obj.ningcd_cant_talla9;
                            Cantidades[9] = obj.ningcd_cant_talla10;

                            idkardex[0] = obj.ningcd_iid_kardex1;
                            idkardex[1] = obj.ningcd_iid_kardex2;
                            idkardex[2] = obj.ningcd_iid_kardex3;
                            idkardex[3] = obj.ningcd_iid_kardex4;
                            idkardex[4] = obj.ningcd_iid_kardex5;
                            idkardex[5] = obj.ningcd_iid_kardex6;
                            idkardex[6] = obj.ningcd_iid_kardex7;
                            idkardex[7] = obj.ningcd_iid_kardex8;
                            idkardex[8] = obj.ningcd_iid_kardex9;
                            idkardex[9] = obj.ningcd_iid_kardex10;
                            for (int i = 0; i <= 9; i++)
                            {
                                string codigoexterno = obj.pr_vcodigo_externo + Tallas[i];
                                oProducto = new BCentral().VerificarProdProducto(codigoexterno);
                                if (oProducto.Count > 0)
                                {
                                    EProdKardex objk = new EProdKardex();
                                    objk.kardc_ianio = Parametros.intEjercicio;
                                    objk.kardx_sfecha = DateTime.Now;
                                    objk.iid_almacen = Convert.ToInt32(objNotaIngreso.ningc_iid_almacen);
                                    objk.iid_producto = oProducto[0].pr_icod_producto;
                                    objk.puvec_icod_punto_venta = objNotaIngreso.puvec_icod_punto_venta;
                                    objk.Cantidad = Convert.ToDecimal(Cantidades[i]);
                                    objk.NroNota = Convert.ToInt32(objNotaIngreso.ningc_inumero_nota_ingreso);
                                    objk.iid_tipo_doc = Convert.ToInt32(objNotaIngreso.ningc_iid_tipo_doc);
                                    objk.NroDocumento = objNotaIngreso.ningc_inumero_doc;
                                    objk.movimiento = 1;
                                    objk.Motivo = Convert.ToInt32(objNotaIngreso.tablc_iid_motivo);
                                    objk.Beneficiario = objNotaIngreso.ningc_vreferencia;
                                    objk.Observaciones = objNotaIngreso.ningc_vobservaciones;
                                    objk.intUsuario = obj.intUsuario;
                                    objk.Item = Convert.ToInt32(obj.ningcd_num_item);
                                    objk.stocc_codigo_producto = codigoexterno;
                                    objk.kardc_iid_correlativo = Convert.ToInt32(idkardex[i]);
                                    if (objk.kardc_iid_correlativo > 0)
                                        if (objk.Cantidad == 0)
                                            objCentralData.EliminarKardexPvt(objk);
                                        else
                                            objCentralData.modificarKardexPvt(objk);
                                    else if (objk.Cantidad > 0)
                                        idkardex[i] = objCentralData.InsertarKardexpvt(objk);
                                    EProdStockProducto objStock = new EProdStockProducto()
                                    {
                                        stocc_ianio = Parametros.intEjercicio,
                                        stocc_iid_almacen = objk.iid_almacen,
                                        stocc_iid_producto = objk.iid_producto,
                                        stocc_nstock_prod = objk.Cantidad,
                                        intTipoMovimiento = objk.movimiento,
                                        puvec_icod_punto_venta = objNotaIngreso.puvec_icod_punto_venta,
                                        vcodigo_externo = codigoexterno,
                                    };
                                    objCentralData.actualizarStockPvt(objStock);
                                }
                            }
                            obj.ningcd_iid_kardex1 = Convert.ToInt64(idkardex[0]);
                            obj.ningcd_iid_kardex2 = Convert.ToInt64(idkardex[1]);
                            obj.ningcd_iid_kardex3 = Convert.ToInt64(idkardex[2]);
                            obj.ningcd_iid_kardex4 = Convert.ToInt64(idkardex[3]);
                            obj.ningcd_iid_kardex5 = Convert.ToInt64(idkardex[4]);
                            obj.ningcd_iid_kardex6 = Convert.ToInt64(idkardex[5]);
                            obj.ningcd_iid_kardex7 = Convert.ToInt64(idkardex[6]);
                            obj.ningcd_iid_kardex8 = Convert.ToInt64(idkardex[7]);
                            obj.ningcd_iid_kardex9 = Convert.ToInt64(idkardex[8]);
                            obj.ningcd_iid_kardex10 = Convert.ToInt64(idkardex[9]);
                            new BCentral().ActualizarProdNotaIngresoDetalle(obj);
                        }

                    }

                    foreach (var objelimi in mlisteliminado)
                    {

                        int[] icod_karde = new int[10];
                        long[] talla = new long[10];
                        icod_karde[0] = Convert.ToInt32(objelimi.ningcd_iid_kardex1);
                        icod_karde[1] = Convert.ToInt32(objelimi.ningcd_iid_kardex2);
                        icod_karde[2] = Convert.ToInt32(objelimi.ningcd_iid_kardex3);
                        icod_karde[3] = Convert.ToInt32(objelimi.ningcd_iid_kardex4);
                        icod_karde[4] = Convert.ToInt32(objelimi.ningcd_iid_kardex5);
                        icod_karde[5] = Convert.ToInt32(objelimi.ningcd_iid_kardex6);
                        icod_karde[6] = Convert.ToInt32(objelimi.ningcd_iid_kardex7);
                        icod_karde[7] = Convert.ToInt32(objelimi.ningcd_iid_kardex8);
                        icod_karde[8] = Convert.ToInt32(objelimi.ningcd_iid_kardex9);
                        icod_karde[9] = Convert.ToInt32(objelimi.ningcd_iid_kardex10);
                        talla[0] = Convert.ToInt32(objelimi.ningcd_talla1);
                        talla[1] = Convert.ToInt32(objelimi.ningcd_talla2);
                        talla[2] = Convert.ToInt32(objelimi.ningcd_talla3);
                        talla[3] = Convert.ToInt32(objelimi.ningcd_talla4);
                        talla[4] = Convert.ToInt32(objelimi.ningcd_talla5);
                        talla[5] = Convert.ToInt32(objelimi.ningcd_talla6);
                        talla[6] = Convert.ToInt32(objelimi.ningcd_talla7);
                        talla[7] = Convert.ToInt32(objelimi.ningcd_talla8);
                        talla[8] = Convert.ToInt32(objelimi.ningcd_talla9);
                        talla[9] = Convert.ToInt32(objelimi.ningcd_talla10);
                        for (int j = 0; j <= 9; j++)
                        {

                            string codigoexterno = objelimi.pr_vcodigo_externo + talla[j];
                            var oProducto = new BCentral().VerificarProdProducto(codigoexterno);
                            if (oProducto.Any())
                            {
                                EProdKardex ekar = new EProdKardex
                                {
                                    kardc_iid_correlativo = icod_karde[j],
                                    intUsuario = objNotaIngreso.intUsuario,
                                };
                                objCentral.EliminarKardexPvt(ekar);
                                EProdStockProducto objStock = new EProdStockProducto()
                                {
                                    stocc_ianio = Parametros.intEjercicio,
                                    stocc_iid_almacen = objelimi.ningc_iid_almacen.Value,
                                    stocc_iid_producto = oProducto[0].pr_icod_producto,
                                    stocc_nstock_prod = 0,

                                };
                                objCentral.actualizarStockPvt(objStock);
                            }
                        }
                        objCentral.EliminarProdNotaIngresoDetalle(objelimi);
                    }
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarProdNotaIngreso(EProdNotaIngreso objEProdNotaIngreso)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarProdNotaIngreso(objEProdNotaIngreso);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public int InsertarProdNotaIngresoAlmacen(EProdNotaIngresoAlmacen objEProdNotaIngreso, List<EProdNotaIngresoDetalleAlmacen> mlist)
        {
            int blnResultado = 0;
            int intIdNotaIngreso = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIdNotaIngreso = objCentral.InsertarProdNotaIngresoAlmacen(objEProdNotaIngreso, mlist);

                    blnResultado = 1;
                    tx.Complete();
                }
                return intIdNotaIngreso;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EProdNotaIngresoAlmacen> ListarProdNotaIngresoAlmacenFecha(EProdNotaIngresoAlmacen obj)
        {
            List<EProdNotaIngresoAlmacen> lista = null;
            try
            {
                lista = objCentral.ListarProdNotaIngresoAlmacenFecha(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProdNotaIngresoDetalleAlmacen> ListarProdGuiaIngresoReporte(EProdReporte obj)
        {
            List<EProdNotaIngresoDetalleAlmacen> lista = null;
            try
            {
                lista = objCentral.ListarProdGuiaIngresoReporte(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EProdNotaIngresoDetalleAlmacen> ListarProdNotaIngresoDetalleAlmacen(int Code)
        {
            List<EProdNotaIngresoDetalleAlmacen> lista = null;
            try
            {
                lista = objCentral.ListarProdNotaIngresoDetalleAlmacen(Code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProdNotaIngresoDetalle> ListarProdNotaIngresoDetalle(int Code)
        {
            List<EProdNotaIngresoDetalle> lista = null;
            try
            {
                lista = objCentral.ListarProdNotaIngresoDetalle(Code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarProdNotaIngresoDetalle(EProdNotaIngresoDetalle objNotaIngresoDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.InsertarProdNotaIngresoDetalle(objNotaIngresoDetalle);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarProdNotaIngresoDetalle(EProdNotaIngresoDetalle objEProdNotaIngresoDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ActualizarProdNotaIngresoDetalle(objEProdNotaIngresoDetalle);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarProdNotaIngresoDetalle(EProdNotaIngresoDetalle objEProdNotaIngresoDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarProdNotaIngresoDetalle(objEProdNotaIngresoDetalle);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EProdNotaIngresoDetalle> ListarProdNotaIngresoXfechaAlmacenDetalle(EProdReporte obj)
        {
            List<EProdNotaIngresoDetalle> lista = null;
            try
            {
                lista = objCentral.ListarProdNotaIngresoXfechaAlmacenDetalle(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        #endregion
        #region Nota Salida
        //public List<EProdNotaSalidaAlmacen> ListarProdGuiaSalidaReporte(EProdReporte obj)
        //{
        //    List<EProdNotaSalidaAlmacen> lista = null;
        //    try
        //    {
        //        lista = objCentral.ListarProdGuiaSalidaReporte(obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return lista;
        //}
        //public List<EProdNotaSalidaAlmacen> ListarGuiaSalidaAlmacenFecha(EProdNotaSalidaAlmacen obj)
        //{
        //    List<EProdNotaSalidaAlmacen> lista = null;
        //    try
        //    {
        //        lista = objCentral.ListarProdGuiaSalidaAlmacenFecha(obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return lista;
        //}
        public void EliminarProdNotaSalidaAlmacen(int Code)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarProdNotaSalidaAlmacen(Code);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //public List<EProdNotaSalidaAlmacen> ListarProdNotaSalidaAlmacenXIcod(int icodNotaSalida)
        //{
        //    List<EProdNotaSalidaAlmacen> lista = null;
        //    try
        //    {
        //        lista = objCentral.ListarProdNotaSalidaAlmacenXIcod(icodNotaSalida);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return lista;
        //}
        public int InsertarProdNotaSalidaAlmacen(EProdNotaSalidaAlmacen objEProdNotaSalida, List<EProdNotaSalidaDetalleAlmacen> mlist)
        {
            int blnResultado = 0;
            int intIdNotaSalida = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIdNotaSalida = objCentral.InsertarProdNotaSalidaAlmacen(objEProdNotaSalida, mlist);

                    blnResultado = 1;
                    tx.Complete();
                }
                return intIdNotaSalida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EProdNotaSalidaAlmacen> ListarProdNotaSalidaAlmacen()
        {
            List<EProdNotaSalidaAlmacen> lista = null;
            try
            {
                lista = objCentral.ListarProdNotaSalidaAlmacen();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProdNotaSalidaDetalle> ListarProdNotaSalidaXfechaAlmacenDetalle(EProdReporte ob)
        {
            List<EProdNotaSalidaDetalle> lista = null;
            try
            {
                lista = objCentral.ListarProdNotaSalidaXfechaAlmacenDetalle(ob);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProdNotaSalidaDetalle> ListarProdNotaSalidaReporte(int salgc_icod_nota_salida)
        {
            List<EProdNotaSalidaDetalle> lista = null;
            try
            {
                lista = objCentral.ListarProdNotaSalidaReporte(salgc_icod_nota_salida);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProdNotaSalida> ListarProdNotaSalidaXfechaAlmacen(EProdReporte ob)
        {
            List<EProdNotaSalida> lista = null;
            try
            {
                lista = objCentral.ListarProdNotaSalidaXfechaAlmacen(ob);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProdNotaSalida> ListarProdNotaSalida(int intfechainicio)
        {
            List<EProdNotaSalida> lista = null;
            try
            {
                lista = objCentral.ListarProdNotaSalida(intfechainicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int InsertarProdNotaSalida(EProdNotaSalida objNotaSalida, List<EProdNotaSalidaDetalle> mlistdetalle)
        {
            int Code = 0;
            int index = 0;
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    Timeout = TimeSpan.FromMinutes(20)
                }))
                {
                    Code = objCentral.InsertarProdNotaSalida(objNotaSalida);
                    foreach (var obj in mlistdetalle)
                    {
                        index++;
                        object[] Tallas = new object[10];
                        object[] Cantidades = new object[10];
                        object[] idkardex = new object[10];
                        object[] idproducto = new object[10];
                        Tallas[0] = obj.salgcd_talla1;
                        Tallas[1] = obj.salgcd_talla2;
                        Tallas[2] = obj.salgcd_talla3;
                        Tallas[3] = obj.salgcd_talla4;
                        Tallas[4] = obj.salgcd_talla5;
                        Tallas[5] = obj.salgcd_talla6;
                        Tallas[6] = obj.salgcd_talla7;
                        Tallas[7] = obj.salgcd_talla8;
                        Tallas[8] = obj.salgcd_talla9;
                        Tallas[9] = obj.salgcd_talla10;
                        Cantidades[0] = obj.salgcd_cant_talla1;
                        Cantidades[1] = obj.salgcd_cant_talla2;
                        Cantidades[2] = obj.salgcd_cant_talla3;
                        Cantidades[3] = obj.salgcd_cant_talla4;
                        Cantidades[4] = obj.salgcd_cant_talla5;
                        Cantidades[5] = obj.salgcd_cant_talla6;
                        Cantidades[6] = obj.salgcd_cant_talla7;
                        Cantidades[7] = obj.salgcd_cant_talla8;
                        Cantidades[8] = obj.salgcd_cant_talla9;
                        Cantidades[9] = obj.salgcd_cant_talla10;
                        idproducto[0] = obj.salgcd_icod_producto1;
                        idproducto[1] = obj.salgcd_icod_producto2;
                        idproducto[2] = obj.salgcd_icod_producto3;
                        idproducto[3] = obj.salgcd_icod_producto4;
                        idproducto[4] = obj.salgcd_icod_producto5;
                        idproducto[5] = obj.salgcd_icod_producto6;
                        idproducto[6] = obj.salgcd_icod_producto7;
                        idproducto[7] = obj.salgcd_icod_producto8;
                        idproducto[8] = obj.salgcd_icod_producto9;
                        idproducto[9] = obj.salgcd_icod_producto10;
                        for (int i = 0; i <= 9; i++)
                        {
                            if (Convert.ToInt32(Cantidades[i]) != 0)
                            {
                                EProdKardex objk = new EProdKardex();
                                objk.kardx_sfecha = DateTime.Now;
                                objk.kardc_ianio = Parametros.intEjercicio;
                                objk.iid_almacen = Convert.ToInt32(objNotaSalida.salgc_iid_almacen);
                                objk.iid_producto = Convert.ToInt32(idproducto[i]);
                                objk.puvec_icod_punto_venta = objNotaSalida.puvec_icod_punto_venta;
                                objk.Cantidad = Convert.ToDecimal(Cantidades[i]);
                                objk.NroNota = Convert.ToInt32(objNotaSalida.salgc_inumero_nota_salida);
                                objk.iid_tipo_doc = Convert.ToInt32(objNotaSalida.salgc_iid_tipo_doc);
                                objk.NroDocumento = objNotaSalida.salgc_inumero_doc;
                                objk.movimiento = 0;

                                objk.Motivo = Convert.ToInt32(objNotaSalida.tablc_iid_motivo);
                                objk.Beneficiario = objNotaSalida.salgc_vreferencia;
                                objk.Observaciones = objNotaSalida.salgc_vobservaciones;
                                objk.intUsuario = objNotaSalida.intUsuario;
                                objk.Item = Convert.ToInt32(obj.salgcd_num_item);
                                objk.stocc_codigo_producto = obj.pr_vcodigo_externo;
                                objk.operacion = 1;
                                objk.kardc_iid_correlativo = new CentralData().InsertarKardexpvt(objk);
                                idkardex[i] = objk.kardc_iid_correlativo;
                                idproducto[i] = objk.iid_producto;
                            }

                        }
                        obj.salgcd_iid_kardex1 = Convert.ToInt64(idkardex[0]);
                        obj.salgcd_iid_kardex2 = Convert.ToInt64(idkardex[1]);
                        obj.salgcd_iid_kardex3 = Convert.ToInt64(idkardex[2]);
                        obj.salgcd_iid_kardex4 = Convert.ToInt64(idkardex[3]);
                        obj.salgcd_iid_kardex5 = Convert.ToInt64(idkardex[4]);
                        obj.salgcd_iid_kardex6 = Convert.ToInt64(idkardex[5]);
                        obj.salgcd_iid_kardex7 = Convert.ToInt64(idkardex[6]);
                        obj.salgcd_iid_kardex8 = Convert.ToInt64(idkardex[7]);
                        obj.salgcd_iid_kardex9 = Convert.ToInt64(idkardex[8]);
                        obj.salgcd_iid_kardex10 = Convert.ToInt64(idkardex[9]);

                        obj.salgc_icod_nota_salida = Code;//codigo cabecera
                        new BCentral().InsertarProdNotaSalidaDetalle(obj);
                    }
                    tx.Complete();




                }
                mlistdetalle.ForEach(obj =>
                {
                    object[] Cantidades = new object[10];
                    object[] idproducto = new object[10];
                    Cantidades[0] = obj.salgcd_cant_talla1;
                    Cantidades[1] = obj.salgcd_cant_talla2;
                    Cantidades[2] = obj.salgcd_cant_talla3;
                    Cantidades[3] = obj.salgcd_cant_talla4;
                    Cantidades[4] = obj.salgcd_cant_talla5;
                    Cantidades[5] = obj.salgcd_cant_talla6;
                    Cantidades[6] = obj.salgcd_cant_talla7;
                    Cantidades[7] = obj.salgcd_cant_talla8;
                    Cantidades[8] = obj.salgcd_cant_talla9;
                    Cantidades[9] = obj.salgcd_cant_talla10;
                    idproducto[0] = obj.salgcd_icod_producto1;
                    idproducto[1] = obj.salgcd_icod_producto2;
                    idproducto[2] = obj.salgcd_icod_producto3;
                    idproducto[3] = obj.salgcd_icod_producto4;
                    idproducto[4] = obj.salgcd_icod_producto5;
                    idproducto[5] = obj.salgcd_icod_producto6;
                    idproducto[6] = obj.salgcd_icod_producto7;
                    idproducto[7] = obj.salgcd_icod_producto8;
                    idproducto[8] = obj.salgcd_icod_producto9;
                    idproducto[9] = obj.salgcd_icod_producto10;
                    for (int i = 0; i <= 9; i++)
                    {
                        if (Convert.ToInt32(Cantidades[i]) != 0)
                        {
                            EProdStockProducto objStock = new EProdStockProducto()
                            {
                                stocc_ianio = Parametros.intEjercicio,
                                stocc_iid_almacen = Convert.ToInt32(objNotaSalida.salgc_iid_almacen),
                                stocc_iid_producto = Convert.ToInt32(idproducto[i]),
                                stocc_nstock_prod = Convert.ToDecimal(Cantidades[i]),
                                intTipoMovimiento = 0,
                                puvec_icod_punto_venta = objNotaSalida.puvec_icod_punto_venta,
                                vcodigo_externo = obj.pr_vcodigo_externo,
                            };
                            objCentralData.actualizarStockPvt(objStock);
                        }

                    }

                });
            }
            catch (Exception ex)
            {
                index = index;
                throw ex;
            }
            return Code;

        }


        //Task InsertarDetNotaSalida() { 

        //}

        public void ActualizarReporteProduccionKardex(EOrdenProduccion objEReporteProduccion, EProdKardex objEKardex)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //long IdKardex = 0;
                    ////Insertar Kardex
                    //IdKardex = objAlmacenData.insertarKardex(objEKardex);
                    ////ACTUALIZAR STOCK ***falta 
                    //objAlmacenData.TRUEActualizarStockProductoCantidadkardex(Parametros.intEjercicio, Convert.ToInt32(objEKardex.almac_icod_almacen), Convert.ToInt32(objEKardex.prdc_icod_producto));

                    ////Actualiza el reporte de produccion con el kardex generado
                    //objEReporteProduccion.rp_id_kardex_producto_ingreso = IdKardex;
                    //objEReporteProduccion.rp_sfecha_ing_kardex = objEKardex.kardc_fecha_movimiento;
                    //objComprasData.ActualizarReporteProduccionKardex(objEReporteProduccion);

                    object[] Cantidades = new object[10];
                    object[] idkardex = new object[10];
                    object[] idproducto = new object[10];

                    Cantidades[0] = objEReporteProduccion.orprc_cant_talla1;
                    Cantidades[1] = objEReporteProduccion.orprc_cant_talla2;
                    Cantidades[2] = objEReporteProduccion.orprc_cant_talla3;
                    Cantidades[3] = objEReporteProduccion.orprc_cant_talla4;
                    Cantidades[4] = objEReporteProduccion.orprc_cant_talla5;
                    Cantidades[5] = objEReporteProduccion.orprc_cant_talla6;
                    Cantidades[6] = objEReporteProduccion.orprc_cant_talla7;
                    Cantidades[7] = objEReporteProduccion.orprc_cant_talla8;
                    Cantidades[8] = objEReporteProduccion.orprc_cant_talla9;
                    Cantidades[9] = objEReporteProduccion.orprc_cant_talla10;
                    idproducto[0] = objEReporteProduccion.orprc_icod_producto1;
                    idproducto[1] = objEReporteProduccion.orprc_icod_producto2;
                    idproducto[2] = objEReporteProduccion.orprc_icod_producto3;
                    idproducto[3] = objEReporteProduccion.orprc_icod_producto4;
                    idproducto[4] = objEReporteProduccion.orprc_icod_producto5;
                    idproducto[5] = objEReporteProduccion.orprc_icod_producto6;
                    idproducto[6] = objEReporteProduccion.orprc_icod_producto7;
                    idproducto[7] = objEReporteProduccion.orprc_icod_producto8;
                    idproducto[8] = objEReporteProduccion.orprc_icod_producto9;
                    idproducto[9] = objEReporteProduccion.orprc_icod_producto10;
                    for (int i = 0; i <= 9; i++)
                    {
                        if (Convert.ToInt32(Cantidades[i]) != 0)
                        {
                            objEKardex.iid_producto = Convert.ToInt32(idproducto[i]);
                            objEKardex.Cantidad = Convert.ToDecimal(Cantidades[i]);
                            objEKardex.kardc_iid_correlativo = new BCentral().InsertarKardexpvt(objEKardex);
                            idkardex[i] = objEKardex.kardc_iid_correlativo;
                            idproducto[i] = objEKardex.iid_producto;
                            EProdStockProducto objStock = new EProdStockProducto()
                            {
                                stocc_ianio = Parametros.intEjercicio,
                                stocc_iid_almacen = Convert.ToInt32(objEReporteProduccion.orprc_iid_almacen),
                                stocc_iid_producto = objEKardex.iid_producto,
                                stocc_nstock_prod = Convert.ToDecimal(Cantidades[i]),
                                intTipoMovimiento = objEKardex.movimiento,
                            };
                            objCentralData.actualizarStockPvt(objStock);
                        }

                    }
                    objEReporteProduccion.orprc_iid_kardex1 = Convert.ToInt64(idkardex[0]);
                    objEReporteProduccion.orprc_iid_kardex2 = Convert.ToInt64(idkardex[1]);
                    objEReporteProduccion.orprc_iid_kardex3 = Convert.ToInt64(idkardex[2]);
                    objEReporteProduccion.orprc_iid_kardex4 = Convert.ToInt64(idkardex[3]);
                    objEReporteProduccion.orprc_iid_kardex5 = Convert.ToInt64(idkardex[4]);
                    objEReporteProduccion.orprc_iid_kardex6 = Convert.ToInt64(idkardex[5]);
                    objEReporteProduccion.orprc_iid_kardex7 = Convert.ToInt64(idkardex[6]);
                    objEReporteProduccion.orprc_iid_kardex8 = Convert.ToInt64(idkardex[7]);
                    objEReporteProduccion.orprc_iid_kardex9 = Convert.ToInt64(idkardex[8]);
                    objEReporteProduccion.orprc_iid_kardex10 = Convert.ToInt64(idkardex[9]);

                    objEReporteProduccion.orprc_sfecha_ing_kardex = objEKardex.kardx_sfecha;
                    objCentralData.ActualizarPVTOrdenProduccionKardex(objEReporteProduccion);



                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void actualizarStockPvt(EProdStockProducto objStock)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.actualizarStockPvt(objStock);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EProdKardex> listarkardex(int intanio)
        {
            List<EProdKardex> lista = null;
            try
            {
                lista = objCentral.listarkardex(intanio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void actualizarStockAnio(EProdStockProducto objStock)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.actualizarStockPvt(objStock);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarProdNotaSalida(EProdNotaSalida objNotaSalida, List<EProdNotaSalidaDetalle> mlistDetalle, List<EProdNotaSalidaDetalle> mlistDetalleElimi)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ActualizarProdNotaSalida(objNotaSalida);
                    foreach (var obj in mlistDetalle)
                    {
                        if (obj.operacion == 1)
                        {
                            //insertar kardex
                            object[] Tallas = new object[10];
                            object[] Cantidades = new object[10];
                            object[] idkardex = new object[10];
                            object[] idProducto = new object[10];
                            Tallas[0] = obj.salgcd_talla1;
                            Tallas[1] = obj.salgcd_talla2;
                            Tallas[2] = obj.salgcd_talla3;
                            Tallas[3] = obj.salgcd_talla4;
                            Tallas[4] = obj.salgcd_talla5;
                            Tallas[5] = obj.salgcd_talla6;
                            Tallas[6] = obj.salgcd_talla7;
                            Tallas[7] = obj.salgcd_talla8;
                            Tallas[8] = obj.salgcd_talla9;
                            Tallas[9] = obj.salgcd_talla10;
                            Cantidades[0] = obj.salgcd_cant_talla1;
                            Cantidades[1] = obj.salgcd_cant_talla2;
                            Cantidades[2] = obj.salgcd_cant_talla3;
                            Cantidades[3] = obj.salgcd_cant_talla4;
                            Cantidades[4] = obj.salgcd_cant_talla5;
                            Cantidades[5] = obj.salgcd_cant_talla6;
                            Cantidades[6] = obj.salgcd_cant_talla7;
                            Cantidades[7] = obj.salgcd_cant_talla8;
                            Cantidades[8] = obj.salgcd_cant_talla9;
                            Cantidades[9] = obj.salgcd_cant_talla10;
                            for (int i = 0; i <= 9; i++)
                            {

                                if (Convert.ToInt32(Cantidades[i]) != 0)
                                {
                                    string codigoExterno = obj.pr_vcodigo_externo + Tallas[i];
                                    var oProducto = new BCentral().VerificarProdProducto(codigoExterno).SingleOrDefault();
                                    EProdKardex objk = new EProdKardex();
                                    objk.kardc_ianio = Parametros.intEjercicio;
                                    objk.kardx_sfecha = DateTime.Now;
                                    objk.iid_almacen = Convert.ToInt32(objNotaSalida.salgc_iid_almacen);
                                    objk.iid_producto = oProducto.pr_icod_producto;
                                    objk.puvec_icod_punto_venta = objNotaSalida.puvec_icod_punto_venta;
                                    objk.Cantidad = Convert.ToDecimal(Cantidades[i]);
                                    objk.NroNota = Convert.ToInt32(objNotaSalida.salgc_inumero_nota_salida);
                                    objk.iid_tipo_doc = Convert.ToInt32(objNotaSalida.salgc_iid_tipo_doc);
                                    objk.NroDocumento = objNotaSalida.salgc_inumero_doc;
                                    objk.movimiento = 0;
                                    objk.Motivo = Convert.ToInt32(objNotaSalida.tablc_iid_motivo);
                                    objk.Beneficiario = objNotaSalida.salgc_vreferencia;
                                    objk.Observaciones = objNotaSalida.salgc_vobservaciones;
                                    objk.intUsuario = objNotaSalida.intUsuario;
                                    objk.Item = Convert.ToInt32(obj.salgcd_num_item);
                                    objk.stocc_codigo_producto = obj.pr_vcodigo_externo;
                                    objk.operacion = 1;
                                    objk.kardc_iid_correlativo = new BCentral().InsertarKardexpvt(objk);
                                    idkardex[i] = objk.kardc_iid_correlativo;
                                    EProdStockProducto objStock = new EProdStockProducto()
                                    {
                                        stocc_ianio = Parametros.intEjercicio,
                                        stocc_iid_almacen = objk.iid_almacen,
                                        stocc_iid_producto = oProducto.pr_icod_producto,
                                        stocc_nstock_prod = objk.Cantidad,
                                        intTipoMovimiento = objk.movimiento,
                                        vcodigo_externo = codigoExterno,
                                        puvec_icod_punto_venta = objNotaSalida.puvec_icod_punto_venta
                                    };
                                    objCentralData.actualizarStockPvt(objStock);
                                }

                            }
                            obj.salgcd_iid_kardex1 = Convert.ToInt64(idkardex[0]);
                            obj.salgcd_iid_kardex2 = Convert.ToInt64(idkardex[1]);
                            obj.salgcd_iid_kardex3 = Convert.ToInt64(idkardex[2]);
                            obj.salgcd_iid_kardex4 = Convert.ToInt64(idkardex[3]);
                            obj.salgcd_iid_kardex5 = Convert.ToInt64(idkardex[4]);
                            obj.salgcd_iid_kardex6 = Convert.ToInt64(idkardex[5]);
                            obj.salgcd_iid_kardex7 = Convert.ToInt64(idkardex[6]);
                            obj.salgcd_iid_kardex8 = Convert.ToInt64(idkardex[7]);
                            obj.salgcd_iid_kardex9 = Convert.ToInt64(idkardex[8]);
                            obj.salgcd_iid_kardex10 = Convert.ToInt64(idkardex[9]);

                            obj.salgc_icod_nota_salida = objNotaSalida.salgc_icod_nota_salida;//codigo cabecera
                            new BCentral().InsertarProdNotaSalidaDetalle(obj);

                        }
                        else if (obj.operacion == 2)
                        {

                            object[] Tallas = new object[10];
                            object[] Cantidades = new object[10];
                            object[] idkardex = new object[10];
                            object[] idProducto = new object[10];

                            Tallas[0] = obj.salgcd_talla1;
                            Tallas[1] = obj.salgcd_talla2;
                            Tallas[2] = obj.salgcd_talla3;
                            Tallas[3] = obj.salgcd_talla4;
                            Tallas[4] = obj.salgcd_talla5;
                            Tallas[5] = obj.salgcd_talla6;
                            Tallas[6] = obj.salgcd_talla7;
                            Tallas[7] = obj.salgcd_talla8;
                            Tallas[8] = obj.salgcd_talla9;
                            Tallas[9] = obj.salgcd_talla10;
                            Cantidades[0] = obj.salgcd_cant_talla1;
                            Cantidades[1] = obj.salgcd_cant_talla2;
                            Cantidades[2] = obj.salgcd_cant_talla3;
                            Cantidades[3] = obj.salgcd_cant_talla4;
                            Cantidades[4] = obj.salgcd_cant_talla5;
                            Cantidades[5] = obj.salgcd_cant_talla6;
                            Cantidades[6] = obj.salgcd_cant_talla7;
                            Cantidades[7] = obj.salgcd_cant_talla8;
                            Cantidades[8] = obj.salgcd_cant_talla9;
                            Cantidades[9] = obj.salgcd_cant_talla10;
                            idkardex[0] = obj.salgcd_iid_kardex1;
                            idkardex[1] = obj.salgcd_iid_kardex2;
                            idkardex[2] = obj.salgcd_iid_kardex3;
                            idkardex[3] = obj.salgcd_iid_kardex4;
                            idkardex[4] = obj.salgcd_iid_kardex5;
                            idkardex[5] = obj.salgcd_iid_kardex6;
                            idkardex[6] = obj.salgcd_iid_kardex7;
                            idkardex[7] = obj.salgcd_iid_kardex8;
                            idkardex[8] = obj.salgcd_iid_kardex9;
                            idkardex[9] = obj.salgcd_iid_kardex10;
                            for (int i = 0; i <= 9; i++)
                            {

                                string codigoExterno = obj.pr_vcodigo_externo + Tallas[i];
                                var oProducto = new BCentral().VerificarProdProducto(codigoExterno).SingleOrDefault();
                                if (oProducto != null)
                                {
                                    EProdKardex objk = new EProdKardex();
                                    objk.kardc_iid_correlativo = Convert.ToInt32(idkardex[i]);
                                    objk.kardc_ianio = Parametros.intEjercicio;
                                    objk.kardx_sfecha = Convert.ToDateTime(objNotaSalida.salgc_sfecha_nota_salida);
                                    objk.iid_almacen = Convert.ToInt32(objNotaSalida.salgc_iid_almacen);
                                    objk.iid_producto = oProducto.pr_icod_producto;
                                    objk.puvec_icod_punto_venta = objNotaSalida.puvec_icod_punto_venta;
                                    objk.Cantidad = Convert.ToDecimal(Cantidades[i]);
                                    objk.NroNota = Convert.ToInt32(objNotaSalida.salgc_inumero_nota_salida);
                                    objk.iid_tipo_doc = Convert.ToInt32(objNotaSalida.salgc_iid_tipo_doc);
                                    objk.NroDocumento = objNotaSalida.salgc_inumero_doc;
                                    objk.movimiento = 0;
                                    objk.Motivo = Convert.ToInt32(objNotaSalida.tablc_iid_motivo);
                                    objk.Beneficiario = objNotaSalida.salgc_vreferencia;
                                    objk.Observaciones = objNotaSalida.salgc_vobservaciones;
                                    objk.intUsuario = objNotaSalida.intUsuario;
                                    objk.Item = Convert.ToInt32(obj.salgcd_num_item);
                                    objk.stocc_codigo_producto = obj.pr_vcodigo_externo;

                                    if (objk.kardc_iid_correlativo > 0)
                                        if (objk.Cantidad == 0)
                                            objCentralData.EliminarKardexPvt(objk);
                                        else
                                            objCentralData.modificarKardexPvt(objk);
                                    else if (objk.Cantidad > 0)
                                        idkardex[i] = objCentralData.InsertarKardexpvt(objk);

                                    EProdStockProducto objStock = new EProdStockProducto()
                                    {
                                        stocc_ianio = Parametros.intEjercicio,
                                        stocc_iid_almacen = objk.iid_almacen,
                                        stocc_iid_producto = objk.iid_producto,
                                        stocc_nstock_prod = objk.Cantidad,
                                        puvec_icod_punto_venta = objNotaSalida.puvec_icod_punto_venta,
                                        vcodigo_externo = obj.pr_vcodigo_externo,
                                    };
                                    objCentralData.actualizarStockPvt(objStock);
                                }



                            }
                            obj.salgcd_iid_kardex1 = Convert.ToInt64(idkardex[0]);
                            obj.salgcd_iid_kardex2 = Convert.ToInt64(idkardex[1]);
                            obj.salgcd_iid_kardex3 = Convert.ToInt64(idkardex[2]);
                            obj.salgcd_iid_kardex4 = Convert.ToInt64(idkardex[3]);
                            obj.salgcd_iid_kardex5 = Convert.ToInt64(idkardex[4]);
                            obj.salgcd_iid_kardex6 = Convert.ToInt64(idkardex[5]);
                            obj.salgcd_iid_kardex7 = Convert.ToInt64(idkardex[6]);
                            obj.salgcd_iid_kardex8 = Convert.ToInt64(idkardex[7]);
                            obj.salgcd_iid_kardex9 = Convert.ToInt64(idkardex[8]);
                            obj.salgcd_iid_kardex10 = Convert.ToInt64(idkardex[9]);
                            new BCentral().ActualizarProdNotaSalidaDetalle(obj);
                        }

                    }
                    foreach (var objelimi in mlistDetalleElimi)
                    {
                        EProdKardex ekar = new EProdKardex();
                        int[] icod_karde = new int[10];
                        long[] talla = new long[10];
                        object[] idProducto = new object[10];



                        icod_karde[0] = Convert.ToInt32(objelimi.salgcd_iid_kardex1);
                        icod_karde[1] = Convert.ToInt32(objelimi.salgcd_iid_kardex2);
                        icod_karde[2] = Convert.ToInt32(objelimi.salgcd_iid_kardex3);
                        icod_karde[3] = Convert.ToInt32(objelimi.salgcd_iid_kardex4);
                        icod_karde[4] = Convert.ToInt32(objelimi.salgcd_iid_kardex5);
                        icod_karde[5] = Convert.ToInt32(objelimi.salgcd_iid_kardex6);
                        icod_karde[6] = Convert.ToInt32(objelimi.salgcd_iid_kardex7);
                        icod_karde[7] = Convert.ToInt32(objelimi.salgcd_iid_kardex8);
                        icod_karde[8] = Convert.ToInt32(objelimi.salgcd_iid_kardex9);
                        icod_karde[9] = Convert.ToInt32(objelimi.salgcd_iid_kardex10);
                        talla[0] = Convert.ToInt32(objelimi.salgcd_talla1);
                        talla[1] = Convert.ToInt32(objelimi.salgcd_talla2);
                        talla[2] = Convert.ToInt32(objelimi.salgcd_talla3);
                        talla[3] = Convert.ToInt32(objelimi.salgcd_talla4);
                        talla[4] = Convert.ToInt32(objelimi.salgcd_talla5);
                        talla[5] = Convert.ToInt32(objelimi.salgcd_talla6);
                        talla[6] = Convert.ToInt32(objelimi.salgcd_talla7);
                        talla[7] = Convert.ToInt32(objelimi.salgcd_talla8);
                        talla[8] = Convert.ToInt32(objelimi.salgcd_talla9);
                        talla[9] = Convert.ToInt32(objelimi.salgcd_talla10);

                        for (int j = 0; j <= 9; j++)
                        {
                            string codigoexterno = objelimi.pr_vcodigo_externo + talla[j];
                            var oProducto = new BCentral().VerificarProdProducto(codigoexterno);
                            if (oProducto.Any())
                            {

                                ekar.kardc_iid_correlativo = icod_karde[j];
                                ekar.intUsuario = objNotaSalida.intUsuario;
                                new BCentral().EliminarKardexPvt(ekar);

                                EProdStockProducto objStock = new EProdStockProducto()
                                {
                                    stocc_ianio = Parametros.intEjercicio,
                                    stocc_iid_almacen = ekar.iid_almacen,
                                    stocc_iid_producto = Convert.ToInt32(oProducto[0].pr_icod_producto),
                                    stocc_nstock_prod = 0,

                                };
                                objCentralData.actualizarStockPvt(objStock);
                            }


                        }
                        new BCentral().EliminarProdNotaSalidaDetalle(objelimi);
                    }
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarProdNotaSalida(EProdNotaSalida objEProdNotaSalida)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarProdNotaSalida(objEProdNotaSalida);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarProdNotaSalidaDetalleAlmacen(List<EProdNotaSalidaDetalleAlmacen> mlist)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarProdNotaSalidaDetalleAlmacen(mlist);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EProdNotaSalidaDetalleAlmacen> ListarProdNotaSalidaDetalleAlmacen(int Code)
        {
            List<EProdNotaSalidaDetalleAlmacen> lista = null;
            try
            {
                lista = objCentral.ListarProdNotaSalidaDetalleAlmacen(Code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EProdNotaSalidaDetalle> ListarProdNotaSalidaDetalle(int Code)
        {
            List<EProdNotaSalidaDetalle> lista = null;
            try
            {
                lista = objCentral.ListarProdNotaSalidaDetalle(Code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarProdNotaSalidaDetalle(EProdNotaSalidaDetalle objNotaSalidaDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.InsertarProdNotaSalidaDetalle(objNotaSalidaDetalle);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarProdNotaSalidaDetalle(EProdNotaSalidaDetalle objEProdNotaSalidaDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ActualizarProdNotaSalidaDetalle(objEProdNotaSalidaDetalle);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarProdNotaSalidaDetalle(EProdNotaSalidaDetalle objEProdNotaSalidaDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarProdNotaSalidaDetalle(objEProdNotaSalidaDetalle);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Kardex

        public int InsertarKardexpvt(EProdKardex obj)
        {
            int Code = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Code = objCentral.InsertarKardexpvt(obj);
                    EProdStockProducto objStock = new EProdStockProducto()
                    {
                        stocc_ianio = Parametros.intEjercicio,
                        stocc_iid_almacen = obj.iid_almacen,
                        stocc_iid_producto = obj.iid_producto,
                        stocc_nstock_prod = obj.Cantidad,
                        intTipoMovimiento = obj.movimiento,
                    };
                    objCentralData.actualizarStockPvt(objStock);
                    tx.Complete();
                }
                return Code;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarKardexPvt(EProdKardex obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.modificarKardexPvt(obj);
                    EProdStockProducto objStock = new EProdStockProducto()
                    {
                        stocc_ianio = Parametros.intEjercicio,
                        stocc_iid_almacen = obj.iid_almacen,
                        stocc_iid_producto = obj.iid_producto,
                        stocc_nstock_prod = obj.Cantidad,
                        intTipoMovimiento = obj.movimiento,
                    };
                    objCentralData.actualizarStockPvt(objStock);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarKardexPvt(EProdKardex obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarKardexPvt(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EProdKardex> ListarProdKardexFechaAlmacen(EProdStockProducto obj)
        {
            List<EProdKardex> lista = null;
            try
            {
                lista = objCentral.ListarProdKardexFechaAlmacen(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProdKardex> ListarProdKardexFechaIntervaloAlmacen(DateTime dtFechaDesde, DateTime dtFechaHasta, int? intAlmacen, int? intProducto)
        {
            List<EProdKardex> lista = null;
            try
            {
                lista = objCentral.ListarProdKardexFechaIntervaloAlmacen(dtFechaDesde, dtFechaHasta, intAlmacen, intProducto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProdStockProducto> listarVerificarStockMercaderia(EProdStockProducto obj)
        {
            List<EProdStockProducto> lista = null;
            try
            {
                lista = objCentral.listarVerificarStockMercaderia(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProdconsultaKardex> ListarProdResumenMovimientoProductos(EProdReporte objReporte)
        {
            List<EProdconsultaKardex> lista = null;
            try
            {
                lista = objCentral.ListarProdResumenMovimientoProductos(objReporte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion

        #region Transferecia
        public List<EProdTransferencia> ListarProdTransferencia()
        {
            List<EProdTransferencia> lista = null;
            try
            {
                lista = objCentral.ListarProdTransferencia();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarProdTransferencia(EProdTransferencia objEProdTransferencia, List<EProdTransferenciaDetalle> objdetalletrans)
        {
            int intIdTransferencia = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIdTransferencia = objCentral.InsertarProdTransferencia(objEProdTransferencia);
                    foreach (EProdTransferenciaDetalle obj in objdetalletrans)
                    {
                        if (obj.operacion == 1)
                        {
                            obj.trfc_icod_transf = intIdTransferencia;
                            #region kardex Salida
                            EProdKardex oeKardexSalida = new EProdKardex();
                            oeKardexSalida.kardc_ianio = Parametros.intEjercicio;
                            oeKardexSalida.kardx_sfecha = objEProdTransferencia.trfc_sfecha_transf;
                            oeKardexSalida.iid_almacen = obj.trfcd_iid_alm_sal;
                            oeKardexSalida.iid_producto = obj.trfcd_icod_producto;
                            oeKardexSalida.Cantidad = obj.trfcd_ncantidad;
                            oeKardexSalida.puvec_icod_punto_venta = objEProdTransferencia.puvec_icod_punto_venta;
                            oeKardexSalida.NroNota = obj.trfcd_num_item;
                            oeKardexSalida.iid_tipo_doc = 55;//tranasferencia entre almacenes
                            oeKardexSalida.NroDocumento = string.Format("{0:00000}", objEProdTransferencia.trfc_inum_transf);
                            oeKardexSalida.movimiento = 0;
                            oeKardexSalida.Motivo = 2;//TRANSFERENCIA
                            oeKardexSalida.Beneficiario = "TRANFERENCIA DE PRODUCTOS";
                            oeKardexSalida.Observaciones = obj.pr_vdescripcion_producto;
                            oeKardexSalida.intUsuario = obj.iusuario;
                            oeKardexSalida.operacion = obj.operacion;
                            oeKardexSalida.stocc_codigo_producto = obj.trfcd_vcodigo_externo;
                            oeKardexSalida.kardc_iid_correlativo = Convert.ToInt32(obj.trfcd_idsal_kardex);
                            obj.trfcd_idsal_kardex = Convert.ToInt32((new BCentral()).InsertarKardexpvt(oeKardexSalida));
                            EProdStockProducto objStock = new EProdStockProducto()
                            {
                                stocc_ianio = Parametros.intEjercicio,
                                stocc_iid_almacen = obj.trfcd_iid_alm_sal,
                                stocc_iid_producto = obj.trfcd_icod_producto,
                                stocc_nstock_prod = obj.trfcd_ncantidad,
                                intTipoMovimiento = oeKardexSalida.movimiento,
                            };
                            objCentralData.actualizarStockPvt(objStock);
                            #endregion
                            #region kardex Ingreso
                            EProdKardex oeKardexIngreso = new EProdKardex();
                            oeKardexIngreso.kardc_ianio = Parametros.intEjercicio;
                            oeKardexIngreso.kardx_sfecha = objEProdTransferencia.trfc_sfecha_transf;
                            oeKardexIngreso.iid_almacen = obj.trfcd_iid_alm_ing;
                            oeKardexIngreso.iid_producto = obj.trfcd_icod_producto;
                            oeKardexIngreso.Cantidad = obj.trfcd_ncantidad;
                            oeKardexIngreso.puvec_icod_punto_venta = objEProdTransferencia.puvec_icod_punto_venta;
                            oeKardexIngreso.NroNota = obj.trfcd_num_item;
                            oeKardexIngreso.iid_tipo_doc = 55;//TRANAFERENCIA DE ALMACENES
                            oeKardexIngreso.NroDocumento = string.Format("{0:00000}", objEProdTransferencia.trfc_inum_transf + 1);
                            oeKardexIngreso.movimiento = 0;
                            oeKardexIngreso.Motivo = 1;//TRANSFERENCIA
                            oeKardexIngreso.Beneficiario = "TRANASFERENCIA DE PRODUCTOS";
                            oeKardexIngreso.Observaciones = obj.pr_vdescripcion_producto;
                            oeKardexIngreso.intUsuario = obj.iusuario;
                            oeKardexIngreso.operacion = obj.operacion;
                            oeKardexIngreso.stocc_codigo_producto = obj.trfcd_vcodigo_externo;
                            oeKardexIngreso.kardc_iid_correlativo = Convert.ToInt32(obj.trfcd_iding_kardex);
                            obj.trfcd_iding_kardex = Convert.ToInt32((new BCentral()).InsertarKardexpvt(oeKardexIngreso));
                            EProdStockProducto objstock = new EProdStockProducto()
                            {
                                stocc_ianio = Parametros.intEjercicio,
                                stocc_iid_almacen = obj.trfcd_iid_alm_ing,
                                stocc_iid_producto = obj.trfcd_icod_producto,
                                stocc_nstock_prod = obj.trfcd_ncantidad,
                                intTipoMovimiento = oeKardexIngreso.movimiento,
                            };
                            objCentralData.actualizarStockPvt(objstock);
                            #endregion

                            new BCentral().InsertarProdTransferenciaDetalle(obj);

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

        public void ActualizarProdTransferencia(EProdTransferencia objEProdTransferencia, List<EProdTransferenciaDetalle> objdetalletrans, List<EProdTransferenciaDetalle> mlistaEliminados)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objCentral.ActualizarProdTransferencia(objEProdTransferencia);

                    if (objdetalletrans.Where(val => val.operacion == 1).ToList().Count > 0)
                    {
                        foreach (EProdTransferenciaDetalle inser in objdetalletrans)
                        {
                            if (inser.operacion == 1)
                            {
                                inser.trfc_icod_transf = objEProdTransferencia.trfc_icod_transf;
                                #region kardex Salida
                                EProdKardex oeKardexSalida = new EProdKardex();
                                oeKardexSalida.kardc_ianio = Parametros.intEjercicio;
                                oeKardexSalida.kardx_sfecha = objEProdTransferencia.trfc_sfecha_transf;
                                oeKardexSalida.iid_almacen = inser.trfcd_iid_alm_sal;
                                oeKardexSalida.iid_producto = inser.trfcd_icod_producto;
                                oeKardexSalida.puvec_icod_punto_venta = objEProdTransferencia.puvec_icod_punto_venta;
                                oeKardexSalida.Cantidad = inser.trfcd_ncantidad;
                                oeKardexSalida.NroNota = inser.trfcd_num_item;
                                oeKardexSalida.iid_tipo_doc = 55;//TRANSFERENCIA ENTRE ALMACENES
                                oeKardexSalida.NroDocumento = string.Format("{0:00000}", objEProdTransferencia.trfc_inum_transf);
                                oeKardexSalida.movimiento = 0;
                                oeKardexSalida.Motivo = 2;//TRANAFERENCIA
                                oeKardexSalida.Beneficiario = "no lo se";
                                oeKardexSalida.Observaciones = inser.pr_vdescripcion_producto;
                                oeKardexSalida.intUsuario = inser.iusuario;
                                oeKardexSalida.operacion = inser.operacion;
                                oeKardexSalida.stocc_codigo_producto = inser.trfcd_vcodigo_externo;
                                oeKardexSalida.kardc_iid_correlativo = Convert.ToInt32(inser.trfcd_idsal_kardex);
                                inser.trfcd_idsal_kardex = Convert.ToInt32((new BCentral()).InsertarKardexpvt(oeKardexSalida));
                                EProdStockProducto objStock = new EProdStockProducto()
                                {
                                    stocc_ianio = Parametros.intEjercicio,
                                    stocc_iid_almacen = inser.trfcd_iid_alm_sal,
                                    stocc_iid_producto = inser.trfcd_icod_producto,
                                    stocc_nstock_prod = inser.trfcd_ncantidad,
                                    intTipoMovimiento = oeKardexSalida.movimiento,
                                };
                                objCentralData.actualizarStockPvt(objStock);
                                #endregion
                                #region kardex Ingreso
                                EProdKardex oeKardexIngreso = new EProdKardex();
                                oeKardexIngreso.kardc_ianio = Parametros.intEjercicio;
                                oeKardexIngreso.kardx_sfecha = objEProdTransferencia.trfc_sfecha_transf;
                                oeKardexIngreso.iid_almacen = inser.trfcd_iid_alm_ing;
                                oeKardexIngreso.iid_producto = inser.trfcd_icod_producto;
                                oeKardexIngreso.puvec_icod_punto_venta = objEProdTransferencia.puvec_icod_punto_venta;
                                oeKardexIngreso.Cantidad = inser.trfcd_ncantidad;
                                oeKardexIngreso.NroNota = inser.trfcd_num_item;
                                oeKardexIngreso.iid_tipo_doc = 55;//TRANAFERENCIA ENTRE ALMACENES
                                oeKardexIngreso.NroDocumento = string.Format("{0:00000}", objEProdTransferencia.trfc_inum_transf + 1);
                                oeKardexIngreso.movimiento = 1;
                                oeKardexIngreso.Motivo = 2;
                                oeKardexIngreso.Beneficiario = "no lo se";
                                oeKardexIngreso.Observaciones = inser.pr_vdescripcion_producto;
                                oeKardexIngreso.intUsuario = inser.iusuario;
                                oeKardexIngreso.operacion = inser.operacion;
                                oeKardexIngreso.stocc_codigo_producto = inser.trfcd_vcodigo_externo;
                                oeKardexIngreso.kardc_iid_correlativo = Convert.ToInt32(inser.trfcd_iding_kardex);
                                inser.trfcd_iding_kardex = Convert.ToInt32((new BCentral()).InsertarKardexpvt(oeKardexIngreso));

                                EProdStockProducto objstock = new EProdStockProducto()
                                {
                                    stocc_ianio = Parametros.intEjercicio,
                                    stocc_iid_almacen = inser.trfcd_iid_alm_ing,
                                    stocc_iid_producto = inser.trfcd_icod_producto,
                                    stocc_nstock_prod = inser.trfcd_ncantidad,
                                    intTipoMovimiento = oeKardexIngreso.movimiento,
                                };
                                objCentralData.actualizarStockPvt(objstock);
                                #endregion
                                new BCentral().InsertarProdTransferenciaDetalle(inser);
                            }
                        }
                    }

                    if (objdetalletrans.Where(val => val.operacion == 2).ToList().Count > 0)
                    {
                        foreach (EProdTransferenciaDetalle actua in objdetalletrans)
                        {
                            if (actua.operacion == 2)
                            {
                                actua.trfc_icod_transf = objEProdTransferencia.trfc_icod_transf;
                                objCentral.ActualizarProdTransferenciaDetalle(actua);
                                #region kardex Salida
                                EProdKardex oeKardexSalida = new EProdKardex();
                                oeKardexSalida.kardc_ianio = Parametros.intEjercicio;
                                oeKardexSalida.kardx_sfecha = objEProdTransferencia.trfc_sfecha_transf;
                                oeKardexSalida.iid_almacen = actua.trfcd_iid_alm_sal;
                                oeKardexSalida.iid_producto = actua.trfcd_icod_producto;
                                oeKardexSalida.puvec_icod_punto_venta = objEProdTransferencia.puvec_icod_punto_venta;
                                oeKardexSalida.Cantidad = actua.trfcd_ncantidad;
                                oeKardexSalida.NroNota = actua.trfcd_num_item;
                                oeKardexSalida.iid_tipo_doc = 55;
                                oeKardexSalida.NroDocumento = string.Format("{0:00000}", objEProdTransferencia.trfc_inum_transf);
                                oeKardexSalida.movimiento = 0;
                                oeKardexSalida.Motivo = 2;
                                oeKardexSalida.Beneficiario = "TRANSFERENCIA DE PRODUCTOS";
                                oeKardexSalida.Observaciones = actua.pr_vdescripcion_producto;
                                oeKardexSalida.intUsuario = actua.iusuario;
                                oeKardexSalida.operacion = actua.operacion;
                                oeKardexSalida.stocc_codigo_producto = actua.trfcd_vcodigo_externo;
                                oeKardexSalida.kardc_iid_correlativo = Convert.ToInt32(actua.trfcd_idsal_kardex);
                                (new BCentral()).InsertarKardexpvt(oeKardexSalida);
                                EProdStockProducto objStock = new EProdStockProducto()
                                {
                                    stocc_ianio = Parametros.intEjercicio,
                                    stocc_iid_almacen = actua.trfcd_iid_alm_sal,
                                    stocc_iid_producto = actua.trfcd_icod_producto,
                                    stocc_nstock_prod = actua.trfcd_ncantidad,
                                    intTipoMovimiento = oeKardexSalida.movimiento,
                                };
                                objCentralData.actualizarStockPvt(objStock);
                                #endregion
                                #region Kadex Ingreso
                                EProdKardex oeKardexIngreso = new EProdKardex();
                                oeKardexIngreso.kardc_ianio = Parametros.intEjercicio;
                                oeKardexIngreso.kardx_sfecha = objEProdTransferencia.trfc_sfecha_transf;
                                oeKardexIngreso.iid_almacen = actua.trfcd_iid_alm_ing;
                                oeKardexIngreso.iid_producto = actua.trfcd_icod_producto;
                                oeKardexIngreso.puvec_icod_punto_venta = objEProdTransferencia.puvec_icod_punto_venta;
                                oeKardexIngreso.Cantidad = actua.trfcd_ncantidad;
                                oeKardexIngreso.NroNota = actua.trfcd_num_item;
                                oeKardexIngreso.iid_tipo_doc = 55;
                                oeKardexIngreso.NroDocumento = string.Format("{0:00000}", objEProdTransferencia.trfc_inum_transf + 1);//no lo se
                                oeKardexIngreso.movimiento = 1;
                                oeKardexIngreso.Motivo = 2;
                                oeKardexIngreso.Beneficiario = "TRANSFERENCIA DE PRODUCTOS";
                                oeKardexIngreso.Observaciones = actua.pr_vdescripcion_producto;
                                oeKardexIngreso.intUsuario = actua.iusuario;
                                oeKardexIngreso.operacion = actua.operacion;
                                oeKardexIngreso.stocc_codigo_producto = actua.trfcd_vcodigo_externo;
                                oeKardexIngreso.kardc_iid_correlativo = Convert.ToInt32(actua.trfcd_iding_kardex);
                                (new BCentral()).InsertarKardexpvt(oeKardexIngreso);
                                EProdStockProducto objstock = new EProdStockProducto()
                                {
                                    stocc_ianio = Parametros.intEjercicio,
                                    stocc_iid_almacen = actua.trfcd_iid_alm_ing,
                                    stocc_iid_producto = actua.trfcd_icod_producto,
                                    stocc_nstock_prod = actua.trfcd_ncantidad,
                                    intTipoMovimiento = oeKardexIngreso.movimiento,
                                };
                                objCentralData.actualizarStockPvt(objstock);
                                #endregion
                            }
                        }
                    }

                    if (mlistaEliminados.Count > 0)
                    {
                        foreach (EProdTransferenciaDetalle elimin in mlistaEliminados)
                        {
                            if (elimin.operacion == 3)
                            {
                                elimin.trfc_icod_transf = objEProdTransferencia.trfc_icod_transf;
                                #region Kardex Salida
                                EProdKardex oeKardexSalida = new EProdKardex();
                                oeKardexSalida.kardc_ianio = Parametros.intEjercicio;
                                oeKardexSalida.kardx_sfecha = objEProdTransferencia.trfc_sfecha_transf;
                                oeKardexSalida.iid_almacen = elimin.trfcd_iid_alm_sal;
                                oeKardexSalida.iid_producto = elimin.trfcd_icod_producto;
                                oeKardexSalida.puvec_icod_punto_venta = objEProdTransferencia.puvec_icod_punto_venta;
                                oeKardexSalida.Cantidad = elimin.trfcd_ncantidad;
                                oeKardexSalida.NroNota = elimin.trfcd_num_item;
                                oeKardexSalida.iid_tipo_doc = 55;
                                oeKardexSalida.NroDocumento = string.Format("{0:00000}", objEProdTransferencia.trfc_inum_transf);
                                oeKardexSalida.movimiento = 0;
                                oeKardexSalida.Motivo = 2;
                                oeKardexSalida.Beneficiario = "TRANSFERENCIA DE PRODUCTOS";
                                oeKardexSalida.Observaciones = elimin.pr_vdescripcion_producto;
                                oeKardexSalida.intUsuario = elimin.iusuario;
                                oeKardexSalida.operacion = elimin.operacion;
                                oeKardexSalida.stocc_codigo_producto = elimin.trfcd_vcodigo_externo;
                                oeKardexSalida.kardc_iid_correlativo = Convert.ToInt32(elimin.trfcd_idsal_kardex);
                                (new BCentral()).InsertarKardexpvt(oeKardexSalida);
                                EProdStockProducto objStock = new EProdStockProducto()
                                {
                                    stocc_ianio = Parametros.intEjercicio,
                                    stocc_iid_almacen = elimin.trfcd_iid_alm_sal,
                                    stocc_iid_producto = elimin.trfcd_icod_producto,
                                    stocc_nstock_prod = elimin.trfcd_ncantidad,
                                    intTipoMovimiento = oeKardexSalida.movimiento,
                                };
                                objCentralData.actualizarStockPvt(objStock);
                                #endregion
                                #region Kadex Ingreso
                                EProdKardex oeKardexIngreso = new EProdKardex();
                                oeKardexIngreso.kardc_ianio = Parametros.intEjercicio;
                                oeKardexIngreso.kardx_sfecha = objEProdTransferencia.trfc_sfecha_transf;
                                oeKardexIngreso.iid_almacen = elimin.trfcd_iid_alm_ing;
                                oeKardexIngreso.iid_producto = elimin.trfcd_icod_producto;
                                oeKardexIngreso.puvec_icod_punto_venta = objEProdTransferencia.puvec_icod_punto_venta;
                                oeKardexIngreso.Cantidad = elimin.trfcd_ncantidad;
                                oeKardexIngreso.NroNota = elimin.trfcd_num_item;
                                oeKardexIngreso.iid_tipo_doc = 55;
                                oeKardexIngreso.NroDocumento = string.Format("{0:00000}", objEProdTransferencia.trfc_inum_transf);
                                oeKardexIngreso.movimiento = 1;
                                oeKardexIngreso.Motivo = 2;
                                oeKardexIngreso.Beneficiario = "TRANSFERENCIA DE PRODUCTOS";
                                oeKardexIngreso.Observaciones = elimin.pr_vdescripcion_producto;
                                oeKardexIngreso.intUsuario = elimin.iusuario;
                                oeKardexIngreso.operacion = elimin.operacion;
                                oeKardexIngreso.stocc_codigo_producto = elimin.trfcd_vcodigo_externo;
                                oeKardexIngreso.kardc_iid_correlativo = Convert.ToInt32(elimin.trfcd_iding_kardex);
                                (new BCentral()).InsertarKardexpvt(oeKardexIngreso);
                                EProdStockProducto objstock = new EProdStockProducto()
                                {
                                    stocc_ianio = Parametros.intEjercicio,
                                    stocc_iid_almacen = elimin.trfcd_iid_alm_ing,
                                    stocc_iid_producto = elimin.trfcd_icod_producto,
                                    stocc_nstock_prod = elimin.trfcd_ncantidad,
                                    intTipoMovimiento = oeKardexIngreso.movimiento,
                                };
                                objCentralData.actualizarStockPvt(objstock);
                                #endregion
                                objCentral.EliminarProdTransferenciaDetalle(elimin.trfcd_icod_transf);
                            }
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

        public void EliminarProdTransferencia(int intIdTransferencia, int icod_punto_venta)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    List<EProdTransferenciaDetalle> mlistDetalle = new List<EProdTransferenciaDetalle>();

                    objCentral.EliminarProdTransferencia(intIdTransferencia);
                    mlistDetalle = new BCentral().ListarProdTransferenciaDetalle(intIdTransferencia);
                    if (mlistDetalle.Count > 0)
                    {
                        foreach (EProdTransferenciaDetalle obj in mlistDetalle)
                        {
                            #region Kardex Salida
                            EProdKardex oeKardexSalida = new EProdKardex();
                            oeKardexSalida.kardc_ianio = Parametros.intEjercicio;
                            oeKardexSalida.kardx_sfecha = DateTime.Today;
                            oeKardexSalida.iid_almacen = obj.trfcd_iid_alm_sal;
                            oeKardexSalida.iid_producto = obj.trfcd_icod_producto;
                            oeKardexSalida.puvec_icod_punto_venta = icod_punto_venta;
                            oeKardexSalida.Cantidad = obj.trfcd_ncantidad;
                            oeKardexSalida.NroNota = obj.trfcd_num_item;
                            oeKardexSalida.iid_tipo_doc = 55;
                            oeKardexSalida.NroDocumento = "";
                            oeKardexSalida.movimiento = 0;
                            oeKardexSalida.Motivo = 2;
                            oeKardexSalida.Beneficiario = "TRANAFERENCIA DE PRODUCTOS";
                            oeKardexSalida.Observaciones = obj.pr_vdescripcion_producto;
                            oeKardexSalida.intUsuario = obj.iusuario;
                            oeKardexSalida.operacion = 3;
                            oeKardexSalida.stocc_codigo_producto = obj.trfcd_vcodigo_externo;
                            oeKardexSalida.kardc_iid_correlativo = Convert.ToInt32(obj.trfcd_idsal_kardex);
                            (new BCentral()).InsertarKardexpvt(oeKardexSalida);
                            EProdStockProducto objStock = new EProdStockProducto()
                            {
                                stocc_ianio = Parametros.intEjercicio,
                                stocc_iid_almacen = obj.trfcd_iid_alm_sal,
                                stocc_iid_producto = obj.trfcd_icod_producto,
                                stocc_nstock_prod = obj.trfcd_ncantidad,
                                intTipoMovimiento = oeKardexSalida.movimiento,
                            };
                            objCentralData.actualizarStockPvt(objStock);
                            #endregion
                            #region Kardex Ingreso
                            EProdKardex oeKardexIngreso = new EProdKardex();
                            oeKardexIngreso.kardc_ianio = Parametros.intEjercicio;
                            oeKardexIngreso.kardx_sfecha = DateTime.Today;
                            oeKardexIngreso.iid_almacen = obj.trfcd_iid_alm_ing;
                            oeKardexIngreso.iid_producto = obj.trfcd_icod_producto;
                            oeKardexIngreso.puvec_icod_punto_venta = icod_punto_venta;
                            oeKardexIngreso.Cantidad = obj.trfcd_ncantidad;
                            oeKardexIngreso.NroNota = obj.trfcd_num_item;
                            oeKardexIngreso.iid_tipo_doc = 55;
                            oeKardexIngreso.NroDocumento = "";
                            oeKardexIngreso.movimiento = 1;
                            oeKardexIngreso.Motivo = 2;
                            oeKardexIngreso.Beneficiario = "TRANSFERENCIA DE PRODUCTOS";
                            oeKardexIngreso.Observaciones = obj.pr_vdescripcion_producto;
                            oeKardexIngreso.intUsuario = obj.iusuario;
                            oeKardexIngreso.operacion = 3;
                            oeKardexIngreso.stocc_codigo_producto = obj.trfcd_vcodigo_externo;
                            oeKardexIngreso.kardc_iid_correlativo = Convert.ToInt32(obj.trfcd_iding_kardex);
                            (new BCentral()).InsertarKardexpvt(oeKardexIngreso);
                            EProdStockProducto objstock = new EProdStockProducto()
                            {
                                stocc_ianio = Parametros.intEjercicio,
                                stocc_iid_almacen = obj.trfcd_iid_alm_ing,
                                stocc_iid_producto = obj.trfcd_icod_producto,
                                stocc_nstock_prod = obj.trfcd_ncantidad,
                                intTipoMovimiento = oeKardexIngreso.movimiento,
                            };
                            objCentralData.actualizarStockPvt(objstock);
                            #endregion
                            objCentral.EliminarProdTransferenciaDetalle(obj.trfcd_icod_transf);
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


        public List<EProdTransferenciaDetalle> ListarProdTransferenciaDetalle(int intNumDoc)
        {
            List<EProdTransferenciaDetalle> lista = null;
            try
            {
                lista = objCentral.ListarProdTransferenciaDetalle(intNumDoc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public void InsertarProdTransferenciaDetalle(EProdTransferenciaDetalle objEProdTransferenciaDetalle)
        {

            int intIdTransferenciaDetalle = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIdTransferenciaDetalle = objCentral.InsertarProdTransferenciaDetalle(objEProdTransferenciaDetalle);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarProdTransferenciaDetalle(EProdTransferenciaDetalle objEProdTransferenciaDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ActualizarProdTransferenciaDetalle(objEProdTransferenciaDetalle);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarProdTransferenciaDetalle(int intIdTransferenciaDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarProdTransferenciaDetalle(intIdTransferenciaDetalle);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Codigo de Barra
        public List<EProdCodigoBarra> ListarProdCodigoBarra()
        {
            List<EProdCodigoBarra> lista = null;
            try
            {
                lista = objCentral.ListarProdCodigoBarra();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarProdCodigoBarra(EProdCodigoBarra obj, List<EProdCodigoBarraDetalle> mlistaCodiBarra)
        {
            int? codigo = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    codigo = objCentral.InsertarProdCodigoBarra(obj);
                    foreach (EProdCodigoBarraDetalle objcodi in mlistaCodiBarra)
                    {
                        objcodi.picbd_icod_plan_cod_barr = Convert.ToInt32(codigo);
                        InsertarProdCodigoBarraDetalle(objcodi);
                    }
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarProdCodigoBarra(EProdCodigoBarra obj, List<EProdCodigoBarraDetalle> mlistaCodiBarra, List<EProdCodigoBarraDetalle> mlistaElimnadosDet)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ModificarProdCodigoBarra(obj);

                    foreach (EProdCodigoBarraDetalle objj in mlistaCodiBarra)
                    {
                        if (objj.operacion == 1)
                        {
                            objj.picbd_icod_plan_cod_barr = obj.picbc_icod_plan_cod_barr;
                            InsertarProdCodigoBarraDetalle(objj);
                        }
                        if (objj.operacion == 2)
                        {
                            ActualizarProdCodigoBarraDetalle(objj);
                        }
                    }




                    if (mlistaElimnadosDet.Count > 0)
                    {
                        foreach (EProdCodigoBarraDetalle ol in mlistaElimnadosDet)
                        {
                            EliminarProdCodigoBarraDetalle(ol);
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
        public void EliminarProdCodigoBarra(EProdCodigoBarra obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    List<EProdCodigoBarraDetalle> mlistdetalle = new List<EProdCodigoBarraDetalle>();
                    objCentral.EliminarProdCodigoBarra(obj);
                    mlistdetalle = listarProdCodigoBarraDetalle(obj.picbc_icod_plan_cod_barr);
                    foreach (EProdCodigoBarraDetalle objh in mlistdetalle)
                    {
                        EliminarProdCodigoBarraDetalle(objh);
                    }
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EProdCodigoBarraDetalle> listarProdCodigoBarraDetalle(int picbd_icod_plan_cod_barr)
        {
            List<EProdCodigoBarraDetalle> lista = null;
            try
            {
                lista = objCentral.listarProdCodigoBarraDetalle(picbd_icod_plan_cod_barr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarProdCodigoBarraDetalle(EProdCodigoBarraDetalle obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.InsertarProdCodigoBarraDetalle(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarProdCodigoBarraDetalle(EProdCodigoBarraDetalle obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ActualizarProdCodigoBarraDetalle(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarProdCodigoBarraDetalle(EProdCodigoBarraDetalle obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarProdCodigoBarraDetalle(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public int VerificarProdProdMovAlmacen(int iOpcion, int iid_almacen)
        {
            int cantidad;
            try
            {
                cantidad = objCentral.VerificarProdMovAlmacen(iOpcion, iid_almacen);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cantidad;
        }
        public List<EProdProducto> VerificarProductoPvt(string code)
        {
            List<EProdProducto> lista = null;
            try
            {
                lista = objCentral.VerificarProductoPvt(code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;

        }
        public List<EProdProducto> VerificarProdStockProducto(int cod, int almac_icod_alma, int intanio)
        {
            List<EProdProducto> lista = null;
            try
            {
                lista = objCentral.VerificarProdStockProducto(cod, almac_icod_alma, intanio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProdProducto> ListarProdProductosXAlmacen(int stocc_iid_almacen, int intanio)
        {
            List<EProdProducto> lista = null;
            try
            {
                lista = objCentral.ListarProdProductosXAlmacen(stocc_iid_almacen, intanio);
                //lista = centralService.ProductoPorAlmacenListar(stocc_iid_almacen, intanio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #region Punto Venta

        public List<EPuntoVenta> ListarPuntoVenta()
        {
            List<EPuntoVenta> lista = null;
            try
            {
                lista = objCentral.ListarPuntoVenta();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int InsertarPuntoVenta(EPuntoVenta obe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.InsertarPuntoVenta(obe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarPuntoVenta(EPuntoVenta objEPuntoVenta)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ActualizarPuntoVenta(objEPuntoVenta);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarPuntoVenta(EPuntoVenta ob)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarPuntoVenta(ob);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int ValidarCodigoPuntoVenta()
        {
            int Cod_punto_venta;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Cod_punto_venta = objCentral.ValidarCodigoPuntoVenta();
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Cod_punto_venta;
        }
        #endregion
        #region Stock
        public List<EProdKardex> ListarProdKardexProductoFechaAlmacen(EProdStockProducto obj)
        {
            List<EProdKardex> lista = null;
            try
            {
                lista = objCentral.ListarProdKardexProductoFechaAlmacen(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProdStockProducto> ListarProdStockPorAlmacen(DateTime dtFechaDesde, DateTime dtFechaHasta, int? intAlmacen, int? intProducto)
        {
            List<EProdStockProducto> lista = null;
            try
            {
                lista = objCentral.ListarProdStockPorAlmacen(dtFechaDesde, dtFechaHasta, intAlmacen, intProducto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion

        #region Envio de Informacion
        public List<ESendInfo> ListarSendInfo()
        {
            List<ESendInfo> lista = null;
            try
            {
                lista = objCentral.ListarSendInfo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void crearArchivo(List<EMenu> lista)
        {
            string ruta = "C:\\Info";
            StreamWriter fs = File.CreateText(ruta + "\\Formularios.txt");
            foreach (EMenu obj in lista)
            {
                fs.Write(fs.NewLine + obj.FullNameSpace + " % " + obj.FullDescription + " % " + obj.Description);
            }
            fs.Close();
        }
        public void crearArchivoTransferencia(string ruta, List<ESendInfo> listSend, Object jonathan)
        {
            //jonathan. = 1;
            //jonathan.Properties.PercentView = true;
            //jonathan.Properties.Maximum = 10;
            //jonathan.Properties.Minimum = 0;
            //jonathan.GetType

            //using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
            //{

            //    string activeDir = ruta;
            //    string newPath = System.IO.Path.Combine(activeDir, "mySubDir");
            //    System.IO.Directory.CreateDirectory(newPath);

            //    Random rA = new Random();
            //    FastZip zip = new ICSharpCode.SharpZipLib.Zip.FastZip();
            //    zip.Password = "5384250";
            //    foreach (ESendInfo obj in listSend)
            //    {
            //        if (obj.estado == true)
            //        {
            //            string direcc;
            //            Random r = new Random();
            //            int aleat = r.Next(100);
            //            direcc = newPath + "\\" + aleat.ToString() + obj.trans_nombre_archivo;
            //            objCentral.crearArchivoTransferencia(direcc, obj.trans_nombre_tabla);
            //        }
            //    }
            //    zip.CreateEmptyDirectories = true;
            //    zip.CreateZip(ruta + "\\" + rA.Next(1000000, 9999999) + ".zip", newPath + "\\", true, "\\.txt");
            //    Directory.Delete(newPath, true);
            //}
        }
        #endregion

        #region Lista Precio
        #region Cabecera
        CentralData objCentralData = new CentralData();

        public List<EProdListaPreciosDetalle> ListarProdProductoTallas(string pr_vcodigo_externo)
        {
            List<EProdListaPreciosDetalle> lista = null;
            try
            {
                lista = objCentralData.ListarProdProductoTallas(pr_vcodigo_externo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }


        public void ActulizarProdlprec_bdetalle(int lprec_icod_precio, bool lprec_bdetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.ActulizarProdlprec_bdetalle(lprec_icod_precio, lprec_bdetalle);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EProdListaPrecios> ListarProdListaPreciosUnRegistro(int lprec_icod_precio)
        {
            List<EProdListaPrecios> lista = null;
            try
            {
                lista = objCentralData.ListarProdListaPreciosUnRegistro(lprec_icod_precio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public EProdListaPrecios ListarListaPreciosXCodigo(string codigo_Externo)
        {
            EProdListaPrecios obj = null;
            try
            {
                obj = objCentralData.ListarProdListaPreciosXCodigo(codigo_Externo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public List<EProdListaPrecios> ListaProdProductoSinTalla(int id_marca, int id_modelo, int id_color)
        {
            List<EProdListaPrecios> lista = null;
            try
            {
                lista = objCentralData.ListaProdProductoSinTalla(id_marca, id_modelo, id_color);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public void InsertarProdListaPrecio(EProdListaPrecios objMovCaja, List<EProdListaPreciosDetalle> mlistDetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    int icod_lista_precio;
                    icod_lista_precio = Convert.ToInt32(objCentralData.InsertarProdListaPrecio(objMovCaja));
                    foreach (var obj in mlistDetalle)
                    {
                        obj.lprec_icod_precio = icod_lista_precio;
                        objCentralData.InsertarProdListaPrecioDetalle(obj);
                    }
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarProdListaPrecio(EProdListaPrecios objMovCaja, List<EProdListaPreciosDetalle> mlistDeta, List<EProdListaPreciosDetalle> mlistEliminados)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.ActualizarProdListaPrecio(objMovCaja);
                    foreach (var obj in mlistDeta)
                    {
                        if (obj.operacion == 1)
                        {
                            obj.lprec_icod_precio = objMovCaja.lprec_icod_precio;
                            objCentralData.InsertarProdListaPrecioDetalle(obj);
                        }
                        if (obj.operacion == 2)
                        {
                            objCentralData.ActualizarProdListaPrecioDetalle(obj);
                        }
                    }
                    foreach (var objEli in mlistEliminados)
                    {
                        objCentralData.EliminarProdListaPrecioDetalle(objEli);
                    }
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarProdListaPrecio(EProdListaPrecios objMovCaja)
        {
            List<EProdListaPreciosDetalle> lista = new List<EProdListaPreciosDetalle>();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.EliminarProdListaPrecio(objMovCaja);
                    lista = ListarProdListaPrecioDetalle(objMovCaja.lprec_icod_precio);
                    foreach (EProdListaPreciosDetalle obj in lista)
                    {
                        EliminarProdListaPrecioDetalle(obj);
                    }
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Detalle
        public List<EProdListaPreciosDetalle> ListarProdListaPrecioDetalle(int icod_lista_precio)
        {
            List<EProdListaPreciosDetalle> lista = null;
            try
            {
                lista = objCentralData.ListarProdListaPrecioDetalle(icod_lista_precio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarProdListaPrecioDetalle(EProdListaPreciosDetalle obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.InsertarProdListaPrecioDetalle(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarProdListaPrecioDetalle(EProdListaPreciosDetalle obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.ActualizarProdListaPrecioDetalle(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarProdListaPrecioDetalle(EProdListaPreciosDetalle obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.EliminarProdListaPrecioDetalle(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #endregion
        #region Registro de Serie
        public List<ERegistroSerie> ListarRegistroSerie()
        {
            List<ERegistroSerie> lista = null;
            try
            {
                lista = objCentral.ListarRegistroSerie();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int InsertarRegistroSerie(ERegistroSerie obj)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new CentralData().InsertarRegistroSerie(obj);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ModificarRegistroSerie(ERegistroSerie obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ActualizarRegistroSerie(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarRegistroSerie(ERegistroSerie obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarRegistroSerie(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Orden Produccion
        public List<EOrdenProduccion> ListarOrdenProduccion(int Ejercicio)
        {
            List<EOrdenProduccion> lista = null;
            try
            {
                lista = objCentral.ListarOrdenProduccion(Ejercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public EOrdenProduccion OrdenProduccionGetById(int orprc_iid_orden_produccion)
        {
            return objCentral.OrdenProduccionGetById(orprc_iid_orden_produccion);
        }



        public int InsertarOrdenProduccion(EOrdenProduccion obj, List<EOrdenProduccionDet> objdetalle, List<EOrdenProduccionAreas> lstOrdenProduccionArea)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new CentralData().InsertarOrdenProduccion(obj);
                    if (intIcod == 0)
                        throw new Exception("El número de la Orden de Producción ya fue utilizado, intente con un número de Orden de Producción superior");

                    foreach (var objd in objdetalle)
                    {
                        objd.orprc_iid_orden_produccion = intIcod;
                        insertarOrdenPrduccionDetalle(objd);
                    }
                    foreach (var obja in lstOrdenProduccionArea)
                    {
                        obja.orprc_iid_orden_produccion = intIcod;
                        insertarOrdenPrduccionArea(obja);
                    }
                    EParametroProduccion ob = new EParametroProduccion();
                    var _Be = new BAdministracionSistema().listarParametroProduccion();
                    ob.pmprc_inumero_orden_pedido = _Be[0].pmprc_inumero_orden_pedido;
                    ob.pmprc_inumero_ficha_tecnica = _Be[0].pmprc_inumero_ficha_tecnica;
                    ob.pmprc_inumero_orden_produccion = Convert.ToInt32(obj.orprc_icod_orden_produccion);
                    ob.pmprc_icod_parametro_produccion = _Be[0].pmprc_icod_parametro_produccion;
                    ob.pmprc_inumero_nota_pedido = _Be[0].pmprc_inumero_nota_pedido;
                    new BAdministracionSistema().modificarCorrelativoOP(ob);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ModificarOrdenProduccion(EOrdenProduccion obj, List<EOrdenProduccionDet> objdetalle, List<EOrdenProduccionAreas> lstOrdenProducionArea, List<EOrdenProduccionDet> objdelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ActualizarOrdenProduccion(obj);
                    if (objdetalle != null)
                        foreach (var objd in objdetalle)
                        {
                            if (objd.intTipoOperacion == 1)
                            {
                                objd.orprc_iid_orden_produccion = obj.orprc_iid_orden_produccion;
                                insertarOrdenPrduccionDetalle(objd);
                            }
                            else if (objd.intTipoOperacion == 2)
                            {
                                modificarOrdenPrduccionDetalle(objd);
                            }
                        }
                    foreach (var ob in lstOrdenProducionArea)
                    {
                        if (ob.orprac_iid_codigo > 0)
                        {
                            modificarOrdenPrduccionArea(ob);
                        }
                        else
                        {
                            ob.orprc_iid_orden_produccion = obj.orprc_iid_orden_produccion;
                            insertarOrdenPrduccionArea(ob);
                        }
                    }
                    if (objdelete != null)
                        foreach (var objelimi in objdelete)
                        {
                            eliminarOrdenPrduccionDetalle(objelimi);
                        }
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarOrdenProduccion(EOrdenProduccion obj)
        {

            var Mlist = listarOrdenPrduccionDetalle(obj.orprc_iid_orden_produccion);
            foreach (var x in Mlist)
            {
                eliminarOrdenPrduccionDetalle(x);
            }
            var lst = listarOrdenPrduccionArea(obj.orprc_iid_orden_produccion);
            foreach (var y in lst)
            {

                eliminarOrdenPrduccionArea(y);
            }
            new CentralData().EliminarOrdenProduccion(obj);

        }

        public void EliminarOrdenProduccionVarios(EOrdenProduccion obj)
        {



            new CentralData().EliminarOrdenProduccionVarios(obj);

        }
        #endregion
        #region Orden Produccion Detalle
        public List<EOrdenProduccionDet> listarOrdenPrduccionDetalle(int intOrdenPrduccion)
        {
            List<EOrdenProduccionDet> lista = new List<EOrdenProduccionDet>();
            try
            {
                lista = objCentralData.listarOrdenPrduccionDetalle(intOrdenPrduccion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarOrdenPrduccionDetalle(EOrdenProduccionDet objEOrdenPrduccion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.insertarOrdenPrduccionDetalle(objEOrdenPrduccion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarOrdenPrduccionDetalle(EOrdenProduccionDet objEOrdenPrduccion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.modificarOrdenPrduccionDetalle(objEOrdenPrduccion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarOrdenPrduccionDetalle(EOrdenProduccionDet objEOrdenPrduccion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.eliminarOrdenPrduccionDetalle(objEOrdenPrduccion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Orden Produccion Area
        public List<EOrdenProduccionAreas> listarOrdenPrduccionArea(int intOrdenPrduccion)
        {
            List<EOrdenProduccionAreas> lista = new List<EOrdenProduccionAreas>();
            try
            {
                lista = objCentralData.listarOrdenPrduccionArea(intOrdenPrduccion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarOrdenPrduccionArea(EOrdenProduccionAreas objEOrdenPrduccion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.insertarOrdenPrduccionArea(objEOrdenPrduccion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarOrdenPrduccionArea(EOrdenProduccionAreas objEOrdenPrduccion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.modificarOrdenPrduccionArea(objEOrdenPrduccion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarOrdenPrduccionArea(EOrdenProduccionAreas objEOrdenPrduccion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.eliminarOrdenPrduccionArea(objEOrdenPrduccion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Orden de Pedido Cab
        public List<EOrdenPedidoCab> ListarOrdenPedidoCab(int intEjericio)
        {
            List<EOrdenPedidoCab> lista = new List<EOrdenPedidoCab>();
            try
            {
                lista = objCentral.ListarOrdenPedidoCab(intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public async Task<int> InsertarOrdenPedidoCab(EOrdenPedidoCab objEOrdenPedido, List<EOrdenPedidoDet> lstOrdenPedDet)
        {


            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    #region Orden de Pedido Cab
                    objEOrdenPedido.orpec_iid_orden_pedido = objCentral.InsertarOrdenPedidoCab(objEOrdenPedido);

                    //CREAMOS SU CARPETA EN LA NUBE

                    if (!WebDavTest.Crearcarpeta(Path.Combine(Rutas.RutaImagenes, Rutas.RutaOrdenPedido), objEOrdenPedido.orpec_iid_orden_pedido.ToString()))
                        throw new Exception("Error al Guardar Imagenes en la Nube");

                    if (objEOrdenPedido.orpec_iid_orden_pedido == 0)
                    {
                        throw new Exception("El número de la Orden de Pedido ya fue utilizado, intente con un número de Orden de Pedido superior");
                    }
                    #endregion

                    #region Orden de Pedido Det. Insertar

                    foreach (var obj in lstOrdenPedDet)
                    {
                        object[] Tallas = new object[10];
                        object[] Cantidades = new object[10];

                        Tallas[0] = obj.orped_talla1;
                        Tallas[1] = obj.orped_talla2;
                        Tallas[2] = obj.orped_talla3;
                        Tallas[3] = obj.orped_talla4;
                        Tallas[4] = obj.orped_talla5;
                        Tallas[5] = obj.orped_talla6;
                        Tallas[6] = obj.orped_talla7;
                        Tallas[7] = obj.orped_talla8;
                        Tallas[8] = obj.orped_talla9;
                        Tallas[9] = obj.orped_talla10;
                        Cantidades[0] = obj.orped_cant_talla1;
                        Cantidades[1] = obj.orped_cant_talla2;
                        Cantidades[2] = obj.orped_cant_talla3;
                        Cantidades[3] = obj.orped_cant_talla4;
                        Cantidades[4] = obj.orped_cant_talla5;
                        Cantidades[5] = obj.orped_cant_talla6;
                        Cantidades[6] = obj.orped_cant_talla7;
                        Cantidades[7] = obj.orped_cant_talla8;
                        Cantidades[8] = obj.orped_cant_talla9;
                        Cantidades[9] = obj.orped_cant_talla10;

                        obj.orpec_iid_orden_pedido = objEOrdenPedido.orpec_iid_orden_pedido;
                        obj.orped_icod_item_orden_pedido = insertarOrdenPedidoDet(obj);
                        // ACTUALIZAR CORRELATIVO DE ORDEN DE PEDIDO Y FICHA TECNICA
                        var objCurrentCorrelativo = lstOrdenPedDet.OrderByDescending(x => x.orped_iitem_orden_pedido).FirstOrDefault();
                        EParametroProduccion ob = new EParametroProduccion();
                        var _Be = new BAdministracionSistema().listarParametroProduccion();
                        ob.pmprc_inumero_orden_pedido = Convert.ToInt32(objEOrdenPedido.orpec_icod_orden_pedido);
                        ob.pmprc_inumero_ficha_tecnica = Convert.ToInt32(objCurrentCorrelativo.orped_vnum_ficha_tecnica); ;
                        ob.pmprc_inumero_orden_produccion = _Be[0].pmprc_inumero_orden_produccion;
                        ob.pmprc_icod_parametro_produccion = _Be[0].pmprc_icod_parametro_produccion;
                        ob.pmprc_inumero_nota_pedido = _Be[0].pmprc_inumero_nota_pedido;
                        new BAdministracionSistema().modificarCorrelativoOP(ob);
                    };
                    #endregion
                    tx.Complete();
                }
                var taskGuardarImagenes = lstOrdenPedDet.Where(x => x.imagen2 != null).Select(x => WebDavTest.GuardarImagenes(Rutas.RutaImagenes, $"{Rutas.RutaOrdenPedido}{objEOrdenPedido.orpec_iid_orden_pedido}/{x.orped_icod_item_orden_pedido}.png", x.imagen2));
                Task.WhenAll(taskGuardarImagenes);
                return objEOrdenPedido.orpec_iid_orden_pedido;
            }
            catch (Exception ex)
            {
                await WebDavTest.EliminarArchivo($"{Rutas.RutaImagenes}{Rutas.RutaOrdenPedido}{objEOrdenPedido.orpec_iid_orden_pedido}");
                throw ex;
            }
        }

        public async Task modificarOrdenPedidoCab(EOrdenPedidoCab objEOrdenPedido, List<EOrdenPedidoDet> lstOrdenPedDet, List<EOrdenPedidoDet> listdelete)
        {
            bool addNewItem = false;
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //MODIFICAR LA FACTURA

                    objCentral.ActualizarOrdenPedidoCab(objEOrdenPedido);

                    #region Factura Det.

                    foreach (var obj in lstOrdenPedDet)
                    {
                        if (obj.intTipoOperacion == 1)
                        {
                            //insertar kardex
                            object[] Tallas = new object[10];
                            object[] Cantidades = new object[10];
                            Tallas[0] = obj.orped_talla1;
                            Tallas[1] = obj.orped_talla2;
                            Tallas[2] = obj.orped_talla3;
                            Tallas[3] = obj.orped_talla4;
                            Tallas[4] = obj.orped_talla5;
                            Tallas[5] = obj.orped_talla6;
                            Tallas[6] = obj.orped_talla7;
                            Tallas[7] = obj.orped_talla8;
                            Tallas[8] = obj.orped_talla9;
                            Tallas[9] = obj.orped_talla10;
                            Cantidades[0] = obj.orped_cant_talla1;
                            Cantidades[1] = obj.orped_cant_talla2;
                            Cantidades[2] = obj.orped_cant_talla3;
                            Cantidades[3] = obj.orped_cant_talla4;
                            Cantidades[4] = obj.orped_cant_talla5;
                            Cantidades[5] = obj.orped_cant_talla6;
                            Cantidades[6] = obj.orped_cant_talla7;
                            Cantidades[7] = obj.orped_cant_talla8;
                            Cantidades[8] = obj.orped_cant_talla9;
                            Cantidades[9] = obj.orped_cant_talla10;

                            obj.orpec_iid_orden_pedido = objEOrdenPedido.orpec_iid_orden_pedido;
                            obj.orped_icod_item_orden_pedido = insertarOrdenPedidoDet(obj);
                            addNewItem = true;
                        }
                        if (obj.intTipoOperacion == 2)
                        {
                            object[] Tallas = new object[10];
                            object[] Cantidades = new object[10];


                            Tallas[0] = obj.orped_talla1;
                            Tallas[1] = obj.orped_talla2;
                            Tallas[2] = obj.orped_talla3;
                            Tallas[3] = obj.orped_talla4;
                            Tallas[4] = obj.orped_talla5;
                            Tallas[5] = obj.orped_talla6;
                            Tallas[6] = obj.orped_talla7;
                            Tallas[7] = obj.orped_talla8;
                            Tallas[8] = obj.orped_talla9;
                            Tallas[9] = obj.orped_talla10;
                            Cantidades[0] = obj.orped_cant_talla1;
                            Cantidades[1] = obj.orped_cant_talla2;
                            Cantidades[2] = obj.orped_cant_talla3;
                            Cantidades[3] = obj.orped_cant_talla4;
                            Cantidades[4] = obj.orped_cant_talla5;
                            Cantidades[5] = obj.orped_cant_talla6;
                            Cantidades[6] = obj.orped_cant_talla7;
                            Cantidades[7] = obj.orped_cant_talla8;
                            Cantidades[8] = obj.orped_cant_talla9;
                            Cantidades[9] = obj.orped_cant_talla10;

                            obj.orped_icod_item_orden_pedido = obj.orped_icod_item_orden_pedido;
                            modificarOrdenPedidoDet(obj);
                        }
                    }
                    foreach (var objelimi in listdelete)
                    {
                        EliminarOrdenPedidoDet(objelimi);
                    }
                    #endregion

                    if (addNewItem)
                    {
                        var param = new BAdministracionSistema().listarParametroProduccion().FirstOrDefault();

                        var objCurrentCorrelativo = lstOrdenPedDet.OrderByDescending(x => x.orped_iitem_orden_pedido).FirstOrDefault();

                        param.pmprc_inumero_ficha_tecnica = Convert.ToInt32(objCurrentCorrelativo.orped_vnum_ficha_tecnica);
                        param.strPc = objEOrdenPedido.strPc;
                        param.intUsuario = objEOrdenPedido.intUsuario;
                        new BAdministracionSistema().modificarParametroProduccion(param);
                    }


                    tx.Complete();
                }
                var taskEliminarImagenes = listdelete.Select(x => WebDavTest.EliminarArchivo($"{Rutas.RutaImagenes}{Rutas.RutaOrdenPedido}{objEOrdenPedido.orpec_iid_orden_pedido}/{x.orped_icod_item_orden_pedido}.png"));
                Task.WhenAll(taskEliminarImagenes).Wait();
                var taskGuardarImagenes = lstOrdenPedDet.Where(x => x.imagen2 != null && x.intTipoOperacion == 1 || x.intTipoOperacion == 2).Select(x => WebDavTest.GuardarImagenes(Rutas.RutaImagenes, $"{Rutas.RutaOrdenPedido}{objEOrdenPedido.orpec_iid_orden_pedido}/{x.orped_icod_item_orden_pedido}.png", x.imagen2));
                Task.WhenAll(taskGuardarImagenes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarOrdenPedidoCab(EOrdenPedidoCab objEOrdenPedido)
        {

            List<EOrdenPedidoDet> Mlist = new List<EOrdenPedidoDet>();
            Mlist = ListarOrdenPedidoDet(objEOrdenPedido.orpec_iid_orden_pedido);
             
            foreach (var x in Mlist)
            {
                x.orped_icod_item_orden_pedido = Mlist[0].orped_icod_item_orden_pedido;
                EliminarOrdenPedidoDet(x);
            }

            new CentralData().EliminarOrdenPedidoCab(objEOrdenPedido);
        }
        #endregion
        #region Orden de Pedido Detalle
        public List<EOrdenPedidoDet> ListarOrdenPedidoDet(int intordenpedido, bool conImagen = false)
        {
            List<EOrdenPedidoDet> lista = new List<EOrdenPedidoDet>();
            try
            {
                lista = objCentral.ListarOrdenPedidoDet(intordenpedido);
                if (conImagen)
                    lista.ForEach(x =>
                    {
                        var imagen = WebDavTest.Get($"{Rutas.RutaImagenes}{Rutas.RutaOrdenPedido }{x.orpec_iid_orden_pedido}/", $"{x.orped_icod_item_orden_pedido}.png");
                        if (!(imagen is null))
                            x.imagen2 = Image.FromStream(imagen);
                        imagen = null;
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }



        public int insertarOrdenPedidoDet(EOrdenPedidoDet objEOrdenPedido) => objCentral.insertarOrdenPedidoDet(objEOrdenPedido);

        public void modificarOrdenPedidoDet(EOrdenPedidoDet objEOrdenPedido)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ActualizarOrdenPedidoDet(objEOrdenPedido);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarOrdenPedidoDet(EOrdenPedidoDet objEOrdenPedido)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarOrdenPedidoDet(objEOrdenPedido);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Nota de Pedido Cab
        public List<ENotaPedidoCab> ListarNotaPedidoCab(int intEjericio)
        {
            List<ENotaPedidoCab> lista = new List<ENotaPedidoCab>();
            try
            {
                lista = objCentral.ListarNotaPedidoCab(intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public async Task<int> InsertarNotaPedidoCab(ENotaPedidoCab objEOrdenPedido, List<ENotaPedidoDet> lstOrdenPedDet)
        {


            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {


                    objEOrdenPedido.nopec_iid_nota_pedido = objCentral.InsertarNotaPedidoCab(objEOrdenPedido);

                    if (!WebDavTest.Crearcarpeta(Path.Combine(Rutas.RutaImagenes, Rutas.RutaNotaPedido), objEOrdenPedido.nopec_iid_nota_pedido.ToString()))
                        throw new Exception("Error al Guardar Imagenes en la Nube");

                    if (objEOrdenPedido.nopec_iid_nota_pedido == 0)
                    {
                        throw new Exception("El número de la Nota de Pedido ya fue utilizado, intente con un número de Orden de Pedido superior");
                    }




                    foreach (var obj in lstOrdenPedDet)
                    {
                        object[] Tallas = new object[10];
                        object[] Cantidades = new object[10];

                        Tallas[0] = obj.noped_talla1;
                        Tallas[1] = obj.noped_talla2;
                        Tallas[2] = obj.noped_talla3;
                        Tallas[3] = obj.noped_talla4;
                        Tallas[4] = obj.noped_talla5;
                        Tallas[5] = obj.noped_talla6;
                        Tallas[6] = obj.noped_talla7;
                        Tallas[7] = obj.noped_talla8;
                        Tallas[8] = obj.noped_talla9;
                        Tallas[9] = obj.noped_talla10;
                        Cantidades[0] = obj.noped_cant_talla1;
                        Cantidades[1] = obj.noped_cant_talla2;
                        Cantidades[2] = obj.noped_cant_talla3;
                        Cantidades[3] = obj.noped_cant_talla4;
                        Cantidades[4] = obj.noped_cant_talla5;
                        Cantidades[5] = obj.noped_cant_talla6;
                        Cantidades[6] = obj.noped_cant_talla7;
                        Cantidades[7] = obj.noped_cant_talla8;
                        Cantidades[8] = obj.noped_cant_talla9;
                        Cantidades[9] = obj.noped_cant_talla10;

                        obj.nopec_iid_nota_pedido = objEOrdenPedido.nopec_iid_nota_pedido;
                        obj.noped_icod_item_nota_pedido = objCentral.insertarNotaPedidoDet(obj);



                    };
                    // ACTUALIZAR CORRELATIVO DE ORDEN DE PEDIDO Y FICHA TECNICA
                    EParametroProduccion ob = new EParametroProduccion();
                    var _Be = new BAdministracionSistema().listarParametroProduccion();
                    ob.pmprc_inumero_orden_pedido = _Be[0].pmprc_inumero_orden_pedido;
                    ob.pmprc_inumero_ficha_tecnica = _Be[0].pmprc_inumero_ficha_tecnica;
                    ob.pmprc_inumero_orden_produccion = _Be[0].pmprc_inumero_orden_produccion;
                    ob.pmprc_icod_parametro_produccion = _Be[0].pmprc_icod_parametro_produccion;
                    ob.pmprc_inumero_nota_pedido = Convert.ToInt32(objEOrdenPedido.nopec_icod_nota_pedido);
                    new BAdministracionSistema().modificarCorrelativoOP(ob);

                    tx.Complete();
                }
                var taskGuardarImagenes = lstOrdenPedDet.Where(x => x.imagen2 != null).Select(x => WebDavTest.GuardarImagenes(Rutas.RutaImagenes, $"{Rutas.RutaNotaPedido}{objEOrdenPedido.nopec_iid_nota_pedido}/{x.noped_icod_item_nota_pedido}.png", x.imagen2));
                Task.WhenAll(taskGuardarImagenes);
                return objEOrdenPedido.nopec_iid_nota_pedido;
            }
            catch (Exception ex)
            {
                await WebDavTest.EliminarArchivo($"{Rutas.RutaImagenes}{Rutas.RutaOrdenPedido}{objEOrdenPedido.nopec_iid_nota_pedido}");
                throw ex;
            }
        }


        public async Task modificarNotaPedidoCab(ENotaPedidoCab objEOrdenPedido, List<ENotaPedidoDet> lstOrdenPedDet, List<ENotaPedidoDet> listdelete)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //MODIFICAR LA FACTURA

                    objCentral.ActualizarNotaPedidoCab(objEOrdenPedido);

                    #region Factura Det.

                    foreach (var obj in lstOrdenPedDet)
                    {
                        if (obj.intTipoOperacion == 1)
                        {
                            //insertar kardex
                            object[] Tallas = new object[10];
                            object[] Cantidades = new object[10];


                            Tallas[0] = obj.noped_talla1;
                            Tallas[1] = obj.noped_talla2;
                            Tallas[2] = obj.noped_talla3;
                            Tallas[3] = obj.noped_talla4;
                            Tallas[4] = obj.noped_talla5;
                            Tallas[5] = obj.noped_talla6;
                            Tallas[6] = obj.noped_talla7;
                            Tallas[7] = obj.noped_talla8;
                            Tallas[8] = obj.noped_talla9;
                            Tallas[9] = obj.noped_talla10;
                            Cantidades[0] = obj.noped_cant_talla1;
                            Cantidades[1] = obj.noped_cant_talla2;
                            Cantidades[2] = obj.noped_cant_talla3;
                            Cantidades[3] = obj.noped_cant_talla4;
                            Cantidades[4] = obj.noped_cant_talla5;
                            Cantidades[5] = obj.noped_cant_talla6;
                            Cantidades[6] = obj.noped_cant_talla7;
                            Cantidades[7] = obj.noped_cant_talla8;
                            Cantidades[8] = obj.noped_cant_talla9;
                            Cantidades[9] = obj.noped_cant_talla10;

                            obj.nopec_iid_nota_pedido = objEOrdenPedido.nopec_iid_nota_pedido;

                            obj.noped_icod_item_nota_pedido = objCentral.insertarNotaPedidoDet(obj);
                        }
                        if (obj.intTipoOperacion == 2)
                        {
                            object[] Tallas = new object[10];
                            object[] Cantidades = new object[10];


                            Tallas[0] = obj.noped_talla1;
                            Tallas[1] = obj.noped_talla2;
                            Tallas[2] = obj.noped_talla3;
                            Tallas[3] = obj.noped_talla4;
                            Tallas[4] = obj.noped_talla5;
                            Tallas[5] = obj.noped_talla6;
                            Tallas[6] = obj.noped_talla7;
                            Tallas[7] = obj.noped_talla8;
                            Tallas[8] = obj.noped_talla9;
                            Tallas[9] = obj.noped_talla10;
                            Cantidades[0] = obj.noped_cant_talla1;
                            Cantidades[1] = obj.noped_cant_talla2;
                            Cantidades[2] = obj.noped_cant_talla3;
                            Cantidades[3] = obj.noped_cant_talla4;
                            Cantidades[4] = obj.noped_cant_talla5;
                            Cantidades[5] = obj.noped_cant_talla6;
                            Cantidades[6] = obj.noped_cant_talla7;
                            Cantidades[7] = obj.noped_cant_talla8;
                            Cantidades[8] = obj.noped_cant_talla9;
                            Cantidades[9] = obj.noped_cant_talla10;

                            obj.noped_icod_item_nota_pedido = obj.noped_icod_item_nota_pedido;
                            modificarNotaPedidoDet(obj);
                        }
                    }
                    foreach (var objelimi in listdelete)
                    {
                        EliminarNotaPedidoDet(objelimi);
                    }
                    #endregion

                    tx.Complete();
                    var taskEliminarImagenes = listdelete.Select(x => WebDavTest.EliminarArchivo($"{Rutas.RutaImagenes}{Rutas.RutaNotaPedido}{objEOrdenPedido.nopec_iid_nota_pedido}/{x.noped_icod_item_nota_pedido}.png"));
                    Task.WhenAll(taskEliminarImagenes).Wait();
                    var taskGuardarImagenes = lstOrdenPedDet.Where(x => x.imagen2 != null && x.intTipoOperacion == 1 || x.intTipoOperacion == 2).Select(x => WebDavTest.GuardarImagenes(Rutas.RutaImagenes, $"{Rutas.RutaNotaPedido}{objEOrdenPedido.nopec_iid_nota_pedido}/{x.noped_icod_item_nota_pedido}.png", x.imagen2));
                    Task.WhenAll(taskGuardarImagenes);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarNotaPedidoCab(ENotaPedidoCab objEOrdenPedido)
        {

            List<ENotaPedidoDet> Mlist = new List<ENotaPedidoDet>();
            Mlist = ListarNotaPedidoDet(objEOrdenPedido.nopec_iid_nota_pedido);
            foreach (var x in Mlist)
            {
                x.noped_icod_item_nota_pedido = Mlist[0].noped_icod_item_nota_pedido;
                EliminarNotaPedidoDet(x);
            }

            new CentralData().EliminarNotaPedidoCab(objEOrdenPedido);
        }
        #endregion
        #region Nota de Pedido Detalle
        public List<ENotaPedidoDet> ListarNotaPedidoDet(int intordenpedido)
        {
            List<ENotaPedidoDet> lista = new List<ENotaPedidoDet>();
            try
            {
                lista = objCentral.ListarNotaPedidoDet(intordenpedido);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public void modificarNotaPedidoDet(ENotaPedidoDet objEOrdenPedido)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ActualizarNotaPedidoDet(objEOrdenPedido);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarNotaPedidoDet(ENotaPedidoDet objEOrdenPedido)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarNotaPedidoDet(objEOrdenPedido);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Ficha Tecnica
        public List<EFichaTecnicaCab> ListarFichaTecnicaCab(int intEjericio)
        {
            List<EFichaTecnicaCab> lista = new List<EFichaTecnicaCab>();
            try
            {
                lista = objCentral.ListarFichaTecnicaCab(intEjericio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int InsertarFichaTecnicaCab(EFichaTecnicaCab objEFichaTecnica, List<EFichaTecnicaDet> lstOrdenPedDet)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {


                    objEFichaTecnica.fitec_iid_ficha_tecnica = objCentral.InsertarFichaTecnicaCab(objEFichaTecnica);

                    if (objEFichaTecnica.fitec_iid_ficha_tecnica == 0)
                    {
                        throw new Exception("El número de la Ficha Tecnica ya fue utilizado, intente con un número de Ficha Tecnica superior");
                    }
                    //CREAMOS SU CARPETA EN LA NUBE

                    if (!WebDavTest.Crearcarpeta(Path.Combine(Rutas.RutaImagenes, Rutas.RutaFichaTecnica), objEFichaTecnica.fitec_iid_ficha_tecnica.ToString()))
                        throw new Exception("Error al Guardar Imagenes en la Nube");
                    if (!WebDavTest.Put($"{Rutas.RutaImagenes}{Rutas.RutaFichaTecnica}{objEFichaTecnica.fitec_iid_ficha_tecnica}/", $"{objEFichaTecnica.fitec_iid_ficha_tecnica.ToString()}.png", objEFichaTecnica.imagen))
                        throw new Exception("Error al Guardar Imagenes en la Nube");
                    foreach (var obj in lstOrdenPedDet)
                    {
                        obj.fitec_iid_ficha_tecnica = objEFichaTecnica.fitec_iid_ficha_tecnica;
                        insertarFichaTecnicaDet(obj);

                    };
                    tx.Complete();
                }
                return objEFichaTecnica.fitec_iid_ficha_tecnica;
            }
            catch (Exception ex)
            {
                WebDavTest.EliminarArchivo($"{Rutas.RutaImagenes}{Rutas.RutaFichaTecnica}{objEFichaTecnica.fitec_iid_ficha_tecnica}");
                throw ex;
            }
        }
        public void modificarFichaTecnicaCab(EFichaTecnicaCab objEFichaTecnica, List<EFichaTecnicaDet> lstOrdenPedDet, List<EFichaTecnicaDet> lstdelete)
        {
            try
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //MODIFICAR LA FACTURA

                    objCentral.ActualizarFichaTecnicaCab(objEFichaTecnica);

                    #region Factura Det.

                    foreach (var obj in lstOrdenPedDet)
                    {

                        if (obj.intTipoOperacion == 1)
                        {
                            obj.fitec_iid_ficha_tecnica = objEFichaTecnica.fitec_iid_ficha_tecnica;
                            insertarFichaTecnicaDet(obj);
                        }
                        else
                        {
                            obj.fitec_iid_ficha_tecnica = objEFichaTecnica.fitec_iid_ficha_tecnica;
                            modificarFichaTecnicaDet(obj);
                        }

                    }
                    foreach (var ob in lstdelete)
                    {

                        EliminarFichaTecnicaDet(ob);
                    }
                    #endregion

                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarFichaTecnicaCab(EFichaTecnicaCab objEFichaTecnica)
        {

            List<EFichaTecnicaDet> Mlist = new List<EFichaTecnicaDet>();
            Mlist = ListarFichaTecnicaDet(objEFichaTecnica.fitec_iid_ficha_tecnica);
            foreach (var x in Mlist)
            {
                x.fited_icod_item_ficha_tecnica = Mlist[0].fited_icod_item_ficha_tecnica;
                EliminarFichaTecnicaDet(x);
            }

            new CentralData().EliminarFichaTecnicaCab(objEFichaTecnica);
        }
        #endregion
        #region Ficha Tecnica
        public List<EFichaTecnicaDet> ListarFichaTecnicaDet(int intordenpedido)
        {
            List<EFichaTecnicaDet> lista = new List<EFichaTecnicaDet>();
            try
            {
                lista = objCentral.ListarFichaTecnicaDet(intordenpedido);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarFichaTecnicaDet(EFichaTecnicaDet objEFichaTecnica)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.insertarFichaTecnicaDet(objEFichaTecnica);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFichaTecnicaDet(EFichaTecnicaDet objEFichaTecnica)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ActualizarFichaTecnicaDet(objEFichaTecnica);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarFichaTecnicaDet(EFichaTecnicaDet objEFichaTecnica)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarFichaTecnicaDet(objEFichaTecnica);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Ficha Tecnica Materiales
        public List<EFichaTecnicaMateriales> ListarFichaTecnicaMateriales(int mo_icod_modelo)
        {
            List<EFichaTecnicaMateriales> lista = new List<EFichaTecnicaMateriales>();
            try
            {
                lista = objCentral.ListarFichaTecnicaMateriales(mo_icod_modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarFichaTecnicaMateriales(EFichaTecnicaMateriales objEFichaTecnica)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new CentralData().insertarFichaTecnicaMateriales(objEFichaTecnica);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFichaTecnicaMateriales(EFichaTecnicaMateriales objEFichaTecnica)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ActualizarFichaTecnicaMateriales(objEFichaTecnica);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarFichaTecnicaMateriales(EFichaTecnicaMateriales objEFichaTecnica)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarFichaTecnicaMateriales(objEFichaTecnica);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Plantilla Materiales
        public List<EPlantillaMateriales> ListarPlantillaMateriales(int mo_icod_modelo)
        {
            List<EPlantillaMateriales> lista = new List<EPlantillaMateriales>();
            try
            {
                lista = objCentral.ListarPlantillaMateriales(mo_icod_modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarPlantillaMateriales(EPlantillaMateriales objEFichaTecnica)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new CentralData().insertarPlantillaMateriales(objEFichaTecnica);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPlantillaMateriales(EPlantillaMateriales objEFichaTecnica)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ActualizarPlantillaMateriales(objEFichaTecnica);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarPlantillaMateriales(EPlantillaMateriales objEFichaTecnica)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarPlantillaMateriales(objEFichaTecnica);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Requerimiento Material
        public List<ERequerimientoMaterial> ListarRequerimientoMaterial(int Ejercicio)
        {
            List<ERequerimientoMaterial> lista = null;
            try
            {
                lista = objCentral.ListarRequerimientoMaterial(Ejercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int InsertarRequerimientoMaterial(ERequerimientoMaterial obj, List<ERequerimientoMaterialDet> objdetalle)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    ////cabecera------

                    intIcod = new CentralData().InsertarRequerimientoMaterial(obj);
                    if (intIcod == 0)
                    {
                        throw new Exception("El número de la Orde de Producción ya fue utilizado, intente con un número de Orden de Producción superior");
                    }
                    ///detalle-------

                    foreach (var objd in objdetalle)
                    {
                        objd.rqmac_iid_requerimiento_material = intIcod;
                        insertarRequerimientoMaterialDetalle(objd);
                    }

                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ModificarRequerimientoMaterial(ERequerimientoMaterial obj, List<ERequerimientoMaterialDet> objdetalle, List<ERequerimientoMaterialDet> objdelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ModificarRequerimientoMaterial(obj);
                    foreach (var objd in objdetalle)
                    {
                        if (objd.intTipoOperacion == 1)
                        {
                            objd.rqmac_iid_requerimiento_material = obj.rqmac_iid_requerimiento_material;
                            insertarRequerimientoMaterialDetalle(objd);
                        }
                        else if (objd.intTipoOperacion == 2)
                        {
                            modificarRequerimientoMaterialDetalle(objd);
                        }
                    }

                    foreach (var objelimi in objdelete)
                    {
                        eliminarRequerimientoMaterialDetalle(objelimi);
                    }
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ModificarEntregaMaterial(ERequerimientoMaterial obj, List<ERequerimientoMaterialDet> objdetalle, List<EEntregaMaterialDet> objentregadetalle, List<EEntregaMaterialDet> lstdelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ModificarRequerimientoMaterial(obj);
                    objdetalle.ForEach(objd =>
                    {
                        objentregadetalle.ForEach(x =>
                        {
                            if (objd.rqmad_icod_item_requerimiento_material == x.rqmad_icod_item_requerimiento_material)
                            {
                                if (x.rqmed_ncantidad_entregada > 0 && x.rqmed_kardc_icod_correlativo == 0)
                                {
                                    EKardex obkardex = new EKardex();
                                    obkardex.kardc_ianio = Parametros.intEjercicio;
                                    obkardex.kardc_fecha_movimiento = x.rqmed_sfecha_entrega;
                                    obkardex.almac_icod_almacen = obj.rqmac_iid_almacen;
                                    obkardex.prdc_icod_producto = objd.prdc_icod_producto;
                                    obkardex.kardc_icantidad_prod = x.rqmed_ncantidad_entregada;
                                    obkardex.tdocc_icod_tipo_doc = 124;
                                    obkardex.kardc_numero_doc = obj.rqmac_icod_requerimiento_material;
                                    obkardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                                    obkardex.kardc_iid_motivo = 680;
                                    obkardex.kardc_beneficiario = "OP" + obj.rqmac_iorden_produccion + " " + obj.stroperario;
                                    obkardex.kardc_observaciones = "ENTREGA DE MATERIALES";
                                    obkardex.intUsuario = obj.intUsuario;
                                    obkardex.strPc = obj.strPc;
                                    x.rqmed_kardc_icod_correlativo = objAlmacenData.insertarKardex(obkardex);
                                    x.rqmad_icod_item_requerimiento_material = objd.rqmad_icod_item_requerimiento_material;
                                    x.rqmac_iid_requerimiento_material = obj.rqmac_iid_requerimiento_material;
                                    insertarEntregaMaterialDetalle(x);

                                    EStock objStock = new EStock()
                                    {
                                        stocc_ianio = Parametros.intEjercicio,
                                        almac_icod_almacen = obj.rqmac_iid_almacen,
                                        prdc_icod_producto = objd.prdc_icod_producto,
                                        stocc_stock_producto = x.rqmed_ncantidad_entregada,
                                        intTipoMovimiento = obkardex.kardc_tipo_movimiento
                                    };
                                    objAlmacenData.actualizarStock(objStock);
                                }


                            }
                        });
                        lstdelete.ForEach(b =>
                        {
                            if (objd.rqmad_icod_item_requerimiento_material == b.rqmad_icod_item_requerimiento_material)
                            {
                                EKardex obKardex = new EKardex();
                                obKardex.kardc_icod_correlativo = b.rqmed_kardc_icod_correlativo;
                                obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                                obKardex.intUsuario = b.intUsuario;
                                obKardex.strPc = b.strPc;

                                if (obKardex.kardc_icod_correlativo > 0)
                                {
                                    /*-----eliminar el kardex y stock-----------------------------*/
                                    objAlmacenData.eliminarKardex(obKardex);
                                    eliminarEntregaMaterialDetalle(b);
                                }
                            }
                        });

                        objd.rqmad_icod_item_requerimiento_material = objd.rqmad_icod_item_requerimiento_material;
                        ActualizarEntregaMaterialDetalle(objd);



                        //else if (objd.rqmad_ncantidad_entregada == 0 && objd.rqmad_kardc_icod_correlativo > 0)
                        //{
                        //    EKardex obKardex = new EKardex();
                        //    obKardex.kardc_icod_correlativo = objd.rqmad_kardc_icod_correlativo;
                        //    obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                        //    obKardex.intUsuario = objd.intUsuario;
                        //    obKardex.strPc = objd.strPc;

                        //    if (obKardex.kardc_icod_correlativo > 0)
                        //    {
                        //        /*-----eliminar el kardex y stock-----------------------------*/
                        //        objAlmacenData.eliminarKardex(obKardex);
                        //        objd.rqmad_kardc_icod_correlativo = 0;
                        //        ActualizarEntregaMaterialDetalle(objd);
                        //    }
                        //}
                        //else if (objd.rqmad_ncantidad_entregada > 0 && objd.rqmad_kardc_icod_correlativo > 0)
                        //{

                        //    EKardex obkardex = new EKardex();
                        //    obkardex.kardc_icod_correlativo = objd.rqmad_kardc_icod_correlativo;
                        //    obkardex.kardc_ianio = Parametros.intEjercicio;
                        //    obkardex.kardc_fecha_movimiento = obj.rqmac_sfecha_requerimiento_material;
                        //    obkardex.almac_icod_almacen = obj.rqmac_iid_almacen;
                        //    obkardex.prdc_icod_producto = objd.prdc_icod_producto;
                        //    obkardex.kardc_icantidad_prod = objd.rqmad_ncantidad_entregada;
                        //    obkardex.tdocc_icod_tipo_doc = 124;
                        //    obkardex.kardc_numero_doc = obj.rqmac_icod_requerimiento_material;
                        //    obkardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                        //    obkardex.kardc_iid_motivo = 680;
                        //    obkardex.kardc_beneficiario = "OP" + obj.rqmac_iorden_produccion + " " + obj.stroperario;
                        //    obkardex.kardc_observaciones = "Entrega de Materiales";
                        //    obkardex.intUsuario = obj.intUsuario;
                        //    obkardex.strPc = obj.strPc;
                        //    objAlmacenData.modificarKardex(obkardex);
                        //    objd.rqmad_icod_item_requerimiento_material = objd.rqmad_icod_item_requerimiento_material;
                        //    ActualizarEntregaMaterialDetalle(objd);

                        //    EStock objStock = new EStock()
                        //    {
                        //        stocc_ianio = Parametros.intEjercicio,
                        //        almac_icod_almacen = obj.rqmac_iid_almacen,
                        //        prdc_icod_producto = objd.prdc_icod_producto,
                        //        stocc_stock_producto = objd.rqmad_ncantidad_entregada,
                        //        intTipoMovimiento = obkardex.kardc_tipo_movimiento
                        //    };
                        //    objAlmacenData.actualizarStock(objStock);
                        //}

                    });
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ModificarDevolucionMaterial(ERequerimientoMaterial obj, List<ERequerimientoMaterialDet> objdetalle)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ModificarRequerimientoMaterial(obj);
                    objdetalle.ForEach(objd =>
                    {
                        if (objd.rqmad_ncantidad_devuelta > 0 && objd.rqmad_kardc_icod_correlativo == 0)
                        {
                            EKardex obkardex = new EKardex();
                            obkardex.kardc_ianio = Parametros.intEjercicio;
                            obkardex.kardc_fecha_movimiento = obj.rqmac_sfecha_requerimiento_material;
                            obkardex.almac_icod_almacen = obj.rqmac_iid_almacen;
                            obkardex.prdc_icod_producto = objd.prdc_icod_producto;
                            obkardex.kardc_icantidad_prod = objd.rqmad_ncantidad_devuelta;
                            obkardex.tdocc_icod_tipo_doc = 124;
                            obkardex.kardc_numero_doc = obj.rqmac_icod_requerimiento_material;
                            obkardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                            obkardex.kardc_iid_motivo = 681;
                            obkardex.kardc_beneficiario = "OP" + obj.rqmac_iorden_produccion + " " + obj.stroperario;
                            obkardex.kardc_observaciones = "Devolución de Materiales";
                            obkardex.intUsuario = obj.intUsuario;
                            obkardex.strPc = obj.strPc;
                            objd.rqmad_kardc_icod_correlativo = objAlmacenData.insertarKardex(obkardex);
                            objd.rqmad_icod_item_requerimiento_material = objd.rqmad_icod_item_requerimiento_material;
                            ActualizarDevolucionMaterialDetalle(objd);

                            EStock objStock = new EStock()
                            {
                                stocc_ianio = Parametros.intEjercicio,
                                almac_icod_almacen = obj.rqmac_iid_almacen,
                                prdc_icod_producto = objd.prdc_icod_producto,
                                stocc_stock_producto = objd.rqmad_ncantidad_devuelta,
                                intTipoMovimiento = obkardex.kardc_tipo_movimiento
                            };
                            objAlmacenData.actualizarStock(objStock);
                        }
                        else if (objd.rqmad_ncantidad_devuelta == 0 && objd.rqmad_kardc_icod_correlativo > 0)
                        {
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_icod_correlativo = objd.rqmad_kardc_icod_correlativo;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.intUsuario = objd.intUsuario;
                            obKardex.strPc = objd.strPc;

                            if (obKardex.kardc_icod_correlativo > 0)
                            {
                                /*-----eliminar el kardex y stock-----------------------------*/
                                objAlmacenData.eliminarKardex(obKardex);
                                objd.rqmad_kardc_icod_correlativo = 0;
                                ActualizarDevolucionMaterialDetalle(objd);
                            }
                        }
                        else if (objd.rqmad_ncantidad_devuelta > 0 && objd.rqmad_kardc_icod_correlativo > 0)
                        {

                            EKardex obkardex = new EKardex();
                            obkardex.kardc_icod_correlativo = objd.rqmad_kardc_icod_correlativo;
                            obkardex.kardc_ianio = Parametros.intEjercicio;
                            obkardex.kardc_fecha_movimiento = obj.rqmac_sfecha_requerimiento_material;
                            obkardex.almac_icod_almacen = obj.rqmac_iid_almacen;
                            obkardex.prdc_icod_producto = objd.prdc_icod_producto;
                            obkardex.kardc_icantidad_prod = objd.rqmad_ncantidad_devuelta;
                            obkardex.tdocc_icod_tipo_doc = 124;
                            obkardex.kardc_numero_doc = obj.rqmac_icod_requerimiento_material;
                            obkardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                            obkardex.kardc_iid_motivo = 681;
                            obkardex.kardc_beneficiario = "OP" + obj.rqmac_iorden_produccion + " " + obj.stroperario;
                            obkardex.kardc_observaciones = "Entrega de Materiales";
                            obkardex.intUsuario = obj.intUsuario;
                            obkardex.strPc = obj.strPc;
                            objAlmacenData.modificarKardex(obkardex);
                            objd.rqmad_icod_item_requerimiento_material = objd.rqmad_icod_item_requerimiento_material;
                            ActualizarDevolucionMaterialDetalle(objd);

                            EStock objStock = new EStock()
                            {
                                stocc_ianio = Parametros.intEjercicio,
                                almac_icod_almacen = obj.rqmac_iid_almacen,
                                prdc_icod_producto = objd.prdc_icod_producto,
                                stocc_stock_producto = objd.rqmad_ncantidad_devuelta,
                                intTipoMovimiento = obkardex.kardc_tipo_movimiento
                            };
                            objAlmacenData.actualizarStock(objStock);
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
        public void EliminarRequerimientoMaterial(ERequerimientoMaterial obj)
        {
            List<ERequerimientoMaterialDet> Mlist = new List<ERequerimientoMaterialDet>();
            Mlist = listarRequerimientoMaterialDetalle(obj.rqmac_iid_requerimiento_material);
            foreach (var x in Mlist)
            {
                x.rqmad_icod_item_requerimiento_material = Mlist[0].rqmad_icod_item_requerimiento_material;
                eliminarRequerimientoMaterialDetalle(x);
            }

            new CentralData().EliminarRequerimientoMaterial(obj);

        }
        #endregion
        #region Requerimiento Material Detalle
        public List<ERequerimientoMaterialDet> listarRequerimientoMaterialDetalle(int intOrdenPrduccion)
        {
            List<ERequerimientoMaterialDet> lista = new List<ERequerimientoMaterialDet>();
            try
            {
                lista = objCentralData.listarRequerimientoMaterialDetalle(intOrdenPrduccion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EEntregaMaterialDet> listarEntregaMaterialDetalle(int intOrdenPrduccion)
        {
            List<EEntregaMaterialDet> lista = new List<EEntregaMaterialDet>();
            try
            {
                lista = objCentralData.listarEntregaMaterialDetalle(intOrdenPrduccion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarRequerimientoMaterialDetalle(ERequerimientoMaterialDet objEOrdenPrduccion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.insertarRequerimientoMaterialDetalle(objEOrdenPrduccion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void insertarEntregaMaterialDetalle(EEntregaMaterialDet objEOrdenPrduccion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.insertarEntregaMaterialDetalle(objEOrdenPrduccion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarRequerimientoMaterialDetalle(ERequerimientoMaterialDet objEOrdenPrduccion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.modificarRequerimientoMaterialDetalle(objEOrdenPrduccion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarEntregaMaterialDetalle(ERequerimientoMaterialDet objEOrdenPrduccion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.ActualizarEntregaMaterialDetalle(objEOrdenPrduccion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarDevolucionMaterialDetalle(ERequerimientoMaterialDet objEOrdenPrduccion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.ActualizarDevolucionMaterialDetalle(objEOrdenPrduccion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarRequerimientoMaterialDetalle(ERequerimientoMaterialDet objEOrdenPrduccion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.eliminarRequerimientoMaterialDetalle(objEOrdenPrduccion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarEntregaMaterialDetalle(EEntregaMaterialDet objEOrdenPrduccion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentralData.eliminarEntregaMaterialDetalle(objEOrdenPrduccion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Registro Personal
        public List<EPVTPersonal> ListarRegistroPersonal()
        {
            List<EPVTPersonal> lista = null;
            try
            {
                lista = objCentral.ListarRegistroPersonal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int InsertarRegistroPersonal(EPVTPersonal obj)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    intIcod = new CentralData().InsertarRegistroPersonal(obj);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ModificarRegistroPersonal(EPVTPersonal obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.ActualizarRegistroPersonal(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarRegistroPersonal(EPVTPersonal obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCentral.EliminarRegistroPersonal(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        public void EliminarPTKardex(EOrdenProduccion objEReporteProduccion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    EProdKardex ekar = new EProdKardex();
                    BCentral oblKardex = new BCentral();

                    int[] icod_karde = new int[10];
                    long[] talla = new long[10];
                    object[] idProducto = new object[10];
                    icod_karde[0] = Convert.ToInt32(objEReporteProduccion.orprc_iid_kardex1);
                    icod_karde[1] = Convert.ToInt32(objEReporteProduccion.orprc_iid_kardex2);
                    icod_karde[2] = Convert.ToInt32(objEReporteProduccion.orprc_iid_kardex3);
                    icod_karde[3] = Convert.ToInt32(objEReporteProduccion.orprc_iid_kardex4);
                    icod_karde[4] = Convert.ToInt32(objEReporteProduccion.orprc_iid_kardex5);
                    icod_karde[5] = Convert.ToInt32(objEReporteProduccion.orprc_iid_kardex6);
                    icod_karde[6] = Convert.ToInt32(objEReporteProduccion.orprc_iid_kardex7);
                    icod_karde[7] = Convert.ToInt32(objEReporteProduccion.orprc_iid_kardex8);
                    icod_karde[8] = Convert.ToInt32(objEReporteProduccion.orprc_iid_kardex9);
                    icod_karde[9] = Convert.ToInt32(objEReporteProduccion.orprc_iid_kardex10);
                    idProducto[0] = objEReporteProduccion.orprc_icod_producto1;
                    idProducto[1] = objEReporteProduccion.orprc_icod_producto2;
                    idProducto[2] = objEReporteProduccion.orprc_icod_producto3;
                    idProducto[3] = objEReporteProduccion.orprc_icod_producto4;
                    idProducto[4] = objEReporteProduccion.orprc_icod_producto5;
                    idProducto[5] = objEReporteProduccion.orprc_icod_producto6;
                    idProducto[6] = objEReporteProduccion.orprc_icod_producto7;
                    idProducto[7] = objEReporteProduccion.orprc_icod_producto8;
                    idProducto[8] = objEReporteProduccion.orprc_icod_producto9;
                    idProducto[9] = objEReporteProduccion.orprc_icod_producto10;
                    for (int j = 0; j <= 9; j++)
                    {
                        //KARDEX
                        ekar.kardc_ianio = Parametros.intEjercicio;
                        ekar.kardx_sfecha = DateTime.Today;
                        ekar.iid_almacen = Convert.ToInt32(objEReporteProduccion.orprc_iid_almacen);
                        ekar.iid_producto = Convert.ToInt32(idProducto[j]);
                        ekar.movimiento = 1;
                        ekar.operacion = 3;
                        ekar.kardc_iid_correlativo = icod_karde[j];
                        ekar.intUsuario = objEReporteProduccion.intUsuario;
                        if (ekar.iid_producto > 0)
                        {
                            oblKardex.EliminarKardexPvt(ekar);
                        }
                        EProdStockProducto objStock = new EProdStockProducto()
                        {
                            stocc_ianio = Parametros.intEjercicio,
                            stocc_iid_almacen = ekar.iid_almacen,
                            stocc_iid_producto = ekar.iid_producto,
                            stocc_nstock_prod = 0,

                        };
                        oblKardex.actualizarStockPvt(objStock);
                    }


                    //Actualiza el reporte de produccion con el kardex generado
                    objEReporteProduccion.orprc_iid_kardex1 = 0;
                    objEReporteProduccion.orprc_iid_kardex2 = 0;
                    objEReporteProduccion.orprc_iid_kardex3 = 0;
                    objEReporteProduccion.orprc_iid_kardex4 = 0;
                    objEReporteProduccion.orprc_iid_kardex5 = 0;
                    objEReporteProduccion.orprc_iid_kardex6 = 0;
                    objEReporteProduccion.orprc_iid_kardex7 = 0;
                    objEReporteProduccion.orprc_iid_kardex8 = 0;
                    objEReporteProduccion.orprc_iid_kardex9 = 0;
                    objEReporteProduccion.orprc_iid_kardex10 = 0;
                    objEReporteProduccion.orprc_sfecha_ing_kardex = null;
                    objCentralData.ActualizarPVTOrdenProduccionKardex(objEReporteProduccion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EProdKardex> listarStockConsolidadoxAlmacen(int intEjercicio)
        {
            List<EProdKardex> lista = null;
            try
            {
                lista = objCentral.listarStockConsolidadoxAlmacen(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void cierreAnualAlmacenes(DataTable Mlist)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Borrar el pase realizado anteriormente
                    objCentral.DeleteCierreAnualAlmacenes(Parametros.intEjercicio + 1);
                    objCentral.InsetarMasivoKardexb(Mlist);
                    new CentralData().stockActualizarConKardex(Parametros.intEjercicio + 1);

                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Obtener_Kardex_Max_Correlativo()
        {
            int lista = 0;
            try
            {
                lista = objCentral.Obtener_Kardex_Max_Correlativo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EProdKardex> listarAlmacenSaldoInicial(int intEjercicio)
        {
            List<EProdKardex> lista = null;
            try
            {
                lista = objCentral.listarAlmacenSaldoInicial(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }


    }
}
