using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
//using SGE.WindowForms.Planillas.Registro_de_Datos;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmIncialesProvPlanillaDet : DevExpress.XtraEditors.XtraForm
    {
        private BSMaintenanceStatus mStatus;
        List<EInicial_Prov_planilla_Det> lstInicialesProvPlanillaDetalle = new List<EInicial_Prov_planilla_Det>();
        public int intIcodFondosPensiones = 0;
        public decimal SumaPorcentaje = 0;
        public frmIncialesProvPlanillaDet()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();


        }
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
            }
        }

        //void Modify(int Cab_icod_correlativo)
        //{
        //    cargar();
        //    int index = lstFondosPensionesConceptos.FindIndex(obe => obe.fdpc_icod_fondo_pension == Cab_icod_correlativo);
        //    viewAlmacen.FocusedRowHandle = index;
        //}

        private void cargar()
        {
            lstInicialesProvPlanillaDetalle = new BPlanillas().ListarInicial_Prov_Planilla_Detalle(intIcodFondosPensiones).Where(q => q.ippd_sflag_estado == true).ToList();
            grdAlmacen.DataSource = lstInicialesProvPlanillaDetalle;
            viewAlmacen.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstInicialesProvPlanillaDetalle.FindIndex(x => x.ippd_icod_inicial_provision_planilla_detalle == intIcod);
            viewAlmacen.FocusedRowHandle = index;
            viewAlmacen.Focus();
        }
        private void nuevo()
        {
            frmRegistroInicialesProvPlanillaDetalle frm = new frmRegistroInicialesProvPlanillaDetalle();
            frm.MiEvento += new frmRegistroInicialesProvPlanillaDetalle.DelegadoMensaje(reload);
            if (lstInicialesProvPlanillaDetalle.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:00}", lstInicialesProvPlanillaDetalle.Max(x => Convert.ToInt32(x.ippd_iid_inicial_provision_planilla_detalle) + 1));
            else
                frm.txtCodigo.Text = "01";
            frm.intIcodFondosPensiones = intIcodFondosPensiones;
            frm.lstInicialesProvPlanillaDetalle = lstInicialesProvPlanillaDetalle;
            //Exporta datos de suma de porcentaje
            //frmRegistroFondosPensiones frm2 = new frmRegistroFondosPensiones();
            //SumaPorcentaje = lstFondosPensionesConceptos.Sum(x =>Convert.ToDecimal(x.fdpd_nporcentaje_concepto));
            //frm2.SumaPorcentaje = SumaPorcentaje;
            //------------
            frm.SetInsert();
            frm.ShowDialog();
            frm.txtMontBasico.Focus();

        }
        private void modificar()
        {
            EInicial_Prov_planilla_Det Obe = (EInicial_Prov_planilla_Det)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmRegistroInicialesProvPlanillaDetalle frm = new frmRegistroInicialesProvPlanillaDetalle();
            frm.MiEvento += new frmRegistroInicialesProvPlanillaDetalle.DelegadoMensaje(reload);
            frm.lstInicialesProvPlanillaDetalle = lstInicialesProvPlanillaDetalle;
            frm.Obe = Obe;
            frm.SetModify();
            frm.setValues();
            frm.ShowDialog();
            frm.txtMontBasico.Focus();
        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {

        }

        private void eliminar()
        {
            try
            {
                EInicial_Prov_planilla_Det Obe = (EInicial_Prov_planilla_Det)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewAlmacen.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar al Personal : " + Obe.strNomyApell + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BPlanillas().eliminarInicial_Prov_Planilla_Detalle(Obe);
                    cargar();
                    if (lstInicialesProvPlanillaDetalle.Count >= index + 1)
                        viewAlmacen.FocusedRowHandle = index;
                    else
                        viewAlmacen.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void buscarCriterio()
        {
            grdAlmacen.DataSource = lstInicialesProvPlanillaDetalle.Where(x =>
                                                   x.ippd_iid_inicial_provision_planilla_detalle.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.strNomyApell.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }



        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }



        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void grdAlmacen_DoubleClick(object sender, EventArgs e)
        {
            //EFondosPensiones Obe = (EFondosPensiones)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            //if (Obe == null)
            //    return;
            //frmRegistroFondosPensiones frm = new frmRegistroFondosPensiones();
            //frm.Obe = Obe;
            //frm.SetCancel();
            //frm.Show();
            //frm.setValues();
        }
        //string filePath = "";
        //private void ImportarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog ofd = new OpenFileDialog();
        //    ofd.FileName = "";
        //    if (DialogResult.OK == ofd.ShowDialog(this))
        //    {
        //        string Extension = Path.GetExtension(ofd.FileName);

        //        if (Extension == ".xls")
        //        {
        //            filePath = ofd.FileName;
        //            ImportarDatosExcel();
        //        }
        //        else
        //        {
        //            ClearLista();

        //        }

        //    }



        //}


        //private void ImportarDatosExcel()
        //{


        //    DataTable dt;
        //    OleDbDataAdapter MyDataAdapter;

        //    string connString = "provider=Microsoft.Jet.OLEDB.4.0;" + @"data source=" + filePath + ";" + "Extended Properties=Excel 8.0;";
        //    OleDbConnection oledbConn = new OleDbConnection(connString);
        //    try
        //    {

        //        MyDataAdapter = new OleDbDataAdapter("SELECT * FROM [Hoja1$]", connString);
        //        dt = new DataTable();
        //        MyDataAdapter.Fill(dt);               
        //        FillList(dt);


        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //    finally
        //    {
        //        oledbConn.Close();
        //    }
        //}        

        //private void FillList(DataTable dt)
        //{
        //    int contF = 0;
        //    string nomC = String.Empty;
        //    try
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            int b = dt.Rows.Count;
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                contF++;
        //                ETablaPlanillaDetalle obe = new ETablaPlanillaDetalle();
        //                foreach (DataColumn column in dt.Columns)
        //                {

        //                    switch (column.ToString().ToUpper().Trim())
        //                    {
        //                        //TablaPlanilla

        //                        case "CÓDIGO":
        //                            nomC = "CÓDIGO";
        //                            obe.tbpd_iid_vcodigo_tabla_planilla_detalle = row[column].ToString();
        //                            break;
        //                        case "DESCRIPCIÓN":
        //                            nomC = "DESCRIPCIÓN";
        //                            obe.tbpd_vdescripcion_detalle = row[column].ToString();
        //                            break;

        //                        case "ABREVIADO":
        //                            nomC = "ABREVIADO";
        //                            obe.tbpd_vabreviado_detalle = row[column].ToString();
        //                            break;
        //                        case "OTROS":
        //                            nomC = "OTROS";
        //                            obe.tbpd_votros_datos = row[column].ToString();                                    
        //                            break;

        //                    }

        //                }
        //                obe.tbpd_flag_estado = true;
        //                obe.tbpc_icod_tabla_planilla = intIcodFondosPensiones;
        //                obe.intUsuario = Valores.intUsuario;
        //                obe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
        //                lstTablaPlanillaDetalle.Add(obe);

        //                obe.tbpd_icod_tabla_planilla_detalle = new BPlanillas().insertarTablaPlanillaDetalle(obe);


        //            }
        //        }

        //        grdAlmacen.DataSource = lstTablaPlanillaDetalle;
        //        viewAlmacen.RefreshData();



        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show("Error Fila: " + contF + "\t Columna: " + nomC, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}



    }
}