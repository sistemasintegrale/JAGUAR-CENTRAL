using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Ventas
{
    public partial class FrmManteEventoVenta : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;

        public FrmManteEventoVenta()
        {
            this.KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();
        }

        public List<EEventoVenta> oDetail;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        private BVentas Obl;

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
            txtNEvento.Enabled = !Enabled;
            txtlugar.Enabled = !Enabled;

            txtNEvento.Focus();
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
            EEventoVenta oBe = new EEventoVenta();
            Obl = new BVentas();
            try
            {

                if (string.IsNullOrEmpty(txtnombreEvento.Text))
                {
                    oBase = txtnombreEvento;
                    throw new ArgumentException("Ingrese el nombre del Evento");
                }
                if (string.IsNullOrEmpty(txtNEvento.Text))
                {
                    oBase = txtNEvento;
                    throw new ArgumentException("Ingresar Código");
                }

                if (string.IsNullOrEmpty(txtlugar.Text))
                {
                    oBase = txtNEvento;
                    throw new ArgumentException("Ingresar Lugar");
                }
                if (string.IsNullOrEmpty(txtDireccion.Text))
                {
                    oBase = txtDireccion;
                    throw new ArgumentException("Ingresar Dirección");
                }
                if (string.IsNullOrEmpty(txtRepresentante.Text))
                {
                    oBase = txtRepresentante;
                    throw new ArgumentException("Ingresar Representante");
                }
                if (Convert.ToInt32(lkpalmacen.EditValue) == 0)
                {
                    oBase = lkpalmacen;
                    throw new ArgumentException("Ingresar Almacén");
                }
                oBe.evev_vnumero_evento_venta = txtNEvento.Text;
                oBe.evev_isituacion_even_venta = Convert.ToInt32(LkpSituacion.EditValue);
                oBe.evev_vlugar_evento_venta = txtlugar.Text;
                oBe.evev_vDirecc_evento_venta = txtDireccion.Text;
                oBe.evev_vcorreo_evento_venta = txtcorreo.Text;
                oBe.evev_vcontac_evento_venta = txtcontacto.Text;
                oBe.evev_vTelefo_evento_venta = txttelefono.Text;
                oBe.evev_sfecha_evento_inicio = Convert.ToDateTime(dteFechaInicio.EditValue);
                oBe.evev_sfecha_evento_final = Convert.ToDateTime(dteFechaFinal.EditValue);
                oBe.almac_icod_almacen = Convert.ToInt32(lkpalmacen.EditValue);
                oBe.almac_vresponsa_even_venta = txtRepresentante.Text;
                oBe.evev_flag_estado = true;
                oBe.intUsuario = Valores.intUsuario;
                oBe.evev_vnombre_evento_venta = txtnombreEvento.Text;



                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obl.InsertarEventoVenta(oBe);
                }
                else
                {
                    oBe.evev_icod_evento_venta = Correlative;
                    Obl.ActualizarEventoVenta(oBe);
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

        private void FrmManteGiroCliente_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            var lstEstado = new BGeneral().listarTablaRegistro(58);
            BSControls.LoaderLook(LkpSituacion, lstEstado, "tarec_vdescripcion", "tarec_iid_tabla_registro", false);

            List<EAlmacen> lstAlmacenes = new List<EAlmacen>();
            lstAlmacenes = new BAlmacen().listarAlmacenes().Where(ob => ob.almac_tipo_event == 2).ToList(); ;
            BSControls.LoaderLook(lkpalmacen, lstAlmacenes, "almac_vdescripcion", "almac_icod_almacen", false);
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }






    }
}