using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Producción
{
    public partial class frmManteOrdenProdución : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteOrdenProdución));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EOrdenProduccion oBe = new EOrdenProduccion();
        public List<EOrdenProduccion> lstOrdenProduccion = new List<EOrdenProduccion>();
        List<EOrdenProduccionDet> lstOrdenProduccionDetalle = new List<EOrdenProduccionDet>();
        List<EOrdenProduccionDet> lstDelete = new List<EOrdenProduccionDet>();
        List<EOrdenProduccionAreas> lstOrdenProduccionAreas = new List<EOrdenProduccionAreas>();
        List<EOrdenProduccionAreas> lstdeleteOPA = new List<EOrdenProduccionAreas>();
        List<EAreaProduccion> lstProceso = new List<EAreaProduccion>();
        List<EFichaTecnicaDet> lstMaterial = new List<EFichaTecnicaDet>();
        public bool FromAsignacion = false;
        public frmManteOrdenProdución()
        {
            InitializeComponent();
            txtNrOrden.Properties.ReadOnly = true;
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
            //txtcodigo.Enabled = Enabled;
            dteFecha.Enabled = !Enabled;
            btnPedido.Enabled = !Enabled;
            btnItemPedido.Enabled = !Enabled;
            btnFichaTecnica.Enabled = !Enabled;
            lkpResponsable.Enabled = !Enabled;
            LkpColor.Enabled = !Enabled;
            LkpMarca.Enabled = !Enabled;
            lkpTipo.Enabled = !Enabled;
            btnmodelo.Enabled = !Enabled;
            lkpSerie.Enabled = !Enabled;
            lkpLinea.Enabled = !Enabled;
            txtTotal.Enabled = !Enabled;
            //txtNrOrden.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtNrOrden.Enabled = Enabled;
                dteFecha.Enabled = Enabled;
                btnPedido.Enabled = Enabled;
                btnFichaTecnica.Enabled = Enabled;
                //txtResponsable.Enabled = Enabled;
                LkpColor.Enabled = Enabled;
                LkpMarca.Enabled = Enabled;
                lkpTipo.Enabled = Enabled;
                btnmodelo.Enabled = Enabled;
                lkpSerie.Enabled = Enabled;
                btncodigoProducto.Enabled = Enabled;
                btnItemPedido.Enabled = Enabled;
                lkpLinea.Enabled = Enabled;
                btnGenerar.Enabled = Enabled;
            }
            //if (Status == BSMaintenanceStatus.CreateNew)
            //txtNrOrden.Enabled = Enabled;
        }
        public void setValues()
        {
            txtNrOrden.Text = oBe.orprc_icod_orden_produccion;
            dteFecha.EditValue = oBe.orprc_sfecha_orden_produccion;
            btnPedido.Tag = oBe.orprc_ipedido;
            btnPedido.Text = oBe.orprc_vpedido;
            btnItemPedido.Tag = oBe.orprc_iitem_pedido;
            btnItemPedido.Text = oBe.orprc_vitem_pedido;
            btnFichaTecnica.Tag = oBe.orprc_ificha_tecnica;
            btnFichaTecnica.Text = oBe.orprc_vficha_tecnica;
            btncodigoProducto.Tag = oBe.orprc_vcodigo_producto;
            btncodigoProducto.Text = oBe.orprc_vcodigo_producto;
            txtdescripcion.Text = oBe.orprc_vdescripcion_producto;
            LkpMarca.EditValue = oBe.orprc_imarca;
            btnmodelo.Tag = oBe.orprc_imodelo;
            btnmodelo.Text = oBe.DesModelo;
            LkpColor.EditValue = oBe.orprc_icolor;
            lkpTipo.EditValue = oBe.orprc_itipo;
            lkpSerie.EditValue = oBe.orprc_iserie;
            txttalla1.Text = oBe.orprc_talla1.ToString();
            txttalla2.Text = oBe.orprc_talla2.ToString();
            txttalla3.Text = oBe.orprc_talla3.ToString();
            txttalla4.Text = oBe.orprc_talla4.ToString();
            txttalla5.Text = oBe.orprc_talla5.ToString();
            txttalla6.Text = oBe.orprc_talla6.ToString();
            txttalla7.Text = oBe.orprc_talla7.ToString();
            txttalla8.Text = oBe.orprc_talla8.ToString();
            txttalla9.Text = oBe.orprc_talla9.ToString();
            txttalla10.Text = oBe.orprc_talla10.ToString();
            txtcantidad1.Text = oBe.orprc_cant_talla1.ToString();
            txtcantidad2.Text = oBe.orprc_cant_talla2.ToString();
            txtcantidad3.Text = oBe.orprc_cant_talla3.ToString();
            txtcantidad4.Text = oBe.orprc_cant_talla4.ToString();
            txtcantidad5.Text = oBe.orprc_cant_talla5.ToString();
            txtcantidad6.Text = oBe.orprc_cant_talla6.ToString();
            txtcantidad7.Text = oBe.orprc_cant_talla7.ToString();
            txtcantidad8.Text = oBe.orprc_cant_talla8.ToString();
            txtcantidad9.Text = oBe.orprc_cant_talla9.ToString();
            txtcantidad10.Text = oBe.orprc_cant_talla10.ToString();
            icodProducto[0] = oBe.orprc_icod_producto1;
            icodProducto[1] = oBe.orprc_icod_producto2;
            icodProducto[2] = oBe.orprc_icod_producto3;
            icodProducto[3] = oBe.orprc_icod_producto4;
            icodProducto[4] = oBe.orprc_icod_producto5;
            icodProducto[5] = oBe.orprc_icod_producto6;
            icodProducto[6] = oBe.orprc_icod_producto7;
            icodProducto[7] = oBe.orprc_icod_producto8;
            icodProducto[8] = oBe.orprc_icod_producto9;
            icodProducto[9] = oBe.orprc_icod_producto10;
            txtProcesar1.Text = oBe.orprc_cant_aprocesar1.ToString();
            txtProcesar2.Text = oBe.orprc_cant_aprocesar2.ToString();
            txtProcesar3.Text = oBe.orprc_cant_aprocesar3.ToString();
            txtProcesar4.Text = oBe.orprc_cant_aprocesar4.ToString();
            txtProcesar5.Text = oBe.orprc_cant_aprocesar5.ToString();
            txtProcesar6.Text = oBe.orprc_cant_aprocesar6.ToString();
            txtProcesar7.Text = oBe.orprc_cant_aprocesar7.ToString();
            txtProcesar8.Text = oBe.orprc_cant_aprocesar8.ToString();
            txtProcesar9.Text = oBe.orprc_cant_aprocesar9.ToString();
            txtProcesar10.Text = oBe.orprc_cant_aprocesar10.ToString();
            txtTotal.Text = oBe.orprc_ntotal.ToString();
            txtDocenas.Text = oBe.orprc_ndocenas.ToString();
            lkpLinea.EditValue = oBe.orprc_ilinea;
            lkpLinea.Text = oBe.orprc_vlinea;
            lkpResponsable.EditValue = oBe.orprc_iresponsable;
            lkpResponsable.Text = oBe.orprc_vresponsable;
            lkpSituacion.EditValue = oBe.orprc_isituacion;

            lstOrdenProduccionDetalle = new BCentral().listarOrdenPrduccionDetalle(oBe.orprc_iid_orden_produccion);
            grdMateriales.DataSource = lstOrdenProduccionDetalle;
            viewMateriales.RefreshData();
            if (lstOrdenProduccionDetalle.Count > 0)
            {
                btnPedido.Enabled = false;
                btnItemPedido.Enabled = false;
                btnFichaTecnica.Enabled = false;
            }

            lstOrdenProduccionAreas = new BCentral().listarOrdenPrduccionArea(oBe.orprc_iid_orden_produccion);
            if (lstOrdenProduccionAreas.Count == 0)
            {
                lstProceso = new BCentral().listarAreaProduccion();
                foreach (var objd in lstProceso)
                {
                    EOrdenProduccionAreas op = new EOrdenProduccionAreas();
                    op.orprac_icod_proceso = objd.arprc_iid_codigo;
                    op.strproceso = objd.arprc_vdescripcion;
                    op.orprac_sfecha_asignacion = "";
                    op.intTipoOperacion = 1;
                    op.orprc_isituacion = Constantes.EstadoTareaPersonalPendiente;
                    op.strEstado = "PENDIENTE";
                    lstOrdenProduccionAreas.Add(op);
                }
                grdPersonal.DataSource = lstOrdenProduccionAreas;
                viewPersonal.RefreshData();
            }
            else
            {
                grdPersonal.DataSource = lstOrdenProduccionAreas;
                viewPersonal.RefreshData();
            }
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            getNroDoc();

        }

        public void GenerarAreasproduccion()
        {
            lstProceso = new BCentral().listarAreaProduccion().Where(x => lstMaterial.Exists(y => y.fited_iarea == x.arprc_iid_codigo)).ToList();
            foreach (var objd in lstProceso)
            {
                EOrdenProduccionAreas op = new EOrdenProduccionAreas();
                op.orprac_icod_proceso = objd.arprc_iid_codigo;
                op.strproceso = objd.arprc_vdescripcion;
                if (op.orprac_sfecha_asignacion == Convert.ToString("01/01/0001 0:00:00"))
                {
                    op.orprac_sfecha_asignacion = "";
                }
                if (op.orprac_sfecha_terminado == Convert.ToString("01/01/0001 0:00:00"))
                {
                    op.orprac_sfecha_terminado = "";
                }
                lstOrdenProduccionAreas.Add(op);
            }
            grdPersonal.DataSource = lstOrdenProduccionAreas;
            viewPersonal.RefreshData();
        }
        private void getNroDoc()
        {
            try
            {


                txtNrOrden.Text = new BCentral().UltimoCorrelativoTabla(Tablas.ORDEN_PRODUCCION).ToString("D6");

                //txtNumero.Text = (Convert.ToInt32(lst[0].puvec_icorrelativo_factura) + 1).ToString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;


            txtNrOrden.Enabled = true;
            btnPedido.Enabled = true;
            btnItemPedido.Enabled = true;
            btnFichaTecnica.Enabled = true;

            setValues();
            txtcantidad1.Enabled = Convert.ToInt32(txtProcesar1.Text) > 0;
            txtcantidad2.Enabled = Convert.ToInt32(txtProcesar2.Text) > 0;
            txtcantidad3.Enabled = Convert.ToInt32(txtProcesar3.Text) > 0;
            txtcantidad4.Enabled = Convert.ToInt32(txtProcesar4.Text) > 0;
            txtcantidad5.Enabled = Convert.ToInt32(txtProcesar5.Text) > 0;
            txtcantidad6.Enabled = Convert.ToInt32(txtProcesar6.Text) > 0;
            txtcantidad7.Enabled = Convert.ToInt32(txtProcesar7.Text) > 0;
            txtcantidad8.Enabled = Convert.ToInt32(txtProcesar8.Text) > 0;
            txtcantidad9.Enabled = Convert.ToInt32(txtProcesar9.Text) > 0;
            txtcantidad10.Enabled = Convert.ToInt32(txtProcesar10.Text) > 0;
        }
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                if (string.IsNullOrEmpty(txtNrOrden.Text))
                {
                    oBase = txtNrOrden;
                    throw new ArgumentException("Ingresar Numero de Orden");
                }

                if (string.IsNullOrEmpty(Convert.ToString(btnFichaTecnica.Tag)))
                {
                    oBase = btnFichaTecnica;
                    throw new ArgumentException("Seleccionar Ficha de Tecnica");
                }

                if (string.IsNullOrEmpty(Convert.ToString(btnmodelo.Tag)))
                {
                    oBase = btnmodelo;
                    throw new ArgumentException("Selecione Modelo");
                }
                TextEdit[] textoCantidad = GetTextoCantidad();
                TextEdit[] textoTalla = GetTextoTalla();

                oBe.orprc_icod_orden_produccion = txtNrOrden.Text;
                oBe.orprc_sfecha_orden_produccion = Convert.ToDateTime(dteFecha.Text);
                oBe.orprc_ipedido = Convert.ToInt32(btnPedido.Tag);
                oBe.orprc_vpedido = btnPedido.Text;
                oBe.orprc_iitem_pedido = Convert.ToInt32(btnItemPedido.Tag);
                oBe.orprc_vitem_pedido = btnItemPedido.Text;
                oBe.pedido = "P-" + oBe.orprc_vitem_pedido + "." + oBe.orprc_vitem_pedido;
                oBe.orprc_ificha_tecnica = Convert.ToInt32(btnFichaTecnica.Tag);
                oBe.orprc_vficha_tecnica = btnFichaTecnica.Text;
                oBe.orprc_vcodigo_producto = btncodigoProducto.Text;
                oBe.orprc_vdescripcion_producto = txtdescripcion.Text;
                oBe.orprc_imarca = Convert.ToInt32(LkpMarca.EditValue);
                oBe.orprc_imodelo = Convert.ToInt32(btnmodelo.Tag);
                oBe.orprc_icolor = Convert.ToInt32(LkpColor.EditValue);
                oBe.orprc_itipo = Convert.ToInt32(lkpTipo.EditValue);
                oBe.orprc_iserie = Convert.ToInt32(lkpSerie.EditValue);
                oBe.orprc_talla1 = Convert.ToInt32((textoTalla[0].Text == "") ? null : textoTalla[0].Text);
                oBe.orprc_talla2 = Convert.ToInt32((textoTalla[1].Text == "") ? null : textoTalla[1].Text);
                oBe.orprc_talla3 = Convert.ToInt32((textoTalla[2].Text == "") ? null : textoTalla[2].Text);
                oBe.orprc_talla4 = Convert.ToInt32((textoTalla[3].Text == "") ? null : textoTalla[3].Text);
                oBe.orprc_talla5 = Convert.ToInt32((textoTalla[4].Text == "") ? null : textoTalla[4].Text);
                oBe.orprc_talla6 = Convert.ToInt32((textoTalla[5].Text == "") ? null : textoTalla[5].Text);
                oBe.orprc_talla7 = Convert.ToInt32((textoTalla[6].Text == "") ? null : textoTalla[6].Text);
                oBe.orprc_talla8 = Convert.ToInt32((textoTalla[7].Text == "") ? null : textoTalla[7].Text);
                oBe.orprc_talla9 = Convert.ToInt32((textoTalla[8].Text == "") ? null : textoTalla[8].Text);
                oBe.orprc_talla10 = Convert.ToInt32((textoTalla[9].Text == "") ? null : textoTalla[9].Text);
                oBe.orprc_cant_talla1 = Convert.ToInt32((textoCantidad[0].Text == "") ? null : textoCantidad[0].Text);
                oBe.orprc_cant_talla2 = Convert.ToInt32((textoCantidad[1].Text == "") ? null : textoCantidad[1].Text);
                oBe.orprc_cant_talla3 = Convert.ToInt32((textoCantidad[2].Text == "") ? null : textoCantidad[2].Text);
                oBe.orprc_cant_talla4 = Convert.ToInt32((textoCantidad[3].Text == "") ? null : textoCantidad[3].Text);
                oBe.orprc_cant_talla5 = Convert.ToInt32((textoCantidad[4].Text == "") ? null : textoCantidad[4].Text);
                oBe.orprc_cant_talla6 = Convert.ToInt32((textoCantidad[5].Text == "") ? null : textoCantidad[5].Text);
                oBe.orprc_cant_talla7 = Convert.ToInt32((textoCantidad[6].Text == "") ? null : textoCantidad[6].Text);
                oBe.orprc_cant_talla8 = Convert.ToInt32((textoCantidad[7].Text == "") ? null : textoCantidad[7].Text);
                oBe.orprc_cant_talla9 = Convert.ToInt32((textoCantidad[8].Text == "") ? null : textoCantidad[8].Text);
                oBe.orprc_cant_talla10 = Convert.ToInt32((textoCantidad[9].Text == "") ? null : textoCantidad[9].Text);
                oBe.orprc_icod_producto1 = icodProducto[0];
                oBe.orprc_icod_producto2 = icodProducto[1];
                oBe.orprc_icod_producto3 = icodProducto[2];
                oBe.orprc_icod_producto4 = icodProducto[3];
                oBe.orprc_icod_producto5 = icodProducto[4];
                oBe.orprc_icod_producto6 = icodProducto[5];
                oBe.orprc_icod_producto7 = icodProducto[6];
                oBe.orprc_icod_producto8 = icodProducto[7];
                oBe.orprc_icod_producto9 = icodProducto[8];
                oBe.orprc_icod_producto10 = icodProducto[9];
                oBe.orprc_cant_aprocesar1 = Convert.ToInt32((txtProcesar1.Text == "") ? null : txtProcesar1.Text);
                oBe.orprc_cant_aprocesar2 = Convert.ToInt32((txtProcesar2.Text == "") ? null : txtProcesar2.Text);
                oBe.orprc_cant_aprocesar3 = Convert.ToInt32((txtProcesar3.Text == "") ? null : txtProcesar3.Text);
                oBe.orprc_cant_aprocesar4 = Convert.ToInt32((txtProcesar4.Text == "") ? null : txtProcesar4.Text);
                oBe.orprc_cant_aprocesar5 = Convert.ToInt32((txtProcesar5.Text == "") ? null : txtProcesar5.Text);
                oBe.orprc_cant_aprocesar6 = Convert.ToInt32((txtProcesar6.Text == "") ? null : txtProcesar6.Text);
                oBe.orprc_cant_aprocesar7 = Convert.ToInt32((txtProcesar7.Text == "") ? null : txtProcesar7.Text);
                oBe.orprc_cant_aprocesar8 = Convert.ToInt32((txtProcesar8.Text == "") ? null : txtProcesar8.Text);
                oBe.orprc_cant_aprocesar9 = Convert.ToInt32((txtProcesar9.Text == "") ? null : txtProcesar9.Text);
                oBe.orprc_cant_aprocesar10 = Convert.ToInt32((txtProcesar10.Text == "") ? null : txtProcesar10.Text);
                oBe.orprc_ntotal = Convert.ToInt32(txtTotal.Text);
                oBe.orprc_ndocenas = Convert.ToDecimal(txtDocenas.Text);
                oBe.orprc_ilinea = Convert.ToInt32(lkpLinea.EditValue);
                oBe.orprc_vlinea = lkpLinea.Text;
                oBe.orprc_iresponsable = Convert.ToInt32(lkpResponsable.EditValue);
                oBe.orprc_vresponsable = lkpResponsable.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.vpc = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.orprc_iid_situacion = Parametros.intSitReporteProduccionGenerado;
                oBe.orprc_isituacion = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.strsituacion = lkpSituacion.Text;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.orprc_iid_orden_produccion = new BCentral().InsertarOrdenProduccion(oBe, lstOrdenProduccionDetalle, lstOrdenProduccionAreas);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BCentral().ModificarOrdenProduccion(oBe, lstOrdenProduccionDetalle, lstOrdenProduccionAreas, lstDelete);
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
                    this.MiEvento(oBe.orprc_iid_orden_produccion);
                    this.Close();
                }
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
        public List<EProdTablaRegistro> lstLinea = new List<EProdTablaRegistro>();
        private void frmManteSeries_Load(object sender, EventArgs e)
        {
            EProdTablaRegistro ob = new EProdTablaRegistro();
            ob.iid_tipo_tabla = 11;
            BSControls.LoaderLook(LkpMarca, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            ob.iid_tipo_tabla = 8;
            BSControls.LoaderLook(LkpColor, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            BSControls.LoaderLook(lkpSerie, new BCentral().ListarRegistroSerie(), "resec_vdescripcion", "resec_iid_registro_serie", true);
            ob.iid_tipo_tabla = 2;
            BSControls.LoaderLook(lkpTipo, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            ob.iid_tipo_tabla = 3;
            lstLinea = new BCentral().ListarProdTablaRegistro(ob);
            BSControls.LoaderLook(lkpLinea, lstLinea, "descripcion", "icod_tabla", true);
            setFecha(dteFecha);
            txtTotal.Enabled = false;
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(95), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpResponsable, new BCompras().ListarProveedorComboOPE(), "proc_responsable_produccion", "iid_icod_proveedor", true);
            lkpSituacion.Enabled = false;
            btnFichaTecnica.Enabled = false;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                LkpMarca.EditValue = oBe.orprc_imarca;
                LkpColor.EditValue = oBe.orprc_icolor;
                lkpSerie.EditValue = oBe.orprc_iserie;
                lkpLinea.EditValue = oBe.orprc_ilinea;
                lkpTipo.EditValue = oBe.orprc_itipo;
                lkpSituacion.EditValue = oBe.orprc_isituacion;
                lkpResponsable.EditValue = oBe.orprc_iresponsable;
            }
            Personal.Enabled = !FromAsignacion;
        }
        public void getCorrelativo()
        {
            //var lst = new BAdministracionSistema().listarParametroProduccion();

            //txtPedido.Text = (Convert.ToInt32(lst[0].pmprc_inumero_orden_pedido) + 1).ToString();
            //txtFichaT.Text = (Convert.ToInt32(lst[0].pmprc_inumero_ficha_tecnica) + 1).ToString();
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

            using (frmManteOrdenProduccionDetalle frm = new frmManteOrdenProduccionDetalle())
            {
                frm.SetInsert();

                frm.lstOrdenProduccionDetalle = lstOrdenProduccionDetalle;
                frm.txtitem.Text = (lstOrdenProduccionDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstOrdenProduccionDetalle.Count + 1);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstOrdenProduccionDetalle = frm.lstOrdenProduccionDetalle;
                    grdMateriales.DataSource = lstOrdenProduccionDetalle;
                    viewMateriales.RefreshData();
                    viewMateriales.MoveLast();
                }
            }

        }

        private void btnmodelo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            List<EProdTablaRegistro> listRegistro = new List<EProdTablaRegistro>();
            EProdTablaRegistro ob = new EProdTablaRegistro();
            LkpColor.EditValue = "0";
            EProdModelo obje = new EProdModelo();
            ob.iid_tipo_tabla = 11;
            listRegistro = new BCentral().ListarProdTablaRegistro(ob).Where(obj => obj.tarec_viid_correlativo == LkpMarca.EditValue.ToString()).ToList();
            listRegistro.ForEach(obe =>
            {
                obje.tarec_icorrelativo = obe.icod_tabla;
            });
            using (ListarModelo listmodelo = new ListarModelo())
            {
                listmodelo.CodeMarcas = Convert.ToInt32(obje.tarec_icorrelativo);
                if (listmodelo.ShowDialog() == DialogResult.OK)
                {
                    btnmodelo.Tag = string.Format("{0:0000}", listmodelo._Be.mo_iid_modelo);
                    btnmodelo.Text = listmodelo._Be.mo_vdescripcion;

                }
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {

        }

        private void btncodigoProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //using (FrmListarProducto frm = new FrmListarProducto())
            //{
            //    frm.indicador_OP = 1;
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        btncodigoProducto.Tag = frm._Be.pr_icod_producto;
            //        btncodigoProducto.Text = frm._Be.pr_vcodigo_externo.Substring(0, frm._Be.pr_vcodigo_externo.Length - 2);
            //        txtdescripcion.Text = frm._Be.pr_vdescripcion_producto;

            //        //LkpMarca.EditValue = frm._Be.pr_iid_marca;
            //        //LkpMarca.Text = frm._Be.pr_viid_marca;
            //        //btnmodelo.Tag = frm._Be.pr_iid_modelo;
            //        //btnmodelo.Text = frm._Be.pr_viid_modelo;
            //        //LkpColor.EditValue = frm._Be.pr_iid_color;
            //        //LkpColor.Text = frm._Be.pr_viid_color;

            //    }
            //}
        }
        public int?[] icodProducto = new int?[10];
        private void btnGenerar_Click_1(object sender, EventArgs e)
        {
            List<ERegistroSerie> lstSerie = new BCentral().ListarRegistroSerie().Where(x => x.resec_iid_registro_serie == Convert.ToInt32(lkpSerie.EditValue)).ToList();


            if (lstSerie[0].resec_vtalla_inicial != "0" && lstSerie[0].resec_vtalla_final != "0")
            {
                List<EProdProducto> oProducto = new List<EProdProducto>();



                int i = -1;
                TextEdit[] textoCantidad = GetTextoCantidad();
                TextEdit[] textoTalla = GetTextoTalla();
                for (int x = Convert.ToInt32(lstSerie[0].resec_vtalla_inicial); x <= Convert.ToInt32(lstSerie[0].resec_vtalla_final); x++)
                {

                    i++;
                    textoTalla[i].Text = string.Format("{0:00}", x).ToString();
                    textoCantidad[i].Text = "0";

                    string codigoexterno = btncodigoProducto.Text + textoTalla[i].Text;
                    oProducto = new BCentral().VerificarProductoPvt(codigoexterno);

                    if (oProducto.Count > 0)
                    {

                        textoCantidad[i].Enabled = true;
                        icodProducto[i] = oProducto[0].pr_icod_producto;
                    }
                    else
                        textoCantidad[i].Enabled = false;


                }

                btncodigoProducto.Enabled = false;
                lkpSerie.Enabled = false;
                btnGenerar.Enabled = false;
                LkpMarca.Enabled = false;
                btnmodelo.Enabled = false; LkpColor.Enabled = false;
                lkpTipo.Enabled = false;
                btnPedido.Enabled = false;
                btnItemPedido.Enabled = false;
                btnFichaTecnica.Enabled = false;
                lkpLinea.Enabled = false;
                lkpResponsable.Enabled = false;

                txtcantidad1.Enabled = Convert.ToInt32(txtProcesar1.Text) > 0;
                txtcantidad2.Enabled = Convert.ToInt32(txtProcesar2.Text) > 0;
                txtcantidad3.Enabled = Convert.ToInt32(txtProcesar3.Text) > 0;
                txtcantidad4.Enabled = Convert.ToInt32(txtProcesar4.Text) > 0;
                txtcantidad5.Enabled = Convert.ToInt32(txtProcesar5.Text) > 0;
                txtcantidad6.Enabled = Convert.ToInt32(txtProcesar6.Text) > 0;
                txtcantidad7.Enabled = Convert.ToInt32(txtProcesar7.Text) > 0;
                txtcantidad8.Enabled = Convert.ToInt32(txtProcesar8.Text) > 0;
                txtcantidad9.Enabled = Convert.ToInt32(txtProcesar9.Text) > 0;
                txtcantidad10.Enabled = Convert.ToInt32(txtProcesar10.Text) > 0;


            }
            else
            {
                XtraMessageBox.Show("Ingrese Serie", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private TextEdit[] GetTextoCantidad()
        {
            TextEdit[] texto = new TextEdit[] { txtcantidad1, txtcantidad2, txtcantidad3, txtcantidad4,
                            txtcantidad5, txtcantidad6, txtcantidad7, txtcantidad8, txtcantidad9, txtcantidad10};
            return texto;
        }

        private TextEdit[] GetTextoTalla()
        {
            TextEdit[] texto = new TextEdit[] { txttalla1, txttalla2, txttalla3, txttalla4,
                            txttalla5, txttalla6, txttalla7, txttalla8, txttalla9, txttalla10};
            return texto;
        }

        private void btnmodelo_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            List<EProdTablaRegistro> listRegistro = new List<EProdTablaRegistro>();
            EProdTablaRegistro ob = new EProdTablaRegistro();
            LkpColor.EditValue = "0";
            EProdModelo obje = new EProdModelo();
            ob.iid_tipo_tabla = 11;
            listRegistro = new BCentral().ListarProdTablaRegistro(ob).Where(obj => obj.icod_tabla == Convert.ToInt32(LkpMarca.EditValue)).ToList();
            listRegistro.ForEach(obe =>
            {
                obje.tarec_icorrelativo = obe.icod_tabla;
            });
            using (ListarModelo listmodelo = new ListarModelo())
            {
                listmodelo.CodeMarcas = Convert.ToInt32(obje.tarec_icorrelativo);
                if (listmodelo.ShowDialog() == DialogResult.OK)
                {
                    btnmodelo.Tag = string.Format("{0:0000}", listmodelo._Be.mo_iid_modelo);
                    btnmodelo.Text = listmodelo._Be.mo_vdescripcion;
                }
            }
        }

        private void txtTotal_EditValueChanged(object sender, EventArgs e)
        {

        }
        private void Totalizar()
        {
            int Suma = 0;
            Suma = Convert.ToInt32(txtcantidad1.Text = (txtcantidad1.Text == "") ? "0" : txtcantidad1.Text) +
            Convert.ToInt32(txtcantidad2.Text = (txtcantidad2.Text == "") ? "0" : txtcantidad2.Text) +
            Convert.ToInt32(txtcantidad3.Text = (txtcantidad3.Text == "") ? "0" : txtcantidad3.Text) +
            Convert.ToInt32(txtcantidad4.Text = (txtcantidad4.Text == "") ? "0" : txtcantidad4.Text) +
            Convert.ToInt32(txtcantidad5.Text = (txtcantidad5.Text == "") ? "0" : txtcantidad5.Text) +
            Convert.ToInt32(txtcantidad6.Text = (txtcantidad6.Text == "") ? "0" : txtcantidad6.Text) +
            Convert.ToInt32(txtcantidad7.Text = (txtcantidad7.Text == "") ? "0" : txtcantidad7.Text) +
            Convert.ToInt32(txtcantidad8.Text = (txtcantidad8.Text == "") ? "0" : txtcantidad8.Text) +
            Convert.ToInt32(txtcantidad9.Text = (txtcantidad9.Text == "") ? "0" : txtcantidad9.Text) +
            Convert.ToInt32(txtcantidad10.Text = (txtcantidad10.Text == "") ? "0" : txtcantidad10.Text);
            txtTotal.Text = Suma.ToString();
            txtDocenas.Text = (Convert.ToDecimal(txtTotal.Text) / 12).ToString();
        }

        private void txtcantidad1_EditValueChanged(object sender, EventArgs e)
        {

            //if (Convert.ToInt32(txtcantidad1.Text) > Convert.ToInt32(txtProcesar1.Text))
            //{

            //    XtraMessageBox.Show("La Cantidad deber menor o igual que Por Procesar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            Totalizar();
        }

        private void txtcantidad2_EditValueChanged(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(txtcantidad2.Text) > Convert.ToInt32(txtProcesar2.Text))
            //{
            //    XtraMessageBox.Show("La Cantidad deber menor o igual que Por Procesar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            Totalizar();
        }

        private void txtcantidad3_EditValueChanged(object sender, EventArgs e)
        {

            //if (Convert.ToInt32(txtcantidad3.Text) > Convert.ToInt32(txtProcesar3.Text))
            //{
            //    XtraMessageBox.Show("La Cantidad deber menor o igual que Por Procesar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            Totalizar();
        }

        private void txtcantidad4_EditValueChanged(object sender, EventArgs e)
        {

            //if (Convert.ToInt32(txtcantidad4.Text) > Convert.ToInt32(txtProcesar4.Text))
            //{
            //    XtraMessageBox.Show("La Cantidad deber menor o igual que Por Procesar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            Totalizar();
        }

        private void txtcantidad5_EditValueChanged(object sender, EventArgs e)
        {

            //if (Convert.ToInt32(txtcantidad5.Text) > Convert.ToInt32(txtProcesar5.Text))
            //{
            //    XtraMessageBox.Show("La Cantidad deber menor o igual que Por Procesar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            Totalizar();
        }

        private void txtcantidad6_EditValueChanged(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(txtcantidad6.Text) > Convert.ToInt32(txtProcesar6.Text))
            //{
            //    XtraMessageBox.Show("La Cantidad deber menor o igual que Por Procesar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            Totalizar();
        }

        private void txtcantidad7_EditValueChanged(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(txtcantidad7.Text) > Convert.ToInt32(txtProcesar7.Text))
            //{
            //    XtraMessageBox.Show("La Cantidad deber menor o igual que Por Procesar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            Totalizar();
        }

        private void txtcantidad8_EditValueChanged(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(txtcantidad8.Text) > Convert.ToInt32(txtProcesar8.Text))
            //{
            //    XtraMessageBox.Show("La Cantidad deber menor o igual que Por Procesar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            Totalizar();
        }

        private void txtcantidad9_EditValueChanged(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(txtcantidad9.Text) > Convert.ToInt32(txtProcesar9.Text))
            //{
            //    XtraMessageBox.Show("La Cantidad deber menor o igual que Por Procesar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            Totalizar();
        }

        private void txtcantidad10_EditValueChanged(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(txtcantidad10.Text) > Convert.ToInt32(txtProcesar10.Text))
            //{
            //    XtraMessageBox.Show("La Cantidad deber menor o igual que Por Procesar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            Totalizar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EOrdenProduccionDet obe = (EOrdenProduccionDet)viewMateriales.GetRow(viewMateriales.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstOrdenProduccionDetalle.Remove(obe);
            viewMateriales.RefreshData();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificarItem();
        }
        private void modificarItem()
        {
            EOrdenProduccionDet obe = (EOrdenProduccionDet)viewMateriales.GetRow(viewMateriales.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteOrdenProduccionDetalle frm = new frmManteOrdenProduccionDetalle())
            {
                frm.obe = obe;
                frm.lstOrdenProduccionDetalle = lstOrdenProduccionDetalle;
                frm.SetModify();
                frm.txtitem.Text = String.Format("{0:000}", obe.orprd_iitem_orden_produccion);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstOrdenProduccionDetalle = frm.lstOrdenProduccionDetalle;
                    viewMateriales.RefreshData();
                    viewMateriales.MoveLast();
                }
            }
        }

        private void btncodigoProducto_Click(object sender, EventArgs e)
        {
            using (FrmListarProducto frm = new FrmListarProducto())
            {

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btncodigoProducto.Tag = frm._Be.pr_icod_producto;
                    btncodigoProducto.Text = frm._Be.pr_vcodigo_externo.Substring(0, frm._Be.pr_vcodigo_externo.Length - 2);
                    txtdescripcion.Text = frm._Be.pr_vdescripcion_producto.Substring(0, frm._Be.pr_vdescripcion_producto.Length - 3);
                    if (string.IsNullOrEmpty(btnPedido.Text) && string.IsNullOrEmpty(btnItemPedido.Text))
                    {
                        btnmodelo.Tag = frm._Be.pr_iid_modelo;
                        btnmodelo.Text = frm._Be.pr_viid_modelo;
                        LkpMarca.EditValue = frm._Be.icodmarca;
                        LkpColor.EditValue = frm._Be.icodcolor;
                        LkpMarca.Properties.ReadOnly = true;
                        LkpMarca.Enabled = false;
                        LkpColor.Properties.ReadOnly = true;
                        LkpColor.Enabled = false;
                        btnFichaTecnica.Enabled = true;
                    }


                }
            }
        }

        private void btnPedido_Click(object sender, EventArgs e)
        {
            try
            {
                using (ListarOrdenPedido frm = new ListarOrdenPedido())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnPedido.Tag = frm._Be.orpec_iid_orden_pedido;
                        btnPedido.Text = frm._Be.orpec_icod_orden_pedido;
                    }
                }
                btnPedido.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public int?[] icodProducto1 = new int?[10];
        private void btnItemPedido_Click(object sender, EventArgs e)
        {

            using (ListarOrdenPedidoDetalle frm = new ListarOrdenPedidoDetalle())
            {
                frm.icod = Convert.ToInt32(btnPedido.Tag);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnItemPedido.Tag = frm._Be.orped_icod_item_orden_pedido;
                    btnItemPedido.Text = frm._Be.orped_iitem_orden_pedido.ToString();
                    btnFichaTecnica.Tag = frm._Be.orped_ificha_tecnica;
                    btnFichaTecnica.Text = frm._Be.strfichatecnica;
                    LkpMarca.EditValue = frm._Be.orped_imarca;
                    btnmodelo.Tag = frm._Be.orped_imodelo;
                    btnmodelo.Text = frm._Be.strmodelo;
                    lkpTipo.EditValue = frm._Be.orped_itipo;
                    LkpColor.EditValue = frm._Be.orped_icolor;
                    lkpSerie.EditValue = frm._Be.orped_serie;
                    lkpLinea.EditValue = frm._Be.fitec_ilinea;
                    lkpResponsable.EditValue = frm._Be.orped_iresponsable;
                    txtProcesar1.Text = frm._Be.orped_cant_talla1.ToString();
                    txtProcesar2.Text = frm._Be.orped_cant_talla2.ToString();
                    txtProcesar3.Text = frm._Be.orped_cant_talla3.ToString();
                    txtProcesar4.Text = frm._Be.orped_cant_talla4.ToString();
                    txtProcesar5.Text = frm._Be.orped_cant_talla5.ToString();
                    txtProcesar6.Text = frm._Be.orped_cant_talla6.ToString();
                    txtProcesar7.Text = frm._Be.orped_cant_talla7.ToString();
                    txtProcesar8.Text = frm._Be.orped_cant_talla8.ToString();
                    txtProcesar9.Text = frm._Be.orped_cant_talla9.ToString();
                    txtProcesar10.Text = frm._Be.orped_cant_talla10.ToString();
                    var modeloSelect = new BCentral().ModeloGetById(Convert.ToInt32(btnmodelo.Tag));
                    lkpLinea.EditValue = lstLinea.Where(x => x.tarec_iid_correlativo == modeloSelect.pr_iid_linea).FirstOrDefault().icod_tabla;
                    lkpLinea.Enabled = false;

                    var list = new BCentral().ListarProdProductosIndexes("", "", frm._Be.strmodelo);
                    var _obj = list.FirstOrDefault();
                    if (_obj != null)
                    {
                        btncodigoProducto.Text = _obj.pr_vcodigo_externo.Substring(0, _obj.pr_vcodigo_externo.Length - 2);
                        btncodigoProducto.Tag = _obj.pr_icod_producto;
                        txtdescripcion.Text = _obj.pr_vdescripcion_producto.Substring(0, _obj.pr_vdescripcion_producto.Length - 2);
                    }


                    if (Convert.ToInt32(btnFichaTecnica.Tag) != 0)
                        lstMaterial = new BCentral().ListarFichaTecnicaDet(Convert.ToInt32(btnFichaTecnica.Tag));
                    else
                        XtraMessageBox.Show("Item del la Orden de Pedido no tiene Ficha Técnica", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    foreach (var objd in lstMaterial)
                    {
                        EOrdenProduccionDet op = new EOrdenProduccionDet();
                        op.orprd_iitem_orden_produccion = objd.fited_iitem_ficha_tecnica;
                        op.orprd_iproceso = objd.fited_iarea;
                        op.strproceso = objd.strarea;
                        op.orprd_iubicacion = objd.fited_iubicacion;
                        op.orprd_vubicacion = objd.strubicacion;
                        op.orprd_vmaterial = objd.fited_vdescripcion == "" ? objd.strProducto : objd.fited_vdescripcion;
                        op.prdc_icod_producto = objd.prdc_icod_producto;
                        op.strCodeProducto = objd.strCodeProducto;
                        op.strProducto = objd.strProducto;
                        op.strmedida = objd.strUnidadMedida;
                        op.orprd_ncantidad = objd.fited_ncantidad;
                        op.orprd_imedida = objd.fited_imedida;
                        op.intTipoOperacion = 1;
                        lstOrdenProduccionDetalle.Add(op);
                    }
                    btnGenerar_Click_1(sender, e);

                    grdMateriales.DataSource = lstOrdenProduccionDetalle;
                    viewMateriales.RefreshData();
                    GenerarAreasproduccion();
                }
                btnItemPedido.Enabled = false;
                btnFichaTecnica.Enabled = false;
            }
        }

        private void btnFichaTecnica_Click(object sender, EventArgs e)
        {
            try
            {
                using (ListarFichaTecnica frm = new ListarFichaTecnica())
                {
                    frm.txtMarca.Text = LkpMarca.Text;
                    frm.txtColor.Text = LkpColor.Text;
                    frm.txtModelo.Text = btnmodelo.Text;
                    frm.BuscarCriterio();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnFichaTecnica.Tag = frm._Be.fitec_iid_ficha_tecnica;
                        btnFichaTecnica.Text = frm._Be.fitec_icod_ficha_tecnica + "-" + frm._Be.fitec_icorrelativo_contramuestra;
                        if (string.IsNullOrEmpty(btnPedido.Text) && string.IsNullOrEmpty(btnItemPedido.Text))
                        {
                            lkpTipo.EditValue = frm._Be.fitec_itipo;
                            lkpLinea.EditValue = frm._Be.fitec_ilinea;
                            lstMaterial = new BCentral().ListarFichaTecnicaDet(Convert.ToInt32(btnFichaTecnica.Tag));
                            foreach (var objd in lstMaterial)
                            {
                                EOrdenProduccionDet op = new EOrdenProduccionDet();
                                op.orprd_iitem_orden_produccion = objd.fited_iitem_ficha_tecnica;
                                op.orprd_iproceso = objd.fited_iarea;
                                op.strproceso = objd.strarea;
                                op.orprd_iubicacion = objd.fited_iubicacion;
                                op.orprd_vubicacion = objd.strubicacion;
                                op.orprd_vmaterial = objd.fited_vdescripcion == "" ? objd.strProducto : objd.fited_vdescripcion;
                                op.prdc_icod_producto = objd.prdc_icod_producto;
                                op.strCodeProducto = objd.strCodeProducto;
                                op.strProducto = objd.strProducto;
                                op.strmedida = objd.strUnidadMedida;
                                op.orprd_ncantidad = objd.fited_ncantidad;
                                op.orprd_imedida = objd.fited_imedida;
                                op.intTipoOperacion = 1;
                                lstOrdenProduccionDetalle.Add(op);
                            }
                            grdMateriales.DataSource = lstOrdenProduccionDetalle;
                            viewMateriales.RefreshData();
                        }
                        btnmodelo.Tag = frm._Be.fitec_imodelo;
                        btnmodelo.Text = frm._Be.strmodelo;
                        LkpMarca.EditValue = frm._Be.fitec_imarca;
                        LkpMarca.Text = frm._Be.strmarca;
                        LkpColor.EditValue = frm._Be.fitec_icolor;
                        LkpColor.Text = frm._Be.strcolor;
                        lkpTipo.EditValue = frm._Be.fitec_itipo;
                        lkpTipo.Text = frm._Be.strtipo;
                        lkpSerie.EditValue = frm._Be.fitec_iserie;
                        lkpSerie.Text = frm._Be.strserie;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EOrdenProduccionAreas obe = (EOrdenProduccionAreas)viewPersonal.GetRow(viewPersonal.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteOrdenProduccionAreas frm = new frmManteOrdenProduccionAreas())
            {
                frm.Text = $"Orden de Producción {txtNrOrden.Text} - {obe.strproceso}";
                frm.obe = obe;
                frm.FromAsignacion = FromAsignacion;
                frm.lstOrdenProduccionAreas = lstOrdenProduccionAreas;
                frm.FromAsignacion = FromAsignacion;
                frm.SetModify();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstOrdenProduccionAreas = frm.lstOrdenProduccionAreas;
                    viewPersonal.RefreshData();

                }
            }
        }
    }
}