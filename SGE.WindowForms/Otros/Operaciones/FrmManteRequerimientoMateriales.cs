using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Operaciones
{
    public partial class FrmManteRequerimientoMateriales : DevExpress.XtraEditors.XtraForm
    {
        public ERequerimientoMateriales oBe = new ERequerimientoMateriales();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        List<ERequerimientoMaterialesDetalle> lstRequerimientoMaterialesDetalle = new List<ERequerimientoMaterialesDetalle>();
        List<ERequerimientoMaterialesDetalle> lstDelete = new List<ERequerimientoMaterialesDetalle>();
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public int IcodRubros = 0;
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }
        public void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {

            }
        }

        public void setValues()
        {
            txtNumero.Text = oBe.rqmc_numero_req_material;
            dteFecha.EditValue = oBe.rqmc_sfecha_req_material;
            lkpSituacion.EditValue = oBe.tablc_iid_situación_hc;
            txtObservaciones.Text = oBe.rqmc_vdescripcion;
            txtHCN.Text = oBe.NumHojaCosto;
            lkpTipo.EditValue = oBe.tablc_iid_tipo_requerimiento;
            bteProyecto.Tag = oBe.pryc_icod_proyecto;
            bteProyecto.Text = string.Format("{0:00000}", oBe.pryc_icod_proyecto);
            IcodRubros = oBe.hjcc_icod_hoja_costo;
            lstRequerimientoMaterialesDetalle = new BVentas().listarRequerimientoMaterialesDetalle(oBe.rqmc_icod_requerimiento_materiales);

        }

        public FrmManteRequerimientoMateriales()
        {
            InitializeComponent();
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }
        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            setValues();
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        private void cargar()
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                setFecha(dteFecha);
            }

            grdMateriales.DataSource = lstRequerimientoMaterialesDetalle;


        }
        public void CargarControles()
        {

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
            using (frmManteRequerimientoMaterielaesDetalle frm = new frmManteRequerimientoMaterielaesDetalle())
            {
                frm.SetInsert();
                frm.IcodRubros = IcodRubros;
                frm.lstRequerimientoMaterialesDetalle = lstRequerimientoMaterialesDetalle;
                frm.txtDescripcion.Enabled = false;
                frm.txtItem.Text = (lstRequerimientoMaterialesDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstRequerimientoMaterialesDetalle.Count + 1);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstRequerimientoMaterialesDetalle = frm.lstRequerimientoMaterialesDetalle;
                    viewMateriales.RefreshData();
                    viewMateriales.MoveLast();

                }
            }
        }



        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (Convert.ToInt32(txtNumero.Text) == 0)
                {
                    oBase = txtNumero;
                    throw new ArgumentException("Ingrese Nro. de Requerimiento");
                }
                if ((txtObservaciones.Text) == "")
                {
                    oBase = txtObservaciones;
                    throw new ArgumentException("Ingrese Observaciones");
                }

                oBe.rqmc_numero_req_material = txtNumero.Text;
                oBe.rqmc_sfecha_req_material = Convert.ToDateTime(dteFecha.Text);
                oBe.tablc_iid_situación_hc = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.tablc_iid_tipo_requerimiento = Convert.ToInt32(lkpTipo.EditValue);
                oBe.pryc_icod_proyecto = Convert.ToInt32(bteProyecto.Tag);
                oBe.rqmc_vdescripcion = txtObservaciones.Text;
                oBe.rqmc_flag_estado = true;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.rqmc_icod_requerimiento_materiales = new BVentas().insertarRequerimientoMateriales(oBe, lstRequerimientoMaterialesDetalle);

                }
                else
                {
                    new BVentas().modificarRequerimientoMateriales(oBe, lstRequerimientoMaterialesDetalle, lstDelete);
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
                    MiEvento(oBe.rqmc_icod_requerimiento_materiales);
                    Close();
                }
            }
        }

        private void FrmManteFactura_Load(object sender, EventArgs e)
        {
            cargar();
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(74), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpTipo, new BGeneral().listarTablaRegistro(73).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

        }
        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ERequerimientoMaterialesDetalle obe = (ERequerimientoMaterialesDetalle)viewMateriales.GetRow(viewMateriales.FocusedRowHandle);
            if (obe == null)
                return;
            modificarItem();
        }

        private void modificarItem()
        {
            ERequerimientoMaterialesDetalle obe = (ERequerimientoMaterialesDetalle)viewMateriales.GetRow(viewMateriales.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteRequerimientoMaterielaesDetalle frm = new frmManteRequerimientoMaterielaesDetalle())
            {
                frm.Obe = obe;
                frm.lstRequerimientoMaterialesDetalle = lstRequerimientoMaterialesDetalle;
                frm.SetModify();
                frm.txtDescripcion.Enabled = false;
                frm.txtItem.Text = String.Format("{0:000}", obe.rqmd_vcodigo_item_requerim);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstRequerimientoMaterialesDetalle = frm.lstRequerimientoMaterialesDetalle;
                    viewMateriales.RefreshData();
                    viewMateriales.MoveLast();
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ERequerimientoMaterialesDetalle obe = (ERequerimientoMaterialesDetalle)viewMateriales.GetRow(viewMateriales.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstRequerimientoMaterialesDetalle.Remove(obe);
            viewMateriales.RefreshData();
        }

        private void txtSerie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }


        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ERequerimientoMaterialesDetalle obe = (ERequerimientoMaterialesDetalle)viewMateriales.GetRow(viewMateriales.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstRequerimientoMaterialesDetalle.Remove(obe);
            viewMateriales.RefreshData();

        }

        private void bteProyecto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProyecto();
        }
        private void ListarProyecto()
        {
            using (frmListarProyectos frm = new frmListarProyectos())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteProyecto.Tag = frm._Be.pryc_icod_proyecto;
                    bteProyecto.Text = frm._Be.pryc_vcorrelativo;
                    txtHCN.Text = frm._Be.NumHojaCosto;
                    IcodRubros = frm._Be.hjcc_icod_hoja_costo;

                }
            }
        }


    }
}