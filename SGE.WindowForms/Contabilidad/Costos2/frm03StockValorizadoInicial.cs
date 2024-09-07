using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using SGE.Entity;
using SGE.BusinessLogic;
using System.Linq;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using SGE.WindowForms.Reportes.Almacen.Registros;
using SGE.WindowForms.Otros.Contabilidad;

namespace SGE.WindowForms.Contabilidad.Costos
{
    public partial class frm03StockValorizadoInicial : DevExpress.XtraEditors.XtraForm
    {
        List<EKardexValorizado> lstKardexVal = new List<EKardexValorizado>();

        public frm03StockValorizadoInicial()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }       
       
        private void cargar()
        {
            lstKardexVal = new BAlmacen().listarKardexValorizadoSaldoInicial(Parametros.intEjercicio);
            grdKardexVal.DataSource = lstKardexVal;
            viewKardexVal.Focus();
        }
        void reload(Int64 intIcod)
        {
            cargar();
            int index = lstKardexVal.FindIndex(x => x.kardv_icod_correlativo == intIcod);
            viewKardexVal.FocusedRowHandle = index;
            viewKardexVal.Focus();   
        } 
       
        private void nuevo()
        {
            FrmManteIngresoStockInicialValorizado frm = new FrmManteIngresoStockInicialValorizado();
            frm.MiEvento += new FrmManteIngresoStockInicialValorizado.DelegadoMensaje(reload);
            frm.lstKardexVal = lstKardexVal;
            frm.SetInsert();
            frm.Show();            
        }

        
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            EKardexValorizado Obe = (EKardexValorizado)viewKardexVal.GetRow(viewKardexVal.FocusedRowHandle);
            if (Obe == null)
                return;
            FrmManteIngresoStockInicialValorizado frm = new FrmManteIngresoStockInicialValorizado();
            frm.oBe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }
        
        private void imprimir()
        {
            //if (lstAlmacenes.Count > 0)
            //{
            //    rptAlmacen rpt = new rptAlmacen();
            //    rpt.cargar("RELACIÓN DE ALMACENES", "", lstAlmacenes);
            //}
           
        }
        
        private void filtrar()
        {
            grdKardexVal.DataSource = lstKardexVal.Where(x =>
                                                   x.strCodProducto.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.strDesProducto.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }
       
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }       

        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewKardexVal.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewKardexVal.ClearColumnsFilter();
        }

        private void verDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EKardexValorizado oBe = (EKardexValorizado)viewKardexVal.GetRow(viewKardexVal.FocusedRowHandle);
            if (oBe == null)
                return;

            FrmDetalleStockValorizadoInicial frm = new FrmDetalleStockValorizadoInicial();
            frm.MiEvento += new FrmDetalleStockValorizadoInicial.DelegadoMensaje(reload);
            frm.lstKardexVal.Add(oBe);
            frm.Show();

        }

        private void frm03StockValorizadoInicial_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lstKardexVal.Count(ob => ob.intOperacion == 2) > 0)
            {
                if (Sfag_actualizado == true)
                {
                    if (XtraMessageBox.Show("¿Desea Guardar los Cambios?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        foreach (var _BE in lstKardexVal)
                        {
                            if (_BE.intOperacion == 2)
                            {
                                new BAlmacen().modificarKardexValorizado(_BE);
                                Sfag_actualizado = false;
                            }
                        }
                    }
                }
            }
        }

        private void viewKardexVal_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            EKardexValorizado oBe = (EKardexValorizado)viewKardexVal.GetRow(viewKardexVal.FocusedRowHandle);
            if (oBe != null)
            {
                oBe.intOperacion = 2;
            }
        }
        int xpposition = 0;
        Boolean Sfag_actualizado=true;
        private void guardarLosCambiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xpposition = viewKardexVal.FocusedRowHandle;
            viewKardexVal.MoveLast();
            viewKardexVal.MoveFirst();
                new BAlmacen().modificarKardexValorizadoLista(lstKardexVal);
                Sfag_actualizado = false;
             cargar();
             viewKardexVal.FocusedRowHandle=xpposition;
             XtraMessageBox.Show("Los datos se actualizaron Satisfactoriamente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
        } 
    }
}