using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
using static SGE.Common.Codigos;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteGuiaRemisionDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<EGuiaRemisionDet> lstDetalle = new List<EGuiaRemisionDet>();
        public EGuiaRemisionDet oBe = new EGuiaRemisionDet();
        public EGuiaRemision oBeCab = new EGuiaRemision();
        public int IcodAlmacen = 0;
        public int intmoneda = 0;
        public string DesProd = "";
        public string DesProd2 = "";
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public string descripcion { get; set; }
        public List<EProdProducto> oProducto;
        public int icod_almacen;

        public frmManteGuiaRemisionDetalle()
        {
            InitializeComponent();
        }

        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                //btncodigoProducto.Enabled = Enabled;
                //txtCodigo.Enabled = Enabled;
                //lkpSerie.Enabled = Enabled;
                //txtdescripcion.Enabled = Enabled;
                //btnGenerar.Enabled = Enabled;
                //txtrangotallas.Enabled = Enabled;
                //txtCantidad.Enabled = Enabled;
            }
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            setValues();
        }

        private void setValues()
        {

            btncodigoProducto.Text = oBe.dremc_vcodigo_extremo_prod;
            txtModelo.Text = oBe.dremc_icodigo.ToString();
            txtCantidad.Text = oBe.dremc_ncantidad_producto.ToString();
            descripcion = oBe.dremc_vdescripcion_prod;
            txtdescripcion.Text = oBe.dremc_vdescripcion_prod;
            lkpSerie.EditValue = oBe.dremc_iserie;
            txtrangotallas.Text = oBe.dremc_rango_talla;

            txttalla1.Text = string.Format("{0:00}", oBe.dremc_talla1);
            txttalla2.Text = string.Format("{0:00}", oBe.dremc_talla2);
            txttalla3.Text = string.Format("{0:00}", oBe.dremc_talla3);
            txttalla4.Text = string.Format("{0:00}", oBe.dremc_talla4);
            txttalla5.Text = string.Format("{0:00}", oBe.dremc_talla5);
            txttalla6.Text = string.Format("{0:00}", oBe.dremc_talla6);
            txttalla7.Text = string.Format("{0:00}", oBe.dremc_talla7);
            txttalla8.Text = string.Format("{0:00}", oBe.dremc_talla8);
            txttalla9.Text = string.Format("{0:00}", oBe.dremc_talla9);
            txttalla10.Text = string.Format("{0:00}", oBe.dremc_talla10);
            txtcantidad1.Text = oBe.dremc_cant_talla1.ToString();
            txtcantidad2.Text = oBe.dremc_cant_talla2.ToString();
            txtcantidad3.Text = oBe.dremc_cant_talla3.ToString();
            txtcantidad4.Text = oBe.dremc_cant_talla4.ToString();
            txtcantidad5.Text = oBe.dremc_cant_talla5.ToString();
            txtcantidad6.Text = oBe.dremc_cant_talla6.ToString();
            txtcantidad7.Text = oBe.dremc_cant_talla7.ToString();
            txtcantidad8.Text = oBe.dremc_cant_talla8.ToString();
            txtcantidad9.Text = oBe.dremc_cant_talla9.ToString();
            txtcantidad10.Text = oBe.dremc_cant_talla10.ToString();
            icodProducto[0] = Convert.ToInt32(oBe.dremc_icod_producto1);
            icodProducto[1] = Convert.ToInt32(oBe.dremc_icod_producto2);
            icodProducto[2] = Convert.ToInt32(oBe.dremc_icod_producto3);
            icodProducto[3] = Convert.ToInt32(oBe.dremc_icod_producto4);
            icodProducto[4] = Convert.ToInt32(oBe.dremc_icod_producto5);
            icodProducto[5] = Convert.ToInt32(oBe.dremc_icod_producto6);
            icodProducto[6] = Convert.ToInt32(oBe.dremc_icod_producto7);
            icodProducto[7] = Convert.ToInt32(oBe.dremc_icod_producto8);
            icodProducto[8] = Convert.ToInt32(oBe.dremc_icod_producto9);
            icodProducto[9] = Convert.ToInt32(oBe.dremc_icod_producto10);


        }
        public void Redimencionarstock()
        {
            List<EProdProducto> ostockproducto = new List<EProdProducto>();
            TextEdit[] textoCantidad = GetTextoCantidad();
            TextEdit[] textoTalla = GetTextoTalla();
            //TextEdit[] textostock = GetTextoCantidadStock();


            for (int x = 0; x <= 9; x++)
            {

                string codigoexterno = btncodigoProducto.Text + textoTalla[x].Text;
                oProducto = new BCentral().VerificarProductoPvt(codigoexterno);
                if (oProducto.Count > 0)
                {
                    ostockproducto = new BCentral().VerificarProdStockProducto(oProducto[0].pr_icod_producto, Convert.ToInt32(icod_almacen), Parametros.intEjercicio);
                }

                if (textoTalla[x].ToString() != "00")
                    textoCantidad[x].Enabled = true;
                else
                    textoCantidad[x].Enabled = false;

                oProducto.Clear();
                ostockproducto.Clear();
            }
            btncodigoProducto.Enabled = false;
            //lkpSerie.Enabled = false;

        }
        private void setSave()
        {
            BaseEdit oBase = null;
            try
            {
                if ((txtModelo.Text) == "")
                {
                    oBase = txtModelo;
                    throw new ArgumentException("Ingrese Codigo");
                }

                if (Convert.ToDecimal(txtCantidad.Text) <= 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("La cantidad debe ser mayor a 0");
                }

                TextEdit[] textoCantidad = GetTextoCantidad();
                TextEdit[] textoTalla = GetTextoTalla();
                oBe.dremc_itipo_producto = (int)TipoProducto.Mercadería;
                oBe.dremc_icodigo = txtModelo.Text;
                oBe.dremc_inro_item = Convert.ToInt16(txtItem.Text);
                oBe.dremc_ncantidad_producto = Convert.ToDecimal(txtCantidad.Text);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.dremc_vcodigo_extremo_prod = btncodigoProducto.Text;
                oBe.dremc_vdescripcion_prod = txtdescripcion.Text;
                oBe.dremc_iserie = Convert.ToInt32(lkpSerie.EditValue);
                oBe.resec_vdescripcion = lkpSerie.Text;
                oBe.dremc_rango_talla = txtrangotallas.Text;
                oBe.dremc_talla1 = Convert.ToInt32((textoTalla[0].Text == "") ? null : textoTalla[0].Text);
                oBe.dremc_talla2 = Convert.ToInt32((textoTalla[1].Text == "") ? null : textoTalla[1].Text);
                oBe.dremc_talla3 = Convert.ToInt32((textoTalla[2].Text == "") ? null : textoTalla[2].Text);
                oBe.dremc_talla4 = Convert.ToInt32((textoTalla[3].Text == "") ? null : textoTalla[3].Text);
                oBe.dremc_talla5 = Convert.ToInt32((textoTalla[4].Text == "") ? null : textoTalla[4].Text);
                oBe.dremc_talla6 = Convert.ToInt32((textoTalla[5].Text == "") ? null : textoTalla[5].Text);
                oBe.dremc_talla7 = Convert.ToInt32((textoTalla[6].Text == "") ? null : textoTalla[6].Text);
                oBe.dremc_talla8 = Convert.ToInt32((textoTalla[7].Text == "") ? null : textoTalla[7].Text);
                oBe.dremc_talla9 = Convert.ToInt32((textoTalla[8].Text == "") ? null : textoTalla[8].Text);
                oBe.dremc_talla10 = Convert.ToInt32((textoTalla[9].Text == "") ? null : textoTalla[9].Text);
                oBe.dremc_cant_talla1 = Convert.ToInt32((textoCantidad[0].Text == "") ? null : textoCantidad[0].Text);
                oBe.dremc_cant_talla2 = Convert.ToInt32((textoCantidad[1].Text == "") ? null : textoCantidad[1].Text);
                oBe.dremc_cant_talla3 = Convert.ToInt32((textoCantidad[2].Text == "") ? null : textoCantidad[2].Text);
                oBe.dremc_cant_talla4 = Convert.ToInt32((textoCantidad[3].Text == "") ? null : textoCantidad[3].Text);
                oBe.dremc_cant_talla5 = Convert.ToInt32((textoCantidad[4].Text == "") ? null : textoCantidad[4].Text);
                oBe.dremc_cant_talla6 = Convert.ToInt32((textoCantidad[5].Text == "") ? null : textoCantidad[5].Text);
                oBe.dremc_cant_talla7 = Convert.ToInt32((textoCantidad[6].Text == "") ? null : textoCantidad[6].Text);
                oBe.dremc_cant_talla8 = Convert.ToInt32((textoCantidad[7].Text == "") ? null : textoCantidad[7].Text);
                oBe.dremc_cant_talla9 = Convert.ToInt32((textoCantidad[8].Text == "") ? null : textoCantidad[8].Text);
                oBe.dremc_cant_talla10 = Convert.ToInt32((textoCantidad[9].Text == "") ? null : textoCantidad[9].Text);
                oBe.dremc_icod_producto1 = icodProducto[0];
                oBe.dremc_icod_producto2 = icodProducto[1];
                oBe.dremc_icod_producto3 = icodProducto[2];
                oBe.dremc_icod_producto4 = icodProducto[3];
                oBe.dremc_icod_producto5 = icodProducto[4];
                oBe.dremc_icod_producto6 = icodProducto[5];
                oBe.dremc_icod_producto7 = icodProducto[6];
                oBe.dremc_icod_producto8 = icodProducto[7];
                oBe.dremc_icod_producto9 = icodProducto[8];
                oBe.dremc_icod_producto10 = icodProducto[9];
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.intTipoOperacion = 1;
                    lstDetalle.Add(oBe);
                }
                else
                {
                    if (oBe.intTipoOperacion != 1)
                        oBe.intTipoOperacion = 2;
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public decimal dblStockDisponible = 0;
        private void listarProducto()
        {
            BaseEdit oBase = null;
            try
            {

                using (ListarStockPorAlmacenGR frm = new ListarStockPorAlmacenGR())
                {
                    frm.intAlmacen = Convert.ToInt32(oBeCab.almac_icod_almacen);
                    //frm.intAlmacen = IcodAlmacen;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        oBe.strCategoria = frm._Be.strCategoria;
                        oBe.strSubCategoriaUno = frm._Be.strSubCategoriaUno;
                        oBe.dblStockDisponible = frm._Be.stocc_stock_producto;
                        oBe.strDesProducto = frm._Be.strDesProducto;
                        DesProd2 = frm._Be.strCodProducto;
                    }
                }

            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void bteProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProducto();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void bteProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarProducto();
        }




        private void frmManteGuiaRemisionDetalle_Load(object sender, EventArgs e)
        {
            EProdTablaRegistro ob = new EProdTablaRegistro();
            BSControls.LoaderLook(lkpSerie, new BCentral().ListarRegistroSerie(), "resec_vdescripcion", "resec_iid_registro_serie", true);
            txtCantidad.Enabled = false;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpSerie.EditValue = oBe.dremc_iserie;
                txtdescripcion.Text = oBe.dremc_vdescripcion_prod;
            }
        }


        public int[] icodProducto = new int[10]; //---tener los icod de todos los productos
        private void btnGenerar_Click(object sender, EventArgs e)
        {

            List<ERegistroSerie> lstSerie = new BCentral().ListarRegistroSerie().Where(x => x.resec_iid_registro_serie == Convert.ToInt32(lkpSerie.EditValue)).ToList();
            if (lstSerie[0].resec_vtalla_inicial != "0" && lstSerie[0].resec_vtalla_final != "0")
            {
                List<EProdProducto> ostockproducto = new List<EProdProducto>();


                if ((btncodigoProducto.Tag) == null)
                {
                    XtraMessageBox.Show("Ingrese el Producto", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    //BorrarCantidades();
                    //BorrarTalla();
                    int i = -1;
                    TextEdit[] textoCantidad = GetTextoCantidad();
                    TextEdit[] textoTalla = GetTextoTalla();
                    //TextEdit[] textoStock = GetTextoCantidadStock();
                    for (int x = Convert.ToInt32(lstSerie[0].resec_vtalla_inicial); x <= Convert.ToInt32(lstSerie[0].resec_vtalla_final); x++)
                    {
                        i++;
                        textoTalla[i].Text = string.Format("{0:00}", x).ToString();
                        textoCantidad[i].Text = "0";

                        string codigoexterno = btncodigoProducto.Text + textoTalla[i].Text;
                        oProducto = new BCentral().VerificarProductoPvt(codigoexterno);
                        if (oProducto.Count > 0)
                        {
                            ostockproducto = new BCentral().VerificarProdStockProducto(oProducto[0].pr_icod_producto, Convert.ToInt32(icod_almacen), Parametros.intEjercicio);
                        }

                        if (oProducto.Count > 0)
                        {
                            if (ostockproducto.Count > 0)
                            {
                                //textoStock[i].Text = Convert.ToInt32(ostockproducto[0].stocc_nstock_prod).ToString();
                            }
                            icodProducto[i] = oProducto[0].pr_icod_producto;
                        }
                        textoCantidad[i].Enabled = true;
                    }
                    lkpSerie.Enabled = false;
                    btncodigoProducto.Enabled = false;
                }
            }
            else
            {
                XtraMessageBox.Show("Ingrese Serie", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


        }
        private TextEdit[] GetTextoCantidad()
        {
            TextEdit[] texto = new TextEdit[] { txtcantidad1, txtcantidad2, txtcantidad3, txtcantidad4,
                            txtcantidad5, txtcantidad6, txtcantidad7, txtcantidad8, txtcantidad9, txtcantidad10};
            return texto;
        }


        private TextEdit[] GetTextoTalla()
        {
            TextEdit[] texto = new TextEdit[] { txttalla1, txttalla2, txttalla3, txttalla4,
                            txttalla5, txttalla6, txttalla7, txttalla8, txttalla9, txttalla10};
            return texto;
        }
        private void BorrarTalla()
        {
            TextEdit[] texto = GetTextoTalla();
            for (int x = 0; x < texto.Length; x++)
            {
                texto[x].Text = "";
                texto[x].Tag = 0;
            }

        }
        private void BorrarCantidades()
        {
            TextEdit[] texto = GetTextoCantidad();
            for (int x = 0; x < texto.Length; x++)
            {
                texto[x].Text = "";
                texto[x].Enabled = false;
            }
        }
        private void Totalizar()
        {
            int Suma = Convert.ToInt32(txtcantidad1.Text = (txtcantidad1.Text == "") ? "0" : txtcantidad1.Text) +
            Convert.ToInt32(txtcantidad2.Text = (txtcantidad2.Text == "") ? "0" : txtcantidad2.Text) +
            Convert.ToInt32(txtcantidad3.Text = (txtcantidad3.Text == "") ? "0" : txtcantidad3.Text) +
            Convert.ToInt32(txtcantidad4.Text = (txtcantidad4.Text == "") ? "0" : txtcantidad4.Text) +
            Convert.ToInt32(txtcantidad5.Text = (txtcantidad5.Text == "") ? "0" : txtcantidad5.Text) +
            Convert.ToInt32(txtcantidad6.Text = (txtcantidad6.Text == "") ? "0" : txtcantidad6.Text) +
            Convert.ToInt32(txtcantidad7.Text = (txtcantidad7.Text == "") ? "0" : txtcantidad7.Text) +
            Convert.ToInt32(txtcantidad8.Text = (txtcantidad8.Text == "") ? "0" : txtcantidad8.Text) +
            Convert.ToInt32(txtcantidad9.Text = (txtcantidad9.Text == "") ? "0" : txtcantidad9.Text) +
            Convert.ToInt32(txtcantidad10.Text = (txtcantidad10.Text == "") ? "0" : txtcantidad10.Text);
            txtCantidad.Text = Suma.ToString();
        }

        private void txtcantidad1_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }

        private void btncodigoProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (FrmListarProducto Producto = new FrmListarProducto())
            {
                if (Producto.ShowDialog() == DialogResult.OK)
                {
                    btncodigoProducto.Tag = Producto._Be.pr_icod_producto;
                    btncodigoProducto.Text = Producto._Be.pr_vcodigo_externo.Substring(0, Producto._Be.pr_vcodigo_externo.Length - 2);
                    txtdescripcion.Text = Producto._Be.pr_vdescripcion_producto.Substring(0, Producto._Be.pr_vdescripcion_producto.Length - 2) + lkpSerie.Text;
                    descripcion = Producto._Be.pr_vdescripcion_producto.Substring(0, Producto._Be.pr_vdescripcion_producto.Length - 2);
                    oBe.CodigoSUNAT = Producto._Be.CodigoSUNAT;
                    txtModelo.Text = Producto._Be.pr_viid_modelo;
                    oBe.unidc_icod_unidad_medida = Convert.ToInt32(Producto._Be.unidc_icod_unidad_medida);
                    oBe.strAbreUM = Producto._Be.abreviaUnidadMedi;
                    oBe.strDesUM = Producto._Be.descripUnidadMedi;
                }
            }
        }

        private void lkpSerie_EditValueChanged(object sender, EventArgs e)
        {

            if (Status == BSMaintenanceStatus.CreateNew)
            {

                List<ERegistroSerie> lstSerie = new BCentral().ListarRegistroSerie().Where(x => x.resec_iid_registro_serie == Convert.ToInt32(lkpSerie.EditValue)).ToList();
                txtrangotallas.Text = lstSerie[0].resec_vtalla_inicial + "/" + lstSerie[0].resec_vtalla_final;
                if (txtdescripcion.Text != "")
                {

                    txtdescripcion.Text = descripcion + lkpSerie.Text;
                }
            }

        }
    }
}