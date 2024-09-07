using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class FrmNotaIngresoRegistro : DevExpress.XtraEditors.XtraForm
    {
        public List<EProdNotaIngreso> oDetail;//notas de ingreso
        public List<EProdNotaIngresoDetalle> mlistdet = new List<EProdNotaIngresoDetalle>();//detalle de nota de ingreso
        public List<EProdNotaIngresoDetalle> mlistdetEliminados = new List<EProdNotaIngresoDetalle>();//lista de eliminados
        public int ningc_icod_nota_ingreso;//icod_nota_ingreso
        public int Correlativo;
        public bool flag_precio;

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public delegate void DelegadoMensaje(int icod);
        public event DelegadoMensaje MiEvento;

        public FrmNotaIngresoRegistro()
        {
            InitializeComponent();
        }
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            lkpmotivo.Enabled = !Enabled;
            dtmfecha.Enabled = !Enabled;
            dtmfechadocumento.Enabled = !Enabled;
            txtnrodocumento.Enabled = !Enabled;
            txtreferencia.Enabled = !Enabled;
            txtobservacion.Enabled = !Enabled;
        }
        private void clearControl()
        {
            txtnroNota.Text = "";
            dtmfecha.EditValue = DateTime.Now;
            dtmfechadocumento.EditValue = DateTime.Now;

        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
        }
        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }
        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }
        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void GenerarCodigo()
        {
            int i = 0;
            var lista = oDetail.Where(ob => ob.ningc_iid_almacen == Convert.ToInt32(btncodigoalmacen.Tag)).ToList();
            if (lista.Count > 0)
                i = lista.Max(ob => Convert.ToInt32(ob.ningc_inumero_nota_ingreso));
            txtnroNota.Text = String.Format("{0:000000}", i + 1).ToString();
        }
        private void btncodigoalmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (FrmListarAlmacen Almacen = new FrmListarAlmacen())
            {
                Almacen.puvec_icod_punto_venta = Convert.ToInt32(lkpPuntoVenta.EditValue);
                if (Almacen.ShowDialog() == DialogResult.OK)
                {
                    btncodigoalmacen.Tag = Almacen._Be.id_almacen;
                    btncodigoalmacen.Text = Almacen._Be.idf_almacen;
                    txtalmacen.Text = Almacen._Be.descripcion;
                    GenerarCodigo();
                }
            }
        }


        private void Cargar()
        {
            //EModulo obj = new EModulo { moduc_icod_modulo = 1 };
            BSControls.LoaderLook(lkptipodocumento, new BAdministracionSistema().listarTipoDocumentoPorModulo(2), "tdocc_vabreviatura_tipo_doc", "tdocc_icod_tipo_doc", true);
            //EProdTablaRegistro obe = new EProdTablaRegistro() { iid_tipo_tabla = 36 };
            BSControls.LoaderLook(lkpmotivo, new BGeneral().listarTablaRegistro(34), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpPuntoVenta, new BCentral().ListarPuntoVenta(), "puvec_vdescripcion", "puvec_icod_punto_venta", true);
            lkptipodocumento.EditValue = 36;
            //cargar detalle
            mlistdet = new BCentral().ListarProdNotaIngresoDetalle(ningc_icod_nota_ingreso);
            grcNotaDetalle.DataSource = mlistdet;
            decimal? TotalItem = mlistdet.Sum(ob => ob.ningcd_monto_total);
            decimal? CantidadTotal = mlistdet.Sum(ob => ob.pr_ncant_total_producto);

        }
        private void FrmNotaIngresoRegistro_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }
        private void SetSave()
        {
            BCentral obl = new BCentral();
            EProdNotaIngreso obj = new EProdNotaIngreso();
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (btncodigoalmacen.Tag == null)
                {
                    oBase = btncodigoalmacen;
                    throw new ArgumentException("Ingresar ALmacén");
                }
                if (lkpmotivo.EditValue == null)
                {
                    oBase = lkpmotivo;
                    throw new ArgumentException("Ingresar Motivo");
                }
                if (lkptipodocumento.EditValue == null)
                {
                    oBase = lkptipodocumento;
                    throw new ArgumentException("Ingresar Documento");
                }

                obj.ningc_icod_nota_ingreso = ningc_icod_nota_ingreso;
                obj.ningc_inumero_nota_ingreso = Convert.ToInt32(txtnroNota.Text);
                obj.ningc_sfecha_nota_ingreso = Convert.ToDateTime(dtmfecha.EditValue);
                obj.tablc_iid_motivo = Convert.ToInt32(lkpmotivo.EditValue);
                obj.ningc_iid_almacen = Convert.ToInt32(btncodigoalmacen.Tag);
                obj.ningc_iid_tipo_doc = Convert.ToInt32(lkptipodocumento.EditValue);
                obj.puvec_icod_punto_venta = Convert.ToInt32(lkpPuntoVenta.EditValue);
                obj.ningc_inumero_doc = txtnrodocumento.Text;
                obj.ningc_vreferencia = txtreferencia.Text;
                obj.ningc_vobservaciones = txtobservacion.Text;
                obj.puvec_icod_punto_venta = Convert.ToInt32(lkpPuntoVenta.EditValue);
                obj.ningc_sfecha_compra = Convert.ToDateTime(dtmfechadocumento.EditValue);
                obj.tablc_iid_tipo_moneda = 1;
                obj.orpec_iid_orden_pedido = Convert.ToInt32(btnPedido.Tag);
                obj.numero_pedido = btnPedido.Text;
                obj.orped_icod_item_orden_pedido = Convert.ToInt32(btnItemPedido.Tag);
                obj.numero_item_pedido = string.IsNullOrEmpty(btnItemPedido.Text) ? 0 : Convert.ToInt32(btnItemPedido.Text);


                obj.intUsuario = Valores.intUsuario;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obj.ningc_icod_nota_ingreso = obl.InsertarProdNotaIngreso(obj, mlistdet);
                }
                else
                {
                    obj.ningc_icod_nota_ingreso = ningc_icod_nota_ingreso;
                    obl.ActualizarProdNotaIngresoo(obj, mlistdet, mlistdetEliminados);
                }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {

                if (Flag)
                {
                    this.MiEvento(obj.ningc_icod_nota_ingreso);
                    this.Close();
                }
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmNotaIngresoRegistroDet frmdetalle = new FrmNotaIngresoRegistroDet();
            frmdetalle.icod_almacen = Convert.ToInt32(btncodigoalmacen.Tag);
            frmdetalle.icod_motivo = Convert.ToInt32(lkpmotivo.EditValue);
            frmdetalle.txtitem.Text = (mlistdet.Count + 1).ToString();
            frmdetalle.btnModificar.Enabled = false;
            frmdetalle.btnGuardar.Enabled = false;
            if (frmdetalle.ShowDialog() == DialogResult.OK)
            {
                EProdNotaIngresoDetalle _obe = new EProdNotaIngresoDetalle();
                _obe.ningc_iid_almacen = frmdetalle._BE.ningc_iid_almacen;
                _obe.ningcd_num_item = frmdetalle._BE.ningcd_num_item;
                _obe.tablc_iid_motivo = frmdetalle._BE.tablc_iid_motivo;
                _obe.pr_icod_producto = frmdetalle._BE.pr_icod_producto;
                _obe.pr_vcodigo_externo = frmdetalle._BE.pr_vcodigo_externo;
                _obe.pr_vdescripcion_producto = frmdetalle._BE.pr_vdescripcion_producto;
                _obe.ningcd_rango_talla = frmdetalle._BE.ningcd_rango_talla;
                _obe.ningcd_iid_moneda = frmdetalle._BE.ningcd_iid_moneda;
                _obe.pr_ncant_total_producto = frmdetalle._BE.pr_ncant_total_producto;
                _obe.ningcd_monto_unit = frmdetalle._BE.ningcd_monto_unit;
                _obe.ningcd_monto_total = frmdetalle._BE.ningcd_monto_total;
                _obe.ningcd_talla1 = frmdetalle._BE.ningcd_talla1;
                _obe.ningcd_talla2 = frmdetalle._BE.ningcd_talla2;
                _obe.ningcd_talla3 = frmdetalle._BE.ningcd_talla3;
                _obe.ningcd_talla4 = frmdetalle._BE.ningcd_talla4;
                _obe.ningcd_talla5 = frmdetalle._BE.ningcd_talla5;
                _obe.ningcd_talla6 = frmdetalle._BE.ningcd_talla6;
                _obe.ningcd_talla7 = frmdetalle._BE.ningcd_talla7;
                _obe.ningcd_talla8 = frmdetalle._BE.ningcd_talla8;
                _obe.ningcd_talla9 = frmdetalle._BE.ningcd_talla9;
                _obe.ningcd_talla10 = frmdetalle._BE.ningcd_talla10;
                _obe.ningcd_cant_talla1 = frmdetalle._BE.ningcd_cant_talla1;
                _obe.ningcd_cant_talla2 = frmdetalle._BE.ningcd_cant_talla2;
                _obe.ningcd_cant_talla3 = frmdetalle._BE.ningcd_cant_talla3;
                _obe.ningcd_cant_talla4 = frmdetalle._BE.ningcd_cant_talla4;
                _obe.ningcd_cant_talla5 = frmdetalle._BE.ningcd_cant_talla5;
                _obe.ningcd_cant_talla6 = frmdetalle._BE.ningcd_cant_talla6;
                _obe.ningcd_cant_talla7 = frmdetalle._BE.ningcd_cant_talla7;
                _obe.ningcd_cant_talla8 = frmdetalle._BE.ningcd_cant_talla8;
                _obe.ningcd_cant_talla9 = frmdetalle._BE.ningcd_cant_talla9;
                _obe.ningcd_cant_talla10 = frmdetalle._BE.ningcd_cant_talla10;
                _obe.operacion = 1;
                mlistdet.Add(_obe);
                CalcularCant();
                grcNotaDetalle.RefreshDataSource();
            }
        }
        public void CalcularCant()
        {
            txtcantidadTotal.Text = (mlistdet.Sum(ob => ob.pr_ncant_total_producto)).ToString();
        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EProdNotaIngresoDetalle _obe = (EProdNotaIngresoDetalle)grvNotaIngresoDet.GetRow(grvNotaIngresoDet.FocusedRowHandle);
            if (_obe != null)
            {
                FrmNotaIngresoRegistroDet frmdetalle = new FrmNotaIngresoRegistroDet();
                string[] grupo = _obe.ningcd_rango_talla.Split('/');
                frmdetalle.txtitem.Text = _obe.ningcd_num_item.ToString();
                frmdetalle.btncodigoProducto.Tag = _obe.pr_icod_producto;
                frmdetalle.btncodigoProducto.Text = _obe.pr_vcodigo_externo;
                frmdetalle.txtdescripcion.Text = _obe.pr_vdescripcion_producto;
                frmdetalle.txtcantidaddetalle.Text = _obe.pr_ncant_total_producto.ToString();
                frmdetalle.txtgrupo.Text = grupo[0].ToString();
                frmdetalle.txtgrupo2.Text = grupo[1].ToString();
                frmdetalle.txtpreciounit.Text = _obe.ningcd_monto_unit.ToString();
                frmdetalle.txttotalitem.Text = _obe.ningcd_monto_total.ToString();
                frmdetalle.txttalla1.Text = string.Format("{0:00}", _obe.ningcd_talla1);
                frmdetalle.txttalla2.Text = string.Format("{0:00}", _obe.ningcd_talla2);
                frmdetalle.txttalla3.Text = string.Format("{0:00}", _obe.ningcd_talla3);
                frmdetalle.txttalla4.Text = string.Format("{0:00}", _obe.ningcd_talla4);
                frmdetalle.txttalla5.Text = string.Format("{0:00}", _obe.ningcd_talla5);
                frmdetalle.txttalla6.Text = string.Format("{0:00}", _obe.ningcd_talla6);
                frmdetalle.txttalla7.Text = string.Format("{0:00}", _obe.ningcd_talla7);
                frmdetalle.txttalla8.Text = string.Format("{0:00}", _obe.ningcd_talla8);
                frmdetalle.txttalla9.Text = string.Format("{0:00}", _obe.ningcd_talla9);
                frmdetalle.txttalla10.Text = string.Format("{0:00}", _obe.ningcd_talla10);
                frmdetalle.txtcantidad1.Text = _obe.ningcd_cant_talla1.ToString();
                frmdetalle.txtcantidad2.Text = _obe.ningcd_cant_talla2.ToString();
                frmdetalle.txtcantidad3.Text = _obe.ningcd_cant_talla3.ToString();
                frmdetalle.txtcantidad4.Text = _obe.ningcd_cant_talla4.ToString();
                frmdetalle.txtcantidad5.Text = _obe.ningcd_cant_talla5.ToString();
                frmdetalle.txtcantidad6.Text = _obe.ningcd_cant_talla6.ToString();
                frmdetalle.txtcantidad7.Text = _obe.ningcd_cant_talla7.ToString();
                frmdetalle.txtcantidad8.Text = _obe.ningcd_cant_talla8.ToString();
                frmdetalle.txtcantidad9.Text = _obe.ningcd_cant_talla9.ToString();
                frmdetalle.txtcantidad10.Text = _obe.ningcd_cant_talla10.ToString();
                frmdetalle.btncodigoProducto.Enabled = false;
                frmdetalle.txtgrupo.Enabled = false;
                frmdetalle.txtgrupo2.Enabled = false;
                frmdetalle.btngenerar.Enabled = false;
                frmdetalle.btnGuardar.Enabled = false;
                frmdetalle.modificarDeta();
                if (frmdetalle.ShowDialog() == DialogResult.OK)
                {
                    _obe.pr_ncant_total_producto = frmdetalle._BE.pr_ncant_total_producto;
                    _obe.ningcd_monto_unit = frmdetalle._BE.ningcd_monto_unit;
                    _obe.ningcd_monto_total = frmdetalle._BE.ningcd_monto_total;
                    _obe.ningcd_talla1 = frmdetalle._BE.ningcd_talla1;
                    _obe.ningcd_talla2 = frmdetalle._BE.ningcd_talla2;
                    _obe.ningcd_talla3 = frmdetalle._BE.ningcd_talla3;
                    _obe.ningcd_talla4 = frmdetalle._BE.ningcd_talla4;
                    _obe.ningcd_talla5 = frmdetalle._BE.ningcd_talla5;
                    _obe.ningcd_talla6 = frmdetalle._BE.ningcd_talla6;
                    _obe.ningcd_talla7 = frmdetalle._BE.ningcd_talla7;
                    _obe.ningcd_talla8 = frmdetalle._BE.ningcd_talla8;
                    _obe.ningcd_talla9 = frmdetalle._BE.ningcd_talla9;
                    _obe.ningcd_talla10 = frmdetalle._BE.ningcd_talla10;
                    _obe.ningcd_cant_talla1 = frmdetalle._BE.ningcd_cant_talla1;
                    _obe.ningcd_cant_talla2 = frmdetalle._BE.ningcd_cant_talla2;
                    _obe.ningcd_cant_talla3 = frmdetalle._BE.ningcd_cant_talla3;
                    _obe.ningcd_cant_talla4 = frmdetalle._BE.ningcd_cant_talla4;
                    _obe.ningcd_cant_talla5 = frmdetalle._BE.ningcd_cant_talla5;
                    _obe.ningcd_cant_talla6 = frmdetalle._BE.ningcd_cant_talla6;
                    _obe.ningcd_cant_talla7 = frmdetalle._BE.ningcd_cant_talla7;
                    _obe.ningcd_cant_talla8 = frmdetalle._BE.ningcd_cant_talla8;
                    _obe.ningcd_cant_talla9 = frmdetalle._BE.ningcd_cant_talla9;
                    _obe.ningcd_cant_talla10 = frmdetalle._BE.ningcd_cant_talla10;
                    if (_obe.ningcd_icod_nota_ingreso == 0)
                        _obe.operacion = 1;
                    else
                    {
                        if (_obe.operacion != 1)
                        {
                            _obe.operacion = 2;
                        }
                    }
                    CalcularCant();
                    grcNotaDetalle.RefreshDataSource();

                }
            }
        }



        private void generarCodigosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<EProdIntervalo> mlistadeSeries = new List<EProdIntervalo>();




            using (frmImportarProductosNotaIngreso frmCliente = new frmImportarProductosNotaIngreso())
            {
                if (frmCliente.ShowDialog() == DialogResult.OK)
                {
                    var listaInventarioDetalle = frmCliente.ListaImportacion;

                    foreach (EProdInventarioFisicoDetalle obj in listaInventarioDetalle)
                    {
                        ArrayList tallasAculadas = new ArrayList();
                        foreach (EProdInventarioFisicoDetalle objAux in listaInventarioDetalle)
                        {
                            if (obj.pr_vcodigo_externo.Substring(0, 13).Trim() == objAux.pr_vcodigo_externo.Substring(0, 13).Trim() && objAux.flag == false)
                            {
                                tallasAculadas.Add(Convert.ToInt32(objAux.pr_vcodigo_externo.Substring(13, 2)));
                                objAux.flag = true;
                            }
                        }
                        object[] tallasAcumuladasArray = tallasAculadas.ToArray();
                        if (tallasAcumuladasArray.LongCount() > 0)
                        {
                            int TallaMayor = Convert.ToInt32(tallasAcumuladasArray[0]);
                            int TallaMenor = Convert.ToInt32(tallasAcumuladasArray[0]);
                            for (int i = 0; i < tallasAcumuladasArray.Length; i++)
                            {
                                if (Convert.ToInt32(tallasAcumuladasArray[i]) > TallaMayor)
                                {
                                    TallaMayor = Convert.ToInt32(tallasAcumuladasArray[i]);
                                }
                                if (Convert.ToInt32(tallasAcumuladasArray[i]) < TallaMenor)
                                {
                                    TallaMenor = Convert.ToInt32(tallasAcumuladasArray[i]);
                                }
                            }
                            EProdIntervalo objEintervalo = new EProdIntervalo();
                            objEintervalo.serie1 = TallaMenor;
                            objEintervalo.serie2 = TallaMayor;
                            objEintervalo.codigoExterno = obj.pr_vcodigo_externo.Substring(0, 13);
                            objEintervalo.Descripcion = obj.pr_vdescripcion_producto.Substring(0, obj.pr_vdescripcion_producto.Length - 2);
                            mlistadeSeries.Add(objEintervalo);
                        }
                    }
                    GuardarDetalleConCodigosImportados(listaInventarioDetalle, mlistadeSeries);
                }
            }
            CalcularCant();
        }

        public List<EProdProducto> oProducto;
        private void GuardarDetalleConCodigosImportados(List<EProdInventarioFisicoDetalle> mlitaProductos, List<EProdIntervalo> mlistaIntervalos)
        {


            int i = 1;
            foreach (EProdIntervalo objIntervalo in mlistaIntervalos)
            {
                ArrayList AcumuladoKardex = new ArrayList();
                ArrayList Tallas = new ArrayList();
                ArrayList cantidades = new ArrayList();

                foreach (EProdInventarioFisicoDetalle objProduc in mlitaProductos)
                {
                    if (objProduc.pr_vcodigo_externo.Substring(0, 13).Trim() == objIntervalo.codigoExterno.Trim())
                    {
                        string codigoexterno = objProduc.pr_vcodigo_externo;
                        oProducto = new BCentral().VerificarProdProducto(codigoexterno);
                        if (oProducto.Count > 0)
                        {
                            Tallas.Add(objProduc.pr_vcodigo_externo.Substring(13, 2));
                            cantidades.Add(objProduc.infid_ncant_fisica);
                        }
                    }
                }

                object[] TallasArray = Tallas.ToArray();
                object[] TallasOri = new object[10];
                for (int u = 0; u < 10; u++)
                {
                    if (TallasArray.Length > u)
                    {
                        TallasOri[u] = TallasArray[u];
                    }
                    else
                    {
                        TallasOri[u] = 0;
                    }
                }
                object[] CantidadesArray = cantidades.ToArray();
                object[] CantidadesOri = new object[10];
                for (int t = 0; t < 10; t++)
                {
                    if (CantidadesArray.Length > t)
                    {
                        CantidadesOri[t] = CantidadesArray[t];
                    }
                    else
                    {
                        CantidadesOri[t] = 0;
                    }
                }
                EProdNotaIngresoDetalle objd = new EProdNotaIngresoDetalle();
                objd.ningc_icod_nota_ingreso = 0;
                //objd.salgc_icod_nota_salida = salgc_icod_nota_salida;
                objd.ningc_iid_almacen = Convert.ToInt32(btncodigoalmacen.Tag);
                objd.ningcd_num_item = i;
                objd.tablc_iid_motivo = Convert.ToInt32(lkpmotivo.EditValue);
                objd.pr_vcodigo_externo = objIntervalo.codigoExterno;
                objd.ningcd_monto_unit = 0;
                objd.ningcd_monto_total = 0;
                objd.pr_vdescripcion_producto = objIntervalo.Descripcion;
                objd.pr_ncant_total_producto = mlitaProductos.Where(o => o.pr_vcodigo_externo.Substring(0, 13).Trim() == objIntervalo.codigoExterno.Trim()).Sum(ob => ob.infid_ncant_fisica);
                objd.ningcd_rango_talla = objIntervalo.serie1 + "/" + objIntervalo.serie2;
                objd.ningcd_iid_moneda = 2;
                objd.ningcd_talla1 = Convert.ToInt32(TallasOri[0]);
                objd.ningcd_talla2 = Convert.ToInt32(TallasOri[1]);
                objd.ningcd_talla3 = Convert.ToInt32(TallasOri[2]);
                objd.ningcd_talla4 = Convert.ToInt32(TallasOri[3]);
                objd.ningcd_talla5 = Convert.ToInt32(TallasOri[4]);
                objd.ningcd_talla6 = Convert.ToInt32(TallasOri[5]);
                objd.ningcd_talla7 = Convert.ToInt32(TallasOri[6]);
                objd.ningcd_talla8 = Convert.ToInt32(TallasOri[7]);
                objd.ningcd_talla9 = Convert.ToInt32(TallasOri[8]);
                objd.ningcd_talla10 = Convert.ToInt32(TallasOri[9]);
                objd.ningcd_cant_talla1 = Convert.ToInt32((CantidadesOri[0]));
                objd.ningcd_cant_talla2 = Convert.ToInt32((CantidadesOri[1]));
                objd.ningcd_cant_talla3 = Convert.ToInt32((CantidadesOri[2]));
                objd.ningcd_cant_talla4 = Convert.ToInt32((CantidadesOri[3]));
                objd.ningcd_cant_talla5 = Convert.ToInt32((CantidadesOri[4]));
                objd.ningcd_cant_talla6 = Convert.ToInt32((CantidadesOri[5]));
                objd.ningcd_cant_talla7 = Convert.ToInt32((CantidadesOri[6]));
                objd.ningcd_cant_talla8 = Convert.ToInt32((CantidadesOri[7]));
                objd.ningcd_cant_talla9 = Convert.ToInt32((CantidadesOri[8]));
                objd.ningcd_cant_talla10 = Convert.ToInt32((CantidadesOri[9]));
                objd.intUsuario = Valores.intUsuario;
                objd.ningcd_iactivo = 1;
                objd.operacion = 1;
                mlistdet.Add(objd);
                i++;
                //obld.InsertarNotaSalidaDetalle(objd);
            }
            grcNotaDetalle.DataSource = mlistdet;
            grcNotaDetalle.RefreshDataSource();
        }

        public class EProdIntervalo
        {
            public int serie1 { get; set; }
            public int serie2 { get; set; }
            public string codigoExterno { get; set; }
            public string Descripcion { get; set; }

        }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlistdet.Count > 0)
            {
                if (XtraMessageBox.Show("Esta seguro de eliminar", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    Delete();
                }
            }
            else
                XtraMessageBox.Show("No hay registro por eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void Delete()
        {
            EProdNotaIngresoDetalle obj = (EProdNotaIngresoDetalle)grvNotaIngresoDet.GetRow(grvNotaIngresoDet.FocusedRowHandle);
            if (obj.operacion == 1)
            {
                mlistdet.Remove(obj);
                grvNotaIngresoDet.RefreshData();
                grvNotaIngresoDet.MovePrev();
            }
            else
            {
                //creo listado de eliminados para mandarlo a la BD para actualizar su estado
                if (mlistdet.Count > 0)
                {
                    obj.operacion = 3;
                    mlistdetEliminados.Add(obj);
                    mlistdet.Remove(obj);
                    grvNotaIngresoDet.RefreshData();
                    grvNotaIngresoDet.MovePrev();
                }

            }
        }

        private void mnuAlmacen_Opening(object sender, CancelEventArgs e)
        {
            generarCodigosToolStripMenuItem.Enabled = !mlistdet.Any();
        }

        private void genericoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<EProdKardex> listaKardex = new List<EProdKardex>();
            Dictionary<string, string> importedProducts = new Dictionary<string, string>();
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = dialog.FileName;
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split(';');
                        importedProducts.Add(values[0], values[1]);
                    }
                    int index = 0;
                    foreach (var item in importedProducts)
                    {
                        var product = new BAlmacen().ProductoPvtGetByCode(item.Key);

                        EProdKardex objk = new EProdKardex();
                        objk.kardc_ianio = Parametros.intEjercicio;
                        objk.kardx_sfecha = Convert.ToDateTime(dtmfecha.DateTime);
                        objk.iid_almacen = Convert.ToInt32(btncodigoalmacen.Tag);
                        objk.iid_producto = product.pr_icod_producto;
                        objk.puvec_icod_punto_venta = Convert.ToInt32(lkpPuntoVenta.EditValue);
                        objk.Cantidad = Convert.ToDecimal(item.Value);
                        objk.NroNota = Convert.ToInt32(txtnroNota.Text);
                        objk.iid_tipo_doc = Convert.ToInt32(lkptipodocumento.EditValue);
                        objk.NroDocumento = txtnrodocumento.Text;
                        objk.movimiento = 1;
                        objk.Motivo = Convert.ToInt32(lkpmotivo.EditValue);
                        objk.Beneficiario = txtreferencia.Text;
                        objk.Observaciones = txtobservacion.Text;
                        objk.intUsuario = Valores.intUsuario;
                        objk.Item = ++index;
                        objk.stocc_codigo_producto = item.Key;
                        objk.operacion = 1;
                        listaKardex.Add(objk);

                    }
                    FrmCommonForm frm = new FrmCommonForm();
                    frm.listaKardex = listaKardex;
                    frm.Show();
                }
            }
        }

        private void btnPedido_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            using (ListarOrdenPedido frm = new ListarOrdenPedido())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnPedido.Tag = frm._Be.orpec_iid_orden_pedido;
                    btnPedido.Text = frm._Be.orpec_icod_orden_pedido + " - " + frm._Be.cliec_vnombre_cliente;
                }
            }
        }

        private void btnItemPedido_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            using (ListarOrdenPedidoDetalle frm = new ListarOrdenPedidoDetalle())
            {
                frm.icod = Convert.ToInt32(btnPedido.Tag);
                frm.validarFichaTec = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnItemPedido.Tag = frm._Be.orped_icod_item_orden_pedido;
                    btnItemPedido.Text = frm._Be.orped_iitem_orden_pedido.ToString();
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            btnPedido.Tag = 0;
            btnPedido.Text = string.Empty;
            btnItemPedido.Tag = 0;
            btnItemPedido.Text = string.Empty;
        }
    }
}