using SGE.BusinessLogic;
using SGE.Entity;
using SGE.Entity.FacturaElectronica;
using SIDE.COMUN.DTO.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGE.WindowForms
{
    public static class FacturaElectronicaDto
    {
        public static GuiaRemision ModelDto(EGuiaRemisionElectronica obe, List<EGuiaRemisionElectronicaDet> mlisdet)
        {
            GuiaRemision objdocumento = new GuiaRemision();
            objdocumento.CodigoPuerto = obe.CodigoPuerto;
            objdocumento.NumeroContenedor = obe.NumeroContenedor;
            objdocumento.DocumentoRelacionado = new DocumentoRelacionado();
            objdocumento.DocumentoRelacionado.TipoDocumento = "01";
            objdocumento.DocumentoRelacionado.NroDocumento = "F001-0000001";
            objdocumento.DocumentoRelacionado.TipoDocqModifica = "-";
            objdocumento.DocumentoRelacionado.NroDocqModifica = "-";
            objdocumento.DireccionLlegada = new Direccion();
            objdocumento.DireccionLlegada.DireccionCompleta = obe.dirLlegada;
            objdocumento.DireccionLlegada.Ubigeo = obe.ubiLlegada;
            objdocumento.FechaEmision = obe.FechaEmision;

            objdocumento.DireccionPartida = new Direccion();
            objdocumento.DireccionPartida.DireccionCompleta = obe.dirPartida;
            objdocumento.DireccionPartida.Ubigeo = obe.ubiPartida;
            objdocumento.NroDocumentoConductor = obe.NroDocumentoConductor;
            objdocumento.NroPlacaVehiculo = obe.NroPlacaVehiculo;
            objdocumento.RazonSocialTransportista = obe.RazonSocialTransportista;
            objdocumento.FechaInicioTraslado = obe.FechaInicioTraslado;
            objdocumento.ModalidadTraslado = obe.RucTransportista == obe.numDocRemitente ? "02" : "01";
            objdocumento.RucTransportista = objdocumento.ModalidadTraslado == "02" ? "" : obe.RucTransportista;
            objdocumento.NroPallets = obe.NroPallets;
            objdocumento.PesoBrutoTotal = obe.PesoBrutoTotal;
            objdocumento.DescripcionMotivo = obe.DescripcionMotivo;
            objdocumento.CodigoMotivoTraslado = obe.CodigoMotivoTraslado;





            objdocumento.GuiaBaja = new DocumentoRelacionado();
            objdocumento.GuiaBaja.NroDocqModifica = "";
            objdocumento.GuiaBaja.NroDocumento = "";
            objdocumento.GuiaBaja.TipoDocqModifica = "";
            objdocumento.GuiaBaja.TipoDocumento = "-";

            objdocumento.Tercero = new Contribuyente();
            objdocumento.Tercero.NroDocumento = "";
            objdocumento.Tercero.TipoDocumento = "";
            objdocumento.Tercero.NombreComercial = "";
            objdocumento.Tercero.NombreLegal = "";

            objdocumento.Destinatario = new Contribuyente();
            objdocumento.Destinatario.NroDocumento = obe.numDocDestinatario.Trim();
            objdocumento.Destinatario.TipoDocumento = obe.tipDocDestinatario.Trim();
            objdocumento.Destinatario.NombreComercial = obe.rznSocialDestinatario.Trim();
            objdocumento.Destinatario.NombreLegal = obe.rznSocialDestinatario.Trim();

            objdocumento.Remitente = new Contribuyente();
            objdocumento.Remitente.NroDocumento = obe.numDocRemitente;
            objdocumento.Remitente.TipoDocumento = obe.tipDocRemitente;
            objdocumento.Remitente.NombreComercial = obe.rznSocialRemitente;
            objdocumento.Remitente.NombreLegal = obe.rznSocialRemitente;

            objdocumento.Glosa = obe.Glosa;
            objdocumento.TipoDocumento = obe.TipoDocumento;
            objdocumento.FechaEmision = obe.FechaEmision;
            objdocumento.IdDocumento = obe.IdDocumento;
            objdocumento.Transbordo = obe.Transbordo;
            objdocumento.ShipmentId = obe.ShipmentId;


            objdocumento.BienesATransportar = new List<DetalleGuia>();
            foreach (var item in mlisdet)
            {
                objdocumento.BienesATransportar.Add(new DetalleGuia()
                {
                    Correlativo = item.Correlativo,
                    CodigoItem = item.UnidadMedida != "ZZ" ? item.CodigoItem : "",
                    Descripcion = item.Descripcion.TrimEnd().Replace("  ", " "),
                    UnidadMedida = item.UnidadMedida,
                    Cantidad = item.Cantidad,
                    LineaReferencia = item.LineaReferencia
                });
            }

            return objdocumento;
        }
        public static DocumentoElectronico ModelDto(EFacturaVentaElectronica obe, List<EFacturaVentaDetalleElectronico> mlisdet)
        {
            DocumentoElectronico objdocumento = new DocumentoElectronico();
            objdocumento.TipoOperacion = "0101";
            objdocumento.IdDocumento = obe.idDocumento;


            objdocumento.FechaEmision = Convert.ToDateTime(obe.fechaEmision).ToString("yyyy-MM-dd");
            objdocumento.HoraEmision = DateTime.Now.ToString("hh:mm:ss");
            objdocumento.FechaVencimiento = Convert.ToDateTime(obe.fechaVencimiento).ToString("yyyy-MM-dd");


            objdocumento.TipoDocumento = obe.tipoDocumento;
            objdocumento.Moneda = obe.moneda;
            objdocumento.CantidadItems = obe.cantidadItems.ToString();

            objdocumento.Emisor = new Contribuyente();
            objdocumento.Emisor.NombreComercial = obe.nombreComercialEmisor;
            objdocumento.Emisor.NombreLegal = obe.nombreLegalEmisor;
            objdocumento.Emisor.TipoDocumento = obe.tipoDocumentoEmisor;
            objdocumento.Emisor.NroDocumento = obe.nroDocumentoEmisior;

            objdocumento.Emisor.Ubigeo = "0000";
            //objdocumento.CodLocalEmisor = obe.CodLocalEmisor;

            objdocumento.Receptor = new Contribuyente();
            objdocumento.Receptor.NroDocumento = obe.nroDocumentoReceptor;
            objdocumento.Receptor.TipoDocumento = obe.tipoDocumentoReceptor;
            objdocumento.Receptor.NombreLegal = obe.nombreLegalReceptor;

            objdocumento.CodMotivoDescuento = obe.CodMotivoDescuento.ToString();
            objdocumento.PorcDescuento = obe.PorcDescuento;
            objdocumento.MontoDescuentoGlobal = obe.MontoDescuentoGlobal;
            objdocumento.BaseMontoDescuento = obe.BaseMontoDescuento;
            objdocumento.MontoTotalImpuesto = Math.Round(obe.MontoTotalImpuesto, 2);
            objdocumento.MontoGravadosIGV = Math.Round(obe.MontoGravadasIGV, 2);
            objdocumento.TotalIgv = Math.Round(obe.totalIgv, 2);

            //objdocumento.CodigoTributo = obe.CodigoTributo.ToString();
            objdocumento.TotalIvap = 0;
            objdocumento.MontoGravadasIVAP = 0;
            objdocumento.MontoExonerado = obe.MontoExonerado;
            objdocumento.MontoInafecto = obe.MontoInafecto;
            objdocumento.MontoGratuitoImpuesto = obe.MontoGratuitoImpuesto;
            objdocumento.MontoBaseGratuito = obe.MontoBaseGratuito;

            objdocumento.MontoGravadosISC = obe.MontoGravadosISC;
            objdocumento.TotalIsc = obe.totalIsc;
            objdocumento.MontoGravadosOtros = obe.MontoGravadosOtros;
            objdocumento.TotalOtrosTributos = obe.totalOtrosTributos;
            objdocumento.TotalValorVenta = Math.Round(obe.TotalValorVenta, 2);
            objdocumento.TotalPrecioVenta = obe.TotalPrecioVenta;
            objdocumento.MontoDescuento = obe.MontoDescuento;
            objdocumento.MontoTotalCargo = obe.MontoTotalCargo;
            objdocumento.MontoTotalAnticipo = obe.MontoTotalAnticipo;
            objdocumento.ImporteTotalVenta = obe.ImporteTotalVenta;

            //Nuevas Variables

            //Variable Forma de Pago

            if (objdocumento.TipoDocumento == "01")
            {
                objdocumento.FormaPago = obe.FormaPago; //Contado o Credito


                //Variable Lista de Cuotas
                if (obe.FormaPago == "Credito")
                {
                    objdocumento.MontoTotalPago = Convert.ToDecimal(obe.cuotas.Sum(x=>x.fcc_nmonto));// //Monto del Pago
                    objdocumento.Cuotas = new List<Cuotas>();
                    foreach (var item in obe.cuotas)
                    {
                        objdocumento.Cuotas.Add(new Cuotas
                        {
                            NroCuota = "Cuota"+ item.fcc_inro_cuota.Value.ToString("d3"),
                            MontoCuota = Convert.ToDecimal(item.fcc_nmonto),
                            FechaPago = Convert.ToDateTime(item.fcc_sfecha_pago).ToString("yyyy-MM-dd")
                        });
                    }
                }
            }


            //Items
            objdocumento.Items = new List<DetalleDocumento>();
            foreach (var item in mlisdet)
            {
                objdocumento.Items.Add(new DetalleDocumento
                {
                    NumeroOrdenItem = item.NumeroOrdenItem,
                    Cantidad = item.cantidad,
                    UnidadMedida = item.unidadMedida.TrimStart().TrimEnd(),
                    ValorVentaItem = Convert.ToDecimal(item.ValorVentaItem),
                    CodMotivoDescuentoItem = item.CodMotivoDescuentoItem.ToString(),
                    FactorDescuentoItem = item.FactorDescuentoItem,
                    DescuentoItem = item.DescuentoItem,
                    BaseDescuentotem = item.BaseDescuentotem,
                    CodMotivoCargoItem = item.CodMotivoCargoItem.ToString(),
                    FactorCargoItem = item.FactorCargoItem,
                    MontoCargoItem = item.MontoCargoItem,
                    BaseCargoItem = item.BaseCargoItem,
                    MontoTotalImpuestosItem = item.MontoTotalImpuestosItem,
                    MontoImpuestoIgvItem = item.MontoImpuestoIgvItem,
                    MontoAfectoImpuestoIgvItem = item.MontoAfectoImpuestoIgv,
                    PorcentajeIGVItem = item.PorcentajeIGVItem,
                    MontoInafectoItem = item.MontoInafectoItem,
                    MontoImpuestoISCItem = item.MontoImpuestoISCItem,
                    MontoAfectoImpuestoISCItem = item.MontoAfectoImpuestoIsc,
                    PorcentajeISCtem = item.PorcentajeISCtem,
                    MontoImpuestoIVAPtem = item.MontoImpuestoIVAPtem,
                    MontoAfectoImpuestoIVAPItem = item.MontoAfectoImpuestoIVAPItem,
                    PorcentajeIVAPItem = item.PorcentajeIVAPItem,
                    Descripcion = item.descripcion.TrimStart().TrimEnd() + " - " + item.ObservacionesItem,
                    CodigoItem = item.codigoItem,
                    TipoImpuesto = "10",
                    ObservacionesItem = item.ObservacionesItem,
                    ValorUnitarioItem = item.ValorUnitarioItem,
                    PrecioVentaUnitarioItem = item.PrecioVentaUnitarioItem
                });
            }
            return objdocumento;
        }

        public static DocumentoElectronico ModelDtoNC(EFacturaVentaElectronica obe, List<EFacturaVentaDetalleElectronico> mlisdet)
        {
            DocumentoElectronico objdocumento = new DocumentoElectronico();
            objdocumento.IdDocumento = obe.idDocumento.Trim();
            objdocumento.FechaEmision = Convert.ToDateTime(obe.fechaEmision).ToString("yyy-MM-dd");
            objdocumento.HoraEmision = DateTime.Now.ToString("hh:mm:ss");
            objdocumento.FechaVencimiento = Convert.ToDateTime(obe.fechaVencimiento).ToString("yyy-MM-dd");
            objdocumento.TipoDocumento = obe.tipoDocumento;
            objdocumento.Moneda = obe.moneda;
            objdocumento.CantidadItems = obe.cantidadItems.ToString();

            objdocumento.Emisor = new Contribuyente();
            objdocumento.Emisor.NombreComercial = obe.nombreComercialEmisor;
            objdocumento.Emisor.NombreLegal = obe.nombreLegalEmisor;
            objdocumento.Emisor.TipoDocumento = obe.tipoDocumentoEmisor;
            objdocumento.Emisor.NroDocumento = obe.nroDocumentoEmisior;
            //adicinar
            objdocumento.Emisor.CodLocalEmisor = "0";

            objdocumento.Emisor.Ubigeo = "0000";

            objdocumento.Receptor = new Contribuyente();
            objdocumento.Receptor.NroDocumento = obe.nroDocumentoReceptor;
            objdocumento.Receptor.TipoDocumento = obe.tipoDocumentoReceptor;
            objdocumento.Receptor.NombreLegal = obe.nombreLegalReceptor;



            objdocumento.CodMotivoDescuento = obe.CodMotivoDescuento.ToString();
            objdocumento.PorcDescuento = obe.PorcDescuento;
            objdocumento.MontoDescuentoGlobal = obe.MontoDescuentoGlobal;
            objdocumento.BaseMontoDescuento = obe.BaseMontoDescuento;
            objdocumento.MontoTotalImpuesto = obe.MontoTotalImpuesto;
            objdocumento.MontoGravadosIGV = obe.MontoGravadasIGV;
            //objdocumento.CodigoTributo = obe.CodigoTributo.ToString();
            objdocumento.MontoExonerado = obe.MontoExonerado;
            objdocumento.MontoInafecto = obe.MontoInafecto;
            objdocumento.MontoGratuitoImpuesto = obe.MontoGratuitoImpuesto;
            objdocumento.MontoBaseGratuito = obe.MontoBaseGratuito;

            objdocumento.TotalIgv = obe.totalIgv;
            objdocumento.MontoGravadosISC = obe.MontoGravadosISC;
            objdocumento.TotalIsc = obe.totalIsc;
            objdocumento.MontoGravadosOtros = obe.MontoGravadosOtros;
            objdocumento.TotalOtrosTributos = obe.totalOtrosTributos;
            objdocumento.TotalValorVenta = obe.TotalValorVenta;
            objdocumento.TotalPrecioVenta = obe.TotalPrecioVenta;
            objdocumento.MontoDescuento = obe.MontoDescuento;
            objdocumento.MontoTotalCargo = obe.MontoTotalCargo;
            objdocumento.MontoTotalAnticipo = obe.MontoTotalAnticipo;
            objdocumento.ImporteTotalVenta = obe.ImporteTotalVenta;



            //Items
            objdocumento.Items = new List<DetalleDocumento>();
            foreach (var item in mlisdet)
            {
                objdocumento.Items.Add(new DetalleDocumento
                {
                    //IdItems = item.IdItems,
                    NumeroOrdenItem = item.NumeroOrdenItem,
                    Cantidad = Convert.ToDecimal(item.cantidad),
                    UnidadMedida = item.unidadMedida.Trim(),
                    ValorVentaItem = item.ValorVentaItem,
                    CodMotivoDescuentoItem = item.CodMotivoDescuentoItem.ToString(),
                    FactorDescuentoItem = (item.FactorDescuentoItem),
                    DescuentoItem = item.DescuentoItem,
                    BaseDescuentotem = (item.BaseDescuentotem),
                    CodMotivoCargoItem = item.CodMotivoCargoItem.ToString(),
                    FactorCargoItem = (item.FactorCargoItem),
                    MontoCargoItem = (item.MontoCargoItem),
                    BaseCargoItem = (item.BaseCargoItem),
                    MontoTotalImpuestosItem = (item.MontoTotalImpuestosItem),
                    MontoImpuestoIgvItem = item.MontoImpuestoIgvItem,
                    MontoAfectoImpuestoIgvItem = item.MontoAfectoImpuestoIgv,
                    PorcentajeIGVItem = (item.PorcentajeIGVItem),
                    MontoImpuestoISCItem = (item.MontoImpuestoISCItem),
                    MontoAfectoImpuestoISCItem = (item.MontoAfectoImpuestoIsc),
                    PorcentajeISCtem = (item.PorcentajeISCtem),
                    MontoImpuestoIVAPtem = (item.MontoImpuestoIVAPtem),
                    MontoAfectoImpuestoIVAPItem = (item.MontoAfectoImpuestoIVAPItem),
                    PorcentajeIVAPItem = (item.PorcentajeIVAPItem),
                    Descripcion = item.descripcion.Trim(),
                    CodigoItem = item.codigoItem.Trim(),
                    //ObservacionesItem = item.ObservacionesItem,
                    ValorUnitarioItem = item.ValorUnitarioItem,
                    //CodConceptoAsignarItem = item.CodConceptoAsignarItem.ToString().Trim(),
                    //descripcionAdicionalItem = item.DescripcionAdicionalItem
                    PrecioVentaUnitarioItem = ((item.MontoAfectoImpuestoIgv + item.MontoImpuestoIgvItem) / item.cantidad)
                });
            }
            return objdocumento;
        }
        public static ComunicacionBaja DocumentoElectronicoBaja(ESunatDocumentosBaja Obe)
        {
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();
            EParametro lstParametro = new BAdministracionSistema().listarParametro().FirstOrDefault();
            ComunicacionBaja obj = new ComunicacionBaja();


            obj.Bajas = new List<DocumentoBaja>();


            obj.Bajas.Add(new DocumentoBaja
            {
                Correlativo = Obe.Correlativo,
                MotivoBaja = Obe.MotivoBaja,
                Id = Obe.Id,
                TipoDocumento = Obe.TipoDocumento,
                Serie = Obe.Serie,

            });

            obj.IdDocumento = string.Format("RA-{0}-{1}", DateTime.Now.ToString("yyyyMMdd"), 1);
            obj.FechaEmision = Convert.ToDateTime(Obe.FechaEmision).ToString("yyyy/MM/dd");
            obj.FechaReferencia = DateTime.Now.ToString("yyyy/MM/dd");

            obj.Emisor = new Contribuyente();
            obj.Emisor.NroDocumento = lstParametro.Ruc.Trim();
            obj.Emisor.TipoDocumento = "6";
            obj.Emisor.NombreLegal = lstParametro.pm_nombre_empresa.Trim();
            obj.Emisor.NombreComercial = lstParametro.pm_nombre_empresa.Trim();
            obj.Emisor.Direccion = lstParametro.pm_direccion_empresa.Trim();

            return obj;
        }
        public static ResumenDiarioNuevo DocumentoElectronicoResumen(List<ESunatResumenDet> objBaja)
        {
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();
            EParametro lstParametro = new BAdministracionSistema().listarParametro().FirstOrDefault();

            ResumenDiarioNuevo obj_ = new ResumenDiarioNuevo();

            //---Resumen detalle

            obj_.Resumenes = new List<GrupoResumenNuevo>();
            int id = 1;
            foreach (var item in objBaja)
            {
                obj_.Resumenes.Add(new GrupoResumenNuevo
                {
                    IdDocumento = item.IdDocumento,
                    TipoDocumentoReceptor = item.TipoDocumentoReceptor,  //6 -> DNI
                    NroDocumentoReceptor = item.NroDocumentoReceptor,
                    CodigoEstadoItem = item.CodigoEstadoItem,
                    DocumentoRelacionado = item.DocumentoRelacionado,
                    TipoDocumentoRelacionado = item.TipoDocumentoRelacionado,
                    CorrelativoInicio = item.CorrelativoInicio,
                    CorrelativoFin = item.CorrelativoFin,
                    Moneda = item.Moneda,
                    TotalVenta = item.TotalVenta,
                    TotalDescuentos = item.TotalDescuentos,
                    TotalIgv = item.TotalIgv,
                    TotalIsc = item.TotalIsc,
                    TotalOtrosImpuestos = item.TotalOtrosImpuestos,
                    Gravadas = item.Gravadas,
                    Exoneradas = item.Exoneradas,
                    Inafectas = item.Inafectas,
                    Exportacion = item.Exportacion,
                    Gratuitas = item.Gratuitas,
                    Id = id,
                    TipoDocumento = item.TipoDocumento, //03 ->Boleta , 07->Nota Credito , 08 ->Nota debito 
                    Serie = item.Serie
                });
                id++;
            }

            obj_.IdDocumento = string.Format("RC-{0}-{1}", DateTime.Now.ToString("yyyyMMdd"), 1);
            obj_.FechaEmision = DateTime.Now.ToString("yyyy/MM/dd");
            obj_.FechaReferencia = DateTime.Now.ToString("yyyy/MM/dd");

            obj_.Emisor = new Contribuyente();
            obj_.Emisor.NroDocumento = lstParametro.Ruc.Trim();
            obj_.Emisor.TipoDocumento = "6";
            obj_.Emisor.NombreLegal = lstParametro.pm_nombre_empresa.Trim();
            obj_.Emisor.NombreComercial = lstParametro.pm_nombre_empresa.Trim();
            obj_.Emisor.Ubigeo = "13131";
            obj_.Emisor.Direccion = lstParametro.pm_direccion_empresa.Trim();
            obj_.Emisor.Urbanizacion = "-";
            obj_.Emisor.Departamento = "Lima";
            obj_.Emisor.Provincia = "Lima";
            obj_.Emisor.Distrito = "Surquillo";

            return obj_;
        }

        public static ResumenDiarioNuevo DocumentoElectronicoResumenDocumentos(ESunatResumenDocumentosCab objBaja, List<ESunatResumenDocumentosDet> objBajaDet)
        {
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();
            EParametro lstParametro = new BAdministracionSistema().listarParametro().FirstOrDefault();

            ResumenDiarioNuevo obj_ = new ResumenDiarioNuevo();

            //---Resumen detalle

            obj_.Resumenes = new List<GrupoResumenNuevo>();
            int id = 1;
            foreach (var item in objBajaDet)
            {
                obj_.Resumenes.Add(new GrupoResumenNuevo
                {
                    Id = item.Id,
                    TipoDocumento = item.TipoDocumento.Trim(),
                    IdDocumento = item.IdDocumento.Trim(),
                    TipoDocumentoReceptor = item.TipoDocumentoReceptor.Trim(),
                    NroDocumentoReceptor = item.NroDocumentoReceptor.Trim(),
                    CodigoEstadoItem = item.CodigoEstadoItem,
                    DocumentoRelacionado = item.DocumentoRelacionado.Trim(),
                    TipoDocumentoRelacionado = item.TipoDocumentoRelacionado.Trim(),
                    Moneda = item.Moneda.Trim(),
                    TotalVenta = item.TotalVenta,
                    TotalDescuentos = item.TotalDescuentos,
                    TotalIgv = item.TotalIgv,
                    TotalIsc = item.TotalIsc,
                    TotalIvap = item.TotalIvap,
                    TotalOtrosImpuestos = item.TotalOtrosImpuestos,
                    Gravadas = item.Gravadas,
                    Exoneradas = item.Exoneradas,
                    Inafectas = item.Inafectas,
                    Exportacion = item.Exportacion,
                    Gratuitas = item.Gratuitas,
                    Serie = ""
                });
                id++;
            }

            obj_.IdDocumento = objBaja.IdDocumento.Trim();
            //obj_.FechaEmision = objBaja.FechaEmision;
            //obj_.FechaReferencia = objBaja.FechaGeneracion;

            obj_.FechaEmision = Convert.ToDateTime(objBaja.FechaEmision).ToString("yyyy-MM-dd");
            obj_.FechaReferencia = Convert.ToDateTime(objBaja.FechaGeneracion).ToString("yyyy-MM-dd");

            obj_.Emisor = new Contribuyente();
            obj_.Emisor.NroDocumento = objBaja.NroDocumento.Trim();
            obj_.Emisor.TipoDocumento = objBaja.TipoDocumento.Trim();
            obj_.Emisor.NombreLegal = objBaja.NombreLegal;
            obj_.Emisor.NombreComercial = objBaja.NombreComercial;
            obj_.Emisor.Ubigeo = objBaja.Ubigeo;
            obj_.Emisor.Direccion = objBaja.Direccion;
            obj_.Emisor.Urbanizacion = objBaja.Urbanizacion;
            obj_.Emisor.Departamento = objBaja.Departamento;
            obj_.Emisor.Provincia = objBaja.Provincia;
            obj_.Emisor.Distrito = objBaja.Distrito;

            return obj_;
        }

    }
}
