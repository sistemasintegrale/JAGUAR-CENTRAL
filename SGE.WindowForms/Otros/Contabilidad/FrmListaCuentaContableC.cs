using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class FrmListaCuentaContableC : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        private List<ECuentaContable> mlist = new List<ECuentaContable>();
        private List<ECuentaContable> auxList = new List<ECuentaContable>();
        public ECuentaContable _Be { get; set; }
        public bool tipocuenta = false;
        public int saldo_inicial = 0;
        public FrmListaCuentaContableC()
        {
            InitializeComponent();
        }

        private void FrmListaCuentaContable_Load(object sender, EventArgs e)
        {
            Carga();
            //LoadMask();
        }
        private void LoadMask()
        {
            List<EParametroContable> mlista = new List<EParametroContable>();
            mlista = (new BContabilidad()).listarParametroContable();
            if (mlista.Count == 0)
            {
            }
            else
            {
                mlista.ForEach(obe =>
                {
                    this.txtCodigo.Properties.Mask.EditMask = obe.parac_vmascara;
                    this.txtCodigo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                    this.txtCodigo.Properties.Mask.ShowPlaceHolders = false;
                });
            }
        }
        private void Carga()
        {

            mlist = new BContabilidad().listarCuentaContable();

            if (tipocuenta)
            {
                if (saldo_inicial == 1)
                {
                    auxList = mlist.Where(x =>
                    x.tablc_iid_tipo_cuenta == 2).ToList().Where(y => Convert.ToInt32(y.ctacc_numero_cuenta_contable.ToString().Substring(0, 2)) <= 59).ToList();
                    dgrCuentaContable.DataSource = auxList;
                }
                else
                {
                    auxList = mlist.Where(x =>
                    x.tablc_iid_tipo_cuenta == 2).ToList();
                    dgrCuentaContable.DataSource = auxList;
                }
            }
            else
            {
                auxList = mlist;
                dgrCuentaContable.DataSource = auxList;

            }

            gridView1.RefreshData();
        }

        private void BuscarCriterio()
        {
            dgrCuentaContable.DataSource = auxList.Where(obj =>
                                                   obj.ctacc_nombre_descripcion.ToUpper().Contains(textEdit1.Text.ToUpper()) &&
                                                   obj.ctacc_numero_cuenta_contable.ToUpper().Replace(".", "").StartsWith(txtCodigo.Text.Replace(".", "").ToUpper()) &&
                                                   obj.tablc_iid_tipo_cuenta == 2
                                             ).ToList();

        }
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            returnOk();
        }

        private void textEdit1_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                returnOk();
            }
        }
        private void returnOk()
        {
            _Be = (ECuentaContable)gridView1.GetRow(gridView1.FocusedRowHandle);
            this.DialogResult = DialogResult.OK;
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}