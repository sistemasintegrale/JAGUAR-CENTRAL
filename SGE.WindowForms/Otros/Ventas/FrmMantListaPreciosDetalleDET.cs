using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmMantListaPreciosDetalleDET : DevExpress.XtraEditors.XtraForm
    {
        public FrmMantListaPreciosDetalleDET()
        {
            InitializeComponent();
        }
        public int flag_ope;//0:ingresar:1:modificar
        public List<EProdListaPreciosDetalle> mlitaDetalle = new List<EProdListaPreciosDetalle>();
        public string pr_vcodigo_externo;
        public EProdListaPreciosDetalle _oBe = new EProdListaPreciosDetalle();

        List<EProdListaPreciosDetalle> listtallas = new List<EProdListaPreciosDetalle>();

        public void cargar()
        {

            listtallas = new BCentral().ListarProdProductoTallas(pr_vcodigo_externo);
            if (flag_ope == 0)
            {
                foreach (EProdListaPreciosDetalle ob in mlitaDetalle)
                {
                    int talla1, talla2;
                    string rango;
                    rango = ob.lpred_vrango_tallas;
                    talla1 = Convert.ToInt32(ob.lpred_vrango_tallas.Substring(0, 2));
                    talla2 = Convert.ToInt32(ob.lpred_vrango_tallas.Substring(3, 2));
                    for (int i = talla1; talla1 <= talla2; talla1++)
                    {
                        listtallas = listtallas.Where(li => Convert.ToInt32(li.destalla) != (talla1)).ToList();
                    }

                }
            }
            BSControls.LoaderLook(lkpserie1, listtallas, "destalla", "destalla", true);
            BSControls.LoaderLook(lkpserie2, listtallas, "destalla", "destalla", true);

        }
        private void FrmMantListaPreciosDetalleDET_Load(object sender, EventArgs e)
        {

        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void Agregar()
        {

            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if ((lkpserie1.EditValue) == null)
                {
                    oBase = lkpserie1;
                    throw new ArgumentException("Ingrese el rango de tallas");
                }
                if ((lkpserie2.EditValue) == null)
                {
                    oBase = lkpserie2;
                    throw new ArgumentException("Ingrese el rango de tallas");
                }
                if (Convert.ToDecimal(txtMinorista.Text) == 0)
                {
                    oBase = txtMinorista;
                    throw new ArgumentException("Ingrese Precio Minorista");
                }
                if (Convert.ToDecimal(txtMinorista.Text) != 0 || Convert.ToDecimal(txtMayorista.Text) != 0)
                {
                    if (Convert.ToDecimal(txtMinorista.Text) <= Convert.ToDecimal(txtMayorista.Text))
                    {
                        oBase = txtMayorista;
                        throw new ArgumentException("Precio de Mayorista es Mayor que el Precio Minorista");
                    }
                }
                if (Convert.ToDecimal(txtRem.Text) != 0)
                {
                    if (Convert.ToDecimal(txtRem.Text) >= Convert.ToDecimal(txtMinorista.Text))
                    {
                        oBase = txtRem;
                        throw new ArgumentException("Precio de Remate es Mayor que el Precio Minorista");
                    }

                }
                _oBe.lpred_vrango_tallas = lkpserie1.Text + "/" + lkpserie2.Text;
                _oBe.lpred_nprecio_vtamay = Convert.ToDecimal(txtMayorista.Text);
                _oBe.lpred_nprecio_vtamin = Convert.ToDecimal(txtMinorista.Text);
                _oBe.lpred_nprecio_vtaotros = Convert.ToDecimal(txtRem.Text);
                _oBe.lpred_iactivo = 1;
                _oBe.iusuario = Valores.intUsuario;
                this.DialogResult = DialogResult.OK;
                ////if (Status == BSMaintenanceStatus.CreateNew)
                ////    OBL.InsertarListaPrecioDetalle(oBe);
                ////else
                ////{
                ////    oBe.lpred_icod_det_precio = lpred_icod_det_precio;
                ////    OBL.ActualizarListaPrecioDetalle(oBe);
                ////}
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
                    this.Close();
                }
            }

        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Agregar();
        }

        private void lkpserie2_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpserie1.Text != "" && lkpserie2.Text != "")
            {
                if (Convert.ToInt32(lkpserie2.Text) < Convert.ToInt32(lkpserie1.Text))
                {
                    lkpserie2.EditValue = null;
                }
            }
        }

        private void lkpserie1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Modificar();
        }
        private void Modificar()
        {

            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (Convert.ToDecimal(txtMayorista.Text) == 0)
                {
                    oBase = txtMayorista;
                    throw new ArgumentException("Ingrese Precio Venta");
                }
                _oBe.lpred_nprecio_vtamay = Convert.ToDecimal(txtMayorista.Text);
                _oBe.lpred_nprecio_vtamin = Convert.ToDecimal(txtMinorista.Text);
                _oBe.lpred_nprecio_vtaotros = Convert.ToDecimal(txtRem.Text);
                _oBe.lpred_iactivo = 1;
                _oBe.iusuario = Valores.intUsuario;
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
                else
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.Close();
                }
            }

        }
    }
}