using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.WindowForms.Otros.Ventas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
namespace SGE.WindowForms.Otros.Operaciones
{
    public partial class frmManteProyectos : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteVendedor));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EProyectos Obe = new EProyectos();
        public List<EProyectos> lstPersonal = new List<EProyectos>();
        public int Almacen = 0;
        public int Proyecto = 0;

        public frmManteProyectos()
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
            dteFechaInicio.Enabled = !Enabled;
            dteFechaEntrega.Enabled = !Enabled;
            lkpEstado.Enabled = !Enabled;
            txtDescripcion.Enabled = !Enabled;
            txtRentabilidad.Enabled = !Enabled;
            lkpActaEntrega.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;

        }
        public void setValues()
        {
            txtSerie.Text = Obe.pryc_ianio.ToString();
            txtNumero.Text = Obe.pryc_vcorrelativo;
            dteFechaInicio.EditValue = Obe.pryc_sfecha_inicio;
            dteFechaEntrega.EditValue = Obe.pryc_sfecha_emtrega;
            lkpEstado.EditValue = Obe.pryc_iestado;
            txtDescripcion.Text = Obe.pryc_vdescripcion;
            txtRentabilidad.Text = Obe.pryc_nrentabilidad.ToString();
            lkpActaEntrega.EditValue = Obe.pryc_icod_acta_entrega;
            txtDireccion.Text = Obe.pryc_vdireccion_prov;
            txtRUC.Text = Obe.RUC;
            bteCliente.Tag = Obe.pryc_icod_cliente;
            bteCliente.Text = Obe.NomCliente;

            bteCCosto.Tag = Obe.pryc_icod_ccosto;
            bteCCosto.Text = Obe.CentroCosto;

            bteAlmacen.Tag = Obe.almac_icod_almacen;
            bteAlmacen.Text = Obe.StrAlmacen;
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
                if (Convert.ToInt32(txtNumero.Text) == 0)
                {
                    oBase = txtNumero;
                    throw new ArgumentException("Ingrese Nro. de Proyecto");
                }
                if (String.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese Descripcion");
                }
                if (String.IsNullOrWhiteSpace(txtDireccion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese Direccion de Obra");
                }
                if (Convert.ToInt32(bteCliente.Tag) == 0)
                {
                    oBase = bteCliente;
                    throw new ArgumentException("Seleccione Cliente");
                }
                if (Convert.ToDecimal(txtRentabilidad.Text) == 0)
                {
                    oBase = txtRentabilidad;
                    throw new ArgumentException("Ingrese Rentabilidad");
                }

                Obe.pryc_ianio = Convert.ToInt32(txtSerie.Text);
                Obe.pryc_vcorrelativo = txtSerie.Text + txtNumero.Text;
                Obe.pryc_sfecha_inicio = Convert.ToDateTime(dteFechaInicio.EditValue);
                Obe.pryc_iestado = Convert.ToInt32(lkpEstado.EditValue);
                Obe.pryc_vdescripcion = txtDescripcion.Text;
                Obe.pryc_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                Obe.almac_icod_almacen = Convert.ToInt32(bteAlmacen.Tag);
                Obe.pryc_vdireccion_prov = txtDireccion.Text;
                Obe.pryc_nrentabilidad = Convert.ToDecimal(txtRentabilidad.Text);
                Obe.pryc_sfecha_emtrega = Convert.ToDateTime(dteFechaEntrega.EditValue);
                Obe.pryc_icod_acta_entrega = Convert.ToInt32(lkpActaEntrega.EditValue);
                Obe.pryc_icod_ccosto = Convert.ToInt32(bteCCosto.Tag);
                Obe.pryc_vdireccion_prov = txtDireccion.Text;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.pryc_flag_estado = true;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.pryc_icod_proyecto = new BVentas().insertarProyectos(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    Obe.almac_icod_almacen_2 = Almacen;
                    new BVentas().modificarProyectos(Obe);
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
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(Obe.pryc_icod_proyecto);
                    this.Close();
                }
            }
        }

        private bool verificarNombre(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstPersonal.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstPersonal.Where(x => x.pryc_vdescripcion.Replace(" ", "").Trim() == strNombre.Replace(" ", "").Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstPersonal.Where(x => x.pryc_vdescripcion.Replace(" ", "").Trim() == strNombre.Replace(" ", "").Trim() && x.pryc_icod_proyecto != Obe.pryc_icod_proyecto).ToList().Count > 0)
                            exists = true;
                    }
                }
                return exists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool verificarCodigo(string strCodigo)
        {
            try
            {
                bool exists = false;
                if (lstPersonal.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstPersonal.Where(x => x.pryc_vcorrelativo == (strCodigo)).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstPersonal.Where(x => x.pryc_vcorrelativo == (strCodigo) && x.pryc_icod_proyecto != Obe.pryc_icod_proyecto).ToList().Count > 0)
                            exists = true;
                    }
                }
                return exists;
            }
            catch (Exception ex)
            {
                throw ex;
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

        private void frmMantePersonal_Load(object sender, EventArgs e)
        {
            dteFechaInicio.EditValue = DateTime.Now;
            dteFechaEntrega.EditValue = DateTime.Now;
            BSControls.LoaderLook(lkpEstado, new BGeneral().listarTablaRegistro(64), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpActaEntrega, new BGeneral().listarTablaRegistro(65), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

        }
        private void GetOCL()
        {
            string correlativo = new BAdministracionSistema().ObtenerCorrelativoProyecto(Convert.ToInt32(txtSerie.Text));
            txtNumero.Text = correlativo;

        }
        private void lkpArea_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void bteCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCliente();
        }
        private void listarCliente()
        {
            try
            {
                using (FrmListarCliente frm = new FrmListarCliente())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteCliente.Tag = frm._Be.cliec_icod_cliente;
                        bteCliente.Text = frm._Be.cliec_vnombre_cliente;
                        //txtDireccion.Text = frm._Be.cliec_vdireccion_cliente;
                        txtRUC.Text = frm._Be.cliec_cruc;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bteCosto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCentroCosto();
        }
        private void ListarCentroCosto()
        {
            using (frmListarCentroCostoProyectos Ccosto = new frmListarCentroCostoProyectos())
            {

                if (Ccosto.ShowDialog() == DialogResult.OK)
                {
                    bteCCosto.Text = Ccosto._Be.cecoc_vcodigo_centro_costo;//cecoc_ccodigo_centro_costo - centro_costo
                    bteCCosto.Tag = Ccosto._Be.cecoc_icod_centro_costo;//cecoc_icod_centro_costo (correlativo) - centro_costo

                }
            }
        }



        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarAlmacen();
        }
        private void ListarAlmacen()
        {
            try
            {
                using (frmListarAlmacenProyectos frm = new frmListarAlmacenProyectos())
                {
                    frm.Proyecto = Proyecto;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteAlmacen.Tag = frm._Be.almac_icod_almacen;
                        bteAlmacen.Text = frm._Be.almac_vdescripcion;

                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void txtSerie_EditValueChanged(object sender, EventArgs e)
        {
            GetOCL();
        }

        private void bteAlmacen_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}