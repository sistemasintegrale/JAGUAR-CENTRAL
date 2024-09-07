using DevExpress.Utils;
using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace SGE.WindowForms.UI
{
    public partial class FrmSystemLogin : DevExpress.XtraEditors.XtraForm
    {
        private List<EUsuario> mlist = new List<EUsuario>();
        private AppearanceDefault AppStyle;
        public bool cerrar = false;

        public FrmSystemLogin()
        {
            InitializeComponent();
            StyleTheme();
            AppStyle = new AppearanceDefault(Color.Black, Color.FromArgb(252, 235, 199),
                                             Color.Empty, Color.FromArgb(247, 199, 98),
                                             LinearGradientMode.Vertical);
        }

        private void FrmSystemLogin_Load(object sender, EventArgs e)
        {
            //this.dlfSkin.LookAndFeel.SkinName = "Dark Side";
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();
            cargaUsuarioHistorial();
            cargar();
            //txtPassword.Text = "rogola2012";
        }
        private void StyleTheme()
        {
            DevExpress.Skins.SkinManager.EnableFormSkins();
        }
        private void cargaUsuarioHistorial()
        {
            string[] Valores = null;
            try
            {
                string ruta = "C:\\SGIUSER\\user.txt";
                if (System.IO.File.Exists(ruta))
                {
                    Valores = leerArchivoLocal(ruta);
                    txtUser.Text = Valores[0];
                    //txtPassword.Text = Valores[1];
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }

        }
        private void grabarUsuarioHistorial()
        {
            try
            {
                string[] Valores = new string[2];
                string ruta = "C:\\SGIUSER\\user.txt";
                string rutaActualizador = "C:\\SGIUSER\\userUpdate.txt";
                if (System.IO.File.Exists(ruta))
                {
                    eliminarArchivoLocal(ruta);
                }

                if (System.IO.File.Exists(rutaActualizador))
                {
                    eliminarArchivoLocal(rutaActualizador);
                }

                Valores[0] = txtUser.Text;
                Valores[1] = UserCoDec.codec(txtPassword.Text);
                crearArchivoLocal(Valores);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            verificarActualizacion();
        }
        private ControlVersiones objVersion = new ControlVersiones();
        private ControlEquipos objEquipo = new ControlEquipos();
        void verificarActualizacion()
        {

            string strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = new IPHostEntry();
            ipEntry = Dns.GetHostEntry(strHostName);
            Valores.strNombreEquipo = Convert.ToString(ipEntry.HostName);
            Valores.strIdCpu = GetICPU.get();

            objEquipo = new BAdministracionSistema().Equipo_Obtner_Datos(Valores.strNombreEquipo, Valores.strIdCpu);
            if (objEquipo.cep_bflag_acceso == false)
            {
                XtraMessageBox.Show($"El Equipo {Valores.strNombreEquipo} no tiene premiso para acceder al sistema", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (objEquipo.ceq_icod_equipo == 0)
                    new BGeneral().Equipo_Ingresar(Valores.strNombreEquipo, Valores.strIdCpu);
                Application.Exit();
                cerrar = true;
            }

            else
            {
                objVersion = new BAdministracionSistema().Listar_Versiones().OrderByDescending(x => x.cvr_sfecha_version).FirstOrDefault();
                Valores.strUbicacionActualizador = objEquipo.cep_vubicacion_actualizador;
                if (objVersion.cvr_icod_version != objEquipo.cvr_icod_version) //actualizar
                {
                    if (!string.IsNullOrWhiteSpace(objEquipo.cep_vubicacion_actualizador))
                    {

                        Process.Start(objEquipo.cep_vubicacion_actualizador);
                        WaitForm1 waitForm1 = new WaitForm1();
                        waitForm1.Show();
                        Thread.Sleep(3000);
                        Application.Exit();
                        cerrar = true;
                    }
                    else
                    {
                        eliminarArchivoLocal("C:\\SGIUSER\\userUpdate.txt");
                    }

                }
                else
                {
                    eliminarArchivoLocal("C:\\SGIUSER\\userUpdate.txt");
                }
            }

        }
        public void crearArchivoLocal(string[] datos)
        {
            if (Directory.Exists(@"C:\SGIUSER") == false)
            {
                Directory.CreateDirectory(@"C:\SGIUSER");
            }


            StreamWriter fs = File.CreateText("C:\\SGIUSER\\user.txt");
            for (int i = 0; i < datos.Length; i++)
            {
                fs.Write(datos[i] + ",");
            }
            fs.Close();

            StreamWriter fs2 = File.CreateText("C:\\SGIUSER\\userUpdate.txt");
            for (int i = 0; i < datos.Length; i++)
            {
                fs2.Write(datos[i] + ",");
            }
            fs2.Write("7" + ","); //CONNECION 
            fs2.Close();
        }
        private string[] leerArchivoLocal(string ruta)
        {
            string Linea;
            string[] Valores = null;
            if (File.Exists(ruta))
            {
                using (StreamReader lector = new StreamReader(ruta))
                {
                    Linea = lector.ReadLine();
                    Valores = Linea.Split(",".ToCharArray());
                }
            }
            return Valores;
        }
        public void eliminarArchivoLocal(string ruta)
        {
            if (System.IO.File.Exists(ruta))
            {
                try
                {
                    System.IO.File.Delete(ruta);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
            }
        }

        public void cargar()
        {
            mlist = new BAdministracionSistema().listarUsuarios();
            var lstEjercicio = new BContabilidad().listarAnioEjercicio();
            BSControls.LoaderLook(lkpAnio, lstEjercicio, "anioc_iid_anio_ejercicio", "anioc_iid_anio_ejercicio", true);
            if (lstEjercicio.Where(x => x.anioc_iid_anio_ejercicio == DateTime.Now.Year).ToList().Count == 1)
                lkpAnio.EditValue = DateTime.Now.Year;

        }

        private void clearControl()
        {
            txtUser.Text = "";
            txtPassword.Text = "";
        }

        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pre_login();
            }
            if (e.KeyCode == Keys.Escape)
            {
                //XtraMessageBox.Show("no puede salir");
                this.Close();
            }
        }
        private void pre_login()
        {
            string mensaje = "ok";
            BaseEdit oBase = null;
            try
            {

                if (txtUser.Enabled == true)
                {
                    if (string.IsNullOrEmpty(txtUser.Text))
                    {
                        oBase = lkpAnio;
                        throw new ArgumentException("Ingresar Usuario");
                    }
                    if (string.IsNullOrEmpty(txtPassword.Text))
                    {
                        oBase = lkpAnio;
                        throw new ArgumentException("Ingresar Password");
                    }
                    switch (User_Verification(txtUser.Text, txtPassword.Text))
                    {

                        case 0:
                            if (!cerrar)
                            {
                                clearControl();
                                this.Hide();
                                frmMain main = new frmMain();
                                main.Show();
                            }
                            break;
                        case 1:
                            oBase = lkpAnio;
                            mensaje = "Contraseña Incorrecta";
                            break;
                        case 2:
                            oBase = lkpAnio;
                            mensaje = "Nombre de usuario no existe";
                            break;
                    }
                }
                else
                {
                    this.Hide();
                    frmMain main = new frmMain();
                    main.Show();
                }
                if (mensaje != "ok")
                    throw new ArgumentException(mensaje);
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        int usua_flag;
        public int User_Verification(string usua_usuario, string usua_pass)
        {
            int result;
            // 0 datos correctos
            // 1 password incorrecto
            // 2 usuario incorrecto
            usua_flag = mlist.FindIndex(x => x.usua_codigo_usuario == usua_usuario);

            if (usua_flag >= 0)
            {
                if (mlist[usua_flag].usua_password_usuario == SGE.WindowForms.Modules.UserCoDec.codec(usua_pass))
                {
                    Valores.intUsuario = mlist[usua_flag].usua_icod_usuario;
                    Valores.strUsuario = mlist[usua_flag].usua_codigo_usuario;
                    Parametros.intEjercicio = Convert.ToInt32(lkpAnio.Text);
                    Parametros.intEjercicio = Convert.ToInt32(lkpAnio.Text);
                    /*--------------------------------------*/
                    var lstParametro = new BAdministracionSistema().listarParametro();
                    Valores.strNombreEmpresa = lstParametro[0].pm_nombre_empresa;
                    Valores.strRUC = lstParametro[0].pm_vruc;
                    Valores.strDireccionFiscal = lstParametro[0].pm_direccion_empresa;
                    /*--------------------------------------*/
                    grabarUsuarioHistorial();
                    result = 0;
                }
                else
                {
                    result = 1;
                }
            }
            else
                result = 2;
            return result;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            pre_login();
        }

        private void FrmSystemLogin_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

    }
}