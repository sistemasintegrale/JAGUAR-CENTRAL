using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Ventas;
using SGE.Entity;
using SGE.BusinessLogic;
using DevExpress.XtraGrid.Views.Grid;
using System.Linq;

namespace SGE.WindowForms.Almacén.Registro_de_Datos 
{
    public partial class Frm10GuiaRemision : DevExpress.XtraEditors.XtraForm
    {
        List<EGuiaRemision> lstFacturas = new List<EGuiaRemision>();
        public Frm10GuiaRemision()
        {
            InitializeComponent();
        }

        private void cargar()
        {
             try
            {
            lstFacturas = new BVentas().listarGuiaRemision(Parametros.intEjercicio);
            grdGuiaRemision.DataSource = lstFacturas;
            }
             catch (Exception ex)
             {
                 XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }  
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstFacturas.FindIndex(x => x.remic_icod_remision == intIcod);
            viewGuiaRemision.FocusedRowHandle = index;
            viewGuiaRemision.Focus();
        }

        private void nuevo()
        {
            frmManteGuiaRemision frm = new frmManteGuiaRemision();
            frm.MiEvento += new frmManteGuiaRemision.DelegadoMensaje(reload);
            frm.SetInsert();
            frm.CargarControles();
            frm.AlmacenPrincipal();
            frm.Show();
            frm.dteFechaTranslado.EditValue = DateTime.Now;
            frm.txtSerie.Text = "001";
            frm.lkpSituacion.EditValue = 218;
            frm.lkpMotivo.EditValue = 226;
            frm.btnAlmacen.Tag = 53;
            frm.txtDestinatario.Text = "SERVIPARAMO PERU S.A.C";
            frm.txtRUC.Text = "20543468755";
         
            //frm.btnCliente.Enabled = true;
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void Frm04Factura_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void modificar()
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                EGuiaRemision _BEGuia = new EGuiaRemision();
                _BEGuia = new BVentas().listarGuiaRemisionxID(obe.remic_icod_remision);
                if (_BEGuia.tablc_iid_situacion_remision != 218)
                    throw new ArgumentException(String.Format("La Guía de Remisión no puede ser Modificado, su situación es ", _BEGuia.StrSitucion));
                //if (verificarDocVentaPlanilla(obe.favc_icod_factura))
                //    throw new ArgumentException("La factura ha sido generada desde una Planilla de Venta, no puede ser modificada");

                frmManteGuiaRemision frm = new frmManteGuiaRemision();
                frm.MiEvento += new frmManteGuiaRemision.DelegadoMensaje(reload);
                frm.oBe = obe;
                frm.CargarControles();
                frm.SetModify();
                frm.btnCliente.Enabled = false;
                frm.btnAlmacenIngreso.Enabled = false;
                frm.Show();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private bool verificarDocVentaPlanilla(int intIcodDoc)
        {
            bool flagExiste = false;
            var intFlag = new BVentas().verificarDocVentaPlanilla(Parametros.intTipoDocFacturaVenta, intIcodDoc);
            if (intFlag > 0)
                flagExiste = true;          

            return flagExiste;
 
        }
               

     

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

       
        private void txtNumero_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void filtrar()
        {
            grdGuiaRemision.DataSource = lstFacturas.Where(x => x.remic_vnumero_remision.Trim().Contains(txtNumero.Text.Trim()) &&
                x.remic_vnombre_destinatario.ToUpper().Trim().Contains(txtCliente.Text.Trim().ToUpper())).ToList();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;



            List<EUbicacion> lUbicacion = new BVentas().ListarUbicacion();
            int? tipo = null;
            int? Dios = null;
            int? padre = null;
            int? abuelo = null;
            int? bisabuelo = null;

            string pais;
            string Departamento = "";
            string Provincia = "";
            string Distrito = "";
            lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion).ToList().ForEach(oB =>
            {
                tipo = oB.tablc_iid_tipo_ubicacion;
                padre = oB.ubicc_icod_ubicacion_padre;
            });
            switch (tipo)
            {
                case 4:
                    Dios = obe.ubicc_icod_ubicacion;
                    break;
                case 3:
                    foreach (var _BBE in lUbicacion)
                    {
                        if (_BBE.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion)
                        {
                            Departamento = _BBE.ubicc_vnombre_ubicacion;
                        }
                    }

                    break;
                case 2:
                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == padre).ToList().ForEach(oB =>
                    {
                        abuelo = oB.ubicc_icod_ubicacion_padre;
                        Departamento = oB.ubicc_vnombre_ubicacion;
                    });
                    foreach (var _BBE in lUbicacion)
                    {
                        if (_BBE.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion)
                        {
                            Provincia = _BBE.ubicc_vnombre_ubicacion;
                        }
                    }
                    break;
                case 1:
                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == padre).ToList().ForEach(oB =>
                    {
                        abuelo = oB.ubicc_icod_ubicacion_padre;
                        Provincia = oB.ubicc_vnombre_ubicacion;
                    });

                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == abuelo).ToList().ForEach(oB =>
                    {
                        bisabuelo = oB.ubicc_icod_ubicacion_padre;
                        Departamento = oB.ubicc_vnombre_ubicacion;
                    });
                    foreach (var _BBE in lUbicacion)
                    {
                        if (_BBE.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion)
                        {
                            Distrito = _BBE.ubicc_vnombre_ubicacion;
                        }
                    }
                    break;

            }
            var lstdet = new BVentas().listarGuiaRemisionDet(obe.remic_icod_remision,Parametros.intEjercicio);


            List<EGuiaRemisionDet> mlistDetalle = new List<EGuiaRemisionDet>();
            int cont = 1;
            foreach (var _BE in lstdet)
            {
                mlistDetalle.Add(_BE);

                string[] partes = partes = _BE.dremc_vobservaciones.Split('@');
                int cont_estapacios = 0;
                for (int i = 0; i < partes.Length; i++)
                {
                    if (partes[i] == "")
                    {
                        cont_estapacios = cont_estapacios + 1;
                    }
                }
                if (cont_estapacios != partes.Length)
                {
                    for (int i = 0; i < partes.Length; i++)
                    {
                        if (i == 0)
                        {
                            EGuiaRemisionDet __be = new EGuiaRemisionDet();
                            __be.strDesProducto = "" + partes[i];
                            __be.OrdenItemImprimir = cont + 1;
                            cont++;
                            mlistDetalle.Add(__be);
                        }
                        else
                        {
                            if (partes[i] != "")
                            {
                                EGuiaRemisionDet __be = new EGuiaRemisionDet();
                                __be.strDesProducto = "" + partes[i];
                                __be.OrdenItemImprimir = cont + 1;
                                cont++;
                                mlistDetalle.Add(__be);
                            }
                        }
                    }
                }

            }
            Boolean Ventas = false;
            Boolean Compras = false;
            Boolean Transformacion = false;
            Boolean Consignacion = false;
            Boolean Devolucion = false;
            Boolean Transferencia = false;
            Boolean TransladoEmision = false;
            Boolean Otros = false;

            if (obe.tablc_iid_motivo == 221)
            {
                Ventas = true;
            }
            else if (obe.tablc_iid_motivo == 222)
            {
                Compras = true;
            }
            else if (obe.tablc_iid_motivo == 223)
            {
                Transformacion = true;
            }
            else if (obe.tablc_iid_motivo == 224)
            {
                Consignacion = true;
            }
            else if (obe.tablc_iid_motivo == 225)
            {
                Devolucion = true;
            }
            else if (obe.tablc_iid_motivo == 226)
            {
                Transferencia = true;
            }
            else if (obe.tablc_iid_motivo == 227)
            {
                TransladoEmision = true;
            }
            else if (obe.tablc_iid_motivo == 228)
            {
                Otros = true;
            }

            rptGuiaRemision rpt = new rptGuiaRemision();
            rpt.cargar(obe, mlistDetalle,"","","" ,Ventas, Compras,Transformacion,Consignacion,Devolucion,Transferencia,TransladoEmision,Otros);
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar(); 
        }

        private void eliminar()
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                EGuiaRemision _BEguiaConstatar = new BVentas().listarGuiaRemisionxID(obe.remic_icod_remision);
                if (_BEguiaConstatar.tablc_iid_situacion_remision != 218)
                {
                    throw new ArgumentException(String.Format("La Guia de Remisión no puede ser eliminada, su situación es {0}", _BEguiaConstatar.StrSitucion));
                    int xPosition = viewGuiaRemision.FocusedRowHandle;
                    cargar();
                    viewGuiaRemision.FocusedRowHandle = xPosition;
                }
                //if (verificarDocVentaPlanilla(obe.favc_icod_factura))
                //    throw new ArgumentException("La factura ha sido generada desde una Planilla de Venta, no puede ser eliminada");
                if (XtraMessageBox.Show("¿Esta seguro que desea ELIMINAR la Guía de Remisión?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    List<EGuiaRemisionDet> lstDetalle = new List<EGuiaRemisionDet>();
                    lstDetalle = new BVentas().listarGuiaRemisionDet(obe.remic_icod_remision, Parametros.intEjercicio);
                    new BVentas().eliminarGuiaRemision(obe, lstDetalle);
                    reload(obe.remic_icod_remision);

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            int xPosition = viewGuiaRemision.FocusedRowHandle;
            cargar();
            viewGuiaRemision.FocusedRowHandle = xPosition;

        }

        private void viewGuiaRemision_DoubleClick(object sender, EventArgs e)
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
               
                frmManteGuiaRemision frm = new frmManteGuiaRemision();
                frm.MiEvento += new frmManteGuiaRemision.DelegadoMensaje(reload);
                frm.oBe = obe;
                frm.SetModify();
                frm.SetCancel();
                frm.Show();
                frm.btnGuardar.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }

        private void actualizarToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            List<EGuiaRemision> Mlist = new List<EGuiaRemision>();
            foreach (var _BEE in lstFacturas)
            {
                List<EGuiaRemisionDet> lstDetalle = new List<EGuiaRemisionDet>();
                lstDetalle = new BVentas().listarGuiaRemisionDet(_BEE.remic_icod_remision, Parametros.intEjercicio);
                if (lstDetalle.Sum(og => og.kardc_icod_correlativo) == 0)
                {
                    Mlist.Add(_BEE);
                }
            }
            foreach (var _CE in Mlist)
            {
                List<EGuiaRemisionDet> lstDetalle = new List<EGuiaRemisionDet>();
                List<EGuiaRemisionDet> lstDetalleEliminar = new List<EGuiaRemisionDet>();
                lstDetalle = new BVentas().listarGuiaRemisionDet(_CE.remic_icod_remision, Parametros.intEjercicio);
                foreach (var _cc in lstDetalle)
                {
                    _cc.intTipoOperacion = 2;
                }

                new BVentas().modificarGuiaRemision(_CE, lstDetalle, lstDetalleEliminar);
            }
           
        }

        private void anularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Anular();
        }
        private void Anular()
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                   EGuiaRemision _BEguiaConstatar = new BVentas().listarGuiaRemisionxID(obe.remic_icod_remision);
                if (_BEguiaConstatar.tablc_iid_situacion_remision != 218)
                {
                    throw new ArgumentException(String.Format("La Guia de Remisión no puede ser eliminada, su situación es {0}", _BEguiaConstatar.StrSitucion));
                    int xPosition = viewGuiaRemision.FocusedRowHandle;
                    cargar();
                    viewGuiaRemision.FocusedRowHandle = xPosition;
                }
                //if (verificarDocVentaPlanilla(obe.favc_icod_factura))
                //    throw new ArgumentException("La factura ha sido generada desde una Planilla de Venta, no puede ser eliminada");
                if (XtraMessageBox.Show("¿Esta seguro que desea ELIMINAR la Guía de Remisión?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    List<EGuiaRemisionDet> lstDetalle = new List<EGuiaRemisionDet>();
                    lstDetalle = new BVentas().listarGuiaRemisionDet(obe.remic_icod_remision, Parametros.intEjercicio);
                    new BVentas().anularGuiaRemision(obe, lstDetalle);
                    reload(obe.remic_icod_remision);

                }
            }
            catch (Exception ex)
            {
                
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void viewGuiaRemision_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string strSituacion = View.GetRowCellDisplayText(e.RowHandle, View.Columns["StrSitucion"]);
                if (strSituacion == "ANULADO")
                {
                    e.Appearance.BackColor = Color.LightSalmon;
                    //e.Appearance.BackColor2 = Color.SeaShell;

                }
            }
        }
    }
}