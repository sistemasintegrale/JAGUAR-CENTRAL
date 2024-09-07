using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Sistema
{
    public partial class FrmTipoCambioMensual : DevExpress.XtraEditors.XtraForm
    {
        private List<ETipoCambioMensual> mlist = new List<ETipoCambioMensual>();
        public FrmTipoCambioMensual()
        {
            InitializeComponent();
        }
        private void Carga()
        {
            mlist = new BGeneral().ListarTipoCambioMensual();
            dgrTipoCambio.DataSource = mlist;
        }
        private void FrmTipoCambioMensual_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMes.EditValue = DateTime.Now.Month;
            Carga();
        }
        void form2_MiEvento()
        {
            Carga();
        }
        private void Nuevo()
        {
            FrmManteTipoCambioMensual TipoCambios = new FrmManteTipoCambioMensual();
            TipoCambios.MiEvento += new FrmManteTipoCambioMensual.DelegadoMensaje(form2_MiEvento);
            TipoCambios.Show();
            TipoCambios.oDetail = mlist;
            TipoCambios.SetInsert();
        }
        private void Modificar()
        {
            if (mlist.Count > 0)
            {
                ETipoCambioMensual oBe = (ETipoCambioMensual)viewTipoCambio.GetRow(viewTipoCambio.FocusedRowHandle);
                FrmManteTipoCambioMensual TipoCambios = new FrmManteTipoCambioMensual();
                TipoCambios.MiEvento += new FrmManteTipoCambioMensual.DelegadoMensaje(form2_MiEvento);
                TipoCambios.Show();
                TipoCambios.oDetail = mlist;
                TipoCambios.SetModify(oBe);

            }
        }
        private void Eliminar()
        {
            if (mlist.Count > 0)
            {
                if (XtraMessageBox.Show("¿Desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ETipoCambioMensual oBe = (ETipoCambioMensual)viewTipoCambio.GetRow(viewTipoCambio.FocusedRowHandle);
                    BGeneral oBl = new BGeneral();
                    oBl.EliminarTipoCambioMensual(oBe);
                    Carga();
                }
            }
        }
        private void Imprimir()
        {
            //if (mlist.Count > 0)
            //{                
            //    rptTipoCambioMensual reporte = new rptTipoCambioMensual();
            //    reporte.carga(mlist);
            //}
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Imprimir();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgrTipoCambio.DataSource = mlist.Where(x => x.mesec_iid_mes == Convert.ToInt32(lkpMes.EditValue)).ToList();
            dgrTipoCambio.Refresh();
        }

        private void viewTipoCambio_DoubleClick(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                ETipoCambioMensual oBe = (ETipoCambioMensual)viewTipoCambio.GetRow(viewTipoCambio.FocusedRowHandle);
                FrmManteTipoCambioMensual TipoCambios = new FrmManteTipoCambioMensual();
                TipoCambios.MiEvento += new FrmManteTipoCambioMensual.DelegadoMensaje(form2_MiEvento);
                TipoCambios.Show();
                TipoCambios.oDetail = mlist;
                TipoCambios.SetCancel(oBe);
            }
        }
    }
}