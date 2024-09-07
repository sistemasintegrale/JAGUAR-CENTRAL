using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using SGE.WindowForms.Otros.Administracion_del_Sistema.Listados;
using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Caja
{
    public partial class FrmManteConceptoDeCajaEfectivo : DevExpress.XtraEditors.XtraForm
    {

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteConceptoDeCaja));

        private List<EConceptoCaja> mlist = new List<EConceptoCaja>();
        private List<EConceptoCaja> mlistDetalle = new List<EConceptoCaja>();
        private List<ECuentaContable> mlistCuenta = new List<ECuentaContable>();
        private List<ECuentaContable> aux = new List<ECuentaContable>();
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;



        public FrmManteConceptoDeCajaEfectivo()
        {
            InitializeComponent();
        }


        public List<EConceptoCaja> oDetail;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        private BTesoreria Obl;
        public int Correlative = 0;
        public int code = 0;

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
            if (Status == BSMaintenanceStatus.View)
            {
                txtCod.Enabled = false;
                txtDescripcion.Enabled = false;
                bteTipoDoc.Enabled = false;
                bteClaseDoc.Enabled = false;
                bteCuenta.Enabled = false;
                BtnGuardar.Enabled = false;
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCod.Enabled = false;
            txtCod.Focus();
        }
        private void txtcuentaContable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                ListarCuenta();
            }
        }

        private void cargar()
        {
            mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
        }

        private void FrmManteConceptoDeCaja_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpTipo, new BGeneral().listarTablaRegistro(97), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            cargar();
            LoadMask();
        }
        private void LoadMask()
        {
            List<EParametroContable> mlista = (new BContabilidad()).listarParametroContable();
            mlista.ForEach(obe =>
            {
                this.bteCuenta.Properties.Mask.BeepOnError = true;
                this.bteCuenta.Properties.Mask.EditMask = obe.parac_vmascara;
                this.bteCuenta.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.bteCuenta.Properties.Mask.ShowPlaceHolders = false;
                this.bteCuenta.Properties.Mask.UseMaskAsDisplayFormat = true;
            });
        }

        void form2_MiEvento()
        {
            cargar();
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

        private void SetSave()
        {
            {
                BaseEdit oBase = null;
                Boolean Flag = true;
                EConceptoCaja oBe = new EConceptoCaja();
                Obl = new BTesoreria();
                try
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        var VerCodigo = oDetail.Where(oB => oB.ccod_concep_mov.ToUpper() == txtCod.Text.ToUpper()).ToList();
                        if (VerCodigo.Count > 0)
                        {
                            oBase = txtCod;
                            throw new ArgumentException("El código ingresado ya existe");
                        }

                    }
                    if (string.IsNullOrEmpty(txtCod.Text))
                    {
                        oBase = txtCod;
                        throw new ArgumentException("Ingrese código");
                    }

                    if (string.IsNullOrEmpty(txtDescripcion.Text))
                    {
                        oBase = txtDescripcion;
                        throw new ArgumentException("Ingrese descripción");
                    }
                    if (bteTipoDoc.Tag != null)
                    {
                        //if (bteClaseDoc.Tag == null)
                        //{
                        //    oBase = bteClaseDoc;
                        //    throw new ArgumentException("Seleccione Clase de Documento");
                        //}
                    }

                    if (bteTipoDoc.Tag == null && bteCuenta.Tag == null)
                    {
                        oBase = bteTipoDoc;
                        throw new ArgumentException("Debe seleccionar un Tipo de Documento y/o una Cuenta Contable");
                    }


                    //else if (bteCuenta.Enabled == true)
                    //{
                    //    if (bteCuenta.Tag == null)
                    //    {
                    //        int cuenta = Convert.ToInt32(bteCuenta.Text.Replace(".",""));
                    //        if (mlistCuenta.Where(x => x.ctacc_icod_cuenta_contable == cuenta).ToList().Count != 1)
                    //        {
                    //            oBase = bteCuenta;
                    //            throw new ArgumentException("Ingrese o seleccione una cuenta contable válida ");
                    //        }
                    //    }
                    //}

                    int? ValNull;
                    ValNull = null;
                    oBe.icod_concepto_caja = 0;
                    oBe.ccod_concep_mov = txtCod.Text;
                    oBe.vdescripcion = txtDescripcion.Text;
                    oBe.tdocc_icod_tipo_doc = Convert.ToInt32(bteTipoDoc.Tag);
                    oBe.iid_correlativo = (bteClaseDoc.Tag != null) ? Convert.ToInt32(bteClaseDoc.Tag) : ValNull;
                    oBe.iid_cuenta_contable = Convert.ToInt64(bteCuenta.Tag);
                    oBe.iid_situacion_cuenta = 1;
                    oBe.cmvcc_icod_tipo = Convert.ToInt32(lkpTipo.EditValue);
                    oBe.iusuario_crea = Valores.intUsuario;
                    oBe.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();

                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        Obl.InsertarCajaEfectivo(oBe);
                    }
                    else
                    {
                        oBe.iusuario_modifica = Valores.intUsuario;
                        oBe.vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                        oBe.icod_concepto_caja = Correlative;
                        Obl.ActualizarCajaEfectivo(oBe);
                    }
                }
                catch (Exception ex)
                {
                    if (oBase != null)
                    {
                        oBase.Focus();
                        oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                        oBase.ErrorText = ex.Message;
                        oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                    XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }
        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }
        private void BtnCancelar1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void btnCtaContable_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCuenta();
        }
        private void ListarCuenta()
        {
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                frm.flagSeleccionImpresion = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteCuenta.Text = frm._Be.ctacc_numero_cuenta_contable;
                    bteCuenta.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    txtCuentaDes.Text = frm._Be.ctacc_nombre_descripcion;
                    //bteTipoDoc.Enabled = false;
                }
            }
        }
        private void clearcta()
        {
            txtCuentaDes.Text = string.Empty;
            bteCuenta.Tag = null;
        }
        private void bteCuenta_KeyUp(object sender, KeyEventArgs e)
        {
            if (bteCuenta.Text == "")
            {
                clearcta();
                //bteTipoDoc.Enabled = true;
                return;
            }
            //bteTipoDoc.Enabled = false;
            aux = mlistCuenta.Where(x => x.ctacc_icod_cuenta_contable == Convert.ToInt32(bteCuenta.Text.Replace(".", ""))).ToList();

            if (aux.Count == 1)
            {
                bteCuenta.Tag = aux[0].ctacc_icod_cuenta_contable;
                txtCuentaDes.Text = aux[0].ctacc_nombre_descripcion;
            }
            else
            {
                clearcta();
            }
        }

        private void bteTipoDoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarTipoDoc();
        }
        private void listarTipoDoc()
        {
            using (frmListarTipoDocumento frm = new frmListarTipoDocumento())
            {
                frm.intIdModulo = Parametros.intModuloTesoreria;
                frm.bModuloall = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteTipoDoc.Text = frm._Be.tdocc_vabreviatura_tipo_doc;
                    bteTipoDoc.Tag = frm._Be.tdocc_icod_tipo_doc;
                    bteClaseDoc.Enabled = true;
                    //bteCuenta.Enabled = false;
                }
            }
        }

        private void bteClaseDoc_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarClaseDoc();
        }
        private void listarClaseDoc()
        {
            using (frmListarClaseDocumento frm = new frmListarClaseDocumento())
            {
                frm.intTipoDoc = Convert.ToInt32(bteTipoDoc.Tag);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteClaseDoc.Text = String.Format("{0:00}", frm._Be.tdocd_iid_codigo_doc_det.ToString());
                    bteClaseDoc.Tag = frm._Be.tdocd_iid_correlativo;
                }
            }
        }

        private void bteTipoDoc_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarTipoDoc();
        }

        private void bteClaseDoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarClaseDoc();
        }


    }
}