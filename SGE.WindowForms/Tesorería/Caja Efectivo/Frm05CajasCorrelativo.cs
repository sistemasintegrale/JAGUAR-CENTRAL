using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Otros.Tesoreria.Caja;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace SGE.WindowForms.Tesorería.Caja_Efectivo
{
    public partial class Frm05CajasCorrelativo : DevExpress.XtraEditors.XtraForm
    {
        private List<ECajaCorrelativo> mlist = new List<ECajaCorrelativo>();
        public Frm05CajasCorrelativo()
        {
            InitializeComponent();
        }
        private void Carga()
        {
            mlist = new BTesoreria().listarCajasCorrelativo();
            grdCajaChica.DataSource = mlist;
        }
        void form2_MiEvento()
        {
            Carga();
        }
        void Modify()
        {
            Carga();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteCajasCorrelativos frm = new FrmManteCajasCorrelativos();
            frm.MiEvento += new FrmManteCajasCorrelativos.DelegadoMensaje(form2_MiEvento);
            frm.oDetail = mlist;
            //frm.txtNumeroCaja.Text = (mlist.Count > 0) ? mlist.Max(x => Convert.ToInt32(x.vnro_caja_liquida) + 1).ToString() : "1";
            frm.Show();
            frm.SetInsert();
        }

        private void Datos(string TypE)
        {
            ECajaCorrelativo Obe = (ECajaCorrelativo)viewCajaChica.GetRow(viewCajaChica.FocusedRowHandle);
            if (Obe != null)
            {
                FrmManteCajasCorrelativos frm = new FrmManteCajasCorrelativos();
                frm.MiEvento += new FrmManteCajasCorrelativos.DelegadoMensaje(Modify);
                frm.oDetail = mlist;
                frm.Show();
                frm.Correlative = Obe.cc_icod_caja_correlativo;
                frm.lkpCaja.EditValue = Obe.bcoc_icod_banco;
                frm.lkpCuenta.EditValue = Obe.bcod_icod_banco_cuenta;
                frm.lkpTipoDoc.EditValue = Obe.tdocc_icod_tipo_doc;
                frm.txtCorrelativo.Text = Obe.cc_inumero_correlativo.ToString();
                frm.lkpTipo.EditValue = Obe.cc_icod_tipo;
                //frm.txtCuentaDes.Text = Obe.vdescripcion_cuenta_contable;
                if (TypE == "Modificar")
                    frm.SetModify();
                else
                    frm.SetCancel();
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datos("Modificar");
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Desea Eliminar el registro", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                ECajaCorrelativo Obe = (ECajaCorrelativo)viewCajaChica.GetRow(viewCajaChica.FocusedRowHandle);
                BTesoreria oBl = new BTesoreria();

                oBl.eliminarCajasCorrelativo(Obe);
                viewCajaChica.DeleteRow(viewCajaChica.FocusedRowHandle);
            }
        }
        private void BuscarCriterio()
        {
            //grdCajaChica.DataSource = mlist.Where(obj =>
            //                                       obj.vdescrip_caja_liquida.ToUpper().Contains(txtDescripcion.Text.ToUpper()) &&
            //                                       obj.vnro_caja_liquida.ToUpper().Contains(txtNroCaja.Text.ToUpper())
            //                                 ).ToList();

        }
        private void FrmCajaChica_Load(object sender, EventArgs e)
        {
            Carga();
        }

        private void viewCajaChica_DoubleClick(object sender, EventArgs e)
        {
            Datos("");
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void viewCajaChica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
                nuevoToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F5)
                Datos("Modificar");
            if (e.KeyCode == Keys.F9)
                eliminarToolStripMenuItem_Click(null, null);
        }
    }
}