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

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmManteKardexImportacion : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteKardexImportacion));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public int codImp = 0;
        public string impc_vnumero_importacion = "";
        public int impc_icod_importacion = 0;
        EFacturaCompra Obe = new EFacturaCompra();
        List<EImportacionFactura> lstImpFactura = new List<EImportacionFactura>();
        List<EFacturaCompraDet> lstImpFacturaDet = new List<EFacturaCompraDet>();
        public frmManteKardexImportacion()
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
            //bool Enabled = (Status == BSMaintenanceStatus.View);


            //if (Status == BSMaintenanceStatus.ModifyCurrent) ;



            //    if (Status == BSMaintenanceStatus.CreateNew) ;



        }
        public void setValues()

        {


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
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                /*----------------------*/
                //if (String.IsNullOrEmpty(txtAbreviatura.Text))
                //{
                //    oBase = txtAbreviatura;
                //    throw new ArgumentException("Ingrese código de Línea");
                //}              
                //if (verificarAbreviaturaFamilia(txtAbreviatura.Text))
                //{
                //    oBase = txtAbreviatura;
                //    throw new ArgumentException("El código ingresado ya existe en los registros de Línea");
                //}
                ///*----------------------*/
                //if (String.IsNullOrEmpty(txtDescripcion.Text))
                //{
                //    oBase = txtDescripcion;
                //    throw new ArgumentException("Ingrese descripción de Familia");
                //}
                //if (verificarDescripcionFamilia(txtDescripcion.Text))
                //{
                //    oBase = txtDescripcion;
                //    throw new ArgumentException("La descripción ingresada ya existe en los registros de Línea");
                //}

                int? intNullVal = null;
                Obe.impc_icod_importacion = impc_icod_importacion;
                Obe.fcoc_icod_doc = Convert.ToInt32(lstImpFactura[0].fcoc_icod_doc);
                Obe.almac_icod_almacen = Convert.ToInt32(bteAlmacen.Tag);
                Obe.fcoc_sfecha_doc = Convert.ToDateTime(dtFecha.EditValue);
                Obe.strProveedor = lstImpFactura[0].proc_vnombrecompleto;
                Obe.fcoc_num_doc = lstImpFactura[0].fcoc_num_doc;
                Obe.impc_vnumero_importacion = impc_vnumero_importacion;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;



                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.fcoc_icod_doc = new BCompras().insertarKardexFactImportacion(Obe, lstImpFacturaDet);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    //new BAlmacen().modificarFamiliaCab(Obe);
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
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    //this.MiEvento(Obe.fcoc_icod_doc);
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

        private void frmManteFamiliaCab_Load(object sender, EventArgs e)
        {
            setFecha(dtFecha);
        }
        private void listarAlmacen()
        {
            using (frmListarAlmacen frm = new frmListarAlmacen())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteAlmacen.Tag = frm._Be.almac_icod_almacen;
                    bteAlmacen.Text = frm._Be.almac_vdescripcion;
                }
            }
        }

        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
        }
        private void setFecha(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
        }
        public void AlmacenPrincipal()
        {
            List<EAlmacen> lstAlamcen = new List<EAlmacen>();
            lstAlamcen = new BAlmacen().listarAlmacenes().Where(x => x.almac_icod_almacen == 53).ToList();
            bteAlmacen.Text = lstAlamcen[0].almac_vdescripcion;
            bteAlmacen.Tag = lstAlamcen[0].almac_icod_almacen;
        }
        public void Cargar()
        {
            List<EFacturaCompraDet> detalle = new List<EFacturaCompraDet>();
            BCompras detalle2 = new BCompras();
            lstImpFactura = new BCompras().ListarImportacionFactura(codImp);
            lstImpFactura.ForEach(x =>
            {

                detalle = detalle2.listarFacCompraDet(Convert.ToInt32(x.fcoc_icod_doc));
                lstImpFacturaDet.AddRange(detalle);
            });

        }

    }
}