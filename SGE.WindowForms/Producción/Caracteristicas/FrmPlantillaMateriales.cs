using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Otros.Produccion;
using SGE.WindowForms.Otros.Producción;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SGE.WindowForms.Producción.Caracteristicas
{
    public partial class FrmPlantillaMateriales : DevExpress.XtraEditors.XtraForm
    {
        private List<EPlantillaMateriales> mlist = new List<EPlantillaMateriales>();
        private int xposition = 0;
        public int icodModelo;
        public FrmPlantillaMateriales()
        {
            InitializeComponent();
        }
        public void Carga()
        {
            mlist = new BCentral().ListarPlantillaMateriales(icodModelo);
            dgrMotonave.DataSource = mlist;
        }

        void form2_MiEvento(int intIcod)
        {
            Carga();
            int index = mlist.FindIndex(x => x.fited_icod_item_ficha_tecnica == intIcod);
            viewMotonave.FocusedRowHandle = index;
            viewMotonave.Focus();
        }

        void Modify()
        {
            Carga();
            viewMotonave.FocusedRowHandle = xposition;
        }
        void AddEvent()
        {
            this.viewMotonave.DoubleClick += new System.EventHandler(this.viewMotonave_DoubleClick);
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                frmMantePlantillaMateriales frmdetalle = new frmMantePlantillaMateriales();
                frmdetalle.MiEvento += new frmMantePlantillaMateriales.DelegadoMensaje(form2_MiEvento);
                viewMotonave.MoveLast();
                frmdetalle.txtitem.Text = (mlist.Count + 1).ToString();
                frmdetalle.icodModelo = icodModelo;
                frmdetalle.btnModificar.Enabled = false;
                frmdetalle.Show();
                frmdetalle.SetInsert();
                frmdetalle.btnAgregar.Enabled = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Datos()
        {

            EPlantillaMateriales obe = (EPlantillaMateriales)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (obe == null)
                return;
            frmMantePlantillaMateriales frmdetalle = new frmMantePlantillaMateriales();
            frmdetalle.MiEvento += new frmMantePlantillaMateriales.DelegadoMensaje(form2_MiEvento);
            frmdetalle._BE = obe;
            frmdetalle.SetModify();
            frmdetalle.Show();
            frmdetalle.txtitem.Text = obe.fited_iitem_ficha_tecnica.ToString();
            frmdetalle.lkpArea.EditValue = obe.fited_iarea;
            frmdetalle.lkpUbicacion.EditValue = obe.fited_iubicacion;
            frmdetalle.txtDescripcion.Text = obe.fited_vdescripcion;
            frmdetalle.bteProducto.Tag = obe.prdc_icod_producto;
            frmdetalle.bteProducto.Text = obe.strCodeProducto;
            frmdetalle.txtDescProd.Text = obe.strProducto;
            frmdetalle.txtUM.Text = obe.strUnidadMedida;
            frmdetalle.txtCantidad.Text = Convert.ToString(obe.fited_ncantidad);
            //frmdetalle.btnAgregar.Enabled = false;


        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datos();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Desea Eliminar el registro", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {

                EFichaTecnicaMateriales Obe = (EFichaTecnicaMateriales)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);

                new BCentral().EliminarFichaTecnicaMateriales(Obe);
                viewMotonave.DeleteRow(viewMotonave.FocusedRowHandle);
                viewMotonave.RefreshData();

            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PreViewPrint();
        }

        private void FrmMotonave_Load(object sender, EventArgs e)
        {
            Carga();
        }

        //public void PreViewPrint()
        //{
        //    CompositeLink oClk = new CompositeLink(new PrintingSystem());
        //    oClk.Margins.Bottom = 0;
        //    oClk.Margins.Left = 0;
        //    oClk.Margins.Right = 0;
        //    oClk.Margins.Top = 0;
        //    Link oLkh = new Link();
        //    oLkh.CreateDetailArea += CreateHeaderArea;
        //    PrintableComponentLink oPcld = new PrintableComponentLink(new PrintingSystem());
        //    oPcld.Component = dgrMotonave;
        //    oClk.Links.Add(oLkh);
        //    oClk.Links.Add(oPcld);
        //    oClk.ShowPreviewDialog();

        //}

        //private void CreateHeaderArea(object sender, CreateAreaEventArgs e)
        //{
        //    BrickGraphics oBg = e.Graph;
        //    oBg.BackColor = Color.Transparent;
        //    oBg.PageUnit = GraphicsUnit.Millimeter;
        //    oBg.StringFormat = new BrickStringFormat(StringAlignment.Near, StringAlignment.Near);
        //    TextBrick txbData = default(TextBrick);

        //    oBg.Font = new Font("Tahoma", 9);
        //    txbData = oBg.DrawString("INTERLOON S.A.C - AÑO " + DateTime.Now.Year.ToString(), Color.Black, new RectangleF(5, 10, 50, 4), DevExpress.XtraPrinting.BorderSide.None);
        //    txbData = oBg.DrawString("ALMACENES", Color.Black, new RectangleF(18, 15, 50, 4), DevExpress.XtraPrinting.BorderSide.None);
        //    txbData = oBg.DrawString("FECHA : " + DateTime.Now.ToShortDateString(), Color.Black, new RectangleF(169, 10, 40, 4), DevExpress.XtraPrinting.BorderSide.None);
        //    txbData = oBg.DrawString("HORA : " + DateTime.Now.ToShortTimeString(), Color.Black, new RectangleF(169, 15, 30, 4), DevExpress.XtraPrinting.BorderSide.None);
        //    BrickGraphics oB = e.Graph;
        //    oB.StringFormat = new BrickStringFormat(StringAlignment.Center, StringAlignment.Center);
        //    txbData = oB.DrawString("LISTADO DE TIPO", Color.Black, new RectangleF(55, 28, 100, 4), DevExpress.XtraPrinting.BorderSide.None);
        //    txbData = oB.DrawString(string.Empty, Color.Black, new RectangleF(162, 36, 37, 4), DevExpress.XtraPrinting.BorderSide.None);
        //}

        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (mlist.Count > 0)
                {
                    FrmManteModelo Motonave = new FrmManteModelo();
                    Motonave.MiEvento += new FrmManteModelo.DelegadoMensaje(AddEvent);
                    Motonave.Show();
                    Motonave.SetCancel();
                    EProdModelo Obe = (EProdModelo)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                    Motonave.txtcodigo.Text = Obe.mo_viid_modelo;
                    Motonave.txtdescripcion.Text = Obe.mo_vdescripcion;
                    if (Obe.mo_ruta != null)
                        Motonave.pictureEdit1.Image = Image.FromFile(Obe.mo_ruta);
                    Motonave.BtnGuardar.Enabled = false;
                    Motonave.txtcodigo.Enabled = false;
                }
                this.viewMotonave.DoubleClick -= new System.EventHandler(this.viewMotonave_DoubleClick);
            }
            catch (Exception ex)
            {
            }
        }

        private void FrmMotonave_Activated(object sender, EventArgs e)
        {

        }
        private void BuscarCriterio()
        {
            //dgrMotonave.DataSource = mlist.Where(obj =>
            //                                       obj.mo_vdescripcion.ToUpper().Contains(textEdit1.Text.ToUpper()) &&
            //                                       obj.mo_viid_modelo.ToString().Contains(txtCodigo.Text.ToUpper())
            //                                 ).ToList();

        }
        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void viewMotonave_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
                nuevoToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F5)
                Datos();
            if (e.KeyCode == Keys.F9)
                eliminarToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F8) { }
            //PreViewPrint();
        }
    }
}