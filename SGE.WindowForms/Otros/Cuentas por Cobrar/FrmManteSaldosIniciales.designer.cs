namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    partial class FrmManteSaldosIniciales
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.BtnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.BtnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gcDatos = new DevExpress.XtraEditors.GroupControl();
            this.bteCliente = new DevExpress.XtraEditors.ButtonEdit();
            this.deFechaVencimiento = new DevExpress.XtraEditors.DateEdit();
            this.lblFechaVencimiento = new DevExpress.XtraEditors.LabelControl();
            this.lblConcepto = new DevExpress.XtraEditors.LabelControl();
            this.txtConcepto = new DevExpress.XtraEditors.TextEdit();
            this.txtTipoCambio = new DevExpress.XtraEditors.TextEdit();
            this.lblTipoCambio = new DevExpress.XtraEditors.LabelControl();
            this.LkpTipoMoneda = new DevExpress.XtraEditors.LookUpEdit();
            this.deFechaDocumento = new DevExpress.XtraEditors.DateEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblCliente = new DevExpress.XtraEditors.LabelControl();
            this.lblDescripcionClaseDocumento = new DevExpress.XtraEditors.LabelControl();
            this.txtNumeroDocumento = new DevExpress.XtraEditors.TextEdit();
            this.txtSerie = new DevExpress.XtraEditors.TextEdit();
            this.bteClaseDocumento = new DevExpress.XtraEditors.ButtonEdit();
            this.bteTipoDocumento = new DevExpress.XtraEditors.ButtonEdit();
            this.lblDocumento = new DevExpress.XtraEditors.LabelControl();
            this.gcImporte = new DevExpress.XtraEditors.GroupControl();
            this.lblSubTotalValor = new DevExpress.XtraEditors.LabelControl();
            this.txtInafecto = new DevExpress.XtraEditors.TextEdit();
            this.txtOperacionGrabada = new DevExpress.XtraEditors.TextEdit();
            this.lblSubTotal = new DevExpress.XtraEditors.LabelControl();
            this.lblInafecto = new DevExpress.XtraEditors.LabelControl();
            this.lblOperacionGrabada = new DevExpress.XtraEditors.LabelControl();
            this.gcPrecioVenta = new DevExpress.XtraEditors.GroupControl();
            this.lblSaldoValor = new DevExpress.XtraEditors.LabelControl();
            this.lblSaldo = new DevExpress.XtraEditors.LabelControl();
            this.lblPrecioVentaValor = new DevExpress.XtraEditors.LabelControl();
            this.lblPrecioVenta = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDatos)).BeginInit();
            this.gcDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bteCliente.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaVencimiento.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaVencimiento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConcepto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTipoCambio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpTipoMoneda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaDocumento.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaDocumento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroDocumento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerie.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteClaseDocumento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteTipoDocumento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcImporte)).BeginInit();
            this.gcImporte.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtInafecto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperacionGrabada.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPrecioVenta)).BeginInit();
            this.gcPrecioVenta.SuspendLayout();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.BtnGuardar,
            this.BtnCancelar});
            this.barManager1.MaxItemId = 2;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.BtnGuardar),
            new DevExpress.XtraBars.LinkPersistInfo(this.BtnCancelar)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // BtnGuardar
            // 
            this.BtnGuardar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.BtnGuardar.Caption = "Guardar";
            this.BtnGuardar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_save;
            this.BtnGuardar.Id = 0;
            this.BtnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.BtnGuardar.Name = "BtnGuardar";
            this.BtnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.BtnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnGuardar_ItemClick);
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Caption = "Cancelar";
            this.BtnCancelar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_exit;
            this.BtnCancelar.Id = 1;
            this.BtnCancelar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Escape);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.BtnCancelar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnCancelar_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(756, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 242);
            this.barDockControlBottom.Size = new System.Drawing.Size(756, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 242);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(756, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 242);
            // 
            // gcDatos
            // 
            this.gcDatos.Controls.Add(this.bteCliente);
            this.gcDatos.Controls.Add(this.deFechaVencimiento);
            this.gcDatos.Controls.Add(this.lblFechaVencimiento);
            this.gcDatos.Controls.Add(this.lblConcepto);
            this.gcDatos.Controls.Add(this.txtConcepto);
            this.gcDatos.Controls.Add(this.txtTipoCambio);
            this.gcDatos.Controls.Add(this.lblTipoCambio);
            this.gcDatos.Controls.Add(this.LkpTipoMoneda);
            this.gcDatos.Controls.Add(this.deFechaDocumento);
            this.gcDatos.Controls.Add(this.labelControl9);
            this.gcDatos.Controls.Add(this.labelControl2);
            this.gcDatos.Controls.Add(this.lblCliente);
            this.gcDatos.Controls.Add(this.lblDescripcionClaseDocumento);
            this.gcDatos.Controls.Add(this.txtNumeroDocumento);
            this.gcDatos.Controls.Add(this.txtSerie);
            this.gcDatos.Controls.Add(this.bteClaseDocumento);
            this.gcDatos.Controls.Add(this.bteTipoDocumento);
            this.gcDatos.Controls.Add(this.lblDocumento);
            this.gcDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcDatos.Location = new System.Drawing.Point(0, 0);
            this.gcDatos.Name = "gcDatos";
            this.gcDatos.Size = new System.Drawing.Size(756, 108);
            this.gcDatos.TabIndex = 0;
            this.gcDatos.Text = "Documentos por Cobrar";
            // 
            // bteCliente
            // 
            this.bteCliente.Location = new System.Drawing.Point(435, 25);
            this.bteCliente.Name = "bteCliente";
            this.bteCliente.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteCliente.Properties.ReadOnly = true;
            this.bteCliente.Size = new System.Drawing.Size(310, 20);
            this.bteCliente.TabIndex = 5;
            this.bteCliente.ToolTip = "Presione F10 para desplegar lista";
            this.bteCliente.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteCliente_ButtonClick_1);
            this.bteCliente.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.bteCliente_PreviewKeyDown);
            // 
            // deFechaVencimiento
            // 
            this.deFechaVencimiento.EditValue = null;
            this.deFechaVencimiento.Location = new System.Drawing.Point(628, 51);
            this.deFechaVencimiento.Name = "deFechaVencimiento";
            this.deFechaVencimiento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFechaVencimiento.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deFechaVencimiento.Size = new System.Drawing.Size(116, 20);
            this.deFechaVencimiento.TabIndex = 10;
            // 
            // lblFechaVencimiento
            // 
            this.lblFechaVencimiento.Location = new System.Drawing.Point(529, 54);
            this.lblFechaVencimiento.Name = "lblFechaVencimiento";
            this.lblFechaVencimiento.Size = new System.Drawing.Size(93, 13);
            this.lblFechaVencimiento.TabIndex = 0;
            this.lblFechaVencimiento.Text = "Fecha Vencimiento:";
            // 
            // lblConcepto
            // 
            this.lblConcepto.Location = new System.Drawing.Point(11, 80);
            this.lblConcepto.Name = "lblConcepto";
            this.lblConcepto.Size = new System.Drawing.Size(50, 13);
            this.lblConcepto.TabIndex = 0;
            this.lblConcepto.Text = "Concepto:";
            // 
            // txtConcepto
            // 
            this.txtConcepto.EditValue = "";
            this.txtConcepto.Location = new System.Drawing.Point(76, 77);
            this.txtConcepto.MenuManager = this.barManager1;
            this.txtConcepto.Name = "txtConcepto";
            this.txtConcepto.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConcepto.Properties.MaxLength = 50;
            this.txtConcepto.Size = new System.Drawing.Size(668, 20);
            this.txtConcepto.TabIndex = 9;
            // 
            // txtTipoCambio
            // 
            this.txtTipoCambio.EditValue = "0.0000";
            this.txtTipoCambio.Location = new System.Drawing.Point(471, 51);
            this.txtTipoCambio.MenuManager = this.barManager1;
            this.txtTipoCambio.Name = "txtTipoCambio";
            this.txtTipoCambio.Properties.Mask.EditMask = "n4";
            this.txtTipoCambio.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtTipoCambio.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTipoCambio.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTipoCambio.Size = new System.Drawing.Size(52, 20);
            this.txtTipoCambio.TabIndex = 8;
            // 
            // lblTipoCambio
            // 
            this.lblTipoCambio.Location = new System.Drawing.Point(403, 54);
            this.lblTipoCambio.Name = "lblTipoCambio";
            this.lblTipoCambio.Size = new System.Drawing.Size(62, 13);
            this.lblTipoCambio.TabIndex = 0;
            this.lblTipoCambio.Text = "Tipo Cambio:";
            // 
            // LkpTipoMoneda
            // 
            this.LkpTipoMoneda.Location = new System.Drawing.Point(281, 51);
            this.LkpTipoMoneda.Name = "LkpTipoMoneda";
            this.LkpTipoMoneda.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpTipoMoneda.Properties.NullText = "";
            this.LkpTipoMoneda.Size = new System.Drawing.Size(116, 20);
            this.LkpTipoMoneda.TabIndex = 7;
            // 
            // deFechaDocumento
            // 
            this.deFechaDocumento.EditValue = null;
            this.deFechaDocumento.Location = new System.Drawing.Point(111, 51);
            this.deFechaDocumento.Name = "deFechaDocumento";
            this.deFechaDocumento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFechaDocumento.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deFechaDocumento.Size = new System.Drawing.Size(116, 20);
            this.deFechaDocumento.TabIndex = 6;
            this.deFechaDocumento.EditValueChanged += new System.EventHandler(this.deFechaDocumento_EditValueChanged);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(233, 54);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(42, 13);
            this.labelControl9.TabIndex = 0;
            this.labelControl9.Text = "Moneda:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 54);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(90, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Fecha Documento:";
            // 
            // lblCliente
            // 
            this.lblCliente.Location = new System.Drawing.Point(390, 28);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(37, 13);
            this.lblCliente.TabIndex = 0;
            this.lblCliente.Text = "Cliente:";
            // 
            // lblDescripcionClaseDocumento
            // 
            this.lblDescripcionClaseDocumento.Location = new System.Drawing.Point(321, 28);
            this.lblDescripcionClaseDocumento.Name = "lblDescripcionClaseDocumento";
            this.lblDescripcionClaseDocumento.Size = new System.Drawing.Size(0, 13);
            this.lblDescripcionClaseDocumento.TabIndex = 0;
            // 
            // txtNumeroDocumento
            // 
            this.txtNumeroDocumento.EditValue = "000000";
            this.txtNumeroDocumento.Location = new System.Drawing.Point(243, 25);
            this.txtNumeroDocumento.Name = "txtNumeroDocumento";
            this.txtNumeroDocumento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroDocumento.Properties.Appearance.Options.UseFont = true;
            this.txtNumeroDocumento.Properties.DisplayFormat.FormatString = "d8";
            this.txtNumeroDocumento.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtNumeroDocumento.Properties.EditFormat.FormatString = "d8";
            this.txtNumeroDocumento.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtNumeroDocumento.Properties.Mask.EditMask = "d8";
            this.txtNumeroDocumento.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtNumeroDocumento.Properties.Mask.ShowPlaceHolders = false;
            this.txtNumeroDocumento.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtNumeroDocumento.Properties.MaxLength = 8;
            this.txtNumeroDocumento.Size = new System.Drawing.Size(72, 20);
            this.txtNumeroDocumento.TabIndex = 4;
            // 
            // txtSerie
            // 
            this.txtSerie.EditValue = "";
            this.txtSerie.Location = new System.Drawing.Point(198, 25);
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerie.Properties.Appearance.Options.UseFont = true;
            this.txtSerie.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSerie.Properties.Mask.ShowPlaceHolders = false;
            this.txtSerie.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtSerie.Properties.MaxLength = 4;
            this.txtSerie.Size = new System.Drawing.Size(39, 20);
            this.txtSerie.TabIndex = 3;
            // 
            // bteClaseDocumento
            // 
            this.bteClaseDocumento.AllowDrop = true;
            this.bteClaseDocumento.EditValue = "";
            this.bteClaseDocumento.Location = new System.Drawing.Point(137, 25);
            this.bteClaseDocumento.MenuManager = this.barManager1;
            this.bteClaseDocumento.Name = "bteClaseDocumento";
            this.bteClaseDocumento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteClaseDocumento.Properties.MaxLength = 3;
            this.bteClaseDocumento.Properties.ReadOnly = true;
            this.bteClaseDocumento.Size = new System.Drawing.Size(55, 20);
            this.bteClaseDocumento.TabIndex = 2;
            this.bteClaseDocumento.ToolTip = "Presione F10 para desplegar lista";
            this.bteClaseDocumento.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteClaseDocumento_ButtonClick);
            this.bteClaseDocumento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bteClaseDocumento_KeyDown);
            // 
            // bteTipoDocumento
            // 
            this.bteTipoDocumento.AllowDrop = true;
            this.bteTipoDocumento.EditValue = "";
            this.bteTipoDocumento.Location = new System.Drawing.Point(76, 25);
            this.bteTipoDocumento.MenuManager = this.barManager1;
            this.bteTipoDocumento.Name = "bteTipoDocumento";
            this.bteTipoDocumento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteTipoDocumento.Properties.MaxLength = 3;
            this.bteTipoDocumento.Properties.ReadOnly = true;
            this.bteTipoDocumento.Size = new System.Drawing.Size(55, 20);
            this.bteTipoDocumento.TabIndex = 1;
            this.bteTipoDocumento.ToolTip = "Presione F10 para desplegar lista";
            this.bteTipoDocumento.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteTipoDocumento_ButtonClick);
            this.bteTipoDocumento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bteTipoDocumento_KeyDown);
            // 
            // lblDocumento
            // 
            this.lblDocumento.Location = new System.Drawing.Point(12, 28);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(58, 13);
            this.lblDocumento.TabIndex = 0;
            this.lblDocumento.Text = "Documento:";
            // 
            // gcImporte
            // 
            this.gcImporte.Controls.Add(this.lblSubTotalValor);
            this.gcImporte.Controls.Add(this.txtInafecto);
            this.gcImporte.Controls.Add(this.txtOperacionGrabada);
            this.gcImporte.Controls.Add(this.lblSubTotal);
            this.gcImporte.Controls.Add(this.lblInafecto);
            this.gcImporte.Controls.Add(this.lblOperacionGrabada);
            this.gcImporte.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcImporte.Location = new System.Drawing.Point(0, 108);
            this.gcImporte.Name = "gcImporte";
            this.gcImporte.Size = new System.Drawing.Size(756, 101);
            this.gcImporte.TabIndex = 1;
            this.gcImporte.Text = "Importe";
            // 
            // lblSubTotalValor
            // 
            this.lblSubTotalValor.Location = new System.Drawing.Point(115, 77);
            this.lblSubTotalValor.Name = "lblSubTotalValor";
            this.lblSubTotalValor.Size = new System.Drawing.Size(0, 13);
            this.lblSubTotalValor.TabIndex = 0;
            // 
            // txtInafecto
            // 
            this.txtInafecto.EditValue = "0.00";
            this.txtInafecto.Location = new System.Drawing.Point(115, 51);
            this.txtInafecto.MenuManager = this.barManager1;
            this.txtInafecto.Name = "txtInafecto";
            this.txtInafecto.Properties.Mask.EditMask = "n2";
            this.txtInafecto.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtInafecto.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtInafecto.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtInafecto.Size = new System.Drawing.Size(109, 20);
            this.txtInafecto.TabIndex = 12;
            this.txtInafecto.Enter += new System.EventHandler(this.txtInafecto_Enter);
            this.txtInafecto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtInafecto_KeyUp);
            // 
            // txtOperacionGrabada
            // 
            this.txtOperacionGrabada.EditValue = "0.00";
            this.txtOperacionGrabada.Location = new System.Drawing.Point(115, 25);
            this.txtOperacionGrabada.MenuManager = this.barManager1;
            this.txtOperacionGrabada.Name = "txtOperacionGrabada";
            this.txtOperacionGrabada.Properties.Mask.EditMask = "n2";
            this.txtOperacionGrabada.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtOperacionGrabada.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtOperacionGrabada.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtOperacionGrabada.Size = new System.Drawing.Size(109, 20);
            this.txtOperacionGrabada.TabIndex = 11;
            this.txtOperacionGrabada.Enter += new System.EventHandler(this.txtOperacionGrabada_Enter);
            this.txtOperacionGrabada.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtOperacionGrabada_KeyUp);
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.Location = new System.Drawing.Point(12, 77);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(44, 13);
            this.lblSubTotal.TabIndex = 0;
            this.lblSubTotal.Text = "Subtotal:";
            // 
            // lblInafecto
            // 
            this.lblInafecto.Location = new System.Drawing.Point(12, 54);
            this.lblInafecto.Name = "lblInafecto";
            this.lblInafecto.Size = new System.Drawing.Size(45, 13);
            this.lblInafecto.TabIndex = 0;
            this.lblInafecto.Text = "Inafecto:";
            // 
            // lblOperacionGrabada
            // 
            this.lblOperacionGrabada.Location = new System.Drawing.Point(12, 28);
            this.lblOperacionGrabada.Name = "lblOperacionGrabada";
            this.lblOperacionGrabada.Size = new System.Drawing.Size(97, 13);
            this.lblOperacionGrabada.TabIndex = 0;
            this.lblOperacionGrabada.Text = "Operación Gravada:";
            // 
            // gcPrecioVenta
            // 
            this.gcPrecioVenta.Controls.Add(this.lblSaldoValor);
            this.gcPrecioVenta.Controls.Add(this.lblSaldo);
            this.gcPrecioVenta.Controls.Add(this.lblPrecioVentaValor);
            this.gcPrecioVenta.Controls.Add(this.lblPrecioVenta);
            this.gcPrecioVenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcPrecioVenta.Location = new System.Drawing.Point(0, 209);
            this.gcPrecioVenta.Name = "gcPrecioVenta";
            this.gcPrecioVenta.ShowCaption = false;
            this.gcPrecioVenta.Size = new System.Drawing.Size(756, 33);
            this.gcPrecioVenta.TabIndex = 0;
            // 
            // lblSaldoValor
            // 
            this.lblSaldoValor.Location = new System.Drawing.Point(329, 8);
            this.lblSaldoValor.Name = "lblSaldoValor";
            this.lblSaldoValor.Size = new System.Drawing.Size(0, 13);
            this.lblSaldoValor.TabIndex = 0;
            // 
            // lblSaldo
            // 
            this.lblSaldo.Location = new System.Drawing.Point(281, 8);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(30, 13);
            this.lblSaldo.TabIndex = 0;
            this.lblSaldo.Text = "Saldo:";
            // 
            // lblPrecioVentaValor
            // 
            this.lblPrecioVentaValor.Location = new System.Drawing.Point(115, 8);
            this.lblPrecioVentaValor.Name = "lblPrecioVentaValor";
            this.lblPrecioVentaValor.Size = new System.Drawing.Size(0, 13);
            this.lblPrecioVentaValor.TabIndex = 0;
            // 
            // lblPrecioVenta
            // 
            this.lblPrecioVenta.Location = new System.Drawing.Point(12, 8);
            this.lblPrecioVenta.Name = "lblPrecioVenta";
            this.lblPrecioVenta.Size = new System.Drawing.Size(64, 13);
            this.lblPrecioVenta.TabIndex = 0;
            this.lblPrecioVenta.Text = "Precio Venta:";
            // 
            // FrmManteSaldosIniciales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 269);
            this.Controls.Add(this.gcPrecioVenta);
            this.Controls.Add(this.gcImporte);
            this.Controls.Add(this.gcDatos);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.Name = "FrmManteSaldosIniciales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento de Saldos iniciales";
            this.Load += new System.EventHandler(this.FrmManteSaldosIniciales_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmManteSaldosIniciales_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDatos)).EndInit();
            this.gcDatos.ResumeLayout(false);
            this.gcDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bteCliente.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaVencimiento.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaVencimiento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConcepto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTipoCambio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpTipoMoneda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaDocumento.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaDocumento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroDocumento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerie.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteClaseDocumento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteTipoDocumento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcImporte)).EndInit();
            this.gcImporte.ResumeLayout(false);
            this.gcImporte.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtInafecto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperacionGrabada.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPrecioVenta)).EndInit();
            this.gcPrecioVenta.ResumeLayout(false);
            this.gcPrecioVenta.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem BtnCancelar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraBars.BarButtonItem BtnGuardar;
        private DevExpress.XtraEditors.GroupControl gcDatos;
        private DevExpress.XtraEditors.LabelControl lblDocumento;
        public DevExpress.XtraEditors.TextEdit txtNumeroDocumento;
        public DevExpress.XtraEditors.TextEdit txtSerie;
        private DevExpress.XtraEditors.LabelControl lblCliente;
        public DevExpress.XtraEditors.DateEdit deFechaDocumento;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.LookUpEdit LkpTipoMoneda;
        private DevExpress.XtraEditors.GroupControl gcImporte;
        private DevExpress.XtraEditors.LabelControl lblTipoCambio;
        private DevExpress.XtraEditors.LabelControl lblOperacionGrabada;
        private DevExpress.XtraEditors.LabelControl lblSubTotal;
        private DevExpress.XtraEditors.LabelControl lblInafecto;
        private DevExpress.XtraEditors.LabelControl lblSaldo;
        private DevExpress.XtraEditors.LabelControl lblPrecioVenta;
        public DevExpress.XtraEditors.ButtonEdit bteTipoDocumento;
        public DevExpress.XtraEditors.ButtonEdit bteClaseDocumento;
        public DevExpress.XtraEditors.TextEdit txtTipoCambio;
        public DevExpress.XtraEditors.LabelControl lblSubTotalValor;
        public DevExpress.XtraEditors.TextEdit txtInafecto;
        public DevExpress.XtraEditors.TextEdit txtOperacionGrabada;
        public DevExpress.XtraEditors.LabelControl lblSaldoValor;
        public DevExpress.XtraEditors.LabelControl lblPrecioVentaValor;
        private DevExpress.XtraEditors.LabelControl lblConcepto;
        public DevExpress.XtraEditors.TextEdit txtConcepto;
        public DevExpress.XtraEditors.DateEdit deFechaVencimiento;
        private DevExpress.XtraEditors.LabelControl lblFechaVencimiento;
        public DevExpress.XtraEditors.LabelControl lblDescripcionClaseDocumento;
        public DevExpress.XtraEditors.ButtonEdit bteCliente;
        private DevExpress.XtraEditors.GroupControl gcPrecioVenta;
    }
}