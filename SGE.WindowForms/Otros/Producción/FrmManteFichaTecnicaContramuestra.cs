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
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Producción
{
    public partial class FrmManteFichaTecnicaContramuestra : DevExpress.XtraEditors.XtraForm
    {
        public EFichaTecnicaCab oBe = new EFichaTecnicaCab();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        public List<EFichaTecnicaCab> oDetail;
        List<EFichaTecnicaDet> lstFichaTecnicaDet = new List<EFichaTecnicaDet>();
        List<EFichaTecnicaDet> lstDelete = new List<EFichaTecnicaDet>();
        public string ruta;
        string strCodCliente = "";
        public string newRuta;
        public bool nuevo = false;
        //int Numero_dias_vencimiento_cliente = 0;
        //public int TipodeFactura=0;
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
        public void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                //txtFichaTecnica.Enabled = Enabled;
                dteFechaFichaTecnica.Enabled = !Enabled;
                btnmodelo.Enabled = !Enabled;
                lkpTipo.Enabled = !Enabled;
                LkpColor.Enabled = !Enabled;
                lkpLinea.Enabled = !Enabled;
                lkpTipoTrabajo.Enabled = !Enabled;
                lkpMarca.Enabled = !Enabled;

            }
            else
            {
                //txtFichaTecnica.Enabled = !Enabled;
                dteFechaFichaTecnica.Enabled = !Enabled;
                //btnmodelo.Enabled = !Enabled;
                //lkpTipo.Enabled = !Enabled;
                LkpColor.Enabled = !Enabled;
                //lkpLinea.Enabled = !Enabled;
                lkpTipoTrabajo.Enabled = !Enabled;
                //lkpMarca.Enabled = !Enabled;
            }
        }

        public void setValues()
        {
            txtFichaTecnica.Text = oBe.fitec_icod_ficha_tecnica;
            dteFechaFichaTecnica.EditValue = oBe.fitec_sfecha_ficha_tecnica;
            lkpMarca.EditValue = oBe.fitec_imarca;
            btnmodelo.Tag = oBe.fitec_imodelo;
            btnmodelo.Text = oBe.strmodelo;
            lkpTipo.EditValue = oBe.fitec_itipo;
            LkpColor.EditValue = oBe.fitec_icolor;
            lkpLinea.EditValue = oBe.fitec_ilinea;
            lkpTipoTrabajo.EditValue = oBe.fitec_itipo_trabajo;
            lkpSerie.EditValue = oBe.fitec_iserie;
            txtObservaciones.Text = oBe.fitec_vobservaciones;
            lstFichaTecnicaDet = new BCentral().ListarFichaTecnicaDet(oBe.fitec_iid_ficha_tecnica);

            viewFichaTecnica.RefreshData();
        }

        public FrmManteFichaTecnicaContramuestra()
        {
            InitializeComponent();
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            getCorrelativo();

        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;

        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
            SetCancel();
        }

        private void cargar()
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                setFecha(dteFechaFichaTecnica);
            }

            grdFichaTecnica.DataSource = lstFichaTecnicaDet;

            EProdTablaRegistro ob = new EProdTablaRegistro();
            ob.iid_tipo_tabla = 2;
            BSControls.LoaderLook(lkpTipo, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            ob.iid_tipo_tabla = 8;
            BSControls.LoaderLook(LkpColor, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            ob.iid_tipo_tabla = 3;
            BSControls.LoaderLook(lkpLinea, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            ob.iid_tipo_tabla = 20;
            BSControls.LoaderLook(lkpTipoTrabajo, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            ob.iid_tipo_tabla = 11;
            BSControls.LoaderLook(lkpMarca, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            BSControls.LoaderLook(lkpSerie, new BCentral().ListarRegistroSerie(), "resec_vdescripcion", "resec_iid_registro_serie", true);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpMarca.EditValue = oBe.fitec_imarca;
                lkpTipo.EditValue = oBe.fitec_itipo;
                LkpColor.EditValue = oBe.fitec_icolor;
                lkpLinea.EditValue = oBe.fitec_ilinea;
                lkpTipoTrabajo.EditValue = oBe.fitec_itipo_trabajo;
                lkpSerie.EditValue = oBe.fitec_iserie;
            }

        }
        public void CargarControles()
        {

        }
        public int CountRuta;
        private void getRuta()
        {
            try
            {
                var lst = new BAdministracionSistema().listarParametro();

                ruta = lst[0].DirecciónXML;

                CountRuta = lst[0].DirecciónXML.Trim().Length;

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void setFecha(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                frmManteFichaTecnicaDetalle frmdetalle = new frmManteFichaTecnicaDetalle();

                frmdetalle.txtitem.Text = (lstFichaTecnicaDet.Count + 1).ToString();
                frmdetalle.btnModificar.Enabled = false;
                frmdetalle.SetInsert();
                frmdetalle.btnAgregar.Enabled = true;
                if (frmdetalle.ShowDialog() == DialogResult.OK)
                {
                    EFichaTecnicaDet _obe = new EFichaTecnicaDet();
                    _obe.fited_iitem_ficha_tecnica = frmdetalle._BE.fited_iitem_ficha_tecnica;
                    _obe.fited_iarea = frmdetalle._BE.fited_iarea;
                    _obe.strarea = frmdetalle._BE.strarea;
                    _obe.fited_iubicacion = frmdetalle._BE.fited_iubicacion;
                    _obe.strubicacion = frmdetalle._BE.strubicacion;
                    _obe.fited_vdescripcion = frmdetalle._BE.fited_vdescripcion;
                    _obe.prdc_icod_producto = frmdetalle._BE.prdc_icod_producto;
                    _obe.strCodeProducto = frmdetalle._BE.strCodeProducto;
                    _obe.strProducto = frmdetalle._BE.strProducto;
                    _obe.strUnidadMedida = frmdetalle._BE.strUnidadMedida;
                    _obe.fited_ncantidad = frmdetalle._BE.fited_ncantidad;
                    _obe.intTipoOperacion = 1;
                    lstFichaTecnicaDet.Add(_obe);
                    grdFichaTecnica.DataSource = lstFichaTecnicaDet;
                    grdFichaTecnica.RefreshDataSource();

                }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    //oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
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

                }
            }

        }

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (txtFichaTecnica.Text == "")
                {
                    oBase = txtFichaTecnica;
                    throw new ArgumentException("Ingrese Nro. de Serie de Factura");
                }

                if (Convert.ToDateTime(dteFechaFichaTecnica.Text).Year != Parametros.intEjercicio)
                {
                    oBase = dteFechaFichaTecnica;
                    throw new ArgumentException("La fecha seleccionada esta fuera del rango del ejercicio");
                }
                oBe.fitec_icod_ficha_tecnica = txtFichaTecnica.Text;
                oBe.fitec_sfecha_ficha_tecnica = Convert.ToDateTime(dteFechaFichaTecnica.Text);
                oBe.fitec_imarca = Convert.ToInt32(lkpMarca.EditValue);
                oBe.strmarca = lkpMarca.Text;
                oBe.fitec_imodelo = Convert.ToInt32(btnmodelo.Tag);
                oBe.strmodelo = btnmodelo.Text;
                oBe.fitec_itipo = Convert.ToInt32(lkpTipo.EditValue);
                oBe.strtipo = lkpTipo.Text;
                oBe.fitec_icolor = Convert.ToInt32(LkpColor.EditValue);
                oBe.strcolor = LkpColor.Text;
                oBe.fitec_ilinea = Convert.ToInt32(lkpLinea.EditValue);
                oBe.strlinea = lkpLinea.Text;
                oBe.fitec_itipo_trabajo = Convert.ToInt32(lkpTipoTrabajo.EditValue);
                oBe.strtipo_trabajo = lkpTipoTrabajo.Text;
                oBe.fitec_iserie = Convert.ToInt32(lkpSerie.EditValue);
                oBe.strserie = lkpSerie.Text;
                if (ChosenFile == "")
                {

                    //oBe.fitec_vruta = imagen;
                }
                else
                {
                    //oBe.fitec_vruta = ChosenFile;


                    if (ruta != newRuta)
                    {
                        string Rutamage = ChosenFile.Substring(newRuta.Length);
                        oBe.fitec_vruta = Rutamage;
                    }
                    else
                    {

                        string Rutamage = ChosenFile.Substring(CountRuta);
                        oBe.fitec_vruta = Rutamage;
                    }
                }
                oBe.fitec_vobservaciones = txtObservaciones.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.fitec_icorrelativo_contramuestra = Convert.ToInt32(txtContramuestra.Text);

                if (ruta != newRuta && ruta != "" && ruta != null)
                {

                    try
                    {
                        ptrimagen.Image.Save(ruta + oBe.fitec_vruta, System.Drawing.Imaging.ImageFormat.Jpeg);// GUARDA LA IMAGEN SI ESTA SIENDO IMPORTADA DE UNA RUTA DIFERENTE REGUISTRADA EN LOS PARAMETROS

                    }
                    catch (Exception)
                    {
                        //EN CASO DE QUE LA IMAGEN YA EXISTE EN LA CARPETA MODELOS                         
                    }
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.fitec_iid_ficha_tecnica = new BCentral().InsertarFichaTecnicaCab(oBe, lstFichaTecnicaDet);
                }
                else
                {
                    new BCentral().modificarFichaTecnicaCab(oBe, lstFichaTecnicaDet, lstDelete);
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
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    MiEvento(oBe.fitec_iid_ficha_tecnica);
                    Close();
                }
            }
        }

        private void FrmManteFactura_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EFacturaDet obe = (EFacturaDet)viewFichaTecnica.GetRow(viewFichaTecnica.FocusedRowHandle);
            if (obe == null)
                return;

            modificarItem();

        }

        private void modificarItem()
        {
            EFichaTecnicaDet obe = (EFichaTecnicaDet)viewFichaTecnica.GetRow(viewFichaTecnica.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteFichaTecnicaDetalle frmdetalle = new frmManteFichaTecnicaDetalle())
            {
                frmdetalle._BE = obe;
                frmdetalle.txtitem.Text = obe.fited_iitem_ficha_tecnica.ToString();
                frmdetalle.lkpArea.EditValue = obe.fited_iarea;
                frmdetalle.lkpUbicacion.EditValue = obe.fited_iubicacion;
                frmdetalle.txtDescripcion.Text = obe.fited_vdescripcion;
                frmdetalle.bteProducto.Tag = obe.prdc_icod_producto;
                frmdetalle.bteProducto.Text = obe.strCodeProducto;
                frmdetalle.txtDescProd.Text = obe.strProducto;
                frmdetalle.LkpUnidad.Text = obe.strUnidadMedida;
                frmdetalle.txtCantidad.Text = Convert.ToString(obe.fited_ncantidad);
                frmdetalle.btnAgregar.Enabled = false;
                frmdetalle.SetModify();
                if (frmdetalle.ShowDialog() == DialogResult.OK)
                {
                    obe.fited_iarea = frmdetalle._BE.fited_iarea;
                    obe.fited_iubicacion = frmdetalle._BE.fited_iubicacion;
                    obe.fited_vdescripcion = frmdetalle._BE.fited_vdescripcion;
                    obe.prdc_icod_producto = frmdetalle._BE.prdc_icod_producto;
                    obe.strCodeProducto = frmdetalle._BE.strCodeProducto;
                    obe.strProducto = frmdetalle._BE.strProducto;
                    obe.strUnidadMedida = frmdetalle._BE.strUnidadMedida;
                    obe.fited_ncantidad = frmdetalle._BE.fited_ncantidad;
                    obe.intTipoOperacion = 2;
                    grdFichaTecnica.RefreshDataSource();
                }
            }


        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EFichaTecnicaDet obe = (EFichaTecnicaDet)viewFichaTecnica.GetRow(viewFichaTecnica.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstFichaTecnicaDet.Remove(obe);
            viewFichaTecnica.RefreshData();
        }

        private void txtObservaciones_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.None);
        }

        private void grdFactura_MouseMove(object sender, MouseEventArgs e)
        {
            //this.btnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
        }

        private void txtSerie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EFichaTecnicaDet obe = (EFichaTecnicaDet)viewFichaTecnica.GetRow(viewFichaTecnica.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstFichaTecnicaDet.Remove(obe);
            viewFichaTecnica.RefreshData();
            //setTotal();

        }

        private void txtSerie_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {


            }
        }

        private void btnGuardar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void FrmManteFactura_Load_1(object sender, EventArgs e)
        {
            cargar();
        }
        public void getCorrelativo()
        {
            var lst = new BAdministracionSistema().listarParametroProduccion();

            //txtPedido.Text = (Convert.ToInt32(lst[0].pmprc_inumero_orden_pedido) + 1).ToString();
            txtFichaTecnica.Text = (Convert.ToInt32(lst[0].pmprc_inumero_ficha_tecnica) + 1).ToString();
        }
        private void modificarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            EFichaTecnicaDet obe = (EFichaTecnicaDet)viewFichaTecnica.GetRow(viewFichaTecnica.FocusedRowHandle);
            if (obe == null)
                return;

            modificarItem();
        }

        private void eliminarToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            eliminar();
        }
        private void eliminar()
        {
            EFichaTecnicaDet obe = (EFichaTecnicaDet)viewFichaTecnica.GetRow(viewFichaTecnica.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstFichaTecnicaDet.Remove(obe);
            viewFichaTecnica.RefreshData();
        }

        public string ChosenFile = "";
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            getRuta();
            FileInfo Files;
            OpenFd.InitialDirectory = ruta;
            OpenFd.Title = "Insertar Imagen";
            OpenFd.FileName = "";
            OpenFd.Filter = "JPG Imagenes(*.jpg)|*.jpg|JPEG Imagenes(*.jpeg)|*.jpeg|PNG Imagenes(*.png)|*.png";
            if (OpenFd.ShowDialog() == DialogResult.OK)
            {

                ChosenFile = OpenFd.FileName;
                Files = new FileInfo(OpenFd.FileName);
                newRuta = OpenFd.FileName.Substring(0, OpenFd.FileName.Length - OpenFd.SafeFileName.Length);
                ptrimagen.Image = Image.FromFile(ChosenFile);

            }
        }
        public string imagen = "";
        private void btnmodelo_Click(object sender, EventArgs e)
        {
            List<EProdTablaRegistro> listRegistro = new List<EProdTablaRegistro>();
            EProdTablaRegistro ob = new EProdTablaRegistro();
            LkpColor.EditValue = "0";
            EProdModelo obje = new EProdModelo();
            ob.iid_tipo_tabla = 11;
            listRegistro = new BCentral().ListarProdTablaRegistro(ob).Where(obj => obj.icod_tabla == Convert.ToInt32(lkpMarca.EditValue)).ToList();
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
                    if (listmodelo._Be.mo_ruta == "")
                    {

                    }
                    else
                    {
                        ptrimagen.Image = Image.FromFile(listmodelo._Be.mo_ruta);
                    }
                    lkpMarca.EditValue = listmodelo._Be.tarec_iid_tabla_registro;
                    imagen = listmodelo._Be.mo_ruta;
                    lkpTipo.EditValue = listmodelo._Be.pr_iid_categoria;
                    lkpTipo.Text = listmodelo._Be.pr_iid_categoria_descripcion;
                    lkpLinea.EditValue = listmodelo._Be.pr_iid_linea;
                    lkpLinea.Text = listmodelo._Be.pr_iid_linea_descripcion;


                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lstFichaTecnicaDet.Clear();
            List<EFichaTecnicaMateriales> mListMateriales = new List<EFichaTecnicaMateriales>();
            mListMateriales = new BCentral().ListarFichaTecnicaMateriales(Convert.ToInt32(btnmodelo.Tag));
            foreach (var _BEmateriales in mListMateriales)
            {
                EFichaTecnicaDet _BEe = new EFichaTecnicaDet();
                _BEe.fited_iitem_ficha_tecnica = _BEmateriales.fited_iitem_ficha_tecnica;
                _BEe.fited_iarea = _BEmateriales.fited_iarea;
                _BEe.strarea = _BEmateriales.strarea;
                _BEe.fited_iubicacion = _BEmateriales.fited_iubicacion;
                _BEe.strubicacion = _BEmateriales.strubicacion;
                _BEe.fited_vdescripcion = _BEmateriales.fited_vdescripcion;
                _BEe.prdc_icod_producto = _BEmateriales.prdc_icod_producto;
                _BEe.strCodeProducto = _BEmateriales.strCodeProducto;
                _BEe.strProducto = _BEmateriales.strProducto;
                _BEe.strUnidadMedida = _BEmateriales.strUnidadMedida;
                _BEe.fited_ncantidad = _BEmateriales.fited_ncantidad;
                lstFichaTecnicaDet.Add(_BEe);
            }
            nuevoToolStripMenuItem.Enabled = false;
            viewFichaTecnica.RefreshData();
        }
    }
}