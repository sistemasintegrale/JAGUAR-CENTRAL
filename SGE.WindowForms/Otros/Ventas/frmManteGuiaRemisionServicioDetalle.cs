using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;
using static SGE.Common.Codigos;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteGuiaRemisionServicioDetalle : XtraForm
    {
        public List<EGuiaRemisionMPDet> lstMPDetalle = new List<EGuiaRemisionMPDet>();
        public EGuiaRemisionDet oBe = new EGuiaRemisionDet();
        public EGuiaRemision oBeCab = new EGuiaRemision();
        public EGuiaRemisionMPDet oBeMt = new EGuiaRemisionMPDet();


        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        internal List<EGuiaRemisionDet> lstDetalle = new List<EGuiaRemisionDet>();
        internal int TipoGuia;

        public frmManteGuiaRemisionServicioDetalle()
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
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtCantidad.Text = "1.00";
            }
        }

        public void SetInsert() => Status = BSMaintenanceStatus.CreateNew;

        public void SetCancel() => Status = BSMaintenanceStatus.View;

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            setValues();
        }

        private void setValues()
        {
            if (TipoGuia == (int)TipoGuiaRemision.ProductoTerminado)
            {
                txtCantidad.Text = oBe.dremc_ncantidad_producto.ToString();
                txtObservaciones.Text = oBe.dremc_vdescripcion_prod;
                lkpUnidadMedida.EditValue = oBe.unidc_icod_unidad_medida;
                txtItem.Text = oBe.dremc_inro_item.ToString();
            }
            else
            {
                txtCantidad.Text = oBeMt.dremc_ncantidad_producto.ToString();
                txtObservaciones.Text = oBeMt.dremc_vobservaciones;
                lkpUnidadMedida.EditValue = oBeMt.unidc_icod_unidad_medida;
                txtItem.Text = oBeMt.dremc_inro_item.ToString();
            }


        }

        private void setSave()
        {
            BaseEdit oBase = null;
            try
            {



                if (string.IsNullOrEmpty(txtObservaciones.Text))
                {
                    oBase = txtObservaciones;
                    throw new ArgumentException("Ingrese descripción de servicio");
                }
                oBe.dremc_inro_item = Convert.ToInt16(txtItem.Text);
                oBe.dremc_ncantidad_producto = Convert.ToDecimal(txtCantidad.Text);
                oBe.strCodProducto = "SERV";
                oBe.strDesUM = lkpUnidadMedida.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.unidc_icod_unidad_medida = Convert.ToInt32(lkpUnidadMedida.EditValue);
                oBe.dremc_itipo_producto = (int)TipoProducto.Servicio;
                oBe.dremc_vobservaciones = "";
                oBe.strCodProducto = "SERVICIO";
                oBe.strDesUM = "SRV";
                oBe.strDesProducto = txtObservaciones.Text;
                oBe.dremc_vdescripcion_prod = txtObservaciones.Text;
                oBe.dremc_vcodigo_extremo_prod = "SERVICIO";
                oBe.strAbreUM = "SRV";

                #region GuiaRemisionElectronicaDet
                oBe.electronicaDet.UnidadMedida = "ZZ";
                oBe.electronicaDet.Cantidad = Convert.ToDecimal(txtCantidad.Text);
                oBe.electronicaDet.Descripcion = oBe.strDesProducto;
                oBe.electronicaDet.CodigoItem = "";
                oBe.electronicaDet.Correlativo = oBe.dremc_inro_item;
                oBe.electronicaDet.PesoItem = 0;
                oBe.electronicaDet.UM = lkpUnidadMedida.Text;
                #endregion

                oBeMt.dremc_inro_item = Convert.ToInt16(txtItem.Text);
                oBeMt.dremc_ncantidad_producto = Convert.ToDecimal(txtCantidad.Text);
                oBeMt.strCodProducto = "SERV";
                oBeMt.strDesUM = lkpUnidadMedida.Text;
                oBeMt.intUsuario = Valores.intUsuario;
                oBeMt.strPc = WindowsIdentity.GetCurrent().Name;
                oBeMt.unidc_icod_unidad_medida = Convert.ToInt32(lkpUnidadMedida.EditValue);
                oBeMt.dremc_itipo_producto = (int)TipoProducto.Servicio;
                oBeMt.dremc_vobservaciones = txtObservaciones.Text;
                oBeMt.strCodProducto = "SERVICIO";
                oBeMt.strDesUM = "SRV";
                oBeMt.strDesProducto = txtObservaciones.Text;
                oBeMt.strAbreUM = "SRV";


                #region GuiaRemisionElectronicaDet
                oBeMt.electronicaDet.UnidadMedida = "ZZ";
                oBeMt.electronicaDet.Cantidad = Convert.ToDecimal(txtCantidad.Text);
                oBeMt.electronicaDet.Descripcion = oBe.strDesProducto;
                oBeMt.electronicaDet.CodigoItem = "";
                oBeMt.electronicaDet.Correlativo = oBe.dremc_inro_item;
                oBeMt.electronicaDet.PesoItem = 0;
                oBeMt.electronicaDet.UM = lkpUnidadMedida.Text;
                #endregion

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.intTipoOperacion = 1;
                    lstDetalle.Add(oBe);
                    lstMPDetalle.Add(oBeMt);
                }
                else
                {
                    if (oBe.intTipoOperacion != 1)
                        oBe.intTipoOperacion = 2;
                    if (oBeMt.intTipoOperacion != 1)
                        oBeMt.intTipoOperacion = 2;
                }

                this.DialogResult = DialogResult.OK;
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




        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => setSave();
        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => Close();
        private void txtObservaciones_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.None);
        }



        private void frmManteGuiaRemisionDetalle_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpUnidadMedida, new BAlmacen().listarUnidadMedida(), "unidc_vdescripcion", "unidc_icod_unidad_medida", true);
            lkpUnidadMedida.EditValue = (int)UnidadMedida.Servicio;
        }




    }
}