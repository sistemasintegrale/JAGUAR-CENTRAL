using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Produccion;
using SGE.WindowForms.Otros.Producción;
using SGE.WindowForms.Producción.Caracteristicas;
using SGE.WindowForms.Reportes.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGE.WindowForms.Producción.Produccion
{
    public partial class FrmOrdenProducción : XtraForm
    {
        private void btnagregar_ItemClick(object sender, ItemClickEventArgs e) => nuevo();
        private void btnmodificar_ItemClick(object sender, ItemClickEventArgs e) => Datos();
        private void btneliminar_ItemClick(object sender, ItemClickEventArgs e) => eliminar();
        private void btnimprimir_ItemClick(object sender, ItemClickEventArgs e) => imprimir();
        private void btnmodelo_ItemClick(object sender, ItemClickEventArgs e) => modelo();
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e) => Datos();
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e) => eliminar();
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e) => imprimir();
        private void FrmMarca_Load(object sender, EventArgs e) => Carga();
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e) => nuevo();
        private void txtCodigo_KeyUp(object sender, KeyEventArgs e) => BuscarCriterio();
        private void generarReqMatToolStripMenuItem_Click(object sender, EventArgs e) => GenerarRequerimientoMaterial();
        public FrmOrdenProducción() => InitializeComponent();

        private List<EOrdenProduccion> mlist = new List<EOrdenProduccion>();
        public void Carga()
        {
            EOrdenProduccion obj = new EOrdenProduccion();
            mlist = new BCentral().ListarOrdenProduccion(Parametros.intEjercicio);
            mlist.ForEach(x => x.pedido = "P-" + x.orprc_vpedido + "." + x.orprc_vitem_pedido);
            dgrMotonave.DataSource = mlist;
        }
        void reload(int intIcod)
        {
            Carga();
            int index = mlist.FindIndex(x => x.orprc_iid_orden_produccion == intIcod);
            viewMotonave.FocusedRowHandle = index;
            viewMotonave.Focus();
        }

        private void nuevo()
        {
            frmManteOrdenProdución frm = new frmManteOrdenProdución();
            frm.MiEvento += new frmManteOrdenProdución.DelegadoMensaje(reload);
            frm.lstOrdenProduccion = mlist;
            frm.SetInsert();
            frm.Show();
            frm.txtcantidad1.Focus();

        }

        private void Datos()
        {
            EOrdenProduccion Obe = viewMotonave.GetFocusedRow() as EOrdenProduccion;
            if (Obe == null) return;
            frmManteOrdenProdución frm = new frmManteOrdenProdución();
            frm.MiEvento += new frmManteOrdenProdución.DelegadoMensaje(reload);
            frm.oBe = Obe;
            frm.lstOrdenProduccion = mlist;
            frm.SetModify();
            frm.Show();

        }


        private void eliminar()
        {
            if (XtraMessageBox.Show("Desea Eliminar el registro", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {

                EOrdenProduccion Obe = (EOrdenProduccion)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                BCentral oBl = new BCentral();
                Obe.intUsuario = Valores.intUsuario;
                Obe.vpc = WindowsIdentity.GetCurrent().Name;
                oBl.EliminarOrdenProduccion(Obe);
                viewMotonave.DeleteRow(viewMotonave.FocusedRowHandle);
                viewMotonave.RefreshData();
            }
        }

        private void imprimir()
        {

            clImprimir.Visible = true;
            clImprimir.VisibleIndex = 0;
            indicadorReporte = false;


        }
        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {
            EOrdenProduccion Obe = (EOrdenProduccion)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe is null) return;
            frmManteOrdenProdución Motonave = new frmManteOrdenProdución();
            Motonave.MiEvento += new frmManteOrdenProdución.DelegadoMensaje(reload);
            Motonave.Show();
            Motonave.SetCancel();
            Motonave.oBe = Obe;
            Motonave.setValues();
        }



        private void BuscarCriterio()
        {
            dgrMotonave.DataSource = mlist.Where(obj => obj.orprc_icod_orden_produccion.ToString().Contains(txtCodigo.Text.ToUpper())).ToList();
        }



        private void viewMotonave_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
                nuevoToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F5)
                Datos();
            if (e.KeyCode == Keys.F9)
                eliminarToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F8) { imprimir(); }

        }

        private void modelo()
        {
            EProdTablaRegistro Obe = (EProdTablaRegistro)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe is null) return;
            FrmModelo Modelo = new FrmModelo();
            Modelo.CodeMarcas = Convert.ToInt32(Obe.icod_tabla);
            Modelo.Show();

        }



        private void ingresoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (mlist.Count > 0)
                {
                    int IdSituacion = int.Parse(viewMotonave.GetFocusedRowCellValue("orprc_iid_situacion").ToString());
                    if (IdSituacion == Parametros.intSitReporteProduccionGenerado)
                    {
                        FrmManteIngresoReporteProduccion Reporte = new FrmManteIngresoReporteProduccion();
                        Reporte.MiEvento += new FrmManteIngresoReporteProduccion.DelegadoMensaje(Carga);
                        EOrdenProduccion Obe = (EOrdenProduccion)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                        Reporte.IdReporteProduccion = Obe.orprc_iid_orden_produccion;
                        Reporte.oBe = Obe;
                        Reporte.Show();
                        Reporte.txtCodigo.Text = Obe.orprc_icod_orden_produccion.ToString();
                        Reporte.txtCantidad.EditValue = Obe.orprc_ntotal;
                        Reporte.dtmFecha.DateTime = Obe.orprc_sfecha_orden_produccion;
                    }
                    else
                    {
                        string sSituacion = "";
                        if (IdSituacion == Parametros.intSitReporteProduccionActualizado) { sSituacion = "Actualizado"; }
                        XtraMessageBox.Show("No se puede ingresar el producto al almacén \n" + "Situación : " + sSituacion, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarPTDelAlmacénToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EOrdenProduccion Obe = (EOrdenProduccion)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe.orprc_iid_kardex1 != 0)
            {
                if (XtraMessageBox.Show("Desea eliminar el producto Terminado del almacén", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.orprc_iid_situacion = Parametros.intSitReporteProduccionGenerado;
                    new BCentral().EliminarPTKardex(Obe);
                }

            }
        }

        private void imprimirReporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clImprimir.Visible = true;
            clImprimir.VisibleIndex = 0;
            indicadorReporte = true;
        }

        private void nuevoVariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGenerarOrdenProduccionMultiple frm = new frmGenerarOrdenProduccionMultiple();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Carga();
            }
        }

        private void eliminarVariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!clSeleccionarEliminar.Visible)
            {
                clSeleccionarEliminar.Visible = true;
                clSeleccionarEliminar.VisibleIndex = 0;
                eliminarVariosToolStripMenuItem.Text = "Cancelar";
                guna2Button1.Visible = mlist.Exists(x => x.eliminar);
            }
            else
            {
                clSeleccionarEliminar.Visible = false;
                clSeleccionarEliminar.VisibleIndex = -1;
                eliminarVariosToolStripMenuItem.Text = "Eliminar Varios";
                guna2Button1.Visible = false;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            if (XtraMessageBox.Show("Desea Eliminar los registros seleccionados ?!", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                return;

            BCentral oBl = new BCentral();
            mlist.Where(x => x.eliminar).ToList().ForEach(Obe =>
            {


                Obe.intUsuario = Valores.intUsuario;
                Obe.vpc = WindowsIdentity.GetCurrent().Name;
                oBl.EliminarOrdenProduccionVarios(Obe);
                viewMotonave.DeleteRow(viewMotonave.FocusedRowHandle);
                viewMotonave.RefreshData();

            });

            clSeleccionarEliminar.Visible = false;
            guna2Button1.Visible = false;
        }


        private void viewMotonave_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            guna2Button1.Visible = mlist.Exists(x => x.eliminar);
            btnImprimirVarios.Visible = mlist.Exists(x => x.imprimir);
        }

        bool indicadorReporte;

        public List<ORPRReporte> TransformarLista(EOrdenProduccion Obe, List<EOrdenProduccionDet> mlista)
        {
            var lista = new List<ORPRReporte>();
            mlista.ForEach(x =>
            {
                lista.Add(new ORPRReporte
                {

                    orprc_vitem_pedido = Obe.orprc_vitem_pedido,
                    orprc_vficha_tecnica = Obe.orprc_vficha_tecnica,
                    orprc_vresponsable = Obe.orprc_vresponsable,
                    DesModelo = Obe.DesModelo,
                    DesMarca = Obe.DesMarca,
                    strtipo = Obe.strtipo,
                    strcolor = Obe.strcolor,
                    resec_vdescripcion = Obe.resec_vdescripcion.Substring((Obe.resec_vdescripcion.Length - 2), 2),
                    orprc_vlinea = Obe.orprc_vlinea,
                    orprc_ntotal = Obe.orprc_ntotal.ToString(),
                    orprc_ndocenas = Obe.orprc_ndocenas.ToString(),
                    orprc_talla1 = Obe.orprc_talla1.ToString(),
                    orprc_talla2 = Obe.orprc_talla2.ToString(),
                    orprc_talla3 = Obe.orprc_talla3.ToString(),
                    orprc_talla4 = Obe.orprc_talla4.ToString(),
                    orprc_talla5 = Obe.orprc_talla5.ToString(),
                    orprc_talla6 = Obe.orprc_talla6.ToString(),
                    orprc_talla7 = Obe.orprc_talla7.ToString(),
                    orprc_cant_talla1 = Obe.orprc_cant_talla1.ToString(),
                    orprc_cant_talla2 = Obe.orprc_cant_talla2.ToString(),
                    orprc_cant_talla3 = Obe.orprc_cant_talla3.ToString(),
                    orprc_cant_talla4 = Obe.orprc_cant_talla4.ToString(),
                    orprc_cant_talla5 = Obe.orprc_cant_talla5.ToString(),
                    orprc_cant_talla6 = Obe.orprc_cant_talla6.ToString(),
                    orprc_cant_talla7 = Obe.orprc_cant_talla7.ToString(),
                    orprc_icod_orden_produccion = "OP" + "-" + Obe.orprc_icod_orden_produccion,
                    strproceso = x.strproceso,
                    orprd_vubicacion = x.orprd_vubicacion,
                    strProducto = x.strProducto,
                    orprd_ncantidad = x.orprd_ncantidad,
                    strmedida = x.strmedida,
                });
            });

            return lista;
        }
        void compositeLink_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
        {
            RectangleF rectLeftPage = new RectangleF(0, 0, e.Graph.ClientPageSize.Width / 2, 20);
            RectangleF rectRightPage = new RectangleF(e.Graph.ClientPageSize.Width / 2, 0, e.Graph.ClientPageSize.Width / 2, 20);

            // Puedes personalizar las áreas de pie de página según tus necesidades
            e.Graph.DrawString("Izquierda", Color.Black, rectLeftPage, BorderSide.None);
            e.Graph.DrawString("Derecha", Color.Black, rectRightPage, BorderSide.None);
        }

        private void btnImprimirVarios_Click(object sender, EventArgs e)
        {
            int primero = 0;
            string path = AppDomain.CurrentDomain.BaseDirectory;

            if (!indicadorReporte)
            {

                rptOrdenProduccionMulti rptPrinciapal = new rptOrdenProduccionMulti();
                mlist.Where(x => x.imprimir).ToList().ForEach(Obe =>
                {
                    if (primero == 0)
                    {
                        primero++;
                        rptPrinciapal.Cargar(Obe, new BCentral().listarOrdenPrduccionDetalle(Obe.orprc_iid_orden_produccion));
                        rptPrinciapal.CreateDocument();
                    }
                    else
                    {
                        rptOrdenProduccionMulti rpt = new rptOrdenProduccionMulti();
                        rpt.Cargar(Obe, new BCentral().listarOrdenPrduccionDetalle(Obe.orprc_iid_orden_produccion));
                        rpt.CreateDocument();
                        rptPrinciapal.Pages.AddRange(rpt.Pages);
                        rptPrinciapal.PrintingSystem.ContinuousPageNumbering = true;
                    }

                });
                PdfExportOptions options = new PdfExportOptions();
                options.PdfACompatibility = PdfACompatibility.PdfA2b;
                string reportPath = path + Guid.NewGuid().ToString() + ".pdf";
                rptPrinciapal.ExportToPdf(reportPath, options);
                Process.Start(reportPath);
                clImprimir.Visible = false;
                btnImprimirVarios.Visible = false;
                mlist.ForEach(x => x.imprimir = false);

            }
            else
            {
                rptReporteProduccion rptPrinciapal = new rptReporteProduccion();
                mlist.Where(x => x.imprimir).ToList().ForEach(Obe =>
                {
                    if (primero == 0)
                    {
                        primero++;
                        rptPrinciapal.cargar(Obe);
                        rptPrinciapal.CreateDocument();
                    }
                    else
                    {
                        rptReporteProduccion rpt = new rptReporteProduccion();
                        rpt.cargar(Obe);
                        rpt.CreateDocument();
                        rptPrinciapal.Pages.AddRange(rpt.Pages);
                        rptPrinciapal.PrintingSystem.ContinuousPageNumbering = true;
                    }

                });
                using (ReportPrintTool printTool = new ReportPrintTool(rptPrinciapal))
                {
                    printTool.ShowPreviewDialog();
                }
                clImprimir.Visible = false;
                btnImprimirVarios.Visible = false;
                mlist.ForEach(x => x.imprimir = false);


            }
        }

        private void enableLoading(bool flag)
        {
            picLoading.Visible = flag;
            mnumotonave.Enabled = !flag;
            if (flag)
                picLoading.Dock = DockStyle.Fill;
            else
                picLoading.Dock = DockStyle.None;
        }

        public async void GenerarRequerimientoMaterial()
        {
            var Obe = viewMotonave.GetFocusedRow() as EOrdenProduccion;
            if (Obe is null) return;

            if (XtraMessageBox.Show($"Está seguro de Generar los RM?", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                return;


            enableLoading(true);
            try
            {
                var lstRMexistentes = new BCentral().ListarRequerimientoMaterial(Parametros.intEjercicio);
                string Reqgenerados = string.Empty;
                await Task.Run(() =>
                {
                    var selectOPR = new BCentral().ListarOrdenProduccion(Parametros.intEjercicio).Where(x => x.orprc_iid_orden_produccion == Obe.orprc_iid_orden_produccion).FirstOrDefault();
                    var lstMaterial = new BCentral().listarOrdenPrduccionDetalle(Obe.orprc_iid_orden_produccion);
                    var lstMaterialesOPR = new List<ERequerimientoMaterialDet>();
                    var maxCorrelativo = lstRMexistentes.Max(x => Convert.ToInt32(x.rqmac_icod_requerimiento_material));
                    foreach (var objd in lstMaterial.OrderBy(x => x.orprd_iproceso))
                    {
                        ERequerimientoMaterialDet op = new ERequerimientoMaterialDet();
                        op.rqmad_iproceso = objd.orprd_iproceso;
                        op.rqmad_iubicacion = objd.orprd_iubicacion;
                        op.strubicacion = objd.orprd_vubicacion;
                        op.rqmad_vmaterial = objd.orprd_vmaterial == null ? objd.strProducto : objd.orprd_vmaterial;
                        op.rqmad_vmaterial = objd.orprd_vmaterial == "" ? objd.strProducto : objd.orprd_vmaterial;
                        op.prdc_icod_producto = objd.prdc_icod_producto;
                        op.strCodeProducto = objd.strCodeProducto;
                        op.strProducto = objd.strProducto;
                        op.strmedida = objd.strmedida;
                        op.rqmad_ncantidad_requerida = objd.orprd_ncantidad;
                        op.orprd_imedida = objd.orprd_imedida;
                        lstMaterialesOPR.Add(op);
                    }

                    var lstKeyproceso = lstMaterialesOPR.GroupBy(x => x.rqmad_iproceso).ToList().Select(x => x.Key).ToList();
                    lstKeyproceso.ForEach(x =>
                    {

                        ERequerimientoMaterial oBe = new ERequerimientoMaterial();
                        oBe.rqmac_icod_requerimiento_material = (++maxCorrelativo).ToString("d4");

                        oBe.rqmac_sfecha_requerimiento_material = DateTime.Now;
                        oBe.rqmac_iorden_produccion = selectOPR.orprc_iid_orden_produccion;
                        oBe.strordenproduccion = selectOPR.orprc_icod_orden_produccion;
                        oBe.rqmac_iproceso = x;
                        oBe.rqmac_ipedido = selectOPR.orprc_ipedido;
                        oBe.strpedido = selectOPR.orprc_vpedido;
                        oBe.rqmac_iitem_pedido = selectOPR.orprc_iitem_pedido;
                        oBe.stritempedido = Convert.ToInt32(selectOPR.orprc_vitem_pedido);
                        oBe.rqmac_ificha_tecnica = selectOPR.orprc_ificha_tecnica;
                        oBe.rqmac_imarca = selectOPR.orprc_imarca;
                        oBe.rqmac_imodelo = selectOPR.orprc_imodelo;
                        oBe.rqmac_icolor = selectOPR.orprc_icolor;
                        oBe.rqmac_itipo = selectOPR.orprc_itipo;
                        oBe.rqmac_iserie = selectOPR.orprc_iserie;
                        oBe.rqmac_talla1 = selectOPR.orprc_talla1;
                        oBe.rqmac_talla2 = selectOPR.orprc_talla2;
                        oBe.rqmac_talla3 = selectOPR.orprc_talla3;
                        oBe.rqmac_talla4 = selectOPR.orprc_talla4;
                        oBe.rqmac_talla5 = selectOPR.orprc_talla5;
                        oBe.rqmac_talla6 = selectOPR.orprc_talla6;
                        oBe.rqmac_talla7 = selectOPR.orprc_talla7;
                        oBe.rqmac_talla8 = selectOPR.orprc_talla8;
                        oBe.rqmac_talla9 = selectOPR.orprc_talla9;
                        oBe.rqmac_talla10 = selectOPR.orprc_talla10;
                        oBe.rqmac_cant_talla1 = selectOPR.orprc_cant_talla1;
                        oBe.rqmac_cant_talla2 = selectOPR.orprc_cant_talla2;
                        oBe.rqmac_cant_talla3 = selectOPR.orprc_cant_talla3;
                        oBe.rqmac_cant_talla4 = selectOPR.orprc_cant_talla4;
                        oBe.rqmac_cant_talla5 = selectOPR.orprc_cant_talla5;
                        oBe.rqmac_cant_talla6 = selectOPR.orprc_cant_talla6;
                        oBe.rqmac_cant_talla7 = selectOPR.orprc_cant_talla7;
                        oBe.rqmac_cant_talla8 = selectOPR.orprc_cant_talla8;
                        oBe.rqmac_cant_talla9 = selectOPR.orprc_cant_talla9;
                        oBe.rqmac_cant_talla10 = selectOPR.orprc_cant_talla10;
                        oBe.rqmac_icod_producto1 = selectOPR.orprc_icod_producto1;
                        oBe.rqmac_icod_producto2 = selectOPR.orprc_icod_producto2;
                        oBe.rqmac_icod_producto3 = selectOPR.orprc_icod_producto3;
                        oBe.rqmac_icod_producto4 = selectOPR.orprc_icod_producto4;
                        oBe.rqmac_icod_producto5 = selectOPR.orprc_icod_producto5;
                        oBe.rqmac_icod_producto6 = selectOPR.orprc_icod_producto6;
                        oBe.rqmac_icod_producto7 = selectOPR.orprc_icod_producto7;
                        oBe.rqmac_icod_producto8 = selectOPR.orprc_icod_producto8;
                        oBe.rqmac_icod_producto9 = selectOPR.orprc_icod_producto9;
                        oBe.rqmac_icod_producto10 = selectOPR.orprc_icod_producto10;
                        oBe.rqmac_ntotal = Total(selectOPR);
                        oBe.rqmac_ndocenas = Docenas(selectOPR);
                        oBe.rqmac_ilinea = selectOPR.orprc_ilinea;
                        oBe.intUsuario = Valores.intUsuario;
                        oBe.vpc = WindowsIdentity.GetCurrent().Name.ToString();
                        oBe.rqmac_iid_situacion = Parametros.intSitReporteProduccionGenerado;
                        oBe.rqmac_isituacion = new BGeneral().listarTablaRegistro(93).FirstOrDefault().tarec_iid_tabla_registro;
                        oBe.rqmac_iid_almacen = new BAlmacen().listarAlmacenes().FirstOrDefault().almac_icod_almacen;
                        if (!lstRMexistentes.Exists(RM => RM.rqmac_iorden_produccion == selectOPR.orprc_iid_orden_produccion && RM.rqmac_iproceso == x))
                        {
                            new BCentral().InsertarRequerimientoMaterial(oBe, lstMaterialesOPR.Where(y => y.rqmad_iproceso == x).ToList());
                            Reqgenerados += oBe.rqmac_icod_requerimiento_material + " - ";
                        }

                    });
                });
                string mensaje = Reqgenerados.Length != 0 ? Reqgenerados.Substring(0, Reqgenerados.Length - 3) : Reqgenerados;
                mensaje = mensaje.Length == 0 ? "La OPR ya tiene registrada sus RM de todas sus Areas" : $"Requerimiento Material N° {mensaje} Fueron Generados Correctamente.";
                XtraMessageBox.Show(mensaje, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                enableLoading(false);
            }


        }

        private int Total(EOrdenProduccion opr)
        {
            return opr.orprc_cant_talla1 + opr.orprc_cant_talla2 + opr.orprc_cant_talla3 + opr.orprc_cant_talla4 + opr.orprc_cant_talla5 +
                opr.orprc_cant_talla6 + opr.orprc_cant_talla7 + opr.orprc_cant_talla8 + opr.orprc_cant_talla9 + opr.orprc_cant_talla10;
        }
        private decimal Docenas(EOrdenProduccion opr)
        {
            return Total(opr) / 12;
        }
    }
}