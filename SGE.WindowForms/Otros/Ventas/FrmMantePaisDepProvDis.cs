using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Ventas
{
    public partial class FrmMantePaisDepProvDis : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;

        public FrmMantePaisDepProvDis()
        {
            this.KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();
        }

        public List<EUbicacion> oDetail;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        private BVentas Obl;

        public string habilitar = "";

        public int Correlative = 0;
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
            txtIdUbicacion.Enabled = !Enabled;
            txtNombre.Enabled = !Enabled;
            LkpTipoUbicacion.Enabled = !Enabled;
            LkpSituacion.Enabled = !Enabled;

            txtIdUbicacion.Focus();
        }

        private void clearControl()
        {
            txtIdUbicacion.Text = "";
            txtNombre.Text = "";
            LkpTipoUbicacion.EditValue = null;
            LkpSituacion.EditValue = null;

            LkpPais.EditValue = null;
            LkpDepartamento.EditValue = null;
            LkpProvincia.EditValue = null;
        }

        private void FrmManteVendedor_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            var lstEstado = new BGeneral().listarTablaRegistro(Parametros.intTipoTablaEstado);
            BSControls.LoaderLook(LkpSituacion, lstEstado, "tarec_vdescripcion", "tarec_icorrelativo_registro", false);
            BSControls.LoaderLook(LkpTipoUbicacion, new BGeneral().listarTablaRegistro(30), "tarec_vdescripcion", "tarec_icorrelativo_registro", false);
        }



        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
            LkpSituacion.ItemIndex = 0;
            LkpTipoUbicacion.ItemIndex = 0;
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
            EUbicacion oBe = new EUbicacion();
            Obl = new BVentas();
            try
            {
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (string.IsNullOrEmpty(txtIdUbicacion.Text))
                    {
                        oBase = txtIdUbicacion;
                        throw new ArgumentException("Ingresar Código");
                    }
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    var BuscarCodigo = oDetail.Where(oB => oB.ubicc_ccod_ubicacion.ToUpper() == txtIdUbicacion.Text.ToUpper() && oB.tablc_iid_tipo_ubicacion == Convert.ToInt32(LkpTipoUbicacion.EditValue)).ToList();
                    if (BuscarCodigo.Count > 0)
                    {
                        oBase = txtIdUbicacion;
                        throw new ArgumentException("El Código Existe");
                    }
                }

                oBe.ubicc_ccod_ubicacion = txtIdUbicacion.Text;
                oBe.ubicc_vnombre_ubicacion = txtNombre.Text;
                oBe.tablc_iid_tipo_ubicacion = Convert.ToInt32(LkpTipoUbicacion.EditValue);
                oBe.ubicc_iid_situacion_ubicacion = Convert.ToInt32(LkpSituacion.EditValue);

                int tipo = Convert.ToInt32(LkpTipoUbicacion.EditValue);
                switch (tipo)
                {
                    case 0:
                        oBase = LkpTipoUbicacion;
                        throw new ArgumentException("Seleccione un tipo de ubicación geográfica");
                        break;
                    case 1:
                        if (LkpProvincia.EditValue != null)
                            oBe.ubicc_icod_ubicacion_padre = Convert.ToInt32(LkpProvincia.EditValue);
                        else
                        {
                            oBase = LkpProvincia;
                            throw new ArgumentException("Seleccione una Provincia");
                        }
                        break;
                    case 2:
                        if (LkpDepartamento.EditValue != null)
                            oBe.ubicc_icod_ubicacion_padre = Convert.ToInt32(LkpDepartamento.EditValue);
                        else
                        {
                            oBase = LkpDepartamento;
                            throw new ArgumentException("Seleccione un Departamento");
                        }
                        break;
                    case 3:
                        if (LkpPais.EditValue != null)
                            oBe.ubicc_icod_ubicacion_padre = Convert.ToInt32(LkpPais.EditValue);
                        else
                        {
                            oBase = LkpPais;
                            throw new ArgumentException("Seleccione un País");
                        }
                        break;
                    case 4:
                        oBe.ubicc_icod_ubicacion_padre = null;
                        break;
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obl.InsertarUbicacion(oBe);
                }
                else
                {
                    oBe.ubicc_icod_ubicacion = Correlative;
                    Obl.ActualizarUbicacion(oBe);
                }
            }
            catch (Exception ex)
            {
                oBase.Focus();
                oBase.ErrorText = ex.Message;
                oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {
                if (Flag)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                        Status = BSMaintenanceStatus.View;
                    else
                        Status = BSMaintenanceStatus.View;

                    Status = BSMaintenanceStatus.View;
                    this.MiEvento();
                    this.Close();
                }
            }
        }

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


        private void txtdescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Escape)
            {
                cerrar_form(sender, e);
            }
        }

        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }

        private void FrmManteDetalleAnalitica_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MiEvento();
        }

        private void LkpTipoUbicacion_EditValueChanged(object sender, EventArgs e)
        {

            //if(habilitar != "ver"){
            LkpPais.EditValue = null;
            LkpDepartamento.EditValue = null;
            LkpProvincia.EditValue = null;
            int tipo = Convert.ToInt32(LkpTipoUbicacion.EditValue);
            switch (tipo)
            {
                case 0:
                    LkpPais.Enabled = !Enabled;
                    LkpDepartamento.Enabled = !Enabled;
                    LkpProvincia.Enabled = !Enabled;
                    break;
                case 1:
                    if (habilitar != "ver")
                    {
                        LkpPais.Enabled = Enabled;
                        LkpDepartamento.Enabled = Enabled;
                        LkpProvincia.Enabled = Enabled;
                    }
                    BSControls.LoaderLook(LkpPais, oDetail.Where(obj => obj.tablc_iid_tipo_ubicacion == 4).ToList(), "ubicc_vnombre_ubicacion", "ubicc_icod_ubicacion", false);
                    break;
                case 2:
                    if (habilitar != "ver")
                    {
                        LkpPais.Enabled = Enabled;
                        LkpDepartamento.Enabled = Enabled;
                    }
                    LkpProvincia.Enabled = !Enabled;
                    BSControls.LoaderLook(LkpPais, oDetail.Where(obj => obj.tablc_iid_tipo_ubicacion == 4).ToList(), "ubicc_vnombre_ubicacion", "ubicc_icod_ubicacion", false);
                    break;
                case 3:
                    if (habilitar != "ver")
                        LkpPais.Enabled = Enabled;
                    LkpDepartamento.Enabled = !Enabled;
                    LkpProvincia.Enabled = !Enabled;
                    BSControls.LoaderLook(LkpPais, oDetail.Where(obj => obj.tablc_iid_tipo_ubicacion == 4).ToList(), "ubicc_vnombre_ubicacion", "ubicc_icod_ubicacion", false);
                    break;
                case 4:
                    LkpPais.Enabled = !Enabled;
                    LkpDepartamento.Enabled = !Enabled;
                    LkpProvincia.Enabled = !Enabled;
                    break;
            }
            //}

        }

        private void LkpPais_EditValueChanged(object sender, EventArgs e)
        {
            BSControls.LoaderLook(LkpDepartamento, oDetail.Where(obj => obj.ubicc_icod_ubicacion_padre == Convert.ToInt32(LkpPais.EditValue)).ToList(), "ubicc_vnombre_ubicacion", "ubicc_icod_ubicacion", false);
        }

        private void LkpDepartamento_EditValueChanged(object sender, EventArgs e)
        {
            BSControls.LoaderLook(LkpProvincia, oDetail.Where(obj => obj.ubicc_icod_ubicacion_padre == Convert.ToInt32(LkpDepartamento.EditValue)).ToList(), "ubicc_vnombre_ubicacion", "ubicc_icod_ubicacion", false);
        }



    }
}