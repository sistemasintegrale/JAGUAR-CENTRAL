using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class FrmManteTipo : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteTipo));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public int TipoRegistro;
        public string NombreFormulario;

        public List<ECategoria> mlistaRegistro = new List<ECategoria>();
        public List<ECategoria> oDetail;

        public FrmManteTipo()
        {

            this.KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();

        }
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
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
            txtdescripcion.Enabled = !Enabled;
            txtabreviatura.Enabled = !Enabled;
        }

        private void clearControl()
        {
            if (TipoRegistro == 1 || TipoRegistro == 4 ||
                TipoRegistro == 6 || TipoRegistro == 7)
                txtcodigo.Text = String.Format("{0}", Convert.ToInt32(Correlative));
            if (TipoRegistro == 11 || TipoRegistro == 2 ||
                TipoRegistro == 3 || TipoRegistro == 5 ||
                TipoRegistro == 8 || TipoRegistro == 9 ||
                TipoRegistro == 10)
                txtcodigo.Text = String.Format("{0:00}", Convert.ToInt32(Correlative));
            txtdescripcion.Text = "";
            txtabreviatura.Text = "";
            txtdescripcion.Focus();
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
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
            ECategoria oBe = new ECategoria();
            try
            {
                if (string.IsNullOrEmpty(txtcodigo.Text))
                {
                    oBase = txtcodigo;
                    throw new ArgumentException("Ingresar Codigo");
                }

                if (string.IsNullOrEmpty(txtdescripcion.Text))
                {
                    oBase = txtdescripcion;
                    throw new ArgumentException("Ingresar Descripcion");
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    var BuscarCosto = oDetail.Where(oB => oB.CatNUno_vdescripcion.ToUpper() == txtdescripcion.Text.ToUpper()).ToList();
                    if (BuscarCosto.Count > 0)
                    {
                        oBase = txtdescripcion;
                        throw new ArgumentException("La Descripcion Existe");
                    }

                    var CodigoRepetido = oDetail.Where(oB => oB.tarec_viid_correlativo.ToUpper() == txtcodigo.Text.ToUpper()).ToList();
                    if (CodigoRepetido.Count > 0)
                    {
                        oBase = txtcodigo;
                        throw new ArgumentException("El Codigo ya Existe");
                    }

                }

                oBe.CatNUno_iid_tabla_registro = 0;
                oBe.CatNUno_iid_tipo_tabla = TipoRegistro;
                oBe.CatNUno_icorrelativo_registro = Convert.ToInt32(txtcodigo.Text);
                oBe.CatNUno_vdescripcion = txtdescripcion.Text;
                oBe.CatNUno_nvalor_numerico = 0;
                oBe.CatNUno_vvalor_texto = txtabreviatura.Text;
                oBe.CatNUno_cestado = 'A';
                oBe.vestado = "ACTIVO";
                oBe.intUsuario = Valores.intUsuario;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    new BAlmacen().InsertarCategoriaSubUno(oBe);
                }
                else
                {
                    oBe.CatNUno_iid_tabla_registro = Correlative;
                    new BAlmacen().ActualizarCategoriaSubUno(oBe);
                }
            }
            catch (Exception ex)
            {
                oBase.Focus();
                oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                oBase.ErrorText = ex.Message;
                oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {
                if (Flag)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        Status = BSMaintenanceStatus.View;

                    }
                    else
                    {
                        Status = BSMaintenanceStatus.View;

                    }
                    Status = BSMaintenanceStatus.View;
                    this.MiEvento();
                    this.Close();
                }
            }
        }

        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();

        }

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetSave();
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void FrmManteMotonave_Load(object sender, EventArgs e)
        {
            this.Text = "MANTENIMIENTO DE " + NombreFormulario;
        }

        private void FrmManteMotonave_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MiEvento();
        }

        private void txtcodigo_Leave(object sender, EventArgs e)
        {

        }


    }
}