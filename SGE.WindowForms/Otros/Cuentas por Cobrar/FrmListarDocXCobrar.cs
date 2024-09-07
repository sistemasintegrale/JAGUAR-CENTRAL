using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmListarDocXCobrar : DevExpress.XtraEditors.XtraForm
    {
        private List<EDocXCobrar> mlist = new List<EDocXCobrar>();
        public EDocXCobrar _Be { get; set; }

        public int intCliente;
        public int intTipoDoc;
        public bool filtraCliente = false;
        public bool flagCanje = false;
        public bool flagBovFav = false;
        public long? icod_dxc_letra;
        public int icod_tipo_doc = 0;
        public int icod_tipo_documento_MP_MER;
        public FrmListarDocXCobrar()
        {
            InitializeComponent();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (viewDxc.RowCount > 0)
            {
                _Be = (EDocXCobrar)viewDxc.GetRow(viewDxc.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void textEdit1_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void BuscarCriterio()
        {
            grdDxc.DataSource = mlist.Where(obj => obj.doxcc_vnumero_doc.Contains(txtCodigo.Text)).ToList();
        }

        private void FrmListarDocXCobrarCobranza_Load(object sender, EventArgs e)
        {
            mlist = new BCuentasPorCobrar().listarDxcPendientes(Parametros.intEjercicio);
            if (filtraCliente)
                mlist = new BCuentasPorCobrar().listarDxcPendientes(Parametros.intEjercicio).Where(dxc => dxc.tdocc_icod_tipo_doc == intTipoDoc && dxc.cliec_icod_cliente == intCliente).ToList(); // por tipo de documento y cliente
            else if (flagCanje)
                mlist = new BCuentasPorCobrar().listarDxcPendientes(Parametros.intEjercicio).Where(dxc => dxc.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoCliente && dxc.tdocc_icod_tipo_doc != Parametros.intTipoDocNotaCreditoCliente && dxc.cliec_icod_cliente == intCliente).ToList(); //Para el módulo de Cuentas por Pagar                            
            else if (intTipoDoc == Parametros.intTipoDocAdelantoCliente)
                mlist = new BCuentasPorCobrar().listarDxcPendientes(Parametros.intEjercicio).Where(dxc => dxc.tdocc_icod_tipo_doc == intTipoDoc).ToList(); //Para adelantos

            else if (flagBovFav)
            {
                if (icod_tipo_documento_MP_MER == 2)
                {
                    mlist = new BCuentasPorCobrar().listarDxcConPagosSuministros(Parametros.intEjercicio, intCliente);
                }
                else
                {
                    mlist = new BCuentasPorCobrar().listarDxcConPagos(Parametros.intEjercicio, intCliente);
                }
            }
            else
                mlist = new BCuentasPorCobrar().listarDxcPendientes(Parametros.intEjercicio).Where(dxc => dxc.tdocc_icod_tipo_doc != 1 && dxc.tdocc_icod_tipo_doc != 36 && dxc.doxcc_icod_correlativo != icod_dxc_letra && dxc.cliec_icod_cliente == intCliente).ToList(); //Para el módulo de Cuentas por Pagar

            grdDxc.DataSource = mlist;
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }



    }
}