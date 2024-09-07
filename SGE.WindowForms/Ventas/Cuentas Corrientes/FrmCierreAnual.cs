using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Cuentas_por_Cobrar;
using SGE.WindowForms.Ventas.Consultas_de_Cuentas_Corrientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Cuentas_Corrientes
{
    public partial class FrmCierreAnual : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        private List<ECliente> Lista;

        public FrmCierreAnual()
        {
            InitializeComponent();
        }

        void form2_MiEvento()
        {
            Cargar();
        }

        private void FrmEstadoCuentaClientes_Load(object sender, EventArgs e)
        {
            this.Cargar();
        }

        private void Cargar()
        {
            Lista = new BCuentasPorCobrar().ListarDocPorCobrarSaldos(Parametros.intEjercicio);
            dgr.DataSource = Lista;
        }


        private void todos_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                ECliente eCliente = (ECliente)view.GetRow(view.FocusedRowHandle);

                FrmConsultarDocXCobrarCliente dxc = new FrmConsultarDocXCobrarCliente();
                dxc.MiEvento += new FrmConsultarDocXCobrarCliente.DelegadoMensaje(form2_MiEvento);
                dxc.eCliente = eCliente;
                dxc.filtro = false;
                dxc.mnu.Items[1].Visible = true;
                dxc.mnu.Items[2].Visible = true;
                dxc.Show();
                xposition = view.FocusedRowHandle;
            }
            else
                XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void pendientes_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                ECliente eCliente = (ECliente)view.GetRow(view.FocusedRowHandle);
                FrmConsultarDocXCobrarCliente dxc = new FrmConsultarDocXCobrarCliente();
                dxc.MiEvento += new FrmConsultarDocXCobrarCliente.DelegadoMensaje(form2_MiEvento);
                dxc.eCliente = eCliente;
                dxc.filtro = true;
                dxc.mnu.Items[1].Visible = true;
                dxc.mnu.Items[2].Visible = true;
                dxc.Show();
                xposition = view.FocusedRowHandle;
            }
            else
                XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void EstadoCuenta_Click(object sender, EventArgs e)
        {

        }

        private void imprimirLista_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                List<ECliente> listaTempCliente = new List<ECliente>();
                rptEstadoCuentaClienteLista rpt = new rptEstadoCuentaClienteLista();

                rpt.cargar(listaTempCliente.Where(x => (x.doxcc_nmonto_saldo_soles + x.doxcc_nmonto_saldo_dolares) != 0).ToList(), Parametros.intEjercicio.ToString(), "");
            }
            else
                XtraMessageBox.Show("No hay registro por reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void imprimirConDocumentos_Click(object sender, EventArgs e)
        {
            List<EDocXCobrar> listaTempCliente = new List<EDocXCobrar>();
            if (Lista.Count > 0)
            {
                rptEstadoCuentaDocumentos rpt = new rptEstadoCuentaDocumentos();
                rpt.cargar(listaTempCliente, Parametros.intEjercicio.ToString(), false, "");
            }
            else
                XtraMessageBox.Show("No hay registro por reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void imprimirSoloPendientes_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                List<EDocXCobrar> mlistaDoxcobrar = new List<EDocXCobrar>();


                if (mlistaDoxcobrar.Count > 0)
                {
                    rptEstadoCuentaDocumentos rpt = new rptEstadoCuentaDocumentos();
                    rpt.cargar(mlistaDoxcobrar, Parametros.intEjercicio.ToString(), true, "");
                }
            }
            else
                XtraMessageBox.Show("No hay registro por reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


        private void imprimirListaDudosa_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                List<ECliente> listaTempCliente = new List<ECliente>();
                listaTempCliente = new BCuentasPorCobrar().ListarClientesSaldosCobranzaDudosa(Parametros.intEjercicio);


                rptEstadoCuentaClienteLista rpt = new rptEstadoCuentaClienteLista();
                rpt.cargar(listaTempCliente, Parametros.intEjercicio.ToString(), "");

            }
            else
                XtraMessageBox.Show("No hay registro por reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void imprimirConDocumentosDudosa_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                List<EDocXCobrar> mlistCuentasDoc = new List<EDocXCobrar>();
                mlistCuentasDoc = new BCuentasPorCobrar().EstadoCuentaDocumentos(Parametros.intEjercicio).Where(duda => duda.idTipodocuemnto == 19).ToList();


                rptEstadoCuentaDocumentos rpt = new rptEstadoCuentaDocumentos();
                rpt.cargar(mlistCuentasDoc, Parametros.intEjercicio.ToString(), false, "");
            }
            else
                XtraMessageBox.Show("No hay registro por reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void imprimirSóloPendientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                List<EDocXCobrar> mlista = new List<EDocXCobrar>();
                mlista = new BCuentasPorCobrar().EstadoCuentaDocumentos(Parametros.intEjercicio).Where(dxc => dxc.idTipodocuemnto == 19).ToList();


                rptEstadoCuentaDocumentos rpt = new rptEstadoCuentaDocumentos();
                rpt.cargar(mlista, Parametros.intEjercicio.ToString(), true, "");

            }
            else
                XtraMessageBox.Show("No hay registro por reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void cierreDelEjercicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EAnioEjercicio a = new BCuentasPorCobrar().VerificarExistenciaAnoSiguiente(Parametros.intEjercicio);
            if (a.anioc_iid_anio_ejercicio == (Parametros.intEjercicio + 1))
            {
                if (XtraMessageBox.Show("Esta opción servirá únicamente para efectuar la inicialización de saldos x cobrar del prox. año. Si desea continuar presione el botón OK", "Información del Sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Cierre();
                    XtraMessageBox.Show("Pase Exitoso", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show("Año " + (Parametros.intEjercicio + 1).ToString() + " no se encuentra registrado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void Cierre()
        {

            try
            {
                new BCuentasPorCobrar().CierreDocumentoXCobrar(Parametros.intEjercicio, Valores.intUsuario);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}