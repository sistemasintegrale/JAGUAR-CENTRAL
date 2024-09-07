using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Administracion_del_Sistema
{
    public partial class frmListaAccesosNoPermitidos : DevExpress.XtraEditors.XtraForm
    {
        public List<EFormulario> lstAccesos = new List<EFormulario>();
        public int intUsuario_;

        public frmListaAccesosNoPermitidos()
        {
            InitializeComponent();
        }
        private void FrmListaAcceso_Load(object sender, EventArgs e)
        {
            cargar();
            gridColumn2.Group();
        }

        public void cargar()
        {
            lstAccesos = new BAdministracionSistema().listarAccesosNoPermitidos(intUsuario_);
            gridForms.DataSource = lstAccesos;
        }
        private void BuscarCriterio()
        {
            gridForms.DataSource = lstAccesos.Where(obj =>
                obj.strModulo.ToUpper().Contains(txtcodigo.Text.ToUpper()) &&
                obj.formc_vdescripcion.Contains(txtdescripcion.Text.ToUpper())).ToList();
        }
        private void SetSave()
        {
            viewForms.MoveLast();
            txtcodigo.Focus();
            try
            {
                for (int i = 0; i < lstAccesos.Count; i++)
                {
                    if (lstAccesos[i].flag == true)
                    {
                        lstAccesos[i].intUsuarioAcceso = intUsuario_;
                        lstAccesos[i].intUsuario = Modules.Valores.intUsuario;
                        new BAdministracionSistema().insertarAccesoUsuario(lstAccesos[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            List<EFormulario> mlistAux = new List<EFormulario>();
            mlistAux = lstAccesos.Where(obj =>
                obj.strModulo.ToUpper().Contains(txtcodigo.Text.ToUpper()) &&
                obj.formc_vdescripcion.Contains(txtdescripcion.Text.ToUpper())).ToList();
            if (checkBox1.Checked == true)
            {
                foreach (EFormulario x in mlistAux)
                {
                    foreach (EFormulario y in lstAccesos)
                    {
                        if (x.formc_icod_forms == y.formc_icod_forms)
                        {
                            y.flag = true;
                        }
                    }
                }
                gridForms.DataSource = lstAccesos.Where(obj =>
                obj.strModulo.ToUpper().Contains(txtcodigo.Text.ToUpper()) &&
                obj.formc_vdescripcion.Contains(txtdescripcion.Text.ToUpper())).ToList();
                viewForms.RefreshData();

            }
            else
            {

                foreach (EFormulario objk in mlistAux)
                {
                    foreach (EFormulario list in lstAccesos)
                    {
                        if (objk.formc_icod_forms == list.formc_icod_forms)
                        {
                            list.flag = false;
                        }
                    }
                }
                gridForms.DataSource = lstAccesos.Where(obj =>
                obj.strModulo.ToUpper().Contains(txtcodigo.Text.ToUpper()) &&
                obj.formc_vdescripcion.Contains(txtdescripcion.Text.ToUpper())).ToList();
                viewForms.RefreshData();
            }
        }

        private void txtcodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }


    }
}