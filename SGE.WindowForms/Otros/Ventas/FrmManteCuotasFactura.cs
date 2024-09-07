using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmManteCuotasFactura : DevExpress.XtraEditors.XtraForm
    {
        public EFacturaCab eFacturaCab = new EFacturaCab();
        public List<ECuotasFactura> listaCuotas = new List<ECuotasFactura>();
        public List<ECuotasFactura> listaCuotasEliminar = new List<ECuotasFactura>();

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
        public void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {

            }
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            //setValues();
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
            SetCancel();
        }
        public FrmManteCuotasFactura()
        {
            InitializeComponent();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteCuota frm = new FrmManteCuota();
            frm.spinNro.EditValue = listaCuotas.Count == 0 ? 1 : listaCuotas.Max(x => x.fcc_inro_cuota) + 1;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                listaCuotas.Add(frm.eCuotasFactura);
                grdLista.DataSource = listaCuotas;
                grdLista.RefreshDataSource();
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ECuotasFactura cuota = (ECuotasFactura)viewLista.GetRow(viewLista.FocusedRowHandle);
            FrmManteCuota frm = new FrmManteCuota();
            frm.eCuotasFactura = cuota;
            frm.EFacturaCab = eFacturaCab;
            frm.SetModify();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                cuota = frm.eCuotasFactura;
                grdLista.DataSource = listaCuotas;
                grdLista.RefreshDataSource();
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ECuotasFactura cuota = (ECuotasFactura)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (XtraMessageBox.Show("Está Seguro de Eliminar la Cuota?", "Información del Sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                return;
            listaCuotas.Remove(cuota);
            listaCuotasEliminar.Add(cuota);
            Numerar();
            grdLista.DataSource = listaCuotas;
            grdLista.RefreshDataSource();
        }

        private void Numerar()
        {
            List<ECuotasFactura> lista = new List<ECuotasFactura>();
            int index = 1;
            foreach (var item in listaCuotas)
            {
                item.fcc_inro_cuota = index++;
                lista.Add(item);
            }
            listaCuotas = lista;
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void FrmManteCuotasFactura_Load(object sender, EventArgs e)
        {

            BSControls.LoaderLookRepository(lkpGrdEstado, new BGeneral().listarTablaRegistro(98), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

            grdLista.DataSource = listaCuotas;
            grdLista.RefreshDataSource();
        }
    }
}