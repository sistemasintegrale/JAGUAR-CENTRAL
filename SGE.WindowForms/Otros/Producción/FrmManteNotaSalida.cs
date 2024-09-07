using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Administracion_del_Sistema.Listados;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class FrmManteNotaSalida : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteNotaSalida));
        public delegate void DelegadoMensaje(int icod);
        public event DelegadoMensaje MiEvento;
        MyKeyPress myKeyPressHandler = new MyKeyPress();
        public int Correlative;
        //public List<EProdNotaSalida> lstCabecerasNI = new List<EProdNotaSalida>();
        public List<EProdNotaSalida> oDetail;
        public List<EProdNotaSalidaDetalle> oDetailNotaSalida = new List<EProdNotaSalidaDetalle>();
        public List<EProdNotaSalidaDetalle> oDetailNotaSalidaEliminado = new List<EProdNotaSalidaDetalle>();

        private BCentral oblKardex = new BCentral();
        int Codigo = 0;

        public int salgc_icod_nota_salida;

        public int indicador;//0:crear nuevo. 1:modificar

        public FrmManteNotaSalida()
        {
            List<EProdNotaSalidaDetalle> oDetailNotaSalida = new List<EProdNotaSalidaDetalle>();
            InitializeComponent();
            gridControl1.DataSource = oDetailNotaSalida;
        }
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        private BCentral Obl;
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }
        public void CargarDetalle(int Code)
        {
            oDetailNotaSalida = new BCentral().ListarProdNotaSalidaDetalle(Code);
            gridControl1.DataSource = oDetailNotaSalida;
        }

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            lkpmotivo.Enabled = !Enabled;
            dtmfecha.Enabled = !Enabled;
            txtnrodocumento.Enabled = !Enabled;

            txtobservacion.Enabled = !Enabled;
        }

        private void clearControl()
        {
            txtnroNota.Text = "";
            bteTipoDoc.Tag = 82;
            dtmfecha.EditValue = DateTime.Now;

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
        private void FrmManteNotaSalida_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void btncodigoalmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit texto = (ButtonEdit)sender;
            if (texto.Name == "btncodigoalmacen")
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
                    getNroNS();
                }
            }

        }
        private void getNroNS()
        {
            try
            {
                if (oDetail.Where(x =>
                    x.salgc_iid_almacen == Convert.ToInt32(btncodigoalmacen.Tag)).ToList().Count > 0)
                {
                    var nro = oDetail.Where(x =>
                        x.salgc_iid_almacen == Convert.ToInt32(btncodigoalmacen.Tag)).ToList().Max(a => Convert.ToInt32(a.salgc_inumero_nota_salida)) + 1;
                    txtnroNota.Text = String.Format("{0:000000}", nro);

                }
                else
                    txtnroNota.Text = String.Format("{0:000000}", 1);
                var lstTipoDocAux = new BAdministracionSistema().listarTipoDocumentoPorModulo(13).Where(x => x.tdocc_vabreviatura_tipo_doc == "N/S").ToList();
                if (lstTipoDocAux.Count > 0)
                {
                    bteTipoDoc.Text = lstTipoDocAux[0].tdocc_vabreviatura_tipo_doc;
                    bteTipoDoc.Tag = lstTipoDocAux[0].tdocc_icod_tipo_doc;
                }
                txtnrodocumento.Text = txtnroNota.Text;
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GenerarCodigo()
        {
            int i = 0;
            var lista = oDetail.Where(ob => ob.salgc_iid_almacen == Convert.ToInt32(btncodigoalmacen.Tag)).ToList();
            if (lista.Count > 0)
                i = lista.Max(ob => Convert.ToInt32(ob.salgc_inumero_nota_salida));
            txtnroNota.Text = String.Format("{0:000000}", i + 1).ToString();
        }
        private void Cargar()
        {

            BSControls.LoaderLook(lkpmotivo, new BGeneral().listarTablaRegistro(35), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpPuntoVenta, new BCentral().ListarPuntoVenta(), "puvec_vdescripcion", "puvec_icod_punto_venta", true);
        }
        public void CalcularCant()
        {
            txtcantidadTotal.Text = (oDetailNotaSalida.Sum(ob => ob.pr_ncant_total_producto)).ToString();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                FrmNotaSalidaDetalle frmdetalle = new FrmNotaSalidaDetalle();
                frmdetalle.indicador = 1;
                frmdetalle.icod_punto_venta = Convert.ToInt32(lkpPuntoVenta.EditValue);
                frmdetalle.icod_almacen = Convert.ToInt32(btncodigoalmacen.Tag);
                frmdetalle.icod_motivo = Convert.ToInt32(lkpmotivo.EditValue);
                frmdetalle.txtitem.Text = (oDetailNotaSalida.Count + 1).ToString();
                frmdetalle.btnModificar.Enabled = false;
                frmdetalle.btnAgregar.Enabled = true;
                if (frmdetalle.ShowDialog() == DialogResult.OK)
                {
                    EProdNotaSalidaDetalle _obe = new EProdNotaSalidaDetalle();
                    _obe.salgc_iid_almacen = frmdetalle._BE.salgc_iid_almacen;
                    _obe.salgcd_num_item = frmdetalle._BE.salgcd_num_item;
                    _obe.tablc_iid_motivo = frmdetalle._BE.tablc_iid_motivo;
                    _obe.pr_icod_producto = frmdetalle._BE.pr_icod_producto;
                    _obe.pr_vcodigo_externo = frmdetalle._BE.pr_vcodigo_externo;
                    _obe.pr_vdescripcion_producto = frmdetalle._BE.pr_vdescripcion_producto;
                    _obe.salgcd_icod_serie = frmdetalle._BE.salgcd_icod_serie;
                    _obe.salgcd_rango_talla = frmdetalle._BE.salgcd_rango_talla;
                    _obe.resec_vdescripcion = frmdetalle._BE.resec_vdescripcion;
                    _obe.salgcd_iid_moneda = frmdetalle._BE.salgcd_iid_moneda;
                    _obe.pr_ncant_total_producto = frmdetalle._BE.pr_ncant_total_producto;
                    _obe.salgcd_monto_unit = frmdetalle._BE.salgcd_monto_unit;
                    _obe.salgcd_monto_total = frmdetalle._BE.salgcd_monto_total;
                    _obe.salgcd_talla1 = frmdetalle._BE.salgcd_talla1;
                    _obe.salgcd_talla2 = frmdetalle._BE.salgcd_talla2;
                    _obe.salgcd_talla3 = frmdetalle._BE.salgcd_talla3;
                    _obe.salgcd_talla4 = frmdetalle._BE.salgcd_talla4;
                    _obe.salgcd_talla5 = frmdetalle._BE.salgcd_talla5;
                    _obe.salgcd_talla6 = frmdetalle._BE.salgcd_talla6;
                    _obe.salgcd_talla7 = frmdetalle._BE.salgcd_talla7;
                    _obe.salgcd_talla8 = frmdetalle._BE.salgcd_talla8;
                    _obe.salgcd_talla9 = frmdetalle._BE.salgcd_talla9;
                    _obe.salgcd_talla10 = frmdetalle._BE.salgcd_talla10;
                    _obe.salgcd_cant_talla1 = frmdetalle._BE.salgcd_cant_talla1;
                    _obe.salgcd_cant_talla2 = frmdetalle._BE.salgcd_cant_talla2;
                    _obe.salgcd_cant_talla3 = frmdetalle._BE.salgcd_cant_talla3;
                    _obe.salgcd_cant_talla4 = frmdetalle._BE.salgcd_cant_talla4;
                    _obe.salgcd_cant_talla5 = frmdetalle._BE.salgcd_cant_talla5;
                    _obe.salgcd_cant_talla6 = frmdetalle._BE.salgcd_cant_talla6;
                    _obe.salgcd_cant_talla7 = frmdetalle._BE.salgcd_cant_talla7;
                    _obe.salgcd_cant_talla8 = frmdetalle._BE.salgcd_cant_talla8;
                    _obe.salgcd_cant_talla9 = frmdetalle._BE.salgcd_cant_talla9;
                    _obe.salgcd_cant_talla10 = frmdetalle._BE.salgcd_cant_talla10;
                    _obe.salgcd_icod_producto1 = frmdetalle._BE.salgcd_icod_producto1;
                    _obe.salgcd_icod_producto2 = frmdetalle._BE.salgcd_icod_producto2;
                    _obe.salgcd_icod_producto3 = frmdetalle._BE.salgcd_icod_producto3;
                    _obe.salgcd_icod_producto4 = frmdetalle._BE.salgcd_icod_producto4;
                    _obe.salgcd_icod_producto5 = frmdetalle._BE.salgcd_icod_producto5;
                    _obe.salgcd_icod_producto6 = frmdetalle._BE.salgcd_icod_producto6;
                    _obe.salgcd_icod_producto7 = frmdetalle._BE.salgcd_icod_producto7;
                    _obe.salgcd_icod_producto8 = frmdetalle._BE.salgcd_icod_producto8;
                    _obe.salgcd_icod_producto9 = frmdetalle._BE.salgcd_icod_producto9;
                    _obe.salgcd_icod_producto10 = frmdetalle._BE.salgcd_icod_producto10;
                    _obe.operacion = 1;
                    oDetailNotaSalida.Add(_obe);
                    CalcularCant();
                    gridControl1.DataSource = oDetailNotaSalida;
                    gridControl1.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
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

                }
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EProdNotaSalidaDetalle _obe = (EProdNotaSalidaDetalle)gvNotaSalida.GetRow(gvNotaSalida.FocusedRowHandle);
            if (_obe != null)
            {
                FrmNotaSalidaDetalle frmdetalle = new FrmNotaSalidaDetalle();

                frmdetalle.txtitem.Text = _obe.salgcd_num_item.ToString();
                frmdetalle.salgcd_icod_nota_salida = _obe.salgcd_icod_nota_salida;
                frmdetalle.icod_almacen = Convert.ToInt32(btncodigoalmacen.Tag);
                frmdetalle.icod_punto_venta = Convert.ToInt32(lkpPuntoVenta.EditValue);
                frmdetalle.icod_motivo = Convert.ToInt32(lkpmotivo.EditValue);
                frmdetalle.btncodigoProducto.Tag = _obe.pr_icod_producto;
                frmdetalle.indicador = 0;
                frmdetalle.btncodigoProducto.Text = _obe.pr_vcodigo_externo;
                frmdetalle.txtdescripcion.Text = _obe.pr_vdescripcion_producto;
                frmdetalle.txtcantidaddetalle.Text = _obe.pr_ncant_total_producto.ToString();
                frmdetalle.lkpSerie.EditValue = _obe.salgcd_icod_serie;
                frmdetalle.txtrangotallas.Text = _obe.salgcd_rango_talla;
                frmdetalle.txttalla1.Text = string.Format("{0:00}", _obe.salgcd_talla1);
                frmdetalle.txttalla2.Text = string.Format("{0:00}", _obe.salgcd_talla2);
                frmdetalle.txttalla3.Text = string.Format("{0:00}", _obe.salgcd_talla3);
                frmdetalle.txttalla4.Text = string.Format("{0:00}", _obe.salgcd_talla4);
                frmdetalle.txttalla5.Text = string.Format("{0:00}", _obe.salgcd_talla5);
                frmdetalle.txttalla6.Text = string.Format("{0:00}", _obe.salgcd_talla6);
                frmdetalle.txttalla7.Text = string.Format("{0:00}", _obe.salgcd_talla7);
                frmdetalle.txttalla8.Text = string.Format("{0:00}", _obe.salgcd_talla8);
                frmdetalle.txttalla9.Text = string.Format("{0:00}", _obe.salgcd_talla9);
                frmdetalle.txttalla10.Text = string.Format("{0:00}", _obe.salgcd_talla10);
                frmdetalle.txtcantidad1.Text = _obe.salgcd_cant_talla1.ToString();
                frmdetalle.txtcantidad2.Text = _obe.salgcd_cant_talla2.ToString();
                frmdetalle.txtcantidad3.Text = _obe.salgcd_cant_talla3.ToString();
                frmdetalle.txtcantidad4.Text = _obe.salgcd_cant_talla4.ToString();
                frmdetalle.txtcantidad5.Text = _obe.salgcd_cant_talla5.ToString();
                frmdetalle.txtcantidad6.Text = _obe.salgcd_cant_talla6.ToString();
                frmdetalle.txtcantidad7.Text = _obe.salgcd_cant_talla7.ToString();
                frmdetalle.txtcantidad8.Text = _obe.salgcd_cant_talla8.ToString();
                frmdetalle.txtcantidad9.Text = _obe.salgcd_cant_talla9.ToString();
                frmdetalle.txtcantidad10.Text = _obe.salgcd_cant_talla10.ToString();
                frmdetalle.icodProducto[0] = _obe.salgcd_icod_producto1;
                frmdetalle.icodProducto[1] = _obe.salgcd_icod_producto2;
                frmdetalle.icodProducto[2] = _obe.salgcd_icod_producto3;
                frmdetalle.icodProducto[3] = _obe.salgcd_icod_producto4;
                frmdetalle.icodProducto[4] = _obe.salgcd_icod_producto5;
                frmdetalle.icodProducto[5] = _obe.salgcd_icod_producto6;
                frmdetalle.icodProducto[6] = _obe.salgcd_icod_producto7;
                frmdetalle.icodProducto[7] = _obe.salgcd_icod_producto8;
                frmdetalle.icodProducto[8] = _obe.salgcd_icod_producto9;
                frmdetalle.icodProducto[9] = _obe.salgcd_icod_producto10;
                frmdetalle.btncodigoProducto.Enabled = false;
                frmdetalle.btnGenerar.Enabled = false;
                frmdetalle.btnAgregar.Enabled = false;
                frmdetalle.Redimencionarstock();
                frmdetalle.cargarcantidadesOriginales();
                frmdetalle.SetModify();
                frmdetalle._BE = _obe;
                if (frmdetalle.ShowDialog() == DialogResult.OK)
                {
                    _obe.pr_ncant_total_producto = frmdetalle._BE.pr_ncant_total_producto;
                    _obe.salgcd_talla1 = frmdetalle._BE.salgcd_talla1;
                    _obe.salgcd_talla2 = frmdetalle._BE.salgcd_talla2;
                    _obe.salgcd_talla3 = frmdetalle._BE.salgcd_talla3;
                    _obe.salgcd_talla4 = frmdetalle._BE.salgcd_talla4;
                    _obe.salgcd_talla5 = frmdetalle._BE.salgcd_talla5;
                    _obe.salgcd_talla6 = frmdetalle._BE.salgcd_talla6;
                    _obe.salgcd_talla7 = frmdetalle._BE.salgcd_talla7;
                    _obe.salgcd_talla8 = frmdetalle._BE.salgcd_talla8;
                    _obe.salgcd_talla9 = frmdetalle._BE.salgcd_talla9;
                    _obe.salgcd_talla10 = frmdetalle._BE.salgcd_talla10;
                    _obe.salgcd_cant_talla1 = frmdetalle._BE.salgcd_cant_talla1;
                    _obe.salgcd_cant_talla2 = frmdetalle._BE.salgcd_cant_talla2;
                    _obe.salgcd_cant_talla3 = frmdetalle._BE.salgcd_cant_talla3;
                    _obe.salgcd_cant_talla4 = frmdetalle._BE.salgcd_cant_talla4;
                    _obe.salgcd_cant_talla5 = frmdetalle._BE.salgcd_cant_talla5;
                    _obe.salgcd_cant_talla6 = frmdetalle._BE.salgcd_cant_talla6;
                    _obe.salgcd_cant_talla7 = frmdetalle._BE.salgcd_cant_talla7;
                    _obe.salgcd_cant_talla8 = frmdetalle._BE.salgcd_cant_talla8;
                    _obe.salgcd_cant_talla9 = frmdetalle._BE.salgcd_cant_talla9;
                    _obe.salgcd_cant_talla10 = frmdetalle._BE.salgcd_cant_talla10;

                    if (_obe.salgcd_icod_nota_salida == 0)
                        _obe.operacion = 1;
                    else
                    {
                        if (_obe.operacion != 1)
                        {
                            _obe.operacion = 2;
                        }
                    }
                    CalcularCant();
                    gridControl1.RefreshDataSource();
                }
            }
        }


        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }
        private void SetSave()
        {
            BCentral obl = new BCentral();
            EProdNotaSalida obj = new EProdNotaSalida();
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
                if (bteTipoDoc.Tag == null)
                {
                    oBase = bteTipoDoc;
                    throw new ArgumentException("Ingresar Documento");
                }

                obj.salgc_icod_nota_salida = salgc_icod_nota_salida;
                obj.salgc_inumero_nota_salida = Convert.ToInt32(txtnroNota.Text);
                obj.salgc_sfecha_nota_salida = Convert.ToDateTime(dtmfecha.EditValue);
                obj.tablc_iid_motivo = Convert.ToInt32(lkpmotivo.EditValue);
                obj.salgc_iid_almacen = Convert.ToInt32(btncodigoalmacen.Tag);
                obj.salgc_iid_tipo_doc = Convert.ToInt32(bteTipoDoc.Tag);
                obj.salgc_inumero_doc = txtnrodocumento.Text;
                obj.salgc_vreferencia = txtReferencia.Text;
                obj.salgc_vobservaciones = txtobservacion.Text;
                obj.puvec_icod_punto_venta = Convert.ToInt32(lkpPuntoVenta.EditValue);
                obj.salgc_sfecha_compra = Convert.ToDateTime(dtmfecha.EditValue);
                obj.tablc_iid_tipo_moneda = 1;
                obj.intUsuario = Valores.intUsuario;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obj.salgc_icod_nota_salida = obl.InsertarProdNotaSalida(obj, oDetailNotaSalida);
                }
                else
                {
                    obj.salgc_icod_nota_salida = salgc_icod_nota_salida;
                    obl.ActualizarProdNotaSalida(obj, oDetailNotaSalida, oDetailNotaSalidaEliminado);
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
                    this.MiEvento(obj.salgc_icod_nota_salida);
                    this.Close();
                }
            }
        }
        private void txtnroNota_EditValueChanged(object sender, EventArgs e)
        {
            if (txtnroNota.Text != "")
            {
                txtnrodocumento.Text = string.Format("{0:00000}", Convert.ToInt32(txtnroNota.Text));
            }
        }


        public class EIntervalo
        {
            public int serie1 { get; set; }
            public int serie2 { get; set; }
            public string codigoExterno { get; set; }
            public string Descripcion { get; set; }

        }



        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (oDetail.Count > 0)
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
            EProdNotaSalidaDetalle obj = (EProdNotaSalidaDetalle)gvNotaSalida.GetRow(gvNotaSalida.FocusedRowHandle);
            if (obj.salgcd_icod_nota_salida == 0)
            {
                oDetailNotaSalida.Remove(obj);
                gvNotaSalida.RefreshData();
                gvNotaSalida.MovePrev();
            }
            else
            {
                //creo listado de eliminados para mandarlo a la BD para actualizar su estado
                if (oDetailNotaSalida.Count > 0)
                {
                    obj.operacion = 3;
                    oDetailNotaSalidaEliminado.Add(obj);
                    oDetailNotaSalida.Remove(obj);
                    gvNotaSalida.RefreshData();
                    gvNotaSalida.MovePrev();
                }

            }
        }

        private void btnsalirformulario_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


        private void generarCódigosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<EIntervalo> mlistadeSeries = new List<EIntervalo>();


            List<EProdInventarioFisicoDetalle> listaInventarioDetalle = new List<EProdInventarioFisicoDetalle>();

            using (frmImportarProductosNotaIngreso frmCliente = new frmImportarProductosNotaIngreso())
            {
                if (frmCliente.ShowDialog() == DialogResult.OK)
                {
                    listaInventarioDetalle = frmCliente.ListaImportacion;

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
                            EIntervalo objEintervalo = new EIntervalo();
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
        }

        public List<EProdProducto> oProductoStock;
        private void GuardarDetalleConCodigosImportados(List<EProdInventarioFisicoDetalle> mlitaProductos, List<EIntervalo> mlistaIntervalos)
        {
            int i = 1;
            foreach (EIntervalo objIntervalo in mlistaIntervalos)
            {
                ArrayList AcumuladoKardex = new ArrayList();
                ArrayList Tallas = new ArrayList();
                ArrayList cantidades = new ArrayList();

                foreach (EProdInventarioFisicoDetalle objProduc in mlitaProductos)
                {
                    if (objProduc.pr_vcodigo_externo.Substring(0, 13).Trim() == objIntervalo.codigoExterno.Trim())
                    {
                        string codigoexterno = objProduc.pr_vcodigo_externo;
                        //oProductoStock = new BCentral().VerificarProdStockProducto(codigoexterno, Convert.ToInt32(btncodigoalmacen.Tag), Convert.ToInt32(lkpPuntoVenta.EditValue));
                        //if (oProductoStock.Count > 0)
                        //{
                        Tallas.Add(objProduc.pr_vcodigo_externo.Substring(13, 2));
                        cantidades.Add(objProduc.infid_ncant_fisica);
                        //}
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
                EProdNotaSalidaDetalle objd = new EProdNotaSalidaDetalle();
                objd.salgc_icod_nota_salida = 0;
                //objd.salgc_icod_nota_salida = salgc_icod_nota_salida;
                objd.salgc_iid_almacen = Convert.ToInt32(btncodigoalmacen.Tag);
                objd.salgcd_num_item = i;
                objd.operacion = 1;
                objd.tablc_iid_motivo = Convert.ToInt32(lkpmotivo.EditValue);
                objd.pr_vcodigo_externo = objIntervalo.codigoExterno;
                objd.pr_vdescripcion_producto = objIntervalo.Descripcion;
                objd.pr_ncant_total_producto = mlitaProductos.Where(o => o.pr_vcodigo_externo.Substring(0, 13).Trim() == objIntervalo.codigoExterno.Trim()).Sum(ob => ob.infid_ncant_fisica);
                objd.salgcd_rango_talla = objIntervalo.serie1 + "/" + objIntervalo.serie2;
                objd.salgcd_iid_moneda = 2;
                objd.salgcd_talla1 = Convert.ToInt32(TallasOri[0]);
                objd.salgcd_talla2 = Convert.ToInt32(TallasOri[1]);
                objd.salgcd_talla3 = Convert.ToInt32(TallasOri[2]);
                objd.salgcd_talla4 = Convert.ToInt32(TallasOri[3]);
                objd.salgcd_talla5 = Convert.ToInt32(TallasOri[4]);
                objd.salgcd_talla6 = Convert.ToInt32(TallasOri[5]);
                objd.salgcd_talla7 = Convert.ToInt32(TallasOri[6]);
                objd.salgcd_talla8 = Convert.ToInt32(TallasOri[7]);
                objd.salgcd_talla9 = Convert.ToInt32(TallasOri[8]);
                objd.salgcd_talla10 = Convert.ToInt32(TallasOri[9]);
                objd.salgcd_cant_talla1 = Convert.ToInt32((CantidadesOri[0]));
                objd.salgcd_cant_talla2 = Convert.ToInt32((CantidadesOri[1]));
                objd.salgcd_cant_talla3 = Convert.ToInt32((CantidadesOri[2]));
                objd.salgcd_cant_talla4 = Convert.ToInt32((CantidadesOri[3]));
                objd.salgcd_cant_talla5 = Convert.ToInt32((CantidadesOri[4]));
                objd.salgcd_cant_talla6 = Convert.ToInt32((CantidadesOri[5]));
                objd.salgcd_cant_talla7 = Convert.ToInt32((CantidadesOri[6]));
                objd.salgcd_cant_talla8 = Convert.ToInt32((CantidadesOri[7]));
                objd.salgcd_cant_talla9 = Convert.ToInt32((CantidadesOri[8]));
                objd.salgcd_cant_talla10 = Convert.ToInt32((CantidadesOri[9]));
                objd.intUsuario = Valores.intUsuario;
                objd.salgcd_iactivo = 1;
                oDetailNotaSalida.Add(objd);
                i++;
                //obld.InsertarNotaSalidaDetalle(objd);
            }
            gridControl1.DataSource = oDetailNotaSalida;
            gridControl1.RefreshDataSource();
        }

        private void bteTipoDoc_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarDocumento();
        }
        private void ListarDocumento()
        {
            try
            {
                if (Convert.ToInt32(btncodigoalmacen.Tag) == 0)
                {
                    XtraMessageBox.Show("Seleccione Almacén", "Infomación del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                frmListarTipoDocumento frm = new frmListarTipoDocumento();
                frm.intIdModulo = 2;//nro: 2 es el ID de Almacenes   
                frm.bModuloall = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteTipoDoc.Tag = frm._Be.tdocc_icod_tipo_doc;
                    bteTipoDoc.Text = frm._Be.tdocc_vabreviatura_tipo_doc;
                }
                txtnrodocumento.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}