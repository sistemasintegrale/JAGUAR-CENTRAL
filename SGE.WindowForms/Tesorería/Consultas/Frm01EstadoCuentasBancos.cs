using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Tesorería.Consultas
{
    public partial class Frm01EstadoCuentasBancos : DevExpress.XtraEditors.XtraForm
    {
        private List<ELibroBancos> mlist = new List<ELibroBancos>();
        public Frm01EstadoCuentasBancos()
        {
            InitializeComponent();
        }

        private void Frm01EstadoCuentasBancos_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpBanco, new BTesoreria().listarBancos(), "bcoc_vnombre_banco", "bcoc_icod_banco", true);
            var lstMeses = new BGeneral().listarTablaRegistro(4);
            BSControls.LoaderLook(lkpMes, lstMeses, "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMes.EditValue = DateTime.Now.Month;
            lkpBanco.ItemIndex = 0;
            Carga();
        }
        private void Carga()
        {
            try
            {
                //Cursor = Cursors.WaitCursor;
                mlist = new BTesoreria().ListarLibroBancos(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue), Convert.ToInt32(lkpCuenta.EditValue));
                dgrLibroBancos.DataSource = mlist;

                if (mlist.Count > 0)
                {
                    decimal dmlSaldoLibro = (from p in mlist
                                             select p.SaldoLibro).Last();

                    txtSaldoAnterior.EditValue = mlist[0].SaldoAnterior;
                    txtSaldoDisponible.EditValue = mlist[0].SaldoDisponible;
                    txtSaldoLibro.EditValue = dmlSaldoLibro;
                    txtRegistros.EditValue = mlist.Count;
                }
                else
                {
                    if (lkpMes.EditValue != null)
                    {
                        List<ELibroBancos> ListaSaldoAnterior = new List<ELibroBancos>();
                        List<ELibroBancos> ListaSaldoDisponible = new List<ELibroBancos>();

                        ListaSaldoAnterior = new BTesoreria().ListarLibroBancosSaldoAnterior(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue), Convert.ToInt32(lkpCuenta.EditValue));
                        ListaSaldoDisponible = new BTesoreria().ListarLibroBancosSaldoDisponible(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue), Convert.ToInt32(lkpCuenta.EditValue));

                        if (ListaSaldoAnterior.Count > 0)
                        {
                            txtSaldoAnterior.EditValue = ListaSaldoAnterior[0].SaldoAnterior;
                            if (ListaSaldoDisponible.Count > 0)
                                txtSaldoDisponible.EditValue = ListaSaldoDisponible[0].SaldoDisponible;
                            else
                                txtSaldoDisponible.EditValue = 0;
                            txtSaldoLibro.EditValue = 0;
                            txtRegistros.EditValue = 0;
                        }
                        else
                        {
                            txtSaldoAnterior.EditValue = 0;
                            txtSaldoDisponible.EditValue = 0;
                            txtSaldoLibro.EditValue = 0;
                            txtRegistros.EditValue = 0;
                        }
                    }

                }

                //Cursor = Cursors.Default;
                //APROBACION DE PAUTAS DE DISTRIBUCION
                //CLIENTES(AGENCIAS)
                //-->SOLICITUD DE PAUTAS
                //<--APROBACION DE PAUTAS
                //PRODUCCION(DE DIARIOS)
                //-->ORDEN DE PRODUCCION(EN BASE LAS PAUTAS APROBADAS)
                //<--APROBACION DE ORDEN DE PRODUCCION
                //<--ORDEN DE COMPRA DE MATERIA PRIMA
                //LOGISTICA
                //-->ORDEN DE COMPRA DE MATERIA PRIMA
                //<--APROBACION DE ORDEN DE COMPRA
                //CONTABILIDAD
                //-->CLIENTES FACTURADOS
            }

            catch (Exception ex)
            {
                //Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lkpMes_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpMes.EditValue != null)
            {
                Carga();
            }
        }

        private void imprimirEstadoDeCuentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                List<ELibroBancos> olist = new List<ELibroBancos>();
                olist = new BTesoreria().ListarEstadoCuenta(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue), Convert.ToInt32(lkpCuenta.EditValue));
                rpt01EstadoCuentasBancos rpt = new rpt01EstadoCuentasBancos();
                rpt.carga(olist, lkpMes.Text, lkpBanco.Text, lkpCuenta.Text);
            }
            else
            {
                XtraMessageBox.Show("No existen movimientos dentro de este mes", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void imprimirConciliaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                List<ELibroBancos> olist = new List<ELibroBancos>();
                olist = new BTesoreria().listarMovimientosSinConciliar(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue), Convert.ToInt32(lkpCuenta.EditValue));
                rpt01EstadoCuentasBcosConciliacion rpt = new rpt01EstadoCuentasBcosConciliacion();
                decimal mto_total_abonos;
                decimal mto_total_cargos;
                decimal mto_saldo_disponible;
                decimal mto_saldo_libro;
                mto_total_abonos = Convert.ToDecimal(olist.Sum(x => x.Abono));
                mto_total_cargos = Convert.ToDecimal(olist.Sum(x => x.Cargo));
                mto_saldo_libro = Convert.ToDecimal(txtSaldoLibro.Text);
                mto_saldo_disponible = mto_saldo_libro - mto_total_abonos + mto_total_cargos;

                olist.ForEach(x =>
                {
                    if (x.cflag_tipo_movimiento == "1")
                    {
                        x.descripcion_Libro_Bancos = "MENOS : ABONOS NO REGISTRADOS EN BANCOS ===>";
                        x.mto_total = mto_total_abonos;
                    }
                    else
                    {
                        x.descripcion_Libro_Bancos = "MAS   : CARGOS NO REGISTRADOS EN BANCOS ===>";
                        x.mto_total = mto_total_cargos;
                    }
                });
                rpt.carga(olist, lkpMes.Text, lkpBanco.Text, lkpCuenta.Text,
                    Convert.ToDecimal(mto_saldo_disponible), Convert.ToDecimal(mto_saldo_libro));
            }
            else
            {
                XtraMessageBox.Show("No existen movimientos dentro de este mes", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lkpBanco_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpBanco.EditValue != null)
            {
                var lstCuentas = new BTesoreria().listarBancoCuentas(Convert.ToInt32(lkpBanco.EditValue));
                BSControls.LoaderLook(lkpCuenta, lstCuentas, "bcod_vnumero_cuenta", "bcod_icod_banco_cuenta", true);
                if (lstCuentas.Count == 0)
                {
                    mlist.Clear();
                    viewLibroBancos.RefreshData();
                    mnuLibroBancos.Enabled = false;
                }
                else
                    mnuLibroBancos.Enabled = true;
            }
        }

        private void lkpCuenta_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpCuenta.EditValue != null)
            {
                Carga();
            }
        }
    }
}