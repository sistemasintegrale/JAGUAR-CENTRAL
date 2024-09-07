namespace SGE.WindowForms.Otros.Tesoreria.Caja
{
    partial class FrmManteConceptoDeCajaEfectivo
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
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.lkpTipo = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtCuentaDes = new DevExpress.XtraEditors.TextEdit();
            this.bteClaseDoc = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.bteTipoDoc = new DevExpress.XtraEditors.ButtonEdit();
            this.bteCuenta = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescripcion = new DevExpress.XtraEditors.TextEdit();
            this.txtCod = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuentaDes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteClaseDoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteTipoDoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCuenta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCod.Properties)).BeginInit();
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
            this.BtnCancelar1.Hint = "Cancelar";
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
            this.barDockControlTop.Size = new System.Drawing.Size(463, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 126);
            this.barDockControlBottom.Size = new System.Drawing.Size(463, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 126);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(463, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 126);
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl1.Location = new System.Drawing.Point(3, 17);
            this.barDockControl1.Size = new System.Drawing.Size(0, 155);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.lkpTipo);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txtCuentaDes);
            this.groupControl1.Controls.Add(this.bteClaseDoc);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.bteTipoDoc);
            this.groupControl1.Controls.Add(this.bteCuenta);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtDescripcion);
            this.groupControl1.Controls.Add(this.txtCod);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(463, 126);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "Datos";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(82, 93);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(4, 13);
            this.labelControl10.TabIndex = 68;
            this.labelControl10.Text = ":";
            // 
            // lkpTipo
            // 
            this.lkpTipo.Location = new System.Drawing.Point(89, 92);
            this.lkpTipo.Name = "lkpTipo";
            this.lkpTipo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpTipo.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpTipo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTipo.Properties.NullText = "";
            this.lkpTipo.Size = new System.Drawing.Size(139, 20);
            this.lkpTipo.TabIndex = 67;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(5, 93);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(72, 13);
            this.labelControl4.TabIndex = 66;
            this.labelControl4.Text = "Tipo Mov. Caja";
            // 
            // txtCuentaDes
            // 
            this.txtCuentaDes.Enabled = false;
            this.txtCuentaDes.Location = new System.Drawing.Point(185, 70);
            this.txtCuentaDes.Name = "txtCuentaDes";
            this.txtCuentaDes.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCuentaDes.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCuentaDes.Size = new System.Drawing.Size(266, 20);
            this.txtCuentaDes.TabIndex = 6;
            // 
            // bteClaseDoc
            // 
            this.bteClaseDoc.EditValue = "";
            this.bteClaseDoc.Enabled = false;
            this.bteClaseDoc.Location = new System.Drawing.Point(261, 48);
            this.bteClaseDoc.Name = "bteClaseDoc";
            this.bteClaseDoc.Properties.AppearanceDisabled.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bteClaseDoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.bteClaseDoc.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.bteClaseDoc.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.bteClaseDoc.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            this.bteClaseDoc.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.bteClaseDoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteClaseDoc.Properties.ReadOnly = true;
            this.bteClaseDoc.Size = new System.Drawing.Size(83, 20);
            this.bteClaseDoc.TabIndex = 4;
            this.bteClaseDoc.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteClaseDoc_ButtonClick);
            this.bteClaseDoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bteClaseDoc_KeyDown);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(72, 29);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(4, 13);
            this.labelControl9.TabIndex = 65;
            this.labelControl9.Text = ":";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(72, 51);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(4, 13);
            this.labelControl8.TabIndex = 64;
            this.labelControl8.Text = ":";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(72, 73);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(4, 13);
            this.labelControl7.TabIndex = 63;
            this.labelControl7.Text = ":";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(185, 52);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(70, 13);
            this.labelControl6.TabIndex = 62;
            this.labelControl6.Text = "Clase de Doc.:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(121, 29);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(58, 13);
            this.labelControl5.TabIndex = 61;
            this.labelControl5.Text = "Descripción:";
            // 
            // bteTipoDoc
            // 
            this.bteTipoDoc.Location = new System.Drawing.Point(77, 48);
            this.bteTipoDoc.Name = "bteTipoDoc";
            this.bteTipoDoc.Properties.AppearanceDisabled.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bteTipoDoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.bteTipoDoc.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.bteTipoDoc.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.bteTipoDoc.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            this.bteTipoDoc.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.bteTipoDoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteTipoDoc.Properties.ReadOnly = true;
            this.bteTipoDoc.Size = new System.Drawing.Size(102, 20);
            this.bteTipoDoc.TabIndex = 3;
            this.bteTipoDoc.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteTipoDoc_ButtonClick);
            this.bteTipoDoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bteTipoDoc_KeyDown);
            // 
            // bteCuenta
            // 
            this.bteCuenta.Location = new System.Drawing.Point(77, 70);
            this.bteCuenta.Name = "bteCuenta";
            this.bteCuenta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.bteCuenta.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.bteCuenta.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteCuenta.Properties.ReadOnly = true;
            this.bteCuenta.Size = new System.Drawing.Size(102, 20);
            this.bteCuenta.TabIndex = 5;
            this.bteCuenta.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnCtaContable_ButtonClick);
            this.bteCuenta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcuentaContable_KeyDown);
            this.bteCuenta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.bteCuenta_KeyUp);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(4, 73);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(67, 13);
            this.labelControl3.TabIndex = 56;
            this.labelControl3.Text = "Cta. Contable";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(4, 51);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(45, 13);
            this.labelControl1.TabIndex = 54;
            this.labelControl1.Text = "Tipo Doc.";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(4, 29);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(33, 13);
            this.labelControl2.TabIndex = 53;
            this.labelControl2.Text = "Código";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(185, 26);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtDescripcion.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtDescripcion.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Properties.MaxLength = 50;
            this.txtDescripcion.Size = new System.Drawing.Size(266, 20);
            this.txtDescripcion.TabIndex = 2;
            // 
            // txtCod
            // 
            this.txtCod.Location = new System.Drawing.Point(77, 26);
            this.txtCod.Name = "txtCod";
            this.txtCod.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCod.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCod.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCod.Properties.MaxLength = 3;
            this.txtCod.Size = new System.Drawing.Size(38, 20);
            this.txtCod.TabIndex = 1;
            // 
            // FrmManteConceptoDeCajaEfectivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 153);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmManteConceptoDeCajaEfectivo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento - Conceptos de Caja Efectivo";
            this.Load += new System.EventHandler(this.FrmManteConceptoDeCaja_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuentaDes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteClaseDoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteTipoDoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCuenta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCod.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem BtnCancelar1;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        public DevExpress.XtraEditors.ButtonEdit bteTipoDoc;
        public DevExpress.XtraEditors.ButtonEdit bteCuenta;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.TextEdit txtDescripcion;
        public DevExpress.XtraEditors.TextEdit txtCod;
        public DevExpress.XtraBars.BarButtonItem BtnGuardar;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        public DevExpress.XtraEditors.ButtonEdit bteClaseDoc;
        public DevExpress.XtraEditors.TextEdit txtCuentaDes;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraEditors.LookUpEdit lkpTipo;
    }
}