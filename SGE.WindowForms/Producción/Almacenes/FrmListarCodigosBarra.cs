using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGE.WindowForms.Producción.Almacenes
{
    public partial class FrmListarCodigosBarra : DevExpress.XtraEditors.XtraForm
    {
        List<EProdCodigoBarra> mlista = new List<EProdCodigoBarra>();
        BCentral oblCodiBarr = new BCentral();
        private int xposition = 0;

        public FrmListarCodigosBarra()
        {
            InitializeComponent();
        }
        private void Cargar()
        {
            mlista = oblCodiBarr.ListarProdCodigoBarra();
            dgrcodigoBarra.DataSource = mlista;
        }
        private void FrmListarCodigosBarra_Load(object sender, EventArgs e)
        {
            Cargar();
        }
        void Modify()
        {
            Cargar();
            gvCodigosBarra.FocusedRowHandle = xposition;
        }
        private void nuevo()
        {
            FrmMantCodigosBarra frmCodi = new FrmMantCodigosBarra();
            frmCodi.MiEvento += new FrmMantCodigosBarra.DelegadoMensaje(Modify);
            if (mlista.Count > 0)
            {
                frmCodi.txtNroTransferencia.Text = (mlista.Max(ob => ob.picbc_inumero_plant) + 1).ToString();
            }
            else
                frmCodi.txtNroTransferencia.Text = "1";
            frmCodi.Show();
            frmCodi.SetInsert();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlista.Count > 0)
            {
                Datos();
            }
            else
                XtraMessageBox.Show("No hay registro por modificar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void Datos()
        {
            EProdCodigoBarra Obee = (EProdCodigoBarra)gvCodigosBarra.GetRow(gvCodigosBarra.FocusedRowHandle);
            if (Obee != null)
            {
                FrmMantCodigosBarra FrmCodiBarra = new FrmMantCodigosBarra();
                FrmCodiBarra.MiEvento += new FrmMantCodigosBarra.DelegadoMensaje(Modify);
                FrmCodiBarra.SetModify();
                FrmCodiBarra.picbd_icod_plan_cod_barr = Obee.picbc_icod_plan_cod_barr;
                FrmCodiBarra.Show();
                FrmCodiBarra.txtNroTransferencia.Text = Obee.picbc_inumero_plant.ToString();
                FrmCodiBarra.dtpFecha.EditValue = Obee.picbc_sfecha_plant;
                FrmCodiBarra.txtObservaciones.Text = Obee.picbc_vdescrip;
                FrmCodiBarra.LkpMarca.EditValue = Obee.picbc_iicod_marca;
                FrmCodiBarra.btnmodelo.Tag = Obee.picbc_iicod_modelo;
                FrmCodiBarra.btnmodelo.Text = Obee.DescripModelo;
                FrmCodiBarra.btnGenerar.Enabled = false;
                xposition = gvCodigosBarra.FocusedRowHandle;
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }
        private void eliminar()
        {
            if (mlista.Count > 0)
            {
                EProdCodigoBarra Obee = (EProdCodigoBarra)gvCodigosBarra.GetRow(gvCodigosBarra.FocusedRowHandle);
                Obee.iusuario = 1;
                if (XtraMessageBox.Show("¿Está seguro de Eliminar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    oblCodiBarr.EliminarProdCodigoBarra(Obee);
                    gvCodigosBarra.DeleteRow(gvCodigosBarra.FocusedRowHandle);
                }

            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }
        private void imprimir()
        {
            if (rbCentral.Checked == true)
            {


                int[] talla = new int[10];
                int[] cantidad = new int[10];


                List<EProdCodigoBarraDetalle> mlistDet = new List<EProdCodigoBarraDetalle>();
                EProdCodigoBarra Obee = (EProdCodigoBarra)gvCodigosBarra.GetRow(gvCodigosBarra.FocusedRowHandle);
                if (Obee != null)
                {
                    string CodigoExterno;
                    mlistDet = oblCodiBarr.listarProdCodigoBarraDetalle(Obee.picbc_icod_plan_cod_barr);

                    foreach (EProdCodigoBarraDetalle objDe in mlistDet)
                    {
                        CodigoExterno = objDe.pr_vcodigo_externo.Substring(0, 13);
                        talla[0] = objDe.picbd_talla1;
                        talla[1] = objDe.picbd_talla2;
                        talla[2] = objDe.picbd_talla3;
                        talla[3] = objDe.picbd_talla4;
                        talla[4] = objDe.picbd_talla5;
                        talla[5] = objDe.picbd_talla6;
                        talla[6] = objDe.picbd_talla7;
                        talla[7] = objDe.picbd_talla8;
                        talla[8] = objDe.picbd_talla9;
                        talla[9] = objDe.picbd_talla10;
                        cantidad[0] = objDe.picbd_cant_talla1;
                        cantidad[1] = objDe.picbd_cant_talla2;
                        cantidad[2] = objDe.picbd_cant_talla3;
                        cantidad[3] = objDe.picbd_cant_talla4;
                        cantidad[4] = objDe.picbd_cant_talla5;
                        cantidad[5] = objDe.picbd_cant_talla6;
                        cantidad[6] = objDe.picbd_cant_talla7;
                        cantidad[7] = objDe.picbd_cant_talla8;
                        cantidad[8] = objDe.picbd_cant_talla9;
                        cantidad[9] = objDe.picbd_cant_talla10;

                        int IndiceModelo = objDe.pr_vdescripcion_producto.Trim().IndexOf(objDe.pr_vdescripcion_modelo.Trim());
                        int IndiceColor = objDe.pr_vdescripcion_producto.Trim().IndexOf(objDe.pr_vdescripcion_color.Trim());
                        string NuevoTexto1 = objDe.pr_vdescripcion_producto.Trim().Substring(0, IndiceModelo);
                        string NuevoTexto2 = objDe.pr_vdescripcion_producto.Trim().Substring(IndiceModelo + objDe.pr_vdescripcion_modelo.Length, objDe.pr_vdescripcion_producto.Trim().Length - ((IndiceModelo + objDe.pr_vdescripcion_modelo.Trim().Length)));
                        NuevoTexto2 = NuevoTexto2.Substring(0, NuevoTexto2.Trim().Length - objDe.pr_vdescripcion_color.Trim().Length);

                        for (int i = 0; i < 10; i++)
                        {
                            if (cantidad[i] != 0)
                            {
                                List<eimpCod> mlistCodi = new List<eimpCod>();

                                for (int y = 0; y < cantidad[i]; y++)
                                {
                                    eimpCod objImpreCod = new eimpCod();
                                    objImpreCod.CodigoProducto = CodigoExterno + "" + talla[i];
                                    objImpreCod.DescripModelo = objDe.pr_vdescripcion_modelo;
                                    objImpreCod.talla = talla[i].ToString();
                                    objImpreCod.color = objDe.pr_vdescripcion_color;
                                    objImpreCod.FechaImpresion = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                                    objImpreCod.DescripcionProducto = NuevoTexto1 + "" + NuevoTexto2;
                                    objImpreCod.Precio = objDe.PrecioProducto;
                                    mlistCodi.Add(objImpreCod);
                                    this.PrintCentral(ImpresoraPorDefecto(), mlistCodi);
                                    mlistCodi = new List<eimpCod>();

                                }
                            }

                        }

                    }
                }
            }
            else
            {
                int[] talla = new int[10];
                int[] cantidad = new int[10];


                List<EProdCodigoBarraDetalle> mlistDet = new List<EProdCodigoBarraDetalle>();
                EProdCodigoBarra Obee = (EProdCodigoBarra)gvCodigosBarra.GetRow(gvCodigosBarra.FocusedRowHandle);
                if (Obee != null)
                {
                    string CodigoExterno;
                    mlistDet = oblCodiBarr.listarProdCodigoBarraDetalle(Obee.picbc_icod_plan_cod_barr);

                    foreach (EProdCodigoBarraDetalle objDe in mlistDet)
                    {
                        CodigoExterno = objDe.pr_vcodigo_externo.Substring(0, 13);
                        talla[0] = objDe.picbd_talla1;
                        talla[1] = objDe.picbd_talla2;
                        talla[2] = objDe.picbd_talla3;
                        talla[3] = objDe.picbd_talla4;
                        talla[4] = objDe.picbd_talla5;
                        talla[5] = objDe.picbd_talla6;
                        talla[6] = objDe.picbd_talla7;
                        talla[7] = objDe.picbd_talla8;
                        talla[8] = objDe.picbd_talla9;
                        talla[9] = objDe.picbd_talla10;
                        cantidad[0] = objDe.picbd_cant_talla1;
                        cantidad[1] = objDe.picbd_cant_talla2;
                        cantidad[2] = objDe.picbd_cant_talla3;
                        cantidad[3] = objDe.picbd_cant_talla4;
                        cantidad[4] = objDe.picbd_cant_talla5;
                        cantidad[5] = objDe.picbd_cant_talla6;
                        cantidad[6] = objDe.picbd_cant_talla7;
                        cantidad[7] = objDe.picbd_cant_talla8;
                        cantidad[8] = objDe.picbd_cant_talla9;
                        cantidad[9] = objDe.picbd_cant_talla10;

                        int IndiceModelo = objDe.pr_vdescripcion_producto.Trim().IndexOf(objDe.pr_vdescripcion_modelo.Trim());
                        int IndiceColor = objDe.pr_vdescripcion_producto.Trim().IndexOf(objDe.pr_vdescripcion_color.Trim());
                        string NuevoTexto1 = objDe.pr_vdescripcion_producto.Trim().Substring(0, IndiceModelo);
                        string NuevoTexto2 = objDe.pr_vdescripcion_producto.Trim().Substring(IndiceModelo + objDe.pr_vdescripcion_modelo.Length, objDe.pr_vdescripcion_producto.Trim().Length - ((IndiceModelo + objDe.pr_vdescripcion_modelo.Trim().Length)));
                        NuevoTexto2 = NuevoTexto2.Substring(0, NuevoTexto2.Trim().Length - objDe.pr_vdescripcion_color.Trim().Length);

                        for (int i = 0; i < 10; i++)
                        {
                            if (cantidad[i] != 0)
                            {
                                List<eimpCod> mlistCodi = new List<eimpCod>();

                                for (int y = 0; y < cantidad[i]; y++)
                                {
                                    eimpCod objImpreCod = new eimpCod();
                                    objImpreCod.CodigoProducto = CodigoExterno + "" + talla[i];
                                    objImpreCod.DescripModelo = objDe.pr_vdescripcion_modelo;
                                    objImpreCod.talla = talla[i].ToString();
                                    objImpreCod.color = objDe.pr_vdescripcion_color;
                                    objImpreCod.FechaImpresion = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                                    objImpreCod.DescripcionProducto = NuevoTexto1 + "" + NuevoTexto2;
                                    objImpreCod.Precio = objDe.PrecioProducto;
                                    mlistCodi.Add(objImpreCod);
                                    this.PrintSucursal(ImpresoraPorDefecto(), mlistCodi);
                                    mlistCodi = new List<eimpCod>();

                                }
                            }

                        }

                    }
                }
            }

        }
        public void PrintCentral(string printerName, List<eimpCod> mlisBarra)
        {
            StringBuilder sb;

            if (printerName == null)
            {
                throw new ArgumentNullException("printerName");
            }
            sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("N");
            sb.AppendLine("q832");
            sb.AppendLine("Q200,24");
            sb.AppendLine("JB");
            sb.AppendLine("D6");
            sb.AppendLine("S2");
            sb.AppendLine("O");

            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "B55,96,0,1,2,10,70,B,\"{0}\"", mlisBarra[0].CodigoProducto));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A25,12,0,4,1,2,N,\"{0}\"", mlisBarra[0].DescripModelo));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A310,14,0,5,1,1,N,\"{0}\"", mlisBarra[0].talla));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A155,15,0,3,1,1,N,\"{0}\"", mlisBarra[0].color));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A170,40,0,2,1,1,N,\"{0}\"", mlisBarra[0].FechaImpresion));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A25,75,0,3,1,1,N,\"{0}\"", mlisBarra[0].DescripcionProducto));
            sb.AppendLine("GG12,86,DB6");

            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "B485,96,0,1,2,10,70,B,\"{0}\"", mlisBarra[0].CodigoProducto));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A450,12,0,4,1,2,N,\"{0}\"", mlisBarra[0].DescripModelo));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A740,14,0,5,1,1,N,\"{0}\"", mlisBarra[0].talla));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A585,15,0,3,1,1,N,\"{0}\"", mlisBarra[0].color));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A600,40,0,2,1,1,N,\"{0}\"", mlisBarra[0].FechaImpresion));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A455,75,0,3,1,1,N,\"{0}\"", mlisBarra[0].DescripcionProducto));
            sb.AppendLine("GG442,86,DB6");
            sb.AppendLine("P1");
            sb.AppendLine("N");

            RawPrinterHelper.SendStringToPrinter(printerName, sb.ToString());
        }
        public void PrintSucursal(string printerName, List<eimpCod> mlisBarra)
        {
            StringBuilder sb;

            if (printerName == null)
            {
                throw new ArgumentNullException("printerName");
            }
            sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("N");
            sb.AppendLine("q832");
            sb.AppendLine("Q200,24");
            sb.AppendLine("JB");
            sb.AppendLine("D6");
            sb.AppendLine("S2");
            sb.AppendLine("O");

            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "B25,96,0,1,2,10,70,B,\"{0}\"", mlisBarra[0].CodigoProducto));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A290,177,0,1,1,1,N,\"{0}\"", mlisBarra[0].FechaImpresion));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A25,12,0,4,1,2,N,\"{0}\"", mlisBarra[0].talla));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A274,12,0,5,1,1,N,\"{0}\"", mlisBarra[0].Precio));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A238,10,0,3,1,1,N,\"{0}\"", "S/."));
            //sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A301,10,0,1,1,1,N,\"{0}\"", "Precio"));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A100,15,0,3,1,1,N,\"{0}\"", mlisBarra[0].DescripModelo));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A110,40,0,2,1,1,N,\"{0}\"", mlisBarra[0].color));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A25,75,0,3,1,1,N,\"{0}\"", mlisBarra[0].DescripcionProducto));
            sb.AppendLine("GG12,86,DB6");

            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "B455,96,0,1,2,10,70,B,\"{0}\"", mlisBarra[0].CodigoProducto));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A715,177,0,1,1,1,N,\"{0}\"", mlisBarra[0].FechaImpresion));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A450,12,0,4,1,2,N,\"{0}\"", mlisBarra[0].talla));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A704,12,0,5,1,1,N,\"{0}\"", mlisBarra[0].Precio));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A668,10,0,3,1,1,N,\"{0}\"", "S/."));
            //sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A731,10,0,1,1,1,N,\"{0}\"", "Precio"));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A530,15,0,3,1,1,N,\"{0}\"", mlisBarra[0].DescripModelo));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A540,40,0,2,1,1,N,\"{0}\"", mlisBarra[0].color));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A455,75,0,3,1,1,N,\"{0}\"", mlisBarra[0].DescripcionProducto));
            sb.AppendLine("GG442,86,DB6");
            sb.AppendLine("P1");
            sb.AppendLine("N");

            RawPrinterHelper.SendStringToPrinter(printerName, sb.ToString());
        }
        private string ImpresoraPorDefecto()
        {
            PrinterSettings settings = new PrinterSettings();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)
                    return printer;
            }

            return string.Empty;
        }
        #region entidad de codigo de barra
        public class eimpCod
        {
            public string CodigoProducto { get; set; }
            public string DescripModelo { get; set; }
            public string talla { get; set; }
            public string color { get; set; }
            public string FechaImpresion { get; set; }
            public string DescripcionProducto { get; set; }
            public decimal Precio { get; set; }
        }
        #endregion

        private void btnagregar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnmodificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Datos();
        }

        private void btnsalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void btnimprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }
    }
}