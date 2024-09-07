using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmPagoNotaCredito : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPagoNotaCredito));

        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;

        private List<ENotaCreditoPago> Lista = new List<ENotaCreditoPago>();

        public EDocXCobrar eDocXCobrar = new EDocXCobrar();

        private int xposition = 0;
        public int mes;

        public FrmPagoNotaCredito()
        {
            InitializeComponent();
        }

        void form2_MiEvento()
        {
            Carga();
        }

        void Modify()
        {
            Carga();
            grv.FocusedRowHandle = xposition;
        }

        private void FrmPagoNotaCredito_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MiEvento();
        }

        private void FrmPagoNotaCredito_Load(object sender, EventArgs e)
        {
            Carga();
            this.Text = "Pagos efectuados por Notas de Crédito de " + eDocXCobrar.Abreviatura + "-" + eDocXCobrar.doxcc_vnumero_doc;
            grv.GroupPanelText = "Pagos efectuados por Notas de Crédito de " + eDocXCobrar.Abreviatura + "-" + eDocXCobrar.doxcc_vnumero_doc;
        }

        private void Carga()
        {
            //Lista = (new BCuentasPorCobrar().ListarPagoNotaCredito(0, eDocXCobrar.doxcc_icod_correlativo, 0, Parametros.intEjercicio));
            Lista = (new BCuentasPorCobrar().ListarPagoNotaCredito2(eDocXCobrar.doxcc_icod_correlativo, Parametros.intEjercicio));
            grd.DataSource = Lista;
        }


        private void Nuevo_Click(object sender, EventArgs e)
        {
            if (eDocXCobrar.doxcc_nmonto_saldo == 0)
            {
                XtraMessageBox.Show("El documento ya se encuentra cancelado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                using (FrmMantePagoNotaCredito p = new FrmMantePagoNotaCredito())
                {
                    p.MiEvento += new FrmMantePagoNotaCredito.DelegadoMensaje(form2_MiEvento);
                    p.oBeDxc = eDocXCobrar;
                    p.saldoGralDxC = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_saldo);
                    grv.MoveLast();
                    p.SetInsert();
                    p.ShowDialog();
                }
            }
        }

        private void Modificar_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                Datos();
            }
            else
                XtraMessageBox.Show("No hay registro por modificar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void Datos()
        {
            ENotaCreditoPago Obe = (ENotaCreditoPago)grv.GetRow(grv.FocusedRowHandle);
            FrmMantePagoNotaCredito frm = new FrmMantePagoNotaCredito();
            frm.MiEvento += new FrmMantePagoNotaCredito.DelegadoMensaje(form2_MiEvento);

            if (Obe.tablc_iid_tipo_moneda == eDocXCobrar.tablc_iid_tipo_moneda)
            {
                frm.saldoGralDxC = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_saldo + Obe.ncpac_nmonto_pago);
                frm.pagoGralDxC = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_pagado - Obe.ncpac_nmonto_pago);
            }
            else
            {
                if (eDocXCobrar.tablc_iid_tipo_moneda == Parametros.intSoles)
                {
                    frm.saldoGralDxC = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_saldo + (Obe.ncpac_nmonto_pago * Obe.ncpac_nmonto_tipo_cambio));
                    frm.pagoGralDxC = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_pagado - (Obe.ncpac_nmonto_pago * Obe.ncpac_nmonto_tipo_cambio));
                }
                else
                {
                    frm.saldoGralDxC = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_saldo + (Obe.ncpac_nmonto_pago / Obe.ncpac_nmonto_tipo_cambio));
                    frm.pagoGralDxC = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_pagado - (Obe.ncpac_nmonto_pago / Obe.ncpac_nmonto_tipo_cambio));
                }
            }

            if (frm.saldoGralDxC > eDocXCobrar.doxcc_nmonto_total)
            {
                frm.saldoGralDxC = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_total);
                frm.pagoGralDxC = 0;
            }

            frm.oBeDxc = eDocXCobrar;
            frm.oBeDxcNc = new EDocXCobrar()
            {
                doxcc_icod_correlativo = Obe.doxcc_icod_correlativo_nota_credito,
                doxcc_vnumero_doc = Obe.vnumero_documento_NC,
                tablc_iid_tipo_moneda = Obe.tablc_iid_tipo_moneda,
                doxcc_nmonto_saldo = (Obe.SaldoDXCNotaCredito + Obe.ncpac_nmonto_pago),
                doxcc_nmonto_pagado = (Obe.doxcc_nmonto_pagado - Obe.ncpac_nmonto_pago),
                SimboloMoneda = Obe.SimboloMoneda,
                tdodc_iid_correlativo = Obe.iid_correlativo_nota_credito
            };
            frm.codDXCPagoNC = Obe.ncpac_icod_correlativo;
            frm.codDXCPago = Convert.ToInt64(Obe.pdxcc_icod_correlativo);
            frm.bteDXCNotaCredito.Tag = Obe.doxcc_icod_correlativo_nota_credito;
            frm.bteDXCNotaCredito.Text = Obe.vnumero_documento_NC;
            frm.deFechaDocumento.EditValue = Obe.ncpac_sfecha_pago;
            frm.lblMoneda.Text = Obe.SimboloMoneda;
            frm.txtMonto.Text = Obe.ncpac_nmonto_pago.ToString();
            frm.txtTipoCambio.Text = Obe.ncpac_nmonto_tipo_cambio.ToString();
            frm.txtObservacion.Text = Obe.ncpac_vdescripcion;

            frm.SetModify();
            frm.ShowDialog();
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                ENotaCreditoPago ObeNC = (ENotaCreditoPago)grv.GetRow(grv.FocusedRowHandle);
                EDocXCobrarPago Obe = new EDocXCobrarPago()
                {
                    pdxcc_icod_correlativo = Convert.ToInt64(ObeNC.pdxcc_icod_correlativo),
                    doxcc_icod_correlativo = ObeNC.doxcc_icod_correlativo_pago,
                    doxcc_icod_correlativo_pago = ObeNC.doxcc_icod_correlativo_nota_credito,
                    pdxcc_nmonto_cobro = ObeNC.ncpac_nmonto_pago,
                    pdxcc_nmonto_tipo_cambio = ObeNC.ncpac_nmonto_tipo_cambio,
                    tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente,
                    tablc_iid_tipo_moneda = ObeNC.tablc_iid_tipo_moneda
                };
                if (XtraMessageBox.Show("¿Está seguro de Eliminar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    EliminarPago(ObeNC, Obe);
                Lista.Remove(ObeNC);
                grv.RefreshData();
            }
            else
                XtraMessageBox.Show("No hay registro por eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void EliminarPago(ENotaCreditoPago ObeNC, EDocXCobrarPago Obe)
        {
            if (Obe.tablc_iid_tipo_moneda == eDocXCobrar.tablc_iid_tipo_moneda)
            {
                Obe.saldoDxP = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_saldo + Obe.pdxcc_nmonto_cobro);
                Obe.pagoDxP = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_pagado - Obe.pdxcc_nmonto_cobro);
            }
            else
            {
                if (eDocXCobrar.tablc_iid_tipo_moneda == Parametros.intSoles)
                {
                    Obe.saldoDxP = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_saldo + (Obe.pdxcc_nmonto_cobro * Obe.pdxcc_nmonto_tipo_cambio));
                    Obe.pagoDxP = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_pagado - (Obe.pdxcc_nmonto_cobro * Obe.pdxcc_nmonto_tipo_cambio));
                }
                else
                {
                    Obe.saldoDxP = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_saldo + (Obe.pdxcc_nmonto_cobro / Obe.pdxcc_nmonto_tipo_cambio));
                    Obe.pagoDxP = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_pagado - (Obe.pdxcc_nmonto_cobro / Obe.pdxcc_nmonto_tipo_cambio));
                }
            }

            if (Obe.saldoDxP > eDocXCobrar.doxcc_nmonto_total)
            {
                Obe.saldoDxP = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_total);
                Obe.pagoDxP = 0;
            }
            Obe.strPc = WindowsIdentity.GetCurrent().Name;
            Obe.intUsuario = Valores.intUsuario;
            ObeNC.ncpac_vpc_modifica = WindowsIdentity.GetCurrent().Name;
            ObeNC.ncpac_iusuario_modifica = Valores.intUsuario;
            new BCuentasPorCobrar().EliminarPagoNotaCredito(ObeNC, Obe);
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


    }
}