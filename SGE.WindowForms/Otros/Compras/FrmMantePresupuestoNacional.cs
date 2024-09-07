using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmMantePresupuestoNacional : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMantePresupuestoNacional));
        public List<PresupuestoNacionalDetalleBE> mListaPresupuestoNacionalDetalleOrigen = new List<PresupuestoNacionalDetalleBE>();
        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public BSMaintenanceStatus oState;
        public int IdPresupuestoNacional = 0;
        private int IdCentroCosto = 0;
        private int xposition = 0;
        public int Icod_Importacion = 0;
        public int IcodImpoDet = 0;

        private List<EFacturaCompra> lstFacCompra = new List<EFacturaCompra>();
        private List<EFacturaCompra> lstFacCompraElimnados = new List<EFacturaCompra>();
        private BSMaintenanceStatus mStatus;
        List<EDXPImportacion> lstDXPImpDet = new List<EDXPImportacion>();
        private List<EImportacionFactura> lstImpFacturaDet = new List<EImportacionFactura>();
        private List<EImportacionFactura> lstImpFacturaDet2 = new List<EImportacionFactura>();
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }

        public enum Operacion
        {
            Nuevo = 1,
            Modificar = 2,
            Eliminar = 3,
            Consultar = 4
        }

        #endregion  

        #region "Eventos"

        public FrmMantePresupuestoNacional()
        {
            InitializeComponent();
        }

        private void FrmMantePresupuestoNacional_Load(object sender, EventArgs e)
        {

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtNumImportacion.Enabled = false;
                txtNumImportacion2.Enabled = false;

            }

            txtNumImportacion.Text = DateTime.Now.Year.ToString();
            dtmEmbarque.DateTime = DateTime.Now;
            txtNumImportacion.Enabled = true;
            txtNumImportacion2.Enabled = true;
            lkpMoneda.Enabled = true;

            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            ListaTipoCambio = new BAdministracionSistema().listarTipoCambio();

            BSControls.LoaderLook(lkpProyecto, new BVentas().listarProyectos(), "pryc_vcorrelativo", "pryc_icod_proyecto", true);
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(79), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);


            dtmFecha.EditValue = DateTime.Now;



            mListaPresupuestoNacionalDetalleOrigen = new List<PresupuestoNacionalDetalleBE>();
            bsListado.DataSource = mListaPresupuestoNacionalDetalleOrigen;
            grdPresupuestoNacionalDetalle.DataSource = bsListado;




            lstFacCompra = new BCompras().listarFacCompraRelacionPresupuesto(Parametros.intEjercicio, IdPresupuestoNacional);
            lstFacCompraElimnados = lstFacCompra.Where(ob => ob.sflag_relacion_presupuesto == true).ToList();

            //factor();   
            ListarImportacionDet();
        }

        private void dtmFecha_EditValueChanged(object sender, EventArgs e)
        {
            //var Lista = ListaTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dtmFecha.EditValue).ToShortDateString()).ToList();
            //Lista.ForEach(obe =>
            //{
            //    txtTipoDeCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
            //});
        }

        private void btnProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProducto();
        }



        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mListaPresupuestoNacionalDetalleOrigen.Count > 0)
            {
                FrmMantePresupuestoNacionalDetalle movDetalle = new FrmMantePresupuestoNacionalDetalle();
                movDetalle.SetModify();
                movDetalle.CargaControles();
                //movDetalle.txtTipoDeCambio.EditValue = txtTipoDeCambio.EditValue;
                movDetalle.txtConcepto.Text = viewPresupuestoNacionalDetalle.GetFocusedRowCellValue("cpnd_vdescripcion").ToString();
                movDetalle.lkpMoneda.EditValue = Convert.ToInt32(viewPresupuestoNacionalDetalle.GetFocusedRowCellValue("tablc_iid_tipo_moneda_origen"));

                movDetalle.txtMontoTotal.Text = Convert.ToDecimal(viewPresupuestoNacionalDetalle.GetFocusedRowCellValue("prepd_nmont_tot_concepto")).ToString();

                if (Convert.ToDecimal(viewPresupuestoNacionalDetalle.GetFocusedRowCellValue("prepd_nmont_tot_concepto")) == 0)
                {
                    movDetalle.txtMontoUnitario.Text = "0.00";
                }
                else
                {
                    movDetalle.txtMontoUnitario.Text = Convert.ToDecimal(viewPresupuestoNacionalDetalle.GetFocusedRowCellValue("prepd_nmont_unit_concepto")).ToString();
                }
                movDetalle.txtMontoOrigen.Text = Convert.ToDecimal(viewPresupuestoNacionalDetalle.GetFocusedRowCellValue("prepd_nmont_tot_concepto_origen")).ToString();
                movDetalle.txtMontoTotal.Focus();
                //movDetalle.lkpMoneda.EditValue = Convert.ToInt32(lkpMoneda.EditValue);
                if (movDetalle.ShowDialog() == DialogResult.OK)
                {
                    xposition = viewPresupuestoNacionalDetalle.FocusedRowHandle;
                    if (movDetalle.oBE != null)
                    {
                        viewPresupuestoNacionalDetalle.SetRowCellValue(xposition, "prepd_nmont_tot_concepto", movDetalle.oBE.prepd_nmont_tot_concepto);
                        viewPresupuestoNacionalDetalle.SetRowCellValue(xposition, "prepd_nmont_unit_concepto", movDetalle.oBE.prepd_nmont_unit_concepto);
                        viewPresupuestoNacionalDetalle.SetRowCellValue(xposition, "tablc_iid_tipo_moneda_origen", movDetalle.oBE.tablc_iid_tipo_moneda_origen);
                        viewPresupuestoNacionalDetalle.SetRowCellValue(xposition, "prepd_nmont_tot_concepto_origen", movDetalle.oBE.prepd_nmont_tot_concepto_origen);
                        viewPresupuestoNacionalDetalle.SetRowCellValue(xposition, "TipoMoneda", movDetalle.oBE.TipoMoneda);
                        viewPresupuestoNacionalDetalle.SetRowCellValue(xposition, "TipOper", Operacion.Modificar);
                        viewPresupuestoNacionalDetalle.UpdateCurrentRow();

                        CalcularTotales();
                    }
                }
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tmrNumero_Tick(object sender, EventArgs e)
        {
            //Obtener el correlativo del documento
            if (Status == BSMaintenanceStatus.CreateNew)
                ObtenerCorrelativo();
        }

        #endregion

        #region "Metodos"

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            dtmFecha.Enabled = !Enabled;
            dtmEmbarque.Enabled = !Enabled;
            txtConocEmbarq.Enabled = !Enabled;
            txtProcedencia.Enabled = !Enabled;
            lkpProyecto.Enabled = !Enabled;
            txtCCostos.Enabled = !Enabled;
            txtEmpTransp.Enabled = !Enabled;
            txtNave.Enabled = !Enabled;
            txtDUA.Enabled = !Enabled;
            dtmArribo.Enabled = !Enabled;
            dtmIngreso.Enabled = !Enabled;
            txtGuiaIngreso.Enabled = !Enabled;
            txtMontoTotal.Enabled = !Enabled;

            //btnProducto.Focus();
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtNumImportacion.Enabled = true;
                txtNumImportacion2.Enabled = true;
                bteAduana.Enabled = true;
                lkpMoneda.Enabled = true;
            }
        }

        private void clearControl()
        {
            //txtTipoDeCambio.Text = "0.0000";
            //btnProducto.Text = "";                       
            txtMontoTotal.Text = "0.00";
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            tmrNumero.Enabled = true;
            tmrNumero.Interval = 10000;
            //Obtener el correlativo del documento
            ObtenerCorrelativo();
            Cargar(true);

        }
        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
            Cargar(false);
        }
        void form2_MiEvento()
        {
            Cargar(false);
        }
        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            Cargar(false);
            TotalSoles();
            TotalDolares();
        }
        public void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            EImportacion oBe = new EImportacion();

            try
            {
                if (string.IsNullOrEmpty(txtNumImportacion.Text))
                {
                    oBase = txtNumImportacion;
                    throw new ArgumentException("Ingresar el N° Presupuesto");
                }
                if (Convert.ToInt32(bteAduana.Tag) == 0)
                {
                    oBase = bteAduana;
                    throw new ArgumentException("Ingresar la Ag. Aduana");
                }
                oBe.impc_icod_importacion = IdPresupuestoNacional;
                oBe.impc_vnumero_importacion = txtNumImportacion.Text + "-" + txtNumImportacion2.Text;
                oBe.impc_sfecha_importacion = dtmFecha.DateTime;
                oBe.tablc_iid_sit_import = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.add_icod_aduana = Convert.ToInt32(bteAduana.Tag);
                if (Convert.ToInt32(bteAduana.Tag) == 0)
                {
                    oBe.add_vrazon = null;
                }
                else
                {
                    oBe.add_vrazon = bteAduana.Text;
                }
                //oBe.impc_sfecha_embarque=dtmEmbarque.DateTime;-----------

                if (dtmEmbarque.DateTime == null || dtmEmbarque.Text == "" || dtmEmbarque.Text == "01/01/0001")
                {
                    oBe.impc_sfecha_embarque = (DateTime?)null;
                }
                else
                {
                    oBe.impc_sfecha_embarque = dtmEmbarque.DateTime;
                }
                oBe.impc_vconoc_embarque = txtConocEmbarq.Text;
                oBe.impc_vprocedencia = txtProcedencia.Text;
                oBe.impc_vemp_transporte = txtEmpTransp.Text;
                oBe.impc_vnave = txtNave.Text;
                oBe.impc_vdua = txtDUA.Text;
                //oBe.impc_sfecha_arribo=dtmArribo.DateTime;------------------------
                if (dtmArribo.DateTime == null || dtmArribo.Text == "" || dtmArribo.Text == "01/01/0001")
                {
                    oBe.impc_sfecha_arribo = (DateTime?)null;
                }
                else
                {
                    oBe.impc_sfecha_arribo = dtmArribo.DateTime;
                }

                //oBe.impc_sfecha_ingreso=dtmIngreso.DateTime;----------------------------
                if (dtmIngreso.DateTime == null || dtmIngreso.Text == "" || dtmIngreso.Text == "01/01/0001")
                {
                    oBe.impc_sfecha_ingreso = (DateTime?)null;
                }
                else
                {
                    oBe.impc_sfecha_ingreso = dtmIngreso.DateTime;
                }

                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                //oBe.impc_nfactor_dolar=null;
                oBe.impc_nfactor_sol = null;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.impc_flag_estado = true;
                oBe.impc_vguia_ingreso = txtGuiaIngreso.Text;
                oBe.pryc_icod_proyecto = Convert.ToInt32(lkpProyecto.EditValue);
                oBe.impc_nfactor_dolar = Convert.ToDecimal(txtFactorDolar.Text);
                oBe.almac_icod_almacen = 0;
                oBe.fcoc_sfecha_doc = null;
                oBe.impc_nmonto_total_soles = Convert.ToDecimal(txtTotSoles.Text);
                oBe.impc_nmonto_total_dolares = Convert.ToDecimal(txtMontoTotal.Text);

                //Presupuesto Nacional Detalle
                List<EImportacionConceptos> lstPresupuestoNacionalDetalle = new List<EImportacionConceptos>();

                foreach (var item in mListaPresupuestoNacionalDetalleOrigen)
                {
                    EImportacionConceptos objE_PresupuestoNacionalDetalle = new EImportacionConceptos();
                    objE_PresupuestoNacionalDetalle.impd_icod_importacion_detalle = item.impd_icod_importacion_detalle;
                    objE_PresupuestoNacionalDetalle.impc_icod_importacion = IdPresupuestoNacional;
                    objE_PresupuestoNacionalDetalle.cpn_icod_concepto_nacional = item.cpn_icod_concepto_nacional;
                    objE_PresupuestoNacionalDetalle.cpnd_icod_detalle_nacional = item.cpnd_icod_detalle_nacional;
                    objE_PresupuestoNacionalDetalle.impd_nmont_tot_concepto = item.prepd_nmont_tot_concepto;
                    objE_PresupuestoNacionalDetalle.impd_nmont_unit_concepto = item.prepd_nmont_unit_concepto;
                    objE_PresupuestoNacionalDetalle.tablc_iid_tipo_moneda_origen = item.tablc_iid_tipo_moneda_origen;
                    objE_PresupuestoNacionalDetalle.impd_nmont_tot_concepto_origen = item.prepd_nmont_tot_concepto_origen;
                    objE_PresupuestoNacionalDetalle.impd_nmont_tot_ejecut = item.prepd_nmont_tot_ejecut;
                    objE_PresupuestoNacionalDetalle.impd_nmont_unit_ejecut = item.prepd_nmont_unit_ejecut;
                    //objE_PresupuestoNacionalDetalle.prepd_iusuario_crea = 0;
                    //objE_PresupuestoNacionalDetalle.prepd_iusuario_modifica = 0;
                    objE_PresupuestoNacionalDetalle.intUsuario = Valores.intUsuario;
                    objE_PresupuestoNacionalDetalle.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                    objE_PresupuestoNacionalDetalle.impd_flag_estado = true;
                    //objE_PresupuestoNacionalDetalle.TipOper = item.prepd_icod_detalle==0? Convert.ToInt32(Operacion.Nuevo):item.TipOper;
                    lstPresupuestoNacionalDetalle.Add(objE_PresupuestoNacionalDetalle);

                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    //new BCompras().InsertarPresupuestoNacional(oBe, lstPresupuestoNacionalDetalle, lstFacCompra);
                    new BCompras().InsertarimportacionyConceptos(oBe, lstPresupuestoNacionalDetalle, lstFacCompra);
                }
                else
                {

                    //new BCompras().ActualizarPresupuestoNacional(oBe, lstPresupuestoNacionalDetalle, lstFacCompra, lstFacCompraElimnados);
                    new BCompras().ActualizarImportacionyConceptos(oBe, lstPresupuestoNacionalDetalle, lstFacCompra, lstFacCompraElimnados);
                    ActualizarCostos();
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
                    XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Flag = false;
                }
            }
            finally
            {
                if (Flag)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                        Status = BSMaintenanceStatus.View;
                    else
                        Status = BSMaintenanceStatus.View;

                    Status = BSMaintenanceStatus.View;
                    this.MiEvento();
                    this.Close();
                }
            }
        }
        public void ActualizarCostos()
        {
            decimal Factor = 0;
            decimal Total = 0;
            decimal FCob = 0;
            decimal UnitCosto = 0;
            decimal TotCosto = 0;

            lstImpFacturaDet2 = new BCompras().ListarImportacionFactura(IdPresupuestoNacional);
            lstImpFacturaDet2.ForEach(x =>
            {
                List<EFacturaCompraDet> ListarDetalle = new List<EFacturaCompraDet>();
                ListarDetalle = new BCompras().listarFacCompraDet(Convert.ToInt32(x.fcoc_icod_doc));
                ListarDetalle.ForEach(xd =>
                {
                    List<EImportacionConceptos> LstImpConceptos = new List<EImportacionConceptos>();
                    LstImpConceptos = new BCompras().ListarImportacionConceptos(IdPresupuestoNacional);
                    FCob = LstImpConceptos.Where(su => Convert.ToInt32(su.cpn_icod_concepto_nacional) == 1 && Convert.ToInt32(su.cpnd_icod_detalle_nacional) == 1).ToList().Sum(su => Convert.ToDecimal(su.impd_nmonto_concepto_dol));
                    Total = LstImpConceptos.Sum(s => Convert.ToDecimal(s.impd_nmonto_concepto_dol));
                    Factor = Total / FCob;
                    UnitCosto = xd.fcod_nmonto_unit * Factor;
                    TotCosto = xd.fcod_nmonto_total * Factor;
                    EFacturaCompraDet ObeFD = new EFacturaCompraDet();
                    ObeFD.fcoc_icod_doc = Convert.ToInt32(x.fcoc_icod_doc);
                    ObeFD.fcod_nmonto_unit_costo = UnitCosto;
                    ObeFD.fcod_nmonto_total_costo = TotCosto;
                    new BCompras().ActualizarCostos(Convert.ToInt32(xd.fcod_icod_doc), UnitCosto, TotCosto);
                });
            });
        }
        public void ListarImportacionDet()
        {

            lstImpFacturaDet = new BCompras().ListarImportacionFactura(IdPresupuestoNacional);

        }

        private void listarProducto()
        {
            BaseEdit oBase = null;
            try
            {

                using (frmFacCompraPresupuesto frm = new frmFacCompraPresupuesto())
                {
                    frm.prep_cod_presupuesto = IdPresupuestoNacional;
                    frm.lstFacCompra = lstFacCompra;
                    frm.cargar();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {

                        lstFacCompra = frm.lstFacCompra;
                        //txtSituacion.Text = frm._Be.descripcion_situacion;
                        //IdCentroCosto = Convert.ToInt32(frm._Be.cecoc_icod_centro_costo);

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




        private void ObtenerCorrelativo()
        {
            //txtNumImportacion.Text = Parametros.intEjercicio.ToString();
            //txtNumImportacion2.Text=String.Format("{0:0000}", new BCompras().NumeroCorrelativoPresupuestoNacional(Parametros.intEjercicio));
        }

        private void CalcularTotales()
        {
            decimal dmlMontoTotal = 0;
            decimal dmlMontoUnitario = 0;

            if (viewPresupuestoNacionalDetalle.RowCount > 0)
            {
                for (int i = 0; i < viewPresupuestoNacionalDetalle.RowCount; i++)
                {
                    dmlMontoTotal = dmlMontoTotal + Convert.ToDecimal(viewPresupuestoNacionalDetalle.GetRowCellValue(i, "prepd_nmont_tot_concepto"));
                    dmlMontoUnitario = Convertir.RedondearNumero((dmlMontoUnitario + Convert.ToDecimal(viewPresupuestoNacionalDetalle.GetRowCellValue(i, "prepd_nmont_unit_concepto"))));
                }
            }

            txtMontoTotal.EditValue = dmlMontoTotal;

        }

        private void Cargar(bool Plantilla)
        {

            List<EPresupuestoNacionalDetalle> lstPresupuestoNacionalDetalle = new List<EPresupuestoNacionalDetalle>();
            List<EImportacionConceptos> LstImpConceptos = new List<EImportacionConceptos>();
            if (Plantilla)

                lstPresupuestoNacionalDetalle = new BCompras().ListarNacionalPlantilla();
            else
                LstImpConceptos = new BCompras().ListarImportacionConceptos(IdPresupuestoNacional);

            foreach (EPresupuestoNacionalDetalle item in lstPresupuestoNacionalDetalle)
            {
                PresupuestoNacionalDetalleBE objPresupuestoNacionalDetalleBE = new PresupuestoNacionalDetalleBE();
                objPresupuestoNacionalDetalleBE.prepd_icod_detalle = item.prepd_icod_detalle;
                objPresupuestoNacionalDetalleBE.prep_icod_presupuesto = item.prep_icod_presupuesto;
                objPresupuestoNacionalDetalleBE.cpn_icod_concepto_nacional = item.cpn_icod_concepto_nacional;
                objPresupuestoNacionalDetalleBE.cpn_vdescripcion_concepto_nacional = item.cpn_vdescripcion_concepto_nacional;
                objPresupuestoNacionalDetalleBE.cpnd_icod_detalle_nacional = item.cpnd_icod_detalle_nacional;
                objPresupuestoNacionalDetalleBE.cpnd_vdescripcion = item.cpnd_vdescripcion;
                objPresupuestoNacionalDetalleBE.prepd_nmont_tot_concepto = item.prepd_nmont_tot_concepto;
                objPresupuestoNacionalDetalleBE.prepd_nmont_unit_concepto = item.prepd_nmont_unit_concepto;
                objPresupuestoNacionalDetalleBE.tablc_iid_tipo_moneda_origen = item.tablc_iid_tipo_moneda_origen;
                objPresupuestoNacionalDetalleBE.TipoMoneda = item.TipoMoneda;
                objPresupuestoNacionalDetalleBE.prepd_nmont_tot_concepto_origen = item.prepd_nmont_tot_concepto_origen;
                objPresupuestoNacionalDetalleBE.prepd_nmont_tot_ejecut = item.prepd_nmont_tot_ejecut;
                objPresupuestoNacionalDetalleBE.prepd_nmont_unit_ejecut = item.prepd_nmont_unit_ejecut;
                objPresupuestoNacionalDetalleBE.TipOper = item.TipOper;
                objPresupuestoNacionalDetalleBE.strCod = item.strCod;

                mListaPresupuestoNacionalDetalleOrigen.Add(objPresupuestoNacionalDetalleBE);
            }

            bsListado.DataSource = mListaPresupuestoNacionalDetalleOrigen;
            grdPresupuestoNacionalDetalle.DataSource = bsListado;
            grdPresupuestoNacionalDetalle.RefreshDataSource();
            viewPresupuestoNacionalDetalle.ExpandAllGroups();
            //--------------------------------------------------------
            //-------------Importacion_Conceptos---------ADEIR--------
            //--------------------------------------------------------
            int contador = 0;
            foreach (EImportacionConceptos item in LstImpConceptos)
            {
                PresupuestoNacionalDetalleBE objPresupuestoNacionalDetalleBE = new PresupuestoNacionalDetalleBE();
                objPresupuestoNacionalDetalleBE.impd_icod_importacion_detalle = Convert.ToInt32(item.impd_icod_importacion_detalle);
                objPresupuestoNacionalDetalleBE.prepd_icod_detalle = Convert.ToInt32(item.impc_icod_importacion);
                //objPresupuestoNacionalDetalleBE.prep_icod_presupuesto = item.prep_icod_presupuesto;
                objPresupuestoNacionalDetalleBE.cpn_icod_concepto_nacional = Convert.ToInt32(item.cpn_icod_concepto_nacional);
                //objPresupuestoNacionalDetalleBE.cpn_vdescripcion_concepto_nacional = item.cpn_vdescripcion_concepto_nacional;
                objPresupuestoNacionalDetalleBE.cpnd_icod_detalle_nacional = Convert.ToInt32(item.cpnd_icod_detalle_nacional);
                objPresupuestoNacionalDetalleBE.cpnd_vdescripcion = item.cpnd_vdescripcion;
                objPresupuestoNacionalDetalleBE.prepd_nmont_tot_concepto = Convert.ToDecimal(item.impd_nmont_tot_concepto);
                objPresupuestoNacionalDetalleBE.prepd_nmont_unit_concepto = Convert.ToDecimal(item.impd_nmont_unit_concepto);
                objPresupuestoNacionalDetalleBE.tablc_iid_tipo_moneda_origen = Convert.ToInt32(item.tablc_iid_tipo_moneda_origen);
                objPresupuestoNacionalDetalleBE.TipoMoneda = item.TipoMoneda;
                objPresupuestoNacionalDetalleBE.prepd_nmont_tot_concepto_origen = Convert.ToDecimal(item.impd_nmont_tot_concepto_origen);
                objPresupuestoNacionalDetalleBE.prepd_nmont_tot_ejecut = Convert.ToDecimal(item.impd_nmont_tot_ejecut);
                objPresupuestoNacionalDetalleBE.prepd_nmont_unit_ejecut = Convert.ToDecimal(item.impd_nmont_unit_ejecut);
                objPresupuestoNacionalDetalleBE.strCod = item.strCod;
                IcodImpoDet = Convert.ToInt32(item.impd_icod_importacion_detalle);
                objPresupuestoNacionalDetalleBE.impd_nmonto_concepto_sol = Convert.ToDecimal(item.impd_nmonto_concepto_sol);

                objPresupuestoNacionalDetalleBE.impd_nmonto_concepto_dol = Convert.ToDecimal(item.impd_nmonto_concepto_dol);

                contador = contador + 1;

                //objPresupuestoNacionalDetalleBE.TipOper = item.TipOper;
                mListaPresupuestoNacionalDetalleOrigen.Add(objPresupuestoNacionalDetalleBE);
                //txtMontoTotal.Text = Convert.ToString(Convert.ToDecimal(txtMontoTotal.Text) + objPresupuestoNacionalDetalleBE.impd_nmonto_concepto_dol);
                //txtTotSoles.Text = Convert.ToString(Convert.ToDecimal(txtTotSoles.Text) + objPresupuestoNacionalDetalleBE.impd_nmonto_concepto_sol);

                txtMontoTotal.Text = LstImpConceptos.Sum(x => x.impd_nmonto_concepto_dol).ToString();
                txtTotSoles.Text = LstImpConceptos.Sum(x => x.impd_nmonto_concepto_sol).ToString();
                if (contador == 1 && (Convert.ToDecimal(txtMontoTotal.Text) > 0 || Convert.ToDecimal(txtTotSoles.Text) > 0))
                {
                    decimal FOBSoles = 0;
                    FOBSoles = Convert.ToDecimal(item.impd_nmonto_concepto_sol);
                    decimal FOBDolares = 0;
                    FOBDolares = Convert.ToDecimal(item.impd_nmonto_concepto_dol);
                    if (FOBSoles > 0)
                    {
                        txtFactorSoles.Text = (Convert.ToDecimal(txtTotSoles.Text) / FOBSoles).ToString();
                    }
                    if (FOBDolares > 0)
                    {
                        txtFactorDolar.Text = (Convert.ToDecimal(txtMontoTotal.Text) / FOBDolares).ToString();
                    }


                }
            }
            bsListado.DataSource = mListaPresupuestoNacionalDetalleOrigen;
            grdPresupuestoNacionalDetalle.DataSource = bsListado;
            grdPresupuestoNacionalDetalle.RefreshDataSource();
            viewPresupuestoNacionalDetalle.ExpandAllGroups();
            LstImpConceptos.ForEach(x =>
            {

                List<EDXPImportacion> lstDXPImpDet2 = new List<EDXPImportacion>();

                lstDXPImpDet2 = new BCompras().listarDXPImpDet(Convert.ToInt32(x.impd_icod_importacion_detalle));
                if (lstDXPImpDet2.Count != 0)
                {
                    foreach (var item in lstDXPImpDet2)
                    {
                        EDXPImportacion obeDet = new EDXPImportacion();
                        obeDet.tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc;
                        obeDet.doxpc_vnumero_doc = item.doxpc_vnumero_doc;
                        obeDet.doxpc_sfecha_doc = Convert.ToDateTime(item.doxpc_sfecha_doc);
                        obeDet.Moneda = item.Moneda;
                        obeDet.doxpc_nmonto_tipo_cambio = Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio);
                        obeDet.dxpd2_nmonto_importacion = Convert.ToDecimal(item.dxpd2_nmonto_importacion);
                        obeDet.proc_vnombrecompleto = item.proc_vnombrecompleto;
                        obeDet.tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda);
                        obeDet.impd_icod_importacion_detalle = Convert.ToInt32(item.impd_icod_importacion_detalle);
                        lstDXPImpDet.Add(obeDet);
                    }

                }

            });
            TotalSoles();
            TotalDolares();
        }

        #endregion
        public void factor()
        {
            EImportacionConceptos Obe = (EImportacionConceptos)viewPresupuestoNacionalDetalle.GetRow(viewPresupuestoNacionalDetalle.FocusedRowHandle);
            if (Obe == null)
                return;
            decimal df = 0;
            df = Convert.ToDecimal(Obe.impd_nmonto_concepto_sol);
        }
        public class PresupuestoNacionalDetalleBE
        {
            public int impd_icod_importacion_detalle { get; set; }
            public int prepd_icod_detalle { get; set; }
            public int prep_icod_presupuesto { get; set; }
            public int cpn_icod_concepto_nacional { get; set; }
            public string cpn_vdescripcion_concepto_nacional { get; set; }
            public int cpnd_icod_detalle_nacional { get; set; }
            public string cpnd_vdescripcion { get; set; }
            public decimal prepd_nmont_tot_concepto { get; set; }
            public decimal prepd_nmont_unit_concepto { get; set; }
            public int tablc_iid_tipo_moneda_origen { get; set; }
            public string TipoMoneda { get; set; }
            public decimal prepd_nmont_tot_concepto_origen { get; set; }
            public decimal prepd_nmont_tot_ejecut { get; set; }
            public decimal prepd_nmont_unit_ejecut { get; set; }
            public int TipOper { get; set; }
            public string strCod { get; set; }
            public decimal impd_nmonto_concepto_sol { get; set; }
            public decimal impd_nmonto_concepto_dol { get; set; }

            public PresupuestoNacionalDetalleBE()
            {

            }
        }

        private void btnGuardar_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.SetSave();
        }

        private void btnSalir_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnMotonave_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarMotoNave();
        }
        private void ListarMotoNave()
        {
            //FrmListarMotonaves MotoNave = new FrmListarMotonaves();
            //MotoNave.Carga();
            //if (MotoNave.ShowDialog() == DialogResult.OK)
            //{
            //    btnMotonave.Tag = MotoNave._Be.idd_icod_motonaves;
            //    btnMotonave.Text = MotoNave._Be.Descripcion;
            //}
            //btnMotonave.Focus();
        }

        private void bteAduana_Click(object sender, EventArgs e)
        {

            using (frmListarAduana frm = new frmListarAduana())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteAduana.Tag = frm._Be.idd_icod_aduana;
                    bteAduana.Text = frm._Be.razon;
                    //txtDireccion.Text = frm._Be.cliec_vdireccion_cliente;
                    //txtRUC.Text = frm._Be.cliec_cruc;

                }

            }

        }

        private void GetOCL()
        {
            //string correlativo =String.Format("{0:0000}", new BCompras().NumeroCorrelativoPresupuestoNacional(Convert.ToInt32(txtNumImportacion.Text)));
            //txtNumImportacion2.Text = correlativo;

        }

        private void txtNumImportacion_EditValueChanged(object sender, EventArgs e)
        {
            //GetOCL();
        }

        private void lkpProyecto_EditValueChanged(object sender, EventArgs e)
        {
            List<EProyectos> LstProyecto = new List<EProyectos>();
            LstProyecto = new BVentas().listarProyectos();
            List<ECentroCosto> LstCCostos = new List<ECentroCosto>();
            LstCCostos = new BContabilidad().listarCentroCosto();





            List<EProyectos> aux = new List<EProyectos>();
            aux = LstProyecto.Where(x => x.pryc_icod_proyecto == Convert.ToInt32(lkpProyecto.EditValue)).ToList();


            if (aux.Count == 1)
            {
                //txtCCostos.Text = aux[0].pryc_icod_ccosto.ToString();
                int Ccostos = aux[0].pryc_icod_ccosto;

                List<ECentroCosto> cc = new List<ECentroCosto>();
                cc = LstCCostos.Where(x => x.cecoc_icod_centro_costo == Convert.ToInt32(Ccostos)).ToList();


                if (aux.Count == 1)
                {
                    txtCCostos.Text = cc[0].cecoc_vcodigo_centro_costo.ToString();

                }

            }



        }

        private void DetFacturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListarFacturaImportacion frm = new FrmListarFacturaImportacion();
            frm.Icod_Importacion = IdPresupuestoNacional;
            frm.Show();
        }

        private void detalleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmListarDXPImpoDet frm = new frmListarDXPImpoDet();
            frm.IcodImpoDet = Convert.ToInt32(viewPresupuestoNacionalDetalle.GetFocusedRowCellValue("impd_icod_importacion_detalle"));
            frm.Show();
        }
        public void ActualizarMontos()
        {
            List<EImportacionConceptos> LstImpConceptosDet = new List<EImportacionConceptos>();
            List<EDXPImportacion> lstDXPImpDet = new List<EDXPImportacion>();
            LstImpConceptosDet = new BCompras().ListarImportacionConceptos(IdPresupuestoNacional);
            LstImpConceptosDet.ForEach(x =>
            {
                lstDXPImpDet = new BCompras().listarDXPImpDetTodo().Where(xd => xd.impd_icod_importacion_detalle == x.impd_icod_importacion_detalle).ToList();

            });
        }
        private void TotalSoles()
        {
            decimal TotalSoles = 0;
            decimal TotalSolesConvertir = 0;
            decimal Suma = 0;
            decimal SumaNCP = 0;
            decimal SumaNCI = 0;
            EDXPImportacion Obe1 = new EDXPImportacion();
            EDXPImportacion Obe2 = new EDXPImportacion();
            List<EDXPImportacion> lisImpo = new List<EDXPImportacion>();
            lstDXPImpDet.ForEach(x =>
            {
                List<EDXPImportacion> lstDXPImpDet3 = new List<EDXPImportacion>();

                lstDXPImpDet3 = new BCompras().listarDXPImpDet(Convert.ToInt32(x.impd_icod_importacion_detalle));
                lstDXPImpDet3.ForEach(xd =>
                {
                    if (xd.tablc_iid_tipo_moneda == 4)
                    {
                        TotalSolesConvertir = Convertir.RedondearNumero(xd.dxpd2_nmonto_importacion * xd.doxpc_nmonto_tipo_cambio);
                        lisImpo.Add(new EDXPImportacion
                        {
                            dxpd2_nmonto_importacion = TotalSolesConvertir,
                            tdocc_icod_tipo_doc = xd.tdocc_icod_tipo_doc
                        });
                    }
                    else
                    {
                        TotalSoles = Convertir.RedondearNumero(xd.dxpd2_nmonto_importacion);
                        lisImpo.Add(new EDXPImportacion
                        {
                            dxpd2_nmonto_importacion = TotalSoles,
                            tdocc_icod_tipo_doc = xd.tdocc_icod_tipo_doc
                        });
                    }

                    if (lisImpo[0].tdocc_icod_tipo_doc == 86)
                    {
                        SumaNCP = lisImpo.Where(xp => xp.tdocc_icod_tipo_doc == 86).Sum(xs => xs.dxpd2_nmonto_importacion);
                        Suma = lisImpo.Where(xp => xp.tdocc_icod_tipo_doc != 86).Sum(xs => xs.dxpd2_nmonto_importacion) - SumaNCP;
                        new BCompras().modificarImportacionConceptosDXPMontoSoles(Convert.ToInt32(xd.impd_icod_importacion_detalle), Suma);
                    }
                    else if (lisImpo[0].tdocc_icod_tipo_doc == 119)
                    {
                        SumaNCI = lisImpo.Where(xp => xp.tdocc_icod_tipo_doc == 119).Sum(xs => xs.dxpd2_nmonto_importacion);
                        Suma = lisImpo.Where(xp => xp.tdocc_icod_tipo_doc != 119).Sum(xs => xs.dxpd2_nmonto_importacion) - SumaNCI;
                        new BCompras().modificarImportacionConceptosDXPMontoSoles(Convert.ToInt32(xd.impd_icod_importacion_detalle), Suma);
                    }
                    else
                    {
                        Suma = lisImpo.Sum(xs => xs.dxpd2_nmonto_importacion);
                        new BCompras().modificarImportacionConceptosDXPMontoSoles(Convert.ToInt32(xd.impd_icod_importacion_detalle), Suma);
                    }





                });
                lisImpo.Clear();
            });

        }
        private void TotalDolares()
        {
            decimal TotalSoles = 0;
            decimal TotalSolesConvertir = 0;
            decimal Suma = 0;
            decimal SumaNCP = 0;
            decimal SumaNCI = 0;
            EDXPImportacion Obe1 = new EDXPImportacion();
            List<EDXPImportacion> lisImpo = new List<EDXPImportacion>();
            lstDXPImpDet.ForEach(x =>
            {
                List<EDXPImportacion> lstDXPImpDet3 = new List<EDXPImportacion>();

                lstDXPImpDet3 = new BCompras().listarDXPImpDet(Convert.ToInt32(x.impd_icod_importacion_detalle));
                lstDXPImpDet3.ForEach(xd =>
                {

                    if (xd.tablc_iid_tipo_moneda == 3)
                    {
                        TotalSolesConvertir = Convertir.RedondearNumero(xd.dxpd2_nmonto_importacion / xd.doxpc_nmonto_tipo_cambio);
                        lisImpo.Add(new EDXPImportacion
                        {
                            dxpd2_nmonto_importacion = TotalSolesConvertir,
                            tdocc_icod_tipo_doc = xd.tdocc_icod_tipo_doc

                        });
                    }
                    else
                    {
                        TotalSoles = Convertir.RedondearNumero(xd.dxpd2_nmonto_importacion);
                        lisImpo.Add(new EDXPImportacion
                        {
                            dxpd2_nmonto_importacion = TotalSoles,
                            tdocc_icod_tipo_doc = xd.tdocc_icod_tipo_doc
                        });
                    }
                    if (lisImpo[0].tdocc_icod_tipo_doc == 86)
                    {
                        SumaNCP = lisImpo.Where(xp => xp.tdocc_icod_tipo_doc == 86).Sum(xs => xs.dxpd2_nmonto_importacion);
                        Suma = lisImpo.Where(xp => xp.tdocc_icod_tipo_doc != 86).Sum(xs => xs.dxpd2_nmonto_importacion) - SumaNCP;
                        new BCompras().modificarImportacionConceptosDXPMontoDolares(Convert.ToInt32(xd.impd_icod_importacion_detalle), Suma);
                    }
                    else if (lisImpo[0].tdocc_icod_tipo_doc == 119)
                    {
                        SumaNCI = lisImpo.Where(xp => xp.tdocc_icod_tipo_doc == 119).Sum(xs => xs.dxpd2_nmonto_importacion);
                        Suma = lisImpo.Where(xp => xp.tdocc_icod_tipo_doc != 119).Sum(xs => xs.dxpd2_nmonto_importacion) - SumaNCI;
                        new BCompras().modificarImportacionConceptosDXPMontoDolares(Convert.ToInt32(xd.impd_icod_importacion_detalle), Suma);
                    }
                    else
                    {
                        Suma = lisImpo.Sum(xs => xs.dxpd2_nmonto_importacion);
                        new BCompras().modificarImportacionConceptosDXPMontoDolares(Convert.ToInt32(xd.impd_icod_importacion_detalle), Suma);

                    }




                });
                lisImpo.Clear();
            });


        }
    }
}