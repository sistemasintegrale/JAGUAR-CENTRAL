using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class FrmManteDetalleComprobante : DevExpress.XtraEditors.XtraForm
    {

        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        List<EComprobanteDetalle> mlistDetail = new List<EComprobanteDetalle>();
        List<EComprobanteDetalle> mlistDelete = new List<EComprobanteDetalle>();
        List<ESubDiario> ListaSubdiario = new List<ESubDiario>();
        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();
        List<ECuentaContable> mlistCuenta = new List<ECuentaContable>();
        public EComprobante modiComp = null;
        public int Tipo_Moneda = 0;
        public int corigen_icod;

        public string Menus = "";
        public int Correlative;
        public int code;
        public FrmManteDetalleComprobante()
        {
            InitializeComponent();
        }

        public List<EComprobante> oDetail;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public int CodeComprobante = 0;
        public int CodeAnio = 0;
        public int CodeMes = 0;
        public int CodeSubdiario = 0;
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
            bteSubDiario.Enabled = false;
            txtdescripcion.Enabled = false;
            lkpMoneda.Enabled = false;
            DtmFecha.Enabled = false;
            mnuComprobantes.Enabled = false;
            BtnGuardar.Enabled = false;
            txttipcambio.Enabled = false;
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                bteSubDiario.Enabled = true;
                txtdescripcion.Enabled = true;
                lkpMoneda.Enabled = true;
                DtmFecha.Enabled = true;
                mnuComprobantes.Enabled = true;
                BtnGuardar.Enabled = true;
                txttipcambio.Enabled = true;
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtdescripcion.Enabled = true;
                mnuComprobantes.Enabled = true;
                BtnGuardar.Enabled = true;
                txttipcambio.Enabled = false;
            }
            bteSubDiario.Focus();
        }

        private void clearControl()
        {
            bteSubDiario.Text = "";
            bteSubDiario.Tag = null;
            txtnumcomprobante.Text = "";
            txttipcambio.Text = "0.0000";
            txtdescripcion.Text = "";
            txtmovimiento.Text = "";
            lkpMoneda.EditValue = null;
            LkpEstado.EditValue = null;
            txtdebesoles.Text = "";
            txtdebedolares.Text = "";
            txthaberdolares.Text = "";
            txthabersoles.Text = "";
        }

        private void cargar()
        {
            mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
            ListaSubdiario = new BContabilidad().listarSubDiario();
            ListaTipoCambio = new BAdministracionSistema().listarTipoCambio();

            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(LkpEstado, new BGeneral().listarTablaRegistro(2), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            if (mlistDetail.Count == 0)
            {
                LkpEstado.EditValue = 4;
            }
        }
        public void cargar2()
        {
            if (modiComp != null)
            {
                DtmFecha.EditValue = Convert.ToDateTime(modiComp.sfec_nota_contable);
                bteSubDiario.Text = String.Format("{0:00}", modiComp.idf_SubDiario);
                bteSubDiario.Tag = modiComp.subdi_icod_subdiario;
                txtnumcomprobante.Text = modiComp.vnumero_voucher_contable;
                txtdescripcion.Text = modiComp.vglosa;
                Tipo_Moneda = Convert.ToInt32(modiComp.tablc_iid_moneda);
                lkpMoneda.EditValue = modiComp.tablc_iid_moneda;
                LkpEstado.EditValue = modiComp.iid_situacion_voucher_contable;
                txttipcambio.Text = modiComp.tipocambio;

            }
        }
        private void loadlista()
        {
            grd.DataSource = mlistDetail.Where(oBe => oBe.ctacc_iid_cuenta_contable_ref == null).ToList();
            txtmovimiento.Text = mlistDetail.Count.ToString();
        }
        private void FrmManteDetalleComprobante_Load(object sender, EventArgs e)
        {
            mlistDetail = new BContabilidad().ListarComprobanteDetalle(code);
            cargar();
            cargar2();
            if (modiComp != null)
            {
                if (modiComp.vcocc_difcambio == 1)
                {
                    txttipcambio.Text = "0.0000";
                    txttipcambio.Tag = null;
                }
            }
            else
            {
                var Lista = ListaTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(DtmFecha.EditValue).ToShortDateString()).ToList();
                Lista.ForEach(obe =>
                {
                    txttipcambio.Text = obe.ticac_tipo_cambio_venta.ToString();
                    txttipcambio.Tag = obe.ticac_icod_tipo_cambio;
                });
            }

            if (Status != BSMaintenanceStatus.CreateNew)
                loadlista();
            gridView1.RefreshData();
            TotalSum();
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
            lkpMoneda.ItemIndex = 0;
            if (CodeMes == DateTime.Now.Month)
                DtmFecha.EditValue = DateTime.Now;
            else
                DtmFecha.EditValue = DateTime.MinValue.AddYears(DateTime.Now.Year - 1).AddMonths(CodeMes - 1);
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            EComprobante obj = new EComprobante();
            BContabilidad obl = new BContabilidad();
            try
            {
                if (string.IsNullOrEmpty(bteSubDiario.Text))
                {
                    oBase = bteSubDiario;
                    throw new ArgumentException("Ingrese Sub - Diario");
                }
                if (bteSubDiario.Tag == null)
                {
                    oBase = bteSubDiario;
                    throw new ArgumentException("Ingrese un Sub - Diario válido");
                }

                if (string.IsNullOrEmpty(txtdescripcion.Text))
                {
                    oBase = txtdescripcion;
                    throw new ArgumentException("Ingrese la descripción");
                }

                if (mlistDetail.Count == 0)
                {
                    if (XtraMessageBox.Show("¿Está seguro que desea grabar el Comprobante SIN DETALLE?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        Flag = true;
                    else
                        Flag = false;
                }
                if (Flag)
                {
                    obj.iid_anio = CodeAnio;
                    obj.iid_mes = CodeMes;
                    obj.iid_voucher_contable = code;
                    //
                    obj.idf_SubDiario = Convert.ToInt32(bteSubDiario.Tag);
                    //
                    obj.sfec_nota_contable = Convert.ToDateTime(DtmFecha.EditValue);
                    obj.vglosa = txtdescripcion.Text;
                    obj.vobservacion = txtdescripcion.Text;
                    obj.vnumero_voucher_contable = txtnumcomprobante.Text.Trim();
                    obj.iid_situacion_voucher_contable = Convert.ToInt32(LkpEstado.EditValue);
                    obj.iid_origen_voucher_contable = corigen_icod;
                    obj.tablc_iid_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                    obj.tablc_iid_tipo_cambio = Convert.ToInt32(txttipcambio.Tag);
                    obj.nmto_tot_debe_sol = mlistDetail.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.nmto_tot_debe_sol);
                    obj.nmto_tot_haber_sol = mlistDetail.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.nmto_tot_haber_sol);
                    obj.nmto_tot_debe_dol = mlistDetail.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.nmto_tot_debe_dol);
                    obj.nmto_tot_haber_dol = mlistDetail.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.nmto_tot_haber_dol);
                    obj.cestado = 'A';
                    obj.tipocambio = txttipcambio.Text;
                    foreach (EComprobanteDetalle comp in mlistDetail)
                    {
                        comp.iusuario_crea = obj.iusuario_crea;
                        comp.vpc_crea = obj.vpc_crea;
                        comp.iusuario_modifica = obj.iusuario_modifica;
                        comp.vpc_modifica = obj.vpc_modifica;
                    }

                    ////if (Status == BSMaintenanceStatus.CreateNew)
                    ////{
                    ////    obj.iusuario_crea = Modules.Valores.intUsuario;
                    ////    obj.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                    ////    obl.InsertarComprobantes(obj, mlistDetail, 0);
                    ////}
                    ////else
                    ////{
                    ////    obj.iusuario_modifica = Modules.Valores.intUsuario;
                    ////    obj.vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                    ////    obl.ModificarComprobante(obj, mlistDetail, mlistDelete);
                    ////}
                }
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
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento();
                    this.Close();
                }
            }
        }

        private void DtmFecha_EditValueChanged(object sender, EventArgs e)
        {
            txttipcambio.Text = "0";
            string auxTipo = "0";
            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.View)
                return;

            if (Convert.ToDateTime(DtmFecha.EditValue).Month != CodeMes || Convert.ToDateTime(DtmFecha.EditValue).Year != CodeAnio)
            {
                XtraMessageBox.Show("La fecha tiene que estar dentro del mes de ejercicio seleccionado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DtmFecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(CodeMes - 1);
            }
            else
            {
                if (mlistDetail.Count > 0)
                {
                    auxTipo = mlistDetail[mlistDetail.Count - 1].tipocambio;
                }

                var Lista = ListaTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(DtmFecha.EditValue).ToShortDateString()).ToList();
                Lista.ForEach(obe =>
                {
                    txttipcambio.Text = obe.ticac_tipo_cambio_venta.ToString();
                });
                if (mlistDetail.Count > 0)
                {
                    if (Convert.ToDecimal(auxTipo) != Convert.ToDecimal(txttipcambio.Text))
                    {
                        if (Convert.ToDecimal(txttipcambio.Text) != 0)
                        {
                            if (modiComp.vcocc_difcambio != 1)
                                recalcular();
                        }
                        else
                            XtraMessageBox.Show("No existe registro de Tipo de Cambio para la fecha seleccionada\nNota: Se requiere registrar Tipo de Cambio para poder continuar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void recalcular()
        {
            mlistDetail.ForEach(x =>
            {
                if (lkpMoneda.Text.Trim() == "NUEVOS SOLES")
                {
                    if (x.nmto_tot_debe_sol > 0 && x.nmto_tot_haber_sol == 0)
                    {
                        x.nmto_tot_debe_sol = x.nmto_tot_debe_sol;
                        x.nmto_tot_debe_dol = Math.Round(Convert.ToDecimal(x.nmto_tot_debe_sol) / Convert.ToDecimal(txttipcambio.Text), 2);
                    }
                    if (x.nmto_tot_haber_sol > 0 && x.nmto_tot_debe_sol == 0)
                    {
                        x.nmto_tot_haber_sol = x.nmto_tot_haber_sol;
                        x.nmto_tot_haber_dol = Math.Round(Convert.ToDecimal(x.nmto_tot_haber_sol) / Convert.ToDecimal(txttipcambio.Text), 2);

                    }
                }
                else if (lkpMoneda.Text.Trim() == "DOLARES")
                {
                    if (x.nmto_tot_debe_dol > 0 && x.nmto_tot_haber_dol == 0)
                    {
                        x.nmto_tot_debe_sol = Math.Round(Convert.ToDecimal(x.nmto_tot_debe_dol) * Convert.ToDecimal(txttipcambio.Text), 2);
                        x.nmto_tot_debe_dol = x.nmto_tot_debe_dol;
                    }
                    if (x.nmto_tot_haber_dol > 0 && x.nmto_tot_debe_dol == 0)
                    {
                        x.nmto_tot_haber_dol = x.nmto_tot_haber_dol;
                        x.nmto_tot_haber_sol = Math.Round(Convert.ToDecimal(x.nmto_tot_haber_dol) * Convert.ToDecimal(txttipcambio.Text), 2);
                    }
                }

            });

            loadlista();
            gridView1.RefreshData();
            TotalSum();
        }

        private void Delete()
        {
            try
            {
                EComprobanteDetalle obj = (EComprobanteDetalle)gridView1.GetRow(gridView1.FocusedRowHandle);
                var Lista = mlistCuenta.Where(Ob => Ob.ctacc_icod_cuenta_contable == Convert.ToInt32(obj.iid_cuenta_contable)).ToList();
                Lista.ForEach(Obe =>
                {
                    obj.iid_cautomatica_debe = Obe.ctacc_icod_cuenta_debe_auto;
                    obj.iid_cautomatica_haber = Obe.ctacc_icod_cuenta_haber_auto;
                });
                if (obj.ctacc_iid_cuenta_contable_ref != null)
                    throw new Exception("No se puede eliminar Cuenta Automática\nNota : Elimine Cuenta principal");
                else
                {
                    if (obj.iid_det_correlat != 0)
                    {
                        if (obj.iid_cautomatica_debe != null)
                        {
                            DeleteCtaAutomatica(obj);
                            obj.operacion = 3;
                        }
                        else
                        {
                            obj.operacion = 3;
                            mlistDelete.Add(obj);
                            mlistDetail.Remove(obj);

                            loadlista();
                            gridView1.RefreshData();
                            TotalSum();
                            Cuadrar();
                        }
                    }
                    else
                    {
                        if (obj.iid_cautomatica_debe != null)
                        {
                            DeleteCtaAutomatica(obj);
                            obj.operacion = 3;
                        }
                        else
                        {
                            obj.operacion = 3;
                            mlistDelete.Add(obj);
                            mlistDetail.Remove(obj);

                            loadlista();
                            gridView1.RefreshData();
                            TotalSum();
                            Cuadrar();
                        }
                    }
                }

                loadlista();
                gridView1.RefreshData();
                TotalSum();
                Cuadrar();
                renumerar();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void renumerar()
        {
            for (int i = 0; i < mlistDetail.Count; i++)
            {
                mlistDetail[i].nro_item_det = i + 1;
                if (mlistDetail[i].operacion == 1) { } else { mlistDetail[i].operacion = 2; }
            }
        }



        private void TotalSum()
        {
            txtdebesoles.Text = gridColumn4.SummaryText;
            txtdebedolares.Text = gridColumn6.SummaryText;
            txthabersoles.Text = gridColumn5.SummaryText;
            txthaberdolares.Text = gridColumn7.SummaryText;
        }


        private void DeleteCtaAutomatica(EComprobanteDetalle ob)
        {
            int location;
            location = mlistDetail.FindIndex(x => x.nro_item_det == ob.nro_item_det);


            mlistDetail[location].operacion = 3;
            mlistDelete.Add(mlistDetail[location]);
            mlistDetail.Remove(mlistDetail[location]);

            var lstAuto = mlistDetail.Where(x => x.ctacc_iid_cuenta_contable_ref == mlistDetail[location].iid_cuenta_contable).ToList();
            if (lstAuto.Count > 0)
            {
                mlistDetail[location + 1].operacion = 3;
                mlistDetail[location + 2].operacion = 3;

                mlistDelete.Add(mlistDetail[location + 1]);
                mlistDelete.Add(mlistDetail[location + 2]);

                mlistDetail.Remove(mlistDetail[location + 2]);
                mlistDetail.Remove(mlistDetail[location + 1]);

            }
        }

        private void Cuadrar()
        {
            if (mlistDetail.Count > 0)
            {
                if (mlistDetail.Sum(x => x.nmto_tot_debe_sol) == mlistDetail.Sum(x => x.nmto_tot_haber_sol) &&
                    mlistDetail.Sum(x => x.nmto_tot_debe_dol) == mlistDetail.Sum(x => x.nmto_tot_haber_dol))
                    LkpEstado.EditValue = 1;
                else
                    LkpEstado.EditValue = 2;
            }
            else
                LkpEstado.EditValue = 4;
            txtmovimiento.Text = mlistDetail.Count.ToString();

        }

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //GridView View = sender as GridView;
            //if (e.RowHandle >= 0)
            //{
            //    string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["id_llave"]);
            //    if (category != "0")
            //    {
            //        e.Appearance.BackColor = Color.LightBlue;
            //        e.Appearance.BackColor2 = Color.SeaShell;

            //    }
            //}

        }



        private void FrmManteDetalleComprobante_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MiEvento();
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private bool VerifyFields()
        {
            BaseEdit oBase = null;
            bool flag = true;
            try
            {
                if (string.IsNullOrEmpty(bteSubDiario.Text))
                {
                    oBase = bteSubDiario;
                    throw new ArgumentException("Ingrese Sub - Diario");
                }
                if (bteSubDiario.Tag == null)
                {
                    oBase = bteSubDiario;
                    throw new ArgumentException("Ingrese un Sub - Diario válido");
                }
                if (string.IsNullOrEmpty(txtdescripcion.Text))
                {
                    oBase = txtdescripcion;
                    throw new ArgumentException("Ingrese la descripción");
                }
                if (Convert.ToDecimal(txttipcambio.Text) == 0)
                {
                    oBase = DtmFecha;
                    throw new ArgumentException("Tipo de Cambio no registrado para la fecha seleccionada\nNota: No se puede continuar sin Tipo de Cambio registrado");
                }

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
                flag = false;
            }
            return flag;

        }
        private void nuevo()
        {
            if (VerifyFields())
            {
                try
                {
                    using (FrmManteDetComp frm = new FrmManteDetComp())
                    {
                        frm.SetAdd();
                        frm.btnModificar.Enabled = false;
                        frm.mlistDetail = mlistDetail;
                        frm.code = code;
                        frm.TipoMoneda = lkpMoneda.Text.Trim();
                        frm.TipoCambio = Convert.ToDecimal(txttipcambio.Text);
                        frm.txtglosa.Text = txtdescripcion.Text;
                        if (mlistDetail.Count > 0)
                        {
                            frm.NumDoc = mlistDetail[mlistDetail.Count - 1].vnumero_documento;
                        }
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            mlistDetail = frm.mlistDetail;
                            loadlista();
                            Cuadrar();
                            TotalSum();
                            gridView1.RefreshData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
            }
        }
        private void modificar()
        {
            if (mlistDetail.Count > 0)
            {
                EComprobanteDetalle obj = (EComprobanteDetalle)gridView1.GetRow(gridView1.FocusedRowHandle);
                if (obj.ctacc_iid_cuenta_contable_ref != null)
                {
                    //XtraMessageBox.Show(obj.ctacc_iid_cuenta_contable_ref.ToString());
                }
                else
                {
                    using (FrmManteDetComp frm = new FrmManteDetComp())
                    {
                        frm.obj = obj;
                        frm.SetModify();
                        frm.btnAgregar.Enabled = false;
                        frm.mlistDetail = mlistDetail;
                        //if (Status == BSMaintenanceStatus.CreateNew) { frm.indicador = true; }
                        //if (Status == BSMaintenanceStatus.ModifyCurrent) { frm.indicador = false; }
                        frm.code = code;
                        frm.TipoMoneda = lkpMoneda.Text;
                        frm.TipoCambio = Convert.ToDecimal(txttipcambio.Text);
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            loadlista();
                            Cuadrar();
                            TotalSum();
                            gridView1.RefreshData();
                        }
                    }
                }
            }
        }
        private void eliminar()
        {
            if (mlistDetail.Count > 0)
                Delete();
        }

        private void bteSubDiario_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarSubDiarios();
        }
        private void ListarSubDiarios()
        {
            using (frmListarSubDiario frm = new frmListarSubDiario())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteSubDiario.Tag = frm._Be.subdi_icod_subdiario;
                    bteSubDiario.Text = frm._Be.subdi_icod_subdiario.ToString();
                    GetVoucherNumber();
                }
            }
        }
        private void bteSubDiario_KeyUp(object sender, KeyEventArgs e)
        {
            List<ESubDiario> aux = new List<ESubDiario>();

            aux = ListaSubdiario.Where(x => x.subdi_icod_subdiario.ToString() == bteSubDiario.Text).ToList();

            if (aux.Count == 1)
            {
                bteSubDiario.Tag = aux[0].subdi_icod_subdiario;
                GetVoucherNumber();

            }
            else
            {
                txtnumcomprobante.Text = string.Empty;
                bteSubDiario.Tag = null;
            }
        }

        private void GetVoucherNumber()
        {
            string numcomp = "0";
            if (oDetail.Count > 0)
            {
                var lista = oDetail.Where(x => x.iid_anio == Parametros.intEjercicio
                 && x.iid_mes == CodeMes && x.subdi_icod_subdiario == Convert.ToInt32(bteSubDiario.Tag)).ToList();

                int auxcomp;
                auxcomp = Convert.ToInt32(lista.Max(x => (x.vnumero_voucher_contable)));

                numcomp = String.Format("{0:00000}", (auxcomp + 1));
                txtnumcomprobante.Text = numcomp;
            }
            else
            {
                numcomp = String.Format("{0:00000}", 1);
                txtnumcomprobante.Text = numcomp;
            }
        }

        private void bteSubDiario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= (char)48 && e.KeyChar <= (char)57 || e.KeyChar <= (char)8))
                e.Handled = false;
            else
                e.Handled = true;
        }


        private void lkpMoneda_EditValueChanged(object sender, EventArgs e)
        {
            if (mlistDetail.Count > 0)
            {
                //recalcular();
                //if (Tipo_Moneda != 0)
                //{
                //    if (Tipo_Moneda != Convert.ToInt32(lkpMoneda.EditValue))
                //    {
                //        recalcular();
                //        Tipo_Moneda = Convert.ToInt32(lkpMoneda.EditValue);
                //    }
                //}
            }
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void bteSubDiario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                ListarSubDiarios();
            }
        }
    }
}

