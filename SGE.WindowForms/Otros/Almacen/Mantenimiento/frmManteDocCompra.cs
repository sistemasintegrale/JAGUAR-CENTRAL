using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Otros.Compras;
using SGE.WindowForms.Otros.Operaciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmManteDocCompra : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        /*--------------*/
        public EDocCompra Obe = new EDocCompra();
        public List<EDocCompraDet> lstDetalle = new List<EDocCompraDet>();
        public List<EDocCompraDet> lstDelete = new List<EDocCompraDet>();
        /*--------------*/
        public bool flag_NoGravado = false;
        public bool flag_carga = false;

        public frmManteDocCompra()
        {
            InitializeComponent();
        }

        private void frmManteDocCompra_Load(object sender, EventArgs e)
        {
            cargar();
            visibleNogravado(flag_NoGravado);
            txtSerie.Focus();
        }

        private void visibleNogravado(bool flag)
        {
            labelControl11.Visible = !flag;
            txtIGV.Visible = !flag;
            cbIncluyeIGV.Visible = !flag;
            labelControl22.Visible = !flag;
            txtMtoNeto.Visible = !flag;
        }

        public void setGuardar()
        {
            SetSave();
        }
        private void cargar()
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(21), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpFormaPago, new BGeneral().listarTablaRegistro(20), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            /**/
            List<TipoDoc> lst = new List<TipoDoc>();
            if (flag_NoGravado)
            {
                lst.Add(new TipoDoc { intCodigo = 84, strTipoDoc = "BOC" });
                lst.Add(new TipoDoc { intCodigo = 96, strTipoDoc = "TKB" });
                lst.Add(new TipoDoc { intCodigo = 97, strTipoDoc = "NTC" });
            }
            else
            {
                lst.Add(new TipoDoc { intCodigo = 84, strTipoDoc = "BOC" });
                lst.Add(new TipoDoc { intCodigo = 24, strTipoDoc = "FAC" });
                lst.Add(new TipoDoc { intCodigo = 95, strTipoDoc = "TKF" });
            }
            /**/
            BSControls.LoaderLook(lkpTipoDoc, lst.OrderBy(x => x.intCodigo).ToList(), "strTipoDoc", "intCodigo", true);
            /**/
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtIGV.Text = Parametros.strPorcIGV;
                setFecha(dtFecha);
                setFecha(dtFechaVencimiento);
                setAlmacen();
                txtCorrelativo.Text = String.Format("{0:00000}", new BCuentasPorPagar().getCorrelativoDocPorPagar(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue)));
            }
            else
                lstDetalle = new BCompras().listarDocCompraDet(Obe.facc_icod_doc);
            grdDetalle.DataSource = lstDetalle;
        }

        public class TipoDoc
        {
            public int intCodigo { get; set; }
            public string strTipoDoc { get; set; }
        }

        private void setFecha(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
            {
                fecha.EditValue = DateTime.Now;

            }
            else
            {
                fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);

            }
        }

        private void setAlmacen()
        {
            var lstAlmacen = new BAlmacen().listarAlmacenes();
            if (lstAlmacen.Count > 0)
            {
                bteAlmacen.Text = lstAlmacen[0].almac_vdescripcion;
                bteAlmacen.Tag = lstAlmacen[0].almac_icod_almacen;
            }
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
            lkpTipoDoc.Enabled = !Enabled;
            txtSerie.Enabled = !Enabled;
            txtCorrelativo.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpTipoDoc.Enabled = Enabled;
                bteAlmacen.Enabled = Enabled;
                cbIncluyeIGV.Enabled = Enabled;
                bteProveedor.Enabled = Enabled;
            }
            if (Status == BSMaintenanceStatus.View)
            {
                lkpMoneda.Enabled = false;
                bteAlmacen.Enabled = false;
                bteProveedor.Enabled = false;
                txtObservacion.Properties.ReadOnly = true;
                lkpFormaPago.Enabled = false;
                bteComprador.Enabled = false;
                cbIncluyeIGV.Enabled = false;
                lkpMes.Enabled = false; mnu.Enabled = false;
                dtFecha.Enabled = false; dtFechaVencimiento.Enabled = false;
            }
        }

        public void setValues()
        {
            flag_carga = true;

            lkpTipoDoc.EditValue = Obe.tdocc_icod_tipo_doc;
            string[] split = Obe.facc_num_doc.Split('-');
            if (split.Count() == 1)
            {
                txtSerie.Text = Obe.facc_num_doc.Substring(0, 3);
                txtNumeroDocumento.Text = Obe.facc_num_doc.Substring(3);
            }
            else
            {
                txtNroDocumento.Text = Obe.facc_num_doc;
            }

            dtFecha.EditValue = Obe.facc_sfecha_doc;
            lkpSituacion.EditValue = Obe.facc_isituacion;
            bteProveedor.Text = Obe.strProveedor;
            bteProveedor.Tag = Obe.proc_icod_proveedor;
            lkpFormaPago.EditValue = Obe.facc_iforma_pago;
            lkpMoneda.EditValue = Obe.tablc_iid_tipo_moneda;
            bteAlmacen.Text = Obe.strAlmacen;
            bteAlmacen.Tag = Obe.almac_icod_almacen;
            dtFechaVencimiento.EditValue = Obe.facc_svencimiento;
            txtObservacion.Text = Obe.facc_vobservaciones;
            txtIGV.Text = Obe.facc_nporcent_imp_doc.ToString();
            txtCorrelativo.Text = String.Format("{0:00000}", Obe.intCorrelativoDXP);
            lkpMes.EditValue = Convert.ToInt32(Obe.facc_mes);
            bteComprador.Tag = Convert.ToInt32(Obe.facc_icod_comprador);
            bteComprador.Text = Obe.strComprador;
            cbIncluyeIGV.Checked = Obe.facc_flag_incluye_igv;
            txtMtoNeto.Text = Obe.facc_nmonto_neto_doc.ToString();
            txtMtoTotal.Text = Obe.facc_nmonto_total_doc.ToString();
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
            Boolean flagCorr = false;

            try
            {
                if (Convert.ToInt32(lkpTipoDoc.EditValue) == 24)
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (Convert.ToInt64(txtSerie.Text) == 0)
                        {
                            oBase = txtSerie;
                            throw new ArgumentException("Ingrese el Nro. de serie");
                        }
                    }
                if (Convert.ToInt64(txtCorrelativo.Text) == 0)
                {
                    oBase = txtCorrelativo;
                    throw new ArgumentException("Ingrese el correlativo del Doc de Compra");
                }
                if (bteProveedor.Tag == null)
                {
                    oBase = bteProveedor;
                    throw new ArgumentException("Seleccione proveedor");
                }

                if (bteAlmacen.Tag == null)
                {
                    oBase = bteAlmacen;
                    throw new ArgumentException("Seleccione almacén");
                }

                if (lstDetalle.Count == 0)
                {
                    throw new ArgumentException("No ha ingresado items en el detalle del documento de compra");
                }

                if (Convert.ToDateTime(dtFecha.EditValue).Year != Parametros.intEjercicio)
                {
                    if (XtraMessageBox.Show("La fecha ingresa no esta dentro del año de ejercicio, ¿Esta seguro que desea continuar con la grabación?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        Flag = false;
                        return;
                    }
                }

                if (Convert.ToInt32(bteComprador.Tag) == 0)
                {
                    oBase = bteComprador;
                    throw new ArgumentException("Seleccione Comprador");
                }
                if (Convert.ToInt32(lkpMes.EditValue) == 0)
                {
                    oBase = lkpMes;
                    throw new ArgumentException("Seleccione Mes del Proceso");
                }

                Obe.facc_anio = Convert.ToInt16(Parametros.intEjercicio);
                Obe.facc_mes = Convert.ToInt16(lkpMes.EditValue);
                Obe.intCorrelativoDXP = Convert.ToInt64(txtCorrelativo.Text);
                Obe.flagCorrelativo = false;
                /**/
                Obe.strTipoDoc = lkpTipoDoc.Text;
                Obe.tdocc_icod_tipo_doc = Convert.ToInt32(lkpTipoDoc.EditValue);
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (Convert.ToInt32(lkpTipoDoc.EditValue) == 24 || Convert.ToInt32(lkpTipoDoc.EditValue) == 84)
                    {
                        Obe.facc_num_doc = txtSerie.Text + txtNumeroDocumento.Text;
                    }
                    else
                    {
                        Obe.facc_num_doc = txtNroDocumento.Text;
                    }
                }

                Obe.facc_sfecha_doc = Convert.ToDateTime(dtFecha.EditValue);
                Obe.facc_isituacion = Convert.ToInt32(lkpSituacion.EditValue);
                Obe.strProveedor = bteProveedor.Text;
                Obe.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
                Obe.facc_iforma_pago = Convert.ToInt32(lkpFormaPago.EditValue);
                Obe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                Obe.strAlmacen = bteAlmacen.Text;
                Obe.almac_icod_almacen = Convert.ToInt32(bteAlmacen.Tag);
                Obe.facc_svencimiento = Convert.ToDateTime(dtFechaVencimiento.EditValue);
                Obe.facc_vobservaciones = txtObservacion.Text;
                Obe.facc_nporcent_imp_doc = Convert.ToDecimal(txtIGV.Text);
                if (cbIncluyeIGV.Visible)
                    Obe.facc_flag_incluye_igv = cbIncluyeIGV.Checked;
                /**/
                Obe.facc_nmonto_total_doc = Convert.ToDecimal(txtMtoTotal.Text);
                Obe.facc_nmonto_neto_doc = (flag_NoGravado) ? 0 : Convert.ToDecimal(txtMtoNeto.Text);
                Obe.facc_nmonto_imp = (flag_NoGravado) ? 0 : Obe.facc_nmonto_total_doc - Obe.facc_nmonto_neto_doc;
                Obe.facc_icod_comprador = Convert.ToInt32(bteComprador.Tag);


                /**/
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    var lngCorrelativo = new BCuentasPorPagar().getCorrelativoDocPorPagar(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue));
                    if (lngCorrelativo != Obe.intCorrelativoDXP)
                    {
                        flagCorr = true;
                        Obe.intCorrelativoDXP = lngCorrelativo;
                    }
                    Obe.facc_icod_doc = new BCompras().insertarDocCompra(Obe, lstDetalle);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BCompras().modificarDocCompra(Obe, lstDetalle, lstDelete);
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
                    this.MiEvento(Obe.facc_icod_doc);
                    this.Close();
                    if (flagCorr)
                        XtraMessageBox.Show(String.Format("El número CORRELATIVO del Doc. Por Pagar es: {0:00000}", Obe.intCorrelativoDXP), "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void listarProveedor()
        {
            FrmListarProveedor frm = new FrmListarProveedor();
            frm.Carga();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bteProveedor.Tag = frm._Be.iid_icod_proveedor;
                bteProveedor.Text = frm._Be.vnombrecompleto;
            }
        }

        private void bteProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProveedor();
        }

        private void bteProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarProveedor();
        }

        private void listarAlmacen()
        {
            using (frmListarAlmacen frm = new frmListarAlmacen())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteAlmacen.Tag = frm._Be.almac_icod_almacen;
                    bteAlmacen.Text = frm._Be.almac_vdescripcion;
                }
            }
        }

        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
        }

        private void bteAlmacen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarAlmacen();
        }
        private void nuevo()
        {
            BaseEdit oBase = null;
            try
            {
                if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                {
                    oBase = bteAlmacen;
                    throw new ArgumentException("Seleccione Almacén");
                }
                using (frmManteDocCompraDetalle frm = new frmManteDocCompraDetalle())
                {
                    frm.SetInsert();
                    frm.lstDetalle = lstDetalle;
                    frm.txtMoneda.Text = lkpMoneda.Text;
                    frm.txtItem.Text = (lstDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstDetalle.Count + 1);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        viewDetalle.RefreshData();
                        viewDetalle.MoveLast();
                        setTotal();
                    }
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
            }
        }

        private void setTotal()
        {
            if (lstDetalle.Count > 0)
            {
                if (flag_NoGravado == false)
                {
                    if (cbIncluyeIGV.Checked)
                    {
                        decimal total = lstDetalle.Sum(x => x.facd_nmonto_total);
                        decimal neto = Math.Round(total / Convert.ToDecimal("1." + txtIGV.Text.Replace(".", "")), 2);
                        txtMtoNeto.Text = Convertir.RedondearNumero(neto).ToString();
                        txtMtoTotal.Text = Convertir.RedondearNumero(total).ToString();
                    }
                    else
                    {
                        decimal neto = lstDetalle.Sum(x => x.facd_nmonto_total);
                        decimal total = Math.Round(neto * Convert.ToDecimal("1." + txtIGV.Text.Replace(".", "")), 2);
                        txtMtoNeto.Text = Convertir.RedondearNumero(neto).ToString();
                        txtMtoTotal.Text = Convertir.RedondearNumero(total).ToString();
                    }
                }
                else
                {
                    decimal total = lstDetalle.Sum(x => x.facd_nmonto_total);
                    txtMtoTotal.Text = Convertir.RedondearNumero(total).ToString();
                }
            }
        }

        private void modificar()
        {
            EDocCompraDet obe = (EDocCompraDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            int index = viewDetalle.FocusedRowHandle;
            using (frmManteDocCompraDetalle frm = new frmManteDocCompraDetalle())
            {
                frm.obe = obe;
                frm.intClasificacion = obe.intClasificacion;
                frm.lstDetalle = lstDetalle;
                frm.SetModify();
                frm.txtMoneda.Text = lkpMoneda.Text;
                frm.txtItem.Text = String.Format("{0:000}", obe.facd_iitem);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewDetalle.RefreshData();
                    setTotal();
                    viewDetalle.FocusedRowHandle = index;
                }
            }
        }

        private void eliminar()
        {
            EDocCompraDet obe = (EDocCompraDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstDetalle.Remove(obe);
            viewDetalle.RefreshData();
            setTotal();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void cbIncluyeIGV_CheckedChanged(object sender, EventArgs e)
        {
            setTotal();
        }

        private void fechaChanged(bool carga)
        {
            try
            {
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (Convert.ToDateTime(dtFecha.EditValue).Year < Parametros.intEjercicio)
                    {

                        txtCorrelativo.Text = String.Format("{0:00000}", new BCuentasPorPagar().getCorrelativoDocPorPagar(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue)));
                    }
                    else
                    {

                        txtCorrelativo.Text = String.Format("{0:00000}", new BCuentasPorPagar().getCorrelativoDocPorPagar(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue)));
                    }
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    if (!flag_carga)
                    {
                        if (Convert.ToDateTime(dtFecha.EditValue).Year != Parametros.intEjercicio)
                        {
                            dtFecha.EditValue = Obe.facc_sfecha_doc;
                            throw new ArgumentException("La fecha seleccionada esta fuera del año de ejercicio");
                        }

                        if (Convert.ToDateTime(dtFecha.EditValue).Month != Convert.ToInt32(lkpMes.EditValue))
                        {
                            dtFecha.EditValue = Obe.facc_sfecha_doc;
                            throw new ArgumentException("La fecha seleccionada esta fuera del mes de proceso");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtFecha_EditValueChanged(object sender, EventArgs e)
        {
            fechaChanged(flag_carga);
            flag_carga = false;
            switch (Convert.ToDateTime(dtFecha.EditValue).Month)
            {
                case 0:
                    lkpMes.EditValue = 43;
                    break;
                case 1:
                    lkpMes.EditValue = 44;
                    break;
                case 2:
                    lkpMes.EditValue = 45;
                    break;
                case 3:
                    lkpMes.EditValue = 46;
                    break;
                case 4:
                    lkpMes.EditValue = 47;
                    break;
                case 5:
                    lkpMes.EditValue = 48;
                    break;
                case 6:
                    lkpMes.EditValue = 49;
                    break;
                case 7:
                    lkpMes.EditValue = 50;
                    break;
                case 8:
                    lkpMes.EditValue = 51;
                    break;
                case 9:
                    lkpMes.EditValue = 43;
                    break;
                case 10:
                    lkpMes.EditValue = 52;
                    break;
                case 11:
                    lkpMes.EditValue = 54;
                    break;
                case 12:
                    lkpMes.EditValue = 55;
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }

        private void lkpMes_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtCorrelativo.Text = String.Format("{0:00000}", new BCuentasPorPagar().getCorrelativoDocPorPagar(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue)));
            }
        }

        private void bteComprador_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarPersonal frm = new frmListarPersonal())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteComprador.Tag = frm._Be.perc_icod_personal;
                    bteComprador.Text = frm._Be.perc_vapellidos_nombres;
                }
            }
        }

        private void dtFechaVencimiento_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtSerie_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtSerie.Text) == 0)
            {
                txtNroDocumento.Properties.ReadOnly = false;
            }
            else
            {
                txtNroDocumento.Properties.ReadOnly = true;
                txtNroDocumento.Text = "";
            }
        }

        private void txtNroDocumento_EditValueChanged(object sender, EventArgs e)
        {
            if (txtNroDocumento.Text == "")
            {
                txtSerie.Properties.ReadOnly = false;
                txtCorrelativo.Properties.ReadOnly = false;
            }
            else
            {
                txtSerie.Properties.ReadOnly = true;
                txtCorrelativo.Properties.ReadOnly = true;
            }
        }

        private void lkpTipoDoc_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpTipoDoc.EditValue) == 24 || Convert.ToInt32(lkpTipoDoc.EditValue) == 84)
            {
                txtSerie.Properties.ReadOnly = false;
                txtSerie.Text = "";
                txtNumeroDocumento.Properties.ReadOnly = false;
                txtNumeroDocumento.Text = "";
                txtNroDocumento.Properties.ReadOnly = true;
                txtNroDocumento.Text = "";
            }
            else
            {
                txtSerie.Properties.ReadOnly = true;
                txtSerie.Text = "";
                txtNumeroDocumento.Properties.ReadOnly = true;
                txtNumeroDocumento.Text = "";
                txtNroDocumento.Properties.ReadOnly = false;
                txtNroDocumento.Text = "";
            }
        }
    }
}