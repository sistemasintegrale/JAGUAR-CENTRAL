using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class FrmMantePuntoVenta : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        public FrmMantePuntoVenta()
        {
            this.KeyUp += cerrar_form;
            InitializeComponent();
        }
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public List<EPuntoVenta> lista = new List<EPuntoVenta>();
        public EPuntoVenta oBe = new EPuntoVenta();

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
            txtPuntoVenta.Enabled = !Enabled;
            txtCodigoPuntoVenta.Enabled = !Enabled;
            lkpsituacion.Enabled = !Enabled;
            txtPuntoVenta.Focus();
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtPuntoVenta.Enabled = !Enabled;
                txtCodigoPuntoVenta.Enabled = Enabled;
                lkpsituacion.Enabled = Enabled;
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtCodigoPuntoVenta.Enabled = Enabled;
                lkpsituacion.Enabled = !Enabled;
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
            txtCodigoPuntoVenta.Text = (oBe.puvec_vcod_punto_venta).ToString();
            txtPuntoVenta.Text = oBe.puvec_vdescripcion;

            txtSerieFactura.Text = oBe.puvec_vserie_factura;
            txtCorrelativoFactura.Text = oBe.puvec_icorrelativo_factura.ToString();

            txtSerieSuministros.Text = oBe.puvec_vserie_factura_suministros;
            txtCorrelativoSuministros.Text = oBe.puvec_icorrelativo_factura_suministros.ToString();
            txtSerieAlquileres.Text = oBe.puvec_vserie_factura_alquileres;
            txtCorrelativoAlquileres.Text = oBe.puvec_icorrelativo_factura_alquileres.ToString();

            txtSerieBoleta.Text = oBe.puvec_vserie_boleta;
            txtCorrelativoBoleta.Text = oBe.puvec_icorrelativo_boleta.ToString();
            txtSerieFNotaCredito.Text = oBe.puvec_vserie_notaF_credito;
            txtSerieBNotaCredito.Text = oBe.puvec_vserie_notaB_credito;
            txtCorrelativoNotaCredito.Text = oBe.puvec_icorrelativo_nota_credito.ToString();
            txtSerieFNotaDebito.Text = oBe.puvec_vserie_notaF_debito;
            txtSerieBNotaDebito.Text = oBe.puvec_vserie_notaB_debito;
            txtCorrelativoNotaDebito.Text = oBe.puvec_icorrelativo_nota_debito.ToString();
            txtDireccion.Text = oBe.puvec_vdireccion;
            txtCodigoSunat.Text = oBe.puvec_vcodigo_sunat;
            txtUsuarioOSE.Text = oBe.puvec_vusuario_ose;
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;


            try
            {

                if (string.IsNullOrEmpty(txtPuntoVenta.Text))
                {
                    oBase = txtPuntoVenta;
                    throw new ArgumentException("Ingresar Descripción");
                }
                if (string.IsNullOrEmpty(txtCodigoPuntoVenta.Text))
                {
                    oBase = txtCodigoPuntoVenta;
                    throw new ArgumentException("Ingresar Código");
                }
                if (lkpsituacion.EditValue == null)
                {
                    oBase = lkpsituacion;
                    throw new ArgumentException("Ingresar Situación");
                }
                oBe.puvec_vdescripcion = txtPuntoVenta.Text;
                oBe.puvec_vcod_punto_venta = Convert.ToInt32(txtCodigoPuntoVenta.Text);
                oBe.puvec_iactivo = Convert.ToInt32(lkpsituacion.EditValue);
                oBe.puvec_iactivo_descrpcion = lkpsituacion.Text;
                oBe.puvec_vdireccion = txtDireccion.Text;
                oBe.puvec_vcodigo_sunat = txtCodigoSunat.Text;
                oBe.puvec_vusuario_ose = txtUsuarioOSE.Text;

                if (txtSerieFactura.Text == "0000")
                    oBe.puvec_vserie_factura = "";
                else
                    oBe.puvec_vserie_factura = txtSerieFactura.Text;

                if (txtCorrelativoFactura.Text == "00000000")
                    oBe.puvec_icorrelativo_factura = 0;
                else
                    oBe.puvec_icorrelativo_factura = Convert.ToInt32(txtCorrelativoFactura.Text);


                if (txtSerieSuministros.Text == "0000")
                    oBe.puvec_vserie_factura_suministros = "";
                else
                    oBe.puvec_vserie_factura_suministros = txtSerieSuministros.Text;

                if (txtCorrelativoSuministros.Text == "00000000")
                    oBe.puvec_icorrelativo_factura_suministros = 0;
                else
                    oBe.puvec_icorrelativo_factura_suministros = Convert.ToInt32(txtCorrelativoSuministros.Text);

                if (txtSerieAlquileres.Text == "0000")
                    oBe.puvec_vserie_factura_alquileres = "";
                else
                    oBe.puvec_vserie_factura_alquileres = txtSerieAlquileres.Text;

                if (txtCorrelativoAlquileres.Text == "00000000")
                    oBe.puvec_icorrelativo_factura_alquileres = 0;
                else
                    oBe.puvec_icorrelativo_factura_alquileres = Convert.ToInt32(txtCorrelativoAlquileres.Text);




                if (txtSerieBoleta.Text == "0000")
                    oBe.puvec_vserie_boleta = "";
                else
                    oBe.puvec_vserie_boleta = txtSerieBoleta.Text;

                if (txtCorrelativoBoleta.Text == "00000000")
                    oBe.puvec_icorrelativo_boleta = 0;
                else
                    oBe.puvec_icorrelativo_boleta = Convert.ToInt32(txtCorrelativoBoleta.Text);

                if (txtSerieFNotaCredito.Text == "0000")
                    oBe.puvec_vserie_notaF_credito = "";
                else
                    oBe.puvec_vserie_notaF_credito = txtSerieFNotaCredito.Text;

                if (txtSerieBNotaCredito.Text == "0000")
                    oBe.puvec_vserie_notaB_credito = "";
                else
                    oBe.puvec_vserie_notaB_credito = txtSerieBNotaCredito.Text;

                if (txtCorrelativoNotaCredito.Text == "00000000")
                    oBe.puvec_icorrelativo_nota_credito = 0;
                else
                    oBe.puvec_icorrelativo_nota_credito = Convert.ToInt32(txtCorrelativoNotaCredito.Text);

                if (txtSerieFNotaDebito.Text == "0000")
                    oBe.puvec_vserie_notaF_debito = "";
                else
                    oBe.puvec_vserie_notaF_debito = txtSerieFNotaDebito.Text;
                if (txtSerieBNotaDebito.Text == "0000")
                    oBe.puvec_vserie_notaB_debito = "";
                else
                    oBe.puvec_vserie_notaB_debito = txtSerieBNotaDebito.Text;
                if (txtCorrelativoNotaDebito.Text == "00000000")
                    oBe.puvec_icorrelativo_nota_debito = 0;
                else
                    oBe.puvec_icorrelativo_nota_debito = Convert.ToInt32(txtCorrelativoNotaDebito.Text);

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.puvec_icod_punto_venta = new BCentral().InsertarPuntoVenta(oBe);
                }
                else
                {

                    new BCentral().ActualizarPuntoVenta(oBe);
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
                    this.MiEvento(oBe.puvec_icod_punto_venta);
                    this.Close();
                }
            }
        }



        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void FrmMantePuntoVenta_Load(object sender, EventArgs e)
        {
            carga();
        }

        private void txtPuntoVenta_KeyUp(object sender, KeyEventArgs e)
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

        private void FrmMantePuntoVenta_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.MiEvento();
        }
        private void carga()
        {
            BSControls.LoaderLook(lkpsituacion, new BGeneral().listarTablaRegistro(1), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            txtCodigoPuntoVenta.Text = (new BCentral().ValidarCodigoPuntoVenta()).ToString();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }



    }
}