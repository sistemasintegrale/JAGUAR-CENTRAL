using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmMantePlantillaEmpaq : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteNotaIngreso));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        /*----------------------------------------------------*/
        public EEmpaquePlantilla oBe = new EEmpaquePlantilla();
        /*----------------------------------------------------*/
        List<EEmpaquePlantilla> lstDetalle = new List<EEmpaquePlantilla>();
        List<EEmpaquePlantilla> lstDelete = new List<EEmpaquePlantilla>();
        /*----------------------------------------------------*/
        public List<EEmpaquePlantilla> lstCabecerasNI = new List<EEmpaquePlantilla>();
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
        public frmMantePlantillaEmpaq()
        {
            InitializeComponent();
        }

        private void FrmManteNotaIngreso_Load(object sender, EventArgs e)
        {
            Carga();
            if (Parametros.intEjercicio == DateTime.Now.Year)
                dteFecha.EditValue = DateTime.Now;
            else
                dteFecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);

        }

        private void Carga()
        {
            if (Status != BSMaintenanceStatus.CreateNew)
                lstDetalle = new BAlmacen().EmpaquePlantillaDetListar(oBe.plemc_iid);
            grdDetalle.DataSource = lstDetalle;
        }

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtNumero.Enabled = !Enabled;
            dteFecha.Enabled = !Enabled;
            txtUm.Enabled = !Enabled;

            txtObservaciones.Enabled = !Enabled;



        }

        public void setValues()
        {
            txtNumero.Text = string.Format("{0:0000}", oBe.plemc_icod);
            dteFecha.EditValue = oBe.plemc_sfecha;
            btnProducto.Tag = oBe.prdc_icod_producto;
            txtDescripcion.Text = oBe.prdc_vdescripcion_larga;
            btnProducto.Text = oBe.prdc_vcode_producto;
            txtUm.Text = oBe.unidc_vabreviatura_unidad_medida;
            txtObservaciones.Text = oBe.plemc_vobservacion;

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

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                using (frmMantePlantillaEmpaqDetalle frm = new frmMantePlantillaEmpaqDetalle())
                {
                    frm.obeCab = oBe;
                    frm.lstDetalle = lstDetalle;
                    frm.SetInsert();
                    frm.txtitem.Text = (lstDetalle.Count + 1).ToString();
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
                EEmpaquePlantilla obe = (EEmpaquePlantilla)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
                if (obe != null)
                {
                    int index = viewDetalle.FocusedRowHandle;
                    using (frmMantePlantillaEmpaqDetalle frm = new frmMantePlantillaEmpaqDetalle())
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
                EEmpaquePlantilla obe = (EEmpaquePlantilla)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
                if (obe != null)
                {
                    int index = viewDetalle.FocusedRowHandle;
                    lstDelete.Add(obe);
                    lstDetalle.Remove(obe);

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

        public void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (Convert.ToInt32(btnProducto.Tag) == 0)
                {
                    oBase = btnProducto;
                    throw new ArgumentException("Seleccione Prodcuto Final");
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
                oBe.plemc_icod = Convert.ToInt32(txtNumero.Text);
                oBe.plemc_sfecha = Convert.ToDateTime(dteFecha.EditValue);
                oBe.prdc_icod_producto = Convert.ToInt32(btnProducto.Tag);
                oBe.plemc_vobservacion = txtObservaciones.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.plemd_flag_estado = true;
                /*---------------------------------------------------------*/

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.plemc_iid = new BAlmacen().EmpaquePlantillaInsertar(oBe, lstDetalle);
                }
                else
                {
                    new BAlmacen().EmpaquePlantillaModificar(oBe, lstDetalle, lstDelete);
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
                    this.MiEvento(oBe.plemc_iid);
                    //imprimir();
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

        private void btnProducto_DoubleClick(object sender, EventArgs e)
        {
            listarProducto();
        }

        private void listarProducto()
        {
            try
            {
                frmListarProducto frm = new frmListarProducto();
                frm.flag_solo_prods = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnProducto.Tag = frm._Be.prdc_icod_producto;
                    btnProducto.Text = frm._Be.prdc_vcode_producto;
                    txtDescripcion.Text = frm._Be.prdc_vdescripcion_larga;
                    txtUm.Text = frm._Be.StrUnidadMedida;

                }
                txtObservaciones.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProducto();
        }

    }
}