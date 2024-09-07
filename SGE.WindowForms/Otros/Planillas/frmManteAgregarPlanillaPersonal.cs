﻿using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
//using SGE.WindowForms.Planillas;
using SGE.WindowForms.Maintenance;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmManteAgregarPlanillaPersonal : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteAgregarPlanillaPersonal));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        public EAnioEjercicio Obe = new EAnioEjercicio();
        public List<EAnioEjercicio> lstAnioEjercicio = new List<EAnioEjercicio>();

        public decimal rem_basica;
        public string Nombre_Apellidos, num_documento, cussp, strCargo;
        public int icod_personal;
        public bool beps, basignacion_familiar;
        public DateTime? fecha_incio, fecha_cese;

        public frmManteAgregarPlanillaPersonal()
        {
            InitializeComponent();
        }
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
        private void StatusControl()
        {
            //txtAnioEjercicio.Enabled = false;
            //lkpEstado.Enabled = false;
            btnGuardar.Enabled = false;

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                //lkpEstado.Enabled = true;
                btnGuardar.Enabled = true;
            }
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                //txtAnioEjercicio.Enabled = true;
                //lkpEstado.Enabled = true;
                btnGuardar.Enabled = true;
            }
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
        public void setValues()
        {
            //txtAnioEjercicio.Text = Obe.anioc_iid_anio_ejercicio.ToString();
            //lkpEstado.EditValue = Convert.ToInt32(Obe.anioc_iactivo);
        }
        private void cargar()
        {
            List<EAnioEjercicio> ListaSituacion = new List<EAnioEjercicio>();
            //BSControls.LoaderLook(lkpEstado, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaEstadoAnioEjercicio), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            //lkpEstado.ItemIndex = 1;
        }
        private void FrmManteAnioEjercicio_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                //if (string.IsNullOrEmpty(txtAnioEjercicio.Text))
                //{
                //    oBase = txtAnioEjercicio;
                //    throw new ArgumentException("Ingrese año de ejercicio");
                //}
                //if (Status == BSMaintenanceStatus.CreateNew)
                //{
                //    if(lstAnioEjercicio.Count > 0)
                //        if (lstAnioEjercicio.Where(x => x.anioc_iid_anio_ejercicio == Convert.ToInt32(txtAnioEjercicio.Text)).ToList().Count == 1)
                //        {
                //            oBase = txtAnioEjercicio;
                //            throw new ArgumentException("El año de ejercicio ingresado ya está registrado");
                //        }
                //}
                //if (lkpEstado.EditValue == null)
                //{
                //    oBase = lkpEstado;
                //    throw new ArgumentException("Seleccione situación del año de ejercicio");
                //}

                //Obe.anioc_iid_anio_ejercicio = Convert.ToInt32(txtAnioEjercicio.Text);                
                //Obe.anioc_iactivo = Convert.ToBoolean(lkpEstado.EditValue);


                if (Status == BSMaintenanceStatus.CreateNew)
                    Obe.anioc_icod_anio_ejercicio = new BContabilidad().insertarAnioEjercicio(Obe);
                if (Status == BSMaintenanceStatus.ModifyCurrent)
                    new BContabilidad().modificarAnioEjercicio(Obe);
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
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(Obe.anioc_icod_anio_ejercicio);
                    this.Close();
                }
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            returnSeleccion();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bteCCosto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarPersonal frm = new frmListarPersonal())
            {
                //frm.flagSeleccionImpresion = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteCCosto.Text = frm._Be.ApellNomb;
                    bteCCosto.Tag = frm._Be.perc_icod_personal;

                    icod_personal = frm._Be.perc_icod_personal;
                    Nombre_Apellidos = frm._Be.ApellNomb;
                    num_documento = frm._Be.perc_vnum_doc;
                    cussp = frm._Be.perc_vcuspp;
                    rem_basica = Convert.ToDecimal(frm._Be.perc_nmont_basico);
                    beps = Convert.ToBoolean(frm._Be.perc_beps);
                    fecha_incio = frm._Be.perc_sfech_inicio;
                    fecha_cese = frm._Be.perc_sfech_cese;
                    basignacion_familiar = Convert.ToBoolean(frm._Be.perc_basig_familiar);
                    strCargo = frm._Be.strCargo;
                }
            }
        }


        private void returnSeleccion()
        {


            this.DialogResult = DialogResult.OK;

        }
    }
}