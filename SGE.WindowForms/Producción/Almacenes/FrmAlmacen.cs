using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace SGE.WindowForms.Producción.Almacenes
{

    public partial class FrmAlmacen : DevExpress.XtraEditors.XtraForm
    {
        private List<EProdAlmacen> mlist = new List<EProdAlmacen>();
        private int xposition = 0;

        public FrmAlmacen()
        {
            InitializeComponent();
        }

        private void FrmAlmacen_Load(object sender, EventArgs e)
        {
            Carga();
        }
        void form2_MiEvento()
        {
            Carga();
        }

        private void Carga()
        {
            mlist = new BCentral().ListarProdAlmacen();
            dgrAlmacen.DataSource = mlist;
        }
        void Modify()
        {
            Carga();
            viewAlmacen.FocusedRowHandle = xposition;
        }
        void AddEvent()
        {
            this.viewAlmacen.DoubleClick += new System.EventHandler(this.viewAlmacen_DoubleClick);
        }
        private void nuevo()
        {
            FrmManteAlmacen Almacen = new FrmManteAlmacen();
            Almacen.MiEvento += new FrmManteAlmacen.DelegadoMensaje(form2_MiEvento);
            viewAlmacen.MoveLast();
            EProdAlmacen Obe = (EProdAlmacen)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            int i = 0;
            if (mlist.Count > 0)
                i = mlist.Max(ob => Convert.ToInt32(ob.idf_almacen));
            Almacen.Correlative = Convert.ToInt32(i) + 1;
            Almacen.oDetail = mlist;
            Almacen.Show();
            Almacen.txtCodigo.Enabled = true;
            Almacen.SetInsert();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datos();
        }
        private void Datos()
        {
            FrmManteAlmacen Almacen = new FrmManteAlmacen();
            Almacen.MiEvento += new FrmManteAlmacen.DelegadoMensaje(Modify);
            Almacen.oDetail = mlist;
            Almacen.Show();
            EProdAlmacen Obe = (EProdAlmacen)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            Almacen.Correlative = Obe.id_almacen;
            Almacen.lkp_PuntoVenta.EditValue = Obe.puvec_icod_punto_venta;
            Almacen.txtCodigo.Text = Obe.idf_almacen.ToString();
            Almacen.txtDescripcion.Text = Obe.descripcion;
            Almacen.txtDireccion.Text = Obe.direccion;
            Almacen.txtRepresentante.Text = Obe.representante;
            Almacen.txtCodigo.Enabled = false;
            Almacen.txtUbigeo.Text = Obe.codigo_ubigeo;
            Almacen.SetModify();
            xposition = viewAlmacen.FocusedRowHandle;
        }
        private void eliminar()
        {
            BCentral oblVerificar = new BCentral();
            int acumulador = 0;
            try
            {
                if (XtraMessageBox.Show("Desea Eliminar el registro", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    EProdAlmacen Obe = (EProdAlmacen)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                    BCentral oBl = new BCentral();
                    if (oblVerificar.VerificarProdProdMovAlmacen(1, Obe.id_almacen) == 0)
                    {
                        acumulador = acumulador + 1;
                    }
                    if (oblVerificar.VerificarProdProdMovAlmacen(2, Obe.id_almacen) == 0)
                    {
                        acumulador = acumulador + 1;
                    }
                    if (oblVerificar.VerificarProdProdMovAlmacen(3, Obe.id_almacen) == 0)
                    {
                        acumulador = acumulador + 1;
                    }
                    if (oblVerificar.VerificarProdProdMovAlmacen(4, Obe.id_almacen) == 0)
                    {
                        acumulador = acumulador + 1;
                    }
                    if (acumulador > 0)
                    {
                        oBl.EliminarProdAlmacen(Obe.id_almacen);
                        viewAlmacen.DeleteRow(viewAlmacen.FocusedRowHandle);
                    }
                    else
                    {
                        XtraMessageBox.Show("No se puede Eliminar: el ALmacén tiene Movimientos", "Informacion del Sitema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Informacion del Sitema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void viewAlmacen_DoubleClick(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                FrmManteAlmacen Almacen = new FrmManteAlmacen();
                Almacen.MiEvento += new FrmManteAlmacen.DelegadoMensaje(AddEvent);
                Almacen.SetCancel();
                EProdAlmacen Obe = (EProdAlmacen)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                Almacen.Show();
                Almacen.txtCodigo.Text = Obe.idf_almacen.ToString();
                Almacen.txtDescripcion.Text = Obe.descripcion;
                Almacen.txtDireccion.Text = Obe.direccion;
                Almacen.txtRepresentante.Text = Obe.representante;
                Almacen.btnGuardar.Enabled = false;
            }
            this.viewAlmacen.DoubleClick -= new EventHandler(this.viewAlmacen_DoubleClick);
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }
        private void BuscarCriterio()
        {

            dgrAlmacen.DataSource = mlist.Where(obj =>
                                                   obj.descripcion.ToUpper().Contains(textEdit1.Text.ToUpper()) &&
                                                   obj.idf_almacen.ToUpper().Contains(txtCodigo.Text.ToUpper())
                                             ).ToList();

        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Imprimir();
        }

        private void viewAlmacen_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
                nuevoToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F5)
                Datos();
            if (e.KeyCode == Keys.F9)
                eliminarToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F8) { Imprimir(); }

        }
        private void Imprimir()
        {
            if (mlist.Count > 0)
            {
                //rptAlmacen rptAlmacen = new rptAlmacen();
                //rptAlmacen.cargar(mlist);
            }
        }



        private void btnAGREGAR_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnmodificar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Datos();
        }

        private void btnsalir_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void btnImprimir_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Imprimir();
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}