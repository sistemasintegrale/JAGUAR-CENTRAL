using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmMantePercepcion : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        /*--------------*/
        public EPercepcion Obe = new EPercepcion();
        public List<EPercepcionDet> lstDetalle = new List<EPercepcionDet>();
        public List<EPercepcionDet> lstDelete = new List<EPercepcionDet>();
        private List<ETipoCambio> lstTipoCambio = new List<ETipoCambio>();

        public frmMantePercepcion()
        {
            InitializeComponent();
        }

        private void frmMantePercepcion_Load(object sender, EventArgs e)
        {
            cargar();
            grdDetalle.DataSource = lstDetalle;
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                setFecha(dtFecha);
                getTipoCambio();
            }
        }

        private void cargar()
        {
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lstTipoCambio = new BAdministracionSistema().listarTipoCambio();
        }

        private void setFecha(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
        }

        public void getTipoCambio()
        {
            var Lista = lstTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dtFecha.EditValue).ToShortDateString()).ToList();
            Lista.ForEach(obe =>
            {
                txtTipoCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
            });
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
            txtSerie.Enabled = !Enabled;
            txtCorrelativo.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            mnu.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.View)
                enableControls(false);

        }

        private void enableControls(bool Enabled)
        {
            txtSerie.Enabled = Enabled;
            txtCorrelativo.Enabled = Enabled;
            dtFecha.Enabled = Enabled;
            bteProveedor.Enabled = Enabled;
            lkpMoneda.Enabled = Enabled;
            txtTipoCambio.Enabled = Enabled;
        }

        public void setValues()
        {
            bteProveedor.Text = Obe.strProveedor;
            bteProveedor.Tag = Obe.proc_icod_proveedor;
            txtSerie.Text = Obe.percc_vnro_percepcion.Substring(0, 3);
            txtCorrelativo.Text = Obe.percc_vnro_percepcion.Substring(3, Obe.percc_vnro_percepcion.Trim().Length - 3);
            dtFecha.EditValue = Obe.percc_sfecha_percepcion;
            lkpMoneda.EditValue = Obe.tablc_iid_tipo_moneda;
            //
            txtMontoCobrado.Text = Obe.percc_nmonto_cobrado.ToString();
            txtMontoPercibido.Text = Obe.percc_nmonto_percibido.ToString();
            txtTipoCambio.Text = Obe.percc_tipo_cambio.ToString();
            /**/
            lstDetalle = new BCompras().listarPercepcionDet(Obe.percc_icod_percepcion);
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

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                if (bteProveedor.Tag == null)
                {
                    oBase = bteProveedor;
                    throw new ArgumentException("Seleccione proveedor");
                }

                if (String.IsNullOrWhiteSpace(txtSerie.Text))
                {
                    oBase = txtSerie;
                    throw new ArgumentException("Ingrese el Nro. de Percepción");
                }



                Obe.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
                Obe.percc_vnro_percepcion = txtSerie.Text + txtCorrelativo.Text;
                Obe.percc_sfecha_percepcion = Convert.ToDateTime(dtFecha.EditValue);
                Obe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                Obe.percc_nmonto_cobrado = Convert.ToDecimal(txtMontoCobrado.Text);
                Obe.percc_nmonto_percibido = Convert.ToDecimal(txtMontoPercibido.Text);
                Obe.percc_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                Obe.tablc_iid_situacion = 1;// 1: Generado, 2: Anulado
                /**/
                Obe.strProveedor = bteProveedor.Text;
                /**/
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.percc_icod_percepcion = new BCompras().insertarPercepcionCab(Obe, lstDetalle);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BCompras().modificarPercepcionCab(Obe, lstDetalle, lstDelete);
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
                    this.MiEvento(Obe.percc_icod_percepcion);
                    this.Close();
                }
            }
        }

        private void listarProveedor()
        {
            FrmListarProveedor frm = new FrmListarProveedor();
            frm.Carga();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bteProveedor.Tag = frm._Be.iid_icod_proveedor;
                bteProveedor.Text = frm._Be.vnombrecompleto;
            }
        }

        private void dtFecha_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
                getTipoCambio();
        }

        private void setTotal()
        {
            txtMontoCobrado.Text = lstDetalle.Sum(x => x.percd_nmonto_doc).ToString();
            txtMontoPercibido.Text = lstDetalle.Sum(x => x.percd_nmonto_percibido_doc).ToString();
        }

        private void nuevo()
        {
            BaseEdit oBase = null;
            try
            {
                if (Convert.ToInt32(bteProveedor.Tag) == 0)
                {
                    oBase = bteProveedor;
                    throw new ArgumentException("Seleccione proveedor");
                }
                using (frmMantePercepcionDetalle frm = new frmMantePercepcionDetalle())
                {
                    frm.SetInsert();
                    frm.id_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                    frm.id_proveedor = Convert.ToInt32(bteProveedor.Tag);
                    frm.lstDetalle = lstDetalle;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        setTotal();
                        viewDetalle.RefreshData();
                        viewDetalle.Focus();
                    }
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
            }
        }

        private void modificar()
        {
            EPercepcionDet oBe_ = (EPercepcionDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (oBe_ == null)
                return;
            using (frmMantePercepcionDetalle frm = new frmMantePercepcionDetalle())
            {
                frm.oBe = oBe_;
                frm.SetModify();
                frm.id_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                frm.id_proveedor = Convert.ToInt32(bteProveedor.Tag);
                frm.lstDetalle = lstDetalle;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    setTotal();
                    viewDetalle.RefreshData();
                    viewDetalle.Focus();
                }
            }
        }

        private void eliminar()
        {
            EPercepcionDet oBe_ = (EPercepcionDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (oBe_ == null)
                return;
            lstDelete.Add(oBe_);
            lstDetalle.Remove(oBe_);
            viewDetalle.RefreshData();
            setTotal();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void bteProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProveedor();
        }
    }
}