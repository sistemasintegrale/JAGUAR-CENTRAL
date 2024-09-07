using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Operaciones
{
    public partial class FrmManteConsultarHojaCostos : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteConsultarHojaCostos));
        public EHojaCostos oBe = new EHojaCostos();
        EHojaCostosConceptos oBeC = new EHojaCostosConceptos();
        public EhojaCostosSubConceptos oBeSC = new EhojaCostosSubConceptos();
        public EHojaCostosRubros oBeR = new EHojaCostosRubros();

        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        List<EHojaCostosConceptos> lstHojaCostosConceptos = new List<EHojaCostosConceptos>();
        List<EhojaCostosSubConceptos> lstHojaCostosSubConcepto = new List<EhojaCostosSubConceptos>();
        List<EHojaCostosRubros> lstHojaCostosRubros = new List<EHojaCostosRubros>();
        public string Cod1 = "";
        public string Cod2 = "";
        public string Cod3 = "";
        public string CodCompleto = "";
        List<EHojaCostosConceptos> lstDeleteHCConceptos = new List<EHojaCostosConceptos>();
        List<EhojaCostosSubConceptos> lstDeleteHCSubConceptos = new List<EhojaCostosSubConceptos>();
        List<EHojaCostosRubros> lstDeleteHCRubros = new List<EHojaCostosRubros>();
        public int Proyecto = 0;

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


            lstHojaCostosConceptos = new BVentas().listarHojaCostosConceptos(oBe.hjcc_icod_hoja_costo);
            grConceptos.DataSource = lstHojaCostosConceptos;

            lstHojaCostosSubConcepto = new BVentas().listarHojaCostosSubConceptos(oBe.hjcc_icod_hoja_costo);
            grdSubConceptos.DataSource = lstHojaCostosSubConcepto;


            lstHojaCostosRubros = new BVentas().listarHojaCostosRubros(oBe.hjcc_icod_hoja_costo);
            grdRubros.DataSource = lstHojaCostosRubros;

            CargarConceptos();
            cargarSubConceptos();

        }

        public FrmManteConsultarHojaCostos()
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
            grConceptos.DataSource = lstHojaCostosConceptos;


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
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    //oBe.hjcc_icod_hoja_costo = new BVentas().insertarHojaCostos(oBe, lstHojaCostosConceptos, lstHojaCostosSubConcepto);
                }
                else
                {
                    //new BVentas().modificarHojaCostos(oBe, lstHojaCostosConceptos, lstDeleteHCConceptos, lstHojaCostosSubConcepto,lstDeleteHCSubConceptos);
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
            if (e.KeyCode == Keys.F7)
                NuevoConcepto_Click(null, null);
            if (e.KeyCode == Keys.F5)
                ModificarConcepto_Click(null, null);
            if (e.KeyCode == Keys.F9)
                EliminarConcepto_Click(null, null);
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
        private void viewConceptos_Click(object sender, EventArgs e)
        {
            //CargarConceptos();
            //cargarSubConceptos();
        }
        private void viewSubConceptos_Click(object sender, EventArgs e)
        {
            //cargarSubConceptos();
        }

        private void viewConceptos_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            CargarConceptos();
            cargarSubConceptos();
        }

        private void viewSubConceptos_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            cargarSubConceptos();

        }
        private void CargarConceptos()
        {
            EHojaCostosConceptos Obe = (EHojaCostosConceptos)viewConceptos.GetRow(viewConceptos.FocusedRowHandle);
            if (Obe != null)
            {
                grdSubConceptos.DataSource = lstHojaCostosSubConcepto.Where(x => x.hjcc1_iiten == Obe.hjcd1_vcodigo_concepto_hc).ToList();
                if (lstHojaCostosSubConcepto.Where(x => x.hjcc1_iiten == Obe.hjcd1_vcodigo_concepto_hc).ToList().Count() == 0)
                {
                    grdRubros.DataSource = lstHojaCostosRubros.Where(x => x.hjcd2_iitem == "0").ToList();
                }
            }
        }
        private void cargarSubConceptos()
        {
            EhojaCostosSubConceptos Obe = (EhojaCostosSubConceptos)viewSubConceptos.GetRow(viewSubConceptos.FocusedRowHandle);
            if (Obe != null)
            {
                grdRubros.DataSource = lstHojaCostosRubros.Where(x => x.hjcd2_iitem == Obe.hjcd2_vcodigo_concepto_hc).ToList();
            }
        }

        private void NuevoConcepto_Click(object sender, EventArgs e)
        {
            using (frmManteHojaCostosConceptos frm = new frmManteHojaCostosConceptos())
            {
                frm.SetInsert();
                frm.lstHojaCostosConceptos = lstHojaCostosConceptos;
                if (lstHojaCostosConceptos.Count == 0)
                {
                    frm.txtCodigo.Text = "01";
                }
                else
                {
                    frm.txtCodigo.Text = String.Format("{0:00}", (lstHojaCostosConceptos.Max(ob => Convert.ToInt32(ob.hjcd1_vcodigo_concepto_hc)) + 1));
                }
                Cod1 = frm.txtCodigo.Text;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstHojaCostosConceptos = frm.lstHojaCostosConceptos.OrderBy(E => E.hjcd1_vcodigo_concepto_hc).ToList();
                    lstHojaCostosSubConcepto.Where(x => x.hjcd2_vcodigo_concepto_hc == "01").ToList();
                    viewConceptos.RefreshData();
                    grConceptos.DataSource = lstHojaCostosConceptos;
                }
            }
        }

        private void ModificarConcepto_Click(object sender, EventArgs e)
        {
            EHojaCostosConceptos obe = (EHojaCostosConceptos)viewConceptos.GetRow(viewConceptos.FocusedRowHandle);
            if (obe == null)
                return;
            int index = viewConceptos.FocusedRowHandle;
            using (frmManteHojaCostosConceptos frm = new frmManteHojaCostosConceptos())
            {
                frm.Obe = obe;
                frm.lstHojaCostosConceptos = lstHojaCostosConceptos;
                frm.SetModify();
                frm.setValues();
                frm.txtCodigo.Text = String.Format("{0:00}", Convert.ToInt32(obe.hjcd1_vcodigo_concepto_hc));
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstHojaCostosConceptos = frm.lstHojaCostosConceptos.OrderBy(E => E.hjcd1_vcodigo_concepto_hc).ToList();
                    grConceptos.RefreshDataSource();
                    viewSubConceptos.MoveLast();
                    viewConceptos.FocusedRowHandle = index;
                }
            }
        }
        private void EliminarConcepto_Click(object sender, EventArgs e)
        {
            //EHojaCostosConceptos obe = (EHojaCostosConceptos)viewConceptos.GetRow(viewConceptos.FocusedRowHandle);
            //if (obe == null)
            //    return;
            //lstDeleteHCConceptos.Add(obe);
            //lstHojaCostosConceptos.Remove(obe);
            //viewConceptos.RefreshData();
            EHojaCostosConceptos obe = (EHojaCostosConceptos)viewConceptos.GetRow(viewConceptos.FocusedRowHandle);
            if (obe == null)
                return;
            EhojaCostosSubConceptos obeS = (EhojaCostosSubConceptos)viewSubConceptos.GetRow(viewSubConceptos.FocusedRowHandle);
            if (obeS == null)
                return;
            EHojaCostosRubros obeR = (EHojaCostosRubros)viewRubros.GetRow(viewRubros.FocusedRowHandle);
            if (oBeR == null)
                return;
            if (lstHojaCostosRubros.Count > 0)
            {
                if (obeR.hjcd2_iitem == obeS.hjcd2_vcodigo_concepto_hc)
                {
                    lstDeleteHCRubros.Add(obeR);
                    lstHojaCostosRubros.Remove(obeR);
                    viewRubros.RefreshData();
                }
            }
            if (lstHojaCostosSubConcepto.Count > 0)
            {
                if (obeS.hjcc1_iiten == obe.hjcd1_vcodigo_concepto_hc)
                {
                    lstDeleteHCSubConceptos.Add(obeS);
                    lstHojaCostosSubConcepto.Remove(obeS);
                    viewSubConceptos.RefreshData();
                }
            }
            if (lstHojaCostosConceptos.Count > 0)
            {
                if (obe.hjcd1_vcodigo_concepto_hc == obeS.hjcc1_iiten)
                {
                    lstDeleteHCConceptos.Add(obe);
                    lstHojaCostosConceptos.Remove(obe);
                    viewConceptos.RefreshData();
                    /**/
                    lstHojaCostosRubros.Remove(obeR);
                    viewRubros.RefreshData();
                    /**/
                    lstHojaCostosSubConcepto.Remove(obeS);
                    viewSubConceptos.RefreshData();
                }
            }
        }

        private void NuevoSubConcepto_Click(object sender, EventArgs e)
        {
            EHojaCostosConceptos Obe = (EHojaCostosConceptos)viewConceptos.GetRow(viewConceptos.FocusedRowHandle);
            if (Obe == null)
                return;
            using (frmManteHojaCostosSubConceptos frm = new frmManteHojaCostosSubConceptos())
            {
                frm.SetInsert();
                frm.ItemConcepto = Obe.hjcd1_vcodigo_concepto_hc;
                frm.lstHojaCostosSubConceptos = lstHojaCostosSubConcepto;
                if (lstHojaCostosSubConcepto.Count == 0)
                {
                    frm.txtCodigo.Text = "01";
                }
                else
                {
                    frm.txtCodigo.Text = string.Format("{0:00}", (lstHojaCostosSubConcepto.Max(ob => Convert.ToInt32(ob.hjcd2_vcodigo_concepto_hc)) + 1));
                }
                Cod2 = frm.txtCodigo.Text;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstHojaCostosSubConcepto = frm.lstHojaCostosSubConceptos.OrderBy(ob => ob.hjcd2_vcodigo_concepto_hc).ToList();
                    grdSubConceptos.DataSource = lstHojaCostosSubConcepto.Where(x => x.hjcc1_iiten == Obe.hjcd1_vcodigo_concepto_hc).ToList();
                    viewSubConceptos.RefreshData();
                }

            }
        }
        private void ModificarSubConcepto_Click(object sender, EventArgs e)
        {
            EhojaCostosSubConceptos obe = (EhojaCostosSubConceptos)viewSubConceptos.GetRow(viewSubConceptos.FocusedRowHandle);
            if (obe == null)
                return;
            int index = viewSubConceptos.FocusedRowHandle;
            using (frmManteHojaCostosSubConceptos frm = new frmManteHojaCostosSubConceptos())
            {
                frm.Obe = obe;
                frm.lstHojaCostosSubConceptos = lstHojaCostosSubConcepto;
                frm.SetModify();
                frm.setValues();
                frm.txtCodigo.Text = String.Format("{0:00}", Convert.ToInt32(obe.hjcd2_vcodigo_concepto_hc));
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstHojaCostosSubConcepto = frm.lstHojaCostosSubConceptos.OrderBy(ob => ob.hjcd2_vcodigo_concepto_hc).ToList();
                    viewSubConceptos.RefreshData();
                    viewSubConceptos.MoveLast();
                    viewSubConceptos.FocusedRowHandle = index;


                }
            }
        }
        private void EliminarSubConcepto_Click(object sender, EventArgs e)
        {
            EHojaCostosConceptos obec = (EHojaCostosConceptos)viewConceptos.GetRow(viewConceptos.FocusedRowHandle);
            if (obec == null)
                return;
            EhojaCostosSubConceptos obe = (EhojaCostosSubConceptos)viewSubConceptos.GetRow(viewSubConceptos.FocusedRowHandle);
            if (obe == null)
                return;
            lstDeleteHCSubConceptos.Add(obe);
            lstHojaCostosSubConcepto.Remove(obe);
            viewSubConceptos.RefreshData();
            grdSubConceptos.DataSource = lstHojaCostosSubConcepto.Where(x => x.hjcc1_iiten == obec.hjcd1_vcodigo_concepto_hc).ToList();
            TotalesSubConcepto();
            TotalesConcepto();
            TotalSoles();
            TotalDolares();

        }

        private void NuevoRubro_Click(object sender, EventArgs e)
        {
            EhojaCostosSubConceptos Obe = (EhojaCostosSubConceptos)viewSubConceptos.GetRow(viewSubConceptos.FocusedRowHandle);
            if (Obe == null)
                return;
            if (VerifyFields())
            {
                using (frmManteHojaCostosRubros frm = new frmManteHojaCostosRubros())
                {
                    frm.SetInsert();
                    frm.CargarControles();
                    frm.lstHojaCostosRubros = lstHojaCostosRubros;
                    frm.ItemSubConcepto = Obe.hjcd2_vcodigo_concepto_hc;
                    if (lstHojaCostosRubros.Count == 0)
                    {
                        frm.txtCodigo.Text = "01";
                    }
                    else
                    {
                        frm.txtCodigo.Text = string.Format("{0:00}", (lstHojaCostosRubros.Max(ob => Convert.ToInt32(ob.hjcd3_vcodigo_concepto_hc)) + 1));
                    }
                    Cod3 = frm.txtCodigo.Text;
                    CodCompleto = Cod1 + "-" + Cod2 + "-" + Cod3;
                    frm.CodCompleto = CodCompleto;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {

                        lstHojaCostosRubros = frm.lstHojaCostosRubros.OrderBy(ob => ob.hjcd3_vcodigo_concepto_hc).ToList();
                        grdRubros.DataSource = lstHojaCostosRubros.Where(x => x.hjcd2_iitem == Obe.hjcd2_vcodigo_concepto_hc).ToList();
                        viewRubros.RefreshData();
                        TotalesSubConcepto();
                        TotalesConcepto();
                        TotalSoles();
                        TotalDolares();
                    }

                }
            }
        }
        private void ModificarRubro_Click(object sender, EventArgs e)
        {
            EHojaCostosRubros obe = (EHojaCostosRubros)viewRubros.GetRow(viewRubros.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteHojaCostosRubros frm = new frmManteHojaCostosRubros())
            {
                frm.Obe = obe;
                frm.lstHojaCostosRubros = lstHojaCostosRubros;
                frm.SetModify();
                frm.setValues();
                frm.txtCodigo.Text = String.Format("{0:00}", Convert.ToInt32(obe.hjcd3_vcodigo_concepto_hc));
                if (frm.ShowDialog() == DialogResult.OK)
                {

                    lstHojaCostosRubros = frm.lstHojaCostosRubros.OrderBy(E => E.hjcd3_vcodigo_concepto_hc).ToList();
                    grdRubros.RefreshDataSource();
                    viewRubros.MoveLast();
                    TotalesSubConcepto();
                    TotalesConcepto();
                    TotalSoles();
                }
            }
        }
        private void EliminarRubro_Click(object sender, EventArgs e)
        {
            EhojaCostosSubConceptos obes = (EhojaCostosSubConceptos)viewSubConceptos.GetRow(viewSubConceptos.FocusedRowHandle);
            if (obes == null)
                return;
            EHojaCostosRubros obe = (EHojaCostosRubros)viewRubros.GetRow(viewRubros.FocusedRowHandle);
            if (obe == null)
                return;
            lstDeleteHCRubros.Add(obe);
            lstHojaCostosRubros.Remove(obe);
            viewRubros.RefreshData();
            grdRubros.DataSource = lstHojaCostosRubros.Where(x => x.hjcd2_iitem == obes.hjcd2_vcodigo_concepto_hc).ToList();
            TotalesSubConcepto();
            TotalesConcepto();
            TotalSoles();
            TotalDolares();

        }

        private void TotalesSubConcepto()
        {
            EHojaCostosConceptos ObeC = (EHojaCostosConceptos)viewConceptos.GetRow(viewConceptos.FocusedRowHandle);
            if (ObeC == null)
                return;
            EhojaCostosSubConceptos ObeS = (EhojaCostosSubConceptos)viewSubConceptos.GetRow(viewSubConceptos.FocusedRowHandle);
            if (ObeS == null)
                return;
            string PresupuestoS = "";
            string Convertir = "";
            decimal ConvertirSoles = 0;

            Convertir = lstHojaCostosRubros.Where(x => x.hjcd2_iitem == ObeS.hjcd2_vcodigo_concepto_hc).ToList().Where(x => x.tablc_icod_tipo_moneda == 4).Sum(x => x.hjcd3_nmonto_real).ToString();
            ConvertirSoles = Convert.ToDecimal(Convertir) * Convert.ToDecimal(txtTipoCambio.Text);
            PresupuestoS = lstHojaCostosRubros.Where(x => x.hjcd2_iitem == ObeS.hjcd2_vcodigo_concepto_hc).ToList().Where(x => x.tablc_icod_tipo_moneda == 3).Sum(x => x.hjcd3_nmonto_real).ToString();
            ObeS.hjcd2_nmonto_presup = Convert.ToDecimal(PresupuestoS) + ConvertirSoles;
            grdSubConceptos.DataSource = lstHojaCostosSubConcepto.Where(x => x.hjcc1_iiten == ObeC.hjcd1_vcodigo_concepto_hc).ToList();
        }
        private void TotalesConcepto()
        {
            EHojaCostosConceptos ObeC = (EHojaCostosConceptos)viewConceptos.GetRow(viewConceptos.FocusedRowHandle);
            if (ObeC == null)
                return;
            string PresuspuestoC = "";
            PresuspuestoC = lstHojaCostosSubConcepto.Where(x => x.hjcc1_iiten == ObeC.hjcd1_vcodigo_concepto_hc).ToList().Sum(x => x.hjcd2_nmonto_presup).ToString();
            ObeC.hjcd1_nmonto_presup = Convert.ToDecimal(PresuspuestoC);
            grConceptos.DataSource = lstHojaCostosConceptos;
            viewConceptos.RefreshData();
        }

        private void TotalSoles()
        {
            decimal TotalSoles = 0;
            decimal Convertir = 0;
            decimal TotalSolesConvertir = 0;
            Convertir = lstHojaCostosRubros.Where(x => x.tablc_icod_tipo_moneda == 4).Sum(x => x.hjcd3_nmonto_real);
            TotalSolesConvertir = Convertir * Convert.ToDecimal(txtTipoCambio.Text);
            TotalSoles = lstHojaCostosRubros.Where(x => x.tablc_icod_tipo_moneda == 3).Sum(x => x.hjcd3_nmonto_real);
            txtTotalSoles.Text = (TotalSoles + TotalSolesConvertir).ToString();
        }
        private void TotalDolares()
        {
            decimal TotalDolares = 0;
            decimal Convertir = 0;
            decimal TotalDolaresConvertir = 0;
            Convertir = lstHojaCostosRubros.Where(x => x.tablc_icod_tipo_moneda == 3).Sum(x => x.hjcd3_nmonto_real);
            TotalDolaresConvertir = Convertir / Convert.ToDecimal(txtTipoCambio.Text);
            TotalDolares = lstHojaCostosRubros.Where(x => x.tablc_icod_tipo_moneda == 4).Sum(x => x.hjcd3_nmonto_real);
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

    }
}