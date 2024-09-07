using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.WindowForms.Otros.Planillas;

namespace SGE.WindowForms.Planillas.Registro_de_Datos
{
    public partial class FrmParametrosPlanilla : DevExpress.XtraEditors.XtraForm
    {
        public FrmParametrosPlanilla()
        {
            InitializeComponent();
        }

        private void rptFrm01ParametroContable_Load(object sender, EventArgs e)
        {
            FrmParametrosPlanilla2 fmr = new FrmParametrosPlanilla2();
            fmr.MiEvento += new FrmParametrosPlanilla2.DelegadoMensaje(Cerrar);
            if (fmr.ShowDialog() == DialogResult.OK)
            {
                this.Close();
            }
        }
        void Cerrar(int Intdx)
        {
            //this.Close();
        }
        private void rptFrm01ParametroContable_MouseMove(object sender, MouseEventArgs e)
        {
            //this.Close();
            //FrmParametrosPlanilla2 frm = new FrmParametrosPlanilla2();
            //frm.Show();           
        }

        private void rptFrm01ParametroContable_MouseClick(object sender, MouseEventArgs e)
        {
            //this.Close();
            //FrmParametrosPlanilla2 frm = new FrmParametrosPlanilla2();
            //frm.Show();           
        }
    }
}