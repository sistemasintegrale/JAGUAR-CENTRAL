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

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteRetencion : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        /*--------------*/
        public ERetencion Obe = new ERetencion();
        public List<ERetencionDet> lstDetalle = new List<ERetencionDet>();
        public List<ERetencionDet> lstDelete = new List<ERetencionDet>();
        private List<ETipoCambio> lstTipoCambio = new List<ETipoCambio>();
        public int mes = 0;

        public frmManteRetencion()
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
                dteFechaPago.EditValue = DateTime.Now;
            }
        }

        private void cargar()
        {
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
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
            bteCliente.Enabled = Enabled;
            lkpMoneda.Enabled = Enabled;
            txtTipoCambio.Enabled = Enabled;
        }

        public void setValues()
        {
            bteCliente.Text = Obe.strCliente;
            bteCliente.Tag = Obe.proc_icod_cliente;
            txtSerie.Text = Obe.retc_vnumero_comprob_reten.Substring(0, 4);
            txtCorrelativo.Text = Obe.retc_vnumero_comprob_reten.Substring(4, Obe.retc_vnumero_comprob_reten.Trim().Length - 4);
            dtFecha.EditValue = Obe.retc_sfec_comprob_reten;
            lkpMoneda.EditValue = Obe.tablc_iid_moneda;

            //
            txtTotal.Text = Obe.retc_nmto_total_pago.ToString();
            txtRetencion.Text = Obe.retc_nmto_total_retencion.ToString();
            txtTipoCambio.Text = Obe.retc_nmto_tipo_cambio.ToString();
            /**/
            lstDetalle = new BVentas().listarRetencionDet(Obe.retc_icod_comprobante_retencion);
            grdDetalle.DataSource = lstDetalle;
            setTotal();
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
                if (bteCliente.Tag == null)
                {
                    oBase = bteCliente;
                    throw new ArgumentException("Seleccione proveedor");
                }

                if (String.IsNullOrWhiteSpace(txtSerie.Text))
                {
                    oBase = txtSerie;
                    throw new ArgumentException("Ingrese el Nro. de Percepción");
                }

                Obe.proc_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                Obe.retc_vnumero_comprob_reten = txtSerie.Text + txtCorrelativo.Text;
                Obe.retc_sfec_comprob_reten = Convert.ToDateTime(dtFecha.Text);
                Obe.tablc_iid_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                Obe.retc_nmto_total_pago = Convert.ToDecimal(txtTotal.Text);
                Obe.retc_nmto_total_retencion = Convert.ToDecimal(txtRetencion.Text);
                Obe.retc_nmto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                Obe.tablc_iid_situacion = 1;// 1: Generado, 2: Anulado
                Obe.anioc_iid_anio = Parametros.intEjercicio;
                Obe.mesec_iid_mes = mes;
                Obe.retc_sfecha_pago = Convert.ToDateTime(dteFechaPago.EditValue);
                /**/
                Obe.strCliente = bteCliente.Text;
                /**/
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.retc_icod_comprobante_retencion = new BVentas().insertarRetencionCab(Obe, lstDetalle);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().modificarRetencionCab(Obe, lstDetalle, lstDelete);
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
                    this.MiEvento(Obe.retc_icod_comprobante_retencion);
                    this.Close();
                }
            }
        }

        private void listarProveedor()
        {
            FrmListarCliente frm = new FrmListarCliente();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bteCliente.Tag = frm._Be.cliec_icod_cliente;
                bteCliente.Text = frm._Be.cliec_vnombre_cliente;
            }
        }

        private void dtFecha_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
                getTipoCambio();
        }

        private void setTotal()
        {
            txtTotal.Text = lstDetalle.Sum(x => x.retd_nmto_total_doc).ToString();
            txtPago.Text = lstDetalle.Sum(x => x.retd_nmto_pago_doc).ToString();
            txtRetencion.Text = lstDetalle.Sum(x => x.retd_nmto_retencion).ToString();
        }

        private void nuevo()
        {
            BaseEdit oBase = null;
            try
            {
                if (Convert.ToInt32(bteCliente.Tag) == 0)
                {
                    oBase = bteCliente;
                    throw new ArgumentException("Seleccione cliente");
                }
                using (frmManteRetencionDetalle frm = new frmManteRetencionDetalle())
                {
                    frm.SetInsert();
                    frm.id_tipo_moneda_Cab = Convert.ToInt32(lkpMoneda.EditValue);
                    frm.TipoCambioCab = Convert.ToDecimal(txtTipoCambio.Text);
                    frm.id_cliente = Convert.ToInt32(bteCliente.Tag);
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
            ERetencionDet oBe_ = (ERetencionDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (oBe_ == null)
                return;
            using (frmManteRetencionDetalle frm = new frmManteRetencionDetalle())
            {
                frm.oBe = oBe_;
                frm.SetModify();
                frm.id_tipo_moneda_Cab = Convert.ToInt32(lkpMoneda.EditValue);
                frm.id_cliente = Convert.ToInt32(bteCliente.Tag);
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
            ERetencionDet oBe_ = (ERetencionDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
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