using DevExpress.XtraEditors;
using SGE.BusinessLogic;
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
    public partial class FrmMantCodigosBarra : DevExpress.XtraEditors.XtraForm
    {

        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;

        public int picbd_icod_plan_cod_barr;

        List<EProdTablaRegistro> LISTAMarca = new List<EProdTablaRegistro>();

        List<EProdCodigoBarraDetalle> mlistaCodigoDet = new List<EProdCodigoBarraDetalle>();
        List<EProdCodigoBarraDetalle> ListaEliminadosDet = new List<EProdCodigoBarraDetalle>();

        BCentral oblCodiBarr = new BCentral();
        Evariable oblvariables = new Evariable();
        private BSMaintenanceStatus mStatus;
        string CodigoAbreviado;

        public FrmMantCodigosBarra()
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
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
        }
        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            dtpFecha.Enabled = !Enabled;
            txtDescripcion.Enabled = Enabled;
            LkpMarca.Enabled = !Enabled;
            btnmodelo.Enabled = !Enabled;
            txtgrupo.Enabled = !Enabled;
            txtgrupo2.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                dtpFecha.Enabled = Enabled;
                txtDescripcion.Enabled = !Enabled;
                LkpMarca.Enabled = Enabled;
                btnmodelo.Enabled = Enabled;
            }
            txtDescripcion.Focus();
        }
        private void clearControl()
        {
            dtpFecha.EditValue = DateTime.Now;
            txtDescripcion.Text = "";
            LkpMarca.EditValue = 1;
            btnmodelo.Tag = null;
            btnmodelo.Text = "";
            txtDescripcion.Focus();

        }
        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }
        private void Cargar()
        {
            EProdTablaRegistro ob = new EProdTablaRegistro();
            ob.iid_tipo_tabla = 11;
            LISTAMarca = new BCentral().ListarProdTablaRegistro(ob);
            BSControls.LoaderLook(LkpUnidad, new BCentral().ListarProdUnidadMedida(), "descripcion", "id_unidad_medida", false);
            BSControls.LoaderLook(LkpMarca, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            mlistaCodigoDet = oblCodiBarr.listarProdCodigoBarraDetalle(picbd_icod_plan_cod_barr);
            dgdCodigo.DataSource = mlistaCodigoDet;

        }
        private void FrmMantCodigosBarra_Load(object sender, EventArgs e)
        {
            this.Cargar();
            btnmodelo.Tag = null;
            //btncodigoProducto.Tag = null;
            gcDetallecodigoBarra.Visible = false;
        }
        private void btnmodelo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            oblvariables = new Evariable();
            List<EProdTablaRegistro> listRegistro = new List<EProdTablaRegistro>();
            EProdTablaRegistro ob = new EProdTablaRegistro();
            EProdModelo obje = new EProdModelo();
            ob.iid_tipo_tabla = 11;
            listRegistro = new BCentral().ListarProdTablaRegistro(ob).Where(obj => obj.icod_tabla == Convert.ToInt32(LkpMarca.EditValue)).ToList();
            listRegistro.ForEach(obe =>
            {
                obje.tarec_icorrelativo = obe.icod_tabla;//ver
                CodigoAbreviado = obe.tarec_vabreviatura;
            });
            using (ListarModelo listmodelo = new ListarModelo())
            {
                listmodelo.CodeMarcas = Convert.ToInt32(obje.tarec_icorrelativo);
                if (listmodelo.ShowDialog() == DialogResult.OK)
                {
                    btnmodelo.Tag = string.Format("{0:0000}", listmodelo._Be.mo_iid_modelo);
                    btnmodelo.Text = listmodelo._Be.mo_vdescripcion;
                    oblvariables.pr_iid_categoria_descripcion = listmodelo._Be.pr_iid_categoria_descripcion;
                    oblvariables.pr_iid_linea_descripcion = listmodelo._Be.pr_iid_linea_descripcion;
                    oblvariables.pr_iid_tipo_marca_descripcion = listmodelo._Be.pr_iid_tipo_marca_descripcion;
                    oblvariables.pr_tarec_icorrelativo = Convert.ToInt32(obje.tarec_icorrelativo);
                    oblvariables.icod_modelo = listmodelo._Be.mo_icod_modelo;
                }
            }
        }
        private class Evariable
        {
            public string pr_iid_categoria_descripcion { get; set; }
            public string pr_iid_linea_descripcion { get; set; }
            public string pr_iid_tipo_marca_descripcion { get; set; }
            public int pr_tarec_icorrelativo { get; set; }
            public int icod_modelo { get; set; }
        }
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            List<EProdProducto> mlistProducto = new List<EProdProducto>();

            if (btnmodelo.Tag == null)
            {
                mlistProducto = new BCentral().ListarProdProductos().Where(obt => obt.pr_iid_marca == Convert.ToInt32(LkpMarca.EditValue)).ToList();
            }
            else
            {
                mlistProducto = new BCentral().ListarProdProductos().Where(obt => obt.pr_iid_marca == Convert.ToInt32(LkpMarca.EditValue) && obt.pr_iid_modelo == Convert.ToInt32(btnmodelo.Tag)).ToList();
            }

            foreach (EProdProducto obj in mlistProducto)
            {
                int TallaMayor;
                int TallaMenor;

                TallaMayor = Convert.ToInt32(obj.pr_vcodigo_externo.Substring(13, 2));
                TallaMenor = Convert.ToInt32(obj.pr_vcodigo_externo.Substring(13, 2));
                EProdCodigoBarraDetalle objCodBarra = new EProdCodigoBarraDetalle();
                objCodBarra.pr_vcodigo_externo = obj.pr_vcodigo_externo.Substring(0, 13);
                objCodBarra.pr_vdescripcion_producto = obj.pr_vdescripcion_producto.Substring(0, obj.pr_vdescripcion_producto.Length - 2);

                List<EProdModelo> mlistModelo = new List<EProdModelo>();
                EProdModelo objmodelo = new EProdModelo() { tarec_icorrelativo = Convert.ToInt32(obj.pr_tarec_icorrelativo) };
                mlistModelo = new BCentral().ListarProdModelo(objmodelo);
                if (mlistModelo.Count > 0)
                {
                    objCodBarra.pr_vdescripcion_modelo = mlistModelo[0].mo_vdescripcion;
                }

                List<EProdProducto> mliPro = new List<EProdProducto>();
                mliPro = new BCentral().ListarProdProductos().Where(oj => oj.pr_vcodigo_externo.Substring(0, 13) == obj.pr_vcodigo_externo.Substring(0, 13)).ToList();
                if (mliPro.Count > 0)
                {
                    objCodBarra.pr_vdescripcion_color = mliPro[0].pr_viid_color;
                }
                //objCodBarra.iusuario = ValPVTC.puvec_icod_punto_venta;
                objCodBarra.picbd_iactivo = 1;





                if (mlistaCodigoDet.Count(o => o.pr_vcodigo_externo == obj.pr_vcodigo_externo.Substring(0, 13)) == 0)
                {
                    foreach (EProdProducto objAux in mlistProducto)
                    {
                        if (obj.pr_vcodigo_externo.Substring(0, 13) == objAux.pr_vcodigo_externo.Substring(0, 13))
                        {
                            if (Convert.ToInt32(objAux.pr_vcodigo_externo.Substring(13, 2)) > TallaMayor)
                                TallaMayor = Convert.ToInt32(objAux.pr_vcodigo_externo.Substring(13, 2));

                            if (Convert.ToInt32(objAux.pr_vcodigo_externo.Substring(13, 2)) < TallaMenor)
                                TallaMenor = Convert.ToInt32(objAux.pr_vcodigo_externo.Substring(13, 2));
                        }
                    }
                    objCodBarra.picbd_rango_talla = string.Format("{0:00}", TallaMenor).ToString() + "/" + string.Format("{0:00}", TallaMayor).ToString();
                    mlistaCodigoDet.Add(objCodBarra);
                }
            }
            viewCodigos.RefreshData();

        }

        string modelo = "";
        string color = "";
        private void btncodigoProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (FrmListarProducto Producto = new FrmListarProducto())
            {
                Producto.icod_marca = Convert.ToInt32(LkpMarca.EditValue);
                Producto.condicion = 1;
                if (Producto.ShowDialog() == DialogResult.OK)
                {
                    List<EProdProducto> mlistProducto = new List<EProdProducto>();
                    //btncodigoProducto.Tag = Producto._Be.pr_icod_producto;
                    //btncodigoProducto.Text = Producto._Be.pr_vcodigo_externo.Substring(0, 13);
                    txtDescripcion.Text = Producto._Be.pr_vdescripcion_producto.Substring(0, Producto._Be.pr_vdescripcion_producto.Length - 2);
                    modelo = Producto._Be.pr_viid_modelo;
                    color = Producto._Be.pr_viid_color;
                    int tallaMayor;
                    int tallaMenor;
                    int unidadMedida = 0;
                    tallaMayor = 0;
                    tallaMenor = 90;

                    mlistProducto = (new BCentral()).ListarProdProductos().Where(o => o.pr_vcodigo_externo.Substring(0, 13) == Producto._Be.pr_vcodigo_externo.Substring(0, 13)).ToList();
                    foreach (EProdProducto objpro in mlistProducto)
                    {
                        if (Convert.ToInt32(objpro.pr_vcodigo_externo.Substring(13, 2)) > tallaMayor)
                            tallaMayor = Convert.ToInt32(objpro.pr_vcodigo_externo.Substring(13, 2));

                        if (Convert.ToInt32(objpro.pr_vcodigo_externo.Substring(13, 2)) < tallaMenor)
                            tallaMenor = Convert.ToInt32(objpro.pr_vcodigo_externo.Substring(13, 2));

                        unidadMedida = Convert.ToInt32(objpro.unidc_icod_unidad_medida);
                    }
                    txtgrupo.Text = tallaMenor.ToString();
                    txtgrupo2.Text = tallaMayor.ToString();
                    LkpUnidad.EditValue = unidadMedida;
                }
            }
        }
        private void limpiarDetalle()
        {
            //btncodigoProducto.Text = "";
            //btncodigoProducto.Tag = null;
            txtDescripcion.Text = "";
            txtgrupo.Text = "";
            txtgrupo2.Text = "";
            LkpUnidad.EditValue = null;

        }
        private void kardexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pcDetalle.Visible = true;
            gcDetallecodigoBarra.Visible = true;
            limpiarDetalle();
            btnGuardar.Enabled = false;
            btnSalir.Enabled = false;
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            pcDetalle.Visible = false;
            gcDetallecodigoBarra.Visible = false;
            btnGuardar.Enabled = true;
            btnSalir.Enabled = true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtgrupo.Text != "00" && txtgrupo2.Text != "00")
            {
                if (Convert.ToInt32(txtgrupo2.Text) > Convert.ToInt32(txtgrupo.Text))
                {
                    if (btnmodelo.Tag != null)
                    {


                        List<EProdProducto> mlistProducto = new List<EProdProducto>();
                        List<EProdProducto> mlistProductoColor = new List<EProdProducto>();
                        if (btnmodelo.Tag == null)
                        {
                            mlistProducto = new BCentral().ListarProdProductosPorMarca(Convert.ToInt32(LkpMarca.EditValue));
                        }
                        else
                        {
                            //mlistProducto = new BCentral().ListarProdProductos().Where(obt => obt.pr_tarec_icorrelativo == Convert.ToInt32(LkpMarca.EditValue) && obt.pr_iid_modelo == Convert.ToInt32(btnmodelo.Tag)).ToList();
                            mlistProducto = new BCentral().ListarProdProductosPorMarca(Convert.ToInt32(LkpMarca.EditValue)).Where(obt => obt.pr_iid_modelo == Convert.ToInt32(btnmodelo.Tag)).ToList();
                            mlistProductoColor = mlistProducto;
                            mlistProductoColor.Select(s => Convert.ToInt32(s.pr_iid_color)).Distinct();
                        }

                        foreach (EProdProducto obj in mlistProductoColor)
                        {
                            int TallaMayor;
                            int TallaMenor;

                            List<EProdProducto> mliPro = new List<EProdProducto>();

                            mliPro = mlistProducto.Where(oj => oj.pr_vcodigo_externo.Substring(0, 13) == obj.pr_vcodigo_externo.Substring(0, 13) && (Convert.ToInt32(oj.pr_viid_talla) >= Convert.ToInt32(txtgrupo.Text) && Convert.ToInt32(oj.pr_viid_talla) <= Convert.ToInt32(txtgrupo2.Text))).ToList();
                            if (mliPro.Count > 0)
                            {
                                EProdCodigoBarraDetalle objCodBarra = new EProdCodigoBarraDetalle();
                                objCodBarra.pr_vcodigo_externo = obj.pr_vcodigo_externo.Substring(0, 13);
                                objCodBarra.pr_vdescripcion_producto = obj.pr_vdescripcion_producto.Substring(0, obj.pr_vdescripcion_producto.Length - 2);

                                if (btnmodelo.Tag == null)
                                {
                                    List<EProdModelo> mlistModelo = new List<EProdModelo>();
                                    EProdModelo objmodelo = new EProdModelo() { tarec_icorrelativo = Convert.ToInt32(obj.pr_tarec_icorrelativo) };
                                    mlistModelo = new BCentral().ListarProdModeloTodo().Where(x => x.mo_iid_modelo == mliPro[0].pr_iid_modelo).ToList();
                                    objCodBarra.pr_vdescripcion_modelo = mlistModelo[0].mo_vdescripcion;
                                }
                                else
                                {
                                    objCodBarra.pr_vdescripcion_modelo = btnmodelo.Text;
                                }



                                objCodBarra.pr_vdescripcion_color = mliPro[0].pr_viid_color;
                                objCodBarra.icod_color = Convert.ToInt32(mliPro[0].pr_iid_color);
                                objCodBarra.iusuario = Convert.ToInt32(obj.puvec_icod_punto_venta);
                                objCodBarra.picbd_iactivo = 1;

                                if (mlistaCodigoDet.Count(o => o.pr_vcodigo_externo == obj.pr_vcodigo_externo.Substring(0, 13) && o.picbd_rango_talla == txtgrupo.Text + "/" + txtgrupo2.Text) == 0)
                                {
                                    List<EProdProducto> mlistaNueva = mlistProducto.Where(x => x.pr_iid_color == objCodBarra.icod_color).ToList();

                                    TallaMayor = mlistaNueva.Max(c => Convert.ToInt32(txtgrupo2.Text));
                                    TallaMenor = mlistaNueva.Min(c => Convert.ToInt32(txtgrupo.Text));

                                    objCodBarra.picbd_rango_talla = string.Format("{0:00}", TallaMenor).ToString() + "/" + string.Format("{0:00}", TallaMayor).ToString();
                                    mlistaCodigoDet.Add(objCodBarra);
                                }

                            }


                        }
                        viewCodigos.RefreshData();
                        gcDetallecodigoBarra.Visible = false;
                        pcDetalle.Visible = false;
                        btnGuardar.Enabled = true;
                        btnSalir.Enabled = true;

                    }
                    else
                    {
                        XtraMessageBox.Show("Elija un Producto ", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Ingresar la Segunda Talla NO puede ser Menor", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Ingresar la Series", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }
        private void Delete()
        {
            EProdCodigoBarraDetalle obj = (EProdCodigoBarraDetalle)viewCodigos.GetRow(viewCodigos.FocusedRowHandle);
            if (obj != null)
            {
                if (obj.operacion == 1)
                {
                    mlistaCodigoDet.Remove(obj);
                    viewCodigos.RefreshData();
                    viewCodigos.MovePrev();
                }
                else
                {
                    if (mlistaCodigoDet.Count != 1)
                    {
                        obj.operacion = 3;
                        ListaEliminadosDet.Add(obj);
                        mlistaCodigoDet.Remove(obj);
                        viewCodigos.RefreshData();
                        viewCodigos.MovePrev();
                    }
                    else
                    {
                        XtraMessageBox.Show("Debe tener al menos un Item", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.setSave();
        }
        private void setSave()
        {
            int contador = 0;//verifica que registros faltan ingresar sus cantidades
            BaseEdit oBase = null;
            Boolean Flag = true;
            EProdCodigoBarra objCodiBarr = new EProdCodigoBarra();
            oblCodiBarr = new BCentral();
            try
            {
                if (mlistaCodigoDet.Count == 0)
                {
                    throw new ArgumentException("Ingresar detalles");
                }
                if (string.IsNullOrEmpty(txtObservaciones.Text))
                {
                    oBase = txtObservaciones;
                    throw new ArgumentException("Ingresar descripción");
                }

                if (string.IsNullOrEmpty(txtgrupo.Text))
                {
                    oBase = txtgrupo;
                    throw new ArgumentException("Ingresar el Grupo de Talla");
                }
                if (string.IsNullOrEmpty(txtgrupo2.Text))
                {
                    oBase = txtgrupo2;
                    throw new ArgumentException("Ingresar el Grupo de Talla");
                }

                if (Convert.ToInt32(txtgrupo2.Text) < Convert.ToInt32(txtgrupo.Text))
                {
                    oBase = txtgrupo2;
                    throw new ArgumentException("La Segunda Talla NO puede ser mayor");
                }

                objCodiBarr.picbc_inumero_plant = Convert.ToInt32(txtNroTransferencia.Text);
                objCodiBarr.picbc_sfecha_plant = Convert.ToDateTime(dtpFecha.EditValue);
                objCodiBarr.picbc_vdescrip = txtObservaciones.Text;
                objCodiBarr.picbc_iicod_marca = Convert.ToInt32(LkpMarca.EditValue);
                if (btnmodelo.Tag != null)
                {
                    objCodiBarr.picbc_iicod_modelo = Convert.ToInt32(btnmodelo.Tag);
                    objCodiBarr.tarec_icorrelativo_modelo = oblvariables.icod_modelo;
                }
                else
                {
                    objCodiBarr.picbc_iicod_modelo = 0;
                    objCodiBarr.tarec_icorrelativo_modelo = 0;
                }


                objCodiBarr.picbc_iactivo = 1;
                objCodiBarr.iusuario = Valores.intUsuario;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oblCodiBarr.InsertarProdCodigoBarra(objCodiBarr, mlistaCodigoDet);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    objCodiBarr.picbc_icod_plan_cod_barr = picbd_icod_plan_cod_barr;
                    oblCodiBarr.ModificarProdCodigoBarra(objCodiBarr, mlistaCodigoDet, ListaEliminadosDet);
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
                else
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    Status = BSMaintenanceStatus.View;
                    this.MiEvento();
                    this.Close();
                }
            }

        }
        private void registroDeCantidadToolStripMenuItem_Click(object sender, EventArgs e)
        {

            EProdCodigoBarraDetalle obj = (EProdCodigoBarraDetalle)viewCodigos.GetRow(viewCodigos.FocusedRowHandle);
            if (obj != null)
            {
                FrmCantidadeCodigoBarraDeT frmCantidadCodi = new FrmCantidadeCodigoBarraDeT();
                frmCantidadCodi.Text = "Cantidades del Producto: " + obj.pr_vdescripcion_producto;
                frmCantidadCodi.objCodigoBarra = obj;

                if (obj.picbd_icod_plan_cod_barr_det != 0)
                {
                    frmCantidadCodi.SetModify();
                }
                else
                {
                    frmCantidadCodi.SetModify();
                }
                if (frmCantidadCodi.ShowDialog() == DialogResult.OK)
                {
                    obj = frmCantidadCodi.objCodigoBarra;
                    viewCodigos.RefreshData();
                }
            }
        }

        private void btnmodelo_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnGenerar_Click_1(object sender, EventArgs e)
        {

        }
    }
}