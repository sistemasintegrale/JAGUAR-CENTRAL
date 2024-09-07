using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Cuentas_por_Cobrar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmManteNotaCreditoSuministros : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        /**/
        public ENotaCredito oBe = new ENotaCredito();
        List<ENotaCreditoSuministrosDet> lstDetalle = new List<ENotaCreditoSuministrosDet>();
        List<ENotaCreditoSuministrosDet> lstDelete = new List<ENotaCreditoSuministrosDet>();
        /**/
        public List<ENotaCredito> lstCabeceras = new List<ENotaCredito>();//este listado se utiliza para comparar si ya existe el nro. de nc que se esta registrando
        private List<ETipoCambio> lstTipoCambio = new List<ETipoCambio>();
        public string DireccionCliente;
        public int icod_dxc;
        public int IndicadorRef;
        public int TDComercial = 0;
        List<EPuntoVenta> lstPuntoVenta = new List<EPuntoVenta>();

        private void FrmManteNotaCredito_Load(object sender, EventArgs e)
        {
            cargar();
            grdNC.DataSource = lstDetalle;
            lstPuntoVenta = new BCentral().ListarPuntoVenta().Where(x => x.puvec_icod_punto_venta == 2).ToList();
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
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtSerie.Enabled = Enabled;
                txtNumero.Enabled = Enabled;
                dteFecha.Enabled = Enabled;
                lkpSituacion.Enabled = Enabled;
                txtIgv.Enabled = Enabled;
                lkpMoneda.Enabled = false;
            }
        }

        public void setValues()
        {
            #region Setting values (cabecera...)
            cbIncluyeIGV.Checked = Convert.ToBoolean(oBe.ncrec_bincluye_igv);
            txtSerie.Text = oBe.ncrec_vnumero_credito.Substring(0, 4);
            txtNumero.Text = oBe.ncrec_vnumero_credito.Substring(4, 8);
            dteFecha.EditValue = oBe.ncrec_sfecha_credito;
            lkpSituacion.EditValue = oBe.ncrec_iid_situacion_credito;
            bteCliente.Tag = oBe.cliec_icod_cliente;
            bteCliente.Text = oBe.strDesCliente;
            txtRuc.Text = oBe.strRuc;
            bteNroDoc.Text = oBe.ncrec_vnumero_documento;
            lkpTipoDoc.EditValue = oBe.tdocc_icod_tipo_doc;
            dteFechaDoc.EditValue = oBe.ncrec_sfecha_documento;
            lkpMoneda.EditValue = oBe.tablc_iid_tipo_moneda;
            txtIgv.Text = oBe.ncrec_npor_imp_igv.ToString();
            txtMontoNeto.Text = oBe.ncrec_nmonto_neto.ToString();
            txtMontoTotal.Text = oBe.ncrec_nmonto_total.ToString();
            lkpMotivoSUNAT.EditValue = oBe.ncrec_vmotivo_sunat;
            txtDetalle.Text = oBe.ncrec_vdetalle;
            DireccionCliente = oBe.DirecionCliente;
            #endregion
            lstDetalle = new BVentas().listarNotaCreditoSuministrosClienteDet(oBe.ncrec_icod_credito);
            grdNC.DataSource = lstDetalle;
            viewNC.RefreshData();
        }

        public FrmManteNotaCreditoSuministros()
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
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
            SetCancel();
        }

        private void cargar()
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaSituacionDocumento), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMotivoSUNAT, new BVentas().TablasSunatDetListar(1), "suntd_vdescripcion", "suntd_codigo", true);
            lstTipoCambio = new BAdministracionSistema().listarTipoCambio();
            #region load tipo doc
            List<ETipoDocumento> lstTD = new List<ETipoDocumento>();
            for (int i = 0; i < 3; i++)
            {
                ETipoDocumento oBeTD = new ETipoDocumento();
                switch (i)
                {
                    case 0:
                        oBeTD.tdocc_icod_tipo_doc = 0;
                        oBeTD.tdocc_vabreviatura_tipo_doc = "";
                        break;
                    case 1:
                        oBeTD.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                        oBeTD.tdocc_vabreviatura_tipo_doc = "FAV";
                        break;
                    case 2:
                        oBeTD.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                        oBeTD.tdocc_vabreviatura_tipo_doc = "BOV";
                        break;
                }
                lstTD.Add(oBeTD);
            }
            BSControls.LoaderLook(lkpTipoDoc, lstTD, "tdocc_vabreviatura_tipo_doc", "tdocc_icod_tipo_doc", true);
            #endregion
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                setFecha(dteFecha);
                getNroDoc();
                txtIgv.Text = Parametros.strPorcIGV;
                getTipoCambio();
            }
        }
        private void getNroDoc()
        {
            try
            {
                //var lst = new BAdministracionSistema().getCorrelativoTipoDoc(Parametros.intTipoDocNotaCreditoCliente,txtSerie.Text);
                //if (Convert.ToInt32(lst[0].tdocc_nro_serie) != 0)
                //{
                //    txtSerie.Text = lst[0].tdocc_nro_serie;
                //    txtNumero.Text = (Convert.ToInt32(lst[0].tdocc_nro_correlativo) + 1).ToString();
                //}


                //var lst = new BVentas().getCorrelativoPVT(2);
                //txtSerie.Text = lst[0].puvec_vserie_notaF_credito;
                //txtNumero.Text = (Convert.ToInt32(lst[0].puvec_icorrelativo_nota_credito) + 1).ToString();

                //if (txtSerie.Text.Length == 4)
                //{
                //    var lst = new BVentas().listarSeries().Where(x => x.rs_vserie == txtSerie.Text).ToList();
                //    if (Convert.ToInt32(lst.Count()) > 0)
                //    {
                //        txtNumero.Text = (Convert.ToInt32(lst[0].rs_icorrelativo) + 1).ToString();
                //    }
                //    else
                //    {
                //        throw new ArgumentException("Numero de Serie Incorrecto");
                //    }
                //}

                var lst = new BVentas().listarSeries().Where(x => x.rs_icod_pvt == 2 && x.rs_itipo_documento == TDComercial).ToList();
                if (Convert.ToInt32(lst.Count()) > 0)
                {
                    txtSerie.Text = lst[0].rs_vserie;
                    txtNumero.Text = (Convert.ToInt32(lst[0].rs_icorrelativo) + 1).ToString();
                }
                else
                {
                    throw new ArgumentException("Numero de Serie Incorrecto");
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
            using (frmManteNotaCredSuministrosDetalle frm = new frmManteNotaCredSuministrosDetalle())
            {
                frm.SetInsert();
                frm.lstDetalle = lstDetalle;
                //frm.indicador = 1;
                frm.txtMoneda.Text = lkpMoneda.Text;
                frm.txtItem.Text = (lstDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstDetalle.Count + 1);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    grdNC.DataSource = lstDetalle;
                    viewNC.RefreshData();
                    grdNC.Refresh();
                    viewNC.MoveLast();

                    setTotales();
                }
            }
        }

        private void nuevoServicio()
        {
            //using (frmManteNotaCredServicioDetalle frm = new frmManteNotaCredServicioDetalle())
            //{
            //    frm.SetInsert();
            //    frm.lstDetalle = lstDetalle;
            //    frm.lkpMoneda.EditValue = lkpMoneda.EditValue;
            //    frm.txtItem.Text = (lstDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstDetalle.Count + 1);
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        lstDetalle = frm.lstDetalle;
            //        grdNC.DataSource = lstDetalle;
            //        viewNC.RefreshData();
            //        viewNC.MoveLast();
            //        setTotales();
            //    }
            //}
        }

        private void modificarServicio()
        {
            //ENotaCreditoDet oBeDet = (ENotaCreditoDet)viewNC.GetRow(viewNC.FocusedRowHandle);
            //if (oBeDet == null)
            //    return;
            //using (frmManteNotaCredServicioDetalle frm = new frmManteNotaCredServicioDetalle())
            //{
            //    frm.oBe = oBeDet;
            //    frm.lstDetalle = lstDetalle;
            //    frm.SetModify();
            //    frm.setValues();
            //    frm.lkpMoneda.EditValue = lkpMoneda.EditValue;
            //    frm.txtItem.Text = String.Format("{0:000}", oBeDet.dcrec_inro_item);
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        lstDetalle = frm.lstDetalle;
            //        grdNC.DataSource = lstDetalle;
            //        viewNC.RefreshData();
            //        viewNC.MoveLast();
            //        setTotales();
            //    }
            //}
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (txtSerie.Text == "")
                {
                    oBase = txtSerie;
                    throw new ArgumentException("Ingrese Nro. de Serie de la N/C");
                }

                if (Convert.ToInt32(txtNumero.Text) == 0)
                {
                    oBase = txtNumero;
                    throw new ArgumentException("Ingrese Nro. de la N/C");
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                    if (lstCabeceras.Where(x => x.ncrec_vnumero_credito == String.Format("{0}{1}", txtSerie.Text, txtNumero.Text)).ToList().Count > 0)
                    {
                        oBase = txtNumero;
                        throw new ArgumentException("El Nro. de N/C, ya existe en los registros!");
                    }

                if (Convert.ToDateTime(dteFecha.Text).Year != Parametros.intEjercicio)
                {
                    oBase = dteFecha;
                    throw new ArgumentException("La fecha seleccionada esta fuera del rango del ejercicio");
                }

                if (Convert.ToInt32(bteCliente.Tag) == 0)
                {
                    oBase = bteCliente;
                    throw new ArgumentException("Seleccione el cliente");
                }

                if (lstDetalle.Count == 0)
                {
                    throw new ArgumentException("No ha ingresado ningun ítem en el detalle de la N/C!!!");
                }

                oBe.ncrec_vnumero_credito = String.Format("{0}{1}", txtSerie.Text, txtNumero.Text);
                oBe.ncrec_sfecha_credito = Convert.ToDateTime(dteFecha.EditValue);
                oBe.ncrec_ianio = Parametros.intEjercicio;
                oBe.cliec_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                oBe.tdocc_icod_tipo_doc = Convert.ToInt32(lkpTipoDoc.EditValue);
                oBe.tdodc_iid_correlativo = 54;//nota de credito devolucion mercaderia
                oBe.ncrec_vnumero_documento = bteNroDoc.Text;
                oBe.ncrec_sfecha_documento = Convert.ToDateTime(dteFechaDoc.EditValue);
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                oBe.ncrec_nmonto_neto = Convert.ToDecimal(txtMontoNeto.Text);
                oBe.ncrec_npor_imp_igv = Convert.ToDecimal(txtIgv.Text);
                oBe.ncrec_nmonto_total = Convert.ToDecimal(txtMontoTotal.Text);
                oBe.ncrec_iid_situacion_credito = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.ncrec_bincluye_igv = cbIncluyeIGV.Checked;
                oBe.ncrec_tipo_nota_credito = 1;//NOTA DE CREDITO COMERCIAL
                oBe.ncrec_vmotivo_sunat = lkpMotivoSUNAT.EditValue.ToString();
                oBe.ncrec_vdetalle = txtDetalle.Text;

                #region Facturacion Electronica
                oBe.idDocumento = oBe.ncrec_vnumero_credito.Remove(4, 8) + '-' + oBe.ncrec_vnumero_credito.Remove(0, 4);
                oBe.fechaEmision = oBe.ncrec_sfecha_credito.ToString();
                oBe.fechaVencimiento = oBe.ncrec_sfecha_credito.ToString();
                oBe.tipoDocumento = "07";
                if (Convert.ToInt32(lkpMoneda.EditValue) == 3)
                {
                    oBe.moneda = "PEN";
                }
                else
                {
                    oBe.moneda = "USD";
                }

                oBe.CodMotivoNota = lkpMotivoSUNAT.EditValue.ToString();
                oBe.DescripMotivoNota = lkpMotivoSUNAT.Text;
                oBe.NroDocqModifica = bteNroDoc.Text;

                if (Convert.ToInt32(lkpTipoDoc.EditValue) == 26)
                {
                    oBe.TipoDocqModifica = "01";
                }
                else
                {
                    oBe.TipoDocqModifica = "03";
                }
                oBe.cantidadItems = lstDetalle.Count;
                oBe.nombreComercialEmisor = Valores.strNombreEmpresa;
                oBe.nombreLegalEmisor = Valores.strNombreEmpresa;
                oBe.tipoDocumentoEmisor = "6";
                oBe.nroDocumentoEmisior = Valores.strRUC;
                oBe.CodLocalEmisor = "0000";
                oBe.nroDocumentoReceptor = txtRuc.Text;
                oBe.tipoDocumentoReceptor = "6";
                oBe.nombreLegalReceptor = bteCliente.Text;
                oBe.CodMotivoDescuento = 0;
                oBe.PorcDescuento = 0;
                oBe.MontoDescuentoGlobal = 0;
                oBe.BaseMontoDescuento = 0;
                oBe.MontoTotalImpuesto = (Convert.ToDecimal(txtMontoTotal.Text) - Convert.ToDecimal(txtMontoNeto.Text));
                oBe.MontoGravadasIGV = Convert.ToDecimal(txtMontoNeto.Text);
                oBe.CodigoTributo = 1000;
                oBe.MontoExonerado = 0;
                oBe.MontoInafecto = 0;
                oBe.MontoGratuitoImpuesto = 0;
                oBe.MontoBaseGratuito = 0;
                oBe.totalIgv = (Convert.ToDecimal(txtMontoTotal.Text) - Convert.ToDecimal(txtMontoNeto.Text));
                oBe.MontoGravadosISC = 0;
                oBe.totalIsc = 0;
                oBe.MontoGravadosOtros = 0;
                oBe.totalOtrosTributos = 0;
                oBe.TotalValorVenta = Convert.ToDecimal(txtMontoNeto.Text);
                oBe.TotalPrecioVenta = Convert.ToDecimal(txtMontoTotal.Text);
                oBe.MontoDescuento = 0;
                oBe.MontoTotalCargo = 0;
                oBe.MontoTotalAnticipo = 0;
                oBe.ImporteTotalVenta = Convert.ToDecimal(txtMontoTotal.Text);
                oBe.EstadoFacturacion = 4;
                oBe.direccionReceptor = DireccionCliente;
                if (lstPuntoVenta.Count > 0)
                {
                    oBe.CodigoSunat = lstPuntoVenta[0].puvec_vcodigo_sunat;
                    oBe.UsuarioOSE = lstPuntoVenta[0].puvec_vusuario_ose;
                }
                #endregion

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.ncrec_icod_credito = new BVentas().insertarNotaCreditoSuministrosClienteCab(oBe, lstDetalle);
                }
                else
                {
                    new BVentas().modificarNotaCreditoSuministrosClienteCab(oBe, lstDetalle, lstDelete);
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
                    MiEvento(oBe.ncrec_icod_credito);
                    Close();
                }
            }
        }


        private void bteCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCliente();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void setTotales()
        {
            if (lstDetalle.Count > 0)
            {
                txtMontoTotal.Text = lstDetalle.Sum(x => x.dcrec_nmonto_item).ToString();
                txtMontoNeto.Text = Math.Round((Convert.ToDecimal(txtMontoTotal.Text) / Convert.ToDecimal(String.Format("1.{0}", Parametros.strPorcIGV.Replace(".", "")))), 2).ToString();

            }
            else
            {
                txtMontoNeto.Text = "0.00";
                txtMontoTotal.Text = "0.00";
            }

            if (lstDetalle.Count > 0)
            {
                if (cbIncluyeIGV.Checked)
                {
                    decimal total = lstDetalle.Sum(x => x.dcrec_nmonto_item);
                    decimal igv = Convert.ToDecimal(String.Format("1.{0}", Parametros.strPorcIGV.Replace(".", ""), 2));
                    decimal neto = Math.Round(total / Convert.ToDecimal("1." + txtIgv.Text.Replace(".", "")), 2);
                    txtMontoNeto.Text = Convertir.RedondearNumero(neto).ToString();
                    txtMontoTotal.Text = Convertir.RedondearNumero(total).ToString();
                    //txtMontoIGV.Text = (Convert.ToDecimal(txtMontoTotal.Text) - Convert.ToDecimal(txtMontoNeto.Text)).ToString();
                }
                else
                {
                    decimal neto = lstDetalle.Sum(x => x.dcrec_nmonto_item);
                    decimal igv = Convert.ToDecimal(String.Format("1.{0}", Parametros.strPorcIGV.Replace(".", ""), 2));
                    decimal total = Math.Round(neto * Convert.ToDecimal("1." + txtIgv.Text.Replace(".", "")), 2);
                    txtMontoNeto.Text = Convertir.RedondearNumero(neto).ToString();
                    txtMontoTotal.Text = Convertir.RedondearNumero(total).ToString();
                    //txtMontoIGV.Text = (Convert.ToDecimal(txtMontoTotal.Text) - Convert.ToDecimal(txtMontoNeto.Text)).ToString();
                }
            }

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ENotaCreditoSuministrosDet obe = (ENotaCreditoSuministrosDet)viewNC.GetRow(viewNC.FocusedRowHandle);
            if (obe == null)
                return;
            if (obe.intClasificacion != Parametros.intTipoPrdServicio)
                modificarItem();
            else
                modificarServicio();
        }

        private void modificarItem()
        {
            ENotaCreditoSuministrosDet obe = (ENotaCreditoSuministrosDet)viewNC.GetRow(viewNC.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteNotaCredSuministrosDetalle frm = new frmManteNotaCredSuministrosDetalle())
            {
                frm.oBe = obe;
                frm.intClasificacion = obe.intClasificacion;
                frm.lstDetalle = lstDetalle;
                frm.SetModify();
                //frm.cargarcantidadesOriginales();
                frm.txtMoneda.Text = lkpMoneda.Text;
                //frm.indicador = 0;
                frm.txtItem.Text = String.Format("{0:000}", obe.dcrec_inro_item);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    grdNC.DataSource = lstDetalle;
                    viewNC.RefreshData();
                    viewNC.MoveLast();
                    setTotales();
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ENotaCreditoSuministrosDet obe = (ENotaCreditoSuministrosDet)viewNC.GetRow(viewNC.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstDetalle.Remove(obe);
            renumerar();
            grdNC.DataSource = lstDetalle;

            viewNC.RefreshData();
            setTotales();
        }

        private void renumerar()
        {
            Int16 intCont = 0;
            lstDetalle.ForEach(x =>
            {
                intCont += 1;
                x.dcrec_inro_item = intCont;
            });
        }

        private void nuevoServicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevoServicio();
        }

        private void bteNroDoc_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarDocumentoReferencia();
        }

        private void bteCliente_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCliente();
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
                        txtRuc.Text = frm._Be.cliec_cruc;
                        /**/
                        oBe.cliec_icod_cliente = frm._Be.cliec_icod_cliente;
                        DireccionCliente = frm._Be.cliec_vdireccion_cliente;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listarDocumentoReferencia()
        {
            try
            {
                if (Convert.ToInt32(bteCliente.Tag) == 0)
                    throw new ArgumentException("Seleccione el cliente");

                using (FrmListarDocXCobrar frm = new FrmListarDocXCobrar())
                {
                    frm.intCliente = Convert.ToInt32(bteCliente.Tag);
                    frm.icod_tipo_doc = Convert.ToInt32(lkpTipoDoc.EditValue);
                    frm.flagBovFav = true;
                    frm.icod_tipo_documento_MP_MER = 2;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lkpTipoDoc.EditValue = frm._Be.tdocc_icod_tipo_doc;
                        bteNroDoc.Text = frm._Be.doxcc_vnumero_doc;
                        bteNroDoc.Tag = frm._Be.doxcc_icod_correlativo;
                        dteFechaDoc.EditValue = frm._Be.doxcc_sfecha_doc;
                        bteCliente.Tag = frm._Be.cliec_icod_cliente;
                        bteCliente.Text = frm._Be.cliec_vnombre_cliente;
                        txtRuc.Text = frm._Be.NumDocCliente;
                        DireccionCliente = frm._Be.DireccionCliente;
                        lkpMoneda.EditValue = frm._Be.tablc_iid_tipo_moneda;

                        oBe.tdodc_iid_correlativo = frm._Be.tdodc_iid_correlativo;
                        icod_dxc = Convert.ToInt32(frm._Be.doxcc_icod_correlativo);
                        ListarDet();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ListarDet()
        {
            //if (lstDetalle.Count > 0)
            //{
            //    lstDetalle.Clear();
            //}
            //if (lkpTipoDoc.Text == "FAV")
            //{
            //    List<EFacturaCab> lstFV = new List<EFacturaCab>();
            //    lstFV = new BVentas().listarFacturaCab(Parametros.intEjercicio).Where(x => x.doxcc_icod_correlativo == icod_dxc).ToList();

            //    List<EFacturaDet> lstFVDet = new List<EFacturaDet>();
            //    lstFVDet = new BVentas().listarFacturaDetalle(lstFV[0].favc_icod_factura);
            //    foreach (var item in lstFVDet)
            //    {
            //        ENotaCreditoDet BENC = new ENotaCreditoDet();
            //        BENC.dcrec_inro_item = Convert.ToInt16(item.favd_iitem_factura);
            //        BENC.prdc_icod_producto = item.prdc_icod_producto;
            //        BENC.dcrec_vcodigo_extremo_prod = item.favd_vcodigo_extremo_prod;
            //        BENC.strLinea = item.strCategoria;
            //        BENC.strSubLinea = item.strSubCategoriaUno;
            //        BENC.dcrec_ncantidad_producto = Convert.ToDecimal(item.favd_ncantidad_total_producto);
            //        BENC.strDesUM = item.strDesUM;
            //        BENC.dcrec_vdescripcion = item.favd_vdescripcion_prod;
            //        BENC.strMoneda = item.strMoneda;
            //        BENC.dcrec_nmonto_unitario = item.favd_nprecio_unitario_item;
            //        BENC.dcrec_nmonto_item = Convert.ToDecimal(item.favd_nprecio_total_item);
            //        //BENC.dcrec_nmonto_descuento_item = Convert.ToDecimal(item.favd_nporcentaje_descuento_item);
            //        BENC.dcrec_nmonto_impuesto = item.favd_nmonto_impuesto_item;
            //        BENC.almac_icod_almacen = item.almac_icod_almacen;
            //        BENC.strAlmacen = item.strAlmacen;

            //        BENC.unidc_icod_unidad_medida = item.unidc_icod_unidad_medida;

            //        BENC.dcrec_icod_serie = item.favd_icod_serie;
            //        BENC.dcrec_rango_talla = item.favd_rango_talla;
            //        BENC.Serie = item.resec_vdescripcion;


            //        BENC.dcrec_talla1 = item.favd_talla1;
            //        BENC.dcrec_talla2 = item.favd_talla2;
            //        BENC.dcrec_talla3 = item.favd_talla3;
            //        BENC.dcrec_talla4 = item.favd_talla4;
            //        BENC.dcrec_talla5 = item.favd_talla5;
            //        BENC.dcrec_talla6 = item.favd_talla6;
            //        BENC.dcrec_talla7 = item.favd_talla7;
            //        BENC.dcrec_talla8 = item.favd_talla8;
            //        BENC.dcrec_talla9 = item.favd_talla9;
            //        BENC.dcrec_talla10 = item.favd_talla10;
            //        BENC.dcrec_cant_talla1 = item.favd_cant_talla1;
            //        BENC.dcrec_cant_talla2 = item.favd_cant_talla2;
            //        BENC.dcrec_cant_talla3 = item.favd_cant_talla3;
            //        BENC.dcrec_cant_talla4 = item.favd_cant_talla4;
            //        BENC.dcrec_cant_talla5 = item.favd_cant_talla5;
            //        BENC.dcrec_cant_talla6 = item.favd_cant_talla6;
            //        BENC.dcrec_cant_talla7 = item.favd_cant_talla7;
            //        BENC.dcrec_cant_talla8 = item.favd_cant_talla8;
            //        BENC.dcrec_cant_talla9 = item.favd_cant_talla9;
            //        BENC.dcrec_cant_talla10 = item.favd_cant_talla10;
            //        BENC.dcrec_icod_producto1 = item.favd_icod_producto1;
            //        BENC.dcrec_icod_producto2 = item.favd_icod_producto2;
            //        BENC.dcrec_icod_producto3 = item.favd_icod_producto3;
            //        BENC.dcrec_icod_producto4 = item.favd_icod_producto4;
            //        BENC.dcrec_icod_producto5 = item.favd_icod_producto5;
            //        BENC.dcrec_icod_producto6 = item.favd_icod_producto6;
            //        BENC.dcrec_icod_producto7 = item.favd_icod_producto7;
            //        BENC.dcrec_icod_producto8 = item.favd_icod_producto8;
            //        BENC.dcrec_icod_producto9 = item.favd_icod_producto9;
            //        BENC.dcrec_icod_producto10 = item.favd_icod_producto10;

            //        BENC.dcrec_iid_kardex1 = item.favd_iid_kardex1;
            //        BENC.dcrec_iid_kardex2 = item.favd_iid_kardex2;
            //        BENC.dcrec_iid_kardex3 = item.favd_iid_kardex3;
            //        BENC.dcrec_iid_kardex4 = item.favd_iid_kardex4;
            //        BENC.dcrec_iid_kardex5 = item.favd_iid_kardex5;
            //        BENC.dcrec_iid_kardex6 = item.favd_iid_kardex6;
            //        BENC.dcrec_iid_kardex7 = item.favd_iid_kardex7;
            //        BENC.dcrec_iid_kardex8 = item.favd_iid_kardex8;
            //        BENC.dcrec_iid_kardex9 = item.favd_iid_kardex9;
            //        BENC.dcrec_iid_kardex10 = item.favd_iid_kardex10;
            //        BENC.unidc_icod_unidad_medida = item.unidc_icod_unidad_medida;




            //        #region Factura Electronica Detalle
            //        BENC.NumeroOrdenItem = item.favd_iitem_factura;
            //        BENC.cantidad = Convert.ToDecimal(item.favd_ncantidad_total_producto);
            //        BENC.unidadMedida = item.CodigoSUNAT;
            //        BENC.ValorVentaItem = Convert.ToDecimal(item.favd_nprecio_total_item);
            //        BENC.CodMotivoDescuentoItem = 0;
            //        BENC.FactorDescuentoItem = 0;
            //        BENC.DescuentoItem = 0;
            //        BENC.BaseDescuentotem = 0;
            //        BENC.CodMotivoCargoItem = 0;
            //        BENC.FactorCargoItem = 0;
            //        BENC.MontoCargoItem = 0;
            //        BENC.BaseCargoItem = 0;
            //        BENC.MontoTotalImpuestosItem = item.favd_nmonto_impuesto_item;
            //        BENC.MontoImpuestoIgvItem = item.favd_nmonto_impuesto_item;
            //        if (item.favd_nmonto_impuesto_item == 0)
            //        {
            //            //BENC.MontoInafectoItem = item.favd_nprecio_total_item;
            //            BENC.MontoAfectoImpuestoIgv = 0;
            //        }
            //        else
            //        {

            //            //BENC.MontoInafectoItem = 0;
            //            BENC.MontoAfectoImpuestoIgv = (item.favd_nprecio_total_item - item.favd_nmonto_impuesto_item);
            //        }
            //        BENC.PorcentajeIGVItem = Convert.ToDecimal(Parametros.strPorcIGV);
            //        BENC.MontoImpuestoISCItem = 0;
            //        BENC.MontoAfectoImpuestoIsc = 0;
            //        BENC.PorcentajeISCtem = 0;
            //        BENC.MontoImpuestoIVAPtem = 0;
            //        BENC.MontoAfectoImpuestoIVAPItem = 0;
            //        BENC.PorcentajeIVAPItem = 0;
            //        BENC.descripcion = item.favd_vdescripcion_prod;
            //        BENC.codigoItem = item.favd_vcodigo_extremo_prod;
            //        BENC.ObservacionesItem = "";
            //        BENC.ValorUnitarioItem = (item.favd_nprecio_unitario_item - item.favd_nmonto_impuesto_item);
            //        BENC.UMedida = item.UMedida;
            //        //if (item.favd_nmonto_impuesto_item == 0)
            //        //{
            //        //    BENC.tipoImpuesto = "30";
            //        //}
            //        //else
            //        //{
            //        //    BENC.tipoImpuesto = "10";
            //        //}

            //        #endregion



            //        lstDetalle.Add(BENC);
            //        grdNC.DataSource = lstDetalle;
            //        viewNC.RefreshData();
            //        setTotales();
            //        IndicadorRef = 1;
            //    }
            //}
            //if (lkpTipoDoc.Text == "BOV")
            //{
            //    List<EBoletaCab> lstBV = new List<EBoletaCab>();
            //    lstBV = new BVentas().listarBoletaCab(Parametros.intEjercicio).Where(x => x.doxcc_icod_correlativo == icod_dxc).ToList();
            //    List<EBoletaDet> lstBVDet = new List<EBoletaDet>();
            //    lstBVDet = new BVentas().listarBoletaDetalle(lstBV[0].bovc_icod_boleta);
            //    foreach (var item in lstBVDet)
            //    {
            //        ENotaCreditoDet BENC = new ENotaCreditoDet();
            //        BENC.dcrec_inro_item = Convert.ToInt16(item.bovd_iitem_boleta);
            //        BENC.prdc_icod_producto = item.prdc_icod_producto;
            //        BENC.strCodProducto = item.strCodProducto;
            //        BENC.strLinea = item.strCategoria;
            //        BENC.strSubLinea = item.strSubCategoriaUno;
            //        BENC.dcrec_ncantidad_producto = item.bovd_ncantidad;
            //        BENC.strDesUM = item.strDesUM;
            //        BENC.strDesProducto = item.strDesProducto;
            //        BENC.strMoneda = item.strMoneda;
            //        BENC.dcrec_nmonto_unitario = item.bovd_nprecio_unitario_item;
            //        BENC.dcrec_nmonto_item = Convert.ToDecimal(item.bovd_nprecio_total_item);
            //        //BENC.dcrec_nprecio_lista_item = Convert.ToDecimal(item.bovd_nprecio_lista_item);
            //        //BENC.dcrec_nmonto_descuento_item = Convert.ToDecimal(item.bovd_nporcentaje_descuento_item);
            //        BENC.dcrec_nmonto_impuesto = item.bovd_nmonto_impuesto_item;
            //        BENC.almac_icod_almacen = item.almac_icod_almacen;
            //        BENC.strAlmacen = item.strAlmacen;

            //        //BENC.unidc_icod_unidad_medida = item.unidc_icod_unidad_medida


            //        BENC.dcrec_icod_serie = item.bovd_icod_serie;
            //        BENC.dcrec_rango_talla = item.bovd_rango_talla;
            //        BENC.Serie = item.resec_vdescripcion;


            //        BENC.dcrec_talla1 = item.bovd_talla1;
            //        BENC.dcrec_talla2 = item.bovd_talla2;
            //        BENC.dcrec_talla3 = item.bovd_talla3;
            //        BENC.dcrec_talla4 = item.bovd_talla4;
            //        BENC.dcrec_talla5 = item.bovd_talla5;
            //        BENC.dcrec_talla6 = item.bovd_talla6;
            //        BENC.dcrec_talla7 = item.bovd_talla7;
            //        BENC.dcrec_talla8 = item.bovd_talla8;
            //        BENC.dcrec_talla9 = item.bovd_talla9;
            //        BENC.dcrec_talla10 = item.bovd_talla10;
            //        BENC.dcrec_cant_talla1 = item.bovd_cant_talla1;
            //        BENC.dcrec_cant_talla2 = item.bovd_cant_talla2;
            //        BENC.dcrec_cant_talla3 = item.bovd_cant_talla3;
            //        BENC.dcrec_cant_talla4 = item.bovd_cant_talla4;
            //        BENC.dcrec_cant_talla5 = item.bovd_cant_talla5;
            //        BENC.dcrec_cant_talla6 = item.bovd_cant_talla6;
            //        BENC.dcrec_cant_talla7 = item.bovd_cant_talla7;
            //        BENC.dcrec_cant_talla8 = item.bovd_cant_talla8;
            //        BENC.dcrec_cant_talla9 = item.bovd_cant_talla9;
            //        BENC.dcrec_cant_talla10 = item.bovd_cant_talla10;
            //        BENC.dcrec_icod_producto1 = item.bovd_icod_producto1;
            //        BENC.dcrec_icod_producto2 = item.bovd_icod_producto2;
            //        BENC.dcrec_icod_producto3 = item.bovd_icod_producto3;
            //        BENC.dcrec_icod_producto4 = item.bovd_icod_producto4;
            //        BENC.dcrec_icod_producto5 = item.bovd_icod_producto5;
            //        BENC.dcrec_icod_producto6 = item.bovd_icod_producto6;
            //        BENC.dcrec_icod_producto7 = item.bovd_icod_producto7;
            //        BENC.dcrec_icod_producto8 = item.bovd_icod_producto8;
            //        BENC.dcrec_icod_producto9 = item.bovd_icod_producto9;
            //        BENC.dcrec_icod_producto10 = item.bovd_icod_producto10;

            //        BENC.dcrec_iid_kardex1 = item.bovd_iid_kardex1;
            //        BENC.dcrec_iid_kardex2 = item.bovd_iid_kardex2;
            //        BENC.dcrec_iid_kardex3 = item.bovd_iid_kardex3;
            //        BENC.dcrec_iid_kardex4 = item.bovd_iid_kardex4;
            //        BENC.dcrec_iid_kardex5 = item.bovd_iid_kardex5;
            //        BENC.dcrec_iid_kardex6 = item.bovd_iid_kardex6;
            //        BENC.dcrec_iid_kardex7 = item.bovd_iid_kardex7;
            //        BENC.dcrec_iid_kardex8 = item.bovd_iid_kardex8;
            //        BENC.dcrec_iid_kardex9 = item.bovd_iid_kardex9;
            //        BENC.dcrec_iid_kardex10 = item.bovd_iid_kardex10;





            //        #region Factura Electronica Detalle
            //        BENC.NumeroOrdenItem = item.bovd_iitem_boleta;
            //        BENC.cantidad = Convert.ToDecimal(item.bovd_ncantidad);
            //        BENC.unidadMedida = "PR";
            //        BENC.ValorVentaItem = Convert.ToDecimal(item.bovd_nprecio_total_item);
            //        BENC.CodMotivoDescuentoItem = 0;
            //        BENC.FactorDescuentoItem = 0;
            //        BENC.DescuentoItem = 0;
            //        BENC.BaseDescuentotem = 0;
            //        BENC.CodMotivoCargoItem = 0;
            //        BENC.FactorCargoItem = 0;
            //        BENC.MontoCargoItem = 0;
            //        BENC.BaseCargoItem = 0;
            //        BENC.MontoTotalImpuestosItem = item.bovd_nmonto_impuesto_item;
            //        BENC.MontoImpuestoIgvItem = item.bovd_nmonto_impuesto_item;
            //        if (item.bovd_nmonto_impuesto_item == 0)
            //        {
            //            //BENC.MontoInafectoItem = item.bovd_nprecio_total_item;
            //            BENC.MontoAfectoImpuestoIgv = 0;
            //        }
            //        else
            //        {

            //            //BENC.MontoInafectoItem = 0;
            //            BENC.MontoAfectoImpuestoIgv = (item.bovd_nprecio_total_item - item.bovd_nmonto_impuesto_item);
            //        }
            //        BENC.PorcentajeIGVItem = Convert.ToDecimal(Parametros.strPorcIGV);
            //        BENC.MontoImpuestoISCItem = 0;
            //        BENC.MontoAfectoImpuestoIsc = 0;
            //        BENC.PorcentajeISCtem = 0;
            //        BENC.MontoImpuestoIVAPtem = 0;
            //        BENC.MontoAfectoImpuestoIVAPItem = 0;
            //        BENC.PorcentajeIVAPItem = 0;
            //        BENC.descripcion = item.strDesProducto;
            //        BENC.codigoItem = item.strCodProducto;
            //        BENC.ObservacionesItem = "";
            //        BENC.ValorUnitarioItem = (item.bovd_nprecio_unitario_item - item.bovd_nmonto_impuesto_item);
            //        BENC.UMedida = item.UMedida;
            //        //if (item.bovd_nmonto_impuesto_item == 0)
            //        //{
            //        //    BENC.tipoImpuesto = "30";
            //        //}
            //        //else
            //        //{
            //        //    BENC.tipoImpuesto = "10";
            //        //}
            //        #endregion

            //        lstDetalle.Add(BENC);
            //        grdNC.DataSource = lstDetalle;
            //        viewNC.RefreshData();
            //        setTotales();
            //        IndicadorRef = 1;
            //    }
            //}
        }



        private void cbIncluyeIGV_CheckedChanged(object sender, EventArgs e)
        {
            setTotales();
        }

        private void txtSerie_EditValueChanged(object sender, EventArgs e)
        {
            getNroDoc();
        }

        private void dteFecha_EditValueChanged(object sender, EventArgs e)
        {
            getTipoCambio();
        }

        private void dteFechaDoc_EditValueChanged(object sender, EventArgs e)
        {
            getTipoCambioDoc();
        }
        public void getTipoCambio()
        {
            var Lista = lstTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dteFecha.EditValue).ToShortDateString()).ToList();
            Lista.ForEach(obe =>
            {
                txtTipoCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
            });
        }
        public void getTipoCambioDoc()
        {
            var Lista = lstTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dteFechaDoc.EditValue).ToShortDateString()).ToList();
            Lista.ForEach(obe =>
            {
                txtTipoCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
            });
        }

        private void bteNroDoc_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lkpTipoDoc_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void bteNroDoc_Leave(object sender, EventArgs e)
        {
            if (bteNroDoc.Text.Length > 13)
            {
                throw new ArgumentException("Numero de Referencia no debe ser menor a 12 digitos");
            }
        }
    }
}