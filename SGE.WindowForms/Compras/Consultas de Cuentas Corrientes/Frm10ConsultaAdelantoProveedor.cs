using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Cuentas_por_Pagar;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SGE.WindowForms.Compras.Consultas_de_Cuentas_Corrientes
{
    public partial class Frm10ConsultaAdelantoProveedor : DevExpress.XtraEditors.XtraForm
    {
        List<EAdelantoProveedor> mlistPro = new List<EAdelantoProveedor>();
        public Frm10ConsultaAdelantoProveedor()
        {
            InitializeComponent();
        }

        private void FrmConsultaAdelantoProveedor_Load(object sender, EventArgs e)
        {
            Cargar();
        }
        public void Cargar()
        {
            mlistPro = new BCuentasPorPagar().ListarAdelantoProveedorTodo(Parametros.intEjercicio);
            grd.DataSource = mlistPro;
        }

        private void todos_Click(object sender, EventArgs e)
        {
            EAdelantoProveedor Obe = (EAdelantoProveedor)viewAdelanto.GetRow(viewAdelanto.FocusedRowHandle);
            if (Obe != null)
            {
                EDocPorPagar obe = new EDocPorPagar();
                obe.Abreviatura = " CON EL ADELANTO Nº";
                obe.doxpc_vnumero_doc = Obe.vnumero_documento;


                FrmConsultarAdelantoPagos av = new FrmConsultarAdelantoPagos();
                av.eDocXPagar = obe;
                av.doxcc_icod_correlativo_adelanto = Convert.ToInt64(Obe.doxpc_icod_correlativo);
                av.Show();
            }
        }

        private void mnu_Opening(object sender, CancelEventArgs e)
        {

        }

        private void pendientes_Click(object sender, EventArgs e)
        {
            if (mlistPro.Count > 0)
            {
                rptConsultaAdelantoProveedor rpt = new rptConsultaAdelantoProveedor();
                rpt.cargar(mlistPro, Parametros.intEjercicio.ToString(), true);
            }
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewAdelanto.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewAdelanto.ClearColumnsFilter();
        }
    }
}