using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Tesoreria.Ventas;
using SGE.WindowForms.Otros.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class Frm03Cliente : DevExpress.XtraEditors.XtraForm
    {
        private List<ECliente> lstCliente = new List<ECliente>();
        private int xposition = 0;

        public Frm03Cliente()
        {
            InitializeComponent();

        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            this.CargarControles();
            this.cargar();
        }
        public void CargarControles()
        {

            var lstEstado = new BGeneral().listarTablaRegistro(Parametros.intTipoTablaEstado);
            ETablaRegistro entidad = new ETablaRegistro();
            entidad.tarec_icorrelativo_registro = 0;
            entidad.tarec_vdescripcion = "Todos";
            lstEstado.Insert(0, entidad);

            BSControls.LoaderLook(LkpSituacion, lstEstado, "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            //BSControls.LoaderLook(LkpVendedor, new BVendedor().ListarVendedorFiltro(), "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);
            BSControls.LoaderLook(LkpGiro, new BVentas().ListarGiroFiltro(), "giroc_vnombre_giro", "giroc_icod_giro", true);
        }
        public void cargar()
        {
            lstCliente = new BVentas().ListarCliente();
            dgrCliente.DataSource = lstCliente;

        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstCliente.FindIndex(x => x.cliec_icod_cliente == intIcod);
            viewCliente.FocusedRowHandle = index;
            viewCliente.Focus();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteCliente Cliente = new FrmManteCliente();
            Cliente.MiEvento += new FrmManteCliente.DelegadoMensaje(reload);
            //viewCliente.MoveLast();
            //ECliente Obe = (ECliente)viewCliente.GetRow(viewCliente.FocusedRowHandle);
            int i = 0;
            if (lstCliente.Count > 0)
                i = lstCliente.Max(ob => ob.cliec_icod_cliente);

            Cliente.Correlative = Convert.ToInt32(i) + 1;
            Cliente.oDetail = lstCliente;
            Cliente.Show();
            Cliente.SetInsert();
            Cliente.deFecha.EditValue = DateTime.Today;
            Cliente.txtDocumento.Enabled = false;
            Cliente.cbCredito.Checked = false;

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            datos();


        }
        private void datos()
        {
            ECliente Obe = (ECliente)viewCliente.GetRow(viewCliente.FocusedRowHandle);
            if (Obe == null)
                return;
            FrmManteCliente Cliente = new FrmManteCliente();
            Cliente.MiEvento += new FrmManteCliente.DelegadoMensaje(reload);
            Cliente.oDetail = lstCliente;
            Cliente.Show();

            Cliente.Correlative = Obe.cliec_icod_cliente;
            Cliente.txtCodigoCliente.Text = Obe.cliec_vcod_cliente;
            Cliente.txtCelular.Text = Obe.cliec_icod_cliente.ToString();
            Cliente.LkpGiro.EditValue = Obe.giroc_icod_giro;
            Cliente.txtRazonSocial.Text = Obe.cliec_vnombre_cliente;
            Cliente.txtComercial.Text = Obe.cliec_vnombre_comercial;
            Cliente.txtDireccion.Text = Obe.cliec_vdireccion_cliente;
            Cliente.LkpPais.EditValue = Obe.ubicc_icod_ubicacion;
            Cliente.txtTelefono.Text = Obe.cliec_vnro_telefono;
            Cliente.txtFax.Text = Obe.cliec_vnro_fax;
            Cliente.txtCelular.Text = Obe.cliec_vnro_celular;
            Cliente.txtCorreo.Text = Obe.cliec_vcorreo_electronico;
            Cliente.LkpTipoDoc.EditValue = Obe.tabl_iid_tipo_documento;
            Cliente.txtDocumento.Text = Obe.cliec_vnumero_doc_cli;
            Cliente.txtContacto.Text = Obe.cliec_vnombre_contacto;
            Cliente.LkpEmpresaRelacionada.EditValue = Obe.tablc_iid_tipo_relacion_cli;
            Cliente.LkpVendedor.EditValue = Obe.vendc_icod_vendedor;
            Cliente.deFecha.EditValue = Obe.cliec_sfecha_registro_cliente;
            Cliente.LkpSituacion.EditValue = Obe.cliec_iid_situacion_cliente;
            Cliente.cbCredito.Checked = Convert.ToBoolean(Obe.cliec_bcredito);
            Cliente.txtnumerodias.Text = Obe.cliec_nnumero_dias.ToString();
            Cliente.txtUbigeo.Text = Obe.cliec_vubigeo;
            Cliente.ckAgenteRetenedor.Checked = Obe.cliec_bagente_retenedor;
            List<EUbicacion> lUbicacion = new BVentas().ListarUbicacion();
            int? tipo = null;
            int? padre = null;
            int? abuelo = null;
            int? bisabuelo = null;
            lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == Obe.ubicc_icod_ubicacion).ToList().ForEach(oB =>
            {
                tipo = oB.tablc_iid_tipo_ubicacion;
                padre = oB.ubicc_icod_ubicacion_padre;
            });
            switch (tipo)
            {
                case 4:
                    Cliente.LkpPais.EditValue = Obe.ubicc_icod_ubicacion;
                    break;
                case 3:
                    Cliente.LkpPais.EditValue = padre;
                    Cliente.LkpDepartamento.EditValue = Obe.ubicc_icod_ubicacion;
                    break;
                case 2:
                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == padre).ToList().ForEach(oB =>
                    {
                        abuelo = oB.ubicc_icod_ubicacion_padre;
                    });
                    Cliente.LkpPais.EditValue = abuelo;
                    Cliente.LkpDepartamento.EditValue = padre;
                    Cliente.LkpProvincia.EditValue = Obe.ubicc_icod_ubicacion;
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

                    Cliente.LkpPais.EditValue = bisabuelo;
                    Cliente.LkpDepartamento.EditValue = abuelo;
                    Cliente.LkpProvincia.EditValue = padre;
                    Cliente.LkpDistrito.EditValue = Obe.ubicc_icod_ubicacion;
                    break;

            }

            Cliente.txtComercial.Text = Obe.cliec_vcorreo_electronico;
            Cliente.txtPaterno.Text = Obe.cliec_vapellido_paterno;
            Cliente.txtMaterno.Text = Obe.cliec_vapellido_materno;
            Cliente.txtNombre.Text = Obe.cliec_vnombres;
            Cliente.LkpTipoCliente.EditValue = Obe.tablc_iid_tipo_cliente;
            Cliente.txtRUC.Text = Obe.cliec_cruc;
            Cliente.anac_icod_analitica = Obe.anac_icod_analitica;
            Cliente.SetModify();
            xposition = viewCliente.FocusedRowHandle;

        }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstCliente.Count > 0)
            {
                ECliente Obe = (ECliente)viewCliente.GetRow(viewCliente.FocusedRowHandle);
                BVentas obl = new BVentas();
                if (XtraMessageBox.Show("Esta seguro de Eliminar", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    obl.EliminarCliente(Obe.cliec_icod_cliente, 1, WindowsIdentity.GetCurrent().Name.ToString(), Convert.ToInt32(Obe.anac_icod_analitica));
                    viewCliente.DeleteRow(viewCliente.FocusedRowHandle);
                }
            }
        }




        private void viewCliente_DoubleClick(object sender, EventArgs e)
        {
            if (lstCliente.Count > 0)
            {
                FrmManteCliente Cliente = new FrmManteCliente();
                Cliente.MiEvento += new FrmManteCliente.DelegadoMensaje(reload);
                Cliente.Show();
                Cliente.SetCancel();
                Cliente.habilitar = "Ver";
                ECliente Obe = (ECliente)viewCliente.GetRow(viewCliente.FocusedRowHandle);
                Cliente.Correlative = Obe.cliec_icod_cliente;
                Cliente.txtCodigoCliente.Text = Obe.cliec_vcod_cliente;
                Cliente.txtCelular.Text = Obe.cliec_icod_cliente.ToString();
                Cliente.LkpGiro.EditValue = Obe.giroc_icod_giro;
                Cliente.txtRazonSocial.Text = Obe.cliec_vnombre_cliente;
                Cliente.txtComercial.Text = Obe.cliec_vnombre_comercial;
                Cliente.txtDireccion.Text = Obe.cliec_vdireccion_cliente;
                Cliente.txtCorreo.Text = Obe.cliec_vcorreo_electronico;
                Cliente.txtTelefono.Text = Obe.cliec_vnro_telefono;
                Cliente.txtFax.Text = Obe.cliec_vnro_fax;
                Cliente.txtCelular.Text = Obe.cliec_vnro_celular;
                Cliente.LkpTipoDoc.EditValue = Obe.tabl_iid_tipo_documento;
                Cliente.txtDocumento.Text = Obe.cliec_vnumero_doc_cli;
                Cliente.txtContacto.Text = Obe.cliec_vnombre_contacto;
                Cliente.LkpEmpresaRelacionada.EditValue = Obe.tablc_iid_tipo_relacion_cli;
                Cliente.LkpVendedor.EditValue = Obe.vendc_icod_vendedor;
                Cliente.deFecha.EditValue = Obe.cliec_sfecha_registro_cliente;
                Cliente.LkpSituacion.EditValue = Obe.cliec_iid_situacion_cliente;

                List<EUbicacion> lUbicacion = new BVentas().ListarUbicacion();
                int? tipo = null;
                int? padre = null;
                int? abuelo = null;
                int? bisabuelo = null;
                lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == Obe.ubicc_icod_ubicacion).ToList().ForEach(oB =>
                {
                    tipo = oB.tablc_iid_tipo_ubicacion;
                    padre = oB.ubicc_icod_ubicacion_padre;
                });
                switch (tipo)
                {
                    case 4:
                        Cliente.LkpPais.EditValue = Obe.ubicc_icod_ubicacion;
                        break;
                    case 3:
                        Cliente.LkpPais.EditValue = padre;
                        Cliente.LkpDepartamento.EditValue = Obe.ubicc_icod_ubicacion;
                        break;
                    case 2:
                        lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == padre).ToList().ForEach(oB =>
                        {
                            abuelo = oB.ubicc_icod_ubicacion_padre;
                        });
                        Cliente.LkpPais.EditValue = abuelo;
                        Cliente.LkpDepartamento.EditValue = padre;
                        Cliente.LkpProvincia.EditValue = Obe.ubicc_icod_ubicacion;
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

                        Cliente.LkpPais.EditValue = bisabuelo;
                        Cliente.LkpDepartamento.EditValue = abuelo;
                        Cliente.LkpProvincia.EditValue = padre;
                        Cliente.LkpDistrito.EditValue = Obe.ubicc_icod_ubicacion;
                        break;
                }


                Cliente.txtComercial.Text = Obe.cliec_vcorreo_electronico;
                Cliente.txtPaterno.Text = Obe.cliec_vapellido_paterno;
                Cliente.txtMaterno.Text = Obe.cliec_vapellido_materno;
                Cliente.txtNombre.Text = Obe.cliec_vnombres;
                Cliente.LkpTipoCliente.EditValue = Obe.tablc_iid_tipo_cliente;
                Cliente.txtRUC.Text = Obe.cliec_cruc;

                Cliente.BtnGuardar.Enabled = false;
            }
            this.viewCliente.DoubleClick -= new System.EventHandler(this.viewCliente_DoubleClick);
        }

        private void txtCodigo_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void BuscarCriterio()
        {
            if (Convert.ToInt32(LkpSituacion.EditValue) == 0)
            {
                if (Convert.ToInt32(LkpGiro.EditValue) == 0)
                {
                    if (Convert.ToInt32(LkpVendedor.EditValue) == 0)
                    {

                        if (Convert.ToInt32(radioGroup1.EditValue) == 0)
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.cliec_vnumero_doc_cli.Contains((string.IsNullOrEmpty(null) ? obj.cliec_vnumero_doc_cli : "")) && obj.cliec_cruc.Contains((string.IsNullOrEmpty(null) ? obj.cliec_cruc : "")) && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }
                        else if (Convert.ToInt32(radioGroup1.EditValue) == 1)
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.cliec_vnumero_doc_cli != "" && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }
                        else
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.cliec_cruc != "" && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(radioGroup1.EditValue) == 0)
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.vendc_icod_vendedor == Convert.ToInt32(LkpVendedor.EditValue) && obj.cliec_vnumero_doc_cli.Contains((string.IsNullOrEmpty(null) ? obj.cliec_vnumero_doc_cli : "")) && obj.cliec_cruc.Contains((string.IsNullOrEmpty(null) ? obj.cliec_cruc : "")) && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }
                        else if (Convert.ToInt32(radioGroup1.EditValue) == 1)
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.vendc_icod_vendedor == Convert.ToInt32(LkpVendedor.EditValue) && obj.cliec_vnumero_doc_cli != "" && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }
                        else
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.vendc_icod_vendedor == Convert.ToInt32(LkpVendedor.EditValue) && obj.cliec_cruc != "" && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }
                    }
                }
                else
                {
                    if (Convert.ToInt32(LkpVendedor.EditValue) == 0)
                    {
                        if (Convert.ToInt32(radioGroup1.EditValue) == 0)
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.giroc_icod_giro == Convert.ToInt32(LkpGiro.EditValue) && obj.cliec_vnumero_doc_cli.Contains((string.IsNullOrEmpty(null) ? obj.cliec_vnumero_doc_cli : "")) && obj.cliec_cruc.Contains((string.IsNullOrEmpty(null) ? obj.cliec_cruc : "")) && obj.cliec_iid_situacion_cliente == 1 && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }
                        else if (Convert.ToInt32(radioGroup1.EditValue) == 1)
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.giroc_icod_giro == Convert.ToInt32(LkpGiro.EditValue) && obj.cliec_vnumero_doc_cli != "" && obj.cliec_iid_situacion_cliente == 1 && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }
                        else
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.giroc_icod_giro == Convert.ToInt32(LkpGiro.EditValue) && obj.cliec_cruc != "" && obj.cliec_iid_situacion_cliente == 1 && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }

                    }
                    else
                    {
                        if (Convert.ToInt32(radioGroup1.EditValue) == 0)
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.giroc_icod_giro == Convert.ToInt32(LkpGiro.EditValue) && obj.vendc_icod_vendedor == Convert.ToInt32(LkpVendedor.EditValue) && obj.cliec_vnumero_doc_cli.Contains((string.IsNullOrEmpty(null) ? obj.cliec_vnumero_doc_cli : "")) && obj.cliec_cruc.Contains((string.IsNullOrEmpty(null) ? obj.cliec_cruc : "")) && obj.cliec_iid_situacion_cliente == 1 && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();

                        }
                        else if (Convert.ToInt32(radioGroup1.EditValue) == 1)
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.giroc_icod_giro == Convert.ToInt32(LkpGiro.EditValue) && obj.vendc_icod_vendedor == Convert.ToInt32(LkpVendedor.EditValue) && obj.cliec_vnumero_doc_cli != "" && obj.cliec_iid_situacion_cliente == 1 && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }
                        else
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.giroc_icod_giro == Convert.ToInt32(LkpGiro.EditValue) && obj.vendc_icod_vendedor == Convert.ToInt32(LkpVendedor.EditValue) && obj.cliec_cruc != "" && obj.cliec_iid_situacion_cliente == 1 && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }
                    }
                }
            }
            else
            {
                if (Convert.ToInt32(LkpGiro.EditValue) == 0)
                {
                    if (Convert.ToInt32(LkpVendedor.EditValue) == 0)
                    {
                        if (Convert.ToInt32(radioGroup1.EditValue) == 0)
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.cliec_iid_situacion_cliente == Convert.ToInt32(LkpSituacion.EditValue) && obj.cliec_vnumero_doc_cli.Contains((string.IsNullOrEmpty(null) ? obj.cliec_vnumero_doc_cli : "")) && obj.cliec_cruc.Contains((string.IsNullOrEmpty(null) ? obj.cliec_cruc : "")) && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }
                        else if (Convert.ToInt32(radioGroup1.EditValue) == 1)
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.cliec_iid_situacion_cliente == Convert.ToInt32(LkpSituacion.EditValue) && obj.cliec_vnumero_doc_cli != "" && obj.cliec_iid_situacion_cliente == 1 && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }
                        else
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.cliec_iid_situacion_cliente == Convert.ToInt32(LkpSituacion.EditValue) && obj.cliec_cruc != "" && obj.cliec_iid_situacion_cliente == 1 && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(radioGroup1.EditValue) == 0)
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.cliec_iid_situacion_cliente == Convert.ToInt32(LkpSituacion.EditValue) && obj.vendc_icod_vendedor == Convert.ToInt32(LkpVendedor.EditValue) && obj.cliec_vnumero_doc_cli.Contains((string.IsNullOrEmpty(null) ? obj.cliec_vnumero_doc_cli : "")) && obj.cliec_cruc.Contains((string.IsNullOrEmpty(null) ? obj.cliec_cruc : "")) && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }
                        else if (Convert.ToInt32(radioGroup1.EditValue) == 1)
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.cliec_iid_situacion_cliente == Convert.ToInt32(LkpSituacion.EditValue) && obj.vendc_icod_vendedor == Convert.ToInt32(LkpVendedor.EditValue) && obj.cliec_vnumero_doc_cli != "" && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }
                        else
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.cliec_iid_situacion_cliente == Convert.ToInt32(LkpSituacion.EditValue) && obj.vendc_icod_vendedor == Convert.ToInt32(LkpVendedor.EditValue) && obj.cliec_cruc != "" && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }
                    }
                }
                else
                {
                    if (Convert.ToInt32(LkpVendedor.EditValue) == 0)
                    {
                        if (Convert.ToInt32(radioGroup1.EditValue) == 0)
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.cliec_iid_situacion_cliente == Convert.ToInt32(LkpSituacion.EditValue) && obj.giroc_icod_giro == Convert.ToInt32(LkpGiro.EditValue) && obj.cliec_vnumero_doc_cli.Contains((string.IsNullOrEmpty(null) ? obj.cliec_vnumero_doc_cli : "")) && obj.cliec_cruc.Contains((string.IsNullOrEmpty(null) ? obj.cliec_cruc : "")) && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();

                        }
                        else if (Convert.ToInt32(radioGroup1.EditValue) == 1)
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.cliec_iid_situacion_cliente == Convert.ToInt32(LkpSituacion.EditValue) && obj.giroc_icod_giro == Convert.ToInt32(LkpGiro.EditValue) && obj.cliec_vnumero_doc_cli != "" && obj.cliec_iid_situacion_cliente == 1 && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }
                        else
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.cliec_iid_situacion_cliente == Convert.ToInt32(LkpSituacion.EditValue) && obj.giroc_icod_giro == Convert.ToInt32(LkpGiro.EditValue) && obj.cliec_cruc != "" && obj.cliec_iid_situacion_cliente == 1 && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }

                    }
                    else
                    {
                        if (Convert.ToInt32(radioGroup1.EditValue) == 0)
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.cliec_iid_situacion_cliente == Convert.ToInt32(LkpSituacion.EditValue) && obj.giroc_icod_giro == Convert.ToInt32(LkpGiro.EditValue) && obj.vendc_icod_vendedor == Convert.ToInt32(LkpVendedor.EditValue) && obj.cliec_vnumero_doc_cli.Contains((string.IsNullOrEmpty(null) ? obj.cliec_vnumero_doc_cli : "")) && obj.cliec_cruc.Contains((string.IsNullOrEmpty(null) ? obj.cliec_cruc : "")) && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }
                        else if (Convert.ToInt32(radioGroup1.EditValue) == 1)
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.cliec_iid_situacion_cliente == Convert.ToInt32(LkpSituacion.EditValue) && obj.giroc_icod_giro == Convert.ToInt32(LkpGiro.EditValue) && obj.vendc_icod_vendedor == Convert.ToInt32(LkpVendedor.EditValue) && obj.cliec_vnumero_doc_cli != "" && obj.cliec_iid_situacion_cliente == 1 && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }
                        else
                        {
                            dgrCliente.DataSource = lstCliente.Where(obj => obj.cliec_iid_situacion_cliente == Convert.ToInt32(LkpSituacion.EditValue) && obj.giroc_icod_giro == Convert.ToInt32(LkpGiro.EditValue) && obj.vendc_icod_vendedor == Convert.ToInt32(LkpVendedor.EditValue) && obj.cliec_cruc != "" && obj.cliec_iid_situacion_cliente == 1 && obj.cliec_vcod_cliente.ToUpper().Contains(txtCodigo.Text.ToUpper()) && obj.cliec_vnombre_cliente.ToUpper().Contains(txtRazonSocial.Text.ToUpper())).ToList();
                        }

                    }
                }
            }


        }

        private void viewCliente_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
                nuevoToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F5)
                modificarToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F9)
                eliminarToolStripMenuItem_Click(null, null);
        }

        private void LkpVendedor_EditValueChanged(object sender, EventArgs e)
        {
            BuscarCriterio();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ECliente> listado = (List<ECliente>)dgrCliente.DataSource;

            if (listado.Count > 0)
            {
                rptCliente rpt = new rptCliente();
                rpt.cargar(listado, Parametros.intEjercicio.ToString(), "", LkpSituacion.Text, LkpGiro.Text, LkpVendedor.Text);
            }
            else
                XtraMessageBox.Show("No hay registro por Reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuscarCriterio();
        }

        private void txtCodigo_KeyUp_1(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void direccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ECliente Obe = (ECliente)viewCliente.GetRow(viewCliente.FocusedRowHandle);
            if (Obe == null)
                return;
            FrmListarDirecionCliente Cliente = new FrmListarDirecionCliente();
            Cliente.cliec_icod_cliente = Obe.cliec_icod_cliente;
            Cliente.Text = String.Format("Lista de Direcciones {0}", Obe.cliec_vnombre_cliente);
            Cliente.Show();
        }
    }
}