using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Consultas
{
    public partial class FrmConsultaVentasDaot : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsultaVentasDaot));

        public FrmConsultaVentasDaot()
        {
            InitializeComponent();
        }

        List<EVentasDaot> Lista = new List<EVentasDaot>();
        decimal monto;

        private void FrmConsultaVentasDaot_Load(object sender, EventArgs e)
        {
            txtMontoUIT.Text = new BAdministracionSistema().listarParametro()[0].pm_nuit_parametro.ToString();
        }

        public void Carga()
        {
            BaseEdit oBase = null;
            try
            {
                if (Convert.ToDecimal(txtMontoUIT.Text) == 0)
                {
                    oBase = txtMontoUIT;
                    throw new ArgumentException("Ingrese monto UIT");
                }

                if (Convert.ToDecimal(txtCantidad.Text) == 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("Ingrese una cantidad");
                }

                monto = Convert.ToDecimal(txtMontoUIT.Text) * Convert.ToDecimal(txtCantidad.Text);
                Lista = new BVentas().ListarVentasDaot(monto, Parametros.intEjercicio);
                grd.DataSource = Lista;
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Carga();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                Carga();
        }

        private void detalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmConsultaVentasDaotDetalle frm = new FrmConsultaVentasDaotDetalle())
            {
                EVentasDaot obe = (EVentasDaot)gv.GetRow(gv.FocusedRowHandle);
                frm.icod_clie = Convert.ToInt64(obe.cliec_icod_cliente);
                frm.nombre_clie = obe.cliec_vnombre_cliente;
                frm.ShowDialog();
            }
        }

        private void listaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista != null && Lista.Count != 0)
            {
                rptVentasDaotSD rpt = new rptVentasDaotSD();
                rpt.Cargar(Lista, monto);
            }
            else
                XtraMessageBox.Show("No hay registros a imprimir", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void listaConDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista != null && Lista.Count != 0)
            {
                rptVentasDaotCD rpt = new rptVentasDaotCD();
                rpt.Cargar(new BVentas().ListarVentasDaotDetalle(monto, Parametros.intEjercicio), Lista, monto);
            }
            else
                XtraMessageBox.Show("No hay registros a imprimir", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void detalleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Lista != null && Lista.Count != 0)
            {
                EVentasDaot obe = (EVentasDaot)gv.GetRow(gv.FocusedRowHandle);
                rptVentasDaotxClieCD rpt = new rptVentasDaotxClieCD();
                rpt.Cargar(new BVentas().ListarVentasDaotDetallexCliente(Convert.ToInt64(obe.cliec_icod_cliente), Parametros.intEjercicio), obe.cliec_vnombre_cliente);
            }
            else
                XtraMessageBox.Show("No hay registros a imprimir", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exportarADBFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lista != null && Lista.Count != 0)
                {
                    string sourcePath = "\\\\JABSASERVER-PC\\SGE-Publish\\DAOT";
                    string fileName = "INGRESOS.DBF";
                    ExportarA_DBF(sourcePath, fileName);


                    //int eleccion;
                    //using (FrmDaotEleccionOrigen frm = new FrmDaotEleccionOrigen())
                    //{
                    //    frm.ShowDialog();
                    //    if (frm.DialogResult == DialogResult.OK)
                    //    {
                    //        eleccion = frm.eleccion;
                    //        string fileName, sourcePath;

                    //        if (eleccion == 0) // archivo predefinido
                    //        {
                    //            sourcePath = "SERVIDOR\\SGIPublish\\OTROS\\DAOT";
                    //            fileName = "INGRESOS.DBF";
                    //            ExportarA_DBF(sourcePath, fileName);
                    //        }
                    //        else // archivo definido por el usuario
                    //        {
                    //            if (ofd.ShowDialog(this) == DialogResult.OK)
                    //            {
                    //                sourcePath = Path.GetDirectoryName(ofd.FileName);
                    //                fileName = ofd.SafeFileName;
                    //                ExportarA_DBF(sourcePath, fileName);
                    //            }
                    //        }
                    //    }
                    //}
                }
                else
                    XtraMessageBox.Show("No hay registros para generar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OleDbException ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ExportarA_DBF(string sourcePath, string fileName)
        {
            try
            {
                string fileNameSinExt, destPath, sourceFile, destFile;
                fileNameSinExt = Path.GetFileNameWithoutExtension(fileName);
                int cont = 0;
                using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sourcePath + ";Extended Properties=dBASE IV;"))
                {
                    cn.Open();
                    OleDbCommand cmd = new OleDbCommand("DELETE FROM " + fileNameSinExt, cn);
                    cmd.ExecuteNonQuery();
                    foreach (var item in Lista.Where(obe => obe.cliec_vcod_cliente != "99999999999"))
                    {
                        cont++;
                        cmd.CommandText = "INSERT INTO " + fileNameSinExt + " VALUES(@cont,@tipo_persona_fija,@empresa_ruc,@periodo,@tipo_persona,@tipo_doc_cliente,@num_doc,@importe,@apep,@apem,@nom1,@nom2,@clie_nom)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@cont", cont);
                        cmd.Parameters.AddWithValue("@tipo_persona_fija", 6);
                        cmd.Parameters.AddWithValue("@empresa_ruc", Valores.strRUC);
                        cmd.Parameters.AddWithValue("@periodo", Parametros.intEjercicio);
                        cmd.Parameters.AddWithValue("@tipo_persona", (item.tipo_persona != null) ? item.tipo_persona : string.Empty);
                        cmd.Parameters.AddWithValue("@tipo_doc_cliente", (item.tip_doc_cliente != null) ? item.tip_doc_cliente.ToString() : string.Empty);
                        cmd.Parameters.AddWithValue("@num_doc", (item.num_doc_cliente != null) ? item.num_doc_cliente : string.Empty);
                        cmd.Parameters.AddWithValue("@importe", item.valor_venta_soles);
                        cmd.Parameters.AddWithValue("@apep", item.cliec_vapellido_paterno);
                        cmd.Parameters.AddWithValue("@apem", item.cliec_vapellido_materno);
                        cmd.Parameters.AddWithValue("@nom1", item.cliec_vnombre1);
                        cmd.Parameters.AddWithValue("@nom2", item.cliec_vnombre2);
                        cmd.Parameters.AddWithValue("@clie_nom", (item.cliec_vnombre_cliente.Length < 40) ? item.cliec_vnombre_cliente : item.cliec_vnombre_cliente.Substring(0, 40));
                        cmd.ExecuteNonQuery();
                    }
                    cn.Close();
                }
                if (cont > 0)
                {
                    XtraMessageBox.Show("Se generó el archivo DBF correctamente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Copia del archivo al usuario
                    bool bCopy = false;
                    do
                    {
                        if (fbd.ShowDialog(this) == DialogResult.OK)
                        {
                            destPath = fbd.SelectedPath;
                            sourceFile = Path.Combine(sourcePath, fileName);
                            destFile = Path.Combine(destPath, fileName);
                            File.Copy(sourceFile, destFile, true);
                            bCopy = true;
                            XtraMessageBox.Show("Se copió el archivo correctamente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            System.Diagnostics.Process.Start(destPath);
                        }
                        else
                        {
                            if (XtraMessageBox.Show("¿Desea salir sin haber realizado una copia del archivo?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                bCopy = true;
                            }
                            else
                            {
                                bCopy = false;
                            }
                        }
                    } while (!bCopy);
                }
                else
                {
                    XtraMessageBox.Show("No se generó el archivo DBF, intentarlo nuevamente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}