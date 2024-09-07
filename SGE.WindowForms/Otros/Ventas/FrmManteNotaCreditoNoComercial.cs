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
    public partial class FrmManteNotaCreditoNoComercial : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        /**/
        public ENotaCredito oBe = new ENotaCredito();
        List<ENotaCreditoDet> lstDetalle = new List<ENotaCreditoDet>();
        List<ENotaCreditoDet> lstDelete = new List<ENotaCreditoDet>();
        /**/
        public List<ENotaCredito> lstCabeceras = new List<ENotaCredito>();//este listado se utiliza para comparar si ya existe el nro. de nc que se esta registrando
        public string DireccionCliente;
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
            txtNumero.Text = oBe.ncrec_vnumero_credito.Substring(4);
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
            lkpClaseDoc.EditValue = oBe.ncrec_iclase_doc;
            lkpMotivoSUNAT.EditValue = oBe.ncrec_vmotivo_sunat;
            txtDetalle.Text = oBe.ncrec_vdetalle;
            DireccionCliente = oBe.DirecionCliente;
            #endregion
            lstDetalle = new BVentas().listarNotaCreditoNoComercialClienteDet(oBe.ncrec_icod_credito);
            grdNC.DataSource = lstDetalle;
            foreach (var _ve in lstDetalle)
            {
                string[] partes = _ve.dcrec_vdescripcion.Split('@');
                for (int i = 0; i < partes.Length; i++)
                {
                    if (partes[i] != "")
                    {
                        _ve.strDesProductoPresentar = _ve.strDesProductoPresentar + " " + partes[i];
                    }
                }
            }
            grdNC.DataSource = lstDetalle;
            viewNC.RefreshData();
        }

        public FrmManteNotaCreditoNoComercial()
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
            BSControls.LoaderLook(lkpClaseDoc, new BGeneral().listarTablaRegistro(62), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaSituacionDocumento), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMotivoSUNAT, new BVentas().TablasSunatDetListar(1), "suntd_vdescripcion", "suntd_codigo", true);
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
            using (frmManteNotaCredDetalle frm = new frmManteNotaCredDetalle())
            {
                frm.SetInsert();
                frm.lstDetalle = lstDetalle;
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
            using (frmManteNotaCredNoComerDetalle frm = new frmManteNotaCredNoComerDetalle())
            {
                frm.SetInsert();
                frm.lstDetalle = lstDetalle;
                frm.porcentaje_igv = txtIgv.Text;
                frm.flag_afecto_igv = cbIncluyeIGV.Checked;
                frm.txtItem.Text = (lstDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstDetalle.Count + 1);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    grdNC.DataSource = lstDetalle;
                    foreach (var _ve in lstDetalle)
                    {
                        string[] partes = _ve.dcrec_vdescripcion.Split('@');
                        for (int i = 0; i < partes.Length; i++)
                        {
                            if (partes[i] != "")
                            {
                                _ve.strDesProductoPresentar = _ve.strDesProductoPresentar + " " + partes[i];
                            }
                        }
                    }
                    viewNC.RefreshData();
                    viewNC.MoveLast();
                    setTotales();
                }
            }
        }

        private void modificarServicio()
        {
            ENotaCreditoDet oBeDet = (ENotaCreditoDet)viewNC.GetRow(viewNC.FocusedRowHandle);
            if (oBeDet == null)
                return;
            using (frmManteNotaCredNoComerDetalle frm = new frmManteNotaCredNoComerDetalle())
            {
                frm.porcentaje_igv = txtIgv.Text;
                frm.flag_afecto_igv = cbIncluyeIGV.Checked;
                frm.oBe = oBeDet;
                frm.lstDetalle = lstDetalle;
                frm.SetModify();
                frm.setValues();

                frm.txtItem.Text = String.Format("{0:000}", oBeDet.dcrec_inro_item);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    grdNC.DataSource = lstDetalle;
                    foreach (var _ve in lstDetalle)
                    {
                        string[] partes = _ve.dcrec_vdescripcion.Split('@');
                        for (int i = 0; i < partes.Length; i++)
                        {
                            if (partes[i] != "")
                            {
                                _ve.strDesProductoPresentar = _ve.strDesProductoPresentar + " " + partes[i];
                            }
                        }
                    }
                    viewNC.RefreshData();
                    viewNC.MoveLast();
                    setTotales();
                }
            }
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if ((txtSerie.Text) == "")
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
                if (bteNroDoc.Text.Length < 12)
                {
                    oBase = bteNroDoc;
                    throw new ArgumentException("Ingrese Numero Referencia mayor a 12");
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
                oBe.ncrec_tipo_nota_credito = 2;//NOTA DE CREDITO NO COMERCIAL
                oBe.ncrec_iclase_doc = Convert.ToInt32(lkpClaseDoc.EditValue);
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
                    oBe.ncrec_icod_credito = new BVentas().insertarNotaCreditoClienteCab(oBe, lstDetalle);
                }
                else
                {
                    new BVentas().modificarNotaCreditoClienteCab(oBe, lstDetalle, lstDelete);
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
            //if (lstDetalle.Count > 0)
            //{
            //    txtMontoTotal.Text = lstDetalle.Sum(x => x.dcrec_nmonto_item).ToString();
            //    txtMontoNeto.Text = Math.Round((Convert.ToDecimal(txtMontoTotal.Text) / Convert.ToDecimal(String.Format("1.{0}", Parametros.strPorcIGV.Replace(".", "")))), 2).ToString();

            //}
            //else
            //{
            //    txtMontoNeto.Text = "0.00";                
            //    txtMontoTotal.Text = "0.00";
            //}

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
            else
            {
                txtMontoNeto.Text = "0.00";
                txtMontoTotal.Text = "0.00";
            }

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ENotaCreditoDet obe = (ENotaCreditoDet)viewNC.GetRow(viewNC.FocusedRowHandle);
            if (obe == null)
                return;

            modificarServicio();
        }



        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ENotaCreditoDet obe = (ENotaCreditoDet)viewNC.GetRow(viewNC.FocusedRowHandle);
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
                    frm.flagBovFav = true;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lkpTipoDoc.EditValue = frm._Be.tdocc_icod_tipo_doc;
                        bteNroDoc.Text = frm._Be.doxcc_vnumero_doc;
                        bteNroDoc.Tag = frm._Be.doxcc_icod_correlativo;
                        dteFechaDoc.EditValue = frm._Be.doxcc_sfecha_doc;
                        oBe.tdodc_iid_correlativo = frm._Be.tdodc_iid_correlativo;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbIncluyeIGV_CheckedChanged(object sender, EventArgs e)
        {
            setTotales();
        }

        private void txtSerie_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                getNroDoc();
            }

        }

        private void bteNroDoc_Leave(object sender, EventArgs e)
        {

        }
    }
}