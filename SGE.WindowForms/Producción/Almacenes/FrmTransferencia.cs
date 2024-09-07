using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Producción.Almacenes
{
    public partial class FrmTransferencia : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        private List<EProdTransferencia> mlist = new List<EProdTransferencia>();

        private EProdTransferencia oblCabeceraImprmir = new EProdTransferencia();
        private List<EProdTransferenciaDetalle> oblDetalleImprimir = new List<EProdTransferenciaDetalle>();

        BCentral oblCabecera = new BCentral();
        BCentral oblDetalle = new BCentral();


        public FrmTransferencia()
        {
            InitializeComponent();
        }

        private void FrmTransferencia_Load(object sender, EventArgs e)
        {
            ListarTransferencia();
        }
        private void BuscarCriterio()
        {

            dgrTransferencia.DataSource = mlist.Where(obj => string.Format("{0:000000}", obj.trfc_inum_transf).Contains(txtCodigo.Text) &&
                obj.almac_vdescripcion_ingreso.ToString().ToUpper().Contains(txtalmIngreso.Text.ToUpper()) &&
                obj.almac_vdescripcion_salida.ToString().ToUpper().Contains(txtalmSalida.Text.ToUpper())).ToList();



            //dgrTransferencia.DataSource = mlist.Where(obj =>
            //                                            Convert.ToDateTime(obj.trfc_sfecha_transf.ToShortDateString()) >= Convert.ToDateTime(dtpFechaIni.EditValue) & Convert.ToDateTime(obj.trfc_sfecha_transf.ToShortDateString()) <= Convert.ToDateTime(dtpFechaFin.EditValue) &&
            //                                            obj.trfc_inum_transf.ToString().Contains((Convert.ToInt32(txtCodigo.Text)).ToString()) &&
            //                                            obj.almac_vdescripcion_ingreso.ToString().ToUpper().Contains(txtalmIngreso.Text.ToUpper()) &&
            //                                            obj.almac_vdescripcion_salida.ToUpper().Contains(txtalmSalida.Text.ToUpper())).ToList();

        }
        private void ListarTransferencia()
        {
            try
            {
                BCentral objBTransferencia = new BCentral();
                mlist = objBTransferencia.ListarProdTransferencia();
                dgrTransferencia.DataSource = mlist;
                viewTransferencia.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void nuevo()
        {
            frmRegistroTransferenciaAlmacenes frmTransferencia = new frmRegistroTransferenciaAlmacenes();
            frmTransferencia.MiEvento += new frmRegistroTransferenciaAlmacenes.DelegadoMensaje(ListarTransferencia);
            frmTransferencia.Show();
            //frmTransferencia.lkpPuntoVenta.EditValue = ValPVTC.puvec_icod_punto_venta;
            frmTransferencia.SetInsert();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                Datos();

            }
        }
        private void Datos()
        {
            EProdTransferencia Obe = (EProdTransferencia)viewTransferencia.GetRow(viewTransferencia.FocusedRowHandle);
            if (Obe != null)
            {
                frmRegistroTransferenciaAlmacenes frmTransferencia = new frmRegistroTransferenciaAlmacenes();
                frmTransferencia.MiEvento += new frmRegistroTransferenciaAlmacenes.DelegadoMensaje(ListarTransferencia);
                frmTransferencia.trfc_icod_transf = Obe.trfc_icod_transf;
                frmTransferencia.Show();
                frmTransferencia.txtNroTransferencia.Text = string.Format("{0:000000}", Obe.trfc_inum_transf);
                frmTransferencia.dtpFecha.EditValue = Obe.trfc_sfecha_transf;
                frmTransferencia.LkpAlmacenSalida.EditValue = Obe.trfc_iid_alm_sal;
                frmTransferencia.LkpAlmacenIngreso.EditValue = Obe.trfc_iid_alm_ing;
                frmTransferencia.txtObservaciones.Text = Obe.trfc_vobservaciones;
                frmTransferencia.SetModify();
            }

        }
        private void eliminar()
        {
            EProdTransferencia Obe = (EProdTransferencia)viewTransferencia.GetRow(viewTransferencia.FocusedRowHandle);
            if (Obe != null)
            {
                oblCabecera.EliminarProdTransferencia(Obe.trfc_icod_transf, Obe.puvec_icod_punto_venta);
            }
        }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void txtalmIngreso_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void txtalmSalida_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }
        private void imprimir()
        {
            if (mlist.Count > 0)
            {
                EProdTransferencia Obe = (EProdTransferencia)viewTransferencia.GetRow(viewTransferencia.FocusedRowHandle);
                oblCabeceraImprmir = Obe;
                oblDetalleImprimir = (new BCentral()).ListarProdTransferenciaDetalle(Obe.trfc_icod_transf);
                rptTransferencia RPT = new rptTransferencia();
                RPT.cargar(Obe, oblDetalleImprimir);
            }
        }

        private void btnagregar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnagregar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();

        }

        private void btnmodificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Datos();
        }

        private void btneliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void btnimprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }
    }
}