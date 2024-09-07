using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.WindowForms.Reportes.Contabilidad.Registros;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Registros
{
    public partial class frm02Analitica : DevExpress.XtraEditors.XtraForm
    {
        List<ETablaRegistro> lstTipoAnaliticas = new List<ETablaRegistro>();
        public frm02Analitica()
        {
            InitializeComponent();
        }

        private void frm01CentroCosto_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstTipoAnaliticas = new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoAnalitica).OrderBy(x => x.tarec_icorrelativo_registro).ToList(); ;
            grdAnalitica.DataSource = lstTipoAnaliticas;
            viewAnalitica.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstTipoAnaliticas.FindIndex(x => x.tarec_iid_tabla_registro == intIcod);
            viewAnalitica.FocusedRowHandle = index;
            viewAnalitica.Focus();
        }
        private void nuevo()
        {
            frmManteAnalitica frm = new frmManteAnalitica();
            frm.MiEvento += new frmManteAnalitica.DelegadoMensaje(reload);
            frm.lstTipoAnaliticas = lstTipoAnaliticas;
            frm.SetInsert();
            frm.Show();
        }
        private void modificar()
        {
            ETablaRegistro Obe = (ETablaRegistro)viewAnalitica.GetRow(viewAnalitica.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteAnalitica frm = new frmManteAnalitica();
            frm.MiEvento += new frmManteAnalitica.DelegadoMensaje(reload);
            frm.lstTipoAnaliticas = lstTipoAnaliticas;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();
        }
        private void viewCentroCosto_DoubleClick(object sender, EventArgs e)
        {
            ETablaRegistro Obe = (ETablaRegistro)viewAnalitica.GetRow(viewAnalitica.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteAnalitica frm = new frmManteAnalitica();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }
        private void eliminar()
        {
            try
            {
                ETablaRegistro Obe = (ETablaRegistro)viewAnalitica.GetRow(viewAnalitica.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewAnalitica.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    new BAdministracionSistema().eliminarTablaRegistro(Obe);
                    cargar();
                    if (lstTipoAnaliticas.Count >= index + 1)
                        viewAnalitica.FocusedRowHandle = index;
                    else
                        viewAnalitica.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void imprimir()
        {
            if (lstTipoAnaliticas.Count > 0)
            {
                rpt02Analiticas rpt = new rpt02Analiticas();
                rpt.cargar(lstTipoAnaliticas);
            }
        }
        private void buscarCriterio()
        {
            grdAnalitica.DataSource = lstTipoAnaliticas.Where(x =>
                                                   x.tarec_icorrelativo_registro.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.tarec_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
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

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
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

        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }
        private void listarAnaliticaDetalle()
        {
            ETablaRegistro Obe = (ETablaRegistro)viewAnalitica.GetRow(viewAnalitica.FocusedRowHandle);
            if (Obe == null)
                return;
            frmAnaliticaDetalle frm = new frmAnaliticaDetalle();
            frm.intTipoAnalitica = Obe.tarec_icorrelativo_registro;
            frm.Show();
        }
        private void analíticaDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listarAnaliticaDetalle();
        }

        private void btnSubAnalitica_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            listarAnaliticaDetalle();
        }
    }
}