using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmConsultarDocXCobrarClienteAUnaFecha : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;

        private List<EDocXCobrar> Lista = new List<EDocXCobrar>();
        public ECliente eCliente;
        public DateTime sfecha;
        public bool filtro;
        private int xposition = 0;

        public FrmConsultarDocXCobrarClienteAUnaFecha()
        {
            InitializeComponent();
        }

        private void FrmConsultarDocXCobrarClienteAUnaFecha_Load(object sender, EventArgs e)
        {
            Lista = new BCuentasPorCobrar().BuscarDocumentosXCobrarClienteAUnaFecha(eCliente.cliec_icod_cliente, Parametros.intEjercicio, sfecha).Where(x => x.tdocc_icod_tipo_doc != 112).ToList();
            if (filtro)
            {
                this.Text = "DOCUMENTOS PENDIENTES POR COBRAR DE " + eCliente.cliec_vnombre_cliente;
                view.GroupPanelText = "DOCUMENTOS PENDIENTES POR COBRAR DE " + eCliente.cliec_vnombre_cliente;
                dgr.DataSource = Lista.Where(dxc => dxc.tablc_iid_situacion_documento == 108 || dxc.tablc_iid_situacion_documento == 109).ToList();
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
                        FrmConsultaPagosAdelantosAunaFecha a = new FrmConsultaPagosAdelantosAunaFecha();
                        a.sfecha = sfecha;
                        a.eDocXCobrar = Obe;
                        a.Show();
                        xposition = view.FocusedRowHandle;
                        break;
                    case 36: //nota de crédito
                        FrmConsultaPagosNotaCreditoAunaFecha nc = new FrmConsultaPagosNotaCreditoAunaFecha();
                        nc.sfecha = sfecha;
                        nc.eDocXCobrar = Obe;
                        nc.Show();
                        xposition = view.FocusedRowHandle;
                        break;
                    default:
                        FrmConsultaPagosDocumentosXCobrarAUnaFecha p = new FrmConsultaPagosDocumentosXCobrarAUnaFecha();
                        p.sfecha = sfecha;
                        p.eDocXCobrar = Obe;
                        p.Show();
                        xposition = view.FocusedRowHandle;
                        break;
                }
            }
            else
                XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void imprimirDetalle_Click(object sender, EventArgs e)
        {
            Lista = new BCuentasPorCobrar().EstadoCuentaDetalleClienteAUnaFecha(eCliente.cliec_icod_cliente, Parametros.intEjercicio, sfecha);
            rptEstadoCuentaDetalleCliente rpt = new rptEstadoCuentaDetalleCliente();
            if (filtro)
                rpt.cargar(Lista.Where(dxc => dxc.tablc_iid_situacion_documento == 108 || dxc.tablc_iid_situacion_documento == 109).ToList(), Parametros.intEjercicio.ToString(), eCliente.cliec_vnombre_cliente, "AL " + sfecha.ToString("dd/MM/yyyy"));
            else
                rpt.cargar(Lista, Parametros.intEjercicio.ToString(), eCliente.cliec_vnombre_cliente, "AL " + sfecha.ToString("dd/MM/yyyy"));
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
                    rpt.cargar(c.FiltrarDataTable(odxc.EstadoCuentaDetallePagoClienteAunaFecha(eCliente.cliec_icod_cliente, sfecha, Parametros.intEjercicio), "tablc_iid_situacion_documento in (108, 109)"), Parametros.intEjercicio.ToString(), eCliente.cliec_vnombre_cliente, "AL " + sfecha.ToString("dd/MM/yyyy"));
                }
                else
                    rpt.cargar(odxc.EstadoCuentaDetallePagoClienteAunaFecha(eCliente.cliec_icod_cliente, sfecha, Parametros.intEjercicio), Parametros.intEjercicio.ToString(), eCliente.cliec_vnombre_cliente, "AL " + sfecha.ToString("dd/MM/yyyy"));
            }
            else
                XtraMessageBox.Show("No hay registro por Reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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