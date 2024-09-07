using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmListarProveedor : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        private List<EProveedor> lstProveedores = new List<EProveedor>();
        public EProveedor _Be { get; set; }
        public bool bNoExceptuadosRetencion = false;

        #endregion

        #region "Eventos"

        public FrmListarProveedor()
        {
            InitializeComponent();
        }

        private void FrmListarProveedor_Load(object sender, EventArgs e)
        {
            if (bNoExceptuadosRetencion)
                CargaNoExceptuadosRetencion();
            else
                Carga();

            txtRUC.Focus();
        }

        #endregion

        #region "Metodos"

        public void Carga()
        {
            lstProveedores = new BCompras().ListarProveedor();
            grdProveedor.DataSource = lstProveedores;
        }

        public void CargaNoExceptuadosRetencion()
        {
            //mlist = new BProveedoresExceptuadosRetencion().ListarProveedoresNoExcetuadosRetencion();
            grdProveedor.DataSource = lstProveedores;
        }

        private void BuscarCriterio()
        {
            string cod = txtRUC.Text, desc = txtRazonSocial.Text;
            grdProveedor.DataSource = lstProveedores.Where(obe => ((cod != string.Empty) ? obe.vcod_proveedor.Contains(cod) : obe.vnombrecompleto.Contains(desc))).ToList();

        }

        private void viewProveedor_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void DgAcept()
        {
            _Be = (EProveedor)viewProveedor.GetRow(viewProveedor.FocusedRowHandle);
            this.DialogResult = DialogResult.OK;
        }

        #endregion

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DgAcept();
        }


        private void txtRUC_EditValueChanged(object sender, EventArgs e)
        {
            if (txtRUC.ContainsFocus)
            {
                txtRazonSocial.Text = string.Empty;
                BuscarCriterio();
            }
        }

        private void txtRazonSocial_EditValueChanged(object sender, EventArgs e)
        {
            if (txtRazonSocial.ContainsFocus)
            {
                txtRUC.Text = string.Empty;
                BuscarCriterio();
            }
        }

        void reload(int intIcod)
        {
            Carga();
            grdProveedor.DataSource = lstProveedores;
            int index = lstProveedores.FindIndex(x => x.iid_icod_proveedor == intIcod);
            viewProveedor.FocusedRowHandle = index;
            viewProveedor.Focus();
        }


        private void nuevo()
        {
            FrmManteProveedor frm = new FrmManteProveedor();
            frm.MiEvento += new FrmManteProveedor.DelegadoMensaje(reload);
            viewProveedor.MoveLast();
            EProveedor Obe = (EProveedor)viewProveedor.GetRow(viewProveedor.FocusedRowHandle);
            int i = 0;
            if (lstProveedores.Count > 0)
                i = lstProveedores.Max(ob => ob.iid_proveedor);

            frm.Correlative = Convert.ToInt32(i) + 1;
            frm.oDetail = lstProveedores;
            frm.Show();
            frm.txtcodigo.Enabled = true;
            frm.SetInsert();
        }

        private void modificar()
        {
            try
            {
                EProveedor Obe = (EProveedor)viewProveedor.GetRow(viewProveedor.FocusedRowHandle);
                if (Obe == null)
                    return;
                FrmManteProveedor frm = new FrmManteProveedor();
                frm.MiEvento += new FrmManteProveedor.DelegadoMensaje(reload);
                frm.oDetail = lstProveedores;
                frm.Show();

                frm.Correlative = Obe.iid_icod_proveedor;
                frm.txtcodigo.Text = Obe.vcod_proveedor;
                frm.txtNombre.Text = (Obe.iid_tipo_persona == 1 || Obe.iid_tipo_persona == 3) ? Obe.vnombre : "";
                frm.txtPaterno.Text = (Obe.iid_tipo_persona == 1 || Obe.iid_tipo_persona == 3) ? Obe.vpaterno : "";
                frm.txtMaterno.Text = (Obe.iid_tipo_persona == 1 || Obe.iid_tipo_persona == 3) ? Obe.vmaterno : "";
                frm.txtRazonSocial.Text = (Obe.iid_tipo_persona == 1) ? Obe.vnombre + " " + Obe.vpaterno + " " + Obe.vmaterno : Obe.vnombrecompleto;
                frm.txtComercial.Text = Obe.vcomercial;
                //Proveedor.LkpTipoPersona.EditValue = Obe.iid_tipo_persona;
                frm.dtFecha.EditValue = Obe.proc_sfecha;
                if (Convert.ToInt32(Obe.proc_tipo_doc) == 0)
                    frm.lkpTipDoc.ItemIndex = 2;
                else
                    frm.lkpTipDoc.EditValue = Obe.proc_tipo_doc;
                frm.txtcorreo.Text = Obe.proc_vcorreo;
                frm.txtCtaBancoNacion.Text = Obe.proc_vcta_bco_nacion;
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
                        frm.LkpPais.EditValue = Obe.ubicc_icod_ubicacion;
                        break;
                    case 3:
                        frm.LkpPais.EditValue = padre;
                        frm.LkpDepartamento.EditValue = Obe.ubicc_icod_ubicacion;
                        break;
                    case 2:
                        lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == padre).ToList().ForEach(oB =>
                        {
                            abuelo = oB.ubicc_icod_ubicacion_padre;
                        });
                        frm.LkpPais.EditValue = abuelo;
                        frm.LkpDepartamento.EditValue = padre;
                        frm.LkpProvincia.EditValue = Obe.ubicc_icod_ubicacion;
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

                        frm.LkpPais.EditValue = bisabuelo;
                        frm.LkpDepartamento.EditValue = abuelo;
                        frm.LkpProvincia.EditValue = padre;
                        frm.LkpDistrito.EditValue = Obe.ubicc_icod_ubicacion;
                        break;
                }

                frm.txtdireccion.Text = Obe.vdireccion;
                frm.txtdni.Text = Obe.vdni;
                frm.txttelefono.Text = Obe.vtelefono;
                frm.txtfax.Text = Obe.vfax;
                frm.LkpEstado.EditValue = Obe.isituacion;
                frm.txtrepresentante.Text = Obe.vrepresentante;
                frm.txtruc.Text = Obe.vruc;
                frm.LkpTipoPersona.EditValue = Obe.iid_tipo_persona;
                frm.txtcodigo.Enabled = false;
                frm.anac_icod_analitica = Obe.anac_icod_analitica;
                frm.SetModify();
                if (frm.lkpTipDoc.EditValue == null)
                {
                    frm.txtdni.Enabled = false;
                }
                else if (Convert.ToInt32(frm.lkpTipDoc.EditValue) == 0)
                {
                    frm.txtdni.Enabled = false;
                }
                else
                {
                    frm.txtdni.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }
    }
}


