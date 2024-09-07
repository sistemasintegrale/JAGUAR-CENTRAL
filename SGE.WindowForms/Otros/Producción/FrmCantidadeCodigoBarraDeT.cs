using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class FrmCantidadeCodigoBarraDeT : DevExpress.XtraEditors.XtraForm
    {
        private BSMaintenanceStatus mStatus;
        public EProdCodigoBarraDetalle objCodigoBarra = new EProdCodigoBarraDetalle();
        public List<EProdProducto> oProducto;

        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;

            }
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            // clearControl();
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }
        public FrmCantidadeCodigoBarraDeT()
        {
            InitializeComponent();
        }

        private TextEdit[] GetTextoCantidad()
        {
            TextEdit[] texto = new TextEdit[] { txtcantidad1, txtcantidad2, txtcantidad3, txtcantidad4,
            txtcantidad5, txtcantidad6, txtcantidad7, txtcantidad8, txtcantidad9, txtcantidad10};
            return texto;
        }

        private TextEdit[] GetTextoTalla()
        {
            TextEdit[] texto = new TextEdit[] { txttalla1, txttalla2, txttalla3, txttalla4,
            txttalla5, txttalla6, txttalla7, txttalla8, txttalla9, txttalla10};
            return texto;
        }

        private void FrmCantidadeCodigoBarraDeT_Load(object sender, EventArgs e)
        {
            int cont = 0;
            int i = 0;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {

                int[] Cantidad = new int[10];
                int[] Talla = new int[10];
                TextEdit[] textoCantidad = GetTextoCantidad();
                TextEdit[] textoTalla = GetTextoTalla();
                Cantidad[0] = objCodigoBarra.picbd_cant_talla1;
                Cantidad[1] = objCodigoBarra.picbd_cant_talla2;
                Cantidad[2] = objCodigoBarra.picbd_cant_talla3;
                Cantidad[3] = objCodigoBarra.picbd_cant_talla4;
                Cantidad[4] = objCodigoBarra.picbd_cant_talla5;
                Cantidad[5] = objCodigoBarra.picbd_cant_talla6;
                Cantidad[6] = objCodigoBarra.picbd_cant_talla7;
                Cantidad[7] = objCodigoBarra.picbd_cant_talla8;
                Cantidad[8] = objCodigoBarra.picbd_cant_talla9;
                Cantidad[9] = objCodigoBarra.picbd_cant_talla10;
                Talla[0] = objCodigoBarra.picbd_talla1;
                Talla[1] = objCodigoBarra.picbd_talla2;
                Talla[2] = objCodigoBarra.picbd_talla3;
                Talla[3] = objCodigoBarra.picbd_talla4;
                Talla[4] = objCodigoBarra.picbd_talla5;
                Talla[5] = objCodigoBarra.picbd_talla6;
                Talla[6] = objCodigoBarra.picbd_talla7;
                Talla[7] = objCodigoBarra.picbd_talla8;
                Talla[8] = objCodigoBarra.picbd_talla9;
                Talla[9] = objCodigoBarra.picbd_talla10;
                for (int p = 0; p < 10; p++)
                {
                    textoCantidad[p].Text = Cantidad[p].ToString();
                    textoTalla[p].Text = Talla[p].ToString();
                    string codigoexterno = objCodigoBarra.pr_vcodigo_externo + string.Format("{0:00}", Talla[p]).ToString();
                    oProducto = new BCentral().VerificarProdProducto(codigoexterno);
                    if (oProducto.Count > 0)
                        textoCantidad[p].Enabled = true;
                    else
                        textoCantidad[p].Enabled = false;
                    cont = cont + Cantidad[p];
                }
                if (cont == 0)
                {
                    TextEdit[] textCantidad = GetTextoCantidad();
                    TextEdit[] textTalla = GetTextoTalla();
                    for (int x = Convert.ToInt32(objCodigoBarra.picbd_rango_talla.Substring(0, 2)); x <= Convert.ToInt32(objCodigoBarra.picbd_rango_talla.Substring(3, 2)); x++)
                    {

                        textTalla[i].Text = string.Format("{0:00}", x).ToString();
                        textCantidad[i].Text = "0";
                        string codigoexterno = objCodigoBarra.pr_vcodigo_externo + textTalla[i].Text;
                        oProducto = new BCentral().VerificarProdProducto(codigoexterno);

                        if (oProducto.Count > 0)
                            textCantidad[i].Enabled = true;
                        else
                            textCantidad[i].Enabled = false;
                        i++;
                    }
                }
            }

            if (Status == BSMaintenanceStatus.CreateNew)
            {
                TextEdit[] textoCantidad = GetTextoCantidad();
                TextEdit[] textoTalla = GetTextoTalla();
                for (int x = Convert.ToInt32(objCodigoBarra.picbd_rango_talla.Substring(0, 2)); x <= Convert.ToInt32(objCodigoBarra.picbd_rango_talla.Substring(3, 2)); x++)
                {

                    textoTalla[i].Text = string.Format("{0:00}", x).ToString();
                    textoCantidad[i].Text = "0";
                    string codigoexterno = objCodigoBarra.pr_vcodigo_externo + textoTalla[i].Text;
                    oProducto = new BCentral().VerificarProdProducto(codigoexterno);

                    if (oProducto.Count > 0)
                        textoCantidad[i].Enabled = true;
                    else
                        textoCantidad[i].Enabled = false;
                    i++;
                }
            }
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            objCodigoBarra.picbd_talla1 = Convert.ToInt32(txttalla1.Text);
            objCodigoBarra.picbd_talla2 = Convert.ToInt32(txttalla2.Text);
            objCodigoBarra.picbd_talla3 = Convert.ToInt32(txttalla3.Text);
            objCodigoBarra.picbd_talla4 = Convert.ToInt32(txttalla4.Text);
            objCodigoBarra.picbd_talla5 = Convert.ToInt32(txttalla5.Text);
            objCodigoBarra.picbd_talla6 = Convert.ToInt32(txttalla6.Text);
            objCodigoBarra.picbd_talla7 = Convert.ToInt32(txttalla7.Text);
            objCodigoBarra.picbd_talla8 = Convert.ToInt32(txttalla8.Text);
            objCodigoBarra.picbd_talla9 = Convert.ToInt32(txttalla9.Text);
            objCodigoBarra.picbd_talla10 = Convert.ToInt32(txttalla10.Text);
            objCodigoBarra.picbd_cant_talla1 = Convert.ToInt32(txtcantidad1.Text);
            objCodigoBarra.picbd_cant_talla2 = Convert.ToInt32(txtcantidad2.Text);
            objCodigoBarra.picbd_cant_talla3 = Convert.ToInt32(txtcantidad3.Text);
            objCodigoBarra.picbd_cant_talla4 = Convert.ToInt32(txtcantidad4.Text);
            objCodigoBarra.picbd_cant_talla5 = Convert.ToInt32(txtcantidad5.Text);
            objCodigoBarra.picbd_cant_talla6 = Convert.ToInt32(txtcantidad6.Text);
            objCodigoBarra.picbd_cant_talla7 = Convert.ToInt32(txtcantidad7.Text);
            objCodigoBarra.picbd_cant_talla8 = Convert.ToInt32(txtcantidad8.Text);
            objCodigoBarra.picbd_cant_talla9 = Convert.ToInt32(txtcantidad9.Text);
            objCodigoBarra.picbd_cant_talla10 = Convert.ToInt32(txtcantidad10.Text);
            objCodigoBarra.iusuario = Valores.intUsuario;
            objCodigoBarra.picbd_iactivo = 1;

            if (Status == BSMaintenanceStatus.CreateNew)
                objCodigoBarra.operacion = 1;
            else if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                if (objCodigoBarra.operacion != 1)
                {
                    objCodigoBarra.operacion = 2;
                }
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}