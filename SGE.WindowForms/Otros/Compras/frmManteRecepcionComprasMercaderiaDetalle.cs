using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmManteRecepcionComprasMercaderiaDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<EProdProducto> oProducto = new List<EProdProducto>();
        public List<ERecepcionComprasMercaderiaDetalle> lstDetalle = new List<ERecepcionComprasMercaderiaDetalle>();
        public ERecepcionComprasMercaderiaDetalle _BE = new ERecepcionComprasMercaderiaDetalle();
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public int icod_almacen;
        public int icod_motivo;
        public int Indicador;

        public frmManteRecepcionComprasMercaderiaDetalle()
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
                //bteProducto.Enabled = Enabled;
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
            string[] grupo = _BE.rcmd_rango_talla.Split('/');
            txtitem.Text = _BE.rcmd_iid_detalle.ToString();
            btncodigoProducto.Tag = _BE.prdc_icod_producto;
            btncodigoProducto.Text = _BE.strCodProd;
            txtdescripcion.Text = _BE.DesProducto;
            txtcantidaddetalle.Text = _BE.rcmd_ncantidad.ToString();
            txtgrupo.Text = grupo[0].ToString();
            txtgrupo2.Text = grupo[1].ToString();
            //txtpreciounit.Text = _BE.ocod_ncunitario.ToString();
            //txttotalitem.Text = _BE.ocod_nmonto_total.ToString();
            txttalla1.Text = string.Format("{0:00}", _BE.rcmd_talla1);
            txttalla2.Text = string.Format("{0:00}", _BE.rcmd_talla2);
            txttalla3.Text = string.Format("{0:00}", _BE.rcmd_talla3);
            txttalla4.Text = string.Format("{0:00}", _BE.rcmd_talla4);
            txttalla5.Text = string.Format("{0:00}", _BE.rcmd_talla5);
            txttalla6.Text = string.Format("{0:00}", _BE.rcmd_talla6);
            txttalla7.Text = string.Format("{0:00}", _BE.rcmd_talla7);
            txttalla8.Text = string.Format("{0:00}", _BE.rcmd_talla8);
            txttalla9.Text = string.Format("{0:00}", _BE.rcmd_talla9);
            txttalla10.Text = string.Format("{0:00}", _BE.rcmd_talla10);

            txtCantOC1.Text = _BE.CantOC1.ToString();
            txtCantOC2.Text = _BE.CantOC2.ToString();
            txtCantOC3.Text = _BE.CantOC3.ToString();
            txtCantOC4.Text = _BE.CantOC4.ToString();
            txtCantOC5.Text = _BE.CantOC5.ToString();
            txtCantOC6.Text = _BE.CantOC6.ToString();
            txtCantOC7.Text = _BE.CantOC7.ToString();
            txtCantOC8.Text = _BE.CantOC8.ToString();
            txtCantOC9.Text = _BE.CantOC9.ToString();
            txtCantOC10.Text = _BE.CantOC10.ToString();

            txtSaldoOC1.Text = _BE.SaldoOC1.ToString();
            txtSaldoOC2.Text = _BE.SaldoOC2.ToString();
            txtSaldoOC3.Text = _BE.SaldoOC3.ToString();
            txtSaldoOC4.Text = _BE.SaldoOC4.ToString();
            txtSaldoOC5.Text = _BE.SaldoOC5.ToString();
            txtSaldoOC6.Text = _BE.SaldoOC6.ToString();
            txtSaldoOC7.Text = _BE.SaldoOC7.ToString();
            txtSaldoOC8.Text = _BE.SaldoOC8.ToString();
            txtSaldoOC9.Text = _BE.SaldoOC9.ToString();
            txtSaldoOC10.Text = _BE.SaldoOC10.ToString();

            txtcantidad1.Text = _BE.rcmd_cant_talla1.ToString();
            txtcantidad2.Text = _BE.rcmd_cant_talla2.ToString();
            txtcantidad3.Text = _BE.rcmd_cant_talla3.ToString();
            txtcantidad4.Text = _BE.rcmd_cant_talla4.ToString();
            txtcantidad5.Text = _BE.rcmd_cant_talla5.ToString();
            txtcantidad6.Text = _BE.rcmd_cant_talla6.ToString();
            txtcantidad7.Text = _BE.rcmd_cant_talla7.ToString();
            txtcantidad8.Text = _BE.rcmd_cant_talla8.ToString();
            txtcantidad9.Text = _BE.rcmd_cant_talla9.ToString();
            txtcantidad10.Text = _BE.rcmd_cant_talla10.ToString();
        }
        private void BorrarTexto()
        {
            txtitem.Text = "";
            btncodigoProducto.Text = "";
            txtdescripcion.Text = "";
            txtgrupo.Text = "0";
            txtgrupo2.Text = "0";
            txtcantidaddetalle.Text = "0";
            txtpreciounit.Text = "0.00";
            txttotalitem.Text = "0.00";
            txtgrupo.Enabled = true;
            txtgrupo2.Enabled = true;
            btncodigoProducto.Enabled = true;

            //BorrarTalla();
            //BorrarCantidades();
        }
        private void btncodigoProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (FrmListarProducto Producto = new FrmListarProducto())
            {
                if (Producto.ShowDialog() == DialogResult.OK)
                {
                    btncodigoProducto.Tag = Producto._Be.pr_icod_producto;
                    btncodigoProducto.Text = Producto._Be.pr_vcodigo_externo.Substring(0, 13);
                    txtdescripcion.Text = Producto._Be.pr_vdescripcion_producto;
                }
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AgregarDetalle();
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
                if ((txtgrupo.Text) == "" || txtgrupo2.Text == "")
                {
                    oBase = txtgrupo;
                    XtraMessageBox.Show("Ingrese rango de tallas", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (Convert.ToDecimal(txtcantidaddetalle.Text) == 0)
                {
                    oBase = txtgrupo;
                    XtraMessageBox.Show("Ingrese la cantidad de las tallas", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                TextEdit[] textoCantidad = GetTextoCantidad();
                TextEdit[] textoTalla = GetTextoTalla();

                //_BE.ningc_iid_almacen = Convert.ToInt32(icod_almacen);
                _BE.rcmd_iid_detalle = Convert.ToInt32(txtitem.Text);
                //_BE.tablc_iid_motivo = Convert.ToInt32(icod_motivo);
                _BE.prdc_icod_producto = Convert.ToInt32(btncodigoProducto.Tag);
                _BE.strCodProd = btncodigoProducto.Text;
                _BE.DesProducto = txtdescripcion.Text;
                _BE.rcmd_rango_talla = txtgrupo.Text + "/" + txtgrupo2.Text;
                //_BE.ningcd_iid_moneda = 1;
                _BE.rcmd_flag_estado = true;
                _BE.rcmd_ncantidad = Convert.ToDecimal(txtcantidaddetalle.Text);
                //_BE.grcd_ncunitario = Convert.ToDecimal(txtpreciounit.Text);
                //_BE.grcd_nmonto_total = Convert.ToDecimal(txttotalitem.Text);
                _BE.rcmd_talla1 = Convert.ToInt32((textoTalla[0].Text == "") ? null : textoTalla[0].Text);
                _BE.rcmd_talla2 = Convert.ToInt32((textoTalla[1].Text == "") ? null : textoTalla[1].Text);
                _BE.rcmd_talla3 = Convert.ToInt32((textoTalla[2].Text == "") ? null : textoTalla[2].Text);
                _BE.rcmd_talla4 = Convert.ToInt32((textoTalla[3].Text == "") ? null : textoTalla[3].Text);
                _BE.rcmd_talla5 = Convert.ToInt32((textoTalla[4].Text == "") ? null : textoTalla[4].Text);
                _BE.rcmd_talla6 = Convert.ToInt32((textoTalla[5].Text == "") ? null : textoTalla[5].Text);
                _BE.rcmd_talla7 = Convert.ToInt32((textoTalla[6].Text == "") ? null : textoTalla[6].Text);
                _BE.rcmd_talla8 = Convert.ToInt32((textoTalla[7].Text == "") ? null : textoTalla[7].Text);
                _BE.rcmd_talla9 = Convert.ToInt32((textoTalla[8].Text == "") ? null : textoTalla[8].Text);
                _BE.rcmd_talla10 = Convert.ToInt32((textoTalla[9].Text == "") ? null : textoTalla[9].Text);
                _BE.rcmd_cant_talla1 = Convert.ToInt32((textoCantidad[0].Text == "") ? null : textoCantidad[0].Text);
                _BE.rcmd_cant_talla2 = Convert.ToInt32((textoCantidad[1].Text == "") ? null : textoCantidad[1].Text);
                _BE.rcmd_cant_talla3 = Convert.ToInt32((textoCantidad[2].Text == "") ? null : textoCantidad[2].Text);
                _BE.rcmd_cant_talla4 = Convert.ToInt32((textoCantidad[3].Text == "") ? null : textoCantidad[3].Text);
                _BE.rcmd_cant_talla5 = Convert.ToInt32((textoCantidad[4].Text == "") ? null : textoCantidad[4].Text);
                _BE.rcmd_cant_talla6 = Convert.ToInt32((textoCantidad[5].Text == "") ? null : textoCantidad[5].Text);
                _BE.rcmd_cant_talla7 = Convert.ToInt32((textoCantidad[6].Text == "") ? null : textoCantidad[6].Text);
                _BE.rcmd_cant_talla8 = Convert.ToInt32((textoCantidad[7].Text == "") ? null : textoCantidad[7].Text);
                _BE.rcmd_cant_talla9 = Convert.ToInt32((textoCantidad[8].Text == "") ? null : textoCantidad[8].Text);
                _BE.rcmd_cant_talla10 = Convert.ToInt32((textoCantidad[9].Text == "") ? null : textoCantidad[9].Text);


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    _BE.intTipoOperacion = 1;
                    lstDetalle.Add(_BE);
                }
                else
                {
                    if (_BE.intTipoOperacion != 1)
                        _BE.intTipoOperacion = 2;
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
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finally
            {

            }
        }
        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        public void modificarDeta()
        {
            TextEdit[] textoCantidad = GetTextoCantidad();
            TextEdit[] textoTalla = GetTextoTalla();
            for (int x = 0; x <= 9; x++)
            {
                string codigoexterno = btncodigoProducto.Text + textoTalla[x].Text;
                oProducto = new BCentral().VerificarProdProducto(codigoexterno);
                if (oProducto.Count > 0)
                    textoCantidad[x].Enabled = true;
                else
                    textoCantidad[x].Enabled = false;
            }
        }
        private void FrmNotaIngresoRegistroDet_Load(object sender, EventArgs e)
        {

        }

        private void btngenerar_Click(object sender, EventArgs e)
        {

            if (txtgrupo.Text != "" && txtgrupo2.Text != "")
            {
                oProducto = new List<EProdProducto>();

                if ((btncodigoProducto.Tag) == null)
                {
                    XtraMessageBox.Show("Ingrese el Producto", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Convert.ToInt32(txtgrupo.Text) > Convert.ToInt32(txtgrupo2.Text))
                {
                    XtraMessageBox.Show("El numero de talla es incorrecta", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (((Convert.ToInt32(txtgrupo2.Text) - Convert.ToInt32(txtgrupo.Text)) + 1) > 10)
                {
                    XtraMessageBox.Show("El rango de Talla es de 1 - 10", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    BorrarCantidades();
                    BorrarTalla();
                    int i = -1;
                    TextEdit[] textoCantidad = GetTextoCantidad();
                    TextEdit[] textoTalla = GetTextoTalla();
                    for (int x = Convert.ToInt32(txtgrupo.Text); x <= Convert.ToInt32(txtgrupo2.Text); x++)
                    {
                        i++;
                        textoTalla[i].Text = string.Format("{0:00}", x).ToString();
                        textoCantidad[i].Text = "0";
                        string codigoexterno = btncodigoProducto.Text + textoTalla[i].Text;
                        oProducto = new BCentral().VerificarProdProducto(codigoexterno);

                        if (oProducto.Count > 0)
                            textoCantidad[i].Enabled = true;
                        else
                            textoCantidad[i].Enabled = false;
                    }
                    btnGuardar.Enabled = true;
                }
            }
        }

        private void txtcantidad1_EditValueChanged(object sender, EventArgs e)
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
            txttotalitem.Text = (Convert.ToDecimal(txtpreciounit.Text) * Suma).ToString();
            lblmontototal.Text = (Convert.ToDecimal(txtpreciounit.Text) * Suma).ToString();
            txtcantidaddetalle.Text = Suma.ToString();
            lblcantidadtotal.Text = Suma.ToString();
            if (Indicador == 1)
            {
                bool flag = true;
                BaseEdit oBase = null;
                try
                {
                    if (Convert.ToInt32(txtcantidad1.Text) > Convert.ToInt32(txtSaldoOC1.Text))
                    {
                        oBase = txtcantidad1;
                        throw new ArgumentException("La cantidad no puede ser mayor al Saldo, favor de registrar RUC del Cliente");
                    }
                    if (Convert.ToInt32(txtcantidad2.Text) > Convert.ToInt32(txtSaldoOC2.Text))
                    {
                        oBase = txtcantidad2;
                        throw new ArgumentException("La cantidad no puede ser mayor al Saldo, favor de registrar RUC del Cliente");
                    }
                    if (Convert.ToInt32(txtcantidad3.Text) > Convert.ToInt32(txtSaldoOC3.Text))
                    {
                        oBase = txtcantidad3;
                        throw new ArgumentException("La cantidad no puede ser mayor al Saldo, favor de registrar RUC del Cliente");
                    }
                    if (Convert.ToInt32(txtcantidad4.Text) > Convert.ToInt32(txtSaldoOC4.Text))
                    {
                        oBase = txtcantidad4;
                        throw new ArgumentException("La cantidad no puede ser mayor al Saldo, favor de registrar RUC del Cliente");
                    }
                    if (Convert.ToInt32(txtcantidad5.Text) > Convert.ToInt32(txtSaldoOC5.Text))
                    {
                        oBase = txtcantidad5;
                        throw new ArgumentException("La cantidad no puede ser mayor al Saldo, favor de registrar RUC del Cliente");
                    }
                    if (Convert.ToInt32(txtcantidad6.Text) > Convert.ToInt32(txtSaldoOC6.Text))
                    {
                        oBase = txtcantidad6;
                        throw new ArgumentException("La cantidad no puede ser mayor al Saldo, favor de registrar RUC del Cliente");
                    }
                    if (Convert.ToInt32(txtcantidad7.Text) > Convert.ToInt32(txtSaldoOC7.Text))
                    {
                        oBase = txtcantidad7;
                        throw new ArgumentException("La cantidad no puede ser mayor al Saldo, favor de registrar RUC del Cliente");
                    }
                    if (Convert.ToInt32(txtcantidad8.Text) > Convert.ToInt32(txtSaldoOC8.Text))
                    {
                        oBase = txtcantidad8;
                        throw new ArgumentException("La cantidad no puede ser mayor al Saldo, favor de registrar RUC del Cliente");
                    }
                    if (Convert.ToInt32(txtcantidad9.Text) > Convert.ToInt32(txtSaldoOC9.Text))
                    {
                        oBase = txtcantidad9;
                        throw new ArgumentException("La cantidad no puede ser mayor al Saldo, favor de registrar RUC del Cliente");
                    }
                    if (Convert.ToInt32(txtcantidad10.Text) > Convert.ToInt32(txtSaldoOC10.Text))
                    {
                        oBase = txtcantidad10;
                        throw new ArgumentException("La cantidad no puede ser mayor al Saldo, favor de registrar RUC del Cliente");
                    }

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

        }

        private void btncodigoProducto_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtpreciounit_EditValueChanged(object sender, EventArgs e)
        {
            txttotalitem.Text = (Convert.ToDecimal(txtpreciounit.Text) * Convert.ToDecimal(txtcantidaddetalle.Text)).ToString();
        }


    }
}