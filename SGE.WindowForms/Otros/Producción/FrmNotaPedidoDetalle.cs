using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Producción
{
    public partial class FrmNotaPedidoDetalle : DevExpress.XtraEditors.XtraForm
    {
        public int orped_icod_item_orden_pedido;//referencia
        public string rutaFichaTecnica;
        public Boolean flag_exonerado;
        public Boolean flag_afecto_igv;
        public string PorIGV;
        public List<ENotaPedidoCab> oPedido;
        public ENotaPedidoDet _BE = new ENotaPedidoDet();
        public BSMaintenanceStatus oState;
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
        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpResponsable.Enabled = !Enabled;

                btnmodelo.Enabled = !Enabled;
                LkpColor.Enabled = !Enabled;
                LkpMarca.Enabled = !Enabled;
                lkpTipo.Enabled = !Enabled;
                lkpSerie.Enabled = Enabled;
                txtTotal.Enabled = !Enabled;
                txtDocenas.Enabled = !Enabled;
                btnGenerar.Enabled = Enabled;
                txtitem.Enabled = Enabled;
                txtCodigoCliente.Enabled = false;
                txtColorCliente.Enabled = false;
            }
            else
            {
                lkpResponsable.Enabled = !Enabled;

                btnmodelo.Enabled = !Enabled;
                LkpColor.Enabled = !Enabled;
                LkpMarca.Enabled = !Enabled;
                lkpTipo.Enabled = !Enabled;
                lkpSerie.Enabled = !Enabled;
                txtTotal.Enabled = Enabled;
                txtDocenas.Enabled = Enabled;
            }
            if (txttalla1.Text == Convert.ToString(0))
            {

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
        public FrmNotaPedidoDetalle()
        {
            InitializeComponent();
        }
        public void setValues()
        {
            if (_BE.noped_icod_item_nota_pedido != 0)
            {
                try { _BE.imagen2 = Image.FromStream(WebDavTest.Get($"{Rutas.RutaImagenes}{Rutas.RutaNotaPedido}{_BE.noped_icod_item_nota_pedido}/", $"{_BE.noped_icod_item_nota_pedido}.png")); }
                catch { }
            }
            ptrimagen.Image = _BE.imagen2;

        }


        private void btnGenerar_Click(object sender, EventArgs e)
        {
            List<ERegistroSerie> lstSerie = new BCentral().ListarRegistroSerie().Where(x => x.resec_iid_registro_serie == Convert.ToInt32(lkpSerie.EditValue)).ToList();

            if (lstSerie[0].resec_vtalla_inicial != "0" && lstSerie[0].resec_vtalla_final != "0")
            {

                //BorrarCantidades();
                //BorrarTalla();
                int i = -1;
                TextEdit[] textoCantidad = GetTextoCantidad();
                TextEdit[] textoTalla = GetTextoTalla();

                for (int x = Convert.ToInt32(lstSerie[0].resec_vtalla_inicial); x <= Convert.ToInt32(lstSerie[0].resec_vtalla_final); x++)
                {
                    i++;
                    textoTalla[i].Text = string.Format("{0:00}", x).ToString();
                    textoCantidad[i].Text = "0";

                    //string codigoexterno = btncodigoProducto.Text + textoTalla[i].Text;
                    textoCantidad[i].Enabled = true;
                }

                lkpResponsable.Enabled = false;
                LkpMarca.Enabled = false;
                lkpTipo.Enabled = false;
                btnmodelo.Enabled = false;
                LkpColor.Enabled = false;
                lkpSerie.Enabled = false;
                btnGenerar.Enabled = false;
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


        private TextEdit[] GetTextoTalla()
        {
            TextEdit[] texto = new TextEdit[] { txttalla1, txttalla2, txttalla3, txttalla4,
                            txttalla5, txttalla6, txttalla7, txttalla8, txttalla9, txttalla10};
            return texto;
        }
        private void FrmNotaSalidaDetalle_Load(object sender, EventArgs e)
        {
            EProdTablaRegistro ob = new EProdTablaRegistro();
            ob.iid_tipo_tabla = 11;
            BSControls.LoaderLook(LkpMarca, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            ob.iid_tipo_tabla = 8;
            BSControls.LoaderLook(LkpColor, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            BSControls.LoaderLook(lkpSerie, new BCentral().ListarRegistroSerie(), "resec_vdescripcion", "resec_iid_registro_serie", true);
            ob.iid_tipo_tabla = 2;
            BSControls.LoaderLook(lkpTipo, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            BSControls.LoaderLook(lkpResponsable, new BCompras().ListarProveedorComboOPE(), "proc_responsable_produccion", "iid_icod_proveedor", true);
            lkpResponsable.EditValue = 1150;
            txtTotal.Enabled = false;
            txtDocenas.Enabled = false;
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(94), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            lkpSituacion.Enabled = false;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                LkpMarca.EditValue = _BE.noped_imarca;
                LkpColor.EditValue = _BE.noped_icolor;
                lkpTipo.EditValue = _BE.noped_itipo;
                lkpSerie.EditValue = _BE.noped_serie;
                lkpResponsable.EditValue = _BE.noped_iresponsable;
                lkpSituacion.EditValue = _BE.noped_isituacion;
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

                if (Convert.ToDecimal(txtTotal.Text) <= 0)
                {
                    oBase = txtTotal;
                    throw new ArgumentException("El Total debe ser mayor a 0");
                }


                TextEdit[] textoCantidad = GetTextoCantidad();
                TextEdit[] textoTalla = GetTextoTalla();

                _BE.noped_iitem_nota_pedido = Convert.ToInt32(txtitem.Text);
                _BE.noped_iresponsable = Convert.ToInt32(lkpResponsable.EditValue);
                _BE.NomVendedor = lkpResponsable.Text;

                _BE.noped_imodelo = Convert.ToInt32(btnmodelo.Tag);
                _BE.strmodelo = btnmodelo.Text;
                _BE.noped_icolor = Convert.ToInt32(LkpColor.EditValue);
                _BE.pr_viid_color = LkpColor.Text;
                _BE.noped_imarca = Convert.ToInt32(LkpMarca.EditValue);
                _BE.pr_viid_marca = LkpMarca.Text;
                _BE.noped_itipo = Convert.ToInt32(lkpTipo.EditValue);
                _BE.pr_viid_tipo = lkpTipo.Text;
                _BE.noped_serie = Convert.ToInt32(lkpSerie.EditValue);
                _BE.resec_vdescripcion = lkpSerie.Text;
                _BE.noped_itotal_item = Convert.ToInt32(txtTotal.Text);
                _BE.noped_ndocenas = Convert.ToDecimal(txtDocenas.Text);
                _BE.noped_talla1 = Convert.ToInt32((textoTalla[0].Text == "") ? null : textoTalla[0].Text);
                _BE.noped_talla2 = Convert.ToInt32((textoTalla[1].Text == "") ? null : textoTalla[1].Text);
                _BE.noped_talla3 = Convert.ToInt32((textoTalla[2].Text == "") ? null : textoTalla[2].Text);
                _BE.noped_talla4 = Convert.ToInt32((textoTalla[3].Text == "") ? null : textoTalla[3].Text);
                _BE.noped_talla5 = Convert.ToInt32((textoTalla[4].Text == "") ? null : textoTalla[4].Text);
                _BE.noped_talla6 = Convert.ToInt32((textoTalla[5].Text == "") ? null : textoTalla[5].Text);
                _BE.noped_talla7 = Convert.ToInt32((textoTalla[6].Text == "") ? null : textoTalla[6].Text);
                _BE.noped_talla8 = Convert.ToInt32((textoTalla[7].Text == "") ? null : textoTalla[7].Text);
                _BE.noped_talla9 = Convert.ToInt32((textoTalla[8].Text == "") ? null : textoTalla[8].Text);
                _BE.noped_talla10 = Convert.ToInt32((textoTalla[9].Text == "") ? null : textoTalla[9].Text);
                _BE.noped_cant_talla1 = Convert.ToInt32((textoCantidad[0].Text == "") ? null : textoCantidad[0].Text);
                _BE.noped_cant_talla2 = Convert.ToInt32((textoCantidad[1].Text == "") ? null : textoCantidad[1].Text);
                _BE.noped_cant_talla3 = Convert.ToInt32((textoCantidad[2].Text == "") ? null : textoCantidad[2].Text);
                _BE.noped_cant_talla4 = Convert.ToInt32((textoCantidad[3].Text == "") ? null : textoCantidad[3].Text);
                _BE.noped_cant_talla5 = Convert.ToInt32((textoCantidad[4].Text == "") ? null : textoCantidad[4].Text);
                _BE.noped_cant_talla6 = Convert.ToInt32((textoCantidad[5].Text == "") ? null : textoCantidad[5].Text);
                _BE.noped_cant_talla7 = Convert.ToInt32((textoCantidad[6].Text == "") ? null : textoCantidad[6].Text);
                _BE.noped_cant_talla8 = Convert.ToInt32((textoCantidad[7].Text == "") ? null : textoCantidad[7].Text);
                _BE.noped_cant_talla9 = Convert.ToInt32((textoCantidad[8].Text == "") ? null : textoCantidad[8].Text);
                _BE.noped_cant_talla10 = Convert.ToInt32((textoCantidad[9].Text == "") ? null : textoCantidad[9].Text);
                _BE.noped_vcodigo_cliente = txtCodigoCliente.Text;
                _BE.noped_vcolor_cliente = txtColorCliente.Text;

                _BE.intTipoOperacion = 1;
                _BE.noped_isituacion = Convert.ToInt32(lkpSituacion.EditValue);
                _BE.strsituacion = lkpSituacion.Text;

                _BE.noped_nprecio_fabrica = Convert.ToDecimal(txtPrecioFabrica.Text);
                _BE.noped_nprecio_cliente = Convert.ToDecimal(txtPrecioCliente.Text);

                _BE.imagen2 = ptrimagen.Image;

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

                _BE.noped_iresponsable = Convert.ToInt32(lkpResponsable.EditValue);
                _BE.NomVendedor = lkpResponsable.Text;
                _BE.noped_imodelo = Convert.ToInt32(btnmodelo.Tag);
                _BE.strmodelo = btnmodelo.Text;
                _BE.noped_icolor = Convert.ToInt32(LkpColor.EditValue);
                _BE.pr_viid_color = LkpColor.Text;
                _BE.noped_imarca = Convert.ToInt32(LkpMarca.EditValue);
                _BE.pr_viid_marca = LkpMarca.Text;
                _BE.noped_itipo = Convert.ToInt32(lkpTipo.EditValue);
                _BE.pr_viid_tipo = lkpTipo.Text;
                _BE.noped_itotal_item = Convert.ToInt32(txtTotal.Text);
                _BE.noped_ndocenas = Convert.ToDecimal(txtDocenas.Text);
                _BE.noped_talla1 = Convert.ToInt32((textoTalla[0].Text == "") ? null : textoTalla[0].Text);
                _BE.noped_talla2 = Convert.ToInt32((textoTalla[1].Text == "") ? null : textoTalla[1].Text);
                _BE.noped_talla3 = Convert.ToInt32((textoTalla[2].Text == "") ? null : textoTalla[2].Text);
                _BE.noped_talla4 = Convert.ToInt32((textoTalla[3].Text == "") ? null : textoTalla[3].Text);
                _BE.noped_talla5 = Convert.ToInt32((textoTalla[4].Text == "") ? null : textoTalla[4].Text);
                _BE.noped_talla6 = Convert.ToInt32((textoTalla[5].Text == "") ? null : textoTalla[5].Text);
                _BE.noped_talla7 = Convert.ToInt32((textoTalla[6].Text == "") ? null : textoTalla[6].Text);
                _BE.noped_talla8 = Convert.ToInt32((textoTalla[7].Text == "") ? null : textoTalla[7].Text);
                _BE.noped_talla9 = Convert.ToInt32((textoTalla[8].Text == "") ? null : textoTalla[8].Text);
                _BE.noped_talla10 = Convert.ToInt32((textoTalla[9].Text == "") ? null : textoTalla[9].Text);
                _BE.noped_cant_talla1 = Convert.ToInt32((textoCantidad[0].Text == "") ? null : textoCantidad[0].Text);
                _BE.noped_cant_talla2 = Convert.ToInt32((textoCantidad[1].Text == "") ? null : textoCantidad[1].Text);
                _BE.noped_cant_talla3 = Convert.ToInt32((textoCantidad[2].Text == "") ? null : textoCantidad[2].Text);
                _BE.noped_cant_talla4 = Convert.ToInt32((textoCantidad[3].Text == "") ? null : textoCantidad[3].Text);
                _BE.noped_cant_talla5 = Convert.ToInt32((textoCantidad[4].Text == "") ? null : textoCantidad[4].Text);
                _BE.noped_cant_talla6 = Convert.ToInt32((textoCantidad[5].Text == "") ? null : textoCantidad[5].Text);
                _BE.noped_cant_talla7 = Convert.ToInt32((textoCantidad[6].Text == "") ? null : textoCantidad[6].Text);
                _BE.noped_cant_talla8 = Convert.ToInt32((textoCantidad[7].Text == "") ? null : textoCantidad[7].Text);
                _BE.noped_cant_talla9 = Convert.ToInt32((textoCantidad[8].Text == "") ? null : textoCantidad[8].Text);
                _BE.noped_cant_talla10 = Convert.ToInt32((textoCantidad[9].Text == "") ? null : textoCantidad[9].Text);
                _BE.intTipoOperacion = 2;
                _BE.noped_nprecio_fabrica = Convert.ToDecimal(txtPrecioFabrica.Text);
                _BE.noped_nprecio_cliente = Convert.ToDecimal(txtPrecioCliente.Text);
                _BE.imagen2 = ptrimagen.Image;
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
            Totalizar();
        }

        private void Totalizar()
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
            txtTotal.Text = Suma.ToString();
            txtDocenas.Text = (Convert.ToDecimal(txtTotal.Text) / 12).ToString();
        }


        private void txtcantidaddetalle_EditValueChanged(object sender, EventArgs e)
        {

        }



        int[] cantotales = new int[10];
        public void Redimencionarstock()
        {
            TextEdit[] textoCantidad = GetTextoCantidad();



            for (int x = 0; x <= 9; x++)
            {


                textoCantidad[x].Enabled = true;
            }

        }
        public void cargarcantidadesOriginales()
        {

            cantotales[0] = Convert.ToInt32(txtcantidad1.Text);
            cantotales[1] = Convert.ToInt32(txtcantidad2.Text);
            cantotales[2] = Convert.ToInt32(txtcantidad3.Text);
            cantotales[3] = Convert.ToInt32(txtcantidad4.Text);
            cantotales[4] = Convert.ToInt32(txtcantidad5.Text);
            cantotales[5] = Convert.ToInt32(txtcantidad6.Text);
            cantotales[6] = Convert.ToInt32(txtcantidad7.Text);
            cantotales[7] = Convert.ToInt32(txtcantidad8.Text);
            cantotales[8] = Convert.ToInt32(txtcantidad9.Text);
            cantotales[9] = Convert.ToInt32(txtcantidad10.Text);
        }


        private void btnmodelo_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            List<EProdTablaRegistro> listRegistro = new List<EProdTablaRegistro>();
            EProdTablaRegistro ob = new EProdTablaRegistro();
            LkpColor.EditValue = "0";
            EProdModelo obje = new EProdModelo();
            ob.iid_tipo_tabla = 11;
            listRegistro = new BCentral().ListarProdTablaRegistro(ob).Where(obj => obj.icod_tabla == Convert.ToInt32(LkpMarca.EditValue)).ToList();
            listRegistro.ForEach(obe =>
            {
                obje.tarec_icorrelativo = obe.icod_tabla;
            });
            using (ListarModelo listmodelo = new ListarModelo())
            {
                listmodelo.CodeMarcas = Convert.ToInt32(obje.tarec_icorrelativo);
                if (listmodelo.ShowDialog() == DialogResult.OK)
                {
                    btnmodelo.Tag = string.Format("{0:0000}", listmodelo._Be.mo_icod_modelo);
                    btnmodelo.Text = listmodelo._Be.mo_vdescripcion;
                    lkpTipo.EditValue = listmodelo._Be.pr_iid_categoria;
                    lkpTipo.Text = listmodelo._Be.pr_iid_categoria_descripcion;
                }
            }
        }



        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OpenFd.Title = "Insertar Imagen";
            OpenFd.FileName = "";
            OpenFd.Filter = "JPG Imagenes(*.jpg)|*.jpg|JPEG Imagenes(*.jpeg)|*.jpeg|PNG Imagenes(*.png)|*.png";
            if (OpenFd.ShowDialog() == DialogResult.OK)
            {
                using (var bmpTemp = new Bitmap(OpenFd.FileName))
                {
                    ptrimagen.Image = new Bitmap(bmpTemp);
                }
            }
        }



        private void txtPrecioFabrica_EditValueChanged(object sender, EventArgs e)
        {
            txtPrecioTotal.Text = (Convert.ToDecimal(txtPrecioFabrica.Text) * Convert.ToDecimal(txtTotal.Text)).ToString();
        }
    }
}