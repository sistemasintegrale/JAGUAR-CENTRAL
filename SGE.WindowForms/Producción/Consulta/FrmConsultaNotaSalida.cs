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
    public partial class FrmConsultaNotaSalida : DevExpress.XtraEditors.XtraForm
    {
        List<EProdNotaSalida> mlista = new List<EProdNotaSalida>();
        public FrmConsultaNotaSalida()
        {
            InitializeComponent();
        }

        private void FrmConsultaNotaSalida_Load(object sender, EventArgs e)
        {
            dtpFechaIni.EditValue = Convert.ToDateTime("01/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString());
            dtpFechaFin.EditValue = DateTime.Now;
            cargar();
        }
        private void cargar()
        {
            BSControls.LoaderLook(LkpAlmacen, new BCentral().ListarProdAlmacen().Where(ob => ob.puvec_icod_punto_venta == 2).ToList(), "descripcion", "id_almacen", true);
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
                mlista = new BCentral().ListarProdNotaSalidaXfechaAlmacen(obj);
                dgrMotonave.DataSource = mlista;
            }
        }

        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {
            FrmManteNotaSalida Motonave = new FrmManteNotaSalida();

            Motonave.oDetail = mlista;
            EProdNotaSalida oBe = (EProdNotaSalida)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (oBe != null)
            {
                Motonave.indicador = 3;
                Motonave.CargarDetalle(oBe.salgc_icod_nota_salida);
                Motonave.Show();
                Motonave.Correlative = oBe.salgc_icod_nota_salida;
                Motonave.txtnroNota.Text = oBe.salgc_vnumero_nota_salida;
                Motonave.btncodigoalmacen.Tag = oBe.salgc_iid_almacen;
                Motonave.btncodigoalmacen.Text = oBe.salgc_viid_almacen;
                Motonave.txtalmacen.Text = oBe.salgc_vdescripcion_almacen;
                Motonave.lkpmotivo.EditValue = oBe.tablc_iid_motivo;
                Motonave.bteTipoDoc.Tag = oBe.salgc_iid_tipo_doc;
                Motonave.txtnrodocumento.Text = oBe.salgc_inumero_doc;
                Motonave.dtmfecha.EditValue = oBe.salgc_sfecha_nota_salida;
                Motonave.txtobservacion.Text = oBe.salgc_vobservaciones;

                Motonave.SetCancel();
                Motonave.dtmfecha.Enabled = false;
                Motonave.btncodigoalmacen.Enabled = false;
                Motonave.txtnrodocumento.Enabled = false;
                Motonave.lkpmotivo.Enabled = false;
                Motonave.bteTipoDoc.Enabled = false;
                Motonave.gridControl1.Enabled = false;
                Motonave.btnGuardar.Enabled = false;
                Motonave.groupControl1.Enabled = false;
            }
        }

        private void kardexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlista.Count > 0)
            {
                rptNotaSalidaSinDetalle rpteSalida = new rptNotaSalidaSinDetalle();
                rpteSalida.cargar(mlista, dtpFechaIni.Text, dtpFechaFin.Text, LkpAlmacen.Text);
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<EProdNotaSalidaDetalle> mlistadetalle = new List<EProdNotaSalidaDetalle>();
            if (Validate())
            {
                EProdReporte obj = new EProdReporte()
                {
                    sfechaInicio = Convert.ToDateTime(dtpFechaIni.EditValue),
                    sfechaFinal = Convert.ToDateTime(dtpFechaFin.EditValue),
                    icod_almacen = Convert.ToInt32(LkpAlmacen.EditValue)
                };
                mlistadetalle = new BCentral().ListarProdNotaSalidaXfechaAlmacenDetalle(obj);
                if (mlistadetalle.Count > 0)
                {
                    foreach (EProdNotaSalidaDetalle conve in mlistadetalle)
                    {
                        int[] tallas = new int[10];
                        int[] Cantidad = new int[10];
                        tallas[0] = Convert.ToInt32(conve.salgcd_talla1);
                        tallas[1] = Convert.ToInt32(conve.salgcd_talla2);
                        tallas[2] = Convert.ToInt32(conve.salgcd_talla3);
                        tallas[3] = Convert.ToInt32(conve.salgcd_talla4);
                        tallas[4] = Convert.ToInt32(conve.salgcd_talla5);
                        tallas[5] = Convert.ToInt32(conve.salgcd_talla6);
                        tallas[6] = Convert.ToInt32(conve.salgcd_talla7);
                        tallas[7] = Convert.ToInt32(conve.salgcd_talla8);
                        tallas[8] = Convert.ToInt32(conve.salgcd_talla9);
                        tallas[9] = Convert.ToInt32(conve.salgcd_talla10);
                        Cantidad[0] = Convert.ToInt32(conve.salgcd_cant_talla1);
                        Cantidad[1] = Convert.ToInt32(conve.salgcd_cant_talla2);
                        Cantidad[2] = Convert.ToInt32(conve.salgcd_cant_talla3);
                        Cantidad[3] = Convert.ToInt32(conve.salgcd_cant_talla4);
                        Cantidad[4] = Convert.ToInt32(conve.salgcd_cant_talla5);
                        Cantidad[5] = Convert.ToInt32(conve.salgcd_cant_talla6);
                        Cantidad[6] = Convert.ToInt32(conve.salgcd_cant_talla7);
                        Cantidad[7] = Convert.ToInt32(conve.salgcd_cant_talla8);
                        Cantidad[8] = Convert.ToInt32(conve.salgcd_cant_talla9);
                        Cantidad[9] = Convert.ToInt32(conve.salgcd_cant_talla10);
                        for (int i = 0; i < 10; i++)
                        {
                            if (tallas[i] != 0)
                            {
                                conve.TallaAcumulada = conve.TallaAcumulada + " - " + tallas[i].ToString() + "/" + Cantidad[i].ToString();
                            }
                        }
                    }
                    rptNotaSalidaConDetalle rpteDealle = new rptNotaSalidaConDetalle();
                    rpteDealle.cargar(mlistadetalle, dtpFechaIni.Text, dtpFechaFin.Text, LkpAlmacen.Text);

                }
            }
        }

    }
}