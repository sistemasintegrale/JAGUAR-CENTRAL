using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Tesoreria.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class Frm01PaisDepProvDis : DevExpress.XtraEditors.XtraForm
    {
        private int ubicc_icod_ubicacion;
        private int xposition = 0;
        private List<EUbicacion> mlist = new List<EUbicacion>();

        public Frm01PaisDepProvDis()
        {
            InitializeComponent();
        }

        private void FrmPaisDepProvDis_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpTipo, new BGeneral().listarTablaRegistro(30), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            Carga();
        }

        private void Carga()
        {
            mlist = new BVentas().ListarUbicacion();
            dgrLugar.DataSource = mlist;
        }

        private void BuscarCriterio()
        {
            if (Convert.ToInt32(lkpTipo.EditValue) == 0)
                dgrLugar.DataSource = mlist.Where(obj => obj.ubicc_ccod_ubicacion.ToUpper().Contains(txtCodigo.Text.ToUpper()) &&
                                                   obj.ubicc_vnombre_ubicacion.ToUpper().Contains(txtNombre.Text.ToUpper())
                                             ).ToList();
            else
                dgrLugar.DataSource = mlist.Where(obj => obj.ubicc_ccod_ubicacion.ToUpper().Contains(txtCodigo.Text.ToUpper()) &&
                                                   obj.ubicc_vnombre_ubicacion.ToUpper().Contains(txtNombre.Text.ToUpper()) &&
                                                    obj.tablc_iid_tipo_ubicacion == Convert.ToInt32(lkpTipo.EditValue)
                                             ).ToList();
        }

        private void textEdit1_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        void form2_MiEvento()
        {
            Carga();
            BuscarCriterio();
        }

        void Modify()
        {
            Carga();
            gridView1.FocusedRowHandle = xposition;
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMantePaisDepProvDis Ubicacion = new FrmMantePaisDepProvDis();
            Ubicacion.MiEvento += new FrmMantePaisDepProvDis.DelegadoMensaje(form2_MiEvento);
            Ubicacion.oDetail = mlist;
            Ubicacion.habilitar = "Insertar";
            Ubicacion.Show();
            Ubicacion.SetInsert();

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
            FrmMantePaisDepProvDis Ubicacion = new FrmMantePaisDepProvDis();
            Ubicacion.MiEvento += new FrmMantePaisDepProvDis.DelegadoMensaje(Modify);
            Ubicacion.oDetail = mlist;
            Ubicacion.habilitar = "Insertar";
            Ubicacion.Show();

            EUbicacion Obe = (EUbicacion)gridView1.GetRow(gridView1.FocusedRowHandle);
            Ubicacion.Correlative = Obe.ubicc_icod_ubicacion;
            Ubicacion.txtIdUbicacion.Text = Obe.ubicc_ccod_ubicacion;
            Ubicacion.txtNombre.Text = Obe.ubicc_vnombre_ubicacion;
            Ubicacion.LkpSituacion.EditValue = Obe.ubicc_iid_situacion_ubicacion;
            Ubicacion.LkpTipoUbicacion.EditValue = Obe.tablc_iid_tipo_ubicacion;

            int? tipo = Obe.tablc_iid_tipo_ubicacion;
            switch (tipo)
            {
                case 3:
                    Ubicacion.LkpPais.EditValue = Obe.ubicc_icod_ubicacion_padre;
                    break;
                case 2:
                    int? codePais = 0;
                    mlist.Where(oB => oB.ubicc_icod_ubicacion == Obe.ubicc_icod_ubicacion_padre).ToList().ForEach(oB =>
                    {
                        codePais = oB.ubicc_icod_ubicacion_padre;
                    });
                    Ubicacion.LkpPais.EditValue = codePais;
                    Ubicacion.LkpDepartamento.EditValue = Obe.ubicc_icod_ubicacion_padre;
                    break;
                case 1:
                    int? codeDepartamento = 0;
                    int? CodePais = 0;
                    mlist.Where(oB => oB.ubicc_icod_ubicacion == Obe.ubicc_icod_ubicacion_padre).ToList().ForEach(oB =>
                    {
                        codeDepartamento = oB.ubicc_icod_ubicacion_padre;
                    });
                    mlist.Where(obj => obj.ubicc_icod_ubicacion == codeDepartamento).ToList().ForEach(obj =>
                    {
                        CodePais = obj.ubicc_icod_ubicacion_padre;
                    });
                    Ubicacion.LkpPais.EditValue = CodePais;
                    Ubicacion.LkpDepartamento.EditValue = codeDepartamento;
                    Ubicacion.LkpProvincia.EditValue = Obe.ubicc_icod_ubicacion_padre;
                    break;

            }

            Ubicacion.SetModify();
            xposition = gridView1.FocusedRowHandle;
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                int ubicc_icod_ubicacion = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ubicc_icod_ubicacion");

                var TieneHijos = mlist.Where(oB => oB.ubicc_icod_ubicacion_padre == ubicc_icod_ubicacion).ToList();
                if (TieneHijos.Count > 0)
                {
                    XtraMessageBox.Show("No se puede eliminar porque existen registros dependientes", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    BVentas obl = new BVentas();
                    if (XtraMessageBox.Show("Esta seguro de Eliminar", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        obl.EliminarUbicacion(ubicc_icod_ubicacion);
                        gridView1.DeleteRow(gridView1.FocusedRowHandle);
                    }
                }
            }
        }





        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                FrmMantePaisDepProvDis Ubicacion = new FrmMantePaisDepProvDis();
                Ubicacion.MiEvento += new FrmMantePaisDepProvDis.DelegadoMensaje(form2_MiEvento);
                EUbicacion Obe = (EUbicacion)gridView1.GetRow(gridView1.FocusedRowHandle);
                Ubicacion.habilitar = "ver";
                Ubicacion.Show();
                Ubicacion.SetCancel();
                Ubicacion.BtnGuardar.Enabled = false;
                Ubicacion.oDetail = mlist;
                Ubicacion.Correlative = Obe.ubicc_icod_ubicacion;
                Ubicacion.txtIdUbicacion.Text = Obe.ubicc_ccod_ubicacion;
                Ubicacion.txtNombre.Text = Obe.ubicc_vnombre_ubicacion;
                Ubicacion.LkpSituacion.EditValue = Obe.ubicc_iid_situacion_ubicacion;
                Ubicacion.LkpTipoUbicacion.EditValue = Obe.tablc_iid_tipo_ubicacion;

                int? tipo = Obe.tablc_iid_tipo_ubicacion;
                switch (tipo)
                {
                    case 3:
                        Ubicacion.LkpPais.EditValue = Obe.ubicc_icod_ubicacion_padre;
                        break;
                    case 2:
                        int? codePais = 0;
                        mlist.Where(oB => oB.ubicc_icod_ubicacion == Obe.ubicc_icod_ubicacion_padre).ToList().ForEach(oB =>
                        {
                            codePais = oB.ubicc_icod_ubicacion_padre;
                        });
                        Ubicacion.LkpPais.EditValue = codePais;
                        Ubicacion.LkpDepartamento.EditValue = Obe.ubicc_icod_ubicacion_padre;
                        break;
                    case 1:
                        int? codeDepartamento = 0;
                        int? CodePais = 0;
                        mlist.Where(oB => oB.ubicc_icod_ubicacion == Obe.ubicc_icod_ubicacion_padre).ToList().ForEach(oB =>
                        {
                            codeDepartamento = oB.ubicc_icod_ubicacion_padre;
                        });
                        mlist.Where(obj => obj.ubicc_icod_ubicacion == codeDepartamento).ToList().ForEach(obj =>
                        {
                            CodePais = obj.ubicc_icod_ubicacion_padre;
                        });
                        Ubicacion.LkpPais.EditValue = CodePais;
                        Ubicacion.LkpDepartamento.EditValue = codeDepartamento;
                        Ubicacion.LkpProvincia.EditValue = Obe.ubicc_icod_ubicacion_padre;
                        break;
                }
            }
        }

        private void lkpTipo_EditValueChanged(object sender, EventArgs e)
        {
            BuscarCriterio();
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

        }

        private void imprimirToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            List<EUbicacion> listado = (List<EUbicacion>)dgrLugar.DataSource;

            if (listado.Count > 0)
            {
                rptDistrito rpt = new rptDistrito();
                if (Convert.ToInt32(lkpTipo.EditValue) == 0)
                {

                    rpt.cargar(listado, Parametros.intEjercicio.ToString(), lkpTipo.Text);
                }
                else
                {
                    rpt.cargar(listado.Where(ob => ob.tablc_iid_tipo_ubicacion == Convert.ToInt32(lkpTipo.EditValue)).ToList(), Parametros.intEjercicio.ToString(), lkpTipo.Text);
                }
            }
            else
                XtraMessageBox.Show("No hay registro por Reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


    }
}