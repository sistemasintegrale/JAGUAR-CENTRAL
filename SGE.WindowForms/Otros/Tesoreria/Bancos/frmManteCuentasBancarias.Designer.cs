namespace SGE.WindowForms.Otros.Tesoreria.Bancos
{
    partial class frmManteCuentasBancarias
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
            this.btnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.bteCCosto = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.bteAnalitica = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.bteCtaContable = new DevExpress.XtraEditors.ButtonEdit();
            this.txtAnalitica = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.Codigo = new DevExpress.XtraEditors.LabelControl();
            this.txtCodigo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtNroCuenta = new DevExpress.XtraEditors.TextEdit();
            this.lkpSituacion = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpTipoCuenta = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpMoneda = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtCCosto = new DevExpress.XtraEditors.TextEdit();
            this.txtCtaDes = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bteCCosto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteAnalitica.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCtaContable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnalitica.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNroCuenta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSituacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoCuenta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMoneda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCCosto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCtaDes.Properties)).BeginInit();
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
            this.btnGuardar,
            this.btnCancelar});
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnGuardar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCancelar)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnGuardar.Caption = "Guardar";
            this.btnGuardar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_save;
            this.btnGuardar.Id = 0;
            this.btnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGuardar_ItemClick);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Caption = "Cancelar";
            this.btnCancelar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_exit;
            this.btnCancelar.Id = 1;
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
            this.barDockControlTop.Size = new System.Drawing.Size(459, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 167);
            this.barDockControlBottom.Size = new System.Drawing.Size(459, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 167);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(459, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 167);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtCtaDes);
            this.groupControl1.Controls.Add(this.txtCCosto);
            this.groupControl1.Controls.Add(this.bteCCosto);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.bteAnalitica);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.bteCtaContable);
            this.groupControl1.Controls.Add(this.txtAnalitica);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl16);
            this.groupControl1.Controls.Add(this.labelControl15);
            this.groupControl1.Controls.Add(this.Codigo);
            this.groupControl1.Controls.Add(this.txtCodigo);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.txtNroCuenta);
            this.groupControl1.Controls.Add(this.lkpSituacion);
            this.groupControl1.Controls.Add(this.lkpTipoCuenta);
            this.groupControl1.Controls.Add(this.lkpMoneda);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(459, 167);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "Datos";
            // 
            // bteCCosto
            // 
            this.bteCCosto.Location = new System.Drawing.Point(81, 138);
            this.bteCCosto.MenuManager = this.barManager1;
            this.bteCCosto.Name = "bteCCosto";
            this.bteCCosto.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteCCosto.Properties.ReadOnly = true;
            this.bteCCosto.Size = new System.Drawing.Size(100, 20);
            this.bteCCosto.TabIndex = 60;
            this.bteCCosto.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteCCosto_ButtonClick);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(31, 141);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(43, 13);
            this.labelControl1.TabIndex = 59;
            this.labelControl1.Text = "C.Costo:";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(31, 117);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(44, 13);
            this.labelControl8.TabIndex = 58;
            this.labelControl8.Text = "Analítica:";
            // 
            // bteAnalitica
            // 
            this.bteAnalitica.Location = new System.Drawing.Point(81, 114);
            this.bteAnalitica.MenuManager = this.barManager1;
            this.bteAnalitica.Name = "bteAnalitica";
            this.bteAnalitica.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteAnalitica.Properties.ReadOnly = true;
            this.bteAnalitica.Size = new System.Drawing.Size(100, 20);
            this.bteAnalitica.TabIndex = 57;
            this.bteAnalitica.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteAnalitica_ButtonClick);
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(4, 95);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(71, 13);
            this.labelControl7.TabIndex = 56;
            this.labelControl7.Text = "Cta. Contable:";
            // 
            // bteCtaContable
            // 
            this.bteCtaContable.Location = new System.Drawing.Point(81, 92);
            this.bteCtaContable.MenuManager = this.barManager1;
            this.bteCtaContable.Name = "bteCtaContable";
            this.bteCtaContable.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteCtaContable.Properties.ReadOnly = true;
            this.bteCtaContable.Size = new System.Drawing.Size(100, 20);
            this.bteCtaContable.TabIndex = 55;
            this.bteCtaContable.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteCtaContable_ButtonClick);
            // 
            // txtAnalitica
            // 
            this.txtAnalitica.Location = new System.Drawing.Point(284, 114);
            this.txtAnalitica.MenuManager = this.barManager1;
            this.txtAnalitica.Name = "txtAnalitica";
            this.txtAnalitica.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAnalitica.Properties.MaxLength = 12;
            this.txtAnalitica.Size = new System.Drawing.Size(112, 20);
            this.txtAnalitica.TabIndex = 54;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(215, 117);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(67, 13);
            this.labelControl4.TabIndex = 53;
            this.labelControl4.Text = "Cd. Analítica :";
            // 
            // labelControl16
            // 
            this.labelControl16.Location = new System.Drawing.Point(249, 28);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(4, 13);
            this.labelControl16.TabIndex = 51;
            this.labelControl16.Text = ":";
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(249, 71);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(4, 13);
            this.labelControl15.TabIndex = 50;
            this.labelControl15.Text = ":";
            // 
            // Codigo
            // 
            this.Codigo.Location = new System.Drawing.Point(38, 28);
            this.Codigo.Name = "Codigo";
            this.Codigo.Size = new System.Drawing.Size(37, 13);
            this.Codigo.TabIndex = 38;
            this.Codigo.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(81, 25);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCodigo.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCodigo.Size = new System.Drawing.Size(97, 20);
            this.txtCodigo.TabIndex = 39;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(199, 28);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(43, 13);
            this.labelControl5.TabIndex = 40;
            this.labelControl5.Text = "Situación";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(13, 71);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(62, 13);
            this.labelControl2.TabIndex = 46;
            this.labelControl2.Text = "Tipo Cuenta:";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(199, 71);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(38, 13);
            this.labelControl6.TabIndex = 44;
            this.labelControl6.Text = "Moneda";
            // 
            // txtNroCuenta
            // 
            this.txtNroCuenta.Location = new System.Drawing.Point(81, 46);
            this.txtNroCuenta.Name = "txtNroCuenta";
            this.txtNroCuenta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtNroCuenta.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtNroCuenta.Properties.MaxLength = 30;
            this.txtNroCuenta.Size = new System.Drawing.Size(315, 20);
            this.txtNroCuenta.TabIndex = 43;
            // 
            // lkpSituacion
            // 
            this.lkpSituacion.Location = new System.Drawing.Point(259, 25);
            this.lkpSituacion.Name = "lkpSituacion";
            this.lkpSituacion.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpSituacion.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpSituacion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpSituacion.Properties.NullText = "";
            this.lkpSituacion.Size = new System.Drawing.Size(137, 20);
            this.lkpSituacion.TabIndex = 41;
            // 
            // lkpTipoCuenta
            // 
            this.lkpTipoCuenta.Location = new System.Drawing.Point(81, 68);
            this.lkpTipoCuenta.Name = "lkpTipoCuenta";
            this.lkpTipoCuenta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpTipoCuenta.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpTipoCuenta.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTipoCuenta.Properties.NullText = "";
            this.lkpTipoCuenta.Size = new System.Drawing.Size(112, 20);
            this.lkpTipoCuenta.TabIndex = 47;
            // 
            // lkpMoneda
            // 
            this.lkpMoneda.Location = new System.Drawing.Point(259, 68);
            this.lkpMoneda.Name = "lkpMoneda";
            this.lkpMoneda.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpMoneda.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpMoneda.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpMoneda.Properties.NullText = "";
            this.lkpMoneda.Size = new System.Drawing.Size(137, 20);
            this.lkpMoneda.TabIndex = 45;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(21, 51);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(54, 13);
            this.labelControl3.TabIndex = 42;
            this.labelControl3.Text = "Nº Cuenta:";
            // 
            // txtCCosto
            // 
            this.txtCCosto.Location = new System.Drawing.Point(187, 138);
            this.txtCCosto.MenuManager = this.barManager1;
            this.txtCCosto.Name = "txtCCosto";
            this.txtCCosto.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCCosto.Properties.MaxLength = 12;
            this.txtCCosto.Properties.ReadOnly = true;
            this.txtCCosto.Size = new System.Drawing.Size(209, 20);
            this.txtCCosto.TabIndex = 61;
            // 
            // txtCtaDes
            // 
            this.txtCtaDes.Location = new System.Drawing.Point(187, 92);
            this.txtCtaDes.MenuManager = this.barManager1;
            this.txtCtaDes.Name = "txtCtaDes";
            this.txtCtaDes.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCtaDes.Properties.MaxLength = 12;
            this.txtCtaDes.Properties.ReadOnly = true;
            this.txtCtaDes.Size = new System.Drawing.Size(209, 20);
            this.txtCtaDes.TabIndex = 62;
            // 
            // frmManteCuentasBancarias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 194);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmManteCuentasBancarias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento - Cuentas Bancarias";
            this.Load += new System.EventHandler(this.frmManteCuentasBancarias_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bteCCosto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteAnalitica.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCtaContable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnalitica.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNroCuenta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSituacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoCuenta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMoneda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCCosto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCtaDes.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnGuardar;
        private DevExpress.XtraBars.BarButtonItem btnCancelar;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LabelControl Codigo;
        public DevExpress.XtraEditors.TextEdit txtCodigo;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        public DevExpress.XtraEditors.TextEdit txtNroCuenta;
        public DevExpress.XtraEditors.LookUpEdit lkpSituacion;
        public DevExpress.XtraEditors.LookUpEdit lkpTipoCuenta;
        public DevExpress.XtraEditors.LookUpEdit lkpMoneda;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraEditors.TextEdit txtAnalitica;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.ButtonEdit bteCtaContable;
        private DevExpress.XtraEditors.ButtonEdit bteCCosto;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.TextEdit txtCCosto;
        public DevExpress.XtraEditors.TextEdit txtCtaDes;
        public DevExpress.XtraEditors.ButtonEdit bteAnalitica;
    }
}