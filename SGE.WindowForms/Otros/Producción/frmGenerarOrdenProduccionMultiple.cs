using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Producción
{
    public partial class frmGenerarOrdenProduccionMultiple : XtraForm
    {
        public List<EProdTablaRegistro> lstLinea = new List<EProdTablaRegistro>();
        public EOrdenProduccion obj = new EOrdenProduccion();
        public List<EOrdenProduccion> lstOrdenProduc = new List<EOrdenProduccion>();
        List<EOrdenProduccionAreas> lstOrdenProduccionAreas = new List<EOrdenProduccionAreas>();
        List<EFichaTecnicaDet> lstMaterial = new List<EFichaTecnicaDet>();
        List<EOrdenProduccionDet> lstOrdenProduccionDetalle = new List<EOrdenProduccionDet>();
        GridColumn[] Columns;
        TextEdit[] CantidadesPorProcesar;
        TextEdit[] Tallas;
        TextEdit[] Factor;
        TextEdit[] CantidadesPorProcesarF;
        TextEdit[] Direrencia;

        public frmGenerarOrdenProduccionMultiple() => InitializeComponent();
        private void frmGenerarOrdenProduccionMultiple_Load(object sender, EventArgs e) => Cargar();
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e) => Dispose();
        private async void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show("Está seguro de guardar los registros?", "Infomarción del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                    return;
                if (XtraMessageBox.Show("Se procederá a guardar los registros, Continuar?", "Infomarción del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                    return;
                txtContadorOrdenes.Text = "";
                txtContadorOrdenes.Visible = true;
                int contadorOrdenes = 0;
                enableLoading(true);
                await Task.Run(() =>
                {
                    lstOrdenProduc.ForEach(obe =>
                    {
                        new BCentral().InsertarOrdenProduccion(obe, lstOrdenProduccionDetalle, lstOrdenProduccionAreas);

                        contadorOrdenes++;
                        this.Invoke((MethodInvoker)delegate
                        {

                            txtContadorOrdenes.Text = $"{contadorOrdenes} de {txtTotalORP.Text} Ordenes de produccion guardadas...";
                        });
                    });
                });
                DialogResult = DialogResult.OK;
                enableLoading(false);
            }
            catch (Exception ex)
            {
                enableLoading(false);
                throw ex;
            }

        }

        private void enableLoading(bool flag)
        {
            picLoading.Visible = flag;
            barButtonItem2.Enabled = !flag;
            barButtonItem1.Enabled = !flag;
            simpleButton2.Enabled = !flag;
            if (flag)
                picLoading.Dock = DockStyle.Fill;
            else
                picLoading.Dock = DockStyle.None;
        }

        public void GenerarAreasproduccion()
        {
            var lstProceso = new BCentral().listarAreaProduccion().Where(x => lstMaterial.Exists(y => y.fited_iarea == x.arprc_iid_codigo)).ToList();
            foreach (var objd in lstProceso)
            {
                EOrdenProduccionAreas op = new EOrdenProduccionAreas();
                op.orprac_icod_proceso = objd.arprc_iid_codigo;
                op.strproceso = objd.arprc_vdescripcion;
                if (op.orprac_sfecha_asignacion == Convert.ToString("01/01/0001 0:00:00"))
                {
                    op.orprac_sfecha_asignacion = "";
                }
                if (op.orprac_sfecha_terminado == Convert.ToString("01/01/0001 0:00:00"))
                {
                    op.orprac_sfecha_terminado = "";
                }
                lstOrdenProduccionAreas.Add(op);
            }

        }

        private void Cargar()
        {
            EProdTablaRegistro ob = new EProdTablaRegistro();
            lstLinea = new BCentral().ListarProdTablaRegistro(new EProdTablaRegistro() { iid_tipo_tabla = 3 });
            BSControls.LoaderLook(LkpMarca, new BCentral().ListarProdTablaRegistro(new EProdTablaRegistro() { iid_tipo_tabla = 11 }), "descripcion", "icod_tabla", true);
            BSControls.LoaderLook(LkpColor, new BCentral().ListarProdTablaRegistro(new EProdTablaRegistro() { iid_tipo_tabla = 8 }), "descripcion", "icod_tabla", true);
            BSControls.LoaderLook(lkpSerie, new BCentral().ListarRegistroSerie(), "resec_vdescripcion", "resec_iid_registro_serie", true);
            BSControls.LoaderLook(lkpTipo, new BCentral().ListarProdTablaRegistro(new EProdTablaRegistro() { iid_tipo_tabla = 2 }), "descripcion", "icod_tabla", true);
            BSControls.LoaderLook(lkpLinea, lstLinea, "descripcion", "icod_tabla", true);
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(95), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpResponsable, new BCompras().ListarProveedorComboOPE(), "proc_responsable_produccion", "iid_icod_proveedor", true);
            lkpSituacion.Enabled = false;
            btnFichaTecnica.Enabled = false;
            dteFecha.DateTime = DateTime.Now;
            Columns = new GridColumn[] { clTalla1, clTalla2, clTalla3, clTalla4, clTalla5, clTalla6, clTalla7, clTalla8, clTalla9, clTalla10 };
            CantidadesPorProcesar = new TextEdit[] { txtProcesar1, txtProcesar2, txtProcesar3, txtProcesar4, txtProcesar5, txtProcesar6, txtProcesar7, txtProcesar8, txtProcesar9, txtProcesar10 };
            Tallas = new TextEdit[] { txttalla1, txttalla2, txttalla3, txttalla4, txttalla5, txttalla6, txttalla7, txttalla8, txttalla9, txttalla10 };
            Factor = new TextEdit[] { txtFactor1, txtFactor2, txtFactor3, txtFactor4, txtFactor5, txtFactor6, txtFactor7, txtFactor8, txtFactor9, txtFactor10 };
            CantidadesPorProcesarF = new TextEdit[] { txtProcesarF1, txtProcesarF2, txtProcesarF3, txtProcesarF4, txtProcesarF5, txtProcesarF6, txtProcesarF7, txtProcesarF8, txtProcesarF9, txtProcesarF10 };
            Direrencia = new TextEdit[] { txtDireferencia1, txtDireferencia2, txtDireferencia3, txtDireferencia4, txtDireferencia5, txtDireferencia6, txtDireferencia7, txtDireferencia8, txtDireferencia9, txtDireferencia10 };
        }

        private void btnPedido_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            using (ListarOrdenPedido frm = new ListarOrdenPedido())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnPedido.Tag = frm._Be.orpec_iid_orden_pedido;
                    btnPedido.Text = frm._Be.orpec_icod_orden_pedido;
                }
            }
        }

        private void btnItemPedido_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            using (ListarOrdenPedidoDetalle frm = new ListarOrdenPedidoDetalle())
            {
                frm.icod = Convert.ToInt32(btnPedido.Tag);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnItemPedido.Tag = frm._Be.orped_icod_item_orden_pedido;
                    btnItemPedido.Text = frm._Be.orped_iitem_orden_pedido.ToString();
                    btnFichaTecnica.Tag = frm._Be.orped_ificha_tecnica;
                    btnFichaTecnica.Text = frm._Be.strfichatecnica;
                    LkpMarca.EditValue = frm._Be.orped_imarca;
                    btnmodelo.Tag = frm._Be.orped_imodelo;
                    btnmodelo.Text = frm._Be.strmodelo;
                    lkpTipo.EditValue = frm._Be.orped_itipo;
                    LkpColor.EditValue = frm._Be.orped_icolor;
                    lkpSerie.EditValue = frm._Be.orped_serie;
                    lkpLinea.EditValue = frm._Be.fitec_ilinea;
                    lkpResponsable.EditValue = frm._Be.orped_iresponsable;
                    txtProcesar1.Text = frm._Be.orped_cant_talla1.ToString();
                    txtFactor1.ReadOnly = !(frm._Be.orped_cant_talla1 > 0);
                    txtProcesar2.Text = frm._Be.orped_cant_talla2.ToString();
                    txtFactor2.ReadOnly = !(frm._Be.orped_cant_talla2 > 0);
                    txtProcesar3.Text = frm._Be.orped_cant_talla3.ToString();
                    txtFactor3.ReadOnly = !(frm._Be.orped_cant_talla3 > 0);
                    txtProcesar4.Text = frm._Be.orped_cant_talla4.ToString();
                    txtFactor4.ReadOnly = !(frm._Be.orped_cant_talla4 > 0);
                    txtProcesar5.Text = frm._Be.orped_cant_talla5.ToString();
                    txtFactor5.ReadOnly = !(frm._Be.orped_cant_talla5 > 0);
                    txtProcesar6.Text = frm._Be.orped_cant_talla6.ToString();
                    txtFactor6.ReadOnly = !(frm._Be.orped_cant_talla6 > 0);
                    txtProcesar7.Text = frm._Be.orped_cant_talla7.ToString();
                    txtFactor7.ReadOnly = !(frm._Be.orped_cant_talla7 > 0);
                    txtProcesar8.Text = frm._Be.orped_cant_talla8.ToString();
                    txtFactor8.ReadOnly = !(frm._Be.orped_cant_talla8 > 0);
                    txtProcesar9.Text = frm._Be.orped_cant_talla9.ToString();
                    txtFactor9.ReadOnly = !(frm._Be.orped_cant_talla9 > 0);
                    txtProcesar10.Text = frm._Be.orped_cant_talla10.ToString();
                    txtFactor10.ReadOnly = !(frm._Be.orped_cant_talla10 > 0);
                    var modeloSelect = new BCentral().ModeloGetById(Convert.ToInt32(btnmodelo.Tag));
                    lkpLinea.EditValue = lstLinea.Where(x => x.tarec_iid_correlativo == modeloSelect.pr_iid_linea).FirstOrDefault().icod_tabla;
                    lkpLinea.Enabled = false;

                    List<ERegistroSerie> lstSerie = new BCentral().ListarRegistroSerie().Where(x => x.resec_iid_registro_serie == Convert.ToInt32(lkpSerie.EditValue)).ToList();


                    int i = -1;

                    for (int x = Convert.ToInt32(lstSerie[0].resec_vtalla_inicial); x <= Convert.ToInt32(lstSerie[0].resec_vtalla_final); x++)
                    {

                        i++;
                        Columns[i].Caption = string.Format("{0:00}", x).ToString();
                        Columns[i].Visible = x > 0;
                        Tallas[i].Text = string.Format("{0:00}", x).ToString();
                        CantidadesPorProcesarF[i].Visible = x > 0;
                        Direrencia[i].Visible = x > 0;
                    }

                    clTotal.Visible = true;
                    clDocenas.Visible = true;

                    btnItemPedido.Enabled = false;
                    btnFichaTecnica.Enabled = false;
                    LkpMarca.Enabled = false;
                    btnmodelo.Enabled = false;
                    lkpTipo.Enabled = false;
                    LkpColor.Enabled = false;
                    lkpSerie.Enabled = false;
                    lkpResponsable.Enabled = false;
                }

            }
        }

        private async void simpleButton2_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Está seguro de generar lor OPR?", "Infomarción del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                return;


            enableLoading(true);
            lstOrdenProduc.Clear();

            var Correlativo = new BCentral().UltimoCorrelativoTabla(Tablas.ORDEN_PRODUCCION);



            int[] CantidadMaxima = new int[] {
            Convert.ToInt32(txtProcesar1.Text),
            Convert.ToInt32(txtProcesar2.Text),
            Convert.ToInt32(txtProcesar3.Text),
            Convert.ToInt32(txtProcesar4.Text),
            Convert.ToInt32(txtProcesar5.Text),
            Convert.ToInt32(txtProcesar6.Text),
            Convert.ToInt32(txtProcesar7.Text),
            Convert.ToInt32(txtProcesar8.Text),
            Convert.ToInt32(txtProcesar9.Text),
            Convert.ToInt32(txtProcesar10.Text)
            };

            int[] valores = new int[CantidadMaxima.Length];


            bool seguir = true;
            await Task.Run(() =>
            {
                while (seguir)
                {
                    Correlativo++;

                    for (int i = 0; i < CantidadMaxima.Length; i++)
                    {
                        if (CantidadMaxima[i] >= Convert.ToInt32(Factor[i].Text))
                        {
                            valores[i] = Convert.ToInt32(Factor[i].Text);
                            CantidadMaxima[i] = CantidadMaxima[i] - Convert.ToInt32(Factor[i].Text);

                        }
                        else
                        {
                            valores[i] = CantidadMaxima[i];
                            CantidadMaxima[i] = 0;
                        }


                    }

                    if (valores.Sum(x => x) != Convert.ToInt32(txtTotal.Text) && CantidadMaxima.Sum(x => x) > 0)
                    {
                        if ((CantidadMaxima.Sum(x => x) + valores.Sum(x => x)) >= Convert.ToInt32(txtTotal.Text))
                        {
                            for (int i = 0; i < CantidadMaxima.Length; i++)
                            {
                                if (CantidadMaxima[i] > 0)
                                {

                                    for (int y = i; y < CantidadMaxima.Length; y++)
                                    {
                                        int xx = valores[y];
                                        string txt = Factor[y].Text;
                                        if (valores[y] != 0 && Convert.ToInt32(Factor[y].Text) != 0 && valores.Sum(x => x) != Convert.ToInt32(txtTotal.Text))
                                        {
                                            int siguiente = i;

                                            while (valores.Sum(x => x) != Convert.ToInt32(txtTotal.Text) && CantidadMaxima[i] != 0)
                                            {
                                                int diferencia = Convert.ToInt32(txtTotal.Text) - valores.Sum(x => x);
                                                if (CantidadMaxima[siguiente] >= diferencia)
                                                {
                                                    valores[y] = valores[y] + diferencia;
                                                    CantidadMaxima[siguiente] = CantidadMaxima[siguiente] - diferencia;
                                                }
                                                else
                                                {
                                                    valores[y] = valores[y] + CantidadMaxima[siguiente];
                                                    CantidadMaxima[siguiente] = 0;
                                                    siguiente++;

                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < CantidadMaxima.Length; i++)
                            {
                                valores[i] = CantidadMaxima[i];
                                CantidadMaxima[i] = 0;
                            }

                        }
                    }

                    var obj = new EOrdenProduccion();
                    obj.orprc_icod_orden_produccion = Correlativo.ToString("D6");
                    obj.orprc_sfecha_orden_produccion = Convert.ToDateTime(dteFecha.Text);
                    obj.orprc_ipedido = Convert.ToInt32(btnPedido.Tag);
                    obj.orprc_vpedido = btnPedido.Text;
                    obj.orprc_iitem_pedido = Convert.ToInt32(btnItemPedido.Tag);
                    obj.orprc_vitem_pedido = btnItemPedido.Text;
                    obj.pedido = "P-" + btnPedido.Text + "." + btnItemPedido.Text;
                    obj.orprc_ificha_tecnica = Convert.ToInt32(btnFichaTecnica.Tag);
                    obj.orprc_vficha_tecnica = btnFichaTecnica.Text;
                    obj.orprc_imarca = Convert.ToInt32(LkpMarca.EditValue);
                    obj.orprc_imodelo = Convert.ToInt32(btnmodelo.Tag);
                    obj.orprc_icolor = Convert.ToInt32(LkpColor.EditValue);
                    obj.orprc_itipo = Convert.ToInt32(lkpTipo.EditValue);
                    obj.orprc_iserie = Convert.ToInt32(lkpSerie.EditValue);
                    obj.orprc_talla1 = Convert.ToInt32(Tallas[0].Text);
                    obj.orprc_talla2 = Convert.ToInt32(Tallas[1].Text);
                    obj.orprc_talla3 = Convert.ToInt32(Tallas[2].Text);
                    obj.orprc_talla4 = Convert.ToInt32(Tallas[3].Text);
                    obj.orprc_talla5 = Convert.ToInt32(Tallas[4].Text);
                    obj.orprc_talla6 = Convert.ToInt32(Tallas[5].Text);
                    obj.orprc_talla7 = Convert.ToInt32(Tallas[6].Text);
                    obj.orprc_talla8 = Convert.ToInt32(Tallas[7].Text);
                    obj.orprc_talla9 = Convert.ToInt32(Tallas[8].Text);
                    obj.orprc_talla10 = Convert.ToInt32(Tallas[9].Text);
                    obj.orprc_cant_talla1 = valores[0];
                    obj.orprc_cant_talla2 = valores[1];
                    obj.orprc_cant_talla3 = valores[2];
                    obj.orprc_cant_talla4 = valores[3];
                    obj.orprc_cant_talla5 = valores[4];
                    obj.orprc_cant_talla6 = valores[5];
                    obj.orprc_cant_talla7 = valores[6];
                    obj.orprc_cant_talla8 = valores[7];
                    obj.orprc_cant_talla9 = valores[8];
                    obj.orprc_cant_talla10 = valores[9];
                    obj.orprc_cant_aprocesar1 = Convert.ToInt32(txtProcesar1.Text);
                    obj.orprc_cant_aprocesar2 = Convert.ToInt32(txtProcesar2.Text);
                    obj.orprc_cant_aprocesar3 = Convert.ToInt32(txtProcesar3.Text);
                    obj.orprc_cant_aprocesar4 = Convert.ToInt32(txtProcesar4.Text);
                    obj.orprc_cant_aprocesar5 = Convert.ToInt32(txtProcesar5.Text);
                    obj.orprc_cant_aprocesar6 = Convert.ToInt32(txtProcesar6.Text);
                    obj.orprc_cant_aprocesar7 = Convert.ToInt32(txtProcesar7.Text);
                    obj.orprc_cant_aprocesar8 = Convert.ToInt32(txtProcesar8.Text);
                    obj.orprc_cant_aprocesar9 = Convert.ToInt32(txtProcesar9.Text);
                    obj.orprc_cant_aprocesar10 = Convert.ToInt32(txtProcesar10.Text);
                    obj.orprc_ntotal = Total(obj);
                    obj.orprc_ndocenas = Math.Round(Convert.ToDecimal(obj.orprc_ntotal) / 12, 4);
                    obj.orprc_ilinea = Convert.ToInt32(lkpLinea.EditValue);
                    obj.orprc_vlinea = lkpLinea.Text;
                    obj.orprc_iresponsable = Convert.ToInt32(lkpResponsable.EditValue);
                    obj.orprc_vresponsable = lkpResponsable.Text;
                    obj.intUsuario = Valores.intUsuario;
                    obj.vpc = WindowsIdentity.GetCurrent().Name.ToString();
                    obj.orprc_iid_situacion = Parametros.intSitReporteProduccionGenerado;
                    obj.orprc_isituacion = Convert.ToInt32(lkpSituacion.EditValue);
                    lstOrdenProduc.Add(obj);

                    if (CantidadMaxima.Sum(x => x) == 0)
                    {
                        seguir = false;
                    }

                }
                lstMaterial = new BCentral().ListarFichaTecnicaDet(Convert.ToInt32(btnFichaTecnica.Tag));

                GenerarAreasproduccion();
                GenerOrdenProduccionDetalle();
            });

            for (int i = 0; i < Factor.Length; i++)
            {
                Factor[i].Enabled = false;
            }

            grdLista.DataSource = lstOrdenProduc;
            grdLista.RefreshDataSource();

            enableLoading(false);
            simpleButton2.Enabled = false;
            RefreshDireferencia();
        }



        public void GenerOrdenProduccionDetalle()
        {


            foreach (var objd in lstMaterial)
            {
                EOrdenProduccionDet op = new EOrdenProduccionDet();
                op.orprd_iitem_orden_produccion = objd.fited_iitem_ficha_tecnica;
                op.orprd_iproceso = objd.fited_iarea;
                op.strproceso = objd.strarea;
                op.orprd_iubicacion = objd.fited_iubicacion;
                op.orprd_vubicacion = objd.strubicacion;
                op.orprd_vmaterial = objd.fited_vdescripcion == "" ? objd.strProducto : objd.fited_vdescripcion;
                op.prdc_icod_producto = objd.prdc_icod_producto;
                op.strCodeProducto = objd.strCodeProducto;
                op.strProducto = objd.strProducto;
                op.strmedida = objd.strUnidadMedida;
                op.orprd_ncantidad = objd.fited_ncantidad;
                op.orprd_imedida = objd.fited_imedida;
                op.intTipoOperacion = 1;
                lstOrdenProduccionDetalle.Add(op);
            }
        }




        public int Total(EOrdenProduccion Ojb)
        {
            var Suma =
           Ojb.orprc_cant_talla1 +
           Ojb.orprc_cant_talla2 +
           Ojb.orprc_cant_talla3 +
           Ojb.orprc_cant_talla4 +
           Ojb.orprc_cant_talla5 +
           Ojb.orprc_cant_talla6 +
           Ojb.orprc_cant_talla7 +
           Ojb.orprc_cant_talla8 +
           Ojb.orprc_cant_talla9 +
           Ojb.orprc_cant_talla10;

            return Suma;
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            var obe = viewLista.GetFocusedRow() as EOrdenProduccion;
            if (obe is null) return;
            if (XtraMessageBox.Show("Está seguro de eliminar el registro?", "Infomarción del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                return;
            lstOrdenProduc.Remove(obe);
            viewLista.RefreshData();
        }

        private void viewLista_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var obe = viewLista.GetFocusedRow() as EOrdenProduccion;
            if (obe is null) return;
            obe.orprc_ntotal = Total(obe);
            obe.orprc_ndocenas = Math.Round(Convert.ToDecimal(obj.orprc_ntotal) / 12, 4);
            viewLista.RefreshData();
            RefreshDireferencia();
        }

        void RefreshDireferencia()
        {
            for (int i = 0; i < Columns.Length; i++)
            {
                var sum = Columns[i].SummaryText == "" ? "0" : Columns[i].SummaryText;

                Direrencia[i].Text = (Convert.ToInt32(CantidadesPorProcesarF[i].Text) - Convert.ToInt32(sum)).ToString();
            }
        }

        private void txtFactor1_EditValueChanged(object sender, EventArgs e)
        {
            txtTotalFactor.Text = Factor.Sum(x => Convert.ToInt32(x.Text)).ToString();
        }

        private void txtProcesar_EditValueChanged(object sender, EventArgs e)
        {
            var textEdit = sender as TextEdit;
            int index = Array.IndexOf(CantidadesPorProcesar, textEdit);
            CantidadesPorProcesarF[index].Text = textEdit.Text;
            txtTotalCantidades.Text = CantidadesPorProcesar.Sum(x => Convert.ToInt32(x.Text)).ToString();

            txtTotalORP.Text = txtTotal.Text != "0" ? Math.Ceiling(Convert.ToDecimal(txtTotalCantidades.Text) / Convert.ToDecimal(txtTotal.Text)).ToString() : "0";

        }

        private void txtTotal_EditValueChanged(object sender, EventArgs e)
        {
            if (!(sender as TextEdit).ContainsFocus) return;


            txtTotalORP.Text = txtTotal.Text != "0" ? Math.Ceiling(Convert.ToDecimal(txtTotalCantidades.Text) / Convert.ToDecimal(txtTotal.Text)).ToString() : "0";

            for (int i = 0; i < CantidadesPorProcesar.Length; i++)
            {
                if (Convert.ToInt32(CantidadesPorProcesar[i].Text) != 0)
                {
                    if (Convert.ToInt32(txtTotalORP.Text) == 0)
                        Factor[i].Text = 0.ToString();
                    else
                        Factor[i].Text = Math.Round((double)(Convert.ToInt32(CantidadesPorProcesar[i].Text) / Convert.ToInt32(txtTotalORP.Text))).ToString();
                }

            }

        }

        private void txtTotalFactor_EditValueChanged(object sender, EventArgs e)
        {
            simpleButton2.Enabled = Convert.ToInt32(txtTotalFactor.Text) != 0;
        }
    }
}