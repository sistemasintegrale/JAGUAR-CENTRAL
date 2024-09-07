using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmManteDocRecepcionCompraSuministros : DevExpress.XtraEditors.XtraForm
    {
        public EDocRecepcionCompra oBe = new EDocRecepcionCompra();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        List<EDocRecepcionCompraDet> lstDetalle = new List<EDocRecepcionCompraDet>();
        List<EDocRecepcionCompraDet> lstDelete = new List<EDocRecepcionCompraDet>();
        string strCodCliente = "";
        public int IcodAlmacen = 0;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
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
            txtSerie.Enabled = !Enabled;
            txtNumero.Enabled = !Enabled;
            dteFecha.Enabled = !Enabled;
            //lkpSituacion.Enabled = !Enabled;
            btnProveedor.Enabled = !Enabled;
            lkpMotivo.Enabled = !Enabled;
            btnAlmacen.Enabled = !Enabled;
            txtObservaciones.Enabled = !Enabled;
            lkpMotivo.Enabled = !Enabled;
            dteFecha.Enabled = !Enabled;

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtSerie.Enabled = Enabled;
                txtNumero.Enabled = Enabled;
                dteFecha.Enabled = Enabled;
                btnProveedor.Enabled = false;
                btnAlmacen.Enabled = Enabled;
                //btnCentroCostos.Enabled = Enabled;
                lkpMotivo.Enabled = Enabled;
                dteFecha.Enabled = Enabled;
            }
        }

        private void setValues()
        {
            txtSerie.Text = oBe.drcc_vnumero_doc_recepcion_compra.Substring(0, 3);
            txtNumero.Text = oBe.drcc_vnumero_doc_recepcion_compra.Substring(3, oBe.drcc_vnumero_doc_recepcion_compra.Length - 3);

            dteFecha.EditValue = oBe.drcc_sfecha;
            btnProveedor.Tag = oBe.proc_icod_proveedor;
            btnProveedor.Text = oBe.NomProveedor;
            txtObservaciones.Text = oBe.drcc_vobservaciones;
            btnAlmacen.Tag = oBe.almac_icod_almacen;
            btnAlmacen.Text = oBe.DesAlmacen;
            lkpMotivo.EditValue = oBe.tablc_iid_motivo;
            lkpSituacion.EditValue = oBe.tablc_iid_situacion;
            lstDetalle = new BCompras().listarDocRecepcionComprasDetalle(oBe.drcc_icod_doc_recepcion_compra);
            viewGuiaRemision.RefreshData();
            lkpTipoDoc.Text = oBe.TipoAbreviatura;
        }

        public frmManteDocRecepcionCompraSuministros()
        {
            InitializeComponent();

        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            setValues();
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
            setValues();
        }

        private void cargar()
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                setFecha(dteFecha);
                getNroDoc();
            }
            grdGuiaRemision.DataSource = lstDetalle;

            List<TipoDoc> lst = new List<TipoDoc>();
            //lst.Add(new TipoDoc { intCodigo = 0, strTipoDoc = ".......Eligir un documento....." });
            lst.Add(new TipoDoc { intCodigo = 103, strTipoDoc = "GRC" });
            lst.Add(new TipoDoc { intCodigo = 120, strTipoDoc = "DRC" });
            BSControls.LoaderLook(lkpTipoDoc, lst, "strTipoDoc", "intCodigo", true);
        }
        public void CargarControles()
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaSituacionGuiaRemision), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMotivo, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMotivoGuiaRemision), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
        }
        private void getNroDoc()
        {
            try
            {
                var lst = new BAdministracionSistema().getCorrelativoTipoDoc(Parametros.intTipoDocFacturaVenta, txtSerie.Text);

                if (Convert.ToInt32(lst[0].tdocc_nro_serie) != 0)
                {
                    txtSerie.Text = lst[0].tdocc_nro_serie;
                    txtNumero.Text = (Convert.ToInt32(lst[0].tdocc_nro_correlativo) + 1).ToString();
                }


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void setFecha(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnAlmacen.Tag) == 0)
            {
                XtraMessageBox.Show("Seleccione el Almacén", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (frmManteDocRecepcionCompraSuministrosDet frm = new frmManteDocRecepcionCompraSuministrosDet())
            {
                oBe.almac_icod_almacen = Convert.ToInt32(btnAlmacen.Tag);
                frm.oBeCab = oBe;
                //frm.IcodAlmacen =Convert.ToInt32(btnAlmacen.Tag);
                frm.SetInsert();
                frm.lstDetalle = lstDetalle;
                frm.txtItem.Text = (lstDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstDetalle.Count + 1);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewGuiaRemision.RefreshData();
                    viewGuiaRemision.MoveLast();
                }
            }
        }

        private void setCliente(int intCliente)
        {
            try
            {
                var _Be = new BVentas().ListarCliente().Where(x => x.cliec_icod_cliente == intCliente).ToList()[0];
                btnProveedor.Tag = _Be.cliec_icod_cliente;
                btnProveedor.Text = _Be.cliec_vnombre_cliente;
                txtObservaciones.Text = _Be.cliec_vdireccion_cliente;
                strCodCliente = _Be.cliec_vcod_cliente;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (Convert.ToInt32(txtSerie.Text) == 0)
                {
                    oBase = txtSerie;
                    throw new ArgumentException("Ingrese Nro. de Serie de Guía de Remisión");
                }

                if (txtSerie.Text == "000")
                {
                    oBase = txtSerie;
                    throw new ArgumentException("N° de Serie no registrado, registrar N° serie en REGISTRO DE TIPOS DE DOCUMENTOS");
                }

                if (Convert.ToInt32(txtNumero.Text) == 0)
                {
                    oBase = txtNumero;
                    throw new ArgumentException("Ingrese Nro. de Guía de Remisión");
                }

                if (Convert.ToDateTime(dteFecha.Text).Year != Parametros.intEjercicio)
                {
                    oBase = dteFecha;
                    throw new ArgumentException("La fecha seleccionada esta fuera del rango del ejercicio");
                }

                //if (Convert.ToInt32(btnCliente.Tag) == 0)
                //{
                //    oBase = btnCliente;
                //    throw new ArgumentException("Seleccione el destinatario");
                //}

                //if (String.IsNullOrWhiteSpace(txtRUC.Text))
                //{
                //    oBase = txtRUC;
                //    throw new ArgumentException("Cliente no cuenta con RUC registrado, favor de registrar RUC del Cliente");
                //}
                //if (Convert.ToInt32(lkpMotivo.EditValue) == 226)
                //{
                //    if (Convert.ToInt32(btnAlmacenIngreso.Tag) == 0)
                //    {
                //        oBase = btnAlmacenIngreso;
                //        throw new ArgumentException("Ingrese el Almacén para el ingreso del Producto");
                //    }
                //}
                //if (Convert.ToInt32(bteProyecto.Tag) > 0)
                //{
                //    if (Convert.ToInt32(btnCentroCostos.Tag) < 0)
                //    {
                //         throw new ArgumentException("Ingrese Centro Costo");
                //    }
                //}
                oBe.drcc_vnumero_doc_recepcion_compra = String.Format("{0}{1}", txtSerie.Text, txtNumero.Text);
                oBe.drcc_sfecha = Convert.ToDateTime(dteFecha.Text);
                oBe.proc_icod_proveedor = Convert.ToInt32(btnProveedor.Tag);
                //oBe.remic_vnombre_destinatario = btnProveedor.Text;
                oBe.drcc_vobservaciones = txtObservaciones.Text;
                oBe.almac_icod_almacen = Convert.ToInt32(btnAlmacen.Tag);
                oBe.tablc_iid_motivo = Convert.ToInt32(lkpMotivo.EditValue);
                oBe.tablc_iid_situacion = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.tdocc_icod_tipo_doc = Convert.ToInt32(lkpTipoDoc.EditValue);
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.drcc_icod_doc_recepcion_compra = new BCompras().insertarDocRecepcionCompras(oBe, lstDetalle);
                }
                else
                {
                    new BCompras().modificarDocRecepcionCompras(oBe, lstDetalle, lstDelete);
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
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    MiEvento(oBe.drcc_icod_doc_recepcion_compra);
                    Close();
                }
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

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EDocRecepcionCompraDet obe = (EDocRecepcionCompraDet)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteDocRecepcionCompraSuministrosDet frm = new frmManteDocRecepcionCompraSuministrosDet())
            {
                frm.oBe = obe;
                frm.lstDetalle = lstDetalle;
                frm.SetModify();

                frm.txtItem.Text = String.Format("{0:000}", obe.drcd_iitem);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewGuiaRemision.RefreshData();
                    viewGuiaRemision.MoveLast();
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EDocRecepcionCompraDet obe = (EDocRecepcionCompraDet)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstDetalle.Remove(obe);
            viewGuiaRemision.RefreshData();
        }

        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
        }
        private void listarAlmacen()
        {
            using (frmListarAlmacen frm = new frmListarAlmacen())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnAlmacen.Tag = frm._Be.almac_icod_almacen;
                    btnAlmacen.Text = frm._Be.almac_vdescripcion;
                    /***************************************************************/
                    oBe.almac_icod_almacen = frm._Be.almac_icod_almacen;

                }
            }
        }

        private void bteAlmacen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarAlmacen();
        }
        private void txtItems_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }
        private void txtdias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtSerie_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                getNroDoc();
            }
        }


        private void lkpMotivo_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
        }

        private void lkpMotivo_EditValueChanged_2(object sender, EventArgs e)
        {

        }




        private void txtNumero_Leave(object sender, EventArgs e)
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            List<EGuiaRemision> Lstver = new BVentas().getGRCabVerificarNumero(String.Format("{0}{1}", txtSerie.Text, txtNumero.Text));
            if (Lstver.Count > 0)
            {

                oBase = txtNumero;
                XtraMessageBox.Show("El Número " + String.Format("{0}{1}", txtSerie.Text, txtNumero.Text) + " N° G/R: Ya Existia");


            }
        }



        private void frmManteGuiaRemision_KeyDown(object sender, KeyEventArgs e)
        {
        }
        private void frmManteGuiaRemision_Load(object sender, EventArgs e)
        {
            cargar();
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                List<TipoDoc> lst = new List<TipoDoc>();
                //lst.Add(new TipoDoc { intCodigo = 0, strTipoDoc = ".......Eligir un documento....." });
                lst.Add(new TipoDoc { intCodigo = 103, strTipoDoc = "GRC" });
                lst.Add(new TipoDoc { intCodigo = 120, strTipoDoc = "DRC" });
                BSControls.LoaderLook(lkpTipoDoc, lst, "strTipoDoc", "intCodigo", true);
                lkpTipoDoc.Text = oBe.TipoAbreviatura;
            }
        }
        public class TipoDoc
        {
            public int intCodigo { get; set; }
            public string strTipoDoc { get; set; }
        }
        private void btnCliente_KeyDown(object sender, KeyEventArgs e)
        {
        }
        private void txtSerie_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProveedor();
        }
        private void ListarProveedor()
        {
            FrmListarProveedor Proveedor = new FrmListarProveedor();
            Proveedor.Carga();
            if (Proveedor.ShowDialog() == DialogResult.OK)
            {
                btnProveedor.Tag = Proveedor._Be.iid_icod_proveedor;
                btnProveedor.Text = Proveedor._Be.vcod_proveedor;
            }

        }

        private void lkpTipoDoc_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpTipoDoc.EditValue) == 103)
            {
                txtSerie.Enabled = true;
                txtNumero.Enabled = true;
                txtNumero2.Enabled = false;
            }
            else if (Convert.ToInt32(lkpTipoDoc.EditValue) == 120)
            {
                txtSerie.Enabled = false;
                txtNumero.Enabled = false;
                txtNumero2.Enabled = true;
            }
        }
    }
}