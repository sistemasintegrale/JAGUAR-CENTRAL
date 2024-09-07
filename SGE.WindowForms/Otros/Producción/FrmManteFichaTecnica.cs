using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Producción
{
    public partial class FrmManteFichaTecnica : DevExpress.XtraEditors.XtraForm
    {
        public int fitec_iid_ficha_tecnica_ref;
        public string strFicharef;
        public bool UseFichaRef = false;
        public EFichaTecnicaCab oBe = new EFichaTecnicaCab();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        public List<EFichaTecnicaCab> oDetail;
        List<EFichaTecnicaDet> lstFichaTecnicaDet = new List<EFichaTecnicaDet>();
        List<EFichaTecnicaDet> lstDelete = new List<EFichaTecnicaDet>();
        string strCodCliente = "";
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }
        private void btnmodelo_ClientSizeChanged(object sender, EventArgs e) { }
        private void btnmodelo_EditValueChanged(object sender, EventArgs e) { }
        private void btnCancelar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => Dispose();
        private void buttonEdit1_EditValueChanged(object sender, EventArgs e) { }
        public FrmManteFichaTecnica() => InitializeComponent();
        private void btnGuardar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => setSave();
        private void eliminarToolStripMenuItem1_Click_1(object sender, EventArgs e) => eliminar();
        private void FrmManteFactura_Load_1(object sender, EventArgs e) => cargar();
        public void SetModify() => Status = BSMaintenanceStatus.ModifyCurrent;
        public void getCorrelativo() => txtFichaTecnica.Text = (Convert.ToInt32(new BAdministracionSistema().listarParametroProduccion()[0].pmprc_inumero_ficha_tecnica) + 1).ToString();
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            getCorrelativo();
        }
        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
            SetCancel();
        }

        public void dobleclick()
        {
            dteFechaFichaTecnica.Enabled = false;
            btnmodelo.Properties.ReadOnly = true;
            lkpTipo.Properties.ReadOnly = true;
            LkpColor.Properties.ReadOnly = true;
            lkpLinea.Properties.ReadOnly = true;
            lkpTipoTrabajo.Properties.ReadOnly = true;
            lkpMarca.Properties.ReadOnly = true;
            lkpSerie.Properties.ReadOnly = true;
            txtObservaciones.Properties.ReadOnly = true;
            txtFichaTecnica.Properties.ReadOnly = true;
            txtContramuestra.Properties.ReadOnly = true;
            btnmodelo.Enabled = false;
            lkpTipo.Enabled = false;
            LkpColor.Enabled = false;
            lkpLinea.Enabled = false;
            lkpTipoTrabajo.Enabled = false;
            lkpMarca.Enabled = false;
            btnGuardar.Enabled = false;
            lkpSerie.Enabled = false;
            nuevoToolStripMenuItem.Enabled = false;
            modificarToolStripMenuItem.Enabled = false;
            eliminarToolStripMenuItem1.Enabled = false;
            button1.Enabled = false;
            simpleButton1.Enabled = false;
            btnFichaRef.Enabled = false;
        }

        public void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                dteFechaFichaTecnica.Enabled = !Enabled;
                btnmodelo.Enabled = !Enabled;
                lkpTipo.Enabled = !Enabled;
                LkpColor.Enabled = !Enabled;
                lkpLinea.Enabled = !Enabled;
                lkpTipoTrabajo.Enabled = !Enabled;
                lkpMarca.Enabled = !Enabled;
                btnmodelo.Enabled = false;
                LkpColor.Enabled = false;
                lkpMarca.Enabled = false;
                lkpTipo.Enabled = false;
                lkpSerie.Enabled = false;
                btnItemOP.Enabled = false;
                btnOrdenPedido.Enabled = false;
                lkpLinea.Enabled = false;
            }
            else
            {
                dteFechaFichaTecnica.Enabled = !Enabled;
                btnmodelo.Enabled = !Enabled;
                lkpTipo.Enabled = !Enabled;
                LkpColor.Enabled = !Enabled;
                lkpLinea.Enabled = !Enabled;
                lkpTipoTrabajo.Enabled = !Enabled;
                lkpMarca.Enabled = !Enabled;
            }
        }


        public void setValues()
        {
            if (UseFichaRef)
            {
                btnFichaRef.Text = strFicharef;
                btnFichaRef.Tag = fitec_iid_ficha_tecnica_ref.ToString();
            }
            else
            {
                btnFichaRef.Text = oBe.strFicharef == null ? "" : oBe.strFicharef.Replace(" ", "");
                btnFichaRef.Tag = oBe.fitec_iid_ficha_tecnica_ref.ToString();
            }
            try { oBe.imagen = Image.FromStream(WebDavTest.Get($"{Rutas.RutaImagenes}{Rutas.RutaFichaTecnica}{oBe.fitec_iid_ficha_tecnica}/", $"{oBe.fitec_iid_ficha_tecnica}.png")); } catch { }

            txtFichaTecnica.Text = oBe.fitec_icod_ficha_tecnica;
            dteFechaFichaTecnica.EditValue = oBe.fitec_sfecha_ficha_tecnica;
            lkpMarca.EditValue = oBe.fitec_imarca;
            btnmodelo.Tag = oBe.fitec_imodelo;
            btnmodelo.Text = oBe.strmodelo;
            lkpTipo.EditValue = oBe.fitec_itipo;
            LkpColor.EditValue = oBe.fitec_icolor;
            lkpLinea.EditValue = oBe.fitec_ilinea;
            lkpTipoTrabajo.EditValue = oBe.fitec_itipo_trabajo;
            lkpSerie.EditValue = oBe.fitec_iserie;
            txtObservaciones.Text = oBe.fitec_vobservaciones;
            txtContramuestra.Text = oBe.fitec_icorrelativo_contramuestra.ToString();
            btnOrdenPedido.Tag = oBe.fitec_icod_orden_pedido;
            btnOrdenPedido.Text = oBe.StrOrdenPedido;
            btnItemOP.Tag = oBe.fitec_icod_orden_pedido_det;
            btnItemOP.Text = oBe.StrOrdenPedidoDet;
            lstFichaTecnicaDet = new BCentral().ListarFichaTecnicaDet(oBe.fitec_iid_ficha_tecnica);
            grdFichaTecnica.DataSource = lstFichaTecnicaDet;
            viewFichaTecnica.RefreshData();
            ptrimagen.Image = oBe.imagen;
        }

        public List<EProdTablaRegistro> lstLinea = new List<EProdTablaRegistro>();

        private void cargar()
        {
            if (Status == BSMaintenanceStatus.CreateNew)

                setFecha(dteFechaFichaTecnica);

            grdFichaTecnica.DataSource = lstFichaTecnicaDet;
            EProdTablaRegistro ob = new EProdTablaRegistro();
            ob.iid_tipo_tabla = 2;
            BSControls.LoaderLook(lkpTipo, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            ob.iid_tipo_tabla = 8;
            BSControls.LoaderLook(LkpColor, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            ob.iid_tipo_tabla = 3;
            lstLinea = new BCentral().ListarProdTablaRegistro(ob);
            BSControls.LoaderLook(lkpLinea, lstLinea, "descripcion", "icod_tabla", true); ob.iid_tipo_tabla = 20;
            BSControls.LoaderLook(lkpTipoTrabajo, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            ob.iid_tipo_tabla = 11;
            BSControls.LoaderLook(lkpMarca, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            BSControls.LoaderLook(lkpSerie, new BCentral().ListarRegistroSerie(), "resec_vdescripcion", "resec_iid_registro_serie", true);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpMarca.EditValue = oBe.fitec_imarca;
                lkpTipo.EditValue = oBe.fitec_itipo;
                LkpColor.EditValue = oBe.fitec_icolor;
                lkpLinea.EditValue = oBe.fitec_ilinea;
                lkpTipoTrabajo.EditValue = oBe.fitec_itipo_trabajo;
                lkpSerie.EditValue = oBe.fitec_iserie;
                btnFichaRef.Enabled = false;
            }

        }




        private void setFecha(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                frmManteFichaTecnicaDetalle frmdetalle = new frmManteFichaTecnicaDetalle();

                frmdetalle.txtitem.Text = (lstFichaTecnicaDet.Count + 1).ToString();
                frmdetalle.btnModificar.Enabled = false;
                frmdetalle.SetInsert();
                frmdetalle.btnAgregar.Enabled = true;
                if (frmdetalle.ShowDialog() == DialogResult.OK)
                {
                    EFichaTecnicaDet _obe = new EFichaTecnicaDet();
                    _obe.fited_iitem_ficha_tecnica = frmdetalle._BE.fited_iitem_ficha_tecnica;
                    _obe.fited_iarea = frmdetalle._BE.fited_iarea;
                    _obe.strarea = frmdetalle._BE.strarea;
                    _obe.fited_iubicacion = frmdetalle._BE.fited_iubicacion;
                    _obe.strubicacion = frmdetalle._BE.strubicacion;
                    _obe.fited_vdescripcion = frmdetalle._BE.fited_vdescripcion;
                    _obe.prdc_icod_producto = frmdetalle._BE.prdc_icod_producto;
                    _obe.strCodeProducto = frmdetalle._BE.strCodeProducto;
                    _obe.strProducto = frmdetalle._BE.strProducto;
                    _obe.strUnidadMedida = frmdetalle._BE.strUnidadMedida;
                    _obe.fited_ncantidad = frmdetalle._BE.fited_ncantidad;
                    _obe.fited_imedida = frmdetalle._BE.fited_imedida;
                    _obe.intTipoOperacion = 1;
                    lstFichaTecnicaDet.Add(_obe);
                    grdFichaTecnica.DataSource = lstFichaTecnicaDet;
                    grdFichaTecnica.RefreshDataSource();

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
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (txtFichaTecnica.Text == "")
                {
                    oBase = txtFichaTecnica;
                    throw new ArgumentException("Ingrese Nro. de Serie de Ficha Técnica");
                }
                oBe.fitec_iid_ficha_tecnica_ref = Convert.ToInt32(btnFichaRef.Tag);
                oBe.fitec_icod_ficha_tecnica = txtFichaTecnica.Text;
                oBe.fitec_sfecha_ficha_tecnica = Convert.ToDateTime(dteFechaFichaTecnica.Text);
                oBe.fitec_imarca = Convert.ToInt32(lkpMarca.EditValue);
                oBe.strmarca = lkpMarca.Text;
                oBe.fitec_imodelo = Convert.ToInt32(btnmodelo.Tag);
                oBe.strmodelo = btnmodelo.Text;
                oBe.fitec_itipo = Convert.ToInt32(lkpTipo.EditValue);
                oBe.strtipo = lkpTipo.Text;
                oBe.fitec_icolor = Convert.ToInt32(LkpColor.EditValue);
                oBe.strcolor = LkpColor.Text;
                oBe.fitec_ilinea = Convert.ToInt32(lkpLinea.EditValue);
                oBe.strlinea = lkpLinea.Text;
                oBe.fitec_itipo_trabajo = Convert.ToInt32(lkpTipoTrabajo.EditValue);
                oBe.strtipo_trabajo = lkpTipoTrabajo.Text;
                oBe.fitec_iserie = Convert.ToInt32(lkpSerie.EditValue);
                oBe.strserie = lkpSerie.Text;
                oBe.fitec_vobservaciones = txtObservaciones.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.fitec_icorrelativo_contramuestra = Convert.ToInt32(txtContramuestra.Text);
                oBe.fitec_icod_orden_pedido = Convert.ToInt32(btnOrdenPedido.Tag);
                oBe.fitec_icod_orden_pedido_det = Convert.ToInt32(btnItemOP.Tag);
                oBe.imagen = ptrimagen.Image;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.fitec_iid_ficha_tecnica = new BCentral().InsertarFichaTecnicaCab(oBe, lstFichaTecnicaDet);
                }
                else
                {
                    new BCentral().modificarFichaTecnicaCab(oBe, lstFichaTecnicaDet, lstDelete);
                }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    MiEvento(oBe.fitec_iid_ficha_tecnica);
                    Close();
                }
            }
        }
        private void modificarItem()
        {
            EFichaTecnicaDet obe = (EFichaTecnicaDet)viewFichaTecnica.GetRow(viewFichaTecnica.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteFichaTecnicaDetalle frmdetalle = new frmManteFichaTecnicaDetalle())
            {
                frmdetalle._BE = obe;
                frmdetalle.txtitem.Text = obe.fited_iitem_ficha_tecnica.ToString();
                frmdetalle.lkpArea.EditValue = obe.fited_iarea;
                frmdetalle.lkpUbicacion.EditValue = obe.fited_iubicacion;
                frmdetalle.txtDescripcion.Text = obe.fited_vdescripcion;
                frmdetalle.bteProducto.Tag = obe.prdc_icod_producto;
                frmdetalle.bteProducto.Text = obe.strCodeProducto;
                frmdetalle.txtDescProd.Text = obe.strProducto;
                frmdetalle.txtCantidad.Text = Convert.ToString(obe.fited_ncantidad);
                frmdetalle.btnAgregar.Enabled = false;
                frmdetalle.SetModify();
                if (frmdetalle.ShowDialog() == DialogResult.OK)
                {
                    obe.fited_iarea = frmdetalle._BE.fited_iarea;
                    obe.fited_iubicacion = frmdetalle._BE.fited_iubicacion;
                    obe.fited_vdescripcion = frmdetalle._BE.fited_vdescripcion;
                    obe.prdc_icod_producto = frmdetalle._BE.prdc_icod_producto;
                    obe.strCodeProducto = frmdetalle._BE.strCodeProducto;
                    obe.strProducto = frmdetalle._BE.strProducto;
                    obe.strUnidadMedida = frmdetalle._BE.strUnidadMedida;
                    obe.fited_ncantidad = frmdetalle._BE.fited_ncantidad;
                    obe.fited_imedida = frmdetalle._BE.fited_imedida;
                    obe.intTipoOperacion = 2;
                    grdFichaTecnica.RefreshDataSource();
                }
            }
        }



        private void modificarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            EFichaTecnicaDet obe = (EFichaTecnicaDet)viewFichaTecnica.GetRow(viewFichaTecnica.FocusedRowHandle);
            var z = viewFichaTecnica.GetFocusedRow() as EFichaTecnicaDet;
            if (obe == null)
                return;

            modificarItem();
        }


        private void eliminar()
        {
            EFichaTecnicaDet obe = (EFichaTecnicaDet)viewFichaTecnica.GetRow(viewFichaTecnica.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstFichaTecnicaDet.Remove(obe);
            viewFichaTecnica.RefreshData();
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OpenFd.Title = "Insertar Imagen";
            OpenFd.FileName = "";
            OpenFd.Filter = "JPG Imagenes(*.jpg)|*.jpg|JPEG Imagenes(*.jpeg)|*.jpeg|PNG Imagenes(*.png)|*.png";
            if (OpenFd.ShowDialog() == DialogResult.OK)
            {
                using (var bmpTemp = new Bitmap(OpenFd.FileName))
                {
                    ptrimagen.Image = new Bitmap(bmpTemp);
                }
            }
        }

        private void btnmodelo_Click(object sender, EventArgs e)
        {
            List<EProdTablaRegistro> listRegistro = new List<EProdTablaRegistro>();
            EProdTablaRegistro ob = new EProdTablaRegistro();
            LkpColor.EditValue = "0";
            EProdModelo obje = new EProdModelo();
            ob.iid_tipo_tabla = 11;
            listRegistro = new BCentral().ListarProdTablaRegistro(ob).Where(obj => obj.icod_tabla == Convert.ToInt32(lkpMarca.EditValue)).ToList();
            listRegistro.ForEach(obe => obje.tarec_icorrelativo = obe.icod_tabla);
            using (ListarModelo listmodelo = new ListarModelo())
            {
                listmodelo.CodeMarcas = Convert.ToInt32(obje.tarec_icorrelativo);
                if (listmodelo.ShowDialog() == DialogResult.OK)
                {
                    btnmodelo.Tag = string.Format("{0:0000}", listmodelo._Be.mo_icod_modelo);
                    btnmodelo.Text = listmodelo._Be.mo_vdescripcion;
                    lkpMarca.EditValue = listmodelo._Be.tarec_iid_tabla_registro;
                    lkpTipo.EditValue = listmodelo._Be.pr_iid_categoria;
                    lkpTipo.Text = listmodelo._Be.pr_iid_categoria_descripcion;
                    lkpLinea.EditValue = listmodelo._Be.pr_iid_linea;
                    lkpLinea.Text = listmodelo._Be.pr_iid_linea_descripcion;
                    lkpTipo.Enabled = false;
                    lkpLinea.Enabled = false;
                    lkpSerie.Enabled = false;
                    simpleButton1.Enabled = false;

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lstFichaTecnicaDet.Clear();
            List<EFichaTecnicaMateriales> mListMateriales = new List<EFichaTecnicaMateriales>();
            mListMateriales = new BCentral().ListarFichaTecnicaMateriales(Convert.ToInt32(btnmodelo.Tag));
            foreach (var _BEmateriales in mListMateriales)
            {
                EFichaTecnicaDet _BEe = new EFichaTecnicaDet();
                _BEe.fited_iitem_ficha_tecnica = _BEmateriales.fited_iitem_ficha_tecnica;
                _BEe.fited_iarea = _BEmateriales.fited_iarea;
                _BEe.strarea = _BEmateriales.strarea;
                _BEe.fited_iubicacion = _BEmateriales.fited_iubicacion;
                _BEe.strubicacion = _BEmateriales.strubicacion;
                _BEe.fited_vdescripcion = _BEmateriales.fited_vdescripcion;
                _BEe.prdc_icod_producto = _BEmateriales.prdc_icod_producto;
                _BEe.strCodeProducto = _BEmateriales.strCodeProducto;
                _BEe.strProducto = _BEmateriales.strProducto;
                _BEe.strUnidadMedida = _BEmateriales.strUnidadMedida;
                _BEe.fited_ncantidad = _BEmateriales.fited_ncantidad;
                _BEe.fited_imedida = _BEmateriales.fited_imedida;
                lstFichaTecnicaDet.Add(_BEe);
            }
            nuevoToolStripMenuItem.Enabled = false;
            viewFichaTecnica.RefreshData();
        }
        private void buttonEdit1_Click(object sender, EventArgs e)
        {
            using (ListarFichaTecnicaRef frmlista = new ListarFichaTecnicaRef())
            {
                if (frmlista.ShowDialog() == DialogResult.OK)
                {
                    oBe = frmlista._Be;
                    fitec_iid_ficha_tecnica_ref = frmlista._Be.fitec_iid_ficha_tecnica;
                    strFicharef = oBe.fitec_icod_ficha_tecnica.ToString();
                    oBe.fitec_icod_ficha_tecnica = txtFichaTecnica.Text;
                    oBe.fitec_icorrelativo_contramuestra = Convert.ToInt32(txtContramuestra.Text);
                    UseFichaRef = true;
                    setValues();
                }
            }
        }

        private void btnOrdenPedido_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmOrdenPedidoListar frm = new FrmOrdenPedidoListar();
            frm.Text = "Lista de Ordenes de Pedido";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnOrdenPedido.Text = frm.OrdenSelect.orpec_icod_orden_pedido;
                btnOrdenPedido.Tag = frm.OrdenSelect.orpec_iid_orden_pedido;
                btnItemOP.Enabled = true;
            }
        }

        private void btnItemOP_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmOrdenPedidoDetalleListar frm = new FrmOrdenPedidoDetalleListar();
            frm.Text = $"Lista de Items de la Orden de Pedido {btnOrdenPedido.Text}";
            frm.orpec_iid_orden_pedido = Convert.ToInt32(btnOrdenPedido.Tag);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnItemOP.Text = frm.selectDet.orped_iitem_orden_pedido.ToString();
                btnItemOP.Tag = frm.selectDet.orped_icod_item_orden_pedido;
                btnmodelo.Tag = frm.selectDet.orped_imodelo;
                btnmodelo.Text = frm.selectDet.strmodelo;
                LkpColor.EditValue = frm.selectDet.orped_icolor;
                lkpMarca.EditValue = frm.selectDet.orped_imarca;
                lkpTipo.EditValue = frm.selectDet.orped_itipo;
                lkpSerie.EditValue = frm.selectDet.orped_serie;
                txtFichaTecnica.Text = frm.selectDet.orped_vnum_ficha_tecnica;
                try { ptrimagen.Image = Image.FromStream(WebDavTest.Get($"{Rutas.RutaImagenes}{Rutas.RutaOrdenPedido}{btnOrdenPedido.Tag.ToString()}/", $"{btnItemOP.Tag.ToString()}.png")); } catch { };
                btnmodelo.Enabled = false;
                LkpColor.Enabled = false;
                lkpMarca.Enabled = false;
                lkpTipo.Enabled = false;
                lkpSerie.Enabled = false;
                var modeloSelect = new BCentral().ModeloGetById(Convert.ToInt32(btnmodelo.Tag));
                lkpLinea.EditValue = lstLinea.Where(x => x.tarec_iid_correlativo == modeloSelect.pr_iid_linea).FirstOrDefault().icod_tabla;
                lkpLinea.Enabled = false;
            }
        }
    }
}