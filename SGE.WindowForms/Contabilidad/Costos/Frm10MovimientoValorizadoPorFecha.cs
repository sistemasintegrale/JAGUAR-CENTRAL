using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Costos
{
    public partial class Frm10MovimientoValorizadoPorFecha : DevExpress.XtraEditors.XtraForm
    {
        DateTime dtFInicio;
        DateTime dtFFin;
        int intTipoMov;
        int intMotivoMov;
        string strTipo = "";
        string strMotivo = "";
        List<EKardexValorizado> lstResumenValorizado = new List<EKardexValorizado>();
        public Frm10MovimientoValorizadoPorFecha()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDateTime(dtInicio.EditValue).Year != Parametros.intEjercicio)
                    throw new ArgumentException("El rango de fehcas debe estar dentro el Año de Ejercicio " + Parametros.intEjercicio.ToString());
                if (Convert.ToDateTime(dtFin.EditValue).Year != Parametros.intEjercicio)
                    throw new ArgumentException("El rango de fehcas debe estar dentro el Año de Ejercicio " + Parametros.intEjercicio.ToString());

                dtFInicio = Convert.ToDateTime(dtInicio.EditValue);
                dtFFin = Convert.ToDateTime(dtFin.EditValue);
                intTipoMov = Convert.ToInt32(lkpTipoMov.EditValue);
                intMotivoMov = Convert.ToInt32(lkpMotivoMov.EditValue);
                strTipo = lkpTipoMov.Text;
                strMotivo = lkpMotivoMov.Text;
                cargar();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void cargar()
        {
            lstResumenValorizado = new BAlmacen().listarKardexValorizadoPorMotivoFecha(Parametros.intEjercicio, dtFInicio, dtFFin, intTipoMov, intMotivoMov);
            grdResumen.DataSource = lstResumenValorizado;
            viewResumen.Focus();
        }

        private void imprimir()
        {
            if (lstResumenValorizado.Count == 0)
                return;
            rptKardexValorizadoPorFechaMotivo rpt = new rptKardexValorizadoPorFechaMotivo();
            string titulo = "MOVIMIENTOS DE " + strTipo + " - " + strMotivo;
            string titulo2 = String.Format("DE : {0} A: {1}", dtFInicio.ToShortDateString(), dtFFin.ToShortDateString());
            rpt.carga(lstResumenValorizado, titulo, "");

        }

        private void imprimirListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void Frm04ResumenValorizadoProductos_Load(object sender, EventArgs e)
        {
            List<ETablaRegistro> lstTablaregistro = new List<ETablaRegistro>();
            for (int i = 0; i < 2; i++)
            {
                ETablaRegistro obj = new ETablaRegistro();
                if (i == 0)
                {
                    obj.tarec_vdescripcion = "INGRESOS";
                    obj.tarec_icorrelativo_registro = 1;
                    lstTablaregistro.Add(obj);
                }
                else
                {
                    obj.tarec_vdescripcion = "SALIDAS";
                    obj.tarec_icorrelativo_registro = 0;
                    lstTablaregistro.Add(obj);
                }
            }
            BSControls.LoaderLook(lkpTipoMov, lstTablaregistro, "tarec_vdescripcion", "tarec_icorrelativo_registro", true);


            if (DateTime.Now.Year == Parametros.intEjercicio)
            {
                dtInicio.EditValue = Convert.ToDateTime("01/" + DateTime.Now.Month + "/" + Parametros.intEjercicio);
                dtFin.EditValue = DateTime.Now;
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EKardexValorizado obe = (EKardexValorizado)viewResumen.GetRow(viewResumen.FocusedRowHandle);
            if (obe != null)
            {
                rpt02StockValorizado rpt = new rpt02StockValorizado();
                string[] arreglo = obe.prdc_icod_producto.ToString().Split("-".ToCharArray());
                obe.prdc_icod_producto = Convert.ToInt32(arreglo[0]);
                var lista = new BAlmacen().ListarKardexValorizadoInventario(obe.almcc_icod_almacen, obe.prdc_icod_producto, dtFInicio, dtFFin);
                rpt.Cargar(obe, lista, obe.strDesAlmacenCtbl);
            }
        }

        private void lkpTipoMov_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpTipoMov.EditValue) == 0)
            {
                BSControls.LoaderLook(lkpMotivoMov, new BGeneral().listarTablaRegistro(35), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            }
            else
            {
                BSControls.LoaderLook(lkpMotivoMov, new BGeneral().listarTablaRegistro(34), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            }
            //BSControls.LoaderLook(lkpMotivoMov, new BRegistro().listarMotivoKardex(Convert.ToInt32(lkpTipoMov.EditValue)), "Descripcion", "Correlativo", true);
        }
    }
}