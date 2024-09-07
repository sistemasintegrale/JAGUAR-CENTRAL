using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class FrmManteDocxPagarAdicional : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteDocxPagarAdicional));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        //MyKeyPress myKeyPressHandler = new MyKeyPress();
        //public int? CodeCentro { get; set; }
        //public int? CodeFamilia { get; set; }
        //public int CodePresupuesto { get; set; }
        //public decimal? PercentSeguro { get; set; }
        //List<TipoCambio> ListaTipoCambio = new List<TipoCambio>();
        public FrmManteDocxPagarAdicional()
        {
            InitializeComponent();
        }

        public EDxPDatosAdicionales obeDatAdic = new EDxPDatosAdicionales();
        EDocPorPagar obeDocXPagar = new EDocPorPagar();
        private BCuentasPorPagar Obl = new BCuentasPorPagar();

        //public List<EPresupuestoImportacion> oDetail;
        //public List<EPresupuestoImportacionDetalle> oDetailPresupuesto;
        //public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        //private BPresupuestoImprotacion Obl;
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
            bool Enabled = (Status == BSMaintenanceStatus.ModifyCurrent);
            txtCodAduana.Properties.ReadOnly = Enabled;
            txtAnioDocAduana.Properties.ReadOnly = Enabled;
            txtCorrelativo.Properties.ReadOnly = Enabled;
            txtNumFormulario.Properties.ReadOnly = Enabled;
            txtNumOrdenForm.Properties.ReadOnly = Enabled;
            txtNumSerieDoc.Properties.ReadOnly = Enabled;
            txtNumTicket.Properties.ReadOnly = Enabled;
            txtNumCorrelDoc.Properties.ReadOnly = Enabled;
        }
        //public void cargardetalle(int code)
        //{
        //    oDetailPresupuesto = new BPresupuestoImprotacion().ListarPresupuestoImportacionDetalle(code);
        //    dgrPlantilla.DataSource = oDetailPresupuesto;
        //    viewPlantilla.ExpandAllGroups();

        //}
        private void clearControl()
        {
            //txtcodigo.Text = String.Format("{0:000}", Convert.ToInt32(Correlative));
            //txtanio.Text = "";
            //txtcodigoproducto.Text = "";
            //txtproducto.Text = "";
            //txtestadoproducto.Text = "";
            //txtsituacionproducto.Text = "";
            //txtunidmedproducto.Text = "";
            //txtcodigomotonave.Text = "";
            //txtmotonave.Text = "";
            //txtcantimportacion.Text = "0";
            //txtqty.Text = "0.0000";
            //txtkilos.Text = "0.0000";
            //txtsac.Text = "0.0000";
            //txtcontain.Text = "0.0000";
            //txtprecioctp.Text = "0.00";
            //txtpreciofob.Text = "0.00";
            //txtflete.Text = "0.00";
            //txtcif.Text = "0.00";
            //txttipocambio.Text = "0.0000";
            //txtconcepto.Text = "";
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
        }
        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }
        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }
        private void CargarCombos()
        {
            //EDGAR////EDGAR////EDGAR//
            //EDGAR////EDGAR////EDGAR//
            //EDGAR////EDGAR////EDGAR//
            //BSControls.LoaderLook(lkpTipocompra, new BTablasSunat().TablasSunatDetListar(1), "suntd_vdescripcion", "suntd_icod", true);

            //BSControls.LoaderLook(lkpTipoPersona, new BTablasSunat().TablasSunatDetListar(3), "suntd_vdescripcion", "suntd_icod", true);

            //BSControls.LoaderLook(lkptipodocumento, new BTablasSunat().TablasSunatDetListar(4), "suntd_vdescripcion", "suntd_icod", true);            

            //BSControls.LoaderLook(lkpCodigoDestino, new BTablasSunat().TablasSunatDetListar(5), "suntd_vdescripcion", "suntd_icod", true);
        }

        private void FrmManteDocxPagarAdicional_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarDatos();
        }

        public void CargarDatos()
        {
            lkpTipocompra.EditValue = obeDatAdic.doxpc_tipo_compra;
            lkpTipocompra.Text = obeDatAdic.tipo_compra_descripcion;
            lkpTipoPersona.Tag = obeDatAdic.proc_tipo_persona;
            lkpTipoPersona.Text = obeDatAdic.tipo_persona_descripcion;
            lkptipodocumento.EditValue = obeDatAdic.doxpc_tipo_documento;
            lkptipodocumento.Text = obeDatAdic.tipo_documento_descripcion;
            if (obeDatAdic.doxpc_tipo_destino != null)
            {
                lkpCodigoDestino.EditValue = obeDatAdic.doxpc_tipo_destino;
                lkpCodigoDestino.Text = obeDatAdic.tipo_destino_descripcion;
            }
        }



        private void BtnGuardar_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetSave();
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (txtCodAduana.Properties.ReadOnly == false)
                {
                    if (Convert.ToInt32(txtCodAduana.Text) == 0)
                    {
                        oBase = txtCodAduana;
                        throw new ArgumentException("Ingrese Código Aduana");
                    }
                    else
                        obeDatAdic.doxpc_cod_aduana = txtCodAduana.Text;
                }
                else
                    obeDatAdic.doxpc_cod_aduana = null;

                if (txtAnioDocAduana.Properties.ReadOnly == false)
                {
                    if (Convert.ToInt32(txtAnioDocAduana.Text) == 0)
                    {
                        oBase = txtAnioDocAduana;
                        throw new ArgumentException("Ingrese el año");
                    }
                    else
                        obeDatAdic.doxpc_anio_aduana = Convert.ToInt32(txtAnioDocAduana.Text);
                }
                else
                    obeDatAdic.doxpc_anio_aduana = null;

                if (txtCorrelativo.Properties.ReadOnly == false)
                {
                    if (Convert.ToInt32(txtCorrelativo.Text) == 0)
                    {
                        oBase = txtCorrelativo;
                        throw new ArgumentException("Ingrese Correlativo");
                    }
                    else
                        obeDatAdic.doxpc_corre_aduana = txtCorrelativo.Text;
                }
                else
                    obeDatAdic.doxpc_corre_aduana = null;

                if (txtNumFormulario.Properties.ReadOnly == false)
                {
                    if (Convert.ToInt32(txtNumFormulario.Text) == 0)
                    {
                        oBase = txtNumFormulario;
                        throw new ArgumentException("Ingrese Nº Formulario");
                    }
                }//falta campo

                if (txtNumSerieDoc.Properties.ReadOnly == false)
                {
                    if (Convert.ToInt32(txtNumSerieDoc.Text) == 0)
                    {
                        oBase = txtNumSerieDoc;
                        throw new ArgumentException("Ingrese Nº Serie del documento");
                    }
                    else
                    {
                        obeDatAdic.doxpc_num_serie = txtNumSerieDoc.Text;
                    }
                }
                else
                    obeDatAdic.doxpc_num_serie = null;

                if (txtNumTicket.Properties.ReadOnly == false)
                {
                    if (txtNumTicket.Text.TrimStart().TrimEnd() == string.Empty)
                    {
                        oBase = txtNumTicket;
                        throw new ArgumentException("Ingrese Nº del Ticket");
                    }
                    else
                    {
                        obeDatAdic.doxpc_num_doc_domiciliado = txtNumTicket.Text;
                    }
                }
                else
                    obeDatAdic.doxpc_num_doc_domiciliado = null;

                if (txtNumOrdenForm.Properties.ReadOnly == false)
                {
                    if (Convert.ToInt32(txtNumOrdenForm.Text) == 0)
                    {
                        oBase = txtNumOrdenForm;
                        throw new ArgumentException("Ingrese Nº orden del formulario");
                    }
                }//falta campo

                if (txtNumCorrelDoc.Properties.ReadOnly == false)
                {
                    if (Convert.ToInt32(txtNumCorrelDoc.Text) == 0)
                    {
                        oBase = txtNumCorrelDoc;
                        throw new ArgumentException("Ingrese Nº correlativo del documento");
                    }
                    else
                        obeDatAdic.doxpc_num_doc_domiciliado = txtNumCorrelDoc.Text;
                }
                else
                    obeDatAdic.doxpc_num_doc_domiciliado = null;

                if (txtNombre.Properties.ReadOnly == false)
                {
                    if (txtNombre.Text.TrimStart().TrimEnd() == string.Empty)
                    {
                        oBase = txtNombre;
                        throw new ArgumentException("Ingrese nombre del proveedor");
                    }
                    else
                        obeDatAdic.proc_vnombre = txtNombre.Text;
                }
                else
                    obeDatAdic.proc_vnombre = null;

                if (txtApPaterno.Properties.ReadOnly == false)
                {
                    if (txtApPaterno.Text.TrimStart().TrimEnd() == string.Empty)
                    {
                        oBase = txtApPaterno;
                        throw new ArgumentException("Ingrese apellido paterno del proveedor");
                    }
                    else
                        obeDatAdic.proc_vpaterno = txtApPaterno.Text;
                }
                else
                    obeDatAdic.proc_vpaterno = null;

                if (txtApMaterno.Properties.ReadOnly == false)
                {
                    if (txtApMaterno.Text.TrimStart().TrimEnd() == string.Empty)
                    {
                        oBase = txtApMaterno;
                        throw new ArgumentException("Ingrese apellido paterno del proveedor");
                    }
                    else
                    {
                        obeDatAdic.proc_vmaterno = txtApMaterno.Text;
                    }
                }
                else
                    obeDatAdic.proc_vmaterno = null;

                if (txtIndDetraccion.Properties.ReadOnly == false)
                {
                    if (txtIndDetraccion.Text.TrimStart().TrimEnd() == string.Empty)
                    {
                        oBase = txtIndDetraccion;
                        throw new ArgumentException("Ingrese índice de detracción");
                    }
                    else
                    {
                        obeDatAdic.doxpc_ind_detraccion = Convert.ToInt32(txtIndDetraccion.Text);
                    }
                }
                else
                    obeDatAdic.doxpc_ind_detraccion = null;

                obeDatAdic.doxpc_tipo_compra = Convert.ToInt32(lkpTipocompra.EditValue);
                obeDatAdic.doxpc_tipo_comprobante = Convert.ToInt32(bteTipoComprobante.Tag);
                obeDatAdic.proc_tipo_persona = Convert.ToInt32(lkpTipoPersona.EditValue);
                obeDatAdic.doxpc_tipo_documento = Convert.ToInt32(lkptipodocumento.EditValue);
                obeDatAdic.doxpc_tipo_destino = Convert.ToInt32(lkpCodigoDestino.EditValue);
                obeDatAdic.doxpc_ind_detraccion = (chkIndDetraccion.Checked) ? 1 : 2;
                obeDatAdic.doxpc_ind_retencion = (chkIndRetencion.Checked) ? 1 : 2;

                Obl.InsertarDxPDatosAdicionales(obeDatAdic);
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
                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void BtnCancelar_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bteTipocomprobante_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarTipoComprobante(bteTipoComprobante);
        }

        private void ListarTipoComprobante(ButtonEdit obj)
        {
            //EDGAR////EDGAR//
            //EDGAR////EDGAR//
            //EDGAR////EDGAR//
            //EDGAR////EDGAR//
            //using (FrmListarTipoComprobantes frm = new FrmListarTipoComprobantes())
            //{
            //    frm.Carga();
            //    frm.ShowDialog();
            //    if (frm.DialogResult == DialogResult.OK)
            //    {
            //        obj.Tag = frm.obj.suntd_codigo;
            //        obj.Text = frm.obj.suntd_vdescripcion;
            //    }
            //}
            //txtNumTicket.Focus();
        }

        private void bteTipoComprobante_EditValueChanged(object sender, EventArgs e)
        {
            ValidarControles();
        }

        private void ValidarControles()
        {
            if (bteTipoComprobante.Tag != null)
            {
                int[] lstAduana = { 50, 52, 53, 54 };
                int[] lstNumFormulario = { 91, 98 };
                int[] lstNumSerieDoc = { 10, 12, 14, 50, 52, 53, 54, 91, 98 }; //no
                int[] lstNumCorrelDoc = { 12, 50, 52, 53, 54, 91, 98 }; //no
                int tipComp = Convert.ToInt32(bteTipoComprobante.Tag);

                if (lstAduana.Contains(tipComp))
                {
                    txtCodAduana.Properties.ReadOnly = false;
                    txtAnioDocAduana.Properties.ReadOnly = false;
                    txtCorrelativo.Properties.ReadOnly = false;
                }
                else
                {
                    txtCodAduana.Properties.ReadOnly = true;
                    txtAnioDocAduana.Properties.ReadOnly = true;
                    txtCorrelativo.Properties.ReadOnly = true;
                }

                if (lstNumFormulario.Contains(tipComp))
                {
                    txtNumFormulario.Properties.ReadOnly = false;
                    txtNumOrdenForm.Properties.ReadOnly = false;
                }
                else
                {
                    txtNumFormulario.Properties.ReadOnly = true;
                    txtNumOrdenForm.Properties.ReadOnly = true;
                }

                if (!lstNumSerieDoc.Contains(tipComp))
                    txtNumSerieDoc.Properties.ReadOnly = false;
                else
                    txtNumSerieDoc.Properties.ReadOnly = true;

                if (tipComp == 12)
                {
                    txtNumTicket.Properties.ReadOnly = false;
                    txtCodAduana.Properties.ReadOnly = true;
                    txtAnioDocAduana.Properties.ReadOnly = true;
                    txtCorrelativo.Properties.ReadOnly = true;
                }
                else
                    txtNumTicket.Properties.ReadOnly = true;

                if (!lstNumCorrelDoc.Contains(tipComp))
                    txtNumCorrelDoc.Properties.ReadOnly = false;
                else
                    txtNumCorrelDoc.Properties.ReadOnly = true;
            }
        }

        private void bteTipoComprobanteRef_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarTipoComprobante(bteTipoComprobanteRef);
        }

        private void chkIndDetraccion_CheckedChanged(object sender, EventArgs e)
        {
            txtIndDetraccion.Properties.ReadOnly = !chkIndDetraccion.Checked;
        }

    }
}