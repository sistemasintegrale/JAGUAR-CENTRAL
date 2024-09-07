using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.DataAccess;
using SGE.Entity;
using SGE.Entity.FacturaElectronica;
using SGE.WindowForms.Otros.Ventas;
using SIDE.COMUN.DTO.Modelos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class Frm10GuiaRemisionMP : DevExpress.XtraEditors.XtraForm
    {
        List<EGuiaRemision> lstFacturas = new List<EGuiaRemision>();
        public Frm10GuiaRemisionMP()
        {
            InitializeComponent();
        }

        private void cargar()
        {
            try
            {
                lstFacturas = new BVentas().listarGuiaRemision(Parametros.intEjercicio).Where(x => x.remic_itipo_guia == 2).ToList();
                grdGuiaRemision.DataSource = lstFacturas;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstFacturas.FindIndex(x => x.remic_icod_remision == intIcod);
            viewGuiaRemision.FocusedRowHandle = index;
            viewGuiaRemision.Focus();
        }

        private void nuevo()
        {
            frmManteGuiaRemision frm = new frmManteGuiaRemision();
            frm.MiEvento += new frmManteGuiaRemision.DelegadoMensaje(reload);
            frm.SetInsert();
            frm.TipodeGuia = 2;//Suministros
            frm.TDComercial = 685;
            frm.CargarControles();
            frm.AlmacenPrincipal();
            frm.Show();
            frm.dteFechaTranslado.EditValue = DateTime.Now;
            frm.lkpSituacion.EditValue = 218;
            frm.lkpMotivo.EditValue = 226;
            frm.lkpModalidadTraslado.EditValue = "01";

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void Frm04Factura_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void modificar()
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                EGuiaRemision _BEGuia = new EGuiaRemision();
                _BEGuia = new BVentas().listarGuiaRemisionxID(obe.remic_icod_remision);
                if (_BEGuia.tablc_iid_situacion_remision != 218)
                    throw new ArgumentException(String.Format("La Guía de Remisión no puede ser Modificado, su situación es ", _BEGuia.StrSitucion));
                if (obe.EstadoSunat == "ACEPTADO" || obe.EstadoSunat == "PENDIENTE VALIDAR" || obe.EstadoSunat == "DE BAJA")
                    throw new ArgumentException("La Guía ya fué enviada a SUNAT, no se puede modificar");
                frmManteGuiaRemision frm = new frmManteGuiaRemision();
                frm.MiEvento += new frmManteGuiaRemision.DelegadoMensaje(reload);
                frm.oBe = obe;
                frm.CargarControles();
                frm.SetModify();
                frm.btnCliente.Enabled = false;
                frm.btnAlmacenIngreso.Enabled = false;
                frm.Show();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private bool verificarDocVentaPlanilla(int intIcodDoc)
        {
            bool flagExiste = false;
            var intFlag = new BVentas().verificarDocVentaPlanilla(Parametros.intTipoDocFacturaVenta, intIcodDoc);
            if (intFlag > 0)
                flagExiste = true;

            return flagExiste;

        }




        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }


        private void txtNumero_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void filtrar()
        {
            grdGuiaRemision.DataSource = lstFacturas.Where(x => x.remic_vnumero_remision.Trim().Contains(txtNumero.Text.Trim()) &&
                x.remic_vnombre_destinatario.ToUpper().Trim().Contains(txtCliente.Text.Trim().ToUpper())).ToList();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;



            List<EUbicacion> lUbicacion = new BVentas().ListarUbicacion();
            int? tipo = null;
            int? Dios = null;
            int? padre = null;
            int? abuelo = null;
            int? bisabuelo = null;

            string pais;
            string Departamento = "";
            string Provincia = "";
            string Distrito = "";
            lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion).ToList().ForEach(oB =>
            {
                tipo = oB.tablc_iid_tipo_ubicacion;
                padre = oB.ubicc_icod_ubicacion_padre;
            });
            switch (tipo)
            {
                case 4:
                    Dios = obe.ubicc_icod_ubicacion;
                    break;
                case 3:
                    foreach (var _BBE in lUbicacion)
                    {
                        if (_BBE.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion)
                        {
                            Departamento = _BBE.ubicc_vnombre_ubicacion;
                        }
                    }

                    break;
                case 2:
                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == padre).ToList().ForEach(oB =>
                    {
                        abuelo = oB.ubicc_icod_ubicacion_padre;
                        Departamento = oB.ubicc_vnombre_ubicacion;
                    });
                    foreach (var _BBE in lUbicacion)
                    {
                        if (_BBE.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion)
                        {
                            Provincia = _BBE.ubicc_vnombre_ubicacion;
                        }
                    }
                    break;
                case 1:
                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == padre).ToList().ForEach(oB =>
                    {
                        abuelo = oB.ubicc_icod_ubicacion_padre;
                        Provincia = oB.ubicc_vnombre_ubicacion;
                    });

                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == abuelo).ToList().ForEach(oB =>
                    {
                        bisabuelo = oB.ubicc_icod_ubicacion_padre;
                        Departamento = oB.ubicc_vnombre_ubicacion;
                    });
                    foreach (var _BBE in lUbicacion)
                    {
                        if (_BBE.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion)
                        {
                            Distrito = _BBE.ubicc_vnombre_ubicacion;
                        }
                    }
                    break;

            }
            var lstdet = new BVentas().listarGuiaRemisionMPDet(obe.remic_icod_remision, Parametros.intEjercicio);


            List<EGuiaRemisionMPDet> mlistDetalle = new List<EGuiaRemisionMPDet>();
            //int cont = 1;
            //foreach (var _BE in lstdet)
            //{
            //    mlistDetalle.Add(_BE);

            //    string[] partes = partes = _BE.dremc_vobservaciones.Split('@');
            //    int cont_estapacios = 0;
            //    for (int i = 0; i < partes.Length; i++)
            //    {
            //        if (partes[i] == "")
            //        {
            //            cont_estapacios = cont_estapacios + 1;
            //        }
            //    }
            //    if (cont_estapacios != partes.Length)
            //    {
            //        for (int i = 0; i < partes.Length; i++)
            //        {
            //            if (i == 0)
            //            {
            //                EGuiaRemisionMPDet __be = new EGuiaRemisionMPDet();
            //                __be.strDesProducto = "" + partes[i];
            //                __be.OrdenItemImprimir = cont + 1;
            //                cont++;
            //                mlistDetalle.Add(__be);
            //            }
            //            else
            //            {
            //                if (partes[i] != "")
            //                {
            //                    EGuiaRemisionMPDet __be = new EGuiaRemisionMPDet();
            //                    __be.strDesProducto = "" + partes[i];
            //                    __be.OrdenItemImprimir = cont + 1;
            //                    cont++;
            //                    mlistDetalle.Add(__be);
            //                }
            //            }
            //        }
            //    }

            //}
            Boolean Ventas = false;
            Boolean Compras = false;
            Boolean Transformacion = false;
            Boolean Consignacion = false;
            Boolean Devolucion = false;
            Boolean Transferencia = false;
            Boolean TransladoEmision = false;
            Boolean Otros = false;

            if (obe.tablc_iid_motivo == 221)
            {
                Ventas = true;
            }
            else if (obe.tablc_iid_motivo == 222)
            {
                Compras = true;
            }
            else if (obe.tablc_iid_motivo == 223)
            {
                Transformacion = true;
            }
            else if (obe.tablc_iid_motivo == 224)
            {
                Consignacion = true;
            }
            else if (obe.tablc_iid_motivo == 225)
            {
                Devolucion = true;
            }
            else if (obe.tablc_iid_motivo == 226)
            {
                Transferencia = true;
            }
            else if (obe.tablc_iid_motivo == 227)
            {
                TransladoEmision = true;
            }
            else if (obe.tablc_iid_motivo == 228)
            {
                Otros = true;
            }

            //foreach (EGuiaRemisionMPDet conve in lstdet)
            //{
            //    int[] tallas = new int[10];
            //    int[] Cantidad = new int[10];
            //    tallas[0] = Convert.ToInt32(conve.dremc_talla1);
            //    tallas[1] = Convert.ToInt32(conve.dremc_talla2);
            //    tallas[2] = Convert.ToInt32(conve.dremc_talla3);
            //    tallas[3] = Convert.ToInt32(conve.dremc_talla4);
            //    tallas[4] = Convert.ToInt32(conve.dremc_talla5);
            //    tallas[5] = Convert.ToInt32(conve.dremc_talla6);
            //    tallas[6] = Convert.ToInt32(conve.dremc_talla7);
            //    tallas[7] = Convert.ToInt32(conve.dremc_talla8);
            //    tallas[8] = Convert.ToInt32(conve.dremc_talla9);
            //    tallas[9] = Convert.ToInt32(conve.dremc_talla10);
            //    Cantidad[0] = Convert.ToInt32(conve.dremc_cant_talla1);
            //    Cantidad[1] = Convert.ToInt32(conve.dremc_cant_talla2);
            //    Cantidad[2] = Convert.ToInt32(conve.dremc_cant_talla3);
            //    Cantidad[3] = Convert.ToInt32(conve.dremc_cant_talla4);
            //    Cantidad[4] = Convert.ToInt32(conve.dremc_cant_talla5);
            //    Cantidad[5] = Convert.ToInt32(conve.dremc_cant_talla6);
            //    Cantidad[6] = Convert.ToInt32(conve.dremc_cant_talla7);
            //    Cantidad[7] = Convert.ToInt32(conve.dremc_cant_talla8);
            //    Cantidad[8] = Convert.ToInt32(conve.dremc_cant_talla9);
            //    Cantidad[9] = Convert.ToInt32(conve.dremc_cant_talla10);
            //for (int i = 0; i < 10; i++)
            //{
            //    if (Cantidad[i] != 0)
            //    {
            //        conve.TallaAcumulada = conve.TallaAcumulada + " - " + tallas[i].ToString() + "/" + Cantidad[i].ToString();
            //    }
            //}
            //}

            rptGuiaRemisionMP rpt = new rptGuiaRemisionMP();
            rpt.cargar(obe, lstdet, "", "", "", Ventas, Compras, Transformacion, Consignacion, Devolucion, Transferencia, TransladoEmision, Otros);
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void eliminar()
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                EGuiaRemision _BEguiaConstatar = new BVentas().listarGuiaRemisionxID(obe.remic_icod_remision);
                if (_BEguiaConstatar.tablc_iid_situacion_remision != 218)
                {
                    throw new ArgumentException(String.Format("La Guia de Remisión no puede ser eliminada, su situación es {0}", _BEguiaConstatar.StrSitucion));
                    int xPosition = viewGuiaRemision.FocusedRowHandle;
                    cargar();
                    viewGuiaRemision.FocusedRowHandle = xPosition;
                }
                //if (verificarDocVentaPlanilla(obe.favc_icod_factura))
                //    throw new ArgumentException("La factura ha sido generada desde una Planilla de Venta, no puede ser eliminada");
                if (XtraMessageBox.Show("¿Esta seguro que desea ELIMINAR la Guía de Remisión?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    List<EGuiaRemisionDet> lstDetalle = new List<EGuiaRemisionDet>();
                    lstDetalle = new BVentas().listarGuiaRemisionDet(obe.remic_icod_remision, Parametros.intEjercicio);
                    new BVentas().eliminarGuiaRemision(obe, lstDetalle);
                    reload(obe.remic_icod_remision);

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int xPosition = viewGuiaRemision.FocusedRowHandle;
            cargar();
            viewGuiaRemision.FocusedRowHandle = xPosition;

        }

        private void viewGuiaRemision_DoubleClick(object sender, EventArgs e)
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {

                frmManteGuiaRemision frm = new frmManteGuiaRemision();
                frm.MiEvento += new frmManteGuiaRemision.DelegadoMensaje(reload);
                frm.oBe = obe;
                frm.Show();
                frm.CargarControles();

                frm.SetCancel();

                frm.btnGuardar.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void actualizarToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            List<EGuiaRemision> Mlist = new List<EGuiaRemision>();
            foreach (var _BEE in lstFacturas)
            {
                List<EGuiaRemisionDet> lstDetalle = new List<EGuiaRemisionDet>();
                lstDetalle = new BVentas().listarGuiaRemisionDet(_BEE.remic_icod_remision, Parametros.intEjercicio);
                if (lstDetalle.Sum(og => og.kardc_icod_correlativo) == 0)
                {
                    Mlist.Add(_BEE);
                }
            }
            foreach (var _CE in Mlist)
            {
                List<EGuiaRemisionDet> lstDetalle = new List<EGuiaRemisionDet>();
                List<EGuiaRemisionDet> lstDetalleEliminar = new List<EGuiaRemisionDet>();
                lstDetalle = new BVentas().listarGuiaRemisionDet(_CE.remic_icod_remision, Parametros.intEjercicio);
                foreach (var _cc in lstDetalle)
                {
                    _cc.intTipoOperacion = 2;
                }

                new BVentas().modificarGuiaRemision(_CE, lstDetalle, lstDetalleEliminar, null);
            }

        }

        private void anularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Anular();
        }
        private void Anular()
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                EGuiaRemision _BEguiaConstatar = new BVentas().listarGuiaRemisionxID(obe.remic_icod_remision);
                if (_BEguiaConstatar.tablc_iid_situacion_remision != 218)
                    throw new ArgumentException(String.Format("La Guia de Remisión no puede ser ANULADA, su situación es {0}", _BEguiaConstatar.StrSitucion));

                if (XtraMessageBox.Show("¿Esta seguro que desea ANULAR la Guía de Remisión?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
                if (obe.EstadoSunat == "PENDIENTE ENVIAR")
                    throw new ArgumentException("La Guía ya aún no fué enviada a SUNAT, no se puede ANULAR");
                List<EGuiaRemisionDet> lstDetalle = new List<EGuiaRemisionDet>();
                lstDetalle = new BVentas().listarGuiaRemisionDet(obe.remic_icod_remision, Parametros.intEjercicio);
                new BVentas().anularGuiaRemision(obe, lstDetalle);
                reload(obe.remic_icod_remision);
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void enableLoading(bool flag)
        {
            picLoading.Visible = flag;

            mnu.Enabled = !flag;
            if (flag)
                picLoading.Dock = DockStyle.Fill;
            else
                picLoading.Dock = DockStyle.None;
        }

        private async void enviarASunatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            enableLoading(true);
            var Obe = new VentasData().GetByIDGuiaRemisionElectronica(obe.remic_icod_remision);
            var mlisdet = new VentasData().GetByIDGuiaRemisionElectronicaDet(obe.remic_icod_remision);

            if (obe.remic_itipo_guia == 1)
                Obe.ubiPartida = new BCentral().ListarProdAlmacen().Where(x => x.id_almacen == obe.almac_icod_almacen).First().codigo_ubigeo;
            else
                Obe.ubiPartida = new BAlmacen().listarAlmacenes().Where(x => x.almac_icod_almacen == obe.almac_icod_almacen).First().almac_vubigeo;

            var model = new DocumentoElectronicoResponse();
            var objdocumento = new GuiaRemision();
            objdocumento.DireccionLlegada = new Direccion();
            objdocumento.DireccionPartida = new Direccion();
            objdocumento.Remitente = new Contribuyente();
            objdocumento.Destinatario = new Contribuyente();
            objdocumento.BienesATransportar = new List<DetalleGuia>();

            objdocumento = FacturaElectronicaDto.ModelDto(Obe, mlisdet);
            var response = await model.GuiaRemisionElectronica(objdocumento);

            var responseGRE = new EGuiaRemisionElectronicaResponse();
            responseGRE.remic_icod_remision = obe.remic_icod_remision;
            responseGRE.FechaEnvio = DateTime.Now;
            responseGRE.Exito = response.Exito;
            responseGRE.MensajeError = response.MensajeError;
            responseGRE.MensajeRespuesta = response.MensajeRespuesta;
            responseGRE.NumeroTicket = response.NroTicketCdr;
            responseGRE.LinkDescarga = response.Urldescarga;
            Obe.EstadoEnvio = response.CodigoRespuesta is null ? 4 : Convert.ToInt32(response.CodigoRespuesta);
            new VentasData().GuiaRemisionElectronicaResponseGuardar(responseGRE);

            if (response.Exito)
            {
                if (Convert.ToInt32(response.CodigoRespuesta) == 0)
                {
                    new VentasData().GuiaRemisionElectronicaCambiarEstado(responseGRE.remic_icod_remision, Constantes.PendienteValidar);
                    XtraMessageBox.Show($"La GRE {objdocumento.IdDocumento} se Envió Conrrectamente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    new VentasData().GuiaRemisionElectronicaCambiarEstado(responseGRE.remic_icod_remision, Constantes.Rechazado);
                    XtraMessageBox.Show($"{responseGRE.MensajeRespuesta}", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                new VentasData().GuiaRemisionElectronicaCambiarEstado(responseGRE.remic_icod_remision, Constantes.Rechazado);
                XtraMessageBox.Show($"{responseGRE.MensajeError}", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }



            backgroundWorker1.RunWorkerAsync();
            reload(responseGRE.remic_icod_remision);
        }

        private void imprimirElectronicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;


            var lstdet = new BVentas().listarGuiaRemisionMPDet(obe.remic_icod_remision, Parametros.intEjercicio);
            rptGRESuministros rpt = new rptGRESuministros();
            rpt.cargar(obe, lstdet);

        }

        private async void descargarCDRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;

            if (obe.EstadoSunat == "PENDIENTE ENVIAR")
                return;
            var parametro = new BAdministracionSistema().listarParametro().FirstOrDefault();
            var ObeE = new VentasData().GetByIDGuiaRemisionElectronica(obe.remic_icod_remision);
            string nombreArchivo = $"{parametro.Ruc}-{ObeE.TipoDocumento}-{ObeE.IdDocumento}.zip";


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = nombreArchivo;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                enableLoading(true);
                if (obe.EstadoSunat == "PENDIENTE DE VALIDAR")
                {
                    var model = new DocumentoElectronicoResponse();
                    var response = await model.DescargarCDR(obe.NumeroTicket);
                    var responseGRE = new EGuiaRemisionElectronicaResponse();
                    responseGRE.Exito = response.Exito;
                    responseGRE.MensajeError = response.MensajeError;
                    responseGRE.MensajeRespuesta = response.MensajeRespuesta;
                    responseGRE.NumeroTicket = response.NroTicketCdr;
                    responseGRE.LinkDescarga = response.Urldescarga;
                    responseGRE.CodigoRespuesta = Convert.ToInt32(response.CodigoRespuesta);

                    if (response.TramaZipCdr.Contains("{\"codRespuesta\":\"98\"}"))
                    {
                        XtraMessageBox.Show("El Documento Electrónico está en Proceso", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        backgroundWorker1.RunWorkerAsync();
                        reload(obe.remic_icod_remision);
                        return;
                    }

                    new VentasData().GuiaRemisionElectronicaResponseModificar(responseGRE);
                    if (Convert.ToInt32(response.CodigoRespuesta) == 0)
                    {
                        new VentasData().GuiaRemisionElectronicaCambiarEstado(obe.remic_icod_remision, Constantes.Aceptado);
                        string rutaArchivoEnviado = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{nombreArchivo}");
                        if (File.Exists(rutaArchivoEnviado))
                            File.Delete(rutaArchivoEnviado);
                        File.WriteAllBytes($"{rutaArchivoEnviado}", Convert.FromBase64String(response.TramaZipCdr));
                        WebDavTest.Put($"{parametro.DirecciónXML}{Rutas.CarpataGRE}", $"{nombreArchivo}", rutaArchivoEnviado);
                        XtraMessageBox.Show(response.MensajeRespuesta, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        WebDavTest.Descargar($"{parametro.DirecciónXML}{Rutas.CarpataGRE}{nombreArchivo}", saveFileDialog.FileName);
                        Process.Start(saveFileDialog.FileName.Replace($"\\{nombreArchivo}", ""));
                    }
                    else
                    {
                        new VentasData().GuiaRemisionElectronicaCambiarEstado(obe.remic_icod_remision, Constantes.Rechazado);
                        XtraMessageBox.Show(response.MensajeRespuesta, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    WebDavTest.Descargar($"{parametro.DirecciónXML}{Rutas.CarpataGRE}{nombreArchivo}", saveFileDialog.FileName);
                    Process.Start(saveFileDialog.FileName.Replace($"\\{nombreArchivo}", ""));
                }
            }

            backgroundWorker1.RunWorkerAsync();
            reload(obe.remic_icod_remision);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            enableLoading(false);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            cargar();
        }

        private void viewGuiaRemision_RowStyle_1(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string strSituacion = View.GetRowCellDisplayText(e.RowHandle, View.Columns["StrSitucion"]);
                if (strSituacion == "ANULADO")
                {
                    e.Appearance.BackColor = Color.LightSalmon;

                }
            }
        }

        private void mnu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            enviarASunatToolStripMenuItem.Visible = obe.EstadoSunat == "PENDIENTE ENVIAR" ? true : obe.EstadoSunat == "RECHAZADO" ? true : false;
            descargarCDRToolStripMenuItem.Visible = obe.EstadoSunat != "PENDIENTE ENVIAR" ? true : false;
        }
    }
}