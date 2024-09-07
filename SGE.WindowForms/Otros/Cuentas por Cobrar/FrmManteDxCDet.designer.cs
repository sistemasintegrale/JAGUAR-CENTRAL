namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    partial class FrmManteDxCDet
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
            this.btnAgregar = new DevExpress.XtraBars.BarButtonItem();
            this.btnModificar = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.txtConcepto = new DevExpress.XtraEditors.TextEdit();
            this.labelControl21 = new DevExpress.XtraEditors.LabelControl();
            this.txtMonto = new DevExpress.XtraEditors.TextEdit();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.bteCuenta = new DevExpress.XtraEditors.ButtonEdit();
            this.bteSubAnalitica = new DevExpress.XtraEditors.ButtonEdit();
            this.bteAnalitica = new DevExpress.XtraEditors.ButtonEdit();
            this.txtcentrocosto = new DevExpress.XtraEditors.TextEdit();
            this.txtCuentaDes = new DevExpress.XtraEditors.TextEdit();
            this.bteCCosto = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl24 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl23 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtConcepto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCuenta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteSubAnalitica.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteAnalitica.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcentrocosto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuentaDes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCCosto.Properties)).BeginInit();
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
            this.btnAgregar,
            this.btnModificar,
            this.btnCancelar});
            this.barManager1.MaxItemId = 3;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAgregar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnModificar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCancelar)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnAgregar.Caption = "Agregar";
            this.btnAgregar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_check;
            this.btnAgregar.Id = 0;
            this.btnAgregar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnAgregar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAgregar_ItemClick);
            // 
            // btnModificar
            // 
            this.btnModificar.Caption = "Modificar";
            this.btnModificar.Enabled = false;
            this.btnModificar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.btnModificar.Id = 1;
            this.btnModificar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnModificar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnModificar_ItemClick);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Caption = "Cancelar";
            this.btnCancelar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_exit;
            this.btnCancelar.Id = 2;
            this.btnCancelar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Escape);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnCancelar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCancelar_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(395, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 139);
            this.barDockControlBottom.Size = new System.Drawing.Size(395, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 139);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(395, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 139);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl26);
            this.groupControl1.Controls.Add(this.txtConcepto);
            this.groupControl1.Controls.Add(this.labelControl21);
            this.groupControl1.Controls.Add(this.txtMonto);
            this.groupControl1.Controls.Add(this.labelControl20);
            this.groupControl1.Controls.Add(this.bteCuenta);
            this.groupControl1.Controls.Add(this.bteSubAnalitica);
            this.groupControl1.Controls.Add(this.bteAnalitica);
            this.groupControl1.Controls.Add(this.txtcentrocosto);
            this.groupControl1.Controls.Add(this.txtCuentaDes);
            this.groupControl1.Controls.Add(this.bteCCosto);
            this.groupControl1.Controls.Add(this.labelControl24);
            this.groupControl1.Controls.Add(this.labelControl23);
            this.groupControl1.Controls.Add(this.labelControl22);
            this.groupControl1.Controls.Add(this.labelControl19);
            this.groupControl1.Controls.Add(this.labelControl16);
            this.groupControl1.Controls.Add(this.labelControl17);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(395, 139);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "Datos";
            // 
            // labelControl26
            // 
            this.labelControl26.Location = new System.Drawing.Point(69, 115);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(4, 13);
            this.labelControl26.TabIndex = 36;
            this.labelControl26.Text = ":";
            // 
            // txtConcepto
            // 
            this.txtConcepto.Location = new System.Drawing.Point(81, 112);
            this.txtConcepto.Name = "txtConcepto";
            this.txtConcepto.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.txtConcepto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtConcepto.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtConcepto.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtConcepto.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConcepto.Size = new System.Drawing.Size(302, 20);
            this.txtConcepto.TabIndex = 34;
            // 
            // labelControl21
            // 
            this.labelControl21.Location = new System.Drawing.Point(6, 115);
            this.labelControl21.Name = "labelControl21";
            this.labelControl21.Size = new System.Drawing.Size(46, 13);
            this.labelControl21.TabIndex = 35;
            this.labelControl21.Text = "Concepto";
            // 
            // txtMonto
            // 
            this.txtMonto.EditValue = "0.00";
            this.txtMonto.Location = new System.Drawing.Point(81, 90);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Properties.Appearance.Options.UseTextOptions = true;
            this.txtMonto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtMonto.Properties.Mask.EditMask = "n2";
            this.txtMonto.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMonto.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMonto.Size = new System.Drawing.Size(87, 20);
            this.txtMonto.TabIndex = 32;
            // 
            // labelControl20
            // 
            this.labelControl20.Location = new System.Drawing.Point(6, 93);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(67, 13);
            this.labelControl20.TabIndex = 33;
            this.labelControl20.Text = "Monto           :";
            // 
            // bteCuenta
            // 
            this.bteCuenta.Location = new System.Drawing.Point(81, 25);
            this.bteCuenta.MenuManager = this.barManager1;
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
            this.bteCuenta.Size = new System.Drawing.Size(87, 20);
            this.bteCuenta.TabIndex = 20;
            this.bteCuenta.ToolTip = "Presione F10 para desplegar lista";
            this.bteCuenta.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteCuenta_ButtonClick);
            this.bteCuenta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bteCuenta_KeyDown);
            this.bteCuenta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.bteCuenta_KeyUp);
            // 
            // bteSubAnalitica
            // 
            this.bteSubAnalitica.Enabled = false;
            this.bteSubAnalitica.Location = new System.Drawing.Point(171, 68);
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
            this.bteSubAnalitica.TabIndex = 25;
            this.bteSubAnalitica.ToolTip = "Presione F10 para desplegar lista";
            this.bteSubAnalitica.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteSubAnalitica_ButtonClick);
            this.bteSubAnalitica.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bteSubAnalitica_KeyDown);
            // 
            // bteAnalitica
            // 
            this.bteAnalitica.Enabled = false;
            this.bteAnalitica.Location = new System.Drawing.Point(81, 68);
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
            this.bteAnalitica.Properties.DisplayFormat.FormatString = "{0:00}";
            this.bteAnalitica.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bteAnalitica.Properties.ReadOnly = true;
            this.bteAnalitica.Size = new System.Drawing.Size(87, 20);
            this.bteAnalitica.TabIndex = 24;
            this.bteAnalitica.ToolTip = "Presione F10 para desplegar lista";
            this.bteAnalitica.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteAnalitica_ButtonClick);
            this.bteAnalitica.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bteAnalitica_KeyDown);
            // 
            // txtcentrocosto
            // 
            this.txtcentrocosto.Enabled = false;
            this.txtcentrocosto.Location = new System.Drawing.Point(171, 46);
            this.txtcentrocosto.Name = "txtcentrocosto";
            this.txtcentrocosto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtcentrocosto.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtcentrocosto.Size = new System.Drawing.Size(212, 20);
            this.txtcentrocosto.TabIndex = 23;
            // 
            // txtCuentaDes
            // 
            this.txtCuentaDes.Enabled = false;
            this.txtCuentaDes.Location = new System.Drawing.Point(171, 25);
            this.txtCuentaDes.Name = "txtCuentaDes";
            this.txtCuentaDes.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCuentaDes.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCuentaDes.Size = new System.Drawing.Size(212, 20);
            this.txtCuentaDes.TabIndex = 21;
            // 
            // bteCCosto
            // 
            this.bteCCosto.Enabled = false;
            this.bteCCosto.Location = new System.Drawing.Point(81, 47);
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
            this.bteCCosto.Size = new System.Drawing.Size(87, 20);
            this.bteCCosto.TabIndex = 22;
            this.bteCCosto.ToolTip = "Presione F10 para desplegar lista";
            this.bteCCosto.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteCCosto_ButtonClick);
            this.bteCCosto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bteCCosto_KeyDown);
            this.bteCCosto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.bteCCosto_KeyUp);
            // 
            // labelControl24
            // 
            this.labelControl24.Location = new System.Drawing.Point(69, 71);
            this.labelControl24.Name = "labelControl24";
            this.labelControl24.Size = new System.Drawing.Size(4, 13);
            this.labelControl24.TabIndex = 31;
            this.labelControl24.Text = ":";
            // 
            // labelControl23
            // 
            this.labelControl23.Location = new System.Drawing.Point(69, 49);
            this.labelControl23.Name = "labelControl23";
            this.labelControl23.Size = new System.Drawing.Size(4, 13);
            this.labelControl23.TabIndex = 29;
            this.labelControl23.Text = ":";
            // 
            // labelControl22
            // 
            this.labelControl22.Location = new System.Drawing.Point(69, 28);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(4, 13);
            this.labelControl22.TabIndex = 27;
            this.labelControl22.Text = ":";
            // 
            // labelControl19
            // 
            this.labelControl19.Location = new System.Drawing.Point(6, 71);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(40, 13);
            this.labelControl19.TabIndex = 30;
            this.labelControl19.Text = "Analítica";
            // 
            // labelControl16
            // 
            this.labelControl16.Location = new System.Drawing.Point(6, 50);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(42, 13);
            this.labelControl16.TabIndex = 28;
            this.labelControl16.Text = "C.Costo ";
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(6, 28);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(62, 13);
            this.labelControl17.TabIndex = 26;
            this.labelControl17.Text = "Nro. Cuenta ";
            // 
            // FrmManteDxCDet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 166);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmManteDxCDet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento ";
            this.Load += new System.EventHandler(this.FrmManteMovVariosDet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtConcepto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCuenta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteSubAnalitica.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteAnalitica.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcentrocosto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuentaDes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCCosto.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnCancelar;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.ButtonEdit bteCuenta;
        private DevExpress.XtraEditors.ButtonEdit bteSubAnalitica;
        private DevExpress.XtraEditors.ButtonEdit bteAnalitica;
        public DevExpress.XtraEditors.TextEdit txtcentrocosto;
        public DevExpress.XtraEditors.TextEdit txtCuentaDes;
        private DevExpress.XtraEditors.ButtonEdit bteCCosto;
        private DevExpress.XtraEditors.LabelControl labelControl24;
        private DevExpress.XtraEditors.LabelControl labelControl23;
        private DevExpress.XtraEditors.LabelControl labelControl22;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        public DevExpress.XtraEditors.TextEdit txtMonto;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        private DevExpress.XtraEditors.LabelControl labelControl26;
        public DevExpress.XtraEditors.TextEdit txtConcepto;
        private DevExpress.XtraEditors.LabelControl labelControl21;
        private DevExpress.XtraBars.BarButtonItem btnAgregar;
        private DevExpress.XtraBars.BarButtonItem btnModificar;
    }
}