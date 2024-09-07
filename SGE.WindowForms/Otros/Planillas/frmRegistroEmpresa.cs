using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;


namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmRegistroEmpresa : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroEmpresa));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EEmpresa Obe = new EEmpresa();
        public List<EEmpresa> LstEmpresa = new List<EEmpresa>();




        public frmRegistroEmpresa()
        {
            InitializeComponent();
        }

        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }
        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtRazonSocial.Enabled = false;
            txtRUC.Enabled = !Enabled;
            txtDireccion.Enabled = !Enabled;
            txtTelefonos.Enabled = !Enabled;
            txtRegistroPatronal.Enabled = !Enabled;
            txtRepresentateLegal.Enabled = !Enabled;
            txtPaginaWeb.Enabled = !Enabled;
            barButtonItem2.Enabled = !Enabled;


        }


        public void setValues()
        {

            txtRazonSocial.Text = Obe.cia_vrazon_social;
            txtRUC.Text = Obe.cia_vruc;
            txtDireccion.Text = Obe.cia_vdireccion_empr;
            txtTelefonos.Text = Obe.cia_vtelefonos;
            txtRegistroPatronal.Text = Obe.cia_vregistro_patronal;
            txtRepresentateLegal.Text = Obe.cia_vrepresentante_legal;
            txtPaginaWeb.Text = Obe.cia_vpagina_web;


        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        private bool verificarNombreRazonSocial(string strNombreRazonSocial)
        {
            try
            {
                bool exists = false;
                if (LstEmpresa.Count > 0)
                {

                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (LstEmpresa.Where(x => x.cia_vrazon_social.Trim() == strNombreRazonSocial.Trim() && x.cia_icod_empresa != Obe.cia_icod_empresa).ToList().Count > 0)
                            exists = true;
                    }
                }
                return exists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            //Boolean Flag = true;

            try
            {
                if (String.IsNullOrEmpty(txtRazonSocial.Text))
                {
                    oBase = txtRazonSocial;
                    throw new ArgumentException("Ingrese nombre de la Razón Social de la Empresa");
                }
                if (verificarNombreRazonSocial(txtRazonSocial.Text))
                {
                    oBase = txtRazonSocial;
                    throw new ArgumentException("El nombre ingresado ya existe en los registros de la Empresa");
                }
                if (String.IsNullOrEmpty(txtRUC.Text))
                {
                    oBase = txtRUC;
                    throw new ArgumentException("Ingrese el RUC de la Empresa");
                }
                if (String.IsNullOrEmpty(txtDireccion.Text))
                {
                    oBase = txtDireccion;
                    throw new ArgumentException("Ingrese Dirección de la Empresa");
                }
                if (String.IsNullOrEmpty(txtTelefonos.Text))
                {
                    oBase = txtTelefonos;
                    throw new ArgumentException("Ingrese Telefono de la Empresa");
                }

                if (String.IsNullOrEmpty(txtRegistroPatronal.Text))
                {
                    oBase = txtRegistroPatronal;
                    throw new ArgumentException("Ingrese el Registro Patronal de la Empresa");
                }

                if (String.IsNullOrEmpty(txtRepresentateLegal.Text))
                {
                    oBase = txtRepresentateLegal;
                    throw new ArgumentException("Ingrese el Representate Legal de la Empresa");
                }


                if (String.IsNullOrEmpty(txtPaginaWeb.Text))
                {
                    oBase = txtPaginaWeb;
                    throw new ArgumentException("Ingrese la Pagina Web de la Empresa");
                }


                Obe.cia_vrazon_social = txtRazonSocial.Text;
                Obe.cia_vruc = txtRUC.Text;
                Obe.cia_vdireccion_empr = txtDireccion.Text;
                Obe.cia_vtelefonos = txtTelefonos.Text;
                Obe.cia_vregistro_patronal = txtRegistroPatronal.Text;
                Obe.cia_vrepresentante_legal = txtRepresentateLegal.Text;
                Obe.cia_vpagina_web = txtPaginaWeb.Text;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;



                if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BPlanillas().modificarEmpresa(Obe);
                }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //Flag = false;
            }
            finally
            {
                //if (Flag)
                //{
                //    this.MiEvento(Obe.almac_icod_almacen);
                this.Close();
                //}
            }
        }


        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmRegistroEmpresa_Load(object sender, EventArgs e)
        {

        }


    }
}
