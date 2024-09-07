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
    public partial class frmManteRequerimientoMaterial : XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteRequerimientoMaterial));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public ERequerimientoMaterial oBe = new ERequerimientoMaterial();
        public List<ERequerimientoMaterial> lstRequerimientoMaterial = new List<ERequerimientoMaterial>();
        List<ERequerimientoMaterialDet> lstRequerimientoMaterialDetalle = new List<ERequerimientoMaterialDet>();
        List<ERequerimientoMaterialDet> lstDelete = new List<ERequerimientoMaterialDet>();
        List<EOrdenProduccionDet> lstMaterial = new List<EOrdenProduccionDet>();
        public frmManteRequerimientoMaterial()
        {
            InitializeComponent();
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
            btnOperario.Enabled = !Enabled;
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
                txtNºRM.Enabled = Enabled;
                dteFecha.Enabled = Enabled;
                btnPedido.Enabled = Enabled;
                btnFichaTecnica.Enabled = Enabled;
                LkpColor.Enabled = Enabled;
                LkpMarca.Enabled = Enabled;
                lkpTipo.Enabled = Enabled;
                btnmodelo.Enabled = Enabled;
                lkpSerie.Enabled = Enabled;
                btncodigoProducto.Enabled = Enabled;
                btnItemPedido.Enabled = Enabled;
                lkpLinea.Enabled = Enabled;
                lkpProceso.Enabled = false;
            }

        }
        public void setValues()
        {
            txtNºRM.Text = oBe.rqmac_icod_requerimiento_material;
            dteFecha.EditValue = oBe.rqmac_sfecha_requerimiento_material;
            btnOrdenProduccion.Tag = oBe.rqmac_iorden_produccion;
            btnOrdenProduccion.Text = oBe.strordenproduccion;
            lkpProceso.EditValue = oBe.rqmac_iproceso;
            lkpProceso.Text = oBe.strproceso;
            btnPedido.Tag = oBe.rqmac_ipedido;
            btnPedido.Text = oBe.strpedido;
            btnItemPedido.Tag = oBe.rqmac_iitem_pedido;
            btnItemPedido.Text = oBe.stritempedido.ToString();
            btnFichaTecnica.Tag = oBe.rqmac_ificha_tecnica;
            btnFichaTecnica.Text = oBe.strfichatecnica;
            btncodigoProducto.Tag = oBe.rqmac_vcodigo_producto;
            btncodigoProducto.Text = oBe.rqmac_vcodigo_producto;
            txtdescripcion.Text = oBe.rqmac_vdescripcion_producto;
            LkpMarca.EditValue = oBe.rqmac_imarca;
            btnmodelo.Tag = oBe.rqmac_imodelo;
            btnmodelo.Text = oBe.DesModelo;
            LkpColor.EditValue = oBe.rqmac_icolor;
            lkpTipo.EditValue = oBe.rqmac_itipo;
            lkpSerie.EditValue = oBe.rqmac_iserie;
            txttalla1.Text = oBe.rqmac_talla1.ToString();
            txttalla2.Text = oBe.rqmac_talla2.ToString();
            txttalla3.Text = oBe.rqmac_talla3.ToString();
            txttalla4.Text = oBe.rqmac_talla4.ToString();
            txttalla5.Text = oBe.rqmac_talla5.ToString();
            txttalla6.Text = oBe.rqmac_talla6.ToString();
            txttalla7.Text = oBe.rqmac_talla7.ToString();
            txttalla8.Text = oBe.rqmac_talla8.ToString();
            txttalla9.Text = oBe.rqmac_talla9.ToString();
            txttalla10.Text = oBe.rqmac_talla10.ToString();
            txtcantidad1.Text = oBe.rqmac_cant_talla1.ToString();
            txtcantidad2.Text = oBe.rqmac_cant_talla2.ToString();
            txtcantidad3.Text = oBe.rqmac_cant_talla3.ToString();
            txtcantidad4.Text = oBe.rqmac_cant_talla4.ToString();
            txtcantidad5.Text = oBe.rqmac_cant_talla5.ToString();
            txtcantidad6.Text = oBe.rqmac_cant_talla6.ToString();
            txtcantidad7.Text = oBe.rqmac_cant_talla7.ToString();
            txtcantidad8.Text = oBe.rqmac_cant_talla8.ToString();
            txtcantidad9.Text = oBe.rqmac_cant_talla9.ToString();
            txtcantidad10.Text = oBe.rqmac_cant_talla10.ToString();
            icodProducto[0] = oBe.rqmac_icod_producto1;
            icodProducto[1] = oBe.rqmac_icod_producto2;
            icodProducto[2] = oBe.rqmac_icod_producto3;
            icodProducto[3] = oBe.rqmac_icod_producto4;
            icodProducto[4] = oBe.rqmac_icod_producto5;
            icodProducto[5] = oBe.rqmac_icod_producto6;
            icodProducto[6] = oBe.rqmac_icod_producto7;
            icodProducto[7] = oBe.rqmac_icod_producto8;
            icodProducto[8] = oBe.rqmac_icod_producto9;
            icodProducto[9] = oBe.rqmac_icod_producto10;
            txtTotal.Text = oBe.rqmac_ntotal.ToString();
            txtDocenas.Text = oBe.rqmac_ndocenas.ToString();
            lkpLinea.EditValue = oBe.rqmac_ilinea;
            lkpLinea.Text = oBe.strlinea;
            btnOperario.Tag = oBe.rqmac_ioperario;
            btnOperario.Text = oBe.stroperario;
            lkpSituacion.EditValue = oBe.rqmac_isituacion;
            lkpAlmacen.EditValue = oBe.rqmac_iid_almacen;
            lkpAlmacen.Text = oBe.stralmacen;
            lstRequerimientoMaterialDetalle = new BCentral().listarRequerimientoMaterialDetalle(oBe.rqmac_iid_requerimiento_material);
            grdMateriales.DataSource = lstRequerimientoMaterialDetalle;
            viewMateriales.RefreshData();
            FiltarComboProceso();

        }

        public void FiltarComboProceso()
        {


            var materialesRef = new BCentral().listarOrdenPrduccionDetalle(Convert.ToInt32(btnOrdenProduccion.Tag));
            var listaProcesos = new BCentral().listarAreaProduccion().Where(x => materialesRef.Exists(y => x.arprc_iid_codigo == y.orprd_iproceso)).ToList();
            BSControls.LoaderLook(lkpProceso, listaProcesos, "arprc_vdescripcion", "arprc_iid_codigo", false);
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

            setValues();
        }
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                if (string.IsNullOrEmpty(txtNºRM.Text))
                {
                    oBase = txtNºRM;
                    throw new ArgumentException("Ingrese el NUmero de Requerimiento");
                }

                if (string.IsNullOrEmpty(Convert.ToString(btnPedido.Tag)))
                {
                    oBase = btnPedido;
                    throw new ArgumentException("Seleccionar Pedido");
                }
                if (string.IsNullOrEmpty(Convert.ToString(btnItemPedido.Tag)))
                {
                    oBase = btnItemPedido;
                    throw new ArgumentException("Seleccionar Item Pedido");
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

                if (lstRequerimientoMaterial.Exists(x => x.rqmac_iorden_produccion == Convert.ToInt32(btnOrdenProduccion.Tag) && x.rqmac_iproceso == Convert.ToInt32(lkpProceso.EditValue)))
                {
                    throw new ArgumentException("Ya existe Requerimiento Material con la misma Orden de Producción y el mismo proceso");
                }

                TextEdit[] textoCantidad = GetTextoCantidad();
                TextEdit[] textoTalla = GetTextoTalla();

                oBe.rqmac_icod_requerimiento_material = txtNºRM.Text;
                oBe.rqmac_sfecha_requerimiento_material = Convert.ToDateTime(dteFecha.Text);
                oBe.rqmac_iorden_produccion = Convert.ToInt32(btnOrdenProduccion.Tag);
                oBe.strordenproduccion = btnOrdenProduccion.Text;
                oBe.rqmac_iproceso = Convert.ToInt32(lkpProceso.EditValue);
                oBe.strproceso = lkpProceso.Text;
                oBe.rqmac_ipedido = Convert.ToInt32(btnPedido.Tag);
                oBe.strpedido = btnPedido.Text;
                oBe.rqmac_iitem_pedido = Convert.ToInt32(btnItemPedido.Tag);
                oBe.stritempedido = Convert.ToInt32(btnItemPedido.Text);
                oBe.pedido = "P-" + oBe.strpedido + "." + oBe.stritempedido;
                oBe.rqmac_ificha_tecnica = Convert.ToInt32(btnFichaTecnica.Tag);
                oBe.strfichatecnica = btnFichaTecnica.Text;
                oBe.rqmac_vcodigo_producto = btncodigoProducto.Text;
                oBe.rqmac_vdescripcion_producto = txtdescripcion.Text;
                oBe.rqmac_imarca = Convert.ToInt32(LkpMarca.EditValue);
                oBe.rqmac_imodelo = Convert.ToInt32(btnmodelo.Tag);
                oBe.rqmac_icolor = Convert.ToInt32(LkpColor.EditValue);
                oBe.rqmac_itipo = Convert.ToInt32(lkpTipo.EditValue);
                oBe.rqmac_iserie = Convert.ToInt32(lkpSerie.EditValue);
                oBe.rqmac_talla1 = Convert.ToInt32((textoTalla[0].Text == "") ? null : textoTalla[0].Text);
                oBe.rqmac_talla2 = Convert.ToInt32((textoTalla[1].Text == "") ? null : textoTalla[1].Text);
                oBe.rqmac_talla3 = Convert.ToInt32((textoTalla[2].Text == "") ? null : textoTalla[2].Text);
                oBe.rqmac_talla4 = Convert.ToInt32((textoTalla[3].Text == "") ? null : textoTalla[3].Text);
                oBe.rqmac_talla5 = Convert.ToInt32((textoTalla[4].Text == "") ? null : textoTalla[4].Text);
                oBe.rqmac_talla6 = Convert.ToInt32((textoTalla[5].Text == "") ? null : textoTalla[5].Text);
                oBe.rqmac_talla7 = Convert.ToInt32((textoTalla[6].Text == "") ? null : textoTalla[6].Text);
                oBe.rqmac_talla8 = Convert.ToInt32((textoTalla[7].Text == "") ? null : textoTalla[7].Text);
                oBe.rqmac_talla9 = Convert.ToInt32((textoTalla[8].Text == "") ? null : textoTalla[8].Text);
                oBe.rqmac_talla10 = Convert.ToInt32((textoTalla[9].Text == "") ? null : textoTalla[9].Text);
                oBe.rqmac_cant_talla1 = Convert.ToInt32((textoCantidad[0].Text == "") ? null : textoCantidad[0].Text);
                oBe.rqmac_cant_talla2 = Convert.ToInt32((textoCantidad[1].Text == "") ? null : textoCantidad[1].Text);
                oBe.rqmac_cant_talla3 = Convert.ToInt32((textoCantidad[2].Text == "") ? null : textoCantidad[2].Text);
                oBe.rqmac_cant_talla4 = Convert.ToInt32((textoCantidad[3].Text == "") ? null : textoCantidad[3].Text);
                oBe.rqmac_cant_talla5 = Convert.ToInt32((textoCantidad[4].Text == "") ? null : textoCantidad[4].Text);
                oBe.rqmac_cant_talla6 = Convert.ToInt32((textoCantidad[5].Text == "") ? null : textoCantidad[5].Text);
                oBe.rqmac_cant_talla7 = Convert.ToInt32((textoCantidad[6].Text == "") ? null : textoCantidad[6].Text);
                oBe.rqmac_cant_talla8 = Convert.ToInt32((textoCantidad[7].Text == "") ? null : textoCantidad[7].Text);
                oBe.rqmac_cant_talla9 = Convert.ToInt32((textoCantidad[8].Text == "") ? null : textoCantidad[8].Text);
                oBe.rqmac_cant_talla10 = Convert.ToInt32((textoCantidad[9].Text == "") ? null : textoCantidad[9].Text);
                oBe.rqmac_icod_producto1 = oBe.rqmac_icod_producto1;
                oBe.rqmac_icod_producto2 = oBe.rqmac_icod_producto2;
                oBe.rqmac_icod_producto3 = oBe.rqmac_icod_producto3;
                oBe.rqmac_icod_producto4 = oBe.rqmac_icod_producto4;
                oBe.rqmac_icod_producto5 = oBe.rqmac_icod_producto5;
                oBe.rqmac_icod_producto6 = oBe.rqmac_icod_producto6;
                oBe.rqmac_icod_producto7 = oBe.rqmac_icod_producto7;
                oBe.rqmac_icod_producto8 = oBe.rqmac_icod_producto8;
                oBe.rqmac_icod_producto9 = oBe.rqmac_icod_producto9;
                oBe.rqmac_icod_producto10 = oBe.rqmac_icod_producto10;
                oBe.rqmac_ntotal = Convert.ToInt32(txtTotal.Text);
                oBe.rqmac_ndocenas = Convert.ToDecimal(txtDocenas.Text);
                oBe.rqmac_ilinea = Convert.ToInt32(lkpLinea.EditValue);
                oBe.strlinea = lkpLinea.Text;
                oBe.rqmac_ioperario = Convert.ToInt32(btnOperario.Tag);
                oBe.stroperario = btnOperario.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.vpc = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.rqmac_iid_situacion = Parametros.intSitReporteProduccionGenerado;
                oBe.rqmac_isituacion = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.situacion = lkpSituacion.Text;
                oBe.rqmac_iid_almacen = Convert.ToInt32(lkpAlmacen.EditValue);
                oBe.stralmacen = lkpAlmacen.Text;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.rqmac_iid_requerimiento_material = new BCentral().InsertarRequerimientoMaterial(oBe, lstRequerimientoMaterialDetalle);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BCentral().ModificarRequerimientoMaterial(oBe, lstRequerimientoMaterialDetalle, lstDelete);
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
                    this.MiEvento(oBe.rqmac_iid_requerimiento_material);
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
            BSControls.LoaderLook(lkpLinea, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            setFecha(dteFecha);
            txtTotal.Enabled = false;
            ob.iid_tipo_tabla = 21;
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(93), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            //BSControls.LoaderLook(lkpSituacion, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true); anterior
            if (Status == BSMaintenanceStatus.CreateNew)
                BSControls.LoaderLook(lkpProceso, new BCentral().listarAreaProduccion(), "arprc_vdescripcion", "arprc_iid_codigo", false);
            BSControls.LoaderLook(lkpAlmacen, new BAlmacen().listarAlmacenes(), "almac_vdescripcion", "almac_icod_almacen", true);
            lkpSituacion.Enabled = false;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                LkpMarca.EditValue = oBe.rqmac_imarca;
                LkpColor.EditValue = oBe.rqmac_icolor;
                lkpSerie.EditValue = oBe.rqmac_iserie;
                lkpLinea.EditValue = oBe.rqmac_ilinea;
                lkpTipo.EditValue = oBe.rqmac_itipo;
                lkpSituacion.EditValue = oBe.rqmac_isituacion;
                lkpProceso.EditValue = oBe.rqmac_iproceso;
                lkpAlmacen.EditValue = oBe.rqmac_iid_almacen;
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

            using (frmManteRequerimientoMaterialDetalle frm = new frmManteRequerimientoMaterialDetalle())
            {
                frm.SetInsert();

                frm.lstRequerimientoMaterialDet = lstRequerimientoMaterialDetalle;
                frm.cod_proceso = Convert.ToInt32(lkpProceso.EditValue);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstRequerimientoMaterialDetalle = frm.lstRequerimientoMaterialDet;
                    grdMateriales.DataSource = lstRequerimientoMaterialDetalle;
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

        }
        public int?[] icodProducto = new int?[10];

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
            ERequerimientoMaterialDet obe = (ERequerimientoMaterialDet)viewMateriales.GetRow(viewMateriales.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstRequerimientoMaterialDetalle.Remove(obe);
            viewMateriales.RefreshData();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificarItem();
        }
        private void modificarItem()
        {
            ERequerimientoMaterialDet obe = (ERequerimientoMaterialDet)viewMateriales.GetRow(viewMateriales.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteRequerimientoMaterialDetalle frm = new frmManteRequerimientoMaterialDetalle())
            {
                frm.obe = obe;
                frm.lstRequerimientoMaterialDet = lstRequerimientoMaterialDetalle;
                frm.SetModify();
                frm.cod_proceso = Convert.ToInt32(lkpProceso.EditValue);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstRequerimientoMaterialDetalle = frm.lstRequerimientoMaterialDet;
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
                    txtdescripcion.Text = frm._Be.pr_vdescripcion_producto;

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
                    LkpMarca.Text = frm._Be.pr_viid_marca;
                    btnmodelo.Tag = frm._Be.orped_imodelo;
                    btnmodelo.Text = frm._Be.strmodelo;
                    lkpTipo.EditValue = frm._Be.orped_itipo;
                    lkpTipo.Text = frm._Be.pr_viid_tipo;
                    LkpColor.EditValue = frm._Be.orped_icolor;
                    LkpColor.Text = frm._Be.pr_viid_color;
                    lkpSerie.EditValue = frm._Be.orped_serie;
                    lkpSerie.Text = frm._Be.resec_vdescripcion;
                    lkpLinea.EditValue = frm._Be.fitec_ilinea;
                    lkpLinea.Text = frm._Be.strlinea;

                }
            }
        }

        private void btnFichaTecnica_Click(object sender, EventArgs e)
        {
            try
            {
                using (ListarFichaTecnica frm = new ListarFichaTecnica())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnFichaTecnica.Tag = frm._Be.fitec_iid_ficha_tecnica;
                        btnFichaTecnica.Text = frm._Be.fitec_icod_ficha_tecnica;

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

        }

        private void btnOrdenProduccion_Click(object sender, EventArgs e)
        {
            try
            {
                using (ListarOrdenProduccion frm = new ListarOrdenProduccion())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnOrdenProduccion.Tag = frm._Be.orprc_iid_orden_produccion;
                        btnOrdenProduccion.Text = frm._Be.orprc_icod_orden_produccion;
                        btnPedido.Tag = frm._Be.orprc_ipedido;
                        btnPedido.Text = frm._Be.orprc_vpedido;
                        btnItemPedido.Tag = frm._Be.orprc_iitem_pedido;
                        btnItemPedido.Text = frm._Be.orprc_vitem_pedido;
                        btncodigoProducto.Text = frm._Be.orprc_vcodigo_producto;
                        txtdescripcion.Text = frm._Be.orprc_vdescripcion_producto;
                        btnFichaTecnica.Tag = frm._Be.orprc_ificha_tecnica;
                        btnFichaTecnica.Text = frm._Be.orprc_vficha_tecnica;
                        LkpMarca.EditValue = frm._Be.orprc_imarca;
                        btnmodelo.Tag = frm._Be.orprc_imodelo;
                        btnmodelo.Text = frm._Be.DesModelo;
                        lkpTipo.EditValue = frm._Be.orprc_itipo;
                        LkpColor.EditValue = frm._Be.orprc_icolor;
                        lkpSerie.EditValue = frm._Be.orprc_iserie;
                        txttalla1.Text = frm._Be.orprc_talla1.ToString();
                        txttalla2.Text = frm._Be.orprc_talla2.ToString();
                        txttalla3.Text = frm._Be.orprc_talla3.ToString();
                        txttalla4.Text = frm._Be.orprc_talla4.ToString();
                        txttalla5.Text = frm._Be.orprc_talla5.ToString();
                        txttalla6.Text = frm._Be.orprc_talla6.ToString();
                        txttalla7.Text = frm._Be.orprc_talla7.ToString();
                        txttalla8.Text = frm._Be.orprc_talla8.ToString();
                        txttalla9.Text = frm._Be.orprc_talla9.ToString();
                        txttalla10.Text = frm._Be.orprc_talla10.ToString();
                        txtcantidad1.Text = frm._Be.orprc_cant_talla1.ToString();
                        txtcantidad2.Text = frm._Be.orprc_cant_talla2.ToString();
                        txtcantidad3.Text = frm._Be.orprc_cant_talla3.ToString();
                        txtcantidad4.Text = frm._Be.orprc_cant_talla4.ToString();
                        txtcantidad5.Text = frm._Be.orprc_cant_talla5.ToString();
                        txtcantidad6.Text = frm._Be.orprc_cant_talla6.ToString();
                        txtcantidad7.Text = frm._Be.orprc_cant_talla7.ToString();
                        txtcantidad8.Text = frm._Be.orprc_cant_talla8.ToString();
                        txtcantidad9.Text = frm._Be.orprc_cant_talla9.ToString();
                        txtcantidad10.Text = frm._Be.orprc_cant_talla10.ToString();
                        oBe.rqmac_icod_producto1 = frm._Be.orprc_icod_producto1;
                        oBe.rqmac_icod_producto2 = frm._Be.orprc_icod_producto2;
                        oBe.rqmac_icod_producto3 = frm._Be.orprc_icod_producto3;
                        oBe.rqmac_icod_producto4 = frm._Be.orprc_icod_producto4;
                        oBe.rqmac_icod_producto5 = frm._Be.orprc_icod_producto5;
                        oBe.rqmac_icod_producto6 = frm._Be.orprc_icod_producto6;
                        oBe.rqmac_icod_producto7 = frm._Be.orprc_icod_producto7;
                        oBe.rqmac_icod_producto8 = frm._Be.orprc_icod_producto8;
                        oBe.rqmac_icod_producto9 = frm._Be.orprc_icod_producto9;
                        oBe.rqmac_icod_producto10 = frm._Be.orprc_icod_producto10;
                        lkpLinea.EditValue = frm._Be.orprc_ilinea;
                        //lkpLinea.Text = frm._Be.orprc_vlinea;

                        lstMaterial = new BCentral().listarOrdenPrduccionDetalle(Convert.ToInt32(btnOrdenProduccion.Tag));
                        foreach (var objd in lstMaterial)
                        {
                            ERequerimientoMaterialDet op = new ERequerimientoMaterialDet();
                            op.rqmad_iproceso = objd.orprd_iproceso;
                            op.rqmad_iubicacion = objd.orprd_iubicacion;
                            op.strubicacion = objd.orprd_vubicacion;
                            op.rqmad_vmaterial = objd.orprd_vmaterial == null ? objd.strProducto : objd.orprd_vmaterial;
                            op.rqmad_vmaterial = objd.orprd_vmaterial == "" ? objd.strProducto : objd.orprd_vmaterial;
                            op.prdc_icod_producto = objd.prdc_icod_producto;
                            op.strCodeProducto = objd.strCodeProducto;
                            op.strProducto = objd.strProducto;
                            op.strmedida = objd.strmedida;
                            op.rqmad_ncantidad_requerida = objd.orprd_ncantidad;
                            op.orprd_imedida = objd.orprd_imedida;
                            lstRequerimientoMaterialDetalle.Add(op);
                        }
                        grdMateriales.DataSource = lstRequerimientoMaterialDetalle;
                        viewMateriales.RefreshData();

                        FiltarComboProceso();
                        DisableControls();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void DisableControls()
        {
            btnFichaTecnica.Enabled = false;
            LkpMarca.Enabled = false;
            btnmodelo.Enabled = false;
            lkpTipo.Enabled = false;
            LkpColor.Enabled = false;
            lkpLinea.Enabled = false;
            lkpSerie.Enabled = false;

        }

        private void btnOperario_Click(object sender, EventArgs e)
        {

            using (ListarPersonal frm = new ListarPersonal())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnOperario.Tag = frm._Be.perc_iid_personal;
                    btnOperario.Text = frm._Be.perc_vnombres_apellidos;
                }
            }

        }

        private void lkpProceso_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                lstRequerimientoMaterialDetalle.Clear();
                lstMaterial = new BCentral().listarOrdenPrduccionDetalle(Convert.ToInt32(btnOrdenProduccion.Tag)).Where(obj => obj.orprd_iproceso == Convert.ToInt32(lkpProceso.EditValue)).ToList();
                foreach (var objd in lstMaterial)
                {
                    ERequerimientoMaterialDet op = new ERequerimientoMaterialDet();
                    op.rqmad_iproceso = objd.orprd_iproceso;
                    op.rqmad_iubicacion = objd.orprd_iubicacion;
                    op.strubicacion = objd.orprd_vubicacion;
                    op.rqmad_vmaterial = objd.orprd_vmaterial;
                    op.prdc_icod_producto = objd.prdc_icod_producto;
                    op.strCodeProducto = objd.strCodeProducto;
                    op.strProducto = objd.strProducto;
                    op.strmedida = objd.strmedida;
                    op.rqmad_ncantidad_requerida = objd.orprd_ncantidad;
                    lstRequerimientoMaterialDetalle.Add(op);
                }
                grdMateriales.DataSource = lstRequerimientoMaterialDetalle;
                grdMateriales.RefreshDataSource();




            }
        }
    }
}