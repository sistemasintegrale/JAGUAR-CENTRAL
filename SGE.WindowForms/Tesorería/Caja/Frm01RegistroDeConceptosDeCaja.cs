using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Otros.Tesoreria.Caja;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace SGE.WindowForms.Tesorería.Caja
{
    public partial class Frm01RegistroDeConceptosDeCaja : DevExpress.XtraEditors.XtraForm
    {
        private List<EConceptoMovCaja> mlist = new List<EConceptoMovCaja>();
        public Frm01RegistroDeConceptosDeCaja()
        {
            InitializeComponent();
        }

        private void Carga()
        {
            mlist = new BTesoreria().ListarConceptoCaja();
            grdConceptoCaja.DataSource = mlist;
        }

        void form2_MiEvento()
        {
            Carga();
        }
        void Modify()
        {
            Carga();
        }

        private void FrmRegistroDeConceptosDeCaja_Load(object sender, EventArgs e)
        {
            Carga();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteConceptoDeCaja frm = new FrmManteConceptoDeCaja();
            frm.MiEvento += new FrmManteConceptoDeCaja.DelegadoMensaje(form2_MiEvento);
            frm.oDetail = mlist;
            frm.Show();
            frm.SetInsert();
        }


        private void Datos(string TypE)
        {
            EConceptoMovCaja Obe = (EConceptoMovCaja)viewConceptoCaja.GetRow(viewConceptoCaja.FocusedRowHandle);
            if (Obe != null)
            {
                FrmManteConceptoDeCaja frm = new FrmManteConceptoDeCaja();
                frm.MiEvento += new FrmManteConceptoDeCaja.DelegadoMensaje(Modify);
                frm.oDetail = mlist;
                frm.Show();
                frm.Correlative = Obe.icod_concepto_caja;
                frm.txtCod.Text = Obe.ccod_concep_mov;
                frm.txtDescripcion.Text = Obe.vdescripcion;
                frm.bteTipoDoc.Tag = Obe.tdocc_icod_tipo_doc;
                frm.bteTipoDoc.Text = Obe.tdocc_vabreviatura_tipo_doc;
                frm.bteClaseDoc.Tag = Obe.iid_correlativo;
                frm.bteClaseDoc.Text = Obe.tdodc_iid_codigo_doc_det;
                frm.bteCuenta.Text = Obe.iid_cuenta_contable.ToString();
                frm.bteCuenta.Tag = Obe.iid_cuenta_contable;
                frm.txtCuentaDes.Text = Obe.cuenta_vdes;
                if (TypE == "Modificar")
                {
                    frm.SetModify();
                    //frm.bteTipoDoc.Enabled = (Obe.iid_correlativo == null) ? false : true;
                    //frm.bteClaseDoc.Enabled = (Obe.iid_correlativo == null) ? false : true;
                    //frm.bteCuenta.Enabled = (Obe.iid_cuenta_contable == null) ? false : true;
                }
                else
                {
                    frm.SetCancel();
                }
            }
        }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Desea Eliminar el registro", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                EConceptoMovCaja Obe = (EConceptoMovCaja)viewConceptoCaja.GetRow(viewConceptoCaja.FocusedRowHandle);
                BTesoreria oBl = new BTesoreria();

                oBl.EliminarMovCaja(Obe);
                viewConceptoCaja.DeleteRow(viewConceptoCaja.FocusedRowHandle);
            }
        }
        private void viewConceptoCajaChica_DoubleClick(object sender, EventArgs e)
        {
            Datos("");
        }
        //private void FrmEntidadFinanciera_Activated(object sender, EventArgs e)
        //{
        //    ((FrmMain)MdiParent).oInstance = this;
        //}
        private void viewEntidadFinanciera_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
                nuevoToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F5)
                Datos("Modificar");
            if (e.KeyCode == Keys.F9)
                eliminarToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F8)
                imprimir();
        }

        private void modificarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Datos("Modificar");
        }

        private void imprimirToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            imprimir();
        }
        private void imprimir()
        {
            if (mlist.Count > 0)
            {
                rpt01ConceptoCaja rpt = new rpt01ConceptoCaja();
                rpt.carga(mlist);
            }
        }

        private void eliminarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Desea Eliminar el registro", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                EConceptoMovCaja Obe = (EConceptoMovCaja)viewConceptoCaja.GetRow(viewConceptoCaja.FocusedRowHandle);
                BTesoreria oBl = new BTesoreria();

                oBl.EliminarMovCaja(Obe);
                viewConceptoCaja.DeleteRow(viewConceptoCaja.FocusedRowHandle);
            }
        }
        private void BuscarCriterio()
        {
            grdConceptoCaja.DataSource = mlist.Where(obj =>
                                                   obj.ccod_concep_mov.ToUpper().Contains(txtCodigo.Text.ToUpper()) &&
                                                   obj.vdescripcion.ToUpper().Contains(txtDecripcion.Text.ToUpper())
                                             ).ToList();

        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void txtDecripcion_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }
    }
}