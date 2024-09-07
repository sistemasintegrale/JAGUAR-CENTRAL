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

namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class FrmConsultarDocXPagarProveedorAUnaFecha : DevExpress.XtraEditors.XtraForm
    {
        List<EDocPorPagar> Lista = new List<EDocPorPagar>();
        public EProveedor Eproveedor = new EProveedor();
        public Boolean filtro = true;
        BCuentasPorPagar Obl = new BCuentasPorPagar();
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        private int xposition = 0;

        public DateTime sfecha;
        public FrmConsultarDocXPagarProveedorAUnaFecha()
        {
            InitializeComponent();
        }

        private void Pagos_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocPorPagar Obe = (EDocPorPagar)view.GetRow(view.FocusedRowHandle);

                switch (Obe.tdocc_icod_tipo_doc)
                {
                    case 1:
                        FrmConsultarAdelantoPagosAunaFecha av = new FrmConsultarAdelantoPagosAunaFecha();
                        av.eDocXPagar = Obe;
                        av.sfecha = sfecha;
                        av.doxcc_icod_correlativo_adelanto = Obe.doxpc_icod_correlativo;
                        av.Show();
                        xposition = view.FocusedRowHandle;
                        break;
                    case 86:
                        FrmConsultaPagosNotaCreditoAUnaFecha FrmNotaC = new FrmConsultaPagosNotaCreditoAUnaFecha();
                        FrmNotaC.eDocXPagar = Obe;
                        FrmNotaC.sfecha = sfecha;
                        //FrmNotaC.doxcc_icod_correlativo_nota_credito = Obe.doxpc_icod_correlativo;
                        FrmNotaC.Show();
                        xposition = view.FocusedRowHandle;
                        break;
                    default:
                        FrmConsultaPagosDocumentosXPagarAUnaFecha a = new FrmConsultaPagosDocumentosXPagarAUnaFecha();
                        a.eDocXPagar = Obe;
                        a.sfecha = sfecha;
                        a.Show();
                        xposition = view.FocusedRowHandle;
                        break;
                }

            }
        }

        private void FrmConsultarDocXPagarProveedorAUnaFecha_Load(object sender, EventArgs e)
        {
            Lista = Obl.BuscarDocumentosXPagarProveedorAUnaFecha(Eproveedor.iid_icod_proveedor, sfecha, Parametros.intEjercicio);
            if (filtro)
            {
                this.Text = "DOCUMENTOS PENDIENTES POR PAGAR DE " + Eproveedor.vnombrecompleto;
                view.GroupPanelText = "DOCUMENTOS PENDIENTES POR PAGAR DE " + Eproveedor.vnombrecompleto;
                dgr.DataSource = Lista.Where(dxc => dxc.tablc_iid_situacion_documento == 292 || dxc.tablc_iid_situacion_documento == 293).ToList();
            }
            else
            {
                this.Text = "DOCUMENTOS POR COBRAR DE " + Eproveedor.vnombrecompleto;
                view.GroupPanelText = "DOCUMENTOS POR COBRAR DE " + Eproveedor.vnombrecompleto;
                dgr.DataSource = Lista;
            }
        }

        private void imprimirDetalle_Click(object sender, EventArgs e)
        {
            Lista = new BCuentasPorPagar().EstadoCuentaDetalleProveedoresAUnaFecha(Eproveedor.iid_icod_proveedor, Parametros.intEjercicio, sfecha);
            rptEstadoCuentaDetalleProveedor rpt = new rptEstadoCuentaDetalleProveedor();

            string titulo1;
            string titulo2;
            titulo1 = "ESTADO DE CUENTAS AL " + sfecha.ToString("dd/MM/yyyy");
            titulo2 = "DEL PROVEEDOR " + Eproveedor.vnombrecompleto;

            if (filtro)
                rpt.cargar(Lista.Where(dxc => dxc.tablc_iid_situacion_documento == 292 || dxc.tablc_iid_situacion_documento == 293).ToList(), Parametros.intEjercicio.ToString(), titulo1, titulo2);
            else
                rpt.cargar(Lista, Parametros.intEjercicio.ToString(), titulo1, titulo2);
        }

        private void ConPagos_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                BCuentasPorPagar odxp = new BCuentasPorPagar();
                string titulo1;
                string titulo2;
                titulo1 = "ESTADO DE CUENTAS AL " + sfecha.ToString("dd/MM/yyyy");
                titulo2 = "DEL PROVEEDOR " + Eproveedor.vnombrecompleto;
                rptEstadoCuentaDetallePagoProveedor rpt = new rptEstadoCuentaDetallePagoProveedor();
                if (filtro)
                {
                    Convertir c = new Convertir();
                    rpt.cargar(c.FiltrarDataTable(odxp.EstadoCuentaDetallePagoProveedorAunaFecha(Eproveedor.iid_icod_proveedor, Parametros.intEjercicio, sfecha), "tablc_iid_situacion_documento in (292, 293)"), Parametros.intEjercicio.ToString(), titulo1, titulo2);
                }
                else
                    rpt.cargar(odxp.EstadoCuentaDetallePagoProveedorAunaFecha(Eproveedor.iid_icod_proveedor, Parametros.intEjercicio, sfecha), Parametros.intEjercicio.ToString(), titulo1, titulo2);
            }
            else
                XtraMessageBox.Show("No hay registro por Reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}