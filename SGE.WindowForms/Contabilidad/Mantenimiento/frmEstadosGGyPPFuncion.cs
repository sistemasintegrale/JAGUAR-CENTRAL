using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Mantenimiento
{
    public partial class frmEstadosGGyPPFuncion : DevExpress.XtraEditors.XtraForm
    {
        #region Propiedades

        List<EEstadoGanPerFuncion> Lista = new List<EEstadoGanPerFuncion>();
        private BContabilidad obl = new BContabilidad();
        //List<EEstadoGanPer> ListaCta = new List<EEstadoGanPer>();

        #endregion

        public frmEstadosGGyPPFuncion()
        {
            InitializeComponent();
        }

        private void frmRegFormatoPosFinanciera_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            Lista = new BContabilidad().ListarEstadoGanPerFuncion();
            grd.DataSource = Lista;
        }

        private void Modify(int Cab_icod_correlativo)
        {
            Cargar();
            int index = Lista.FindIndex(obe => obe.egpfc_icod_estado_gan_per_funcion == Cab_icod_correlativo);
            gv.FocusedRowHandle = index;
        }

        #region Posicion Financiera Cabecera

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (frmManteEstadoGanPerFuncion frm = new frmManteEstadoGanPerFuncion())
            {

                frm.MiEvento += new frmManteEstadoGanPerFuncion.DelegadoMensaje(Modify);
                frm.ListaPosFinanIcod.AddRange(Lista.Select(obe => obe.egpfc_vlinea).ToList());
                frm.SetInsert();
                frm.lkpTipoLinea.EditValue = 354;
                frm.CargarControles();
                frm.ShowDialog();
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
                DatosCab(BSMaintenanceStatus.ModifyCurrent);
            else
                XtraMessageBox.Show("No hay registros para modificar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gv_DoubleClick(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
                DatosCab(BSMaintenanceStatus.View);
        }

        private void DatosCab(BSMaintenanceStatus accion)
        {
            EEstadoGanPerFuncion Obe = (EEstadoGanPerFuncion)gv.GetRow(gv.FocusedRowHandle);
            using (frmManteEstadoGanPerFuncion frm = new frmManteEstadoGanPerFuncion())
            {
                frm.MiEvento += new frmManteEstadoGanPerFuncion.DelegadoMensaje(Modify);
                frm.ListaPosFinanIcod.AddRange(Lista.Select(obe => obe.egpfc_vlinea).ToList());
                frm.ListaPosFinanIcod.Remove(Obe.egpfc_vlinea);
                frm.obeEstadoGanPer = Obe;
                frm.Cab_icod_correlativo = Convert.ToInt32(Obe.egpfc_icod_estado_gan_per_funcion);
                if (accion == BSMaintenanceStatus.ModifyCurrent)
                    frm.SetModify();
                else
                    frm.SetCancel();
                frm.ShowDialog();
            }
        }

        #endregion

        private void ctasContablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EEstadoGanPerFuncion Obe = (EEstadoGanPerFuncion)gv.GetRow(gv.FocusedRowHandle);
            using (frmEstadoGanPerCtaFuncion frm = new frmEstadoGanPerCtaFuncion())
            {
                frm.obePosFinan = Obe;
                frm.ShowDialog();
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                if (XtraMessageBox.Show("¿Está seguro de eliminar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    EEstadoGanPerFuncion Obe = (EEstadoGanPerFuncion)gv.GetRow(gv.FocusedRowHandle);
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    Obe.intUsuario = Valores.intUsuario;
                    obl.EliminarEstadoGanPerFuncion(Obe);
                    Lista.Remove(Obe);
                    gv.RefreshData();
                }
            }
            else
            {
                XtraMessageBox.Show("No hay registros para eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (Lista.Count > 0)
            //{
            //    ListaCta= obl.ListarPosicionFinancieraCtas();

            //    List<string> ListaCodLinea = new List<string>();
            //    foreach (string item in ListaCta.Select(obe => obe.posc_vcodigo_linea).Distinct())
            //        ListaCodLinea.Add(item);

            //    foreach (EPosicionFinanciera item in Lista)
            //    {
            //        if (!ListaCodLinea.Exists(obe => obe == item.posc_vcodigo_linea))
            //        {
            //            ListaCta.Add(new EPosFinanCta()
            //            {
            //                posc_vcodigo_linea = item.posc_vcodigo_linea,
            //                posc_vid_tipo_linea = item.posc_vid_tipo_linea,
            //                posc_vconcepto_linea = item.posc_vconcepto_linea,
            //                posc_vid_tipo_total = item.posc_vid_tipo_total,
            //                posd_iid_cuenta_contable = 0
            //            });
            //        }
            //    }

            //    rptRegFormatoPosFinanciera rpt = new rptRegFormatoPosFinanciera();
            //    rpt.Cargar(ListaCta);
            //}
            //else
            //    XtraMessageBox.Show("No hay registros para imprimir", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}