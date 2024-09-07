using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmManteTransferencia : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteNotaIngreso));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        /*----------------------------------------------------*/
        public ETransferenciaAlmacen oBe = new ETransferenciaAlmacen();
        /*----------------------------------------------------*/
        List<ETransferenciaAlmacenDet> lstDetalle = new List<ETransferenciaAlmacenDet>();
        List<ETransferenciaAlmacenDet> lstDelete = new List<ETransferenciaAlmacenDet>();
        /*----------------------------------------------------*/
        public List<ETransferenciaAlmacen> lstTransferencias = new List<ETransferenciaAlmacen>();
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
        public frmManteTransferencia()
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
            if (Status == BSMaintenanceStatus.CreateNew)
                getNroTransf();
            grdDetalle.DataSource = lstDetalle;
        }

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            bteAlmacenSal.Enabled = !Enabled;
            bteAlmacenIng.Enabled = !Enabled;
            txtObservaciones.Enabled = !Enabled;
            dteFecha.Enabled = !Enabled;
            mnuNotaIngresoDetalle.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            bteAlmacenSal.Focus();
        }

        public void setValues()
        {
            bteAlmacenSal.Tag = oBe.almac_icod_alm_sal;
            bteAlmacenSal.Text = oBe.strAlmacenSal;
            bteAlmacenIng.Tag = oBe.almac_icod_alm_ing;
            bteAlmacenIng.Text = oBe.strAlmacenIng;
            txtNroTransferencia.Text = String.Format("{0:0000}", oBe.trfc_inum_transf);
            dteFecha.EditValue = oBe.trfc_sfecha_transf;
            txtObservaciones.Text = oBe.trnfc_vobservaciones;

            lstDetalle = new BAlmacen().listarTransferenciaAlmacenDet(oBe.trfc_icod_transf);
            grdDetalle.DataSource = lstDetalle;
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
            listarAlmacenSal();
        }


        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(bteAlmacenSal.Tag) == 0)
                    throw new ArgumentException("Seleccione Almacén");
                using (frmManteTransferenciaDetalle frm = new frmManteTransferenciaDetalle())
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
                ETransferenciaAlmacenDet obe = (ETransferenciaAlmacenDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
                if (obe != null)
                {
                    int index = viewDetalle.FocusedRowHandle;
                    using (frmManteTransferenciaDetalle frm = new frmManteTransferenciaDetalle())
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
                ETransferenciaAlmacenDet obe = (ETransferenciaAlmacenDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
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
                lstDetalle[i].trfd_nro_item = i + 1;
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
                if (Convert.ToInt32(bteAlmacenSal.Tag) == 0)
                {
                    oBase = bteAlmacenSal;
                    throw new ArgumentException("Seleccione almacén");
                }

                if (Convert.ToInt32(bteAlmacenIng.Tag) == 0)
                {
                    oBase = bteAlmacenIng;
                    throw new ArgumentException("Seleccione almacén");
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
                oBe.trfc_inum_transf = Convert.ToInt32(txtNroTransferencia.Text);
                oBe.almac_icod_alm_sal = Convert.ToInt32(bteAlmacenSal.Tag);
                oBe.almac_icod_alm_ing = Convert.ToInt32(bteAlmacenIng.Tag);
                oBe.trfc_sfecha_transf = Convert.ToDateTime(dteFecha.DateTime.ToShortDateString());
                oBe.trnfc_vobservaciones = txtObservaciones.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.trnfc_iid_motivo = 102;//TRANSFERENCIA A ALMACEN
                /*---------------------------------------------------------*/
                oBe.strAlmacenSal = bteAlmacenSal.Text.ToUpper();
                oBe.strAlmacenIng = bteAlmacenIng.Text.ToUpper();

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.trfc_icod_transf = new BAlmacen().insertarTransferenciaAlmacen(oBe, lstDetalle);
                }
                else
                {
                    new BAlmacen().modificarTransferenciaAlmacen(oBe, lstDetalle, lstDelete);
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
                    this.MiEvento(oBe.trfc_icod_transf);
                    this.Close();
                }
            }
        }

        private void getNroTransf()
        {
            try
            {
                if (lstTransferencias.Count > 0)
                    txtNroTransferencia.Text = String.Format("{0:0000}", Convert.ToInt32(lstTransferencias.Max(x => x.trfc_inum_transf) + 1));
                else
                    txtNroTransferencia.Text = "0001";

            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listarAlmacenSal()
        {
            try
            {
                frmListarAlmacen frm = new frmListarAlmacen();

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (Convert.ToInt32(bteAlmacenIng.Tag) == frm._Be.almac_icod_almacen)
                    {
                        XtraMessageBox.Show("El Almacén de Salida NO puede ser el mismo que el Almacén de Ingreso", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    bteAlmacenSal.Tag = frm._Be.almac_icod_almacen;
                    bteAlmacenSal.Text = frm._Be.almac_vdescripcion;
                    oBe.almac_icod_alm_sal = frm._Be.almac_icod_almacen;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listarAlmacenIng()
        {
            try
            {
                frmListarAlmacen Almacen = new frmListarAlmacen();

                if (Almacen.ShowDialog() == DialogResult.OK)
                {
                    if (Convert.ToInt32(bteAlmacenSal.Tag) == Almacen._Be.almac_icod_almacen)
                    {
                        XtraMessageBox.Show("El Almacén de Ingreso NO puede ser el mismo que el Almacén de Salida", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    bteAlmacenIng.Tag = Almacen._Be.almac_icod_almacen;
                    bteAlmacenIng.Text = Almacen._Be.almac_vdescripcion;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void bteAlmacenIng_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacenIng();
        }

    }
}