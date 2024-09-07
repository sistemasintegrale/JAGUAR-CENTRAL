using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Ventas
{
    public partial class FrmManteDireccionCliente : DevExpress.XtraEditors.XtraForm
    {
        ComponentResourceManager resources = new ComponentResourceManager(typeof(FrmManteCliente));
        public delegate void DelegadoMensaje(int intCod);
        public event DelegadoMensaje MiEvento;

        public FrmManteDireccionCliente()
        {
            KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();
        }

        public string habilitar = "";
        public List<EUbicacion> lUbicacion = null;
        public int? anac_icod_analitica;
        public List<EDireccionCliente> oDetail;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        private BVentas Obl;
        public int Correlative = 0;
        public int cliec_icod_cliente;
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
            txtDireccion.Enabled = !Enabled;



        }

        private void clearControl()
        {
            txtDireccion.Text = string.Empty;
        }

        private void FrmManteCliente_Load(object sender, EventArgs e)
        {
            lUbicacion = new BVentas().ListarUbicacion();
            cargar();
        }

        private void cargar()
        {

        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }


        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            EDireccionCliente oBe = new EDireccionCliente();
            Obl = new BVentas();
            try
            {


                //if (LkpVendedor.EditValue == null)
                //{
                //    oBase = LkpVendedor;
                //    throw new ArgumentException("Seleccionar Vendedor");
                //}
                if (string.IsNullOrEmpty(txtDireccion.Text))
                {
                    oBase = txtDireccion;
                    throw new ArgumentException("Ingresar Dirección");
                }
                oBe.cliec_icod_cliente = cliec_icod_cliente;
                oBe.dc_vdireccion = string.IsNullOrEmpty(txtDireccion.Text) ? null : txtDireccion.Text;



                if (Status == BSMaintenanceStatus.CreateNew)
                {

                    Correlative = Obl.insertarDireccionCliente(oBe);
                }
                else
                {

                    oBe.cliec_icod_cliente = Correlative;
                    Obl.modificarDireccionCliente(oBe);
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
                    XtraMessageBox.Show("No se pudo realizar la operación", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Flag = false;

            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(Correlative);
                    this.Close();
                }
            }
        }

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetSave();
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }






        private void LkpDistrito_EditValueChanged(object sender, EventArgs e)
        {
            // BSControls.LoaderLook(lkpMercado, new BPuntoCompra().ListarPuntoCompraXDistrito(Convert.ToInt32(LkpDistrito.EditValue)), "pcomc_vdescripcion", "pcomc_icod_pcompra", false);
        }


        private void txtnumerodias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }




    }
}