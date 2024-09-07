using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Ventas;
using SGE.WindowForms.Ventas.Reporte;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Consultas_de_Ventas
{
    public partial class frm02ConsultaNotaCredito : DevExpress.XtraEditors.XtraForm
    {
        List<ENotaCredito> lstNotaCredito = new List<ENotaCredito>();
        DateTime f1, f2;
        public frm02ConsultaNotaCredito()
        {
            InitializeComponent();
        }



        private void cargar()
        {


            BaseEdit oBase = null;
            try
            {
                f1 = (DateTime)dteFechaDesde.EditValue;
                f2 = (DateTime)dteFechaHasta.EditValue;
                if (f1.Year != Parametros.intEjercicio)
                {
                    oBase = dteFechaDesde;
                    throw new ArgumentException("La fecha no esta dentro el año de ejercicio");
                }

                if (f2.Year != Parametros.intEjercicio)
                {
                    oBase = dteFechaHasta;
                    throw new ArgumentException("La fecha no esta dentro el año de ejercicio");
                }

                if (Convert.ToDateTime(f2.ToShortDateString()) < Convert.ToDateTime(f1.ToShortDateString()))
                {
                    oBase = dteFechaHasta;
                    throw new ArgumentException("La fecha inicial no puede ser mayor que fecha final");
                }


                lstNotaCredito = new BVentas().ConsultaNotaCreditoClienteCab(Parametros.intEjercicio, f1, f2, Convert.ToInt32(bteCliente.Tag));
                grdNotaCredito.DataSource = lstNotaCredito;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstNotaCredito.FindIndex(x => x.ncrec_icod_credito == intIcod);
            viewNotaCredito.FocusedRowHandle = index;
            viewNotaCredito.Focus();
        }

        private void nuevo()
        {
            decimal nmonto_tipoCambio = new BContabilidad().getTipoCambioPorFecha(DateTime.Now);
            if (nmonto_tipoCambio != 0)
            {
                FrmManteNotaCredito frm = new FrmManteNotaCredito();
                frm.MiEvento += new FrmManteNotaCredito.DelegadoMensaje(reload);
                frm.lstCabeceras = lstNotaCredito;
                frm.TDComercial = 686;
                frm.SetInsert();
                frm.Show();
                //frm.lkpMoneda.EditValue = 4;//DOLARES
                //frm.txtSerie.Text = "001";
            }
            else
            {
                XtraMessageBox.Show("No existe el Tipo de Cambio a la Fecha, Ingrese el Tipo Cambio para seguir con este proceso", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificar()
        {
            ENotaCredito oBe = (ENotaCredito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                if (oBe.ncrec_iid_situacion_credito != Parametros.intSitDocCobrarGenerado)
                    throw new ArgumentException(String.Format("La N/C no puede ser modificada, su situación es {0}", oBe.strSituacion));

                FrmManteNotaCredito frm = new FrmManteNotaCredito();
                frm.MiEvento += new FrmManteNotaCredito.DelegadoMensaje(reload);
                frm.oBe = oBe;
                frm.SetModify();
                frm.Show();
                frm.setValues();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminar()
        {
            ENotaCredito oBe = (ENotaCredito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
            if (oBe == null)
                return;
            int index = viewNotaCredito.FocusedRowHandle;
            try
            {
                if (oBe.ncrec_iid_situacion_credito != Parametros.intSitDocCobrarGenerado)
                    throw new ArgumentException(String.Format("La N/C no puede ser eliminada, su situación es {0}", oBe.strSituacion));

                if (XtraMessageBox.Show(String.Format("¿Está seguro que desea eliminar la NC {0}?", oBe.ncrec_vnumero_credito), "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    oBe.intUsuario = Valores.intUsuario;
                    oBe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BVentas().eliminarNotaCreditoClienteCab(oBe);
                    /**/
                    reload(oBe.ncrec_icod_credito);
                    /***********************************************/
                    if (lstNotaCredito.Count >= index + 1)
                        viewNotaCredito.FocusedRowHandle = index;
                    else
                        viewNotaCredito.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ver()
        {
            ENotaCredito oBe = (ENotaCredito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                //frmManteFacturaCompra frm = new frmManteFacturaCompra();
                //frm.MiEvento += new frmManteFacturaCompra.DelegadoMensaje(reload);
                //frm.Obe = obe;
                //frm.SetCancel();
                //frm.Show();
                //frm.setValues();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }


        private void viewDocCompra_DoubleClick(object sender, EventArgs e)
        {
            ver();
        }


        private void frm11NotaCredito_Load(object sender, EventArgs e)
        {
            setFechas();
        }
        private void setFechas()
        {
            if (Parametros.intEjercicio == DateTime.Now.Year)
            {
                dteFechaDesde.EditValue = Convert.ToDateTime("01/01/" + Parametros.intEjercicio.ToString());
                dteFechaHasta.EditValue = DateTime.Now;
            }
            else
            {
                dteFechaDesde.EditValue = Convert.ToDateTime("01/01/" + Parametros.intEjercicio.ToString());
                dteFechaHasta.EditValue = Convert.ToDateTime("31/12/" + Parametros.intEjercicio.ToString());
            }
        }


        private void imprimirFacturaElectronicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ENotaCredito ObeFC = (ENotaCredito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
            List<EFacturaVentaElectronica> lstFE = new List<EFacturaVentaElectronica>();
            EFacturaVentaElectronica Obe = new EFacturaVentaElectronica();
            List<EFacturaVentaDetalleElectronico> mlisdet = new List<EFacturaVentaDetalleElectronico>();
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();
            List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
            lstFE = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == ObeFC.ncrec_icod_credito && x.tipoDocumento == "07").ToList();
            Obe = lstFE.FirstOrDefault();

            mlisdet = new BVentas().listarfacturaVentaElectronicaDetalle(Obe.IdCabecera);
            mlisdet.ForEach(x =>
            {
                x.ValorUnitarioItem = x.MontoAfectoImpuestoIgv / x.cantidad;
            });


            rptNotaCreditoElectronico rptFcatura = new rptNotaCreditoElectronico();

            if (Obe.tipoDocumento == "07")
            {
                List<ENotaCredito> lstFV = new BVentas().listarNotaCreditoClienteCab(Parametros.intEjercicio).Where(x => x.ncrec_icod_credito == Obe.doc_icod_documento).ToList();
                if (lstFV.Count > 0)
                {
                    Obe.FechaReferencia = ObeFC.ncrec_sfecha_documento.ToString("dd/MM/yyyy");
                    Obe.DetalleDes = lstFV[0].ncrec_vdetalle;

                }

                rptFcatura.cargar(Obe, mlisdet);
                rptFcatura.ShowPreview();
            }
        }
        public int[] icodTalla = new int[10];
       
        

        private void bteCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCliente();
        }
        private void listarCliente()
        {
            try
            {
                using (FrmListarCliente frm = new FrmListarCliente())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteCliente.Tag = frm._Be.cliec_icod_cliente;
                        bteCliente.Text = frm._Be.cliec_vnombre_cliente;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                {
                    grdNotaCredito.DataSource = lstNotaCredito;
                    string fileName = sfdRuta.FileName;
                    if (!fileName.Contains(".xlsx"))
                    {
                        grdNotaCredito.ExportToXlsx(fileName + ".xlsx");
                        System.Diagnostics.Process.Start(fileName + ".xlsx");
                    }
                    else
                    {
                        grdNotaCredito.ExportToXlsx(fileName);
                        System.Diagnostics.Process.Start(fileName);
                    }
                    sfdRuta.FileName = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cargar();
        }
    }
}