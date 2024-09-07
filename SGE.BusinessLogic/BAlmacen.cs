using SGE.Common;
using SGE.DataAccesDapper.Modules;
using SGE.DataAccess;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Transactions;

namespace SGE.BusinessLogic
{
    public class BAlmacen
    {
        AlmacenData objAlmacenData = new AlmacenData();
        private readonly AlmacenService _almacenService;
        public BAlmacen()
        {
            _almacenService = new AlmacenService();
        }

        public int StockProductoPorAnio(int stocc_ianio, int stocc_iid_producto)
        {
            return _almacenService.StockProductoPorAnio(stocc_ianio, stocc_iid_producto);
        }

        public EProdProducto ProductoPvtGetByCode(string pr_vcodigo_externo)
        {
            return _almacenService.ProductoPvtGetByCode(pr_vcodigo_externo);
        }

        #region Almacenes
        public List<EAlmacen> listarAlmacenes()
        {
            List<EAlmacen> lista = null;
            try
            {
                lista = new AlmacenData().listarAlmacenes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EKardex> listarkardex()
        {
            List<EKardex> lista = null;
            try
            {
                lista = new AlmacenData().listarkardex();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EAlmacen> listarAlmacenesGR()
        {
            List<EAlmacen> lista = null;
            try
            {
                lista = new AlmacenData().listarAlmacenesGR();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EAlmacen> listarAlmacenesProyectos()
        {
            List<EAlmacen> lista = null;
            try
            {
                lista = new AlmacenData().listarAlmacenesProyectos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarAlmacen(EAlmacen oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new AlmacenData().insertarAlmacen(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().modificarAlmacen(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EntregaMaterialesGuardar(EEntregaMaterial obj, ERequerimientoMaterialDet objItem)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    EKardex obKardex = new EKardex();
                    obKardex.kardc_icod_correlativo = objItem.rqmad_kardc_icod_correlativo;
                    obKardex.kardc_ianio = obj.em_sfecha_entrega.Year;
                    obKardex.kardc_fecha_movimiento = obj.em_sfecha_entrega;
                    obKardex.almac_icod_almacen = 120;
                    obKardex.prdc_icod_producto = obj.prdc_icod_producto;
                    obKardex.kardc_icantidad_prod = obj.em_ncantidad;
                    obKardex.tdocc_icod_tipo_doc = 125;
                    obKardex.kardc_numero_doc = objItem.NumeroEntrega;
                    obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                    obKardex.kardc_iid_motivo = 194;
                    obKardex.kardc_beneficiario = obj.NombrePersonal;
                    obKardex.kardc_observaciones = "Entrega Material";
                    obKardex.intUsuario = obj.intUsuario;
                    obKardex.strPc = obj.strPc;
                    obj.IdKardex = objItem.rqmad_kardc_icod_correlativo;
                    /*------------------------------------------------------------*/
                    if (objItem.rqmad_kardc_icod_correlativo == 0)
                        obj.IdKardex = objAlmacenData.insertarKardex(obKardex);
                    else
                        objAlmacenData.modificarKardex(obKardex);
                    EStock objStock = new EStock()
                    {
                        stocc_ianio = obKardex.kardc_ianio,
                        almac_icod_almacen = 120,
                        prdc_icod_producto = obj.prdc_icod_producto,

                        stocc_stock_producto = obKardex.kardc_icantidad_prod,
                        intTipoMovimiento = obKardex.kardc_tipo_movimiento
                    };
                    objAlmacenData.actualizarStock(objStock);
                    objAlmacenData.EntregaMaterialesGuardar(obj);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().eliminarAlmacen(oBe);
                    tx.Complete();
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
            List<EUnidadMedida> lista = null;
            try
            {
                lista = new AlmacenData().listarUnidadMedida();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarUnidadMedida(EUnidadMedida oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new AlmacenData().insertarUnidadMedida(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EEntregaMaterial EntregaMaterialesGeyById(int icod_entrega_material)
        {
            return objAlmacenData.EntregaMaterialesGeyById(icod_entrega_material);
        }

        public void modificarUnidadMedida(EUnidadMedida oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().modificarUnidadMedida(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().eliminarUnidadMedida(oBe);
                    tx.Complete();
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
            List<EFamiliaCab> lista = null;
            try
            {
                lista = new AlmacenData().listarFamiliaCab();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarFamiliaCab(EFamiliaCab oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new AlmacenData().insertarFamiliaCab(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().modificarFamiliaCab(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().eliminarFamiliaCab(oBe);
                    tx.Complete();
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
            List<EFamiliaDet> lista = null;
            try
            {
                lista = new AlmacenData().listarFamiliaDet(intIcodFamiliaCab);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarFamiliaDet(EFamiliaDet oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new AlmacenData().insertarFamiliaDet(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().modificarFamiliaDet(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().eliminarFamiliaDet(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Producto
        public void ModificarProductoLista(List<EProducto> mlist)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //MODIFICAMOS EL MOVIMIENTO DEL KARDEX VAL.
                    foreach (var _b in mlist)
                    {
                        if (_b.intOperacion == 2)
                        {
                            new AlmacenData().modificarProducto(_b);
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
        public List<EStock> listaricodstock()
        {
            List<EStock> lista = null;
            try
            {
                lista = new AlmacenData().listaricodstock();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void modificaricotstock(EStock oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().modificaricodstock(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EProducto> listarProducto(int anio)
        {
            List<EProducto> lista = null;
            try
            {
                lista = new AlmacenData().listarProducto(anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProducto> listarProductoCodigoDescrip(int anio, string codigo, string descripcion)
        {
            List<EProducto> lista = null;
            try
            {
                lista = new AlmacenData().listarProductoCodigoDescrip(anio, codigo, descripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProducto> listarProductoXCodigp(int anio, string prdc_vcode_producto)
        {
            List<EProducto> lista = null;
            try
            {
                lista = new AlmacenData().listarProductoXCodigp(anio, prdc_vcode_producto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProducto> listarProductoPrecios(int intEjercicio)
        {
            List<EProducto> lista = null;
            try
            {
                lista = new AlmacenData().listarProductoPrecios(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocCompraDet> listarProductoPreciosDetalle(int intProducto, int intEjercicio)
        {
            List<EDocCompraDet> lista = null;
            try
            {
                lista = new AlmacenData().listarProductoPreciosDetalle(intProducto, intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int getCorrelativoProducto(int intFamilia)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new AlmacenData().getCorrelativoProducto(intFamilia);
                    tx.Complete();
                }
                return intIcod + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarProducto(EProducto oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new AlmacenData().insertarProducto(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().modificarProducto(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().eliminarProducto(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EProducto> VerificarProducto(string code)
        {
            List<EProducto> lista = null;
            try
            {
                lista = new AlmacenData().VerificarProducto(code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;

        }
        #endregion

        #region Nota de Ingreso
        public List<ENotaIngreso> listarNotaIngreso(int intEjercicio, int intAlmacen, DateTime? dtFechaDesde, DateTime? dtFechaHasta)
        {
            List<ENotaIngreso> lista = null;
            try
            {
                lista = objAlmacenData.listarNotaIngreso(intEjercicio, intAlmacen, dtFechaDesde, dtFechaHasta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarNotaIngreso(ENotaIngreso oBe, List<ENotaIngresoDetalle> lstDetalle)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAlmacenData.insertarNotaIngreso(oBe);
                    var lstTipoDocAux = new BAdministracionSistema().listarTipoDocumento().Where(y => y.tdocc_vabreviatura_tipo_doc.Trim() == "N/I").ToList();
                    if (lstTipoDocAux.Count == 0)
                        throw new ArgumentException("Tipo de documento no registrado N/I");
                    lstDetalle.ForEach(x =>
                    {

                        /*---------------ingreso al kardex---------------------------*/
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_ianio = oBe.ningc_ianio;
                        obKardex.kardc_fecha_movimiento = oBe.ningc_fecha_nota_ingreso;
                        obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                        obKardex.prdc_icod_producto = x.prdc_icod_producto;
                        if (Convert.ToInt32(oBe.fcoc_icod_doc) == 0)
                            obKardex.kardc_icantidad_prod = x.dninc_cantidad;
                        else
                            obKardex.kardc_icantidad_prod = x.dnicc_ncantidad_recibida;
                        obKardex.tdocc_icod_tipo_doc = lstTipoDocAux[0].tdocc_icod_tipo_doc;
                        obKardex.kardc_numero_doc = oBe.ningc_numero_nota_ingreso;
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                        obKardex.kardc_iid_motivo = oBe.ningc_iid_motivo;
                        obKardex.kardc_beneficiario = oBe.ningc_referencia;
                        obKardex.kardc_observaciones = oBe.ningc_observaciones;
                        obKardex.intUsuario = oBe.intUsuario;
                        obKardex.strPc = oBe.strPc;
                        /*------------------------------------------------------------*/
                        x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                        x.ningc_icod_nota_ingreso = intIcod;
                        insertarNotaIngresoDetalle(x);
                        /*------------------------------------------------------------*/
                        EStock objStock = new EStock()
                        {
                            stocc_ianio = oBe.ningc_ianio,
                            almac_icod_almacen = oBe.almac_icod_almacen,
                            prdc_icod_producto = x.prdc_icod_producto,

                            stocc_stock_producto = obKardex.kardc_icantidad_prod,
                            intTipoMovimiento = obKardex.kardc_tipo_movimiento
                        };
                        objAlmacenData.actualizarStock(objStock);
                    });
                    //Cambiar Situacion en FAC
                    if (Convert.ToInt32(oBe.fcoc_icod_doc) != 0)
                    {
                        List<EFacturaCompra> lstVerificacion = new BCompras().listarFacCompraXID(Parametros.intEjercicio, Convert.ToInt32(oBe.fcoc_icod_doc));
                        if (lstVerificacion[0].fcoc_iestado_recep == 273)
                        {
                            EFacturaCompra _beee = new EFacturaCompra();
                            _beee.fcoc_icod_doc = Convert.ToInt32(oBe.fcoc_icod_doc);

                            _beee.fcoc_iestado_recep = 274;//RECIIBIDO
                            new BCompras().modificarFacCompraEstadoRecepcion(_beee);
                        }
                        else
                        {
                            throw new ArgumentException("No se puede recepcionar la Factura ya que se encuentra RECEPCIONADO");
                        }
                    }
                    //------------------------------------
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarNotaIngreso(ENotaIngreso oBe, List<ENotaIngresoDetalle> lstDetalle, List<ENotaIngresoDetalle> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAlmacenData.modificarNotaIngreso(oBe);
                    var lstTipoDocAux = new BAdministracionSistema().listarTipoDocumento().Where(y => y.tdocc_vabreviatura_tipo_doc == "N/I").ToList();
                    if (lstTipoDocAux.Count == 0)
                        throw new ArgumentException("Tipo de documento no registrado N/I");
                    /*-------------------------------------------------------------*/
                    lstDelete.ForEach(x =>
                    {
                        /*---------------eliminar movimiento kardex---------------------------*/
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_icod_correlativo = x.kardc_icod_correlativo;
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                        obKardex.intUsuario = oBe.intUsuario;
                        obKardex.strPc = oBe.strPc;

                        if (obKardex.kardc_icod_correlativo > 0)
                        {
                            /*--eliminar el kardex y stock--------------------------------*/
                            objAlmacenData.eliminarKardex(obKardex);
                            eliminarNotaIngresoDetalle(x);
                            /*------------------------------------------------------------*/
                        }
                    });
                    /*-------------------------------------------------------------*/
                    lstDetalle.ForEach(x =>
                    {
                        /*---------------ingreso al kardex---------------------------*/
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_icod_correlativo = x.kardc_icod_correlativo;
                        obKardex.kardc_ianio = oBe.ningc_ianio;
                        obKardex.kardc_fecha_movimiento = oBe.ningc_fecha_nota_ingreso;
                        obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                        obKardex.prdc_icod_producto = x.prdc_icod_producto;
                        if (Convert.ToInt32(oBe.fcoc_icod_doc) == 0)
                            obKardex.kardc_icantidad_prod = x.dninc_cantidad;
                        else
                            obKardex.kardc_icantidad_prod = x.dnicc_ncantidad_recibida;

                        obKardex.tdocc_icod_tipo_doc = lstTipoDocAux[0].tdocc_icod_tipo_doc;
                        obKardex.kardc_numero_doc = oBe.ningc_numero_nota_ingreso;
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                        obKardex.kardc_iid_motivo = oBe.ningc_iid_motivo;
                        obKardex.kardc_beneficiario = oBe.ningc_referencia;
                        obKardex.kardc_observaciones = oBe.ningc_observaciones;
                        obKardex.intUsuario = oBe.intUsuario;
                        obKardex.strPc = oBe.strPc;
                        /*------------------------------------------------------------*/
                        if (x.intTipoOperacion == 1)
                        {
                            x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                            x.ningc_icod_nota_ingreso = oBe.ningc_icod_nota_ingreso;
                            insertarNotaIngresoDetalle(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {

                            objAlmacenData.modificarKardex(obKardex);
                            modificarNotaIngresoDetalle(x);
                        }

                        EStock objStock = new EStock()
                        {
                            stocc_ianio = oBe.ningc_ianio,
                            almac_icod_almacen = oBe.almac_icod_almacen,
                            prdc_icod_producto = x.prdc_icod_producto,
                            stocc_stock_producto = obKardex.kardc_icantidad_prod,
                            intTipoMovimiento = obKardex.kardc_tipo_movimiento
                        };

                        objAlmacenData.actualizarStock(objStock);
                    });
                    /*-------------------------------------------------------------*/
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    var lst = listarNotaIngresoDetalle(oBe.ningc_icod_nota_ingreso);
                    lst.ForEach(x =>
                    {
                        /*---------------eliminar el movimiento del kardex---------------------------*/
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_icod_correlativo = x.kardc_icod_correlativo;
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                        obKardex.intUsuario = oBe.intUsuario;
                        obKardex.strPc = oBe.strPc;

                        if (obKardex.kardc_icod_correlativo > 0)
                        {
                            /*------eliminar el kardex y sotck----------------------------*/
                            objAlmacenData.eliminarKardex(obKardex);
                            eliminarNotaIngresoDetalle(x);
                            /*------------------------------------------------------------*/
                        }
                    });
                    objAlmacenData.eliminarNotaIngreso(oBe);
                    //Cambiar Situacion en FAC
                    if (Convert.ToInt32(oBe.fcoc_icod_doc) != 0)
                    {
                        List<EFacturaCompra> lstVerificacion = new BCompras().listarFacCompraXID(Parametros.intEjercicio, Convert.ToInt32(oBe.fcoc_icod_doc));
                        if (lstVerificacion[0].fcoc_iestado_recep == 274)
                        {
                            EFacturaCompra _beee = new EFacturaCompra();
                            _beee.fcoc_icod_doc = Convert.ToInt32(oBe.fcoc_icod_doc);

                            _beee.fcoc_iestado_recep = 273;//RECIIBIDO
                            new BCompras().modificarFacCompraEstadoRecepcion(_beee);
                        }
                        else
                        {
                            //throw new ArgumentException("No se puede recepcionar la Factura ya que se encuentra RECEPCIONADO");
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
        #endregion
        #region Nota de Ingreso - Detalle
        public List<ENotaIngresoDetalle> listarNotaIngresoDetalle(int intNotaIngreso)
        {
            List<ENotaIngresoDetalle> lista = null;
            try
            {
                lista = new AlmacenData().listarNotaIngresoDetalle(intNotaIngreso);
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().insertarNotaIngresoDetalle(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().modificarNotaIngresoDetalle(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().eliminarNotaIngresoDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Nota de Salida
        public List<ENotaSalida> listarNotaSalida(int intEjercicio, int intAlmacen, DateTime? dtFechaDesde, DateTime? dtFechaHasta)
        {
            List<ENotaSalida> lista = null;
            try
            {
                lista = new AlmacenData().listarNotaSalida(intEjercicio, intAlmacen, dtFechaDesde, dtFechaHasta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarNotaSalida(ENotaSalida oBe, List<ENotaSalidaDetalle> lstDetalle)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAlmacenData.insertarNotaSalida(oBe);
                    var lstTipoDocAux = new BAdministracionSistema().listarTipoDocumento().Where(y => y.tdocc_vabreviatura_tipo_doc == "N/S").ToList();
                    if (lstTipoDocAux.Count == 0)
                        throw new ArgumentException("Tipo de documento no registrado N/S");
                    lstDetalle.ForEach(x =>
                    {
                        /*---------------salida del kardex---------------------------*/
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_ianio = oBe.nsalc_ianio;
                        obKardex.kardc_fecha_movimiento = oBe.nsalc_fecha_nota_salida;
                        obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                        obKardex.prdc_icod_producto = x.prdc_icod_producto;
                        obKardex.kardc_icantidad_prod = x.dnsalc_cantidad;
                        obKardex.tdocc_icod_tipo_doc = lstTipoDocAux[0].tdocc_icod_tipo_doc;
                        obKardex.kardc_numero_doc = oBe.nsalc_numero_nota_salida;
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                        obKardex.kardc_iid_motivo = oBe.nsalc_iid_motivo;
                        obKardex.kardc_beneficiario = oBe.nsalc_referencia;
                        obKardex.kardc_observaciones = oBe.nsalc_observaciones;
                        obKardex.intUsuario = oBe.intUsuario;
                        obKardex.strPc = oBe.strPc;
                        /*------------------------------------------------------------*/
                        x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                        x.nsalc_icod_nota_salida = intIcod;
                        insertarNotaSalidaDetalle(x);
                        /*------------------------------------------------------------*/
                        EStock objStock = new EStock()
                        {
                            stocc_ianio = oBe.nsalc_ianio,
                            almac_icod_almacen = oBe.almac_icod_almacen,
                            prdc_icod_producto = x.prdc_icod_producto,
                            stocc_stock_producto = x.dnsalc_cantidad,
                            intTipoMovimiento = obKardex.kardc_tipo_movimiento
                        };
                        objAlmacenData.actualizarStock(objStock);
                        /*------------------------------------------------------------*/
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
        public void modificarNotaSalida(ENotaSalida oBe, List<ENotaSalidaDetalle> lstDetalle, List<ENotaSalidaDetalle> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAlmacenData.modificarNotaSalida(oBe);
                    var lstTipoDocAux = new BAdministracionSistema().listarTipoDocumento().Where(y => y.tdocc_vabreviatura_tipo_doc == "N/S").ToList();
                    if (lstTipoDocAux.Count == 0)
                        throw new ArgumentException("Tipo de documento no registrado N/S");
                    /*-------------------------------------------------------------*/
                    lstDelete.ForEach(x =>
                    {
                        /*---------------eliminar movimiento kardex---------------------------*/
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_icod_correlativo = x.kardc_icod_correlativo;
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                        obKardex.intUsuario = oBe.intUsuario;
                        obKardex.strPc = oBe.strPc;

                        if (obKardex.kardc_icod_correlativo > 0)
                        {
                            /*-----eliminar el kardex y stock-----------------------------*/
                            objAlmacenData.eliminarKardex(obKardex);
                            eliminarNotaSalidaDetalle(x);
                            /*------------------------------------------------------------*/
                        }
                    });
                    /*-------------------------------------------------------------*/
                    lstDetalle.ForEach(x =>
                    {
                        /*---------------salida del kardex---------------------------*/
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_icod_correlativo = x.kardc_icod_correlativo;
                        obKardex.kardc_ianio = oBe.nsalc_ianio;
                        obKardex.kardc_fecha_movimiento = oBe.nsalc_fecha_nota_salida;
                        obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                        obKardex.prdc_icod_producto = x.prdc_icod_producto;
                        obKardex.kardc_icantidad_prod = x.dnsalc_cantidad;
                        obKardex.tdocc_icod_tipo_doc = lstTipoDocAux[0].tdocc_icod_tipo_doc;
                        obKardex.kardc_numero_doc = oBe.nsalc_numero_nota_salida;
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                        obKardex.kardc_iid_motivo = oBe.nsalc_iid_motivo;
                        obKardex.kardc_beneficiario = oBe.nsalc_referencia;
                        obKardex.kardc_observaciones = oBe.nsalc_observaciones;
                        obKardex.intUsuario = oBe.intUsuario;
                        obKardex.strPc = oBe.strPc;
                        /*------------------------------------------------------------*/
                        if (x.intTipoOperacion == 1)
                        {
                            x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                            x.nsalc_icod_nota_salida = oBe.nsalc_icod_nota_salida;
                            insertarNotaSalidaDetalle(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {
                            objAlmacenData.modificarKardex(obKardex);
                            modificarNotaSalidaDetalle(x);
                        }
                        /*------------------------------------------------------------*/
                        EStock objStock = new EStock()
                        {
                            stocc_ianio = oBe.nsalc_ianio,
                            almac_icod_almacen = oBe.almac_icod_almacen,
                            prdc_icod_producto = x.prdc_icod_producto,
                            stocc_stock_producto = x.dnsalc_cantidad,
                            intTipoMovimiento = obKardex.kardc_tipo_movimiento
                        };
                        objAlmacenData.actualizarStock(objStock);
                        /*------------------------------------------------------------*/
                    });
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    var lst = listarNotaSalidaDetalle(oBe.nsalc_icod_nota_salida);
                    lst.ForEach(x =>
                    {
                        /*---------------eliminar movimiento kardex---------------------------*/
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_icod_correlativo = x.kardc_icod_correlativo;
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                        obKardex.intUsuario = oBe.intUsuario;
                        obKardex.strPc = oBe.strPc;

                        if (obKardex.kardc_icod_correlativo > 0)
                        {
                            /*-------eliminar kardex y stock------------------------------*/
                            objAlmacenData.eliminarKardex(obKardex);

                            EStock objStock = new EStock()
                            {
                                stocc_ianio = Parametros.intEjercicio,
                                almac_icod_almacen = oBe.almac_icod_almacen,
                                prdc_icod_producto = x.prdc_icod_producto,
                                stocc_stock_producto = x.dnsalc_cantidad,
                                intTipoMovimiento = obKardex.kardc_tipo_movimiento
                            };
                            objAlmacenData.actualizarStock(objStock);

                            eliminarNotaSalidaDetalle(x);
                            /*------------------------------------------------------------*/
                        }
                    });
                    objAlmacenData.eliminarNotaSalida(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Nota de Salida - Detalle
        public List<ENotaSalidaDetalle> listarNotaSalidaDetalle(int intNotaSalida)
        {
            List<ENotaSalidaDetalle> lista = null;
            try
            {
                lista = new AlmacenData().listarNotaSalidaDetalle(intNotaSalida);
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().insertarNotaSalidaDetalle(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().modificarNotaSalidaDetalle(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().eliminarNotaSalidaDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region KArdex Valorizado

        public List<EKardexValorizado> listarKardexValorizadoPorMotivoFecha(int intEjercicio, DateTime dtInicio, DateTime dtFin, int intTipoMov, int intMotivoMov)
        {
            List<EKardexValorizado> lista = null;
            try
            {
                lista = (new AlmacenData()).listarKardexValorizadoPorMotivoFecha(intEjercicio, dtInicio, dtFin, intTipoMov, intMotivoMov);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EKardexValorizado> listarResumenValorizadoProductos(int intEjercicio, DateTime dtInicio, DateTime dtFin)
        {
            List<EKardexValorizado> lista = null;
            try
            {
                lista = (new AlmacenData()).listarResumenValorizadoProductos(intEjercicio, dtInicio, dtFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void ActualizarKardexValorizadoMontoManual(EKardexValorizado objEKardexValorizado, decimal MontoManual)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    AlmacenData objKardexData = new AlmacenData();
                    //Actualizar monto manual
                    objKardexData.ActualizarKardexValorizadoMontoManual(Convert.ToInt32(objEKardexValorizado.kardv_icod_correlativo), MontoManual);

                    objKardexData.ActualizarPrecioCostoPromedioKardexValorizadoXproAlma(
                       objEKardexValorizado.prdc_icod_producto,
                       objEKardexValorizado.almcc_icod_almacen,
                       objEKardexValorizado.kardv_ianio);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    (new AlmacenData()).ActualizarFechaKardexValorizado(obj);
                    (new AlmacenData()).ActualizarPrecioCostoPromedioKardexValorizadoXproAlma(obj.prdc_icod_producto, obj.almcc_icod_almacen, obj.kardv_ianio);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<EKardexValorizadoCompras> ListarKardexValorizadoCompras(int Periodo, DateTime FechaInicio, DateTime FechaFin)
        {
            List<EKardexValorizadoCompras> lista = null;
            try
            {
                lista = (new AlmacenData()).ListarKardexValorizadoCompras(Periodo, FechaInicio, FechaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EKardexValorizadoCompras> ListarKardexValorizadoComprasImportacion(int Periodo, DateTime FechaInicio, DateTime FechaFin)
        {
            List<EKardexValorizadoCompras> lista = null;
            try
            {
                lista = (new AlmacenData()).ListarKardexValorizadoComprasImportacion(Periodo, FechaInicio, FechaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EKardexValorizadoCompras> ListarKardexValorizadoVentas(int Periodo, DateTime FechaInicio, DateTime FechaFin)
        {
            List<EKardexValorizadoCompras> lista = null;
            try
            {
                lista = (new AlmacenData()).ListarKardexValorizadoVentas(Periodo, FechaInicio, FechaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EKardexValorizadoCompras> ListarKardexValorizadoDevoluciones(int Periodo, DateTime FechaInicio, DateTime FechaFin)
        {
            List<EKardexValorizadoCompras> lista = null;
            try
            {
                lista = (new AlmacenData()).ListarKardexValorizadoDevoluciones(Periodo, FechaInicio, FechaFin);
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    AlmacenData objKardexData = new AlmacenData();
                    objKardexData.EliminarKardexValorizadoActualizacion(Periodo, FechaInicio, FechaFin, TipoActualizacion);

                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    AlmacenData objKardexData = new AlmacenData();
                    objKardexData.ActualizacionVentas(Periodo, FechaInicio, FechaFin, TipoActualizacion);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void KardexValorizadoComprasIngresar(List<EKardexValorizadoCompras> lstCompras, int IdUsuario)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    lstCompras.ForEach(x =>
                    {
                        EKardexValorizado oBe = new EKardexValorizado();
                        oBe.kardv_ianio = Parametros.intEjercicio;
                        oBe.kardv_sfecha_movimiento = x.fecha;
                        oBe.almcc_icod_almacen = x.icod_almacen_contable;
                        oBe.prdc_icod_producto = x.icod_producto_especifico;
                        oBe.kardv_icantidad_prod = x.cant_ingreso;
                        oBe.tdocc_icod_tipo_doc = x.icod_tipo_doc;
                        oBe.kardv_inumero_doc = x.doc_numero;
                        oBe.kardv_itipo_movimiento = Parametros.intKardexIn;
                        oBe.kardv_iid_motivo = Parametros.intMotivoKrdComprasIn;
                        oBe.kardv_vbeneficiario = x.referencia;
                        oBe.kardv_vobservaciones = x.observacion;

                        oBe.kardv_monto_total_compra = x.monto_total;
                        oBe.kardv_monto_saldo_valorizado = 0;
                        oBe.kardv_monto_unitario_compra = x.costo_unitario;
                        oBe.kardv_nmonto_ingreso_manual = 0;

                        oBe.intUsuario = IdUsuario;
                        oBe.kardv_itipo_actualizacion = Parametros.intTipoActualizacionCompras;
                        insertarKardexValorizado(oBe);
                        new AlmacenData().ActualizarPrecioCostoPromedioKardexValorizadoXproAlma(x.icod_producto_especifico, x.icod_almacen_contable, Parametros.intEjercicio);
                    });

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void KardexValorizadoImportacionesIngresar(List<EKardexValorizadoCompras> lstCompras, int IdUsuario)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    lstCompras.ForEach(x =>
                    {
                        EKardexValorizado oBe = new EKardexValorizado();
                        oBe.kardv_ianio = Parametros.intEjercicio;
                        oBe.kardv_sfecha_movimiento = x.fecha;
                        oBe.almcc_icod_almacen = x.icod_almacen_contable;
                        oBe.prdc_icod_producto = x.icod_producto_especifico;
                        oBe.kardv_icantidad_prod = x.cant_ingreso;
                        oBe.tdocc_icod_tipo_doc = x.icod_tipo_doc;
                        oBe.kardv_inumero_doc = x.doc_numero;
                        oBe.kardv_itipo_movimiento = Parametros.intKardexIn;
                        oBe.kardv_iid_motivo = Parametros.intMotivoKrdComprasIn;
                        oBe.kardv_vbeneficiario = x.referencia;
                        oBe.kardv_vobservaciones = x.observacion;

                        oBe.kardv_monto_total_compra = x.monto_total;
                        oBe.kardv_monto_saldo_valorizado = 0;
                        oBe.kardv_monto_unitario_compra = x.costo_unitario;
                        oBe.kardv_nmonto_ingreso_manual = 0;

                        oBe.intUsuario = IdUsuario;
                        oBe.kardv_itipo_actualizacion = Parametros.intTipoActualizacionImportacion;
                        insertarKardexValorizado(oBe);
                        new AlmacenData().ActualizarPrecioCostoPromedioKardexValorizadoXproAlma(x.icod_producto_especifico, x.icod_almacen_contable, Parametros.intEjercicio);
                    });

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void KardexValorizadoVentasIngresar(List<EKardexValorizadoCompras> lstVentas, int IdUsuario, int anio)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    lstVentas.ForEach(x =>
                    {
                        EKardexValorizado oBe = new EKardexValorizado();
                        oBe.kardv_ianio = Parametros.intEjercicio;
                        oBe.kardv_sfecha_movimiento = x.fecha;
                        oBe.almcc_icod_almacen = x.icod_almacen_contable;
                        oBe.prdc_icod_producto = x.icod_producto_especifico;
                        oBe.kardv_icantidad_prod = x.cant_salida;
                        oBe.tdocc_icod_tipo_doc = x.icod_tipo_doc;
                        oBe.kardv_inumero_doc = x.doc_numero;
                        oBe.kardv_itipo_movimiento = x.tip_movimiento;
                        oBe.kardv_iid_motivo = x.icod_motivo;
                        oBe.kardv_vbeneficiario = x.referencia;
                        oBe.kardv_vobservaciones = x.observacion;
                        oBe.kardv_monto_total_compra = x.monto_total;
                        oBe.kardv_monto_saldo_valorizado = 0;
                        oBe.kardv_monto_unitario_compra = x.costo_unitario;
                        oBe.kardv_nmonto_ingreso_manual = 0;
                        oBe.intUsuario = IdUsuario;
                        oBe.kardv_itipo_actualizacion = Parametros.intTipoActualizacionVentas;
                        insertarKardexValorizado(oBe);
                        new AlmacenData().ActualizarPrecioCostoPromedioKardexValorizadoXproAlma(x.icod_producto_especifico, x.icod_almacen_contable, Parametros.intEjercicio);
                    });
                    //new AlmacenData().ActualizarPrecioCostoPromedioKardexValorizado(anio);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void KardexValorizadoDevolucionesIngresar(List<EKardexValorizadoCompras> lstDevoluciones, int IdUsuario)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    lstDevoluciones.ForEach(x =>
                    {
                        EKardexValorizado oBe = new EKardexValorizado();
                        oBe.kardv_ianio = Parametros.intEjercicio;
                        oBe.kardv_sfecha_movimiento = x.fecha;
                        oBe.almcc_icod_almacen = x.icod_almacen_contable;
                        oBe.prdc_icod_producto = x.icod_producto_especifico;
                        oBe.kardv_icantidad_prod = x.cant_salida;
                        oBe.tdocc_icod_tipo_doc = x.icod_tipo_doc;
                        oBe.kardv_inumero_doc = x.doc_numero;
                        oBe.kardv_itipo_movimiento = x.tip_movimiento;
                        oBe.kardv_iid_motivo = x.icod_motivo;
                        oBe.kardv_vbeneficiario = x.referencia;
                        oBe.kardv_vobservaciones = x.observacion;
                        oBe.kardv_monto_total_compra = x.monto_total;
                        oBe.kardv_monto_saldo_valorizado = 0;
                        oBe.kardv_monto_unitario_compra = x.costo_unitario;
                        oBe.kardv_nmonto_ingreso_manual = 0;
                        oBe.intUsuario = IdUsuario;
                        oBe.kardv_itipo_actualizacion = Parametros.intTipoActualizacionDevoluciones;
                        insertarKardexValorizado(oBe);
                    });
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int64 insertarKardexValorizado(EKardexValorizado oBe)
        {
            Int64 intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //INSERTARMOS EL MOVIMIENTO EN EL KARDEX VAL.
                    intIcod = objAlmacenData.insertarKardexValorizado(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarKardexValorizadoLista(List<EKardexValorizado> mlist)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //MODIFICAMOS EL MOVIMIENTO DEL KARDEX VAL.
                    foreach (var _b in mlist)
                    {
                        if (_b.intOperacion == 2)
                        {

                            _b.kardv_monto_total_compra = Convert.ToDecimal(_b.kardv_precio_costo_promedio) * Convert.ToDecimal(_b.kardv_icantidad_prod);
                            _b.kardv_precio_costo_promedio = Convert.ToDecimal(_b.kardv_precio_costo_promedio);
                            _b.kardv_monto_saldo_valorizado = 0;
                            _b.kardv_monto_unitario_compra = Convert.ToDecimal(_b.kardv_precio_costo_promedio);
                            _b.kardv_nmonto_ingreso_manual = 0;
                            _b.dcmlIngreso = (_b.kardv_itipo_movimiento == Parametros.intKardexIn) ? _b.kardv_icantidad_prod : 0;
                            _b.dcmlSalida = (_b.kardv_itipo_movimiento == Parametros.intKardexOut) ? _b.kardv_icantidad_prod : 0;
                            _b.intUsuario = 1;
                            _b.strPc = WindowsIdentity.GetCurrent().Name;
                            objAlmacenData.modificarKardexValorizado(_b);
                            //actualizar kardex valorizado
                            objAlmacenData.ActualizarPrecioCostoPromedioKardexValorizadoXproAlma
                                (_b.prdc_icod_producto,
                                 _b.almcc_icod_almacen,
                                 _b.kardv_ianio);

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

        public void modificarKardexValorizado(EKardexValorizado oBe)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //MODIFICAMOS EL MOVIMIENTO DEL KARDEX VAL.
                    objAlmacenData.modificarKardexValorizado(oBe);
                    //ACTUALIZAMOS EL PCP - AL ACTUALIZAR ACTUALIZARA EL STOCK VAL. O LO INSERTARA SI ES NECESARIO
                    objAlmacenData.ActualizarPrecioCostoPromedioKardexValorizadoXproAlma
                        (oBe.prdc_icod_producto,
                         oBe.almcc_icod_almacen,
                         oBe.kardv_ianio);

                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //ELIMINAMOS EL MOVIMIENTO DEL KARDEX VAL.
                    objAlmacenData.eliminarKardexValorizado(oBe);
                    //ACTUALIZAMOS EL PCP
                    objAlmacenData.ActualizarPrecioCostoPromedioKardexValorizadoXproAlma
                       (oBe.prdc_icod_producto,
                        oBe.almcc_icod_almacen,
                        oBe.kardv_ianio);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EKardexValorizado> listarKardexValorizadoSaldoInicial(int intEjercicio)
        {
            List<EKardexValorizado> lista = null;
            try
            {
                lista = objAlmacenData.listarKardexValorizadoSaldoInicial(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion
        #region Kardex
        public int Obtener_Kardex_Max_Correlativo()
        {
            int lista = 0;
            try
            {
                lista = objAlmacenData.Obtener_Kardex_Max_Correlativo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int insertarKardex(EKardex oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new AlmacenData().insertarKardex(oBe);
                    //ACTUALIZAR STOCK
                    EStock stck = new EStock();
                    stck.stocc_ianio = oBe.kardc_ianio;
                    stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                    stck.prdc_icod_producto = Convert.ToInt32(oBe.prdc_icod_producto);
                    stck.stocc_stock_producto = Convert.ToInt32(oBe.kardc_icantidad_prod);
                    stck.intTipoMovimiento = 1;
                    objAlmacenData.actualizarStock(stck);
                    //
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int modificarKardex(EKardex oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAlmacenData.eliminarKardex(oBe);
                    intIcod = objAlmacenData.insertarKardex(oBe);
                    //ACTUALIZAR STOCK
                    EStock stck = new EStock();
                    stck.stocc_ianio = oBe.kardc_ianio;
                    stck.almac_icod_almacen = Convert.ToInt32(oBe.almac_icod_almacen);
                    stck.prdc_icod_producto = Convert.ToInt32(oBe.prdc_icod_producto);
                    stck.stocc_stock_producto = Convert.ToInt32(oBe.kardc_icantidad_prod);
                    stck.intTipoMovimiento = 1;
                    objAlmacenData.actualizarStock(stck);
                    //
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAlmacenData.eliminarKardex(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EKardex> listarStockConsolidado(int intEjercicio)
        {
            List<EKardex> lista = null;
            try
            {
                lista = objAlmacenData.listarStockConsolidado(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EKardex> listarStockConsolidadoxAlmacen(int intEjercicio)
        {
            List<EKardex> lista = null;
            try
            {
                lista = objAlmacenData.listarStockConsolidadoxAlmacen(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EKardex> listarStockConsolidadoxAlmacen2(int intEjercicio)
        {
            List<EKardex> lista = null;
            try
            {
                lista = objAlmacenData.listarStockConsolidadoxAlmacen2(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EKardex> listarKardexPorFechaAlmacenProducto(DateTime dtFechaDesde, DateTime dtFechaHasta, int? intAlmacen, int? intProducto)
        {
            List<EKardex> lista = null;
            try
            {
                lista = objAlmacenData.listarKardexPorFechaAlmacenProducto(dtFechaDesde, dtFechaHasta, intAlmacen, intProducto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EKardex> listarKardexAlmacenProductoVerificar(int? intAlmacen, int? intProducto)
        {
            List<EKardex> lista = null;
            try
            {
                lista = objAlmacenData.listarKardexAlmacenProductoVerificar(intAlmacen, intProducto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EKardex> listarKardexPorMotivoAlmacenProducto(DateTime dtFechaDesde, DateTime dtFechaHasta, int? intTipoMov, int? intMotivo, int? intProducto)
        {
            List<EKardex> lista = null;
            try
            {
                lista = objAlmacenData.listarKardexPorMotivoAlmacenProducto(dtFechaDesde, dtFechaHasta, intTipoMov, intMotivo, intProducto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EKardex> listarResumenPorAlmacen(DateTime dtFechaDesde, DateTime dtFechaHasta, int? intAlmacen)
        {
            List<EKardex> lista = null;
            try
            {
                lista = objAlmacenData.listarResumenPorAlmacen(dtFechaDesde, dtFechaHasta, intAlmacen);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EKardex> listarKardexStockPorFechaAlmacenProducto(DateTime dtFechaDesde, DateTime dtFechaHasta, int? intAlmacen, int? intProducto)
        {
            List<EKardex> lista = null;
            try
            {
                lista = objAlmacenData.listarKardexStockPorFechaAlmacenProducto(dtFechaDesde, dtFechaHasta, intAlmacen, intProducto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EKardex> listarInventarioPermanenteUnidades(DateTime dtFechaDesde, DateTime dtFechaHasta, int? intAlmacen/*, int? intProducto*/)
        {
            List<EKardex> lista = null;
            try
            {
                lista = objAlmacenData.listarInventarioPermanenteUnidades(dtFechaDesde, dtFechaHasta, intAlmacen/*, intProducto*/);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }


        public List<EKardex> listarVerificarStock(int anio)
        {
            List<EKardex> lista = null;
            try
            {
                lista = objAlmacenData.listarVerificarStock(anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EKardex> listarAlmacenSaldoInicial(int intEjercicio)
        {
            List<EKardex> lista = null;
            try
            {
                lista = objAlmacenData.listarAlmacenSaldoInicial(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }



        #endregion
        #region Stock
        public List<EStock> listarStockPorAlmacen(int intEjercicio, int intAlmacen)
        {
            List<EStock> lista = null;
            try
            {
                lista = objAlmacenData.listarStockPorAlmacen(intEjercicio, intAlmacen);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EStock> listarStockPorAlmacenOptimizado(int intEjercicio, int intAlmacen, string codigo, string descripcion)
        {
            List<EStock> lista = null;
            try
            {
                lista = objAlmacenData.listarStockPorAlmacenOptimizado(intEjercicio, intAlmacen, codigo, descripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EStock> listarStockPorAlmacenOptimizadoAfecto(int intEjercicio, int intAlmacen, string codigo, string descripcion, Boolean afecto)
        {
            List<EStock> lista = null;
            try
            {
                lista = objAlmacenData.listarStockPorAlmacenOptimizadoAfecto(intEjercicio, intAlmacen, codigo, descripcion, afecto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        #endregion
        #region Inventario Cab
        public List<EInventarioCab> listarInventarioFisico(int intPeriodo)
        {
            List<EInventarioCab> lista = null;
            try
            {
                lista = new AlmacenData().listarInventarioFisico(intPeriodo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarInventarioFisico(EInventarioCab oBe, List<EInventarioDet> lstDetalle)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new AlmacenData().insertarInventarioFisico(oBe);

                    foreach (EInventarioDet x in lstDetalle)
                    {
                        x.invc_icod_inventario = intIcod;
                        objAlmacenData.insertarInventarioFisicoDet(x);
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

        public void modificarInventarioFisicoCab(EInventarioCab oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAlmacenData.modificarInventarioFisico(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarInventarioFisico(EInventarioCab oBe, List<EInventarioDet> lstDetalle, List<EInventarioDet> lstDelete, int intTipoModificacion)
        {
            //intTipoModificacion --> 1 = Gen , 2 = Reg , 3 = Act
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAlmacenData.modificarInventarioFisico(oBe);

                    lstDelete.ForEach(x =>
                    {
                        objAlmacenData.eliminarInventarioFisicoDet(x);
                    });

                    lstDetalle.ForEach(x =>
                    {
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;


                        if (intTipoModificacion == 1)
                        {
                            if (x.intOperacion == 1)
                            {
                                x.invc_icod_inventario = oBe.invc_icod_inventario;
                                objAlmacenData.insertarInventarioFisicoDet(x);
                            }
                            if (x.intOperacion == 2)
                                objAlmacenData.modificarInventarioFisicoDet(x);
                        }
                        else if (intTipoModificacion == 2)
                        {
                            if (x.invd_icantidad != 0)
                            {

                                objAlmacenData.modificarInventarioFisicoDet(x);

                            }
                            else
                            {
                                if (x.intOperacion == 2)
                                    objAlmacenData.modificarInventarioFisicoDet(x);

                            }

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

        public void actualizarInventarioFisico(EInventarioCab oBe, List<EInventarioDet> lstDetalle)
        {
            try
            {
                ENotaIngreso oBeNI = new ENotaIngreso();
                ENotaSalida oBeNS = new ENotaSalida();

                List<ENotaIngresoDetalle> lstNotaIngreso = new List<ENotaIngresoDetalle>();
                List<ENotaSalidaDetalle> lstNotaSalida = new List<ENotaSalidaDetalle>();
                int intContadorNI = 0;
                int intContadorNS = 0;

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    #region NOTA DE INGRESO CAB
                    oBeNI.ningc_ianio = oBe.invc_sfecha_inventario.Year;
                    oBeNI.ningc_numero_nota_ingreso = String.Format("{0:000000}", getNroNI(oBe.almac_icod_almacen));
                    oBeNI.almac_icod_almacen = oBe.almac_icod_almacen;
                    oBeNI.ningc_iid_motivo = Parametros.intMotivoKrdAjusteInventIn;
                    oBeNI.ningc_fecha_nota_ingreso = oBe.invc_sfecha_inventario;
                    oBeNI.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaIngresoAlmacen;
                    oBeNI.ningc_numero_doc = oBeNI.ningc_numero_nota_ingreso;
                    oBeNI.ningc_referencia = String.Format("INVENTARIO N° {0:00000}", oBe.invc_iid_correlativo);
                    oBeNI.ningc_observaciones = "INGRESOS POR INVENTARIO";
                    oBeNI.intUsuario = oBe.intUsuario;
                    oBeNI.strPc = oBe.strPc;
                    #endregion
                    #region NOTA DE SALIDA CAB
                    oBeNS.nsalc_ianio = oBe.invc_sfecha_inventario.Year;
                    oBeNS.nsalc_numero_nota_salida = String.Format("{0:000000}", getNroNS(oBe.almac_icod_almacen));
                    oBeNS.almac_icod_almacen = oBe.almac_icod_almacen;
                    oBeNS.nsalc_iid_motivo = Parametros.intMotivoKrdAjusteInventOut;
                    oBeNS.nsalc_fecha_nota_salida = oBe.invc_sfecha_inventario;
                    oBeNS.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaSalidaAlmacen;
                    oBeNS.nsalc_numero_doc = oBeNS.nsalc_numero_nota_salida;
                    oBeNS.nsalc_referencia = String.Format("INVENTARIO N° {0:00000}", oBe.invc_iid_correlativo);
                    oBeNS.nsalc_observaciones = "SALIDAS POR INVENTARIO";
                    oBeNS.intUsuario = oBe.intUsuario;
                    oBeNS.strPc = oBe.strPc;
                    #endregion

                    lstDetalle.ForEach(x =>
                    {
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;

                        if (x.dblDiferencia > 0)
                        {
                            #region NOTA INGRESO DETALLE
                            intContadorNI += 1;
                            ENotaIngresoDetalle obeNIDet = new ENotaIngresoDetalle();
                            obeNIDet.dninc_nro_item = intContadorNI;
                            obeNIDet.prdc_icod_producto = x.prdc_icod_producto;
                            obeNIDet.dninc_cantidad = x.dblDiferencia;
                            obeNIDet.intTipoOperacion = 1;
                            obeNIDet.intUsuario = oBe.intUsuario;
                            obeNIDet.strPc = oBe.strPc;

                            #endregion
                            lstNotaIngreso.Add(obeNIDet);

                        }
                        else if (x.dblDiferencia < 0)
                        {
                            #region NOTA SALIDA DETALLE
                            intContadorNS += 1;
                            ENotaSalidaDetalle obeNSDet = new ENotaSalidaDetalle();
                            obeNSDet.dnsalc_nro_item = intContadorNS;
                            obeNSDet.prdc_icod_producto = x.prdc_icod_producto;
                            obeNSDet.dnsalc_cantidad = x.dblDiferencia * -1;
                            obeNSDet.intTipoOperacion = 1;
                            obeNSDet.intUsuario = oBe.intUsuario;
                            obeNSDet.strPc = oBe.strPc;
                            #endregion
                            lstNotaSalida.Add(obeNSDet);
                        }
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        if (x.invd_sis_stock != 0)
                        {
                            objAlmacenData.modificarInventarioFisicoDet(x);
                        }
                    });

                    if (lstNotaIngreso.Count > 0)
                    {
                        int intIcod = 0;
                        intIcod = new AlmacenData().insertarNotaIngreso(oBeNI);
                        oBe.ningc_icod_nota_ingreso = intIcod;
                        //var lstTipoDocAux = new BAdministracionSistema().listarTipoDocumento().Where(y => y.tdocc_vabreviatura_tipo_doc.Trim() == "N/I").ToList();
                        //if (lstTipoDocAux.Count == 0)
                        //    throw new ArgumentException("Tipo de documento no registrado N/I");
                        lstNotaIngreso.ForEach(x =>
                        {

                            /*---------------ingreso al kardex---------------------------*/
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = oBeNI.ningc_ianio;
                            obKardex.kardc_fecha_movimiento = oBeNI.ningc_fecha_nota_ingreso;
                            obKardex.almac_icod_almacen = oBe.almac_icod_almacen;
                            obKardex.prdc_icod_producto = x.prdc_icod_producto;
                            obKardex.kardc_icantidad_prod = x.dninc_cantidad;
                            obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaIngresoAlmacen;
                            obKardex.kardc_numero_doc = oBeNI.ningc_numero_nota_ingreso;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                            obKardex.kardc_iid_motivo = oBeNI.ningc_iid_motivo;
                            obKardex.kardc_beneficiario = oBeNI.ningc_referencia;
                            obKardex.kardc_observaciones = oBeNI.ningc_observaciones;
                            obKardex.intUsuario = oBeNI.intUsuario;
                            obKardex.strPc = oBe.strPc;
                            /*------------------------------------------------------------*/
                            x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                            x.ningc_icod_nota_ingreso = intIcod;
                            insertarNotaIngresoDetalle(x);
                            /*------------------------------------------------------------*/
                            EStock objStock = new EStock()
                            {
                                stocc_ianio = oBeNI.ningc_ianio,
                                almac_icod_almacen = oBe.almac_icod_almacen,
                                prdc_icod_producto = x.prdc_icod_producto,
                                stocc_stock_producto = x.dninc_cantidad,
                                intTipoMovimiento = obKardex.kardc_tipo_movimiento
                            };
                            objAlmacenData.actualizarStock(objStock);
                        });

                        //oBe.ningc_icod_nota_ingreso = insertarNotaIngreso(oBeNI, lstNotaIngreso);
                    }

                    if (lstNotaSalida.Count > 0)
                    {
                        int intIcod = 0;
                        intIcod = new AlmacenData().insertarNotaSalida(oBeNS);
                        oBe.nsalc_icod_nota_salida = intIcod;
                        //var lstTipoDocAux = new BAdministracionSistema().listarTipoDocumento().Where(y => y.tdocc_vabreviatura_tipo_doc == "N/S").ToList();
                        //if (lstTipoDocAux.Count == 0)
                        //    throw new ArgumentException("Tipo de documento no registrado N/S");
                        lstNotaSalida.ForEach(x =>
                        {
                            /*---------------salida del kardex---------------------------*/
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = oBeNS.nsalc_ianio;
                            obKardex.kardc_fecha_movimiento = oBeNS.nsalc_fecha_nota_salida;
                            obKardex.almac_icod_almacen = oBeNS.almac_icod_almacen;
                            obKardex.prdc_icod_producto = x.prdc_icod_producto;
                            obKardex.kardc_icantidad_prod = x.dnsalc_cantidad;
                            obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaSalidaAlmacen;
                            obKardex.kardc_numero_doc = oBeNS.nsalc_numero_nota_salida;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.kardc_iid_motivo = oBeNS.nsalc_iid_motivo;
                            obKardex.kardc_beneficiario = oBeNS.nsalc_referencia;
                            obKardex.kardc_observaciones = oBeNS.nsalc_observaciones;
                            obKardex.intUsuario = oBe.intUsuario;
                            obKardex.strPc = oBe.strPc;
                            /*------------------------------------------------------------*/
                            x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                            x.nsalc_icod_nota_salida = intIcod;
                            insertarNotaSalidaDetalle(x);
                            /*------------------------------------------------------------*/
                            EStock objStock = new EStock()
                            {
                                stocc_ianio = oBeNS.nsalc_ianio,
                                almac_icod_almacen = oBe.almac_icod_almacen,
                                prdc_icod_producto = x.prdc_icod_producto,
                                stocc_stock_producto = x.dnsalc_cantidad,
                                intTipoMovimiento = obKardex.kardc_tipo_movimiento
                            };
                            objAlmacenData.actualizarStock(objStock);
                            /*------------------------------------------------------------*/
                        });
                    }
                    //oBe.nsalc_icod_nota_salida = insertarNotaSalida(oBeNS, lstNotaSalida);

                    objAlmacenData.modificarInventarioFisico(oBe);
                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int getNroNI(int intAlmacen)
        {
            int nro;
            var lstCabecerasNI = new BAlmacen().listarNotaIngreso(Parametros.intEjercicio, intAlmacen, null, null);
            if (lstCabecerasNI.Where(x =>
                   x.almac_icod_almacen == intAlmacen).ToList().Count > 0)
            {
                nro = lstCabecerasNI.Where(x =>
                    x.almac_icod_almacen == intAlmacen).ToList().Max(a => Convert.ToInt32(a.ningc_numero_nota_ingreso)) + 1;
            }
            else
                nro = 1;
            return nro;
        }

        private int getNroNS(int intAlmacen)
        {
            int nro;
            var lstCabecerasNI = new BAlmacen().listarNotaSalida(Parametros.intEjercicio, intAlmacen, null, null);
            if (lstCabecerasNI.Where(x =>
                   x.almac_icod_almacen == intAlmacen).ToList().Count > 0)
            {
                nro = lstCabecerasNI.Where(x =>
                    x.almac_icod_almacen == intAlmacen).ToList().Max(a => Convert.ToInt32(a.nsalc_numero_nota_salida)) + 1;
            }
            else
                nro = 1;
            return nro;
        }

        public void eliminarInventarioFisico(EInventarioCab oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().eliminarInventarioFisico(oBe);
                    tx.Complete();
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
            List<EInventarioDet> lista = null;
            try
            {
                lista = new AlmacenData().listarInventarioFisicoDet(intInventario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EInventarioCab> listarInventarioFisicoDetExcel(int intInventario, DateTime SFECHA)
        {
            List<EInventarioCab> lista = null;
            try
            {
                lista = new AlmacenData().listarInventarioFisicoDetExcel(intInventario, SFECHA);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EInventarioDet> listarInventarioFisicoDetActualizacion(int intInventario)
        {
            List<EInventarioDet> lista = null;
            try
            {
                lista = new AlmacenData().listarInventarioFisicoDetActualizacion(intInventario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public void eliminarInventarioFisicoDet(EInventarioDet oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().eliminarInventarioFisicoDet(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Cierre Anual Almacenes

        public void cierreAnualAlmacenes(DataTable Mlist)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Borrar el pase realizado anteriormente
                    objAlmacenData.DeleteCierreAnualAlmacenes(Parametros.intEjercicio + 1);
                    objAlmacenData.InsetarMasivoKardexb(Mlist);
                    new AlmacenData().stockActualizarConKardex(Parametros.intEjercicio + 1);

                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EKardex> listarCierreAnualAlmacenes(int intEjercicio)
        {
            List<EKardex> lista = null;
            try
            {
                lista = objAlmacenData.listarCierreAnualAlmacenes(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        #endregion
        #region Transferencia Almacenes
        public List<ETransferenciaAlmacen> listarTransferenciaAlmacen(int intEjercicio, DateTime? dtFechaDesde, DateTime? dtFechaHasta)
        {
            List<ETransferenciaAlmacen> lista = null;
            try
            {
                lista = objAlmacenData.listarTransferenciaAlmacen(intEjercicio, dtFechaDesde, dtFechaHasta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTransferenciaAlmacen(ETransferenciaAlmacen oBe, List<ETransferenciaAlmacenDet> lstDetalle)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAlmacenData.insertarTransferenciaAlmacen(oBe);
                    lstDetalle.ForEach(x =>
                    {
                        /*------------------------------------------------------------*/
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_ianio = oBe.trfc_sfecha_transf.Year;
                        obKardex.kardc_fecha_movimiento = oBe.trfc_sfecha_transf;
                        obKardex.almac_icod_almacen = oBe.almac_icod_alm_ing;
                        obKardex.prdc_icod_producto = x.prdc_icod_producto;
                        obKardex.kardc_icantidad_prod = x.trfd_ncantidad;
                        obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocTransferenciaAlmacen;
                        obKardex.kardc_numero_doc = String.Format("{0:0000}", oBe.trfc_inum_transf);
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                        obKardex.kardc_iid_motivo = Parametros.intMotivoKrdTransferenciaIn;
                        obKardex.kardc_beneficiario = String.Format("TRANSF. A ALMACÉN {0}", oBe.strAlmacenIng);
                        obKardex.kardc_observaciones = String.Format("TRANSF. DE ALMACÉN {0}", oBe.strAlmacenSal);
                        obKardex.intUsuario = oBe.intUsuario;
                        obKardex.strPc = oBe.strPc;
                        /*-------GUARDAMOS EL INGRESO AL KARDEX AL ALM. DE INGRESO-----------------------------------------------------*/
                        x.kardc_icod_correlativo_ing = objAlmacenData.insertarKardex(obKardex);
                        /*-------MODIFICAMOS LOS DATOS PARA DAR SALIDA DEL KARDEX DEL ALM. DE SALIDA-----------------------------------------------------*/
                        obKardex.almac_icod_almacen = oBe.almac_icod_alm_sal;
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                        obKardex.kardc_iid_motivo = Parametros.intMotivoKrdTransferenciaOut;
                        /*-------GUARDAMOS LA SALIDA DEL KARDEX DEL ALM. DE SALIDA-----------------------------------------------------*/
                        x.kardc_icod_correlativo_sal = objAlmacenData.insertarKardex(obKardex);
                        /*-------INSERTAMOS EL DET. DE LA TRANSF.-----------------------------------------------------*/
                        x.trfc_icod_transf = intIcod;
                        objAlmacenData.insertarTransferenciaAlmacenDet(x);
                        /*-------AHORA ACTUALIZAMOS EL STOCK PARA CADA ALM.-----------------------------------------------------*/
                        /*-------ALM. DE INGRESO-----------------------------------------------------*/
                        EStock objStock = new EStock()
                        {
                            stocc_ianio = oBe.trfc_sfecha_transf.Year,
                            almac_icod_almacen = oBe.almac_icod_alm_ing,
                            prdc_icod_producto = x.prdc_icod_producto,
                            stocc_stock_producto = x.trfd_ncantidad,
                            intTipoMovimiento = obKardex.kardc_tipo_movimiento
                        };
                        objAlmacenData.actualizarStock(objStock);
                        /*-------ALM. DE SALIDA-----------------------------------------------------*/
                        objStock.almac_icod_almacen = oBe.almac_icod_alm_sal;
                        objAlmacenData.actualizarStock(objStock);
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
        public void modificarTransferenciaAlmacen(ETransferenciaAlmacen oBe, List<ETransferenciaAlmacenDet> lstDetalle, List<ETransferenciaAlmacenDet> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAlmacenData.modificarTransferenciaAlmacen(oBe);
                    /*--------BORRAMOS LO QUE ESTA EN LA LISTA DE BORRAR (KARDEX Y STOCK)-----------------------------------------------------*/
                    lstDelete.ForEach(x =>
                    {
                        /*------EN LA ELIMINACIÓN DEL KARDEX, SE ACTUALIZA AUTOMATICAMENTE EL STOCK-------------------------------------------------------*/
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ing);
                        obKardex.intUsuario = oBe.intUsuario;
                        obKardex.strPc = oBe.strPc;
                        /*------ELIMINAMOS EL ING.-------------------------------------------------------*/
                        objAlmacenData.eliminarKardex(obKardex);
                        /*------ELIMINAMOS LA SAL.-------------------------------------------------------*/
                        obKardex.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_sal);
                        objAlmacenData.eliminarKardex(obKardex);
                        /*-------SE ELIMINA EL DETALLE------------------------------------------------------*/
                        objAlmacenData.eliminarTransferenciaAlmacenDet(x);
                    });

                    /*-------------------------------------------------------------*/
                    lstDetalle.ForEach(x =>
                    {
                        /*-------------------------------------------------------------*/
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ing);
                        obKardex.kardc_ianio = oBe.trfc_sfecha_transf.Year;
                        obKardex.kardc_fecha_movimiento = oBe.trfc_sfecha_transf;
                        obKardex.almac_icod_almacen = oBe.almac_icod_alm_ing;
                        obKardex.prdc_icod_producto = x.prdc_icod_producto;
                        obKardex.kardc_icantidad_prod = x.trfd_ncantidad;
                        obKardex.tdocc_icod_tipo_doc = Parametros.intTipoDocTransferenciaAlmacen;
                        obKardex.kardc_numero_doc = String.Format("{0:0000}", oBe.trfc_inum_transf);
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                        obKardex.kardc_iid_motivo = Parametros.intMotivoKrdTransferenciaIn;
                        obKardex.kardc_beneficiario = String.Format("TRANSF. A ALMACÉN {0}", oBe.strAlmacenIng);
                        obKardex.kardc_observaciones = String.Format("TRANSF. DE ALMACÉN {0}", oBe.strAlmacenSal);
                        obKardex.intUsuario = oBe.intUsuario;
                        obKardex.strPc = oBe.strPc;
                        /*------------------------------------------------------------*/
                        if (x.intTipoOperacion == 1)
                        {
                            /*-------GUARDAMOS EL INGRESO AL KARDEX AL ALM. DE INGRESO-----------------------------------------------------*/
                            x.kardc_icod_correlativo_ing = objAlmacenData.insertarKardex(obKardex);
                            /*-------MODIFICAMOS LOS DATOS PARA DAR SALIDA DEL KARDEX DEL ALM. DE SALIDA-----------------------------------------------------*/
                            obKardex.almac_icod_almacen = oBe.almac_icod_alm_sal;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.kardc_iid_motivo = Parametros.intMotivoKrdTransferenciaOut;
                            /*-------GUARDAMOS LA SALIDA DEL KARDEX DEL ALM. DE SALIDA-----------------------------------------------------*/
                            x.kardc_icod_correlativo_sal = objAlmacenData.insertarKardex(obKardex);
                            /*-------INSERTAMOS EL DET. DE LA TRANSF.-----------------------------------------------------*/
                            x.trfc_icod_transf = oBe.trfc_icod_transf;
                            objAlmacenData.insertarTransferenciaAlmacenDet(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {
                            objAlmacenData.modificarKardex(obKardex);
                            /*-------MODIFICAMOS LOS DATOS PARA MODIFICAR SALIDA DEL KARDEX DEL ALM. DE SALIDA-----------------------------------------------------*/
                            obKardex.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_sal);
                            obKardex.almac_icod_almacen = oBe.almac_icod_alm_sal;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.kardc_iid_motivo = Parametros.intMotivoKrdTransferenciaOut;
                            objAlmacenData.modificarKardex(obKardex);

                            objAlmacenData.modificarTransferenciaAlmacenDet(x);
                        }
                        /*-------ACTUALIZAMOS EL STOCK DEL ALM. DE INGRESO-----------------------------------------------------*/
                        EStock objStock = new EStock()
                        {
                            stocc_ianio = oBe.trfc_sfecha_transf.Year,
                            almac_icod_almacen = oBe.almac_icod_alm_ing,
                            prdc_icod_producto = x.prdc_icod_producto,
                            stocc_stock_producto = x.trfd_ncantidad,
                            intTipoMovimiento = obKardex.kardc_tipo_movimiento
                        };
                        objAlmacenData.actualizarStock(objStock);
                        /*-------ACTUALIZAMOS EL STOCK DEL ALM. DE SALIDA-----------------------------------------------------*/
                        objStock.almac_icod_almacen = oBe.almac_icod_alm_sal;
                        objAlmacenData.actualizarStock(objStock);
                    });
                    /*-------------------------------------------------------------*/
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAlmacenData.eliminarTransferenciaAlmacen(oBe);
                    var lst = objAlmacenData.listarTransferenciaAlmacenDet(oBe.trfc_icod_transf);
                    lst.ForEach(x =>
                    {
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_ing);
                        obKardex.intUsuario = oBe.intUsuario;
                        obKardex.strPc = oBe.strPc;
                        /*------ELIMINAMOS EL ING.-------------------------------------------------------*/
                        objAlmacenData.eliminarKardex(obKardex);
                        /*------ELIMINAMOS LA SAL.-------------------------------------------------------*/
                        obKardex.kardc_icod_correlativo = Convert.ToInt32(x.kardc_icod_correlativo_sal);
                        objAlmacenData.eliminarKardex(obKardex);
                        /*-------SE ELIMINA EL DETALLE------------------------------------------------------*/
                        objAlmacenData.eliminarTransferenciaAlmacenDet(x);
                    });
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ETransferenciaAlmacenDet> listarTransferenciaAlmacenDet(int intIcod)
        {
            List<ETransferenciaAlmacenDet> lista = null;
            try
            {
                lista = objAlmacenData.listarTransferenciaAlmacenDet(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion
        #region Kardex Valorizado
        public List<EKardexValorizado> ListarKardexValorizadoInventarioResumen(DateTime FechaDesde, DateTime FechaHasta)
        {
            List<EKardexValorizado> lista = null;
            try
            {
                lista = (new AlmacenData()).ListarKardexValorizadoInventarioResumen(FechaDesde, FechaHasta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EKardexValorizado> ListarStockValorizadoAunaFecha(DateTime FechaDesde, DateTime FechaHasta)
        {
            List<EKardexValorizado> lista = null;
            try
            {
                lista = (new AlmacenData()).ListarStockValorizadoAunaFecha(FechaDesde, FechaHasta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EKardexValorizado> ListarKardexValorizadoInventario(int IdAlmacen, int IdProducto, DateTime FechaDesde, DateTime FechaHasta)
        {
            List<EKardexValorizado> lista = null;
            try
            {
                lista = (new AlmacenData()).ListarKardexValorizadoInventario(IdAlmacen, IdProducto, FechaDesde, FechaHasta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion  
        #region Pase Almacen
        public void PaseSaldosAlmacenes(int periodo, int iUSUARIO)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().PaseSaldosAlmacenes(periodo, iUSUARIO);
                    //new AlmacenData().stockActualizarConKardex(periodo + 1);
                    tx.Complete();
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
                List<EEmpaquePlantilla> lista = objAlmacenData.EmpaquePlantillaListar();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EmpaquePlantillaInsertar(EEmpaquePlantilla obe, List<EEmpaquePlantilla> Mlist)
        {
            try
            {
                int? id = objAlmacenData.EmpaquePlantillaInsertar(obe);
                foreach (var ob in Mlist)
                {
                    ob.plemc_iid = Convert.ToInt32(id);
                    objAlmacenData.EmpaquePlantillaDetInsertar(ob);
                }
                return Convert.ToInt32(id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EmpaquePlantillaModificar(EEmpaquePlantilla obe, List<EEmpaquePlantilla> Mlist, List<EEmpaquePlantilla> MlistDelete)
        {
            try
            {
                objAlmacenData.EmpaquePlantillaModificar(obe);
                foreach (var ob in MlistDelete)
                {
                    objAlmacenData.EmpaquePlantillaDetEliminar(ob);
                }
                foreach (var ob in Mlist)
                {
                    if (ob.intTipoOperacion == 1)
                    {
                        ob.plemc_iid = obe.plemc_iid;
                        objAlmacenData.EmpaquePlantillaDetInsertar(ob);
                    }
                    if (ob.intTipoOperacion == 2)
                    {
                        objAlmacenData.EmpaquePlantillaDetModificar(ob);
                    }
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
                objAlmacenData.EmpaquePlantillaEliminar(obe);
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
                List<EEmpaquePlantilla> lista = objAlmacenData.EmpaquePlantillaDetListar(plemc_iid);
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
                objAlmacenData.EmpaquePlantillaDetInsertar(obe);
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
                objAlmacenData.EmpaquePlantillaDetModificar(obe);
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
                objAlmacenData.EmpaquePlantillaDetEliminar(obe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Plantilla Empaque
        #region Empaque Desempaque Cab

        public List<EEmpaqueDesempaqueCab> EmpaqueDesempaqueListar()
        {
            try
            {
                List<EEmpaqueDesempaqueCab> lista = objAlmacenData.EmpaqueDesempaqueListar();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EmpaqueDesempaqueInsertar(EEmpaqueDesempaqueCab obe, List<EEmpaqueDesempaqueCab> Mlist)
        {
            try
            {
                int? id = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    if (obe.Tablc_itipo_empaque == 239)//EMPAQUE
                    {
                        #region Kardex Ingreso
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_ianio = Parametros.intEjercicio;
                        obKardex.kardc_fecha_movimiento = obe.emp_sfecha_desempaque;
                        obKardex.almac_icod_almacen = obe.almac_icod_almacen;
                        obKardex.prdc_icod_producto = obe.prdc_icod_producto;
                        obKardex.kardc_icantidad_prod = obe.emp_dcantidad_desempaque;
                        obKardex.tdocc_icod_tipo_doc = 102;//empaque /desempaque E/D
                        obKardex.kardc_numero_doc = obe.emp_vnuemro_desempaque;
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                        obKardex.kardc_iid_motivo = 241;//EMPAQUE
                        obKardex.kardc_beneficiario = obe.prdc_vdescripcion_larga;
                        obKardex.kardc_observaciones = obe.emp_vobservaciones;
                        obKardex.intUsuario = obe.intUsuario;
                        obKardex.strPc = obe.strPc;
                        /*------------------------------------------------------------*/
                        obe.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);

                        EStock objStock = new EStock()
                        {
                            stocc_ianio = Parametros.intEjercicio,
                            almac_icod_almacen = obe.almac_icod_almacen,
                            prdc_icod_producto = obe.prdc_icod_producto,
                            stocc_stock_producto = obe.emp_dcantidad_desempaque,
                            intTipoMovimiento = obKardex.kardc_tipo_movimiento
                        };
                        objAlmacenData.actualizarStock(objStock);
                        #endregion

                    }
                    if (obe.Tablc_itipo_empaque == 240)//DESEMPAQUE
                    {
                        #region Kardex Salida
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_ianio = Parametros.intEjercicio;
                        obKardex.kardc_fecha_movimiento = obe.emp_sfecha_desempaque;
                        obKardex.almac_icod_almacen = obe.almac_icod_almacen;
                        obKardex.prdc_icod_producto = obe.prdc_icod_producto;
                        obKardex.kardc_icantidad_prod = obe.emp_dcantidad_desempaque;
                        obKardex.tdocc_icod_tipo_doc = 102;//empaque /desempaque E/D
                        obKardex.kardc_numero_doc = obe.emp_vnuemro_desempaque;
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                        obKardex.kardc_iid_motivo = 242;//DESEMPAQUE
                        obKardex.kardc_beneficiario = obe.prdc_vdescripcion_larga;
                        obKardex.kardc_observaciones = obe.emp_vobservaciones;
                        obKardex.intUsuario = obe.intUsuario;
                        obKardex.strPc = obe.strPc;
                        /*------------------------------------------------------------*/
                        obe.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                        /*------------------------------------------------------------*/
                        //verificar stock del producto
                        decimal Stock_Producto = new AlmacenData().listarStockProductoPorAlmacen(Parametros.intEjercicio, obe.almac_icod_almacen, obe.prdc_icod_producto);
                        if (Stock_Producto < Convert.ToDecimal(obe.emp_dcantidad_desempaque))
                        {
                            throw new Exception("Stock insuficiente para el producto: " + obe.prdc_vdescripcion_larga + "\nStock actual para este producto es: " + Stock_Producto.ToString());
                        }
                        EStock objStock = new EStock()
                        {
                            stocc_ianio = Parametros.intEjercicio,
                            almac_icod_almacen = obe.almac_icod_almacen,
                            prdc_icod_producto = obe.prdc_icod_producto,
                            stocc_stock_producto = obe.emp_dcantidad_desempaque,
                            intTipoMovimiento = obKardex.kardc_tipo_movimiento
                        };
                        objAlmacenData.actualizarStock(objStock);
                        #endregion
                    }

                    id = objAlmacenData.EmpaqueDesempaqueInsertar(obe);
                    foreach (var x in Mlist)
                    {
                        if (obe.Tablc_itipo_empaque == 239)//EMPAQUE
                        {
                            #region Kardex Salida
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = Parametros.intEjercicio;
                            obKardex.kardc_fecha_movimiento = obe.emp_sfecha_desempaque;
                            obKardex.almac_icod_almacen = obe.almac_icod_almacen;
                            obKardex.prdc_icod_producto = x.prdc_icod_producto;
                            obKardex.kardc_icantidad_prod = x.empd_dcantidad_desempaque * obe.emp_dcantidad_desempaque;
                            obKardex.tdocc_icod_tipo_doc = 102;//empaque /desempaque E/D
                            obKardex.kardc_numero_doc = obe.emp_vnuemro_desempaque;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.kardc_iid_motivo = 241;//EMPAQUE
                            obKardex.kardc_beneficiario = x.prdc_vdescripcion_larga;
                            obKardex.kardc_observaciones = obe.emp_vobservaciones;
                            obKardex.intUsuario = obe.intUsuario;
                            obKardex.strPc = obe.strPc;
                            /*------------------------------------------------------------*/
                            x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);
                            /*------------------------------------------------------------*/
                            //verificar stock del producto
                            decimal Stock_Producto = new AlmacenData().listarStockProductoPorAlmacen(Parametros.intEjercicio, obe.almac_icod_almacen, x.prdc_icod_producto);
                            if (Stock_Producto < Convert.ToDecimal(obe.emp_dcantidad_desempaque * x.empd_dcantidad_desempaque))
                            {
                                throw new Exception("Stock insuficiente para el producto: " + x.prdc_vdescripcion_larga + "\nStock actual para este producto es: " + Stock_Producto.ToString());
                            }
                            EStock objStock = new EStock()
                            {
                                stocc_ianio = Parametros.intEjercicio,
                                almac_icod_almacen = obe.almac_icod_almacen,
                                prdc_icod_producto = x.prdc_icod_producto,
                                stocc_stock_producto = obe.emp_dcantidad_desempaque * x.empd_dcantidad_desempaque,
                                intTipoMovimiento = obKardex.kardc_tipo_movimiento
                            };
                            objAlmacenData.actualizarStock(objStock);
                            #endregion
                        }
                        if (obe.Tablc_itipo_empaque == 240)//DESEMPAQUE
                        {
                            #region Kardex Ingreso
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_ianio = Parametros.intEjercicio;
                            obKardex.kardc_fecha_movimiento = obe.emp_sfecha_desempaque;
                            obKardex.almac_icod_almacen = obe.almac_icod_almacen;
                            obKardex.prdc_icod_producto = x.prdc_icod_producto;
                            obKardex.kardc_icantidad_prod = obe.emp_dcantidad_desempaque * x.empd_dcantidad_desempaque;
                            obKardex.tdocc_icod_tipo_doc = 102;//empaque /desempaque E/D
                            obKardex.kardc_numero_doc = obe.emp_vnuemro_desempaque;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                            obKardex.kardc_iid_motivo = 242;//DESEMPAQUE
                            obKardex.kardc_beneficiario = x.prdc_vdescripcion_larga;
                            obKardex.kardc_observaciones = obe.emp_vobservaciones;
                            obKardex.intUsuario = obe.intUsuario;
                            obKardex.strPc = obe.strPc;
                            /*------------------------------------------------------------*/
                            x.kardc_icod_correlativo = objAlmacenData.insertarKardex(obKardex);

                            EStock objStock = new EStock()
                            {
                                stocc_ianio = Parametros.intEjercicio,
                                almac_icod_almacen = obe.almac_icod_almacen,
                                prdc_icod_producto = x.prdc_icod_producto,
                                stocc_stock_producto = obe.emp_dcantidad_desempaque * x.empd_dcantidad_desempaque,
                                intTipoMovimiento = obKardex.kardc_tipo_movimiento
                            };
                            objAlmacenData.actualizarStock(objStock);
                            #endregion
                        }
                        x.emp_icod_desempaque = Convert.ToInt32(id);
                        objAlmacenData.EmpaqueDesempaqueDetInsertar(x);
                    }
                    tx.Complete();
                }
                return Convert.ToInt32(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EmpaqueDesempaqueModificar(EEmpaqueDesempaqueCab obe, List<EEmpaqueDesempaqueCab> Mlist, List<EEmpaqueDesempaqueCab> MlistDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (obe.Tablc_itipo_empaque == 239)//EMPAQUE
                    {
                        #region Kardex Ingreso
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_icod_correlativo = obe.kardc_icod_correlativo;
                        obKardex.kardc_ianio = Parametros.intEjercicio;
                        obKardex.kardc_fecha_movimiento = obe.emp_sfecha_desempaque;
                        obKardex.almac_icod_almacen = obe.almac_icod_almacen;
                        obKardex.prdc_icod_producto = obe.prdc_icod_producto;
                        obKardex.kardc_icantidad_prod = obe.emp_dcantidad_desempaque;
                        obKardex.tdocc_icod_tipo_doc = 102;//empaque /desempaque E/D
                        obKardex.kardc_numero_doc = obe.emp_vnuemro_desempaque;
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                        obKardex.kardc_iid_motivo = 241;//EMPAQUE
                        obKardex.kardc_beneficiario = obe.prdc_vdescripcion_larga;
                        obKardex.kardc_observaciones = obe.emp_vobservaciones;
                        obKardex.intUsuario = obe.intUsuario;
                        obKardex.strPc = obe.strPc;
                        /*------------------------------------------------------------*/
                        objAlmacenData.modificarKardex(obKardex);

                        EStock objStock = new EStock()
                        {
                            stocc_ianio = Parametros.intEjercicio,
                            almac_icod_almacen = obe.almac_icod_almacen,
                            prdc_icod_producto = obe.prdc_icod_producto,
                            stocc_stock_producto = obe.emp_dcantidad_desempaque,
                            intTipoMovimiento = obKardex.kardc_tipo_movimiento
                        };
                        objAlmacenData.actualizarStock(objStock);
                        #endregion

                    }
                    if (obe.Tablc_itipo_empaque == 240)//DESEMPAQUE
                    {
                        #region Kardex Salida
                        EKardex obKardex = new EKardex();
                        obKardex.kardc_icod_correlativo = obe.kardc_icod_correlativo;
                        obKardex.kardc_ianio = Parametros.intEjercicio;
                        obKardex.kardc_fecha_movimiento = obe.emp_sfecha_desempaque;
                        obKardex.almac_icod_almacen = obe.almac_icod_almacen;
                        obKardex.prdc_icod_producto = obe.prdc_icod_producto;
                        obKardex.kardc_icantidad_prod = obe.emp_dcantidad_desempaque;
                        obKardex.tdocc_icod_tipo_doc = 102;//empaque /desempaque E/D
                        obKardex.kardc_numero_doc = obe.emp_vnuemro_desempaque;
                        obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                        obKardex.kardc_iid_motivo = 242;//DESEMPAQUE
                        obKardex.kardc_beneficiario = obe.prdc_vdescripcion_larga;
                        obKardex.kardc_observaciones = obe.emp_vobservaciones;
                        obKardex.intUsuario = obe.intUsuario;
                        obKardex.strPc = obe.strPc;
                        /*------------------------------------------------------------*/
                        objAlmacenData.modificarKardex(obKardex);
                        /*------------------------------------------------------------*/
                        //verificar stock del producto
                        decimal Stock_Producto = new AlmacenData().listarStockProductoPorAlmacen(Parametros.intEjercicio, obe.almac_icod_almacen, obe.prdc_icod_producto);
                        if (Stock_Producto < Convert.ToDecimal(obe.emp_dcantidad_desempaque))
                        {
                            throw new Exception("Stock insuficiente para el producto: " + obe.prdc_vdescripcion_larga + "\nStock actual para este producto es: " + Stock_Producto.ToString());
                        }
                        EStock objStock = new EStock()
                        {
                            stocc_ianio = Parametros.intEjercicio,
                            almac_icod_almacen = obe.almac_icod_almacen,
                            prdc_icod_producto = obe.prdc_icod_producto,
                            stocc_stock_producto = obe.emp_dcantidad_desempaque,
                            intTipoMovimiento = obKardex.kardc_tipo_movimiento
                        };
                        objAlmacenData.actualizarStock(objStock);
                        #endregion
                    }

                    objAlmacenData.EmpaqueDesempaqueModificar(obe);

                    foreach (var x in Mlist)
                    {
                        if (obe.Tablc_itipo_empaque == 239)//EMPAQUE
                        {
                            #region Kardex Salida
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_icod_correlativo = x.kardc_icod_correlativo;
                            obKardex.kardc_ianio = Parametros.intEjercicio;
                            obKardex.kardc_fecha_movimiento = obe.emp_sfecha_desempaque;
                            obKardex.almac_icod_almacen = obe.almac_icod_almacen;
                            obKardex.prdc_icod_producto = x.prdc_icod_producto;
                            obKardex.kardc_icantidad_prod = x.empd_dcantidad_desempaque * obe.emp_dcantidad_desempaque;
                            obKardex.tdocc_icod_tipo_doc = 102;//empaque /desempaque E/D
                            obKardex.kardc_numero_doc = obe.emp_vnuemro_desempaque;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexOut;
                            obKardex.kardc_iid_motivo = 241;//EMPAQUE
                            obKardex.kardc_beneficiario = x.prdc_vdescripcion_larga;
                            obKardex.kardc_observaciones = obe.emp_vobservaciones;
                            obKardex.intUsuario = obe.intUsuario;
                            obKardex.strPc = obe.strPc;
                            /*------------------------------------------------------------*/
                            objAlmacenData.modificarKardex(obKardex);
                            /*------------------------------------------------------------*/
                            //verificar stock del producto
                            decimal Stock_Producto = new AlmacenData().listarStockProductoPorAlmacen(Parametros.intEjercicio, obe.almac_icod_almacen, x.prdc_icod_producto);
                            if (Stock_Producto < Convert.ToDecimal(obe.emp_dcantidad_desempaque * x.empd_dcantidad_desempaque))
                            {
                                throw new Exception("Stock insuficiente para el producto: " + x.prdc_vdescripcion_larga + "\nStock actual para este producto es: " + Stock_Producto.ToString());
                            }
                            EStock objStock = new EStock()
                            {
                                stocc_ianio = Parametros.intEjercicio,
                                almac_icod_almacen = obe.almac_icod_almacen,
                                prdc_icod_producto = x.prdc_icod_producto,
                                stocc_stock_producto = obe.emp_dcantidad_desempaque * x.empd_dcantidad_desempaque,
                                intTipoMovimiento = obKardex.kardc_tipo_movimiento
                            };
                            objAlmacenData.actualizarStock(objStock);
                            #endregion
                        }
                        if (obe.Tablc_itipo_empaque == 240)//DESEMPAQUE
                        {
                            #region Kardex Ingreso
                            EKardex obKardex = new EKardex();
                            obKardex.kardc_icod_correlativo = x.kardc_icod_correlativo;
                            obKardex.kardc_ianio = Parametros.intEjercicio;
                            obKardex.kardc_fecha_movimiento = obe.emp_sfecha_desempaque;
                            obKardex.almac_icod_almacen = obe.almac_icod_almacen;
                            obKardex.prdc_icod_producto = x.prdc_icod_producto;
                            obKardex.kardc_icantidad_prod = obe.emp_dcantidad_desempaque * x.empd_dcantidad_desempaque;
                            obKardex.tdocc_icod_tipo_doc = 102;//empaque /desempaque E/D
                            obKardex.kardc_numero_doc = obe.emp_vnuemro_desempaque;
                            obKardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                            obKardex.kardc_iid_motivo = 242;//DESEMPAQUE
                            obKardex.kardc_beneficiario = x.prdc_vdescripcion_larga;
                            obKardex.kardc_observaciones = obe.emp_vobservaciones;
                            obKardex.intUsuario = obe.intUsuario;
                            obKardex.strPc = obe.strPc;
                            /*------------------------------------------------------------*/
                            objAlmacenData.modificarKardex(obKardex);

                            EStock objStock = new EStock()
                            {
                                stocc_ianio = Parametros.intEjercicio,
                                almac_icod_almacen = obe.almac_icod_almacen,
                                prdc_icod_producto = x.prdc_icod_producto,
                                stocc_stock_producto = obe.emp_dcantidad_desempaque * x.empd_dcantidad_desempaque,
                                intTipoMovimiento = obKardex.kardc_tipo_movimiento
                            };
                            objAlmacenData.actualizarStock(objStock);
                            #endregion
                        }
                        objAlmacenData.EmpaqueDesempaqueDetModificar(x);

                    }
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal listarStockProductoPorAlmacen(int almacen, int producto)
        {
            decimal Stock_Producto = new AlmacenData().listarStockProductoPorAlmacen(Parametros.intEjercicio, almacen, producto);
            return Stock_Producto;
        }
        public void EmpaqueDesempaqueEliminar(EEmpaqueDesempaqueCab obe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAlmacenData.EmpaqueDesempaqueEliminar(obe);
                    #region Kardex eliminar
                    EKardex obKardex = new EKardex();
                    obKardex.kardc_icod_correlativo = obe.kardc_icod_correlativo;
                    obKardex.intUsuario = obe.intUsuario;
                    obKardex.strPc = obe.strPc;
                    /*------------------------------------------------------------*/
                    objAlmacenData.eliminarKardex(obKardex);

                    EStock objStock = new EStock()
                    {
                        stocc_ianio = Parametros.intEjercicio,
                        almac_icod_almacen = obe.almac_icod_almacen,
                        prdc_icod_producto = obe.prdc_icod_producto,
                        stocc_stock_producto = obe.emp_dcantidad_desempaque,
                        intTipoMovimiento = obKardex.kardc_tipo_movimiento
                    };
                    objAlmacenData.actualizarStock(objStock);
                    #endregion
                    List<EEmpaqueDesempaqueCab> lista = objAlmacenData.EmpaqueDesempaqueDetListar(obe.emp_icod_desempaque);
                    foreach (var x in lista)
                    {
                        #region Kardex eliminar
                        EKardex obKardexDet = new EKardex();
                        obKardexDet.kardc_icod_correlativo = x.kardc_icod_correlativo;
                        obKardexDet.intUsuario = x.intUsuario;
                        obKardexDet.strPc = x.strPc;
                        /*------------------------------------------------------------*/
                        objAlmacenData.eliminarKardex(obKardexDet);

                        EStock objStockDet = new EStock()
                        {
                            stocc_ianio = Parametros.intEjercicio,
                            almac_icod_almacen = obe.almac_icod_almacen,
                            prdc_icod_producto = x.prdc_icod_producto,
                            stocc_stock_producto = x.empd_dcantidad_desempaque,
                            intTipoMovimiento = obKardexDet.kardc_tipo_movimiento
                        };
                        objAlmacenData.actualizarStock(objStockDet);
                        #endregion
                        objAlmacenData.EmpaqueDesempaqueDetEliminar(x);
                    }
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Plantilla Empaque
        #region Plantilla Empaque Detalle

        public List<EEmpaqueDesempaqueCab> EmpaqueDesempaqueDetListar(int emp_icod_desempaque)
        {
            try
            {
                List<EEmpaqueDesempaqueCab> lista = objAlmacenData.EmpaqueDesempaqueDetListar(emp_icod_desempaque);
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
                objAlmacenData.EmpaqueDesempaqueDetInsertar(obe);
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
                objAlmacenData.EmpaqueDesempaqueDetModificar(obe);
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
                objAlmacenData.EmpaqueDesempaqueDetEliminar(obe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Plantilla Empaque
        #region Editorial
        public List<EEditorial> listarEditorial()
        {
            List<EEditorial> lista = null;
            try
            {
                lista = new AlmacenData().listarEditorial();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarEditorial(EEditorial oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new AlmacenData().insertarEditorial(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().modificarEditorial(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AlmacenData().eliminarEditorial(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Registros Categorias

        public List<ECategoria> RegistroCategoriasListar()
        {
            List<ECategoria> lista = new List<ECategoria>();
            try
            {
                lista = objAlmacenData.RegistroCategoriasListar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int RegistroCategoriasInsertar(ECategoria oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = Convert.ToInt32(objAlmacenData.RegistroCategoriasInsertar(oBe));
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RegistroCategoriasActualizar(ECategoria oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAlmacenData.RegistroCategoriasActualizar(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RegistroCategoriasEliminar(ECategoria oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAlmacenData.RegistroCategoriasEliminar(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Registros Caracteristicas det

        public List<ECategoria> ListarCategoriaSubUno(ECategoria obj)
        {
            List<ECategoria> lista = new List<ECategoria>();
            try
            {
                lista = objAlmacenData.ListarCategoriaSubUno(obj);
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
                lista = objAlmacenData.ListarCategoriaSubUnoo(tablc_iid_tipo_tabla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarCategoriaSubUno(ECategoria obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAlmacenData.InsertarCategoriaSubUno(obj);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAlmacenData.ActualizarCategoriaSubUno(obj);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAlmacenData.EliminarCategoriaSubUno(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Registros Caracteristicas det

        public List<ECategoria> ListarCategoriaSubDos(ECategoria obj)
        {
            List<ECategoria> lista = new List<ECategoria>();
            try
            {
                lista = objAlmacenData.ListarCategoriaSubDos(obj);
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
                lista = objAlmacenData.ListarCategoriaSubDoss(tablc_iid_tipo_tabla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarCategoriaSubDos(ECategoria obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAlmacenData.InsertarCategoriaSubDos(obj);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAlmacenData.ActualizarCategoriaSubDos(obj);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAlmacenData.EliminarCategoriaSubDos(obj);
                    tx.Complete();
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
                lista = objAlmacenData.listarVerificacionStockRequerimientoMateriales(IcodRQ);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EKardexValorizado> listarKardexValorizadoPorFechaAlmacenProducto(DateTime dtFechaDesde, DateTime dtFechaHasta, int? intAlmacen, int? intProducto, int anio)
        {
            List<EKardexValorizado> lista = null;
            try
            {
                lista = objAlmacenData.listarKardexValorizadoPorFechaAlmacenProducto(dtFechaDesde, dtFechaHasta, intAlmacen, intProducto, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void ActualizarStockReal(EStock oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAlmacenData.actualizarStock(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EAlmacen> ListarAlmacen()
        {
            throw new NotImplementedException();
        }
    }
}
