using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;



namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmManteFamiliaCab : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteFamiliaCab));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EFamiliaCab Obe = new EFamiliaCab();
        public List<EFamiliaCab> lstFamiliaCab = new List<EFamiliaCab>();


        public frmManteFamiliaCab()
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
            txtAbreviatura.Enabled = !Enabled;
            txtDescripcion.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            lkpTipo.Enabled = !Enabled;

            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtAbreviatura.Enabled = Enabled;


            if (Status == BSMaintenanceStatus.CreateNew)
                txtAbreviatura.Enabled = !Enabled;


        }
        public void setValues()

        {
            txtAbreviatura.Text = Obe.famic_vabreviatura;
            txtDescripcion.Text = Obe.famic_vdescripcion;
            lkpTipo.EditValue = Obe.famic_icod_tipo;
            lkpAlmContable.EditValue = Obe.clasc_icod_clasificacion;
            chkSerie.Checked = Obe.famic_flag_serie;
            txtTipoExistencia.Text = Obe.famic_vtipo_existencia;


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
                /*----------------------*/
                if (String.IsNullOrEmpty(txtAbreviatura.Text))
                {
                    oBase = txtAbreviatura;
                    throw new ArgumentException("Ingrese código de Línea");
                }
                if (verificarAbreviaturaFamilia(txtAbreviatura.Text))
                {
                    oBase = txtAbreviatura;
                    throw new ArgumentException("El código ingresado ya existe en los registros de Línea");
                }
                /*----------------------*/
                if (String.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese descripción de Familia");
                }
                if (verificarDescripcionFamilia(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("La descripción ingresada ya existe en los registros de Línea");
                }

                int? intNullVal = null;

                //Obe.famic_iid_familia = (lstFamiliaCab.Count + 1);
                Obe.famic_vabreviatura = txtAbreviatura.Text;
                Obe.famic_vdescripcion = txtDescripcion.Text;
                Obe.famic_icod_tipo = Convert.ToInt32(lkpTipo.EditValue);
                Obe.clasc_icod_clasificacion = Convert.ToInt32(lkpAlmContable.EditValue);
                Obe.famic_flag_estado = true;
                Obe.famic_flag_serie = Convert.ToBoolean(chkSerie.Checked);
                Obe.famic_vtipo_existencia = txtTipoExistencia.Text;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.famic_icod_familia = new BAlmacen().insertarFamiliaCab(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BAlmacen().modificarFamiliaCab(Obe);
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
                    this.MiEvento(Obe.famic_icod_familia);
                    this.Close();
                }
            }
        }
        private bool verificarCodigoFamilia(string strCodigo)
        {
            try
            {
                bool exists = false;
                if (lstFamiliaCab.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstFamiliaCab.Where(x => x.famic_iid_familia.ToString().Trim() == Convert.ToInt32(strCodigo).ToString().Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstFamiliaCab.Where(x => x.famic_iid_familia.ToString().Trim() == Convert.ToInt32(strCodigo).ToString().Trim() && x.famic_icod_familia != Obe.famic_icod_familia).ToList().Count > 0)
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
        private bool verificarAbreviaturaFamilia(string strAbreviatura)
        {
            try
            {
                bool exists = false;
                if (lstFamiliaCab.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstFamiliaCab.Where(x => x.famic_vabreviatura.Trim() == strAbreviatura.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstFamiliaCab.Where(x => x.famic_vabreviatura.Trim() == strAbreviatura.Trim() && x.famic_icod_familia != Obe.famic_icod_familia).ToList().Count > 0)
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
        private bool verificarDescripcionFamilia(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstFamiliaCab.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstFamiliaCab.Where(x => x.famic_vdescripcion.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstFamiliaCab.Where(x => x.famic_vdescripcion.Trim() == strNombre.Trim() && x.famic_icod_familia != Obe.famic_icod_familia).ToList().Count > 0)
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

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmManteFamiliaCab_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpTipo, new BGeneral().listarTablaRegistro(63), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(lkpAlmContable, new BContabilidad().listarClasificacionProducto(), "clasc_vdescripcion", "clasc_icod_clasificacion", true);
        }

        //private void bteClasificacion_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    listarClasificacion();
        //}

        //private void listarClasificacion()
        //{
        //    using (frmListarClasificacionProducto frm = new frmListarClasificacionProducto())
        //    {
        //        if (frm.ShowDialog() == DialogResult.OK)
        //        {
        //            bteClasificacion.Tag = frm._Be.clasc_icod_clasificacion;
        //            bteClasificacion.Text = frm._Be.clasc_vdescripcion;
        //        }
        //    }
        //}

    }
}