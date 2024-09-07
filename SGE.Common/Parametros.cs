namespace SGE.Common
{
    public class Parametros
    {
        public static string strPorcIGV = "";
        public static string strPorcRenta4taCat = "";
        public static int intEjercicio = 2013;

        //---------- TIPO DE ANALITICA --------------------------------------
        public static int intTipoAnaliticaBancos = 1;
        public static int intTipoAnaliticaClientes = 2;
        public static int intTipoAnaliticaProveedores = 5;
        /*ID´s de Tipos de Tabla--------------------------------------------------------------*/
        public static int intTipoTablaEstado = 1;
        public static int intTipoTablaSituacionVoucher = 2;
        public static int intTipoTablaOrigenVoucher = 3;
        public static int intTipoTablaMeses = 4;
        public static int intTipoTablaTipoMoneda = 5;
        public static int intTipoTablaTipoCuentaBanco = 10;
        public static int intTipoTablaSituacionDocumento = 21;
        public static int intTipoTablaSituacionGuiaRemision = 50;
        public static int intTipoTablaMotivoGuiaRemision = 51;
        public static int intTipoTablaFormaPago = 20;
        public static int intTipoTablaTipoPersona = 22;
        public static int intTipoTablaTipoAnalitica = 24;
        public static int intTipoTablaTipoCuentaContable = 25;
        public static int intTipoTablaTipoProducto = 26;
        public static int intTipoTablaTipoRotacion = 27;
        public static int intTipoTablaEstadoAnioEjercicio = 28;
        public static int intTipoTablaTipoInventario = 44;
        public static int intTipoTablaSituacionInventario = 45;
        public static int intTipoTablaCondicion = 47;
        public static int intTipoTablaUbicacion = 48;
        public static int intTipoventa = 60;
        public static int intTipoPedido = 61;
        public static int intTipoEstado = 59;


        /*------------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------*/

        public static int intInventarioGenerado = 190;
        public static int intInventarioRegistrado = 191;
        public static int intInventarioActualizado = 192;


        /*------------------------------------------------------------------------------------*/
        /*Situación de Vouchers Contables-----------------------------------------------------*/
        public static int intSitVcoCuadrado = 1;
        public static int intSitVcoNoCuadrado = 2;
        public static int intSitVcoAnulado = 3;
        public static int intSitVcoSinDetalle = 4;
        /*------------------------------------------------------------------------------------*/
        /*Origenes de Vouchers Contables------------------------------------------------------*/
        public static int intOriVcoManual = 1;
        public static int intOriVcoOtroSistema = 2;
        public static int intOriVcoPorRedondeo = 3;
        public static int intOriVcoPorDiferenciaCambio = 4;
        public static int intOriVcoImporPlanilla = 5;
        /*------------------------------------------------------------------------------------*/
        /*Tipo Operaciones--------------------------------------------------------------------*/
        public static int intOperacionNuevo = 1;
        public static int intOperacionModificar = 2;
        public static int intOperacionEliminar = 3;
        /*------------------------------------------------------------------------------------*/
        /*Tipo Monedas------------------------------------------------------------------------*/
        public static int intTipoMonedaSoles = 3;
        public static int intTipoMonedaDolares = 4;
        public static int intTipoMonedaHistorico = 5;
        /*------------------------------------------------------------------------------------*/
        /*Módulos-----------------------------------------------------------------------------*/
        public static int intModuloAdministracionSitema = 1;
        public static int intModuloAlmacen = 2;
        public static int intModuloCompras = 3;
        public static int intModuloCtasPorCobrar = 4;
        public static int intModuloCtasPorPagar = 5;
        public static int intModuloTesoreria = 6;
        public static int intModuloVentas = 7;
        public static int intModuloContabilidad = 8;
        public static int intModuloCaja = 14;
        /*-----tipos de movimientos kardex-------------------------------------------------------*/
        public static int intKardexIn = 1;
        public static int intKardexOut = 0;
        public static int intMovIngTransformacion = 644;
        /*------------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------*/
        //---------- MOTIVOS DE MOVIMIENTOS DE BANCOS -----------------------
        public static int intMotivoApertura = 104;
        public static int intMotivoSaldoInicial = 105;
        public static int intMotivoCuentasPorPagar = 106;
        public static int intMotivoCuentasPorCobrar = 107;
        public static int intMotivoAdelantosProveedores = 108;
        public static int intMotivoAdelantosClientes = 109;
        public static int intMotivoVarios = 110;
        public static int intMotivoPagoAdelantadoProveedores = 111;
        public static int intMotivoPagoAdelantadoClientes = 112;
        public static int intMotivoTransferenciaCuentas = 182;
        //-------------------------------------------------------------------
        public static string intTipoMovimientoCargo = "0";
        public static string intTipoMovimientoAbono = "1";
        //---------- TIPO DE DOCUMENTO --------------------------------------
        public static int intTipoDocAdelantoProveedor = 1;
        public static int intTipoDocAperturaKardex = 3;
        public static int intTipoDocLiquidacionCompra = 35;
        public static int intTipoDocNotaCreditoCliente = 36;
        public static int intTipoDocNotaCreditoProveedor = 86;
        public static int intTipoDocPresupuestoNacional = 87;
        public static int intTipoDocPresupuestoImportacion = 46;
        public static int intTipoDocComprobanteRetencion = 50;
        public static int intTipoDocReciboPorHonorarios = 51;
        public static int intTipoDocAdelantoCliente = 83;
        public static int intTipoDocLetraRechazada = 33;
        public static int intTipoDocFacturaCompra = 24;
        public static int intTipoDocDeclaracionUnicaAduana = 106;
        public static int intTipoDocDeclaracionImportacionCourier = 22;
        public static int intTipoDocLiquidacionCobranzaAduana = 31;
        public static int intTipoDocReporteProduccion = 47;
        public static int intTipoDocNotaIngresoContable = 38;
        public static int intTipoDocNotaSalidaContable = 39;
        public static int intTipoDocNotaIngresoAlmacen = 27;
        public static int intTipoDocNotaSalidaAlmacen = 29;
        public static int intTipoDocAperturaCtaBanco = 90;
        public static int intTipoDocLetraProveedores = 34;
        //public static int intTipoDocNotaDebito = 37;
        public static int intTipoDocNotaDebito = 110;
        public static int intTipoDocTicket = 54;
        public static int intTipoDocLiquidacionCobranza = 32;
        public static int intTipoDocSaldoInicialCtaBanco = 93;
        public static int intTipoDocTicketCintaMaqReg = 54;
        public static int intTipoDocPlanillaVenta = 100;
        public static int intTipoDocFacturaVenta = 26;
        public static int intTipoDocBoletaVenta = 9;
        public static int intTipoDocLetraCliente = 85;
        public static int intTipoDocLetraProveedor = 34;

        public static int intTipoDocumentoOC = 13;
        //-------------------------------------------------------------------
        //---------- SITUACION ADELANTO PROVEEDOR -----------------------
        public static int intSitProveedorGenerado = 8;
        public static int intSitProveedorPagadoParcial = 9;
        public static int intSitProveedorCancelado = 10;
        public static int intSitProveedorAnulado = 11;
        //-------------------------------------------------------------------
        //---------- TIPO DE DOCUMENTO CLASE CUENTA -------------------------
        public static int intClaseTipoDocAdelantoProveedor = 27;
        public static int intClaseTipoDocAdelantoCliente = 30;
        public static int intClaseTipoDocLetraRechazada = 36;
        public static int intClaseTipoDocLetraPorCobrar = 37;
        public static int intClaseTipoDocLetraProveedor = 52;
        //-------------------------------------------------------------------
        //---------- SITUACION DOCUMENTO X PAGAR ----------------------------
        public static int intSitDocGenerado = 8;
        public static int intSitDocPagadoParcial = 9;
        public static int intSitDocCancelado = 10;
        //-------------------------------------------------------------------
        //---------- SITUACION ADELANTO CLIENTE -----------------------------
        public static int intSitClienteGenerado = 8;
        public static int intSitClientePagadoParcial = 9;
        public static int intSitClienteCancelado = 10;
        public static int intSitClienteAnulado = 11;
        //-------------------------------------------------------------------
        //---------- SITUACION DOCUMENTO X COBRAR ---------------------------
        public static int intSitDocCobrarGenerado = 8;
        public static int intSitDocCobrarPagadoParcial = 9;
        public static int intSitDocCobrarCancelado = 10;
        public static int intSitDocCobrarAnulado = 11;
        //-------------------------------------------------------------------
        //---------- SITUACION LIBRO BANCOS ---------------------------------
        public static int intSitLibroBancosRegistrado = 114;
        public static int intSitLibroBancosAnulado = 115;
        //-------------------------------------------------------------------
        public static double dblPorRetencion = 0.00;
        //---------- MONEDA -------------------------------------------------
        public static int intSoles = 3;
        public static int intDolares = 4;
        //-------------------------------------------------------------------
        //---------- TIPO MOV ALAMCEN -------------------------------------------------
        public static int intIngresoAlmacen = 34;
        public static int intSalidaAlmacen = 35;
        //-------------------------------------------------------------------
        //----------PLANILLA TIPOS DE MOVIMIENTOS--------------------
        public static int intPlnFacturacion = 1;
        public static int intPlnPago = 2;
        public static int intPlnAnticipo = 3;
        //---------- MOTIVOS DE MOVIMIENTOS DE KARDEX -----------------------
        public static int intMotivoKrdSaldoInicial = 100;
        public static int intMotivoKrdVentas = 101;
        public static int intMotivoKrdCompras = 97;
        public static int intMotivoKrdTransferenciaIn = 98;
        public static int intMotivoKrdTransferenciaOut = 102;

        //---------- SITUACION PLANILLA DE VENTA -----------------------
        public static int intSitPlnPendiente = 1;
        public static int intSitPlnEnProceso = 2;
        public static int intSitPlnCerrado = 3;
        public static int intSitPlnFacturado = 4;
        public static int intSitPlnAnulado = 5;


        //---------- TIPOS DE PAGO DE DOC DE VENTA -----------------------
        public static int intTipoPgoEfectivo = 1;
        public static int intTipoPgoTarjetaCredito = 2;
        public static int intTipoPgoNotaCredito = 3;
        public static int intTipoPgoCheque = 4;
        public static int intTipoPgoTransfBancaria = 5;
        public static int intTipoPgoCredito = 6;
        public static int intTipoPgoAnticipo = 7;

        //---------- TIPOS DE PRODUCTO -----------------------
        public static int intTipoPrdServicio = 3;


        //---------- TIPOS MOV. LIQUIDACION CAJA -----------------------
        public static string strTipoLiqCajaCContable = "CCONTABLE";
        public static string strTipoLiqCajaPagoProvision = "PGOPROVIS";
        public static string strTipoLiqCajaGenProvision = "GENPROVIS";
        //---------- TIPOS ACTUALIZACIÓN DE COSTOS -----------------------
        public static int intTipoActualizacionCompras = 1;
        public static int intTipoActualizacionDevoluciones = 2;
        public static int intTipoActualizacionTransformaciones = 3;
        public static int intTipoActualizacionVentas = 4;
        public static int intTipoActualizacionImportacion = 5;

        //tipo tarjeta
        public static int tablc_tipo_tarjeta = 40;
        #region
        public static int Tabla_Tabla_registro_Visa = 1;
        public static int Tabla_Tabla_registro_Mastercard = 2;
        public static int Tabla_Tabla_registro_Diners = 3;
        #endregion
        //Baco tarjetas
        public static int bcoc_icod_banco_tarjeta = 17;
        #region
        public static int Cuenta_tarjeta_visa = 103003;
        public static int Cuenta_tarjeta_mastercard = 103003;
        public static int Cuenta_tarjeta_dinners = 103003;
        public static int Cuenta_tarjeta_American_express = 103003;

        #endregion
        //Banco tarjetas
        public static int bcoc_icod_banco_Caja_Efectivo = 16;
        #region
        public static int Cuenta_id_caja_efectivo_la_molina_MN = 101001;
        public static int Cuenta_id_caja_efectivo_la_molina_ME = 101002;
        public static int Cuenta_id_caja_pucusana = 101003;
        #endregion

        //subdiarios
        public static int intSubDiarioDiferenciaCambio = 13;


        //------ TABLAS COMUNES ------------------------------------------------------
        public static int intTablaTipoPresupuestoNacional = 53;
        //---------- SITUACION PRESUPUESTO NACIONAL -------------------------
        public static int intSitPrepNacGenerado = 245;
        public static int intSitPrepNacRecibido = 246;
        public static int intSitPrepNacCerrado = 247;
        public static int intSitPrepNacAnulado = 248;
        //-------------------------------------------------------------------
        public static int intSitReporteProduccionActualizado = 647;
        public static int intSitReporteProduccionGenerado = 646;










        public static int intTipoTablaFormaPagoA = 20;
        #region
        public static int intTipoPagoContado = 116;
        public static int intTipoPagoCredito = 117;
        #endregion

        public static int intTipoTablaSituacionDeOC = 57;
        #region
        public static int intSituacOCRegistrado = 254;
        public static int intSituacOCAnulado = 255;
        public static int intSituacOCAutorizado = 256;
        public static int intSituacOCConOrden = 257;
        #endregion

        //------ TABLAS PVT ------------------------------------------------------
        public static int intTipo = 1;
        public static int intCategoria = 2;
        public static int intLinea = 3;
        public static int intMarca = 11;
        public static int intColor = 8;
        public static int intTalla = 10;
        public static string strPorcRetencion;


        public static int intIcodModulo = 0;



        /*Tipo de Documentos------------------------------------------------------------------*/
        public static int intTipoDocAperturaBanco = 12;
        public static int intTipoDocSaldoInicialBanco = 13;

        public static int intTipoDocBoletaCompra = 84;
        public static int intTipoDocGuiaRemision = 28;

        public static int intTipoDocRetencion = 50;

        public static int intTipoDocLiquidacionCaja = 77;

        public static int intTipoDocPercepcionCompra = 98;

        public static int intTipoDocTicketFactura = 95;
        public static int intTipoDocTicketBoleta = 96;
        public static int intTipoDocNotaCompra = 97;
        public static int intTipoDocGarantiaProveedor = 113;
        public static int intTipoDocGarantiaClientes = 112;
        //-------------------------------------------------------------------

        public static int intClaseTipoDocPercepcionCompra = 63;
        public static int intClaseTipoDocRetencion = 56;


        public static int intClaseTipoDocLetraCliente = 37;
        public static int intClaseTipoDocFacturaVentaServicios = 7;
        public static int intClaseTipoDocBoletaVentaServicios = 10;
        public static int intClaseTipoDocNotaCredClienteDevolucion = 21;

        public static int intClaseTipoDocTicketFactura = 67;
        public static int intClaseTipoDocTicketBoleta = 68;
        public static int intClaseTipoDocNotaCompra = 64;

        public static int intClaseTipoDocFacturaCompra = 32;
        public static int intClaseTipoDocBoletaCompra = 31;

        //-------------------------------------------------------------------

        //-------------------------------------------------------------------
        //-----------ORIGEN EN DXP-----------------
        public static string origenManual = "2";
        public static string origenAlmacenCompra = "9";
        public static string origenComprasFac = "8";
        public static string origenComprasPercepcion = "4";
        public static string origenAlmacenNCP = "5";
        public static string origenLiquidacionCaja = "6";

        //-----------ORIGEN EN DXC-----------------
        public static string origenLetraPorCobrar = "3";
        public static string origenLetraPorPagar = "6";

        //---------- MOTIVOS DE MOVIMIENTOS DE KARDEX -----------------------
        public static int intMotivoKrdDevolucionProdIn = 99;
        public static int intMotivoKrdSaldoInicialIn = 101;
        public static int intMotivoKrdVentasOut = 101;
        public static int intMotivoKrdComprasIn = 97;

        public static int intMotivoKrdAjusteInventIn = 193;
        public static int intMotivoKrdAjusteInventOut = 194;
        //---------- SITUACION PLANILLA DE VENTA -----------------------
        //---------- CLIENTE -----------------------
        public static int intClientePortador = 1528;
        public static int intTipoDocTransferenciaAlmacen = 88;
    }
}
