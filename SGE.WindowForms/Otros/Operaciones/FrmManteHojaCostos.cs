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

namespace SGE.WindowForms.Otros.Operaciones
{
    public partial class FrmManteHojaCostos : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteHojaCostos));

        public EHojaCostos oBe = new EHojaCostos();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        public int Proyecto = 0;
        public string Cod1 = "";
        List<ESuministros> lstSuministros = new List<ESuministros>();
        List<ESuministros> lstDeleteSuministros = new List<ESuministros>();
        List<EServicios> lstServicios = new List<EServicios>();
        List<EServicios> lstDeleteServicios = new List<EServicios>();
        List<EGastosAdministrativos> lstGastosAdministrativos = new List<EGastosAdministrativos>();
        List<EGastosAdministrativos> lstDeleteGastosAdministrativos = new List<EGastosAdministrativos>();
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
        public void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtNumero.Enabled = Enabled;
                lkpMoneda.Enabled = Enabled;

            }
        }

        public void setValues()
        {
            txtNumero.Text = oBe.hjcc_vnumero_hoja_costo;
            dteFecha.EditValue = oBe.hjcc_sfecha_hoja_costo;
            lkpSituacion.EditValue = oBe.tablc_iid_situacion_hc;
            bteProyecto.Tag = oBe.pryc_icod_proyecto;
            bteProyecto.Text = oBe.pryc_icod_proyecto.ToString();
            lkpMoneda.EditValue = oBe.tablc_iid_tipo_moneda;
            txtTipoCambio.Text = oBe.hjcc_ntipo_cambio.ToString();
            txtDescripcion.Text = oBe.hjcc_vdescripcion;
            bteProyecto.Text = string.Format("{0:00000}", oBe.pryc_icod_proyecto);
            txtCC.Text = oBe.CentroCostos;
            txtDescProyecto.Text = oBe.pryc_vdescripcion;
            txtTotalSoles.Text = oBe.hjcc_ntotal_soles.ToString();
            txtTotalDolares.Text = oBe.hjcc_ntotal_dolares.ToString();

            lstSuministros = new BVentas().listarHojaCostosSuministros(oBe.hjcc_icod_hoja_costo);
            grdSuministros.DataSource = lstSuministros;

            lstServicios = new BVentas().listarHojaCostosServicios(oBe.hjcc_icod_hoja_costo);
            grdServicios.DataSource = lstServicios;

            lstGastosAdministrativos = new BVentas().listarHojaCostosGastosAdministrativos(oBe.hjcc_icod_hoja_costo);
            grdGastosAdministrativos.DataSource = lstGastosAdministrativos;


        }

        public FrmManteHojaCostos()
        {
            InitializeComponent();
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetModify()
        {
            lkpMoneda.EditValue = oBe.tablc_iid_tipo_moneda;
            Status = BSMaintenanceStatus.ModifyCurrent;
            setValues();
        }

        public void SetCancel()
        {
            lkpMoneda.EditValue = oBe.tablc_iid_tipo_moneda;
            Status = BSMaintenanceStatus.View;
            setValues();
        }

        private void cargar()
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                setFecha(dteFecha);
            }
        }
        private void setFecha(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
        }
        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (Convert.ToInt32(txtNumero.Text) == 0)
                {
                    oBase = txtNumero;
                    throw new ArgumentException("Ingrese Nro. de Hoja Costo");
                }
                if (Convert.ToInt32(bteProyecto.Tag) == 0)
                {
                    oBase = bteProyecto;
                    throw new ArgumentException("Seleccione Proyecto");
                }
                if ((txtDescripcion.Text) == "")
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese Descripcion Hoja Costo");
                }
                if (Convert.ToDecimal(txtTipoCambio.Text) == 0)
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese Tipo de Cambio");
                }
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    List<EHojaCostos> Lstver = new BVentas().getHCCabVerificarNumero(String.Format("{0}", txtNumero.Text), Parametros.intEjercicio);
                    if (Lstver.Count > 0)
                    {
                        oBase = txtNumero;
                        throw new ArgumentException("El Número " + String.Format("{0}", txtNumero.Text) + " N° H/C: Ya Existia");

                    }
                }
                oBe.hjcc_vnumero_hoja_costo = txtNumero.Text;
                oBe.hjcc_sfecha_hoja_costo = Convert.ToDateTime(dteFecha.Text);
                oBe.hjcc_vdescripcion = txtDescripcion.Text;
                oBe.pryc_icod_proyecto = Convert.ToInt32(bteProyecto.Tag);
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                oBe.tablc_iid_situacion_hc = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.hjcc_ntipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.hjcc_flag_estado = true;
                oBe.pryc_icod_proyecto_2 = Proyecto;
                oBe.hjcc_ntotal_soles = Convert.ToDecimal(txtTotalSoles.Text);
                oBe.hjcc_ntotal_dolares = Convert.ToDecimal(txtTotalDolares.Text);
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.hjcc_icod_hoja_costo = new BVentas().insertarHojaCostos(oBe, lstSuministros, lstServicios, lstGastosAdministrativos);
                }
                else
                {
                    new BVentas().modificarHojaCostos(oBe, lstSuministros, lstDeleteSuministros, lstServicios, lstDeleteServicios, lstGastosAdministrativos, lstDeleteGastosAdministrativos);
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
                    MiEvento(oBe.hjcc_icod_hoja_costo);
                    Close();
                }
            }
        }

        private void FrmManteFactura_Load(object sender, EventArgs e)
        {
            cargar();

        }
        public void CargarControles()
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(72), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda).Where(x => x.tarec_iid_tabla_registro != 5).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }


        private void txtSerie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void grdFactura_KeyUp(object sender, KeyEventArgs e)
        {

        }
        private void bteProyecto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProyecto();
        }
        private void ListarProyecto()
        {
            using (frmListarProyectos frm = new frmListarProyectos())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteProyecto.Tag = frm._Be.pryc_icod_proyecto;
                    bteProyecto.Text = frm._Be.pryc_vcorrelativo;
                    txtDescProyecto.Text = frm._Be.pryc_vdescripcion;
                    txtCC.Text = frm._Be.CentroCosto;
                }
            }

        }

        private void TotalSoles()
        {
            decimal TotalSoles = 0;
            decimal Convertir = 0;
            decimal TotalSolesConvertir = 0;
            //Convertir = lstHojaCostosRubros.Where(x=> x.tablc_icod_tipo_moneda == 4).Sum(x=> x.hjcd3_nmonto_real);
            TotalSolesConvertir = Convertir * Convert.ToDecimal(txtTipoCambio.Text);
            //TotalSoles = lstHojaCostosRubros.Where(x => x.tablc_icod_tipo_moneda == 3).Sum(x => x.hjcd3_nmonto_real);
            txtTotalSoles.Text = (TotalSoles + TotalSolesConvertir).ToString();
        }
        private void TotalDolares()
        {
            decimal TotalDolares = 0;
            decimal Convertir = 0;
            decimal TotalDolaresConvertir = 0;
            //Convertir = lstHojaCostosRubros.Where(x => x.tablc_icod_tipo_moneda == 3).Sum(x => x.hjcd3_nmonto_real);
            TotalDolaresConvertir = Convertir / Convert.ToDecimal(txtTipoCambio.Text);
            //TotalDolares = lstHojaCostosRubros.Where(x => x.tablc_icod_tipo_moneda == 4).Sum(x => x.hjcd3_nmonto_real);
            txtTotalDolares.Text = (TotalDolares + TotalDolaresConvertir).ToString();
        }
        private bool VerifyFields()
        {
            BaseEdit oBase = null;
            bool flag = true;
            try
            {
                if (Convert.ToDecimal(txtTipoCambio.Text) == 0)
                {
                    oBase = txtTipoCambio;
                    throw new ArgumentException("Ingrese un tipo de cambio");
                }

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
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = false;
            }
            return flag;

        }

        string filePath = "";
        private void ImportarSuministros_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            if (DialogResult.OK == ofd.ShowDialog(this))
            {
                string Extension = Path.GetExtension(ofd.FileName);

                if (Extension == ".xlsx")
                {
                    filePath = ofd.FileName;
                    ImportarDatosExcelSuministros();
                }
                else
                {
                    //ClearLista();
                    //XtraMessageBox.Show("El archivo no coincide con el tipo de archivo seleccionado " + lkpTipoArchivo.Text, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void ImportarDatosExcelSuministros()
        {
            //ClearLista();

            DataTable dt;
            OleDbDataAdapter MyDataAdapter;

            string connString = "provider=Microsoft.ACE.OLEDB.12.0;" + @"data source=" + filePath + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection oledbConn = new OleDbConnection(connString);
            try
            {

                MyDataAdapter = new OleDbDataAdapter("SELECT * FROM [Suministros$]", connString);
                dt = new DataTable();
                MyDataAdapter.Fill(dt);
                FillListSuministros(dt);


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
        private void FillListSuministros(DataTable dt)
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
                        ESuministros obe = new ESuministros();
                        foreach (DataColumn column in dt.Columns)
                        {

                            switch (column.ToString().ToUpper().Trim())
                            {
                                //TablaPlanilla

                                case "PARTIDA":
                                    nomC = "PARTIDA";
                                    obe.sumd_vpartida = row[column].ToString();
                                    break;
                                case "DESCRIPCIÓN":
                                    nomC = "DESCRIPCIÓN";
                                    obe.sumd_vdescripcion = row[column].ToString();
                                    break;
                                case "UM":
                                    nomC = "UM";
                                    obe.sumd_vunidad_medida = row[column].ToString();
                                    break;
                                case "CANTIDAD":
                                    nomC = "CANTIDAD";
                                    obe.sumd_icantidad = Convert.ToInt32(row[column]);
                                    break;
                                case "PRECIO_UNIT":
                                    nomC = "PRECIO_UNIT";
                                    obe.sumd_precio_unitario = Convert.ToDecimal(row[column]);
                                    break;
                                case "TOTAL":
                                    nomC = "TOTAL";
                                    obe.sumd_precio_total = Convert.ToDecimal(row[column]);
                                    break;

                            }

                        }
                        obe.intUsuario = Valores.intUsuario;
                        obe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                        lstSuministros.Add(obe);


                    }
                }
                grdSuministros.DataSource = lstSuministros;
                viewSuministros.RefreshData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error Fila: " + contF + "\t Columna: " + nomC, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImportarServicios_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            if (DialogResult.OK == ofd.ShowDialog(this))
            {
                string Extension = Path.GetExtension(ofd.FileName);

                if (Extension == ".xlsx")
                {
                    filePath = ofd.FileName;
                    ImportarDatosExcelServicios();
                }
                else
                {
                    //ClearLista();
                    //XtraMessageBox.Show("El archivo no coincide con el tipo de archivo seleccionado " + lkpTipoArchivo.Text, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void ImportarDatosExcelServicios()
        {
            //ClearLista();

            DataTable dt;
            OleDbDataAdapter MyDataAdapter;

            string connString = "provider=Microsoft.ACE.OLEDB.12.0;" + @"data source=" + filePath + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection oledbConn = new OleDbConnection(connString);
            try
            {

                MyDataAdapter = new OleDbDataAdapter("SELECT * FROM [Servicios$]", connString);
                dt = new DataTable();
                MyDataAdapter.Fill(dt);
                FillListServicios(dt);


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
        private void FillListServicios(DataTable dt)
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
                        EServicios obe = new EServicios();
                        foreach (DataColumn column in dt.Columns)
                        {

                            switch (column.ToString().ToUpper().Trim())
                            {
                                //TablaPlanilla

                                case "DESCRIPCIÓN":
                                    nomC = "DESCRIPCIÓN";
                                    obe.servd_vdescripcion = row[column].ToString();
                                    break;
                                case "UM":
                                    nomC = "UM";
                                    obe.servd_vunidad_medida = row[column].ToString();
                                    break;
                                case "CANTIDAD":
                                    nomC = "CANTIDAD";
                                    obe.servd_icantidad = Convert.ToInt32(row[column]);
                                    break;
                                case "PRECIO_UNIT":
                                    nomC = "PRECIO_UNIT";
                                    obe.servd_precio_unitario = Convert.ToDecimal(row[column]);
                                    break;
                                case "TOTAL":
                                    nomC = "TOTAL";
                                    obe.servd_precio_total = Convert.ToDecimal(row[column]);
                                    break;

                            }

                        }
                        obe.intUsuario = Valores.intUsuario;
                        obe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                        lstServicios.Add(obe);


                    }
                }
                grdServicios.DataSource = lstServicios;
                viewServicios.RefreshData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error Fila: " + contF + "\t Columna: " + nomC, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImportarGastosAdministrativos_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            if (DialogResult.OK == ofd.ShowDialog(this))
            {
                string Extension = Path.GetExtension(ofd.FileName);

                if (Extension == ".xlsx")
                {
                    filePath = ofd.FileName;
                    ImportarDatosExcelGastosAdministrativos();
                }
                else
                {
                    //ClearLista();
                    //XtraMessageBox.Show("El archivo no coincide con el tipo de archivo seleccionado " + lkpTipoArchivo.Text, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void ImportarDatosExcelGastosAdministrativos()
        {
            //ClearLista();

            DataTable dt;
            OleDbDataAdapter MyDataAdapter;

            string connString = "provider=Microsoft.ACE.OLEDB.12.0;" + @"data source=" + filePath + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection oledbConn = new OleDbConnection(connString);
            try
            {

                MyDataAdapter = new OleDbDataAdapter("SELECT * FROM [GastosAdministrativos$]", connString);
                dt = new DataTable();
                MyDataAdapter.Fill(dt);
                FillListGastosAdministrativos(dt);


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
        private void FillListGastosAdministrativos(DataTable dt)
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
                        EGastosAdministrativos obe = new EGastosAdministrativos();
                        foreach (DataColumn column in dt.Columns)
                        {

                            switch (column.ToString().ToUpper().Trim())
                            {
                                //TablaPlanilla

                                case "PARTIDA":
                                    nomC = "PARTIDA";
                                    obe.gadm_vpartida = row[column].ToString();
                                    break;
                                case "DESCRIPCIÓN":
                                    nomC = "DESCRIPCIÓN";
                                    obe.gadm_vdescripcion = row[column].ToString();
                                    break;
                                case "UM":
                                    nomC = "UM";
                                    obe.gadm_vunidad_medida = row[column].ToString();
                                    break;
                                case "CANTIDAD":
                                    nomC = "CANTIDAD";
                                    obe.gadm_icantidad = Convert.ToInt32(row[column]);
                                    break;
                                case "PRECIO_UNIT":
                                    nomC = "PRECIO_UNIT";
                                    obe.gadm_precio_unitario = Convert.ToDecimal(row[column]);
                                    break;
                                case "TOTAL":
                                    nomC = "TOTAL";
                                    obe.gadm_precio_total = Convert.ToDecimal(row[column]);
                                    break;

                            }

                        }
                        obe.intUsuario = Valores.intUsuario;
                        obe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                        lstGastosAdministrativos.Add(obe);


                    }
                }
                grdGastosAdministrativos.DataSource = lstGastosAdministrativos;
                viewGastosAdministrativos.RefreshData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error Fila: " + contF + "\t Columna: " + nomC, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NuevoSuministros_Click(object sender, EventArgs e)
        {
            using (frmManteHojaCostosSuministros frm = new frmManteHojaCostosSuministros())
            {
                frm.SetInsert();
                frm.lstHojaCostosSuministros = lstSuministros;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstSuministros = frm.lstHojaCostosSuministros;
                    grdSuministros.DataSource = lstSuministros;
                    viewSuministros.RefreshData();
                }
            }
        }
        private void ModificarSuministros_Click(object sender, EventArgs e)
        {
            ESuministros obe = (ESuministros)viewSuministros.GetRow(viewSuministros.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteHojaCostosSuministros frm = new frmManteHojaCostosSuministros())
            {
                frm.Obe = obe;
                frm.lstHojaCostosSuministros = lstSuministros;
                frm.SetModify();
                frm.setValues();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstSuministros = frm.lstHojaCostosSuministros;
                    grdSuministros.DataSource = lstSuministros;
                    viewSuministros.RefreshData();
                }
            }
        }
        private void EliminarSuministros_Click(object sender, EventArgs e)
        {
            ESuministros obe = (ESuministros)viewSuministros.GetRow(viewSuministros.FocusedRowHandle);
            if (obe == null)
                return;
            lstDeleteSuministros.Add(obe);
            lstSuministros.Remove(obe);
            viewSuministros.RefreshData();
        }

        private void NuevoServicios_Click(object sender, EventArgs e)
        {
            using (frmManteHojaCostosServicios frm = new frmManteHojaCostosServicios())
            {
                frm.SetInsert();
                frm.lstHojaCostosServicios = lstServicios;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstServicios = frm.lstHojaCostosServicios;
                    grdServicios.DataSource = lstServicios;
                    viewServicios.RefreshData();
                }
            }
        }
        private void ModificarServicios_Click(object sender, EventArgs e)
        {
            EServicios obe = (EServicios)viewServicios.GetRow(viewServicios.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteHojaCostosServicios frm = new frmManteHojaCostosServicios())
            {
                frm.Obe = obe;
                frm.lstHojaCostosServicios = lstServicios;
                frm.SetModify();
                frm.setValues();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstServicios = frm.lstHojaCostosServicios;
                    grdServicios.DataSource = lstServicios;
                    viewServicios.RefreshData();
                }
            }
        }
        private void EliminarServicios_Click(object sender, EventArgs e)
        {
            EServicios obe = (EServicios)viewServicios.GetRow(viewServicios.FocusedRowHandle);
            if (obe == null)
                return;
            lstDeleteServicios.Add(obe);
            lstServicios.Remove(obe);
            viewServicios.RefreshData();
        }

        private void NuevoGastosAdministrativos_Click(object sender, EventArgs e)
        {
            using (frmManteHojaCostosGastosAdministrativos frm = new frmManteHojaCostosGastosAdministrativos())
            {
                frm.SetInsert();
                frm.lstHojaCostosGastosAdministrativos = lstGastosAdministrativos;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstGastosAdministrativos = frm.lstHojaCostosGastosAdministrativos;
                    grdGastosAdministrativos.DataSource = lstGastosAdministrativos;
                    viewGastosAdministrativos.RefreshData();
                }
            }
        }
        private void ModificarGastosAdministrativos_Click(object sender, EventArgs e)
        {
            EGastosAdministrativos obe = (EGastosAdministrativos)viewGastosAdministrativos.GetRow(viewGastosAdministrativos.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteHojaCostosGastosAdministrativos frm = new frmManteHojaCostosGastosAdministrativos())
            {
                frm.Obe = obe;
                frm.lstHojaCostosGastosAdministrativos = lstGastosAdministrativos;
                frm.SetModify();
                frm.setValues();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstGastosAdministrativos = frm.lstHojaCostosGastosAdministrativos;
                    grdGastosAdministrativos.DataSource = lstGastosAdministrativos;
                    viewGastosAdministrativos.RefreshData();
                }
            }
        }
        private void EliminarGastosAdministrativos_Click(object sender, EventArgs e)
        {
            EGastosAdministrativos obe = (EGastosAdministrativos)viewGastosAdministrativos.GetRow(viewGastosAdministrativos.FocusedRowHandle);
            if (obe == null)
                return;
            lstDeleteGastosAdministrativos.Add(obe);
            lstGastosAdministrativos.Remove(obe);
            viewGastosAdministrativos.RefreshData();
        }


    }
}