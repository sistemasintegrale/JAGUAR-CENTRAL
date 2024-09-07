using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmListarDirecion : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        private List<ETablaRegistro> lstCliente = new List<ETablaRegistro>();
        public ETablaRegistro _Be { get; set; }
        public int cliec_icod_cliente;

        public FrmListarDirecion()
        {
            InitializeComponent();
        }


        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void DgAcept()
        {
            _Be = (ETablaRegistro)viewCliente.GetRow(viewCliente.FocusedRowHandle);
            if (_Be != null)
                this.DialogResult = DialogResult.OK;
        }

        private void BuscarCriterio()
        {
            string desc = txtDireccion.Text;
            grd.DataSource = lstCliente.Where(obe => (obe.tarec_vdescripcion.ToUpper().Contains(desc.ToUpper()))).ToList();
        }

        private void FrmListarCliente_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstCliente = new BGeneral().listarTablaRegistro(cliec_icod_cliente);
            grd.DataSource = lstCliente;
            viewCliente.Focus();
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstCliente.FindIndex(x => x.tablc_iid_tipo_tabla == intIcod);
            viewCliente.FocusedRowHandle = index;
            viewCliente.Focus();
        }

        private void btnsalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DgAcept();
        }

        private void btnPrev_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (viewCliente.FocusedRowHandle == 0)
                viewCliente.MoveLast();
            else
                viewCliente.MovePrev();
        }

        private void btnNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (viewCliente.FocusedRowHandle == viewCliente.RowCount - 1)
                viewCliente.MoveFirst();
            else
                viewCliente.MoveNext();
        }

        private void txtCodigo_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtRazon_EditValueChanged(object sender, EventArgs e)
        {
            if (txtDireccion.ContainsFocus)
            {
                BuscarCriterio();
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmManteDireccionCliente Cliente = new FrmManteDireccionCliente();
            //Cliente.MiEvento += new FrmManteDireccionCliente.DelegadoMensaje(reload);
            //int i = 0;
            //if (lstCliente.Count > 0)
            //    i = lstCliente.Max(ob => ob.cliec_icod_cliente);

            //Cliente.Correlative = Convert.ToInt32(i) + 1;
            //Cliente.oDetail = lstCliente;
            //Cliente.cliec_icod_cliente = cliec_icod_cliente;
            //Cliente.Show();
            //Cliente.SetInsert();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstCliente.Count > 0)
            {
                datos();

            }
        }

        private void datos()
        {
            //EDireccionCliente Obe = (EDireccionCliente)viewCliente.GetRow(viewCliente.FocusedRowHandle);
            //if (Obe == null)
            //    return;
            //FrmManteDireccionCliente Cliente = new FrmManteDireccionCliente();
            //Cliente.MiEvento += new FrmManteDireccionCliente.DelegadoMensaje(reload);
            //Cliente.oDetail = lstCliente;
            //Cliente.Show();
            //Cliente.Correlative = Obe.cliec_icod_cliente;
            //Cliente.txtDireccion.Text = Obe.dc_vdireccion;
            //Cliente.SetModify();
            //xposition = viewCliente.FocusedRowHandle;

        }

    }
}