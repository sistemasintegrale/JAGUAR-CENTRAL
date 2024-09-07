using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Cuentas_por_Cobrar;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Ventas.Consultas_de_Cuentas_Corrientes
{
    public partial class Frm06ConsultaAdelantoCliente : DevExpress.XtraEditors.XtraForm
    {
        BCuentasPorCobrar obl = new BCuentasPorCobrar();
        List<EAdelantoCliente> list = new List<EAdelantoCliente>();
        public Frm06ConsultaAdelantoCliente()
        {
            InitializeComponent();
        }

        private void FrmConsultaAdelantoCliente_Load(object sender, EventArgs e)
        {
            this.Cargar();
        }
        private void Cargar()
        {
            list = obl.ListarAdelantoClienteTodo(Parametros.intEjercicio);
            grd.DataSource = list;
        }

        private void todos_Click(object sender, EventArgs e)
        {
            EAdelantoCliente Obe = (EAdelantoCliente)gv.GetRow(gv.FocusedRowHandle);
            if (Obe != null)
            {

                EDocXCobrar _OBE = new EDocXCobrar();
                _OBE.doxcc_icod_correlativo = Obe.doxcc_icod_correlativo;
                FrmConsultaPagosDocumentosXCobrar p = new FrmConsultaPagosDocumentosXCobrar();
                p.eDocXCobrar = _OBE;
                p.ShowDialog();

            }
        }

        private void pendientes_Click(object sender, EventArgs e)
        {
            if (list.Count > 0)
            {
                rptConsultaAdelantoClientes rpt = new rptConsultaAdelantoClientes();
                rpt.cargar(list, Parametros.intEjercicio.ToString(), true);
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            bool activo = gv.OptionsView.ShowAutoFilterRow;
            gv.ClearColumnsFilter();
            if (activo)
            {
                btnFiltrar.Text = "Mostrar filtro";
                gv.OptionsView.ShowAutoFilterRow = !activo;
            }
            else
            {
                btnFiltrar.Text = "Ocultar filtro";
                gv.OptionsView.ShowAutoFilterRow = !activo;
            }
        }

    }
}