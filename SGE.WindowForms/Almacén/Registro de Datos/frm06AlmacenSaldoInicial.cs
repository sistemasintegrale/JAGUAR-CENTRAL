using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Almacén.Registro_de_Datos
{
    public partial class frm06AlmacenSaldoInicial : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm06AlmacenSaldoInicial));
        List<EKardex> lstKardex = new List<EKardex>();

        public frm06AlmacenSaldoInicial()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
            bteAlmacen.Tag = 53;
            bteAlmacen.Text = "PRINCIPAL";
            filtrar();
        }

        private void cargar()
        {
            lstKardex = new BAlmacen().listarAlmacenSaldoInicial(Parametros.intEjercicio);
            grdKardex.DataSource = lstKardex;

        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstKardex.FindIndex(x => x.kardc_icod_correlativo == intIcod);
            viewKardex.FocusedRowHandle = index;
            viewKardex.Focus();
        }

        private void imprimir()
        {
            //if (lstKardex.Count > 0)
            //{
            //    rptStock rpt = new rptStock();
            //    rpt.cargar(String.Format("STOCK DE ALMACÉN: {0} - PRODUCTO: {1}", bteAlmacen.Text, bteProducto.Text), String.Format("HASTA: {0}", f2.ToShortDateString()), lstKardex);
            //}
        }



        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }

        private void verKardex()
        {
            //try
            //{
            //    if (lstKardex.Count == 0)
            //        return;
            //    frmConsultaKardexPorFecAlmProd frm = new frmConsultaKardexPorFecAlmProd();
            //    frm.Text = String.Format("Kardex: {0} - {1}", bteAlmacen.Text, bteProducto.Text);
            //    frm.f1 = f1;
            //    frm.f2 = f2;
            //    frm.intAlmacen = Convert.ToInt32(bteAlmacen.Tag);
            //    frm.intProducto = Convert.ToInt32(bteProducto.Tag);
            //    frm.Show();
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void verKardexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            verKardex();
        }

        private void btnKardex_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            verKardex();
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewKardex.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewKardex.ClearColumnsFilter();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMantAlmacenSaldoInicial frm = new frmMantAlmacenSaldoInicial();
            frm.MiEvento += new frmMantAlmacenSaldoInicial.DelegadoMensaje(reload);
            frm.lstKardex = lstKardex;
            frm.SetInsert();
            frm.Show();
        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            EKardex Obe = (EKardex)viewKardex.GetRow(viewKardex.FocusedRowHandle);
            if (Obe == null)
                return;
            frmMantAlmacenSaldoInicial frm = new frmMantAlmacenSaldoInicial();
            frm.MiEvento += new frmMantAlmacenSaldoInicial.DelegadoMensaje(reload);
            frm.oBe = Obe;
            frm.lstKardex = lstKardex;
            frm.SetModify();
            frm.Show();
            frm.setValues();

        }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EKardex Obe = (EKardex)viewKardex.GetRow(viewKardex.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewKardex.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BAlmacen().eliminarKardex(Obe);
                    cargar();
                    if (lstKardex.Count >= index + 1)
                        viewKardex.FocusedRowHandle = index;
                    else
                        viewKardex.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAlmacen_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void txtProducto_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void filtrar()
        {
            grdKardex.DataSource = lstKardex.Where(x => x.almac_icod_almacen == Convert.ToInt32(bteAlmacen.Tag) && x.strProducto.Contains(txtProducto.Text.ToUpper())).ToList();
        }

        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
        }
        private void listarAlmacen()
        {
            try
            {
                frmListarAlmacen frm = new frmListarAlmacen();

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteAlmacen.Tag = frm._Be.almac_icod_almacen;
                    bteAlmacen.Text = frm._Be.almac_vdescripcion;
                    filtrar();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        string filePath = "";
        private void importarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(bteAlmacen.Tag) != 0)
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
            else
            {
                XtraMessageBox.Show("Para Importar los datos es necesario que seleccione un Almacén", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            List<EInventarioDet> lstDetalleaUXAX = new List<EInventarioDet>();

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
                lstDetalleaUX = lstDetalleaUX.Where(ob => (ob.invd_icantidad) != 0).Distinct().ToList();
                foreach (var _q in lstDetalleaUX)
                {
                    if (lstDetalleaUXAX.Count(ob => ob.strCodProducto.Trim() == _q.strCodProducto.Trim()) == 0)
                    {
                        lstDetalleaUXAX.Add(_q);
                    }
                    else
                    {
                        foreach (var pp in lstDetalleaUXAX)
                        {
                            if (pp.strCodProducto.Trim() == _q.strCodProducto.Trim())
                            {
                                pp.invd_icantidad = pp.invd_icantidad + _q.invd_icantidad;
                            }
                        }
                    }

                }


                if (lstDetalleaUXAX.Count() != 0)
                {
                    if (XtraMessageBox.Show("Los registros serán actualizados en Kardex y Stock del sistema ¿Desea continuar con la operación?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        foreach (var _baux in lstDetalleaUXAX)
                        {
                            List<EProducto> MlistProd = new BAlmacen().listarProductoXCodigp(Parametros.intEjercicio, _baux.strCodProducto.Trim());

                            if (MlistProd.Count() == 1)
                            {
                                if (_baux.invd_icantidad != 0)
                                {
                                    EKardex oBe = new EKardex();
                                    oBe.kardc_ianio = Parametros.intEjercicio;
                                    oBe.kardc_fecha_movimiento = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1);
                                    oBe.almac_icod_almacen = Convert.ToInt32(bteAlmacen.Tag);
                                    oBe.prdc_icod_producto = Convert.ToInt32(MlistProd[0].prdc_icod_producto);
                                    oBe.kardc_icantidad_prod = Convert.ToDecimal(_baux.invd_icantidad);
                                    oBe.tdocc_icod_tipo_doc = Parametros.intTipoDocAperturaKardex;
                                    oBe.kardc_numero_doc = "000001";
                                    oBe.kardc_tipo_movimiento = Parametros.intKardexIn;
                                    oBe.kardc_iid_motivo = 100;//INGRESO A ALMACEN POR SALDO INICIAL
                                    oBe.kardc_beneficiario = String.Format("SALDO INICIAL DEL PRODUCTO {0}", bteAlmacen.Text);
                                    oBe.kardc_observaciones = "";
                                    oBe.intUsuario = Valores.intUsuario;
                                    oBe.strPc = Valores.strUsuario;
                                    _baux.prdc_icod_producto = MlistProd[0].prdc_icod_producto;
                                    oBe.kardc_icod_correlativo = new BAlmacen().insertarKardex(oBe);
                                }
                            }
                        }
                    }

                }

                cargar();
                grdKardex.RefreshDataSource();
                //List<EInventarioDet> MlistInexistentes = new List<EInventarioDet>();
                //foreach (var _b in lstDetalleaUX)
                //{
                //    if (_b.strCodProducto.Trim() != "")
                //    {
                //        int contador = lstDetalle.Count(ob => ob.strCodProducto == _b.strCodProducto);
                //        if (contador == 0)
                //        {
                //            MlistInexistentes.Add(_b);
                //        }
                //    }
                //}

                //if (MlistInexistentes.Count > 0)
                //{

                //    if (XtraMessageBox.Show("Existen productos que no estan dentro del catálogo de productos ¿Desea importarlos en un documento TXT?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                //    {
                //        if (sfdTXT.ShowDialog(this) == DialogResult.OK)
                //        {
                //            string fileName = sfdTXT.FileName;
                //            if (!fileName.Contains(".txt"))
                //            {
                //                ExportarATXT(fileName + ".txt", MlistInexistentes);
                //                System.Diagnostics.Process.Start(fileName + ".txt");
                //            }
                //            else
                //            {
                //                ExportarATXT(fileName, MlistInexistentes);
                //                System.Diagnostics.Process.Start(fileName);
                //            }
                //        }
                //        sfdTXT.FileName = string.Empty;


                //    }
                //}



                //viewDetalle.RefreshData();



            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error Fila: " + contF + "\t Columna: " + nomC, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}