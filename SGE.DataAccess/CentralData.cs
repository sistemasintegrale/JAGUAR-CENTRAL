using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace SGE.DataAccess
{
    public class CentralData
    {
        #region Mantenimiento Modelo
        public List<EProdModelo> ListarProdModeloTodo()
        {
            List<EProdModelo> lista = new List<EProdModelo>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_MODELO_LISTAR_TODO();
                    foreach (var item in query)
                    {
                        lista.Add(new EProdModelo()
                        {
                            mo_icod_modelo = item.mo_icod_modelo,
                            mo_iid_modelo = item.mo_iid_modelo,
                            pr_iid_tipo = item.pr_iid_tipo,
                            pr_vid_tipo = string.Format("{0:000}", item.pr_iid_tipo),
                            pr_iid_tipo_descripcion = item.pr_iid_tipo_descripcion,
                            pr_iid_categoria = item.pr_iid_categoria,
                            pr_vid_categoria = string.Format("{0:000}", item.pr_iid_categoria),
                            pr_iid_categoria_descripcion = item.pr_iid_categoria_descripcion,
                            pr_iid_linea = item.pr_iid_linea,
                            pr_vid_linea = string.Format("{0:000}", item.pr_iid_linea),
                            pr_iid_linea_descripcion = item.pr_iid_linea_descripcion,
                            pr_iid_capellada = item.pr_iid_capellada,
                            pr_vid_capellada = string.Format("{0:000}", item.pr_iid_capellada),
                            pr_iid_capellada_descripcion = item.pr_iid_capellada_descripcion,
                            pr_iid_planta = item.pr_iid_planta,
                            pr_vid_planta = string.Format("{0:000}", item.pr_iid_planta),
                            pr_iid_planta_descripcion = item.pr_iid_planta_descripcion,
                            pr_iid_forro = item.pr_iid_forro,
                            pr_vid_forro = string.Format("{0:000}", item.pr_iid_forro),
                            pr_iid_forro_descripcion = item.pr_iid_forro_descripcion,
                            pr_iid_tipo_marca = item.pr_iid_tipo_marca,
                            pr_vid_tipo_marca = string.Format("{0:000}", item.pr_iid_tipo_marca),
                            pr_iid_tipo_marca_descripcion = item.pr_iid_tipo_marca_descripcion,
                            pr_iid_taco = item.pr_iid_taco,
                            pr_vid_taco = string.Format("{0:000}", item.pr_iid_taco),
                            pr_iid_taco_descripcion = item.pr_iid_taco_descripcion,
                            pr_iid_horma = item.pr_iid_horma,
                            pr_vid_horma = string.Format("{0:000}", item.pr_iid_horma),
                            pr_iid_horma_descripcion = item.pr_iid_horma_descripcion,
                            mo_viid_modelo = string.Format("{0:000}", item.mo_iid_modelo),
                            mo_vdescripcion = item.mo_vdescripcion,
                            tarec_icorrelativo = item.tarec_icorrelativo,
                            mo_estado = Convert.ToInt32(item.mo_estado),
                            mo_ruta = item.mo_ruta,
                            mo_imagen = item.mo_imagen,
                            tarec_iid_tabla_registro = Convert.ToInt32(item.tarec_iid_tabla_registro),
                            tarec_vdescripcion = item.tarec_vdescripcion,
                            mo_iserie = item.mo_iserie,
                            mo_cestado = item.mo_cestado
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

        public List<ENotaIngresoOPDetalle> NotaIngresoOpDetalleListar(int niop_icod_nota_ingreso)
        {
            List<ENotaIngresoOPDetalle> lista = new List<ENotaIngresoOPDetalle>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var collection = dc.SGE_PVT_NOTA_INGRESO_OP_DETALLE_LISTAR(niop_icod_nota_ingreso);
                    foreach (var item in collection)
                    {
                        lista.Add(new ENotaIngresoOPDetalle
                        {
                            niopd_icod_nota_ingreso_detalle = item.niopd_icod_nota_ingreso_detalle,
                            niopd_iitem_nota_ingreso_detalle = item.niopd_iitem_nota_ingreso_detalle,
                            niop_icod_nota_ingreso = item.niop_icod_nota_ingreso,
                            orped_icod_item_orden_pedido = item.orped_icod_item_orden_pedido,
                            niopd_cant_1 = item.niopd_cant_1,
                            niopd_cant_2 = item.niopd_cant_2,
                            niopd_cant_3 = item.niopd_cant_3,
                            niopd_cant_4 = item.niopd_cant_4,
                            niopd_cant_5 = item.niopd_cant_5,
                            niopd_cant_6 = item.niopd_cant_6,
                            niopd_cant_7 = item.niopd_cant_7,
                            niopd_cant_8 = item.niopd_cant_8,
                            niopd_cant_9 = item.niopd_cant_9,
                            niopd_cant_10 = item.niopd_cant_10,
                            niopd_iprod_1 = item.niopd_iprod_1,
                            niopd_iprod_2 = item.niopd_iprod_2,
                            niopd_iprod_3 = item.niopd_iprod_3,
                            niopd_iprod_4 = item.niopd_iprod_4,
                            niopd_iprod_5 = item.niopd_iprod_5,
                            niopd_iprod_6 = item.niopd_iprod_6,
                            niopd_iprod_7 = item.niopd_iprod_7,
                            niopd_iprod_8 = item.niopd_iprod_8,
                            niopd_iprod_9 = item.niopd_iprod_9,
                            niopd_iprod_10 = item.niopd_iprod_10,
                            niopd_kardex_1 = item.niopd_kardex_1,
                            niopd_kardex_2 = item.niopd_kardex_2,
                            niopd_kardex_3 = item.niopd_kardex_3,
                            niopd_kardex_4 = item.niopd_kardex_4,
                            niopd_kardex_5 = item.niopd_kardex_5,
                            niopd_kardex_6 = item.niopd_kardex_6,
                            niopd_kardex_7 = item.niopd_kardex_7,
                            niopd_kardex_8 = item.niopd_kardex_8,
                            niopd_kardex_9 = item.niopd_kardex_9,
                            niopd_kardex_10 = item.niopd_kardex_10,
                            NumeroItemOP = item.NumeroItemOP.ToString()
                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public void OrdenPedidoDetSaldoInsertar(ENotaIngresoOPDetalleSaldo obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_NOTA_INGRESO_OP_DETALLE_SALDOS_INSERTAR(
                        obj.orped_icod_item_orden_pedido,
                        obj.niopds_saldos_1,
                        obj.niopds_saldos_2,
                        obj.niopds_saldos_3,
                        obj.niopds_saldos_4,
                        obj.niopds_saldos_5,
                        obj.niopds_saldos_6,
                        obj.niopds_saldos_7,
                        obj.niopds_saldos_8,
                        obj.niopds_saldos_9,
                        obj.niopds_saldos_10
                        );
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
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_NOTA_INGRESO_OP_ELIMINAR(
                        objCab.niop_icod_nota_ingreso,
                        objCab.intUsuario,
                        DateTime.Now,
                        objCab.strPc
                        );
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void NotaIngresoOpModificar(ENotaIngresoOP objCab)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_NOTA_INGRESO_OP_MODIFICAR(
                        objCab.niop_icod_nota_ingreso,
                        objCab.niop_inumero,
                        objCab.niop_ialmacen,
                        objCab.niop_sfecha,
                        objCab.niop_vobservacion,
                        objCab.niop_vreferencia,
                        objCab.orpec_iid_orden_pedido,
                        objCab.intUsuario,
                        DateTime.Now,
                        objCab.strPc
                        );
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public EOrdenPedidoDet OrdenPedidoDetalleGetById(int orped_icod_item_orden_pedido)
        {
            EOrdenPedidoDet lista = new EOrdenPedidoDet();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_PVT_ORDEN_PEDIDO_DET_GET_BY_ID(orped_icod_item_orden_pedido);
                    foreach (var item in query)
                    {
                        lista = new EOrdenPedidoDet()
                        {
                            orped_icod_item_orden_pedido = item.orped_icod_item_orden_pedido,
                            orpec_iid_orden_pedido = item.orpec_iid_orden_pedido,
                            orped_iitem_orden_pedido = item.orped_iitem_orden_pedido,
                            orped_iresponsable = Convert.ToInt32(item.orped_iresponsable),
                            orped_ificha_tecnica = Convert.ToInt32(item.orped_ificha_tecnica),
                            orped_imodelo = Convert.ToInt32(item.orped_imodelo),
                            orped_icolor = Convert.ToInt32(item.orped_icolor),
                            orped_imarca = Convert.ToInt32(item.orped_imarca),
                            orped_itipo = Convert.ToInt32(item.orped_itipo),
                            orped_serie = Convert.ToInt32(item.orped_serie),
                            orped_talla1 = Convert.ToInt32(item.orped_talla1),
                            orped_talla2 = Convert.ToInt32(item.orped_talla2),
                            orped_talla3 = Convert.ToInt32(item.orped_talla3),
                            orped_talla4 = Convert.ToInt32(item.orped_talla4),
                            orped_talla5 = Convert.ToInt32(item.orped_talla5),
                            orped_talla6 = Convert.ToInt32(item.orped_talla6),
                            orped_talla7 = Convert.ToInt32(item.orped_talla7),
                            orped_talla8 = Convert.ToInt32(item.orped_talla8),
                            orped_talla9 = Convert.ToInt32(item.orped_talla9),
                            orped_talla10 = Convert.ToInt32(item.orped_talla10),
                            orped_cant_talla1 = Convert.ToInt32(item.orped_cant_talla1),
                            orped_cant_talla2 = Convert.ToInt32(item.orped_cant_talla2),
                            orped_cant_talla3 = Convert.ToInt32(item.orped_cant_talla3),
                            orped_cant_talla4 = Convert.ToInt32(item.orped_cant_talla4),
                            orped_cant_talla5 = Convert.ToInt32(item.orped_cant_talla5),
                            orped_cant_talla6 = Convert.ToInt32(item.orped_cant_talla6),
                            orped_cant_talla7 = Convert.ToInt32(item.orped_cant_talla7),
                            orped_cant_talla8 = Convert.ToInt32(item.orped_cant_talla8),
                            orped_cant_talla9 = Convert.ToInt32(item.orped_cant_talla9),
                            orped_cant_talla10 = Convert.ToInt32(item.orped_cant_talla10),
                            orped_itotal_item = Convert.ToInt32(item.orped_itotal_item),
                            orped_ndocenas = Convert.ToDecimal(item.orped_ndocenas),
                            orped_flag_estado = item.orped_flag_estado,
                            resec_vdescripcion = item.resec_vdescripcion,
                            pr_viid_marca = item.pr_viid_marca,
                            pr_viid_tipo = item.pr_viid_tipo,
                            pr_viid_color = item.pr_viid_color,
                            NomVendedor = item.NomVendedor,
                            strfichatecnica = item.strfichatecnica,
                            strmodelo = item.strmodelo,
                            orped_isituacion = Convert.ToInt32(item.orped_isituacion),
                            strsituacion = item.strsituacion,
                            fitec_ilinea = Convert.ToInt32(item.fitec_ilinea),
                            strlinea = item.strlinea,
                            orped_vruta = item.orped_vruta,
                            orped_vcodigo_cliente = item.orped_vcodigo_cliente,
                            orped_vcolor_cliente = item.orped_vcolor_cliente,
                            orped_dprecio_cliente = Convert.ToDecimal(item.orped_dprecio_cliente),
                            //orped_vhorma = item.orped_vhorma,
                            orped_btercerizado = Convert.ToBoolean(item.orped_btercerizado),
                            orped_vnum_ficha_tecnica = item.orped_vnum_ficha_tecnica,
                            orped_dprecio_total = item.orped_dprecio_total,
                            ConOPR = item.ConOPR
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public void NotaIngresoOpDetalleModificar(ENotaIngresoOPDetalle item)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_NOTA_INGRESO_OP_DETALLE_MODIFICAR
                        (
                        item.niopd_icod_nota_ingreso_detalle,
                        item.niop_icod_nota_ingreso,
                        item.orped_icod_item_orden_pedido,
                        item.niopd_iitem_nota_ingreso_detalle,
                        item.niopd_cant_1,
                        item.niopd_cant_2,
                        item.niopd_cant_3,
                        item.niopd_cant_4,
                        item.niopd_cant_5,
                        item.niopd_cant_6,
                        item.niopd_cant_7,
                        item.niopd_cant_8,
                        item.niopd_cant_9,
                        item.niopd_cant_10,
                        item.niopd_iprod_1,
                        item.niopd_iprod_2,
                        item.niopd_iprod_3,
                        item.niopd_iprod_4,
                        item.niopd_iprod_5,
                        item.niopd_iprod_6,
                        item.niopd_iprod_7,
                        item.niopd_iprod_8,
                        item.niopd_iprod_9,
                        item.niopd_iprod_10,
                        item.niopd_kardex_1,
                        item.niopd_kardex_2,
                        item.niopd_kardex_3,
                        item.niopd_kardex_4,
                        item.niopd_kardex_5,
                        item.niopd_kardex_6,
                        item.niopd_kardex_7,
                        item.niopd_kardex_8,
                        item.niopd_kardex_9,
                        item.niopd_kardex_10,
                        item.intUsuario,
                        DateTime.Now,
                        item.strPc
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void NotaIngresoOpDetalleElimiar(ENotaIngresoOPDetalle item)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_NOTA_INGRESO_OP_DETALLE_ELIMINAR
                        (
                        item.niopd_icod_nota_ingreso_detalle,
                        item.intUsuario,
                        DateTime.Now,
                        item.strPc
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void NotaIngresoOpDetalleInsertar(ENotaIngresoOPDetalle item)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_NOTA_INGRESO_OP_DETALLE_INSERTAR
                        (
                        item.niop_icod_nota_ingreso,
                        item.orped_icod_item_orden_pedido,
                        item.niopd_iitem_nota_ingreso_detalle,
                        item.niopd_cant_1,
                        item.niopd_cant_2,
                        item.niopd_cant_3,
                        item.niopd_cant_4,
                        item.niopd_cant_5,
                        item.niopd_cant_6,
                        item.niopd_cant_7,
                        item.niopd_cant_8,
                        item.niopd_cant_9,
                        item.niopd_cant_10,
                        item.niopd_iprod_1,
                        item.niopd_iprod_2,
                        item.niopd_iprod_3,
                        item.niopd_iprod_4,
                        item.niopd_iprod_5,
                        item.niopd_iprod_6,
                        item.niopd_iprod_7,
                        item.niopd_iprod_8,
                        item.niopd_iprod_9,
                        item.niopd_iprod_10,
                        item.niopd_kardex_1,
                        item.niopd_kardex_2,
                        item.niopd_kardex_3,
                        item.niopd_kardex_4,
                        item.niopd_kardex_5,
                        item.niopd_kardex_6,
                        item.niopd_kardex_7,
                        item.niopd_kardex_8,
                        item.niopd_kardex_9,
                        item.niopd_kardex_10,
                        item.intUsuario,
                        DateTime.Now,
                        item.strPc
                        );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public void OrdenPedidoDetSaldoActualizar(int orped_icod_item_orden_pedido)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_NOTA_INGRESO_OP_DETALLE_SALDOS_ACTUALIZAR(orped_icod_item_orden_pedido);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int NotaIngresoOpInsertar(ENotaIngresoOP objCab)
        {
            int? icod = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_NOTA_INGRESO_OP_INSERTAR(
                        ref icod,
                        objCab.niop_inumero,
                        objCab.niop_ialmacen,
                        objCab.niop_sfecha,
                        objCab.niop_vobservacion,
                        objCab.niop_vreferencia,
                        objCab.orpec_iid_orden_pedido,
                        objCab.intUsuario,
                        DateTime.Now,
                        objCab.strPc

                        );

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return icod.Value;
        }



        public ENotaIngresoOPDetalleSaldo OrdenPedidoDetSaldo(int orped_icod_item_orden_pedido)
        {
            var obj = new ENotaIngresoOPDetalleSaldo();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var collection = dc.SGE_PVT_NOTA_INGRESO_OP_DETALLE_SALDOS_LISTAR(orped_icod_item_orden_pedido);
                    foreach (var item in collection)
                    {
                        obj.niopds_icod_nota_ingreso_detalle_saldo = item.niopds_icod_nota_ingreso_detalle_saldo;
                        obj.orped_icod_item_orden_pedido = item.orped_icod_item_orden_pedido;
                        obj.niopds_saldos_1 = item.niopds_saldos_1;
                        obj.niopds_saldos_2 = item.niopds_saldos_2;
                        obj.niopds_saldos_3 = item.niopds_saldos_3;
                        obj.niopds_saldos_4 = item.niopds_saldos_4;
                        obj.niopds_saldos_5 = item.niopds_saldos_5;
                        obj.niopds_saldos_6 = item.niopds_saldos_6;
                        obj.niopds_saldos_7 = item.niopds_saldos_7;
                        obj.niopds_saldos_8 = item.niopds_saldos_8;
                        obj.niopds_saldos_9 = item.niopds_saldos_9;
                        obj.niopds_saldos_10 = item.niopds_saldos_10;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return obj;
        }

        public List<ENotaIngresoOP> NotaIngresoOpListar()
        {
            List<ENotaIngresoOP> lista = new List<ENotaIngresoOP>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var collection = dc.SGE_PVT_NOTA_INGRESO_OP_LISTAR();
                    foreach (var item in collection)
                    {
                        lista.Add(new ENotaIngresoOP
                        {
                            niop_icod_nota_ingreso = item.niop_icod_nota_ingreso,
                            niop_inumero = item.niop_inumero,
                            niop_ialmacen = item.niop_ialmacen,
                            niop_sfecha = item.niop_sfecha,
                            niop_vobservacion = item.niop_vobservacion,
                            niop_vreferencia = item.niop_vreferencia,
                            orpec_iid_orden_pedido = item.orpec_iid_orden_pedido,
                            NumeroPedido = item.NumeroPedido,
                            NumeroAlmacen = item.NumeroAlmacen.ToString(),
                            NombreAlmacen = item.NombreAlmacen
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

        public List<ControlPersonalOPR> ControlPersonalOprListar()
        {
            var lista = new List<ControlPersonalOPR>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var collection = dc.SGE_PVT_CONTROL_PERSONAL_OPR_LISTAR();
                    foreach (var item in collection)
                    {
                        lista.Add(new ControlPersonalOPR
                        {
                            IdOrdenProduccion = item.IdOrdenProduccion,
                            NumeroOrdenProduccion = item.NumeroOrdenProduccion,
                            FechaOrdenProduccion = item.FechaOrdenProduccion,
                            NombreCliente = item.NombreCliente,
                            Tipo = item.Tipo,
                            Serie = item.Serie,
                            Pedido = item.Pedido,
                            PedidoItem = item.PedidoItem,
                            Modelo = item.Modelo,
                            Color = item.Color,
                            Docenas = item.Docenas,
                            PorCortar = item.PorCortar,
                            Situacion = item.Situacion
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

        public List<EDestinos> DestinosListar()
        {
            var lista = new List<EDestinos>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var collection = dc.SGE_DESTINO_LISTAR();
                    foreach (var item in collection)
                    {
                        lista.Add(new EDestinos()
                        {
                            des_icod = item.des_icod,
                            des_id = item.des_id,
                            des_vdescripcion = item.des_vdescripcion,
                            fited_iarea = item.fited_iarea
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

        public int DestinosGuadar(EDestinos obj)
        {
            int? icod = obj.des_icod;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_DESTINO_GUARDAR(
                        ref icod,
                        obj.des_id,
                        obj.des_vdescripcion,
                        obj.fited_iarea,
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
            return Convert.ToInt32(icod);
        }

        public List<EProdProducto> ListarProdProductoByMarcaModelo(int intMarca, int intModelo)
        {
            var lista = new List<EProdProducto>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var collection = dc.SGE_PVT_PRODUCTOS_GET_BY_MARCA_MODELO(intMarca, intModelo);
                    foreach (var item in collection)
                    {
                        lista.Add(new EProdProducto
                        {
                            pr_icod_producto = item.pr_icod_producto,
                            pr_vcodigo_externo = item.pr_vcodigo_externo,
                            pr_vdescripcion_producto = item.pr_vdescripcion_producto,
                            pr_vabreviatura_producto = item.pr_vabreviatura_producto,
                            pr_tarec_icorrelativo = Convert.ToInt32(item.pr_tarec_icorrelativo),
                            pr_iid_color = item.pr_iid_color,
                            pr_iid_talla = item.pr_iid_talla,
                            unidc_icod_unidad_medida = item.unidc_icod_unidad_medida,
                            rutaModelo = item.rutaModelo,
                            pr_isituacion_producto = item.pr_isituacion_producto
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

        public List<EProdProducto> ListarProdProductoByMarcaModeloStock(int intMarca, int intModelo, int anio)
        {
            var lista = new List<EProdProducto>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var collection = dc.SGE_PVT_PRODUCTOS_GET_BY_MARCA_MODELO_STOCK(intMarca, intModelo, anio);
                    foreach (var item in collection)
                    {
                        lista.Add(new EProdProducto
                        {
                            pr_icod_producto = item.pr_icod_producto,
                            pr_vcodigo_externo = item.pr_vcodigo_externo,
                            pr_vdescripcion_producto = item.pr_vdescripcion_producto,
                            pr_vabreviatura_producto = item.pr_vabreviatura_producto,
                            pr_tarec_icorrelativo = Convert.ToInt32(item.pr_tarec_icorrelativo),
                            pr_iid_color = item.pr_iid_color,
                            pr_iid_talla = item.pr_iid_talla,
                            unidc_icod_unidad_medida = item.unidc_icod_unidad_medida,
                            rutaModelo = item.rutaModelo,
                            pr_isituacion_producto = item.pr_isituacion_producto,
                            stocc_nstock_prod = item.Stock
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

        public EProdProducto ProductoPvtGetByID(int pr_icod_producto)
        {
            EProdProducto result = new EProdProducto();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var collection = dc.SGE_PVT_PRODUCTOS_GET_BY_ID(pr_icod_producto);
                    foreach (var item in collection)
                    {
                        result.pr_icod_producto = item.pr_icod_producto;
                        result.pr_iid_producto = item.pr_iid_producto;
                        result.pr_iid_marca = item.pr_iid_marca;
                        result.pr_viid_marca = (item.pr_viid_marca);
                        result.pr_iid_modelo = item.pr_iid_modelo;
                        result.pr_viid_modelo = (item.pr_viid_modelo);
                        result.pr_iid_color = item.pr_iid_color;
                        result.pr_viid_color = (item.pr_viid_color);
                        result.pr_iid_talla = item.pr_iid_talla;
                        result.pr_viid_talla = (item.pr_viid_talla);
                        result.pr_vcodigo_externo = item.pr_vcodigo_externo;
                        result.pr_vdescripcion_producto = item.pr_vdescripcion_producto;
                        result.pr_vabreviatura_producto = item.pr_vabreviatura_producto;
                        result.pr_isituacion_producto = item.pr_isituacion_producto;
                        result.pr_tarec_icorrelativo = Convert.ToInt32(item.pr_tarec_icorrelativo);
                        result.pr_vsituacion = (item.pr_isituacion_producto == 0) ? "Inact" : "Act";
                        result.unidc_icod_unidad_medida = item.unidc_icod_unidad_medida;
                        result.descripUnidadMedi = item.descripUnidadMedi;
                        result.abreviaUnidadMedi = item.abreviaUnidadMedi;
                        result.rutaModelo = item.rutaModelo;
                        result.pr_iestado_producto = Convert.ToInt32(item.pr_iestado_producto);
                        result.pr_afecto_igv = Convert.ToBoolean(item.pr_afecto_igv);
                        result.pr_nporcentaje_igv = Convert.ToDecimal(item.pr_nporcentaje_igv);
                        result.CodigoSUNAT = item.CodigoSUNAT;
                        result.strUM = item.strUM;
                        result.pr_icod_serie = Convert.ToInt32(item.pr_icod_serie);
                        result.resec_vtalla_inicial = item.resec_vtalla_inicial;
                        result.resec_vtalla_final = item.resec_vtalla_final;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        public int ProductoPvtGetMaxID()
        {
            int? number = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_PRODUCTO_GET_MAX_ID(ref number);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Convert.ToInt32(number);
        }

        public EProdModelo ModeloGetById(int mo_icod_modelo)
        {
            var result = new EProdModelo();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var collection = dc.SGE_PVT_MODELO_GET_BY_ID(mo_icod_modelo);
                    foreach (var item in collection)
                    {
                        result.mo_icod_modelo = item.mo_icod_modelo;
                        result.mo_iid_modelo = item.mo_iid_modelo;
                        result.pr_iid_tipo = item.pr_iid_tipo;
                        result.pr_vid_tipo = string.Format("{0:000}", item.pr_iid_tipo);
                        result.pr_iid_tipo_descripcion = item.pr_iid_tipo_descripcion;
                        result.pr_iid_categoria = item.pr_iid_categoria;
                        result.pr_vid_categoria = string.Format("{0:000}", item.pr_iid_categoria);
                        result.pr_iid_categoria_descripcion = item.pr_iid_categoria_descripcion;
                        result.pr_iid_linea = item.pr_iid_linea;
                        result.pr_vid_linea = string.Format("{0:000}", item.pr_iid_linea);
                        result.pr_iid_linea_descripcion = item.pr_iid_linea_descripcion;
                        result.pr_iid_capellada = item.pr_iid_capellada;
                        result.pr_vid_capellada = string.Format("{0:000}", item.pr_iid_capellada);
                        result.pr_iid_capellada_descripcion = item.pr_iid_capellada_descripcion;
                        result.pr_iid_planta = item.pr_iid_planta;
                        result.pr_vid_planta = string.Format("{0:000}", item.pr_iid_planta);
                        result.pr_iid_planta_descripcion = item.pr_iid_planta_descripcion;
                        result.pr_iid_forro = item.pr_iid_forro;
                        result.pr_vid_forro = string.Format("{0:000}", item.pr_iid_forro);
                        result.pr_iid_forro_descripcion = item.pr_iid_forro_descripcion;
                        result.pr_iid_tipo_marca = item.pr_iid_tipo_marca;
                        result.pr_vid_tipo_marca = string.Format("{0:000}", item.pr_iid_tipo_marca);
                        result.pr_iid_tipo_marca_descripcion = item.pr_iid_tipo_marca_descripcion;
                        result.pr_iid_taco = item.pr_iid_taco;
                        result.pr_vid_taco = string.Format("{0:000}", item.pr_iid_taco);
                        result.pr_iid_taco_descripcion = item.pr_iid_taco_descripcion;
                        result.pr_iid_horma = item.pr_iid_horma;
                        result.pr_vid_horma = string.Format("{0:000}", item.pr_iid_horma);
                        result.pr_iid_horma_descripcion = item.pr_iid_horma_descripcion;
                        result.mo_viid_modelo = string.Format("{0:000}", item.mo_iid_modelo);
                        result.mo_vdescripcion = item.mo_vdescripcion;
                        result.tarec_icorrelativo = item.tarec_icorrelativo;
                        result.mo_estado = Convert.ToInt32(item.mo_estado);
                        result.mo_ruta = item.mo_ruta;
                        result.mo_imagen = item.mo_imagen;
                        result.tarec_iid_tabla_registro = Convert.ToInt32(item.tarec_iid_tabla_registro);
                        result.tarec_vdescripcion = item.tarec_vdescripcion;
                        result.mo_cestado = item.mo_cestado;
                        result.mo_iserie = item.mo_iserie;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public List<EProdModelo> ModeloListarSimple()
        {
            var lista = new List<EProdModelo>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var collection = dc.SGE_PVT_MODELO_LISTAR_SIMPLE();
                    foreach (var item in collection)
                    {
                        lista.Add(new EProdModelo
                        {
                            mo_icod_modelo = item.mo_icod_modelo,
                            mo_vdescripcion = item.mo_vdescripcion,
                            pr_iid_tipo = item.pr_iid_tipo,
                            pr_iid_categoria = item.pr_iid_categoria,
                            pr_iid_linea = item.pr_iid_linea
                        });
                    };
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return lista;
        }

        public List<EProdModelo> ListarProdModelo(EProdModelo obj)
        {
            List<EProdModelo> lista = new List<EProdModelo>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_MODELO_LISTAR(obj.tarec_icorrelativo);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdModelo()
                        {
                            mo_icod_modelo = item.mo_icod_modelo,
                            mo_iid_modelo = item.mo_iid_modelo,
                            pr_iid_tipo = item.pr_iid_tipo,
                            pr_vid_tipo = string.Format("{0:000}", item.pr_iid_tipo),
                            pr_iid_tipo_descripcion = item.pr_iid_tipo_descripcion,
                            pr_iid_categoria = item.pr_iid_categoria,
                            pr_vid_categoria = string.Format("{0:000}", item.pr_iid_categoria),
                            pr_iid_categoria_descripcion = item.pr_iid_categoria_descripcion,
                            pr_iid_linea = item.pr_iid_linea,
                            pr_vid_linea = string.Format("{0:000}", item.pr_iid_linea),
                            pr_iid_linea_descripcion = item.pr_iid_linea_descripcion,
                            pr_iid_capellada = item.pr_iid_capellada,
                            pr_vid_capellada = string.Format("{0:000}", item.pr_iid_capellada),
                            pr_iid_capellada_descripcion = item.pr_iid_capellada_descripcion,
                            pr_iid_planta = item.pr_iid_planta,
                            pr_vid_planta = string.Format("{0:000}", item.pr_iid_planta),
                            pr_iid_planta_descripcion = item.pr_iid_planta_descripcion,
                            pr_iid_forro = item.pr_iid_forro,
                            pr_vid_forro = string.Format("{0:000}", item.pr_iid_forro),
                            pr_iid_forro_descripcion = item.pr_iid_forro_descripcion,
                            pr_iid_tipo_marca = item.pr_iid_tipo_marca,
                            pr_vid_tipo_marca = string.Format("{0:000}", item.pr_iid_tipo_marca),
                            pr_iid_tipo_marca_descripcion = item.pr_iid_tipo_marca_descripcion,
                            pr_iid_taco = item.pr_iid_taco,
                            pr_vid_taco = string.Format("{0:000}", item.pr_iid_taco),
                            pr_iid_taco_descripcion = item.pr_iid_taco_descripcion,
                            pr_iid_horma = item.pr_iid_horma,
                            pr_vid_horma = string.Format("{0:000}", item.pr_iid_horma),
                            pr_iid_horma_descripcion = item.pr_iid_horma_descripcion,
                            mo_viid_modelo = string.Format("{0:000}", item.mo_iid_modelo),
                            mo_vdescripcion = item.mo_vdescripcion,
                            tarec_icorrelativo = item.tarec_icorrelativo,
                            mo_ruta = item.mo_ruta,
                            mo_imagen = item.mo_imagen,
                            mo_cestado = item.mo_cestado,
                            mo_iserie = item.mo_iserie
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
        public int InsertarProdModelo(EProdModelo obj)
        {
            int? icod = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_MODELO_INSERTAR(
                        ref icod,
                        obj.mo_iid_modelo,
                        obj.pr_iid_tipo,
                        obj.pr_iid_categoria,
                        obj.pr_iid_linea,
                        obj.pr_iid_capellada,
                        obj.pr_iid_planta,
                        obj.pr_iid_forro,
                        obj.pr_iid_tipo_marca,
                        obj.pr_iid_taco,
                        obj.pr_iid_horma,
                        obj.mo_vdescripcion,
                        obj.intUsuario,
                        obj.strPc,
                        obj.tarec_icorrelativo,
                        obj.mo_ruta,
                        obj.mo_imagen,
                        obj.mo_cestado,
                        obj.mo_iserie
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Convert.ToInt32(icod);
        }
        public void ActualizarProdModelo(EProdModelo obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_MODELO_MODIFICAR(
                    obj.mo_icod_modelo,
                    obj.mo_iid_modelo,
                    obj.pr_iid_tipo,
                    obj.pr_iid_categoria,
                    obj.pr_iid_linea,
                    obj.pr_iid_capellada,
                    obj.pr_iid_planta,
                    obj.pr_iid_forro,
                    obj.pr_iid_tipo_marca,
                    obj.pr_iid_taco,
                    obj.pr_iid_horma,
                    obj.mo_vdescripcion,
                    obj.intUsuario,
                    obj.strPc,
                    obj.mo_ruta,
                    obj.mo_imagen,
                    obj.mo_cestado,
                    obj.mo_iserie
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarProdModelo(EProdModelo obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_MODELO_ELIMINAR(obj.mo_icod_modelo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Mantenimiento Productos
        public List<EProdProducto> ListarProdProductos()
        {
            List<EProdProducto> lista = new List<EProdProducto>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_PRODUCTOS_LISTAR();
                    dc.CommandTimeout = 999999999;
                    foreach (var item in query)
                    {
                        lista.Add(new EProdProducto()
                        {

                            pr_icod_producto = item.pr_icod_producto,
                            pr_iid_producto = item.pr_iid_producto,
                            pr_iid_marca = item.pr_iid_marca,
                            pr_viid_marca = (item.pr_viid_marca),
                            pr_iid_modelo = item.pr_iid_modelo,
                            pr_viid_modelo = (item.pr_viid_modelo),
                            pr_iid_color = item.pr_iid_color,
                            pr_viid_color = (item.pr_viid_color),
                            pr_iid_talla = item.pr_iid_talla,
                            pr_viid_talla = (item.pr_viid_talla),
                            pr_vcodigo_externo = item.pr_vcodigo_externo,
                            pr_vdescripcion_producto = item.pr_vdescripcion_producto,
                            pr_vabreviatura_producto = item.pr_vabreviatura_producto,
                            pr_isituacion_producto = item.pr_isituacion_producto,
                            pr_tarec_icorrelativo = Convert.ToInt32(item.pr_tarec_icorrelativo),
                            pr_vsituacion = (item.pr_isituacion_producto == 0) ? "Inact" : "Act",
                            unidc_icod_unidad_medida = item.unidc_icod_unidad_medida,
                            descripUnidadMedi = item.descripUnidadMedi,
                            abreviaUnidadMedi = item.abreviaUnidadMedi,
                            rutaModelo = item.rutaModelo,
                            pr_iestado_producto = Convert.ToInt32(item.pr_iestado_producto),
                            pr_afecto_igv = Convert.ToBoolean(item.pr_afecto_igv),
                            pr_nporcentaje_igv = Convert.ToDecimal(item.pr_nporcentaje_igv),
                            CodigoSUNAT = item.CodigoSUNAT,
                            strUM = item.strUM,
                            pr_icod_serie = Convert.ToInt32(item.pr_icod_serie),
                            resec_vtalla_inicial = item.resec_vtalla_inicial,
                            resec_vtalla_final = item.resec_vtalla_final
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
        public List<EProdProducto> ListarProdProductosIndexes(string Codigo, string Descripcion, string modelo)
        {
            List<EProdProducto> lista = new List<EProdProducto>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_PRODUCTOS_LISTAR_INDIXES(Codigo, Descripcion, modelo);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdProducto()
                        {

                            pr_icod_producto = item.pr_icod_producto,
                            pr_iid_producto = item.pr_iid_producto,
                            pr_iid_marca = item.pr_iid_marca,
                            pr_viid_marca = (item.pr_viid_marca),
                            pr_iid_modelo = item.pr_iid_modelo,
                            pr_viid_modelo = (item.pr_viid_modelo),
                            pr_iid_color = item.pr_iid_color,
                            pr_viid_color = (item.pr_viid_color),
                            pr_iid_talla = item.pr_iid_talla,
                            pr_viid_talla = (item.pr_viid_talla),
                            pr_vcodigo_externo = item.pr_vcodigo_externo,
                            pr_vdescripcion_producto = item.pr_vdescripcion_producto,
                            pr_vabreviatura_producto = item.pr_vabreviatura_producto,
                            pr_isituacion_producto = item.pr_isituacion_producto,
                            pr_tarec_icorrelativo = Convert.ToInt32(item.pr_tarec_icorrelativo),
                            pr_vsituacion = (item.pr_isituacion_producto == 0) ? "Inact" : "Act",
                            unidc_icod_unidad_medida = item.unidc_icod_unidad_medida,
                            descripUnidadMedi = item.descripUnidadMedi,
                            abreviaUnidadMedi = item.abreviaUnidadMedi,
                            rutaModelo = item.rutaModelo,
                            pr_iestado_producto = Convert.ToInt32(item.pr_iestado_producto),
                            icodmarca = Convert.ToInt32(item.icodmarca),
                            icodcolor = Convert.ToInt32(item.icodcolor),
                            pr_afecto_igv = Convert.ToBoolean(item.pr_afecto_igv),
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
        public int InsertarProdProductos(EProdProducto obj)
        {
            int? icod = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_PRODUCTOS_INSERTAR(
                        ref icod,
                        obj.pr_iid_producto,
                        obj.pr_iid_marca,
                        obj.pr_iid_modelo,
                        obj.pr_iid_color,
                        obj.pr_iid_talla,
                        obj.pr_vcodigo_externo,
                        obj.pr_vdescripcion_producto,
                        obj.pr_vabreviatura_producto,
                        obj.pr_isituacion_producto,
                        obj.intUsuario,
                        obj.strPc,
                        obj.pr_tarec_icorrelativo,
                        obj.unidc_icod_unidad_medida,
                        obj.pr_afecto_igv,
                        obj.pr_nporcentaje_igv,
                        obj.pr_icod_serie);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return icod.Value;
        }
        public void ActualizarProdProductos(EProdProducto obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_PRODUCTOS_MODIFICAR(
                            obj.pr_icod_producto,
                            obj.pr_iid_marca,
                            obj.pr_iid_modelo,
                            obj.pr_iid_color,
                            obj.pr_iid_talla,
                            obj.pr_vcodigo_externo,
                            obj.pr_vdescripcion_producto,
                            obj.pr_vabreviatura_producto,
                            obj.pr_isituacion_producto,
                            obj.intUsuario,
                            obj.strPc,
                            obj.pr_tarec_icorrelativo,
                            obj.unidc_icod_unidad_medida,
                            obj.pr_afecto_igv,
                            obj.pr_nporcentaje_igv);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarProdProductos(EProdProducto obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_PRODUCTOS_ELIMINAR(obj.pr_icod_producto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EProdProducto> VerificarProdProducto(string Code)
        {
            List<EProdProducto> lista = new List<EProdProducto>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_VERIFICAR_PRODUCTO(Code);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdProducto()
                        {
                            pr_vcodigo_externo = item.pr_vcodigo_externo,
                            pr_icod_producto = item.pr_icod_producto,
                            pr_vdescripcion_producto = item.pr_vdescripcion_producto
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
        public int VerificarProdProductoEnkardex(int Kardc_iid_producto)
        {
            int cantidad = 0;
            List<EProdProducto> lista = new List<EProdProducto>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_VERIFICAR_PRODUCTO_EN_KARDEX(Kardc_iid_producto);
                    foreach (var item in query)
                    {
                        cantidad = Convert.ToInt32(item.cantidad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cantidad;
        }
        #endregion
        #region UnidadMedida


        public List<EProdUnidadMedida> ListarProdUnidadMedida()
        {
            List<EProdUnidadMedida> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdUnidadMedida>();
                    var query = dc.SGE_PVT_UNIDAD_MEDIDA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EProdUnidadMedida()
                        {
                            id_unidad_medida = Convert.ToInt32(item.unidc_icod_unidad_medida),
                            idf_unidad_medida = String.Format("{0:00}", item.unidc_iid_unidad_medida),
                            abreviatura_unidad_medida = item.unidc_vabreviatura_unidad_medida,
                            descripcion = item.unidc_vdescripcion,
                            unidc_vcodigo_sunat = item.unidc_vcodigo_sunat,
                            unidc_iactivo = Convert.ToInt32(item.unidc_iactivo)
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

        public int InsertarProdUnidadMedida(EProdUnidadMedida objEUnidadMedida)
        {
            int intIdUnidadMedida = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    int? intId = 0;
                    dc.SGE_PVT_UNIDAD_MEDIDA_INSERTAR(
                    objEUnidadMedida.id_unidad_medida,
                    objEUnidadMedida.abreviatura_unidad_medida,
                    objEUnidadMedida.descripcion,
                    objEUnidadMedida.unidc_vcodigo_sunat,
                    objEUnidadMedida.usuario_crea,
                    objEUnidadMedida.fecha_crea,
                    ref intId
                    );

                    intIdUnidadMedida = Convert.ToInt32(intId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intIdUnidadMedida;
        }

        public int ActualizarProdUnidadMedida(EProdUnidadMedida objEUnidadMedida)
        {
            int intIdUnidadMedida = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_UNIDAD_MEDIDA_ACTUALIZAR(
                    objEUnidadMedida.id_unidad_medida,
                    objEUnidadMedida.abreviatura_unidad_medida,
                    objEUnidadMedida.descripcion,
                    objEUnidadMedida.unidc_vcodigo_sunat,
                    objEUnidadMedida.usuario_modifica,
                    objEUnidadMedida.fecha_modifica);
                }
                intIdUnidadMedida = objEUnidadMedida.id_unidad_medida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intIdUnidadMedida;

        }

        public int EliminarProdUnidadMedida(int intIdUnidadMedida)
        {
            int intResultado = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_UNIDAD_MEDIDA_ELIMINAR(intIdUnidadMedida);
                }
                intResultado = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intResultado;
        }
        #endregion
        #region Mantenimiento Almacen
        public List<EProdAlmacen> ListarProdAlmacen()
        {
            List<EProdAlmacen> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdAlmacen>();
                    var query = dc.SGE_PVT_ALMACEN_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EProdAlmacen()
                        {
                            id_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            puvec_icod_punto_venta = Convert.ToInt32(item.puvec_icod_punto_venta),
                            descripcion_puntoVenta = item.descripcion_puntoventa,
                            idf_almacen = String.Format("{0:00}", item.almac_iid_almacen),
                            descripcion = item.almac_vdescripcion,
                            direccion = item.almac_vdireccion,
                            representante = item.almac_vrepresentante,
                            iactivo = Convert.ToInt32(item.almac_iactivo),
                            codigo_ubigeo = item.codigo_ubigeo
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
        public int InsertarProdAlmacen(EProdAlmacen objEAlmacen)
        {
            int? intId = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_ALMACEN_INSERTAR(
                        objEAlmacen.id_almacen,
                        objEAlmacen.puvec_icod_punto_venta,
                        objEAlmacen.descripcion,
                        objEAlmacen.direccion,
                        objEAlmacen.representante,
                        objEAlmacen.intUsuario,
                        ref intId,
                        objEAlmacen.iactivo,
                        objEAlmacen.codigo_ubigeo
                    );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intId.Value;
        }
        public int ActualizarProdAlmacen(EProdAlmacen objEAlmacen)
        {
            int intIdAlmacen = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_ALMACEN_ACTUALIZAR(
                    objEAlmacen.id_almacen,
                    objEAlmacen.puvec_icod_punto_venta,
                    objEAlmacen.descripcion,
                    objEAlmacen.direccion,
                    objEAlmacen.representante,
                    objEAlmacen.intUsuario,
                    objEAlmacen.codigo_ubigeo
                    );
                }
                intIdAlmacen = objEAlmacen.id_almacen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intIdAlmacen;
        }
        public int EliminarProdAlmacen(int intIdAlmacen)
        {
            int intResultado = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_ALMACEN_ELIMINAR(intIdAlmacen);
                }
                intResultado = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intResultado;
        }

        #endregion
        #region Verificar Movimientos

        public int VerificarProdMovAlmacen(int iOpcion, int iid_almacen)
        {
            int cantidad = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_VERIFICAR_MOVIMIENTOS_DE_ALMACEN(iOpcion, iid_almacen);
                    foreach (var item in query)
                    {
                        cantidad = Convert.ToInt32(item.cantidad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cantidad;
        }

        #endregion
        #region Tabla Registro

        public List<EProdTabla> listarProdTabla()
        {
            List<EProdTabla> lista = new List<EProdTabla>(); ;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_PVT_TABLA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EProdTabla()
                        {
                            tablc_iid_tipo_tabla = item.tablc_iid_tipo_tabla,
                            tablc_vdescripcion = item.tablc_vdescripcion.Trim(),
                            tablc_cestado = Convert.ToChar(item.tablc_cestado),
                            strEstado = (item.tablc_cestado == 'A') ? "Activo" : "Inactivo"
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
        public int insertarProdTabla(EProdTabla obj)
        {
            int? intIcod = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_TABLA_INSERTAR(
                        ref intIcod,
                        obj.tablc_vdescripcion,
                        obj.tablc_cestado
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarProdTabla(EProdTabla obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_TABLA_MODIFICAR(
                        obj.tablc_iid_tipo_tabla,
                        obj.tablc_vdescripcion,
                        obj.tablc_cestado);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarProdTabla(EProdTabla obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_TABLA_ELIMINAR(
                        obj.tablc_iid_tipo_tabla);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public List<EProdTablaRegistro> ListarProdTablaRegistro(EProdTablaRegistro obj)
        {
            List<EProdTablaRegistro> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdTablaRegistro>();
                    var query = dc.SGE_PVT_TABLA_REGISTRO_LISTAR(obj.iid_tipo_tabla);
                    int TipoRegistro = obj.iid_tipo_tabla;
                    foreach (var item in query)
                    {
                        lista.Add(new EProdTablaRegistro()
                        {

                            icod_tabla = item.tarec_iid_tabla_registro,
                            iid_tipo_tabla = Convert.ToInt32(item.tablc_iid_tipo_tabla),
                            tarec_iid_correlativo = Convert.ToInt32(item.tarec_icorrelativo_registro),
                            tarec_viid_correlativo = (TipoRegistro == 2 || TipoRegistro == 1 ||
                                                      TipoRegistro == 3 || TipoRegistro == 5 ||
                                                      TipoRegistro == 8 || TipoRegistro == 9 ||
                                                      TipoRegistro == 10 || TipoRegistro == 7) ? string.Format("{0:000}", item.tarec_icorrelativo_registro) :
                                                                            string.Format("{0:000}", item.tarec_icorrelativo_registro),
                            descripcion = item.tarec_vdescripcion,
                            Estado = Convert.ToChar(item.tarec_cestado),
                            tarec_vabreviatura = item.tarec_vvalor_texto
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
        public void InsertarProdTablaRegistro(EProdTablaRegistro obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_TABLA_REGISTRO_INSERTAR(
                       obj.tablc_iid_tipo_tabla,
                        obj.tarec_icorrelativo_registro,
                        obj.tarec_vdescripcion,
                        obj.intUsuario,
                        obj.tarec_vabreviatura
                   );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarProdTablaRegistro(EProdTablaRegistro obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_TABLA_REGISTRO_MODIFICAR(
                          obj.icod_tabla,
                        obj.tarec_vdescripcion,
                        obj.Estado,
                        obj.intUsuario,
                        obj.tarec_vabreviatura);
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
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_TABLA_REGISTRO_ELIMINAR(obj.icod_tabla);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EProdTablaRegistro> ListarProdOpcionesRegistro(EProdTablaRegistro obj)
        {
            List<EProdTablaRegistro> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdTablaRegistro>();
                    var query = dc.SGE_PVT_OPCIONES_REGISTRO_LISTAR(obj.iid_tipo_tabla);
                    int TipoRegistro = obj.iid_tipo_tabla;
                    foreach (var item in query)
                    {
                        lista.Add(new EProdTablaRegistro()
                        {
                            icod_tabla = item.tarec_iid_tabla_registro,
                            iid_tipo_tabla = Convert.ToInt32(item.tablc_iid_tipo_tabla),
                            tarec_iid_correlativo = Convert.ToInt32(item.tarec_icorrelativo_registro),
                            tarec_viid_correlativo = string.Format("{0:00}", item.tarec_icorrelativo_registro),
                            descripcion = item.tarec_vdescripcion,
                            Estado = Convert.ToChar(item.tarec_cestado),
                            tarec_vabreviatura = item.tarec_vvalor_texto

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
        public List<EProdTablaRegistro> ListarProdMotivoXTipo(int tablc_iid_tipo_tabla)
        {
            List<EProdTablaRegistro> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdTablaRegistro>();
                    var query = dc.SGE_PVT_MOTIVO_LISTAR_X_TABLA(tablc_iid_tipo_tabla);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdTablaRegistro()
                        {
                            icod_tabla = item.tarec_iid_tabla_registro,
                            iid_tipo_tabla = Convert.ToInt32(item.tablc_iid_tipo_tabla),
                            descripcion = item.tarec_vdescripcion,
                            tarec_iid_correlativo = Convert.ToInt32(item.tarec_icorrelativo_registro)
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
        public List<EProdTablaRegistro> ListarProdTablaRegistroTodo()
        {
            List<EProdTablaRegistro> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdTablaRegistro>();
                    var query = dc.SGE_PVT_TABLA_REGISTRO_LISTAR_TODO();
                    foreach (var item in query)
                    {
                        lista.Add(new EProdTablaRegistro()
                        {
                            tarec_iid_tabla_registro = item.tarec_iid_tabla_registro,
                            tablc_iid_tipo_tabla = item.tablc_iid_tipo_tabla,
                            tarec_icorrelativo_registro = item.tarec_icorrelativo_registro,
                            tarec_vdescripcion = item.tarec_vdescripcion,
                            tarec_nvalor_numerico = item.tarec_nvalor_numerico,
                            tarec_vvalor_texto = item.tarec_vvalor_texto,
                            tarec_cestado = Convert.ToChar(item.tarec_cestado),
                            tarec_iusuario_crea = item.tarec_iusuario_crea,
                            tarec_iusuario_modifica = item.tarec_iusuario_modifica,
                            tarec_sfecha_crea = item.tarec_sfecha_crea,
                            tarec_sfecha_modifica = item.tarec_sfecha_modifica
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
        #region Area Produccion
        public List<EAreaProduccion> listarAreaProduccion()
        {
            List<EAreaProduccion> lista = new List<EAreaProduccion>(); ;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_AREA_PRODUCCION_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EAreaProduccion()
                        {
                            arprc_iid_codigo = item.arprc_iid_codigo,
                            arprc_vdescripcion = item.arprc_vdescripcion.Trim(),
                            arprc_cestado = Convert.ToChar(item.arprc_cestado),
                            strEstado = (item.arprc_cestado == 'A') ? "Activo" : "Inactivo",
                            arprc_icolor = item.arprc_icolor
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
        public int insertarAreaProduccion(EAreaProduccion obj)
        {
            int? intIcod = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_AREA_PRODUCCION_INSERTAR(
                        ref intIcod,
                        obj.arprc_vdescripcion,
                        obj.arprc_vabreviatura,
                        obj.intUsuario,
                        obj.strPc,
                        obj.arprc_cestado,
                        obj.arprc_icolor
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarAreaProduccion(EAreaProduccion obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_AREA_PRODUCCION_MODIFICAR(
                        obj.arprc_iid_codigo,
                        obj.arprc_vdescripcion,
                        obj.arprc_vabreviatura,
                        obj.intUsuario,
                        obj.strPc,
                        obj.arprc_cestado,
                        obj.arprc_icolor
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarAreaProduccion(EAreaProduccion obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_AREA_PRODUCCION_ELIMINAR(
                        obj.arprc_iid_codigo,
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
        #region Area Ubicacion
        public List<EAreaUbicacion> ListarAreaUbicacion(EAreaUbicacion obj)
        {
            List<EAreaUbicacion> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EAreaUbicacion>();
                    var query = dc.SGE_AREA_UBICACION_LISTAR(obj.arprc_iid_codigo);
                    int TipoRegistro = obj.arprc_iid_codigo;
                    foreach (var item in query)
                    {
                        lista.Add(new EAreaUbicacion()
                        {

                            arubd_iid_area_ubicacion = item.arubd_iid_area_ubicacion,
                            arprc_iid_codigo = Convert.ToInt32(item.arprc_iid_codigo),
                            arubd_codigo = Convert.ToInt32(item.arubd_codigo),
                            arubd_vcodigo = (TipoRegistro == 2 || TipoRegistro == 1 ||
                                                      TipoRegistro == 3 || TipoRegistro == 5 ||
                                                      TipoRegistro == 8 || TipoRegistro == 9 ||
                                                      TipoRegistro == 10 || TipoRegistro == 7) ? string.Format("{0:000}", item.arubd_codigo) :
                                                                            string.Format("{0:000}", item.arubd_codigo),
                            descripcion = item.arubd_vdescripcion,
                            Estado = Convert.ToChar(item.arubd_cestado)
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
        public void InsertarAreaUbicacion(EAreaUbicacion obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_AREA_UBICACION_INSERTAR(
                       obj.arprc_iid_codigo,
                        obj.arubd_codigo,
                        obj.descripcion,
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
        public void ActualizarAreaUbicacion(EAreaUbicacion obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_AREA_UBICACION_MODIFICAR(
                          obj.arubd_iid_area_ubicacion,
                        obj.descripcion,
                        obj.Estado,
                        obj.intUsuario,
                        obj.strPc);
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
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_AREA_UBICACION_ELIMINAR(
                        obj.arubd_iid_area_ubicacion,
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
        #region Area Empresa
        public List<EAreaEmpresa> listarAreaEmpresa()
        {
            List<EAreaEmpresa> lista = new List<EAreaEmpresa>(); ;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_AREA_EMPRESA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EAreaEmpresa()
                        {
                            aremc_iid_codigo = item.aremc_iid_codigo,
                            aremc_vdescripcion = item.aremc_vdescripcion.Trim(),
                            aremc_cestado = Convert.ToChar(item.aremc_cestado),
                            strEstado = (item.aremc_cestado == 'A') ? "Activo" : "Inactivo"
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
        public int insertarAreaEmpresa(EAreaEmpresa obj)
        {
            int? intIcod = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_AREA_EMPRESA_INSERTAR(
                        ref intIcod,
                        obj.aremc_vdescripcion,
                        obj.aremc_vabreviatura,
                        obj.intUsuario,
                        obj.strPc,
                        obj.aremc_cestado
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarAreaEmpresa(EAreaEmpresa obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_AREA_EMPRESA_MODIFICAR(
                        obj.aremc_iid_codigo,
                        obj.aremc_vdescripcion,
                        obj.aremc_vabreviatura,
                        obj.intUsuario,
                        obj.strPc,
                        obj.aremc_cestado
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarAreaEmpresa(EAreaEmpresa obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_AREA_EMPRESA_ELIMINAR(
                        obj.aremc_iid_codigo,
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
        #region Sub Area Empresa
        public List<ESubAreaEmpresa> ListarSubAreaEmpresa(ESubAreaEmpresa obj)
        {
            List<ESubAreaEmpresa> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<ESubAreaEmpresa>();
                    var query = dc.SGE_SUB_AREA_EMPRESA_LISTAR(obj.aremc_iid_codigo);
                    int TipoRegistro = obj.aremc_iid_codigo;
                    foreach (var item in query)
                    {
                        lista.Add(new ESubAreaEmpresa()
                        {

                            aremd_iid_sub_area_empresa = item.aremd_iid_sub_area_empresa,
                            aremc_iid_codigo = Convert.ToInt32(item.aremc_iid_codigo),
                            aremd_codigo = Convert.ToInt32(item.aremd_codigo),
                            aremd_vcodigo = (TipoRegistro == 2 || TipoRegistro == 1 ||
                                                      TipoRegistro == 3 || TipoRegistro == 5 ||
                                                      TipoRegistro == 8 || TipoRegistro == 9 ||
                                                      TipoRegistro == 10 || TipoRegistro == 7) ? string.Format("{0:000}", item.aremd_codigo) :
                                                                            string.Format("{0:000}", item.aremd_codigo),
                            descripcion = item.aremd_vdescripcion,
                            Estado = Convert.ToChar(item.aremd_cestado)
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
        public void InsertarSubAreaEmpresa(ESubAreaEmpresa obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_SUB_AREA_EMPRESA_INSERTAR(
                       obj.aremc_iid_codigo,
                        obj.aremd_codigo,
                        obj.descripcion,
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
        public void ActualizarSubAreaEmpresa(ESubAreaEmpresa obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_SUB_AREA_EMPRESA_MODIFICAR(
                          obj.aremd_iid_sub_area_empresa,
                        obj.descripcion,
                        obj.Estado,
                        obj.intUsuario,
                        obj.strPc);
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
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_SUB_AREA_EMPRESA_ELIMINAR(
                        obj.aremd_iid_sub_area_empresa,
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
        #region Area Descripcion
        public List<EAreaDescripcion> ListarAreaDescripcion(EAreaDescripcion obj)
        {
            List<EAreaDescripcion> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EAreaDescripcion>();
                    var query = dc.SGE_AREA_DESCRIPCION_LISTAR(obj.arprc_iid_codigo);
                    int TipoRegistro = obj.arprc_iid_codigo;
                    foreach (var item in query)
                    {
                        lista.Add(new EAreaDescripcion()
                        {

                            arded_iid_area_descripcion = item.arded_iid_area_descripcion,
                            arprc_iid_codigo = Convert.ToInt32(item.arprc_iid_codigo),
                            arded_codigo = Convert.ToInt32(item.arded_codigo),
                            arded_vcodigo = (TipoRegistro == 2 || TipoRegistro == 1 ||
                                                      TipoRegistro == 3 || TipoRegistro == 5 ||
                                                      TipoRegistro == 8 || TipoRegistro == 9 ||
                                                      TipoRegistro == 10 || TipoRegistro == 7) ? string.Format("{0:000}", item.arded_codigo) :
                                                                            string.Format("{0:000}", item.arded_codigo),
                            descripcion = item.arded_vdescripcion,
                            Estado = Convert.ToChar(item.arded_cestado)
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
        public void InsertarAreaDescripcion(EAreaDescripcion obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_AREA_DESCRIPCION_INSERTAR(
                       obj.arprc_iid_codigo,
                        obj.arded_codigo,
                        obj.descripcion,
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
        public void ActualizarAreaDescripcion(EAreaDescripcion obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_AREA_DESCRIPCION_MODIFICAR(
                          obj.arded_iid_area_descripcion,
                        obj.descripcion,
                        obj.Estado,
                        obj.intUsuario,
                        obj.strPc);
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
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_AREA_DESCRIPCION_ELIMINAR(
                        obj.arded_iid_area_descripcion,
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
        #region Notas de Ingreso

        public List<EProdNotaIngresoDetalleAlmacen> ListarProdGuiaIngresoReporte(EProdReporte obj)
        {
            List<EProdNotaIngresoDetalleAlmacen> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdNotaIngresoDetalleAlmacen>();
                    var query = dc.SGE_PVT_GUIA_INGRESO_REPORTE_LISTAR(obj.icod_almacen, obj.sfechaInicio, obj.sfechaFinal, obj.puvec_icod_punto_venta);
                    foreach (var nitem in query)
                    {
                        lista.Add(new EProdNotaIngresoDetalleAlmacen()
                        {
                            numero_icod_nota_ingreso = Convert.ToInt32(nitem.ningc_inumero_nota_ingreso),
                            id_motivo = Convert.ToInt32(nitem.ningc_iid_motivo),
                            descripcion_motivo = nitem.tarec_vdescripcion,
                            fecha_nota_ingreso = Convert.ToDateTime(nitem.ningc_sfecha_nota_ingreso),
                            id_tipo_doc = Convert.ToInt32(nitem.ningc_iid_tipo_doc),
                            descripcion_tipo_doc = nitem.tdocc_vdescripcion,
                            numero_doc = nitem.ningc_inumero_doc,
                            referencia = nitem.ningc_vreferencia,
                            vNumeroDocumento = nitem.tdocc_vdescripcion + "-" + nitem.ningc_inumero_doc,
                            observaciones = nitem.ningc_vobservaciones,

                            item = Convert.ToInt32(nitem.dninc_iitem),
                            descripcion_producto = nitem.pr_vdescripcion_producto,
                            descripcion_unidad_medida = nitem.unidad,
                            vcodigo_externo = nitem.dninc_vcodigo_externo,
                            cantidad = Convert.ToDecimal(nitem.dninc_ncantidad),

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
        public List<EProdNotaIngresoAlmacen> ListarProdNotaIngresoAlmacenFecha(EProdNotaIngresoAlmacen obj)
        {
            List<EProdNotaIngresoAlmacen> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdNotaIngresoAlmacen>();
                    var query = dc.SGE_PVT_GUIA_INGRESO_LISTAR(obj.id_almacen, obj.fecha_inicio, obj.fecha_fin, obj.puvec_icod_punto_venta);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdNotaIngresoAlmacen()
                        {
                            numero_nota_ingreso = Convert.ToInt32(item.ningc_inumero_nota_ingreso),
                            numerof_nota_ingreso = String.Format("{0:000000}", item.ningc_inumero_nota_ingreso),
                            id_almacen = Convert.ToInt32(item.ningc_iid_almacen),
                            id_motivo = Convert.ToInt32(item.ningc_iid_motivo),
                            descripcion_motivo = item.tarec_vdescripcion,
                            fecha_nota_ingreso = Convert.ToDateTime(item.ningc_sfecha_nota_ingreso),
                            id_tipo_doc = Convert.ToInt32(item.ningc_iid_tipo_doc),
                            descripcion_tipo_doc = item.tdocc_vdescripcion,
                            numero_doc = item.ningc_inumero_doc,
                            referencia = item.ningc_vreferencia,
                            observaciones = item.ningc_vobservaciones,
                            descripcion_almacen = item.almac_vdescripcion,
                            numero_icod_nota_ingreso = item.ningc_icod_nota_ingreso_almacen,
                            vid_almacen = string.Format("{0:00}", item.almac_iid_almacen),
                            vNumeroDocumento = item.tdocc_vdescripcion + "-" + item.ningc_inumero_doc
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
        public int EliminarProdNotaIngresoAlmacen(int Code)
        {
            int intResultado = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_GUIA_INGRESO_ELIMINAR(Code);

                }
                intResultado = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intResultado;
        }
        public int InsertarProdNotaIngresoAlmacen(EProdNotaIngresoAlmacen objENotaIngreso, List<EProdNotaIngresoDetalleAlmacen> mlist)
        {
            int intIdNotaIngreso = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    int? intId = 0;
                    dc.SGE_PVT_GUIA_INGRESO_INSERTAR(
                        ref intId,
                        objENotaIngreso.numero_nota_ingreso,
                        objENotaIngreso.id_almacen,
                        objENotaIngreso.id_motivo,
                        objENotaIngreso.puvec_icod_punto_venta,
                        objENotaIngreso.fecha_nota_ingreso,
                        objENotaIngreso.id_tipo_doc,
                        objENotaIngreso.numero_doc,
                        objENotaIngreso.referencia,
                        objENotaIngreso.observaciones,
                        objENotaIngreso.intUsuario
                    );
                    intIdNotaIngreso = Convert.ToInt32(intId);


                }
                EProdKardex objk = new EProdKardex();
                foreach (EProdNotaIngresoDetalleAlmacen obj in mlist)
                {
                    obj.icod_nota_ingreso_detalle = intIdNotaIngreso;

                    if (obj.Operacion == 1)
                    {
                        objk.kardx_sfecha = objENotaIngreso.fecha_nota_ingreso;
                        objk.iid_almacen = objENotaIngreso.id_almacen;
                        objk.iid_producto = obj.pr_icod_producto;
                        objk.Cantidad = obj.cantidad;
                        objk.stocc_codigo_producto = obj.vcodigo_externo;
                        objk.NroNota = objENotaIngreso.numero_nota_ingreso;
                        objk.iid_tipo_doc = objENotaIngreso.id_tipo_doc;
                        objk.Item = obj.item;
                        objk.NroDocumento = objENotaIngreso.numero_doc;
                        objk.movimiento = 0;
                        objk.Motivo = objENotaIngreso.id_motivo;
                        objk.Beneficiario = objENotaIngreso.referencia;
                        objk.Observaciones = objENotaIngreso.observaciones;
                        objk.puvec_icod_punto_venta = objENotaIngreso.puvec_icod_punto_venta;
                        objk.intUsuario = Convert.ToInt32(objENotaIngreso.intUsuario);
                        objk.operacion = 1;
                        objk.kardc_iid_correlativo = InsertarKardexpvt(objk);
                        obj.dninc_kardex = objk.kardc_iid_correlativo;
                    }
                }
                InsertarProdNotaIngresoDetalleAlmacen(mlist);
                return intIdNotaIngreso;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<EProdNotaIngreso> ListarProdNotaIngresoXfechaAlmacen(EProdReporte obj)
        {
            List<EProdNotaIngreso> lista = new List<EProdNotaIngreso>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_NOTA_INGRESO_CONSULTA_FECHAS_LISTAR(obj.sfechaInicio, obj.sfechaFinal, obj.icod_almacen);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdNotaIngreso()
                        {
                            ningc_icod_nota_ingreso = item.ningc_icod_nota_ingreso,
                            ningc_inumero_nota_ingreso = item.ningc_inumero_nota_ingreso,
                            ningc_vnumero_nota_ingreso = string.Format("{0:000000}", item.ningc_inumero_nota_ingreso),
                            ningc_sfecha_nota_ingreso = item.ningc_sfecha_nota_ingreso,
                            tablc_iid_motivo = item.tablc_iid_motivo,
                            descripcionMotivo = item.descripcionMotivo,
                            ningc_iid_almacen = item.ningc_iid_almacen,
                            ningc_vdescripcion_almacen = item.almac_vdescripcion,
                            ningc_iid_tipo_doc = item.ningc_iid_tipo_doc,
                            ningc_vdescripcion_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            ningc_inumero_doc = item.ningc_inumero_doc,
                            ningc_vreferencia = item.ningc_vreferencia,
                            ningc_vobservaciones = item.ningc_vobservaciones,
                            ningc_sfecha_compra = item.ningc_sfecha_compra,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,

                            ningc_iactivo = item.ningc_iactivo,
                            ningc_viid_almacen = string.Format("{0:00}", item.almac_iid_almacen),
                            proc_vcod_proveedor = item.proc_vcod_proveedor,
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            Documento = item.tdocc_vabreviatura_tipo_doc + "-" + item.ningc_inumero_doc,
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            cantidadNota = Convert.ToDecimal(item.cantidadNota)
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
        public List<EProdNotaIngreso> ListarProdNotaIngreso(int intfechainicio)
        {
            List<EProdNotaIngreso> lista = new List<EProdNotaIngreso>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_NOTA_INGRESO_LISTAR(intfechainicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdNotaIngreso()
                        {
                            ningc_icod_nota_ingreso = item.ningc_icod_nota_ingreso,
                            ningc_inumero_nota_ingreso = item.ningc_inumero_nota_ingreso,
                            ningc_vnumero_nota_ingreso = string.Format("{0:000000}", item.ningc_inumero_nota_ingreso),
                            ningc_sfecha_nota_ingreso = item.ningc_sfecha_nota_ingreso,
                            tablc_iid_motivo = item.tablc_iid_motivo,
                            puvec_icod_punto_venta = item.puvec_icod_punto_venta,
                            ningc_iid_almacen = item.ningc_iid_almacen,
                            ningc_vdescripcion_almacen = item.almac_vdescripcion,
                            ningc_iid_tipo_doc = item.ningc_iid_tipo_doc,
                            ningc_vdescripcion_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            ningc_inumero_doc = item.ningc_inumero_doc,
                            ningc_vreferencia = item.ningc_vreferencia,
                            ningc_vobservaciones = item.ningc_vobservaciones,
                            ningc_sfecha_compra = item.ningc_sfecha_compra,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,

                            ningc_iactivo = item.ningc_iactivo,
                            ningc_viid_almacen = string.Format("{0:00}", item.almac_iid_almacen),
                            proc_vcod_proveedor = item.proc_vcod_proveedor,
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            Documento = item.tdocc_vabreviatura_tipo_doc + "-" + item.ningc_inumero_doc,
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            orpec_iid_orden_pedido = item.orpec_iid_orden_pedido,
                            numero_pedido = item.numero_pedido,
                            orped_icod_item_orden_pedido = item.orped_icod_item_orden_pedido,
                            numero_item_pedido = item.numero_item_pedido
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
        public int InsertarProdNotaIngreso(EProdNotaIngreso obj)
        {
            int intIdNota = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    int? intId = 0;
                    dc.SGE_PVT_NOTA_INGRESO_INSERTAR(
                         ref intId,
                        obj.ningc_inumero_nota_ingreso,
                        obj.ningc_sfecha_nota_ingreso,
                        obj.puvec_icod_punto_venta,
                        obj.tablc_iid_motivo,
                        obj.ningc_iid_almacen,
                        obj.ningc_iid_tipo_doc,
                        obj.ningc_inumero_doc,
                        obj.ningc_vreferencia,
                        obj.ningc_vobservaciones,
                        obj.ningc_sfecha_compra,
                        obj.tablc_iid_tipo_moneda,
                        obj.intUsuario,
                        obj.proc_icod_proveedor,
                        obj.orpec_iid_orden_pedido,
                        obj.orped_icod_item_orden_pedido
                        );
                    intIdNota = Convert.ToInt32(intId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intIdNota;
        }
        public void ActualizarProdNotaIngreso(EProdNotaIngreso obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_NOTA_INGRESO_ACTUALIZAR(
                        obj.ningc_icod_nota_ingreso,
                        obj.ningc_sfecha_nota_ingreso,
                        obj.ningc_inumero_doc,
                        obj.ningc_vreferencia,
                        obj.ningc_vobservaciones,
                        obj.ningc_sfecha_compra,
                        obj.tablc_iid_tipo_moneda,
                        obj.intUsuario,
                        obj.orpec_iid_orden_pedido,
                        obj.orped_icod_item_orden_pedido
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarProdNotaIngreso(EProdNotaIngreso obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_NOTA_INGRESO_ELIMINAR(obj.ningc_icod_nota_ingreso);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Notas de Ingreso Detalle

        public List<EProdNotaIngresoDetalleAlmacen> ListarProdNotaIngresoDetalleAlmacen(int Code)
        {
            List<EProdNotaIngresoDetalleAlmacen> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdNotaIngresoDetalleAlmacen>();
                    //var query = dc.SIGC_NotaIngresoDetalle_LISTAR(objENotaIngresoDetalle.Codigo, objTIpoCodigoRelacion.Descripcion, objTIpoCodigoRelacion.Estado);
                    var query = dc.SGE_PVT_GUIA_INGRESO_DETALLE_LISTAR(Code);
                    foreach (var nitem in query)
                    {
                        lista.Add(new EProdNotaIngresoDetalleAlmacen()
                        {
                            icod_nota_ingreso_detalle = nitem.dninc_icod_detalle_ingreso_almacen,
                            item = Convert.ToInt32(nitem.dninc_iitem),
                            numero_nota_ingreso = Convert.ToInt32(nitem.dninc_inumero_nota_ingreso),
                            id_almacen = Convert.ToInt32(nitem.dninc_iid_almacen),
                            pr_icod_producto = Convert.ToInt32(nitem.pr_icod_producto),
                            descripcion_producto = nitem.pr_vdescripcion_producto,
                            descripcion_unidad_medida = "PAR",
                            vcodigo_externo = nitem.dninc_vcodigo_externo,
                            cantidad = Convert.ToDecimal(nitem.dninc_ncantidad),
                            dninc_kardex = nitem.dninc_kardex
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
        public void InsertarProdNotaIngresoDetalleAlmacen(List<EProdNotaIngresoDetalleAlmacen> mlist)
        {

            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    mlist.ForEach(obj =>
                    {
                        if (obj.Operacion == 1)
                        {
                            dc.SGE_PVT_GUIA_INGRESO_DETALLE_INSERTAR(
                                obj.item,
                                obj.icod_nota_ingreso_detalle,
                                obj.id_almacen,
                                obj.vcodigo_externo,
                                obj.cantidad,
                                obj.intUsuario,
                                obj.dninc_kardex
                             );
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return intIdNotaIngresoDetalle;
        }
        public List<EProdNotaIngresoDetalle> ListarProdNotaIngresoDetalle(int Code)
        {
            List<EProdNotaIngresoDetalle> lista = new List<EProdNotaIngresoDetalle>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.CommandTimeout = 30;
                    var query = dc.SGE_PVT_NOTA_INGRESO_DETALLE_LISTAR(Code);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdNotaIngresoDetalle()
                        {
                            ningcd_icod_nota_ingreso = item.ningcd_icod_nota_ingreso,
                            ningc_icod_nota_ingreso = item.ningc_icod_nota_ingreso,
                            ningc_iid_almacen = item.ningc_iid_almacen,
                            ningcd_num_item = item.ningcd_num_item,
                            tablc_iid_motivo = item.tablc_iid_motivo,
                            pr_vcodigo_externo = item.pr_vcodigo_externo,
                            pr_vdescripcion_producto = item.pr_vdescripcion_producto,
                            pr_ncant_total_producto = item.pr_ncant_total_producto,
                            ningcd_rango_talla = item.ningcd_rango_talla,
                            ningcd_iid_moneda = item.ningcd_iid_moneda,
                            ningcd_monto_unit = item.ningcd_monto_unit,
                            ningcd_talla1 = item.ningcd_talla1,
                            ningcd_talla2 = item.ningcd_talla2,
                            ningcd_talla3 = item.ningcd_talla3,
                            ningcd_talla4 = item.ningcd_talla4,
                            ningcd_talla5 = item.ningcd_talla5,
                            ningcd_talla6 = item.ningcd_talla6,
                            ningcd_talla7 = item.ningcd_talla7,
                            ningcd_talla8 = item.ningcd_talla8,
                            ningcd_talla9 = item.ningcd_talla9,
                            ningcd_talla10 = item.ningcd_talla10,
                            ningcd_cant_talla1 = item.ningcd_cant_talla1,
                            ningcd_cant_talla2 = item.ningcd_cant_talla2,
                            ningcd_cant_talla3 = item.ningcd_cant_talla3,
                            ningcd_cant_talla4 = item.ningcd_cant_talla4,
                            ningcd_cant_talla5 = item.ningcd_cant_talla5,
                            ningcd_cant_talla6 = item.ningcd_cant_talla6,
                            ningcd_cant_talla7 = item.ningcd_cant_talla7,
                            ningcd_cant_talla8 = item.ningcd_cant_talla8,
                            ningcd_cant_talla9 = item.ningcd_cant_talla9,
                            ningcd_cant_talla10 = item.ningcd_cant_talla10,
                            ningcd_iid_kardex1 = item.ningcd_iid_kardex1,
                            ningcd_iid_kardex2 = item.ningcd_iid_kardex2,
                            ningcd_iid_kardex3 = item.ningcd_iid_kardex3,
                            ningcd_iid_kardex4 = item.ningcd_iid_kardex4,
                            ningcd_iid_kardex5 = item.ningcd_iid_kardex5,
                            ningcd_iid_kardex6 = item.ningcd_iid_kardex6,
                            ningcd_iid_kardex7 = item.ningcd_iid_kardex7,
                            ningcd_iid_kardex8 = item.ningcd_iid_kardex8,
                            ningcd_iid_kardex9 = item.ningcd_iid_kardex9,
                            ningcd_iid_kardex10 = item.ningcd_iid_kardex10,

                            ningcd_iactivo = item.ningcd_iactivo,
                            ningcd_monto_total = item.ningcd_monto_unit * item.pr_ncant_total_producto

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
        public void InsertarProdNotaIngresoDetalle(EProdNotaIngresoDetalle obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_NOTA_INGRESO_DETALLE_INSERTAR(
                        obj.ningc_icod_nota_ingreso,
                        obj.ningc_iid_almacen,
                        obj.ningcd_num_item,
                        obj.tablc_iid_motivo,
                        obj.pr_vcodigo_externo,
                        obj.pr_vdescripcion_producto,
                        obj.pr_ncant_total_producto,
                        obj.ningcd_rango_talla,
                        obj.ningcd_iid_moneda,
                        obj.ningcd_monto_unit,
                        obj.ningcd_talla1,
                        obj.ningcd_talla2,
                        obj.ningcd_talla3,
                        obj.ningcd_talla4,
                        obj.ningcd_talla5,
                        obj.ningcd_talla6,
                        obj.ningcd_talla7,
                        obj.ningcd_talla8,
                        obj.ningcd_talla9,
                        obj.ningcd_talla10,
                        obj.ningcd_cant_talla1,
                        obj.ningcd_cant_talla2,
                        obj.ningcd_cant_talla3,
                        obj.ningcd_cant_talla4,
                        obj.ningcd_cant_talla5,
                        obj.ningcd_cant_talla6,
                        obj.ningcd_cant_talla7,
                        obj.ningcd_cant_talla8,
                        obj.ningcd_cant_talla9,
                        obj.ningcd_cant_talla10,
                        obj.ningcd_iid_kardex1,
                        obj.ningcd_iid_kardex2,
                        obj.ningcd_iid_kardex3,
                        obj.ningcd_iid_kardex4,
                        obj.ningcd_iid_kardex5,
                        obj.ningcd_iid_kardex6,
                        obj.ningcd_iid_kardex7,
                        obj.ningcd_iid_kardex8,
                        obj.ningcd_iid_kardex9,
                        obj.ningcd_iid_kardex10,
                        obj.intUsuario
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarProdNotaIngresoDetalle(EProdNotaIngresoDetalle obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_NOTA_INGRESO_DETALLE_ACTUALIZAR(
                obj.ningcd_icod_nota_ingreso,
                obj.ningc_iid_almacen,
                obj.ningcd_num_item,
                obj.tablc_iid_motivo,
                obj.pr_vcodigo_externo,
                obj.pr_vdescripcion_producto,
                obj.pr_ncant_total_producto,
                obj.ningcd_rango_talla,
                obj.ningcd_iid_moneda,
                obj.ningcd_monto_unit,
                obj.ningcd_talla1,
                obj.ningcd_talla2,
                obj.ningcd_talla3,
                obj.ningcd_talla4,
                obj.ningcd_talla5,
                obj.ningcd_talla6,
                obj.ningcd_talla7,
                obj.ningcd_talla8,
                obj.ningcd_talla9,
                obj.ningcd_talla10,
                obj.ningcd_cant_talla1,
                obj.ningcd_cant_talla2,
                obj.ningcd_cant_talla3,
                obj.ningcd_cant_talla4,
                obj.ningcd_cant_talla5,
                obj.ningcd_cant_talla6,
                obj.ningcd_cant_talla7,
                obj.ningcd_cant_talla8,
                obj.ningcd_cant_talla9,
                obj.ningcd_cant_talla10,
                obj.ningcd_iid_kardex1,
                obj.ningcd_iid_kardex2,
                obj.ningcd_iid_kardex3,
                obj.ningcd_iid_kardex4,
                obj.ningcd_iid_kardex5,
                obj.ningcd_iid_kardex6,
                obj.ningcd_iid_kardex7,
                obj.ningcd_iid_kardex8,
                obj.ningcd_iid_kardex9,
                obj.ningcd_iid_kardex10,
                obj.intUsuario);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarProdNotaIngresoDetalle(EProdNotaIngresoDetalle obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_NOTA_INGRESO_DETALLE_ELIMINAR(obj.ningcd_icod_nota_ingreso);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EProdNotaIngresoDetalle> ListarProdNotaIngresoXfechaAlmacenDetalle(EProdReporte obj)
        {
            List<EProdNotaIngresoDetalle> lista = new List<EProdNotaIngresoDetalle>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_NOTA_INGRESO_CONSULTA_FECHAS_DETALLE_LISTAR(obj.sfechaInicio, obj.sfechaFinal, obj.icod_almacen);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdNotaIngresoDetalle()
                        {
                            ningc_icod_nota_ingreso = item.ningc_icod_nota_ingreso,
                            ningc_inumero_nota_ingreso = item.ningc_inumero_nota_ingreso,
                            ningc_vnumero_nota_ingreso = string.Format("{0:000000}", item.ningc_inumero_nota_ingreso),
                            ningc_sfecha_nota_ingreso = item.ningc_sfecha_nota_ingreso,
                            tablc_iid_motivo = item.tablc_iid_motivo,
                            descripcionMotivo = item.descripcionMotivo,
                            ningc_vdescripcion_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            ningc_inumero_doc = item.ningc_inumero_doc,
                            ningc_vreferencia = item.ningc_vreferencia,
                            ningc_vobservaciones = item.ningc_vobservaciones,
                            Documento = item.tdocc_vabreviatura_tipo_doc + "-" + item.ningc_inumero_doc,
                            ningcd_num_item = item.ningcd_num_item,
                            pr_vcodigo_externo = item.pr_vcodigo_externo,
                            pr_vdescripcion_producto = item.pr_vdescripcion_producto,
                            pr_ncant_total_producto = item.pr_ncant_total_producto,
                            ningcd_talla1 = item.ningcd_talla1,
                            ningcd_talla2 = item.ningcd_talla2,
                            ningcd_talla3 = item.ningcd_talla3,
                            ningcd_talla4 = item.ningcd_talla4,
                            ningcd_talla5 = item.ningcd_talla5,
                            ningcd_talla6 = item.ningcd_talla6,
                            ningcd_talla7 = item.ningcd_talla7,
                            ningcd_talla8 = item.ningcd_talla8,
                            ningcd_talla9 = item.ningcd_talla9,
                            ningcd_talla10 = item.ningcd_talla10,
                            ningcd_cant_talla1 = item.ningcd_cant_talla1,
                            ningcd_cant_talla2 = item.ningcd_cant_talla2,
                            ningcd_cant_talla3 = item.ningcd_cant_talla3,
                            ningcd_cant_talla4 = item.ningcd_cant_talla4,
                            ningcd_cant_talla5 = item.ningcd_cant_talla5,
                            ningcd_cant_talla6 = item.ningcd_cant_talla6,
                            ningcd_cant_talla7 = item.ningcd_cant_talla7,
                            ningcd_cant_talla8 = item.ningcd_cant_talla8,
                            ningcd_cant_talla9 = item.ningcd_cant_talla9,
                            ningcd_cant_talla10 = item.ningcd_cant_talla10
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
        #region Notas de Salida

        public int EliminarProdNotaSalidaAlmacen(int Code)
        {
            int intResultado = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_GUIA_SALIDA_ELIMINAR(Code);

                }
                intResultado = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intResultado;
        }
        //public List<EProdNotaSalidaAlmacen> ListarProdNotaSalidaAlmacenXIcod(int icodNotaSalida)
        //{
        //    List<EProdNotaSalidaAlmacen> lista = null;
        //    try
        //    {
        //        using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
        //        {
        //            lista = new List<EProdNotaSalidaAlmacen>();
        //            var query = dc.SGE_PVT_NOTA_SALIDA_ALAMACEN_LISTAR_X_ICOD(icodNotaSalida);
        //            foreach (var item in query)
        //            {
        //                lista.Add(new EProdNotaSalidaAlmacen()
        //                {
        //                    numero_nota_salida = Convert.ToInt32(item.ningc_inumero_nota_salida),
        //                    numerof_nota_salida = String.Format("{0:000000}", item.ningc_inumero_nota_salida),
        //                    id_almacen = Convert.ToInt32(item.ningc_iid_almacen),
        //                    id_motivo = Convert.ToInt32(item.ningc_iid_motivo),
        //                    descripcion_motivo = item.tarec_vdescripcion,
        //                    fecha_nota_salida = Convert.ToDateTime(item.ningc_sfecha_nota_ingreso),
        //                    puvec_icod_punto_venta = Convert.ToInt32(item.puvec_icod_punto_venta),
        //                    id_tipo_doc = Convert.ToInt32(item.ningc_iid_tipo_doc),
        //                    descripcion_tipo_doc = item.tdocc_vdescripcion,
        //                    numero_doc = item.ningc_inumero_doc,
        //                    referencia = item.ningc_vreferencia,
        //                    observaciones = item.ningc_vobservaciones,
        //                    descripcion_almacen = item.almac_vdescripcion,
        //                    numero_icod_nota_salida = item.ningc_icod_nota_salida_almacen,
        //                    vid_almacen = string.Format("{0:00}", item.almac_iid_almacen)
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
        public int InsertarProdNotaSalidaAlmacen(EProdNotaSalidaAlmacen objENotaSalida, List<EProdNotaSalidaDetalleAlmacen> mlist)
        {
            int intIdNotaSalida = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    int? intId = 0;
                    dc.SGE_PVT_GUIA_SALIDA_INSERTAR(
                        ref intId,
                        objENotaSalida.numero_nota_salida,
                        objENotaSalida.id_almacen,
                        objENotaSalida.id_motivo,
                        objENotaSalida.puvec_icod_punto_venta,
                        objENotaSalida.fecha_nota_salida,
                        objENotaSalida.id_tipo_doc,
                        objENotaSalida.numero_doc,
                        objENotaSalida.referencia,
                        objENotaSalida.observaciones,
                        objENotaSalida.intUsuario
                    );
                    intIdNotaSalida = Convert.ToInt32(intId);


                }
                EProdKardex objk = new EProdKardex();
                foreach (EProdNotaSalidaDetalleAlmacen obj in mlist)
                {
                    obj.icod_nota_salida_detalle = intIdNotaSalida;

                    if (obj.Operacion == 1)
                    {
                        objk.kardx_sfecha = objENotaSalida.fecha_nota_salida;
                        objk.iid_almacen = objENotaSalida.id_almacen;
                        objk.iid_producto = obj.pr_icod_producto;
                        objk.Cantidad = obj.cantidad;
                        objk.stocc_codigo_producto = obj.vcodigo_externo;
                        objk.NroNota = objENotaSalida.numero_nota_salida;
                        objk.iid_tipo_doc = objENotaSalida.id_tipo_doc;
                        objk.Item = obj.item;
                        objk.NroDocumento = objENotaSalida.numero_doc;
                        objk.movimiento = 1;
                        objk.Motivo = objENotaSalida.id_motivo;
                        objk.Beneficiario = objENotaSalida.referencia;
                        objk.Observaciones = objENotaSalida.observaciones;
                        objk.puvec_icod_punto_venta = objENotaSalida.puvec_icod_punto_venta;
                        objk.intUsuario = Convert.ToInt32(objENotaSalida.intUsuario);
                        objk.operacion = 1;
                        objk.kardc_iid_correlativo = InsertarKardexpvt(objk);
                        obj.dninc_kardex = objk.kardc_iid_correlativo;
                    }
                }
                InsertarProdNotaSalidaDetalleAlmacen(mlist);
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
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdNotaSalidaAlmacen>();
                    var query = dc.SGE_PVT_GUIA_SALIDA_ALAMACEN_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EProdNotaSalidaAlmacen()
                        {
                            numero_nota_salida = Convert.ToInt32(item.ningc_inumero_nota_salida),
                            numerof_nota_salida = String.Format("{0:000000}", item.ningc_inumero_nota_salida),
                            id_almacen = Convert.ToInt32(item.ningc_iid_almacen),
                            id_motivo = Convert.ToInt32(item.ningc_iid_motivo),
                            descripcion_motivo = item.tarec_vdescripcion,
                            fecha_nota_salida = Convert.ToDateTime(item.ningc_sfecha_nota_ingreso),
                            id_tipo_doc = Convert.ToInt32(item.ningc_iid_tipo_doc),
                            descripcion_tipo_doc = item.tdocc_vdescripcion,
                            numero_doc = item.ningc_inumero_doc,
                            referencia = item.ningc_vreferencia,
                            observaciones = item.ningc_vobservaciones,
                            descripcion_almacen = item.almac_vdescripcion,
                            numero_icod_nota_salida = item.ningc_icod_nota_salida_almacen,
                            vid_almacen = string.Format("{0:00}", item.almac_iid_almacen)
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
        public List<EProdNotaSalida> ListarProdNotaSalidaXfechaAlmacen(EProdReporte obj)
        {
            List<EProdNotaSalida> lista = new List<EProdNotaSalida>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_NOTA_SALIDA_CONSULTA_FECHAS_LISTAR(obj.sfechaInicio, obj.sfechaFinal, obj.icod_almacen);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdNotaSalida()
                        {
                            salgc_icod_nota_salida = item.salgc_icod_nota_salida,
                            salgc_inumero_nota_salida = item.salgc_inumero_nota_salida,
                            salgc_vnumero_nota_salida = string.Format("{0:000000}", item.salgc_inumero_nota_salida),
                            salgc_sfecha_nota_salida = item.salgc_sfecha_nota_salida,
                            tablc_iid_motivo = item.tablc_iid_motivo,
                            descripcionMotivo = item.descripcionMotivo,
                            salgc_iid_almacen = item.salgc_iid_almacen,
                            salgc_vdescripcion_almacen = item.almac_vdescripcion,
                            salgc_iid_tipo_doc = item.salgc_iid_tipo_doc,
                            salgc_vdescripcion_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            salgc_inumero_doc = item.salgc_inumero_doc,
                            salgc_vreferencia = item.salgc_vreferencia,
                            salgc_vobservaciones = item.salgc_vobservaciones,

                            salgc_iactivo = item.salgc_iactivo,
                            salgc_viid_almacen = string.Format("{0:00}", item.almac_iid_almacen),
                            Documento = item.tdocc_vabreviatura_tipo_doc + "-" + item.salgc_inumero_doc,
                            cantidadNota = item.cantidadNota
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
        public List<EProdNotaSalida> ListarProdNotaSalida(int intfechainicio)
        {
            List<EProdNotaSalida> lista = new List<EProdNotaSalida>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_NOTA_SALIDA_LISTAR(intfechainicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdNotaSalida()
                        {
                            salgc_icod_nota_salida = item.salgc_icod_nota_salida,
                            salgc_inumero_nota_salida = item.salgc_inumero_nota_salida,
                            salgc_vnumero_nota_salida = string.Format("{0:000000}", item.salgc_inumero_nota_salida),
                            salgc_sfecha_nota_salida = item.salgc_sfecha_nota_salida,
                            tablc_iid_motivo = item.tablc_iid_motivo,
                            puvec_icod_punto_venta = item.puvec_icod_punto_venta,
                            salgc_iid_almacen = item.salgc_iid_almacen,
                            salgc_vdescripcion_almacen = item.almac_vdescripcion,
                            salgc_iid_tipo_doc = item.salgc_iid_tipo_doc,
                            salgc_vdescripcion_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            salgc_inumero_doc = item.salgc_inumero_doc,
                            salgc_vreferencia = item.salgc_vreferencia,
                            salgc_vobservaciones = item.salgc_vobservaciones,

                            salgc_iactivo = item.salgc_iactivo,
                            salgc_viid_almacen = string.Format("{0:00}", item.almac_iid_almacen),
                            Documento = item.tdocc_vabreviatura_tipo_doc + "-" + item.salgc_inumero_doc,
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
        public int InsertarProdNotaSalida(EProdNotaSalida obj)
        {
            int intIdNota = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    int? intId = 0;
                    dc.SGE_PVT_NOTA_SALIDA_INSERTAR(
                         ref intId,
                        obj.salgc_inumero_nota_salida,
                        obj.salgc_sfecha_nota_salida,
                        obj.tablc_iid_motivo,
                        obj.puvec_icod_punto_venta,
                        obj.salgc_iid_almacen,
                        obj.salgc_iid_tipo_doc,
                        obj.salgc_inumero_doc,
                        obj.salgc_vreferencia,
                        obj.salgc_vobservaciones,
                        obj.intUsuario
                        );
                    intIdNota = Convert.ToInt32(intId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intIdNota;
        }
        public void ActualizarProdNotaSalida(EProdNotaSalida obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_NOTA_SALIDA_ACTUALIZAR(
                    obj.salgc_icod_nota_salida,
                        obj.salgc_sfecha_nota_salida,
                        obj.salgc_inumero_doc,
                        obj.salgc_vreferencia,
                        obj.salgc_vobservaciones,
                        obj.intUsuario);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarProdNotaSalida(EProdNotaSalida obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_NOTA_SALIDA_ELIMINAR(obj.salgc_icod_nota_salida);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Notas de Salida Detalle

        public int EliminarProdNotaSalidaDetalleAlmacen(List<EProdNotaSalidaDetalleAlmacen> mlistdelete)
        {
            int intResultado = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    foreach (EProdNotaSalidaDetalleAlmacen obj in mlistdelete)
                    {
                        dc.SGE_PVT_GUIA_SALIDA_DETALLE_ELIMINAR(obj.icod_nota_salida_detalle);
                    }
                }
                intResultado = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intResultado;
        }
        public List<EProdNotaSalidaDetalleAlmacen> ListarProdNotaSalidaDetalleAlmacen(int Code)
        {
            List<EProdNotaSalidaDetalleAlmacen> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdNotaSalidaDetalleAlmacen>();
                    var query = dc.SGE_PVT_GUIA_SALIDA_DETALLE_LISTAR(Code);
                    foreach (var nitem in query)
                    {
                        lista.Add(new EProdNotaSalidaDetalleAlmacen()
                        {
                            icod_nota_salida_detalle = nitem.dninc_icod_detalle_salida_almacen,
                            item = Convert.ToInt32(nitem.dninc_iitem),
                            numero_nota_salida = Convert.ToInt32(nitem.dninc_inumero_nota_salida),
                            id_almacen = Convert.ToInt32(nitem.dninc_iid_almacen),
                            pr_icod_producto = Convert.ToInt32(nitem.pr_icod_producto),
                            descripcion_producto = nitem.pr_vdescripcion_producto,
                            descripcion_unidad_medida = "PAR",
                            vcodigo_externo = nitem.dninc_vcodigo_externo,
                            cantidad = Convert.ToDecimal(nitem.dninc_ncantidad),
                            dninc_kardex = nitem.dninc_kardex
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
        public void InsertarProdNotaSalidaDetalleAlmacen(List<EProdNotaSalidaDetalleAlmacen> mlist)
        {

            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    mlist.ForEach(obj =>
                    {
                        if (obj.Operacion == 1)
                        {
                            dc.SGE_PVT_GUIA_SALIDA_DETALLE_INSERTAR(
                                obj.item,
                                obj.icod_nota_salida_detalle,
                                obj.id_almacen,
                                obj.vcodigo_externo,
                                obj.cantidad,
                                obj.intUsuario,
                                obj.dninc_kardex);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return intIdNotaSalidaDetalle;
        }
        public List<EProdNotaSalidaDetalle> ListarProdNotaSalidaXfechaAlmacenDetalle(EProdReporte obj)
        {
            List<EProdNotaSalidaDetalle> lista = new List<EProdNotaSalidaDetalle>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_NOTA_SALIDA_CONSULTA_FECHAS_DETALLE_LISTAR(obj.sfechaInicio, obj.sfechaFinal, obj.icod_almacen);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdNotaSalidaDetalle()
                        {
                            salgc_icod_nota_salida = item.salgc_icod_nota_salida,
                            salgc_inumero_nota_salida = item.salgc_inumero_nota_salida,
                            salgc_vnumero_nota_salida = string.Format("{0:000000}", item.salgc_inumero_nota_salida),
                            salgc_sfecha_nota_salida = item.salgc_sfecha_nota_salida,
                            tablc_iid_motivo = item.tablc_iid_motivo,
                            descripcionMotivo = item.descripcionMotivo,
                            salgc_vdescripcion_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            salgc_inumero_doc = item.salgc_inumero_doc,
                            salgc_vreferencia = item.salgc_vreferencia,
                            salgc_vobservaciones = item.salgc_vobservaciones,
                            Documento = item.tdocc_vabreviatura_tipo_doc + "-" + item.salgc_inumero_doc,
                            salgcd_num_item = item.salgcd_num_item,
                            pr_vcodigo_externo = item.pr_vcodigo_externo,
                            pr_vdescripcion_producto = item.pr_vdescripcion_producto,
                            pr_ncant_total_producto = item.pr_ncant_total_producto,
                            salgcd_talla1 = item.salgcd_talla1,
                            salgcd_talla2 = item.salgcd_talla2,
                            salgcd_talla3 = item.salgcd_talla3,
                            salgcd_talla4 = item.salgcd_talla4,
                            salgcd_talla5 = item.salgcd_talla5,
                            salgcd_talla6 = item.salgcd_talla6,
                            salgcd_talla7 = item.salgcd_talla7,
                            salgcd_talla8 = item.salgcd_talla8,
                            salgcd_talla9 = item.salgcd_talla9,
                            salgcd_talla10 = item.salgcd_talla10,
                            salgcd_cant_talla1 = item.salgcd_cant_talla1,
                            salgcd_cant_talla2 = item.salgcd_cant_talla2,
                            salgcd_cant_talla3 = item.salgcd_cant_talla3,
                            salgcd_cant_talla4 = item.salgcd_cant_talla4,
                            salgcd_cant_talla5 = item.salgcd_cant_talla5,
                            salgcd_cant_talla6 = item.salgcd_cant_talla6,
                            salgcd_cant_talla7 = item.salgcd_cant_talla7,
                            salgcd_cant_talla8 = item.salgcd_cant_talla8,
                            salgcd_cant_talla9 = item.salgcd_cant_talla9,
                            salgcd_cant_talla10 = item.salgcd_cant_talla10,
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
        public List<EProdNotaSalidaDetalle> ListarProdNotaSalidaReporte(int salgc_icod_nota_salida)
        {
            List<EProdNotaSalidaDetalle> lista = new List<EProdNotaSalidaDetalle>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_NOTA_SALIDA_REPORTE(salgc_icod_nota_salida);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdNotaSalidaDetalle()
                        {
                            salgc_icod_nota_salida = item.salgc_icod_nota_salida,
                            salgc_inumero_nota_salida = item.salgc_inumero_nota_salida,
                            salgc_vnumero_nota_salida = string.Format("{0:000000}", item.salgc_inumero_nota_salida),
                            salgc_sfecha_nota_salida = item.salgc_sfecha_nota_salida,
                            tablc_iid_motivo = item.tablc_iid_motivo,
                            descripcionMotivo = item.descripcionMotivo,
                            salgc_vdescripcion_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            salgc_inumero_doc = item.salgc_inumero_doc,
                            salgc_vreferencia = item.salgc_vreferencia,
                            salgc_vobservaciones = item.salgc_vobservaciones,
                            Documento = item.tdocc_vabreviatura_tipo_doc + "-" + item.salgc_inumero_doc,
                            salgcd_num_item = item.salgcd_num_item,
                            pr_vcodigo_externo = item.pr_vcodigo_externo,
                            pr_vdescripcion_producto = item.pr_vdescripcion_producto,
                            pr_ncant_total_producto = item.pr_ncant_total_producto,
                            salgcd_talla1 = item.salgcd_talla1,
                            salgcd_talla2 = item.salgcd_talla2,
                            salgcd_talla3 = item.salgcd_talla3,
                            salgcd_talla4 = item.salgcd_talla4,
                            salgcd_talla5 = item.salgcd_talla5,
                            salgcd_talla6 = item.salgcd_talla6,
                            salgcd_talla7 = item.salgcd_talla7,
                            salgcd_talla8 = item.salgcd_talla8,
                            salgcd_talla9 = item.salgcd_talla9,
                            salgcd_talla10 = item.salgcd_talla10,
                            salgcd_cant_talla1 = item.salgcd_cant_talla1,
                            salgcd_cant_talla2 = item.salgcd_cant_talla2,
                            salgcd_cant_talla3 = item.salgcd_cant_talla3,
                            salgcd_cant_talla4 = item.salgcd_cant_talla4,
                            salgcd_cant_talla5 = item.salgcd_cant_talla5,
                            salgcd_cant_talla6 = item.salgcd_cant_talla6,
                            salgcd_cant_talla7 = item.salgcd_cant_talla7,
                            salgcd_cant_talla8 = item.salgcd_cant_talla8,
                            salgcd_cant_talla9 = item.salgcd_cant_talla9,
                            salgcd_cant_talla10 = item.salgcd_cant_talla10,
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
        public List<EProdNotaSalidaDetalle> ListarProdNotaSalidaDetalle(int Code)
        {
            List<EProdNotaSalidaDetalle> lista = new List<EProdNotaSalidaDetalle>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_NOTA_SALIDA_DETALLE_LISTAR(Code);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdNotaSalidaDetalle()
                        {

                            salgcd_icod_nota_salida = item.salgcd_icod_nota_salida,
                            salgc_icod_nota_salida = item.salgc_icod_nota_salida,
                            salgc_iid_almacen = item.salgc_iid_almacen,
                            salgcd_num_item = item.salgcd_num_item,
                            tablc_iid_motivo = item.tablc_iid_motivo,
                            pr_vcodigo_externo = item.pr_vcodigo_externo,
                            pr_vdescripcion_producto = item.pr_vdescripcion_producto,
                            pr_ncant_total_producto = item.pr_ncant_total_producto,
                            salgcd_icod_serie = Convert.ToInt32(item.salgcd_icod_serie),
                            salgcd_rango_talla = item.salgcd_rango_talla,
                            salgcd_talla1 = item.salgcd_talla1,
                            salgcd_talla2 = item.salgcd_talla2,
                            salgcd_talla3 = item.salgcd_talla3,
                            salgcd_talla4 = item.salgcd_talla4,
                            salgcd_talla5 = item.salgcd_talla5,
                            salgcd_talla6 = item.salgcd_talla6,
                            salgcd_talla7 = item.salgcd_talla7,
                            salgcd_talla8 = item.salgcd_talla8,
                            salgcd_talla9 = item.salgcd_talla9,
                            salgcd_talla10 = item.salgcd_talla10,
                            salgcd_cant_talla1 = item.salgcd_cant_talla1,
                            salgcd_cant_talla2 = item.salgcd_cant_talla2,
                            salgcd_cant_talla3 = item.salgcd_cant_talla3,
                            salgcd_cant_talla4 = item.salgcd_cant_talla4,
                            salgcd_cant_talla5 = item.salgcd_cant_talla5,
                            salgcd_cant_talla6 = item.salgcd_cant_talla6,
                            salgcd_cant_talla7 = item.salgcd_cant_talla7,
                            salgcd_cant_talla8 = item.salgcd_cant_talla8,
                            salgcd_cant_talla9 = item.salgcd_cant_talla9,
                            salgcd_cant_talla10 = item.salgcd_cant_talla10,
                            salgcd_iid_kardex1 = item.salgcd_iid_kardex1,
                            salgcd_iid_kardex2 = item.salgcd_iid_kardex2,
                            salgcd_iid_kardex3 = item.salgcd_iid_kardex3,
                            salgcd_iid_kardex4 = item.salgcd_iid_kardex4,
                            salgcd_iid_kardex5 = item.salgcd_iid_kardex5,
                            salgcd_iid_kardex6 = item.salgcd_iid_kardex6,
                            salgcd_iid_kardex7 = item.salgcd_iid_kardex7,
                            salgcd_iid_kardex8 = item.salgcd_iid_kardex8,
                            salgcd_iid_kardex9 = item.salgcd_iid_kardex9,
                            salgcd_iid_kardex10 = item.salgcd_iid_kardex10,
                            salgcd_icod_producto1 = Convert.ToInt32(item.salgcd_icod_producto1),
                            salgcd_icod_producto2 = Convert.ToInt32(item.salgcd_icod_producto2),
                            salgcd_icod_producto3 = Convert.ToInt32(item.salgcd_icod_producto3),
                            salgcd_icod_producto4 = Convert.ToInt32(item.salgcd_icod_producto4),
                            salgcd_icod_producto5 = Convert.ToInt32(item.salgcd_icod_producto5),
                            salgcd_icod_producto6 = Convert.ToInt32(item.salgcd_icod_producto6),
                            salgcd_icod_producto7 = Convert.ToInt32(item.salgcd_icod_producto7),
                            salgcd_icod_producto8 = Convert.ToInt32(item.salgcd_icod_producto8),
                            salgcd_icod_producto9 = Convert.ToInt32(item.salgcd_icod_producto9),
                            salgcd_icod_producto10 = Convert.ToInt32(item.salgcd_icod_producto10),

                            salgcd_iactivo = item.salgcd_iactivo,


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
        public void InsertarProdNotaSalidaDetalle(EProdNotaSalidaDetalle obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_NOTA_SALIDA_DETALLE_INSERTAR(
                        obj.salgc_icod_nota_salida,
                        obj.salgc_iid_almacen,
                        obj.salgcd_num_item,
                        obj.tablc_iid_motivo,
                        obj.pr_vcodigo_externo,
                        obj.pr_vdescripcion_producto,
                        obj.pr_ncant_total_producto,
                        obj.salgcd_icod_serie,
                        obj.salgcd_rango_talla,
                        obj.salgcd_talla1,
                        obj.salgcd_talla2,
                        obj.salgcd_talla3,
                        obj.salgcd_talla4,
                        obj.salgcd_talla5,
                        obj.salgcd_talla6,
                        obj.salgcd_talla7,
                        obj.salgcd_talla8,
                        obj.salgcd_talla9,
                        obj.salgcd_talla10,
                        obj.salgcd_cant_talla1,
                        obj.salgcd_cant_talla2,
                        obj.salgcd_cant_talla3,
                        obj.salgcd_cant_talla4,
                        obj.salgcd_cant_talla5,
                        obj.salgcd_cant_talla6,
                        obj.salgcd_cant_talla7,
                        obj.salgcd_cant_talla8,
                        obj.salgcd_cant_talla9,
                        obj.salgcd_cant_talla10,
                        obj.salgcd_iid_kardex1,
                        obj.salgcd_iid_kardex2,
                        obj.salgcd_iid_kardex3,
                        obj.salgcd_iid_kardex4,
                        obj.salgcd_iid_kardex5,
                        obj.salgcd_iid_kardex6,
                        obj.salgcd_iid_kardex7,
                        obj.salgcd_iid_kardex8,
                        obj.salgcd_iid_kardex9,
                        obj.salgcd_iid_kardex10,
                        obj.salgcd_icod_producto1,
                        obj.salgcd_icod_producto2,
                        obj.salgcd_icod_producto3,
                        obj.salgcd_icod_producto4,
                        obj.salgcd_icod_producto5,
                        obj.salgcd_icod_producto6,
                        obj.salgcd_icod_producto7,
                        obj.salgcd_icod_producto8,
                        obj.salgcd_icod_producto9,
                        obj.salgcd_icod_producto10,
                        obj.intUsuario
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarProdNotaSalidaDetalle(EProdNotaSalidaDetalle obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_NOTA_SALIDA_DETALLE_ACTUALIZAR(
                obj.salgcd_icod_nota_salida,
                obj.salgc_iid_almacen,
                obj.salgcd_num_item,
                obj.tablc_iid_motivo,
                obj.pr_vcodigo_externo,
                obj.pr_vdescripcion_producto,
                obj.pr_ncant_total_producto,
                obj.salgcd_icod_serie,
                obj.salgcd_rango_talla,
                obj.salgcd_talla1,
                obj.salgcd_talla2,
                obj.salgcd_talla3,
                obj.salgcd_talla4,
                obj.salgcd_talla5,
                obj.salgcd_talla6,
                obj.salgcd_talla7,
                obj.salgcd_talla8,
                obj.salgcd_talla9,
                obj.salgcd_talla10,
                obj.salgcd_cant_talla1,
                obj.salgcd_cant_talla2,
                obj.salgcd_cant_talla3,
                obj.salgcd_cant_talla4,
                obj.salgcd_cant_talla5,
                obj.salgcd_cant_talla6,
                obj.salgcd_cant_talla7,
                obj.salgcd_cant_talla8,
                obj.salgcd_cant_talla9,
                obj.salgcd_cant_talla10,
                obj.salgcd_iid_kardex1,
                obj.salgcd_iid_kardex2,
                obj.salgcd_iid_kardex3,
                obj.salgcd_iid_kardex4,
                obj.salgcd_iid_kardex5,
                obj.salgcd_iid_kardex6,
                obj.salgcd_iid_kardex7,
                obj.salgcd_iid_kardex8,
                obj.salgcd_iid_kardex9,
                obj.salgcd_iid_kardex10,
                obj.salgcd_icod_producto1,
                obj.salgcd_icod_producto2,
                obj.salgcd_icod_producto3,
                obj.salgcd_icod_producto4,
                obj.salgcd_icod_producto5,
                obj.salgcd_icod_producto6,
                obj.salgcd_icod_producto7,
                obj.salgcd_icod_producto8,
                obj.salgcd_icod_producto9,
                obj.salgcd_icod_producto10,
                obj.intUsuario);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarProdNotaSalidaDetalle(EProdNotaSalidaDetalle obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_NOTA_SALIDA_DETALLE_ELIMINAR(obj.salgcd_icod_nota_salida);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region transferencia entre Almacenes

        public List<EProdTransferencia> ListarProdTransferencia()
        {
            List<EProdTransferencia> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdTransferencia>();
                    var query = dc.SGE_PVT_TRANSFERENCIA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EProdTransferencia()
                        {
                            trfc_icod_transf = item.trfc_icod_transf,
                            trfc_inum_transf = Convert.ToInt32(item.trfc_inum_transf),
                            trfc_sfecha_transf = Convert.ToDateTime(item.trfc_sfecha_transf),
                            trfc_iid_alm_sal = Convert.ToInt32(item.trfc_iid_alm_sal),
                            puvec_icod_punto_venta = Convert.ToInt32(item.puvec_icod_punto_venta),
                            almac_vdescripcion_ingreso = item.almac_vdescripcion_ingreso,
                            trfc_iid_alm_ing = Convert.ToInt32(item.trfc_iid_alm_ing),
                            almac_vdescripcion_salida = item.almac_vdescripcion_salida,
                            trfc_vobservaciones = item.trfc_vobservaciones,
                            trfc_iactivo = item.trfc_iactivo
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
        public int InsertarProdTransferencia(EProdTransferencia objETransferencia)
        {
            int intIdTransferencia = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    int? intId = 0;

                    dc.SGE_PVT_TRANSFERENCIA_INSERTAR(
                    ref intId,
                    objETransferencia.trfc_inum_transf,
                    objETransferencia.trfc_sfecha_transf,
                    objETransferencia.puvec_icod_punto_venta,
                    objETransferencia.trfc_iid_alm_sal,
                    objETransferencia.trfc_iid_alm_ing,
                    objETransferencia.trfc_vobservaciones,
                    objETransferencia.trfc_iactivo,
                    objETransferencia.isuario
                    );
                    intIdTransferencia = Convert.ToInt32(intId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intIdTransferencia;
        }
        public void ActualizarProdTransferencia(EProdTransferencia objETransferencia)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_TRANSFERENCIA_ACTUALIZAR(
                    objETransferencia.trfc_icod_transf,
                    objETransferencia.trfc_vobservaciones,
                    objETransferencia.trfc_sfecha_transf,
                    objETransferencia.trfc_iactivo,
                    objETransferencia.isuario
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarProdTransferencia(int intIdTransferencia)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_TRANSFERENCIA_ELIMINAR(intIdTransferencia);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region transferencia entre Almacenes Detalle
        public List<EProdTransferenciaDetalle> ListarProdTransferenciaDetalle(int intNumDoc)
        {
            List<EProdTransferenciaDetalle> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdTransferenciaDetalle>();
                    var query = dc.SGE_PVT_TRANSFERENCIA_DETALLE_LISTAR(intNumDoc);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdTransferenciaDetalle()
                        {
                            trfcd_icod_transf = item.trfcd_icod_transf,
                            trfc_icod_transf = item.trfc_icod_transf,
                            trfcd_num_item = item.trfcd_num_item,
                            trfcd_icod_producto = item.trfcd_icod_producto,
                            pr_vdescripcion_producto = item.pr_vdescripcion_producto,
                            trfcd_vcodigo_externo = item.trfcd_vcodigo_externo,
                            trfcd_ncantidad = item.trfcd_ncantidad,
                            trfcd_iid_alm_sal = item.trfcd_iid_alm_sal,
                            DescripAlmacenSalida = item.DescripAlmacenSalida,
                            trfcd_idsal_kardex = Convert.ToInt64(item.trfcd_idsal_kardex),
                            trfcd_iid_alm_ing = item.trfcd_iid_alm_ing,
                            DescripAlmacenIngreso = item.DescripAlmacenIngreso,
                            trfcd_iding_kardex = Convert.ToInt64(item.trfcd_iding_kardex),
                            trfcd_iactivo = item.trfcd_iactivo,
                            UnidadMedi = item.UnidadMedi
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
        public int InsertarProdTransferenciaDetalle(EProdTransferenciaDetalle objETransferenciaDetalle)
        {
            int intIdTransferenciaDetalle = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    int? intId = 0;
                    dc.SGE_PVT_TRANSFERENCIA_DETALLE_INSERTAR(
                    ref intId,
                    objETransferenciaDetalle.trfc_icod_transf,
                    objETransferenciaDetalle.trfcd_num_item,
                    objETransferenciaDetalle.trfcd_icod_producto,
                    objETransferenciaDetalle.trfcd_vcodigo_externo,
                    objETransferenciaDetalle.trfcd_ncantidad,
                    objETransferenciaDetalle.trfcd_iid_alm_sal,
                    objETransferenciaDetalle.trfcd_idsal_kardex,
                    objETransferenciaDetalle.trfcd_iid_alm_ing,
                    objETransferenciaDetalle.trfcd_iding_kardex,
                    objETransferenciaDetalle.trfcd_iactivo,
                    objETransferenciaDetalle.iusuario
                    );
                    intIdTransferenciaDetalle = Convert.ToInt32(intId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intIdTransferenciaDetalle;
        }
        public void ActualizarProdTransferenciaDetalle(EProdTransferenciaDetalle objETransferenciaDetalle)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_TRANSFERENCIA_DETALLE_ACTUALIZAR(
                    objETransferenciaDetalle.trfcd_icod_transf,
                    objETransferenciaDetalle.trfcd_ncantidad,
                    objETransferenciaDetalle.iusuario,
                    objETransferenciaDetalle.trfcd_iactivo
                    );
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
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_TRANSFERENCIA_DETALLE_ELIMINAR(intIdTransferenciaDetalle);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Inventario Físico

        //public string ValidarProdNumeroInventarioFisico()
        //{
        //    string numinvFisi = null;
        //    try
        //    {
        //        using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
        //        {
        //            var query = dc.SGE_PVT_INVENTARO_FISICO_VALIDAR_CORRELATIVO();
        //            foreach (var item in query)
        //            {
        //                numinvFisi = item.NumInvFisi;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return numinvFisi;
        //}
        public List<EProdInventarioFisico> ListarProdInventarioFisico()
        {
            List<EProdInventarioFisico> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdInventarioFisico>();
                    var query = dc.SGE_PVT_INVENTARO_FISICO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EProdInventarioFisico()
                        {
                            invfi_icod_invent = item.invfi_icod_invent,
                            almac_icod_almacen = item.almac_icod_almacen,
                            almac_vdescripcion = item.almac_vdescripcion,
                            invfi_vnumero_invent = item.invfi_vnumero_invent,
                            invfi_sinvent = item.invfi_sinvent,
                            invfi_itipo_invent = item.invfi_itipo_invent,
                            DesTipoInventario = item.DesTipoInventario,
                            gingc_icod_guia_ingreso = item.gingc_icod_guia_ingreso,
                            numeroGuiaIngreso = item.numeroGuiaIngreso,
                            gsalc_icod_guia_salida = item.gsalc_icod_guia_salida,
                            numeroGuiaSalida = item.numeroGuiaSalida,
                            invfi_bsituacion = item.invfi_bsituacion,
                            desSituacion = item.desSituacion,
                            tarec_iid_marca = item.tarec_iid_marca,
                            desMarca = item.DesMarca,
                            mo_iid_modelo = item.mo_iid_modelo,
                            DesModelo = item.desModelo,
                            tarec_iid_color = item.tarec_iid_color,
                            DesColor = item.DesColor,
                            invfi_iestado = item.invfi_iestado
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
        //public List<EProdSituacion> ListarProdTipoInventarioListar()
        //{
        //    List<EProdSituacion> lista = new List<EProdSituacion>();
        //    try
        //    {
        //        using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
        //        {
        //            var query = dc.SGE_PVT_TIPO_INVENTARIO_LISTAR();
        //            foreach (var item in query)
        //            {
        //                lista.Add(new EProdSituacion()
        //                {
        //                    tarec_icorrelativo_registro = item.tarec_icorrelativo_registro,
        //                    tarec_vdescripcion = item.tarec_vdescripcion
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
        public int? InsertarProdInventarioFisico(EProdInventarioFisico obj)
        {
            int? IdInventario = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_INVENTARO_FISICO_INSERTAR(
                        ref IdInventario,
                        obj.almac_icod_almacen,
                        obj.invfi_vnumero_invent,
                        obj.invfi_sinvent,
                        obj.invfi_itipo_invent,
                        obj.gingc_icod_guia_ingreso,
                        obj.gsalc_icod_guia_salida,
                        obj.invfi_bsituacion,
                        obj.tarec_iid_marca,
                        obj.mo_iid_modelo,
                        obj.tarec_iid_color,
                        obj.invfi_iestado,
                        obj.iusuario,
                        obj.tarec_correlative
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IdInventario;
        }
        public void ActualizarProdInventarioFisico(EProdInventarioFisico obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_INVENTARO_FISICO_ACTUALIZAR(
                        obj.almac_icod_almacen,
                        obj.invfi_sinvent,
                        obj.invfi_icod_invent,
                        obj.gingc_icod_guia_ingreso,
                        obj.gsalc_icod_guia_salida,
                        obj.invfi_bsituacion
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarProdInventarioFisico(EProdInventarioFisico obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_INVENTARO_FISICO_ELIMINAR(
                        obj.invfi_icod_invent
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
        #region Inventario Físico Detalle

        public void InsertarProdInventarioFisicoDetalle(EProdInventarioFisicoDetalle obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_INVENTARO_FISICO_DETALLE_INSERTAR(
                        obj.invfi_icod_invent,
                        obj.pr_icod_producto,
                        obj.infid_ncant_fisica,
                        obj.infid_ncant_sistema,
                        obj.infid_iactivo,
                        obj.iusuario
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarProdInventarioFisicoDetalle(EProdInventarioFisicoDetalle obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_INVENTARO_FISICO_DETALLE_ACTUALIZAR(
                        obj.infid_icod_invent,
                        obj.infid_ncant_fisica,
                        obj.infid_ncant_sistema,
                        obj.iusuario
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EProdInventarioFisicoDetalle> ListarProdInventarioFisicoDetalle(int invfi_icod_invent)
        {
            List<EProdInventarioFisicoDetalle> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdInventarioFisicoDetalle>();
                    var query = dc.SGE_PVT_INVENTARO_FISICO_DETALLE_LISTAR(invfi_icod_invent);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdInventarioFisicoDetalle()
                        {
                            infid_icod_invent = item.infid_icod_invent,
                            invfi_icod_invent = item.invfi_icod_invent,
                            pr_icod_producto = item.pr_icod_producto,
                            pr_vcodigo_externo = item.pr_vcodigo_externo,
                            pr_vdescripcion_producto = item.pr_vdescripcion_producto,
                            infid_ncant_fisica = item.infid_ncant_fisica,
                            infid_ncant_sistema = item.infid_ncant_sistema,
                            infid_iactivo = item.infid_iactivo,
                            diferencia = Convert.ToDecimal(item.Diferencia),
                            abreviaUnidadMedi = item.abreviaUnidadMedi
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
        public void EliminarProdInventarioFisicoDetalle(EProdInventarioFisicoDetalle obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_INVENTARO_FISICO_DETALLE_ELIMINAR(
                        obj.infid_icod_invent
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
        public List<EProdKardex> listarkardex(int intanio)
        {
            List<EProdKardex> lista = new List<EProdKardex>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_KARDEX_LISTAR(intanio);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdKardex()
                        {
                            kardc_ianio = Convert.ToInt32(item.kardc_ianio),
                            kardx_sfecha = Convert.ToDateTime(item.kardc_sfecha_movimiento),
                            iid_almacen = Convert.ToInt32(item.kardc_iid_almacen),
                            iid_producto = Convert.ToInt32(item.kardc_iid_producto),
                            Cantidad = Convert.ToInt32(item.kardc_icantidad_prod),
                            movimiento = Convert.ToInt32(item.kardc_itipo_movimiento)
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

        public void actualizarStockPvt(EProdStockProducto oBe)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGEA_STOCK_ACTUALIZAR_PVT(
                        oBe.stocc_ianio,
                        oBe.stocc_iid_almacen,
                        oBe.stocc_iid_producto,
                        oBe.stocc_nstock_prod
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertarKardexpvt(EProdKardex obj)
        {
            int? kardc_iid_correlativo = 0;
            try
            {

                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {

                    dc.SGEA_KARDEX_PVT_INSERTAR(
                        ref kardc_iid_correlativo,
                        obj.kardc_ianio,
                        obj.kardx_sfecha,
                        obj.iid_almacen,
                        obj.iid_producto,
                        obj.puvec_icod_punto_venta,
                        obj.Cantidad,
                        obj.NroNota,
                        obj.iid_tipo_doc,
                        obj.NroDocumento,
                        obj.movimiento,
                        obj.Motivo,
                        obj.Beneficiario,
                        obj.Observaciones,
                        obj.intUsuario
                        );
                }
                return Convert.ToInt32(kardc_iid_correlativo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertarKardexpvtOP(EProdKardex obj)
        {
            int? kardc_iid_correlativo = 0;
            try
            {

                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {

                    dc.SGEA_KARDEX_PVT_INSERTAR_OP(
                        ref kardc_iid_correlativo,
                        obj.kardc_ianio,
                        obj.kardx_sfecha,
                        obj.iid_almacen,
                        obj.iid_producto,
                        obj.puvec_icod_punto_venta,
                        obj.Cantidad,
                        obj.NroNota,
                        obj.iid_tipo_doc,
                        obj.NroDocumento,
                        obj.movimiento,
                        obj.Motivo,
                        obj.Beneficiario,
                        obj.Observaciones,
                        obj.intUsuario,
                        obj.orped_icod_item_orden_pedido
                        );
                }
                return Convert.ToInt32(kardc_iid_correlativo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarKardexPvt(EProdKardex oBe)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGEA_KARDEX_PVT_MODIFICAR(
                        oBe.kardc_iid_correlativo,
                        oBe.kardc_ianio,
                        oBe.kardx_sfecha,
                        oBe.iid_almacen,
                        oBe.iid_producto,
                        oBe.puvec_icod_punto_venta,
                        oBe.Cantidad,
                        oBe.NroNota,
                        oBe.iid_tipo_doc,
                        oBe.NroDocumento,
                        oBe.movimiento,
                        oBe.Motivo,
                        oBe.Beneficiario,
                        oBe.Observaciones,
                        oBe.intUsuario,
                        DateTime.Now,
                        oBe.strPc
                        );
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
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGEA_KARDEX_PVT_ELIMINAR(
                        obj.kardc_iid_correlativo,
                        obj.intUsuario,
                        DateTime.Now,
                        obj.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<EProdKardex> ListarProdKardexFechaAlmacen(EProdStockProducto obj)
        {
            List<EProdKardex> lista = new List<EProdKardex>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_LISTAR_KARDEX_FECHA_ALMACEN(obj.stocc_iid_producto, obj.stocc_iid_almacen, obj.puvec_icod_punto_venta, obj.fecha);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdKardex()
                        {
                            kardx_sfecha = Convert.ToDateTime(item.fecha_movimiento),
                            vmotivo = item.motivo,
                            NroDocumento = item.nrodoc,
                            cantidad_ingreso = Convert.ToDecimal(item.cantidad_ingreso),
                            cantidad_salida = Convert.ToDecimal(item.cantidad_salida),
                            cantidad_saldo_prod = Convert.ToDecimal(item.saldo),
                            Beneficiario = item.referencia,
                            Observaciones = item.observacion,
                            vNroNota = string.Format("{0:000000}", item.nota),
                            iid_producto = Convert.ToInt32(item.id_producto)
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
        public List<EProdKardex> ListarProdKardexFechaIntervaloAlmacen(DateTime dtFechaDesde, DateTime dtFechaHasta, int? intAlmacen, int? intProducto)
        {
            List<EProdKardex> lista = new List<EProdKardex>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_KARDEX_LISTAR_X_FECHA_ALMACEN_PRODUCTO_PVT(dtFechaDesde, dtFechaHasta, intAlmacen, intProducto);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdKardex()
                        {

                            kardx_sfecha = item.kardc_fecha_movimiento,
                            iid_almacen = item.almac_icod_almacen,
                            iid_producto = item.prdc_icod_producto,
                            Cantidad = Convert.ToDecimal(item.kardc_icantidad_prod),
                            iid_tipo_doc = item.tdocc_icod_tipo_doc,
                            NroDocumento = item.strDocumento,
                            movimiento = item.kardc_tipo_movimiento,
                            Motivo = item.kardc_iid_motivo,
                            Beneficiario = item.kardc_beneficiario,
                            Observaciones = item.kardc_observaciones,
                            cantidad_ingreso = Convert.ToDecimal(item.dblIngreso),
                            cantidad_salida = Convert.ToDecimal(item.dblSalida),
                            cantidad_saldo_prod = Convert.ToDecimal(item.dblSaldo),
                            strDocumento = item.strDocumento,
                            vmotivo = item.strMotivo,
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

        public void EliminarOrdenProduccionVarios(EOrdenProduccion obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_ORDEN_PRODUCCION_ELIMINAR_VARIOS(obj.orprc_iid_orden_produccion, obj.intUsuario, obj.strPc);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<EProdStockProducto> listarVerificarStockMercaderia(EProdStockProducto obj)
        {
            List<EProdStockProducto> lista = new List<EProdStockProducto>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_VERIFICAR_STOCK_MERCADERIA(obj.fecha.Year);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdStockProducto()
                        {
                            stocc_iid_almacen = Convert.ToInt32(item.kardc_iid_almacen),
                            stocc_iid_producto = Convert.ToInt32(item.kardc_iid_producto),
                            descripcion_almacen = item.strAlmacen,
                            vcodigo_externo = item.strCodProducto,
                            descripcion_producto = item.strProducto,
                            descripcion_unidad_medida = item.strUnidadMedida,
                            stock_prod = Convert.ToDecimal(item.stock),
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
        public List<EProdconsultaKardex> ListarProdResumenMovimientoProductos(EProdReporte objReporte)
        {
            List<EProdconsultaKardex> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.CommandTimeout = 0;
                    lista = new List<EProdconsultaKardex>();
                    var query = dc.SGE_PVT_RESUMEN_MOVIMIENTO_PRODUCTOS(objReporte.sfechaInicio, objReporte.sfechaFinal, objReporte.puvec_icod_punto_venta);
                    foreach (var item in query)
                    {
                        EProdconsultaKardex _obe = new EProdconsultaKardex();
                        _obe.kardc_iidalmacen = item.kardc_iid_almacen;
                        _obe.kardc_iid_producto = item.kardc_iid_producto;
                        _obe.stockaAnterior = Convert.ToInt32(item.stockaAnterior);
                        _obe.pr_vdescripcion_producto = item.pr_vdescripcion_producto;
                        _obe.pr_vcodigo_externo = item.pr_vcodigo_externo;
                        _obe.DesUnidadMedida = item.DesUnidadMedida;
                        _obe.tipo_movimiento_ingreso = Convert.ToInt32(item.tipo_movimiento_ingreso);
                        _obe.tipo_movimiento_salida = Convert.ToInt32(item.tipo_movimiento_salida);
                        _obe.descripcionAlmacen = item.almac_vdescripcion;

                        if ((_obe.tipo_movimiento_ingreso + _obe.tipo_movimiento_salida) != 0)
                        {
                            _obe.stockActual = Convert.ToInt32(_obe.stockaAnterior + item.tipo_movimiento_ingreso - item.tipo_movimiento_salida);
                            lista.Add(_obe);
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




        #endregion
        #region Codigo de Barra
        public List<EProdCodigoBarra> ListarProdCodigoBarra()
        {
            List<EProdCodigoBarra> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdCodigoBarra>();
                    var query = dc.SGE_PVT_IMPOR_COD_BARR_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EProdCodigoBarra()
                        {
                            picbc_icod_plan_cod_barr = item.picbc_icod_plan_cod_barr,
                            picbc_inumero_plant = item.picbc_inumero_plant,
                            picbc_sfecha_plant = item.picbc_sfecha_plant,
                            picbc_vdescrip = item.picbc_vdescrip,
                            picbc_iicod_marca = item.picbc_iicod_marca,
                            picbc_iicod_modelo = item.picbc_iicod_modelo,
                            DescripModelo = item.DescripModelo
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
        public int? InsertarProdCodigoBarra(EProdCodigoBarra obj)
        {
            int? codigo = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_IMPOR_COD_BARR_INSERTAR(
                        ref codigo,
                        obj.picbc_inumero_plant,
                        obj.picbc_sfecha_plant,
                        obj.picbc_vdescrip,
                        obj.picbc_iicod_marca,
                        obj.picbc_iicod_modelo,
                        obj.tarec_icorrelativo_modelo,
                        obj.picbc_iactivo,
                        obj.iusuario
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return codigo;
        }
        public void ModificarProdCodigoBarra(EProdCodigoBarra obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_IMPOR_COD_BARR_MODIFICAR(
                        obj.picbc_icod_plan_cod_barr,
                        obj.picbc_vdescrip,
                        obj.iusuario
                        );
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
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_IMPOR_COD_BARR_ELIMINAR(
                        obj.picbc_icod_plan_cod_barr,
                        obj.iusuario
                        );
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
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdCodigoBarraDetalle>();
                    var query = dc.SGE_PVT_IMPOR_COD_BARR_DETALLE_LISTAR(picbd_icod_plan_cod_barr);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdCodigoBarraDetalle()
                        {
                            picbd_icod_plan_cod_barr_det = item.picbd_icod_plan_cod_barr_det,
                            picbd_icod_plan_cod_barr = item.picbd_icod_plan_cod_barr,
                            picbd_rango_talla = item.picbd_rango_talla,
                            pr_vcodigo_externo = item.pr_vcodigo_externo,
                            pr_vdescripcion_producto = item.pr_vdescripcion_producto,
                            pr_vdescripcion_modelo = item.pr_vdescripcion_modelo,
                            pr_vdescripcion_color = item.pr_vdescripcion_color,
                            picbd_talla1 = Convert.ToInt32(item.picbd_talla1),
                            picbd_talla2 = Convert.ToInt32(item.picbd_talla2),
                            picbd_talla3 = Convert.ToInt32(item.picbd_talla3),
                            picbd_talla4 = Convert.ToInt32(item.picbd_talla4),
                            picbd_talla5 = Convert.ToInt32(item.picbd_talla5),
                            picbd_talla6 = Convert.ToInt32(item.picbd_talla6),
                            picbd_talla7 = Convert.ToInt32(item.picbd_talla7),
                            picbd_talla8 = Convert.ToInt32(item.picbd_talla8),
                            picbd_talla9 = Convert.ToInt32(item.picbd_talla9),
                            picbd_talla10 = Convert.ToInt32(item.picbd_talla10),
                            picbd_cant_talla1 = Convert.ToInt32(item.picbd_cant_talla1),
                            picbd_cant_talla2 = Convert.ToInt32(item.picbd_cant_talla2),
                            picbd_cant_talla3 = Convert.ToInt32(item.picbd_cant_talla3),
                            picbd_cant_talla4 = Convert.ToInt32(item.picbd_cant_talla4),
                            picbd_cant_talla5 = Convert.ToInt32(item.picbd_cant_talla5),
                            picbd_cant_talla6 = Convert.ToInt32(item.picbd_cant_talla6),
                            picbd_cant_talla7 = Convert.ToInt32(item.picbd_cant_talla7),
                            picbd_cant_talla8 = Convert.ToInt32(item.picbd_cant_talla8),
                            picbd_cant_talla9 = Convert.ToInt32(item.picbd_cant_talla9),
                            picbd_cant_talla10 = Convert.ToInt32(item.picbd_cant_talla10)
                            //PrecioProducto = Convert.ToInt32(item.PrecioProducto)
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
        public void InsertarProdCodigoBarraDetalle(EProdCodigoBarraDetalle obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_IMPOR_COD_BARR_DETALLE_INSERTAR(
                        obj.picbd_icod_plan_cod_barr,
                        obj.picbd_rango_talla,
                        obj.pr_vcodigo_externo,
                        obj.pr_vdescripcion_producto,
                        obj.pr_vdescripcion_modelo,
                        obj.pr_vdescripcion_color,
                        obj.picbd_talla1,
                        obj.picbd_talla2,
                        obj.picbd_talla3,
                        obj.picbd_talla4,
                        obj.picbd_talla5,
                        obj.picbd_talla6,
                        obj.picbd_talla7,
                        obj.picbd_talla8,
                        obj.picbd_talla9,
                        obj.picbd_talla10,
                        obj.picbd_cant_talla1,
                        obj.picbd_cant_talla2,
                        obj.picbd_cant_talla3,
                        obj.picbd_cant_talla4,
                        obj.picbd_cant_talla5,
                        obj.picbd_cant_talla6,
                        obj.picbd_cant_talla7,
                        obj.picbd_cant_talla8,
                        obj.picbd_cant_talla9,
                        obj.picbd_cant_talla10,
                        obj.picbd_iactivo,
                        obj.iusuario
                        );
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
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_IMPOR_COD_BARR_DETALLE_ACTUALIZAR(
                        obj.picbd_icod_plan_cod_barr_det,
                        obj.picbd_talla1,
                        obj.picbd_talla2,
                        obj.picbd_talla3,
                        obj.picbd_talla4,
                        obj.picbd_talla5,
                        obj.picbd_talla6,
                        obj.picbd_talla7,
                        obj.picbd_talla8,
                        obj.picbd_talla9,
                        obj.picbd_talla10,
                        obj.picbd_cant_talla1,
                        obj.picbd_cant_talla2,
                        obj.picbd_cant_talla3,
                        obj.picbd_cant_talla4,
                        obj.picbd_cant_talla5,
                        obj.picbd_cant_talla6,
                        obj.picbd_cant_talla7,
                        obj.picbd_cant_talla8,
                        obj.picbd_cant_talla9,
                        obj.picbd_cant_talla10,
                        obj.picbd_iactivo,
                        obj.iusuario
                        );
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
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_IMPOR_COD_BARR_DETALLE_ELIMINAR(
                        obj.picbd_icod_plan_cod_barr_det
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public List<EProdProducto> VerificarProductoPvt(string Code)
        {
            List<EProdProducto> lista = new List<EProdProducto>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_VERIFICAR_PRODUCTO_PVT(Code);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdProducto()
                        {
                            pr_icod_producto = item.pr_icod_producto,
                            pr_vdescripcion_producto = item.pr_vdescripcion_producto
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
        public List<EProdProducto> VerificarProdStockProducto(int Code, int almac_icod_almacen, int intanio)
        {
            List<EProdProducto> lista = new List<EProdProducto>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_VERIFICAR_STOCK_DE_UN_PRODUCTO(Code, almac_icod_almacen, intanio);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdProducto()
                        {
                            pr_icod_producto = item.pr_icod_producto,
                            pr_vdescripcion_producto = item.pr_vdescripcion_producto,
                            stocc_nstock_prod = item.stocc_nstock_prod
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
        public List<EProdProducto> ListarProdProductosXAlmacen(int stocc_iid_almacen, int intanio)
        {
            List<EProdProducto> lista = new List<EProdProducto>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_STOCK_PRODUCTO_LISTAR_PRODUCTOS_X_ALMACEN(stocc_iid_almacen, intanio);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdProducto()
                        {
                            pr_icod_producto = item.pr_icod_producto,
                            pr_iid_marca = item.pr_iid_marca,
                            pr_viid_marca = string.Format("{0:00}", item.pr_iid_marca),
                            pr_iid_modelo = item.pr_iid_modelo,
                            pr_viid_modelo = string.Format("{0:0000}", item.pr_iid_modelo),
                            pr_iid_color = item.pr_iid_color,
                            pr_viid_color = string.Format("{0:00}", item.pr_iid_color),
                            pr_vcodigo_externo = item.pr_vcodigo_externo,
                            pr_vdescripcion_producto = item.pr_vdescripcion_producto,
                            stocc_nstock_prod = item.stocc_nstock_prod,
                            stocc_ianio = Convert.ToInt32(item.stocc_ianio),
                            pr_afecto_igv = Convert.ToBoolean(item.Afecto),
                            CodigoSUNAT = item.CodigoSUNAT,
                            strUM = item.strUM
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
        #region Punto Venta
        public List<EPuntoVenta> ListarPuntoVenta()
        {
            List<EPuntoVenta> lista = new List<EPuntoVenta>(); ;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PUNTO_VENTA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EPuntoVenta()
                        {
                            puvec_icod_punto_venta = item.puvec_icod_punto_venta,
                            puvec_vcod_punto_venta = item.puvec_vcod_punto_venta,
                            puvec_vdescripcion = item.puvec_vdescripcion,
                            puvec_iactivo = item.puvec_iactivo,
                            puvec_vserie_factura = item.puvec_vserie_factura,
                            puvec_icorrelativo_factura = Convert.ToInt32(item.puvec_icorrelativo_factura),
                            puvec_vserie_factura_suministros = item.puvec_vserie_factura_suministros,
                            puvec_icorrelativo_factura_suministros = Convert.ToInt32(item.puvec_icorrelativo_factura_suministros),
                            puvec_vserie_factura_alquileres = item.puvec_vserie_factura_alquileres,
                            puvec_icorrelativo_factura_alquileres = Convert.ToInt32(item.puvec_icorrelativo_factura_alquileres),
                            puvec_vserie_boleta = item.puvec_vserie_boleta,
                            puvec_icorrelativo_boleta = Convert.ToInt32(item.puvec_icorrelativo_boleta),
                            puvec_vserie_notaF_credito = item.puvec_vserie_notaF_credito,
                            puvec_vserie_notaB_credito = item.puvec_vserie_notaB_credito,
                            puvec_icorrelativo_nota_credito = Convert.ToInt32(item.puvec_icorrelativo_nota_credito),
                            puvec_vserie_notaF_debito = item.puvec_vserie_notaF_debito,
                            puvec_vserie_notaB_debito = item.puvec_vserie_notaB_debito,
                            puvec_icorrelativo_nota_debito = Convert.ToInt32(item.puvec_icorrelativo_nota_debito),
                            puvec_vcodigo_sunat = (item.puvec_vcodigo_sunat),
                            puvec_vdireccion = item.puvec_vdireccion,
                            puvec_vusuario_ose = item.puvec_vusuario_ose,
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
        public int InsertarPuntoVenta(EPuntoVenta ob)
        {
            int? intIcod = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PUNTO_VENTA_INSERTAR(
                        ob.puvec_vcod_punto_venta,
                        ob.puvec_vdescripcion,
                        ob.puvec_iactivo,
                        ob.puvec_vserie_factura,
                        ob.puvec_icorrelativo_factura,
                        ob.puvec_vserie_factura_suministros,
                        ob.puvec_icorrelativo_factura_suministros,
                        ob.puvec_vserie_factura_alquileres,
                        ob.puvec_icorrelativo_factura_alquileres,
                        ob.puvec_vserie_boleta,
                        ob.puvec_icorrelativo_boleta,
                        ob.puvec_vserie_notaF_credito,
                        ob.puvec_vserie_notaB_credito,
                        ob.puvec_icorrelativo_nota_credito,
                        ob.puvec_vserie_notaF_debito,
                        ob.puvec_vserie_notaB_debito,
                        ob.puvec_icorrelativo_nota_debito,
                        ob.puvec_vcodigo_sunat.ToString(),
                        ob.puvec_vdireccion,
                        ob.puvec_vusuario_ose,
                        ob.intUsuario,
                        ob.strPc
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarPuntoVenta(EPuntoVenta ob)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PUNTO_VENTA_ACTUALIZAR(
                        ob.puvec_icod_punto_venta,
                        ob.puvec_vdescripcion,
                        ob.puvec_iactivo,
                        ob.puvec_vserie_factura,
                        ob.puvec_icorrelativo_factura,
                        ob.puvec_vserie_factura_suministros,
                        ob.puvec_icorrelativo_factura_suministros,
                        ob.puvec_vserie_factura_alquileres,
                        ob.puvec_icorrelativo_factura_alquileres,
                        ob.puvec_vserie_boleta,
                        ob.puvec_icorrelativo_boleta,
                        ob.puvec_vserie_notaF_credito,
                        ob.puvec_vserie_notaB_credito,
                        ob.puvec_icorrelativo_nota_credito,
                        ob.puvec_vserie_notaF_debito,
                        ob.puvec_vserie_notaB_debito,
                        ob.puvec_icorrelativo_nota_debito,
                        ob.puvec_vcodigo_sunat.ToString(),
                        ob.puvec_vdireccion,
                        ob.puvec_vusuario_ose,
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
        public void EliminarPuntoVenta(EPuntoVenta ob)
        {

            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PUNTO_VENTA_ELIMINAR(
                        ob.puvec_icod_punto_venta,
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
        public int ValidarCodigoPuntoVenta()
        {
            int codigoPuntoVenta = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_VALIDAR_NUMERO_PUNTO_VENTA();
                    foreach (var item in query)
                    {
                        codigoPuntoVenta = Convert.ToInt32(item.CodigoPuntoVenta);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return codigoPuntoVenta;
        }
        #endregion

        #region Stock
        public List<EProdKardex> ListarProdKardexProductoFechaAlmacen(EProdStockProducto obj)
        {
            List<EProdKardex> lista = new List<EProdKardex>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    //var query = dc.SGE_PVT_LISTAR_KARDEX_PRODUCTO_FECHA_ALMACEN(obj.id_producto, obj.id_almacen, obj.fecha, obj.fechafin);
                    var query = dc.SGEA_KARDEX_LISTAR_X_FECHA_ALMACEN_PRODUCTO_PVT(obj.fecha, obj.fechafin, obj.stocc_iid_almacen, obj.stocc_iid_producto);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdKardex()
                        {
                            kardx_sfecha = Convert.ToDateTime(item.kardc_fecha_movimiento),
                            vmotivo = item.strMotivo,
                            NroDocumento = item.strDocumento,
                            cantidad_ingreso = Convert.ToDecimal(item.dblIngreso),
                            cantidad_salida = Convert.ToDecimal(item.dblSalida),
                            cantidad_saldo_prod = Convert.ToDecimal(item.dblSaldo),
                            Beneficiario = item.kardc_beneficiario,
                            Observaciones = item.kardc_observaciones
                            //vNroNota = string.Format("{0:000000}", item.nota)
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
        public List<EProdStockProducto> ListarProdStockPorAlmacen(DateTime dtFechaDesde, DateTime dtFechaHasta, int? intAlmacen, int? intProducto)
        {
            List<EProdStockProducto> lista = new List<EProdStockProducto>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_KARDEX_STOCK_LISTAR_X_FECHA_ALMACEN_PRODUCTO_PVT(dtFechaDesde, dtFechaHasta, intAlmacen, intProducto);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdStockProducto()
                        {
                            stocc_iid_almacen = Convert.ToInt32(item.kardc_iid_almacen),
                            stocc_iid_producto = Convert.ToInt32(item.kardc_iid_producto),
                            descripcion_almacen = item.strAlmacen,
                            vcodigo_externo = item.strCodProducto,
                            descripcion_producto = item.strProducto,
                            descripcion_unidad_medida = item.strUnidadMedida,
                            stock_prod = Convert.ToDecimal(item.stock)
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


        #region Envio de Informacion
        public List<ESendInfo> ListarSendInfo()
        {
            List<ESendInfo> lista = null;

            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<ESendInfo>();
                    var query = dc.SGE_SEND_INFO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ESendInfo()
                        {
                            trans_icod_trans = Convert.ToInt32(item.trans_icod_trans),
                            trans_descripcion = item.trans_descripcion,
                            trans_nombre_tabla = item.trans_nombre_tabla,
                            trans_nombre_archivo = item.trans_nombre_archivo,
                            sfecha_crea = Convert.ToDateTime(item.sfecha_crea),
                            estado = true
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
        public void crearArchivoTransferencia(string ruta, string tabla)
        {

            StreamWriter fs = File.CreateText(ruta);
            //if (tabla == "SIGT_TABLA")
            //{
            //    #region SIGT_TABLA
            //    List<ETablaRegistro> mlisregistro = new List<ETablaRegistro>();
            //    List<ETabla> mlistTabla = new List<ETabla>();
            //    mlistTabla = ListarTablaTodo();
            //    mlisregistro = ListarTablaRegistroTodo();
            //    if (mlisregistro.Count > 0)
            //    {

            //        foreach (ETabla obj in mlistTabla)
            //        {
            //            fs.Write("SIGT_TABLA," + obj.tablc_iid_tipo_tabla + "," +
            //                     obj.tablc_vdescripcion + "," +
            //                     obj.tablc_cestado);
            //            fs.WriteLine();
            //        }
            //        fs.WriteLine();
            //        foreach (ETablaRegistro obj1 in mlisregistro)
            //        {
            //            fs.Write("SIGT_TABLA_REGISTRO," + obj1.tarec_iid_tabla_registro + "," +
            //                     obj1.tablc_iid_tipo_tabla + "," +
            //                     obj1.tarec_icorrelativo_registro + "," +
            //                     obj1.tarec_vdescripcion + "," +
            //                     obj1.tarec_nvalor_numerico + "," +
            //                     obj1.tarec_vvalor_texto + "," +
            //                     obj1.tarec_cestado + "," +
            //                     obj1.tarec_iusuario_crea + "," +
            //                     obj1.tarec_iusuario_modifica + "," +
            //                     obj1.tarec_sfecha_crea + "," +
            //                     obj1.tarec_sfecha_modifica + ",");
            //            fs.WriteLine();
            //        }
            //    }
            //    #endregion
            //}
            if (tabla == "SGE_PVT_TABLA")
            {
                #region SIGT_TABLA
                List<EProdTablaRegistro> mlisOpcionesDet = new List<EProdTablaRegistro>();
                List<EProdTabla> mlistOpciones = new List<EProdTabla>();
                mlistOpciones = listarProdTabla();
                mlisOpcionesDet = ListarProdTablaRegistroTodo();
                if (mlistOpciones.Count > 0)
                {

                    foreach (EProdTabla obj in mlistOpciones)
                    {
                        fs.Write("SIGT_TABLA," + obj.tablc_iid_tipo_tabla + "," +
                                 obj.tablc_vdescripcion + "," +
                                 obj.tablc_cestado);
                        fs.WriteLine();
                    }
                    fs.WriteLine();
                    foreach (EProdTablaRegistro obj1 in mlisOpcionesDet)
                    {
                        fs.Write("SIGT_TABLA_REGISTRO," + obj1.tarec_iid_tabla_registro + "," +
                                 obj1.tablc_iid_tipo_tabla + "," +
                                 obj1.tarec_icorrelativo_registro + "," +
                                 obj1.tarec_vdescripcion + "," +
                                 obj1.tarec_nvalor_numerico + "," +
                                 obj1.tarec_vvalor_texto + "," +
                                 obj1.tarec_cestado + "," +
                                 obj1.tarec_iusuario_crea + "," +
                                 obj1.tarec_iusuario_modifica + "," +
                                 obj1.tarec_sfecha_crea + "," +
                                 obj1.tarec_sfecha_modifica);
                        fs.WriteLine();
                    }
                }
                #endregion
            }
            if (tabla == "SGE_PVT_ALMACEN")
            {
                #region SIGT_ALMACEN
                List<EProdAlmacen> mlisAlmacen = new List<EProdAlmacen>();
                mlisAlmacen = ListarProdAlmacen();
                if (mlisAlmacen.Count > 0)
                {
                    foreach (EProdAlmacen objal in mlisAlmacen)
                    {
                        fs.Write("SIGT_ALMACEN," + objal.id_almacen + "," +
                                 objal.idf_almacen + "," +
                                 objal.puvec_icod_punto_venta + "," +
                                 objal.descripcion + "," +
                                 objal.direccion + "," +
                                 objal.representante + "," +
                                 objal.codigo_ubigeo + "," +
                                 objal.almac_iusuario_crea + "," +
                                 objal.almac_sfecha_crea + "," +
                                 objal.almac_iusuario_modifica + "," +
                                 objal.almac_sfecha_modifica + "," +
                                 objal.iactivo);
                        fs.WriteLine();
                    }

                }
                #endregion
            }
            //if (tabla == "PVT_CAJA")
            //{
            //    #region PVT_CAJA
            //    List<ECaja> mlisCaja = new List<ECaja>();
            //    mlisCaja = ListarCajaTodo();
            //    if (mlisCaja.Count > 0)
            //    {
            //        foreach (ECaja objal in mlisCaja)
            //        {
            //            fs.Write("PVT_CAJA," + objal.cajac_icod_caja + "," +
            //                     objal.cajac_vcod_caja + "," +
            //                     objal.puvec_icod_punto_venta + "," +
            //                     objal.cajac_vdescripcion + "," +
            //                     objal.cajac_vNombreLocal + "," +
            //                     objal.cajac_vDirecLocal + "," +
            //                     objal.cajac_vNumeroSerie + "," +
            //                     objal.cajac_vSerieImpres + "," +
            //                     objal.cajac_inro_serie_factura + "," +
            //                     objal.cajac_iNumCorrelTck + "," +
            //                     objal.cajac_inro_serie_boleta + "," +
            //                     objal.cajac_inro_serie_nota_credito + "," +
            //                     objal.cajac_icorrelativo_factura + "," +
            //                     objal.cajac_icorrelativo_boleta + "," +
            //                     objal.cajac_icorrelativo_nota_credito + "," +
            //                     objal.cajac_iactivo);
            //            fs.WriteLine();
            //        }

            //    }
            //    #endregion
            //}
            if (tabla == "SGE_PVT_PRODUCTOS")
            {
                #region SIGT_PRODUCTOS
                List<EProdProducto> mlisProducto = new List<EProdProducto>();
                mlisProducto = ListarProdProductos();
                if (mlisProducto.Count > 0)
                {
                    foreach (EProdProducto objal in mlisProducto)
                    {
                        fs.Write("SIGT_PRODUCTOS," + objal.pr_icod_producto + "," +
                                 objal.pr_iid_producto + "," +
                                 objal.pr_iid_marca + "," +
                                 objal.pr_iid_modelo + "," +
                                 objal.pr_iid_color + "," +
                                 objal.pr_iid_talla + "," +
                                 objal.pr_vcodigo_externo + "," +
                                 objal.pr_vdescripcion_producto + "," +
                                 objal.pr_vabreviatura_producto + "," +
                                 objal.pr_isituacion_producto + "," +
                                 objal.pr_iestado_producto + "," +
                                 objal.pr_tarec_icorrelativo + "," +
                                 objal.unidc_icod_unidad_medida);
                        fs.WriteLine();
                    }

                }
                #endregion
            }
            if (tabla == "SGE_PUNTO_VENTA")
            {
                #region PVT_PUNTO_VENTA
                List<EPuntoVenta> mlisPuntoVenta = new List<EPuntoVenta>();
                mlisPuntoVenta = ListarPuntoVenta();
                if (mlisPuntoVenta.Count > 0)
                {
                    foreach (EPuntoVenta objal in mlisPuntoVenta)
                    {
                        fs.Write("PVT_PUNTO_VENTA," + objal.puvec_icod_punto_venta + "," +
                                 objal.puvec_vcod_punto_venta + "," +
                                 objal.puvec_vdescripcion + "," +
                                 objal.puvec_iactivo);
                        fs.WriteLine();
                    }
                }
                #endregion
            }
            if (tabla == "SGE_PVT_LISTA_PRECIOS")
            {
                #region PVT_LISTA_PRECIOS
                List<EProdListaPrecios> mliPRECIOS = new List<EProdListaPrecios>();
                List<EProdListaPreciosDetalle> mliPRECIOSDeta = new List<EProdListaPreciosDetalle>();
                mliPRECIOS = ListarProdListaPreciosTodo();
                mliPRECIOSDeta = ListarProdListaPrecioDetalleTodo();
                if (mliPRECIOS.Count > 0)
                {
                    foreach (EProdListaPrecios obj in mliPRECIOS)
                    {
                        fs.Write("PVT_LISTA_PRECIOS," + obj.lprec_icod_precio + "," +
                                 obj.pr_icod_producto + "," +
                                 obj.pr_vcodigo_externo + "," +
                                 obj.pr_vdescripcion_producto + "," +
                                 obj.lprec_nprecio_costo + "," +
                                 obj.lprec_nporc_utilidad + "," +
                                 obj.lprec_nprecio_vtamin + "," +
                                 obj.lprec_nprecio_vtamay + "," +
                                 obj.lprec_nprecio_vtaotros + "," +
                                 obj.lprec_bdetalle + "," +
                                 obj.lprec_iactivo
                                 );
                        fs.WriteLine();
                    }
                    fs.WriteLine();
                    foreach (EProdListaPreciosDetalle obj1 in mliPRECIOSDeta)
                    {
                        fs.Write("PVT_LISTA_PRECIOS_DET," + obj1.lpred_icod_det_precio + "," +
                                 obj1.lprec_icod_precio + "," +
                                 obj1.lpred_vrango_tallas + "," +
                                 obj1.lpred_nprecio_vtamin + "," +
                                 obj1.lpred_nprecio_vtamay + "," +
                                 obj1.lpred_nprecio_vtaotros + "," +
                                 obj1.lpred_iactivo);

                        fs.WriteLine();
                    }
                }
                #endregion

            }
            if (tabla == "SGE_PVT_MODELO")
            {
                #region SIGT_MODELO
                List<EProdModelo> mlisMODELO = new List<EProdModelo>();
                mlisMODELO = ListarProdModeloTodo();
                if (mlisMODELO.Count > 0)
                {
                    foreach (EProdModelo objal in mlisMODELO)
                    {
                        fs.Write("SIGT_MODELO," + objal.mo_icod_modelo + "," +
                                 objal.mo_iid_modelo + "," +
                                 objal.pr_iid_tipo + "," +
                                 objal.pr_iid_categoria + "," +
                                 objal.pr_iid_linea + "," +
                                 objal.pr_iid_capellada + "," +
                                 objal.pr_iid_planta + "," +
                                 objal.pr_iid_forro + "," +
                                 objal.pr_iid_tipo_marca + "," +
                                 objal.pr_iid_taco + "," +
                                 objal.pr_iid_horma + "," +
                                 objal.mo_vdescripcion + "," +
                                 objal.tarec_icorrelativo + "," +
                                 objal.mo_estado + "," +
                                 objal.mo_ruta);
                        fs.WriteLine();
                    }
                }
                #endregion
            }
            if (tabla == "SGE_PVT_UNIDAD_MEDIDA")
            {
                #region PVT_UNIDAD_MEDIDA
                List<EProdUnidadMedida> mlisUniMe = new List<EProdUnidadMedida>();
                mlisUniMe = ListarProdUnidadMedida();
                if (mlisUniMe.Count > 0)
                {
                    foreach (EProdUnidadMedida objal in mlisUniMe)
                    {
                        fs.Write("PVT_UNIDAD_MEDIDA," + objal.id_unidad_medida + "," +
                                 objal.idf_unidad_medida + "," +
                                 objal.abreviatura_unidad_medida + "," +
                                 objal.descripcion + "," +
                                 objal.unidc_iactivo);

                        fs.WriteLine();
                    }
                }
                #endregion
            }
            //if (tabla == "PVT_GIRO")
            //{
            //    #region PVT_GIRO
            //    List<EGiro> mlisGiro = new List<EGiro>();
            //    mlisGiro = ListarGiroTodo();
            //    if (mlisGiro.Count > 0)
            //    {
            //        foreach (EGiro objal in mlisGiro)
            //        {
            //            fs.Write("PVT_GIRO," + objal.giroc_icod_giro + "," +
            //                     objal.giroc_vcod_giro + "," +
            //                     objal.giroc_vnombre_giro + "," +
            //                     objal.giroc_iactivo
            //                    );
            //            fs.WriteLine();
            //        }

            //    }
            //    #endregion

            //}
            //if (tabla == "PVT_UBICACION")
            //{
            //    #region PVT_UBICACION
            //    List<EUbicacion> mlisUbica = new List<EUbicacion>();
            //    mlisUbica = ListarUbicacionTodo();
            //    if (mlisUbica.Count > 0)
            //    {
            //        foreach (EUbicacion objal in mlisUbica)
            //        {
            //            fs.Write("PVT_UBICACION," + objal.ubicc_icod_ubicacion + "," +
            //                     objal.tablc_iid_tipo_ubicacion + "," +
            //                     objal.ubicc_vcod_ubicacion + "," +
            //                     objal.ubicc_vnombre_ubicacion + "," +
            //                     objal.ubicc_icod_ubicacion_padre + "," +
            //                     objal.ubicc_iactivo
            //                  );
            //            fs.WriteLine();
            //        }

            //    }
            //    #endregion

            //}
            //if (tabla == "PVT_PERSONAL")
            //{
            //    #region PVT_PERSONAL
            //    List<EPersonal> mlisPersonal = new List<EPersonal>();
            //    mlisPersonal = Listar();
            //    if (mlisPersonal.Count > 0)
            //    {
            //        foreach (EPersonal objal in mlisPersonal)
            //        {
            //            fs.Write("PVT_PERSONAL," + objal.persc_icod_personal + "," +
            //                     objal.persc_vcod_personal + "," +
            //                     objal.persc_vnombre_personal + "," +
            //                     objal.persc_vdireccion + "," +
            //                     objal.persc_vnumero_telefono + "," +
            //                     objal.persc_vnumero_dni + "," +
            //                     objal.persc_vcorreo + "," +
            //                     objal.tablc_iid_tipo_personal + "," +
            //                     objal.persc_iactivo);
            //            fs.WriteLine();
            //        }

            //    }
            //    #endregion
            //}
            //if (tabla == "PVT_TRANSPORTISTA")
            //{
            //    #region PVT_TRANSPORTISTA
            //    List<ETransportista> mlisTrans = new List<ETransportista>();
            //    mlisTrans = ListarTransportistaTodo();
            //    if (mlisTrans.Count > 0)
            //    {
            //        foreach (ETransportista objal in mlisTrans)
            //        {
            //            fs.Write("PVT_TRANSPORTISTA," + objal.tranc_icod_transportista + "," +
            //                     objal.tranc_iid_transportista + "," +
            //                     objal.tranc_vnombre_transportista + "," +
            //                     objal.tranc_vruc + "," +
            //                     objal.tranc_vdireccion + "," +
            //                     objal.tranc_vnumero_telefono + "," +
            //                     objal.tranc_vnumero_dni + "," +
            //                     objal.tranc_vnombre_conductor + "," +
            //                     objal.tranc_vnum_licencia_conducir + "," +
            //                     objal.tranc_vnum_placa + "," +
            //                     objal.tranc_vnum_matricula + "," +
            //                     objal.tranc_iactivo);
            //            fs.WriteLine();
            //        }

            //    }
            //    #endregion
            //}
            //if (tabla == "SIGT_PROVEEDOR")
            //{
            //    #region PVT_TRANSPORTISTA
            //    List<EProveedor> mlisProvee = new List<EProveedor>();
            //    mlisProvee = ListarProveedorTodo();
            //    if (mlisProvee.Count > 0)
            //    {
            //        foreach (EProveedor objal in mlisProvee)
            //        {
            //            fs.Write("SIGT_PROVEEDOR," + objal.iid_icod_proveedor + "," +
            //                     objal.iid_proveedor + "," +
            //                     objal.vcod_proveedor + "," +
            //                     objal.vnombre + "," +
            //                     objal.vpaterno + "," +
            //                     objal.vmaterno + "," +
            //                     objal.vnombrecompleto + "," +
            //                     objal.iid_tipo_persona + "," +
            //                     objal.vcomercial + "," +
            //                     objal.vdireccion + "," +
            //                     objal.vtelefono + "," +
            //                     objal.vfax + "," +
            //                     objal.isituacion + "," +
            //                     objal.vrepresentante + "," +
            //                     objal.vruc + "," +
            //                     objal.vempresarelacionada + "," +
            //                     objal.iactivo + "," +
            //                     objal.vdni + "," +
            //                     objal.proc_vcorreo + "," +
            //                     objal.proc_sfecha + "," +
            //                     objal.proc_tipo_doc + "," +
            //                     objal.ubicc_icod_ubicacion + "," +
            //                     objal.anac_icod_analitica);
            //            fs.WriteLine();
            //        }

            //    }
            //    #endregion
            //}
            fs.Close();
        }
        #endregion

        #region Lista de Precio
        #region lista Precios

        public List<EProdListaPreciosDetalle> ListarProdProductoTallas(string pr_vcodigo_externo)
        {
            List<EProdListaPreciosDetalle> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdListaPreciosDetalle>();
                    var query = dc.SGE_PVT_LISTA_PRODUCTO_TALLAS(pr_vcodigo_externo);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdListaPreciosDetalle()
                        {
                            pr_vcodigo_externo = item.pr_vcodigo_externo,
                            destalla = item.destalla
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
        public List<EProdListaPrecios> ListarProdListaPreciosUnRegistro(int lprec_icod_precio)
        {
            List<EProdListaPrecios> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdListaPrecios>();
                    var query = dc.SGE_PVT_LISTA_PRECIOS_LISTAR_UN_REGISTRO(lprec_icod_precio);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdListaPrecios()
                        {
                            pr_icod_producto = item.pr_icod_producto,
                            pr_vcodigo_externo = item.pr_vcodigo_externo,
                            pr_vdescripcion_producto = item.pr_vdescripcion_producto,
                            lprec_nprecio_costo = item.lprec_nprecio_costo,
                            lprec_nporc_utilidad = item.lprec_nporc_utilidad,
                            lprec_nprecio_vtamin = item.lprec_nprecio_vtamin,
                            lprec_nprecio_vtamay = item.lprec_nprecio_vtamay,
                            lprec_nprecio_vtaotros = item.lprec_nprecio_vtaotros,
                            lprec_bdetalle = item.lprec_bdetalle,
                            lprec_iactivo = item.lprec_iactivo
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
        public void ActulizarProdlprec_bdetalle(int lprec_icod_precio, bool lprec_bdetalle)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_LISTA_PRECIOS_INSERTARDETALLE(lprec_icod_precio, lprec_bdetalle);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EProdListaPrecios> ListaProdProductoSinTalla(int id_marca, int id_modelo, int id_color)
        {
            List<EProdListaPrecios> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdListaPrecios>();
                    var query = dc.SGE_PVT_LISTA_PRODUCTO_SIN_TALLA(id_marca, id_modelo, id_color);
                    foreach (var item in query)
                    {
                        if (lista.Where(ob => ob.pr_vcodigo_externo.Trim().Substring(0, 13) == item.pr_vcodigo_externo.Trim().Substring(0, 13)).Count() == 0)
                        {
                            lista.Add(new EProdListaPrecios()
                            {
                                pr_vcodigo_externo = item.pr_vcodigo_externo,
                                pr_vdescripcion_producto = item.pr_vdescripcion_producto,
                                lprec_nprecio_costo = item.lprec_nprecio_costo,
                                lprec_nporc_utilidad = item.lprec_nporc_utilidad,
                                //lpred_nprecio_vtamin = item.lpred_nprecio_vtamin,
                                //lpred_nprecio_vtamay = item.lpred_nprecio_vtamay,
                                //lpred_nprecio_vtaotros = item.lpred_nprecio_vtaotros,
                                lprec_bdetalle = item.lprec_bdetalle,
                                lprec_iactivo = item.lprec_iactivo,
                                pr_iid_marca = Convert.ToInt32(item.pr_iid_marca),
                                pr_iid_modelo = Convert.ToInt32(item.pr_iid_modelo),
                                pr_iid_color = Convert.ToInt32(item.pr_iid_color)
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
        public EProdListaPrecios ListarProdListaPreciosXCodigo(string codigo_Externo)
        {
            EProdListaPrecios obj = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    obj = new EProdListaPrecios();
                    var query = dc.SGE_PVT_LISTA_PRECIOS_LISTAR_X_CODIGO(codigo_Externo.Substring(0, 13));
                    foreach (var item in query)
                    {

                        obj.lprec_icod_precio = item.lprec_icod_precio;
                        obj.pr_icod_producto = item.pr_icod_producto;
                        obj.pr_vcodigo_externo = item.pr_vcodigo_externo;
                        obj.pr_vdescripcion_producto = item.pr_vdescripcion_producto;
                        obj.lprec_nprecio_costo = item.lprec_nprecio_costo;
                        obj.lprec_nporc_utilidad = item.lprec_nporc_utilidad;
                        obj.lprec_nprecio_vtamin = item.lprec_nprecio_vtamin;
                        obj.lprec_nprecio_vtamay = item.lprec_nprecio_vtamay;
                        obj.lprec_nprecio_vtaotros = item.lprec_nprecio_vtaotros;
                        obj.lprec_bdetalle = item.lprec_bdetalle;
                        obj.lprec_iactivo = item.lprec_iactivo;

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;

        }

        public int? InsertarProdListaPrecio(EProdListaPrecios obj)
        {
            int? icod_lista_precio = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_LISTA_PRECIOS_INSERTAR(
                        ref icod_lista_precio,
                        obj.pr_icod_producto,
                        obj.pr_vcodigo_externo,
                        obj.pr_vdescripcion_producto,
                        obj.lprec_nprecio_costo,
                        obj.lprec_nporc_utilidad,
                        obj.lprec_nprecio_vtamin,
                        obj.lprec_nprecio_vtamay,
                        obj.lprec_nprecio_vtaotros,
                        obj.lprec_bdetalle,
                        obj.lprec_iactivo,
                        obj.iusuario
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return icod_lista_precio;
        }

        public void ActualizarProdListaPrecio(EProdListaPrecios obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_LISTA_PRECIOS_ACTUALIZAR(
                        obj.lprec_icod_precio,
                        obj.lprec_nprecio_costo,
                        obj.lprec_nporc_utilidad,
                        obj.lprec_nprecio_vtamin,
                        obj.lprec_nprecio_vtamay,
                        obj.lprec_nprecio_vtaotros,
                        obj.lprec_bdetalle,
                        obj.lprec_iactivo,
                        obj.iusuario);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarProdListaPrecio(EProdListaPrecios obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_LISTA_PRECIOS_ELIMINAR(obj.lprec_icod_precio);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Lista Precios Detalle
        public List<EProdListaPreciosDetalle> ListarProdListaPrecioDetalle(int lprec_icod_precio)
        {
            List<EProdListaPreciosDetalle> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdListaPreciosDetalle>();
                    var query = dc.SGE_PVT_LISTA_PRECIOS_DETALLE_LISTAR(lprec_icod_precio);
                    foreach (var item in query)
                    {

                        lista.Add(new EProdListaPreciosDetalle()
                        {
                            lpred_icod_det_precio = item.lpred_icod_det_precio,
                            lprec_icod_precio = item.lprec_icod_precio,
                            descripcionProducto = item.descripcionProducto,
                            lpred_vrango_tallas = item.lpred_vrango_tallas,
                            lpred_nprecio_vtamin = item.lpred_nprecio_vtamin,
                            lpred_nprecio_vtamay = item.lpred_nprecio_vtamay,
                            lpred_nprecio_vtaotros = item.lpred_nprecio_vtaotros,
                            lpred_iactivo = item.lpred_iactivo,

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
        public void InsertarProdListaPrecioDetalle(EProdListaPreciosDetalle obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_LISTA_PRECIOS_DETALLE_INSERTAR(
                        obj.lprec_icod_precio,
                        obj.lpred_vrango_tallas,
                        obj.lpred_nprecio_vtamin,
                        obj.lpred_nprecio_vtamay,
                        obj.lpred_nprecio_vtaotros,
                        obj.lpred_iactivo,
                        obj.iusuario
                        );
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
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_LISTA_PRECIOS_DETALLE_ACTUALIZAR(
                        obj.lpred_icod_det_precio,
                        obj.lpred_vrango_tallas,
                        obj.lpred_nprecio_vtamin,
                        obj.lpred_nprecio_vtamay,
                        obj.lpred_nprecio_vtaotros,
                        obj.lpred_iactivo,
                        obj.iusuario);
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
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_LISTA_PRECIOS_DETALLE_ELIMINAR(obj.lpred_icod_det_precio);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public List<EProdListaPrecios> ListarProdListaPreciosTodo()
        {
            List<EProdListaPrecios> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdListaPrecios>();
                    var query = dc.SGE_PVT_LISTA_PRECIOS_LISTAR_TRANS();
                    foreach (var item in query)
                    {
                        lista.Add(new EProdListaPrecios()
                        {
                            lprec_icod_precio = item.lprec_icod_precio,
                            pr_icod_producto = item.pr_icod_producto,
                            pr_vcodigo_externo = item.pr_vcodigo_externo,
                            pr_vdescripcion_producto = item.pr_vdescripcion_producto,
                            lprec_nprecio_costo = item.lprec_nprecio_costo,
                            lprec_nporc_utilidad = item.lprec_nporc_utilidad,
                            lprec_nprecio_vtamin = item.lprec_nprecio_vtamin,
                            lprec_nprecio_vtamay = item.lprec_nprecio_vtamay,
                            lprec_nprecio_vtaotros = item.lprec_nprecio_vtaotros,
                            lprec_bdetalle = item.lprec_bdetalle,
                            lprec_iactivo = item.lprec_iactivo
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
        public List<EProdListaPreciosDetalle> ListarProdListaPrecioDetalleTodo()
        {
            List<EProdListaPreciosDetalle> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EProdListaPreciosDetalle>();
                    var query = dc.SGE_PVT_LISTA_PRECIOS_DETALLE_LISTAR_TRANS();
                    foreach (var item in query)
                    {

                        lista.Add(new EProdListaPreciosDetalle()
                        {
                            lpred_icod_det_precio = item.lpred_icod_det_precio,
                            lprec_icod_precio = item.lprec_icod_precio,
                            lpred_vrango_tallas = item.lpred_vrango_tallas,
                            lpred_nprecio_vtamin = item.lpred_nprecio_vtamin,
                            lpred_nprecio_vtamay = item.lpred_nprecio_vtamay,
                            lpred_nprecio_vtaotros = item.lpred_nprecio_vtaotros,
                            lpred_iactivo = item.lpred_iactivo,

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

        #region Registro de serie
        public List<ERegistroSerie> ListarRegistroSerie()
        {
            List<ERegistroSerie> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<ERegistroSerie>();
                    var query = dc.SGE_PVT_REGISTRO_SERIE_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ERegistroSerie()
                        {
                            resec_iid_registro_serie = item.resec_iid_registro_serie,
                            resec_icod_registro_serie = item.resec_icod_registro_serie,
                            resec_vdescripcion = item.resec_vdescripcion,
                            resec_vtalla_inicial = item.resec_vtalla_inicial,
                            resec_vtalla_final = item.resec_vtalla_final,
                            resec_flag_estado = item.resec_flag_estado,
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
        public int InsertarRegistroSerie(ERegistroSerie obj)
        {
            int? intIcod = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {

                    dc.SGE_PVT_REGISTRO_SERIE_INSERTAR(
                        ref intIcod,
                        obj.resec_icod_registro_serie,
                        obj.resec_vdescripcion,
                        obj.resec_vtalla_inicial,
                        obj.resec_vtalla_final,
                        obj.intUsuario,
                        obj.vpc
                         );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarRegistroSerie(ERegistroSerie obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_REGISTRO_SERIE_MODIFICAR(
                        obj.resec_iid_registro_serie,
                        obj.resec_icod_registro_serie,
                        obj.resec_vdescripcion,
                        obj.resec_vtalla_inicial,
                        obj.resec_vtalla_final,
                        obj.intUsuario,
                        obj.vpc
                        );
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
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_REGISTRO_SERIE_ELIMINAR(
                    obj.resec_iid_registro_serie,
                    obj.intUsuario,
                    obj.vpc
                    );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Orden de Produccion
        public List<EOrdenProduccion> ListarOrdenProduccion(int Ejercicio)
        {
            List<EOrdenProduccion> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EOrdenProduccion>();
                    var query = dc.SGE_PVT_ORDEN_PRODUCCION_LISTAR(Ejercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenProduccion()
                        {
                            orprc_iid_orden_produccion = item.orprc_iid_orden_produccion,
                            orprc_icod_orden_produccion = item.orprc_icod_orden_produccion,
                            orprc_sfecha_orden_produccion = Convert.ToDateTime(item.orprc_sfecha_orden_produccion),
                            orprc_ipedido = Convert.ToInt32(item.orprc_ipedido),
                            orprc_vpedido = item.orprc_vpedido.ToString(),
                            orprc_iitem_pedido = Convert.ToInt32(item.orprc_iitem_pedido),
                            orprc_vitem_pedido = item.orprc_vitem_pedido.ToString(),
                            orprc_ificha_tecnica = Convert.ToInt32(item.orprc_ificha_tecnica),
                            orprc_vficha_tecnica = item.orprc_vficha_tecnica.ToString(),
                            orprc_iresponsable = Convert.ToInt32(item.orprc_iresponsable),
                            orprc_vresponsable = item.orprc_vresponsable,
                            orprc_imodelo = Convert.ToInt32(item.orprc_imodelo),
                            DesModelo = item.DesModelo,
                            orprc_icolor = Convert.ToInt32(item.orprc_icolor),
                            orprc_imarca = Convert.ToInt32(item.orprc_imarca),
                            DesMarca = item.DesMarca,
                            orprc_itipo = Convert.ToInt32(item.orprc_itipo),
                            orprc_iserie = Convert.ToInt32(item.orprc_iserie),
                            orprc_ilinea = Convert.ToInt32(item.orprc_ilinea),
                            orprc_vlinea = item.orprc_vlinea.ToString(),
                            orprc_talla1 = Convert.ToInt32(item.orprc_talla1),
                            orprc_talla2 = Convert.ToInt32(item.orprc_talla2),
                            orprc_talla3 = Convert.ToInt32(item.orprc_talla3),
                            orprc_talla4 = Convert.ToInt32(item.orprc_talla4),
                            orprc_talla5 = Convert.ToInt32(item.orprc_talla5),
                            orprc_talla6 = Convert.ToInt32(item.orprc_talla6),
                            orprc_talla7 = Convert.ToInt32(item.orprc_talla7),
                            orprc_talla8 = Convert.ToInt32(item.orprc_talla8),
                            orprc_talla9 = Convert.ToInt32(item.orprc_talla9),
                            orprc_talla10 = Convert.ToInt32(item.orprc_talla10),
                            orprc_cant_talla1 = Convert.ToInt32(item.orprc_cant_talla1),
                            orprc_cant_talla2 = Convert.ToInt32(item.orprc_cant_talla2),
                            orprc_cant_talla3 = Convert.ToInt32(item.orprc_cant_talla3),
                            orprc_cant_talla4 = Convert.ToInt32(item.orprc_cant_talla4),
                            orprc_cant_talla5 = Convert.ToInt32(item.orprc_cant_talla5),
                            orprc_cant_talla6 = Convert.ToInt32(item.orprc_cant_talla6),
                            orprc_cant_talla7 = Convert.ToInt32(item.orprc_cant_talla7),
                            orprc_cant_talla8 = Convert.ToInt32(item.orprc_cant_talla8),
                            orprc_cant_talla9 = Convert.ToInt32(item.orprc_cant_talla9),
                            orprc_cant_talla10 = Convert.ToInt32(item.orprc_cant_talla10),
                            orprc_iid_kardex1 = item.orprc_iid_kardex1,
                            orprc_iid_kardex2 = item.orprc_iid_kardex2,
                            orprc_iid_kardex3 = item.orprc_iid_kardex3,
                            orprc_iid_kardex4 = item.orprc_iid_kardex4,
                            orprc_iid_kardex5 = item.orprc_iid_kardex5,
                            orprc_iid_kardex6 = item.orprc_iid_kardex6,
                            orprc_iid_kardex7 = item.orprc_iid_kardex7,
                            orprc_iid_kardex8 = item.orprc_iid_kardex8,
                            orprc_iid_kardex9 = item.orprc_iid_kardex9,
                            orprc_iid_kardex10 = item.orprc_iid_kardex10,
                            orprc_icod_producto1 = item.orprc_icod_producto1,
                            orprc_icod_producto2 = item.orprc_icod_producto2,
                            orprc_icod_producto3 = item.orprc_icod_producto3,
                            orprc_icod_producto4 = item.orprc_icod_producto4,
                            orprc_icod_producto5 = item.orprc_icod_producto5,
                            orprc_icod_producto6 = item.orprc_icod_producto6,
                            orprc_icod_producto7 = item.orprc_icod_producto7,
                            orprc_icod_producto8 = item.orprc_icod_producto8,
                            orprc_icod_producto9 = item.orprc_icod_producto9,
                            orprc_icod_producto10 = item.orprc_icod_producto10,
                            orprc_cant_aprocesar1 = Convert.ToInt32(item.orprc_cant_aprocesar1),
                            orprc_cant_aprocesar2 = Convert.ToInt32(item.orprc_cant_aprocesar2),
                            orprc_cant_aprocesar3 = Convert.ToInt32(item.orprc_cant_aprocesar3),
                            orprc_cant_aprocesar4 = Convert.ToInt32(item.orprc_cant_aprocesar4),
                            orprc_cant_aprocesar5 = Convert.ToInt32(item.orprc_cant_aprocesar5),
                            orprc_cant_aprocesar6 = Convert.ToInt32(item.orprc_cant_aprocesar6),
                            orprc_cant_aprocesar7 = Convert.ToInt32(item.orprc_cant_aprocesar7),
                            orprc_cant_aprocesar8 = Convert.ToInt32(item.orprc_cant_aprocesar8),
                            orprc_cant_aprocesar9 = Convert.ToInt32(item.orprc_cant_aprocesar9),
                            orprc_cant_aprocesar10 = Convert.ToInt32(item.orprc_cant_aprocesar10),
                            orprc_sfecha_ing_kardex = item.orprc_sfecha_ing_kardex,
                            orprc_iid_situacion = Convert.ToInt32(item.orprc_iid_situacion),
                            orprc_ntotal = Convert.ToInt32(item.orprc_ntotal),
                            orprc_ndocenas = Convert.ToDecimal(item.orprc_ndocenas),
                            orprc_vcodigo_producto = item.orprc_vcodigo_producto,
                            orprc_vdescripcion_producto = item.orprc_vdescripcion_producto,
                            orprc_flag_estado = item.orprc_flag_estado,
                            orprc_isituacion = Convert.ToInt32(item.orprc_isituacion),
                            strsituacion = item.strsituacion,
                            strtipo = item.strtipo,
                            strcolor = item.strcolor,
                            resec_vdescripcion = item.resec_vdescripcion,
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

        public EOrdenProduccion OrdenProduccionGetById(int orprc_iid_orden_produccion)
        {
            var obj = new EOrdenProduccion();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_PVT_ORDEN_PRODUCCION_GET_BY_ID(orprc_iid_orden_produccion);
                    foreach (var item in query)
                    {
                        obj = new EOrdenProduccion()
                        {
                            orprc_iid_orden_produccion = item.orprc_iid_orden_produccion,
                            orprc_icod_orden_produccion = item.orprc_icod_orden_produccion,
                            orprc_sfecha_orden_produccion = Convert.ToDateTime(item.orprc_sfecha_orden_produccion),
                            orprc_ipedido = Convert.ToInt32(item.orprc_ipedido),
                            orprc_vpedido = item.orprc_vpedido.ToString(),
                            orprc_iitem_pedido = Convert.ToInt32(item.orprc_iitem_pedido),
                            orprc_vitem_pedido = item.orprc_vitem_pedido.ToString(),
                            orprc_ificha_tecnica = Convert.ToInt32(item.orprc_ificha_tecnica),
                            orprc_vficha_tecnica = item.orprc_vficha_tecnica.ToString(),
                            orprc_iresponsable = Convert.ToInt32(item.orprc_iresponsable),
                            orprc_vresponsable = item.orprc_vresponsable,
                            orprc_imodelo = Convert.ToInt32(item.orprc_imodelo),
                            DesModelo = item.DesModelo,
                            orprc_icolor = Convert.ToInt32(item.orprc_icolor),
                            orprc_imarca = Convert.ToInt32(item.orprc_imarca),
                            DesMarca = item.DesMarca,
                            orprc_itipo = Convert.ToInt32(item.orprc_itipo),
                            orprc_iserie = Convert.ToInt32(item.orprc_iserie),
                            orprc_ilinea = Convert.ToInt32(item.orprc_ilinea),
                            orprc_vlinea = item.orprc_vlinea.ToString(),
                            orprc_talla1 = Convert.ToInt32(item.orprc_talla1),
                            orprc_talla2 = Convert.ToInt32(item.orprc_talla2),
                            orprc_talla3 = Convert.ToInt32(item.orprc_talla3),
                            orprc_talla4 = Convert.ToInt32(item.orprc_talla4),
                            orprc_talla5 = Convert.ToInt32(item.orprc_talla5),
                            orprc_talla6 = Convert.ToInt32(item.orprc_talla6),
                            orprc_talla7 = Convert.ToInt32(item.orprc_talla7),
                            orprc_talla8 = Convert.ToInt32(item.orprc_talla8),
                            orprc_talla9 = Convert.ToInt32(item.orprc_talla9),
                            orprc_talla10 = Convert.ToInt32(item.orprc_talla10),
                            orprc_cant_talla1 = Convert.ToInt32(item.orprc_cant_talla1),
                            orprc_cant_talla2 = Convert.ToInt32(item.orprc_cant_talla2),
                            orprc_cant_talla3 = Convert.ToInt32(item.orprc_cant_talla3),
                            orprc_cant_talla4 = Convert.ToInt32(item.orprc_cant_talla4),
                            orprc_cant_talla5 = Convert.ToInt32(item.orprc_cant_talla5),
                            orprc_cant_talla6 = Convert.ToInt32(item.orprc_cant_talla6),
                            orprc_cant_talla7 = Convert.ToInt32(item.orprc_cant_talla7),
                            orprc_cant_talla8 = Convert.ToInt32(item.orprc_cant_talla8),
                            orprc_cant_talla9 = Convert.ToInt32(item.orprc_cant_talla9),
                            orprc_cant_talla10 = Convert.ToInt32(item.orprc_cant_talla10),
                            orprc_iid_kardex1 = item.orprc_iid_kardex1,
                            orprc_iid_kardex2 = item.orprc_iid_kardex2,
                            orprc_iid_kardex3 = item.orprc_iid_kardex3,
                            orprc_iid_kardex4 = item.orprc_iid_kardex4,
                            orprc_iid_kardex5 = item.orprc_iid_kardex5,
                            orprc_iid_kardex6 = item.orprc_iid_kardex6,
                            orprc_iid_kardex7 = item.orprc_iid_kardex7,
                            orprc_iid_kardex8 = item.orprc_iid_kardex8,
                            orprc_iid_kardex9 = item.orprc_iid_kardex9,
                            orprc_iid_kardex10 = item.orprc_iid_kardex10,
                            orprc_icod_producto1 = item.orprc_icod_producto1,
                            orprc_icod_producto2 = item.orprc_icod_producto2,
                            orprc_icod_producto3 = item.orprc_icod_producto3,
                            orprc_icod_producto4 = item.orprc_icod_producto4,
                            orprc_icod_producto5 = item.orprc_icod_producto5,
                            orprc_icod_producto6 = item.orprc_icod_producto6,
                            orprc_icod_producto7 = item.orprc_icod_producto7,
                            orprc_icod_producto8 = item.orprc_icod_producto8,
                            orprc_icod_producto9 = item.orprc_icod_producto9,
                            orprc_icod_producto10 = item.orprc_icod_producto10,
                            orprc_cant_aprocesar1 = Convert.ToInt32(item.orprc_cant_aprocesar1),
                            orprc_cant_aprocesar2 = Convert.ToInt32(item.orprc_cant_aprocesar2),
                            orprc_cant_aprocesar3 = Convert.ToInt32(item.orprc_cant_aprocesar3),
                            orprc_cant_aprocesar4 = Convert.ToInt32(item.orprc_cant_aprocesar4),
                            orprc_cant_aprocesar5 = Convert.ToInt32(item.orprc_cant_aprocesar5),
                            orprc_cant_aprocesar6 = Convert.ToInt32(item.orprc_cant_aprocesar6),
                            orprc_cant_aprocesar7 = Convert.ToInt32(item.orprc_cant_aprocesar7),
                            orprc_cant_aprocesar8 = Convert.ToInt32(item.orprc_cant_aprocesar8),
                            orprc_cant_aprocesar9 = Convert.ToInt32(item.orprc_cant_aprocesar9),
                            orprc_cant_aprocesar10 = Convert.ToInt32(item.orprc_cant_aprocesar10),
                            orprc_sfecha_ing_kardex = item.orprc_sfecha_ing_kardex,
                            orprc_iid_situacion = Convert.ToInt32(item.orprc_iid_situacion),
                            orprc_ntotal = Convert.ToInt32(item.orprc_ntotal),
                            orprc_ndocenas = Convert.ToDecimal(item.orprc_ndocenas),
                            orprc_vcodigo_producto = item.orprc_vcodigo_producto,
                            orprc_vdescripcion_producto = item.orprc_vdescripcion_producto,
                            orprc_flag_estado = item.orprc_flag_estado,
                            orprc_isituacion = Convert.ToInt32(item.orprc_isituacion),
                            strsituacion = item.strsituacion,
                            strtipo = item.strtipo,
                            strcolor = item.strcolor,
                            resec_vdescripcion = item.resec_vdescripcion,
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public int InsertarOrdenProduccion(EOrdenProduccion obj)
        {
            int? intIcod = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {

                    dc.SGE_PVT_ORDEN_PRODUCCION_INSERTAR(
                        ref intIcod,
                        obj.orprc_icod_orden_produccion,
                        Convert.ToDateTime(obj.orprc_sfecha_orden_produccion),
                        obj.orprc_ipedido,
                        obj.orprc_vpedido,
                        obj.orprc_iitem_pedido,
                        obj.orprc_vitem_pedido,
                        obj.orprc_ificha_tecnica,
                        obj.orprc_vficha_tecnica,
                        obj.orprc_iresponsable,
                        obj.orprc_vresponsable,
                        obj.orprc_imodelo,
                        obj.orprc_icolor,
                        obj.orprc_imarca,
                        obj.orprc_itipo,
                        obj.orprc_iserie,
                        obj.orprc_ilinea,
                        obj.orprc_vlinea,
                        obj.orprc_talla1,
                        obj.orprc_talla2,
                        obj.orprc_talla3,
                        obj.orprc_talla4,
                        obj.orprc_talla5,
                        obj.orprc_talla6,
                        obj.orprc_talla7,
                        obj.orprc_talla8,
                        obj.orprc_talla9,
                        obj.orprc_talla10,
                        obj.orprc_cant_talla1,
                        obj.orprc_cant_talla2,
                        obj.orprc_cant_talla3,
                        obj.orprc_cant_talla4,
                        obj.orprc_cant_talla5,
                        obj.orprc_cant_talla6,
                        obj.orprc_cant_talla7,
                        obj.orprc_cant_talla8,
                        obj.orprc_cant_talla9,
                        obj.orprc_cant_talla10,
                        obj.orprc_iid_kardex1,
                        obj.orprc_iid_kardex2,
                        obj.orprc_iid_kardex3,
                        obj.orprc_iid_kardex4,
                        obj.orprc_iid_kardex5,
                        obj.orprc_iid_kardex6,
                        obj.orprc_iid_kardex7,
                        obj.orprc_iid_kardex8,
                        obj.orprc_iid_kardex9,
                        obj.orprc_iid_kardex10,
                        obj.orprc_icod_producto1,
                        obj.orprc_icod_producto2,
                        obj.orprc_icod_producto3,
                        obj.orprc_icod_producto4,
                        obj.orprc_icod_producto5,
                        obj.orprc_icod_producto6,
                        obj.orprc_icod_producto7,
                        obj.orprc_icod_producto8,
                        obj.orprc_icod_producto9,
                        obj.orprc_icod_producto10,
                        obj.orprc_cant_aprocesar1,
                        obj.orprc_cant_aprocesar2,
                        obj.orprc_cant_aprocesar3,
                        obj.orprc_cant_aprocesar4,
                        obj.orprc_cant_aprocesar5,
                        obj.orprc_cant_aprocesar6,
                        obj.orprc_cant_aprocesar7,
                        obj.orprc_cant_aprocesar8,
                        obj.orprc_cant_aprocesar9,
                        obj.orprc_cant_aprocesar10,
                        obj.orprc_iid_almacen,
                        obj.orprc_sfecha_ing_kardex,
                        obj.orprc_iid_situacion,
                        obj.orprc_ntotal,
                        obj.orprc_ndocenas,
                        obj.orprc_vcodigo_producto,
                        obj.orprc_vdescripcion_producto,
                        obj.intUsuario,
                        obj.vpc,
                        obj.orprc_isituacion
                         );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarOrdenProduccion(EOrdenProduccion obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_ORDEN_PRODUCCION_MODIFICAR(
                        obj.orprc_iid_orden_produccion,
                        obj.orprc_icod_orden_produccion,
                         Convert.ToDateTime(obj.orprc_sfecha_orden_produccion),
                        obj.orprc_ipedido,
                        obj.orprc_vpedido,
                        obj.orprc_iitem_pedido,
                        obj.orprc_vitem_pedido,
                        obj.orprc_ificha_tecnica,
                        obj.orprc_vficha_tecnica,
                        obj.orprc_iresponsable,
                        obj.orprc_vresponsable,
                        obj.orprc_imodelo,
                        obj.orprc_icolor,
                        obj.orprc_imarca,
                        obj.orprc_itipo,
                        obj.orprc_iserie,
                        obj.orprc_ilinea,
                        obj.orprc_vlinea,
                        obj.orprc_talla1,
                        obj.orprc_talla2,
                        obj.orprc_talla3,
                        obj.orprc_talla4,
                        obj.orprc_talla5,
                        obj.orprc_talla6,
                        obj.orprc_talla7,
                        obj.orprc_talla8,
                        obj.orprc_talla9,
                        obj.orprc_talla10,
                        obj.orprc_cant_talla1,
                        obj.orprc_cant_talla2,
                        obj.orprc_cant_talla3,
                        obj.orprc_cant_talla4,
                        obj.orprc_cant_talla5,
                        obj.orprc_cant_talla6,
                        obj.orprc_cant_talla7,
                        obj.orprc_cant_talla8,
                        obj.orprc_cant_talla9,
                        obj.orprc_cant_talla10,
                        obj.orprc_iid_kardex1,
                        obj.orprc_iid_kardex2,
                        obj.orprc_iid_kardex3,
                        obj.orprc_iid_kardex4,
                        obj.orprc_iid_kardex5,
                        obj.orprc_iid_kardex6,
                        obj.orprc_iid_kardex7,
                        obj.orprc_iid_kardex8,
                        obj.orprc_iid_kardex9,
                        obj.orprc_iid_kardex10,
                        obj.orprc_icod_producto1,
                        obj.orprc_icod_producto2,
                        obj.orprc_icod_producto3,
                        obj.orprc_icod_producto4,
                        obj.orprc_icod_producto5,
                        obj.orprc_icod_producto6,
                        obj.orprc_icod_producto7,
                        obj.orprc_icod_producto8,
                        obj.orprc_icod_producto9,
                        obj.orprc_icod_producto10,
                        obj.orprc_cant_aprocesar1,
                        obj.orprc_cant_aprocesar2,
                        obj.orprc_cant_aprocesar3,
                        obj.orprc_cant_aprocesar4,
                        obj.orprc_cant_aprocesar5,
                        obj.orprc_cant_aprocesar6,
                        obj.orprc_cant_aprocesar7,
                        obj.orprc_cant_aprocesar8,
                        obj.orprc_cant_aprocesar9,
                        obj.orprc_cant_aprocesar10,
                        obj.orprc_iid_almacen,
                        obj.orprc_sfecha_ing_kardex,
                        obj.orprc_iid_situacion,
                        obj.orprc_ntotal,
                        obj.orprc_ndocenas,
                        obj.orprc_vcodigo_producto,
                        obj.orprc_vdescripcion_producto,
                        obj.intUsuario,
                        obj.vpc,
                        obj.orprc_isituacion
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarOrdenProduccion(EOrdenProduccion obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_ORDEN_PRODUCCION_ELIMINAR(
                    obj.orprc_iid_orden_produccion,
                    obj.intUsuario,
                    obj.vpc
                    );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Orden Produccion Detalle
        public List<EOrdenProduccionDet> listarOrdenPrduccionDetalle(int intOrdenPrduccion)
        {
            List<EOrdenProduccionDet> lista = new List<EOrdenProduccionDet>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_ORDEN_PRODUCCION_DET_LISTAR(intOrdenPrduccion);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenProduccionDet()
                        {
                            orprd_icod_item_orden_produccion = item.orprd_icod_item_orden_produccion,
                            orprc_iid_orden_produccion = item.orprc_iid_orden_produccion,
                            orprd_iitem_orden_produccion = item.orprd_iitem_orden_produccion,
                            orprd_iproceso = Convert.ToInt32(item.orprd_iproceso),
                            orprd_vubicacion = item.orprd_vubicacion,
                            orprd_iubicacion = Convert.ToInt32(item.orprd_iubicacion),
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            orprd_vmaterial = item.orprd_vmateria,
                            orprd_ncantidad = Convert.ToDecimal(item.orprd_ncantidad),
                            orprd_imedida = Convert.ToInt32(item.orprd_imedida),
                            strproceso = item.strproceso,
                            strCodeProducto = item.strCodeProducto,
                            strProducto = item.strProducto,
                            strmedida = item.strmedida
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
        public void insertarOrdenPrduccionDetalle(EOrdenProduccionDet ob)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_ORDEN_PRODUCCION_DET_INSERTAR(
                            ob.orprc_iid_orden_produccion,
                            ob.orprd_iitem_orden_produccion,
                            ob.orprd_iproceso,
                            ob.orprd_iubicacion,
                            ob.orprd_vubicacion,
                            ob.prdc_icod_producto,
                            ob.orprd_vmaterial,
                            ob.orprd_ncantidad,
                            ob.orprd_imedida,
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
        public void modificarOrdenPrduccionDetalle(EOrdenProduccionDet ob)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_ORDEN_PRODUCCION_DET_MODIFICAR(
                            ob.orprd_icod_item_orden_produccion,
                            ob.orprd_iitem_orden_produccion,
                            ob.orprd_iproceso,
                            ob.orprd_iubicacion,
                            ob.orprd_vubicacion,
                            ob.prdc_icod_producto,
                            ob.orprd_vmaterial,
                            ob.orprd_ncantidad,
                            ob.orprd_imedida,
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
        public void eliminarOrdenPrduccionDetalle(EOrdenProduccionDet ob)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_ORDEN_PRODUCCION_DET_ELIMINAR(
                            ob.orprd_icod_item_orden_produccion,
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
        #region Orden Produccion Area
        public List<EOrdenProduccionAreas> listarOrdenPrduccionArea(int intOrdenPrduccion)
        {
            List<EOrdenProduccionAreas> lista = new List<EOrdenProduccionAreas>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_ORDEN_PRODUCCION_AREAS_LISTAR(intOrdenPrduccion);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenProduccionAreas()
                        {
                            orprac_iid_codigo = item.orprac_iid_codigo,
                            orprc_iid_orden_produccion = item.orprc_iid_orden_produccion,
                            orprac_icod_proceso = Convert.ToInt32(item.orprac_icod_proceso),
                            orprac_ipersonal = Convert.ToInt32(item.orprac_ipersonal),
                            orprac_sfecha_asignacion = item.orprac_sfecha_asignacion,
                            orprac_sfecha_terminado = item.orprac_sfecha_terminado,
                            orprac_bterminado = Convert.ToBoolean(item.orprac_bterminado),
                            orprac_bvisto_bueno = Convert.ToBoolean(item.orprac_bvisto_bueno),
                            strproceso = item.strproceso,
                            strpersonal = item.strpersonal,
                            orprc_isituacion = item.orprc_isituacion,
                            strEstado = item.strEstado
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
        public void insertarOrdenPrduccionArea(EOrdenProduccionAreas ob)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_ORDEN_PRODUCCION_AREAS_INSERTAR(
                            ob.orprc_iid_orden_produccion,
                            ob.orprac_icod_proceso,
                            ob.orprac_ipersonal,
                            ob.orprac_sfecha_asignacion,
                            ob.orprac_sfecha_terminado,
                            ob.orprac_bterminado,
                            ob.orprac_bvisto_bueno,
                            ob.intUsuario,
                            ob.strPc,
                            ob.orprc_isituacion
                           );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarOrdenPrduccionArea(EOrdenProduccionAreas ob)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_ORDEN_PRODUCCION_AREAS_MODIFICAR(
                            ob.orprac_iid_codigo,
                            ob.orprac_icod_proceso,
                            ob.orprac_ipersonal,
                            ob.orprac_sfecha_asignacion,
                            ob.orprac_sfecha_terminado,
                            ob.orprac_bterminado,
                            ob.orprac_bvisto_bueno,
                            ob.intUsuario,
                            ob.strPc,
                            ob.orprc_isituacion
                            );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarOrdenPrduccionArea(EOrdenProduccionAreas ob)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_ORDEN_PRODUCCION_AREAS_ELIMINAR(
                            ob.orprac_iid_codigo,
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
        #region Orden de Pedido Cab
        public List<EOrdenPedidoCab> ListarOrdenPedidoCab(int intEjericio)
        {
            List<EOrdenPedidoCab> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EOrdenPedidoCab>();
                    var query = dc.SGE_PVT_ORDEN_PEDIDO_CAB_LISTAR(intEjericio);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenPedidoCab()
                        {
                            orpec_iid_orden_pedido = item.orpec_iid_orden_pedido,
                            orpec_icod_orden_pedido = item.orpec_icod_orden_pedido,
                            orpec_sfecha_pedido = Convert.ToDateTime(item.orpec_sfecha_pedido),
                            orpec_sfecha_entrega_inicial = Convert.ToDateTime(item.orpec_sfecha_entrega_inicial),
                            orpec_sfecha_entrega_final = Convert.ToDateTime(item.orpec_sfecha_entrega_final),
                            orpec_icod_cliente = Convert.ToInt32(item.orpec_icod_cliente),
                            orpec_vnumero_OC = item.orpec_vnumero_OC,
                            orpec_vforma_pago = item.orpec_vforma_pago,
                            orpec_vtransporte = item.orpec_vtransporte,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            pmprc_inumero_orden_pedido = Convert.ToInt32(item.pmprc_inumero_orden_pedido),
                            orpec_itotal_unidades = Convert.ToInt32(item.orpec_itotal_unidades),
                            orpec_ntotal_docenas = Convert.ToDecimal(item.orpec_ntotal_docenas),
                            orpec_isituacion = Convert.ToInt32(item.orpec_isituacion),
                            strsituacion = item.strsituacion,
                            tranc_icod_transportista = Convert.ToInt32(item.tranc_icod_transportista),
                            tranc_vnombre_transportista = item.tranc_vnombre_transportista
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
        public int InsertarOrdenPedidoCab(EOrdenPedidoCab obj)
        {
            int? intIcod = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {

                    dc.SGE_PVT_ORDEN_PEDIDO_CAB_INSERTAR(
                        ref intIcod,
                        obj.orpec_icod_orden_pedido,
                        obj.orpec_sfecha_pedido,
                        obj.orpec_sfecha_entrega_inicial,
                        obj.orpec_sfecha_entrega_final,
                        obj.orpec_icod_cliente,
                        obj.orpec_vnumero_OC,
                        obj.orpec_vforma_pago,
                        obj.orpec_vtransporte,
                        obj.orpec_itotal_unidades,
                        obj.orpec_ntotal_docenas,
                        obj.intUsuario,
                        obj.vpc,
                        obj.orpec_isituacion,
                        obj.tranc_icod_transportista
                         );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarOrdenPedidoCab(EOrdenPedidoCab obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_ORDEN_PEDIDO_CAB_MODIFICAR(
                        obj.orpec_iid_orden_pedido,
                        obj.orpec_icod_orden_pedido,
                        obj.orpec_sfecha_pedido,
                        obj.orpec_sfecha_entrega_inicial,
                        obj.orpec_sfecha_entrega_final,
                        obj.orpec_icod_cliente,
                        obj.orpec_vnumero_OC,
                        obj.orpec_vforma_pago,
                        obj.orpec_vtransporte,
                        obj.orpec_itotal_unidades,
                        obj.orpec_ntotal_docenas,
                        obj.intUsuario,
                        obj.vpc,
                        obj.orpec_isituacion,
                        obj.tranc_icod_transportista
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarOrdenPedidoCab(EOrdenPedidoCab obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_ORDEN_PEDIDO_CAB_ELIMINAR(
                    obj.orpec_iid_orden_pedido,
                    obj.intUsuario,
                    obj.vpc
                    );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Orden de Pedido Detalle
        public List<EOrdenPedidoDet> ListarOrdenPedidoDet(int intordenpedido)
        {
            List<EOrdenPedidoDet> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EOrdenPedidoDet>();
                    var query = dc.SGE_PVT_ORDEN_PEDIDO_DET_LISTAR(intordenpedido);
                    foreach (var item in query)
                    {
                        lista.Add(new EOrdenPedidoDet()
                        {
                            orped_icod_item_orden_pedido = item.orped_icod_item_orden_pedido,
                            orpec_iid_orden_pedido = item.orpec_iid_orden_pedido,
                            orped_iitem_orden_pedido = item.orped_iitem_orden_pedido,
                            orped_iresponsable = Convert.ToInt32(item.orped_iresponsable),
                            orped_ificha_tecnica = Convert.ToInt32(item.orped_ificha_tecnica),
                            orped_imodelo = Convert.ToInt32(item.orped_imodelo),
                            orped_icolor = Convert.ToInt32(item.orped_icolor),
                            orped_imarca = Convert.ToInt32(item.orped_imarca),
                            orped_itipo = Convert.ToInt32(item.orped_itipo),
                            orped_serie = Convert.ToInt32(item.orped_serie),
                            orped_talla1 = Convert.ToInt32(item.orped_talla1),
                            orped_talla2 = Convert.ToInt32(item.orped_talla2),
                            orped_talla3 = Convert.ToInt32(item.orped_talla3),
                            orped_talla4 = Convert.ToInt32(item.orped_talla4),
                            orped_talla5 = Convert.ToInt32(item.orped_talla5),
                            orped_talla6 = Convert.ToInt32(item.orped_talla6),
                            orped_talla7 = Convert.ToInt32(item.orped_talla7),
                            orped_talla8 = Convert.ToInt32(item.orped_talla8),
                            orped_talla9 = Convert.ToInt32(item.orped_talla9),
                            orped_talla10 = Convert.ToInt32(item.orped_talla10),
                            orped_cant_talla1 = Convert.ToInt32(item.orped_cant_talla1),
                            orped_cant_talla2 = Convert.ToInt32(item.orped_cant_talla2),
                            orped_cant_talla3 = Convert.ToInt32(item.orped_cant_talla3),
                            orped_cant_talla4 = Convert.ToInt32(item.orped_cant_talla4),
                            orped_cant_talla5 = Convert.ToInt32(item.orped_cant_talla5),
                            orped_cant_talla6 = Convert.ToInt32(item.orped_cant_talla6),
                            orped_cant_talla7 = Convert.ToInt32(item.orped_cant_talla7),
                            orped_cant_talla8 = Convert.ToInt32(item.orped_cant_talla8),
                            orped_cant_talla9 = Convert.ToInt32(item.orped_cant_talla9),
                            orped_cant_talla10 = Convert.ToInt32(item.orped_cant_talla10),
                            orped_itotal_item = Convert.ToInt32(item.orped_itotal_item),
                            orped_ndocenas = Convert.ToDecimal(item.orped_ndocenas),
                            orped_flag_estado = item.orped_flag_estado,
                            resec_vdescripcion = item.resec_vdescripcion,
                            pr_viid_marca = item.pr_viid_marca,
                            pr_viid_tipo = item.pr_viid_tipo,
                            pr_viid_color = item.pr_viid_color,
                            NomVendedor = item.NomVendedor,
                            strfichatecnica = item.strfichatecnica,
                            strmodelo = item.strmodelo,
                            orped_isituacion = Convert.ToInt32(item.orped_isituacion),
                            strsituacion = item.strsituacion,
                            fitec_ilinea = Convert.ToInt32(item.fitec_ilinea),
                            strlinea = item.strlinea,
                            orped_vruta = item.orped_vruta,
                            orped_vcodigo_cliente = item.orped_vcodigo_cliente,
                            orped_vcolor_cliente = item.orped_vcolor_cliente,
                            orped_dprecio_cliente = Convert.ToDecimal(item.orped_dprecio_cliente),
                            //orped_vhorma = item.orped_vhorma,
                            orped_btercerizado = Convert.ToBoolean(item.orped_btercerizado),
                            orped_vnum_ficha_tecnica = item.orped_vnum_ficha_tecnica,
                            orped_dprecio_total = item.orped_dprecio_total,
                            ConOPR = item.ConOPR,
                            orped_isituacion_entrega = item.orped_isituacion_entrega,
                            SituacionEntrega = item.SituacionEntrega
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
        public int insertarOrdenPedidoDet(EOrdenPedidoDet obj)
        {
            int? codigo = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_ORDEN_PEDIDO_DET_INSERTAR(
                            ref codigo,
                            obj.orpec_iid_orden_pedido,
                            obj.orped_iitem_orden_pedido,
                            obj.orped_iresponsable,
                            obj.orped_ificha_tecnica,
                            obj.orped_imodelo,
                            obj.orped_icolor,
                            obj.orped_imarca,
                            obj.orped_itipo,
                            obj.orped_serie,
                            obj.orped_talla1,
                            obj.orped_talla2,
                            obj.orped_talla3,
                            obj.orped_talla4,
                            obj.orped_talla5,
                            obj.orped_talla6,
                            obj.orped_talla7,
                            obj.orped_talla8,
                            obj.orped_talla9,
                            obj.orped_talla10,
                            obj.orped_cant_talla1,
                            obj.orped_cant_talla2,
                            obj.orped_cant_talla3,
                            obj.orped_cant_talla4,
                            obj.orped_cant_talla5,
                            obj.orped_cant_talla6,
                            obj.orped_cant_talla7,
                            obj.orped_cant_talla8,
                            obj.orped_cant_talla9,
                            obj.orped_cant_talla10,
                            obj.orped_itotal_item,
                            obj.orped_ndocenas,
                            obj.intUsuario,
                            obj.strPc,
                            obj.orped_isituacion,
                            obj.orped_vruta,
                            obj.orped_vcodigo_cliente,
                            obj.orped_vcolor_cliente,
                            obj.orped_dprecio_cliente,
                            obj.orped_btercerizado,
                            obj.orped_vnum_ficha_tecnica,
                            obj.orped_dprecio_total
                           );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Convert.ToInt32(codigo);
        }
        public void ActualizarOrdenPedidoDet(EOrdenPedidoDet obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_ORDEN_PEDIDO_DET_MODIFICAR(
                        obj.orped_icod_item_orden_pedido,
                        obj.orped_iitem_orden_pedido,
                        obj.orped_iresponsable,
                        obj.orped_ificha_tecnica,
                        obj.orped_imodelo,
                        obj.orped_icolor,
                        obj.orped_imarca,
                        obj.orped_itipo,
                        obj.orped_serie,
                        obj.orped_talla1,
                        obj.orped_talla2,
                        obj.orped_talla3,
                        obj.orped_talla4,
                        obj.orped_talla5,
                        obj.orped_talla6,
                        obj.orped_talla7,
                        obj.orped_talla8,
                        obj.orped_talla9,
                        obj.orped_talla10,
                        obj.orped_cant_talla1,
                        obj.orped_cant_talla2,
                        obj.orped_cant_talla3,
                        obj.orped_cant_talla4,
                        obj.orped_cant_talla5,
                        obj.orped_cant_talla6,
                        obj.orped_cant_talla7,
                        obj.orped_cant_talla8,
                        obj.orped_cant_talla9,
                        obj.orped_cant_talla10,
                        obj.orped_itotal_item,
                        obj.orped_ndocenas,
                        obj.intUsuario,
                        obj.vpc,
                        obj.orped_isituacion,
                        obj.orped_vruta,
                        obj.orped_vcodigo_cliente,
                        obj.orped_vcolor_cliente,
                        obj.orped_dprecio_cliente,
                        obj.orped_btercerizado,
                        obj.orped_vnum_ficha_tecnica,
                        obj.orped_dprecio_total
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarOrdenPedidoDet(EOrdenPedidoDet obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_ORDEN_PEDIDO_DET_ELIMINAR(
                    obj.orped_icod_item_orden_pedido,
                    obj.intUsuario,
                    obj.vpc
                    );
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
            List<ENotaPedidoCab> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<ENotaPedidoCab>();
                    var query = dc.SGE_NOTA_PEDIDO_CAB_LISTAR(intEjericio);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaPedidoCab()
                        {
                            nopec_iid_nota_pedido = item.nopec_iid_nota_pedido,
                            nopec_icod_nota_pedido = item.nopec_icod_nota_pedido,
                            nopec_sfecha_pedido = Convert.ToDateTime(item.nopec_sfecha_pedido),
                            nopec_sfecha_entrega_inicial = Convert.ToDateTime(item.nopec_sfecha_entrega_inicial),
                            nopec_sfecha_entrega_final = Convert.ToDateTime(item.nopec_sfecha_entrega_final),
                            nopec_icod_cliente = Convert.ToInt32(item.nopec_icod_cliente),
                            nopec_vnumero_OC = item.nopec_vnumero_OC,
                            nopec_vforma_pago = item.nopec_vforma_pago,
                            nopec_vtransporte = item.nopec_vtransporte,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            pmprc_inumero_orden_pedido = Convert.ToInt32(item.pmprc_inumero_orden_pedido),
                            nopec_itotal_unidades = Convert.ToInt32(item.nopec_itotal_unidades),
                            nopec_ntotal_docenas = Convert.ToDecimal(item.nopec_ntotal_docenas),
                            nopec_isituacion = Convert.ToInt32(item.nopec_isituacion),
                            strsituacion = item.strsituacion,
                            tranc_icod_transportista = Convert.ToInt32(item.tranc_icod_transportista),
                            tranc_vnombre_transportista = item.tranc_vnombre_transportista
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
        public int InsertarNotaPedidoCab(ENotaPedidoCab obj)
        {
            int? intIcod = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {

                    dc.SGE_NOTA_PEDIDO_CAB_INSERTAR(
                        ref intIcod,
                        obj.nopec_icod_nota_pedido,
                        obj.nopec_sfecha_pedido,
                        obj.nopec_sfecha_entrega_inicial,
                        obj.nopec_sfecha_entrega_final,
                        obj.nopec_icod_cliente,
                        obj.nopec_vnumero_OC,
                        obj.nopec_vforma_pago,
                        obj.nopec_vtransporte,
                        obj.nopec_itotal_unidades,
                        obj.nopec_ntotal_docenas,
                        obj.intUsuario,
                        obj.vpc,
                        obj.nopec_isituacion,
                        obj.tranc_icod_transportista
                         );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarNotaPedidoCab(ENotaPedidoCab obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_PEDIDO_CAB_MODIFICAR(
                        obj.nopec_iid_nota_pedido,
                        obj.nopec_icod_nota_pedido,
                        obj.nopec_sfecha_pedido,
                        obj.nopec_sfecha_entrega_inicial,
                        obj.nopec_sfecha_entrega_final,
                        obj.nopec_icod_cliente,
                        obj.nopec_vnumero_OC,
                        obj.nopec_vforma_pago,
                        obj.nopec_vtransporte,
                        obj.nopec_itotal_unidades,
                        obj.nopec_ntotal_docenas,
                        obj.intUsuario,
                        obj.vpc,
                        obj.nopec_isituacion,
                        obj.tranc_icod_transportista
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarNotaPedidoCab(ENotaPedidoCab obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_PEDIDO_CAB_ELIMINAR(
                    obj.nopec_iid_nota_pedido,
                    obj.intUsuario,
                    obj.vpc
                    );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Nota de Pedido Detalle
        public List<ENotaPedidoDet> ListarNotaPedidoDet(int intordenpedido)
        {
            List<ENotaPedidoDet> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<ENotaPedidoDet>();
                    var query = dc.SGE_NOTA_PEDIDO_DET_LISTAR(intordenpedido);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaPedidoDet()
                        {
                            noped_icod_item_nota_pedido = item.noped_icod_item_nota_pedido,
                            nopec_iid_nota_pedido = item.nopec_iid_nota_pedido,
                            noped_iitem_nota_pedido = item.noped_iitem_nota_pedido,
                            noped_iresponsable = Convert.ToInt32(item.noped_iresponsable),
                            noped_ificha_tecnica = Convert.ToInt32(item.noped_ificha_tecnica),
                            noped_imodelo = Convert.ToInt32(item.noped_imodelo),
                            noped_icolor = Convert.ToInt32(item.noped_icolor),
                            noped_imarca = Convert.ToInt32(item.noped_imarca),
                            noped_itipo = Convert.ToInt32(item.noped_itipo),
                            noped_serie = Convert.ToInt32(item.noped_serie),
                            noped_talla1 = Convert.ToInt32(item.noped_talla1),
                            noped_talla2 = Convert.ToInt32(item.noped_talla2),
                            noped_talla3 = Convert.ToInt32(item.noped_talla3),
                            noped_talla4 = Convert.ToInt32(item.noped_talla4),
                            noped_talla5 = Convert.ToInt32(item.noped_talla5),
                            noped_talla6 = Convert.ToInt32(item.noped_talla6),
                            noped_talla7 = Convert.ToInt32(item.noped_talla7),
                            noped_talla8 = Convert.ToInt32(item.noped_talla8),
                            noped_talla9 = Convert.ToInt32(item.noped_talla9),
                            noped_talla10 = Convert.ToInt32(item.noped_talla10),
                            noped_cant_talla1 = Convert.ToInt32(item.noped_cant_talla1),
                            noped_cant_talla2 = Convert.ToInt32(item.noped_cant_talla2),
                            noped_cant_talla3 = Convert.ToInt32(item.noped_cant_talla3),
                            noped_cant_talla4 = Convert.ToInt32(item.noped_cant_talla4),
                            noped_cant_talla5 = Convert.ToInt32(item.noped_cant_talla5),
                            noped_cant_talla6 = Convert.ToInt32(item.noped_cant_talla6),
                            noped_cant_talla7 = Convert.ToInt32(item.noped_cant_talla7),
                            noped_cant_talla8 = Convert.ToInt32(item.noped_cant_talla8),
                            noped_cant_talla9 = Convert.ToInt32(item.noped_cant_talla9),
                            noped_cant_talla10 = Convert.ToInt32(item.noped_cant_talla10),
                            noped_itotal_item = Convert.ToInt32(item.noped_itotal_item),
                            noped_ndocenas = Convert.ToDecimal(item.noped_ndocenas),
                            noped_flag_estado = item.noped_flag_estado,
                            resec_vdescripcion = item.resec_vdescripcion,
                            pr_viid_marca = item.pr_viid_marca,
                            pr_viid_tipo = item.pr_viid_tipo,
                            pr_viid_color = item.pr_viid_color,
                            NomVendedor = item.NomVendedor,
                            strfichatecnica = item.strfichatecnica,
                            strmodelo = item.strmodelo,
                            noped_isituacion = Convert.ToInt32(item.noped_isituacion),
                            strsituacion = item.strsituacion,
                            fitec_ilinea = Convert.ToInt32(item.fitec_ilinea),
                            strlinea = item.strlinea,
                            noped_vruta = item.noped_vruta,
                            noped_vruta_nombre = item.noped_vruta_nombre,
                            noped_vcodigo_cliente = item.noped_vcodigo_cliente,
                            noped_vcolor_cliente = item.noped_vcolor_cliente,
                            noped_nprecio_fabrica = Convert.ToDecimal(item.noped_nprecio_fabrica),
                            noped_nprecio_cliente = Convert.ToDecimal(item.noped_nprecio_cliente),
                            fitec_icorrelativo_contramuestra = item.fitec_icorrelativo_contramuestra.ToString()
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
        public int insertarNotaPedidoDet(ENotaPedidoDet obj)
        {
            int? icod = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_PEDIDO_DET_INSERTAR(
                           ref icod,
                           obj.nopec_iid_nota_pedido,
                           obj.noped_iitem_nota_pedido,
                           obj.noped_iresponsable,
                           obj.noped_ificha_tecnica,
                           obj.noped_imodelo,
                           obj.noped_icolor,
                           obj.noped_imarca,
                           obj.noped_itipo,
                           obj.noped_serie,
                           obj.noped_talla1,
                           obj.noped_talla2,
                           obj.noped_talla3,
                           obj.noped_talla4,
                           obj.noped_talla5,
                           obj.noped_talla6,
                           obj.noped_talla7,
                           obj.noped_talla8,
                           obj.noped_talla9,
                           obj.noped_talla10,
                           obj.noped_cant_talla1,
                           obj.noped_cant_talla2,
                           obj.noped_cant_talla3,
                           obj.noped_cant_talla4,
                           obj.noped_cant_talla5,
                           obj.noped_cant_talla6,
                           obj.noped_cant_talla7,
                           obj.noped_cant_talla8,
                           obj.noped_cant_talla9,
                           obj.noped_cant_talla10,
                           obj.noped_itotal_item,
                           obj.noped_ndocenas,
                           obj.intUsuario,
                           obj.strPc,
                           obj.noped_isituacion,
                           obj.noped_vruta,
                           obj.noped_vruta_nombre,
                           obj.noped_vcodigo_cliente,
                           obj.noped_vcolor_cliente,
                           obj.noped_nprecio_fabrica,
                           obj.noped_nprecio_cliente
                          );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Convert.ToInt32(icod);
        }
        public void ActualizarNotaPedidoDet(ENotaPedidoDet obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_PEDIDO_DET_MODIFICAR(
                        obj.noped_icod_item_nota_pedido,
                        obj.noped_iitem_nota_pedido,
                        obj.noped_iresponsable,
                        obj.noped_ificha_tecnica,
                        obj.noped_imodelo,
                        obj.noped_icolor,
                        obj.noped_imarca,
                        obj.noped_itipo,
                        obj.noped_serie,
                        obj.noped_talla1,
                        obj.noped_talla2,
                        obj.noped_talla3,
                        obj.noped_talla4,
                        obj.noped_talla5,
                        obj.noped_talla6,
                        obj.noped_talla7,
                        obj.noped_talla8,
                        obj.noped_talla9,
                        obj.noped_talla10,
                        obj.noped_cant_talla1,
                        obj.noped_cant_talla2,
                        obj.noped_cant_talla3,
                        obj.noped_cant_talla4,
                        obj.noped_cant_talla5,
                        obj.noped_cant_talla6,
                        obj.noped_cant_talla7,
                        obj.noped_cant_talla8,
                        obj.noped_cant_talla9,
                        obj.noped_cant_talla10,
                        obj.noped_itotal_item,
                        obj.noped_ndocenas,
                        obj.intUsuario,
                        obj.vpc,
                        obj.noped_isituacion,
                        obj.noped_vruta,
                        obj.noped_vruta_nombre,
                        obj.noped_vcodigo_cliente,
                        obj.noped_vcolor_cliente,
                        obj.noped_nprecio_fabrica,
                        obj.noped_nprecio_cliente
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarNotaPedidoDet(ENotaPedidoDet obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_NOTA_PEDIDO_DET_ELIMINAR(
                    obj.noped_icod_item_nota_pedido,
                    obj.intUsuario,
                    obj.vpc
                    );
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
            List<EFichaTecnicaCab> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EFichaTecnicaCab>();
                    var query = dc.SGE_FICHA_TECNICA_LISTAR(intEjericio);
                    foreach (var item in query)
                    {
                        lista.Add(new EFichaTecnicaCab()
                        {
                            fitec_iid_ficha_tecnica = item.fitec_iid_ficha_tecnica,
                            fitec_icod_ficha_tecnica = item.fitec_icod_ficha_tecnica,
                            fitec_icorrelativo_contramuestra = Convert.ToInt32(item.fitec_icorrelativo_contramuestra),
                            fitec_sfecha_ficha_tecnica = Convert.ToDateTime(item.fitec_sfecha_ficha_tecnica),
                            fitec_imarca = Convert.ToInt32(item.fitec_imarca),
                            fitec_imodelo = Convert.ToInt32(item.fitec_imodelo),
                            fitec_itipo = Convert.ToInt32(item.fitec_itipo),
                            fitec_icolor = Convert.ToInt32(item.fitec_icolor),
                            fitec_ilinea = Convert.ToInt32(item.fitec_ilinea),
                            fitec_itipo_trabajo = Convert.ToInt32(item.fitec_itipo_trabajo),
                            fitec_iserie = Convert.ToInt32(item.fitec_iserie),
                            fitec_vruta = item.fitec_vruta,
                            fitec_vobservaciones = item.fitec_vobservaciones,
                            fitec_flag_estado = item.fitec_flag_estado,
                            strmarca = item.strmarca,
                            strmodelo = item.strmodelo,
                            strtipo = item.strtipo,
                            strcolor = item.strcolor,
                            strlinea = item.strlinea,
                            strtipo_trabajo = item.strtipo_trabajo,
                            strserie = item.strserie,
                            strFicharef = item.strFicharef,
                            fitec_iid_ficha_tecnica_ref = item.fitec_iid_ficha_tecnica_ref,
                            fitec_icod_orden_pedido = item.fitec_icod_orden_pedido,
                            fitec_icod_orden_pedido_det = item.fitec_icod_orden_pedido_det,
                            StrOrdenPedido = item.StrOrdenPedido,
                            StrOrdenPedidoDet = item.StrOrdenPedidoDet.ToString()
                        }); ;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int InsertarFichaTecnicaCab(EFichaTecnicaCab obj)
        {
            int? intIcod = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {

                    dc.SGE_FICHA_TECNICA_INSERTAR(
                        ref intIcod,
                        obj.fitec_icod_ficha_tecnica,
                        obj.fitec_icorrelativo_contramuestra,
                        obj.fitec_sfecha_ficha_tecnica,
                        obj.fitec_imarca,
                        obj.fitec_imodelo,
                        obj.fitec_itipo,
                        obj.fitec_icolor,
                        obj.fitec_ilinea,
                        obj.fitec_itipo_trabajo,
                        obj.fitec_iserie,
                        obj.fitec_vruta,
                        obj.fitec_vobservaciones,
                        obj.fitec_iid_ficha_tecnica_ref,
                        obj.intUsuario,
                        obj.vpc,
                        obj.fitec_icod_orden_pedido,
                        obj.fitec_icod_orden_pedido_det
                         );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarFichaTecnicaCab(EFichaTecnicaCab obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_FICHA_TECNICA_MODIFICAR(
                        obj.fitec_iid_ficha_tecnica,
                        obj.fitec_icod_ficha_tecnica,
                        obj.fitec_icorrelativo_contramuestra,
                        obj.fitec_sfecha_ficha_tecnica,
                        obj.fitec_imarca,
                        obj.fitec_imodelo,
                        obj.fitec_itipo,
                        obj.fitec_icolor,
                        obj.fitec_ilinea,
                        obj.fitec_itipo_trabajo,
                        obj.fitec_iserie,
                        obj.fitec_vruta,
                        obj.fitec_vobservaciones,
                        obj.intUsuario,
                        obj.vpc,
                        obj.fitec_icod_orden_pedido,
                        obj.fitec_icod_orden_pedido_det
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarFichaTecnicaCab(EFichaTecnicaCab obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_FICHA_TECNICA_ELIMINAR(
                    obj.fitec_iid_ficha_tecnica,
                    obj.intUsuario,
                    obj.vpc
                    );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Ficha Tecnica Detalle
        public List<EFichaTecnicaDet> ListarFichaTecnicaDet(int intordenpedido)
        {
            List<EFichaTecnicaDet> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EFichaTecnicaDet>();
                    var query = dc.SGE_FICHA_TECNICA_DET_LISTAR(intordenpedido);
                    foreach (var item in query)
                    {
                        lista.Add(new EFichaTecnicaDet()
                        {
                            fited_icod_item_ficha_tecnica = item.fited_icod_item_ficha_tecnica,
                            fitec_iid_ficha_tecnica = item.fitec_iid_ficha_tecnica,
                            fited_iitem_ficha_tecnica = item.fited_iitem_ficha_tecnica,
                            fited_iarea = Convert.ToInt32(item.fited_iarea),
                            fited_iubicacion = Convert.ToInt32(item.fited_iubicacion),
                            fited_vdescripcion = item.fited_vdescripcion,
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            fited_ncantidad = Convert.ToDecimal(item.fited_ncantidad),
                            fited_flag_estado = item.fited_flag_estado,
                            strarea = item.strarea,
                            strubicacion = item.strubicacion,
                            strCodeProducto = item.strCodeProducto,
                            strProducto = item.strProducto,
                            strUnidadMedida = item.unidc_vabreviatura_unidad_medida == null ? item.strUnidadMedida : item.unidc_vabreviatura_unidad_medida,
                            fited_imedida = item.icod_unidad_medida == null ? Convert.ToInt32(item.unidc_icod_unidad_medida) : Convert.ToInt32(item.icod_unidad_medida),
                            strDescripUnidMed = item.unidc_vdescripcion,
                            strareaG = item.strarea,

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
        public void insertarFichaTecnicaDet(EFichaTecnicaDet obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_FICHA_TECNICA_DET_INSERTAR(
                            obj.fitec_iid_ficha_tecnica,
                            obj.fited_iitem_ficha_tecnica,
                            obj.fited_iarea,
                            obj.fited_iubicacion,
                            obj.fited_vdescripcion,
                            obj.prdc_icod_producto,
                            obj.fited_imedida,
                            obj.fited_ncantidad,
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
        public void ActualizarFichaTecnicaDet(EFichaTecnicaDet obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_FICHA_TECNICA_DET_MODIFICAR(
                        obj.fited_icod_item_ficha_tecnica,
                        obj.fited_iitem_ficha_tecnica,
                        obj.fited_iarea,
                        obj.fited_iubicacion,
                        obj.fited_vdescripcion,
                        obj.prdc_icod_producto,
                        obj.fited_imedida,
                        obj.fited_ncantidad,
                        obj.intUsuario,
                        obj.vpc
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarFichaTecnicaDet(EFichaTecnicaDet obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_FICHA_TECNICA_DET_ELIMINAR(
                    obj.fited_icod_item_ficha_tecnica,
                    obj.intUsuario,
                    obj.vpc
                    );
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
            List<EFichaTecnicaMateriales> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EFichaTecnicaMateriales>();
                    var query = dc.SGE_FICHA_TECNICA_MATERIALES_LISTAR(mo_icod_modelo);
                    foreach (var item in query)
                    {
                        lista.Add(new EFichaTecnicaMateriales()
                        {
                            fited_icod_item_ficha_tecnica = item.fited_icod_item_ficha_tecnica,
                            mo_icod_modelo = item.mo_icod_modelo,
                            fited_iitem_ficha_tecnica = item.fited_iitem_ficha_tecnica,
                            fited_iarea = Convert.ToInt32(item.fited_iarea),
                            fited_iubicacion = Convert.ToInt32(item.fited_iubicacion),
                            fited_vdescripcion = item.fited_vdescripcion,
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            fited_ncantidad = Convert.ToDecimal(item.fited_ncantidad),
                            fited_flag_estado = item.fited_flag_estado,
                            strarea = item.strarea,
                            strubicacion = item.strubicacion,
                            strCodeProducto = item.strCodeProducto,
                            strProducto = item.strProducto,
                            strUnidadMedida = item.strUnidadMedida,
                            strUnidMed = item.unidc_vdescripcion,
                            fited_imedida = Convert.ToInt32(item.fited_imedida)
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
        public int insertarFichaTecnicaMateriales(EFichaTecnicaMateriales obj)
        {
            int? intIcod = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_FICHA_TECNICA_MATERIALES_INSERTAR(
                            ref intIcod,
                            obj.mo_icod_modelo,
                            obj.fited_iitem_ficha_tecnica,
                            obj.fited_iarea,
                            obj.fited_iubicacion,
                            obj.fited_vdescripcion,
                            obj.prdc_icod_producto,
                            obj.fited_ncantidad,
                            obj.fited_imedida,
                            obj.intUsuario,
                            obj.strPc,
                            obj.unidc_icod_unidad_medida
                           );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarFichaTecnicaMateriales(EFichaTecnicaMateriales obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_FICHA_TECNICA_MATERIALES_MODIFICAR(
                        obj.fited_icod_item_ficha_tecnica,
                        obj.fited_iitem_ficha_tecnica,
                        obj.fited_iarea,
                        obj.fited_iubicacion,
                        obj.fited_vdescripcion,
                        obj.prdc_icod_producto,
                        obj.fited_ncantidad,
                        obj.fited_imedida,
                        obj.intUsuario,
                        obj.vpc,
                        obj.unidc_icod_unidad_medida
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarFichaTecnicaMateriales(EFichaTecnicaMateriales obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_FICHA_TECNICA_MATERIALES_ELIMINAR(
                    obj.fited_icod_item_ficha_tecnica,
                    obj.intUsuario,
                    obj.vpc
                    );
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
            List<EPlantillaMateriales> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EPlantillaMateriales>();
                    var query = dc.SGE_PLANTILLA_MATERIALES_LISTAR(mo_icod_modelo);
                    foreach (var item in query)
                    {
                        lista.Add(new EPlantillaMateriales()
                        {
                            fited_icod_item_ficha_tecnica = item.fited_icod_item_ficha_tecnica,
                            mo_icod_modelo = item.mo_icod_modelo,
                            fited_iitem_ficha_tecnica = item.fited_iitem_ficha_tecnica,
                            fited_iarea = Convert.ToInt32(item.fited_iarea),
                            fited_iubicacion = Convert.ToInt32(item.fited_iubicacion),
                            fited_vdescripcion = item.fited_vdescripcion,
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            fited_ncantidad = Convert.ToDecimal(item.fited_ncantidad),
                            fited_flag_estado = item.fited_flag_estado,
                            strarea = item.strarea,
                            strubicacion = item.strubicacion,
                            strCodeProducto = item.strCodeProducto,
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
        public int insertarPlantillaMateriales(EPlantillaMateriales obj)
        {
            int? intIcod = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PLANTILLA_MATERIALES_INSERTAR(
                            ref intIcod,
                            obj.mo_icod_modelo,
                            obj.fited_iitem_ficha_tecnica,
                            obj.fited_iarea,
                            obj.fited_iubicacion,
                            obj.fited_vdescripcion,
                            obj.prdc_icod_producto,
                            obj.fited_ncantidad,
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
        public void ActualizarPlantillaMateriales(EPlantillaMateriales obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PLANTILLA_MATERIALES_MODIFICAR(
                        obj.fited_icod_item_ficha_tecnica,
                        obj.fited_iitem_ficha_tecnica,
                        obj.fited_iarea,
                        obj.fited_iubicacion,
                        obj.fited_vdescripcion,
                        obj.prdc_icod_producto,
                        obj.fited_ncantidad,
                        obj.intUsuario,
                        obj.vpc
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarPlantillaMateriales(EPlantillaMateriales obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PLANTILLA_MATERIALES_ELIMINAR(
                    obj.fited_icod_item_ficha_tecnica,
                    obj.intUsuario,
                    obj.vpc
                    );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Orden de Produccion
        public List<EPVTPersonal> ListarRegistroPersonal()
        {
            List<EPVTPersonal> lista = null;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<EPVTPersonal>();
                    var query = dc.SGE_PVT_PERSONAL_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EPVTPersonal()
                        {
                            perc_iid_personal = item.perc_iid_personal,
                            perc_vcod_personal = item.perc_vcod_personal,
                            perc_vnombres_apellidos = item.perc_vnombres_apellidos,
                            perc_vDNI = item.perc_vDNI,
                            perc_vdomicillo = item.perc_vdomicillo,
                            perc_vtelefono = item.perc_vtelefono,
                            perc_iarea_empresa = Convert.ToInt32(item.perc_iarea_empresa),
                            perc_isub_area_empresa = Convert.ToInt32(item.perc_isub_area_empresa),
                            perc_iarea_proceso = Convert.ToInt32(item.perc_iarea_proceso),
                            perc_icargo = Convert.ToInt32(item.perc_icargo),
                            perc_sfecha_nacimiento = Convert.ToDateTime(item.perc_sfecha_nacimiento),
                            perc_isexo = Convert.ToInt32(item.perc_isexo),
                            perc_irelacion_laboral = Convert.ToInt32(item.perc_irelacion_laboral),
                            perc_flag_estado = item.perc_flag_estado,
                            strarea_empresa = item.strarea_empresa,
                            strarea_proceso = item.strarea_proceso,
                            strcargo = item.strcargo,
                            strrelacion_laboral = item.strrelacion_laboral,
                            strsexo = item.strsexo,
                            perc_icod_analitica = Convert.ToInt32(item.perc_icod_analitica),
                            anac_iid_analitica = item.anad_iid_analitica,
                            strsubareaempresa = item.strsubareaempresa,
                            perc_isituacion = item.perc_isituacion,
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
        public int InsertarRegistroPersonal(EPVTPersonal obj)
        {
            int? intIcod = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {

                    dc.SGE_PVT_PERSONAL_INSERTAR(
                        ref intIcod,
                        obj.perc_vcod_personal,
                        obj.perc_vnombres_apellidos,
                        obj.perc_vDNI,
                        obj.perc_vdomicillo,
                        obj.perc_vtelefono,
                        obj.perc_iarea_empresa,
                        obj.perc_isub_area_empresa,
                        obj.perc_iarea_proceso,
                        obj.perc_icargo,
                        obj.perc_sfecha_nacimiento,
                        obj.perc_isexo,
                        obj.perc_irelacion_laboral,
                        obj.intUsuario,
                        obj.strPc,
                        obj.perc_isituacion
                         );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarRegistroPersonal(EPVTPersonal obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_PERSONAL_MODIFICAR(
                        obj.perc_iid_personal,
                        obj.perc_vcod_personal,
                        obj.perc_vnombres_apellidos,
                        obj.perc_vDNI,
                        obj.perc_vdomicillo,
                        obj.perc_vtelefono,
                        obj.perc_iarea_empresa,
                        obj.perc_isub_area_empresa,
                        obj.perc_iarea_proceso,
                        obj.perc_icargo,
                        obj.perc_sfecha_nacimiento,
                        obj.perc_isexo,
                        obj.perc_irelacion_laboral,
                        obj.intUsuario,
                        obj.strPc,
                        obj.perc_icod_analitica,
                        obj.perc_isituacion
                        );
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
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_PERSONAL_ELIMINAR(
                    obj.perc_iid_personal,
                    obj.intUsuario,
                    obj.strPc,
                    obj.perc_icod_analitica
                    );
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
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    lista = new List<ERequerimientoMaterial>();
                    var query = dc.SGE_REQUERIMIENTO_MATERIAL_LISTAR(Ejercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new ERequerimientoMaterial()
                        {
                            rqmac_iid_requerimiento_material = item.rqmac_iid_requerimiento_material,
                            rqmac_icod_requerimiento_material = item.rqmac_icod_requerimiento_material,
                            rqmac_sfecha_requerimiento_material = Convert.ToDateTime(item.rqmac_sfecha_requerimiento_material),
                            rqmac_iorden_produccion = item.rqmac_iorden_produccion,
                            rqmac_iproceso = Convert.ToInt32(item.rqmac_iproceso),
                            rqmac_ipedido = Convert.ToInt32(item.rqmac_ipedido),
                            rqmac_iitem_pedido = Convert.ToInt32(item.rqmac_iitem_pedido),
                            rqmac_ificha_tecnica = Convert.ToInt32(item.rqmac_ificha_tecnica),
                            rqmac_ioperario = Convert.ToInt32(item.rqmac_ioperario),
                            rqmac_imodelo = Convert.ToInt32(item.rqmac_imodelo),
                            DesModelo = item.DesModelo,
                            rqmac_icolor = Convert.ToInt32(item.rqmac_icolor),
                            rqmac_imarca = Convert.ToInt32(item.rqmac_imarca),
                            DesMarca = item.DesMarca,
                            rqmac_itipo = Convert.ToInt32(item.rqmac_itipo),
                            rqmac_iserie = Convert.ToInt32(item.rqmac_iserie),
                            rqmac_ilinea = Convert.ToInt32(item.rqmac_ilinea),
                            rqmac_talla1 = Convert.ToInt32(item.rqmac_talla1),
                            rqmac_talla2 = Convert.ToInt32(item.rqmac_talla2),
                            rqmac_talla3 = Convert.ToInt32(item.rqmac_talla3),
                            rqmac_talla4 = Convert.ToInt32(item.rqmac_talla4),
                            rqmac_talla5 = Convert.ToInt32(item.rqmac_talla5),
                            rqmac_talla6 = Convert.ToInt32(item.rqmac_talla6),
                            rqmac_talla7 = Convert.ToInt32(item.rqmac_talla7),
                            rqmac_talla8 = Convert.ToInt32(item.rqmac_talla8),
                            rqmac_talla9 = Convert.ToInt32(item.rqmac_talla9),
                            rqmac_talla10 = Convert.ToInt32(item.rqmac_talla10),
                            rqmac_cant_talla1 = Convert.ToInt32(item.rqmac_cant_talla1),
                            rqmac_cant_talla2 = Convert.ToInt32(item.rqmac_cant_talla2),
                            rqmac_cant_talla3 = Convert.ToInt32(item.rqmac_cant_talla3),
                            rqmac_cant_talla4 = Convert.ToInt32(item.rqmac_cant_talla4),
                            rqmac_cant_talla5 = Convert.ToInt32(item.rqmac_cant_talla5),
                            rqmac_cant_talla6 = Convert.ToInt32(item.rqmac_cant_talla6),
                            rqmac_cant_talla7 = Convert.ToInt32(item.rqmac_cant_talla7),
                            rqmac_cant_talla8 = Convert.ToInt32(item.rqmac_cant_talla8),
                            rqmac_cant_talla9 = Convert.ToInt32(item.rqmac_cant_talla9),
                            rqmac_cant_talla10 = Convert.ToInt32(item.rqmac_cant_talla10),
                            rqmac_icod_producto1 = item.rqmac_icod_producto1,
                            rqmac_icod_producto2 = item.rqmac_icod_producto2,
                            rqmac_icod_producto3 = item.rqmac_icod_producto3,
                            rqmac_icod_producto4 = item.rqmac_icod_producto4,
                            rqmac_icod_producto5 = item.rqmac_icod_producto5,
                            rqmac_icod_producto6 = item.rqmac_icod_producto6,
                            rqmac_icod_producto7 = item.rqmac_icod_producto7,
                            rqmac_icod_producto8 = item.rqmac_icod_producto8,
                            rqmac_icod_producto9 = item.rqmac_icod_producto9,
                            rqmac_icod_producto10 = item.rqmac_icod_producto10,
                            rqmac_sfecha_ing_kardex = item.rqmac_sfecha_ing_kardex,
                            rqmac_iid_situacion = Convert.ToInt32(item.rqmac_iid_situacion),
                            rqmac_ntotal = Convert.ToInt32(item.rqmac_ntotal),
                            rqmac_ndocenas = Convert.ToDecimal(item.rqmac_ndocenas),
                            rqmac_iid_almacen = Convert.ToInt32(item.rqmac_iid_almacen),
                            rqmac_vcodigo_producto = item.rqmac_vcodigo_producto,
                            rqmac_vdescripcion_producto = item.rqmac_vdescripcion_producto,
                            rqmac_flag_estado = item.rqmac_flag_estado,
                            rqmac_isituacion = Convert.ToInt32(item.rqmac_isituacion),
                            strsituacion = item.strsituacion,
                            strtipo = item.strtipo,
                            strcolor = item.strcolor,
                            resec_vdescripcion = item.resec_vdescripcion,
                            strpedido = item.strpedido,
                            stritempedido = Convert.ToInt32(item.stritempedido),
                            strfichatecnica = item.strfichatecnica,
                            strordenproduccion = item.strordenproducion,
                            stralmacen = item.stralmacen,
                            stroperario = item.stroperario,
                            strproceso = item.strproceso,
                            strlinea = item.strlinea,
                            situacion = item.situacion
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
        public int InsertarRequerimientoMaterial(ERequerimientoMaterial obj)
        {
            int? intIcod = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {

                    dc.SGE_REQUERIMIENTO_MATERIAL_INSERTAR(
                        ref intIcod,
                        obj.rqmac_icod_requerimiento_material,
                        Convert.ToDateTime(obj.rqmac_sfecha_requerimiento_material),
                        obj.rqmac_iorden_produccion,
                        obj.rqmac_iproceso,
                        obj.rqmac_ipedido,
                        obj.rqmac_iitem_pedido,
                        obj.rqmac_ificha_tecnica,
                        obj.rqmac_ioperario,
                        obj.rqmac_imodelo,
                        obj.rqmac_icolor,
                        obj.rqmac_imarca,
                        obj.rqmac_itipo,
                        obj.rqmac_iserie,
                        obj.rqmac_ilinea,
                        obj.rqmac_talla1,
                        obj.rqmac_talla2,
                        obj.rqmac_talla3,
                        obj.rqmac_talla4,
                        obj.rqmac_talla5,
                        obj.rqmac_talla6,
                        obj.rqmac_talla7,
                        obj.rqmac_talla8,
                        obj.rqmac_talla9,
                        obj.rqmac_talla10,
                        obj.rqmac_cant_talla1,
                        obj.rqmac_cant_talla2,
                        obj.rqmac_cant_talla3,
                        obj.rqmac_cant_talla4,
                        obj.rqmac_cant_talla5,
                        obj.rqmac_cant_talla6,
                        obj.rqmac_cant_talla7,
                        obj.rqmac_cant_talla8,
                        obj.rqmac_cant_talla9,
                        obj.rqmac_cant_talla10,
                        obj.rqmac_icod_producto1,
                        obj.rqmac_icod_producto2,
                        obj.rqmac_icod_producto3,
                        obj.rqmac_icod_producto4,
                        obj.rqmac_icod_producto5,
                        obj.rqmac_icod_producto6,
                        obj.rqmac_icod_producto7,
                        obj.rqmac_icod_producto8,
                        obj.rqmac_icod_producto9,
                        obj.rqmac_icod_producto10,
                        obj.rqmac_iid_almacen,
                        obj.rqmac_sfecha_ing_kardex,
                        obj.rqmac_iid_situacion,
                        obj.rqmac_ntotal,
                        obj.rqmac_ndocenas,
                        obj.rqmac_vcodigo_producto,
                        obj.rqmac_vdescripcion_producto,
                        obj.intUsuario,
                        obj.vpc,
                        obj.rqmac_isituacion
                         );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarRequerimientoMaterial(ERequerimientoMaterial obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTO_MATERIAL_MODIFICAR(
                        obj.rqmac_iid_requerimiento_material,
                        obj.rqmac_icod_requerimiento_material,
                        Convert.ToDateTime(obj.rqmac_sfecha_requerimiento_material),
                        obj.rqmac_iorden_produccion,
                        obj.rqmac_iproceso,
                        obj.rqmac_ipedido,
                        obj.rqmac_iitem_pedido,
                        obj.rqmac_ificha_tecnica,
                        obj.rqmac_ioperario,
                        obj.rqmac_imodelo,
                        obj.rqmac_icolor,
                        obj.rqmac_imarca,
                        obj.rqmac_itipo,
                        obj.rqmac_iserie,
                        obj.rqmac_ilinea,
                        obj.rqmac_talla1,
                        obj.rqmac_talla2,
                        obj.rqmac_talla3,
                        obj.rqmac_talla4,
                        obj.rqmac_talla5,
                        obj.rqmac_talla6,
                        obj.rqmac_talla7,
                        obj.rqmac_talla8,
                        obj.rqmac_talla9,
                        obj.rqmac_talla10,
                        obj.rqmac_cant_talla1,
                        obj.rqmac_cant_talla2,
                        obj.rqmac_cant_talla3,
                        obj.rqmac_cant_talla4,
                        obj.rqmac_cant_talla5,
                        obj.rqmac_cant_talla6,
                        obj.rqmac_cant_talla7,
                        obj.rqmac_cant_talla8,
                        obj.rqmac_cant_talla9,
                        obj.rqmac_cant_talla10,
                        obj.rqmac_icod_producto1,
                        obj.rqmac_icod_producto2,
                        obj.rqmac_icod_producto3,
                        obj.rqmac_icod_producto4,
                        obj.rqmac_icod_producto5,
                        obj.rqmac_icod_producto6,
                        obj.rqmac_icod_producto7,
                        obj.rqmac_icod_producto8,
                        obj.rqmac_icod_producto9,
                        obj.rqmac_icod_producto10,
                        obj.rqmac_iid_almacen,
                        obj.rqmac_sfecha_ing_kardex,
                        obj.rqmac_iid_situacion,
                        obj.rqmac_ntotal,
                        obj.rqmac_ndocenas,
                        obj.rqmac_vcodigo_producto,
                        obj.rqmac_vdescripcion_producto,
                        obj.intUsuario,
                        obj.vpc,
                        obj.rqmac_isituacion
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarRequerimientoMaterial(ERequerimientoMaterial obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTO_MATERIAL_ELIMINAR(
                    obj.rqmac_iid_requerimiento_material,
                    obj.intUsuario,
                    obj.vpc
                    );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Requerimiento Material Detalle
        public List<ERequerimientoMaterialDet> listarRequerimientoMaterialDetalle(int intOrdenPrduccion)
        {
            List<ERequerimientoMaterialDet> lista = new List<ERequerimientoMaterialDet>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_REQUERIMIENTO_MATERIAL_DET_LISTAR(intOrdenPrduccion);
                    foreach (var item in query)
                    {
                        lista.Add(new ERequerimientoMaterialDet()
                        {
                            rqmad_icod_item_requerimiento_material = item.rqmad_icod_item_requerimiento_material,
                            rqmac_iid_requerimiento_material = item.rqmac_iid_requerimiento_material,
                            rqmad_iubicacion = Convert.ToInt32(item.rqmad_iubicacion),
                            prdc_icod_producto = Convert.ToInt32(item.prdc_icod_producto),
                            rqmad_vmaterial = item.rqmad_vmateria,
                            rqmad_ncantidad_requerida = Convert.ToDecimal(item.rqmad_ncantidad_requerida),
                            rqmad_ncantidad_entregada = Convert.ToDecimal(item.rqmad_ncantidad_entregada),
                            rqmad_ncantidad_devuelta = Convert.ToDecimal(item.rqmad_ncantidad_devuelta),
                            orprd_imedida = Convert.ToInt32(item.rqmad_imedida),
                            rqmad_flag_estado = item.rqmad_flag_estado,
                            strCodeProducto = item.strCodeProducto,
                            strProducto = item.strProducto,
                            strmedida = item.strmedida,
                            strubicacion = item.strubicacion,
                            rqmad_kardc_icod_correlativo = Convert.ToInt32(item.rqmad_kardc_icod_correlativo),
                            rqmad_cantidad_saldo = Convert.ToDecimal(item.rqmad_ncantidad_requerida) - Convert.ToDecimal(item.rqmad_ncantidad_entregada),
                            icod_entrega_material = Convert.ToInt32(item.icod_entrega_material)
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
        public void insertarRequerimientoMaterialDetalle(ERequerimientoMaterialDet ob)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTO_MATERIAL_DET_INSERTAR(
                            ob.rqmac_iid_requerimiento_material,
                            ob.rqmad_iubicacion,
                            ob.prdc_icod_producto,
                            ob.rqmad_vmaterial,
                            ob.rqmad_ncantidad_requerida,
                            ob.orprd_imedida,
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
        public void modificarRequerimientoMaterialDetalle(ERequerimientoMaterialDet ob)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTO_MATERIAL_DET_MODIFICAR(
                            ob.rqmad_icod_item_requerimiento_material,
                            ob.rqmad_iubicacion,
                            ob.prdc_icod_producto,
                            ob.rqmad_vmaterial,
                            ob.rqmad_ncantidad_requerida,
                            ob.orprd_imedida,
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
        public void ActualizarEntregaMaterialDetalle(ERequerimientoMaterialDet ob)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTO_MATERIAL_DET_ACTUALIZAR_CE(
                            ob.rqmad_icod_item_requerimiento_material,
                            ob.rqmad_ncantidad_entregada,
                            ob.rqmad_kardc_icod_correlativo,
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
        public void ActualizarDevolucionMaterialDetalle(ERequerimientoMaterialDet ob)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTO_MATERIAL_DET_ACTUALIZAR_CD(
                            ob.rqmad_icod_item_requerimiento_material,
                            ob.rqmad_ncantidad_devuelta,
                            ob.rqmad_kardc_icod_correlativo,
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
        public void eliminarRequerimientoMaterialDetalle(ERequerimientoMaterialDet ob)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTO_MATERIAL_DET_ELIMINAR(
                            ob.rqmad_icod_item_requerimiento_material,
                            ob.intUsuario,
                            ob.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EEntregaMaterialDet> listarEntregaMaterialDetalle(int intOrdenPrduccion)
        {
            List<EEntregaMaterialDet> lista = new List<EEntregaMaterialDet>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_REQUERIMIENTO_MATERIAL_ENTREGAS_DET_LISTAR(intOrdenPrduccion);
                    foreach (var item in query)
                    {
                        lista.Add(new EEntregaMaterialDet()
                        {
                            rqmed_icod_item_requerimiento_material_entrega = item.rqmed_icod_item_requerimiento_material_entrega,
                            rqmad_icod_item_requerimiento_material = item.rqmad_icod_item_requerimiento_material,
                            rqmac_iid_requerimiento_material = item.rqmac_iid_requerimiento_material,
                            rqmed_sfecha_entrega = Convert.ToDateTime(item.rqmed_sfecha_entrega),
                            rqmed_ncantidad_entregada = Convert.ToDecimal(item.rqmed_ncantidad_entregada),
                            rqmed_flag_estado = item.rqmed_flag_estado,
                            rqmed_kardc_icod_correlativo = Convert.ToInt32(item.rqmed_kardc_icod_correlativo),
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
        public void insertarEntregaMaterialDetalle(EEntregaMaterialDet ob)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTO_MATERIAL_ENTREGAS_DET_INSERTAR(
                            ob.rqmad_icod_item_requerimiento_material,
                            ob.rqmac_iid_requerimiento_material,
                            ob.rqmed_sfecha_entrega,
                            ob.rqmed_ncantidad_entregada,
                            ob.rqmed_kardc_icod_correlativo,
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
        public void eliminarEntregaMaterialDetalle(EEntregaMaterialDet ob)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_REQUERIMIENTO_MATERIAL_ENTREGAS_DET_ELIMINAR(
                            ob.rqmed_icod_item_requerimiento_material_entrega,
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
        public void ActualizarPVTOrdenProduccionKardex(EOrdenProduccion obj)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_ORDEN_PRODUCCIOM_ACTUALIZAR_KARDEX(
                        obj.orprc_iid_orden_produccion,
                        Convert.ToInt64(obj.orprc_iid_kardex1),
                        Convert.ToInt64(obj.orprc_iid_kardex2),
                        Convert.ToInt64(obj.orprc_iid_kardex3),
                        Convert.ToInt64(obj.orprc_iid_kardex4),
                        Convert.ToInt64(obj.orprc_iid_kardex5),
                        Convert.ToInt64(obj.orprc_iid_kardex6),
                        Convert.ToInt64(obj.orprc_iid_kardex7),
                        Convert.ToInt64(obj.orprc_iid_kardex8),
                        Convert.ToInt64(obj.orprc_iid_kardex9),
                        Convert.ToInt64(obj.orprc_iid_kardex10),
                        obj.orprc_iid_almacen,
                        obj.orprc_sfecha_ing_kardex,
                        obj.orprc_iid_situacion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EProdKardex> listarStockConsolidadoxAlmacen(int intEjercicio)
        {
            List<EProdKardex> lista = new List<EProdKardex>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_STOCK_CONSOLIDADO_X_ALMACEN_MERCADERIA(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdKardex()
                        {
                            iid_producto = Convert.ToInt32(item.stocc_iid_producto),
                            strCodProducto = item.strCodProducto,
                            strProducto = item.strProducto,
                            strUnidadMedida = item.strUnidadMedida,
                            cantidad_saldo_prod = Convert.ToDecimal(item.dblStock),
                            iid_almacen = Convert.ToInt32(item.stocc_iid_almacen),
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
        public int DeleteCierreAnualAlmacenes(int intEjercicio)
        {
            int? intIcod = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGE_SALDO_INICIAL_POR_CIERRE_DELETE_MERCADERIA(
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
                    using (var cmd = new SqlCommand("INSERCION_MASIVA_KARDEX_MERCADERIA", conn))
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
        public void stockActualizarConKardex(int periodo)
        {
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    var query = dc.SGEA_STOCK_ACTUALIZAR_CON_KADEX_MERCADERIA(periodo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int Obtener_Kardex_Max_Correlativo()
        {
            int? intIcod = 0;
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {
                    dc.SGEA_KARDEX_MAX_CORRELATIVO_MERCADERIA(
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

        public List<EProdKardex> listarAlmacenSaldoInicial(int intEjercicio)
        {
            List<EProdKardex> lista = new List<EProdKardex>();
            try
            {
                using (CentralDataContext dc = new CentralDataContext(Helper.conexion()))
                {

                    var query = dc.SGEA_ALMACEN_SALDO_INICIAL_LISTAR_MERCADERIA(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EProdKardex()
                        {
                            kardc_iid_correlativo = Convert.ToInt32(item.kardc_iid_correlativo),
                            kardx_sfecha = Convert.ToDateTime(item.kardc_sfecha_movimiento),
                            iid_almacen = Convert.ToInt32(item.kardc_iid_almacen),
                            iid_producto = Convert.ToInt32(item.kardc_iid_producto),
                            iid_tipo_doc = Convert.ToInt32(item.kardc_iid_tipo_doc),
                            NroDocumento = item.kardc_inumero_doc,
                            Cantidad = Convert.ToDecimal(item.kardc_icantidad_prod),
                            strAlmacen = item.strDesAlmacen,
                            strCodProducto = item.strCodProducto,
                            strProducto = item.strDesProducto,
                            strUnidadMedida = item.strUnidadMedida,
                            //strTipoDoc = item.strTipoDoc
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
        public void InsertMasivo(DataTable tbl, string procedure)
        {
            try
            {
                using (var conn = new SqlConnection(Helper.conexion()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(procedure, conn))
                    {
                        cmd.CommandTimeout = 999999999;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@VCC", tbl);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
