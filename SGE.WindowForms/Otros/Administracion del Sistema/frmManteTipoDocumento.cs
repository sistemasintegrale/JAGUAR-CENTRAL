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

namespace SGE.WindowForms.Otros.Administracion_del_Sistema
{
    public partial class frmManteTipoDocumento : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteTipoDocumento));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        public frmManteTipoDocumento()
        {
            InitializeComponent();

        }
        public List<ETipoDocumento> lstTipoDocumento;
        public List<ETipoDocumentoDetalle> lstTipoDocDetalle;
        public List<EModulo> lstModulos;

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public ETipoDocumento Obe = new ETipoDocumento();



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
            txtCodigo.Enabled = !Enabled;
            txtDescripcion.Enabled = !Enabled;
            txtCoa.Enabled = !Enabled;
            txtNroCorrelativo.Enabled = !Enabled;
            txtNroSerie.Enabled = !Enabled;

            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = Enabled;
            txtCodigo.Focus();
        }

        //private void clearControl()
        //{
        //    txtCodigo.Text = "";
        //    txtCoa.Text = "";
        //    txtDescripcion.Text = "";

        //}
        public void setValues()
        {
            txtCodigo.Text = Obe.tdocc_vabreviatura_tipo_doc;
            txtDescripcion.Text = Obe.tdocc_vdescripcion;
            txtCoa.Text = Obe.tdocc_coa;
            txtNroCorrelativo.Text = (Obe.tdocc_nro_correlativo != null) ? Obe.tdocc_nro_correlativo.ToString() : "";
            txtNroSerie.Text = (Obe.tdocc_nro_serie != null) ? Obe.tdocc_nro_serie : "";


        }
        public void cargar()
        {
            lstTipoDocDetalle = new BAdministracionSistema().listarTipoDocumentoDetalle(Obe);
            /*----------------------------------------------------------------------------------------*/
            lstModulos = new BAdministracionSistema().listarModulo();
            lstModulos.ForEach(x =>
            {
                var Lista = lstTipoDocDetalle.Where(obj => obj.moduc_icod_modulo == x.moduc_icod_modulo).ToList();
                if (Lista.Count > 0)
                    x.moduc_flag_estado = true;
                else
                    x.moduc_flag_estado = false;
            });

            grdTipoDetalle.DataSource = lstModulos;
        }
        private void FrmManteTipoDocumento_Load(object sender, EventArgs e)
        {
            cargar();
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            //clearControl();
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

                if (string.IsNullOrEmpty(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Ingrese código /abreviatura del documento");
                }
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese la descripción del documento");
                }

                if (Convert.ToInt32(txtNroSerie.Text) < 0)
                {
                    oBase = txtNroCorrelativo;
                    throw new ArgumentException("El número de serie, no debe ser menor a cero (0)");
                }

                if (Convert.ToInt32(txtNroCorrelativo.Text) < 0)
                {
                    oBase = txtNroCorrelativo;
                    throw new ArgumentException("El número correlativo, no debe ser menor a cero (0)");
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    var BuscarCodDoc = lstTipoDocumento.Where(oB => oB.tdocc_vabreviatura_tipo_doc.ToUpper() == txtCodigo.Text.ToUpper()).ToList();
                    if (BuscarCodDoc.Count > 0)
                    {
                        oBase = txtCodigo;
                        throw new ArgumentException("El código/abreviatura ingresado ya existe");
                    }
                }

                int? intNull = null;

                Obe.tdocc_nro_serie = (txtNroSerie.Text != "000") ? txtNroSerie.Text : "";
                Obe.tdocc_nro_serie_2 = "";

                Obe.tdocc_vdescripcion = txtDescripcion.Text;
                Obe.tdocc_vabreviatura_tipo_doc = txtCodigo.Text;
                Obe.tdocc_coa = (txtCoa.Text == "00") ? "" : txtCoa.Text;
                Obe.tdocc_iid_tipo_doc = lstTipoDocumento.Count + 1;

                Obe.tdocc_nro_correlativo = (String.IsNullOrEmpty(txtNroCorrelativo.Text)) ? intNull : Convert.ToInt32(txtNroCorrelativo.Text);
                Obe.tdocc_nro_correlativo_2 = 0;

                Obe.tdocc_flag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                viewTipoDetalle.MoveLast();
                txtDescripcion.Focus();

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    var lstModulosFlag = lstModulos.Where(x => x.moduc_flag_estado).ToList();
                    Obe.tdocc_icod_tipo_doc = new BAdministracionSistema().insertarTipoDocumento(Obe, lstModulosFlag);
                }
                else
                {
                    var lstModulosFlag = lstModulos.Where(x => x.moduc_flag_estado).ToList();
                    new BAdministracionSistema().modificarTipoDocumento(Obe, lstModulosFlag);
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
                Flag = false;

            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(Obe.tdocc_icod_tipo_doc);
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
    }
}