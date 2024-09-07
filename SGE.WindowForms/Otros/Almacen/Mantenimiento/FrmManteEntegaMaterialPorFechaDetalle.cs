using DevExpress.XtraEditors;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class FrmManteEntegaMaterialPorFechaDetalle : DevExpress.XtraEditors.XtraForm
    {
        public int icod_item_requerimiento_material;//referencia
        //public int icod_almacen;
        public decimal cantida_pedida;
        public decimal cantidad_entregada;
        public decimal stock;
        public List<EOrdenPedidoCab> oPedido;
        public EEntregaMaterialDet _BE = new EEntregaMaterialDet();
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
        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);


        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            txtCantidadRequerida.Text = cantida_pedida.ToString();
            txtTotalCantidadEntregada.Text = cantidad_entregada.ToString();
            txtStock.Text = stock.ToString();
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;

        }
        public FrmManteEntegaMaterialPorFechaDetalle()
        {
            InitializeComponent();
        }
        public void setValues()
        {


        }

        private void FrmNotaSalidaDetalle_Load(object sender, EventArgs e)
        {

            setFecha();
        }
        private void setFecha()
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                dteFecha.EditValue = DateTime.Now;
            else
                dteFecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
        }
        private void btnAgregar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AgregarDetalle();
        }
        private void AgregarDetalle()
        {
            bool flag = true;
            BaseEdit oBase = null;
            try
            {

                if (Convert.ToDecimal(txtCantidad.Text) < 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("La Cantidad Por Entregar debe ser Mayor Igual a Cero");
                }
                if (Convert.ToDecimal(txtCantidad.Text) > Convert.ToDecimal(txtStock.Text))
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("La Cantidad Por Entregar debe ser Menor Igual que el stock");
                }
                _BE.rqmad_icod_item_requerimiento_material = icod_item_requerimiento_material;
                _BE.rqmed_ncantidad_entregada = Convert.ToDecimal(txtCantidad.Text);
                _BE.rqmed_sfecha_entrega = Convert.ToDateTime(dteFecha.Text);
                _BE.intTipoOperacion = 1;
                this.DialogResult = DialogResult.OK;
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
            }
            finally
            {

            }
        }
        private void btnsalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

    }
}