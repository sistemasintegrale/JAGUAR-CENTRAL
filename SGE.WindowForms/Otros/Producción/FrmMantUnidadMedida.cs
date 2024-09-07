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

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class FrmMantUnidadMedida : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public FrmMantUnidadMedida()
        {
            InitializeComponent();
        }
        public List<EProdUnidadMedida> oDetail;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        private BCentral Obl;
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
            txtDescripcion.Enabled = !Enabled;
            txtAbreviatura.Enabled = !Enabled;
            txtCodigo.Focus();
        }

        private void clearControl()
        {
            txtCodigo.Text = Convert.ToString(Convert.ToInt32(Correlative));
            txtDescripcion.Text = "";
            txtAbreviatura.Text = "";
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
            EProdUnidadMedida oBe = new EProdUnidadMedida();
            Obl = new BCentral();
            try
            {
                if (string.IsNullOrEmpty(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Ingresar Codigo");
                }
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingresar Descripcion");
                }
                if (string.IsNullOrEmpty(txtAbreviatura.Text))
                {
                    oBase = txtAbreviatura;
                    throw new ArgumentException("Ingresar Abreviatura");
                }


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    var BuscarAbreviatura = oDetail.Where(oB => oB.abreviatura_unidad_medida.ToUpper() == txtAbreviatura.Text.ToUpper()).ToList();
                    if (BuscarAbreviatura.Count > 0)
                    {
                        oBase = txtAbreviatura;
                        throw new ArgumentException("La Unidad Abreviada Existe");
                    }
                    var CodigoRepetido = oDetail.Where(oB => oB.idf_unidad_medida.ToUpper() == txtCodigo.Text.ToUpper()).ToList();
                    if (CodigoRepetido.Count > 0)
                    {
                        oBase = txtCodigo;
                        throw new ArgumentException("El Codigo ya Existe");
                    }
                    var BuscarDesripcion = oDetail.Where(oB => oB.descripcion.ToUpper() == txtDescripcion.Text.ToUpper()).ToList();

                    if (BuscarDesripcion.Count > 0)
                    {
                        oBase = txtDescripcion;
                        throw new ArgumentException("La Descripcion o Unidad Abreviada Existen");
                    }
                }



                oBe.idf_unidad_medida = txtCodigo.Text;
                oBe.descripcion = txtDescripcion.Text;
                oBe.abreviatura_unidad_medida = txtAbreviatura.Text;
                oBe.unidc_vcodigo_sunat = txtCodigoSUNAT.Text;
                oBe.usuario_crea = Valores.intUsuario;
                oBe.fecha_crea = DateTime.Now;
                oBe.usuario_modifica = Valores.intUsuario;
                oBe.fecha_modifica = DateTime.Now;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.id_unidad_medida = Convert.ToInt32(oBe.idf_unidad_medida);
                    Obl.InsertarProdUnidadMedida(oBe);
                    oDetail.Add(oBe);
                }
                else
                {
                    oBe.id_unidad_medida = Correlative;
                    Obl.ActualizarProdUnidadMedida(oBe);
                }

            }
            catch (Exception ex)
            {
                oBase.Focus();
                //oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
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
                        Status = BSMaintenanceStatus.View;
                    else

                        Status = BSMaintenanceStatus.View;
                    Status = BSMaintenanceStatus.View;
                    this.MiEvento();
                    this.Close();
                }
            }
        }
        private void FrmMantUnidadMedida_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            this.SetSave();
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

    }
}