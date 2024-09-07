using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class FrmManteModelo : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteTipo));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        MyKeyPress myKeyPressHandler = new MyKeyPress();
        public int CodeMarca;
        public List<EProdModelo> oDetail;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        private EProdModelo Obl;
        public int Correlative = 0;
        public FrmManteModelo()
        {
            this.KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();
            txtcodigo.KeyPress += new KeyPressEventHandler(myKeyPressHandler.MyKeyCounter);
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
            txtdescripcion.Enabled = !Enabled;
        }

        private void clearControl()
        {
            txtcodigo.Text = String.Format("{0:0000}", Convert.ToInt32(Correlative));
            txtdescripcion.Text = "";
            txtdescripcion.Focus();
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
        }
        public void SetCancel() => Status = BSMaintenanceStatus.View;
        public void SetModify() => Status = BSMaintenanceStatus.ModifyCurrent;

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            EProdModelo oBe = new EProdModelo();
            BCentral Obl = new BCentral();
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
                    var BuscarCosto = oDetail.Where(oB => oB.mo_vdescripcion.ToUpper() == txtdescripcion.Text.ToUpper()).ToList();
                    if (BuscarCosto.Count > 0)
                    {
                        oBase = txtdescripcion;
                        throw new ArgumentException("La Descripcion Existe");
                    }


                }
                if (Convert.ToInt32(lkpCategoria.EditValue) == 0)
                {
                    oBase = lkpCategoria;
                    throw new ArgumentException("Selecione Categoria");
                }
                if (Convert.ToInt32(lkpTipo.EditValue) == 0)
                {
                    oBase = lkpTipo;
                    throw new ArgumentException("Selecione Tipo");
                }
                if (Convert.ToInt32(LkpLinea.EditValue) == 0)
                {
                    oBase = LkpLinea;
                    throw new ArgumentException("Selecione Linea");
                }
                oBe.mo_icod_modelo = 0;
                oBe.tarec_icorrelativo = CodeMarca;
                oBe.mo_iid_modelo = Convert.ToInt32(txtcodigo.Text);

                oBe.pr_iid_tipo = Convert.ToInt32(lkpCategoria.EditValue);
                oBe.pr_iid_categoria = Convert.ToInt32(lkpTipo.EditValue);
                oBe.pr_iid_linea = Convert.ToInt32(LkpLinea.EditValue);
                oBe.mo_vdescripcion = txtdescripcion.Text;
                oBe.mo_imagen = null;
                oBe.mo_estado = 1;
                oBe.intUsuario = Valores.intUsuario;

                oBe.mo_cestado = (Convert.ToInt32(lkpSituacion.EditValue) == 1) ? "A" : "I";
                oBe.mo_iserie = Convert.ToInt32(lkpSerie.EditValue);
                oBe.image = pictureEdit1.Image;
                oBe.intUsuario = Valores.intUsuario;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obl.InsertarProdModelo(oBe);
                }
                else
                {
                    oBe.mo_icod_modelo = Correlative;
                    Obl.ActualizarProdModelo(oBe);
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

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => this.SetSave();
        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => this.Close();
        private void FrmManteMotonave_FormClosing(object sender, FormClosingEventArgs e) => this.MiEvento();
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OpenFd.Title = "Insertar Imagen";
            OpenFd.FileName = "";
            OpenFd.Filter = "Image Files(*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            if (OpenFd.ShowDialog() == DialogResult.OK)
            {
                using (var bmpTemp = new Bitmap(OpenFd.FileName))
                    pictureEdit1.Image = new Bitmap(bmpTemp);
            }
        }
        private void Cargar()
        {
            BSControls.LoaderLook(lkpCategoria, new BCentral().ListarProdTablaRegistro(new EProdTablaRegistro { iid_tipo_tabla = Constantes.Categoria }), "descripcion", "tarec_viid_correlativo", false);
            BSControls.LoaderLook(LkpLinea, new BCentral().ListarProdTablaRegistro(new EProdTablaRegistro { iid_tipo_tabla = Constantes.Linea }), "descripcion", "tarec_viid_correlativo", false);
            BSControls.LoaderLook(lkpTipo, new BCentral().ListarProdTablaRegistro(new EProdTablaRegistro { iid_tipo_tabla = Constantes.Tipo }), "descripcion", "tarec_viid_correlativo", false);
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaEstado), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(lkpSerie, new BCentral().ListarRegistroSerie(), "resec_vdescripcion", "resec_iid_registro_serie", true);
        }
        private void FrmManteModelo_Load(object sender, EventArgs e)
        {
            Cargar();
            txtcodigo.Properties.ReadOnly = true;
        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }
    }
}