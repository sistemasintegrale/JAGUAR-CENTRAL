using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraNavBar;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Newtonsoft.Json;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace SGE.WindowForms.UI
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        List<EParametro> lstParametro = new List<EParametro>();
        private TreeListNode hotTrackNode;
        private AppearanceDefault AppStyle;
        private List<EMenu> oList;
        private Dictionary<string, XtraForm> Ins;
        public DateTime fechaInicio = DateTime.Today;
        public DateTime today = DateTime.Today;
     
        public frmMain()
        {
            InitializeComponent();
            Ins = new Dictionary<string, XtraForm>();
            StyleTheme();
            oList = new List<EMenu>();
            AppStyle = new AppearanceDefault(Color.Black, Color.FromArgb(252, 235, 199),
                                             Color.Empty, Color.FromArgb(247, 199, 98),
                                             LinearGradientMode.Vertical);
            hotTrackNode = null;
            
        }
        private void StyleTheme()
        {
            DevExpress.Skins.SkinManager.EnableFormSkins();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            lstParametro = new BAdministracionSistema().listarParametro();

            //string Version = "Version :" + ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            this.btnVersion.Caption = "Ver. 12.55.09.10";
            fillForm();
            btnUsuario.Caption = "Usuario: " + Valores.strUsuario;
            btnEjercicio.Caption = "Año de Ejercicio: " + Parametros.intEjercicio.ToString();
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();
            SkinHelper.InitSkinPopupMenu(skinsLink);
            Formulario("SGE.WindowForms.Otros.Administracion_del_Sistema.frmFondo");
            Valores.lstAccesosUsuario = new BAdministracionSistema().listarAccesosPermitidos(Valores.intUsuario);
            var lst = new BAdministracionSistema().listarParametro();
            Parametros.strPorcIGV = Convert.ToDecimal(lst[0].pm_nigv_parametro).ToString();
            Parametros.strPorcIGV = Convert.ToDecimal(lst[0].pm_nigv_parametro).ToString();

            Parametros.strPorcRenta4taCat = Convert.ToDecimal(lst[0].pm_ncategoria_parametro).ToString();
            Parametros.strPorcRenta4taCat = Convert.ToDecimal(lst[0].pm_ncategoria_parametro).ToString();

            Constantes.PorcentajeRetencion = Convert.ToDecimal(lst[0].pm_porc_retencion);


            navBarControl2.OptionsNavPane.NavPaneState = DevExpress.XtraNavBar.NavPaneState.Collapsed;

            Valores.pm_icod_parametro = lstParametro[0].pm_icod_parametro;
            Valores.longCorrelativoOR = lstParametro[0].pm_correlativo_OR;
            obtenerTipoCambioDelDia();

        }

        public class Enombre
        {
            public string modulo { get; set; }
            public string sis_name { get; set; }
            public string ui_name { get; set; }
        }

        private void Formulario(string Name)
        {

            TextEdit txt = new TextEdit();
            XtraForm oForm = new XtraForm();
            if (!Ins.TryGetValue(Name, out oForm) || oForm.IsDisposed)
            {
                Type oType = Type.GetType(Name);
                oForm = (XtraForm)Activator.CreateInstance(oType);
                Ins[Name] = oForm;
            }
            oForm.MdiParent = this;
            oForm.Show();
            Focus();
            oForm.BringToFront();
        }

        private Assembly oAss;
        private TreeList tree;
        private TreeListNode mNode;
        private TreeListNode mParent;
        private TreeListNode mChild;

        private void fillForm()
        {
            /**/
            List<Enombre> listaForms = new List<Enombre>();

            /**/
            List<EMenu> _List = new List<EMenu>();
            oAss = Assembly.GetExecutingAssembly();
            Type[] Types = oAss.GetTypes();
            Hashtable Nivel = new Hashtable();
            Hashtable SubNivel = new Hashtable();

            Array.ForEach(Types, oTp =>
            {
                if (string.Compare(oTp.BaseType.Name, "XtraForm", false) == 0 &&
                    !oTp.Namespace.Contains("SGE.WindowForms.UI") &&
                    !oTp.Namespace.Contains("Otros") &&
                    !oTp.Namespace.Contains("Reportes"))
                /*if(string.Compare(oTp.BaseType.Name, "XtraForm", false) == 0 &&
                    ((oTp.Namespace.Contains("Almacén") ||
                    oTp.Namespace.Contains("Ventas") ||
                    oTp.Namespace.Contains("Contabilidad"))
                    && !oTp.Namespace.Contains("Otros")))*/
                {


                    EMenu oBE = new EMenu { FullDescription = oTp.FullName };
                    Type oTy = oAss.GetType(oBE.FullDescription);
                    string[] oFile = oTy.Namespace.Split('.');
                    oBE.Parent = null;
                    oBE.FullNameSpace = oFile[2];
                    object obj = Activator.CreateInstance(Type.GetType(oBE.FullDescription));
                    oBE.Description = ((XtraForm)obj).Text;
                    oBE.Parent = oBE.FullNameSpace;
                    /*Esta parte es para crear el archivo excel con los nombres de los formularios*/
                    Enombre objForm = new Enombre();
                    objForm.modulo = oBE.Parent;
                    objForm.sis_name = oTp.Name;
                    objForm.ui_name = oBE.Description;
                    listaForms.Add(objForm);
                    /**/
                    _List.Add(oBE);
                }
            });
            /*Aquí se ejecuta el método para la exportación del archivo excel con los nombres de los formularios*/
            crearArchivo(listaForms);
            /**/
            var mList = _List.OrderBy(x => x.FullDescription).ToList();
            oList = mList;
            mList.ForEach(obj =>
            {
                string[] oFile = obj.FullDescription.Split('.');
                try
                {
                    Nivel.Add(oFile[2], oFile[2]);
                    NavBarGroup Nbar = new NavBarGroup();
                    Nbar.Caption = oFile[2].Replace("_", " ");
                    navBarControl1.BeginUpdate(); //Nuevo linea

                    navBarControl1.Groups.Add(Nbar);


                    if (Nbar.Caption == "Sistema")
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.settings_;
                    else if (Nbar.Caption == "Almacén")
                    {
                        Nbar.Expanded = true;
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.almacen;
                        Nbar.Caption = "Almacén Suministros";
                    }
                    else if (Nbar.Caption == "Contabilidad")
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.contabilidad;
                    else if (Nbar.Caption == "Compras")
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.ctaxpagar;
                    else if (Nbar.Caption == "Ventas")
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.ctaxcobrar;
                    else if (Nbar.Caption == "Tesorería")
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.tesoreria;
                    else if (Nbar.Caption == "Operaciones")
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.operaciones;
                    else if (Nbar.Caption == "Administración")
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.administracion_;
                    else if (Nbar.Caption == "Presupuesto")
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.notes;
                    else if (Nbar.Caption == "Planillas")
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.notes;
                    else if (Nbar.Caption == "Producción")
                    {
                        Nbar.SmallImage = SGE.WindowForms.Properties.Resources.notes;
                        Nbar.Caption = "Producción";
                    }


                    tree = new TreeList();
                    tree.CustomDrawNodeCell += Object_CustomDrawNodeCell;
                    tree.Click += trlID_Click;
                    tree.MouseMove += this.Object_MouseMove;
                    tree.Columns.Add();
                    tree.Columns[0].Visible = true;
                    tree.BorderStyle = BorderStyles.NoBorder;
                    tree.OptionsBehavior.Editable = false;
                    tree.OptionsSelection.EnableAppearanceFocusedCell = false;
                    tree.OptionsView.ShowIndicator = false;
                    tree.OptionsView.ShowColumns = false;
                    tree.OptionsView.ShowHorzLines = false;
                    tree.OptionsView.ShowVertLines = false;
                    Nbar.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
                    Nbar.ControlContainer.Controls.Add(tree);
                    tree.Dock = DockStyle.Fill;
                    navBarControl1.Width = 255;
                    mParent = null;
                    if (oFile.Length - 1 == 3)
                    {
                        mNode = tree.AppendNode(obj.Description, mParent, obj.Description);
                        mNode.SetValue(0, obj.Description);
                        mNode.SetValue(1, obj.FullNameSpace);
                    }
                    else if (oFile.Length - 1 == 4)
                    {
                        try
                        {
                            SubNivel.Add(oFile[2] + " " + oFile[3].Replace("_", " ").Replace("_", " "), oFile[2] + " " + oFile[3].Replace("_", " "));
                            mNode = tree.AppendNode(oFile[3].Replace("_", " "), mParent, oFile[3].Replace("_", " "));
                            mNode.SetValue(0, oFile[3].Replace("_", " "));
                            mNode.SetValue(1, obj.FullNameSpace);
                            mChild = tree.AppendNode(obj.Description, mNode, obj.Description);
                            mChild.SetValue(0, obj.Description);
                            mChild.SetValue(1, obj.FullNameSpace);
                        }
                        catch
                        {
                            mChild = tree.AppendNode(obj.Description, mNode, obj.Description);
                            mChild.SetValue(0, obj.Description);
                            mChild.SetValue(1, obj.FullNameSpace);
                        }
                    }
                    navBarControl1.EndUpdate();// final de agregar modulos
                }
                catch
                {
                    mParent = null;
                    if (oFile.Length - 1 == 3)
                    {
                        mNode = tree.AppendNode(obj.Description, mParent, obj.Description);
                        mNode.SetValue(0, obj.Description);
                        mNode.SetValue(1, obj.FullNameSpace);
                    }
                    else if (oFile.Length - 1 == 4)
                    {
                        try
                        {
                            SubNivel.Add(oFile[2] + " " + oFile[3].Replace("_", " "), oFile[2] + " " + oFile[3].Replace("_", " "));
                            mNode = tree.AppendNode(oFile[3].Replace("_", " "), mParent, oFile[3].Replace("_", " "));
                            mNode.SetValue(0, oFile[3].Replace("_", " "));
                            mNode.SetValue(1, obj.FullNameSpace);
                            mChild = tree.AppendNode(obj.Description, mNode, obj.Description);
                            mChild.SetValue(0, obj.Description);
                            mChild.SetValue(1, obj.FullNameSpace);
                        }
                        catch
                        {
                            mChild = tree.AppendNode(obj.Description, mNode, obj.Description);
                            mChild.SetValue(0, obj.Description);
                            mChild.SetValue(1, obj.FullNameSpace);
                        }
                    }
                }
            });

        }
        public void crearArchivo(List<Enombre> lista)
        {
            //string ruta = "C:\\CRAC\\Formularios.xlsx";
            //gridControl1.DataSource = lista.OrderBy(x => x.modulo).ToList();
            //gridControl1.ExportToXlsx(ruta);

        }
        private void Object_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            TreeListNode tl = sender as TreeListNode; // se codigo codigo
            if (!e.Node.Focused)
            {
                if (!e.Node.HasChildren)
                {
                    SolidBrush brush = GetSolidBrush(SystemColors.Window);
                    e.Graphics.FillRectangle(brush, e.Bounds);
                    SolidBrush highlightBrush = GetSolidBrush(Color.Transparent);
                    Rectangle R = new Rectangle(e.EditViewInfo.ContentRect.Left, e.EditViewInfo.ContentRect.Top,
                        Convert.ToInt32(e.Graphics.MeasureString(e.CellText, e.Appearance.Font).Width + 1), e.EditViewInfo.ContentRect.Height);
                    e.Graphics.FillRectangle(highlightBrush, R);
                    SolidBrush highlightForeBrush = GetSolidBrush(Color.Black);
                    using (StringFormat format = new StringFormat { LineAlignment = StringAlignment.Center })
                    {
                        e.Graphics.DrawString(e.CellText, e.Appearance.Font, highlightForeBrush, R, format);
                    }
                    e.Handled = true;
                }
            }
            if (e.Node == hotTrackNode && !e.Node.HasChildren)
                AppearanceHelper.Apply(e.Appearance, AppStyle);
        }
        private SolidBrush GetSolidBrush(Color systemColor)
        {
            Color color = LookAndFeelHelper.GetSystemColor(tree.LookAndFeel, systemColor);
            SolidBrush brush = new SolidBrush(color);
            return brush;
        }
        private void trlID_Click(object sender, EventArgs e)
        {
            TreeList tree = (TreeList)sender;
            Point pt = tree.PointToClient(Control.MousePosition);
            DoNodeClick(tree, pt);
        }
        private void DoNodeClick(TreeList tree, Point pt)
        {
            TreeListHitInfo info = tree.CalcHitInfo(pt);
            if (info.HitInfoType == HitInfoType.Cell)
            {
                if (tree.FocusedNode != null)
                {
                    if (!tree.FocusedNode.HasChildren)
                    {
                        object oBE = (object)tree.GetDataRecordByNode(tree.FocusedNode);
                        ArrayList oAr = (ArrayList)oBE;
                        string mName = oAr[0].ToString();
                        var Lista = oList.Where(obj => obj.Description == mName).ToList();
                        String Name = Lista[0].FullDescription;
                        string mNameSpace = Lista[0].FullNameSpace;
                        XtraForm oForm;
                        if (!Ins.TryGetValue(Name, out oForm) || oForm.IsDisposed)
                        {
                            Type oType = Type.GetType(Name);
                            oForm = (XtraForm)Activator.CreateInstance(oType);
                            Ins[Name] = oForm;
                        }
                        if (acceso(oForm.Name) == 1)
                        {
                            oForm.MdiParent = this;
                            oForm.Show();
                            Focus();
                            oForm.BringToFront();
                        }
                    }
                }

            }
        }
        public int acceso(string form)
        {
            int flag;
            var fr = Modules.Valores.lstAccesosUsuario.Where(x => x.formc_vnombre_forms == form);
            flag = Modules.Valores.lstAccesosUsuario.FindIndex(x => x.formc_vnombre_forms == form);
            if (flag >= 0)
                flag = 1;
            else
            {
                XtraMessageBox.Show("Acceso no permitido", "Datos del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = 0;
            }

            return flag;
        }
        private void Object_MouseMove(object sender, MouseEventArgs e)
        {
            TreeList tl = sender as TreeList;
            TreeListHitInfo info = tl.CalcHitInfo(new Point(e.X, e.Y));
            if ((info.HitInfoType == HitInfoType.Cell) && (info.Node != hotTrackNode))
            {
                TreeListNode tempNode = hotTrackNode;
                hotTrackNode = null;
                tl.InvalidateNode(tempNode);
                hotTrackNode = info.Node;
                tl.Cursor = Cursors.Hand;
                tl.InvalidateNode(hotTrackNode);
            }
            if (info.Node == null)
            {
                TreeListNode tempNode = hotTrackNode;
                hotTrackNode = null;
                tl.InvalidateNode(tempNode);
                hotTrackNode = info.Node;
                tl.Cursor = Cursors.Default;
                tl.InvalidateNode(hotTrackNode);
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Application.OpenForms.Count == 3)
                    Application.Exit();
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    string nombreForm = Application.OpenForms[i].ToString();
                    if (nombreForm.Contains("frmGenerarVoucher") != false)
                    {
                        throw new ArgumentException("Se esta ejecutando un proceso de generación de vouchers, no puede salir de la aplicación");
                    }
                }
                Application.Exit();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void btnGaleria_ItemClick(object sender, ItemClickEventArgs e)
        {
            var galeria = new DevExpress.XtraBars.Ribbon.GalleryDropDown();
            galeria.Manager = barManager1;
            SkinHelper.InitSkinGalleryDropDown(galeria);
            galeria.ShowPopup(MousePosition);
        }

        private void navBarControl1_Click(object sender, EventArgs e)
        {

        }

        private void logo_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        //private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    frm04Vehiculo frm = new frm04Vehiculo();
        //    if (!flagOpened(frm.Text))
        //    {
        //        frm.MdiParent = this;
        //        frm.Show();
        //        frm.Focus();
        //    }
        //}

        //private bool flagOpened(string titulo)
        //{
        //    bool babierto = false;
        //    try
        //    {
        //        foreach (DevExpress.XtraTabbedMdi.XtraMdiTabPage item in xtraTabbedMdiManager1.Pages)
        //        {
        //            babierto = titulo == item.Text;
        //            if (babierto)
        //            {
        //                xtraTabbedMdiManager1.SelectedPage = item;
        //                break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return babierto;
        //}



        public Producción.Almacenes.FrmProducto oInstance { get; set; }

        private void navBarControl2_Click(object sender, EventArgs e)
        {

        }
        public class Objconsult
        {
            public string token { get; set; }
            public tipo_cambio tipo_cambio { get; set; }

        }
        public class tipo_cambio
        {
            public string moneda { get; set; }
            public string fecha_inicio { get; set; }
            public string fecha_fin { get; set; }
        }


        public class Objesultado
        {
            public bool success { get; set; }
            public List<exchange_rate> exchange_rates { get; set; }
        }

        public class exchange_rate
        {
            public string fecha { get; set; }
            public string moneda { get; set; }
            public double compra { get; set; }
            public double venta { get; set; }
        }

        void obtenerTipoCambioDelDia()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://ruc.com.pe/api/v1/consultas");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            var consult = new Objconsult()
            {
                token = "25aedf03-3af0-4f83-bff1-8015a61cc984-ee63abbc-d83f-4e36-8a17-ff7189bdc3f9",
                tipo_cambio = new tipo_cambio()
                {
                    moneda = "PEN",
                    fecha_inicio = (fechaInicio.ToString("dd/MM/yyyy")),
                    fecha_fin = (fechaInicio.ToString("dd/MM/yyyy")),

                }

            };

            string json = JsonConvert.SerializeObject(consult);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }
            var resultado = new Objesultado();
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    resultado = JsonConvert.DeserializeObject<Objesultado>(result);
                    ETipoCambio tipoCambio = new ETipoCambio()
                    {
                        ticac_fecha_tipo_cambio = Convert.ToDateTime(today),
                        ticac_tipo_cambio_compra = Convert.ToDecimal(resultado.exchange_rates[0].compra),
                        ticac_tipo_cambio_venta = Convert.ToDecimal(resultado.exchange_rates[0].venta)
                    };

                    int ticac_icod_tipo_cambio = new BAdministracionSistema().insertarTipoCambio(tipoCambio);
                }


            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Error en el servidor remoto: (401) No autorizado."))
                {
                    return;
                }
                else
                {
                    fechaInicio = fechaInicio.AddDays(-1);
                    obtenerTipoCambioDelDia();
                }

            }
        }

    }
}