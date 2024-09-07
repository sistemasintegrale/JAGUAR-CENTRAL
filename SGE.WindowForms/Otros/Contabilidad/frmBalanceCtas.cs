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
    public partial class frmBalanceCtas : DevExpress.XtraEditors.XtraForm
    {
        #region Propiedades

        public EBalance obePosFinan = new EBalance();
        public List<EBalanceCtas> Lista = new List<EBalanceCtas>();
        private BContabilidad obl = new BContabilidad();

        #endregion

        public frmBalanceCtas()
        {
            InitializeComponent();
        }

        private void frmPosFinanCtaCont_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            Lista = obl.ListarBalanceCtasxIcodPosFin(Convert.ToInt32(obePosFinan.blgc_icod_balance));
            grd.DataSource = Lista;
        }

        #region Mantenimiento

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmManteBalanceCtaCont frm = new frmManteBalanceCtaCont())
            {
                frm.MiEvento += new frmManteBalanceCtaCont.DelegadoMensaje(Modify);
                frm.ListaCtasUtilizadas.AddRange(Lista.Select(obe => Convert.ToInt32(obe.blgd_iid_cuenta_contable)).ToList());
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
                    EBalanceCtas Obe = (EBalanceCtas)gv.GetRow(gv.FocusedRowHandle);
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    Obe.intUsuario = Valores.intUsuario;
                    obl.EliminarBalanceCtas(Obe);
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
            int index = Lista.FindIndex(obe => obe.blgd_icod_ctas_balance == Cab_icod_correlativo);
            gv.FocusedRowHandle = index;
        }


    }
}