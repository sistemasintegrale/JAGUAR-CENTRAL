namespace SGE.WindowForms.Otros.Planillas
{
    partial class frmRegistroPersonalCcosto
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtAño = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.lkpMes = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtnomCCosto = new DevExpress.XtraEditors.TextEdit();
            this.bteCCosto = new DevExpress.XtraEditors.ButtonEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.dteFecha = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAño.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnomCCosto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCCosto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFecha.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFecha.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtAño);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.lkpMes);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtnomCCosto);
            this.groupControl1.Controls.Add(this.bteCCosto);
            this.groupControl1.Controls.Add(this.labelControl12);
            this.groupControl1.Controls.Add(this.dteFecha);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(587, 83);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Datos de C. Costo";
            // 
            // txtAño
            // 
            this.txtAño.Location = new System.Drawing.Point(50, 25);
            this.txtAño.Name = "txtAño";
            this.txtAño.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAño.Properties.Appearance.Options.UseFont = true;
            this.txtAño.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtAño.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtAño.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAño.Properties.Mask.EditMask = "0000";
            this.txtAño.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            this.txtAño.Properties.MaxLength = 50;
            this.txtAño.Size = new System.Drawing.Size(84, 20);
            this.txtAño.TabIndex = 84;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(18, 27);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(26, 13);
            this.labelControl5.TabIndex = 85;
            this.labelControl5.Text = "Año :";
            // 
            // lkpMes
            // 
            this.lkpMes.Location = new System.Drawing.Point(176, 25);
            this.lkpMes.Name = "lkpMes";
            this.lkpMes.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpMes.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpMes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpMes.Properties.NullText = "";
            this.lkpMes.Size = new System.Drawing.Size(139, 20);
            this.lkpMes.TabIndex = 82;
            this.lkpMes.EditValueChanged += new System.EventHandler(this.lkpMes_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(144, 27);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(26, 13);
            this.labelControl2.TabIndex = 83;
            this.labelControl2.Text = "Mes :";
            // 
            // txtnomCCosto
            // 
            this.txtnomCCosto.EditValue = "";
            this.txtnomCCosto.Enabled = false;
            this.txtnomCCosto.Location = new System.Drawing.Point(321, 51);
            this.txtnomCCosto.Name = "txtnomCCosto";
            this.txtnomCCosto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtnomCCosto.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtnomCCosto.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtnomCCosto.Properties.Mask.ShowPlaceHolders = false;
            this.txtnomCCosto.Properties.MaxLength = 50;
            this.txtnomCCosto.Size = new System.Drawing.Size(256, 20);
            this.txtnomCCosto.TabIndex = 81;
            // 
            // bteCCosto
            // 
            this.bteCCosto.Location = new System.Drawing.Point(225, 51);
            this.bteCCosto.MenuManager = this.barManager1;
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
            this.bteCCosto.Properties.Mask.EditMask = "00.00.00";
            this.bteCCosto.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            this.bteCCosto.Size = new System.Drawing.Size(90, 20);
            this.bteCCosto.TabIndex = 80;
            this.bteCCosto.ToolTip = "Presione F10 para desplegar lista";
            this.bteCCosto.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteCCosto_ButtonClick);
            this.bteCCosto.EditValueChanged += new System.EventHandler(this.bteCCosto_EditValueChanged);
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
            this.barDockControlTop.Size = new System.Drawing.Size(587, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 83);
            this.barDockControlBottom.Size = new System.Drawing.Size(587, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 83);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(587, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 83);
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(170, 54);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(49, 13);
            this.labelControl12.TabIndex = 79;
            this.labelControl12.Text = "C. Costo :";
            // 
            // dteFecha
            // 
            this.dteFecha.EditValue = null;
            this.dteFecha.Location = new System.Drawing.Point(50, 51);
            this.dteFecha.Name = "dteFecha";
            this.dteFecha.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.dteFecha.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.dteFecha.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFecha.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFecha.Size = new System.Drawing.Size(104, 20);
            this.dteFecha.TabIndex = 8;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 54);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Fecha :";
            // 
            // frmRegistroPersonalCcosto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 110);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmRegistroPersonalCcosto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento - Registro C. Costo";
            this.Load += new System.EventHandler(this.frmManteAlmacen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAño.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnomCCosto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCCosto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFecha.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFecha.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnGuardar;
        private DevExpress.XtraBars.BarButtonItem btnCancelar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.DateEdit dteFecha;
        private DevExpress.XtraEditors.ButtonEdit bteCCosto;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        public DevExpress.XtraEditors.TextEdit txtnomCCosto;
        public DevExpress.XtraEditors.LookUpEdit lkpMes;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.TextEdit txtAño;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}