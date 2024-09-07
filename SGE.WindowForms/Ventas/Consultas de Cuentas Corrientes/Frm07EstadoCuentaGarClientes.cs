using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Cuentas_por_Cobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Consultas_de_Cuentas_Corrientes
{
    public partial class Frm07EstadoCuentaGarClientes : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        private List<ECliente> Lista;

        public Frm07EstadoCuentaGarClientes()
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
            Lista = new BCuentasPorCobrar().ListarDocPorCobrarSaldosGarantia(Parametros.intEjercicio);
            dgr.DataSource = Lista;
        }


        private void todos_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                ECliente eCliente = (ECliente)view.GetRow(view.FocusedRowHandle);

                FrmConsultarDocXCobrarClienteGar dxc = new FrmConsultarDocXCobrarClienteGar();
                dxc.MiEvento += new FrmConsultarDocXCobrarClienteGar.DelegadoMensaje(form2_MiEvento);
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
                FrmConsultarDocXCobrarClienteGar dxc = new FrmConsultarDocXCobrarClienteGar();
                dxc.MiEvento += new FrmConsultarDocXCobrarClienteGar.DelegadoMensaje(form2_MiEvento);
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
                rptEstadoCuentaClienteLista rpt = new rptEstadoCuentaClienteLista();
                Lista = Lista.Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
                rpt.cargar(Lista.Where(x => (x.doxcc_nmonto_saldo_soles + x.doxcc_nmonto_saldo_dolares) != 0).ToList(), Parametros.intEjercicio.ToString(), "");
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
                listaTempCliente = new BCuentasPorCobrar().EstadoCuentaDocumentos(Parametros.intEjercicio).Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
                if (listaTempCliente.Count > 0)
                {
                    rpt.cargar(listaTempCliente, Parametros.intEjercicio.ToString(), false, "");
                }

            }
            else
                XtraMessageBox.Show("No hay registro por reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void imprimirSoloPendientes_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                List<EDocXCobrar> mlistaDoxcobrar = new List<EDocXCobrar>();

                rptEstadoCuentaDocumentos rpt = new rptEstadoCuentaDocumentos();
                mlistaDoxcobrar = new BCuentasPorCobrar().EstadoCuentaDocumentos(Parametros.intEjercicio).Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
                if (mlistaDoxcobrar.Count > 0)
                {
                    rpt.cargar(mlistaDoxcobrar.Where(ob => ob.tablc_iid_situacion_documento != 10).ToList(), Parametros.intEjercicio.ToString(), false, "");
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


                if (listaTempCliente.Count > 0)
                {
                    rptEstadoCuentaClienteLista rpt = new rptEstadoCuentaClienteLista();
                    rpt.cargar(listaTempCliente, Parametros.intEjercicio.ToString(), "");
                }
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



                if (mlistCuentasDoc.Count > 0)
                {
                    rptEstadoCuentaDocumentos rpt = new rptEstadoCuentaDocumentos();
                    rpt.cargar(mlistCuentasDoc, Parametros.intEjercicio.ToString(), false, "");
                }
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

                if (mlista.Count > 0)
                {
                    rptEstadoCuentaDocumentos rpt = new rptEstadoCuentaDocumentos();
                    rpt.cargar(mlista, Parametros.intEjercicio.ToString(), true, "");
                }
            }
            else
                XtraMessageBox.Show("No hay registro por reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void txtNombre_EditValueChanged(object sender, EventArgs e)
        {
            if (txtNombre.ContainsFocus)
            {
                List<ECliente> listaTempCliente = new List<ECliente>();
                listaTempCliente = Lista.Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
                dgr.DataSource = listaTempCliente;
            }
        }



        private void txtcodigo_EditValueChanged(object sender, EventArgs e)
        {
            List<ECliente> listaTempCliente = new List<ECliente>();
            listaTempCliente = Lista.Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
            dgr.DataSource = listaTempCliente;
        }

        private void CobranzaDudosa_Click(object sender, EventArgs e)
        {

        }



    }
}