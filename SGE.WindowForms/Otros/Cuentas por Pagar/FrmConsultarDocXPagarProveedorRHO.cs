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
    public partial class FrmConsultarDocXPagarProveedorRHO : DevExpress.XtraEditors.XtraForm
    {
        List<EDocPorPagar> Lista = new List<EDocPorPagar>();
        public EProveedor Eproveedor = new EProveedor();
        public Boolean filtro = true;
        BCuentasPorPagar Obl = new BCuentasPorPagar();
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        private int xposition = 0;

        public FrmConsultarDocXPagarProveedorRHO()
        {
            InitializeComponent();
        }

        private void FrmConsultarDocXPagarProveedorRHO_Load(object sender, EventArgs e)
        {
            Lista = Obl.BuscarDocumentosXPagarProveedor(Eproveedor.iid_icod_proveedor, Parametros.intEjercicio, 51);
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

        private void Pagos_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocPorPagar Obe = (EDocPorPagar)view.GetRow(view.FocusedRowHandle);
                if (Obe != null)
                {
                    FrmConsultaPagosDocumentosXPagar a = new FrmConsultaPagosDocumentosXPagar();
                    a.eDocXPagar = Obe;
                    a.Show();
                    xposition = view.FocusedRowHandle;
                }
            }
        }

        private void imprimirDetalle_Click(object sender, EventArgs e)
        {
            Lista = new BCuentasPorPagar().EstadoCuentaDetalleProveedores(Eproveedor.iid_icod_proveedor, Parametros.intEjercicio, 51);
            rptEstadoCuentaDetalleProveedor rpt = new rptEstadoCuentaDetalleProveedor();

            string titulo1;
            string titulo2;
            titulo1 = "ESTADO DE CUENTAS RHO DEL AÑO " + Parametros.intEjercicio;
            titulo2 = "DEL PROVEEDOR " + Eproveedor.vnombrecompleto;

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
                titulo1 = "ESTADO DE CUENTAS RHO DEL AÑO " + Parametros.intEjercicio;
                titulo2 = "DEL PROVEEDOR " + Eproveedor.vnombrecompleto;

                rptEstadoCuentaDetallePagoProveedor rpt = new rptEstadoCuentaDetallePagoProveedor();
                if (filtro)
                {
                    Convertir c = new Convertir();
                    rpt.cargar(c.FiltrarDataTable(odxp.EstadoCuentaDetallePagoProveedor(Eproveedor.iid_icod_proveedor, Parametros.intEjercicio, 51), "tablc_iid_situacion_documento in (292, 293)"), Parametros.intEjercicio.ToString(), titulo1, titulo2);
                }
                else
                    rpt.cargar(odxp.EstadoCuentaDetallePagoProveedor(Eproveedor.iid_icod_proveedor, Parametros.intEjercicio, 51), Parametros.intEjercicio.ToString(), titulo1, titulo2);
            }
            else
                XtraMessageBox.Show("No hay registro por Reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}