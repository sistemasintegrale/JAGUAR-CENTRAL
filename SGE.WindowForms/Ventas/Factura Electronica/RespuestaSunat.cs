using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Factura_Electronica
{
    public partial class RespuestaSunat : Form
    {
        public List<string> ListaMensajeRespuesta = new List<string>();
        public RespuestaSunat()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RespuestaSunat_Load(object sender, EventArgs e)
        {
            foreach (var item in ListaMensajeRespuesta)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    textMensaje.AppendText($"- {item.Split(';')[0].ToString()}: {item.Split(';')[1].ToString()}\n");
                }
            }
        }
    }
}
