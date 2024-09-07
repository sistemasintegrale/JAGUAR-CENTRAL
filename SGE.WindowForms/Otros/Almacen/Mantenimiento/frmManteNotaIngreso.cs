using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Administracion_del_Sistema.Listados;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Reportes.Almacen.Registros;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmManteNotaIngreso : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteNotaIngreso));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        /*----------------------------------------------------*/
        public ENotaIngreso oBe = new ENotaIngreso();
        /*----------------------------------------------------*/
        List<ENotaIngresoDetalle> lstDetalle = new List<ENotaIngresoDetalle>();
        List<ENotaIngresoDetalle> lstDelete = new List<ENotaIngresoDetalle>();

        /*----------------------------------------------------*/
        public List<ENotaIngreso> lstCabecerasNI = new List<ENotaIngreso>();
        /*----------------------------------------------------*/
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
        public frmManteNotaIngreso()
        {
            InitializeComponent();
        }

        private void FrmManteNotaIngreso_Load(object sender, EventArgs e)
        {
            Carga();
            BSControls.LoaderLook(lkpMotivo, new BGeneral().listarTablaRegistro(34), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            if (Parametros.intEjercicio == DateTime.Now.Year)
                dteFecha.EditValue = DateTime.Now;
            else
                dteFecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);

        }

        private void Carga()
        {
            if (Status != BSMaintenanceStatus.CreateNew)
                lstDetalle = new BAlmacen().listarNotaIngresoDetalle(oBe.ningc_icod_nota_ingreso);
            grdDetalle.DataSource = lstDetalle;
        }

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            bteAlmacen.Enabled = !Enabled;
            bteTipoDoc.Enabled = !Enabled;
            txtNroDocumento.Enabled = !Enabled;
            txtReferencia.Enabled = !Enabled;
            txtObservaciones.Enabled = !Enabled;
            lkpMotivo.Enabled = !Enabled;
            dteFecha.Enabled = !Enabled;
            mnuNotaIngresoDetalle.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                bteAlmacen.Enabled = Enabled;
                bteTipoDoc.Enabled = Enabled;
            }
            bteAlmacen.Focus();
        }

        public void setValues()
        {
            bteAlmacen.Tag = oBe.almac_icod_almacen;
            bteAlmacen.Text = oBe.strAlmacen;
            txtNumeroNI.Text = oBe.ningc_numero_nota_ingreso;
            bteTipoDoc.Tag = oBe.tdocc_icod_tipo_doc;
            bteTipoDoc.Text = oBe.strTipoDoc;
            txtNroDocumento.Text = oBe.ningc_numero_doc;
            lkpMotivo.EditValue = oBe.ningc_iid_motivo;
            dteFecha.EditValue = oBe.ningc_fecha_nota_ingreso;
            txtReferencia.Text = oBe.ningc_referencia;
            txtObservaciones.Text = oBe.ningc_observaciones;
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        private void btnAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
        }

        private void btnDocumento_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarDocumento();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                    throw new ArgumentException("Seleccione Almacén");
                using (FrmManteNotaIngresoDetalle frm = new FrmManteNotaIngresoDetalle())
                {
                    frm.obeCab = oBe;
                    frm.lstDetalle = lstDetalle;
                    frm.SetInsert();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        viewDetalle.RefreshData();
                        viewDetalle.MoveLast();
                        viewDetalle.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ENotaIngresoDetalle obe = (ENotaIngresoDetalle)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
                if (obe != null)
                {
                    int index = viewDetalle.FocusedRowHandle;
                    using (FrmManteNotaIngresoDetalle frm = new FrmManteNotaIngresoDetalle())
                    {
                        frm.oBe = obe;
                        frm.obeCab = oBe;
                        frm.lstDetalle = lstDetalle;
                        frm.SetModify();
                        frm.setValues();
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            lstDetalle = frm.lstDetalle;
                            viewDetalle.RefreshData();
                            viewDetalle.FocusedRowHandle = index;
                            viewDetalle.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ENotaIngresoDetalle obe = (ENotaIngresoDetalle)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
                if (obe != null)
                {
                    int index = viewDetalle.FocusedRowHandle;
                    lstDelete.Add(obe);
                    lstDetalle.Remove(obe);
                    renumerar();
                    viewDetalle.RefreshData();
                    viewDetalle.FocusedRowHandle = index;
                    viewDetalle.Focus();
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void renumerar()
        {
            for (int i = 0; i < lstDetalle.Count; i++)
            {
                lstDetalle[i].dninc_nro_item = i + 1;
                if (lstDetalle[i].intTipoOperacion == 0)
                    lstDetalle[i].intTipoOperacion = Parametros.intOperacionModificar;
            }
        }


        public void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                {
                    oBase = bteAlmacen;
                    throw new ArgumentException("Seleccione almacén");
                }

                if (Convert.ToInt32(bteTipoDoc.Tag) == 0)
                {
                    oBase = bteTipoDoc;
                    throw new ArgumentException("Seleccionar el Documento");
                }

                if (string.IsNullOrEmpty(txtNroDocumento.Text))
                {
                    oBase = txtNroDocumento;
                    throw new ArgumentException("Ingresar el numero de documento");
                }

                if (lstDetalle.Count == 0)
                {
                    throw new ArgumentException("Ingresar al menos un producto");
                }
                if (Convert.ToDateTime(dteFecha.EditValue).Year != Parametros.intEjercicio)
                {
                    oBase = dteFecha;
                    throw new ArgumentException("La fecha seleccionada no esta dentro del año de ejercicio" + Parametros.intEjercicio);
                }

                /*---------------------------------------------------------*/
                oBe.ningc_ianio = Parametros.intEjercicio;
                oBe.ningc_numero_nota_ingreso = txtNumeroNI.Text;
                oBe.almac_icod_almacen = Convert.ToInt32(bteAlmacen.Tag);
                oBe.ningc_iid_motivo = Convert.ToInt32(lkpMotivo.EditValue);
                oBe.ningc_fecha_nota_ingreso = Convert.ToDateTime(dteFecha.DateTime.ToShortDateString());
                oBe.tdocc_icod_tipo_doc = Convert.ToInt32(bteTipoDoc.Tag);
                oBe.ningc_numero_doc = txtNroDocumento.Text;
                oBe.ningc_referencia = txtReferencia.Text;
                oBe.ningc_observaciones = txtObservaciones.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;


                /*---------------------------------------------------------*/
                oBe.strAlmacen = bteAlmacen.Text.ToUpper();
                oBe.strMotivo = lkpMotivo.Text.ToUpper();
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.ningc_icod_nota_ingreso = new BAlmacen().insertarNotaIngreso(oBe, lstDetalle);
                }
                else
                {
                    new BAlmacen().modificarNotaIngreso(oBe, lstDetalle, lstDelete);
                }
                /*-------------------------------------------------*/
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(oBe.ningc_icod_nota_ingreso);
                    //imprimir();
                    this.Close();
                }
            }
        }
        private void imprimir()
        {
            if (XtraMessageBox.Show("¿Desea imprimir la nota de ingreso?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            var lstDetalle = new BAlmacen().listarNotaIngresoDetalle(oBe.ningc_icod_nota_ingreso);
            rptNotaIngreso rpt = new rptNotaIngreso();
            rpt.cargar(String.Format("NOTA DE INGRESO N° {0}", oBe.ningc_numero_nota_ingreso), oBe.strAlmacen, lstDetalle, oBe);

        }
        private void getNroNI()
        {
            try
            {
                if (lstCabecerasNI.Where(x =>
                    x.almac_icod_almacen == Convert.ToInt32(bteAlmacen.Tag)).ToList().Count > 0)
                {
                    var nro = lstCabecerasNI.Where(x =>
                        x.almac_icod_almacen == Convert.ToInt32(bteAlmacen.Tag)).ToList().Max(a => Convert.ToInt32(a.ningc_numero_nota_ingreso)) + 1;
                    txtNumeroNI.Text = String.Format("{0:000000}", nro);

                }
                else
                    txtNumeroNI.Text = String.Format("{0:000000}", 1);
                var lstTipoDocAux = new BAdministracionSistema().listarTipoDocumento().Where(x => x.tdocc_vabreviatura_tipo_doc == "N/I").ToList();
                if (lstTipoDocAux.Count > 0)
                {
                    bteTipoDoc.Text = lstTipoDocAux[0].tdocc_vabreviatura_tipo_doc;
                    bteTipoDoc.Tag = lstTipoDocAux[0].tdocc_icod_tipo_doc;
                }
                txtNroDocumento.Text = txtNumeroNI.Text;
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listarAlmacen()
        {
            try
            {
                frmListarAlmacen Almacen = new frmListarAlmacen();

                if (Almacen.ShowDialog() == DialogResult.OK)
                {

                    bteAlmacen.Tag = Almacen._Be.almac_icod_almacen;
                    bteAlmacen.Text = Almacen._Be.almac_vdescripcion;
                }
                getNroNI();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListarDocumento()
        {
            try
            {
                if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                {
                    XtraMessageBox.Show("Seleccione Almacén", "Infomación del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                frmListarTipoDocumento frm = new frmListarTipoDocumento();
                frm.intIdModulo = 2;//nro: 2 es el ID de Almacenes            
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteTipoDoc.Tag = frm._Be.tdocc_icod_tipo_doc;
                    bteTipoDoc.Text = frm._Be.tdocc_vabreviatura_tipo_doc;
                }
                txtNroDocumento.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }
        private void Imprimir()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            //ENotaIngreso oBe = new ENotaIngreso();
            //Obl = new BNotaIngreso();
            try
            {
                //        if (string.IsNullOrEmpty(btnAlmacen.Text))
                //        {
                //            oBase = btnAlmacen;
                //            throw new ArgumentException("Ingresar el Almacén");
                //        }

                //        if (string.IsNullOrEmpty(btnDocumento.Text))
                //        {
                //            oBase = btnDocumento;
                //            throw new ArgumentException("Seleccionar el Documento");
                //        }

                //        if (string.IsNullOrEmpty(txtNroDocumento.Text))
                //        {
                //            oBase = txtNroDocumento;
                //            throw new ArgumentException("Ingresar el numero de documento");
                //        }

                //        if (mListaNotaIngresoDetalleOrigen.Count == 0)
                //        {
                //            oBase = txtNroDocumento;
                //            throw new ArgumentException("Ingresar al menos un producto");
                //        }
                //        if (XtraMessageBox.Show("Para Imprimir, se Guardaran todos los datos /n ¿Desea Continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //        {
                //            GrabarDatos();
                //            if (intIdNotaIngreso == 0)
                //            {
                //                rptRegistroNotaIngresoReporte repor = new rptRegistroNotaIngresoReporte();
                //                repor.cargar(lstDetalle, oBe);
                //            }
                //            else
                //            {
                //                List<ENotaIngresoDetalle> mllistAux = new List<ENotaIngresoDetalle>();
                //                mllistAux = new BNotaIngresoDetalle().ListarNotaIngresoDetalle(intIdNotaIngreso);
                //                rptRegistroNotaIngresoReporte repor = new rptRegistroNotaIngresoReporte();
                //                repor.cargar(mllistAux, oBe);
                //            }
                //        }
                //        else
                //        {
                //            Flag = false;
                //        }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Flag = false;
                }
            }
            finally

            {
                if (Flag)
                {

                    //this.MiEvento();
                    this.Close();
                }
            }

        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }




    }
}