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
//using SGE.WindowForms.Planillas;



namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmImportacionFactura : DevExpress.XtraEditors.XtraForm
    {
        List<EImportacionFactura> lstImpFactura = new List<EImportacionFactura>();
        public int codImp = 0;
        public EImportacion ObeI = new EImportacion();
        public decimal sumSol = 0;
        public decimal sumDol = 0;
        public decimal convSol = 0;
        public decimal convDol = 0;

        public frmImportacionFactura()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstImpFactura = new BCompras().ListarImportacionFactura(codImp);
            grdAlmacen.DataSource = lstImpFactura;
            viewAlmacen.Focus();
            decimal monto_detalle = 0;
            monto_detalle = lstImpFactura.Sum(x => Convert.ToDecimal(x.fcoc_nmonto_total_detalle));
            TotalDolares();
            TotalSoles();

        }

        private void TotalSoles()
        {
            decimal TotalSoles = 0;
            decimal Convertir = 0;
            decimal TotalSolesConvertir = 0;
            decimal TotalSolesConvertir_SQL = 0;
            if (lstImpFactura.Sum(x => x.fcoc_nmonto_total_detalle) == 0)
            {
                TotalSolesConvertir_SQL = 0;
            }
            else
            {
                Convertir = lstImpFactura.Where(x => Convert.ToDecimal(x.tablc_iid_tipo_moneda) == 4).Sum(x => Convert.ToDecimal(x.fcoc_nmonto_total_detalle));
                TotalSolesConvertir = Convertir * Convert.ToDecimal(lstImpFactura[0].fcoc_nmonto_tipo_cambio);
                TotalSoles = lstImpFactura.Where(x => Convert.ToDecimal(x.tablc_iid_tipo_moneda) == 3).Sum(x => Convert.ToDecimal(x.fcoc_nmonto_total_detalle));
                TotalSolesConvertir_SQL = (TotalSoles + TotalSolesConvertir);
            }



            new BCompras().modificarImportacionConceptosMontoSoles(codImp, TotalSolesConvertir_SQL);
        }
        private void TotalDolares()
        {
            decimal TotalDolares = 0;
            decimal Convertir = 0;
            decimal TotalDolaresConvertir = 0;
            decimal TotalDolaresConvertir_SQL = 0;

            if (lstImpFactura.Sum(x => x.fcoc_nmonto_total_detalle) == 0)
            {
                TotalDolaresConvertir_SQL = 0;
            }
            else
            {
                Convertir = lstImpFactura.Where(x => Convert.ToDecimal(x.tablc_iid_tipo_moneda) == 3).Sum(x => Convert.ToDecimal(x.fcoc_nmonto_total_detalle));
                TotalDolaresConvertir = Convertir / Convert.ToDecimal(lstImpFactura[0].fcoc_nmonto_tipo_cambio);
                TotalDolares = lstImpFactura.Where(x => Convert.ToDecimal(x.tablc_iid_tipo_moneda) == 4).Sum(x => Convert.ToDecimal(x.fcoc_nmonto_total_detalle));
                TotalDolaresConvertir_SQL = (TotalDolares + TotalDolaresConvertir);
            }



            new BCompras().modificarImportacionConceptosMontoDolares(codImp, TotalDolaresConvertir_SQL);
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstImpFactura.FindIndex(x => x.impd1_icod_import_factura == intIcod);
            viewAlmacen.FocusedRowHandle = index;
            viewAlmacen.Focus();

        }
        private void nuevo()
        {
            if (ObeI.tablc_iid_sit_import == 333)
            {
                frmRegistroImpFactura frm = new frmRegistroImpFactura();
                frm.MiEvento += new frmRegistroImpFactura.DelegadoMensaje(reload);
                frm.lstImpFactura = lstImpFactura;
                frm.SetInsert();
                frm.codImp = codImp;
                frm.ShowDialog();
                cargar();
                frm.ActualizarCostos();
                frm.ActualizarImportacion();
                //TotalDolares();
                //TotalSoles();
            }
            else
            {
                XtraMessageBox.Show("Importacion Se Encuentra Ingresado al Kardex", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void modificar()
        {
            //EAreas Obe = (EAreas)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            //if (Obe == null)
            //    return;
            //frmRegistroAreas frm = new frmRegistroAreas();
            //frm.MiEvento += new frmRegistroAreas.DelegadoMensaje(reload);
            //frm.lstAreas = lstAreas;
            //frm.Obe = Obe;
            //frm.SetModify();
            //frm.setValues();
            //frm.ShowDialog();

            //frm.txtDescripcion.Focus();
        }

        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {

        }

        private void eliminar()
        {
            try
            {
                EImportacionFactura Obe = (EImportacionFactura)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewAlmacen.FocusedRowHandle;
                if (ObeI.tablc_iid_sit_import == 333)
                {
                    if (XtraMessageBox.Show("¿Esta seguro que desea eliminar la Factura de Importación " + Obe.fcoc_num_doc + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        Obe.intUsuario = Valores.intUsuario;
                        Obe.strPc = WindowsIdentity.GetCurrent().Name;
                        new BCompras().eliminarImportacionFactura(Obe);
                        new BCompras().ACTUALIZAR_FAC_COMPRA_IMP_FACT(Convert.ToInt32(Obe.fcoc_icod_doc), 0, 0);
                        //TotalDolares();
                        //TotalSoles();
                        cargar();
                        if (lstImpFactura.Count >= index + 1)
                            viewAlmacen.FocusedRowHandle = index;
                        else
                            viewAlmacen.FocusedRowHandle = index - 1;


                    }
                }
                else
                {
                    XtraMessageBox.Show("Importacion Se Encuentra Ingresado al Kardex", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void buscarCriterio()
        {
            grdAlmacen.DataSource = lstImpFactura.Where(x =>
                                                   x.proc_vnombrecompleto.Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.fcoc_num_doc.Contains(txtDescripcion.Text.ToUpper())
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



        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void viewAlmacen_DoubleClick(object sender, EventArgs e)
        {
            //EImportacionFactura Obe = (EImportacionFactura)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            //if (Obe == null)
            //    return;
            //frmRegistroAreas frm = new frmRegistroAreas();
            //frm.Obe = Obe;
            //frm.SetCancel();
            //frm.setValues();
            //frm.ShowDialog();

        }







    }
}