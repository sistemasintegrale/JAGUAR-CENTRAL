using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Cuentas_por_Cobrar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Consultas_de_Cuentas_Corrientes
{
    public partial class Frm05ConsultaNCreditoCliente : DevExpress.XtraEditors.XtraForm
    {
        List<ENotaCreditoCliente> Lista = new List<ENotaCreditoCliente>();
        BCuentasPorCobrar obl = new BCuentasPorCobrar();

        public Frm05ConsultaNCreditoCliente()
        {
            InitializeComponent();
        }

        private void FrmConsultaNCreditoCliente_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            Lista = obl.ListarNotaCreditoClienteTodo(Parametros.intEjercicio);
            grd.DataSource = Lista;
        }

        private void pagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmConsultaPagosConNCredito frm = new FrmConsultaPagosConNCredito())
            {
                ENotaCreditoCliente obe = (ENotaCreditoCliente)gv.GetRow(gv.FocusedRowHandle);
                switch (obe.tablc_iid_situacion_documento)
                {
                    case 1:
                        XtraMessageBox.Show("No hay pagos realizados", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                    case 4:
                        XtraMessageBox.Show("El documento se encuentra anulado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                    default:
                        frm.codENotaCreditoCliente = Convert.ToInt64(obe.doxcc_icod_correlativo);
                        frm.ShowDialog();
                        break;
                }
            }
        }

        private void todosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ENotaCreditoPago> ListaPago = new List<ENotaCreditoPago>();
            ListaPago = (new BCuentasPorCobrar()).ListarPagoNotaCredito(0, 0, 1, Parametros.intEjercicio);
            rptConsultaNCreditoClientes frm = new rptConsultaNCreditoClientes();
            frm.Cargar(Lista, ListaPago);
        }

        private void pendientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptConsultaNCreditoClientesPend frm = new rptConsultaNCreditoClientesPend();
            frm.Cargar(Lista.Where(ob => ob.tablc_iid_situacion_documento == 8 || ob.tablc_iid_situacion_documento == 9).ToList());
        }

    }
}