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
    public partial class frmFormatoBalance : DevExpress.XtraEditors.XtraForm
    {
        #region Propiedades

        List<EBalance> Lista = new List<EBalance>();
        private BContabilidad obl = new BContabilidad();
        //List<EEstadoGanPer> ListaCta = new List<EEstadoGanPer>();

        #endregion

        public frmFormatoBalance()
        {
            InitializeComponent();
        }

        private void frmRegFormatoPosFinanciera_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            Lista = new BContabilidad().ListarBalance();
            grd.DataSource = Lista;
        }

        private void Modify(int Cab_icod_correlativo)
        {
            Cargar();
            int index = Lista.FindIndex(obe => obe.blgc_icod_balance == Cab_icod_correlativo);
            gv.FocusedRowHandle = index;
        }

        #region Posicion Financiera Cabecera

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (frmManteBalance frm = new frmManteBalance())
            {

                frm.MiEvento += new frmManteBalance.DelegadoMensaje(Modify);
                frm.ListaPosFinanIcod.AddRange(Lista.Select(obe => obe.blgc_vlinea).ToList());
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
            EBalance Obe = (EBalance)gv.GetRow(gv.FocusedRowHandle);
            using (frmManteBalance frm = new frmManteBalance())
            {
                frm.MiEvento += new frmManteBalance.DelegadoMensaje(Modify);
                frm.ListaPosFinanIcod.AddRange(Lista.Select(obe => obe.blgc_vlinea).ToList());
                frm.ListaPosFinanIcod.Remove(Obe.blgc_vlinea);
                frm.obeEstadoGanPer = Obe;
                frm.Cab_icod_correlativo = Convert.ToInt32(Obe.blgc_icod_balance);
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
            EBalance Obe = (EBalance)gv.GetRow(gv.FocusedRowHandle);
            using (frmBalanceCtas frm = new frmBalanceCtas())
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
                    EBalance Obe = (EBalance)gv.GetRow(gv.FocusedRowHandle);
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    Obe.intUsuario = Valores.intUsuario;
                    obl.EliminarBalance(Obe);
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

        }

    }
}