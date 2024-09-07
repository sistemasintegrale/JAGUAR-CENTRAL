using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Compras;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
//using SGE.WindowForms.Contabilidad.Activos_Fijos;


namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmRegistroActivosFijos : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroActivosFijos));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EActivoFijo Obe = new EActivoFijo();
        public List<EActivoFijo> lstActivoFijo = new List<EActivoFijo>();
        public List<ECuentaContable> mlistCuenta = new List<ECuentaContable>();
        public List<EParametroContable> mlista = (new BContabilidad()).listarParametroContable();

        public List<ECentroCosto> mlistCosto = new List<ECentroCosto>();
        public List<ECentroCosto> mlistaCentroCosto = (new BContabilidad()).listarCentroCosto();

        public frmRegistroActivosFijos()
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
            txtCodigo.Enabled = !Enabled;
            lkpClasificacion.Enabled = !Enabled;
            lkpSituacion.Enabled = !Enabled;
            txtDescripcion.Enabled = !Enabled;
            txtMarca.Enabled = !Enabled;
            txtModelo.Enabled = !Enabled;
            txtSerie.Enabled = !Enabled;
            txtCaracteristicas.Enabled = !Enabled;
            txtCantidad.Enabled = !Enabled;
            lkpMoneda.Enabled = !Enabled;
            txtCostoActual.Enabled = !Enabled;
            txtTotalDepreciacion.Enabled = !Enabled;
            txtCodigoInventario.Enabled = !Enabled;
            bteLocalidad.Enabled = !Enabled;
            dteFechaAdqui.Enabled = !Enabled;
            txtCostoAdqui.Enabled = !Enabled;
            txtAñoVida.Enabled = !Enabled;
            txtPorcentajeDeprec.Enabled = !Enabled;
            dteFechaAlta.Enabled = !Enabled;
            dteInicioUso.Enabled = !Enabled;
            bteCCosto.Enabled = !Enabled;
            bteCContable.Enabled = !Enabled;
            bteProveedor.Enabled = !Enabled;
            dteFechaBaja.Enabled = !Enabled;
            txtMotivo.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;



            if (Status == BSMaintenanceStatus.CreateNew)
            {


            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtCodigo.Enabled = false;


            }
        }
        public void setValues()
        {

            txtCodigo.Text = String.Format("{0:000000}", Obe.acfc_iid_activo_fijo);
            txtDescripcion.Text = Obe.acfc_vdescripcion;
            txtMarca.Text = Obe.acfc_vmarca;
            txtModelo.Text = Obe.acfc_vmodelo;
            txtSerie.Text = Obe.acfc_vserie;
            txtCaracteristicas.Text = Obe.acfc_vcaracterist;
            txtCantidad.Text = Obe.acfc_icantidad.ToString();
            txtCostoActual.Text = Obe.acfc_ncosto_act.ToString();
            txtTotalDepreciacion.Text = Obe.acfc_ntotal_deprec.ToString();
            txtCodigoInventario.Text = Obe.acfc_vcodigo_invent;
            bteLocalidad.Tag = Obe.lafc_icod_localidad;
            dteFechaAdqui.DateTime = Convert.ToDateTime(Obe.acfc_sfech_adqui);
            txtCostoAdqui.Text = Obe.acfc_ncosto_adqui.ToString();
            txtAñoVida.Text = Obe.acfc_ianio_vida.ToString();
            txtPorcentajeDeprec.Text = Obe.acfc_nporct_deprec.ToString();
            dteFechaAlta.DateTime = Convert.ToDateTime(Obe.acfc_sfech_alta);
            dteInicioUso.DateTime = Convert.ToDateTime(Obe.acfc_sfecha_inic_uso);
            bteCCosto.Tag = Obe.ccoc_icod_centro_costo;
            bteCCosto.Text = Obe.ccoc_numero_centro_costo;
            bteCContable.Tag = Obe.ctacc_icod_cuenta_contable;
            bteCContable.Text = Obe.ctacc_numero_cuenta_contable;
            bteProveedor.Tag = Obe.proc_icod_proveedor;
            dteFechaBaja.DateTime = Convert.ToDateTime(Obe.acfc_sfecha_baja);
            txtMotivo.Text = Obe.acfc_vmotivo;
            bteLocalidad.Text = Obe.lafc_vdescripcion;
            bteProveedor.Text = Obe.proc_vnombrecompleto;
            if (Obe.acfc_vfoto == null || Obe.acfc_vfoto == "")
            {
                imgFoto.Image = null;
            }
            else
            {
                imgFoto.Image = Image.FromFile(Obe.acfc_vfoto);
            }
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
                if (Convert.ToInt32(txtCodigo.Text) == 0)
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Ingrese código");
                }
                if (verificarCodigo(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("El código ingresado ya existe en los Conceptos por Ingreso");
                }
                /*----------------------*/
                if (String.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese la Descripción");
                }
                /*----------------------*/

                if (verificarNombre(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("La Descripción ya existe en los Conceptos por Ingreso");
                }

                /*----------------------*/
                if (String.IsNullOrWhiteSpace(txtCantidad.Text))
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("Ingrese la Cantidad");
                }

                /*----------------------*/
                if (dteFechaAdqui.DateTime == null || dteFechaAdqui.Text == "")
                {
                    oBase = dteFechaAdqui;
                    throw new ArgumentException("Ingrese la Fecha de Adquisición");
                }


                /*----------------------*/
                if (String.IsNullOrWhiteSpace(txtAñoVida.Text))
                {
                    oBase = txtAñoVida;
                    throw new ArgumentException("Ingrese el Año de Vida");
                }

                /*----------------------*/
                if (String.IsNullOrWhiteSpace(txtPorcentajeDeprec.Text))
                {
                    oBase = txtPorcentajeDeprec;
                    throw new ArgumentException("Ingrese el Pocentaje de Depreciación");
                }

                /*----------------------*/
                if (Convert.ToDecimal(txtPorcentajeDeprec.Text) >= 100)
                {
                    oBase = txtPorcentajeDeprec;
                    throw new ArgumentException("El Porcentaje no debe ser MAYOR al 100%");
                }


                /*----------------------*/
                if (dteInicioUso.DateTime == null || dteInicioUso.Text == "")
                {
                    oBase = dteInicioUso;
                    throw new ArgumentException("Ingrese la Fecha Inicio de Uso");
                }


                /*----------------------*/

                if (bteCCosto.Text == null || bteCCosto.Text == "")
                {
                    oBase = bteCCosto;
                    throw new ArgumentException("Ingrese el Centro de Costo");

                }
                /*----------------------*/
                if (bteCContable.Text == null || bteCContable.Text == "")
                {
                    oBase = bteCContable;
                    throw new ArgumentException("Ingrese las Cuenta Contable");

                }







                /*----------------------*/

                Obe.acfc_iid_activo_fijo = txtCodigo.Text;
                Obe.tarec_iid_clasificacion_af = Convert.ToInt32(lkpClasificacion.EditValue);
                Obe.tarec_iid_situacion_af = Convert.ToInt32(lkpSituacion.EditValue);
                Obe.acfc_vdescripcion = txtDescripcion.Text;
                Obe.acfc_vmarca = txtMarca.Text;
                Obe.acfc_vmodelo = txtModelo.Text;
                Obe.acfc_vserie = txtSerie.Text;
                Obe.acfc_vcaracterist = txtCaracteristicas.Text;
                Obe.acfc_icantidad = Convert.ToInt32(txtCantidad.Text);
                Obe.tarec_iid_tip_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                Obe.acfc_ncosto_act = Convert.ToDecimal(txtCostoActual.Text);
                Obe.acfc_ntotal_deprec = Convert.ToDecimal(txtTotalDepreciacion.Text);
                Obe.acfc_vcodigo_invent = txtCodigoInventario.Text;

                //-----
                if (bteLocalidad.Text == null || bteLocalidad.Text == "")
                { Obe.lafc_icod_localidad = 0; }
                else
                {
                    Obe.lafc_icod_localidad = Convert.ToInt32(bteLocalidad.Tag);
                }
                //-----
                if (dteFechaAdqui.DateTime == null || dteFechaAdqui.Text == "")
                {
                    Obe.acfc_sfech_adqui = (DateTime?)null;
                }
                else
                {
                    Obe.acfc_sfech_adqui = dteFechaAdqui.DateTime;
                }

                Obe.acfc_ncosto_adqui = Convert.ToDecimal(txtCostoAdqui.Text);
                Obe.acfc_ianio_vida = Convert.ToInt32(txtAñoVida.Text);
                Obe.acfc_nporct_deprec = Convert.ToDecimal(txtPorcentajeDeprec.Text);
                //-----
                if (dteFechaAlta.DateTime == null || dteFechaAlta.Text == "" || dteFechaAlta.Text == "01/01/0001")
                {
                    Obe.acfc_sfech_alta = (DateTime?)null;
                }
                else
                {
                    Obe.acfc_sfech_alta = dteFechaAlta.DateTime;
                }
                //-----
                if (dteInicioUso.DateTime == null || dteInicioUso.Text == "")
                {
                    Obe.acfc_sfecha_inic_uso = (DateTime?)null;
                }
                else
                {
                    Obe.acfc_sfecha_inic_uso = dteInicioUso.DateTime;
                }
                //-----
                if (bteCCosto.Text == null || bteCCosto.Text == "")
                { Obe.ccoc_icod_centro_costo = 0; }
                else
                {
                    Obe.ccoc_icod_centro_costo = Convert.ToInt32(bteCCosto.Tag);
                }
                //-----
                if (bteCContable.Text == null || bteCContable.Text == "")
                { Obe.ctacc_icod_cuenta_contable = 0; }
                else
                {
                    Obe.ctacc_icod_cuenta_contable = Convert.ToInt32(bteCContable.Tag);
                }
                //-----
                if (bteProveedor.Text == null || bteProveedor.Text == "")
                { Obe.proc_icod_proveedor = 0; }
                else
                {
                    Obe.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
                }

                //-----
                if (dteFechaBaja.DateTime == null || dteFechaBaja.Text == "" || dteFechaBaja.Text == "01/01/0001")
                {
                    Obe.acfc_sfecha_baja = (DateTime?)null;
                }
                else
                {
                    Obe.acfc_sfecha_baja = dteFechaBaja.DateTime;
                }

                Obe.acfc_vmotivo = txtMotivo.Text;
                Obe.ccoc_numero_centro_costo = bteCCosto.Text;
                Obe.ctacc_numero_cuenta_contable = bteCContable.Text;
                Obe.acfc_flag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.lafc_vdescripcion = bteLocalidad.Text;
                Obe.proc_vnombrecompleto = bteProveedor.Text;
                //----Imagen

                //Obe.acfc_vfoto = openFile1.FileName;               
                //imgFoto.Image = Image.FromFile(Obe.acfc_vfoto);


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.acfc_icod_activo_fijo = new BContabilidad().InsertarActivoFijo(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BContabilidad().modificarActivoFijo(Obe);
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
                    this.MiEvento(Obe.acfc_icod_activo_fijo);
                    this.Close();
                }
            }
        }

        private bool verificarNombre(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstActivoFijo.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstActivoFijo.Where(x => x.acfc_vdescripcion.Replace(" ", "").Trim() == strNombre.Replace(" ", "").Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstActivoFijo.Where(x => x.acfc_vdescripcion.Replace(" ", "").Trim() == strNombre.Replace(" ", "").Trim() && x.acfc_icod_activo_fijo != Obe.acfc_icod_activo_fijo).ToList().Count > 0)
                            exists = true;
                    }
                }
                return exists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool verificarCodigo(string strCodigo)
        {
            try
            {
                bool exists = false;
                if (lstActivoFijo.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstActivoFijo.Where(x => x.acfc_iid_activo_fijo == strCodigo).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstActivoFijo.Where(x => x.acfc_iid_activo_fijo == strCodigo && x.acfc_icod_activo_fijo != Obe.acfc_icod_activo_fijo).ToList().Count > 0)
                            exists = true;
                    }
                }
                return exists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmMantePersonal_Load(object sender, EventArgs e)
        {
            txtDescripcion.Focus();
            var lstMoneda = new BGeneral().listarTablaRegistro(5);
            var lstClasificacion = new BGeneral().listarTablaRegistro(76);
            var lstSituacion = new BGeneral().listarTablaRegistro(77);
            BSControls.LoaderLook(lkpMoneda, lstMoneda, "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpClasificacion, lstClasificacion, "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpSituacion, lstSituacion, "tarec_vdescripcion", "tarec_iid_tabla_registro", true);


            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {

                lkpClasificacion.EditValue = Obe.tarec_iid_clasificacion_af;//--   
                lkpMoneda.EditValue = Obe.tarec_iid_tip_moneda;//--
                lkpSituacion.EditValue = Obe.tarec_iid_situacion_af;//--
                if (dteFechaAlta.Text == "01/01/0001")
                {
                    dteFechaAlta.Text = null;
                }
                if (dteFechaBaja.Text == "01/01/0001")
                {
                    dteFechaBaja.Text = null;
                }

            }

            mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();

            mlista.ForEach(obe =>
            {
                //this.bteCCosto.Properties.Mask.BeepOnError = true;
                //this.bteCCosto.Properties.Mask.EditMask = obe.parac_vmascara;
                //this.bteCCosto.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                //this.bteCCosto.Properties.Mask.ShowPlaceHolders = false;

                this.bteCContable.Properties.Mask.BeepOnError = true;
                this.bteCContable.Properties.Mask.EditMask = obe.parac_vmascara;
                this.bteCContable.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.bteCContable.Properties.Mask.ShowPlaceHolders = false;
            });

        }
        public void LimpiarListas()
        {



            lkpClasificacion.EditValue = null;//--  
            lkpClasificacion.Text = null;
            lkpMoneda.EditValue = null;//--
            lkpMoneda.Text = null;
            lkpSituacion.EditValue = null;//--  
            lkpSituacion.Text = null;


        }
        private void lkpArea_EditValueChanged(object sender, EventArgs e)
        {


        }


        private void labelControl13_Click(object sender, EventArgs e)
        {

        }

        private void bteCuenta_EditValueChanged(object sender, EventArgs e)
        {


        }

        private void bteCuenta_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }
        private void ListarCuenta()
        {

            using (frmListarCentroCosto frm = new frmListarCentroCosto())
            {
                //frm.flagSeleccionImpresion = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteCCosto.Text = frm._Be.cecoc_vcodigo_centro_costo;
                    bteCCosto.Tag = frm._Be.cecoc_icod_centro_costo;

                }
            }
        }

        private void bteCuenta2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                frm.flagSeleccionImpresion = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteCContable.Text = frm._Be.ctacc_numero_cuenta_contable;
                    bteCContable.Tag = frm._Be.ctacc_icod_cuenta_contable;
                }
            }
        }

        private void bteCuenta2_EditValueChanged(object sender, EventArgs e)
        {
            if (bteCContable.Text == "")
            {

                return;
            }
            List<ECuentaContable> aux = new List<ECuentaContable>();
            aux = mlistCuenta.Where(x => x.ctacc_icod_cuenta_contable == Convert.ToInt32(bteCContable.Text.Replace(".", ""))).ToList();


            if (aux.Count == 1)
            {
                bteCContable.Tag = aux[0].ctacc_icod_cuenta_contable;

            }

        }
        private void ListarProveedor()
        {
            FrmListarProveedor Proveedor = new FrmListarProveedor();
            Proveedor.Carga();
            if (Proveedor.ShowDialog() == DialogResult.OK)
            {
                bteProveedor.Tag = Proveedor._Be.iid_icod_proveedor;//proc_icod_proveedor
                bteProveedor.Text = Proveedor._Be.vcod_proveedor;
                bteProveedor.Text = Proveedor._Be.vnombrecompleto;
            }

        }
        private void bteProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProveedor();
        }


        private void ListarLocalidad()
        {
            FrmListarLocalidad Localidad = new FrmListarLocalidad();
            Localidad.Carga();
            if (Localidad.ShowDialog() == DialogResult.OK)
            {
                bteLocalidad.Tag = Localidad._Be.lafc_icod_localidades;
                bteLocalidad.Text = Localidad._Be.lafc_iid_localidades;
                bteLocalidad.Text = Localidad._Be.lafc_vdescripcion;
            }

        }

        private void bteLocalidad_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarLocalidad();
        }
        public OpenFileDialog openFile1 = new OpenFileDialog();
        private void simpleButton1_Click(object sender, EventArgs e)
        {

            openFile1.Filter = "All files(*.*)|*.*";
            openFile1.ShowDialog();

            Obe.acfc_vfoto = openFile1.FileName;
            if (Obe.acfc_vfoto == null || Obe.acfc_vfoto == "")
            {
                imgFoto.Image = null;
            }
            else
            {
                imgFoto.Image = Image.FromFile(Obe.acfc_vfoto);
            }




        }

        private void btnEliminarImagen_Click(object sender, EventArgs e)
        {
            imgFoto.Image = null;
            Obe.acfc_vfoto = null;
        }

        private void bteCCosto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCuenta();
        }

        private void bteCCosto_EditValueChanged(object sender, EventArgs e)
        {
            if (bteCCosto.Text == "")
            {

                return;
            }
            List<ECentroCosto> aux = new List<ECentroCosto>();
            aux = mlistaCentroCosto.Where(x => x.cecoc_vcodigo_centro_costo == bteCCosto.Text).ToList();


            if (aux.Count == 1)
            {
                bteCCosto.Tag = aux[0].cecoc_icod_centro_costo;
            }
        }

    }
}