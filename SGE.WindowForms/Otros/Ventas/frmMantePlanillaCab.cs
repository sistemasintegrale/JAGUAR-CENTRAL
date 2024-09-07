using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmMantePlanillaCab : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        public EPlanillaCobranzaCab ObePlnCab = new EPlanillaCobranzaCab();

        List<EPlanillaCobranzaDet> lstPlanillas = new List<EPlanillaCobranzaDet>();


        public frmMantePlanillaCab()
        {
            InitializeComponent();
        }

        public BSMaintenanceStatus oState;
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

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            //if (Status == BSMaintenanceStatus.ModifyCurrent)
            //    {
            //  //  dteFecha.Enabled = false;
            //    }
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;

        }

        private void frm07VentaPorDia_Load(object sender, EventArgs e)
        {
            setFecha();
            cargar();
            viewPlanilla.Focus();
        }

        private void cargar()
        {
            lstPlanillas = new BVentas().listarPlanillaCobranzaDetalle(ObePlnCab.plnc_icod_planilla);
            grdPlanilla.DataSource = lstPlanillas;
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(43), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            setTotales();
            if (lstPlanillas.Count == 0)
                dteFecha.Enabled = true;
            else
                dteFecha.Enabled = false;
        }

        void reload(int intIcod, EPlanillaCobranzaCab oBePlnCabReload)
        {
            ObePlnCab = oBePlnCabReload;
            if (ObePlnCab.plnc_icod_planilla > 0)
                SetModify();
            cargar();

            int index = lstPlanillas.FindIndex(x => x.plnd_icod_detalle == intIcod);
            viewPlanilla.FocusedRowHandle = index;
            viewPlanilla.Focus();

            setTotales();

            modificarPlanillaCab();
        }

        private void setTotales()
        {
            /**FACTURACION**/
            txtImporte.Text = lstPlanillas.Where(x => x.tablc_iid_tipo_mov == Parametros.intPlnFacturacion && x.intSituacionFavBov != 4).ToList().Sum(x => x.plnd_nmonto).ToString();
            txtMonto.Text = lstPlanillas.Where(x => x.tablc_iid_tipo_mov == Parametros.intPlnFacturacion && x.intSituacionFavBov != 4).ToList().Sum(x => x.plnd_nmonto_pagado).ToString();

            /**PAGOS**/
            txtPagosTotalSol.Text = lstPlanillas.Where(x => x.tablc_iid_tipo_mov == Parametros.intPlnPago && x.tablc_iid_tipo_moneda == 3).ToList().Sum(x => x.plnd_nmonto_pagado).ToString();
            txtPagosTotalDol.Text = lstPlanillas.Where(x => x.tablc_iid_tipo_mov == Parametros.intPlnPago && x.tablc_iid_tipo_moneda == 4).ToList().Sum(x => x.plnd_nmonto_pagado).ToString();

            /**ANTICIPOS**/
            txtAnticipoTotalSol.Text = lstPlanillas.Where(x => x.tablc_iid_tipo_mov == Parametros.intPlnAnticipo && x.tablc_iid_tipo_moneda == 3).ToList().Sum(x => x.plnd_nmonto_pagado).ToString();
            txtAnticipoTotalDol.Text = lstPlanillas.Where(x => x.tablc_iid_tipo_mov == Parametros.intPlnAnticipo && x.tablc_iid_tipo_moneda == 4).ToList().Sum(x => x.plnd_nmonto_pagado).ToString();

        }

        public void setValues()
        {
            txtPlanilla.Text = ObePlnCab.plnc_vnumero_planilla;
            txtDesripción.Text = ObePlnCab.plnc_vobservaciones;
            txtImporte.Text = ObePlnCab.plnc_nmonto_importe.ToString();
            txtMonto.Text = ObePlnCab.plnc_nmonto_pagado.ToString();
            dteFecha.EditValue = ObePlnCab.plnc_sfecha_planilla;
        }


        private void setFecha()
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {

                if (DateTime.Now.Year == Parametros.intEjercicio)
                    dteFecha.EditValue = DateTime.Now;
                else
                    dteFecha.EditValue = Convert.ToDateTime("01/01/" + Parametros.intEjercicio.ToString());
            }
        }

        private void nuevo(int intOpcion)
        {
            try
            {
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    ObePlnCab.plnc_vnumero_planilla = txtPlanilla.Text;
                    ObePlnCab.plnc_sfecha_planilla = Convert.ToDateTime(dteFecha.EditValue);
                    ObePlnCab.plnc_vobservaciones = txtDesripción.Text;
                    ObePlnCab.intUsuario = Valores.intUsuario;
                    ObePlnCab.strPc = WindowsIdentity.GetCurrent().Name;
                    ObePlnCab.tblc_iid_situacion = Convert.ToInt32(lkpSituacion.EditValue);
                }

                if (intOpcion == 1) //FATURACION
                {
                    frmMantePlanillaCobranza frm = new frmMantePlanillaCobranza();
                    frm.MiEvento += new frmMantePlanillaCobranza.DelegadoMensaje(reload);
                    frm.ObePlnCab = ObePlnCab;
                    frm.SetInsert();
                    frm.dtFechaDoc.EditValue = dteFecha.EditValue;
                    frm.Show();
                    frm.lkpTipoDoc.EditValue = 9;
                    frm.dtFechaVenc.EditValue = dteFecha.EditValue;

                }
                else if (intOpcion == 2) //PAGO
                {
                    frmPlanillaCabPago frm = new frmPlanillaCabPago();
                    frm.MiEvento += new frmPlanillaCabPago.DelegadoMensaje(reload);
                    frm.oBePlnCab = ObePlnCab;
                    frm.SetInsert();
                    frm.dteFecha.EditValue = dteFecha.EditValue;
                    frm.Show();
                }
                else if (intOpcion == 3) //ANTICIPO
                {
                    frmManteAnticipo frm = new frmManteAnticipo();
                    frm.MiEvento += new frmManteAnticipo.DelegadoMensaje(reload);
                    frm.oBePlnCab = ObePlnCab;
                    frm.SetInsert();
                    frm.dteFecha.EditValue = dteFecha.EditValue;
                    frm.Show();
                    frm.txtNroAnticipo.Text = (lstPlanillas.Where(x => x.tablc_iid_tipo_mov == 3).ToList().Count + 1).ToString();

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificar(int intOpcion)
        {

            try
            {
                if (intOpcion == 1) // FACTURACION
                {
                    EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
                    frmMantePlanillaCobranza frm = new frmMantePlanillaCobranza();
                    frm.MiEvento += new frmMantePlanillaCobranza.DelegadoMensaje(reload);
                    frm.ObePlnCab = ObePlnCab;
                    frm.oBePln = oBe;
                    // frm.afecto2 = Convert.ToBoolean(frm.afectoIGV.Checked);
                    frm.SetModify();
                    frm.Show();
                    frm.setValues();
                    frm.BtnGuiaRemision.Enabled = false;
                    frm.lkpMoneda.Enabled = false;
                    frm.afectoIGV.Enabled = false;






                }
                else if (intOpcion == 2) //PAGO
                {
                    EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
                    frmPlanillaCabPago frm = new frmPlanillaCabPago();
                    frm.MiEvento += new frmPlanillaCabPago.DelegadoMensaje(reload);
                    frm.oBePlnCab = ObePlnCab;
                    frm.oBePlnDet = oBe;
                    frm.SetModify();
                    frm.Show();
                    frm.setValues();
                }
                else if (intOpcion == 3) //ANTICIPO
                {
                    EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
                    frmManteAnticipo frm = new frmManteAnticipo();
                    frm.MiEvento += new frmManteAnticipo.DelegadoMensaje(reload);
                    frm.oBePlnCab = ObePlnCab;
                    frm.oBePlnDet = oBe;
                    frm.SetModify();
                    frm.setValues();
                    frm.Show();

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminar(int intOpcion)
        {
            try
            {
                int index = viewPlanilla.FocusedRowHandle;

                if (intOpcion == Parametros.intPlnFacturacion)//FACTURACION
                {
                    EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
                    if (oBe == null)
                        return;

                    if (oBe.plnd_icod_tipo_doc == Parametros.intTipoDocFacturaVenta)
                    {
                        EFacturaCab obeFav = new EFacturaCab();

                        obeFav = new BVentas().getFacturaCab(Convert.ToInt32(oBe.plnd_icod_documento))[0];
                        /***********************************************/
                        new BVentas().eliminarFactura(obeFav, oBe);
                        reload(oBe.plnd_icod_detalle, ObePlnCab);
                        /***********************************************/

                    }
                    else if (oBe.plnd_icod_tipo_doc == Parametros.intTipoDocBoletaVenta)
                    {
                        EBoletaCab oBeBov = new BVentas().getBoletaCab(Convert.ToInt32(oBe.plnd_icod_documento))[0];

                        /***********************************************/
                        new BVentas().eliminarBoleta(oBeBov, oBe);
                        reload(oBe.plnd_icod_detalle, ObePlnCab);
                    }

                }
                else if (intOpcion == 2) //PAGO
                {
                    EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
                    //
                    EPagoDocVenta oBePgo = new EPagoDocVenta();
                    oBePgo.pgoc_dxc_icod_pago = Convert.ToInt64(oBe.pgoc_dxc_icod_pago);
                    oBePgo.pgoc_icod_pago = Convert.ToInt32(oBe.pgoc_icod_pago);
                    oBePgo.intUsuario = Valores.intUsuario;
                    oBePgo.strPc = WindowsIdentity.GetCurrent().Name;
                    //
                    new BVentas().eliminarPagoPln(ObePlnCab, oBe, oBePgo);
                    reload(0, ObePlnCab);
                }
                else if (intOpcion == 3) //ANTICIPO
                {
                    EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
                    //
                    EAnticipo oBeAntc = new EAnticipo();
                    oBeAntc.antc_icod_anticipo = Convert.ToInt32(oBe.antc_icod_anticipo);
                    oBeAntc.antc_icod_adelanto_cliente = Convert.ToInt32(oBe.plnd_icod_documento);
                    oBeAntc.intUsuario = Valores.intUsuario;
                    oBeAntc.strPc = WindowsIdentity.GetCurrent().Name;
                    //
                    new BVentas().eliminarAnticipoPln(ObePlnCab, oBe, oBeAntc);
                    reload(0, ObePlnCab);
                }
                /***********************************************/
                if (lstPlanillas.Count >= index + 1)
                    viewPlanilla.FocusedRowHandle = index;
                else
                    viewPlanilla.FocusedRowHandle = index - 1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void view(int intOpcion)
        {
            try
            {
                if (intOpcion == 1) // FACTURACION
                {
                    EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
                    frmMantePlanillaCobranza frm = new frmMantePlanillaCobranza();
                    frm.MiEvento += new frmMantePlanillaCobranza.DelegadoMensaje(reload);
                    frm.ObePlnCab = ObePlnCab;
                    frm.oBePln = oBe;
                    frm.SetModify();
                    frm.Show();
                    frm.setValues();


                }
                else if (intOpcion == 2) //PAGO
                {
                    EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
                    frmPlanillaCabPago frm = new frmPlanillaCabPago();
                    frm.MiEvento += new frmPlanillaCabPago.DelegadoMensaje(reload);
                    frm.oBePlnCab = ObePlnCab;
                    frm.oBePlnDet = oBe;
                    frm.SetCancel();
                    frm.Show();
                    frm.setValues();
                }
                else if (intOpcion == 3) //ANTICIPO
                {
                    EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
                    frmManteAnticipo frm = new frmManteAnticipo();
                    frm.MiEvento += new frmManteAnticipo.DelegadoMensaje(reload);
                    frm.oBePlnCab = ObePlnCab;
                    frm.oBePlnDet = oBe;
                    frm.SetCancel();
                    frm.Show();
                    frm.setValues();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (oBe == null)
                return;

            if (oBe.intSituacionFavBov == Parametros.intSitDocCobrarAnulado)
            {
                XtraMessageBox.Show("El documento se encuentra ANULADO", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (oBe.tablc_iid_tipo_mov == 1)
            {
                //var i = new BCuentasPorCobrar().getSituacionDocPorCobrar(Convert.ToInt64(oBe.intIcodDxc));
                //if (i != Parametros.intSitDocCobrarGenerado)
                //{
                //    XtraMessageBox.Show("El documento se encuentra, su situación es diferente de generado ", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    view(oBe.tablc_iid_tipo_mov);
                //}
                //else
                modificar(oBe.tablc_iid_tipo_mov);

            }
            else if (oBe.tablc_iid_tipo_mov == 3)// MOVIMIENTO DE ANTICIPO
            {
                //CUANDO ES ANTICIPO SE DEBE VERIFICAR SU SITUACION SI YA ESTA PAGADO O NO....
                int intSituacionADC = new BCuentasPorCobrar().getSituacionDocPorCobrar(Convert.ToInt64(oBe.intIcodDxc));
                if (intSituacionADC != Parametros.intSitDocCobrarGenerado)
                {
                    string strSituacion = (intSituacionADC == Parametros.intSitDocCobrarPagadoParcial) ? "PARC. PAGADO" : (intSituacionADC == Parametros.intSitDocCobrarCancelado) ? "CANCELADO" : "";
                    XtraMessageBox.Show(String.Format("El anticipo NO puede ser modificado, su situación es {0}", strSituacion), "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    DateTime dtHoy = Convert.ToDateTime("27/03/2014");
                    if (DateTime.Now.ToShortDateString() == dtHoy.ToShortDateString())
                        modificar(oBe.tablc_iid_tipo_mov);
                    else
                        view(oBe.tablc_iid_tipo_mov);
                }
                else
                {
                    modificar(oBe.tablc_iid_tipo_mov);
                }

            }
            else
                modificar(oBe.tablc_iid_tipo_mov);

        }

        private void facturaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo(1);
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (oBe == null)
                return;
            if (oBe.tablc_iid_tipo_mov == 3)// MOVIMIENTO DE ANTICIPO
            {
                //CUANDO ES ANTICIPO SE DEBE VERIFICAR SU SITUACION SI YA ESTA PAGADO O NO....
                int intSituacionADC = new BCuentasPorCobrar().getSituacionDocPorCobrar(Convert.ToInt64(oBe.intIcodDxc));
                if (intSituacionADC != Parametros.intSitDocCobrarGenerado)
                {
                    string strSituacion = (intSituacionADC == Parametros.intSitDocCobrarPagadoParcial) ? "PARC. PAGADO" : (intSituacionADC == Parametros.intSitDocCobrarCancelado) ? "CANCELADO" : "";
                    XtraMessageBox.Show(String.Format("El anticipo NO puede ser eliminado, su situación es {0}", strSituacion), "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                else
                {
                    if (XtraMessageBox.Show("Esta seguro que desea ELIMINAR el anticipo", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        eliminar(oBe.tablc_iid_tipo_mov);
                }
            }
            else
            {
                if (XtraMessageBox.Show("Esta seguro que desea ELIMINAR el registro", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    eliminar(oBe.tablc_iid_tipo_mov);
            }

        }

        private void pagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo(2);
        }

        private void anticipoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo(3);
        }

        private void frmMantePlanillaCab_FormClosing(object sender, FormClosingEventArgs e)
        {
            modificarPlanillaCab();
            MiEvento(ObePlnCab.plnc_icod_planilla);
        }

        private void modificarPlanillaCab()
        {
            if (ObePlnCab.plnc_icod_planilla == 0)
                return;
            ObePlnCab.plnc_sfecha_planilla = Convert.ToDateTime(dteFecha.EditValue);
            ObePlnCab.plnc_vobservaciones = txtDesripción.Text;
            ObePlnCab.plnc_nmonto_importe = Convert.ToDecimal(txtImporte.Text);
            ObePlnCab.plnc_nmonto_pagado = Convert.ToDecimal(txtMonto.Text);

            new BVentas().modificarPlanillaCab(ObePlnCab);
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void viewPlanilla_DoubleClick(object sender, EventArgs e)
        {
            EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (oBe == null)
                return;
            view(oBe.tablc_iid_tipo_mov);
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void anularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                if (oBe.tablc_iid_tipo_mov != Parametros.intPlnFacturacion)
                {
                    XtraMessageBox.Show("La anulación solo esta disponible para FACTURAS y BOLETAS", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                if (oBe.tablc_iid_tipo_mov == Parametros.intPlnFacturacion)
                {
                    if (oBe.intSituacionFavBov == Parametros.intSitDocCobrarAnulado)
                    {
                        XtraMessageBox.Show("El documento ya se encuentra ANULADO", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (XtraMessageBox.Show("Esta seguro que desea ANULAR el registro", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        if (oBe.plnd_icod_tipo_doc == Parametros.intTipoDocFacturaVenta)
                        {
                            EFacturaCab obeFav = new EFacturaCab();
                            obeFav.favc_icod_factura = Convert.ToInt32(oBe.plnd_icod_documento);
                            obeFav.intUsuario = Valores.intUsuario;
                            obeFav.strPc = WindowsIdentity.GetCurrent().Name;

                            new BVentas().anularFactura(obeFav);
                            reload(oBe.plnd_icod_detalle, ObePlnCab);
                        }
                        else if (oBe.plnd_icod_tipo_doc == Parametros.intTipoDocBoletaVenta)
                        {
                            EBoletaCab obeBov = new EBoletaCab();
                            obeBov.bovc_icod_boleta = Convert.ToInt32(oBe.plnd_icod_documento);
                            obeBov.intUsuario = Valores.intUsuario;
                            obeBov.strPc = WindowsIdentity.GetCurrent().Name;

                            new BVentas().anularBoleta(obeBov);
                            reload(oBe.plnd_icod_detalle, ObePlnCab);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void viewPlanilla_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string strSituacion = View.GetRowCellDisplayText(e.RowHandle, View.Columns["strSituacionFavBov"]);
                if (strSituacion == "ANULADO")
                {
                    e.Appearance.BackColor = Color.LightSalmon;
                    //e.Appearance.BackColor2 = Color.SeaShell;

                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            lstPlanillas.ForEach(x =>
            {
                if (x.tablc_iid_tipo_mov == 1)
                {
                    if (x.strSituacionFavBov != "ANULADO")
                    {
                        frmMantePlanillaCobranza frm = new frmMantePlanillaCobranza();
                        frm.MiEvento += new frmMantePlanillaCobranza.DelegadoMensaje(reload);
                        frm.ObePlnCab = ObePlnCab;
                        frm.oBePln = x;
                        frm.SetModify();
                        frm.Show();
                        frm.setValues();
                        frm.setsave2();
                    }
                    else if (x.strSituacionFavBov == "ANULADO")
                    {
                        if (x.plnd_icod_tipo_doc == Parametros.intTipoDocFacturaVenta)
                        {
                            EFacturaCab obeFav = new EFacturaCab();
                            obeFav.favc_icod_factura = Convert.ToInt32(x.plnd_icod_documento);
                            obeFav.intUsuario = Valores.intUsuario;
                            obeFav.strPc = WindowsIdentity.GetCurrent().Name;

                            new BVentas().anularFactura(obeFav);
                            //reload(x.plnd_icod_detalle, ObePlnCab);
                        }
                        else if (x.plnd_icod_tipo_doc == Parametros.intTipoDocBoletaVenta)
                        {
                            EBoletaCab obeBov = new EBoletaCab();
                            obeBov.bovc_icod_boleta = Convert.ToInt32(x.plnd_icod_documento);
                            obeBov.intUsuario = Valores.intUsuario;
                            obeBov.strPc = WindowsIdentity.GetCurrent().Name;

                            new BVentas().anularBoleta(obeBov);
                            //reload(oBe.plnd_icod_detalle, ObePlnCab);
                        }
                    }
                }

            });
        }

        private void facturaGrandeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (oBe == null)
                return;

            if (oBe.tablc_iid_tipo_mov != Parametros.intPlnFacturacion)
            {
                XtraMessageBox.Show("La impresión solo esta disponible para FACTURAS y BOLETAS", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (oBe.intSituacionFavBov == Parametros.intSitDocCobrarAnulado)
            {
                XtraMessageBox.Show("El documento se encuentra ANULADO", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (oBe.plnd_icod_tipo_doc == Parametros.intTipoDocFacturaVenta)
            {
                var oBeCab = new BVentas().getFacturaCab(Convert.ToInt32(oBe.plnd_icod_documento))[0];
                //
                string total = Convertir.ConvertNumeroEnLetras(oBeCab.favc_nmonto_total.ToString());

                var lstdet = new BVentas().listarFacturaDetalle(oBeCab.favc_icod_factura);
                rptFactura rpt = new rptFactura();
                rpt.cargar(oBeCab, lstdet, total, "", "", "");
                //using (FrmElegirImpresora frmImpresora = new FrmElegirImpresora())
                //{
                //    frmImpresora.cargar();
                //    frmImpresora.ckImpresora.Checked = true;
                //    if (frmImpresora.ShowDialog() == DialogResult.OK)
                //    {
                //        rptFactura rpt = new rptFactura();
                //        rpt.cargar(oBeCab, lstdet, total, "", "", "");
                //        //rpt.Print();
                //        rpt.Print(frmImpresora.url_impresora);
                //    }
                //}
            }
            else if (oBe.plnd_icod_tipo_doc == Parametros.intTipoDocBoletaVenta)
            {
                var oBeCab = new BVentas().getBoletaCab(Convert.ToInt32(oBe.plnd_icod_documento))[0];
                //
                string total = Convertir.ConvertNumeroEnLetras(oBeCab.bovc_nmonto_total.ToString());

                var lstdet = new BVentas().listarBoletaDetalle(oBeCab.bovc_icod_boleta);
                rptBoleta rpt = new rptBoleta();
                rpt.cargar(oBeCab, lstdet, total, "", "", "");
            }
        }

        private void facturaChicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (oBe == null)
                return;

            if (oBe.tablc_iid_tipo_mov != Parametros.intPlnFacturacion)
            {
                XtraMessageBox.Show("La impresión solo esta disponible para FACTURAS y BOLETAS", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (oBe.intSituacionFavBov == Parametros.intSitDocCobrarAnulado)
            {
                XtraMessageBox.Show("El documento se encuentra ANULADO", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (oBe.plnd_icod_tipo_doc == Parametros.intTipoDocFacturaVenta)
            {
                var oBeCab = new BVentas().getFacturaCab(Convert.ToInt32(oBe.plnd_icod_documento))[0];
                //
                string total = Convertir.ConvertNumeroEnLetras(oBeCab.favc_nmonto_total.ToString());

                var lstdet = new BVentas().listarFacturaDetalle(oBeCab.favc_icod_factura);
                rptFacturaChica rpt = new rptFacturaChica();
                rpt.cargar(oBeCab, lstdet, total, "", "", "");
                //using (FrmElegirImpresora frmImpresora = new FrmElegirImpresora())
                //{
                //    frmImpresora.cargar();
                //    frmImpresora.ckImpresora.Checked = true;
                //    if (frmImpresora.ShowDialog() == DialogResult.OK)
                //    {
                //        rptFacturaChica rpt = new rptFacturaChica();
                //        rpt.cargar(oBeCab, lstdet, total, "", "", "");
                //        //rpt.Print();
                //        rpt.Print(frmImpresora.url_impresora);
                //    }
                //}
            }
            else if (oBe.plnd_icod_tipo_doc == Parametros.intTipoDocBoletaVenta)
            {
                var oBeCab = new BVentas().getBoletaCab(Convert.ToInt32(oBe.plnd_icod_documento))[0];
                //
                string total = Convertir.ConvertNumeroEnLetras(oBeCab.bovc_nmonto_total.ToString());

                var lstdet = new BVentas().listarBoletaDetalle(oBeCab.bovc_icod_boleta);
                rptBoletaChica rpt = new rptBoletaChica();
                rpt.cargar(oBeCab, lstdet, total, "", "", "");
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }






    }
}