using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmManteListaCompraCab : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        /*--------------*/
        public EListaPrecioCab Obe = new EListaPrecioCab();
        public List<EListaPrecioDetalle> lstDetalle = new List<EListaPrecioDetalle>();
        public List<EListaPrecioDetalle> lstDelete = new List<EListaPrecioDetalle>();
        /*--------------*/

        public frmManteListaCompraCab()
        {
            InitializeComponent();
        }

        private void frmManteFacturaCompra_Load(object sender, EventArgs e)
        {
            cargar();
        }

        public void setGuardar()
        {
            SetSave();
        }
        private void cargar()
        {
            int contadorWExi = 0;
            lstDetalle = new BCompras().listarPrecioCompraDet(Convert.ToInt32(Obe.lprec_icod_proveedor), Convert.ToInt32(bteProveedor.Tag), Parametros.intEjercicio);
            foreach (var _be in lstDetalle)
            {
                if (_be.prdc_icod_producto == 0)
                {
                    List<EProducto> MlisProdu = new BAlmacen().listarProductoXCodigp(Parametros.intEjercicio, _be.prdc_vcode_producto);
                    if (MlisProdu.Count() == 1)
                    {
                        _be.prdc_icod_producto = MlisProdu[0].prdc_icod_producto;
                        _be.intTipoOperacion = 2;
                        _be.lpedid_bExisteCatalogo = true;
                        _be.prdc_vdescripcion_larga = MlisProdu[0].prdc_vdescripcion_larga;
                        _be.prdc_vcode_producto = MlisProdu[0].prdc_vcode_producto;
                        _be.prdc_vAutor = MlisProdu[0].prdc_vAutor;
                        _be.edit_vdescripcion = MlisProdu[0].strEditorial;
                        contadorWExi = contadorWExi + 1;
                    }
                }
            }
            grdDetalle.DataSource = lstDetalle;
            txtTotalRegistro.Text = lstDetalle.Count().ToString();
            txtNuevos.Text = lstDetalle.Where(o => o.lpedid_bExisteCatalogo == false).ToList().Count().ToString();
            txtcatalogo.Text = lstDetalle.Where(o => o.lpedid_bExisteCatalogo == true).ToList().Count().ToString();

            if (contadorWExi != 0)
            {
                XtraMessageBox.Show("Se agregó " + contadorWExi + " productos al catálogo de productos, para actualizar la lista precios presione el Botón GUARDAR", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void setFecha(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
        }

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
            btnGuardar.Enabled = !Enabled;
            mnu.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.View)
                enableControls(false);

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                //bteRefreshTipoCambio.Enabled = false;
            }
            if (Status == BSMaintenanceStatus.View)
            {
                txtObservacion.Properties.ReadOnly = true;

            }

        }

        private void enableControls(bool Enabled)
        {
            txtListaNro.Enabled = Enabled;
            dtFecha.Enabled = Enabled;
            bteProveedor.Enabled = Enabled;

        }

        public void setValues()
        {
            bteProveedor.Text = Obe.edit_vdescripcion;
            bteProveedor.Tag = Obe.edit_icod_editorial;


            txtListaNro.Text = Obe.lprec_Numerolista;

            dtFecha.EditValue = Obe.lprec_sfecha_lista;
            txtObservacion.Text = Obe.lprec_Observaciones;
            vcod_proveedor = Obe.proc_vcod_proveedor;

        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                if (bteProveedor.Tag == null)
                {
                    oBase = bteProveedor;
                    throw new ArgumentException("Seleccione proveedor");
                }
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (txtListaNro.Enabled)
                    {
                        if (txtListaNro.Text == "000")
                        {
                            txtListaNro.Focus();
                            throw new ArgumentException("Ingrese nro. de Serie de la factura");
                        }



                    }

                }

                if (lstDetalle.Count == 0)
                {
                    XtraMessageBox.Show("La Factura de Compra, debe tener al menos un registro de un producto", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Flag = false;
                    return;

                }

                /**/
                DateTime? dtNullVal = null;
                int? intNullVal = null;
                Int16? intNullVal16 = null;

                Obe.edit_icod_editorial = Convert.ToInt32(bteProveedor.Tag);
                Obe.lprec_sfecha_lista = Convert.ToDateTime(dtFecha.EditValue);
                Obe.lprec_Numerolista = txtListaNro.Text;
                Obe.lprec_Observaciones = txtObservacion.Text;
                Obe.lprec_sflag_estado = true;
                /**/
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.lprec_icod_proveedor = new BCompras().insertarPrecioCompra(Obe, lstDetalle);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BCompras().modificarPrecioCompra(Obe, lstDetalle, lstDelete);
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
                    this.MiEvento(Obe.lprec_icod_proveedor);
                    this.Close();
                }
            }
        }
        string vcod_proveedor = "";
        private void listarProveedor()
        {
            FrmListarProveedorMercaderia frm = new FrmListarProveedorMercaderia();
            frm.Carga();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bteProveedor.Tag = frm._Be.iid_icod_proveedor;
                bteProveedor.Text = frm._Be.vnombrecompleto;
                vcod_proveedor = frm._Be.vcod_proveedor;
            }
        }


        private void bteProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProveedor();
            txtObservacion.Focus();
        }

        private void bteProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarProveedor();
        }



        //private void nuevo()
        //{
        //    BaseEdit oBase = null;
        //    try
        //    {

        //        using (frmManteListaPreciosDetalle frm = new frmManteListaPreciosDetalle())
        //        {
        //            frm.CargarControles();

        //            frm.SetInsert();
        //            frm.lstDetalle = lstDetalle;
        //            frm.lkpmoneda.EditValue = 4;//dolares
        //            if (frm.ShowDialog() == DialogResult.OK)
        //            {
        //                lstDetalle = frm.lstDetalle;
        //                viewDetalle.RefreshData();
        //                viewDetalle.MoveLast();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (oBase != null)
        //        {
        //            oBase.Focus();
        //            oBase.ErrorText = ex.Message;
        //            oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
        //        }
        //        XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void modificar()
        {
            EListaPrecioDetalle obe = (EListaPrecioDetalle)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;

            int index = viewDetalle.FocusedRowHandle;
            using (frmManteListaPreciosDetalle frm = new frmManteListaPreciosDetalle())
            {
                frm.CargarControles();
                frm.obe = obe;
                frm.lstDetalle = lstDetalle;
                frm.SetModify();
                frm.lkpmoneda.Enabled = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewDetalle.RefreshData();

                    viewDetalle.FocusedRowHandle = index;
                }
            }
        }

        private void eliminar()
        {
            EListaPrecioDetalle obe = (EListaPrecioDetalle)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstDetalle.Remove(obe);
            viewDetalle.RefreshData();
        }

        //private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    nuevo();
        //}

        //private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    modificar();
        //}

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void bteRefreshTipoCambio_Click(object sender, EventArgs e)
        {
            cargar();
        }

        private void exportarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdRuta.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = sfdRuta.FileName;
                if (!fileName.Contains(".xlsx"))
                {
                    grdDetalle.ExportToXlsx(fileName + ".xlsx");
                    System.Diagnostics.Process.Start(fileName + ".xlsx");
                }
                else
                {
                    grdDetalle.ExportToXlsx(fileName);
                    System.Diagnostics.Process.Start(fileName);
                }
                grdDetalle.DataSource = null;
                sfdRuta.FileName = string.Empty;
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }


        string filePath = "";
        private void importarPreciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            if (DialogResult.OK == ofd.ShowDialog(this))
            {
                string Extension = Path.GetExtension(ofd.FileName);

                if (Extension == ".xls")
                {
                    filePath = ofd.FileName;
                    ImportarDatosExcel();
                }
                else
                {
                    ClearLista();
                    //XtraMessageBox.Show("El archivo no coincide con el tipo de archivo seleccionado " + lkpTipoArchivo.Text, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }
        private void ImportarDatosExcel()
        {
            ClearLista();

            DataTable dt;
            OleDbDataAdapter MyDataAdapter;

            string connString = "provider=Microsoft.Jet.OLEDB.4.0;" + @"data source=" + filePath + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection oledbConn = new OleDbConnection(connString);
            try
            {

                MyDataAdapter = new OleDbDataAdapter("SELECT * FROM [ListaPrecio$]", connString);
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
        private void ClearLista()
        {
            lstDetalle.Clear();
            viewDetalle.RefreshData();
        }

        private void FillList(DataTable dt)
        {
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
                        EListaPrecioDetalle obe = new EListaPrecioDetalle();

                        foreach (DataColumn column in dt.Columns)
                        {

                            switch (column.ToString().ToUpper().Trim())
                            {
                                //planilla

                                case "CODPROVEEDOR":
                                    nomC = "CODPROVEEDOR";
                                    obe.prov_vcodigo_prov = row[column].ToString();
                                    break;
                                case "CODIGOPROD":
                                    nomC = "CODIGOPROD";
                                    obe.prdc_vcode_producto = row[column].ToString();
                                    List<EProducto> MlistProd = new BAlmacen().listarProductoXCodigp(Parametros.intEjercicio, obe.prdc_vcode_producto);
                                    if (MlistProd.Count > 0)
                                    {
                                        obe.prdc_icod_producto = Convert.ToInt32(MlistProd[0].prdc_icod_producto);
                                        obe.prdc_vdescripcion_larga = (MlistProd[0].prdc_vdescripcion_larga).ToUpper();
                                        obe.prdc_vAutor = (MlistProd[0].prdc_vAutor).ToUpper();
                                        obe.strEditorial = (MlistProd[0].strEditorial).ToUpper();
                                        obe.lpedid_bExisteCatalogo = true;
                                    }
                                    else
                                    {
                                        obe.lpedid_bExisteCatalogo = false;
                                    }
                                    break;

                                case "DESCRIPCION":
                                    nomC = "DESCRIPCION";
                                    if (obe.prdc_icod_producto == 0)
                                        obe.prdc_vdescripcion_larga = row[column].ToString().ToUpper();
                                    break;

                                case "AUTOR":
                                    nomC = "AUTOR";
                                    if (obe.prdc_icod_producto == 0)
                                        obe.prdc_vAutor = row[column].ToString().ToUpper();
                                    break;

                                case "EDITORIAL":
                                    nomC = "EDITORIAL";
                                    if (obe.prdc_icod_producto == 0)
                                        obe.edit_vdescripcion = row[column].ToString().ToUpper();
                                    break;

                                case "MONEDA":
                                    nomC = "MONEDA";
                                    obe.lpred_icod_moneda = row[column].ToString() == "S/." ? 3 : 4;
                                    obe.lpred_vdescripcion_moneda = row[column].ToString();
                                    break;
                                case "PRECIOLISTA":
                                    nomC = "PRECIOLISTA";
                                    obe.lpred_nprecio_lista = Convert.ToDecimal(row[column]);

                                    break;
                                //----------------------------------------------------------
                                //DEPOSITO
                                case "DESCUENTO":
                                    nomC = "DESCUENTO";
                                    obe.lpred_nperso_desc = Convert.ToDecimal(row[column]);
                                    break;
                                case "PRECIONETO":
                                    nomC = "PRECIONETO";
                                    obe.lpred_nprecio_neto = Convert.ToDecimal(row[column]);
                                    break;
                            }
                        }


                        obe.intUsuario = Valores.intUsuario;
                        obe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                        obe.intTipoOperacion = 1;
                        obe.lpred_sflag_estado = true;
                        lstDetalle.Add(obe);

                    }
                }
                grdDetalle.DataSource = lstDetalle.Where(e => e.prov_vcodigo_prov == vcod_proveedor).ToList().GroupBy(i => i.prdc_vcode_producto).Select(group => group.First()).ToList();

                viewDetalle.RefreshData();

                List<EListaPrecioDetalle> ListProvDist = new List<EListaPrecioDetalle>();
                foreach (var _be in lstDetalle)
                {
                    if (_be.prov_vcodigo_prov != vcod_proveedor)
                    {
                        ListProvDist.Add(_be);
                    }
                }
                if (lstDetalle.Count > 0)
                {
                    btnGuardar.Enabled = true;
                }
                if (ListProvDist.Count > 0)
                {
                    if (XtraMessageBox.Show("¿Desea Exportar los Registros que no Pertenecen a este Proveedor?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ExportarDatos(ListProvDist);
                    }
                }
                XtraMessageBox.Show("Se importaron " + (lstDetalle.Where(e => e.prov_vcodigo_prov == vcod_proveedor).ToList().GroupBy(i => i.prdc_vcode_producto).Select(group => group.First()).ToList().Count()) + " registros", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtTotalRegistro.Text = lstDetalle.Count().ToString();

                txtNuevos.Text = lstDetalle.Where(o => o.lpedid_bExisteCatalogo == false).ToList().Count().ToString();
                txtcatalogo.Text = lstDetalle.Where(o => o.lpedid_bExisteCatalogo == true).ToList().Count().ToString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error Fila: " + contF + "\t Columna: " + nomC, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportarDatos(List<EListaPrecioDetalle> Mlist)
        {
            try
            {
                if (sfdTXT.ShowDialog(this) == DialogResult.OK)
                {
                    string fileName = sfdTXT.FileName;
                    if (!fileName.Contains(".txt"))
                    {
                        ExportarATXT(fileName + ".txt", Mlist);
                        System.Diagnostics.Process.Start(fileName + ".txt");
                    }
                    else
                    {
                        ExportarATXT(fileName, Mlist);
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
        private void ExportarATXT(String ruta, List<EListaPrecioDetalle> Mlist)
        {
            StreamWriter sw = new StreamWriter(ruta);
            string error = string.Empty;
            int cont = 0;
            string columna = string.Empty;
            try
            {
                int totFilas = Mlist.Count;
                foreach (EListaPrecioDetalle item in Mlist)
                {
                    cont++;
                    error = item.prov_vcodigo_prov;
                    columna = "1";
                    sw.Write(item.prov_vcodigo_prov + "|"); // 1
                    columna = "2";
                    sw.Write(item.prdc_vcode_producto + "|"); // 2
                    columna = "3";
                    sw.Write(item.prdc_vdescripcion_larga + "|"); // A: Aptertura M: Mes C:Cierre // 3
                    columna = "4";
                    sw.Write(item.prdc_vAutor + "|"); // 4
                    columna = "5";
                    sw.Write(item.edit_vdescripcion + "|"); // 5


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
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void viewDetalle_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string prdc_icod_producto = View.GetRowCellDisplayText(e.RowHandle, View.Columns["prdc_icod_producto"]);

                if (Convert.ToDecimal(prdc_icod_producto) == 0)
                {
                    e.Appearance.BackColor = Color.LightSalmon;
                }
            }
        }
    }
}