﻿using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmListarGuiaRemisionSuministrosVarios : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        public List<EGuiaRemisionCompras> lstGRM = new List<EGuiaRemisionCompras>();
        public List<EGuiaRemisionCompras> lstGRSelecionados = new List<EGuiaRemisionCompras>();
        public EGuiaRemisionCompras _Be { get; set; }
        public bool bNoExceptuadosRetencion = false;
        public int proc_icod_proveedor = 0;
        public bool flag_multiple = false;

        #endregion

        #region "Eventos"

        public FrmListarGuiaRemisionSuministrosVarios()
        {
            InitializeComponent();
        }

        private void FrmListarProveedor_Load(object sender, EventArgs e)
        {

            Carga();

            txtnumeroOC.Focus();
        }

        #endregion

        #region "Metodos"

        public void Carga()
        {
            lstGRM = new BCompras().listarGuiaRemisionCompras(Parametros.intEjercicio).Where(_be => _be.proc_icod_proveedor == proc_icod_proveedor).ToList();
            lstGRM.ForEach(x =>
            {
                x.flag_multiple = false;
            });
            grdManoObra.DataSource = lstGRM;
        }



        private void BuscarCriterio()
        {
            string cod = txtnumeroOC.Text, desc = txtProveedor.Text;
            grdManoObra.DataSource = lstGRM.Where(obe => ((cod != string.Empty) ? obe.grcc_vnumero_grc.Contains(cod) : obe.NomProveedor.Contains(desc))).ToList();

        }

        private void viewProveedor_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void DgAcept()
        {
            _Be = (EGuiaRemisionCompras)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            if (flag_multiple)
            {
                viewManoObra.MoveLast();
                viewManoObra.MoveFirst();
            }
            if (_Be != null)
                this.DialogResult = DialogResult.OK;


        }
        #endregion

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DgAcept();
        }


        private void txtRUC_EditValueChanged(object sender, EventArgs e)
        {
            if (txtnumeroOC.ContainsFocus)
            {
                txtProveedor.Text = string.Empty;
                BuscarCriterio();
            }
        }

        private void txtRazonSocial_EditValueChanged(object sender, EventArgs e)
        {
            if (txtProveedor.ContainsFocus)
            {
                txtnumeroOC.Text = string.Empty;
                BuscarCriterio();
            }
        }

        private void viewManoObra_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }
    }
}


