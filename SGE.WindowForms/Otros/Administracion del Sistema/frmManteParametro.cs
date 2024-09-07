using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Administracion_del_Sistema
{
    public partial class frmManteParametro : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteParametro));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public ControlEquipos objEquipo = new ControlEquipos();
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public EParametro oBe = new EParametro();

        public frmManteParametro()
        {
            InitializeComponent();
        }

        private void FrmVariables_Load(object sender, EventArgs e)
        {
            btnUbicacionActualizador.Text = Valores.strUbicacionActualizador;
        }

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetSave();
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
            txtIGV.Enabled = !Enabled;
            txtUIT.Enabled = !Enabled;
            txt4taCategoria.Enabled = !Enabled;
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

        private async void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (txtIGV.Text == "0.00")
                {
                    oBase = txtIGV;
                    throw new ArgumentException("Ingrese IGV");
                }


                if (txtUIT.Text == "0.00")
                {
                    oBase = txtIGV;
                    throw new ArgumentException("Ingrese UIT");
                }
                if (string.IsNullOrEmpty(txtEmpresa.Text))
                {
                    oBase = txtEmpresa;
                    throw new ArgumentException("Ingrese nombre de empresa");
                }
                if (string.IsNullOrEmpty(txtDireccion.Text))
                {
                    oBase = txtDireccion;
                    throw new ArgumentException("Ingrese dirección fiscal de la empresa");
                }
                if (string.IsNullOrEmpty(txtRuc.Text))
                {
                    oBase = txtRuc;
                    throw new ArgumentException("Ingrese RUC de la empresa");
                }

                oBe.pm_nigv_parametro = Convert.ToDecimal(txtIGV.Text);
                oBe.pm_nuit_parametro = Convert.ToDecimal(txtUIT.Text);
                oBe.pm_ncategoria_parametro = Convert.ToDecimal(txt4taCategoria.Text);
                oBe.pm_nombre_empresa = txtEmpresa.Text;
                oBe.pm_direccion_empresa = txtDireccion.Text;
                oBe.pm_vruc = txtRuc.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.pm_correlativo_OR = Convert.ToInt64(txtCorrelativoOR.Text);
                oBe.urlServicioFacturaElectronica = txtServicioFE.Text;
                oBe.urlServicioNotaCredito = txtServicioNC.Text;
                oBe.urlServicioNotaDebito = txtServicioND.Text;
                oBe.Ruc = txtRucFE.Text;
                oBe.UsuarioSol = txtUsuarioSol.Text;
                oBe.ClaveSol = txtClaveSol.Text;
                oBe.EndPointUrlPrueba = txtURLPrueba.Text;
                oBe.EndPointUrlDesarrollo = txtURLDesarrollo.Text;
                oBe.PasswordCertificado = txtPwCertificado.Text;
                oBe.urlServicioEnviarDocumento = txtServicioEnviarDoc.Text;
                oBe.urlServicioFirma = txtServicioFirma.Text;
                oBe.IdServiceValidacion = txtValidacion.Text;
                oBe.pm_sfecha_inicio = Convert.ToDateTime(dtmFechaInicio.EditValue);
                oBe.urlServicioEnvioResumen = txtServicioER.Text;
                oBe.urlServicoGenerarResumen = txtServicioGR.Text;
                oBe.IdServiceValidacionResumen = txtValidacionResumen.Text;
                oBe.urlServicioDocumentoBaja = txtServicioDB.Text;
                oBe.ServiceConsultaTiket = txtServicioCT.Text;
                oBe.pm_correlativo_doc_sunat = Convert.ToInt32(txtDocSUNAT.Text);
                oBe.DirecciónXML = txtDireccionModelos.Text;
                oBe.UrlBaseServicioGRE = txtUrlServicioGRE.Text;
                oBe.UrlBaseEnvioGRE = txtUrlEnvioGRE.Text;
                oBe.usuarioGRE = txtUsuarioGR.Text;
                oBe.claveGRE = txtClaveGR.Text;
                oBe.UsuarioSolGRE = txtUsuarioSolGRE.Text;
                oBe.ClaveSolGRE = txtClaveSolGRE.Text;
                oBe.pm_porc_retencion = Convert.ToDecimal(txtRetenciones.Text);

                new BAdministracionSistema().modificarParametro(oBe);
                Constantes.PorcentajeRetencion = Convert.ToDecimal(txtRetenciones.Text);
                objEquipo = new BAdministracionSistema().Equipo_Obtner_Datos(Valores.strNombreEquipo, Valores.strIdCpu);
                objEquipo.cep_vubicacion_actualizador = btnUbicacionActualizador.Text;
                Valores.strUbicacionActualizador = btnUbicacionActualizador.Text;
                await new BAdministracionSistema().Equipo_Modificar(objEquipo);

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
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento();
                    this.Close();
                }
            }
        }

        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnUbicacionActualizador_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                btnUbicacionActualizador.Text = openFileDialog.FileName;
            }
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }
    }
}