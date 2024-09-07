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
    public partial class frmEstadoGanPerCtaFuncion : DevExpress.XtraEditors.XtraForm
    {
        #region Propiedades

        public EEstadoGanPerFuncion obePosFinan = new EEstadoGanPerFuncion();
        public List<EEstadoGanPerCtasFuncion> Lista = new List<EEstadoGanPerCtasFuncion>();
        private BContabilidad obl = new BContabilidad();

        #endregion

        public frmEstadoGanPerCtaFuncion()
        {
            InitializeComponent();
        }

        private void frmPosFinanCtaCont_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            Lista = obl.ListarEstadoGanPerCtasxIcodPosFinFuncion(Convert.ToInt32(obePosFinan.egpfc_icod_estado_gan_per_funcion));
            grd.DataSource = Lista;
        }

        #region Mantenimiento

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmManteEstadoGanPerCtaFuncion frm = new frmManteEstadoGanPerCtaFuncion())
            {
                frm.MiEvento += new frmManteEstadoGanPerCtaFuncion.DelegadoMensaje(Modify);
                frm.ListaCtasUtilizadas.AddRange(Lista.Select(obe => Convert.ToInt32(obe.egpfd_iid_cuenta_contable)).ToList());
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
                    EEstadoGanPerCtasFuncion Obe = (EEstadoGanPerCtasFuncion)gv.GetRow(gv.FocusedRowHandle);
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    Obe.intUsuario = Valores.intUsuario;
                    obl.EliminarEstadoGanPerCtasFuncion(Obe);
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
            int index = Lista.FindIndex(obe => obe.egpfd_icod_ctas_estado_gan_per_funcion == Cab_icod_correlativo);
            gv.FocusedRowHandle = index;
        }


    }
}