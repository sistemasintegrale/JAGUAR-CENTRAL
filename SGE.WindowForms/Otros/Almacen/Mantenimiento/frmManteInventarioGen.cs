using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmManteInventarioGen : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteNotaIngreso));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        /*----------------------------------------------------*/
        public EInventarioCab oBe = new EInventarioCab();
        /*----------------------------------------------------*/
        List<EInventarioDet> lstDetalle = new List<EInventarioDet>();
        List<EInventarioDet> lstDelete = new List<EInventarioDet>();
        /*----------------------------------------------------*/
        public List<ENotaIngreso> lstCabecerasNI = new List<ENotaIngreso>();
        int cont = 0;
        /*----------------------------------------------------*/
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
        public frmManteInventarioGen()
        {
            InitializeComponent();
        }

        private void FrmManteNotaIngreso_Load(object sender, EventArgs e)
        {
            Carga();
            BSControls.LoaderLook(lkpTipoInventario, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoInventario), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaSituacionInventario), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            if (Parametros.intEjercicio == DateTime.Now.Year)
                dteFecha.EditValue = DateTime.Now;
            else
                dteFecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);

        }

        private void Carga()
        {
            if (Status != BSMaintenanceStatus.CreateNew)
            {
                lstDetalle = new BAlmacen().listarInventarioFisicoDet(oBe.invc_icod_inventario);
                cont = lstDetalle.Max(x => x.invd_inro_item);
            }
            grdDetalle.DataSource = lstDetalle;
        }

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtObservaciones.Enabled = !Enabled;
            //lkpTipoInventario.Enabled = !Enabled;            
            dteFecha.Enabled = !Enabled;
            mnu.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;

        }

        public void setValues()
        {
            txtCorrelativo.Text = oBe.invc_iid_correlativo.ToString();
            lkpTipoInventario.EditValue = oBe.tablc_iid_tipo_inventario;
            lkpSituacion.EditValue = oBe.tablc_iid_situacion;
            dteFecha.EditValue = oBe.invc_sfecha_inventario;
            txtObservaciones.Text = oBe.invc_vobservaciones;
            bteAlmacen.Tag = oBe.almac_icod_almacen;
            bteAlmacen.Text = oBe.strAlmacen;
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

        public void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                {
                    oBase = bteAlmacen;
                    throw new ArgumentException("Seleccione el almacén");
                }

                if (lstDetalle.Count == 0)
                {
                    throw new ArgumentException("No se ha generado una lista para el inventario");
                }
                if (Convert.ToDateTime(dteFecha.EditValue).Year != Parametros.intEjercicio)
                {
                    oBase = dteFecha;
                    throw new ArgumentException("La fecha seleccionada no esta dentro del año de ejercicio" + Parametros.intEjercicio);
                }

                /*---------------------------------------------------------*/
                oBe.invc_iid_correlativo = Convert.ToInt32(txtCorrelativo.Text);
                oBe.almac_icod_almacen = Convert.ToInt32(bteAlmacen.Tag);
                oBe.invc_sfecha_inventario = Convert.ToDateTime(dteFecha.EditValue);
                oBe.tablc_iid_situacion = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.tablc_iid_tipo_inventario = Convert.ToInt32(lkpTipoInventario.EditValue);
                oBe.invc_vobservaciones = txtObservaciones.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                /*---------------------------------------------------------*/

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.invc_icod_inventario = new BAlmacen().insertarInventarioFisico(oBe, lstDetalle);
                }
                else
                {
                    new BAlmacen().modificarInventarioFisico(oBe, lstDetalle, lstDelete, 1);
                }
                /*-------------------------------------------------*/
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
                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(oBe.invc_icod_inventario);
                    imprimir();
                    this.Close();
                }
            }
        }
        private void imprimir()
        {
            //if (XtraMessageBox.Show("¿Desea imprimir la nota de ingreso?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            //    return;
            //var lstDetalle = new BAlmacen().listarNotaIngresoDetalle(oBe.ningc_icod_nota_ingreso);
            //rptNotaIngreso rpt = new rptNotaIngreso();
            //rpt.cargar(String.Format("NOTA DE INGRESO N° {0}", oBe.ningc_numero_nota_ingreso), oBe.strAlmacen, lstDetalle, oBe);

        }




        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }
        private void Imprimir()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            //ENotaIngreso oBe = new ENotaIngreso();
            //Obl = new BNotaIngreso();
            try
            {

            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Flag = false;
                }
            }
            finally

            {
                if (Flag)
                {

                    //this.MiEvento();
                    this.Close();
                }
            }

        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void generarListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var lstProductos = new BAlmacen().listarProducto(Parametros.intEjercicio).Where(x => x.prdc_isituacion).ToList();

            foreach (EProducto x in lstProductos)
            {

                EInventarioDet obe = new EInventarioDet();
                obe.prdc_icod_producto = x.prdc_icod_producto;
                obe.invd_sis_stock = 0;
                obe.invd_icantidad = 0;
                obe.strCodProducto = x.prdc_vcode_producto;
                obe.strDesProducto = x.prdc_vdescripcion_larga;
                obe.strCategoria = x.strCategoria;
                obe.strSubCategoriaUno = x.strSubCategoriaUno;
                obe.strSubCategoriaDos = x.strSubCategoriaDos;
                obe.strEditorial = x.strEditorial;
                obe.strUnidadMedida = x.StrUnidadMedida;
                obe.intUsuario = Valores.intUsuario;
                obe.strPc = WindowsIdentity.GetCurrent().Name;
                obe.intOperacion = 2;

                bool flagExists = lstDetalle.Any(cus => cus.prdc_icod_producto == obe.prdc_icod_producto);

                if (!flagExists)
                {
                    cont += 1;
                    obe.intOperacion = 1;
                    obe.invd_inro_item = cont;
                    lstDetalle.Add(obe);
                }
            }

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                if (lstProductos.Count < lstDetalle.Count)
                {
                    foreach (EInventarioDet item in lstDetalle)
                    {
                        bool flagExists = lstProductos.Any(cus => cus.prdc_icod_producto == item.prdc_icod_producto);
                        if (!flagExists)
                            lstDelete.Add(item);
                        if (item.intOperacion != 1)
                            item.intOperacion = 2;
                    }

                    foreach (EInventarioDet item in lstDelete)
                    {
                        lstDetalle.Remove(item);
                    }
                }

                reEnumerar();
            }
            grdDetalle.DataSource = lstDetalle;
            viewDetalle.RefreshData();
        }

        private void reEnumerar()
        {
            int cont2 = 0;
            lstDetalle.ForEach(x =>
            {
                cont2 += 1;
                x.invd_inro_item = cont2;
            });
        }

        private void borrarListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstDelete = lstDetalle;
            lstDetalle.Clear();
            viewDetalle.RefreshData();
        }

        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
        }

        private void listarAlmacen()
        {
            try
            {
                frmListarAlmacen frm = new frmListarAlmacen();

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteAlmacen.Tag = frm._Be.almac_icod_almacen;
                    bteAlmacen.Text = frm._Be.almac_vdescripcion;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListarProductoInv frm = new frmListarProductoInv();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                frm.lstProductos.Where(x => x.flag_select).ToList().ForEach(x =>
                {

                    EInventarioDet obe = new EInventarioDet();

                    //obe.prdc_icod_producto = x.prdc_icod_producto;
                    //obe.invd_sis_stock = 0;
                    //obe.invd_icantidad = 0;
                    //obe.strCodProducto = x.prdc_vcode_producto;
                    //obe.strDesProducto = x.prdc_vdescripcion_larga;
                    //obe.strFamilia = x.strDesFamilia;
                    //obe.strSubFamilia = x.strDesSubFamilia;
                    //obe.strUnidadMedida = x.strUnidadMedida;
                    //obe.intUsuario = Valores.intUsuario;
                    //obe.strPc = WindowsIdentity.GetCurrent().Name;
                    //if (lstDetalle.Where(d => d.prdc_icod_producto == obe.prdc_icod_producto).ToList().Count > 0)
                    //{ }
                    //else
                    //{
                    //    cont += 1;
                    //    obe.invd_inro_item = cont;
                    //    lstDetalle.Add(obe);
                    //}
                });
            }

            grdDetalle.DataSource = lstDetalle;
            viewDetalle.RefreshData();
        }

        private void lkpTipoInventario_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpTipoInventario.ItemIndex == 0)
            {

                generarListaToolStripMenuItem.Enabled = true;
                nuevoItemPorMarcaToolStripMenuItem.Enabled = false;
            }
            else
            {
                generarListaToolStripMenuItem.Enabled = false;
                nuevoItemPorMarcaToolStripMenuItem.Enabled = true;
            }

        }

        private void borrarÍtemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EInventarioDet Obe = (EInventarioDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (Obe == null)
                return;
            lstDelete.Add(Obe);
            lstDetalle.Remove(Obe);
            reEnumerar();
        }

        private void nuevoItemPorMarcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmListarProductoMarcaModelo frm = new frmListarProductoMarcaModelo();

            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    frm.lstProductos.Where(x => x.flag_select).ToList().ForEach(x =>
            //    {

            //        //EInventarioDet obe = new EInventarioDet();

            //        //obe.prdc_icod_producto = x.prdc_icod_producto;
            //        //obe.invd_sis_stock = 0;
            //        //obe.invd_icantidad = 0;
            //        //obe.strCodProducto = x.prdc_vcode_producto;
            //        //obe.strDesProducto = x.prdc_vdescripcion_larga;
            //        //obe.strFamilia = x.strDesFamilia;
            //        //obe.strSubFamilia = x.strDesSubFamilia;
            //        //obe.strUnidadMedida = x.strUnidadMedida;
            //        //obe.marc_vdescripcion = x.strMarca;
            //        //obe.modc_vdescripcion = x.strModelo;
            //        //obe.intUsuario = Valores.intUsuario;
            //        //obe.strPc = WindowsIdentity.GetCurrent().Name;
            //        //if (lstDetalle.Where(d => d.prdc_icod_producto == obe.prdc_icod_producto).ToList().Count > 0)
            //        //{ }
            //        //else
            //        //{
            //        //    cont += 1;
            //        //    obe.invd_inro_item = cont;
            //        //    lstDetalle.Add(obe);
            //        //}
            //    });
            //}

            grdDetalle.DataSource = lstDetalle;
            viewDetalle.RefreshData();
        }

    }
}