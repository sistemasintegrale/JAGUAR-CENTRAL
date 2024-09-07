using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.Entity.FacturaElectronica;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmManteResumenDocumentos : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteResumenDocumentos));


        List<ESunatResumenDocumentosDet> mlisResumenDocumentosDet = new List<ESunatResumenDocumentosDet>();
        public ESunatResumenDocumentosCab Obe { get; internal set; }
        private int xposition = 0;
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
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
        #region "Metodos"

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            //lkpAlmacen.Enabled = !Enabled;
        }

        private void clearControl()
        {
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

        void form2_MiEvento()
        {
            Carga();
        }

        void Modify()
        {
            Carga();
            viewResumenDocumentosDetalle.FocusedRowHandle = xposition;
        }
        private void Carga()
        {
            //mListaDetalle = OblAlmacen.ListarNotaIngresoDetalle(intIdNotaIngreso);
            //grdResumenDocumentosDetalle.DataSource = mListaDetalle;
            grdResumenDocumentosDetalle.RefreshDataSource();
        }



        #endregion
        public FrmManteResumenDocumentos()
        {
            InitializeComponent();
        }

        private void FrmManteNotaIngreso_Load(object sender, EventArgs e)
        {
            CargaControles();
            Carga();
        }

        private void CargaControles()
        {
            dtmFecha.EditValue = DateTime.Now;
            if (mStatus == BSMaintenanceStatus.View)
            {
            }


        }
        private List<int> PermitirTipoDocumento()
        {
            List<int> correlativo = new List<int>();
            //int Tipodocumento1 = Parametros.TipoDocumentoFAC;
            //int Tipodocumento2 = Parametros.intTipoDocumentoNI;

            //correlativo.Add(Tipodocumento1);
            //correlativo.Add(Tipodocumento2);

            return correlativo;
        }
        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }
        public void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            ESunatResumenDocumentosCab oBe = new ESunatResumenDocumentosCab();

            try
            {

                oBe.IdDocumento = "RC" + "-" + Convert.ToDateTime(dtmFecha.EditValue).ToString("yyyyMMdd") + "-" + txtNumeroNic.Text;
                //oBe.IdDocumento = "RC" + "-" + dtmFecha.DateTime.Year + dtmFecha.DateTime.Month + dtmFecha.DateTime.Day + "-" + txtNumeroNic.Text;
                oBe.FechaEmision = dtmFecha.EditValue.ToString();
                oBe.FechaGeneracion = dtmFecha.EditValue.ToString();
                oBe.NroDocumento = Valores.strRUC;
                oBe.TipoDocumento = "6";
                oBe.NombreLegal = Valores.strNombreEmpresa;
                oBe.NombreComercial = Valores.strNombreEmpresa;
                oBe.Ubigeo = "";
                oBe.Direccion = Valores.strDireccionFiscal;
                oBe.Departamento = "";
                oBe.Departamento = "";
                oBe.Provincia = "";
                oBe.Distrito = "";
                oBe.EstadoResumen = 3;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.IdCabecera = new BVentas().InsertarResumenDocumentos(oBe, mlisResumenDocumentosDet);
                }
                else
                {
                    //oBe.ningc_icod_nota_ingreso = intIdNotaIngreso;
                    //OblAlmacen.ActualizarNotaIngreso(_oBe, mListaDetalle,mlistEliminados);
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
                //txtSerie.Focus();
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(oBe.IdCabecera);
                    this.Close();
                }
            }
        }

        private void lkpAlmacen_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
            List<EFacturaVentaElectronica> lstFVE = new List<EFacturaVentaElectronica>();
            lstFVE = new BVentas().listarfacturaVentaElectronicaResumen(lstParamatro[0].pm_sfecha_inicio, Convert.ToDateTime(dtmFecha.EditValue));

            //List<EBoletaVentaCab> lstBov = new List<EBoletaVentaCab>();
            //lstBov = new BVentas().listarBoletaVenta().Where(x=> x.fbolv_sfecha_boleta == Convert.ToDateTime(dtmFecha.EditValue)).ToList();


            int count = 1;
            //var quitar = lstFVE.FirstOrDefault();
            //lstFVE.Remove(quitar);
            foreach (var item in lstFVE)
            {
                ESunatResumenDocumentosDet RDD = new ESunatResumenDocumentosDet();
                if (item.tipoDocumento == "03") //Boletas
                {
                    RDD.Id = count++;
                    RDD.TipoDocumento = item.tipoDocumento;
                    RDD.strTipoDoc = item.StrTipoDoc;
                    RDD.IdDocumento = item.idDocumento;
                    RDD.TipoDocumentoReceptor = item.tipoDocumentoReceptor;
                    RDD.NroDocumentoReceptor = item.nroDocumentoReceptor;
                    RDD.CodigoEstadoItem = 1;
                    RDD.DocumentoRelacionado = "";
                    RDD.TipoDocumentoRelacionado = "";
                    RDD.Moneda = item.moneda;
                    RDD.TotalVenta = item.TotalPrecioVenta;
                    RDD.TotalDescuentos = 0;
                    RDD.TotalIgv = item.totalIgv;
                    RDD.TotalIsc = item.totalIsc;
                    RDD.TotalOtrosImpuestos = item.MontoGravadosOtros;
                    RDD.Gravadas = item.MontoGravadasIGV;
                    RDD.Exoneradas = item.MontoExonerado;
                    RDD.Inafectas = item.MontoInafecto;
                    RDD.Exportacion = 0;
                    RDD.Gratuitas = item.MontoGratuitoImpuesto;
                    RDD.doc_icod_documento = item.doc_icod_documento;
                }
                else //Nota de Credito y Debito
                {
                    RDD.Id = count++;
                    RDD.TipoDocumento = item.tipoDocumento;
                    RDD.strTipoDoc = item.StrTipoDoc;
                    RDD.IdDocumento = item.idDocumento;
                    RDD.TipoDocumentoReceptor = item.tipoDocumentoReceptor;
                    RDD.NroDocumentoReceptor = item.nroDocumentoReceptor;
                    RDD.CodigoEstadoItem = 1;
                    RDD.DocumentoRelacionado = item.NroDocqModifica.Insert(4, "-");
                    RDD.TipoDocumentoRelacionado = item.TipoDocqModifica;
                    RDD.Moneda = item.moneda;
                    RDD.TotalVenta = item.TotalPrecioVenta;
                    RDD.TotalDescuentos = 0;
                    RDD.TotalIgv = item.totalIgv;
                    RDD.TotalIsc = item.totalIsc;
                    RDD.TotalOtrosImpuestos = item.MontoGravadosOtros;
                    RDD.Gravadas = item.MontoGravadasIGV;
                    RDD.Exoneradas = item.MontoExonerado;
                    RDD.Inafectas = item.MontoInafecto;
                    RDD.Exportacion = 0;
                    RDD.Gratuitas = item.MontoGratuitoImpuesto;
                    RDD.doc_icod_documento = item.doc_icod_documento;
                }
                mlisResumenDocumentosDet.Add(RDD);

            }
            grdResumenDocumentosDetalle.DataSource = mlisResumenDocumentosDet;
        }
        internal void SetValues()
        {
            string[] data = Obe.IdDocumento.Split('-');
            dtmFecha.EditValue = data[1];
            txtNumeroNic.Text = data[2];
            mlisResumenDocumentosDet = new BVentas().listarSunatResumenDocumentosDet(Obe.IdCabecera);
            grdResumenDocumentosDetalle.DataSource = mlisResumenDocumentosDet;
            grdResumenDocumentosDetalle.RefreshDataSource();
        }

        internal void SetView()
        {
            Status = BSMaintenanceStatus.View;
        }
    }
}
