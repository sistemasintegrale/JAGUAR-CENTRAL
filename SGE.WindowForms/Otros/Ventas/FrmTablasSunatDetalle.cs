using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmTablasSunatDetalle : DevExpress.XtraEditors.XtraForm
    {
        List<ETablaSunatDet> mlist = new List<ETablaSunatDet>();
        public ETablaSunatCab obe;
        public FrmTablasSunatDetalle()
        {
            InitializeComponent();
        }

        private void FrmTablasSunatDetalle_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            mlist = (new BVentas()).TablasSunatDetListar(obe.suntc_icod);
            grdTablaSunatDet.DataSource = mlist;
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmManteTablasSunatDet frm = new FrmManteTablasSunatDet())
            {
                frm.cod_cab = obe.suntc_icod;
                frm.mlist = mlist;
                frm.SetInsert();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    cargar();
                }
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ETablaSunatDet Obe = (ETablaSunatDet)viewTablaSunatDet.GetRow(viewTablaSunatDet.FocusedRowHandle);
            if (Obe != null)
            {
                using (FrmManteTablasSunatDet frm = new FrmManteTablasSunatDet())
                {
                    frm.cod_cab = Obe.suntc_icod;
                    frm.det_cab = Obe.suntd_icod;
                    frm.mlist = mlist;
                    frm.txtCodigo.Text = Obe.suntd_codigo;
                    frm.txtCodigo.Tag = Obe.suntd_icod;
                    frm.txtDescripcion.Text = Obe.suntd_vdescripcion;
                    frm.SetModify();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        cargar();
                    }
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ETablaSunatDet Obe = (ETablaSunatDet)viewTablaSunatDet.GetRow(viewTablaSunatDet.FocusedRowHandle);
            if (Obe != null)
            {
                BVentas obl = new BVentas();
                obl.TablasSunatDetEliminar(Obe);
                cargar();
            }

        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                //rptTablasSunatDet rpt = new rptTablasSunatDet();FALTA REPORTE
                //rpt.cargar(mlist, obe);
            }
        }
        private void BuscarCriterio()
        {
            grdTablaSunatDet.DataSource = mlist.Where(x => x.suntd_codigo.ToUpper().Contains(txtCodigo.Text.ToUpper()) &&
                x.suntd_vdescripcion.ToUpper().Contains(txtDescripcion.Text.ToUpper())).ToList();
        }
        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}