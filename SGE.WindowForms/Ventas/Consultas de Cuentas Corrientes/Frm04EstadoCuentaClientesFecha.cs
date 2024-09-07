using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Cuentas_por_Cobrar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Consultas_de_Cuentas_Corrientes
{
    public partial class Frm04EstadoCuentaClientesFecha : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        List<ECliente> Lista = new List<ECliente>();
        public Frm04EstadoCuentaClientesFecha()
        {
            InitializeComponent();
        }

        private void FrmEstadoCuentaClientesFecha_Load(object sender, EventArgs e)
        {
            deInicio.EditValue = DateTime.Now;
            this.ActivarControles();
        }
        private void Cargar()
        {
            Lista = new BCuentasPorCobrar().ListarClientesSaldosAUnaFecha(Convert.ToDateTime(deInicio.EditValue), Parametros.intEjercicio);
            dgr.DataSource = Lista;
            ActivarControles();
        }
        private void ActivarControles()
        {
            if (Lista.Count == 0)
            {
                rdTipoMovimiento.Enabled = false;
                txtcodigo.Properties.ReadOnly = true;
                txtNombre.Properties.ReadOnly = true;
            }
            else
            {
                rdTipoMovimiento.Enabled = true;
                txtcodigo.Properties.ReadOnly = false;
                txtNombre.Properties.ReadOnly = false;
            }
        }

        void form2_MiEvento()
        {
            Cargar();
        }
        private void todos_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                ECliente eCliente = (ECliente)view.GetRow(view.FocusedRowHandle);

                FrmConsultarDocXCobrarClienteAUnaFecha dxc = new FrmConsultarDocXCobrarClienteAUnaFecha();
                dxc.MiEvento += new FrmConsultarDocXCobrarClienteAUnaFecha.DelegadoMensaje(form2_MiEvento);
                dxc.eCliente = eCliente;
                dxc.sfecha = Convert.ToDateTime(deInicio.EditValue);
                dxc.filtro = false;
                dxc.mnu.Items[1].Visible = true;
                dxc.mnu.Items[2].Visible = true;
                dxc.Show();
                xposition = view.FocusedRowHandle;
            }
            else
                XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void pendientes_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                ECliente eCliente = (ECliente)view.GetRow(view.FocusedRowHandle);

                FrmConsultarDocXCobrarClienteAUnaFecha dxc = new FrmConsultarDocXCobrarClienteAUnaFecha();
                dxc.MiEvento += new FrmConsultarDocXCobrarClienteAUnaFecha.DelegadoMensaje(form2_MiEvento);
                dxc.eCliente = eCliente;
                dxc.sfecha = Convert.ToDateTime(deInicio.EditValue);
                dxc.filtro = true;
                dxc.mnu.Items[1].Visible = true;
                dxc.mnu.Items[2].Visible = true;
                dxc.Show();
                xposition = view.FocusedRowHandle;
            }
            else
                XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void deInicio_EditValueChanged(object sender, EventArgs e)
        {
            this.Cargar();
        }

        private void EstadoCuenta_Click(object sender, EventArgs e)
        {

        }

        private void imprimirLista_Click(object sender, EventArgs e)
        {

            if (Lista.Count > 0)
            {
                List<ECliente> listaTempCliente = new List<ECliente>();
                rptEstadoCuentaClienteLista rpt = new rptEstadoCuentaClienteLista();
                if (rdTipoMovimiento.SelectedIndex == 0)
                {
                    listaTempCliente = Lista.Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
                }
                else
                {
                    listaTempCliente = Lista.Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd()) && ob.giroc_icod_giro == 3).ToList();
                }
                rpt.cargar(listaTempCliente, Parametros.intEjercicio.ToString(), "AL " + deInicio.Text);
            }
            else
                XtraMessageBox.Show("No hay registro por reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void imprimirConDocumentos_Click(object sender, EventArgs e)
        {
            List<EDocXCobrar> listaTempCliente = new List<EDocXCobrar>();
            if (Lista.Count > 0)
            {
                rptEstadoCuentaDocumentos rpt = new rptEstadoCuentaDocumentos();
                if (rdTipoMovimiento.SelectedIndex == 0)
                {
                    listaTempCliente = new BCuentasPorCobrar().EstadoCuentaDocumentosAUnaFecha(Parametros.intEjercicio, Convert.ToDateTime(deInicio.EditValue)).Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
                }
                else
                {
                    listaTempCliente = new BCuentasPorCobrar().EstadoCuentaDocumentosAUnaFecha(Parametros.intEjercicio, Convert.ToDateTime(deInicio.EditValue)).Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd()) && ob.giroc_icod_giro == 3).ToList();
                }

                rpt.cargar(listaTempCliente, Parametros.intEjercicio.ToString(), false, "AL " + deInicio.Text);
            }
            else
                XtraMessageBox.Show("No hay registro por reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void imprimirSoloPendientes_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                List<EDocXCobrar> mlistaDoxcobrar = new List<EDocXCobrar>();

                if (rdTipoMovimiento.SelectedIndex == 0)
                {
                    mlistaDoxcobrar = new BCuentasPorCobrar().EstadoCuentaDocumentosAUnaFecha(Parametros.intEjercicio, Convert.ToDateTime(deInicio.EditValue)).Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
                }
                else
                {

                    mlistaDoxcobrar = new BCuentasPorCobrar().EstadoCuentaDocumentosAUnaFecha(Parametros.intEjercicio, Convert.ToDateTime(deInicio.EditValue)).Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd()) && ob.giroc_icod_giro == 3).ToList();
                }
                //mlistaDoxcobrar = new BDocumentoXCobrar().EstadoCuentaDocumentos(Parametros.intPeriodo).Where(dxc => dxc.tablc_iid_situacion_documento == 108 || dxc.tablc_iid_situacion_documento == 109).ToList();
                if (mlistaDoxcobrar.Count > 0)
                {
                    rptEstadoCuentaDocumentos rpt = new rptEstadoCuentaDocumentos();
                    rpt.cargar(mlistaDoxcobrar, Parametros.intEjercicio.ToString(), true, "AL " + deInicio.Text);
                }
            }
            else
                XtraMessageBox.Show("No hay registro por reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void imprimirListaDudosa_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                List<ECliente> listaTempCliente = new List<ECliente>();
                listaTempCliente = new BCuentasPorCobrar().ListarClientesSaldosCobranzaDudosaAUnaFecha(Parametros.intEjercicio, Convert.ToDateTime(deInicio.EditValue));

                if (rdTipoMovimiento.SelectedIndex == 0)
                {
                    listaTempCliente = listaTempCliente.Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
                }
                else
                {
                    listaTempCliente = listaTempCliente.Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd()) && ob.giroc_icod_giro == 3).ToList();
                }

                if (listaTempCliente.Count > 0)
                {
                    rptEstadoCuentaClienteLista rpt = new rptEstadoCuentaClienteLista();
                    rpt.cargar(listaTempCliente, Parametros.intEjercicio.ToString(), "AL " + deInicio.Text);
                }
            }
            else
                XtraMessageBox.Show("No hay registro por reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void imprimirSóloPendientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                List<EDocXCobrar> mlista = new List<EDocXCobrar>();
                mlista = new BCuentasPorCobrar().EstadoCuentaDocumentosAUnaFecha(Parametros.intEjercicio, Convert.ToDateTime(deInicio.EditValue)).Where(dxc => dxc.idTipodocuemnto == 19).ToList();

                if (rdTipoMovimiento.SelectedIndex == 0)
                {
                    mlista = mlista.Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
                }
                else
                {
                    mlista = mlista.Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd()) && ob.giroc_icod_giro == 3).ToList();
                }

                if (mlista.Count > 0)
                {
                    rptEstadoCuentaDocumentos rpt = new rptEstadoCuentaDocumentos();
                    rpt.cargar(mlista, Parametros.intEjercicio.ToString(), true, "AL " + deInicio.Text);
                }
            }
            else
                XtraMessageBox.Show("No hay registro por reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void imprimirConDocumentosDudosa_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                List<EDocXCobrar> mlistCuentasDoc = new List<EDocXCobrar>();
                mlistCuentasDoc = new BCuentasPorCobrar().EstadoCuentaDocumentosAUnaFecha(Parametros.intEjercicio, Convert.ToDateTime(deInicio.EditValue));

                if (rdTipoMovimiento.SelectedIndex == 0)
                {
                    mlistCuentasDoc = mlistCuentasDoc.Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
                }
                else
                {
                    mlistCuentasDoc = mlistCuentasDoc.Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd()) && ob.giroc_icod_giro == 3).ToList();
                }

                if (mlistCuentasDoc.Count > 0)
                {
                    rptEstadoCuentaDocumentos rpt = new rptEstadoCuentaDocumentos();
                    rpt.cargar(mlistCuentasDoc, Parametros.intEjercicio.ToString(), false, "AL " + deInicio.Text);
                }
            }
            else
                XtraMessageBox.Show("No hay registro por reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void rdTipoMovimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ECliente> listaTempCliente = new List<ECliente>();
            if (rdTipoMovimiento.SelectedIndex == 0)
            {
                listaTempCliente = Lista.Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
            }
            else
            {
                listaTempCliente = Lista.Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd()) && ob.giroc_icod_giro == 3).ToList();
            }
            dgr.DataSource = listaTempCliente;
        }

        private void txtcodigo_EditValueChanged(object sender, EventArgs e)
        {
            List<ECliente> listaTempCliente = new List<ECliente>();
            listaTempCliente = Lista.Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
            dgr.DataSource = listaTempCliente;
        }

        private void txtNombre_EditValueChanged(object sender, EventArgs e)
        {
            if (txtNombre.ContainsFocus)
            {
                List<ECliente> listaTempCliente = new List<ECliente>();
                listaTempCliente = Lista.Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
                dgr.DataSource = listaTempCliente;
            }
        }

        private void cobranzaPorRangoDeDíasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                List<ECliente> lstCliente = new List<ECliente>();

                foreach (var item in Lista)
                {
                    List<ETipoCambio> lstTipoCambio = new List<ETipoCambio>();
                    lstTipoCambio = new BAdministracionSistema().listarTipoCambio().Where(q => q.ticac_fecha_tipo_cambio.Date == deInicio.DateTime.Date).ToList();
                    item.MontoUS = item.doxcc_nmonto_saldo_dolares + Convert.ToDecimal(item.doxcc_nmonto_saldo_soles / lstTipoCambio[0].ticac_tipo_cambio_venta);
                    List<EDocXCobrar> lstDocumentopoCobrar = new List<EDocXCobrar>();
                    lstDocumentopoCobrar = new BCuentasPorCobrar().BuscarDocumentosXCobrarClienteAUnaFecha(item.cliec_icod_cliente, Parametros.intEjercicio, deInicio.DateTime);
                    item.dias_0_30 = 0;
                    item.dias_31_60 = 0;
                    item.dias_61_90 = 0;
                    item.dias_91_120 = 0;
                    item.dias_121_180 = 0;
                    item.dias_181_mas = 0;
                    int cont = 0;
                    foreach (var obe in lstDocumentopoCobrar)
                    {
                        cont = cont + 1;
                        DateTime fecha30dias, fecha60dias, fecha90dias, fecha120dias, fecha180dias;
                        fecha30dias = deInicio.DateTime.AddDays(-30);
                        fecha60dias = deInicio.DateTime.AddDays(-60);
                        fecha90dias = deInicio.DateTime.AddDays(-90);
                        fecha120dias = deInicio.DateTime.AddDays(-120);
                        fecha180dias = deInicio.DateTime.AddDays(-180);

                        if (fecha30dias <= obe.doxcc_sfecha_doc)
                        {
                            if (obe.tdocc_icod_tipo_doc == 83 || obe.tdocc_icod_tipo_doc == 36)
                            {
                                if (obe.tablc_iid_tipo_moneda == 3)
                                {
                                    item.dias_0_30 = item.dias_0_30 - (obe.doxcc_nmonto_saldo / lstTipoCambio[0].ticac_tipo_cambio_venta);
                                }
                                else
                                {
                                    item.dias_0_30 = item.dias_0_30 - obe.doxcc_nmonto_saldo;
                                }
                            }
                            else
                            {
                                if (obe.tablc_iid_tipo_moneda == 3)
                                {
                                    item.dias_0_30 = item.dias_0_30 + (obe.doxcc_nmonto_saldo / lstTipoCambio[0].ticac_tipo_cambio_venta);
                                }
                                else
                                {
                                    item.dias_0_30 = item.dias_0_30 + obe.doxcc_nmonto_saldo;
                                }
                            }



                        }
                        else if (fecha60dias <= obe.doxcc_sfecha_doc)
                        {

                            if (obe.tdocc_icod_tipo_doc == 83 || obe.tdocc_icod_tipo_doc == 36)
                            {
                                if (obe.tablc_iid_tipo_moneda == 3)
                                {
                                    item.dias_31_60 = item.dias_31_60 - (obe.doxcc_nmonto_saldo / lstTipoCambio[0].ticac_tipo_cambio_venta);
                                }
                                else
                                {
                                    item.dias_31_60 = item.dias_31_60 - obe.doxcc_nmonto_saldo;
                                }
                            }
                            else
                            {
                                if (obe.tablc_iid_tipo_moneda == 3)
                                {
                                    item.dias_31_60 = item.dias_31_60 + (obe.doxcc_nmonto_saldo / lstTipoCambio[0].ticac_tipo_cambio_venta);
                                }
                                else
                                {
                                    item.dias_31_60 = item.dias_31_60 + obe.doxcc_nmonto_saldo;
                                }
                            }


                        }
                        else if (fecha90dias <= obe.doxcc_sfecha_doc)
                        {

                            if (obe.tdocc_icod_tipo_doc == 83 || obe.tdocc_icod_tipo_doc == 36)
                            {
                                if (obe.tablc_iid_tipo_moneda == 3)
                                {
                                    item.dias_61_90 = item.dias_61_90 - (obe.doxcc_nmonto_saldo / lstTipoCambio[0].ticac_tipo_cambio_venta);
                                }
                                else
                                {
                                    item.dias_61_90 = item.dias_61_90 - obe.doxcc_nmonto_saldo;
                                }
                            }
                            else
                            {
                                if (obe.tablc_iid_tipo_moneda == 3)
                                {
                                    item.dias_61_90 = item.dias_61_90 + (obe.doxcc_nmonto_saldo / lstTipoCambio[0].ticac_tipo_cambio_venta);
                                }
                                else
                                {
                                    item.dias_61_90 = item.dias_61_90 + obe.doxcc_nmonto_saldo;
                                }
                            }


                        }
                        else if (fecha120dias <= obe.doxcc_sfecha_doc)
                        {
                            if (obe.tdocc_icod_tipo_doc == 83 || obe.tdocc_icod_tipo_doc == 36)
                            {
                                if (obe.tablc_iid_tipo_moneda == 3)
                                {
                                    item.dias_91_120 = item.dias_91_120 - (obe.doxcc_nmonto_saldo / lstTipoCambio[0].ticac_tipo_cambio_venta);
                                }
                                else
                                {
                                    item.dias_91_120 = item.dias_91_120 - obe.doxcc_nmonto_saldo;
                                }
                            }
                            else
                            {
                                if (obe.tablc_iid_tipo_moneda == 3)
                                {
                                    item.dias_91_120 = item.dias_91_120 + (obe.doxcc_nmonto_saldo / lstTipoCambio[0].ticac_tipo_cambio_venta);
                                }
                                else
                                {
                                    item.dias_91_120 = item.dias_91_120 + obe.doxcc_nmonto_saldo;
                                }
                            }


                        }
                        else if (fecha180dias <= obe.doxcc_sfecha_doc)
                        {
                            if (obe.tdocc_icod_tipo_doc == 83 || obe.tdocc_icod_tipo_doc == 36)
                            {
                                if (obe.tablc_iid_tipo_moneda == 3)
                                {
                                    item.dias_121_180 = item.dias_121_180 - (obe.doxcc_nmonto_saldo / lstTipoCambio[0].ticac_tipo_cambio_venta);
                                }
                                else
                                {
                                    item.dias_121_180 = item.dias_121_180 - obe.doxcc_nmonto_saldo;
                                }
                            }
                            else
                            {
                                if (obe.tablc_iid_tipo_moneda == 3)
                                {
                                    item.dias_121_180 = item.dias_121_180 + (obe.doxcc_nmonto_saldo / lstTipoCambio[0].ticac_tipo_cambio_venta);
                                }
                                else
                                {
                                    item.dias_121_180 = item.dias_121_180 + obe.doxcc_nmonto_saldo;
                                }
                            }



                        }
                        else //if (  fecha180dias < obe.doxcc_sfecha_doc)
                        {
                            if (obe.tdocc_icod_tipo_doc == 83 || obe.tdocc_icod_tipo_doc == 36)
                            {
                                if (obe.tablc_iid_tipo_moneda == 3)
                                {
                                    item.dias_181_mas = item.dias_181_mas - (obe.doxcc_nmonto_saldo / lstTipoCambio[0].ticac_tipo_cambio_venta);
                                }
                                else
                                {
                                    item.dias_181_mas = item.dias_181_mas - obe.doxcc_nmonto_saldo;
                                }
                            }
                            else
                            {
                                if (obe.tablc_iid_tipo_moneda == 3)
                                {
                                    item.dias_181_mas = item.dias_181_mas + (obe.doxcc_nmonto_saldo / lstTipoCambio[0].ticac_tipo_cambio_venta);
                                }
                                else
                                {
                                    item.dias_181_mas = item.dias_181_mas + obe.doxcc_nmonto_saldo;
                                }
                            }


                        }

                        if (cont == lstDocumentopoCobrar.Count())
                        {
                            if (item.dias_181_mas == 0)
                            {
                                item.dias_181_mas = null;
                            }


                            if (item.dias_121_180 == 0)
                            {
                                item.dias_121_180 = null;
                            }

                            if (item.dias_91_120 == 0)
                            {
                                item.dias_91_120 = null;
                            }

                            if (item.dias_61_90 == 0)
                            {
                                item.dias_61_90 = null;
                            }

                            if (item.dias_31_60 == 0)
                            {
                                item.dias_31_60 = null;
                            }

                            if (item.dias_0_30 == 0)
                            {
                                item.dias_0_30 = null;
                            }
                        }


                    }

                    lstCliente.Add(item);
                }
                grdCobranzaporRango.DataSource = lstCliente;

                rptEstadoCuentaRangoFecha rpt = new rptEstadoCuentaRangoFecha();
                rpt.cargar(deInicio.DateTime.Date, lstCliente);

            }
            catch (Exception ex)
            {

                XtraMessageBox.Show("Error No tiene Registrado el Tipo de Cambio del " + deInicio.DateTime.ToShortDateString(), "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void exportarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                List<ECliente> lstCliente = new List<ECliente>();

                foreach (var item in Lista)
                {
                    List<ETipoCambio> lstTipoCambio = new List<ETipoCambio>();
                    lstTipoCambio = new BAdministracionSistema().listarTipoCambio().Where(q => q.ticac_fecha_tipo_cambio.Date == deInicio.DateTime.Date).ToList();
                    item.MontoUS = item.doxcc_nmonto_saldo_dolares + Convert.ToDecimal(item.doxcc_nmonto_saldo_soles / lstTipoCambio[0].ticac_tipo_cambio_venta);
                    List<EDocXCobrar> lstDocumentopoCobrar = new List<EDocXCobrar>();
                    lstDocumentopoCobrar = new BCuentasPorCobrar().BuscarDocumentosXCobrarClienteAUnaFecha(item.cliec_icod_cliente, Parametros.intEjercicio, deInicio.DateTime);
                    item.dias_0_30 = 0;
                    item.dias_31_60 = 0;
                    item.dias_61_90 = 0;
                    item.dias_91_120 = 0;
                    item.dias_121_180 = 0;
                    item.dias_181_mas = 0;
                    int cont = 0;
                    foreach (var obe in lstDocumentopoCobrar)
                    {
                        cont = cont + 1;
                        DateTime fecha30dias, fecha60dias, fecha90dias, fecha120dias, fecha180dias;
                        fecha30dias = deInicio.DateTime.AddDays(-30);
                        fecha60dias = deInicio.DateTime.AddDays(-60);
                        fecha90dias = deInicio.DateTime.AddDays(-90);
                        fecha120dias = deInicio.DateTime.AddDays(-120);
                        fecha180dias = deInicio.DateTime.AddDays(-180);

                        if (fecha30dias <= obe.doxcc_sfecha_doc)
                        {
                            if (obe.tablc_iid_tipo_moneda == 3)
                            {
                                item.dias_0_30 = item.dias_0_30 + (obe.doxcc_nmonto_saldo / obe.doxcc_nmonto_tipo_cambio);
                            }
                            else
                            {
                                item.dias_0_30 = item.dias_0_30 + obe.doxcc_nmonto_saldo;
                            }


                        }
                        else if (fecha60dias <= obe.doxcc_sfecha_doc)
                        {
                            if (obe.tablc_iid_tipo_moneda == 3)
                            {
                                item.dias_31_60 = item.dias_31_60 + (obe.doxcc_nmonto_saldo / obe.doxcc_nmonto_tipo_cambio);
                            }
                            else
                            {
                                item.dias_31_60 = item.dias_31_60 + obe.doxcc_nmonto_saldo;
                            }

                        }
                        else if (fecha90dias <= obe.doxcc_sfecha_doc)
                        {
                            if (obe.tablc_iid_tipo_moneda == 3)
                            {
                                item.dias_61_90 = item.dias_61_90 + (obe.doxcc_nmonto_saldo / obe.doxcc_nmonto_tipo_cambio);
                            }
                            else
                            {
                                item.dias_61_90 = item.dias_61_90 + obe.doxcc_nmonto_saldo;
                            }

                        }
                        else if (fecha120dias <= obe.doxcc_sfecha_doc)
                        {
                            if (obe.tablc_iid_tipo_moneda == 3)
                            {
                                item.dias_91_120 = item.dias_91_120 + (obe.doxcc_nmonto_saldo / obe.doxcc_nmonto_tipo_cambio);
                            }
                            else
                            {
                                item.dias_91_120 = item.dias_91_120 + obe.doxcc_nmonto_saldo;
                            }

                        }
                        else if (fecha180dias <= obe.doxcc_sfecha_doc)
                        {
                            if (obe.tablc_iid_tipo_moneda == 3)
                            {
                                item.dias_121_180 = item.dias_121_180 + (obe.doxcc_nmonto_saldo / obe.doxcc_nmonto_tipo_cambio);
                            }
                            else
                            {
                                item.dias_121_180 = item.dias_121_180 + obe.doxcc_nmonto_saldo;
                            }

                        }
                        else //if (  fecha180dias < obe.doxcc_sfecha_doc)
                        {
                            if (obe.tablc_iid_tipo_moneda == 3)
                            {
                                item.dias_181_mas = item.dias_181_mas + (obe.doxcc_nmonto_saldo / obe.doxcc_nmonto_tipo_cambio);
                            }
                            else
                            {
                                item.dias_181_mas = item.dias_181_mas + obe.doxcc_nmonto_saldo;
                            }

                        }

                        if (cont == lstDocumentopoCobrar.Count())
                        {
                            if (item.dias_181_mas == 0)
                            {
                                item.dias_181_mas = null;
                            }


                            if (item.dias_121_180 == 0)
                            {
                                item.dias_121_180 = null;
                            }

                            if (item.dias_91_120 == 0)
                            {
                                item.dias_91_120 = null;
                            }

                            if (item.dias_61_90 == 0)
                            {
                                item.dias_61_90 = null;
                            }

                            if (item.dias_31_60 == 0)
                            {
                                item.dias_31_60 = null;
                            }

                            if (item.dias_0_30 == 0)
                            {
                                item.dias_0_30 = null;
                            }
                        }

                    }

                    lstCliente.Add(item);
                }
                grdCobranzaporRango.DataSource = lstCliente;

                if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                {
                    grdCobranzaporRango.DataSource = lstCliente;//.OrderBy(obe => obe.strNroCuenta).ThenBy(obe => obe.anac_cecoc_tipo).ThenBy(obe => obe.anac_cecoc_code).ThenBy(obe => obe.fec_cab).ToList();
                    string fileName = sfdRuta.FileName;
                    if (!fileName.Contains(".xlsx"))
                    {
                        grdCobranzaporRango.ExportToXlsx(fileName + ".xlsx");
                        System.Diagnostics.Process.Start(fileName + ".xlsx");
                    }
                    else
                    {
                        grdCobranzaporRango.ExportToXlsx(fileName);
                        System.Diagnostics.Process.Start(fileName);
                    }
                    grdCobranzaporRango.DataSource = null;
                    sfdRuta.FileName = string.Empty;
                }

            }
            catch (Exception ex)
            {

                XtraMessageBox.Show("Error No tiene Registrado el Tipo de Cambio del " + deInicio.DateTime.ToShortDateString(), "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}