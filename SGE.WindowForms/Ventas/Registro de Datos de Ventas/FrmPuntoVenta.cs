using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class FrmPuntoVenta : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        public List<EPuntoVenta> lista = new List<EPuntoVenta>();

        private void Carga()
        {
            lista = new BCentral().ListarPuntoVenta();
            dgr.DataSource = lista;
        }

        private void BuscarCriterio()
        {
            dgr.DataSource = lista.Where(obj =>
                                                   string.Format("{0:00}", obj.puvec_vcod_punto_venta).Contains(txtCodigo.Text.ToUpper()) &&
                                                   obj.puvec_vdescripcion.ToUpper().Contains(txtGiro.Text.ToUpper())
                                             ).ToList();
        }

        public FrmPuntoVenta()
        {
            InitializeComponent();
        }

        private void FrmPuntoVenta_Load(object sender, EventArgs e)
        {
            Carga();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }
        void form2_MiEvento()
        {
            Carga();
        }

        void Modify()
        {
            Carga();
            gridView1.FocusedRowHandle = xposition;
        }
        private void nuevo()
        {

            FrmMantePuntoVenta frm = new FrmMantePuntoVenta();
            frm.MiEvento += new FrmMantePuntoVenta.DelegadoMensaje(reload);
            if (lista.Count > 0)
                frm.txtCodigoPuntoVenta.Text = String.Format("{0:00}", lista.Max(x => Convert.ToInt32(x.puvec_vcod_punto_venta) + 1));
            else
                frm.txtCodigoPuntoVenta.Text = "01";
            frm.lista = lista;
            frm.SetInsert();
            frm.Show();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lista.Count > 0)
            {
                Datos();
            }
        }
        private void Datos()
        {
            EPuntoVenta Obe = (EPuntoVenta)gridView1.GetRow(gridView1.FocusedRowHandle);
            if (Obe != null)
            {
                //if (Obe.puvec_icod_punto_venta != ValPVTC.puvec_icod_punto_venta)
                //{
                FrmMantePuntoVenta frm = new FrmMantePuntoVenta();
                frm.MiEvento += new FrmMantePuntoVenta.DelegadoMensaje(reload);
                frm.lista = lista;
                frm.oBe = Obe;
                frm.Show();
                frm.setValues();
                frm.SetModify();

                //}
                //else
                //{
                //    XtraMessageBox.Show("El punto de venta Central no puede ser modificado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //}
            }
        }
        void reload(int intIcod)
        {
            Carga();
            int index = lista.FindIndex(x => x.puvec_icod_punto_venta == intIcod);
            gridView1.FocusedRowHandle = index;
            gridView1.Focus();
        }
        private void eliminar()
        {

            if (lista.Count > 0)
            {
                EPuntoVenta obe = (EPuntoVenta)gridView1.GetRow(gridView1.FocusedRowHandle);
                if (obe == null)
                    return;
                int index = gridView1.FocusedRowHandle;

                if (XtraMessageBox.Show("¿Está seguro de eliminar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {


                    new BCentral().EliminarPuntoVenta(obe);
                    Carga();
                    if (lista.Count >= index + 1)
                        gridView1.FocusedRowHandle = index;
                    else
                        gridView1.FocusedRowHandle = index - 1;
                }

            }
        }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (lista.Count > 0)
            {
                FrmMantePuntoVenta PuntoVenta = new FrmMantePuntoVenta();
                PuntoVenta.MiEvento += new FrmMantePuntoVenta.DelegadoMensaje(reload); ;
                EPuntoVenta Obe = (EPuntoVenta)gridView1.GetRow(gridView1.FocusedRowHandle);
                PuntoVenta.Show();
                PuntoVenta.SetCancel();
                PuntoVenta.btnGuardar.Enabled = false;
                PuntoVenta.Correlative = Obe.puvec_icod_punto_venta;
                PuntoVenta.txtCodigoPuntoVenta.Text = (Obe.puvec_vcod_punto_venta).ToString();
                PuntoVenta.lkpsituacion.EditValue = Obe.puvec_iactivo;
                PuntoVenta.txtPuntoVenta.Text = Obe.puvec_vdescripcion;
            }
        }

        private void gridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
                nuevoToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F5)
                modificarToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F9)
                eliminarToolStripMenuItem_Click(null, null);
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }
        private void imprimir()
        {
            if (lista.Count > 0)
            {
                //rptPuntoVenta rpt = new rptPuntoVenta();
                //rpt.cargar(lista);
            }
        }

        private void brnagregar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnmosdificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Datos();
        }

        private void btneliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void btnimprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }


    }
}