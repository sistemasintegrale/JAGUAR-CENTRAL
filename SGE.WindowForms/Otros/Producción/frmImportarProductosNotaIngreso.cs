using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace SGE.WindowForms.Otros.Produccion
{
    public partial class frmImportarProductosNotaIngreso : DevExpress.XtraEditors.XtraForm
    {
        public List<EProdInventarioFisicoDetalle> ListaImportacion = new List<EProdInventarioFisicoDetalle>();
        List<EImportarProducto> listaProducto = new List<EImportarProducto>();
        List<EImportarProducto> listaProductoNoExiste = new List<EImportarProducto>();


        public frmImportarProductosNotaIngreso()
        {
            InitializeComponent();
        }

        private void frmImportarProductosNotaIngreso_Load(object sender, EventArgs e)
        {

        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            listaProducto.Clear();
            dgrMotonave.DataSource = null;


            OpenFileDialog dialogo = new OpenFileDialog();
            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                StreamReader objReader = new StreamReader(dialogo.FileName);
                string Extension = Path.GetExtension(dialogo.FileName);
                if (Extension == ".txt" || Extension == ".TXT")
                {
                    string sLine = "";
                    ArrayList arrText = new ArrayList();

                    ArrayList resultadoCodigos = new ArrayList();
                    ArrayList cantidades = new ArrayList();

                    while (sLine != null)
                    {
                        sLine = objReader.ReadLine();
                        if (sLine != null)
                        {
                            //sLine = sLine.Substring(0, 15);
                            arrText.Add(sLine);
                        }
                    }
                    objReader.Close();


                    int ind = 0;
                    foreach (string codigos in arrText)
                    {
                        int cont = 0;
                        foreach (string codigosauxiliares in arrText)
                        {
                            if (resultadoCodigos.Contains(codigos) == false)
                            {
                                if (codigos.Trim() == codigosauxiliares.Trim())
                                {
                                    arrText.Remove(ind);
                                    cont++;

                                }
                            }
                        }
                        resultadoCodigos.Add(codigos);
                        if (cont != 0)
                        {
                            cantidades.Add(cont);
                        }
                        ind++;
                    }
                    object[] cant = cantidades.ToArray();

                    foreach (string sOutput in arrText)
                    {


                        EImportarProducto obj = new EImportarProducto();
                        if (sOutput.Length == 15)
                        {
                            obj.codigo_externo = sOutput.Trim();

                            //auxproducto = mlist.Where(ob => ob.pr_vcodigo_externo.Trim() == sOutput.Trim()).ToList();
                            var auxproducto = new BAlmacen().ProductoPvtGetByCode(sOutput.Trim());
                            if (listaProducto.Where(jo => jo.codigo_externo.Trim() == sOutput.Trim()).LongCount() < 1)
                            {
                                if (auxproducto != null)
                                {
                                    obj.producto_descripcion = auxproducto.pr_vdescripcion_producto;
                                    obj.icod_producto = auxproducto.pr_icod_producto;
                                    //obj.cantidad = auxproducto[0].stocc_nstock_prod;

                                    obj.estado = true;
                                    listaProducto.Add(obj);
                                }
                                else
                                {
                                    obj.estado = false;
                                    listaProductoNoExiste.Add(obj);
                                }

                            }
                        }
                        else
                        {
                            obj.codigo_externo = sOutput.Trim();
                            listaProductoNoExiste.Add(obj);
                        }
                    }
                    int j = 0;
                    foreach (EImportarProducto obj in listaProducto)
                    {
                        obj.cantidad = Convert.ToInt32(cant[j]);
                        j++;
                    }
                }
                else
                {
                    XtraMessageBox.Show("La Extensión del Archivo no es correcto", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            dgrMotonave.DataSource = listaProducto;
            gridNoexisten.DataSource = listaProductoNoExiste;
        }
        private class EImportarProducto
        {
            public string codigo_externo { get; set; }
            public int icod_producto { get; set; }
            public string producto_descripcion { get; set; }
            public string Unidad_medida { get; set; }
            public bool estado { get; set; }
            public int cantidad { get; set; }

            public EImportarProducto()
            {

            }
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (EImportarProducto obj in listaProducto)
            {
                EProdInventarioFisicoDetalle objInvFisico = new EProdInventarioFisicoDetalle();
                if (obj.estado == true)
                {
                    objInvFisico.pr_vcodigo_externo = obj.codigo_externo;
                    objInvFisico.pr_icod_producto = obj.icod_producto;
                    objInvFisico.pr_vdescripcion_producto = obj.producto_descripcion;
                    objInvFisico.infid_ncant_fisica = 0;
                    objInvFisico.infid_ncant_fisica = obj.cantidad;
                    objInvFisico.operacion = 1;
                    ListaImportacion.Add(objInvFisico);
                }
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}