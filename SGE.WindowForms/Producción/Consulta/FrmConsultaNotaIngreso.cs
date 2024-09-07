using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Producción.Consulta
{
    public partial class FrmConsultaNotaIngreso : DevExpress.XtraEditors.XtraForm
    {
        List<EProdNotaIngreso> mlista = new List<EProdNotaIngreso>();

        public FrmConsultaNotaIngreso()
        {
            InitializeComponent();
        }
        private void cargar()
        {
            BSControls.LoaderLook(LkpAlmacen, new BCentral().ListarProdAlmacen().Where(ob => ob.puvec_icod_punto_venta == 2).ToList(), "descripcion", "id_almacen", true);
        }
        private void FrmConsultaNotaIngreso_Load(object sender, EventArgs e)
        {
            dtpFechaIni.EditValue = Convert.ToDateTime("01/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString());
            dtpFechaFin.EditValue = DateTime.Now;
            cargar();
        }
        private Boolean Validate()
        {
            Boolean Flag = true;
            try
            {
                if (Convert.ToDateTime(dtpFechaIni.EditValue) >= Convert.ToDateTime(dtpFechaFin.EditValue))
                    throw new Exception("La fecha de Inicio debe ser menor a la fecha final");
            }
            catch (Exception e)
            {
                XtraMessageBox.Show(e.Message, "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            return Flag;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                EProdReporte obj = new EProdReporte()
                {
                    sfechaInicio = Convert.ToDateTime(dtpFechaIni.EditValue),
                    sfechaFinal = Convert.ToDateTime(dtpFechaFin.EditValue),
                    icod_almacen = Convert.ToInt32(LkpAlmacen.EditValue)
                };
                mlista = new BCentral().ListarProdNotaIngresoXfechaAlmacen(obj);
                dgrMotonave.DataSource = mlista;
            }
        }



        private void kardexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlista.Count > 0)
            {
                rptNotaIngresoSinDetalle rptIngr = new rptNotaIngresoSinDetalle();
                rptIngr.cargar(mlista, dtpFechaIni.Text, dtpFechaFin.Text, LkpAlmacen.Text);
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<EProdNotaIngresoDetalle> mlistadetalle = new List<EProdNotaIngresoDetalle>();
            if (Validate())
            {
                EProdReporte obj = new EProdReporte()
                {
                    sfechaInicio = Convert.ToDateTime(dtpFechaIni.EditValue),
                    sfechaFinal = Convert.ToDateTime(dtpFechaFin.EditValue),
                    icod_almacen = Convert.ToInt32(LkpAlmacen.EditValue)
                };
                mlistadetalle = new BCentral().ListarProdNotaIngresoXfechaAlmacenDetalle(obj);
                if (mlistadetalle.Count > 0)
                {
                    foreach (EProdNotaIngresoDetalle conve in mlistadetalle)
                    {
                        int[] tallas = new int[10];
                        int[] Cantidad = new int[10];
                        tallas[0] = Convert.ToInt32(conve.ningcd_talla1);
                        tallas[1] = Convert.ToInt32(conve.ningcd_talla2);
                        tallas[2] = Convert.ToInt32(conve.ningcd_talla3);
                        tallas[3] = Convert.ToInt32(conve.ningcd_talla4);
                        tallas[4] = Convert.ToInt32(conve.ningcd_talla5);
                        tallas[5] = Convert.ToInt32(conve.ningcd_talla6);
                        tallas[6] = Convert.ToInt32(conve.ningcd_talla7);
                        tallas[7] = Convert.ToInt32(conve.ningcd_talla8);
                        tallas[8] = Convert.ToInt32(conve.ningcd_talla9);
                        tallas[9] = Convert.ToInt32(conve.ningcd_talla10);
                        Cantidad[0] = Convert.ToInt32(conve.ningcd_cant_talla1);
                        Cantidad[1] = Convert.ToInt32(conve.ningcd_cant_talla2);
                        Cantidad[2] = Convert.ToInt32(conve.ningcd_cant_talla3);
                        Cantidad[3] = Convert.ToInt32(conve.ningcd_cant_talla4);
                        Cantidad[4] = Convert.ToInt32(conve.ningcd_cant_talla5);
                        Cantidad[5] = Convert.ToInt32(conve.ningcd_cant_talla6);
                        Cantidad[6] = Convert.ToInt32(conve.ningcd_cant_talla7);
                        Cantidad[7] = Convert.ToInt32(conve.ningcd_cant_talla8);
                        Cantidad[8] = Convert.ToInt32(conve.ningcd_cant_talla9);
                        Cantidad[9] = Convert.ToInt32(conve.ningcd_cant_talla10);
                        for (int i = 0; i < 10; i++)
                        {
                            if (tallas[i] != 0)
                            {
                                conve.TallaAcumulada = conve.TallaAcumulada + " - " + tallas[i].ToString() + "/" + Cantidad[i].ToString();
                            }
                        }
                    }
                    rptNotaIngresoConDetalle rpteDealle = new rptNotaIngresoConDetalle();
                    rpteDealle.cargar(mlistadetalle, dtpFechaIni.Text, dtpFechaFin.Text, LkpAlmacen.Text);

                }
            }
        }

        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {
            if (mlista.Count > 0)
            {
                FrmNotaIngresoRegistro Motonave = new FrmNotaIngresoRegistro();

                Motonave.Show();
                Motonave.SetCancel();
                EProdNotaIngreso oBe = (EProdNotaIngreso)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                Motonave.ningc_icod_nota_ingreso = oBe.ningc_icod_nota_ingreso;
                Motonave.txtnroNota.Text = oBe.ningc_vnumero_nota_ingreso;
                Motonave.btncodigoalmacen.Tag = oBe.ningc_iid_almacen;
                Motonave.btncodigoalmacen.Text = oBe.ningc_viid_almacen;
                Motonave.txtalmacen.Text = oBe.ningc_vdescripcion_almacen;
                Motonave.lkpmotivo.EditValue = oBe.tablc_iid_motivo;
                Motonave.lkptipodocumento.EditValue = oBe.ningc_iid_tipo_doc;
                Motonave.txtnrodocumento.Text = oBe.ningc_inumero_doc;
                Motonave.dtmfechadocumento.EditValue = oBe.ningc_sfecha_compra;
                Motonave.dtmfecha.EditValue = oBe.ningc_sfecha_nota_ingreso;
                Motonave.txtobservacion.Text = oBe.ningc_vobservaciones;
                Motonave.txtreferencia.Text = oBe.ningc_vreferencia;

                Motonave.dtmfecha.Enabled = false;
                Motonave.btncodigoalmacen.Enabled = false;
                Motonave.txtnrodocumento.Enabled = false;
                Motonave.dtmfechadocumento.Enabled = false;
                Motonave.lkpmotivo.Enabled = false;
                Motonave.lkptipodocumento.Enabled = false;
                Motonave.grcNotaDetalle.Enabled = false;

            }
        }
    }
}