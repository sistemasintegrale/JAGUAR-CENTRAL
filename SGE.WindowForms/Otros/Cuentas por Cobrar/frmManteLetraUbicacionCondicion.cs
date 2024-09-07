using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class frmManteLetraUbicacionCondicion : DevExpress.XtraEditors.XtraForm
    {
        public ELetraPorCobrar oBeLetraXC = new ELetraPorCobrar();
        public ELetraPorPagar oBeLetraXP = new ELetraPorPagar();
        //-----------------
        public bool flagLXC = false;

        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        public frmManteLetraUbicacionCondicion()
        {
            InitializeComponent();
        }

        private void frmManteLetraUbicacionCondicion_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            BSControls.LoaderLook(LkpCondicion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaCondicion), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(LkpUbicacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaUbicacion), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(lkpBanco, new BTesoreria().listarBancos(), "bcoc_vnombre_banco", "bcoc_icod_banco", true);
        }

        public void setValues()
        {
            if (flagLXC)
            {
                txtLetra.Text = oBeLetraXC.lexcc_vnumero_letra;
                LkpCondicion.EditValue = oBeLetraXC.tablc_iid_condicion_letra;
                lkpBanco.EditValue = oBeLetraXC.efinc_icod_entidad_financiera;


                LkpUbicacion.EditValue = oBeLetraXC.tablc_iid_ubicacion_letra;
                txtNumeroUnico.Text = oBeLetraXC.lexcc_vnumero_ubd;
            }
            else
            {
                txtLetra.Text = oBeLetraXP.lexpc_vnumero_letra;
                LkpCondicion.EditValue = oBeLetraXP.tablc_iid_condicion_letra;
                lkpBanco.EditValue = oBeLetraXP.efinc_icod_entidad_financiera;


                LkpUbicacion.EditValue = oBeLetraXP.tablc_iid_ubicacion_letra;
                txtNumeroUnico.Text = oBeLetraXP.lexpc_vnumero_ubd;
            }
        }

        private void setSave()
        {
            bool flag = true;
            BaseEdit oBase = null;

            try
            {
                int? intNullVal = null;

                if (Convert.ToInt32(LkpUbicacion.EditValue) == 0)
                {
                    oBase = LkpUbicacion;
                    throw new ArgumentException("Seleccione la ubicación de la letra");
                }

                if (Convert.ToInt32(LkpUbicacion.EditValue) == 2)
                {
                    if (Convert.ToInt32(lkpBanco.EditValue) == 0)
                    {
                        oBase = lkpBanco;
                        throw new ArgumentException("Seleccion el banco");
                    }
                    if (String.IsNullOrEmpty(txtNumeroUnico.Text))
                    {
                        oBase = txtNumeroUnico;
                        throw new ArgumentException("Ingreso número requerido");
                    }
                }

                if (Convert.ToInt32(LkpCondicion.EditValue) == 0)
                {
                    oBase = LkpUbicacion;
                    throw new ArgumentException("Seleccione la condición de la letra");
                }

                if (flagLXC)
                {
                    oBeLetraXC.tablc_iid_condicion_letra = Convert.ToInt32(LkpCondicion.EditValue);
                    oBeLetraXC.efinc_icod_entidad_financiera = (Convert.ToInt32(lkpBanco.EditValue) == 0) ? intNullVal : Convert.ToInt32(lkpBanco.EditValue);
                    oBeLetraXC.lexcc_vnumero_ubd = txtNumeroUnico.Text;
                    oBeLetraXC.tablc_iid_ubicacion_letra = Convert.ToInt32(LkpUbicacion.EditValue);
                    oBeLetraXC.intUsuario = Valores.intUsuario;
                    oBeLetraXC.strPc = WindowsIdentity.GetCurrent().Name;

                    new BCuentasPorCobrar().modificarLetraPorCobrarUbiCon(oBeLetraXC);
                }
                else
                {
                    oBeLetraXP.tablc_iid_condicion_letra = Convert.ToInt32(LkpCondicion.EditValue);
                    oBeLetraXP.efinc_icod_entidad_financiera = (Convert.ToInt32(lkpBanco.EditValue) == 0) ? intNullVal : Convert.ToInt32(lkpBanco.EditValue);
                    oBeLetraXP.lexpc_vnumero_ubd = txtNumeroUnico.Text;
                    oBeLetraXP.tablc_iid_ubicacion_letra = Convert.ToInt32(LkpUbicacion.EditValue);
                    oBeLetraXP.intUsuario = Valores.intUsuario;
                    oBeLetraXP.strPc = WindowsIdentity.GetCurrent().Name;

                    new BCuentasPorPagar().modificarLetraPorPagarUbiCon(oBeLetraXP);
                }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag = false;
            }
            finally
            {
                if (flag)
                {
                    if (flagLXC)
                    {
                        this.MiEvento(oBeLetraXC.lexcc_icod_correlativo);
                        this.Close();
                    }
                    else
                    {
                        this.MiEvento(oBeLetraXP.lexpc_icod_correlativo);
                        this.Close();
                    }
                }
            }

        }

        private void LkpUbicacion_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(LkpUbicacion.EditValue) == 2)
            {
                lkpBanco.Enabled = true;
                txtNumeroUnico.Enabled = true;

                lkpBanco.ItemIndex = 0;
                txtNumeroUnico.Text = string.Empty;
            }
            else
            {
                lkpBanco.Enabled = false;
                txtNumeroUnico.Enabled = false;

                lkpBanco.EditValue = null;
                txtNumeroUnico.Text = string.Empty;
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}