using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.WindowForms.Reportes.Contabilidad.Registros;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Registros
{
    public partial class frm06SaldosIniciales : DevExpress.XtraEditors.XtraForm
    {
        int intMes = -1;
        string strMes;
        List<EVoucherContableCab> lstComprobantes = new List<EVoucherContableCab>();

        BContabilidad objContabilidadData = new BContabilidad();
        List<ECuentaContable> lstCuentaContable = new List<ECuentaContable>();

        public frm06SaldosIniciales()
        {
            InitializeComponent();
        }

        private string opcionProceso = "";

        private void FrmComprobantes_Load(object sender, EventArgs e)
        {
            lstCuentaContable = objContabilidadData.listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
            BSControls.LoaderLook(LkpMeses, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro == 0).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
        }

        public void cargar()
        {
            lstComprobantes = objContabilidadData.listarVoucherContableCab(Parametros.intEjercicio, intMes);
        }

        void reload(int intIcod)
        {
            cargar();
            dgrComprobante.DataSource = lstComprobantes;
            int index = lstComprobantes.FindIndex(x => x.vcocc_icod_vcontable == intIcod);
            viewComprobante.FocusedRowHandle = index;
            viewComprobante.Focus();
        }

        private void nuevo()
        {
            FrmManteSaldosIniciales frm = new FrmManteSaldosIniciales();
            frm.MiEvento += new FrmManteSaldosIniciales.DelegadoMensaje(reload);
            frm.intOrigen = Parametros.intOriVcoManual;
            frm.intMes = intMes;
            frm.SetInsert();
            frm.lstComprobantes = lstComprobantes;
            frm.txtNroComprobante.Text = (lstComprobantes.Count + 1).ToString();
            frm.Show();
        }

        private void modificar()
        {

            EVoucherContableCab oBe = (EVoucherContableCab)viewComprobante.GetRow(viewComprobante.FocusedRowHandle);
            if (oBe != null)
            {
                if (oBe.tarec_icorrelativo_origen_vcontable == Parametros.intOriVcoOtroSistema)
                {
                    XtraMessageBox.Show("El comprobante no puede ser modificado porque ha sido generado desde otro sistema", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    FrmManteSaldosIniciales frm = new FrmManteSaldosIniciales();
                    frm.MiEvento += new FrmManteSaldosIniciales.DelegadoMensaje(reload);
                    frm.intMes = intMes;
                    frm.objCab = oBe;
                    frm.lstComprobantes = lstComprobantes;
                    frm.SetModify();
                    frm.Show();
                    frm.setValues();
                }
            }
        }
        private void eliminar()
        {
            try
            {
                EVoucherContableCab Obe = (EVoucherContableCab)viewComprobante.GetRow(viewComprobante.FocusedRowHandle);
                if (Obe != null)
                {
                    int index = viewComprobante.FocusedRowHandle;
                    if (Obe.tarec_icorrelativo_origen_vcontable == Parametros.intOriVcoOtroSistema)
                    {
                        XtraMessageBox.Show("El comprobante no puede ser eliminado porque ha sido generado desde otro sistema", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (XtraMessageBox.Show("¿Está seguro que desea eliminar el comprobante " + Obe.strNroVco + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        objContabilidadData.eliminarVoucherContableCab(Obe);
                        cargar();
                        if (lstComprobantes.Count >= index + 1)
                            viewComprobante.FocusedRowHandle = index;
                        else
                            viewComprobante.FocusedRowHandle = index - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void viewAnalitica_DoubleClick(object sender, EventArgs e)
        {
            EVoucherContableCab oBe = (EVoucherContableCab)viewComprobante.GetRow(viewComprobante.FocusedRowHandle);
            if (oBe != null)
            {
                FrmManteSaldosIniciales frm = new FrmManteSaldosIniciales();
                frm.SetCancel();
                frm.intMes = intMes;
                frm.objCab = oBe;
                frm.Show();
                frm.setValues();
            }
        }


        private void buscarCriterio()
        {
            dgrComprobante.DataSource = lstComprobantes.Where(x => x.vcocc_glosa.ToUpper().Contains(txtDescripcion.Text.ToUpper()) &&
                                                   x.strNroVco.ToString().ToUpper().StartsWith(txtCodigo.Text.ToUpper())).ToList();

        }
        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void imprimirComprobanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImprimirDetalle();
        }

        private void ImprimirDetalle()
        {
            EVoucherContableCab Obe = (EVoucherContableCab)viewComprobante.GetRow(viewComprobante.FocusedRowHandle);
            if (Obe != null)
            {
                List<EVoucherContableDet> lista = new BContabilidad().listarVoucherContableDet(Obe.vcocc_icod_vcontable);
                //rptComprobante reporte = new rptComprobante();
                //reporte.cargar(Obe, lista, strMes);
            }
        }

        private void imprimirRelaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstComprobantes.Count > 0)
            {
                //rptRelacionComprobante reporte = new rptRelacionComprobante();
                //reporte.cargar(lstComprobantes, strMes, Parametros.intPeriodo.ToString());
            }
        }
        private void RedondeoComprobante()
        {
            lstComprobantes.ForEach(x =>
            {
                if (x.vcocc_nmto_tot_debe_sol == x.vcocc_nmto_tot_haber_sol && x.vcocc_nmto_tot_debe_dol != x.vcocc_nmto_tot_haber_dol)
                {
                    if (x.vcocc_nmto_tot_debe_dol > x.vcocc_nmto_tot_haber_dol)
                        redondearVoucher(x.vcocc_icod_vcontable, 1);
                    else if (x.vcocc_nmto_tot_debe_dol < x.vcocc_nmto_tot_haber_dol)
                        redondearVoucher(x.vcocc_icod_vcontable, 2);
                }
                else if (x.vcocc_nmto_tot_debe_sol != x.vcocc_nmto_tot_haber_sol && x.vcocc_nmto_tot_debe_dol == x.vcocc_nmto_tot_haber_dol)
                {
                    if (x.vcocc_nmto_tot_debe_sol > x.vcocc_nmto_tot_haber_sol)
                        redondearVoucher(x.vcocc_icod_vcontable, 3);
                    else if (x.vcocc_nmto_tot_debe_sol < x.vcocc_nmto_tot_haber_sol)
                        redondearVoucher(x.vcocc_icod_vcontable, 4);
                }
            });
            cargar();
        }
        private void redondearVoucher(int vcocc_icod_vcontable, int intCaso)
        {
            objContabilidadData.redondearVoucher(vcocc_icod_vcontable, intCaso, Valores.intUsuario, WindowsIdentity.GetCurrent().Name);
        }

        private void limpiarLista()
        {
            lstComprobantes.Clear();
            viewComprobante.RefreshData();
            viewComprobante.GroupPanelText = "Resultado de la Búsqueda";
        }

        /*****************************************************************************************************************/
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (LkpMeses.EditValue != null)
            {
                intMes = Convert.ToInt32(LkpMeses.EditValue);
                strMes = LkpMeses.Text;
                limpiarLista();
                cargar();
            }
        }
        /*****************************************************************************************************************/
        private void eliminarActualizarRedondeos()
        {
            new BContabilidad().eliminarRedondeoVoucher(Parametros.intEjercicio, intMes);
            cargar();
        }

        private void viewComprobante_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string strSituacion = View.GetRowCellDisplayText(e.RowHandle, View.Columns["strVcoSituacion"]);
                if (strSituacion == "No Cuadrado")
                {
                    e.Appearance.BackColor = Color.LightSalmon;
                    //e.Appearance.BackColor2 = Color.SeaShell;

                }
            }
        }

        private void LkpMeses_EditValueChanged(object sender, EventArgs e)
        {
            if (LkpMeses.EditValue != null)
            {
                intMes = Convert.ToInt32(LkpMeses.EditValue);
                strMes = LkpMeses.Text;
                limpiarLista();
                cargar();
                dgrComprobante.DataSource = lstComprobantes;
                viewComprobante.Focus();
                viewComprobante.GroupPanelText = String.Format("COMPROBANTES MES : {0}", strMes.ToUpper());
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void btnCalculadora_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("calc");
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EVoucherContableCab Obe = (EVoucherContableCab)viewComprobante.GetRow(viewComprobante.FocusedRowHandle);
            if (Obe != null)
            {
                var lstDetalle = new BContabilidad().listarVoucherContableDet(Obe.vcocc_icod_vcontable);
                rpt05Comprobantes rpt = new rpt05Comprobantes();
                rpt.cargar(Obe, lstDetalle, strMes);
            }
        }
    }
}



