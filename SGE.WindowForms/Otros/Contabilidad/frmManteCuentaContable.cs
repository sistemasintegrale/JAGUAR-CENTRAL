using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmManteCuentaContable : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteCuentaContable));
        public delegate void DelegadoMensaje(long intIcod);
        public event DelegadoMensaje MiEvento;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        public ECuentaContable Obe = new ECuentaContable();
        public List<ECuentaContable> lstCuentasContables = new List<ECuentaContable>();
        public List<EParametroContable> lstParametroContable = new List<EParametroContable>();

        [DXCategory("Events")]
        public event InvalidValueExceptionEventHandler InvalidValue;

        public frmManteCuentaContable()
        {
            InitializeComponent();
        }
        private void FrmCuentaContable_Load(object sender, EventArgs e)
        {
            cargar();
            loadMask();
        }

        public BSMaintenanceStatus Status
        {
            get
            {
                return (mStatus);
            }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);

            if (Status == BSMaintenanceStatus.View)
            {
                txtCuentaContable.Enabled = !Enabled;
                txtDescripcionLarga.Enabled = !Enabled;
                ChkCentroCosto.Enabled = !Enabled;
                lkpTipoCuenta.Enabled = !Enabled;
                LkpTipoAnalitica.Enabled = !Enabled;
                bteCuentaDebe.Enabled = !Enabled;
                bteCuentaHaber.Enabled = !Enabled;
                LkpEstado.Enabled = !Enabled;
                lkpTipoMoneda.Enabled = !Enabled;
                btnGuardar.Enabled = !Enabled;
                BtnModificar.Enabled = !Enabled;
                TreeView1.Enabled = !Enabled;
                txtbuscar.Enabled = !Enabled;
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtCuentaContable.Enabled = Enabled;
            }
        }

        private void cargar()
        {
            ETablaRegistro oBe = new ETablaRegistro() { tarec_icorrelativo_registro = 0, tarec_vdescripcion = "NINGUNO" };
            var lstTipoAnalitica = new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoAnalitica);
            lstTipoAnalitica.Insert(0, oBe);

            BSControls.LoaderLook(lkpTipoCuenta, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoCuentaContable), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(lkpTipoMoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(LkpTipoAnalitica, lstTipoAnalitica, "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(LkpEstado, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaEstado), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        private Boolean validarNivel()
        {
            Boolean Flag = true;
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                String Longitud = "";
                lstParametroContable.ForEach(obe =>
                {
                    Longitud = obe.parac_vvalor_texto;
                });

                string Cadena = txtCuentaContable.Text;
                string[] arreglo = Cadena.Split('.');
                string[] Longitudes = Longitud.Split('-');
                string Cuenta = "";
                int cont = -1;
                foreach (string word in arreglo)
                {
                    cont++;
                    Cuenta = Cuenta + word;
                    if (word != "")
                        Longitud = Longitudes[cont];
                }

                if (!string.IsNullOrEmpty(arreglo[5].ToString()))
                {
                    Longitud = (arreglo.GetValue(5).ToString().Length).ToString();
                }

                Cadena = Cuenta.Substring(0, Cuenta.Length - Convert.ToInt32(Longitud));
                if (Cadena.Length != 0)
                {
                    var Lista = lstCuentasContables.Where(ob => ob.ctacc_icod_cuenta_contable == Convert.ToInt64(Cadena)).ToList();
                    if (Lista.Count > 0)
                        Flag = true;
                    else
                        Flag = false;
                }
            }
            else
            {
                Flag = true;
            }



            return Flag;
        }


        public void SetSave()
        {
            BaseEdit oBase = null;
            Boolean flag = true;
            try
            {
                if (string.IsNullOrEmpty(txtCuentaContable.Text))
                {
                    oBase = txtCuentaContable;
                    throw new ArgumentException("Ingrese cuenta contable");
                }



                if (validarNivel() == true)
                {
                    if (string.IsNullOrEmpty(txtDescripcionLarga.Text))
                    {
                        oBase = txtDescripcionLarga;
                        throw new ArgumentException("Ingrese descripción");
                    }

                    String Longitud = "";
                    lstParametroContable.ForEach(obes =>
                    {
                        Longitud = obes.parac_vvalor_texto;
                    });

                    string Cadena = txtCuentaContable.Text;
                    string[] arreglo = Cadena.Split('.');
                    string[] Longitudes = Longitud.Split('-');
                    string Cuenta = "";
                    int x = 0;
                    int i = -1;
                    foreach (string word in arreglo)
                    {
                        x++;
                        i++;
                        Cuenta = Cuenta + word;
                        Obe.ctacc_icod_cuenta_contable = Convert.ToInt64(Cuenta.Trim());
                        Obe.ctacc_iid_cuenta_padre = Convert.ToInt64(Cuenta.Trim());
                        Longitud = Longitudes[i];
                        if (word != "")
                        {
                            if (x == 1)
                                Obe.ctacc_ = Convert.ToInt32(Cuenta.Substring(0, 2));
                            else
                                Obe.ctacc_ = Convert.ToInt32(Cuenta.Substring(0, Cuenta.Length - Convert.ToInt32(Longitud)));
                        }
                    }
                    Obe.anioc_iid_anio = Parametros.intEjercicio;
                    Obe.ctacc_numero_cuenta_contable = txtCuentaContable.Text;
                    Obe.ctacc_nombre_descripcion = txtDescripcionLarga.Text;
                    Obe.ctacc_nivel_cuenta = getNivel();
                    Obe.tablc_iid_tipo_cuenta = Convert.ToInt32(lkpTipoCuenta.EditValue);
                    Obe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpTipoMoneda.EditValue);
                    Obe.tablc_iid_tipo_analitica = Convert.ToInt32(LkpTipoAnalitica.EditValue);
                    Obe.ctacc_flag_estado = Convert.ToBoolean(LkpEstado.EditValue);
                    Obe.ctacc_iccosto = ChkCentroCosto.Checked;
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;

                    if (bteCuentaDebe.Tag != null || bteCuentaHaber.Tag != null)
                    {
                        if (bteCuentaDebe.Tag != null && bteCuentaHaber.Tag == null)
                        {
                            oBase = bteCuentaHaber;
                            throw new ArgumentException("Ingrese Cuenta Haber automática");
                        }
                        if (bteCuentaDebe.Tag == null && bteCuentaHaber.Tag != null)
                        {
                            oBase = bteCuentaDebe;
                            throw new ArgumentException("Ingrese Cuenta Debe automática");
                        }
                        if (bteCuentaDebe.Tag != null && bteCuentaHaber.Tag != null)
                        {
                            Obe.ctacc_icod_cuenta_debe_auto = Convert.ToInt32(bteCuentaDebe.Tag);
                            Obe.ctacc_icod_cuenta_haber_auto = Convert.ToInt32(bteCuentaHaber.Tag);
                        }
                    }

                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        var Lista = lstCuentasContables.Where(ob => ob.ctacc_icod_cuenta_contable == Obe.ctacc_icod_cuenta_contable).ToList();
                        if (Lista.Count > 0)
                            throw new Exception("La cuenta contable ya existe");
                    }

                    if (Status == BSMaintenanceStatus.CreateNew)
                        new BContabilidad().insertarCuentaContable(Obe);
                    else
                        new BContabilidad().modificarCuentaContable(Obe);
                }
                else
                {
                    throw new Exception("Nivel de cuenta incorrecto");
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
                flag = false;
            }
            finally
            {
                if (flag)
                {
                    this.MiEvento(Obe.ctacc_icod_cuenta_contable);
                    this.Close();
                }
            }
        }


        private int getNivel()
        {
            int contador = 0;
            string[] arreglo = txtCuentaContable.Text.Split('.');
            for (int i = 0; i < arreglo.Length; i++)
            {
                if (arreglo[i] != "")
                    contador = contador + 1;
            }
            return contador;
        }

        public string mask;

        private void loadMask()
        {
            if (lstParametroContable.Count == 0)
            {
                XtraMessageBox.Show("No se ha registrado una Estructura de Cuenta Contable \nNota: Registre la Estructura de Cuenta Contable en la opción de Registro de Parámetros Contables...", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }

            lstParametroContable.ForEach(obe =>
            {
                mask = obe.parac_vmascara;
                this.txtCuentaContable.Properties.Mask.EditMask = obe.parac_vmascara;
                this.txtCuentaContable.Properties.Mask.MaskType = MaskType.RegEx;
                this.txtCuentaContable.Properties.Mask.ShowPlaceHolders = false;

            });

        }

        private void textEdit1_Validating(object sender, CancelEventArgs e)
        {
            TextEdit textEdit = sender as TextEdit;
            string editValue = textEdit.EditValue as string;
            Regex regex = new Regex("^[a-zA-Z0-9 ]*$");
            if (!regex.IsMatch(editValue))
            {
                txtCuentaContable.ErrorText = "The name should only contain alphanumeric characters";
                e.Cancel = true;
            }
        }

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }
        public void setValues()
        {
            int? nullVall = null;
            txtCuentaContable.Text = Obe.ctacc_numero_cuenta_contable;
            txtDescripcionLarga.Text = Obe.ctacc_nombre_descripcion;

            lkpTipoCuenta.EditValue = Convert.ToInt32(Obe.tablc_iid_tipo_cuenta);
            lkpTipoMoneda.EditValue = Convert.ToInt32(Obe.tablc_iid_tipo_moneda);
            LkpTipoAnalitica.EditValue = Convert.ToInt32(Obe.tablc_iid_tipo_analitica);
            /*---------------------------------------------------------------------------------------------------------------------*/
            bteCuentaDebe.Text = (Obe.ctacc_icod_cuenta_debe_auto != null) ? Obe.ctacc_icod_cuenta_debe_auto.ToString() : "";
            bteCuentaDebe.Tag = (Obe.ctacc_icod_cuenta_debe_auto != null) ? Convert.ToInt32(Obe.ctacc_icod_cuenta_debe_auto) : nullVall;
            txtDescripcionDebe.Text = (Obe.ctacc_icod_cuenta_debe_auto != null) ?
                lstCuentasContables.Where(x => x.ctacc_icod_cuenta_contable == Convert.ToInt32(Obe.ctacc_icod_cuenta_debe_auto)).ToList()[0].ctacc_nombre_descripcion : "";
            /*---------------------------------------------------------------------------------------------------------------------*/
            bteCuentaHaber.Text = (Obe.ctacc_icod_cuenta_haber_auto != null) ? Obe.ctacc_icod_cuenta_haber_auto.ToString() : "";
            bteCuentaHaber.Tag = (Obe.ctacc_icod_cuenta_haber_auto != null) ? Convert.ToInt32(Obe.ctacc_icod_cuenta_haber_auto) : nullVall;
            txtDescripcionHaber.Text = (Obe.ctacc_icod_cuenta_haber_auto != null) ?
                lstCuentasContables.Where(x => x.ctacc_icod_cuenta_contable == Convert.ToInt32(Obe.ctacc_icod_cuenta_haber_auto)).ToList()[0].ctacc_nombre_descripcion : "";
            /*---------------------------------------------------------------------------------------------------------------------*/
            LkpEstado.EditValue = Convert.ToInt32(Obe.ctacc_flag_estado);
            ChkCentroCosto.Checked = Obe.ctacc_iccosto;
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void lkpTipoCuenta_EditValueChanged(object sender, EventArgs e)
        {
            enabling();
        }
        private void enabling()
        {
            if (Convert.ToInt32(lkpTipoCuenta.EditValue) == 1)
            {
                lkpTipoMoneda.EditValue = 1;
                lkpTipoMoneda.Enabled = false;
                /*--------------------------------------------------------*/
                LkpTipoAnalitica.EditValue = null;
                LkpTipoAnalitica.Enabled = false;
                /*--------------------------------------------------------*/
                bteCuentaDebe.Enabled = false;
                bteCuentaDebe.Text = string.Empty;
                bteCuentaDebe.Tag = null;
                txtDescripcionDebe.Text = string.Empty;
                /*--------------------------------------------------------*/
                bteCuentaHaber.Enabled = false;
                bteCuentaHaber.Text = string.Empty;
                bteCuentaHaber.Tag = null;
                txtDescripcionHaber.Text = string.Empty;
                /*--------------------------------------------------------*/
                if (Status == BSMaintenanceStatus.CreateNew)
                    LkpEstado.EditValue = 1;
                /*--------------------------------------------------------*/
                ChkCentroCosto.Checked = false;
                ChkCentroCosto.Enabled = false;
            }
            else
            {
                lkpTipoMoneda.Enabled = true;
                LkpTipoAnalitica.Enabled = true;
                bteCuentaDebe.Enabled = true;
                bteCuentaHaber.Enabled = true;
                ChkCentroCosto.Enabled = true;
            }
        }
        private void bteCuentaDebe_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmListarCuentaContable frm = new frmListarCuentaContable();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm._Be.ctacc_icod_cuenta_contable == Convert.ToInt32(bteCuentaHaber.Tag))
                {
                    XtraMessageBox.Show("La 1era cuenta automática debe ser diferente a la 2da cuenta automática", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    bteCuentaDebe.Text = frm._Be.ctacc_numero_cuenta_contable;
                    bteCuentaDebe.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    txtDescripcionDebe.Text = frm._Be.ctacc_nombre_descripcion;
                }

            }
        }

        private void bteCuentaHaber_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmListarCuentaContable Litautomatica = new frmListarCuentaContable();
            if (Litautomatica.ShowDialog() == DialogResult.OK)
            {
                if (Litautomatica._Be.ctacc_icod_cuenta_contable == Convert.ToInt32(bteCuentaDebe.Tag))
                {
                    XtraMessageBox.Show("La 2da cuenta automática debe ser diferente a la 1era cuenta automática", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    bteCuentaHaber.Text = Litautomatica._Be.ctacc_numero_cuenta_contable;
                    bteCuentaHaber.Tag = Litautomatica._Be.ctacc_icod_cuenta_contable;
                    txtDescripcionHaber.Text = Litautomatica._Be.ctacc_nombre_descripcion;
                }
            }
        }

        private void btnGuardar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtCuentaContable.Text = string.Empty;
            this.Close();
        }

        private void txtCuentaContable_InvalidValue(object sender, InvalidValueExceptionEventArgs e)
        {

            e.ErrorText = "formato incorrecto";
        }

        private void txtCuentaContable_Validating(object sender, CancelEventArgs e)
        {

            Regex regex = new Regex(mask);
            if (txtCuentaContable.Text != mask)
            {
                string cadena = txtCuentaContable.Text;

                if (cadena.Length == 7)
                {
                    e.Cancel = false;
                }
                else
                if (cadena.Length == 9)
                {
                    e.Cancel = false;
                }
                else
                if (cadena.Length == 11)
                {
                    e.Cancel = false;
                }
                else
                if (cadena.Length == 13)
                {
                    e.Cancel = false;
                }
                else
                if (cadena.Length == 15)
                {
                    e.Cancel = false;
                }
                else
                if (cadena.Length > 15)
                {
                    e.Cancel = false;
                }
                else if (cadena.Length == 0)
                {
                    e.Cancel = false;
                }
                else
                    e.Cancel = true;
            }
            else
                e.Cancel = false;

        }
    }
}