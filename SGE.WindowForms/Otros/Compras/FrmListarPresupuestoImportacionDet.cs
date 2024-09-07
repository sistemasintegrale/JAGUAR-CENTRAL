using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmListarPresupuestoImportacionDet : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        public EPresupuestoNacionalDetalle movDetalle = new EPresupuestoNacionalDetalle();

        List<EPresupuestoNacional> mLista = new List<EPresupuestoNacional>();
        public EPresupuestoNacional _Be { get; set; }
        public bool bNoExceptuadosRetencion = false;
        public int proc_icod_proveedor = 0;

        #endregion
        public int IdPresupuestoNacional = 0;
        #region "Eventos"
        private List<EFacturaCompra> lstFacCompra = new List<EFacturaCompra>();
        public List<PresupuestoNacionalDetalleBE> mListaPresupuestoNacionalDetalleOrigen = new List<PresupuestoNacionalDetalleBE>();
        public FrmListarPresupuestoImportacionDet()
        {
            InitializeComponent();
        }

        private void FrmListarProveedor_Load(object sender, EventArgs e)
        {

            mListaPresupuestoNacionalDetalleOrigen = new List<PresupuestoNacionalDetalleBE>();
            bsListado.DataSource = mListaPresupuestoNacionalDetalleOrigen;
            grdPresupuestoNacionalDetalle.DataSource = bsListado;

            lstFacCompra = new BCompras().listarFacCompraRelacionPresupuesto(Parametros.intEjercicio, IdPresupuestoNacional);
            Cargar(false);
        }
        private void Cargar(bool Plantilla)
        {
            List<EPresupuestoNacionalDetalle> lstPresupuestoNacionalDetalle = new List<EPresupuestoNacionalDetalle>();

            if (Plantilla)

                lstPresupuestoNacionalDetalle = new BCompras().ListarNacionalPlantilla();
            else
                lstPresupuestoNacionalDetalle = new BCompras().ListarPresupuestoNacionalDetalle(IdPresupuestoNacional);

            foreach (EPresupuestoNacionalDetalle item in lstPresupuestoNacionalDetalle)
            {
                PresupuestoNacionalDetalleBE objPresupuestoNacionalDetalleBE = new PresupuestoNacionalDetalleBE();
                objPresupuestoNacionalDetalleBE.prepd_icod_detalle = item.prepd_icod_detalle;
                objPresupuestoNacionalDetalleBE.prep_icod_presupuesto = item.prep_icod_presupuesto;
                objPresupuestoNacionalDetalleBE.cpn_icod_concepto_nacional = item.cpn_icod_concepto_nacional;
                objPresupuestoNacionalDetalleBE.cpn_vdescripcion_concepto_nacional = item.cpn_vdescripcion_concepto_nacional;
                objPresupuestoNacionalDetalleBE.cpnd_icod_detalle_nacional = item.cpnd_icod_detalle_nacional;
                objPresupuestoNacionalDetalleBE.cpnd_vdescripcion = item.cpnd_vdescripcion;
                objPresupuestoNacionalDetalleBE.prepd_nmont_tot_concepto = item.prepd_nmont_tot_concepto;
                objPresupuestoNacionalDetalleBE.prepd_nmont_unit_concepto = item.prepd_nmont_unit_concepto;
                objPresupuestoNacionalDetalleBE.tablc_iid_tipo_moneda_origen = item.tablc_iid_tipo_moneda_origen;
                objPresupuestoNacionalDetalleBE.TipoMoneda = item.TipoMoneda;
                objPresupuestoNacionalDetalleBE.prepd_nmont_tot_concepto_origen = item.prepd_nmont_tot_concepto_origen;
                objPresupuestoNacionalDetalleBE.prepd_nmont_tot_ejecut = item.prepd_nmont_tot_ejecut;
                objPresupuestoNacionalDetalleBE.prepd_nmont_unit_ejecut = item.prepd_nmont_unit_ejecut;
                objPresupuestoNacionalDetalleBE.TipOper = item.TipOper;
                objPresupuestoNacionalDetalleBE.ctacc_iid_cuenta_contable = item.ctacc_iid_cuenta_contable;
                mListaPresupuestoNacionalDetalleOrigen.Add(objPresupuestoNacionalDetalleBE);
            }

            bsListado.DataSource = mListaPresupuestoNacionalDetalleOrigen;
            grdPresupuestoNacionalDetalle.DataSource = bsListado;
            grdPresupuestoNacionalDetalle.RefreshDataSource();
            viewPresupuestoNacionalDetalle.ExpandAllGroups();
        }
        public class PresupuestoNacionalDetalleBE
        {
            public int prepd_icod_detalle { get; set; }
            public int prep_icod_presupuesto { get; set; }
            public int cpn_icod_concepto_nacional { get; set; }
            public string cpn_vdescripcion_concepto_nacional { get; set; }
            public int cpnd_icod_detalle_nacional { get; set; }
            public string cpnd_vdescripcion { get; set; }
            public decimal prepd_nmont_tot_concepto { get; set; }
            public decimal prepd_nmont_unit_concepto { get; set; }
            public int tablc_iid_tipo_moneda_origen { get; set; }
            public string TipoMoneda { get; set; }
            public decimal prepd_nmont_tot_concepto_origen { get; set; }
            public decimal prepd_nmont_tot_ejecut { get; set; }
            public decimal prepd_nmont_unit_ejecut { get; set; }
            public int TipOper { get; set; }
            public int ctacc_iid_cuenta_contable { get; set; }

            public PresupuestoNacionalDetalleBE()
            {

            }
        }

        #endregion

        #region "Metodos"

        private void DgAcept()
        {
            //_Be = (EPresupuestoNacional)viewPresupuestoNacional.GetRow(viewPresupuestoNacional.FocusedRowHandle);
            //this.DialogResult = DialogResult.OK;
        }

        #endregion

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void viewPresupuestoNacional_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void viewPresupuestoNacionalDetalle_DoubleClick(object sender, EventArgs e)
        {
            if (mListaPresupuestoNacionalDetalleOrigen.Count > 0)
            {



                movDetalle.prepd_icod_detalle = Convert.ToInt32(viewPresupuestoNacionalDetalle.GetFocusedRowCellValue("prepd_icod_detalle"));

                if (movDetalle.prepd_icod_detalle != 0)
                {
                    movDetalle.ctacc_iid_cuenta_contable = Convert.ToInt32(viewPresupuestoNacionalDetalle.GetFocusedRowCellValue("ctacc_iid_cuenta_contable"));
                    movDetalle.cpnd_vdescripcion = viewPresupuestoNacionalDetalle.GetFocusedRowCellValue("cpnd_vdescripcion").ToString();
                    this.DialogResult = DialogResult.OK;
                }


            }
        }
    }
}




