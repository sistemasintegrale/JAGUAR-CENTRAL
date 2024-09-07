using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;

namespace SGE.WindowForms.Otros.Almacen
{
    public partial class FrmCommonForm : DevExpress.XtraEditors.XtraForm
    {
        public FrmCommonForm()
        {
            InitializeComponent();
        }

        public List<EProdKardex> listaKardex { get; internal set; }

        private void ejecuatarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region insert masivo kardex
            var dataTableKardex = new DataTable();
            dataTableKardex.Columns.Add("kardc_iid_correlativo", typeof(int));
            dataTableKardex.Columns.Add("kardc_ianio", typeof(int));
            dataTableKardex.Columns.Add("kardc_fecha_movimiento", typeof(DateTime));
            dataTableKardex.Columns.Add("kardc_iid_almacen", typeof(int));
            dataTableKardex.Columns.Add("kardc_iid_producto", typeof(int));
            dataTableKardex.Columns.Add("puvec_icod_punto_venta", typeof(int));
            dataTableKardex.Columns.Add("kardc_icantidad_prod", typeof(decimal));
            dataTableKardex.Columns.Add("kardc_inumero_nota", typeof(int));
            dataTableKardex.Columns.Add("kardc_iid_tipo_doc", typeof(int));
            dataTableKardex.Columns.Add("kardc_inumero_doc", typeof(string));
            dataTableKardex.Columns.Add("kardc_itipo_movimiento", typeof(int));
            dataTableKardex.Columns.Add("kardc_iid_motivo", typeof(int));
            dataTableKardex.Columns.Add("kardc_vbeneficiario", typeof(string));
            dataTableKardex.Columns.Add("kardc_vobservaciones", typeof(string));
            dataTableKardex.Columns.Add("intUsuario", typeof(int));
            int UltimoCorrelativoKardex = new BCentral().UltimoCorrelativoTabla("SGE_PVT_KARDEX");
            foreach (var kardex in listaKardex)
            {
                dataTableKardex.Rows.Add(
                    ++UltimoCorrelativoKardex,
                    Parametros.intEjercicio,
                    kardex.kardx_sfecha,
                    kardex.iid_almacen,
                    kardex.iid_producto,
                    kardex.puvec_icod_punto_venta,
                    kardex.Cantidad,
                    kardex.NroNota,
                    kardex.iid_tipo_doc,
                    kardex.NroDocumento,
                    kardex.movimiento,
                    kardex.Motivo,
                    kardex.Beneficiario,
                    kardex.Observaciones,
                    kardex.intUsuario
                );
            }
            new BCentral().InsertMasivo(dataTableKardex, "SGEA_KARDEX_PVT_INSERTAR_MASIVO");
            #endregion
        }

        private void FrmCommonForm_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = listaKardex;
            gridControl1.Refresh();
        }
    }
}