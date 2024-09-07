namespace SGE.WindowForms.Otros.Tesoreria.Caja
{
    partial class FrmManteCajasCorrelativos
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
            this.lkpTipo = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lkpCuenta = new DevExpress.XtraEditors.LookUpEdit();
            this.txtCorrelativo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.lkpTipoDoc = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lkpCaja = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCuenta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCorrelativo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoDoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCaja.Properties)).BeginInit();
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
            this.BtnGuardar.Hint = "Guardar";
            this.BtnGuardar.Id = 0;
            this.BtnGuardar.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_save;
            this.BtnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.BtnGuardar.Name = "BtnGuardar";
            this.BtnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.BtnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnGuardar_ItemClick);
            // 
            // BtnCancelar1
            // 
            this.BtnCancelar1.Caption = "Cancelar";
            this.BtnCancelar1.Id = 1;
            this.BtnCancelar1.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_exit;
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
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(505, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 105);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(505, 28);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 105);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(505, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 105);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.lkpTipo);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.lkpCuenta);
            this.groupControl1.Controls.Add(this.txtCorrelativo);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.lkpTipoDoc);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.lkpCaja);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(505, 105);
            this.groupControl1.TabIndex = 57;
            this.groupControl1.Text = "Datos";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(92, 76);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(4, 13);
            this.labelControl10.TabIndex = 93;
            this.labelControl10.Text = ":";
            // 
            // lkpTipo
            // 
            this.lkpTipo.Location = new System.Drawing.Point(99, 75);
            this.lkpTipo.Name = "lkpTipo";
            this.lkpTipo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpTipo.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpTipo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTipo.Properties.NullText = "";
            this.lkpTipo.Size = new System.Drawing.Size(139, 20);
            this.lkpTipo.TabIndex = 92;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(15, 76);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(72, 13);
            this.labelControl5.TabIndex = 91;
            this.labelControl5.Text = "Tipo Mov. Caja";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(57, 52);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(4, 13);
            this.labelControl3.TabIndex = 90;
            this.labelControl3.Text = ":";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(13, 53);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(40, 13);
            this.labelControl4.TabIndex = 88;
            this.labelControl4.Text = "Cuentas";
            // 
            // lkpCuenta
            // 
            this.lkpCuenta.Location = new System.Drawing.Point(63, 49);
            this.lkpCuenta.Name = "lkpCuenta";
            this.lkpCuenta.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lkpCuenta.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lkpCuenta.Properties.Appearance.Options.UseFont = true;
            this.lkpCuenta.Properties.Appearance.Options.UseForeColor = true;
            this.lkpCuenta.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpCuenta.Properties.NullText = "";
            this.lkpCuenta.Size = new System.Drawing.Size(178, 20);
            this.lkpCuenta.TabIndex = 89;
            // 
            // txtCorrelativo
            // 
            this.txtCorrelativo.EditValue = "";
            this.txtCorrelativo.Location = new System.Drawing.Point(337, 49);
            this.txtCorrelativo.Name = "txtCorrelativo";
            this.txtCorrelativo.Properties.Mask.EditMask = "00000000";
            this.txtCorrelativo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCorrelativo.Properties.Mask.ShowPlaceHolders = false;
            this.txtCorrelativo.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCorrelativo.Properties.MaxLength = 8;
            this.txtCorrelativo.Size = new System.Drawing.Size(77, 20);
            this.txtCorrelativo.TabIndex = 87;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(273, 52);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(57, 13);
            this.labelControl8.TabIndex = 86;
            this.labelControl8.Text = "Correlativo:";
            // 
            // lkpTipoDoc
            // 
            this.lkpTipoDoc.Location = new System.Drawing.Point(320, 23);
            this.lkpTipoDoc.Name = "lkpTipoDoc";
            this.lkpTipoDoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpTipoDoc.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpTipoDoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTipoDoc.Properties.NullText = "";
            this.lkpTipoDoc.Size = new System.Drawing.Size(130, 20);
            this.lkpTipoDoc.TabIndex = 85;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(262, 26);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 13);
            this.labelControl2.TabIndex = 84;
            this.labelControl2.Text = "Tipo Doc. :";
            // 
            // lkpCaja
            // 
            this.lkpCaja.Location = new System.Drawing.Point(51, 23);
            this.lkpCaja.Name = "lkpCaja";
            this.lkpCaja.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpCaja.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpCaja.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpCaja.Properties.NullText = "";
            this.lkpCaja.Size = new System.Drawing.Size(190, 20);
            this.lkpCaja.TabIndex = 83;
            this.lkpCaja.EditValueChanged += new System.EventHandler(this.lkpCaja_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(29, 13);
            this.labelControl1.TabIndex = 76;
            this.labelControl1.Text = "Caja :";
            // 
            // FrmManteCajasCorrelativos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 133);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmManteCajasCorrelativos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento - Cajas Chicas";
            this.Load += new System.EventHandler(this.FrmCajaChica_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCuenta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCorrelativo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoDoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCaja.Properties)).EndInit();
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
        public DevExpress.XtraBars.BarButtonItem BtnGuardar;
        public DevExpress.XtraBars.BarButtonItem BtnCancelar1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.TextEdit txtCorrelativo;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        public DevExpress.XtraEditors.LookUpEdit lkpTipoDoc;
        public DevExpress.XtraEditors.LookUpEdit lkpCaja;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraEditors.LookUpEdit lkpCuenta;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        public DevExpress.XtraEditors.LookUpEdit lkpTipo;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}