using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class FrmConsultaSubdiario : DevExpress.XtraEditors.XtraForm
    {
        private List<ESubDiario> Lista = new List<ESubDiario>();
        public ESubDiario obeSubdiario;

        public FrmConsultaSubdiario()
        {
            InitializeComponent();
        }

        public FrmConsultaSubdiario(string titulo)
        {
            InitializeComponent();
            this.Text += titulo;
        }

        private void FrmConsultaSubdiario_Load(object sender, EventArgs e)
        {
            Carga();
        }

        private void Carga()
        {
            Lista = new BContabilidad().listarSubDiario();
            Lista.Add(new ESubDiario() { subdi_icod_subdiario = 0, subdi_vdescripcion = "NINGUNO" });
            //Lista = Lista.OrderBy(obe => obe.idf_SubDiario).ToList();
            grd.DataSource = Lista;
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Aceptar();
        }

        private void grd_DoubleClick(object sender, EventArgs e)
        {
            Aceptar();
        }

        private void Aceptar()
        {
            if (gv.RowCount > 0)
            {
                obeSubdiario = (ESubDiario)gv.GetRow(gv.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void txtCodigo_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCodigo.ContainsFocus)
            {
                txtDescripcion.Text = string.Empty;
                Buscar();
            }
        }

        private void txtDescripcion_EditValueChanged(object sender, EventArgs e)
        {
            if (txtDescripcion.ContainsFocus)
            {
                txtCodigo.Text = string.Empty;
                Buscar();
            }
        }

        private void Buscar()
        {
            string cod = txtCodigo.Text, desc = txtDescripcion.Text;
            grd.DataSource = Lista;//Where(obe => ((cod != string.Empty) ? obe.idf_SubDiario.Contains(cod) : obe.subdi_vdescripcion.Contains(desc))).ToList();
        }

        private void btnPrev_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gv.FocusedRowHandle == 0)
                gv.MoveLast();
            else
                gv.MovePrev();
        }

        private void btnNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gv.FocusedRowHandle == gv.RowCount - 1)
                gv.MoveFirst();
            else
                gv.MoveNext();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}