using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Tesoreria.Caja;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Tesorería.Consultas
{
    public partial class Frm04ResumenMovPorFechas : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm04ResumenMovPorFechas));
        private List<EEntidadFinancieraCuenta> mlist = new List<EEntidadFinancieraCuenta>();
        public Frm04ResumenMovPorFechas()
        {
            InitializeComponent();
        }

        private void Frm04ResumenMovPorFechas_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            BaseEdit oBase = null;
            try
            {
                if (dtmFechaI.EditValue == null)
                {
                    oBase = dtmFechaI;
                    throw new ArgumentException("Selecione una fecha de inicio válido para el rango de fechas");
                }
                if (dtmFechaF.EditValue == null)
                {
                    oBase = dtmFechaF;
                    throw new ArgumentException("Selecione una fecha de termino válido para el rango de fechas");
                }
                Buscar();
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
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void CheckFecha(object obj)
        {
            DateEdit Obj = (DateEdit)obj;
            DateTime fecha = Convert.ToDateTime(Obj.EditValue);
            if (fecha.Year != Parametros.intEjercicio)
            {
                XtraMessageBox.Show("La fecha selecciona no se encuentra dentro del año de ejercicio", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Obj.EditValue = DateTime.Now;
            }
        }

        private void Buscar()
        {
            mlist = new BTesoreria().ListarResumenMovimientoCuentas(Convert.ToDateTime(dtmFechaI.EditValue), Convert.ToDateTime(dtmFechaF.EditValue), Parametros.intEjercicio);
            grdResumen.DataSource = mlist;
        }

        private void dtmFechaI_EditValueChanged(object sender, EventArgs e)
        {
            CheckFecha(sender);
        }

        private void dtmFechaF_EditValueChanged(object sender, EventArgs e)
        {
            CheckFecha(sender);
        }

        private void resumenDeMovimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                rpt04ResumenMovimientos rpt = new rpt04ResumenMovimientos();
                rpt.carga(mlist, dtmFechaI.Text, dtmFechaF.Text);
            }
        }

        private void detalleDeMovimientosDeBancosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ELibroBancos> mlista = new BTesoreria().ListarMovimientoCuentasDetalle(Convert.ToDateTime(dtmFechaI.EditValue), Convert.ToDateTime(dtmFechaF.EditValue), Parametros.intEjercicio);
            if (mlista.Count > 0)
            {
                rpt04DetalleMovimientos rpt = new rpt04DetalleMovimientos();
                rpt.carga(mlista, dtmFechaI.Text, dtmFechaF.Text);
            }
        }

        private void pagosEfectuadosPorClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (mlist.Count > 0)
            //{
            //    List<ECobranzaAbonoCuentaContable> mlista = new BTesoreria().ListarPagosEfectuadosClientes(Convert.ToDateTime(dtmFechaI.EditValue), Convert.ToDateTime(dtmFechaF.EditValue));
            //    rpt04PagosClientes rpt = new rpt04PagosClientes();
            //    rpt.carga(mlista, dtmFechaI.Text, dtmFechaF.Text);
            //}
        }

        private void verMovimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EEntidadFinancieraCuenta Obe = (EEntidadFinancieraCuenta)viewLibroBancos.GetRow(viewLibroBancos.FocusedRowHandle);
            if (Obe != null)
            {
                FrmMovimientosCtasBancos frm = new FrmMovimientosCtasBancos();
                frm.icod_cuenta = Obe.id_entidad_Financiera_cuenta;
                frm.FechaI = dtmFechaI.Text;
                frm.FechaF = dtmFechaF.Text;
                frm.Text = "MOVIMIENTOS DE " + Obe.vdes_entidad_financiera + " " + Obe.Descripcion + " DEL " + dtmFechaI.Text + " AL " + dtmFechaF.Text;
                frm.Show();
            }
        }
    }
}