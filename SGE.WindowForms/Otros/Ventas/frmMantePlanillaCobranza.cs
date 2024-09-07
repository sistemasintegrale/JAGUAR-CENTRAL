using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmMantePlanillaCobranza : DevExpress.XtraEditors.XtraForm
    {

        public EPlanillaCobranzaCab ObePlnCab = new EPlanillaCobranzaCab();
        public EPlanillaCobranzaDet oBePln = new EPlanillaCobranzaDet();
        public EFacturaCab oBe = new EFacturaCab();
        //  public EFacturaDet oBed = new EFacturaDet();
        public EPagoDocVenta pag = new EPagoDocVenta();
        List<EFacturaDet> lstFacturaDetalle = new List<EFacturaDet>();
        List<EFacturaDet> lstDelete = new List<EFacturaDet>();
        List<EPagoDocVenta> lstPagos = new List<EPagoDocVenta>();
        List<EPagoDocVenta> lstDeletePagos = new List<EPagoDocVenta>();
        private List<ETipoCambio> lstTipoCambio = new List<ETipoCambio>();
        // public Boolean afecto;


        public delegate void DelegadoMensaje(int intIcod, EPlanillaCobranzaCab oBePlnCabReload);
        public event DelegadoMensaje MiEvento;

        public bool flag_MasDeUnaFactura = false;
        public int intClienteOT = 0;
        public Boolean afecto2;
        public int moneda = 0;

        public frmMantePlanillaCobranza()
        {
            InitializeComponent();
        }

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
            lkpTipoDoc.Enabled = !Enabled;
            txtSerie.Enabled = !Enabled;
            txtNumero.Enabled = !Enabled;

            bteCliente.Enabled = !Enabled;
            mnu.Enabled = !Enabled;
            mnuPagos.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            lkpFormaPago.Enabled = !Enabled;
            spinDias.Enabled = !Enabled;
            bteRefreshTipoCambio.Enabled = !Enabled;


            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpTipoDoc.Enabled = Enabled;
                txtSerie.Enabled = Enabled;
                txtNumero.Enabled = Enabled;
                spinDias.Enabled = Enabled;




            }
        }

        private void frmManteVentaPorDia_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            grdDetalle.DataSource = lstFacturaDetalle;
            grdPagos.DataSource = lstPagos;
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaSituacionDocumento), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

            List<TipoDoc> lst = new List<TipoDoc>();
            lst.Add(new TipoDoc { intCodigo = 26, strTipoDoc = "FAV" });
            lst.Add(new TipoDoc { intCodigo = 9, strTipoDoc = "BOV" });
            BSControls.LoaderLook(lkpTipoDoc, lst, "strTipoDoc", "intCodigo", true);

            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda).Where(ob => ob.tarec_iid_tabla_registro != 5).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpFormaPago, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaFormaPago).Where(x => x.tarec_iid_tabla_registro == 116 || x.tarec_iid_tabla_registro == 117
                ).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            /**/

            if (Status == BSMaintenanceStatus.CreateNew)
            {
                getNroDoc();
                getTipoCambio();
            }


        }
        public void setsave2()
        {
            setSave();
        }
        public void setValues()
        {


            if (oBePln.plnd_icod_tipo_doc == Parametros.intTipoDocBoletaVenta)
            {
                EBoletaCab oBeBov = new BVentas().getBoletaCab(Convert.ToInt32(oBePln.plnd_icod_documento))[0];
                oBe = getFactura(oBeBov);


                var lst = new BVentas().listarBoletaDetalle(oBeBov.bovc_icod_boleta);
                lstFacturaDetalle = getFacturaDetalle(lst);
                grdDetalle.DataSource = lstFacturaDetalle;
            }

            #region Cabecera    

            if (oBePln.plnd_icod_tipo_doc == Parametros.intTipoDocFacturaVenta)
                oBe = new BVentas().getFacturaCab(Convert.ToInt32(oBePln.plnd_icod_documento))[0];
            txtSerie.Text = oBe.favc_vnumero_factura.Substring(0, 3);
            txtNumero.Text = oBe.favc_vnumero_factura.Substring(3, 7);
            dtFechaDoc.EditValue = oBe.favc_sfecha_factura;
            lkpSituacion.EditValue = oBe.tablc_iid_situacion;
            bteCliente.Tag = oBe.favc_icod_cliente;
            bteCliente.Text = oBe.cliec_vnombre_cliente;
            txtRUC.Text = oBe.favc_vruc;
            txtDireccion.Text = oBe.favc_vdireccion_cliente;
            lkpMoneda.EditValue = oBe.tablc_iid_tipo_moneda;
            txtMontoTotal.Text = oBe.favc_nmonto_total.ToString();
            lkpTipoDoc.EditValue = oBePln.plnd_icod_tipo_doc;
            lkpFormaPago.EditValue = oBe.tablc_iid_forma_pago;
            if (oBePln.plnd_tipo_cambio == 0)
                oBePln.plnd_tipo_cambio = new BContabilidad().getTipoCambioPorFecha(oBe.favc_sfecha_factura);
            dtFechaVenc.EditValue = oBe.favc_sfecha_vencim_factura;
            txtTipoCambio.Text = oBePln.plnd_tipo_cambio.ToString();
            int intDias = ((TimeSpan)(oBe.favc_sfecha_vencim_factura - oBe.favc_sfecha_factura)).Days;
            spinDias.EditValue = intDias;
            txtObservaciones.Text = oBe.favc_vobservacion;
            txtRazonSocial.Text = oBe.bovc_vnombre_cliente;
            BtnGuiaRemision.Tag = oBe.remic_icod_remision;
            BtnGuiaRemision.Text = oBe.remic_vnumero_remision;
            afectoIGV.Checked = oBe.favc_bincluye_igv;


            #endregion

            if (oBePln.plnd_icod_tipo_doc == Parametros.intTipoDocFacturaVenta)
            {
                lstFacturaDetalle = new BVentas().listarFacturaDetalle(oBe.favc_icod_factura);
                grdDetalle.DataSource = lstFacturaDetalle;
            }
            lstPagos = new BVentas().listarPago(Convert.ToInt32(oBePln.plnd_icod_tipo_doc), Convert.ToInt32(oBePln.plnd_icod_documento));
            grdPagos.DataSource = lstPagos;
            setTotales();


        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            string strMasDeUnaFactura = "";

            try
            {

                if (Convert.ToInt32(bteCliente.Tag) == 0)
                {
                    oBase = bteCliente;
                    throw new ArgumentException("Seleccione Cliente");
                }

                if (lstFacturaDetalle.Count == 0)
                {
                    throw new ArgumentException("Debe ingresar el detalle del documento");
                }

                if (Convert.ToInt32(lkpTipoDoc.EditValue) == 26 && String.IsNullOrWhiteSpace(txtRUC.Text))
                {
                    oBase = txtRUC;
                    throw new ArgumentException("Cliente no cuenta con RUC registrado, favor de registrar RUC del Cliente");
                }

                if (txtSerie.Text == "000")
                {
                    oBase = txtSerie;
                    throw new ArgumentException("N° de Serie no registrado, registrar N° serie en REGISTRO DE TIPOS DE DOCUMENTOS");
                }

                if (Convert.ToDecimal(txtTipoCambio.Text) == 0)
                {

                }

                //if (!flag_MasDeUnaFactura)
                //    if (lstPagos.Count == 0)
                //    {
                //        //oBe.flagGrabrarEnPlanilla = false;
                //        if (XtraMessageBox.Show("No ha ingresado pagos para este documento. El documento solo será grabado en Cuentas Corrientes y no en la Planilla de Cobranza\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t¿Esta seguro que desea continuar con la grabación?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                //        {
                //            Flag = false;
                //            return;
                //        }
                //    }

                int? nullVal = null;
                #region Cabecera del Doc - Factura o Boleta
                oBe.favc_vnumero_factura = String.Format("{0}{1}", txtSerie.Text, txtNumero.Text);
                oBe.favc_sfecha_factura = Convert.ToDateTime(dtFechaDoc.Text);
                oBe.favc_sfecha_vencim_factura = Convert.ToDateTime(dtFechaDoc.Text);
                oBe.favc_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                oBe.clic_vcod_cliente = bteCliente.Text;
                oBe.favc_vdireccion_cliente = txtDireccion.Text;
                oBe.favc_vruc = txtRUC.Text;

                oBe.bovc_vnombre_cliente = txtRazonSocial.Text;

                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                oBe.tablc_iid_forma_pago = Convert.ToInt32(lkpFormaPago.EditValue);
                if (Convert.ToInt32(spinDias.Text) != 0)
                {
                    oBe.favc_sfecha_vencim_factura = Convert.ToDateTime(dtFechaVenc.EditValue);
                }
                else
                {
                    oBe.favc_sfecha_vencim_factura = Convert.ToDateTime(dtFechaDoc.Text);
                }
                oBe.tablc_iid_situacion = Convert.ToInt32(lkpSituacion.EditValue);
                //oBe.favc_npor_imp_igv = Convert.ToDecimal(Parametros.strPorcIGV);
                oBe.favc_nmonto_total = Convert.ToDecimal(txtMontoTotal.Text);
                //oBe.favc_nmonto_neto = Math.Round((Convert.ToDecimal(txtMontoTotal.Text) / Convert.ToDecimal(String.Format("1.{0}", Parametros.strPorcIGV.Replace(".", "")))), 2);
                //oBe.favc_nmonto_imp = oBe.favc_nmonto_total - oBe.favc_nmonto_neto;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                //oBe.strDesCliente = bteCliente.Text;
                oBe.favc_vobservacion = txtObservaciones.Text;
                //oBe.favc_bafecto_igv = afectoIGV.Checked;



                oBe.remic_icod_remision = Convert.ToInt32(BtnGuiaRemision.Tag);
                oBe.remic_vnumero_remision = BtnGuiaRemision.Text;

                if (oBe.favc_bincluye_igv = afectoIGV.Checked)
                {
                    oBe.favc_npor_imp_igv = Convert.ToDecimal(Parametros.strPorcIGV);
                    oBe.favc_nmonto_total = Convert.ToDecimal(txtMontoTotal.Text);
                    oBe.favc_nmonto_neto = Math.Round((Convert.ToDecimal(txtMontoTotal.Text) / Convert.ToDecimal(String.Format("1.{0}", Parametros.strPorcIGV.Replace(".", "")))), 2);
                    oBe.favc_nmonto_imp = oBe.favc_nmonto_total - oBe.favc_nmonto_neto;
                }
                else
                {

                    oBe.favc_npor_imp_igv = 0;
                    oBe.favc_nmonto_imp = 0;
                    oBe.favc_nmonto_neto = oBe.favc_nmonto_total - oBe.favc_nmonto_imp;

                }


                #endregion
                #region Datos de la Planilla
                if (oBePln == null)
                    oBePln = new EPlanillaCobranzaDet();
                oBePln.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                oBePln.plnd_sfecha_doc = Convert.ToDateTime(dtFechaDoc.EditValue);
                oBePln.plnd_icod_tipo_doc = Convert.ToInt32(lkpTipoDoc.EditValue);
                oBePln.plnd_vnumero_doc = txtSerie.Text + txtNumero.Text;
                oBePln.plnd_nmonto = oBe.favc_nmonto_total;
                oBePln.plnd_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);

                //oBePln.tablc_isituacion_planilla = 1;
                oBePln.intUsuario = Valores.intUsuario;
                oBePln.strPc = WindowsIdentity.GetCurrent().Name;
                #endregion

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (Convert.ToInt32(lkpTipoDoc.EditValue) == Parametros.intTipoDocFacturaVenta)
                    {

                        oBe.favc_icod_factura = new BVentas().insertarFacturaDesdePlanilla(oBe, lstFacturaDetalle, ref ObePlnCab, oBePln, lstPagos);
                        ////if (lstFacturaDetalle.Count <= 12)
                        ////{


                        ////}
                        ////else
                        ////{
                        ////    int nroFacturas = Convert.ToInt32(Math.Ceiling((double)(lstFacturaDetalle.Count) / (double)(12)));

                        ////    for (int i = 0; i < nroFacturas; i++)
                        ////    {
                        ////        //DETALLE DE LA FACTURA Y DETALLE DE LA PLANILLA
                        ////        var lstParcial = getFavDetalleParcial(i);
                        ////        oBe.favc_nmonto_total = lstParcial.Sum(x => x.favd_nprecio_total_item);
                        ////        oBe.favc_nmonto_neto = Math.Round((oBe.favc_nmonto_total / Convert.ToDecimal(String.Format("1.{0}", Parametros.strPorcIGV.Replace(".", "")))), 2);
                        ////        oBe.favc_nmonto_imp = oBe.favc_nmonto_total - oBe.favc_nmonto_neto;
                        ////        if (oBePln != null)
                        ////            oBePln.plnd_nmonto = oBe.favc_nmonto_total;

                        ////        if (i > 0)
                        ////        {
                        ////            int intCont = 0;
                        ////            getNroDoc();
                        ////            oBe.favc_vnumero_factura = String.Format("{0}{1}", txtSerie.Text, txtNumero.Text);
                        ////            if (oBePln != null)
                        ////                oBePln.plnd_vnumero_doc = oBe.favc_vnumero_factura;

                        ////            lstParcial.ForEach(x =>
                        ////            {
                        ////                intCont += 1;
                        ////                x.favd_iitem_factura = intCont;
                        ////            });                                   
                        ////        }
                        ////        strMasDeUnaFactura = strMasDeUnaFactura + txtSerie.Text + "-" + txtNumero.Text + " ";
                        ////        oBe.favc_icod_factura = new BVentas().insertarFacturaDesdePlanilla(oBe, lstParcial, ref ObePlnCab, oBePln, lstPagos);                                  

                        ////    }
                        ////}
                    }
                    else if (Convert.ToInt32(lkpTipoDoc.EditValue) == Parametros.intTipoDocBoletaVenta)
                    {
                        EBoletaCab oBeBov = getBoleta(oBe);
                        List<EBoletaDet> lstBoletaDetalle = getBoletaDetalle(lstFacturaDetalle);
                        oBe.favc_icod_factura = new BVentas().insertarBoletaDesdePlanilla(oBeBov, lstBoletaDetalle, ref ObePlnCab, oBePln, lstPagos);

                        ////if (lstBoletaDetalle.Count <= 12)
                        ////{


                        ////}
                        ////else
                        ////{                           
                        ////    int nroFacturas = Convert.ToInt32(Math.Ceiling((double)(lstBoletaDetalle.Count) / (double)(12)));

                        ////    for (int i = 0; i < nroFacturas; i++)
                        ////    {
                        ////        //DETALLE DE LA FACTURA Y DETALLE DE LA PLANILLA
                        ////        var lstParcial = getBovDetalleParcial(i, lstBoletaDetalle);
                        ////        oBeBov.bovc_nmonto_total = lstParcial.Sum(x => x.bovd_nprecio_total_item);
                        ////        oBeBov.bovc_nmonto_neto = Math.Round((oBeBov.bovc_nmonto_total / Convert.ToDecimal(String.Format("1.{0}", Parametros.strPorcIGV.Replace(".", "")))), 2);
                        ////        oBeBov.bovc_nmonto_imp = oBeBov.bovc_nmonto_total - oBeBov.bovc_nmonto_neto;
                        ////        if (oBePln != null)
                        ////            oBePln.plnd_nmonto = oBe.favc_nmonto_total;

                        ////        if (i > 0)
                        ////        {

                        ////            int intCont = 0;
                        ////            getNroDoc();

                        ////            oBeBov.bovc_vnumero_boleta = String.Format("{0}{1}", txtSerie.Text, txtNumero.Text);
                        ////            if (oBePln != null)
                        ////                oBePln.plnd_vnumero_doc = oBeBov.bovc_vnumero_boleta;

                        ////            lstParcial.ForEach(x =>
                        ////            {
                        ////                intCont += 1;
                        ////                x.bovd_iitem_boleta = intCont;
                        ////            });
                        ////        }

                        ////        strMasDeUnaFactura = strMasDeUnaFactura + txtSerie.Text + "-" + txtNumero.Text + " ";

                        ////        oBe.favc_icod_factura = new BVentas().insertarBoletaDesdePlanilla(oBeBov, lstParcial, ref ObePlnCab, oBePln, lstPagos);
                        ////    }
                        ////} 
                    }
                }
                else
                {
                    if (Convert.ToInt32(lkpTipoDoc.EditValue) == Parametros.intTipoDocFacturaVenta)
                        new BVentas().modificarFacturaDesdePlanilla(oBe, lstFacturaDetalle, lstDelete, ObePlnCab, oBePln, lstPagos, lstDeletePagos);
                    else if (Convert.ToInt32(lkpTipoDoc.EditValue) == Parametros.intTipoDocBoletaVenta)
                    {
                        EBoletaCab oBeBov = getBoleta(oBe);
                        List<EBoletaDet> lstBoletaDetalle = getBoletaDetalle(lstFacturaDetalle);
                        List<EBoletaDet> lstDeleteBoletaDet = getBoletaDetalle(lstDelete);
                        new BVentas().modificarBoletaDesdePlanilla(oBeBov, lstBoletaDetalle, lstDeleteBoletaDet, ObePlnCab, oBePln, lstPagos, lstDeletePagos);
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
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    if (strMasDeUnaFactura != "")
                        XtraMessageBox.Show(String.Format("Las {0}´s creadas son: {1}", lkpTipoDoc.Text, strMasDeUnaFactura), "Información del Sistema");
                    MiEvento(oBe.favc_icod_factura, ObePlnCab);
                    Close();
                }
            }
        }

        private List<EFacturaDet> getFavDetalleParcial(int intRango)
        {
            List<EFacturaDet> lstParcial = new List<EFacturaDet>();
            switch (intRango)
            {
                case 0:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura <= 12).ToList();
                    break;
                case 1:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura > 12 && x.favd_iitem_factura <= 24).ToList();
                    break;
                case 2:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura > 24 && x.favd_iitem_factura <= 36).ToList();
                    break;
                case 3:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura > 36 && x.favd_iitem_factura <= 48).ToList();
                    break;
                case 4:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura > 48 && x.favd_iitem_factura <= 60).ToList();
                    break;
                case 5:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura > 60 && x.favd_iitem_factura <= 72).ToList();
                    break;
                case 6:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura > 72 && x.favd_iitem_factura <= 84).ToList();
                    break;
                case 7:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura > 84 && x.favd_iitem_factura <= 96).ToList();
                    break;
                case 8:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura > 96 && x.favd_iitem_factura <= 108).ToList();
                    break;
                case 9:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura > 108 && x.favd_iitem_factura <= 120).ToList();
                    break;
                case 10:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura > 120 && x.favd_iitem_factura <= 132).ToList();
                    break;

            }

            return lstParcial;
        }

        private List<EBoletaDet> getBovDetalleParcial(int intRango, List<EBoletaDet> lstBoletaDet)
        {
            List<EBoletaDet> lstParcial = new List<EBoletaDet>();
            switch (intRango)
            {
                case 0:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta <= 12).ToList();
                    break;
                case 1:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta > 12 && x.bovd_iitem_boleta <= 24).ToList();
                    break;
                case 2:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta > 24 && x.bovd_iitem_boleta <= 36).ToList();
                    break;
                case 3:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta > 36 && x.bovd_iitem_boleta <= 48).ToList();
                    break;
                case 4:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta > 48 && x.bovd_iitem_boleta <= 60).ToList();
                    break;
                case 5:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta > 60 && x.bovd_iitem_boleta <= 72).ToList();
                    break;
                case 6:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta > 72 && x.bovd_iitem_boleta <= 84).ToList();
                    break;
                case 7:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta > 84 && x.bovd_iitem_boleta <= 96).ToList();
                    break;
                case 8:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta > 96 && x.bovd_iitem_boleta <= 108).ToList();
                    break;
                case 9:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta > 108 && x.bovd_iitem_boleta <= 120).ToList();
                    break;
                case 10:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta > 120 && x.bovd_iitem_boleta <= 132).ToList();
                    break;
            }

            return lstParcial;
        }

        private EBoletaCab getBoleta(EFacturaCab oBeFav)
        {
            EBoletaCab oBeBov = new EBoletaCab();
            oBeBov.bovc_icod_boleta = oBeFav.favc_icod_factura;
            oBeBov.bovc_vnumero_boleta = oBeFav.favc_vnumero_factura;
            oBeBov.bovc_sfecha_boleta = oBeFav.favc_sfecha_factura;
            oBeBov.bovc_sfecha_vencim_boleta = oBeFav.favc_sfecha_vencim_factura;
            oBeBov.tablc_iid_situacion = oBeFav.tablc_iid_situacion;
            oBeBov.cliec_icod_cliente = oBeFav.favc_icod_cliente;
            oBeBov.cliec_vcod_cliente = oBeFav.clic_vcod_cliente;
            oBeBov.cliec_vdireccion_cliente = oBeFav.favc_vdireccion_cliente;
            oBeBov.cliec_cruc = oBeFav.favc_vruc;
            oBeBov.tablc_iid_tipo_moneda = oBeFav.tablc_iid_tipo_moneda;
            oBeBov.tablc_iid_forma_pago = oBeFav.tablc_iid_forma_pago;
            oBeBov.tablc_iid_situacion = oBeFav.tablc_iid_situacion;
            oBeBov.bovc_npor_imp_igv = oBeFav.favc_npor_imp_igv;
            oBeBov.bovc_nmonto_neto = oBeFav.favc_nmonto_neto;
            oBeBov.bovc_nmonto_imp = oBeFav.favc_nmonto_imp;
            oBeBov.bovc_nmonto_total = oBeFav.favc_nmonto_total;
            oBeBov.doxcc_icod_correlativo = oBeFav.doxcc_icod_correlativo;
            oBeBov.intUsuario = oBeFav.intUsuario;
            oBeBov.strPc = oBe.strPc;
            oBeBov.anio = Parametros.intEjercicio;
            oBeBov.cliec_vnombre_cliente = oBeFav.cliec_vnombre_cliente;
            oBeBov.bovc_vobservacion = oBeFav.favc_vobservacion;
            oBeBov.bovc_vnombre_cliente = oBeFav.bovc_vnombre_cliente;
            oBeBov.remic_icod_remision = oBeFav.remic_icod_remision;
            oBeBov.remic_vnumero_remision = oBeFav.remic_vnumero_remision;
            oBeBov.bfavc_bafecto_igv = oBeFav.favc_bincluye_igv;
            return oBeBov;
        }

        private EFacturaCab getFactura(EBoletaCab oBeBov_)
        {
            EFacturaCab oBeFav = new EFacturaCab();
            oBeFav.favc_icod_factura = oBeBov_.bovc_icod_boleta;
            oBeFav.favc_vnumero_factura = oBeBov_.bovc_vnumero_boleta;
            oBeFav.favc_sfecha_factura = oBeBov_.bovc_sfecha_boleta;
            oBeFav.favc_sfecha_vencim_factura = oBeBov_.bovc_sfecha_vencim_boleta;
            oBeFav.tablc_iid_situacion = oBeBov_.tablc_iid_situacion;
            oBeFav.favc_icod_cliente = oBeBov_.cliec_icod_cliente;
            oBeFav.clic_vcod_cliente = oBeBov_.cliec_vcod_cliente;
            oBeFav.favc_vdireccion_cliente = oBeBov_.cliec_vdireccion_cliente;
            oBeFav.favc_vruc = oBeBov_.cliec_cruc;
            oBeFav.tablc_iid_tipo_moneda = oBeBov_.tablc_iid_tipo_moneda;
            oBeFav.tablc_iid_forma_pago = oBeBov_.tablc_iid_forma_pago;
            oBeFav.tablc_iid_situacion = oBeBov_.tablc_iid_situacion;
            oBeFav.favc_npor_imp_igv = oBeBov_.bovc_npor_imp_igv;
            oBeFav.favc_nmonto_neto = oBeBov_.bovc_nmonto_neto;
            oBeFav.favc_nmonto_imp = oBeBov_.bovc_nmonto_imp;
            oBeFav.favc_nmonto_total = oBeBov_.bovc_nmonto_total;
            oBeFav.doxcc_icod_correlativo = oBeBov_.doxcc_icod_correlativo;
            oBeFav.intUsuario = oBeBov_.intUsuario;
            oBeFav.strPc = oBeBov_.strPc;
            oBeFav.anio = oBeBov_.anio;
            oBeFav.cliec_vnombre_cliente = oBeBov_.cliec_vnombre_cliente;
            oBeFav.favc_vobservacion = oBeBov_.bovc_vobservacion;
            oBeFav.bovc_vnombre_cliente = oBeBov_.bovc_vnombre_cliente;
            oBeFav.remic_icod_remision = oBeBov_.remic_icod_remision;
            oBeFav.remic_vnumero_remision = oBeBov_.remic_vnumero_remision;
            oBeFav.favc_bincluye_igv = oBeBov_.bfavc_bafecto_igv;
            return oBeFav;
        }

        private List<EBoletaDet> getBoletaDetalle(List<EFacturaDet> lstDetFav)
        {
            List<EBoletaDet> lstBovDetalle = new List<EBoletaDet>();
            lstDetFav.ForEach(x =>
            {
                EBoletaDet oBeDetBov = new EBoletaDet();
                oBeDetBov.bovd_icod_item_boleta = x.favd_icod_item_factura;
                oBeDetBov.bovc_icod_boleta = x.favc_icod_factura;
                oBeDetBov.bovd_iitem_boleta = x.favd_iitem_factura;
                oBeDetBov.almac_icod_almacen = x.almac_icod_almacen;
                oBeDetBov.prdc_icod_producto = x.prdc_icod_producto;
                oBeDetBov.bovd_ncantidad = x.favd_ncantidad;
                oBeDetBov.bovd_vdescripcion = x.favd_vdescripcion;
                oBeDetBov.bovd_nprecio_unitario_item = x.favd_nprecio_unitario_item;
                oBeDetBov.bovd_nmonto_impuesto_item = x.favd_nmonto_impuesto_item;
                oBeDetBov.bovd_nporcentaje_descuento_item = x.favd_nporcentaje_descuento_item;
                oBeDetBov.bovd_nprecio_total_item = x.favd_nprecio_total_item;
                oBeDetBov.kardc_icod_correlativo = x.kardc_icod_correlativo;
                oBeDetBov.intTipoOperacion = x.intTipoOperacion;
                oBeDetBov.intUsuario = x.intUsuario;
                oBeDetBov.strPc = x.strPc;
                oBeDetBov.flagPlanilla = x.flagPlanilla;
                oBeDetBov.tablc_iid_tipo_venta = x.tablc_iid_tipo_venta;
                oBeDetBov.strTipoVenta = x.strTipoVenta;


                lstBovDetalle.Add(oBeDetBov);
            });
            return lstBovDetalle;
        }

        private List<EFacturaDet> getFacturaDetalle(List<EBoletaDet> lstDetBov)
        {
            List<EFacturaDet> lstFavDetalle = new List<EFacturaDet>();
            lstDetBov.ForEach(x =>
            {
                EFacturaDet oBeDetFav = new EFacturaDet();
                oBeDetFav.favd_icod_item_factura = x.bovd_icod_item_boleta;
                oBeDetFav.favc_icod_factura = x.bovc_icod_boleta;
                oBeDetFav.favd_iitem_factura = x.bovd_iitem_boleta;
                oBeDetFav.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                oBeDetFav.prdc_icod_producto = x.prdc_icod_producto;
                oBeDetFav.favd_ncantidad = x.bovd_ncantidad;
                oBeDetFav.favd_vdescripcion = x.bovd_vdescripcion;
                oBeDetFav.favd_nprecio_unitario_item = x.bovd_nprecio_unitario_item;
                oBeDetFav.favd_nmonto_impuesto_item = x.bovd_nmonto_impuesto_item;
                oBeDetFav.favd_nporcentaje_descuento_item = x.bovd_nporcentaje_descuento_item;
                oBeDetFav.favd_nprecio_total_item = x.bovd_nprecio_total_item;
                oBeDetFav.kardc_icod_correlativo = x.kardc_icod_correlativo;
                oBeDetFav.intTipoOperacion = x.intTipoOperacion;
                oBeDetFav.intUsuario = x.intUsuario;
                oBeDetFav.strPc = x.strPc;
                oBeDetFav.flagPlanilla = x.flagPlanilla;
                oBeDetFav.strCodProducto = x.strCodProducto;
                oBeDetFav.strDesProducto = x.strDesProducto;
                oBeDetFav.strCategoria = x.strCategoria;
                oBeDetFav.strSubCategoriaUno = x.strSubCategoriaUno;
                oBeDetFav.dblStockDisponible = x.dblStockDisponible;
                oBeDetFav.strDesUM = x.strDesUM;
                oBeDetFav.strAlmacen = x.strAlmacen;
                oBeDetFav.tablc_iid_tipo_venta = x.tablc_iid_tipo_venta;
                oBeDetFav.strTipoVenta = x.strTipoVenta;
                lstFavDetalle.Add(oBeDetFav);
            });
            return lstFavDetalle;
        }

        private void getNroDoc()
        {
            try
            {
                if (Convert.ToInt32(lkpTipoDoc.EditValue) == Parametros.intTipoDocFacturaVenta)
                {
                    var lst = new BAdministracionSistema().getCorrelativoTipoDoc(Parametros.intTipoDocFacturaVenta, (txtSerie.Text).Trim());
                    //if (!cbPucusana.Checked)
                    //{
                    if (Convert.ToInt32(lst[0].tdocc_nro_serie) != 0)
                    {
                        txtSerie.Text = lst[0].tdocc_nro_serie;
                        txtNumero.Text = (Convert.ToInt32(lst[0].tdocc_nro_correlativo) + 1).ToString();
                    }

                    //txtSerie.Text = lst[0].tdocc_nro_serie;
                    //txtNumero.Text = (Convert.ToInt32(lst[0].tdocc_nro_correlativo) + 1).ToString();
                    //}
                    //else
                    //{
                    //    if (Convert.ToInt32(lst[0].tdocc_nro_serie_2) == 0)
                    //    {
                    //        txtSerie.Text = "";
                    //        txtNumero.Text = (Convert.ToInt32(lst[0].tdocc_nro_correlativo_2) + 1).ToString();
                    //        throw new ArgumentException("El N° de Serie del documento seleccionado no se encuentra registrado. \nNota: Registrar N° de Serie en la opción REGISTRO DE TIPOS DE DOCUMENTOS");
                    //    }

                    //    txtSerie.Text = lst[0].tdocc_nro_serie_2;
                    //    txtNumero.Text = (Convert.ToInt32(lst[0].tdocc_nro_correlativo_2) + 1).ToString();
                    //}
                }
                else if (Convert.ToInt32(lkpTipoDoc.EditValue) == Parametros.intTipoDocBoletaVenta)
                {
                    var lst = new BAdministracionSistema().getCorrelativoTipoDoc(Parametros.intTipoDocBoletaVenta, txtSerie.Text);
                    //if (!cbPucusana.Checked)
                    //{                        
                    if (Convert.ToInt32(lst[0].tdocc_nro_serie) != 0)
                    {
                        txtSerie.Text = lst[0].tdocc_nro_serie;
                        txtNumero.Text = (Convert.ToInt32(lst[0].tdocc_nro_correlativo) + 1).ToString();
                    }
                    //}
                    //else
                    //{
                    //    if (Convert.ToInt32(lst[0].tdocc_nro_serie_2) == 0)
                    //    {
                    //        txtSerie.Text = "";
                    //        txtNumero.Text = (Convert.ToInt32(lst[0].tdocc_nro_correlativo_2) + 1).ToString();
                    //        throw new ArgumentException("El N° de Serie del documento seleccionado no se encuentra registrado. \nNota: Registrar N° de Serie en la opción REGISTRO DE TIPOS DE DOCUMENTOS");
                    //    }
                    //    txtSerie.Text = lst[0].tdocc_nro_serie_2;
                    //    txtNumero.Text = (Convert.ToInt32(lst[0].tdocc_nro_correlativo_2) + 1).ToString();
                    //}
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public class TipoDoc
        {
            public int intCodigo { get; set; }
            public string strTipoDoc { get; set; }
        }

        private void clearControls()
        {
            bteCliente.Tag = null;
            bteCliente.Text = String.Empty;
            txtDNI.Text = String.Empty;
            txtRUC.Text = String.Empty;
            txtDireccion.Text = String.Empty;

        }

        private void nuevoItem()
        {
            //using (frmManteFacturaDetalle frm = new frmManteFacturaDetalle())
            //{

            //    frm.SetInsert();
            //    frm.CargarCombos();
            //    frm.lstFacturaDetalle = lstFacturaDetalle;
            //    frm.txtMoneda.Text = lkpMoneda.Text;
            //    frm.tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
            //    frm.afecto = Convert.ToBoolean(afectoIGV.Checked);
            //    frm.txtItem.Text = (lstFacturaDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstFacturaDetalle.Count + 1);
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        lstFacturaDetalle = frm.lstFacturaDetalle;
            //        viewDetalle.RefreshData();
            //        viewDetalle.MoveLast();
            //        setTotales();

            //    }
            //}

        }

        private void nuevoServicio()
        {
            using (frmManteFacturaServicioDetalle frm = new frmManteFacturaServicioDetalle())
            {
                frm.SetInsert();

                frm.lstFacturaDetalle = lstFacturaDetalle;
                frm.lkpMoneda.EditValue = lkpMoneda.EditValue;
                frm.txtItem.Text = (lstFacturaDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstFacturaDetalle.Count + 1);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstFacturaDetalle = frm.lstFacturaDetalle;
                    viewDetalle.RefreshData();
                    viewDetalle.MoveLast();
                    setTotales();

                }
            }
        }

        private void modificarItem()
        {
            //EFacturaDet obe = (EFacturaDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            //if (obe == null)
            //    return;
            //using (frmManteFacturaDetalle frm = new frmManteFacturaDetalle())
            //{

            //    frm.obe = obe;
            //    frm.dblStockDisponible = Convert.ToDecimal(obe.dblStockDisponible);
            //    frm.lstFacturaDetalle = lstFacturaDetalle;
            //    frm.SetModify();
            //    frm.CargarCombos();
            //    frm.txtMoneda.Text = lkpMoneda.Text;
            //    frm.txtItem.Text = String.Format("{0:000}", obe.favd_iitem_factura);
            //    frm.bteProducto.Properties.ReadOnly = true;
            //    frm.lkpTipoVenta.EditValue = obe.tablc_iid_tipo_venta;
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        lstFacturaDetalle = frm.lstFacturaDetalle;
            //        viewDetalle.RefreshData();
            //        viewDetalle.MoveLast();
            //        setTotales();
            //    }
            //}
        }

        private void modificarServicio()
        {
            EFacturaDet obe = (EFacturaDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteFacturaServicioDetalle frm = new frmManteFacturaServicioDetalle())
            {
                frm.obe = obe;
                frm.lstFacturaDetalle = lstFacturaDetalle;
                frm.SetModify();
                frm.lkpMoneda.EditValue = lkpMoneda.EditValue;
                frm.txtItem.Text = String.Format("{0:000}", obe.favd_iitem_factura);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstFacturaDetalle = frm.lstFacturaDetalle;
                    viewDetalle.RefreshData();
                    viewDetalle.MoveLast();
                    setTotales();
                }
            }
        }

        private void eliminarItem()
        {
            EFacturaDet obe = (EFacturaDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstFacturaDetalle.Remove(obe);
            viewDetalle.RefreshData();
            setTotales();
        }

        private void nuevoPago()
        {
            using (frmPagoPlanilla frm = new frmPagoPlanilla())
            {
                frm.oBe.pgoc_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                frm.oBe.pgoc_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                frm.oBe.tdocc_icoc_tipo_documento = Parametros.intTipoDocPlanillaVenta;
                frm.oBe.pgoc_vnumero_planilla = ObePlnCab.plnc_vnumero_planilla;
                frm.dblTotal = Convert.ToDecimal(txtResta.Text);
                frm.lstPagos = lstPagos;
                frm.SetInsert();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstPagos = frm.lstPagos;
                    viewPagos.RefreshData();
                    setTotales();
                }
            }
        }

        private void modificarPago()
        {
            EPagoDocVenta oBe = (EPagoDocVenta)viewPagos.GetRow(viewPagos.FocusedRowHandle);
            if (oBe == null)
                return;
            using (frmPagoPlanilla frm = new frmPagoPlanilla())
            {
                frm.dblTotal = Convert.ToDecimal(txtResta.Text);
                frm.lstPagos = lstPagos;
                frm.oBe = oBe;
                frm.SetModify();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstPagos = frm.lstPagos;
                    viewPagos.RefreshData();
                    setTotales();
                }
            }
        }

        private void eliminarPago()
        {
            EPagoDocVenta oBe = (EPagoDocVenta)viewPagos.GetRow(viewPagos.FocusedRowHandle);
            if (oBe == null)
                return;
            if (XtraMessageBox.Show("¿Está seguro que desea eliminar el pago?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lstPagos.Remove(oBe);
                setTotales();
                viewPagos.RefreshData();
            }
        }

        //private void setTotales()
        //{

        //    // txtMontoTotal.Text = lstFacturaDetalle.Where(x => x.strTipoVenta == "NINGUNO").ToList().Sum(x => x.favd_nprecio_total_item).ToString();
        //    txtMontoTotal.Text = lstFacturaDetalle.Where(x => x.tablc_iid_tipo_venta == 266).ToList().Sum(x => x.favd_nprecio_total_item).ToString();

        //    /****************************************************************************************************/
        //    decimal dcmlMontoPagado = 0;
        //    decimal dcmlMontoAux = 0;
        //    if (lstPagos.Where(x => x.pgoc_icod_tipo_moneda == Parametros.intTipoMonedaSoles).ToList().Count > 0)
        //    {
        //        lstPagos.ForEach(x =>
        //        {
        //            dcmlMontoAux = (x.pgoc_icod_tipo_moneda == Parametros.intTipoMonedaDolares) ? x.pgoc_nmonto * x.pgoc_tipo_cambio : x.pgoc_nmonto;
        //            dcmlMontoPagado = dcmlMontoPagado + dcmlMontoAux;
        //        });
        //        txtPago.Text = dcmlMontoPagado.ToString();
        //    }
        //    else
        //        txtPago.Text = lstPagos.Sum(x => x.pgoc_nmonto).ToString();
        //    /****************************************************************************************************/
        //    txtResta.Text = (Convert.ToDecimal(txtMontoTotal.Text) - Convert.ToDecimal(txtPago.Text)).ToString();
        //}


        private void setTotales()
        {

            // txtMontoTotal.Text = lstFacturaDetalle.Where(x => x.strTipoVenta == "NINGUNO").ToList().Sum(x => x.favd_nprecio_total_item).ToString();
            txtMontoTotal.Text = lstFacturaDetalle.Where(x => x.tablc_iid_tipo_venta == 266).ToList().Sum(x => x.favd_nprecio_total_item).ToString();

            /****************************************************************************************************/
            decimal dcmlMontoPagado = 0;
            decimal dcmlMontoAux = 0;
            oBe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
            if (lstPagos.ToList().Count > 0)
            {

                lstPagos.ForEach(x =>
                {
                    if (x.pgoc_icod_tipo_moneda == oBe.tablc_iid_tipo_moneda)
                    {
                        dcmlMontoPagado = dcmlMontoPagado + x.pgoc_nmonto;
                    }
                    else
                    {
                        if (x.pgoc_icod_tipo_moneda == Parametros.intTipoMonedaSoles)
                        {
                            dcmlMontoAux = (x.pgoc_icod_tipo_moneda == Parametros.intTipoMonedaSoles) ? x.pgoc_nmonto / x.pgoc_tipo_cambio : x.pgoc_nmonto;
                            dcmlMontoPagado = dcmlMontoPagado + dcmlMontoAux;
                        }
                        else
                        {
                            dcmlMontoAux = (x.pgoc_icod_tipo_moneda == Parametros.intTipoMonedaDolares) ? x.pgoc_nmonto * x.pgoc_tipo_cambio : x.pgoc_nmonto;
                            dcmlMontoPagado = dcmlMontoPagado + dcmlMontoAux;
                        }
                    }
                });
                txtPago.Text = dcmlMontoPagado.ToString();
            }
            else
                txtPago.Text = lstPagos.Sum(x => x.pgoc_nmonto).ToString();
            /****************************************************************************************************/
            txtResta.Text = (Convert.ToDecimal(txtMontoTotal.Text) - Convert.ToDecimal(txtPago.Text)).ToString();
        }





        ////private void setTotales()
        ////{

        ////    // txtMontoTotal.Text = lstFacturaDetalle.Where(x => x.strTipoVenta == "NINGUNO").ToList().Sum(x => x.favd_nprecio_total_item).ToString();
        ////    txtMontoTotal.Text = lstFacturaDetalle.Where(x => x.tablc_iid_tipo_venta == 266).ToList().Sum(x => x.favd_nprecio_total_item).ToString();

        ////    /****************************************************************************************************/
        ////    decimal dcmlMontoPagado = 0;
        ////    decimal dcmlMontoAux = 0;


        ////        if (lstPagos.Where(x => x.pgoc_icod_tipo_moneda == Parametros.intTipoMonedaSoles).ToList().Count > 0)
        ////    {
        ////        lstPagos.ForEach(x =>
        ////        {
        ////            dcmlMontoAux = (x.pgoc_icod_tipo_moneda == Parametros.intTipoMonedaDolares) ? x.pgoc_nmonto * x.pgoc_tipo_cambio : x.pgoc_nmonto;
        ////            dcmlMontoPagado = dcmlMontoPagado + dcmlMontoAux;
        ////        });
        ////        txtPago.Text = dcmlMontoPagado.ToString();
        ////    }
        ////        //else 
        ////        //    txtPago.Text = lstPagos.Sum(x => x.pgoc_nmonto).ToString();
        ////        //{
        ////        //    if (lstPagos.Where(x => x.pgoc_icod_tipo_moneda == Parametros.intTipoMonedaSoles).ToList().Count > 0)
        ////        //    {
        ////        //        txtPago.Text = lstPagos.Sum(x => x.pgoc_nmonto * x.pgoc_tipo_cambio).ToString();
        ////        //    }
        ////        //    else
        ////        //    {
        ////        //        txtPago.Text = lstPagos.Sum(x => x.pgoc_nmonto / x.pgoc_tipo_cambio).ToString();
        ////        //    }
        ////        //}
        ////        //    txtPago.Text = lstPagos.Sum(x => x.pgoc_nmonto).ToString();

        ////        //if (lstPagos.Where(x => x.pgoc_icod_tipo_moneda == Parametros.intTipoMonedaSoles).ToList().Count > 0)
        ////        //{
        ////        //    lstPagos.ForEach(x =>
        ////        //    {
        ////        //        dcmlMontoAux = (x.pgoc_icod_tipo_moneda == Parametros.intTipoMonedaDolares) ? x.pgoc_nmonto * x.pgoc_tipo_cambio : x.pgoc_nmonto;
        ////        //        dcmlMontoPagado = dcmlMontoPagado + dcmlMontoAux;
        ////        //    });
        ////        //    txtPago.Text = dcmlMontoPagado.ToString();
        ////        //}
        ////        //else
        ////        //    txtPago.Text = lstPagos.Sum(x => x.pgoc_nmonto * x.pgoc_tipo_cambio).ToString();

        ////    ///****************************************************************************************************/
        ////        txtResta.Text = (Convert.ToDecimal(txtMontoTotal.Text) - Convert.ToDecimal(txtPago.Text)).ToString();
        ////}



        private void lkpTipoDoc_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                getNroDoc();
            }
        }

        private void listarCliente()
        {
            try
            {
                using (FrmListarCliente frm = new FrmListarCliente())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteCliente.Tag = frm._Be.cliec_icod_cliente;
                        bteCliente.Text = frm._Be.cliec_vnombre_cliente;
                        txtDireccion.Text = frm._Be.cliec_vdireccion_cliente;
                        txtDNI.Text = frm._Be.cliec_vnumero_doc_cli;
                        txtRUC.Text = frm._Be.cliec_cruc;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setCliente(int intCliente)
        {
            try
            {
                var _Be = new BVentas().ListarCliente().Where(x => x.cliec_icod_cliente == intCliente).ToList()[0];
                bteCliente.Tag = _Be.cliec_icod_cliente;
                bteCliente.Text = _Be.cliec_vnombre_cliente;
                txtDireccion.Text = _Be.cliec_vdireccion_cliente;
                txtRUC.Text = _Be.cliec_cruc;
                //txtTelefono.Text = _Be.cliec_vnro_telefono;
                //strCodCliente = _Be.cliec_vcod_cliente;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void listarVehiculo()
        {
            BaseEdit oBase = null;
            try
            {


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

        private void bteCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCliente();
        }

        private void btePlaca_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarVehiculo();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevoItem();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EFacturaDet obe = (EFacturaDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;

            modificarItem();

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminarItem();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            nuevoPago();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            modificarPago();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            eliminarPago();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void lkpFormaPago_EditValueChanged(object sender, EventArgs e)
        {
            if (Status != BSMaintenanceStatus.View)
            {
                if (Convert.ToInt32(lkpFormaPago.EditValue) != 1)
                    spinDias.Enabled = true;
                else
                {
                    spinDias.Enabled = false;
                    spinDias.EditValue = "0";
                    dtFechaVenc.EditValue = dtFechaDoc.EditValue;
                }
            }
        }

        private void spinDias_EditValueChanged(object sender, EventArgs e)
        {
            dtFechaVenc.EditValue = Convert.ToDateTime(dtFechaDoc.EditValue).AddDays(Convert.ToInt32(spinDias.EditValue));
        }

        public void getTipoCambio()
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                lstTipoCambio = new BAdministracionSistema().listarTipoCambio();
                var Lista = lstTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dtFechaDoc.EditValue).ToShortDateString()).ToList();
                Lista.ForEach(obe =>
                {
                    txtTipoCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
                });
                //if (lstDetalle.Count > 0)
                //    recalcular();
            }
        }



        private void bteRefreshTipoCambio_Click(object sender, EventArgs e)
        {
            getTipoCambio();
        }

        private void cbPucusana_CheckedChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                getNroDoc();
            }
        }

        private void nuevoServicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevoServicio();
        }

        private void bteCliente_EditValueChanged(object sender, EventArgs e)
        {

            if (Convert.ToInt32(bteCliente.Tag) == 12578)
            {
                txtRazonSocial.Properties.ReadOnly = false;
            }
            else
            {
                txtRazonSocial.Properties.ReadOnly = true;
            }

        }

        private void BtnGuiaRemision_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarGuiaRemision();
        }

        private void listarGuiaRemision()
        {
            try
            {
                using (FrmListarGuiaRemision frm = new FrmListarGuiaRemision())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstFacturaDetalle.Clear();
                        BtnGuiaRemision.Tag = frm._Be.remic_icod_remision;
                        BtnGuiaRemision.Text = frm._Be.remic_vnumero_remision;

                        List<EGuiaRemisionDet> mListGuiaDet = new List<EGuiaRemisionDet>();
                        mListGuiaDet = new BVentas().listarGuiaRemisionDet(frm._Be.remic_icod_remision, Parametros.intEjercicio);
                        foreach (var _BEguia in mListGuiaDet)
                        {
                            EFacturaDet _BEe = new EFacturaDet();
                            _BEe.prdc_icod_producto = _BEguia.prdc_icod_producto;
                            _BEe.favd_iitem_factura = _BEguia.dremc_inro_item;
                            _BEe.strCodProducto = _BEguia.strCodProducto;
                            _BEe.favd_vdescripcion = _BEguia.strDesProducto;
                            _BEe.favd_nprecio_unitario_item = _BEguia.dremc_nprecio_lista;
                            _BEe.favd_ncantidad = (_BEguia.dremc_ncantidad_producto);
                            _BEe.favd_nmonto_impuesto_item = _BEguia.dremc_nprecio_lista;
                            _BEe.favd_nporcentaje_descuento_item = _BEguia.dremc_nDescuento;
                            _BEe.favd_nprecio_total_item = _BEguia.dremc_nMonto_Total;
                            _BEe.favd_nobservaciones = _BEguia.dremc_vobservaciones;
                            _BEe.intUsuario = Valores.intUsuario;
                            _BEe.almac_icod_almacen = frm._Be.almac_icod_almacen;
                            _BEe.strAlmacen = frm._Be.strDesAlmacen;

                            _BEe.tablc_iid_tipo_venta = _BEguia.gtablc_iid_tipo_venta;
                            _BEe.strTipoVenta = _BEguia.StrTipoVenta;

                            _BEe.strDesProducto = _BEguia.strDesProducto;
                            _BEe.prdc_vpart_number = _BEguia.prdc_vpart_number;
                            _BEe.strDesUM = _BEguia.strDesUM;
                            _BEe.strCategoria = _BEguia.strCategoria;
                            _BEe.strSubCategoriaUno = _BEguia.strSubCategoriaUno;


                            _BEe.intTipoOperacion = 1;
                            lstFacturaDetalle.Add(_BEe);

                        }
                        //nuevoToolStripMenuItem.Enabled = false;
                        //eliminarToolStripMenuItem1.Enabled = false;
                        //nuevoServicioToolStripMenuItem.Enabled = false;
                        viewDetalle.RefreshData();
                        setTotales();
                        //setTotal();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSerie_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
                getNroDoc();
        }
        string filePath = "";
        private void importarProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            if (DialogResult.OK == ofd.ShowDialog(this))
            {
                string Extension = Path.GetExtension(ofd.FileName);

                if (Extension == ".xlsx")
                {
                    filePath = ofd.FileName;
                    ImportarDatosExcel();
                }
                else
                {
                    //ClearLista();
                    //XtraMessageBox.Show("El archivo no coincide con el tipo de archivo seleccionado " + lkpTipoArchivo.Text, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void ImportarDatosExcel()
        {
            //ClearLista();

            DataTable dt;
            OleDbDataAdapter MyDataAdapter;

            string connString = "provider=Microsoft.ACE.OLEDB.12.0;" + @"data source=" + filePath + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection oledbConn = new OleDbConnection(connString);
            try
            {

                MyDataAdapter = new OleDbDataAdapter("SELECT * FROM [Sheet$]", connString);
                dt = new DataTable();
                MyDataAdapter.Fill(dt);
                FillList(dt);


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                oledbConn.Close();
            }
        }
        private void FillList(DataTable dt)
        {
            List<EInventarioDet> lstDetalleaUX = new List<EInventarioDet>();

            int contF = 0;
            string nomC = String.Empty;
            try
            {
                if (dt.Rows.Count > 0)
                {
                    int b = dt.Rows.Count;
                    foreach (DataRow row in dt.Rows)
                    {
                        contF++;
                        EInventarioDet obe = new EInventarioDet();
                        EProducto oBed = new EProducto();
                        foreach (DataColumn column in dt.Columns)
                        {

                            switch (column.ToString().ToUpper().Trim())
                            {

                                case "CÓDIGO":
                                    nomC = "CÓDIGO";
                                    obe.strCodProducto = row[column].ToString();
                                    break;
                                case "CANTIDAD":
                                    nomC = "CANTIDAD";
                                    obe.invd_icantidad = Convert.ToInt32(row[column]);
                                    break;
                                case "VENTA":
                                    nomC = "VENTA";
                                    obe.dcPrecioProducto = Convert.ToDecimal(row[column]);
                                    break;
                                case "DESCRIPCION":
                                    nomC = "DESCRIPCION";
                                    obe.strDesProducto = row[column].ToString();
                                    break;
                            }
                        }

                        obe.intUsuario = Valores.intUsuario;
                        obe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                        lstDetalleaUX.Add(obe);

                    }
                }

                foreach (var _BEE in lstDetalleaUX)
                {
                    List<EProducto> MlistProduc = new BAlmacen().listarProductoXCodigp(Parametros
                        .intEjercicio, _BEE.strCodProducto.Trim());
                    if (MlistProduc.Count > 0)
                    {
                        EFacturaDet obe = new EFacturaDet();
                        obe.almac_icod_almacen = 10;//ALMACEN CENTRAL
                        if (lstFacturaDetalle.Count() == 0)
                        {
                            obe.favd_iitem_factura = 1;
                        }
                        else
                            obe.favd_iitem_factura = Convert.ToInt16(lstFacturaDetalle.Count() + 1);
                        obe.prdc_icod_producto = Convert.ToInt32(MlistProduc[0].prdc_icod_producto);
                        // obe.prdc_icod_producto = Convert.ToInt32(_BEE.strCodProducto);
                        obe.favd_ncantidad = Convert.ToDecimal(_BEE.invd_icantidad);
                        obe.favd_vdescripcion = MlistProduc[0].prdc_vdescripcion_larga;
                        obe.favd_nmonto_impuesto_item = Convert.ToDecimal(0);
                        obe.favd_nporcentaje_descuento_item = Convert.ToDecimal(MlistProduc[0].prdc_nPor_Descuento);

                        // obe.favd_nprecio_unitario_item = Convert.ToDecimal(MlistProduc[0].prdc_nPrecio_venta);
                        obe.favd_nprecio_unitario_item = Convert.ToDecimal(_BEE.dcPrecioProducto);
                        obe.favd_nprecio_total_item = Convert.ToDecimal(obe.favd_nprecio_unitario_item) * Convert.ToDecimal(_BEE.invd_icantidad);
                        /**/
                        obe.strCodProducto = MlistProduc[0].prdc_vcode_producto;
                        obe.strDesUM = MlistProduc[0].StrUnidadMedida;
                        //obe.strDesProducto = MlistProduc[0].prdc_vdescripcion_larga;
                        obe.strDesProducto = _BEE.strDesProducto;
                        obe.strAlmacen = "ALMACÉN CENTRAL";
                        obe.dblStockDisponible = 0;
                        obe.dblMontoDescuento = (obe.favd_ncantidad * obe.favd_nprecio_unitario_item) - obe.favd_nprecio_total_item;
                        obe.strMoneda = lkpMoneda.Text;
                        obe.strTipoVenta = "NINGUNO";
                        obe.tablc_iid_tipo_venta = 266;

                        obe.intUsuario = Valores.intUsuario;
                        obe.strPc = WindowsIdentity.GetCurrent().Name;
                        obe.flagPlanilla = true;
                        obe.prdc_vpart_number = "";

                        string Descripci = "";
                        string DescripciExtra = "";
                        obe.favd_nobservaciones = Descripci;


                        obe.strCategoria = MlistProduc[0].strCategoria; ;
                        obe.strSubCategoriaUno = MlistProduc[0].strSubCategoriaUno;
                        obe.intTipoOperacion = 1;


                        lstFacturaDetalle.Add(obe);
                    }

                }
                setTotales();
                viewDetalle.RefreshData();
                viewDetalle.MoveLast();



                List<EInventarioDet> mlistNuevos = new List<EInventarioDet>();
                foreach (var BE in lstDetalleaUX)
                {
                    List<EProducto> MlistProduc = new BAlmacen().listarProductoXCodigp(Parametros.intEjercicio, BE.strCodProducto.Trim());
                    if (MlistProduc.Count() == 0)
                    {
                        mlistNuevos.Add(BE);
                    }
                }
                lstFacturaDetalle = lstFacturaDetalle.Where(ob => ob.prdc_icod_producto != 0).ToList();

                grdDetalle.DataSource = lstFacturaDetalle;
                viewDetalle.RefreshData();

                if (mlistNuevos.Count > 0)
                {
                    if (XtraMessageBox.Show("¿Desea Exportar los que no existen en el catálogo de productos?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ExportarDatos(mlistNuevos);
                    }
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error Fila: " + contF + "\t Columna: " + nomC, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void afectoIGV_CheckedChanged(object sender, EventArgs e)
        {
            if (!afectoIGV.Checked)
            {


                txtIGV.Text = "0.0";

            }
            else
            {
                txtIGV.Text = Parametros.strPorcIGV;
            }

        }

        private void ExportarDatos(List<EInventarioDet> Mlist)
        {
            try
            {
                if (sfdTXT.ShowDialog(this) == DialogResult.OK)
                {
                    string fileName = sfdTXT.FileName;
                    if (!fileName.Contains(".txt"))
                    {
                        ExportarATXT(fileName + ".txt", Mlist);
                        System.Diagnostics.Process.Start(fileName + ".txt");
                    }
                    else
                    {
                        ExportarATXT(fileName, Mlist);
                        System.Diagnostics.Process.Start(fileName);
                    }
                }
                sfdTXT.FileName = string.Empty;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportarATXT(String ruta, List<EInventarioDet> Mlist)
        {
            StreamWriter sw = new StreamWriter(ruta);
            string error = string.Empty;
            int cont = 0;
            string columna = string.Empty;
            try
            {
                int totFilas = Mlist.Count;
                foreach (EInventarioDet item in Mlist)
                {
                    cont++;
                    error = item.strCodProducto;
                    columna = "1";
                    sw.Write(item.strCodProducto + "|"); // 1
                    columna = "2";
                    sw.Write(item.invd_icantidad + "|"); // 2
                    columna = "3";
                    sw.Write(item.dcPrecioProducto + "|"); // A: Aptertura M: Mes C:Cierre // 3
                    columna = "4";
                    sw.Write(item.strDesProducto + "|"); // 4



                    if (totFilas != cont)
                    {
                        sw.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message + "\nFila " + cont + "\nDocumento " + error + "\nColumna Nº " + columna);
            }
            finally
            {
                sw.Close();
            }
        }

        private void mnuPagos_Opening(object sender, CancelEventArgs e)
        {

        }

        private void txtTipoCambio_EditValueChanged(object sender, EventArgs e)
        {

        }

    }
}