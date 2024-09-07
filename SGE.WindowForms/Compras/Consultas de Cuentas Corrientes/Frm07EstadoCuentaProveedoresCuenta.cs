using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Compras.Consultas_de_Cuentas_Corrientes
{
    public partial class Frm07EstadoCuentaProveedoresCuenta : DevExpress.XtraEditors.XtraForm
    {
        BCuentasPorPagar CPP = new BCuentasPorPagar();
        List<EDocPorPagar> mlista = new List<EDocPorPagar>();

        public Frm07EstadoCuentaProveedoresCuenta()
        {
            InitializeComponent();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void cargar()
        {
            mlista = CPP.BuscarDocumentosXPagarCuenta(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue));
            dgr.DataSource = mlista;
        }
        private void lkpMes_EditValueChanged(object sender, EventArgs e)
        {
            cargar();
        }

        private void FrmEstadoCuentaProveedoresCuenta_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMes.EditValue = DateTime.Now.Month;
            cargar();
        }

        private void pendientes_Click(object sender, EventArgs e)
        {
            if (mlista.Count > 0)
            {
                rptEstadoCuentaProveedoresCuenta rpt = new rptEstadoCuentaProveedoresCuenta();
                rpt.cargar(mlista, Parametros.intEjercicio.ToString(), lkpMes.Text.ToUpper());
            }
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewEstado.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewEstado.ClearColumnsFilter();
        }
    }
}