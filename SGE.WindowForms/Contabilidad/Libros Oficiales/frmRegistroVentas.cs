using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Libros_Oficiales
{
    public partial class frmRegistroVentas : DevExpress.XtraEditors.XtraForm
    {
        private List<ERegistroVentas> Lista = new List<ERegistroVentas>();

        public frmRegistroVentas()
        {
            InitializeComponent();
        }

        private void frmRegistroVentas_Load(object sender, EventArgs e)
        {
            var lstMeses = new BGeneral().listarTablaRegistro(4);
            BSControls.LoaderLook(lkpMes, lstMeses.Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", false);
            lkpMes.EditValue = DateTime.Now.Month;
            //cargaLkpTipoDoc();
        }

        private void Carga()
        {
            Lista = new BContabilidad().ListarRegistroDeVentas(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue)).Where(ob => ob.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoCliente).ToList();
            Lista = Lista.OrderBy(ord => ord.tdocc_vabreviatura_tipo_doc).ThenBy(ord => ord.doxcc_vnumero_doc).ToList();
            grd.DataSource = Lista;
        }

        private void lkpMes_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpMes.EditValue != null)
                Carga();
            //cargaLkpTipoDoc();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                rptRegistroVentas reporte = new rptRegistroVentas();
                reporte.Cargar(Lista, lkpMes.Text);
            }
        }

        private void exportarAExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (sfdRuta.ShowDialog(this) == DialogResult.OK)
            //{
            //    //#region Visible1
            //    ////FALSE
            //    //gc1.Visible = false;
            //    //gc2.Visible = false;
            //    //gc3.Visible = false;
            //    //gc4.Visible = false;
            //    //gc5.Visible = false;
            //    //gc6.Visible = false;
            //    //gc7.Visible = false;
            //    //gc8.Visible = false;
            //    //gc9.Visible = false;
            //    //gc10.Visible = false;
            //    //gc11.Visible = false;

            //    //gc13.Visible = false;
            //    //gc14.Visible = false;
            //    //gc15.Visible = false;
            //    //gc16.Visible = false;
            //    //gc17.Visible = false;
            //    ////TRUE
            //    //gc18.Visible = true;
            //    //gc19.Visible = true;
            //    //gc20.Visible = true;
            //    //gc21.Visible = true;
            //    //gc22.Visible = true;
            //    //gc23.Visible = true;
            //    //gc24.Visible = true;
            //    //gc25.Visible = true;
            //    //gc26.Visible = true;
            //    //gc27.Visible = true;
            //    //gc28.Visible = true;
            //    //gc29.Visible = true;
            //    //gc30.Visible = true;
            //    //gc31.Visible = true;
            //    //gc32.Visible = true;
            //    //gc33.Visible = true;
            //    //gc34.Visible = true;
            //    //gc35.Visible = true;
            //    //gc36.Visible = true;
            //    //gc37.Visible = true;
            //    //gc38.Visible = true;
            //    //gc39.Visible = true;
            //    //#endregion

            //    //grd.DataSource = Lista;
            //    //string fileName = sfdRuta.FileName;
            //    //if (!fileName.Contains(".xlsx"))
            //    //{
            //    //    grd.ExportToXlsx(fileName + ".xlsx");
            //    //    System.Diagnostics.Process.Start(fileName + ".xlsx");
            //    //}
            //    //else
            //    //{
            //    //    grd.ExportToXlsx(fileName);
            //    //    System.Diagnostics.Process.Start(fileName);
            //    //}

            //    //#region Visible2
            //    ////FALSE
            //    //gc18.Visible = false;
            //    //gc19.Visible = false;
            //    //gc20.Visible = false;
            //    //gc21.Visible = false;
            //    //gc22.Visible = false;
            //    //gc23.Visible = false;
            //    //gc24.Visible = false;
            //    //gc25.Visible = false;
            //    //gc26.Visible = false;
            //    //gc27.Visible = false;
            //    //gc28.Visible = false;
            //    //gc29.Visible = false;
            //    //gc30.Visible = false;
            //    //gc31.Visible = false;
            //    //gc32.Visible = false;
            //    //gc33.Visible = false;
            //    //gc34.Visible = false;
            //    //gc35.Visible = false;
            //    //gc36.Visible = false;
            //    //gc37.Visible = false;
            //    //gc38.Visible = false;
            //    //gc39.Visible = false;
            //    ////TRUE
            //    //gc1.Visible = true;                
            //    //gc2.Visible = true;
            //    //gc3.Visible = true;
            //    //gc4.Visible = true;
            //    //gc5.Visible = true;
            //    //gc6.Visible = true;
            //    ////gc7.Visible = true;
            //    //gc8.Visible = true;
            //    //gc9.Visible = true;
            //    //gc10.Visible = true;
            //    //gc11.Visible = true;

            //    //gc13.Visible = true;
            //    //gc14.Visible = true;
            //    //gc15.Visible = true;
            //    //gc16.Visible = true;
            //    //gc17.Visible = true;

            //    //gc1.VisibleIndex = 0;
            //    //gc2.VisibleIndex = 1;
            //    //gc3.VisibleIndex = 2;
            //    //gc4.VisibleIndex = 3;
            //    //gc5.VisibleIndex = 4;
            //    //gc6.VisibleIndex = 5;
            //    ////gc7.VisibleIndex = 6;
            //    //gc8.VisibleIndex = 7;
            //    //gc9.VisibleIndex = 8;
            //    //gc10.VisibleIndex = 9;
            //    //gc11.VisibleIndex = 10;

            //    //gc13.VisibleIndex = 12;
            //    //gc14.VisibleIndex = 13;
            //    //gc15.VisibleIndex = 14;
            //    //gc16.VisibleIndex = 15;
            //    //gc17.VisibleIndex = 16;
            //    //#endregion


            //}
            if (sfdRuta.ShowDialog(this) == DialogResult.OK)
            {

                if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                {
                    grd.DataSource = Lista;
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
                    grd.DataSource = null;
                    sfdRuta.FileName = string.Empty;
                }

                grd.DataSource = Lista;


            }

        }

        private void tXTToolStripMenuItem_Click(object sender, EventArgs e)
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
                string Nombre = "LE" + Valores.strRUC + Parametros.intEjercicio.ToString() + Mes + "00" + "140100" + "00" + "1" + ConSinInfo + "1" + "1" + ".txt";
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
                foreach (ERegistroVentas item in Lista)
                {
                    cont++;
                    error = item.tdocc_vabreviatura_tipo_doc + " " + item.doxcc_vnumero_doc;
                    columna = "1";
                    sw.Write(Parametros.intEjercicio.ToString() + String.Format("{0:00}", lkpMes.EditValue) + "00|"); // 1
                    columna = "2";
                    if (item.tablc_iid_situacion_documento == Parametros.intSitDocCobrarAnulado)
                    {
                        //sw.Write(item.CUO + "|"); // 2
                        sw.Write(Parametros.intEjercicio.ToString() + String.Format("{0:00}", lkpMes.EditValue) + "|");
                    }
                    else
                    {
                        sw.Write(Parametros.intEjercicio.ToString() + String.Format("{0:00}", lkpMes.EditValue) + item.CUO + "|"); // 2
                    }
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
                    sw.Write(Convert.ToDateTime(item.doxcc_sfecha_doc).ToString("dd/MM/yyyy") + "|"); // 4
                    columna = "5";
                    sw.Write(Convert.ToDateTime(item.doxcc_sfecha_doc).ToString("dd/MM/yyyy") + "|"); // 5
                    columna = "6";
                    sw.Write(item.tdocc_vcodigo_tipo_doc_sunat + "|"); // 6
                    columna = "7";
                    sw.Write("0" + item.doxcc_vnumero_doc.Substring(0, 3) + "|"); // 7
                    columna = "8";
                    sw.Write(item.doxcc_vnumero_doc.Substring(3) + "|"); // 8
                    columna = "9";
                    sw.Write("|"); // 9

                    columna = "10";
                    if (((item.tip_doc_cliente_2 == "6") && (item.tip_doc_cliente_2.Length != 11) && (item.tdocc_icod_tipo_doc != Parametros.intTipoDocFacturaVenta)) ||
                        ((item.tip_doc_cliente_2 == "1") && (item.tip_doc_cliente_2.Length != 8)))
                    {
                        sw.Write("0|"); // 10
                    }
                    else
                    {
                        sw.Write(item.tip_doc_cliente_2 + "|"); // 10
                    }

                    columna = "11";
                    sw.Write(item.num_doc_cliente_2.TrimEnd().TrimStart() + "|"); // 11
                    columna = "12";
                    sw.Write(item.cliec_vnombre_cliente_2 + "|"); // 12
                    // Montos
                    columna = "13";
                    //sw.Write(String.Format("{0:0.00}", item.doxcc_nmonto_exportacion) + "|"); // 13
                    sw.Write("0.00" + "|");
                    columna = "14";
                    sw.Write(String.Format("{0:0.00}", item.doxcc_nmonto_afecto) + "|"); // 14
                    columna = "15";
                    sw.Write(String.Format("{0:0.00}", item.valor_ex) + "|"); // 15
                    columna = "16";
                    sw.Write(String.Format("{0:0.00}", item.doxcc_nmonto_impuesto) + "|"); // 18
                    columna = "17";
                    sw.Write(String.Format("{0:0.00}", item.valor_ex) + "|"); // 15
                    columna = "18";
                    sw.Write(String.Format("{0:0.00}", item.valor_ex) + "|"); // 15
                    columna = "19";
                    sw.Write("0.00" + "|"); // 16
                    columna = "20";
                    //sw.Write(String.Format("{0:0.00}", item.doxcc_nmonto_isc) + "|"); // 17
                    sw.Write("0.00" + "|");
                    columna = "21";
                    sw.Write(String.Format("{0:0.00}", item.base_imp_ivap) + "|"); // 19
                    columna = "22";
                    sw.Write("" + "|"); // 22 I mpuetso a la bolsa
                    columna = "23";
                    //sw.Write(String.Format("{0:0.00}", item.doxcc_nmonto_iva) + "|"); // 20
                    sw.Write("0.00" + "|");
                    columna = "24";
                    sw.Write("0.00" + "|"); // 21
                    columna = "25";
                    sw.Write(String.Format("{0:0.00}", item.doxcc_nmonto_total) + "|"); // 22
                    //sw.Write(((item.doxcc_sfecha_emision_referencia != null) ? Convert.ToDateTime(item.doxcc_sfecha_emision_referencia).ToString("dd/MM/yyyy") : "") + "|"); // 25
                    columna = "26";
                    string Moneda = "";
                    if (item.tablc_iid_tipo_moneda == 3)
                    {
                        Moneda = "PEN";
                    }
                    else
                    {
                        Moneda = "USD";
                    }
                    sw.Write(Moneda + "|"); // 22
                    columna = "27";
                    sw.Write(String.Format("{0:0.000}", Math.Round(Convert.ToDecimal(item.doxcc_nmonto_tipo_cambio), 3)) + "|"); // 23
                    columna = "28";
                    sw.Write(Convert.ToDateTime(item.doxcc_sfecha_emision_referencia).ToString("dd/MM/yyyy") + "|"); // 27
                    columna = "29";
                    string indicador = "";
                    if (item.tablc_iid_situacion_documento == 11)
                    {
                        indicador = "";
                    }
                    else
                    {
                        if (item.doxcc_tipo_comprobante_referencia == 26)
                        {
                            indicador = "01";
                        }
                        else if (item.doxcc_tipo_comprobante_referencia == 9)
                        {
                            indicador = "03";
                        }
                        else
                        {
                            indicador = "";
                        }
                    }

                    sw.Write(indicador + "|"); // 29
                    columna = "30";
                    sw.Write(item.doxcc_num_serie_referencia + "|"); // 29
                    // 30 NO SE APLICA
                    columna = "31";
                    sw.Write(item.doxcc_num_comprobante_referencia + "|"); // 30
                    columna = "32";
                    sw.Write("" + "|"); // 31
                    columna = "33";
                    sw.Write("" + "|"); // 32
                    columna = "34";
                    sw.Write("1" + "|"); // 33
                    columna = "35";
                    if (item.tablc_iid_situacion_documento == 11)
                    {
                        sw.Write("2" + "|"); // 34
                    }
                    else
                        sw.Write("1" + "|"); // 34


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


        //private void ExportarATXT(String ruta)
        //{
        //    StreamWriter sw = new StreamWriter(ruta);
        //    string error = string.Empty;
        //    int cont = 0;
        //    string columna = string.Empty;
        //    try
        //    {
        //        int totFilas = Lista.Count;
        //        foreach (ERegistroVentas item in Lista)
        //        {
        //            cont++;
        //            error = item.tdocc_vabreviatura_tipo_doc + " " + item.doxcc_vnumero_doc;
        //            columna = "1";
        //            sw.Write(Parametros.intEjercicio.ToString() + String.Format("{0:00}", lkpMes.EditValue) + "00|"); // 1
        //            columna = "2";
        //            sw.Write(item.doxcc_icod_correlativo + "|"); // 2
        //            columna = "3";
        //            sw.Write("M" + String.Format("{0:00000}", cont) + "|"); // A: Aptertura M: Mes C:Cierre // 3
        //            columna = "4";
        //            sw.Write(Convert.ToDateTime(item.doxcc_sfecha_doc).ToString("dd/MM/yyyy") + "|"); // 4
        //            columna = "5";
        //            sw.Write(Convert.ToDateTime(item.doxcc_sfecha_vencimiento_doc).ToString("dd/MM/yyyy") + "|"); // 5
        //            columna = "6";
        //            sw.Write(item.tdocc_vcodigo_tipo_doc_sunat + "|"); // 6
        //            columna = "7";
        //            sw.Write("0" + item.doxcc_vnumero_doc.Substring(0, 3) + "|"); // 7
        //            columna = "8";
        //            sw.Write(item.doxcc_vnumero_doc.Substring(3) + "|"); // 8
        //            columna = "9";
        //            sw.Write("|"); // 9

        //            columna = "10";
        //            if (((item.tip_doc_cliente_2 == "6") && (item.tip_doc_cliente_2.Length != 11) && (item.tdocc_icod_tipo_doc != Parametros.intTipoDocFacturaVenta)) ||
        //                ((item.tip_doc_cliente_2 == "1") && (item.tip_doc_cliente_2.Length != 8)))
        //            {
        //                sw.Write("0|"); // 10
        //            }
        //            else
        //            {
        //                sw.Write(item.tip_doc_cliente_2 + "|"); // 10
        //            }

        //            columna = "11";
        //            sw.Write(item.num_doc_cliente_2.TrimEnd().TrimStart() + "|"); // 11
        //            columna = "12";
        //            sw.Write(item.cliec_vnombre_cliente_2 + "|"); // 12
        //            // Montos

        //            columna = "13";
        //            sw.Write(String.Format("{0:0.00}", item.doxcc_nmonto_afecto) + "|"); // 14
        //            columna = "14";
        //            sw.Write("0.00" + "|"); // 16

        //            columna = "15";
        //            sw.Write(String.Format("{0:0.00}", item.doxcc_nmonto_impuesto) + "|"); // 18
        //            columna = "16";
        //            sw.Write(String.Format("{0:0.00}", item.base_imp_ivap) + "|"); // 19

        //            columna = "21";
        //            sw.Write("0.00" + "|"); // 21
        //            columna = "22";
        //            sw.Write(String.Format("{0:0.00}", item.doxcc_nmonto_total) + "|"); // 22
        //            columna = "23";
        //            sw.Write(String.Format("{0:0.000}", Math.Round(Convert.ToDecimal(item.doxcc_nmonto_tipo_cambio), 3)) + "|"); // 23
        //            columna = "24";
        //            sw.Write(((item.doxcc_sfecha_emision_referencia != null) ? Convert.ToDateTime(item.doxcc_sfecha_emision_referencia).ToString("dd/MM/yyyy") : "") + "|"); // 25
        //            columna = "25";
        //            sw.Write(item.Coareferencia + "|"); // 25
        //            columna = "26";
        //            sw.Write(((!string.IsNullOrEmpty(item.doxcc_num_serie_referencia)) ? item.doxcc_num_serie_referencia : "") + "|"); // 27
        //            columna = "27";
        //            sw.Write(((!string.IsNullOrEmpty(item.doxcc_num_comprobante_referencia)) ? item.doxcc_num_comprobante_referencia: String.Empty) + "|"); // 27
        //            columna = "28";
        //            sw.Write("0.00" + "|"); // 28
        //            columna = "29";
        //            sw.Write(((item.tablc_iid_situacion_documento != Parametros.intSitDocCobrarAnulado) ? "1" : "2") + "|"); // 29
        //             //30 NO SE APLICA

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