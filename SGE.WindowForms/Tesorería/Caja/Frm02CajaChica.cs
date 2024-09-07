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
    public partial class Frm02CajaChica : DevExpress.XtraEditors.XtraForm
    {
        private List<ECajaChica> mlist = new List<ECajaChica>();
        public Frm02CajaChica()
        {
            InitializeComponent();
        }
        private void Carga()
        {
            mlist = new BTesoreria().ListarCajaChica();
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
            FrmManteCajaChica frm = new FrmManteCajaChica();
            frm.MiEvento += new FrmManteCajaChica.DelegadoMensaje(form2_MiEvento);
            frm.oDetail = mlist;
            frm.txtNumeroCaja.Text = (mlist.Count > 0) ? mlist.Max(x => Convert.ToInt32(x.vnro_caja_liquida) + 1).ToString() : "1";
            frm.Show();
            frm.SetInsert();
        }

        private void Datos(string TypE)
        {
            ECajaChica Obe = (ECajaChica)viewCajaChica.GetRow(viewCajaChica.FocusedRowHandle);
            if (Obe != null)
            {
                FrmManteCajaChica frm = new FrmManteCajaChica();
                frm.MiEvento += new FrmManteCajaChica.DelegadoMensaje(Modify);
                frm.oDetail = mlist;
                frm.Show();
                frm.Correlative = Obe.icod_caja_liquida;
                frm.txtNumeroCaja.Text = Obe.vnro_caja_liquida;
                frm.txtDescripcion.Text = Obe.vdescrip_caja_liquida;
                frm.txtResponsable.Text = Obe.vnom_responsable;
                frm.lkpMoneda.EditValue = Obe.iid_tipo_moneda;
                frm.bteCuenta.Tag = Obe.iid_cuenta_contable;
                frm.bteCuenta.Text = Obe.iid_cuenta_contable.ToString();
                frm.txtCuentaDes.Text = Obe.vdescripcion_cuenta_contable;
                frm.bteAnalitica.Tag = Obe.tblc_tipo_analitica;
                frm.bteAnalitica.Text = string.Format("{0:00}", Obe.tblc_tipo_analitica);
                frm.bteSubAnalitica.Tag = Obe.icod_analitica;
                frm.bteSubAnalitica.Text = Obe.anac_vdescripcion;
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
                ECajaChica Obe = (ECajaChica)viewCajaChica.GetRow(viewCajaChica.FocusedRowHandle);
                BTesoreria oBl = new BTesoreria();

                oBl.EliminarCajaChica(Obe);
                viewCajaChica.DeleteRow(viewCajaChica.FocusedRowHandle);
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }
        private void imprimir()
        {
            if (mlist.Count > 0)
            {
                rpt02CajaChica rpt = new rpt02CajaChica();
                rpt.carga(mlist);
            }
        }
        private void BuscarCriterio()
        {
            grdCajaChica.DataSource = mlist.Where(obj =>
                                                   obj.vdescrip_caja_liquida.ToUpper().Contains(txtDescripcion.Text.ToUpper()) &&
                                                   obj.vnro_caja_liquida.ToUpper().Contains(txtNroCaja.Text.ToUpper())
                                             ).ToList();

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
            if (e.KeyCode == Keys.F8)
                imprimir();
        }
    }
}