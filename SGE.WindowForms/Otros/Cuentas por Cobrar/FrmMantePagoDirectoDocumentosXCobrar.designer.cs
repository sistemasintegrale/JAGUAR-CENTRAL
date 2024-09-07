namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    partial class FrmMantePagoDirectoDocumentosXCobrar
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
            this.lblSaldo = new DevExpress.XtraEditors.LabelControl();
            this.lblDocumentoXCobrar = new DevExpress.XtraEditors.LabelControl();
            this.lblObservacion = new DevExpress.XtraEditors.LabelControl();
            this.txtMonto = new DevExpress.XtraEditors.TextEdit();
            this.lblMonto = new DevExpress.XtraEditors.LabelControl();
            this.txtObservacion = new DevExpress.XtraEditors.TextEdit();
            this.txtTipoCambio = new DevExpress.XtraEditors.TextEdit();
            this.lblTipoCambio = new DevExpress.XtraEditors.LabelControl();
            this.LkpTipoMoneda = new DevExpress.XtraEditors.LookUpEdit();
            this.deFechaDocumento = new DevExpress.XtraEditors.DateEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblDescripcionClaseDocumento = new DevExpress.XtraEditors.LabelControl();
            this.txtNumeroDocumento = new DevExpress.XtraEditors.TextEdit();
            this.bteTipoDocumento = new DevExpress.XtraEditors.ButtonEdit();
            this.lblDocumento = new DevExpress.XtraEditors.LabelControl();
            this.bteAnalitica = new DevExpress.XtraEditors.ButtonEdit();
            this.bteSubAnalitica = new DevExpress.XtraEditors.ButtonEdit();
            this.bteCCosto = new DevExpress.XtraEditors.ButtonEdit();
            this.txtcentrocosto = new DevExpress.XtraEditors.TextEdit();
            this.txtCuentaDes = new DevExpress.XtraEditors.TextEdit();
            this.bteCuenta = new DevExpress.XtraEditors.ButtonEdit();
            this.lblAnalitica = new DevExpress.XtraEditors.LabelControl();
            this.lblCentroCosto = new DevExpress.XtraEditors.LabelControl();
            this.lblCuentaContable = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDatos)).BeginInit();
            this.gcDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTipoCambio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpTipoMoneda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaDocumento.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaDocumento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroDocumento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteTipoDocumento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteAnalitica.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteSubAnalitica.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCCosto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcentrocosto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuentaDes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCuenta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
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
            this.barDockControlTop.Size = new System.Drawing.Size(592, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 227);
            this.barDockControlBottom.Size = new System.Drawing.Size(592, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 227);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(592, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 227);
            // 
            // gcDatos
            // 
            this.gcDatos.Controls.Add(this.lblSaldo);
            this.gcDatos.Controls.Add(this.lblDocumentoXCobrar);
            this.gcDatos.Controls.Add(this.lblObservacion);
            this.gcDatos.Controls.Add(this.txtMonto);
            this.gcDatos.Controls.Add(this.lblMonto);
            this.gcDatos.Controls.Add(this.txtObservacion);
            this.gcDatos.Controls.Add(this.txtTipoCambio);
            this.gcDatos.Controls.Add(this.lblTipoCambio);
            this.gcDatos.Controls.Add(this.LkpTipoMoneda);
            this.gcDatos.Controls.Add(this.deFechaDocumento);
            this.gcDatos.Controls.Add(this.labelControl9);
            this.gcDatos.Controls.Add(this.labelControl2);
            this.gcDatos.Controls.Add(this.lblDescripcionClaseDocumento);
            this.gcDatos.Controls.Add(this.txtNumeroDocumento);
            this.gcDatos.Controls.Add(this.bteTipoDocumento);
            this.gcDatos.Controls.Add(this.lblDocumento);
            this.gcDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcDatos.Location = new System.Drawing.Point(0, 0);
            this.gcDatos.Name = "gcDatos";
            this.gcDatos.Size = new System.Drawing.Size(592, 122);
            this.gcDatos.TabIndex = 0;
            this.gcDatos.Text = "Pagos Directo de Documentos por Cobrar";
            this.gcDatos.Paint += new System.Windows.Forms.PaintEventHandler(this.gcDatos_Paint);
            // 
            // lblSaldo
            // 
            this.lblSaldo.Location = new System.Drawing.Point(302, 31);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(30, 13);
            this.lblSaldo.TabIndex = 0;
            this.lblSaldo.Text = "Saldo:";
            // 
            // lblDocumentoXCobrar
            // 
            this.lblDocumentoXCobrar.Location = new System.Drawing.Point(12, 31);
            this.lblDocumentoXCobrar.Name = "lblDocumentoXCobrar";
            this.lblDocumentoXCobrar.Size = new System.Drawing.Size(113, 13);
            this.lblDocumentoXCobrar.TabIndex = 0;
            this.lblDocumentoXCobrar.Text = "Documento por Cobrar:";
            // 
            // lblObservacion
            // 
            this.lblObservacion.Location = new System.Drawing.Point(15, 98);
            this.lblObservacion.Name = "lblObservacion";
            this.lblObservacion.Size = new System.Drawing.Size(75, 13);
            this.lblObservacion.TabIndex = 0;
            this.lblObservacion.Text = "Observaciones:";
            // 
            // txtMonto
            // 
            this.txtMonto.EditValue = "0.00";
            this.txtMonto.Location = new System.Drawing.Point(278, 72);
            this.txtMonto.MenuManager = this.barManager1;
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Properties.Mask.EditMask = "n2";
            this.txtMonto.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMonto.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMonto.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtMonto.Size = new System.Drawing.Size(103, 20);
            this.txtMonto.TabIndex = 5;
            this.txtMonto.EditValueChanged += new System.EventHandler(this.txtMonto_EditValueChanged);
            // 
            // lblMonto
            // 
            this.lblMonto.Location = new System.Drawing.Point(238, 75);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(34, 13);
            this.lblMonto.TabIndex = 0;
            this.lblMonto.Text = "Monto:";
            // 
            // txtObservacion
            // 
            this.txtObservacion.EditValue = "";
            this.txtObservacion.Location = new System.Drawing.Point(98, 95);
            this.txtObservacion.MenuManager = this.barManager1;
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObservacion.Properties.MaxLength = 50;
            this.txtObservacion.Size = new System.Drawing.Size(372, 20);
            this.txtObservacion.TabIndex = 7;
            // 
            // txtTipoCambio
            // 
            this.txtTipoCambio.EditValue = "0.0000";
            this.txtTipoCambio.Enabled = false;
            this.txtTipoCambio.Location = new System.Drawing.Point(444, 72);
            this.txtTipoCambio.MenuManager = this.barManager1;
            this.txtTipoCambio.Name = "txtTipoCambio";
            this.txtTipoCambio.Properties.Mask.EditMask = "n4";
            this.txtTipoCambio.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtTipoCambio.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTipoCambio.Properties.ReadOnly = true;
            this.txtTipoCambio.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTipoCambio.Size = new System.Drawing.Size(116, 20);
            this.txtTipoCambio.TabIndex = 6;
            this.txtTipoCambio.EditValueChanged += new System.EventHandler(this.txtTipoCambio_EditValueChanged);
            // 
            // lblTipoCambio
            // 
            this.lblTipoCambio.Location = new System.Drawing.Point(404, 75);
            this.lblTipoCambio.Name = "lblTipoCambio";
            this.lblTipoCambio.Size = new System.Drawing.Size(21, 13);
            this.lblTipoCambio.TabIndex = 0;
            this.lblTipoCambio.Text = "T/C:";
            // 
            // LkpTipoMoneda
            // 
            this.LkpTipoMoneda.Location = new System.Drawing.Point(98, 72);
            this.LkpTipoMoneda.Name = "LkpTipoMoneda";
            this.LkpTipoMoneda.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpTipoMoneda.Properties.NullText = "";
            this.LkpTipoMoneda.Size = new System.Drawing.Size(126, 20);
            this.LkpTipoMoneda.TabIndex = 4;
            this.LkpTipoMoneda.EditValueChanged += new System.EventHandler(this.LkpTipoMoneda_EditValueChanged);
            // 
            // deFechaDocumento
            // 
            this.deFechaDocumento.EditValue = null;
            this.deFechaDocumento.Location = new System.Drawing.Point(444, 50);
            this.deFechaDocumento.Name = "deFechaDocumento";
            this.deFechaDocumento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFechaDocumento.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deFechaDocumento.Size = new System.Drawing.Size(116, 20);
            this.deFechaDocumento.TabIndex = 3;
            this.deFechaDocumento.EditValueChanged += new System.EventHandler(this.deFechaDocumento_EditValueChanged);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(48, 75);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(42, 13);
            this.labelControl9.TabIndex = 0;
            this.labelControl9.Text = "Moneda:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(405, 53);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(33, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Fecha:";
            // 
            // lblDescripcionClaseDocumento
            // 
            this.lblDescripcionClaseDocumento.Location = new System.Drawing.Point(256, 53);
            this.lblDescripcionClaseDocumento.Name = "lblDescripcionClaseDocumento";
            this.lblDescripcionClaseDocumento.Size = new System.Drawing.Size(0, 13);
            this.lblDescripcionClaseDocumento.TabIndex = 0;
            // 
            // txtNumeroDocumento
            // 
            this.txtNumeroDocumento.EditValue = "";
            this.txtNumeroDocumento.Location = new System.Drawing.Point(159, 50);
            this.txtNumeroDocumento.Name = "txtNumeroDocumento";
            this.txtNumeroDocumento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroDocumento.Properties.Appearance.Options.UseFont = true;
            this.txtNumeroDocumento.Properties.Mask.ShowPlaceHolders = false;
            this.txtNumeroDocumento.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtNumeroDocumento.Properties.MaxLength = 15;
            this.txtNumeroDocumento.Size = new System.Drawing.Size(85, 20);
            this.txtNumeroDocumento.TabIndex = 2;
            // 
            // bteTipoDocumento
            // 
            this.bteTipoDocumento.EditValue = "";
            this.bteTipoDocumento.Location = new System.Drawing.Point(98, 50);
            this.bteTipoDocumento.MenuManager = this.barManager1;
            this.bteTipoDocumento.Name = "bteTipoDocumento";
            this.bteTipoDocumento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteTipoDocumento.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.bteTipoDocumento.Properties.MaxLength = 3;
            this.bteTipoDocumento.Properties.ReadOnly = true;
            this.bteTipoDocumento.Size = new System.Drawing.Size(55, 20);
            this.bteTipoDocumento.TabIndex = 1;
            this.bteTipoDocumento.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteTipoDocumento_ButtonClick);
            // 
            // lblDocumento
            // 
            this.lblDocumento.Location = new System.Drawing.Point(32, 53);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(58, 13);
            this.lblDocumento.TabIndex = 0;
            this.lblDocumento.Text = "Documento:";
            // 
            // bteAnalitica
            // 
            this.bteAnalitica.Enabled = false;
            this.bteAnalitica.Location = new System.Drawing.Point(97, 69);
            this.bteAnalitica.Name = "bteAnalitica";
            this.bteAnalitica.Properties.AppearanceDisabled.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bteAnalitica.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.bteAnalitica.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.bteAnalitica.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.bteAnalitica.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            this.bteAnalitica.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
            this.bteAnalitica.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.bteAnalitica.Properties.AppearanceReadOnly.Options.UseForeColor = true;
            this.bteAnalitica.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteAnalitica.Properties.ReadOnly = true;
            this.bteAnalitica.Size = new System.Drawing.Size(89, 20);
            this.bteAnalitica.TabIndex = 38;
            this.bteAnalitica.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteAnalitica_ButtonClick);
            // 
            // bteSubAnalitica
            // 
            this.bteSubAnalitica.Enabled = false;
            this.bteSubAnalitica.Location = new System.Drawing.Point(192, 69);
            this.bteSubAnalitica.Name = "bteSubAnalitica";
            this.bteSubAnalitica.Properties.AppearanceDisabled.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bteSubAnalitica.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.bteSubAnalitica.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.bteSubAnalitica.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.bteSubAnalitica.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            this.bteSubAnalitica.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
            this.bteSubAnalitica.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.bteSubAnalitica.Properties.AppearanceReadOnly.Options.UseForeColor = true;
            this.bteSubAnalitica.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteSubAnalitica.Properties.ReadOnly = true;
            this.bteSubAnalitica.Size = new System.Drawing.Size(212, 20);
            this.bteSubAnalitica.TabIndex = 39;
            this.bteSubAnalitica.ToolTip = "Presione F10 para desplegar lista...";
            this.bteSubAnalitica.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteSubAnalitica_ButtonClick);
            // 
            // bteCCosto
            // 
            this.bteCCosto.Enabled = false;
            this.bteCCosto.Location = new System.Drawing.Point(97, 46);
            this.bteCCosto.Name = "bteCCosto";
            this.bteCCosto.Properties.AppearanceDisabled.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bteCCosto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.bteCCosto.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.bteCCosto.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.bteCCosto.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            this.bteCCosto.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
            this.bteCCosto.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.bteCCosto.Properties.AppearanceReadOnly.Options.UseForeColor = true;
            this.bteCCosto.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteCCosto.Properties.Mask.EditMask = "\\d{2}?\\.\\d{2}?\\.\\d{2}?";
            this.bteCCosto.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.bteCCosto.Properties.Mask.ShowPlaceHolders = false;
            this.bteCCosto.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.bteCCosto.Size = new System.Drawing.Size(89, 20);
            this.bteCCosto.TabIndex = 36;
            this.bteCCosto.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteCCosto_ButtonClick);
            this.bteCCosto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.bteCCosto_KeyUp);
            // 
            // txtcentrocosto
            // 
            this.txtcentrocosto.Location = new System.Drawing.Point(192, 46);
            this.txtcentrocosto.Name = "txtcentrocosto";
            this.txtcentrocosto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtcentrocosto.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtcentrocosto.Properties.ReadOnly = true;
            this.txtcentrocosto.Size = new System.Drawing.Size(212, 20);
            this.txtcentrocosto.TabIndex = 37;
            // 
            // txtCuentaDes
            // 
            this.txtCuentaDes.Location = new System.Drawing.Point(192, 24);
            this.txtCuentaDes.Name = "txtCuentaDes";
            this.txtCuentaDes.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCuentaDes.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCuentaDes.Properties.ReadOnly = true;
            this.txtCuentaDes.Size = new System.Drawing.Size(212, 20);
            this.txtCuentaDes.TabIndex = 35;
            // 
            // bteCuenta
            // 
            this.bteCuenta.Location = new System.Drawing.Point(97, 24);
            this.bteCuenta.Name = "bteCuenta";
            this.bteCuenta.Properties.AppearanceDisabled.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bteCuenta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.bteCuenta.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.bteCuenta.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.bteCuenta.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            this.bteCuenta.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
            this.bteCuenta.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.bteCuenta.Properties.AppearanceReadOnly.Options.UseForeColor = true;
            this.bteCuenta.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteCuenta.Size = new System.Drawing.Size(89, 20);
            this.bteCuenta.TabIndex = 34;
            this.bteCuenta.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteCuenta_ButtonClick);
            this.bteCuenta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.bteCuenta_KeyUp);
            // 
            // lblAnalitica
            // 
            this.lblAnalitica.Location = new System.Drawing.Point(45, 72);
            this.lblAnalitica.Name = "lblAnalitica";
            this.lblAnalitica.Size = new System.Drawing.Size(44, 13);
            this.lblAnalitica.TabIndex = 0;
            this.lblAnalitica.Text = "Analítica:";
            // 
            // lblCentroCosto
            // 
            this.lblCentroCosto.Location = new System.Drawing.Point(21, 49);
            this.lblCentroCosto.Name = "lblCentroCosto";
            this.lblCentroCosto.Size = new System.Drawing.Size(68, 13);
            this.lblCentroCosto.TabIndex = 0;
            this.lblCentroCosto.Text = "Centro Costo:";
            // 
            // lblCuentaContable
            // 
            this.lblCuentaContable.Location = new System.Drawing.Point(26, 27);
            this.lblCuentaContable.Name = "lblCuentaContable";
            this.lblCuentaContable.Size = new System.Drawing.Size(63, 13);
            this.lblCuentaContable.TabIndex = 0;
            this.lblCuentaContable.Text = "Nro. Cuenta:";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.bteAnalitica);
            this.groupControl1.Controls.Add(this.txtCuentaDes);
            this.groupControl1.Controls.Add(this.bteSubAnalitica);
            this.groupControl1.Controls.Add(this.lblCuentaContable);
            this.groupControl1.Controls.Add(this.bteCCosto);
            this.groupControl1.Controls.Add(this.lblCentroCosto);
            this.groupControl1.Controls.Add(this.txtcentrocosto);
            this.groupControl1.Controls.Add(this.lblAnalitica);
            this.groupControl1.Controls.Add(this.bteCuenta);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 122);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(592, 105);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "Cuenta Contable";
            // 
            // FrmMantePagoDirectoDocumentosXCobrar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 254);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.gcDatos);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximumSize = new System.Drawing.Size(608, 292);
            this.MinimumSize = new System.Drawing.Size(608, 292);
            this.Name = "FrmMantePagoDirectoDocumentosXCobrar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento de Pago Directos de Documentos por Cobrar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMantePagoDirectoDocumentosXCobrar_FormClosing);
            this.Load += new System.EventHandler(this.FrmMantePagoDirectoDocumentosXCobrar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDatos)).EndInit();
            this.gcDatos.ResumeLayout(false);
            this.gcDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTipoCambio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpTipoMoneda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaDocumento.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaDocumento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroDocumento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteTipoDocumento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteAnalitica.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteSubAnalitica.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCCosto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcentrocosto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuentaDes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCuenta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

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
        public DevExpress.XtraEditors.DateEdit deFechaDocumento;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.LookUpEdit LkpTipoMoneda;
        private DevExpress.XtraEditors.LabelControl lblTipoCambio;
        public DevExpress.XtraEditors.ButtonEdit bteTipoDocumento;
        public DevExpress.XtraEditors.TextEdit txtTipoCambio;
        private DevExpress.XtraEditors.LabelControl lblObservacion;
        public DevExpress.XtraEditors.TextEdit txtObservacion;
        public DevExpress.XtraEditors.TextEdit txtMonto;
        private DevExpress.XtraEditors.LabelControl lblMonto;
        private DevExpress.XtraEditors.LabelControl lblAnalitica;
        private DevExpress.XtraEditors.LabelControl lblCentroCosto;
        private DevExpress.XtraEditors.LabelControl lblCuentaContable;
        public DevExpress.XtraEditors.LabelControl lblDescripcionClaseDocumento;
        private DevExpress.XtraEditors.LabelControl lblDocumentoXCobrar;
        private DevExpress.XtraEditors.LabelControl lblSaldo;
        public DevExpress.XtraEditors.TextEdit txtCuentaDes;
        public DevExpress.XtraEditors.TextEdit txtcentrocosto;
        public DevExpress.XtraEditors.ButtonEdit bteCuenta;
        public DevExpress.XtraEditors.ButtonEdit bteCCosto;
        public DevExpress.XtraEditors.ButtonEdit bteAnalitica;
        public DevExpress.XtraEditors.ButtonEdit bteSubAnalitica;
        private DevExpress.XtraEditors.GroupControl groupControl1;
    }
}