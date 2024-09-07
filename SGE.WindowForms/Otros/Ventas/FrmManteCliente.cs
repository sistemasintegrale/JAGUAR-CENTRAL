using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
using static SGE.Common.Codigos;

namespace SGE.WindowForms.Otros.Tesoreria.Ventas
{
    public partial class FrmManteCliente : DevExpress.XtraEditors.XtraForm
    {
        ComponentResourceManager resources = new ComponentResourceManager(typeof(FrmManteCliente));
        public delegate void DelegadoMensaje(int intCod);
        public event DelegadoMensaje MiEvento;

        public FrmManteCliente()
        {
            KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();
        }

        public string habilitar = "";
        public List<EUbicacion> lUbicacion = null;
        public int? anac_icod_analitica;
        public List<ECliente> oDetail;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        private BVentas Obl;
        public int Correlative = 0;
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
            txtCodigoCliente.Enabled = !Enabled;
            LkpSituacion.Enabled = !Enabled;
            LkpTipoDoc.Enabled = !Enabled;
            txtDocumento.Enabled = !Enabled;
            LkpGiro.Enabled = !Enabled;
            LkpPais.Enabled = !Enabled;
            LkpDepartamento.Enabled = !Enabled;
            LkpProvincia.Enabled = !Enabled;
            LkpDistrito.Enabled = !Enabled;
            txtRazonSocial.Enabled = !Enabled;
            txtComercial.Enabled = !Enabled;
            txtDireccion.Enabled = !Enabled;
            txtTelefono.Enabled = !Enabled;
            txtFax.Enabled = !Enabled;
            txtCelular.Enabled = !Enabled;
            txtCorreo.Enabled = !Enabled;
            LkpVendedor.Enabled = !Enabled;
            txtContacto.Enabled = !Enabled;
            LkpEmpresaRelacionada.Enabled = !Enabled;
            deFecha.Enabled = !Enabled;
            LkpTipoCliente.Enabled = !Enabled;
            txtRUC.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtCodigoCliente.Enabled = Enabled;
            }
            LkpSituacion.Focus();

        }

        private void clearControl()
        {
            txtCodigoCliente.Text = string.Empty;
            LkpSituacion.EditValue = null;
            LkpTipoDoc.EditValue = null;
            LkpGiro.EditValue = null;
            LkpPais.EditValue = null;
            LkpDepartamento.EditValue = null;
            LkpProvincia.EditValue = null;
            LkpDistrito.EditValue = null;
            txtRazonSocial.Text = string.Empty;
            txtComercial.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtCelular.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            LkpVendedor.EditValue = null;
            txtContacto.Text = string.Empty;
            LkpEmpresaRelacionada.EditValue = null;
            deFecha.EditValue = null;
            txtNombre.Text = string.Empty;
            txtPaterno.Text = string.Empty;
            txtMaterno.Text = string.Empty;
            LkpTipoCliente.EditValue = null;
            txtRUC.Text = string.Empty;
        }

        private void FrmManteCliente_Load(object sender, EventArgs e)
        {
            lUbicacion = new BVentas().ListarUbicacion();
            cargar();
        }

        private void cargar()
        {
            BSControls.LoaderLook(LkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaEstado), "tarec_vdescripcion", "tarec_icorrelativo_registro", false);
            BSControls.LoaderLook(LkpTipoCliente, new BGeneral().listarTablaRegistro(22), "tarec_vdescripcion", "tarec_icorrelativo_registro", false);
            BSControls.LoaderLook(LkpTipoDoc, new BGeneral().listarTablaRegistro((int)TipoDocumentoPersonal.Id), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(LkpGiro, new BVentas().ListarGiro(), "giroc_vnombre_giro", "giroc_icod_giro", false);
            BSControls.LoaderLook(LkpPais, lUbicacion.Where(obj => obj.tablc_iid_tipo_ubicacion == 4).ToList(), "ubicc_vnombre_ubicacion", "ubicc_icod_ubicacion", false);
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
            LkpSituacion.ItemIndex = 0;
        }

        public void SetModify() => Status = BSMaintenanceStatus.ModifyCurrent;
        public void SetCancel() => Status = BSMaintenanceStatus.View;

        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }


        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            ECliente oBe = new ECliente();
            Obl = new BVentas();
            try
            {
                if (string.IsNullOrEmpty(txtCodigoCliente.Text))
                {
                    oBase = txtCodigoCliente;
                    throw new ArgumentException("Ingresar Código");
                }

                var codcli = oDetail.Where(oB => oB.cliec_vcod_cliente.ToUpper() == txtCodigoCliente.Text.ToUpper() && oB.cliec_icod_cliente != Correlative).ToList();
                if (codcli.Count > 0)
                {
                    oBase = txtCodigoCliente;
                    throw new ArgumentException("El Código ya existe");
                }

                if (Convert.ToInt32(LkpTipoCliente.EditValue) == 3 && LkpTipoDoc.EditValue == null)
                {
                    oBase = LkpTipoDoc;
                    throw new ArgumentException("Seleccionar Tipo de Documento");
                }
                if (Convert.ToInt32(LkpTipoCliente.EditValue) == 1 && LkpTipoDoc.EditValue == null)
                {
                    oBase = LkpTipoDoc;
                    throw new ArgumentException("Seleccionar Tipo de Documento");
                }
                if (LkpTipoCliente.EditValue == null)
                {
                    oBase = LkpTipoCliente;
                    throw new ArgumentException("Seleccionar Tipo de Cliente");

                }
                else
                {

                    if (Convert.ToInt32(LkpTipoCliente.EditValue) == 1)
                    {
                        if (Convert.ToInt32(LkpTipoDoc.EditValue) != 0 && txtDocumento.Text == "")
                        {
                            oBase = txtDocumento;
                            throw new ArgumentException("Ingresar Documento");
                        }
                    }
                    else if (Convert.ToInt32(LkpTipoCliente.EditValue) == 3)
                    {
                        if (Convert.ToInt32(LkpTipoDoc.EditValue) != 0 && txtDocumento.Text == "")
                        {
                            oBase = txtDocumento;
                            throw new ArgumentException("Ingresar Documento");
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(LkpTipoCliente.EditValue) == 2)
                        {
                            if (Convert.ToInt32(LkpTipoDoc.EditValue) == 3)
                            {
                                if (txtRUC.Text.Length == 0)
                                {
                                    oBase = txtRUC;
                                    throw new ArgumentException("Ingresar el RUC del Cliente");
                                }
                            }
                        }



                    }
                }




                if (LkpGiro.EditValue == null)
                {
                    oBase = LkpGiro;
                    throw new ArgumentException("Seleccionar Giro");
                }
                if (txtDocumento.Text.Length != 0)
                {
                    var documento = oDetail.Where(oB => oB.cliec_vnumero_doc_cli != null && oB.cliec_vnumero_doc_cli.ToUpper() == txtDocumento.Text.ToUpper() && oB.cliec_icod_cliente != Correlative).ToList();
                    if (documento.Count > 0)
                    {
                        if (Convert.ToInt32(LkpTipoDoc.EditValue) == 2)
                        {
                            oBase = txtDocumento;
                            throw new ArgumentException("El DNI ya existe");
                        }
                        else
                        {
                            oBase = txtDocumento;
                            throw new ArgumentException("El Carnet de extranjería ya existe");
                        }
                    }
                }
                var razon = oDetail.Where(oB => oB.cliec_vnombre_cliente.ToUpper() == txtRazonSocial.Text.ToUpper() && oB.cliec_icod_cliente != Correlative).ToList();
                if (razon.Count > 0)
                {
                    oBase = txtRazonSocial;
                    throw new ArgumentException("La Razón Social ya existe");
                }

                var ruc = oDetail.Where(oB => oB.cliec_cruc == txtRUC.Text && oB.cliec_icod_cliente != Correlative && oB.cliec_cruc != "").ToList();
                if (ruc.Count > 0)
                {
                    oBase = txtRUC;
                    throw new ArgumentException("El RUC ya existe");
                }

                if (string.IsNullOrEmpty(txtRazonSocial.Text))
                {
                    oBase = txtRazonSocial;
                    throw new ArgumentException("Ingresar Razón Social");
                }

                //if (LkpVendedor.EditValue == null)
                //{
                //    oBase = LkpVendedor;
                //    throw new ArgumentException("Seleccionar Vendedor");
                //}
                if (string.IsNullOrEmpty(txtDireccion.Text))
                {
                    oBase = txtDireccion;
                    throw new ArgumentException("Ingresar Dirección");
                }
                if (lkpMercado.EditValue == null)
                {
                    oBe.pcomc_icod_pcompra = null;
                }
                else
                {
                    oBe.pcomc_icod_pcompra = Convert.ToInt32(lkpMercado.EditValue);
                }
                oBe.giroc_icod_giro = Convert.ToInt32(LkpGiro.EditValue);
                oBe.cliec_vnombre_cliente = string.IsNullOrEmpty(txtRazonSocial.Text) ? null : txtRazonSocial.Text;
                oBe.cliec_vnombre_comercial = string.IsNullOrEmpty(txtComercial.Text) ? null : txtComercial.Text;
                oBe.cliec_vdireccion_cliente = string.IsNullOrEmpty(txtDireccion.Text) ? null : txtDireccion.Text;

                if (LkpDistrito.EditValue != null)
                    oBe.ubicc_icod_ubicacion = Convert.ToInt32(LkpDistrito.EditValue);
                if (LkpProvincia.EditValue != null & LkpDistrito.EditValue == null)
                    oBe.ubicc_icod_ubicacion = Convert.ToInt32(LkpProvincia.EditValue);
                if (LkpDepartamento.EditValue != null & LkpProvincia.EditValue == null)
                    oBe.ubicc_icod_ubicacion = Convert.ToInt32(LkpDepartamento.EditValue);
                if (LkpPais.EditValue != null & LkpDepartamento.EditValue == null)
                    oBe.ubicc_icod_ubicacion = Convert.ToInt32(LkpPais.EditValue);
                if (LkpPais.EditValue == null)
                    oBe.ubicc_icod_ubicacion = null;

                oBe.cliec_vnro_telefono = string.IsNullOrEmpty(txtTelefono.Text) ? null : txtTelefono.Text;
                oBe.cliec_vnro_fax = string.IsNullOrEmpty(txtFax.Text) ? null : txtFax.Text;
                oBe.cliec_vnro_celular = string.IsNullOrEmpty(txtCelular.Text) ? null : txtCelular.Text;

                if (LkpTipoDoc.EditValue == null)
                    oBe.tabl_iid_tipo_documento = null;
                else
                    oBe.tabl_iid_tipo_documento = Convert.ToInt32(LkpTipoDoc.EditValue);
                oBe.cliec_vnumero_doc_cli = string.IsNullOrEmpty(txtDocumento.Text) ? null : txtDocumento.Text;

                oBe.cliec_vnombre_contacto = string.IsNullOrEmpty(txtContacto.Text) ? null : txtContacto.Text;

                if (LkpEmpresaRelacionada.EditValue == null)
                    oBe.tablc_iid_tipo_relacion_cli = null;
                else
                    oBe.tablc_iid_tipo_relacion_cli = Convert.ToInt32(LkpEmpresaRelacionada.EditValue);

                oBe.vendc_icod_vendedor = Convert.ToInt32(LkpVendedor.EditValue);

                if (deFecha.EditValue == null)
                    oBe.cliec_sfecha_registro_cliente = null;
                else
                    oBe.cliec_sfecha_registro_cliente = Convert.ToDateTime(deFecha.EditValue);

                oBe.cliec_iid_situacion_cliente = Convert.ToInt32(LkpSituacion.EditValue);
                oBe.usuario = 1;
                oBe.pc = WindowsIdentity.GetCurrent().Name.ToString();

                oBe.cliec_vcorreo_electronico = string.IsNullOrEmpty(txtCorreo.Text) ? null : txtCorreo.Text;
                oBe.cliec_vapellido_paterno = string.IsNullOrEmpty(txtPaterno.Text) ? null : txtPaterno.Text;
                oBe.cliec_vapellido_materno = string.IsNullOrEmpty(txtMaterno.Text) ? null : txtMaterno.Text;
                oBe.cliec_vnombres = string.IsNullOrEmpty(txtNombre.Text) ? null : txtNombre.Text;

                if (LkpTipoCliente.EditValue == null)
                    oBe.tablc_iid_tipo_cliente = null;
                else
                    oBe.tablc_iid_tipo_cliente = Convert.ToInt32(LkpTipoCliente.EditValue);

                oBe.cliec_cruc = string.IsNullOrEmpty(txtRUC.Text) ? null : txtRUC.Text;
                oBe.cliec_vcod_cliente = txtCodigoCliente.Text;
                if (lkpMercado.EditValue == null)
                {
                    oBe.pcomc_icod_pcompra = null;
                }
                else
                {
                    oBe.pcomc_icod_pcompra = Convert.ToInt32(lkpMercado.EditValue);
                }
                oBe.cliec_bcredito = Convert.ToBoolean(cbCredito.Checked);
                oBe.cliec_nnumero_dias = Convert.ToInt32(txtnumerodias.Text);
                oBe.cliec_vubigeo = txtUbigeo.Text;
                oBe.cliec_bagente_retenedor = ckAgenteRetenedor.Checked;

                if (Status == BSMaintenanceStatus.CreateNew)
                {

                    Correlative = Obl.InsertarCliente(oBe);
                }
                else
                {

                    oBe.cliec_icod_cliente = Correlative;
                    oBe.anac_icod_analitica = anac_icod_analitica;
                    Obl.ActualizarCliente(oBe);
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
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    XtraMessageBox.Show("No se pudo realizar la operación", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Flag = false;

            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(Correlative);
                    this.Close();
                }
            }
        }

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => this.SetSave();

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => this.Close();

        private void LkpTipoCliente_EditValueChanged(object sender, EventArgs e)
        {
            if (habilitar != "Ver")
            {
                switch (Convert.ToInt32(LkpTipoCliente.EditValue))
                {
                    case 1:
                    case 3:
                        txtNombre.Enabled = Enabled;
                        txtPaterno.Enabled = Enabled;
                        txtMaterno.Enabled = Enabled;
                        break;
                    case 2:
                    case 4:
                        txtNombre.Enabled = !Enabled;
                        txtPaterno.Enabled = !Enabled;
                        txtMaterno.Enabled = !Enabled;
                        txtNombre.Text = "";
                        txtPaterno.Text = "";
                        txtMaterno.Text = "";
                        break;
                }
            }

        }

        private void LkpPais_EditValueChanged(object sender, EventArgs e)
        {
            BSControls.LoaderLook(LkpDepartamento, lUbicacion.Where(obj => obj.ubicc_icod_ubicacion_padre == Convert.ToInt32(LkpPais.EditValue)).ToList(), "ubicc_vnombre_ubicacion", "ubicc_icod_ubicacion", false);
        }

        private void LkpDepartamento_EditValueChanged(object sender, EventArgs e)
        {
            BSControls.LoaderLook(LkpProvincia, lUbicacion.Where(obj => obj.ubicc_icod_ubicacion_padre == Convert.ToInt32(LkpDepartamento.EditValue)).ToList(), "ubicc_vnombre_ubicacion", "ubicc_icod_ubicacion", false);
        }

        private void LkpProvincia_EditValueChanged(object sender, EventArgs e)
        {
            BSControls.LoaderLook(LkpDistrito, lUbicacion.Where(obj => obj.ubicc_icod_ubicacion_padre == Convert.ToInt32(LkpProvincia.EditValue)).ToList(), "ubicc_vnombre_ubicacion", "ubicc_icod_ubicacion", false);
        }

        private void LkpTipoDoc_EditValueChanged(object sender, EventArgs e)
        {
            txtDocumento.Text = "";
            switch (LkpTipoDoc.EditValue == null ? 0 : Convert.ToInt32(LkpTipoDoc.EditValue))
            {
                case (int)TipoDocumentoPersonal.Dni:
                    txtDocumento.Properties.MaxLength = 8;
                    txtDocumento.Enabled = true;
                    break;
                case (int)TipoDocumentoPersonal.PartidaDeNacimiento:
                case (int)TipoDocumentoPersonal.Otros:
                    txtDocumento.Properties.MaxLength = 15;
                    txtDocumento.Enabled = true;
                    break;
                case (int)TipoDocumentoPersonal.Pasaporte:
                case (int)TipoDocumentoPersonal.CarnetDeExtrangería:
                    txtDocumento.Properties.MaxLength = 12;
                    txtDocumento.Enabled = true;
                    break;
            }
        }

        private void txtNombre_Leave(object sender, EventArgs e) => txtRazonSocial.Text = txtNombre.Text;
        private void txtPaterno_Leave(object sender, EventArgs e) => txtRazonSocial.Text = txtRazonSocial.Text + " " + txtPaterno.Text;
        private void txtMaterno_Leave(object sender, EventArgs e) => txtRazonSocial.Text = txtRazonSocial.Text + " " + txtMaterno.Text;
        private void LkpDistrito_EditValueChanged(object sender, EventArgs e) { }

        private void cbCredito_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCredito.Checked == true)
            {
                txtnumerodias.Enabled = true;
                txtnumerodias.Text = "";
            }
            else
            {
                txtnumerodias.Enabled = false;
                txtnumerodias.Text = "";
            }
        }

        private void txtnumerodias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtDocumento_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(LkpTipoDoc.EditValue) == (int)TipoDocumentoPersonal.Dni)
                if (txtDocumento.Text.Trim().Length == 8 && txtRUC.ContainsFocus)
                    ConsutaDNI();
        }

        private void txtRUC_EditValueChanged(object sender, EventArgs e)
        {
            if (txtRUC.Text.Trim().Length == 11 && txtRUC.ContainsFocus)
                ConsultaRuc();
        }

        public void ConsutaDNI()
        {
            try
            {

                var resultado = ConsultaRucApi.ConsutaDNI(txtDocumento.Text);

                string[] nombres = resultado.nombre_completo.Split(' ');

                lblNombre.Text = resultado.nombre_completo;
                txtMaterno.Text = nombres[1].Trim();
                txtPaterno.Text = nombres[0].Trim();
                txtRazonSocial.Text = "";
                txtNombre.Text = "";
                for (int i = 0; i < nombres.Count(); i++)
                {
                    txtRazonSocial.Text = txtRazonSocial.Text + nombres[i].Trim() + " ";
                    if (i >= 2)
                    {
                        txtNombre.Text = txtNombre.Text + nombres[i].Trim() + " ";

                    }
                };
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Por favor, ingrese número de DNI válido", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConsultaRuc()
        {

            try
            {
                var resultado = ConsultaRucApi.ConsultaRuc(txtRUC.Text);
                txtRazonSocial.Text = resultado.nombre_o_razon_social;
                txtDireccion.Text = resultado.direccion_completa;
                txtRUC.Text = resultado.ruc;
                txtUbigeo.Text = resultado.ubigeo;
                if (resultado.distrito != "-")
                {
                    var ubicacion = lUbicacion.Where(x => x.ubicc_vnombre_ubicacion.ToUpper() == resultado.distrito.ToUpper()).FirstOrDefault();
                    setUbicacion(ubicacion.ubicc_icod_ubicacion);
                }

                else if (resultado.provincia != "-")
                {
                    var ubicacion = lUbicacion.Where(x => x.ubicc_vnombre_ubicacion.ToUpper() == resultado.provincia.ToUpper()).FirstOrDefault();
                    setUbicacion(ubicacion.ubicc_icod_ubicacion);
                }

                else if (resultado.departamento != "-")
                {
                    var ubicacion = lUbicacion.Where(x => x.ubicc_vnombre_ubicacion.ToUpper() == resultado.departamento.ToUpper()).FirstOrDefault();
                    setUbicacion(ubicacion.ubicc_icod_ubicacion);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Por favor, ingrese número de RUC válido", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void setUbicacion(int codigo)
        {
            List<EUbicacion> lUbicacion = new BVentas().ListarUbicacion();
            int? tipo = null;
            int? padre = null;
            int? abuelo = null;
            int? bisabuelo = null;
            lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == codigo).ToList().ForEach(oB =>
            {
                tipo = oB.tablc_iid_tipo_ubicacion;
                padre = oB.ubicc_icod_ubicacion_padre;
            });
            switch (tipo)
            {
                case 4:
                    LkpPais.EditValue = codigo;
                    break;
                case 3:
                    LkpPais.EditValue = padre;
                    LkpDepartamento.EditValue = codigo;
                    break;
                case 2:
                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == padre).ToList().ForEach(oB =>
                    {
                        abuelo = oB.ubicc_icod_ubicacion_padre;
                    });
                    LkpPais.EditValue = abuelo;
                    LkpDepartamento.EditValue = padre;
                    LkpProvincia.EditValue = codigo;
                    break;
                case 1:
                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == padre).ToList().ForEach(oB =>
                    {
                        abuelo = oB.ubicc_icod_ubicacion_padre;
                    });

                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == abuelo).ToList().ForEach(oB =>
                    {
                        bisabuelo = oB.ubicc_icod_ubicacion_padre;
                    });

                    LkpPais.EditValue = bisabuelo;
                    LkpDepartamento.EditValue = abuelo;
                    LkpProvincia.EditValue = padre;
                    LkpDistrito.EditValue = codigo;
                    break;
            }
        }




        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}