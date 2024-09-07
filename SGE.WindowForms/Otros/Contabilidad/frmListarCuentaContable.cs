using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmListarCuentaContable : DevExpress.XtraEditors.XtraForm
    {
        private List<ECuentaContable> lstCuentasContables = new List<ECuentaContable>();
        private List<EParametroContable> lstParametroContable = new List<EParametroContable>();
        public ECuentaContable _Be { get; set; }
        public bool flagSeleccionImpresion = false;
        public int IndicadorGestion = 0;

        public frmListarCuentaContable()
        {
            InitializeComponent();
        }
        private void FrmCuentaContable_Load(object sender, EventArgs e)
        {
            cargar();
            loadMask();
        }

        private void cargar()
        {
            //if (!flagSeleccionImpresion)
            //    //if (IndicadorGestion == 1)
            //    //    {
            //            lstCuentasContables = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
            //        //}else
            //        // lstCuentasContables = new BContabilidad().listarCuentaContable();


            //else
            lstCuentasContables = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
            /*-----------------------------------------------------------------------------------------------*/
            grdCuentaContable.DataSource = lstCuentasContables;
            /*-----------------------------------------------------------------------------------------------*/
            lstParametroContable = new BContabilidad().listarParametroContable();
        }

        private void loadMask()
        {
            lstParametroContable = new BContabilidad().listarParametroContable();
            if (lstParametroContable.Count > 0)
            {
                lstParametroContable.ForEach(obe =>
                {
                    this.txtCodigo.Properties.Mask.EditMask = obe.parac_vmascara;
                    this.txtCodigo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                    this.txtCodigo.Properties.Mask.ShowPlaceHolders = false;
                });
            }
        }

        private void returnSeleccion()
        {
            if (lstCuentasContables.Count > 0)
            {
                _Be = (ECuentaContable)viewCuentaContable.GetRow(viewCuentaContable.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }

        private void BuscarCriterio()
        {
            grdCuentaContable.DataSource = lstCuentasContables.Where(obj =>
                                                   obj.ctacc_nombre_descripcion.ToUpper().Contains(textEdit1.Text.ToUpper()) &&
                                                   obj.ctacc_numero_cuenta_contable.Replace(".", "").ToUpper().StartsWith(txtCodigo.Text.Replace(".", "").ToUpper())).ToList();

        }

        private void textEdit1_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            returnSeleccion();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}