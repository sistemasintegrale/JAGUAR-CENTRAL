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
    public partial class FrmManteAlmacen : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteAlmacen));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        //MyKeyPress myKeyPressHandler = new MyKeyPress();

        public FrmManteAlmacen()
        {
            this.KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();
            //txtCodigo.KeyPress += new KeyPressEventHandler(myKeyPressHandler.MyKeyCounter);
        }

        public List<EProdAlmacen> oDetail;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        private EProdAlmacen Obl;
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
            txtDireccion.Enabled = !Enabled;
            txtRepresentante.Enabled = !Enabled;
            txtCodigo.Focus();
        }

        private void clearControl()
        {
            txtCodigo.Text = String.Format("{0:0000}", Convert.ToInt32(Correlative));
            txtDescripcion.Text = "";
            txtRepresentante.Text = "";
            txtDireccion.Text = "";
            lkp_PuntoVenta.EditValue = null;
        }
        private void cargar()
        {
            BSControls.LoaderLook(lkp_PuntoVenta, new BCentral().ListarPuntoVenta(), "puvec_vdescripcion", "puvec_icod_punto_venta", true);
        }

        private void frmAlmacenInsertUpdate_Load(object sender, EventArgs e)
        {
            cargar();
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
            EProdAlmacen oBe = new EProdAlmacen();
            //Obl = new BCentral();
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
                if (string.IsNullOrEmpty(txtDireccion.Text))
                {
                    oBase = txtDireccion;
                    throw new ArgumentException("Ingresar Direccion");
                }
                //if (lkp_PuntoVenta.EditValue == null)
                //{
                //    oBase = lkp_PuntoVenta;
                //    throw new ArgumentException("Ingresar Punto de Venta");
                //}

                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese Descripción");
                }


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    var CodigoRepetido = oDetail.Where(oB => oB.idf_almacen.ToUpper() == txtCodigo.Text.ToUpper()).ToList();
                    if (CodigoRepetido.Count > 0)
                    {
                        oBase = txtCodigo;
                        throw new ArgumentException("El Codigo ya Existe");
                    }
                }

                oBe.idf_almacen = txtCodigo.Text;
                oBe.descripcion = txtDescripcion.Text;
                oBe.direccion = txtDireccion.Text;
                oBe.representante = txtRepresentante.Text;
                oBe.puvec_icod_punto_venta = Convert.ToInt32(lkp_PuntoVenta.EditValue);
                oBe.iactivo = 1;
                oBe.intUsuario = Valores.intUsuario;
                oBe.codigo_ubigeo = txtUbigeo.Text;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    var BuscarDesripcion = oDetail.Where(oB => oB.descripcion.ToUpper() == txtDescripcion.Text.ToUpper()).ToList();

                    if (BuscarDesripcion.Count > 0)
                    {
                        oBase = txtDescripcion;
                        throw new ArgumentException("La Descripcion ya Existen");
                    }

                    oBe.id_almacen = Convert.ToInt32(oBe.idf_almacen);
                    new BCentral().InsertarProdAlmacen(oBe);
                    oDetail.Add(oBe);
                }
                else
                {
                    oBe.id_almacen = Correlative;
                    new BCentral().ActualizarProdAlmacen(oBe);
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
        private void FrmManteAlmacen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }
        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }

        private void FrmManteAlmacen_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MiEvento();
        }
    }
}