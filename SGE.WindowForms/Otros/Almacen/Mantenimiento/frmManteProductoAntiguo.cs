using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Otros.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;


namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmManteProductoAntiguo : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteProductoAntiguo));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EProducto Obe = new EProducto();
        public EFamiliaCab obeF = new EFamiliaCab();
        public List<EProducto> lstProductos = new List<EProducto>();
        List<EFamiliaCab> lstFamilia = new List<EFamiliaCab>();
        List<EFamiliaDet> lstSubFamilia = new List<EFamiliaDet>();
        private BAlmacen Obl;
        public string codigoLinea;
        public List<EProducto> oProducto;
        public int IntSerie;
        public frmManteProductoAntiguo()
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
            bteTipo.Enabled = !Enabled;
            txtDesLarga.Enabled = !Enabled;
            lkpUnidadMedida.Enabled = !Enabled;
            lkpColor.Enabled = !Enabled;
            txtStockMin.Enabled = !Enabled;
            rbActivo.Enabled = !Enabled;
            rbInactivo.Enabled = !Enabled;
            txtcodFabricante.Enabled = !Enabled;
            txtSerie1.Enabled = Enabled;
            txtSerie2.Enabled = Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                if (IntSerie == 1)
                {
                    bteFamilia.Enabled = false;
                    bteTipo.Enabled = false;
                    //txtcalibre.Enabled = false;
                    //txtcosto.Enabled = false;
                    txtDesLarga.Enabled = false;
                    lkpUnidadMedida.Enabled = false;
                    lkpColor.Enabled = false;
                    txtStockMin.Enabled = false;
                    rbActivo.Enabled = false;
                    rbInactivo.Enabled = false;
                    txtcodFabricante.Enabled = false;
                    lkpTipo.Enabled = false;
                    txtcalibre.Enabled = false;
                    txtcosto.Enabled = false;
                    chkSerie.Enabled = true;
                    txtSerie1.Enabled = true;
                    txtSerie2.Enabled = true;
                }
                else
                {
                    chkSerie.Enabled = false;
                    bteFamilia.Enabled = Enabled;
                    txtSerie1.Enabled = Enabled;
                    txtSerie2.Enabled = Enabled;
                }

            }
        }
        public void setValues()
        {
            bteFamilia.Tag = Obe.famic_icod_familia;
            lkpTipo.Tag = Obe.famid_icod_familia;
            List<EProdTablaRegistro> lsttablaRegistro = new List<EProdTablaRegistro>();
            //EProdTablaRegistro xx = new EProdTablaRegistro();
            //xx.iid_tipo_tabla = 13;
            //lsttablaRegistro = new BCentral().ListarProdTablaRegistro(xx).ToList();
            bteFamilia.Text = Obe.strDesFamilia;
            codigoLinea = Obe.strFamilia;
            lkpTipo.Text = Obe.strDesSubFamilia;
            /*--------------------------------------------------------------------*/
            if (Obe.prdc_vcode_producto == "")
            {
                bteFamilia.Enabled = true;
            }
            else
            {
                txtCorrelativo.Text = Obe.prdc_vcode_producto.Substring(4);
            }
            //txtCorrelativo.Text = Obe.prdc_vcode_producto.ToString();
            rbActivo.Checked = Obe.prdc_isituacion;
            rbInactivo.Checked = !Obe.prdc_isituacion;
            txtDesLarga.Text = Obe.prdc_vdescripcion_larga;
            lkpUnidadMedida.EditValue = Obe.unidc_icod_unidad_medida;
            txtcalibre.Text = Obe.prdc_calibre.ToString();
            lkpColor.EditValue = Obe.tarec_icorrelativo_colorprod;
            btnProveedor.Tag = Obe.prdc_icod_proveedor;
            txtcosto.Text = Obe.prdc_precio_costo.ToString();
            txtcodFabricante.Text = Obe.prdc_vcodigo_fabricante;


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
            Obl = new BAlmacen();
            try
            {
                if (Convert.ToInt32(bteFamilia.Tag) == 0)
                {
                    oBase = bteFamilia;
                    throw new ArgumentException("Código de Familia inválido");
                }
                if (txtDesLarga.Text == "")
                {
                    oBase = txtDesLarga;
                    throw new ArgumentException("Ingrese Descripcion");
                }

                int? nullVall = null;
                Obe.famic_icod_familia = Convert.ToInt32(bteFamilia.Tag);
                Obe.prdc_isituacion = rbActivo.Checked;
                Obe.prdc_vcode_producto = String.Format("{0}-{1}", codigoLinea.Trim(), txtCorrelativo.Text.Trim());
                Obe.prdc_vdescripcion_larga = txtDesLarga.Text;
                Obe.prdc_flag_estado = true;

                if (Convert.ToInt32(lkpUnidadMedida.EditValue) == 0)
                {
                    Obe.unidc_icod_unidad_medida = null;
                }
                else
                    Obe.unidc_icod_unidad_medida = Convert.ToInt32(lkpUnidadMedida.EditValue);
                Obe.prdc_stock_minimo = Convert.ToDecimal(txtStockMin.Text);
                Obe.prdc_calibre = Convert.ToDecimal(txtcalibre.Text);
                Obe.tarec_icorrelativo_colorprod = Convert.ToInt32(lkpColor.EditValue);
                Obe.prdc_icod_proveedor = Convert.ToInt32(btnProveedor.Tag);
                Obe.prdc_precio_costo = Convert.ToDecimal(txtcosto.Text);
                Obe.prdc_vcodigo_fabricante = txtcodFabricante.Text;
                Obe.famid_icod_familia = Convert.ToInt32(lkpTipo.EditValue);
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (chkSerie.CheckState == CheckState.Checked)
                    {
                        if (XtraMessageBox.Show("Desea Generar Codigos de talla del " + txtSerie1.Text + " al " + txtSerie2.Text, "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            for (int y = Convert.ToInt32(txtSerie1.Text); y <= Convert.ToInt32(txtSerie2.Text); y++)
                            {
                                x[10] = string.Format("{0:00}", y);
                                Obe.prdc_vdescripcion_larga = txtDesLarga.Text + " " + x[10];
                                oProducto = Obl.VerificarProducto(Obe.prdc_vdescripcion_larga);
                                if (oProducto.Count == 0)
                                {
                                    List<EProdTablaRegistro> mlistaTalla = new List<EProdTablaRegistro>();
                                    EProdTablaRegistro xx = new EProdTablaRegistro();
                                    EProdProducto talla = new EProdProducto();
                                    xx.iid_tipo_tabla = 10;
                                    mlistaTalla = new BCentral().ListarProdTablaRegistro(xx);
                                    var Lista = mlistaTalla.Where(ob => ob.descripcion == (Convert.ToInt32(x[10])).ToString()).ToList();
                                    //Lista.ForEach(ob => { talla.pr_iid_talla = ob.tarec_iid_correlativo; });
                                    if (Lista.Count == 1)
                                    {
                                        Obe.prdc_italla = y;
                                        Obe.prdc_icod_producto = new BAlmacen().insertarProducto(Obe);
                                    }
                                }
                            }
                        }
                    }
                    else
                        Obe.prdc_icod_producto = new BAlmacen().insertarProducto(Obe);

                }
                else
                {
                    if (chkSerie.CheckState == CheckState.Checked)
                    {
                        if (XtraMessageBox.Show("Desea Generar Codigos de talla del " + txtSerie1.Text + " al " + txtSerie2.Text, "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            for (int y = Convert.ToInt32(txtSerie1.Text); y <= Convert.ToInt32(txtSerie2.Text); y++)
                            {
                                x[10] = string.Format("{0:00}", y);
                                Obe.prdc_vdescripcion_larga = Obe.prdc_vdescripcion_larga.Substring(0, txtDesLarga.Text.Length - 2) + x[10];
                                //Obe.prdc_vdescripcion_larga = txtDesLarga.Text + " " + x[10];
                                oProducto = Obl.VerificarProducto(Obe.prdc_vdescripcion_larga);
                                if (oProducto.Count == 0)
                                {
                                    List<EProdTablaRegistro> mlistaTalla = new List<EProdTablaRegistro>();
                                    EProdTablaRegistro xx = new EProdTablaRegistro();
                                    EProdProducto talla = new EProdProducto();
                                    xx.iid_tipo_tabla = 10;
                                    mlistaTalla = new BCentral().ListarProdTablaRegistro(xx);
                                    var Lista = mlistaTalla.Where(ob => ob.descripcion == x[10]).ToList();
                                    //Lista.ForEach(ob => { talla.pr_iid_talla = ob.tarec_iid_correlativo; });
                                    if (Lista.Count == 1)
                                    {
                                        Obe.prdc_italla = y;
                                        Obe.prdc_icod_producto = new BAlmacen().insertarProducto(Obe);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Obe.prdc_icod_producto = Obe.prdc_icod_producto;
                        new BAlmacen().modificarProducto(Obe);
                    }
                }
            }
            catch (Exception ex)
            {
                oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                oBase.ErrorText = ex.Message;
                oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {
                if (Flag)
                {

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
            EProdTablaRegistro xx = new EProdTablaRegistro();
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
            xx.iid_tipo_tabla = 13;
            //BSControls.LoaderLook(lkpColor, new BCentral().ListarProdTablaRegistro(xx), "descripcion", "tarec_iid_correlativo", true);

            int index = new BCentral().ListarProdTablaRegistro(xx).FindIndex(x => x.tarec_iid_correlativo == 180);
            BSControls.LoaderLook(lkpColor, new BCentral().ListarProdTablaRegistro(xx).ToList(), "descripcion", "tarec_iid_correlativo", true);
            lkpColor.ItemIndex = index;

            BSControls.LoaderLook(lkpUnidadMedida, new BAlmacen().listarUnidadMedida(), "unidc_vdescripcion", "unidc_icod_unidad_medida", true);
            rbActivo.Checked = true;


            BSControls.LoaderLook(lkpTipo, new BAlmacen().listarFamiliaDet(Convert.ToInt32(bteFamilia.Tag)), "famid_vdescripcion", "famid_icod_familia_tipo", true);

            Tipo();

        }
        private void Tipo()
        {
            BSControls.LoaderLook(lkpTipo, new BAlmacen().listarFamiliaDet(Convert.ToInt32(bteFamilia.Tag)), "famid_vdescripcion", "famid_icod_familia_tipo", true);
        }
        private void generarCodigo()
        {
            string strCodigo = "";

            if (Convert.ToInt32(bteFamilia.Tag) != 0)
                strCodigo = String.Format("{0:000000}", new BAlmacen().getCorrelativoProducto(Convert.ToInt32(bteFamilia.Tag)));

            txtCorrelativo.Text = strCodigo;
        }
        private void generarDesLarga()
        {
            string strDesLarga = "";
            strDesLarga = String.Format("{0}",
                (Convert.ToInt32(bteFamilia.Tag) == 0) ? "" : lstFamilia.Where(x => x.famic_icod_familia == Convert.ToInt32(bteFamilia.Tag)).ToList()[0].famic_vdescripcion);
        }


        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void bteFamilia_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarFamilia frm = new frmListarFamilia())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteFamilia.Tag = frm._Be.famic_icod_familia;
                    bteFamilia.Text = frm._Be.famic_vdescripcion;
                    codigoLinea = frm._Be.famic_vabreviatura;
                    lstSubFamilia = new BAlmacen().listarFamiliaDet(Convert.ToInt32(bteFamilia.Tag));
                    bteTipo.Enabled = true;
                    generarCodigo();
                    generarDesLarga();
                    lblMsj.Visible = false;
                    //bteTipo.Focus();
                    Tipo();
                    if (frm._Be.famic_flag_serie == true)
                    {
                        //chkSerie.Enabled = true;
                        chkSerie.Checked = true;
                    }
                    else
                    {
                        chkSerie.Checked = false;
                        chkSerie.Enabled = false;
                    }

                }

            }
        }

        private void bteSubFamilia_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarSubFamilia frm = new frmListarSubFamilia())
            {
                frm.intFamilia = Convert.ToInt32(bteFamilia.Tag);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteTipo.Tag = frm._Be.famid_icod_familia_tipo;
                    bteTipo.Text = frm._Be.famid_vdescripcion;
                    //generarCodigo();
                    //generarDesLarga();
                    lblMsj.Visible = false;
                    //bteFamilia.Enabled = false;
                }

            }
        }

        private void bteFamilia_EditValueChanged_1(object sender, EventArgs e)
        {
            if (Status != BSMaintenanceStatus.CreateNew)
                return;
            var lstAux = lstFamilia.Where(x => x.famic_vdescripcion.Trim() == bteFamilia.Text.Trim()).ToList();
            if (lstAux.Count > 0)
            {
                bteFamilia.Tag = lstAux[0].famic_icod_familia;
                lstSubFamilia = new BAlmacen().listarFamiliaDet(Convert.ToInt32(bteFamilia.Tag));
                //bteTipo.Enabled = true;
                generarCodigo();
                generarDesLarga();
                lblMsj.Visible = false;


            }
            else
            {
                lblMsj.Visible = true;
                //bteTipo.Enabled = false;
                bteFamilia.Tag = null;
                lstSubFamilia.Clear();
                generarCodigo();
                generarDesLarga();


            }
        }

        private void bteSubFamilia_EditValueChanged_1(object sender, EventArgs e)
        {
            if (Status != BSMaintenanceStatus.CreateNew)
                return;
            if (Convert.ToInt32(bteFamilia.Tag) == 0)
                return;

            var lstAux = lstSubFamilia.Where(x => x.famid_vdescripcion.Trim() == bteTipo.Text.Trim()).ToList();
            if (lstAux.Count > 0)
            {
                bteTipo.Tag = lstAux[0].famid_icod_familia_tipo;
                //generarCodigo();
                lblMsj.Visible = false;
                //bteFamilia.Enabled = false;
            }
            else
            {
                lblMsj.Visible = true;
                bteTipo.Tag = null;
                generarCodigo();
                generarDesLarga();
                //bteFamilia.Enabled = false;
            }
            if (bteTipo.Text == "")
            {
                bteFamilia.Enabled = true;
                lblMsj.Visible = false;
            }
        }

        private void txtCaracteristica_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProveedor();
        }
        private void ListarProveedor()
        {
            FrmListarProveedor Proveedor = new FrmListarProveedor();
            Proveedor.Carga();
            if (Proveedor.ShowDialog() == DialogResult.OK)
            {
                btnProveedor.Tag = Proveedor._Be.iid_icod_proveedor;//proc_icod_proveedor
                btnProveedor.Text = Proveedor._Be.vnombrecompleto;
            }
            //txtStockMin.Focus();
        }
        string[] x = new string[12];
        private void chkgrupal_CheckedChanged(object sender, EventArgs e)
        {
            this.txtSerie1.Enabled = Convert.ToBoolean(chkSerie.CheckState);
            this.txtSerie2.Enabled = Convert.ToBoolean(chkSerie.CheckState);
        }


    }
}