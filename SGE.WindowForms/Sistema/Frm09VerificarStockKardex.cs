using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;

namespace SGE.WindowForms.Sistema
{
    public partial class Frm09VerificarStockKardex : XtraForm
    {
        private List<ComparacionKardexStock> lista = new List<ComparacionKardexStock>();
        public Frm09VerificarStockKardex() => InitializeComponent();
        private void Frm09VerificarStockKardex_Load(object sender, EventArgs e) => Cargar();
        private void btnRefresh_Click(object sender, EventArgs e) => Cargar();
        private void txtCodigoProducto_EditValueChanged(object sender, EventArgs e) => Filtrar();
        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e) => ActualizarStock();
        private void Cargar()
        {
            lista = new BAdministracionSistema().ComparacionKardexStockListar(Parametros.intEjercicio);
            grdLista.DataSource = lista;
            grdLista.RefreshDataSource();
            viewLista.BestFitColumns();
        }

        private void Filtrar()
        {
            var dictionary = new Dictionary<string, string>();
            dictionary.Add("CodigoProducto", txtCodigoProducto.Text);
            dictionary.Add("DescripcionProducto", txtDescripcion.Text);
            Services.FilterView(viewLista, dictionary);
        }



        private void ActualizarStock()
        {
            var tabla = new DataTable();
            tabla.Columns.Add("IdProducto", typeof(int));
            tabla.Columns.Add("IdAlmacen", typeof(int));
            tabla.Columns.Add("CantidadKardex", typeof(decimal));

            lista.ForEach(x =>
            {
                DataRow row = tabla.NewRow();
                row["IdProducto"] = x.IdProducto;
                row["IdAlmacen"] = x.IdAlmacen;
                row["CantidadKardex"] = x.CantidadKardex;
                tabla.Rows.Add(row);
            });

            new BAdministracionSistema().ActualizarStock(tabla);
            Cargar();
        }
    }
}