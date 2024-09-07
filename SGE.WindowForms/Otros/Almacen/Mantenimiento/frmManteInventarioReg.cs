using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmManteInventarioReg : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteNotaIngreso));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        /*----------------------------------------------------*/
        public EInventarioCab oBe = new EInventarioCab();
        /*----------------------------------------------------*/
        List<EInventarioDet> lstDetalle = new List<EInventarioDet>();
        List<EInventarioDet> lstDelete = new List<EInventarioDet>();
        /*----------------------------------------------------*/
        public List<ENotaIngreso> lstCabecerasNI = new List<ENotaIngreso>();
        /*----------------------------------------------------*/
        bool flag_close = false;

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
        public frmManteInventarioReg()
        {
            InitializeComponent();
        }

        private void FrmManteNotaIngreso_Load(object sender, EventArgs e)
        {
            Carga();
            BSControls.LoaderLook(lkpTipoInventario, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoInventario), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaSituacionInventario), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            if (Parametros.intEjercicio == DateTime.Now.Year)
                dteFecha.EditValue = DateTime.Now;
            else
                dteFecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
            lstDetalle = new BAlmacen().listarInventarioFisicoDet(oBe.invc_icod_inventario);
            grdDetalle.DataSource = lstDetalle;

        }

        private void Carga()
        {
            if (Status != BSMaintenanceStatus.CreateNew)
                lstDetalle = new BAlmacen().listarInventarioFisicoDet(oBe.invc_icod_inventario);
            grdDetalle.DataSource = lstDetalle;
        }

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtObservaciones.Enabled = !Enabled;
            //lkpTipoInventario.Enabled = !Enabled;            
            dteFecha.Enabled = !Enabled;
            //mnuNotaIngresoDetalle.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                dteFecha.Enabled = Enabled;
            }

        }

        public void setValues()
        {
            txtCorrelativo.Text = oBe.invc_iid_correlativo.ToString();
            lkpTipoInventario.EditValue = oBe.tablc_iid_tipo_inventario;
            lkpSituacion.EditValue = oBe.tablc_iid_situacion;
            dteFecha.EditValue = oBe.invc_sfecha_inventario;
            txtObservaciones.Text = oBe.invc_vobservaciones;
            bteAlmacen.Tag = oBe.almac_icod_almacen;
            bteAlmacen.Text = oBe.strAlmacen;
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

        public void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (lstDetalle.Count == 0)
                {
                    throw new ArgumentException("No se ha generado una lista para el inventario");
                }
                if (Convert.ToDateTime(dteFecha.EditValue).Year != Parametros.intEjercicio)
                {
                    oBase = dteFecha;
                    throw new ArgumentException("La fecha seleccionada no esta dentro del año de ejercicio" + Parametros.intEjercicio);
                }

                /*---------------------------------------------------------*/
                oBe.invc_iid_correlativo = Convert.ToInt32(txtCorrelativo.Text);
                oBe.invc_sfecha_inventario = Convert.ToDateTime(dteFecha.EditValue);
                oBe.tablc_iid_situacion = Parametros.intInventarioRegistrado;
                oBe.tablc_iid_tipo_inventario = Convert.ToInt32(lkpTipoInventario.EditValue);
                oBe.invc_vobservaciones = txtObservaciones.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                /*---------------------------------------------------------*/

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    //oBe.invc_icod_inventario = new BAlmacen().insertarInventarioFisico(oBe, lstDetalle);
                }
                else
                {
                    viewDetalle.MoveLast();
                    viewDetalle.MoveFirst();

                    new BAlmacen().modificarInventarioFisico(oBe, lstDetalle, lstDelete, 2);
                }
                /*-------------------------------------------------*/
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    flag_close = true;
                    this.MiEvento(oBe.invc_icod_inventario);
                    imprimir();
                    this.Close();
                }
            }
        }
        private void imprimir()
        {
            //if (XtraMessageBox.Show("¿Desea imprimir la nota de ingreso?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            //    return;
            //var lstDetalle = new BAlmacen().listarNotaIngresoDetalle(oBe.ningc_icod_nota_ingreso);
            //rptNotaIngreso rpt = new rptNotaIngreso();
            //rpt.cargar(String.Format("NOTA DE INGRESO N° {0}", oBe.ningc_numero_nota_ingreso), oBe.strAlmacen, lstDetalle, oBe);

        }




        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }
        private void Imprimir()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            //ENotaIngreso oBe = new ENotaIngreso();
            //Obl = new BNotaIngreso();
            try
            {

            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Flag = false;
                }
            }
            finally

            {
                if (Flag)
                {

                    //this.MiEvento();
                    this.Close();
                }
            }

        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void generarListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var lstProductos = new BAlmacen().listarProducto(Parametros.intEjercicio).Where(x => x.prdc_isituacion && x.tarec_icorrelativo_registro_tipo != 2).ToList();
            //int cont = 0;
            //lstProductos.ForEach(x => 
            //{
            //    cont += 1;
            //    EInventarioDet obe = new EInventarioDet();
            //    obe.invd_inro_item = cont;
            //    obe.prdc_icod_producto = x.prdc_icod_producto;
            //    obe.invd_sis_stock = x.dblStockActual;
            //    obe.invd_icantidad = 0;
            //    obe.strCodProducto = x.prdc_vcode_producto;
            //    obe.strDesProducto = x.prdc_vdescripcion_larga;
            //    obe.strFamilia = x.strDesFamilia;
            //    obe.strSubFamilia = x.strDesSubFamilia;
            //    obe.strUnidadMedida = x.strUnidadMedida;
            //    obe.intUsuario = Valores.intUsuario;
            //    obe.strPc = WindowsIdentity.GetCurrent().Name;
            //    lstDetalle.Add(obe);
            //});

            //grdDetalle.DataSource = lstDetalle;
            //viewDetalle.RefreshData();
        }

        private void borrarListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstDetalle.Clear();
            viewDetalle.RefreshData();
        }

        private void frmManteInventarioReg_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!flag_close)
            {
                if (XtraMessageBox.Show("No se guardaran los datos ingresados ¿Esta seguro de salir del formulario?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    flag_close = true;
                    Close();
                }
                else
                    e.Cancel = true;
            }

        }

        private void viewDetalle_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            EInventarioDet Obe = (EInventarioDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (oBe != null)
            {
                Obe.intOperacion = 2;
            }
        }
        string filePath = "";
        private void exportarExcelToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void ImportarDatosExcelNuevo()
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
                FillListNuevo(dt);


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

                foreach (var _BEE in lstDetalle)
                {
                    foreach (var _baux in lstDetalleaUX)
                    {
                        if (_baux.strCodProducto.Trim() == _BEE.strCodProducto.Trim())
                        {
                            _BEE.invd_icantidad = _baux.invd_icantidad;
                            if (_BEE.invd_icantidad != 0)
                            {
                                _BEE.intOperacion = 2;
                            }

                        }
                    }

                }

                List<EInventarioDet> MlistInexistentes = new List<EInventarioDet>();
                foreach (var _b in lstDetalleaUX)
                {
                    if (_b.strCodProducto.Trim() != "")
                    {
                        int contador = lstDetalle.Count(ob => ob.strCodProducto == _b.strCodProducto);
                        if (contador == 0)
                        {
                            MlistInexistentes.Add(_b);
                        }
                    }
                }

                if (MlistInexistentes.Count > 0)
                {

                    if (XtraMessageBox.Show("Existen productos que no estan dentro del catálogo de productos ¿Desea importarlos en un documento TXT?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        if (sfdTXT.ShowDialog(this) == DialogResult.OK)
                        {
                            string fileName = sfdTXT.FileName;
                            if (!fileName.Contains(".txt"))
                            {
                                ExportarATXT(fileName + ".txt", MlistInexistentes);
                                System.Diagnostics.Process.Start(fileName + ".txt");
                            }
                            else
                            {
                                ExportarATXT(fileName, MlistInexistentes);
                                System.Diagnostics.Process.Start(fileName);
                            }
                        }
                        sfdTXT.FileName = string.Empty;


                    }
                }



                viewDetalle.RefreshData();



            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error Fila: " + contF + "\t Columna: " + nomC, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportarATXT(String ruta, List<EInventarioDet> Lista)
        {
            StreamWriter sw = new StreamWriter(ruta);
            string error = string.Empty;
            int cont = 0;
            string columna = string.Empty;
            try
            {
                int totFilas = Lista.Count;
                foreach (EInventarioDet item in Lista)
                {
                    cont++;
                    error = item.strCodProducto;
                    columna = "1";
                    sw.Write(item.strCodProducto + " |"); // 1
                    columna = "2";
                    sw.Write(item.invd_icantidad + "|"); // 2

                    // 30 NO SE APLICA

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
        private void FillListNuevo(DataTable dt)
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

                foreach (var _BEE in lstDetalle)
                {
                    foreach (var _baux in lstDetalleaUX)
                    {
                        if (_baux.strCodProducto.Trim() == _BEE.strCodProducto.Trim())
                        {
                            _BEE.invd_icantidad = _baux.invd_icantidad;
                            if (_BEE.invd_icantidad != 0)
                            {
                                _BEE.intOperacion = 2;
                            }

                        }
                    }

                }


                viewDetalle.RefreshData();



            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error Fila: " + contF + "\t Columna: " + nomC, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}