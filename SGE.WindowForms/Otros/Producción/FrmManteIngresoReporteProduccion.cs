using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using System;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class FrmManteIngresoReporteProduccion : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteIngresoReporteProduccion));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public BSMaintenanceStatus oState;
        private BCentral Obl;
        public int IdReporteProduccion = 0;
        public int IdProductoEspecifico = 0;
        public string NumeroRP = "";
        public EOrdenProduccion oBe = new EOrdenProduccion();
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

        #endregion

        #region "Eventos"

        public FrmManteIngresoReporteProduccion()
        {
            InitializeComponent();
        }

        private void FrmManteIngresoReporteProduccion_Load(object sender, EventArgs e)
        {
            dtmFecha.EditValue = DateTime.Now;
            dtmFecha.Focus();
        }

        #endregion

        #region "Metodos"

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            dtmFecha.Enabled = !Enabled;
            dtmFecha.Focus();
        }

        public void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            Obl = new BCentral();
            try
            {
                if (Convert.ToInt32(btnAlmacen.Tag) == 0)
                {
                    oBase = btnAlmacen;
                    throw new ArgumentException("Seleccione el almacén");
                }

                //Datos del Reporte de Producción
                oBe.orprc_iid_orden_produccion = IdReporteProduccion;
                //oBe.rp_id_kardex_producto_ingreso = 0;
                oBe.orprc_sfecha_ing_kardex = Convert.ToDateTime(dtmFecha.EditValue);
                oBe.orprc_iid_situacion = Parametros.intSitReporteProduccionActualizado;

                //Datos del Kardex
                EProdKardex objE_Kardex = new EProdKardex();
                //objE_Kardex.kardc_ianio = Parametros.intEjercicio;
                //objE_Kardex.kardc_fecha_movimiento = Convert.ToDateTime(dtmFecha.EditValue);
                //objE_Kardex.almac_icod_almacen = Convert.ToInt32(btnAlmacen.Tag);
                //objE_Kardex.prdc_icod_producto = IdProductoEspecifico;
                //objE_Kardex.kardc_icantidad_prod = Convert.ToDecimal(txtCantidad.EditValue);
                //objE_Kardex.tdocc_icod_tipo_doc = Parametros.intTipoDocReporteProduccion;
                //objE_Kardex.kardc_numero_doc = NumeroRP;
                //objE_Kardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                //objE_Kardex.kardc_iid_motivo = Parametros.intMovIngTransformacion;
                //objE_Kardex.kardc_beneficiario = btnAlmacen.Text;
                //objE_Kardex.kardc_observaciones = "REPORTE DE PRODUCCIÓN";
                //objE_Kardex.intUsuario = 0;
                //objE_Kardex.kardc_flag_estado = true;

                objE_Kardex.kardx_sfecha = Convert.ToDateTime(dtmFecha.EditValue);
                objE_Kardex.kardc_ianio = Parametros.intEjercicio;
                objE_Kardex.iid_almacen = Convert.ToInt32(btnAlmacen.Tag);
                //objE_Kardex.iid_producto = Convert.ToInt32(idproducto[i]);
                //objE_Kardex.puvec_icod_punto_venta = objNotaSalida.puvec_icod_punto_venta;
                //objE_Kardex.Cantidad = Convert.ToDecimal(Cantidades[i]);
                //objE_Kardex.NroNota = Convert.ToInt32(objNotaSalida.salgc_inumero_nota_salida);
                objE_Kardex.iid_tipo_doc = Parametros.intTipoDocReporteProduccion;
                objE_Kardex.NroDocumento = NumeroRP;
                objE_Kardex.movimiento = 1;
                objE_Kardex.Motivo = Parametros.intMovIngTransformacion;
                objE_Kardex.Beneficiario = btnAlmacen.Text;
                objE_Kardex.Observaciones = "ORDEN DE PRODUCCION";
                //objE_Kardex.UsuarioCrea = objNotaSalida.salgc_iusuario_crea;
                //objE_Kardex.Item = Convert.ToInt32(obj.salgcd_num_item);
                //objE_Kardex.stocc_codigo_producto = obj.pr_vcodigo_externo;
                objE_Kardex.operacion = 1;



                Obl.ActualizarReporteProduccionKardex(oBe, objE_Kardex);

            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Flag = false;
                }
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



        #endregion

        private void btnAgregar_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btncance_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarAlamcen();
        }

        private void ListarAlamcen()
        {
            FrmListarAlmacen Almacen = new FrmListarAlmacen();
            Almacen.puvec_icod_punto_venta = 2;
            if (Almacen.ShowDialog() == DialogResult.OK)
            {
                btnAlmacen.Tag = Almacen._Be.id_almacen;
                btnAlmacen.Text = Almacen._Be.descripcion;
            }
            dtmFecha.Focus();
        }

    }
}