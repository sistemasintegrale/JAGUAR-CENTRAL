using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmPagoDirectoDocumentosXCobrar : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPagoDirectoDocumentosXCobrar));

        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;

        private List<EDocXCobrarPago> Lista = new List<EDocXCobrarPago>();
        public EDocXCobrar eDocXCobrar = new EDocXCobrar();
        private int xposition = 0;
        public int mes;

        public FrmPagoDirectoDocumentosXCobrar()
        {
            InitializeComponent();
        }

        void form2_MiEvento()
        {
            Carga();
        }

        void Modify()
        {
            Carga();
            grv.FocusedRowHandle = xposition;
        }

        private void FrmPagoDirectoDocumentosXCobrar_Load(object sender, EventArgs e)
        {
            Carga();
            this.Text = "Pagos efectuados por " + eDocXCobrar.Abreviatura + "-" + eDocXCobrar.doxcc_vnumero_doc;
            grv.GroupPanelText = "Pagos efectuados por " + eDocXCobrar.Abreviatura + "-" + eDocXCobrar.doxcc_vnumero_doc;
        }

        private void Carga()
        {
            Lista = (new BCuentasPorCobrar().ListarPagoDirectoDocumentoXCobrar(eDocXCobrar.doxcc_icod_correlativo)).Where(ob => ob.pdxcc_vorigen == "D").ToList();
            grd.DataSource = Lista;
        }

        private void Nuevo_Click(object sender, EventArgs e)
        {
            if (eDocXCobrar.doxcc_nmonto_saldo == 0)
            {
                XtraMessageBox.Show("El documento ya se encuentra cancelado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                using (FrmMantePagoDirectoDocumentosXCobrar p = new FrmMantePagoDirectoDocumentosXCobrar())
                {
                    p.MiEvento += new FrmMantePagoDirectoDocumentosXCobrar.DelegadoMensaje(form2_MiEvento);
                    p.obeDocXCobrar = eDocXCobrar;
                    p.saldoGral = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_saldo);
                    grv.MoveLast();
                    p.SetInsert();
                    p.cargar();
                    p.ShowDialog();
                    p.txtNumeroDocumento.Focus();
                }
            }
        }

        private void FrmPagoDirectoDocumentosXCobrar_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MiEvento();
        }

        private void Modificar_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                Datos();
            }
            else
                XtraMessageBox.Show("No hay registro por modificar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void Datos()
        {
            EDocXCobrarPago Obe = (EDocXCobrarPago)grv.GetRow(grv.FocusedRowHandle);
            FrmMantePagoDirectoDocumentosXCobrar p = new FrmMantePagoDirectoDocumentosXCobrar();
            p.MiEvento += new FrmMantePagoDirectoDocumentosXCobrar.DelegadoMensaje(Modify);
            p._BE = Obe;
            p.obeDocXCobrar = eDocXCobrar;
            p.cargar();
            p.SetValues();
            p.SetModify();
            p.ShowDialog();
            xposition = grv.FocusedRowHandle;

        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocXCobrarPago Obe = (EDocXCobrarPago)grv.GetRow(grv.FocusedRowHandle);
                Obe.doxcc_icod_correlativo_pago = Obe.doxcc_icod_correlativo;
                if (XtraMessageBox.Show("¿Está seguro de Eliminar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    EliminarPago(Obe);

                }
            }
            else
                XtraMessageBox.Show("No hay registro por eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void EliminarPago(EDocXCobrarPago Obe)
        {

            Obe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
            Obe.intUsuario = Valores.intUsuario;
            new BCuentasPorCobrar().EliminarPagoDirectoDocumentoXCobrar(Obe);
            Lista.Remove(Obe);
            grv.RefreshData();
        }

        private void btn_cancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

    }
}