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
    public partial class FrmConsultarDocXPagarGapProveedor : DevExpress.XtraEditors.XtraForm
    {

        List<EDocPorPagar> Lista = new List<EDocPorPagar>();
        public EProveedor Eproveedor = new EProveedor();
        public Boolean filtro = true;
        BCuentasPorPagar Obl = new BCuentasPorPagar();
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        private int xposition = 0;

        public FrmConsultarDocXPagarGapProveedor()
        {
            InitializeComponent();
        }

        private void FrmConsultarDocXPagarProveedor_Load(object sender, EventArgs e)
        {
            Lista = Obl.BuscarDocumentosXPagarGarantiaProveedor(Eproveedor.iid_icod_proveedor, Parametros.intEjercicio, 0);
            if (filtro)
            {
                this.Text = "DOCUMENTOS PENDIENTES POR PAGAR DE " + Eproveedor.vnombrecompleto;
                view.GroupPanelText = "DOCUMENTOS PENDIENTES POR PAGAR DE " + Eproveedor.vnombrecompleto;
                //dgr.DataSource = Lista.Where(dxc => dxc.tablc_iid_situacion_documento == 1 || dxc.tablc_iid_situacion_documento == 2).ToList();
                dgr.DataSource = Lista.Where(dxc => dxc.tablc_iid_situacion_documento != 10).ToList();
            }
            else
            {
                this.Text = "DOCUMENTOS POR PAGAR DE " + Eproveedor.vnombrecompleto;
                //view.GroupPanelText = "DOCUMENTOS POR PAGAR DE " + Eproveedor.vnombrecompleto;
                dgr.DataSource = Lista;
            }
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void Pagos_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocPorPagar Obe = (EDocPorPagar)view.GetRow(view.FocusedRowHandle);

                switch (Obe.tdocc_icod_tipo_doc)
                {
                    case 1:
                        FrmConsultarAdelantoPagos av = new FrmConsultarAdelantoPagos();
                        av.eDocXPagar = Obe;
                        av.doxcc_icod_correlativo_adelanto = Obe.doxpc_icod_correlativo;
                        av.Show();
                        xposition = view.FocusedRowHandle;
                        break;
                    case 86:
                        FrmConsultaPagosNotaCredito FrmNotaC = new FrmConsultaPagosNotaCredito();
                        FrmNotaC.eDocXPagar = Obe;
                        FrmNotaC.Cab_icod_correlativo = Obe.doxpc_icod_correlativo;
                        FrmNotaC.Show();
                        xposition = view.FocusedRowHandle;
                        FrmNotaC.mnu.Enabled = false;
                        break;
                    default:
                        FrmConsultaPagosDocumentosXPagar a = new FrmConsultaPagosDocumentosXPagar();
                        a.eDocXPagar = Obe;
                        a.Show();
                        xposition = view.FocusedRowHandle;
                        break;
                }

            }
        }

        private void imprimirDetalle_Click(object sender, EventArgs e)
        {
            Lista = new BCuentasPorPagar().EstadoCuentaDetalleProveedores(Eproveedor.iid_icod_proveedor, Parametros.intEjercicio, 0);

            string titulo1;
            string titulo2;
            titulo1 = "ESTADO DE CUENTAS DEL AÑO " + Parametros.intEjercicio;
            titulo2 = "DEL PROVEEDOR " + Eproveedor.vnombrecompleto;
            rptEstadoCuentaDetalleProveedor rpt = new rptEstadoCuentaDetalleProveedor();
            if (filtro)
                rpt.cargar(Lista.Where(dxc => dxc.tablc_iid_situacion_documento == 1 || dxc.tablc_iid_situacion_documento == 2).ToList(), Parametros.intEjercicio.ToString(), titulo1, titulo2);
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
                titulo1 = "ESTADO DE CUENTAS DEL AÑO " + Parametros.intEjercicio;
                titulo2 = "DEL PROVEEDOR " + Eproveedor.vnombrecompleto;

                rptEstadoCuentaDetallePagoProveedor rpt = new rptEstadoCuentaDetallePagoProveedor();
                if (filtro)
                {
                    Convertir c = new Convertir();
                    rpt.cargar(c.FiltrarDataTable(odxp.EstadoCuentaDetallePagoProveedor(Eproveedor.iid_icod_proveedor, Parametros.intEjercicio, 0), "tablc_iid_situacion_documento in (1, 2)"), Parametros.intEjercicio.ToString(), titulo1, titulo2);
                }
                else
                    rpt.cargar(odxp.EstadoCuentaDetallePagoProveedor(Eproveedor.iid_icod_proveedor, Parametros.intEjercicio, 0), Parametros.intEjercicio.ToString(), titulo1, titulo2);
            }
            else
                XtraMessageBox.Show("No hay registro por Reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}




