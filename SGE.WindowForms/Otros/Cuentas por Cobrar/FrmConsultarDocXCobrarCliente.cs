using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmConsultarDocXCobrarCliente : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;


        private List<EDocXCobrar> Lista = new List<EDocXCobrar>();
        public ECliente eCliente;

        public bool filtro;

        private int xposition = 0;

        public FrmConsultarDocXCobrarCliente()
        {
            InitializeComponent();
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            Lista = new BCuentasPorCobrar().BuscarDocumentosXCobrarCliente(eCliente.cliec_icod_cliente, Parametros.intEjercicio);
            if (filtro)
            {
                this.Text = "DOCUMENTOS PENDIENTES POR COBRAR DE " + eCliente.cliec_vnombre_cliente;
                view.GroupPanelText = "DOCUMENTOS PENDIENTES POR COBRAR DE " + eCliente.cliec_vnombre_cliente;
                dgr.DataSource = Lista.Where(dxc => dxc.tablc_iid_situacion_documento == 8 || dxc.tablc_iid_situacion_documento == 9).ToList();
            }
            else
            {
                this.Text = "DOCUMENTOS POR COBRAR DE " + eCliente.cliec_vnombre_cliente;
                view.GroupPanelText = "DOCUMENTOS POR COBRAR DE " + eCliente.cliec_vnombre_cliente;
                dgr.DataSource = Lista;
            }
        }

        private void Pagos_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocXCobrar Obe = (EDocXCobrar)view.GetRow(view.FocusedRowHandle);
                switch (Obe.tdocc_icod_tipo_doc)
                {
                    case 1: //adelanto
                        FrmConsultaPagosAdelantos a = new FrmConsultaPagosAdelantos();
                        a.eDocXCobrar = Obe;
                        a.Show();
                        xposition = view.FocusedRowHandle;
                        break;
                    case 36: //nota de crédito
                        FrmConsultaPagosNotaCredito nc = new FrmConsultaPagosNotaCredito();
                        nc.eDocXCobrar = Obe;
                        nc.Show();
                        xposition = view.FocusedRowHandle;
                        break;
                    default:
                        FrmConsultaPagosDocumentosXCobrar p = new FrmConsultaPagosDocumentosXCobrar();
                        p.eDocXCobrar = Obe;
                        p.Show();
                        xposition = view.FocusedRowHandle;
                        break;
                }
            }
            else
                XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void imprimirDetalle_Click(object sender, EventArgs e)
        {
            Lista = new BCuentasPorCobrar().EstadoCuentaDetalleCliente(eCliente.cliec_icod_cliente, Parametros.intEjercicio);
            rptEstadoCuentaDetalleCliente rpt = new rptEstadoCuentaDetalleCliente();
            if (filtro)
                rpt.cargar(Lista.Where(dxc => dxc.tablc_iid_situacion_documento == 1 || dxc.tablc_iid_situacion_documento == 2).ToList(), Parametros.intEjercicio.ToString(), eCliente.cliec_vnombre_cliente, "");
            else
                rpt.cargar(Lista, Parametros.intEjercicio.ToString(), eCliente.cliec_vnombre_cliente, "");
        }

        private void ConPagos_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                BCuentasPorCobrar odxc = new BCuentasPorCobrar();
                rptEstadoCuentaDetallePagoCliente rpt = new rptEstadoCuentaDetallePagoCliente();
                if (filtro)
                {
                    Convertir c = new Convertir();
                    rpt.cargar(c.FiltrarDataTable(odxc.EstadoCuentaDetallePagoCliente(eCliente.cliec_icod_cliente, Parametros.intEjercicio), "tablc_iid_situacion_documento in (108, 109)"), Parametros.intEjercicio.ToString(), eCliente.cliec_vnombre_cliente, "");
                }
                else
                    rpt.cargar(odxc.EstadoCuentaDetallePagoCliente(eCliente.cliec_icod_cliente, Parametros.intEjercicio), Parametros.intEjercicio.ToString(), eCliente.cliec_vnombre_cliente, "");
            }
            else
                XtraMessageBox.Show("No hay registro por Reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void dgr_Click(object sender, EventArgs e)
        {


        }

        private void btnsalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            bool activo = view.OptionsView.ShowAutoFilterRow;
            view.ClearColumnsFilter();
            if (activo)
            {
                btnFiltrar.Text = "Mostrar filtro";
                view.OptionsView.ShowAutoFilterRow = !activo;
            }
            else
            {
                btnFiltrar.Text = "Ocultar filtro";
                view.OptionsView.ShowAutoFilterRow = !activo;
            }
        }



    }
}