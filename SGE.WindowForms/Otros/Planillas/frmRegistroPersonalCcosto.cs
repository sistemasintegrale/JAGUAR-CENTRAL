using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmRegistroPersonalCcosto : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroPersonalCcosto));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        private BSMaintenanceStatus mStatus;

        public EPersonalCCostos Obe = new EPersonalCCostos();
        public List<EPersonalCCostos> lstPersonalCC = new List<EPersonalCCostos>();
        public frmRegistroPersonal frmPersona = new frmRegistroPersonal();
        public string perc_vnum_doc = "";

        public frmRegistroPersonalCcosto()
        {
            InitializeComponent();
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

            dteFecha.Enabled = !Enabled;
            bteCCosto.Enabled = !Enabled;
            txtnomCCosto.Enabled = !Enabled;

            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                dteFecha.Enabled = !Enabled;
                bteCCosto.Enabled = !Enabled;
                txtnomCCosto.Enabled = !Enabled;
            }

        }
        public void setValues()
        {

            dteFecha.DateTime = Obe.pccd_sfecha;
            bteCCosto.EditValue = Obe.ccoc_numero_centro_costo;
            txtnomCCosto.Text = Obe.ccoc_vdescripcion_ccosto;
            txtAño.Text = Obe.pccd_iaño.ToString();



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
                if (dteFecha.DateTime == null || dteFecha.Text == "")
                {
                    oBase = dteFecha;
                    throw new ArgumentException("Ingrese Fecha  ");
                }

                if (bteCCosto.Text == "" || bteCCosto.Text == "__.__.__")
                {
                    oBase = bteCCosto;
                    throw new ArgumentException("Ingrese Centro de Costos ");
                }

                if (verificarCCostos(bteCCosto.Text))
                {
                    oBase = bteCCosto;
                    throw new ArgumentException("No se puede Registrar el mismo C. Costos en el mismo Mes");
                }
                if (Convert.ToInt32(txtAño.Text) > Convert.ToInt32(Parametros.intEjercicio))
                {
                    oBase = txtAño;
                    throw new ArgumentException("El Año no debe ser Mayor al Año de Ejercicio");
                }

                Obe.perc_vnum_doc = perc_vnum_doc;
                Obe.pccd_sfecha = dteFecha.DateTime;
                Obe.ccoc_numero_centro_costo = Convert.ToString(bteCCosto.Text);
                Obe.ccoc_vdescripcion_ccosto = txtnomCCosto.Text;

                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.pccd_flag_estado = true;
                Obe.pccd_imes = Convert.ToInt32(lkpMes.EditValue);
                Obe.pccd_iaño = Convert.ToInt32(txtAño.Text);


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.Contador = lstPersonalCC.Count + 1;
                    Obe.intTpoOperacion = 1;
                    lstPersonalCC.Add(Obe);

                }
                if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    if (Obe.intTpoOperacion != 1)
                    {
                        Obe.intTpoOperacion = 2;
                    }
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
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();

                }
            }
        }


        private bool verificarCCostos(string strCCostos)
        {
            try
            {
                bool exists = false;
                if (lstPersonalCC.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstPersonalCC.Where(x => x.ccoc_numero_centro_costo.Trim() == strCCostos.Trim() && x.pccd_imes == Convert.ToInt32(lkpMes.EditValue) && x.pccd_iaño == Convert.ToInt32(txtAño.Text)).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstPersonalCC.Where(x => x.ccoc_numero_centro_costo.Trim() == strCCostos.Trim() && x.pccd_imes != Convert.ToInt32(lkpMes.EditValue) && x.pccd_iaño == Convert.ToInt32(txtAño.Text) && x.perc_icod_personal != Obe.perc_icod_personal).ToList().Count > 0)
                            exists = true;
                    }
                }
                return exists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private bool verificarAbreviadoCargo(string strNombre)
        //{
        //try
        //{
        //    bool exists = false;
        //    if (lstCargo.Count > 0)
        //    {
        //        if (Status == BSMaintenanceStatus.CreateNew)
        //        {
        //            if (lstCargo.Where(x => x.carg_vabreviado.Trim() == strNombre.Trim()).ToList().Count > 0)
        //                exists = true;
        //        }
        //        if (Status == BSMaintenanceStatus.ModifyCurrent)
        //        {
        //            if (lstCargo.Where(x => x.carg_vabreviado.Trim() == strNombre.Trim() && x.carg_icod_cargo != Obe.carg_icod_cargo).ToList().Count > 0)
        //                exists = true;
        //        }
        //    }
        //    return exists;
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
        //}
        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmManteAlmacen_Load(object sender, EventArgs e)
        {
            txtnomCCosto.Enabled = false;
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtAño.Text = Parametros.intEjercicio.ToString();
            }
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMes.EditValue = Convert.ToInt32(DateTime.Now.Month);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpMes.EditValue = Obe.pccd_imes;
            }

        }



        private void lkpEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void bteCCosto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarCentroCosto frm = new frmListarCentroCosto())
            {
                //frm.flagSeleccionImpresion = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteCCosto.Text = frm._Be.cecoc_vcodigo_centro_costo;
                    bteCCosto.Tag = frm._Be.cecoc_icod_centro_costo;

                }
            }
        }

        private void bteCCosto_EditValueChanged(object sender, EventArgs e)
        {
            List<ECentroCosto> mlistaCentroCosto = (new BContabilidad()).listarCentroCosto();

            if (bteCCosto.Text == "" || bteCCosto.Text == "__.__.__")
            {
                txtnomCCosto.Text = null;
                return;
            }
            List<ECentroCosto> aux = new List<ECentroCosto>();
            aux = mlistaCentroCosto.Where(x => x.cecoc_vcodigo_centro_costo == bteCCosto.Text).ToList();


            if (aux.Count == 1)
            {
                bteCCosto.Tag = aux[0].cecoc_icod_centro_costo;
                txtnomCCosto.Text = aux[0].cecoc_vdescripcion;
            }
        }

        private void lkpMes_EditValueChanged(object sender, EventArgs e)
        {
            dteFecha.DateTime = Convert.ToDateTime("01-" + Convert.ToInt32(lkpMes.EditValue) + "-" + Convert.ToInt32(txtAño.Text) + "");
        }




    }
}