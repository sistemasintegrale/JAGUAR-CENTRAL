using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Otros.Tesoreria.Ventas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmListarCliente : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        private List<ECliente> lstCliente = new List<ECliente>();
        public ECliente _Be { get; set; }

        public FrmListarCliente()
        {
            InitializeComponent();
        }


        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void DgAcept()
        {
            _Be = (ECliente)viewCliente.GetRow(viewCliente.FocusedRowHandle);
            if (_Be != null)
                this.DialogResult = DialogResult.OK;
        }

        private void BuscarCriterio()
        {
            string cod = txtCodigo.Text, desc = txtRazon.Text;
            grd.DataSource = lstCliente.Where(obe => ((cod != string.Empty) ? obe.cliec_vcod_cliente.ToUpper().Contains(cod.ToUpper()) : obe.cliec_vnombre_cliente.ToUpper().Contains(desc.ToUpper()))).ToList();
        }

        private void FrmListarCliente_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstCliente = new BVentas().ListarCliente();
            grd.DataSource = lstCliente.Where(obj => obj.cliec_iid_situacion_cliente == 1).ToList();
            viewCliente.Focus();
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstCliente.FindIndex(x => x.cliec_icod_cliente == intIcod);
            viewCliente.FocusedRowHandle = index;
            viewCliente.Focus();
        }

        private void btnsalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DgAcept();
        }

        private void btnPrev_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (viewCliente.FocusedRowHandle == 0)
                viewCliente.MoveLast();
            else
                viewCliente.MovePrev();
        }

        private void btnNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (viewCliente.FocusedRowHandle == viewCliente.RowCount - 1)
                viewCliente.MoveFirst();
            else
                viewCliente.MoveNext();
        }

        private void txtCodigo_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCodigo.ContainsFocus)
            {
                txtRazon.Text = string.Empty;
                BuscarCriterio();
            }
        }

        private void txtRazon_EditValueChanged(object sender, EventArgs e)
        {
            if (txtRazon.ContainsFocus)
            {
                txtCodigo.Text = string.Empty;
                BuscarCriterio();
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteCliente Cliente = new FrmManteCliente();
            Cliente.MiEvento += new FrmManteCliente.DelegadoMensaje(reload);
            int i = 0;
            if (lstCliente.Count > 0)
                i = lstCliente.Max(ob => ob.cliec_icod_cliente);

            Cliente.Correlative = Convert.ToInt32(i) + 1;
            Cliente.oDetail = lstCliente;
            Cliente.Show();
            Cliente.SetInsert();
            Cliente.deFecha.EditValue = DateTime.Today;
            Cliente.txtDocumento.Enabled = false;
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstCliente.Count > 0)
            {
                datos();

            }
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

    }
}