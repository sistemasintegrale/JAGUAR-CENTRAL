using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmEstadoGanPerCtaCont : DevExpress.XtraEditors.XtraForm
    {
        #region Propiedades

        public EEstadoGanPer obePosFinan = new EEstadoGanPer();
        public List<EEstadoGanPerCtas> Lista = new List<EEstadoGanPerCtas>();
        private BContabilidad obl = new BContabilidad();

        #endregion

        public frmEstadoGanPerCtaCont()
        {
            InitializeComponent();
        }

        private void frmPosFinanCtaCont_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            Lista = obl.ListarEstadoGanPerCtasxIcodPosFin(Convert.ToInt32(obePosFinan.egpc_icod_estado_gan_per));
            grd.DataSource = Lista;
        }

        #region Mantenimiento

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmMantePosFinanCtaCont frm = new frmMantePosFinanCtaCont())
            {
                frm.MiEvento += new frmMantePosFinanCtaCont.DelegadoMensaje(Modify);
                frm.ListaCtasUtilizadas.AddRange(Lista.Select(obe => Convert.ToInt32(obe.egpd_iid_cuenta_contable)).ToList());
                frm.obePosFinan = obePosFinan;
                frm.SetInsert();
                frm.ShowDialog();
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                if (XtraMessageBox.Show("¿Está seguro de que quiere eliminar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    EEstadoGanPerCtas Obe = (EEstadoGanPerCtas)gv.GetRow(gv.FocusedRowHandle);
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    Obe.intUsuario = Valores.intUsuario;
                    obl.EliminarEstadoGanPerCtas(Obe);
                    Lista.Remove(Obe);
                    gv.RefreshData();
                }
            }
            else
            {
                XtraMessageBox.Show("No hay registros para eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void Modify(int Cab_icod_correlativo)
        {
            Cargar();
            int index = Lista.FindIndex(obe => obe.egpd_icod_ctas_estado_gan_per == Cab_icod_correlativo);
            gv.FocusedRowHandle = index;
        }


    }
}