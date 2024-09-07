using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Administracion_del_Sistema.Listados
{
    public partial class frmListarTipoDocumento : DevExpress.XtraEditors.XtraForm
    {
        List<ETipoDocumento> mlist = new List<ETipoDocumento>();
        public ETipoDocumento _Be { get; set; }
        public int intIdModulo;

        public bool bModulo = false;
        public bool bModuloall = false;
        public bool bmoduloDocumento = false;
        public bool bSaldosIniciales = false;//s

        public frmListarTipoDocumento()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void returnSeleccion()
        {
            if (mlist.Count > 0)
            {
                _Be = (ETipoDocumento)viewTipoDocumento.GetRow(viewTipoDocumento.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void cargar()
        {
            if (bModulo)//permite filtrar por módulo
                mlist = new BAdministracionSistema().listarTipoDocumentoPorModulo(Parametros.intModuloCompras).Where(ob => ob.tdocc_icod_tipo_doc == 54 || ob.tdocc_icod_tipo_doc == 24).ToList();//modulo compras
            else if (bModuloall)
                mlist = new BAdministracionSistema().listarTipoDocumentoPorModulo(intIdModulo).ToList();
            else
                mlist = new BAdministracionSistema().listarTipoDocumentoPorModulo(13);

            if (bmoduloDocumento == true)
            {
                mlist = new BAdministracionSistema().listarTipoDocumentoPorModulo(2).Where(ob => ob.tdocc_icod_tipo_doc == 84 || ob.tdocc_icod_tipo_doc == 24).ToList();//modulo compras
            }

            if (intIdModulo != Parametros.intModuloCtasPorPagar)
                grdTipoDocumento.DataSource = mlist;
            else
            {
                if (!bSaldosIniciales)
                {

                    // mlist = mlist.Where(obe => obe.tdocc_icod_tipo_doc != Parametros.intTipoDocReciboPorHonorarios && obe.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoProveedor).ToList();
                    //mlist = mlist.Where(obe => obe.tdocc_icod_tipo_doc != Parametros.intTipoDocReciboPorHonorarios).ToList();
                    mlist = mlist.ToList();
                }
                else
                {
                    //mlist = mlist.Where(obe => obe.tdocc_icod_tipo_doc != Parametros.intTipoDocReciboPorHonorarios).ToList();
                    mlist = mlist.ToList();
                }
                grdTipoDocumento.DataSource = mlist;
            }
            /**/
            viewTipoDocumento.Focus();
        }

        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }

        private void buscarCriterio()
        {
            grdTipoDocumento.DataSource = mlist.Where(x =>
                                                   x.tdocc_vabreviatura_tipo_doc.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.tdocc_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            returnSeleccion();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

    }
}