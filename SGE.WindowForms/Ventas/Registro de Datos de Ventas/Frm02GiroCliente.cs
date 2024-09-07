using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Tesoreria.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class Frm02GiroCliente : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        private List<EGiro> mlist = new List<EGiro>();

        public Frm02GiroCliente()
        {
            InitializeComponent();
        }

        private void FrmGiroCliente_Load(object sender, EventArgs e)
        {
            Carga();
        }


        private void Carga()
        {
            mlist = new BVentas().ListarGiro();
            dgrGiro.DataSource = mlist;
        }

        private void BuscarCriterio()
        {
            dgrGiro.DataSource = mlist.Where(obj =>
                                                   Convert.ToString(obj.giroc_iid_giro).ToUpper().Contains(txtCodigo.Text.ToUpper()) &&
                                                   obj.giroc_vnombre_giro.ToUpper().Contains(txtGiro.Text.ToUpper())
                                             ).ToList();
        }

        private void textEdit1_KeyUp(object sender, KeyEventArgs e)
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

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteGiroCliente Giro = new FrmManteGiroCliente();
            Giro.MiEvento += form2_MiEvento;
            Giro.oDetail = mlist;
            Giro.Show();
            Giro.SetInsert();

        }


        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                Datos();
            }
        }

        private void Datos()
        {
            FrmManteGiroCliente Giro = new FrmManteGiroCliente();
            Giro.MiEvento += Modify;
            Giro.oDetail = mlist;
            Giro.Show();

            EGiro Obe = (EGiro)gridView1.GetRow(gridView1.FocusedRowHandle);
            Giro.Correlative = Obe.giroc_icod_giro;
            Giro.txtIdGiro.Text = Convert.ToString(Obe.giroc_iid_giro);
            Giro.txtGiro.Text = Obe.giroc_vnombre_giro;
            Giro.LkpSituacion.EditValue = Obe.tablc_iid_situacion_giro;
            Giro.chkIndicador.Checked = Obe.giroc_bindicador_expo_nextel;

            Giro.SetModify();
            xposition = gridView1.FocusedRowHandle;
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                int giroc_icod_giro = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "giroc_icod_giro");
                BVentas obl = new BVentas();
                if (XtraMessageBox.Show("Esta seguro de Eliminar", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    List<ECliente> mlistClI = new List<ECliente>();
                    mlistClI = new BVentas().ListarCliente();
                    if (mlistClI.Count == 0)
                    {
                        obl.EliminarGiro(giroc_icod_giro);
                        gridView1.DeleteRow(gridView1.FocusedRowHandle);
                    }
                    else
                    {
                        if (mlistClI.Count(ob => ob.giroc_icod_giro == giroc_icod_giro) > 0)
                        {
                            XtraMessageBox.Show("No puede Eliminar, Existen clientes registrados con este Giro", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            obl.EliminarGiro(giroc_icod_giro);
                            gridView1.DeleteRow(gridView1.FocusedRowHandle);
                        }
                    }

                }
            }
        }


        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                FrmManteGiroCliente Giro = new FrmManteGiroCliente();
                Giro.MiEvento += form2_MiEvento;
                EGiro Obe = (EGiro)gridView1.GetRow(gridView1.FocusedRowHandle);
                Giro.Show();
                Giro.SetCancel();
                Giro.BtnGuardar.Enabled = false;
                Giro.Correlative = Obe.giroc_icod_giro;
                Giro.txtIdGiro.Text = Convert.ToString(Obe.giroc_iid_giro);
                Giro.txtGiro.Text = Obe.giroc_vnombre_giro;
                Giro.LkpSituacion.EditValue = Obe.tablc_iid_situacion_giro;
                Giro.chkIndicador.Checked = Obe.giroc_bindicador_expo_nextel;
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
            List<EGiro> listado = (List<EGiro>)dgrGiro.DataSource;
            if (listado.Count > 0)
            {
                rptGiroCliente rpt = new rptGiroCliente();
                foreach (var oj in listado)
                {
                    if (oj.giroc_bindicador_expo_nextel = true)
                    {
                        oj.indicador_expo_nextel = "-SI-";
                    }
                    else
                    {
                        oj.indicador_expo_nextel = "-NO-";
                    }

                }
                rpt.cargar(listado, Parametros.intEjercicio.ToString());
            }
            else
                XtraMessageBox.Show("No hay registro por Reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void dgrGiro_Click(object sender, EventArgs e)
        {

        }










    }
}