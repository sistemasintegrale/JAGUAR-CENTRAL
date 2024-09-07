using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Compras;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Compras.Compras_Suministros
{
    public partial class frm03BocCompra : DevExpress.XtraEditors.XtraForm
    {
        List<EBoletaCompra> lstBocCompra = new List<EBoletaCompra>();
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();

        public frm03BocCompra()
        {
            InitializeComponent();
        }

        private void frm08DocCompra_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstBocCompra = new BCompras().listarBoletaCompraNacional(Parametros.intEjercicio).Where(ob => ob.bcoc_flag_importacion == false).ToList();
            grdDocCompra.DataSource = lstBocCompra;

        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstBocCompra.FindIndex(x => x.bcoc_icod_doc == intIcod);
            viewDocCompra.FocusedRowHandle = index;
            viewDocCompra.Focus();
        }

        private void nuevo()
        {
            frmManteBoletaCompra frm = new frmManteBoletaCompra();
            frm.MiEvento += new frmManteBoletaCompra.DelegadoMensaje(reload);
            frm.SetInsert();
            frm.Show();
        }

        private void modificar()
        {
            EBoletaCompra obe = (EBoletaCompra)viewDocCompra.GetRow(viewDocCompra.FocusedRowHandle);
            if (obe == null)
                return;
            EDocPorPagar objE_DocPorPagar = new EDocPorPagar();
            objE_DocPorPagar.anio = Parametros.intEjercicio;
            objE_DocPorPagar.mesec_iid_mes = obe.bcoc_sfecha_doc.Month;
            List<EDocPorPagar> ListarDXP = new BCuentasPorPagar().ListarEDocPorPagar(objE_DocPorPagar).Where(x => x.doxpc_icod_correlativo == obe.intDXP).ToList();
            if (ListarDXP[0].tablc_iid_situacion_documento == 8)
            {
                try
                {
                    frmManteBoletaCompra frm = new frmManteBoletaCompra();
                    frm.MiEvento += new frmManteBoletaCompra.DelegadoMensaje(reload);
                    frm.Obe = obe;
                    frm.SetModify();
                    frm.Show();
                    frm.setValues();
                    frm.importarDatosToolStripMenuItem.Enabled = false;
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("La BOC no puede ser MODICADO, ya que se encuentra con pagos", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminar()
        {
            EBoletaCompra obe = (EBoletaCompra)viewDocCompra.GetRow(viewDocCompra.FocusedRowHandle);
            if (obe == null)
                return;
            //List<EFacturaCompra> lstVerificar = new BCompras().listarFacCompraXID(Parametros.intEjercicio, obe.fcoc_icod_doc);
            //if (lstVerificar[0].fcoc_iestado_recep == 273)//FACTURADO
            //{
            int index = viewDocCompra.FocusedRowHandle;
            try
            {
                if (obe.bcoc_isituacion != 8)
                    throw new ArgumentException(String.Format("La Boleta no puede ser eliminada, su situación es {0}", obe.strSituacion));
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar la Boleta?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    obe.intUsuario = Valores.intUsuario;
                    obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BCompras().eliminarBoletaCompraNacional(obe);
                    cargar();
                    if (lstBocCompra.Count >= index + 1)
                        viewDocCompra.FocusedRowHandle = index;
                    else
                        viewDocCompra.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //}
            //else
            //{
            //    XtraMessageBox.Show("La FAC no puede ser ELIMINADO, ya que se encuentra RECIBIDO", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }


        private void ver()
        {
            EBoletaCompra obe = (EBoletaCompra)viewDocCompra.GetRow(viewDocCompra.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                frmManteBoletaCompra frm = new frmManteBoletaCompra();
                frm.MiEvento += new frmManteBoletaCompra.DelegadoMensaje(reload);
                frm.Obe = obe;
                frm.SetCancel();
                frm.Show();
                frm.setValues();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void dtFechaInicio_EditValueChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(dtFechaInicio.Text))
                return;
            //
            dt1 = Convert.ToDateTime(dtFechaInicio.Text);
            //
            if (String.IsNullOrWhiteSpace(dtFechaFin.Text))
            {
                dt2 = Convert.ToDateTime(dtFechaInicio.Text);
                dtFechaFin.EditValue = dtFechaInicio.EditValue;
            }
            //
            filtrar();
        }

        private void filtrar()
        {
            if (String.IsNullOrWhiteSpace(dtFechaInicio.Text))
                grdDocCompra.DataSource = lstBocCompra.Where(
                    x => x.bcoc_num_doc.Contains(txtNroDoc.Text)
                        && x.strProveedor.Contains(txtProveedor.Text.ToUpper())
                    ).ToList();
            else
                grdDocCompra.DataSource = lstBocCompra.Where(x =>
                    x.bcoc_num_doc.Contains(txtNroDoc.Text) &&
                    x.strProveedor.Contains(txtProveedor.Text.ToUpper()) &&
                    x.bcoc_sfecha_doc >= dt1.AddDays(-1) && x.bcoc_sfecha_doc <= dt2.AddDays(1)
                    ).ToList();
        }

        private void txtNroDoc_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();

        }

        private void dtFechaFin_EditValueChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(dtFechaFin.Text))
                return;
            dt2 = Convert.ToDateTime(dtFechaFin.Text);
            filtrar();
        }

        private void viewDocCompra_DoubleClick(object sender, EventArgs e)
        {
            ver();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNroDoc.Text = String.Empty;
            dtFechaInicio.EditValue = null;
            dtFechaInicio.Text = String.Empty;
            dtFechaFin.EditValue = null;
            dtFechaFin.Text = String.Empty;
            grdDocCompra.DataSource = lstBocCompra;
        }

        private void txtProveedor_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //lstFacCompra.ForEach(x =>
            //{
            //    frmManteFacturaCompra frm = new frmManteFacturaCompra();
            //    frm.MiEvento += new frmManteFacturaCompra.DelegadoMensaje(reload);
            //    frm.Obe = x;
            //    frm.SetModify();
            //    frm.Show();
            //    frm.setValues();
            //    frm.setGuardar();
            //});
        }


    }
}