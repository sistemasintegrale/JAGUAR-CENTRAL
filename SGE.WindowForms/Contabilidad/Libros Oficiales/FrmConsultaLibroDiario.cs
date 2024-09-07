using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Libros_Oficiales
{
    public partial class FrmConsultaLibroDiario : DevExpress.XtraEditors.XtraForm
    {

        List<EComprobante> Lista = new List<EComprobante>();
        List<EComprobante> listaTXT = new List<EComprobante>();


        public FrmConsultaLibroDiario()
        {
            InitializeComponent();
        }

        private void FrmConsultaLibroDiario_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(4).Where(ob => ob.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMes.EditValue = DateTime.Now.Month;
        }

        private void bteSubInicial_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            CargarSubInicial();
        }

        private void bteSubInicial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                CargarSubInicial();
            else if (e.KeyCode == Keys.Enter)
                Buscar();
        }

        private void CargarSubInicial()
        {
            using (FrmConsultaSubdiario frm = new FrmConsultaSubdiario("Inicial"))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (frm.obeSubdiario.subdi_icod_subdiario == 0)
                    {
                        bteSubInicial.Tag = null;
                        bteSubInicial.Text = string.Empty;
                        lblSubInicial.Text = string.Empty;
                    }
                    else
                    {
                        bteSubInicial.Tag = frm.obeSubdiario.subdi_icod_subdiario;
                        bteSubInicial.Text = string.Format("{0:00}", frm.obeSubdiario.subdi_icod_subdiario);
                        lblSubInicial.Text = frm.obeSubdiario.subdi_vdescripcion;
                        bteSubFinal.Focus();
                    }
                }
            }
        }

        private void bteSubFinal_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            CargarSubFinal();
        }

        private void bteSubFinal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                CargarSubFinal();
            else if (e.KeyCode == Keys.Enter)
                Buscar();
        }

        private void CargarSubFinal()
        {
            using (FrmConsultaSubdiario frm = new FrmConsultaSubdiario("Final"))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (frm.obeSubdiario.subdi_icod_subdiario == 0)
                    {
                        bteSubFinal.Tag = null;
                        bteSubFinal.Text = string.Empty;
                        lblSubFinal.Text = string.Empty;
                    }
                    else
                    {
                        bteSubFinal.Tag = frm.obeSubdiario.subdi_icod_subdiario;
                        bteSubInicial.Text = string.Format("{0:00}", frm.obeSubdiario.subdi_icod_subdiario);
                        lblSubFinal.Text = frm.obeSubdiario.subdi_vdescripcion;
                    }
                }
            }
        }

        private void Buscar()
        {
            try
            {
                if (bteSubInicial.Tag == null && bteSubFinal.Tag != null)
                    throw new ArgumentException("Seleccione Subdiario Inicial");
                else if (bteSubFinal.Tag == null && bteSubInicial.Tag != null)
                    throw new ArgumentException("Seleccione Subdiario Final");
                else if (Convert.ToInt32(bteSubInicial.Tag) > Convert.ToInt32(bteSubFinal.Tag))
                    throw new ArgumentException("El rango de Subdiarios es incorrecto");
                else
                {
                    Lista = new BContabilidad().ListarComprobantesxSubdiario(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue), Convert.ToInt32(bteSubInicial.Tag), Convert.ToInt32(bteSubFinal.Tag));
                    grd.DataSource = Lista;
                    listaTXT = new BContabilidad().ListarComprobantesxSubdiario_TXT(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue));
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lkpMes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Buscar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void gv_DoubleClick(object sender, EventArgs e)
        {
            View();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View();
        }

        private void View()
        {
            EComprobante oBe = (EComprobante)gv.GetRow(gv.FocusedRowHandle);
            if (oBe != null)
            {
                FrmManteDetalleComprobante frm = new FrmManteDetalleComprobante();
                frm.MiEvento += new FrmManteDetalleComprobante.DelegadoMensaje(viewEvent);
                frm.SetCancel();
                frm.CodeAnio = Parametros.intEjercicio;
                frm.CodeMes = Convert.ToInt32(lkpMes.EditValue);
                frm.modiComp = oBe;
                frm.code = oBe.iid_voucher_contable;
                frm.oDetail = Lista;
                frm.Show();
            }
        }

        void viewEvent()
        {
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count != 0)
            {

                rptConsultaCompDetXSubdiario rpt = new rptConsultaCompDetXSubdiario();
                rpt.Cargar(new BContabilidad().ListarComprobanteDetallexSubdiario(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue), Convert.ToInt32(bteSubInicial.Tag), Convert.ToInt32(bteSubFinal.Tag)),
                    Lista, lkpMes.Text);
            }
            else
                XtraMessageBox.Show("No hay registros por imprimir", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        struct StructSubdiario
        {
            public string numSubdiario { get; set; }
            public string descSubdiario { get; set; }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            bool activo = gv.OptionsView.ShowAutoFilterRow;
            gv.ClearColumnsFilter();
            if (activo)
            {
                btnFiltrar.Text = "Mostrar filtro";
                gv.OptionsView.ShowAutoFilterRow = !activo;
            }
            else
            {
                btnFiltrar.Text = "Ocultar filtro";
                gv.OptionsView.ShowAutoFilterRow = !activo;
            }
        }

        private void exportarAExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                {
                    grdExcel.DataSource = new BContabilidad().ListarComprobanteDetallexSubdiario(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue), Convert.ToInt32(bteSubInicial.Tag), Convert.ToInt32(bteSubFinal.Tag)).OrderBy(obe => obe.iid_subdiario_vnum_voucher).ToList();
                    string fileName = sfdRuta.FileName;
                    if (!fileName.Contains(".xlsx"))
                    {
                        grdExcel.ExportToXlsx(fileName + ".xlsx");
                        System.Diagnostics.Process.Start(fileName + ".xlsx");
                    }
                    else
                    {
                        grdExcel.ExportToXlsx(fileName);
                        System.Diagnostics.Process.Start(fileName);
                    }
                    grdExcel.DataSource = null;
                    sfdRuta.FileName = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void exportarTXTToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
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
                int totFilas = listaTXT.Count;
                foreach (EComprobante item in listaTXT)
                {
                    cont++;

                    columna = "1";
                    sw.Write(item.vcocd_AnioDocu.ToString() + "|");

                    columna = "2";
                    sw.Write(item.vcocd_formato_txt.ToString() + "|");
                    columna = "3";
                    sw.Write(item.vnumero_voucher_contable.ToString() + "|");
                    columna = "4";
                    sw.Write(item.ctacc_icod_cuenta_contable.ToString() + "|");

                    columna = "5";
                    sw.Write("|");
                    columna = "6";
                    sw.Write("|");
                    columna = "7";
                    sw.Write(item.vcocd_moneda.ToString() + "|");


                    columna = "8";
                    if (item.anad_icod_analitica > 0)
                    {
                        if (item.tarec_icorrelativo_tipo_analitica == 5 || item.tarec_icorrelativo_tipo_analitica == 2)
                        {
                            if (item.anad_iid_analitica.Substring(0, 2) == "20" || item.anad_iid_analitica.Substring(0, 2) == "10" || item.anad_iid_analitica.Substring(0, 2) == "15" || item.anad_iid_analitica.Substring(0, 2) == "17")
                            {
                                if (item.anad_iid_analitica.Trim().Length == 11)
                                {
                                    sw.Write("6" + "|");
                                }
                                else if (item.anad_iid_analitica.Trim().Length == 8)
                                {
                                    sw.Write("1" + "|");
                                }
                            }
                            else
                            {
                                sw.Write("|");
                            }
                        }
                        else
                        {
                            sw.Write("|");
                        }
                    }
                    else
                    {
                        sw.Write("|");
                    }
                    columna = "9";
                    if (item.anad_icod_analitica > 0)
                    {
                        if (item.tarec_icorrelativo_tipo_analitica == 5 || item.tarec_icorrelativo_tipo_analitica == 2)
                        {
                            if (item.anad_iid_analitica.Substring(0, 2) == "20" || item.anad_iid_analitica.Substring(0, 2) == "10" || item.anad_iid_analitica.Substring(0, 2) == "15" || item.anad_iid_analitica.Substring(0, 2) == "17")
                            {
                                if (item.anad_iid_analitica.Trim().Length == 11)
                                {
                                    sw.Write(item.anad_iid_analitica + "|");
                                }
                                else if (item.anad_iid_analitica.Trim().Length == 8)
                                {
                                    sw.Write(item.anad_iid_analitica + "|");
                                }
                            }
                            else
                            {
                                sw.Write("|");
                            }
                        }
                        else
                        {
                            sw.Write("|");
                        }
                    }
                    else
                    {
                        sw.Write("|");
                    }

                    columna = "10";
                    sw.Write(item.tdocc_coa.ToString() + "|");


                    columna = "11";
                    if (item.tdocc_coa == "05")
                    {
                        sw.Write("1" + "|");
                    }
                    else if (item.tdocc_coa == "54")
                    {
                        sw.Write(item.doxpc_codigo_aduana + "|");
                    }
                    else if (item.tdocc_coa == "20")
                    {
                        sw.Write(item.vcocd_numero_doc.Substring(0, 4) + "|");
                    }
                    else
                        sw.Write(item.vcocd_numero_doc__show.ToString() + "|");

                    columna = "12";
                    if (item.tdocc_coa == "05")
                    {
                        sw.Write(item.vcocd_numero_doc.ToString() + "|");
                    }
                    else if (item.tdocc_coa == "20")
                    {
                        sw.Write(item.vcocd_numero_doc.Substring(4) + "|");
                    }
                    else
                        sw.Write(item.vcocd_numero_doc.ToString() + "|");

                    columna = "13";
                    sw.Write("|");

                    columna = "14";
                    sw.Write("|");

                    columna = "15";
                    sw.Write(Convert.ToDateTime(item.sfec_nota_contable).ToString("dd/MM/yyyy") + "|");


                    columna = "16";
                    sw.Write(item.vglosa.ToString() + "|");



                    columna = "17";
                    sw.Write("|"); // 8 


                    columna = "18";
                    sw.Write(item.nmto_tot_debe_sol.ToString() + "|");



                    columna = "19";
                    sw.Write(item.nmto_tot_haber_dol.ToString() + "|"); // 8 



                    columna = "20";
                    sw.Write("|");


                    columna = "21";
                    sw.Write(item.vcocd_Vperido_fech.ToString() + "|");



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


    }
}