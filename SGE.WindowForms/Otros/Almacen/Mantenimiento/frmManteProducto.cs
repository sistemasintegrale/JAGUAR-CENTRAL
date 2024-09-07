using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;


namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmManteProducto : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteProducto));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EProducto Obe = new EProducto();
        public EFamiliaCab obeF = new EFamiliaCab();
        public List<EProducto> lstProductos = new List<EProducto>();
        List<EFamiliaCab> lstFamilia = new List<EFamiliaCab>();
        List<EFamiliaDet> lstSubFamilia = new List<EFamiliaDet>();


        public frmManteProducto()
        {
            InitializeComponent();
        }
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }
        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            bteFamilia.Enabled = !Enabled;
            txtDesLarga.Enabled = !Enabled;
            lkpUnidadMedida.Enabled = !Enabled;
            txtStockMin.Enabled = !Enabled;
            txtCaracteristica.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            rbActivo.Enabled = !Enabled;
            rbInactivo.Enabled = !Enabled;
            txtMarca.Enabled = !Enabled;
            txtModelo.Enabled = !Enabled;
            txtParte.Enabled = !Enabled;
            txtSerie.Enabled = !Enabled;

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                bteFamilia.Enabled = Enabled;
                bteSubFamilia.Enabled = Enabled;

            }

        }
        public void setValues()
        {
            bteFamilia.Tag = Obe.famic_icod_familia;
            bteSubFamilia.Tag = Obe.famid_icod_familia;
            bteFamilia.Text = Obe.strFamilia;
            bteSubFamilia.Text = Obe.strSubFamilia;
            /*--------------------------------------------------------------------*/
            if (Obe.prdc_vcode_producto == "")
            {
                bteFamilia.Enabled = true;
            }
            else
            {
                txtCorrelativo.Text = Obe.prdc_vcode_producto.Substring((Obe.prdc_vcode_producto.Length - 4));
            }
            //txtCorrelativo.Text = Obe.prdc_vcode_producto.ToString();
            rbActivo.Checked = Obe.prdc_isituacion;
            rbInactivo.Checked = !Obe.prdc_isituacion;
            txtDesLarga.Text = Obe.prdc_vdescripcion_larga;
            txtMarca.Text = Obe.prdc_vmarca;
            txtModelo.Text = Obe.prdc_vmodelo;
            txtParte.Text = Obe.prdc_vparte;
            txtSerie.Text = Obe.prdc_vserie;
            lkpUnidadMedida.EditValue = Obe.unidc_icod_unidad_medida;
            txtCaracteristica.Text = Obe.prdc_vcaracteristica;

        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                if (Convert.ToInt32(bteFamilia.Tag) == 0)
                {
                    oBase = bteFamilia;
                    throw new ArgumentException("Código de Familia inválido");
                }

                if (Convert.ToInt32(bteSubFamilia.Tag) == 0)
                {
                    oBase = bteSubFamilia;
                    throw new ArgumentException("Código de Sub-Familia inválido");
                }

                int? nullVall = null;
                Obe.famic_icod_familia = Convert.ToInt32(bteFamilia.Tag);
                Obe.famid_icod_familia = Convert.ToInt32(bteSubFamilia.Tag);

                Obe.prdc_isituacion = rbActivo.Checked;
                Obe.prdc_vcode_producto = String.Format("{0}-{1}-{2}", bteFamilia.Text.Trim(), bteSubFamilia.Text.Trim(), txtCorrelativo.Text.Trim());
                //Obe.prdc_vcode_producto = String.Format("{0}", txtCorrelativo.Text.Trim());
                Obe.prdc_vdescripcion_larga = txtDesLarga.Text;

                Obe.prdc_vmarca = txtMarca.Text;
                Obe.prdc_vmodelo = txtModelo.Text;
                Obe.prdc_vparte = txtParte.Text;
                Obe.prdc_vserie = txtSerie.Text;
                Obe.prdc_vcaracteristica = txtCaracteristica.Text;
                Obe.prdc_flag_estado = true;


                if (Convert.ToInt32(lkpUnidadMedida.EditValue) == 0)
                {
                    Obe.unidc_icod_unidad_medida = null;
                }
                else
                    Obe.unidc_icod_unidad_medida = Convert.ToInt32(lkpUnidadMedida.EditValue);


                Obe.prdc_stock_minimo = Convert.ToDecimal(txtStockMin.Text);
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    //Obe.prdc_iid_correlativo = lstProductos.Count + 1;
                    Obe.prdc_icod_producto = new BAlmacen().insertarProducto(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BAlmacen().modificarProducto(Obe);
                }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    if (Obe.prdc_icod_producto > 0)
                        this.MiEvento(Obe.prdc_icod_producto);
                    this.Close();
                }
            }
        }



        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
        /*                 
         * /*/

        private void frmManteProducto_Load(object sender, EventArgs e)
        {

            /*--------------------------------------------------------*/
            lstFamilia = new BAlmacen().listarFamiliaCab();
            if (Status != BSMaintenanceStatus.CreateNew)
                lstSubFamilia = new BAlmacen().listarFamiliaDet(Convert.ToInt32(Obe.famic_icod_familia));

            /*--------------------------------------------------------*/
            if (lstFamilia.Count == 0)
            {
                XtraMessageBox.Show("Debe registrar familias y subfamilias para continuar con el registro de productos", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
            /*--------------------------------------------------------*/
            /*-----------------------------------------------------------------------------------------------------------------------------------------------*/



            BSControls.LoaderLook(lkpUnidadMedida, new BAlmacen().listarUnidadMedida(), "unidc_vdescripcion", "unidc_icod_unidad_medida", true);
            rbActivo.Checked = true;


        }
        private void generarCodigo()
        {
            string strCodigo = "";

            if (Convert.ToInt32(bteFamilia.Tag) != 0 && Convert.ToInt32(bteSubFamilia.Tag) != 0)
                strCodigo = String.Format("{0:00000}", new BAlmacen().getCorrelativoProducto(Convert.ToInt32(bteFamilia.Tag)));

            txtCorrelativo.Text = strCodigo;
        }
        private void generarDesLarga()
        {
            string strDesLarga = "";
            strDesLarga = String.Format("{0} {1}",
                (Convert.ToInt32(bteFamilia.Tag) == 0) ? "" : lstFamilia.Where(x => x.famic_icod_familia == Convert.ToInt32(bteFamilia.Tag)).ToList()[0].famic_vdescripcion,
                (Convert.ToInt32(bteSubFamilia.Tag) == 0) ? "" : lstSubFamilia.Where(x => x.famid_icod_familia_tipo == Convert.ToInt32(bteSubFamilia.Tag)).ToList()[0].famid_vdescripcion);
        }
        private void bteFamilia_EditValueChanged(object sender, EventArgs e)
        {
            if (Status != BSMaintenanceStatus.CreateNew)
                return;
            var lstAux = lstFamilia.Where(x => x.famic_vabreviatura.Trim() == bteFamilia.Text.Trim()).ToList();
            if (lstAux.Count > 0)
            {
                bteFamilia.Tag = lstAux[0].famic_icod_familia;
                lstSubFamilia = new BAlmacen().listarFamiliaDet(Convert.ToInt32(bteFamilia.Tag));
                bteSubFamilia.Enabled = true;
                generarCodigo();
                generarDesLarga();
                lblMsj.Visible = false;


            }
            else
            {
                lblMsj.Visible = true;
                bteSubFamilia.Enabled = false;
                bteFamilia.Tag = null;
                lstSubFamilia.Clear();
                generarCodigo();
                generarDesLarga();


            }

        }

        private void bteSubFamilia_EditValueChanged(object sender, EventArgs e)
        {
            if (Status != BSMaintenanceStatus.CreateNew)
                return;
            if (Convert.ToInt32(bteFamilia.Tag) == 0)
                return;

            var lstAux = lstSubFamilia.Where(x => x.famid_vabreviatura.Trim() == bteSubFamilia.Text.Trim()).ToList();
            if (lstAux.Count > 0)
            {
                bteSubFamilia.Tag = lstAux[0].famid_icod_familia_tipo;
                generarCodigo();
                lblMsj.Visible = false;
                bteFamilia.Enabled = false;
            }
            else
            {
                lblMsj.Visible = true;
                bteSubFamilia.Tag = null;
                generarCodigo();
                generarDesLarga();
                bteFamilia.Enabled = false;
            }
            if (bteSubFamilia.Text == "")
            {
                bteFamilia.Enabled = true;
                lblMsj.Visible = false;
            }
        }

        private void bteFamilia_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarFamilia frm = new frmListarFamilia())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteFamilia.Tag = frm._Be.famic_icod_familia;
                    bteFamilia.Text = frm._Be.famic_vabreviatura;
                    lstSubFamilia = new BAlmacen().listarFamiliaDet(Convert.ToInt32(bteFamilia.Tag));
                    bteSubFamilia.Enabled = true;
                    generarCodigo();
                    generarDesLarga();
                    lblMsj.Visible = false;
                    bteSubFamilia.Focus();


                }

            }
        }

        private void bteSubFamilia_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarSubFamilia frm = new frmListarSubFamilia())
            {
                frm.intFamilia = Convert.ToInt32(bteFamilia.Tag);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteSubFamilia.Tag = frm._Be.famid_icod_familia_tipo;
                    bteSubFamilia.Text = frm._Be.famid_vabreviatura;
                    generarCodigo();
                    generarDesLarga();
                    lblMsj.Visible = false;
                    bteFamilia.Enabled = false;
                }

            }
        }

        private void btnGuardar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void txtCaracteristica_EditValueChanged(object sender, EventArgs e)
        {

        }








    }
}