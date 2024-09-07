using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Otros.Compras;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Compras.Compras_Mercaderia
{
    public partial class Frm01Proveedor : DevExpress.XtraEditors.XtraForm
    {
        private List<EProveedor> lstProveedores = new List<EProveedor>();
        private int xposition = 0;
        public Frm01Proveedor()
        {
            InitializeComponent();
        }
        public void Carga()
        {
            lstProveedores = new BCompras().ListarProveedor();
            grdProveedor.DataSource = lstProveedores;
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

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();

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
                if (Convert.ToInt32(Obe.proc_pais_nodomic) == 0)
                    frm.lkpPaisesNoDomic.EditValue = 0;
                else
                    frm.lkpPaisesNoDomic.EditValue = Obe.proc_pais_nodomic;
                frm.txtcorreo.Text = Obe.proc_vcorreo;
                frm.txtCtaBancoNacion.Text = Obe.proc_vcta_bco_nacion;
                frm.txtResponsableProduccion.Text = Obe.proc_responsable_produccion;
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
                frm.txtModalidadPago.Text = Obe.proc_vmodalidad_pago;
                frm.txtBanco.Text = Obe.proc_vbanco_pago;
                frm.txtCuentaBancaria.Text = Obe.proc_vcuenta_corriente_banco;
                frm.txtCtaInterbancaria.Text = Obe.proc_vcodigo_interbancario;
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
                xposition = viewProveedor.FocusedRowHandle;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void eliminar()
        {
            try
            {
                EProveedor Obe = (EProveedor)viewProveedor.GetRow(viewProveedor.FocusedRowHandle);
                if (Obe == null)
                    return;
                if (XtraMessageBox.Show("Desea Eliminar el registro", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {

                    BCompras oBl = new BCompras();
                    oBl.EliminarProveedor(Obe);
                    viewProveedor.DeleteRow(viewProveedor.FocusedRowHandle);
                    viewProveedor.RefreshData();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void imprimir()
        { }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void FrmProveedor_Load(object sender, EventArgs e)
        {
            Carga();
        }

        private void viewProveedor_DoubleClick(object sender, EventArgs e)
        {
            if (lstProveedores.Count > 0)
            {
                FrmManteProveedor frm = new FrmManteProveedor();
                frm.Show();
                EProveedor Obe = (EProveedor)viewProveedor.GetRow(viewProveedor.FocusedRowHandle);
                frm.txtcodigo.Text = Obe.vcod_proveedor;
                frm.txtNombre.Text = (Obe.iid_tipo_persona == 1) ? Obe.vnombre : "";
                frm.txtPaterno.Text = (Obe.iid_tipo_persona == 1) ? Obe.vpaterno : "";
                frm.txtMaterno.Text = (Obe.iid_tipo_persona == 1) ? Obe.vmaterno : "";
                frm.txtRazonSocial.Text = (Obe.iid_tipo_persona == 1) ? Obe.vnombre + " " + Obe.vpaterno + " " + Obe.vmaterno : Obe.vnombrecompleto;
                frm.txtComercial.Text = Obe.vcomercial;
                frm.LkpTipoPersona.EditValue = Obe.iid_tipo_persona;
                frm.dtFecha.EditValue = Obe.proc_sfecha;
                frm.lkpTipDoc.EditValue = Obe.proc_tipo_doc;
                frm.txtcorreo.Text = Obe.proc_vcorreo;
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
                frm.txtdni.Text = Obe.vdni;
                //Proveedor.txtpais.Text = String.Format("{0:00}", Obe.idd_pais);
                frm.txtdireccion.Text = Obe.vdireccion;
                frm.txttelefono.Text = Obe.vtelefono;
                frm.txtfax.Text = Obe.vfax;
                frm.LkpEstado.EditValue = Obe.isituacion;
                frm.txtrepresentante.Text = Obe.vrepresentante;
                frm.txtruc.Text = Obe.vruc; ;
                frm.LkpTipoPersona.EditValue = Obe.iid_tipo_persona;
                frm.anac_icod_analitica = Obe.anac_icod_analitica;
                frm.BtnGuardar.Enabled = false;
                frm.SetCancel();
            }
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }

        private void buscarCriterio()
        {
            try
            {
                grdProveedor.DataSource = lstProveedores.Where(x =>
                    x.vcod_proveedor.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                    x.vnombrecompleto.Contains(txtDescripcion.Text.ToUpper())).ToList();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void mnuProveedor_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}