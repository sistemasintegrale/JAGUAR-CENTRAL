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
    public partial class frmInventarioResultadoCtas : DevExpress.XtraEditors.XtraForm
    {
        #region Propiedades

        public EInventarioResultado obePosFinan = new EInventarioResultado();
        public List<EInventarioResultadoCtas> Lista = new List<EInventarioResultadoCtas>();
        private BContabilidad obl = new BContabilidad();

        #endregion

        public frmInventarioResultadoCtas()
        {
            InitializeComponent();
        }

        private void frmPosFinanCtaCont_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            Lista = obl.ListarInventarioResultadoCtasxIcodPosFin(Convert.ToInt32(obePosFinan.irc_icod_inventario_resultado));
            grd.DataSource = Lista;
        }

        #region Mantenimiento

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmManteInventarioResultadoCtaCont frm = new frmManteInventarioResultadoCtaCont())
            {
                frm.MiEvento += new frmManteInventarioResultadoCtaCont.DelegadoMensaje(Modify);
                frm.ListaCtasUtilizadas.AddRange(Lista.Select(obe => Convert.ToInt32(obe.ird_iid_cuenta_contable)).ToList());
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
                    EInventarioResultadoCtas Obe = (EInventarioResultadoCtas)gv.GetRow(gv.FocusedRowHandle);
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    Obe.intUsuario = Valores.intUsuario;
                    obl.EliminarInventarioResultadoCtas(Obe);
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
            int index = Lista.FindIndex(obe => obe.ird_icod_ctas_inventario_resultado == Cab_icod_correlativo);
            gv.FocusedRowHandle = index;
        }


    }
}