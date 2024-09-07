namespace SGE.WindowForms.Otros.Tesoreria.Caja
{
    partial class FrmManteCajaChica
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
            this.BtnCancelar1 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.bteSubAnalitica = new DevExpress.XtraEditors.ButtonEdit();
            this.txtCuentaDes = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblAnalitica = new DevExpress.XtraEditors.LabelControl();
            this.lkpMoneda = new DevExpress.XtraEditors.LookUpEdit();
            this.bteAnalitica = new DevExpress.XtraEditors.ButtonEdit();
            this.bteCuenta = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtResponsable = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.Re = new DevExpress.XtraEditors.LabelControl();
            this.txtDescripcion = new DevExpress.XtraEditors.TextEdit();
            this.txtNumeroCaja = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bteSubAnalitica.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuentaDes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMoneda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteAnalitica.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCuenta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResponsable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroCaja.Properties)).BeginInit();
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
            this.BtnCancelar1});
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
            new DevExpress.XtraBars.LinkPersistInfo(this.BtnCancelar1)});
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
            this.BtnGuardar.Hint = "Guardar";
            this.BtnGuardar.Id = 0;
            this.BtnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.BtnGuardar.Name = "BtnGuardar";
            this.BtnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.BtnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnGuardar_ItemClick);
            // 
            // BtnCancelar1
            // 
            this.BtnCancelar1.Caption = "Cancelar";
            this.BtnCancelar1.Glyph = global::SGE.WindowForms.Properties.Resources.doc_exit;
            this.BtnCancelar1.Id = 1;
            this.BtnCancelar1.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Escape);
            this.BtnCancelar1.Name = "BtnCancelar1";
            this.BtnCancelar1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.BtnCancelar1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnCancelar1_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(471, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 123);
            this.barDockControlBottom.Size = new System.Drawing.Size(471, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 123);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(471, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 123);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.bteSubAnalitica);
            this.groupControl1.Controls.Add(this.txtCuentaDes);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.lblAnalitica);
            this.groupControl1.Controls.Add(this.lkpMoneda);
            this.groupControl1.Controls.Add(this.bteAnalitica);
            this.groupControl1.Controls.Add(this.bteCuenta);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.txtResponsable);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.Re);
            this.groupControl1.Controls.Add(this.txtDescripcion);
            this.groupControl1.Controls.Add(this.txtNumeroCaja);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(471, 123);
            this.groupControl1.TabIndex = 57;
            this.groupControl1.Text = "Datos";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(80, 73);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(4, 13);
            this.labelControl10.TabIndex = 82;
            this.labelControl10.Text = ":";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(80, 50);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(4, 13);
            this.labelControl9.TabIndex = 81;
            this.labelControl9.Text = ":";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(201, 29);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(4, 13);
            this.labelControl8.TabIndex = 80;
            this.labelControl8.Text = ":";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(80, 29);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(4, 13);
            this.labelControl7.TabIndex = 79;
            this.labelControl7.Text = ":";
            // 
            // bteSubAnalitica
            // 
            this.bteSubAnalitica.Enabled = false;
            this.bteSubAnalitica.Location = new System.Drawing.Point(181, 92);
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
            this.bteSubAnalitica.Size = new System.Drawing.Size(280, 20);
            this.bteSubAnalitica.TabIndex = 8;
            this.bteSubAnalitica.ToolTip = "Presione F10 para desplegar lista...";
            this.bteSubAnalitica.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteSubAnalitica_ButtonClick);
            this.bteSubAnalitica.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bteSubAnalitica_KeyDown);
            // 
            // txtCuentaDes
            // 
            this.txtCuentaDes.Enabled = false;
            this.txtCuentaDes.Location = new System.Drawing.Point(181, 70);
            this.txtCuentaDes.Name = "txtCuentaDes";
            this.txtCuentaDes.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCuentaDes.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCuentaDes.Size = new System.Drawing.Size(280, 20);
            this.txtCuentaDes.TabIndex = 6;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(143, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(54, 13);
            this.labelControl1.TabIndex = 76;
            this.labelControl1.Text = "Descripción";
            // 
            // lblAnalitica
            // 
            this.lblAnalitica.Location = new System.Drawing.Point(80, 95);
            this.lblAnalitica.Name = "lblAnalitica";
            this.lblAnalitica.Size = new System.Drawing.Size(4, 13);
            this.lblAnalitica.TabIndex = 73;
            this.lblAnalitica.Text = ":";
            // 
            // lkpMoneda
            // 
            this.lkpMoneda.Location = new System.Drawing.Point(360, 47);
            this.lkpMoneda.Name = "lkpMoneda";
            this.lkpMoneda.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpMoneda.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpMoneda.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpMoneda.Properties.NullText = "";
            this.lkpMoneda.Size = new System.Drawing.Size(101, 20);
            this.lkpMoneda.TabIndex = 4;
            // 
            // bteAnalitica
            // 
            this.bteAnalitica.Enabled = false;
            this.bteAnalitica.Location = new System.Drawing.Point(87, 92);
            this.bteAnalitica.Name = "bteAnalitica";
            this.bteAnalitica.Properties.AppearanceDisabled.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bteAnalitica.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.bteAnalitica.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.bteAnalitica.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.bteAnalitica.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteAnalitica.Properties.ReadOnly = true;
            this.bteAnalitica.Size = new System.Drawing.Size(92, 20);
            this.bteAnalitica.TabIndex = 7;
            this.bteAnalitica.ToolTip = "Presione F10 para desplegar lista...";
            this.bteAnalitica.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnAnalitica_ButtonClick);
            this.bteAnalitica.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bteAnalitica_KeyDown);
            // 
            // bteCuenta
            // 
            this.bteCuenta.Location = new System.Drawing.Point(87, 70);
            this.bteCuenta.Name = "bteCuenta";
            this.bteCuenta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.bteCuenta.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.bteCuenta.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteCuenta.Size = new System.Drawing.Size(92, 20);
            this.bteCuenta.TabIndex = 5;
            this.bteCuenta.ToolTip = "Presione F10 para desplegar lista...";
            this.bteCuenta.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnCtaContable_ButtonClick);
            this.bteCuenta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bteCuenta_KeyDown);
            this.bteCuenta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.bteCuenta_KeyUp);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(11, 95);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(40, 13);
            this.labelControl5.TabIndex = 69;
            this.labelControl5.Text = "Analítica";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(11, 74);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(67, 13);
            this.labelControl6.TabIndex = 68;
            this.labelControl6.Text = "Cta. Contable";
            // 
            // txtResponsable
            // 
            this.txtResponsable.Enabled = false;
            this.txtResponsable.Location = new System.Drawing.Point(87, 48);
            this.txtResponsable.Name = "txtResponsable";
            this.txtResponsable.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtResponsable.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtResponsable.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtResponsable.Properties.Mask.ShowPlaceHolders = false;
            this.txtResponsable.Size = new System.Drawing.Size(219, 20);
            this.txtResponsable.TabIndex = 3;
            this.txtResponsable.Tag = "";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(312, 51);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(42, 13);
            this.labelControl4.TabIndex = 66;
            this.labelControl4.Text = "Moneda:";
            // 
            // Re
            // 
            this.Re.Location = new System.Drawing.Point(11, 50);
            this.Re.Name = "Re";
            this.Re.Size = new System.Drawing.Size(61, 13);
            this.Re.TabIndex = 65;
            this.Re.Text = "Responsable";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Enabled = false;
            this.txtDescripcion.Location = new System.Drawing.Point(211, 26);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtDescripcion.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtDescripcion.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Properties.Mask.ShowPlaceHolders = false;
            this.txtDescripcion.Size = new System.Drawing.Size(250, 20);
            this.txtDescripcion.TabIndex = 2;
            // 
            // txtNumeroCaja
            // 
            this.txtNumeroCaja.EditValue = "";
            this.txtNumeroCaja.Location = new System.Drawing.Point(87, 26);
            this.txtNumeroCaja.Name = "txtNumeroCaja";
            this.txtNumeroCaja.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtNumeroCaja.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtNumeroCaja.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumeroCaja.Properties.Mask.EditMask = "d4";
            this.txtNumeroCaja.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtNumeroCaja.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtNumeroCaja.Properties.MaxLength = 4;
            this.txtNumeroCaja.Size = new System.Drawing.Size(50, 20);
            this.txtNumeroCaja.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(11, 28);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(37, 13);
            this.labelControl2.TabIndex = 61;
            this.labelControl2.Text = "Caja Nº";
            // 
            // FrmManteCajaChica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 150);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmManteCajaChica";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento - Caja Chica";
            this.Load += new System.EventHandler(this.FrmCajaChica_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bteSubAnalitica.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuentaDes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMoneda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteAnalitica.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCuenta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResponsable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroCaja.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraBars.BarButtonItem BtnGuardar;
        public DevExpress.XtraBars.BarButtonItem BtnCancelar1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl lblAnalitica;
        public DevExpress.XtraEditors.LookUpEdit lkpMoneda;
        public DevExpress.XtraEditors.ButtonEdit bteAnalitica;
        public DevExpress.XtraEditors.ButtonEdit bteCuenta;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        public DevExpress.XtraEditors.TextEdit txtResponsable;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl Re;
        public DevExpress.XtraEditors.TextEdit txtDescripcion;
        public DevExpress.XtraEditors.TextEdit txtNumeroCaja;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.TextEdit txtCuentaDes;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        public DevExpress.XtraEditors.ButtonEdit bteSubAnalitica;
    }
}