using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.WindowForms.Otros.Operaciones;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
using static SGE.Common.Codigos;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteGuiaRemision : XtraForm
    {
        public EGuiaRemision oBe = new EGuiaRemision();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        List<EGuiaRemisionDet> lstDetalle = new List<EGuiaRemisionDet>();
        List<EGuiaRemisionDet> lstDelete = new List<EGuiaRemisionDet>();
        bool ConductorIngresado = false;
        List<EGuiaRemisionMPDet> lstMPDetalle = new List<EGuiaRemisionMPDet>();
        List<EGuiaRemisionMPDet> lstMPDelete = new List<EGuiaRemisionMPDet>();

        string strCodCliente = "";
        public int IcodAlmacen = 0;
        public int TipodeGuia = 0;
        public int TDComercial = 0;

        List<EImportarProducto> listaProducto = new List<EImportarProducto>();
        List<EImportarProducto> listaProductoNoExiste = new List<EImportarProducto>();
        List<EProdProducto> mlist = new List<EProdProducto>();
        EConductor objConductor = new EConductor();

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
            btnCliente.Enabled = !Enabled;
            txtRUC.Enabled = !Enabled;
            lkpMotivo.Enabled = !Enabled;
            txtNombreComercial.Enabled = !Enabled;
            btnAlmacen.Enabled = !Enabled;
            btnAlmacenIngreso.Enabled = !Enabled;
            txtDestinatario.Enabled = !Enabled;
            txtPartida.Enabled = !Enabled;
            txtLlegada.Enabled = !Enabled;
            btnTransportista.Enabled = !Enabled;
            txtLicencia.Enabled = !Enabled;
            txtMatricula.Enabled = !Enabled;
            txtRUCTransportista.Enabled = !Enabled;
            txtFactura.Enabled = !Enabled;
            lkpMotivo.Enabled = !Enabled;
            dteFecha.Enabled = !Enabled;

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtSerie.Enabled = false;
                txtNumero.Enabled = false;
                dteFecha.Enabled = true;
                btnCliente.Enabled = false;
                btnAlmacen.Enabled = false;
                btnAlmacenIngreso.Enabled = false;
                bteProyecto.Enabled = false;
                //btnCentroCostos.Enabled = Enabled;
                lkpMotivo.Enabled = false;
                dteFecha.Enabled = true;
            }
        }

        private void setValues()
        {
            txtSerie.Text = oBe.remic_vnumero_remision.Substring(0, 4);
            txtNumero.Text = oBe.remic_vnumero_remision.Substring(4);
            btnUbigeo.Text = oBe.remic_vubigeo_destino;
            dteFecha.EditValue = oBe.remic_sfecha_remision;
            btnCliente.Tag = oBe.cliec_icod_cliente;
            btnCliente.Text = oBe.NomClie;
            txtDestinatario.Text = oBe.remic_vnombre_destinatario;
            txtRUC.Text = oBe.remic_vruc_destinatario;
            txtPartida.Text = oBe.remic_vdireccion_procedencia;
            txtLlegada.Text = oBe.remic_vdireccion_destinatario;
            btnAlmacen.Tag = oBe.almac_icod_almacen;
            btnAlmacen.Text = oBe.strDesAlmacen;
            lkpMotivo.EditValue = oBe.tablc_iid_motivo;
            btnTransportista.Tag = oBe.tranc_icod_transportista;
            btnTransportista.Text = oBe.strTransportista;
            txtLicenciaConducir.Text = oBe.remic_vlicencia;
            txtRUCTransportista.Text = oBe.remic_vruc_transportista;
            lkpSituacion.EditValue = oBe.tablc_iid_situacion_remision;
            txtFactura.Text = oBe.remic_vreferencia;
            txttipoDoc.Text = oBe.str_Tipo_doc;
            txtdocNumero.Text = oBe.Vnumero_Documento;
            dteFechaTranslado.EditValue = oBe.remic_sfecha_inicio;
            btnCentroCostos.Tag = oBe.cecoc_icod_centro_costo;
            txtVehiMarPla.Text = oBe.remic_vmarca_placa;
            txtCertificado.Text = oBe.remic_vcertif_inscripcion;
            btnAlmacenIngreso.Tag = oBe.almac_icod_almacen_ingreso;
            btnAlmacenIngreso.Text = oBe.strDesAlmaceningreso;
            bteProyecto.Text = oBe.CodProyecto;
            btnCentroCostos.Tag = oBe.cecoc_icod_centro_costo;
            btnCentroCostos.Text = oBe.CentroCosto;
            lkpTipoDocRef.Text = oBe.tdocc_vabreviatura_tipo_doc;
            bteNroDocRef.Text = oBe.remic_num_comprobante;
            TipodeGuia = oBe.remic_itipo_guia;
            txtDesMotivo.Text = oBe.remic_vdescripcion_motivo;
            var selectmotivo = new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMotivoGuiaRemision).Where(x => x.tarec_iid_tabla_registro == oBe.tablc_iid_motivo).FirstOrDefault();


            oBe.guiaRemisionElectronica.CodigoMotivoTraslado = selectmotivo.tarec_vtipo_operacion;

            if (oBe.remic_itipo_guia == 1)
            {

                lstDetalle = new BVentas().listarGuiaRemisionDet(oBe.remic_icod_remision, Parametros.intEjercicio);
                grdGuiaRemision.DataSource = lstDetalle;
                viewGuiaRemision.RefreshData();

                txtTotalCajas.Text = oBe.remic_inum_cajas.ToString();
                txtTotalPares.Text = lstDetalle.Sum(x => Convert.ToInt32(x.dremc_ncantidad_producto)).ToString();
            }
            else if (oBe.remic_itipo_guia == 2)
            {

                lstMPDetalle = new BVentas().listarGuiaRemisionMPDet(oBe.remic_icod_remision, Parametros.intEjercicio);
                grdGuiaRemisionMP.DataSource = lstMPDetalle;
                viewGuiaRemisionMP.RefreshData();
            }


            lkpModalidadTraslado.EditValue = oBe.remic_vruc_transportista == Valores.strRUC ? "02" : "01";

            objConductor = new BVentas().ConductorGet(oBe.remic_icod_remision);
            ConductorIngresado = objConductor.remic_icod_remision > 0;
            objConductor.remic_icod_remision = oBe.remic_icod_remision;
            setCliente(oBe.cliec_icod_cliente);
        }

        public frmManteGuiaRemision()
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
            lst.Add(new TipoDoc { intCodigo = 0, strTipoDoc = ".......Eligir un documento....." });
            lst.Add(new TipoDoc { intCodigo = 26, strTipoDoc = "FAV" });
            lst.Add(new TipoDoc { intCodigo = 9, strTipoDoc = "BOV" });
            BSControls.LoaderLook(lkpTipoDocRef, lst, "strTipoDoc", "intCodigo", true);


            if (oBe.remic_itipo_guia == 1 || TipodeGuia == 1)
            {
                xtraTabControl1.SelectedTabPage = xtraMercaderia;
                xtraSuministros.PageEnabled = false;
            }
            else
            {
                xtraTabControl1.SelectedTabPage = xtraSuministros;
                xtraMercaderia.PageEnabled = false;
            }
        }

        class TipoTranslado
        {
            public string descripcion { get; set; }
            public string valor { get; set; }
        }
        public void CargarControles()
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaSituacionGuiaRemision), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMotivo, new BGeneral().listarTablaRegistro((int)MotivoGuiaRemision.Id), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            List<TipoTranslado> list = new List<TipoTranslado>();
            list.Add(new TipoTranslado() { descripcion = "PUBLICO", valor = "01" });
            list.Add(new TipoTranslado() { descripcion = "PRIVADO", valor = "02" });
            BSControls.LoaderLook(lkpModalidadTraslado, list, "descripcion", "valor", true);
        }
        private void getNroDoc()
        {
            var serie = new BVentas().listarSeries().Where(x => x.rs_icod_registro_serie == (TipodeGuia == 1 ? Constantes.GuiaRemisionMerdaderiaCentral : Constantes.GuiaRemisionSuministroCentral)).FirstOrDefault();
            txtSerie.Text = serie.rs_vserie;
            txtNumero.Text = (serie.rs_icorrelativo + 1).ToString();
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
            if (TipodeGuia == 1 || oBe.remic_itipo_guia == 1)
            {

                if (Convert.ToInt32(btnAlmacen.Tag) == 0)
                {
                    XtraMessageBox.Show("Seleccione el Almacén", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                using (frmManteGuiaRemisionDetalle frm = new frmManteGuiaRemisionDetalle())
                {
                    oBe.almac_icod_almacen = Convert.ToInt32(btnAlmacen.Tag);
                    frm.oBeCab = oBe;
                    //frm.IcodAlmacen =Convert.ToInt32(btnAlmacen.Tag);
                    frm.SetInsert();
                    frm.lstDetalle = lstDetalle;
                    frm.txtItem.Text = (lstDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstDetalle.Max(x => x.dremc_inro_item) + 1);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        grdGuiaRemision.DataSource = lstDetalle;
                        viewGuiaRemision.RefreshData();
                        viewGuiaRemision.MoveLast();
                        TotalizarPares();
                    }
                }
            }
            else
            {
                if (Convert.ToInt32(btnAlmacen.Tag) == 0)
                {
                    XtraMessageBox.Show("Seleccione el Almacén", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                using (frmManteGuiaRemisionMPDetalle frm = new frmManteGuiaRemisionMPDetalle())
                {
                    oBe.almac_icod_almacen = Convert.ToInt32(btnAlmacen.Tag);
                    frm.oBeCab = oBe;

                    frm.SetInsert();
                    frm.lstDetalle = lstMPDetalle;
                    frm.txtItem.Text = (lstMPDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstMPDetalle.Max(x => x.dremc_inro_item) + 1);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstMPDetalle = frm.lstDetalle;
                        grdGuiaRemisionMP.DataSource = lstMPDetalle;
                        viewGuiaRemisionMP.RefreshData();
                        viewGuiaRemisionMP.MoveLast();
                    }
                }
            }

        }
        public void TotalizarPares()
        {
            txtTotalPares.Text = lstDetalle.Where(x => x.dremc_itipo_producto == (int)TipoProducto.Mercadería).Sum(x => Convert.ToInt32(x.dremc_ncantidad_producto)).ToString();

        }
        private void listarCliente()
        {
            try
            {
                using (FrmListarCliente frm = new FrmListarCliente())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {

                        btnCliente.Tag = frm._Be.cliec_icod_cliente;
                        btnCliente.Text = frm._Be.cliec_vnombre_cliente;
                        txtDestinatario.Text = frm._Be.cliec_vnombre_cliente;
                        txtRUC.Text = frm._Be.cliec_cruc;
                        strCodCliente = frm._Be.cliec_vcod_cliente;
                        txtLlegada.Text = frm._Be.cliec_vdireccion_cliente;
                        btnUbigeo.Text = frm._Be.cliec_vubigeo;
                        oBe.cliec_itipo_documento = (int)TipoDocumentoPersonal.Ruc;
                        if (string.IsNullOrEmpty(btnUbigeo.Text))
                        {
                            var result = ConsultaRucApi.ConsultaRuc(frm._Be.cliec_cruc);
                            btnUbigeo.Text = result.ubigeo;
                        }
                        if (string.IsNullOrEmpty(frm._Be.cliec_cruc))
                        {
                            txtRUC.Text = frm._Be.cliec_vnumero_doc_cli;
                            oBe.cliec_itipo_documento = frm._Be.tabl_iid_tipo_documento;
                        }
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
                oBe.cliec_itipo_documento = (int)TipoDocumentoPersonal.Ruc;
                if (string.IsNullOrEmpty(_Be.cliec_cruc))
                {
                    oBe.cliec_itipo_documento = _Be.tabl_iid_tipo_documento;
                }

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
                if (Convert.ToInt32(txtSerie.Text.Length) == 0)
                {
                    oBase = txtSerie;
                    throw new ArgumentException("Ingrese Nro. de Serie de Guía de Remisión");
                }

                if (txtSerie.Text == "0000")
                {
                    oBase = txtSerie;
                    throw new ArgumentException("N° de Serie no registrado, registrar N° serie en REGISTRO DE TIPOS DE DOCUMENTOS");
                }

                if (txtSerie.Text.Length != 4)
                {
                    oBase = txtSerie;
                    throw new ArgumentException("N° de Serie debe ser de 4 cacteres");
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

                if (Convert.ToInt32(lkpMotivo.EditValue) == 226)
                {
                    if (Convert.ToInt32(btnAlmacenIngreso.Tag) == 0)
                    {
                        oBase = btnAlmacenIngreso;
                        throw new ArgumentException("Ingrese el Almacén para el ingreso del Producto");
                    }
                }
                if (Convert.ToInt32(bteProyecto.Tag) > 0)
                {
                    if (Convert.ToInt32(btnCentroCostos.Tag) < 0)
                    {
                        throw new ArgumentException("Ingrese Centro Costo");
                    }
                }
                if (string.IsNullOrEmpty(btnUbigeo.Text))
                {
                    oBase = btnUbigeo;
                    throw new ArgumentException("Ingrese Ubigeo del Cliente");
                }

                if (lkpModalidadTraslado.EditValue.ToString() == "02")
                {
                    if (ConductorIngresado == false)
                    {
                        oBase = lkpModalidadTraslado;
                        throw new ArgumentException("Ingrese los datos del conductor");
                    }
                    if (string.IsNullOrEmpty(txtVehiMarPla.Text))
                    {
                        oBase = txtVehiMarPla;
                        throw new ArgumentException("Ingrese placa del vehículo");
                    }
                }

                if (btnUbigeo.Text != new BVentas().UbigeoGet(btnUbigeo.Text).ubigeo_inei)
                {
                    oBase = btnUbigeo;
                    throw new ArgumentException("Ingrese un Ubigeo correcto");
                }

                if (string.IsNullOrEmpty(txtRUC.Text))
                {
                    oBase = txtRUC;
                    throw new ArgumentException("Ingrese RUC o el Nro Documento del Destinatario");
                }

                if (string.IsNullOrEmpty(txtDesMotivo.Text) && Convert.ToInt32(lkpMotivo.EditValue) == (int)MotivoGuiaRemision.Otros)
                {
                    oBase = txtDesMotivo;
                    throw new ArgumentException("Ingrese la descripción del motivo");
                }
                oBe.NomClie = btnCliente.Text;
                oBe.remic_vnumero_remision = string.Format("{0}{1}", txtSerie.Text, txtNumero.Text);
                oBe.remic_sfecha_remision = Convert.ToDateTime(dteFecha.Text);
                oBe.cliec_icod_cliente = Convert.ToInt32(btnCliente.Tag);
                oBe.remic_vnombre_destinatario = btnCliente.Text;
                oBe.remic_vnombre_destinatario = txtDestinatario.Text;
                oBe.remic_vruc_destinatario = txtRUC.Text;
                oBe.remic_vdireccion_procedencia = txtPartida.Text;
                oBe.remic_vdireccion_destinatario = txtLlegada.Text;
                oBe.almac_icod_almacen = Convert.ToInt32(btnAlmacen.Tag);
                oBe.tablc_iid_motivo = Convert.ToInt32(lkpMotivo.EditValue);
                oBe.tranc_icod_transportista = Convert.ToInt32(btnTransportista.Tag);
                oBe.remic_vlicencia = txtLicenciaConducir.Text;
                oBe.remic_vruc_transportista = txtRUCTransportista.Text;
                oBe.tablc_iid_situacion_remision = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.remic_vreferencia = txtFactura.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.tablc_iid_situacion_remision = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.remic_sfecha_inicio = Convert.ToDateTime(dteFechaTranslado.Text);
                oBe.almac_icod_almacen_ingreso = Convert.ToInt32(btnAlmacenIngreso.Tag);
                oBe.pryc_icod_proyecto = Convert.ToInt32(bteProyecto.Tag);
                oBe.cecoc_icod_centro_costo = Convert.ToInt32(btnCentroCostos.Tag);
                oBe.remic_vmarca_placa = txtVehiMarPla.Text;
                oBe.remic_vcertif_inscripcion = txtCertificado.Text;
                oBe.tdocc_vabreviatura_tipo_doc = lkpTipoDocRef.Text;
                oBe.remic_num_comprobante = bteNroDocRef.Text;
                oBe.remic_inum_cajas = Convert.ToInt32(txtTotalCajas.Text);
                oBe.remic_vubigeo_destino = btnUbigeo.Text;
                oBe.remic_itipo_guia = TipodeGuia;
                oBe.remic_vdescripcion_motivo = txtDesMotivo.Text;
                #region Guia Remision Electronica
                oBe.guiaRemisionElectronica.FechaEmision = oBe.remic_sfecha_remision.ToString("yyyy-MM-dd");
                oBe.guiaRemisionElectronica.HoraEmision = DateTime.Now.ToString("H:mm:ss");
                oBe.guiaRemisionElectronica.TipoDocumento = "09";
                oBe.guiaRemisionElectronica.IdDocumento = txtSerie.Text + "-" + txtNumero.Text;
                oBe.guiaRemisionElectronica.numDocDestinatario = txtRUC.Text;
                oBe.guiaRemisionElectronica.numDocRemitente = Valores.strRUC;
                oBe.guiaRemisionElectronica.tipDocRemitente = "6";
                oBe.guiaRemisionElectronica.rznSocialRemitente = Valores.strNombreEmpresa;

                oBe.guiaRemisionElectronica.tipDocDestinatario = oBe.cliec_itipo_documento.ToString();
                oBe.guiaRemisionElectronica.rznSocialDestinatario = txtDestinatario.Text;

                oBe.guiaRemisionElectronica.DescripcionMotivo = oBe.remic_vdescripcion_motivo;
                oBe.guiaRemisionElectronica.PesoBrutoTotal = 100;
                oBe.guiaRemisionElectronica.UmPesoBruto = "KGM";
                oBe.guiaRemisionElectronica.NumBultos = lstDetalle.Sum(x => x.dremc_ncantidad_producto);
                oBe.guiaRemisionElectronica.ModalidadTraslado = lkpModalidadTraslado.EditValue.ToString();
                oBe.guiaRemisionElectronica.FechaInicioTraslado = dteFechaTranslado.DateTime.ToString("yyyy-MM-dd");
                oBe.guiaRemisionElectronica.RucTransportista = txtRUCTransportista.Text;
                oBe.guiaRemisionElectronica.RazonSocialTransportista = btnTransportista.Text;
                oBe.guiaRemisionElectronica.TipoDocumentoTransportista = "6";
                oBe.guiaRemisionElectronica.NroPlacaVehiculo = txtVehiMarPla.Text;
                oBe.guiaRemisionElectronica.NroDocumentoConductor = txtRUCTransportista.Text;
                oBe.guiaRemisionElectronica.ubiLlegada = oBe.remic_vubigeo_destino;
                oBe.guiaRemisionElectronica.dirLlegada = txtLlegada.Text;
                oBe.guiaRemisionElectronica.ubiPartida = "010101";
                oBe.guiaRemisionElectronica.dirPartida = txtPartida.Text;


                #endregion


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (TipodeGuia == 1 || oBe.remic_itipo_guia == 1)
                    {
                        oBe.remic_itotal_pares = Convert.ToInt32(txtTotalPares.Text);
                        oBe.remic_icod_remision = new BVentas().insertarGuiaRemision(oBe, lstDetalle, objConductor);
                    }
                    else
                    {
                        oBe.remic_icod_remision = new BVentas().insertarGuiaRemisionMP(oBe, lstMPDetalle, objConductor);
                    }
                }
                else
                {
                    if (TipodeGuia == 1 || oBe.remic_itipo_guia == 1)
                    {
                        oBe.remic_itotal_pares = Convert.ToInt32(txtTotalPares.Text);
                        new BVentas().modificarGuiaRemision(oBe, lstDetalle, lstDelete, objConductor);
                    }
                    else
                    {
                        new BVentas().modificarGuiaRemisionMP(oBe, lstMPDetalle, lstMPDelete, objConductor);
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
                    MiEvento(oBe.remic_icod_remision);
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

        private void ModificarDetalleProductoTerminado()
        {
            EGuiaRemisionDet obe = (EGuiaRemisionDet)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null) return;
            if (obe.dremc_itipo_producto == (int)TipoProducto.Mercadería)
            {


                using (frmManteGuiaRemisionDetalle frm = new frmManteGuiaRemisionDetalle())
                {
                    frm.oBe = obe;
                    frm.lstDetalle = lstDetalle;

                    frm.SetModify();
                    frm.Redimencionarstock();
                    frm.txtItem.Text = String.Format("{0:000}", obe.dremc_inro_item);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        viewGuiaRemision.RefreshData();

                        TotalizarPares();
                    }
                }
            }
            else
            {
                using (frmManteGuiaRemisionServicioDetalle frm = new frmManteGuiaRemisionServicioDetalle())
                {
                    frm.TipoGuia = (int)TipoGuiaRemision.ProductoTerminado;
                    frm.oBe = obe;
                    frm.lstDetalle = lstDetalle;
                    frm.SetModify();
                    frm.txtItem.Text = String.Format("{0:000}", obe.dremc_inro_item);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        viewGuiaRemisionMP.RefreshData();
                    }
                }

            }
        }

        private void ModificarDetalleSuministros()
        {
            EGuiaRemisionMPDet obe = (EGuiaRemisionMPDet)viewGuiaRemisionMP.GetRow(viewGuiaRemisionMP.FocusedRowHandle);
            if (obe == null)
                return;
            if (obe.dremc_itipo_producto == (int)TipoProducto.Mercadería)
            {
                using (frmManteGuiaRemisionMPDetalle frm = new frmManteGuiaRemisionMPDetalle())
                {
                    frm.oBe = obe;
                    frm.lstDetalle = lstMPDetalle;
                    frm.SetModify();
                    frm.UmSunat = obe.CodigoSUNAT;
                    frm.txtItem.Text = String.Format("{0:000}", obe.dremc_inro_item);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstMPDetalle = frm.lstDetalle;
                        viewGuiaRemisionMP.RefreshData();

                    }
                }
            }
            else
            {
                using (frmManteGuiaRemisionServicioDetalle frm = new frmManteGuiaRemisionServicioDetalle())
                {
                    frm.TipoGuia = (int)TipoGuiaRemision.Suministros;
                    frm.oBeMt = obe;
                    frm.lstMPDetalle = lstMPDetalle;
                    frm.SetModify();
                    frm.txtItem.Text = String.Format("{0:000}", obe.dremc_inro_item);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        viewGuiaRemisionMP.RefreshData();
                    }
                }

            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (TipodeGuia == (int)TipoGuiaRemision.Suministros || oBe.remic_itipo_guia == (int)TipoGuiaRemision.Suministros)
            {
                ModificarDetalleSuministros();
            }
            else
            {
                ModificarDetalleProductoTerminado();
            }

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EGuiaRemisionDet obe = (EGuiaRemisionDet)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstDetalle.Remove(obe);
            viewGuiaRemision.RefreshData();
            TotalizarPares();
        }

        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
        }
        private void listarAlmacen()
        {

            if (TipodeGuia == 1 || oBe.remic_itipo_guia == 1)
            {
                using (FrmListarAlmacen Almacen = new FrmListarAlmacen())
                {
                    Almacen.puvec_icod_punto_venta = 2;
                    if (Almacen.ShowDialog() == DialogResult.OK)
                    {
                        btnAlmacen.Tag = Almacen._Be.id_almacen;
                        btnAlmacen.Text = Almacen._Be.descripcion;
                        if (Convert.ToInt32(lkpMotivo.EditValue) == 226)
                        {
                            txtPartida.Text = Almacen._Be.direccion;
                        }
                        else
                        {
                            txtPartida.Text = Almacen._Be.direccion;
                        }
                        oBe.almac_icod_almacen = Almacen._Be.id_almacen;
                    }
                }
            }
            else
            {
                using (frmListarAlmacen frm = new frmListarAlmacen())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnAlmacen.Tag = frm._Be.almac_icod_almacen;
                        btnAlmacen.Text = frm._Be.almac_vdescripcion;
                        if (Convert.ToInt32(lkpMotivo.EditValue) == 226)
                        {
                            txtPartida.Text = frm._Be.almac_vdireccion;
                        }
                        else
                        {
                            txtPartida.Text = frm._Be.almac_vdireccion;
                        }
                    }
                }
            }

        }
        private void listarAlmacenIngreso()
        {

            if (TipodeGuia == 1 || oBe.remic_itipo_guia == 1)
            {
                using (FrmListarAlmacen Almacen = new FrmListarAlmacen())
                {
                    Almacen.puvec_icod_punto_venta = 2;
                    if (Almacen.ShowDialog() == DialogResult.OK)
                    {
                        btnAlmacenIngreso.Tag = Almacen._Be.id_almacen;
                        btnAlmacenIngreso.Text = Almacen._Be.descripcion;
                        if (Convert.ToInt32(lkpMotivo.EditValue) == 226)
                        {
                            txtLlegada.Text = Almacen._Be.direccion;
                        }
                        oBe.almac_icod_almacen = Almacen._Be.id_almacen;
                    }
                }
            }
            else
            {
                using (frmListarAlmacen frm = new frmListarAlmacen())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnAlmacenIngreso.Tag = frm._Be.almac_icod_almacen;
                        btnAlmacenIngreso.Text = frm._Be.almac_vdescripcion;
                    }
                }
            }

        }
        private void bteCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarCliente();
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

        private void CargarDetallePedido()
        {
            int contSinStock = 0;
            List<EPedidoClienDet> lstDetallePe = new List<EPedidoClienDet>();
            //lstDetallePe = new BVentas().listarPedidoVentaDet(Convert.ToInt32(btnPedido.Tag), Parametros.intEjercicio);
            foreach (var _be in lstDetallePe)
            {
                decimal stock = new BAlmacen().listarStockProductoPorAlmacen(Convert.ToInt32(btnAlmacen.Tag), _be.prdc_icod_producto);
                if (stock >= _be.lpedid_nCant_pedido)
                {
                    EGuiaRemisionDet _bee = new EGuiaRemisionDet();
                    _bee.dremc_icod_detalle_remision = 0;

                    _bee.remic_icod_remision = 0;
                    if (lstDetalle.Count == 0)
                    {
                        _bee.dremc_inro_item = 1;
                    }
                    else
                    {
                        _bee.dremc_inro_item = Convert.ToInt16(lstDetalle.Max(x => x.dremc_inro_item) + 1);
                    }
                    _bee.prdc_icod_producto = Convert.ToInt32(_be.prdc_icod_producto);
                    _bee.dremc_ncantidad_producto = _be.lpedid_nCant_pedido;
                    _bee.dremc_vcantidad_producto = _be.lpedid_nCant_pedido.ToString();
                    _bee.kardc_icod_correlativo = 0;
                    _bee.tablc_iid_sit_item_guia_remision = 0;
                    _bee.strCodProducto = _be.prdc_vcode_producto;
                    _bee.strDesProducto = _be.prdc_vdescripcion_larga;
                    _bee.strCategoria = _be.strCategoria;
                    _bee.strSubCategoriaUno = _be.strSubCategoriaUno;
                    _bee.strDesUM = _be.StrUnidadMedida;
                    _bee.dremc_vobservaciones = "";
                    _bee.dblStockDisponible = Convert.ToDecimal(_be.lpedid_nstock_producto);
                    _bee.dremc_PastBibli = false;
                    _bee.dremc_nDescuento = 0;
                    _bee.dremc_nprecio_lista = 0;
                    _bee.dremc_nPrecio_Unitario = Convert.ToDecimal(_be.lpedid_nprecio_uni);
                    _bee.dremc_nMonto_Total = Convert.ToDecimal(_be.lpedid_nCant_pedido) * Convert.ToDecimal(_be.lpedid_nprecio_uni);
                    _bee.dremc_PastBibli = false;
                    _bee.intTipoOperacion = 1;
                    lstDetalle.Add(_bee);
                }
                else
                {
                    XtraMessageBox.Show("EL producto " + _be.prdc_vdescripcion_larga + " tiene stock insuficiente para cubrir la cantidad de " + _be.lpedid_nCant_pedido.ToString() + " Productos.", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            grdGuiaRemision.DataSource = lstDetalle;
            viewGuiaRemision.RefreshData();
        }
        private void cargarPedidoDetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnAlmacen.Tag) == 0)
            {
                XtraMessageBox.Show("Seleccione el Almacén", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CargarDetallePedido();
        }

        private void txtSerie_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                getNroDoc();
            }
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacenIngreso();
        }

        private void lkpMotivo_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpMotivo.EditValue) == 226)
            {
                btnAlmacenIngreso.Enabled = true;
            }
            else
            {
                btnAlmacenIngreso.Enabled = false;
            }
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
                            }
                        }
                        obe.intUsuario = Valores.intUsuario;
                        obe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                        lstDetalleaUX.Add(obe);

                    }
                }

                foreach (var _BEE in lstDetalleaUX)
                {
                    List<EProducto> MlistProduc = new BAlmacen().listarProductoXCodigp(Parametros.intEjercicio, _BEE.strCodProducto.Trim());
                    if (MlistProduc.Count > 0)
                    {
                        EGuiaRemisionDet _BEGuia = new EGuiaRemisionDet();
                        _BEGuia.dremc_icod_detalle_remision = 0;
                        _BEGuia.remic_icod_remision = 0;
                        if (lstDetalle.Count() == 0)
                            _BEGuia.dremc_inro_item = 1;
                        else
                            _BEGuia.dremc_inro_item = Convert.ToInt16(lstDetalle.Count() + 1);
                        _BEGuia.prdc_icod_producto = Convert.ToInt32(MlistProduc[0].prdc_icod_producto);
                        _BEGuia.dremc_ncantidad_producto = _BEE.invd_icantidad;
                        _BEGuia.dremc_vcantidad_producto = _BEE.invd_icantidad.ToString();
                        _BEGuia.kardc_icod_correlativo = 0;
                        _BEGuia.tablc_iid_sit_item_guia_remision = 1;
                        _BEGuia.strCodProducto = MlistProduc[0].prdc_vcode_producto;
                        _BEGuia.strDesProducto = MlistProduc[0].prdc_vdescripcion_larga;
                        _BEGuia.strCategoria = MlistProduc[0].strCategoria;
                        _BEGuia.strSubCategoriaUno = MlistProduc[0].strSubCategoriaUno;
                        _BEGuia.strDesUM = MlistProduc[0].StrUnidadMedida;
                        _BEGuia.dremc_vobservaciones = "";
                        _BEGuia.dblStockDisponible = new BAlmacen().listarStockProductoPorAlmacen(Convert.ToInt32(btnAlmacen.Tag), Convert.ToInt32(MlistProduc[0].prdc_icod_producto));
                        _BEGuia.dremc_PastBibli = false;
                        _BEGuia.dremc_nDescuento = Convert.ToDecimal(MlistProduc[0].prdc_nPor_Descuento);
                        _BEGuia.dremc_nprecio_lista = Convert.ToDecimal(0);
                        _BEGuia.dremc_nPrecio_Unitario = Convert.ToDecimal(MlistProduc[0].prdc_nPrecio_venta);
                        _BEGuia.dremc_nMonto_Total = Convert.ToDecimal(_BEGuia.dremc_nPrecio_Unitario) * Convert.ToDecimal(_BEE.invd_icantidad);
                        _BEGuia.kardc_icod_correlativo_ingreso = 0;
                        _BEGuia.intTipoOperacion = 1;
                        lstDetalle.Add(_BEGuia);
                    }

                }
                viewGuiaRemision.RefreshData();
                viewGuiaRemision.MoveLast();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error Fila: " + contF + "\t Columna: " + nomC, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCliente();
        }

        private void btnAlmacenIngreso_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacenIngreso();
        }

        private void btnAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
        }

        private void lkpMotivo_EditValueChanged_2(object sender, EventArgs e)
        {

            btnAlmacenIngreso.Enabled = Convert.ToInt32(lkpMotivo.EditValue) == (int)MotivoGuiaRemision.TransfEntreEstablecimientosMismaEmpr;
            btnCliente.Enabled = Convert.ToInt32(lkpMotivo.EditValue) != (int)MotivoGuiaRemision.TransfEntreEstablecimientosMismaEmpr;
            lblDesMotivo.Visible = Convert.ToInt32(lkpMotivo.EditValue) == (int)MotivoGuiaRemision.Otros;
            txtDesMotivo.Visible = Convert.ToInt32(lkpMotivo.EditValue) == (int)MotivoGuiaRemision.Otros;
            txtDesMotivo.Text = Convert.ToInt32(lkpMotivo.EditValue) != (int)MotivoGuiaRemision.Otros ? lkpMotivo.Text : "";
            var selectmotivo = new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMotivoGuiaRemision).Where(x => x.tarec_iid_tabla_registro == Convert.ToInt32(lkpMotivo.EditValue)).FirstOrDefault();
            oBe.guiaRemisionElectronica.CodigoMotivoTraslado = selectmotivo.tarec_vtipo_operacion;
        }

        private void btnTransportista_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            if (Convert.ToInt32(lkpModalidadTraslado.EditValue) == 0)
            {
                XtraMessageBox.Show("Ingrese la Modalidad de Traslado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (frmListarTransportista frm = new frmListarTransportista())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (lkpModalidadTraslado.EditValue.ToString() == "02")
                    {
                        if (frm._Be.tranc_vruc != Valores.strRUC)
                        {
                            XtraMessageBox.Show("Si la modalidad de traslado es PRIVADO el transportista debe ser igual al EMISOR", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (string.IsNullOrEmpty(frm._Be.tranc_cnumero_dni))
                        {
                            XtraMessageBox.Show("El transportista seleccionado no tiene registrado los datos del conductor", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }


                    }

                    btnTransportista.Tag = frm._Be.tranc_icod_transportista;
                    btnTransportista.Text = frm._Be.tranc_vnombre_transportista;
                    txtRUCTransportista.Text = frm._Be.tranc_vruc;
                    txtVehiMarPla.Text = frm._Be.tranc_vnum_placa;
                    txtLicencia.Text = frm._Be.tranc_vnum_licencia_conducir;

                }
            }
        }

        private void btnCentroCostos_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCentroCosto();
        }
        private void ListarCentroCosto()
        {
            using (frmListarCentroCostoProyectos Ccosto = new frmListarCentroCostoProyectos())
            {

                if (Ccosto.ShowDialog() == DialogResult.OK)
                {
                    btnCentroCostos.Text = Ccosto._Be.cecoc_vcodigo_centro_costo;//cecoc_ccodigo_centro_costo - centro_costo
                    btnCentroCostos.Tag = Ccosto._Be.cecoc_icod_centro_costo;//cecoc_icod_centro_costo (correlativo) - centro_costo

                }
            }
        }

        private void bteProyecto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProyecto();
        }
        private void ListarProyecto()
        {
            using (frmListarProyectos frm = new frmListarProyectos())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteProyecto.Tag = frm._Be.pryc_icod_proyecto;
                    bteProyecto.Text = frm._Be.pryc_vdescripcion;
                    //btnCentroCostos.Tag = frm._Be.pryc_icod_ccosto;
                    //btnCentroCostos.Text = frm._Be.CentroCosto;
                    //btnAlmacenIngreso.Tag = frm._Be.almac_icod_almacen;
                    //btnAlmacenIngreso.Text = frm._Be.StrAlmacen;
                    txtLlegada.Text = frm._Be.StrAlmacenDir;
                }
            }
        }
        public void AlmacenPrincipal()
        {
            //List<EAlmacen> lstAlamcen = new List<EAlmacen>();
            //lstAlamcen = new BAlmacen().listarAlmacenes().Where(x => x.almac_icod_almacen == 53).ToList();
            //btnAlmacen.Text = lstAlamcen[0].almac_vdescripcion;
            //txtPartida.Text = lstAlamcen[0].almac_vdireccion;
            //btnAlmacen.Tag = 53;
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
                lst.Add(new TipoDoc { intCodigo = 0, strTipoDoc = ".......Eligir un documento....." });
                lst.Add(new TipoDoc { intCodigo = 26, strTipoDoc = "FAV" });
                lst.Add(new TipoDoc { intCodigo = 9, strTipoDoc = "BOV" });
                BSControls.LoaderLook(lkpTipoDocRef, lst, "strTipoDoc", "intCodigo", true);
                lkpTipoDocRef.Text = oBe.tdocc_vabreviatura_tipo_doc;
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

        private void bteNroDocRef_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            if (TipodeGuia == 1 || oBe.remic_itipo_guia == 1)
            {

                FrmListarDoRef frm = new FrmListarDoRef();
                frm.TipoDoc = Convert.ToInt32(lkpTipoDocRef.EditValue);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteNroDocRef.Text = frm.EDocPorCobrar.doxcc_vnumero_doc;
                }
            }
            else
            {
                FrmListarDoRefMP frm = new FrmListarDoRefMP();
                frm.TipoDoc = Convert.ToInt32(lkpTipoDocRef.EditValue);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteNroDocRef.Text = frm.EDocPorCobrar.favc_vnumero_factura;
                }
            }
        }

        private void btnDireccion_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //FrmListarDirecionCliente Cliente = new FrmListarDirecionCliente();
            //Cliente.cliec_icod_cliente = Convert.ToInt32(btnCliente.Tag);
            //Cliente.Show();
            //txtLlegada.Text = Cliente._Be.dc_vdireccion;


            try
            {
                using (FrmListarDirecionCliente frm = new FrmListarDirecionCliente())
                {
                    frm.cliec_icod_cliente = Convert.ToInt32(btnCliente.Tag);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        txtLlegada.Text = frm._Be.dc_vdireccion;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (FrmListarDirecionCliente frm = new FrmListarDirecionCliente())
                {
                    frm.cliec_icod_cliente = Convert.ToInt32(btnCliente.Tag);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        txtLlegada.Text = frm._Be.dc_vdireccion;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (FrmListarDirecionCliente frm = new FrmListarDirecionCliente())
                {
                    frm.cliec_icod_cliente = Convert.ToInt32(btnCliente.Tag);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        txtLlegada.Text = frm._Be.dc_vdireccion;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public class EIntervalo
        {
            public int serie1 { get; set; }
            public int serie2 { get; set; }
            public string codigoExterno { get; set; }
            public string Descripcion { get; set; }
            public string Unidad_medida { get; set; }
            public int icod_unidad_medida { get; set; }
            public string pr_viid_modelo { get; set; }

        }
        private void importarProductosTXTToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (TipodeGuia == 1 || oBe.remic_itipo_guia == 1)
            {

                listaProducto.Clear();
                grdGuiaRemision.DataSource = null;

                mlist = (new BCentral()).ListarProdProductos();
                OpenFileDialog dialogo = new OpenFileDialog();
                if (dialogo.ShowDialog() == DialogResult.OK)
                {
                    StreamReader objReader = new StreamReader(dialogo.FileName);
                    string Extension = Path.GetExtension(dialogo.FileName);
                    if (Extension == ".txt" || Extension == ".TXT")
                    {
                        string sLine = "";
                        ArrayList arrText = new ArrayList();

                        ArrayList resultadoCodigos = new ArrayList();
                        ArrayList cantidades = new ArrayList();

                        while (sLine != null)
                        {
                            sLine = objReader.ReadLine();
                            if (sLine != null)
                            {
                                //sLine = sLine.Substring(0, 15);
                                arrText.Add(sLine);
                            }
                        }
                        objReader.Close();


                        int ind = 0;
                        foreach (string codigos in arrText)
                        {
                            int cont = 0;
                            foreach (string codigosauxiliares in arrText)
                            {
                                if (resultadoCodigos.Contains(codigos) == false)
                                {
                                    if (codigos.Trim() == codigosauxiliares.Trim())
                                    {
                                        arrText.Remove(ind);
                                        cont++;

                                    }
                                }
                            }
                            resultadoCodigos.Add(codigos);
                            if (cont != 0)
                            {
                                cantidades.Add(cont);
                            }
                            ind++;
                        }
                        object[] cant = cantidades.ToArray();

                        foreach (string sOutput in arrText)
                        {

                            List<EProdProducto> auxproducto = new List<EProdProducto>();
                            EImportarProducto obj = new EImportarProducto();
                            if (sOutput.Length == 15)
                            {
                                obj.codigo_externo = sOutput.Trim();

                                auxproducto = mlist.Where(ob => ob.pr_vcodigo_externo.Trim() == sOutput.Trim()).ToList();

                                if (listaProducto.Where(jo => jo.codigo_externo.Trim() == sOutput.Trim()).LongCount() < 1)
                                {
                                    if (auxproducto.Count == 1)
                                    {
                                        obj.producto_descripcion = auxproducto[0].pr_vdescripcion_producto;
                                        obj.icod_producto = auxproducto[0].pr_icod_producto;
                                        obj.icod_unidad_medida = Convert.ToInt32(auxproducto[0].unidc_icod_unidad_medida);
                                        obj.Unidad_medida = auxproducto[0].strUM;
                                        obj.pr_viid_modelo = auxproducto[0].pr_viid_modelo;
                                        //obj.cantidad = auxproducto[0].stocc_nstock_prod;

                                        obj.estado = true;
                                        listaProducto.Add(obj);
                                    }
                                    else
                                    {
                                        obj.estado = false;
                                        listaProductoNoExiste.Add(obj);
                                    }

                                }
                            }
                            else
                            {
                                obj.codigo_externo = sOutput.Trim();
                                listaProductoNoExiste.Add(obj);
                            }
                        }

                        int j = 0;
                        foreach (EImportarProducto obj in listaProducto)
                        {
                            obj.cantidad = Convert.ToInt32(cant[j]);
                            j++;
                        }

                        List<EProdInventarioFisicoDetalle> ListaImportacion = new List<EProdInventarioFisicoDetalle>();
                        /**/
                        foreach (EImportarProducto obj in listaProducto)
                        {
                            EProdInventarioFisicoDetalle objInvFisico = new EProdInventarioFisicoDetalle();
                            if (obj.estado == true)
                            {
                                objInvFisico.pr_vcodigo_externo = obj.codigo_externo;
                                objInvFisico.pr_icod_producto = obj.icod_producto;
                                objInvFisico.pr_vdescripcion_producto = obj.producto_descripcion;
                                objInvFisico.infid_ncant_fisica = 0;
                                objInvFisico.infid_ncant_fisica = obj.cantidad;
                                objInvFisico.icod_unidad_medida = obj.icod_unidad_medida;
                                objInvFisico.Unidad_medida = obj.Unidad_medida;
                                objInvFisico.pr_viid_modelo = obj.pr_viid_modelo;
                                ListaImportacion.Add(objInvFisico);
                            }
                        }



                        List<EProdInventarioFisicoDetalle> listaInventarioDetalle = new List<EProdInventarioFisicoDetalle>();
                        List<EIntervalo> mlistadeSeries = new List<EIntervalo>();

                        listaInventarioDetalle = ListaImportacion;

                        foreach (EProdInventarioFisicoDetalle obj in listaInventarioDetalle)
                        {
                            ArrayList tallasAculadas = new ArrayList();
                            foreach (EProdInventarioFisicoDetalle objAux in listaInventarioDetalle)
                            {
                                if (obj.pr_vcodigo_externo.Substring(0, 13).Trim() == objAux.pr_vcodigo_externo.Substring(0, 13).Trim() && objAux.flag == false)
                                {
                                    tallasAculadas.Add(Convert.ToInt32(objAux.pr_vcodigo_externo.Substring(13, 2)));
                                    objAux.flag = true;
                                }
                            }

                            object[] tallasAcumuladasArray = tallasAculadas.ToArray();
                            if (tallasAcumuladasArray.LongCount() > 0)
                            {
                                int TallaMayor = Convert.ToInt32(tallasAcumuladasArray[0]);
                                int TallaMenor = Convert.ToInt32(tallasAcumuladasArray[0]);
                                for (int i = 0; i < tallasAcumuladasArray.Length; i++)
                                {
                                    if (Convert.ToInt32(tallasAcumuladasArray[i]) > TallaMayor)
                                    {
                                        TallaMayor = Convert.ToInt32(tallasAcumuladasArray[i]);
                                    }
                                    if (Convert.ToInt32(tallasAcumuladasArray[i]) < TallaMenor)
                                    {
                                        TallaMenor = Convert.ToInt32(tallasAcumuladasArray[i]);
                                    }
                                }
                                EIntervalo objEintervalo = new EIntervalo();
                                objEintervalo.serie1 = TallaMenor;
                                objEintervalo.serie2 = TallaMayor;
                                objEintervalo.codigoExterno = obj.pr_vcodigo_externo.Substring(0, 13);
                                objEintervalo.Descripcion = obj.pr_vdescripcion_producto.Substring(0, obj.pr_vdescripcion_producto.Length - 2);
                                objEintervalo.icod_unidad_medida = obj.icod_unidad_medida;
                                objEintervalo.Unidad_medida = obj.Unidad_medida;
                                objEintervalo.pr_viid_modelo = obj.pr_viid_modelo;
                                mlistadeSeries.Add(objEintervalo);
                            }


                        }
                        GuardarDetalleConCodigosImportados(listaInventarioDetalle, mlistadeSeries);

                    }
                    else
                    {
                        XtraMessageBox.Show("La Extensión del Archivo no es correcto", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
                grdGuiaRemision.DataSource = lstDetalle;
                TotalizarPares();
                //gridNoexisten.DataSource = listaProductoNoExiste;

            }
            else
            {
                listaProducto.Clear();
                grdGuiaRemision.DataSource = null;

                mlist = (new BCentral()).ListarProdProductos();
                OpenFileDialog dialogo = new OpenFileDialog();
                if (dialogo.ShowDialog() == DialogResult.OK)
                {
                    StreamReader objReader = new StreamReader(dialogo.FileName);
                    string Extension = Path.GetExtension(dialogo.FileName);
                    if (Extension == ".txt" || Extension == ".TXT")
                    {
                        string sLine = "";
                        ArrayList arrText = new ArrayList();

                        ArrayList resultadoCodigos = new ArrayList();
                        ArrayList cantidades = new ArrayList();

                        while (sLine != null)
                        {
                            sLine = objReader.ReadLine();
                            if (sLine != null)
                            {
                                //sLine = sLine.Substring(0, 15);
                                arrText.Add(sLine);
                            }
                        }
                        objReader.Close();


                        int ind = 0;
                        foreach (string codigos in arrText)
                        {
                            int cont = 0;
                            foreach (string codigosauxiliares in arrText)
                            {
                                if (resultadoCodigos.Contains(codigos) == false)
                                {
                                    if (codigos.Trim() == codigosauxiliares.Trim())
                                    {
                                        arrText.Remove(ind);
                                        cont++;

                                    }
                                }
                            }
                            resultadoCodigos.Add(codigos);
                            if (cont != 0)
                            {
                                cantidades.Add(cont);
                            }
                            ind++;
                        }
                        object[] cant = cantidades.ToArray();

                        foreach (string sOutput in arrText)
                        {

                            List<EProdProducto> auxproducto = new List<EProdProducto>();
                            EImportarProducto obj = new EImportarProducto();
                            if (sOutput.Length == 15)
                            {
                                obj.codigo_externo = sOutput.Trim();

                                auxproducto = mlist.Where(ob => ob.pr_vcodigo_externo.Trim() == sOutput.Trim()).ToList();

                                if (listaProducto.Where(jo => jo.codigo_externo.Trim() == sOutput.Trim()).LongCount() < 1)
                                {
                                    if (auxproducto.Count == 1)
                                    {
                                        obj.producto_descripcion = auxproducto[0].pr_vdescripcion_producto;
                                        obj.icod_producto = auxproducto[0].pr_icod_producto;
                                        obj.icod_unidad_medida = Convert.ToInt32(auxproducto[0].unidc_icod_unidad_medida);
                                        obj.Unidad_medida = auxproducto[0].strUM;
                                        //obj.cantidad = auxproducto[0].stocc_nstock_prod;

                                        obj.estado = true;
                                        listaProducto.Add(obj);
                                    }
                                    else
                                    {
                                        obj.estado = false;
                                        listaProductoNoExiste.Add(obj);
                                    }

                                }
                            }
                            else
                            {
                                obj.codigo_externo = sOutput.Trim();
                                listaProductoNoExiste.Add(obj);
                            }
                        }
                        int j = 0;
                        int count = 0;
                        foreach (EImportarProducto obj in listaProducto)
                        {
                            EGuiaRemisionMPDet _BeGR = new EGuiaRemisionMPDet();
                            count++;
                            _BeGR.dremc_inro_item = Convert.ToInt16(count);
                            _BeGR.prdc_icod_producto = obj.icod_producto;
                            _BeGR.strCodProducto = obj.codigo_externo;
                            _BeGR.strDesProducto = obj.producto_descripcion;
                            _BeGR.unidc_icod_unidad_medida = obj.icod_unidad_medida;
                            _BeGR.strDesUM = obj.Unidad_medida;
                            _BeGR.dremc_ncantidad_producto = Convert.ToInt32(cant[j]);
                            obj.cantidad = Convert.ToInt32(cant[j]);
                            j++;
                            lstMPDetalle.Add(_BeGR);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("La Extensión del Archivo no es correcto", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
                grdGuiaRemisionMP.DataSource = lstMPDetalle;
                TotalizarPares();
            }



        }
        private void GuardarDetalleConCodigosImportados(List<EProdInventarioFisicoDetalle> mlitaProductos, List<EIntervalo> mlistaIntervalos)
        {
            int i = 1;
            foreach (EIntervalo objIntervalo in mlistaIntervalos)
            {
                ArrayList AcumuladoKardex = new ArrayList();
                ArrayList Tallas = new ArrayList();
                ArrayList cantidades = new ArrayList();

                mlitaProductos.OrderBy(o => o.pr_vcodigo_externo.Substring(0, 13).Trim());

                foreach (EProdInventarioFisicoDetalle objProduc in mlitaProductos)
                {
                    if (objProduc.pr_vcodigo_externo.Substring(0, 13).Trim() == objIntervalo.codigoExterno.Trim())
                    {
                        string codigoexterno = objProduc.pr_vcodigo_externo;
                        //oProductoStock = new BCentral().VerificarProdStockProducto(codigoexterno, Convert.ToInt32(btncodigoalmacen.Tag), Convert.ToInt32(lkpPuntoVenta.EditValue));
                        //if (oProductoStock.Count > 0)
                        //{
                        Tallas.Add(objProduc.pr_vcodigo_externo.Substring(13, 2));
                        cantidades.Add(objProduc.infid_ncant_fisica);
                        //}
                    }
                }

                object[] TallasArray = Tallas.ToArray();
                object[] TallasOri = new object[10];
                for (int u = 0; u < 10; u++)
                {
                    if (TallasArray.Length > u)
                    {
                        TallasOri[u] = TallasArray[u];
                    }
                    else
                    {
                        TallasOri[u] = 0;
                    }
                }
                object[] CantidadesArray = cantidades.ToArray();
                object[] CantidadesOri = new object[10];
                for (int t = 0; t < 10; t++)
                {
                    if (CantidadesArray.Length > t)
                    {
                        CantidadesOri[t] = CantidadesArray[t];
                    }
                    else
                    {
                        CantidadesOri[t] = 0;
                    }
                }
                EGuiaRemisionDet objd = new EGuiaRemisionDet();
                objd.dremc_icod_detalle_remision = 0;
                //objd.salgc_icod_nota_salida = salgc_icod_nota_salida;
                //objd.salgc_iid_almacen = Convert.ToInt32(btnAlmacen.Tag);
                objd.dremc_inro_item = Convert.ToInt16(i);
                //objd.operacion = 1;
                //objd.tablc_iid_motivo = Convert.ToInt32(lkpMotivo.EditValue);
                objd.dremc_vcodigo_extremo_prod = objIntervalo.codigoExterno;
                objd.dremc_vdescripcion_prod = objIntervalo.Descripcion;
                objd.dremc_ncantidad_producto = mlitaProductos.Where(o => o.pr_vcodigo_externo.Substring(0, 13).Trim() == objIntervalo.codigoExterno.Trim()).Sum(ob => ob.infid_ncant_fisica);
                objd.dremc_rango_talla = objIntervalo.serie1 + "/" + objIntervalo.serie2;

                //List<ERegistroSerie> lstSerie = new BCentral().ListarRegistroSerie().Where(x => x.resec_vtalla_inicial == objIntervalo.serie1.ToString() && x.resec_vtalla_final == objIntervalo.serie2.ToString()).ToList();
                List<ERegistroSerie> lstSerie = new BCentral().ListarRegistroSerie();
                lstSerie.ForEach(x =>
                {
                    if (Convert.ToInt32(x.resec_vtalla_inicial) == objIntervalo.serie1)
                    {
                        objd.dremc_iserie = Convert.ToInt32(lstSerie[0].resec_iid_registro_serie);
                        objd.resec_vdescripcion = lstSerie[0].resec_vdescripcion;
                    }
                });
                //if (lstSerie.Count > 0)
                //{              
                //objd.dremc_iserie = Convert.ToInt32(lstSerie[0].resec_iid_registro_serie);
                //objd.resec_vdescripcion = lstSerie[0].resec_vdescripcion;
                //}

                //objd.salgcd_iid_moneda = 2;
                objd.dremc_icodigo = (objIntervalo.pr_viid_modelo);
                objd.dremc_talla1 = Convert.ToInt32(TallasOri[0]);
                objd.dremc_talla2 = Convert.ToInt32(TallasOri[1]);
                objd.dremc_talla3 = Convert.ToInt32(TallasOri[2]);
                objd.dremc_talla4 = Convert.ToInt32(TallasOri[3]);
                objd.dremc_talla5 = Convert.ToInt32(TallasOri[4]);
                objd.dremc_talla6 = Convert.ToInt32(TallasOri[5]);
                objd.dremc_talla7 = Convert.ToInt32(TallasOri[6]);
                objd.dremc_talla8 = Convert.ToInt32(TallasOri[7]);
                objd.dremc_talla9 = Convert.ToInt32(TallasOri[8]);
                objd.dremc_talla10 = Convert.ToInt32(TallasOri[9]);
                objd.dremc_cant_talla1 = Convert.ToInt32((CantidadesOri[0]));
                objd.dremc_cant_talla2 = Convert.ToInt32((CantidadesOri[1]));
                objd.dremc_cant_talla3 = Convert.ToInt32((CantidadesOri[2]));
                objd.dremc_cant_talla4 = Convert.ToInt32((CantidadesOri[3]));
                objd.dremc_cant_talla5 = Convert.ToInt32((CantidadesOri[4]));
                objd.dremc_cant_talla6 = Convert.ToInt32((CantidadesOri[5]));
                objd.dremc_cant_talla7 = Convert.ToInt32((CantidadesOri[6]));
                objd.dremc_cant_talla8 = Convert.ToInt32((CantidadesOri[7]));
                objd.dremc_cant_talla9 = Convert.ToInt32((CantidadesOri[8]));
                objd.dremc_cant_talla10 = Convert.ToInt32((CantidadesOri[9]));
                objd.intUsuario = Valores.intUsuario;
                objd.unidc_icod_unidad_medida = objIntervalo.icod_unidad_medida;
                objd.strAbreUM = objIntervalo.Unidad_medida;
                //objd.salgcd_sfecha_crea = DateTime.Now;
                //objd.salgcd_iactivo = 1;
                lstDetalle.Add(objd);
                i++;
                //obld.InsertarNotaSalidaDetalle(objd);
            }
            grdGuiaRemision.DataSource = lstDetalle;
            grdGuiaRemision.RefreshDataSource();
        }
        private class EImportarProducto
        {
            public string codigo_externo { get; set; }
            public int icod_producto { get; set; }
            public string producto_descripcion { get; set; }
            public string Unidad_medida { get; set; }
            public int icod_unidad_medida { get; set; }
            public bool estado { get; set; }
            public int cantidad { get; set; }
            public string pr_viid_modelo { get; set; }

            public EImportarProducto()
            {

            }
        }

        private void grdGuiaRemisionMP_Click(object sender, EventArgs e)
        {

        }

        private void btnCliente_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lkpModalidadTraslado_EditValueChanged(object sender, EventArgs e)
        {
            btnConductor.Visible = lkpModalidadTraslado.EditValue.ToString() == "02";
            if (lkpModalidadTraslado.EditValue.ToString() == "02" && lkpModalidadTraslado.ContainsFocus)
            {
                DatosConductor();
            }

        }

        private void btnConductor_Click(object sender, EventArgs e) => DatosConductor();

        private void DatosConductor()
        {
            FrmManteConductor frm = new FrmManteConductor();
            frm.Text = $"Conductor para la GR {txtSerie.Text}-{txtNumero.Text}";
            frm.obe = objConductor;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                objConductor = frm.obe;
                ConductorIngresado = true;
            }

        }

        private void btnUbigeo_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var frm = new FrmUbigeo();
            frm.Text = "Registro de Ubigeos";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnUbigeo.Text = frm.select.ubigeo_inei;
            }

        }

        private void NuevoServicioSuministro()
        {
            using (frmManteGuiaRemisionServicioDetalle frm = new frmManteGuiaRemisionServicioDetalle())
            {
                oBe.almac_icod_almacen = Convert.ToInt32(btnAlmacen.Tag);
                frm.oBeCab = oBe;
                frm.TipoGuia = (int)TipoGuiaRemision.Suministros;
                frm.SetInsert();
                frm.lstMPDetalle = lstMPDetalle;
                frm.txtItem.Text = (lstMPDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstMPDetalle.Max(x => x.dremc_inro_item) + 1);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstMPDetalle = frm.lstMPDetalle;
                    grdGuiaRemisionMP.DataSource = lstMPDetalle;
                    viewGuiaRemisionMP.RefreshData();
                    viewGuiaRemisionMP.MoveLast();
                }
            }
        }

        private void NuevoServicioProductoterminado()
        {
            using (frmManteGuiaRemisionServicioDetalle frm = new frmManteGuiaRemisionServicioDetalle())
            {
                oBe.almac_icod_almacen = Convert.ToInt32(btnAlmacen.Tag);
                frm.oBeCab = oBe;
                frm.TipoGuia = (int)TipoGuiaRemision.ProductoTerminado;
                frm.SetInsert();
                frm.lstDetalle = lstDetalle;
                frm.txtItem.Text = (lstDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstDetalle.Max(x => x.dremc_inro_item) + 1);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    grdGuiaRemision.DataSource = lstDetalle;
                    viewGuiaRemision.RefreshData();
                    viewGuiaRemision.MoveLast();
                }
            }
        }

        private void nuevoServicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TipodeGuia == (int)TipoGuiaRemision.Suministros || oBe.remic_itipo_guia == (int)TipoGuiaRemision.Suministros)
            {
                NuevoServicioSuministro();
            }
            else
            {
                NuevoServicioProductoterminado();
            }

        }
    }
}