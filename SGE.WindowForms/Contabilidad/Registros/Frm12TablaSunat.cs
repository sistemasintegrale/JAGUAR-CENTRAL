using SGE.Entity;
//using SGE.BusinessLogic.Comunes;
using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Registros
{
    public partial class Frm12TablaSunat : DevExpress.XtraEditors.XtraForm
    {
        List<ETablaSunatCab> mlist = new List<ETablaSunatCab>();

        public Frm12TablaSunat()
        {
            InitializeComponent();
        }

        private void Frm12TablaSunat_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            //mlist = new BTablasSunat().TablasSunatListar();
            //grdTablaSunat.DataSource = mlist;

        }
        private void BuscarCriterio()
        {
            //grdTablaSunat.DataSource = mlist.Where(x => x.suntc_codigo.ToUpper().Contains(txtCodigo.Text.ToUpper()) &&
            //    x.suntc_vdescripcion.ToUpper().Contains(txtDescripcion.Text.ToUpper())).ToList();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteTablaSuntaCab frm = new FrmManteTablaSuntaCab();
            frm.txtCodigo.Text = (Convert.ToInt32(mlist.Max(x => x.suntc_codigo)) + 1).ToString();
            frm.mlist = mlist;
            frm.SetInsert();
            if (frm.ShowDialog() == DialogResult.OK)
                cargar();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ETablaSunatCab Obe = (ETablaSunatCab)viewTablaSunat.GetRow(viewTablaSunat.FocusedRowHandle);
            if (Obe != null)
            {
                FrmManteTablaSuntaCab frm = new FrmManteTablaSuntaCab();
                frm.txtCodigo.Text = Obe.suntc_codigo;
                frm.txtCodigo.Tag = Obe.suntc_icod;
                frm.txtDescripcion.Text = Obe.suntc_vdescripcion;
                frm.mlist = mlist;
                frm.SetModify();
                if (frm.ShowDialog() == DialogResult.OK)
                    cargar();
            }

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ETablaSunatCab Obe = (ETablaSunatCab)viewTablaSunat.GetRow(viewTablaSunat.FocusedRowHandle);
            //if (Obe != null)
            //{
            //    List<ETablaSunatDet> List = new List<ETablaSunatDet>();
            //    List = (new BTablasSunat()).TablasSunatDetListar(Obe.suntc_icod);
            //    BTablasSunat Ob = new BTablasSunat();
            //    if (List.Count > 0)
            //    {
            //        if (XtraMessageBox.Show("La tabla será eliminada conjuntamente con su detalle\n\t\t¿Está seguro que desea continuar?",
            //            "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.OK)
            //        {
            //            Ob.TablasSunatEliminar(Obe);
            //        }
            //    }
            //    else
            //    {
            //        if (XtraMessageBox.Show("¿Está seguro que desea eliminar la tabla?",
            //            "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.OK)
            //        {
            //            Ob.TablasSunatEliminar(Obe);
            //        }
            //    }

            //}
        }
        private void registrarDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ETablaSunatCab Obe = (ETablaSunatCab)viewTablaSunat.GetRow(viewTablaSunat.FocusedRowHandle);
            //if (Obe != null)
            //{
            //    FrmTablasSunatDetalle frm = new FrmTablasSunatDetalle();
            //    frm.obe = Obe;
            //    frm.Text = "SGI - DETALLE DE " + Obe.suntc_vdescripcion;
            //    frm.Show();
            //}
        }
        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void viewTablaSunat_DoubleClick(object sender, EventArgs e)
        {
            //ETablaSunatCab Obe = (ETablaSunatCab)viewTablaSunat.GetRow(viewTablaSunat.FocusedRowHandle);
            //if (Obe != null)
            //{
            //    FrmManteTablaSuntaCab frm = new FrmManteTablaSuntaCab();
            //    frm.txtCodigo.Text = Obe.suntc_codigo;
            //    frm.txtCodigo.Tag = Obe.suntc_icod;
            //    frm.txtDescripcion.Text = Obe.suntc_vdescripcion;
            //    //frm.mlist = mlist;
            //    frm.SetCancel();
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    { }                   
            //}
        }
    }
}