using DevExpress.XtraBars;
using DevExpress.XtraBars.ViewInfo;
using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Reportes.Almacen.Consultas;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Almacén.Consultas
{
    public partial class frm03NotaIngresoPorFecha : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm03NotaIngresoPorFecha));
        private List<ENotaIngreso> lstNotaIngreso = new List<ENotaIngreso>();
        DateTime f1, f2;
        public frm03NotaIngresoPorFecha()
        {
            InitializeComponent();
        }

        private void FrmRegistroNotaIngreso_Load(object sender, EventArgs e)
        {
            setFechas();
        }

        private void setFechas()
        {
            if (Parametros.intEjercicio == DateTime.Now.Year)
            {
                dteFechaDesde.EditValue = DateTime.Now;
                dteFechaHasta.EditValue = DateTime.Now;
            }
            else
            {
                dteFechaDesde.EditValue = Convert.ToDateTime("01/01/" + Parametros.intEjercicio.ToString());
                dteFechaHasta.EditValue = Convert.ToDateTime("31/12/" + Parametros.intEjercicio.ToString());
            }
        }

        private void buscar()
        {
            BaseEdit oBase = null;
            try
            {
                f1 = (DateTime)dteFechaDesde.EditValue;
                f2 = (DateTime)dteFechaHasta.EditValue;
                if (f1.Year != Parametros.intEjercicio)
                {
                    oBase = dteFechaDesde;
                    throw new ArgumentException("La fecha no esta dentro el año de ejercicio");
                }

                if (f2.Year != Parametros.intEjercicio)
                {
                    oBase = dteFechaHasta;
                    throw new ArgumentException("La fecha no esta dentro el año de ejercicio");
                }

                if (Convert.ToDateTime(f2.ToShortDateString()) < Convert.ToDateTime(f1.ToShortDateString()))
                {
                    oBase = dteFechaHasta;
                    throw new ArgumentException("La fecha inicial no puede ser mayor que fecha final");
                }

                if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                {
                    oBase = bteAlmacen;
                    throw new ArgumentException("Seleccione el almacén a consultar");
                }

                lstNotaIngreso = new BAlmacen().listarNotaIngreso(Parametros.intEjercicio, Convert.ToInt32(bteAlmacen.Tag), f1, f2);
                grdNotaIngreso.DataSource = lstNotaIngreso;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void listarAlmacen()
        {
            try
            {
                frmListarAlmacen Almacen = new frmListarAlmacen();
                if (Almacen.ShowDialog() == DialogResult.OK)
                {
                    bteAlmacen.Tag = Almacen._Be.almac_icod_almacen;
                    bteAlmacen.Text = Almacen._Be.almac_vdescripcion;
                    /*----------------------------------------------------*/
                    lstNotaIngreso.Clear();
                    viewNotaIngreso.RefreshData();
                    /*----------------------------------------------------*/
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void impSinDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimirSinDetalle();
        }
        private void imprimirSinDetalle()
        {
            if (lstNotaIngreso.Count > 0)
            {
                rptNotaIngresoPorFechas rpt = new rptNotaIngresoPorFechas();
                rpt.cargar(String.Format("RELACIÓN DE NOTAS DE INGRESO A: {0}", bteAlmacen.Text), String.Format("DESDE: {0} HASTA: {1}", f1.ToShortDateString(), f2.ToShortDateString()), lstNotaIngreso);
            }
        }

        private void imprimirConDetalle()
        {
            List<ENotaIngresoDetalle> lstGeneral = new List<ENotaIngresoDetalle>();
            if (lstNotaIngreso.Count > 0)
            {
                lstNotaIngreso.ForEach(x =>
                {
                    var lst = new BAlmacen().listarNotaIngresoDetalle(x.ningc_icod_nota_ingreso);
                    int cont = 0;
                    lst.ForEach(a =>
                    {
                        cont += 1;
                        a.dninc_nro_item = cont;
                        a.cabNroNota = x.ningc_numero_nota_ingreso;
                        a.cabFechaNota = x.ningc_fecha_nota_ingreso;
                        a.cabMotivo = x.strMotivo;
                        a.cabDocumento = x.strTipoNroDoc;
                        a.cabReferencia = x.ningc_referencia;
                        a.cabObservacion = x.ningc_observaciones;
                        lstGeneral.Add(a);
                    });
                });
            }
            rptNotaIngresoPorFechasDetalle rpt = new rptNotaIngresoPorFechasDetalle();
            rpt.cargar(String.Format("RELACIÓN DE NOTAS DE INGRESO A: {0}", bteAlmacen.Text), String.Format("DESDE: {0} HASTA: {1}", f1.ToShortDateString(), f2.ToShortDateString()), lstGeneral);
        }

        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mnu.Show();
        }

        private void impConDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimirConDetalle();
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewNotaIngreso.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewNotaIngreso.ClearColumnsFilter();
        }

        private void barSubItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Link == null)
                OpenSubItem(e.Item);
        }

        void OpenSubItem(BarItem item)
        {
            if (!(item is BarSubItem) || item.Links.Count == 0) return;
            BarSelectionInfo info;
            info = item.Manager.InternalGetService(typeof(BarSelectionInfo)) as BarSelectionInfo;
            if (info != null)
                info.PressLink(item.Links[0]);
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            imprimirSinDetalle();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            imprimirConDetalle();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}