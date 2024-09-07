using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Libros_Oficiales
{
    public partial class frmRegistroCompras : DevExpress.XtraEditors.XtraForm
    {
        private List<ERegistroCompras> ListaGeneral = new List<ERegistroCompras>();
        private List<ERegistroCompras> Lista = new List<ERegistroCompras>();
        private List<ERegistroCompras> ListaND = new List<ERegistroCompras>();

        public frmRegistroCompras()
        {
            InitializeComponent();
        }

        private void frmRegistroCompras_Load(object sender, EventArgs e)
        {
            var lstMeses = new BGeneral().listarTablaRegistro(4);
            BSControls.LoaderLook(lkpMes, lstMeses.Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", false);
            lkpMes.EditValue = DateTime.Now.Month;
        }

        private void lkpMes_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpMes.EditValue != null)
                Carga();
        }

        private void Carga()
        {

            ListaGeneral = new BContabilidad().ListarRegistroDeComprasGeneral(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue)).Where(ob => ob.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoProveedor).ToList();
            ListaGeneral = ListaGeneral.OrderBy(ord => ord.doxpc_viid_correlativo).ToList();
            grd.DataSource = ListaGeneral;

            Lista = new BContabilidad().ListarRegistroDeCompras(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue)).Where(ob => ob.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoProveedor).ToList();
            Lista = Lista.OrderBy(ord => ord.doxpc_viid_correlativo).ToList();

            ListaND = new BContabilidad().ListarRegistroDeComprasNoDomic(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue)).Where(ob => ob.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoProveedor).ToList();
            ListaND = ListaND.OrderBy(ord => ord.doxpc_viid_correlativo).ToList();
        }

        private void exportarAExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdRuta.ShowDialog(this) == DialogResult.OK)
            {
                #region Visible1
                //FALSE
                gc1.Visible = false;
                gc2.Visible = false;
                gc3.Visible = false;
                gc4.Visible = false;
                gc5.Visible = false;
                gc6.Visible = false;
                gc7.Visible = false;
                gc8.Visible = false;
                gc9.Visible = false;
                gc10.Visible = false;
                gc11.Visible = false;
                gc12.Visible = false;
                gc13.Visible = false;
                gc14.Visible = false;
                gc15.Visible = false;
                gc16.Visible = false;
                gc17.Visible = false;
                gc18.Visible = false;
                gc19.Visible = false;
                //gc20.Visible = false;
                gc21.Visible = false;
                gc22.Visible = false;
                gc23.Visible = false;
                gc24.Visible = false;
                gc25.Visible = false;
                gc26.Visible = false;
                gc27.Visible = false;
                //TRUE
                gc28.Visible = true;
                gc29.Visible = true;
                gc30.Visible = true;
                gc31.Visible = true;
                gc32.Visible = true;
                gc33.Visible = true;
                gc34.Visible = true;
                gc35.Visible = true;
                gc36.Visible = true;
                gc37.Visible = true;
                gc38.Visible = true;
                gc39.Visible = true;
                gc40.Visible = true;
                gc41.Visible = true;
                gc42.Visible = true;
                gc43.Visible = true;
                //gc44.Visible = true;
                //gc45.Visible = true;
                //gc46.Visible = true;
                gc47.Visible = true;
                gc48.Visible = true;
                gc49.Visible = true;
                gc50.Visible = true;
                gc51.Visible = true;
                gc52.Visible = true;
                gc53.Visible = true;
                #endregion

                string fileName = sfdRuta.FileName;
                if (!fileName.Contains(".xlsx"))
                {
                    grd.ExportToXlsx(fileName + ".xlsx");
                    System.Diagnostics.Process.Start(fileName + ".xlsx");
                }
                else
                {
                    grd.ExportToXlsx(fileName);
                    System.Diagnostics.Process.Start(fileName);
                }

                #region Visible2
                //FALSE
                gc28.Visible = false;
                gc29.Visible = false;
                gc30.Visible = false;
                gc31.Visible = false;
                gc32.Visible = false;
                gc33.Visible = false;
                gc34.Visible = false;
                gc35.Visible = false;
                gc36.Visible = false;
                gc37.Visible = false;
                gc38.Visible = false;
                gc39.Visible = false;
                gc40.Visible = false;
                gc41.Visible = false;
                gc42.Visible = false;
                gc43.Visible = false;
                //gc44.Visible = false;
                //gc45.Visible = false;
                //gc46.Visible = false;
                gc47.Visible = false;
                gc48.Visible = false;
                gc49.Visible = false;
                gc50.Visible = false;
                gc51.Visible = false;
                gc52.Visible = false;
                gc53.Visible = false;
                //TRUE
                gc1.Visible = true;
                gc2.Visible = true;
                gc3.Visible = true;
                gc4.Visible = true;
                gc5.Visible = true;
                gc6.Visible = true;
                gc7.Visible = true;
                //gc8.Visible = true;
                gc9.Visible = true;
                gc10.Visible = true;
                gc11.Visible = true;
                gc12.Visible = true;
                gc13.Visible = true;
                gc14.Visible = true;
                gc15.Visible = true;
                gc16.Visible = true;
                gc17.Visible = true;
                gc18.Visible = true;
                gc19.Visible = true;
                //gc20.Visible = true;
                gc21.Visible = true;
                gc22.Visible = true;
                gc23.Visible = true;
                gc24.Visible = true;
                gc25.Visible = true;
                gc26.Visible = true;
                gc27.Visible = true;

                gc1.VisibleIndex = 0;
                gc2.VisibleIndex = 1;
                gc3.VisibleIndex = 2;
                gc4.VisibleIndex = 3;
                gc5.VisibleIndex = 4;
                gc6.VisibleIndex = 5;
                gc7.VisibleIndex = 6;
                //gc8.VisibleIndex = 7;
                gc9.VisibleIndex = 8;
                gc10.VisibleIndex = 9;
                gc11.VisibleIndex = 10;
                gc12.VisibleIndex = 11;
                gc13.VisibleIndex = 12;
                gc14.VisibleIndex = 13;
                gc15.VisibleIndex = 14;
                gc16.VisibleIndex = 15;
                gc17.VisibleIndex = 16;
                gc18.VisibleIndex = 17;
                gc19.VisibleIndex = 18;
                //gc20.VisibleIndex = 19;
                gc21.VisibleIndex = 20;
                gc22.VisibleIndex = 21;
                gc23.VisibleIndex = 22;
                gc24.VisibleIndex = 23;
                gc25.VisibleIndex = 24;
                gc26.VisibleIndex = 25;
                gc27.VisibleIndex = 26;
                #endregion
            }
        }

        private void porTipoDeDocumentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                rptRegistroCompras reporte = new rptRegistroCompras();
                reporte.Cargar(Lista, lkpMes.Text);
            }
        }

        private void imprimirListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                rptRegistroComprasSinQuiebre reporte = new rptRegistroComprasSinQuiebre();
                reporte.Cargar(Lista, lkpMes.Text);
            }
        }

        private void exportarATXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string ConSinInfo, Mes;
                if (Lista.Count > 0)
                {
                    ConSinInfo = "1";
                }
                else
                {
                    ConSinInfo = "0";
                }
                if (lkpMes.EditValue.ToString().Trim().Length == 1)
                {
                    Mes = "0" + lkpMes.EditValue.ToString();
                }
                else
                {
                    Mes = lkpMes.EditValue.ToString();
                }
                string Nombre = "LE" + Valores.strRUC + Parametros.intEjercicio.ToString() + Mes + "00" + "080100" + "00" + "1" + ConSinInfo + "1" + "1" + ".txt";
                sfdTXT.FileName = Nombre;
                if (sfdTXT.ShowDialog(this) == DialogResult.OK)
                {
                    string fileName = sfdTXT.FileName;
                    if (!fileName.Contains(".txt"))
                    {
                        ExportarATXT(fileName + ".txt");
                        System.Diagnostics.Process.Start(fileName + ".txt");
                    }
                    else
                    {
                        ExportarATXT(fileName);
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
        private void ExportarATXT(String ruta)
        {
            StreamWriter sw = new StreamWriter(ruta);
            string error = string.Empty;
            int cont = 0;
            string columna = string.Empty;
            try
            {
                int totFilas = Lista.Count;
                foreach (ERegistroCompras item in Lista)
                {
                    cont++;
                    error = item.tdocc_vabreviatura_tipo_doc + " " + item.doxpc_vnumero_doc;
                    columna = "1";
                    sw.Write(Parametros.intEjercicio.ToString() + String.Format("{0:00}", lkpMes.EditValue) + "00|"); // 1
                    columna = "2";
                    sw.Write(item.CUO + "|"); // 2
                    //sw.Write(cont + "|"); // 2
                    columna = "3";
                    string Letra = "";
                    if (Convert.ToInt32(lkpMes.EditValue) == 0)
                    {
                        Letra = "A";
                    }
                    else if (Convert.ToInt32(lkpMes.EditValue) == 13)
                    {
                        Letra = "C";
                    }
                    else
                    {
                        Letra = "M";
                    }
                    sw.Write(Letra + String.Format("{0:00000}", cont) + "|"); // A: Aptertura M: Mes C:Cierre // 3
                    columna = "4";
                    sw.Write(Convert.ToDateTime(item.doxpc_sfecha_doc).ToString("dd/MM/yyyy") + "|"); // 4

                    columna = "5";
                    if (Convert.ToDateTime(item.doxpc_sfecha_vencimiento_doc).Month <= Convert.ToInt32(lkpMes.EditValue) &&
                        Convert.ToDateTime(item.doxpc_sfecha_vencimiento_doc).Year == Parametros.intEjercicio)
                    {
                        sw.Write(Convert.ToDateTime(item.doxpc_sfecha_vencimiento_doc).ToString("dd/MM/yyyy") + "|"); // 5
                    }
                    else
                    {
                        sw.Write(Convert.ToDateTime(item.doxpc_sfecha_doc).ToString("dd/MM/yyyy") + "|"); // 5
                    }
                    columna = "6";
                    sw.Write(item.tdocc_vcodigo_tipo_doc_sunat + "|"); // 6

                    columna = "7";
                    //solo para enero
                    if (item.doxpc_numdoc_tipo == 1)
                    {

                        if (item.tdocc_icod_tipo_doc == 5)//Boleto Aerio
                        {
                            sw.Write("1" + "|"); // 7 
                        }
                        else
                        {
                            sw.Write(item.doxpc_vnumero_serio + "|"); // 7
                        }

                    }
                    else
                    {

                        if (item.tdocc_vcodigo_tipo_doc_sunat == "50")
                        {
                            sw.Write(item.doxpc_vnumero_doc.Substring(0, 3) + "|");
                        }
                        else
                        {
                            sw.Write("" + "|"); // 7
                        }
                    }

                    columna = "8";
                    if (item.tdocc_vcodigo_tipo_doc_sunat == "50")
                    {
                        sw.Write(item.doxpc_vnumero_doc.Substring(3, 4) + "|");
                    }
                    else
                    {
                        if (item.tdocc_vcodigo_tipo_doc_sunat == "54")
                        {
                            sw.Write(item.doxpc_codigo_aduana + "|");
                        }
                        else
                        {
                            sw.Write("|"); // 8 NO SE APLICA
                        }

                    }




                    //solo para enero
                    columna = "9";
                    if (item.doxpc_numdoc_tipo == 1)
                    {
                        if (item.tdocc_icod_tipo_doc == 5)//Boleto Aerio
                        {

                            if (item.doxpc_vnumero_serio == "" || item.doxpc_vnumero_serio == null)
                            {
                                sw.Write(item.doxpc_vnumero_serio + item.doxpc_vnumero_correlativo + "|");
                            }
                            else
                            {
                                sw.Write(item.doxpc_vnumero_serio.Substring(1) + item.doxpc_vnumero_correlativo + "|");
                            }


                        }
                        else
                        {
                            sw.Write(item.doxpc_vnumero_correlativo + "|"); // 9
                        }
                    }
                    else
                    {
                        if (item.tdocc_icod_tipo_doc == 119)
                        {
                            sw.Write(item.doxpc_vnumero_doc + "|");
                        }
                        else
                            sw.Write(item.doxpc_vnumero_doc.Substring(9) + "|");
                    }

                    columna = "10";
                    sw.Write("" + "|"); // 10
                    columna = "11";
                    sw.Write(item.tip_doc_proveedor + "|"); // 11
                    columna = "12";
                    sw.Write(item.num_doc_proveedor.TrimEnd().TrimStart() + "|"); // 12
                    columna = "13";
                    sw.Write(item.proc_vnombrecompleto + "|"); // 13                                       
                    // MONTOS
                    columna = "14";
                    if (item.tdocc_vcodigo_tipo_doc_sunat == "50")
                    {
                        sw.Write(String.Format("{0:0.00}", item.MontoDUA) + "|"); // 14
                    }
                    else
                    {
                        sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_destino_gravado) + "|"); // 14
                    }
                    columna = "15";
                    sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_imp_destino_gravado) + "|"); // 15
                    columna = "16";
                    sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_destino_mixto) + "|"); // 16
                    columna = "17";
                    sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_imp_destino_mixto) + "|"); // 17
                    columna = "18";
                    sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_destino_nogravado) + "|"); // 18
                    columna = "19";
                    sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_imp_destino_nogravado) + "|"); // 19
                    columna = "20";
                    sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_nogravado) + "|"); // 20
                    columna = "21";
                    sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_isc) + "|"); // 21
                    columna = "22";
                    sw.Write(string.Format("{0:0.00}", (item.doxpc_otros_impuestos)) + "|"); // 22
                    columna = "23";
                    sw.Write(string.Format("{0:0.00}", ("" + "")) + "|"); // 22
                    columna = "24";
                    if (item.tdocc_vcodigo_tipo_doc_sunat == "50")
                    {
                        sw.Write(String.Format("{0:0.00}", item.MontoTotalDUA) + "|");
                    }
                    else
                    {
                        sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_total_documento) + "|"); // 23
                    }


                    columna = "25";
                    if (item.tablc_iid_tipo_moneda == 3)
                    {
                        sw.Write("PEN" + "|"); // 24
                    }
                    else
                    {
                        sw.Write("USD" + "|"); // 24
                    }
                    columna = "26";
                    sw.Write(String.Format("{0:0.000}", Math.Round(Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio), 3)) + "|"); // 24
                    columna = "27";
                    sw.Write(((item.doxpc_sfecha_emision_referencia != null) ? Convert.ToDateTime(item.doxpc_sfecha_emision_referencia).ToString("dd/MM/yyyy") : "") + "|"); // 25
                    columna = "28";
                    sw.Write(item.nc_dxc_tipodoc + "|"); // 26
                    columna = "29";

                    sw.Write(((!string.IsNullOrEmpty(item.doxpc_num_serie_referencia)) ? string.Format("{0:0000}", ((item.doxpc_num_serie_referencia))) : "") + "|"); // 27
                    columna = "30";
                    sw.Write("" + "|"); // 32
                    columna = "31";
                    sw.Write(((!String.IsNullOrEmpty(item.doxpc_num_comprobante_referencia)) ? item.doxpc_num_comprobante_referencia : "") + "|"); // 29
                    columna = "32";
                    sw.Write(((item.doxpc_sfec_deposito_detraccion != null) ? Convert.ToDateTime(item.doxpc_sfec_deposito_detraccion).ToString("dd/MM/yyyy") : String.Empty) + "|"); // 31
                    columna = "33";
                    sw.Write(item.doxpc_vnro_deposito_detraccion + "|"); // 32
                    columna = "34";
                    sw.Write("" + "|"); // 33

                    columna = "35";
                    sw.Write(item.ViddAdquisicion + "|"); // 33//tipo adquisicion
                    columna = "36";
                    sw.Write("" + "|"); // 33
                    columna = "37";
                    sw.Write("" + "|"); // 33
                    columna = "38";
                    sw.Write("" + "|"); // 33
                    columna = "39";
                    sw.Write("" + "|"); // 33
                    columna = "40";
                    sw.Write("" + "|"); // 33
                    columna = "41";
                    sw.Write("1" + "|"); // 33
                    columna = "42";
                    if ((Convert.ToInt32(lkpMes.EditValue) == Convert.ToDateTime(item.doxpc_sfecha_doc).Month && Parametros.intEjercicio == Convert.ToDateTime(item.doxpc_sfecha_doc).Year && item.tdocc_icod_tipo_doc != 84))
                    {

                        sw.Write("1" + "|"); // 34
                    }
                    else if (item.tdocc_icod_tipo_doc == 84)
                    {
                        sw.Write("0" + "|"); // 34
                    }
                    else
                    {
                        sw.Write("6" + "|"); // 34
                    }

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

        private void tXTNoDomiciliadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (sfdTXT.ShowDialog(this) == DialogResult.OK)
                {
                    string fileName = sfdTXT.FileName;
                    if (!fileName.Contains(".txt"))
                    {
                        ExportarATXTNoDomi(fileName + ".txt");
                        System.Diagnostics.Process.Start(fileName + ".txt");
                    }
                    else
                    {
                        ExportarATXTNoDomi(fileName);
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
        private void ExportarATXTNoDomi(String ruta)
        {
            StreamWriter sw = new StreamWriter(ruta);
            string error = string.Empty;
            int cont = 0;
            string columna = string.Empty;
            try
            {
                int totFilas = ListaND.Count;
                foreach (ERegistroCompras item in ListaND)
                {
                    cont++;
                    error = item.tdocc_vabreviatura_tipo_doc + " " + item.doxpc_vnumero_doc;
                    columna = "1";// Periodo
                    sw.Write(Parametros.intEjercicio.ToString() + String.Format("{0:00}", lkpMes.EditValue) + "00|");
                    columna = "2";// CUO     
                    sw.Write(item.CUO + "|");
                    columna = "3";// A: Aptertura M: Mes C:Cierre
                    string Letra = "";
                    if (Convert.ToInt32(lkpMes.EditValue) == 0)
                    {
                        Letra = "A";
                    }
                    else if (Convert.ToInt32(lkpMes.EditValue) == 13)
                    {
                        Letra = "C";
                    }
                    else
                    {
                        Letra = "M";
                    }
                    sw.Write(Letra + String.Format("{0:00000}", cont) + "|");
                    columna = "4";// F.Emision Comprbante de Pago o Documento
                    sw.Write(Convert.ToDateTime(item.doxpc_sfecha_doc).ToString("dd/MM/yyyy") + "|");
                    columna = "5"; // Tipo Comprobante de Pago
                    sw.Write(item.tdocc_vcodigo_tipo_doc_sunat + "|");

                    columna = "6";// NO SE APLICA
                    sw.Write("|");

                    columna = "7";// Numero de Comprobante de Pago
                    if (item.doxpc_numdoc_tipo == 1)
                    {
                        if (item.tdocc_icod_tipo_doc == 5)
                        {
                            sw.Write(item.doxpc_vnumero_serio.Substring(1) + item.doxpc_vnumero_correlativo + "|"); // 9
                        }
                        else
                        {
                            sw.Write(item.doxpc_vnumero_correlativo + "|"); // 9
                        }
                    }
                    else
                    {
                        sw.Write(item.doxpc_vnumero_doc + "|"); // 9
                    }
                    columna = "8";// Valor de las Adquisiciones
                    sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_total_documento) + "|");
                    columna = "9";// Otros Conceptos Adicionales
                    sw.Write("0.00" + "|");
                    columna = "10";// Total Importe Adquisiciones
                    sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_total_documento) + "|");
                    columna = "11";// Tipo Comprobante
                    sw.Write("50" + "|");
                    columna = "12";// Serie De Comprobante de Pago o Doc
                    sw.Write(item.doxpc_codigo_aduana + "|");
                    columna = "13";// Anio Emisio de la DUA 
                    sw.Write(item.doxpc_anio + "|");
                    columna = "14";// Numero Comprobante de Pago o Doc/ Num de Orden de Formulario
                    sw.Write(item.doxpc_numero_declaracion + "|");
                    columna = "15";// Monto Retencion IGV
                    sw.Write("0.00" + "|");
                    columna = "16";// Tipo Moneda
                    if (item.tablc_iid_tipo_moneda == 3)
                    {
                        sw.Write("PEN" + "|");
                    }
                    else
                    {
                        sw.Write("USD" + "|");
                    }
                    columna = "17";//Tipo de Cambio
                    sw.Write(String.Format("{0:0.000}", Math.Round(Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio), 3)) + "|");
                    columna = "18";// Pais de la Residencia del Sujeto no Domiciliado
                    sw.Write(item.proc_pais_nodomic + "|");
                    columna = "19";// Apellidos, Nombres, Denominacion o razon social del sujeto NoDomi
                    sw.Write(item.proc_vnombrecompleto + "|");
                    columna = "20";// Domicilio En el Extranjero del Sujeto NoDomi
                    sw.Write(item.proc_vdireccion + "|");
                    columna = "21";// Numero de Identificacion del Sujeto NoDomi
                    sw.Write(item.proc_vdni + "|");
                    columna = "22";// Pagos
                    sw.Write("|");
                    columna = "23";// Apellidos, Nombres, Denominacion o razon social del Beneficiario Efectivo d elos Pagos
                    sw.Write("|");
                    columna = "24";// Pais de la Residencia del Beneficiario Efectivo de los Pagos
                    sw.Write("|");
                    columna = "25";// Vinculo entre Contribuyente y el Residente en el Extranjero
                    sw.Write("|");
                    columna = "26";// Renta Bruta
                    sw.Write("0.00" + "|");
                    columna = "27";// Deducion/ Costo de Enajeracion de bienes de capital
                    sw.Write("0.00" + "|");
                    columna = "28";// Renta Neta
                    sw.Write("0.00" + "|");
                    columna = "29";// Tasa de Retencion
                    sw.Write("0.00" + "|");
                    columna = "30";// Impuesto Retenido
                    sw.Write("0.00" + "|");
                    columna = "31";// Convenios para evitar Doble Imposicion
                    sw.Write("00" + "|");
                    columna = "32";// Exoneracion Aplicada
                    sw.Write("|");
                    columna = "33";// Tipo Renta
                    sw.Write("00" + "|");
                    columna = "34";// Modalidad de Servicio Prestado por el NoDomic
                    sw.Write("|");
                    columna = "35";// Aplicacion de penultimo parrafo del Art 76° de la ley Impuesto a la Rebta
                    sw.Write("|");
                    columna = "36";// Estado que Identifica la Oportunidad de la Anotacion 
                    sw.Write("0" + "|");
                    columna = "37";// Campos de Libre Utilizacion 
                    sw.Write("|");
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

        private void mnu_Opening(object sender, CancelEventArgs e)
        {

        }

        private void sfdRuta_FileOk(object sender, CancelEventArgs e)
        {

        }
        //private void ExportarATXT(String ruta)
        //{
        //    StreamWriter sw = new StreamWriter(ruta);
        //    string error = string.Empty;
        //    int cont = 0;
        //    string columna = string.Empty;
        //    try
        //    {
        //        int totFilas = Lista.Count;
        //        foreach (ERegistroCompras item in Lista)
        //        {
        //            cont++;
        //            error = item.tdocc_vabreviatura_tipo_doc + " " + item.doxpc_vnumero_doc;
        //            columna = "1";
        //            sw.Write(Parametros.intEjercicio.ToString() + String.Format("{0:00}", lkpMes.EditValue) + "00|"); // 1
        //            columna = "2";
        //            sw.Write(item.doxpc_icod_correlativo + "|"); // 2
        //            columna = "3";
        //            sw.Write("M" + String.Format("{0:00000}", cont) + "|"); // A: Aptertura M: Mes C:Cierre // 3
        //            columna = "4";
        //            sw.Write(Convert.ToDateTime(item.doxpc_sfecha_doc).ToString("dd/MM/yyyy") + "|"); // 4

        //            columna = "5";
        //            if (Convert.ToDateTime(item.doxpc_sfecha_vencimiento_doc).Month <= Convert.ToInt32(lkpMes.EditValue) &&
        //                Convert.ToDateTime(item.doxpc_sfecha_vencimiento_doc).Year == Parametros.intEjercicio)
        //            {
        //                sw.Write(Convert.ToDateTime(item.doxpc_sfecha_vencimiento_doc).ToString("dd/MM/yyyy") + "|"); // 5
        //            }
        //            else
        //            {
        //                sw.Write(Convert.ToDateTime(item.doxpc_sfecha_doc).ToString("dd/MM/yyyy") + "|"); // 5
        //            }

        //            columna = "6";
        //            sw.Write(item.tdocc_vcodigo_tipo_doc_sunat + "|"); // 6

        //            columna = "7";
        //            //solo para enero
        //            sw.Write("0" + item.doxpc_vnumero_doc.Substring(0, 3) + "|"); // 7
        //            //para los demas meses
        //            ////if (item.doxpc_numdoc_tipo == 1)
        //            ////{
        //            ////    sw.Write("0" + item.doxpc_vnumero_doc.Substring(0, 3) + "|"); // 7
        //            ////}
        //            ////else
        //            ////{
        //            ////    sw.Write("|");
        //            ////}

        //            columna = "8";
        //            sw.Write("|"); // 8 NO SE APLICA

        //            //solo para enero
        //            columna = "9";


        //            string cad2 = item.doxpc_vnumero_doc.Replace("-", "");


        //            sw.Write(cad2.Remove(0, 3) + "|"); // 9

        //            //para los demas meses
        //            ////columna = "9";
        //            ////if (item.doxpc_numdoc_tipo == 1)
        //            ////{
        //            ////    sw.Write(item.doxpc_vnumero_doc.Substring(4) + "|"); // 9
        //            ////}
        //            ////else
        //            ////{
        //            ////    sw.Write(item.doxpc_vnumero_doc + "|"); // 9
        //            ////}

        //            columna = "10";
        //            sw.Write("0" + "|"); // 10
        //            columna = "11";
        //            sw.Write(item.tip_doc_proveedor + "|"); // 11
        //            columna = "12";
        //            sw.Write(item.num_doc_proveedor.TrimEnd().TrimStart() + "|"); // 12
        //            columna = "13";
        //            sw.Write(item.proc_vnombrecompleto + "|"); // 13                                       
        //            // MONTOS
        //            columna = "14";
        //            sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_destino_gravado) + "|"); // 14
        //            columna = "15";
        //            sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_imp_destino_gravado) + "|"); // 15
        //            columna = "16";
        //            sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_destino_mixto) + "|"); // 16
        //            columna = "17";
        //            sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_imp_destino_mixto) + "|"); // 17
        //            columna = "18";
        //            sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_destino_nogravado) + "|"); // 18
        //            columna = "19";
        //            sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_imp_destino_nogravado) + "|"); // 19
        //            columna = "20";
        //            sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_nogravado) + "|"); // 20
        //            columna = "21";
        //            sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_isc) + "|"); // 21
        //            columna = "22";
        //            sw.Write("0.00" + "|"); // 22
        //            columna = "23";
        //            sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_total_documento) + "|"); // 23
        //            columna = "24";
        //            sw.Write(String.Format("{0:0.000}", Math.Round(Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio), 3)) + "|"); // 24
        //            columna = "25";
        //            sw.Write(((item.doxpc_sfecha_emision_referencia != null) ? Convert.ToDateTime(item.doxpc_sfecha_emision_referencia).ToString("dd/MM/yyyy") : "") + "|"); // 25
        //            columna = "26";
        //            sw.Write(item.nc_dxc_tipodoc + "|"); // 26
        //            columna = "27";
        //            sw.Write(((!string.IsNullOrEmpty(item.doxpc_num_serie_referencia)) ? item.doxpc_num_serie_referencia : "") + "|"); // 27
        //            columna = "28";
        //            sw.Write("|"); // 28 NO SE APLICA
        //            columna = "29";
        //            sw.Write(((!String.IsNullOrEmpty(item.doxpc_num_comprobante_referencia)) ? item.doxpc_num_comprobante_referencia : "") + "|"); // 29
        //            columna = "30";
        //            sw.Write("|"); // 30 NO SE APLICA
        //            columna = "31";
        //            sw.Write(((item.doxpc_sfec_deposito_detraccion != null) ? Convert.ToDateTime(item.doxpc_sfec_deposito_detraccion).ToString("dd/MM/yyyy") : String.Empty) + "|"); // 31
        //            columna = "32";
        //            sw.Write(item.doxpc_vnro_deposito_detraccion + "|"); // 32
        //            columna = "33";
        //            sw.Write("0" + "|"); // 33

        //            columna = "34";
        //            if ((Convert.ToInt32(lkpMes.EditValue) == Convert.ToDateTime(item.doxpc_sfecha_doc).Month && Parametros.intEjercicio == Convert.ToDateTime(item.doxpc_sfecha_doc).Year)
        //                && item.tdocc_icod_tipo_doc != 84)//boleta de compra
        //            {
        //                sw.Write("1" + "|"); // 34
        //            }
        //            else if (item.tdocc_icod_tipo_doc == 84)
        //            {
        //                sw.Write("0" + "|"); // 34
        //            }
        //            else
        //            {
        //                sw.Write("6" + "|"); // 34
        //            }

        //            // 35 NO SE APLICA

        //            if (totFilas != cont)
        //            {
        //                sw.WriteLine();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ArgumentException(ex.Message + "\nFila " + cont + "\nDocumento " + error + "\nColumna Nº " + columna);
        //    }
        //    finally
        //    {
        //        sw.Close();
        //    }
        //}


    }
}