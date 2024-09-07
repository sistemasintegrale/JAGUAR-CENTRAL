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
    public partial class frmRegistroImpFactura : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        /*--------------*/
        public EImportacionFactura Obe = new EImportacionFactura();
        public List<EPercepcionDet> lstDetalle = new List<EPercepcionDet>();
        public List<EPercepcionDet> lstDelete = new List<EPercepcionDet>();
        private List<ETipoCambio> lstTipoCambio = new List<ETipoCambio>();
        public List<EImportacionFactura> lstImpFactura = new List<EImportacionFactura>();
        public int codProvee = 0;//------
        public int codFactComp = 0;//------CODFACT_COMPRA
        List<EImportacionFactura> lstImpFacturaDet = new List<EImportacionFactura>();
        public int codImp = 0;


        public frmRegistroImpFactura()
        {
            InitializeComponent();
        }

        private void frmMantePercepcion_Load(object sender, EventArgs e)
        {
            cargar();



            if (Status == BSMaintenanceStatus.CreateNew)
            {
                //setFecha(dtFecha);
                getTipoCambio();
            }
        }

        private void cargar()
        {
            //List<EProveedor> LstProveedor = new List<EProveedor>();
            //LstProveedor = new BCompras().ListarProveedor();
            BSControls.LoaderLook(lkpProveedor, new BCompras().ListarProveedor(), "vnombrecompleto", "iid_icod_proveedor", true);
            //BSControls.LoaderLook(lkpFactura, new BCompras().FILTAR_FAC_IMP_X_PROVEE(Convert.ToInt32(lkpProveedor.EditValue)), "fcoc_icod_doc", "fcoc_icod_doc", true);

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
            lkpProveedor.Enabled = !Enabled;
            lkpFactura.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            mnu.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.View)
                enableControls(false);

        }

        private void enableControls(bool Enabled)
        {

        }

        public void setValues()
        {

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
                //if (bteProveedor.Tag == null)
                //{
                //    oBase = bteProveedor;
                //    throw new ArgumentException("Seleccione proveedor");
                //}







                //Obe.impc_icod_importacion = Convert.ToInt32(lkpProveedor.EditValue);
                //Obe.fcoc_icod_doc = Convert.ToInt32(lkpFactura.EditValue);
                Obe.impc_icod_importacion = codImp;
                Obe.fcoc_icod_doc = Convert.ToInt32(bteFacturaCompra.Tag);
                Obe.impd1_flag_estado = true;
                Obe.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);

                /**/


                /**/
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.impd1_icod_import_factura = new BCompras().InsertarImportacionFactura(Obe);
                    new BCompras().ACTUALIZAR_FAC_COMPRA_IMP_FACT(Convert.ToInt32(Obe.fcoc_icod_doc), Convert.ToInt32(Obe.impd1_icod_import_factura), Convert.ToInt32(Obe.impc_icod_importacion));

                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    //new BCompras().modificarPercepcionCab(Obe, lstDetalle, lstDelete);
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
                    this.MiEvento(Convert.ToInt32(Obe.impd1_icod_import_factura));
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
                codProvee = Convert.ToInt32(bteProveedor.Tag);
            }
        }

        private void dtFecha_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
                getTipoCambio();
        }

        private void setTotal()
        {
            //txtMontoCobrado.Text = lstDetalle.Sum(x => x.percd_nmonto_doc).ToString();
            //txtMontoPercibido.Text = lstDetalle.Sum(x => x.percd_nmonto_percibido_doc).ToString();
        }






        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

        }

        private void lkpProveedor_EditValueChanged(object sender, EventArgs e)
        {
            //BSControls.LoaderLook(lkpFactura, new BCompras().listarFacCompra(2016).Where(x => x.proc_icod_proveedor == Convert.ToInt32(lkpProveedor.EditValue)), "fcoc_num_doc", "fcoc_icod_doc", true);

            BSControls.LoaderLook(lkpFactura, new BCompras().FILTAR_FAC_IMP_X_PROVEE(Convert.ToInt32(lkpProveedor.EditValue)), "fcoc_num_doc", "fcoc_icod_doc", true);
        }

        private void bteProveedor_Click(object sender, EventArgs e)
        {

            listarProveedor();
        }

        private void bteFacturaCompra_Click(object sender, EventArgs e)
        {
            try
            {
                FrmListarFactCompraXprovee frm = new FrmListarFactCompraXprovee();
                frm.codProve = codProvee;
                frm.Text = String.Format("Factura del Proveedor : " + bteProveedor.Text);
                frm.Carga();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteFacturaCompra.Tag = frm._Be.fcoc_icod_doc;
                    bteFacturaCompra.Text = frm._Be.fcoc_num_doc;
                    //codFactComp=frm._Be.

                }


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        public void ActualizarCostos()
        {
            decimal Factor = 0;
            decimal Total = 0;
            decimal FCob = 0;
            decimal UnitCosto = 0;
            decimal TotCosto = 0;

            lstImpFacturaDet = new BCompras().ListarImportacionFactura(codImp);
            lstImpFacturaDet.ForEach(x =>
            {
                List<EFacturaCompraDet> ListarDetalle = new List<EFacturaCompraDet>();
                ListarDetalle = new BCompras().listarFacCompraDet(Convert.ToInt32(x.fcoc_icod_doc));
                ListarDetalle.ForEach(xd =>
                {
                    List<EImportacionConceptos> LstImpConceptos = new List<EImportacionConceptos>();
                    LstImpConceptos = new BCompras().ListarImportacionConceptos(codImp);
                    FCob = LstImpConceptos.Where(su => Convert.ToInt32(su.cpn_icod_concepto_nacional) == 1 && Convert.ToInt32(su.cpnd_icod_detalle_nacional) == 1).ToList().Sum(su => Convert.ToDecimal(su.impd_nmonto_concepto_dol));
                    Total = LstImpConceptos.Sum(s => Convert.ToDecimal(s.impd_nmonto_concepto_dol));
                    Factor = Total / FCob;
                    UnitCosto = xd.fcod_nmonto_unit * Factor;
                    TotCosto = xd.fcod_nmonto_total * Factor;
                    EFacturaCompraDet ObeFD = new EFacturaCompraDet();
                    ObeFD.fcoc_icod_doc = Convert.ToInt32(x.fcoc_icod_doc);
                    ObeFD.fcod_nmonto_unit_costo = UnitCosto;
                    ObeFD.fcod_nmonto_total_costo = TotCosto;
                    new BCompras().ActualizarCostos(Convert.ToInt32(xd.fcod_icod_doc), UnitCosto, TotCosto);
                });


            });
        }
        public void ActualizarImportacion()
        {
            Decimal? MontoDolares = 0;
            Decimal? MontoSoles = 0;
            List<EImportacionConceptos> LstImpConceptos = new List<EImportacionConceptos>();
            LstImpConceptos = new BCompras().ListarImportacionConceptos(codImp);

            MontoDolares = LstImpConceptos.Sum(x => x.impd_nmonto_concepto_dol);
            MontoSoles = LstImpConceptos.Sum(x => x.impd_nmonto_concepto_sol);
            new BCompras().ACTUALIZAR_IMPORTACION_MONTOS(codImp, Convert.ToDecimal(MontoSoles), Convert.ToDecimal(MontoDolares));
        }
    }
}