using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Compras.Consultas_de_Cuentas_Corrientes
{
    public partial class Frm03DocXPagarFechaVencimiento : DevExpress.XtraEditors.XtraForm
    {
        BCuentasPorPagar Obl = new BCuentasPorPagar();
        List<EDocPorPagar> Lista = new List<EDocPorPagar>();

        public Frm03DocXPagarFechaVencimiento()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Validate())
                Cargar();

        }
        private Boolean Validate()
        {
            Boolean Flag = true;
            try
            {
                if (Convert.ToDateTime(dtpFechaIni.EditValue) >= Convert.ToDateTime(dtpFechaFin.EditValue))
                    throw new Exception("La fecha de Inicio debe ser menor a la fecha final");
                else
                {
                    if (Convert.ToDateTime(dtpFechaIni.EditValue).Year != Parametros.intEjercicio || Convert.ToDateTime(dtpFechaFin.EditValue).Year != Parametros.intEjercicio)
                    {
                        throw new Exception("La fecha no esta dentro del periodo");
                    }
                }
            }
            catch (Exception e)
            {
                XtraMessageBox.Show(e.Message, "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            return Flag;
        }
        private void Cargar()
        {
            if (VerificarEjercicio(Convert.ToDateTime(dtpFechaIni.EditValue)) == true && VerificarEjercicio(Convert.ToDateTime(dtpFechaFin.EditValue)))
            {
                Lista = Obl.BuscarDocumentosXPagarFechaVencimiento(Parametros.intEjercicio, Convert.ToDateTime(dtpFechaIni.EditValue), Convert.ToDateTime(dtpFechaFin.EditValue));
                dgr.DataSource = Lista;
                if (Lista.Count > 0)
                {
                    txtcodigo.Enabled = true;
                    txtNombre.Enabled = true;
                }
                else
                {
                    txtcodigo.Enabled = false;
                    txtNombre.Enabled = false;
                }
            }
            else
            {
                XtraMessageBox.Show("La fecha seleccionada no esta dentro del ejercicio " + Parametros.intEjercicio, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private bool VerificarEjercicio(DateTime sfecha)
        {
            bool Rpt;
            if (Parametros.intEjercicio == sfecha.Year)
            {
                Rpt = true;
            }
            else
            {
                Rpt = false;
            }
            return Rpt;
        }
        private void FrmDocXPagarFechaVencimiento_Load(object sender, EventArgs e)
        {
            dtpFechaIni.EditValue = Convert.ToDateTime("01/" + DateTime.Now.Month.ToString() + "/" + Parametros.intEjercicio);
            dtpFechaFin.EditValue = DateTime.Now;
            this.Cargar();
        }

        private void EstadoCuenta_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                List<EDocPorPagar> mlistaAux = new List<EDocPorPagar>();
                mlistaAux = Lista.Where(obj =>
                                                   obj.proc_vcod_proveedor.ToUpper().Contains(txtcodigo.Text.ToUpper()) &&
                                                   obj.proc_vnombrecompleto.ToString().Contains(txtNombre.Text.ToUpper())
                                             ).ToList();
                rptDocXPagarFechaVencimiento rpt = new rptDocXPagarFechaVencimiento();

                rpt.cargar(mlistaAux, Parametros.intEjercicio.ToString(), dtpFechaIni.Text, dtpFechaFin.Text);
            }
        }

        private void txtcodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }
        private void BuscarCriterio()
        {
            dgr.DataSource = Lista.Where(obj =>
                                                   obj.proc_vcod_proveedor.ToUpper().Contains(txtcodigo.Text.ToUpper()) &&
                                                   obj.proc_vnombrecompleto.ToString().Contains(txtNombre.Text.ToUpper())
                                             ).ToList();

        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewEstadoCuenta.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewEstadoCuenta.ClearColumnsFilter();
        }


    }

}