using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmManteEmpaqueDesempaque : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteNotaIngreso));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        /*----------------------------------------------------*/
        public EEmpaqueDesempaqueCab oBe = new EEmpaqueDesempaqueCab();
        /*----------------------------------------------------*/
        List<EEmpaqueDesempaqueCab> lstDetalle = new List<EEmpaqueDesempaqueCab>();
        List<EEmpaqueDesempaqueCab> lstDelete = new List<EEmpaqueDesempaqueCab>();
        /*----------------------------------------------------*/
        public List<EEmpaqueDesempaqueCab> lstCabecerasNI = new List<EEmpaqueDesempaqueCab>();
        /*----------------------------------------------------*/
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
        public frmManteEmpaqueDesempaque()
        {
            InitializeComponent();
        }

        private void FrmManteNotaIngreso_Load(object sender, EventArgs e)
        {
            cargarControles();
            Carga();
            if (Parametros.intEjercicio == DateTime.Now.Year)
                dteFecha.EditValue = DateTime.Now;
            else
                dteFecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
            lkpTipo.EditValue = 239;//EMPAQUE

        }
        private void cargarControles()
        {
            BSControls.LoaderLook(lkpTipo, new BGeneral().listarTablaRegistro(52), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
        }
        private void Carga()
        {
            lstDetalle = new BAlmacen().EmpaqueDesempaqueDetListar(oBe.emp_icod_desempaque);
            grdDetalle.DataSource = lstDetalle;
        }

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtNumero.Enabled = !Enabled;
            dteFecha.Enabled = !Enabled;
            txtUm.Enabled = !Enabled;

            txtObservaciones.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                btnPlanilla.Enabled = false;
                bteAlmacen.Enabled = false;
                lkpTipo.Enabled = false;
            }


        }

        public void setValues()
        {
            txtNumero.Text = oBe.emp_vnuemro_desempaque;
            dteFecha.EditValue = oBe.emp_sfecha_desempaque;
            btnPlanilla.Tag = oBe.plemc_iid;
            btnPlanilla.Text = oBe.plemc_Vicod;
            txtDescripcionPro.Tag = oBe.prdc_icod_producto;
            txtDescripcionPro.Text = oBe.prdc_vdescripcion_larga;
            lkpTipo.EditValue = oBe.Tablc_itipo_empaque;
            bteAlmacen.Tag = oBe.almac_icod_almacen;
            bteAlmacen.Text = oBe.almac_vdescripcion;
            txtObservaciones.Text = oBe.emp_vobservaciones;
            txtUm.Text = oBe.unidc_vabreviatura_unidad_medida;
            txtCantidad.Text = oBe.emp_dcantidad_desempaque.ToString();
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
                if (Convert.ToInt32(btnPlanilla.Tag) == 0)
                {
                    oBase = btnPlanilla;
                    throw new ArgumentException("Seleccione Plantilla de Empaque");
                }


                if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                {
                    oBase = bteAlmacen;
                    throw new ArgumentException("Seleccione Almacén");
                }

                if (Convert.ToDateTime(dteFecha.EditValue).Year != Parametros.intEjercicio)
                {
                    oBase = dteFecha;
                    throw new ArgumentException("La fecha seleccionada no esta dentro del año de ejercicio" + Parametros.intEjercicio);
                }
                if (Convert.ToDecimal(txtCantidad.Text) == 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("Ingrese Cantidad");
                }
                /*---------------------------------------------------------*/
                oBe.emp_vnuemro_desempaque = txtNumero.Text;
                oBe.emp_sfecha_desempaque = Convert.ToDateTime(dteFecha.EditValue);
                oBe.plemc_iid = Convert.ToInt32(btnPlanilla.Tag);
                oBe.Tablc_itipo_empaque = Convert.ToInt32(lkpTipo.EditValue);
                oBe.prdc_icod_producto = Convert.ToInt32(txtDescripcionPro.Tag);
                oBe.almac_icod_almacen = Convert.ToInt32(bteAlmacen.Tag);
                oBe.emp_vobservaciones = txtObservaciones.Text;
                oBe.emp_flag_estado = true;
                oBe.emp_dcantidad_desempaque = Convert.ToDecimal(txtCantidad.Text);
                oBe.prdc_vdescripcion_larga = txtDescripcionPro.Text;
                /*---------------------------------------------------------*/

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.plemc_iid = new BAlmacen().EmpaqueDesempaqueInsertar(oBe, lstDetalle);
                }
                else
                {
                    new BAlmacen().EmpaqueDesempaqueModificar(oBe, lstDetalle, lstDelete);
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
                    this.MiEvento(oBe.emp_icod_desempaque);
                    //imprimir();
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

        private void btnProducto_DoubleClick(object sender, EventArgs e)
        {

        }

        private void listarProducto()
        {
            try
            {

                frmListarPlantillaEmpaque frm = new frmListarPlantillaEmpaque();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnPlanilla.Tag = frm._Be.plemc_iid;
                    btnPlanilla.Text = frm._Be.plemc_vcod;
                    txtDescripcionPro.Text = frm._Be.prdc_vdescripcion_larga;
                    txtUm.Text = frm._Be.unidc_vabreviatura_unidad_medida;
                    txtDescripcionPro.Tag = frm._Be.prdc_icod_producto;

                    List<EEmpaquePlantilla> lstDetallePlant = new List<EEmpaquePlantilla>();
                    lstDetallePlant = new BAlmacen().EmpaquePlantillaDetListar(frm._Be.plemc_iid);

                    lstDetalle = new List<EEmpaqueDesempaqueCab>();
                    foreach (var _BEE in lstDetallePlant)
                    {
                        EEmpaqueDesempaqueCab _BE = new EEmpaqueDesempaqueCab();
                        _BE.empd_iitem_desempaque = _BEE.plemd_iitem;
                        _BE.prdc_icod_producto = Convert.ToInt32(_BEE.prdc_icod_producto);
                        _BE.prdc_vdescripcion_larga = _BEE.prdc_vdescripcion_larga;
                        _BE.prdc_vcode_producto = _BEE.prdc_vcode_producto;
                        _BE.unidc_vabreviatura_unidad_medida = _BEE.unidc_vabreviatura_unidad_medida;
                        _BE.empd_dcantidad_desempaque = _BEE.plemd_ncantidad;
                        _BE.empd_flag_estado = true;
                        lstDetalle.Add(_BE);
                    }
                }
                grdDetalle.DataSource = lstDetalle;
                txtObservaciones.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProducto();
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
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}