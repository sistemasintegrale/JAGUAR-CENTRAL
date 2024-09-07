using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;


namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmManteFamiliaDet : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteFamiliaDet));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EFamiliaDet Obe = new EFamiliaDet();
        public List<EFamiliaDet> lstFamiliaDet = new List<EFamiliaDet>();
        public int intIcodFamiliaCab = 0;


        public frmManteFamiliaDet()
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
            txtAbreviatura.Enabled = !Enabled;
            txtDescripcion.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtAbreviatura.Enabled = Enabled;
            if (Status == BSMaintenanceStatus.CreateNew)
                txtAbreviatura.Enabled = !Enabled;
        }
        public void setValues()
        {

            txtAbreviatura.Text = Obe.famid_vabreviatura;
            txtDescripcion.Text = Obe.famid_vdescripcion;

            bteCuentaSerPropio.Tag = Obe.cuenta_iservicio_propio;
            bteCuentaSerPropio.Text = Obe.NumeroCuentaSerPro;
            txtCuentaPropio.Text = Obe.DesCuentaSerPro;

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
                /*----------------------*/
                if (String.IsNullOrEmpty(txtAbreviatura.Text))
                {
                    oBase = txtAbreviatura;
                    throw new ArgumentException("Ingrese código de Sub - Línea");
                }
                if (verificarAbreviaturaFamilia(txtAbreviatura.Text))
                {
                    oBase = txtAbreviatura;
                    throw new ArgumentException("El código ingresado ya existe en los registros de Sub - Línea");
                }
                /*----------------------*/
                if (String.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese descripción de la Sub - Línea");
                }
                if (verificarDescripcionFamilia(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("La descripción ingresada ya existe en los registros de Sub - Línea");
                }


                //Obe.famid_iid_familia = lstFamiliaDet.Count + 1;
                Obe.famic_icod_familia = intIcodFamiliaCab;
                Obe.famid_vabreviatura = txtAbreviatura.Text;
                Obe.famid_vdescripcion = txtDescripcion.Text;
                Obe.famid_flag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.cuenta_iservicio_propio = Convert.ToInt32(bteCuentaSerPropio.Tag);


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.famic_icod_familia = new BAlmacen().insertarFamiliaDet(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BAlmacen().modificarFamiliaDet(Obe);
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
                    this.MiEvento(Obe.famid_icod_familia_tipo);
                    this.Close();
                }
            }
        }
        private bool verificarCodigoFamilia(string strCodigo)
        {
            try
            {
                bool exists = false;
                if (lstFamiliaDet.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstFamiliaDet.Where(x => x.famid_iid_familia.ToString().Trim() == Convert.ToInt32(strCodigo).ToString().Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstFamiliaDet.Where(x => x.famid_iid_familia.ToString().Trim() == Convert.ToInt32(strCodigo).ToString().Trim() && x.famic_icod_familia != Obe.famic_icod_familia).ToList().Count > 0)
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
        private bool verificarAbreviaturaFamilia(string strAbreviatura)
        {
            try
            {
                bool exists = false;
                if (lstFamiliaDet.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstFamiliaDet.Where(x => x.famid_vabreviatura.Trim() == strAbreviatura.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstFamiliaDet.Where(x => x.famid_vabreviatura.Trim() == strAbreviatura.Trim() && x.famic_icod_familia != Obe.famic_icod_familia).ToList().Count > 0)
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
        private bool verificarDescripcionFamilia(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstFamiliaDet.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstFamiliaDet.Where(x => x.famid_vdescripcion.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstFamiliaDet.Where(x => x.famid_vdescripcion.Trim() == strNombre.Trim() && x.famic_icod_familia != Obe.famic_icod_familia).ToList().Count > 0)
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

        private void frmManteFamiliaDet_Load(object sender, EventArgs e)
        {

        }

        //private void bteCuenta_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    listarCuenta1(sender);
        //}
        //private void listarCuenta1(object sender)
        //{
        //    ButtonEdit opcion = (ButtonEdit)sender;
        //    using (frmListarCuentaContable frm = new frmListarCuentaContable())
        //    {
        //        if (frm.ShowDialog() == DialogResult.OK)
        //        {
        //            opcion.Tag = frm._Be.ctacc_icod_cuenta_contable;
        //            opcion.Text = frm._Be.ctacc_numero_cuenta_contable.ToString();
        //            txtCuentaTerceros.Text = frm._Be.ctacc_nombre_descripcion;

        //                //case "bteCtaCompras":
        //                //    txtCuentaCompras.Text = frm._Be.ctacc_nombre_descripcion;
        //                //    break;
        //                //case "bteCtaCostos":
        //                //    txtCuentaCostos.Text = frm._Be.ctacc_nombre_descripcion;
        //                //    break;

        //        }
        //    }
        //}
        //private void listarCuenta2(object sender)
        //{
        //    ButtonEdit opcion = (ButtonEdit)sender;
        //    using (frmListarCuentaContable frm = new frmListarCuentaContable())
        //    {
        //        if (frm.ShowDialog() == DialogResult.OK)
        //        {
        //            opcion.Tag = frm._Be.ctacc_icod_cuenta_contable;
        //            opcion.Text = frm._Be.ctacc_numero_cuenta_contable.ToString();


        //                    txtCuentaPropio.Text = frm._Be.ctacc_nombre_descripcion;

        //                //case "bteCtaCompras":
        //                //    txtCuentaCompras.Text = frm._Be.ctacc_nombre_descripcion;
        //                //    break;
        //                //case "bteCtaCostos":
        //                //    txtCuentaCostos.Text = frm._Be.ctacc_nombre_descripcion;
        //               //    break;

        //        }
        //    }
        //}
        //private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    listarCuenta2(sender);
        //}
    }
}