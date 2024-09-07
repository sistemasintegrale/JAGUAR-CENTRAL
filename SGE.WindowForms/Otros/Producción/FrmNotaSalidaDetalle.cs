using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class FrmNotaSalidaDetalle : DevExpress.XtraEditors.XtraForm
    {
        public int salgcd_icod_nota_salida;//referencia
        public int icod_almacen;
        public int icod_punto_venta;
        public int icod_motivo;
        public int indicador;
        public List<EProdProducto> oProducto;
        public List<EProdProducto> ostockproducto;
        public int lista;
        public EProdNotaSalidaDetalle _BE = new EProdNotaSalidaDetalle();
        private BSMaintenanceStatus mStatus;
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }
        public FrmNotaSalidaDetalle()
        {
            InitializeComponent();
        }
        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                //bteAlmacen.Enabled = Enabled;
                //bteProducto.Enabled = Enabled;
                //lkpTipoVenta.Enabled = Enabled;
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

        }
        private void btncodigoProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (FrmListarProductosDeStock frm = new FrmListarProductosDeStock())
            {
                frm.stocc_iid_almacen = Convert.ToInt32(icod_almacen);
                frm.puvec_icod_punto_venta = Convert.ToInt32(icod_punto_venta);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btncodigoProducto.Tag = frm._Be.pr_icod_producto;
                    btncodigoProducto.Text = frm._Be.pr_vcodigo_externo.Substring(0, frm._Be.pr_vcodigo_externo.Length - 2);
                    txtdescripcion.Text = frm._Be.pr_vdescripcion_producto;
                }
            }
        }
        public int[] icodProducto = new int[10];

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
                    TextEdit[] textoStock = GetTextoCantidadStock();
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
                            textoCantidad[i].Enabled = true;
                            if (ostockproducto.Count > 0)
                            {
                                textoStock[i].Text = Convert.ToInt32(ostockproducto[0].stocc_nstock_prod).ToString();
                            }
                            icodProducto[i] = oProducto[0].pr_icod_producto;
                        }
                        else
                            textoCantidad[i].Enabled = false;

                    }
                    btncodigoProducto.Enabled = false;
                    lkpSerie.Enabled = false;
                }
            }
            else
            {
                XtraMessageBox.Show("Ingrese Serie", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void BorrarCantidades()
        {
            TextEdit[] texto = GetTextoCantidad();
            for (int x = 0; x < texto.Length; x++)
            {
                texto[x].Text = "0";
                texto[x].Tag = 0;
                texto[x].Enabled = false;
            }
        }
        private void BorrarTalla()
        {
            TextEdit[] texto = GetTextoTalla();
            for (int x = 0; x < texto.Length; x++)
            {
                texto[x].Text = "0";
                texto[x].Tag = 0;
            }

        }
        private TextEdit[] GetTextoCantidad()
        {
            TextEdit[] texto = new TextEdit[] { txtcantidad1, txtcantidad2, txtcantidad3, txtcantidad4,
                            txtcantidad5, txtcantidad6, txtcantidad7, txtcantidad8, txtcantidad9, txtcantidad10};
            return texto;
        }
        private TextEdit[] GetTextoCantidadStock()
        {
            TextEdit[] texto = new TextEdit[] { txtStock1, txtStock2, txtStock3 , txtStock4,
                            txtStock5, txtStock6, txtStock7, txtStock8, txtStock9, txtStock10};
            return texto;
        }

        private TextEdit[] GetTextoTalla()
        {
            TextEdit[] texto = new TextEdit[] { txttalla1, txttalla2, txttalla3, txttalla4,
                            txttalla5, txttalla6, txttalla7, txttalla8, txttalla9, txttalla10};
            return texto;
        }
        private void FrmNotaSalidaDetalle_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpSerie, new BCentral().ListarRegistroSerie(), "resec_vdescripcion", "resec_iid_registro_serie", true);
            List<ERegistroSerie> lstSerie = new BCentral().ListarRegistroSerie().Where(x => x.resec_iid_registro_serie == Convert.ToInt32(lkpSerie.EditValue)).ToList();
            txtrangotallas.Text = lstSerie[0].resec_vtalla_inicial + "/" + lstSerie[0].resec_vtalla_final;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpSerie.EditValue = _BE.salgcd_icod_serie;
                txtrangotallas.Text = _BE.salgcd_rango_talla;
                txtitem.Text = Convert.ToString(_BE.salgcd_num_item);
                lkpSerie.Enabled = false;
            }
        }
        public void Redimencionarstock()
        {
            List<EProdProducto> ostockproducto = new List<EProdProducto>();
            TextEdit[] textoCantidad = GetTextoCantidad();
            TextEdit[] textoTalla = GetTextoTalla();
            TextEdit[] textostock = GetTextoCantidadStock();


            if (salgcd_icod_nota_salida != 0)
            {
                for (int x = 0; x <= 9; x++)
                {

                    string codigoexterno = btncodigoProducto.Text + textoTalla[x].Text;
                    oProducto = new BCentral().VerificarProductoPvt(codigoexterno);
                    if (oProducto.Count > 0)
                    {
                        ostockproducto = new BCentral().VerificarProdStockProducto(oProducto[0].pr_icod_producto, Convert.ToInt32(icod_almacen), Parametros.intEjercicio);
                    }
                    if (ostockproducto.Count > 0)
                        textostock[x].Text = Convert.ToInt32(ostockproducto[0].stocc_nstock_prod).ToString();
                    else
                        textostock[x].Text = "0";

                    if (Convert.ToInt32(textostock[x].Text) > 0)
                        textoCantidad[x].Enabled = true;
                    else
                        textoCantidad[x].Enabled = false;

                    oProducto.Clear();
                    ostockproducto.Clear();
                }
                btncodigoProducto.Enabled = false;
                lkpSerie.Enabled = false;
            }
            else
            {
                for (int x = 0; x <= 9; x++)
                {
                    string codigoexterno = btncodigoProducto.Text + textoTalla[x].Text;
                    oProducto = new BCentral().VerificarProductoPvt(codigoexterno);
                    if (oProducto.Count > 0)
                    {
                        ostockproducto = new BCentral().VerificarProdStockProducto(oProducto[0].pr_icod_producto, Convert.ToInt32(icod_almacen), Parametros.intEjercicio);
                    }
                    if (ostockproducto.Count > 0)
                        textostock[x].Text = (Convert.ToInt32(ostockproducto[0].stocc_nstock_prod) - Convert.ToInt32(textoCantidad[x].Text)).ToString();
                    else
                        textostock[x].Text = "0";

                    if (Convert.ToInt32(textostock[x].Text) > 0)
                        textoCantidad[x].Enabled = true;
                    else
                        textoCantidad[x].Enabled = false;


                    oProducto.Clear();
                    ostockproducto.Clear();
                }
                btncodigoProducto.Enabled = false;
                lkpSerie.Enabled = false;
            }
        }
        private void btnAgregar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AgregarDetalle();
        }
        private void AgregarDetalle()
        {
            bool flag = true;
            BaseEdit oBase = null;
            try
            {
                if ((btncodigoProducto.Tag) == null)
                {
                    oBase = btncodigoProducto;
                    XtraMessageBox.Show("Ingrese el Producto", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                TextEdit[] textoCantidad = GetTextoCantidad();
                TextEdit[] textoTalla = GetTextoTalla();

                _BE.salgc_iid_almacen = Convert.ToInt32(icod_almacen);
                _BE.salgcd_num_item = Convert.ToInt32(txtitem.Text);
                _BE.tablc_iid_motivo = Convert.ToInt32(icod_motivo);
                _BE.pr_icod_producto = Convert.ToInt32(btncodigoProducto.Tag);
                _BE.pr_vcodigo_externo = btncodigoProducto.Text;
                _BE.pr_vdescripcion_producto = txtdescripcion.Text;
                _BE.salgcd_icod_serie = Convert.ToInt32(lkpSerie.EditValue);
                _BE.salgcd_rango_talla = txtrangotallas.Text;
                _BE.resec_vdescripcion = lkpSerie.Text;
                _BE.salgcd_iid_moneda = 1;
                _BE.pr_ncant_total_producto = Convert.ToDecimal(txtcantidaddetalle.Text);
                _BE.salgcd_monto_unit = 0;
                _BE.salgcd_monto_total = 0;
                _BE.salgcd_talla1 = Convert.ToInt32((textoTalla[0].Text == "") ? null : textoTalla[0].Text);
                _BE.salgcd_talla2 = Convert.ToInt32((textoTalla[1].Text == "") ? null : textoTalla[1].Text);
                _BE.salgcd_talla3 = Convert.ToInt32((textoTalla[2].Text == "") ? null : textoTalla[2].Text);
                _BE.salgcd_talla4 = Convert.ToInt32((textoTalla[3].Text == "") ? null : textoTalla[3].Text);
                _BE.salgcd_talla5 = Convert.ToInt32((textoTalla[4].Text == "") ? null : textoTalla[4].Text);
                _BE.salgcd_talla6 = Convert.ToInt32((textoTalla[5].Text == "") ? null : textoTalla[5].Text);
                _BE.salgcd_talla7 = Convert.ToInt32((textoTalla[6].Text == "") ? null : textoTalla[6].Text);
                _BE.salgcd_talla8 = Convert.ToInt32((textoTalla[7].Text == "") ? null : textoTalla[7].Text);
                _BE.salgcd_talla9 = Convert.ToInt32((textoTalla[8].Text == "") ? null : textoTalla[8].Text);
                _BE.salgcd_talla10 = Convert.ToInt32((textoTalla[9].Text == "") ? null : textoTalla[9].Text);
                _BE.salgcd_cant_talla1 = Convert.ToInt32((textoCantidad[0].Text == "") ? null : textoCantidad[0].Text);
                _BE.salgcd_cant_talla2 = Convert.ToInt32((textoCantidad[1].Text == "") ? null : textoCantidad[1].Text);
                _BE.salgcd_cant_talla3 = Convert.ToInt32((textoCantidad[2].Text == "") ? null : textoCantidad[2].Text);
                _BE.salgcd_cant_talla4 = Convert.ToInt32((textoCantidad[3].Text == "") ? null : textoCantidad[3].Text);
                _BE.salgcd_cant_talla5 = Convert.ToInt32((textoCantidad[4].Text == "") ? null : textoCantidad[4].Text);
                _BE.salgcd_cant_talla6 = Convert.ToInt32((textoCantidad[5].Text == "") ? null : textoCantidad[5].Text);
                _BE.salgcd_cant_talla7 = Convert.ToInt32((textoCantidad[6].Text == "") ? null : textoCantidad[6].Text);
                _BE.salgcd_cant_talla8 = Convert.ToInt32((textoCantidad[7].Text == "") ? null : textoCantidad[7].Text);
                _BE.salgcd_cant_talla9 = Convert.ToInt32((textoCantidad[8].Text == "") ? null : textoCantidad[8].Text);
                _BE.salgcd_cant_talla10 = Convert.ToInt32((textoCantidad[9].Text == "") ? null : textoCantidad[9].Text);

                _BE.salgcd_icod_producto1 = icodProducto[0];
                _BE.salgcd_icod_producto2 = icodProducto[1];
                _BE.salgcd_icod_producto3 = icodProducto[2];
                _BE.salgcd_icod_producto4 = icodProducto[3];
                _BE.salgcd_icod_producto5 = icodProducto[4];
                _BE.salgcd_icod_producto6 = icodProducto[5];
                _BE.salgcd_icod_producto7 = icodProducto[6];
                _BE.salgcd_icod_producto8 = icodProducto[7];
                _BE.salgcd_icod_producto9 = icodProducto[8];
                _BE.salgcd_icod_producto10 = icodProducto[9];


                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finally
            {

            }
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Modificar();
        }
        private void Modificar()
        {
            BaseEdit oBase = null;
            try
            {
                TextEdit[] textoCantidad = GetTextoCantidad();
                TextEdit[] textoTalla = GetTextoTalla();

                _BE.pr_ncant_total_producto = Convert.ToDecimal(txtcantidaddetalle.Text);
                _BE.salgcd_monto_unit = 0;
                _BE.salgcd_monto_total = 0;
                _BE.salgcd_talla1 = Convert.ToInt32((textoTalla[0].Text == "") ? null : textoTalla[0].Text);
                _BE.salgcd_talla2 = Convert.ToInt32((textoTalla[1].Text == "") ? null : textoTalla[1].Text);
                _BE.salgcd_talla3 = Convert.ToInt32((textoTalla[2].Text == "") ? null : textoTalla[2].Text);
                _BE.salgcd_talla4 = Convert.ToInt32((textoTalla[3].Text == "") ? null : textoTalla[3].Text);
                _BE.salgcd_talla5 = Convert.ToInt32((textoTalla[4].Text == "") ? null : textoTalla[4].Text);
                _BE.salgcd_talla6 = Convert.ToInt32((textoTalla[5].Text == "") ? null : textoTalla[5].Text);
                _BE.salgcd_talla7 = Convert.ToInt32((textoTalla[6].Text == "") ? null : textoTalla[6].Text);
                _BE.salgcd_talla8 = Convert.ToInt32((textoTalla[7].Text == "") ? null : textoTalla[7].Text);
                _BE.salgcd_talla9 = Convert.ToInt32((textoTalla[8].Text == "") ? null : textoTalla[8].Text);
                _BE.salgcd_talla10 = Convert.ToInt32((textoTalla[9].Text == "") ? null : textoTalla[9].Text);
                _BE.salgcd_cant_talla1 = Convert.ToInt32((textoCantidad[0].Text == "") ? null : textoCantidad[0].Text);
                _BE.salgcd_cant_talla2 = Convert.ToInt32((textoCantidad[1].Text == "") ? null : textoCantidad[1].Text);
                _BE.salgcd_cant_talla3 = Convert.ToInt32((textoCantidad[2].Text == "") ? null : textoCantidad[2].Text);
                _BE.salgcd_cant_talla4 = Convert.ToInt32((textoCantidad[3].Text == "") ? null : textoCantidad[3].Text);
                _BE.salgcd_cant_talla5 = Convert.ToInt32((textoCantidad[4].Text == "") ? null : textoCantidad[4].Text);
                _BE.salgcd_cant_talla6 = Convert.ToInt32((textoCantidad[5].Text == "") ? null : textoCantidad[5].Text);
                _BE.salgcd_cant_talla7 = Convert.ToInt32((textoCantidad[6].Text == "") ? null : textoCantidad[6].Text);
                _BE.salgcd_cant_talla8 = Convert.ToInt32((textoCantidad[7].Text == "") ? null : textoCantidad[7].Text);
                _BE.salgcd_cant_talla9 = Convert.ToInt32((textoCantidad[8].Text == "") ? null : textoCantidad[8].Text);
                _BE.salgcd_cant_talla10 = Convert.ToInt32((textoCantidad[9].Text == "") ? null : textoCantidad[9].Text);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void btnsalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void txtcantidad1_EditValueChanged(object sender, EventArgs e)
        {
            if (indicador == 0)
            {
                txtStock1.Text = (txtcantidad1.Text == "0") ? (cantotales[0]).ToString() : (cantotales[0] - Convert.ToInt32(txtcantidad1.Text)).ToString();
                txtStock2.Text = (txtcantidad2.Text == "0") ? (cantotales[1]).ToString() : (cantotales[1] - Convert.ToInt32(txtcantidad2.Text)).ToString();
                txtStock3.Text = (txtcantidad3.Text == "0") ? (cantotales[2]).ToString() : (cantotales[2] - Convert.ToInt32(txtcantidad3.Text)).ToString();
                txtStock4.Text = (txtcantidad4.Text == "0") ? (cantotales[3]).ToString() : (cantotales[3] - Convert.ToInt32(txtcantidad4.Text)).ToString();
                txtStock5.Text = (txtcantidad5.Text == "0") ? (cantotales[4]).ToString() : (cantotales[4] - Convert.ToInt32(txtcantidad5.Text)).ToString();
                txtStock6.Text = (txtcantidad6.Text == "0") ? (cantotales[5]).ToString() : (cantotales[5] - Convert.ToInt32(txtcantidad6.Text)).ToString();
                txtStock7.Text = (txtcantidad7.Text == "0") ? (cantotales[6]).ToString() : (cantotales[6] - Convert.ToInt32(txtcantidad7.Text)).ToString();
                txtStock8.Text = (txtcantidad8.Text == "0") ? (cantotales[7]).ToString() : (cantotales[7] - Convert.ToInt32(txtcantidad8.Text)).ToString();
                txtStock9.Text = (txtcantidad9.Text == "0") ? (cantotales[8]).ToString() : (cantotales[8] - Convert.ToInt32(txtcantidad9.Text)).ToString();
                txtStock10.Text = (txtcantidad10.Text == "0") ? (cantotales[9]).ToString() : (cantotales[9] - Convert.ToInt32(txtcantidad10.Text)).ToString();

                int Suma = 0;
                Suma = Convert.ToInt32(txtcantidad1.Text = (txtcantidad1.Text == "") ? "0" : txtcantidad1.Text) +
                Convert.ToInt32(txtcantidad2.Text = (txtcantidad2.Text == "") ? "0" : txtcantidad2.Text) +
                Convert.ToInt32(txtcantidad3.Text = (txtcantidad3.Text == "") ? "0" : txtcantidad3.Text) +
                Convert.ToInt32(txtcantidad4.Text = (txtcantidad4.Text == "") ? "0" : txtcantidad4.Text) +
                Convert.ToInt32(txtcantidad5.Text = (txtcantidad5.Text == "") ? "0" : txtcantidad5.Text) +
                Convert.ToInt32(txtcantidad6.Text = (txtcantidad6.Text == "") ? "0" : txtcantidad6.Text) +
                Convert.ToInt32(txtcantidad7.Text = (txtcantidad7.Text == "") ? "0" : txtcantidad7.Text) +
                Convert.ToInt32(txtcantidad8.Text = (txtcantidad8.Text == "") ? "0" : txtcantidad8.Text) +
                Convert.ToInt32(txtcantidad9.Text = (txtcantidad9.Text == "") ? "0" : txtcantidad9.Text) +
                Convert.ToInt32(txtcantidad10.Text = (txtcantidad10.Text == "") ? "0" : txtcantidad10.Text);
                txtcantidaddetalle.Text = Suma.ToString();
            }
            else if (indicador == 1)
            {
                int Suma = 0;
                Suma = Convert.ToInt32(txtcantidad1.Text = (txtcantidad1.Text == "") ? "0" : txtcantidad1.Text = (Convert.ToInt32(txtcantidad1.Text) > Convert.ToInt32(txtStock1.Text)) ? "0" : txtcantidad1.Text) +
                Convert.ToInt32(txtcantidad2.Text = (txtcantidad2.Text == "") ? "0" : txtcantidad2.Text = (Convert.ToInt32(txtcantidad2.Text) > Convert.ToInt32(txtStock2.Text)) ? "0" : txtcantidad2.Text) +
                Convert.ToInt32(txtcantidad3.Text = (txtcantidad3.Text == "") ? "0" : txtcantidad3.Text = (Convert.ToInt32(txtcantidad3.Text) > Convert.ToInt32(txtStock3.Text)) ? "0" : txtcantidad3.Text) +
                Convert.ToInt32(txtcantidad4.Text = (txtcantidad4.Text == "") ? "0" : txtcantidad4.Text = (Convert.ToInt32(txtcantidad4.Text) > Convert.ToInt32(txtStock4.Text)) ? "0" : txtcantidad4.Text) +
                Convert.ToInt32(txtcantidad5.Text = (txtcantidad5.Text == "") ? "0" : txtcantidad5.Text = (Convert.ToInt32(txtcantidad5.Text) > Convert.ToInt32(txtStock5.Text)) ? "0" : txtcantidad5.Text) +
                Convert.ToInt32(txtcantidad6.Text = (txtcantidad6.Text == "") ? "0" : txtcantidad6.Text = (Convert.ToInt32(txtcantidad6.Text) > Convert.ToInt32(txtStock6.Text)) ? "0" : txtcantidad6.Text) +
                Convert.ToInt32(txtcantidad7.Text = (txtcantidad7.Text == "") ? "0" : txtcantidad7.Text = (Convert.ToInt32(txtcantidad7.Text) > Convert.ToInt32(txtStock7.Text)) ? "0" : txtcantidad7.Text) +
                Convert.ToInt32(txtcantidad8.Text = (txtcantidad8.Text == "") ? "0" : txtcantidad8.Text = (Convert.ToInt32(txtcantidad8.Text) > Convert.ToInt32(txtStock8.Text)) ? "0" : txtcantidad8.Text) +
                Convert.ToInt32(txtcantidad9.Text = (txtcantidad9.Text == "") ? "0" : txtcantidad9.Text = (Convert.ToInt32(txtcantidad9.Text) > Convert.ToInt32(txtStock9.Text)) ? "0" : txtcantidad9.Text) +
                Convert.ToInt32(txtcantidad10.Text = (txtcantidad10.Text == "") ? "0" : txtcantidad10.Text = (Convert.ToInt32(txtcantidad10.Text) > Convert.ToInt32(txtStock10.Text)) ? "0" : txtcantidad10.Text);
                txtcantidaddetalle.Text = Suma.ToString();

            }
            else if (indicador == 3)
            {

                int Suma = 0;
                Suma = Convert.ToInt32(txtcantidad1.Text = (txtcantidad1.Text == "") ? "0" : txtcantidad1.Text) +
                Convert.ToInt32(txtcantidad2.Text = (txtcantidad2.Text == "") ? "0" : txtcantidad2.Text) +
                Convert.ToInt32(txtcantidad3.Text = (txtcantidad3.Text == "") ? "0" : txtcantidad3.Text) +
                Convert.ToInt32(txtcantidad4.Text = (txtcantidad4.Text == "") ? "0" : txtcantidad4.Text) +
                Convert.ToInt32(txtcantidad5.Text = (txtcantidad5.Text == "") ? "0" : txtcantidad5.Text) +
                Convert.ToInt32(txtcantidad6.Text = (txtcantidad6.Text == "") ? "0" : txtcantidad6.Text) +
                Convert.ToInt32(txtcantidad7.Text = (txtcantidad7.Text == "") ? "0" : txtcantidad7.Text) +
                Convert.ToInt32(txtcantidad8.Text = (txtcantidad8.Text == "") ? "0" : txtcantidad8.Text) +
                Convert.ToInt32(txtcantidad9.Text = (txtcantidad9.Text == "") ? "0" : txtcantidad9.Text) +
                Convert.ToInt32(txtcantidad10.Text = (txtcantidad10.Text == "") ? "0" : txtcantidad10.Text);
                txtcantidaddetalle.Text = Suma.ToString();
            }
        }
        int[] cantotales = new int[10];
        public void cargarcantidadesOriginales()
        {

            cantotales[0] = Convert.ToInt32(txtcantidad1.Text) + Convert.ToInt32(txtStock1.Text);
            cantotales[1] = Convert.ToInt32(txtcantidad2.Text) + Convert.ToInt32(txtStock2.Text);
            cantotales[2] = Convert.ToInt32(txtcantidad3.Text) + Convert.ToInt32(txtStock3.Text);
            cantotales[3] = Convert.ToInt32(txtcantidad4.Text) + Convert.ToInt32(txtStock4.Text);
            cantotales[4] = Convert.ToInt32(txtcantidad5.Text) + Convert.ToInt32(txtStock5.Text);
            cantotales[5] = Convert.ToInt32(txtcantidad6.Text) + Convert.ToInt32(txtStock6.Text);
            cantotales[6] = Convert.ToInt32(txtcantidad7.Text) + Convert.ToInt32(txtStock7.Text);
            cantotales[7] = Convert.ToInt32(txtcantidad8.Text) + Convert.ToInt32(txtStock8.Text);
            cantotales[8] = Convert.ToInt32(txtcantidad9.Text) + Convert.ToInt32(txtStock9.Text);
            cantotales[9] = Convert.ToInt32(txtcantidad10.Text) + Convert.ToInt32(txtStock10.Text);
        }

        private void txtcantidad1_Leave(object sender, EventArgs e)
        {
            if (indicador == 0)
            {
                int Suma = 0;
                Suma = Convert.ToInt32(txtcantidad1.Text = (Convert.ToInt32(txtcantidad1.Text) > cantotales[0] || Convert.ToInt32(txtcantidad1.Text) < 0) ? "0" : txtcantidad1.Text) +
                Convert.ToInt32(txtcantidad2.Text = (Convert.ToInt32(txtcantidad2.Text) > cantotales[1] || Convert.ToInt32(txtcantidad2.Text) < 0) ? "0" : txtcantidad2.Text) +
                Convert.ToInt32(txtcantidad3.Text = (Convert.ToInt32(txtcantidad3.Text) > cantotales[2] || Convert.ToInt32(txtcantidad3.Text) < 0) ? "0" : txtcantidad3.Text) +
                Convert.ToInt32(txtcantidad4.Text = (Convert.ToInt32(txtcantidad4.Text) > cantotales[3] || Convert.ToInt32(txtcantidad4.Text) < 0) ? "0" : txtcantidad4.Text) +
                Convert.ToInt32(txtcantidad5.Text = (Convert.ToInt32(txtcantidad5.Text) > cantotales[4] || Convert.ToInt32(txtcantidad5.Text) < 0) ? "0" : txtcantidad5.Text) +
                Convert.ToInt32(txtcantidad6.Text = (Convert.ToInt32(txtcantidad6.Text) > cantotales[5] || Convert.ToInt32(txtcantidad6.Text) < 0) ? "0" : txtcantidad6.Text) +
                Convert.ToInt32(txtcantidad7.Text = (Convert.ToInt32(txtcantidad7.Text) > cantotales[6] || Convert.ToInt32(txtcantidad7.Text) < 0) ? "0" : txtcantidad7.Text) +
                Convert.ToInt32(txtcantidad8.Text = (Convert.ToInt32(txtcantidad8.Text) > cantotales[7] || Convert.ToInt32(txtcantidad8.Text) < 0) ? "0" : txtcantidad8.Text) +
                Convert.ToInt32(txtcantidad9.Text = (Convert.ToInt32(txtcantidad9.Text) > cantotales[8] || Convert.ToInt32(txtcantidad9.Text) < 0) ? "0" : txtcantidad9.Text) +
                Convert.ToInt32(txtcantidad10.Text = (Convert.ToInt32(txtcantidad10.Text) > cantotales[9] || Convert.ToInt32(txtcantidad10.Text) < 0) ? "0" : txtcantidad10.Text);
                txtcantidaddetalle.Text = Suma.ToString();
            }
            else if (indicador == 1)
            {
                int Suma = 0;
                Suma = Convert.ToInt32(txtcantidad1.Text = (txtcantidad1.Text == "") ? "0" : txtcantidad1.Text = (Convert.ToInt32(txtcantidad1.Text) > Convert.ToInt32(txtStock1.Text)) ? "0" : txtcantidad1.Text) +
                Convert.ToInt32(txtcantidad2.Text = (txtcantidad2.Text == "") ? "0" : txtcantidad2.Text = (Convert.ToInt32(txtcantidad2.Text) > Convert.ToInt32(txtStock2.Text)) ? "0" : txtcantidad2.Text) +
                Convert.ToInt32(txtcantidad3.Text = (txtcantidad3.Text == "") ? "0" : txtcantidad3.Text = (Convert.ToInt32(txtcantidad3.Text) > Convert.ToInt32(txtStock3.Text)) ? "0" : txtcantidad3.Text) +
                Convert.ToInt32(txtcantidad4.Text = (txtcantidad4.Text == "") ? "0" : txtcantidad4.Text = (Convert.ToInt32(txtcantidad4.Text) > Convert.ToInt32(txtStock4.Text)) ? "0" : txtcantidad4.Text) +
                Convert.ToInt32(txtcantidad5.Text = (txtcantidad5.Text == "") ? "0" : txtcantidad5.Text = (Convert.ToInt32(txtcantidad5.Text) > Convert.ToInt32(txtStock5.Text)) ? "0" : txtcantidad5.Text) +
                Convert.ToInt32(txtcantidad6.Text = (txtcantidad6.Text == "") ? "0" : txtcantidad6.Text = (Convert.ToInt32(txtcantidad6.Text) > Convert.ToInt32(txtStock6.Text)) ? "0" : txtcantidad6.Text) +
                Convert.ToInt32(txtcantidad7.Text = (txtcantidad7.Text == "") ? "0" : txtcantidad7.Text = (Convert.ToInt32(txtcantidad7.Text) > Convert.ToInt32(txtStock7.Text)) ? "0" : txtcantidad7.Text) +
                Convert.ToInt32(txtcantidad8.Text = (txtcantidad8.Text == "") ? "0" : txtcantidad8.Text = (Convert.ToInt32(txtcantidad8.Text) > Convert.ToInt32(txtStock8.Text)) ? "0" : txtcantidad8.Text) +
                Convert.ToInt32(txtcantidad9.Text = (txtcantidad9.Text == "") ? "0" : txtcantidad9.Text = (Convert.ToInt32(txtcantidad9.Text) > Convert.ToInt32(txtStock9.Text)) ? "0" : txtcantidad9.Text) +
                Convert.ToInt32(txtcantidad10.Text = (txtcantidad10.Text == "") ? "0" : txtcantidad10.Text = (Convert.ToInt32(txtcantidad10.Text) > Convert.ToInt32(txtStock10.Text)) ? "0" : txtcantidad10.Text);
                txtcantidaddetalle.Text = Suma.ToString();
            }
            else if (indicador == 3)
            {

                int Suma = 0;
                Suma = Convert.ToInt32(txtcantidad1.Text = (txtcantidad1.Text == "") ? "0" : txtcantidad1.Text) +
                Convert.ToInt32(txtcantidad2.Text = (txtcantidad2.Text == "") ? "0" : txtcantidad2.Text) +
                Convert.ToInt32(txtcantidad3.Text = (txtcantidad3.Text == "") ? "0" : txtcantidad3.Text) +
                Convert.ToInt32(txtcantidad4.Text = (txtcantidad4.Text == "") ? "0" : txtcantidad4.Text) +
                Convert.ToInt32(txtcantidad5.Text = (txtcantidad5.Text == "") ? "0" : txtcantidad5.Text) +
                Convert.ToInt32(txtcantidad6.Text = (txtcantidad6.Text == "") ? "0" : txtcantidad6.Text) +
                Convert.ToInt32(txtcantidad7.Text = (txtcantidad7.Text == "") ? "0" : txtcantidad7.Text) +
                Convert.ToInt32(txtcantidad8.Text = (txtcantidad8.Text == "") ? "0" : txtcantidad8.Text) +
                Convert.ToInt32(txtcantidad9.Text = (txtcantidad9.Text == "") ? "0" : txtcantidad9.Text) +
                Convert.ToInt32(txtcantidad10.Text = (txtcantidad10.Text == "") ? "0" : txtcantidad10.Text);
                txtcantidaddetalle.Text = Suma.ToString();
            }
        }

        private void lkpSerie_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                List<ERegistroSerie> lstSerie = new BCentral().ListarRegistroSerie().Where(x => x.resec_iid_registro_serie == Convert.ToInt32(lkpSerie.EditValue)).ToList();
                txtrangotallas.Text = lstSerie[0].resec_vtalla_inicial + "/" + lstSerie[0].resec_vtalla_final;
            }
        }
    }
}