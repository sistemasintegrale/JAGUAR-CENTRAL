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
    public partial class frmFormatoInventarioResultado : DevExpress.XtraEditors.XtraForm
    {
        #region Propiedades

        List<EInventarioResultado> Lista = new List<EInventarioResultado>();
        private BContabilidad obl = new BContabilidad();
        //List<EEstadoGanPer> ListaCta = new List<EEstadoGanPer>();

        #endregion

        public frmFormatoInventarioResultado()
        {
            InitializeComponent();
        }

        private void frmRegFormatoPosFinanciera_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            Lista = new BContabilidad().ListarInventarioResultado();
            grd.DataSource = Lista;
        }

        private void Modify(int Cab_icod_correlativo)
        {
            Cargar();
            int index = Lista.FindIndex(obe => obe.irc_icod_inventario_resultado == Cab_icod_correlativo);
            gv.FocusedRowHandle = index;
        }

        #region Posicion Financiera Cabecera

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (frmManteInventarioResultado frm = new frmManteInventarioResultado())
            {

                frm.MiEvento += new frmManteInventarioResultado.DelegadoMensaje(Modify);
                frm.ListaPosFinanIcod.AddRange(Lista.Select(obe => obe.irc_vlinea).ToList());
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
            EInventarioResultado Obe = (EInventarioResultado)gv.GetRow(gv.FocusedRowHandle);
            using (frmManteInventarioResultado frm = new frmManteInventarioResultado())
            {
                frm.MiEvento += new frmManteInventarioResultado.DelegadoMensaje(Modify);
                frm.ListaPosFinanIcod.AddRange(Lista.Select(obe => obe.irc_vlinea).ToList());
                frm.ListaPosFinanIcod.Remove(Obe.irc_vlinea);
                frm.obeEstadoGanPer = Obe;
                frm.Cab_icod_correlativo = Convert.ToInt32(Obe.irc_icod_inventario_resultado);
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
            EInventarioResultado Obe = (EInventarioResultado)gv.GetRow(gv.FocusedRowHandle);
            using (frmInventarioResultadoCtas frm = new frmInventarioResultadoCtas())
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
                    EInventarioResultado Obe = (EInventarioResultado)gv.GetRow(gv.FocusedRowHandle);
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    Obe.intUsuario = Valores.intUsuario;
                    obl.EliminarInventarioResultado(Obe);
                    Lista.Remove(Obe);
                    gv.RefreshData();
                }
            }
            else
            {
                XtraMessageBox.Show("No hay registros para eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}