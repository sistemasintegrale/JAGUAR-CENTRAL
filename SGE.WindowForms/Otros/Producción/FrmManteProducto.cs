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
    public partial class FrmManteProducto : DevExpress.XtraEditors.XtraForm
    {
        string CodigoAbreviado;
        public List<EProdProducto> oProducto;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteProducto));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        MyKeyPress myKeyPressHandler = new MyKeyPress();
        public EProdTablaRegistro _Be { get; set; }
        public int TipoRegistro;
        public string NombreFormulario;
        public int pr_tarec_icorrelativo;


        Evariable oblvariables = new Evariable();
         
        public FrmManteProducto()
        {
            this.KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();
            txtcodigo.KeyPress += new KeyPressEventHandler(myKeyPressHandler.MyKeyCounter);
        }

        public List<EProdProducto> oDetail;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        private BCentral Obl;
        public int Correlative = 0;

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
            txtdescripcion.Enabled = !Enabled;
            txtabreviatura.Enabled = !Enabled;
        }

        private void clearControl()
        {
            txtcodigo.Text = "";
            txtdescripcion.Text = "";
            txtabreviatura.Text = "";
            LkpUnidad.EditValue = 1;
            txtdescripcion.Focus();
        }
        private List<EProdTablaRegistro> mlistaTalla = new List<EProdTablaRegistro>();

        private void carga()
        {
            EProdTablaRegistro ob = new EProdTablaRegistro();
            ob.iid_tipo_tabla = 11;
             

            BSControls.LoaderLook(LkpMarca, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "tarec_viid_correlativo", true);

            BSControls.LoaderLook(LkpUnidad, new BCentral().ListarProdUnidadMedida(), "descripcion", "id_unidad_medida", false);

            ob.iid_tipo_tabla = 8;
            BSControls.LoaderLook(LkpColor, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "tarec_viid_correlativo", false);

            ob.iid_tipo_tabla = 10;
            mlistaTalla = new BCentral().ListarProdTablaRegistro(ob);
            BSControls.LoaderLook(LkpTalla, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "tarec_viid_correlativo", false);

            BSControls.LoaderLook(lkpSerie, new BCentral().ListarRegistroSerie(), "resec_vdescripcion", "resec_iid_registro_serie", true);

        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            EProdProducto oBe = new EProdProducto();
            Obl = new BCentral();
            try
            {
                if (!(ValidarCarcateristicas()))
                {
                    oBase = ValidarLookup();
                    throw new ArgumentException("Ingresar Todas las Carcateristicas");
                }
                if (string.IsNullOrEmpty(txtcodigo.Text))
                {
                    oBase = txtcodigo;
                    throw new ArgumentException("Ingresar Codigo");
                }

                if (string.IsNullOrEmpty(txtdescripcion.Text))
                {
                    oBase = txtdescripcion;
                    throw new ArgumentException("Ingresar Descripcion");
                }
                if (string.IsNullOrEmpty(LkpUnidad.Text))
                {
                    oBase = LkpUnidad;
                    throw new ArgumentException("Ingresar Unidad de Medida");
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

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    var BuscarCosto = oDetail.Where(oB => oB.pr_vdescripcion_producto.ToUpper() == txtdescripcion.Text.ToUpper()).ToList();
                    if (BuscarCosto.Count > 0)
                    {
                        oBase = txtdescripcion;
                        throw new ArgumentException("La Descripcion Existe");
                    }

                    var CodigoRepetido = oDetail.Where(oB => oB.pr_vcodigo_externo.ToUpper() == txtcodigo.Text.ToUpper()).ToList();
                    if (CodigoRepetido.Count > 0)
                    {
                        oBase = txtcodigo;
                        throw new ArgumentException("El Codigo ya Existe");
                    }

                }

                oBe.pr_icod_producto = 0;
                oBe.pr_iid_producto = Correlative;

                oBe.pr_iid_marca = Convert.ToInt32(LkpMarca.EditValue);
                oBe.pr_iid_modelo = Convert.ToInt32(btnmodelo.Tag);
                oBe.pr_iid_color = Convert.ToInt32(LkpColor.EditValue);
                oBe.pr_iid_talla = Convert.ToInt32(LkpTalla.EditValue);

                oBe.pr_vcodigo_externo = txtcodigo.Text;
                oBe.pr_vdescripcion_producto = txtdescripcion.Text;
                oBe.pr_vabreviatura_producto = txtabreviatura.Text;
                oBe.pr_isituacion_producto = (comboBoxEdit1.Text == "Activo") ? 1 : 0;
                oBe.unidc_icod_unidad_medida = Convert.ToInt32(LkpUnidad.EditValue);
                oBe.intUsuario = Valores.intUsuario;


                if (Status == BSMaintenanceStatus.CreateNew)
                    oBe.pr_tarec_icorrelativo = oblvariables.pr_tarec_icorrelativo;
                else
                    oBe.pr_tarec_icorrelativo = pr_tarec_icorrelativo;


                oBe.pr_afecto_igv = chkIGV.Checked;
                oBe.pr_nporcentaje_igv = Convert.ToDecimal(txtPorcentajeIGV.Text);

                oBe.pr_icod_serie = Convert.ToInt32(lkpSerie.EditValue);


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (chkgrupal.CheckState == CheckState.Checked)
                    {
                        if (XtraMessageBox.Show("Desea Generar Codigos de talla del " + txtgrupo.Text + " al " + txtgrupo2.Text, "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            for (int y = Convert.ToInt32(txtgrupo.Text); y <= Convert.ToInt32(txtgrupo2.Text); y++)
                            {
                                x[10] = string.Format("{0:00}", y);
                                oBe.pr_vdescripcion_producto = oBe.pr_vdescripcion_producto.Substring(0, txtdescripcion.Text.Length - 1) + " " + x[10];
                                oBe.pr_vcodigo_externo = txtcodigo.Text.Trim().Substring(0, txtcodigo.Text.Trim().Length - 2) + x[10];
                                oProducto = Obl.VerificarProdProducto(oBe.pr_vcodigo_externo);
                                if (oProducto.Count == 0)
                                {
                                    var Lista = mlistaTalla.Where(ob => ob.descripcion == (Convert.ToInt32(x[10])).ToString()).ToList();
                                    Lista.ForEach(ob => { oBe.pr_iid_talla = ob.tarec_iid_correlativo; });
                                    if (Lista.Count == 1)
                                        Obl.InsertarProdProductos(oBe);
                                }
                            }
                        }
                    }
                    else
                        Obl.InsertarProdProductos(oBe);

                }
                else
                {
                    if (chkgrupal.CheckState == CheckState.Checked)
                    {
                        if (XtraMessageBox.Show("Desea Generar Codigos de talla del " + txtgrupo.Text + " al " + txtgrupo2.Text, "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            for (int y = Convert.ToInt32(txtgrupo.Text); y <= Convert.ToInt32(txtgrupo2.Text); y++)
                            {
                                x[10] = string.Format("{0:00}", y);
                                oBe.pr_vdescripcion_producto = oBe.pr_vdescripcion_producto.Substring(0, txtdescripcion.Text.Length - 1) + x[10];
                                oBe.pr_vcodigo_externo = txtcodigo.Text.Trim().Substring(0, txtcodigo.Text.Trim().Length - 2) + x[10];
                                oProducto = Obl.VerificarProdProducto(oBe.pr_vcodigo_externo);
                                if (oProducto.Count == 0)
                                {
                                    var Lista = mlistaTalla.Where(ob => ob.descripcion == x[10]).ToList();
                                    Lista.ForEach(ob => { oBe.pr_iid_talla = ob.tarec_iid_correlativo; });
                                    if (Lista.Count == 1)
                                        Obl.InsertarProdProductos(oBe);
                                }
                            }
                        }
                    }
                    else
                    {
                        oBe.pr_icod_producto = Correlative;
                        Obl.ActualizarProdProductos(oBe);
                    }
                }
            }
            catch (Exception ex)
            { 
                Services.SetErrorInput(oBase,ex.Message);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento();
                    this.Close();
                }
            }
        }

        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }

        private void FrmManteProducto_Load(object sender, EventArgs e)
        {
            carga();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetSave();
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void FrmManteProducto_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        string[] x = new string[12];
        private void chkgrupal_CheckedChanged(object sender, EventArgs e)
        {
            this.txtgrupo.Enabled = Convert.ToBoolean(chkgrupal.CheckState);
            this.txtgrupo2.Enabled = Convert.ToBoolean(chkgrupal.CheckState);
        }


        private Boolean ValidarCarcateristicas()
        {
            Boolean Foco = true;
            if (LkpMarca.Text == "") Foco = false;
            else if (LkpColor.Text == "") Foco = false;
            if (chkgrupal.CheckState == CheckState.Unchecked)
            {
                if (LkpTalla.Text == "") Foco = false;
            }
            else if (btnmodelo.Text == "") Foco = false;
            return Foco;
        }

        private LookUpEdit ValidarLookup()
        {
            LookUpEdit Texto = null;
            if (LkpMarca.Text == "") Texto = LkpMarca;

            else if (LkpColor.Text == "") Texto = LkpColor;
            else if (LkpTalla.Text == "") Texto = LkpTalla;
            return Texto;
        }

        private void LkpMarca_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit Look = (LookUpEdit)sender;
            x[0] = "CJA";
            if (Look.Name == "LkpMarca")
            {
                if (Look.EditValue != null)
                { }
                x[1] = Look.EditValue.ToString();
            }
            if (Look.Name == "LkpColor")
                x[3] = Look.EditValue.ToString();
            if (Look.Name == "LkpTalla")
                x[4] = Look.Text.ToString();
            if (btnmodelo.Tag != null)
                x[2] = btnmodelo.Tag.ToString();

            if (x[4] == "")
            {
                x[4] = "0";
            }
            if (x[4] == null)
            {
                x[4] = "0";
            }
            txtcodigo.Text = x[0] + x[1] + x[2] + x[3] + string.Format("{0:00}", Convert.ToInt32(x[4])).ToString();

            txtdescripcion.Text = String.Format("{0} {1} {2} {3} {4} {5} {6}", LkpMarca.Text, btnmodelo.Text, oblvariables.pr_iid_categoria_descripcion, oblvariables.pr_iid_linea_descripcion, oblvariables.pr_iid_tipo_marca_descripcion, LkpColor.Text, LkpTalla.Text);
            txtabreviatura.Text = CodigoAbreviado + btnmodelo.Text;
        }

        private void btncodigoalmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            oblvariables = new Evariable();
            List<EProdTablaRegistro> listRegistro = new List<EProdTablaRegistro>();
            EProdTablaRegistro ob = new EProdTablaRegistro();
            LkpColor.EditValue = "0";
            LkpTalla.EditValue = "0";
            txtdescripcion.Text = "";
            EProdModelo obje = new EProdModelo();
            ob.iid_tipo_tabla = 11;
            listRegistro = new BCentral().ListarProdTablaRegistro(ob).Where(obj => obj.tarec_viid_correlativo == LkpMarca.EditValue.ToString()).ToList();
            listRegistro.ForEach(obe =>
            {
                obje.tarec_icorrelativo = obe.icod_tabla;
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
                }
            }
        }

        private void btnmodelo_EditValueChanged(object sender, EventArgs e)
        {

        }
        private class Evariable
        {
            public string pr_iid_categoria_descripcion { get; set; }
            public string pr_iid_linea_descripcion { get; set; }
            public string pr_iid_tipo_marca_descripcion { get; set; }
            public int pr_tarec_icorrelativo { get; set; }
        }

        private void lkpSerie_EditValueChanged(object sender, EventArgs e)
        {
            List<ERegistroSerie> lstSerie = new BCentral().ListarRegistroSerie().Where(x => x.resec_iid_registro_serie == Convert.ToInt32(lkpSerie.EditValue)).ToList();
            if (lstSerie.Any())
            {
                txtgrupo.Text = lstSerie[0].resec_vtalla_inicial;
                txtgrupo2.Text = lstSerie[0].resec_vtalla_final;
            }


        }

        private void chkIGV_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}